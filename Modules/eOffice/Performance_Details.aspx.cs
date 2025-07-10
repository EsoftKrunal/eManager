using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Text;

public partial class Emtm_Performance_Details : System.Web.UI.Page
{
    public int KPIId
    {
        get
        {
            return Common.CastAsInt32(ViewState["KPIId"]);
        }
        set
        {
            ViewState["KPIId"] = value;
        }
    }
    public int KPIYear
    {
        get
        {
            return Common.CastAsInt32(ViewState["KPIYear"]);
        }
        set
        {
            ViewState["KPIYear"] = value;
        }
    }
    public int EMPID
    {
        get
        {
            return Common.CastAsInt32(ViewState["EMPID"]);
        }
        set
        {
            ViewState["EMPID"] = value;
        }
    }
    DataTable dtVessels;
    DataTable dtAssignmentPeriods;
    DataTable dtData;

    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        EMPID = Common.CastAsInt32(Request.QueryString["EmpId"]);
        KPIId = Common.CastAsInt32(Request.QueryString["KPIId"]);
        KPIYear = Common.CastAsInt32(Request.QueryString["KPIYear"]);
        string YearStart = new DateTime(KPIYear, 1, 1).ToString("dd-MMM-yyyy");
        string YearEnd = new DateTime(KPIYear, 12, 31).ToString("dd-MMM-yyyy");

        //======================================================

        string SQL = "SELECT *,(SELECT FIRSTNAME + ' ' + MIDDLENAME + ' ' + FAMILYNAME FROM DBO.Hr_PersonalDetails WHERE USERID=" + EMPID + ") AS EMPNAME FROM DBO.KPI_ENTRY K INNER JOIN DBO.KPI_Period PP ON K.PeriodId=PP.PeriodId WHERE ENTRYID=" + KPIId;
        DataTable dt1 = Common.Execute_Procedures_Select_ByQueryCMS(SQL);
        if (dt1.Rows.Count > 0)
        {
            lblpagename.Text = dt1.Rows[0]["KPINAME"].ToString() + " - [ " + dt1.Rows[0]["periodname"].ToString() + " ]  <br> " + dt1.Rows[0]["EMPNAME"].ToString();
        }
        string SQL1 = "SELECT PERIODID FROM DBO.KPI_ENTRY WHERE ENTRYID=" + KPIId;
        DataTable dt2 = Common.Execute_Procedures_Select_ByQueryCMS(SQL1);
        int PeriodId = Common.CastAsInt32(dt2.Rows[0][0]);

