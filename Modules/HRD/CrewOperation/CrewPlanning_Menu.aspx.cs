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

public partial class CrewOperation_CrewPlanning_Menu : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        //Alerts.SetHelp(imgHelp);
        if (!Page.IsPostBack)
        {
            Session["lasttab"] = "0";
        }
        b1.CssClass = "btn1";
        b2.CssClass = "btn1";
        b3.CssClass = "btn1";
        b4.CssClass = "btn1";
        b5.CssClass = "btn1";
        b6.CssClass = "btn1";
        if (Session["lasttab"] == null)
        {
            Session["lasttab"] = 3;
        }
        switch (Common.CastAsInt32(Session["lasttab"]))
        {
            case 0:
                b1.CssClass = "selbtn";
                break;
            case 1:
                b2.CssClass = "selbtn";
                break;
            case 2:
                b3.CssClass = "selbtn";
                break;
            case 3:
                b4.CssClass = "selbtn";
                break;
            case 4:
                b5.CssClass = "selbtn";
                break;
            case 5:
                b6.CssClass = "selbtn";
                break;
            default:
                break;
        }
        // ((Button)this.FindControl("b" + (Common.CastAsInt32(Session["lasttab"]) + 1).ToString())).CssClass = "activetab";
    }
    protected void Menu_Click(object sender, EventArgs e)
    {
        b1.CssClass = "btn1";
        b2.CssClass = "btn1";
        b3.CssClass = "btn1";
        b4.CssClass = "btn1";
        b5.CssClass = "btn1";
        b6.CssClass = "btn1";
        Button btn = (Button)sender;
        int i = Common.CastAsInt32(btn.CommandArgument);
        Session["lasttab"] = i;
        //if (i == 0)
        // {
        //     b1.CssClass = "selbtn";
        //     Response.Redirect("~/Modules/HRD/CrewOperation/GPT.aspx");  
        // }
        // else if (i == 1)
        // {
        //     b2.CssClass = "selbtn";
        //     Response.Redirect("~/Modules/HRD/CrewOperation/CrewChange.aspx");
        // }
        // else if (i == 2)
        // {
        //     b3.CssClass = "selbtn";
        //     Response.Redirect("~/Modules/HRD/CrewOperation/CrewTravel.aspx");
        // }
        // else if (i == 3)
        // {
        //     b4.CssClass = "selbtn";
        //     Response.Redirect("~/Modules/HRD/CrewOperation/CrewPlanning.aspx");
        // }
        // else if (i == 4)
        // {
        //     b5.CssClass = "selbtn";
        //     Response.Redirect("~/Modules/HRD/CrewOperation/VesselPlanning.aspx");
        // }
        switch (i)
        {
            case 0:
                b1.CssClass = "selbtn";
                Session["lasttab"] = 0;
                Response.Redirect("~/Modules/HRD/CrewOperation/CrewPlanning_Menu.aspx");
                break;
            case 1:
                Session["lasttab"] = 1;
                Response.Redirect("~/Modules/HRD/CrewOperation/CrewChange.aspx");
                break;
            case 2:
                Session["lasttab"] = 2;
                Response.Redirect("~/Modules/HRD/CrewOperation/CrewTravel.aspx");
                break;
            case 3:
                b4.CssClass = "selbtn";
                Session["lasttab"] = 3;
                frm.Attributes.Add("src", "~/Modules/HRD/CrewOperation/CrewPlanning.aspx");
                //Response.Redirect("~/Modules/HRD/CrewOperation/CrewPlanning.aspx");
                break;
            case 4:
                b5.CssClass = "selbtn";
                Session["lasttab"] = 4;
                frm.Attributes.Add("src", "~/Modules/HRD/CrewOperation/VesselPlanning.aspx");
               // Response.Redirect("~/Modules/HRD/CrewOperation/VesselPlanning.aspx");
                break;
            case 5:
                b6.CssClass = "selbtn";
                Session["lasttab"] = 5;
                frm.Attributes.Add("src", "~/Modules/HRD/CRMActivities/CRM_CrewCommunication.aspx");
                // Response.Redirect("~/Modules/HRD/CrewOperation/VesselPlanning.aspx");
                break;
            default:
                break;
        }
    }
}
