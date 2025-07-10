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

public partial class Reports_ObservationReporting_Crystal : System.Web.UI.Page
{
    string strInspGrpId = "";
    string strVessel = "";
    string CrewId= "";
    string InspectorName = "";
    string Chapter = "";
    string SelMode="";
    string CrewNumber = "";
    string Rank="";
    string CrewName="";
    string FD="", TD = "";
    string OpenSearch = "";

    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        try
        {
            strInspGrpId = Page.Request.QueryString["InspID"].ToString();
            strVessel = Page.Request.QueryString["VesselID"].ToString();
            CrewId = Page.Request.QueryString["CrewId"].ToString();
            InspectorName = Page.Request.QueryString["Inspector"].ToString();
            Chapter = Page.Request.QueryString["Chapter"].ToString();
            SelMode = Page.Request.QueryString["SelMode"].ToString();
            FD = Page.Request.QueryString["FD"].ToString();
            TD = Page.Request.QueryString["TD"].ToString();
            OpenSearch = Page.Request.QueryString["Open"].ToString();
            
            DataTable dt = Budget.getTable("select crewid,isnull(firstname,'') + ' ' +isnull(lastname,'') as crewname from dbo.crewpersonaldetails where crewnumber='" + CrewId + "'").Tables[0];
            if(dt.Rows.Count >0)
            {
                CrewNumber = CrewId;
                CrewName= dt.Rows[0][1].ToString();  
                CrewId = dt.Rows[0][0].ToString();  
            }
            else
            {
                CrewId="0";
            }
            dt = Budget.getTable("select rankcode from dbo.rank where rank.rankid in(select currentrankid from dbo.crewpersonaldetails where CrewId=" + CrewId + ")").Tables[0];
            
            if(dt.Rows.Count >0)
            {
                Rank = dt.Rows[0][0].ToString();  
            }
            else
            {
                Rank = "";
            }
        }
        catch { }
        Show_Report();
    }
    private void Show_Report()
    {
        DataTable dt;
        if (SelMode == "C") // By Crew No
        {
            dt = Operator_Reporting.SelectObservationReportingDetails(strInspGrpId, strVessel, "", CrewId, "",FD,TD );
        }
        else if (SelMode == "I") // By Inspector
        {
            dt = Operator_Reporting.SelectObservationReportingDetails(strInspGrpId, strVessel, InspectorName, "0","",FD,TD);
        }
        else if (SelMode == "H") // By Chapter no
        {
            dt = Operator_Reporting.SelectObservationReportingDetails(strInspGrpId, strVessel, "", "0", Chapter, FD, TD);
        }
        else
        {
            dt = Operator_Reporting.SelectObservationReportingDetails(strInspGrpId, strVessel, "", "0", "", FD, TD);
            DataView dv= dt.DefaultView;
            dv.RowFilter = "deficiency like '%"  + OpenSearch + "%'";
            dt=dv.ToTable();  
        }
        //-------------------
        if (dt.Rows.Count > 0)
        {
            this.CrystalReportViewer1.Visible = true;

            CrystalReportViewer1.ReportSource = rpt;
            rpt.Load(Server.MapPath("RPT_ObservationDefiency.rpt"));
            rpt.SetDataSource(dt);
            if (SelMode == "C") // By Crew No
            {
                rpt.SetParameterValue("@Header", "Crew Member : " + CrewName  + " / " + Rank + " / " + CrewNumber);
            }
            else if (SelMode == "I") // By Inspector
            {
                rpt.SetParameterValue("@Header", "");
            }
            else
            {
                rpt.SetParameterValue("@Header", " Chapter Name : " + Chapter);
            }
            rpt.SetParameterValue("@RowsCount", dt.Rows.Count.ToString());
            
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
