using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.Caching;   

public partial class PrintVarianceReportCrystal5 : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();

    #region Properties ****************************************************
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        //---------------------------------------
        ProjectCommon.SessionCheck();
        //---------------------------------------
        if (!IsPostBack)
        {
           Session["PrintData"] = null;
           Session["TUtil"] = null;
        }
        ShowReport();
    }
    protected void btnPrint_OnClick(object sender, EventArgs e)
    {
        
    }
    public void ShowReport()
    {
        if (Common.CastAsInt32(Page.Request.QueryString["ReportLevel"]) == 1)
        {
            GeneralSummary();
        }
        else if (Common.CastAsInt32(Page.Request.QueryString["ReportLevel"]) == 2)
        {
            budgetSummary();
        }
        else if (Common.CastAsInt32(Page.Request.QueryString["ReportLevel"]) == 3)
        {
            AccountSummary();
        }
        else if (Common.CastAsInt32(Page.Request.QueryString["ReportLevel"]) == 4)
        {
            AccountDetails();
        }
    }
    public void GeneralSummary()
    {
        DataTable DtVRGeneralSummary=new DataTable();
        decimal TargetUtil=0;
        if (Session["PrintData"] == null)
        {
            Common.Set_Procedures("getVarianceRepport_vsl");
            Common.Set_ParameterLength(4);
            Common.Set_Parameters(
                                new MyParameter("@COMPCODE ", Page.Request.QueryString["CompCode"].ToString()),
                                new MyParameter("@MNTH", Page.Request.QueryString["Month"].ToString()),
                                new MyParameter("@YR", Page.Request.QueryString["Year"].ToString()),
                                new MyParameter("@VSLCODE", Page.Request.QueryString["VesselCode"].ToString())
                );
            DataSet DsValue = Common.Execute_Procedures_Select();
            if (DsValue != null)
            {
                if (DsValue.Tables[0].Rows.Count > 0)
                {
                    if (Convert.ToBoolean(Page.Request.QueryString["IndianFinYear"].ToString()))
                    {
                        int period = Convert.ToInt32(DsValue.Tables[0].Rows[0]["PERIOD"]);
                        int USFinMonth = Opex.GetUSFinMonth(period);
                        TargetUtil = Math.Round(((Common.CastAsDecimal(USFinMonth) / 12) * 100), 0);
                    }
                    else
                    {
                        TargetUtil = Math.Round(((Common.CastAsDecimal(DsValue.Tables[0].Rows[0]["PERIOD"]) / 12) * 100), 0);
                    }
                    
                    
                }
                DtVRGeneralSummary = DsValue.Tables[0];
                Session["PrintData"] = DtVRGeneralSummary;
                Session["TUtil"] = TargetUtil;
            }
        }
        else
        {
            TargetUtil = (decimal)Session["TUtil"];
            DtVRGeneralSummary = (DataTable)Session["PrintData"];
        }

        string totalBudgetDays = "0";
        string yearBudgetDays = "0";
        string monthBudgetDays = "0";

        string sql = "SELECT TOP 1 VDAYS.YEARDAYS FROM [dbo].tblSMDBudgetForecastYear as VDAYS WHERE VDAYS.CurFinYear = '" + Page.Request.QueryString["Year"].ToString() + "' AND VDAYS.CoCode = '" + Page.Request.QueryString["CompCode"].ToString() + "' AND VDAYS.ShipId = '" + Page.Request.QueryString["VesselCode"].ToString() + "' ORDER BY VDAYS.YEARDAYS DESC ";
        DataTable dtdays = Common.Execute_Procedures_Select_ByQuery(sql);
        if (dtdays.Rows.Count > 0)
        {
            totalBudgetDays = dtdays.Rows[0]["YEARDAYS"].ToString();
        }


        if (Page.Request.QueryString["Year"].ToString() != "")
        {
            DataTable dtBudgetdays = Opex.GetYearofDaysfromBudget(Convert.ToInt32(Page.Request.QueryString["Month"].ToString()), Page.Request.QueryString["Year"].ToString(), Page.Request.QueryString["CompCode"].ToString(), Page.Request.QueryString["VesselCode"].ToString());
            if (dtBudgetdays.Rows.Count > 0)
            {
                yearBudgetDays = dtBudgetdays.Rows[0]["Days"].ToString();
            }
        }

        int MonthDays = 0;
        if (Page.Request.QueryString["Year"].ToString() != "")
        {
            MonthDays = Opex.GetNodays(Page.Request.QueryString["Year"].ToString(), Convert.ToInt32(Page.Request.QueryString["Month"].ToString()));
        }
        monthBudgetDays = MonthDays.ToString();

        CrystalReportViewer1.ReportSource = rpt;
        int month = Convert.ToInt32(Page.Request.QueryString["Month"].ToString());
        int year = Convert.ToInt32(Page.Request.QueryString["Year"].Substring(0, 4));
        
        rpt.Load(Server.MapPath("~/Modules/OPEX/Report/VarianceReportGeneralSummary.rpt"));
        DtVRGeneralSummary.TableName = "getVarianceRepport;1";
        rpt.SetDataSource(DtVRGeneralSummary);
        if (month >= 1 && month <= 3)
        {
            year = year + 1;
        }
        rpt.SetParameterValue("Header", "VARIANCE REPORT FOR " + ProjectCommon.GetMonthName(Page.Request.QueryString["Month"].ToString()).ToUpper() + "-" + year.ToString());
        rpt.SetParameterValue("Company", Page.Request.QueryString["Company"].ToString());
        rpt.SetParameterValue("ReportLevel", "General Summary");
        rpt.SetParameterValue("Vessel", Page.Request.QueryString["Vessel"].ToString());
        rpt.SetParameterValue("TargetUtilization", "Target Util:" + TargetUtil + "%");
        rpt.SetParameterValue("FinanceYear", Page.Request.QueryString["Year"].ToString());
        rpt.SetParameterValue("MonthDays", MonthDays);
        rpt.SetParameterValue("YearDays", yearBudgetDays);
        rpt.SetParameterValue("BudgetDays", totalBudgetDays);
    }
    public void budgetSummary()
    {
        DataTable DtVRGeneralSummary=new DataTable();
        decimal TargetUtil = 0;
        if (Session["PrintData"] == null)
        {
            Common.Set_Procedures("getVarianceRepport_vsl");
        Common.Set_ParameterLength(4);
        Common.Set_Parameters(
                            new MyParameter("@COMPCODE ", Page.Request.QueryString["CompCode"].ToString()),
                            new MyParameter("@MNTH", Page.Request.QueryString["Month"].ToString()),
                            new MyParameter("@YR", Page.Request.QueryString["Year"].ToString()),
                            new MyParameter("@VSLCODE", Page.Request.QueryString["VesselCode"].ToString())
            );
        DataSet DsValue = Common.Execute_Procedures_Select();
        if (DsValue != null)
        {
            if (DsValue.Tables[0].Rows.Count > 0)
            {
                    if (Convert.ToBoolean(Page.Request.QueryString["IndianFinYear"].ToString()))
                    {
                        int period = Convert.ToInt32(DsValue.Tables[0].Rows[0]["PERIOD"]);
                        int USFinMonth = Opex.GetUSFinMonth(period);
                        TargetUtil = Math.Round(((Common.CastAsDecimal(USFinMonth) / 12) * 100), 0);
                    }
                    else
                    {
                        TargetUtil = Math.Round(((Common.CastAsDecimal(DsValue.Tables[0].Rows[0]["PERIOD"]) / 12) * 100), 0);
                    }
            }
            DtVRGeneralSummary = DsValue.Tables[1];
            Session["PrintData"] = DtVRGeneralSummary;
            Session["TUtil"] = TargetUtil;
        }
        }
        else
        {
            TargetUtil = (decimal)Session["TUtil"];
            DtVRGeneralSummary = (DataTable)Session["PrintData"];
        }

        string totalBudgetDays = "0";
        string yearBudgetDays = "0";
        string monthBudgetDays = "0";

        string sql = "SELECT TOP 1 VDAYS.YEARDAYS FROM [dbo].tblSMDBudgetForecastYear as VDAYS WHERE VDAYS.CurFinYear = '" + Page.Request.QueryString["Year"].ToString() + "' AND VDAYS.CoCode = '" + Page.Request.QueryString["CompCode"].ToString() + "' AND VDAYS.ShipId = '" + Page.Request.QueryString["VesselCode"].ToString() + "' ORDER BY VDAYS.YEARDAYS DESC ";
        DataTable dtdays = Common.Execute_Procedures_Select_ByQuery(sql);
        if (dtdays.Rows.Count > 0)
        {
            totalBudgetDays = dtdays.Rows[0]["YEARDAYS"].ToString();
        }
        if (Page.Request.QueryString["Year"].ToString() != "")
        {
            DataTable dtBudgetdays = Opex.GetYearofDaysfromBudget(Convert.ToInt32(Page.Request.QueryString["Month"].ToString()), Page.Request.QueryString["Year"].ToString(), Page.Request.QueryString["CompCode"].ToString(), Page.Request.QueryString["VesselCode"].ToString());
            if (dtBudgetdays.Rows.Count > 0)
            {
                yearBudgetDays = dtBudgetdays.Rows[0]["Days"].ToString();
            }
        }
        int MonthDays = 0;
        if (Page.Request.QueryString["Year"].ToString() != "")
        {
            MonthDays = Opex.GetNodays(Page.Request.QueryString["Year"].ToString(), Convert.ToInt32(Page.Request.QueryString["Month"].ToString()));
        }
        monthBudgetDays = MonthDays.ToString();


        CrystalReportViewer1.ReportSource = rpt;
        rpt.Load(Server.MapPath("~/Modules/OPEX/Report/VarianceReportBudgetSummary.rpt"));
        int month = Convert.ToInt32(Page.Request.QueryString["Month"].ToString());
        int year = Convert.ToInt32(Page.Request.QueryString["Year"].Substring(0, 4));
        DtVRGeneralSummary.TableName = "getVarianceRepport;1";
        rpt.SetDataSource(DtVRGeneralSummary);
        if (month >= 1 && month <= 3)
        {
            year = year + 1;
        }
        rpt.SetParameterValue("Header", "VARIANCE REPORT FOR " + ProjectCommon.GetMonthName(Page.Request.QueryString["Month"].ToString()).ToUpper() + "-" + year.ToString());
        rpt.SetParameterValue("Company", Page.Request.QueryString["Company"].ToString());
        rpt.SetParameterValue("ReportLevel", "Budget Summary");
        rpt.SetParameterValue("Vessel", Page.Request.QueryString["Vessel"].ToString());
        rpt.SetParameterValue("TargetUtilization", "Target Util:" + TargetUtil + "%");
        rpt.SetParameterValue("FinanceYear", Page.Request.QueryString["Year"].ToString());
        rpt.SetParameterValue("MonthDays", MonthDays);
        rpt.SetParameterValue("YearDays", yearBudgetDays);
        rpt.SetParameterValue("BudgetDays", totalBudgetDays);
    }
    public void AccountSummary()
    {
        DataTable DtVRGeneralSummary=new DataTable();
        decimal TargetUtil = 0;
        if (Session["PrintData"] == null)
        {
        Common.Set_Procedures("getVarianceRepport_vsl");
        Common.Set_ParameterLength(4);
        Common.Set_Parameters(
                            new MyParameter("@COMPCODE ", Page.Request.QueryString["CompCode"].ToString()),
                            new MyParameter("@MNTH", Page.Request.QueryString["Month"].ToString()),
                            new MyParameter("@YR", Page.Request.QueryString["Year"].ToString()),
                            new MyParameter("@VSLCODE", Page.Request.QueryString["VesselCode"].ToString())
            );
        DataSet DsValue = Common.Execute_Procedures_Select();
        if (DsValue != null)
        {
            if (DsValue.Tables[0].Rows.Count > 0)
            {
                    if (Convert.ToBoolean(Page.Request.QueryString["IndianFinYear"].ToString()))
                    {
                        int period = Convert.ToInt32(DsValue.Tables[0].Rows[0]["PERIOD"]);
                        int USFinMonth = Opex.GetUSFinMonth(period);
                        TargetUtil = Math.Round(((Common.CastAsDecimal(USFinMonth) / 12) * 100), 0);
                    }
                    else
                    {
                        TargetUtil = Math.Round(((Common.CastAsDecimal(DsValue.Tables[0].Rows[0]["PERIOD"]) / 12) * 100), 0);
                    }
            }
            DtVRGeneralSummary = DsValue.Tables[2];
            Session["PrintData"] = DtVRGeneralSummary;
            Session["TUtil"] = TargetUtil;
        }
        }
        else
        {
            TargetUtil = (decimal)Session["TUtil"];
            DtVRGeneralSummary = (DataTable)Session["PrintData"];
        }
        string totalBudgetDays = "0";
        string yearBudgetDays = "0";
        string monthBudgetDays = "0";

        string sql = "SELECT TOP 1 VDAYS.YEARDAYS FROM [dbo].tblSMDBudgetForecastYear as VDAYS WHERE VDAYS.CurFinYear = '" + Page.Request.QueryString["Year"].ToString() + "' AND VDAYS.CoCode = '" + Page.Request.QueryString["CompCode"].ToString() + "' AND VDAYS.ShipId = '" + Page.Request.QueryString["VesselCode"].ToString() + "' ORDER BY VDAYS.YEARDAYS DESC ";
        DataTable dtdays = Common.Execute_Procedures_Select_ByQuery(sql);
        if (dtdays.Rows.Count > 0)
        {
            totalBudgetDays = dtdays.Rows[0]["YEARDAYS"].ToString();
        }
        if (Page.Request.QueryString["Year"].ToString() != "")
        {
            DataTable dtBudgetdays = Opex.GetYearofDaysfromBudget(Convert.ToInt32(Page.Request.QueryString["Month"].ToString()), Page.Request.QueryString["Year"].ToString(), Page.Request.QueryString["CompCode"].ToString(), Page.Request.QueryString["VesselCode"].ToString());
            if (dtBudgetdays.Rows.Count > 0)
            {
                yearBudgetDays = dtBudgetdays.Rows[0]["Days"].ToString();
            }
        }
        int MonthDays = 0;
        if (Page.Request.QueryString["Year"].ToString() != "")
        {
            MonthDays = Opex.GetNodays(Page.Request.QueryString["Year"].ToString(), Convert.ToInt32(Page.Request.QueryString["Month"].ToString()));
        }
        monthBudgetDays = MonthDays.ToString();
        CrystalReportViewer1.ReportSource = rpt;
        int month = Convert.ToInt32(Page.Request.QueryString["Month"].ToString());
        int year = Convert.ToInt32(Page.Request.QueryString["Year"].Substring(0,4));
        rpt.Load(Server.MapPath("~/Modules/OPEX/Report/VarianceReportAccountSummary.rpt"));
        DtVRGeneralSummary.TableName = "getVarianceRepport;1";
        rpt.SetDataSource(DtVRGeneralSummary);
        if (month >= 1 && month <= 3)
        {
            year = year + 1;
        }

        rpt.SetParameterValue("Header", "VARIANCE REPORT FOR " + ProjectCommon.GetMonthName(Page.Request.QueryString["Month"].ToString()).ToUpper() + "-" + year.ToString());
        rpt.SetParameterValue("Company", Page.Request.QueryString["Company"].ToString());
        rpt.SetParameterValue("ReportLevel", "Account Summary");
        rpt.SetParameterValue("Vessel", Page.Request.QueryString["Vessel"].ToString());
        rpt.SetParameterValue("TargetUtilization", "Target Util:" + TargetUtil + "%");
        rpt.SetParameterValue("FinanceYear", Page.Request.QueryString["Year"].ToString());
        rpt.SetParameterValue("MonthDays", MonthDays);
        rpt.SetParameterValue("YearDays", yearBudgetDays);
        rpt.SetParameterValue("BudgetDays", totalBudgetDays);
    }
    public void AccountDetails()
    {
        DataTable DtVRGeneralSummary=new DataTable();
        decimal TargetUtil = 0;
        if (Session["PrintData"] == null)
        {
            Common.Set_Procedures("getVarianceRepport_vsl");
        Common.Set_ParameterLength(4);
        Common.Set_Parameters(
                            new MyParameter("@COMPCODE ", Page.Request.QueryString["CompCode"].ToString()),
                            new MyParameter("@MNTH", Page.Request.QueryString["Month"].ToString()),
                            new MyParameter("@YR", Page.Request.QueryString["Year"].ToString()),
                            new MyParameter("@VSLCODE", Page.Request.QueryString["VesselCode"].ToString())
            );
        DataSet DsValue = Common.Execute_Procedures_Select();
        if (DsValue != null)
        {
            if (DsValue.Tables[0].Rows.Count > 0)
            {
                    if (Convert.ToBoolean(Page.Request.QueryString["IndianFinYear"].ToString()))
                    {
                        int period = Convert.ToInt32(DsValue.Tables[0].Rows[0]["PERIOD"]);
                        int USFinMonth = Opex.GetUSFinMonth(period);
                        TargetUtil = Math.Round(((Common.CastAsDecimal(USFinMonth) / 12) * 100), 0);
                    }
                    else
                    {
                        TargetUtil = Math.Round(((Common.CastAsDecimal(DsValue.Tables[0].Rows[0]["PERIOD"]) / 12) * 100), 0);
                    }
            }
            DtVRGeneralSummary = DsValue.Tables[3];
            Session["PrintData"] = DtVRGeneralSummary;
            Session["TUtil"] = TargetUtil;
        }
        }
        else
        {
            TargetUtil = (decimal)Session["TUtil"];
            DtVRGeneralSummary = (DataTable)Session["PrintData"];
        }

        string totalBudgetDays = "0";
        string yearBudgetDays = "0";
        string monthBudgetDays = "0";

        string sql = "SELECT TOP 1 VDAYS.YEARDAYS FROM [dbo].tblSMDBudgetForecastYear as VDAYS WHERE VDAYS.CurFinYear = '" + Page.Request.QueryString["Year"].ToString() + "' AND VDAYS.CoCode = '" + Page.Request.QueryString["CompCode"].ToString() + "' AND VDAYS.ShipId = '" + Page.Request.QueryString["VesselCode"].ToString() + "' ORDER BY VDAYS.YEARDAYS DESC ";
        DataTable dtdays = Common.Execute_Procedures_Select_ByQuery(sql);
        if (dtdays.Rows.Count > 0)
        {
            totalBudgetDays = dtdays.Rows[0]["YEARDAYS"].ToString();
        }
        if (Page.Request.QueryString["Year"].ToString() != "")
        {
            DataTable dtBudgetdays = Opex.GetYearofDaysfromBudget(Convert.ToInt32(Page.Request.QueryString["Month"].ToString()), Page.Request.QueryString["Year"].ToString(), Page.Request.QueryString["CompCode"].ToString(), Page.Request.QueryString["VesselCode"].ToString());
            if (dtBudgetdays.Rows.Count > 0)
            {
                yearBudgetDays = dtBudgetdays.Rows[0]["Days"].ToString();
            }
        }
        int MonthDays = 0;
        if (Page.Request.QueryString["Year"].ToString() != "")
        {
            MonthDays = Opex.GetNodays(Page.Request.QueryString["Year"].ToString(), Convert.ToInt32(Page.Request.QueryString["Month"].ToString()));
        }
        monthBudgetDays = MonthDays.ToString(); 

        CrystalReportViewer1.ReportSource = rpt;
        int month = Convert.ToInt32(Page.Request.QueryString["Month"].ToString());
        int year = Convert.ToInt32(Page.Request.QueryString["Year"].Substring(0, 4));
        rpt.Load(Server.MapPath("~/Modules/OPEX/Report/VarianceReportAccountDetails.rpt"));
        DtVRGeneralSummary.TableName = "getVarianceRepport;1";
        rpt.SetDataSource(DtVRGeneralSummary);
        if (month >= 1 && month <= 3)
        {
            year = year + 1;
        }
        rpt.SetParameterValue("Header", "VARIANCE REPORT FOR " + ProjectCommon.GetMonthName(Page.Request.QueryString["Month"].ToString()).ToUpper() + "-" + year.ToString());
        rpt.SetParameterValue("Company", Page.Request.QueryString["Company"].ToString());
        rpt.SetParameterValue("ReportLevel", "Account Summary");
        rpt.SetParameterValue("Vessel", Page.Request.QueryString["Vessel"].ToString());
        rpt.SetParameterValue("TargetUtilization", "Target Util:" + TargetUtil + "%");
        rpt.SetParameterValue("FinanceYear", Page.Request.QueryString["Year"].ToString());
        rpt.SetParameterValue("MonthDays", MonthDays);
        rpt.SetParameterValue("YearDays", yearBudgetDays);
        rpt.SetParameterValue("BudgetDays", totalBudgetDays);
    }
    protected void Page_UnLoad(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }

}
