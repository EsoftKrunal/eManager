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

public partial class Report_CrewPlanning : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();

    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        //========== Code to check report printing authority
        CrystalReportViewer1.HasPrintButton = Alerts.Check_ReportPrintingAuthority(Convert.ToInt32(Session["loginid"].ToString()), 99);
        CrystalReportViewer2.HasPrintButton = Alerts.Check_ReportPrintingAuthority(Convert.ToInt32(Session["loginid"].ToString()), 99);
        //==========
      
        ShowData();
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
    public void ShowData()
    {
        this.CrystalReportViewer1.Visible = true;
        CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
        CrystalReportViewer1.ReportSource = rpt;
        rpt.Load(Server.MapPath("CrewPlanning.rpt"));
        string Sql=Session["SqlPrintCrewPlanning"].ToString();
	int mf=Common.CastAsInt32(Request.QueryString["mf"]);
	string finalsql="";

        DataTable dtROff = Common.Execute_Procedures_Select_ByQueryCMS("select RecruitingOfficeId from userlogin where loginid=" + mf.ToString());
        string AllowedOffices = "";
        if (dtROff.Rows.Count > 0)
            AllowedOffices = dtROff.Rows[0][0].ToString();

        string whereclause = " where ( Off_RecruitmentOfficeId in (-1," + AllowedOffices + ") OR ManningUserId=" + mf.ToString() + " OR ON_RecruitmentOfficeId in (-1," + AllowedOffices + ") ) ";



	if(mf<=0)
		finalsql=Session["SqlPrintCrewPlanning"].ToString() + Session["SqlPrintCrewPlanning_where"].ToString() + " ORDER BY PLANRANKLEVEL";
	else
        	finalsql=Session["SqlPrintCrewPlanning"].ToString() + whereclause + " ORDER BY PLANRANKLEVEL";

//	finalsql=Session["SqlPrintCrewPlanning"].ToString() + " where ManningUserId=" + mf.ToString() + " ORDER BY PLANRANKLEVEL";




//Response.Write(finalsql);

        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(finalsql);        
        dt.TableName = "Vw_PrintCrewPlanning";    
           
        rpt.SetDataSource(dt);
        rpt.SetParameterValue("Heading", Session["Heading"].ToString());
    }
}
