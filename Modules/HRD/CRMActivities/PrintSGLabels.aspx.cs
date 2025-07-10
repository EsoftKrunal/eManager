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

public partial class CRMActivities_PrintSGLabels : System.Web.UI.Page
{
    public int HolidayId
    {
        set { ViewState["HolidayId"] = value; }
        get { return Common.CastAsInt32(ViewState["HolidayId"]); }
    }
    public int RecOfficeId
    {
        set { ViewState["RecOfficeId"] = value; }
        get { return Common.CastAsInt32(ViewState["RecOfficeId"]); }
    }
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
            HolidayId = Common.CastAsInt32(Request.QueryString["HId"].ToString());
            RecOfficeId = Common.CastAsInt32(Request.QueryString["OfficeId"].ToString());
            BindCrewStatusDropDown();
            BindRankDropDown();
            ShowRecords();
        }
    }

    private void BindCrewStatusDropDown()
    {
        ProcessSelectCrewStatus obj = new ProcessSelectCrewStatus();
        obj.Invoke();
        DataView dv = new DataView(obj.ResultSet.Tables[0]);
        dv.RowFilter = "CrewStatusName = 'On Leave' OR CrewStatusName = 'On Board' ";
        ddl_CrewStatus_Search.DataSource = dv;
        ddl_CrewStatus_Search.DataTextField = "CrewStatusName";
        ddl_CrewStatus_Search.DataValueField = "CrewStatusId";
        ddl_CrewStatus_Search.DataBind();
        ddl_CrewStatus_Search.Items.Insert(0, new ListItem(" All ", ""));

    }
    private void BindRankDropDown()
    {
        ProcessSelectRank obj = new ProcessSelectRank();
        obj.Invoke();
        ddl_Rank_Search.DataSource = obj.ResultSet.Tables[0];
        ddl_Rank_Search.DataTextField = "RankName";
        ddl_Rank_Search.DataValueField = "RankId";
        ddl_Rank_Search.DataBind();
        ddl_Rank_Search.Items.RemoveAt(0);
        ddl_Rank_Search.Items.Insert(0, new ListItem(" All ", ""));

    }
    
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ShowRecords();
    }
    public void ShowRecords()
    {
       string WHERE = "WHERE 1=1 ";

        string SQL = "SELECT CPD.CrewId,CrewNumber, FirstName + ' ' + MiddleName + ' ' + LastName AS CrewName,DateOfBirth, R.RankCode,CS.CrewStatusName,RO.RecruitingOfficeName, " +
                      "(Address1 + ' ' + Address2 + ' ' + Address3 + ' ' + City + ' ' + [State] + C.CountryName + ' ' + PINCode) AS [Address], CCD.City " +
                        "FROM CrewPersonalDetails CPD  " +
                        "LEFT JOIN CrewContactDetails CCD ON CPD.CrewId = CCD.CrewId AND CCD.AddressType='C'  " +
                        "LEFT JOIN Country C ON CCD.CountryId = C.CountryId   " +
                        "INNER JOIN CrewSeasonsGreetingCardDetails CSGD ON CPD.CrewId = CSGD.CrewId  AND CSGD.HolidayId = " + HolidayId + " AND CSGD.Year = year(getdate())  " +
                        "INNER JOIN [RANK] R ON CPD.CurrentRankId = R.RankId   " +
                        "INNER JOIN RecruitingOffice RO ON CPD.RecruitmentOfficeId = RO.RecruitingOfficeId   " +
                        "INNER JOIN CrewStatus CS ON CPD.CrewStatusId = CS.CrewStatusId  ";


        if (ddl_Rank_Search.SelectedIndex != 0)
        {
            WHERE = WHERE + " AND CPD.CurrentRankId = " + ddl_Rank_Search.SelectedValue.Trim();
        }
        if (ddl_CrewStatus_Search.SelectedIndex != 0)
        {
            WHERE = WHERE + " AND CPD.CrewStatusId = " + ddl_CrewStatus_Search.SelectedValue.Trim();
        }
        //if (ddl_Recr_Office.SelectedIndex != 0)
        //{
        //    WHERE = WHERE + " AND CPD.RecruitmentOfficeId = " + ddl_Recr_Office.SelectedValue.Trim();
        //}

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