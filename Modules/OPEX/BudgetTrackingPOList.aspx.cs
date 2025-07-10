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
    
    public int TaskID
    {
        get
        { return int.Parse("0" + ViewState["_TaskID"].ToString()); }
        set
        { ViewState["_TaskID"] = value; }
    }
    public int SelBidID
    {
        get
        { return int.Parse("0" + ViewState["SelBidID"].ToString()); }
        set
        { ViewState["SelBidID"] = value; }
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

            TaskID = Common.CastAsInt32(Request.QueryString["TaskID"]);

            ShowHeader();
            showOrders();
            //------
        }
        
    }
    public void WriteInResponse(string data)
    {
        Response.Clear();
        Response.Write(data);
        Response.End();
    }
  
    public string SetColorForVPer(object val)
    {
        if (Convert.ToDecimal(val) > 0)
            return "error_msg";
        else
            return "";
    }
    public string SetColorForYearPer(object val)
    {
        if (Convert.ToDecimal(val) >= 100)
            return "error_msg";
        else
            return "";
    }   
    public string GetBudgetVariance(object b, object c)
    {
        decimal bb = Common.CastAsDecimal(b);
        decimal cc = Common.CastAsDecimal(c);
        if (cc == 0)
            return "0.00";
        else
            return Common.CastAsDecimal(((bb - cc) * 100) / bb).ToString("0.00");

    }

    protected void lblOrdersCount_Click(object sender, EventArgs e)
    {
        string path = "BudgetTrackingPOList_Unlink.aspx?company=" + CompanyCode + "&vessel=" +VesselCode + "&year=" + BudgetYear + "&AccountId=" + AccountId + "&majcatid=" + majcatid + "&midcatid=" + midcatid + "&mincatid=" + midcatid + "&mode=0";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "op", "window.open('" + path + "','');", true);
    }
    //protected void lblOrdersCount1_Click(object sender, EventArgs e)
    //{
    //    string path = "BudgetTrackingPOList_Unlink.aspx?company=" + CompanyCode + "&vessel=" + VesselCode + "&year=" + BudgetYear + "&AccountId=" + AccountId + "&majcatid=" + majcatid + "&midcatid=" + midcatid + "&mincatid=" + midcatid + "&mode=1";
    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "op", "window.open('" + path + "','');", true);
    //}
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
        //Show header information
        int Month = DateTime.Today.Month;
        DataSet DsValue = GetTrackingData(CompanyCode, BudgetYear, Month, VesselCode);
        DataTable Dt = DsValue.Tables[3];
        DataView DV = Dt.DefaultView;
        DV.RowFilter = "MAJCATID=" + majcatid.ToString() + " and MIDCATID=" + midcatid.ToString() + " and MINCATID=" + mincatid.ToString() + " and AccountID=" + AccountId.ToString(); ;
        if (DV.ToTable().Rows.Count > 0)
        {
            DataRow dr = DV.ToTable().Rows[0];

            lblAccountNoNameTaskList.Text = dr["Accountnumber"].ToString() + " - " + dr["AccountName"].ToString();
            lblYTDActule.Text = ProjectCommon.FormatCurrencyWithoutSignNoDecimal(Common.CastAsInt32(dr["AcctYTDAct"]));
            lblYTDCommitted.Text = ProjectCommon.FormatCurrencyWithoutSignNoDecimal(Common.CastAsInt32(dr["AcctYTD_Comm"]));
            lblYTDConsumed.Text = ProjectCommon.FormatCurrencyWithoutSignNoDecimal(Common.CastAsInt32(dr["AcctYTDCons"]));
            lblYTDBudget.Text = ProjectCommon.FormatCurrencyWithoutSignNoDecimal(Common.CastAsInt32(dr["AcctYTDBgt"]));
            lblYTDVariance.Text = ProjectCommon.FormatCurrencyWithoutSignNoDecimal(Common.CastAsInt32(dr["AcctYTDVar"]));
            lblYTDVariancePer.Text = String.Format("{0:0.00}", Common.CastAsDecimal(dr["Col1"])) + " % ";
            lblYTDAnnualBudget.Text = ProjectCommon.FormatCurrencyWithoutSignNoDecimal(Common.CastAsInt32(dr["AcctFYBudget"]));
            lblYTDAnnualUtilization.Text = String.Format("{0:0.00}", Common.CastAsDecimal(dr["Col2"])) + " % ";
        }
        int per1 = 0;
        int per2 = 0;
        decimal amt1 = Common.CastAsDecimal(lblYTDBudget.Text);
        decimal amt2 = Common.CastAsDecimal(lblYTDConsumed.Text);

        decimal max = (amt1 > amt2) ? amt1 : amt2;
        try
        {
            per1 = Common.CastAsInt32((amt1 * 100) / max);
        }
        catch { }
        try
        {
            per2 = Common.CastAsInt32((amt2 * 100) / max);
        }
        catch { }
        dvbud.Style.Add("width", per1.ToString() + "%");
        dvbudcons.Style.Add("width", per2.ToString() + "%");
        decimal varper = Common.CastAsDecimal(lblYTDVariancePer.Text);
        if (varper > 0)
        {
            lblYTDVariancePer.ForeColor = System.Drawing.Color.Red;
        }
        else
        {
            lblYTDVariancePer.ForeColor = System.Drawing.Color.Green;
        }

        decimal annutil = Common.CastAsDecimal(lblYTDAnnualUtilization.Text);
        if (annutil > 0)
        {
            lblYTDAnnualUtilization.ForeColor = System.Drawing.Color.Red;
        }
        else
        {
            lblYTDAnnualUtilization.ForeColor = System.Drawing.Color.Green;
        }
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.TBL_BUDGETTRACKING WHERE TASKID=" + TaskID.ToString());
        lblTaskName.Text = dt.Rows[0]["TaskDescription"].ToString();

        //-------------- unlinked orders
        string sql1 = " select p.invoiceid, m.BidID,BidPoNum,s.SupplierName,m.BidSMDLevel1ApprovalDate as PoDate,  " +
                    "   COALESCE( " +
                    "  (SELECT SUM(TransUSD) FROM DBO.tblApEntries WHERE BIDID = m.bidid AND ScreenName = 'INV'), " +
                    "  (SELECT TransUSD FROM DBO.tblApEntries WHERE BIDID = m.bidid AND ScreenName = 'APP') " +
                    "  ) AS poamount, accountnumber, " +
                    "  bidstatusname, ApproveComments, REfNo " +
                    " , (SELECT TOP 1 INTRAV FROM DBO.tblApEntries WHERE BIDID = m.bidid ORDER BY INTRAV DESC) AS CA " +
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
                    "  left join tbl_BudgetTracking t on t.TaskID = bo.ItemID " +
                    "  where " +
                    "  SHIPID = '" + VesselCode + "' AND po.AccountID =" + AccountId + " AND YEAR(m.BidSMDLevel1ApprovalDate) = " + BudgetYear + " and m.bidstatusid > 0  and t.taskid is null ";
        //" " +


        DataTable DT = Common.Execute_Procedures_Select_ByQuery(sql1);
        lblOrdersCount.Text = DT.Rows.Count.ToString();


        //sql1 = "select count(entrynum) from dbo.VW_NONPO_RECORDS where cocode='" + CompanyCode + "' and shipid='" + VesselCode + "' and year=" + BudgetYear + " And AccountId=" + AccountId + " And TaskId is null";
        //DataTable dt1 = Common.Execute_Procedures_Select_ByQuery(sql1);
        //lblNONPoOrders.Text = dt1.Rows[0][0].ToString();
    }  
    public void showOrders()
    {
        string sql = "select * from VW_TaskLinkedOrders where taskid=" + TaskID.ToString();        
        DataTable Dt = Common.Execute_Procedures_Select_ByQuery(sql);        
        rptTaskDetails.DataSource = Dt;
        rptTaskDetails.DataBind();
        lblctot.Text = ProjectCommon.FormatCurrencyWithoutSign(Dt.Compute("SUM(poamount)", ""));

        //sql = "select * from dbo.VW_NONPO_RECORDS where cocode='" + CompanyCode + "' and shipid='" + VesselCode + "' and year=" + BudgetYear + " And AccountId=" + AccountId + " And TaskId=" + TaskID;
        //DataTable dt1= Common.Execute_Procedures_Select_ByQuery(sql);
        //RPTnONpo.DataSource = dt1;
        //RPTnONpo.DataBind();

        //lblNonPoSum.Text = ProjectCommon.FormatCurrencyWithoutSign(dt1.Compute("SUM(amount)", ""));
    }
    protected void btnLinkPoToTaskPopup(object sender, EventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        SelBidID = Common.CastAsInt32(btn.CommandArgument);
        divLinkPoToTask.Visible = true;
        ShowTaskList_LinkPo();
    }
    protected void btnLinkNONPoToTaskPopup(object sender, EventArgs e)
    {
        //ImageButton btn = (ImageButton)sender;
        //SelBidID = Common.CastAsInt32(btn.CommandArgument);
        //divLinkPoToTask.Visible = true;
        //ShowTaskList_LinkPo();
    }
    
    public void ShowTaskList_LinkPo()
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
        string sql = " update tbl_BudgetTracking_Orders set ItemID=" + hfdSelTaskID_Linkpo.Value + "  Where BidID=" + SelBidID + " ";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
        ShowTaskList_LinkPo();
    }
    protected void btnCloseLinkPoPopup_OnClick(object sender, EventArgs e)
    {
        divLinkPoToTask.Visible = false;
        SelBidID = 0;
    }
}
