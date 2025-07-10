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

public partial class SiteAdministration_ManageRoles : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["PageRoleRights"] == "1")
        {
            CheckBoxList1.SelectedValue = "1";
        }
        if (Session["PageRoleRights"] == "2")
        {
            CheckBoxList1.SelectedValue = "2";
        }
    }
    protected void CheckBoxList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (CheckBoxList1.SelectedValue == "1")
        {
            Response.Redirect("UserRoleRelation.aspx");
        }
        if (CheckBoxList1.SelectedValue == "2")
        {
            Response.Redirect("UserPageRelation.aspx");
        }
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("AdminDashBoard.aspx");
    }
}
