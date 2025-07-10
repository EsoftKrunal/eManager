using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;


public partial class DD_OFC_JobMaster : System.Web.UI.Page
{
    #region -------- PROPERTIES ------------------

   
    public int JobId
    {
        set { ViewState["JobId"] = value; }
        get { return Common.CastAsInt32(ViewState["JobId"]); }
    }
    public int SubJobId
    {
        set { ViewState["SubJobId"] = value; }
        get { return Common.CastAsInt32(ViewState["SubJobId"]); }
    }

    #endregion -----------------------------------
    Random rnd = new Random();
    protected void Page_Load(object sender, EventArgs e)
    {
        // -------------------------- SESSION CHECK ----------------------------------------------
        ProjectCommon.SessionCheck();
        // -------------------------- SESSION CHECK END ----------------------------------------------
        if (!IsPostBack)
        {
            Session["DDPageId"] = "DD_OFC_JobMaster";
            LoadCategory();
        }
    }
    protected void LoadCategory()
    {
        DataTable dtGroups = new DataTable();
        string strSQL = "SELECT CatId,CatCode + ' : ' + CatName As Cat FROM DD_JobCategory Order By CatCode";
        dtGroups = Common.Execute_Procedures_Select_ByQuery(strSQL);
        ddlDDCategory.DataSource = dtGroups;
        ddlDDCategory.DataTextField = "Cat";
        ddlDDCategory.DataValueField = "CatId";
        ddlDDCategory.DataBind(); 
        ddlDDCategory.Items.Insert(0, new System.Web.UI.WebControls.ListItem("< Select >", "0"));
    }

