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

public partial class NextYearBudgetReports : System.Web.UI.Page
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
                    " WHERE (((VW_sql_tblSMDPRVessels.Company)='" + ddlCompany.SelectedValue + "')) AND ACTIVE='A' ORDER BY VW_sql_tblSMDPRVessels.ShipID";
        DataTable DtVessel = Common.Execute_Procedures_Select_ByQuery(sql);
        if (DtVessel != null)
        {
            rptVessels.DataSource = DtVessel;
            rptVessels.DataBind();
        }

    }
    public void BindVesselBYFleet()
    {
        string sql = "";
        sql = "select ROW_NUMBER() OVER (ORDER BY VesselCode) AS SNO,VesselCode,vesselName from Vessel where fleetID=" + ddlFleet.SelectedValue + " And VesselStatusid<>2  order by vesselName ";
        DataTable dtFleet = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        if (dtFleet != null)
        {
            if (dtFleet.Rows.Count >= 0)
            {
                rptVessels.DataSource = dtFleet;
                rptVessels.DataBind();
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
            lblyear.Text = (DateTime.Today.Year + 1).ToString();
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

    protected void FleetSummary_ByVessel()
    {
        DateTime FromDate = new DateTime((DateTime.Today.Year + 1), 1, 1);
        DateTime ToDate = new DateTime((DateTime.Today.Year + 1),12,31);
        string Days = "[" + (ToDate.Subtract(FromDate).Days + 1).ToString() + "] days";
        //---------------------------------
        DataTable dt = BindFleetView();
        ExportToPDF("Fleet Summary By Vessel\n" + ddlFleet.SelectedItem.Text + " ( Budget - " + lblyear.Text + " )\n" + Days, dt);
        //---------------------------------
    }
    protected void CompanySummary_ByVessel()
    {
        DateTime FromDate = new DateTime((DateTime.Today.Year + 1), 1, 1);
        DateTime ToDate = new DateTime((DateTime.Today.Year + 1), 12, 31);
        string Days = "[" + (ToDate.Subtract(FromDate).Days + 1).ToString() + "] days";
        //---------------------------------
        DataTable dt = BindFleetView();
        ExportToPDF("Fleet Summary By Vessel\n" + ddlCompany.SelectedItem.Text + " ( Budget - " + lblyear.Text + " )\n" + Days, dt);
        //---------------------------------
    }

    protected void BinddATA(String CoCode, string VeselCode, int BYear, ref int retDaysCnt, ref int retDateCntLast, ref DataTable Dt1, ref DataTable Dt2, DataTable dtAccts, DataTable dtAccts1, DataTable dtActCom_Proj)
    {
        int TodayYear = BYear;
        string Qry = "select FORECAST from dbo.tblsmdbudgetforecastyear where cocode='" + CoCode + "' AND YEAR=" + (BYear - 2).ToString() + " AND ACCOUNTNUMBER=5100 AND SHIPID='" + VeselCode + "'";
        DataTable dtheader = Common.Execute_Procedures_Select_ByQuery(Qry);
        int Amt_5100 = 0;
        if (dtheader.Rows.Count > 0)
        {
            Amt_5100 = Common.CastAsInt32(dtheader.Rows[0][0]);

            if (Amt_5100 > 0)
                Amt_5100 = ProjectCommon.Get_ManningAmount(CoCode, VeselCode, BYear);
        }
       
        //---------------------------
        DataTable dt = new DataTable();
        DataTable dt_1 = new DataTable();
        dt.Columns.Add("MidCatId");
        dt.Columns.Add("AccountHead");
        dt.Columns.Add("ActComm");
        dt.Columns.Add("Proj");
        dt.Columns.Add("Bud");
        dt.Columns.Add("Fcast");
        dt.Columns.Add("Var1");
        dt.Columns.Add("Var2");
        dt.Columns.Add("Var3");
        dt.Columns.Add("Comments");
        //---------------------------
        dt_1.Columns.Add("MidCatId");
        dt_1.Columns.Add("AccountHead");
        dt_1.Columns.Add("ActComm");
        dt_1.Columns.Add("Proj");
        dt_1.Columns.Add("Bud");
        dt_1.Columns.Add("Fcast");
        dt_1.Columns.Add("Var1");
        dt_1.Columns.Add("Var2");
        dt_1.Columns.Add("Var3");
        dt_1.Columns.Add("Comments");
        //---------------------------
        string[] Names = { "Manning :", "Consumables :", "Lube Oils :", "Spare, Maintenance & Repair :", "General Expenses :", "Insurance :", "Management & Admin Fee:" };
        string[] Names1 = { "Damage / Repairs :", "Principle Controlled Expenses :", "Capital Expenditure :", "Dry Docking :" };
        int start = 2;

        decimal ColumnSum = 0, ColumnSumLast = 0;
        decimal ColumnSum2 = 0, ColumnSum3 = 0;
        //---------------------------
        DataTable dtDays = Common.Execute_Procedures_Select_ByQuery("select top 1 Days From dbo.v_New_CurrYearBudgetHome Where shipid='" + VeselCode + "' AND year=" + TodayYear.ToString() + " order by days desc");
        DataTable dtDaysLast = Common.Execute_Procedures_Select_ByQuery("select top 1 Days From dbo.v_New_CurrYearBudgetHome Where shipid='" + VeselCode + "' AND year=" + (TodayYear - 1).ToString() + " order by days desc");

        int DaysCnt = 1;
        if (dtDays != null)
        {
            if (dtDays.Rows.Count > 0)
            {
                DaysCnt = Common.CastAsInt32(dtDays.Rows[0][0]);
            }
        }
        int DaysCntLast = 1;
        if (dtDaysLast != null)
        {
            if (dtDaysLast.Rows.Count > 0)
            {
                DaysCntLast = Common.CastAsInt32(dtDaysLast.Rows[0][0]);
            }
        }
        retDaysCnt = DaysCnt;
        retDateCntLast = DaysCntLast;
        //---------------------------
        //------------------
        //DataTable dtActCom_Proj = Common.Execute_Procedures_Select_ByQuery("EXEC [dbo].[fn_NEW_GETCMBUDGETACTUAL_MIDCATWISE] '" + CoCode + "',12," + (TodayYear).ToString() + ",'" + VeselCode + "'");
        DataView dv = dtActCom_Proj.DefaultView;
         //---------------------------
        for (int i = 0; i <= dtAccts.Rows.Count - 1; i++)
        {
            int RowSum = 0;
            dv.RowFilter = "MidCatId=" + dtAccts.Rows[i][0].ToString();
            DataTable dt1 = dv.ToTable();
            decimal ActComm = 0, Projected = 0, Budget = 0, ForeCast = 0;

            if (i != 0) { Amt_5100 = 0; }
            if (Amt_5100 > 0)
            {
                ColumnSum3 += Amt_5100;

                ActComm = 0;
                ColumnSum2 += Common.CastAsInt32(dt1.Rows[0]["ACT_CONS"]);
                ActComm = Common.CastAsDecimal(dt1.Rows[0]["ACT_CONS"]);

                Projected = Amt_5100;
            }
            else
            {
                if (dt1.Rows.Count > 0)
                {
                    ColumnSum2 += Common.CastAsInt32(dt1.Rows[0]["ACT_CONS"]);
                    ColumnSum3 += Common.CastAsInt32(dt1.Rows[0]["PROJECTED"]);

                    ActComm = Common.CastAsDecimal(dt1.Rows[0]["ACT_CONS"]);
                    Projected = Common.CastAsDecimal(dt1.Rows[0]["PROJECTED"]);
                }
                else
                {
                    ActComm = 0;
                    Projected = 0;
                }
            }
            DataTable dtShip = Common.Execute_Procedures_Select_ByQuery("select budget,nextyearforecastamount from dbo.v_New_CurrYearBudgetHome where shipid='" + VeselCode + "' AND year=" + (TodayYear - 1).ToString() + " and  midcatid=" + dtAccts.Rows[i][0].ToString());
            if (dtShip != null)
            {
                if (dtShip.Rows.Count > 0)
                {
                    ColumnSum += Common.CastAsInt32(dtShip.Rows[0]["budget"]);
                    Budget = Common.CastAsDecimal(dtShip.Rows[0]["budget"]);
                    //---------------------
                    ColumnSumLast += Common.CastAsInt32(dtShip.Rows[0]["nextyearforecastamount"]);
                    ForeCast = Common.CastAsDecimal(dtShip.Rows[0]["nextyearforecastamount"]);
                }
                else
                {
                    Budget = 0;
                    ForeCast = 0;
                }
            }
            else
            {
                Budget = 0;
                ForeCast = 0;
            }

            DataRow dr = dt.NewRow();
            dr["MidcatId"] = dtAccts.Rows[i][0].ToString();
            dr["AccountHead"] = Names[i];
            dr["ActComm"] = ActComm.ToString();
            dr["Proj"] = Projected.ToString();
            dr["Bud"] = Budget.ToString();
            dr["Fcast"] = ForeCast.ToString();

            if (Budget > 0)
                ActComm = ((Projected - Budget) / Budget) * 100;
            else
                ActComm = 0;

            if (Projected > 0)
                Projected = ((ForeCast - Projected) / Projected) * 100;
            else
                Projected = 0;

            if (Budget > 0)
                Budget = ((ForeCast - Budget) / Budget) * 100;
            else
                Budget = 0;

            start++;

            dr["Var1"] = Math.Round(ActComm, 0);
            dr["Var2"] = Math.Round(Budget, 0);
            dr["Var3"] = Math.Round(Projected, 0);
            dt.Rows.Add(dr);
        }

        start++;
        //----------------------------------------------
        for (int i = 0; i <= dtAccts1.Rows.Count - 1; i++)
        {
            int RowSum = 0;
            dv.RowFilter = "MidCatId=" + dtAccts1.Rows[i][0].ToString();
            DataTable dt1 = dv.ToTable();
            decimal ActComm = 0, Projected = 0, Budget = 0, ForeCast = 0;

            if (dt1.Rows.Count > 0)
            {
                ColumnSum2 += Common.CastAsInt32(dt1.Rows[0]["ACT_CONS"]);
                ColumnSum3 += Common.CastAsInt32(dt1.Rows[0]["PROJECTED"]);

                ActComm = Common.CastAsDecimal(dt1.Rows[0]["ACT_CONS"]);
                Projected = Common.CastAsDecimal(dt1.Rows[0]["PROJECTED"]);
            }
            else
            {
                ActComm = 0;
                Projected = 0;
            }

            DataTable dtShip = Common.Execute_Procedures_Select_ByQuery("select budget,nextyearforecastamount from dbo.v_New_CurrYearBudgetHome where shipid='" + VeselCode + "' AND year=" + (TodayYear - 1).ToString() + " and  midcatid=" + dtAccts1.Rows[i][0].ToString());
            if (dtShip != null)
            {
                if (dtShip.Rows.Count > 0)
                {
                    ColumnSum += Common.CastAsInt32(dtShip.Rows[0]["budget"]);
                    Budget = Common.CastAsDecimal(dtShip.Rows[0]["budget"]);
                    //----------------
                    ColumnSumLast += Common.CastAsInt32(dtShip.Rows[0]["nextyearforecastamount"]);
                    ForeCast = Common.CastAsDecimal(dtShip.Rows[0]["nextyearforecastamount"]);
                }
                else
                {
                    Budget = 0;
                    ForeCast = 0;
                }
            }
            else
            {
                Budget = 0;
                ForeCast = 0;
            }

        
            DataRow dr = dt_1.NewRow();
            dr["MidcatId"] = dtAccts1.Rows[i][0].ToString();
            dr["AccountHead"] = Names1[i];
            dr["ActComm"] = ActComm.ToString();
            dr["Proj"] = Projected.ToString();
            dr["Bud"] = Budget.ToString();
            dr["Fcast"] = ForeCast.ToString();


            if (Budget > 0)
                ActComm = ((Projected - Budget) / Budget) * 100;
            else
                ActComm = 0;

            if (Projected > 0)
                Projected = ((ForeCast - Projected) / Projected) * 100;
            else
                Projected = 0;

            if (Budget > 0)
                Budget = ((ForeCast - Budget) / Budget) * 100;
            else
                Budget = 0;

            start++;

            dr["Var1"] = Math.Round(ActComm, 0);
            dr["Var2"] = Math.Round(Budget, 0);
            dr["Var3"] = Math.Round(Projected, 0);
            dt_1.Rows.Add(dr);
        }
        Dt1 = dt;
        Dt2 = dt_1;
    }
    protected void Calculate_Variance(decimal ActComm, decimal Budget, decimal Projected, decimal ForeCast, ref string var1, ref string var2, ref string var3)
    {
        if (Budget > 0)
            ActComm = ((Projected - Budget) / Budget) * 100;
        else
            ActComm = 0;

        if (Budget > 0)
            Budget = ((ForeCast - Budget) / Budget) * 100;
        else
            Budget = 0;

        if (Projected > 0)
            Projected = ((ForeCast - Projected) / Projected) * 100;
        else
            Projected = 0;

        var1 = Math.Round(ActComm, 0).ToString() + "%";
        var2 = Math.Round(Budget, 0).ToString() + "%";
        var3 = Math.Round(Projected, 0).ToString() + "%";
    }
    protected void PrintCompanySummary()
    {
            int DaysTillyearEnd = new DateTime(Common.CastAsInt32(lblyear.Text) - 1, 12, 31).Subtract(DateTime.Today).Days;
            int LastYear = Common.CastAsInt32(lblyear.Text) - 1;
            string CoCode = ddlCompany.SelectedValue;
            string VesselCode = "";
            //------------------
            DataTable dtAccts = Common.Execute_Procedures_Select_ByQuery("select distinct midcatid,midcat,MajSeqNo,MidSeqNo from dbo.v_New_CurrYearBudgetHome WHERE MajCatId=6 ORDER BY MajSeqNo,MidSeqNo");
            DataTable dtAccts1 = Common.Execute_Procedures_Select_ByQuery("select distinct midcatid,midcat,MajSeqNo,MidSeqNo from dbo.v_New_CurrYearBudgetHome WHERE MajCatId<>6 And MidcatId not in (25,26) ORDER BY MajSeqNo,MidSeqNo");
            //------------------
            DataTable dtActCom_Proj_Comp = Common.Execute_Procedures_Select_ByQuery("EXEC [dbo].[fn_NEW_GETCMBUDGETACTUAL_MIDCATWISE_COMP] '" + CoCode + "'," + DateTime.Today.Month.ToString() + "," + (LastYear).ToString() + "");
            DataView dv_dtActCom_Proj_Comp=dtActCom_Proj_Comp.DefaultView;
            //------------------

            Common.Set_Procedures("ExecQuery");
            Common.Set_ParameterLength(1);
            DataTable dt = new DataTable();
            DataTable dt_1 = new DataTable();
            dt.Columns.Add("MidCatId");
            dt.Columns.Add("AccountHead");
            dt.Columns.Add("ActComm");
            dt.Columns.Add("Proj");
            dt.Columns.Add("Bud");
            dt.Columns.Add("Fcast");
            dt.Columns.Add("Var1");
            dt.Columns.Add("Var2");
            dt.Columns.Add("Var3");
            dt.Columns.Add("Comments");
            //---------------------------
            dt_1.Columns.Add("MidCatId");
            dt_1.Columns.Add("AccountHead");
            dt_1.Columns.Add("ActComm");
            dt_1.Columns.Add("Proj");
            dt_1.Columns.Add("Bud");
            dt_1.Columns.Add("Fcast");
            dt_1.Columns.Add("Var1");
            dt_1.Columns.Add("Var2");
            dt_1.Columns.Add("Var3");
            dt_1.Columns.Add("Comments");
            //---------------------------

            string[] NamesId = { "12 :", "4:", "27", "14", "28", "11", "22" };
            string[] NamesId1 = { "5", "13", "2", "6" };

            string[] Names = { "Manning :", "Consumables :", "Lube Oils :", "Spare, Maintenance & Repair :", "General Expenses :", "Insurance :", "Management & Admin Fee:" };
            string[] Names1 = { "Damage / Repairs :", "Principle Controlled Expenses :", "Capital Expenditure :", "Dry Docking :" };

            for (int i = 0; i <= Names.Length - 1; i++)
            {
                dt.Rows.Add(dt.NewRow());
                dt.Rows[i][0] = NamesId[i].ToString();
                dt.Rows[i][1] = Names[i].ToString();
            }

            for (int i = 0; i <= Names1.Length - 1; i++)
            {
                dt_1.Rows.Add(dt_1.NewRow());
                dt_1.Rows[i][0] = NamesId1[i].ToString();
                dt_1.Rows[i][1] = Names1[i].ToString();
            }

            DataTable Temp1 = new DataTable();
            DataTable Temp2 = new DataTable();

          

            DataTable dtShips = Common.Execute_Procedures_Select_ByQuery("Select VesselCode As ShipID,  AccontCompany As Company from Vessel with(nolock)  WHERE AccontCompany='" + CoCode + "'");

            int retDaysCnt_Total = 0;
            int retDaysCntLast_Total = 0;
            int Temp_retDaysCnt = 0;
            int Temp_retDaysCntLast = 0;
            int Temp_DaysTilltoday = 0;

            foreach (DataRow dr in dtShips.Rows)
            {

                VesselCode = dr["SHIPID"].ToString();
                dv_dtActCom_Proj_Comp.RowFilter="ShipId='" + VesselCode + "'";
                DataTable dtActCom_Proj=dv_dtActCom_Proj_Comp.ToTable();

                BinddATA(CoCode, VesselCode, LastYear, ref Temp_retDaysCnt, ref Temp_retDaysCntLast, ref Temp1, ref Temp2, dtAccts, dtAccts1, dtActCom_Proj);
                retDaysCnt_Total += Temp_retDaysCnt;
                retDaysCntLast_Total += Temp_retDaysCntLast;

                //------- New Line
                int DaysTilltoday = Temp_retDaysCntLast - DaysTillyearEnd;
                if (DaysTilltoday > 365)
                    DaysTilltoday = 365;

                Temp_DaysTilltoday += DaysTilltoday;
                //-------------------------

                for (int i = 0; i <= Temp1.Rows.Count - 1; i++)
                {
                    dt.Rows[i][2] = Common.CastAsInt32(dt.Rows[i][2]) + Common.CastAsInt32(Temp1.Rows[i][2]);
                    dt.Rows[i][3] = Common.CastAsInt32(dt.Rows[i][3]) + Common.CastAsInt32(Temp1.Rows[i][3]);
                    dt.Rows[i][4] = Common.CastAsInt32(dt.Rows[i][4]) + Common.CastAsInt32(Temp1.Rows[i][4]);
                    dt.Rows[i][5] = Common.CastAsInt32(dt.Rows[i][5]) + Common.CastAsInt32(Temp1.Rows[i][5]);
                }

                for (int i = 0; i <= Temp2.Rows.Count - 1; i++)
                {
                    dt_1.Rows[i][2] = Common.CastAsInt32(dt_1.Rows[i][2]) + Common.CastAsInt32(Temp2.Rows[i][2]);
                    dt_1.Rows[i][3] = Common.CastAsInt32(dt_1.Rows[i][3]) + Common.CastAsInt32(Temp2.Rows[i][3]);
                    dt_1.Rows[i][4] = Common.CastAsInt32(dt_1.Rows[i][4]) + Common.CastAsInt32(Temp2.Rows[i][4]);
                    dt_1.Rows[i][5] = Common.CastAsInt32(dt_1.Rows[i][5]) + Common.CastAsInt32(Temp2.Rows[i][5]);
                }
            }
            //-------------------------- CALCULATING VARIANCE
            string Var1 = "", Var2 = "", Var3 = "";
            decimal ColumnSum_ActComm = 0;
            decimal ColumnSum_Budget = 0;
            decimal ColumnSum_Projected = 0;
            decimal ColumnSum_ForeCast = 0;

            foreach (DataRow dr in dt.Rows)
            {
                decimal ActComm = Common.CastAsDecimal(dr["ActComm"]);
                decimal Budget = Common.CastAsDecimal(dr["Bud"]);
                decimal Projected = Common.CastAsDecimal(dr["Proj"]);
                decimal ForeCast = Common.CastAsDecimal(dr["Fcast"]);

                ColumnSum_ActComm += ActComm;
                ColumnSum_Budget += Budget;
                ColumnSum_Projected += Projected;
                ColumnSum_ForeCast += ForeCast;
                //5696

                Calculate_Variance(ActComm, Budget, Projected, ForeCast, ref Var1, ref Var2, ref Var3);
                dr["Var1"] = Var1;
                dr["Var2"] = Var2;
                dr["Var3"] = Var3;
            }
            //--------- TOTAL 
            dt.Rows.Add(dt.NewRow());
            dt.Rows[dt.Rows.Count - 1][0] = "";
            dt.Rows[dt.Rows.Count - 1][1] = "Total (US$): ";
            dt.Rows[dt.Rows.Count - 1]["ActComm"] = ColumnSum_ActComm.ToString();
            dt.Rows[dt.Rows.Count - 1]["Bud"] = ColumnSum_Budget.ToString();
            dt.Rows[dt.Rows.Count - 1]["Proj"] = ColumnSum_Projected.ToString();
            dt.Rows[dt.Rows.Count - 1]["Fcast"] = ColumnSum_ForeCast.ToString();
            Calculate_Variance(ColumnSum_ActComm, ColumnSum_Budget, ColumnSum_Projected, ColumnSum_ForeCast, ref Var1, ref Var2, ref Var3);
            dt.Rows[dt.Rows.Count - 1]["Var1"] = Var1;
            dt.Rows[dt.Rows.Count - 1]["Var2"] = Var2;
            dt.Rows[dt.Rows.Count - 1]["Var3"] = Var3;
            //--------- AVERAGE 
            dt.Rows.Add(dt.NewRow());
            dt.Rows[dt.Rows.Count - 1][0] = "";
            dt.Rows[dt.Rows.Count - 1][1] = "Avg Daily Cost(US$): ";

            //dt.Rows[dt.Rows.Count - 1]["ActComm"] = Math.Round(ColumnSum_ActComm / retDaysCntLast_Total).ToString();
            dt.Rows[dt.Rows.Count - 1]["ActComm"] = Math.Round(ColumnSum_ActComm / Temp_DaysTilltoday).ToString();
        
            dt.Rows[dt.Rows.Count - 1]["Bud"] = Math.Round(ColumnSum_Budget / retDaysCntLast_Total).ToString();
            dt.Rows[dt.Rows.Count - 1]["Proj"] = Math.Round(ColumnSum_Projected / retDaysCntLast_Total).ToString();
            dt.Rows[dt.Rows.Count - 1]["Fcast"] = Math.Round(ColumnSum_ForeCast / retDaysCnt_Total).ToString();

            //---------  IIND TABLE
            foreach (DataRow dr in dt_1.Rows)
            {
                decimal ActComm = Common.CastAsDecimal(dr["ActComm"]);
                decimal Budget = Common.CastAsDecimal(dr["Bud"]);
                decimal Projected = Common.CastAsDecimal(dr["Proj"]);
                decimal ForeCast = Common.CastAsDecimal(dr["Fcast"]);

                ColumnSum_ActComm += ActComm;
                ColumnSum_Budget += Budget;
                ColumnSum_Projected += Projected;
                ColumnSum_ForeCast += ForeCast;

                Calculate_Variance(ActComm, Budget, Projected, ForeCast, ref Var1, ref Var2, ref Var3);
                dr["Var1"] = Var1;
                dr["Var2"] = Var2;
                dr["Var3"] = Var3;
            }
            //--------- TOTAL 
            dt_1.Rows.Add(dt_1.NewRow());
            dt_1.Rows[dt_1.Rows.Count - 1][0] = "";
            dt_1.Rows[dt_1.Rows.Count - 1][1] = "Gross Total (US$): ";
            dt_1.Rows[dt_1.Rows.Count - 1]["ActComm"] = ColumnSum_ActComm.ToString();
            dt_1.Rows[dt_1.Rows.Count - 1]["Bud"] = ColumnSum_Budget.ToString();
            dt_1.Rows[dt_1.Rows.Count - 1]["Proj"] = ColumnSum_Projected.ToString();
            dt_1.Rows[dt_1.Rows.Count - 1]["Fcast"] = ColumnSum_ForeCast.ToString();
            Calculate_Variance(ColumnSum_ActComm, ColumnSum_Budget, ColumnSum_Projected, ColumnSum_ForeCast, ref Var1, ref Var2, ref Var3);
            dt_1.Rows[dt_1.Rows.Count - 1]["Var1"] = Var1;
            dt_1.Rows[dt_1.Rows.Count - 1]["Var2"] = Var2;
            dt_1.Rows[dt_1.Rows.Count - 1]["Var3"] = Var3;
            //--------- 

            //Session["d11"] = dt;
            //Session["d22"] = dt_1;
            //Session["DaysCnt"] = retDaysCnt_Total;
            //Session["DaysCntLast"] = retDaysCntLast_Total;

            //Page.ClientScript.RegisterStartupScript(this.GetType(), "a", "window.open('ComapnyBFCSummaryReport.aspx?Comp="  + ddlCompany.SelectedItem.Text  +  "&Year=" + lblyear.Text  +  "');", true);

            dt.Columns.RemoveAt(0);
            dt_1.Columns.RemoveAt(0);

            dt.Columns.RemoveAt(dt.Columns.Count - 1);
            dt_1.Columns.RemoveAt(dt_1.Columns.Count - 1);

            ExportToPDF_CompanySummary(ddlCompany.SelectedItem.Text, dt, dt_1,Common.CastAsInt32(lblyear.Text));
    }
    private void ExportToPDF_CompanySummary(string CompanyName, DataTable dt, DataTable dt_1, int ForeCastYear)
    {
        try
        {
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
            p2.Add(new Phrase("Fleet Budget Summary" + "\n" + CompanyName + "\n" + "Year-" + ForeCastYear.ToString() + "\n", FontFactory.GetFont("ARIAL", 12, iTextSharp.text.Font.BOLD)));
            Cell c2 = new Cell(p2);
            c2.HorizontalAlignment = Element.ALIGN_CENTER;
            tb_header.AddCell(c2);

            
            HeaderFooter header = new HeaderFooter(new Phrase(""), false);
            document.Header = header;

            header.Border = iTextSharp.text.Rectangle.NO_BORDER;
            string foot_Txt = "Published On : " + DateTime.Today.ToString("dd-MMM-yyyy");
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
            iTextSharp.text.Font fCapText_5 = FontFactory.GetFont("ARIAL", 8, iTextSharp.text.Font.NORMAL);
            //------------ TABLE HEADER ROW 
            int ColumnsCount = dt.Columns.Count;
            iTextSharp.text.Table tb1 = new iTextSharp.text.Table(ColumnsCount);
            tb1.BackgroundColor = iTextSharp.text.Color.LIGHT_GRAY;
            tb1.Width = 100;

            //float[] ws = new float[ColumnsCount];
            //ws[0] = 15;
            //for (int i = 2; i <= ws.Length - 1; i++)
            //    ws[i] = 75 / (ws.Length - 1);

            //ws[ws.Length - 2] = ws[ws.Length - 2] + 5;
            //ws[ws.Length - 1] = ws[ws.Length - 1] + 5;
            float[] ws = { 20, 10, 10, 10, 10, 10, 15, 15 };
            tb1.Widths = ws;

            tb1.Alignment = Element.ALIGN_CENTER;
            tb1.Border = iTextSharp.text.Rectangle.NO_BORDER;
            tb1.DefaultHorizontalAlignment = Element.ALIGN_CENTER;
            tb1.BorderColor = iTextSharp.text.Color.WHITE;
            tb1.Cellspacing = 1;
            tb1.Cellpadding = 1;

            string[] colnamames = {   "",
                                      "[" + (ForeCastYear-1).ToString() + "]",
                                      "[" + (ForeCastYear-1).ToString() + "]",
                                      "[" + (ForeCastYear-1).ToString() + "]",
                                      "[" + ForeCastYear.ToString() + "]",
                                      (ForeCastYear-1).ToString() + "- Var % ",
                                      ForeCastYear.ToString() + " - Forecast Var%",
                                      ForeCastYear.ToString() + " - Forecast Var%"
                                   };

            string[] colnamames1 = {  "",
                                      "",
                                      "",
                                      "",
                                      "",
                                      "",
                                      "v/s",
                                      "v/s",
                                   };
            string[] colnamames2 = {  "AccountHead",
                                      "Act & Comm",
                                      "Projected",
                                      "Budget",
                                      "Forecast",
                                      "[ B v/s P ]",
                                      (ForeCastYear-1).ToString() + " - [B]",
                                      (ForeCastYear-1).ToString() + " - [P]",
                                   };

            for (int i = 0; i <= colnamames.Length - 1; i++)
            {
                Cell tc = new Cell(new Phrase(colnamames[i], fCapText));
                tb1.AddCell(tc);
            }
            for (int i = 0; i <= colnamames1.Length - 1; i++)
            {
                Cell tc = new Cell(new Phrase(colnamames1[i], fCapText));
                tb1.AddCell(tc);
            }
            for (int i = 0; i <= colnamames2.Length - 1; i++)
            {
                Cell tc = new Cell(new Phrase(colnamames2[i], fCapText));
                tb1.AddCell(tc);
            }

            document.Add(tb1);
            //------------ TABLE DATA ROW 
            iTextSharp.text.Table tbdata = new iTextSharp.text.Table(ColumnsCount);
            tbdata.Width = 100;
            tbdata.Widths = ws;
            tbdata.Alignment = Element.ALIGN_CENTER;
            tbdata.Border = iTextSharp.text.Rectangle.NO_BORDER;
            tbdata.DefaultHorizontalAlignment = Element.ALIGN_CENTER;
            tbdata.BorderColor = iTextSharp.text.Color.GRAY;
            tbdata.Cellspacing = 1;
            tbdata.Cellpadding = 1;
            for (int k = 0; k <= dt.Rows.Count - 1; k++)
            {
                DataRow dr = dt.Rows[k];
                for (int i = 0; i <= dt.Columns.Count - 1; i++)
                {
                    Cell tc = new Cell(new Phrase(dr[i].ToString(), fCapText_5));
                    if (k == 7 || k == 8)
                    {
                        tc.BackgroundColor = iTextSharp.text.Color.LIGHT_GRAY;
                    }
                    tc.HorizontalAlignment = Element.ALIGN_RIGHT;
                    tbdata.AddCell(tc);
                }
            }
            //--------------
            for (int k = 0; k <= dt_1.Rows.Count - 1; k++)
            {
                DataRow dr = dt_1.Rows[k];
                for (int i = 0; i <= dt_1.Columns.Count - 1; i++)
                {
                    Cell tc = new Cell(new Phrase(dr[i].ToString(), fCapText_5));
                    if (k == 4)
                    {
                        tc.BackgroundColor = iTextSharp.text.Color.LIGHT_GRAY;
                    }
                    tc.HorizontalAlignment = Element.ALIGN_RIGHT;
                    tbdata.AddCell(tc);
                }
            }

            document.Add(tbdata);
           
            document.Close();
            string PublishPath = Server.MapPath("~/");
            string FileName = PublishPath + "\\" + "Company_Summary.pdf";
            if (File.Exists(FileName))
            {
                File.Delete(FileName);
            }

            FileStream fs = new FileStream(FileName, FileMode.Create);
            byte[] bb = msReport.ToArray();
            fs.Write(bb, 0, bb.Length);
            fs.Flush();
            fs.Close();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "a", "window.open('Company_Summary.pdf?r=" + R.NextDouble().ToString() + "');", true);
        }
        catch (System.Exception ex)
        {

        }
    }
    protected void btnPrntBudFore_Click(object sender, EventArgs e)
    {
        btnPrntBudFore.CssClass="btnsel";
        btnPrntSummary.CssClass = "btn1";
        btnCompSummary.CssClass = "btn1";
        dv_BudgetForecast.Visible = true;
        btnShow.Visible = false;
        radF.Enabled = true;
    }
    protected void btnPrntSummary_Click(object sender, EventArgs e)
    {
        btnPrntBudFore.CssClass = "btn1";
        btnPrntSummary.CssClass = "btnsel";
        btnCompSummary.CssClass = "btn1";
        dv_BudgetForecast.Visible = false;
        btnShow.Visible = true;
        radF.Enabled = true;
    }
    protected void btnCompSummary_Click(object sender, EventArgs e)
    {
        btnPrntBudFore.CssClass = "btn1";
        btnPrntSummary.CssClass = "btn1";
        btnCompSummary.CssClass = "btnsel";
        dv_BudgetForecast.Visible = false;
        btnShow.Visible = true;
        radC.Checked = true;
        radF.Checked = false;
        radF.Enabled = false;
        RadFC_OnSelectedIndexChanged(sender, e);
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {   
        if (btnPrntSummary.CssClass == "btnsel" || btnCompSummary.CssClass == "btnsel")
        {
            if (ddlCompany.Visible)
            {
                if (ddlCompany.SelectedIndex <= 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "anc", "alert('Please select Company.');", true);
                    ddlCompany.Focus();
                }
                else
                {
                    if (btnPrntSummary.CssClass == "btnsel")
                    {
                        CompanySummary_ByVessel();
                    }
                    else
                    {
                        PrintCompanySummary();
                    }
                }
            }

            if (ddlFleet.Visible)
            {
                if (ddlFleet.SelectedIndex <= 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "anc", "alert('Please select Fleet.');", true);
                    ddlFleet.Focus();
                }
                else
                {
                    if (btnPrntSummary.CssClass == "btnsel")
                    {
                        FleetSummary_ByVessel();
                    }
                }
            }
        }
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        string VesselCode = ((ImageButton)sender).CommandArgument.Trim();
        string VesselName = ((ImageButton)sender).ToolTip.Trim();
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM [dbo].[VW_sql_tblSMDPRCompany] WHERE COMPANY IN (SELECT COMPANY FROM [dbo].[VW_sql_tblSMDPRVessels] WHERE SHIPID='" + VesselCode + "' and Active='A')");
        string CompanyCode = dt.Rows[0]["Company"].ToString();
        string CompanyName = dt.Rows[0]["Company Name"].ToString();
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
        string QrtString = "Print.aspx?BudgetForeCast=true&Comp=" + CompanyPart + "&Vessel=" + VesselCode + "&BType=< All >&StartDate=" + StartDate + "&EndDate=" + EndDate + "&year=" + lblyear.Text + "&YearDays=" + Days + "&MajCatID=6";
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
            p2.Add(new Phrase(Company + "\n" + "\n", FontFactory.GetFont("ARIAL", 12, iTextSharp.text.Font.NORMAL)));
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
            iTextSharp.text.Font fCapText_5 = FontFactory.GetFont("ARIAL", 7, iTextSharp.text.Font.NORMAL);
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
    public string FormatCurrency(object InValue)
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
}
    
    
