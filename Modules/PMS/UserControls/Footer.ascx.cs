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


public partial class PMS_UserControls_Footer : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LinkButton li = new LinkButton();
        footer.Style.Add("background-Image", "url(" + li.ResolveClientUrl("~/Images/footer_bg.jpg") + ")");

        aComm.Visible = (Session["UserName"] != null);

        if (Session["UserType"] != null)
        {
            footer.Visible = (Session["UserType"].ToString().Trim() == "S");
        }
    }
}
