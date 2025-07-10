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
using System.Configuration;

public partial class HSSQE_Regulation : System.Web.UI.Page
{
    public int UserId
    {
        get { return Common.CastAsInt32(ViewState["UserId"]); }
        set { ViewState["UserId"] = value; }
    }
    public int REGId
    {
        get { return Common.CastAsInt32(ViewState["REGId"]); }
        set { ViewState["REGId"] = value; }
    }
    public int Mail_REGId
    {
        get { return Common.CastAsInt32(ViewState["Mail_REGId"]); }
        set { ViewState["Mail_REGId"] = value; }
    }
    public string UserName
    {
        get { return ViewState["UserName"].ToString(); }
        set { ViewState["UserName"] = value; }
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
            BindREGCat();
            Bindgrid();
            BindMainSource();
        }
    }

    protected void btnDeleteREG_Click(object sender, EventArgs e)
    {
        int REGId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        Common.Execute_Procedures_Select_ByQuery("DELETE FROM DBO.Reg_Regulation WHERE RegId=" + REGId.ToString());
        Common.Execute_Procedures_Select_ByQuery("DELETE FROM DBO.Reg_Categories WHERE RegId=" + REGId.ToString());
        Bindgrid();
    }
    protected void btnPublishREG_Click(object sender, EventArgs e)
    {
        //int REGId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        Common.Execute_Procedures_Select_ByQuery("UPDATE DBO.Reg_Regulation SET STATUS='P' WHERE RegId=" + REGId.ToString());
        btnSave.Visible = btnPublish.Visible = false;
        Bindgrid();

    }

    protected void btnDownloadFile_Click(object sender, EventArgs e)
    {
        int REGId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.Reg_Regulation WHERE RegId=" + REGId.ToString());
        if (dt.Rows.Count > 0)
        {
            string FileName = dt.Rows[0]["AttachmentFileName"].ToString();
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

    protected void Filter_LFI(object sender, EventArgs e)
    {
        Bindgrid();
    }

    public void BindMainSource()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT [MSId],[MSName] FROM [dbo].[Reg_MainSource] ORDER BY MSName");
        ddlMainSource.DataSource = dt;
        ddlMainSource.DataTextField = "MSName";
        ddlMainSource.DataValueField = "MSId";
        ddlMainSource.DataBind();
        ddlMainSource.Items.Insert(0, new ListItem("< Select >", "0"));

        ddlMainSource_Filter.DataSource = dt;
        ddlMainSource_Filter.DataTextField = "MSName";
        ddlMainSource_Filter.DataValueField = "MSId";
        ddlMainSource_Filter.DataBind();
        ddlMainSource_Filter.Items.Insert(0, new ListItem("< Select >", "0"));

    }
    protected void ddlMainSource_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlMainSource.SelectedIndex > 0)
        {
            DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT [SSId],[SSName] FROM [dbo].[Reg_SubSource] WHERE MSId = " + ddlMainSource.SelectedValue.Trim() + "  ORDER BY SSName");
            ddlSubSource.DataSource = dt;
            ddlSubSource.DataTextField = "SSName";
            ddlSubSource.DataValueField = "SSId";
            ddlSubSource.DataBind();
            ddlSubSource.Items.Insert(0, new ListItem("< Select >", "0"));
        }
        else
        {
            ddlSubSource.Items.Clear();
            ddlSubSource.Items.Insert(0, new ListItem("< Select >", "0"));
        }
    }
    protected void ddlMainSource_Filter_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlMainSource_Filter.SelectedIndex > 0)
        {
            DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT [SSId],[SSName] FROM [dbo].[Reg_SubSource] WHERE MSId = " + ddlMainSource_Filter.SelectedValue.Trim() + "  ORDER BY SSName");
            ddlSubSource_Filter.DataSource = dt;
            ddlSubSource_Filter.DataTextField = "SSName";
            ddlSubSource_Filter.DataValueField = "SSId";
            ddlSubSource_Filter.DataBind();
            ddlSubSource_Filter.Items.Insert(0, new ListItem("< Select >", "0"));
        }
        else
        {
            ddlSubSource_Filter.Items.Clear();
            ddlSubSource_Filter.Items.Insert(0, new ListItem("< Select >", "0"));
        }

        Bindgrid();
    }
    
    protected void BindREGCat()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.Reg_Category ORDER BY RegCatName");
        ddlLFICat.DataSource = dt;
        ddlLFICat.DataTextField = "RegCatName";
        ddlLFICat.DataValueField = "RegCatId";
        ddlLFICat.DataBind();
        ddlLFICat.Items.Insert(0, new ListItem("< ALL >", "0"));

        chkREGCats.DataSource = dt;
        chkREGCats.DataTextField = "RegCatName";
        chkREGCats.DataValueField = "RegCatId";
        chkREGCats.DataBind();
    }
    protected void BindVessel()
    {
        string SQLUser = "SELECT V.VESSELCODE,   V.VESSELNAME,EMAIL,SENTON,ACKON FROM DBO.VESSEL V LEFT JOIN DBO.Reg_Vessel_Notifications E ON E.RegId=" + Mail_REGId + " AND E.VESSELCODE=V.VESSELCODE WHERE VESSELSTATUSID=1  " +
                         "UNION " +
                         "SELECT '' AS VESSELCODE, '' AS VESSELNAME,Email,null As SENTON, null AS ACKON FROM dbo.EmTM_department WHERE Email is not null  ORDER BY VESSELNAME ";

        DataTable dtUsers = Common.Execute_Procedures_Select_ByQuery(SQLUser);
        rptUsers.DataSource = dtUsers;
        rptUsers.DataBind();
    }
    protected void btnSendMail_Click(object sender, EventArgs e)
    {
        Mail_REGId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
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
            CheckBox chkSendMail = (CheckBox)rpt.FindControl("chkSendMail");
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

        string SQL = "SELECT * FROM DBO.Reg_Regulation WHERE RegId = " + Mail_REGId;
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
        dt.TableName = "Reg_Regulation";
        string REGNumber = dt.Rows[0]["RegNumber"].ToString();
        string Title = dt.Rows[0]["Title"].ToString();
        string AppName = ConfigurationManager.AppSettings["AppName"].ToString();
        string SchemaFile = Server.MapPath("/"+ AppName + "/EMANAGERBLOB/LPSQE/Regulation/REGSchema.xml");
        string DataFile = Server.MapPath("/" + AppName + "/EMANAGERBLOB/LPSQE/Regulation/REGData.xml");
        string ZipFile = Server.MapPath("/" + AppName + "/EMANAGERBLOB/LPSQE/Regulation/" + REGNumber + ".zip");
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
        sb.Append("<b>REG# : </b>" + REGNumber);
        sb.Append("<br/>");
        sb.Append("<b>Title : </b>" + Title);
        sb.Append("<br/>");
        sb.Append("<br/>");
        sb.Append("Please find the attached data packet to import Regulation.");
        sb.Append("<br/>");
        sb.Append("<br/>");
        sb.Append("***********************************************************************");
        sb.Append("<br/>");
        sb.Append("<br/>");
        sb.Append(fckContentText.Text);

        foreach (RepeaterItem rpt in rptUsers.Items)
        {
            CheckBox chkSendMail = (CheckBox)rpt.FindControl("chkSendMail");
            HiddenField hfdVesselCode = (HiddenField)rpt.FindControl("hfdVesselCode");
            string EmailAddress = chkSendMail.ToolTip;
            string VesselCode = hfdVesselCode.Value;
            if (chkSendMail.Checked)
            {
                if (VesselCode.Trim() != "")
                {
                    Common.Set_Procedures("DBO.REG_InsertRegulation_Vessel_Notifications");
                    Common.Set_ParameterLength(2);
                    Common.Set_Parameters(new MyParameter("@RegId", Mail_REGId), new MyParameter("@VesselCode", VesselCode));
                    DataSet ds = new DataSet();
                    if (Common.Execute_Procedures_IUD(ds))
                    {

                    }
                }
                if (!string.IsNullOrWhiteSpace(EmailAddress))
                {
                    BCCMails.Add(EmailAddress);
                }
                
            }
        }
        string UserEmail = ProjectCommon.getUserEmailByID(UserId.ToString());
        string result = SendMail.SendRegulationMail(ProjectCommon.getUserEmailByID(UserId.ToString()), UserEmail, BCCMails.ToArray(), REGNumber, sb.ToString(), ZipFile);
        if (result == "SENT")
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
        string REGCatSQL = "WHERE 1=1 ";

        if (ddlLFICat.SelectedIndex > 0)
        {
            REGCatSQL = REGCatSQL +  " AND RegID IN ( SELECT RegID FROM DBO.Reg_Categories WHERE RegCATID=" + ddlLFICat.SelectedValue + ")";
        }
        if (txtFilterTag.Text.Trim() != "")
        {
            REGCatSQL = REGCatSQL + " AND Tag Like '%" + txtFilterTag.Text.Trim() + "%' ";
        }
        if (ddlMainSource_Filter.SelectedIndex > 0)
        {
            REGCatSQL = REGCatSQL + " AND MSId =" + ddlMainSource_Filter.SelectedValue + " ";
        }
        if (ddlSubSource_Filter.SelectedIndex > 0)
        {
            REGCatSQL = REGCatSQL + " AND SSId =" + ddlSubSource_Filter.SelectedValue + " ";
        }

        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT RegID,STATUS,RegNUMBER,RegDATE,TITLE,Tag,ATTACHMENTFILENAME,(SELECT CONVERT(VARCHAR,COUNT(*)) FROM DBO.Reg_Vessel_Notifications N WHERE N.RegID=DBO.Reg_Regulation.RegID) AS TOTALSENT,(SELECT CONVERT(VARCHAR,COUNT(*)) FROM DBO.Reg_Vessel_Notifications N WHERE N.RegID=DBO.Reg_Regulation.RegID AND ACKON IS NOT NULL) AS TOTALACK,(SELECT DBO.getREGCategories(RegID)) AS RegCATS, (SELECT ReferenceKey FROM dbo.Moc_Request WHERE ReferenceKey = RegID AND Topic='REGULATION' ) AS ReferenceKey from DBO.Reg_Regulation " + REGCatSQL);

        if (dt != null)
        {
            DataView dv = dt.DefaultView;
            string Filter = " 1=1 ";

            if (txtFDate.Text.Trim() != "")
            {
                Filter += " AND RegDATE >='" + txtFDate.Text + "'";
            }
            if (txtTDate.Text.Trim() != "")
            {
                Filter += " AND RegDATE <='" + txtTDate.Text + "'";
            }

            dv.RowFilter = Filter;
            rptLFI.DataSource = dv.ToTable();
            rptLFI.DataBind();
        }
        else
        {
            rptLFI.DataSource = null;
            rptLFI.DataBind();
        }
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        Bindgrid();
        dvEmailNotification.Visible = false;
    }
    protected void btnAddLFI_Click(object sender, EventArgs e)
    {
        REGId = 0;
        txtREGNo.Text = "";
        txtTitle.Text = "";
        txtRegTag.Text = "";
        txtREGDate.Text = "";
        chkSMSChngReq.Checked = false;
        ddlMainSource.SelectedIndex = 0;
        ddlSubSource.Items.Clear();
        ddlSubSource.Items.Insert(0, new ListItem("< Select >", "0"));
        txtEADate.Text = "";
        chkREGCats.ClearSelection();
        btnPublish.Visible = false;
        btnSave.Visible = true;
        dv_AddLFI.Visible = true;
        txtREGNo.Focus();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        byte[] FileContent = new byte[0];
        string FileName = "";
        //-------------------------------------------
        if (flpUpload.HasFile)
        {
            if (!flpUpload.FileName.EndsWith(".pdf"))
            {
                ProjectCommon.ShowMessage("Please check.. ! Only pdf files are allowed.");
                return;
            }

            FileName = flpUpload.FileName;
            FileContent = flpUpload.FileBytes;
        }
        else
        {
            if (REGId <= 0)
            {
                ProjectCommon.ShowMessage("Please select attachment.");
                return;
            }
        }
        //-------------------------------------------
        string CategoryList = "";
        foreach (System.Web.UI.WebControls.ListItem li in chkREGCats.Items)
        {
            if (li.Selected)
                CategoryList += "," + li.Value;
        }
        if (CategoryList.StartsWith(","))
            CategoryList = CategoryList.Substring(1);

        //-------------------------------------------

        if (txtREGNo.Text.Trim() != "")
        {
            DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT RegID FROM DBO.Reg_Regulation WHERE RegNUMBER='" + txtREGNo.Text.Trim() + "' AND RegID <> " + REGId);
            if (dt.Rows.Count > 0)
            {
                ProjectCommon.ShowMessage("Ref# aready exists in database.");
                txtREGNo.Focus();
                return;
            }
        }

        //-------------------------------------------
        Common.Set_Procedures("DBO.REG_INSERTUPDATERegulation");
        Common.Set_ParameterLength(13);
        Common.Set_Parameters(new MyParameter("@RegID", REGId),
                              new MyParameter("@RegNUMBER", txtREGNo.Text.Trim()),
                              new MyParameter("@MSId", ddlMainSource.SelectedValue.Trim()),
                              new MyParameter("@SSId", ddlSubSource.SelectedValue.Trim()),
                              new MyParameter("@RegDATE", txtREGDate.Text.Trim()),
                              new MyParameter("@EarAppDate", txtEADate.Text.Trim()),
                              new MyParameter("@TITLE", txtTitle.Text.Trim()),
                              new MyParameter("@Tag", txtRegTag.Text.Trim()),
                              new MyParameter("@SMSChangeReqd", chkSMSChngReq.Checked ? "Y" : "N"),
                              new MyParameter("@ATTACHMENTFILENAME", FileName),
                              new MyParameter("@ATTACHMENT", FileContent),
                              new MyParameter("@CREATEDBY", UserName),
                              new MyParameter("@Reg_Categories", CategoryList));
        DataSet ds = new DataSet();
        if (Common.Execute_Procedures_IUD(ds))
        {
            dv_AddLFI.Visible = false;
            Bindgrid();
        }
        else
        {
            ProjectCommon.ShowMessage(Common.ErrMsg);
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        REGId = 0;
        btnClip.Visible = btnClipText.Visible = false;
        btnClipText.Text = "";
        dv_AddLFI.Visible = false;
    }
    protected void btnEditLFI_Click(object sender, EventArgs e)
    {
        REGId = Common.CastAsInt32(((LinkButton)sender).CommandArgument);
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT RegNumber,MSId,SSId,RegDate,EarAppDate,Title,Tag,SMSChangeReqd,Status,AttachmentFileName FROM DBO.Reg_Regulation WHERE RegId=" + REGId.ToString());
        if (dt.Rows.Count > 0)
        {
            txtREGNo.Text = dt.Rows[0]["RegNumber"].ToString();
            ddlMainSource.SelectedValue = dt.Rows[0]["MSId"].ToString();
            ddlMainSource_OnSelectedIndexChanged(sender, e);
            ddlSubSource.SelectedValue = dt.Rows[0]["SSId"].ToString();
            txtTitle.Text = dt.Rows[0]["Title"].ToString();
            txtRegTag.Text = dt.Rows[0]["Tag"].ToString();
            chkSMSChngReq.Checked = (dt.Rows[0]["SMSChangeReqd"].ToString() == "Y");
            txtREGDate.Text = Common.ToDateString(dt.Rows[0]["RegDate"]);
            txtEADate.Text = Common.ToDateString(dt.Rows[0]["EarAppDate"]);

            chkREGCats.ClearSelection();
            SelectREGCategory();

            if (dt.Rows[0]["AttachmentFileName"].ToString().Trim() != "")
            {
                btnClip.Visible = true;
                btnClipText.Visible = true;
                btnClipText.Text = dt.Rows[0]["AttachmentFileName"].ToString();
            }

            btnSave.Visible = (dt.Rows[0]["Status"].ToString() == "O");
            btnPublish.Visible = (dt.Rows[0]["Status"].ToString() == "O");

            dv_AddLFI.Visible = true;
        }
    }
    public void SelectREGCategory()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT RegCatId FROM DBO.[Reg_Categories] WHERE RegId=" + REGId.ToString());
        if (dt.Rows.Count > 0)
        {
            foreach (DataRow dr in dt.Rows)
            {
                foreach (ListItem lst in chkREGCats.Items)
                {
                    if (lst.Value.Trim() == dr["RegCatId"].ToString().Trim())
                        lst.Selected = true;
                }
            }
        }

    }
    protected void btnClip_Click(object sender, ImageClickEventArgs e)
    {
        //int REGId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        DownloadFile();
    }
    protected void btnClipText_Click(object sender, EventArgs e)
    {
        //int REGId = Common.CastAsInt32(((LinkButton)sender).CommandArgument);
        DownloadFile();
    }
    protected void DownloadFile()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT AttachmentFileName,Attachment FROM DBO.Reg_Regulation WHERE RegId=" + REGId);
        if (dt.Rows.Count > 0)
        {
            string FileName = dt.Rows[0]["AttachmentFileName"].ToString();
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
        REGId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);

        dv_Action.Visible = true;
    }
    protected void btnCloseAction_Click(object sender, EventArgs e)
    {
        REGId = 0;
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
                new MyParameter("@Topic", "REGULATION"),
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
                new MyParameter("@ReferenceKey", REGId)
                );
            DataSet ds = new DataSet();
            ds.Clear();
            Boolean res;
            res = Common.Execute_Procedures_IUD(ds);

            if (res)
            {
                //Common.Execute_Procedures_Select_ByQuery("UPDATE DBO.Cir_Circular SET STATUS=3 WHERE CId=" + CId);
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