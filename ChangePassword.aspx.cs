using System;
using System.Collections;
using System.Configuration;
using System.Data;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class ChnagePassword : System.Web.UI.Page
{
    int UserId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] == null)
        {
            Response.Redirect("Login.aspx");
        }
        Session["Home Page"] = "Home Page";
        UserId = int.Parse(Session["UserId"].ToString());
        txt_New.Attributes.Add("value",txt_New.Text);     
        txt_Confirm.Attributes.Add("value",txt_Confirm.Text);     
    }
    protected void btnPost_Click(object sender, EventArgs e)
    {
        if (txt_New.Text.Contains("#") || txt_New.Text.Contains("?") || txt_New.Text.Contains(" ") || txt_New.Text.Contains("&"))
        {
            Msgbox.ShowMessage("Please enter a valid password (4-15) Chars. Password does not allow (# , ? , & , )", true);
            txt_New.Focus();
            return;
        }

        if (!(Validator.ValidatePassword(txt_New.Text)))
        {
            Msgbox.ShowMessage("Invalid Password length must in between(4-15).", true);
            txt_New.Focus();
            return; 
        }
        if (txt_Confirm.Text.Trim() != txt_New.Text.Trim())
        {
            Msgbox.ShowMessage("Old password & conform password are not matching.", true);
            txt_Confirm.Focus();
            return;
        }
        try
        {
            Common.Execute_Procedures_Select_ByQuery("Update UserMaster set Password='" + ProjectCommon.Encrypt(txt_New.Text.Trim(), "qwerty1235") + "' Where LoginId=" + UserId.ToString());
            Session["Password"] = txt_New.Text.Trim() ;
            Session["Pwd"] = txt_New.Text.Trim();
            Session.Abandon();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "re", "alert('Password changed successfully. Please login again');window.location = 'login.aspx';", true);
            //Response.Redirect("login.aspx");
        }
        catch
        {
            Msgbox.ShowMessage("Unable to update password.", true);
        }
    }
}

