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
//using Microsoft.Office.Interop.Outlook;    
/// <summary>
/// Page Name            : OrderList.aspx
/// Purpose              : Listing Of Purchase Orders
/// Author               : Jitendra Kumar Vishwakrma
/// Developed on         : 04 May 2011
/// </summary>

public partial class OrderList : System.Web.UI.Page
{
    public AuthenticationManager authPO = new AuthenticationManager(0, 0, ObjectType.Page);
            
    public int SelectedPoId
    {
        get { return Convert.ToInt32("0" + hfBidID.Value); }
        set { hfBidID.Value = value.ToString(); }
    }
    public string SessionDeleimeter = ConfigurationManager.AppSettings["SessionDelemeter"];
    //------------------------------
    #region ---------- PageLoad ------------
    // PAGE LOAD
    protected void Page_Load(object sender, EventArgs e)
    {
        //---------------------------------------
        ProjectCommon.SessionCheck();
        //---------------------------------------
        #region --------- USER RIGHTS MANAGEMENT -----------
        try
        {
            AuthenticationManager authPO = new AuthenticationManager(1064, int.Parse(Session["loginid"].ToString()), ObjectType.Page);
            if (!(authPO.IsView))
            {
                Response.Redirect("~/NoPermission.aspx",false);
            }

            btnCancelPO.Visible = authPO.IsDelete;

            AuthenticationManager authRecGoods = new AuthenticationManager(1064, int.Parse(Session["loginid"].ToString()), ObjectType.Page);
            btnRecGoods.Visible = authRecGoods.IsView;

            //AuthenticationManager authRecInv = new AuthenticationManager(1064, int.Parse(Session["loginid"].ToString()), ObjectType.Page);
            //btnEnterInv.Visible = authRecInv.IsView;
        }
        catch (Exception ex)
        {
            Response.Redirect("~/NoPermission.aspx?Message=" + ex.Message);
        }
        #endregion ----------------------------------------

        lblMsg.Text = "";
        //this.Form.DefaultButton = btnSearch.UniqueID;
        if (!Page.IsPostBack)
        {
            BindVesselDropdown();
            BindStatus();
            BindPrType();
            txtFromDate.Text = new DateTime(DateTime.Today.Year, 1, 1).ToString("dd-MMM-yyyy"); //
            txtToDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
            LoadSession();
           
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
    private void LoadSession()
    {
        string[] Delemeters = { SessionDeleimeter};
        string values = "" + Session["POOrderList"];
        string[] ValueList = values.Split(Delemeters,StringSplitOptions.None);
        try
        {
            ChkAllVess.Checked = ValueList[0] == "Y";
            ddlVessel.SelectedValue = ValueList[1];
            ddlPRType.SelectedValue = ValueList[2];
            txtPRNumber.Text = ValueList[3];
            ddlStatus.SelectedValue = ValueList[4];
            txtFromDate.Text = ValueList[5];
            txtToDate.Text = ValueList[6];
            SelectedPoId = int.Parse("0" + ValueList[7]);
        }catch { } 
    }
    private void WriteSession()
    {
        string values = ((ChkAllVess.Checked) ? "Y" : "N") + SessionDeleimeter + 
                        ddlVessel.SelectedValue + SessionDeleimeter +  
                        ddlPRType.SelectedValue + SessionDeleimeter +
                        txtPRNumber.Text + SessionDeleimeter +
                        ddlStatus.SelectedValue  + SessionDeleimeter +
                        txtFromDate.Text+ SessionDeleimeter +
                        txtToDate.Text + SessionDeleimeter + 
                        SelectedPoId;
        Session["POOrderList"] = values; 
    }
    private void ClearSession()
    {
        Session["POOrderList"] = null;
        SelectedPoId = 0;
    }
    #endregion
    //------------------------------
    #region -------- Page Control Loader ------------
    public void BindStatus()
    {
         try
        {
            string sql = "select BidStatusID,BidStatusName from VW_tblSMDBIDStatusCodes WHERE BidStatusID in (3,4,5,6,-2)";
            Common.Set_Procedures("ExecQuery");
            Common.Set_ParameterLength(1);
            Common.Set_Parameters(new MyParameter("@Query", sql));
            DataSet dsPrType = new DataSet();
            dsPrType.Clear();
            dsPrType = Common.Execute_Procedures_Select();

            ddlStatus.DataSource = dsPrType;
            ddlStatus.DataTextField = "BidStatusName";
            ddlStatus.DataValueField = "BidStatusID";
            ddlStatus.DataBind();
            ddlStatus.Items.Insert(0, new ListItem("All","999"));
            ddlStatus.SelectedIndex = 2;
        }
        catch { }
    }
    public void BindPrType()
    {
        string sql = "select * from vw_sql_tblSMDPRTypes";
        Common.Set_Procedures("ExecQuery");
        Common.Set_ParameterLength(1);
        Common.Set_Parameters(new MyParameter("@Query", sql));
        DataSet dsPrType = new DataSet();
        dsPrType.Clear();
        dsPrType = Common.Execute_Procedures_Select();

        ddlPRType.DataSource = dsPrType;
        ddlPRType.DataTextField = "PrtypeDesc";
        ddlPRType.DataValueField = "prtype";
        ddlPRType.DataBind();
        ddlPRType.Items.Insert(0, new ListItem("Select", "0")); ;
        ddlPRType.SelectedIndex = 0;
    }
    #endregion    
    //------------------------------
    protected void BindRPRepeater()
    {
        WriteSession();
        //string sql = "select Poid,ShipID,PRTypeCode,bidponum,prnum,BidStatusDate,BidStatusName ,(select TOP 1 comment from VW_tblBudgetVActualComments where CommBidId=VM.BIDID) as Comments from VW_qRFQListing_sql VM";
        string sql = "select VM.BidId,VM.Poid,VM.ShipID,VM.PRTypeCode,VM.bidponum,VM.prnum,mst.REQNO,VM.BidStatusDate,VM.BidStatusName,Comm.Comment as Comments,Abid.OrderStatusComment,SupplierName,acc.accountnumber,ISNULL(EQUIPNAME,'') as sparedetails, I.InvoiceId, I.RefNo,I.InvDate, VM.RequisitionTitle, VM.BidPODate,VM.totalpoamount from VW_qRFQListing_sql VM " +
            "left join VW_tblBudgetVActualComments Comm on Comm.CommBidId=VM.BIDID left join Add_tblSMDPoMasterBID Abid on Abid.BidID=VM.BidID " +
            "left join VW_tblSMDPOMaster mst on mst.poid=VM.poid " +
            "left join dbo.sql_tblSMDPRAccounts acc on acc.accountid=mst.accountid " +
            "left join ( Select I.InvoiceId,I.RefNo,I.InvDate,p.bidid from POS_Invoice_Payment_PO p  " +
            " INNER join POS_Invoice i on p.InvoiceId = I.InvoiceId and ISNULL(i.IsAdvPayment,0) = 0 )  as I on I.BidId = vm.BidID ";
        
        string WhereClause = " Where 1=1 "; 
       
//	if (ddlStatus.SelectedValue == "-2")
  //                  WhereClause = " Where BidStatusID=-2"; 

        if (ddlVessel.SelectedValue.Trim() == "0")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ALE", "alert('Please select vessel to continue.');", true);
            ddlVessel.Focus();
            return;
        }

            if (ddlVessel.SelectedValue != "")
        {
            if (WhereClause != "")
            {
                WhereClause = WhereClause + " and VM.ShipID='" + ddlVessel.SelectedValue + "'";
            }
            else
            {
                WhereClause = " where VM.ShipID='" + ddlVessel.SelectedValue + "'";
            }
        }
        else /// code for nwc vessels
        {
            if (Session["NWC"].ToString() == "Y")
            {
                WhereClause = WhereClause + " and VM.ShipID IN (SELECT vw.ShipID FROM VW_sql_tblSMDPRVessels vw WHERE vw.Company='NWC')";
            }
            else
            {
                WhereClause = WhereClause + " and VM.ShipID IN (SELECT vw.ShipID FROM VW_sql_tblSMDPRVessels vw WHERE vw.Company<>'NWC')";
            }
        }


        if (ddlPRType.SelectedIndex != 0)
        {
            if (WhereClause != "")
            {
                WhereClause = WhereClause + " and MST.prtype=" + ddlPRType.SelectedValue;
            }
            else
            {
                WhereClause = " where MST.Sprtype=" + ddlPRType.SelectedValue;
            }
        }
        if (txtPRNumber.Text.Trim() != "")
        {
           // Int16 test = 0;
            if (WhereClause != "")
            {
                WhereClause = WhereClause + " and (VM.prnum like '%" + txtPRNumber.Text.Trim() + "%'or isnull(VM.BidPoNum,'') like '%" + txtPRNumber.Text.Trim() + "%')";
                //if (txtPRNumber.Text.Trim().Length == 4 && Int16.TryParse(txtPRNumber.Text.Trim(), out test))
                //{
                //    WhereClause = WhereClause + " and prnum=" + txtPRNumber.Text.Trim();
                //}
                //else
                //{
                //    WhereClause = WhereClause + " and isnull(BidPoNum,'')='" + txtPRNumber.Text.Trim() + "'";
                //}
            }
            else
            {
                WhereClause = WhereClause + " and (VM.prnum like '%" + txtPRNumber.Text.Trim() + "%'or isnull(VM.BidPoNum,'') like '%" + txtPRNumber.Text.Trim() + "%')";
                //if (txtPRNumber.Text.Trim().Length == 4 && Int16.TryParse(txtPRNumber.Text.Trim(),out test))
                //{
                //    WhereClause = " where prnum=" + txtPRNumber.Text.Trim();
                //}
                //else
                //{
                //    WhereClause = " where isnull(BidPoNum,'')='" + txtPRNumber.Text.Trim() + "'";
                //}
            }
        }
        if (ddlStatus.SelectedIndex != 0)
        {
            if (WhereClause != "")
            {
                if (ddlStatus.SelectedValue == "-2")
                    WhereClause = WhereClause + " and BidStatusID in (-2,-1)";
                else
                    WhereClause = WhereClause + " and BidStatusID=" + ddlStatus.SelectedValue;
            }
            else
            {
                WhereClause = " where BidStatusID=" + ddlStatus.SelectedValue;
            }
        }
        else
        {
            WhereClause = WhereClause + " And BidStatusID>2 ";
        }

        if (txtFromDate.Text.Trim() != "")
        {
            if (WhereClause != "")
            {
                WhereClause = WhereClause + " and convert(datetime,BidStatusDate,103) >= convert(datetime,'" + txtFromDate.Text.Trim() + "',101)";
            }
            else
            {
                WhereClause = " where convert(datetime,BidStatusDate,103) >= convert(datetime,'" + txtFromDate.Text.Trim() + "',101)";
            }
        }

        if (txtToDate.Text.Trim() != "")
        {
            if (WhereClause != "")
            {
                WhereClause = WhereClause + " and convert(datetime,BidStatusDate,103) < convert(datetime,dateadd(dd, 1,'" + txtToDate.Text.Trim() + "'),101)";
            }
            else
            {
                WhereClause = " where convert(datetime,BidStatusDate,103) < convert(datetime,dateadd(dd, 1,'" + txtToDate.Text.Trim() + "'),101)";
            }
        }
        if (txtAcctCode.Text.Trim() != "")
        {
            if (WhereClause != "")
            {
                WhereClause = WhereClause + " and acc.accountnumber='" + txtAcctCode.Text.Trim() + "'";
            }
            else
            {
                WhereClause = " where acc.accountnumber='" + txtAcctCode.Text.Trim() + "'";
            }
        }
        
        if (WhereClause != "")
        {
            WhereClause = WhereClause + " and VM.prtype is not null";
        }
        else
        {
            WhereClause = " where VM.prtype is not null";
        }
        sql = sql + WhereClause;
        sql = sql + " order by vm.Poid desc";

//Response.Write(sql);

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
        ddlVessel.Items.Insert(0, new ListItem("Select", "0"));
    }
    public string getStatusColor(string StatusText)
    {
        switch (StatusText)
        {
            case "Receipt in Progress" :
                return "Red";         
            case "Order Placed":
                return "Green";       
            case "Receipt Complete" :
                return "Blue";      
            default:
                return "Black";         
        }
    }
    //------------------------------
    protected void ChkAllVess_CheckedChanged(object sender, EventArgs e)
    {
        //ddlVessel.IncludeInActive = ChkAllVess.Checked;   
        BindVesselDropdown();
    }
    protected void ChkNWC_CheckedChanged(object sender, EventArgs e)
    {
        //ddlVessel.IncludeNWC = chkNWC.Checked;
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
    protected void btnCancelPO_Click(object sender, EventArgs e)
    {
        if (Common.CastAsInt32(hfBidID.Value) <= 0)
        {
            lblMsg.Text = "Please select a Purchase Order.";
            return;
        }

       // ImageButton imgBtn = (ImageButton)sender;
        int BidId = Common.CastAsInt32(hfBidID.Value);

        if (GetBidStatus(Common.CastAsInt32(hfBidID.Value)) <0)
        {
            lblMsg.Text = "Unable to cancel this PO (Already canceled).";
            return;
        }

        if (GetBidStatus(Common.CastAsInt32(hfBidID.Value)) >= 6)
        {
            lblMsg.Text = "Unable to cancel this PO (Invoice Entered).";
            return;
        }

        Common.Set_Procedures("sp_NewPR_CancelPO");
        Common.Set_ParameterLength(3);
        Common.Set_Parameters(new MyParameter("@POId", BidId), new MyParameter("@UserName", Session["UserName"]), new MyParameter("@LoginId", Session["loginid"]));
        DataSet ds = new DataSet();
        if (Common.Execute_Procedures_IUD(ds))
        {
            BindRPRepeater();
            lblMsg.Text = "PO cancelled successfully.";
        }
        else
        {
            lblMsg.Text = "Unable to cancel PO. Error : " + Common.ErrMsg;
        }
    }
    protected void btnViewPO_OnClick(object sender, EventArgs e)
    {
        if (Common.CastAsInt32(hfBidID.Value) <= 0)
        {
            lblMsg.Text = "Please select a Purchase Order.";
            return; 
        }
        WriteSession();
        //Response.Redirect("VeiwRFQDetailsForApproval.aspx?BidId=" + hfBidID.Value + "");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "RFQ", "window.open('VeiwRFQDetailsForApproval.aspx?BidId=" + hfBidID.Value+"','','');", true);
    }
    protected void btnViewGoodsRcv_OnClick(object sender, EventArgs e)
    {
        if (Common.CastAsInt32(hfBidID.Value) <= 0)
        {
            lblMsg.Text = "Please select a Purchase Order.";
            return;
        }
        WriteSession();

        if (GetBidStatus(Common.CastAsInt32(hfBidID.Value)) < 0)
        {
            lblMsg.Text = "Can not recieve goods (PO is canceled).";
            return;
        }
        
        //Response.Redirect("ReceivePO.aspx?BidId=" + hfBidID.Value + "");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "rfq", "window.open('ReceivePO.aspx?BidId=" + hfBidID.Value + "');", true);
        hfBidID.Value = "0";
    }
    //------------------------------
    protected void btnInvoice_OnClick(object sender, EventArgs e)
    {
        if (Common.CastAsInt32(hfBidID.Value) <= 0)
        {
            lblMsg.Text = "Please select a Purchase Order.";
            return;
        }
        WriteSession();
        if (GetBidStatus(Common.CastAsInt32(hfBidID.Value)) > 4)
        {
            //Response.Redirect("InvoiceEntry.aspx?BidId=" + hfBidID.Value + "");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Invoice", "window.open('../Invoice/InvoiceEntry.aspx?BidId=" + hfBidID.Value + "');", true);
            hfBidID.Value = "0";
        }
        else
        {
            lblMsg.Text = "Can not enter invoice(Reciept not completed).";
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        //ChkAllVess.Checked = false;   
        ddlVessel.SelectedIndex = 0;
        ddlPRType.SelectedIndex = 0;
        txtPRNumber.Text = "";
        ddlStatus.SelectedIndex = 0;
        txtFromDate.Text = new DateTime(DateTime.Today.Year, 1, 1).ToString("dd-MMM-yyyy");
        txtToDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
        ClearSession(); 
        BindRPRepeater(); 
    }
    protected void BtnReload_OnClick(object sender, EventArgs e)
    {
        BindRPRepeater();
    }
    public string TrimRemarks(Object In)
    {
        string Data = In.ToString();
        if (Data.Length > 500)
        {
            return Data.Substring(0, 250) + "..."; 
        }
        else
        {
            return Data;
        }
    }
    public int GetBidStatus(int _BidID)
    {
        string sql = "select  BidStatusID  from vw_tblsmdpomasterbid where BidID="+_BidID+"";
        DataTable Dt = Common.Execute_Procedures_Select_ByQuery(sql);
        if (Dt != null)
        {
            if (Dt.Rows.Count > 0)
            {
                return Common.CastAsInt32(Dt.Rows[0][0]);
            }
            else
            {
                return 0;
            }
        }
        else
            return 0;
    }
    //------------------------------
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "tr", "SetLastFocus('dvscroll_Order');", true);
    }
}
