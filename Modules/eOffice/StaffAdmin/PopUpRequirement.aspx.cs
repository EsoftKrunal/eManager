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

public partial class emtm_StaffAdmin_Emtm_PopUpRequirement : System.Web.UI.Page
{
    public int TrainingId
    {
        get
        {
            return Common.CastAsInt32(ViewState["TrainingId"]);
        }
        set
        {
            ViewState["TrainingId"] = value;
        }
    }
    public string TrainingRecommIds
    {
        get
        {
            return (ViewState["TrainingRecommIds"].ToString());
        }
        set
        {
            ViewState["TrainingRecommIds"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        if (!IsPostBack)
        {
            TrainingRecommIds = Session["TRIDs"].ToString();
            if (TrainingRecommIds.Trim() != "")
            {
                DataTable dtEMps = Common.Execute_Procedures_Select_ByQueryCMS("SELECT * FROM HR_TrainingRecommended WHERE TrainingRecommID IN (" + TrainingRecommIds + ")");
                TrainingId =Common.CastAsInt32(dtEMps.Rows[0]["TRAININGID"]);
                DataTable dtTrainingName = Common.Execute_Procedures_Select_ByQueryCMS("SELECT TRAININGNAME FROM HR_TrainingMaster WHERE TRAININGID=" + TrainingId.ToString() + "");
                BindPosition();
                lblTrainingName.Text = "Training Planning < " + dtTrainingName.Rows[0]["TRAININGNAME"] + " >";
                BindPlannedTrainings();
                BindRecommEmployees();
                BindTime();
            }
        }

    }
    public void BindPosition()
    {
        string sql = "";
        sql = "Select PositionID,PositionName from Position order by PositionName";
        DataTable dtPosition = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        ddlSPosition.DataSource = dtPosition;
        ddlSPosition.DataTextField = "PositionName";
        ddlSPosition.DataValueField = "PositionID";
        ddlSPosition.DataBind();
        ddlSPosition.Items.Insert(0, new ListItem(" All ", ""));
    }
    public void BindTime()
    {
        for (int i = 0; i < 24; i++)
        {
            if (i < 10)
            {
                ddlStHr.Items.Add(new ListItem("0" + i.ToString(), "0" + i.ToString()));
                ddlEtHr.Items.Add(new ListItem("0" + i.ToString(), "0" + i.ToString()));
            }
            else
            {
                ddlStHr.Items.Add(new ListItem(i.ToString(), i.ToString()));
                ddlEtHr.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
        }

        for (int i = 0; i < 60; i++)
        {
            if (i < 10)
            {
                ddlStMin.Items.Add(new ListItem("0" + i.ToString(), "0" + i.ToString()));
                ddlEtMin.Items.Add(new ListItem("0" + i.ToString(), "0" + i.ToString()));
            }
            else
            {
                ddlStMin.Items.Add(new ListItem(i.ToString(), i.ToString()));
                ddlEtMin.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
        }
    }
    public void BindRecommEmployees()
    {
        string strSQL = "SELECT EmpCode,PD.EmpId,TPD.TrainingRecommId,TPD.TrainingId,(FirstName + ' ' + FamilyName) As EmpName,DJC,P.PositionName,O.OfficeName,D.DeptName AS DepartmentName,TPD.Important,P.PositionId,O.OfficeId,D.DeptId As DepartmentId, Source=(Case when RecommSource='A' then 'Assign' when RecommSource='P' then 'PEAP' else 'NA' END), REPLACE(CONVERT(VARCHAR(11), TPD.DueDate,106),' ','-') AS DueDate " +
                         "FROM HR_TrainingRecommended TPD " +
                         "INNER JOIN Hr_PersonalDetails PD ON TPD.EmpId = PD.EmpId " +
                         "LEFT JOIN Position P ON P.PositionId = PD.Position " +
                         "LEFT JOIN Office O ON O.OfficeId = PD.Office " +
                         "LEFT JOIN HR_Department D ON D.DeptId = PD.Department " +
                         "WHERE TPD.TrainingId = " + TrainingId + " AND TrainingRecommId NOT IN (SELECT TrainingRecommId FROM HR_TrainingPlanningDetails P1 WHERE P1.TrainingPlanningId In (SELECT P2.TrainingPlanningId from HR_TrainingPlanning P2 WHERE P2.Status <> 'C')) " +
                         "ORDER BY EmpName ";
        DataTable dtrecomm = Common.Execute_Procedures_Select_ByQueryCMS(strSQL);

        //rptRecommEmp.DataSource = dtrecomm;
        //rptRecommEmp.DataBind();
    }
    public void BindNonRecommEmployees()
    {
        string strSQL = "SELECT EmpCode,EmpId,(FirstName + ' ' + FamilyName) As EmpName,DJC,P.PositionName,O.OfficeName,D.DeptName AS DepartmentName " +
                        "FROM Hr_PersonalDetails PD " +
                        "LEFT JOIN Position P ON P.PositionId = PD.Position " +
                        "LEFT JOIN Office O ON O.OfficeId = PD.Office " +
                        "LEFT JOIN HR_Department D ON D.DeptId = PD.Department " +
                        "WHERE EmpId NOT IN ( SELECT EmpId FROM HR_TrainingRecommended WHERE TrainingId = " + TrainingId + " ) ";
        string whereClause = " AND 1=1 ";

        if (txtSECode.Text.Trim() != "")
            whereClause += "AND EmpCode LIKE '" + txtSECode.Text.Trim() + "%'";

        if (txtSEName.Text.Trim() != "")
        {
            whereClause += "AND ( FirstName LIKE '" + txtSEName.Text.Trim() + "%' OR FamilyName LIKE '" + txtSEName.Text.Trim() + "%' )";
        }

        if (ddlSPosition.SelectedIndex > 0)
            whereClause += "AND Position =" + ddlSPosition.SelectedValue;

        DataTable dtNonRecomm = Common.Execute_Procedures_Select_ByQueryCMS(strSQL + whereClause + " ORDER BY EmpName ");

        rptAddEmp.DataSource = dtNonRecomm;
        rptAddEmp.DataBind();
    }
    public void BindPlannedTrainings()
    {
        string SQL = "SELECT Row_number() OVER(ORDER BY TP.TrainingPlanningId) AS SrNo,TP.TrainingPlanningId,TM.TrainingName, TP.StartDate,TP.EndDate, " +
                     "(SELECT Count(*) FROM HR_TrainingRecommended TPD INNER JOIN Hr_PersonalDetails PD ON TPD.EmpId = PD.EmpId " +
                     "WHERE TrainingRecommId IN (SELECT TrainingRecommId FROM HR_TrainingPlanningDetails P1 WHERE P1.TrainingPlanningId In " +
                     "(SELECT P2.TrainingPlanningId from HR_TrainingPlanning P2 WHERE P2.Status = 'A'))) As NumOfSeats,ValidityPeriod " +
                     "FROM HR_TrainingPlanning TP " +
                     "INNER JOIN HR_TrainingMaster TM ON TP.TrainingId = TM.TrainingId and TP.TrainingId=" + TrainingId.ToString() +
                     " WHERE TP.Status = 'A' ";

        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(SQL);

        if (dt != null)
        {
            rptPlannedTrainings.DataSource = dt;
            rptPlannedTrainings.DataBind();
        }

    }
    protected void btnPlan_Click(object sender, ImageClickEventArgs e)
    {
        TrainingId = Common.CastAsInt32(((ImageButton)sender).CommandArgument.ToString());
        BindRecommEmployees();
        BindNonRecommEmployees();
        btnAddMore.Visible = true;

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        TrainingId = 0;
        txtStartDt.Text = "";
        txtEndDt.Text = "";
        txtLocation.Text = "";
        ddlStHr.SelectedIndex = 0;
        ddlStMin.SelectedIndex = 0;
        ddlEtHr.SelectedIndex = 0;
        ddlEtMin.SelectedIndex = 0;
        btnAddMore.Visible = false;        
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (TrainingId == 0)
        {
            lblPlanMsg.Text = "Please select a training.";
            return;
        }
        
        if (txtStartDt.Text.Trim() == "")
        {
            lblPlanMsg.Text = "Please select start date.";
            txtStartDt.Focus();
            return;
        }
        DateTime dt;
        if (!DateTime.TryParse(txtStartDt.Text.Trim(), out dt))
        {
            lblPlanMsg.Text = "Please enter valid date.";
            txtStartDt.Focus();
            return;
        }
        
        if (txtEndDt.Text.Trim() == "")
        {
            lblPlanMsg.Text = "Please select end date.";
            txtEndDt.Focus();
            return;
        }
        DateTime dt1;
        if (!DateTime.TryParse(txtEndDt.Text.Trim(), out dt1))
        {
            lblPlanMsg.Text = "Please enter valid date.";
            txtEndDt.Focus();
            return;
        }

        if (Convert.ToDateTime(txtStartDt.Text.Trim()) > Convert.ToDateTime(txtEndDt.Text.Trim()))
        {
            lblPlanMsg.Text = "Start date must be less than end date.";
            txtStartDt.Focus();
            return;
        }

        
        string StartTime = ddlStHr.SelectedValue.Trim() + ":" + ddlStMin.SelectedValue.Trim();
        string EndTime = ddlEtHr.SelectedValue.Trim() + ":" + ddlEtMin.SelectedValue.Trim();

        Common.Set_Procedures("HR_InsertUpdateTrainingPlanning");
        Common.Set_ParameterLength(7);
        Common.Set_Parameters(new MyParameter("@TrainingPlanningId", 0),
            new MyParameter("@TrainingId", TrainingId),
            new MyParameter("@Location", txtLocation.Text.Trim()),
            new MyParameter("@StartDate", txtStartDt.Text.Trim()),
            new MyParameter("@EndDate", txtEndDt.Text.Trim()),
            new MyParameter("@StartTime", StartTime.Trim()),
            new MyParameter("@EndTime", EndTime.Trim()));
        DataSet ds = new DataSet();
        bool success = true;
        if (Common.Execute_Procedures_IUD_CMS(ds))
        {
            int TrainingPlanningId = Common.CastAsInt32(ds.Tables[0].Rows[0]["PlanningId"].ToString());

            //foreach (RepeaterItem rpt in rptRecommEmp.Items)
            //{
            //    HiddenField hfEmpId = (HiddenField)rpt.FindControl("hfEmpId");
            //    HiddenField hfPositionId = (HiddenField)rpt.FindControl("hfPositionId");
            //    HiddenField hfOfficeId = (HiddenField)rpt.FindControl("hfOfficeId");
            //    HiddenField hfDepartmentId = (HiddenField)rpt.FindControl("hfDepartmentId");
            //    HiddenField hfRecommId = (HiddenField)rpt.FindControl("hfRecommId");

            //    Common.Set_Procedures("HR_InsertTrainingPlanningDetailsForRecomanded");
            //    Common.Set_ParameterLength(6);
            //    Common.Set_Parameters(new MyParameter("@TrainingPlanningId", TrainingPlanningId),
            //        new MyParameter("@EmpId", hfEmpId.Value.Trim()),
            //        new MyParameter("@OfficeId", hfOfficeId.Value.Trim()),
            //        new MyParameter("@PositionId", hfPositionId.Value.Trim()),
            //        new MyParameter("@DepartmentId", hfDepartmentId.Value.Trim()),
            //        new MyParameter("@TrainingRecommId", hfRecommId.Value.Trim()));
            //    DataSet ds1 = new DataSet();

            //    if (!Common.Execute_Procedures_IUD_CMS(ds1))
            //    {
            //        success = false;
            //        break;
            //    }
            //}
            //if (success)
            //{
            //    BindRecommEmployees();
            //    btnCancel_Click(sender, e);
            //    BindPlannedTrainings();
            //    divAddEmp.Visible = false;
            //    ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "RefreshParent();", true);
            //    ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Planning saved successfully.');", true);
            //}
            //else
            //    ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Unable to make planning.');", true);

        }
    }
    protected void btnREDelete_Click(object sender, ImageClickEventArgs e)
    {
        int TrainingRecommId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("DELETE FROM HR_TrainingRecommended WHERE TrainingRecommId = " + TrainingRecommId + " ; SELECT -1");
        if (dt.Rows.Count > 0)
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Employee deleted successfully.');", true);
            BindRecommEmployees();
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Unable to delete employee.');", true);
        }
    }
    protected void btnAddMore_Click(object sender, EventArgs e)
    {
        divAddEmp.Visible = true;
        btnAddMore.Visible = false;
        btnHide.Visible = true;
    }
    protected void btnHide_Click(object sender, EventArgs e)
    {
        divAddEmp.Visible = false;
        btnAddMore.Visible = true;
        btnHide.Visible = false;
    }
    protected void btnAddEmp_Click(object sender, EventArgs e)
    {
        bool IsChecked = false;
        if (TrainingId == 0)
        {
            lblAddMsg.Text = "Please select a training.";
            return;
        }

        foreach (RepeaterItem rpt in rptAddEmp.Items)
        {
            CheckBox chkSelect = (CheckBox)rpt.FindControl("chkSelect");
            HiddenField hfRecommId = (HiddenField)rpt.FindControl("hfRecommId");

            if (chkSelect.Checked)
            {
                IsChecked = true;
                break;
            }
        }

        if (!IsChecked)
        {
            lblAddMsg.Text = "Please select atleast one employee.";
            return;
        }

        foreach (RepeaterItem rptItem in rptAddEmp.Items)
        {
            CheckBox chkSelect = (CheckBox)rptItem.FindControl("chkSelect");
            HiddenField hfAddEmpId = (HiddenField)rptItem.FindControl("hfAddEmpId");

            if (chkSelect.Checked)
            {
                Common.Set_Procedures("HR_InsertNonRecommanded");
                Common.Set_ParameterLength(7);
                Common.Set_Parameters(new MyParameter("@TrainingRecommId", 0),
                    new MyParameter("@EmpId", hfAddEmpId.Value.Trim()),
                    new MyParameter("@TrainingId", TrainingId),
                    new MyParameter("@RecommSource", "A"),
                    new MyParameter("@RecommBy", Session["loginid"].ToString()),
                    new MyParameter("@DueDate", null),
                    new MyParameter("@Important", "N"));
                DataSet ds = new DataSet();

                if (Common.Execute_Procedures_IUD_CMS(ds))
                {
                    //ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Record saved successfully.');", true);
                    lblAddMsg.Text = "Employee added successfully.";

                }
                else
                {
                    //ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Unable to save record.');", true);
                    lblAddMsg.Text = "Unable to add employee.";
                }
            }
        }
        BindRecommEmployees();
        BindNonRecommEmployees();
    }
    protected void Search_Click(object sender, EventArgs e)
    {
        BindNonRecommEmployees();
    }


    //--------------------
    protected void btnPlanNewTraining_OnClick(object sender, EventArgs e)
    {
        pnlPlanTraining.Visible = true;
        pnlExistinTraining.Visible = false;

        btnPlanNewTraining.CssClass = "btn11sel";
        btnExistingTraining.CssClass = "btn11";
    }
    protected void btnExistingTraining_OnClick(object sender, EventArgs e)
    {
        pnlPlanTraining.Visible = false;
        pnlExistinTraining.Visible = true;

        btnPlanNewTraining.CssClass = "btn11";
        btnExistingTraining.CssClass = "btn11sel";
    }
}
