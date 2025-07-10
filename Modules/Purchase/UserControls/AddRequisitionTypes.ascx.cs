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

public partial class UserControls_AddRequisitionTypes : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["FormSelection"] == "5") // STORES
        {
            rdbAddReqTypes.SelectedValue = "1";
        }
        if (Session["FormSelection"] == "4") // SPARES
        {
            rdbAddReqTypes.SelectedValue = "2";
        }
        if (Session["FormSelection"] == "6") // SERVICES
        {
            rdbAddReqTypes.SelectedValue = "3";
        }
    }
    protected void rdbAddReqTypes_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdbAddReqTypes.SelectedValue == "2")
        {
            Response.Redirect("~/AddRequisition.aspx");
        }
        if (rdbAddReqTypes.SelectedValue == "1")
        {
            Response.Redirect("~/AddStoresRequisition.aspx");
        }
        if (rdbAddReqTypes.SelectedValue == "3")
        {
            Response.Redirect("~/AddServiceRequisition.aspx");
        }
    }
}
