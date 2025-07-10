using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modules_LPSQE_Procedures_SMSAdminHome : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int UserId = 0;
        UserId = int.Parse(Session["loginid"].ToString());

        btnSMSMGMT.Visible = (new AuthenticationManager(6, UserId, ObjectType.Module).IsView && new AuthenticationManager(1057, UserId, ObjectType.Page).IsView);
        btnAPPROVAL.Visible = (new AuthenticationManager(6, UserId, ObjectType.Module).IsView && new AuthenticationManager(1057, UserId, ObjectType.Page).IsView);
        btnComm.Visible = (new AuthenticationManager(6, UserId, ObjectType.Module).IsView && new AuthenticationManager(1057, UserId, ObjectType.Page).IsView);
        //btnRegister.Visible = (new AuthenticationManager(42, UserId, ObjectType.Module).IsView && (new AuthenticationManager(303, UserId, ObjectType.Page).IsView || new AuthenticationManager(304, UserId, ObjectType.Page).IsView));
        // ?? Authentication
        btnQuetionair.Visible = (new AuthenticationManager(6, UserId, ObjectType.Module).IsView && new AuthenticationManager(1057, UserId, ObjectType.Page).IsView);

        //btnAPPROVAL.CssClass = "menu_2";
        //btnComm.CssClass = "menu_2";
        //btnRegister.CssClass = "menu_2";
        //btnSMSMGMT.CssClass = "menu_2";
        //btnQuetionair.CssClass = "menu_2";

        //string Main = Convert.ToString(Session["MM"]).Trim();
        //if (Main == "S")
        //{
        //    btnSMSMGMT.CssClass = "selmenu_2";
        //}
        //else if (Main == "A")
        //{
        //    btnAPPROVAL.CssClass = "selmenu_2";
        //}
        //else if (Main == "C")
        //{
        //    btnComm.CssClass = "selmenu_2";
        //}
        //else if (Main == "R")
        //{
        //    btnRegister.CssClass = "selmenu_2";
        //}
        //else if (Main == "Q")
        //{
        //    btnQuetionair.CssClass = "selmenu_2";
        //}
        //else
        //{
        //    btnSMSMGMT.CssClass = "selmenu_2";
        //}
    }

    protected void btnSMSMGMT_OnClick(object sender, EventArgs e)
    {
        Session["MM"] = "S";
        Session["MS"] = "M";
        //Response.Redirect("ViewManualHeadings.aspx");
        frm.Attributes.Add("src", "~/Modules/LPSQE/Procedures/ViewManualHeadings.aspx");
    }
    protected void btnAPPROVAL_OnClick(object sender, EventArgs e)
    {
        Session["MM"] = "A";
        Session["MS"] = "M";
        //Response.Redirect("Approval.aspx");
        frm.Attributes.Add("src", "~/Modules/LPSQE/Procedures/Approval.aspx");
    }
    protected void btnComm_OnClick(object sender, EventArgs e)
    {
        Session["MM"] = "C";
        Session["MS"] = "M";
        //Response.Redirect("Communication.aspx");
        frm.Attributes.Add("src", "~/Modules/LPSQE/Procedures/Communication.aspx");
    }
    protected void btnRegister_OnClick(object sender, EventArgs e)
    {
        Session["MM"] = "R";
        Session["MS"] = "M";
        //Response.Redirect("SMS_ManualMaster.aspx");
        frm.Attributes.Add("src", "~/Modules/LPSQE/Procedures/SMS_ManualMaster.aspx");
    }
    protected void btnQuetionair_OnClick(object sender, EventArgs e)
    {
        Session["MM"] = "Q";
        Session["MS"] = "M";
        //Response.Redirect("SMS_AssessmentQuestion.aspx");
        frm.Attributes.Add("src", "~/Modules/LPSQE/Procedures/SMS_AssessmentQuestion.aspx");
    }
}