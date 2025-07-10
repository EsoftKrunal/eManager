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
        int LoginId = 0;
        if (Session["loginid"] == null)
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.close();", true);
        }
        else
        {
            LoginId = Convert.ToInt32(Session["loginid"].ToString());
        }

        if (!IsPostBack)
        {
            string path = HttpContext.Current.Request.Url.AbsolutePath;
            if (path.Contains("RiskAnalysis"))
            {
                Session["EVPageId"] = "RiskAnalysis";
            }    
        }
        btnExport.CssClass = "btn1";
        btnHazardsMaster.CssClass = "btn1";
        btnRiskMangement.CssClass = "btn1";
        btnTemplateMaster.CssClass = "btn1";

        string Main = Convert.ToString(Session["RM"]).Trim();
        if (Main == "E")
        {
            btnExport.CssClass = "selbtn";
        }
        //else if (Main == "S")
        //{
        //    btnSMS.CssClass = "selbtn";
        //}
        else if (Main == "H")
        {
            btnHazardsMaster.CssClass = "selbtn";
        }
        else if (Main == "R")
        {
            btnRiskMangement.CssClass = "selbtn";
        }
        else if (Main == "T")
        {
            btnTemplateMaster.CssClass = "selbtn";
        }
       
        //ShowActive();
    }
    //protected void ShowActive()
    //{
    //    string Mode = Session["EVPageId"].ToString();
    //    HtmlAnchor[] btns = { btnHazardsMaster, btnRiskMangement, btnExport, btnTemplateMaster };
    //    foreach (HtmlAnchor anc in btns)
    //    {
    //        anc.Attributes.Add("class", "btn1");
    //        string newMode = anc.Attributes["ModuleId"].ToString();
    //        if (Mode == newMode)
    //        {
    //            anc.Attributes.Add("class", "selbtn");
    //        }
    //    }
    //}
    public void RedirectToPage(object sender,EventArgs e)
    {
        string Mode = ((System.Web.UI.HtmlControls.HtmlAnchor)(sender)).Attributes["ModuleId"].ToString();
        Session["EVPageId"] = Mode;
        Response.Redirect(Mode + ".aspx");
    }

    protected void btnRiskMangement_Click(object sender, EventArgs e)
    {
        btnExport.CssClass = "btn1";
        btnHazardsMaster.CssClass = "btn1";
        btnRiskMangement.CssClass = "selbtn";
        btnTemplateMaster.CssClass = "btn1";
        Response.Redirect("RiskAssessment.aspx");
    }

    protected void btnTemplateMaster_Click(object sender, EventArgs e)
    {
        btnExport.CssClass = "btn1";
        btnHazardsMaster.CssClass = "btn1";
        btnRiskMangement.CssClass = "btn1";
        btnTemplateMaster.CssClass = "selbtn";
        Response.Redirect("TemplateMaster.aspx");
    }

    protected void btnHazardsMaster_Click(object sender, EventArgs e)
    {
        btnExport.CssClass = "btn1";
        btnHazardsMaster.CssClass = "selbtn";
        btnRiskMangement.CssClass = "btn1";
        btnTemplateMaster.CssClass = "btn1";
        Response.Redirect("HazardMaster.aspx");
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        btnExport.CssClass = "selbtn";
        btnHazardsMaster.CssClass = "btn1";
        btnRiskMangement.CssClass = "btn1";
        btnTemplateMaster.CssClass = "btn1";
        Response.Redirect("Export.aspx");
    }
}
