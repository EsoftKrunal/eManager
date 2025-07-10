using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modules_LPSQE_Transactions_InspectionHome : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        if (!Page.IsPostBack)
        {
            Session["Inspection"] = "0";
        }
        btnInsCloser.CssClass = "btn1";
        btnInsDocs.CssClass = "btn1";
        btnInsExpenses.CssClass = "btn1";
        btnInsPlanning.CssClass = "btn1";
        btnInsResponse.CssClass = "btn1";
        btnInsSearch.CssClass = "btn1";
        btnInsTravelSchedule.CssClass = "btn1";
        if (Session["Inspection"] == null)
        {
            Session["Inspection"] = 0;
        }
        switch (Common.CastAsInt32(Session["Inspection"]))
        {
            case 0:
                btnInsSearch.CssClass = "selbtn";
                break;
            case 1:
                btnInsPlanning.CssClass = "selbtn";
                break;
            case 2:
                btnInsTravelSchedule.CssClass = "selbtn";
                break;
            case 3:
                btnInsResponse.CssClass = "selbtn";
                break;
            case 4:
                btnInsDocs.CssClass = "selbtn";
                break;
            case 5:
                btnInsExpenses.CssClass = "selbtn";
                break;
            case 6:
                btnInsCloser.CssClass = "selbtn";
                break;
            default:
                break;
        }

        
    }

    protected void btnTabs_Click(object sender, EventArgs e)
    {
        btnInsCloser.CssClass = "btn1";
        btnInsDocs.CssClass = "btn1";
        btnInsExpenses.CssClass = "btn1";
        btnInsPlanning.CssClass = "btn1";
        btnInsResponse.CssClass = "btn1";
        btnInsSearch.CssClass = "btn1";
        btnInsTravelSchedule.CssClass = "btn1";

        Button btn = (Button)sender;
        int i = Common.CastAsInt32(btn.CommandArgument);
        Session["Inspection"] = i;

        switch (i)
        {
            case 0:
                btnInsSearch.CssClass = "selbtn";
                Session["Inspection"] = 0;
                frm.Attributes.Add("src", "~/Modules/Inspection/InspectionSearch.aspx");
                break;
            case 1:
                btnInsPlanning.CssClass = "selbtn";
                Session["Inspection"] = 1;
                frm.Attributes.Add("src", "~/Modules/Inspection/InspectionPlanning.aspx");
                break;
            case 2:
                btnInsTravelSchedule.CssClass = "selbtn";
                Session["Inspection"] = 2;
                frm.Attributes.Add("src", "~/Modules/Inspection/InspectionTravelSchedule.aspx");
                break;
            case 3:
                btnInsResponse.CssClass = "selbtn";
                Session["Inspection"] = 3;
                frm.Attributes.Add("src", "~/Modules/Inspection/InspectionResponse.aspx");
                break;
            case 4:
                btnInsDocs.CssClass = "selbtn";
                Session["Inspection"] = 4;
                frm.Attributes.Add("src", "~/Modules/Inspection/InspectionDocuments.aspx");
                break;
            case 5:
                btnInsExpenses.CssClass = "selbtn";
                Session["Inspection"] = 5;
                frm.Attributes.Add("src", "~/Modules/Inspection/InspectionExpenses.aspx");
                break;
            case 6:
                btnInsCloser.CssClass = "selbtn";
                Session["Inspection"] = 6;
                frm.Attributes.Add("src", "~/Modules/Inspection/InspectionCloser.aspx");
                break;
            default:
                break;
        }
    }
}