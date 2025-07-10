

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class emtm_kpi_result_company : System.Web.UI.Page
{
    public static Random r = new Random();
    string FileName = "";
    string[] Colors = { "orange", "#21b9a5", "#03467a", "#8f2be2", "#42bce6", "#0aaf2a", "#abaa10" };
    
    public int KpiId
    {
        get { return Common.CastAsInt32(ViewState["KpiId"]); }
        set { ViewState["KpiId"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        if (!IsPostBack)
        {
            KpiId = Common.CastAsInt32(Request.QueryString["key"]);            
            string sql = "select * from dbo.kpi_entry where entryid = " + KpiId;
            DataTable dtkpi = Common.Execute_Procedures_Select_ByQueryCMS(sql);
            if (dtkpi.Rows.Count > 0)
            {
                lblKPIName.Text = dtkpi.Rows[0]["KPIName"].ToString();
            }

            string yearstart = new DateTime(2017, 1, 1).ToString("dd-MMM-yyyy");

            sql = "select v.vesselid,v.vesselname,'" + yearstart + "' as EffDate,getdate() as LastDate, " +
              "(select count(*) from VW_ALL_ALERS al where al.vesselid = v.vesselid and al.kpiid=" + KpiId + " and status='Done' and (startdate between '" + yearstart + "' and getdate())) as total, " +
              "(select count(*) from VW_ALL_ALERS al where al.vesselid = v.vesselid and al.kpiid=" + KpiId + " and result = 1 and (startdate between '" + yearstart + "' and getdate())) as success, " +
              "(select count(*) from VW_ALL_ALERS al where al.vesselid = v.vesselid and al.kpiid=" + KpiId + " and result = 0 and (startdate between '" + yearstart + "' and getdate())) as error " +
              " from vessel v where vesselstatusid=1  order by vesselname";

//Response.Write(sql);
//Response.End();


            DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
            rptkpi.DataSource = dt;
            rptkpi.DataBind();

            lblSumreq.Text = dt.Compute("SUM(total)","").ToString();
            lblSumsuccess.Text = dt.Compute("SUM(success)", "").ToString();
            lblSumerror.Text = dt.Compute("SUM(error)", "").ToString();
        }

    }
    
    protected void lnkVessel_Click(object sender,EventArgs e)
    {
        LinkButton l = ((LinkButton)sender);
        string vesselid = l.Attributes["vesselid"];
        string startdate = l.CssClass;
        string posid = l.Attributes["posid"];

        string yearstart = new DateTime(2017, 1, 1).ToString("dd-MMM-yyyy");

        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("select vesselname from vessel where vesselid=" + vesselid);
        lblVesselName.Text = dt.Rows[0][0].ToString();
        //-------------
        string sql="";
        if (KpiId == 35)
        {
            sql = "select ROW_NUMBER() OVER(order by al.startdate) AS SNo, m.ReportNo, al.startdate, al.DueDate, al.DoneDate,case when al.Result = 1 then '+Ve' when al.Result = 0 then '-Ve' else null end as Result,al.Status " +
            "from VW_ALL_ALERS al " +
            "inner join dbo.MortorMaster m on al.RefKey = convert(varchar, mid) " +
            "where kpiid = " + KpiId + " and al.vesselid=" + vesselid + " and al.StartDate between '" + yearstart + "' and getdate()";
        }
        if (KpiId == 24 )
        {
            sql = "select ROW_NUMBER() OVER(order by al.startdate) AS SNo, 'Technical Inspection' as ReportNo, al.startdate, al.DueDate, al.DoneDate,case when al.Result = 1 then '+Ve' when al.Result = 0 then '-Ve' else null end as Result,al.Status " +
            "from VW_ALL_ALERS al " +
            "inner join dbo.t_inspectiondue m on al.RefKey = convert(varchar, id) " +
            "where kpiid = " + KpiId + " and al.vesselid=" + vesselid + " and al.StartDate between '" + yearstart + "' and getdate()";
        }
	if (KpiId == 44)
        {
            sql = "select ROW_NUMBER() OVER(order by al.startdate) AS SNo, mi.Code as ReportNo, al.startdate, al.DueDate, al.DoneDate,case when al.Result = 1 then '+Ve' when al.Result = 0 then '-Ve' else null end as Result,al.Status " +
            "from VW_ALL_ALERS al " +
            "inner join dbo.t_inspectiondue m on al.RefKey = convert(varchar, id) " +
            "inner join dbo.m_inspection mi on mi.id = m.inspectionid " +
            "where kpiid = " + KpiId + " and al.vesselid=" + vesselid + " and al.StartDate between '" + yearstart + "' and getdate()";
        }
	if (KpiId == 45)
        {
            sql = "select ROW_NUMBER() OVER(order by al.startdate) AS SNo, mi.Code as ReportNo, al.startdate, al.DueDate, al.DoneDate,case when al.Result = 1 then '+Ve' when al.Result = 0 then '-Ve' else null end as Result,al.Status " +
            "from VW_ALL_ALERS al " +
            "inner join dbo.t_inspectiondue m on al.RefKey = convert(varchar, id) " +
            "inner join dbo.m_inspection mi on mi.id = m.inspectionid " +
            "where kpiid = " + KpiId + " and al.vesselid=" + vesselid + " and al.StartDate between '" + yearstart + "' and getdate()";
        }
	if (KpiId == 33 || KpiId == 34)
        {
            sql = "select ROW_NUMBER() OVER(order by al.startdate) AS SNo,  CONVERT(varchar,al.startdate,106) as ReportNo, al.startdate, al.DueDate, al.DoneDate,case when al.Result = 1 then '+Ve' when al.Result = 0 then '-Ve' else null end as Result,al.Status " +
            "from VW_ALL_ALERS al " +
            "where kpiid = " + KpiId + " and al.vesselid=" + vesselid + " and al.StartDate between '" + yearstart + "' and getdate()";
        }
	if (KpiId == 14)
        {
            sql = "select ROW_NUMBER() OVER(order by al.startdate) AS SNo, CONVERT(varchar,al.startdate,106) as ReportNo, al.startdate, al.DueDate, al.DoneDate,case when al.Result = 1 then '+Ve' when al.Result = 0 then '-Ve' else null end as Result,al.Status " +
            "from VW_ALL_ALERS al " +
            "where kpiid = " + KpiId + " and al.vesselid=" + vesselid + " and al.StartDate between '" + yearstart + "' and getdate()";
        }
	if (KpiId == 21)
        {
            sql = "select ROW_NUMBER() OVER(order by al.startdate) AS SNo, CONVERT(varchar,al.startdate,106) as ReportNo, al.startdate, al.DueDate, al.DoneDate,case when al.Result = 1 then '+Ve' when al.Result = 0 then '-Ve' else null end as Result,al.Status " +
            "from VW_ALL_ALERS al " +
            "where kpiid = " + KpiId + " and a.vesselid=" + vesselid + " and al.StartDate between '" + yearstart + "' and getdate()";
        }
	if (KpiId == 16 || KpiId == 20 || KpiId == 28 || KpiId == 19 || KpiId == 48 || KpiId == 40 || KpiId == 49 || KpiId == 50)
        {
            sql = "select ROW_NUMBER() OVER(order by al.startdate) AS SNo, REFKEY as ReportNo, al.startdate, al.DueDate, al.DoneDate,case when al.Result = 1 then '+Ve' when al.Result = 0 then '-Ve' else null end as Result,al.Status " +
            "from VW_ALL_ALERS al " +
            " where kpiid = " + KpiId + " and al.vesselid=" + vesselid + " and al.StartDate between '" + yearstart + "' and getdate()";
        }
	if (KpiId == 2 || KpiId == 3 || KpiId == 39 || KpiId == 8 || KpiId == 9)
        {
            sql = "select ROW_NUMBER() OVER(order by al.startdate) AS SNo, InspectionNo as ReportNo, al.startdate, al.DueDate, al.DoneDate,case when al.Result = 1 then '+Ve' when al.Result = 0 then '-Ve' else null end as Result,al.Status " +
            "from VW_ALL_ALERS al " +
            "inner join dbo.t_inspectiondue m on al.RefKey = convert(varchar, id) " +
            " where kpiid = " + KpiId + " and al.vesselid=" + vesselid + " and al.StartDate between '" + yearstart + "' and getdate()";
        }
	if (KpiId == 18)
        {
            sql = "select ROW_NUMBER() OVER(order by al.startdate) AS SNo, DocketNo as ReportNo, al.startdate, al.DueDate, al.DoneDate,case when al.Result = 1 then '+Ve' when al.Result = 0 then '-Ve' else null end as Result,al.Status " +
            "from VW_ALL_ALERS al " +
            "inner join dbo.DD_DOCKETMASTER m on al.RefKey = convert(varchar, docketid) " +
            " where kpiid = " + KpiId + " and al.vesselid=" + vesselid + " and al.StartDate between '" + yearstart + "' and getdate()";
        }
	if (KpiId == 5)
        {
            sql = "select ROW_NUMBER() OVER(order by al.startdate) AS SNo, ( cO_NUMBER + ' - ' + CO_ISSUEDFROM )as ReportNo, al.startdate, al.DueDate, al.DoneDate,case when al.Result = 1 then '+Ve' when al.Result = 0 then '-Ve' else null end as Result,al.Status " +
            "from VW_ALL_ALERS al " +
            "inner join DBO.FR_COC c on al.RefKey = convert(varchar, co_id) and co_vesselid=al.vesselid " +
            " where kpiid = " + KpiId + " and al.vesselid=" + vesselid + " and al.StartDate between '" + yearstart + "' and getdate()";
        }

        dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        tptDetails.DataSource = dt;
        tptDetails.DataBind();
    }
}
