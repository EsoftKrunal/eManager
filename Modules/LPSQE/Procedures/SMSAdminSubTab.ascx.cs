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

public partial class UserControls_SMSAdminSubTab : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int UserId = 0;
        UserId = int.Parse(Session["loginid"].ToString());

        btnSMSMGMT.Visible =  (new AuthenticationManager(6, UserId, ObjectType.Module).IsView && new AuthenticationManager(1057, UserId, ObjectType.Page).IsView);
        //btnAPPROVAL.Visible = (new AuthenticationManager(42, UserId, ObjectType.Module).IsView && new AuthenticationManager(301, UserId, ObjectType.Page).IsView);
        //btnComm.Visible =     (new AuthenticationManager(42, UserId, ObjectType.Module).IsView && new AuthenticationManager(302, UserId, ObjectType.Page).IsView);
        //btnRegister.Visible = (new AuthenticationManager(42, UserId, ObjectType.Module).IsView && (new AuthenticationManager(303, UserId, ObjectType.Page).IsView || new AuthenticationManager(304, UserId, ObjectType.Page).IsView));
        // ?? Authentication
        btnForms.Visible = (new AuthenticationManager(6, UserId, ObjectType.Module).IsView && new AuthenticationManager(1057, UserId, ObjectType.Page).IsView);
        btnQuetionair.Visible = (new AuthenticationManager(6, UserId, ObjectType.Module).IsView && new AuthenticationManager(1057, UserId, ObjectType.Page).IsView);

        //btnAPPROVAL.CssClass = "btn1";
        //btnComm.CssClass = "btn1";
        btnRegister.CssClass = "btn1";
        btnSMSMGMT.CssClass = "btn1";
        btnQuetionair.CssClass = "btn1";
        btnForms.CssClass = "btn1";

        string Main = Convert.ToString(Session["MM"]).Trim();
        if (Main == "S")
        {
            btnSMSMGMT.CssClass = "selbtn";
        }
        
        else if (Main == "R")
        {
            //btnRegister.CssClass = "selbtn";
            btnForms.CssClass = "selbtn";
        }
        else if (Main == "Q")
        {
            btnQuetionair.CssClass = "selbtn";
        }
        else
        {
            btnSMSMGMT.CssClass = "selbtn";
        }
    }

    protected void btnSMSMGMT_OnClick(object sender, EventArgs e)
    {
        Session["MM"] = "S";
        Session["MS"] = "M";
        Response.Redirect("ViewManualHeadings.aspx");
    }
   
    protected void btnRegister_OnClick(object sender, EventArgs e)
    {
        Session["MM"] = "R";
        Session["MS"] = "M";
        Response.Redirect("SMS_ManualMaster.aspx");
    }
    protected void btnQuetionair_OnClick(object sender, EventArgs e)
    {
        Session["MM"] = "Q";
        Session["MS"] = "M";
        Response.Redirect("SMS_AssessmentQuestion.aspx");
    }

    protected void btnForms_Click(object sender, EventArgs e)
    {
        Session["MM"] = "R";
        Session["MS"] = "M";
        Response.Redirect("AddForms.aspx");
    }

}
