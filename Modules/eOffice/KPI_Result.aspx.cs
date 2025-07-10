

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class emtm_kpi_result : System.Web.UI.Page
{
    public static Random r = new Random();
    string FileName = "";
    string[] Colors = { "orange", "#21b9a5", "#03467a", "#8f2be2", "#42bce6", "#0aaf2a", "#abaa10" };
    //public int MyOfficeId
    //{
    //    get { return Common.CastAsInt32(ViewState["MyOfficeId"]); }
    //    set { ViewState["MyOfficeId"] = value; }
    //}
    //public int MyDeptId
    //{
    //    get { return Common.CastAsInt32(ViewState["MyDeptId"]); }
    //    set { ViewState["MyDeptId"] = value; }
    //}
    public int KpiUserId
    {
        get { return Common.CastAsInt32(ViewState["KpiUserId"]); }
        set { ViewState["KpiUserId"] = value; }
    }
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
            KpiUserId = Common.CastAsInt32(Request.QueryString["key1"]);

            string sql = "select firstname +' ' + middlename + '' + familyname as empname,userid,positionname,deptname from DBO.Hr_PersonalDetails e inner join DBO.office o on e.office = o.officeid inner join emtm_department d on e.department=d.deptid inner join DBO.position p on e.position = p.positionid where userid = " + KpiUserId;
            DataTable dtuser = Common.Execute_Procedures_Select_ByQueryCMS(sql);
            if (dtuser.Rows.Count > 0)
            {
                lblUserName.Text = dtuser.Rows[0]["empname"].ToString();
                lblPositionName.Text = dtuser.Rows[0]["positionname"].ToString();
                lblDepartment.Text = dtuser.Rows[0]["deptname"].ToString();
                //---------------------
            }
            sql = "select * from dbo.kpi_entry where entryid = " + KpiId;
            DataTable dtkpi = Common.Execute_Procedures_Select_ByQueryCMS(sql);
            if (dtkpi.Rows.Count > 0)
            {
                lblKPIName.Text = dtkpi.Rows[0]["KPIName"].ToString();
            }

            string yearstart = new DateTime(2017, 1, 1).ToString("dd-MMM-yyyy");

            sql = "select a.vesselid,a.vpid,v.vesselname,e.userid,positionname,a.EffDate,a.LastDate, " +
              "(select count(*) from VW_ALL_ALERS al where al.vesselid = a.vesselid and al.kpiid=" + KpiId + " and status='Done'  and((startdate between (case when a.EffDate<'" + yearstart + "' then '" + yearstart + "' else a.EffDate end) and lastdate) or(startdate >= (case when a.EffDate<'" + yearstart + "' then '" + yearstart + "' else a.EffDate end) and lastdate is null))) as total, " +
              "(select count(*) from VW_ALL_ALERS al where al.vesselid = a.vesselid and al.kpiid=" + KpiId + " and result = 1 and ((startdate between (case when a.EffDate<'" + yearstart + "' then '" + yearstart + "' else a.EffDate end) and lastdate) or(startdate >= (case when a.EffDate<'" + yearstart + "' then '" + yearstart + "' else a.EffDate end) and lastdate is null))) as success, " +
              "(select count(*) from VW_ALL_ALERS al where al.vesselid = a.vesselid and al.kpiid=" + KpiId + " and result = 0 and ((startdate between (case when a.EffDate<'" + yearstart + "' then '" + yearstart + "' else a.EffDate end) and lastdate) or(startdate >= (case when a.EffDate<'" + yearstart + "' then '" + yearstart + "' else a.EffDate end) and lastdate is null))) as error " +
              " from Hr_PersonalDetails e left join vw_active_vessel_assignments a on e.userid = a.userid left join vessel v on a.vesselid=v.vesselid left join vesselpositions p on a.vpid = p.vpid where ( (year(case when a.EffDate<'" + yearstart + "' then '" + yearstart + "' else a.EffDate end) = 2017 and a.LastDate >='" + yearstart + "' ) or LastDate is null) and e.userid = " + KpiUserId.ToString() + " order by v.vesselname" ;

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
            "from vw_active_vessel_assignments a " +
            "inner join VW_ALL_ALERS al on a.vesselid = al.vesselid " +
            "inner join dbo.MortorMaster m on al.RefKey = convert(varchar, mid) " +
            "and((al.startdate between (case when a.EffDate<'" + yearstart + "' then '" + yearstart + "' else a.EffDate end) and a.lastdate) or(al.startdate >= (case when a.EffDate<'" + yearstart + "' then '" + yearstart + "' else a.EffDate end) and a.lastdate is null)) " +

            "and kpiid = " + KpiId + " and a.vesselid=" + vesselid + " and vPId=" + posid + " and EffDate = '" + startdate + "'";
        }
        if (KpiId == 24 )
        {
            sql = "select ROW_NUMBER() OVER(order by al.startdate) AS SNo, 'Technical Inspection' as ReportNo, al.startdate, al.DueDate, al.DoneDate,case when al.Result = 1 then '+Ve' when al.Result = 0 then '-Ve' else null end as Result,al.Status " +
            "from vw_active_vessel_assignments a " +
            "inner join VW_ALL_ALERS al on a.vesselid = al.vesselid " +
            "inner join dbo.t_inspectiondue m on al.RefKey = convert(varchar, id) " +
            "and((al.startdate between (case when a.EffDate<'" + yearstart + "' then '" + yearstart + "' else a.EffDate end) and a.lastdate) or(al.startdate >= (case when a.EffDate<'" + yearstart + "' then '" + yearstart + "' else a.EffDate end) and a.lastdate is null)) " +
            "and kpiid = " + KpiId + " and a.vesselid=" + vesselid + " and vPId=" + posid + " and EffDate = '" + startdate + "'";
        }
	if (KpiId == 44)
        {
            sql = "select ROW_NUMBER() OVER(order by al.startdate) AS SNo, mi.Code as ReportNo, al.startdate, al.DueDate, al.DoneDate,case when al.Result = 1 then '+Ve' when al.Result = 0 then '-Ve' else null end as Result,al.Status " +
            "from vw_active_vessel_assignments a " +
            "inner join VW_ALL_ALERS al on a.vesselid = al.vesselid " +
            "inner join dbo.t_inspectiondue m on al.RefKey = convert(varchar, id) " +
            "inner join dbo.m_inspection mi on mi.id = m.inspectionid " +
            "and((al.startdate between (case when a.EffDate<'" + yearstart + "' then '" + yearstart + "' else a.EffDate end) and a.lastdate) or(al.startdate >= (case when a.EffDate<'" + yearstart + "' then '" + yearstart + "' else a.EffDate end) and a.lastdate is null)) " +
            "and kpiid = " + KpiId + " and a.vesselid=" + vesselid + " and vPId=" + posid + " and EffDate = '" + startdate + "'";
        }
	if (KpiId == 45)
        {
            sql = "select ROW_NUMBER() OVER(order by al.startdate) AS SNo, mi.Code as ReportNo, al.startdate, al.DueDate, al.DoneDate,case when al.Result = 1 then '+Ve' when al.Result = 0 then '-Ve' else null end as Result,al.Status " +
            "from vw_active_vessel_assignments a " +
            "inner join VW_ALL_ALERS al on a.vesselid = al.vesselid " +
            "inner join dbo.t_inspectiondue m on al.RefKey = convert(varchar, id) " +
            "inner join dbo.m_inspection mi on mi.id = m.inspectionid " +
            "and((al.startdate between (case when a.EffDate<'" + yearstart + "' then '" + yearstart + "' else a.EffDate end) and a.lastdate) or(al.startdate >= (case when a.EffDate<'" + yearstart + "' then '" + yearstart + "' else a.EffDate end) and a.lastdate is null)) " +
            "and kpiid = " + KpiId + " and a.vesselid=" + vesselid + " and vPId=" + posid + " and EffDate = '" + startdate + "'";
        }
	if (KpiId == 33 || KpiId == 34)
        {
            sql = "select ROW_NUMBER() OVER(order by al.startdate) AS SNo,  CONVERT(varchar,al.startdate,106) as ReportNo, al.startdate, al.DueDate, al.DoneDate,case when al.Result = 1 then '+Ve' when al.Result = 0 then '-Ve' else null end as Result,al.Status " +
            "from vw_active_vessel_assignments a " +
            "inner join VW_ALL_ALERS al on a.vesselid = al.vesselid " +
            "and((al.startdate between (case when a.EffDate<'" + yearstart + "' then '" + yearstart + "' else a.EffDate end) and a.lastdate) or(al.startdate >= (case when a.EffDate<'" + yearstart + "' then '" + yearstart + "' else a.EffDate end) and a.lastdate is null)) " +
            "and kpiid = " + KpiId + " and a.vesselid=" + vesselid + " and vPId=" + posid + " and EffDate = '" + startdate + "'";
        }
	if (KpiId == 14)
        {
            sql = "select ROW_NUMBER() OVER(order by al.startdate) AS SNo, CONVERT(varchar,al.startdate,106) as ReportNo, al.startdate, al.DueDate, al.DoneDate,case when al.Result = 1 then '+Ve' when al.Result = 0 then '-Ve' else null end as Result,al.Status " +
            "from vw_active_vessel_assignments a " +
            "inner join VW_ALL_ALERS al on a.vesselid = al.vesselid " +
            "and((al.startdate between (case when a.EffDate<'" + yearstart + "' then '" + yearstart + "' else a.EffDate end) and a.lastdate) or(al.startdate >= (case when a.EffDate<'" + yearstart + "' then '" + yearstart + "' else a.EffDate end) and a.lastdate is null)) " +
            "and kpiid = " + KpiId + " and a.vesselid=" + vesselid + " and vPId=" + posid + " and EffDate = '" + startdate + "'";
        }
	if (KpiId == 21)
        {
            sql = "select ROW_NUMBER() OVER(order by al.startdate) AS SNo, CONVERT(varchar,al.startdate,106) as ReportNo, al.startdate, al.DueDate, al.DoneDate,case when al.Result = 1 then '+Ve' when al.Result = 0 then '-Ve' else null end as Result,al.Status " +
            "from vw_active_vessel_assignments a " +
            "inner join VW_ALL_ALERS al on a.vesselid = al.vesselid " +
            "and((al.startdate between (case when a.EffDate<'" + yearstart + "' then '" + yearstart + "' else a.EffDate end) and a.lastdate) or(al.startdate >= (case when a.EffDate<'" + yearstart + "' then '" + yearstart + "' else a.EffDate end) and a.lastdate is null)) " +
            "and kpiid = " + KpiId + " and a.vesselid=" + vesselid + " and vPId=" + posid + " and EffDate = '" + startdate + "'";
        }
	if (KpiId == 16 || KpiId == 20 || KpiId == 28 || KpiId == 19 || KpiId == 48)
        {
            sql = "select ROW_NUMBER() OVER(order by al.startdate) AS SNo, REFKEY as ReportNo, al.startdate, al.DueDate, al.DoneDate,case when al.Result = 1 then '+Ve' when al.Result = 0 then '-Ve' else null end as Result,al.Status " +
            "from vw_active_vessel_assignments a " +
            "inner join VW_ALL_ALERS al on a.vesselid = al.vesselid " +
            "and((al.startdate between (case when a.EffDate<'" + yearstart + "' then '" + yearstart + "' else a.EffDate end) and a.lastdate) or(al.startdate >= (case when a.EffDate<'" + yearstart + "' then '" + yearstart + "' else a.EffDate end) and a.lastdate is null)) " +
            "and kpiid = " + KpiId + " and a.vesselid=" + vesselid + " and vPId=" + posid + " and EffDate = '" + startdate + "'";
        }
	if (KpiId == 2 || KpiId == 3 || KpiId == 39 || KpiId == 8 || KpiId == 9 || KpiId == 10)
        {
            sql = "select ROW_NUMBER() OVER(order by al.startdate) AS SNo, InspectionNo as ReportNo, al.startdate, al.DueDate, al.DoneDate,case when al.Result = 1 then '+Ve' when al.Result = 0 then '-Ve' else null end as Result,al.Status " +
            "from vw_active_vessel_assignments a " +
            "inner join VW_ALL_ALERS al on a.vesselid = al.vesselid " +
            "inner join dbo.t_inspectiondue m on al.RefKey = convert(varchar, id) " +
            "and((al.startdate between (case when a.EffDate<'" + yearstart + "' then '" + yearstart + "' else a.EffDate end) and a.lastdate) or(al.startdate >= (case when a.EffDate<'" + yearstart + "' then '" + yearstart + "' else a.EffDate end) and a.lastdate is null)) " +
            "and kpiid = " + KpiId + " and a.vesselid=" + vesselid + " and vPId=" + posid + " and EffDate = '" + startdate + "'";
        }
	if (KpiId == 18)
        {
            sql = "select ROW_NUMBER() OVER(order by al.startdate) AS SNo, DocketNo as ReportNo, al.startdate, al.DueDate, al.DoneDate,case when al.Result = 1 then '+Ve' when al.Result = 0 then '-Ve' else null end as Result,al.Status " +
            "from vw_active_vessel_assignments a " +
            "inner join VW_ALL_ALERS al on a.vesselid = al.vesselid " +
            "inner join dbo.DD_DOCKETMASTER m on al.RefKey = convert(varchar, docketid) " +
            "and((al.startdate between (case when a.EffDate<'" + yearstart + "' then '" + yearstart + "' else a.EffDate end) and a.lastdate) or(al.startdate >= (case when a.EffDate<'" + yearstart + "' then '" + yearstart + "' else a.EffDate end) and a.lastdate is null)) " +
            "and kpiid = " + KpiId + " and a.vesselid=" + vesselid + " and vPId=" + posid + " and EffDate = '" + startdate + "'";
        }


        dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        tptDetails.DataSource = dt;
        tptDetails.DataBind();
    }
}
