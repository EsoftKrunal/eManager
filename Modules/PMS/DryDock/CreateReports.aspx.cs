using System;
using System.Data;
using System.Data.SqlClient;
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

public partial class DryDock_CreateReports : System.Web.UI.Page
{
    static Random R = new Random();
    DataSet ds = new DataSet();
    DataRow dr;
    
    int intLogin_Id = 0;
    public int intInspDueId = 0;

    public string strInsp_Status
    {
        set { ViewState["strInsp_Status"] = value; }
        get { return ViewState["strInsp_Status"].ToString(); }
    }
    public string SNO
    {
        set { ViewState["SNO"] = value; }
        get { return ViewState["SNO"].ToString(); }
    }

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
        if (Session["loginid"] == null)
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Default.aspx';", true);
        }
        else
        {
            intLogin_Id = Convert.ToInt32(Session["loginid"].ToString());
        }
   
       if (!IsPostBack)
       {
   
           DocketId = Common.CastAsInt32(Request.QueryString["DocketId"]);
           if (DocketId > 0)
           {
               DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT RFQID FROM DD_Docket_RFQ_Master WHERE DOCKETID=" + DocketId + " AND STATUS='P'");
               if (dt.Rows.Count > 0)
               {
                   RFQId = Common.CastAsInt32(dt.Rows[0]["RFQId"]);

                   SNO = "";
                   Show_Header_Record();
                   BindJobCategory();
               }
           }



       }
    }
    //protected void ImportRecords()
    //{ 
    //     DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM InspReport_Main WHERE InspectionDueID=" + intInspDueId.ToString());
    //     if (dt.Rows.Count <= 0)
    //     {
    //         string str = "INSERT INTO InspReport_MAIN(INSPECTIONDUEID,SRNO,CONTENTHEADING,CONTENTTEXT,CREATEDBY,CREATEDON) " +
    //                      "SELECT " + intInspDueId.ToString() + ",SNO,MAINHEADING,'',1,GETDATE() FROM m_InspectionReportCategory WHERE INSID IN (SELECT INSPECTIONID FROM T_INSPECTIONDUE WHERE ID=" + intInspDueId.ToString() + ")";
    //         Common.Execute_Procedures_Select_ByQuery(str);
    //     }
    //}
    //---------------- NEW CODE
    protected void BindJobCategory()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT ( ROW_Number() OVER( Order By CatCode) + 1 ) AS Srno, CatId,CatCode,CatName,(SELECT COUNT(*) from [dbo].[DD_Docket_RFQ_Jobs_Planning] WHERE RFQID=" + RFQId + " AND DocketJobId IN (SELECT RR.DOCKETJOBID FROM DD_Docket_RFQ_Jobs RR WHERE RR.RFQID=" + RFQId + " AND RR.CATID=DD_JobCategory.CatId ) AND ISNULL(REPORT,0)=1) as NOR FROM DD_JobCategory ");
        DataRow dr=dt.NewRow();
        dr["Srno"] = "1";
        dr["CatId"] = 0;
        dr["CatCode"] = "";
        dr["CatName"] = "Executive Summary";
        dt.Rows.InsertAt(dr,0);
        rptQuestions.DataSource = dt;
        rptQuestions.DataBind();
    }
    protected void Select_Row(object sender, EventArgs e)
    {
        CatId = Common.CastAsInt32(((LinkButton)sender).CommandArgument);
        Show_Data();
        BindJobCategory();
        btn_Save.Enabled = true;
    }
    protected void Delete_Row(object sender, EventArgs e)
    {
        //SNO = ((ImageButton)(sender)).CommandArgument.ToString().Trim();
        //Common.Execute_Procedures_Select_ByQuery("DELETE FROM InspReport_Child WHERE InspectionDueID=" + intInspDueId + " AND SrNo='" + SNO + "'");
        //Common.Execute_Procedures_Select_ByQuery("DELETE FROM InspReport_Main WHERE InspectionDueID=" + intInspDueId + " AND SrNo='" + SNO + "'");
        //SNO = "";
        //BindRecords();
        //Show_Data();
    }
    
    protected void Show_Data()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DD_DocketReport WHERE DocketId = " + DocketId + " AND CatId =" + CatId);
        if (dt.Rows.Count > 0)
        {
            txtDescr.Text = dt.Rows[0]["ContentText"].ToString();
            //MainId = Common.CastAsInt32(dt.Rows[0]["Id"]);
            btnUploadMore.Enabled = true;
        }
        else
        {
            txtDescr.Text = "";
            //MainId = 0;
            btnUploadMore.Enabled = false;
        }
        Show_Data_Details();
    }
    protected void Show_Data_Details()
    {
        string SQL = "SELECT I.*, ('~/'+FileName) AS FPath, " +
                     "(SELECT COUNT(*) FROM DD_DocketReportImages WHERE DocketId =1 AND CatId =1) AS TotalImages, " +
                     "'Picture '+CAST(Row_Number() over (ORDER BY DocketId) AS VARCHAR)+' : ' AS PicNumber, " +
                     "'Caption '+CAST(Row_Number() over (ORDER BY DocketId) AS VARCHAR)+' : ' AS CapNumber  " +
                     "FROM DD_DocketReportImages I WHERE DocketId = " + DocketId + " AND CatId =" + CatId;
        try
        {
            DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
            if (dt.Rows.Count > 0)
            {
                DataList1.DataSource = dt;
                DataList1.DataBind();
            }
            else
            {
                DataList1.DataSource = null;
                DataList1.DataBind();
            }
        }
        catch
        {
        }
    }
    //---------------- NEW CODE
    protected void Show_Header_Record()
    {
        try
        {
            DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT D.*, (SELECT VesselCode + ' - ' + VesselName from dbo.vessel Where VesselId =D.VesselId ) AS Vessel,Status,ReportNotifyOn,ReportApprovedOn,FinalPublishedOn FROM DD_DocketMaster D WHERE DocketId=" + DocketId);
            foreach (DataRow dr in dt.Rows)
            {
                txtDocketNo.Text = dr["DocketNo"].ToString();
                txtStartDate.Text = Common.ToDateString(dr["StartDate"]);
                txtEndDate.Text = Common.ToDateString(dr["EndDate"]);
                txtVesselName.Text = dr["Vessel"].ToString();
                string Status = dr["Status"].ToString();
                
                //----------------------------
                btnNotify.Visible = false;
                btnApprove.Visible = false;
                btn_Publish.Visible = false;

                DataTable dtPosition = Common.Execute_Procedures_Select_ByQuery("select position from dbo.Hr_PersonalDetails where userid=" + Session["loginid"].ToString());
                DataTable dtPublishCount = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DD_Docket_Publish_History WHERE DOCKETID=" + DocketId);

                int PositionId = 0;
                if (dtPosition.Rows.Count > 0)
                {
                    PositionId = Common.CastAsInt32(dtPosition.Rows[0][0]);
                }

                bool GM = (PositionId == 4 || PositionId == 1 || PositionId == 89);


                btnNotify.Visible = (Convert.IsDBNull(dt.Rows[0]["ReportApprovedOn"])) && (Status == "E");
                btnApprove.Visible = Convert.IsDBNull(dt.Rows[0]["ReportApprovedOn"]) && (!(Convert.IsDBNull(dt.Rows[0]["ReportNotifyOn"]))) && GM;
//                btn_Publish.Visible = Convert.IsDBNull(dt.Rows[0]["FinalPublishedOn"]) && (!(Convert.IsDBNull(dt.Rows[0]["ReportApprovedOn"])));

 	        DataTable dtpublish = Common.Execute_Procedures_Select_ByQuery("select * from dbo.DD_Reports where ddno LIKE '" + txtDocketNo.Text +"%'");
           

                btn_Publish.Visible = (Status != "C");;


            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void btnDel_Click(object sender,EventArgs e)
    {
        ImageButton btn = ((ImageButton)(sender));
        string strFilePath = "~\\UploadFiles\\" + btn.Attributes["filename"];

        if (File.Exists(Server.MapPath(strFilePath)))
        {
            File.Delete(Server.MapPath(strFilePath));
        }
        Common.Execute_Procedures_Select_ByQuery("DELETE FROM DD_DocketReportImages WHERE TABLEID=" + btn.CommandArgument);
        Show_Data_Details();
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtDescr.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "alert('Please enter description.');", true);
                txtDescr.Focus();
                return;
            }

            Common.Set_Procedures("DBO.DD_InsertUpdateDocketReport");
            Common.Set_ParameterLength(4);
            Common.Set_Parameters(new MyParameter("@DocketId", DocketId),                                  
                                  new MyParameter("@CatId", CatId),
                                  new MyParameter("@ContentText", txtDescr.Text.Trim()),
                                  new MyParameter("@CreatedBy", Session["loginid"].ToString())
                                  );
            DataSet ds = new DataSet();
            if (Common.Execute_Procedures_IUD(ds))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "alert('Added/ Updated successfully.');", true);
                btnUploadMore.Enabled = true;
            }
            else
            {
                lblmessage.Text = Common.ErrMsg.ToString();
            }            
        }
        catch (Exception ex)
        {
            lblmessage.Text = ex.Message.ToString();
        }
    }
    public bool chk_FileExtension(string str)
    {
        string extension = str;
        string MIMEType = null;
        switch (extension)
        {
            case ".jpg":
                return true;
            default:
                return false;
                break;
        }
    }
    protected void rpt_OnDataBound(object sender, RepeaterItemEventArgs e)
    {
        ((FileUpload)e.Item.FindControl("flpupld")).Attributes.Add("onchange", "document.getElementById('" + ((TextBox)e.Item.FindControl("txt_Caption2")).ClientID + "').value=GetFileName(this.value);");
    }
    protected void btnPost_Click(object sender, EventArgs e)
    {
        Show_Data_Details();
    }
    private class MyPageEventHandler : PdfPageEventHelper
    {
        public override void OnEndPage(PdfWriter writer, Document document)
        {
            //iTextSharp.text.Table tb_header = new iTextSharp.text.Table(1);
            //iTextSharp.text.Cell c1 = new Cell();
        }
    }
    // Publish
    protected void btnUploadMore_Click(object sender, EventArgs e)
    {
        string[] Files = System.IO.Directory.GetFiles(Server.MapPath("~/Temp"));
        foreach (string fl in Files)
        {
            System.IO.File.Delete(fl);
        }

        dvUploadFiles.Visible = true;
        frmUpload.Attributes.Add("src", "../UploadFiles.aspx?DocketId=" + DocketId + "&CatId=" + CatId);
    }
    protected void btnClose_OnClick(object sender, EventArgs e)
    {
        dvUploadFiles.Visible = false;
        frmUpload.Attributes.Add("src", "");
        Show_Data_Details();
    }
    protected void btn_Publish_Click(object sender, EventArgs e)
    {
        //-------------------------------------------------------------------------------
        DataTable dt_Master = Common.Execute_Procedures_Select_ByQuery("SELECT RFQNO,EXECFROM,EXECTO,VESSELNAME,D.VESSELID,(SELECT PORTNAME FROM [dbo].[DD_YardMaster] YM WHERE YM.YARDID=R.YARDID) AS YARDNAME FROM DD_Docket_RFQ_Master R  " +
                                                                        "INNER JOIN DD_DOCKETMASTER D ON R.DOCKETID=D.DOCKETID " +
                                                                        "INNER JOIN DBO.VESSEL V ON V.VESSELID=D.VESSELID  " +
                                                                        "WHERE RFQID=" + RFQId);
        //-------------------------------------------------------------------------------
        if (PublishToPDF(dt_Master))
        {
            string strpath = Server.MapPath("~\\DryDock\\publish.pdf");

            String connString;
            string filename = "";
            //ReportDocument repDoc = getReportDocument();
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            try
            {
                
                connString = ConfigurationManager.ConnectionStrings["eMANAGER"].ToString();
                SqlConnection myConnection = new SqlConnection(connString);
                SqlCommand myCommand = new SqlCommand("MW_InsertDDreports", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter DDId = new SqlParameter("@DDId", SqlDbType.Int);
                SqlParameter VesselId = new SqlParameter("@VesselId", SqlDbType.Int);
                SqlParameter DDNo = new SqlParameter("@DDNo", SqlDbType.VarChar);
                SqlParameter DoneDate = new SqlParameter("@DoneDate", SqlDbType.VarChar);
                SqlParameter PortDone = new SqlParameter("@PortDone", SqlDbType.VarChar);
                SqlParameter FileName = new SqlParameter("@FileName", SqlDbType.VarChar);
                DDId.Value = RFQId;
                VesselId.Value = dt_Master.Rows[0]["VESSELID"].ToString();
                DDNo.Value = dt_Master.Rows[0]["RFQNO"].ToString();
                DoneDate.Value = Common.ToDateString(dt_Master.Rows[0]["EXECTO"]);
                PortDone.Value = dt_Master.Rows[0]["YARDNAME"].ToString();
                string strDDNO = dt_Master.Rows[0]["RFQNO"].ToString().Replace("/", "-") + ".pdf";
                FileName.Value = strDDNO;

                myCommand.Parameters.Add(DDId);
                myCommand.Parameters.Add(VesselId);
                myCommand.Parameters.Add(DDNo);
                myCommand.Parameters.Add(DoneDate);
                myCommand.Parameters.Add(PortDone);
                myCommand.Parameters.Add(FileName);
                myConnection.Open();
                myCommand.ExecuteNonQuery();
                myConnection.Close();
                
                //-----------  UPLOADING FILE

                string newpath = ConfigurationManager.AppSettings["InspectionsPath"].ToString() + strDDNO;
                if (System.IO.File.Exists(newpath))
                    System.IO.File.Delete(newpath);

                System.IO.File.Copy(strpath, newpath);
            }
            catch (Exception ex)
            {
                lblmessage.Text = "File Created. But unable to publis. Error :" + ex.Message;
                ex = null;
            }

            Common.Execute_Procedures_Select_ByQuery("UPDATE DD_DOCKETMASTER SET FinalPublishedOn=GETDATE() WHERE DOCKETID=" + DocketId);
   
            
            lblmessage.Text = "Published Successfully.";
        }
        else
        {

        }
    }
    private bool PublishToPDF(DataTable DD_Master)
    {
        try
        {
            DataRow dr = DD_Master.Rows[0];
            string imagePath = ConfigurationManager.AppSettings["VslImgLink"].ToString() + "vessel_" + dr["VESSELID"].ToString() + ".jpg";
            if (!File.Exists(imagePath))
            {
                imagePath = ConfigurationManager.AppSettings["VslImgLink"].ToString() + "NoImage.jpg";
            }


            Document document = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4, 10, 10, 10, 10);

            

            System.IO.MemoryStream msReport = new System.IO.MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(document, msReport);
            document.AddAuthor("eMANAGER");
            document.AddSubject("DryDock Report");
            iTextSharp.text.Image logoImg = default(iTextSharp.text.Image);
            logoImg = iTextSharp.text.Image.GetInstance(Server.MapPath("~\\Images\\Logo\\Logo.png"));
            Chunk chk = new Chunk(logoImg, 0, 0, true);
    
            PdfPTable tb_header = new PdfPTable(1);
            tb_header.SplitLate = false;
            tb_header.SplitRows = true;
            float[] wsCom = { 100 };
            //float[] wsCom80 = { 80 };
            float[] wsCom5050 = { 50, 50 };
            tb_header.SetWidths(wsCom);
            
            PdfPCell c1 = new PdfPCell();
            c1.HorizontalAlignment = Element.ALIGN_LEFT;
            c1.AddElement(chk);
            c1.Border = Rectangle.NO_BORDER;
            tb_header.AddCell(c1);

            Phrase p2 = new Phrase();
            p2.Add(new Phrase("DryDock Report " + "\n" + "\n", FontFactory.GetFont("ARIAL", 18, iTextSharp.text.Font.BOLD)));
            PdfPCell c2 = new PdfPCell(p2);
            c2.Border = Rectangle.NO_BORDER;
            c2.HorizontalAlignment = Element.ALIGN_CENTER;
            tb_header.AddCell(c2);

    
            document.Open();
            document.Add(tb_header);

            // ---------------------- FRONT PAGE -------------------------------------------

            PdfPTable tbF = new PdfPTable(1);
            tbF.SplitLate = false;
            tbF.SplitRows = true;
            tbF.SetWidths(wsCom);
            tbF.DefaultCell.Border = Rectangle.NO_BORDER;
        
            iTextSharp.text.Image Img1 = default(iTextSharp.text.Image);
            Img1 = iTextSharp.text.Image.GetInstance(imagePath);
            Img1.ScaleAbsoluteWidth(500);
            Img1.ScaleAbsoluteHeight(400);
            PdfPCell tcImg1 = new PdfPCell(Img1);
            tcImg1.Border = iTextSharp.text.Rectangle.NO_BORDER;
            tbF.AddCell(tcImg1);


            iTextSharp.text.Font fCapText_11 = FontFactory.GetFont("ARIAL", 11, iTextSharp.text.Font.BOLD);
            iTextSharp.text.Font fCapText_10 = FontFactory.GetFont("ARIAL", 10, iTextSharp.text.Font.NORMAL);

            document.Add(tbF);

            //-------------------------------------------

            PdfPTable tbM = new PdfPTable(2);
            tbM.SplitLate = false;
            tbM.SplitRows = true;

            tbM.SetWidths(wsCom5050);
            tbM.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell tc = new PdfPCell(new Phrase("Vessel Name : ", fCapText_11));
            tbM.AddCell(tc);

            PdfPCell tc1 = new PdfPCell(new Phrase(dr["Vesselname"].ToString(), fCapText_11));
            tbM.AddCell(tc1);

            PdfPCell tcR1 = new PdfPCell(new Phrase("DD # : ", fCapText_11));
            tbM.AddCell(tcR1);

            PdfPCell tcR11 = new PdfPCell(new Phrase(dr["RFQNO"].ToString(), fCapText_11));
            tbM.AddCell(tcR11);

            PdfPCell tcR33 = new PdfPCell(new Phrase("FROM DATE : ", fCapText_11));
            tbM.AddCell(tcR33);

            PdfPCell tcR33a = new PdfPCell(new Phrase(Common.ToDateString(dr["EXECFROM"]), fCapText_11));
            tbM.AddCell(tcR33a);

            PdfPCell tcR3 = new PdfPCell(new Phrase("TO DATE : ", fCapText_11));
            tbM.AddCell(tcR3);

            PdfPCell tcR31 = new PdfPCell(new Phrase(Common.ToDateString(dr["EXECTO"]), fCapText_11));
            tbM.AddCell(tcR31);

            PdfPCell tcR4 = new PdfPCell(new Phrase("PLACE", fCapText_11));
            tbM.AddCell(tcR4);

            PdfPCell tcR41 = new PdfPCell(new Phrase(dr["YARDNAME"].ToString(), fCapText_11));
            tbM.AddCell(tcR41);

            //PdfPCell tcR5 = new PdfPCell(new Phrase("DATE OF REPORT :", fCapText_11));
            //tbM.AddCell(tcR5);

            //PdfPCell tcR51 = new PdfPCell(new Phrase(Common.ToDateString(DateTime.Today), fCapText_11));
            // tbM.AddCell(tcR51);


            document.Add(tbM);
            document.Add(new Phrase("\n", FontFactory.GetFont("ARIAL", 12, iTextSharp.text.Font.BOLD)));

            // -----------------------------------------------------------------------------
            

            //'-----------------------------------//'-----------------------------------//'-----------------------------------
            //string strDetails = "SELECT MDId,SrNo,Title,Descr FROM MortorDetails WHERE MID = " + MID;
            string strCats = "SELECT DISTINCT CATID,CATCODE,CATNAME,(SELECT ContentText FROM DD_DocketReport DR WHERE DR.DOCKETID=DJ.DOCKETID AND DR.CATID=DJ.CATID) AS ContentText FROM [dbo].[DD_Docket_RFQ_Jobs] DJ WHERE DOCKETID=" + DocketId + " ORDER BY CATCODE";
            DataTable dt11 = Common.Execute_Procedures_Select_ByQuery("SELECT ContentText FROM DD_DocketReport DR WHERE DR.DOCKETID=" + DocketId + " AND DR.CATID=0");
            DataTable dtCats = Common.Execute_Procedures_Select_ByQuery(strCats);
            DataRow drNew=dtCats.NewRow();
            drNew["CATID"] = 0;
            drNew["CATCODE"] = "";
            drNew["CATNAME"] = "Executive Summary";
            if(dt11.Rows.Count >0)
                drNew["ContentText"] = dt11.Rows[0][0].ToString();
            else
                drNew["ContentText"] = "";
            dtCats.Rows.InsertAt(drNew, 0);
            foreach (DataRow drc in dtCats.Rows)
            {
                document.NewPage();

                PdfPTable tbC1 = new PdfPTable(1);
                tbC1.SplitLate = false;
                tbC1.SplitRows = true;

                tbC1.SetWidths(wsCom);
                tbC1.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;

                PdfPCell tcC11 = new PdfPCell(new Phrase(drc["CATCODE"].ToString() + " : " + drc["CATNAME"].ToString(), fCapText_11));
                tbC1.AddCell(tcC11);

                PdfPCell tcC12 = new PdfPCell(new Phrase(drc["ContentText"].ToString(), fCapText_10));
                tbC1.AddCell(tcC12);

                //Content table
                string strImages = "SELECT CAPTION,FILENAME FROM DD_DocketReportImages WHERE DOCKETID=" + DocketId + " AND CATID=" + drc["CATID"].ToString() + " ORDER BY CATID,TABLEID";
                DataTable dtImages = Common.Execute_Procedures_Select_ByQuery(strImages);

                PdfPTable tbImages = new PdfPTable(2);
                //tbImages.SplitLate = false;
                //tbImages.SplitRows = true;
                tbImages.SetWidths(wsCom5050);

                for (int i = 0; i <= dtImages.Rows.Count - 1; i++)
                {
                   
                    PdfPTable tbImage = new PdfPTable(1);
                    tbImage.SetWidths(wsCom);
                    tbImage.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        
                    PdfPCell cell_tbImage = new PdfPCell();
                    cell_tbImage.HorizontalAlignment = Element.ALIGN_CENTER;

                    cell_tbImage.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    cell_tbImage.Border = Rectangle.NO_BORDER;
                    iTextSharp.text.Image Img11 = default(iTextSharp.text.Image);
                    Img11 = iTextSharp.text.Image.GetInstance(Server.MapPath("~\\DryDock\\UploadFiles\\" + dtImages.Rows[i]["FILENAME"].ToString()));
                    Img11.ScaleAbsolute(200f, 200f);
                    Chunk chk1 = new Chunk(Img11, 0, 0, true);
                    cell_tbImage.AddElement(chk1);
                    tbImage.AddCell(cell_tbImage);

                    PdfPCell cell1_tbImage = new PdfPCell(new Phrase(dtImages.Rows[i]["CAPTION"].ToString()));
                    cell1_tbImage.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    cell1_tbImage.Border = Rectangle.NO_BORDER;
                    tbImage.AddCell(cell1_tbImage);

                    PdfPCell rtt2 = new PdfPCell(tbImage);
                    tbImages.AddCell(rtt2);
                }
                if (dtImages.Rows.Count % 2 != 0)
                {
                    PdfPCell cell_blank = new PdfPCell(new Phrase(""));
                    tbImages.AddCell(cell_blank);
                }

                //PdfPCell tcImages = new PdfPCell(tbImages);
                //tbC1.AddCell(tcImages);

                document.Add(tbC1);
                document.Add(tbImages);
            }
            document.Close();

            //-----------------------------------------------------
            string strpath = "~\\DryDock\\publish.pdf";
            if (File.Exists(Server.MapPath(strpath)))
            {
                File.Delete(Server.MapPath(strpath));
            }
            FileStream fs = new FileStream(Server.MapPath(strpath), FileMode.Create);
            byte[] bb = msReport.ToArray();
            fs.Write(bb, 0, bb.Length);
            fs.Flush();
            fs.Close();

            return true;

        }
        catch (System.Exception ex)
        {
            lblmessage.Text = ex.Message.ToString();
            return false;
        }
    }
    protected void tbnDownload_Click(object sender, EventArgs e)
    {
        //DataTable DT=Common.Execute_Procedures_Select_ByQuery("select FILEPATH from dbo.t_InspectionDocuments inner join dbo.t_inspectiondue on t_InspectionDocuments.DocumentName=t_inspectiondue.InspectionNo and t_InspectionDocuments.inspectiondueid=t_inspectiondue.id WHERE InspectionNo IN (SELECT DOCKETNO FROM DBO.DD_DOCKETMASTER WHERE DOCKETID=" + DocketId.ToString() + ")");
        DataTable DT=Common.Execute_Procedures_Select_ByQuery("select RFQNO from dbo.DD_Docket_RFQ_Master WHERE rfqid=" + RFQId.ToString());
        if (DT.Rows.Count > 0)
        {
            Response.WriteFile(ConfigurationManager.AppSettings["InspectionsPath"].ToString() + DT.Rows[0]["RFQNO"].ToString().Replace("/", "-") + ".pdf");
        }
    }
    protected void btn_Notify_Click(object sender, EventArgs e)
    {
        //-------------------
        int LoginId = Common.CastAsInt32(Session["loginid"].ToString());
        Common.Execute_Procedures_Select_ByQuery("UPDATE DD_DocketMaster SET ReportNotified=1,ReportNotifiedBy='" + Session["FullName"].ToString() + "',ReportNotifyOn=GETDATE() WHERE DOCKETID=" + DocketId.ToString());
        string[] ToAdd = { "asingh@energiossolutions.com", "asingh@energiossolutions.com" };
        //string[] ToAdd = { "asingh@energiossolutions.com", "asingh@energiossolutions.com" };
        DataTable dtEmail = Common.Execute_Procedures_Select_ByQuery("select Email from dbo.userlogin where loginid=" + LoginId);
        string selfemail = "";
        if (dtEmail.Rows.Count > 0)
            if (dtEmail.Rows[0][0].ToString().Trim() != "")
                selfemail = dtEmail.Rows[0][0].ToString();

        string[] CCAdd = { selfemail };
        string[] NoAdd = { };
        string error;
        EProjectCommon.SendeMail_MTM(LoginId, "emanager@energiossolutions.com", "emanager@energiossolutions.com", ToAdd, CCAdd, NoAdd, txtDocketNo.Text + " : DD Report Ready", "Dear Sir,<br/><br/>Please review DD Report for docket no. " + txtDocketNo.Text + ". If ok, please approve from the report module in PMS.<br/><br/>Thanks<br/>------", out error, "");
        //------------------------ SEND MAIL TO GM FOR DOCKET NOTIFY
        
        lblmessage.Text = "GM notified successfully";
    }
    protected void btn_Approve_Click(object sender, EventArgs e)
    {
        //-------------------
        Common.Execute_Procedures_Select_ByQuery("UPDATE DD_DocketMaster SET ReportApproved=1,ReportApporvedBy='" + Session["FullName"].ToString() + "',ReportApprovedOn=GETDATE() WHERE DOCKETID=" + DocketId.ToString());
        ////------------------------ SEND MAIL TO GM FOR DOCKET NOTIFY
        lblmessage.Text = "Approved successfully";
    }
}
