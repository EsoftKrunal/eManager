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

public partial class Master_RestHour : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        //btnTM.CssClass = "btn1";
        //btnTMg.CssClass = "btn1";
        //btnTr.CssClass = "btn1";
        //Alerts.SetHelp(imgHelp);
        if (Request.QueryString["reset"] != null)
        {
            Session["MM"] = 1;
        }
        //switch (Common.CastAsInt32(Session["MM"]))
        //{
        //    case 1:
        //        btnTM.CssClass = "selbtn";
        //        break;
        //    case 2:
        //        btnTMg.CssClass = "selbtn";
        //        break;
        //    case 3:
        //        btnTr.CssClass = "selbtn";
        //        break;
        //    default:
        //        btnTM.CssClass = "selbtn";
        //        break;
        //}
    }
    protected void RegisterSelect(object sender, EventArgs e)
    {
       
        
        Button btn = (Button)sender;
        int CommArg = Common.CastAsInt32(btn.CommandArgument);

        
        switch (CommArg)
        {
            case 1:
                Session["MM"] = 1;
                Response.Redirect("../CrewOperation/UpdateTrainingMatrix.aspx");  
                break;
            case 2:
                Session["MM"] = 2;
                Response.Redirect("../CrewOperation/TrainingMgmt.aspx");  
                break;
            case 3:
                Session["MM"] = 3;
                Response.Redirect("../CrewOperation/TrainingRegister.aspx");  
                break;
            default:
                break;
        }

    }
}
  