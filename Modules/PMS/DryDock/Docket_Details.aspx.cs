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
using Ionic.Zip;

public partial class Docket_Details : System.Web.UI.Page
{
    public int DocketId
    {
        get { return Common.CastAsInt32(ViewState["DocketId"]); }
        set { ViewState["DocketId"]=value; }
    }
    public int DocketJobId
    {
        get { return Common.CastAsInt32(ViewState["DocketJobId"]); }
        set { ViewState["DocketJobId"] = value; }
    }
    public int JobId
    {
        get { return Common.CastAsInt32(ViewState["JobId"]); }
        set { ViewState["JobId"] = value; }
    }
    public int DocketSubJobId
    {
        get { return Common.CastAsInt32(ViewState["DocketSubJobId"]); }
        set { ViewState["DocketSubJobId"] = value; }
    }
    public string Status
    {
        get { return ViewState["Status"].ToString(); }
        set { ViewState["Status"] = value; }
    }
    //-------------------------
    
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsgMain.Text = "";
        lblMsgSubJob.Text = "";
        lbl_A_MsgSubJob.Text = "";
        lblDefMsg.Text = "";
        lblMsgImportJC.Text = "";

        // -------------------------- SESSION CHECK ----------------------------------------------
        ProjectCommon.SessionCheck();
        // -------------------------- SESSION CHECK END ----------------------------------------------
        if (!IsPostBack)
        {
            DocketId = Common.CastAsInt32(Request.QueryString["DDId"]);
            DocketJobId = 0;
            JobId = 0;
            DocketSubJobId = 0;
            ddlCat.DataSource = Common.Execute_Procedures_Select_ByQuery("SELECT CATID,CATCODE + ' : ' + CATNAME AS FULLNAME FROM DD_JobCategory ORDER BY CATCODE");
            ddlCat.DataTextField = "FULLNAME";
            ddlCat.DataValueField= "CATID";
            ddlCat.DataBind();

            ddlCat_Def.DataSource = Common.Execute_Procedures_Select_ByQuery("SELECT CATID,CATCODE + ' : ' + CATNAME AS FULLNAME FROM DD_JobCategory ORDER BY CATCODE");
            ddlCat_Def.DataTextField = "FULLNAME";
            ddlCat_Def.DataValueField = "CATID";
            ddlCat_Def.DataBind();
            ddlCat_Def.Items.Insert(0, new System.Web.UI.WebControls.ListItem("< Select >", "0"));

            ShowSummary();
            BindJobs();
            BindPublishHistory();
            ViewState["DeleteVisible"] = false;      
      
            DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DD_Docket_RFQ_Master WHERE DocketId=" + DocketId );
            Status = "";
            if (dt.Rows.Count == 0)
            {
                ViewState["DeleteVisible"] = true;

            }
            else
            {
                DataView dv = dt.DefaultView;
                dv.RowFilter = "Status = 'P' ";
                DataTable dtStatus = dv.ToTable();
                if (dtStatus.Rows.Count > 0)
                {
                    Status = dtStatus.Rows[0]["Status"].ToString();
                }
            }

            btnImportDefect.Visible = (Status != "P");
            btnPublish.Visible = (Status != "P");
            //btnDownloadSOR.Visible = (Status != "P");
        }
    }
    public void ShowSummary()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT *,(SELECT VESSELNAME FROM DBO.VESSEL V WHERE V.VESSELID=D.VESSELID) AS VESSELNAME,PublishNotifyOn,ApprovalOn FROM DD_DocketMaster D WHERE DOCKETID=" + DocketId.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            lblDocketNo.Text = dt.Rows[0]["DocketNo"].ToString();
            lblVessel.Text = dt.Rows[0]["VESSELNAME"].ToString();
            lblType.Text = dt.Rows[0]["DocketType"].ToString();
            lblPlanDuration.Text = Common.ToDateString(dt.Rows[0]["StartDate"]) + " To " + Common.ToDateString(dt.Rows[0]["EndDate"]);
       

            //----------------------------
            btnNotifyToGM.Visible = false;
            btnGMApproval.Visible = false;

            //DataTable dt1 = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DD_Docket_RFQ_Master R WHERE R.DOCKETID=" + DocketId + " AND R.STATUS='P'");
            DataTable dtPosition = Common.Execute_Procedures_Select_ByQuery("select Approval4 from dbo.CrewPlanningApprovalAuthority where LoginID=" + Session["loginid"].ToString());
            DataTable dtPublishCount = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DD_Docket_Publish_History WHERE DOCKETID=" + DocketId);

            bool PositionId = false;
            if (dtPosition.Rows.Count > 0)
            {
                PositionId = Convert.ToBoolean(dtPosition.Rows[0][0]);
            }

            bool GM = PositionId;


            btnNotifyToGM.Visible = Convert.IsDBNull(dt.Rows[0]["ApprovalOn"]) && dtPublishCount.Rows.Count>0;
            btnGMApproval.Visible = Convert.IsDBNull(dt.Rows[0]["ApprovalOn"]) && (!(Convert.IsDBNull(dt.Rows[0]["PublishNotifyOn"]))) && GM;
           
        }
    }
    protected void ddlCat_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        DocketJobId = 0;
        DocketSubJobId = 0;
        btn_A_AddJob.Visible = false; 
        BindJobs();
        rptSubJobs.DataSource = null;
        rptSubJobs.DataBind();
    }
    
    protected void btnSelectJob_Click(object sender, EventArgs e)
    {
        //DocketJobId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        JobId = Common.CastAsInt32(((RadioButton)sender).Attributes["JobId"]);
        DocketJobId = Common.CastAsInt32(((RadioButton)sender).Attributes["DocketJobId"]);

        lblJobCode.Text = ((RadioButton)sender).CssClass;
        BindJobs();
        BindSubJobs();
        btn_A_AddJob.Visible = (Status != "P" );
    }
    protected void btnEditJob_Click(object sender, EventArgs e)
    {
        //JobId = Common.CastAsInt32(((ImageButton)sender).Attributes["JobId"]);
        DocketJobId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);

        DataTable dtJobs = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DD_DOCKETJOBS DJ WHERE DJ.DOCKETID=" + DocketId + " And DJ.DocketJobId=" + DocketJobId);
        txtDescr.Text = "";
        lblJobCode1.Text = "";
        if (dtJobs.Rows.Count == 1)
        {
            lblJobCode1.Text = " - " + dtJobs.Rows[0]["JobCode"].ToString();
            txtDescr.Text = dtJobs.Rows[0]["JobDesc"].ToString();

            dv_ModifyJOb.Visible = (Status != "P");
        }
        
        
    }
    protected void btnSaveJob_Click(object sender, EventArgs e)
    {
        if (DocketJobId > 0)
        {
            Common.Execute_Procedures_Select_ByQuery("UPDATE DD_DOCKETJOBS SET JobDesc='" + txtDescr.Text.Trim().Replace("'", "`") + "' WHERE DOCKETID=" + DocketId + " And DocketJobId=" + DocketJobId);
            dv_ModifyJOb.Visible = false;
            lblMsgMain.Text = "Job description updated successfully.";
        }
    }
    protected void btnCloseJob_Click(object sender, EventArgs e)
    {
        dv_ModifyJOb.Visible = false;
    }

    public void BindJobs()
    {
        DataTable dtJobs = Common.Execute_Procedures_Select_ByQuery("SELECT DJ.DocketJobId,DJ.JobId,DJ.JOBCODE,DJ.JOBNAME, CASE WHEN EXISTS(SELECT RFQId FROM DD_Docket_RFQ_Jobs WHERE DocketJobId = DJ.DocketJobId AND DocketId = " + DocketId + ") THEN 'false' ELSE 'true' END AS DeleteVisible FROM DD_DOCKETJOBS DJ WHERE DJ.DOCKETID=" + DocketId + " And DJ.CatId=" + ddlCat.SelectedValue + " ORDER BY JOBCODE");
        rptJobs.DataSource = dtJobs;
        rptJobs.DataBind();
    }
    public void BindSubJobs()
    {
        DataTable dtSubJobs = Common.Execute_Procedures_Select_ByQuery("SELECT DocketSubJobId,[DocketJobId],[DocketId],[SubJobCode],[SubJobName],AttachmentName,Unit,BidQty FROM [dbo].[DD_DocketSubJobs] WHERE DOCKETID=" + DocketId + " And DocketJobId=" + DocketJobId + " ORDER BY SubJobCode");
        rptSubJobs.DataSource = dtSubJobs;
        rptSubJobs.DataBind();
    }
    public void ShowSubJob()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT SubJobCode,SubJobName,Unit,BidQty,LongDescr, CostCategory, OutsideRepair,isnull(ReqForJobTrack,'Y') as ReqForJobTrack FROM DD_DocketSubJobs WHERE DOCKETID=" + DocketId + " and DocketJobId=" + DocketJobId + " And DocketSubJobId=" + DocketSubJobId.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            lblSubJobCode.Text = dt.Rows[0]["SubJobCode"].ToString();
            txtSubJobName.Text = dt.Rows[0]["SubJobName"].ToString();
            txtSubJobBidQty.Text = dt.Rows[0]["BidQty"].ToString();
            txtSubJobUnit.Text = dt.Rows[0]["Unit"].ToString();
            txtLongDescr.Text = dt.Rows[0]["LongDescr"].ToString();
            rdoYardCost.Checked = (dt.Rows[0]["CostCategory"].ToString() == "Y");
            rdoNonYardCost.Checked = (dt.Rows[0]["CostCategory"].ToString() == "N");
            chkOutsideRepair.Checked = (dt.Rows[0]["OutsideRepair"].ToString() == "Y");
            chkReqJT.Checked = (dt.Rows[0]["ReqForJobTrack"].ToString() == "Y");
        }
        btn_U_SaveJob.Visible = false;
        btn_U_RemoveAttachment.Visible = false;
        btn_U_DeleteJob.Visible = Convert.ToBoolean(ViewState["DeleteVisible"]);
    }

    protected void btn_A_AddJob_Click(object sender, EventArgs e)
    {
        dv_A_SubJobs.Visible = true;
        BindMasterSubJobs();
        txt_A_SubJobName.Text="";
        txt_A_SubJobunit.Text = "";
        txt_A_SubJobBidQty.Text = "";
        txt_A_LongDescr.Text = "";
        rdo_A_YardCost.Checked = true;
        rdo_A_NonYardCost.Checked = false;
        chk_A_OutsideRepair.Checked = false;
        txt_A_SubJobName.Focus();
    }
    protected void btn_A_SaveJob_Click(object sender, EventArgs e)
    {
        //================
        if (pnlMaster.Visible)
        {
            //---------------------
            foreach (RepeaterItem ri in rpt_MasterSubJobs.Items)
            {
                CheckBox ch = (CheckBox)ri.FindControl("chkSelect");
                if (ch.Checked)
                {
                    Common.Execute_Procedures_Select_ByQuery("exec dbo.DD_ImportSubJob " + DocketId + "," + DocketJobId + "," + ch.CssClass);
                }
            }
            BindMasterSubJobs();
            lbl_A_MsgSubJob.Text = "Selected jobs imported successfully.";
        }
        else 
        {
            if (txt_A_SubJobName.Text.Trim() == "")
            {
                lbl_A_MsgSubJob.Text = "Please enter short descr.";
                return;
            }

            try
            {
                string FileName = "";
                byte[] FileContent = new byte[0];
                if (ftp_A_Upload.HasFile)
                    if (ftp_A_Upload.PostedFile.ContentLength > 0)
                    {
                        FileName = System.IO.Path.GetFileName(ftp_A_Upload.PostedFile.FileName);
                        FileContent = ftp_A_Upload.FileBytes;
                    }

                Common.Set_Procedures("[dbo].[DD_InsertJobSpecification_Other]");
                Common.Set_ParameterLength(12);
                Common.Set_Parameters(
                   new MyParameter("@DocketId", DocketId),
                   new MyParameter("@DocketJobId", DocketJobId),
                   new MyParameter("@SubJobName", txt_A_SubJobName.Text.Trim().Replace("'", "`")),
                   new MyParameter("@AttachmentName", FileName),
                   new MyParameter("@Attachment", FileContent),
                   new MyParameter("@Unit", txt_A_SubJobunit.Text.Trim().Replace("'", "`")),
                   new MyParameter("@BidQty",Common.CastAsDecimal(txt_A_SubJobBidQty.Text)),
                   new MyParameter("@LongDescr", txt_A_LongDescr.Text.Trim()),
                   new MyParameter("@CostCategory", rdo_A_YardCost.Checked ? "Y" : "N"),
                   new MyParameter("@OutsideRepair", chk_A_OutsideRepair.Checked ? "Y" : "N"),
                   new MyParameter("@DefectNo", ""),
                   new MyParameter("@ReqForJobTrack", chk_A_ReqJT.Checked ? "Y" : "N")                                                                      
                   );
                DataSet ds = new DataSet();
                ds.Clear();
                Boolean res;
                res = Common.Execute_Procedures_IUD(ds);
                if(res)
                {
                    BindSubJobs();
                    txt_A_SubJobName.Text = "";
                    txt_A_SubJobunit.Text = "";
                    txt_A_SubJobBidQty.Text="";
                    txt_A_LongDescr.Text = "";
                    rdo_A_YardCost.Checked = true;
                    rdo_A_NonYardCost.Checked = false;
                    chk_A_OutsideRepair.Checked = false;
                    chk_A_ReqJT.Checked = false;
                    lbl_A_MsgSubJob.Text = "Job added successfully.";
                }
                else
                {
                    lbl_A_MsgSubJob.Text = "Unable to add nwe job. Error :" + Common.ErrMsg;
                }
            }
            catch (Exception ex)
            {
                lbl_A_MsgSubJob.Text = "Unable to add new job. Error :" + ex.Message;
            }
        
        }
    }
    protected void btn_A_CloseJob_Click(object sender, EventArgs e)
    {
        BindSubJobs();
        dv_A_SubJobs.Visible = false;
    }
    
    protected void radMasterNew_OnCheckedChanged(object sender, EventArgs e)
    {
        pnlMaster.Visible = radMaster.Checked;
        pnlNew.Visible =! radMaster.Checked;
        if (pnlMaster.Visible)
        {
            BindMasterSubJobs();
        }
    }
    protected void BindMasterSubJobs()
    {
        rpt_MasterSubJobs.DataSource = Common.Execute_Procedures_Select_ByQuery("select * from [dbo].[DD_SubJobs] WHERE JOBID=" + JobId + " And SubJobId Not in (SELECT SubJobId FROM DD_DocketSubJobs WHERE DOCKETID=" + DocketId + " And DocketJobId=" + DocketJobId + ")");
        rpt_MasterSubJobs.DataBind();

    }

    protected void btnSelectSubJob_Click(object sender, EventArgs e)
    {
        DocketSubJobId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        BindSubJobs();
        dv_U_SubJobs.Visible = true;
        btn_U_EditJob.Visible = (Status != "P");
        btn_U_DeleteJob.Visible = Convert.ToBoolean(ViewState["DeleteVisible"]);
        ShowSubJob();
    }
    protected void btn_U_EditJob_Click(object sender, EventArgs e)
    {
        btn_U_EditJob.Visible = false;
        btn_U_SaveJob.Visible = true;
        btn_U_RemoveAttachment.Visible = true;
    }
    protected void btn_U_RemoveAttachment_Click(object sender, EventArgs e)
    {
        try
        {
            Common.Execute_Procedures_Select_ByQuery("UPDATE DD_DocketSubJobs SET AttachmentName='',Attachment=NULL WHERE DOCKETID=" + DocketId.ToString() + " AND DOCKETJOBID=" + DocketJobId.ToString() + " AND DOCKETSUBJOBID=" + DocketSubJobId.ToString());
            BindSubJobs();
            lblMsgSubJob.Text = "Attachment removed successfully.";
        }
        catch (Exception ex)
        {
            lblMsgSubJob.Text = "Unable to remove attchment. Error :" + ex.Message;
        }
    }
    
    protected void btn_U_SaveJob_Click(object sender, EventArgs e)
    {
        if (txtSubJobName.Text.Trim() == "")
        {
            lblMsgSubJob.Text = "Please enter short descr.";
            return;
        }
        //================

        string FileName="";
        byte[] FileContent=new byte[0];
        if(flp1.HasFile)
            if(flp1.PostedFile.ContentLength>0)
            {
                FileName=System.IO.Path.GetFileName(flp1.PostedFile.FileName);
                FileContent=flp1.FileBytes;
            }
        try
        {
            Common.Execute_Procedures_Select_ByQuery("UPDATE DD_DocketSubJobs SET SUBJOBNAME ='" + txtSubJobName.Text.Trim().Replace("'","`") + "' WHERE DOCKETID=" + DocketId.ToString() + " AND DOCKETJOBID=" + DocketJobId.ToString() + " AND DOCKETSUBJOBID=" + DocketSubJobId.ToString());
            Common.Set_Procedures("[dbo].[DD_UpdateJobSpecification]");
            Common.Set_ParameterLength(12);
            Common.Set_Parameters(
               new MyParameter("@DocketId", DocketId),
               new MyParameter("@DocketJobId", DocketJobId),
               new MyParameter("@DocketSubJobId", DocketSubJobId),
               new MyParameter("@SubJobName", txtSubJobName.Text.Trim().Replace("'", "`")),
               new MyParameter("@AttachmentName", FileName), 
               new MyParameter("@Attachment", FileContent),
               new MyParameter("@Unit", txtSubJobUnit.Text.Trim().Replace("'", "`")),
               new MyParameter("@BidQty", Common.CastAsDecimal(txtSubJobBidQty.Text)),
               new MyParameter("@LongDescr", txtLongDescr.Text.Trim()),
               new MyParameter("@CostCategory", rdoYardCost.Checked ? "Y" : "N"),
               new MyParameter("@OutsideRepair", chkOutsideRepair.Checked ? "Y" : "N"),
               new MyParameter("@ReqForJobTrack", chkReqJT.Checked ? "Y" : "N")
               );
            DataSet ds = new DataSet();
            ds.Clear();
            Boolean res;
            res = Common.Execute_Procedures_IUD(ds);
            if(res)
            {
                BindSubJobs();
                lblMsgSubJob.Text = "Job details updated successfully.";
            }
            else
            {
                lblMsgSubJob.Text = "Unable to update job details. Error :" + Common.ErrMsg;
            }
        }
        catch (Exception ex)
        {
            lblMsgSubJob.Text = "Unable to update job details. Error :" + ex.Message;
        }
    }
    protected void btn_U_DeleteJob_Click(object sender, EventArgs e)
    {
        try
        {
            Common.Execute_Procedures_Select_ByQuery("DELETE FROM DD_DocketSubJobs WHERE DOCKETID=" + DocketId.ToString() + " AND DOCKETJOBID=" + DocketJobId.ToString() + " AND DOCKETSUBJOBID=" + DocketSubJobId.ToString());
            BindSubJobs();            
            lblMsgSubJob.Text = "Job deleted successfully.";
            btn_U_SaveJob.Visible = false;
            btn_U_RemoveAttachment.Visible = false;
            btn_U_DeleteJob.Visible = false; 
        }
        catch (Exception ex)
        {
            lblMsgSubJob.Text = "Unable to delete job. Error :" + ex.Message;
        }
    }
    protected void btn_U_CloseJob_Click(object sender, EventArgs e)
    {
        btn_U_EditJob.Visible = true;
        dv_U_SubJobs.Visible = false;
        btn_U_SaveJob.Visible = false;
        btn_U_RemoveAttachment.Visible = false;
        btn_U_DeleteJob.Visible = Convert.ToBoolean(ViewState["DeleteVisible"]);
    }

    protected void imgDownload_Click(object sender, EventArgs e)
    {
        int DocketSubJobId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT AttachmentName,Attachment FROM DD_DocketSubJobs WHERE DOCKETID=" + DocketId.ToString() + " AND DOCKETJOBID=" + DocketJobId.ToString() + " AND DOCKETSUBJOBID=" + DocketSubJobId.ToString());
        byte[] buff = (byte[])dt.Rows[0]["Attachment"];
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + dt.Rows[0]["AttachmentName"].ToString());
        Response.BinaryWrite(buff);
        Response.Flush();
        Response.End(); 
    }

    protected void btnImportDefect_Click(object sender, EventArgs e)
    {
        dvImportPMS.Visible = true;
        string strSearch = "SELECT ROW_NUMBER() OVER( ORDER BY DD.VesselCode,CM.ComponentCode) AS SrNo,DD.VesselCode,CM.ComponentId,CM.ComponentCode,CM.ComponentName,DD.DefectNo,REPLACE(CONVERT(varchar, DD.ReportDt,106),' ','-') AS ReportDt,REPLACE(CONVERT(varchar, DD.TargetDt,106),' ','-')  AS TargetDt,  CASE DD.CompStatus WHEN 'W' THEN 'Working' WHEN 'N' THEN 'Not Working' ELSE ' ' END AS CompStatus ,DD.RqnNo,REPLACE(CONVERT(varchar, DD.RqnDate,106),' ','-') AS RqnDate,CM.CriticalType,(Case when CompletionDt is null then 'Open' else 'Closed' end )DefectStatus ,(replace(convert(varchar,CompletionDt,106),' ','-'))CompletionDt, CASE WHEN (DD.TargetDt < getdate() AND CompletionDt IS NULL) THEN 'OD' ELSE '' END AS [Status],( SELECT TOP 1 Remarks FROM VSL_DefectRemarks DR WHERE DR.VesselCode = DD.vesselcode AND DR.DefectNo = DD.DefectNo ORDER BY EnteredOn DESC ) AS Remarks, " +
                         "DefectDetails, " +
                         "( " +
                            "(CASE WHEN Vessel = 'Y' THEN 'Vessel,' ELSE '' END) +" +
                            "(CASE WHEN Spares = 'Y' THEN 'Spares,' ELSE '' END) +" +
                            "(CASE WHEN ShoreAssist = 'Y' THEN 'ShoreAssist,' ELSE '' END) + " +
                            "(CASE WHEN Drydock = 'Y' THEN 'Drydock,' ELSE '' END) + " +
                            "(CASE WHEN Guarentee = 'Y' THEN 'Guarentee' ELSE '' END)" +
                          ") as Responsibility" +
                          ",(RTRIM(CM.ComponentCode)+ ' : ' + CM.ComponentName) AS Component, '' AS FleetName " +
                         "FROM Vsl_DefectDetailsMaster DD INNER JOIN VSL_ComponentMasterForVessel CMV ON CMV.VesselCode = DD.VesselCode AND CMV.ComponentId = DD.ComponentId AND CMV.Status = 'A' AND DD.Status = 'A' INNER JOIN ComponentMaster CM ON CM.ComponentId = CMV.ComponentId WHERE DD.VESSELCODE='" + lblDocketNo.Text.Substring(0, 3) + "' AND CompletionDt IS NULL AND DD.Drydock = 'Y' AND  DD.ShoreAssist = 'Y' AND DD.DefectNo NOT IN (SELECT ISNULL(DefectNo, '') FROM DD_DocketSubJobs WHERE DocketId = " + DocketId + ") ";
        rptDefects.DataSource = Common.Execute_Procedures_Select_ByQuery(strSearch);
        rptDefects.DataBind();

    }
    protected void btnImpCancel_Click(object sender, EventArgs e)
    {
        ddlCat_Def.SelectedIndex = 0;
        ddlCat_Def_OnSelectedIndexChanged(sender, e);
        txtDefBidQty.Text = "";
        txtDefSubjobUnit.Text = "";
        rdoDefYardCost.Checked = true;
        rdoDefNonYardCost.Checked = false;
        chkDefOutsideRepair.Checked = false;
        chkDefReqJT.Checked = false;
        BindSubJobs();
        dvImportPMS.Visible = false;
    }
    protected void ddlCat_Def_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dtJobs = Common.Execute_Procedures_Select_ByQuery("SELECT DocketJobId,JobId,(JOBCODE + ' : ' + JOBNAME ) AS JOBNAME FROM DD_DOCKETJOBS WHERE DOCKETID=" + DocketId + " And CatId=" + ddlCat_Def.SelectedValue + " ORDER BY JOBCODE");
        ddlJobs_Def.DataSource = dtJobs;
        ddlJobs_Def.DataTextField = "JOBNAME";
        ddlJobs_Def.DataValueField = "DocketJobId";           
        ddlJobs_Def.DataBind();
        ddlJobs_Def.Items.Insert(0, new System.Web.UI.WebControls.ListItem("< Select >", "0"));
    }

    protected void btnSaveDefect_Click(object sender, EventArgs e)
    {
        bool Selected = false;

        foreach (RepeaterItem item in rptDefects.Items)
        {
            CheckBox chkSelectDef = (CheckBox)item.FindControl("chkSelectDef");
            if (chkSelectDef.Checked)
            {
                Selected = true;
                break;
            }
        }

        if (!Selected)
        {
            lblDefMsg.Text = "Please select a defect";
            return;
        }

        if (ddlCat_Def.SelectedIndex == 0)
        {
            lblDefMsg.Text = "Please select docking category.";
            ddlCat_Def.Focus();
            return;
        }
        if (ddlJobs_Def.SelectedIndex == 0)
        {
            lblDefMsg.Text = "Please select job.";
            ddlJobs_Def.Focus();
            return;
        }

        foreach (RepeaterItem rptItem in rptDefects.Items)
        {
            CheckBox chkSelectDef = (CheckBox)rptItem.FindControl("chkSelectDef");
            if (chkSelectDef.Checked)
            {
                string DefectNo = chkSelectDef.CssClass.ToString();
                DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT [DefectDetails],[RepairsDetails] FROM [dbo].[VSL_DefectDetailsMaster] WHERE [DefectNo]='" + DefectNo + "'");
                string ShortDescr = dt.Rows[0]["DefectDetails"].ToString();
                string LongDescr = dt.Rows[0]["RepairsDetails"].ToString();

                try
                {
                    
                    byte[] FileContent = new byte[0];                   

                    Common.Set_Procedures("[dbo].[DD_InsertJobSpecification_Other]");
                    Common.Set_ParameterLength(12);
                    Common.Set_Parameters(
                       new MyParameter("@DocketId", DocketId),
                       new MyParameter("@DocketJobId", ddlJobs_Def.SelectedValue.Trim()),
                       new MyParameter("@SubJobName", ShortDescr),
                       new MyParameter("@AttachmentName", ""),
                       new MyParameter("@Attachment", FileContent),
                       new MyParameter("@Unit", txtDefSubjobUnit.Text.Trim().Replace("'", "`")),
                       new MyParameter("@BidQty", Common.CastAsDecimal(txtDefBidQty.Text)),
                       new MyParameter("@LongDescr", LongDescr),
                       new MyParameter("@CostCategory", rdoDefYardCost.Checked ? "Y" : "N"),
                       new MyParameter("@OutsideRepair", chkDefOutsideRepair.Checked ? "Y" : "N"),
                       new MyParameter("@DefectNo", DefectNo),
                       new MyParameter("@ReqForJobTrack", chkDefReqJT.Checked ? "Y" : "N")
                       

                       );
                    DataSet ds = new DataSet();
                    ds.Clear();
                    Boolean res;
                    res = Common.Execute_Procedures_IUD(ds);
                    if (res)
                    {
                        lblDefMsg.Text = "Imported successfully.";
                    }
                    else
                    {
                        lblDefMsg.Text = "Unable to import. Error :" + Common.ErrMsg;
                    }
                }
                catch (Exception ex)
                {
                    lblDefMsg.Text = "Unable to import. Error :" + ex.Message;
                }
            }
        }
        btnImportDefect_Click(sender, e);
    }
    protected void btnPublish_Click(object sender, EventArgs e)
    {
        if (DocketId > 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ff", "window.open('PublishDocket.aspx?DocketId=" + DocketId.ToString() + "&Publish=Y','');", true);
        }

        //DataTable dtSummary = Common.Execute_Procedures_Select_ByQuery("SELECT *,(SELECT VESSELNAME FROM DBO.VESSEL V WHERE V.VESSELID=D.VESSELID) AS VESSELNAME FROM DD_DocketMaster D WHERE DOCKETID=" + DocketId.ToString());
        //DataTable dtCats = Common.Execute_Procedures_Select_ByQuery("SELECT CATID,CATCODE,CATNAME FROM DD_JobCategory ORDER BY CATCODE");
        //DataTable dtJobs = Common.Execute_Procedures_Select_ByQuery("SELECT CATID,DocketJobId,JOBCODE,JOBNAME,JobDesc FROM DD_DOCKETJOBS WHERE DOCKETID=" + DocketId + " ORDER BY JOBCODE");
        //DataTable dtSubJobs = Common.Execute_Procedures_Select_ByQuery("SELECT DocketSubJobId,[DocketJobId],[DocketId],[SubJobCode],[SubJobName],AttachmentName,[Unit],[BidQty],[LongDescr],CASE WHEN [CostCategory] = 'Y' THEN 'Yard Cost' WHEN [CostCategory] = 'N' THEN 'Non Yard Cost' ELSE '' END AS [CostCategory] , CASE WHEN [OutsideRepair] = 'Y' THEN 'Yes'  WHEN [OutsideRepair] = 'N' THEN 'No' ELSE '' END AS [OutsideRepair] FROM [dbo].[DD_DocketSubJobs] WHERE DOCKETID=" + DocketId + " ORDER BY SubJobCode");
        //string ErrorMessage="";
        //if (CreatePDF(dtSummary, dtCats, dtJobs, dtSubJobs,ref ErrorMessage))
        //{
        //    ////string FileName = Server.MapPath("~/DryDock/" + "Docket.pdf");
        //    ////byte[] FileContent = File.ReadAllBytes(FileName);

        //    ////Common.Set_Procedures("[dbo].[DD_PublishDocket]");
        //    ////Common.Set_ParameterLength(3);
        //    ////Common.Set_Parameters(
        //    ////    new MyParameter("@DocketId", DocketId),
        //    ////    new MyParameter("@PublishedBy", Session["FullName"].ToString()),
        //    ////    new MyParameter("@Attachment", FileContent)
        //    ////    );
        //    ////DataSet ds = new DataSet();
        //    ////ds.Clear();
        //    ////Boolean res;
        //    ////res = Common.Execute_Procedures_IUD(ds);
        //    ////if (res)
        //    ////{
        //    ////    BindPublishHistory();
        //    ////    lblMsgMain.Text = "Published successfully.";
        //    ////}
        //    ////else
        //    ////{
        //    ////    lblMsgMain.Text = "Unable to Publish. Error :" + Common.ErrMsg;
        //    ////}

        //    lblMsgMain.Text = "Published successfully.";
        //}
        //else
        //{
        //    lblMsgMain.Text = "Unable to Publish. Error :" + ErrorMessage;
        //}
    }
    protected void btnPrintO_Click(object sender, EventArgs e)
    {
        if (DocketId > 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ff", "window.open('PublishDocket.aspx?DocketId=" + DocketId.ToString() + "&Publish=N&Cost=O','');", true);
        }
    }
    protected void btnPrintY_Click(object sender, EventArgs e)
    {
        if (DocketId > 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ff", "window.open('PublishDocket.aspx?DocketId=" + DocketId.ToString() + "&Publish=N&Cost=Y','');", true);
        }
    }
    
    private bool CreatePDF(DataTable dtSummary, DataTable dtCats, DataTable dtJobs, DataTable dtSubJobs,ref string ErrorMessage)
    {
        bool result = false;
        try
        {

            Document document = new Document(PageSize.A4, 0f, 0f, 10f, 10f);
            System.IO.MemoryStream msReport = new System.IO.MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(document, msReport);
            document.Open();

            //------------ TABLE HEADER FONT 
            iTextSharp.text.Font fCapText_11_Reg = FontFactory.GetFont("ARIAL", 7, iTextSharp.text.Font.NORMAL);
            iTextSharp.text.Font fCapText_11 = FontFactory.GetFont("ARIAL", 7, iTextSharp.text.Font.BOLD);
            iTextSharp.text.Font fCapText_13 = FontFactory.GetFont("ARIAL", 9, iTextSharp.text.Font.BOLD);
            iTextSharp.text.Font fCapText_15 = FontFactory.GetFont("ARIAL", 12, iTextSharp.text.Font.BOLD);


            //=============================================================================
            // Page -1 
            PdfPTable tblPage1 = new PdfPTable(1);
            tblPage1.SplitLate = false;
            tblPage1.SplitRows = true;

            tblPage1.HorizontalAlignment = Element.ALIGN_CENTER;
            float[] wsCom = { 100 };
            tblPage1.SetWidths(wsCom);

            float[] wsCom_90 = { 90 };
            tblPage1.SetWidths(wsCom);

            // Heading -----------------
            PdfPCell cell = new PdfPCell(new Phrase("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\nDRYDOCK REPAIR LIST\n\n", fCapText_15));
            cell.BorderColor = Color.WHITE;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            tblPage1.AddCell(cell);
            // Vessel Name -----------------
            cell = new PdfPCell(new Phrase(dtSummary.Rows[0]["VESSELNAME"].ToString() + "\n\n", fCapText_13));
            cell.BorderColor= Color.WHITE;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            tblPage1.AddCell(cell);
            // DocketNo -----------------
            cell = new PdfPCell(new Phrase(dtSummary.Rows[0]["DocketNo"].ToString() + "\n\n", fCapText_13));
            cell.BorderColor = Color.WHITE;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            tblPage1.AddCell(cell);

            document.Add(tblPage1);
            //=============================================================================
            // Page-2 
            document.NewPage();

            PdfPTable tblPage2 = new PdfPTable(1);
            tblPage2.SplitLate = false;
            tblPage2.SplitRows = true;

            tblPage2.HorizontalAlignment = Element.ALIGN_CENTER;
            tblPage2.SetWidths(wsCom);
            // Heading -----------------
            cell = new PdfPCell(new Phrase("CONTENTS", fCapText_15));
            cell.BorderColor = Color.WHITE;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            tblPage2.AddCell(cell);

            document.Add(tblPage2);

            PdfPTable tblPage2_1 = new PdfPTable(2);
            tblPage2_1.SplitLate = false;
            tblPage2_1.SplitRows = true;

            float[] wsCom_2_1 = { 5,95 };
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
                tblPage_Cats.SplitLate = false;
                tblPage_Cats.SplitRows = true;

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
                DataTable dtJobs_Temp=dvJobs.ToTable();

                PdfPTable tblPage_Jobs = new PdfPTable(2);
                tblPage_Jobs.SplitLate = false;
                tblPage_Jobs.SplitRows = true;

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
                    dv_SubJobs.RowFilter="DocketJobId=" + drJ["DocketJobId"].ToString();
                    DataTable dt_SubJobs_Temp = dv_SubJobs.ToTable();

                    PdfPTable tblPage_SubJobs = new PdfPTable(1);
                    //tblPage_SubJobs.SplitLate = false;
                    //tblPage_SubJobs.SplitRows = true;

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

                            PdfPTable tblPage_SubJobs_OtherDetails = new PdfPTable(5);
                            tblPage_SubJobs_OtherDetails.SplitLate = false;
                            tblPage_SubJobs_OtherDetails.SplitRows = true;

                            float[] wsCom_SubJobs_other = { 20, 20 , 20 , 20 , 20 };
                            tblPage_SubJobs_OtherDetails.HorizontalAlignment = Element.ALIGN_CENTER;
                            tblPage_SubJobs_OtherDetails.SetWidths(wsCom_SubJobs_other);

                            cell = new PdfPCell(new Phrase("Job Code", fCapText_11));
                            cell.BorderColor = Color.BLACK;
                            cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            tblPage_SubJobs_OtherDetails.AddCell(cell);

                            cell = new PdfPCell(new Phrase("Bid Qty", fCapText_11));
                            cell.BorderColor = Color.BLACK;
                            cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            tblPage_SubJobs_OtherDetails.AddCell(cell);

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

                            cell = new PdfPCell(new Phrase(drS["BidQty"].ToString(), fCapText_11_Reg));
                            cell.BorderColor = Color.BLACK;
                            cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            tblPage_SubJobs_OtherDetails.AddCell(cell);

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
                            cell.Colspan = 6;
                            cell.HorizontalAlignment = Element.ALIGN_LEFT;
                            cell.AddElement(new Phrase("Short Description : ", fCapText_11));
                            cell.AddElement(new Phrase(drS["SubJobName"].ToString(), fCapText_11_Reg));
                            cell.AddElement(new Phrase("Long Description : ", fCapText_11));
                            cell.AddElement(new Phrase(drS["LongDescr"].ToString(), fCapText_11_Reg));
                            if (drS["AttachmentName"].ToString().Trim() != "")
                            {
                                cell.AddElement(new Phrase("Attachment : " + drS["AttachmentName"].ToString() + "\n\n", fCapText_11));
                            }

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
            if (File.Exists(Server.MapPath("~/DryDock/" + "Docket.pdf")))
            {
                File.Delete(Server.MapPath("~/DryDock/" + "Docket.pdf"));
            }
            FileStream fs = new FileStream(Server.MapPath("~/DryDock/" + "Docket.pdf"), FileMode.Create);
            byte[] bb = msReport.ToArray();
            fs.Write(bb, 0, bb.Length);
            fs.Flush();
            fs.Close();
            result = true;
            lblMsgMain.Text = "File Created successfully.";
        }
        catch (System.Exception ex)
        {
            ErrorMessage = ex.Message;
            lblMsgMain.Text = "Unable to create file. Error : " + ex.Message;
        }
        return result;
    }
    protected void BindPublishHistory()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT ROW_NUMBER() OVER(ORDER BY VERSIONNO) AS SNO,D.*,DOCKETNO FROM DD_Docket_Publish_History D INNER JOIN DD_DOCKETMASTER M ON M.DOCKETID=D.DOCKETID WHERE D.DOCKETID=" + DocketId);
        rprPublishHistory.DataSource = dt;
        rprPublishHistory.DataBind();
    }
    protected void btn_Download_PublishedFile_Click(object sender, EventArgs e)
    {
        int TableId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT VersionNo,Attachment FROM DD_Docket_Publish_History WHERE TABLEID=" + TableId);
        if(dt.Rows.Count>0)
        {
            string FileName = lblDocketNo.Text.Replace("/", "-") + "-" + dt.Rows[0]["VersionNo"].ToString() + ".pdf";
            byte[] buff = (byte[])dt.Rows[0]["Attachment"];
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileName);
            Response.BinaryWrite(buff);
            Response.Flush();
            Response.End(); 
        }
    }

    public void BindJobCategory()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM dbo.DD_Jobs WHERE [JobId] NOT IN (SELECT JobId FROM DD_DOCKETJOBS WHERE DocketId = " + DocketId + " )");

        rptJobcategory.DataSource = dt;
        rptJobcategory.DataBind();
    }
    protected void btn_ImportJobCategory_Click(object sender, EventArgs e)
    {
        BindJobCategory();
        dvImportJobCategory.Visible = true;
    }
    protected void btnSaveJobCat_Click(object sender, EventArgs e)
    {
        bool selected = false;
        foreach (RepeaterItem ri in rptJobcategory.Items)
        {
            CheckBox ch = (CheckBox)ri.FindControl("chkSelect");
            if (ch.Checked)
            {
                selected = true;
                break;
            }
        }

        if (!selected)
        {
            lblMsgImportJC.Text = "Please select a job category to import.";
            return;
        }

        foreach (RepeaterItem ri in rptJobcategory.Items)
        {
            CheckBox ch = (CheckBox)ri.FindControl("chkSelect");
            if (ch.Checked)
            {
                Common.Execute_Procedures_Select_ByQuery("exec dbo.DD_ImportJobCategory " + DocketId + "," + ch.CssClass);
            }
        }

        BindJobCategory();
        
        lblMsgImportJC.Text = "Selected job categories imported successfully.";
    }
    protected void btnCloseJobCat_Click(object sender, EventArgs e)
    {
        BindJobs();
        dvImportJobCategory.Visible = false;
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        int DocketJobId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);

        try
        {
            Common.Execute_Procedures_Select_ByQuery("DELETE FROM DD_DocketSubJobs WHERE DocketJobId = " + DocketJobId + " AND DocketId = " + DocketId + " ; DELETE FROM DD_DOCKETJOBS WHERE DocketJobId = " + DocketJobId + " AND DocketId = " + DocketId);
            BindJobs();
            BindSubJobs();
            lblMsgMain.Text = "Job Category Deleted Successfully.";
        }
        catch (Exception ex)
        {
            lblMsgMain.Text = "Unable to delete Job Category. Error : " + ex.Message;
        }

    }
    protected void btnDownloadSOR_Click(object sender, EventArgs e)
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT [SubJobCode],[SubJobName],AttachmentName,Attachment, RIGHT(AttachmentName, 5) As FileExt  FROM [dbo].[DD_DocketSubJobs] WHERE DOCKETID=" + DocketId + " AND AttachmentName IS NOT Null and ATTACHMENT IS NOT NULL ORDER BY SubJobCode ");

        if (dt.Rows.Count > 0)
        {
            if (Directory.Exists(Server.MapPath("DownLoadSOR")))
            {
                Array.ForEach(Directory.GetFiles(Server.MapPath("DownLoadSOR")), File.Delete);
            }
            else
            {
                Directory.CreateDirectory(Server.MapPath("DownLoadSOR"));
            }

            foreach (DataRow dr in dt.Rows)
            {
                string Extname = dr["FileExt"].ToString();
                if (!Extname.StartsWith("."))
                    Extname = Extname.Substring(Extname.IndexOf("."));

                string FileName = dr["SubJobCode"].ToString().Trim() + Extname;
                byte[] buff = (byte[])dr["Attachment"];
                System.IO.File.WriteAllBytes(Server.MapPath("DownLoadSOR/" + FileName), buff);
            }

            string FileToZip = Server.MapPath("DownLoadSOR");
            string ZipFile = Server.MapPath("~/TEMP/SOR.zip");

            if (File.Exists(ZipFile))
                File.Delete(ZipFile);

            using (ZipFile zip = new ZipFile())
            {
                zip.AddDirectory(FileToZip);
                zip.Save(ZipFile);
            }

            byte[] buff1 = System.IO.File.ReadAllBytes(ZipFile);
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(ZipFile));
            Response.BinaryWrite(buff1);
            Response.Flush();
            Response.End();
        }
        else
        {
            lblMsgMain.Text = "No SOR to download in this docket.";
        }
    }

    protected void btnNotifyGM_Click(object sender, EventArgs e)
    {
        //-------------------
        int LoginId=Common.CastAsInt32(Session["loginid"].ToString());
        Common.Execute_Procedures_Select_ByQuery("UPDATE DD_DocketMaster SET PublishNotify=1,PublishNotifyBy='" + Session["FullName"].ToString() + "',PublishNotifyOn=GETDATE(),PublishNotifyComments='' WHERE DOCKETID=" + DocketId.ToString());
        string[] ToAdd={"emanager@energiossolutions.com","emanager@energiossolutions.com"};
        //string[] ToAdd = { "pankaj.k@esoftech.com", "emanager@energiossolutions.com" };
        DataTable dtEmail = Common.Execute_Procedures_Select_ByQuery("select Email from dbo.userlogin where loginid=" + LoginId);
        string selfemail = "";
        if (dtEmail.Rows.Count > 0)
            if (dtEmail.Rows[0][0].ToString().Trim() != "")
                selfemail = dtEmail.Rows[0][0].ToString();

        string[] CCAdd = { selfemail };
        string[] NoAdd = { };
        string error;
        EProjectCommon.SendeMail_MTM(LoginId, "emanager@energiossolutions.com", "emanager@energiossolutions.com", ToAdd, CCAdd, NoAdd, lblVessel.Text + " : DD Specs Approval Request", "Dear Sir,<br/><br/>Please review DD Specs for " + lblVessel.Text + ". If ok, please approve docket no. " + lblDocketNo.Text + " from the drydock module in PMS.<br/><br/>Thanks<br/>------", out error, "");
        //------------------------ SEND MAIL TO GM FOR DOCKET NOTIFY
        //------------------------ SEND MAIL TO GM FOR DOCKET NOTIFY

        lblMsgMain.Text = "Technical Manager notified successfully";
    }
    protected void btnGMApproval_Click(object sender, EventArgs e)
    {
        //-------------------
        Common.Execute_Procedures_Select_ByQuery("UPDATE DD_DocketMaster SET ApprovalBy='" + Session["FullName"].ToString() + "',ApprovalOn=GETDATE(),Comments='' WHERE DOCKETID=" + DocketId.ToString());
        //------------------------ SEND MAIL TO GM FOR DOCKET NOTIFY
        lblMsgMain.Text = "Approval completed successfully";
    }
 }

