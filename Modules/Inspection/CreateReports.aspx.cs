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
using ShipSoft.CrewManager.BusinessLogicLayer;
using ShipSoft.CrewManager.BusinessObjects;
using ShipSoft.CrewManager.Operational;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;
using iTextSharp.text.pdf;
using iTextSharp.text;

public partial class Transactions_CreateReports : System.Web.UI.Page
{

    static Random R = new Random();
    Authority Auth;
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

    public int VesselID
    {
        set { ViewState["VesselID"] = value; }
        get { return Common.CastAsInt32(ViewState["VesselID"]); }
    }
    public int MainId
    {
        set { ViewState["MainId"] = value; }
        get { return Common.CastAsInt32(ViewState["MainId"]); }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        if (Session["loginid"] == null)
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Default.aspx';", true);
        }
        else
        {
            intLogin_Id = Convert.ToInt32(Session["loginid"].ToString());
        }
        //--------------------
        if (Session["Insp_Id"] != null)
        {
            intInspDueId = int.Parse(Session["Insp_Id"].ToString());
            hfd_INSPID.Value = intInspDueId.ToString();
            DataTable dt = Inspection_Planning.CheckInspectionStatus(int.Parse(Session["Insp_Id"].ToString()));
            if (dt.Rows.Count > 0)
            {
                strInsp_Status = dt.Rows[0]["Status"].ToString();
            }
        }
        try
        {
            //****Code to enable/disable Publish button
            //try
            //{
            //    if (Session["Insp_Id"] != null)
            //    {
            //        DataTable dtrec = Safety_Inspection.InsertSafetyInspDetails(intInspDueId, "", "", "", 0, "ById");
            //        if (dtrec.Rows.Count > 0)
            //        {
            //            DataTable dtst = Inspection_Planning.CheckInspectionStatus(int.Parse(Session["Insp_Id"].ToString()));
            //            if (dtst.Rows.Count > 0)
            //            {
            //                strInsp_Status = dtst.Rows[0]["Status"].ToString();
            //            }
            //            if ((strInsp_Status == "Closed") || (strInsp_Status == "Due"))
            //            {
            //                DataTable dtinsp = Inspection_Observation.CheckInspType(int.Parse(Session["Insp_Id"].ToString()));
            //                if (dtinsp.Rows.Count > 0)
            //                {
            //                    strInspType = dtinsp.Rows[0]["InspectionType"].ToString();
            //                }
            //                if (strInspType == "Internal")
            //                {
            //                    btn_Publish.Enabled = true;
            //                    btn_Publish.Enabled = Auth.isSecondApproval;
            //                }
            //                else
            //                {
            //                    btn_Publish.Enabled = false;
            //                }
            //            }
            //            else
            //                btn_Publish.Enabled = false;
            //        }
            //        else
            //            btn_Publish.Enabled = false;
            //    }
            //}
            //catch (Exception ex) { throw ex; }
            //************************************************************
        }
        catch { }
        if (!Page.IsPostBack)
        {
            //***********Check whether Crew Details have been updated on Observation or not******
            //DataTable dt65 = Safety_Inspection.CheckCrewDetails(Convert.ToInt32(Session["Insp_Id"].ToString()));
            //if (int.Parse(dt65.Rows[0][0].ToString()) == 0)
            //{
            //    lblmessage.Text = "Crew Details have not been updated on Observation. Can't proceed.";
            //    btnFiles.Enabled = false;
            //    btn_Add.Enabled = false;
            //    lnk_Edit.Enabled = false;
            //    btn_Save.Enabled = false;
            //    btn_Delete.Enabled = false;
            //    //btn_Print.Enabled = false;
            //    return;
            //}
            //else
            //{
            //    //btnFiles.Enabled = true;
            //    btn_Add.Enabled = true;
            //    lnk_Edit.Enabled = true;
            //    btn_Save.Enabled = true;
            //    btn_Delete.Enabled = true;
            //    //btn_Print.Enabled = true;
            //}
            //***********************************************************************************

            //******Accessing UserOnBehalf/Subordinate Right******
            //try
            //{
            //    if (Session["Insp_Id"] != null)
            //    {
            //        DataTable dt88 = Inspection_Planning.CheckInspectionStatus(int.Parse(Session["Insp_Id"].ToString()));
            //        if (dt88.Rows.Count > 0)
            //        {
            //            strInsp_Status = dt88.Rows[0]["Status"].ToString();
            //        }
            //        if ((strInsp_Status == "Due") || (strInsp_Status == "Closed"))
            //        {
            //            btn_Add.Enabled = false;
            //            lnk_Edit.Enabled = false;
            //            btn_Save.Enabled = false;
            //            btn_Delete.Enabled = false;
            //        }
            //        else
            //        {
            //            int useronbehalfauth = Alerts.UserOnBehalfRight(Convert.ToInt32(Session["loginid"].ToString()), Convert.ToInt32(Session["Insp_Id"].ToString()));
            //            if (useronbehalfauth <= 0)
            //            {
            //                btn_Add.Enabled = false;
            //                btn_Save.Enabled = false;
            //                lnk_Edit.Enabled = false;
            //                btn_Delete.Enabled = false;
            //            }
            //            else
            //            {
            //                btn_Add.Enabled = true;
            //                btn_Save.Enabled = true;
            //                lnk_Edit.Enabled = true;
            //                btn_Delete.Enabled = true;
            //            }
            //        }                    
            //    }
            //}
            //catch
            //{
            //    Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Default.aspx';", true);
            //}
            //****************************************************
        }
        //---------------- NEW CODE
        if (!IsPostBack)
        {
            // IMPORTING RECORDS FROM REGISTER
            ImportRecords();
            SNO = "";
            Show_Header_Record(intInspDueId);
            BindRecords();
        }
    }
    protected void ImportRecords()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM InspReport_Main WHERE InspectionDueID=" + intInspDueId.ToString());
        if (dt.Rows.Count <= 0)
        {
            string str = "INSERT INTO InspReport_MAIN(INSPECTIONDUEID,SRNO,CONTENTHEADING,CONTENTTEXT,CREATEDBY,CREATEDON) " +
                         "SELECT " + intInspDueId.ToString() + ",SNO,MAINHEADING,'',1,GETDATE() FROM m_InspectionReportCategory WHERE INSID IN (SELECT INSPECTIONID FROM T_INSPECTIONDUE WHERE ID=" + intInspDueId.ToString() + ")";
            Common.Execute_Procedures_Select_ByQuery(str);
        }
    }
    //---------------- NEW CODE
    protected void BindRecords()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM InspReport_Main WHERE InspectionDueID=" + intInspDueId + " ORDER BY cast(SRNO as int)");
        rptQuestions.DataSource = dt;
        rptQuestions.DataBind();

        DataTable dt1 = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.tbl_Inspection_Report_Notify N WHERE N.InspectionDueId=" + intInspDueId + " and NotofiedOn is not null");
        if (dt1.Rows.Count > 0)
        {
            lblNotify.Text = "Notified By /On : " + dt1.Rows[0]["NotifiedBy"].ToString() + "/" + Common.ToDateString(dt1.Rows[0]["NotofiedOn"]);
        }
    }
    protected void Select_Row(object sender, EventArgs e)
    {
        SNO = ((LinkButton)(sender)).CommandArgument.ToString().Trim();
        Show_Data();
        BindRecords();

        if ((strInsp_Status == "Due") || (strInsp_Status == "Closed"))
        {
            btn_Save.Enabled = false;
            btnUploadMore.Enabled = false;
        }
        else
        {
            btn_Save.Enabled = true;
            btnUploadMore.Enabled = true;
        }


    }
    protected void Delete_Row(object sender, EventArgs e)
    {
        SNO = ((ImageButton)(sender)).CommandArgument.ToString().Trim();
        Common.Execute_Procedures_Select_ByQuery("DELETE FROM InspReport_Child WHERE InspectionDueID=" + intInspDueId + " AND SrNo='" + SNO + "'");
        Common.Execute_Procedures_Select_ByQuery("DELETE FROM InspReport_Main WHERE InspectionDueID=" + intInspDueId + " AND SrNo='" + SNO + "'");
        SNO = "";
        BindRecords();
        Show_Data();
    }

    protected void Show_Data()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM InspReport_Main WHERE InspectionDueID=" + intInspDueId.ToString() + " AND SRNO='" + SNO + "'");
        if (dt.Rows.Count > 0)
        {
            txtDescr.Text = dt.Rows[0]["ContentText"].ToString();
            MainId = Common.CastAsInt32(dt.Rows[0]["Id"]);

        }
        else
        {
            txtDescr.Text = "";
            MainId = 0;
        }
        Show_Data_Details();
    }
    protected void Show_Data_Details()
    {
        try
        {
            DataTable dt43 = Safety_Inspection.InsertSafetyInspChildDetails(0, intInspDueId, SNO, "", "", 0, "ById");
            if (dt43.Rows.Count > 0)
            {
                DataList1.DataSource = dt43;
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
    protected void Show_Header_Record(int InspectionId)
    {
        try
        {
            DataTable dt1 = Inspection_TravelSchedule.SelectInspectionDetailsByInspId(InspectionId);
            DataTable DtSup = Common.Execute_Procedures_Select_ByQuery("SELECT SUPERINTENDENTID AS SUPID,FIRSTNAME + ' ' + LASTNAME AS SUPNAME FROM t_InspSupt INNER JOIN DBO.USERLOGIN UL ON UL.LOGINID=SUPERINTENDENTID WHERE INSPECTIONDUEID=" + InspectionId.ToString());
            if (DtSup.Rows.Count > 0)
            {
                txtSupName.Text = DtSup.Rows[0][1].ToString();
            }
            foreach (DataRow dr in dt1.Rows)
            {
                txtinspno.Text = dr["InspectionNo"].ToString();
                txtlastdone.Text = dr["DoneDt"].ToString();
                txtplannedport.Text = dr["PortDone"].ToString();

                HiddenField_VslId.Value = dr["VesselId"].ToString();
                HiddenField_InspName.Value = dr["Name"].ToString();
                HiddenField_DoneDt.Value = dr["DoneDt"].ToString();
                HiddenField_PortDone.Value = dr["PortDone"].ToString();
                HiddenField_StartDate.Value = dr["StartDate1"].ToString();

                VesselID = Common.CastAsInt32(dr["VesselID"].ToString());
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void btnDel_Click(object sender, EventArgs e)
    {
        ImageButton btn = ((ImageButton)(sender));
        string strFilePath = "~\\Modules\\Inspection\\UserUploadedDocuments\\Transaction_Reports\\" + btn.Attributes["filename"];

        if (File.Exists(Server.MapPath(strFilePath)))
        {
            File.Delete(Server.MapPath(strFilePath));
        }
        Budget.getTable("DELETE FROM InspReport_Child WHERE TABLEID=" + btn.CommandArgument);
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
            Common.Execute_Procedures_Select_ByQuery("UPDATE INSPREPORT_MAIN SET CONTENTTEXT='" + txtDescr.Text.Trim().Replace("'", "`") + "' WHERE ID=" + MainId.ToString());
            ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "alert('Updated successfully.');", true);
        }
        catch (Exception ex)
        {
            lblmessage.Text = ex.StackTrace.ToString();
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
    // Show Print Report 
    protected void btnPrint_OnClick(object sender, EventArgs e)
    {
        ExportToPDF();

        //FileUpload img = new FileUpload();
        //Byte[] imgByte = File.ReadAllBytes(Server.MapPath("../Report.pdf"));
        //Common.Set_Procedures("dbo.sp_MOTOR_PUBLISH");
        //Common.Set_ParameterLength(4);

        //Common.Set_Parameters(
        //    new MyParameter("@MID", MID),
        //    new MyParameter("@SUPTD", txtSupdtName.Text.Trim()),
        //    new MyParameter("@PREPBY", ""),
        //    new MyParameter("@REPORT", imgByte)

        //    );
        //DataSet ds = new DataSet();
        //bool res;
        //res = Common.Execute_Procedures_IUD(ds);

        //if (res)
        //{
        //    btn_Show_Click(sender, e);
        //    DivAckRecieve.Visible = false;
        //    txtSupdtName.Text = "";
        //    //txtPreparedBy.Text = "";
        //}
    }
    protected void btnPrintWithOutImage_OnClick(object sender, EventArgs e)
    {
        ExportToPDF_WithoutImage();
    }
    private void ExportToPDF_WithoutImage()
    {
        try
        {
            string strVelName = "SELECT Vesselname from dbo.Vessel VSL where VSL.VesselId=" + VesselID + " ";
            DataTable dtVesselName = Budget.getTable(strVelName).Tables[0];

            string imagePath = ConfigurationManager.AppSettings["VslImgLink"].ToString() + "vessel_" + VesselID.ToString() + ".jpg";

            if (!File.Exists(imagePath))
            {
                imagePath = ConfigurationManager.AppSettings["VslImgLink"].ToString() + "NoImage.jpg";
            }


            Document document = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4, 10, 10, 10, 10);

            System.IO.MemoryStream msReport = new System.IO.MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(document, msReport);
            document.AddAuthor(" ");
            document.AddSubject("Monthly Owner’s Technical & Operating Report (MOTOR)");
            //'Adding Header in Document
            iTextSharp.text.Image logoImg = default(iTextSharp.text.Image);
          //  logoImg = iTextSharp.text.Image.GetInstance(Server.MapPath("~\\Images\\Logo\\MTMMLogo.jpg"));
            Chunk chk = new Chunk(logoImg, 0, 0, true);
            //Phrase p1 = new Phrase();
            //p1.Add(chk);

            iTextSharp.text.Table tb_header = new iTextSharp.text.Table(1);
            tb_header.Width = 100;
            tb_header.Border = iTextSharp.text.Rectangle.NO_BORDER;
            tb_header.DefaultCellBorder = iTextSharp.text.Rectangle.NO_BORDER;
            tb_header.DefaultHorizontalAlignment = Element.ALIGN_LEFT;
            tb_header.BorderWidth = 0;
            tb_header.BorderColor = iTextSharp.text.Color.WHITE;
            tb_header.Cellspacing = 1;
            tb_header.Cellpadding = 1;

            Cell c1 = new Cell(chk);
            c1.HorizontalAlignment = Element.ALIGN_LEFT;
            tb_header.AddCell(c1);

            Phrase p2 = new Phrase();
            p2.Add(new Phrase("Technical Inspection " + "\n" + "\n", FontFactory.GetFont("ARIAL", 18, iTextSharp.text.Font.BOLD)));
            Cell c2 = new Cell(p2);
            c2.HorizontalAlignment = Element.ALIGN_CENTER;
            tb_header.AddCell(c2);

            HeaderFooter header = new HeaderFooter(new Phrase(" \n Technical Inspection -" + txtplannedport.Text + " - " + txtlastdone.Text + "                                                     FORM NO : G 129A"), false);
            document.Header = header;

            header.Border = iTextSharp.text.Rectangle.NO_BORDER;
            string foot_Txt = "";
            foot_Txt = foot_Txt + "                                                                                                                ";
            foot_Txt = foot_Txt + "                                                                                                                ";
            foot_Txt = foot_Txt + "";
            HeaderFooter footer = new HeaderFooter(new Phrase(foot_Txt, FontFactory.GetFont("VERDANA", 6, iTextSharp.text.Color.DARK_GRAY)), true);
            footer.Border = iTextSharp.text.Rectangle.NO_BORDER;
            footer.Alignment = Element.ALIGN_LEFT;
            document.Footer = footer;
            //'-----------------------------------
            document.Open();
            document.Add(tb_header);

            // ---------------------- FRONT PAGE -------------------------------------------

            iTextSharp.text.Table tbF = new iTextSharp.text.Table(1);
            tbF.Width = 100;
            tbF.Alignment = Element.ALIGN_CENTER;
            tbF.DefaultHorizontalAlignment = Element.ALIGN_CENTER;
            tbF.Cellspacing = 1;
            tbF.Cellpadding = 1;
            tbF.Border = iTextSharp.text.Rectangle.NO_BORDER;


            iTextSharp.text.Image Img1 = default(iTextSharp.text.Image);
            Img1 = iTextSharp.text.Image.GetInstance(imagePath);
            Img1.ScaleAbsoluteWidth(500);
            Img1.ScaleAbsoluteHeight(400);
            Cell tcImg1 = new Cell(Img1);
            tcImg1.Border = iTextSharp.text.Rectangle.NO_BORDER;
            tbF.AddCell(tcImg1);


            iTextSharp.text.Font fCapText_11 = FontFactory.GetFont("ARIAL", 11, iTextSharp.text.Font.BOLD);
            iTextSharp.text.Font fCapText_10 = FontFactory.GetFont("ARIAL", 10, iTextSharp.text.Font.NORMAL);

            iTextSharp.text.Table tbM = new iTextSharp.text.Table(2);
            tbM.Width = 80;
            tbM.Alignment = Element.ALIGN_CENTER;
            tbM.DefaultHorizontalAlignment = Element.ALIGN_RIGHT;
            tbM.Cellspacing = 1;
            tbM.Cellpadding = 1;
            tbM.BorderWidth = 1;
            tbM.BorderColor = iTextSharp.text.Color.BLACK;

            Cell tc = new Cell(new Phrase("Vessel Name : ", fCapText_11));
            tbM.AddCell(tc);


            Cell tc1 = new Cell(new Phrase(dtVesselName.Rows[0]["Vesselname"].ToString(), fCapText_11));
            tc1.HorizontalAlignment = Element.ALIGN_LEFT;
            tbM.AddCell(tc1);

            Cell tcR1 = new Cell(new Phrase("INSP. # : ", fCapText_11));
            tbM.AddCell(tcR1);

            Cell tcR11 = new Cell(new Phrase(txtinspno.Text, fCapText_11));
            tcR11.HorizontalAlignment = Element.ALIGN_LEFT;
            tbM.AddCell(tcR11);

            Cell tcR33 = new Cell(new Phrase("INSP. START DT. : ", fCapText_11));
            tbM.AddCell(tcR33);
            Cell tcR33a = new Cell(new Phrase(HiddenField_StartDate.Value, fCapText_11));
            tcR33a.HorizontalAlignment = Element.ALIGN_LEFT;
            tbM.AddCell(tcR33a);


            Cell tcR3 = new Cell(new Phrase("INSP. DONE DT. : ", fCapText_11));
            tbM.AddCell(tcR3);
            Cell tcR31 = new Cell(new Phrase(txtlastdone.Text, fCapText_11));
            tcR31.HorizontalAlignment = Element.ALIGN_LEFT;
            tbM.AddCell(tcR31);

            Cell tcR7 = new Cell(new Phrase("PLACE : ", fCapText_11));
            tbM.AddCell(tcR7);
            Cell tcR71 = new Cell(new Phrase(txtplannedport.Text, fCapText_11));
            tcR71.HorizontalAlignment = Element.ALIGN_LEFT;
            tbM.AddCell(tcR71);

            //------------------------
            Cell tcR8 = new Cell(new Phrase("Date of Report : ", fCapText_11));
            tbM.AddCell(tcR8);
            Cell tcR8a = new Cell(new Phrase(DateTime.Now.ToString("dd-MMM-yyyy"), fCapText_11));
            tcR8a.HorizontalAlignment = Element.ALIGN_LEFT;
            tbM.AddCell(tcR8a);

            Cell tcR5 = new Cell(new Phrase("Inspector's Name : ", fCapText_11));
            tbM.AddCell(tcR5);
            Cell tcR51 = new Cell(new Phrase(txtSupName.Text, fCapText_11));
            tcR51.HorizontalAlignment = Element.ALIGN_LEFT;
            tbM.AddCell(tcR51);

            Cell tccont = new Cell(tbM);
            tccont.Border = iTextSharp.text.Rectangle.NO_BORDER;
            tbF.AddCell(tccont);



            document.Add(tbF);
            document.Add(new Phrase("\n", FontFactory.GetFont("ARIAL", 12, iTextSharp.text.Font.BOLD)));

            // -----------------------------------------------------------------------------
            document.NewPage();


            //'-----------------------------------//'-----------------------------------//'-----------------------------------
            //string strDetails = "SELECT MDId,SrNo,Title,Descr FROM MortorDetails WHERE MID = " + MID;
            string strInsp = "SELECT Id,InspectionDueId,SrNo,ContentHeading,ContentText FROM InspReport_Main WHERE InspectionDueId=" + intInspDueId + " order by cast(srno as float)";
            //Content table

            DataTable dtInsp = Budget.getTable(strInsp).Tables[0];
            for (int i = 0; i <= dtInsp.Rows.Count - 1; i++)
            {
                #region --------- Start Main Table -------
                iTextSharp.text.Table tb1 = new iTextSharp.text.Table(1);
                tb1.Width = 100;
                tb1.Alignment = Element.ALIGN_CENTER;
                tb1.BorderWidth = 1;
                tb1.DefaultHorizontalAlignment = Element.ALIGN_LEFT;
                tb1.BorderColor = iTextSharp.text.Color.BLACK;
                tb1.Cellspacing = 1;
                tb1.Cellpadding = 1;
                #endregion
                AddTitle(dtInsp.Rows[i]["SrNo"].ToString() + ". " + dtInsp.Rows[i]["ContentHeading"].ToString(), tb1);
                AddContent(dtInsp.Rows[i]["ContentText"].ToString(), tb1);

                //string strImages = "SELECT * FROM InspReport_Child where InspectionDueId = " + dtInsp.Rows[i]["InspectionDueId"].ToString() + " AND SrNo = " + dtInsp.Rows[i]["SrNo"].ToString() + " ";
                //DataTable dtImages = Budget.getTable(strImages).Tables[0];

                ////------------- Adding Images
                //#region --------- Start Image Table -------
                //iTextSharp.text.Table tbInner = new iTextSharp.text.Table(2);
                //tbInner.Width = 100;
                //float[] ws1 = { 49, 49 };
                //tbInner.Widths = ws1;

                //tbInner.Alignment = Element.ALIGN_CENTER;
                //tbInner.BorderWidth = 0;
                //tbInner.DefaultCellBorderWidth = 0;
                //tbInner.DefaultCellBorderColor = iTextSharp.text.Color.WHITE;

                //tbInner.DefaultHorizontalAlignment = Element.ALIGN_CENTER;
                //tbInner.BorderColor = iTextSharp.text.Color.WHITE;
                //tbInner.Cellspacing = 1;
                //tbInner.Cellpadding = 1;
                //#endregion
                ////-------------
                //for (int j = 0; j <= dtImages.Rows.Count - 1; j++)
                //{
                //    AddImage(dtImages.Rows[j]["FilePath"].ToString(), dtImages.Rows[j]["PicCaption"].ToString(), tbInner);
                //}
                //Cell tcImages = new Cell(tbInner);
                //tcImages.HorizontalAlignment = Element.ALIGN_CENTER;
                //tb1.AddCell(tcImages);
                //------------- Adding Images End

                document.Add(tb1);
                document.Add(new Phrase("\n", FontFactory.GetFont("ARIAL", 12, iTextSharp.text.Font.BOLD)));
            }


            //'-----------------------------------//'-----------------------------------//'-----------------------------------
            document.Close();

            //-----------------------------------------------------
            string inspno = "";
            DataTable dt9 = Common.Execute_Procedures_Select_ByQuery("SELECT INSPECTIONNO FROM T_INSPECTIONDUE WHERE ID=" + intInspDueId.ToString());
            if (dt9.Rows.Count > 0)
            {
                inspno = dt9.Rows[0][0].ToString();
            }

            string strname = inspno.Replace("/", "-") + ".pdf";
            string strpath = "~\\Modules\\Inspection\\UserUploadedDocuments\\Transactions\\" + strname;
            if (File.Exists(Server.MapPath(strpath)))
            {
                File.Delete(Server.MapPath(strpath));
            }
            FileStream fs = new FileStream(Server.MapPath(strpath), FileMode.Create);
            //-----------------------------------------------------
            //if (File.Exists(Server.MapPath("~\\ReportInsp.pdf")))
            //{
            //    File.Delete(Server.MapPath("~\\ReportInsp.pdf"));
            //}

            //FileStream fs = new FileStream(Server.MapPath("~\\ReportInsp.pdf"), FileMode.Create);
            byte[] bb = msReport.ToArray();
            fs.Write(bb, 0, bb.Length);
            fs.Flush();
            fs.Close();
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "a", "window.open('../Inspection/UserUploadedDocuments/Transactions/" + strname + "?" + R.NextDouble().ToString() + "');", true);
        }
        catch (System.Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "a", "alert('" + ex.Message.Replace("'", "`").Replace("\"", "`") + "');", true);
        }
    }
    private void ExportToPDF()
    {
        try
        {
            string strVelName = "SELECT Vesselname from dbo.Vessel VSL where VSL.VesselId=" + VesselID + " ";
            DataTable dtVesselName = Budget.getTable(strVelName).Tables[0];

            string imagePath = ConfigurationManager.AppSettings["VslImgLink"].ToString() + "vessel_" + VesselID.ToString() + ".jpg";

            if (!File.Exists(imagePath))
            {
                imagePath = ConfigurationManager.AppSettings["VslImgLink"].ToString() + "NoImage.jpg";
            }


            Document document = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4, 10, 10, 10, 10);

            System.IO.MemoryStream msReport = new System.IO.MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(document, msReport);
            document.AddAuthor("");
            document.AddSubject("Monthly Owner’s Technical & Operating Report (MOTOR)");
            //'Adding Header in Document
            iTextSharp.text.Image logoImg = default(iTextSharp.text.Image);
            //logoImg = iTextSharp.text.Image.GetInstance(Server.MapPath("~\\Images\\Logo\\MTMMLogo.jpg"));
            Chunk chk = new Chunk(logoImg, 0, 0, true);
            //Phrase p1 = new Phrase();
            //p1.Add(chk);

            iTextSharp.text.Table tb_header = new iTextSharp.text.Table(1);
            tb_header.Width = 100;
            tb_header.Border = iTextSharp.text.Rectangle.NO_BORDER;
            tb_header.DefaultCellBorder = iTextSharp.text.Rectangle.NO_BORDER;
            tb_header.DefaultHorizontalAlignment = Element.ALIGN_LEFT;
            tb_header.BorderWidth = 0;
            tb_header.BorderColor = iTextSharp.text.Color.WHITE;
            tb_header.Cellspacing = 1;
            tb_header.Cellpadding = 1;

            Cell c1 = new Cell(chk);
            c1.HorizontalAlignment = Element.ALIGN_LEFT;
            tb_header.AddCell(c1);

            Phrase p2 = new Phrase();
            p2.Add(new Phrase("Technical Inspection " + "\n" + "\n", FontFactory.GetFont("ARIAL", 18, iTextSharp.text.Font.BOLD)));
            Cell c2 = new Cell(p2);
            c2.HorizontalAlignment = Element.ALIGN_CENTER;
            tb_header.AddCell(c2);

            HeaderFooter header = new HeaderFooter(new Phrase(" \n Technical Inspection -" + txtplannedport.Text + " - " + txtlastdone.Text + ""), false);
            document.Header = header;

            header.Border = iTextSharp.text.Rectangle.NO_BORDER;
            string foot_Txt = "";
            foot_Txt = foot_Txt + "                                                                                                                ";
            foot_Txt = foot_Txt + "                                                                                                                ";
            foot_Txt = foot_Txt + "";
            HeaderFooter footer = new HeaderFooter(new Phrase(foot_Txt, FontFactory.GetFont("VERDANA", 6, iTextSharp.text.Color.DARK_GRAY)), true);
            footer.Border = iTextSharp.text.Rectangle.NO_BORDER;
            footer.Alignment = Element.ALIGN_LEFT;
            document.Footer = footer;
            //'-----------------------------------
            document.Open();
            document.Add(tb_header);

            // ---------------------- FRONT PAGE -------------------------------------------

            iTextSharp.text.Table tbF = new iTextSharp.text.Table(1);
            tbF.Width = 100;
            tbF.Alignment = Element.ALIGN_CENTER;
            tbF.DefaultHorizontalAlignment = Element.ALIGN_CENTER;
            tbF.Cellspacing = 1;
            tbF.Cellpadding = 1;
            tbF.Border = iTextSharp.text.Rectangle.NO_BORDER;


            iTextSharp.text.Image Img1 = default(iTextSharp.text.Image);
            Img1 = iTextSharp.text.Image.GetInstance(imagePath);
            Img1.ScaleAbsoluteWidth(500);
            Img1.ScaleAbsoluteHeight(400);
            Cell tcImg1 = new Cell(Img1);
            tcImg1.Border = iTextSharp.text.Rectangle.NO_BORDER;
            tbF.AddCell(tcImg1);


            iTextSharp.text.Font fCapText_11 = FontFactory.GetFont("ARIAL", 11, iTextSharp.text.Font.BOLD);
            iTextSharp.text.Font fCapText_10 = FontFactory.GetFont("ARIAL", 10, iTextSharp.text.Font.NORMAL);

            iTextSharp.text.Table tbM = new iTextSharp.text.Table(2);
            tbM.Width = 80;
            tbM.Alignment = Element.ALIGN_CENTER;
            tbM.DefaultHorizontalAlignment = Element.ALIGN_RIGHT;
            tbM.Cellspacing = 1;
            tbM.Cellpadding = 1;
            tbM.BorderWidth = 1;
            tbM.BorderColor = iTextSharp.text.Color.BLACK;

            Cell tc = new Cell(new Phrase("Vessel Name : ", fCapText_11));
            tbM.AddCell(tc);


            Cell tc1 = new Cell(new Phrase(dtVesselName.Rows[0]["Vesselname"].ToString(), fCapText_11));
            tc1.HorizontalAlignment = Element.ALIGN_LEFT;
            tbM.AddCell(tc1);

            Cell tcR1 = new Cell(new Phrase("INSP. # : ", fCapText_11));
            tbM.AddCell(tcR1);

            Cell tcR11 = new Cell(new Phrase(txtinspno.Text, fCapText_11));
            tcR11.HorizontalAlignment = Element.ALIGN_LEFT;
            tbM.AddCell(tcR11);

            Cell tcR33 = new Cell(new Phrase("INSP. START DT. : ", fCapText_11));
            tbM.AddCell(tcR33);
            Cell tcR33a = new Cell(new Phrase(HiddenField_StartDate.Value, fCapText_11));
            tcR33a.HorizontalAlignment = Element.ALIGN_LEFT;
            tbM.AddCell(tcR33a);


            Cell tcR3 = new Cell(new Phrase("INSP. DONE DT. : ", fCapText_11));
            tbM.AddCell(tcR3);
            Cell tcR31 = new Cell(new Phrase(txtlastdone.Text, fCapText_11));
            tcR31.HorizontalAlignment = Element.ALIGN_LEFT;
            tbM.AddCell(tcR31);

            Cell tcR7 = new Cell(new Phrase("PLACE : ", fCapText_11));
            tbM.AddCell(tcR7);
            Cell tcR71 = new Cell(new Phrase(txtplannedport.Text, fCapText_11));
            tcR71.HorizontalAlignment = Element.ALIGN_LEFT;
            tbM.AddCell(tcR71);

            //------------------------
            Cell tcR8 = new Cell(new Phrase("Date of Report : ", fCapText_11));
            tbM.AddCell(tcR8);
            Cell tcR8a = new Cell(new Phrase(DateTime.Now.ToString("dd-MMM-yyyy"), fCapText_11));
            tcR8a.HorizontalAlignment = Element.ALIGN_LEFT;
            tbM.AddCell(tcR8a);

            Cell tcR5 = new Cell(new Phrase("Inspector's Name : ", fCapText_11));
            tbM.AddCell(tcR5);
            Cell tcR51 = new Cell(new Phrase(txtSupName.Text, fCapText_11));
            tcR51.HorizontalAlignment = Element.ALIGN_LEFT;
            tbM.AddCell(tcR51);

            Cell tccont = new Cell(tbM);
            tccont.Border = iTextSharp.text.Rectangle.NO_BORDER;
            tbF.AddCell(tccont);



            document.Add(tbF);
            document.Add(new Phrase("\n", FontFactory.GetFont("ARIAL", 12, iTextSharp.text.Font.BOLD)));

            // -----------------------------------------------------------------------------
            document.NewPage();


            //'-----------------------------------//'-----------------------------------//'-----------------------------------
            //string strDetails = "SELECT MDId,SrNo,Title,Descr FROM MortorDetails WHERE MID = " + MID;
            string strInsp = "SELECT Id,InspectionDueId,SrNo,ContentHeading,ContentText FROM InspReport_Main WHERE InspectionDueId=" + intInspDueId + " order by cast(srno as float)";
            //Content table

            DataTable dtInsp = Budget.getTable(strInsp).Tables[0];
            for (int i = 0; i <= dtInsp.Rows.Count - 1; i++)
            {
                #region --------- Start Main Table -------
                iTextSharp.text.Table tb1 = new iTextSharp.text.Table(1);
                tb1.Width = 100;
                tb1.Alignment = Element.ALIGN_CENTER;
                tb1.BorderWidth = 1;
                tb1.DefaultHorizontalAlignment = Element.ALIGN_LEFT;
                tb1.BorderColor = iTextSharp.text.Color.BLACK;
                tb1.Cellspacing = 1;
                tb1.Cellpadding = 1;
                #endregion
                AddTitle(dtInsp.Rows[i]["SrNo"].ToString() + ". " + dtInsp.Rows[i]["ContentHeading"].ToString(), tb1);
                AddContent(dtInsp.Rows[i]["ContentText"].ToString(), tb1);

                string strImages = "SELECT * FROM InspReport_Child where InspectionDueId = " + dtInsp.Rows[i]["InspectionDueId"].ToString() + " AND SrNo = " + dtInsp.Rows[i]["SrNo"].ToString() + " ";
                DataTable dtImages = Budget.getTable(strImages).Tables[0];

                //------------- Adding Images
                #region --------- Start Image Table -------
                iTextSharp.text.Table tbInner = new iTextSharp.text.Table(2);
                tbInner.Width = 100;
                float[] ws1 = { 49, 49 };
                tbInner.Widths = ws1;

                tbInner.Alignment = Element.ALIGN_CENTER;
                tbInner.BorderWidth = 0;
                tbInner.DefaultCellBorderWidth = 0;
                tbInner.DefaultCellBorderColor = iTextSharp.text.Color.WHITE;

                tbInner.DefaultHorizontalAlignment = Element.ALIGN_CENTER;
                tbInner.BorderColor = iTextSharp.text.Color.WHITE;
                tbInner.Cellspacing = 1;
                tbInner.Cellpadding = 1;
                #endregion
                //-------------
                for (int j = 0; j <= dtImages.Rows.Count - 1; j++)
                {
                    AddImage(dtImages.Rows[j]["FilePath"].ToString(), dtImages.Rows[j]["PicCaption"].ToString(), tbInner);
                }
                Cell tcImages = new Cell(tbInner);
                tcImages.HorizontalAlignment = Element.ALIGN_CENTER;
                tb1.AddCell(tcImages);
                //------------- Adding Images End

                document.Add(tb1);
                document.Add(new Phrase("\n", FontFactory.GetFont("ARIAL", 12, iTextSharp.text.Font.BOLD)));
            }


            //'-----------------------------------//'-----------------------------------//'-----------------------------------
            document.Close();

            //-----------------------------------------------------
            string inspno = "";
            DataTable dt9 = Common.Execute_Procedures_Select_ByQuery("SELECT INSPECTIONNO FROM T_INSPECTIONDUE WHERE ID=" + intInspDueId.ToString());
            if (dt9.Rows.Count > 0)
            {
                inspno = dt9.Rows[0][0].ToString();
            }

            string strname = inspno.Replace("/", "-") + ".pdf";
            string strpath = "~\\Modules\\Inspection\\UserUploadedDocuments\\Transactions\\" + strname;
            if (File.Exists(Server.MapPath(strpath)))
            {
                File.Delete(Server.MapPath(strpath));
            }
            FileStream fs = new FileStream(Server.MapPath(strpath), FileMode.Create);
            //-----------------------------------------------------
            //if (File.Exists(Server.MapPath("~\\ReportInsp.pdf")))
            //{
            //    File.Delete(Server.MapPath("~\\ReportInsp.pdf"));
            //}

            //FileStream fs = new FileStream(Server.MapPath("~\\ReportInsp.pdf"), FileMode.Create);
            byte[] bb = msReport.ToArray();
            fs.Write(bb, 0, bb.Length);
            fs.Flush();
            fs.Close();
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "a", "window.open('../Inspection/UserUploadedDocuments/Transactions/" + strname + "?" + R.NextDouble().ToString() + "');", true);
        }
        catch (System.Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "a", "alert('" + ex.Message.Replace("'", "`").Replace("\"", "`") + "');", true);
        }
    }
    private void PublishToPDF()
    {
        try
        {
            string strVelName = "SELECT Vesselname from dbo.Vessel VSL where VSL.VesselId=" + VesselID + " ";
            DataTable dtVesselName = Budget.getTable(strVelName).Tables[0];

            string imagePath = ConfigurationManager.AppSettings["VslImgLink"].ToString() + "vessel_" + VesselID.ToString() + ".jpg";

            if (!File.Exists(imagePath))
            {
                imagePath = ConfigurationManager.AppSettings["VslImgLink"].ToString() + "NoImage.jpg";
            }


            Document document = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4, 10, 10, 10, 10);

            System.IO.MemoryStream msReport = new System.IO.MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(document, msReport);
            document.AddAuthor("");
            document.AddSubject("Monthly Owner’s Technical & Operating Report (MOTOR)");
            //'Adding Header in Document
            iTextSharp.text.Image logoImg = default(iTextSharp.text.Image);
          //  logoImg = iTextSharp.text.Image.GetInstance(Server.MapPath("~\\Images\\Logo\\MTMMLogo.jpg"));
            Chunk chk = new Chunk(logoImg, 0, 0, true);
            //Phrase p1 = new Phrase();
            //p1.Add(chk);

            iTextSharp.text.Table tb_header = new iTextSharp.text.Table(1);
            tb_header.Width = 100;
            tb_header.Border = iTextSharp.text.Rectangle.NO_BORDER;
            tb_header.DefaultCellBorder = iTextSharp.text.Rectangle.NO_BORDER;
            tb_header.DefaultHorizontalAlignment = Element.ALIGN_LEFT;
            tb_header.BorderWidth = 0;
            tb_header.BorderColor = iTextSharp.text.Color.WHITE;
            tb_header.Cellspacing = 1;
            tb_header.Cellpadding = 1;

            Cell c1 = new Cell(chk);
            c1.HorizontalAlignment = Element.ALIGN_LEFT;
            tb_header.AddCell(c1);

            Phrase p2 = new Phrase();
            p2.Add(new Phrase("Technical Inspection " + "\n" + "\n", FontFactory.GetFont("ARIAL", 18, iTextSharp.text.Font.BOLD)));
            Cell c2 = new Cell(p2);
            c2.HorizontalAlignment = Element.ALIGN_CENTER;
            tb_header.AddCell(c2);

            HeaderFooter header = new HeaderFooter(new Phrase(" \n Technical Inspection -" + txtplannedport.Text + " - " + txtlastdone.Text + "                                                     FORM NO : G 129A"), false);
            document.Header = header;

            header.Border = iTextSharp.text.Rectangle.NO_BORDER;
            string foot_Txt = "";
            foot_Txt = foot_Txt + "                                                                                                                ";
            foot_Txt = foot_Txt + "                                                                                                                ";
            foot_Txt = foot_Txt + "";
            HeaderFooter footer = new HeaderFooter(new Phrase(foot_Txt, FontFactory.GetFont("VERDANA", 6, iTextSharp.text.Color.DARK_GRAY)), true);
            footer.Border = iTextSharp.text.Rectangle.NO_BORDER;
            footer.Alignment = Element.ALIGN_LEFT;
            document.Footer = footer;
            //'-----------------------------------
            document.Open();
            document.Add(tb_header);

            // ---------------------- FRONT PAGE -------------------------------------------

            iTextSharp.text.Table tbF = new iTextSharp.text.Table(1);
            tbF.Width = 100;
            tbF.Alignment = Element.ALIGN_CENTER;
            tbF.DefaultHorizontalAlignment = Element.ALIGN_CENTER;
            tbF.Cellspacing = 1;
            tbF.Cellpadding = 1;
            tbF.Border = iTextSharp.text.Rectangle.NO_BORDER;


            iTextSharp.text.Image Img1 = default(iTextSharp.text.Image);
            Img1 = iTextSharp.text.Image.GetInstance(imagePath);
            Img1.ScaleAbsoluteWidth(500);
            Img1.ScaleAbsoluteHeight(400);
            Cell tcImg1 = new Cell(Img1);
            tcImg1.Border = iTextSharp.text.Rectangle.NO_BORDER;
            tbF.AddCell(tcImg1);


            iTextSharp.text.Font fCapText_11 = FontFactory.GetFont("ARIAL", 11, iTextSharp.text.Font.BOLD);
            iTextSharp.text.Font fCapText_10 = FontFactory.GetFont("ARIAL", 10, iTextSharp.text.Font.NORMAL);

            iTextSharp.text.Table tbM = new iTextSharp.text.Table(2);
            tbM.Width = 80;
            tbM.Alignment = Element.ALIGN_CENTER;
            tbM.DefaultHorizontalAlignment = Element.ALIGN_RIGHT;
            tbM.Cellspacing = 1;
            tbM.Cellpadding = 1;
            tbM.BorderWidth = 1;
            tbM.BorderColor = iTextSharp.text.Color.BLACK;

            Cell tc = new Cell(new Phrase("Vessel Name : ", fCapText_11));
            tbM.AddCell(tc);


            Cell tc1 = new Cell(new Phrase(dtVesselName.Rows[0]["Vesselname"].ToString(), fCapText_11));
            tc1.HorizontalAlignment = Element.ALIGN_LEFT;
            tbM.AddCell(tc1);

            Cell tcR1 = new Cell(new Phrase("INSP. # : ", fCapText_11));
            tbM.AddCell(tcR1);

            Cell tcR11 = new Cell(new Phrase(txtinspno.Text, fCapText_11));
            tcR11.HorizontalAlignment = Element.ALIGN_LEFT;
            tbM.AddCell(tcR11);

            Cell tcR33 = new Cell(new Phrase("INSP. START DT. : ", fCapText_11));
            tbM.AddCell(tcR33);
            Cell tcR33a = new Cell(new Phrase(HiddenField_StartDate.Value, fCapText_11));
            tcR33a.HorizontalAlignment = Element.ALIGN_LEFT;
            tbM.AddCell(tcR33a);


            Cell tcR3 = new Cell(new Phrase("INSP. DONE DT. : ", fCapText_11));
            tbM.AddCell(tcR3);
            Cell tcR31 = new Cell(new Phrase(txtlastdone.Text, fCapText_11));
            tcR31.HorizontalAlignment = Element.ALIGN_LEFT;
            tbM.AddCell(tcR31);

            Cell tcR7 = new Cell(new Phrase("PLACE : ", fCapText_11));
            tbM.AddCell(tcR7);
            Cell tcR71 = new Cell(new Phrase(txtplannedport.Text, fCapText_11));
            tcR71.HorizontalAlignment = Element.ALIGN_LEFT;
            tbM.AddCell(tcR71);

            //------------------------
            //Cell tcR8 = new Cell(new Phrase("Date of Report : ", fCapText_11));
            //tbM.AddCell(tcR8);
            //Cell tcR8a = new Cell(new Phrase(DateTime.Now.ToString("dd-MMM-yyyy"), fCapText_11));
            //tcR8a.HorizontalAlignment = Element.ALIGN_LEFT;
            //tbM.AddCell(tcR8a);

            Cell tcR5 = new Cell(new Phrase("Inspector's Name : ", fCapText_11));
            tbM.AddCell(tcR5);
            Cell tcR51 = new Cell(new Phrase(txtSupName.Text, fCapText_11));
            tcR51.HorizontalAlignment = Element.ALIGN_LEFT;
            tbM.AddCell(tcR51);

            Cell tccont = new Cell(tbM);
            tccont.Border = iTextSharp.text.Rectangle.NO_BORDER;
            tbF.AddCell(tccont);



            document.Add(tbF);
            document.Add(new Phrase("\n", FontFactory.GetFont("ARIAL", 12, iTextSharp.text.Font.BOLD)));

            // -----------------------------------------------------------------------------
            document.NewPage();


            //'-----------------------------------//'-----------------------------------//'-----------------------------------
            //string strDetails = "SELECT MDId,SrNo,Title,Descr FROM MortorDetails WHERE MID = " + MID;
            string strInsp = "SELECT Id,InspectionDueId,SrNo,ContentHeading,ContentText FROM InspReport_Main WHERE InspectionDueId=" + intInspDueId + " order by cast(srno as float)";
            //Content table

            DataTable dtInsp = Budget.getTable(strInsp).Tables[0];
            for (int i = 0; i <= dtInsp.Rows.Count - 1; i++)
            {
                #region --------- Start Main Table -------
                iTextSharp.text.Table tb1 = new iTextSharp.text.Table(1);
                tb1.Width = 100;
                tb1.Alignment = Element.ALIGN_CENTER;
                tb1.BorderWidth = 1;
                tb1.DefaultHorizontalAlignment = Element.ALIGN_LEFT;
                tb1.BorderColor = iTextSharp.text.Color.BLACK;
                tb1.Cellspacing = 1;
                tb1.Cellpadding = 1;
                #endregion
                AddTitle(dtInsp.Rows[i]["SrNo"].ToString() + ". " + dtInsp.Rows[i]["ContentHeading"].ToString(), tb1);
                AddContent(dtInsp.Rows[i]["ContentText"].ToString(), tb1);

                string strImages = "SELECT * FROM InspReport_Child where InspectionDueId = " + dtInsp.Rows[i]["InspectionDueId"].ToString() + " AND SrNo = " + dtInsp.Rows[i]["SrNo"].ToString() + " ";
                DataTable dtImages = Budget.getTable(strImages).Tables[0];

                //------------- Adding Images
                #region --------- Start Image Table -------
                iTextSharp.text.Table tbInner = new iTextSharp.text.Table(2);
                tbInner.Width = 100;
                float[] ws1 = { 49, 49 };
                tbInner.Widths = ws1;

                tbInner.Alignment = Element.ALIGN_CENTER;
                tbInner.BorderWidth = 0;
                tbInner.DefaultCellBorderWidth = 0;
                tbInner.DefaultCellBorderColor = iTextSharp.text.Color.WHITE;

                tbInner.DefaultHorizontalAlignment = Element.ALIGN_CENTER;
                tbInner.BorderColor = iTextSharp.text.Color.WHITE;
                tbInner.Cellspacing = 1;
                tbInner.Cellpadding = 1;
                #endregion
                //-------------
                for (int j = 0; j <= dtImages.Rows.Count - 1; j++)
                {
                    AddImage(dtImages.Rows[j]["FilePath"].ToString(), dtImages.Rows[j]["PicCaption"].ToString(), tbInner);
                }
                Cell tcImages = new Cell(tbInner);
                tcImages.HorizontalAlignment = Element.ALIGN_CENTER;
                tb1.AddCell(tcImages);
                //------------- Adding Images End

                document.Add(tb1);
                document.Add(new Phrase("\n", FontFactory.GetFont("ARIAL", 12, iTextSharp.text.Font.BOLD)));
            }


            //'-----------------------------------//'-----------------------------------//'-----------------------------------
            document.Close();

            //-----------------------------------------------------
            string inspno = "";
            DataTable dt9 = Common.Execute_Procedures_Select_ByQuery("SELECT INSPECTIONNO FROM T_INSPECTIONDUE WHERE ID=" + intInspDueId.ToString());
            if (dt9.Rows.Count > 0)
            {
                inspno = dt9.Rows[0][0].ToString();
            }

            string strname = inspno.Replace("/", "-") + ".pdf";
            string strpath = "~\\Modules\\Inspection\\UserUploadedDocuments\\Transactions\\" + strname;
            if (File.Exists(Server.MapPath(strpath)))
            {
                File.Delete(Server.MapPath(strpath));
            }
            FileStream fs = new FileStream(Server.MapPath(strpath), FileMode.Create);
            //-----------------------------------------------------
            //if (File.Exists(Server.MapPath("~\\ReportInsp.pdf")))
            //{
            //    File.Delete(Server.MapPath("~\\ReportInsp.pdf"));
            //}

            //FileStream fs = new FileStream(Server.MapPath("~\\ReportInsp.pdf"), FileMode.Create);
            byte[] bb = msReport.ToArray();
            fs.Write(bb, 0, bb.Length);
            fs.Flush();
            fs.Close();

        }
        catch (System.Exception ex)
        {
            //lblmessage.Text = ex.Message.ToString();
        }
    }
    protected void AddImage(string ImageFileName, string Caption, iTextSharp.text.Table T1)
    {
        iTextSharp.text.Font fCapText_8 = FontFactory.GetFont("ARIAL", 6, iTextSharp.text.Font.NORMAL);
        iTextSharp.text.Image Img1 = default(iTextSharp.text.Image);
        Img1 = iTextSharp.text.Image.GetInstance(Server.MapPath("~\\Modules\\Inspection\\UserUploadedDocuments\\Transaction_Reports\\" + ImageFileName));
        Img1.ScaleToFit(300, 200);

        Cell tcImg1 = new Cell(Img1);
        tcImg1.Add(new Phrase(Caption, fCapText_8));
        T1.AddCell(tcImg1);
    }
    protected void AddTitle(string Title, iTextSharp.text.Table T1)
    {
        iTextSharp.text.Font fCapText_11 = FontFactory.GetFont("ARIAL", 11, iTextSharp.text.Font.BOLD);
        Cell tc = new Cell(new Phrase(Title, fCapText_11));
        tc.Border = iTextSharp.text.Rectangle.NO_BORDER;
        T1.AddCell(tc);

    }
    protected void AddContent(string Content, iTextSharp.text.Table T1)
    {
        iTextSharp.text.Font fCapText_9 = FontFactory.GetFont("ARIAL", 10, iTextSharp.text.Font.NORMAL);
        Cell tc = new Cell(new Phrase(Content, fCapText_9));
        tc.Border = iTextSharp.text.Rectangle.TOP_BORDER;
        T1.AddCell(tc);
    }
    private class MyPageEventHandler : PdfPageEventHelper
    {
        public override void OnEndPage(PdfWriter writer, Document document)
        {
            iTextSharp.text.Table tb_header = new iTextSharp.text.Table(1);
            iTextSharp.text.Cell c1 = new Cell();
        }
    }
    // Publish
    protected void btn_Publish_OnClick(object sender, EventArgs e)
    {
        PublishToPDF();
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "a", "PublishSafetyInspRPT();", true);
    }
    protected void btnUploadMore_Click(object sender, EventArgs e)
    {
        try
        {
            string[] Files = System.IO.Directory.GetFiles(Server.MapPath("~/Temp"));
            foreach (string fl in Files)
            {
                System.IO.File.Delete(fl);
            }
        }
        catch
        {}

        dvUploadFiles.Visible = true;
        frmUpload.Attributes.Add("src", "../Inspection/UploadFiles.aspx?MainId=" + MainId + "&SNO=" + SNO + "&INSP=" + intInspDueId.ToString());
    }
    protected void btnClose_OnClick(object sender, EventArgs e)
    {
        dvUploadFiles.Visible = false;
        frmUpload.Attributes.Add("src", "");
        Show_Data_Details();
    }
    protected void btnNotify_Click(object sender, EventArgs e)
    {
        try
        {

            Common.Set_Procedures("dbo.PR_RPT_InsertUpdateInspReportNotify");
            Common.Set_ParameterLength(3);
            Common.Set_Parameters(
                new MyParameter("@InspectionDueId", intInspDueId),
                new MyParameter("@UserId", intLogin_Id),
                new MyParameter("@NotifiedBy", Session["UserName"].ToString())
                );
            DataSet ds = new DataSet();
            bool res;
            res = Common.Execute_Procedures_IUD(ds);

            if (res)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "err", "alert('Notified successfully. ');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "err", "alert('Unable to Notify. Error : " + Common.getLastError() + "');", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "err", "alert('Unable to Notify. Error : " + ex.Message + "');", true);
        }
    }
}