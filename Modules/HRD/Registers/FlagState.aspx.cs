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

public partial class Registers_FlagState : System.Web.UI.Page
{
    int id;
    Authority Auth;
    string Mode = "New";
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        lbl_FlagState_Message.Text = "";
        lbl_GridView_FlagState.Text = "";
       
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
            bindFlagStateGrid();
            bindStatusDDL();
            Alerts.HidePanel(pnl_FlagState);
            Alerts.HANDLE_AUTHORITY(1, btn_Add_FlagState, btn_Save_FlagState, btn_Cancel_FlagState, btn_Print_FlagState, Auth);     
        }
    }
    public void bindFlagStateGrid()
    {
        DataTable dt1 = FlagState.selectDataFlagStateDetails();
        this.GridView_FlagState.DataSource = dt1;
        this.GridView_FlagState.DataBind();
    }
    public void bindStatusDDL()
    {
        DataTable dt2 = FlagState.selectDataStatusDetails();
        this.ddlStatus_FlagState.DataValueField = "StatusId";
        this.ddlStatus_FlagState.DataTextField = "StatusName";
        this.ddlStatus_FlagState.DataSource = dt2;
        this.ddlStatus_FlagState.DataBind();
    }
    protected void btn_Add_FlagState_Click(object sender, EventArgs e)
    {
        txtFlagStateName.Text = "";
        txtCreatedBy_FlagState.Text = "";
        txtCreatedOn_FlagState.Text = "";
        txtModifiedBy_FlagState.Text = "";
        txtModifiedOn_FlagState.Text = "";
        txtFlagCode.Text = ""; 
        ddlStatus_FlagState.SelectedIndex = 0;
        GridView_FlagState.SelectedIndex = -1;
        HiddenFlagState.Value = "";
     
        Alerts.ShowPanel(pnl_FlagState);
        Alerts.HANDLE_AUTHORITY(2, btn_Add_FlagState, btn_Save_FlagState, btn_Cancel_FlagState, btn_Print_FlagState, Auth);
    }
    protected void btn_Save_FlagState_Click(object sender, EventArgs e)
    {
        int Duplicate=0;
        
            foreach (GridViewRow dg in GridView_FlagState.Rows)
            {
                HiddenField hfd;
                HiddenField hfd1;
                hfd = (HiddenField)dg.FindControl("HiddenFlagStateName");
                hfd1 = (HiddenField)dg.FindControl("HiddenFlagStateId");

                if (hfd.Value.ToString().ToUpper().Trim() == txtFlagStateName.Text.ToUpper().Trim())
                {
                    if (HiddenFlagState.Value.Trim() == "")
                    {
                        lbl_FlagState_Message.Text = "Already Entered.";
                        Duplicate = 1;
                        break;
                    }
                    else if (HiddenFlagState.Value.Trim() != hfd1.Value.ToString())
                    {
                        lbl_FlagState_Message.Text = "Already Entered.";
                        Duplicate = 1;
                        break;
                    }
                }
                else
                {
                    lbl_FlagState_Message.Text = "";
                }
            }
            if (Duplicate == 0)
            {
                int intFlagStateId = -1;
                int intCreatedBy = 0;
                int intModifiedBy = 0;
                if (HiddenFlagState.Value.ToString().Trim() == "")
                {
                    intCreatedBy = Convert.ToInt32(Session["loginid"].ToString());
                }
                else
                {
                    intFlagStateId = Convert.ToInt32(HiddenFlagState.Value);
                    intModifiedBy = Convert.ToInt32(Session["loginid"].ToString());
                }
                string strFlagStateName = txtFlagStateName.Text;
                char charStatusId = Convert.ToChar(ddlStatus_FlagState.SelectedValue);

                FlagState.insertUpdateFlagStateDetails("InsertUpdateFlagStateDetails",
                                              intFlagStateId,
                                              strFlagStateName,
                                              intCreatedBy,
                                              intModifiedBy,
                                              charStatusId,txtFlagCode.Text.Trim());

                bindFlagStateGrid();
                lbl_FlagState_Message.Text = "Record Successfully Saved.";
              
                Alerts.HidePanel(pnl_FlagState);
                Alerts.HANDLE_AUTHORITY(3, btn_Add_FlagState, btn_Save_FlagState, btn_Cancel_FlagState, btn_Print_FlagState, Auth);
            }
    }
    protected void btn_Cancel_FlagState_Click(object sender, EventArgs e)
    {
        GridView_FlagState.SelectedIndex = -1;
        
        Alerts.HidePanel(pnl_FlagState);
        Alerts.HANDLE_AUTHORITY(6, btn_Add_FlagState, btn_Save_FlagState, btn_Cancel_FlagState, btn_Print_FlagState, Auth);
    }
    protected void GridView_FlagState_DataBound(object sender, EventArgs e)
    {
        Alerts.HANDLE_GRID(GridView_FlagState, Auth);  
    }
    protected void btn_Print_FlagState_Click(object sender, EventArgs e)
    {

    }
    protected void Show_Record_FlagState(int FlagStateid)
    {
        HiddenFlagState.Value = FlagStateid.ToString();
        DataTable dt3 = FlagState.selectDataFlagStateDetailsByFlagStateId(FlagStateid);
        foreach (DataRow dr in dt3.Rows)
        {
            txtFlagStateName.Text = dr["FlagStateName"].ToString();
            txtFlagCode.Text = dr["Flag_Code"].ToString();
            txtCreatedBy_FlagState.Text = dr["CreatedBy"].ToString();
            txtCreatedOn_FlagState.Text = dr["CreatedOn"].ToString();
            txtModifiedBy_FlagState.Text = dr["ModifiedBy"].ToString();
            txtModifiedOn_FlagState.Text = dr["ModifiedOn"].ToString();
            ddlStatus_FlagState.SelectedValue = dr["StatusId"].ToString();
        }
    }
    protected void GridView_FlagState_SelectedIndexChanged(object sender, EventArgs e)
    {
      
        HiddenField hfdFlagState;
        hfdFlagState = (HiddenField)GridView_FlagState.Rows[GridView_FlagState.SelectedIndex].FindControl("HiddenFlagStateId");
        id = Convert.ToInt32(hfdFlagState.Value.ToString());
        Show_Record_FlagState(id);
        //----------------
        Alerts.ShowPanel(pnl_FlagState);
        Alerts.HANDLE_AUTHORITY(4, btn_Add_FlagState, btn_Save_FlagState, btn_Cancel_FlagState, btn_Print_FlagState, Auth);
    }
    protected void GridView_FlagState_Row_Editing(object sender, GridViewEditEventArgs e)
    {
        HiddenField hfdFlagState;
        hfdFlagState = (HiddenField)GridView_FlagState.Rows[e.NewEditIndex].FindControl("HiddenFlagStateId");
        id = Convert.ToInt32(hfdFlagState.Value.ToString());
        Show_Record_FlagState(id);
        GridView_FlagState.SelectedIndex = e.NewEditIndex;
        
        Alerts.ShowPanel(pnl_FlagState);
        Alerts.HANDLE_AUTHORITY(5, btn_Add_FlagState, btn_Save_FlagState, btn_Cancel_FlagState, btn_Print_FlagState, Auth);
    }
    protected void GridView_FlagState_Row_Deleting(object sender, GridViewDeleteEventArgs e)
    {
        int modifiedBy = Convert.ToInt32(Session["loginid"].ToString());
        HiddenField hfdFlagState;
        hfdFlagState = (HiddenField)GridView_FlagState.Rows[e.RowIndex].FindControl("HiddenFlagStateId");
        id = Convert.ToInt32(hfdFlagState.Value.ToString());
        FlagState.deleteFlagStateDetailsById("DeleteFlagStateById", id, modifiedBy);
        bindFlagStateGrid();
        if (HiddenFlagState.Value.ToString() == hfdFlagState.Value.ToString())
        {
            btn_Add_FlagState_Click(sender, e);
        }
    }
    protected void GridView_FlagState_PreRender(object sender, EventArgs e)
    {
        if (GridView_FlagState.Rows.Count <= 0) { lbl_GridView_FlagState.Text = "No Records Found..!"; }
    }

    protected void GridView_FlagState_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Modify")
        {
            GridViewRow row = (GridViewRow)(((ImageButton)sender).NamingContainer);
            int Rowindx = row.RowIndex;
            Mode = "Edit";
            HiddenField hfdFlagState;
            hfdFlagState = (HiddenField)GridView_FlagState.Rows[Rowindx].FindControl("hdnFlagStateId");
            id = Convert.ToInt32(hfdFlagState.Value.ToString());
            Show_Record_FlagState(id);
            GridView_FlagState.SelectedIndex = Rowindx;

            Alerts.ShowPanel(pnl_FlagState);
            Alerts.HANDLE_AUTHORITY(5, btn_Add_FlagState, btn_Save_FlagState, btn_Cancel_FlagState, btn_Print_FlagState, Auth);
        }
        }
    protected void btnEditFlagState_click(object sender, ImageClickEventArgs e)
    {
        GridViewRow row = (GridViewRow)(((ImageButton)sender).NamingContainer);
        int Rowindx = row.RowIndex;
        Mode = "Edit";
        HiddenField hfdFlagState;
        hfdFlagState = (HiddenField)GridView_FlagState.Rows[Rowindx].FindControl("hdnFlagStateId");
        id = Convert.ToInt32(hfdFlagState.Value.ToString());
        Show_Record_FlagState(id);
        GridView_FlagState.SelectedIndex = Rowindx;

        Alerts.ShowPanel(pnl_FlagState);
        Alerts.HANDLE_AUTHORITY(5, btn_Add_FlagState, btn_Save_FlagState, btn_Cancel_FlagState, btn_Print_FlagState, Auth);
    }
}