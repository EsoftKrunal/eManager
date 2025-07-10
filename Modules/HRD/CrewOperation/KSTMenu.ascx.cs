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

public partial class KSTMenu : System.Web.UI.UserControl
{
    public string cUrl = ""; 
    protected void Page_Load(object sender, EventArgs e)
    {
        cUrl = (new LinkButton()).ResolveClientUrl("~/Modules/HRD/Images/");  
        int UserId = 0;
        UserId = int.Parse(Session["loginid"].ToString());
        set_Color();
    }
    protected void set_Color()
    {
        Button[] btns = { btnHome, btnClassification, btnbehaviours, btnmtmvalue };
        //Button[] btns = { btnHome, btnMatrix, btnTrainings, btnDrills, btnDrillPlanning };
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
        btnHome.CssClass = "color_tab";
        btnClassification.CssClass = "color_tab";
        btnbehaviours.CssClass = "color_tab";
        btnmtmvalue.CssClass = "color_tab";

        Response.Redirect(ResolveClientUrl(((Button)sender).CommandArgument), true);
    }
}
