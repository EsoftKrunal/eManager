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


public partial class UserControls_FullHeader : System.Web.UI.UserControl
{
    int UserId = 0;
    public bool _HeaderVisible=false;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            UserId = int.Parse(Convert.ToString(Session["UserId"]));
        }
        catch { }
    }
}
