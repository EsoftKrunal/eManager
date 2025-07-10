using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;

public partial class emtm_MyProfile_Public_HOD_LeaveApproval : System.Web.UI.Page
{
    public string Key
    {
        get
        {
            return ViewState["Key"].ToString();
        }
        set
        {
            ViewState["Key"] = value;
        }
    }
    

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["Key"] != null && Request.QueryString["Key"].ToString() != "" )
            {
                Key = Request.QueryString["Key"].ToString();
                ShowRecord();
            }
            else
            {
                btnDone.Visible = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "err", "alert('Invalid Key.'); window.close();", true); 
            }
        }
    }
    public void ShowRecord()
    {
        string sql = "select oa.* ,LocationText=(case when Location=1 THen 'Local' else 'International' end) , " + 
                     "PurposeText=(select Purpose from [HR_LeavePurpose] where purposeid=oa.purposeid),  " +
                     "pd.EmpCode,pd.FirstName +' ' + pd.MiddleName +' ' + pd.FamilyName as [Name], pd.Office,pd.Department,c.OfficeName,pd.Position,dept.DeptName,p.PositionName, " +
                     "(SELECT VesselName FROM DBO.vessel WHERE vesselId = oa.VesselId) As VesselName, hod.RequestedOn, hod.RequestedComments,hod.RepliedOn,hod.RepliedComments " +
                     "from HR_OfficeAbsence oa  " +
                     "INNER JOIN  HR_OfficeAbsence_HODApproval hod on oa.LeaveRequestId = hod.LeaveRequestId " +
                     "left outer join Hr_PersonalDetails pd on oa.empid=pd.empid  " +
                     "left outer join Position p on pd.Position=p.PositionId  " +
                     "Left Outer Join Office c on pd.Office= c.OfficeId  " +
                     "Left Outer Join HR_Department dept on pd.Department=dept.DeptId  " +
                     "WHERE hod.[GUID]='" + Key + "'";
        DataTable dtdata = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        if (dtdata != null && dtdata.Rows.Count > 0)
        {
            DataRow dr = dtdata.Rows[0];

            if (!Convert.IsDBNull(dr["RepliedOn"]))
            {
                btnDone.Visible = false;
                string sessionExpiredURL = ConfigurationManager.AppSettings["SessionExpired"];
                HttpContext.Current.Response.Redirect(sessionExpiredURL);
            }

            lblLocation.Text = "[ " + dr["LocationText"].ToString().Trim() + " Visit ]";
            lblPurpose.Text = dr["PurposeText"].ToString().Trim();
            lblPeriod.Text = "Duration : " + Common.ToDateString(dr["LeaveFrom"]) + " - " + Common.ToDateString(dr["LeaveTo"]);
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
                lblHalfDay.Text += " ( After Office Hrs )";

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
                
        }
        else
        {
            btnDone.Visible = false;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "err", "alert('Invalid Key.'); window.close();", true);
        }
    }
    protected void btnDone_Click(object sender, EventArgs e)
    {

        if (txtAppRejRemarks.Text.Trim() == "")
        {
            txtAppRejRemarks.Focus();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "err", "alert('Please enter comments.');", true);
            return;
        }

        try
        {
            string SQL = "UPDATE [dbo].[HR_OfficeAbsence_HODApproval] SET RepliedComments = '" + txtAppRejRemarks.Text.Replace("'", "`").Trim() + "', RepliedOn=getdate() WHERE [GUID]='" + Key + "'";
            Common.Execute_Procedures_Select_ByQuery(SQL);
            btnDone.Visible = false;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "succ", "alert('Record saved successfully.');window.close();", true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "err", "alert('Unable to save record. Error : " + ex.Message + "');", true);
        }
    }
}