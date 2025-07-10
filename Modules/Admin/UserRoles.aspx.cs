using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public partial class Modules_Admin_UserRoles : System.Web.UI.Page
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
           DataTable selectedTable = tb.AsEnumerable()
                             .Where(r => r.Field<string>("StatusId") == "A")
                             .CopyToDataTable();
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
        energiosSecurity.Role rol = new energiosSecurity.Role();
       ListAssigned.SelectedIndex = -1;
       //ListAssigned.DataSource = rol.GetAll();
       DataTable tb = new DataTable();
       tb = rol.GetAll();
       DataTable selectedTable = tb.AsEnumerable()
                            //.Where(r => r.Field<bool>("IsActive") == true)
                            .CopyToDataTable();
       ListAssigned.DataSource = selectedTable;
       ListAssigned.DataTextField = "RoleName";
       ListAssigned.DataValueField = "RoleID";
       ListAssigned.DataBind();

   }
   
   protected void btnSubmit_Click(object sender, EventArgs e)
   {
        energiosSecurity.UserRole uRole = new energiosSecurity.UserRole();
       int result = 0;

       if (ListUsers.SelectedIndex > 0)
       {
           foreach (ListItem item in ListAssigned.Items)
           {

               if (uRole.IsAlreadyExists(Convert.ToInt32(item.Value.ToString()), Convert.ToInt32(ListUsers.SelectedItem.Value.ToString())) == true)
               {
                   if (item.Selected == false)
                   {
                       //Disable record(make inactive)
                       SqlParameter[] parameters =
                    {   new SqlParameter( "@userid"		, Convert.ToInt32(ListUsers.SelectedItem.Value.ToString()) ) ,
                        new SqlParameter( "@roleid"		, Convert.ToInt32( item.Value.ToString()) ),
                        new SqlParameter( "@IsActive"		, false ),
                        new SqlParameter( "@ModifiedBy"		, Session["UserID"]   )
                
                    };
                       result = uRole.Update(parameters);


                   }
                   else
                   {
                       SqlParameter[] parameters =
                    {   new SqlParameter( "@userid"		, Convert.ToInt32(ListUsers.SelectedItem.Value.ToString()) ) ,
                        new SqlParameter( "@roleid"		, Convert.ToInt32( item.Value.ToString()) ),
                        new SqlParameter( "@IsActive"		, true ),
                        new SqlParameter( "@ModifiedBy"		, Session["UserID"]   )
                
                    };
                       result = uRole.Update(parameters);
                   }

               }
               else
               {
                   if (item.Selected == true)
                   {
                       //Insert record

                       SqlParameter[] parameters =
                    {   new SqlParameter( "@userid"		, Convert.ToInt32(ListUsers.SelectedItem.Value.ToString()) ) ,
                        new SqlParameter( "@roleid"		, Convert.ToInt32( item.Value.ToString()) ),
                        new SqlParameter( "@IsActive"		, true ),
                        new SqlParameter( "@CreatedBy"		, Session["UserID"]   )
                
                    };
                       result = uRole.Insert(parameters);

                   }
               }

           }
           if (result == 1)
           {
               ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", "ShowMessage('Success',' User Role Updated Successfully.')", true);
           }
           else
           {
               ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", "ShowMessage('Error','Saving Process Failed.')", true);
           }
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
       energiosSecurity.UserRole uRole = new energiosSecurity.UserRole();
       if (ListUsers.SelectedIndex > 0)
       {
           System.Data.DataTable dt = new System.Data.DataTable();
           dt = uRole.GetUserRoles(Convert.ToInt32(ListUsers.SelectedItem.Value.ToString()));
           foreach (ListItem item in ListAssigned.Items)
           {
               if (dt.Select("RoleID=" + Convert.ToInt32(item.Value)).Length > 0)
               {
                   item.Selected = true;
               }
               else
               {
                   item.Selected = false;
               }
           }
       }
       else
       {
           ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", "ShowMessage('Error','Please select a user to proceed.')", true);

       }
   }
}