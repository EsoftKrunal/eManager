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

public partial class CrewOperation_CrewPlanningHistory : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        ShowData();
    }
    public void ShowData()
    {
        int CrewId = Common.CastAsInt32(Request.QueryString["CrewId"]);
        int VesselId = Common.CastAsInt32(Request.QueryString["VesselId"]);
        DateTime SignOn = DateTime.Today;

        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("select crewnumber,SignOnDate,firstname + ' ' + middlename + ' ' + lastname as CrewName,(select vesselname from DBO.vessel where vesselid=" + VesselId.ToString() + ") as vesselname from dbo.CrewPersonalDetails where crewid=" + CrewId);
        if (dt.Rows.Count > 0)
        {
            lblCrewDetails.Text = dt.Rows[0]["crewnumber"].ToString() + " | " + dt.Rows[0]["CrewName"].ToString();
            lblVesselName.Text = dt.Rows[0]["vesselname"].ToString();
            SignOn=Convert.ToDateTime(dt.Rows[0]["SignOnDate"]);
        }

        

        dt = Common.Execute_Procedures_Select_ByQueryCMS("select * from vw_crewvesselplaninghistory WHERE ReliveeId = " + CrewId + " and PlannedOn>='" + SignOn.ToString("dd-MMM-yyyy") + "'");

        rptData.DataSource = dt;
        rptData.DataBind();
    }
}
