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

public partial class SiteAdministration_ManageRights : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["PageCodeRights"] == "1")
        {
            CheckBoxList1.SelectedValue = "1";
        }
        if (Session["PageCodeRights"] == "2")
        {
            CheckBoxList1.SelectedValue = "2";
        }
    }
    protected void CheckBoxList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (CheckBoxList1.SelectedValue == "1")
        {
            Response.Redirect("UserRightsRoles.aspx");
        }
        if (CheckBoxList1.SelectedValue == "2")
        {
            Response.Redirect("UserRights.aspx");
        }
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("AdminDashBoard.aspx");
    }
}
