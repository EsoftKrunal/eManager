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

public partial class MotorAdd : System.Web.UI.Page
{
    static Random R = new Random(1000);

    int intLogin_Id = 0;
    string strSrNo = "";
    int temp = 0;
    int imgtemp = 0;
    ArrayList FileName = new ArrayList();
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    public string Mode
    {
        set { ViewState["Mode"] = value; }
        get { return "" + ViewState["Mode"]; }
    }  
    public int MID
    {
        set { ViewState["MID"] = value; }
        get { return int.Parse("0" + ViewState["MID"]); }
    }  
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        lblMasterMsg.Text = "";
        lblmessage.Text = "";
        if (!Page.IsPostBack)
        {
            if (Session["loginid"] == null)
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Default.aspx';", true);
            }
            else
            {
                intLogin_Id = Convert.ToInt32(Session["loginid"].ToString());
            }
            BindVesselDDL();

            if (Page.Request.QueryString["Mode"] != null)
                Mode = Page.Request.QueryString["Mode"].ToString();
            if (Page.Request.QueryString["MID"] != null)
                MID = Common.CastAsInt32(Page.Request.QueryString["MID"]);

            txt_SrNo.Text = "1";
            hfd_HDID.Value = "1";
            txt_ContentHeading.Text = "Technical";
            ViewMasterData();
            btnTechnical_Click(sender, e);  
            if (Mode == "V")
            {
                btn_Save.Visible = false;
                btnFiles.Enabled = false;
            }
        }
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        string strContHeading = "", strContentText = "", strPicCaption = "", strFilePath = "", FileName1 = "", strfilename = "";
        try
        {
            if (MID == 0)
            {
                lblmessage.Text = "Please save inspection details first.";
                return;
            }
            if (txt_SrNo.Text == "")
            {
                lblmessage.Text = "Please enter SrNo.";
                txt_SrNo.Focus();
                return;
            }
            if (txt_ContentHeading.Text == "")
            {
                lblmessage.Text = "Please enter Title.";
                txt_ContentHeading.Focus();
                return;
            }
            if (txt_Content.Text == "")
            {
                lblmessage.Text = "Please enter Description.";
                txt_Content.Focus();
                return;
            }
           
            strSrNo = txt_SrNo.Text;
            strContHeading = txt_ContentHeading.Text.Trim();
            strContentText = txt_Content.Text.Trim().Replace("'","''") ;
            //------------------------------------------
            for (int k = 0; k <= rpt_Images.Items.Count - 1; k++)
            {
                FileUpload flp_ref = (FileUpload)rpt_Images.Items[k].FindControl("flpupld");
                TextBox txt_ref = (TextBox)rpt_Images.Items[k].FindControl("txt_Caption2");
                if (flp_ref.PostedFile != null) // && flp_ref.FileContent.Length > 0)
                {
                    strfilename = flp_ref.FileName;
                    if (txt_ref.Text == "")
                    {
                        lblmessage.Text = "Please enter Caption.";
                        txt_ref.Focus();
                        imgtemp = 1;
                        return;
                    }
                    strPicCaption = txt_ref.Text;
                    HttpPostedFile file1 = flp_ref.PostedFile;
                    strFilePath = "EMANAGERBLOBs/Inspection/Mortor/" + flp_ref.FileName.Trim();
                    FileName1 = UploadFileToServer(file1, strfilename, "", "TR");
                    FileName.Add(FileName1);
                    if (FileName1.StartsWith("?"))
                    {
                        lblmessage.Text = FileName1.Substring(1);
                        imgtemp = 1;
                        return;
                    }
                }
            }
            int aa = FileName.Count;
            int MDID = getMDid();
            if (imgtemp != 1)
            {
                string sqlDetails = "";
                //sqlDetails = "sp_InsertUpdateMorterDetails " + MDID + ", " + MID + ", '" + strSrNo + "', '" + strContHeading.Replace("'", "`") + "', '" + strContentText.Replace("'", "`") + "', " + Session["loginid"].ToString() + ", " + Session["loginid"].ToString();

                Common.Set_Procedures("DBO.sp_InsertUpdateMorterDetails");
                Common.Set_ParameterLength(7);
                Common.Set_Parameters(
                new MyParameter("@MDId", MDID),
                new MyParameter("@MID", MID),
                new MyParameter("@SrNo", strSrNo),
                new MyParameter("@Title", strContHeading.Replace("'", "`")),
                new MyParameter("@Descr", strContentText.Replace("'", "`")),
                new MyParameter("@LoginId", Session["loginid"].ToString()),
                new MyParameter("@CreatedBy", Session["loginid"].ToString())
                );
                
                DataSet dsMDetails=new DataSet();
                if (Common.Execute_Procedures_IUD(dsMDetails))
                {
                        MDID=Common.CastAsInt32(dsMDetails.Tables[0].Rows[0][0].ToString());   
                        for (int d = 0; d <= FileName.Count - 1; d++)
                        {
                            FileUpload flp_ref = (FileUpload)rpt_Images.Items[d].FindControl("flpupld");
                            TextBox txt_ref = (TextBox)rpt_Images.Items[d].FindControl("txt_Caption2");

                            string sqlPicDetails = "sp_InsertUpdateMorterPicDetails 0," + MDID + ", '" + strSrNo + "', '" + txt_ref.Text.Trim().Replace("'", "`") + "', '" + FileName[d].ToString().Replace("'", "`") + "', " + intLogin_Id + " ; SELECT -1 ";
                            DataSet dsPic = Budget.getTable(sqlPicDetails);
                        }
                }
            }
            BindRepeater();
            lblmessage.Text = "Report Contents Saved Successfully.";
            temp = 1;
            txt_Nos.Text = "";
        }
        catch (Exception ex)
        {
            lblmessage.Text = ex.StackTrace.ToString();
        }

        ScriptManager.RegisterStartupScript(this, this.GetType(), "tr", "RefreshParent();", true);
    }
    public string UploadFileToServer(HttpPostedFile fileToUpload, string uploadedFileName, string existingFileName, string fileType)
    {
        // fileType D=Transaction Documents

        string uploadFileName = "";
        string path;
        string fullPath;

        // VIMS
        

        if (fileType == "TR") // Upload Transaction_Reports Documents
        {
            if (fileToUpload.ContentLength > (1024 * 1024 * 10))
            {
                uploadFileName = "?File Size is Too big! Maximum Allowed is 10 MB...";
            }
            else
            {
                // set the path and file
                uploadFileName = "2012_" + fileType + "_" + System.IO.Path.GetRandomFileName() + System.IO.Path.GetExtension(uploadedFileName);
                //uploadFileName = uploadedFileName.Trim();
                path = HttpContext.Current.Server.MapPath("~/EMANAGERBLOB/Inspection/Mortor/");
                fullPath = Path.Combine(path, uploadFileName);
                fileToUpload.SaveAs(fullPath);
                // delete old file if exists
                if (existingFileName.Trim().Length > 0)
                {
                    fullPath = path + existingFileName;
                    FileInfo fi = new FileInfo(fullPath);
                    if (fi.Exists)
                    {
                        fi.Delete();
                    }
                }
            }
        }

        
        return uploadFileName;
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
    protected void btnFiles_Click(object sender, EventArgs e)
    {
        try
        {
            int fcount = int.Parse(txt_Nos.Text);
            int[] dsc = new int[fcount];
            for (int i = 1; i <= dsc.Length; i++)
            {
                dsc[i - 1] = i;
            }
            rpt_Images.Visible = true;
            ////Repeater1.Visible = false;
            rpt_Images.DataSource = dsc;
            rpt_Images.DataBind();
        }
        catch { }
    }
    protected void rpt_OnDataBound(object sender, RepeaterItemEventArgs e)
    {
        ((FileUpload)e.Item.FindControl("flpupld")).Attributes.Add("onchange", "document.getElementById('" + ((TextBox)e.Item.FindControl("txt_Caption2")).ClientID + "').value=GetFileName(this.value);");
    }
    public int getMDid()
    {
        int MDID =0;
        DataSet ds = Budget.getTable("SELECT MDId,MID,SrNo,Title,Descr FROM MortorDetails WHERE MID=" + MID + " AND ltrim(rtrim(SrNo))='" + txt_SrNo.Text.Trim().PadLeft(2,'0') + "'" );
        if (ds.Tables[0].Rows.Count > 0)
        {
            MDID = Common.CastAsInt32(ds.Tables[0].Rows[0]["MdId"].ToString());
        }
        return MDID;
    }
    protected void BindRepeater()
    {
        int MDID = getMDid() ;
        try
        {
            string strSQL = "SELECT *,('~/'+FilePath) AS FPath,(SELECT COUNT(*) FROM MortorPicDetails WHERE MDId=" + MDID + " AND SrNo= " + txt_SrNo.Text.Trim() + ") AS TotalImages,'Picture '+CAST(Row_Number() over (ORDER BY MPDId) AS VARCHAR)+' : ' AS PicNumber,'Caption '+CAST(Row_Number() over (ORDER BY MPDId) AS VARCHAR)+' : ' AS CapNumber FROM MortorPicDetails WHERE MDId= " + MDID;
            DataTable dt43 = Budget.getTable(strSQL).Tables[0];
            if (dt43.Rows.Count > 0)
            {
                rpt_Images.Visible = false;
                rpt_Images.DataSource = null;
                rpt_Images.DataBind();
                Repeater1.Visible = true;
                lbl_NoofPics.Text = dt43.Rows[0]["TotalImages"].ToString();
                HiddenField_ImgUrl.Value = dt43.Rows[0]["FPath"].ToString();
                Repeater1.DataSource = dt43;
                Repeater1.DataBind();

                DataList1.DataSource= dt43;
                DataList1.DataBind();  
            }
            else
            {
                lbl_NoofPics.Text = "";
                Repeater1.Visible = false;
                Repeater1.DataSource = null;
                Repeater1.DataBind();
                rpt_Images.Visible = false;
                rpt_Images.DataSource = null;
                rpt_Images.DataBind();
                DataList1.DataSource = null;
                DataList1.DataBind();  
            }
        }
        catch { }
    }
    protected void btnPost_Click(object sender, EventArgs e)
    {
        BindRepeater();
    }
    private void display()
    {
        ResetClass();
        DataSet ds = Budget.getTable("SELECT MDId,MID,SrNo,Title,Descr FROM MortorDetails WHERE MID=" + MID + " AND SrNo='" + txt_SrNo.Text + "'");
        if (ds.Tables[0].Rows.Count > 0)
        {
            txt_Content.Text = ds.Tables[0].Rows[0]["Descr"].ToString().Replace("''","'") ;;
        }
        else
        {
            txt_Content.Text = "";
        }
        BindRepeater();
    }
    
    // 03 May 2012 =============================================================

    protected void btnTechnical_Click(object sender, EventArgs e)
    {
            txt_SrNo.Text = "01";
            txt_ContentHeading.Text = "Technical";
            display();
            btnTechnical.CssClass = "btnSel";
    }
    protected void btnOperations_Click(object sender, EventArgs e)
    {
            txt_SrNo.Text = "02";
            txt_ContentHeading.Text = "Operations";
            display();
            btnOperations.CssClass = "btnSel";
    }
    protected void btnCrewing_Click(object sender, EventArgs e)
    {
            txt_SrNo.Text = "03";
            txt_ContentHeading.Text = "Crewing";
            display();
            btnCrewing.CssClass = "btnSel";
    }
    protected void btnVetting_Click(object sender, EventArgs e)
    {
            txt_SrNo.Text = "04";
            txt_ContentHeading.Text = "Vetting";
            display();
            btnVetting.CssClass = "btnSel";
    }
    protected void btnOthers_Click(object sender, EventArgs e)
    {
            txt_SrNo.Text = "05";
            txt_ContentHeading.Text = "HSSQE";
            display();
            btnOthers.CssClass = "btnSel";
    }
    protected void ResetClass()
    {
        btnTechnical.CssClass = "btn1";
        btnOperations.CssClass = "btn1";
        btnCrewing.CssClass = "btn1";
        btnVetting.CssClass = "btn1";
        btnOthers.CssClass = "btn1";  
    }
    protected void btnDel_Click(object sender, EventArgs e)
    {
        ImageButton btn = ((ImageButton)(sender));
        string strFilePath = "~\\EMANAGERBLOB\\Inspection\\" + btn.Attributes["filename"];

        if (File.Exists(Server.MapPath(strFilePath)))
        {
            File.Delete(Server.MapPath(strFilePath));
        }
        Budget.getTable("DELETE FROM MortorPicDetails WHERE MPDId=" + btn.CommandArgument);
        BindRepeater();
    }

    // 03 May 2012 =============================================================

    protected void BindVesselDDL()
    {
        DataTable DT;        
        DT = Common.Execute_Procedures_Select_ByQuery("select VesselId,Vesselname from dbo.Vessel ");        

        this.ddl_Vessel.DataTextField = "VesselName";
        this.ddl_Vessel.DataValueField = "VesselId";
        this.ddl_Vessel.DataSource = DT;
        this.ddl_Vessel.DataBind();
        this.ddl_Vessel.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "0"));
        this.ddl_Vessel.Items[0].Value = "0";
    }
    public void ViewMasterData()
    {
        string sql = "select MID ,VesselID,ReportNo ,replace(convert(varchar,ReportDate ,106),' ','-')ReportDate ,SupTdName ,PreparedBy  " +
                    " from MortorMaster where MID="+ MID +"";

        DataSet ds = Budget.getTable(sql);
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr=ds.Tables[0].Rows[0];
                ddl_Vessel.SelectedValue = dr["VesselID"].ToString();
                txtReportNo.Text = dr["ReportNo"].ToString();
                txtReportDate.Text = dr["ReportDate"].ToString();
                lblVesselName.Text = ddl_Vessel.SelectedItem.Text;    
            }
        }
    }
    protected void PublishNow(object sender, EventArgs e)
    {
        ExportToPDF();  
        // SAVE PDF IN DATABASE

        ScriptManager.RegisterStartupScript(Page, this.GetType(), "a", "alert('Publihsed Successfully.');", true);
    }
    protected void PrintNow(object sender, EventArgs e)
    {
        ExportToPDF();
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "a", "window.open('../MotorReport.pdf?rand=" + R.Next().ToString() + "');", true);
    }
    private void ExportToPDF()
    {
        try
        {
            string strMotorData = "SELECT VesselID,(select Vesselname from dbo.Vessel VSL where VSL.VesselId = MM.VesselId) AS Vesselname,ReportNo, UPpER( right(REPLACE(CONVERT(VARCHAR(15),ReportDate,106),' ','-'),8)) AS ReportDate,SupTdName,PreparedBy FROM MortorMaster MM WHERE MID =" + MID;
            DataTable dtMotorDetails = Budget.getTable(strMotorData).Tables[0];

            string imagePath = ConfigurationManager.AppSettings["VslImgLink"].ToString() + "vessel_" + dtMotorDetails.Rows[0]["VesselID"].ToString() + ".jpg";

            if (!File.Exists(imagePath))
            {
                imagePath = ConfigurationManager.AppSettings["VslImgLink"].ToString() + "NoImage.jpg";
            }

            Document document = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4, 10, 10, 10, 10);

            System.IO.MemoryStream msReport = new System.IO.MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(document, msReport);
            document.AddAuthor("MTMSM");
            document.AddSubject("Monthly Owner’s Technical & Operating Report (MOTOR)");
            //'Adding Header in Document
            iTextSharp.text.Image logoImg = default(iTextSharp.text.Image);
            logoImg = iTextSharp.text.Image.GetInstance(Server.MapPath("~\\Images\\Logo\\MTMMLogo.jpg"));
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
            p2.Add(new Phrase("\n\n\n\n\n\n Monthly Owner’s Technical & Operating Report \n\n (MOTOR) " + "\n" + "\n\n\n\n\n\n", FontFactory.GetFont("ARIAL", 18, iTextSharp.text.Font.BOLD)));
            Cell c2 = new Cell(p2);
            c2.HorizontalAlignment = Element.ALIGN_CENTER;
            tb_header.AddCell(c2);

            HeaderFooter header = new HeaderFooter(new Phrase(" \n REPORT NO :" + dtMotorDetails.Rows[0]["ReportNo"].ToString()), false);
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
            //tbF.AddCell(tcImg1);


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


            Cell tc1 = new Cell(new Phrase(dtMotorDetails.Rows[0]["Vesselname"].ToString(), fCapText_11));
            tc1.HorizontalAlignment = Element.ALIGN_LEFT;
            tbM.AddCell(tc1);

            Cell tcR1 = new Cell(new Phrase("Report # : ", fCapText_11));
            tbM.AddCell(tcR1);

            Cell tcR11 = new Cell(new Phrase(dtMotorDetails.Rows[0]["ReportNo"].ToString(), fCapText_11));
            tcR11.HorizontalAlignment = Element.ALIGN_LEFT;
            tbM.AddCell(tcR11);

            Cell tcR3 = new Cell(new Phrase("Report Period : ", fCapText_11));
            tbM.AddCell(tcR3);

            Cell tcR31 = new Cell(new Phrase(dtMotorDetails.Rows[0]["ReportDate"].ToString(), fCapText_11));
            tcR31.HorizontalAlignment = Element.ALIGN_LEFT;
            tbM.AddCell(tcR31);

            Cell tcR5 = new Cell(new Phrase("Suptd Name : ", fCapText_11));
            tbM.AddCell(tcR5);

            Cell tcR51 = new Cell(new Phrase(dtMotorDetails.Rows[0]["SupTdName"].ToString(), fCapText_11));
            tcR51.HorizontalAlignment = Element.ALIGN_LEFT;
            tbM.AddCell(tcR51);

            Cell tcR7 = new Cell(new Phrase("Prepared By : ", fCapText_11));
            tbM.AddCell(tcR7);

            Cell tcR71 = new Cell(new Phrase(dtMotorDetails.Rows[0]["PreparedBy"].ToString(), fCapText_11));
            tcR71.HorizontalAlignment = Element.ALIGN_LEFT;
            tbM.AddCell(tcR71);

            Cell tccont = new Cell(tbM);
            tccont.Border = iTextSharp.text.Rectangle.NO_BORDER;
            tbF.AddCell(tccont);

            
            document.Add(tbF);
            document.Add(new Phrase("\n", FontFactory.GetFont("ARIAL", 12, iTextSharp.text.Font.BOLD)));

            // -----------------------------------------------------------------------------
            document.NewPage();
            //'-----------------------------------//'-----------------------------------//'-----------------------------------
            string strDetails = "SELECT MDId,SrNo,Title,Descr FROM MortorDetails WHERE MID = " + MID;
            DataTable dtDetails = Budget.getTable(strDetails).Tables[0];
            for (int i = 0; i <= dtDetails.Rows.Count - 1; i++)
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
                AddTitle(dtDetails.Rows[i]["SrNo"].ToString() + ". " + dtDetails.Rows[i]["Title"].ToString(), tb1);
                AddContent(dtDetails.Rows[i]["Descr"].ToString(), tb1);

                string strImages = "SELECT PicCaption,FilePath FROM MortorPicDetails WHERE MDId = " + dtDetails.Rows[i]["MDId"].ToString() + " AND SrNo = " + dtDetails.Rows[i]["SrNo"].ToString() + " ";
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
                document.Add(new Phrase ("\n",FontFactory.GetFont("ARIAL", 12, iTextSharp.text.Font.BOLD)));
            }
            

            //'-----------------------------------//'-----------------------------------//'-----------------------------------
            document.Close();
            if (File.Exists(Server.MapPath("~\\MotorReport.pdf")))
            {
                File.Delete(Server.MapPath("~\\MotorReport.pdf"));
            }

            FileStream fs = new FileStream(Server.MapPath("~\\MotorReport.pdf"), FileMode.Create);
            byte[] bb = msReport.ToArray();
            fs.Write(bb, 0, bb.Length);
            fs.Flush();
            fs.Close();
            
        }
        catch (System.Exception ex)
        {
            lblmessage.Text = ex.Message.ToString();
        }
    }
    protected void AddImage(string ImageFileName,string Caption,iTextSharp.text.Table T1 )
    {
        iTextSharp.text.Font fCapText_8 = FontFactory.GetFont("ARIAL", 6, iTextSharp.text.Font.NORMAL);
        iTextSharp.text.Image Img1 = default(iTextSharp.text.Image);
        Img1 = iTextSharp.text.Image.GetInstance(Server.MapPath("~\\EMANAGERBLOB\\Inspection\\Mortor\\" + ImageFileName));
        Img1.ScaleAbsoluteWidth(300);
        Img1.ScaleAbsoluteHeight(200);
        Cell tcImg1 = new Cell(Img1);
        
        tcImg1.Add(new Phrase(Caption, fCapText_8));  
        T1.AddCell(tcImg1);
    }
    protected void AddTitle(string Title, iTextSharp.text.Table T1)
    {
        iTextSharp.text.Font fCapText_11 = FontFactory.GetFont("ARIAL", 11, iTextSharp.text.Font.BOLD);
        Cell tc = new Cell(new Phrase(Title, fCapText_11));
        
        T1.AddCell(tc);
        
    }
    protected void AddContent(string Content, iTextSharp.text.Table T1)
    {
        iTextSharp.text.Font fCapText_9 = FontFactory.GetFont("ARIAL", 10, iTextSharp.text.Font.NORMAL);
        Cell tc = new Cell(new Phrase(Content, fCapText_9));
        tc.Border = iTextSharp.text.Rectangle.NO_BORDER;
        T1.AddCell(tc);
    }
}