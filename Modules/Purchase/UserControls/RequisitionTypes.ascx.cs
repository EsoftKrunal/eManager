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

public partial class UserControls_RequisitionTypes : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["FormSelection"] == "1")
        {
            rdbReqTypes.SelectedValue = "1";
        }
        if (Session["FormSelection"] == "2")
        {
            rdbReqTypes.SelectedValue = "2";
        }
        if (Session["FormSelection"] == "3")
        {
            rdbReqTypes.SelectedValue = "3";
        }
    }
    protected void rdbReqTypes_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdbReqTypes.SelectedValue == "2")
        {
            Response.Redirect("~/AddRequisitionThroughSheet.aspx");
        }
        if (rdbReqTypes.SelectedValue == "1")
        {
            Response.Redirect("~/AddStoresReqSheet.aspx");
        }
        if (rdbReqTypes.SelectedValue == "3")
        {
            Response.Redirect("~/AddServicesReqSheet.aspx");
        }
    }
}
