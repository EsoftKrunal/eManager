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

public partial class ManagePortCall : System.Web.UI.Page
{
    
    public int PCId
    {
        get { return Common.CastAsInt32(ViewState["PCId"]); }
        set { ViewState["PCId"] = value; }
    }
    public int VesselId
    {
        get { return Common.CastAsInt32(ViewState["VesselId"]); }
        set { ViewState["VesselId"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        lblMessage.Text = "";
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        if (!IsPostBack)
        {
            PCId = Common.CastAsInt32(Request.QueryString["PCId"]);
            DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("SELECT h.*,v.VesselName,c.CountryName,p.PortName FROM PORTCALLHEADER h inner join port p on h.portid=p.portid inner join country c on p.CountryId=c.CountryId inner join vessel v on h.VesselId=v.VesselId WHERE PORTCALLID=" + PCId.ToString());

            if (dt.Rows.Count > 0)
            {
                VesselId = Common.CastAsInt32(dt.Rows[0]["VesselId"]);
                lblRefNo.Text=dt.Rows[0]["PortReferenceNumber"].ToString();
                lblCountry.Text = dt.Rows[0]["CountryName"].ToString();
                lvlPort.Text = dt.Rows[0]["PortName"].ToString();
                lblVSL.Text = dt.Rows[0]["VesselName"].ToString();
                lblDuration.Text = Common.ToDateString(dt.Rows[0]["ETA"]) + " to " + Common.ToDateString(dt.Rows[0]["ETD"]);
                ShowData();
            }
        }
    }
    protected void btnAddCrew_Click(object sender, EventArgs e)
    {
        foreach(RepeaterItem ri in rptData.Items)
        {
            CheckBox ch =(CheckBox) ri.FindControl("chkSelect");
            if (ch.Checked)
            {
                int CrewId = Common.CastAsInt32(ch.CssClass);
                string mode = ch.Attributes["mode"].ToString();
                //---------------
                Common.Execute_Procedures_Select_ByQueryCMS("Insert_Port_Planner_Crew " + PCId.ToString() + "," + CrewId + ",'" + mode + "'");
                lblMessage.Text = "Crew added successfully to portcall.";
                //---------------
            }
        }
    }
    protected void ShowData()
    {
        rptData.DataSource= PortPlanner.getCrewDetails(VesselId.ToString());
        rptData.DataBind();
    }
}
