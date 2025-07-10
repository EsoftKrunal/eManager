using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using System.Text;
using System.IO;
using System.Configuration;

public partial class BudgetTrackingPOList : System.Web.UI.Page
{
    // PAGE PROPERTIES ------------------
    
    public string CompanyCode
    {
        get
        { return Convert.ToString(ViewState["SelCompanyCode"]); }
        set
        {ViewState["SelCompanyCode"] = value;}
    }    
    public string VesselCode
    {
        get
        { return  Convert.ToString(ViewState["SelVesselCode"]); }
        set
        { ViewState["SelVesselCode"] = value; }
    }
    public int BudgetYear
    {
        get
        { return int.Parse("0"+ViewState["_BudgetYear"].ToString()); }
        set
        { ViewState["_BudgetYear"] = value; }
    }
    public int AccountId
    {
        get
        { return int.Parse("0" + ViewState["_AccountId"].ToString()); }
        set
        { ViewState["_AccountId"] = value; }
    }

    public int majcatid
    {
        get
        { return int.Parse("0" + ViewState["majcatid"].ToString()); }
        set
        { ViewState["majcatid"] = value; }
    }
    public int midcatid
    {
        get
        { return int.Parse("0" + ViewState["midcatid"].ToString()); }
        set
        { ViewState["midcatid"] = value; }
    }
    public int mincatid
    {
        get
        { return int.Parse("0" + ViewState["mincatid"].ToString()); }
        set
        { ViewState["mincatid"] = value; }
    }
   
    public int SelBidID
    {
        get
        { return int.Parse("0" + ViewState["SelBidID"].ToString()); }
        set
        { ViewState["SelBidID"] = value; }
    }
    public int EntryNum
    { 
        get
        { return int.Parse("0" + ViewState["EntryNum"].ToString()); }
        set
        { ViewState["EntryNum"] = value; }
    }
    public int UpdateMode
    {
        get
        { return int.Parse("0" + ViewState["UpdateMode"].ToString()); }
        set
        { ViewState["UpdateMode"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        AuthenticationManager authRecInv;
        //---------------------------------------
        ProjectCommon.SessionCheck();
        #region --------- USER RIGHTS MANAGEMENT -----------
        try
        {
            authRecInv = new AuthenticationManager(1071, int.Parse(Session["loginid"].ToString()), ObjectType.Page);
            if (!(authRecInv.IsView))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "alert('You have not permissions to access this page.');window.close();", true);
		    return;
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "alert('Your session is expired.');window.close();", true);
		return;
        }

        #endregion ----------------------------------------
        if (!Page.IsPostBack)
        {
            CompanyCode = Request.QueryString["company"];
            VesselCode = Request.QueryString["vessel"];

            BudgetYear = Common.CastAsInt32(Request.QueryString["year"]);
            AccountId = Common.CastAsInt32(Request.QueryString["AccountId"]);
            majcatid = Common.CastAsInt32(Request.QueryString["majcatid"]);
            midcatid = Common.CastAsInt32(Request.QueryString["midcatid"]);
            mincatid = Common.CastAsInt32(Request.QueryString["mincatid"]);
            
            ShowHeader();
            showOrders();
            //------
        }
        
    }
 