    protected void ddlDDCategory_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        LoadJobs();
        btnAddJob.Visible = true;
    }    
    protected void LoadJobs()
    {
        DataTable dtJobs = Common.Execute_Procedures_Select_ByQuery("SELECT JobId,CatId,JobCode,JobName,ComponentId FROM DD_Jobs WHERE CatId = " + ddlDDCategory.SelectedValue.Trim());
        rptJobs.DataSource = dtJobs;
        rptJobs.DataBind();
    }
    protected void tvComponents_TreeNodePopulate(object sender, TreeNodeEventArgs e)
    {
        DataTable dtSubSystems;
        string strSubSystems = "SELECT JObId,JobCode,JobName from DD_Jobs Where CatId=" + e.Node.Value.Trim() + " Order By JobCode";
        dtSubSystems = Common.Execute_Procedures_Select_ByQuery(strSubSystems);
        if (dtSubSystems != null)
        {
            for (int k = 0; k < dtSubSystems.Rows.Count; k++)
            {
                TreeNode ssn = new TreeNode();
                ssn.Text = dtSubSystems.Rows[k]["JobCode"].ToString() + " : " + dtSubSystems.Rows[k]["JobName"].ToString();
                ssn.Value = dtSubSystems.Rows[k]["JObId"].ToString();
                ssn.ToolTip = dtSubSystems.Rows[k]["JobName"].ToString();
                ssn.Expanded = false;
                //if (e.Node.Value.Trim().Length == 6)
                //{
                //    strUnits = "SELECT CM.ComponentCode, CM.ComponentName FROM VSL_ComponentMasterForVessel CMV INNER JOIN COMPONENTMASTER CM ON CM.COMPONENTID = CMV.COMPONENTID where LEN(LTRIM(RTRIM(CM.ComponentCode)))= 12 AND LEFT(CM.ComponentCode, 9 )='" + ssn.Value.Trim() + "' AND CMV.VesselCode ='" + ddlVessels.SelectedValue.Trim() + "' AND CMV.Status='A' Order By ComponentCode";
                //    dtUnits = Common.Execute_Procedures_Select_ByQuery(strUnits);
                //    if (dtUnits.Rows.Count > 0)
                //    {
                //        ssn.PopulateOnDemand = true;
                //    }
                //}
                e.Node.ChildNodes.Add(ssn);
            }
        }
    }      

    //------------- Add/ Edit Job Section
    protected void btnAddJob_Click(object sender, EventArgs e)
    {
        JobId = 0;
        dv_Jobs.Visible = true;

        txtJobCode.Text = "";
        txtJobName.Text = "";
        txtComponentCode.Text = "";
        
        txtJobName.Focus();
    }    
    protected void btnSelectJob_Click(object sender, EventArgs e)
    {
        //JobId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        JobId = Common.CastAsInt32(((RadioButton)sender).Attributes["JobId"]);
        btnEditJob.Visible = true;
        btnDeleteJob.Visible = true;
        LoadJobs();

        SubJobId = 0;
        BindSubJobs();
        imgAddSubJob.Visible = true;
    }
    protected void btnEditJob_Click(object sender, EventArgs e)
    {

        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT JobCode,JobName,JobDesc, (SELECT ComponentCode FROM ComponentMaster WHERE  ComponentId = J.ComponentId) AS ComponentCode FROM DD_Jobs J WHERE  J.JobId=" + JobId);

        if (dt.Rows.Count > 0)
        {
            txtJobCode.Text = dt.Rows[0]["JobCode"].ToString();
            txtJobName.Text = dt.Rows[0]["JobName"].ToString();
            txtJobDesc.Text = dt.Rows[0]["JobDesc"].ToString();
            txtComponentCode.Text = dt.Rows[0]["ComponentCode"].ToString();
        }

        dv_Jobs.Visible = true;
        txtJobName.Focus();
    }
    protected void btnSaveJob_Click(object sender, EventArgs e)
    {
        int ComponentId = 0;
        if (ddlDDCategory.SelectedIndex == 0)
        {
            txtJobName.Focus();
            lblMsgJob.ShowMessage("Please select Job Catagory first.",true);
            return;
        }

        if (txtJobName.Text.Trim() == "")
        {
            txtJobName.Focus();
            lblMsgJob.ShowMessage("Please enter Job Name.", true);
            return;
        }
        //DataTable dt1 = Common.Execute_Procedures_Select_ByQuery("SELECT JobName FROM DD_Jobs WHERE JobName='" + txtJobName.Text.Trim() + "' AND JobId <> " + JobId);

        //if (dt1.Rows.Count > 0)
        //{
        //    txtJobName.Focus();
        //    lblMsgJob.Text = "Please check! Job Name already exists.";
        //    return;
        //}

        if (txtComponentCode.Text.Trim() != "")
        {
            DataTable dtComp = Common.Execute_Procedures_Select_ByQuery("SELECT ComponentId FROM ComponentMaster WHERE ComponentCode='" + txtComponentCode.Text.Trim() + "'");
            if (dtComp.Rows.Count <= 0)
            {
                txtComponentCode.Focus();
                lblMsgJob.ShowMessage("Please enter valid component code.", true);
                return;
            }

            ComponentId = Common.CastAsInt32(dtComp.Rows[0]["ComponentId"].ToString());
        }

        try
        {
            Common.Set_Procedures("[dbo].[DD_InsertUpdateJob]");
            Common.Set_ParameterLength(5);
            Common.Set_Parameters(
               new MyParameter("@JobId", JobId),
               new MyParameter("@CatId", ddlDDCategory.SelectedValue.Trim()),
               new MyParameter("@JobName", txtJobName.Text.Trim()),
               new MyParameter("@ComponentId", ComponentId),
               new MyParameter("@JobDesc", txtJobDesc.Text.Trim())
               );
            DataSet ds = new DataSet();
            ds.Clear();
            Boolean res;
            res = Common.Execute_Procedures_IUD(ds);
            if (res)
            {
                LoadJobs();
                dv_Jobs.Visible = false;
                lblMsgJob.ShowMessage("Job Added/ Edited successfully.", false);
            }
        }
        catch (Exception ex)
        {
            lblMsgJob.ShowMessage( "Unable to add/ edit Job.Error :" + ex.Message.ToString(), true);
        }
    }
    protected void btnCloseJob_Click(object sender, EventArgs e)
    {
        txtJobCode.Text = "";
        txtJobName.Text = "";
        txtComponentCode.Text = "";          
        LoadJobs();
        BindSubJobs();
        dv_Jobs.Visible = false;
    }

    //------------- Add/ Edit Sub Job Section
    
    public void BindSubJobs()
    {
        DataTable dtSubJobs = Common.Execute_Procedures_Select_ByQuery("SELECT [SubJobId],[JobId],[SubJobCode],[SubJobName],[Unit],LongDescr, CostCategory, OutsideRepair FROM [dbo].[DD_SubJobs] WHERE [JobId] = " + JobId);
        rptSubJobs.DataSource = dtSubJobs;
        rptSubJobs.DataBind();         
    }
    protected void btnSaveSubJob_Click(object sender, EventArgs e)
    {
        if (ddlDDCategory.SelectedIndex == 0)
        {
            txtJobName.Focus();
            lblMsg_SubJob.ShowMessage("Please select Job Catagory first.",true);
            return;
        }

        if (JobId <= 0)
        {
            txtJobName.Focus();
            lblMsg_SubJob.ShowMessage("Please select Job first.",true);
            return;
        }

        if (txtSubJobName.Text.Trim() == "")
        {
            txtSubJobName.Focus();
            lblMsg_SubJob.ShowMessage("Please enter Short Description.",true);
            return;
        }
       
        try
        {
            Common.Set_Procedures("[dbo].[DD_InsertUpdateSubJob]");
            Common.Set_ParameterLength(8);
            Common.Set_Parameters(
               new MyParameter("@SubJobId", SubJobId),
               new MyParameter("@JobId", JobId),                
               new MyParameter("@SubJobName", txtSubJobName.Text.Trim()),
               new MyParameter("@Unit", txtSubjobUnit.Text.Trim()),
               new MyParameter("@LongDescr", txtLongDescr.Text.Trim()),
               new MyParameter("@CostCategory", rdoYardCost.Checked ? "Y" : "N"),
               new MyParameter("@OutsideRepair", chkOutsideRepair.Checked ? "Y" : "N"),
               new MyParameter("@ReqForJobTrack", chkReqJT.Checked ? "Y" : "N")
               );
            DataSet ds = new DataSet();
            ds.Clear();
            Boolean res;
            res = Common.Execute_Procedures_IUD(ds);
            if (res)
            {
                BindSubJobs();
                dvAddEditSubJob.Visible = false; 
                lblMsg_SubJob.ShowMessage("Job Added/ Edited successfully.", false);
            }
        }
        catch (Exception ex)
        {
            lblMsg_SubJob.ShowMessage("Unable to add/ edit Job.Error :" + ex.Message.ToString(),true);
        }

    }
    protected void btnAddSubJob_Click(object sender, EventArgs e)
    {
        SubJobId = 0;
        txtSubJobCode.Text = "";
        txtSubJobName.Text = "";
        txtSubjobUnit.Text = "";
        txtLongDescr.Text = "";
        rdoYardCost.Checked = true;
        rdoNonYardCost.Checked = false;
        chkOutsideRepair.Checked = false;
        chkReqJT.Checked = true;
        btnEditSubJob.Visible = false;         
        btnSaveSubJob.Visible = true;
        dvAddEditSubJob.Visible = true;
    }
    protected void btnEditSubJob_Click(object sender, EventArgs e)
    {
        btnSaveSubJob.Visible = true;
        btnEditSubJob.Visible = false; 
    }
    
    protected void btnSelectSubJob_Click(object sender, EventArgs e)
    {
        SubJobId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        BindSubJobs();
        ShowSubJobData();
        dvAddEditSubJob.Visible = true;
    }
    protected void btnCloseSubJob_Click(object sender, EventArgs e)
    {
        BindSubJobs();
        dvAddEditSubJob.Visible = false;
        btnEditSubJob.Visible = true;
        btnSaveSubJob.Visible = false;
    }
    
    protected void ShowSubJobData()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT [SubJobCode],[SubJobName],[Unit],LongDescr, CostCategory, OutsideRepair, ReqForJobTrack FROM [dbo].[DD_SubJobs] WHERE [SubJobId] = " + SubJobId + " AND [JobId] = " + JobId);
        txtSubJobCode.Text = dt.Rows[0]["SubJobCode"].ToString();
        txtSubJobName.Text = dt.Rows[0]["SubJobName"].ToString();
        txtSubjobUnit.Text = dt.Rows[0]["Unit"].ToString();
        txtLongDescr.Text = dt.Rows[0]["LongDescr"].ToString();
        rdoYardCost.Checked = (dt.Rows[0]["CostCategory"].ToString() == "Y");
        rdoNonYardCost.Checked = (dt.Rows[0]["CostCategory"].ToString() == "N");
        chkOutsideRepair.Checked = (dt.Rows[0]["OutsideRepair"].ToString() == "Y");
        chkReqJT.Checked = (dt.Rows[0]["ReqForJobTrack"].ToString() == "Y");
    }

    private void CreatePDF(DataTable dtCats, DataTable dtJobs, DataTable dtSubJobs)
    {           
        try
        {

            Document document = new Document(PageSize.A4, 0f, 0f, 10f, 10f);
            System.IO.MemoryStream msReport = new System.IO.MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(document, msReport);
            document.Open();

            //------------ TABLE HEADER FONT 
            iTextSharp.text.Font fCapText_11_Reg = FontFactory.GetFont("ARIAL", 8, iTextSharp.text.Font.NORMAL);
            iTextSharp.text.Font fCapText_11 = FontFactory.GetFont("ARIAL", 8, iTextSharp.text.Font.BOLD);
            iTextSharp.text.Font fCapText_13 = FontFactory.GetFont("ARIAL", 10, iTextSharp.text.Font.BOLD);
            iTextSharp.text.Font fCapText_15 = FontFactory.GetFont("ARIAL", 12, iTextSharp.text.Font.BOLD);


            ////=============================================================================
            //// Page -1 
            //PdfPTable tblPage1 = new PdfPTable(1);
            //tblPage1.HorizontalAlignment = Element.ALIGN_CENTER;
            float[] wsCom = { 100 };
            //tblPage1.SetWidths(wsCom);

            float[] wsCom_90 = { 90 };
            //tblPage1.SetWidths(wsCom);

            //// Heading -----------------
            //PdfPCell cell = new PdfPCell(new Phrase("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\nDRYDOCK REPAIR LIST\n\n", fCapText_15));
            //cell.BorderColor = BaseColor.WHITE;
            //cell.HorizontalAlignment = Element.ALIGN_CENTER;
            //tblPage1.AddCell(cell);
            //// Vessel Name -----------------
            //cell = new PdfPCell(new Phrase(dtSummary.Rows[0]["VESSELNAME"].ToString() + "\n\n", fCapText_13));
            //cell.BorderColor = BaseColor.WHITE;
            //cell.HorizontalAlignment = Element.ALIGN_CENTER;
            //tblPage1.AddCell(cell);
            //// DocketNo -----------------
            //cell = new PdfPCell(new Phrase(dtSummary.Rows[0]["DocketNo"].ToString() + "\n\n", fCapText_13));
            //cell.BorderColor = BaseColor.WHITE;
            //cell.HorizontalAlignment = Element.ALIGN_CENTER;
            //tblPage1.AddCell(cell);

            //document.Add(tblPage1);
            ////=============================================================================
            //// Page-2 
            document.NewPage();

            PdfPTable tblPage2 = new PdfPTable(1);
            tblPage2.HorizontalAlignment = Element.ALIGN_CENTER;
            tblPage2.SetWidths(wsCom);
            // Heading -----------------
            PdfPCell cell;
            cell = new PdfPCell(new Phrase("CONTENTS", fCapText_15));
            cell.BorderColor = Color.WHITE;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            tblPage2.AddCell(cell);

            document.Add(tblPage2);

            PdfPTable tblPage2_1 = new PdfPTable(2);
            float[] wsCom_2_1 = { 5, 95 };
            tblPage2_1.HorizontalAlignment = Element.ALIGN_CENTER;
            tblPage2_1.SetWidths(wsCom_2_1);

            foreach (DataRow dr in dtCats.Rows)
            {
                cell = new PdfPCell(new Phrase(dr["CATCODE"].ToString(), fCapText_11));
                cell.BorderColor = Color.BLACK;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                tblPage2_1.AddCell(cell);

                cell = new PdfPCell(new Phrase(dr["CATNAME"].ToString(), fCapText_11));
                cell.BorderColor = Color.BLACK;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                tblPage2_1.AddCell(cell);
            }

            document.Add(tblPage2_1);

            //=============================================================================
            foreach (DataRow drC in dtCats.Rows)
            {
                document.NewPage();

                PdfPTable tblPage_Cats = new PdfPTable(1);
                tblPage_Cats.HorizontalAlignment = Element.ALIGN_CENTER;
                tblPage_Cats.SetWidths(wsCom);
                // Heading -----------------
                cell = new PdfPCell(new Phrase(drC["CATCODE"].ToString() + " :" + drC["CATNAME"].ToString(), fCapText_15));
                cell.BorderColor = Color.WHITE;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;

                tblPage_Cats.AddCell(cell);
                document.Add(tblPage_Cats);

                DataView dvJobs = dtJobs.DefaultView;
                dvJobs.RowFilter = "CATID=" + drC["CATID"].ToString();
                DataTable dtJobs_Temp = dvJobs.ToTable();

                PdfPTable tblPage_Jobs = new PdfPTable(2);
                float[] wsCom_Jobs = { 7, 93 };
                tblPage_Jobs.HorizontalAlignment = Element.ALIGN_CENTER;
                tblPage_Jobs.SetWidths(wsCom_Jobs);

                foreach (DataRow drJ in dtJobs_Temp.Rows)
                {
                    cell = new PdfPCell(new Phrase(drJ["JOBCODE"].ToString(), fCapText_11));
                    cell.BorderColor = Color.BLACK;
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    tblPage_Jobs.AddCell(cell);

                    DataView dv_SubJobs = dtSubJobs.DefaultView;
                    dv_SubJobs.RowFilter = "JobId=" + drJ["JobId"].ToString();
                    DataTable dt_SubJobs_Temp = dv_SubJobs.ToTable();

                    PdfPTable tblPage_SubJobs = new PdfPTable(1);
                    tblPage_SubJobs.HorizontalAlignment = Element.ALIGN_CENTER;
                    tblPage_SubJobs.SetWidths(wsCom);

                    cell = new PdfPCell();
                    cell.AddElement(new Phrase(drJ["JOBNAME"].ToString() + "\n", fCapText_11));
                    cell.AddElement(new Phrase("Job Description : ", fCapText_11));
                    cell.AddElement(new Phrase(drJ["JobDesc"].ToString(), fCapText_11_Reg));
                    cell.BorderColor = Color.BLACK;
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    tblPage_SubJobs.AddCell(cell);

                    foreach (DataRow drS in dt_SubJobs_Temp.Rows)
                    {
                        // --- table for columer details of subjobs

                        PdfPTable tblPage_SubJobs_OtherDetails = new PdfPTable(4);
                        float[] wsCom_SubJobs_other = { 25, 25, 25, 25 };
                        tblPage_SubJobs_OtherDetails.HorizontalAlignment = Element.ALIGN_CENTER;
                        tblPage_SubJobs_OtherDetails.SetWidths(wsCom_SubJobs_other);

                        cell = new PdfPCell(new Phrase("Job Code", fCapText_11));
                        cell.BorderColor = Color.BLACK;
                        cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        tblPage_SubJobs_OtherDetails.AddCell(cell);

                        //cell = new PdfPCell(new Phrase("Bid Qty", fCapText_11));
                        //cell.BorderColor = Color.BLACK;
                        //cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        //tblPage_SubJobs_OtherDetails.AddCell(cell);

                        cell = new PdfPCell(new Phrase("Unit", fCapText_11));
                        cell.BorderColor = Color.BLACK;
                        cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        tblPage_SubJobs_OtherDetails.AddCell(cell);

                        cell = new PdfPCell(new Phrase("Cost Cat.", fCapText_11));
                        cell.BorderColor = Color.BLACK;
                        cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        tblPage_SubJobs_OtherDetails.AddCell(cell);

                        cell = new PdfPCell(new Phrase("OutSide Repair", fCapText_11));
                        cell.BorderColor = Color.BLACK;
                        cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        tblPage_SubJobs_OtherDetails.AddCell(cell);

                        //---

                        cell = new PdfPCell(new Phrase(drS["SubJobCode"].ToString(), fCapText_11_Reg));
                        cell.BorderColor = Color.BLACK;
                        cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        tblPage_SubJobs_OtherDetails.AddCell(cell);

                        //cell = new PdfPCell(new Phrase(drS["BidQty"].ToString(), fCapText_11_Reg));
                        //cell.BorderColor = Color.BLACK;
                        //cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        //tblPage_SubJobs_OtherDetails.AddCell(cell);

                        cell = new PdfPCell(new Phrase(drS["Unit"].ToString(), fCapText_11_Reg));
                        cell.BorderColor = Color.BLACK;
                        cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        tblPage_SubJobs_OtherDetails.AddCell(cell);

                        cell = new PdfPCell(new Phrase(drS["CostCategory"].ToString(), fCapText_11_Reg));
                        cell.BorderColor = Color.BLACK;
                        cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        tblPage_SubJobs_OtherDetails.AddCell(cell);

                        cell = new PdfPCell(new Phrase(drS["OutSideRepair"].ToString(), fCapText_11_Reg));
                        cell.BorderColor = Color.BLACK;
                        cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        tblPage_SubJobs_OtherDetails.AddCell(cell);

                        //---

                        cell = new PdfPCell();
                        cell.BorderColor = Color.BLACK;
                        cell.Colspan = 5;
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.AddElement(new Phrase("Short Description : ", fCapText_11));
                        cell.AddElement(new Phrase(drS["SubJobName"].ToString(), fCapText_11_Reg));
                        cell.AddElement(new Phrase("Long Description : ", fCapText_11));
                        cell.AddElement(new Phrase(drS["LongDescr"].ToString(), fCapText_11_Reg));
                        //if (drS["AttachmentName"].ToString().Trim() != "")
                        //{
                        //    cell.AddElement(new Phrase("Attachment : " + drS["AttachmentName"].ToString() + "\n\n", fCapText_11));
                        //}

                        tblPage_SubJobs_OtherDetails.AddCell(cell);

                        cell = new PdfPCell(tblPage_SubJobs_OtherDetails);
                        cell.BorderColor = Color.BLACK;
                        cell.Padding = 10;
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        tblPage_SubJobs.AddCell(cell);

                        //--------------


                    }

                    cell = new PdfPCell(tblPage_SubJobs);
                    cell.BorderColor = Color.BLACK;
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    //cell.AddElement(tblPage_SubJobs);                       

                    tblPage_Jobs.AddCell(cell);
                }

                document.Add(tblPage_Jobs);
            }
            //=============================================================================

            document.Close();
            if (File.Exists(Server.MapPath("~/DryDock/" + "JobList.pdf")))
            {
                File.Delete(Server.MapPath("~/DryDock/" + "JobList.pdf"));
            }
            FileStream fs = new FileStream(Server.MapPath("~/DryDock/" + "JobList.pdf"), FileMode.Create);
            byte[] bb = msReport.ToArray();
            fs.Write(bb, 0, bb.Length);
            fs.Flush();
            fs.Close();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "download", "window.open('JobList.pdf?rnd=" + rnd.Next() + "','','','');", true);

        }
        catch (System.Exception ex)
        { 
            ScriptManager.RegisterStartupScript(this, this.GetType(), "print", "alert('Unable to print. Error : " + ex.Message + "');", true);
        }
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {         
        DataTable dtCats = Common.Execute_Procedures_Select_ByQuery("SELECT CATID,CATCODE,CATNAME FROM DD_JobCategory ORDER BY CATCODE");
        DataTable dtJobs = Common.Execute_Procedures_Select_ByQuery("SELECT CATID,JobId,JOBCODE,JOBNAME, JobDesc FROM DD_JOBS ORDER BY JOBCODE");
        DataTable dtSubJobs = Common.Execute_Procedures_Select_ByQuery("SELECT SubJobId,[JobId],[SubJobCode],[SubJobName],[Unit],[LongDescr], [CostCategory], [OutsideRepair] FROM [dbo].[DD_SubJobs]  ORDER BY SubJobCode");

        CreatePDF(dtCats, dtJobs, dtSubJobs);
    }
    protected void btnDeleteJob_Click(object sender, EventArgs e)
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DD_SubJobs WHERE JobId=" + JobId);
        if (dt != null && dt.Rows.Count > 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "del", "alert('Can not delete this job.It has subjobs.');", true);
        }
        else
        {
            Common.Execute_Procedures_Select_ByQuery("DELETE FROM DD_Jobs WHERE JobId=" + JobId + " AND CatId=" + ddlDDCategory.SelectedValue);
            LoadJobs();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "del", "alert('Job deleted successfully.');", true);
        }
    }

    protected void btnDeleteSubJob_Click(object sender, EventArgs e)
    {
        //DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DD_DocketSubJobs WHERE SubjobId=" + SubJobId );
        //if (dt != null && dt.Rows.Count > 0)
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "del", "alert('Can not delete. This subjob is in use.');", true);
        //}
        //else
        //{
            Common.Execute_Procedures_Select_ByQuery("DELETE FROM DD_SubJobs WHERE SubjobId=" + SubJobId );           
            ScriptManager.RegisterStartupScript(this, this.GetType(), "del", "alert('SubJob deleted successfully.');", true);
        //}
    }
    
}
