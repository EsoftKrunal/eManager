using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.Caching;

public partial class POReportCrystal : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();

    #region Properties ****************************************************
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        //---------------------------------------
        ProjectCommon.SessionCheck();
        //---------------------------------------
        //ShowReport1();
        //ShowReport();
        ShowReportBySP();
    }
    protected void btnPrint_OnClick(object sender, EventArgs e)
    {
        
    }
    //public void ShowReport1()
    //{

    //    DateTime FromDate, toDate;
    //    FromDate = Convert.ToDateTime(Page.Request.QueryString["FromMonth"].ToString() + "/1/" + Page.Request.QueryString["FromYear"].ToString());
    //    toDate = Convert.ToDateTime(Page.Request.QueryString["ToMonth"].ToString() + "/30/" + Page.Request.QueryString["ToYear"].ToString()); // $$$$ 30 should not be fixed

    //    Common.Set_Procedures("sp_New_getPoReportData");
    //    Common.Set_ParameterLength(8);
    //    Common.Set_Parameters(
    //                new MyParameter("@cocode",Page.Request.QueryString["Company"].ToString() ),
    //                new MyParameter("@ShipCode", Page.Request.QueryString["VesselID"].ToString()),
    //                new MyParameter("@RequestType",Page.Request.QueryString["RequestType"].ToString()),
    //                new MyParameter("@SupplierID",Page.Request.QueryString["SupplierID"].ToString()),
    //                new MyParameter("@FromAcc",FromDate ),
    //                new MyParameter("@ToAcc",toDate)
    //               // new MyParameter("@glPer",),
    //                //new MyParameter("@glYear",)
    //        );

    //    DataTable DtAccount=Common.Execute_Procedures_Select();
    //     if (DtAccount != null)
    //    {
    //        CrystalReportViewer1.Visible = true;
    //        CrystalReportViewer1.ReportSource = rpt;
    //        //rpt.Load(Server.MapPath("~/Report/PrintRFQ.rpt"));
    //        if (Common.CastAsInt32(Page.Request.QueryString["SearchBy"]) == 1)
    //        {
    //            rpt.Load(Server.MapPath("~/Report/POReportVendor.rpt"));
    //        }
    //        else if (Common.CastAsInt32(Page.Request.QueryString["SearchBy"]) == 2)
    //        {
    //            rpt.Load(Server.MapPath("~/Report/POReportAccountCode.rpt"));
    //        }
    //        else
    //        {
    //            rpt.Load(Server.MapPath("~/Report/POReport.rpt"));
    //        }
            
    //        DtAccount.TableName = "vw_getPoReportData";
    //        rpt.SetDataSource(DtAccount);
    //    }
    //}
    public void ShowReport()
    {
        string sql = "";
        if (Common.CastAsInt32(Page.Request.QueryString["RequestType"]) == 0)
        {
            if(Common.CastAsInt32(Page.Request.QueryString["BreakDown"])==0)
                sql = "select * from  vw_getPoReportData where Company='" + Page.Request.QueryString["Company"].ToString() + "' and shipID in(" + Page.Request.QueryString["VesselID"].ToString() + ") ";
            else
                sql = "select * from  vw_getPoReportData where Company='" + Page.Request.QueryString["Company"].ToString() + "' and shipID in(" + Page.Request.QueryString["VesselID"].ToString() + ")  And BidID in(SELECT BidID FROM Add_tblSMDPOMasterBid1)";
        }
        else
        {
            sql = "select * from  vw_getPoReportData where Company='" + Page.Request.QueryString["Company"].ToString() + "' and shipID in(" + Page.Request.QueryString["VesselID"].ToString() + ") and PRTYPECode=" + Common.CastAsInt32(Page.Request.QueryString["RequestType"]) + " ";
        }
        string whereclause = "";
        if (Common.CastAsInt32( Page.Request.QueryString["SearchBy"] )== 0) 
        {
            DateTime FromDate, toDate;
            FromDate = Convert.ToDateTime(Page.Request.QueryString["FromMonth"].ToString() + "/1/" + Page.Request.QueryString["FromYear"].ToString());
            toDate = Convert.ToDateTime(Page.Request.QueryString["ToMonth"].ToString() + "/30/" + Page.Request.QueryString["ToYear"].ToString()); // $$$$ 30 should not be fixed
            whereclause = " and ApprovalDate >=convert(datetime,'" + FromDate.ToShortDateString() + "') and ApprovalDate <=convert(datetime,'" + toDate.ToShortDateString() + "')";

        }
        if (Common.CastAsInt32(Page.Request.QueryString["SearchBy"]) == 1) 
        {
            if (Common.CastAsInt32(Page.Request.QueryString["SupplierID"]) != 0)
            {
                whereclause = " and SupplierID=" + Common.CastAsInt32(Page.Request.QueryString["SupplierID"]);
            }
        }
        if (Common.CastAsInt32(Page.Request.QueryString["SearchBy"]) == 2) 
        {
            if (Page.Request.QueryString["Acc"].ToString() != "")
            {

                whereclause = " and AccountNumber>=" + Page.Request.QueryString["Acc"].ToString() + "";
                if (Page.Request.QueryString["Acc1"].ToString() != "")
                {
                    whereclause = whereclause + " and AccountNumber<=" + Page.Request.QueryString["Acc1"].ToString() + "";
                }
            }
        }

        sql = sql + whereclause;
        DataTable DtAccount = Common.Execute_Procedures_Select_ByQuery(sql);
        if (DtAccount != null)
        {
            CrystalReportViewer1.Visible = true;
            CrystalReportViewer1.ReportSource = rpt;
            //rpt.Load(Server.MapPath("~/Report/PrintRFQ.rpt"));
            if (Common.CastAsInt32(Page.Request.QueryString["SearchBy"]) == 1)
            {
                rpt.Load(Server.MapPath("~/Modules/Purchase/Report/POReportVendor.rpt"));
            }
            else if (Common.CastAsInt32(Page.Request.QueryString["SearchBy"]) == 2)
            {
                rpt.Load(Server.MapPath("~/Modules/Purchase/Report/POReportAccountCode.rpt"));
            }
            else
            {
                rpt.Load(Server.MapPath("~/Modules/Purchase/Report/POReport.rpt"));
            }
            
            DtAccount.TableName = "vw_getPoReportData";
            rpt.SetDataSource(DtAccount);
        }
    }
    public void ShowReportBySP()
    {
        //Common.Set_Procedures("SP_NEW_GetPOReportData");
        //Common.Set_ParameterLength(7);
        //Common.Set_Parameters(
        //    new MyParameter("@cocode",Page.Request.QueryString["Company"].ToString()),
        //    new MyParameter("@FROMDATE",Convert.ToDateTime(Page.Request.QueryString["FromMonth"].ToString() + "/1/" + Page.Request.QueryString["FromYear"].ToString())),
        //    new MyParameter("@TODATE",Convert.ToDateTime(Page.Request.QueryString["ToMonth"].ToString() + "/30/" + Page.Request.QueryString["ToYear"].ToString())),
        //    new MyParameter("@SHIPCODE",Page.Request.QueryString["VesselID"].ToString()),
        //    new MyParameter("@REQUESTTYPE",Page.Request.QueryString["RequestType"].ToString()),
        //    new MyParameter("@VENDOR",Page.Request.QueryString["SupplierID"].ToString()),
        //    new MyParameter("@ACCOUNTCODE", Page.Request.QueryString["Acc"].ToString())
        //    );
        string filter = "";
        DateTime FROMDATE, TODATE;
        FROMDATE = DateTime.Parse("1/" + ProjectCommon.GetMonthName(Page.Request.QueryString["FromMonth"].ToString()) + "/" + Page.Request.QueryString["FromYear"].ToString());
        TODATE = DateTime.Parse("1/" + ProjectCommon.GetMonthName(Page.Request.QueryString["ToMonth"].ToString()) + "/" + Page.Request.QueryString["ToYear"].ToString());
        TODATE = TODATE.AddMonths(1);
        TODATE = TODATE.AddDays(-1);   
         
        filter = FROMDATE.ToString("MMM yyyy") + " - " + TODATE.ToString("MMM yyyy");
        
        string sql = "dbo.SP_NEW_GetPOReportData '" + Page.Request.QueryString["Company"].ToString() + "'"+
            ",'" + FROMDATE.ToString("dd/MMM/yyyy") + "'" +
            ",'" + TODATE.ToString("dd/MMM/yyyy") + "'" +
            ",'" + Page.Request.QueryString["VesselID"].ToString() +"'"+
            "," + Page.Request.QueryString["RequestType"].ToString() +
            "," + Page.Request.QueryString["SupplierID"].ToString() +
            "," + Page.Request.QueryString["Acc"].ToString() + "" +
            "," + Page.Request.QueryString["Acc1"].ToString() + ""+
            "," + Page.Request.QueryString["BreakDown"].ToString() + "";

        DataTable DtAccount = Common.Execute_Procedures_Select_ByQuery(sql);
            if (DtAccount != null)
            {
                CrystalReportViewer1.Visible = true;
                CrystalReportViewer1.ReportSource = rpt;
                //rpt.Load(Server.MapPath("~/Report/PrintRFQ.rpt"));
                if (Common.CastAsInt32(Page.Request.QueryString["SearchBy"]) == 1)
                {
                    rpt.Load(Server.MapPath("~/Modules/Purchase/Report/POReportVendor.rpt"));
                }
                else if (Common.CastAsInt32(Page.Request.QueryString["SearchBy"]) == 2)
                {
                    rpt.Load(Server.MapPath("~/Modules/Purchase/Report/POReportAccountCode.rpt"));
                }
                else
                {
                    rpt.Load(Server.MapPath("~/Modules/Purchase/Report/POReport.rpt"));
                }
                DtAccount.TableName = "vw_getPoReportData";
                rpt.SetDataSource(DtAccount);
                rpt.SetParameterValue("VSLName", ("" + Page.Request.QueryString["VSLName"].ToString()));
                rpt.SetParameterValue("Filter", filter);
            }
    }

    #region PageEvents
    protected void Page_UnLoad(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
    #endregion
}

