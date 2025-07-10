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
using System.Activities.Statements;

public partial class Modules_HRD_Vacancy_AssignCrewtoVacancy : System.Web.UI.Page
{
    public int VacancyId
    {
        get { return Common.CastAsInt32(ViewState["VacancyId"]); }
        set { ViewState["VacancyId"] = value; }
    }

    public int Login_Id
    {
        get { return Common.CastAsInt32(ViewState["Login_Id"]); }
        set { ViewState["Login_Id"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        // Main.Text = "";
        Login_Id = Convert.ToInt32(Session["loginid"].ToString());
        if (!Page.IsPostBack)
        {
            VacancyId = Common.CastAsInt32(Request.QueryString["VacancyId"]);
            Load_Vesseltype();
            Load_Rank();  
          //  Load_RecruitingOffice();
            string sql = "Select *, (Select VesselTypeId from Vessel v with(nolock) where  v.VesselId = HRD_Vacancy.VesselId) As VesselTypeId from HRD_Vacancy with(nolock) where VacancyId = " + VacancyId + "";
            DataTable dtVacancy = Common.Execute_Procedures_Select_ByQueryCMS(sql);
            if (dtVacancy.Rows.Count > 0)
            {
                ddl_Rank.SelectedValue = dtVacancy.Rows[0]["RankId"].ToString();
               // ddl_RecOff.SelectedValue = dtVacancy.Rows[0]["Recruiter_Office"].ToString();
                ddl_VesselType.SelectedValue = dtVacancy.Rows[0]["VesselTypeId"].ToString();
            }

        }
    }

    private void Load_Vesseltype()
    {
        DataSet ds = cls_SearchReliever.getMasterData("VesselType", "VesselTypeId", "VesselTypeName");
        ddl_VesselType.DataSource = ds.Tables[0];
        ddl_VesselType.DataTextField = "VesselTypeName";
        ddl_VesselType.DataValueField = "VesselTypeId";
        ddl_VesselType.DataBind();
        ddl_VesselType.Items.Insert(0, new ListItem("< Select >", ""));
    }
   
    //private void Load_RecruitingOffice()
    //{
    //    DataSet ds = cls_SearchReliever.getMasterData("RecruitingOffice", "RecruitingOfficeId", "RecruitingOfficeName");
    //    ddl_RecOff.DataSource = ds.Tables[0];
    //    ddl_RecOff.DataTextField = "RecruitingOfficeName";
    //    ddl_RecOff.DataValueField = "RecruitingOfficeId";
    //    ddl_RecOff.DataBind();
    //    ddl_RecOff.Items.Insert(0, new ListItem("< All >", ""));
    //}
    private void Load_Rank()
    {
        DataSet ds = cls_SearchReliever.getMasterData("Rank", "RankId", "RankCode");
        ddl_Rank.DataSource = ds.Tables[0];
        ddl_Rank.DataTextField = "RankCode";
        ddl_Rank.DataValueField = "RankId";
        ddl_Rank.DataBind();
        ddl_Rank.Items.Insert(0, new ListItem("< Select >", ""));
    }

    protected void btn_Search_Click(object sender, EventArgs e)
    {
        try
        {
            BindGrid();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "", "alert('"+ ex.Message.ToString()+ "')", true);
        }
    }
    protected void BindGrid()
    {
        string sql = "Select (Select RankCode from dbo.rank where Rank.Rankid=b.RankAppliedId) as Rank\r\n,b.FirstName + ' ' + b.MiddleName + ' ' + b.Lastname as [Name]\r\n,(select NationalityCode from dbo.Country where countryid=b.nationalityid) as Country\r\n,replace(Isnull(convert(varchar,b.AvailableFrom,106),'') ,' ','-') as AvailableFrom\r\n, b.CandidateId\r\n, b.RankId\r\n, b.VesselTypes\r\n, b.FirstName\r\n, b.LastName\r\n, b.RankAppliedId\r\nfrom (\r\nSelect cpd.RankAppliedId\r\n,cpd.FirstName,cpd.MiddleName, cpd.Lastname\r\n,cpd.nationalityid\r\n,cpd.AvailableFrom\r\n,cpd.CandidateId \r\n,cpd.RankAppliedId As RankId \r\n,cpd.VesselTypes\r\nfrom CandidatePersonalDetails cpd Where Status = 3 \r\nand cpd.candidateid not in (Select CandidateId from CrewPersonalDetails with(nolock) where isnull(CandidateId,0) <> 0) \r\nand candidateid not in (Select HVC_CandidateId from HRD_Vacancy_CandidateMapping with(nolock) where HVC_Status = 'A')\r\n\r\nUNION ALL\r\n\r\nSelect  cp.RankAppliedId\r\n,cp.FirstName,cp.MiddleName, cp.Lastname\r\n,cp.nationalityid\r\n,cp.AvailableFrom\r\n,cp.CandidateId \r\n,cp.RankAppliedId As RankId \r\n,cpd.VesselTypes\r\nfrom CandidatePersonalDetails cpd with(nolock) \r\ninner join CrewPersonalDetails cp with(nolock) on cpd.CandidateId = cp.CandidateId\r\nWhere Status = 3 \r\nand cp.CrewStatusId <> 4\r\nand cpd.candidateid not in (Select HVC_CandidateId from HRD_Vacancy_CandidateMapping with(nolock) where HVC_Status = 'A')\r\nand cpd.CandidateId not in (Select CandidateId from CrewPersonalDetails cp with(nolock) inner join (Select VesselId from HRD_Vacancy with(nolock) where VacancyID = " + VacancyId + ") as a on cp.CurrentVesselId = a.VesselId and cp.CrewStatusId = 4 ) ) As b where 1 = 1 ";

        if (txt_EmpNo.Text != "")
        {
            sql = sql + " And b.CandidateId =  " + Convert.ToInt32(txt_EmpNo.Text) + "";
        }

        if (!string.IsNullOrWhiteSpace(txt_FirstName.Text) && !string.IsNullOrWhiteSpace(txt_LastName.Text))
        {
            sql = sql + " And (b.FirstName Like ' " + txt_FirstName.Text.Trim() + "%' OR b.Lastname Like '" + txt_LastName.Text.Trim() + "%' ) ";
        }
        else if (string.IsNullOrWhiteSpace(txt_FirstName.Text) && !string.IsNullOrWhiteSpace(txt_LastName.Text))
        {
            sql = sql + " And (b.Lastname Like '" + txt_LastName.Text.Trim() + "%' )";
        }
        else if (!string.IsNullOrWhiteSpace(txt_FirstName.Text) && string.IsNullOrWhiteSpace(txt_LastName.Text))
        {
            sql = sql + " And (b.FirstName Like '" + txt_FirstName.Text.Trim() + "%' )";
        }

        if (ddl_Rank.SelectedIndex > 0)
        {
            sql = sql + " And b.RankAppliedId = " + ddl_Rank.SelectedValue + " ";
        }

        if (ddl_VesselType.SelectedIndex > 0)
        {
            sql = sql + " And (b.VesselTypes like '%" + ddl_VesselType.SelectedValue + "%')";
        }

        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);

        //lblGradingFor.Text = (Mode == "V") ? ViewState["CurrentVesselCode"].ToString() : ViewState["CurrentOwnerCode"].ToString();
        string Filter = "";

        DataView dv = dt.DefaultView;



        Session["RP_FilterData"] = sql + "~" + Filter;
        dv.RowFilter = Filter;

        rpt_SignOnCrewList.DataSource = dv.ToTable();
        rpt_SignOnCrewList.DataBind();
    }

