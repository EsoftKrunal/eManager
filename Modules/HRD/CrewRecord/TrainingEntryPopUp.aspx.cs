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
using ShipSoft.CrewManager.BusinessLogicLayer;
using ShipSoft.CrewManager.BusinessObjects;
using ShipSoft.CrewManager.Operational;

public partial class CrewRecord_TrainingEntryPopUp : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        if (!Page.IsPostBack)
        {
            HiddenPK.Value = Session["CrewId"].ToString();
            BindTrainingTypeDropDown();
            ddl_TrainingType_SelectedIndexChanged(sender, e);
        }
    }
    private void BindTrainingTypeDropDown()
    {
        DataTable dt114 = Training.selectDataTrainingTypeId();
        this.ddl_TrainingType.DataValueField = "TrainingTypeId";
        this.ddl_TrainingType.DataTextField = "TrainingTypeName";
        this.ddl_TrainingType.DataSource = dt114;
        this.ddl_TrainingType.DataBind();
        ddl_TrainingType_SelectedIndexChanged(new object(), new EventArgs());
    }
    protected void btn_Search_Click(object sender, EventArgs e)
    {
        CrewTrainingRequirement crewtrainreq = new CrewTrainingRequirement();
        try
        {
            crewtrainreq.TrainingRequirementId = -1;
            crewtrainreq.N_CrewTrainingStatus = "O";  
            crewtrainreq.CreatedBy = Convert.ToInt32(Session["loginid"].ToString());
            crewtrainreq.CrewId = Convert.ToInt16(HiddenPK.Value);
            crewtrainreq.TrainingId = Convert.ToInt16(this.ddl_TrainingReq_Training.SelectedValue);
            crewtrainreq.Remark = this.txt_Remark.Text;
            crewtrainreq.N_DueDate = txt_DueDate.Text;
            crewtrainreq.N_CrewVerified = chk_TrainingVerified.Checked ? "Y" : "N";
            crewtrainreq.N_CrewAppraisalId = int.Parse(Session["N_CrewAppId"].ToString());

            ProcessCrewTrainingRequirementInsertData addreqdata = new ProcessCrewTrainingRequirementInsertData();
            addreqdata.TrainingRequirement = crewtrainreq;
            addreqdata.Invoke();
            lblMessage.Text = "Record Successfully Saved.";
        }
        catch (Exception es)
        {
            lblMessage.Text = "Record Not Saved.";
        }
        cleartrainingreqdetails();
    }
    private void cleartrainingreqdetails()
    {
        try
        {
            ddl_TrainingType.SelectedIndex = 0;
            ddl_TrainingType_SelectedIndexChanged(new object(), new EventArgs());
            this.ddl_TrainingReq_Training.SelectedIndex = 0;
            chk_TrainingVerified.Checked = false;
            lbl_TrainingStatus.Text = "";
            txt_DueDate.Text = "";
        }
        catch { }
        this.txt_Remark.Text = "";
    }
    protected void ddl_TrainingType_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt133 = Training.selectDataTrainingDetailsByTrainingTypeId(Convert.ToInt32(ddl_TrainingType.SelectedValue));
        this.ddl_TrainingReq_Training.DataValueField = "TrainingId";
        this.ddl_TrainingReq_Training.DataTextField = "TrainingName";
        this.ddl_TrainingReq_Training.DataSource = dt133;
        this.ddl_TrainingReq_Training.DataBind();
        if (ddl_TrainingReq_Training.Items.Count <= 0)
        {
            ddl_TrainingReq_Training.Items.Insert(0, new ListItem("<Select>", "0"));
        }
    }
}
