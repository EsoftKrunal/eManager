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

public partial class UserControls_ModuleMenu : System.Web.UI.UserControl
{
    public string cUrl = ""; 
    protected void Page_Load(object sender, EventArgs e)
    {
        cUrl = (new LinkButton()).ResolveClientUrl("~/Images/");  
        int UserId = 0;
        
        UserId = int.Parse(Session["loginid"].ToString());
        tr_VIMS.Visible = new AuthenticationManager(10, UserId, ObjectType.Module).IsView ;
        tr_Tracker.Visible = new AuthenticationManager(13, UserId, ObjectType.Module).IsView ;
        //tr_Reports.Visible = new AuthenticationManager(11, UserId, ObjectType.Module).IsView ;
        tr_Vessel_Cert.Visible = new AuthenticationManager(14, UserId, ObjectType.Module).IsView ;
        tr_Registers.Visible = new AuthenticationManager(12, UserId, ObjectType.Module).IsView;
        tr_PositionRep.Visible =false ;
        tr_PositionRepNew.Visible = new AuthenticationManager(21, UserId, ObjectType.Module).IsView;
        //tr_Circular.Visible = new AuthenticationManager(31, UserId, ObjectType.Module).IsView;
        tr_ManForms.Visible = (new AuthenticationManager(41, UserId, ObjectType.Module).IsView || new AuthenticationManager(42, UserId, ObjectType.Module).IsView);
        tr_Vettingmgmt.Visible = new AuthenticationManager(43, UserId, ObjectType.Module).IsView;
        tr_PositionReport.Visible = new AuthenticationManager(21, UserId, ObjectType.Module).IsView;
    }
}
