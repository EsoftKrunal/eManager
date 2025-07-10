using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class HRD_UserControls_ModuleMenu : System.Web.UI.UserControl
{
    protected void Page_Load( object sender, EventArgs e)
    {
        int UserId = 0;
        UserId = int.Parse(Session["loginid"].ToString());
        tr_CrewParticular.Visible = new AuthenticationManager(1, UserId, ObjectType.Module).IsView ;
        tr_CrewOperation.Visible = new AuthenticationManager(2, UserId, ObjectType.Module).IsView && (Alerts.getLocation() == "S");
        tr_Accounts.Visible = new AuthenticationManager(3, UserId, ObjectType.Module).IsView && (Alerts.getLocation() == "S");
        tr_Vessel.Visible = new AuthenticationManager(4, UserId, ObjectType.Module).IsView;
        tr_Registers.Visible = new AuthenticationManager(5, UserId, ObjectType.Module).IsView && (Alerts.getLocation() == "S");
        tr_Reporting.Visible = new AuthenticationManager(6, UserId, ObjectType.Module).IsView;
        tr_Training.Visible = new AuthenticationManager(7, UserId, ObjectType.Module).IsView && (Alerts.getLocation() == "S");
       // tr_Invoice.Visible = new AuthenticationManager(8, UserId, ObjectType.Module).IsView && (Alerts.getLocation() == "S");
        tr_CrewApproval.Visible = new AuthenticationManager(9, UserId, ObjectType.Module).IsView && (Alerts.getLocation() == "S");
        tr_CrewAppraisal.Visible = new AuthenticationManager(26, UserId, ObjectType.Module).IsView && (Alerts.getLocation() == "S");
	tr_Applicant.Visible = new AuthenticationManager(46, UserId, ObjectType.Module).IsView && (Alerts.getLocation() == "S");
	tr_CrmActivities.Visible = new AuthenticationManager(47, UserId, ObjectType.Module).IsView && (Alerts.getLocation() == "S");
	tr_CrewRestHr.Visible = new AuthenticationManager(48, UserId, ObjectType.Module).IsView && (Alerts.getLocation() == "S");
    //tr_CrewRestHr_N.Visible = new AuthenticationManager(48, UserId, ObjectType.Module).IsView && (Alerts.getLocation() == "S");


   tr_Seminar.Visible = new AuthenticationManager(48, UserId, ObjectType.Module).IsView && (Alerts.getLocation() == "S");

        m1.Visible = Convert.ToString(Session["NoMenu"]) != "Y";  
    }
}
