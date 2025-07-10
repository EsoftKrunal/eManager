using System;
using System.Data;
using System.Configuration;
using System.Reflection;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using ShipSoft.CrewManager.BusinessLogicLayer;
using ShipSoft.CrewManager.BusinessObjects;
using ShipSoft.CrewManager.Operational;

public partial class VesselRecord_VesselRename : System.Web.UI.Page
{
    Authority obj;
    private void Load_Vessel()
    {
        DataSet ds = SearchSignOff.getVessel(Convert.ToInt32(Session["loginid"].ToString()));
        ddl_From.DataSource = ds.Tables[0];
        ddl_From.DataValueField = "VesselId";
        ddl_From.DataTextField = "VesselName1";
        ddl_From.DataBind();
        ddl_From.Items.Insert(0, new ListItem("< Select >", "0"));

        ddl_To.DataSource = ds.Tables[0];
        ddl_To.DataValueField = "VesselId";
        ddl_To.DataTextField = "VesselName1";
        ddl_To.DataBind();
        ddl_To.Items.Insert(0, new ListItem("< Select >", "0"));

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        Alerts.SetHelp(imgHelp);  
        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 43);
        if (chpageauth <= 0)
        {
            Response.Redirect("../AuthorityError.aspx");

        }
        //*******************
        ProcessCheckAuthority Auth = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 4);
        Auth.Invoke();
        Session["Authority"] = Auth.Authority;
        obj = (Authority)Session["Authority"];
        try
        {
            if (Session["UserName"] == "")
            {
                Response.Redirect("default.aspx");
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("default.aspx");
            return;
        }
        if (!(IsPostBack))
        {
            Load_Vessel();
        }
    }
    protected void btn_ShowMembers_Click(object sender, EventArgs e)
    {
        DataSet ds = VesselCrewMatrix.getData("SelectCrewmatrixData",int.Parse(ddl_From.SelectedValue) , "", "");
        gvmatrix.DataSource = ds;
        gvmatrix.DataBind();
        lblCnt.Text = "(" + ds.Tables[0].Rows.Count.ToString() + ") Records found.";     
    }
    protected void btn_Rename_Click(object sender, EventArgs e)
    {
        //----------------------------
        if (ddl_From.SelectedIndex <= 0)
        {
            lblMsg.Text = "Please select from vessel.";
            ddl_From.Focus();
            return; 
        }
        //----------------------------
        if (ddl_To.SelectedIndex <= 0)
        {
            lblMsg.Text = "Please select to vessel.";
            ddl_To.Focus();
            return;
        }
        //----------------------------
        if (txttodate.Text.Trim()=="")
        {
            lblMsg.Text = "Please enter date of rename.";
            txttodate.Focus(); 
            return;
        }
        //----------------------------
        try
        {
            string Qry = "EXEC RenameVessel " + ddl_From.SelectedValue.ToString() + "," + ddl_To.SelectedValue.ToString() + ",'" + txttodate.Text + "'," + Session["loginid"].ToString();
            DataSet ds = Budget.getTable(Qry);
            lblMsg.Text = "Vessel renamed successfully.";   
        }
        catch 
        {
            lblMsg.Text="Unable to rename vessel."; 
        } 
    }
}
    