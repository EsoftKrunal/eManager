using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class emtm_UserKPI : System.Web.UI.Page
{
    public static Random r = new Random();
    string FileName = "";
    string[] Colors = { "orange", "#21b9a5", "#03467a", "#8f2be2", "#42bce6", "#0aaf2a", "#abaa10" };
    
    public int UserId
    {
        get { return Common.CastAsInt32(ViewState["UserId"]); }
        set { ViewState["UserId"] = value; }
    }
    public int PeapLevel
    {
        get { return Common.CastAsInt32(ViewState["PeapLevel"]); }
        set { ViewState["PeapLevel"] = value; }
    }
    public int Position
    {
        get { return Common.CastAsInt32(ViewState["Position"]); }
        set { ViewState["Position"] = value; }
    }
    public Dictionary<int, decimal> KPISums=new Dictionary<int, decimal>(); 
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        if (!IsPostBack)
        {
            UserId = Common.CastAsInt32(Request.QueryString["key"]);
            string sql = "select PID,VesselPositions,EMPCODE,FIRSTNAME + ' ' + MiddleName + ' ' + FamilyName AS EMPNAME, p.PositionName,vp.PositionName as PosGroup, OfficeName, d.DeptName from " +
            "Hr_PersonalDetails e " +
            "inner join Position p on e.Position = p.PositionId " +
            "inner join Office o on o.OfficeId = p.OfficeId " +
            "left join HR_Department d on d.DeptId = e.Department " +
            "left join VesselPositions vp on vp.VPId = p.VesselPositions " +
            "where userid = " + UserId;

            DataTable dtSelf = Common.Execute_Procedures_Select_ByQueryCMS(sql);
            if (dtSelf.Rows.Count > 0)
            {
                PeapLevel = Common.CastAsInt32(dtSelf.Rows[0]["pid"]);
                Position = Common.CastAsInt32(dtSelf.Rows[0]["VesselPositions"]);

                lblEmpName.Text = dtSelf.Rows[0]["EMPNAME"].ToString() + " [ " + dtSelf.Rows[0]["EMPCODE"].ToString() + " ]";
                lblOffice.Text = dtSelf.Rows[0]["OfficeName"].ToString();
                lblDepartment.Text = dtSelf.Rows[0]["DeptName"].ToString();
                lblPosGroup.Text = dtSelf.Rows[0]["PositionName"].ToString() + " [ " + dtSelf.Rows[0]["PosGroup"].ToString() + " ]";
            }
            ShowKRACat();
        }

    }
    
    protected void lnkJS_Click(object sender, EventArgs e)
    {
        int _JSID = Common.CastAsInt32(((LinkButton)sender).CommandArgument);
        ShowKPI(_JSID);
    }
    public DataTable GetKRA(object kraid)
    {
        string yearstart = new DateTime(DateTime.Today.Year, 1, 1).ToString("dd-MMM-yyyy");
        String sql = "exec dbo.GET_MY_JobResponsibility " + UserId + "," + DateTime.Today.Year.ToString() + "," + kraid.ToString();
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        KPISums.Add(Common.CastAsInt32(kraid), Common.CastAsDecimal(dt.Compute("sum(Scale)", "")));
        return dt;
    }
    protected void ShowKRACat()
    {
        String sql = "select* from HR_KRA order by kra_groupname";
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        rptkra.DataSource = dt;
        rptkra.DataBind();
        decimal tot = 0;
        foreach(decimal d in KPISums.Values)
        {
            tot += d;
        }
        lblTotalScore.Text = " Total Scale : " + tot.ToString();
    }
    
    protected void ShowKPI(int JSId)
    {
        DataTable dtj = Common.Execute_Procedures_Select_ByQueryCMS("SELECT * FROM HR_JobResponsibility WHERE JSID=" + JSId.ToString());
        if(dtj.Rows.Count>0)
        {
            lblJRName.Text = dtj.Rows[0]["JobResponsibility"].ToString();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "dsfa", "document.getElementById('krahead').focus()", true);
            
        }
        string yearstart = new DateTime(DateTime.Today.Year, 1, 1).ToString("dd-MMM-yyyy");        
        string sql = "exec dbo.GET_MY_KPI " + UserId + "," + JSId + "," + DateTime.Today.Year;
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        rptKPI.DataSource = dt;
        rptKPI.DataBind();
    }
    public string getJSSum(object kraid)
    {
        return KPISums[Common.CastAsInt32(kraid)].ToString();
    }
    

}
