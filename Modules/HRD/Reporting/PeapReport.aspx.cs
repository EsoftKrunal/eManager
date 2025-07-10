using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;



public partial class Reporting_PeapReport : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    public int PeapId
    {
        get
        {return Common.CastAsInt32(ViewState["PeapId"].ToString());}
        set
        {ViewState["PeapId"] = value;}
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        if (Request.QueryString["PId"] != null || Request.QueryString["PId"].ToString() != "")
        {
            PeapId = Common.CastAsInt32(Request.QueryString["PId"].ToString());
            ShowReport();
            //-----------------------
            if ("" + Request.QueryString["Mode"] == "C")
            {
                CrystalReportViewer1.Visible = false;
                DataTable DT = Common.Execute_Procedures_Select_ByQueryCMS("select case when Occasion='I' THEN 'Interim' when Occasion='R' THEN 'Routine' else '' end as Ocassion,PeapPeriodTo from HR_EmployeePeapMaster where peapid=1");
                if (DT.Rows.Count > 0)
                {
                    string TempPath = Server.MapPath("~/Temp");

                    DateTime PeapPeriodTo = Convert.ToDateTime(DT.Rows[0]["PeapPeriodTo"].ToString());
                    string DocumentName = "PEAP REPORT";
                    string DocumentNo = "PEAP-" + DT.Rows[0]["Ocassion"].ToString() + "-" + PeapPeriodTo.Year.ToString();
                    string FileName = DocumentNo + "-" + PeapId.ToString() + ".pdf";
                    string FilePath =  TempPath + "\\" + FileName;

                    
                    rpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, FilePath);

                    int EmpId = Common.CastAsInt32(Session["EmpId"]);
                    Common.Set_Procedures("HR_InsertUpdateOtherDocsDetails");
                    Common.Set_ParameterLength(9);
                    Common.Set_Parameters(new MyParameter("@OtherDocId", 0),
                        new MyParameter("@EmpId", EmpId),
                        new MyParameter("@DocumentName", DocumentName),
                        new MyParameter("@DocumentNo", DocumentNo),
                        new MyParameter("@PlaceOfIssue", ""),
                        new MyParameter("@IssueDate", PeapPeriodTo),
                        new MyParameter("@ExpiryDate", DBNull.Value),
                        new MyParameter("@FileName", FileName),
                        new MyParameter("@FileImage", System.IO.File.ReadAllBytes(FilePath)));
                    DataSet ds = new DataSet();
                    if (Common.Execute_Procedures_IUD_CMS(ds))
                    {
                        //---- Closure-----------------
                        string strClosureSQL = "UPDATE HR_EmployeePeapMaster SET [Status] = 4 WHERE PeapId = " + PeapId.ToString();
                        Common.Execute_Procedures_Select_ByQueryCMS(strClosureSQL);
                        //-----------------------------
                       
                        lblMessage.Text = "Peap report closed successfully.";
                    }
                    else
                    {
                        lblMessage.Text = "Unable to close peap report.";
                    }
                }
            }
        }
    }

    public void ShowReport()
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DataTable dtSA = new DataTable();
        DataTable dtJS = new DataTable();
        DataTable dtComp = new DataTable();
        DataTable dtPotAss = new DataTable();
        DataTable dtMF = new DataTable();


        //string SQL = "SELECT PEAPID,CATEGORY As PeapLevel,CASE Occasion WHEN 'R' THEN 'Routine' WHEN 'I' THEN 'Interim' ELSE '' END AS Occasion, POSITIONNAME,EMPCODE,FIRSTNAME , FAMILYNAME,D.DeptName AS DepartmentName, Replace(Convert(varchar(11),PM.SAOnDt,106),' ','-') AS SAOnDt, " +
        //             "Replace(Convert(Varchar,PM.PEAPPERIODFROM,106),' ','-') AS PEAPPERIODFROM ,Replace(Convert(Varchar,PM.PEAPPERIODTO,106),' ','-') AS PEAPPERIODTO,Replace(Convert(Varchar,PD.DJC,106),' ','-') AS DJC,PM.Status, PM.EmpId " +
        //             "FROM HR_EmployeePeapMaster PM  " +
        //             "INNER JOIN dbo.Hr_PersonalDetails PD ON PM.EMPID=PD.EMPID  " +
        //             "LEFT JOIN dbo.HR_PeapCategory PC ON PC.PID= PM.PeapCategory  " +
        //             "LEFT JOIN POSITION P ON P.POSITIONID=PD.POSITION  " +
        //             "LEFT JOIN HR_Department D ON D.DeptId=PD.DEPARTMENT  " +
        //             "WHERE PM.PeapId = " + PeapId.ToString() + " ";

        string SQL = "SELECT * FROM   dbo.vw_PeapReport WHERE PEAPID=" + PeapId.ToString() + " ";
                 
       dt = Common.Execute_Procedures_Select_ByQueryCMS(SQL);
       dt.TableName = "vw_PeapReport";

       string SASQL = "SELECT  * FROM dbo.HR_EmployeePeapSADetails WHERE PEAPID=" + PeapId.ToString() + " ORDER BY QID ";
       dtSA = Common.Execute_Procedures_Select_ByQueryCMS(SASQL);
       dtSA.TableName = "vw_SelfAppraisalData";

       string JSSQL = "SELECT * FROM vw_PerformanceontheJob_kpi_Data WHERE PeapId = " + PeapId.ToString() + " ";
       dtJS = Common.Execute_Procedures_Select_ByQueryCMS(JSSQL);
       dtJS.TableName = "vw_PerformanceontheJobData";

       string CompSQL = "SELECT * FROM vw_CompetencyData WHERE PeapId = " + PeapId.ToString() + " ";
       dtComp = Common.Execute_Procedures_Select_ByQueryCMS(CompSQL);
       dtComp.TableName = "vw_CompetencyData";

       string PotSQL = "SELECT * FROM vw_PotentialAssessmentData WHERE PeapId = " + PeapId.ToString() + " ";
       dtPotAss = Common.Execute_Procedures_Select_ByQueryCMS(PotSQL);
       dtPotAss.TableName = "vw_PotentialAssessmentData";

       string MFSQL = "SELECT * FROM vw_ManagementFeedBackData WHERE PeapId = " + PeapId.ToString() + " ";
       dtMF = Common.Execute_Procedures_Select_ByQueryCMS(MFSQL);
       dtMF.TableName = "vw_ManagementFeedBackData";

       CrystalReportViewer1.ReportSource = rpt;

        rpt.Load(Server.MapPath("../Reporting/PeapReport.rpt"));


        rpt.SetDataSource(dt);

        rpt.Subreports["SelfAppraisalReport.rpt"].SetDataSource(dtSA);
        rpt.Subreports["Performanceonthejob.rpt"].SetDataSource(dtJS);
        rpt.Subreports["EmployeeCompetency.rpt"].SetDataSource(dtComp);
        rpt.Subreports["PotentialAssesment.rpt"].SetDataSource(dtPotAss);
        rpt.Subreports["ManagementFeedBack.rpt"].SetDataSource(dtMF);
        

        //rpt.SetParameterValue("Title", "Jan - " + Request.QueryString["Year"].ToString());
        


    }

    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
}