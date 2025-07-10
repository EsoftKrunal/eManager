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

public partial class UserControls_ModuleMenu : System.Web.UI.UserControl
{
    int UserId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
        UserId = int.Parse(Session["UserId"].ToString());
        }
        catch { }

        btn_CMS.Visible = new EAuthenticationManager(1, UserId, ObjectType.Application).IsView;
        btn_VIMS.Visible = new EAuthenticationManager(2, UserId, ObjectType.Application).IsView;
        btn_POS.Visible = new EAuthenticationManager(3, UserId, ObjectType.Application).IsView;
        btn_PMS.Visible = new EAuthenticationManager(4, UserId, ObjectType.Application).IsView;
        btn_ADMIN.Visible = new EAuthenticationManager(1, UserId, ObjectType.Application).IsSuperUser;
    }
}
