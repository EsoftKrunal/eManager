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

public partial class CRMActivities_PrintWelcomeLabel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        lblMsg.Text = "";
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 1);
        if (chpageauth <= 0)
        {
            Response.Redirect("../AuthorityError.aspx");
        }
        if (!IsPostBack)
        {
            BindRankDropDown();
            BindRecruitingOffice();
            ShowRecords();            
        }
    }
    private void BindRecruitingOffice()
    {
        ProcessGetRecruitingOffice processgetrecruitingoffice = new ProcessGetRecruitingOffice();
        try
        {
            processgetrecruitingoffice.Invoke();
        }
        catch (Exception ex)
        {
            //Response.Write(ex.Message.ToString());
        }
        ddl_Recr_Office.DataValueField = "RecruitingOfficeId";
        ddl_Recr_Office.DataTextField = "RecruitingOfficeName";
        ddl_Recr_Office.DataSource = processgetrecruitingoffice.ResultSet;
        ddl_Recr_Office.DataBind();
        ddl_Recr_Office.Items.RemoveAt(0);
        ddl_Recr_Office.Items.Insert(0, new ListItem(" All ", ""));
    }

    
    private void BindRankDropDown()
    {
        string Where = "WHERE OffCrew = '" + ddl_OR_Search.SelectedValue.Trim() + "' ";
        string SQL = "SELECT RankId, RankCode FROM RANK  ";

        if (ddl_OR_Search.SelectedIndex != 0)
        {
            SQL = SQL + Where;
        }

        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(SQL);
        ddl_Rank_Search.DataSource = dt;
        ddl_Rank_Search.DataTextField = "RankCode";
        ddl_Rank_Search.DataValueField = "RankId";
        ddl_Rank_Search.DataBind();
        ddl_Rank_Search.Items.RemoveAt(0);
        ddl_Rank_Search.Items.Insert(0, new ListItem(" All ", ""));

    }
    protected void ddl_OR_Search_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        BindRankDropDown();
    }
    
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ShowRecords();
    }
    public void ShowRecords()
    {

        string WHERE = " WHERE (DATEADD(dd, 0, DATEDIFF(dd, 0, SignOffDate)) <= DATEADD(dd, 0, DATEDIFF(dd, 0, GETDATE())) AND DATEADD(dd, 0, DATEDIFF(dd, 0, SignOffDate)) >= DATEADD(dd,-15,DATEADD(dd, 0, DATEDIFF(dd, 0, GETDATE())))) AND ISNULL(IsFamilyMember,'N') = 'N' AND CPD.CrewStatusId = 2 ";

            string SQL = "SELECT CPD.CrewId,CrewNumber, FirstName + ' ' + MiddleName + ' ' + LastName AS CrewName,DateOfBirth, R.RankCode,CS.CrewStatusName,RO.RecruitingOfficeName, " +
                         "(Address1 + ' ' + Address2 + ' ' + Address3 + ' ' + City + ' ' + [State] + C.CountryName + ' ' + PINCode) AS [Address], CCD.City " +
                         "FROM CrewPersonalDetails CPD  " +
                         "LEFT JOIN CrewContactDetails CCD ON CPD.CrewId = CCD.CrewId AND CCD.AddressType='C'  " +
                         "LEFT JOIN Country C ON CCD.CountryId = C.CountryId   " +
                         "INNER JOIN CrewWelcomeCardDetails CWD ON CPD.CrewId = CWD.CrewId AND CWD.Year = year(getdate())  " +
                         "INNER JOIN [RANK] R ON CPD.CurrentRankId = R.RankId   " +
                         "INNER JOIN RecruitingOffice RO ON CPD.RecruitmentOfficeId = RO.RecruitingOfficeId   " +
                         "INNER JOIN CrewStatus CS ON CPD.CrewStatusId = CS.CrewStatusId  ";

            if (ddl_Recr_Office.SelectedIndex != 0)
            {
                WHERE = WHERE + " AND CPD.RecruitmentOfficeId = " + ddl_Recr_Office.SelectedValue.Trim();
            }
            if (ddl_Rank_Search.SelectedIndex != 0)
            {
                WHERE = WHERE + " AND CPD.CurrentRankId = " + ddl_Rank_Search.SelectedValue.Trim();
            }
            if (ddl_OR_Search.SelectedIndex != 0)
            {
                WHERE = WHERE + " AND R.OffCrew = '" + ddl_OR_Search.SelectedValue.Trim() + "' ";
            }            
            
            SQL = SQL + WHERE;

            DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(SQL);
            rpt_PrintLabel_Crew.DataSource = dt;
            rpt_PrintLabel_Crew.DataBind();

            lblRcount1.Text = "Total Records : " + dt.Rows.Count;
        
    }

    protected void chkCheckAll_Print_CheckedChanged(object sender, EventArgs e)
    {
        foreach (RepeaterItem riCrew in rpt_PrintLabel_Crew.Items)
        {
            CheckBox chk = (CheckBox)riCrew.FindControl("chkSelect");
            chk.Checked = chkSelectAll_Print_Crew.Checked ? true : false;
        }
    }
    protected void btnPrintBDLabel_Click(object sender, EventArgs e)
    {
        bool check = false;
        string Ids = "";
        
        foreach (RepeaterItem riCrew in rpt_PrintLabel_Crew.Items)
        {
            CheckBox chk = (CheckBox)riCrew.FindControl("chkSelect");
            if (chk.Checked)
            {
                check = true;
                break;
            }
        }
        if (!check)
        {
            lblMsg.Text = "Please select a crew to print label";
            return;
        }

        foreach (RepeaterItem riCrew in rpt_PrintLabel_Crew.Items)
        {
            CheckBox chk = (CheckBox)riCrew.FindControl("chkSelect");
            if (chk.Checked)
            {
                Ids = Ids + chk.Attributes["CrewId"].ToString() + ",";
            }
        }

        Ids = Ids.Remove(Ids.Trim().Length - 1);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "asdsad", "printBDLabel('" + Ids + "');", true);

        
    }
}