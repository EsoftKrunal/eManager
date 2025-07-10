using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Configuration;

public partial class Report_OwnerAccountBalance : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        ProjectCommon.SessionCheck();
        ShowReport();
    }
    public void ShowReport()
    {
        try
        {
            string Qry = "",Company="", FromDate = "", ToDate = "", TransactionType="", AccountName="";
            Qry = Session["sqlOwnerAccountBalance"].ToString().Split('~')[0];
            Company = Session["sqlOwnerAccountBalance"].ToString().Split('~')[1];
            FromDate= Session["sqlOwnerAccountBalance"].ToString().Split('~')[2];
            ToDate = Session["sqlOwnerAccountBalance"].ToString().Split('~')[3];
            TransactionType = Session["sqlOwnerAccountBalance"].ToString().Split('~')[4];
            AccountName = Session["sqlOwnerAccountBalance"].ToString().Split('~')[5];

            decimal OPBal = Common.CastAsInt32(Session["sqlOwnerAccountBalance"].ToString().Split('~')[6]);
            decimal CloseBal = Common.CastAsInt32(Session["sqlOwnerAccountBalance"].ToString().Split('~')[7]);

            DataTable dt = Common.Execute_Procedures_Select_ByQuery(Qry);

            CrystalReportViewer1.ReportSource = rpt;
            rpt.Load(Server.MapPath("~/Modules/OPEX/Report/OwnerAccountBalance.rpt"));
            rpt.SetDataSource(dt);
            rpt.SetParameterValue("Owner", Company);
            rpt.SetParameterValue("OpeningBalance", OPBal);
            rpt.SetParameterValue("ClosingBalance", CloseBal);
            rpt.SetParameterValue("FromDate", FromDate);
            rpt.SetParameterValue("ToDate", ToDate);
            rpt.SetParameterValue("TransactionType", TransactionType);
            rpt.SetParameterValue("AccountName", AccountName);


        }
        catch (System.Exception ex)
        {
           
        }

    }
    
    protected void Page_UnLoad(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
}


