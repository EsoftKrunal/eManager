using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Ionic.Zip;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Configuration;
using System.Net.Mail;

public partial class CircularNew : System.Web.UI.Page
{
    public int UserId
    {
        get { return Common.CastAsInt32(ViewState["UserId"]); }
        set { ViewState["UserId"] = value; }
    }
    public int CId
    {
        get { return Common.CastAsInt32(ViewState["CId"]); }
        set { ViewState["CId"] = value; }
    }

    public int Mail_CIRId
    {
        get { return Common.CastAsInt32(ViewState["Mail_CIRId"]); }
        set { ViewState["Mail_CIRId"] = value; }
    }
    
    public string UserName
    {
        get { return ViewState["UserName"].ToString(); }
        set { ViewState["UserName"] = value; }
    }
    public string FileName
    {
        set { ViewState["FileName"] = value; }
        get { return ViewState["FileName"].ToString(); }
    }
    public string vCircularNumber
    {
        set { ViewState["CircularNumber"] = value; }
        get { return ViewState["CircularNumber"].ToString(); }
    }
    public string vApprejBy
    {
        set { ViewState["ApprejBy"] = value; }
        get { return ViewState["ApprejBy"].ToString(); }
    }
    public string vApprejOn
    {
        set { ViewState["ApprejOn"] = value; }
        get { return ViewState["ApprejOn"].ToString(); }
    } 
    public string vCreatedBy
    {
        set { ViewState["vCreatedBy"] = value; }
        get { return ViewState["vCreatedBy"].ToString(); }
    }
    public string vCreatedOn
    {
        set { ViewState["vCreatedOn"] = value; }
        get { return ViewState["vCreatedOn"].ToString(); }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        lblMsgMail.Text = "";
        lblAction_Msg.Text = "";
        if (!IsPostBack)
        {
            UserId = Common.CastAsInt32(Session["loginid"]);
            UserName = Session["UserName"].ToString();
            txtFDate.Text = "01-Jan-" + DateTime.Today.Year;
            txtTDate.Text = "31-Dec-" + DateTime.Today.Year;
            BindCategory();
            Bindgrid();
        }
    }