        if (PeriodId == 10) // RANDOM
        {
            pnlFixedHeader.Visible = false;
            pnlRandomHeader.Visible = true;
            pnlFixed.Visible = false;
            pnlRandom.Visible = true;

            Common.Set_Procedures("DBO.GET_KPI_PEROFRMANCE_BYCREW");
            Common.Set_ParameterLength(3);
            Common.Set_Parameters(new MyParameter("@USERID", EMPID), new MyParameter("@YR", DateTime.Today.Year), new MyParameter("@KPIID_REQ", KPIId));
            DataSet ds = Common.Execute_Procedures_Select();
            rptRandom.DataSource = ds.Tables[1];
            rptRandom.DataBind();

        }
        else // FIXED KPI
        {
            pnlFixedHeader.Visible = true;
            pnlRandomHeader.Visible = false;

            pnlFixed.Visible = true;
            pnlRandom.Visible = false;

            switch (PeriodId)
            {
                case 1:
                    lblDataType.Text = "Months";
                    break;
                case 2:
                    lblDataType.Text = "Quarters";
                    break;
                case 3:
                    lblDataType.Text = "Half Years";
                    break;
                case 4:
                    lblDataType.Text = "Years";
                    break;
                case 5:
                    lblDataType.Text = "2-Years";
                    break;
                case 6:
                    lblDataType.Text = "Weeks";
                    break;
                case 7:
                    lblDataType.Text = "Period";
                    break;
                case 8:
                    lblDataType.Text = "Bi Months";
                    break;
                default:
                    break;
            }

            dtVessels = Common.Execute_Procedures_Select_ByQueryCMS("SELECT ROW_NUMBER() OVER(ORDER BY VESSELNAME) AS SRNO,* FROM DBO.VESSEL WHERE VESSELID IN ( " +
                "SELECT DISTINCT VESSELID FROM VesselAssignments  " +
                "WHERE (FleetManager=" + EMPID + " OR TechSuptd=" + EMPID + " OR MarineSuptd=" + EMPID + " OR TechAssst=" + EMPID + " OR MarineAsst=" + EMPID + " OR SPA=" + EMPID + " OR AccountOfficer=" + EMPID + ")  " +
                "AND " +
                "( " +
                "    (EFFDATE BETWEEN '" + YearStart + "' AND '" + YearEnd + "') OR " +
                "    (LASTDATE BETWEEN '" + YearStart + "' AND '" + YearEnd + "') OR " +
                "    (EFFDATE< '" + YearStart + "' AND LASTDATE > '" + YearEnd + "') OR " +
                "    (EFFDATE< '" + YearStart + "' AND LASTDATE IS NULL) " +
                "))  ORDER BY VESSELNAME");

            dtAssignmentPeriods = Common.Execute_Procedures_Select_ByQueryCMS("SELECT *,CASE WHEN LASTDATE IS NULL THEN CONVERT(SMALLDATETIME,'01-jan-2050') ELSE LASTDATE END AS NEWLASTDATE FROM VesselAssignments  " +
               "WHERE (FleetManager=" + EMPID + " OR TechSuptd=" + EMPID + " OR MarineSuptd=" + EMPID + " OR TechAssst=" + EMPID + " OR MarineAsst=" + EMPID + " OR SPA=" + EMPID + " OR AccountOfficer=" + EMPID + ")  " +
               "AND " +
               "( " +
               "    (EFFDATE BETWEEN '" + YearStart + "' AND '" + YearEnd + "') OR " +
               "    (LASTDATE BETWEEN '" + YearStart + "' AND '" + YearEnd + "') OR " +
               "    (EFFDATE< '" + YearStart + "' AND LASTDATE > '" + YearEnd + "') OR " +
               "    (EFFDATE< '" + YearStart + "' AND LASTDATE IS NULL) " +
               ")");

            dtData = Common.Execute_Procedures_Select_ByQueryCMS("SELECT * FROM DBO.KPI_PERFORMANCE WHERE KPIID='" + KPIId + "' AND YEAR=" + KPIYear + " ORDER BY SRNO");

            if (PeriodId == 1)
                rptData.DataSource = Common.Execute_Procedures_Select_ByQueryCMS("SELECT DISTINCT SRNO,PERIODFROM,PERIODTO,LEFT(DATENAME(MONTH,PERIODFROM),3) +'-' + CONVERT(VARCHAR,YEAR) AS COLNAME FROM DBO.KPI_PERFORMANCE WHERE KPIID='" + KPIId + "' AND YEAR=" + KPIYear + " ORDER BY SRNO"); ;
            if (PeriodId == 6)
                rptData.DataSource = Common.Execute_Procedures_Select_ByQueryCMS("SELECT DISTINCT SRNO,PERIODFROM,PERIODTO,'WK-' + CONVERT(VARCHAR,SRNO) AS COLNAME FROM DBO.KPI_PERFORMANCE WHERE KPIID='" + KPIId + "' AND YEAR=" + KPIYear + " ORDER BY SRNO"); ;
            if (PeriodId == 9)
                rptData.DataSource = Common.Execute_Procedures_Select_ByQueryCMS("SELECT DISTINCT SRNO,PERIODFROM,PERIODTO,'PERIOD-' + CONVERT(VARCHAR,SRNO) AS COLNAME FROM DBO.KPI_PERFORMANCE WHERE KPIID='" + KPIId + "' AND YEAR=" + KPIYear + " ORDER BY SRNO"); ;
            if (PeriodId == 3)
                rptData.DataSource = Common.Execute_Procedures_Select_ByQueryCMS("SELECT DISTINCT SRNO,PERIODFROM,PERIODTO,'PERIOD-' + CONVERT(VARCHAR,SRNO) AS COLNAME FROM DBO.KPI_PERFORMANCE WHERE KPIID='" + KPIId + "' AND YEAR=" + KPIYear + " ORDER BY SRNO"); ;
            if (PeriodId == 7)
                rptData.DataSource = Common.Execute_Procedures_Select_ByQueryCMS("SELECT DISTINCT SRNO,PERIODFROM,PERIODTO,'PERIOD-' + CONVERT(VARCHAR,SRNO) AS COLNAME FROM DBO.KPI_PERFORMANCE WHERE KPIID='" + KPIId + "' AND YEAR=" + KPIYear + " ORDER BY SRNO"); ;
            if (PeriodId == 8)
                rptData.DataSource = Common.Execute_Procedures_Select_ByQueryCMS("SELECT DISTINCT SRNO,PERIODFROM,PERIODTO,'PERIOD-' + CONVERT(VARCHAR,SRNO) AS COLNAME FROM DBO.KPI_PERFORMANCE WHERE KPIID='" + KPIId + "' AND YEAR=" + KPIYear + " ORDER BY SRNO"); ;
            if (PeriodId == 4)
                rptData.DataSource = Common.Execute_Procedures_Select_ByQueryCMS("SELECT DISTINCT SRNO,PERIODFROM,PERIODTO,'PERIOD-' + CONVERT(VARCHAR,SRNO) AS COLNAME FROM DBO.KPI_PERFORMANCE WHERE KPIID='" + KPIId + "' AND YEAR=" + KPIYear + " ORDER BY SRNO"); ;


            rptData.DataBind();

            StringBuilder sbHeader = new StringBuilder();
            int C = 0;
            foreach (DataRow dr in dtVessels.Rows)
            {
                sbHeader.Append("<td class='center col" + (C % 4).ToString() + "' style='width:50px'>" + dr["VESSELcode"] + "</td>"); C++;
            }
            litHeader.Text = sbHeader.ToString();

        }
    }
    public string GetVesselDetails(object srno, object FromDate, object ToDate)
    {
        StringBuilder sb = new StringBuilder();
        foreach (DataRow dr in dtVessels.Rows)
        {
            int VesselId = Common.CastAsInt32(dr["VesselId"].ToString());

            DataRow[] drsAssign = dtAssignmentPeriods.Select("VESSELID=" + VesselId.ToString() + " AND ( '" + FromDate + "'>=EFFDATE AND '" + FromDate + "'<=NEWLASTDATE )");

            if (drsAssign.Length <= 0)
                drsAssign = dtAssignmentPeriods.Select("VESSELID=" + VesselId.ToString() + " AND ( '" + ToDate + "'>=EFFDATE AND '" + ToDate + "'<=NEWLASTDATE )");

            string classname = "grey2";
            string title = "";
            if (drsAssign.Length > 0)
            {
                DataRow[] drs = dtData.Select("VESSELID=" + VesselId.ToString() + " AND PERIODFROM='" + FromDate + "'");
                if (drs.Length > 0)
                {
                    string status = (Convert.IsDBNull(drs[0]["Status"])) ? "NA" : drs[0]["Status"].ToString();
                    if (status == "NA")
                    {
                        classname = "grey2";
                    }
                    else if (status == "1")
                    {
                        classname = "success";
                        title = drs[0]["REFKEY"].ToString();
                    }
                    else if (status == "0")
                    {
                        classname = "error";
                        title = drs[0]["REFKEY"].ToString();

                        if (Convert.ToDateTime(ToDate) > DateTime.Today)
                            classname = "error_can_corrected";
                    }
                }
            }

            sb.Append("<td class='center " + classname + "' style='width:50px'>" + title + "</td>");
        }
        return sb.ToString();
    }


}