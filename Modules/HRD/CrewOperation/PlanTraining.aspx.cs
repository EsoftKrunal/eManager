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
using ShipSoft.CrewManager.BusinessObjects;
using ShipSoft.CrewManager.BusinessLogicLayer;
using ShipSoft.CrewManager.Operational;

public partial class PlanTraining : System.Web.UI.Page
{
    AuthenticationManager Auth;
    public int TRID
    {
        get { return Common.CastAsInt32(ViewState["TRID"]); }
        set { ViewState["TRID"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 291);
        if (chpageauth <= 0)
        {
            Response.Redirect("Dummy_Training1.aspx");
        }
        //*******************
        Auth = new AuthenticationManager(291, Convert.ToInt32(Session["loginid"].ToString()), ObjectType.Page);
        btnUpdatePlan.Visible = Auth.IsAdd;
        
        if (!Page.IsPostBack)
        {
            ddlMonth.SelectedValue = DateTime.Now.Month.ToString();
            ddlYear.SelectedValue = DateTime.Now.Year.ToString();

            BindNationalityDropDown();
            BindVessel();
            BindYear();
            BindCrewStatus();
            BindRecruitmentOffice();
            BindInstitute();
            BindRank();
        }
    }
    // --------- Event
    protected void btn_show_Click(object sender, EventArgs e)
    {
            int Flag = 0;

            if (ddl_Vessel.SelectedIndex != 0)Flag = Flag + 1;
            if (ddlRecruitmentOff.SelectedIndex != 0) Flag = Flag + 1;
            if (ddlMonth.SelectedIndex != 0) Flag = Flag + 1;
            if (ddlRank.SelectedIndex != 0)Flag = Flag + 1;
            if (ddlSource.SelectedIndex != 0) Flag = Flag + 1;

            if (chkOverDue.Checked) Flag = Flag + 1;
            if (chkRec.Checked) Flag = Flag + 1;

            if (txtTraining.Text.Trim() != "") Flag = Flag + 1;
            if (txt_MemberId.Text.Trim()!="") Flag = Flag + 1;
            if (txtPlanedFor.Text.Trim() != "") Flag = Flag + 1;
            if (txtTraining.Text.Trim() != "")Flag = Flag + 1;

            if (Flag <= 0)
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "", "alert('At least one filter must be selected.')", true);
                return;
            }

