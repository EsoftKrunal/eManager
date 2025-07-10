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

public partial class Top4OnBoard : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        //========== Code to check report printing authority
        CrystalReportViewer1.HasPrintButton =Alerts.Check_ReportPrintingAuthority(Convert.ToInt32(Session["loginid"].ToString()),85);
        //==========
        if (!IsPostBack)
        {
            BindFleet();
        }
        ShowData();
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
    protected void ddlFleet_OnSelectedIndexChanged(object sender, EventArgs e)
    {
       
    }
    public void BindFleet()
    {
        ddlFleet.DataSource = Common.Execute_Procedures_Select_ByQueryCMS("select * from DBO.fleetmaster ORDER BY FLEETNAME");
        ddlFleet.DataTextField="fLEETNAME";
        ddlFleet.DataValueField="FLEETID";
        ddlFleet.DataBind();
        ddlFleet.Items.Insert(0, new ListItem(" All ", "0"));
    }
    public void ShowData()
    {
        this.CrystalReportViewer1.Visible = true;
        CrystalReportViewer1.ReportSource = rpt;
        rpt.Load(Server.MapPath("Top4OnBoard.rpt"));
        string sql = "SELECT * FROM DBO.VW_CREW_EXPERIENCE_LESS4_MONTH WHERE VESSELID IN (SELECT V.VESSELID FROM DBO.VESSEL V WHERE ISNULL(FLEETID,0)>0)";
        if (ddlFleet.SelectedIndex > 0)
        {
            sql = "SELECT * FROM DBO.VW_CREW_EXPERIENCE_LESS4_MONTH WHERE VESSELID IN (SELECT V.VESSELID FROM DBO.VESSEL V WHERE FLEETID=" + ddlFleet.SelectedValue + ")";
        }
        
        rpt.SetDataSource(Common.Execute_Procedures_Select_ByQuery(sql));
    }
}
