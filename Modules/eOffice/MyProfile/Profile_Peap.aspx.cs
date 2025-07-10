using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;

public partial class emtm_MyProfile_Emtm_Profile_Peap : System.Web.UI.Page
{
    public AuthenticationManager auth;
    public string SessionDeleimeter = ConfigurationManager.AppSettings["SessionDelemeter"];
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
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        Session["CurrentModule"] = "PEAP";
        Session["CurrentPage"] = 1;
        
        if (!IsPostBack)
        {
            EmpId = Common.CastAsInt32(Session["ProfileId"]);
            //EmpId = 17;
           
            if (EmpId > 0)
            {
                DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("SELECT * FROM Hr_PersonalDetails WHERE EMPID=" + EmpId.ToString());
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];
                        lbl_EmpName.Text = "[ " + dr["EmpCode"].ToString() + " ] " + dr["FirstName"].ToString() + " " + dr["MiddleName"].ToString() + " " + dr["FamilyName"].ToString();
                        Session["ProfileName"] = (lbl_EmpName.Text.ToString() == string.Empty) ? "" : lbl_EmpName.Text.ToString();

                    }
                }
                BindGrid();
                BindMyAssessments();
            }
            
        }

    }
    protected void BindGrid()
    {   
        string sql = "SELECT  " +
                    " PEAPID,CATEGORY,EMPCODE,FIRSTNAME + ' ' + MIDDLENAME + ' ' + FAMILYNAME AS EMPNAME,OFFICENAME,POSITIONNAME,DeptName AS DEPARTMENTNAME, " +
                    " AppraiselType =  CASE WHEN PM.Occasion = 'R' THEN 'Routine' ELSE 'Interim' END " +
                    " ,Replace(Convert(Varchar,PM.PEAPPERIODFROM,106),' ','-')PEAPPERIODFROM ,Replace(Convert(Varchar,PM.PEAPPERIODTO,106),' ','-')PEAPPERIODTO , " +
                    " STATUS = CASE  " +
                    " WHEN PM.STATUS=-1 THEN 'PEAP Cancelled' " +
                    " WHEN PM.STATUS=0 THEN 'Self Assessment' " +
                    " WHEN PM.STATUS=1 THEN 'Self Assessment' " +
                    " WHEN PM.STATUS=2 THEN 'With Appraiser' " +
                    " WHEN PM.STATUS=3 THEN 'With Management' " +
                    " WHEN PM.STATUS=4 THEN 'PEAP Closed' " +
                    " WHEN PM.STATUS=5 THEN 'With Management' " +
                    " WHEN PM.STATUS=6 THEN 'With Management' " +
                    " ELSE '' END " +
                    " FROM  " +
                    " dbo.HR_EmployeePeapMaster PM  " +
                    " INNER JOIN dbo.Hr_PersonalDetails PD ON PM.EMPID=PD.EMPID " +
                    " LEFT JOIN dbo.HR_PeapCategory PC ON PC.PID=PM.PeapCategory " +
                    " LEFT JOIN OFFICE O ON O.OFFICEID=PM.OFFICEID " + 
                    " LEFT JOIN POSITION P ON P.POSITIONID=PD.POSITION " +
                    " LEFT JOIN HR_Department D ON D.DeptId=PD.DEPARTMENT WHERE PD.EmpId =" + EmpId.ToString();        

        DataTable Ds = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        rptData.DataSource = Ds;
        rptData.DataBind();
    }
    public void BindMyAssessments()
    {
        string SQL = "SELECT   PEAPID,CATEGORY,EMPCODE,FIRSTNAME + ' ' + MIDDLENAME + ' ' + FAMILYNAME AS EMPNAME,OFFICENAME,POSITIONNAME,DeptName AS DEPARTMENTNAME, " +
                     " AppraiselType =  CASE WHEN PM.Occasion = 'R' THEN 'Routine' ELSE 'Interim' END, " +
                     "Replace(Convert(Varchar,PM.PEAPPERIODFROM,106),' ','-')PEAPPERIODFROM , " +
                     "Replace(Convert(Varchar,PM.PEAPPERIODTO,106),' ','-')PEAPPERIODTO ,PM.STATUS AS STATUS1, " +
                      " STATUS = CASE  " +
                     " WHEN PM.STATUS=-1 THEN 'PEAP Cancelled' " +
                     " WHEN PM.STATUS=0 THEN 'Self Assessment' " +
                     " WHEN PM.STATUS=1 THEN 'Self Assessment' " +
                     " WHEN PM.STATUS=2 THEN 'With Appraiser' " +
                     " WHEN PM.STATUS=3 THEN 'With Management' " +
                     " WHEN PM.STATUS=4 THEN 'PEAP Closed' " +
                     " WHEN PM.STATUS=5 THEN 'With Management' " +
                     " WHEN PM.STATUS=6 THEN 'With Management' " +
                     " ELSE '' END " +
                     "FROM   dbo.HR_EmployeePeapMaster PM  " +
                     "INNER JOIN dbo.Hr_PersonalDetails PD ON PM.EMPID=PD.EMPID  " +
                     "LEFT JOIN dbo.HR_PeapCategory PC ON PC.PID=PM.PeapCategory  " +
                     "LEFT JOIN OFFICE O ON O.OFFICEID=PM.OFFICEID  " +
                     "LEFT JOIN POSITION P ON P.POSITIONID=PD.POSITION  " +
                     "LEFT JOIN HR_Department D ON D.DeptId=PD.DEPARTMENT  " +
                     "WHERE PM.Status = 2 AND PEAPID IN ( SELECT PeapId FROM HR_EmployeePeap_Appraisers WHERE AppraiserByUser = " + EmpId.ToString() + " ) " +
                     "UNION " +

                     "SELECT   PEAPID,CATEGORY,EMPCODE,FIRSTNAME + ' ' + MIDDLENAME + ' ' + FAMILYNAME AS EMPNAME,OFFICENAME,POSITIONNAME,DeptName AS DEPARTMENTNAME, " +
                     " AppraiselType =  CASE WHEN PM.Occasion = 'R' THEN 'Routine' ELSE 'Interim' END, " +
                     "Replace(Convert(Varchar,PM.PEAPPERIODFROM,106),' ','-')PEAPPERIODFROM , " +
                     "Replace(Convert(Varchar,PM.PEAPPERIODTO,106),' ','-')PEAPPERIODTO ,PM.STATUS AS STATUS1, " +
                      " STATUS = CASE  " +
                     " WHEN PM.STATUS=-1 THEN 'PEAP Cancelled' " +
                     " WHEN PM.STATUS=0 THEN 'Self Assessment' " +
                     " WHEN PM.STATUS=1 THEN 'Self Assessment' " +
                     " WHEN PM.STATUS=2 THEN 'With Appraiser' " +
                     " WHEN PM.STATUS=3 THEN 'With Management' " +
                     " WHEN PM.STATUS=4 THEN 'PEAP Closed' " +
                     " WHEN PM.STATUS=5 THEN 'With Management' " +
                     " WHEN PM.STATUS=6 THEN 'With Management' " +
                     " ELSE '' END " +
                     "FROM   dbo.HR_EmployeePeapMaster PM  " +
                     "INNER JOIN dbo.Hr_PersonalDetails PD ON PM.EMPID=PD.EMPID  " +
                     "LEFT JOIN dbo.HR_PeapCategory PC ON PC.PID=PM.PeapCategory  " +
                     "LEFT JOIN OFFICE O ON O.OFFICEID=PM.OFFICEID  " +
                     "LEFT JOIN POSITION P ON P.POSITIONID=PD.POSITION  " +
                     "LEFT JOIN HR_Department D ON D.DeptId=PD.DEPARTMENT  " +
                     "WHERE PM.Status = 5 AND PEAPID IN ( SELECT PeapId FROM HR_EmployeePeap_ManagementFeedBack WHERE ManagerId = " + EmpId.ToString() + " ) ";

                    

        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(SQL);
        rptAssessment.DataSource = dt;
        rptAssessment.DataBind();

    }
    protected void btnView_Click(object sender, ImageClickEventArgs e)
    {
        int PeapID = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        Response.Redirect("../StaffAdmin/Emtm_PeapSummary.aspx?PeapID=" + PeapID + "&Mode=P");
    }
}