using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using Ionic.Zip;
using PdfSharp.Drawing;
using PdfSharp.Charting;
using PdfSharp;
using PdfSharp.Pdf;
using System.Drawing;

using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Text;

public partial class eReports_S115_eReport_S115 : System.Web.UI.Page
{
    public string BreakDownNo
    {       
        get{ return ViewState["BreakDownNo"].ToString(); }
        set {ViewState["BreakDownNo"] =value ;}
    }
    public int Edit_CAID
    {
        get { return Common.CastAsInt32(ViewState["_Edit_CAID"]); }
        set { ViewState["_Edit_CAID"] = value; }
    }
    public int Stage
    {
        get { return Common.CastAsInt32(ViewState["_Stage"]); }
        set { ViewState["_Stage"] = value; }
    }
    public string UserName
    {
        get { return ViewState["UserName"].ToString(); }
        set { ViewState["UserName"] = value; }
    }
    public string VesselCode
    {
        get { return ViewState["VesselCode"].ToString(); }
        set { ViewState["VesselCode"] = value; }
    }
    public bool RcaApprovalStatus
    {
        get { return Convert.ToBoolean( ViewState["_RcaApprovalStatus"]); }
        set { ViewState["_RcaApprovalStatus"] = value; }
    }
    public string RcaStatus
    {
        get { return ViewState["_RcaStatus"].ToString(); }
        set { ViewState["_RcaStatus"] = value; }
    }


    // PDF
    int pagemargin = 25;
    int lineheight = 20;
    double pagewidth;
    int startx, starty;
    int maxy;
    int path = 0;

    XFont frca = new XFont("Verdana", 15, XFontStyle.Bold);
    XFont fheading = new XFont("Verdana", 5);
    XFont fheading1 = new XFont("Verdana", 6, XFontStyle.Italic);
    XBrush people = new XSolidBrush(XColor.FromArgb(249, 229, 193));
    XBrush process = new XSolidBrush(XColor.FromArgb(230, 246, 255));
    XBrush equipment = new XSolidBrush(XColor.FromArgb(253, 227, 227));
    XBrush external = new XSolidBrush(XColor.FromArgb(185, 245, 244));

    XBrush maintext = new XSolidBrush(XColor.FromArgb(43, 157, 224));
    XFont fheadingmain = new XFont("Verdana", 8);

