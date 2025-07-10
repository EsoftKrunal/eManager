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

public partial class Reporting_ReliefPlanningUnderOperation_Header : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        this.lblMessage.Text = "";

        //========== Code to check report printing authority
        CrystalReportViewer1.HasPrintButton = Alerts.Check_ReportPrintingAuthority(Convert.ToInt32(Session["loginid"].ToString()), 15);
        showreport();
    }
    private void showreport()
    {
        try
        {
            int vesselid, days, RecruitingOfficeID;
            string RankG, pd, RankList, Fleet;
            vesselid = Convert.ToInt32(Page.Request.QueryString["VID"].ToString());
            days = Convert.ToInt32(string.IsNullOrEmpty(Convert.ToString(Page.Request.QueryString["Days"])) ? "0" : Convert.ToString(Page.Request.QueryString["Days"]));
            RankG = Page.Request.QueryString["RankG"].ToString();
            RankList = Page.Request.QueryString["RankList"].ToString();
            Fleet = Page.Request.QueryString["Fleet"].ToString();
            pd = Page.Request.QueryString["PD"].ToString();
            RecruitingOfficeID = Common.CastAsInt32(Page.Request.QueryString["RO_ID"]);

            string WhereClause = " where cpd.crewstatusid=3 And cpd.IsFamilyMember='N' ";

            if (vesselid != 0)
                WhereClause += " And cpd.Currentvesselid=" + vesselid.ToString();
            else
            {
                if (Fleet != "0")
                    WhereClause += " And cpd.Currentvesselid in (select vsl.vesselid from vessel vsl where vsl.fleetid=" + Fleet.ToString() + ")";
            }

            WhereClause += " And cpd.ReliefDueDate<=dateadd(day," + days.ToString() + ",convert(char(10),getdate(),101))";

            if (RankList.Trim() != "")
                WhereClause += " And cpd.CurrentRankId In (" + RankList + ")";
            else if (RankG.Trim() != "A")
                WhereClause += " And cpd.CurrentRankId In (select RankId from Rank Where OffCrew='" + RankG + "')";

            if (pd == "Y")
                WhereClause += " And cpd.crewstatusid=3 and isnull(cpd.FirstRelieverId,0)<=0 and isnull(cpd.SecondRelieverId,0)<=0";
            if (RecruitingOfficeID != 0)
                WhereClause += " And cpd.RecruitmentOfficeID=" + RecruitingOfficeID + "";

            string SQL = "Select CrewId, " +
                                            "CrewNumber, " +
                                            "FirstName+ ' ' +isnull(MiddleName,'')+ ' ' + Lastname as Name, " +
                                            "(Select VesselCode from vessel Where vessel.vesselid=cpd.Currentvesselid) as Vessel, " +
                                            //"(Select Rankgroupname from rankgroup where rankgroupid=(select rankgroupid from Rank Where Rank.RankID=cpd.currentrankid)) as Rank, " +
                                            "(select RankCode from rank R where R.RankID= cpd.currentrankid) as Rank, " +
                                            "(Select NationalityName from Country where CountryId =cpd.nationalityid) as Country, " +
                                            "replace(convert(varchar,SignOndate,106),' ','-') as SignOndate, " +
                                            "replace(convert(varchar,ReliefDuedate,106),' ','-') as ReliefDuedate, " +
                                            "(select cc.CrewNumber + ' : ' + isnull(cc.FirstName,'') + ' '+ isnull(cc.MiddleName,'')+ ' ' + isnull(cc.Lastname,'') from crewpersonaldetails cc where cc.crewid=cpd.FirstRelieverId) as FirstReliever, " +
                                            "(select replace(convert(varchar,cc.Availablefrom,106),' ','-') from crewpersonaldetails cc where cc.crewid=cpd.FirstRelieverId) as FirstRelieverAvailableDate, " +
                                            "(Select CountryName from country Where CountryId in(select NationalityId from crewpersonaldetails c1 where c1.crewid=cpd.FirstRelieverId)) as Country1, " +
                                            "(select cc.CrewNumber + ' : ' + isnull(cc.FirstName,'') + ' '+ isnull(cc.MiddleName,'')+ ' ' + isnull(cc.Lastname,'') from crewpersonaldetails cc where cc.crewid=cpd.SecondRelieverId) as SecondReliever, " +
                                            "(select replace(convert(varchar,cc.Availablefrom,106),' ','-') from crewpersonaldetails cc where cc.crewid=cpd.SecondRelieverId) as SecondRelieverAvailableDate, " +
                                            "(Select CountryName from country Where CountryId in(select NationalityId from crewpersonaldetails c2 where c2.crewid=cpd.SecondRelieverId)) as Country2, " +
                                            "(Select PortName From Port Where PortId In (Select Top 1 PortId From CrewVesselPlanningHistory cvph Where cvph.RelieverId=cpd.FirstRelieverId and cvph.ReliveeId=cpd.CrewId and PlanType='R' and Status='A' Order By PlanningId Desc)) as  Port, " +
                                            "(Select Top 1 Remark From CrewVesselPlanningHistory cvph Where cvph.RelieverId=cpd.FirstRelieverId and cvph.ReliveeId=cpd.CrewId and PlanType='R' and Status='A' Order By PlanningId Desc) as  Remark, " +
                                            "(Select PortName From Port Where PortId In (Select Top 1 PortId From CrewVesselPlanningHistory cvph Where cvph.RelieverId=cpd.SecondRelieverId and cvph.ReliveeId=cpd.CrewId and PlanType='R' and Status='A' Order By PlanningId Desc)) as  Port1, " +
                                            "(Select Top 1 Remark From CrewVesselPlanningHistory cvph Where cvph.RelieverId=cpd.SecondRelieverId and cvph.ReliveeId=cpd.CrewId and PlanType='R' and Status='A' Order By PlanningId Desc) as  Remark1 " +
                                            "from CrewPersonaldetails cpd " + WhereClause;
            DataTable dt = Budget.getTable(SQL).Tables[0];
            if (dt.Rows.Count > 0)
            {
                this.CrystalReportViewer1.Visible = true;

                CrystalReportViewer1.ReportSource = rpt;
                rpt.Load(Server.MapPath("ReliefPlanningUnderOperation.rpt"));

                rpt.SetDataSource(dt);

                DataTable dt1 = PrintCrewList.selectCompanyDetails();
                foreach (DataRow dr in dt1.Rows)
                {
                    rpt.SetParameterValue("@Company", dr["CompanyName"].ToString());
                }
                //rpt.SetParameterValue("tdays", days.ToString());
                rpt.SetParameterValue("@NoOfDays", days.ToString());
                rpt.SetParameterValue("@Rank", (RankG == "A") ? "All" : ((RankG == "O") ? "Officers" : "Rating"));
                rpt.SetParameterValue("@HeaderText", "");
            }
            else
            {
                this.lblMessage.Text = "No Record Found";
                this.CrystalReportViewer1.Visible = false;
            }
        }
        catch (Exception ex)
        {

        }
    }
}
