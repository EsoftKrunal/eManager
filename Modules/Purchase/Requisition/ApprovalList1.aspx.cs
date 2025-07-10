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
using System.Web.UI.DataVisualization.Charting;
//using Microsoft.Office.Interop.Outlook;    
/// <summary>
/// Page Name            : ReqFromVessels.aspx
/// Purpose              : Listing Of Files Received From Vessel
/// Author               : Shobhita Singh
/// Developed on         : 15 September 2010
/// </summary>

public partial class ApprovalList1 : System.Web.UI.Page
{
    public AuthenticationManager auth = new AuthenticationManager(0, 0, ObjectType.Page);

    public int SelectedPoId
    {
        get { return Convert.ToInt32("0" + hfPRID.Value); }
        set { hfPRID.Value = value.ToString(); }
    }
    public string SessionDeleimeter = ConfigurationManager.AppSettings["SessionDelemeter"];
    public int Sel_BidID
    {
        set { ViewState["_Sel_BidID"] = value; }
        get { return int.Parse("0" + ViewState["_Sel_BidID"]); }
    }


    //------------------------------    
    // PAGE LOAD
    protected void Page_Load(object sender, EventArgs e)
    {
        
        //---------------------------------------
        ProjectCommon.SessionCheck_New();
        //---------------------------------------       
        try
        {
            AuthenticationManager auth = new AuthenticationManager(1063, int.Parse(Session["loginid"].ToString()), ObjectType.Page);
            if (!(auth.IsView))
            {

                Response.Redirect("~/AuthorityError.aspx");
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("~/NoPermission.aspx?Message=" + ex.Message);
        }
	//--------------------------------------- 

        lblMsg.Text = "";
        if (!Page.IsPostBack)
        {
            BindVesselDropdown();
            BindStatus();
            BindFleet();
            if (Page.Request.QueryString["Key"] == "1" & Page.Request.QueryString["Stage"] != "")
            {
               ddlStatus.SelectedValue = Page.Request.QueryString["Stage"];
            }
            //LoadSession(); 
            BindRPRepeater();
        }
        if (Session["NWC"] != null)
        {
            chkNWC.Visible = (Session["NWC"].ToString() == "Y");
        }
        else
        {
            chkNWC.Visible = false;
        }
        
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "tr", "SetLastFocus('dvscroll_RFQ');", true);
    }
    //------------------------------    
    protected void BindVesselDropdown()
    {
        string sql;
        if (ChkAllVess.Checked)
        {
            sql = "SELECT vw.ShipID, vw.ShipName FROM VW_sql_tblSMDPRVessels vw where vw.VesselNo in (Select VesselId from UserVesselRelation with(nolock) where Loginid = "+ Convert.ToInt32(Session["loginid"].ToString())+") ORDER BY vw.ShipID";
        }
        else
        {
            sql = "SELECT vw.ShipID, vw.ShipName FROM VW_sql_tblSMDPRVessels vw WHERE (((vw.Active)='A')) and vw.VesselStatusId <> 2 and vw.VesselNo in (Select VesselId from UserVesselRelation with(nolock) where Loginid = " + Convert.ToInt32(Session["loginid"].ToString())+") ORDER BY active desc,vw.ShipID";
        }

        Common.Set_Procedures("ExecQuery");
        Common.Set_ParameterLength(1);
        Common.Set_Parameters(new MyParameter("@Query", sql));
        DataSet dsVessel = new DataSet();
        dsVessel.Clear();
        dsVessel = Common.Execute_Procedures_Select();
        ddlVessel.DataSource = dsVessel;
        ddlVessel.DataTextField = "ShipName";
        ddlVessel.DataValueField = "ShipID";
        ddlVessel.DataBind();
        ddlVessel.Items.Insert(0, new ListItem("- All -", "0"));
    }
    public void BindStatus()
    {
        try
        {
            string sql = "select StatusID,StatusName from VW_sql_tblSMDPRStatusCodes";
            Common.Set_Procedures("ExecQuery");
            Common.Set_ParameterLength(1);
            Common.Set_Parameters(new MyParameter("@Query", sql));
            DataSet dsPrType = new DataSet();
            dsPrType.Clear();
            dsPrType = Common.Execute_Procedures_Select();


        }
        catch { }
    }

