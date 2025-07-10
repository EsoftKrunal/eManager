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
using ShipSoft.CrewManager.BusinessObjects;
using ShipSoft.CrewManager.BusinessLogicLayer;
using ShipSoft.CrewManager.Operational;

public partial class CrewOperation_VesselPlanning : System.Web.UI.Page
{
    Authority Auth;
    public AuthenticationManager authPage = new AuthenticationManager(0, 0, ObjectType.Page);
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        Session["PageName"] = " - Vessel Planning";         
        //***********Code to check page acessing Permission
        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 3);
        if (chpageauth <= 0)
        {
            Response.Redirect("Dummy2.aspx");
        }
        //*******************
        ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 3);
        OBJ.Invoke();
        Session["Authority"] = OBJ.Authority;
        Auth = OBJ.Authority;
        authPage = new AuthenticationManager(3, Convert.ToInt32(Session["loginid"].ToString()), ObjectType.Page);
        lb_msg.Text = "";         
        if (!(IsPostBack))
        {
            bindddl_VesselName();
        }
    }

    
    //-- Top Page
    #region PageLoaderControl
    public void bindddl_VesselName()
    {
        DataSet dt8 = SearchSignOff.getVessel(Convert.ToInt32(Session["loginid"].ToString()));
        this.ddl_VesselName.DataSource = dt8;
        this.ddl_VesselName.DataValueField = "VesselId";
        this.ddl_VesselName.DataTextField = "Name";
        this.ddl_VesselName.DataSource = dt8;
        this.ddl_VesselName.DataBind();
        ddl_VesselName.Items.Insert(0, new ListItem("< Select >", "0"));
    }
    #endregion
    protected void ddl_VesselName_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_VesselName.SelectedIndex == 0)
        {
            this.rpt_CrewList.DataSource = null;
            this.rpt_CrewList.DataBind();
        }
        else
        {
            binddata();
        }
    }
    private void binddata()//String Sort)
    {
        DataSet ds2 = NewPlanning.getCrewDetails(ddl_VesselName.SelectedValue);
        this.rpt_CrewList.DataSource = ds2.Tables[0];
        this.rpt_CrewList.DataBind();
        
    }

    protected void btnCL_Click(object sender, EventArgs e)
    {
        string PlanningId = ((ImageButton)sender).CommandArgument;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "c", "window.open('ViewCrewCheckList.aspx?_P=" + PlanningId + "');", true);
    }
    protected void btnAddCrew_Click(object sender, EventArgs e)
    {
        if (ddl_VesselName.SelectedIndex == 0)
        {
            lb_msg.Text = "Please select vessel.";
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "c", "alert('Please select vessel.');", true);
            ddl_VesselName.Focus();
            return;
        }

        frmCrewSearch.Attributes.Add("src", "VesselPlanning_CrewSearch.aspx?VesselId=" + ddl_VesselName.SelectedValue.Trim() + "&VesselName=" + ddl_VesselName.SelectedItem.Text.Trim());
        dv_CrewSearch.Visible = true;
        //frmCrewSearch.Attributes.Add("src", "VesselPlanning_CrewSearch.aspx");
        
    }
    protected void imgbtnDelete_Click(object sender, EventArgs e)
    {
        
        int res;         
        DataTable dtroleid = SearchSignOff.getCrewRoleId(Convert.ToInt32(Session["loginid"].ToString()));
        foreach (DataRow dr in dtroleid.Rows)
        {
            if (Convert.ToInt32(dr["RoleId"]) != 4)
            {
                ImageButton btn = ((ImageButton)sender);
                int CrewId = Convert.ToInt32(btn.CommandArgument);
                res = SearchSignOff.Check_Planning_Deleted(Convert.ToInt32(ddl_VesselName.SelectedValue), CrewId);
                if (res == 1)
                {
                    lb_msg.Text = "Contract has been created for this Crew Member.";
                    return;
                }
                else if (res == 2)
                {
                    lb_msg.Text = "RFQ has been created for this Crew Member.";
                    return;
                }
                else if (res == 3)
                {
                    lb_msg.Text = "RFQ has been created for this Crew Member.";
                    return;
                }
                else
                {
                    SearchSignOff.DeleteReliver_planning(CrewId, Convert.ToInt32(ddl_VesselName.SelectedValue));
                }
            }
            else
            {
                lb_msg.Text = "ReadOnly Users Are Not Authorized to Delete.";
            }
        }
        
        binddata();
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        ddl_VesselName_SelectedIndexChanged(sender, e);
        dv_CrewSearch.Visible = false;
    }

    protected void btnCrewListforImoVessel_Click(object sender, EventArgs e)
    {
        //if (rpt_CrewList.Items.Count > 0)
        //{
            ScriptManager.RegisterStartupScript(this, this.GetType(), "c", "window.open('../Reporting/CrewListReportforImoVessel.aspx?VesselId=" + ddl_VesselName.SelectedValue + "&flag=2');", true);
        //}
    }
}