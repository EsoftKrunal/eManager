using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class Reports_SpareMgmtReport : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();

    protected void Page_Load(object sender, EventArgs e)
    {
        // -------------------------- SESSION CHECK ----------------------------------------------
        ProjectCommon.SessionCheck();
        // -------------------------- SESSION CHECK END ----------------------------------------------
        if (!Page.IsPostBack)
        {
            //if (Session["UserType"].ToString() == "S")
            //{
            //    ddlVessels.Items.Insert(0, new ListItem("< SELECT >", "0"));
            //    ddlVessels.Items.Insert(1, new ListItem(Session["CurrentShip"].ToString(), Session["CurrentShip"].ToString()));
            //    ddlVessels.SelectedIndex = 1;
            //    ddlVessels.Visible = false;
            //    tdVessel.Visible = false;
            //}
            //else
            //{
            //    tdVessel.Visible = true;
            //    BindVessels();
            //}
        }
        ShowReport();
    }
    protected void ShowReport()
    {
        string ReportQuery ="";
        //HiddenField hfReportQuery = (HiddenField)Page.PreviousPage.FindControl("hfReportQuery");
        if(Session["hfReportQuery"]!=null)
            ReportQuery = Session["hfReportQuery"].ToString();

        DataTable dtReport = Common.Execute_Procedures_Select_ByQuery(ReportQuery );
        CrystalReportViewer1.ReportSource = rpt;
        rpt.Load(Server.MapPath("SpareListReport.rpt"));
        rpt.SetDataSource(dtReport);
        
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
}
