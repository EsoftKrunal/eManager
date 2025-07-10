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

public partial class SiteAdministration_UserRoles : System.Web.UI.Page
{
    int id;
    Authority Auth;
    string Mode = "New";
    protected void Page_Load(object sender, EventArgs e)
    {
        ProcessCheckAuthority OBJ = new ProcessCheckAuthority(1, 5);
        OBJ.Invoke();
        Session["Authority"] = OBJ.Authority;
        Auth = OBJ.Authority;
        string id = Session["loginid"].ToString();
        if (!Page.IsPostBack)
        {
            bindUserRoleGrid();
            bindStatusDDL();
            HANDLE_AUTHORITY();
        }
    }
    private void HANDLE_AUTHORITY()
    {
        if (Mode == "New")
        {
            btn_Add_UserRole.Visible = ((Auth.isEdit || Auth.isAdd) && (Mode == "New" || Mode == "Edit"));
            btn_Save_UserRole.Visible = false;
            btn_Cancel_UserRole.Visible = false;
            pnl_UserRole.Visible = false;
        }
        else if (Mode == "Edit")
        {
            btn_Add_UserRole.Visible = ((Auth.isEdit || Auth.isAdd) && (Mode == "New" || Mode == "Edit"));
            btn_Save_UserRole.Visible = false;
            btn_Cancel_UserRole.Visible = false;
            pnl_UserRole.Visible = false;
        }
        else // Mode=View
        {
            btn_Add_UserRole.Visible = false;
            btn_Save_UserRole.Visible = false;
            btn_Cancel_UserRole.Visible = false;
            pnl_UserRole.Visible = false;
        }
    }
    public void bindUserRoleGrid()
    {
        DataTable dt1 = UserRoles.selectDataUserRoleDetails();
        this.GridView_UserRole.DataSource = dt1;
        this.GridView_UserRole.DataBind();
    }
    public void bindStatusDDL()
    {
        DataTable dt2 = UserRoles.selectDataStatusDetails();
        this.ddlStatus_UserRole.DataValueField = "StatusId";
        this.ddlStatus_UserRole.DataTextField = "StatusName";
        this.ddlStatus_UserRole.DataSource = dt2;
        this.ddlStatus_UserRole.DataBind();
    }
    protected void btn_Add_UserRole_Click(object sender, EventArgs e)
    {
        txtUserRoleName.Text = "";
        txtCreatedBy_UserRole.Text = "";
        txtCreatedOn_UserRole.Text = "";
        txtModifiedBy_UserRole.Text = "";
        txtModifiedOn_UserRole.Text = "";
        ddlStatus_UserRole.SelectedIndex = 0;
        HiddenUserRole.Value = "";
        pnl_UserRole.Visible = true;
        btn_Save_UserRole.Visible = true;
        btn_Cancel_UserRole.Visible = true;
        btn_Add_UserRole.Visible = false;
        lbl_UserRole_Message.Visible = false;
        btn_Print_UserRole.Visible = true;
    }
    protected void btn_Save_UserRole_Click(object sender, EventArgs e)
    {
        int intRoleSystemId = -1;
        int intCreatedBy = 0;
        int intModifiedBy = 0;
        if (HiddenUserRole.Value.ToString().Trim() == "")
            {
                intCreatedBy = 1;
            }
            else
            {
                intRoleSystemId = Convert.ToInt32(HiddenUserRole.Value);
                intModifiedBy = 53;
            }
            string strUserRoleName = txtUserRoleName.Text;
            int intRoleId = 0;
            char charStatusId = Convert.ToChar(ddlStatus_UserRole.SelectedValue);

            UserRoles.insertUpdateUserRoleDetails("InsertUpdateUserRoleDetails",
                                          intRoleSystemId,
                                          intRoleId,
                                          strUserRoleName,
                                          intCreatedBy,
                                          intModifiedBy,
                                          charStatusId);

            bindUserRoleGrid();
            btn_Add_UserRole.Visible = false;
            btn_Add_UserRole_Click(sender, e);
            btn_Cancel_UserRole_Click(sender, e);
            lbl_UserRole_Message.Visible = true;
            lbl_UserRole_Message.Text = "Record Successfully Saved.";       
    }
    protected void btn_Cancel_UserRole_Click(object sender, EventArgs e)
    {
        if (Mode == "New")
        {
            btn_Add_UserRole.Visible = (Auth.isAdd || Auth.isEdit);
        }
        else if (Mode == "Edit")
        {
            btn_Add_UserRole.Visible = (Auth.isAdd || Auth.isEdit);
        }
        pnl_UserRole.Visible = false;
        btn_Save_UserRole.Visible = false;
        btn_Cancel_UserRole.Visible = false;
        btn_Add_UserRole.Visible = ((Mode == "Edit") || (Mode == "New")) ? true : false;
        GridView_UserRole.SelectedIndex = -1;
        lbl_UserRole_Message.Visible = false;
        btn_Print_UserRole.Visible = false;
    }
    protected void btn_Print_UserRole_Click(object sender, EventArgs e)
    {
        lbl_UserRole_Message.Visible = false;
    }
    protected void GridView_UserRole_DataBound(object sender, EventArgs e)
    {
        try
        {
            
            // Can Add
            if (Auth.isAdd)
            {
            }

            // Can Modify

            this.GridView_UserRole.Columns[1].Visible = Auth.isEdit;

            this.GridView_UserRole.Columns[2].Visible = Auth.isDelete;


            // Can Print
            if (Auth.isPrint)
            {
            }

        }
        catch
        {
           
        }
    }
    protected void Show_Record_UserRole(int UserRoleid)
    {
        lbl_UserRole_Message.Visible = false;
        HiddenUserRole.Value = UserRoleid.ToString();
        DataTable dt3 = UserRoles.selectDataUserRoleDetailsByUserRoleId(UserRoleid);
        foreach (DataRow dr in dt3.Rows)
        {
            txtUserRoleName.Text = dr["RoleName"].ToString();
            txtCreatedBy_UserRole.Text = dr["CreatedBy"].ToString();
            txtCreatedOn_UserRole.Text = dr["CreatedOn"].ToString();
            txtModifiedBy_UserRole.Text = dr["ModifiedBy"].ToString();
            txtModifiedOn_UserRole.Text = dr["ModifiedOn"].ToString();
            ddlStatus_UserRole.SelectedValue = dr["StatusId"].ToString();
        }
        btn_Print_UserRole.Visible = true;
    }
    // VIEW THE RECORD
    protected void GridView_UserRole_SelectedIndexChanged(object sender, EventArgs e)
    {
        lbl_UserRole_Message.Visible = false;
        HiddenField hfdUserRole;
        hfdUserRole = (HiddenField)GridView_UserRole.Rows[GridView_UserRole.SelectedIndex].FindControl("HiddenRoleSystemId");
        id = Convert.ToInt32(hfdUserRole.Value.ToString());
        pnl_UserRole.Visible = true;
        Show_Record_UserRole(id);
        btn_Add_UserRole.Visible = ((Mode == "Edit") || (Mode == "New")) ? true : false;
        btn_Save_UserRole.Visible = false;
        btn_Cancel_UserRole.Visible = true;
    }
    //EDIT THE RECORD
    protected void GridView_UserRole_Row_Editing(object sender, GridViewEditEventArgs e)
    {
        lbl_UserRole_Message.Visible = false;
        Mode = "Edit";
        HiddenField hfdUserRole;
        hfdUserRole = (HiddenField)GridView_UserRole.Rows[e.NewEditIndex].FindControl("HiddenRoleSystemId");
        id = Convert.ToInt32(hfdUserRole.Value.ToString());
        Show_Record_UserRole(id);
        GridView_UserRole.SelectedIndex = e.NewEditIndex;
        pnl_UserRole.Visible = true;
        btn_Add_UserRole.Visible = ((Mode == "Edit") || (Mode == "New")) ? true : false;
        btn_Save_UserRole.Visible = true;
        btn_Cancel_UserRole.Visible = true;
    }
    // DELETE THE RECORD
    protected void GridView_UserRole_Row_Deleting(object sender, GridViewDeleteEventArgs e)
    {
        lbl_UserRole_Message.Visible = false;
        int modifiedBy = 11;
        HiddenField hfdUserRole;
        hfdUserRole = (HiddenField)GridView_UserRole.Rows[e.RowIndex].FindControl("HiddenRoleSystemId");
        id = Convert.ToInt32(hfdUserRole.Value.ToString());
        UserRoles.deleteUserRoleDetailsById("DeleteUserRoleDetailsById", id, modifiedBy);
        bindUserRoleGrid();
        if (HiddenUserRole.Value.ToString() == hfdUserRole.Value.ToString())
        {
            btn_Add_UserRole_Click(sender, e);
        }
    }
    protected void GridView_UserRole_PreRender(object sender, EventArgs e)
    {
        if (GridView_UserRole.Rows.Count <= 0) { lbl_GridView_UserRole.Text = "No Records Found..!"; }
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("AdminDashBoard.aspx");
    }
}