    //-- ItextSharp ------------------------------------------------------------------------------------------------------------
    static BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);


    static iTextSharp.text.Font PageHeading = FontFactory.GetFont("Verdana", 14, iTextSharp.text.Font.BOLD, new iTextSharp.text.Color(57, 60, 61));
    static iTextSharp.text.Font PageSubHeading = FontFactory.GetFont("Verdana", 11, iTextSharp.text.Font.BOLD, new iTextSharp.text.Color(57, 60, 61));

    static iTextSharp.text.Font flabel = FontFactory.GetFont("Verdana", 10, iTextSharp.text.Font.BOLD, new iTextSharp.text.Color(57, 60, 61));
    static iTextSharp.text.Font fvalue = FontFactory.GetFont("Verdana", 10, iTextSharp.text.Font.NORMAL, new iTextSharp.text.Color(57, 60, 61));

    static iTextSharp.text.Font flabel_small = FontFactory.GetFont("Verdana", 8, iTextSharp.text.Font.BOLD, new iTextSharp.text.Color(57, 60, 61));
    static iTextSharp.text.Font fvalue_small = FontFactory.GetFont("Verdana", 8, iTextSharp.text.Font.NORMAL, new iTextSharp.text.Color(57, 60, 61));

    public DataTable DtData
    {
        get
        {
            if (ViewState["_data"] != null)
            {
                return (DataTable)ViewState["_data"];
            }
            else
            {
                string SQL = " select *,(select count(caid) from dbo.VSL_BreakDownRemarks_Analysis_CorrectiveActions c where c.analysisid=a.analysisid) as noca,(select count(commentid) from dbo.VSL_BreakDownRemarks_Analysis_Comments c where c.analysisid=a.analysisid) as noc from dbo.VSL_BreakDownRemarks_RCA_Analysis a where BreakDownNo='" + BreakDownNo + "' and VesselCode='" + VesselCode + "' ";
                DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
                ViewState["_data"] = dt;
                return dt;
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        ProjectCommon.SessionCheck();
        lblMsgAddMembers.Text = "";
        lblMsg.Text = "";
        lblMsgCorrectiveAction.Text = "";
        lblMsgRcaClosure.Text = "";
        //------------------------------------
        if (!Page.IsPostBack)
        {
            BreakDownNo = ""+Page.Request.QueryString["BreakDownNo"];
            VesselCode = Convert.ToString(Page.Request.QueryString["VesselCode"]);

            RcaStatus = "O";
            ShowMasterData();
            BindTeammembers();

            if (RcaStatus == "C")
            {
                btnSaveRcaAssigne.Visible = false;
                btnApproveRCA.Visible = false;
                lnkAddTeamMember.Visible = false;
            }
            else
            {
                btnSaveRcaAssigne.Visible = true;
                btnApproveRCA.Visible = true;
                lnkAddTeamMember.Visible = true;
            }
            //if (Session["loginid"].ToString() == "1")
            //    btnSendTobackStage.Visible = true;
        }
        //------------------------------------
    }
    protected void btnSaveRcaAssigne_OnClick(object sender, EventArgs e)
    {
        try
        {
            if (rdoListSeverity.SelectedIndex < 0)
            {
                lblMsg.Text = "Please select severity";
                return;
            }
            if (txtFocalPoint.Text.Trim()=="")
            {
                lblMsg.Text = "Please enter Short Description";
                return;
            }
            if (rptTeammembers.Items.Count==0)
            {
                lblMsg.Text = "Please select team member";
                return;
            }
            if (hfdTeamLeadValue.Value=="")
            {
                lblMsg.Text = "Please select team leader";
                return;
            }
            if (txtTargetClosureDate.Text.Trim() == "")
            {
                lblMsg.Text = "Please enter target closure date";
                return;
            }

            Common.Set_Procedures("[DBO].[sp_InsertBreakDownRemarks]");
            Common.Set_ParameterLength(8);
            Common.Set_Parameters(
                new MyParameter("@VesselCode", VesselCode),
                new MyParameter("@BreakDownNo", BreakDownNo),
                new MyParameter("@Classification", rdoListSeverity.SelectedValue),
                new MyParameter("@FocalPoint", txtFocalPoint.Text.Trim()),
                new MyParameter("@TargetClosureDate", txtTargetClosureDate.Text.Trim()),
                new MyParameter("@Stage", Stage),
                new MyParameter("@TeamLeader", hfdTeamLeadValue.Value),
                new MyParameter("@InitiatedBy", Session["UserName"].ToString())
                );
            DataSet ds = new DataSet();
            ds.Clear();
            Boolean res;
            res = Common.Execute_Procedures_IUD(ds);
            if (res)
            {
                lblMsg.Text = "Record saved successfully.";
                ShowMasterData();
                BindTeammembers();
            }
            else
            {
                lblMsg.Text = "Unable to save record. " + Common.getLastError();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Unable to save record." + ex.Message;
        }
    }

    protected void btnOPENPDF_OnClick(object sender, EventArgs e)
    { 
	    string ReportNo="";
	    DataTable dt1 = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM [DBO].[VSL_BreakDownRemarks] WHERE VESSELCODE='" + VesselCode + "' AND [BreakDownNo] = '" + BreakDownNo + "'");
        if (dt1.Rows.Count > 0)
        {
            ReportNo = dt1.Rows[0]["ReportNo"].ToString();
            string filename = "RCA_" + ReportNo.Replace("/", "-") + ".pdf";
            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Type", "application/pdf");
            Response.AddHeader("Content-Disposition", "inline;filename=" + filename);
            Response.BinaryWrite((byte[])dt1.Rows[0]["RcaDocument"]);
            Response.Flush();
            Response.End();
        }
    }
    protected void btnDeleterTeamMember_OnClick(object sender, EventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        string SQL = "  Delete  from dbo.VSL_BreakDownRemarks_Team where BreakDownNo='" + BreakDownNo + "' and VesselCode='"+VesselCode+"' and EmpID="+ btn .CommandArgument+ "";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
        BindTeammembers();
    }
    protected void rdoAssignRCA_OnCheckedChanged(object sender, EventArgs e)
    {
        ShowStageByClicking(1);
    }
    protected void rdoFinalRca_OnCheckedChanged(object sender, EventArgs e)
    {
        ShowStageByClicking(2);
    }
    protected void rdoRcaApproval_OnCheckedChanged(object sender, EventArgs e)
    {
        BindRcaApproval();
        ShowStageByClicking(3);

    }
    protected void rdoRcaClosure_OnCheckedChanged(object sender, EventArgs e)
    {
        ShowStageByClicking(4);
    }

    protected void btnApproveRCA_OnClick(object sender, EventArgs e)
    {
        string SQL = " update dbo.VSL_BreakDownRemarks set stage=4 ,ApprovedBy='dfasdf',ApprovedOn=getdate() where BreakDownNo='" + BreakDownNo + "' and VesselCode='" + VesselCode + "' ";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
        ShowMasterData();
        BindTeammembers();
        lblMsg.Text = "Record approved successfully.";
    }
    protected void btnNotifytoOffice_OnClick(object sender, EventArgs e)
    {
        string FromMail = "emanager@energiossolutions.com", toAddress = "emanager@energiossolutions.com", Attachment = "", CCAddresses = "", ReportNo = "";

        string sql = " select RcaDocument from dbo.VSL_BreakDownRemarks where BreakDownNo='" + BreakDownNo + "' and VesselCode='" + VesselCode + "' ";
        DataTable dt3 = Common.Execute_Procedures_Select_ByQuery(sql);
        if (dt3.Rows.Count > 0)
        {
            ReportNo = dt3.Rows[0]["ReportNo"].ToString();
        }
        Attachment = String.Format(Server.MapPath("~/TEMP/RCA_" + ReportNo.Replace("/", "-") + ".pdf"));
        if (dt3.Rows.Count == 0 || dt3.Rows[0][0].ToString() == "")
        {
            lblMsgRcaClosure.Text = "PDF file not found.Please generate pdf first.";
            return;
        }

        if (File.Exists(Attachment))
            File.Delete(Attachment);
        byte[] rcaFile = (byte[])dt3.Rows[0][0];
        File.WriteAllBytes(Attachment, rcaFile);


        if (!File.Exists(Attachment))
        {
            lblMsgRcaClosure.Text = "PDF file not found.Please generate pdf first.";
            return;
        }
        
        //string text= SendMail.SendRcaMails_Office(FromMail,toAddress,CCAddresses, ReportNo,Attachment,true);
        //lblMsgRcaClosure.Text = text;


    }
    protected void btnNotifytoVessel_OnClick(object sender, EventArgs e)
    {
        string ReportNoSerial = "";
        string ReportNo = "";
        DataTable dt1 = Common.Execute_Procedures_Select_ByQuery("SELECT a.Classification,a.BreakDownNo,a.VesselCode,a.FocalPoint,a.TargetClosureDate,a.Stage,a.TeamLeader,a.FwdByApproval,a.FwdByApprovalOn,a.ApprovedBy,a.ApprovedOn,a.RcaDocument FROM [DBO].[VSL_BreakDownRemarks] a WHERE a.VESSELCODE='" + VesselCode + "' AND a.BreakDownNo='" + BreakDownNo + "'");
        if (dt1.Rows.Count > 0)
        {
            ReportNo = dt1.Rows[0]["BreakDownNo"].ToString();
            ReportNoSerial = dt1.Rows[0]["BreakDownNo"].ToString().Replace("/", "-");
            int AccidentSeverity = Common.CastAsInt32(dt1.Rows[0]["Classification"]);

            DataTable VSL_BreakDownRemarks = Common.Execute_Procedures_Select_ByQuery("select *," + AccidentSeverity + " as AccidentSeverity,isnull(datalength(rcadocument),0) as filesize from [dbo].[VSL_BreakDownRemarks] WHERE VesselCode='" + VesselCode + "' AND BreakDownNo='" + BreakDownNo + "'");
            if (VSL_BreakDownRemarks.Rows.Count <= 0)
            {
                ProjectCommon.ShowMessage("Nothing to export. Please save record first.");
                return;
            }
            else if (Common.CastAsInt32(VSL_BreakDownRemarks.Rows[0]["filesize"]) <= 0)
            {
                ProjectCommon.ShowMessage("Please create pdf file to export.");
                return;
            }

            VSL_BreakDownRemarks.TableName = "VSL_BreakDownRemarks";

            DataSet ds = new DataSet();
            ds.Tables.Add(VSL_BreakDownRemarks.Copy());

            string SchemaFile = Server.MapPath("BD_RCA_SCHEMA.xml");
            string DataFile = Server.MapPath("BD_RCA_DATA.xml");

            ds.WriteXml(DataFile);
            ds.WriteXmlSchema(SchemaFile);

            string ZipData = Server.MapPath("BD_RCA_" + ReportNoSerial + ".zip");
            if (File.Exists(ZipData)) { File.Delete(ZipData); }


            string FromAdd = "emanager@energiossolutions.com";

            DataTable dtvem = Common.Execute_Procedures_Select_ByQuery("select email from dbo.vessel where vesselcode='" + VesselCode + "'");
            string vesselemail = "";
            if (dtvem.Rows.Count > 0)
                vesselemail = dtvem.Rows[0][0].ToString();

            string ToAdd = vesselemail;// "emanager@energiossolutions.com";
            
            string CCAdd = "emanager@energiossolutions.com";

            ToAdd = "emanager@energiossolutions.com";
            CCAdd = "pankaj.k@esoftech.com";
            using (ZipFile zip = new ZipFile())
            {
                zip.AddFile(SchemaFile);
                zip.AddFile(DataFile);
                zip.Save(ZipData);

                List<string> ToMails = new List<string>();
                List<string> CCMails = new List<string>();
                List<string> BCCMails = new List<string>();

                ToMails.Add(ToAdd);
                CCMails.Add(CCAdd);
                
                string Subject = "RCA - [ " + ReportNo + " ] ";

                StringBuilder sb = new StringBuilder();
                sb.Append("Dear Captain,<br /><br />");
                sb.Append("Attached please import the Breakdown Report RCA.<br /><br />");
                sb.Append("<br /><br /><br />");
                sb.Append("Thank You,");

                //string result = SendMail.SendSimpleMail("emanager@energiossolutions.com", ToAdd, CCMails.ToArray(), BCCMails.ToArray(), Subject, sb.ToString(), ZipData);
                string Error = "";
                bool result = EProjectCommon.SendeMail_MTM(0, "emanager@energiossolutions.com", "emanager@energiossolutions.com", ToMails.ToArray(), CCMails.ToArray(), BCCMails.ToArray(), Subject, sb.ToString(),out Error, ZipData);
                if (result)
                {
                    Common.Execute_Procedures_Select_ByQuery("UPDATE [DBO].[VSL_BreakDownRemarks] SET ExportedBy='" + Session["UserName"].ToString() + "',RCANo='" + Path.GetFileName(ZipData).Replace(".zip",".pdf") + "', ExportedOn = getdate(),iS_Closed='Y',Is_Closure_Notified='Y' WHERE VesselCode='" + VesselCode + "' AND BreakDownNo='" + BreakDownNo + "'");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "alert('Exported successfully.');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "abort", "alert('Unable to export. Error : " + result + "');", true);
                }
            }
        }
    }
    protected void btnSendTobackStage_OnClick(object sender, EventArgs e)
    {
        //if (Stage == 1)
        //{
        //    Page.ClientScript.RegisterStartupScript(this.GetType(), "Testing", "alert('Current Stage is 1. You can not go back.')", true);
        //    return;
        //}
        //try
        //{
        //    string sql = " EXEC dbo.ER_S115_OFFICE_REVERT_STAGE '" + VesselCode + "'," + ReportId;
        //    DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
        //    ShowMasterData();
        //    BindTeammembers();
        //    Page.ClientScript.RegisterStartupScript(this.GetType(), "Testing", "alert('Stage is moved back successfully. Current stage is " + Stage + "')", true);
        //}
        //catch
        //{
        //    Page.ClientScript.RegisterStartupScript(this.GetType(), "Testing", "alert('Error while updating the record')", true);
        //}
    }
    
    //PDF --------------------------------------------------------------
    protected void btnCreatePDF_OnClick(object sender, EventArgs e)
    {
        CreatePdf_01();
        CreatePdf_02();

        PdfSharp.Pdf.PdfDocument s_document = new PdfSharp.Pdf.PdfDocument();
        s_document.Info.Title = "MTM RCA Document";
        s_document.Info.Author = "Ajay Singh";
        s_document.Info.Subject = "RCA Document";
        s_document.Info.Keywords = "Breakdown, RCA";

        // Create demonstration pages
        PdfSharp.Pdf.PdfPage p = s_document.AddPage();
        //p.Orientation = PageOrientation.Landscape;
        p.Size = PdfSharp.PageSize.A4;
        //----------------------------
        pagewidth = p.Width - (2 * pagemargin);
        startx = pagemargin;
        starty = pagemargin;
        //-----------------------------
        string sql = " select * from dbo.VSL_BreakDownRemarks Where BreakDownNo='" + BreakDownNo + "' and VesselCode='" + VesselCode + "' ";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
        string FocalPoint = ""; string ReportNo = ""; string IncidentDate = ""; string VesselName = "";
        if (dt.Rows.Count > 0)
        {
            FocalPoint = dt.Rows[0]["FocalPoint"].ToString();
        }

        DataTable dt1 = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM [DBO].[VSL_BreakDownMaster] WHERE VESSELCODE='" + VesselCode + "' AND BreakDownNo='" + BreakDownNo + "'");
        if (dt1.Rows.Count > 0)
        {
            ReportNo = dt1.Rows[0]["BreakDownNo"].ToString();
            IncidentDate = Convert.ToDateTime(dt1.Rows[0]["ReportDt"]).ToString("dd-MMM-yyyy HH:mm");
        }

        string filename = String.Format(Server.MapPath("~/TEMP/RCA_"+ ReportNo.Replace("/","-") + ".pdf"));
        if (File.Exists(filename))
            File.Delete(filename);

        sql = "SELECT VESSELName FROM [DBO].Vessel WHERE VESSELCode ='" + VesselCode + "' ";
        DataTable dt2 = Common.Execute_Procedures_Select_ByQuery(sql);
        if(dt2.Rows.Count>0)
        {
            VesselName = dt2.Rows[0]["VEsselName"].ToString();
        }

        XGraphics gfx = XGraphics.FromPdfPage(p);

        gfx.DrawString("ROOT CAUSE ANALYSIS", frca, XBrushes.Red, new PdfSharp.Drawing.XRect(startx, starty, pagewidth, 10), XStringFormat.Center);
        starty += lineheight;
        
        gfx.DrawString("Vessel : " + ""+ VesselName + " , Incident No : "+ ReportNo + " , Incident Date : "+ IncidentDate + " (hrs)", fheading, XBrushes.Black, new PdfSharp.Drawing.XRect(startx, starty, pagewidth, 10), XStringFormat.Center);
        
        Pen pen = new Pen(System.Drawing.Color.Black, 0.5f);

        starty += lineheight;
        gfx.DrawRectangle(maintext, new PdfSharp.Drawing.XRect(pagemargin + 10, starty, 300, 15));      
        
        
        gfx.DrawString("Short Description - "+ FocalPoint, fheadingmain, XBrushes.White, new PdfSharp.Drawing.XRect(pagemargin + 15, starty + 3, 150, 10), XStringFormat.TopLeft);

        starty += 1;
        maxy = starty;
	    string rootname="";
        DrawNode(DtData, gfx, pen, startx, starty, 0,rootname);
        string TempFilename = Server.MapPath("File_03.pdf");
        s_document.Save(TempFilename);

        MergePDFFile();

        string MergedFile = Server.MapPath("File_04.pdf");

        byte[] savedFile = File.ReadAllBytes(MergedFile);
        //sql = "Update dbo.VSL_BreakDownRemarks set RcaDocument='"+ savedFile + "' where ReportID="+ReportId+" and VesselCode='"+VesselCode+"' ";
        //sql = " exec dbo.ER_S115_Update_ReportRCA_File "+ ReportId + ",'"+ VesselCode + "','"+ savedFile + "'";

        //------------------------------------------------------------------------------------
        Common.Set_Procedures("[DBO].VSL_BreakDownRemarks_RCA_File");
        Common.Set_ParameterLength(4);
        Common.Set_Parameters(
            new MyParameter("@BreakDownNo", BreakDownNo),
            new MyParameter("@VesselCode", VesselCode),
            new MyParameter("@File", savedFile),
            new MyParameter("@PdfCreatedBy", Session["UserName"].ToString())            
            );
        DataSet dsComponents = new DataSet();        
        Boolean res;
        res = Common.Execute_Procedures_IUD(dsComponents);
        if (res)
        {
            lblMsgRcaClosure.Text = "PDF created successfully.";
            lnkDownloadPDF.Text = "RCA_" + ReportNo.Replace(" / ", " - ") + ".pdf";
        }
        //------------------------------------------------------------------------------------
        //DataTable dt3 = Common.Execute_Procedures_Select_ByQuery(sql);
        
    }
    protected void lnkDownloadPDF_OnClick(object sender, EventArgs e)
    {
        string sql = " select rcano,RcaDocument from dbo.VSL_BreakDownRemarks where BreakDownNo='" + BreakDownNo + "' and VesselCode='" + VesselCode + "' ";
        DataTable dtfile = Common.Execute_Procedures_Select_ByQuery(sql);
        if (dtfile.Rows.Count > 0)
        {
            string filename = dtfile.Rows[0]["rcano"].ToString();
            byte[] fileBytes = (byte[])dtfile.Rows[0][1];
            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Type", "application/pdf");
            Response.AddHeader("Content-Disposition", "attachment;filename=" + filename);
            Response.BinaryWrite(fileBytes);
            Response.Flush();
            Response.End();
        }
    }
    
    public void DrawNode(DataTable dt, XGraphics gfx, Pen pen, int startx, int starty, int startparent,string rootname)
    {
        startx += 30;
        //starty += 20;
        DataRow[] drs = dt.Select("ParentAnalysisID=" + startparent);
        foreach (DataRow dr in drs)
        {
            maxy += 20;
	    if(startparent==0)
		rootname=dr["cause"].ToString().ToLower();

	    if(rootname=="people")	
	    gfx.DrawRectangle(people, new PdfSharp.Drawing.XRect(startx, maxy, 150, 14));
	    if(rootname=="process")	
	    gfx.DrawRectangle(process, new PdfSharp.Drawing.XRect(startx, maxy, 150, 14));
	    if(rootname=="equipment")	
	    gfx.DrawRectangle(equipment, new PdfSharp.Drawing.XRect(startx, maxy, 150, 14));
	    if(rootname=="external")	
	    gfx.DrawRectangle(external, new PdfSharp.Drawing.XRect(startx, maxy, 150, 14));

            gfx.DrawString(dr["cause"].ToString(), fheading1, XBrushes.Black, new PdfSharp.Drawing.XRect(startx + 2, maxy, 150, 50), XStringFormat.TopLeft);
            gfx.DrawString(((dr["HasChilds"].ToString() == "True") ? "WHY ? " : ((dr["Status"].ToString() == "R") ? "Root Cause" : "Cause")), fheading, XBrushes.Blue, new PdfSharp.Drawing.XRect(startx + 2, maxy + 7, 150, 10), XStringFormat.TopLeft);
            gfx.DrawLine(pen, startx, maxy + 7, startx - 10, maxy + 7);
            gfx.DrawLine(pen, startx - 10, maxy + 7, startx - 10, maxy - ( maxy - starty)+15);
            path++;

            DrawNode(dt, gfx, pen, startx,maxy, Convert.ToInt32(dr["analysisid"]),rootname);
        }
    }

    public void DrawPage(PdfSharp.Pdf.PdfPage page)
    {
        XGraphics gfx = XGraphics.FromPdfPage(page);
        DrawLine(gfx, 1);
    }
    void DrawLine(XGraphics gfx, int number)
    {

        gfx.DrawLine(XPens.DarkGreen, 0, 0, 250, 0);

        gfx.DrawLine(XPens.Gold, 15, 7, 230, 15);

        XPen pen = new XPen(XColors.Navy, 4);
        gfx.DrawLine(pen, 0, 20, 250, 20);

        pen = new XPen(XColors.Firebrick, 6);
        pen.DashStyle = XDashStyle.Dash;
        gfx.DrawLine(pen, 0, 40, 250, 40);
        pen.Width = 7.3;
        pen.DashStyle = XDashStyle.DashDotDot;
        gfx.DrawLine(pen, 0, 60, 250, 60);

        pen = new XPen(XColors.Goldenrod, 10);
        pen.LineCap = XLineCap.Flat;
        gfx.DrawLine(pen, 10, 90, 240, 90);
        gfx.DrawLine(XPens.Black, 10, 90, 240, 90);

        pen = new XPen(XColors.Goldenrod, 10);
        pen.LineCap = XLineCap.Square;
        gfx.DrawLine(pen, 10, 110, 240, 110);
        gfx.DrawLine(XPens.Black, 10, 110, 240, 110);

        pen = new XPen(XColors.Goldenrod, 10);
        pen.LineCap = XLineCap.Round;
        gfx.DrawLine(pen, 10, 130, 240, 130);
        gfx.DrawLine(XPens.Black, 10, 130, 240, 130);


    }
    void DrawRoundedRectangle(XGraphics gfx, int number)
    {

        XPen pen = new XPen(XColors.RoyalBlue, Math.PI);

        gfx.DrawRoundedRectangle(pen, 10, 0, 100, 60, 30, 20);
        gfx.DrawRoundedRectangle(XBrushes.Orange, 130, 0, 100, 60, 30, 20);
        gfx.DrawRoundedRectangle(pen, XBrushes.Orange, 10, 80, 100, 60, 30, 20);
        gfx.DrawRoundedRectangle(pen, XBrushes.Orange, 150, 80, 60, 60, 20, 20);

    }

    public void CreatePdf_01()
    {
        string[] mg = { "People", "Process", "Equipment", "External" };
        string path = Server.MapPath("File_01.pdf");
        FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None);

        DataTable dtMasterData = GetReportData();
        DataRow DrMasterData;
        if (dtMasterData.Rows.Count > 0)
            DrMasterData = dtMasterData.Rows[0];
        else
            DrMasterData = dtMasterData.NewRow();

        DataTable dtRcaTeamData = GetRcaTeamData();
       
        #region Page 1
        Document doc = new Document(iTextSharp.text.PageSize.A4, 3, 3, 2, 3);
        doc.SetMargins(20f, 15f, 15f, 15f);

        doc.AddTitle("PDFsharp XGraphic Sample");
        doc.AddAuthor("Stefan Lange");
        doc.AddSubject("Created with code snippets that show the use of graphical functions");
        doc.AddKeywords("PDFsharp, XGraphics");

        PdfWriter writer = PdfWriter.GetInstance(doc, fs);
        doc.Open();

        Paragraph pHeader = new Paragraph("Breakdown Report", PageHeading);
        pHeader.Alignment = Element.ALIGN_CENTER;
        doc.Add(pHeader);

        Paragraph pVesselName = new Paragraph("" + DrMasterData["VesselName"].ToString() + " [ " + DrMasterData["ReportNo"].ToString() + " ]", PageSubHeading);
        pVesselName.Alignment = Element.ALIGN_CENTER;
        pVesselName.SpacingAfter = 25;
        doc.Add(pVesselName);

        PdfPCell pdfCell = new PdfPCell(new Phrase("", flabel));
        pdfCell.Border = 0;

       

        PdfPTable table = new PdfPTable(2);
        float[] widths = new float[] { 1f, 3f };
        table.SetWidths(widths);
        table.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
        table.DefaultCell.Border = 0;
        table.SpacingAfter = 10;
        //table.WidthPercentage = 100;

        pdfCell = new PdfPCell(new Phrase("Short Description", flabel));
        //pdfCell.Border = 0;
        table.AddCell(pdfCell);

        pdfCell = new PdfPCell(new Phrase(DrMasterData["FocalPoint"].ToString(), fvalue));
        //pdfCell.Border = 0;
        table.AddCell(pdfCell);

        pdfCell = new PdfPCell(new Phrase("Severity", flabel));
        //pdfCell.Border = 0;
        table.AddCell(pdfCell);

        pdfCell = new PdfPCell(new Phrase(DrMasterData["AccidentSeverity"].ToString(), fvalue));
        //pdfCell.Border = 0;
        table.AddCell(pdfCell);

        pdfCell = new PdfPCell(new Phrase("Event Description", flabel));
        //pdfCell.Border = 0;
        table.AddCell(pdfCell);

        pdfCell = new PdfPCell(new Phrase(DrMasterData["EventDescription"].ToString(), fvalue));
        //pdfCell.Border = 0;
        table.AddCell(pdfCell);

        doc.Add(table);



        //pdfCell = new PdfPCell(new Phrase("RCA Team", flabel));
        ////pdfCell.Border = 0;
        //table.AddCell(pdfCell);

        //table.AddCell(new Phrase(""));
        //doc.Add(table);

        Paragraph headingRcaTeam= new Paragraph("RCA Team", PageHeading);
        headingRcaTeam.Alignment = Element.ALIGN_LEFT;
        headingRcaTeam.SpacingAfter = 5;
        //doc.Add(headingRcaTeam);

        PdfPTable tableRcaTeam = new PdfPTable(2);
        float[] widthsRcaTeam = new float[] { 1f, 1f };
        tableRcaTeam.SetWidths(widthsRcaTeam);
        tableRcaTeam.SpacingAfter = 3;


        pdfCell = new PdfPCell(headingRcaTeam);
        pdfCell.Border = 0;
        tableRcaTeam.AddCell(pdfCell);

        pdfCell = new PdfPCell(new Phrase("", flabel));
        pdfCell.Border = 0;
        tableRcaTeam.AddCell(pdfCell);


        pdfCell = new PdfPCell(new Phrase("Name", flabel));
        pdfCell.Border = 0;
        tableRcaTeam.AddCell(pdfCell);

        pdfCell = new PdfPCell(new Phrase("Position", flabel));
        pdfCell.Border = 0;
        tableRcaTeam.AddCell(pdfCell);
        foreach (DataRow Dr in dtRcaTeamData.Rows)
        {
            pdfCell = new PdfPCell(new Phrase(Dr["EmpName"].ToString() + ((Common.CastAsInt32(Dr["EmpID"]) == Common.CastAsInt32(hfdTeamLeadValue.Value) ? " [ Team Leader ]" : "")), fvalue));
            pdfCell.Border = 0;
            tableRcaTeam.AddCell(pdfCell);

            pdfCell = new PdfPCell(new Phrase(Dr["Positionname"].ToString(), fvalue));
            pdfCell.Border = 0;
            tableRcaTeam.AddCell(pdfCell);
        }

        doc.Add(tableRcaTeam);




        //pdfCell = new PdfPCell(new Phrase("Target Closure Date", flabel));
        //pdfCell.Border = 0;
        //table.AddCell(pdfCell);

        //pdfCell = new PdfPCell(new Phrase(Common.ToDateString(DrMasterData["TargetClosureDate"]), fvalue));
        //pdfCell.Border = 0;
        //table.AddCell(pdfCell);

        #endregion
        #region Page2
        //-- New Page Started------------------------------------------------------------------------------
        doc.NewPage();

        Paragraph headingPage2 = new Paragraph("Comments", PageHeading);
        headingPage2.Alignment = Element.ALIGN_CENTER;
        doc.Add(headingPage2);

        for (int c = 0; c <= 3; c++)
        {

            Paragraph headingmg = new Paragraph(mg[c], PageHeading);
            headingmg.Alignment = Element.ALIGN_LEFT;
            headingmg.SpacingAfter = 5;
            doc.Add(headingmg);

            DataTable dtRcaCauseData = GetRcaCauseData(c + 1);
            if (dtRcaCauseData.Rows.Count > 0)
            {
                foreach (DataRow dr in dtRcaCauseData.Rows)
                {
                    if (dr[1].ToString().Trim().ToLower() != mg[c].ToLower())
                    {
                        Paragraph cname = new Paragraph(dr[1].ToString(), PageSubHeading);
                        cname.Alignment = Element.ALIGN_LEFT;
                        cname.SpacingAfter = 5;
                        doc.Add(cname);
                    }
                    DataTable dtRcaCommentsData = GetRcaCommentsData(c + 1, Common.CastAsInt32(dr["analysisid"]));
                    if (dtRcaCommentsData.Rows.Count > 0)
                    {
                        PdfPTable tblComments = new PdfPTable(2);
                        float[] widthsComments = new float[] { 1F, 9f };
                        tblComments.SetWidths(widthsComments);
                        tblComments.SpacingBefore = 10;
                        tblComments.WidthPercentage = 100;



                        pdfCell = new PdfPCell(new Phrase("Sr#", flabel));
                        pdfCell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        pdfCell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                        pdfCell.Padding = 4;
                        tblComments.AddCell(pdfCell);


                        pdfCell = new PdfPCell(new Phrase("Comments", flabel));
                        pdfCell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        pdfCell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                        pdfCell.Padding = 4;
                        tblComments.AddCell(pdfCell);

                        //pdfCell = new PdfPCell(new Phrase("Comments By", flabel));
                        //pdfCell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        //pdfCell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                        //pdfCell.Padding = 4;
                        //tblComments.AddCell(pdfCell);

                        //pdfCell = new PdfPCell(new Phrase("Comments On", flabel));
                        //pdfCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        //pdfCell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                        //pdfCell.Padding = 4;
                        //tblComments.AddCell(pdfCell);

                        foreach (DataRow Dr in dtRcaCommentsData.Rows)
                        {
                            pdfCell = new PdfPCell(new Phrase(Dr["RowNo"].ToString(), fvalue));
                            pdfCell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                            pdfCell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                            pdfCell.Padding = 4;
                            tblComments.AddCell(pdfCell);

                            pdfCell = new PdfPCell(new Phrase(Dr["Comments"].ToString(), fvalue));
                            pdfCell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                            pdfCell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                            pdfCell.Padding = 4;
                            tblComments.AddCell(pdfCell);


                            //pdfCell = new PdfPCell(new Phrase(Dr["CommentBy"].ToString(), fvalue));
                            //pdfCell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                            //pdfCell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                            //pdfCell.Padding = 4;
                            //tblComments.AddCell(pdfCell);


                            //pdfCell = new PdfPCell(new Phrase(Common.ToDateString(Dr["CommentDate"]), fvalue));
                            //pdfCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                            //pdfCell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                            //pdfCell.Padding = 4;
                            //tblComments.AddCell(pdfCell);

                        }

                        doc.Add(tblComments);
                    }
                }
            }
            else
            {
                Paragraph headingNA = new Paragraph("NA", flabel_small);
                headingNA.Alignment = Element.ALIGN_LEFT;
                headingNA.SpacingAfter = 5;
                doc.Add(headingNA);
            }
        }
        #endregion
        #region Page3
        //-- New Page Started------------------------------------------------------------------------------
        //doc.NewPage();

        //Paragraph headingPage3 = new Paragraph("Root Cause and Corrective Action", PageHeading);
        //headingPage3.Alignment = Element.ALIGN_CENTER;
        //headingPage3.SpacingAfter = 10;
        //doc.Add(headingPage3);

        ////string[] mg = { "People", "Process", "Equipment", "External" };

        //for (int c = 0; c <= 3; c++)
        //{

        //    Paragraph headingmg = new Paragraph(mg[c], PageHeading);
        //    headingmg.Alignment = Element.ALIGN_LEFT;
        //    headingmg.SpacingAfter = 5;
        //    doc.Add(headingmg);


        //    DataTable dtRcaApproval = GetRcaApproval(c+1);
        //    if (dtRcaApproval.Rows.Count > 0)
        //    {
        //        DataTable dtCorrectiveAction;

        //        Paragraph paraCause = new Paragraph();
        //        PdfPTable tblRcaApproval = new PdfPTable(3);
        //        tblRcaApproval.WidthPercentage = 100;
        //        int rno = 0;
        //        foreach (DataRow Dr in dtRcaApproval.Rows)
        //        {
        //            paraCause = new Paragraph(Dr["Rowno"].ToString() + " - " + Dr["Cause"].ToString(), flabel);
        //            doc.Add(paraCause);

        //            dtCorrectiveAction = GetCorrectiveAction(Common.CastAsInt32(Dr["AnalysisID"]));

        //            rno++;
        //            pdfCell = new PdfPCell(new Phrase("Corrective Action - " + rno, flabel_small));

        //            tblRcaApproval.AddCell(pdfCell);

        //            pdfCell = new PdfPCell(new Phrase("Responsibility", flabel_small));
        //            pdfCell.Padding = 4;
        //            tblRcaApproval.AddCell(pdfCell);

        //            pdfCell = new PdfPCell(new Phrase("Target Closure Date", flabel_small));
        //            pdfCell.Padding = 4;
        //            pdfCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        //            tblRcaApproval.AddCell(pdfCell);

        //            //pdfCell = new PdfPCell(new Phrase("Created By", flabel_small));
        //            //pdfCell.Padding = 4;
        //            //tblRcaApproval.AddCell(pdfCell);

        //            //pdfCell = new PdfPCell(new Phrase("Created On", flabel_small));
        //            //pdfCell.Padding = 4;
        //            //pdfCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        //            //tblRcaApproval.AddCell(pdfCell);

        //            //pdfCell = new PdfPCell(new Phrase("Modified By", flabel_small));
        //            //pdfCell.Padding = 4;
        //            //tblRcaApproval.AddCell(pdfCell);
        //            //pdfCell = new PdfPCell(new Phrase("Modified On", flabel_small));
        //            //pdfCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        //            ////pdfCell.Border = 0;
        //            //tblRcaApproval.AddCell(pdfCell);
        //            foreach (DataRow DrCorr in dtCorrectiveAction.Rows)
        //            {

        //                float[] widthsApproval = new float[] { 90f, 35F, 30f };
        //                tblRcaApproval.SetWidths(widthsApproval);
        //                tblRcaApproval.SpacingBefore = 10;

        //                pdfCell = new PdfPCell(new Phrase(DrCorr["CorrectiveAction"].ToString(), fvalue_small));
        //                pdfCell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        //                pdfCell.VerticalAlignment = PdfPCell.ALIGN_TOP;
        //                pdfCell.Padding = 4;
        //                tblRcaApproval.AddCell(pdfCell);

        //                pdfCell = new PdfPCell(new Phrase(DrCorr["Responsibility"].ToString(), fvalue_small));
        //                pdfCell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        //                pdfCell.VerticalAlignment = PdfPCell.ALIGN_TOP;
        //                pdfCell.Padding = 4;
        //                tblRcaApproval.AddCell(pdfCell);


        //                pdfCell = new PdfPCell(new Phrase(Common.ToDateString(DrCorr["TargetClosureDate"]), fvalue_small));
        //                pdfCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        //                pdfCell.VerticalAlignment = PdfPCell.ALIGN_TOP;
        //                pdfCell.Padding = 4;
        //                tblRcaApproval.AddCell(pdfCell);


        //                //pdfCell = new PdfPCell(new Phrase(DrCorr["createdby"].ToString(), fvalue_small));
        //                //pdfCell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        //                //pdfCell.VerticalAlignment = PdfPCell.ALIGN_TOP;
        //                //pdfCell.Padding = 4;
        //                //tblRcaApproval.AddCell(pdfCell);

        //                //pdfCell = new PdfPCell(new Phrase(Common.ToDateString(DrCorr["createdOn"]), fvalue_small));
        //                //pdfCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        //                //pdfCell.VerticalAlignment = PdfPCell.ALIGN_TOP;
        //                //pdfCell.Padding = 4;
        //                //tblRcaApproval.AddCell(pdfCell);

        //                //pdfCell = new PdfPCell(new Phrase(DrCorr["modifiedby"].ToString(), fvalue_small));
        //                //pdfCell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        //                //pdfCell.VerticalAlignment = PdfPCell.ALIGN_TOP;
        //                //pdfCell.Padding = 4;
        //                //tblRcaApproval.AddCell(pdfCell);

        //                //pdfCell = new PdfPCell(new Phrase(Common.ToDateString(DrCorr["modifiedOn"]), fvalue_small));
        //                //pdfCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        //                //pdfCell.VerticalAlignment = PdfPCell.ALIGN_TOP;
        //                //pdfCell.Padding = 4;
        //                //tblRcaApproval.AddCell(pdfCell);
        //            }
        //            doc.Add(tblRcaApproval);
        //        }
        //    }
        //    else
        //    {
        //        Paragraph headingNA = new Paragraph("NA", flabel_small);
        //        headingNA.Alignment = Element.ALIGN_LEFT;
        //        headingNA.SpacingAfter = 5;
        //        doc.Add(headingNA);
        //    }
        //}

        #endregion      


        doc.Close();
    }
    public void CreatePdf_02()
    {
        string path = Server.MapPath("File_02.pdf");
        FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None);

        DataTable dtMasterData = GetReportData();
        DataRow DrMasterData;
        if (dtMasterData.Rows.Count > 0)
            DrMasterData = dtMasterData.Rows[0];
        else
            DrMasterData = dtMasterData.NewRow();

        DataTable dtRcaTeamData = GetRcaTeamData();

        #region Page 1
        Document doc = new Document(iTextSharp.text.PageSize.A4, 3, 3, 2, 3);
        doc.SetMargins(20f, 15f, 15f, 15f);

        doc.AddTitle("PDFsharp XGraphic Sample");
        doc.AddAuthor("Stefan Lange");
        doc.AddSubject("Created with code snippets that show the use of graphical functions");
        doc.AddKeywords("PDFsharp, XGraphics");

        PdfWriter writer = PdfWriter.GetInstance(doc, fs);
        doc.Open();

        //Paragraph pHeader = new Paragraph("Incident Report", PageHeading);
        //pHeader.Alignment = Element.ALIGN_CENTER;
        //doc.Add(pHeader);

        //Paragraph pVesselName = new Paragraph("" + DrMasterData["VesselName"].ToString() + " [ " + DrMasterData["ReportNo"].ToString() + " ]", PageSubHeading);
        //pVesselName.Alignment = Element.ALIGN_CENTER;
        //pVesselName.SpacingAfter = 25;
        //doc.Add(pVesselName);

        PdfPCell pdfCell = new PdfPCell(new Phrase("", flabel));
        pdfCell.Border = 0;



        //PdfPTable table = new PdfPTable(2);
        //float[] widths = new float[] { 1f, 3f };
        //table.SetWidths(widths);
        //table.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
        //table.DefaultCell.Border = 0;
        //table.SpacingAfter = 10;
        ////table.WidthPercentage = 100;

        //pdfCell = new PdfPCell(new Phrase("Focal Point", flabel));
        ////pdfCell.Border = 0;
        //table.AddCell(pdfCell);

        //pdfCell = new PdfPCell(new Phrase(DrMasterData["FocalPoint"].ToString(), fvalue));
        ////pdfCell.Border = 0;
        //table.AddCell(pdfCell);

        //pdfCell = new PdfPCell(new Phrase("Severity", flabel));
        ////pdfCell.Border = 0;
        //table.AddCell(pdfCell);

        //pdfCell = new PdfPCell(new Phrase(DrMasterData["AccidentSeverity"].ToString(), fvalue));
        ////pdfCell.Border = 0;
        //table.AddCell(pdfCell);

        //pdfCell = new PdfPCell(new Phrase("Event Description", flabel));
        ////pdfCell.Border = 0;
        //table.AddCell(pdfCell);

        //pdfCell = new PdfPCell(new Phrase(DrMasterData["EventDescription"].ToString(), fvalue));
        ////pdfCell.Border = 0;
        //table.AddCell(pdfCell);

        //doc.Add(table);



        ////pdfCell = new PdfPCell(new Phrase("RCA Team", flabel));
        //////pdfCell.Border = 0;
        ////table.AddCell(pdfCell);

        ////table.AddCell(new Phrase(""));
        ////doc.Add(table);

        //Paragraph headingRcaTeam = new Paragraph("RCA Team", PageHeading);
        //headingRcaTeam.Alignment = Element.ALIGN_LEFT;
        //headingRcaTeam.SpacingAfter = 5;
        ////doc.Add(headingRcaTeam);

        //PdfPTable tableRcaTeam = new PdfPTable(2);
        //float[] widthsRcaTeam = new float[] { 1f, 1f };
        //tableRcaTeam.SetWidths(widthsRcaTeam);
        //tableRcaTeam.SpacingAfter = 3;


        //pdfCell = new PdfPCell(headingRcaTeam);
        //pdfCell.Border = 0;
        //tableRcaTeam.AddCell(pdfCell);

        //pdfCell = new PdfPCell(new Phrase("", flabel));
        //pdfCell.Border = 0;
        //tableRcaTeam.AddCell(pdfCell);


        //pdfCell = new PdfPCell(new Phrase("Name", flabel));
        //pdfCell.Border = 0;
        //tableRcaTeam.AddCell(pdfCell);

        //pdfCell = new PdfPCell(new Phrase("Position", flabel));
        //pdfCell.Border = 0;
        //tableRcaTeam.AddCell(pdfCell);
        //foreach (DataRow Dr in dtRcaTeamData.Rows)
        //{
        //    pdfCell = new PdfPCell(new Phrase(Dr["EmpName"].ToString() + ((Common.CastAsInt32(Dr["EmpID"]) == Common.CastAsInt32(hfdTeamLeadValue.Value) ? " [ Team Leader ]" : "")), fvalue));
        //    pdfCell.Border = 0;
        //    tableRcaTeam.AddCell(pdfCell);

        //    pdfCell = new PdfPCell(new Phrase(Dr["Positionname"].ToString(), fvalue));
        //    pdfCell.Border = 0;
        //    tableRcaTeam.AddCell(pdfCell);
        //}

        //doc.Add(tableRcaTeam);




        //pdfCell = new PdfPCell(new Phrase("Target Closure Date", flabel));
        //pdfCell.Border = 0;
        //table.AddCell(pdfCell);

        //pdfCell = new PdfPCell(new Phrase(Common.ToDateString(DrMasterData["TargetClosureDate"]), fvalue));
        //pdfCell.Border = 0;
        //table.AddCell(pdfCell);

        #endregion
        #region Page2
        //-- New Page Started------------------------------------------------------------------------------
        //doc.NewPage();

        //Paragraph headingPage2 = new Paragraph("Comments", PageHeading);
        //headingPage2.Alignment = Element.ALIGN_CENTER;
        //doc.Add(headingPage2);

        //for (int c = 0; c <= 3; c++)
        //{

        //    Paragraph headingmg = new Paragraph(mg[c], PageHeading);
        //    headingmg.Alignment = Element.ALIGN_LEFT;
        //    headingmg.SpacingAfter = 5;
        //    doc.Add(headingmg);

        //    DataTable dtRcaCauseData = GetRcaCauseData(c + 1);
        //    if (dtRcaCauseData.Rows.Count > 0)
        //    {
        //        foreach (DataRow dr in dtRcaCauseData.Rows)
        //        {
        //            if (dr[1].ToString().Trim().ToLower() != mg[c].ToLower())
        //            {
        //                Paragraph cname = new Paragraph(dr[1].ToString(), PageSubHeading);
        //                cname.Alignment = Element.ALIGN_LEFT;
        //                cname.SpacingAfter = 5;
        //                doc.Add(cname);
        //            }
        //            DataTable dtRcaCommentsData = GetRcaCommentsData(c + 1, Common.CastAsInt32(dr["analysisid"]));
        //            if (dtRcaCommentsData.Rows.Count > 0)
        //            {
        //                PdfPTable tblComments = new PdfPTable(2);
        //                float[] widthsComments = new float[] { 1F, 9f };
        //                tblComments.SetWidths(widthsComments);
        //                tblComments.SpacingBefore = 10;
        //                tblComments.WidthPercentage = 100;



        //                pdfCell = new PdfPCell(new Phrase("Sr#", flabel));
        //                pdfCell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        //                pdfCell.VerticalAlignment = PdfPCell.ALIGN_TOP;
        //                pdfCell.Padding = 4;
        //                tblComments.AddCell(pdfCell);


        //                pdfCell = new PdfPCell(new Phrase("Comments", flabel));
        //                pdfCell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        //                pdfCell.VerticalAlignment = PdfPCell.ALIGN_TOP;
        //                pdfCell.Padding = 4;
        //                tblComments.AddCell(pdfCell);

        //                //pdfCell = new PdfPCell(new Phrase("Comments By", flabel));
        //                //pdfCell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        //                //pdfCell.VerticalAlignment = PdfPCell.ALIGN_TOP;
        //                //pdfCell.Padding = 4;
        //                //tblComments.AddCell(pdfCell);

        //                //pdfCell = new PdfPCell(new Phrase("Comments On", flabel));
        //                //pdfCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        //                //pdfCell.VerticalAlignment = PdfPCell.ALIGN_TOP;
        //                //pdfCell.Padding = 4;
        //                //tblComments.AddCell(pdfCell);

        //                foreach (DataRow Dr in dtRcaCommentsData.Rows)
        //                {
        //                    pdfCell = new PdfPCell(new Phrase(Dr["RowNo"].ToString(), fvalue));
        //                    pdfCell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        //                    pdfCell.VerticalAlignment = PdfPCell.ALIGN_TOP;
        //                    pdfCell.Padding = 4;
        //                    tblComments.AddCell(pdfCell);

        //                    pdfCell = new PdfPCell(new Phrase(Dr["Comments"].ToString(), fvalue));
        //                    pdfCell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        //                    pdfCell.VerticalAlignment = PdfPCell.ALIGN_TOP;
        //                    pdfCell.Padding = 4;
        //                    tblComments.AddCell(pdfCell);


        //                    //pdfCell = new PdfPCell(new Phrase(Dr["CommentBy"].ToString(), fvalue));
        //                    //pdfCell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        //                    //pdfCell.VerticalAlignment = PdfPCell.ALIGN_TOP;
        //                    //pdfCell.Padding = 4;
        //                    //tblComments.AddCell(pdfCell);


        //                    //pdfCell = new PdfPCell(new Phrase(Common.ToDateString(Dr["CommentDate"]), fvalue));
        //                    //pdfCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        //                    //pdfCell.VerticalAlignment = PdfPCell.ALIGN_TOP;
        //                    //pdfCell.Padding = 4;
        //                    //tblComments.AddCell(pdfCell);

        //                }

        //                doc.Add(tblComments);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        Paragraph headingNA = new Paragraph("NA", flabel_small);
        //        headingNA.Alignment = Element.ALIGN_LEFT;
        //        headingNA.SpacingAfter = 5;
        //        doc.Add(headingNA);
        //    }
        //}
        #endregion
        #region Page3
        //-- New Page Started------------------------------------------------------------------------------
        //doc.NewPage();

        Paragraph headingPage3 = new Paragraph("Root Cause and Corrective Action", PageHeading);
        headingPage3.Alignment = Element.ALIGN_CENTER;
        headingPage3.SpacingAfter = 10;
        doc.Add(headingPage3);

        string[] mg = { "People", "Process", "Equipment", "External" };

        for (int c = 0; c <= 3; c++)
        {

            Paragraph headingmg = new Paragraph(mg[c], PageHeading);
            headingmg.Alignment = Element.ALIGN_LEFT;
            headingmg.SpacingAfter = 5;
            doc.Add(headingmg);


            DataTable dtRcaApproval = GetRcaApproval(c + 1);
            if (dtRcaApproval.Rows.Count > 0)
            {
                DataTable dtCorrectiveAction;

                Paragraph paraCause = new Paragraph();
                PdfPTable tblRcaApproval = new PdfPTable(3);
                tblRcaApproval.WidthPercentage = 100;
                int rno = 0;
                foreach (DataRow Dr in dtRcaApproval.Rows)
                {
                    paraCause = new Paragraph(Dr["Rowno"].ToString() + " - " + Dr["Cause"].ToString(), flabel);
                    doc.Add(paraCause);

                    dtCorrectiveAction = GetCorrectiveAction(Common.CastAsInt32(Dr["AnalysisID"]));

                    rno++;
                    pdfCell = new PdfPCell(new Phrase("Corrective Action - " + rno, flabel_small));

                    tblRcaApproval.AddCell(pdfCell);

                    pdfCell = new PdfPCell(new Phrase("Responsibility", flabel_small));
                    pdfCell.Padding = 4;
                    tblRcaApproval.AddCell(pdfCell);

                    pdfCell = new PdfPCell(new Phrase("Target Closure Date", flabel_small));
                    pdfCell.Padding = 4;
                    pdfCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    tblRcaApproval.AddCell(pdfCell);

                    //pdfCell = new PdfPCell(new Phrase("Created By", flabel_small));
                    //pdfCell.Padding = 4;
                    //tblRcaApproval.AddCell(pdfCell);

                    //pdfCell = new PdfPCell(new Phrase("Created On", flabel_small));
                    //pdfCell.Padding = 4;
                    //pdfCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    //tblRcaApproval.AddCell(pdfCell);

                    //pdfCell = new PdfPCell(new Phrase("Modified By", flabel_small));
                    //pdfCell.Padding = 4;
                    //tblRcaApproval.AddCell(pdfCell);
                    //pdfCell = new PdfPCell(new Phrase("Modified On", flabel_small));
                    //pdfCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    ////pdfCell.Border = 0;
                    //tblRcaApproval.AddCell(pdfCell);
                    foreach (DataRow DrCorr in dtCorrectiveAction.Rows)
                    {

                        float[] widthsApproval = new float[] { 90f, 35F, 30f };
                        tblRcaApproval.SetWidths(widthsApproval);
                        tblRcaApproval.SpacingBefore = 10;

                        pdfCell = new PdfPCell(new Phrase(DrCorr["CorrectiveAction"].ToString(), fvalue_small));
                        pdfCell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        pdfCell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                        pdfCell.Padding = 4;
                        tblRcaApproval.AddCell(pdfCell);

                        pdfCell = new PdfPCell(new Phrase(DrCorr["Responsibility"].ToString(), fvalue_small));
                        pdfCell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        pdfCell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                        pdfCell.Padding = 4;
                        tblRcaApproval.AddCell(pdfCell);


                        pdfCell = new PdfPCell(new Phrase(Common.ToDateString(DrCorr["TargetClosureDate"]), fvalue_small));
                        pdfCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        pdfCell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                        pdfCell.Padding = 4;
                        tblRcaApproval.AddCell(pdfCell);


                        //pdfCell = new PdfPCell(new Phrase(DrCorr["createdby"].ToString(), fvalue_small));
                        //pdfCell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        //pdfCell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                        //pdfCell.Padding = 4;
                        //tblRcaApproval.AddCell(pdfCell);

                        //pdfCell = new PdfPCell(new Phrase(Common.ToDateString(DrCorr["createdOn"]), fvalue_small));
                        //pdfCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        //pdfCell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                        //pdfCell.Padding = 4;
                        //tblRcaApproval.AddCell(pdfCell);

                        //pdfCell = new PdfPCell(new Phrase(DrCorr["modifiedby"].ToString(), fvalue_small));
                        //pdfCell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        //pdfCell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                        //pdfCell.Padding = 4;
                        //tblRcaApproval.AddCell(pdfCell);

                        //pdfCell = new PdfPCell(new Phrase(Common.ToDateString(DrCorr["modifiedOn"]), fvalue_small));
                        //pdfCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        //pdfCell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                        //pdfCell.Padding = 4;
                        //tblRcaApproval.AddCell(pdfCell);
                    }
                    doc.Add(tblRcaApproval);
                }
            }
            else
            {
                Paragraph headingNA = new Paragraph("NA", flabel_small);
                headingNA.Alignment = Element.ALIGN_LEFT;
                headingNA.SpacingAfter = 5;
                doc.Add(headingNA);
            }
        }

        #endregion      


        doc.Close();
    }
    public void MergePDFFile()
    {
        string[] sourceFiles = { Server.MapPath("File_01.pdf"),Server.MapPath("File_03.pdf"), Server.MapPath("File_02.pdf") };
        string destinationFile = Server.MapPath("File_04.pdf");
        try
        {
            int f = 0;
            // we create a reader for a certain document
            PdfReader reader = new PdfReader(sourceFiles[f]);
            // we retrieve the total number of pages
            int n = reader.NumberOfPages;
            //Console.WriteLine("There are " + n + " pages in the original file.");
            // step 1: creation of a document-object
            Document document = new Document(reader.GetPageSizeWithRotation(1));
            // step 2: we create a writer that listens to the document
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(destinationFile, FileMode.Create));
            // step 3: we open the document
            document.Open();
            PdfContentByte cb = writer.DirectContent;
            PdfImportedPage page;
            int rotation;
            // step 4: we add content
            while (f < sourceFiles.Length)
            {
                int i = 0;
                while (i < n)
                {
                    i++;
                    document.SetPageSize(reader.GetPageSizeWithRotation(i));
                    document.NewPage();
                    page = writer.GetImportedPage(reader, i);
                    rotation = reader.GetPageRotation(i);
                    if (rotation == 90 || rotation == 270)
                    {
                        cb.AddTemplate(page, 0, -1f, 1f, 0, 0, reader.GetPageSizeWithRotation(i).Height);
                    }
                    else
                    {
                        cb.AddTemplate(page, 1f, 0, 0, 1f, 0, 0);
                    }
                    //Console.WriteLine("Processed page " + i);
                }
                f++;
                if (f < sourceFiles.Length)
                {
                    reader = new PdfReader(sourceFiles[f]);
                    // we retrieve the total number of pages
                    n = reader.NumberOfPages;
                    //Console.WriteLine("There are " + n + " pages in the original file.");
                }
            }
            // step 5: we close the document
            document.Close();
        }
        catch (Exception e)
        {
            string strOb = e.Message;
        }
    }


    public DataTable GetReportData()
    {
        string SQL = "  select r.VesselCode,v.VesselName,r.BreakDownNo as ReportNo " +
                     "  ,case rca.Classification when 1 then 'Minor' when 2 then 'Major' when 3 then 'Severe' end as AccidentSeverity " +
                     "  ,rca.FocalPoint,rca.TargetClosureDate,r.DefectDetails as EventDescription from dbo.VSL_BreakDownMaster r " +
                     "  inner join dbo.VSL_BreakDownRemarks rca on rca.BreakDownNo = r.BreakDownNo and rca.VesselCode = r.VesselCode " +
                     "  inner join dbo.Vessel v on v.VesselCode = r.VesselCode " +
                     "  Where R.BreakDownNo = '" + BreakDownNo + "' and r.VesselCode = '" + VesselCode + "' ";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);

        return dt;
    }
    public DataTable GetRcaTeamData()
    {
        string SQL = " select *,po.PositionName from dbo.VSL_BreakDownRemarks_Team T " +
                     "  INNER JOIN DBO.position PO ON PO.positionid = T.Position " +
                     "  Where BreakDownNo = '" + BreakDownNo + "' and VesselCode = '" + VesselCode + "' order by T.EmpName";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);

        return dt;
    }
    public DataTable GetRcaCommentsData(int mainGroup,int analysisID)
    {
        string SQL = " select ROW_NUMBER()over(order by commentID)as RowNo,* from dbo.VSL_BreakDownRemarks_Analysis_Comments " +
            " where AnalysisID in(select AnalysisID from dbo.VSL_BreakDownRemarks_RCA_Analysis Where BreakDownNo = '" + BreakDownNo + "' and VesselCode = '" + VesselCode + "' and mainGroup=" + mainGroup + " )  ";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);

        return dt;
    }
    public DataTable GetRcaCauseData(int mainGroup)
    {
        string SQL = " select distinct a.analysisid,a.cause from dbo.VSL_BreakDownRemarks_Analysis_Comments c inner join dbo.VSL_BreakDownRemarks_RCA_Analysis a on c.analysisid=a.analysisid Where BreakDownNo = '" + BreakDownNo + "' and a.VesselCode = '" + VesselCode + "' and mainGroup=" + mainGroup + " ";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);

        return dt;
    }
    public DataTable GetRcaApproval(int mainGroup)
    {
        string SQL = " select ROW_NUMBER()over(order by AnalysisID)Rowno, AnalysisID,ParentAnalysisID,BreakDownNo,VesselCode, " +
                     "  Status,CauseId,Cause from dbo.VSL_BreakDownRemarks_RCA_Analysis " +
                     "  where Status = 'R' and BreakDownNo = '" + BreakDownNo + "' and VesselCode='" + VesselCode + "' and mainGroup="+ mainGroup + " ";
        return Common.Execute_Procedures_Select_ByQuery(SQL);
    }
    public DataTable GetCorrectiveAction(int AnalysisID)
    {
        string SQL = " select CAId,AnalysisID,CorrectiveAction,case when responsibility='B' then 'Office, Vessel' when responsibility='V' then 'Vessel' else 'office' end as  Responsibility,TargetClosureDate,createdby,createdon,ModifiedBy,ModifiedOn from dbo.VSL_BreakDownRemarks_Analysis_CorrectiveActions  " +
                        "  where AnalysisID=" + AnalysisID;
        return Common.Execute_Procedures_Select_ByQuery(SQL);

    }
    //-------------------------------------------------------------------------



    public void ShowStageByClicking(int Tab)
    {
        divAssignRca.Visible = false;
        divRcasec.Visible = false;
        divRcaApproval.Visible = false;
        divRcaClosure.Visible = false;
        if (Tab == 1)
            divAssignRca.Visible = true;
        else if (Tab == 2)
            divRcasec.Visible = true;
        else if (Tab == 3)
        {
            BindRcaApproval();
            divRcaApproval.Visible = true;
        }
        else if (Tab == 4)
            divRcaClosure.Visible = true;
    }
    public void ShowStage()
    {
        rdoAssignRCA.Checked = false;
        rdoFinalRca.Checked = false;
        rdoRcaApproval.Checked = false;
        rdoRcaClosure.Checked = false;

        divAssignRca.Visible = false;
        divRcasec.Visible = false;
        divRcaApproval.Visible = false;
        divRcaClosure.Visible = false;

        if (Stage == 1)
        {
            rdoAssignRCA.Checked = true;
            divAssignRca.Visible = true;
        }
        else if (Stage == 2)
        {
            rdoFinalRca.Checked = true;
            divRcasec.Visible = true;
        }
        else if (Stage == 3)
        {
            BindRcaApproval();
            rdoRcaApproval.Checked = true;
            divRcaApproval.Visible = true;
        }
        else if (Stage == 4)
        {
            rdoRcaClosure.Checked = true;
            divRcaClosure.Visible = true;
        }

        rdoAssignRCA.Visible = false;
        rdoFinalRca.Visible = false;
        rdoRcaApproval.Visible = false;
        rdoRcaClosure.Visible = false;

        if (Stage >= 1) rdoAssignRCA.Visible = true;
        if (Stage >= 2) rdoFinalRca.Visible = true;
        if (Stage >= 3) rdoRcaApproval.Visible = true;
        if (Stage >= 4) rdoRcaClosure.Visible = true;
    }

    public void ShowMasterData()
    {
        Stage = 1;
        string SQL = " select * from dbo.VSL_BreakDownRemarks where BreakDownNo = '" + BreakDownNo + "' and VesselCode='" + VesselCode + "' ";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
        if (dt.Rows.Count > 0)
        {
            try
            {
                rdoListSeverity.SelectedValue = dt.Rows[0]["Classification"].ToString();
            }
            catch
            {
                rdoListSeverity.ClearSelection();
            }

            txtFocalPoint.Text = dt.Rows[0]["FocalPoint"].ToString();
            txtTargetClosureDate.Text = Common.ToDateString(dt.Rows[0]["TargetClosureDate"]);
            lblcreatedby.Text = @"Created By\On : " + dt.Rows[0]["InitiatedBy"].ToString() + @" \ " + Common.ToDateString(dt.Rows[0]["InitiatedOn"]);

            if (Common.ToDateString(dt.Rows[0]["PdfCreatedOn"]) != "")
                lblpdfby.Text = @"Pdf Created By\On : " + dt.Rows[0]["PdfCreatedBy"].ToString() + @" \ " + Common.ToDateString(dt.Rows[0]["PdfCreatedOn"]);

            if (Common.ToDateString(dt.Rows[0]["ExportedOn"]) != "")
                lblnotifyby.Text = @"Notified By\On : " + dt.Rows[0]["ExportedBy"].ToString() + @" \ " + Common.ToDateString(dt.Rows[0]["ExportedOn"]);

            Stage = Common.CastAsInt32(dt.Rows[0]["Stage"]);
            hfdTeamLeadValue.Value = Convert.ToString(dt.Rows[0]["TeamLeader"]);
            if (dt.Rows[0]["ApprovedBy"].ToString() == "")
                RcaApprovalStatus = true;
            else
                RcaApprovalStatus = false;
            lnkDownloadPDF.Text = "";
            if (Stage == 4)
            {
                if (dt.Rows[0]["RcaDocument"].ToString() != "")
                {
                    RcaStatus = "C";
                    string ReportNo = BreakDownNo;
                    string filename = "RCA_" + ReportNo.Replace("/", "-") + ".pdf";
                    lnkDownloadPDF.Text = filename;
                }
            }
        }

        ShowStage();
    }
    public void BindTeammembers()
    {
        string SQL = " select *,po.PositionName from dbo.VSL_BreakDownRemarks_Team T " +
                     "  INNER JOIN DBO.position PO ON PO.positionid = T.Position " +
                     "  Where BreakDownNo = '" + BreakDownNo + "' and VesselCode = '" + VesselCode+"' order by T.EmpName";
        DataTable dtTeamMembers = Common.Execute_Procedures_Select_ByQuery(SQL);
        rptTeammembers.DataSource = dtTeamMembers;
        rptTeammembers.DataBind();
    }
    


    //-- Add Team Member -------------------------------------------------------------------------------
    protected void lnkAddTeamMember_OnClick(object sender, EventArgs e)
    {
        ShowTeamMemberToAdd();
        divAddTeamMember.Visible = true;
    }
    protected void btnSaveTeamMember_OnClick(object sender, EventArgs e)
    {
        try
        {
            string empIDs = "";            
            foreach (System.Web.UI.WebControls.ListItem item in chkListFleetManager.Items)
            {
                if (item.Selected)
                    empIDs = empIDs + ',' + item.Value;
            }
            foreach (System.Web.UI.WebControls.ListItem item in chkListTechnicalSuptd.Items)
            {
                if (item.Selected)
                    empIDs = empIDs + ',' + item.Value;
            }
            foreach (System.Web.UI.WebControls.ListItem item in chkListMaringSuptd.Items)
            {
                if (item.Selected)
                    empIDs = empIDs + ',' + item.Value;
            }
            foreach (System.Web.UI.WebControls.ListItem item in chkListManagement.Items)
            {
                if (item.Selected)
                    empIDs = empIDs + ',' + item.Value;
            }
            foreach (System.Web.UI.WebControls.ListItem item in chkListOthers.Items)
            {
                if (item.Selected)
                    empIDs = empIDs + ',' + item.Value;
            }

            if (empIDs.StartsWith(",")) 
                empIDs = empIDs.Substring(1);

            Common.Set_Procedures("[DBO].[VSL_BreakDownRemarks_RCA_Team]");
            Common.Set_ParameterLength(3);
            Common.Set_Parameters(
                new MyParameter("@BreakDownNo", BreakDownNo),
                new MyParameter("@VesselCode", VesselCode),
                new MyParameter("@EmpIDs", empIDs)
                );
            DataSet ds = new DataSet();
            ds.Clear();
            Boolean res;
            res = Common.Execute_Procedures_IUD(ds);
            if (res)
            {
                lblMsgAddMembers.Text = "Record saved successfully.";
                BindTeammembers();
            }
            else
            {
                lblMsgAddMembers.Text = "Unable to save record. " + Common.getLastError();
            }
        }
        catch (Exception ex)
        {
            lblMsgAddMembers.Text = "Unable to save record." + ex.Message;
        }
    }
    protected void btnClose_OnClick(object sender, EventArgs e)
    {
        divAddTeamMember.Visible = false;
    }

    public void ShowTeamMemberToAdd()
    {
        string SQL = " select PD.EmpId,PD.EmpCode,PD.FirstName + ' ' + PD.MiddleName + ' ' + PD.FamilyName as EmpName ,case when Te.EmpName is null then 0 else 1 end EmpSelected "+
                     "  , PO.vesselpositions from DBO.Hr_PersonalDetails PD " +
                     "  left join dbo.VSL_BreakDownRemarks_Team Te on Te.EmpID = PD.EmpID and Te.BreakDownNo='" + BreakDownNo + "' and Te.VesselCode='" + VesselCode + "'" +
                     "  INNER JOIN DBO.position PO ON PO.positionid = PD.Position " +
                     "  WHERE DRC IS NULL order by PO.vesselpositions,EmpName ";
        DataTable dtEmployee = Common.Execute_Procedures_Select_ByQuery(SQL);

        //dtEmployee.AsEnumerable().Where(row => row.Field<int?>("vesselpositions") == 3).DefaultIfEmpty();
        DataView dv = dtEmployee.DefaultView;dv.RowFilter = "vesselpositions=3";

        chkListFleetManager.DataSource = dv;
        chkListFleetManager.DataTextField = "EmpName";
        chkListFleetManager.DataValueField = "EmpId";
        chkListFleetManager.DataBind();
        foreach (System.Web.UI.WebControls.ListItem itm in chkListFleetManager.Items)
        {
            //if (dv.ToTable().AsEnumerable().Where(row => row.Field<int>("EmpSelected") == 1 && row.Field<int>("EmpID") == Common.CastAsInt32( itm.Value)).Count() > 0)
            if(dv.ToTable().Select("EmpSelected=1 and EmpId=" + itm.Value).Length>0)
                itm.Selected = true;
        }

        dv = dtEmployee.DefaultView; dv.RowFilter = "vesselpositions=1";
        chkListTechnicalSuptd.DataSource = dv;
        chkListTechnicalSuptd.DataTextField = "EmpName";
        chkListTechnicalSuptd.DataValueField = "EmpId";
        chkListTechnicalSuptd.DataBind();
        foreach (System.Web.UI.WebControls.ListItem itm in chkListTechnicalSuptd.Items)
        {
            //if (dv.ToTable().AsEnumerable().Where(row => row.Field<int>("EmpSelected") == 1 && row.Field<int>("EmpID") == Common.CastAsInt32(itm.Value)).Count() > 0)
            if (dv.ToTable().Select("EmpSelected=1 and EmpId=" + itm.Value).Length > 0)
                itm.Selected = true;
        }


        dv = dtEmployee.DefaultView; dv.RowFilter = "vesselpositions=2";
        chkListMaringSuptd.DataSource = dv;
        chkListMaringSuptd.DataTextField = "EmpName";
        chkListMaringSuptd.DataValueField = "EmpId";
        chkListMaringSuptd.DataBind();
        foreach (System.Web.UI.WebControls.ListItem itm in chkListMaringSuptd.Items)
        {
            //if (dv.ToTable().AsEnumerable().Where(row => row.Field<int>("EmpSelected") == 1 && row.Field<int>("EmpID") == Common.CastAsInt32(itm.Value)).Count() > 0)
            if (dv.ToTable().Select("EmpSelected=1 and EmpId=" + itm.Value).Length > 0)
                itm.Selected = true;            
        }

        dv = dtEmployee.DefaultView; dv.RowFilter = "vesselpositions=18";
        chkListManagement.DataSource = dv;
        chkListManagement.DataTextField = "EmpName";
        chkListManagement.DataValueField = "EmpId";
        chkListManagement.DataBind();
        foreach (System.Web.UI.WebControls.ListItem itm in chkListManagement.Items)
        {
            //if (dv.ToTable().AsEnumerable().Where(row => row.Field<int>("EmpSelected") == 1 && row.Field<int>("EmpID") == Common.CastAsInt32(itm.Value)).Count() > 0)
            if (dv.ToTable().Select("EmpSelected=1 and EmpId=" + itm.Value).Length > 0)
                itm.Selected = true;
        }
        
        dv = dtEmployee.DefaultView; dv.RowFilter = "vesselpositions=13 or vesselpositions=14 or vesselpositions=26";
        chkListOthers.DataSource = dv;
        chkListOthers.DataTextField = "EmpName";
        chkListOthers.DataValueField = "EmpId";
        chkListOthers.DataBind();
        foreach (System.Web.UI.WebControls.ListItem itm in chkListOthers.Items)
        {
            //if (dv.ToTable().AsEnumerable().Where(row => row.Field<int>("EmpSelected") == 1 && row.Field<int>("EmpID") == Common.CastAsInt32(itm.Value)).Count() > 0)
            if (dv.ToTable().Select("EmpSelected=1 and EmpId=" + itm.Value).Length > 0)
                itm.Selected = true;
        }
        
        
    }

    //-- RCA Approval -------------------------------------------------------------------------------
    
    protected void btnOpenCA_OnClick(object sender, EventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        Edit_CAID = Common.CastAsInt32(btn.CommandArgument);
        ShowCurectiveActionForEdit();
        divCorrectiveAction.Visible = true;
    }
    protected void btnSaveCorrectiveAction_OnClick(object sender, EventArgs e)
    {
        try
        {
            Common.Set_Procedures("[DBO].VSL_BreakDownRemarks_RCA_Analysis_CorrectiveActions");
            Common.Set_ParameterLength(6);
            Common.Set_Parameters(
                new MyParameter("@CAID", Edit_CAID),
                new MyParameter("@AnalysisID", 0),
                new MyParameter("@catext", txtCorrectiveAction.Text ),
                new MyParameter("@Responsibility", rdoListResponsibility.SelectedValue),
                new MyParameter("@TargetClosureDate", txtCATargetClosureDate.Text),
                new MyParameter("@CreatedBy", HttpContext.Current.Session["UserName"].ToString())
                );
            DataSet dsComponents = new DataSet();
            dsComponents.Clear();
            Boolean res;
            res = Common.Execute_Procedures_IUD(dsComponents);
            if (res)
            {
                ShowMasterData();
                BindTeammembers();
                lblMsgCorrectiveAction.Text = "Record updated successfully.";
            }
        }
        catch (Exception ex)
        {
            lblMsgCorrectiveAction.Text = "Error while saving record.";
        }
    }
    protected void btnCloseCorrectiveAction_OnClick(object sender, EventArgs e)
    {
        divCorrectiveAction.Visible = false;
        Edit_CAID = 0;
    }

    public void BindRcaApproval()
    {
        string SQL = " select ROW_NUMBER()over(order by AnalysisID)Rowno, AnalysisID,ParentAnalysisID,BreakDownNo,VesselCode, " +
                     "  Status,CauseId,Cause from dbo.VSL_BreakDownRemarks_RCA_Analysis " +
                     "  where Status = 'R' and BreakDownNo='" + BreakDownNo + "' and VesselCode='" + VesselCode+"' ";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
        rptRcaApproval.DataSource = dt;
        rptRcaApproval.DataBind();
    }
    public DataTable ShowCorrectiveAction(int AnalysisID)
    {
        string SQL = " select CAId,AnalysisID,CorrectiveAction,Responsibility,TargetClosureDate,createdby,createdon,ModifiedBy,ModifiedOn from dbo.VSL_BreakDownRemarks_Analysis_CorrectiveActions  " +
                        "  where AnalysisID="+ AnalysisID;
        return Common.Execute_Procedures_Select_ByQuery(SQL);
        
    }
    public void ShowCurectiveActionForEdit()
    {
        string SQL = " select * from dbo.VSL_BreakDownRemarks_Analysis_CorrectiveActions where CAID=" + Edit_CAID;
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
        if (dt.Rows.Count > 0)
        {
            DataRow dr = dt.Rows[0];
            txtCorrectiveAction.Text = dr["CorrectiveAction"].ToString();
            rdoListResponsibility.SelectedValue = dr["Responsibility"].ToString();
            txtCATargetClosureDate.Text = Common.ToDateString(dr["TargetClosureDate"]);
        }
        else
        {
            txtCorrectiveAction.Text = "";
            rdoListResponsibility.ClearSelection();
            txtCorrectiveAction.Text = "";
        }
    }
}