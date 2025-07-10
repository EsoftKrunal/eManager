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


public partial class DD_OFC_Tracker : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        // -------------------------- SESSION CHECK ----------------------------------------------
        ProjectCommon.SessionCheck();
        // -------------------------- SESSION CHECK END ----------------------------------------------
        if (!IsPostBack)
        {
            Session["DDPageId"] = "DD_OFC_Tracker";
            BindVessels();
            ddlDDType.DataSource = Common.Execute_Procedures_Select_ByQuery("select * from DBO.DD_DDTYPE ORDER BY DDTYPENAME");
            ddlDDType.DataTextField = "DDTYPENAME";
            ddlDDType.DataValueField = "DDTYPEID";
            ddlDDType.DataBind();

            ddlFleet.DataSource = Common.Execute_Procedures_Select_ByQuery("select * from DBO.FLEETMASTER ORDER BY FLEETNAME");
            ddlFleet.DataTextField = "FLEETNAME";
            ddlFleet.DataValueField = "FLEETID";
            ddlFleet.DataBind();
            ddlFleet.Items.Insert(0, new ListItem(" < All > ", ""));
            

            ddlYear.Items.Add(new ListItem(" All ", ""));
            for (int i = DateTime.Today.Year+1; i >= 2015; i--)
            {
                ddlYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
            //ddlYear.SelectedValue = DateTime.Today.Year.ToString();
            BindData();
            
        }
    }
    //--------------------------
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        dv_AddVessel.Visible = true;
    }
    //--------------------------
    protected void btnSaveVessel_Click(object sender, EventArgs e)
    {
        //--------------------
        if (ddlVessel.SelectedIndex > 0)
        {
            Common.Execute_Procedures_Select_ByQuery("EXEC DBO.DD_ADD_VSL_TRACKER " + ddlVessel.SelectedValue + ",'" + txtStdt.Text + "','" + txtEndDt.Text + "','" + txtNextDueDt.Text + "','" + ddlDDType.SelectedItem.Text + "'");
            BindData();
        }
        //--------------------
    }
    protected void btnCloseVesssel_Click(object sender, EventArgs e)
    {
        dv_AddVessel.Visible = false;
    }
    protected void ddlStatus_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
        
    }
    //--------------------------
    protected void BindVessels()
    {
        ddlVessel.DataSource = Common.Execute_Procedures_Select_ByQuery("SELECT VESSELNAME,VESSELID FROM DBO.VESSEL V where V.VESSELID NOT IN (SELECT VT.VESSELID FROM DBO.DD_VesselTracker VT) AND V.VESSELSTATUSID=1  ORDER BY VESSELNAME");
        ddlVessel.DataTextField = "VESSELNAME";
        ddlVessel.DataValueField = "VESSELID";
        ddlVessel.DataBind();
        ddlVessel.Items.Insert(0, new ListItem(" < Select Vessel > ", ""));
    }

    protected void BindData()
    {
        string sql = "SELECT * FROM VW_VESSELTRACKER WHERE 1=1 ";

        string whereclause = "";
        if(ddlYear.SelectedIndex>0)
            whereclause = " AND YEAR(NEXTDUEDATE)=" + ddlYear.SelectedValue;

        if (ddlFleet.SelectedIndex > 0)
            whereclause = " AND FLEETID=" + ddlFleet.SelectedValue;

        rptDocket.DataSource = Common.Execute_Procedures_Select_ByQuery(sql + whereclause + " ORDER BY VESSELNAME ");
        rptDocket.DataBind();

        sql = "SELECT (SELECT COUNT(1) FROM DBO.DD_VesselTracker WHERE YEAR(NEXTDUEDATE)=" + DateTime.Today.Year.ToString() + ") CYEAR, (SELECT COUNT(1) FROM DBO.DD_VesselTracker WHERE YEAR(NEXTDUEDATE)=" + DateTime.Today.AddYears(1).Year.ToString() + ") AS NYEAR";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
        if (dt.Rows.Count > 0)
        {
            lblCurryearCount.Text = "Drydock Count ( " + DateTime.Today.Year.ToString() + " ) : " + Common.CastAsInt32(dt.Rows[0][0]).ToString() + " Records.";
            lblNextyearCount.Text = "Drydock Count (" + (DateTime.Today.Year+1).ToString() + ") : " + Common.CastAsInt32(dt.Rows[0][1]).ToString() + " Records.";
        }
        
    }
    
}
