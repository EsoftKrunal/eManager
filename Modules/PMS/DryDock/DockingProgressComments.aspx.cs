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
using System.IO;
using CrystalDecisions.CrystalReports.Engine;
using iTextSharp.text.pdf;
using iTextSharp.text;

public partial class DryDock_DockingProgressComments : System.Web.UI.Page
{
    ArrayList FileName = new ArrayList();
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();

    public int DocketId
    {
        set { ViewState["DocketId"] = value; }
        get { return Common.CastAsInt32(ViewState["DocketId"]); }        
    }
    public int RFQId
    {
        set { ViewState["RFQId"] = value; }
        get { return Common.CastAsInt32(ViewState["RFQId"]); }
    }
    public int CatId
    {
        set { ViewState["CatId"] = value; }
        get { return Common.CastAsInt32(ViewState["CatId"]); }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
       if (!IsPostBack)
       {
           DocketId = Common.CastAsInt32(Request.QueryString["DocketId"]);
           if (DocketId > 0)
           {
               DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT RFQID FROM DD_Docket_RFQ_Master WHERE DOCKETID=" + DocketId + " AND STATUS='P'");
               if (dt.Rows.Count > 0)
               {
                   RFQId = Common.CastAsInt32(dt.Rows[0]["RFQId"]);
                   CatId = Common.CastAsInt32(Request.QueryString["CatId"]);
                   BindJobs();
               }
           }
       }
    }
    protected void BindJobs()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("select ( ROW_Number() OVER( Order By JOBCODE) + 1 ) AS Srno,DOCKETJOBID,JOBCODE,JOBNAME from DD_Docket_RFQ_jobs WHERE DOCKETID=" + DocketId + " AND RFQID=" + RFQId + " AND CATID=" + CatId + " ORDER BY JOBCODE");
        rptJobs.DataSource = dt;
        rptJobs.DataBind();
    }

    protected int CountComments(object DOCKETJOBID)
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT count(*) from [dbo].[DD_Docket_RFQ_Jobs_Planning] WHERE RFQID=" + RFQId + " AND DOCKETJOBID=" + DOCKETJOBID.ToString() + " AND ISNULL(REPORT,0)=1");
        return Common.CastAsInt32(dt.Rows[0][0]);
    }
    protected DataTable BindComments(object DOCKETJOBID)
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT FOR_DATE,PER,REMARK from [dbo].[DD_Docket_RFQ_Jobs_Planning] WHERE RFQID=" + RFQId + " AND DOCKETJOBID=" + DOCKETJOBID.ToString() + " AND ISNULL(REPORT,0)=1");
        return dt;
    }
   
  
    private void PublishToPDF()
    {
        //try
        //{
        //    string strVelName = "SELECT Vesselname from dbo.Vessel VSL where VSL.VesselId=" + VesselID + " ";
        //    DataTable dtVesselName = Budget.getTable(strVelName).Tables[0];

        //    string imagePath = ConfigurationManager.AppSettings["VslImgLink"].ToString() + "vessel_" + VesselID.ToString() + ".jpg";

        //    if (!File.Exists(imagePath))
        //    {
        //        imagePath = ConfigurationManager.AppSettings["VslImgLink"].ToString() + "NoImage.jpg";
        //    }


        //    Document document = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4, 10, 10, 10, 10);

        //    System.IO.MemoryStream msReport = new System.IO.MemoryStream();
        //    PdfWriter writer = PdfWriter.GetInstance(document, msReport);
        //    document.AddAuthor("eMANAGER");
        //    document.AddSubject("Monthly Owner’s Technical & Operating Report (MOTOR)");
        //    //'Adding Header in Document
        //    iTextSharp.text.Image logoImg = default(iTextSharp.text.Image);
        //    logoImg = iTextSharp.text.Image.GetInstance(Server.MapPath("~\\Images\\Logo\\MTMMLogo.jpg"));
        //    Chunk chk = new Chunk(logoImg, 0, 0, true);
        //    //Phrase p1 = new Phrase();
        //    //p1.Add(chk);

        //    iTextSharp.text.Table tb_header = new iTextSharp.text.Table(1);
        //    tb_header.Width = 100;
        //    tb_header.Border = iTextSharp.text.Rectangle.NO_BORDER;
        //    tb_header.DefaultCellBorder = iTextSharp.text.Rectangle.NO_BORDER;
        //    tb_header.DefaultHorizontalAlignment = Element.ALIGN_LEFT;
        //    tb_header.BorderWidth = 0;
        //    tb_header.BorderColor = iTextSharp.text.Color.WHITE;
        //    tb_header.Cellspacing = 1;
        //    tb_header.Cellpadding = 1;

        //    Cell c1 = new Cell(chk);
        //    c1.HorizontalAlignment = Element.ALIGN_LEFT;
        //    tb_header.AddCell(c1);

        //    Phrase p2 = new Phrase();
        //    p2.Add(new Phrase("Technical Inspection " + "\n" + "\n", FontFactory.GetFont("ARIAL", 18, iTextSharp.text.Font.BOLD)));
        //    Cell c2 = new Cell(p2);
        //    c2.HorizontalAlignment = Element.ALIGN_CENTER;
        //    tb_header.AddCell(c2);

        //    HeaderFooter header = new HeaderFooter(new Phrase(" \n Technical Inspection -" + txtplannedport.Text + " - " + txtlastdone.Text + "                                                     FORM NO : G 129B"), false);
        //    document.Header = header;

        //    header.Border = iTextSharp.text.Rectangle.NO_BORDER;
        //    string foot_Txt = "";
        //    foot_Txt = foot_Txt + "                                                                                                                ";
        //    foot_Txt = foot_Txt + "                                                                                                                ";
        //    foot_Txt = foot_Txt + "";
        //    HeaderFooter footer = new HeaderFooter(new Phrase(foot_Txt, FontFactory.GetFont("VERDANA", 6, iTextSharp.text.Color.DARK_GRAY)), true);
        //    footer.Border = iTextSharp.text.Rectangle.NO_BORDER;
        //    footer.Alignment = Element.ALIGN_LEFT;
        //    document.Footer = footer;
        //    //'-----------------------------------
        //    document.Open();
        //    document.Add(tb_header);

        //    // ---------------------- FRONT PAGE -------------------------------------------

        //    iTextSharp.text.Table tbF = new iTextSharp.text.Table(1);
        //    tbF.Width = 100;
        //    tbF.Alignment = Element.ALIGN_CENTER;
        //    tbF.DefaultHorizontalAlignment = Element.ALIGN_CENTER;
        //    tbF.Cellspacing = 1;
        //    tbF.Cellpadding = 1;
        //    tbF.Border = iTextSharp.text.Rectangle.NO_BORDER;


        //    iTextSharp.text.Image Img1 = default(iTextSharp.text.Image);
        //    Img1 = iTextSharp.text.Image.GetInstance(imagePath);
        //    Img1.ScaleAbsoluteWidth(500);
        //    Img1.ScaleAbsoluteHeight(400);
        //    Cell tcImg1 = new Cell(Img1);
        //    tcImg1.Border = iTextSharp.text.Rectangle.NO_BORDER;
        //    tbF.AddCell(tcImg1);


        //    iTextSharp.text.Font fCapText_11 = FontFactory.GetFont("ARIAL", 11, iTextSharp.text.Font.BOLD);
        //    iTextSharp.text.Font fCapText_10 = FontFactory.GetFont("ARIAL", 10, iTextSharp.text.Font.NORMAL);

        //    iTextSharp.text.Table tbM = new iTextSharp.text.Table(2);
        //    tbM.Width = 80;
        //    tbM.Alignment = Element.ALIGN_CENTER;
        //    tbM.DefaultHorizontalAlignment = Element.ALIGN_RIGHT;
        //    tbM.Cellspacing = 1;
        //    tbM.Cellpadding = 1;
        //    tbM.BorderWidth = 1;
        //    tbM.BorderColor = iTextSharp.text.Color.BLACK;

        //    Cell tc = new Cell(new Phrase("Vessel Name : ", fCapText_11));
        //    tbM.AddCell(tc);


        //    Cell tc1 = new Cell(new Phrase(dtVesselName.Rows[0]["Vesselname"].ToString(), fCapText_11));
        //    tc1.HorizontalAlignment = Element.ALIGN_LEFT;
        //    tbM.AddCell(tc1);

        //    Cell tcR1 = new Cell(new Phrase("INSP. # : ", fCapText_11));
        //    tbM.AddCell(tcR1);

        //    Cell tcR11 = new Cell(new Phrase(txtinspno.Text, fCapText_11));
        //    tcR11.HorizontalAlignment = Element.ALIGN_LEFT;
        //    tbM.AddCell(tcR11);

        //    Cell tcR33 = new Cell(new Phrase("INSP. START DT. : ", fCapText_11));
        //    tbM.AddCell(tcR33);
        //    Cell tcR33a = new Cell(new Phrase(HiddenField_StartDate.Value, fCapText_11));
        //    tcR33a.HorizontalAlignment = Element.ALIGN_LEFT;
        //    tbM.AddCell(tcR33a);


        //    Cell tcR3 = new Cell(new Phrase("INSP. DONE DT. : ", fCapText_11));
        //    tbM.AddCell(tcR3);
        //    Cell tcR31 = new Cell(new Phrase(txtlastdone.Text, fCapText_11));
        //    tcR31.HorizontalAlignment = Element.ALIGN_LEFT;
        //    tbM.AddCell(tcR31);

        //    Cell tcR7 = new Cell(new Phrase("PLACE : ", fCapText_11));
        //    tbM.AddCell(tcR7);
        //    Cell tcR71 = new Cell(new Phrase(txtplannedport.Text, fCapText_11));
        //    tcR71.HorizontalAlignment = Element.ALIGN_LEFT;
        //    tbM.AddCell(tcR71);

        //    //------------------------
        //    Cell tcR8 = new Cell(new Phrase("Date of Report : ", fCapText_11));
        //    tbM.AddCell(tcR8);
        //    Cell tcR8a = new Cell(new Phrase(DateTime.Now.ToString("dd-MMM-yyyy"), fCapText_11));
        //    tcR8a.HorizontalAlignment = Element.ALIGN_LEFT;
        //    tbM.AddCell(tcR8a);

        //    Cell tcR5 = new Cell(new Phrase("Inspector's Name : ", fCapText_11));
        //    tbM.AddCell(tcR5);
        //    Cell tcR51 = new Cell(new Phrase(txtSupName.Text, fCapText_11));
        //    tcR51.HorizontalAlignment = Element.ALIGN_LEFT;
        //    tbM.AddCell(tcR51);

        //    Cell tccont = new Cell(tbM);
        //    tccont.Border = iTextSharp.text.Rectangle.NO_BORDER;
        //    tbF.AddCell(tccont);



        //    document.Add(tbF);
        //    document.Add(new Phrase("\n", FontFactory.GetFont("ARIAL", 12, iTextSharp.text.Font.BOLD)));

        //    // -----------------------------------------------------------------------------
        //    document.NewPage();


        //    //'-----------------------------------//'-----------------------------------//'-----------------------------------
        //    //string strDetails = "SELECT MDId,SrNo,Title,Descr FROM MortorDetails WHERE MID = " + MID;
        //    string strInsp = "SELECT Id,InspectionDueId,SrNo,ContentHeading,ContentText FROM InspReport_Main WHERE InspectionDueId=" + intInspDueId + " order by cast(srno as float)";
        //    //Content table

        //    DataTable dtInsp = Budget.getTable(strInsp).Tables[0];
        //    for (int i = 0; i <= dtInsp.Rows.Count - 1; i++)
        //    {
        //        #region --------- Start Main Table -------
        //        iTextSharp.text.Table tb1 = new iTextSharp.text.Table(1);
        //        tb1.Width = 100;
        //        tb1.Alignment = Element.ALIGN_CENTER;
        //        tb1.BorderWidth = 1;
        //        tb1.DefaultHorizontalAlignment = Element.ALIGN_LEFT;
        //        tb1.BorderColor = iTextSharp.text.Color.BLACK;
        //        tb1.Cellspacing = 1;
        //        tb1.Cellpadding = 1;
        //        #endregion
        //        AddTitle(dtInsp.Rows[i]["SrNo"].ToString() + ". " + dtInsp.Rows[i]["ContentHeading"].ToString(), tb1);
        //        AddContent(dtInsp.Rows[i]["ContentText"].ToString(), tb1);

        //        string strImages = "SELECT * FROM InspReport_Child where InspectionDueId = " + dtInsp.Rows[i]["InspectionDueId"].ToString() + " AND SrNo = " + dtInsp.Rows[i]["SrNo"].ToString() + " ";
        //        DataTable dtImages = Budget.getTable(strImages).Tables[0];

        //        //------------- Adding Images
        //        #region --------- Start Image Table -------
        //        iTextSharp.text.Table tbInner = new iTextSharp.text.Table(2);
        //        tbInner.Width = 100;
        //        float[] ws1 = { 49, 49 };
        //        tbInner.Widths = ws1;

        //        tbInner.Alignment = Element.ALIGN_CENTER;
        //        tbInner.BorderWidth = 0;
        //        tbInner.DefaultCellBorderWidth = 0;
        //        tbInner.DefaultCellBorderColor = iTextSharp.text.Color.WHITE;

        //        tbInner.DefaultHorizontalAlignment = Element.ALIGN_CENTER;
        //        tbInner.BorderColor = iTextSharp.text.Color.WHITE;
        //        tbInner.Cellspacing = 1;
        //        tbInner.Cellpadding = 1;
        //        #endregion
        //        //-------------
        //        for (int j = 0; j <= dtImages.Rows.Count - 1; j++)
        //        {
        //            AddImage(dtImages.Rows[j]["FilePath"].ToString(), dtImages.Rows[j]["PicCaption"].ToString(), tbInner);
        //        }
        //        Cell tcImages = new Cell(tbInner);
        //        tcImages.HorizontalAlignment = Element.ALIGN_CENTER;
        //        tb1.AddCell(tcImages);
        //        //------------- Adding Images End

        //        document.Add(tb1);
        //        document.Add(new Phrase("\n", FontFactory.GetFont("ARIAL", 12, iTextSharp.text.Font.BOLD)));
        //    }


        //    //'-----------------------------------//'-----------------------------------//'-----------------------------------
        //    document.Close();

        //    //-----------------------------------------------------
        //    string inspno = "";
        //    DataTable dt9 = Common.Execute_Procedures_Select_ByQuery("SELECT INSPECTIONNO FROM T_INSPECTIONDUE WHERE ID=" + intInspDueId.ToString());
        //    if (dt9.Rows.Count > 0)
        //    {
        //        inspno = dt9.Rows[0][0].ToString();
        //    }

        //    string strname = inspno.Replace("/", "-") + ".pdf";
        //    string strpath = "~\\UserUploadedDocuments\\Transactions\\" + strname;
        //    if (File.Exists(Server.MapPath(strpath)))
        //    {
        //        File.Delete(Server.MapPath(strpath));
        //    }
        //    FileStream fs = new FileStream(Server.MapPath(strpath), FileMode.Create);
        //    //-----------------------------------------------------
        //    //if (File.Exists(Server.MapPath("~\\ReportInsp.pdf")))
        //    //{
        //    //    File.Delete(Server.MapPath("~\\ReportInsp.pdf"));
        //    //}

        //    //FileStream fs = new FileStream(Server.MapPath("~\\ReportInsp.pdf"), FileMode.Create);
        //    byte[] bb = msReport.ToArray();
        //    fs.Write(bb, 0, bb.Length);
        //    fs.Flush();
        //    fs.Close();

        //}
        //catch (System.Exception ex)
        //{
        //    //lblmessage.Text = ex.Message.ToString();
        //}
    }

}