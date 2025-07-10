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
using System.Text;
using System.IO;

public partial class CurrentYearBudgetReports : System.Web.UI.Page
{
    static Random R = new Random(); 
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
                ddlFleet.Items.Insert(0, new System.Web.UI.WebControls.ListItem("< Select >", "0"));
            }
        }
    }
    public void BindCompany()
    {
        string sql = "SELECT VW_sql_tblSMDPRCompany.Company, VW_sql_tblSMDPRCompany.ReportCo " +
            " ,(VW_sql_tblSMDPRCompany.Company + '-' + VW_sql_tblSMDPRCompany.[Company Name]) as CompName" +
        " FROM VW_sql_tblSMDPRCompany WHERE (((VW_sql_tblSMDPRCompany.InAccts)=1)) and (((VW_sql_tblSMDPRCompany.Active)='Y'))";
        DataTable DtCompany = Common.Execute_Procedures_Select_ByQuery(sql);
        if (DtCompany != null)
        {
            ddlCompany.DataSource = DtCompany;
            ddlCompany.DataTextField = "CompName";
            ddlCompany.DataValueField = "Company";
            ddlCompany.DataBind();
            ddlCompany.Items.Insert(0, new System.Web.UI.WebControls.ListItem("< Select >", "0"));
            ddlCompany.SelectedIndex = 0;

        }

    }
    public void BindVesselBYOwner()
    {
        string sql = "SELECT ROW_NUMBER() OVER (ORDER BY VW_sql_tblSMDPRVessels.ShipID) AS SNO,VW_sql_tblSMDPRVessels.ShipID as VesselCode, VW_sql_tblSMDPRVessels.Company, VW_sql_tblSMDPRVessels.ShipName as vesselName, " +
                    " (VW_sql_tblSMDPRVessels.ShipID+' - '+VW_sql_tblSMDPRVessels.ShipName)as ShipNameCode" +
                    " FROM VW_sql_tblSMDPRVessels " +
                    " WHERE (((VW_sql_tblSMDPRVessels.Company)='" + ddlCompany.SelectedValue + "')) AND ACTIVE='Y' and VesselNo in (Select VesselId from UserVesselRelation with(nolock) where Loginid = "+ Convert.ToInt32(Session["loginid"].ToString())+") ORDER BY VW_sql_tblSMDPRVessels.ShipID";
        DataTable DtVessel = Common.Execute_Procedures_Select_ByQuery(sql);
        if (DtVessel != null)
        {
            rptVessels.DataSource = DtVessel;
            rptVessels.DataBind();
            //--------------------
            ddlShip.DataSource = DtVessel;
            ddlShip.DataTextField = "ShipNameCode";
            ddlShip.DataValueField = "VesselCode";
            ddlShip.DataBind();
            ddlShip.Items.Insert(0, new System.Web.UI.WebControls.ListItem("<Select>", "0"));
        }

    }
    public void BindVesselBYFleet()
    {
        string sql = "";
        sql = "select ROW_NUMBER() OVER (ORDER BY VesselCode) AS SNO,VesselCode,vesselName, VesselCode + ' - ' + vesselName as ShipNameCode from Vessel where fleetID=" + ddlFleet.SelectedValue + " And VesselStatusid<>2  order by vesselName ";
        DataTable dtFleet = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        if (dtFleet != null)
        {
            if (dtFleet.Rows.Count >= 0)
            {
                rptVessels.DataSource = dtFleet;
                rptVessels.DataBind();
                //--------------------
                ddlShip.DataSource = dtFleet;
                ddlShip.DataTextField = "ShipNameCode";
                ddlShip.DataValueField = "VesselCode";
                ddlShip.DataBind();
                ddlShip.Items.Insert(0, new System.Web.UI.WebControls.ListItem("<Select>", "0"));
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //---------------------------------------
       ProjectCommon.SessionCheck();
        //---------------------------------------
        if (!IsPostBack)
        {
            BindFleet();
            BindCompany();
            lblyear.Text = (DateTime.Today.Year).ToString();
        }
    }
    protected void RadFC_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        ddlCompany.Visible = (radC.Checked);
        ddlFleet.Visible = (radF.Checked);
        lblFC.Text = (ddlCompany.Visible) ? "Company" : "Fleet";
    }
    protected void ddlFleet_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        BindVesselBYFleet();
    }
    protected void ddlCompany_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        BindVesselBYOwner();   
    }

    protected void btnPrntBudFore_Click(object sender, EventArgs e)
    {
        btnPrntBudFore.CssClass="selbtn";
        btnPrntSummary.CssClass = "btn1";
        dv_BudgetForecast.Visible = true;
        btnShow.Visible = false;
        radF.Enabled = true;
    }
    protected void btnPrntSummary_Click(object sender, EventArgs e)
    {
        btnPrntBudFore.CssClass = "btn1";
        btnPrntSummary.CssClass = "selbtn";
        dv_BudgetForecast.Visible = false;
        btnShow.Visible = true;
        radF.Enabled = true;
    }

    protected void btnShow_Click(object sender, EventArgs e)
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
        //lit1.Text = sb.ToString();



        if (btnPrntSummary.CssClass == "btnsel")
        {
            if (DtForPDR!= null)
            {
                //DataTable DtForPDR = (DataTable)ViewState["DtForPDR"];
                ExportToPDF(ddlCompany.SelectedItem.Text + " ( Budget - " + System.DateTime.Now.ToString("dd-MMM-yyyy") + " )", DtForPDR);
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
            if (OutValue.Trim() == "")
                OutValue = StrIn.Substring(Len - 3);
            else
                OutValue = StrIn.Substring(Len - 3) + "," + OutValue;

            StrIn = StrIn.Substring(0, Len - 3);
            Len = StrIn.Length;
        }
        OutValue = StrIn + "," + OutValue;
        if (OutValue.EndsWith(",")) { OutValue = OutValue.Substring(0, OutValue.Length - 1); }
        return OutValue;
    }    
    protected void btnPrint_Click(object sender, ImageClickEventArgs e)
    {
        string CompanyCode = "", CompanyName = "";
        string VesselCode = ((ImageButton)sender).CommandArgument.Trim();
        string VesselName = ((ImageButton)sender).ToolTip.Trim();
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM [dbo].[VW_sql_tblSMDPRCompany] WHERE COMPANY IN (SELECT COMPANY FROM [dbo].[VW_sql_tblSMDPRVessels] WHERE SHIPID='" + VesselCode + "')");
        if (dt.Rows.Count > 0)
        {
            CompanyCode = dt.Rows[0]["Company"].ToString();
            CompanyName = dt.Rows[0]["Company Name"].ToString();
        }
        
        string StartDate="",EndDate="";
        int Days=0;
        //---------------------------------            
        string Qry = "select YearDays,approvedby,replace(convert(varchar,approvedon,106),' ','-') as ApprovedOn,UpdatedBy, " +
                           "Importedby,replace(convert(varchar,ImportedOn,106),' ','-') as ImportedOn,replace(convert(varchar,UpdatedOn,106),' ','-') as UpdatedOn," +
                           "replace(convert(varchar,vessstart,106),' ','-') as vessstart,replace(convert(varchar,vessend,106),' ','-') as vessend " +
                           "from dbo.tblsmdbudgetforecastyear where cocode='" + ddlCompany.SelectedValue + "' and shipid='" + VesselCode + "' and [year]=" + (Common.CastAsInt32(lblyear.Text) - 1).ToString();
        DataTable dtheader = Common.Execute_Procedures_Select_ByQuery(Qry);
        if (dtheader.Rows.Count > 0)
        {
            StartDate = dtheader.Rows[0]["VessStart"].ToString();
            EndDate = dtheader.Rows[0]["VessEnd"].ToString();
            Days = Common.CastAsInt32(dtheader.Rows[0]["YearDays"]);
        }
        //---------------------------------
        string VesselPart=VesselCode + " - " +VesselName;
        string CompanyPart=CompanyCode + " - " +CompanyName;

        

        //string QrtString = "Print.aspx?BudgetForeCast=true&Comp=" + CompanyPart + "&Vessel=" + VesselCode + "&BType=< All >&StartDate=" + StartDate + "&EndDate=" + EndDate + "&year=" + lblyear.Text + "&YearDays=" + Days + "&MajCatID=6";
        string QrtString = "Print.aspx?CYBudget=true&Comp=" + CompanyPart + "&Vessel=" + VesselCode + " - " + VesselName + "&BType=< All >&StartDate=" + StartDate + "&EndDate=" + EndDate + "&year=" + lblyear.Text + "&YearDays=" + Days + "&MajCatID=0";
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "aa", "window.open('" + QrtString + "');", true);
        //---------------------------------
    }
    protected DataTable BindFleetView()
    {
        DataTable dt_Result = new DataTable();
        string VesselList="";
        foreach (RepeaterItem ri in rptVessels.Items)
        {
            VesselList += "," + ((ImageButton)ri.FindControl("btnPrint")).CommandArgument.Trim();
        }
        if (VesselList.StartsWith(","))
            VesselList = VesselList.Substring(1);
        string[] VesselArray;
        char[] sep = {','};
        VesselArray = VesselList.Split(sep);

        int[] ColumnSum;
        int[] VesselDays;

        ColumnSum = new int[VesselArray.Length];
        VesselDays = new int[VesselArray.Length];

        StringBuilder sb = new StringBuilder();
        DataTable dtAccts = Common.Execute_Procedures_Select_ByQuery("select distinct midcatid,midcat,MajSeqNo,MidSeqNo from dbo.v_New_CurrYearBudgetHome WHERE MajCatId=6 ORDER BY MajSeqNo,MidSeqNo");
        DataTable dtAccts1 = Common.Execute_Procedures_Select_ByQuery("select distinct midcatid,midcat,MajSeqNo,MidSeqNo from dbo.v_New_CurrYearBudgetHome WHERE MajCatId<>6 ORDER BY MajSeqNo,MidSeqNo");
        sb.Append("<table border='1' width='100%' cellpadding='3' height='390' style='font-size:10px;'");
        sb.Append("<tr>");
        sb.Append("<td class='header' style='font-size:10px;'>Budget Head<br/>Year Built</td>");
        dt_Result.Columns.Add("BudgetHead");
        string Ships = "";
        for (int i = 0; i < VesselArray.Length; i++)
        {
            Ships = Ships + ((Ships.Trim() == "") ? "" : ",") + VesselArray[i];
            //if (i != 0)
            //{
                sb.Append("<td class='header' style='font-size:10px;'><input type='radio' name='radVSL' value='" + VesselArray[i] + "' id='rad" + VesselArray[i] + "'><label for='rad" + VesselArray[i] + "'>" + VesselArray[i] + "</label>");
                DataTable dtDays = Common.Execute_Procedures_Select_ByQuery("select top 1 Days From dbo.v_New_CurrYearBudgetHome Where shipid='" + VesselArray[i] + "' AND year=" + (DateTime.Today.Year).ToString() + " order by days desc");
                if (dtDays != null)
                {
                    if (dtDays.Rows.Count > 0)
                    {
                        VesselDays[i] = Common.CastAsInt32(dtDays.Rows[0][0]);
                    }
                }

                DataTable dtYB = Common.Execute_Procedures_Select_ByQueryCMS("select yearbuilt from dbo.vessel Where vesselcode='" + VesselArray[i] + "'");
                if (dtYB != null)
                {
                    if (dtYB.Rows.Count > 0)
                    {
                        if (dtYB.Rows[0][0].ToString().Trim() != "")
                        {
                            sb.Append("</br> [" + dtYB.Rows[0][0].ToString() + "]");
                        }
                    }
                }

                sb.Append("</td>");
                dt_Result.Columns.Add(VesselArray[i]);
            //}
        }
        //hfd_Ships.Value = Ships;
        sb.Append("<td class='header' style='font-size:10px;'>Total</td>");
        dt_Result.Columns.Add("Total");
        sb.Append("</tr>");
        // YEAR BUILT
        //-----------------
        DataRow dr_yb = dt_Result.NewRow();
        dr_yb[0] = "Year Built";
        for (int j = 0; j < VesselArray.Length; j++)
        {
            //if (j != 0)
            //{
                DataTable dt_yb = Common.Execute_Procedures_Select_ByQueryCMS("select yearbuilt from vessel where vesselcode='" + VesselArray[j] + "'");
                if (dt_yb.Rows.Count > 0)
                {
                    dr_yb[VesselArray[j]] = "[ " + dt_yb.Rows[0][0].ToString() + " ]";
                }
            //}
        }
        dr_yb[dt_Result.Columns.Count - 1] = "";
        dt_Result.Rows.Add(dr_yb);
        //-----------------
        // DATA ROWS 
        for (int i = 0; i <= dtAccts.Rows.Count - 1; i++)
        {
            int RowSum = 0;
            DataRow dr = dt_Result.NewRow();
            sb.Append("<tr>");
            sb.Append("<td>" + dtAccts.Rows[i][1].ToString() + "</td>");
            dr[0] = dtAccts.Rows[i][1].ToString();
            for (int j = 0; j < VesselArray.Length; j++)
            {
                //if (j != 0)
                //{
                    DataTable dtShip = Common.Execute_Procedures_Select_ByQuery("select NEXTYEARFORECASTAMOUNT from dbo.v_New_CurrYearBudgetHome where shipid='" + VesselArray[j] + "' AND year=" + (DateTime.Today.Year - 1).ToString() + " and  midcatid=" + dtAccts.Rows[i][0].ToString());
                    if (dtShip != null)
                    {
                        if (dtShip.Rows.Count > 0)
                        {
                            sb.Append("<td style='text-align:right'>" + FormatCurrency(dtShip.Rows[0][0]) + "</td>");
                            ColumnSum[j] += Common.CastAsInt32(dtShip.Rows[0][0]);
                            RowSum += Common.CastAsInt32(dtShip.Rows[0][0]);
                            dr[VesselArray[j]] = FormatCurrency(dtShip.Rows[0][0]);
                        }
                        else
                        {
                            sb.Append("<td></td>");
                            dr[VesselArray[j]] = "0";
                        }
                    }
                    else
                    {
                        sb.Append("<td></td>");
                        dr[VesselArray[j]] = "0";
                    }
                //}
            }
            sb.Append("<td style='text-align:right'>" + FormatCurrency(RowSum) + "</td>");
            dr[dt_Result.Columns.Count - 1] = FormatCurrency(RowSum);
            sb.Append("</tr>");
            dt_Result.Rows.Add(dr);
        }
        //---------------
        // TOTAL
        int VSLhaving_TotalMoreThan0 = 0;
        DataRow dr1 = dt_Result.NewRow();
        sb.Append("<tr class='header' style='background-color :#C2C2C2;color:Black'>");
        sb.Append("<td style='font-size:10px;text-align:right;'>Total(US$)</td>");
        dr1[0] = "Total(US$)";
        int GrossSum = 0;
        for (int i = 0; i < VesselArray.Length; i++)
        {
            //if (i != 0)
            //{
                sb.Append("<td style='font-size:10px;text-align:right'>" + FormatCurrency(ColumnSum[i]) + "</td>");
                GrossSum += ColumnSum[i];
                dr1[i+1] = FormatCurrency(ColumnSum[i]); //====================================================
                if (ColumnSum[i] > 0)
                {
                    VSLhaving_TotalMoreThan0++;
                }
            //}
        }
        sb.Append("<td style='font-size:10px;text-align:right'>" + FormatCurrency(GrossSum) + "</td>");
        dr1[dt_Result.Columns.Count - 1] = FormatCurrency(GrossSum);
        sb.Append("</tr>");
        dt_Result.Rows.Add(dr1);

        // PER DAY CALC
        DataRow dr2 = dt_Result.NewRow();
        sb.Append("<tr class='header' style='background-color :#C2C2C2;color:Black'>");
        sb.Append("<td style='font-size:10px;text-align:right;'>Avg Daily Cost(US$)</td>");
        dr2[0] = "Avg Daily Cost(US$)";
        //int GrossSum = 0;
        for (int i = 0; i < VesselArray.Length; i++)
        {
            //if (i != 0)
            //{
                try
                {
                    sb.Append("<td style='font-size:10px;text-align:right'>" + FormatCurrency((ColumnSum[i] / VesselDays[i])) + "</td>");
                    dr2[i+1] = FormatCurrency((ColumnSum[i] / VesselDays[i])); //=================================================
                }
                catch (DivideByZeroException ex)
                {
                    sb.Append("<td style='font-size:10px;text-align:right'>0</td>");
                    dr2[i+1] = "0";//=================================================
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                //GrossSum += ColumnSum[i - 1];
            //}
        }
        if (VSLhaving_TotalMoreThan0 > 0)
        {
            sb.Append("<td style='font-size:10px;text-align:right'>" + FormatCurrency((GrossSum / 366) / VSLhaving_TotalMoreThan0) + "</td>");
            dr2[dt_Result.Columns.Count - 1] = FormatCurrency((GrossSum / 366) / VSLhaving_TotalMoreThan0);
        }
        else
        {
            sb.Append("<td style='font-size:10px;text-align:right'>" + FormatCurrency((GrossSum / 366)) + "</td>");
            dr2[dt_Result.Columns.Count - 1] = FormatCurrency((GrossSum / 366));
        }
        sb.Append("</tr>");
        dt_Result.Rows.Add(dr2);

        // DATA ROWS  - 2
        for (int i = 0; i <= dtAccts1.Rows.Count - 1; i++)
        {
            int RowSum = 0;
            DataRow dr = dt_Result.NewRow();
            sb.Append("<tr>");
            sb.Append("<td>" + dtAccts1.Rows[i][1].ToString() + "</td>");
            dr[0] = dtAccts1.Rows[i][1].ToString();
            for (int j = 0; j < VesselArray.Length; j++)
            {
                //if (j != 0)
                //{
                    DataTable dtShip = Common.Execute_Procedures_Select_ByQuery("select NEXTYEARFORECASTAMOUNT from dbo.v_New_CurrYearBudgetHome where shipid='" + VesselArray[j] + "' AND year=" + (DateTime.Today.Year - 1).ToString() + " and  midcatid=" + dtAccts1.Rows[i][0].ToString());
                    if (dtShip != null)
                    {
                        if (dtShip.Rows.Count > 0)
                        {
                            sb.Append("<td style='text-align:right'>" + FormatCurrency(dtShip.Rows[0][0]) + "</td>");
                            ColumnSum[j] += Common.CastAsInt32(dtShip.Rows[0][0]);
                            RowSum += Common.CastAsInt32(dtShip.Rows[0][0]);
                            dr[VesselArray[j]] = FormatCurrency(dtShip.Rows[0][0]);
                        }
                        else
                        {
                            sb.Append("<td></td>");
                            dr[VesselArray[j]] = "0";
                        }
                    }
                    else
                    {
                        sb.Append("<td></td>");
                        dr[VesselArray[j]] = "0";
                    }
                //}
            }
            sb.Append("<td style='text-align:right'>" + FormatCurrency(RowSum) + "</td>");
            sb.Append("</tr>");
            dr[dt_Result.Columns.Count - 1] = FormatCurrency(RowSum);
            dt_Result.Rows.Add(dr);
        }

        // GROSS TOTAL
        DataRow dr3 = dt_Result.NewRow();
        sb.Append("<tr class='header' style='background-color :#C2C2C2;color:Black'>");
        sb.Append("<td style='font-size:10px;text-align:right;'>Gross Total(US$)</td>");
        dr3[0] = "Gross Total(US$)";
        GrossSum = 0;
        for (int i = 0; i < VesselArray.Length; i++)
        {
            //if (i != 0)
            //{
                sb.Append("<td style='font-size:10px;text-align:right'>" + FormatCurrency(ColumnSum[i]) + "</td>");
                GrossSum += ColumnSum[i];
                dr3[i + 1] = FormatCurrency(ColumnSum[i]);//=================================================
            //}
        }
        sb.Append("<td style='font-size:10px;text-align:right'>" + FormatCurrency(GrossSum) + "</td>");
        dr3[dt_Result.Columns.Count - 1] = FormatCurrency(GrossSum);
        sb.Append("</tr>");
        dt_Result.Rows.Add(dr3);
        sb.Append("</table>");
        //--------------
        return dt_Result;
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
            if (File.Exists(Server.MapPath("~\\Budget_Forecast.pdf")))
            {
                File.Delete(Server.MapPath("~\\Budget_Forecast.pdf"));
            }

            FileStream fs = new FileStream(Server.MapPath("~\\Budget_Forecast.pdf"), FileMode.Create);
            byte[] bb = msReport.ToArray();
            fs.Write(bb, 0, bb.Length);
            fs.Flush();
            fs.Close();
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "a", "window.open('./Budget_Forecast.pdf?rnd=" + R.NextDouble().ToString() + "');", true);
        }
        catch (System.Exception ex)
        {

        }
    }
    //public string FormatCurrency(object InValue)
    //{
    //    string StrIn = InValue.ToString();
    //    string OutValue = "";
    //    int Len = StrIn.Length;
    //    while (Len > 3)
    //    {
    //        if (OutValue.Trim() == "")
    //            OutValue = StrIn.Substring(Len - 3);
    //        else
    //            OutValue = StrIn.Substring(Len - 3) + "," + OutValue;

    //        StrIn = StrIn.Substring(0, Len - 3);
    //        Len = StrIn.Length;
    //    }
    //    OutValue = StrIn + "," + OutValue;
    //    if (OutValue.EndsWith(",")) { OutValue = OutValue.Substring(0, OutValue.Length - 1); }
    //    return OutValue;
    //}
}
    
    
