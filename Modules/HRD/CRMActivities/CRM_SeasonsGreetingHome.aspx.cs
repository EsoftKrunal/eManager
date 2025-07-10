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

public partial class CRMActivities_CRM_SeasonsGreetingHome : System.Web.UI.Page
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
            BindOffice();
            //ShowHolidays();
        }
    }

    public void BindOffice()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("SELECT RecruitingOfficeId,RecruitingOfficeName  FROM RecruitingOffice WHERE StatusId = 'A' ORDER BY RecruitingOfficeName");

        ddloffice.DataSource = dt;
        ddloffice.DataTextField = "RecruitingOfficeName";
        ddloffice.DataValueField = "RecruitingOfficeId";
        ddloffice.DataBind();
        ddloffice.Items.Insert(0, new ListItem("< Select >", "0"));
    }

    protected void ddloffice_SelectedIndexChanged(object sender, EventArgs e)
    {
        ShowHolidays(); 
    }

    public void ShowHolidays()
    {
        string SQL = "SELECT Row_Number() OVER(Order By HM.HolidayId ) AS SrNo, *,(SELECT COUNT(*) FROM CrewPersonalDetails WHERE RecruitmentOfficeId=HM.OfficeId AND ISNULL(IsFamilyMember,'N') = 'N' AND CrewStatusId IN (2,3)) AS NoOfCrew FROM HR_HolidayMaster HM WHERE HM.[Year] = Year(getdate()) ";
        string Where = "";

        if (ddloffice.SelectedIndex != 0)
        {
            Where = Where + "AND OfficeId = " + ddloffice.SelectedValue.Trim();
        }

        SQL = SQL + Where;
        DataTable dtHolidays = Common.Execute_Procedures_Select_ByQueryCMS(SQL);

        rpt_Holiday.DataSource = dtHolidays;
        rpt_Holiday.DataBind();
    }
    protected void lnkNoOfCrew_Click(object sender, EventArgs e)
    {
        int HolidayId = Common.CastAsInt32(((LinkButton)sender).CommandArgument);
        Response.Redirect("CRM_SeasonsGreeting.aspx?HId=" + HolidayId + "&OfficeId=" + ddloffice.SelectedValue.Trim());
    }

}