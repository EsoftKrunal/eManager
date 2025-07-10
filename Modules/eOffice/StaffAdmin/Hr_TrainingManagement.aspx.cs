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

public partial class emtm_StaffAdmin_Emtm_Hr_TrainingManagement : System.Web.UI.Page
{

    public string OrderBy
    {
        set { ViewState["OrderBy"] = value; }
        get { try { return ViewState["OrderBy"].ToString(); } catch { return " order by TrainingName"; } }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        lblMsg.Text = "";
        if (!IsPostBack)
        {

            ControlLoader.LoadControl(ddlOffice, DataName.Office, " All ", "0");
            ControlLoader.LoadControl(ddlPosition, DataName.Position, "All", "0", "" );
            BindTraining();
            BindTrainingType();
            BindDepartment();            
            ShowCrew();

        }
    }
    protected void btnSearch_OnClick(object sender, EventArgs e)
    {
        ShowCrew();
    }
    protected void btnClear_OnClick(object sender, EventArgs e)
    {
        ddlTrainingName.SelectedIndex = 0;
        ddlDepartment.SelectedIndex = 0;
        ddlStatus.SelectedIndex = 0;
        ddltypeOfTraining.SelectedIndex = 0;
        txtEmpName.Text = "";
        txtFromDt.Text = "";
        txtToDt.Text = "";
        chkOverDue.Checked = false;
        ShowCrew();
    }
    protected void btnPlan_OnClick(object sender, EventArgs e)
    {
        string TRID="";
        int TrainingID = 0;
        foreach (RepeaterItem Itm in rptCrew.Items)
        {
            CheckBox Chk = (CheckBox)Itm.FindControl("chkDue");
            if (Chk.Checked)
            {
                HiddenField hfTrainingID = (HiddenField)Itm.FindControl("hfTrainingID");
                if (TrainingID == 0)
                    TrainingID = Common.CastAsInt32(hfTrainingID.Value);
                else if (TrainingID != Common.CastAsInt32(hfTrainingID.Value))
                {
                    lblMsg.Text = "Please select same type of training.";
                    return;
                }


                TRID = TRID + "," + Chk.CssClass;
            }
        }
        if (TRID != "")
            TRID = TRID.Substring(1);

        Session.Add("TRIDs", TRID);
        //ScriptManager.RegisterStartupScript(Page, this.GetType(), "", "OpenRecommTraining()", true);
        Page.ClientScript.RegisterStartupScript(this.GetType(), "POPUP", "OpenRecommTraining();", true);
    }
    protected void Sorting(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        OrderBy = " order by " + lnk.CommandArgument;
        ShowCrew();
    }
    protected void ddlOffice_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindDepartment();
        if(ddlOffice.SelectedIndex==0)
            ControlLoader.LoadControl(ddlPosition, DataName.Position, "All", "0", "" );
        else
            ControlLoader.LoadControl(ddlPosition, DataName.Position, "All", "0", "officeid=" + ddlOffice.SelectedValue);
    }
    //   -------------------    Function
    public void ShowCrew()
    {
        string sql = "";
        string WC = "";

        sql = " SELECT TR.TRAININGID,TR.TrainingRecommID,A.TrainingPlanningID,EPD.EMPCODE,(ISNULL(EPD.FirstName,'')+ ' ' +ISNULL(EPD.FamilyName,'')) as EmpName,TM.TRAININGID,TM.TRAININGNAME,TR.DUEDATE,A.STARTDATE AS PLANSTART,A.ENDDATE AS PLANEND,A.STATUS,A.CANCELLEDON ,POS.PositionCode, " +
               " (Case when (DUEDATE <=GETDATE() And (A.STATUS IS NULL OR A.STATUS='A' OR A.STATUS='C')) then 'OverDue' else ''end)IsOverDue," +
              " (CASE A.STATUS WHEN 'C' THEN 'Cancelled' WHEN 'E' THEN 'Completed' WHEN 'A' THEN 'Planned' ELSE 'Due'  END) StatusName , " +
              " (CASE A.STATUS WHEN 'C' THEN 'Cancelled' WHEN 'E' THEN 'Completed' WHEN 'A' THEN 'Planned' ELSE 'Due'  END) StatusCss , " +
              " A.STARTDATE1 AS CompStart,A.ENDDATE1 AS CompEnd,TM.ValidityPeriod  " +
              " FROM dbo.Emtm_TrainingRecommended  TR " +
              " LEFT JOIN  " +
              "   ( " +
              "   SELECT PD.TRAININGPLANNINGID,PD.TRAININGRECOMMID,PM.STARTDATE,PM.ENDDATE,PM.STARTDATE1,PM.ENDDATE1,PM.CANCELLEDON,PM.STATUS FROM dbo.HR_TrainingPlanningDetails PD " +
              "   INNER JOIN dbo.HR_TrainingPlanning PM ON PM.TRAININGPLANNINGID=PD.TRAININGPLANNINGID   " +
              "   ) A ON TR.TRAININGRECOMMID=A.TRAININGRECOMMID " +
              "   INNER JOIN dbo.HR_TrainingMaster TM ON TM.TRAININGID=TR.TRAININGID " +
              "   INNER JOIN dbo.Hr_PersonalDetails EPD ON EPD.EMPID=TR.EMPID " +
              "   left JOIN dbo.HR_Department D ON D.DEPTID=EPD.DEPARTMENT " +
              "   left JOIN dbo.Position POS ON POS.PositionID=EPD.Position Where 1=1 ";


        if (ddltypeOfTraining.SelectedIndex != 0)
            WC = WC + " And TM.TRAININGGROUPID =" + ddltypeOfTraining.SelectedValue;
        if (ddlTrainingName.SelectedIndex != 0)
            WC = WC + " And TM.TRAININGID=" + ddlTrainingName.SelectedValue;

        if (chkOverDue.Checked == false)
        {
            if (ddlStatus.SelectedIndex != 0)
            {
                if (ddlStatus.SelectedIndex == 1)
                    WC = WC + " And A.STATUS IS NULL ";
                else
                    WC = WC + " And A.STATUS='" + ddlStatus.SelectedValue + "'";
            }
        }

        else if (ddlOffice.SelectedIndex != 0)
            WC = WC + " And EPD.Office = " + ddlOffice.SelectedValue;
        if (ddlDepartment.SelectedIndex != 0)
            WC = WC + " And D.DEPTID = " + ddlDepartment.SelectedValue;
        if (ddlPosition.SelectedIndex != 0)
            WC = WC + " And EPD.Position =" + ddlPosition.SelectedValue;
        if (txtEmpName.Text.Trim() != "")
            WC = WC + " And (EPD.FirstName+ ' ' +EPD.FamilyName) like '%" + txtEmpName.Text.Trim() + "%'";
        // --- date fileter
        if (ddlStatus.SelectedIndex != 0)
        {
            if (ddlStatus.SelectedIndex == 1) // Due
                WC = WC + " " + ((txtFromDt.Text.Trim() == "") ? "" : "And TR.DUEDATE >= '" + txtFromDt.Text.Trim() + "'") + " " + ((txtToDt.Text.Trim() == "") ? "" : "And TR.DUEDATE <= '" + txtToDt.Text.Trim() + "'") + "";
            if (ddlStatus.SelectedIndex == 2) // Planned            
                WC = WC + " " + ((txtFromDt.Text.Trim() == "") ? "" : "And A.STARTDATE >= '" + txtFromDt.Text.Trim() + "'") + " " + ((txtToDt.Text.Trim() == "") ? "" : "And A.STARTDATE <= '" + txtToDt.Text.Trim() + "'") + "";
            if (ddlStatus.SelectedIndex == 3) // Completed            
                WC = WC + " " + ((txtFromDt.Text.Trim() == "") ? "" : "And A.ENDDATE1 >= '" + txtFromDt.Text.Trim() + "'") + " " + ((txtToDt.Text.Trim() == "") ? "" : "And A.ENDDATE1 <= '" + txtToDt.Text.Trim() + "'") + "";
            if (ddlStatus.SelectedIndex == 4) // Cancelled
                WC = WC + " " + ((txtFromDt.Text.Trim() == "") ? "" : "And A.CANCELLEDON >= '" + txtFromDt.Text.Trim() + "'") + " " + ((txtToDt.Text.Trim() == "") ? "" : "And A.CANCELLEDON <= '" + txtToDt.Text.Trim() + "'") + "";
        }
        if(chkOverDue.Checked)
            WC = WC + " And (DUEDATE <=GETDATE() And (A.STATUS IS NULL OR A.STATUS='A' OR A.STATUS='C'))";
        
        sql = sql + WC + OrderBy;
        DataSet ds = Budget.getTable(sql);
        rptCrew.DataSource = ds;
        rptCrew.DataBind();
    }
    public void BindTraining()
    {
        string sql = "Select TrainingID,TrainingName from HR_TrainingMaster";
        DataSet ds = Budget.getTable(sql);
        
        ddlTrainingName.DataSource = ds;
        ddlTrainingName.DataTextField = "TrainingName";
        ddlTrainingName.DataValueField = "TrainingID";
        ddlTrainingName.DataBind();
        ddlTrainingName.Items.Insert(0, new ListItem("< All >", "0"));
    }
    public void BindTrainingType()
    {
        string sql = "Select TrainingGroupID,TrainingGroupName from HR_TrainingGroup";
        DataSet ds = Budget.getTable(sql);

        ddltypeOfTraining.DataSource = ds;
        ddltypeOfTraining.DataTextField = "TrainingGroupName";
        ddltypeOfTraining.DataValueField = "TrainingGroupID";
        ddltypeOfTraining.DataBind();
        ddltypeOfTraining.Items.Insert(0, new ListItem("< All >", "0"));
    }
    public void BindDepartment()
    {
        string sql = "Select DeptID,DeptName from HR_Department Where OfficeID="+ddlOffice.SelectedValue+"";
        DataSet ds = Budget.getTable(sql);

        ddlDepartment.DataSource = ds;
        ddlDepartment.DataTextField = "DeptName";
        ddlDepartment.DataValueField = "DeptID";
        ddlDepartment.DataBind();
        ddlDepartment.Items.Insert(0, new ListItem("< All >", "0"));
    }


    //   -------------------    Old Functions
    public void BindYearTrainings()
    {
       
            //string strSQL = "SELECT TrainingPlanningId,TP.TrainingId,TM.TrainingName,Location,Replace(Convert(varchar(11),StartDate,106),' ', '-') AS StartDate,Replace(Convert(varchar(11),EndDate,106),' ', '-') AS EndDate,StartTime,EndTime,[Status],CancelledByUser,Replace(Convert(varchar(11),CancelledOn,106),' ', '-') AS CancelledOn,CompletedByUser,Replace(Convert(varchar(11),CompletedOn,106),' ', '-') AS CompletedOn,Location1,Replace(Convert(varchar(11),StartDate1,106),' ', '-') AS StartDate1,Replace(Convert(varchar(11),EndDate1,106),' ', '-') AS EndDate1,StartTime1,EndTime1 FROM HR_TrainingPlanning TP " +
            //                "INNER JOIN HR_TrainingMaster TM ON TP.TrainingId = TM.TrainingId WHERE YEAR(StartDate) = " + ddlYear.SelectedValue.Trim() + " AND MONTH(StartDate) = " + i + " ORDER BY StartDate ASC";

    }
    public void BindRequirements()
    {
            //string strSQL = "SELECT COUNT(*) AS Requirement FROM Emtm_TrainingRecommended WHERE YEAR(DueDate) =" + ddlYear.SelectedValue.Trim() + " AND MONTH(DueDate) = " + i + " AND TrainingRecommId NOT IN (SELECT TrainingRecommId FROM HR_TrainingPlanningDetails P1 WHERE P1.TrainingPlanningId In (SELECT P2.TrainingPlanningId from HR_TrainingPlanning P2 WHERE P2.Status <> 'C'))";
            //DataTable dtReq = Common.Execute_Procedures_Select_ByQueryCMS(strSQL);            
    }
    #region --------------- Training Details -------------
    protected void CalTrainingDetails_DayRender(object sender, DayRenderEventArgs e)
    {
        //string strSQL = "SELECT TrainingPlanningId,TP.TrainingId,TM.TrainingName,Location,Replace(Convert(varchar(11),StartDate,106),' ', '-') AS StartDate,Replace(Convert(varchar(11),EndDate,106),' ', '-') AS EndDate,StartTime,EndTime,[Status],DAY(StartDate) as StartDay,DAY(EndDate) as EndDay FROM HR_TrainingPlanning TP " +
        //                "INNER JOIN HR_TrainingMaster TM ON TP.TrainingId = TM.TrainingId WHERE YEAR(StartDate) = " + ddlYear.SelectedValue.Trim() + " AND MONTH(StartDate) = " + Month + " ORDER BY DAY(StartDate) ";

        
        
    }
    #endregion
    protected void btnHidden_Click(object sender, EventArgs e)
    {
        int TrainingPlanningId = Common.CastAsInt32(txtHidden.Text);

        ScriptManager.RegisterStartupScript(this, this.GetType(), "asdf", "OpenPlanningDetails('" + TrainingPlanningId.ToString() + "');", true);
        
    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        BindYearTrainings();
        BindRequirements();
        
    }
    protected void btnPlanTraining_Click(object sender, EventArgs e)
    {
        //Month = DateTime.Today.Month;
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "asdf", "OpenRecomm('" + ddlYear.SelectedValue.Trim() + "','" + Month + "','P');", true);
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "tr", "SetLastFocus('dvscroll_TM');", true);
    }
}
