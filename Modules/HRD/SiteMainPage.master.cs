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

using ShipSoft.CrewManager.BusinessLogicLayer;
using ShipSoft.CrewManager.BusinessObjects;
using ShipSoft.CrewManager.Operational;

public partial class SiteMainPage : System.Web.UI.MasterPage
{
    public int LoginId;
    public Random RUpdate= new Random();
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        this.Page.Title = "SHIPSOFT " + ((Alerts.getLocation() == "S") ? "[ LIVE ]" : "[ YANGOON ]") + " :: The Crew Management System";  
        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 127);
        if (chpageauth <= 0)
        {
            this.a.Visible = false;
        }
        else
        {
            this.a.Visible = false && (Alerts.getLocation() == "S") && (LoginId == 1);
        }
        //*******************
        LoginId = Convert.ToInt32(Session["loginid"].ToString());  
        DataTable dt;
        dt = Alerts.getCRMAlert();
        if(dt .Rows.Count >0)
        {
            lbl_CRM.Visible = true;
            lbl_CRM.Text= "CRM Alert (" + dt .Rows.Count + ")"  ; 
        }
        dt = Alerts.get_DocumentAlert();
        if (dt.Rows.Count > 0)
        {
             lbl_Other.Visible = true;
             lbl_Other.Text = "Document Alert (" + dt.Rows.Count + ")"; 
        }
        dt = Alerts.getSignOffCrewAlert();
        if (dt.Rows.Count > 0)
        {
            lbl_SignOffCrew.Visible = true;
            lbl_SignOffCrew.Text = "SignOff Crew Alert (" + dt.Rows.Count + ")";
        }
        dt = Alerts.getVesselManningAlert();
        if (dt.Rows.Count > 0)
        {
            lbl_VesselManning.Visible = true;
            lbl_VesselManning.Text = "Vessel Manning Alert (" + dt.Rows.Count + ")";
        }
        DataSet ds = Budget.getTable("select CrewNumber,Firstname+ '' +  MiddleName + ' ' + LastName as FullName,  " + 
                                    "(select vesselname from vessel where vessel.vesselid=cpd.lastvesselid) as Vessel, " + 
                                    "(select top 1 ContractReferenceNumber from crewcontractheader cch where cch.crewid=cpd.crewid And Status='A' order by contractid desc) as RefNumber, " +
                                    "convert(Varchar,SignOffDate,101) as SignOffDate " + 
                                    "from crewpersonaldetails cpd where cpd.signoffdate is not null and isnull(lastvesselid,0) >0 and not exists " +
                                    "(select top 1 crewid from crewappraisaldetails cad where cad.AppraisalOccasionId=10 and cad.createdon between cpd.signoffdate and dateadd(day,8,cpd.signoffdate)) and cpd.signoffdate between getdate() and dateadd(day,30,getdate())");
        dt = ds.Tables[0]; 
        if (dt.Rows.Count > 0)
        {
            lbl_SignOffAlert.Visible = true;
            lbl_SignOffAlert.Text = "EOC Appraisal Alert (" + dt.Rows.Count + ")";
        }
        

        try
        {
            this.UserName.Text = Session["UserName"].ToString();
        }
        catch
        {
            Response.Redirect("Login.aspx");
        }

        if (LoginId == 1)
            this.a.Visible = true;
        else
            this.a.Visible = false;
    }
    public int isAuthorized(int ModuleId)
    {
        int a;
        a = 0;
        a= Alerts.isAutorized(LoginId, ModuleId);
        return a;
    }
    public Boolean isAdmin()
    {
        int i;
        i = Alerts.getRoleId(LoginId);
        if (i == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
         
    }
    protected void ImageButtonLogout_Click(object sender, ImageClickEventArgs e)
    {
        Session.Abandon();

        if (Alerts.getLocation() == "S")
        {
            Response.Redirect(ConfigurationManager.AppSettings["VIMSLink"]);
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("changepassword.aspx");
    }
}
