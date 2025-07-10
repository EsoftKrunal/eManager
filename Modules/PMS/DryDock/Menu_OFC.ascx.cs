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
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["reset"] == "1")
                Session["EVPageId"] = "DD_OFC_Home";
        }
        ShowActive();
    }
    protected void ShowActive()
    {
        string Mode = Session["DDPageId"].ToString();
        HtmlAnchor[] btns = { btnHome, btnJobMaster, btnDocket,btnDDPlanSettings};
        foreach (HtmlAnchor anc in btns)
        {
            anc.Attributes.Add("class", "");
            string newMode = anc.Attributes["ModuleId"].ToString();
            if (Mode == newMode)
            {
                anc.Attributes.Add("class", "active");
            }
        }
    }
    public void RedirectToPage(object sender, EventArgs e)
    {
        string Mode = ((System.Web.UI.HtmlControls.HtmlAnchor)(sender)).Attributes["ModuleId"].ToString();
        Session["DDPageId"] = Mode;
        Response.Redirect(Mode + ".aspx");
    }
}
