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

public partial class Reports_Office_BreakdownDefectReport1 : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        // -------------------------- SESSION CHECK ----------------------------------------------
        ProjectCommon.SessionCheck();
        // -------------------------- SESSION CHECK END ----------------------------------------------

        if (Session["UserType"] == "O")
        {
            if (Request.QueryString["DN"] != null)
            {
                string strDefectdetails = "SELECT * FROM Vw_BreakDownReport WHERE BreakDownNo = '" + Request.QueryString["DN"].ToString() + "' ";

                DataTable dtDefects = Common.Execute_Procedures_Select_ByQuery(strDefectdetails);

                CrystalReportViewer1.ReportSource = rpt;
                rpt.Load(Server.MapPath("Office_BreakdownDefectReport1.rpt"));

                rpt.SetDataSource(dtDefects);
                rpt.SetParameterValue("@Header", "Break Down");
                rpt.SetParameterValue("VesselName", GetVesselNameOnOffice(Request.QueryString["DN"].ToString().Substring(0,3)));
            }
        }
        else
        {
            if (Request.QueryString["CC"] != null)
            {
                string strSQL = "";
                DataTable dtDefectsDetails;
                if (Request.QueryString["CC"].ToString() != "undefined")
                {
                    strSQL = "Select CM.ComponentCode,CM.ComponentName,DM.* FROM VSL_BreakDownMaster DM INNER JOIN ComponentMaster CM ON CM.ComponentId = DM.ComponentId " +
                             "WHERE DM.VesselCode = '" + Session["CurrentShip"].ToString() + "' AND CM.ComponentCode = '" + Request.QueryString["CC"].ToString() + "'";
                    dtDefectsDetails = Common.Execute_Procedures_Select_ByQuery(strSQL);

                    CrystalReportViewer1.ReportSource = rpt;
                    rpt.Load(Server.MapPath("BreakdownReport.rpt"));
                }
                else
                {
                    strSQL = "Select CM.ComponentCode,CM.ComponentName,DM.* FROM VSL_BreakDownMaster DM INNER JOIN ComponentMaster CM ON CM.ComponentId = DM.ComponentId " +
                             "WHERE DM.VesselCode = '" + Session["CurrentShip"].ToString() + "' ";
                    dtDefectsDetails = Common.Execute_Procedures_Select_ByQuery(strSQL);

                    CrystalReportViewer1.ReportSource = rpt;
                    rpt.Load(Server.MapPath("BreakdownReport_AllComponents.rpt"));
                }

                rpt.SetDataSource(dtDefectsDetails);
                rpt.SetParameterValue("@Header", "Defect Report");
            }
            else if (Request.QueryString["DN"] != null)
            {
                string strDefectdetails = "SELECT * FROM Vw_BreakDownReport WHERE BreakDownNo = '" + Request.QueryString["DN"].ToString() + "' ";

                DataTable dtDefects = Common.Execute_Procedures_Select_ByQuery(strDefectdetails);

                CrystalReportViewer1.ReportSource = rpt;
                rpt.Load(Server.MapPath("Office_BreakdownDefectReport1.rpt"));

                rpt.SetDataSource(dtDefects);
                rpt.SetParameterValue("@Header", "Break Down Report");
                rpt.SetParameterValue("VesselName", GetVesselNameOnShip(Session["CurrentShip"].ToString()));
            }
        }
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }

    public string GetVesselNameOnShip(string VessCode)
    {
        string sql = "select ShipName from Settings where ShipCode='" + VessCode + "'";
        DataTable dtVessName = Common.Execute_Procedures_Select_ByQuery(sql);
        if(dtVessName .Rows.Count>0)
            return dtVessName .Rows[0][0].ToString();
        else
            return "";
    }
    public string GetVesselNameOnOffice(string VessCode)
    {
        string sql = "select VesselName from Vessel where VesselCode='" + VessCode + "'";
        DataTable dtVessName = Common.Execute_Procedures_Select_ByQuery(sql);
        if (dtVessName.Rows.Count > 0)
            return dtVessName.Rows[0][0].ToString();
        else
            return "";
    }

}
