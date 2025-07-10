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

public partial class CRMActivities_CRM_BirthDays : System.Web.UI.Page
{
    public int Days
    {
        set { ViewState["Days"] = value; }
        get { return Common.CastAsInt32(ViewState["Days"]); }
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
            BindCrewStatusDropDown();
            BindRankDropDown();
            BindRecruitingOffice();
            BindRelationDropDown();

            rdoCrew_CheckedChanged(sender, e);
            btnSearchBD_Click(sender, e);
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

    protected void btnSearchBD_Click(object sender, EventArgs e)
    {
        if (rdoCrew.Checked)
        {
            dv_CrewFamily.Visible = false;
            dv_Crew.Visible = true;

            string SQL = "";
            string WHERE = " WHERE ((DATEADD( year, YEAR(getdate()) - YEAR(DateOfBirth), DateOfBirth) BETWEEN DATEADD(dd, 0, DATEDIFF(dd, 0, GETDATE())) AND DATEADD(d," + Days + ",DATEADD(dd, 0, DATEDIFF(dd, 0, GETDATE()))))) ";

            SQL = "SELECT CrewId,CrewNumber, FirstName + ' ' + MiddleName + ' ' + LastName AS CrewName,DateOfBirth, IsFamilyMember,R.RankCode,CS.CrewStatusName,RO.RecruitingOfficeName, CASE WHEN CPD.CrewStatusId = 2 THEN (SELECT VesselCode FROM Vessel WHERE [VesselId]= CPD.LastVesselId) WHEN CPD.CrewStatusId = 3 THEN (SELECT VesselCode FROM Vessel WHERE [VesselId]= CPD.CurrentVesselId) END As Curr_Last_Vsl,(SELECT SentOn FROM CrewBirthDayCardDetails WHERE CrewId = CPD.CrewId AND [Year] = year(getdate())) AS CardSent, (SELECT FirstName + ' ' + LastName FROM UserLogin WHERE LoginId = (SELECT [UpdatedBy] FROM CrewBirthDayCardDetails WHERE CrewId = CPD.CrewId AND [Year] = year(getdate()))) As UpdatedBy, (SELECT [UpdatedOn] FROM CrewBirthDayCardDetails WHERE CrewId = CPD.CrewId AND [Year] = year(getdate())) As UpdatedOn, (SELECT top 1 City FROM CrewContactDetails WHERE CrewId = CPD.CrewId AND AddressType='C') As City FROM CrewPersonalDetails CPD " +
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
            if (ddl_Recr_Office.SelectedIndex != 0)
            {
                WHERE = WHERE + " AND CPD.RecruitmentOfficeId = " + ddl_Recr_Office.SelectedValue.Trim();
            }

            SQL = SQL + WHERE + " ORDER BY MONTH(CPD.DateOfBirth), Day(CPD.DateOfBirth)";
            DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(SQL);
            rpt_Crew.DataSource = dt;
            rpt_Crew.DataBind();

            lblRcount1.Text = "Total Records : " + dt.Rows.Count;
        }

        if (rdoFamily.Checked)
        {
            dv_CrewFamily.Visible = true;
            dv_Crew.Visible = false;

            string WHERE_F = " WHERE ((DATEADD( year, YEAR(getdate()) - YEAR(CFD.DateOfBirth), CFD.DateOfBirth) BETWEEN DATEADD(dd, 0, DATEDIFF(dd, 0, GETDATE())) AND DATEADD(d," + Days + ",DATEADD(dd, 0, DATEDIFF(dd, 0, GETDATE()))))) ";
           
            string SQL_F =  "SELECT CFD.CrewFAMILYId,CFD.FAMILYEMPLOYEENUMBER,CFD.FirstName + ' ' + CFD.MiddleName + ' ' + CFD.LastName AS CrewFamilyName,CFD.DateOfBirth,CPD.CrewNumber,CPD.FirstName + ' ' + CPD.MiddleName + ' ' + CPD.LastName AS CrewName,R.RankCode,CS.CrewStatusName,RO.RecruitingOfficeName, (SELECT VesselCode FROM Vessel WHERE [VesselId]= CPD.LastVesselId) AS LastVsl, (SELECT VesselCode FROM Vessel WHERE [VesselId]= CPD.CurrentVesselId) As CurrentVsl  " +
                            ",Rel.Relation,(SELECT SentOn FROM CrewFamilyBirthDayCardDetails WHERE CrewFamilyId = CFD.CrewFAMILYId AND [Year] = year(getdate())) AS CardSent, (SELECT FirstName + ' ' + LastName FROM UserLogin WHERE LoginId = (SELECT [UpdatedBy] FROM CrewFamilyBirthDayCardDetails WHERE CrewFamilyId = CFD.CrewFAMILYId AND [Year] = year(getdate()))) As UpdatedBy, (SELECT [UpdatedOn] FROM CrewFamilyBirthDayCardDetails WHERE CrewFamilyId = CFD.CrewFAMILYId AND [Year] = year(getdate())) As UpdatedOn, CFD.City " +
                            "FROM CREWFAMILYDETAILS CFD  " +
                            "INNER JOIN CrewPersonalDetails CPD ON CFD.CREWID=CPD.CREWID  " +
                            "INNER JOIN [RANK] R ON CPD.CurrentRankId = R.RankId AND R.OffCrew = 'O' " +
                            "INNER JOIN RecruitingOffice RO ON CPD.RecruitmentOfficeId = RO.RecruitingOfficeId " +
                            "INNER JOIN CrewStatus CS ON CPD.CrewStatusId = CS.CrewStatusId AND CPD.CrewStatusId IN (2,3) " +
                            "INNER JOIN RelationShip Rel ON CFD.RelationshipId = Rel.RelationShipId ";
            
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
            rpt_CrewFamily.DataSource = dt;
            rpt_CrewFamily.DataBind();


            lblRcount1.Text = "Total Records : " + dt.Rows.Count;
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("CRMHome.aspx");
    }
    protected void rdoCrew_CheckedChanged(object sender, EventArgs e)
    {
        if (rdoCrew.Checked)
        {
            ddlrelation.SelectedIndex = 0;
            ddlrelation.Enabled = false;            
        }
        else
        {
            ddlrelation.SelectedIndex = 0;
            ddlrelation.Enabled = true;
        }

        btnSearchBD_Click(sender, e);
    }
    protected void btnSendBD_Click(object sender, EventArgs e)
    {
        bool check = false;
        if(rdoCrew.Checked)
        {
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
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "asdsad", "alert('Please select a crew to send birthday card.')", true);
                lblMsg.Text = "Please select a crew to update card delivery.";
                return;
            }
        }

        if (rdoFamily.Checked)
        {
            check = false;
            foreach (RepeaterItem riCrewFamily in rpt_CrewFamily.Items)
            {
                CheckBox chk = (CheckBox)riCrewFamily.FindControl("chkSent");
                if (chk.Checked)
                {
                    check = true;
                    break;
                }
            }

            if (!check)
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "asdsad", "alert('Please select a crew family member to send birthday card.')", true);
                lblMsg.Text = "Please select a crew family member to update card delivery.";
                return;
            }
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

