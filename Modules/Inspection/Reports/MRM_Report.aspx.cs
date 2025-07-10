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

public partial class Reports_MRM_Report : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    public int DocketId
    {
        get { return Common.CastAsInt32(ViewState["DocketId"]); }
        set { ViewState["DocketId"] = value; }
    }
    public int DocketYear
    {
        get { return Common.CastAsInt32(ViewState["DocketYear"]); }
        set { ViewState["DocketYear"] = value; }
    }
    public int DocketQtr
    {
        get { return Common.CastAsInt32(ViewState["DocketQtr"]); }
        set { ViewState["DocketQtr"] = value; }
    }
    public int OfficeId
    {
        get { return Common.CastAsInt32(ViewState["OfficeId"]); }
        set { ViewState["OfficeId"] = value; }
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        DocketYear = Common.CastAsInt32(Request.QueryString["DocketYear"]);
        DocketQtr = Common.CastAsInt32(Request.QueryString["DocketQtr"]);
        OfficeId = Common.CastAsInt32(Request.QueryString["OfficeId"]);

        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT DOCKETID FROM MRM_DocketMaster WHERE DOCKETYEAR=" + DocketYear.ToString() + " AND OfficeId=" + OfficeId.ToString() + " AND DocketQuarter=" + DocketQtr.ToString());
        if (dt.Rows.Count > 0)
        {
            DocketId = Common.CastAsInt32(dt.Rows[0][0]);
            Show_Report();
        }
    }
    protected void Show_Report()
    {
        this.CrystalReportViewer1.Visible = true;
        CrystalReportViewer1.ReportSource = rpt;
        rpt.Load(Server.MapPath("MRM_Report.rpt"));

        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM vw_MRM_DocketMaster WHERE DOCKETID=" + DocketId.ToString());
        DataTable dt1 = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM MRM_AGENDAMASTER WHERE AGENDAYEAR=" + DocketYear.ToString() + " AND Q" + DocketQtr.ToString() + "=1");
        DataSet ds = new DataSet();
        dt1.TableName = "Result1";
        ds.Tables.Add(dt.Copy());
        ds.Tables.Add(dt1.Copy());

        ds.Tables[0].TableName = "vw_MRM_DocketMaster";
        ds.Tables[1].TableName = "MRM_AGENDAMASTER";
        rpt.SetDataSource(ds);

        DataTable dt_s1 = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM vw_MRM_DocketAttendeeDetails WHERE DOCKETID=" + DocketId.ToString());
        dt_s1.TableName = "vw_MRM_DocketAttendeeDetails";
        rpt.Subreports[0].SetDataSource(dt_s1);


        string sql = "SELECT DAC.DOCKETID,AM.AGENDAID,AM.AGENDANAME,DAC.COMMENTTYPE,DAC.COMMENT,UL.FIRSTNAME + ' ' + UL.LASTNAME AS COMMENTBYUSER,DAC.COMMENTON,TCLDATE,Responsibility,CLOSED=CASE WHEN ISNULL(CLOSED,0)=1 THEN 'YES' ELSE 'NO' END,UL1.FIRSTNAME + ' ' + UL1.LASTNAME AS CLOSEDBYUSER,DAC.CLOSEDON,REMARKS " +
                    "FROM MRM_AGENDAMASTER AM  " +
                    "INNER JOIN dbo.MRM_DocketAgendaComments DAC ON AM.AGENDAID=DAC.AGENDAID AND DOCKETID=" + DocketId.ToString() + " AND DAC.COMMENTTYPE='C' " +
                    "LEFT JOIN DBO.USERLOGIN UL ON UL.LOGINID=DAC.COMMENTBY " +
                    "LEFT JOIN DBO.USERLOGIN UL1 ON UL1.LOGINID=DAC.CLOSEDBY " +
                    "WHERE AGENDAYEAR=" + DocketYear.ToString() + " AND Q" + DocketQtr.ToString() + "=1 " +
                    "ORDER BY DAC.COMMENTON ";

        DataTable dt_s2 = Common.Execute_Procedures_Select_ByQuery(sql);
        dt_s2.TableName = "vw_MRM_DocketCommentsFollowUps";
        rpt.Subreports[1].SetDataSource(dt_s2);


        sql = "SELECT DAC.DOCKETID,AM.AGENDAID,AM.AGENDANAME,DAC.COMMENTTYPE,DAC.COMMENT,UL.FIRSTNAME + ' ' + UL.LASTNAME AS COMMENTBYUSER,DAC.COMMENTON,TCLDATE,Responsibility,CLOSED=CASE WHEN ISNULL(CLOSED,0)=1 THEN 'YES' ELSE 'NO' END,UL1.FIRSTNAME + ' ' + UL1.LASTNAME AS CLOSEDBYUSER,DAC.CLOSEDON,REMARKS " +
                    "FROM MRM_AGENDAMASTER AM  " +
                    "INNER JOIN dbo.MRM_DocketAgendaComments DAC ON AM.AGENDAID=DAC.AGENDAID AND DOCKETID=" + DocketId.ToString() + " AND DAC.COMMENTTYPE='F' " +
                    "LEFT JOIN DBO.USERLOGIN UL ON UL.LOGINID=DAC.COMMENTBY " +
                    "LEFT JOIN DBO.USERLOGIN UL1 ON UL1.LOGINID=DAC.CLOSEDBY " +
                    "WHERE AGENDAYEAR=" + DocketYear.ToString() + " AND Q" + DocketQtr.ToString() + "=1 " +
                    "ORDER BY DAC.COMMENTON ";

        DataTable dt_s3 = Common.Execute_Procedures_Select_ByQuery(sql);
        dt_s3.TableName = "vw_MRM_DocketCommentsFollowUps";
        rpt.Subreports[2].SetDataSource(dt_s3);
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
}
