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
using System.Data.SqlClient;
using System.Xml;
using ShipSoft.CrewManager.BusinessLogicLayer;
using ShipSoft.CrewManager.BusinessObjects;
using ShipSoft.CrewManager.Operational;
using System.IO;

public partial class NavigationAudit : System.Web.UI.Page
{
    string VesselId = "";
    int intLogin_Id = 0;
    Authority Auth;
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        //--------------------------------- PAGE ACCESS AUTHORITY ----------------------------------------------------
                    int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 164);
        if (chpageauth <= 0)
            Response.Redirect("blank.aspx");
        //------------------------------------------------------------------------------------------------------------
           try
           {
            //this.Form.DefaultButton = this.btn_Show.UniqueID.ToString();
            //lblMessage.Text = ""+Session["ms"];
            if (Session["loginid"] == null)
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Default.aspx';", true);
            }
            else
            {
                intLogin_Id = Convert.ToInt32(Session["loginid"].ToString());
            }
            if (Session["loginid"] != null)
            {
                ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()),13);
                OBJ.Invoke();
                Session["Authority"] = OBJ.Authority;
                Auth = OBJ.Authority;
            }
            if (Session["loginid"] != null)
            {
                HiddenField_LoginId.Value = Session["loginid"].ToString();
            }
            if (!Page.IsPostBack)
            {
                Session["FormSelection"] = "1";
                for(int i=DateTime.Today.Year ;i>=2012;i--)
                {
                    ddlYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                BindFleet();
                BindVessel();
                btn_Show_Click(sender, e);
            }
            else
            {
                Session.Remove("FormSelection");
            }
        }
        catch (Exception ex) { throw ex; }
    }
    protected void btn_Show_Click(object sender, EventArgs e)
    {
        ViewState["Last"] = "Show";
        try
        {
            int intCrewId = 0;
            Grd_NearMiss.DataSource = null;
            Grd_NearMiss.DataBind();

            if (ddlVessel.SelectedIndex == 0)
            {
                for (int J = 1; J < ddlVessel.Items.Count; J++)
                {
                    //if (ddlVessel.Items[J].Selected == true)
                    //{
                    if (VesselId == "")
                    {
                        VesselId = ddlVessel.Items[J].Value;
                    }
                    else
                    {
                        VesselId = VesselId + "," + ddlVessel.Items[J].Value;
                    }
                    //}
                }
            }
            else
            {
                VesselId = ddlVessel.SelectedValue;
            }
            string CYear = ddlYear.SelectedValue; 
            string sql = "select v.vesselcode,vesselname, " +
                        "(select vnano from vna_master vm where vm.vesselcode=v.vesselcode and vm.vnano=v.vesselcode+'-" + CYear + "-Q1') as Q1, " +
                        "(select vnano from vna_master vm where vm.vesselcode=v.vesselcode and vm.vnano=v.vesselcode+'-" + CYear + "-Q2') as Q2, " +
                        "(select vnano from vna_master vm where vm.vesselcode=v.vesselcode and vm.vnano=v.vesselcode+'-" + CYear + "-Q3') as Q3, " +
                        "(select vnano from vna_master vm where vm.vesselcode=v.vesselcode and vm.vnano=v.vesselcode+'-" + CYear + "-Q4') as Q4, " +
                        "(select case when VERIFIED=1 then 'inherit' else 'red' end from vna_master vm where vm.vesselcode=v.vesselcode and vm.vnano=v.vesselcode+'-" + CYear + "-Q1') as V1, " +
                        "(select case when VERIFIED=1 then 'inherit' else 'red' end from vna_master vm where vm.vesselcode=v.vesselcode and vm.vnano=v.vesselcode+'-" + CYear + "-Q2') as V2, " +
                        "(select case when VERIFIED=1 then 'inherit' else 'red' end from vna_master vm where vm.vesselcode=v.vesselcode and vm.vnano=v.vesselcode+'-" + CYear + "-Q3') as V3, " +
                        "(select case when VERIFIED=1 then 'inherit' else 'red' end from vna_master vm where vm.vesselcode=v.vesselcode and vm.vnano=v.vesselcode+'-" + CYear + "-Q4') as V4 " +
                        "from dbo.vessel v where  " +
                        "vesselid in (" + VesselId+ ") order by v.vesselname";
            DataTable dt3 = VesselReporting.getTable(sql).Tables[0]; 
            if (dt3.Rows.Count > 0)
            {
                Grd_NearMiss.DataSource = dt3;
                Grd_NearMiss.DataBind();
            }
            else
            {
                BindBlankGrid();
            }
        }
        catch (Exception ex) { throw ex; }
    }
    protected void Grd_NearMiss_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            if( ViewState["Last"].ToString()=="Show")
                btn_Show_Click(sender, e);
            //else
            //    btn_ShowAll_Click(sender, e);
        }
        catch (Exception ex) { throw ex; }
    }
    protected void ddlFleet_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        BindVessel();
        //BindGrid();
    }
    protected void chk_Inactive_OnCheckedChanged(object sender, EventArgs e)
    {
        BindVessel();
    }
    protected void btnClear_OnClick(object sender, EventArgs e)
    {
        ddlFleet.SelectedIndex = 0;
        chk_Inactive.Checked = false;
        BindVessel();
        ddlVessel.SelectedIndex = 0;
        btn_Show_Click(sender, e);
    }
    //protected void btnClose2_Click(object sender, EventArgs e)
    //{
    //    dvAccountBox.Visible = false; 
    //}
    //Added function or event
    public void BindBlankGrid()
    {
        try
        {
            DataTable dt = new DataTable();
            DataRow dr;
            dt.Columns.Add("VesselId");
            dt.Columns.Add("VesselName");
            dt.Columns.Add("KPI");
            dt.Columns.Add("TotalReceived");
            dt.Columns.Add("TotalVerified");

            for (int i = 0; i < 13; i++)
            {
                dr = dt.NewRow();
                dt.Rows.Add(dr);
                dt.Rows[dt.Rows.Count - 1][0] = "";
                dt.Rows[dt.Rows.Count - 1][1] = "";
                dt.Rows[dt.Rows.Count - 1][2] = "";
                dt.Rows[dt.Rows.Count - 1][3] = "";
                dt.Rows[dt.Rows.Count - 1][4] = "";
            }

            Grd_NearMiss.DataSource = dt;
            Grd_NearMiss.DataBind();
            //Grd_NearMiss.SelectedIndex = -1;
        }
        catch (Exception ex) { throw ex; }
    }
    public void BindFleet()
    {
        string Query = "select * from dbo.FleetMaster";
        ddlFleet.DataSource = Budget.getTable(Query);
        ddlFleet.DataTextField = "FleetName";
        ddlFleet.DataValueField = "FleetID";
        ddlFleet.DataBind();
        ddlFleet.Items.Insert(0, new ListItem("<--All-->", "0"));
    }
    public void BindVessel()
    {
        string WhereClause = "";
        string sql = "SELECT VesselID,Vesselname FROM DBO.Vessel v Where 1=1 ";
        if (!chk_Inactive.Checked)
        {
            WhereClause = " and v.VesselStatusid<>2 ";
        }
        if (ddlFleet.SelectedIndex != 0)
        {
            WhereClause = WhereClause + " and fleetID=" + ddlFleet.SelectedValue + "";
        }
        sql = sql + WhereClause + "ORDER BY VESSELNAME";
        ddlVessel.DataSource = VesselReporting.getTable(sql);

        ddlVessel.DataTextField = "Vesselname";
        ddlVessel.DataValueField = "VesselID";
        ddlVessel.DataBind();
        ddlVessel.Items.Insert(0, new ListItem("<--All-->", "0"));
    }
}
