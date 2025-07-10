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

public partial class Motor_Report : System.Web.UI.Page
{
    static Random R = new Random(1000);
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
        //--------------------------------- PAGE ACCESS AUTHORITY ----------------------------------------------------
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 163);
        if (chpageauth <= 0)
            Response.Redirect("blank.aspx");
        //------------------------------------------------------------------------------------------------------------
        
        if (Session["loginid"] == null)
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Login.aspx';", true);
        }
        lblMessage.Text = "";
        if (!Page.IsPostBack)
        {
            ddlFleet.DataSource = Common.Execute_Procedures_Select_ByQuery("select * from dbo.fleetmaster order by fleetname");
            ddlFleet.DataTextField = "FleetName";
            ddlFleet.DataValueField = "Fleetid";
            ddlFleet.DataBind();
            ddlFleet.Items.Insert(0, new System.Web.UI.WebControls.ListItem(" < - All - >", ""));
            BindOwner();
            
            BindVesselDDL();
            ddlMonth.SelectedValue = DateTime.Now.Month.ToString();
            BindYear();
            BindOwnerPO();
            BindYearPO();
            btn_Show_Click(sender, e);
        }
    }

    // Event
    protected void btn_Show_Click(object sender, EventArgs e)
    {
        string sql = "select MID, "+
            "(SELECT COUNT(*)  FROM MortorDetails MD WHERE MD.MID = MM.MID) AS DetailsCount " +
            " ,(select (Case when count(*)=5 then 'False' else 'True' end) from MortorDetails where MID=MM.MID and ltrim(rtrim(descr)) <>'')Exclamation_Vesibility " +
            ",(select VesselName from dbo.vessel v where v.VesselID=MM.VesselID)VesselName " +
            ", ReportNo ,Locked,replace(convert(varchar,ReportDate ,106),' ','-')ReportDate ,SupTdName ,PreparedBy,Published,replace(convert(varchar,PublishedOn ,106),' ','-')PublishedOn,Report, CASE WHEN getdate() > DATEADD(dd,4, DATEADD(MM, 1,  CAST('01-' + RIGHT(ReportNo, 8) AS DateTime))) THEN 'False' ELSE 'True' END AS Editable from MortorMaster MM ";
        string WhereClause = " where 1=1";

        if (ddl_Vessel.SelectedIndex != 0)
        {
            WhereClause = WhereClause + " and VesselID=" + ddl_Vessel.SelectedValue + "";
        }
        else
        {
            string VesselIds = "";
            for (int i = 0; i < ddl_Vessel.Items.Count; i++)
            {
                if (i != 0)
                {
                    VesselIds = VesselIds + "'" + ddl_Vessel.Items[i].Value + "'" + ",";
                }
            }
            if (VesselIds.Length > 0)
            {
                VesselIds = VesselIds.Remove(VesselIds.Length - 1);
            }

            WhereClause = WhereClause + " and VesselID IN (" + VesselIds + ") ";
        }
        if (ddlyear.SelectedIndex!=0)
            WhereClause = WhereClause + " and RYear="+ddlyear.SelectedValue+"";

        if (ddlMonth.SelectedIndex != 0)
            WhereClause = WhereClause + " and RMonth=" + ddlMonth.SelectedValue + "";

        sql = sql + WhereClause + "ORDER BY RYear desc, RMonth desc,VesselName ASC";
        Grd_MOTOR.DataSource = Common.Execute_Procedures_Select_ByQuery(sql);
        Grd_MOTOR.DataBind();  
    }
    protected void btnAddMotor_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "tr", "window.open('MotorAdd.aspx');", true);
    }
    protected void btn_Clear_Click(object sender, EventArgs e)
    {
        ddlFleet.SelectedIndex = 0;
        
    }
    protected void Grd_NearMiss_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Grd_MOTOR.PageIndex = e.NewPageIndex;
        btn_Show_Click(sender, e);
    }
    protected void ddlFleet_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        BindVesselDDL();
        ddlOwner.SelectedIndex = 0;
    }
    protected void Publish_Report(object sender, EventArgs e)
    {
        MID = Common.CastAsInt32(((Button)sender).CommandArgument);
        //DivAckRecieve.Visible = true;
        btnPublishMotor_Click(sender, e);
        //Budget.getTable("EXEC DBO.sp_MOTOR_PUBLISH " + MID.ToString() + ",'tes','tes',NULL");
    }
    protected void btnPublishMotor_Click(object sender, EventArgs e)
    {
        ExportToPDF();

        FileUpload img =  new FileUpload();
        Byte[] imgByte = File.ReadAllBytes(Server.MapPath("../MotorReport.pdf"));


        Common.Set_Procedures("dbo.sp_MOTOR_PUBLISH");
        Common.Set_ParameterLength(3);

        Common.Set_Parameters(
            new MyParameter("@MID", MID),
            new MyParameter("@REPORT", imgByte),
            new MyParameter("@MODE", "N"));
        DataSet ds = new DataSet();
        bool res;
        res = Common.Execute_Procedures_IUD(ds);
        if (res)
        {
            btn_Show_Click(sender, e);
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "a", "alert('Publihsed Successfully.');", true);
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "a", "window.open('../Report.pdf?Rand=" + R.Next().ToString() + "');", true);
            DivAckRecieve.Visible = false;
            txtSupdtName.Text = "";
        }
    }
    private void ExportToPDF( )
    {
        try
        {
            string strMotorData = "SELECT mid,mm.VesselID,VSL.VesselName,ReportNo, UPPER( right(REPLACE(CONVERT(VARCHAR(15),ReportDate,106),' ','-'),8)) AS ReportDate,SupTdName,PreparedBy FROM MortorMaster MM INNER JOIN dbo.Vessel VSL  ON VSL.VESSELID=MM.VESSELID WHERE MID =" + MID;
            DataTable dtMotorDetails = Budget.getTable(strMotorData).Tables[0];

            string Supdt = dtMotorDetails.Rows[0]["SupTdName"].ToString();
            string ReportNo = dtMotorDetails.Rows[0]["ReportNo"].ToString();

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

            Cell tcR1 = new Cell(new Phrase("Report # : " , fCapText_11));
            tbM.AddCell(tcR1);

            Cell tcR11 = new Cell(new Phrase(ReportNo, fCapText_11));
            tcR11.HorizontalAlignment = Element.ALIGN_LEFT;
            tbM.AddCell(tcR11);

            Cell tcR3 = new Cell(new Phrase("Report Period : " , fCapText_11));
            tbM.AddCell(tcR3);

            Cell tcR31 = new Cell(new Phrase(ReportNo.Substring(ReportNo.Length-8), fCapText_11));
            tcR31.HorizontalAlignment = Element.ALIGN_LEFT;
            tbM.AddCell(tcR31);

            Cell tcR5 = new Cell(new Phrase("Suptd Name : ", fCapText_11));
            tbM.AddCell(tcR5);

            Cell tcR51 = new Cell(new Phrase(Supdt, fCapText_11));
            tcR51.HorizontalAlignment = Element.ALIGN_LEFT;
            tbM.AddCell(tcR51);

            Cell tcR7 = new Cell(new Phrase("Published On : ", fCapText_11));
            tbM.AddCell(tcR7);

            Cell tcR71 = new Cell(new Phrase(DateTime.Today.Date.ToString("dd-MMM-yyyy"), fCapText_11));
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
            string strDetails = "SELECT MDId,SrNo,Title,Descr FROM MortorDetails WHERE MID = " + MID + " ORDER BY SrNo";
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
                document.Add(new Phrase("\n", FontFactory.GetFont("ARIAL", 12, iTextSharp.text.Font.BOLD)));
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
            //lblmessage.Text = ex.Message.ToString();
        }
    }
    protected void AddImage(string ImageFileName, string Caption, iTextSharp.text.Table T1)
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
    // Function
    protected void BindOwner()
    {
        try
        {
            this.ddlOwner.DataTextField = "OwnerName";
            this.ddlOwner.DataValueField = "OwnerId";
            this.ddlOwner.DataSource = Inspection_Master.getMasterDataforInspection("Owner", "OwnerId", "OwnerName");
            this.ddlOwner.DataBind();
            this.ddlOwner.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "0"));
            this.ddlOwner.Items[0].Value = "0";
            
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void ddlOwner_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            DataTable dt = new DataTable();
            if (ddlOwner.SelectedIndex > 0)
            {
                    dt = Common.Execute_Procedures_Select_ByQuery("select VESSELNAME,VESSELID FROM dbo.vessel where ownerid=" + ddlOwner.SelectedValue + " and VesselStatusid<>2 " + " order by vesselname");
            }

            ddl_Vessel.Controls.Clear();
            this.ddl_Vessel.DataTextField = "VesselName";
            this.ddl_Vessel.DataValueField = "VesselId";
            this.ddl_Vessel.DataSource = dt;
            this.ddl_Vessel.DataBind();
            this.ddl_Vessel.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "0"));
            ddlFleet.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void BindVesselDDL()
    {
        DataTable DT;
        if(ddlFleet.SelectedIndex==0)
            DT = Common.Execute_Procedures_Select_ByQuery("select VesselId,Vesselname from dbo.Vessel where VesselStatusid<>2  order by vesselname");
        else
            DT = Common.Execute_Procedures_Select_ByQuery("select VesselId,Vesselname from dbo.Vessel where FleetID=" + ddlFleet.SelectedValue + " AND VesselStatusid<>2  order by vesselname");

        this.ddl_Vessel.DataTextField = "VesselName";
        this.ddl_Vessel.DataValueField = "VesselId";
        this.ddl_Vessel.DataSource = DT;
        this.ddl_Vessel.DataBind();
        this.ddl_Vessel.Items.Insert(0, new System.Web.UI.WebControls.ListItem("< All >", "0"));
             
    }
    public void BindYear()
    {
        for (int i = DateTime.Now.Year; i >= 2000; i--)
        {
            ddlyear.Items.Add(new System.Web.UI.WebControls.ListItem(i.ToString(), i.ToString()));
        }
        ddlyear.Items.Insert(0, new System.Web.UI.WebControls.ListItem("< ALL >", "0"));

        ddlyear.SelectedValue = DateTime.Now.Year.ToString();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        DivAckRecieve.Visible = false;  
    }
    protected void btnReport_Click(object sender, ImageClickEventArgs e)
    {
        MID = Common.CastAsInt32(((ImageButton)sender).CommandArgument);

        string strSQL = "SELECT Report FROM MortorMaster WHERE MID =" + MID;
        DataTable dtImage = Budget.getTable(strSQL).Tables[0];
        byte[] imgByte = (byte[]) dtImage.Rows[0]["Report"];

        File.WriteAllBytes(Server.MapPath("../MotorReport.pdf"), imgByte);
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "a", "window.open('../MotorReport.pdf?Rand=" + R.Next().ToString() + "');", true);
    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        btn_Show_Click(sender, e);
    }
    protected void btn_OwnerPublish_Click(object sender, EventArgs e)
    {
        divPublishOwner.Visible = true;
    }
    #region ------------------ PUBLISH OWNER ------------------------
    protected void BindOwnerPO()
    {
        try
        {
            this.ddl_OwnerPublish.DataTextField = "OwnerName";
            this.ddl_OwnerPublish.DataValueField = "OwnerId";
            this.ddl_OwnerPublish.DataSource = Inspection_Master.getMasterDataforInspection("Owner", "OwnerId", "OwnerName");
            this.ddl_OwnerPublish.DataBind();
            this.ddl_OwnerPublish.Items.Insert(0, new System.Web.UI.WebControls.ListItem("< SELECT >", "0"));
            this.ddl_OwnerPublish.Items[0].Value = "0";

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public void BindYearPO()
    {
        for (int i = DateTime.Now.Year; i >= 2000; i--)
        {
            ddl_OwnerYear.Items.Add(new System.Web.UI.WebControls.ListItem(i.ToString(), i.ToString()));
        }
        ddl_OwnerYear.Items.Insert(0, new System.Web.UI.WebControls.ListItem("< SELECT >", "0"));

        //ddl_OwnerYear.SelectedValue = DateTime.Now.Year.ToString();
    }
    protected void ddl_OwnerPublish_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            DataTable dt = new DataTable();
            if (ddl_OwnerPublish.SelectedIndex > 0)
            {
                dt = Common.Execute_Procedures_Select_ByQuery("select VESSELNAME,VESSELID FROM dbo.vessel where ownerid=" + ddl_OwnerPublish.SelectedValue + " and VesselStatusid<>2 " + " order by vesselname");
            }

            ddl_OwnerPublishVessel.Controls.Clear();
            this.ddl_OwnerPublishVessel.DataTextField = "VesselName";
            this.ddl_OwnerPublishVessel.DataValueField = "VesselId";
            this.ddl_OwnerPublishVessel.DataSource = dt;
            this.ddl_OwnerPublishVessel.DataBind();


        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void btn_PublishOwnerCancel_Click(object sender, EventArgs e)
    {
        ddl_OwnerPublish.SelectedIndex = 0;
        ddl_OwnerMonth.SelectedIndex = 0;
        ddl_OwnerYear.SelectedIndex = 0;
        ddl_OwnerPublishVessel.Items.Clear();
        lblmsg_PublishOwner.Text = "";
        divPublishOwner.Visible = false;
    }
    protected void btn_PublishOwner_Click(object sender, EventArgs e)
    {
        lblmsg_PublishOwner.Text = "";

        if (ddl_OwnerPublish.SelectedIndex == 0)
        {
            lblmsg_PublishOwner.Text = "Please select owner.";
            ddl_OwnerPublish.Focus();
            return;
        }
        if (ddl_OwnerMonth.SelectedIndex == 0)
        {
            lblmsg_PublishOwner.Text = "Please select month.";
            ddl_OwnerMonth.Focus();
            return;
        }
        if (ddl_OwnerYear.SelectedIndex == 0)
        {
            lblmsg_PublishOwner.Text = "Please select year.";
            ddl_OwnerYear.Focus();
            return;
        }

        if (ddl_OwnerPublishVessel.Items.Count > 0)
        {
            ExportToPDFFOROwner();
        }
        else
        {
            lblmsg_PublishOwner.Text = "No vessel for selected owner.";
        }
    }
    private void ExportToPDFFOROwner()
    {
        try
        {
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

            foreach (System.Web.UI.WebControls.ListItem lst in ddl_OwnerPublishVessel.Items )
            {
            string strMotorData = "SELECT MID,ReportNo,UPpER( right(REPLACE(CONVERT(VARCHAR(15),ReportDate,106),' ','-'),8)) AS ReportDate,SupTdName,PreparedBy FROM MortorMaster WHERE VesselID =" + lst.Value.ToString() + " AND RMonth = " + ddl_OwnerMonth.SelectedValue.ToString() + " AND RYear = " + ddl_OwnerYear.SelectedValue.Trim();
            DataTable dtMotorDetails = Budget.getTable(strMotorData).Tables[0];

            string imagePath = ConfigurationManager.AppSettings["VslImgLink"].ToString() + "vessel_" + lst.Value.Trim().ToString() + ".jpg";

            if (!File.Exists(imagePath))
            {
                imagePath = ConfigurationManager.AppSettings["VslImgLink"].ToString() + "NoImage.jpg";
            }

            if (dtMotorDetails.Rows.Count > 0)
            {

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
                p2.Add(new Phrase("Monthly Owner’s Technical & Operating Report \n\n (MOTOR) " + "\n" + "\n", FontFactory.GetFont("ARIAL", 18, iTextSharp.text.Font.BOLD)));
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


                Cell tc1 = new Cell(new Phrase(lst.Text.Trim().ToString(), fCapText_11));
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
                string strDetails = "SELECT MDId,SrNo,Title,Descr FROM MortorDetails WHERE MID = " + dtMotorDetails.Rows[0]["MID"].ToString();
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
                    document.Add(new Phrase("\n", FontFactory.GetFont("ARIAL", 12, iTextSharp.text.Font.BOLD)));
                }
            }

                document.NewPage();
            
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
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "a", "window.open('../MotorReport.pdf');", true);
        }
        catch (System.Exception ex)
        {
            lblmsg_PublishOwner.Text = ex.Message.ToString();
        }
    }
    #endregion
}
