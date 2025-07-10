using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modules_LPSQE_Procedures_PB_PublicationCommunicationHome_aspx : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        int UserId = 0;
        UserId = int.Parse(Session["loginid"].ToString());

        //btnPublication.CssClass = "menu_2";
        //btnRegister.CssClass = "menu_2";
        //btnLocation.CssClass = "menu_2";
        //btnCommuniction.CssClass = "menu_2";

        string Main = Convert.ToString(Session["MM"]).Trim();

        //if (Main == "P")
        //{
        //    btnPublication.CssClass = "selmenu_2";
        //}
        //else if (Main == "R")
        //{
        //    btnRegister.CssClass = "selmenu_2";
        //}
        //else if (Main == "L")
        //{
        //    btnLocation.CssClass = "selmenu_2";
        //}
        //else if (Main == "C")
        //{
        //    btnCommuniction.CssClass = "selmenu_2";
        //}

    }
    protected void btnPublication_OnClick(object sender, EventArgs e)
    {
        Session["MM"] = "P";
        Session["MS"] = "P";
        // Response.Redirect("PB_Publication.aspx");
        frm.Attributes.Add("src", "~/Modules/LPSQE/Procedures/PB_Publication.aspx");
    }
    protected void btnRegister_OnClick(object sender, EventArgs e)
    {
        Session["MM"] = "R";
        Session["MS"] = "P";
        // Response.Redirect("PB_PublicationType.aspx");
        frm.Attributes.Add("src", "~/Modules/LPSQE/Procedures/PB_PublicationType.aspx");
    }
    protected void btnLocation_OnClick(object sender, EventArgs e)
    {
        Session["MM"] = "L";
        Session["MS"] = "P";
        //Response.Redirect("PB_LocationCustody.aspx");
        frm.Attributes.Add("src", "~/Modules/LPSQE/Procedures/PB_LocationCustody.aspx");
    }
    protected void btnCommuniction_OnClick(object sender, EventArgs e)
    {
        Session["MM"] = "C";
        Session["MS"] = "P";
        //Response.Redirect("PB_PublicationCommunication.aspx");
        frm.Attributes.Add("src", "~/Modules/LPSQE/Procedures/PB_PublicationCommunication.aspx");
    }
}