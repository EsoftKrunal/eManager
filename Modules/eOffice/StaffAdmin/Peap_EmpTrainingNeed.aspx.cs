using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;

public partial class emtm_StaffAdmin_Emtm_Peap_EmpTrainingNeed : System.Web.UI.Page
{
    public int PeapID
    {
        get
        {
            return Common.CastAsInt32(ViewState["PeapID"]);
        }
        set
        {
            ViewState["PeapID"] = value;
        }
    }
    public int EmpID
    {
        get
        {
            return Common.CastAsInt32(ViewState["EmpID"]);
        }
        set
        {
            ViewState["EmpID"] = value;
        }
    }
    public int AppraiserId
    {
        get
        {
            return Common.CastAsInt32(ViewState["AppraiserId"]);
        }
        set
        {
            ViewState["AppraiserId"] = value;
        }
    }
    public string Mode
    {
        get
        {
            return "" + ViewState["Mode"].ToString();
        }
        set
        {
            ViewState["Mode"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        lblMsg.Text = "";

        if (!Page.IsPostBack)
        {
            if (Request.QueryString["PID"] != null || Request.QueryString["PID"].ToString() != "")
            {
                PeapID = Common.CastAsInt32(Request.QueryString["PID"].ToString());
                Mode = Request.QueryString["Mode"].ToString();
                AppraiserId = Common.CastAsInt32(Request.QueryString["AID"].ToString());

                if (Mode == "A")
                {
                    EmpID = Common.CastAsInt32(Session["EmpId"]);
                }
                else
                {
                    EmpID = Common.CastAsInt32(Session["ProfileId"]);
                }

                ShowRecord();
                BindPlannedTrainings();
               
            }
        }

    }

    public void ShowRecord()
    {
        string strSQL = "SELECT PEAPID,CATEGORY As PeapLevel,CASE Occasion WHEN 'R' THEN 'Routine' WHEN 'I' THEN 'Interim' ELSE '' END AS Occasion, POSITIONNAME,EMPCODE,FIRSTNAME , FAMILYNAME,D.DeptName AS DepartmentName, " +
                        "Replace(Convert(Varchar,PM.PEAPPERIODFROM,106),' ','-') AS PEAPPERIODFROM ,Replace(Convert(Varchar,PM.PEAPPERIODTO,106),' ','-') AS PEAPPERIODTO,Replace(Convert(Varchar,PD.DJC,106),' ','-') AS DJC,PM.Status,(SELECT FIRSTNAME + ' ' + FAMILYNAME FROM Hr_PersonalDetails WHERE EMPID=" + AppraiserId.ToString() + ") AS AppraiserName " +
                        "FROM HR_EmployeePeapMaster PM  " +
                        "INNER JOIN dbo.Hr_PersonalDetails PD ON PM.EMPID=PD.EMPID  " +
                        "LEFT JOIN dbo.HR_PeapCategory PC ON PC.PID= PM.PeapCategory  " +
                        "LEFT JOIN POSITION P ON P.POSITIONID=PD.POSITION  " +
                        "LEFT JOIN HR_Department D ON D.DeptId=PD.DEPARTMENT  " +
                        "WHERE PM.PeapId = " + PeapID.ToString() + " ";

        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(strSQL);


        if (dt.Rows.Count > 0)
        {


            lblPeapLevel.Text = dt.Rows[0]["PeapLevel"].ToString();
            txtOccasion.Text = dt.Rows[0]["Occasion"].ToString();
            txtFirstName.Text = dt.Rows[0]["FIRSTNAME"].ToString();
            txtLastName.Text = dt.Rows[0]["FAMILYNAME"].ToString();
            lblAppraiserName.Text = "|  Appraiser Name : " + dt.Rows[0]["AppraiserName"].ToString();

            int PStatus = Common.CastAsInt32(dt.Rows[0]["Status"]);
            switch (PStatus)
            {
                case -1:
                    lblPeapStatus.Text = "PEAP Cancelled";
                    break;
                case 0:
                    lblPeapStatus.Text = "Self Assessment";
                    break;
                case 1:
                    lblPeapStatus.Text = "Self Assessment";
                    break;
                case 2:
                    lblPeapStatus.Text = "Assessment by Appraiser";
                    break;
                case 3:
                    lblPeapStatus.Text = "Assessment by MD";
                    break;
                case 4:
                    lblPeapStatus.Text = "PEAP Closed";
                    break;
                default:
                    lblPeapStatus.Text = "NA";
                    break;
            }
            lblPeapStatus.Text = "Current Status : " + lblPeapStatus.Text;

            //if (Mode == "A")
            //{
            //    btnSaveJR.Visible = false;
            //    btnSave_Comp.Visible = false;
            //    btnSave_Pot.Visible = false;
            //    btnNotify.Visible = false;
            //    Button1.Visible = false;
            //    Button2.Visible = false;

            //}
            //else
            //{
            //    if (dt.Rows[0]["Status"].ToString() == "2" && EmpID.ToString() == AppraiserId.ToString())
            //    {
            //        btnSaveJR.Visible = true;
            //        btnSave_Comp.Visible = true;
            //        btnSave_Pot.Visible = true;

            //        btnNotify.Visible = true;
            //        Button1.Visible = true;
            //        Button2.Visible = true;
            //    }
            //    else
            //    {
            //        btnSaveJR.Visible = false;
            //        btnSave_Comp.Visible = false;
            //        btnSave_Pot.Visible = false;

            //        btnNotify.Visible = false;
            //        Button1.Visible = false;
            //        Button2.Visible = false;
            //    }
            }

        }

    public void BindPlannedTrainings()
    {
        //string SQL = "SELECT TrainingId,TrainingName, '' AS LastDoneDate FROM HR_TrainingMaster " +
        //             "WHERE TrainingId NOT IN (SELECT TrainingId FROM HR_TrainingRecommended WHERE EmpId = (SELECT EmpId FROM HR_EmployeePeapMaster WHERE PeapId = " + PeapID + ")) ";

        //string SQL = "SELECT TrainingId,TrainingName, '' AS LastDoneDate " +
        //             "FROM HR_TrainingMaster " +
        //             "WHERE TrainingId NOT IN " +
        //             "( " +
        //             "	SELECT REC.TrainingId "+
        //             "	FROM HR_TrainingRecommended REC " +
        //             "	LEFT JOIN HR_TrainingPlanningDetails PLD " +
        //             "		INNER JOIN HR_TrainingPlanning PL ON PL.TrainingPlanningId=PLD.TrainingPlanningId AND ( PL.STATUS IS NULL OR PL.STATUS='A') " +
        //             "	ON PLD.TrainingRecommId=REC.TrainingRecommId " +
        //             "	WHERE REC.EMPID=(SELECT EmpId FROM HR_EmployeePeapMaster WHERE PeapId = " + PeapID + " ) " +
        //             ")" ;

        string SQL = "SELECT Distinct TM.TrainingId,TM.TrainingName,'' AS LastDoneDate,TG.TrainingGroupName " +
                     "FROM HR_TrainingMaster TM " +
                     "INNER JOIN HR_TrainingGroup TG ON TG.TrainingGroupId = TM.TrainingGroupId AND TG.StatusId = 'A' " +
                     "LEFT JOIN dbo.HR_TrainingPlanning TP ON TM.TrainingId = TP.TrainingId " +
                     "WHERE  TM.TrainingId NOT IN ( " +
                     "SELECT TrainingId FROM HR_TrainingPlanning TP1 " +
                     "WHERE TP1.TRAININGPLANNINGID IN (SELECT TPD.TRAININGPLANNINGID FROM dbo.HR_TrainingPlanningDetails TPD WHERE TPD.EMPID= (SELECT EmpId FROM HR_EmployeePeapMaster WHERE PeapId = " + PeapID + " ))) " +
                     "AND  TM.TrainingId NOT IN (SELECT TrainingId FROM HR_TrainingRecommended TR  WHERE TR.EmpId = (SELECT EmpId FROM HR_EmployeePeapMaster WHERE PeapId = " + PeapID + " ))";


        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(SQL);

        if (dt != null)
        {
            rptPlannedTrainings.DataSource = dt;
            rptPlannedTrainings.DataBind();

            BindGroups();
        }

    }

    protected void BindGroups()
    {
        for (int i = 0; i <= rptPlannedTrainings.Items.Count - 1; i ++)
        {
            Label lblGroup = (Label)rptPlannedTrainings.Items[i].FindControl("lblGroup");

            if (i == 0)
            {
                ViewState["Group"] = lblGroup.Text;
                lblGroup.Visible = true;
            }
            else
            {
                if (ViewState["Group"].ToString() != lblGroup.Text)
                {
                    ViewState["Group"] = lblGroup.Text;
                    lblGroup.Visible = true;
                }
                else
                {
                    lblGroup.Visible = false;
                }
            }

        }

        ViewState["Group"] = null;

    }

    protected void chkSelect_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkSelect = (CheckBox)sender;
        TextBox txtDueDt = (TextBox)((CheckBox)sender).FindControl("txtDueDt");

        if (chkSelect.Checked)
        {
            txtDueDt.Enabled = true;
            txtDueDt.Attributes.Add("required","yes");
        }
        else
        {
            txtDueDt.Enabled = false;
            txtDueDt.Attributes.Remove("required");
        }
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        bool IsChecked = false;
        foreach (RepeaterItem rpt in rptPlannedTrainings.Items)
        {
            CheckBox chkSelect = (CheckBox)rpt.FindControl("chkSelect");

            if (chkSelect.Checked)
            {
                IsChecked = true;
                break;
            }
        }

        if (!IsChecked)
        {
            lblMsg.Text = "Please select a training.";
            return;
        }

        foreach (RepeaterItem rpt in rptPlannedTrainings.Items)
        {
            CheckBox chkSelect = (CheckBox)rpt.FindControl("chkSelect");
            TextBox txtDueDt = (TextBox)rpt.FindControl("txtDueDt");

            if (chkSelect.Checked)
            {
                if (txtDueDt.Text.Trim() == "")
                {
                    lblMsg.Text = "Please enter due date.";
                    txtDueDt.Focus();
                    return;
                }
                DateTime dt;
                if (!DateTime.TryParse(txtDueDt.Text.Trim(), out dt))
                {
                    lblMsg.Text = "Please enter valid date.";
                    txtDueDt.Focus();
                    return;
                }
            }
        }

        // -------------------- Saving Training ----------------------------------

        bool success = true; 

        foreach (RepeaterItem rpt in rptPlannedTrainings.Items)
        {
            CheckBox chkSelect = (CheckBox)rpt.FindControl("chkSelect");
            TextBox txtDueDt = (TextBox)rpt.FindControl("txtDueDt");
            HiddenField hfTrainingId = (HiddenField)rpt.FindControl("hfTrainingId");

            if (chkSelect.Checked)
            {
                Common.Set_Procedures("DBO.HR_InsertPeapTrainingRecommanded");
                Common.Set_ParameterLength(4);
                Common.Set_Parameters(
                        new MyParameter("@PeapId", PeapID),
                        new MyParameter("@TrainingId", hfTrainingId.Value.Trim()),
                        new MyParameter("@RecommBy", AppraiserId),
                        new MyParameter("@DueDate", txtDueDt.Text.Trim())
                    );
                Boolean Res;
                DataSet ds = new DataSet();
                Res = Common.Execute_Procedures_IUD(ds);
                if (Res)
                {
                    
                }
                else
                {
                    success = false;
                    break;
                    
                }
            }

        }
        if (success)
        {
            BindPlannedTrainings();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "refresh", "refreshparent();", true);
            lblMsg.Text = "Data saved successfully.";
        }
        else
        {
            lblMsg.Text = "Unable to save data.";
        }
           
    }
}
