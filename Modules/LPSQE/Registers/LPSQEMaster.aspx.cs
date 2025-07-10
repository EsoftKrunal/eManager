using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modules_LPSQE_Registers_LPSQEMaster : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        if (!Page.IsPostBack)
        {
            Session["LPSQEMasters"] = "0";
        }
        btnVSLCertificate.CssClass = "btn1";
        btnVSLReportGrp.CssClass = "btn1";
       
        if (Session["LPSQEMasters"] == null)
        {
            Session["LPSQEMasters"] = 0;
        }
        switch (Common.CastAsInt32(Session["LPSQEMasters"]))
        {
            case 0:
                btnVSLCertificate.CssClass = "selbtn";
                break;
            case 1:
                btnVSLReportGrp.CssClass = "selbtn";
                break;
            case 2:
                btnVSLReport.CssClass = "selbtn";
                break;
            default:
                break;
        }
    }

    protected void btnTabs_Click(object sender, EventArgs e)
    {
        btnVSLCertificate.CssClass = "btn1";
        btnVSLReportGrp.CssClass = "btn1";
        btnVSLReport.CssClass = "btn1";
        Button btn = (Button)sender;
        int i = Common.CastAsInt32(btn.CommandArgument);
        Session["LPSQEMasters"] = i;

        switch (i)
        {
            case 0:
                btnVSLCertificate.CssClass = "selbtn";
                Session["LPSQEMasters"] = 0;
                frm.Attributes.Add("src", "~/Modules/LPSQE/ShipCertificate/CertMaster.aspx");
                break;
            case 1:
                btnVSLReportGrp.CssClass = "selbtn";
                Session["LPSQEMasters"] = 1;
                frm.Attributes.Add("src", "~/Modules/LPSQE/Registers/VesselReportGroup.aspx");
                break;
            case 2:
                btnVSLReport.CssClass = "selbtn";
                Session["LPSQEMasters"] = 2;
                frm.Attributes.Add("src", "~/Modules/LPSQE/Registers/VesselReportsMapping.aspx");
                break;
            default:
                break;
        }
    }

   
}