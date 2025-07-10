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

public partial class CrewRecord_SearchPrint : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();

        //-------------------------------
        if(Session["RP_FilterData"]!=null)
        {
            string[] sql_Filter = Session["RP_FilterData"].ToString().Split('~');
            DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql_Filter[0]);
            DataView dv = dt.DefaultView;
            dv.RowFilter = sql_Filter[1];
            //-----------------------------
            CrystalReportViewer1.ReportSource = rpt;
            rpt.Load(Server.MapPath("SearchPrint.rpt"));
            rpt.SetDataSource(dv.ToTable());
          }  //rpt.SetParameterValue("@HeaderText", header);
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
}
