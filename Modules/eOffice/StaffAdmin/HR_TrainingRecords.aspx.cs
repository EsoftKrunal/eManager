using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using ShipSoft.CrewManager.BusinessLogicLayer;
using ShipSoft.CrewManager.BusinessObjects;
using ShipSoft.CrewManager.Operational;

public partial class emtm_StaffAdmin_Emtm_HR_TrainingRecords : System.Web.UI.Page
{
    public int TrainingPlanningId
    {
        get
        {
            return Common.CastAsInt32(ViewState["TrainingPlanningId"]);
        }
        set
        {
            ViewState["TrainingPlanningId"] = value;
        }
    }

    public int TrainingRecommId
    {
        get
        {
            return Common.CastAsInt32(ViewState["TrainingRecommId"]);
        }
        set
        {
            ViewState["TrainingRecommId"] = value;
        }
    }    
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        lblMsg.Text = "";
        lblAssignMsg.Text = "";
        Session["CurrentModule"] = 5;
        if (!IsPostBack)
        {
           TrainingPlanningId = -1;
           ShowRecord();
           BindEditTraining();
        }
    }

    protected void ShowRecord()
    {
        int EmpId = Common.CastAsInt32(Session["EmpId"]);
        if (EmpId > 0)
        {
            DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("SELECT * FROM Hr_PersonalDetails WHERE EMPID=" + EmpId.ToString());
            if (dt != null)
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    lbl_EmpName.Text = "[ " + dr["EmpCode"].ToString() + " ] " + dr["FirstName"].ToString() + " " + dr["MiddleName"].ToString() + " " + dr["FamilyName"].ToString();
                    Session["EmpName"] = lbl_EmpName.Text.ToString();                    
                }
            BindTrainingRecords(EmpId);
        }
    }

    protected void BindTrainingRecords(int EmpId)
    {

        string strSQL = "SELECT R.TrainingRecommId, ISNULL(M.TrainingPlanningId, '0') AS TrainingPlanningId,TM.TRAININGID,TM.TRAININGNAME,CONVERT(VARCHAR,R.DueDate,106)DueDate,CONVERT(VARCHAR,D.ExpiryDate,106)ExpiryDate " +
                        ",(CONVERT(VARCHAR,STARTDATE,106) + ' - ' + CONVERT(VARCHAR,ENDDATE,106))PlanDate "+
                        ",(CONVERT(VARCHAR,STARTDATE1,106) + ' - ' + CONVERT(VARCHAR,STARTDATE1,106))DoneDate ," +

                        "CASE WHEN M.STATUS IS NULL THEN 'DUE' " +
                        "WHEN M.STATUS='A' THEN 'PLANNED' " +
                        "WHEN M.STATUS='C' THEN 'CANCELLED' " +
                        "WHEN M.STATUS='E' THEN 'COMPLETED' END " +
                        "AS STATUS " +

                        ",CASE WHEN R.RECOMMSOURCE='A' THEN 'MANUAL' WHEN R.RECOMMSOURCE='P' THEN 'PEAP' WHEN R.RECOMMSOURCE='M' THEN 'MATRIX' ELSE 'OTHER' END AS SOURCE , " +
                        "CASE WHEN M.STATUS='E' THEN LOCATION1 ELSE LOCATION END AS LOCATION,D.FileName " +
                        "FROM HR_TrainingRecommended R " +
                        "LEFT JOIN  " +
                        "( " +
                        " HR_TrainingPlanningDetails D  " +
                        " INNER JOIN HR_TrainingPlanning M ON M.TRAININGPLANNINGID=D.TRAININGPLANNINGID " +
                        ") " +
                        "ON D.TRAININGRECOMMID=R.TRAININGRECOMMID AND R.EMPID=D.EMPID  " +
                        "INNER JOIN HR_TrainingMaster TM ON TM.TrainingId=R.TrainingId " +
                        "WHERE R.EMPID=" + EmpId.ToString() + " ";
        
      DataTable dtRecords = Common.Execute_Procedures_Select_ByQueryCMS(strSQL);
        if (dtRecords != null)
        {
            rptRecordList.DataSource = dtRecords;
            rptRecordList.DataBind();
        }

    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        TrainingPlanningId = Common.CastAsInt32(((ImageButton)sender).CommandArgument.ToString());
        ShowRecord();
        divUploadDocs.Visible = true;
    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        
        if (FileUpload1.HasFile)
        {
            int EmpId = Common.CastAsInt32(Session["EmpId"]);
            if (this.FileUpload1 != null && this.FileUpload1.FileContent.Length > 0)
            {
                HttpPostedFile file = FileUpload1.PostedFile;

                string ext = Path.GetExtension(FileUpload1.FileName);
                if (ext.ToUpper() == ".JPG" || ext.ToUpper() == ".PDF")
                {

                }
                else
                {
                    lblMsg.Text = "Uploading file type should be jpg and pdf only.";
                    return;
                }

                try
                {
                    string FileName = "Emtm_TR_" + TrainingPlanningId.ToString() + "_" + EmpId.ToString() + ext;

                    string SQL = "UPDATE HR_TrainingPlanningDetails SET [FileName] = '" + FileName + "' WHERE  TrainingPlanningId = " + TrainingPlanningId.ToString() + " AND EmpId = " + EmpId.ToString() + " ; SELECT -1 ";

                    DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(SQL);
                    if (dt.Rows.Count > 0)
                    {

                        if (this.FileUpload1 != null && this.FileUpload1.FileContent.Length > 0)
                        {
                            if (File.Exists(Server.MapPath("~/EMANAGERBLOB/HRD/EmpTrainingRecord/" + FileName)))
                            {
                                File.Delete(Server.MapPath("~/EMANAGERBLOB/HRD/EmpTrainingRecord/" + FileName));
                            }

                            FileUpload1.SaveAs(Server.MapPath("~/EMANAGERBLOB/HRD/EmpTrainingRecord/" + FileName));
                            btncancel_Click(sender, e);
                            ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Document uploaded successfully.');", true);
                        }
                    }
                }
                catch (Exception ex)
                {
                    lblMsg.Text = "Unable to upload document. " + ex.Message.ToString();
                }
            }
        }
        else
        {
            lblMsg.Text = "Please select a file to upload.";
            FileUpload1.Focus();
        }

    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        TrainingPlanningId = -1;
        ShowRecord();
        this.FileUpload1 = null;
        divUploadDocs.Visible = false;
    }

    protected void lbAssigntraining_Click(object sender, EventArgs e)
    {
        BindTrainings();
        divAssignTraining.Visible = true;
    }
    protected void btnCancelAssign_Click(object sender, EventArgs e)
    {
        divAssignTraining.Visible = false;
    }

    protected void BindTrainings()
    {
        int EmpId = Common.CastAsInt32(Session["EmpId"]);

        string SQL = "SELECT TrainingId,TrainingName FROM dbo.HR_TrainingMaster MM WHERE MM.TRAININGID NOT IN " +
                     "( SELECT R.TRAININGID FROM HR_TrainingRecommended R LEFT JOIN  " +
                     "( HR_TrainingPlanningDetails D INNER JOIN dbo.HR_TrainingPlanning M ON M.TRAININGPLANNINGID=D.TRAININGPLANNINGID ) " +
                     "ON D.TRAININGRECOMMID=R.TRAININGRECOMMID AND R.EMPID=D.EMPID  " +
                     "WHERE (M.STATUS IS NULL OR M.STATUS='A') AND R.EMPID=" + EmpId.ToString() + " ) " +
                     "ORDER BY TRAININGNAME ";                     

        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(SQL);
        if(dt != null)
        {
            rptAssignTraining.DataSource = dt;
            rptAssignTraining.DataBind();

            //ddlEditTraining.DataSource = dt;
            //ddlEditTraining.DataTextField = "TrainingName";
            //ddlEditTraining.DataValueField = "TrainingId";
            //ddlEditTraining.DataBind();
        }
    }

    protected void btnAssignTraining_Click(object sender, EventArgs e)
    {

        bool IsChecked = false;

        if (rptAssignTraining.Items.Count <= 0)
        {
            lblAssignMsg.Text = "There is no training to assign.";
            return;
        }

        foreach (RepeaterItem rptItm in rptAssignTraining.Items)
        {
            CheckBox chkSelect = (CheckBox)rptItm.FindControl("chkSelect");
            if (chkSelect.Checked)
            {
                IsChecked = true;
                break;
            }
        }

        if (!IsChecked)
        {
            lblAssignMsg.Text = "Please select a training.";
            return;
        }

        foreach (RepeaterItem rptItm in rptAssignTraining.Items)
        {
            CheckBox chkSelect = (CheckBox)rptItm.FindControl("chkSelect");
            TextBox txtDueDt = (TextBox)rptItm.FindControl("txtDueDt");
            if (chkSelect.Checked)
            {
                if (txtDueDt.Text.Trim() == "")
                {
                    lblAssignMsg.Text = "Please enter due date.";
                    txtDueDt.Focus();
                    return;
                }
                DateTime dt;
                if (!DateTime.TryParse(txtDueDt.Text.Trim(), out dt))
                {
                    lblAssignMsg.Text = "Please enter valid date.";
                    txtDueDt.Focus();
                    return;
                }
            }
        }

        bool success = true;
        int EmpId = Common.CastAsInt32(Session["EmpId"]);

        foreach (RepeaterItem rpt in rptAssignTraining.Items)
        {
            CheckBox chkSelect = (CheckBox)rpt.FindControl("chkSelect");
            TextBox txtDueDt = (TextBox)rpt.FindControl("txtDueDt");
            HiddenField hfdTrainingId = (HiddenField)rpt.FindControl("hfdTrainingId");

            if (chkSelect.Checked)
            {
                Common.Set_Procedures("HR_InsertTrainingRecommandation");
                Common.Set_ParameterLength(4);
                Common.Set_Parameters(new MyParameter("@EmpId", EmpId),
                    new MyParameter("@TrainingId", hfdTrainingId.Value.Trim()),
                    new MyParameter("@DueDate", txtDueDt.Text.Trim()),
                    new MyParameter("@RecommBy", Session["loginid"].ToString()));
                DataSet ds = new DataSet();

                if (!Common.Execute_Procedures_IUD_CMS(ds))
                {
                    success = false;
                    break;
                }
            }
        }

        if (success)
        {
            BindTrainings();
            ShowRecord();
            lblAssignMsg.Text = "Record saved successfully.";
        }
        else
        {
            lblAssignMsg.Text = "Unable to save record.";
        }
    }

    protected void imgEditRecord_Click(object sender, ImageClickEventArgs e)
    {
        TrainingRecommId = Common.CastAsInt32(((ImageButton)sender).CommandArgument.ToString());

        string SQL = "SELECT REPLACE(CONVERT(varchar(11), DueDate,106),' ','-') AS DueDate,TrainingId FROM HR_TrainingRecommended WHERE TrainingRecommId=" + TrainingRecommId;
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(SQL);

        if (dt.Rows.Count > 0)
        {
            ddlEditTraining.SelectedValue = dt.Rows[0]["TrainingId"].ToString();
            txtEditDueDt.Text = dt.Rows[0]["DueDate"].ToString();
        }

        divEditTraining.Visible = true;
    }

    protected void btnEditTraining_Click(object sender, EventArgs e)
    {
        if (txtEditDueDt.Text.Trim() == "")
        {
            lblEditMsg.Text = "Please enter due date.";
            txtEditDueDt.Focus();
            return;
        }
        DateTime dt;
        if (!DateTime.TryParse(txtEditDueDt.Text.Trim(), out dt))
        {
            lblEditMsg.Text = "Please enter valid date.";
            txtEditDueDt.Focus();
            return;
        }

        int EmpId = Common.CastAsInt32(Session["EmpId"]);


        Common.Set_Procedures("HR_UpdateTrainingRecommandation");
        Common.Set_ParameterLength(5);
        Common.Set_Parameters(
            new MyParameter("@TrainingRecommId", TrainingRecommId),
            new MyParameter("@EmpId", EmpId),
            new MyParameter("@TrainingId", ddlEditTraining.SelectedValue.Trim()),
            new MyParameter("@DueDate", txtEditDueDt.Text.Trim()),
            new MyParameter("@RecommBy", Session["loginid"].ToString()));
        DataSet ds = new DataSet();

        if (Common.Execute_Procedures_IUD_CMS(ds))
        {
            lblEditMsg.Text = "Record updated successfully.";
            BindTrainingRecords(EmpId);
        }
        else
        {
            lblEditMsg.Text = "unable to save record.";
        }

    }

    protected void btnCancelEditTraining_Click(object sender, EventArgs e)
    {
        lblEditMsg.Text = "";
        txtEditDueDt.Text = "";
        ddlEditTraining.SelectedIndex = 0;
        divEditTraining.Visible = false;
    }

    public void BindEditTraining()
    {
       string SQL = "SELECT TrainingId,TrainingName FROM HR_TrainingMaster " +
                     "ORDER BY TRAININGNAME ";

        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(SQL);
        if (dt != null)
        {
            ddlEditTraining.DataSource = dt;
            ddlEditTraining.DataTextField = "TrainingName";
            ddlEditTraining.DataValueField = "TrainingId";
            ddlEditTraining.DataBind();
        }

    }

    protected void imgDeleteRecord_Click(object sender, ImageClickEventArgs e)
    {
        int TrainingRecommId = Common.CastAsInt32(((ImageButton)sender).CommandArgument.ToString());


        string Sql_Delete = "DELETE FROM HR_TrainingRecommended WHERE TrainingRecommId = " + TrainingRecommId + " ; DELETE FROM HR_TrainingPlanningDetails WHERE TrainingRecommId = " + TrainingRecommId + " ; SELECT -1 ";
        DataTable dt_Delete = Common.Execute_Procedures_Select_ByQueryCMS(Sql_Delete);

        if (dt_Delete.Rows.Count > 0)
        {
            ShowRecord();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "alert('Record deleted successfully.')", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "alert('Unable to delete record.')", true);
        }

    }
}