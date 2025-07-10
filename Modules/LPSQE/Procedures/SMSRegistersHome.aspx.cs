using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modules_LPSQE_Procedures_SMSRegistersHome : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        int UserId = 0;
        UserId = int.Parse(Session["loginid"].ToString());

        btnManuals.Visible = new AuthenticationManager(1058, UserId, ObjectType.Page).IsView;
       // btnForms.Visible = new AuthenticationManager(304, UserId, ObjectType.Page).IsView;


        //btnManuals.CssClass = "tab";
        //btnForms.CssClass = "tab";
        //btnManCat.CssClass = "tab";

        string Main = Convert.ToString(Session["MS"]).Trim();
        if (Main == "M")
        {
           // btnManuals.CssClass = "activetab";
        }
        else if (Main == "F")
        {
            //btnForms.CssClass = "activetab";
        }
        else if (Main == "A")
        {
           // btnManCat.CssClass = "activetab";
        }
        else
        {
            Session["MM"] = "R";
            Session["MS"] = "M";
           // btnManuals.CssClass = "activetab";
        }
    }

    protected void btnManuals_Click(object sender, EventArgs e)
    {
        Session["MM"] = "R";
        Session["MS"] = "M";
        // Response.Redirect("SMS_ManualMaster.aspx");
        frm.Attributes.Add("src", "~/Modules/LPSQE/Procedures/SMS_ManualMaster.aspx");
    }
    
    protected void btnManualCat_Click(object sender, EventArgs e)
    {
        Session["MM"] = "R";
        Session["MS"] = "A";
        //Response.Redirect("SMS_ManualCat.aspx");
        frm.Attributes.Add("src", "~/Modules/LPSQE/Procedures/SMS_ManualCat.aspx");
    }

    protected void btnFormDepartment_Click(object sender, EventArgs e)
    {
        frm.Attributes.Add("src", "~/Modules/LPSQE/Procedures/SMS_FormDepartment.aspx");
    }

    protected void btnFormCategory_Click(object sender, EventArgs e)
    {
        frm.Attributes.Add("src", "~/Modules/LPSQE/Procedures/SMS_FormCategory.aspx");
    }
}