    //--Open Task List---------------------------------------------------------------
    public DataSet GetTrackingData(string Company, int BudgetYear, int BudgetMonth, string VesselCode)
    {

        DataSet DS = new DataSet();
        //if (Cache["TrackingDatas"] == null)        
        if (true)
        {
            System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["EMANAGER"].ToString());
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("exec getVarianceRepport_vsl_BudgetTracking '" + Company + "'," + BudgetMonth + "," + BudgetYear + ",'" + VesselCode + "'", con); //???????
            cmd.CommandTimeout = 300;
            cmd.CommandType = CommandType.Text;
            System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter();
            adp.SelectCommand = cmd;
            adp.Fill(DS);
            Cache["TrackingDatas"] = DS;
        }
        else
        {
            DS = (DataSet)(Cache["TrackingDatas"]);
        }
        return DS;
    }
    public void ShowHeader()
    {
        lblCompany.Text = CompanyCode;
        lblVessel.Text = VesselCode;
        lblYear.Text = BudgetYear.ToString();
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT CONVERT(VARCHAR,ACCOUNTNUMBER) + ' : ' + ACCOUNTNAME FROM DBO.sql_tblSMDPRAccounts WHERE ACCOUNTID=" + AccountId.ToString());
        lblAccountNoNameTaskList.Text = dt.Rows[0][0].ToString();
    }  
    public void showOrders()
    {

        string sql = " select p.invoiceid, m.BidID,BidPoNum,s.SupplierName,m.BidSMDLevel1ApprovalDate as PoDate,  " +
                     "   COALESCE( " +
                     "  (SELECT SUM(TransUSD)FROM DBO.tblApEntries WHERE BIDID = m.bidid AND ScreenName = 'INV'), " +
                     "  (SELECT TransUSD FROM DBO.tblApEntries WHERE BIDID = m.bidid AND ScreenName = 'APP') " +
                     "  ) AS poamount, accountnumber, " +
                     "  bidstatusname, ApproveComments, REfNo, " +
                     "  (SELECT TOP 1 INTRAV FROM DBO.tblApEntries WHERE BIDID = m.bidid ORDER BY INTRAV DESC) AS CA " +
                     "  from " +
                     "  dbo.tblsmdpomasterbid m " +
                     "  inner " +
                     "  join dbo.tblSMDBIDStatusCodes stat on m.bidstatusid = stat.bidstatusid " +
                     "  inner " +
                     "  join dbo.tblsmdpomaster po on po.poid = m.poid " +
                     "  inner " +
                     "  join dbo.sql_tblSMDPRAccounts a on a.accountid = po.AccountID " +
                     "  inner " +
                     "  join dbo.tblsmdSuppliers s on s.SupplierID = m.SupplierID " +
                     "  left " +
                     "  join POS_Invoice_Payment_PO p on p.bidid = m.bidid " +
                     "  left " +
                     "  join POS_Invoice i on p.InvoiceId = I.InvoiceId " +
                     "  left " +
                     "  join tbl_BudgetTracking_Orders bo on bo.BidID = m.bidid " +
                     "  left " +
                     "  join tbl_BudgetTracking t on t.TaskID = bo.ItemID " +
                     "  where " +
                     "  SHIPID = '" + VesselCode + "' AND po.AccountID =" + AccountId + " AND YEAR(m.BidSMDLevel1ApprovalDate) = " + BudgetYear + " and m.bidstatusid > 0 and t.taskid is null  "; 


        DataTable Dt = Common.Execute_Procedures_Select_ByQuery(sql);
        rptTaskDetails.DataSource = Dt;
        rptTaskDetails.DataBind();

        lblctot.Text = ProjectCommon.FormatCurrencyWithoutSign(Dt.Compute("SUM(poamount)", ""));
        //------------
        //string sql1 = "select * from dbo.VW_NONPO_RECORDS where cocode='" + CompanyCode + "' and shipid='" + VesselCode + "' and year=" + BudgetYear + " And AccountId=" + AccountId + " And TaskId is null";
        //DataTable dt1 = Common.Execute_Procedures_Select_ByQuery(sql1);
        //RPTnONpo.DataSource = dt1;
        //RPTnONpo.DataBind();
        //lblNonPoSum.Text = ProjectCommon.FormatCurrencyWithoutSign(dt1.Compute("SUM(amount)", ""));
        //lblNONPoOrders.Text = dt1.Rows[0][0].ToString();
    }

    protected void btnLinkPoToTaskPopup(object sender, EventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        SelBidID = Common.CastAsInt32(btn.CommandArgument);
        divLinkPoToTask.Visible = true;
        hfdSelTaskID_Linkpo.Value = "0";
        UpdateMode = 0;
        ShowAvailableTaskList();
    }
    protected void btnLinkPoToTaskPopup1(object sender, EventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        EntryNum = Common.CastAsInt32(btn.CommandArgument);
        divLinkPoToTask.Visible = true;
        hfdSelTaskID_Linkpo.Value = "0";
        UpdateMode = 1;
        ShowAvailableTaskList();
    }
    
    public void ShowAvailableTaskList()
    {
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        string sql = "select *, " +
                    " ( " +
                    "    Case when Jan = 1 then 1 else 0 end + " +
                    "    Case when Feb = 1 then 1 else 0 end + " +
                    "    Case when Mar = 1 then 1 else 0 end + " +
                    "    Case when Apr = 1 then 1 else 0 end + " +
                    "    Case when May = 1 then 1 else 0 end + " +
                    "    Case when Jun = 1 then 1 else 0 end + " +
                    "    Case when Jul = 1 then 1 else 0 end + " +
                    "    Case when Aug = 1 then 1 else 0 end + " +
                    "    Case when Sep = 1 then 1 else 0 end + " +
                    "    Case when Oct = 1 then 1 else 0 end + " +
                    "    Case when Nov = 1 then 1 else 0 end + " +
                    "    Case when Dec = 1 then 1 else 0 end   " +
                    "    ) as ConsumptionMonthCount " +
                    "    ,(select SUM(EstShippingUSD+CreditUSD+BidAmt)as TotConsume from vw_BudgetTracking where taskid=tbl_BudgetTracking.taskid) as TotConsume" +
                    " from tbl_BudgetTracking where Company='" + CompanyCode + "' and VesselCode='" +VesselCode + "' and BudgetYear=" + BudgetYear + " and AccountID=" + AccountId + "";

        
        DataTable DT = Common.Execute_Procedures_Select_ByQuery(sql);
        rptTaskListLink.DataSource = DT;
        rptTaskListLink.DataBind();
    }
    protected void btnTempUpdateBIdID_OnClick(object sender, EventArgs e)
    {
        if (Common.CastAsInt32(hfdSelTaskID_Linkpo.Value) <= 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Fsadf", "alert('Please select any task to continue.');",true);
        }
        else
        {
            if (UpdateMode == 0)
            {
                string sql = "exec dbo.sp_PR_IU_LinkPONonPOToTask " + SelBidID + "," + hfdSelTaskID_Linkpo.Value + "," + UpdateMode;
                DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
                showOrders();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fsa5df", "alert('Record savd successfully.');", true);
            }
            else
            {
                string sql = "exec dbo.sp_PR_IU_LinkPONonPOToTask " + EntryNum + "," + hfdSelTaskID_Linkpo.Value + "," + UpdateMode;
                DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
                showOrders();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fsa5df", "alert('Record savd successfully.');", true);
            }
        }
    }

    protected void btnCloseLinkPoPopup_OnClick(object sender, EventArgs e)
    {
        divLinkPoToTask.Visible = false;
        SelBidID = 0;
    }
}
