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

public partial class PrintDrillPlanner : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        string VesselCode = Request.QueryString["VSL"];
        int Year = Common.CastAsInt32(Request.QueryString["Year"]);
        string DT = Request.QueryString["DT"];
        //------
        int FleetId = Common.CastAsInt32(Request.QueryString["Fleet"]);
        int DTId = Common.CastAsInt32(Request.QueryString["DTId"]);


        Show_Report(Year, FleetId, VesselCode, DT, DTId);
    }
    private void Show_Report(int Year, int FleetId, string VesselCode, string DT, int DTId)
    {
            DataTable dt = Common.Execute_Procedures_Select_ByQuery("exec dbo.PRINT_DRILLPLANNER " + FleetId + ",'" + VesselCode + "'," + Year + ",'" + DT + "'," + DTId);
            //DataTable dt = Common.Execute_Procedures_Select_ByQuery(Session["sqlfinal"].ToString());

            foreach (DataRow dr in dt.Rows)
            {
                for (int i = 1; i <= 12; i++)
                {
                    DateTime dtDate = new DateTime(Common.CastAsInt32(Year), i, DateTime.DaysInMonth(Common.CastAsInt32(Year), i));
                    bool Assgined = (dr["MON" + i.ToString()].ToString() == "True");
                    DateTime? dtForDate = null;
                    if (!Convert.IsDBNull(dr["MON" + i.ToString() + "V"]))
                    {
                        dtForDate = Convert.ToDateTime(dr["MON" + i.ToString() + "V"]);
                    }

                    if (Assgined)
                    {
                        if (dtDate <= DateTime.Today)
                        {
                            // red
                            // green
                            dr["Class" + i.ToString()] = (dtForDate == null) ? "error" : "success";
                        }
                        else
                        {  // yellow
                            // green
                            dr["Class" + i.ToString()] = (dtForDate == null) ? "planned" : "success";
                        }
                    }
                else
                {
                    //blue
                    dr["Class" + i.ToString()] = (dtForDate == null) ? "" : "done-already";
                }
            }
            }


            this.CrystalReportViewer1.Visible = true;
            CrystalReportViewer1.ReportSource = rpt;
            rpt.Load(Server.MapPath("PrintDrillPlanner.rpt"));
            rpt.SetDataSource(dt);
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
}