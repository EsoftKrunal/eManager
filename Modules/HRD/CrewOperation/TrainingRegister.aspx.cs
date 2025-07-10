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
using ShipSoft.CrewManager.BusinessObjects;
using ShipSoft.CrewManager.BusinessLogicLayer;
using ShipSoft.CrewManager.Operational;
using System.IO;
using System.Text;

public partial class TrainingRegister : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        if (!Page.IsPostBack)
        {
            
        }
    }
    protected void RegisterSelect(object sender,EventArgs e)
    {
        btnMTraining.CssClass = "btn1";
        btnTraining.CssClass = "btn1";
        btnTrainingType.CssClass = "btn1";
        btnTM.CssClass="btn1";
        btnTrainingInstitute.CssClass = "btn1";  

        Button btn=(Button)sender ;
        int CommArg = Common.CastAsInt32(btn.CommandArgument);

        btn.CssClass = "selbtn";
        switch (CommArg)
        {
            case 1:
                frm.Attributes.Add("src", "../Registers/TrainingType.aspx");  
                break;
            case 2:
                frm.Attributes.Add("src", "../Registers/Training.aspx");
                break;
            case 3:
                frm.Attributes.Add("src", "../Registers/MTMTraining.aspx");
                break;
            case 4:
                frm.Attributes.Add("src", "../Registers/TrainingInstitute.aspx");
                break;
            case 5:
                frm.Attributes.Add("src", "../Registers/TrainingMatrix.aspx");
                break;
            default:
                break; 
        }
 
    }
}
