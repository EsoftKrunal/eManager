using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modules_Inspection_Registers_InsuranceRegistersHome : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        if (!Page.IsPostBack)
        {
            Session["InsuranceRegisters"] = "0";
        }
        btnInsGroup.CssClass = "btn1";
        btnInsSubGroup.CssClass = "btn1";
        btnInsUW.CssClass = "btn1";
        
        if (Session["InsuranceRegisters"] == null)
        {
            Session["InsuranceRegisters"] = 0;
        }
        switch (Common.CastAsInt32(Session["InsuranceRegisters"]))
        {
            case 0:
                btnInsGroup.CssClass = "selbtn";
                break;
            case 1:
                btnInsSubGroup.CssClass = "selbtn";
                break;
            case 2:
                btnInsUW.CssClass = "selbtn";
                break;
            
            default:
                break;
        }
    }

    protected void btnTabs_Click(object sender, EventArgs e)
    {
        btnInsGroup.CssClass = "btn1";
        btnInsSubGroup.CssClass = "btn1";
        btnInsUW.CssClass = "btn1";

        LinkButton btn = (LinkButton)sender;
        int i = Common.CastAsInt32(btn.CommandArgument);
        Session["InsuranceRegisters"] = i;

        switch (i)
        {
            case 0:
                btnInsGroup.CssClass = "selbtn";
                Session["InsuranceRegisters"] = 0;
                frm.Attributes.Add("src", "~/Modules/Inspection/Registers/IRM_GroupMaster.aspx");
                break;
            case 1:
                btnInsSubGroup.CssClass = "selbtn";
                Session["InsuranceRegisters"] = 1;
                frm.Attributes.Add("src", "~/Modules/Inspection/Registers/IRM_SubGroupMaster.aspx");
                break;
            case 2:
                btnInsUW.CssClass = "selbtn";
                Session["InsuranceRegisters"] = 2;
                frm.Attributes.Add("src", "~/Modules/Inspection/Registers/IRM_UnderWriterMaster.aspx");
                break;
           
            default:
                break;
        }
    }
}