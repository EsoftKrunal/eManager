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

public partial class Reports_PolicyDetails : System.Web.UI.Page
{
    int VesselId;
    string strFollowUpCat = "", strFollowUpFDate = "", strFollowUpTDate = "", strDueInDays = "", strStatus = "", strCritical = "", strResponsiblity = "", strOverDue = "";
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        this.lblmessage.Text = "";
        try
        {
            
        }
        catch { }
        Show_Report();
    }
    private void Show_Report()
    {
        string sql = "SELECT PolicyId " +
                      " ,PolicyNo " +
                      " ,(select VesselCode from Vessel V where V.VesselID=PM.VesselID)VesselID " +
                      " , (select LDT from Vessel  V where V.VesselID=PM.VesselID)LDT " +
                      " , (select YearBuilt from Vessel  V where V.VesselID=PM.VesselID)YB" +
                      " ,(select ShortName from dbo.IRM_Groups G where G.GroupID=PM.GroupID)GroupID " +
                      " ,IssuedDt " +
                      " ,(Case when PM.PaymentByMtm=1 then 'Yes' else '' end)PaymentByMtm " +
                      " ,Rate " +
                      " ,(select UWName from dbo.IRM_UWMaster UW where UW.UWID=PM.UWId)UWId " +
                      " ,InsuranceAmountUSD " +
                      " ,RateUSD " +
                      " ,TotalPremiumUSD " +
                      " FROM dbo.IRM_PolicyMaster PM " +
                    "LEFT JOIN dbo.IRM_Groups GP ON GP.GroupId = PM.GroupId " +
                    "LEFT JOIN dbo.IRM_UWMaster UWM ON UWM.UWId = PM.UWId  ";
        string WhereCond = Session["sWhereCond"].ToString();

        sql = sql + WhereCond + " order by PM.ExpiryDt Desc ";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
        if (dt.Rows.Count > 0)
        {
            this.CrystalReportViewer1.Visible = true;

            CrystalReportViewer1.ReportSource = rpt;
            rpt.Load(Server.MapPath("PolicyDetails.rpt"));
            rpt.SetDataSource(dt);
            //rpt.SetParameterValue("@Header", dt.Rows[0]["VesselName"].ToString() + " - FollowUp List");
            
        }
        else
        {
            lblmessage.Text = "No Record Found.";
            this.CrystalReportViewer1.Visible = false;
        }
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
}
