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

public partial class emtm_Profile_Emtm_PopUp_Profile_TrainingDetails : System.Web.UI.Page
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
    public string Status
    {
        get
        {
            return ""+ViewState["Status"];
        }
        set
        {
            ViewState["Status"] = value;
        }
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        if (!IsPostBack)
        {
            if ((Request.QueryString["TrainingPlanningId"] != null && Request.QueryString["TrainingPlanningId"].ToString() != ""))
            {
                TrainingPlanningId = Common.CastAsInt32(Request.QueryString["TrainingPlanningId"].ToString());                
                ShowTrainingPlanningDetails();
                BindEmployees();                
                TrainingStatus();
            }
        }
    }
    public void TrainingStatus()
    {
        string SQL = "SELECT [Status] FROM HR_TrainingPlanning WHERE TrainingPlanningId =" + TrainingPlanningId + " ";
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(SQL);

        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["Status"].ToString() == "C")
            {
                trCanRemarks.Visible = true;
                lblCancelBy.Visible = true; 
                lblCancelledBy.Visible = true; 
                lblCancelOn.Visible = true; 
                lblCancelledOn.Visible = true;
            }
            if (dt.Rows[0]["Status"].ToString() == "E")
            {
                trPlanningDesc.Visible = true;
                divComplationDetails.Visible = true;
                divComplationDetails1.Visible = true;
                divComplationDetails2.Visible = true;
                divComplationDetails3.Visible = true;
                trTrainingStatus.Visible = false;
            }
        }
    }
   
    #region ---------- Planning Details ------------------
    
    public void ShowTrainingPlanningDetails()
    {
        string strSQL = "SELECT TrainingPlanningId,TP.TrainingId,TM.TrainingName,Location,Replace(Convert(varchar(11),StartDate,106),' ', '-') AS StartDate,Replace(Convert(varchar(11),EndDate,106),' ', '-') AS EndDate,StartTime,EndTime,Location1,Replace(Convert(varchar(11),StartDate1,106),' ', '-') AS StartDate1,Replace(Convert(varchar(11),EndDate1,106),' ', '-') AS EndDate1,StartTime1,EndTime1,CASE WHEN [Status] = 'A' THEN 'Planned' WHEN [Status] = 'C' THEN 'Cancelled' WHEN [Status] = 'E' THEN 'Executed' ELSE '' END AS [Status],Cost,Currency,ExcRate,USD,TP.CancelRemarks,Replace(Convert(varchar(11),CancelledOn,106),' ', '-') AS CancelledOn,(select FirstName + ' ' + Lastname from userlogin WHERE LoginId = ISNULL(TP.CancelledByUser,0)) As cancelledBy FROM HR_TrainingPlanning TP " +
                        "INNER JOIN HR_TrainingMaster TM ON TP.TrainingId = TM.TrainingId WHERE TrainingPlanningId = " + TrainingPlanningId;

         DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(strSQL);

        if (dt != null)
        {
            lblTrainingName.Text = dt.Rows[0]["TrainingName"].ToString();
            lblLocation.Text = dt.Rows[0]["Location"].ToString();
            lblStartDt.Text = dt.Rows[0]["StartDate"].ToString();
            lblEndDt.Text = dt.Rows[0]["EndDate"].ToString();
            lblStartTime.Text = dt.Rows[0]["StartTime"].ToString();
            lblEndTime.Text = dt.Rows[0]["EndTime"].ToString();
            lblStatus.Text = dt.Rows[0]["Status"].ToString();
            lblCancelledBy.Text = dt.Rows[0]["cancelledBy"].ToString();
            lblCancelledOn.Text = dt.Rows[0]["CancelledOn"].ToString();
            lblremarks.Text = dt.Rows[0]["CancelRemarks"].ToString();

            hfTrainingId.Value = dt.Rows[0]["TrainingId"].ToString();
            TrainingId = Common.CastAsInt32(dt.Rows[0]["TrainingId"].ToString());

            lblUTSDT.Text = dt.Rows[0]["StartDate1"].ToString() + " " +dt.Rows[0]["StartTime1"].ToString();
            lblUTEDT.Text = dt.Rows[0]["EndDate1"].ToString() + " " + dt.Rows[0]["EndTime1"].ToString();
            lblUTLocation.Text = dt.Rows[0]["Location1"].ToString();
            lblUTCost.Text = dt.Rows[0]["Cost"].ToString();

            lblUTCurr.Text = dt.Rows[0]["Currency"].ToString();
            lblUTExcRate.Text = dt.Rows[0]["ExcRate"].ToString();
            lblUTUSD.Text = dt.Rows[0]["USD"].ToString();
            
        }
    }

    #endregion

    #region ----------- Employee Details --------------------

    public void BindEmployees()
    {
        string strSQL = "SELECT EmpCode,PD.EmpId,(FirstName + ' ' + FamilyName) As EmpName,DJC,P.PositionName,O.OfficeName,D.DeptName AS DepartmentName,TRD.Important,Source=(Case when TRD.RecommSource='A' then 'Assigned' when TRD.RecommSource='P' then 'PEAP' else 'NA' END),TPD.TrainingRecommId,(SELECT [Status] FROM HR_TrainingPlanning WHERE TrainingPlanningId =" + TrainingPlanningId + ") AS status " + 
                        "FROM HR_TrainingPlanningDetails TPD " +
                        "INNER JOIN HR_TrainingRecommended TRD ON TPD.TrainingRecommId = TRD.TrainingRecommId " +
                        "INNER JOIN Hr_PersonalDetails PD ON TPD.EmpId = PD.EmpId " +
                        "LEFT JOIN Position P ON P.PositionId = TPD.PositionId " +
                        "LEFT JOIN Office O ON O.OfficeId = TPD.OfficeId " +
                        "LEFT JOIN HR_Department D ON D.DeptId = TPD.DepartmentId " +
                        "WHERE TrainingPlanningId =" + TrainingPlanningId + " ";

        DataTable dtEmp = Common.Execute_Procedures_Select_ByQueryCMS(strSQL);

        if (dtEmp != null)
        {
            if (dtEmp.Rows.Count > 0)
            {
                Status = dtEmp.Rows[0]["status"].ToString();
            }
            else
            {
                Status = "A";
            }
            rptEmpDetails.DataSource = dtEmp;
            rptEmpDetails.DataBind();
        }
    }

    #endregion
   
}
