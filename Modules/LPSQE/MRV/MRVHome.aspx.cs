using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Modules_LPSQE_MRV_MRVHome : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        // SessionManager.SessionCheck_New();
        //-----------------------------
        //Alerts.SetHelp(imgHelp);
        if (!Page.IsPostBack)
        {
            Session["MRV"] = "0";
        }
        btnMRVHome.CssClass = "btn1";
        btnVesselSetUp.CssClass = "btn1";
        btnFuleTypes.CssClass = "btn1";
        btnActivity.CssClass = "btn1";
        btnReport.CssClass = "btn1";
        if (Session["MRV"] == null)
        {
            Session["MRV"] = 0;
        }
        switch (Common.CastAsInt32(Session["MRV"]))
        {
            case 0:
                btnMRVHome.CssClass = "selbtn";
                break;
            case 1:
                btnVesselSetUp.CssClass = "selbtn";
                break;
            case 2:
                btnFuleTypes.CssClass = "selbtn";
                break;
            case 3:
                btnActivity.CssClass = "selbtn";
                break;
            case 4:
                btnReport.CssClass = "selbtn";
                break;
            default:
                break;
        }
        // ((Button)this.FindControl("b" + (Common.CastAsInt32(Session["lasttab"]) + 1).ToString())).CssClass = "activetab";
    }
    protected void Menu_Click(object sender, EventArgs e)
    {
        btnMRVHome.CssClass = "btn1";
        btnVesselSetUp.CssClass = "btn1";
        btnFuleTypes.CssClass = "btn1";
        btnActivity.CssClass = "btn1";
        btnReport.CssClass = "btn1";
        Button btn = (Button)sender;
        int i = Common.CastAsInt32(btn.CommandArgument);
        Session["MRV"] = i;

        switch (i)
        {
            case 0:
                btnMRVHome.CssClass = "selbtn";
                Session["MRV"] = 0;
                frm.Attributes.Add("src", "~/Modules/LPSQE/MRV/Home.aspx");
                break;
            case 1:
                btnVesselSetUp.CssClass = "selbtn";
                Session["MRV"] = 1;
                frm.Attributes.Add("src", "~/Modules/LPSQE/MRV/VesselSetup.aspx");
                break;
            case 2:
                btnFuleTypes.CssClass = "selbtn";
                Session["MRV"] = 2;
                frm.Attributes.Add("src", "~/Modules/LPSQE/MRV/FuelTypes.aspx");
                break;
            case 3:
                btnActivity.CssClass = "selbtn";
                Session["MRV"] = 3;
                frm.Attributes.Add("src", "~/Modules/LPSQE/MRV/Activity.aspx");
                break;
            case 4:
                btnReport.CssClass = "selbtn";
                Session["MRV"] = 4;
                frm.Attributes.Add("src", "~/Modules/LPSQE/MRV/ReportFilter.aspx");
                break;
            default:
                break;
        }
    }
}