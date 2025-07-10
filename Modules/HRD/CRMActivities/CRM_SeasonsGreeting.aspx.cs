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

public partial class CRMActivities_CRM_SeasonsGreeting : System.Web.UI.Page
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
            ShowDetails();
            BindCrewStatusDropDown();
            BindRankDropDown();
            //BindRecruitingOffice();
            //ddl_Recr_Office.SelectedValue = Request.QueryString["OfficeId"].ToString();
            //ddl_Recr_Office.Enabled = false;

            btnSearch_Click(sender, e);
        }
    }

    public void ShowDetails()
    {
        DataTable dt1 = Common.Execute_Procedures_Select_ByQueryCMS("SELECT HolidayReason FROM HR_HolidayMaster WHERE HolidayId = " + HolidayId );
        lblHoliday.Text = dt1.Rows[0]["HolidayReason"].ToString();

        DataTable dt2 = Common.Execute_Procedures_Select_ByQueryCMS("SELECT RecruitingOfficeName  FROM RecruitingOffice WHERE RecruitingOfficeId = " + RecOfficeId);
        lblRecOffice.Text = dt2.Rows[0]["RecruitingOfficeName"].ToString();

    }
    private void BindRecruitingOffice()
    {
        //ProcessGetRecruitingOffice processgetrecruitingoffice = new ProcessGetRecruitingOffice();
        //try
        //{
        //    processgetrecruitingoffice.Invoke();
        //}
        //catch (Exception ex)
        //{
        //    //Response.Write(ex.Message.ToString());
        //}
        //ddl_Recr_Office.DataValueField = "RecruitingOfficeId";
        //ddl_Recr_Office.DataTextField = "RecruitingOfficeName";
        //ddl_Recr_Office.DataSource = processgetrecruitingoffice.ResultSet;
        //ddl_Recr_Office.DataBind();
        //ddl_Recr_Office.Items.RemoveAt(0);
        //ddl_Recr_Office.Items.Insert(0, new ListItem(" All ", ""));
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
        string SQL = "";
        string WHERE = (RecOfficeId != 0 ? " WHERE RecruitmentOfficeId = " + RecOfficeId + " " : " WHERE RecruitmentOfficeId <> 0  ") + " AND ISNULL(IsFamilyMember,'N') = 'N'";

        SQL = "SELECT CrewId,CrewNumber, FirstName + ' ' + MiddleName + ' ' + LastName AS CrewName,DateOfBirth, IsFamilyMember,R.RankCode,CS.CrewStatusName,RO.RecruitingOfficeName, CASE WHEN CPD.CrewStatusId = 2 THEN (SELECT VesselCode FROM Vessel WHERE [VesselId]= CPD.LastVesselId) WHEN CPD.CrewStatusId = 3 THEN (SELECT VesselCode FROM Vessel WHERE [VesselId]= CPD.CurrentVesselId) END As Curr_Last_Vsl,(SELECT SentOn FROM CrewSeasonsGreetingCardDetails WHERE CrewId = CPD.CrewId AND HolidayId = " + HolidayId + " AND [Year] = year(getdate())) AS CardSent, (SELECT FirstName + ' ' + LastName FROM UserLogin WHERE LoginId = (SELECT [UpdatedBy] FROM CrewSeasonsGreetingCardDetails WHERE CrewId = CPD.CrewId AND HolidayId= " + HolidayId + " AND [Year] = year(getdate()))) As UpdatedBy, (SELECT [UpdatedOn] FROM CrewSeasonsGreetingCardDetails WHERE CrewId = CPD.CrewId AND HolidayId = " + HolidayId + " AND [Year] = year(getdate())) As UpdatedOn, (SELECT top 1 City FROM CrewContactDetails WHERE CrewId = CPD.CrewId AND AddressType='C') As City FROM CrewPersonalDetails CPD " +
                "INNER JOIN [RANK] R ON CPD.CurrentRankId = R.RankId " +
                "INNER JOIN RecruitingOffice RO ON CPD.RecruitmentOfficeId = RO.RecruitingOfficeId " +
                "INNER JOIN CrewStatus CS ON CPD.CrewStatusId = CS.CrewStatusId AND CPD.CrewStatusId IN (2,3) ";

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
        rpt_Crew.DataSource = dt;
        rpt_Crew.DataBind();

        lblRcount1.Text = "Total Records : " + dt.Rows.Count;

    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("CRM_SeasonsGreetingHome.aspx");
    }

    protected void btnSendCard_Click(object sender, EventArgs e)
    {
        bool check = false;         
        foreach (RepeaterItem riCrew in rpt_Crew.Items)
        {
            CheckBox chk = (CheckBox)riCrew.FindControl("chkSent");
            if (chk.Checked)
            {
                check = true;
                break;
            }
        }

        if (!check)
        {             
            lblMsg.Text = "Please select a crew to update card delivery.";
            return;
        }
        
        dv_SendCard.Visible = true;

    }

    protected void btnSend_BDCard_Click(object sender, EventArgs e)
    {
        if (DateTime.Parse(txtSentDate.Text.Trim()) > DateTime.Today.Date)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "asdsad", "alert('Card delivery date can not be more than today.')", true);
            txtSentDate.Focus();
            return;
        }

        
            foreach (RepeaterItem riCrew in rpt_Crew.Items)
            {
                CheckBox chk = (CheckBox)riCrew.FindControl("chkSent");
                if (chk.Checked)
                {
                    int CrewId = Common.CastAsInt32(chk.Attributes["CrewId"].ToString());

                    string SQL = "INSERT INTO CrewSeasonsGreetingCardDetails VALUES(" + CrewId + ", " + HolidayId + ", " + DateTime.Parse(txtSentDate.Text.Trim()).Year + " , '" + txtSentDate.Text.Trim() + "', " + Session["loginid"].ToString() + ", getdate())";
                    Common.Execute_Procedures_Select_ByQueryCMS(SQL);
                }
            }
        

        lblMsg.Text = "Seasons greeting card delivery updated successfully.";
        btn_Close_Click(sender, e);

    }
    protected void btn_Close_Click(object sender, EventArgs e)
    {
        btnSearch_Click(sender, e);
        txtSentDate.Text = "";
        dv_SendCard.Visible = false;
    }
    protected void chkCheckAll_CheckedChanged(object sender, EventArgs e)
    {        
        foreach (RepeaterItem riCrew in rpt_Crew.Items)
        {
            CheckBox chk = (CheckBox)riCrew.FindControl("chkSent");
            chk.Checked = chkCheckAll_Crew.Checked ? true : false;
        }
    }
    protected void btnPrintLabel_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "asdsad", "openprintLabel('" + HolidayId + "','" + RecOfficeId + "');", true);
    }
}