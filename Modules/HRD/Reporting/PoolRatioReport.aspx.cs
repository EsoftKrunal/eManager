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

public partial class PoolRatioReport : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        string FromDate = "", ToDate = "", RecruitingOfficeId = "", OffCrewID = "", Location="";

        if (Page.Request.QueryString["FromDate"] != null)
            FromDate = Page.Request.QueryString["FromDate"].ToString();
        else
            return;
        if (Page.Request.QueryString["ToDate"] != null)
            ToDate= Page.Request.QueryString["ToDate"].ToString();
        else
            return;
        if (Page.Request.QueryString["RECID"] != null)
            RecruitingOfficeId = Page.Request.QueryString["RECID"].ToString();
        else
            return;
        if (Page.Request.QueryString["OffCrewID"] != null)
            OffCrewID = Page.Request.QueryString["OffCrewID"].ToString();
        else
            return;
        if (Page.Request.QueryString["Location"] != null)
            Location = Page.Request.QueryString["Location"].ToString();
        else
            return;
        //========== Code to check report printing authority
        CrystalReportViewer1.HasPrintButton = Alerts.Check_ReportPrintingAuthority(Convert.ToInt32(Session["loginid"].ToString()),15);
        //----------
        DataSet Ds;
        DataTable dt1;
        string sql = "",WhereClause="" ,SubHeader="";
        SubHeader = "Period : ( "+FromDate+" : "+ToDate+" )";

        string CountryClause = "";
        switch (RecruitingOfficeId)
        {
            case "0":
                CountryClause = "";
                break;
            case "1":
                CountryClause = "and budgetnationality in (70)";
                break;
            case "2":
                CountryClause = "and budgetnationality not in (70,101,116,168)";
                break;
            case "3":
                CountryClause = "and budgetnationality in (101)";
                break;
            case "4":
                CountryClause = "and budgetnationality in (116,168)";
                break;  
            default:
                CountryClause = "";
                break; 
        }

        if (RecruitingOfficeId != "0")
        {
            sql = "select R.RankCode " +
                 " ,(select Count(CrewID) from dbo.get_Crew_History C where C.SignOnDate>='" + FromDate + "' and C.SignOnDate<='" + ToDate + "' and C.CrewID in (select CrewID From CrewPersonalDetails CP where RecruitmentOfficeID=" + RecruitingOfficeId + " and CP.CurrentRankID =R.RankID ) and C.NewRankID=R.RankID )Total " +
                 " ,(select Count(CrewID) from dbo.get_Crew_History C where C.SignOnDate>='" + FromDate + "' and C.SignOnDate<='" + ToDate + "' and C.CrewID in (select crewid from crewcontractheader where Status='A' group by crewid having count(contractid)>=2) and C.CrewID in (select CrewID From CrewPersonalDetails CP where RecruitmentOfficeID=" + RecruitingOfficeId + " and CP.CurrentRankID =R.RankID ) and C.NewRankID=R.RankID )EX  " +
                 " ,(select Count(CrewID) from dbo.get_Crew_History C where C.SignOnDate>='" + FromDate + "' and C.SignOnDate<='" + ToDate + "' and C.CrewID in (select crewid from crewcontractheader where Status='A' group by crewid having count(contractid)<=1) and C.CrewID in (select CrewID From CrewPersonalDetails CP where RecruitmentOfficeID=" + RecruitingOfficeId + " and CP.CurrentRankID =R.RankID ) and C.NewRankID=R.RankID )New  " +

                 " ,(select Count(CrewID) from dbo.get_Crew_History C where C.SingOffDate >='" + FromDate + "' and C.SingOffDate <='" + ToDate + "' and C.CrewID in (select CrewID From CrewPersonalDetails CP where RecruitmentOfficeID=" + RecruitingOfficeId + " and CP.CurrentRankID =R.RankID )  and C.NewRankID=R.RankID )TotalSignOff   " +
                 " ,(select Count(CrewID) from dbo.get_Crew_History C where C.SingOffDate >='" + FromDate + "' and C.SingOffDate <='" + ToDate + "' and C.SignOffReasonID not in (6,13,8,12,19) and C.CrewID in (select CrewID From CrewPersonalDetails CP where RecruitmentOfficeID=" + RecruitingOfficeId + " and CP.CurrentRankID =R.RankID )   and C.NewRankID=R.RankID )SignOffOther " +
                 " ,(select Count(CrewID) from dbo.get_Crew_History C where C.SingOffDate >='" + FromDate + "' and C.SingOffDate <='" + ToDate + "' and C.SignOffReasonID in (6,13) and C.CrewID in (select CrewID From CrewPersonalDetails CP where RecruitmentOfficeID=" + RecruitingOfficeId + " and CP.CurrentRankID =R.RankID ) and C.NewRankID=R.RankID )Medical_Pni   " +

                 " ,(select Count(CrewID) from dbo.get_Crew_History C where C.SingOffDate >='" + FromDate + "' and C.SingOffDate <='" + ToDate + "' and C.SignOffReasonID in (19) and C.CrewID in (select CrewID From CrewPersonalDetails CP where RecruitmentOfficeID=" + RecruitingOfficeId + " and CP.CurrentRankID =R.RankID ) and C.NewRankID=R.RankID )Fire   " +

                 " ,(select Count(CrewID) from dbo.get_Crew_History C where C.SingOffDate >='" + FromDate + "' and C.SingOffDate <='" + ToDate + "' and C.SignOffReasonID in (8) and C.CrewID in (select CrewID From CrewPersonalDetails CP where RecruitmentOfficeID=" + RecruitingOfficeId + " and CP.CurrentRankID =R.RankID ) and C.NewRankID=R.RankID )EOC   " +
                 " ,(select Count(CrewID) from dbo.get_Crew_History C where C.SingOffDate >='" + FromDate + "' and C.SingOffDate <='" + ToDate + "' and C.SignOffReasonID in (12) and C.CrewID in (select CrewID From CrewPersonalDetails CP where RecruitmentOfficeID=" + RecruitingOfficeId + " and CP.CurrentRankID =R.RankID ) and C.NewRankID=R.RankID )Transfer  " +

                 " ,(select Count(CrewID) from CrewPersonalDetails C where C.RecruitmentOfficeID=" + RecruitingOfficeId + " and C.CrewStatusID=3 and  C.CurrentRankID=R.RankID )TotalOnBoard  " +
                 " ,(select Count(CrewID) from CrewPersonalDetails C where C.RecruitmentOfficeID=" + RecruitingOfficeId + " and C.CrewStatusID=2 and  C.CurrentRankID=R.RankID )TotalOnLeave  " +
                 " ,(select sum(budgetmanning) from dbo.VesselBudgetManning where rankid=R.RankID " + CountryClause + ")BManning  " +
                 " from Rank R";
            SubHeader = SubHeader + "      Location : "+Location+"";
        }
        else
        {
            sql = "select R.RankCode " +
                 " ,(select Count(CrewID) from dbo.get_Crew_History C where C.SignOnDate>='" + FromDate + "' and C.SignOnDate<='" + ToDate + "'  and C.NewRankID=R.RankID )Total " +

                 " ,(select Count(CrewID) from dbo.get_Crew_History C where C.SignOnDate>='" + FromDate + "' and C.SignOnDate<='" + ToDate + "' and C.CrewID in (select crewid from crewcontractheader where Status='A' group by crewid having count(contractid)>=2) and C.NewRankID=R.RankID )EX  " +
                 " ,(select Count(CrewID) from dbo.get_Crew_History C where C.SignOnDate>='" + FromDate + "' and C.SignOnDate<='" + ToDate + "' and C.CrewID in (select crewid from crewcontractheader where Status='A' group by crewid having count(contractid)<=1) and C.NewRankID=R.RankID )New  " +

                 " ,(select Count(CrewID) from dbo.get_Crew_History C where C.SingOffDate >='" + FromDate + "' and C.SingOffDate <='" + ToDate + "'  and C.NewRankID=R.RankID )TotalSignOff   " +
                 " ,(select Count(CrewID) from dbo.get_Crew_History C where C.SingOffDate >='" + FromDate + "' and C.SingOffDate <='" + ToDate + "' and C.SignOffReasonID not in (6,13,8,12,19) and C.NewRankID=R.RankID )SignOffOther " +
                 " ,(select Count(CrewID) from dbo.get_Crew_History C where C.SingOffDate >='" + FromDate + "' and C.SingOffDate <='" + ToDate + "' and C.SignOffReasonID in (6,13) and C.NewRankID=R.RankID )Medical_Pni   " +

                 " ,(select Count(CrewID) from dbo.get_Crew_History C where C.SingOffDate >='" + FromDate + "' and C.SingOffDate <='" + ToDate + "' and C.SignOffReasonID in (19) and C.NewRankID=R.RankID )Fire   " +

                 " ,(select Count(CrewID) from dbo.get_Crew_History C where C.SingOffDate >='" + FromDate + "' and C.SingOffDate <='" + ToDate + "' and C.SignOffReasonID in (8) and C.NewRankID=R.RankID )EOC   " +
                 " ,(select Count(CrewID) from dbo.get_Crew_History C where C.SingOffDate >='" + FromDate + "' and C.SingOffDate <='" + ToDate + "' and C.SignOffReasonID in (12) and C.NewRankID=R.RankID )Transfer  " +

                 " ,(select Count(CrewID) from CrewPersonalDetails C where C.CrewStatusID=3 and  C.CurrentRankID=R.RankID )TotalOnBoard  " +
                 " ,(select Count(CrewID) from CrewPersonalDetails C where C.CrewStatusID=2 and  C.CurrentRankID=R.RankID )TotalOnLeave  " +
                 " ,(select sum(budgetmanning) from dbo.VesselBudgetManning where rankid=R.RankID " + CountryClause + ")BManning  " +
                 " from Rank R";
        }
        if (OffCrewID != "A")
        {
            WhereClause = " where R.OffCrew='"+OffCrewID+"'";
            SubHeader = SubHeader + "      Rank Group : "+((OffCrewID=="O")?"Officers":"Rating")+"";
        }
        sql = sql + WhereClause;
        Ds = Budget.getTable(sql);

        if (Ds != null)
        {
            if (Ds.Tables[0].Rows.Count > 0)
            {
                dt1 = Ds.Tables[0];
                this.CrystalReportViewer1.Visible = true;
                CrystalReportViewer1.ReportSource = rpt;
                rpt.Load(Server.MapPath("PoolRatioReport.rpt"));
                rpt.SetDataSource(dt1);
                rpt.SetParameterValue("SubHeader", SubHeader);

                //foreach (DataRow dr in dt2.Rows)
                //{
                //    rpt.SetParameterValue("@Company", dr["CompanyName"].ToString());
                //    rpt.SetParameterValue("@Email", "GOA Crew List");
                //    rpt.SetParameterValue("@FmDt", Session["Vessel"].ToString());
                //    rpt.SetParameterValue("@TDt", Session["AsOn"].ToString());
                //}
            }
            else
            {
                this.CrystalReportViewer1.Visible = false;
            }
        }
        
    }

    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
}