        if (rdoCrew.Checked)
        {
            foreach (RepeaterItem riCrew in rpt_Crew.Items)
            {
                CheckBox chk = (CheckBox)riCrew.FindControl("chkSent");                
                if (chk.Checked)
                {
                    int CrewId = Common.CastAsInt32(chk.Attributes["CrewId"].ToString());

                    string SQL = "INSERT INTO CrewBirthDayCardDetails VALUES(" + CrewId + ", " + DateTime.Parse(txtSentDate.Text.Trim()).Year + " , '" + txtSentDate.Text.Trim() + "', " + Session["loginid"].ToString() + ", getdate())";
                    Common.Execute_Procedures_Select_ByQueryCMS(SQL);
                }
            }            
        }

        if (rdoFamily.Checked)
        {
            
            foreach (RepeaterItem riCrewFamily in rpt_CrewFamily.Items)
            {
                CheckBox chk = (CheckBox)riCrewFamily.FindControl("chkSent");
                if (chk.Checked)
                {
                    int CrewFamilyId = Common.CastAsInt32(chk.Attributes["CrewFamilyId"].ToString());

                    string SQL = "INSERT INTO CrewFamilyBirthDayCardDetails VALUES(" + CrewFamilyId + ", " + DateTime.Parse(txtSentDate.Text.Trim()).Year + " , '" + txtSentDate.Text.Trim() + "', " + Session["loginid"].ToString() + ", getdate())";
                    Common.Execute_Procedures_Select_ByQueryCMS(SQL); 
                }
            }
        }

        //ScriptManager.RegisterStartupScript(this, this.GetType(), "asdsad", "alert('Birthday card sent successfully.')", true);
        lblMsg.Text = "Birthday card delivery updated successfully.";
        btn_Close_Click(sender, e);

    }
    protected void btn_Close_Click(object sender, EventArgs e)
    {
        btnSearchBD_Click(sender, e);
        txtSentDate.Text = "";
        dv_SendCard.Visible = false;
    }
    protected void chkCheckAll_CheckedChanged(object sender, EventArgs e)
    {
        if (rdoCrew.Checked)
        {
            foreach (RepeaterItem riCrew in rpt_Crew.Items)
            {
                CheckBox chk = (CheckBox)riCrew.FindControl("chkSent");
                chk.Checked = chkCheckAll_Crew.Checked ? true : false;
            }
        }

        if (rdoFamily.Checked)
        {
            foreach (RepeaterItem riCrewFamily in rpt_CrewFamily.Items)
            {
                CheckBox chk = (CheckBox)riCrewFamily.FindControl("chkSent");
                chk.Checked = chkCheckAll_Family.Checked ? true : false;
            }
        }
    } 
    protected void btnPrintLabel_Click(object sender, EventArgs e)
    {
        int CF = (rdoCrew.Checked ? 1 : 2);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "asdsad", "openprintLabel('" + CF + "','" + Days + "');", true);
    }
}