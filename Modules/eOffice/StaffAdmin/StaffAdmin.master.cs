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

public partial class Modules_eOffice_StaffAdmin_StaffAdmin : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        btnPersonalDetails.CssClass = "btn1";
        btnCompBenefits.CssClass = "btn1";
        btnLeaveOffAbsence.CssClass = "btn1";
        btnApplicant.CssClass = "btn1";
        btnPEAP.CssClass = "btn1";
        btnRegisters.CssClass = "btn1";
        btnTraining.CssClass = "btn1";

        if (Request.QueryString["reset"] != null)
        {
            Session["SA"] = 0;
        }
        switch (Common.CastAsInt32(Session["SA"]))
        {
            case 0:
                btnPersonalDetails.CssClass = "selbtn";
                break;
            case 1:
                btnCompBenefits.CssClass = "selbtn";
                break;
            case 2:
                btnTraining.CssClass = "selbtn";
                break;
            case 3:
                btnLeaveOffAbsence.CssClass = "selbtn";
                break;
            case 4:
                btnApplicant.CssClass = "selbtn";
                break;
            case 5:
                btnPEAP.CssClass = "selbtn";
                break;
            case 6:
                btnRegisters.CssClass = "selbtn";
                break;
            default:
                break;
        }
    }
    protected void RegisterSelect(object sender, EventArgs e)
    {


        Button btn = (Button)sender;
        int CommArg = Common.CastAsInt32(btn.CommandArgument);


        switch (CommArg)
        {
            case 0:
                Session["SA"] = 0;
                Response.Redirect("~/Modules/eOffice/StaffAdmin/SearchDetail.aspx");
                break;
            case 1:
                Session["SA"] = 1;
                Response.Redirect("~/Modules/eOffice/StaffAdmin/Compensation/CompensationBenifits.aspx");
                break;
            case 2:
                Session["SA"] = 2;
                Response.Redirect("~/Modules/eOffice/StaffAdmin/TrainingMatrix.aspx");
                break;
            case 3:
                Session["SA"] = 3;
                Response.Redirect("~/Modules/eOffice/StaffAdmin/LeaveSearch.aspx");
                break;
            case 4:
                Session["SA"] = 4;
                Response.Redirect("~/Modules/eOffice/StaffAdmin/Applicant.aspx");
                break;
            case 5:
                Session["SA"] = 5;
                Response.Redirect("~/Modules/eOffice/StaffAdmin/HR_Peap.aspx");
                break;
            case 6:
                Session["SA"] = 6;
                Response.Redirect("~/Modules/eOffice/Registers.aspx");
                break;
            
            default:
                break;
        }

    }
}
