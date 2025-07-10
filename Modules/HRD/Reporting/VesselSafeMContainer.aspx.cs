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
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing;
using ShipSoft.CrewManager.Operational;

public partial class Reporting_VesselSafeMContainer : System.Web.UI.Page
{
    int crewid = 0;
    int selindex = 0;
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        //========== Code to check report printing authority
        CrystalReportViewer1.HasPrintButton = Alerts.Check_ReportPrintingAuthority(Convert.ToInt32(Session["loginid"].ToString()), 113);
        //==========

        int Year = Common.CastAsInt32(Request.QueryString["year"]);
        int VesselID = Common.CastAsInt32(Request.QueryString["vid"]);
        if (Request.QueryString["vid"]!= "-1")
        {
            //DataTable dt1=Budget.getTable("select (select mgm.gradename from manninggrademaster mgm where mgm.gradeid=vsm.manninggradeid) as Grade,safemanning " +
            //                              "from vesselsafemanning vsm where vsm.vesselid=" + Request.QueryString["vid"]).Tables[0];

            //DataTable dt2 = Budget.getTable("SELECT (SELECT RANKCODE FROM RANK WHERE RANK.RANKID=VBM.RANKID) AS RANKNAME, " +
            //                                "BUDGETMANNING,(SELECT COUNTRYNAME FROM COUNTRY WHERE COUNTRYID=BUDGETNATIONALITY) as BUDGETNATIONALITY " +
            //                                "FROM VESSELBUDGETMANNING VBM WHERE VBM.VESSELID=" + Request.QueryString["vid"] + " Order By (SELECT RankLevel from rank where rank.rankid=VBM.RANKID)").Tables[0];

            //DataTable dt3 = Budget.getTable("SELECT * FROM (SELECT RankLevel,RANKCODE AS Grade, " +
            //                                  "(SELECT COUNT(*) FROM CREWPERSONALDETAILS CPD WHERE CPD.crewstatusid=3 AND CPD.currentvesselid=" + Request.QueryString["vid"] + " AND RANK.RANKID=CPD.CURRENTRANKID) as SafeManning " +
            //                                  "FROM RANK) AA WHERE SAFEMANNING > 0  Order By RankLevel").Tables[0];


            string sql = " with Tbl " +
             "  as " +
             " ( " +
             "     Select RankCode, RankLevel " +
             "     ,(select safemanning from vesselsafemanning vsm where VesselId = " + VesselID+ " and ManningGradeId = R.RankId )SafeManning " +
             "     ,(select BUDGETMANNING from VESSELBUDGETMANNING Where VesselID = " + VesselID + " and RankID = R.RankId and BYear = " + Year + ")BudgetManning " +
             "     ,(select C.COUNTRYNAME from VESSELBUDGETMANNING V inner join COUNTRY C on V.BudgetNationality = C.COUNTRYID Where VesselID = " + VesselID + " and RankID = R.RankId and BYear = " + Year + ")BudgetCounty " +
             "      ,(select Wages from VESSELBUDGETMANNING Where VesselID = " + VesselID + " and RankID = R.RankId and BYear = " + Year + ")BudgetWages " +
             "   ,(SELECT COUNT(*) FROM CREWPERSONALDETAILS CPD WHERE CPD.crewstatusid = 3 AND CPD.currentvesselid = " + VesselID + " AND CPD.CURRENTRANKID = R.RANKID) as ActualManning  " +
             "  ,dbo.GET_Country_Manning(" + VesselID+ ", R.RANKID) as ActualManningCountries " +
             "  ,((select isnull(SUM(case when COMPONENTTYPE = 'E' THEN amount ELSE 0 END), 0) - isnull(SUM(case when COMPONENTTYPE = 'D' THEN amount ELSE 0 END), 0) from crewcontractdetails ccd WHERE ccd.contractid in (select contractid from crewpersonaldetails where currentvesselid = " + VesselID+ " and currentrankid = R.RANKID and crewstatusid = 3))  +(select isnull(sum(otheramount), 0) from crewcontractheader cch WHERE cch.contractid in (select contractid from crewpersonaldetails where currentvesselid = " + VesselID+ " and currentrankid = R.RANKID and crewstatusid = 3))) as ACTUALWAGES " +
             "     from Rank R " +
             " ) " +
             " select* from Tbl " +
             " where SafeManning > 0 or BudgetManning> 0 or ActualManning> 0 " +
             " order by RankLevel  ";


            DataTable data = Budget.getTable(sql).Tables[0];
            this.CrystalReportViewer1.Visible = true;
            CrystalReportViewer1.ReportSource = rpt;
            rpt.Load(Server.MapPath("VesselSafeManningReport_New.rpt"));
            rpt.SetDataSource(data);
            //rpt.Subreports[0].SetDataSource(dt1);
            //rpt.Subreports[1].SetDataSource(dt2);
            //rpt.Subreports[2].SetDataSource(dt3);
            rpt.SetParameterValue("@Vessel", Request.QueryString["vname"]);
            //rpt.SetParameterValue("@Count1", getSum("safemanning", dt1));
            //rpt.SetParameterValue("@Count2", getSum("BUDGETMANNING", dt2));
            //rpt.SetParameterValue("@Count3", getSum("safemanning", dt3));
            rpt.SetParameterValue("@Count1", getSum("SafeManning", data));
            rpt.SetParameterValue("@Count2", getSum("BudgetManning", data));
            rpt.SetParameterValue("@Count3", getSum("ActualManning", data));
        }
    }

    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
    public int getSum(string Fieldname, DataTable dt)
    {
        int res = 0;
        foreach (DataRow dr in dt.Rows)
        {
            res = res + Common.CastAsInt32(dr[Fieldname]); 
        }
        return res;
    }
}
