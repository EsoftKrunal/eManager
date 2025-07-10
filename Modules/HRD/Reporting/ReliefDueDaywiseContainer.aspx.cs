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

public partial class Reporting_ReliefDueDaywiseContainer : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        //========== Code to check report printing authority
        CrystalReportViewer1.HasPrintButton = Alerts.Check_ReportPrintingAuthority(Convert.ToInt32(Session["loginid"].ToString()), 15);
        string days = Request.QueryString["days"],typename= Request.QueryString["typename"], type= Request.QueryString["type"], vessel = Request.QueryString["vessel"], chk = Request.QueryString["chk"];
        string RankList = Page.Request.QueryString["RankList"].ToString(),Fleet = Page.Request.QueryString["Fleet"].ToString();

        string WhereClause = " where cpd.IsFamilyMember='N' and crewStatusid=3 ";

        if (vessel.Trim() != "0")
            WhereClause += " And cpd.Currentvesselid=" + vessel;
        else
        {
            if (Fleet != "0")
                WhereClause += " And cpd.Currentvesselid in (select vsl.vesselid from vessel vsl where vsl.fleetid=" + Fleet.ToString() + ")";
        }

        WhereClause += " And cpd.ReliefDueDate<=dateadd(day," + days.ToString() + ",convert(char(10),getdate(),101))";

        if (RankList.Trim()!="") 
            WhereClause += " And cpd.CurrentRankId In (" + RankList + ")";
        else if (type.Trim() != "A")
            WhereClause += " And cpd.CurrentRankId In (select RankId from Rank Where OffCrew='" + type + "')";

        if (chk == "Y")
            WhereClause += " And cpd.crewstatusid=3 and isnull(cpd.FirstRelieverId,0)<=0 and isnull(cpd.SecondRelieverId,0)<=0";

        string SQL = "select cpd.crewid,cpd.crewnumber,(firstname+' '+lastname) as Name,(select rankcode from rank where rankid=cpd.currentrankid) as Rank, " +
                    "(select nationalityname from country where countryid=cpd.nationalityid) as Nationality, " +
                    "(select vesselcode from vessel where vesselid=cpd.currentvesselid) as Vessel, " +
                    "replace(convert(varchar,SignOnDate,106),' ','-') as SignOnDate,replace(convert(varchar,ReliefDueDate,106),' ','-') as ReliefDueDate " +
                    "from crewpersonaldetails cpd" + WhereClause;
        DataTable dt1 = Budget.getTable(SQL).Tables[0];

        DataTable dt = PrintCrewList.selectCompanyDetails();
        if (dt1.Rows.Count > 0)
        {
            this.CrystalReportViewer1.Visible = true;
            CrystalReportViewer1.ReportSource = rpt;
            rpt.Load(Server.MapPath("ReliefDueForNextSpecifiedDays.rpt"));

            Session.Add("rptsource2", dt1);
           
            rpt.SetDataSource(dt1);
            foreach (DataRow dr in dt.Rows)
            {
                rpt.SetParameterValue("@Company", dr["CompanyName"].ToString());
            }
            rpt.SetParameterValue("@NoOfDays", days);
            rpt.SetParameterValue("@Rank", "For " + typename + " Off Crew");
        }
        else
        {
            this.CrystalReportViewer1.Visible = false;
        }
    }

    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
}
