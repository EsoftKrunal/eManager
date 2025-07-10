using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modules_Inspection_Registers_InspectionRegistersHome : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        if (!Page.IsPostBack)
        {
            Session["InspectionRegisters"] = "0";
        }
        btnInsGroup.CssClass = "btn1";
        btnInsReportCat.CssClass = "btn1";
        btnInsSettings.CssClass = "btn1";
        btnInsVersion.CssClass = "btn1";
        btnIntInsChecklist.CssClass = "btn1";
        btnMainChapter.CssClass = "btn1";
        btnSubChapter.CssClass = "btn1";
        btnIns.CssClass = "btn1";
        btnVIMSTools.CssClass = "btn1";
        if (Session["InspectionRegisters"] == null)
        {
            Session["InspectionRegisters"] = 0;
        }
        switch (Common.CastAsInt32(Session["InspectionRegisters"]))
        {
            case 0:
                btnInsGroup.CssClass = "selbtn";
                break;
            case 1:
                btnIns.CssClass = "selbtn";
                break;
            case 2:
                btnMainChapter.CssClass = "selbtn";
                break;
            case 3:
                btnSubChapter.CssClass = "selbtn";
                break;
            case 4:
                btnInsSettings.CssClass = "selbtn";
                break;
            case 5:
                btnInsVersion.CssClass = "selbtn";
                break;
            case 6:
                btnInsReportCat.CssClass = "selbtn";
                break;
            case 7:
                btnIntInsChecklist.CssClass = "selbtn";
                break;
            case 8:
                btnVIMSTools.CssClass = "selbtn";
                break;
            default:
                break;
        }
    }

    protected void btnTabs_Click(object sender, EventArgs e)
    {
        btnInsGroup.CssClass = "btn1";
        btnInsReportCat.CssClass = "btn1";
        btnInsSettings.CssClass = "btn1";
        btnInsVersion.CssClass = "btn1";
        btnIntInsChecklist.CssClass = "btn1";
        btnMainChapter.CssClass = "btn1";
        btnSubChapter.CssClass = "btn1";
        btnIns.CssClass = "btn1";
        btnVIMSTools.CssClass = "btn1";
        Button btn = (Button)sender;
        int i = Common.CastAsInt32(btn.CommandArgument);
        Session["InspectionRegisters"] = i;

        switch (i)
        {
            case 0:
                btnInsGroup.CssClass = "selbtn";
                Session["InspectionRegisters"] = 0;
                frm.Attributes.Add("src", "~/Modules/Inspection/Registers/InspectionGroup.aspx");
                break;
            case 1:
                btnIns.CssClass = "selbtn";
                Session["InspectionRegisters"] = 1;
                frm.Attributes.Add("src", "~/Modules/Inspection/Registers/Inspection.aspx");
                break;
            case 2:
                btnMainChapter.CssClass = "selbtn";
                Session["InspectionRegisters"] = 2;
                frm.Attributes.Add("src", "~/Modules/Inspection/Registers/ChaptersEntry.aspx");
                break;
            case 3:
                btnSubChapter.CssClass = "selbtn";
                Session["InspectionRegisters"] = 3;
                frm.Attributes.Add("src", "~/Modules/Inspection/Registers/SubChapter.aspx");
                break;
            case 4:
                btnInsSettings.CssClass = "selbtn";
                Session["InspectionRegisters"] = 4;
                frm.Attributes.Add("src", "~/Modules/Inspection/Registers/InspectionSettings.aspx");
                break;
            case 5:
                btnInsVersion.CssClass = "selbtn";
                Session["InspectionRegisters"] = 5;
                frm.Attributes.Add("src", "~/Modules/Inspection/Registers/InspVersions.aspx");
                break;
            case 6:
                btnInsReportCat.CssClass = "selbtn";
                Session["InspectionRegisters"] = 6;
                frm.Attributes.Add("src", "~/Modules/Inspection/Registers/InspReportCat.aspx");
                break;
            case 7:
                btnIntInsChecklist.CssClass = "selbtn";
                Session["InspectionRegisters"] = 7;
                frm.Attributes.Add("src", "~/Modules/Inspection/Registers/InternalInspectionsQuestions.aspx");
                break;
            case 8:
                btnVIMSTools.CssClass = "selbtn";
                Session["InspectionRegisters"] = 8;
                frm.Attributes.Add("src", "~/Modules/Inspection/Registers/InspectionChangeStatus.aspx");
                break;
            default:
                break;
        }
    }
}