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

public partial class CrewTraining : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        btnTM.CssClass = "btn1";
        btnTMg.CssClass = "btn1";
        btnTr.CssClass = "btn1";
        btnAssessmentQuestion.CssClass = "btn1";
        

        
        if (Request.QueryString["reset"] != null)
        {
            Session["MM"] = 0;
        }
        switch (Common.CastAsInt32(Session["MM"]))
        {
            case 0:
                btnTMg.CssClass = "selbtn";
                break;
            case 1:
                btnOST.CssClass = "selbtn";
                break;
            case 2:
                btnAssign.CssClass = "selbtn";
                break;
            case 3:
                btnReport.CssClass = "selbtn";
                break;
            case 4:
                btnTM.CssClass = "selbtn";
                break;
            case 5:
                btnTr.CssClass = "selbtn";
                break;
            case 6:
                btnSeminar.CssClass = "selbtn";
                break;
            case 7:
                btnAssessmentQuestion.CssClass = "selbtn";
                break;
            case 8:
                btnKSt.CssClass = "selbtn";
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
                Session["MM"] = 0;
                Response.Redirect("~/Modules/HRD/CrewOperation/TrainingMgmt.aspx?Mode=OnBoard");
                break;
            case 1:
                Session["MM"] = 1;
                Response.Redirect("~/Modules/HRD/CrewOperation/TrainingMgmt.aspx?Mode=OnLeave");
                break;
            case 2:
                Session["MM"] = 2;
                Response.Redirect("~/Modules/HRD/CrewOperation/AssignTraining.aspx");
                break;
            case 3:
                Session["MM"] = 3;
                Response.Redirect("~/Modules/HRD/Reporting/CrewTrainingReportCopy.aspx");
                break;
            case 4:
                Session["MM"] = 4;
                Response.Redirect("~/Modules/HRD/CrewOperation/UpdateTrainingMatrix.aspx");  
                break;
            case 5:
                Session["MM"] = 5;
                Response.Redirect("~/Modules/HRD/CrewOperation/TrainingRegister.aspx");  
                break;
            case 6:
                Session["MM"] = 6;
                Response.Redirect("~/Modules/HRD/CrewOperation/Seminar/Seminar.aspx");
                break;
            case 7:
                Session["MM"] = 7;
                Response.Redirect("~/Modules/HRD/CrewOperation/AssessmentQuestion.aspx");
                break;
            case 8:
                Session["MM"] = 8;
                Response.Redirect("~/Modules/HRD/CrewOperation/KST.aspx");
                break;
            default:
                break;
        }

    }
}
  