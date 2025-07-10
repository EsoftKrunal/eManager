using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ApprovalList : System.Web.UI.Page
{
    // current status 
    public int PageNo
    {
        set { ViewState["PageNo"] = value; }
        get { return int.Parse("0" + ViewState["PageNo"]); }
    }
    public int PagesSlot
    {
        set { ViewState["PagesSlot"] = value; }
        get { return int.Parse("0" + ViewState["PagesSlot"]); }
    }
    // size of slot/records
    public int PageSlotsCount = 20;
    public int PageRecordsCount = 13;
    // max no of slot/pages
    public int TotalPages
    {
        set { ViewState["TotalPages"] = value; }
        get { return int.Parse("0" + ViewState["TotalPages"]); }
    }
    public int TotalSlots
    {
        set { ViewState["TotalSlots"] = value; }
        get { return int.Parse("0" + ViewState["TotalSlots"]); }
    }

    public int Sel_BidID
    {
        set { ViewState["_Sel_BidID"] = value; }
        get { return int.Parse("0" + ViewState["_Sel_BidID"]); }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        //---------------------------------------
        ProjectCommon.SessionCheck();
        //---------------------------------------
        lblMSgSendBackToPurchaser.Text = "";
        if (!Page.IsPostBack)
        {
            BindVessel();
            LoadSupt();
            BindRFQ();
        }
    }
    protected void LoadSupt()
    {
        string sql = "SELECT LoginID,FirstName + ' ' + Lastname as SuptName FROM USERMASTER WHERE ROLEID=17";
        sql = " select UserName,LoginId from "+
              "  ( " +
              "      select(FirstName + ' ' + LastName) AS UserName, LoginId from dbo.usermaster where loginid in (select userid from dbo.pos_invoice_mgmt where Approval = 1) AND statusId = 'A' " +
              "      Union " +
              "      select(FirstName + ' ' + LastName) AS UserName, LoginId from dbo.usermaster where loginid in (select userid from dbo.pos_invoice_mgmt where Verification = 1) AND statusId = 'A' " +
              "      Union " +
              "      SELECT FirstName + ' ' + MiddleName + ' ' + FamilyName AS UserName, USERID as LoginId FROM DBO.Hr_PersonalDetails WHERE POSITION IN(4, 89) " +
              "      Union " +
                "    SELECT FirstName + ' ' + MiddleName + ' ' + FamilyName AS UserName, USERID as LoginId FROM DBO.Hr_PersonalDetails WHERE POSITION = 1 " +
                "                ) as t " +
                "                order by t.UserName ";

        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql); //EMAIL,
        ddlSupt.DataSource = dt;
        ddlSupt.DataTextField = "UserName";
        ddlSupt.DataValueField = "LoginID";
        ddlSupt.DataBind();
        ddlSupt.Items.Insert(0, new ListItem("< All >", ""));
        ListItem li = ddlSupt.Items.FindByValue(Session["loginid"].ToString());
        if (li != null)
            li.Selected = true; 
    }
    public void BindVessel()
    {
        //string sql = "";
        //if (ChkAllVess.Checked == false)
        //{
        //    sql = "SELECT vw.ShipID, vw.ShipName FROM VW_sql_tblSMDPRVessels vw WHERE (((vw.Active)='A')) ORDER BY vw.ShipID";
        //}
        //else
        //{
        //    sql = "SELECT vw.ShipID, vw.ShipName FROM VW_sql_tblSMDPRVessels vw WHERE vw.Active in('Y','N') order by active desc,vw.ShipID";
        //}
        //Common.Set_Procedures("ExecQuery");
        //Common.Set_ParameterLength(1);
        //Common.Set_Parameters(new MyParameter("@Query", sql));
        //DataSet dsPrType = new DataSet();
        //dsPrType.Clear();
        //dsPrType = Common.Execute_Procedures_Select();
        //ddlVessel.DataSource = dsPrType;
        //ddlVessel.DataTextField = "ShipName";
        //ddlVessel.DataValueField = "ShipID"; 
        //ddlVessel.DataBind();  
        //ddlVessel.Items.Insert(0,new ListItem("< All >", "0"));
        //ddlVessel.SelectedIndex = 0;
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindRFQ();
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        //ChkAllVess.Checked = false;
        //ddlVessel.SelectedIndex = 0;
        //ddlPRType.SelectedIndex = 0;
        //txtPRNumber.Text = "";
        //ddlStatus.SelectedIndex = 0;
        //txtFromDate.Text = DateTime.Today.AddMonths(-1).ToString("dd-MMM-yyyy");
        //txtToDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
        //ClearSession();
    }
    #region Event ******************************************************************

    // Radio ---------------------------------------------------
    protected void rdoPhase_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        BindRFQ();
    }
    
    protected void BtnReload_OnClick(object sender, EventArgs e)
    {
        BindRFQ();
    }

    // Paging Events and functions
    protected void lnkPrev20Pages_OnClick(object sender, EventArgs e)
    {
        PagesSlot--;
        PageNo = ((PagesSlot - 1) * PageSlotsCount) + 1;
        Refresh();
    }
    protected void lnkNext20Pages_OnClick(object sender, EventArgs e)
    {
        PagesSlot++;
        PageNo = ((PagesSlot - 1) * PageSlotsCount) + 1;

        Refresh();
    }
    protected void lnPageNumber_OnClick(object sender, EventArgs e)
    {
        LinkButton lnPageNumber = (LinkButton)sender;
        PageNo = Common.CastAsInt32(lnPageNumber.Text);
        Refresh();
    }
    protected void Refresh()
    {
        //DataTable dtSearchData = Common.Execute_Procedures_Select_ByQuery(Session["sSqlForPrint"].ToString());
        DataTable dt = (DataTable)Session["sSqlForPrint"];
        BindRFQPagingData(dt);
    }
    protected void BindRFQPagingData(DataTable dtData)
    {
        if (dtData.Rows.Count <= 0)
        {
            TotalPages = 0;
            TotalSlots = 0;

            PageNo = 0;
            PagesSlot = 0;

            lnkPrev20Pages.Visible = false;
            lnkNext20Pages.Visible = false;

            rptRFQList.DataSource = null;
            rptRFQList.DataBind();
            //lblRecordCount.Text = "No Records Found.";

            rptPageNumber.DataSource = null;
            rptPageNumber.DataBind();
        }
        else
        {

            if (PageNo == 0)
            {
                PageNo = 1;
            }
            //lblRecordCount.Text = "(" + dtData.Rows.Count + ") Records Found.";

            TotalPages = Common.CastAsInt32(Math.Ceiling(Convert.ToDecimal(dtData.Rows.Count) / PageRecordsCount));
            TotalSlots = Common.CastAsInt32(Math.Ceiling(Convert.ToDecimal(TotalPages) / PageSlotsCount));

            int StartRow = (PageNo - 1) * PageRecordsCount + 1;
            int EndRow = StartRow + PageRecordsCount - 1;

            DataView dvFiltered = dtData.DefaultView;
            dvFiltered.RowFilter = "Sno>=" + StartRow + " and Sno<=" + EndRow + "";

            rptRFQList.DataSource = dvFiltered; ;
            rptRFQList.DataBind();

            BindPageSlot();
            BindPageRepeater();
        }
    }
    public void BindPageSlot()
    {
        //---------------
        lnkPrev20Pages.Visible = true;
        lnkNext20Pages.Visible = true;
        //---------------
        if (TotalSlots <= 1)
        {
            lnkPrev20Pages.Visible = false;
            lnkNext20Pages.Visible = false;
        }
        else
        {
            if (PagesSlot <= 1)
            {
                lnkPrev20Pages.Visible = false;
            }
            if (PagesSlot >= TotalSlots)
            {
                lnkNext20Pages.Visible = false;
            }
        }
    }
    public void BindPageRepeater()
    {
        if (PagesSlot == 0)
        {
            PagesSlot = 1;
        }
        int PageFrom = ((PagesSlot - 1) * PageSlotsCount) + 1;
        int PageTo = PageFrom + PageSlotsCount - 1;

        DataTable DtPages = new DataTable();
        DtPages.Columns.Add("PageNumber", typeof(int));

        for (int i = PageFrom; i <= PageTo && i <= TotalPages; i++)
        {
            DtPages.Rows.Add(i.ToString());
        }

        rptPageNumber.DataSource = DtPages;
        rptPageNumber.DataBind();
    }
    #endregion
    
    #region Function ***************************************************************
    public void BindRFQ()
    {
        string whereclause = " And 1=1 ";
        string childwhereclause =  "";

        if (ddlVessel.SelectedValue !="")
        {
            whereclause = " And tbl.Shipid='" + ddlVessel.SelectedValue + "'"; 
        }
        else /// code for nwc vessels
        {
            //if (Session["NWC"].ToString() == "Y")
            //{
            //    whereclause = whereclause + " and PM.ShipID IN(SELECT vw.ShipID FROM VW_sql_tblSMDPRVessels vw WHERE vw.Company='NWC')";
            //}
            //else
            //{
            //    whereclause = whereclause + " and PM.ShipID IN(SELECT vw.ShipID FROM VW_sql_tblSMDPRVessels vw WHERE vw.Company<>'NWC')";
            //}
            whereclause = whereclause + " and tbl.ShipID IN(SELECT vw.ShipID FROM VW_sql_tblSMDPRVessels vw WHERE 1=1 )";
        }
        //--------------------------------
        if (txtPRNumber.Text.Trim() != "")
        {
            whereclause = whereclause + " And tbl.prnum like '%" + txtPRNumber.Text.Trim() + "%'"; 
        }
        if (ddlSupt.SelectedIndex > 0)
        {
            whereclause = " And tbl.UserID=" + ddlSupt.SelectedValue + "";
        }
        if (ddlStatus.SelectedIndex > 0)
        {
                    whereclause = whereclause+ " And tbl.ApprovalPhase=" + ddlStatus.SelectedValue;
        }

        //string sql = "select row_number() over (order by bidid) as Sno,TAB.BIDID,TAB.BIDGROUPNAME,TAB.SUPPLIERNAME,TAB.SUPPLIERPORT, " + 
        //             "(case when exists (select bidid from BidApprovalList Bal where Bal.BidID=TAB.BidID and Bal.Approved=1) then 'Ready to place order' " +
        //             "else " +
        //                "case when PM.StatusId=20 then 'Ready For Approval' else 'Ready For 2nd Approval' end " +
        //             "end) as BidStatus, " +
        //             "(case when exists (select bidid from BidApprovalList Bal where Bal.BidID=TAB.BidID and Bal.Approved=1) "+
        //                "then 'false' else case when PM.StatusId=20 then 'true' else "+
        //                "'false' end end) as visibilityStatusDel, "+
        //             "(select LoginID from BidApprovalList apl where apl.BidID =TAB.BidID)as UserID ," +
        //             " ( select poid from vw_tblsmdpomasterbid MBid where MBid.BidID=TAB.BidID) as PoID " +
        //             ",(SELECT USDTOT+ESTSHIPPINGUSD FROM dbo.VW_qUSDTotalsPerBid_SQL DET WHERE DET.BIDID=TAB.BIDID) AS AMT,dbo.getBidPONum(TAB.BIDID) as RFQNO from VW_qRFQListing_SQL TAB  left outer join vw_tblSmdpoMaster PM on PM.Poid=TAB.PoID  where BidId in (select BidID from BidApprovalList where orderplaced<>1 " + childwhereclause + ") " + whereclause;

        string sql = "select row_number() over (order by PM.SHIPID, TAB.BIDID) as Sno,TAB.BIDID,TAB.BIDGROUP as BIDGROUPNAME,TAB.BIDVENNAME as SUPPLIERNAME,(SELECT SUPPLIERPORT FROM VW_tblSMDSuppliers SUP WHERE SUP.SUPPLIERID=TAB.SUPPLIERID) as SUPPLIERPORT, " +
                    "(case when exists (select bidid from BidApprovalList Bal where Bal.BidID=TAB.BidID and Bal.Approved=1) then 'Ready to place order' else case when exists(select ApprovalPhase from BidApprovalList where approvalphase=1 and BidApprovalList.bidid=TAB.BidID) then 'Ready For Approval' else 'Ready For 2nd Approval' end end) as BidStatus, " +
                    "(case when exists (select bidid from BidApprovalList Bal where Bal.BidID=TAB.BidID and Bal.Approved=1) then 'false' else case when exists(select ApprovalPhase from BidApprovalList where approvalphase=1 and BidApprovalList.bidid=TAB.BidID) then 'true' else 'false' end end) as visibilityStatusDel,  " +
                    "(select LoginID from BidApprovalList apl where apl.BidID =TAB.BidID)as UserID , " +
                    "(select poid from vw_tblsmdpomasterbid MBid where MBid.BidID=TAB.BidID) as PoID, " +
                    "(select ESTSHIPPINGUSD+sum(usdpototal) from vw_tblsmdpodetailbid where bidid=TAB.bidid) as AMT, shipid + '-' + convert(varchar,pm.PRNUM) + '-' + BIDGROUP as RFQNO " +
                    "from vw_tblsmdpomasterbid TAB  " +
                    "left outer join vw_tblSmdpoMaster PM on PM.Poid=TAB.PoID   " +
                    "where BidId in (select BidID from BidApprovalList where isnull(orderplaced,0)<>1 " + childwhereclause + ") " + whereclause + " And PM.StatusID!=200  order by PM.SHIPID,TAB.BIDID";

        sql = " select row_number() over (order by PM.SHIPID, TAB.BIDID) as Sno ,TAB.BIDID,TAB.BIDGROUP as BIDGROUPNAME,TAB.BIDVENNAME as SUPPLIERNAME "+
              "  ,case when bal.ApprovalPhase = 1 then 'Ready for Approval 1' " +
              "  when bal.ApprovalPhase = 2 then 'Ready for Approval 2' " +
              "  when bal.ApprovalPhase = 3 then 'Ready for Approval 3' " +
              "  when bal.ApprovalPhase = 4 then 'Ready for Approval 4' " +
              "  when bal.ApprovalPhase = 5 then 'Place Order'  end as BidStatus " +
              "  ,bal.ApprovalPhase" +
              "  ,(SELECT SUPPLIERPORT FROM VW_tblSMDSuppliers SUP WHERE SUP.SUPPLIERID = TAB.SUPPLIERID) as SUPPLIERPORT,bal.loginid as UserID, " +
              "  (select poid from vw_tblsmdpomasterbid MBid where MBid.BidID = TAB.BidID) as PoID,  " +
              "  (select ESTSHIPPINGUSD + sum(usdpototal) from vw_tblsmdpodetailbid where bidid = TAB.bidid) as AMT, shipid + '-' + convert(varchar, pm.PRNUM) + '-' + BIDGROUP as RFQNO " +
              "  from BidApprovalList bal inner " +
              "  join vw_tblsmdpomasterbid TAB   on tab.BidID = bal.BidId " +
              "  left outer join vw_tblSmdpoMaster PM on PM.Poid = TAB.PoID " +
              "  where bal.ApprovedOn is null and PM.ShipID IN(SELECT vw.ShipID FROM VW_sql_tblSMDPRVessels vw WHERE 1 = 1 ) "+ whereclause + " And PM.StatusID != 200  order by PM.SHIPID,TAB.BIDID ";

        sql = " select row_number() over (order by tbl.SHIPID,tbl.BIDID) as Sno ,* from ( " +
                "  select TAB.BIDID,TAB.BIDGROUP as BIDGROUPNAME,TAB.BIDVENNAME as SUPPLIERNAME,PM.SHIPID,pm.PRNUM    " +
              "   ,case when tbl.ApprovalPhase = 1 then 'Ready for Approval 1'   when tbl.ApprovalPhase = 2 then 'Ready for Approval 2'  " +
              "   when tbl.ApprovalPhase = 3 then 'Ready for Approval 3'   when tbl.ApprovalPhase = 4 then 'Ready for Approval 4'  " +
              "    when tbl.ApprovalPhase = 5 then 'Place Order'  end as BidStatus   ,tbl.ApprovalPhase  " +
              "    ,(SELECT SUPPLIERPORT FROM VW_tblSMDSuppliers SUP WHERE SUP.SUPPLIERID = TAB.SUPPLIERID) as SUPPLIERPORT,(select LoginId from BidApprovalList where BidId = tbl.BidId and ApprovalPhase = tbl.ApprovalPhase ) as UserID,    " +
              "     (select poid from vw_tblsmdpomasterbid MBid where MBid.BidID = TAB.BidID) as PoID,    (select ESTSHIPPINGUSD + sum(usdpototal) from vw_tblsmdpodetailbid  " +
              "     where bidid = TAB.bidid) as AMT, shipid + '-' + convert(varchar, pm.PRNUM) + '-' + BIDGROUP as RFQNO  " +
              "     from vw_tblsmdpomasterbid TAB  " +
             "      inner  " +
             "      join  " +
             "   (  " +
             "      select Bidid,min(ApprovalPhase) ApprovalPhase from BidApprovalList where ApprovedOn is null group by Bidid  " +
             "      )tbl on tbl.BidId = TAB.BidID  " +
             "      left outer join vw_tblSmdpoMaster PM on PM.Poid = TAB.PoID  " +
             "      where PM.ShipID IN(SELECT vw.ShipID FROM VW_sql_tblSMDPRVessels vw WHERE 1 = 1 )  And 1 = 1  " +
             "      and PM.ShipID IN(SELECT vw.ShipID FROM VW_sql_tblSMDPRVessels vw WHERE 1 = 1 )  And PM.StatusID != 200  " +
             "  )tbl where 1=1 " + whereclause + " order by tbl.SHIPID,tbl.BIDID     ";

//Response.Write(sql);
        DataTable dtPR = Common.Execute_Procedures_Select_ByQuery(sql);
        Session.Add("sSqlForPrint", dtPR);

        BindRFQPagingData(dtPR);

        //rptRFQList.DataSource = dtPR;
        //rptRFQList.DataBind();
    }
    #endregion
    protected void ChkAllVess_CheckedChanged(object sender, EventArgs e)
    {
        BindVessel(); 
    }

    //----------------------------------------
    protected void imgDelete_OnClick(object sender, EventArgs e)
    {
        ImageButton imgDelete = (ImageButton)sender;
        HiddenField hfBidID = (HiddenField)imgDelete.Parent.FindControl("hfBidID");
        Sel_BidID = Common.CastAsInt32( hfBidID.Value);
        DvSendBackToPurchaser.Visible = true; ;
        //Common.Set_Procedures("sp_NewPR_CancelRequestForApproval");
        //Common.Set_ParameterLength(1);
        //Common.Set_Parameters(
        //    new MyParameter("@BidId", hfBidID.Value)
        //    );
        //Boolean res;
        //DataSet Ds = new DataSet();
        //res = Common.Execute_Procedures_IUD(Ds);
        //BindRFQ();
    }
    protected void btnCloseSendBackToPurchaser_OnClick(object sender, EventArgs e)
    {
        DvSendBackToPurchaser.Visible = false;
        Sel_BidID = 0;
    }
    protected void btnSendBackToPurchaser_OnClick(object sender, EventArgs e)
    {
        bool GoNext = false;
        try
        {
            if (txtPurchaserComments.Text.Trim() == "")
            {
                lblMSgSendBackToPurchaser.Text = " Please enter comments";
                return;
            }
            string sql = "update dbo.tblSMDPOMasterBid set  ApproveComments ='" + txtPurchaserComments.Text.Trim().Replace("'", "`") + "' where BidID=" + Sel_BidID;
            DataTable dt1 = Common.Execute_Procedures_Select_ByQuery(sql);
            GoNext = true;
        }
        catch (Exception ex) { }

        if (GoNext)
        {
            try
            {
                Common.Set_Procedures("sp_NewPR_CancelRequestForApproval");
                Common.Set_ParameterLength(1);
                Common.Set_Parameters(
                    new MyParameter("@BidId", Sel_BidID)
                    );
                Boolean res;
                DataSet Ds = new DataSet();
                res = Common.Execute_Procedures_IUD(Ds);
                lblMSgSendBackToPurchaser.Text = "RFQ sent back to purchaser successfully.";
                BindRFQ();

            }
            catch (Exception ex)
            {
                lblMSgSendBackToPurchaser.Text = "Unable to save the record.";

            }
        }
    }
}
