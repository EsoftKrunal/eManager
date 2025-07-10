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
using System.EnterpriseServices;
using System.Activities.Expressions;
using System.Globalization;
using System.Security.Cryptography;

public partial class Modules_HRD_Vacancy_AddUpdateVacancy : System.Web.UI.Page
{
    public Authority Auth;
    public string Mode;
    int Login_Id;
    int Vacancy_Id;
    public int KeyId
    {
        get { return Common.CastAsInt32(ViewState["KeyId"]); }
        set { ViewState["KeyId"] = value; }

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------

        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 1078);
        if (chpageauth <= 0)
        {
            Response.Redirect("../AuthorityError.aspx");
        }
        // CODE FOR UDATING THE AUTHORITY
        ProcessCheckAuthority Obj = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 1);
        Obj.Invoke();
        Session["Authority"] = Obj.Authority;
        Auth = (Authority)Session["Authority"];
        Login_Id = Convert.ToInt32(Session["loginid"].ToString());
        
        //--
        try
        {
            if (Request.QueryString["VacancyId"] != null)
            {
                KeyId = Convert.ToInt32(Request.QueryString["Key"].ToString());
                Session["VacancyId"] = Request.QueryString["VacancyId"].ToString();
                Vacancy_Id = Convert.ToInt32(Session["VacancyId"]);
                hdnVacancyId.Value = Vacancy_Id.ToString();
            }
            if (Request.QueryString["Mode"] != null)
            {
                Session["Mode"] = Request.QueryString["Mode"].ToString();
                Mode = Session["Mode"].ToString();
            }
           
            else
            {
                try
                {
                    hdnVacancyId.Value = Session["VacancyId"].ToString();
                    Mode = Session["Mode"].ToString();
                }
                catch
                {
                    if (Auth.isAdd)
                    {
                        Mode = "New";
                        hdnVacancyId.Value = "";
                    }
                    else
                    {
                        Response.Redirect("VacancyDetails.aspx");
                    }
                }
            }
        }
        catch
        {

        }

      
        
        if (!(IsPostBack))
        {
            BindRecruitingOffice();
            BindRankApplied();
            BindVesselDropdown(0);
            BindOwnerDropDown();
            BindRecuriterDropdown();
            BindVesselTypeDropdown();
            BindCrewNationalityDropDown();
            if (Mode == "View" && hdnVacancyId.Value.Trim() != "")
            {
                EnableDisableVacancyData(false);
                btn_Save.Enabled = false;
               btnClear.Enabled = false;
            }
            if (Mode == "Edit" && hdnVacancyId.Value.Trim() != "")
            {
                EnableDisableVacancyData(true);
                btn_Save.Enabled = true;
                btnClear.Enabled = true;
            }
            if (Mode != "New" && hdnVacancyId.Value.Trim() != "")
            {
                Show_Record(Convert.ToInt32(hdnVacancyId.Value));
                ddl_Vessel_SelectedIndexChanged(sender, e);
            }
            
        }
    }
    private void BindCrewNationalityDropDown()
    {
        String sql = "SELECT NationalityGroupId,NationalityGroupName from NationalityGroup where statusid='A'";
        DataTable dt7 = Budget.getTable(sql).Tables[0];
        this.ddcrew_nationality.DataValueField = "NationalityGroupId";
        this.ddcrew_nationality.DataTextField = "NationalityGroupName";
        this.ddcrew_nationality.DataSource = dt7;
        this.ddcrew_nationality.DataBind();

        this.ddcrew_nationality1.DataValueField = "NationalityGroupId";
        this.ddcrew_nationality1.DataTextField = "NationalityGroupName";
        this.ddcrew_nationality1.DataSource = dt7;
        this.ddcrew_nationality1.DataBind();

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

            }
        ddl_Recr_Office.DataValueField = "RecruitingOfficeId";
        ddl_Recr_Office.DataTextField = "RecruitingOfficeName";
        ddl_Recr_Office.DataSource = processgetrecruitingoffice.ResultSet;
        ddl_Recr_Office.DataBind();
        }

        private void BindRankApplied()
        {
            ProcessGetRankApplied processgetrankapplied = new ProcessGetRankApplied();
            try
            {
                processgetrankapplied.Invoke();
            }
            catch (Exception ex)
            {

            }
            ddl_Rank_Search.DataValueField = "RankId";
            ddl_Rank_Search.DataTextField = "RankName";
            ddl_Rank_Search.DataSource = processgetrankapplied.ResultSet;
            ddl_Rank_Search.DataBind();     
        }
        private void BindOwnerDropDown()
        {
            DataTable dt3 = VesselDetailsGeneral.selectDataOwnerName();
            this.ddl_Owner.DataValueField = "OwnerId";
            this.ddl_Owner.DataTextField = "OwnerShortName";
            this.ddl_Owner.DataSource = dt3;
            this.ddl_Owner.DataBind();
        }

        private void BindVesselDropdown(int ownerId)
        {
        string sql;
           if (ownerId > 0)
        {
             sql = " select 0 as VesselId,'<Select>' as VesselName   Union ALL Select VesselId, VesselName from Vessel with(nolock) where StatusId='A' and Ownerid = "+ ownerId + " ";
        }
           else
        {
            sql = " select 0 as VesselId,'<Select>' as VesselName   Union ALL Select VesselId, VesselName from Vessel with(nolock) where StatusId='A'";
        }
            
            DataTable dt = Budget.getTable(sql).Tables[0];
            if (dt.Rows.Count > 0)
            {
                this.ddl_Vessel.DataValueField = "VesselId";
                this.ddl_Vessel.DataTextField = "VesselName";
                this.ddl_Vessel.DataSource = dt;
                this.ddl_Vessel.DataBind();
            }
        }

    private void BindVesselTypeDropdown()
    {
        string sql;
        string vesseltypes = ConfigurationManager.AppSettings["VesselType"].ToString();

        sql = " select 0 as VesselTypeId,'< Select >' as VesselTypeName   Union ALL Select VesselTypeId,VesselTypeName from DBO.VesselType Where VesselTypeid in ("+ vesseltypes + ") order By VesselTypeName";
       

        DataTable dt = Budget.getTable(sql).Tables[0];
        if (dt.Rows.Count > 0)
        {
            this.ddl_VesselType.DataValueField = "VesselTypeId";
            this.ddl_VesselType.DataTextField = "VesselTypeName";
            this.ddl_VesselType.DataSource = dt;
            this.ddl_VesselType.DataBind();
        }
    }

    private void BindRecuriterDropdown()
    {
        try
        {
           
            DataTable dtRecuriter = Common.Execute_Procedures_Select_ByQuery("Select loginid As UserId,(FirstName+' '+lastname) AS EmpName from DBO.usermaster with(nolock)  where  StatusId = 'A' order by (FirstName+' '+lastname)");

            this.ddlRecruiterName.DataSource = dtRecuriter;
            this.ddlRecruiterName.DataValueField = "UserId";
            this.ddlRecruiterName.DataTextField = "EmpName";
            this.ddlRecruiterName.DataBind();
            ddlRecruiterName.Items.Insert(0, "<Select>");
            ddlRecruiterName.Items[0].Value = "0";
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
        private void Show_Record(int vacancyid)
        {
            if (vacancyid > 0)
            {
                DataTable dtVacancy = Common.Execute_Procedures_Select_ByQuery("select replace(isnull(convert(varchar,Joining_Date,106),''),' ','-') As Joining_Date, replace(isnull(convert(varchar,Proposal_Date,106),''),' ','-') As Proposal_Date, OwnerId,RankId, VesselId,Salary_M, Contract_Period,RankExperience_VesselType,RankExperience_Total,Age_Limit,IsUSVisa, IsSchengenVisa,Recruiter_Office,Recruiter_Name, replace(isnull(convert(varchar,Vacancy_IssueDt,106),''),' ','-') As Vacancy_IssueDt,Vacancy_Notes, VesselType_Id,NationalityGroupId, NationalityGroupIdRat from HRD_Vacancy with(nolock) where VacancyId = " + vacancyid + "");
                if (dtVacancy.Rows.Count > 0)
                {
                  ddl_Owner.SelectedValue = dtVacancy.Rows[0]["OwnerId"].ToString();
                  ddl_Rank_Search.SelectedValue = dtVacancy.Rows[0]["RankId"].ToString();
                if (!String.IsNullOrWhiteSpace(dtVacancy.Rows[0]["VesselId"].ToString()) && Convert.ToInt32(dtVacancy.Rows[0]["VesselId"].ToString()) > 0) 
                {
                    ddl_Vessel.SelectedValue = dtVacancy.Rows[0]["VesselId"].ToString();
                }
                else
                {
                ddl_Vessel.SelectedIndex = 0;
                }
                 
                //DateTime DateofJoining = DateTime.ParseExact(dtVacancy.Rows[0]["Joining_Date"].ToString(), "MM/dd/yyyy", CultureInfo.InvariantCulture);
                txtDoj.Text = String.Format("{0:dd-MMM-yyyy}", dtVacancy.Rows[0]["Joining_Date"].ToString());
                //DateTime DateOfProposal = DateTime.ParseExact(dtVacancy.Rows[0]["Proposal_Date"].ToString(), "MM/dd/yyyy", CultureInfo.InvariantCulture);
                txtDOP.Text = String.Format("{0:dd-MMM-yyyy}", dtVacancy.Rows[0]["Proposal_Date"].ToString());
                txtSalary.Text = Math.Round(Common.CastAsDecimal(dtVacancy.Rows[0]["Salary_M"].ToString()), 2).ToString(); 
                
                txtContractPeriod.Text = dtVacancy.Rows[0]["Contract_Period"].ToString();
                txtRankExpV.Text = dtVacancy.Rows[0]["RankExperience_VesselType"].ToString();
                txtRankExpT.Text = dtVacancy.Rows[0]["RankExperience_Total"].ToString();
                txtAgelimit.Text = dtVacancy.Rows[0]["Age_Limit"].ToString(); 
                //if (dtVacancy.Rows[0]["IsUSVisa"].ToString() == "True")
                //{
                    chk_UsVisa.Checked = Convert.ToBoolean(dtVacancy.Rows[0]["IsUSVisa"].ToString());
                //}
                //else
                //{
                //    chk_UsVisa.Checked = false;
                //}
                //if (dtVacancy.Rows[0]["IsSchengenVisa"].ToString() == "False")
                //{
                    chk_SchengenVisa.Checked = Convert.ToBoolean(dtVacancy.Rows[0]["IsSchengenVisa"].ToString());
                //}
                //else
                //{
                //    chk_SchengenVisa.Checked = false;
                //}
                ddl_Recr_Office.SelectedValue = dtVacancy.Rows[0]["Recruiter_Office"].ToString();
                ddlRecruiterName.SelectedValue = dtVacancy.Rows[0]["Recruiter_Name"].ToString();
                //DateTime IssueDate = DateTime.ParseExact(dtVacancy.Rows[0]["Vacancy_IssueDt"].ToString(), "MM/dd/yyyy", CultureInfo.InvariantCulture);
                txtIssueDate.Text = String.Format("{0:dd-MMM-yyyy}", dtVacancy.Rows[0]["Vacancy_IssueDt"].ToString());
                txtVacancyNotes.Text = dtVacancy.Rows[0]["Vacancy_Notes"].ToString();
                if (! String.IsNullOrWhiteSpace(dtVacancy.Rows[0]["VesselType_Id"].ToString()) && Convert.ToInt32(dtVacancy.Rows[0]["VesselType_Id"]) > 0)
                {
                    ddl_VesselType.SelectedValue = dtVacancy.Rows[0]["VesselType_Id"].ToString();
                }
                else
                {
                    ddl_VesselType.SelectedIndex = 0;
                }

                foreach (ListItem li in ddcrew_nationality.Items)
                {
                    if (dtVacancy.Rows[0]["NationalityGroupId"].ToString().Contains(li.Value))
                        li.Selected = true;
                }

                foreach (ListItem li in ddcrew_nationality1.Items)
                {
                    if (dtVacancy.Rows[0]["NationalityGroupIdRat"].ToString().Contains(li.Value))
                        li.Selected = true;
                }
            }
            }
        }

    protected void ddl_Vessel_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_Vessel.SelectedIndex > 0)
        {
            string natGrupname = "";
            string natGrupnameRat = "";
            DataTable dtVesselType = Common.Execute_Procedures_Select_ByQuery("Select (Select vt.VesselTypeName from VesselType vt with(nolock) where Vt.VesselTypeId = v.VesselTypeId) As VesselType, v.NationalityGroupId,v.NationalityGroupIdRat, v.VesselTypeId from Vessel v with(nolock) where v.VesselId = " + Convert.ToInt32(ddl_Vessel.SelectedValue)+"");
            if (dtVesselType.Rows.Count > 0)
            {
                ddl_VesselType.SelectedValue = dtVesselType.Rows[0]["VesselTypeId"].ToString();
                //hdnNationalityGrp.Value = dtVesselType.Rows[0]["NationalityGroupId"].ToString();
                //hdnNationalityGrpRat.Value = dtVesselType.Rows[0]["NationalityGroupIdRat"].ToString();

                //if (hdnNationalityGrp.Value != "")
                //{
                    
                //    DataTable dtNatGrp = Common.Execute_Procedures_Select_ByQuery("  SELECT NationalityGroupId,NationalityGroupName from NationalityGroup where statusid='A' and NationalityGroupId in (Select RESULT from [dbo].[CSVtoTable]('" + hdnNationalityGrp.Value  + "',','))");
                //    if (dtNatGrp.Rows.Count > 0)
                //    {
                        
                       
                //            foreach (DataRow dr in dtNatGrp.Rows)
                //            {
                //                if (string.IsNullOrWhiteSpace(natGrupname))
                //                {
                //                natGrupname = dr["NationalityGroupName"].ToString();                        
                //                }
                //                else
                //                {
                //                natGrupname = natGrupname + "," + dr["NationalityGroupName"].ToString();
                //                }
                //            }
                //    }
                   
                //}
                //if (hdnNationalityGrpRat.Value != "")
                //{
                //    DataTable dtNatGrpRat = Common.Execute_Procedures_Select_ByQuery(" SELECT NationalityGroupId,NationalityGroupName from NationalityGroup where statusid='A' and NationalityGroupId in (Select RESULT from [dbo].[CSVtoTable]('" + hdnNationalityGrpRat.Value + "',','))");
                //    if (dtNatGrpRat.Rows.Count > 0)
                //    {
                //        foreach (DataRow dr in dtNatGrpRat.Rows)
                //        {
                //            if (string.IsNullOrWhiteSpace(natGrupnameRat))
                //            {
                //                natGrupnameRat = dr["NationalityGroupName"].ToString();
                //            }
                //            else
                //            {
                //                natGrupnameRat = natGrupnameRat + "," + dr["NationalityGroupName"].ToString();
                //            }
                //        }
                //    }

                //}
                //if (! string.IsNullOrWhiteSpace(natGrupname))
                //{
                //    txtNaionalityOffice.Text = natGrupname;
                //}
                //if (!string.IsNullOrWhiteSpace(natGrupnameRat))
                //{
                //    txtNaionalityRating.Text = natGrupnameRat;
                //}
            }
        }
    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        if (ddl_VesselType.SelectedIndex == 0 && ddl_Vessel.SelectedIndex == 0)
        {
            lblMessage.Text = "Please select Vessel or Vessel type.";
            ddl_VesselType.Focus();
            return;
        }
        object doj,dop;
        if (txtDoj.Text == "")
        {
            doj = DBNull.Value;
        }
        else
        {
            doj = txtDoj.Text;
        }
        if (txtDOP.Text == "")
        {
            dop = DBNull.Value;
        }
        else
        {
            dop = txtDOP.Text;
        }
        string NationalityGroupId = "";
        foreach (ListItem li in ddcrew_nationality.Items)
        {
            if (li.Selected)
                NationalityGroupId = NationalityGroupId + "," + li.Value;
        }
        if (NationalityGroupId.StartsWith(","))
        {
            NationalityGroupId = NationalityGroupId.Substring(1);
        }
        string NationalityGroupIdRat = "";
        foreach (ListItem li in ddcrew_nationality1.Items)
        {
            if (li.Selected)
                NationalityGroupIdRat = NationalityGroupIdRat + "," + li.Value;
        }
        if (NationalityGroupIdRat.StartsWith(","))
        {
            NationalityGroupIdRat = NationalityGroupIdRat.Substring(1);
        }
        try
        {
            Common.Set_Procedures("InsertUpdate_HRD_Vacancy");
            Common.Set_ParameterLength(21);
            Common.Set_Parameters(
                new MyParameter("@vacancyId", Convert.ToInt32(hdnVacancyId.Value)),
                new MyParameter("@ownerId", Convert.ToInt32(ddl_Owner.SelectedValue)),
                new MyParameter("@vesselId", Convert.ToInt32(ddl_Vessel.SelectedValue)),
                new MyParameter("@RankId", Convert.ToInt32(ddl_Rank_Search.SelectedValue)),
                new MyParameter("@join_Date", doj),
                new MyParameter("@Proposal_Date", dop),
                new MyParameter("@salary_M", txtSalary.Text.Trim()),
                new MyParameter("@contract_period", txtContractPeriod.Text.Trim()),
                new MyParameter("@RankExp_VT", txtRankExpV.Text.Trim()),
                new MyParameter("@RankExp_Total", txtRankExpT.Text.Trim()),
                new MyParameter("@Age_Limit", txtAgelimit.Text.Trim()),
                 new MyParameter("@IsUsVisa", chk_UsVisa.Checked ? 1:0),
                 new MyParameter("@IsSchengenVisa", chk_SchengenVisa.Checked ? 1 : 0),
                 new MyParameter("@Recruiter_Office", ddl_Recr_Office.SelectedValue),
                 new MyParameter("@Recruiter_Name", ddlRecruiterName.SelectedValue),
                new MyParameter("@Vacancy_Notes", txtVacancyNotes.Text),
                new MyParameter("@CreatedBy", Login_Id),
                new MyParameter("@vacancy_Status", ddlVStatus.SelectedValue),
                new MyParameter("@VesselType_Id", ddl_VesselType.SelectedValue),
                new MyParameter("@NationalityGroupId", NationalityGroupId),
                new MyParameter("@NationalityGroupIdRat", NationalityGroupIdRat));
            DataSet Ds = new DataSet();
            Boolean res = false;
            res = Common.Execute_Procedures_IUD(Ds);
            if (res == true)
            {
                int c = Common.CastAsInt32(Ds.Tables[0].Rows[0][0]);
                if (c > 0)
                {
                    lblMessage.Text = "Record saved successfully.";
                    //ClearTrackingControl();
                    //BindTrackingTaskList();
                    //UpTask.Update();
                    btnClear_Click(sender, e);
                }
                else
                {
                    lblMessage.Text = "Record could not saved." + Common.ErrMsg.ToString();
                }
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "dialog", "window.opener.RefereshPage();window.close();", true);
            }
            else
            {
                lblMessage.Text = "Record could not saved." + Common.ErrMsg.ToString();
            }
        }
        catch(Exception ex)
        {
            lblMessage.Text = "Data save error : " + ex.Message.ToString();
        }
    }

    protected void ddl_Owner_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_Owner.SelectedIndex > 0)
        {
            BindVesselDropdown(Convert.ToInt32(ddl_Owner.SelectedValue));
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        ddlVStatus.SelectedIndex = 0;
        ddl_Owner.SelectedIndex = 0;
        ddl_Rank_Search.SelectedIndex = 0;
        ddl_Recr_Office.SelectedIndex = 0;
        ddl_Vessel.SelectedIndex = 0;
        txtAgelimit.Text = "";
        txtContractPeriod.Text = "";
        txtDoj.Text = "";
        txtDOP.Text = "";
        txtIssueDate.Text = "";
        ddcrew_nationality.ClearSelection();
        ddcrew_nationality1.ClearSelection();
        txtRankExpT.Text = "";
        txtRankExpV.Text = "";
        txtVacancyNotes.Text = "";
        ddl_VesselType.SelectedIndex = 0;
       
        txtSalary.Text = "";
        chk_SchengenVisa.Checked = false;
        chk_UsVisa.Checked = false;
        ddlRecruiterName.SelectedIndex = 0;
    }

    protected void EnableDisableVacancyData(bool Enable)
    {
        ddlVStatus.Enabled = Enable;
        ddl_Owner.Enabled = Enable;
        ddl_Rank_Search.Enabled = Enable;
        ddl_Recr_Office.Enabled = Enable;
        ddl_Vessel.Enabled = Enable;
        txtAgelimit.Enabled = Enable;
        txtContractPeriod.Enabled = Enable;
        txtDoj.Enabled = Enable;
        txtDOP.Enabled = Enable;
        txtIssueDate.Enabled = Enable;
        ddcrew_nationality.Enabled = Enable;
        ddcrew_nationality1.Enabled = Enable;
        txtRankExpT.Enabled = Enable;
        txtRankExpV.Enabled = Enable;
        txtVacancyNotes.Enabled = Enable;
        ddl_VesselType.Enabled = Enable;
        
        txtSalary.Enabled = Enable;
        chk_SchengenVisa.Enabled = Enable;
        chk_UsVisa.Enabled = Enable;
        ddlRecruiterName.Enabled = Enable;
    }



    protected void btnClose_Click(object sender, EventArgs e)
    {
        if (KeyId == 1)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "", "refreshClose();", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "", "refresh();", true);
        }
       
    }
}
    