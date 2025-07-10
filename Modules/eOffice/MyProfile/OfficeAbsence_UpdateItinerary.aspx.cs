using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.IO;

public partial class emtm_MyProfile_Emtm_OfficeAbsence_UpdateItinerary : System.Web.UI.Page
{
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
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        
        lblUMsg.Text = "";
        if (!Page.IsPostBack)
        {
            BindTime();
            
            if (Request.QueryString["id"] != null)
            {
                LeaveRequestId = Common.CastAsInt32(Request.QueryString["id"]);                
                ShowRecord();
            }
        }
    }

    public void BindTime()
    {
        for (int i = 0; i < 24; i++)
        {
            if (i < 10)
            {
                ddlEndHr.Items.Add(new ListItem("0" + i.ToString(), "0" + i.ToString()));
                ddlEtHr.Items.Add(new ListItem("0" + i.ToString(), "0" + i.ToString()));
            }
            else
            {
                ddlEndHr.Items.Add(new ListItem(i.ToString(), i.ToString()));
                ddlEtHr.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
        }

        for (int i = 0; i < 60; i++)
        {
            if (i < 10)
            {
                ddlEndMin.Items.Add(new ListItem("0" + i.ToString(), "0" + i.ToString()));
                ddlEtMin.Items.Add(new ListItem("0" + i.ToString(), "0" + i.ToString()));
            }
            else
            {
                ddlEndMin.Items.Add(new ListItem(i.ToString(), i.ToString()));
                ddlEtMin.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
        }
    }
    public void ShowRecord()
    {
        DataTable dt = new DataTable();
        string sql = "select oa.* ,LocationText=(case when Location=1 THen 'Local' else 'International' end) , " +
                     "PurposeText=(select Purpose from [HR_LeavePurpose] where purposeid=oa.purposeid),  " +
                     "pd.EmpCode,pd.FirstName +' ' + pd.MiddleName +' ' + pd.FamilyName as [Name], pd.Office,pd.Department,c.OfficeName,pd.Position,dept.DeptName,p.PositionName, " +
                     "(SELECT VesselName FROM DBO.vessel WHERE vesselId = oa.VesselId) As VesselName " +
                     "from HR_OfficeAbsence oa  " +
                     "left outer join Hr_PersonalDetails pd on oa.empid=pd.empid  " +
                     "left outer join Position p on pd.Position=p.PositionId  " +
                     "Left Outer Join Office c on pd.Office= c.OfficeId  " +
                     "Left Outer Join HR_Department dept on pd.Department=dept.DeptId  " +
                     "WHERE oa.LeaveRequestId=" + LeaveRequestId;
        DataTable dtdata = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        if (dtdata != null && dtdata.Rows.Count > 0)
        {
            DataRow dr = dtdata.Rows[0];
            lblLocation.Text = "[ " + dr["LocationText"].ToString().Trim() + " Visit ]";
            lblPurpose.Text =  dr["PurposeText"].ToString().Trim();
            lblPeriod.Text =  "Duration : " + Common.ToDateString(dr["LeaveFrom"]) + " - " + Common.ToDateString(dr["LeaveTo"]);
            lblVesselName.Text = "";

            if (lblPurpose.Text.Contains("Vessel") || lblPurpose.Text.Contains("Docking"))
            {
                lblVesselName.Text = "Vessel : " + dr["VesselName"].ToString().Trim();
            }
            lblRemarks.Text = dr["Reason"].ToString().Trim();

            int HalfDay = Common.CastAsInt32(dr["HalfDay"]);
            switch (HalfDay)
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
            {
                lblHalfDay.Text += " ( After Office Hrs )";
            }

            lblPlannedInspections.Text = "";
            if (lblPurpose.Text.Contains("Vessel"))
            {
                string Inspections = dr["Inspections"].ToString().Trim();
                if (Inspections.Trim() != "")
                {
                    dt = Common.Execute_Procedures_Select_ByQuery("SELECT CODE FROM DBO.m_Inspection WHERE ID IN (" + Inspections + ")");
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

        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "err", "alert('Invalid Key.'); window.close();", true);
            return;
        }

        
    
        string SQL = "SELECT  REPLACE(Convert(varchar(11), OA.ActFromDt, 106),' ','-') AS ActFromDt,REPLACE(Convert(varchar(11), OA.ActToDt, 106),' ','-') AS ActToDt, OA.ActFromDt AS Time,OA.ActToDt AS EndTime, LP.Purpose, REPLACE(Convert(varchar(11), OA.LeaveFrom, 106),' ','-') AS LeaveFrom, REPLACE(Convert(varchar(11), OA.LeaveTo, 106),' ','-') AS LeaveTo FROM HR_OfficeAbsence OA " +
                     "INNER JOIN HR_LeavePurpose LP ON OA.PurposeId = LP.PurposeId " +
                     "WHERE OA.LeaveRequestId = " + LeaveRequestId;
        dt = Common.Execute_Procedures_Select_ByQueryCMS(SQL);

        if (dt.Rows.Count > 0)
        {
            txtActLeaveFrom.Text = dt.Rows[0]["ActFromDt"].ToString();
            txtEndDt.Text = dt.Rows[0]["ActToDt"].ToString() == "01-Jan-1900" ? "" : dt.Rows[0]["ActToDt"].ToString();

            if (dt.Rows[0]["Time"].ToString() != "")
            {
                string time = dt.Rows[0]["Time"].ToString();
                string[] str = time.Split(' ');

                ddlEtHr.SelectedValue = str[1].ToString().Split(':').GetValue(0).ToString().Trim();
                ddlEtMin.SelectedValue = str[1].ToString().Split(':').GetValue(1).ToString().Trim();
            }
            if (dt.Rows[0]["EndTime"].ToString() != "")
            {
                string endtime = dt.Rows[0]["EndTime"].ToString();
                string[] str = endtime.Split(' ');

                ddlEndHr.SelectedValue = str[1].ToString().Split(':').GetValue(0).ToString().Trim();
                ddlEndMin.SelectedValue = str[1].ToString().Split(':').GetValue(1).ToString().Trim();
            }

            lblPurpose.Text = dt.Rows[0]["Purpose"].ToString();
            lblPeriod.Text = dt.Rows[0]["LeaveFrom"].ToString() + " to " + dt.Rows[0]["LeaveTo"].ToString();
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (txtActLeaveFrom.Text.Trim() == "")
        {
            lblUMsg.Text = "Please enter start date.";
            txtActLeaveFrom.Focus();
            return;
        }

        DateTime dt;
        if (!DateTime.TryParse(txtActLeaveFrom.Text.Trim(), out dt))
        {
            lblUMsg.Text = "Please enter valid date.";
            txtActLeaveFrom.Focus();
            return;
        }

        string STime = txtActLeaveFrom.Text.Trim() + " " + ddlEtHr.SelectedValue.Trim() + ":" + ddlEtMin.SelectedValue.Trim();
        string ETime = "";

        if (txtEndDt.Text.Trim() != "")
        {
            ETime = txtEndDt.Text.Trim() + " " + ddlEndHr.SelectedValue.Trim() + ":" + ddlEndMin.SelectedValue.Trim();
        }

        string SQL = "UPDATE HR_OfficeAbsence SET ActFromDt = '" + STime + "', ActToDt = '" + ETime + "' WHERE LeaveRequestId=" + LeaveRequestId + "; SELECT -1";


        DataTable dtUpdate = Common.Execute_Procedures_Select_ByQueryCMS(SQL);

        if (dtUpdate.Rows.Count > 0)
        {
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "alert('Record saved successfully.');", true);
            lblUMsg.Text = "Record saved successfully.";
        }
        else
        {
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "error", "alert('Unable to save record.');", true);
            lblUMsg.Text = "Unable to save record.";
        }
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "openreport('" + LeaveRequestId + "');", true);
    }
}