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

public partial class emtm_StaffAdmin_Emtm_PopUpTrainingDetails : System.Web.UI.Page
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
    public string Mode
    {
        set { ViewState["Mode"] = value; }
        get { return Convert.ToString(ViewState["Mode"]); }
    }
    public void TrainingStatus()
    {
        string SQL = "SELECT [Status] FROM HR_TrainingPlanning WHERE TrainingPlanningId =" + TrainingPlanningId + " ";
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(SQL);

        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["Status"].ToString() == "C")
            {
                tblActions.Visible = false;
                btnEdit.Visible = false; 
                lblCancelBy.Visible = true; 
                lblCancelledBy.Visible = true; 
                lblCancelOn.Visible = true; 
                lblCancelledOn.Visible = true;
                
            }
            if (dt.Rows[0]["Status"].ToString() == "E")
            {
                tblActions.Visible = false;
                btnEdit.Visible = false;

                tblDoneDetails.Visible = true;
            }
        }
    }
    public void BindTime()
    {
        for (int i = 0; i < 24; i++)
        {
            if (i < 10)
            {
                ddlStHr.Items.Add(new ListItem("0" + i.ToString(), "0" + i.ToString()));
                ddlEtHr.Items.Add(new ListItem("0" + i.ToString(), "0" + i.ToString()));

                ddlSTHour1.Items.Add(new ListItem("0" + i.ToString(), "0" + i.ToString()));
                ddlETHour1.Items.Add(new ListItem("0" + i.ToString(), "0" + i.ToString()));
            }
            else
            {
                ddlStHr.Items.Add(new ListItem(i.ToString(), i.ToString()));
                ddlEtHr.Items.Add(new ListItem(i.ToString(), i.ToString()));

                ddlSTHour1.Items.Add(new ListItem(i.ToString(), i.ToString()));
                ddlETHour1.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
        }

        for (int i = 0; i < 60; i++)
        {
            if (i < 10)
            {
                ddlStMin.Items.Add(new ListItem("0" + i.ToString(), "0" + i.ToString()));
                ddlEtMin.Items.Add(new ListItem("0" + i.ToString(), "0" + i.ToString()));

                ddlSTMin1.Items.Add(new ListItem("0" + i.ToString(), "0" + i.ToString()));
                ddlETMin1.Items.Add(new ListItem("0" + i.ToString(), "0" + i.ToString()));
            }
            else
            {
                ddlStMin.Items.Add(new ListItem(i.ToString(), i.ToString()));
                ddlEtMin.Items.Add(new ListItem(i.ToString(), i.ToString()));

                ddlSTMin1.Items.Add(new ListItem(i.ToString(), i.ToString()));
                ddlETMin1.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
        }
    }
    protected void LoadCurrency()
    {

        //System.Data.SqlClient.SqlConnection con=new System.Data.SqlClient.SqlConnection("Data Source=172.30.1.10;Initial Catalog=mtmm2000sql;Persist Security Info=True;User Id=sa;Password=Esoft^%$#@!");
        System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["eMANAGER"].ToString());
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("select distinct for_curr from XCHANGEDAILY order by for_curr",con);
        System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        try
        {
            adp.Fill(dt);
        }
        catch {
            con.ConnectionString = "Data Source=172.30.1.10;Initial Catalog=mtmm2000sql;Persist Security Info=True;User Id=sa;Password=Esoft^%$#@!";
            adp.Fill(dt);
        } 
        ddlCurrency.DataSource = dt;
        ddlCurrency.DataTextField = "for_curr";
        ddlCurrency.DataTextField = "for_curr";
        ddlCurrency.DataBind(); 
		ddlCurrency.Items.Insert(0, new ListItem("< Select >","0"));
    }
    protected decimal getExchangeRates(string Curr)
    {
        decimal rate=0;
       
        System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["eMANAGER"].ToString());
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("select TOP 1 EXC_RATE from XCHANGEDAILY where for_curr='" + Curr + "' AND RATEDATE <='" + DateTime.Today.ToString("dd-MMM-yyyy")  + "' order by ratedate desc", con);
        System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        if (dt.Rows.Count > 0)
            rate = Common.CastAsDecimal(dt.Rows[0][0]);
        return rate;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        lblAddMsg.Text = "";
        lblUpdateTraining.Text = "";
        lblChangePlan.Text = "";
        lblCanMsg.Text = "";

        if (!IsPostBack)
        {
            if ((Request.QueryString["TrainingPlanningId"] != null && Request.QueryString["TrainingPlanningId"].ToString() != ""))
            {
                LoadCurrency();
                BindPosition();
                TrainingPlanningId = Common.CastAsInt32(Request.QueryString["TrainingPlanningId"].ToString());
                BindTime();
                ShowTrainingPlanningDetails();
                BindEmployees();
                BindRecomm();
                TrainingStatus();
            }
        }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        Mode = "E";
        //----------------------- Controls
        lblLocation.Visible = false;
        lblStartDt.Visible = false;
        lblEndDt.Visible = false;
        lblStartTime.Visible = false;
        lblEndTime.Visible = false;

        txtLocation.Visible = true;
        txtStartDt.Visible = true;
        txtEndDt.Visible = true;
        ddlStHr.Visible = true;
        ddlStMin.Visible = true;              
        ddlEtHr.Visible = true;
        ddlEtMin.Visible = true;        
        Span1.Visible = true;
        Span2.Visible = true;
        //-----------------------
        btnEdit.Visible = false;
        btnSave.Visible = true;
        btnCancel.Visible = true;
        BindEmployees();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (txtStartDt.Text.Trim() == "")
        {
            lblChangePlan.Text = "Please select start date.";
            txtStartDt.Focus();
            return;
        }
        DateTime dt;
        if (!DateTime.TryParse(txtStartDt.Text.Trim(), out dt))
        {
            lblChangePlan.Text = "Please enter valid date.";
            txtStartDt.Focus();
            return;
        }
        if (txtEndDt.Text.Trim() == "")
        {
            lblChangePlan.Text = "Please select end date.";
            txtEndDt.Focus();
            return;
        }
        DateTime dt1;
        if (!DateTime.TryParse(txtEndDt.Text.Trim(), out dt1))
        {
            lblChangePlan.Text = "Please enter valid date.";
            txtEndDt.Focus();
            return;
        }

        if (Convert.ToDateTime(txtStartDt.Text.Trim()) > Convert.ToDateTime(txtEndDt.Text.Trim()))
        {
            lblChangePlan.Text = "Start date must be less than end date.";
            txtStartDt.Focus();
            return;
        }
                
        if (txtLocation.Text.Trim() == "")
        {
            lblChangePlan.Text = "Please enter location.";
            txtLocation.Focus();
            return;
        }


        string StartTime = ddlStHr.SelectedValue.Trim() + ":" + ddlStMin.SelectedValue.Trim();
        string EndTime = ddlEtHr.SelectedValue.Trim() + ":" + ddlEtMin.SelectedValue.Trim();

        Common.Set_Procedures("HR_InsertUpdateTrainingPlanning");
        Common.Set_ParameterLength(7);
        Common.Set_Parameters(new MyParameter("@TrainingPlanningId", TrainingPlanningId),
            new MyParameter("@TrainingId", hfTrainingId.Value.Trim()),
            new MyParameter("@Location", txtLocation.Text.Trim()),
            new MyParameter("@StartDate", txtStartDt.Text.Trim()),
            new MyParameter("@EndDate", txtEndDt.Text.Trim()),
            new MyParameter("@StartTime", StartTime.Trim()),
            new MyParameter("@EndTime", EndTime.Trim()));
        DataSet ds = new DataSet();

        if (Common.Execute_Procedures_IUD_CMS(ds))
        {
            ShowTrainingPlanningDetails();
            btnCancel_Click(sender,e);
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Record saved successfully.');", true);
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "RefreshParent();", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Unable to save record.');", true);
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Mode = "";
        //----------------------- Controls
        lblLocation.Visible = true;
        lblStartDt.Visible = true;
        lblEndDt.Visible = true;
        lblStartTime.Visible = true;
        lblEndTime.Visible = true;


        txtLocation.Visible = false;
        txtStartDt.Visible = false;
        txtEndDt.Visible = false;
        ddlStHr.Visible = false;
        ddlStMin.Visible = false;
        ddlEtHr.Visible = false;
        ddlEtMin.Visible = false;
        Span1.Visible = false;
        Span2.Visible = false;
        //-----------------------

        btnEdit.Visible = true;
        btnSave.Visible = false;
        btnCancel.Visible = false;
        BindEmployees();
    }
    
    public void ShowTrainingPlanningDetails()
    {
        string strSQL = "SELECT TrainingPlanningId,TP.TrainingId,TM.TrainingName,Location,Replace(Convert(varchar(11),StartDate,106),' ', '-') AS StartDate,Replace(Convert(varchar(11),EndDate,106),' ', '-') AS EndDate,StartTime,EndTime,Location1,Replace(Convert(varchar(11),StartDate1,106),' ', '-') AS StartDate1,Replace(Convert(varchar(11),EndDate1,106),' ', '-') AS EndDate1,StartTime1,EndTime1,CASE WHEN [Status] = 'A' THEN 'Planned' WHEN [Status] = 'C' THEN 'Cancelled' WHEN [Status] = 'E' THEN 'Executed' ELSE '' END AS [Status],Currency ,convert( Decimal(9,2),Cost)Cost,convert( Decimal(9,2),ExcRate)ExcRate,convert( Decimal(9,2),USD)USD,TP.CancelRemarks,Replace(Convert(varchar(11),CancelledOn,106),' ', '-') AS CancelledOn,(select FirstName + ' ' + Lastname from userlogin WHERE LoginId = ISNULL(TP.CancelledByUser,0)) As cancelledBy FROM HR_TrainingPlanning TP " +
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

            string[] strSTime = dt.Rows[0]["StartTime"].ToString().Split(':');
            string[] strETime = dt.Rows[0]["EndTime"].ToString().Split(':');

            hfTrainingId.Value = dt.Rows[0]["TrainingId"].ToString();
            TrainingId = Common.CastAsInt32(dt.Rows[0]["TrainingId"].ToString());
            txtLocation.Text = dt.Rows[0]["Location"].ToString();
            txtStartDt.Text = dt.Rows[0]["StartDate"].ToString();
            txtEndDt.Text = dt.Rows[0]["EndDate"].ToString();
            ddlStHr.SelectedValue = strSTime[0].Trim();
            ddlStMin.SelectedValue = strSTime[1].Trim();
            ddlEtHr.SelectedValue = strETime[0].Trim();
            ddlEtMin.SelectedValue = strETime[1].Trim();

            txtLocation1.Text = dt.Rows[0]["Location1"].ToString();
            txtStartDt1.Text = dt.Rows[0]["StartDate1"].ToString();
            txtEndDt1.Text = dt.Rows[0]["EndDate1"].ToString();

            if (dt.Rows[0]["StartTime1"].ToString() != "")
            {
                string[] strSTime1 = dt.Rows[0]["StartTime1"].ToString().Split(':');
                ddlSTHour1.SelectedValue = strSTime[0].Trim();
                ddlSTMin1.SelectedValue = strSTime[1].Trim();

            }
            if (dt.Rows[0]["EndTime1"].ToString() != "")
            {
                string[] strETime1 = dt.Rows[0]["EndTime1"].ToString().Split(':');
                ddlETHour1.SelectedValue = strETime[0].Trim();
                ddlETMin1.SelectedValue = strETime[1].Trim();
            }

            lblUTSDT.Text = dt.Rows[0]["StartDate1"].ToString() + " " + dt.Rows[0]["StartTime1"].ToString();
            lblUTEDT.Text = dt.Rows[0]["EndDate1"].ToString() + " " + dt.Rows[0]["EndTime1"].ToString();
            lblUTLocation.Text = dt.Rows[0]["Location1"].ToString();
            lblUTCost.Text = dt.Rows[0]["Cost"].ToString();

            lblUTCurr.Text = dt.Rows[0]["Currency"].ToString();
            lblUTExcRate.Text = dt.Rows[0]["ExcRate"].ToString();
            lblUTUSD.Text = dt.Rows[0]["USD"].ToString();

        }
    }
    public void BindEmployees()
    {
        string strSQL = "SELECT EmpCode,PD.EmpId,(FirstName + ' ' + FamilyName) As EmpName,DJC,P.PositionName,O.OfficeName,D.DeptName AS DepartmentName,TRD.Important,Source=(Case when TRD.RecommSource='A' then 'Assigned' when TRD.RecommSource='P' then 'PEAP' when TRD.RecommSource='M' then 'MARTIX' else 'NA' END),TPD.TrainingRecommId,(SELECT [Status] FROM HR_TrainingPlanning WHERE TrainingPlanningId =" + TrainingPlanningId + ") AS status " + 
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
    public void BindRecomm()
    {
        //string str = "SELECT EmpCode,PD.EmpId,TPD.TrainingRecommId,(FirstName + '' + FamilyName) As EmpName,DJC,P.PositionName,O.OfficeName,D.DepartmentName FROM HR_TrainingRecommended TPD " +
        //             "INNER JOIN Hr_PersonalDetails PD ON TPD.EmpId = PD.EmpId " +
        //             "LEFT JOIN Position P ON P.PositionId = PD.Position " +
        //             "LEFT JOIN Office O ON O.OfficeId = PD.Office " +
        //             "LEFT JOIN Department D ON D.DepartmentId = PD.Department " +
        //             "WHERE TPD.TrainingId IN (SELECT PD1.TrainingId From HR_TrainingPlanning PD1 WHERE PD1.TrainingPlanningId=" + TrainingPlanningId + ") AND TrainingRecommId NOT IN (SELECT TrainingRecommId FROM HR_TrainingPlanningDetails P1 WHERE P1.TrainingPlanningId In (SELECT P2.TrainingPlanningId from HR_TrainingPlanning P2 WHERE P2.Status <> 'C')) " + 
        //             "ORDER BY EmpName ";
        //DataTable dtrecomm = Common.Execute_Procedures_Select_ByQueryCMS(str);

        //rptAddEmp.DataSource = dtrecomm;
        //rptAddEmp.DataBind();

        string strSQL = "SELECT TR1.TrainingRecommId,EmpCode,PD.EmpId,(FirstName + ' ' + FamilyName) As EmpName,DJC,P.PositionName,O.OfficeName,D.DeptName AS DepartmentName " +
                        "FROM Hr_PersonalDetails PD " +
                        "LEFT JOIN HR_TrainingRecommended TR1 ON TR1.EmpId = PD.EmpId And TR1.TrainingId=" + TrainingId.ToString() + " " + 
                        "LEFT JOIN Position P ON P.PositionId = PD.Position " +
                        "LEFT JOIN Office O ON O.OfficeId = PD.Office " +
                        "LEFT JOIN HR_Department D ON D.DeptId = PD.Department " +
                        "WHERE PD.EmpId NOT IN ( SELECT EmpId FROM HR_TrainingPlanningDetails WHERE TrainingPlanningId =" + TrainingPlanningId.ToString() + ") ";
                        //"WHERE PD.EmpId NOT IN ( SELECT EmpId FROM HR_TrainingPlanningDetails WHERE  TrainingId IN (SELECT P2.TrainingPlanningId from HR_TrainingPlanning P2 WHERE P2.Status <> 'C' )) ";
                        
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

    protected void Search_Click(object sender, EventArgs e)
    {
        BindRecomm();
    }
    protected void btnREDelete_Click(object sender, ImageClickEventArgs e)
    {
        int TrainingRecommId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("DELETE FROM HR_TrainingPlanningDetails WHERE TrainingRecommId = " + TrainingRecommId + " ; SELECT -1");
        if (dt.Rows.Count > 0)
        {
            BindEmployees();
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Employee deleted successfully.');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Unable to delete employee.');", true);
        }
    }
    protected void ddlCurrency_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCurrency.SelectedIndex != 0)
        {
            if (txtCost.Text.Trim() == "")
            {
                lblUpdateTraining.Text = "Please enter cost.";
                txtCost.Focus();
                return;
            }
            txtExcRate.Text = Convert.ToString(getExchangeRates(ddlCurrency.SelectedValue.Trim()));
            txtCostUSD.Text = Convert.ToString(Common.CastAsDecimal(txtCost.Text.Trim()) / Common.CastAsDecimal(txtExcRate.Text.Trim()));
        }
        else
        {
            txtExcRate.Text = "";
            txtCostUSD.Text = "";
        }

    }

    // update training
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        divUpdateTraining.Visible = true;
    }
    protected void btnUpdateTrainingDetails_Click(object sender, EventArgs e)
    {
        bool IsChecked = false;

        if (rptEmpDetails.Items.Count <= 0)
        {
            lblUpdateTraining.Text = "Please add employees to training.";
            return;
        }

        if (txtStartDt1.Text.Trim() == "")
        {
            lblUpdateTraining.Text = "Please enter start date.";
            txtStartDt1.Focus();
            return;
        }
        DateTime dt;
        if (!DateTime.TryParse(txtStartDt1.Text.Trim(), out dt))
        {
            lblUpdateTraining.Text = "Please enter valid date.";
            txtStartDt1.Focus();
            return;
        }
        if (txtEndDt1.Text.Trim() == "")
        {
            lblUpdateTraining.Text = "Please enter end date.";
            txtEndDt1.Focus();
            return;
        }
        DateTime dt1;
        if (!DateTime.TryParse(txtEndDt1.Text.Trim(), out dt1))
        {
            lblUpdateTraining.Text = "Please enter valid date.";
            txtEndDt1.Focus();
            return;
        }

        if (Convert.ToDateTime(txtStartDt1.Text.Trim()) > Convert.ToDateTime(txtEndDt1.Text.Trim()))
        {
            lblUpdateTraining.Text = "Start date must be less than end date.";
            txtStartDt1.Focus();
            return;
        }
        if (Convert.ToDateTime(txtEndDt1.Text.Trim()) > DateTime.Today)
        {
            lblUpdateTraining.Text = "End date can not be more than current date.";
            txtStartDt1.Focus();
            return;
        }
                
        if (txtLocation1.Text.Trim() == "")
        {
            lblUpdateTraining.Text = "Please enter location.";
            txtLocation1.Focus();
            return;
        }
   
        string StartTime = ddlSTHour1.SelectedValue.Trim() + ":" + ddlSTMin1.SelectedValue.Trim();
        string EndTime = ddlETHour1.SelectedValue.Trim() + ":" + ddlETMin1.SelectedValue.Trim();

        bool success = true;

        Common.Set_Procedures("HR_UpdateTrainingPlanning");
        Common.Set_ParameterLength(11);
        Common.Set_Parameters(new MyParameter("@TrainingPlanningId", TrainingPlanningId),
            new MyParameter("@StartDate1",  txtStartDt1.Text.Trim()),
            new MyParameter("@EndDate1", txtEndDt1.Text.Trim()),
            new MyParameter("@StartTime1", StartTime.Trim()),
            new MyParameter("@EndTime1", EndTime.Trim()),
            new MyParameter("@Location1", txtLocation1.Text.Trim()),
            new MyParameter("@Cost", Common.CastAsDecimal(txtCost.Text.Trim())),
            new MyParameter("@Currency", ddlCurrency.SelectedValue.Trim()),
            new MyParameter("@ExcRate", Common.CastAsDecimal(txtExcRate.Text.Trim())),
            new MyParameter("@USD", Common.CastAsDecimal(txtCostUSD.Text.Trim())),
            new MyParameter("@CompletedByUser", Session["loginid"].ToString()));
        DataSet ds = new DataSet();
        success = Common.Execute_Procedures_IUD_CMS(ds);
        if (success)
        {
            ShowTrainingPlanningDetails();
            BindEmployees();
            BindRecomm();
            tblActions.Visible = false;
            btnEdit.Visible = false; 
            btnHide_Click(sender, e);
            TrainingStatus();
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "RefreshParent();", true);
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Record saved successfully.');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Unable to save record.');", true);
        }
    }
    protected void btnHide_Click(object sender, EventArgs e)
    {
        divUpdateTraining.Visible = false;
    }
    // add attendees
    protected void btnAddMore_Click(object sender, EventArgs e)
    {
        dvAddEmp.Visible = true;
    }
    protected void btnAddEmp_Click(object sender, EventArgs e)
    {
        bool IsChecked = false;
        string RecommandId = "";
        foreach (RepeaterItem rpt in rptAddEmp.Items)
        {
            CheckBox chkSelect = (CheckBox)rpt.FindControl("chkSelect");
            HiddenField hfRecommId = (HiddenField)rpt.FindControl("hfRecommId");
            HiddenField hfEmpId = (HiddenField)rpt.FindControl("hfAddEmpId");

            if (chkSelect.Checked)
            {
                // ---------------- 
                if (hfRecommId.Value.Trim() == "")
                {
                    //----------- first recommed then 
                    Common.Set_Procedures("HR_InsertNonRecommanded");
                    Common.Set_ParameterLength(7);
                    Common.Set_Parameters(new MyParameter("@TrainingRecommId", 0),
                        new MyParameter("@EmpId", hfEmpId.Value.Trim()),
                        new MyParameter("@TrainingId", TrainingId),
                        new MyParameter("@RecommSource", "A"),
                        new MyParameter("@RecommBy", 1),
                        new MyParameter("@DueDate", null),
                        new MyParameter("@Important", "N"));
                    DataSet ds1 = new DataSet();

                    if (Common.Execute_Procedures_IUD_CMS(ds1))
                    {
                        hfRecommId.Value = ds1.Tables[0].Rows[0]["TrainingRecommId"].ToString().Trim();
                    }

                    //------------
                }
                RecommandId = RecommandId + hfRecommId.Value + ",";
                IsChecked = true;
            }

        }

        if (!IsChecked)
        {
            lblAddMsg.Text = "Please select atleast one employee.";
            return;
        }

        if (RecommandId.Length > 0)
        {
            RecommandId = RecommandId.Remove(RecommandId.Length - 1);
        }

        Common.Set_Procedures("HR_InsertTrainingPlanningDetails");
        Common.Set_ParameterLength(2);
        Common.Set_Parameters(new MyParameter("@TrainingPlanningId", TrainingPlanningId),
            new MyParameter("@TrainingRecommId", RecommandId.Trim()));
        DataSet ds = new DataSet();

        if (Common.Execute_Procedures_IUD_CMS(ds))
        {
            BindEmployees();
            BindRecomm();
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Record saved successfully.');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Unable to save record.');", true);
        }

    }
    protected void btnCloseAdding_Click(object sender, EventArgs e)
    {
        dvAddEmp.Visible = false;
    }
    // cancel training
    protected void btnCancelTraining_Click(object sender, EventArgs e)
    {
        tblCancellRemarks.Visible = true;
    }
    protected void btnCanSave_Click(object sender, EventArgs e)
    {
        if (txtRemarks.Text.Trim() == "")
        {
            lblCanMsg.Text = "Please enter remarks.";
            txtRemarks.Focus();
            return;
        }
        if (txtRemarks.Text.Trim().Length > 500)
        {
            lblCanMsg.Text = "Remarks can not be more than 500 characters.";
            txtRemarks.Focus();
            return;
        }
 
        Common.Set_Procedures("HR_CancelTrainingPlanning");
        Common.Set_ParameterLength(3);
        Common.Set_Parameters(new MyParameter("@TrainingPlanningId", TrainingPlanningId),
            new MyParameter("@Remarks", txtRemarks.Text.Trim()),
            new MyParameter("@CancelledByUser", Session["loginid"].ToString()));
        DataSet ds = new DataSet();

        if (Common.Execute_Procedures_IUD_CMS(ds))
        {
            ShowTrainingPlanningDetails();
            btnCanCancel_Click(sender, e);
            TrainingStatus();
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "RefreshParent();", true);
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Training cancelled successfully.');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Unable to cancel training.');", true);
        }

    }
    protected void btnCanCancel_Click(object sender, EventArgs e)
    {
        tblCancellRemarks.Visible = false;
        btnCancelTraining.Visible = true;
        
    }
}