    protected void btnAssignCrew_Click(object sender, EventArgs e)
    {
        int CandidateId;
        CandidateId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        try
        {
            Common.Set_Procedures("SP_IU_HRD_Vacancy_CandidateMapping");
            Common.Set_ParameterLength(3);
            Common.Set_Parameters(
                new MyParameter("@vacancyId", Convert.ToInt32(VacancyId)),
                new MyParameter("@candidateId", Convert.ToInt32(CandidateId)),
                new MyParameter("@Createdby", Convert.ToInt32(Login_Id))
               );
            DataSet Ds = new DataSet();
            Boolean res = false;
            res = Common.Execute_Procedures_IUD(Ds);
            if (res == true)
            {
                int c = Common.CastAsInt32(Ds.Tables[0].Rows[0][0]);
                if (c > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "", "alert('Candidate Assign successfully for this Vacancy.')", true);
                    Common.Execute_Procedures_Select_ByQueryCMS("Update ProposalToOwner SET PTO_VacancyId = " + VacancyId + " where PTO_CandidateId = " + Convert.ToInt32(CandidateId) + "");
                    BindGrid();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "", "alert('Record could not saved. " + Common.ErrMsg.ToString() + "')", true);

                }
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "dialog", "window.opener.RefereshPage();window.close();", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "", "alert('Record could not saved. " + ex.Message.ToString() + "')", true);
        }
    }
}