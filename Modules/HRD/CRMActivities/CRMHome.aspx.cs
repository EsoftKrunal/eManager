using System;
using System.Data;
using System.Configuration; 
using System.Reflection;
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

public partial class CRMActivities_CRMHome : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 1);
        if (chpageauth <= 0)
        {
            Response.Redirect("../AuthorityError.aspx");
        }
        if (!IsPostBack)
        {
            ShowData();            
        }
    }

    public void ShowData()
    {
        string sql1 = "SELECT CrewId,CrewNumber, FirstName + ' ' + MiddleName + ' ' + LastName AS CrewName,DateOfBirth FROM CrewPersonalDetails " +
                      "WHERE (DATEADD( year, YEAR(getdate()) - YEAR(DateOfBirth), DateOfBirth) BETWEEN DATEADD(dd, 0, DATEDIFF(dd, 0, GETDATE())) AND DATEADD(d," + txtBDays.Text.Trim() + ", DATEADD(dd, 0, DATEDIFF(dd, 0, GETDATE())))) AND CrewStatusId IN (2,3) " +
                      "UNION " +
                      "SELECT CFD.CrewFAMILYId,CFD.FAMILYEMPLOYEENUMBER,CFD.FirstName + ' ' + CFD.MiddleName + ' ' + CFD.LastName AS CrewName,CFD.DateOfBirth " +
                      "FROM CREWFAMILYDETAILS CFD " + 
                      "INNER JOIN CrewPersonalDetails CPD ON CFD.CREWID=CPD.CREWID " +
                      "INNER JOIN [RANK] R ON  CPD.CurrentRankId = R.RankId AND R.OffCrew = 'O' " +
                      "WHERE ((DATEADD( year, YEAR(getdate()) - YEAR(CFD.DateOfBirth), CFD.DateOfBirth)) BETWEEN DATEADD(dd, 0, DATEDIFF(dd, 0, GETDATE())) AND DATEADD(d," + txtBDays.Text.Trim() + ",DATEADD(dd, 0, DATEDIFF(dd, 0, GETDATE())))) AND CPD.CrewStatusId IN (2,3) ";

        DataTable dt1 = Common.Execute_Procedures_Select_ByQueryCMS(sql1);
        int Count1 = Common.CastAsInt32(dt1.Rows.Count);
        lnkBirthDays.Text = Count1.ToString();

        string sql2 = "SELECT CrewId,CrewNumber, FirstName + ' ' + MiddleName + ' ' + LastName AS CrewName,DateOfBirth FROM CrewPersonalDetails " +
                      "WHERE (DATEADD(dd, 0, DATEDIFF(dd, 0, SignOffDate)) <= DATEADD(dd, 0, DATEDIFF(dd, 0, GETDATE())) AND DATEADD(dd, 0, DATEDIFF(dd, 0, SignOffDate)) >= DATEADD(dd,-15,DATEADD(dd, 0, DATEDIFF(dd, 0, GETDATE())))) AND ISNULL(IsFamilyMember,'N') = 'N' AND CrewStatusId = 2 ";
                      
        DataTable dt2 = Common.Execute_Procedures_Select_ByQueryCMS(sql2);
        int Count2 = Common.CastAsInt32(dt2.Rows.Count);
        lnkWelcome.Text = Count2.ToString();


        string sql3 = "select count(crewid) from CrewPersonalDetails where left(crewnumber,1) in ('S','Y') AND CrewStatusId=2";

        DataTable dt3 = Common.Execute_Procedures_Select_ByQueryCMS(sql3);
        int Count3 = Common.CastAsInt32(dt3.Rows[0][0]);
        lnlOnleaveCrew.Text = Count3.ToString();

    }

    protected void txtBDays_TextChanged(object sender, EventArgs e)
    {
        int i;
        if (txtBDays.Text.Trim() != "" && int.TryParse(txtBDays.Text.Trim(), out i))
        {
            ShowData();
        }
    }
    protected void lnkBirthDays_Click(object sender, EventArgs e)
    {
        // Response.Redirect("CRM_BirthDays.aspx?Days=" + txtBDays.Text.Trim());
        ScriptManager.RegisterStartupScript(this, this.GetType(), "fsadf", "window.open('CRM_BirthDays.aspx?Days=" + txtBDays.Text.ToString().Trim() + "');", true);
    }
    protected void lnkSeasonsGreeting_Click(object sender, EventArgs e)
    {
        //Response.Redirect("CRM_SeasonsGreetingHome.aspx");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "fsadf", "window.open('CRM_SeasonsGreetingHome.aspx');", true);
    }
    protected void lnkWelcome_Click(object sender, EventArgs e)
    {
        // Response.Redirect("CRM_WelcomeHome.aspx");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "fsadf", "window.open('CRM_WelcomeHome.aspx');", true);
    }
    protected void lnkInventory_Click(object sender, EventArgs e)
    {
        int CardType = Common.CastAsInt32(((LinkButton)sender).CommandArgument);
        //Response.Redirect("CRM_InventoryHome.aspx?CT=" + CardType);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "fsadf", "window.open('CRM_InventoryHome.aspx?CT=" + CardType.ToString() + "');", true);
    }
    protected void lnlOnleaveCrew_Click(object sender, EventArgs e)
    {
        int CardType = Common.CastAsInt32(((LinkButton)sender).CommandArgument);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "fsadf", "window.open('CRM_CrewCommunication.aspx');", true);        
        


    }
    

}