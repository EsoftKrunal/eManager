using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Net.Mail;
using System.Net;
using System.IO;

public partial class emtm_OfficeAbsence_Emtm_OfficeAbsence : System.Web.UI.Page
{
    public AuthenticationManager auth;
    //protected DataTable dsHolidays;
    //protected DataTable DtLeave;

    //User Defined Properties
    public int LeaveRequestId
    {
        get
        {
            return Common.CastAsInt32(ViewState["LeaveRequestId"]);
        }
        set
        {
            ViewState["LeaveRequestId"] = value;
        }
    }
    public int EmpId
    {
        get
        {
            return Common.CastAsInt32(ViewState["EmpId"]);
        }
        set
        {
            ViewState["EmpId"] = value;
        }
    }
    protected string LeaveColor
    {
        get
        {
            return ViewState["LeaveColor"].ToString();
        }
        set
        {
            ViewState["LeaveColor"] = value;
        }
    }
    public int OfficeId
    {
        get
        {
            return Common.CastAsInt32(ViewState["OfficeId"]);
        }
        set
        {
            ViewState["OfficeId"] = value;
        }
    }
    public int DepartmentId
    {
        get
        {
            return Common.CastAsInt32(ViewState["DepartmentId"]);
        }
        set
        {
            ViewState["DepartmentId"] = value;
        }
    } 
    public int InspGroupId
    {
        get
        {
            return Common.CastAsInt32(ViewState["InspGroupId"]);
        }
        set
        {
            ViewState["InspGroupId"] = value;
        }
    }
    public int InspId
    {
        get
        {
            return Common.CastAsInt32(ViewState["InspId"]);
        }
        set
        {
            ViewState["InspId"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        if (!Page.IsPostBack)
        {
            Bindyear();
            BindVessel();
            EmpId = Common.CastAsInt32(Session["ProfileId"]);
            if (EmpId > 0)
            {
                BindGrid();
                ControlLoader.LoadControl(ddlLeaveType, DataName.HR_LeavePurpose, "Select", "", "");
                if (Request.QueryString["OfficeId"] != null && Request.QueryString["DepartmentId"] != null)
                {
                    
                    ddlYear.SelectedValue = DateTime.Today.Year.ToString();
			year_Changed(sender,e);
                    OfficeId = Common.CastAsInt32(Request.QueryString["OfficeId"]);
                    DepartmentId = Common.CastAsInt32(Request.QueryString["DepartmentId"]);
                }

            }
            setButtons("");
	
        }

    }

    # region --- User Defined Functions ---
    protected void year_Changed(object sender, EventArgs e)
    {
        BindGrid();
    }
    protected void setButtons(string Action)
    {
        switch (Action)
        {
            case "View":
                tblview.Visible = true;
                btnsave.Visible = false;
                break;
            case "Add":
                tblview.Visible = true;
                btnsave.Visible = true && auth.IsUpdate;
                break;
            case "Show":
                break;
            default:
                //divLeaveDetail.Visible = true;
                break;
        }
    }
    protected void ClearControls()
    {
        ddlLocation.SelectedIndex = 0;
        ddlLeaveType.SelectedIndex = 0;
        ddlVessel.SelectedIndex = 0;
        trVessel.Visible = trSelInsp.Visible = false;
        lbSelectInsp.Text = "Select Inspections";
        hfInspIds.Value = "";
        lblInspections.Text = "";
        txtLeaveFrom.Text = "";
        txtLeaveTo.Enabled = true;
        imgLeaveTo.Enabled = true;
        txtLeaveTo.Text = "";
        txtReason.Text = "";
        chkHalfDay.Checked = false;
        DisableHalfdayRadio();
        btnNotify.Visible = false;
    }
    protected void EnableHalfdayRadio()
    {
        rdoFirstHalf.Enabled = true;
        rdoSecondHalf.Enabled = true;
    }
    protected void DisableHalfdayRadio()
    {
        rdoFirstHalf.Enabled = false;
        rdoSecondHalf.Enabled = false;
    }
    public string ShowLeaveDays()
    {
        if (txtLeaveFrom.Text.Trim() != "")
        {
            object d1 = (txtLeaveFrom.Text.Trim() == "") ? DBNull.Value : (object)txtLeaveFrom.Text;
            object d2 = (txtLeaveTo.Text.Trim() == "") ? DBNull.Value : (object)txtLeaveTo.Text;

            string sql = "select dbo.HR_Get_LeaveCount(" + OfficeId.ToString() + ",'" + d1 + "','" + d2 + "' )as Duration";
            DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
            return dt.Rows[0][0].ToString();
        }
        else
        {
            return "0.0";
        }
    }
    public string ShowAbsentDays()
    {
        object d1 = (txtLeaveFrom.Text.Trim() == "") ? DBNull.Value : (object)txtLeaveFrom.Text;
        object d2 = (txtLeaveTo.Text.Trim() == "") ? DBNull.Value : (object)txtLeaveTo.Text;

        if (txtLeaveFrom.Text.Trim() != "" && txtLeaveTo.Text.Trim() != "")
        {
            string sql = "select dbo.getAbsentDays(" + EmpId.ToString() + ",'" + d1 + "','" + d2 + "') as AbsentDays";
            DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
            return dt.Rows[0][0].ToString();
        }
        else
        {
            return "0";
        }
    }
    protected void chkHalfDay_CheckedChanged(object sender, EventArgs e)
    {
        if (chkHalfDay.Checked)
        {
            EnableHalfdayRadio();
            rdoFirstHalf.Checked = true;

            txtLeaveTo.Text = txtLeaveFrom.Text.Trim();
            txtLeaveTo.Enabled = false;
            imgLeaveTo.Enabled = false;
        }
        else
        {
            DisableHalfdayRadio();
            rdoFirstHalf.Checked = false;

            txtLeaveTo.Enabled = true;
            imgLeaveTo.Enabled = true;
        }

        string LeaveDays = ShowLeaveDays();
        //string AbsentDays = ShowAbsentDays();

        if (Common.CastAsInt32(LeaveDays) > Common.CastAsInt32(0) && chkHalfDay.Checked == true)
        {
            lblLeaveDays.Text = ".5 (Days)";
            //lblAbsentDays.Text = "0 (Days)";
        }
        else
        {
            lblLeaveDays.Text = LeaveDays + " (Days)";
            //lblAbsentDays.Text = AbsentDays + " (Days)";
        }

    }
    protected void Bindyear()
    {
        for(int i=DateTime.Today.Year+1;i>=2011;i--)
        {
            ddlYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
        }
   }    
    protected void btnhdn_Click(object sender, EventArgs e)
    {
        if (chkHalfDay.Checked)
        {
            txtLeaveTo.Text = txtLeaveFrom.Text.Trim();
            txtLeaveTo.Enabled = false;
            imgLeaveTo.Enabled = false;
        }
        else
        {
            txtLeaveTo.Enabled = true;
            imgLeaveTo.Enabled = true;
        }

        string LeaveDays = ShowLeaveDays();
        //string AbsentDays = ShowAbsentDays();

        if (Common.CastAsInt32(LeaveDays) > Common.CastAsInt32(0) && chkHalfDay.Checked == true)
        {
            lblLeaveDays.Text = ".5 (Days)";
            //lblAbsentDays.Text = "0 (Days)";
        }
        else
        {
            lblLeaveDays.Text = LeaveDays + " (Days)";
            //lblAbsentDays.Text = AbsentDays + " (Days)";
        }

    }
    public void BindVessel()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT VESSELID,VESSELNAME FROM DBO.Vessel WHERE VesselStatusId <> 2  ORDER BY VESSELNAME");

        ddlVessel.DataSource = dt;
        ddlVessel.DataTextField = "VESSELNAME";
        ddlVessel.DataValueField = "VESSELID";
        ddlVessel.DataBind();

        ddlVessel.Items.Insert(0, new ListItem("< Select >", "0"));

    }
    public void ShowRecord(int Id)
    {
        string sql = "SELECT PurposeId,REPLACE(CONVERT(VARCHAR(12),LeaveFrom,106),' ','-') AS LeaveFrom,REPLACE(CONVERT(VARCHAR(12),LeaveTo,106),' ','-') AS LeaveTo,Reason,REPLACE(CONVERT(VARCHAR(12),RequestDate,106),' ','-') AS RequestDate,HalfDay,(case when HalfDay>0 then 0.5 else dbo.HR_Get_LeaveCount(" + OfficeId.ToString() + ",leavefrom,leaveto) end) as Duration,AfterOfficeHr,Location, dbo.getAbsentDays(empid,LeaveFrom,LeaveTo) as AbsentDays, VesselId, Inspections " +
                     "FROM HR_OfficeAbsence  WHERE LeaveRequestId =" + Id.ToString() + " ";            
           
        DataTable dtdata = Common.Execute_Procedures_Select_ByQueryCMS(sql);

        if (dtdata != null)
            if (dtdata.Rows.Count > 0)
            {
                int halfdayleave;
                DataRow dr = dtdata.Rows[0];
                ddlLocation.SelectedValue = dr["Location"].ToString().Trim();
                ddlLeaveType.SelectedValue = dr["PurposeId"].ToString().Trim();
                Purpose_SelectedIndexChanged(new object(), new EventArgs());
                ddlVessel.SelectedValue = Common.CastAsInt32(dr["VesselId"]).ToString();
                hfInspIds.Value = dr["Inspections"].ToString();
                if (ddlLeaveType.SelectedValue == "1")
                {
                    if (dr["Inspections"].ToString() != "")
                    {
                        DataTable dt = Common.Execute_Procedures_Select_ByQuery("select Code from dbo.m_inspection WHERE Id IN (" + dr["Inspections"].ToString() + ")");
                        string Names = "";
                        foreach (DataRow dr1 in dt.Rows)
                        {
                            Names = Names + dr1["Code"].ToString() + ",";
                        }
                        lblInspections.Text = " [ " + Names.TrimEnd(',') + " ]";
                    }
                    else
                    {
                        lblInspections.Text = "";
                    }

                    lbSelectInsp.Text = (lblInspections.Text.Trim() == "" ? "Select Inspections" : "Modify Inspections");
                }
                txtLeaveFrom.Text = Convert.ToDateTime(dr["LeaveFrom"]).ToString("dd-MMM-yyyy").Trim();
                txtLeaveTo.Text = Convert.ToDateTime(dr["LeaveTo"]).ToString("dd-MMM-yyyy").Trim();
                //txtEJDt.Text = (dr["EstimatedReturnDt"]).ToString().Trim() == "" ? "" : Convert.ToDateTime(dr["EstimatedReturnDt"]).ToString("dd-MMM-yyyy").Trim();
                txtReason.Text = (dr["Reason"].ToString() == string.Empty) ? "" : dr["Reason"].ToString().Trim();
                halfdayleave = Common.CastAsInt32(dr["HalfDay"].ToString());
                //lblAbsentDays.Text = dr["AbsentDays"].ToString().Trim();
                if (dr["AfterOfficeHr"].ToString() != "")
                {
                    chkAfterOfficeHr.Checked = Convert.ToBoolean(dr["AfterOfficeHr"].ToString());
                }
                else
                {
                    chkAfterOfficeHr.Checked = false;
                }

                if (halfdayleave == 1)
                {
                    chkHalfDay.Checked = true;
                    rdoFirstHalf.Checked = true;
                    EnableHalfdayRadio();

                    txtLeaveTo.Text = txtLeaveFrom.Text.Trim();
                    txtLeaveTo.Enabled = false;
                    imgLeaveTo.Enabled = false;
                }
                else if (halfdayleave == 2)
                {
                    chkHalfDay.Checked = true;
                    rdoSecondHalf.Checked = true;
                    EnableHalfdayRadio();

                    txtLeaveTo.Text = txtLeaveFrom.Text.Trim();
                    txtLeaveTo.Enabled = false;
                    imgLeaveTo.Enabled = false;
                }
                else
                {
                    chkHalfDay.Checked = false;
                    DisableHalfdayRadio();
                }

                if (Common.CastAsDecimal(dr["duration"]) > Common.CastAsDecimal(0) && chkHalfDay.Checked == true)
                {
                    lblLeaveDays.Text = ".5 (Days)";
                    //lblAbsentDays.Text = "0 (Days)";
                }
                else
                {
                    lblLeaveDays.Text = dr["duration"].ToString() + " (Days)";
                    //lblAbsentDays.Text = dr["AbsentDays"].ToString() + " (Days)";
                }

                //if (halfdayleave > 0)
                //{
                //    lblAbsentDays.Text = "0 (Days)";
                //}

                btnNotify.Visible = false;
                btnsave.Visible = true;
                btnCancel.Text = "Cancel";
            }            
    }
           
    #endregion
    #region --- Control Events ---

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        ClearControls();
        LeaveRequestId = 0;
        BindGrid();

        pnlContent.Visible = true;
        //btnEdit.Visible = false ;
        //btnDelete.Visible = false;
        btnsave.Visible = true;
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        if (Common.CastAsInt32(ddlLeaveType.SelectedValue) == 1 && hfInspIds.Value.Trim() == "")
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "error", "alert('Please select inspections.');", true);
            return;
        }

        int HalfDay;
        if (chkHalfDay.Checked)
        {
            if (rdoFirstHalf.Checked)
            {
                HalfDay = 1;
            }
            else
            {
                HalfDay = 2;
            }
        }
        else
        {
            HalfDay = 0;
        }
        DateTime date_From, date_To, date_Er;
        if (!(DateTime.TryParse(txtLeaveFrom.Text, out date_From)))
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "error", "alert('From Date is Incorrect.');", true);
            return;
        }
        
        //-------------------------
        if (txtLeaveTo.Text != "")
        {
            if (!(DateTime.TryParse(txtLeaveTo.Text, out date_To)))
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "error", "alert('To Date is Incorrect.');", true);
                return;
            }

            if (date_From > date_To)
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('To Date should be greater than From Date.');", true);
                return;
            }
        }
        //-------------------------
        if (ddlLocation.SelectedValue == "2" && (ddlLeaveType.SelectedValue == "1" || ddlLeaveType.SelectedValue == "2"))
        {
            if (ddlVessel.SelectedIndex <= 0)
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "error", "alert('Please select vessel.');", true);
                return;
            }
        }
        //-------------------------
        if (txtReason.Text.Trim() != "" && txtReason.Text.Trim().Length > 500)
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "error", "alert('Remark should not be more than 500 characters.');", true);
            return;
        }


        object d1 = (txtLeaveFrom.Text.Trim() == "") ? DBNull.Value : (object)txtLeaveFrom.Text;
        object d2 = (txtLeaveTo.Text.Trim() == "") ? DBNull.Value : (object)txtLeaveTo.Text;

        int EmpId = Common.CastAsInt32(Session["ProfileId"]);
        Common.Set_Procedures("HR_InsertUpdateOfficeAbsence");
        Common.Set_ParameterLength(12);
        Common.Set_Parameters(new MyParameter("@LeaveRequestId", Common.CastAsInt32(LeaveRequestId)),
            new MyParameter("@EmpId", EmpId),
            new MyParameter("@PurposeId", Common.CastAsInt32(ddlLeaveType.SelectedValue.Trim())),
            new MyParameter("@LeaveFrom", d1),
            new MyParameter("@LeaveTo", d2),
            new MyParameter("@HalfDay", HalfDay),
            new MyParameter("@Reason", txtReason.Text.Trim()),
            new MyParameter("@AfterOfficeHr", chkAfterOfficeHr.Checked ? 1 : 0),
            new MyParameter("@Location", ddlLocation.SelectedValue.Trim()),
            new MyParameter("@HR_USER", "U"),
            new MyParameter("@VesselId", (ddlVessel.SelectedValue == "" ? 44 : Common.CastAsInt32(ddlVessel.SelectedValue))),
            new MyParameter("@Inspections", hfInspIds.Value.Trim())
            );


        DataSet ds = new DataSet();
        if (Common.Execute_Procedures_IUD_CMS(ds))
        {
            LeaveRequestId = Common.CastAsInt32(ds.Tables[0].Rows[0][0]);


            DataTable dtPos = Common.Execute_Procedures_Select_ByQuery("SELECT Position FROM DBO.Hr_PersonalDetails WHERE EmpId=" + EmpId);

            // NOW AUTO APPROVAL IS APPLIED FOR ALL CASES

            string SQL = "UPDATE [dbo].[HR_OfficeAbsence] SET [AppRejOn]=getdate(),[Status]='A' WHERE LeaveRequestId = " + LeaveRequestId;
            Common.Execute_Procedures_Select_ByQuery(SQL);

            btnsave.Visible = false;
            btnNotify.Visible = false;
            btnCancel.Text = "Cancel";

            // BELOW CODE WAS FOR AUTO APPROVAL IF POSITION IS TOP MOST

            //if (Common.CastAsInt32(dtPos.Rows[0]["Position"]) == 1)
            //{
            //    string SQL = "UPDATE [dbo].[HR_OfficeAbsence] SET [AppRejOn]=getdate(),[Status]='A' WHERE LeaveRequestId = " + LeaveRequestId;
            //    Common.Execute_Procedures_Select_ByQuery(SQL);

            //    btnsave.Visible = false;
            //    btnNotify.Visible = false;
            //    btnCancel.Text = "Cancel";
            //}
            //else
            //{
            //    btnsave.Visible = false;
            //    btnNotify.Visible = true;
            //    btnCancel.Text = "Cancel";
            //}

            BindGrid();

            ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "refreshparent();alert('Record saved successfully.');", true);            
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Unable To Save Record. Error : " + Common.getLastError() + "');", true);
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearControls();
        LeaveRequestId = 0;
        BindGrid();

        pnlContent.Visible = false;
        //btnEdit.Visible = false;
        //btnDelete.Visible = false;
        btnsave.Visible = false ;
    }
    protected void Purpose_SelectedIndexChanged(object sender, EventArgs e)
    {
        trVessel.Visible = (ddlLeaveType.SelectedValue == "1" || ddlLeaveType.SelectedValue == "2");
        trSelInsp.Visible = (ddlLeaveType.SelectedValue == "1");
    }

    #endregion

    #region --- Grid Section ---    
    protected void BindGrid()
    {

        string sql = "SELECT LeaveRequestId,GUID,EmpId,Purpose,REPLACE(CONVERT(VARCHAR(12),LeaveFrom,106),' ','-') AS LeaveFrom,REPLACE(CONVERT(VARCHAR(12),LeaveTo,106),' ','-') AS LeaveTo,Reason,REPLACE(CONVERT(VARCHAR(12),RequestDate,106),' ','-') AS RequestDate, Case WHEN [Status] = 'P' THEN 'Planned' WHEN [Status] = 'R' THEN 'Requested' WHEN [Status] = 'A' THEN 'Approved' WHEN [Status] = 'J' THEN 'Rejected' ELSE '' END AS [Status],ActFromDt,ActToDt,PayRequestedOn  " +
                     "FROM HR_OfficeAbsence  OA " +
                     "INNER JOIN HR_LeavePurpose LP ON LP.PurposeId = OA.PurposeId " +
                     "WHERE EmpId =" + EmpId.ToString() + " AND ( YEAR(LeaveFrom)=" + ddlYear.SelectedValue + " OR YEAR(LeaveTo)=" + ddlYear.SelectedValue + ") ORDER BY LeaveRequestId DESC";

        DataTable Ds = Common.Execute_Procedures_Select_ByQueryCMS(sql);  
        rptOffAbsence.DataSource = Ds;
        rptOffAbsence.DataBind();
    }

    protected void btnView_Click(object sender, ImageClickEventArgs e)
    {
        LeaveRequestId = Common.CastAsInt32(((ImageButton)sender).CommandArgument.ToString());
        ShowRecord_View(LeaveRequestId);
        dv_ViewBT.Visible = true;
        
    }
    public void ShowRecord_View(int Id)
    {
        string sql = "select * ,LocationText=(case when Location=1 THen 'Local' else 'International' end) ,PurposeText=(select Purpose from [HR_LeavePurpose] where purposeid=oa.purposeid),(Select VesselName from dbo.vessel v where v.vesselid=oa.Vesselid) as VesselName from HR_OfficeAbsence oa WHERE LeaveRequestId =" + Id.ToString() + " ";            
        DataTable dtdata = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        if (dtdata != null)
            if (dtdata.Rows.Count > 0)
            {
                DataRow dr = dtdata.Rows[0];
                lblLocation.Text = "[ " + dr["LocationText"].ToString().Trim() + " Visit ]" ;
                lblPurpose.Text = dr["PurposeText"].ToString().Trim();
                lblPeriod.Text ="Duration : " + Common.ToDateString(dr["LeaveFrom"]) + " - " + Common.ToDateString(dr["LeaveTo"]);
                lblRemarks.Text = dr["Reason"].ToString().Trim();
                lblVesselName.Text = "";
                if (lblPurpose.Text.Contains("Vessel") || lblPurpose.Text.Contains("Docking"))
                {
                    lblVesselName.Text = "Vessel : " + dr["VesselName"].ToString().Trim();
                }
                lblPlannedInspections.Text = "";
                if (lblPurpose.Text.Contains("Vessel"))
                {
                    string Inspections = dr["Inspections"].ToString().Trim();
                    if (Inspections.Trim() != "")
                    {
                        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT CODE FROM DBO.m_Inspection WHERE ID IN (" + Inspections + ")");
                        Inspections = "";
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr1 in dt.Rows)
                            {
                                Inspections += "," + dr1["CODE"].ToString();
                            }
                            Inspections = Inspections.Substring(1);
                        }
                        lblPlannedInspections.Text = "Planned Inspections : " + Inspections;
                    }
                }
                if (lblPurpose.Text.Contains("Docking"))
                {
                    lblPlannedInspections.Text = "DD # : ";
                }
                
               

                int HalfDay=Common.CastAsInt32(dr["HalfDay"]);
                switch( HalfDay)
                {
                    case 1:
                        lblHalfDay.Text = "Halfday - First Half";
                    break;
                    case 2:
                        lblHalfDay.Text = "Halfday - Second Half";
                    break;
                    default:
                        lblHalfDay.Text = "";
                        break;
                }

                if (dr["AfterOfficeHr"].ToString() == "True")
                    lblHalfDay.Text += " ( After Office Hrs )";

                if (dr["PurposeText"].ToString().Trim() == "Vessel Attendance")
                    dv_IP.Visible = dv_IPR.Visible = true;
                else
                    dv_IP.Visible = dv_IPR.Visible = false;

                if (dr["PurposeText"].ToString().Trim() == "Docking/Repairs")
                    dv_DD.Visible = dv_DDR.Visible = true;
                else
                    dv_DD.Visible = dv_DDR.Visible = false;

                if (dr["LocationText"].ToString().Trim() == "Local")
                {
                    trHO.Visible = false;
                    trTO.Visible = false;

                    trBriefing.Visible = false;
                    trDeBriefing.Visible = false;
                }
                else
                {
                    trHO.Visible = true;
                    trTO.Visible = true;

                    trBriefing.Visible = true;
                    trDeBriefing.Visible = true;
                }

                if (dr["PaymentStatus"].ToString().Trim() == "P")
                {
                    imgSendAcct_R.Visible = false;
                    imgSendAcct_G.Visible = true;
                }
                else
                {
                    imgSendAcct_R.Visible = true;
                    imgSendAcct_G.Visible = false;
                }

                lblRecdDateByOn.Text = (""+dr["ReceivedBy"]) + " /  "  + Common.ToDateString(dr["ReceivedDate"]);
                lblPaymentByIn.Text = ("" + dr["PaymentBy"]) + " /  " + Common.ToDateString(dr["PaymentDoneDate"]);

                imgDownloadExp.Visible = (dr["FileName"].ToString().Trim() != "");

                btnSaveAccount.Visible = (dr["FileName"].ToString().Trim() == "");
                btnSendToAccount.Visible = (dr["PaymentStatus"].ToString().Trim() == "" && dr["FileName"].ToString().Trim() != "");
            }

        // --------------- Details Section ----------------

        sql = "SELECT (SELECT COUNT(EmpId) FROM [dbo].[HR_OfficeAbsence_HODApproval] WHERE LeaveRequestId = " + LeaveRequestId + " AND RepliedOn IS NOT NULL) AS Recd,(SELECT COUNT(EmpId) FROM [dbo].[HR_OfficeAbsence_HODApproval] WHERE LeaveRequestId = " + LeaveRequestId + " ) AS [Sent] ";
        dtdata = Common.Execute_Procedures_Select_ByQueryCMS(sql);

        lbRecd.Text = dtdata.Rows[0]["Recd"].ToString();
        lbSent.Text = dtdata.Rows[0]["Sent"].ToString();

        if (Common.CastAsInt32(dtdata.Rows[0]["Sent"]) > 0)
        {
            imgStatusOTB_G.Visible = true;
            imgStatusOTB_R.Visible = false;
        }
        else
        {
            imgStatusOTB_G.Visible = false;
            imgStatusOTB_R.Visible = true;
        }

        // ------------------------------------------------ Inspections 
        sql = "select InspectionNo from dbo.t_InspectionDue where id in (select InspectionDueId from DBO.HR_OfficeAbsence_Inspections where LeaveRequestId=" + LeaveRequestId +  ")";
        dtdata = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        string s = "";
        foreach (DataRow dr in dtdata.Rows)
        {
            s +=", "+dr[0].ToString();;
        }
        if(s.StartsWith(","))
            s=s.Substring(1);
        lbSelectedInsps.Text = s;
        imgIP_R.Visible = (lbSelectedInsps.Text.Trim() == "");
        imgIP_G.Visible = (lbSelectedInsps.Text.Trim() != "");

        // ------------------------------------------------ DryDocking 

        //if (dv_DD.Visible)
        //{
        sql = "SELECT *,Case WHEN Status = 'P' THEN 'Planned' WHEN Status = 'E' THEN 'Executed' WHEN Status = 'C' THEN 'Closed' ELSE '' END AS StatusText FROM [dbo].[DD_DocketMaster] dm WHERE dm.[DocketId] IN (SELECT e.[DocketId] FROM  [dbo].[HR_OfficeAbsence] e WHERE e.LeaveRequestId = " + LeaveRequestId + ")";
            dtdata = Common.Execute_Procedures_Select_ByQueryCMS(sql);

            if (dtdata != null && dtdata.Rows.Count > 0)
            {
                lblPlannedDDNo.Text = dtdata.Rows[0]["DocketNo"].ToString();
                lblDDStatus.Text = dtdata.Rows[0]["StatusText"].ToString();

                imgDD_R.Visible = false;
                imgDD_G.Visible = true;

            }
            else
            {
                imgDD_R.Visible = true;
                imgDD_G.Visible = false;
            }
        //}
        //else
        //{
        //    imgDD_R.Visible = true;
        //    imgDD_G.Visible = false;
        //}

        // ------------------------------------------------ HOTO & Brief - De-Brief 
        
        if (lblLocation.Text.Contains("International"))
        {
            string sqlPos = "Select Position from Hr_PersonalDetails WHERE EMPId = " + EmpId + " AND Position IN (Select PositionId from Position WHERE VesselPositions IN (1,2,3)  OR PositionId IN (1,4,89))";
            DataTable dtdataPos = Common.Execute_Procedures_Select_ByQueryCMS(sqlPos);

            if (dtdataPos != null && dtdataPos.Rows.Count > 0)
            {
                trBriefing.Visible = true;
                trDeBriefing.Visible = true;
                trHO.Visible = true;
                trTO.Visible = true;

                // ------------------------------------------------ HandOver  

                sql = "select HandOverToId,(SELECT FIRSTNAME + ' ' + MiddleName + ' ' + FamilyName FROM DBO.Hr_PersonalDetails WHERE EMPID=HandOverToId) as EMPNAME,NotifiedOn,TakeOver,TakeOverOn,HStatus,BStatus from DBO.HR_OfficeAbsence_HOTOMaster where HOtoid=" + LeaveRequestId + "";
                dtdata = Common.Execute_Procedures_Select_ByQueryCMS(sql);
                if (dtdata.Rows.Count > 0)
                {
                    lblPriHandoverTo.Text = dtdata.Rows[0]["EMPNAME"].ToString();
                    lblHandOverDate.Text = Common.ToDateString(dtdata.Rows[0]["NotifiedOn"]);
                    imgHO_R.Visible = (dtdata.Rows[0]["HStatus"].ToString().Trim() == "" && dtdata.Rows[0]["BStatus"].ToString().Trim() == "");
                    imgHO_G.Visible = (dtdata.Rows[0]["HStatus"].ToString().Trim() == "A" || dtdata.Rows[0]["BStatus"].ToString().Trim() == "A");

                    if (dtdata.Rows[0]["TakeOver"].ToString() == "Y")
                    {
                        lblTakeoverDate.Text = Common.ToDateString(dtdata.Rows[0]["TakeOverOn"]);
                        imgTO_R.Visible = false;
                        imgTO_G.Visible = true;
                    }
                    else
                    {
                        imgTO_R.Visible = true;
                        imgTO_G.Visible = false;
                    }
                }
                else
                {
                    imgHO_R.Visible = imgTO_R.Visible = true;
                    imgHO_G.Visible = imgTO_G.Visible = false;
                }

                sql = "SELECT FIRSTNAME + ' ' + MiddleName + ' ' + FamilyName FROM DBO.Hr_PersonalDetails WHERE EMPID IN (select BackUpId from DBO.HR_OfficeAbsence_HOTOMaster where HOtoid=" + LeaveRequestId + ")";
                dtdata = Common.Execute_Procedures_Select_ByQueryCMS(sql);
                if (dtdata.Rows.Count > 0)
                {
                    lblBackupHandoverTo.Text = dtdata.Rows[0][0].ToString();
                }

                // ------------------------------------------------ Brieifng

                sql = "select * from DBO.HR_OfficeAbsence_Briefing where LEAVEREQUESTID=" + LeaveRequestId + "";
                dtdata = Common.Execute_Procedures_Select_ByQueryCMS(sql);
                if (dtdata.Rows.Count > 0)
                {
                    lblBriefingDt.Text = Common.ToDateString(dtdata.Rows[0]["BriefOn"]);
                    lblDEBriefingDt.Text = Common.ToDateString(dtdata.Rows[0]["DeBriefOn"]);

                    imgBrief_R.Visible = imgDeBrief_R.Visible = false;
                    imgBrief_G.Visible = imgDeBrief_G.Visible = true;
                }
                else
                {
                    imgBrief_R.Visible = imgDeBrief_R.Visible = true;
                    imgBrief_G.Visible = imgDeBrief_G.Visible = false;
                }

            }
            else
            {
                trBriefing.Visible = false;
                trDeBriefing.Visible = false;
                trHO.Visible = false;
                trTO.Visible = false;
            }
        }
        else
        {
            //imgHO_R.Visible = imgTO_R.Visible = true;
            //imgHO_G.Visible = imgTO_G.Visible = false;

            //imgBrief_R.Visible = imgDeBrief_R.Visible = true;
            //imgBrief_G.Visible = imgDeBrief_G.Visible = false;

            trBriefing.Visible = false;
            trDeBriefing.Visible = false;
            trHO.Visible = false;
            trTO.Visible = false;
        }


        // ------------------------- Cash Advance 

        sql = "SELECT * FROM DBO.HR_OfficeAbsence_CashAdvance WHERE BizTravelId = " + LeaveRequestId;
        dtdata = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        if (dtdata.Rows.Count > 0)
        {
            imgCA_R.Visible = true;
            imgCA_G.Visible = false;
        }
        else
        {
            imgCA_R.Visible = false;
            imgCA_G.Visible = true;
        }
        string CashTaken = "";
        foreach (DataRow dr in dtdata.Rows)
        {
            if (dr["RecordType"].ToString().Trim() == "G")
                CashTaken = CashTaken + String.Format("{0:F}",dr["CashAdvance"].ToString()) + " " + dr["Currency"].ToString() + ",";
        }

        if (CashTaken.Trim().Length > 0)
            CashTaken = CashTaken.TrimEnd(',');

        lblCashTaken.Text = CashTaken;

        // ------------------------- Update Itinery

        sql = "SELECT  REPLACE(Convert(varchar(11), ActFromDt, 106),' ','-') AS ActFromDt,REPLACE(Convert(varchar(11), ActToDt, 106),' ','-') AS ActToDt, ActFromDt AS Time,ActToDt AS EndTime FROM DBO.HR_OfficeAbsence WHERE LeaveRequestId = " + LeaveRequestId;
        dtdata = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        if (dtdata.Rows.Count > 0)
        {
           string FromDate  = dtdata.Rows[0]["ActFromDt"].ToString();
           string TODate = dtdata.Rows[0]["ActToDt"].ToString() == "01-Jan-1900" ? "" : dtdata.Rows[0]["ActToDt"].ToString();

            if (dtdata.Rows[0]["Time"].ToString() != "")
            {
                string time = dtdata.Rows[0]["Time"].ToString();
                string[] str = time.Split(' ');

                FromDate += " " + str[1].ToString() + " (Hrs)";
            }

            if (dtdata.Rows[0]["EndTime"].ToString() != "")
            {
                string endtime = dtdata.Rows[0]["EndTime"].ToString();
                string[] str = endtime.Split(' ');

                TODate += " " + str[1].ToString() + " (Hrs)";
            }

            lblDepDateTime.Text = FromDate;
            lblArrivalDatetime.Text = TODate;

            if (lblDepDateTime.Text.Trim() == "")
            {
                imgUTI_R.Visible = true;
                imgUTI_G.Visible = false;
            }
            else
            {
                imgUTI_R.Visible = false;
                imgUTI_G.Visible = true;
            }
        }
        else
        {
            imgUTI_R.Visible = true;
            imgUTI_G.Visible = false;
        }


        // ------------------------- Insp Report

        sql = "SELECT COUNT(D.InspectionDueId) FROM [dbo].[HR_OfficeAbsence_Inspections]  D " + 
              "WHERE LeaveRequestId = " + LeaveRequestId + " AND InspectionId IN ( SELECT ID FROM dbo.m_inspection WHERE InspectionGroup IN (SELECT ID FROM dbo.m_InspectionGroup WHERE code='MTM')) " + 
              "AND NOT EXISTS((SELECT NotofiedOn FROM DBO.tbl_Inspection_Report_Notify N WHERE N.InspectionDueId=D.InspectionDueId AND NotofiedOn is not null))";
        dtdata = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        if (dtdata.Rows[0][0].ToString() == "0")
        {
            imgIR_R.Visible = false;
            imgIR_G.Visible = true;

             sql = "SELECT NotofiedOn FROM DBO.tbl_Inspection_Report_Notify WHERE InspectionDueId = (SELECT TOP 1 InspectionDueId FROM [dbo].[HR_OfficeAbsence_Inspections] WHERE LeaveRequestId = " + LeaveRequestId + " AND InspectionId IN ( SELECT ID FROM dbo.m_inspection WHERE InspectionGroup IN (SELECT ID FROM dbo.m_InspectionGroup WHERE code='MTM'))) ";
            dtdata = Common.Execute_Procedures_Select_ByQueryCMS(sql);

            if (dtdata!=null && dtdata.Rows.Count > 0)
            {
                lblInspNotifyDate.Text = Common.ToDateString(dtdata.Rows[0]["NotofiedOn"]);
            }
        }
        else
        {
            imgIR_R.Visible = true;
            imgIR_G.Visible = false;

            lblInspNotifyDate.Text = "";
        }
                

        // ------------------------- Expense 

        sql = "SELECT * FROM DBO.HR_OfficeAbsence_Expense WHERE BizTravelId = " + LeaveRequestId;
        dtdata = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        if (dtdata.Rows.Count > 0)
        {
            imgExp_R.Visible = false;
            imgExp_G.Visible = true;
        }
        else
        {
            imgExp_R.Visible = true;
            imgExp_G.Visible = false;
        }
        
    }
    
    protected void btnCloseview_Click(object sender, EventArgs e)
    {
        dv_ViewBT.Visible = false;
    }
    
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        LeaveRequestId = Common.CastAsInt32(((ImageButton)sender).CommandArgument.ToString());
        ShowRecord(LeaveRequestId);

        pnlContent.Visible = true;
        btnsave.Visible = true;
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        LeaveRequestId = Common.CastAsInt32(((ImageButton)sender).CommandArgument.ToString());

        try
        {
            string DeleteSQL = "DELETE FROM [dbo].[HR_OfficeAbsence_HODApproval] WHERE LeaveRequestId = " + LeaveRequestId.ToString() + " ; " +
                               "DELETE FROM DBO.HR_OfficeAbsence_Inspections WHERE LeaveRequestId= " + LeaveRequestId.ToString() + " ; " +
                               "DELETE FROM DBOEmtm_OfficeAbsence_HOTOMaster WHERE HOtoid= " + LeaveRequestId.ToString() + " ; " +
                               "DELETE FROM DBOEmtm_OfficeAbsence_Briefing WHERE LeaveRequestId= " + LeaveRequestId.ToString() + " ; " +
                               "DELETE FROM DBO.HR_OfficeAbsence_CashAdvance WHERE BizTravelId = " + LeaveRequestId.ToString() + " ; " +
                               "DELETE FROM DBO.HR_OfficeAbsence_Expense WHERE BizTravelId = " + LeaveRequestId.ToString() + " ; " +
                               "DELETE FROM HR_OfficeAbsence WHERE LeaveRequestId = " + LeaveRequestId.ToString() + " ; SELECT -1";
            DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(DeleteSQL);
            if (dt.Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "alert('Record deleted successfully.');", true);
                LeaveRequestId = 0;
                BindGrid();
                pnlContent.Visible = false;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "failure", "alert('Unable to delete record. Error : " + Common.getLastError() + "');", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "failure", "alert('Unable to delete record. Error : " + ex.Message + "');", true);
        }
    }

    protected void btnPrint_Click(object sender, ImageClickEventArgs e)
    {
        int BizTrvlId = Common.CastAsInt32(((ImageButton)sender).CommandArgument.ToString());

        ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "openreport('" + BizTrvlId + "');", true);
        

    }
    
    protected void btnNotify_Click(object sender, EventArgs e)
    {
        btnSendForApproval_Click(sender, e);
    }

    protected void btnBD_Click(object sender, EventArgs e)
    {
        int id = Common.CastAsInt32(((Button)sender).CommandArgument);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "open", "OpenBriefWindow('" + id + "','E');", true);
    }
    protected void btnHoto_Click(object sender, EventArgs e)
    {
        int id = Common.CastAsInt32(((Button)sender).CommandArgument);

        ScriptManager.RegisterStartupScript(this, this.GetType(), "open", "OpenHotoWindow('" + id + "','E');", true);
    }
    protected void btnExpense_Click(object sender, EventArgs e)
    {
        int id = Common.CastAsInt32(((Button)sender).CommandArgument);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "open", "openexpensewindow('" + id + "','E');", true);
    }
    protected void btnCashAdv_Click(object sender, EventArgs e)
    {
        int id = Common.CastAsInt32(((Button)sender).CommandArgument);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "open", "openexpensewindow('" + id + "','C');", true);
    }
    #endregion

    #region ----- Mail Functions -----------------

    public string SendMail()
    {
        string ReplyMess = "";
        string MailFrom = "", MailTo = "asingh@energiossolutions.com";
        //Mail From
        string sqlGetMailFrom = "SELECT pd.EmpID,C.Email FROM Hr_PersonalDetails pd LEFT OUTER JOIN USERLOGIN C ON pd.userid=C.LoginId " +
                                "WHERE pd.EmpID=" + EmpId;



        DataTable dtGetMailFrom = Common.Execute_Procedures_Select_ByQueryCMS(sqlGetMailFrom);
        if (dtGetMailFrom != null)
            if (dtGetMailFrom.Rows.Count > 0)
            {
                DataRow drGetMailFrom = dtGetMailFrom.Rows[0];
                MailFrom = drGetMailFrom["Email"].ToString();
            }

        String EmpFullName = "", EmpPosition = "";
        string sqlFullNameNPosition = "SELECT (select PositionName from Position P where P.PositionID=pd.Position)Position,(pd.FirstName+' '+pd.FamilyName )as UserName FROM Hr_PersonalDetails pd LEFT OUTER JOIN USERLOGIN C ON pd.userid=C.LoginId where pd.EmpID=" + EmpId + "";
        DataTable dtNamePos = Common.Execute_Procedures_Select_ByQueryCMS(sqlFullNameNPosition);
        if (dtNamePos != null)
            if (dtNamePos.Rows.Count > 0)
            {
                EmpPosition = dtNamePos.Rows[0][0].ToString();
                EmpFullName = dtNamePos.Rows[0][1].ToString();
            }



        //Mail To

        string sqlGetEmployeeInfo = "SELECT LeaveFrom,LeaveTo,Reason,Purpose, dbo.getAbsentDays(OA.empid,OA.LeaveFrom,OA.LeaveTo) as AbsentDays FROM HR_OfficeAbsence OA " +
                                    "INNER JOIN HR_LeavePurpose LP ON OA.PurposeId = LP.PurposeId WHERE LeaveRequestId = " + LeaveRequestId;
                 
            DataTable dtGetEmployeeInfo = Common.Execute_Procedures_Select_ByQueryCMS(sqlGetEmployeeInfo);
            if (dtGetEmployeeInfo != null)
                if (dtGetEmployeeInfo.Rows.Count > 0)
                {
                    DataRow drGetEmployeeInfo = dtGetEmployeeInfo.Rows[0];
                    //Sending mails
                    char[] Sep = { ';' };
                    string[] ToAdds = MailTo.ToString().Split(Sep);
                    string[] CCAdds = MailTo.ToString().Split(Sep);
                    string[] BCCAdds = "".Split(Sep);
                    //----------------------
                    String Subject = "Office Absence -- <" + drGetEmployeeInfo["Purpose"].ToString() + ">";
                    String MailBody;

                    MailBody = "Dear All, ";
                    MailBody = MailBody + "<br><br>I will be away from office From <b>" + Convert.ToDateTime(drGetEmployeeInfo["LeaveFrom"]).ToString("dd-MMM-yyyy") + "</b> To <b>" + Convert.ToDateTime(drGetEmployeeInfo["LeaveTo"]).ToString("dd-MMM-yyyy") + "</b>.";
                    MailBody = MailBody + "<br><br>Total Office Absence: " + drGetEmployeeInfo["AbsentDays"].ToString() + "";
                    MailBody = MailBody + "<br><br>Remarks: " + drGetEmployeeInfo["Reason"].ToString() + "";

                    MailBody = MailBody + "<br><br>Thanks & Regards";
                    //MailBody = MailBody + "<br>" + drGetEmployeeInfo["Name"].ToString() + "<br><font color=000080 size=2 face=Century Gothic><strong>" + MailFrom.ToString() + "</strong></font>";
                    MailBody = MailBody + "<br>" + UppercaseWords(EmpFullName);
                    MailBody = MailBody + "<br>" + EmpPosition + "<br><font color=000080 size=2 face=Century Gothic><strong>" + MailFrom.ToString() + "</strong></font>";

                    //------------------
                    string AttachmentFilePath = "";
                    SendEmail.SendeMail(EmpId, MailFrom.ToString(), MailFrom.ToString(), ToAdds, CCAdds, BCCAdds, Subject, MailBody, out ReplyMess, AttachmentFilePath);
                    //SendMail(ToAdds,CCAdds, Subject, MailBody, AttachmentFilePath, "");
                }
       
        return ReplyMess;
    }
    static string UppercaseWords(string value)
    {
        char[] array = value.ToCharArray();
        // Handle the first letter in the string.
        if (array.Length >= 1)
        {
            if (char.IsLower(array[0]))
            {
                array[0] = char.ToUpper(array[0]);
            }
        }
        // Scan through the letters, checking for spaces.
        // ... Uppercase the lowercase letters following spaces.
        for (int i = 1; i < array.Length; i++)
        {
            if (array[i - 1] == ' ')
            {
                if (char.IsLower(array[i]))
                {
                    array[i] = char.ToUpper(array[i]);
                }
            }
        }
        return new string(array);
    }
    public void SendMail(string[] ToAddresses, string[] CCAddresses, string Subject, string BodyContent, string AttachMentPath, string MailDetails)
    {
        MailMessage objMessage = new MailMessage();
        SmtpClient objSmtpClient = new SmtpClient();
        StringBuilder msgFormat = new StringBuilder();

        try
        {
            //if (chkTest.Checked)
            //{
            objMessage.From = new MailAddress("asingh@energiossolutions.com");
            objMessage.To.Add("asingh@energiossolutions.com");
            //objMessage.To.Add("asingh@energiossolutions.com");
            //objMessage.To.Add("asingh@energiossolutions.com");

            objSmtpClient.Host = "smtp.gmail.com";
            objSmtpClient.Port = 25;
            objSmtpClient.EnableSsl = true;
            objSmtpClient.Credentials = new NetworkCredential("a@a.com", "esoft99");
            //}
            //else
            //{
            //    if (MailDetails == "Accident Notification Mail")
            //    {
            //        objMessage.Bcc.Add("asingh@energiossolutions.com");
            //    }
            //    objMessage.From = new MailAddress(SenderAddress);
            //    objSmtpClient.Credentials = new NetworkCredential(SenderUserName, SenderPass);
            //    objSmtpClient.Host = MailClient;
            //    objSmtpClient.Port = Port;

            //    foreach (string add in ToAddresses)
            //    {
            //        objMessage.To.Add(add);
            //    }
            //    if (CCAddresses != null)
            //    {
            //        foreach (string add in CCAddresses)
            //        {
            //            objMessage.CC.Add(add); // Brijveer : - 8764258943
            //        }
            //    }
            //}
            objMessage.Body = BodyContent;
            objMessage.Subject = Subject;
            objMessage.IsBodyHtml = true;
            objMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            if (File.Exists(AttachMentPath))
                objMessage.Attachments.Add(new System.Net.Mail.Attachment(AttachMentPath));
            objSmtpClient.Send(objMessage);
            //AddMessage(MailDetails + "mail sent successfully. FileName:" + AttachMentPath + "");
        }
        catch (System.Exception ex)
        {
            //AddMessage("Error while sending " + MailDetails + "mail. FileName :" + AttachMentPath, ex.Message);
        }
    } 

    #endregion 

    protected void lbAction_SentNow_Click(object sender, EventArgs e)
    {
        dv_ViewBT.Visible = false;
        dv_SentNow.Visible = true;
        btnSendNow.Visible = true;
        chkAll.Visible = true;
        dv_SN_Comments.Visible = true;

        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT EmpId, EmpCode, (FirstName + ' ' + MiddleName + ' ' + FamilyName) As Name, (SELECT RepliedComments FROM [dbo].[HR_OfficeAbsence_HODApproval] WHERE LeaveRequestId = " + LeaveRequestId + " AND EmpId =PD.EmpId) AS RepComment, (SELECT [RequestedOn] FROM [dbo].[HR_OfficeAbsence_HODApproval] WHERE LeaveRequestId = " + LeaveRequestId + " AND EmpId =PD.EmpId ) AS SentOn, (SELECT [RepliedOn] FROM [dbo].[HR_OfficeAbsence_HODApproval] WHERE LeaveRequestId = " + LeaveRequestId + " AND EmpId =PD.EmpId ) AS ReplyOn, (SELECT Email FROM [dbo].UserLogin WHERE LoginId = PD.UserId) As Email, 1 AS Show FROM [dbo].[Hr_PersonalDetails] PD WHERE PD.HOD = 1 ORDER BY NAME");

        if (dt != null && dt.Rows.Count > 0)
        {
            rptSentNow.DataSource = dt;
            rptSentNow.DataBind();

            chkAll.Checked = true;
        }
        else
        {
            rptSentNow.DataSource = null;
            rptSentNow.DataBind();
        }



    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        dv_ViewBT.Visible = true;
        dv_SentNow.Visible = false;
        txtReqComments.Text = "";
    }
    protected void btnSendNow_Click(object sender, EventArgs e)
    {
        bool selected = false;

        foreach (RepeaterItem item in rptSentNow.Items)
        {
            CheckBox chkSelect = (CheckBox)item.FindControl("chkSelect");

            if (chkSelect.Checked)
            {
                selected = true;
                break;
            }
        }

        if (!selected)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "err", "alert('Please select members.');", true);
            return;
        }

        try
        {
            foreach (RepeaterItem item in rptSentNow.Items)
            {
                CheckBox chkSelect = (CheckBox)item.FindControl("chkSelect");

                if (chkSelect.Checked)
                {
                    int empid = Common.CastAsInt32(chkSelect.Attributes["empid"]);

                    Common.Set_Procedures("HR_InsertUpdateOfficeAbsenceHODApproval");
                    Common.Set_ParameterLength(3);
                    Common.Set_Parameters(
                        new MyParameter("@LeaveRequestId", LeaveRequestId),
                        new MyParameter("@EmpId", empid),
                        new MyParameter("@RequestedComments", txtReqComments.Text.Trim())
                        );
                    DataSet ds = new DataSet();
                    if (Common.Execute_Procedures_IUD_CMS(ds))
                    {
                        string Email = chkSelect.Attributes["email"].ToString();
                        SendMail(Email, empid);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "err", "alert('Unable To Save Record. Error : " + Common.getLastError() + "');", true);
                        return;
                    }
                }
            }
            ShowRecord_View(LeaveRequestId);
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Record saved successfully.');", true);

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "err", "alert('Unable To Save Record. Error : " + ex.Message + "');", true);
        }
    }
    protected void lbSent_Click(object sender, EventArgs e)
    {
        dv_ViewBT.Visible = false;
        dv_SentNow.Visible = true;
        btnSendNow.Visible = false;
        chkAll.Visible = false;
        dv_SN_Comments.Visible = false;

        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT EmpId, EmpCode, (FirstName + ' ' + MiddleName + ' ' + FamilyName) As Name, (SELECT RepliedComments FROM [dbo].[HR_OfficeAbsence_HODApproval] WHERE LeaveRequestId = " + LeaveRequestId + " AND EmpId =PD.EmpId) AS RepComment, (SELECT [RequestedOn] FROM [dbo].[HR_OfficeAbsence_HODApproval] WHERE LeaveRequestId = " + LeaveRequestId + " AND EmpId =PD.EmpId ) AS SentOn, (SELECT [RepliedOn] FROM [dbo].[HR_OfficeAbsence_HODApproval] WHERE LeaveRequestId = " + LeaveRequestId + " AND EmpId =PD.EmpId ) AS ReplyOn, '' As Email, 0 AS Show FROM [dbo].[Hr_PersonalDetails] PD WHERE PD.HOD = 1 ORDER BY NAME");

        if (dt != null && dt.Rows.Count > 0)
        {
            rptSentNow.DataSource = dt;
            rptSentNow.DataBind();
        }
        else
        {
            rptSentNow.DataSource = null;
            rptSentNow.DataBind();
        }
    }
    //protected void lbRecd_Click(object sender, EventArgs e)
    //{
    //    dv_SentNow.Visible = true;
    //    btnSendNow.Visible = false;

    //    DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT EmpId, EmpCode, (FirstName + ' ' + MiddleName + ' ' + FamilyName) As Name, (SELECT RepliedComments FROM [dbo].[HR_OfficeAbsence_HODApproval] WHERE LeaveRequestId = " + LeaveRequestId + " AND EmpId =PD.EmpId) AS RepComment, (SELECT TOP 1 RequestedComments FROM [dbo].[HR_OfficeAbsence_HODApproval] WHERE LeaveRequestId = " + LeaveRequestId + ") AS ReqComment FROM [dbo].[Hr_PersonalDetails] PD WHERE PD.HOD = 1 ORDER BY NAME");

    //    if (dt != null && dt.Rows.Count > 0)
    //    {
    //        txtReqComments.Text = dt.Rows[0]["ReqComment"].ToString();

    //        rptSentNow.DataSource = dt;
    //        rptSentNow.DataBind();
    //    }
    //    else
    //    {
    //        txtReqComments.Text = "";

    //        rptSentNow.DataSource = null;
    //        rptSentNow.DataBind();
    //    }


    //}

    public void SendMail(string MailTo, int EmpId)
    {
        if (MailTo.Trim() != "")
        {
            List<string> MailTos = new List<string>();
            MailTos.Add(MailTo);

            DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT [GUID] FROM  [dbo].[HR_OfficeAbsence_HODApproval] WHERE LeaveRequestId = " + LeaveRequestId + " AND EmpId = " + EmpId);
            string GUID = dt.Rows[0]["GUID"].ToString();

            string httpPath = @"http://emanagershore.energiosmaritime.com/cms/EMTM/MyProfile/Public_HOD_LeaveApproval.aspx?Key=" + GUID;
            //string httpPath = @"http://localhost:54817/Web/EMTM/MyProfile/Public_HOD_LeaveApproval.aspx?Key=" + GUID;

            //Sending mail
            char[] Sep = { ';' };
            string[] ToAdds = MailTos.ToArray();
            //string[] CCAdds = { "asingh@energiossolutions.com" };
            string[] CCAdds = {};
            string[] BCCAdds = "".Split(Sep);
            //----------------------

            string sql = "select * ,LocationText=(case when Location=1 THen 'Local' else 'International' end) ,PurposeText=(select Purpose from [HR_LeavePurpose] where purposeid=oa.purposeid),(Select VesselName from dbo.vessel v where v.vesselid=oa.Vesselid) as VesselName from HR_OfficeAbsence oa WHERE LeaveRequestId = " + LeaveRequestId;
            DataTable dtdata = Common.Execute_Procedures_Select_ByQueryCMS(sql);

            string Subject = "OK to Board request.";
            String MailBody;
            MailBody = "Dear All,";
            MailBody = MailBody + "<br><br>My Business travel plan as given below:";
            MailBody = MailBody + "<br><br>Purpose: " + dtdata.Rows[0]["PurposeText"].ToString().Trim();
            MailBody = MailBody + "<br><br>Location: " + dtdata.Rows[0]["LocationText"].ToString().Trim();
            MailBody = MailBody + "<br><br>Period: " + Common.ToDateString(dtdata.Rows[0]["LeaveFrom"]) + " - " + Common.ToDateString(dtdata.Rows[0]["LeaveTo"]);
            if (dtdata.Rows[0]["PurposeText"].ToString().Contains("Vessel") || dtdata.Rows[0]["PurposeText"].ToString().Contains("Docking"))
            {
                MailBody = MailBody + "<br><br>Vessel: " + dtdata.Rows[0]["VesselName"].ToString().Trim();
            }

            MailBody = MailBody + "<br><br>Remarks: " + txtReqComments.Text.ToString().Trim();
            MailBody = MailBody + "<br><br><br><br>Please <a href='" + httpPath + "' style='color: blue; text-decoration: underline;'>click here</a> to provide OK to Board.";
            MailBody = MailBody + "<br><br><font color=FF0000><strong>Note:</strong> <i>In case of no reply , OK to board will be considered automatically.</i></font>";
            MailBody = MailBody + "<br><br><br><br>Thank You, ";
            MailBody = MailBody + "<br>" + Session["UserName"].ToString();

            //------------------
            string ErrMsg = "";
            string AttachmentFilePath = "";
            SendEmail.SendeMailAsync(Common.CastAsInt32(Session["LoginId"].ToString()), "emanager@energiossolutions.com", "emanager@energiossolutions.com", ToAdds, CCAdds, BCCAdds, Subject, MailBody, out ErrMsg, AttachmentFilePath);

        }
            
    }

    protected void btnHideFrame_Click(object sender, EventArgs e)
    {
        dvIframe.Visible = false;
    }
    protected void lbAction_MakeHandOver_Click(object sender, EventArgs e)
    {
        dvIframe.Visible = true;
        frmlnk.Attributes.Add("height", "550px");
        frmlnk.Attributes.Add("src", "OfficeAbsenceHoto.aspx?id="  + LeaveRequestId + "&Mode=E&Type=H");
    }
    protected void lbAction_GetTakeOver_Click(object sender, EventArgs e)
    {
        dvIframe.Visible = true;
        frmlnk.Attributes.Add("height", "300px");
        frmlnk.Attributes.Add("src", "OfficeAbsenceHoto.aspx?id="  + LeaveRequestId + "&Mode=E&Type=T");
    }
    protected void lbAction_Startbreifing_Click(object sender, EventArgs e)
    {
        dvIframe.Visible = true;
        frmlnk.Attributes.Add("height", "425px");
        frmlnk.Attributes.Add("src", "OfficeAbsenceBriefing.aspx?id=" + LeaveRequestId + "&Mode=E&Type=B");
    }
    protected void lbAction_StartDeBriefing_Click(object sender, EventArgs e)
    {
        dvIframe.Visible = true;
        frmlnk.Attributes.Add("height", "425px");
        frmlnk.Attributes.Add("src", "OfficeAbsenceBriefing.aspx?id=" + LeaveRequestId + "&Mode=E&Type=D");
    }
    protected void lbAction_SelectInsps_Click(object sender, EventArgs e)
    {                                        
        dv_ViewBT.Visible = false;
        dv_Inspections.Visible = true;
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("select inspections FROM DBO.HR_OfficeAbsence where LeaveRequestId=" + LeaveRequestId);    
        if(dt.Rows.Count>0)
        {
            string Inspections=dt.Rows[0][0].ToString();
            if(Inspections.Trim()!="")
            {
                dt = Common.Execute_Procedures_Select_ByQuery("SELECT i.id,code,d.InspectionNo ,plandate,planremark  " +
                                                            "FROM dbo.m_inspection i " +
                                                            "left join DBO.HR_OfficeAbsence_Inspections d on i.id=inspectionid  and d.LeaveRequestId=" + LeaveRequestId +
                                                            " left join DBO.T_INSPECTIONDUE p on p.id=d.inspectiondueid where i.id in (" + Inspections + ")");
                if (dt != null && dt.Rows.Count > 0)
                {
                    rptInsp.DataSource = dt;
                    rptInsp.DataBind();
                }
                else
                {
                    rptInsp.DataSource = null;
                    rptInsp.DataBind();
                }
            }
        }
        
    }
    protected void lbAction_PayCash_Click(object sender, EventArgs e)
    {
        dvIframe.Visible = true;
        frmlnk.Attributes.Add("height", "450px");
        frmlnk.Attributes.Add("src", "OfficeCashAdvance.aspx?id=" + LeaveRequestId + "&Mode=E");
    }
    protected void lbAction_Expense_Click(object sender, EventArgs e)
    {
        DataTable dtPos = Common.Execute_Procedures_Select_ByQuery("SELECT Position FROM DBO.Hr_PersonalDetails WHERE EmpId=" + EmpId);
        if (Common.CastAsInt32(dtPos.Rows[0]["Position"]) == 1)
        {
            dvIframe.Visible = true;
            frmlnk.Attributes.Add("height", "600px");
            frmlnk.Attributes.Add("src", "OfficeExpense.aspx?id=" + LeaveRequestId + "&Mode=E");
            return;
        }
        //===========================
        if (trHO.Visible && trTO.Visible && trBriefing.Visible && trDeBriefing.Visible)
        {
            //if (imgStatusOTB_G.Visible && imgIP_G.Visible && imgHO_G.Visible && imgTO_G.Visible && imgBrief_G.Visible && imgDeBrief_G.Visible && imgUTI_G.Visible)
            //{
                dvIframe.Visible = true;
                frmlnk.Attributes.Add("height", "600px");
                frmlnk.Attributes.Add("src", "OfficeExpense.aspx?id=" + LeaveRequestId + "&Mode=E");
            //}
            //else
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "err", "alert('Please complete all the steps.');", true);
            //    return;
            //}
        }
        else
        {
            //if (imgStatusOTB_G.Visible && imgIP_G.Visible && imgUTI_G.Visible)
            //{
                dvIframe.Visible = true;
                frmlnk.Attributes.Add("height", "600px");
                frmlnk.Attributes.Add("src", "OfficeExpense.aspx?id=" + LeaveRequestId + "&Mode=E");
            //}
            //else
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "err", "alert('Please complete all the steps.');", true);
            //    return;
            //}
        }
    }
    protected void lbAction_UpdateItinery_Click(object sender, EventArgs e)
    {
        dvIframe.Visible = true;
        frmlnk.Attributes.Add("height", "230px");
        frmlnk.Attributes.Add("src", "OfficeAbsence_UpdateItinerary.aspx?id=" + LeaveRequestId + "&Mode=E");
    }
    protected void IbAction_Account_Click(object sender, EventArgs e)
    {
        dv_ViewBT.Visible = false;
        dv_SendToAccounts.Visible = true;
    }
    protected void btnSaveAccounts_Click(object sender, EventArgs e)
    {
        byte[] FileContent = new byte[0];
        string FileName = "";
        //-------------------------------------------
        if (flpUpload.HasFile)
        {
            if (!flpUpload.FileName.EndsWith(".pdf"))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "err", "alert('Please check.. ! Only pdf files are allowed.');", true);
                return;
            }

            FileName = flpUpload.FileName;
            FileContent = flpUpload.FileBytes;
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "err", "alert('Please select file to upload.');", true);
            return;
        }
        //-------------------------------------------
        try
        {
            Common.Set_Procedures("DBO.HR_UpdateSendToAccount");
            Common.Set_ParameterLength(3);
            Common.Set_Parameters(new MyParameter("@LeaveRequestId", LeaveRequestId),
                                  new MyParameter("@FileName", FileName),
                                  new MyParameter("@Attachment", FileContent)
                                  );
            DataSet ds = new DataSet();
            if (Common.Execute_Procedures_IUD(ds))
            {
                btnSaveAccount.Visible = false;
                btnSendToAccount.Visible = true;
                
                ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "alert('Uploaded successfully.');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "err", "alert('Unable to upload. Error : " + Common.getLastError() + "');", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "err", "alert('Unable to upload. Error : " + ex.Message + "'');", true);
        }

    }
    protected void btnSendToAccounts_Click(object sender, EventArgs e)
    {
        try
        {
            Common.Execute_Procedures_Select_ByQuery("UPDATE DBO.HR_OfficeAbsence SET PaymentStatus = 'R', PayRequestedOn = getdate() WHERE LeaveRequestId = " + LeaveRequestId);
            btnSendToAccount.Visible = false;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "alert('Sent to Account successfully.');", true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "err", "alert('Unable to send to Account. Error : " + ex.Message + "'');", true);
        }
    }
    protected void btnCloseAccounts_Click(object sender, EventArgs e)
    {
        dv_ViewBT.Visible = true;
        dv_SendToAccounts.Visible = false;
    }
    protected void lbAction_DD_Click(object sender, EventArgs e)
    {
        dv_ViewBT.Visible = false;
        dv_SelectDD.Visible = true;

        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT [DocketId],[DocketNo] FROM [dbo].[DD_DocketMaster] WHERE [VesselId] IN (SELECT VesselId FROM  [dbo].[HR_OfficeAbsence] WHERE LeaveRequestId = " + LeaveRequestId + " )");

        ddlDocket.DataSource = ((dt != null && dt.Rows.Count > 0) ? dt : null);
        ddlDocket.DataTextField = "DocketNo";
        ddlDocket.DataValueField = "DocketId";
        ddlDocket.DataBind();

        ddlDocket.Items.Insert(0, new ListItem("< Select >", ""));
    }

    protected void btnSelectDD_Click(object sender, EventArgs e)
    {
        DateTime dt;

        if (txtDocArrivalDt.Text.Trim() == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "asdf", "alert('Please enter docking arrival date.');", true);
            txtDocArrivalDt.Focus();
            return;
        }
        if (!DateTime.TryParse(txtDocArrivalDt.Text.Trim(), out dt))
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "asdf", "alert('Please enter valid docking arrival  date.');", true);
            txtDocArrivalDt.Focus();
            return;
        }

        if (txtExecFrom.Text.Trim() == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "asdf", "alert('Please enter repair commenced date.');", true);
            txtExecFrom.Focus();
            return;
        }
        if (!DateTime.TryParse(txtExecFrom.Text.Trim(), out dt))
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "asdf", "alert('Please enter valid repair commenced date.');", true);
            txtExecFrom.Focus();
            return;
        }
        if (txtExecTo.Text.Trim() == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "asdf", "alert('Please enter estimated completion date.');", true);
            txtExecTo.Focus();
            return;
        }

        if (!DateTime.TryParse(txtExecTo.Text.Trim(), out dt))
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "asdf", "alert('Please enter valid estimated completion date.');", true);
            txtExecTo.Focus();
            return;
        }
        if (DateTime.Parse(txtExecFrom.Text.Trim()) < DateTime.Today)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "asdf", "alert('Repair commenced date can not be less than today.');", true);
            txtExecFrom.Focus();
            return;
        }
        if (DateTime.Parse(txtExecFrom.Text.Trim()) > DateTime.Parse(txtExecTo.Text.Trim()))
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "asdf", "alert('Repair commenced date should be less than estimated completion date.');", true);
            txtExecFrom.Focus();
            return;
        }
        try
        {
            Common.Set_Procedures("[dbo].HR_DD_EXEC_DOCKET_BY_CMS_OFFICE_ABSECE");
            Common.Set_ParameterLength(5);
            Common.Set_Parameters(
                        new MyParameter("@DocketId", Common.CastAsInt32(ddlDocket.SelectedValue)),
                        new MyParameter("@D1", txtDocArrivalDt.Text.Trim()),
                        new MyParameter("@D2", txtExecFrom.Text.Trim()),
                        new MyParameter("@D3", txtExecTo.Text.Trim()),
                        new MyParameter("@LeaveRequestId", Common.CastAsInt32(LeaveRequestId))
                );

            DataSet ds = new DataSet();
            if (Common.Execute_Procedures_IUD_CMS(ds))
            {
                ShowRecord_View(LeaveRequestId);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "asdf", "alert('RFQ Executed successfully.');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "asdf", "alert('Unable to execute RFQ. Error : " + Common.getLastError() + "');", true);
            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "asdf", "alert('Unable to execute RFQ. Error : " + ex.Message + "');", true);
        }
    }
    protected void ddlDocket_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDocket.SelectedIndex > 0)
        {
            DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM [dbo].DD_Docket_RFQ_Master WHERE DOCKETId = " + ddlDocket.SelectedValue + " AND Status = 'P'");

            if (dt != null && dt.Rows.Count > 0)  // IF DOCKET CONFIRMED
            {
                hfdRFQId.Value = Common.CastAsInt32(dt.Rows[0]["RFQId"]).ToString();
                txtDocArrivalDt.Text = Common.ToDateString(dt.Rows[0]["DockingArrivalDate"]);
                txtExecFrom.Text = Common.ToDateString(dt.Rows[0]["ExecFrom"]);
                txtExecTo.Text = Common.ToDateString(dt.Rows[0]["ExecTo"]);
                
            }
            else
            {
                hfdRFQId.Value = "";
                txtDocArrivalDt.Text = "";
                txtExecFrom.Text = "";
                txtExecTo.Text = "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "asdf", "alert('Please confirm yard first.');", true);
                btnSelectDD.Enabled = false;
            }
        }
        else
        {
            hfdRFQId.Value = "";
            txtDocArrivalDt.Text = "";
            txtExecFrom.Text = "";
            txtExecTo.Text = "";
            btnSelectDD.Enabled = true;
        }
    }
    protected void btnCloseDD_Click(object sender, EventArgs e)
    {
        dv_ViewBT.Visible = true;
        dv_SelectDD.Visible = false;
        ddlDocket.SelectedIndex = 0;
        hfdRFQId.Value = "";
        txtDocArrivalDt.Text = "";
        txtExecFrom.Text = "";
        txtExecTo.Text = "";
        btnSelectDD.Enabled = true;
    }

    protected void btnBack_Insp_Click(object sender, EventArgs e)
    {
        dv_ViewBT.Visible = true;
        dv_Inspections.Visible = false;
    }
    protected void lbSelectInsp_Click(object sender, EventArgs e)
    {
        dv_SelectInsp.Visible = true;
        //lbSelectInsp.Text = "Select Inspections";

        DataTable dt = Common.Execute_Procedures_Select_ByQuery("select * from dbo.m_inspectionGroup");

        rptInspGroups.DataSource = ((dt != null && dt.Rows.Count > 0) ? dt : null);
        rptInspGroups.DataBind();

        if (hfInspIds.Value.Trim() != "")
        {
            CheckSelectedInspections();
        }

    }
    protected DataTable BindInspections(object id)
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("select * from dbo.m_inspection WHERE InspectionGroup=" + id);
        return dt;
    }
    public void CheckSelectedInspections()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("select Code from dbo.m_inspection WHERE Id IN (" + hfInspIds.Value.Trim() + ")");

        foreach (DataRow dr in dt.Rows)
        {
            foreach (RepeaterItem item in rptInspGroups.Items)
            {
                Repeater rptInspections = (Repeater)item.FindControl("rptInspections");

                foreach (RepeaterItem rpt in rptInspections.Items)
                {
                    CheckBox chkInsp = (CheckBox)rpt.FindControl("chkInsp");

                    if (chkInsp.Text == dr["Code"].ToString())
                    {
                        chkInsp.Checked = true;
                    }
                }
            }
        }
    }
    protected void btnCloseInsp_Click(object sender, EventArgs e)
    {
        lbSelectInsp.Text = (lblInspections.Text.Trim() == "" ? "Select Inspections" : "Modify Inspections");
        dv_SelectInsp.Visible = false;
    }
    protected void btnSelectInspctions_Click(object sender, EventArgs e)
    {
        string InspIds = "";

        foreach (RepeaterItem item in rptInspGroups.Items)
        {
            Repeater rptInspections = (Repeater)item.FindControl("rptInspections");

            foreach (RepeaterItem rpt in rptInspections.Items)
            {
                CheckBox chkInsp = (CheckBox)rpt.FindControl("chkInsp");
                
                if (chkInsp.Checked)
                {
                    InspIds = InspIds + chkInsp.Attributes["InspId"].ToString() + ",";
                }
            }
        }

        if (InspIds.Trim() == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "asdf", "alert('Please select inspections.');", true);
            return;
        }

        if (InspIds.Length > 0)
        {
            InspIds = InspIds.TrimEnd(',');
        }

        hfInspIds.Value = InspIds;

        DataTable dt = Common.Execute_Procedures_Select_ByQuery("select Code from dbo.m_inspection WHERE Id IN (" + InspIds + ")");
        string Names = "";
        foreach (DataRow dr in dt.Rows)
        {
            Names = Names + dr["Code"].ToString() + ",";
        }

        lblInspections.Text = " [ " + Names.TrimEnd(',') + " ]";

        btnCloseInsp_Click(sender, e);
    }
    protected void btnSendForApproval_Click(object sender, EventArgs e)
    {
        //LeaveRequestId = Common.CastAsInt32(((Button)sender).CommandArgument);

        dv_SendForApproval.Visible = true;
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT EmpId, EmpCode, (FirstName + ' ' + MiddleName + ' ' + FamilyName) As Name FROM [dbo].[Hr_PersonalDetails]  WHERE HOD = 1 ORDER BY Name");

        ddlApprover.DataSource = dt;
        ddlApprover.DataTextField = "Name";
        ddlApprover.DataValueField = "EmpId";
        ddlApprover.DataBind();

        ddlApprover.Items.Insert(0, new ListItem("< Select >", ""));
    }
    protected void btnSendApprovalTo_Click(object sender, EventArgs e)
    {
        try
        {
            string SQL = "UPDATE [dbo].[HR_OfficeAbsence] SET [ApprovalFwdTo] = " + ddlApprover.SelectedValue.Trim() + ", [RequestedOn]=getdate(),[Status]='R',AppRejOn=null WHERE [LeaveRequestId] = " + LeaveRequestId + " AND [EmpId] = " + EmpId;
            Common.Execute_Procedures_Select_ByQuery(SQL);

            DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT Email FROM [dbo].UserLogin WHERE LoginId = (SELECT UserId FROM [dbo].[Hr_PersonalDetails] WHERE EmpId = " + ddlApprover.SelectedValue.Trim() + " )");
            SendMailForApproval(dt.Rows[0]["Email"].ToString(), "Business Travel Approval Request");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "err", "alert('Sent for approval successfully.');", true);
            dv_SendForApproval.Visible = false;
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "err", "alert('Unable to Send for approval. Error : " + ex.Message + "');", true);
        }

    }
    public void SendMailForApproval(string MailTo, string Subject )
    {
        if (MailTo.Trim() != "")
        {
            List<string> MailTos = new List<string>();
            MailTos.Add(MailTo);
            DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT [GUID] FROM  [dbo].[HR_OfficeAbsence] WHERE LeaveRequestId = " + LeaveRequestId + " AND EmpId = " + EmpId);
            string GUID = dt.Rows[0]["GUID"].ToString();
            string httpPath = @"http://emanagershore.energiosmaritime.com/modules/hrd/EMTM/MyProfile/Public_LeaveApproval.aspx?Key=" + GUID;
            //string httpPath = @"http://localhost:54817/Web/EMTM/MyProfile/Public_LeaveApproval.aspx?Key=" + GUID;

            //Sending mail
            char[] Sep = { ';' };
            string[] ToAdds = MailTos.ToArray();
            //string[] CCAdds = { "asingh@energiossolutions.com" };
            string[] CCAdds = { };
            string[] BCCAdds = "".Split(Sep);
            //----------------------

            String MailBody;
            MailBody = "Dear Sir,";
            MailBody = MailBody + "<br><br>My Business travel plan as given below:";
            MailBody = MailBody + "<br><br>Purpose: " + ddlLeaveType.SelectedItem.Text.Trim();
            MailBody = MailBody + "<br><br>Location: " + ddlLocation.SelectedItem.Text.Trim();
            MailBody = MailBody + "<br><br>Period: " + txtLeaveFrom.Text.Trim() + " - " + txtLeaveTo.Text.Trim();
            MailBody = MailBody + "<br><br>Vessel: " + ddlVessel.SelectedItem.Text.Trim();
            MailBody = MailBody + "<br><br>Remarks: " + txtReason.Text.Trim();
            MailBody = MailBody + "<br><br><br><br>Please <a href='" + httpPath + "' style='color: blue; text-decoration: underline;'>click here</a> to approve/ reject.";
            MailBody = MailBody + "<br><br><br><br>Thank You, ";
            MailBody = MailBody + "<br>" + Session["UserName"].ToString();
            //------------------
            string ErrMsg = "";
            string AttachmentFilePath = "";
            SendEmail.SendeMailAsync(Common.CastAsInt32(Session["LoginId"].ToString()), "emanager@energiossolutions.com", "emanager@energiossolutions.com", ToAdds, CCAdds, BCCAdds, Subject, MailBody, out ErrMsg, AttachmentFilePath);

        }

    }
    protected void btnCloseInspApproval_Click(object sender, EventArgs e)
    {
        LeaveRequestId = 0;
        dv_SendForApproval.Visible = false;
    }
    protected void btnImportInsp_Click(object sender, EventArgs e)
    {
        try
        {
            Common.Set_Procedures("HR_ImportInspections");
            Common.Set_ParameterLength(1);
            Common.Set_Parameters
                (
                  new MyParameter("@LeaveRequestId", Common.CastAsInt32(LeaveRequestId))
                );

            DataSet ds = new DataSet();
            if (Common.Execute_Procedures_IUD_CMS(ds))
            {
                lbAction_SelectInsps_Click(sender, e);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "asdf", "alert('Imported successfully.');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "asdf", "alert('Unable to import. Error : " + Common.getLastError() + "');", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "asdf", "alert('Unable to import. Error : " + ex.Message + "');", true);
        }
    }
    protected void imgDownloadExp_Click(object sender, EventArgs e)
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT FileName,Attachment FROM DBO.HR_OfficeAbsence WHERE LeaveRequestId=" + LeaveRequestId);
        if (dt.Rows.Count > 0)
        {
            string FileName = dt.Rows[0]["FileName"].ToString();
            if (FileName.Trim() != "")
            {
                byte[] buff = (byte[])dt.Rows[0]["Attachment"];
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileName);
                Response.BinaryWrite(buff);
                Response.Flush();
                Response.End();
            }
        }
    }
}