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
using System.Data.SqlClient;
using System.Xml;
using ShipSoft.CrewManager.BusinessLogicLayer;
using ShipSoft.CrewManager.BusinessObjects;
using ShipSoft.CrewManager.Operational;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

public partial class AddSafetyAlert : System.Web.UI.Page
{
    public int SAID
    {
        set { ViewState["SAID"] = value; }
        get { return int.Parse("0" + ViewState["SAID"]); }
    }
    public int Source
    {
        set { ViewState["Source"] = value; }
        get { return int.Parse("0" + ViewState["Source"]); }
    }
    public string SANo
    {
        set { ViewState["SANo"] = value; }
        get { return Convert.ToString(ViewState["SANo"]); }
    }

    public Authority Auth;
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 292);
        OBJ.Invoke();
        Session["Authority"] = OBJ.Authority;
        Auth = OBJ.Authority;

        lblMsg.Text = "";
        lblMsgNotify.Text = "";
        if (!Page.IsPostBack)
        {
            if (Page.Request.QueryString["SAID"] != null)
                SAID = Common.CastAsInt32(Page.Request.QueryString["SAID"]);
            
            txtSADate.Text = System.DateTime.Now.ToString("dd-MMM-yyyy");
            if (Page.Request.QueryString["CreateSAID"] != null)
            {
                if (SAID == 0)
                {
                    SetFormDesignAndData();
                    SetPageControlVisibility("A");

                }
                else
                {
                    ShowAlert();
                    SetPageControlVisibility("E");
                    ShowNotifiedData();
                }
            }
        }
    }

    protected void btnSave_OnClick(object sender, EventArgs e)
    {
        try
        {
            if (txtSADate.Text.Trim() == "")
            {
                lblMsg.Text = "Please enter approval date.";
                txtSADate.Focus();
                return;
            }
            if (txtSATopic.Text.Trim() == "")
            {
                lblMsg.Text = "Please enter circular topic.";
                txtSATopic.Focus();
                return;
            }
            if (txtSADetails.Text.Trim() == "")
            {
                lblMsg.Text = "Please enter circular details.";
                txtSADetails.Focus(); return;
            }

            //if (SAID != 0)
            //{
            //    if (!fuAddFile.HasFile)
            //    {
            //        lblMsg.Text = "Please select a file.";
            //        return;
            //    }
            //}
            if (Session["loginid"] != null)
            {
                string FileName = "";
                if (chk_FileExtension(fuAddFile.FileName.ToLower()) == true)
                {
                    FileName = fuAddFile.FileName;
                    FileName = FileName.Replace(".pdf",""); 
                }
                else
                {
                    lblMsg.Text = "Only PDF file will be acceppted.";
                    return;
                }

                string sql = "";
                if (SAID == 0)
                    sql = "sp_InsertSA_SafetyAlert '" + lblSource.Text.Trim().Replace("'", "''") + "','" + txtSADate.Text.Trim() + "','" + txtSATopic.Text.Trim().Replace("'", "''") + "','" + txtSADetails.Text.Trim().Replace("'", "''") + "','" + FileName + "'," + Common.CastAsInt32(Session["loginid"].ToString());
                else
                    sql = "sp_UPDATESA_SafetyAlert " + SAID + ",'" + lblSource.Text.Trim().Replace("'", "''") + "','" + txtSATopic.Text.Trim().Replace("'", "''") + "','" + txtSADetails.Text.Trim().Replace("'", "''") + "','" + FileName + "'";

                DataSet Ds = Budget.getTable(sql);
                if (Ds != null)
                {

                    if (SAID == 0)
                        SAID = Common.CastAsInt32(Ds.Tables[0].Rows[0]["SAID"].ToString());
                    SetPageControlVisibility("E");

                    SANo = Convert.ToString(Ds.Tables[0].Rows[0]["SANUMBER"]);
                    string UploadedFileName = Convert.ToString(Ds.Tables[0].Rows[0]["FName"]);
                    
                    lblMsg.Text = "Record Saved Successfully.";
                    CreatePDF();
                    int NoOfFile = 0;
                    if (fuAddFile.HasFile)
                        NoOfFile = 2;
                    else
                        NoOfFile = 1;

                    string[] SourceFile = new string[NoOfFile];
                    if (fuAddFile.HasFile)
                    {
                        string toPath = Server.MapPath("~/EMANAGERBLOB/LPSQE/SafetyAlert/" + UploadedFileName);
                        fuAddFile.SaveAs(toPath);
                        SourceFile[0] = Server.MapPath("~/EMANAGERBLOB/LPSQE/SafetyAlert/SafetyAlert.pdf");
                        SourceFile[1] = toPath;
                    }
                    else
                    {
                        SourceFile[0] = Server.MapPath("~/EMANAGERBLOB/LPSQE/SafetyAlert/SafetyAlert.pdf");
                    }
                    string Destination = Server.MapPath("~/EMANAGERBLOB/LPSQE/SafetyAlert/SafetyAlert_" + Ds.Tables[0].Rows[0]["SANUMBER"].ToString() + ".pdf");
                    if (File.Exists(Destination))
                        File.Delete(Destination);

                    //ExportToPDF(SourceFile, Destination, "", " ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- Created By/On    : " + vCreatedBy + " / " + vCreatedOn + "                                                                                                               Circular Number : " + vCircularNumber + "\n Approved By/On : " + vApprejBy + " / " + vApprejOn + "                                                                                                                                                                       Page ");
                    if (NoOfFile==2)
                        ExportToPDF(SourceFile, Destination, "", " ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- Created By/On    : " + Session["UserName"].ToString() + " / " + DateTime.Today.ToString("dd-MMM-yyyy") + "                                                                                                               Safety Alert : " + SANo + "\n " + "                                                                                                                                                                                                               ");
                    else
                        ExportToPDF(SourceFile, Destination, "", " ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- Created By/On    : " + Session["UserName"].ToString() + " / " + DateTime.Today.ToString("dd-MMM-yyyy") + "                                                                                                               Safety Alert : " + SANo + "\n " + "                                                                                                                                                                                                               Page ");
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "ddd", "RefereshParentPage();", true);
                }
            }
            else
            {
                lblMsg.Text = "Record Not Saved.";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Record Not Saved." + ex.Message;
        }
    }
    protected void btnEdit_OnClick(object sender, EventArgs e)
    {
        btnSave.Visible = true;
        SetPageControlVisibility("A");
    }
    protected void btnNotify_OnClick(object sender, EventArgs e)
    {
        string sql = "Update SA_SafetyAlert set NotifiedBy=" + Common.CastAsInt32( Session["loginid"]) + ",NotifiedOn='"+DateTime.Today.ToString("dd-MMM-yyyy")+"' where SAID=" + SAID + "";
        DataSet DT = Budget.getTable(sql);
        ShowNotifiedData();
        
        string[] CCmails = { "emanager@energiossolutions.com", "emanager@energiossolutions.com" };
        SendMail.SafetyAlertNotificationMail("emanager@energiossolutions.com", "emanager@energiossolutions.com", CCmails, "Safety Alert - " + SANo, ViewState["vTopic"].ToString(), Server.MapPath("~/EMANAGERBLOB/LPSQE/SafetyAlert/SafetyAlert_" + SANo + ".pdf").ToString(), true);


        lblMsgNotify.Text = "Mail send successfully.";
    }
    
    protected void btnCancel_OnClick(object sender, EventArgs e)
    {
        txtSADetails.Text = "";
        txtSATopic.Text = "";
        ScriptManager.RegisterStartupScript(Page,this.GetType(),"aa","CloseThisWindow();",true);
    }
    // Function -----------------------------------------------------------------------------------------------    
    public void ShowAlert()
    {
        string sql = "";
        sql = " select SAID,Date,Topic,Details,('SafetyAlert_SA-'+CONVERT(VARCHAR,YEAR(DATE))+'-'+RIGHT ('000'+ CAST (SANumber AS int), 3))CFileName  " +
            " ,( case when len( convert(varchar(7000),SA.Details))>95 then substring(SA.Details,1,90)+'............'else  SA.Details end ) as ShortDetails" +
            " ,('SA-'+CONVERT(VARCHAR,YEAR(DATE))+'-'+RIGHT ('000'+ CAST (SANumber AS int), 3)) SANumber " +
            " ,(case when SA.FileName='' then 'none' else 'block' end)ClipVisibility " +
            " ,replace(convert(varchar,SA.Date,106),' ','-') DateText " +
            " ,Source " +
            //" ,(select  (FirstName+' '+MiddleName+' '+FamilyName) from  dbo.Hr_PersonalDetails U where U.Empid=C.Source)SourceName" +
            " ,CreatedBy " +
            " ,(select  U.FirstName+' '+U.Lastname  from  dbo.userlogin U where U.Loginid=SA.CreatedBy )CreatedByName " +
            " ,CreatedOn " +
            " ,replace(convert(varchar,CreatedOn,106),' ','-') CreatedOnText " +
            " from SA_SafetyAlert SA where SA.SAID=" + SAID + "";

        DataSet DS = Budget.getTable(sql);
        if (DS != null)
        {
            if (DS.Tables[0].Rows.Count > 0)
            {
                DataRow DR = DS.Tables[0].Rows[0];
                txtSADate.Text = DR["DateText"].ToString();
                lblSource.Text = DR["Source"].ToString().Replace("''", "'");
                txtSATopic.Text = DR["Topic"].ToString().Replace("''", "'");
                txtSADetails.Text = DR["Details"].ToString().Replace("''", "'");
                SANo = DR["SANumber"].ToString();

                aFile.Visible = true;
                aFile.HRef = "../UserUploadedDocuments/SafetyAlert/" + DR["CFileName"].ToString()+".pdf";
                
            }
        }
    }
    public void SetFormDesignAndData()
    {
        //lblSource.Text = Session["UserName"].ToString();
        Source = Common.CastAsInt32(Session["loginid"].ToString());
        txtSADate.Enabled = true;
        ImageButton1.Visible = true;
    }
    public bool chk_FileExtension(string str)
    {
        string extension = "";
        if (str != "")
            extension = str.Substring(str.Length - 4, 4);
        else
            return true;
        switch (extension)
        {
            case ".pdf":
                return true;
            default:
                return false;
                break;
        }
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
    private void CreatePDF()
    {
        try
        {
            string sql = "select Topic,Details, ('SA-'+CONVERT(VARCHAR,YEAR(DATE))+'-'+RIGHT ('000'+ CAST (SANumber AS varchar), 3))SANumber,Source" +               
               " ,replace(convert(varchar, Date ,106),' ','-')Date" +
               " ,(select FirstName+' '+LastName from dbo.UserLogin U where U.LoginID=SA.CreatedBy)CreatedByName " +
               " ,replace(convert(varchar,CreatedOn,106),' ','-')CreatedOnText " +
               " from SA_SafetyAlert SA where SAID=" + SAID + "";
            DataSet ds = Budget.getTable(sql);

            string Topic = ds.Tables[0].Rows[0]["Topic"].ToString();            
            string Details = ds.Tables[0].Rows[0]["Details"].ToString();
            string Date = ds.Tables[0].Rows[0]["Date"].ToString();            
            string SANumber = ds.Tables[0].Rows[0]["SANumber"].ToString();
            //vCircularNumber = ds.Tables[0].Rows[0]["CircularNumber"].ToString();

            //vApprejBy = ds.Tables[0].Rows[0]["ApprejBy"].ToString();
            //vApprejOn = ds.Tables[0].Rows[0]["ApprejOn"].ToString();

            string Source = ds.Tables[0].Rows[0]["Source"].ToString();

            //vCreatedBy = ds.Tables[0].Rows[0]["CreatedByName"].ToString();
            //vCreatedOn = ds.Tables[0].Rows[0]["CreatedOnText"].ToString();

            Document document = new Document(PageSize.A4, 10, 10, 30, 40);
            System.IO.MemoryStream msReport = new System.IO.MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(document, msReport);
            //document.AddAuthor("MTMSM");
            //document.AddSubject("Follow Up Sheet");

            //'Adding Header in Document --------------------------------------------------

            //iTextSharp.text.Image logoImg = default(iTextSharp.text.Image);
            //logoImg = iTextSharp.text.Image.GetInstance(System.Windows.Forms.Application.StartupPath + "\\Images\\MTMMlogo.jpg");
            //logoImg.SetAbsolutePosition(0, 5);
            //logoImg.ScalePercent(59);
            //Chunk chk = new Chunk(logoImg, 10, 10, true);

            Phrase p1 = new Phrase();
            // Adding Image --------------------------------------------------
            //p1.Add(chk);

            // Adding Vessel Name 
            //p1.Add(new Phrase("            " + Source, FontFactory.GetFont("ARIAL", 14, iTextSharp.text.Font.BOLD)));
            //Adding As On Date
            //Phrase ph1 = new Phrase("                                                                    ", FontFactory.GetFont("ARIAL", 6, iTextSharp.text.Font.ITALIC));
            //p1.Add(ph1);


            HeaderFooter header = new HeaderFooter(p1, false);
            document.Header = header;
            header.Alignment = Element.ALIGN_LEFT;
            header.Border = iTextSharp.text.Rectangle.NO_BORDER;
            //'Adding Footer in document
            string foot_Txt = "";
            foot_Txt = foot_Txt + "";
            foot_Txt = foot_Txt + "";
            //foot_Txt = foot_Txt + "Approved By  : " + ApprejBy + "              Approved On : " + ApprejOn + "";
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
            //iTextSharp.text.Font fNoCellBorder = FontFactory.GetFont("ARIAL", 2, iTextSharp.text.Font.BOLD,);

            //------------ TABLE HEADER FIRST ROW 
            //iTextSharp.text.Table tb1 = new iTextSharp.text.Table(2);
            //tb1.BackgroundColor = iTextSharp.text.Color.LIGHT_GRAY;
            //tb1.Width = 90;
            //float[] ws = { 50 , 50 };
            //tb1.Widths = ws;
            //tb1.Alignment = Element.ALIGN_CENTER;
            //tb1.Border = iTextSharp.text.Rectangle.NO_BORDER;
            //tb1.DefaultHorizontalAlignment = Element.ALIGN_CENTER;
            //tb1.BorderColor = iTextSharp.text.Color.WHITE;
            //tb1.Cellspacing = 1;
            //tb1.Cellpadding = 1;
            //tb1.AddCell(new Phrase("Sr#", fCapText));
            //tb1.AddCell(new Phrase("Description", fCapText));
            //tb1.AddCell(new Phrase("Due Date", fCapText));
            //tb1.AddCell(new Phrase("Completion Date", fCapText));
            //tb1.AddCell(new Phrase("Responsibility", fCapText));
            //document.Add(tb1);




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
            tbCom.AddCell(new Phrase("Energios Maritime Pvt Ltd.", fCapTextCompheading));
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
            tb1.AddCell(new Phrase("Date : " + Date + "", fCapText)); //????
            tb1.AddCell(new Phrase(" " + "", fCapText));
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
            tb2.AddCell(new Phrase("Safety Alert :                           " + SANumber + "                         \n", fCapTextHeading));
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
            if (File.Exists(Server.MapPath("~/EMANAGERBLOB/LPSQE/SafetyAlert/" + "SafetyAlert.pdf")))
            {
                File.Delete(Server.MapPath("~/EMANAGERBLOB/LPSQE/SafetyAlert/" + "SafetyAlert.pdf"));
            }
            FileStream fs = new FileStream(Server.MapPath("~/EMANAGERBLOB/LPSQE/SafetyAlert/SafetyAlert.pdf"), FileMode.Create);
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
    }
    public void SetPageControlVisibility(string Mode)
    {
        if (Mode == "A")
        {
            btnEdit.Visible = false;
            btnSave.Visible = true;
            tblNotify.Visible = false;
        }
        else
        {
            btnEdit.Visible = true && Auth.isEdit;
            tblNotify.Visible = true;
            btnSave.Visible = false;
        }
    }
    public void ShowNotifiedData()
    {
        tblNotify.Visible = true;
        string sql = "select (Select FirstName+' '+LastName from dbo.UserLogin where LoginID=SA.NotifiedBy)NotifiedBy,replace(convert(varchar,NotifiedOn ,106),' ','-')NotifiedOn,Topic   from SA_SafetyAlert SA where SA.SAID=" + SAID + "";
        DataSet DS = Budget.getTable(sql);
        if (DS != null)
        {
            if (DS.Tables.Count > 0)
            {
                if (DS.Tables[0].Rows.Count > 0)
                {
                    if (DS.Tables[0].Rows[0]["NotifiedBy"].ToString() != "")
                    {
                        ViewState.Add("vTopic", DS.Tables[0].Rows[0]["Topic"].ToString());
                        btnEdit.Visible = false;
                        lblNotifyBy.Visible = true;
                        lblNotifyOn.Visible = true;
                        btnNotify.Visible = false;

                        lblNotifyByDB.Text = DS.Tables[0].Rows[0]["NotifiedBy"].ToString();
                        lblNotifyOnDB.Text = DS.Tables[0].Rows[0]["NotifiedOn"].ToString();
                    }
                    else
                    {
                        btnEdit.Visible = true;
                        lblNotifyBy.Visible = false;
                        lblNotifyOn.Visible = false;
                        btnNotify.Visible = true;
                    }
                }
            }
        }
    }
}