        BindPlaneTraining();
    }
    protected void btnUpdatePlan_OnClick(object sender, EventArgs e)
    {
        foreach(RepeaterItem Itm in rptPlanTraining.Items )
        {
            CheckBox chk = (CheckBox)Itm.FindControl("chk");
            if (chk.Checked)
            {
                int PKId=Common.CastAsInt32(((HiddenField)Itm.FindControl("hfdPKId")).Value);
                int CrewId = Common.CastAsInt32(((HiddenField)Itm.FindControl("hfdCrewId")).Value);
                int TId = Common.CastAsInt32(((HiddenField)Itm.FindControl("hfdTID")).Value);

                string SOURCE = ((Label)Itm.FindControl("lblSOURCE")).Text.Trim();
                string DueDate = ((Label)Itm.FindControl("lblDueDate")).Text.Trim();
                
                if (PKId > 0)// ------------ update
                {
                    string sql = " UPDATE CrewTrainingRequirement SET PlannedFor='" + txtPlanedFor.Text + "',PlannedInstitute=" + ddlInstitute.SelectedValue + ",PlannedBy=" + Common.CastAsInt32(Session["loginid"]) + ",PlannedOn='" + DateTime.Now.ToString("dd-MMM-yyyy") + "' WHERE TrainingRequirementID=" + PKId.ToString() + "";
                    Common.Execute_Procedures_Select_ByQueryCMS(sql);
                }
                else
                {
                    string sql = "INSERT into CrewTrainingRequirement(CrewId,TrainingId,Remark,CreatedBy,CreatedOn,N_DueDate,N_CrewTrainingStatus,N_CrewAppraisalId,N_CrewVerified,SOURCE,PlannedFor,PlannedInstitute,PlannedBy,PlannedOn) " +
                                 "values (" + CrewId.ToString() + "," + TId.ToString() + ",''," + Session["loginid"].ToString() + ",'" + DateTime.Today.ToString("dd-MMM-yyyy") + "','" + DueDate + "','O',0,'N',2,'" + txtPlanedFor.Text + "'," + ddlInstitute.SelectedValue + "," + Common.CastAsInt32(Session["loginid"]) + ",'" + DateTime.Now.ToString("dd-MMM-yyyy") + "')";
                    Common.Execute_Procedures_Select_ByQueryCMS(sql);
                }
            }
        }
        BindPlaneTraining();
    }
    protected void chkAll_OnCheckedChanged(object sender, EventArgs e)
    {
        
        foreach (RepeaterItem Itm in rptPlanTraining.Items)
        {
            CheckBox chk = (CheckBox)Itm.FindControl("chk");
            chk.Checked = chkAll.Checked;
        }
    }
    protected void txtDaysText_Changed(object sender, EventArgs e)
    {
        if (txtPlanedForFilter.Text.Trim() != "")
        {
            chkOverDue.Checked = false;
            chkOverDue.Enabled = false;
        }
        else
        {
            chkOverDue.Enabled = true;
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txt_MemberId.Text = "";
        txtName.Text = "";
        txtTraining.Text = "";
        txtPlanedFor.Text = "";

        ddl_Nationality.SelectedIndex = 0;
        ddlMonth.SelectedIndex = 0;
        ddlYear.SelectedIndex = 0;
        ddl_Vessel.SelectedIndex = 0;
        ddlRecruitmentOff.SelectedIndex = 0;
        ddlRank.SelectedIndex = 0;
        ddlCrewStatus.SelectedIndex = 0;
        ddlSource.SelectedIndex = 0;

        chkRec.Checked = false;
        chkOverDue.Checked = false;
        
    }
    protected void chkOverDue_OnCheckedChanged(object sender, EventArgs e)
    {
        if (chkOverDue.Checked)
        {
            txtPlanedForFilter.Text = "";
            txtPlanedForFilter.Enabled = false;
        }
        else
        {
            txtPlanedForFilter.Text = "";
            txtPlanedForFilter.Enabled = true;
        }
    }
    protected void btnDeleteTraining_Click(object sender, EventArgs e)
    {
        int PK=Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        if (PK > 0)
        {
            Common.Execute_Procedures_Select_ByQueryCMS("DELETE FROM CREWTRAININGREQUIREMENT WHERE TrainingRequirementID=" + PK.ToString());
            BindPlaneTraining();
        }
    }
    
    
    // --------- Function
    public void BindPlaneTraining()
    {
        //DataTable dtRes=Common.Execute_Procedures_Select_ByQueryCMS("exec dbo.sp_getTrainingSearchForPlanning");
        string filter = " 1=1 ";

        Common.Set_Procedures("sp_getTrainingSearchForPlanning");
        Common.Set_ParameterLength(14);
        Common.Set_Parameters(
             new MyParameter("@CrewNumber",txt_MemberId.Text.Trim()),
             new MyParameter("@CrewName",txtName.Text.Trim()),
             new MyParameter("@Nationality",Common.CastAsInt32(ddl_Nationality.SelectedValue)),
             new MyParameter("@DueInMonth", Common.CastAsInt32(ddlMonth.SelectedValue)),
             new MyParameter("@DueInYear", Common.CastAsInt32(ddlYear.SelectedValue)),
             new MyParameter("@VesselID", Common.CastAsInt32(ddl_Vessel.SelectedValue)),
             new MyParameter("@RankID", ddlRank.SelectedValue),
             new MyParameter("@RecruitingOffice", Common.CastAsInt32(ddlRecruitmentOff.SelectedValue)),
             new MyParameter("@TrainingName",txtTraining.Text.Trim()),
             new MyParameter("@CrewStatus", Common.CastAsInt32(ddlCrewStatus.SelectedValue)),
             new MyParameter("@PlandedForDays", txtPlanedForFilter.Text),
             new MyParameter("@OverDueOnly",(chkOverDue.Checked)?1:0),
             new MyParameter("@Source",ddlSource.SelectedValue),
             new MyParameter("@Promo",(chkRec.Checked)?1:0)
             
            );
        DataSet dtRes = Common.Execute_Procedures_Select_CMS();

        rptPlanTraining.DataSource = dtRes;
        rptPlanTraining.DataBind();
        if(dtRes.Tables.Count>0)
            lblTotRow.Text ="Total ( " +dtRes.Tables[0].Rows.Count.ToString()+" ) Records Found.";
        //if (txt_MemberId.Text != "") filter = filter + " And CrewNumber='" + txt_MemberId.Text.Trim() + "'";
        //if (txtName.Text != "") filter = filter + " And CREWNAME like '%" + txtName.Text.Trim() + "%'";

        //if (ddl_Nationality.SelectedIndex > 0) filter = filter + " And NATIONALITYID=" + ddl_Nationality.SelectedValue;
        //if (ddl_Vessel.SelectedIndex > 0) filter = filter + " And CurrentVesselID=" + ddl_Vessel.SelectedValue;

        //if (txtTraining.Text != "") filter = filter + " And TrainingName like '" + txtTraining.Text.Trim() + "%'";


        //if (ddlCrewStatus.SelectedIndex > 0) filter = filter + " And CrewStatusID=" + ddlCrewStatus.SelectedValue;
        //if (ddlRecruitmentOff.SelectedIndex > 0) filter = filter + " And RecruitmentOfficeID=" + ddlRecruitmentOff.SelectedValue;
        
        //if (ddlMonth.SelectedIndex > 0)
        //{
        //    DateTime Stdt = DateTime.Parse("01-" + ddlMonth.SelectedItem.Text + "-" + ddlYear.SelectedValue);
        //    filter = filter + " And NEXTDUE>=#" + Stdt.ToString("dd-MMM-yyyy") + "# And NEXTDUE<#" + Stdt.AddMonths(1).ToString("dd-MMM-yyyy") +"#";
        //} 
        //if (chkRec.Checked) filter = filter + " And PROMO > 0";

        //if (chkOverDue.Checked)
        //    filter = filter + " And NEXTDUE<#" + DateTime.Today.ToString("dd-MMM-yyyy") + "#";
        //else
        //{
        //    if (txtPlanedForFilter.Text.Trim()!="")
        //        filter = filter + " And NEXTDUE<#" + DateTime.Today.AddDays(Common.CastAsInt32(txtPlanedForFilter.Text.Trim())).ToString("dd-MMM-yyyy") + "#";
        //}
        //if (ddlSource.SelectedIndex > 0) filter = filter + " And Source='" + ddlSource.SelectedItem.Text + "'";
        //DataView dv = dtRes.DefaultView;
        //dv.RowFilter = filter;
        //DataTable dt = dv.ToTable(); 
        //rptPlanTraining.DataSource = dt;
        //rptPlanTraining.DataBind();
    }
    private void BindVessel()
    {
        DataSet ds = cls_SearchReliever.getMasterData("Vessel", "VesselId", "VesselName", Convert.ToInt32(Session["loginid"].ToString()));
        ddl_Vessel.DataValueField = "VesselId";
        ddl_Vessel.DataTextField = "VesselName";
        ddl_Vessel.DataSource = ds;
        ddl_Vessel.DataBind();
        ddl_Vessel.Items.Insert(0, new ListItem("< Select >", ""));
    }
    private void BindNationalityDropDown()
    {
        ProcessSelectNationality obj = new ProcessSelectNationality();
        obj.Invoke();
        ddl_Nationality.DataSource = obj.ResultSet.Tables[0];
        ddl_Nationality.DataTextField = "CountryName";
        ddl_Nationality.DataValueField = "CountryId";
        ddl_Nationality.DataBind();
        ddl_Nationality.Items.RemoveAt(0);
        ddl_Nationality.Items.Insert(0, new ListItem("< All >", ""));
    }
    public void BindYear()
    {
        for (int i = -5; i <= 5; i++)
        {
            //ddlYear.Items.Add(new System.Web.UI.WebControls.ListItem((DateTime.Now.Year+i).ToString()));
            ddlYear.Items.Insert(0, new System.Web.UI.WebControls.ListItem((DateTime.Now.Year + i).ToString()));
        }
        ddlYear.SelectedValue = DateTime.Now.Year.ToString();
        
    }
    private void BindCrewStatus()
    {
        //string sql = "SELECT CrewStatusID,CrewStatusName  FROM CrewStatus where CrewStatusid not in(4,5)";
        //DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);

        //ddlCrewStatus.DataValueField = "CrewStatusID";
        //ddlCrewStatus.DataTextField = "CrewStatusName";
        //ddlCrewStatus.DataSource = dt;
        //ddlCrewStatus.DataBind();
        //ddlCrewStatus.Items.Insert(0, new ListItem("< All >", ""));
    }
    private void BindRank()
    {
        string sql = "SELECT RankID,RankCode FROM Rank ";
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);

        ddlRank.DataValueField = "RankID";
        ddlRank.DataTextField = "RankCode";
        ddlRank.DataSource = dt;
        ddlRank.DataBind();
        ddlRank.Items.Insert(0, new ListItem("< All >", ""));
    }
    private void BindRecruitmentOffice()
    {
        string sql = "SELECT RecruitingOfficeID,RecruitingOfficeName  FROM dbo.RecruitingOffice";
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);

        ddlRecruitmentOff.DataValueField = "RecruitingOfficeID";
        ddlRecruitmentOff.DataTextField = "RecruitingOfficeName";
        ddlRecruitmentOff.DataSource = dt;
        ddlRecruitmentOff.DataBind();
        ddlRecruitmentOff.Items.Insert(0, new ListItem("< All >", ""));
    }
    private void BindInstitute()
    {
        string sql = "SELECT INSTITUTENAME ,INSTITUTEID FROM TrainingInstitute WHERE STATUSID='A'";
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);

        ddlInstitute.DataValueField = "INSTITUTEID";
        ddlInstitute.DataTextField = "INSTITUTENAME";
        ddlInstitute.DataSource = dt;
        ddlInstitute.DataBind();
        ddlInstitute.Items.Insert(0, new ListItem("< Select >", ""));
    }
}
