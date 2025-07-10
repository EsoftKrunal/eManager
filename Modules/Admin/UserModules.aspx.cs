using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public partial class UserModules : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindListUsers();
            BindListAssigned();
        }
    }

    private void BindListUsers()
    {
        energiosSecurity.User usr = new energiosSecurity.User();
        ListUsers.SelectedIndex = -1;
        DataTable tb = new DataTable();
        tb = usr.GetAllUser();

        int ObjfilterUserscount = tb.AsEnumerable().Where(r => r.Field<string>("StatusId") == "A").Count();
        if (ObjfilterUserscount > 0)
        {
            DataTable selectedTable = tb.AsEnumerable().Where(r => r.Field<string>("StatusId") == "A").CopyToDataTable();
            ListUsers.DataSource = selectedTable;
            ListUsers.DataTextField = "UserName";
            ListUsers.DataValueField = "LoginId";
            ListUsers.DataBind();
            ListUsers.Items.Insert(0, "< Select User >");
        }
        else
        {
            ListUsers.DataSource = null;
            ListUsers.DataBind();
            ListUsers.Items.Insert(0, "< Please create a user to proceed >");
        }
    }

    private void BindListAssigned()
    {
        energiosSecurity.Module mod = new energiosSecurity.Module();
        ListAssigned.SelectedIndex = -1;

        DataTable tb = new DataTable();
        tb = mod.GetAllModules();
        DataTable selectedTable = tb.AsEnumerable().Where(r => r.Field<bool>("IsActive") == true && r.Field<string>("ShortName").ToUpper() != "SETTINGS").CopyToDataTable();
        ListAssigned.DataTextField = "ShortName";
        ListAssigned.DataValueField = "ModuleID";
        ListAssigned.DataSource = selectedTable;
        ListAssigned.DataBind();

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        energiosSecurity.UserModule uModule = new energiosSecurity.UserModule();
        int result = 0;
        if (ListUsers.SelectedIndex > 0)
        {
            foreach (ListItem item in ListAssigned.Items)
            {

                if (uModule.IsAlreadyExists(Convert.ToInt32(item.Value.ToString()), Convert.ToInt32(ListUsers.SelectedItem.Value.ToString())) == true)
                {
                    if (item.Selected == false)
                    {
                        //Disable record(make inactive)
                        SqlParameter[] parameters =
                                                    {   new SqlParameter( "@userid", Convert.ToInt32(ListUsers.SelectedItem.Value.ToString())),
                                                        new SqlParameter( "@moduleid", Convert.ToInt32( item.Value.ToString())),
                                                        new SqlParameter( "@IsActive", false),
                                                        new SqlParameter( "@ModifiedBy", Session["UserID"])
                                                    };
                        result = uModule.Update(parameters);
                    }
                    else
                    {
                        SqlParameter[] parameters =
                                                    {   new SqlParameter( "@userid", Convert.ToInt32(ListUsers.SelectedItem.Value.ToString())),
                                                        new SqlParameter( "@moduleid", Convert.ToInt32( item.Value.ToString())),
                                                        new SqlParameter( "@IsActive", true),
                                                        new SqlParameter( "@ModifiedBy", Session["UserID"])
                                                    };
                        result = uModule.Update(parameters);
                    }
                }
                else
                {
                    if (item.Selected == true)
                    {
                        //Insert record
                        SqlParameter[] parameters =
                                                {   new SqlParameter( "@userid"		, Convert.ToInt32(ListUsers.SelectedItem.Value.ToString()) ) ,
                                                    new SqlParameter( "@moduleid"		, Convert.ToInt32( item.Value.ToString()) ),
                                                    new SqlParameter( "@IsActive"		, true ),
                                                    new SqlParameter( "@CreatedBy"		, Session["UserID"]   )
                                                };
                        result = uModule.Insert(parameters);
                    }
                }
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", "ShowMessage('Success',' User Modules  updated Successfully.')", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", "ShowMessage('Error','Please select a user to proceed.')", true);
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        foreach (ListItem item in ListAssigned.Items)
        {
            item.Selected = false;
            this.ListUsers.SelectedIndex = -1;
        }
    }
    
    protected void ListUsers_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ListAssigned.Items.Clear();
        lblMsgType.Text = "";
        energiosSecurity.UserModule uModule = new energiosSecurity.UserModule();
        
        if (ListUsers.SelectedIndex > 0)
        {
           
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = uModule.GetUserModules(Convert.ToInt32(ListUsers.SelectedItem.Value.ToString()));
            foreach (ListItem item in ListAssigned.Items)
            {
                if (dt.Rows.Count > 0)
                {
                    if (dt.Select("ModuleID=" + Convert.ToInt32(item.Value)).Length > 0)
                    {
                        item.Selected = true;
                    }
                    else
                    {
                        item.Selected = false;
                    }
                }
                else
                {
                    item.Selected = false;
                }
            }

            if (dt.Rows.Count > 0)
            {
                ddlUserModules.DataSource = dt;
                ddlUserModules.DataTextField = "ModuleName";
                ddlUserModules.DataValueField = "ModuleId";
                ddlUserModules.DataBind();
                ddlUserModules.Items.Insert(0, "< Select Module >");
            }
            chkMenu.DataSource = null;
            chkMenu.DataBind();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", "ShowMessage('Error','Please select a user to proceed.')", true);
        }
    }

    protected void ddlUserModules_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindListMenus();
        }
        catch(Exception ex)
        { }
    }

    private void BindListMenus()
    {
        if (ddlUserModules.SelectedIndex > 0)
        {
            energiosSecurity.User usr = new energiosSecurity.User();
            energiosSecurity.Menu mnu = new energiosSecurity.Menu();
            chkMenu.SelectedIndex = -1;

            System.Data.DataTable dtMain = new System.Data.DataTable();
            dtMain = mnu.GetDetails_ByModuleId(Convert.ToInt32(ddlUserModules.SelectedValue));
            if (dtMain.Rows.Count > 0)
            {
                chkMenu.DataTextField = "menuName";
                chkMenu.DataValueField = "MenuId";
                chkMenu.DataSource = dtMain;
                chkMenu.DataBind();
            }

            System.Data.DataTable dt = new System.Data.DataTable();
            dt = mnu.GetMenuDetails_ByUserIdModuleId(Convert.ToInt32(ddlUserModules.SelectedValue),Convert.ToInt32(ListUsers.SelectedValue));
            foreach (ListItem item in chkMenu.Items)
            {
                if (dt.Rows.Count > 0)
                {
                    if (dt.Select("MenuId=" + Convert.ToInt32(item.Value)).Length > 0)
                    {
                        item.Selected = true;
                    }
                    else
                    {
                        item.Selected = false;
                    }
                }
                else
                {
                    item.Selected = false;
                }

            }
        }
    }
    protected void btnSubmitUserAccess_Click(object sender, EventArgs e)
    {
        energiosSecurity.Menu mnu = new energiosSecurity.Menu();
        int result = 0;
        if (ListUsers.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", "ShowMessage('Error','Please select a user to proceed.')", true);
            ListUsers.Focus();
            return;
        }
        if (ddlUserModules.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", "ShowMessage('Error','Please select module.')", true);
            ddlUserModules.Focus();
            return;
        }
        
        if (ListUsers.SelectedIndex > 0)
        {
            int userid = Convert.ToInt32(ListUsers.SelectedValue);
            int moduleId = Convert.ToInt32(ddlUserModules.SelectedValue);
            energiosSecurity.Menu.deleteUserMenuRelationDetails("deleteappmstr_UserMenuRelation", userid, moduleId);
            foreach (ListItem item in chkMenu.Items)
            {
                    if (item.Selected == true)
                    {
                        //Disable record(make inactive)
                        SqlParameter[] parameters =
                                                    {   new SqlParameter( "@LoginId", Convert.ToInt32(ListUsers.SelectedValue)),
                                                        new SqlParameter( "@Menuid", Convert.ToInt32(item.Value.ToString()))
                                                    };
                        result = mnu.InsertUpdateUserMenuAccess(parameters);
                    }
                    
                }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", "ShowMessage('Success',' User Menu Access updated Successfully.')", true);
        }       
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", "ShowMessage('Error','Please select a user to proceed.')", true);
        }
    }


    protected void btnCanceluserAccess_Click(object sender, EventArgs e)
    {
        foreach (ListItem item in chkMenu.Items)
        {
            item.Selected = false;
            this.chkMenu.SelectedIndex = -1;
        }
    }
}
