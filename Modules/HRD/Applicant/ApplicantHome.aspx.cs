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
using System.Xml;

public partial class CrewApproval_ApplicantHome : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
    }
    protected void btn_Home_Click(object sender, EventArgs e)
    {
        frm1.Attributes.Add("src", "~/Modules/HRD/Applicant/ApplicantHomeNew.aspx");
        btnHome.CssClass = "btn1selected";
        btnApplicant.CssClass = "btn1";
    }

    protected void btn_Applicant_Click(object sender, EventArgs e)
    {
        frm1.Attributes.Add("src", "~/Modules/HRD/Applicant/CandidateDetailInformation.aspx");
        btnHome.CssClass = "btn1";
        btnApplicant.CssClass = "btn1selected";
    }
}
