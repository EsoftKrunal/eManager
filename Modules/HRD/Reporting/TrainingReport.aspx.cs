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

public partial class Reporting_TrainingReport : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    
    public int EmpId
    {
        get {

              return Common.CastAsInt32(ViewState["EmpId"].ToString());
            
            }

        set {
              ViewState["EmpId"] = value;
            }
    }
   
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
            if (Request.QueryString["EmpId"] != null || Request.QueryString["EmpId"].ToString() != "")
            {
                EmpId = Common.CastAsInt32(Request.QueryString["EmpId"].ToString());
            }
            if (Request.QueryString["Year"] != null || Request.QueryString["Year"].ToString() != "")
            {
                 ShowReport();
            }
    }

    public void ShowReport()
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();
        DataTable dt2 = new DataTable();
        DataTable dt3 = new DataTable();
        DataTable dt4 = new DataTable();
        DataTable dt5 = new DataTable();
        DataTable dt6 = new DataTable();
        DataTable dt7 = new DataTable();
        DataTable dt8 = new DataTable();
        DataTable dt9 = new DataTable();
        DataTable dt10 = new DataTable();
        DataTable dt11 = new DataTable();         

        string SQL = "";

        if (EmpId == 0)
        {
            SQL = "SELECT * FROM vw_TrainingReport ";
        }
        else
        {
            SQL =   "SELECT     TP.TrainingPlanningId, TP.TrainingId, TM.TrainingName, TP.Location, CASE TP.Status WHEN 'E' THEN REPLACE(CONVERT(varchar(11), TP.StartDate1, 106), ' ', '-') + ' ' + TP.StartTime1 + ' Hrs' " +
                    "ELSE REPLACE(CONVERT(varchar(11), TP.StartDate, 106), ' ', '-') + ' ' + TP.StartTime + ' Hrs' END AS StartDate, CASE TP.Status WHEN 'E' THEN REPLACE(CONVERT(varchar(11), TP.EndDate1, 106), ' ', '-') + ' ' + TP.EndTime1 + ' Hrs' " +
                    "ELSE REPLACE(CONVERT(varchar(11), TP.EndDate, 106), ' ', '-') + ' ' + TP.EndTime + ' Hrs' END AS EndDate, TP.StartTime, TP.EndTime, (CASE TP.Status WHEN 'A' THEN 'Planned' WHEN 'E' THEN 'Completed' WHEN 'C' THEN 'Cancelled' ELSE ' ' END) AS Status, TP.CancelledByUser, " +
                    "REPLACE(CONVERT(varchar(11), TP.CancelledOn, 106), ' ', '-') AS CancelledOn, TP.CompletedByUser, REPLACE(CONVERT(varchar(11), TP.CompletedOn, 106), ' ', '-') AS CompletedOn, TP.Location1, TP.StartDate AS StartDate1, REPLACE(CONVERT(varchar(11), TP.EndDate1, 106), ' ', '-') AS EndDate1, TP.StartTime1, TP.EndTime1 " +
                    "FROM         dbo.HR_TrainingPlanning AS TP " +
                    "INNER JOIN HR_TrainingPlanningDetails TPD ON TP.TrainingPlanningId = TPD.TrainingPlanningId " +
                    "INNER JOIN dbo.HR_TrainingMaster AS TM ON TP.TrainingId = TM.TrainingId ";
        }

        for (int i = 1; i <= 12; i++)
        {
            string Where = "";

             if (EmpId == 0)
             {
                Where = "WHERE  (YEAR(StartDate1) = " + Request.QueryString["Year"].ToString() + ") AND (MONTH(StartDate1) = " + i.ToString() + ") ORDER BY StartDate"; 
             }
             else
             {
                 Where = " WHERE (YEAR(StartDate) = " + Request.QueryString["Year"].ToString() + ") AND (MONTH(StartDate) = " + i.ToString() + ") AND (TPD.EmpId = " + EmpId.ToString() + " )  ORDER BY StartDate ";
             }
           
            
            if (i == 1)
            {
                dt = Common.Execute_Procedures_Select_ByQueryCMS(SQL + Where);
                dt.TableName = "vw_TrainingReport";
            }
            else if (i == 2)
            {
                dt1 = Common.Execute_Procedures_Select_ByQueryCMS(SQL + Where);
                dt1.TableName = "vw_TrainingReport1";
            }
            else if (i == 3)
            {
                dt2 = Common.Execute_Procedures_Select_ByQueryCMS(SQL + Where);
                dt2.TableName = "vw_TrainingReport2";
            }
            else if (i == 4)
            {
                dt3 = Common.Execute_Procedures_Select_ByQueryCMS(SQL + Where);
            }
            else if (i == 5)
            {
                dt4 = Common.Execute_Procedures_Select_ByQueryCMS(SQL + Where);
            }
            else if (i == 6)
            {
                dt5 = Common.Execute_Procedures_Select_ByQueryCMS(SQL + Where);
            }
            else if (i == 7)
            {
                dt6 = Common.Execute_Procedures_Select_ByQueryCMS(SQL + Where);
            }
            else if (i == 8)
            {
                dt7 = Common.Execute_Procedures_Select_ByQueryCMS(SQL + Where);
                dt7.TableName = "vw_TrainingReport";
            }
            else if (i == 9)
            {
                dt8 = Common.Execute_Procedures_Select_ByQueryCMS(SQL + Where);
                dt8.TableName = "vw_TrainingReport";
            }
            else if (i == 10)
            {
                dt9 = Common.Execute_Procedures_Select_ByQueryCMS(SQL + Where);
                dt9.TableName = "vw_TrainingReport";
            }
            else if (i == 11)
            {
                dt10 = Common.Execute_Procedures_Select_ByQueryCMS(SQL + Where);
                dt10.TableName = "vw_TrainingReport";
            }
            else 
            {
                dt11 = Common.Execute_Procedures_Select_ByQueryCMS(SQL + Where);
                dt11.TableName = "vw_TrainingReport";
            }
            
        }
        CrystalReportViewer1.ReportSource = rpt;
                
        rpt.Load(Server.MapPath("../Reporting/EMPTrainingCalender.rpt"));


        rpt.Subreports[0].SetDataSource(dt);
        rpt.Subreports[1].SetDataSource(dt1);
        rpt.Subreports[2].SetDataSource(dt2);
        rpt.Subreports[3].SetDataSource(dt3);
        rpt.Subreports[4].SetDataSource(dt4);
        rpt.Subreports[5].SetDataSource(dt5);
        rpt.Subreports[6].SetDataSource(dt6);
        rpt.Subreports[7].SetDataSource(dt7);
        rpt.Subreports[8].SetDataSource(dt8);
        rpt.Subreports[9].SetDataSource(dt9);
        rpt.Subreports[10].SetDataSource(dt10);
        rpt.Subreports[11].SetDataSource(dt11);

        rpt.SetParameterValue("Title", "Jan - " + Request.QueryString["Year"].ToString());
        rpt.SetParameterValue("Title1", "Feb - " + Request.QueryString["Year"].ToString());
        rpt.SetParameterValue("Title2", "Mar - " + Request.QueryString["Year"].ToString());
        rpt.SetParameterValue("Title3", "Apr - " + Request.QueryString["Year"].ToString());
        rpt.SetParameterValue("Title4", "May - " + Request.QueryString["Year"].ToString());
        rpt.SetParameterValue("Title5", "Jun - " + Request.QueryString["Year"].ToString());
        rpt.SetParameterValue("Title6", "Jul - " + Request.QueryString["Year"].ToString());
        rpt.SetParameterValue("Title7", "Aug - " + Request.QueryString["Year"].ToString());
        rpt.SetParameterValue("Title8", "Sep - " + Request.QueryString["Year"].ToString());
        rpt.SetParameterValue("Title9", "Oct - " + Request.QueryString["Year"].ToString());
        rpt.SetParameterValue("Title10", "Nov - " + Request.QueryString["Year"].ToString());
        rpt.SetParameterValue("Title11", "Dec - " + Request.QueryString["Year"].ToString());
        
        
    }

    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
}