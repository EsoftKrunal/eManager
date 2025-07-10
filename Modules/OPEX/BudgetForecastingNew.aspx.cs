using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Text;
using System.Configuration;  
using iTextSharp.text;
using iTextSharp.text.pdf;  

public partial class BudgetForecastingNew : System.Web.UI.Page
{ 
    AuthenticationManager authRecInv;
    public void Manage_Menu()
    {

        AuthenticationManager auth = new AuthenticationManager(27, int.Parse(Session["loginid"].ToString()), ObjectType.Module);
        trCurrBudget.Visible = auth.IsView;
        auth = new AuthenticationManager(28, int.Parse(Session["loginid"].ToString()), ObjectType.Module);
        trAnalysis.Visible = auth.IsView;
        auth = new AuthenticationManager(29, int.Parse(Session["loginid"].ToString()), ObjectType.Module);
        trBudgetForecast.Visible = auth.IsView;
        auth = new AuthenticationManager(30, int.Parse(Session["loginid"].ToString()), ObjectType.Module);
        trPublish.Visible = auth.IsView;
    }
    protected void rdoList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdoList.Items[0].Selected)
        {
            ddlFleet.Visible = true;
            ddlCompany.Visible = false;
            lblFC.Text = "Fleet";
        }
        else
        {
            ddlCompany.Visible = true;
            ddlFleet.Visible = false;
            lblFC.Text = "Company";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //---------------------------------------
        ProjectCommon.SessionCheck();
        //---------------------------------------
        #region --------- USER RIGHTS MANAGEMENT -----------
        try
        {
            authRecInv = new AuthenticationManager(269, int.Parse(Session["loginid"].ToString()), ObjectType.Page);
            if (!(authRecInv.IsView))
            {
                Response.Redirect("~/NoPermissionBudget.aspx", false);
            }
            btnEdit.Visible = authRecInv.IsUpdate;
            Print.Visible = authRecInv.IsPrint;   
        }
        catch (Exception ex)
        {
            Response.Redirect("~/NoPermissionBudget.aspx?Message=" + ex.Message);
        }
        #endregion ----------------------------------------
        if (!IsPostBack)
        { 
            Manage_Menu(); 
            BindFleet();
            BindCompany();
            BindVesselBYOwner();
           
        }
    }
    // Event ----------------------------------------------------------------
    // DropDown
    protected void ddlCompany_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        ddlFleet.SelectedIndex = 0;
        BindVesselBYOwner();
        BindRepeater();
    }
    public void ddlShip_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        BindRepeater();
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Search.aspx");
    }
    protected void imgClear_Click(object sender, EventArgs e)
    {


    }
    // Function ----------------------------------------------------------------
    public void BindFleet()
    {
        DataTable dtFleet = Common.Execute_Procedures_Select_ByQueryCMS("select * from FleetMaster");
        if (dtFleet != null)
        {
            if (dtFleet.Rows.Count >= 0)
            {
                ddlFleet.DataSource = dtFleet;
                ddlFleet.DataTextField = "FleetName";
                ddlFleet.DataValueField = "FleetID";
                ddlFleet.DataBind();
                ddlFleet.Items.Insert(0, new System.Web.UI.WebControls.ListItem("< All >", "0"));
            }
        }
    }
    public void BindCompany()
    {
        string sql = "SELECT VW_sql_tblSMDPRCompany.Company, VW_sql_tblSMDPRCompany.ReportCo "+
            " ,(VW_sql_tblSMDPRCompany.Company + '-' + VW_sql_tblSMDPRCompany.[Company Name]) as CompName" +
        " FROM VW_sql_tblSMDPRCompany WHERE (((VW_sql_tblSMDPRCompany.InAccts)=1)) and (((VW_sql_tblSMDPRCompany.Active)='Y'))";
        DataTable DtCompany = Common.Execute_Procedures_Select_ByQuery(sql);
        if (DtCompany != null)
        {
            ddlCompany.DataSource = DtCompany;
            ddlCompany.DataTextField = "CompName";
            ddlCompany.DataValueField = "Company";
            ddlCompany.DataBind();
            ddlCompany.Items.Insert(0, new System.Web.UI.WebControls.ListItem("< Select Company >", "0"));
            ddlCompany.SelectedIndex = 0;
            BindVesselBYOwner();
        }
    }
    public void BindVesselBYOwner()
    {
        string sql = "SELECT VW_sql_tblSMDPRVessels.ShipID, VW_sql_tblSMDPRVessels.Company, VW_sql_tblSMDPRVessels.ShipName, " +
                    " (VW_sql_tblSMDPRVessels.ShipID+' - '+VW_sql_tblSMDPRVessels.ShipName)as ShipNameCode" +
                    " FROM VW_sql_tblSMDPRVessels " +
                    " WHERE (((VW_sql_tblSMDPRVessels.Company)='"+ddlCompany.SelectedValue+"')) AND ACTIVE='A'";
        DataTable DtVessel = Common.Execute_Procedures_Select_ByQuery(sql);
        if (DtVessel != null)
        {
            ddlShip.DataSource = DtVessel;
            ddlShip.DataTextField = "ShipNameCode";
            ddlShip.DataValueField = "ShipID";
            ddlShip.DataBind();
            ddlShip.Items.Insert(0, new System.Web.UI.WebControls.ListItem("<Select>", "0"));
        }
                    
    }
    public void BindVesselBYFleet()
    {
        string sql = "";
        //if (ddlFleet.SelectedIndex == 0)
        //{
        //    sql = "select VesselCode,vesselName  from Vessel order by vesselName ";
        //}
        //else
        //{
            sql = "select VesselCode,vesselName from Vessel where fleetID=" + ddlFleet.SelectedValue + " And VesselStatusid<>2  order by vesselName ";
        //}
        DataTable dtFleet = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        if (dtFleet != null)
        {
            if (dtFleet.Rows.Count >= 0)
            {
                ddlShip.DataSource = dtFleet;
                ddlShip.DataTextField = "vesselName";
                ddlShip.DataValueField = "VesselCode";
                ddlShip.DataBind();
                ddlShip.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "0"));
            }
        }
    }
    string FormatCurrency(object InValue)
    {
        string StrIn = InValue.ToString();
        string OutValue = "";
        int Len = StrIn.Length;
        while (Len > 3)
        {
            if(OutValue.Trim()=="")  
                OutValue = StrIn.Substring(Len - 3);
            else
                OutValue = StrIn.Substring(Len - 3) + ","  + OutValue;

            StrIn = StrIn.Substring(0, Len - 3);
            Len = StrIn.Length;
        }
        OutValue = StrIn + "," + OutValue;
        if (OutValue.EndsWith(",")) { OutValue = OutValue.Substring(0,OutValue.Length-1);} 
        return OutValue;
    }
    public void BindRepeater()
    {
        DataTable DtForPDR = new DataTable();
        DtForPDR.Columns.Add("BudgetHead");
        int[] ColumnSum;
        int[] VesselDays;
        ColumnSum = new int[ddlShip.Items.Count - 1];
        VesselDays = new int[ddlShip.Items.Count - 1];
            
        StringBuilder sb = new StringBuilder();
        //StringBuilder sbLast = new StringBuilder();

        DataTable dtAccts = Common.Execute_Procedures_Select_ByQuery("select distinct midcatid,midcat,MajSeqNo,MidSeqNo from dbo.v_New_CurrYearBudgetHome WHERE MajCatId=6 ORDER BY MajSeqNo,MidSeqNo");
        DataTable dtAccts1 = Common.Execute_Procedures_Select_ByQuery("select distinct midcatid,midcat,MajSeqNo,MidSeqNo from dbo.v_New_CurrYearBudgetHome WHERE MajCatId<>6 ORDER BY MajSeqNo,MidSeqNo");
        sb.Append("<table cellpadding='4' cellspacing='0' width='100%' class='newformat' border='1' style='border-collapse:collapse' bordercolor='white'>");
        sb.Append("<thead>");
        sb.Append("<tr>");
        sb.Append("<td>Budget Head</td>");

        //Create datatable for print pdf-------------------------
        for (int i = 0; i < ddlShip.Items.Count; i++)
        {
            if (i != 0)
            {
                DtForPDR.Columns.Add(ddlShip.Items[i].Value);
            }
        }
        DtForPDR.Columns.Add("Total");
        DataRow DrPDF = DtForPDR.NewRow();
        DrPDF[0] = "Year Head";
        DrPDF["Total"] = "";
        //End creatint datatable for print pdf-------------------------
        for (int i = 0; i < ddlShip.Items.Count;i++)
        {
            if (i != 0)
            {
                sb.Append("<td style='width:80px;'><input type='radio' name='radVSL' value='" + ddlShip.Items[i].Value + "' id='rad" + ddlShip.Items[i].Value + "'><label for='rad" + ddlShip.Items[i].Value + "'>" + ddlShip.Items[i].Value + "</label>");
                DataTable dtDays = Common.Execute_Procedures_Select_ByQuery("select top 1 Days From dbo.v_New_CurrYearBudgetHome Where shipid='" + ddlShip.Items[i].Value + "' AND year=" + (DateTime.Today.Year-1).ToString() + " order by days desc");
                if (dtDays != null)
                {
                    if (dtDays.Rows.Count > 0)
                    {
                        sb.Append("</br>" + dtDays.Rows[0][0].ToString()+ " days");
                        VesselDays[i - 1] = Common.CastAsInt32(dtDays.Rows[0][0]);

                        DrPDF[ddlShip.Items[i].Value] = dtDays.Rows[0][0].ToString() + " days";
                    }
                }
                sb.Append("</td>");
            }
        }

        sb.Append("<td style='width:80px;'>Total</td>");
        sb.Append("</tr>");
        sb.Append("</thead>");
        sb.Append("</table>");
        DrPDF["Total"] = "Total";
        DtForPDR.Rows.Add(DrPDF);

        sb.Append("<table cellpadding='4' cellspacing='0' width='100%' class='newformat' border='1' style='border-collapse:collapse' bordercolor='white'>");
        sb.Append("<tbody>");
    
        //-----------------
        // DATA ROWS 
        for (int i = 0; i <= dtAccts.Rows.Count - 1; i++)
        {
            DataRow drPDF = DtForPDR.NewRow();
            int RowSum = 0;
            sb.Append("<tr onmouseover=''>");
            sb.Append("<td style='text-align:left;'>" + dtAccts.Rows[i][1].ToString() + "</td>");
            drPDF[0] = dtAccts.Rows[i][1].ToString();
            for (int j = 0; j < ddlShip.Items.Count; j++)
            {
                if (j != 0)
                {
                    DataTable dtShip = Common.Execute_Procedures_Select_ByQuery("select budget from dbo.v_New_CurrYearBudgetHome where shipid='" + ddlShip.Items[j].Value + "' AND year=" + (DateTime.Today.Year-1).ToString() + " and  midcatid=" + dtAccts.Rows[i][0].ToString());
                    if (dtShip != null)
                    {
                        if (dtShip.Rows.Count > 0)
                        {
                            sb.Append("<td style='width:80px;text-align:right'>&nbsp;" + FormatCurrency(dtShip.Rows[0][0]) + "</td>");
                            ColumnSum[j-1] += Common.CastAsInt32(dtShip.Rows[0][0]);
                            RowSum += Common.CastAsInt32(dtShip.Rows[0][0]);
                            drPDF[j] = FormatCurrency(dtShip.Rows[0][0]).ToString(); 
                        }
                        else
                        {
                            sb.Append("<td style='width:80px;'></td>");
                        }
                    }
                    else
                    {
                        sb.Append("<td style='width:80px;'></td>");
                    }
                }
            }
            sb.Append("<td style='width:80px;text-align:right'>" + FormatCurrency(RowSum) + "</td>");
            sb.Append("</tr>");
            drPDF["Total"] = FormatCurrency(RowSum);
            DtForPDR.Rows.Add(drPDF);
        }
        //---------------
        // TOTAL
        DataRow drPDFT = DtForPDR.NewRow();
        sb.Append("<tr style='background-color:#FFCC66'>");
        sb.Append("<td style='font-size:10px;text-align:right;'>Total(US$)</td>");
        drPDFT[0] = "Total(US$)";
        int GrossSum = 0;
        for (int i = 0; i < ddlShip.Items.Count; i++)
        {
            if (i != 0)
            {
                sb.Append("<td style='font-size:10px;text-align:right'>" + FormatCurrency(ColumnSum[i - 1]) + "</td>");
                GrossSum += ColumnSum[i - 1];
                drPDFT[i] = FormatCurrency(ColumnSum[i - 1]);
            }
        }
        sb.Append("<td style='font-size:10px;text-align:right'>" + FormatCurrency(GrossSum) + "</td>");
        sb.Append("</tr>");
        
        drPDFT["Total"] = FormatCurrency(GrossSum);
        DtForPDR.Rows.Add(drPDFT);


        // PER DAY CALC
        DataRow drPDFpt = DtForPDR.NewRow();
        sb.Append("<tr style='background-color:#FFCC66'>");
        sb.Append("<td style='font-size:10px;text-align:right;'>Avg Daily Cost(US$)</td>");
        drPDFpt[0] = "Avg Daily Cost(US$)";
        //int GrossSum = 0;
        for (int i = 0; i < ddlShip.Items.Count; i++)
        {
            if (i != 0)
            {
                try
                {
                    sb.Append("<td style='font-size:10px;text-align:right'>" + FormatCurrency((ColumnSum[i - 1] / VesselDays[i - 1])) + "</td>");
                    drPDFpt[i] = FormatCurrency((ColumnSum[i - 1] / VesselDays[i - 1])).ToString();
                }
                catch (DivideByZeroException ex)
                {
                    sb.Append("<td style='font-size:10px;text-align:right'>0</td>");
                }
                catch(Exception ex)
                {
                    throw ex;
                }
                //GrossSum += ColumnSum[i - 1];
            }
        }
        sb.Append("<td style='font-size:10px;text-align:right'></td>");
        sb.Append("</tr>");
        DtForPDR.Rows.Add(drPDFpt);

        // DATA ROWS  - 2
        for (int i = 0; i <= dtAccts1.Rows.Count - 1; i++)
        {
            DataRow drPDF = DtForPDR.NewRow();
            int RowSum = 0;
            sb.Append("<tr>");
            sb.Append("<td style='text-align:left;'>" + dtAccts1.Rows[i][1].ToString() + "</td>");
            drPDF[0] = dtAccts1.Rows[i][1].ToString();
            for (int j = 0; j < ddlShip.Items.Count; j++)
            {
                if (j != 0)
                {
                    DataTable dtShip = Common.Execute_Procedures_Select_ByQuery("select budget from dbo.v_New_CurrYearBudgetHome where shipid='" + ddlShip.Items[j].Value + "' AND year=" + (DateTime.Today.Year-1).ToString() + " and  midcatid=" + dtAccts1.Rows[i][0].ToString());
                    if (dtShip != null)
                    {
                        if (dtShip.Rows.Count > 0)
                        {
                            sb.Append("<td style='text-align:right'>" + FormatCurrency(dtShip.Rows[0][0]) + "</td>");
                            ColumnSum[j - 1] += Common.CastAsInt32(dtShip.Rows[0][0]);
                            RowSum += Common.CastAsInt32(dtShip.Rows[0][0]);
                            drPDF[j] = FormatCurrency(dtShip.Rows[0][0]);
                        }
                        else
                        {
                            sb.Append("<td></td>");
                        }
                    }
                    else
                    {
                        sb.Append("<td></td>");
                    }
                }
            }
            sb.Append("<td style='text-align:right'>" + FormatCurrency(RowSum) + "</td>");
            sb.Append("</tr>");
            drPDF["Total"] = FormatCurrency(RowSum);
            DtForPDR.Rows.Add(drPDF);

        }

        // GROSS TOTAL
        DataRow drPDFgt = DtForPDR.NewRow();
        sb.Append("<tr style='background-color:#FFCC66'>");
        sb.Append("<td style='font-size:10px;text-align:right;'>Gross Total(US$)</td>");
        drPDFgt[0] = "Gross Total(US$)";
        //int GrossSum=0;
        for (int i = 0; i < ddlShip.Items.Count; i++)
        {
            if (i != 0)
            {
                sb.Append("<td style='font-size:10px;text-align:right'>" + FormatCurrency(ColumnSum[i - 1]) + "</td>");
                GrossSum += ColumnSum[i - 1];
                drPDFgt[i] = FormatCurrency(ColumnSum[i - 1]).ToString();

            }
        }
        sb.Append("<td style='font-size:10px;text-align:right'>" + FormatCurrency(GrossSum) + "</td>");
        sb.Append("</tr>");
        //sb.Append(sbLast.ToString()); 
        sb.Append("</table>");

        drPDFgt["Total"] = FormatCurrency(GrossSum);
        DtForPDR.Rows.Add(drPDFgt);
        ViewState.Add("DtForPDR", DtForPDR);
        lit1.Text = sb.ToString();  
    }
    protected void ddlFleet_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        ddlCompany.SelectedIndex = 0;  
        BindVesselBYFleet();
        BindRepeater();
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        string vsl = Request.Form["radVSL"];  
        Response.Redirect("~/BudgetForecasting.aspx?VSL=" + vsl);
    }
    protected void Print_Click(object sender, EventArgs e)
    {
        string Comp="",Vessel="",BType,StartDate="",EndDate="",year="",YearDays="";
        Vessel = (Request.Form["radVSL"] == null) ? "" : Request.Form["radVSL"];

        year = (DateTime.Today.Year-1).ToString();   
        BType = "< All >";
        string script="";
        //----------------------------
        if(Vessel.Trim()=="")
        {
            script="alert('Please select ship.');";
        }
        else
        {
            //-----------------------
            DataTable dt=Common.Execute_Procedures_Select_ByQuery("select a.company+ '-' + [company name] from VW_sql_tblSMDPRCompany a where a.company in (select b.company from VW_sql_tblSMDPRVessels b where b.shipid='" + Vessel + "')");
            if(dt.Rows.Count >0)
            {
                Comp=dt.Rows[0][0].ToString();   
            }
            //-----------------------
            dt=Common.Execute_Procedures_Select_ByQuery("select shipid+'-'+shipname from dbo.VW_sql_tblSMDPRVessels where shipid='" + Vessel + "'");
            if(dt.Rows.Count >0)
            {
                Vessel=dt.Rows[0][0].ToString();   
            }
            //-----------------------
            dt=Common.Execute_Procedures_Select_ByQuery("select TOP 1 replace(convert(varchar,VESSSTART,106),' ','-') VESSSTART,replace(convert(varchar,VESSEND,106),' ','-') VESSEND,YEARDAYS from dbo.tblsmdbudgetforecastyear where cocode='" + Comp.Substring(0,3)  + "' AND SHIPID='" + Vessel.Substring(0,3)  + "' AND YEAR=" + year);
            if(dt.Rows.Count >0)
            {
                StartDate= dt.Rows[0]["VESSSTART"].ToString(); 
                EndDate= dt.Rows[0]["VESSEND"].ToString(); 
                YearDays= dt.Rows[0]["YEARDAYS"].ToString(); 
            }
            script = "window.open('Print.aspx?CYBudget=true&Comp=" + Comp + "&Vessel=" + Vessel + "&BType=" + BType + "&StartDate=" + StartDate + "&EndDate=" + EndDate + "&year=" + DateTime.Today.Year.ToString() + "&YearDays=" + YearDays + "&MajCatID=6', '', '');";
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "a", script, true);      
    }

    private void ExportToPDF(string Company, DataTable dt)
    {
        try
        {
            //Document document = new Document(PageSize.LETTER.Rotate, 10, 10, 30, 10);
            Document document = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4.Rotate(), 10, 10, 30, 10);

            System.IO.MemoryStream msReport = new System.IO.MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(document, msReport);
            document.AddAuthor("MTMSM");
            document.AddSubject("Fleet Summary");
            //'Adding Header in Document
            iTextSharp.text.Image logoImg = default(iTextSharp.text.Image);
            logoImg = iTextSharp.text.Image.GetInstance(Server.MapPath("~\\Images\\MTMMLogo.jpg"));
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
            p2.Add(new Phrase(Company + "\n" + "\n", FontFactory.GetFont("ARIAL", 12, iTextSharp.text.Font.BOLD)));
            Cell c2 = new Cell(p2);
            c2.HorizontalAlignment = Element.ALIGN_CENTER;
            tb_header.AddCell(c2);

            //Chunk ch = new Chunk();

            HeaderFooter header = new HeaderFooter(new Phrase(""), false);
            document.Header = header;

            //header.Alignment = Element.ALIGN_LEFT;
            header.Border = iTextSharp.text.Rectangle.NO_BORDER;
            //'Adding Footer in document
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
            //------------ TABLE HEADER FONT 
            iTextSharp.text.Font fCapText = FontFactory.GetFont("ARIAL", 9, iTextSharp.text.Font.BOLD);
            iTextSharp.text.Font fCapText_5 = FontFactory.GetFont("ARIAL", 8, iTextSharp.text.Font.BOLD);
            //------------ TABLE HEADER ROW 
            int ColumnsCount = dt.Columns.Count;
            iTextSharp.text.Table tb1 = new iTextSharp.text.Table(ColumnsCount);
            tb1.BackgroundColor = iTextSharp.text.Color.LIGHT_GRAY;
            tb1.Width = 100;

            float[] ws = new float[ColumnsCount];
            ws[0] = 15;
            for (int i = 1; i <= ws.Length - 1; i++)
                ws[i] = 80 / (ws.Length - 1);

            ws[ws.Length - 1] = ws[ws.Length - 1] + 5;
            tb1.Widths = ws;

            tb1.Alignment = Element.ALIGN_CENTER;
            tb1.Border = iTextSharp.text.Rectangle.NO_BORDER;
            tb1.DefaultHorizontalAlignment = Element.ALIGN_CENTER;
            tb1.BorderColor = iTextSharp.text.Color.WHITE;
            tb1.Cellspacing = 1;
            tb1.Cellpadding = 1;

            for (int i = 0; i <= dt.Columns.Count - 1; i++)
            {
                Cell tc = new Cell(new Phrase(dt.Columns[i].ColumnName, fCapText));
                tb1.AddCell(tc);
            }

            DataRow dr_yb = dt.Rows[0];
            for (int i = 0; i <= dt.Columns.Count - 1; i++)
            {
                Cell tc = new Cell(new Phrase(dr_yb[i].ToString(), fCapText_5));
                tb1.AddCell(tc);
            }

            document.Add(tb1);
            //------------ TABLE DATA ROW 
            // data rows
            iTextSharp.text.Table tbdata = new iTextSharp.text.Table(ColumnsCount);
            tbdata.Width = 100;
            tbdata.Widths = ws;
            tbdata.Alignment = Element.ALIGN_CENTER;
            tbdata.Border = iTextSharp.text.Rectangle.NO_BORDER;
            tbdata.DefaultHorizontalAlignment = Element.ALIGN_CENTER;
            tbdata.BorderColor = iTextSharp.text.Color.GRAY;
            tbdata.Cellspacing = 1;
            tbdata.Cellpadding = 1;
            for (int k = 1; k < dt.Rows.Count - 1; k++)
            {
                DataRow dr = dt.Rows[k];
                for (int i = 0; i <= dt.Columns.Count - 1; i++)
                {
                    Cell tc = new Cell(new Phrase(dr[i].ToString(), fCapText_5));
                    if (k == 8 || k == 9)
                    {
                        tc.BackgroundColor = iTextSharp.text.Color.LIGHT_GRAY;
                    }
                    if (i == 0)
                        tc.HorizontalAlignment = Element.ALIGN_LEFT;
                    else
                        tc.HorizontalAlignment = Element.ALIGN_RIGHT;
                    tbdata.AddCell(tc);
                }
            }
            document.Add(tbdata);
            //------------ TABLE FOOTER ROW 
            iTextSharp.text.Table tb2 = new iTextSharp.text.Table(ColumnsCount);
            tb2.BackgroundColor = iTextSharp.text.Color.LIGHT_GRAY;
            tb2.Width = 100;
            tb2.Widths = ws;
            tb2.Alignment = Element.ALIGN_CENTER;
            tb2.Border = iTextSharp.text.Rectangle.NO_BORDER;
            tb2.DefaultHorizontalAlignment = Element.ALIGN_CENTER;
            tb2.BorderColor = iTextSharp.text.Color.WHITE;
            tb2.Cellspacing = 1;
            tb2.Cellpadding = 1;
            DataRow drF = dt.Rows[dt.Rows.Count - 1];
            for (int i = 0; i <= dt.Columns.Count - 1; i++)
            {
                Cell tc = new Cell(new Phrase(drF[i].ToString(), fCapText_5));
                if (i == 0)
                    tc.HorizontalAlignment = Element.ALIGN_LEFT;
                else
                    tc.HorizontalAlignment = Element.ALIGN_RIGHT;

                tb2.AddCell(tc);
            }
            //------------------------------------
            document.Add(tb2);
            document.Close();
            if (File.Exists(Server.MapPath("~\\CurrentYearBudget.pdf")))
            {
                File.Delete(Server.MapPath("~\\CurrentYearBudget.pdf"));
            }

            FileStream fs = new FileStream(Server.MapPath("~\\CurrentYearBudget.pdf"), FileMode.Create);
            byte[] bb = msReport.ToArray();
            fs.Write(bb, 0, bb.Length);
            fs.Flush();
            fs.Close();
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "a", "window.open('./CurrentYearBudget.pdf');", true);
        }
        catch (System.Exception ex)
        {

        }
    }
    protected void btnExportToPDF_OnClick(object sender, EventArgs e)
    {
        if (ViewState["DtForPDR"] != null)
        {
            DataTable DtForPDR = (DataTable)ViewState["DtForPDR"];
            ExportToPDF(ddlCompany.SelectedItem.Text + " ( Budget - " + System.DateTime.Now.ToString("dd-MMM-yyyy") + " )", DtForPDR);
        }
    }
}