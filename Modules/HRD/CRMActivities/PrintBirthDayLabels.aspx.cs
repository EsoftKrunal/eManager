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

public partial class CRMActivities_PrintBirthDayLabels : System.Web.UI.Page
{
    public int Days
    {
        set { ViewState["Days"] = value; }
        get { return Common.CastAsInt32(ViewState["Days"]); }
    }
    public int CrewFamily
    {
        set { ViewState["CrewFamily"] = value; }
        get { return Common.CastAsInt32(ViewState["CrewFamily"]); }
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
            Days = Common.CastAsInt32(Request.QueryString["Days"].ToString());
            CrewFamily = Common.CastAsInt32(Request.QueryString["CF"].ToString());
            BindCrewStatusDropDown();
            BindRankDropDown();
            BindRecruitingOffice();
            BindRelationDropDown();
            ShowRecords();
            tdRelationTit.Visible = tdRelationdd.Visible = (CrewFamily==2);
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

    private void BindRelationDropDown()
    {
        FieldInfo[] thisEnumFields = typeof(Relationship).GetFields();
        foreach (FieldInfo thisField in thisEnumFields)
        {
            if (!thisField.IsSpecialName && thisField.Name.ToLower() != "notset")
            {
                int thisValue = (int)thisField.GetValue(0);
                ddlrelation.Items.Add(new ListItem(thisField.Name, thisValue.ToString()));
            }
        }
        ddlrelation.Items.Insert(0, new ListItem(" All ", ""));
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ShowRecords();
    }
    public void ShowRecords()
    {
        if (CrewFamily == 1)
        {
            string WHERE = "WHERE ((DATEADD( year, YEAR(getdate()) - YEAR(DateOfBirth), DateOfBirth) BETWEEN DATEADD(dd, 0, DATEDIFF(dd, 0, GETDATE())) AND DATEADD(d," + Days + ",DATEADD(dd, 0, DATEDIFF(dd, 0, GETDATE()))))) ";
            
            string SQL = "SELECT CPD.CrewId,CrewNumber, FirstName + ' ' + MiddleName + ' ' + LastName AS CrewName,DateOfBirth, R.RankCode,CS.CrewStatusName,RO.RecruitingOfficeName, " +
                //"(SELECT FirstName + ' ' + LastName FROM UserLogin WHERE LoginId = CBD.UpdatedBy) As UpdatedBy, CBD.UpdatedOn, " +
                         "(Address1 + ' ' + Address2 + ' ' + Address3 + ' ' + City + ' ' + [State] + C.CountryName + ' ' + PINCode) AS [Address], CCD.City " +
                         "FROM CrewPersonalDetails CPD  " +
                         "LEFT JOIN CrewContactDetails CCD ON CPD.CrewId = CCD.CrewId AND CCD.AddressType='C'  " +
                         "LEFT JOIN Country C ON CCD.CountryId = C.CountryId   " +
                         "INNER JOIN CrewBirthDayCardDetails CBD ON CPD.CrewId = CBD.CrewId AND CBD.Year = year(getdate())  " +
                         "INNER JOIN [RANK] R ON CPD.CurrentRankId = R.RankId  " +
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
            if (ddl_Recr_Office.SelectedIndex != 0)
            {
                WHERE = WHERE + " AND CPD.RecruitmentOfficeId = " + ddl_Recr_Office.SelectedValue.Trim();
            }

            SQL = SQL + WHERE + " ORDER BY MONTH(CPD.DateOfBirth), Day(CPD.DateOfBirth)";

            DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(SQL);
            rpt_PrintLabel_Crew.DataSource = dt;
            rpt_PrintLabel_Crew.DataBind();

            lblRcount1.Text = "Total Records : " + dt.Rows.Count;

            dv_Print_Crew.Visible = true;
            dv_Print_Family.Visible = false;
            
        }

        if (CrewFamily == 2)
        {
            string WHERE_F = " WHERE ((DATEADD( year, YEAR(getdate()) - YEAR(CFD.DateOfBirth), CFD.DateOfBirth) BETWEEN DATEADD(dd, 0, DATEDIFF(dd, 0, GETDATE())) AND DATEADD(d," + Days + ",DATEADD(dd, 0, DATEDIFF(dd, 0, GETDATE()))))) ";

            string SQL_F = "SELECT CFD.CrewFAMILYId,CFD.FAMILYEMPLOYEENUMBER,CFD.FirstName + ' ' + CFD.MiddleName + ' ' + CFD.LastName AS CrewFamilyName,CFD.DateOfBirth,CPD.CrewNumber,CPD.FirstName + ' ' + CPD.MiddleName + ' ' + CPD.LastName AS CrewName,R.RankCode,RO.RecruitingOfficeName,  " +
                           " Rel.Relation,(Address1 + ' ' + Address2 + ' ' + Address3 + ' ' + City + ' ' + [State] + ' ' + C.CountryName + ' ' + PINCode) AS [Address], CFD.City  " +
                           " FROM CREWFAMILYDETAILS CFD  " + 
                           " INNER JOIN CrewFamilyBirthDayCardDetails CBD ON CFD.CrewFAMILYId = CBD.CrewFamilyId AND CBD.Year = year(getdate()) " + 
                           " INNER JOIN CrewPersonalDetails CPD ON CFD.CREWID=CPD.CREWID  " +
                           " LEFT JOIN Country C ON CFD.CountryId = C.CountryId  " +
                           " INNER JOIN [RANK] R ON CPD.CurrentRankId = R.RankId  AND R.OffCrew = 'O' " + 
                           " INNER JOIN RecruitingOffice RO ON CPD.RecruitmentOfficeId = RO.RecruitingOfficeId  " +
                           " INNER JOIN CrewStatus CS ON CPD.CrewStatusId = CS.CrewStatusId " +
                           " INNER JOIN RelationShip Rel ON CFD.RelationshipId = Rel.RelationShipId ";

            if (ddl_Rank_Search.SelectedIndex != 0)
            {
                WHERE_F = WHERE_F + " AND CPD.CurrentRankId = " + ddl_Rank_Search.SelectedValue.Trim();
            }
            if (ddl_CrewStatus_Search.SelectedIndex != 0)
            {
                WHERE_F = WHERE_F + " AND CPD.CrewStatusId = " + ddl_CrewStatus_Search.SelectedValue.Trim();
            }
            if (ddl_Recr_Office.SelectedIndex != 0)
            {
                WHERE_F = WHERE_F + " AND CPD.RecruitmentOfficeId = " + ddl_Recr_Office.SelectedValue.Trim();
            }
            if (ddlrelation.SelectedIndex != 0)
            {
                WHERE_F = WHERE_F + " AND CFD.RelationshipId = " + ddlrelation.SelectedValue.Trim();
            }

            SQL_F = SQL_F + WHERE_F + " ORDER BY MONTH(CFD.DateOfBirth), Day(CFD.DateOfBirth) ";

            DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(SQL_F);
            rpt_PrintLabel_Family.DataSource = dt;
            rpt_PrintLabel_Family.DataBind();

            dv_Print_Crew.Visible = false;
            dv_Print_Family.Visible = true;
        }
    }

    protected void chkCheckAll_Print_CheckedChanged(object sender, EventArgs e)
    {
        if (CrewFamily == 1)
        {
            foreach (RepeaterItem riCrew in rpt_PrintLabel_Crew.Items)
            {
                CheckBox chk = (CheckBox)riCrew.FindControl("chkSelect");
                chk.Checked = chkSelectAll_Print_Crew.Checked ? true : false;
            }
        }

        if (CrewFamily == 2)
        {
            foreach (RepeaterItem riCrewFamily in rpt_PrintLabel_Family.Items)
            {
                CheckBox chk = (CheckBox)riCrewFamily.FindControl("chkSelect");
                chk.Checked = chkSelectAll_Print_Family.Checked ? true : false;
            }
        }

    }    
    protected void btnPrintBDLabel_Click(object sender, EventArgs e)
    {
        bool check = false;
        string Ids = "";
        if (CrewFamily == 1)
        {
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

        if (CrewFamily == 2)
        {
            foreach (RepeaterItem riCrewFamily in rpt_PrintLabel_Family.Items)
            {
                CheckBox chk = (CheckBox)riCrewFamily.FindControl("chkSelect");
                if (chk.Checked)
                {
                    check = true;
                    break;
                }
            }
            if (!check)
            {
                lblMsg.Text = "Please select a family member to print label.";
                return;
            }

            foreach (RepeaterItem riCrewFamily in rpt_PrintLabel_Family.Items)
            {
                CheckBox chk = (CheckBox)riCrewFamily.FindControl("chkSelect");
                if (chk.Checked)
                {
                    Ids = Ids + chk.Attributes["CrewFamilyId"].ToString() + ",";
                }
            }
            Ids = Ids.Remove(Ids.Trim().Length - 1);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "asdsad", "printFamilyBDLabel('" + Ids + "');", true);
        }

    }
}