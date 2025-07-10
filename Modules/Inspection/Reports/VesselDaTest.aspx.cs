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

public partial class Reports_VesselDaTest : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        string query = "select vdt3.Testid,Vesselname as VesselCode,PortName,TestDate,NextRangeS,NextRangeE,TestedBy,Remarks,Cost,FileName " +
                    "from " +
                    "( " +
                    "select (select top 1 testid from vesseldatest vdt2 where a.vesselid=vdt2.vesselid and a.testdate=vdt2.testdate ) as TestId " +
                    "from " +
                    "( " +
                    "select vesselid,max(testdate) as testdate " +
                    "from vesseldatest vdta " +
                    "group by vesselid " +
                    ") a " +
                    ") b inner join vesseldatest vdt3 on b.testid=vdt3.testid " +
                    "inner join dbo.vessel on vessel.vesselid=vdt3.vesselid " +
                    "inner join dbo.port on port.portid=vdt3.portid";

        //string query = "select vdt3.Testid,Vesselname as VesselCode,PortName,TestDate,NextRangeS,NextRangeE,TestedBy,Remarks,Cost,FileName " +
        //               "from  " +
        //               "(  " +
        //               "     select vesselid,(select top 1 testid from vesseldatest vdt2 where a.vesselid=vdt2.vesselid and a.testdate=vdt2.testdate ) as TestId  " +
        //               "     from  " +
        //               "     (  " +
        //               "         select vesselid,(select max(testdate) from vesseldatest where vesseldatest.vesselid=vessel.vesselid) as testdate  " +
        //               "         from dbo.vessel " +
        //               "     ) a  " +
        //               ") b  " +
        //               "left join vesseldatest vdt3 on b.testid=vdt3.testid  " +
        //               "left join dbo.vessel on vessel.vesselid=b.vesselid  " +
        //               "left join dbo.port on port.portid=vdt3.portid ";

        DataTable dt = Common.Execute_Procedures_Select_ByQuery(query); 
        this.CrystalReportViewer1.Visible = true;
        CrystalReportViewer1.ReportSource = rpt;
        rpt.Load(Server.MapPath("RPT_VesselDaTest.rpt"));
        rpt.SetDataSource(dt);
        rpt.SetParameterValue("@Header", "D&A Test Status");
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
}
