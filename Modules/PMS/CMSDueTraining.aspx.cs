using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Text;
//using Newtonsoft.Json;
using System.IO;
using Ionic.Zip;

public partial class CMSDueTraining : System.Web.UI.Page
{
    public string VesselCode
    {
        get { return Convert.ToString (Session["CurrentShip"]); }
    }
    public int CrewID
    {
        get { return Common.CastAsInt32(ViewState["_CrewID111"]); }
        set { ViewState["_CrewID111"] = value; }
    }

    
    protected void Page_Load(object sender, EventArgs e)
    {
        // -------------------------- SESSION CHECK ----------------------------------------------
        ProjectCommon.SessionCheck();
        //-----------------------------
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "eee", "SetCalender();", true);
        //-----------------------------
        if (!IsPostBack)
        {

            CrewID = Common.CastAsInt32(Page.Request.QueryString["CrewID"]);
            lblVesselName.InnerText = Session["CurrentShip"].ToString();
            ShowCrewDetails();
            BindTrainings();
        }
    }


    protected void BindTrainings()
    {
        string sqlTrng = " select  ROW_NUMBER()over(order by N_DueDate)SNO,* from PMS_CREW_TRAININGREQUIREMENT Where VesselCode='" + VesselCode + "'and CrewID=" + CrewID + " and TrainingRequirementId not in ( select TrainingRequirementId from PMS_CREW_TRAININGCOMPLETED  Where VesselCode='" + VesselCode + "'and CrewID=" + CrewID + " ) ";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sqlTrng);

        rptTrainingsDue.DataSource = dt;
        rptTrainingsDue.DataBind();
    }
    public void ShowCrewDetails()
    {
        string sql = " SElect CrewNumber,CrewName,SignOnDate,ReliefDueDate ,R.RankCode " +
                     "   from PMS_CREW_HISTORY H inner join MP_AllRank R on R.RankId = h.RankId where CrewId =" + CrewID;
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
        if (dt.Rows.Count > 0)
        {
            DataRow dr = dt.Rows[0];
            lblselectedCrewNumber.Text = dr["CrewNumber"].ToString();
            lblselectedUsername.Text = dr["CrewName"].ToString();
            lblselectedRankCode.Text = dr["RankCode"].ToString();

            lblSignOnDate.Text = "[ " + Common.ToDateString(dr["SignOnDate"]) + " : " + Common.ToDateString(dr["ReliefDueDate"]) + " ]";
        }
    }

    //--------------------------------------------------------------------
    protected void btnCloseDue_OnClick(object sender, EventArgs e)
    {
        string trainingids = "";
        foreach (RepeaterItem item in rptTrainingsDue.Items)
        {
            CheckBox chkTraining = (CheckBox)item.FindControl("chkTraining");
            HiddenField hfdTrainingRequirementId = (HiddenField)item.FindControl("hfdTrainingRequirementId");
            if (chkTraining.Checked)
            {
                trainingids = trainingids + "," + hfdTrainingRequirementId.Value;
            }
        }

        if (trainingids == "")
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "eee", "alert('Please select training.')", true);
            return;
        }
        myModel3.Visible = true;

        //string sql = "select TrainingRequirementId,DBO.fn_getSimilerTrainingsName(TRAININGID) as TrainingName,crewid,dbo.sp_getLastDone(crewid,trainingid) as LastDone,n_duedate,PlannedFor,CASE WHEN ISNULL(SOURCE, 0) = 1 THEN 'ASSIGNED' WHEN ISNULL(SOURCE,0)= 2 THEN 'MATRIX' ELSE 'PEAP' END as Sourcename from dbo.CrewTrainingRequirement where TrainingRequirementId in(" + trainingids.Substring(1) + ")";
        string sql = " select ROW_NUMBER()over(order by N_DueDate)SNO,* from PMS_CREW_TRAININGREQUIREMENT Where VesselCode='"+VesselCode+ "' and TrainingRequirementId in(" + trainingids.Substring(1) + ") ";
        rptTrainingMatrix3.DataSource = Common.Execute_Procedures_Select_ByQuery(sql);
        rptTrainingMatrix3.DataBind();
    }
    protected void btnCloseMyModel3_OnClick(object sender, EventArgs e)
    {
        myModel3.Visible = false;
    }
    protected void btnSaveClosure_OnClick(object sender, EventArgs e)
    {
        byte[] attachedfile;
        string FileName = "";
        foreach (RepeaterItem item in rptTrainingMatrix3.Items)
        {
            HiddenField hfdTrainingRequirementId = (HiddenField)item.FindControl("hfdTrainingRequirementId");
            TextBox txtFromDate = (TextBox)item.FindControl("txtFromDate");
            TextBox txtToDate = (TextBox)item.FindControl("txtToDate");
            FileUpload fuAttachment = (FileUpload)item.FindControl("fuAttachment");

            if (txtFromDate.Text == "" || txtToDate.Text == "")
            {
                lblMsg11.Text = "Please enter actual training dates.";
                return;
            }
        }

        foreach (RepeaterItem item in rptTrainingMatrix3.Items)
        {


            HiddenField hfdTrainingRequirementId = (HiddenField)item.FindControl("hfdTrainingRequirementId");
            TextBox txtFromDate = (TextBox)item.FindControl("txtFromDate");
            TextBox txtToDate = (TextBox)item.FindControl("txtToDate");
            //Common.Execute_Procedures_Select_ByQueryCMS("UPDATE CrewTrainingRequirement SET fromdate='" + txtFromDate.Text + "',todate='" + txtToDate.Text + "',trainingplanningid=" + ddlTrainingLocation.SelectedValue + ",Attended='Y',N_CrewTrainingStatus='C',N_CrewVerified='Y',N_VerifiedBy=0,N_VerifiedOn=getdate() WHERE TrainingRequirementID = " + hfdTrainingRequirementId.Value);

            FileUpload fuAttachment = (FileUpload)item.FindControl("fuAttachment");
            FileName = "";
            if (fuAttachment.HasFile)
            {
                attachedfile = (byte[])fuAttachment.FileBytes;
                FileName = fuAttachment.FileName;
            }
            else
            {
                attachedfile = new byte[0];
            }
            Common.Set_Procedures("sp_UpdateCrewTrainingRequirement");
            Common.Set_ParameterLength(6);
            Common.Set_Parameters(
                 new MyParameter("@TrainingRequirementID", hfdTrainingRequirementId.Value),
                 new MyParameter("@txtFromDate", txtFromDate.Text.Trim()),
                 new MyParameter("@txtToDate", txtToDate.Text.Trim()),
                 new MyParameter("@Attachment", attachedfile),
                 new MyParameter("@AttachmentName", FileName),
                 new MyParameter("@ModifiedBy", Session["UserName"].ToString())
                );
            Boolean res;
            DataSet Ds = new DataSet();
            res = Common.Execute_Procedures_IUD(Ds);

        }
        myModel3.Visible = false;
        BindTrainings();
    }
    protected void chkSelAll_OnCheckedChanged(object sender, EventArgs e)
    {
        CheckBox chk = (CheckBox)sender;
        foreach (RepeaterItem item in rptTrainingsDue.Items)
        {
            CheckBox chkTraining = (CheckBox)item.FindControl("chkTraining");
            chkTraining.Checked = chk.Checked;
        }

    }


}