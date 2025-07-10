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
public partial class ChangePassword : System.Web.UI.Page
{
    Authority Auth;
    string Mode;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
    }
    protected void btn_Submit_Click(object sender, EventArgs e)
    {
        int lid;
        lid = Convert.ToInt32(Session["loginid"].ToString());
        lid=UserLogin.UpdatePassword(lid, txt_ConfirmPassword.Text.Trim(), txt_OldPassword.Text.Trim());
        if (lid == 0)
        {
            lblMessage.Text="Old Password is Not Correct." ;
        }
        else
        {
            lblMessage.Text = "Password successfully Changed.";     
        }
    }
    protected void btn_Login_Click(object sender, EventArgs e)
    {
        Response.Redirect("default.aspx");  
    }
}
