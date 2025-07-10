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

public partial class CRMActivities_CRM_WelcomeHome : System.Web.UI.Page
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

            btnSearch_Click(sender, e);
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
        string SQL = "";
        string WHERE = " WHERE (DATEADD(dd, 0, DATEDIFF(dd, 0, SignOffDate)) <= DATEADD(dd, 0, DATEDIFF(dd, 0, GETDATE())) AND DATEADD(dd, 0, DATEDIFF(dd, 0, SignOffDate)) >= DATEADD(dd,-15,DATEADD(dd, 0, DATEDIFF(dd, 0, GETDATE())))) AND ISNULL(IsFamilyMember,'N') = 'N' AND CPD.CrewStatusId = 2 ";

        SQL = "SELECT CrewId,CrewNumber, FirstName + ' ' + MiddleName + ' ' + LastName AS CrewName,SignOffDate, IsFamilyMember,R.RankCode,CS.CrewStatusName,RO.RecruitingOfficeName, CASE WHEN CPD.CrewStatusId = 2 THEN (SELECT VesselCode FROM Vessel WHERE [VesselId]= CPD.LastVesselId) WHEN CPD.CrewStatusId = 3 THEN (SELECT VesselCode FROM Vessel WHERE [VesselId]= CPD.CurrentVesselId) END As Curr_Last_Vsl,(SELECT SentOn FROM CrewWelcomeCardDetails WHERE CrewId = CPD.CrewId  AND [Year] = year(getdate())) AS CardSent, (SELECT FirstName + ' ' + LastName FROM UserLogin WHERE LoginId = (SELECT [UpdatedBy] FROM CrewWelcomeCardDetails WHERE CrewId = CPD.CrewId  AND [Year] = year(getdate()))) As UpdatedBy, (SELECT [UpdatedOn] FROM CrewWelcomeCardDetails WHERE CrewId = CPD.CrewId  AND [Year] = year(getdate())) As UpdatedOn, (SELECT top 1 City FROM CrewContactDetails WHERE CrewId = CPD.CrewId AND AddressType='C') As City FROM CrewPersonalDetails CPD " +
              "INNER JOIN [RANK] R ON CPD.CurrentRankId = R.RankId " +
              "INNER JOIN RecruitingOffice RO ON CPD.RecruitmentOfficeId = RO.RecruitingOfficeId " +
              "INNER JOIN CrewStatus CS ON CPD.CrewStatusId = CS.CrewStatusId ";

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
        rpt_Crew.DataSource = dt;
        rpt_Crew.DataBind();

        lblRcount1.Text = "Total Records : " + dt.Rows.Count;

    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("CRMHome.aspx");
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

                string SQL = "INSERT INTO CrewWelcomeCardDetails VALUES(" + CrewId + ", " + DateTime.Parse(txtSentDate.Text.Trim()).Year + " , '" + txtSentDate.Text.Trim() + "', " + Session["loginid"].ToString() + ", getdate())";
                Common.Execute_Procedures_Select_ByQueryCMS(SQL);
            }
        }


        lblMsg.Text = "Welcome card delivery updated successfully.";
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
       ScriptManager.RegisterStartupScript(this, this.GetType(), "asdsad", "openprintLabel();", true);
    }
}