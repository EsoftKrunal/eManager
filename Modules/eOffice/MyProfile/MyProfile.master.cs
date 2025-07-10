using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modules_eOffice_MyProfile_MyProfile : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        btnPersonalDetails.CssClass = "btn1";
        btnCompBenefits.CssClass = "btn1";
        btnLeaveOffAbsence.CssClass = "btn1";
        
        btnPEAP.CssClass = "btn1";
        btnMD.CssClass = "btn1";
        btnTraining.CssClass = "btn1";

        if (Request.QueryString["reset"] != null)
        {
            Session["MP"] = 0;
        }
        switch (Common.CastAsInt32(Session["MP"]))
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
                
                btnPEAP.CssClass = "selbtn";
                break;
            case 5:
                btnMD.CssClass = "selbtn";
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
                Session["MP"] = 0;
                Response.Redirect("~/Modules/eOffice/MyProfile/Profile_PersonalDetail.aspx");
                break;
            case 1:
                Session["MP"] = 1;
                Response.Redirect("~/Modules/eOffice/MyProfile/CB_Details.aspx");
                break;
            case 2:
                Session["MP"] = 2;
                Response.Redirect("~/Modules/eOffice/MyProfile/Profile_TrainingManagement.aspx");
                break;
            case 3:
                Session["MP"] = 3;
                Response.Redirect("~/Modules/eOffice/MyProfile/Profile_LeaveDetails.aspx");
                break;
            case 4:
                Session["MP"] = 4;
                Response.Redirect("~/Modules/eOffice/MyProfile/Profile_Peap.aspx");
                break;
            case 5:
                Session["MP"] = 5;
                Response.Redirect("~/Modules/eOffice/MyProfile/Profile_TalkToMD.aspx");
                break;
           

            default:
                break;
        }

    }
}
