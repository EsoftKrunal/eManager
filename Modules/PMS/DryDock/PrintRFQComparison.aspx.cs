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
using System.IO;
using Ionic.Zip;

public partial class PrintRFQComparison : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();

    protected void Page_Load(object sender, EventArgs e)
    {
        // -------------------------- SESSION CHECK ----------------------------------------------
        ProjectCommon.SessionCheck();
        // -------------------------- SESSION CHECK END ----------------------------------------------
        ShowReport();
    }
    protected void ShowReport()
    {
        int Sup = Common.CastAsInt32(Request.QueryString["Sup"]);
        int RFQId = Common.CastAsInt32(Request.QueryString["RFQId"]);
        int level = Common.CastAsInt32(Request.QueryString["level"]);
        if (RFQId > 0)
        {
            
            string sql="";
            if(Sup>0)
                sql = "SELECT * FROM DBO.VW_PRINT_RFQ_SUP WHERE RFQID=" + RFQId.ToString();
            else
                sql = "SELECT * FROM DBO.VW_PRINT_RFQ WHERE RFQID=" + RFQId.ToString();

            if (Request.QueryString["CostType"].ToString().Trim() != "")
            {
                sql += " And CostCategory='" + Request.QueryString["CostType"] + "'";
            }
            if (Request.QueryString["CATID"].ToString().Trim() != "0")
            {
                sql += " And CATID=" + Request.QueryString["CATID"] + "";
            }
            if (Request.QueryString["DOCKETJOBID"].ToString().Trim() != "0")
            {
                sql += " And DOCKETJOBID=" + Request.QueryString["DOCKETJOBID"] + "";
            }
            decimal discount = 0;
            DataTable dtdisc = Common.Execute_Procedures_Select_ByQuery("SELECT YardDiscount FROM DBO.DD_Docket_RFQ_Master WHERE RFQID = " + RFQId.ToString());
            if (dtdisc.Rows.Count > 0)
                discount = Common.CastAsDecimal(dtdisc.Rows[0][0]);

            sql += " order by catcode,jobcode,subjobcode";
            DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
            CrystalReportViewer1.ReportSource = rpt;
            rpt.Load(Server.MapPath("PrintRfq" + Request.QueryString["CostType"] + ".rpt"));
            rpt.SetDataSource(dt);
            rpt.SetParameterValue("CostType", Request.QueryString["CostType"]);
            rpt.SetParameterValue("ReportLevel", Request.QueryString["ReportLevel"]);

            if (Request.QueryString["CostType"].ToString().Trim() == "Y")
            {
                rpt.SetParameterValue("FinalYardDiscount", discount);
            }
            else if (Request.QueryString["CostType"].ToString().Trim() == "")
            {
                rpt.SetParameterValue("FinalYardDiscount", discount);
                decimal oc = Common.CastAsDecimal(dt.Compute("sum(netamount_usd)", "CostCategory<>'Y'"));
                rpt.SetParameterValue("OwnersTotal", oc);
                decimal yc = Common.CastAsDecimal(dt.Compute("sum(netamount_usd)", "CostCategory='Y'"));
                rpt.SetParameterValue("YardTotal",yc);
            }
        }
        else
        {
            int Compare = Common.CastAsInt32(Request.QueryString["Compare"]);
            string ModeName = (Request.QueryString["Mode"].ToString().Trim() == "Y") ? "Shipyard Supply Costs" : ((Request.QueryString["Mode"].ToString().Trim() == "N") ? "Owner’s Supply Shipyard Costs" : "Total Shipyard Cost");
            if (Compare > 0)
            {
                string sql="";
                if(Sup>0)
                    sql = "EXEC DD_RFQ_SHOWCOMPARISIONREPORT_SUP " + Request.QueryString["Param"] + ",'" + Request.QueryString["Mode"] + "'";
                else
                    sql="EXEC DD_RFQ_SHOWCOMPARISIONREPORT " + Request.QueryString["Param"] + ",'" + Request.QueryString["Mode"] +"'";

                DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
                CrystalReportViewer1.ReportSource = rpt;

                if (Sup > 0)
                    if (Request.QueryString["Mode"].ToString().Trim() == "Y")
                        rpt.Load(Server.MapPath("PrintRFQComparisonY_SUP.rpt"));
                    else
                        rpt.Load(Server.MapPath("PrintRFQComparisonN_SUP.rpt"));
                else
                    if (Request.QueryString["Mode"].ToString().Trim() == "N")
                    rpt.Load(Server.MapPath("PrintRFQComparisonN.rpt"));
                else
                    rpt.Load(Server.MapPath("PrintRFQComparisonY.rpt"));


                rpt.SetDataSource(dt);
                rpt.SetParameterValue("ReportType", ModeName);
                rpt.SetParameterValue("@Level",level);
            }
        }

       
        
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
}
