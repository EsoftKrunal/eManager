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
using System.IO;

public partial class Registers_ManningAgent : System.Web.UI.Page
{
    int id;
    Authority Auth;
    string Mode = "New";
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        lbl_ManningAgent_Message.Text = "";
        lblManningAgent.Text = "";


        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 10);
        if (chpageauth <= 0)
        {
            Response.Redirect("Dummy.aspx");

        }

        ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 10);
        OBJ.Invoke();
        Session["Authority"] = OBJ.Authority;
        Auth = OBJ.Authority;
        if (!Page.IsPostBack)
        {
            BindStatusDropDown();
            BindGridMinningAgent();
            Alerts.HidePanel(ManningAgentpanel);
            Alerts.HANDLE_AUTHORITY(1, btn_add, btn_save, btn_Cancel, btn_Print, Auth);

        }
    }
    private void BindStatusDropDown()
    {
        DataTable dt2 = ManningAgent.selectDataStatus();
        this.ddstatus_ManningAgent.DataValueField = "StatusId";
        this.ddstatus_ManningAgent.DataTextField = "StatusName";
        this.ddstatus_ManningAgent.DataSource = dt2;
        this.ddstatus_ManningAgent.DataBind();
    }
    private void BindGridMinningAgent()
    {
        DataTable dt = ManningAgent.selectDataManningAgentDetails();
        this.GvManningAgent.DataSource = dt;
        this.GvManningAgent.DataBind();
    }

    protected void btn_save_Click(object sender, EventArgs e)
    {
        int Duplicate = 0;

        foreach (GridViewRow dg in GvManningAgent.Rows)
        {
            HiddenField hfd;
            HiddenField hfd1;
            hfd = (HiddenField)dg.FindControl("HiddenManningAgentName");
            hfd1 = (HiddenField)dg.FindControl("HiddenManningAgentId");

            if (hfd.Value.ToString().ToUpper().Trim() == txtManningAgent.Text.ToUpper().Trim())
            {
                if (HiddenManningAgentpk.Value.Trim() == "")
                {
                    lbl_ManningAgent_Message.Text = "Already Entered.";
                    Duplicate = 1;
                    break;
                }
                else if (HiddenManningAgentpk.Value.Trim() != hfd1.Value.ToString())
                {
                    lbl_ManningAgent_Message.Text = "Already Entered.";
                    Duplicate = 1;
                    break;
                }
            }
            else
            {
                lbl_ManningAgent_Message.Text = "";
            }
        }
        if (Duplicate == 0)
        {
            int manningagentId = -1;
            int createdby = 0, modifiedby = 0;

            string strMinningAgentName = txtManningAgent.Text.Trim();
            string strContactPerson = txtContactPerson.Text.Trim();
            string strContactNo = txtContactNo.Text.Trim();
            string strEmail = txtmailId.Text.Trim();
            char status = Convert.ToChar(ddstatus_ManningAgent.SelectedValue);
            if (HiddenManningAgentpk.Value.Trim() == "")
            {
                createdby = Convert.ToInt32(Session["loginid"].ToString());
            }
            else
            {
                manningagentId = Convert.ToInt32(HiddenManningAgentpk.Value);
                modifiedby = Convert.ToInt32(Session["loginid"].ToString());
            }
            ManningAgent.insertUpdateManningAgentDetails("InsertUpdateMinningAgentDetails",
                                                      manningagentId,
                                                      strMinningAgentName,
                                                      createdby,
                                                      modifiedby,
                                                      status,
                                                      strContactPerson,
                                                      strContactNo,
                                                      strEmail);
            BindGridMinningAgent();
            lbl_ManningAgent_Message.Text = "Record Successfully Saved.";
            Alerts.HidePanel(ManningAgentpanel);
            Alerts.HANDLE_AUTHORITY(3, btn_add, btn_save, btn_Cancel, btn_Print, Auth);

        }
    }

    protected void btn_add_Click(object sender, EventArgs e)
    {
        HiddenManningAgentpk.Value = "";
        txtManningAgent.Text = "";
        txtcreatedby_Department.Text = "";
        GvManningAgent.SelectedIndex = -1;
        txtcreatedon_Department.Text = "";
        txtmodifiedby_Department.Text = "";
        txtmodifiedon_Department.Text = "";
        ddstatus_ManningAgent.SelectedIndex = 0;
        txtContactPerson.Text = "";
        txtContactNo.Text = "";
        txtmailId.Text = "";
        Alerts.ShowPanel(ManningAgentpanel);
        Alerts.HANDLE_AUTHORITY(2, btn_add, btn_save, btn_Cancel, btn_Print, Auth);
    }

    protected void GvManningAgent_SelectIndexChanged(object sender, EventArgs e)
    {
        HiddenField hfdManningAgent;
        hfdManningAgent = (HiddenField)GvManningAgent.Rows[GvManningAgent.SelectedIndex].FindControl("HiddenManningAgentId");
        id = Convert.ToInt32(hfdManningAgent.Value.ToString());
        Show_Record_ManningAgent(id);
        Alerts.ShowPanel(ManningAgentpanel);
        Alerts.HANDLE_AUTHORITY(4, btn_add, btn_save, btn_Cancel,btn_Print, Auth);
    }

    protected void Show_Record_ManningAgent(int manningAgentId)
    {

        HiddenManningAgentpk.Value = manningAgentId.ToString();
        DataTable dt3 = ManningAgent.selectDataManningAgentDetailsByAgentId(manningAgentId);
        foreach (DataRow dr in dt3.Rows)
        {
            txtManningAgent.Text = dr["Manning_AgentName"].ToString();
            txtcreatedby_Department.Text = dr["CreatedBy"].ToString();
            txtcreatedon_Department.Text = dr["CreatedOn"].ToString();
            txtmodifiedby_Department.Text = dr["ModifiedBy"].ToString();
            txtmodifiedon_Department.Text = dr["ModifiedOn"].ToString();
            ddstatus_ManningAgent.SelectedValue = dr["StatusId"].ToString().Trim();
            txtContactPerson.Text = dr["ContactPerson"].ToString().Trim();
            txtContactNo.Text = dr["ContactNo"].ToString().Trim();
            txtmailId.Text = dr["Email"].ToString().Trim();
            
        }

    }

    protected void btn_Print_Click(object sender, EventArgs e)
    {

    }

    protected void btn_Cancel_Click(object sender, EventArgs e)
    {
        GvManningAgent.SelectedIndex = -1;
        Alerts.HidePanel(ManningAgentpanel);
        Alerts.HANDLE_AUTHORITY(6, btn_add, btn_save, btn_Cancel, btn_Print, Auth);
    }

    protected void GvManningAgent_Row_Deleting(object sender, GridViewDeleteEventArgs e)
    {
        int intModifiedBy = Convert.ToInt32(Session["loginid"].ToString());
        HiddenField hfdMinningAgent;
        hfdMinningAgent = (HiddenField)GvManningAgent.Rows[e.RowIndex].FindControl("HiddenManningAgentId");
        id = Convert.ToInt32(hfdMinningAgent.Value.ToString());
        ManningAgent.deleteManningAgentDetails("deleteMinningAgent", id, intModifiedBy);
        BindGridMinningAgent();
        if (HiddenManningAgentpk.Value.Trim() == hfdMinningAgent.Value.ToString())
        {
            btn_add_Click(sender, e);
        }
    }

    //protected void GvManningAgent_Row_Editing(object sender, GridViewEditEventArgs e)
    //{
    //    Mode = "Edit";
    //    HiddenField hfdrank;
    //    hfdrank = (HiddenField)GvManningAgent.Rows[e.NewEditIndex].FindControl("HiddenManningAgentId");
    //    id = Convert.ToInt32(hfdrank.Value.ToString());

    //    Show_Record_ManningAgent(id);
    //    GvManningAgent.SelectedIndex = e.NewEditIndex;
    //    Alerts.ShowPanel(Departmentpanel);
    //    Alerts.HANDLE_AUTHORITY(5, btn_add, btn_save, btn_Cancel, btn_Print, Auth);
    //}

    protected void GvManningAgent_DataBound(object sender, EventArgs e)
    {
        Alerts.HANDLE_GRID(GvManningAgent, Auth);
    }

    protected void GvManningAgent_PreRender(object sender, EventArgs e)
    {
        if (this.GvManningAgent.Rows.Count <= 0)
        {
            lblManningAgent.Text = "No Records Found..!";
        }
    }

    protected void GvManningAgent_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Modify")
        {
            GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
            int Rowindx = row.RowIndex;
            Mode = "Edit";
            HiddenField hfdrank;
            hfdrank = (HiddenField)GvManningAgent.Rows[Rowindx].FindControl("HiddenManningAgentId");
            id = Convert.ToInt32(hfdrank.Value.ToString());

            Show_Record_ManningAgent(id);
            GvManningAgent.SelectedIndex = Rowindx;
            Alerts.ShowPanel(ManningAgentpanel);
            Alerts.HANDLE_AUTHORITY(5, btn_add, btn_save, btn_Cancel, btn_Print, Auth);
        }
    }

    protected void GvManningAgent_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {

    }

    protected void btnEditManningAgent_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow row = (GridViewRow)(((ImageButton)sender).NamingContainer);
        int Rowindx = row.RowIndex;
        Mode = "Edit";
        HiddenField hfdrank;
        hfdrank = (HiddenField)GvManningAgent.Rows[Rowindx].FindControl("HiddenManningAgentId");
        id = Convert.ToInt32(hfdrank.Value.ToString());

        Show_Record_ManningAgent(id);
        GvManningAgent.SelectedIndex = Rowindx;
        // Alerts.ShowPanel(Departmentpanel);
        // Alerts.HANDLE_AUTHORITY(5, btn_add, btn_save, btn_Cancel, btn_Print, Auth);

        ManningAgentpanel.Visible = true;
        btn_Cancel.Visible = true;
        btn_save.Visible = true;
        btn_add.Visible = false;
    }
}