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

public partial class UserControls_Left : System.Web.UI.UserControl
{
    public string ImagesUrl = "";
    protected void Page_Load(object sender, EventArgs e)
    {
       // ImagesUrl = ConfigurationManager.AppSettings["ImagesUrl"].ToString();
        if (Session["loginid"] == null || "" + Session["loginid"] == "")
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "LogOut();", true);
            //Response.Redirect("~/Default.aspx");
        }
        int UserId = Common.CastAsInt32("" + Session["loginid"]);

        TrPurchaseRequest.Visible = new AuthenticationManager(15, UserId, ObjectType.Module).IsView && true;
        TrMasters.Visible = new AuthenticationManager(19, UserId, ObjectType.Module).IsView && true;
        TrVendors.Visible = new AuthenticationManager(18, UserId, ObjectType.Module).IsView && true;
        TrRFQs.Visible = new AuthenticationManager(17, UserId, ObjectType.Module).IsView && true;
        TrPOs.Visible = new AuthenticationManager(16, UserId, ObjectType.Module).IsView && true;
        TrInvs.Visible = new AuthenticationManager(18, UserId, ObjectType.Module).IsView && true;
        TrReports.Visible = new AuthenticationManager(20, UserId, ObjectType.Module).IsView && true;
        ApEntries.Visible = new AuthenticationManager(45, UserId, ObjectType.Module).IsView && true;
       
    }
}
