using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VIMS_VIMSMenu : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        btnVIQ.CssClass = "newbtn";
        btn_Inspections.CssClass = "newbtn";
        if (!IsPostBack)
        {
            switch (Common.CastAsInt32(Session["CP"]))
            {
                case 0:
                    btn_Inspections.CssClass = "newbtnsel";
                    break;
                case 1:
                    btnVIQ.CssClass = "newbtnsel";                     
                    break;
                case 2:
                    btnLFI.CssClass = "newbtnsel";                     
                    break;
                case 3:
                    btnFC.CssClass = "newbtnsel";                     
                    break;
                case 4:
                    btnRegulation.CssClass = "newbtnsel";                     
                    break;
                case 5:
                    btnCircular.CssClass = "newbtnsel";                     
                    break;
                case 6:
                    btnNavigationAudit.CssClass = "newbtnsel";                     
                    break;
                case 7:
                    btnPR.CssClass = "newbtnsel";                   
                    break;
                case 8:
                    btnMWUC.CssClass = "newbtnsel";
                    break;
                case 9:
                    btnSCM.CssClass = "newbtnsel";
                    break;
                case 10:
                    btnRisk.CssClass = "newbtnsel";
                    break;
            }
        }
    }
    protected void Menu_Click(object sender, EventArgs e)
    {
        int cArg = Common.CastAsInt32(((Button)sender).CommandArgument);
        Session["CP"] = cArg;
        switch (cArg)
        {
            case 0:
                Response.Redirect("VimsInspections.aspx");
                break;
            case 1:
                Response.Redirect("ViQPreparation.aspx");
                break;
            case 2:
                Response.Redirect("LFI.aspx");
                break;
            case 3:
                Response.Redirect("FC.aspx");
                break;
            case 4:
                Response.Redirect("Regulation.aspx");
                break;
            case 5:
                Response.Redirect("Circular.aspx");
                break;
            case 6:
                Response.Redirect("VNAHome.aspx");
                break;
            case 7:
                Response.Redirect("positionreport.aspx");
                break;
            case 8:
                Response.Redirect("MWUCList.aspx");
                break;
            case 9:
                Response.Redirect("SCMLIST.aspx");
                break;
            case 10:
                Response.Redirect("RiskManagement.aspx");
                break;
        }

    }
}