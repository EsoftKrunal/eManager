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


public partial class PrintReport : System.Web.UI.Page
{
    string ReportType = "", VesselName = "", StatusValue = "", FleetID="0";
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        if (Page.Request.QueryString["ReportType"]!=null)
            ReportType = Page.Request.QueryString["ReportType"].ToString();
        if (Page.Request.QueryString["VesselName"] != null)
            VesselName = Page.Request.QueryString["VesselName"].ToString();
        if (Page.Request.QueryString["StatusValue"] != null)
            StatusValue = Page.Request.QueryString["StatusValue"].ToString();
        if (Page.Request.QueryString["FleetID"] != null)
            FleetID = Page.Request.QueryString["FleetID"].ToString();
        
            switch (ReportType)
            {
                case "Topic":
                    ShowTopicReport();
                    return;
                case "TopicCount":
                    ShowTopicCount();
                    return;

                    
            }
        
    }
    private void ShowTopicReport()
    {
        string sql = Session["sTopicsSql"].ToString();
        DataSet dsTopic = Budget.getTable(sql);

        if (dsTopic!=null)
        {
            this.CrystalReportViewer1.Visible = true;
            CrystalReportViewer1.ReportSource = rpt;
            rpt.Load(Server.MapPath("../Reports/VesselTopicsReport.rpt"));
            rpt.SetDataSource(dsTopic.Tables[0]);
            rpt.SetParameterValue("VesselName", VesselName);
            rpt.SetParameterValue("Status", StatusValue);
            
        }
        else
        {
            lblmessage.Text = "No Record Found.";
            this.CrystalReportViewer1.Visible = false;
        }
    }
    private void ShowTopicCount()
    {
        string sql = "select V.VesselName,V.VesselCode  ,V.VesselID,T.CreatedBy "+
               " ,(select  UserID  from  dbo.userlogin U where U.Loginid=T.CreatedBy )CreatedByName   "+
                " ,T.TopicDetails,T.CreatedOn,replace( convert(varchar, T.CreatedOn,106),' ','-')CreatedOnStr     "+ 
                " from  dbo.Vessel V   "+
                " left join [dbo].[vw_GetTopicDataRPT] T on T.VesselCode=V.VesselCode "+
                " where 1=1  and v.VesselStatusid<>2   and FleetID" + ((FleetID == "0") ? "!=0" : "=" + FleetID + "") + "" +
                " order by V.VesselCode  ";

        DataSet dsTopic = Budget.getTable(sql);

        string FleetName = "";
        if (FleetID == "0")
            FleetName = " All ";
        if (FleetID == "1")
            FleetName = " Fleet 1 ";
        if (FleetID == "2")
            FleetName = " Fleet 2 ";
        if (FleetID == "3")
            FleetName = " Fleet 3 ";

        
        if (dsTopic != null)
        {
            this.CrystalReportViewer1.Visible = true;
            CrystalReportViewer1.ReportSource = rpt;
            rpt.Load(Server.MapPath("../Reports/TopicCountsReport.rpt"));
            rpt.SetDataSource(dsTopic.Tables[0]);
            rpt.SetParameterValue("Fleet", FleetName);
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
