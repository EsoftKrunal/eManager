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

public partial class HSSQE_MOC_MocMenu : System.Web.UI.UserControl
{
    public string cUrl = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        cUrl = (new LinkButton()).ResolveClientUrl("~/Images/");
        int UserId = 0;
        UserId = int.Parse(Session["loginid"].ToString()); 
        //btnMocRequest.Visible = new AuthenticationManager(294, UserId, ObjectType.Page).IsView;
        

        set_Color();
    }
    protected void set_Color()
    {
        Button[] btns = { btnMocRequest, btnMocRequestNew};
        foreach (Button b in btns)
        {
            if (Request.Url.ToString().EndsWith(b.CommandArgument))
            {
                b.CssClass = "color_tab_sel";
            }
        }
    }
    protected void btn_Tab_Click(object sender, EventArgs e)
    {
        btnMocRequest.CssClass = "color_tab";
        //btnSCM.CssClass = "color_tab";
        Response.Redirect(((Button)sender).CommandArgument, true);
    }
}