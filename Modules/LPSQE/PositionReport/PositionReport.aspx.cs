using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.Security;
using System.Data.Common;
using System.Transactions;
using System.Web.UI.WebControls;
using ShipSoft.CrewManager.BusinessObjects;
using ShipSoft.CrewManager.BusinessLogicLayer;
using ShipSoft.CrewManager.Operational;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.IO;
public partial class PositionReport_PositionReport : System.Web.UI.Page
{
    Authority Auth;
    string Mode;
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        //VesselPositionReporting mp = (VesselPositionReporting)this.Master;
        //mp.ShowMenu = false;
        if (Session["loginid"] == null)
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Default.aspx';", true);
        }

        //--------------------------------- PAGE ACCESS AUTHORITY ----------------------------------------------------
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 1089);
        if (chpageauth <= 0)
            Response.Redirect("blank.aspx");
        //------------------------------------------------------------------------------------------------------------
        //VesselPositionReporting mp = (VesselPositionReporting)this.Master;
        //mp.ShowMenu = false;
        //mp.ShowHeaderbar = false;
        if (Session["loginid"] != null)
        {
            ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 1089);
            OBJ.Invoke();
            Session["Authority"] = OBJ.Authority;
            Auth = OBJ.Authority;
        }
        if (!(IsPostBack))
        {
            for (int i = DateTime.Today.Year; i >= 2014; i--)
                ddlYear.Items.Add(new ListItem(i.ToString(), i.ToString()));

            BindFleet();
            BindVessel();
            BindGrid();
        }
    }

    //Event ---------------------------------------------
    protected void ddlFleet_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        BindVessel();
        BindGrid();
    }
   
    protected void btn_Show_Click(object sender, EventArgs e)
    {
        BindGrid();
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        
        ddlVessel.SelectedIndex = 0;
        txtDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
        btn_Show_Click(sender, e); 
    }
    protected void chk_Inactive_OnCheckedChanged(object sender, EventArgs e)
    {
        BindVessel();
        BindGrid();
    }
    protected void Update_Grid(object sender, EventArgs e)
    {
        BindGrid();
    }
   
    //Function ---------------------------------------------
    public void BindFleet()
    {
        string Query = "select * from FleetMaster";
        ddlFleet.DataSource = Budget.getTable(Query);
        ddlFleet.DataTextField = "FleetName";
        ddlFleet.DataValueField = "FleetID";
        ddlFleet.DataBind();
        ddlFleet.Items.Insert(0, new ListItem("<--All-->", "0"));

        ddlFleet1.DataSource = Budget.getTable(Query);
        ddlFleet1.DataTextField = "FleetName";
        ddlFleet1.DataValueField = "FleetID";
        ddlFleet1.DataBind();
        ddlFleet1.Items.Insert(0, new ListItem("<-- All -->", "0"));
    }
    public void BindVessel()
    {
        string WhereClause = "";
        string sql = "SELECT VesselId,VesselCode,Vesselname FROM Vessel v Where isnull(fleetid,0)>0  ";

        if (!chk_Inactive.Checked)
        {
            WhereClause = " and v.VesselStatusid<>2 ";
        }

        if (ddlFleet.SelectedIndex != 0)
        {
            WhereClause = WhereClause + " and fleetID=" + ddlFleet.SelectedValue + "";
        }

        sql = sql +WhereClause+ " and v.VesselId in (Select VesselId from UserVesselRelation with(nolock) where Loginid = " + Convert.ToInt32(Session["loginid"].ToString()) + ") ORDER BY VESSELNAME";
        ddlVessel.DataSource = VesselReporting.getTable(sql);

        ddlVessel.DataTextField = "Vesselname";
        ddlVessel.DataValueField = "VesselId";
        ddlVessel.DataBind();
        ddlVessel.Items.Insert(0, new ListItem("<--All-->", "0"));


        //--------------------------------------------------------------
        //WhereClause = "";
        //sql = "SELECT VesselId,VesselCode,Vesselname FROM Vessel v Where isnull(fleetid,0)>0 and v.VesselStatusid<>2 and v.vesselid not in (41,43,44) ";

        //if (ddlFleet1.SelectedIndex != 0)
        //{
        //    WhereClause = WhereClause + " and fleetID=" + ddlFleet1.SelectedValue + "";
        //}

        //sql = sql + WhereClause + "ORDER BY VESSELNAME";

        //ddlVessel1.DataSource = VesselReporting.getTable(sql);
        //ddlVessel1.DataTextField = "Vesselname";
        //ddlVessel1.DataValueField = "VesselId";
        //ddlVessel1.DataBind();
        //ddlVessel1.Items.Insert(0, new ListItem("<-- Select -->", "0"));
    }
    private void BindGrid()
    {
        string whereclause= " Where isnull(fleetid,0)>0 and  VESSELID in (Select VesselId from UserVesselRelation with(nolock) where Loginid = " + Convert.ToInt32(Session["loginid"].ToString()) + ") ";
        if (!chk_Inactive.Checked)
        {
            whereclause += " And VesselStatusid=1 ";
        }
        if (ddlVessel.SelectedIndex != 0)
        {
            whereclause += " And VesselId=" + ddlVessel.SelectedValue;
        }
        if (ddlFleet.SelectedIndex != 0)
        {
            whereclause += " And FleetId=" + ddlFleet.SelectedValue;
        }
        if (txtDate.Text.Trim()!="")
        {
            whereclause += " And ReportDate='" + txtDate.Text + "'";
        }
        
        string Query = "Select * from VW_VSL_VPRNoonReport_New_VesselSummary " + whereclause + " order by vesselname";
        grd_Data.DataSource = Budget.getTable(Query);
        grd_Data.DataBind();
    }
    public string GetNextActivity(object loc, object date, object Hrs, object Min, object ArrivalPort)
    {
        try
        {
            if (Common.CastAsInt32(loc) == 1)
            {
                DateTime ArrDate = Convert.ToDateTime(date);
                string FinalDate = "ETA to " + ArrivalPort + " " + ArrDate.ToString("dd MMM yyyy") + " &nbsp;" + ((Common.CastAsInt32(Hrs) > 0) ? Hrs.ToString() : "00").ToString() + ":" + ((Common.CastAsInt32(Min) > 0) ? Min.ToString() : "00").ToString() + " HRS LT ";
                return FinalDate;
            }
            else if (Common.CastAsInt32(loc) == 2)
            {
                return "CARGO OPERATIONS - " + ArrivalPort;
            }
            else if (Common.CastAsInt32(loc) == 3)
            {
                return "ANCHORING - " + ArrivalPort;
            }
            else 
            {
                return "";
            }

        }
        catch
        {
            return "";
        }
        
    }
    public string GetLongitude(object i)
    {
        try
        {
            int Lat = Convert.ToInt32(i);
            if (Lat == 1)
            {
                return "E";
            }
            else if (Lat == 2)
            {
                return "W";
            }
            else
            {
                return "";
            }
        }
        catch
        {
            return "";
        }
    }
    public string GetLattitude(object i)
    {
        try
        {
            int Lon = Convert.ToInt32(i);
            if (Lon == 1)
            {
                return "N";
            }
            else if (Lon == 2)
            {
                return "S";
            }
            else
            {
                return "";
            }
        }
        catch
        {
            return "";
        }
    }
    protected void btnDEEOI_Click(object sender,EventArgs e)
    {
        dv_Down.Visible = true;
    }
    protected void btnDEEOI_Close_Click(object sender, EventArgs e)
    {
        dv_Down.Visible = false;
    }
    protected void btnDEEOIDown_Click(object sender, EventArgs e)
    {
        dv_Down.Visible = false;
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("EXEC GET_EEOI_FLEET_VSL " + ddlFleet1.SelectedValue + "," + ddlVessel1.SelectedValue +"," + ddlYear.SelectedValue);
        ProjectCommon.ExportDatatable(Response, dt, "EEOI_Report");

    }


    //--------------------------

    protected void ddlFleet1_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        BindVessel1();
    }

    public void BindVessel1()
    {
        string WhereClause = "";
        string sql = "SELECT VesselId,VesselCode,Vesselname FROM Vessel v Where isnull(fleetid,0)>0 and v.VesselStatusid<>2 and v.VesselId in (Select VesselId from UserVesselRelation with(nolock) where Loginid = " + Convert.ToInt32(Session["loginid"].ToString()) + ") ";

        if (ddlFleet1.SelectedIndex != 0)
        {
            WhereClause = WhereClause + " and fleetID=" + ddlFleet1.SelectedValue + "";
        }

        sql = sql + WhereClause + "ORDER BY VESSELNAME";

        ddlVessel1.DataSource = VesselReporting.getTable(sql);
        ddlVessel1.DataTextField = "Vesselname";
        ddlVessel1.DataValueField = "VesselId";
        ddlVessel1.DataBind();
        ddlVessel1.Items.Insert(0, new ListItem("<-- Select -->", "0"));
    }
}
