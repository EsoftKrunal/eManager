using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class emtm_Emtm_Home1 : System.Web.UI.Page
{
    public static Random r = new Random();
    string FileName = "";
    string[] Colors = { "orange", "#21b9a5", "#03467a", "#8f2be2", "#42bce6", "#0aaf2a", "#abaa10" };
    public int MyOfficeId
    {
        get { return Common.CastAsInt32(ViewState["MyOfficeId"]); }
        set { ViewState["MyOfficeId"] = value; }
    }
    public int MyDeptId
    {
        get { return Common.CastAsInt32(ViewState["MyDeptId"]); }
        set { ViewState["MyDeptId"] = value; }
    }
    public int MyPositionId
    {
        get { return Common.CastAsInt32(ViewState["MyPositionId"]); }
        set { ViewState["MyPositionId"] = value; }
    }
    public int UserId
    {
        get { return Common.CastAsInt32(ViewState["UserId"]); }
        set { ViewState["UserId"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        //string sql = "SELECT *,PER=CASE WHEN TOTAL<>0 THEN CORRECT*100/TOTAL ELSE 0 END FROM VW_KPI_RESULT Where userid = " + Session["loginid"].ToString() + " ORDER BY SRNO";
        //string sql = "exec dbo.GET_MY_KPI " + Session["loginid"].ToString() + ",2017";
        if (!IsPostBack)
        {
            UserId = Common.CastAsInt32(Session["loginid"]);
            string sql = "select * from Hr_PersonalDetails where userid=" + Session["loginid"].ToString();
            DataTable dtSelf = Common.Execute_Procedures_Select_ByQueryCMS(sql);
            if (dtSelf.Rows.Count > 0)
            {
                MyOfficeId = Common.CastAsInt32(dtSelf.Rows[0]["Office"]);
                MyDeptId = Common.CastAsInt32(dtSelf.Rows[0]["Department"]);
                MyPositionId = Common.CastAsInt32(dtSelf.Rows[0]["Position"]);
            }


            sql = "select jr.* from Hr_PersonalDetails e inner join Position p on e.position=p.positionid inner join vesselpositions vp on vp.vpid=p.VesselPositions left join HR_JobResponsibility jr on jr.PositionId=vp.vpid and jr.Qcat=e.PID where e.userid=" + Session["loginid"].ToString() + " ORDER BY JobResponsibility";
            DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
            rptMyKPI.DataSource = dt;
            rptMyKPI.DataBind();

            ddlOffice.DataSource = Common.Execute_Procedures_Select_ByQueryCMS("select * from office order by officename");
            ddlOffice.DataTextField = "OfficeName";
            ddlOffice.DataValueField = "OfficeId";
            ddlOffice.DataBind();
            ddlOffice.Items.Insert(0, new ListItem("Select Office", "0"));
            try
            {
                ddlOffice.SelectedValue = MyOfficeId.ToString();
            }
            catch { }
            binddepartments();
            bindpositions();
            ShowCompanyKPI();
        }

    }
     protected void ShowCompanyKPI()
    {
        string yearstart = new DateTime(DateTime.Today.Year, 1, 1).ToString("dd-MMM-yyyy");
        String sql = "SELECT *,per=(success*100/case when total=0 then null else total end) FROM ( " +
              "SELECT k.EntryId as KPIID,KPINAME,DESCRIPTION," +
                    "(select count(*) from DBO.vessel v where v.vesselstatusid=1 ) as total, " +
                    "(select count(*) from DBO.vessel v where v.vesselstatusid=1  and not exists(select 1 from VW_ALL_ALERS al where al.kpiid=k.EntryId and al.vesselid=v.vesselid and startdate >='" + yearstart + "')) as success, " +
                    "(select count(*) from DBO.vessel v where v.vesselstatusid=1 and exists(select 1 from VW_ALL_ALERS al where al.kpiid=k.EntryId and al.vesselid=v.vesselid and startdate >='" + yearstart + "')) as error " +
                    "FROM DBO.KPI_ENTRY k WHERE AUDIT = 'Y'" +
            ") B where kpiid in (5)  ORDER BY KPINAME ";
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        rptCompKPI.DataSource = dt;
        rptCompKPI.DataBind();

        String sql1 = "SELECT *,per=(success*100/case when total=0 then null else total end) FROM ( " +
             "SELECT k.EntryId as KPIID,KPINAME,DESCRIPTION," +
                   "(select count(*) from VW_ALL_ALERS al where al.kpiid=k.entryid and startdate >='" + yearstart + "') as total, " +
                   "(select count(*) from VW_ALL_ALERS al where al.kpiid=k.entryid and result = 1 and startdate >='" + yearstart + "') as success, " +
                   "(select count(*) from VW_ALL_ALERS al where al.kpiid=k.entryid and result = 0 and startdate >='" + yearstart + "') as error " +
                   "FROM DBO.KPI_ENTRY k WHERE AUDIT = 'Y'" +
           ") B where kpiid not in (5) ORDER BY KPINAME ";
        DataTable dt1 = Common.Execute_Procedures_Select_ByQueryCMS(sql1);
        rptCompKPI_ByVessel.DataSource = dt1;
        rptCompKPI_ByVessel.DataBind();
    }
    protected void ddlOffice_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        binddepartments();
        ddlPosition.SelectedIndex = 0;
    }
    protected void ddlDepartments_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        bindEmps();
    }
    protected void ddlPosition_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        bindEmps();
    }

    //protected void btnPost_Click(object sender, EventArgs e)
    //{
    //    int Userid = Common.CastAsInt32(hfduserid.Value);
    //    ShowTeamKPI(Userid);
    //}
    //protected void ShowTeamKPI(int Userid)
    //{
    //    string sql = "select firstname +' ' + middlename + '' + familyname as empname,userid,positionname from DBO.Hr_PersonalDetails e inner join DBO.office o on e.office = o.officeid inner join DBO.position p on e.position = p.positionid where userid = " + Userid;
    //    DataTable dtuser = Common.Execute_Procedures_Select_ByQueryCMS(sql);
    //    if (dtuser.Rows.Count > 0)
    //    {
    //        lblUserName.Text = dtuser.Rows[0]["empname"].ToString();
    //        lblPositionName.Text = "(" + dtuser.Rows[0]["positionname"].ToString() + " ) ";
    //        rptEmps.Visible = false;
    //        dvcrewstat.Visible = true;
    //        //---------------------
    //        sql = "select e.userid,jr.* from Hr_PersonalDetails e inner join Position p on e.position=p.positionid inner join vesselpositions vp on vp.vpid=p.VesselPositions left join HR_JobResponsibility jr on jr.PositionId=vp.vpid and jr.Qcat=e.PID where e.userid=" + Userid + " ORDER BY JobResponsibility";
    //        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
    //        rptTeamKPI.DataSource = dt;
    //        rptTeamKPI.DataBind();
    //    }

    //}

    protected void btnOpenMyKPI_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "fas dfsaf","window.open('UserKpi.aspx?Key=" + UserId + "','');", true);
    }
    protected void lnkBack_OnClick(object sender, EventArgs e)
    {
        rptEmps.Visible = true;
        //dvcrewstat.Visible = false;
    }

    protected void binddepartments()
    {
        ddlDepartments.DataSource = Common.Execute_Procedures_Select_ByQueryCMS("select * from emtm_Department where officeid=" + ddlOffice.SelectedValue + " order by deptname");
        ddlDepartments.DataTextField = "DeptName";
        ddlDepartments.DataValueField = "DeptId";
        ddlDepartments.DataBind();
        ddlDepartments.Items.Insert(0, new ListItem("Select Department", "0"));
        try
        {
            ddlDepartments.SelectedValue = MyDeptId.ToString();
        }
        catch { }
        bindEmps();
    }
    protected void bindpositions()
    {
        ddlPosition.DataSource = Common.Execute_Procedures_Select_ByQueryCMS("select* from DBO.VesselPositions order by PositionName");
        ddlPosition.DataTextField = "PositionName";
        ddlPosition.DataValueField = "VPId";
        ddlPosition.DataBind();
        ddlPosition.Items.Insert(0, new ListItem("All Positions", "0"));
        try
        {
            ddlPosition.SelectedValue = MyPositionId.ToString();
        }
        catch { }
        bindEmps();
    }
    
    protected void bindEmps()
    {
        string sql = "select userid,firstname + ' ' + middlename + ' '  + familyname as EmpName,PositionName from Hr_PersonalDetails inner join position p on Hr_PersonalDetails.position=p.PositionId where department=" + ddlDepartments.SelectedValue + " and office=" + ddlOffice.SelectedValue + ((ddlPosition.SelectedIndex>0)?" and VesselPositions=" + ddlPosition.SelectedValue:"")  + " and drc is null order by firstname";
        rptEmps.DataSource = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        rptEmps.DataBind();
    }
    public string getColor(Object srno)
    {
        return Colors[Common.CastAsInt32(srno) % Colors.Length];
    }

    public DataTable BindKPI(object userid, object k)
    {
	int kk=Common.CastAsInt32(k);
        string sql = "exec dbo.GET_MY_KPI " + userid + "," + kk.ToString() + ",2017";
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        return dt;
    }
    
}
