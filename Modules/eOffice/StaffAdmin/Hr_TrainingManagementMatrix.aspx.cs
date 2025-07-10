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

public partial class Emtm_Hr_TrainingManagementMatrix : System.Web.UI.Page
{

    public string OrderBy
    {
        set { ViewState["OrderBy"] = value; }
        get { try { return ViewState["OrderBy"].ToString(); } catch { return " order by TrainingName"; } }
    }
    public int Year
    {
        set { ViewState["Year"] = value; }
        get { return Common.CastAsInt32( ViewState["Year"]);  }
    }
    public int GID
    {
        set { ViewState["_GID"] = value; }
        get { return Common.CastAsInt32(ViewState["_GID"]); }
    }

    
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        if (!IsPostBack)
        {
            int PID = Common.CastAsInt32(Page.Request.QueryString["PID"].ToString());
            GID = Common.CastAsInt32(Page.Request.QueryString["GID"].ToString());
            Year = Common.CastAsInt32(Page.Request.QueryString["Year"].ToString());
            lblYear.Text = Year.ToString();

            DataTable dtPosName = Common.Execute_Procedures_Select_ByQueryCMS("SELECT PositionName FROM dbo.Position WHERE POSITIONID=" + PID.ToString());
            if (dtPosName.Rows.Count > 0)
            {
                lblPos.Text = dtPosName.Rows[0]["PositionName"].ToString();
            }

            DataTable dtPosTrainingType = Common.Execute_Procedures_Select_ByQueryCMS("SELECT TrainingGroupName FROM dbo.HR_TrainingGroup WHERE TrainingGroupID=" + GID.ToString());
            if (dtPosName.Rows.Count > 0)
            {
                lblTG.Text = dtPosTrainingType.Rows[0]["TrainingGroupName"].ToString();
            }

            BindDistinctEmployee(PID);

        }
    }
    protected void Sorting(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        OrderBy = " order by " + lnk.CommandArgument;
        //ShowCrew();
    }
    //   -------------------    Function
    public void BindDistinctEmployee(int PID)
    {
        string sql = "";
        string WC = "";

        //sql = " SELECT EPD.EmpID,TR.TRAININGID,TR.TrainingRecommID,A.TrainingPlanningID,EPD.EMPCODE,(ISNULL(EPD.FirstName,'')+ ' ' +ISNULL(EPD.FamilyName,'')) as EmpName,TM.TRAININGID,TM.TRAININGNAME,TR.DUEDATE,A.STARTDATE AS PLANSTART,A.ENDDATE AS PLANEND,A.STATUS,A.CANCELLEDON ,POS.PositionName, " +
        //       " (Case when (DUEDATE <=GETDATE() And (A.STATUS IS NULL OR A.STATUS='A' OR A.STATUS='C')) then 'OverDue' else ''end)IsOverDue," +
        //      " (CASE A.STATUS WHEN 'C' THEN 'Cancelled' WHEN 'E' THEN 'Completed' WHEN 'A' THEN 'Planned' ELSE 'Due'  END) StatusName , " +
        //      " (CASE A.STATUS WHEN 'C' THEN 'Cancelled' WHEN 'E' THEN 'Completed' WHEN 'A' THEN 'Planned' ELSE 'Due'  END) StatusCss , " +
        //      " A.STARTDATE1 AS CompStart,A.ENDDATE1 AS CompEnd " +
        //      " FROM dbo.HR_TrainingRecommended TR  " +
        //      " LEFT JOIN  " +
        //      "   ( " +
        //      "   SELECT PD.TRAININGPLANNINGID,PD.TRAININGRECOMMID,PM.STARTDATE,PM.ENDDATE,PM.STARTDATE1,PM.ENDDATE1,PM.CANCELLEDON,PM.STATUS FROM dbo.HR_TrainingPlanningDetails PD " +
        //      "   INNER JOIN dbo.HR_TrainingPlanning PM ON PM.TRAININGPLANNINGID=PD.TRAININGPLANNINGID   " +
        //      "   ) A ON TR.TRAININGRECOMMID=A.TRAININGRECOMMID " +
        //      "   INNER JOIN dbo.HR_TrainingMaster TM ON TM.TRAININGID=TR.TRAININGID " +
        //      "   INNER JOIN dbo.Hr_PersonalDetails EPD ON EPD.EMPID=TR.EMPID " +
        //      "   left JOIN dbo.HR_Department D ON D.DEPTID=EPD.DEPARTMENT " +
        //      "   left JOIN dbo.Position POS ON POS.PositionID=EPD.Position Where 1=1 "+


        //sql = "SELECT * FROM vw_HR_TrainingMatrixData V" +
        //      " WHERE  V.TrainingGroupID=" + GID + " And V.Position =" + PID + " And Year(V.DUEDATE)=" + Year.ToString();

        sql = "select EMPID,FIRSTNAME+ ' '+MIDDLENAME + ' ' + FAMILYNAME AS EMPNAME,POSITIONNAME from  " +
              "Hr_PersonalDetails E INNER JOIN POSITION P " +
              "ON E.POSITION=P.POSITIONID where DRC IS NULL AND POSITION=" + PID.ToString();

        sql = sql + WC;
        DataSet ds = Budget.getTable(sql);
        //rptCrew.DataSource = ds;
        //rptCrew.DataBind();
        //----------------
        
        DataView DV = ds.Tables[0].DefaultView;
        string[] Cols = { "EmpName", "EmpID", "PositionName" };


        DataTable TempDt = DV.ToTable(true, Cols);
        //foreach (DataRow dr in TempDt.Rows)
        //{
        //    Common.Set_Procedures("Dbo.HR_AUTO_TRAININGASSIGN");
        //    Common.Set_ParameterLength(2);
        //    Common.Set_Parameters(
        //        new MyParameter("@EMPID", dr["EmpID"].ToString()),
        //        new MyParameter("@GID", GID.ToString())
        //        );
        //    DataSet TempDs = new DataSet();
        //    Common.Execute_Procedures_IUD(TempDs);
        //}

        rptEmployee.DataSource = TempDt;
        rptEmployee.DataBind();
    }

    public DataSet BindTraining(string EmpID)
    {
        string sql = "";
        string WC = "";

        //sql = " SELECT EPD.EmpID,TR.TRAININGID,TR.TrainingRecommID,A.TrainingPlanningID,EPD.EMPCODE,(ISNULL(EPD.FirstName,'')+ ' ' +ISNULL(EPD.FamilyName,'')) as EmpName,TM.TRAININGID,TM.TRAININGNAME,TR.DUEDATE,A.STARTDATE AS PLANSTART,A.ENDDATE AS PLANEND,A.STATUS,A.CANCELLEDON ,POS.PositionCode, " +
        //       " (Case when (DUEDATE <=GETDATE() And (A.STATUS IS NULL OR A.STATUS='A' OR A.STATUS='C')) then 'OverDue' else ''end)IsOverDue," +
        //      " (CASE A.STATUS WHEN 'C' THEN 'Cancelled' WHEN 'E' THEN 'Completed' WHEN 'A' THEN 'Planned' ELSE 'Due'  END) StatusName , " +
        //      " (CASE A.STATUS WHEN 'C' THEN 'Cancelled' WHEN 'E' THEN 'Completed' WHEN 'A' THEN 'Planned' ELSE 'Due'  END) StatusCss , " +
        //      " A.STARTDATE1 AS CompStart,A.ENDDATE1 AS CompEnd,CASE WHEN TR.RECOMMSOURCE='A' THEN 'MANUAL' WHEN TR.RECOMMSOURCE='P' THEN 'PEAP' WHEN TR.RECOMMSOURCE='M' THEN 'MATRIX' ELSE 'OTHER' END AS SOURCE,ValidityPeriod,EXPIRYDATE " +
        //      " FROM dbo.HR_TrainingRecommended TR  " +
        //      " LEFT JOIN  " +
        //      "   ( " +
        //      "   SELECT PD.TRAININGPLANNINGID,PD.TRAININGRECOMMID,PM.STARTDATE,PM.ENDDATE,PM.STARTDATE1,PM.ENDDATE1,PM.CANCELLEDON,PM.STATUS,PD.EXPIRYDATE FROM dbo.HR_TrainingPlanningDetails PD " +
        //      "   INNER JOIN dbo.HR_TrainingPlanning PM ON PM.TRAININGPLANNINGID=PD.TRAININGPLANNINGID   " +
        //      "   ) A ON TR.TRAININGRECOMMID=A.TRAININGRECOMMID " +
        //      "   INNER JOIN dbo.HR_TrainingMaster TM ON TM.TRAININGID=TR.TRAININGID " +
        //      "   INNER JOIN dbo.Hr_PersonalDetails EPD ON EPD.EMPID=TR.EMPID " +
        //      "   left JOIN dbo.HR_Department D ON D.DEPTID=EPD.DEPARTMENT " +
        //      "   left JOIN dbo.Position POS ON POS.PositionID=EPD.Position "+

        sql = " SELECT * FROM vw_HR_TrainingMatrixData V " +
              " Where V.TrainingGroupID=" + GID.ToString() + " And V.EmpID=" + EmpID + " And Year(V.DUEDATE)=" + Year.ToString();
        sql = sql + WC + OrderBy;
        DataSet ds = Budget.getTable(sql);
        return ds;
    }

}