    //protected void btnDeleteCir_Click(object sender, EventArgs e)
    //{
    //    int CIRId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
    //    Common.Execute_Procedures_Select_ByQuery("DELETE FROM DBO.Cir_Circular WHERE CirId=" + CIRId.ToString());
    //    Common.Execute_Procedures_Select_ByQuery("DELETE FROM DBO.Cir_Categories WHERE CIRId=" + CIRId.ToString());
    //    Bindgrid();
    //}
    protected void btnPublishCir_Click(object sender, EventArgs e)
    {
        string CirNo=CreatePDF();
        int NoOfFile = 0;
        if (FileName.Trim() != "")
            NoOfFile = 2;
        else
            NoOfFile = 1;

        string[] SourceFile = new string[NoOfFile];
        if (FileName.Trim() != "")
        {
            SourceFile[0] = Server.MapPath("Publish/CircularDataFile.pdf");
            SourceFile[1] = Server.MapPath("Publish/" + FileName.Trim());
        }
        else
        {
            SourceFile[0] = Server.MapPath("Publish/CircularDataFile.pdf");
        }
        string Destination = Server.MapPath("Publish/" + "CircularFile_" + CId + ".pdf");

        ExportToPDF(SourceFile, Destination, "", " ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- Created By/On    : " + vCreatedBy + " / " + vCreatedOn + "                                                                                                                                                                       Page ");

        try
        {

            byte[] buff = (byte[])File.ReadAllBytes(Destination);

            Common.Set_Procedures("DBO.CIR_PUBLISHCIRCULAR");
            Common.Set_ParameterLength(3);
            Common.Set_Parameters(new MyParameter("@CId", CId),
                                  new MyParameter("@CircularNumber", CirNo),
                                  new MyParameter("@Attachment", buff)
                                  );
            DataSet ds = new DataSet();
            if (Common.Execute_Procedures_IUD(ds))
            {
                Bindgrid();
                btnSave.Visible = btnPublish.Visible = false;
                ProjectCommon.ShowMessage("Published successfully.");
            }
            else
            {
                ProjectCommon.ShowMessage("Unable to publish. Error : " + Common.ErrMsg);
            }
        }
        catch (Exception ex)
        {
            ProjectCommon.ShowMessage("Unable to publish. Error : " + ex.Message);
        }

    }
    protected void btnDownloadFile_Click(object sender, EventArgs e)
    {
        int CIRId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.Cir_Circular WHERE CId=" + CIRId.ToString());
        if (dt.Rows.Count > 0)
        {
            string FileName = dt.Rows[0]["AttachmentFileName"].ToString();
            //string Path = Server.MapPath("~/UserUploadedDocuments/Circular/" + FileName);
            if (FileName.Trim() != "")
            {
                byte[] buff = (byte[])dt.Rows[0]["Attachment"];
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileName);
                Response.BinaryWrite(buff);
                Response.Flush();
                Response.End();
            }
        }
    }
    protected void Filter_Cir(object sender, EventArgs e)
    {
        Bindgrid();
    }
    protected void BindCategory()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.Cir_Category ORDER BY CirCatName");
        ddlCirCat.DataSource = dt;
        ddlCirCat.DataTextField = "CirCatName";
        ddlCirCat.DataValueField = "CirCatId";
        ddlCirCat.DataBind();
        ddlCirCat.Items.Insert(0, new System.Web.UI.WebControls.ListItem("< ALL >", "0"));

        ddlCategory.DataSource = dt;
        ddlCategory.DataTextField = "CirCatName";
        ddlCategory.DataValueField = "CirCatId";
        ddlCategory.DataBind();
        ddlCategory.Items.Insert(0, new System.Web.UI.WebControls.ListItem("< Select >", "0"));
    }
    protected void BindVessel()
    {
        string SQLUser = "SELECT V.VESSELCODE,   V.VESSELNAME,EMAIL,SENTON,ACKON,v.VesselEmailNew  FROM DBO.VESSEL V LEFT JOIN DBO.Cir_Vessel_Notifications E ON E.CId=" + Mail_CIRId + " AND E.VESSELCODE=V.VESSELCODE WHERE VESSELSTATUSID=1 and Isnull(FleetId,0) <> 0  ";
        //+
        //                 "UNION " +
        //                 "SELECT '' AS VESSELCODE, '' AS VESSELNAME,Email,null As SENTON, null AS ACKON FROM dbo.EmTM_department WHERE Email is not null  ORDER BY VESSELNAME "
        DataTable dtUsers = Common.Execute_Procedures_Select_ByQuery(SQLUser);
        rptUsers.DataSource = dtUsers;
        rptUsers.DataBind();
    }
    protected void btnSendMail_Click(object sender, EventArgs e)
    {
          Mail_CIRId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
          BindVessel();
          dvEmailNotification.Visible = true;
    }
    protected void btnSend_Mail_Click(object sender, EventArgs e)
    {

        if (fckContentText.Text.Trim() == "")
        {
            ProjectCommon.ShowMessage("Please enter mail text.");
            return;
        }

        bool selecled = false;
        bool sent = false;

        foreach (RepeaterItem rpt in rptUsers.Items)
        {
            System.Web.UI.HtmlControls.HtmlInputCheckBox chkSendMail = (System.Web.UI.HtmlControls.HtmlInputCheckBox)rpt.FindControl("chkSendMail");
            //CheckBox chkSendMail = (CheckBox)rpt.FindControl("chkSendMail");
            if (chkSendMail.Checked)
            {
                selecled = true;
                break;
            }
        }

        if (!selecled)
        {
            ProjectCommon.ShowMessage("Please select vessel.");
            return;
        }

        string SQL = "SELECT * FROM DBO.Cir_Circular WHERE CId = " + Mail_CIRId;
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
        dt.TableName = "Cir_Circular";
        string CIRNumber = dt.Rows[0]["CircularNumber"].ToString();
        string Topic = dt.Rows[0]["Topic"].ToString();
        string AppName = ConfigurationManager.AppSettings["AppName"].ToString();
        string SchemaFile = Server.MapPath("/"+ AppName + "/EMANAGERBLOB/LPSQE/Circular/CircularSchema.xml");
        string DataFile = Server.MapPath("/" + AppName + "/EMANAGERBLOB/LPSQE/Circular/CircularData.xml");
        string ZipFile = Server.MapPath("/" + AppName + "/EMANAGERBLOB/LPSQE/Circular/" + CIRNumber.Replace("/", "-") + ".zip");
        List<string> BCCMails = new List<string>();

        dt.DataSet.WriteXmlSchema(SchemaFile);
        dt.DataSet.WriteXml(DataFile);
        using (ZipFile zip = new ZipFile())
        {
            zip.AddFile(SchemaFile);
            zip.AddFile(DataFile);
            zip.Save(ZipFile);
        }
        StringBuilder sb = new StringBuilder();
        sb.Append("<br/><br/>");
        sb.Append("***********************************************************************");
        sb.Append("<br/>");
        sb.Append("<br/>");
        sb.Append("<b>Circular# : </b>" + CIRNumber);
        sb.Append("<br/>");
        sb.Append("<b>Topic : </b>" + Topic);
        sb.Append("<br/>");
        sb.Append("<br/>");
        sb.Append("Please find the attached data packet to import Circular.");
        sb.Append("<br/>");
        sb.Append("<br/>");
        sb.Append("***********************************************************************");
        sb.Append("<br/>");
        sb.Append("<br/>");
        sb.Append(fckContentText.Text);
        int i =0 , j = 0;
        foreach (RepeaterItem rpt in rptUsers.Items)
        {
            System.Web.UI.HtmlControls.HtmlInputCheckBox chkSendMail = (System.Web.UI.HtmlControls.HtmlInputCheckBox)rpt.FindControl("chkSendMail");
            //CheckBox chkSendMail = (CheckBox)rpt.FindControl("chkSendMail");
            HiddenField hfdVesselCode = (HiddenField)rpt.FindControl("hfdVesselCode");
            string EmailAddress = chkSendMail.Attributes["title"];
            string VesselCode = hfdVesselCode.Value;
          
            if (chkSendMail.Checked)
            {
                i = i + 1;
                if (VesselCode.Trim() != "")
                {
                    Common.Set_Procedures("DBO.CIR_InsertCircular_Vessel_Notifications");
                    Common.Set_ParameterLength(2);
                    Common.Set_Parameters(new MyParameter("@CId", Mail_CIRId), new MyParameter("@VesselCode", VesselCode));
                    DataSet ds = new DataSet();
                    if (Common.Execute_Procedures_IUD(ds))
                    {

                    }
                }
                string _FromAdd = ConfigurationManager.AppSettings["FromAddress"];
                string UserEmail = ProjectCommon.getUserEmailByID(UserId.ToString());
                if (! string.IsNullOrWhiteSpace(UserEmail))
                {
                    BCCMails.Add(UserEmail);
                }
                string result = SendMail.SendCirMail(_FromAdd, EmailAddress, BCCMails.ToArray(), CIRNumber, sb.ToString(), ZipFile);
                if (result == "SENT")
                {
                    j = j + 1;
                }
                else
                {
                    lblMsgMail.Text = "Unable to sent mail to all selected vessels.";
                }
            }
        }

        if (i == j)
        {
            lblMsgMail.Text = "Mail sent successfully.";
        }
        else
        {
            lblMsgMail.Text = "Unable to sent mail to all selected vessels.";
        }
    }
    protected void Bindgrid()
    {
        string CIRCatSQL = " WHERE 1=1 ";

        if (ddlCirCat.SelectedIndex > 0)
        {
            CIRCatSQL += " AND C.[Category] =" + ddlCirCat.SelectedValue + "";
        }

        string SQL = "SELECT [CId], [CircularNumber], [CircularDate], [CirCatName], [CType],[Source],[Topic], [Details], [NextReviewDate], [AttachmentFileName], (SELECT CONVERT(VARCHAR,COUNT(*)) FROM DBO.Cir_Vessel_Notifications N WHERE N.CId=C.CId) AS TOTALSENT,(SELECT CONVERT(VARCHAR,COUNT(*)) FROM DBO.Cir_Vessel_Notifications N WHERE N.CId=C.CId AND ACKON IS NOT NULL) AS TOTALACK,  " +
                     "[CreatedBy],[CreatedOn], CASE WHEN [Status] = 1 THEN 'Active'  WHEN [Status] = 2 THEN 'InActive' WHEN [Status] = 3 THEN 'In SMS' ELSE '' END AS [StatusText], [Status], (SELECT ReferenceKey FROM dbo.Moc_Request WHERE ReferenceKey = CId AND Topic='CIRCULAR' ) AS ReferenceKey " +
                     "FROM dbo.Cir_Circular C " +
                     "INNER JOIN dbo.Cir_Category CC ON CC.[CirCatId] = C.[Category] ";

        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL + CIRCatSQL + " ORDER BY CircularDate Desc ");
       
        if (dt != null)
        {
            DataView dv = dt.DefaultView;
            string Filter = " 1=1 ";

            if (txtFDate.Text.Trim() != "")
            {
                Filter += " AND CircularDate >='" + txtFDate.Text + "'";
            }
            if (txtTDate.Text.Trim() != "")
            {
                Filter += " AND CircularDate <='" + txtTDate.Text + "'";
            }
            if (txtSearchText.Text.Trim() != "")
            {
                Filter += " AND Details like '%" + txtSearchText.Text.Trim() + "%'";
            }
            if (ddlStatus.SelectedIndex != 0)
            { 
                Filter += " AND Status=" + ddlStatus.SelectedValue;
            }
            if (ddlType_Search.SelectedIndex != 0)
            {
                Filter += " AND CType='" + ddlType_Search.SelectedValue + "'";
            }


            dv.RowFilter = Filter;
            rptCIR.DataSource = dv.ToTable();
            rptCIR.DataBind();      
        }
        else
        {
            rptCIR.DataSource = null;
            rptCIR.DataBind();
        }
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        Bindgrid();
        dvEmailNotification.Visible = false;
    }
    protected void btnAddCir_Click(object sender, EventArgs e)
    {
        CId = 0;
        txtCirDate.Text = "";
        ddlCategory.SelectedIndex = 0;
        txtSource.Text = "";
        ddlType.SelectedIndex = 0;
        txtCularTopic.Text = "";
        txtCircularDetails.Text = "";
        txtNextReviewDate.Text = "";
        btnClipText.Text = "";

        btnSave.Visible = true;
        btnPublish.Visible = false;
        dv_AddCir.Visible = true;
        
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
               
        string FileName = "";
        byte[] FileContent = new byte[0];
               
        //-------------------------------------------
       
        if (flpUpload.HasFile)
        {
            if (!flpUpload.FileName.EndsWith(".pdf"))
            {
                ProjectCommon.ShowMessage("Please check.. ! Only pdf files are allowed.");
                return;
            }
            else
            {
                string TempNameOnly =System.IO.Path.GetFileNameWithoutExtension(flpUpload.FileName);
                string TempExtOnly = System.IO.Path.GetExtension(flpUpload.FileName);
                if (btnClipText.Text.Trim() != "")
                {
                    FileName = btnClipText.Text.Trim();
                }
                else
                {
                    FileName = TempNameOnly + DateTime.Now.ToString("dd-mm-yyy-hh-mm-tt") + TempExtOnly;
                }

                FileContent = flpUpload.FileBytes;
            }
        }
       
        //-------------------------------------------
        Common.Set_Procedures("DBO.CIR_INSERTUPDATECIRCULAR");
        Common.Set_ParameterLength(11);
        Common.Set_Parameters(new MyParameter("@CId", CId),
                              new MyParameter("@CircularDate", txtCirDate.Text.Trim()),
                              new MyParameter("@Category", ddlCategory.SelectedValue.Trim()),
                              new MyParameter("@CType", ddlType.SelectedValue.Trim()),
                              new MyParameter("@Source", txtSource.Text.Trim()),
                              new MyParameter("@Topic", txtCularTopic.Text.Trim()),
                              new MyParameter("@Details", txtCircularDetails.Text.Trim()),
                              new MyParameter("@NextReviewDate", txtNextReviewDate.Text.Trim()),
                              new MyParameter("@AttachmentFileName", FileName),
                              new MyParameter("@Attachment", FileContent),
                              new MyParameter("@CreatedBy", UserName)
                              );
            DataSet ds=new DataSet();
            if (Common.Execute_Procedures_IUD(ds))
            {
                
                //if (flpUpload.HasFile)
                //{
                //    flpUpload.SaveAs(Server.MapPath("~/UserUploadedDocuments/Circular/" + FileName));
                //}
                dv_AddCir.Visible = false;
                Bindgrid();
            }
            else
            {
                ProjectCommon.ShowMessage(Common.ErrMsg);
            }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        CId = 0;
        btnClip.Visible = btnClipText.Visible = false;
        btnClipText.Text = "";
        txtCirDate.Enabled = true;
        ddlCategory.Enabled = true;
        ddlType.Enabled = true;

        dv_AddCir.Visible = false;
    }
    protected void btnEditCir_Click(object sender, EventArgs e)
    {
        CId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT [CId], [CircularNumber], [CircularDate], Category, [CType],[Source],[Topic], Details , [NextReviewDate], [AttachmentFileName],Status FROM DBO.Cir_Circular WHERE CId=" + CId.ToString());
        if (dt.Rows.Count > 0)
        {
            txtCirDate.Text = Common.ToDateString(dt.Rows[0]["CircularDate"]);
            ddlCategory.SelectedValue = dt.Rows[0]["Category"].ToString();
            txtSource.Text = dt.Rows[0]["Source"].ToString();
            ddlType.SelectedValue = dt.Rows[0]["CType"].ToString();
            txtCularTopic.Text = dt.Rows[0]["Topic"].ToString();
            txtCircularDetails.Text = dt.Rows[0]["Details"].ToString();
            txtNextReviewDate.Text = Common.ToDateString(dt.Rows[0]["NextReviewDate"]);
            string _CircularNumber = dt.Rows[0]["CircularNumber"].ToString();

            if (dt.Rows[0]["AttachmentFileName"].ToString().Trim() != "")
            {
                //btnClip.CommandArgument = btnClipText.CommandArgument = CIRId.ToString();
                btnClip.Visible = true;
                btnClipText.Visible = true;
                btnClipText.Text = dt.Rows[0]["AttachmentFileName"].ToString();
                FileName = btnClipText.Text;
            }
            dv_AddCir.Visible = true;
            btnSave.Visible = _CircularNumber.Trim()=="";
            btnPublish.Visible = _CircularNumber.Trim() == "";

           
            txtCirDate.Enabled = false;
            ddlCategory.Enabled = false;
            ddlType.Enabled = false;
            
        }
    }
    //public void SelectLFICategory()
    //{
    //    DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT CirCatId FROM DBO.[Cir_Categories] WHERE CIRId=" + CIRId.ToString());
    //    if (dt.Rows.Count > 0)
    //    {
    //        foreach( DataRow dr in dt.Rows )
    //        {
    //            foreach (ListItem lst in chkCirCats.Items)
    //            {
    //                if (lst.Value.Trim() == dr["CirCatId"].ToString().Trim())
    //                    lst.Selected = true;
    //            }
    //        }
    //    }

    //}
    protected void btnClip_Click(object sender, ImageClickEventArgs e)
    {
        //int CIRId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        DownloadFile();
    }
    protected void btnClipText_Click(object sender, EventArgs e)
    {
        //int CIRId = Common.CastAsInt32(((LinkButton)sender).CommandArgument);
        DownloadFile();
    }
    protected void DownloadFile()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.Cir_Circular WHERE CId=" + CId);
        if (dt.Rows.Count > 0)
        {
            string FileName = dt.Rows[0]["AttachmentFileName"].ToString();
            //string Path = Server.MapPath("~/UserUploadedDocuments/Circular/" + FileName);
            if (FileName.Trim() != "")
            {
                byte[] buff = (byte[])dt.Rows[0]["Attachment"];
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileName);
                Response.BinaryWrite(buff);
                Response.Flush();
                Response.End();
            }
        }
    }

    protected void btnAction_OnClick(object sender, EventArgs e)
    {
        CId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);

        dv_Action.Visible = true;
    }
    protected void ddlAction_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlAction.SelectedValue.Trim() == "2")
        {
            pnlUpdatereviewDt.Visible = true;
            pnlCreateMOC.Visible = false;
        }
        else if (ddlAction.SelectedValue.Trim() == "3")
        {
            pnlUpdatereviewDt.Visible = false;
            pnlCreateMOC.Visible = true;
        }
        else
        {
            pnlUpdatereviewDt.Visible = false;
            pnlCreateMOC.Visible = false;
        }
    }
    protected void btnCloseAction_Click(object sender, EventArgs e)
    {
        CId = 0;

        pnlUpdatereviewDt.Visible = false;
        pnlCreateMOC.Visible = false;

        ddlAction.SelectedIndex = 0;
        txtNextReviewDate.Text = "";

        ddlSource.SelectedIndex = 0;
        ddlVessel_Office.Items.Clear();
        ddlVessel_Office.Items.Insert(0, new System.Web.UI.WebControls.ListItem(" < Select >", "0"));
        txtMOCDate.Text = "";
        cbImpact.SelectedIndex = -1;
        txtReasonforChange.Text = "";
        txtDescr.Text = "";
        txtPropTL.Text = "";

        dv_Action.Visible = false;
    }

    protected void ddlSource_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSource.SelectedValue == "Vessel")
        {
            Load_vessel();
        }
        else if (ddlSource.SelectedValue == "Office")
        {
            Load_office();
        }
        else
        {
            ddlVessel_Office.Items.Clear();
            ddlVessel_Office.Items.Insert(0, new System.Web.UI.WebControls.ListItem(" < Select >", "0"));
        }
    }

    private void Load_vessel()
    {
        DataSet dt = Budget.getTable("Select * from dbo.vessel where vesselstatusid<>2  order by vesselname");
        ddlVessel_Office.DataSource = dt;
        ddlVessel_Office.DataTextField = "VesselName";
        ddlVessel_Office.DataValueField = "VesselCode";
        ddlVessel_Office.DataBind();

        ddlVessel_Office.Items.Insert(0, new System.Web.UI.WebControls.ListItem(" < Select >", "0"));
    }
    private void Load_office()
    {
        DataSet dt = Budget.getTable("SELECT OfficeId, OfficeName, OfficeCode FROM [dbo].[Office] Order By OfficeName");
        ddlVessel_Office.DataSource = dt;
        ddlVessel_Office.DataTextField = "OfficeName";
        ddlVessel_Office.DataValueField = "OfficeId";
        ddlVessel_Office.DataBind();

        ddlVessel_Office.Items.Insert(0, new System.Web.UI.WebControls.ListItem(" < Select >", "0"));
    }

    protected void btnSaveAction_OnClick(object sender, EventArgs e)
    {
        if (ddlAction.SelectedIndex == 0)
        {
            lblAction_Msg.Text = "Please select Action.";
            ddlAction.Focus();
            return;
        }

        if (ddlAction.SelectedValue.Trim() == "1")
        {
            string sql = "UPDATE [dbo].[Cir_Circular] SET Status = 2 WHERE CID =" + CId;
            DataSet DS = Budget.getTable(sql);

            Bindgrid(); ;
            lblAction_Msg.Text = "Updated successfully.";
        }

        if (ddlAction.SelectedValue.Trim() == "2")
        {
            DateTime dt;
            if (txtUpdateNRD.Text.Trim() == "")
            {
                lblAction_Msg.Text = "Please enter next review date.";
                txtUpdateNRD.Focus();
                return;
            }

            if (!DateTime.TryParse(txtUpdateNRD.Text.Trim(), out dt))
            {
                lblAction_Msg.Text = "Please enter valid date.";
                txtNextReviewDate.Focus();
                return;
            }
            try
            {
                string sql = "UPDATE [dbo].[Cir_Circular] SET NextReviewDate = '" + txtUpdateNRD.Text.Trim() + "' WHERE CID =" + CId;
                DataSet DS = Budget.getTable(sql);

                Bindgrid(); ;
                lblAction_Msg.Text = "Next review date updated successfully.";

            }
            catch (Exception ex)
            {

                lblAction_Msg.Text = "Unable to update next review date.Error :" + ex.Message.ToString();
            }


        }

        if (ddlAction.SelectedValue.Trim() == "3")
        {
            string Impact = "";

            if (ddlSource.SelectedIndex == 0)
            {
                lblAction_Msg.Text = "Please select source.";
                ddlSource.Focus();
                return;
            }

            if (ddlSource.SelectedValue.Trim() == "Vessel" && ddlVessel_Office.SelectedIndex == 0)
            {
                lblAction_Msg.Text = "Please select vessel.";
                ddlVessel_Office.Focus();
                return;
            }
            else if (ddlSource.SelectedValue.Trim() == "Office" && ddlVessel_Office.SelectedIndex == 0)
            {
                lblAction_Msg.Text = "Please select office.";
                ddlVessel_Office.Focus();
                return;
            }

            if (txtMOCDate.Text.Trim() == "")
            {
                lblAction_Msg.Text = "Please enter MOC date.";
                txtMOCDate.Focus();
                return;
            }

            foreach (System.Web.UI.WebControls.ListItem lst in cbImpact.Items)
            {
                if (lst.Selected)
                {
                    Impact += lst.Value + ",";
                }
            }

            if (Impact.Trim().Length > 0)
            {
                Impact = Impact.Remove(Impact.LastIndexOf(','));
            }

            if (Impact.Trim() == "")
            {
                lblAction_Msg.Text = "Please select Impact.";
                cbImpact.Focus();
                return;
            }

            if (txtPropTL.Text.Trim() == "")
            {
                lblAction_Msg.Text = "Please enter proposed time line.";
                txtPropTL.Focus();
                return;
            }

            try
            {
                Common.Set_Procedures("dbo.MOC_CreateMocRequest");
                Common.Set_ParameterLength(12);
                Common.Set_Parameters(
                    new MyParameter("@Topic", "CIRCULAR"),
                    new MyParameter("@Source", ddlSource.SelectedValue.Trim()),
                    new MyParameter("@MOCDate", txtMOCDate.Text.Trim()),
                    new MyParameter("@VesselCode", ((ddlSource.SelectedValue.Trim() == "Vessel") ? ddlVessel_Office.SelectedValue.Trim() : "")),
                    new MyParameter("@OfficeId", ((ddlSource.SelectedValue.Trim() == "Office") ? ddlVessel_Office.SelectedValue.Trim() : "")),
                    new MyParameter("@RequestBy", Common.CastAsInt32(Session["loginid"])),
                    new MyParameter("@RequestOn", DateTime.Today.Date.ToString("dd-MMM-yyyy")),
                    new MyParameter("@Impact", Impact),
                    new MyParameter("@ReasonForChange", txtReasonforChange.Text.Trim()),
                    new MyParameter("@ProposedTimeline", txtPropTL.Text.Trim()),
                    new MyParameter("@DescrOfChange", txtDescr.Text.Trim()),
                    new MyParameter("@ReferenceKey", CId)
                    );
                DataSet ds = new DataSet();
                ds.Clear();
                Boolean res;
                res = Common.Execute_Procedures_IUD(ds);

                if (res)
                {
                    Common.Execute_Procedures_Select_ByQuery("UPDATE DBO.Cir_Circular SET STATUS=3 WHERE CId=" + CId);
                    lblAction_Msg.Text = "Included in SMS successfully.";
                }
                else
                {
                    lblAction_Msg.Text = "Unable to include in SMS.Error :" + Common.getLastError();
                }

            }
            catch (Exception ex)
            {

                lblAction_Msg.Text = "Unable to include in SMS.Error :" + ex.Message.ToString();
            }
        }
    }

    private string CreatePDF()
    {
        string cirno = "";
        try
        {
            string sql = " SELECT [CId], [Topic],[Details],'' AS SCircular,dbo.Get_CircularNo(cid) as [CircularNumber],[Source],[CirCatName] AS CircularCatName,[CircularDate], '' AS [ApprejBy], null AS [ApprejOn],[CreatedBy] AS CreatedByName,[CreatedOn] AS CreatedOnText,[CType],  [AttachmentFileName], [Attachment], " +
                        "CASE WHEN [Status] = 1 THEN 'Active'  WHEN [Status] = 2 THEN 'InActive' WHEN [Status] = 3 THEN 'In SMS' ELSE '' END AS [StatusText] " + 
                        "FROM dbo.Cir_Circular C " +
                        "INNER JOIN dbo.Cir_Category CC ON CC.[CirCatId] = C.[Category]  " +
                        "WHERE C.CId =" + CId;

            DataSet ds = Budget.getTable(sql);

            string Topic = ds.Tables[0].Rows[0]["Topic"].ToString();
            string CircularCatName = ds.Tables[0].Rows[0]["CircularCatName"].ToString();
            string Details = ds.Tables[0].Rows[0]["Details"].ToString();
            string CircularDate = ds.Tables[0].Rows[0]["CircularDate"].ToString();
            string CircularNumber = ds.Tables[0].Rows[0]["CircularNumber"].ToString();
            cirno = CircularNumber;
            vCircularNumber = ds.Tables[0].Rows[0]["CircularNumber"].ToString();

            string Source = ds.Tables[0].Rows[0]["Source"].ToString();

            vCreatedBy = ds.Tables[0].Rows[0]["CreatedByName"].ToString();
            vCreatedOn = Common.ToDateString(ds.Tables[0].Rows[0]["CreatedOnText"]);

            FileName = ds.Tables[0].Rows[0]["AttachmentFileName"].ToString();

            if (!Directory.Exists(Server.MapPath("Publish")))
            {
                Directory.CreateDirectory(Server.MapPath("Publish"));
            }
    
            if (FileName.Trim() != "")
            {
                if (File.Exists(Server.MapPath("Publish/" + FileName.Trim())))
                {
                    File.Delete(Server.MapPath("Publish/" + FileName.Trim()));
                }

                byte[] buff = (byte[])ds.Tables[0].Rows[0]["Attachment"];
                System.IO.File.WriteAllBytes(Server.MapPath("Publish/" + FileName), buff);                
            }

            Document document = new Document(PageSize.A4, 10, 10, 30, 40);
            System.IO.MemoryStream msReport = new System.IO.MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(document, msReport);
       
            Phrase p1 = new Phrase();
       
            HeaderFooter header = new HeaderFooter(p1, false);
            document.Header = header;
            header.Alignment = Element.ALIGN_LEFT;
            header.Border = iTextSharp.text.Rectangle.NO_BORDER;
            //'Adding Footer in document
            string foot_Txt = "";
            foot_Txt = foot_Txt + "";
            foot_Txt = foot_Txt + "";
            HeaderFooter footer = new HeaderFooter(new Phrase(foot_Txt, FontFactory.GetFont("VERDANA", 8, iTextSharp.text.Color.DARK_GRAY)), false);
            footer.Border = iTextSharp.text.Rectangle.NO_BORDER;
            footer.Alignment = Element.ALIGN_LEFT;
            document.Footer = footer;
            //'-----------------------------------

            document.Open();
            //------------ TABLE HEADER FONT 
            iTextSharp.text.Font fCapText = FontFactory.GetFont("ARIAL", 11, iTextSharp.text.Font.BOLD);
            iTextSharp.text.Font fCapTextTitle = FontFactory.GetFont("ARIAL", 11, iTextSharp.text.Font.BOLD);
            iTextSharp.text.Font fCapTextDetails = FontFactory.GetFont("ARIAL", 10, iTextSharp.text.Font.NORMAL);
            iTextSharp.text.Font fCapTextCirNum = FontFactory.GetFont("ARIAL", 10, iTextSharp.text.Font.ITALIC);
            iTextSharp.text.Font fCapTextHeading = FontFactory.GetFont("ARIAL", 15, iTextSharp.text.Font.BOLD);
            iTextSharp.text.Font fCapTextCompheading = FontFactory.GetFont("ARIAL", 16, iTextSharp.text.Font.BOLD);
           

            //------------Company Table
            iTextSharp.text.Table tbCom = new iTextSharp.text.Table(1);
            tbCom.DefaultCellBackgroundColor = iTextSharp.text.Color.WHITE;
            tbCom.Width = 90;
            float[] wsCom = { 100 };
            tbCom.Widths = wsCom;
            tbCom.Alignment = Element.ALIGN_CENTER;
            tbCom.Border = iTextSharp.text.Rectangle.NO_BORDER;
            tbCom.DefaultCellBorder = iTextSharp.text.Rectangle.NO_BORDER;
            tbCom.DefaultHorizontalAlignment = Element.ALIGN_CENTER;
            tbCom.BorderColor = iTextSharp.text.Color.WHITE;
            tbCom.Cellspacing = 1;
            tbCom.Cellpadding = 1;
            tbCom.AddCell(new Phrase(" ", fCapTextCompheading));
            document.Add(tbCom);
            document.Add(new Phrase("\n"));

            //------------First TABLE 
            iTextSharp.text.Table tb1 = new iTextSharp.text.Table(2);
            tb1.DefaultCellBackgroundColor = iTextSharp.text.Color.WHITE;
            tb1.Width = 90;
            float[] ws1 = { 50, 50 };
            tb1.Widths = ws1;
            tb1.Alignment = Element.ALIGN_CENTER;
            tb1.Border = iTextSharp.text.Rectangle.NO_BORDER;
            tb1.DefaultHorizontalAlignment = Element.ALIGN_LEFT;
            tb1.BorderColor = iTextSharp.text.Color.WHITE;
            tb1.Cellspacing = 1;
            tb1.Cellpadding = 1;
            tb1.AddCell(new Phrase("Category : " + CircularCatName + "", fCapText));
            tb1.AddCell(new Phrase("Date : " + Common.ToDateString(CircularDate) + "", fCapText));
            //tb1.AddCell(new Phrase("Source : " + Source + "", fCapText));
            //tb1.AddCell(new Phrase(""));
            document.Add(tb1);
            //document.Add(new Phrase("\n"));

            //------------Source Table
            iTextSharp.text.Table tbS = new iTextSharp.text.Table(1);
            tbS.DefaultCellBackgroundColor = iTextSharp.text.Color.WHITE;
            tbS.Width = 90;
            float[] wsS = { 50 };
            tbS.Widths = wsS;
            tbS.Alignment = Element.ALIGN_CENTER;
            tbS.Border = iTextSharp.text.Rectangle.NO_BORDER;
            tbS.DefaultCellBorder = iTextSharp.text.Rectangle.BOX;
            tbS.DefaultHorizontalAlignment = Element.ALIGN_LEFT;
            tbS.BorderColor = iTextSharp.text.Color.WHITE;
            tbS.Cellspacing = 1;
            tbS.Cellpadding = 1;
            tbS.AddCell(new Phrase("Source : " + Source + "", fCapText));
            document.Add(tbS);
            document.Add(new Phrase("\n"));


            //------------Second TABLE 
            iTextSharp.text.Table tb2 = new iTextSharp.text.Table(1);
            tb2.BackgroundColor = iTextSharp.text.Color.WHITE;
            tb2.Width = 90;
            float[] ws2 = { 100 };
            tb2.Widths = ws2;
            tb2.Alignment = Element.ALIGN_CENTER;
            tb2.Border = iTextSharp.text.Rectangle.NO_BORDER;
            tb2.DefaultCellBorder = iTextSharp.text.Rectangle.NO_BORDER;
            tb2.DefaultHorizontalAlignment = Element.ALIGN_LEFT;
            tb2.BorderColor = iTextSharp.text.Color.WHITE;
            tb2.Cellspacing = 1;
            tb2.Cellpadding = 1;
            tb2.AddCell(new Phrase(" ", fCapTextHeading));
            tb2.AddCell(new Phrase("Circular Letter :                           " + CircularNumber + "                         \n", fCapTextHeading));
            //tb2.AddCell(new Phrase("" + CircularNumber + "", fCapTextCirNum));
            document.Add(tb2);
            //document.Add(new Phrase("\n"));

            //------------third TABLE 
            iTextSharp.text.Table tb3 = new iTextSharp.text.Table(1);
            tb3.BackgroundColor = iTextSharp.text.Color.LIGHT_GRAY;
            tb3.Width = 90;
            float[] ws3 = { 100 };
            tb3.Widths = ws3;
            tb3.Alignment = Element.ALIGN_CENTER;
            tb3.Border = iTextSharp.text.Rectangle.NO_BORDER;
            tb3.DefaultHorizontalAlignment = Element.ALIGN_LEFT;
            tb3.DefaultVerticalAlignment = Element.ALIGN_TOP;
            tb3.BorderColor = iTextSharp.text.Color.WHITE;
            tb3.Cellspacing = 1;
            tb3.Cellpadding = 1;
            tb3.AddCell(new Phrase("Topic - " + Topic, fCapTextTitle));
            document.Add(tb3);

            //------------ Fourth TABLE 
            iTextSharp.text.Table tb4 = new iTextSharp.text.Table(1);
            tb4.BackgroundColor = iTextSharp.text.Color.WHITE;
            tb4.Width = 90;
            float[] ws4 = { 100 };
            tb4.Widths = ws4;
            tb4.Alignment = Element.ALIGN_CENTER;
            tb4.Border = iTextSharp.text.Rectangle.NO_BORDER;
            tb4.DefaultCellBorder = iTextSharp.text.Rectangle.NO_BORDER;
            tb4.DefaultHorizontalAlignment = Element.ALIGN_LEFT;
            tb4.BorderColor = iTextSharp.text.Color.WHITE;
            tb4.Cellspacing = 1;
            tb4.Cellpadding = 1;
            tb4.AddCell(new Phrase(Details, fCapTextDetails));
            document.Add(tb4);



            document.Close();
            if (File.Exists(Server.MapPath("Publish/CircularDataFile.pdf")))
            {
                File.Delete(Server.MapPath("Publish/CircularDataFile.pdf"));
            }
            FileStream fs = new FileStream(Server.MapPath("Publish/CircularDataFile.pdf"), FileMode.Create);
            byte[] bb = msReport.ToArray();
            fs.Write(bb, 0, bb.Length);
            fs.Flush();
            fs.Close();
            //AddMessage("Pdf file(" + FileName + ") created successfully.");
        }
        catch (System.Exception ex)
        {
            //AddMessage("Error while creating file(" + FileName + "). ", ex.Message);
        }

        return cirno;
    }
    private void ExportToPDF(string[] SourceFiles, string DestFile, string Header, string Footer)
    {
        MemoryStream MemStream = new MemoryStream();
        iTextSharp.text.Document doc = new iTextSharp.text.Document();
        iTextSharp.text.pdf.PdfReader reader;
        Int32 numberOfPages;
        Int32 currentPageNumber;
        iTextSharp.text.pdf.PdfWriter writer = iTextSharp.text.pdf.PdfWriter.GetInstance(doc, MemStream);
        //-----------------------
        //Adding Header in document
        Phrase p1 = new Phrase();
        p1.Add(new Phrase(Header, FontFactory.GetFont("ARIAL", 9, iTextSharp.text.Font.BOLD)));
        HeaderFooter header = new HeaderFooter(p1, false);
        doc.Header = header;
        header.Alignment = Element.ALIGN_CENTER;
        header.Border = iTextSharp.text.Rectangle.NO_BORDER;
        //Adding Footer in document
        HeaderFooter footer = new HeaderFooter(new Phrase(Footer, FontFactory.GetFont("ARIAL", 7, iTextSharp.text.Font.NORMAL)), true);
        footer.Border = iTextSharp.text.Rectangle.NO_BORDER;
        footer.Alignment = Element.ALIGN_LEFT;
        doc.Footer = footer;

        //-----------------------
        doc.Open();
        iTextSharp.text.pdf.PdfContentByte cb = writer.DirectContent;
        iTextSharp.text.pdf.PdfImportedPage page;
        int rotation;
        //BaseFont baseFont = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, Encoding.ASCII.EncodingName, false);
        BaseFont baseFont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
        for (int i = 0; i <= SourceFiles.Length - 1; i++)
        {
            Byte[] sqlbytes = File.ReadAllBytes(SourceFiles[i]);
            reader = new iTextSharp.text.pdf.PdfReader(sqlbytes);
            numberOfPages = reader.NumberOfPages;
            currentPageNumber = 0;
            while (currentPageNumber < numberOfPages)
            {
                currentPageNumber += 1;
                doc.SetPageSize(PageSize.A4);
                doc.NewPage();
                page = writer.GetImportedPage(reader, currentPageNumber);
                rotation = reader.GetPageRotation(currentPageNumber);
                if ((rotation == 90) || (rotation == 270))
                    cb.AddTemplate(page, 0, -1.0F, 1.0F, 0, 0, reader.GetPageSizeWithRotation(currentPageNumber).Height);
                else
                    cb.AddTemplate(page, 1.0F, 0, 0, 1.0F, 0, 0);
            }
        }
        if (MemStream == null)
        {
            // error message
        }
        else
        {
            doc.Close();
            FileStream fs = new FileStream(DestFile, FileMode.OpenOrCreate, FileAccess.Write);
            fs.Write(MemStream.GetBuffer(), 0, MemStream.GetBuffer().Length);
            fs.Flush();
            fs.Close();
            MemStream.Close();
        }
    }
}
