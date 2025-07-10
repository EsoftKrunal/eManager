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
using System.Text.RegularExpressions;

public partial class Docket_RFQ : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();

    public int DocketId
    {
        get { return Common.CastAsInt32(ViewState["DocketId"]); }
        set { ViewState["DocketId"] = value; }
    }
    public int RFQId
    {
        get { return Common.CastAsInt32(ViewState["RFQId"]); }
        set { ViewState["RFQId"] = value; }
    }
    public string GUID
    {
        get { return ViewState["GUID"].ToString(); }
        set { ViewState["GUID"] = value; }
    }
    public bool OrderCreated
    {
        get { return Convert.ToBoolean(ViewState["OrderCreated"]); }
        set { ViewState["OrderCreated"] = value; }
    }
    public int UserId
    {
        get { return Common.CastAsInt32(ViewState["UserId"]); }
        set { ViewState["UserId"] = value; }
    }
    public int POId
    {
        get { return Common.CastAsInt32(ViewState["POId"]); }
        set { ViewState["POId"] = value; }
    }
    //-------------------------
    Random rnd = new Random();
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg_Mail.Text = "";
        lblMsgMain.Text = "";
        lbl_MsgYards.Text = "";
        lblMsgSendMail.Text = "";
        lblMsg_Pwd.Text = "";
        // -------------------------- SESSION CHECK ----------------------------------------------
        ProjectCommon.SessionCheck();
        // -------------------------- SESSION CHECK END ----------------------------------------------
        if (!IsPostBack)
        {
            btnCreatePO.Visible = false;
            DocketId = Common.CastAsInt32(Request.QueryString["DDId"]);
            UserId = Common.CastAsInt32(Session["loginid"]);
            btn_AddRFQ.Visible = false;
            ShowSummary();
            LoadYards(sender, e);
            BindRFQ();
            //---------------------------

        }
    }
    public void ShowSummary()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT *,(SELECT VESSELNAME FROM DBO.VESSEL V WHERE V.VESSELID=D.VESSELID) AS VESSELNAME,ISNULL((SELECT R.RFQID FROM DD_Docket_RFQ_Master R WHERE R.DOCKETID=D.DOCKETID AND R.STATUS='P'),0) AS PORFQId,ApprovalOn,GMSupApprovalOn FROM DD_DocketMaster D WHERE DOCKETID=" + DocketId.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            lblDocketNo.Text = dt.Rows[0]["DocketNo"].ToString();
            lblVessel.Text = dt.Rows[0]["VESSELNAME"].ToString();
            lblType.Text = dt.Rows[0]["DocketType"].ToString();
            lblPlanDuration.Text = Common.ToDateString(dt.Rows[0]["StartDate"]) + " To " + Common.ToDateString(dt.Rows[0]["EndDate"]);
            POId = Common.CastAsInt32(dt.Rows[0]["PORFQId"]);
            OrderCreated = POId > 0;

            DateTime? ApprovalOn = null;
            if (!Convert.IsDBNull(dt.Rows[0]["ApprovalOn"]))
                ApprovalOn = Convert.ToDateTime(dt.Rows[0]["ApprovalOn"]);

            DateTime? GMSupApprovalOn = null;
            if (!Convert.IsDBNull(dt.Rows[0]["GMSupApprovalOn"]))
                GMSupApprovalOn = Convert.ToDateTime(dt.Rows[0]["GMSupApprovalOn"]);

            //---------------------------
            btn_AddRFQ.Visible = true && !OrderCreated && (ApprovalOn != null);
            btnCreatePO.Visible = !OrderCreated && (GMSupApprovalOn != null);
        }
    }
    protected void LoadYards(object sender, EventArgs e)
    {
        DataTable dtGroups = new DataTable();
        string strSQL = "SELECT * FROM DD_YardMaster where yardname like '%" + txtYard.Text.Trim() + "%' Order By YardName";
        dtGroups = Common.Execute_Procedures_Select_ByQuery(strSQL);
        rptYards.DataSource = dtGroups;
        rptYards.DataBind();
    }
    public void BindRFQ()
    {
        DataTable dtSubJobs = Common.Execute_Procedures_Select_ByQuery("SELECT R.*,Y.YardName,case when R.status='C' then 'RFQ Created' when R.status='W' then 'Awaiting Quote' when R.status='Q' then 'Quotes Recd.' when R.status='I' then 'Cancelled' when R.status='P' then 'Order Placed' else '' end as StatusName from DD_Docket_RFQ_Master R INNER JOIN [dbo].[DD_DocketMaster] D ON R.DOCKETID=D.DOCKETID INNER JOIN [dbo].[DD_YardMaster] Y ON Y.YardId=R.YardId WHERE R.DOCKETID=" + DocketId);
        rptRFQ.DataSource = dtSubJobs;
        rptRFQ.DataBind();
    }

    protected void btnQAnalysis_Click(object sender, EventArgs e)
    {
        Response.Redirect("Docket_Sup_Analysis.aspx?DocketId=" + DocketId);
    }
    protected void btnQA_Click(object sender, EventArgs e)
    {
        Response.Redirect("Docket_Quote_Analysis.aspx?DocketId=" + DocketId);
    }
    protected void btnDocketView_Click(object sender, EventArgs e)
    {
        int TableId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT TOP 1 VersionNo,Attachment FROM DD_Docket_Publish_History WHERE DOCKETID=" + DocketId + " ORDER BY TABLEID DESC");
        if (dt.Rows.Count > 0)
        {
            string FileName = lblDocketNo.Text.Replace("/", "-") + "-" + dt.Rows[0]["VersionNo"].ToString() + ".pdf";
            byte[] buff = (byte[])dt.Rows[0]["Attachment"];
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileName);
            Response.BinaryWrite(buff);
            Response.Flush();
            Response.End();
        }

    }
    protected void btnDownloadSOR_Click(object sender, EventArgs e)
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT [SubJobCode],[SubJobName],AttachmentName,Attachment, RIGHT(AttachmentName, 5) As FileExt  FROM [dbo].[DD_DocketSubJobs] WHERE DOCKETID=" + DocketId + " AND AttachmentName IS NOT Null and ATTACHMENT IS NOT NULL ORDER BY SubJobCode ");

        if (dt.Rows.Count > 0)
        {
            string appname = ConfigurationManager.AppSettings["AppName"].ToString();
            if (Directory.Exists(Server.MapPath("DownLoadSOR")))
            {
                Array.ForEach(Directory.GetFiles(Server.MapPath("/" + appname + "/EMANAGERBLOB/PMS/DryDock/DownLoadSOR")), File.Delete);
            }
            else
            {
                Directory.CreateDirectory(Server.MapPath("/" + appname + "/EMANAGERBLOB/PMS/DryDock/DownLoadSOR"));
            }

            foreach (DataRow dr in dt.Rows)
            {
                //string FileName = dr["SubJobCode"].ToString().Trim() + dr["FileExt"].ToString();
                //byte[] buff = (byte[])dr["Attachment"];
                //System.IO.File.WriteAllBytes(Server.MapPath("DownLoadSOR/" + FileName), buff);

                string Extname = dr["FileExt"].ToString();
                if (!Extname.StartsWith("."))
                    Extname = Extname.Substring(Extname.IndexOf("."));

                string FileName = dr["SubJobCode"].ToString().Trim() + Extname;
                byte[] buff = (byte[])dr["Attachment"];
                System.IO.File.WriteAllBytes(Server.MapPath("/"+ appname + "/EMANAGERBLOB/PMS/DryDock/DownLoadSOR/" + FileName), buff);
            }

            string FileToZip = Server.MapPath("DownLoadSOR");
            string ZipFile = Server.MapPath("/"+ appname + "/EMANAGERBLOB/PMS/DryDock/TEMP/SOR.zip");

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

    protected void btn_AddRFQ_Click(object sender, EventArgs e)
    {
        RFQId = 0;
        BindRFQ();
        btn_EditRFQ.Visible = false;
        dv_Yards.Visible = true;
        btn_CreateRFQ.Enabled = true;
    }
    protected void btnSelectRFQ_Click(object sender, EventArgs e)
    {
        //RFQId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        RFQId = Common.CastAsInt32(((RadioButton)sender).Attributes["RFQId"]);
        BindRFQ();
        DataTable dtRFQ = Common.Execute_Procedures_Select_ByQuery("SELECT STATUS FROM DD_Docket_RFQ_Master WHERE RFQID=" + RFQId);
        if (dtRFQ.Rows.Count > 0)
        {
            btn_EditRFQ.Visible = true & !OrderCreated & (dtRFQ.Rows[0]["STATUS"].ToString().Trim() != "I");
            btn_Resubmit.Visible = true & !OrderCreated & (dtRFQ.Rows[0]["STATUS"].ToString().Trim() == "Q");
        }
    }

    protected void btnUndoCancel_Click(object sender, EventArgs e)
    {
        RFQId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);

        DataTable dtRFQ = Common.Execute_Procedures_Select_ByQuery("UPDATE DD_Docket_RFQ_Master SET STATUS = 'W' WHERE RFQID=" + RFQId);
        BindRFQ();

    }
    protected void btn_CreateRFQ_Click(object sender, EventArgs e)
    {
        //------------------------------
        btn_CreateRFQ.Enabled = false;
        foreach (RepeaterItem ri in rptYards.Items)
        {
            CheckBox ch = (CheckBox)ri.FindControl("chkSelect");
            if (ch.Checked)
            {
                try
                {
                    Common.Set_Procedures("[dbo].[DD_CreateRFQ]");
                    Common.Set_ParameterLength(3);
                    Common.Set_Parameters(
                       new MyParameter("@DocketId", DocketId),
                       new MyParameter("@YardId", ch.CssClass),
                       new MyParameter("@CreatedBy", Session["FullName"].ToString())
                       );
                    DataSet ds = new DataSet();
                    ds.Clear();
                    Boolean res;
                    res = Common.Execute_Procedures_IUD(ds);
                    if (res)
                    {
                        BindRFQ();
                        lbl_MsgYards.Text = "RFQ created successfully.";
                    }
                    else
                    {
                        lbl_MsgYards.Text = "Unable to create RFQ. Error :" + Common.ErrMsg;
                    }
                }
                catch (Exception ex)
                {
                    lbl_MsgYards.Text = "Unable to create RFQ. Error :" + ex.Message;
                }
            }
        }
        //------------------------------
        BindRFQ();
        btn_CreateRFQ.Enabled = true;
        //------------------------------
        //dv_Yards.Visible = true;
    }
    protected void btn_CloseYard_Click(object sender, EventArgs e)
    {
        dv_Yards.Visible = false;
    }

    protected void btnMailSend_Click(object sender, EventArgs e)
    {
        RFQId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT R.*,Y.YardName,case when R.status='C' then 'RFQ Created' when R.status='W' then 'Awaiting Quote' when R.status='Q' then 'Quotes Recd.' end as StatusName, Y.Email  from DD_Docket_RFQ_Master R INNER JOIN [dbo].[DD_DocketMaster] D ON R.DOCKETID=D.DOCKETID INNER JOIN [dbo].[DD_YardMaster] Y ON Y.YardId=R.YardId WHERE R.RFQId=" + RFQId);

        lbl_Mail_RFQNo.Text = dt.Rows[0]["RFQNO"].ToString();
        lbl_Mail_YardName.Text = dt.Rows[0]["YardName"].ToString();
        txtMailTo.Text = dt.Rows[0]["Email"].ToString();
        txtMailCC.Text = EProjectCommon.gerUserEmail(Session["loginid"].ToString());
        txtMailSubject.Text = "Request for Drydock Quotation : " + lblDocketNo.Text.Trim();
        GUID = dt.Rows[0]["GUID"].ToString();
        dv_Mail.Visible = true;
        string url = Request.Url.ToString();
        //  string httpPath = @"http://emanagershore.energiosmaritime.com/public/Docket_quote.aspx?Key=" + GUID;
        string httpPath =  ConfigurationManager.AppSettings["DryDockMail"].ToString() + GUID;
        string DefaultText = File.ReadAllText(Server.MapPath("~/Modules/PMS/DryDock/mailcontent.htm"));
        DefaultText = DefaultText.Replace("$LINK$", httpPath);
        DefaultText = DefaultText.Replace("$YRADNAME$", lbl_Mail_YardName.Text);
        DefaultText = DefaultText.Replace("$SENDERMAIL$", EProjectCommon.gerUserEmail(Session["loginid"].ToString()));
        dv_mailcontent.InnerHtml = DefaultText;
    }
    protected void btnSendMailToSelf_Click(object sender, EventArgs e)
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT R.*,Y.YardName,case when R.status='C' then 'RFQ Created' when R.status='W' then 'Awaiting Quote' when R.status='Q' then 'Quotes Recd.' end as StatusName, Y.Email  from DD_Docket_RFQ_Master R INNER JOIN [dbo].[DD_DocketMaster] D ON R.DOCKETID=D.DOCKETID INNER JOIN [dbo].[DD_YardMaster] Y ON Y.YardId=R.YardId WHERE R.RFQId=" + POId);
        string[] ToAdd = { EProjectCommon.getUserEmailByID(Session["loginid"].ToString()) };
        string[] BlankArray = { };
        string Error = "";
        string _GUID = dt.Rows[0]["GUID"].ToString();
        //string httpPath = @"http://emanagershore.energiosmaritime.com/public/JobTracking.aspx?Key=" + _GUID;
        //string httpPath1 = @"http://emanagershore.energiosmaritime.com/public/CostTracking.aspx?Key=" + _GUID;

        string httpPath = ConfigurationManager.AppSettings["JobTracking"].ToString() + GUID;
        string httpPath1 = ConfigurationManager.AppSettings["CostTracking"].ToString() + GUID;
        string Message = "Dear User,<br/><br/>Please find the links to access the docket from internet.<br/><br/><b>Job Tracking</b><br/><a target='_blank' href='" + httpPath + "'>" + httpPath + "</a><br/><br/><b>Cost Tracking</b><br/><a target='_blank' href='" + httpPath1 + "'>" + httpPath1 + "</a><br/><br/>Thanks<br/>";
        bool success = EProjectCommon.SendeMail_MTM_ATT(Common.CastAsInt32(Session["loginid"]), "emanager@energiossolutions.com", "emanager@energiossolutions.com", ToAdd, BlankArray, BlankArray, "Docket Link(s) : " + lblDocketNo.Text.Trim(), Message, out Error, BlankArray);
        if (success)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "axdxafdsda", "alert('Mail sent successfully.');", true);
        }
    }

    protected void btnSendMail_Click(object sender, EventArgs e)
    {
        if (txtMailTo.Text.Trim() == "")
        {
            lblMsgSendMail.Text = "Please enter email To address.";
            txtMailTo.Focus();
            return;
        }
        if (txtMailSubject.Text.Trim() == "")
        {
            lblMsgSendMail.Text = "Please enter email subject.";
            txtMailSubject.Focus();
            return;
        }
        if (txtMailText.Text.Trim() == "")
        {
            lblMsgSendMail.Text = "Please enter email text.";
            txtMailText.Focus();
            return;
        }

        DataTable dt1 = Common.Execute_Procedures_Select_ByQuery("SELECT DOCKETID FROM DD_Docket_Publish_History D WHERE D.DOCKETID=" + DocketId);
        if (dt1.Rows.Count <= 0)
        {
            lblMsgSendMail.Text = "Docket Need to be Published before mail sending.";
            txtMailText.Focus();
            return;
        }
        string appname = ConfigurationManager.AppSettings["AppName"].ToString();
        string FilePath = Server.MapPath("/" + appname + "/EMANAGERBLOB/PMS/DryDock/TEMP/") + lblDocketNo.Text.Replace("/", "-") + ".pdf";
        string SORFilePath = Server.MapPath("/" + appname + "/EMANAGERBLOB/PMS/DryDock/TEMP/") + lblDocketNo.Text.Replace("/", "-") + "_SOR.zip";

        //string FilePath = Server.MapPath("/EMANAGERBLOB/PMS/DryDock/TEMP/") + lblDocketNo.Text.Replace("/", "-") + ".pdf";
        //string SORFilePath = Server.MapPath("/EMANAGERBLOB/PMS/DryDock/TEMP/") + lblDocketNo.Text.Replace("/", "-") + "_SOR.zip";

        string Error;
        string[] ToAddress = txtMailTo.Text.Trim().Split(';');
        string[] CCAddress = txtMailCC.Text.Trim().Split(';');
        string[] BCCAddress = { };
        //string[] AttachmentFiles = { FilePath, SORFilePath };        
        string[] AttachmentFiles = { };
        string MailText = "";

        try
        {
            MailText = txtMailText.Text;

            //DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT TOP 1 VersionNo,Attachment FROM DD_Docket_Publish_History WHERE DOCKETID=" + DocketId + " ORDER BY TABLEID DESC");
            //if (dt.Rows.Count > 0)
            //{
            //    string url = Request.Url.ToString();
            //    string httpPath = url.Substring(0, url.LastIndexOf("/")).ToLower().Replace("drydock", "Public");
            //    MailText = txtMailText.Text;
            //    byte[] buff = (byte[])dt.Rows[0]["Attachment"];
            //    if (File.Exists(FilePath))
            //    {
            //        File.Delete(FilePath);
            //    }
            //    using (FileStream fs = new FileStream(FilePath, FileMode.Create))
            //    {
            //        fs.Write(buff, 0, buff.Length);
            //        fs.Flush();
            //        fs.Close();
            //    }
            //}

            //DataTable dtSOR = Common.Execute_Procedures_Select_ByQuery("SELECT [SubJobCode],[SubJobName],AttachmentName,Attachment, RIGHT(AttachmentName, 4) As FileExt  FROM [dbo].[DD_DocketSubJobs] WHERE DOCKETID=" + DocketId + " AND AttachmentName IS NOT Null ORDER BY SubJobCode ");

            //if (dtSOR.Rows.Count > 0)
            //{
            //    if (Directory.Exists(Server.MapPath("DownLoadSOR")))
            //    {
            //        Array.ForEach(Directory.GetFiles(Server.MapPath("DownLoadSOR")), File.Delete);
            //    }
            //    else
            //    {
            //        Directory.CreateDirectory(Server.MapPath("DownLoadSOR"));
            //    }

            //    foreach (DataRow dr in dtSOR.Rows)
            //    {
            //        string FileName = dr["SubJobCode"].ToString().Trim() + dr["FileExt"].ToString();
            //        byte[] buff = (byte[])dr["Attachment"];
            //        System.IO.File.WriteAllBytes(Server.MapPath("DownLoadSOR/" + FileName), buff);
            //    }

            //    string FileToZip = Server.MapPath("DownLoadSOR");
            //    string ZipFile = SORFilePath;

            //    if (File.Exists(ZipFile))
            //        File.Delete(ZipFile);

            //    using (ZipFile zip = new ZipFile())
            //    {
            //        zip.AddDirectory(FileToZip);
            //        zip.Save(ZipFile);
            //    }

            //} 

            bool success = EProjectCommon.SendeMail_MTM_ATT(Common.CastAsInt32(Session["loginid"]), "emanager@energiossolutions.com", "emanager@energiossolutions.com", ToAddress, CCAddress, BCCAddress, txtMailSubject.Text.Trim(), MailText, out Error, AttachmentFiles);
            if (success)
            {
                Common.Execute_Procedures_Select_ByQuery("Update DD_Docket_RFQ_Master SET [MailSentBy]='" + Session["FullName"].ToString() + "', [MailSentOn]=getdate(), [Status]='W' WHERE RFQId=" + RFQId);
                BindRFQ();
                lblMsgSendMail.Text = "Mail sent successfully.";
            }
        }
        catch (Exception ex)
        {
            lblMsgSendMail.Text = "Unable to send mail. Error: " + ex.Message;
        }


    }
    protected void btnClose_Mail_Click(object sender, EventArgs e)
    {
        RFQId = 0;
        lbl_Mail_RFQNo.Text = "";
        lbl_Mail_YardName.Text = "";
        txtMailTo.Text = "";
        txtMailCC.Text = "";
        txtMailSubject.Text = "";
        GUID = "";
        dv_Mail.Visible = false;
    }
    protected void btn_EditRFQ_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "editrfq", "window.open('../DryDock/Public/Docket_quote.aspx?RFQId=" + RFQId + "','_blank', '', '');", true);
    }
    protected void btn_Resubmit_Click(object sender, EventArgs e)
    {
        string SQL = "UPDATE DD_Docket_RFQ_Master SET STATUS='W' WHERE RFQId=" + RFQId;
        Common.Execute_Procedures_Select_ByQuery(SQL);
        BindRFQ();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "axdxafdsda", "alert('Status changed successfully.');", true);
    }
    protected void btnPrintRFQ_Click(object sender, EventArgs e)
    {
        int RFQId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        if (RFQId > 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "abc", "window.open('PublishRFQ.aspx?RFQid=" + RFQId + "','');", true);
            //------------------

            //------------------
        }

        //DataTable dtSummary = Common.Execute_Procedures_Select_ByQuery("SELECT R.*,(SELECT VESSELNAME FROM DBO.VESSEL V WHERE V.VESSELID=D.VESSELID) AS VESSELNAME FROM DD_Docket_RFQ_Master R INNER JOIN DD_DocketMaster D ON D.DocketId = R.DocketId WHERE R.RFQId=" + RFQId + " AND R.DocketId=" + DocketId.ToString());
        //DataTable dtCats = Common.Execute_Procedures_Select_ByQuery("SELECT CATID,CATCODE,CATNAME FROM DD_JobCategory ORDER BY CATCODE");
        //DataTable dtJobs = Common.Execute_Procedures_Select_ByQuery("SELECT CATID,DocketJobId,JOBCODE,JOBNAME,JobDesc FROM DD_Docket_RFQ_Jobs WHERE RFQId=" + RFQId + " AND DOCKETID=" + DocketId + " ORDER BY JOBCODE");
        //DataTable dtSubJobs = Common.Execute_Procedures_Select_ByQuery("SELECT DocketSubJobId,[DocketJobId],[DocketId],[SubJobCode],[SubJobName],AttachmentName,[Unit],[BidQty],[LongDescr],CASE WHEN [CostCategory] = 'Y' THEN 'Yard Cost' WHEN [CostCategory] = 'N' THEN 'Non Yard Cost' ELSE '' END AS [CostCategory] , CASE WHEN [OutsideRepair] = 'Y' THEN 'Yes'  WHEN [OutsideRepair] = 'N' THEN 'No' ELSE '' END AS [OutsideRepair],[QuoteQty],[UnitPrice],[DiscountPer],[NetAmount],vendorremarks FROM [dbo].[DD_Docket_RFQ_SubJobs] WHERE RFQId=" + RFQId + " AND DOCKETID=" + DocketId + " ORDER BY SubJobCode");
        //CreatePDF(dtSummary, dtCats, dtJobs, dtSubJobs);
    }

    private void CreatePDF(DataTable dtSummary, DataTable dtCats, DataTable dtJobs, DataTable dtSubJobs)
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

            //iTextSharp.text.Font fCapText_11_blue = FontFactory.GetFont("ARIAL", 8, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLUE);
            //iTextSharp.text.Font fCapText_11_blue_reg = FontFactory.GetFont("ARIAL", 8, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLUE);

            iTextSharp.text.Font fCapText_11_blue = FontFactory.GetFont("ARIAL", 8, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLUE);
            iTextSharp.text.Font fCapText_11_blue_reg = FontFactory.GetFont("ARIAL", 8, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLUE);

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
            PdfPCell cell = new PdfPCell(new Phrase("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\nRFQ Details\n\n", fCapText_15));
            cell.BorderColor = Color.WHITE;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            tblPage1.AddCell(cell);
            // Vessel Name -----------------
            cell = new PdfPCell(new Phrase(dtSummary.Rows[0]["VESSELNAME"].ToString() + "\n\n", fCapText_13));
            cell.BorderColor = Color.WHITE;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            tblPage1.AddCell(cell);
            // DocketNo -----------------
            cell = new PdfPCell(new Phrase(dtSummary.Rows[0]["RFQNo"].ToString() + "\n\n", fCapText_13));
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
                DataTable dtJobs_Temp = dvJobs.ToTable();

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
                    dv_SubJobs.RowFilter = "DocketJobId=" + drJ["DocketJobId"].ToString();
                    DataTable dt_SubJobs_Temp = dv_SubJobs.ToTable();

                    PdfPTable tblPage_SubJobs = new PdfPTable(1);
                    tblPage_SubJobs.SplitLate = false;
                    tblPage_SubJobs.SplitRows = true;

                    tblPage_SubJobs.HorizontalAlignment = Element.ALIGN_CENTER;
                    tblPage_SubJobs.SetWidths(wsCom);

                    cell.AddElement(new Phrase(drJ["JOBNAME"].ToString() + "\n", fCapText_11));
                    cell.AddElement(new Phrase("Job Description : ", fCapText_11));
                    cell.AddElement(new Phrase(drJ["JobDesc"].ToString(), fCapText_11_Reg));
                    cell.BorderColor = Color.BLACK;
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    tblPage_SubJobs.AddCell(cell);

                    foreach (DataRow drS in dt_SubJobs_Temp.Rows)
                    {
                        // --- table for columer details of subjobs

                        PdfPTable tblPage_SubJobs_OtherDetails = new PdfPTable(9);
                        tblPage_SubJobs_OtherDetails.SplitLate = false;
                        tblPage_SubJobs_OtherDetails.SplitRows = true;

                        float[] wsCom_SubJobs_other = { 20, 10, 10, 10, 10, 10, 10, 10, 10 };
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

                        cell = new PdfPCell(new Phrase("Quote Qty", fCapText_11));
                        cell.BorderColor = Color.BLACK;
                        cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        tblPage_SubJobs_OtherDetails.AddCell(cell);

                        cell = new PdfPCell(new Phrase("Unit Rate", fCapText_11));
                        cell.BorderColor = Color.BLACK;
                        cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        tblPage_SubJobs_OtherDetails.AddCell(cell);

                        cell = new PdfPCell(new Phrase("Discount %", fCapText_11));
                        cell.BorderColor = Color.BLACK;
                        cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        tblPage_SubJobs_OtherDetails.AddCell(cell);

                        cell = new PdfPCell(new Phrase("Net Amount", fCapText_11));
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

                        cell = new PdfPCell(new Phrase(drS["QuoteQty"].ToString(), fCapText_11_Reg));
                        cell.BorderColor = Color.BLACK;
                        cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        tblPage_SubJobs_OtherDetails.AddCell(cell);

                        cell = new PdfPCell(new Phrase(drS["UnitPrice"].ToString(), fCapText_11_Reg));
                        cell.BorderColor = Color.BLACK;
                        cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        tblPage_SubJobs_OtherDetails.AddCell(cell);

                        cell = new PdfPCell(new Phrase(drS["DiscountPer"].ToString(), fCapText_11_Reg));
                        cell.BorderColor = Color.BLACK;
                        cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        tblPage_SubJobs_OtherDetails.AddCell(cell);

                        cell = new PdfPCell(new Phrase(drS["NetAmount"].ToString(), fCapText_11_Reg));
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
                        cell.Colspan = 10;
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;

                        cell.AddElement(new Phrase("Vendor Remarks : ", fCapText_11_blue));

                        if (drS["vendorremarks"].ToString().Trim() == "")
                            cell.AddElement(new Phrase("NIL", fCapText_11_blue_reg));
                        else
                            cell.AddElement(new Phrase(drS["vendorremarks"].ToString(), fCapText_11_blue_reg));

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
            string appname = ConfigurationManager.AppSettings["AppName"].ToString();
            if (File.Exists(Server.MapPath("/"+ appname +"/Modules/PMS/DryDock/" + "RFQ.pdf")))
            {
                File.Delete(Server.MapPath("/"+ appname +"/Modules/PMS/DryDock/" + "RFQ.pdf"));
            }
            FileStream fs = new FileStream(Server.MapPath("/" + appname + "/Modules/PMS/DryDock/" + "RFQ.pdf"), FileMode.Create);
            byte[] bb = msReport.ToArray();
            fs.Write(bb, 0, bb.Length);
            fs.Flush();
            fs.Close();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "RFQ Print", "window.open('RFQ.pdf?rnd=" + rnd.Next() + "','','','');", true);

        }
        catch (System.Exception ex)
        {
            lblMsgMain.Text = "Unable to print. Error : " + ex.Message;
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        int RFQId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);

        try
        {
            Common.Execute_Procedures_Select_ByQuery("UPDATE DD_Docket_RFQ_Master SET Status='I' WHERE DOCKETID=" + DocketId + " AND RFQId=" + RFQId);
            BindRFQ();
            lblMsgMain.Text = "RFQ cancelled successfully.";
        }
        catch (Exception ex)
        {
            lblMsgMain.Text = "Unable to cancel RFQ. Error : " + ex.Message;
        }

    }
    protected void btnCreateMail_Click(object sender, EventArgs e)
    {
        //------------------------------------------//
        DataTable dt1 = Common.Execute_Procedures_Select_ByQuery("SELECT DOCKETID FROM DD_Docket_Publish_History D WHERE D.DOCKETID=" + DocketId);
        if (dt1.Rows.Count <= 0)
        {
            lblMsg_Mail.Text = "Docket Need to be Published before creating mail.";
            txtMailText.Focus();
            return;
        }
        //------------------------------------------//
        try
        {
            string appname = ConfigurationManager.AppSettings["AppName"].ToString();
            string FilePath = Server.MapPath("/" + appname + "/EMANAGERBLOB/PMS/DryDock/TEMP/") + lblDocketNo.Text.Replace("/", "-") + ".pdf";
            string SORFilePath = Server.MapPath("/" + appname + "/EMANAGERBLOB/PMS/DryDock/TEMP/") + lblDocketNo.Text.Replace("/", "-") + "_SOR.zip";


            //string FilePath = Server.MapPath("/EMANAGERBLOB/PMS/DryDock/TEMP/") + lblDocketNo.Text.Replace("/", "-") + ".pdf";
            //string SORFilePath = Server.MapPath("/EMANAGERBLOB/PMS/DryDock/TEMP/") + lblDocketNo.Text.Replace("/", "-") + "_SOR.zip";

            dv_mailcontent.InnerHtml = txtMailText.Text;
            //------------------------------------------//
            //DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT TOP 1 VersionNo,Attachment FROM DD_Docket_Publish_History WHERE DOCKETID=" + DocketId + " ORDER BY TABLEID DESC");
            //if (dt.Rows.Count > 0)
            //{
            //    byte[] buff = (byte[])dt.Rows[0]["Attachment"];
            //    if (File.Exists(FilePath))
            //    {
            //        File.Delete(FilePath);
            //    }
            //    using (FileStream fs = new FileStream(FilePath, FileMode.Create))
            //    {
            //        fs.Write(buff, 0, buff.Length);
            //        fs.Flush();
            //        fs.Close();
            //    }
            //}
            PublishReportToFile(FilePath);
            //------------------------------------------//
            DataTable dtSOR = Common.Execute_Procedures_Select_ByQuery("SELECT [SubJobCode],[SubJobName],AttachmentName,Attachment, RIGHT(AttachmentName, 4) As FileExt  FROM [dbo].[DD_DocketSubJobs] WHERE DOCKETID=" + DocketId + "AND isnull(CostCategory,'Y')='Y' AND AttachmentName IS NOT Null and ATTACHMENT IS NOT NULL ORDER BY SubJobCode ");
            if (dtSOR.Rows.Count > 0)
            {

                if (Directory.Exists(Server.MapPath("/" + appname + "/EMANAGERBLOB/PMS/DryDock/DownLoadSOR")))
                { Array.ForEach(Directory.GetFiles(Server.MapPath("/" + appname + "/EMANAGERBLOB/PMS/DryDock/DownLoadSOR")), File.Delete); }
                else
                { Directory.CreateDirectory(Server.MapPath("/" + appname + "/EMANAGERBLOB/PMS/DryDock/DownLoadSOR")); }

                foreach (DataRow dr in dtSOR.Rows)
                {
                    string FileName = dr["SubJobCode"].ToString().Trim() + dr["FileExt"].ToString();
                    byte[] buff = (byte[])dr["Attachment"];
                    System.IO.File.WriteAllBytes(Server.MapPath("/" + appname + "/EMANAGERBLOB/PMS/DryDock/DownLoadSOR/" + FileName), buff);
                }

                string FileToZip = Server.MapPath("/" + appname + "/EMANAGERBLOB/PMS/DryDock/DownLoadSOR");
                string ZipFile = SORFilePath;

                //if (Directory.Exists(Server.MapPath("/EMANAGERBLOB/PMS/DryDock/DownLoadSOR")))
                //{ Array.ForEach(Directory.GetFiles(Server.MapPath("/EMANAGERBLOB/PMS/DryDock/DownLoadSOR")), File.Delete); }
                //else
                //{ Directory.CreateDirectory(Server.MapPath("/EMANAGERBLOB/PMS/DryDock/DownLoadSOR")); }

                //foreach (DataRow dr in dtSOR.Rows)
                //{
                //    string FileName = dr["SubJobCode"].ToString().Trim() + dr["FileExt"].ToString();
                //    byte[] buff = (byte[])dr["Attachment"];
                //    System.IO.File.WriteAllBytes(Server.MapPath("/EMANAGERBLOB/PMS/DryDock/DownLoadSOR/" + FileName), buff);
                //}

                //string FileToZip = Server.MapPath("/EMANAGERBLOB/PMS/DryDock/DownLoadSOR");
                //string ZipFile = SORFilePath;

                if (File.Exists(ZipFile))
                    File.Delete(ZipFile);

                using (ZipFile zip = new ZipFile())
                {
                    zip.AddDirectory(FileToZip);
                    zip.Save(ZipFile);
                }
            }
            //------------------------------------------
            if (File.Exists(FilePath) && File.Exists(SORFilePath))
            {
                lblMsgSendMail.Text = "Mail created successfully. Please send mail now.";
                dv_SendMail.Visible = true;
            }
            else
            {
                lblMsg_Mail.Text = "Docket and SOR attachments are not ready.";
            }
            //------------------------------------------
        }
        catch (Exception ex)
        {
            lblMsg_Mail.Text = "Unable to create mail. Error : " + ex.Message;
        }
    }
    protected void btnCloseSendMail_Click(object sender, EventArgs e)
    {
        dv_SendMail.Visible = false;
    }
    protected void btnCreatePO_Click(object sender, EventArgs e)
    {
        int RFQId = 0; Common.CastAsInt32(Request.Form["rfqlist"]);
        foreach (RepeaterItem item in rptRFQ.Items)
        {
            RadioButton rdoSelect = (RadioButton)item.FindControl("rdoSelect");

            if (rdoSelect.Checked)
            {
                RFQId = Common.CastAsInt32(rdoSelect.Attributes["RFQId"]);
                break;
            }
        }

        if (RFQId > 0)
        {
            Common.Set_Procedures("[dbo].[DD_CreatePO]");
            Common.Set_ParameterLength(1);
            Common.Set_Parameters(new MyParameter("@RFQId", RFQId));
            DataSet ds = new DataSet();
            ds.Clear();
            Boolean res;
            res = Common.Execute_Procedures_IUD(ds);
            if (res)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "111", "alert('PO Created successfully.');", true);
                btnCreatePO.Visible = false;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "111", "alert('Unable to create PO.');", true);
            }

        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "111", "alert('Please select RFQ#.');", true);
        }
    }
    protected void PublishReportToFile(string destfile)
    {
        DataTable dtReport = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.VW_DD_DOCKET_PRINT WHERE ( COSTCATEGORY='Shipyard Supply Costs' OR COSTCATEGORY='') AND DOCKETID=" + DocketId);
        DataTable dtcat = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.DD_JobCategory order by catcode");
        rpt.Load(Server.MapPath("~/Modules/PMS/DryDock/PrintDocket.rpt"));
        rpt.SetDataSource(dtReport);
        rpt.Subreports[0].SetDataSource(dtcat);
        if (File.Exists(destfile))
        {
            File.Delete(destfile);
        }
        rpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, destfile);
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }

    // -------------- Password Management ------------------------

    protected void btnCreatePassword_Click(object sender, EventArgs e)
    {
        RFQId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);

        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DD_Docket_RFQ_Master WHERE RFQId=" + RFQId);
        if (dt != null && dt.Rows.Count > 0)
        {
            Password.Text = dt.Rows[0]["Password"].ToString();
            txtExpiryDate.Text = Common.ToDateString(dt.Rows[0]["PasswordExpiryDate"]);
        }

        dv_PasswordUpdation.Visible = true;

    }

    protected void btnSavePassword_Click(object sender, EventArgs e)
    {
        if (Password.Text.Trim() == "")
        {
            Password.Focus();
            lblMsg_Pwd.Text = "Please enter password.";
            return;
        }

        string reg = @"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$";

        if (!Regex.IsMatch(Password.Text.Trim(), reg))
        {
            Password.Focus();
            lblMsg_Pwd.Text = "Password must be of minimum 8 characters and alphanumeric.";
            return;
        }


        if (txtExpiryDate.Text.Trim() == "")
        {
            txtExpiryDate.Focus();
            lblMsg_Pwd.Text = "Please enter expiry date.";
            return;
        }

        DateTime dt;

        if (!DateTime.TryParse(txtExpiryDate.Text.Trim(), out dt))
        {
            txtExpiryDate.Focus();
            lblMsg_Pwd.Text = "Please enter valid date.";
            return;
        }

        if (Convert.ToDateTime(txtExpiryDate.Text.Trim()) < DateTime.Today.Date)
        {
            txtExpiryDate.Focus();
            lblMsg_Pwd.Text = "Expiry date can't be less than today.";
            return;
        }

        try
        {
            string SQL = "UPDATE DD_Docket_RFQ_Master SET  Password= '" + Password.Text.Trim() + "', PasswordExpiryDate = '" + txtExpiryDate.Text.Trim() + "'  WHERE RFQId=" + RFQId;
            Common.Execute_Procedures_Select_ByQuery(SQL);
            lblMsg_Pwd.Text = "Record saved successfully.";
        }
        catch (Exception ex)
        {
            lblMsg_Pwd.Text = "Unable to save record. Error : " + ex.Message;
        }

    }

    protected void btnClosePassword_Click(object sender, EventArgs e)
    {
        RFQId = 0;
        txtExpiryDate.Text = "";
        Password.Text = "";
        dv_PasswordUpdation.Visible = false;
    }


    // -----------------------------------------------------------

}
