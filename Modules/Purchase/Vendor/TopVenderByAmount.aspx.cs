using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Xml;


public partial class TopVenderByAmount : System.Web.UI.Page
{
    public int Type
    {
        set { ViewState["Type"] = value; }
        get { return Common.CastAsInt32(ViewState["Type"]); }
    }
    public int Fleet
    {
        set { ViewState["Fleet"] = value; }
        get { return Common.CastAsInt32(ViewState["Fleet"]); }
    }
    public string VesselCode
    {
        set { ViewState["_VesselCode"] = value; }
        get { return Convert.ToString(ViewState["_VesselCode"]); }
    }
    public int F_M
    {
        set { ViewState["F_M"] = value; }
        get { return Common.CastAsInt32(ViewState["F_M"]); }
    }
    public int F_Y
    {
        set { ViewState["F_Y"] = value; }
        get { return Common.CastAsInt32(ViewState["F_Y"]); }
    }
    public int T_M
    {
        set { ViewState["T_M"] = value; }
        get { return Common.CastAsInt32(ViewState["T_M"]); }
    }
    public int T_Y
    {
        set { ViewState["T_Y"] = value; }
        get { return Common.CastAsInt32(ViewState["T_Y"]); }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //---------------------------------------
        ProjectCommon.SessionCheck();
        //---------------------------------------

        Type = Common.CastAsInt32(Page.Request.QueryString["Type"]);
        Fleet = Common.CastAsInt32(Page.Request.QueryString["Fleet"]);
        VesselCode = Convert.ToString(Page.Request.QueryString["Vessel"]);

        F_M = Common.CastAsInt32(Page.Request.QueryString["F_M"]);
        F_Y = Common.CastAsInt32(Page.Request.QueryString["F_Y"]);

        T_M = Common.CastAsInt32(Page.Request.QueryString["T_M"]);
        T_Y = Common.CastAsInt32(Page.Request.QueryString["T_Y"]);

        BindTop5VenderByAmount();
        if (!Page.IsPostBack)
        {
            ShowHeader();
        }
    }
    protected void btnPost_Click(object sender, EventArgs e)
    {
        string WhereClause = "";
        DateTime FromDate, ToDate;
        FromDate = new DateTime(F_Y, F_M, 1);
        ToDate = new DateTime(T_Y, T_M, 1).AddMonths(1);
        string sql = "SELECT ROW_NUMBER() OVER(ORDER BY BidSMDLevel1ApprovalDate) AS SNO,bid.BIDID,SHIPID,BIDPONUM,BidSMDLevel1ApprovalDate,InvoiceNo,BidInvoiceDate,(select comment from [dbo].tblBudgetVActualComments where CommBidID=bid.bidid) as ApproveComments ,convert(numeric(18,2),(ESTSHIPPINGUSD + (SELECT SUM(UsdPoTotal) FROM dbo.tblSMDPODETAILbid bids where bids.bidid=bid.bidid))) as TRANSUSD " +
                    "FROM dbo.tblSMDPOMasterbid bid " +
                    "inner join dbo.tblSMDPOMaster po on po.poid = bid.poid " +
                    " inner join dbo.Vessel V on V.VesselCode=po.ShipID " +
                    "where bid.BidSMDLevel1ApprovalDate >= '" + FromDate + "' and BidSMDLevel1ApprovalDate<'" + ToDate + "'  and V.VesselStatusId=1 "+
                    " and BID.SupplierID = " + hfdsupplierid.Value;

        if (VesselCode != "0")
        {
            WhereClause = WhereClause + " and ShipID ='" + VesselCode + "'";
        }
        else if (Fleet != 0)
        {
            WhereClause = WhereClause + " and ShipID in (select vessselcode from dbo.Vessel where fleetid=" + Fleet + ")";
        }

        DataTable Dt = Common.Execute_Procedures_Select_ByQueryCMS(sql + WhereClause + " order by BidSMDLevel1ApprovalDate");
        rptdetails.DataSource = Dt;
        rptdetails.DataBind();
    }

    public void ShowHeader()
    {
        if (Fleet == 0)
            lblFleet.Text = "All";
        else
        {
            DataTable dtFleet = Common.Execute_Procedures_Select_ByQueryCMS("select FleetName from dbo.FleetMaster where FleetID=" + Fleet);
            lblFleet.Text = dtFleet.Rows[0][0].ToString();
        }
        if (VesselCode == "0")
            lblVessle.Text = "All";
        else
        {
            DataTable dtVessle = Common.Execute_Procedures_Select_ByQueryCMS(" select VesselName from dbo.Vessel where VesselCode='" + VesselCode + "' ");
            lblVessle.Text = dtVessle.Rows[0][0].ToString();
        }

        lblPeriod.Text = MonthName(F_M) + "-" + F_Y + " To " + MonthName(T_M) + "-" + T_Y;
    }
    private string MonthName(int m)
    {
        string res;
        switch (m)
        {
            case 1:
                res = "Jan";
                break;
            case 2:
                res = "Feb";
                break;
            case 3:
                res = "Mar";
                break;
            case 4:
                res = "Apr";
                break;
            case 5:
                res = "May";
                break;
            case 6:
                res = "Jun";
                break;
            case 7:
                res = "Jul";
                break;
            case 8:
                res = "Aug";
                break;
            case 9:
                res = "Sep";
                break;
            case 10:
                res = "Oct";
                break;
            case 11:
                res = "Nov";
                break;
            case 12:
                res = "Dec";
                break;
            default:
                res = "";
                break;
        }
        return res;
    }
    public void BindTop5VenderByAmount()
    {
        string WhereClause = "";
        DateTime FromDate, ToDate;
        FromDate = new DateTime(F_Y, F_M, 1);
        ToDate = new DateTime(T_Y, T_M, 1).AddMonths(1);
        if (VesselCode != "0")
        {
            WhereClause = WhereClause + " and ShipID ='" + VesselCode + "'";
        }
        else if (Fleet != 0)
        {
            WhereClause = WhereClause + " and ShipID in (select vessselcode from dbo.Vessel where fleetid=" + Fleet + ")";
        }
        
        string sql = "SELECT top 10 ROW_NUMBER() OVER(ORDER BY convert(numeric(18,2),SUM(amount)) desc) AS Sr,SupplierID,suppliername,suppliercontact,travid,supplieremail,convert(numeric(18,2),SUM(amount)) Amount " +
                    "FROM " +
                    "( " +
                    "SELECT bid.SupplierID, suppliername, suppliercontact, travid, supplieremail, ESTSHIPPINGUSD + (SELECT SUM(UsdPoTotal) FROM dbo.tblSMDPODETAILbid bids where bids.bidid = bid.bidid) as Amount " +
                    "FROM dbo.tblSMDPOMasterbid bid " +
                    "inner join dbo.tblSMDPOMaster po on po.poid = bid.poid " +
                    "inner join dbo.tblSMDSuppliers sp on sp.SupplierID = bid.SupplierID " +
                    "inner join dbo.Vessel V on V.VesselCode=po.ShipID " +
                    "where BidSMDLevel1ApprovalDate >= '" + FromDate + "' and BidSMDLevel1ApprovalDate<'" + ToDate + "' and V.VesselStatusId=1   " + WhereClause +
                    ") A " +
                    "group by SupplierID, suppliername, suppliercontact, travid, supplieremail " +
                    "ORDER BY convert(numeric(18,2),SUM(amount)) DESC";
        
        DataTable Dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        rptTop5VendorByOrder.DataSource = Dt;
        rptTop5VendorByOrder.DataBind();
    }
}
