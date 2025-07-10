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

public partial class Drill_PrintDrillTraining : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();

    public string CurrentVessel
    {
        get { return ViewState["CurrentVessel"].ToString(); }
        set { ViewState["CurrentVessel"] = value; }
    }
    public int DoneId
    {
        get { return Common.CastAsInt32(ViewState["DoneId"]); }
        set { ViewState["DoneId"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CurrentVessel = Session["CurrentShip"].ToString();
            DoneId = Common.CastAsInt32(Request.QueryString["DoneId"]);
        }

        Show_Report();
    }
    private void Show_Report()
    {
        DataSet ds = new DataSet();

        string SQL = "SELECT * FROM [dbo].[vw_DrillTrainings] WHERE VesselCode='" + CurrentVessel + "'";
        DataTable dtList = Common.Execute_Procedures_Select_ByQuery(SQL);
        dtList.TableName = "vw_DrillTrainings";

        SQL = "SELECT * FROM [dbo].[DT_VSL_DrillTrainingsHistory] WHERE VesselCode='" + CurrentVessel + "' AND DoneId=" + DoneId.ToString();
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
        dt.TableName = "DT_VSL_DrillTrainingsHistory";

        SQL = "SELECT CrewNumber,[CrewName] ,[RankName] FROM [dbo].[DT_VSL_DrillTrainingHistoryRanks] WHERE VesselCode='" + CurrentVessel + "' AND DoneId=" + DoneId.ToString() + " AND  [Present] = 1";
        DataTable dtCrew = Common.Execute_Procedures_Select_ByQuery(SQL);
        dtCrew.TableName = "DT_VSL_DrillTrainingHistoryRanks";

        SQL = "SELECT DONEID,[SrNo],[Details],[OnTime] FROM [dbo].[DT_VSL_DrillTrainingHistoryDetails] WHERE [VesselCode] = '" + CurrentVessel + "' AND [DoneId]=" + DoneId;
        DataTable dtDT = Common.Execute_Procedures_Select_ByQuery(SQL);
        dtDT.TableName = "DT_VSL_DrillTrainingHistoryDetails";

        SQL = "SELECT * FROM [dbo].[DT_VSL_DrillTrainingHistoryAttachments] WHERE [VesselCode] = '" + CurrentVessel + "' AND [DoneId]=" + DoneId;
        DataTable dtDTFiles = Common.Execute_Procedures_Select_ByQuery(SQL);
        dtDTFiles.TableName = "DT_VSL_DrillTrainingHistoryAttachments";
        
        ds.Tables.Add(dtList.Copy());
        ds.Tables.Add(dt.Copy());
        ds.Tables.Add(dtCrew.Copy());
        ds.Tables.Add(dtDT.Copy());
        ds.Tables.Add(dtDTFiles.Copy());

        if (ds.Tables[0].Rows.Count > 0)
        {

            this.CrystalReportViewer1.Visible = true;
            CrystalReportViewer1.ReportSource = rpt;
            rpt.Load(Server.MapPath("PrintDrillTraining.rpt"));
            rpt.SetDataSource(ds);
            rpt.Subreports[0].SetDataSource(dtCrew);
        }
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
}