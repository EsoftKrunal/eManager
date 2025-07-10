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
public partial class Home : System.Web.UI.Page
{
    int UserId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] == null)
        {
            Response.Redirect("Login.aspx");
        }
        UserId = int.Parse(Convert.ToString(Session["UserId"]));
    }
    protected void Page_PreRender()
    {
        switch (header1.CurrentModule)
        {
            case 10:
                frmApp.Attributes.Add("src", ConfigurationManager.AppSettings["CMSPath"].ToString() + "?u=" + Session["UserName"].ToString() + "&p=" + Session["Password"].ToString() + "&emtm1=1");
                break;
            case 11:
                frmApp.Attributes.Add("src", "DashboardNew.aspx");
                break;
            case 1:
                if (new AuthenticationManager(1, UserId, ObjectType.Application).IsView)
                    frmApp.Attributes.Add("src", ConfigurationManager.AppSettings["CMSPath"].ToString() + "?u=" + Session["UserName"].ToString() + "&p=" + Session["Password"].ToString());
                else
                    frmApp.Attributes.Add("src", "Unauthorized.aspx");
                break;
            case 2:
                if (new AuthenticationManager(2, UserId, ObjectType.Application).IsView)
                    frmApp.Attributes.Add("src", ConfigurationManager.AppSettings["VIMSPath"].ToString() + "?u=" + Session["UserName"].ToString() + "&p=" + Session["Password"].ToString());
                else
                    frmApp.Attributes.Add("src", "Unauthorized.aspx");
                break;
            case 3:
                if (new AuthenticationManager(6, UserId, ObjectType.Application).IsView)
                    frmApp.Attributes.Add("src", ConfigurationManager.AppSettings["PMSPath"].ToString() + "?u=" + Session["UserName"].ToString() + "&p=" + Session["Password"].ToString());
                else
                    frmApp.Attributes.Add("src", "Unauthorized.aspx");
                break;
            case 4:
                if (new AuthenticationManager(3, UserId, ObjectType.Application).IsView)
                    frmApp.Attributes.Add("src", ConfigurationManager.AppSettings["POSPath"].ToString() + "?u=" + Session["UserName"].ToString() + "&p=" + Session["Password"].ToString());
                else
                    frmApp.Attributes.Add("src", "Unauthorized.aspx");
                break;
            case 5:
                if (UserId == 1)
                    frmApp.Attributes.Add("src", "Administration.aspx");
                else
                    frmApp.Attributes.Add("src", "Unauthorized.aspx");
                break;
            case 6:
                if (new AuthenticationManager(3, UserId, ObjectType.Application).IsView)
                    frmApp.Attributes.Add("src", ConfigurationManager.AppSettings["POSPath"].ToString() + "?u=" + Session["UserName"].ToString() + "&p=" + Session["Password"].ToString() + "&budget=1");
                else
                    frmApp.Attributes.Add("src", "Unauthorized.aspx");
                break;
            case 7:
                frmApp.Attributes.Add("src", ConfigurationManager.AppSettings["CMSPath"].ToString() + "?u=" + Session["UserName"].ToString() + "&p=" + Session["Password"].ToString() + "&emtm=1");
                break;
            case 8:
                if (new AuthenticationManager(2, UserId, ObjectType.Application).IsView)
                    frmApp.Attributes.Add("src", ConfigurationManager.AppSettings["HSSQEPath"].ToString() + "?u=" + Session["UserName"].ToString() + "&p=" + Session["Password"].ToString() + "&HSSQE=1");
                else
                    frmApp.Attributes.Add("src", "Unauthorized.aspx");
                break;
            case 99:
                frmApp.Attributes.Add("src", "ChangePassword.aspx");
                break;
            default:
                frmApp.Attributes.Add("src", "Alerts.aspx");
                break;
        }
        frmApp.Attributes.Add("onload", "return frm_onload(frmApp)");
    }
}