    protected void btnBacktoRFQ_Click(object sender, EventArgs e)
    {
        lblSendBackToRFQ.Text = "";
        dvSendBackToRFQ.Visible = true;
    }
    protected void btnSendBackToRFQ_Close_OnClick(object sender, EventArgs e)
    {
        lblSendBackToRFQ.Text = "";
        dvSendBackToRFQ.Visible = false;
    }
    protected void btnSendBackToRFQ_Save_OnClick(object sender, EventArgs e)
    {
        int __POid = Common.CastAsInt32(ViewState["tpo"]);
        //----------------------------------
        if (__POid > 0)
        {
            if(txtSendBackToRFQMessage.Text.Trim()=="")
            {
                lblSendBackToRFQ.Text = "Please enter comments to continue.";
                return;
            }
            //-------
            //DataTable dt = Common.Execute_Procedures_Select_ByQuery("select count(*) from BidApprovalList ba where ApprovedOn is not null and bidid in (select bidid from vw_tblsmdpomasterbid where poid=" + __POid.ToString() + " and BidStatusID>=2)");
            //int CheckCount = 0;
            //CheckCount = Common.CastAsInt32(dt.Rows[0][0]);
            //if(CheckCount >0)
            //{
            //    lblSendBackToRFQ.Text = "Sorry! All bids must be on Approval-1 stage to send back to RFQ stage.";
            //    return;
            //}
            //else
            //{
                try
                {
                    Common.Set_Procedures("sp_SendBackToRFQStage");
                    Common.Set_ParameterLength(3);
                    Common.Set_Parameters(
                        new MyParameter("@POID", __POid),
                        new MyParameter("@UserId", Common.CastAsInt32(Session["loginid"])),
                        new MyParameter("@Comments", txtSendBackToRFQMessage.Text.Trim())
                        );
                    DataSet dsData = new DataSet();
                    Common.Execute_Procedures_IUD(dsData);
                    
                    dvSendBackToRFQ.Visible = false;
                   // btnCloseApprovedPO_OnClick(sender, e);
                    BindRPRepeater();

                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "tr", "alert('Bids sent to RFQ Stage successfully');", true);                    
                }
                catch(Exception ex) 
                {
                    //----------------------------------
                    lblSendBackToRFQ.Text = "Unable to sent back to to RFQ Stage.";
                }
            //}
        }

    }
    protected void btnQA_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "fasf", "window.open('SMDPOAnalyzer.aspx?Prid=" + ViewState["tpo"].ToString() + "')", true);
    }
    //------------------------------
    protected void BindRPRepeaterData(bool Excel)
    {
        string  sql = " SElect *,StatusText=case when AppPhase=1 then 'Approval-Stage1' when AppPhase=2 then 'Approval-Stage2' when AppPhase=3 then 'Approval-Stage3' when AppPhase=4 then 'Approval-Stage4' when AppPhase=5 then 'Ready to Place Order' ELSE '' END,case when InOrderPlace>0 then 'green_row' else 'row' end  as RowCss from  " +
             "       (  " +
             "       select v.*,vv.ShipName  " +
             "       ,(select COUNT(1)  from vw_tblsmdpomasterbid TAB  inner join (select Bidid,min(ApprovalPhase) ApprovalPhase from BidApprovalList where ApprovedOn is null group by Bidid  )tbl on tbl.BidId = TAB.BidID        left outer join vw_tblSmdpoMaster PM on PM.Poid = TAB.PoID where PM.StatusID <> 200 AND ApprovalPhase=5  and tab.poid =V.POID) InOrderPlace" +
             "       , (select count(B.BIdID) from dbo.tblSMDPOMasterBid B inner join Add_tblSMDPOMasterBid A on a.BidID = B.BidID and A.BidFwdOn is not null where b.poid = V.poid and b.ispo=0 and b.bidstatusid=2 and (select min(approvalphase) from BidApprovalList ba where ba.approvedon is null and ba.bidid=b.bidid)=5)ReadyForOrder " +
             "       , (select count(B.BIdID) from dbo.tblSMDPOMasterBid B inner join Add_tblSMDPOMasterBid A on a.BidID = B.BidID and A.BidFwdOn is not null where b.poid = V.poid and b.ispo=0 and b.bidstatusid>=2) SubmittedCount  " +
             "       ,(select top 1 A.BidFwdOn from dbo.tblSMDPOMasterBid B inner join Add_tblSMDPOMasterBid A on a.BidID = B.BidID  where b.poid = V.poid  ) SubmittedOn  " +
         "        ,(select max(a) from ( select bidid,min(approvalphase) a from dbo.bidapprovallist where bidid in (SELECT BIDID FROM dbo.tblSMDPOMasterBid B WHERE B.POID=V.poid) and ApprovedOn is null group by bidid ) e) As APPPhase, (Select Count(BidID) from tblSMDPOMasterBid where poid = V.poid) As BidCount, (Select count(bidid) from tblSMDPODetailBid where  BidID in  (Select BidID from tblSMDPOMasterBid where poid = 106 and isPO = 1)) As PoAcceptedCount " +
             "            from VW_tblSMDPOMasterData V inner join VW_sql_tblSMDPRVessels vv on v.ShipID=vv.ShipID where statusid<>200 " +
             "       )tbl  ";

//	     "       ,isnull((select min(approvalphase) as phase from bidapprovallist where bidid in (SELECT BIDID FROM dbo.tblSMDPOMasterBid B WHERE B.POID=V.poid) and ApprovedOn is null),0) As APPPhase " +

        string WhereClause = "";

        if (chkMyVessel.Checked == false)
        {
            if (ddlVessel.SelectedValue != "0")
            {
                if (WhereClause != "")
                {
                    WhereClause = WhereClause + " and ShipID='" + ddlVessel.SelectedValue + "'";
                }
                else
                {
                    WhereClause = " where ShipID='" + ddlVessel.SelectedValue + "'";
                }
            }
            if (ddlFleet.SelectedIndex > 0)
            {
                if (WhereClause != "")
                {
                    WhereClause = WhereClause + " and FLEETID=" + ddlFleet.SelectedValue + "";
                }
                else
                {
                    WhereClause = " where FLEETID=" + ddlFleet.SelectedValue + "";
                }
            }

            //else
            //{
            //    if (chkNWC.Checked)
            //    {
            //        if (WhereClause != "")
            //        {
            //            WhereClause = WhereClause + " and ShipID IN(SELECT vw.ShipID FROM VW_sql_tblSMDPRVessels vw WHERE vw.Company='NWC')";
            //        }
            //        else
            //        {
            //            WhereClause = " where ShipID IN(SELECT vw.ShipID FROM VW_sql_tblSMDPRVessels vw WHERE vw.Company='NWC')";
            //        }
            //    }
            //    else
            //    {
            //        if (WhereClause != "")
            //        {
            //            WhereClause = WhereClause + " and ShipID IN(SELECT vw.ShipID FROM VW_sql_tblSMDPRVessels vw WHERE vw.Company<>'NWC')";
            //        }
            //        else
            //        {
            //            WhereClause = " where ShipID IN(SELECT vw.ShipID FROM VW_sql_tblSMDPRVessels vw WHERE vw.Company<>'NWC')";
            //        }
            //    }
            //}

        }
        else
        {
            int UID = Common.CastAsInt32(Session["loginid"].ToString());
            if (WhereClause != "")
            {
                WhereClause = WhereClause + " and ShipID IN (Select VesselCode from dbo.vessel v with(nolock) inner join UserVesselRelation uv with(nolock) on v.vesselid = uv.VesselId where LoginId = "+UID+")";
            }
            else
            {
                WhereClause = " where ShipID IN (Select VesselCode from dbo.vessel v with(nolock) inner join UserVesselRelation uv with(nolock) on v.vesselid = uv.VesselId where LoginId = " + UID + ")";
            }
        }
        if (txtPRNumber.Text.Trim() != "")
        {
            Int16 test = 0;
            if (WhereClause != "")
            {
                WhereClause = WhereClause + " and ( tbl.prnum like '%" + txtPRNumber.Text.Trim() + "%' or isnull(tbl.ReqNo,'') like '%" + txtPRNumber.Text.Trim() + "%') ";
            }
            else
            {
                WhereClause = WhereClause + " where ( tbl.prnum like '%" + txtPRNumber.Text.Trim() + "%' or isnull(tbl.ReqNo,'') like '%" + txtPRNumber.Text.Trim() + "%') ";
            }
        }
	if (ddlStatus.SelectedIndex>0)
        {
  	    if (WhereClause != "")
            {
                WhereClause = WhereClause + " and APPPhase=" + ddlStatus.SelectedValue;
            }
            else
            {
                WhereClause = WhereClause + " where APPPhase=" + ddlStatus.SelectedValue;
            }
            
        }
        //--------------        


        if (WhereClause != "")
        {
            WhereClause = WhereClause + " and tbl.prtype is not null";
        }
        else
        {
            WhereClause = " where tbl.prtype is not null";
        }

        sql = sql + WhereClause;
        //
         sql = sql + " and SubmittedCount>0  order by tbl.Poid desc";

        //sql = sql + " and BidCount<> PoAcceptedCount and tbl.SubmittedOn > '2023-10-26' order by tbl.Poid desc";

//Response.Write(sql);
//Response.End();
//return;

        Common.Set_Procedures("ExecQuery");
        Common.Set_ParameterLength(1);
        Common.Set_Parameters(new MyParameter("@Query", sql));
        DataSet dsPrType = new DataSet();
        dsPrType.Clear();
        dsPrType = Common.Execute_Procedures_Select();

        if (dsPrType != null)
        {
            if (dsPrType.Tables[0].Rows.Count != 0)
            {
                RptPRMaster.DataSource = dsPrType;
                RptPRMaster.DataBind();
                if (Excel)
                {

                    DataTable dtNew = new DataTable();
                    dtNew.Columns.Add("Vessel");
                    dtNew.Columns.Add("Type");
                    dtNew.Columns.Add("Vsl Req No");
                    dtNew.Columns.Add("Off Req No");
                    dtNew.Columns.Add("Department");
                    dtNew.Columns.Add("Recieved Date");
                    dtNew.Columns.Add("Status");
                    dtNew.Columns.Add("AccountNo");
                    dtNew.Columns.Add("Office Comment");
                    dtNew.Columns.Add("Ship Comments");
                    foreach (DataRow dr in dsPrType.Tables[0].Rows)
                    {
                        dtNew.Rows.Add(dtNew.NewRow());
                        dtNew.Rows[dtNew.Rows.Count - 1]["Vessel"] = dr["ShipID"].ToString();
                        dtNew.Rows[dtNew.Rows.Count - 1]["Type"] = dr["PRTypeCode"].ToString();
                        dtNew.Rows[dtNew.Rows.Count - 1]["Vsl Req No"] = dr["reqNo"].ToString();
                        dtNew.Rows[dtNew.Rows.Count - 1]["Off Req No"] = dr["prnum"].ToString();
                        dtNew.Rows[dtNew.Rows.Count - 1]["Department"] = dr["deptName"].ToString();
                        dtNew.Rows[dtNew.Rows.Count - 1]["Recieved Date"] = dr["CreatedDate"].ToString();
                        dtNew.Rows[dtNew.Rows.Count - 1]["Status"] = dr["StatusName"].ToString();
                        dtNew.Rows[dtNew.Rows.Count - 1]["AccountNo"] = dr["AccountNumber"].ToString();
                        dtNew.Rows[dtNew.Rows.Count - 1]["Office Comment"] = dr["officecomments"].ToString();
                        dtNew.Rows[dtNew.Rows.Count - 1]["Ship Comments"] = dr["PoComments2"].ToString();
                    }
                    ECommon.ExportDatatable(Response, dtNew, "RFQ");
                }
            }
            else
            {
                //show msg
                RptPRMaster.DataSource = dsPrType;
                RptPRMaster.DataBind();
                lblMsg.Text = "No Data Found with this selection";
            }
            lblRowCount.Text = "( " + RptPRMaster.Items.Count.ToString() + " ) records found.";

        }
    }
    protected void BindRPRepeater()
    {
        BindRPRepeaterData(false);
    }
    public string getStatusColor(string StatusText)
    {
        switch (StatusText)
        {
            case "RFQ Activity":
                return "Blue";
                break;
            case "Ready for Approval":
                return "Green";
                break;
            case "Order Placed":
                return "Black";
                break;
            default:
                return "Red";
                break;
        }
    }
    //------------------------------
    protected void ChkAllVess_CheckedChanged(object sender, EventArgs e)
    {
        // ddlVessel.IncludeInActive = ChkAllVess.Checked;
        BindVesselDropdown();
    }
    protected void ChkNWC_CheckedChanged(object sender, EventArgs e)
    {
       // ddlVessel.IncludeNWC = chkNWC.Checked;
    }
    protected void btnPost_OnClick(object sender, EventArgs e)
    {
        BindRPRepeater();
    }
    //------------------------------
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindRPRepeater();
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Search.aspx");
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ChkAllVess.Checked = false;
        ddlVessel.SelectedIndex = 0;
        txtPRNumber.Text = "";
        BindRPRepeater();
    }
    //------------------------------
    //public void BindRFQ_Repeater(int POId)
    //{
    //    string sql = " select  " +
    //                 "   row_number() over(order by TAB.bidid) as Sno,TAB.BIDID,A.BidFwdOn,B.poid, " +
    //                 "   BIDGROUPNAME,SUPPLIERNAME,SUPPLIERPORT,B.BidStatusID,(SELECT COUNT(*) FROM BIDAPPROVALLIST AL WHERE AL.BIDID = TAB.BIDID) AS APPREQUESTS, " +
    //                 "       (CASE WHEN B.BidStatusID >= 0 AND B.BidStatusID <= 2 THEN 'Y' ELSE 'N' END) AS CanDelete, BIDSTATUSNAME,     " +
    //                 "           (SELECT USDTOT + ESTSHIPPINGUSD FROM dbo.VW_qUSDTotalsPerBid_SQL DET WHERE DET.BIDID = TAB.BIDID) AS AMT, " +
    //                 "               B.APPROVECOMMENTS,BidCreatedBy,BidCreatedOn " +
    //                 "           from dbo.tblSMDPOMasterBid B " +
    //                 "   inner join Add_tblSMDPOMasterBid A on a.BidID = B.BidID and A.BidFwdOn is not null " +
    //                 "   left join dbo.VW_qRFQListing_SQL TAB  on TAB.BidID = b.BidID ";

    //    sql = " select TAB.BIDID,TAB.BIDGROUP as BIDGROUPNAME,TAB.BIDVENNAME as SUPPLIERNAME,ven.ApprovalTypeName,PM.SHIPID,pm.PRNUM " +

    //         "      ,case when tbl.ApprovalPhase=5 then 'green_row' else 'row' end  as RowCss     " +
	   //  "   ,      (select count(*) from dbo.vw_tblsmdpodetailbid dd where dd.bidid=tbl.BidId and isnull(pricefor,0)=0) as ZeroUPCount" +
    //         "      ,case when tbl.ApprovalPhase = 1 then 'Ready for Approval 1'   when tbl.ApprovalPhase = 2 then 'Ready for Approval 2' " +
    //         "      when tbl.ApprovalPhase = 3 then 'Ready for Approval 3'   when tbl.ApprovalPhase = 4 then 'Ready for Approval 4' " +
    //         "      when tbl.ApprovalPhase = 5 then 'Place Order'  end as BidStatus   ,tbl.ApprovalPhase " +
    //         "      ,(SELECT SUPPLIERPORT FROM VW_tblSMDSuppliers SUP WHERE SUP.SUPPLIERID = TAB.SUPPLIERID) as SUPPLIERPORT,(select LoginId from BidApprovalList where BidId = tbl.BidId and ApprovalPhase = tbl.ApprovalPhase ) as UserID,     " +
    //         "       (select poid from vw_tblsmdpomasterbid MBid where MBid.BidID = TAB.BidID) as PoID,    (select ESTSHIPPINGUSD + sum(usdpototal) from vw_tblsmdpodetailbid " +
    //         "       where bidid = TAB.bidid) as AMT, shipid + '-' + convert(varchar, pm.PRNUM) + '-' + BIDGROUP as RFQNO " +
    //         "       from vw_tblsmdpomasterbid TAB " +
    //         "       left join VW_ALL_VENDERS ven on ven.SupplierID=Tab.SupplierID " +
    //         "       inner     join " +
    //         "       ( " +
    //         "       select Bidid,min(ApprovalPhase) ApprovalPhase from BidApprovalList where ApprovedOn is null group by Bidid " +
    //         "       )tbl on tbl.BidId = TAB.BidID " +
    //         "       left outer join vw_tblSmdpoMaster PM on PM.Poid = TAB.PoID " +
    //         "       where PM.StatusID <> 200   and tab.poid =" + POId.ToString();
    //    //Response.Write(sql);
    //    DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
    //    rptRFQList.DataSource = dt;
    //    rptRFQList.DataBind();
    //    int CountStage1 =Common.CastAsInt32(dt.Compute("Count(Bidid)", "ApprovalPhase=1"));
    //    btnBacktoRFQ.Visible =CountStage1>0 ;// (MaxPhase == 1);
    //}
    public void BindFleet()
    {
        DataTable dtFleet = Common.Execute_Procedures_Select_ByQueryCMS("select * from FleetMaster");
        if (dtFleet != null)
        {
            if (dtFleet.Rows.Count >= 0)
            {
                ddlFleet.DataSource = dtFleet;
                ddlFleet.DataTextField = "FleetName";
                ddlFleet.DataValueField = "FleetID";
                ddlFleet.DataBind();
                ddlFleet.Items.Insert(0, new System.Web.UI.WebControls.ListItem("< All >", "0"));
            }
        }
    }


    //protected void btnRefreshRFQ_Click(object sender, EventArgs e)
    //{
    //    BindRFQ_Repeater(Common.CastAsInt32(ViewState["tpo"]));
    //}
    //protected void btnOpenApprovePoPopup_OnClick(object sender, EventArgs e)
    //{
    //    dvApprovePO.Visible = true;
    //    ImageButton btn = (ImageButton)sender;
    //    int POid = Common.CastAsInt32(btn.CommandArgument);
    //    ViewState["tpo"] = POid;
    //    //POid = 45566;
    //    BindRFQ_Repeater(POid);
    //}
    //protected void btnCloseApprovedPO_OnClick(object sender, EventArgs e)
    //{
    //    dvApprovePO.Visible = false;
    //}

    //----------------------------------------
    protected void btnSendBackToPurchaserPopup_OnClick(object sender, EventArgs e)
    {
        ImageButton imgDelete = (ImageButton)sender;
        HiddenField hfBidID = (HiddenField)imgDelete.Parent.FindControl("hfBidID");
        Sel_BidID = Common.CastAsInt32(hfBidID.Value);
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
        if (txtPurchaserComments.Text.Trim() == "")
        {
            lblMSgSendBackToPurchaser.Text = " Please enter comments";
            return;
        }
        try
        {
            Common.Set_Procedures("sp_NewPR_CancelRequestForApproval");
            Common.Set_ParameterLength(2);
            Common.Set_Parameters(
                new MyParameter("@BidId", Sel_BidID),
                new MyParameter("@Comments", txtPurchaserComments.Text.Trim().Replace("'", "`"))
                );
            Boolean res;
            DataSet Ds = new DataSet();
            res = Common.Execute_Procedures_IUD(Ds);
            lblMSgSendBackToPurchaser.Text = "RFQ sent back to purchaser successfully.";
            SendRfqToPurchaserMail(Sel_BidID);
        }
        catch (Exception ex)
        {
            lblMSgSendBackToPurchaser.Text = "Unable to save the record. Error :" + ex.Message;

        }
    }
    public void SendRfqToPurchaserMail(int BidID)
    {
        string ToEmail = "";
        string PurchaserComments = "",rfqno="" ;
        string sql = " SElect (select Email from dbo.UserLogin where LoginId=B.BidCreatedByID)EmailID,shipid + '-' + convert(varchar,prnum) + '-' + BidGroup  as BIDNO, " +
	" b1.ApproveComments " +
	" from Add_tblSMDPOMasterBid B  " +
	" inner join dbo.tblSMDPOMasterBid b1 on b.bidid=b1.bidid  " +
	" inner join dbo.tblSMDPOMaster p on p.poid=b1.poid  where b.BidID=" + BidID;

        DataTable dtEmail = Common.Execute_Procedures_Select_ByQuery(sql);
        if (dtEmail.Rows.Count > 0)
        {
            ToEmail = dtEmail.Rows[0]["EmailID"].ToString();
            PurchaserComments = dtEmail.Rows[0]["ApproveComments"].ToString();
		rfqno = dtEmail.Rows[0]["BIDNO"].ToString();
        }
        ///ToEmail = "emanager@energiossolutions.com";
        if (ToEmail != "")
        {

            string UserEmail = ProjectCommon.gerUserEmail(Session["loginid"].ToString());
            UserEmail = "emanager@energiossolutions.com";
            string[] ToAdds = { ToEmail };
            string[] CCAdds = { };
            string Subject = "RFQ No. " + rfqno + "  sent back to purchaser.";
            string Message = "Dear User,<br/><br/>Subject RFQ has been sent back to you with following comments.<br/><br/>Comments : " + PurchaserComments + " <br/><br/>From,<br/>" + Session["FullName"].ToString() ;
            string Error = "";
            if (ProjectCommon.SendeMail(UserEmail, UserEmail, ToAdds, CCAdds, CCAdds, Subject, Message, out Error, ""))
            {

            }
        }
    }


    public void chkMyVessel_OnCheckedChanged(object sender, EventArgs e)
    {
        BindRPRepeater();
    }
}
