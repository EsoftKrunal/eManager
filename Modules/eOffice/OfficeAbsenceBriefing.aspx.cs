using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.IO;

public partial class Emtm_OfficeAbsenceBriefing : System.Web.UI.Page
{
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
    public int BriefingID
    {
        get
        {
            return Common.CastAsInt32(ViewState["_BriefingID"]);
        }
        set
        {
            ViewState["_BriefingID"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        lblMsgBriefing.Text = "";        
        if (!Page.IsPostBack)
        {
            EmpId = Common.CastAsInt32(Session["ProfileId"]);
            
            if (Request.QueryString["id"] != null)
            {
                BriefingID = Common.CastAsInt32(Request.QueryString["id"]);
                ShowRecord();
            }
            dvBreifing.Visible = Request.QueryString["Type"] == "B";
            dvDebriefing.Visible = Request.QueryString["Type"] == "D";
        }
        
    }

    // ----------------------------------------  EVENT    
   
    // ----------------------------------------  FUNCTION
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
                     "WHERE oa.LeaveRequestId=" + BriefingID;
        DataTable dtdata = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        if (dtdata != null && dtdata.Rows.Count > 0)
        {
            DataRow dr = dtdata.Rows[0];
            lblLocation.Text = lblLocation1.Text = "[ " + dr["LocationText"].ToString().Trim() + " Visit ]";
            lblPurpose.Text = lblPurpose1.Text = dr["PurposeText"].ToString().Trim();
            lblPeriod.Text = lblPeriod1.Text = "Duration : " + Common.ToDateString(dr["LeaveFrom"]) + " - " + Common.ToDateString(dr["LeaveTo"]);
            lblVesselName.Text = lblVesselName1.Text = "";

            if (lblPurpose.Text.Contains("Vessel") || lblPurpose.Text.Contains("Docking"))
            {
                lblVesselName.Text = lblVesselName1.Text = "Vessel : " + dr["VesselName"].ToString().Trim();
            }
            lblRemarks.Text = lblRemarks1.Text = dr["Reason"].ToString().Trim();

            int HalfDay = Common.CastAsInt32(dr["HalfDay"]);
            switch (HalfDay)
            {
                case 1:
                    lblHalfDay.Text = lblHalfDay1.Text = "Halfday - First Half";
                    break;
                case 2:
                    lblHalfDay.Text = lblHalfDay1.Text = "Halfday - Second Half";
                    break;
                default:
                    lblHalfDay.Text = lblHalfDay1.Text = "";
                    break;
            }

            if (dr["AfterOfficeHr"].ToString() == "True")
            {
                lblHalfDay.Text += " ( After Office Hrs )";
                lblHalfDay1.Text += " ( After Office Hrs )";
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
                    lblPlannedInspections.Text = lblPlannedInspections1.Text = "Planned Inspections : " + Inspections;
                }
            }
            if (lblPurpose.Text.Contains("Docking"))
            {
                lblPlannedInspections.Text = lblPlannedInspections1.Text = "DD # : ";
            }

        }
        
        
       string  SQL = "SELECT (SELECT FIRSTNAME+' '+LASTNAME FROM USERLOGIN UL WHERE UL.LOGINID=B.BriefBy )BriefByName "+
                " , (SELECT FIRSTNAME+' '+LASTNAME FROM USERLOGIN UL WHERE UL.LOGINID=B.DeBriefBy )DeBriefByName " +
                " ,* FROM HR_OfficeAbsence_Briefing B WHERE LEAVEREQUESTID=" + BriefingID.ToString();
        DataTable dt1 = Common.Execute_Procedures_Select_ByQueryCMS(SQL);
        if (dt1.Rows.Count > 0)
        {
            DataRow Dr=dt1.Rows[0];
            // Briefing
            if (Dr["BriefingRemark"].ToString().Trim() != "")
            {
                txtBriefingRemarks.Text = Dr["BriefingRemark"].ToString();
                lblBriefingByOn.Text = Dr["BriefByName"].ToString() + " [ " + Common.ToDateString(Dr["BriefOn"]) + " ]";
            }
            // De Briefing

            if (Dr["DeBriefRemark"].ToString().Trim() != "")
            {
                txtDebriefingRemarks.Text = Dr["DeBriefRemark"].ToString();
                lblDebriefingByOn.Text = Dr["DeBriefByName"].ToString() + " [ " + Common.ToDateString(Dr["DeBriefOn"]) + " ]";
            }
        }
        
    }
    protected void btnSaveBriefing_OnClick(object sender, EventArgs e)
    {
        if (Request.QueryString["Type"] == "B")
        {
            if (txtBriefingRemarks.Text.Trim() == "")
            {
                txtBriefingRemarks.Focus();
                lblMsgBriefing.Text = "Please enter remarks.";
                return;
            }

            Common.Set_Procedures("dbo.HR_IU_Briefing");
            Common.Set_ParameterLength(8);
            Common.Set_Parameters(
                    new MyParameter("@LeaveRequestId", BriefingID),
                    new MyParameter("@BriefingRemark", txtBriefingRemarks.Text.Trim()),
                    new MyParameter("@BriefBy", Common.CastAsInt32(Session["loginID"])),
                    new MyParameter("@BriefOn", DateTime.Today.Date),
                    new MyParameter("@DeBriefRemark", ""),
                    new MyParameter("@DeBriefBy", ""),
                    new MyParameter("@DeBriefOn", ""),
                    new MyParameter("@Mode", "B")
                );


            DataSet ds = new DataSet();
            if (Common.Execute_Procedures_IUD_CMS(ds))
            {

                lblMsgBriefing.Text = "Record saved successfully.";
            }
            else
            {
                lblMsgBriefing.Text = "Unable To Save Record. Error : " + Common.getLastError();
            }
        }
        else
        {
            if (txtDebriefingRemarks.Text.Trim() == "")
            {
                txtDebriefingRemarks.Focus();
                lblMsgBriefing.Text = "Please enter remarks.";
                return;
            }

            Common.Set_Procedures("dbo.HR_IU_Briefing");
            Common.Set_ParameterLength(8);
            Common.Set_Parameters(
                    new MyParameter("@LeaveRequestId", BriefingID),
                    new MyParameter("@BriefingRemark", ""),
                    new MyParameter("@BriefBy", ""),
                    new MyParameter("@BriefOn", ""),
                    new MyParameter("@DeBriefRemark", txtDebriefingRemarks.Text.Trim()),
                    new MyParameter("@DeBriefBy", Common.CastAsInt32(Session["loginID"])),
                    new MyParameter("@DeBriefOn", DateTime.Today.Date),
                    new MyParameter("@Mode", "D")
                );

            DataSet ds = new DataSet();
            if (Common.Execute_Procedures_IUD_CMS(ds))
            {

                lblMsgBriefing.Text = "Record saved successfully.";
            }
            else
            {
                lblMsgBriefing.Text = "Unable To Save Record. Error : " + Common.getLastError();
            }
        }
    }
}