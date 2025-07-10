using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Configuration;

public partial class Modules_Purchase_Invoice_InvoiceSubmitforApproval : System.Web.UI.Page
{
    public AuthenticationManager authRecInv = new AuthenticationManager(0, 0, ObjectType.Page);
    public DataTable PoNos
    {
        set
        { ViewState["PoNos"] = value; }
        get
        {
            if (ViewState["PoNos"] == null)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("PONO", typeof(string)));
                dt.Columns.Add(new DataColumn("BIDID", typeof(string)));
                dt.Columns.Add(new DataColumn("OTHEROTHERINVID", typeof(string)));
                dt.Columns.Add(new DataColumn("OTHERREFNO", typeof(string)));
                ViewState["PoNos"] = dt;
            }

            return (DataTable)ViewState["PoNos"];
        }
    }
    Random r = new Random();
    public int UserId
    {
        get { return Convert.ToInt32(ViewState["UserId"]); }
        set { ViewState["UserId"] = value; }
    }
    public int InvoiceId
    {
        get { return Convert.ToInt32(ViewState["InvoiceId"]); }
        set { ViewState["InvoiceId"] = value; }
    }
    public int EnteredBy
    {
        get { return Convert.ToInt32(ViewState["EnteredBy"]); }
        set { ViewState["EnteredBy"] = value; }
    }
    public int ProcessedBy
    {
        get { return Convert.ToInt32(ViewState["ProcessedBy"]); }
        set { ViewState["ProcessedBy"] = value; }
    }
    public int PaymentBy
    {
        get { return Convert.ToInt32(ViewState["PaymentBy"]); }
        set { ViewState["PaymentBy"] = value; }
    }
    public int PoCount
    {
        get { return Convert.ToInt32(ViewState["PoCount"]); }
        set { ViewState["PoCount"] = value; }
    }
    public int Stage
    {
        get { return Convert.ToInt32(ViewState["Stage"]); }
        set { ViewState["Stage"] = value; }
    }

    public string Status
    {
        get { return Convert.ToString(ViewState["Status"]); }
        set { ViewState["Status"] = value; }
    }
    //public string Currency
    //{
    //    get { return Convert.ToString(ViewState["Currency"]); }
    //    set { ViewState["Currency"] = value; }
    //}
    //public string InvAmount
    //{
    //    get { return Convert.ToString(ViewState["InvAmount"]); }
    //    set { ViewState["InvAmount"] = value; }
    //}

    //public string InvDate
    //{
    //    get { return Convert.ToString(ViewState["InvDate"]); }
    //    set { ViewState["InvDate"] = value; }
    //}
    protected void Page_Load(object sender, EventArgs e)
    {
        ProjectCommon.SessionCheck();
        lbl_inv_Message.Text = "";
        if (!Page.IsPostBack)
        {
            UserId = Common.CastAsInt32(Session["loginid"]);
            InvoiceId = Common.CastAsInt32(Request.QueryString["InvoiceId"]);
           
           // bindPayment_ForwardToddl();
            BindInvoiceDetails();
        }


    }
   
    //protected void bindPayment_ForwardToddl()
    //{
    //    string SQL = "select (FirstName + ' ' + LastName ) AS UserName, LoginId  from dbo.usermaster where loginid in (select userid from pos_invoice_mgmt where payment=1) AND statusId='A' Order By UserName";

    //    DataTable dt1;//= Common.Execute_Procedures_Select_ByQuery("");
    //    //this.ddlPaymentForwardTo.DataValueField = "LoginId";
    //    //this.ddlPaymentForwardTo.DataTextField = "UserName";
    //    //this.ddlPaymentForwardTo.DataSource = dt1;
    //    //this.ddlPaymentForwardTo.DataBind();
    //    //ddlPaymentForwardTo.Items.Insert(0, new ListItem("< Select User >", "0"));

    //    SQL = "select (FirstName + ' ' + LastName ) AS UserName, LoginId from dbo.usermaster where loginid in (select userid from pos_invoice_mgmt where Approval=1) AND statusId='A' Order By UserName";
    //    dt1 = Common.Execute_Procedures_Select_ByQuery(SQL);
    //    this.ddlVerifyForwardTo.DataValueField = "LoginId";
    //    this.ddlVerifyForwardTo.DataTextField = "UserName";
    //    this.ddlVerifyForwardTo.DataSource = dt1;
    //    this.ddlVerifyForwardTo.DataBind();
    //    ddlVerifyForwardTo.Items.Insert(0, new ListItem("< Select User >", "0"));

    //    SQL = "select (FirstName + ' ' + LastName ) AS UserName, LoginId from dbo.usermaster where loginid in (select userid from pos_invoice_mgmt where Verification=1) AND statusId='A' Order By UserName";
    //    dt1 = Common.Execute_Procedures_Select_ByQuery(SQL);
    //    this.ddlApproval2.DataValueField = "LoginId";
    //    this.ddlApproval2.DataTextField = "UserName";
    //    this.ddlApproval2.DataSource = dt1;
    //    this.ddlApproval2.DataBind();
    //    ddlApproval2.Items.Insert(0, new ListItem("< Select User >", "0"));

    //    SQL = "SELECT (FirstName + ' ' + LastName ) AS UserName,LoginId from dbo.usermaster where loginid in (select userid from pos_invoice_mgmt where Approval3=1) AND statusId='A' Order By UserName";
    //    dt1 = Common.Execute_Procedures_Select_ByQuery(SQL);
    //    this.ddlApproval3.DataValueField = "LoginId";
    //    this.ddlApproval3.DataTextField = "UserName";
    //    this.ddlApproval3.DataSource = dt1;
    //    this.ddlApproval3.DataBind();
    //    ddlApproval3.Items.Insert(0, new ListItem("< Select User >", "0"));

    //    SQL = "SELECT (FirstName + ' ' + LastName ) AS UserName,LoginId from dbo.usermaster where loginid in (select userid from pos_invoice_mgmt where Approval4=1) AND statusId='A' Order By UserName";
    //    dt1 = Common.Execute_Procedures_Select_ByQuery(SQL);
    //    this.ddlApproval4.DataValueField = "LoginId";
    //    this.ddlApproval4.DataTextField = "UserName";
    //    this.ddlApproval4.DataSource = dt1;
    //    this.ddlApproval4.DataBind();
    //    ddlApproval4.Items.Insert(0, new ListItem("< Select User >", "0"));

    //    SQL = "select (FirstName + ' ' + LastName ) AS UserName, LoginId from dbo.usermaster where loginid in (select userid from pos_invoice_mgmt where Payment=1) AND statusId='A' Order By UserName";
    //    dt1 = Common.Execute_Procedures_Select_ByQuery(SQL);
    //    this.ddlAccountUser.DataValueField = "LoginId";
    //    this.ddlAccountUser.DataTextField = "UserName";
    //    this.ddlAccountUser.DataSource = dt1;
    //    this.ddlAccountUser.DataBind();
    //    ddlAccountUser.Items.Insert(0, new ListItem("< Select User >", "0"));
    //    DataTable dtAccountUser = Common.Execute_Procedures_Select_ByQuery("exec DBO.POS_INV_getPaymentUser " + InvoiceId);
    //    if (dtAccountUser.Rows.Count > 0)
    //    {
    //        ddlAccountUser.SelectedValue = dtAccountUser.Rows[0][0].ToString();
    //    }

    //}
    protected void ShowDouments()
    {
        string DeleteAllowed = "N";
        DeleteAllowed = (Stage <= 1) ? "Y" : "N";
        string sql2 = "SELECT *,'" + DeleteAllowed + "' as DeleteAllowed FROM DBO.POS_Invoice_Documents WHERE INVOICEID=" + InvoiceId;
        Repeater1.DataSource = Common.Execute_Procedures_Select_ByQuery(sql2);
        Repeater1.DataBind();
    }

    protected void BindInvoiceDetails()
    {
        //-------------------------
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("select * from dbo.vw_Pos_Invoices_001 where InvoiceId=" + InvoiceId);
        if (dt.Rows.Count > 0)
        {
            lblRefNo.Text = dt.Rows[0]["RefNo"].ToString();
            lblSupplier.Text = dt.Rows[0]["Vendor"].ToString();
            lblVendorCode.Text = dt.Rows[0]["VendorCode"].ToString();
            lbl_InvNo.Text = dt.Rows[0]["InvNo"].ToString();
            lbl_InvDate.Text = Common.ToDateString(dt.Rows[0]["InvDate"]);
            lbl_DueDate.Text = Common.ToDateString(dt.Rows[0]["DueDate"]);
            lbl_InvAmount.Text = dt.Rows[0]["InvoiceAmount"].ToString();
            
            lblCurrency.Text = dt.Rows[0]["Currency"].ToString();
            lbl_Vessel.Text = dt.Rows[0]["invVesselCode"].ToString();
            Status = dt.Rows[0]["Status"].ToString();
            Stage = Common.CastAsInt32(dt.Rows[0]["Stage"]);
           // hdnIn.Text = dt.Rows[0]["InvNo"].ToString();
           // InvDate = Common.ToDateString(dt.Rows[0]["InvDate"]);
          //  InvAmount = dt.Rows[0]["InvoiceAmount"].ToString();
          //  Currency = dt.Rows[0]["Currency"].ToString();
          //  lbl_Vessel.Text = dt.Rows[0]["invVesselCode"].ToString();
            //lblPoNo.Text = dt.Rows[0]["PONo"].ToString();
            //txtPoNo.Text = lblPoNo.Text;
            
            string sql = "SELECT * FROM DBO.VW_iNVOICE_POLINK WHERE INVOICEID=" + InvoiceId;
             DataTable dtPoNos = Common.Execute_Procedures_Select_ByQuery(sql);
           
            PoCount = dtPoNos.Rows.Count;
            

            


            Stage = Common.CastAsInt32(dt.Rows[0]["Stage"]);

            dt = Common.Execute_Procedures_Select_ByQuery("select EntertedBy,ApprovalFwdTo,VerificationFwdTo,PaidFwdTo,CancelledBy,ISNULL(CLSINVOICE,0) As CLSINVOICE from dbo.Pos_Invoice where InvoiceId=" + InvoiceId);
            int EnteredBy = Common.CastAsInt32(dt.Rows[0]["EntertedBy"]);
            int ApprovalFwdTo = Common.CastAsInt32(dt.Rows[0]["ApprovalFwdTo"]);
            int VerificationFwdTo = Common.CastAsInt32(dt.Rows[0]["VerificationFwdTo"]);

            int PaidFwdTo = Common.CastAsInt32(dt.Rows[0]["PaidFwdTo"]);
            bool IsCloseInvoice = false;
            IsCloseInvoice = Convert.ToBoolean(dt.Rows[0]["CLSINVOICE"]);
           
            //if (pnl_Approval.Visible)
            //{
            //    try
            //    {
            //        if (IsCloseInvoice)
            //            ddlAccountUser.SelectedValue = "52";
            //        else
            //            ddlAccountUser.SelectedIndex = 0;

            //    }
            //    catch { }
            //}

            //if (Status.Trim() != "Cancelled")
            //{
            //    switch (Stage)
            //    {
                    
            //        case 1:
            //            if (ApprovalFwdTo == UserId)
            //            {
            //                pnl_Approval.Visible = true;
            //                txt_InvAmount.Text = lbl_InvAmount.Text;
            //                lblApprovedUSDAmount.Text = getUSD(lblCurrency.Text, Common.CastAsDecimal(txt_InvAmount.Text), DateTime.Parse(lbl_InvDate.Text)).ToString();
            //            }
                        
            //            break;
                    
                   
            //        default:
            //            break;
            //    }
            //}

            //if (pnl_Approval.Visible && VerificationFwdTo == 0)
            //{
            //    DataTable dt1 = Common.Execute_Procedures_Select_ByQuery("select AcctOfficer from dbo.vessel where vesselid in ( SELECT vesselid FROM dbo.vw_Pos_Invoices WHERE [InvoiceId] = " + InvoiceId + " )");
            //    try
            //    { ddlVerifyForwardTo.SelectedValue = dt1.Rows[0][0].ToString(); }
            //    catch { }
            //}


            //---------
            string sql1 = "SELECT PONO,BidId, " +
                        "CONVERT(VARCHAR,(SELECT TOP 1 INVOICEID FROM DBO.POS_Invoice_Payment_PO P WHERE P.BidId=I.BIDID AND ISNULL(P.BidId,0)<>0 AND  P.InvoiceId<>" + InvoiceId + ")) AS OTHEROTHERINVID, " +
                       "(SELECT REFNO FROM DBO.POS_Invoice I1 WHERE I1.INVOICEID IN (SELECT TOP 1 INVOICEID FROM DBO.POS_Invoice_Payment_PO P WHERE P.BidId=I.BIDID AND ISNULL(P.BidId,0)<>0 AND P.InvoiceId<>" + InvoiceId + ")) AS OTHERREFNO " +
                       "FROM " +
                       "DBO.POS_Invoice_Payment_PO I " +
                       "WHERE INVOICEID=" + InvoiceId;
            PoNos = Common.Execute_Procedures_Select_ByQuery(sql1);
            
            //---------
            ShowDouments();
            //---------
            ShowStageDetails();

        }
        


    }

    //public decimal getUSD(string Curr, decimal Amount, DateTime dtFor)
    //{
    //    decimal xchangerate = 0;
    //    DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT TOP 1  EXC_RATE FROM DBO.XCHANGEDAILY WHERE FOR_CURR='" + Curr + "' AND RATEDATE <='" + dtFor.ToString("dd-MMM-yyyy") + "' ORDER BY RATEDATE DESC");
    //    if (dt.Rows.Count > 0)
    //        xchangerate = Common.CastAsDecimal(dt.Rows[0][0]);

    //    lblExchRate.Text = xchangerate.ToString();

    //    if (xchangerate != 0)
    //        return Math.Round(Amount / xchangerate, 2);
    //    else
    //        return 0;
    //}
    public void setImageUrl(Image I, int Mode)
    {
        I.Visible = true;
        switch (Mode)
        {
            case 0: // DO NOT WORRY
                I.Visible = false;
                break;
            case 1: // STAGE - NOR REQUIRED 
                I.ImageUrl = "~/Modules/HRD/Images/gray_circle.png";
                break;
            case 2: // DONE
                I.ImageUrl = "~/Modules/HRD/Images/green_circle.gif";
                break;
            case 3: // PENDING
                I.ImageUrl = "~/Modules/HRD/Images/red_circle.png";
                break;
            default:
                break;
        }
    }
    public void setStageByOn(DataTable dt, int Mode, Label by, Label on, Label Rem)
    {
        int StageMode = getStageMode(dt, Mode);
        if (StageMode >= 2)
        {
            DataView dv = dt.DefaultView;
            dv.RowFilter = "ApprovalLevel=" + Mode;
            DataTable dt1 = dv.ToTable();

            if (StageMode == 2)
            {
                on.Text = Common.ToDateString(dt1.Rows[0]["ApprovedOn"]);
                Rem.Text = dt1.Rows[0]["Comments"].ToString();
            }
            else
            {
                ((ImageButton)this.FindControl("btnApp" + Mode)).Visible = true && (ProcessedBy == UserId || 1 == UserId);
            }
            by.Text = ProjectCommon.getUserNameByID(dt1.Rows[0]["AppUserId"].ToString());

        }
    }
    public int getStageMode(DataTable dt, int Mode)
    {
        DataView dv = dt.DefaultView;
        dv.RowFilter = "ApprovalLevel=" + Mode;
        DataTable dt1 = dv.ToTable();

        if (dt1.Rows.Count <= 0)
            return 1;
        else
        {
            if (Common.CastAsInt32(dt1.Rows[0]["AppUserId"]) > 0)
                return Convert.IsDBNull(dt1.Rows[0]["ApprovedOn"]) ? 3 : 2;
            else
                return 1;
        }

    }
    public void ShowStageDetails()
    {
        string Sql = "SELECT (SELECT FirstName + ' ' AS LASTNAME FROM  dbo.UserMaster WHERE LoginId =  I.EntertedBy) AS EnteredBy,I.EntertedBy as EntertedById, EnteredOn,EntryComments, " +
                     "ApprovalFwdTo,(SELECT FirstName + ' ' AS LASTNAME FROM  dbo.UserMaster WHERE LoginId =  I.ApprovalFwdTo) AS ApprovalBy,I.ApprovalFwdTo, ApprovalOn, ApprovalComments,  " +
                     "VerificationFwdTo,(SELECT FirstName + ' ' AS LASTNAME FROM  dbo.UserMaster WHERE LoginId =  I.VerificationFwdTo) AS VerifiedBy,VerficationOn, VerificationComments,  " +
                     "PaidFwdTo,(SELECT FirstName + ' ' AS LASTNAME FROM  dbo.UserMaster WHERE LoginId =  I.PaidFwdTo) AS PaidBY, PaidOn,PaidComments, Stage, Status  " +
                     "FROM POS_Invoice I WHERE InvoiceId =" + InvoiceId;
        DataTable dtStages = Common.Execute_Procedures_Select_ByQuery(Sql);

        int Stage = Common.CastAsInt32(dtStages.Rows[0]["Stage"]);
        string Status = dtStages.Rows[0]["Status"].ToString();

        lblEnteredBy.Text = dtStages.Rows[0]["EnteredBy"].ToString();
        lblEnteredOn.Text = Common.ToDateString(dtStages.Rows[0]["EnteredOn"]);
        lblEntryComments.Text = dtStages.Rows[0]["EntryComments"].ToString();

        EnteredBy = Common.CastAsInt32(dtStages.Rows[0]["EntertedById"]);
        ProcessedBy = Common.CastAsInt32(dtStages.Rows[0]["ApprovalFwdTo"]);
        PaymentBy = Common.CastAsInt32(dtStages.Rows[0]["PaidFwdTo"]);

        lblProcessingTo.Text = dtStages.Rows[0]["ApprovalBy"].ToString();
        lblProcessingOn.Text = Common.ToDateString(dtStages.Rows[0]["ApprovalOn"]);
        lblProcessingComments.Text = dtStages.Rows[0]["ApprovalComments"].ToString();

        btnPropose.Visible = (lblProcessingOn.Text.Trim() == "" && (EnteredBy == UserId || 1 == UserId));
        btnPayment.Visible = (Status == "U" && (PaymentBy == UserId || 1 == UserId));

        lblPaymentBy.Text = dtStages.Rows[0]["PaidBY"].ToString();
        lblPaymentOn.Text = Common.ToDateString(dtStages.Rows[0]["PaidOn"]);
        lblPaymentComments.Text = dtStages.Rows[0]["PaidComments"].ToString();

        int EntryMode = 0;
        int ProcessingMode = 0;
        int Approval1Mode = 0;
        int Approval2Mode = 0;
        //int Approval3Mode = 0;
        //int Approval4Mode = 0;
        int ApprovalFinalMode = 0;
        int PaymentMode = 0;

        // 0: DO NOT WORRY
        // 1: STAGE - NOR REQUIRED 
        // 2: DONE
        // 3: PENDING

        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.POS_INVOICE_APPROVALS WHERE INVOICEID=" + InvoiceId);

        EntryMode = 2;

        ProcessingMode = (Convert.IsDBNull(dtStages.Rows[0]["ApprovalFwdTo"])) ? 0 : ((Convert.IsDBNull(dtStages.Rows[0]["ApprovalOn"])) ? 3 : 2);

        if (ProcessingMode == 2)
        {
            Approval1Mode = getStageMode(dt, 1);
            Approval2Mode = getStageMode(dt, 2);
            //Approval3Mode = getStageMode(dt, 3);
            //Approval4Mode = getStageMode(dt, 4);

            ApprovalFinalMode = (Convert.IsDBNull(dtStages.Rows[0]["VerficationOn"])) ? 3 : 2;

            if (ApprovalFinalMode == 2)
            {
                PaymentMode = (Status == "P") ? 2 : 3;
            }
        }


        //switch (Stage)
        //{
        //    case 0: // 
        //        EntryMode = 2;
        //        ProcessingMode = (Convert.IsDBNull(dtStages.Rows[0]["ApprovalOn"])) ? 3 : 2;
        //        break;
        //    case 1:
        //        EntryMode = 2;
        //        ProcessingMode = (Convert.IsDBNull(dtStages.Rows[0]["ApprovalOn"])) ? 3 : 2;
        //        if (ProcessingMode == 2)
        //        {
        //            Approval1Mode = getStageMode(dt, 1);
        //            Approval2Mode = getStageMode(dt, 2);
        //            Approval3Mode = getStageMode(dt, 3);
        //            Approval4Mode = getStageMode(dt, 4);
        //        }
        //        break;
        //    case 2:
        //        EntryMode = 2;
        //        ProcessingMode = (Convert.IsDBNull(dtStages.Rows[0]["ApprovalOn"])) ? 3 : 2;
        //        if (ProcessingMode == 2)
        //        {
        //            Approval1Mode = getStageMode(dt, 1);
        //            Approval2Mode = getStageMode(dt, 2);
        //            Approval3Mode = getStageMode(dt, 3);
        //            Approval4Mode = getStageMode(dt, 4);
        //            PaymentMode = (Status == "P") ? 3 : 2;
        //        }
        //        break;
        //    case 3:
        //        EntryMode = 2;
        //        ProcessingMode =2;
        //        Approval1Mode = getStageMode(dt, 1);
        //        Approval2Mode = getStageMode(dt, 2);
        //        Approval3Mode = getStageMode(dt, 3);
        //        Approval4Mode = getStageMode(dt, 4);
        //        PaymentMode = (Status == "P") ? 3 : 2;
        //        break;
        //    default:
        //        break;
        //}

        //lblApproval1By.Text = dtStages.Rows[0]["VerifiedBy"].ToString();
        //lblApproval1On.Text = Common.ToDateString(dtStages.Rows[0]["VerficationOn"]);
        //lblApproval1Comments.Text = dtStages.Rows[0]["VerificationComments"].ToString();

        setImageUrl(imgEntry, EntryMode);
        setImageUrl(imgProcessing, ProcessingMode);

        setImageUrl(imgApproval1, Approval1Mode);
        setStageByOn(dt, 1, lblApproval1By, lblApproval1On, lblApproval1Comments);
        setImageUrl(imgApproval2, Approval2Mode);
        setStageByOn(dt, 2, lblApproval2By, lblApproval2On, lblApproval2Comments);
        //setImageUrl(imgApproval3, Approval3Mode);
        //setStageByOn(dt, 3, lblApproval3By, lblApproval3On, lblApproval3Comments);
        //setImageUrl(imgApproval4, Approval4Mode);
        //setStageByOn(dt, 4, lblApproval4By, lblApproval4On, lblApproval4Comments);

        setImageUrl(imgPayment, PaymentMode);
       

        if (Stage == 2)
        {

            int App1User = GetAprpovalUserId(dt, 1);
            int App2User = GetAprpovalUserId(dt, 2);
            //int App3User = GetAprpovalUserId(dt, 3);
            //int App4User = GetAprpovalUserId(dt, 4);

            DateTime? App1Date = GetApprovalDate(dt, 1);
            DateTime? App2Date = GetApprovalDate(dt, 2);
            //DateTime? App3Date = GetApprovalDate(dt, 3);
            //DateTime? App4Date = GetApprovalDate(dt, 4);

            DataTable dtAccountUser = Common.Execute_Procedures_Select_ByQuery("exec DBO.POS_INV_getPaymentUser " + InvoiceId);

            if (!pnlApproval.Visible)
            {
                pnlApproval.Visible = (App1User > 0 && App1User == UserId && App1Date == null);
                if (pnlApproval.Visible)
                {
                    lblApprovalLevel.Text = "Approval 1";
                    hfdAppLevel.Value = "1";
                    hfdAppUserId.Value = App1User.ToString();

                    //hfdPaymentUserId.Value = dtAccountUser.Rows[0][0].ToString();
                    //lblForwardedToPaymentNew.Text = dtAccountUser.Rows[0][1].ToString();
                }
            }
            if (!pnlApproval.Visible)
            {
                pnlApproval.Visible = (App2User > 0 && App2User == UserId && App2Date == null);
                if (pnlApproval.Visible)
                {
                    lblApprovalLevel.Text = "Approval 2";
                    hfdAppLevel.Value = "2";
                    hfdAppUserId.Value = App2User.ToString();

                    //hfdPaymentUserId.Value = dtAccountUser.Rows[0][0].ToString();
                    //lblForwardedToPaymentNew.Text = dtAccountUser.Rows[0][1].ToString();
                }
            }
            //if (!pnlApproval.Visible)
            //{
            //    pnlApproval.Visible = (App3User > 0 && App3User == UserId && App3Date == null);
            //    if (pnlApproval.Visible)
            //    {
            //        lblApprovalLevel.Text = "Approval 3";
            //        hfdAppLevel.Value = "3";
            //        hfdAppUserId.Value = App3User.ToString();

            //        //hfdPaymentUserId.Value = dtAccountUser.Rows[0][0].ToString();
            //        //lblForwardedToPaymentNew.Text = dtAccountUser.Rows[0][1].ToString();
            //    }
            //}
            //if (!pnlApproval.Visible)
            //{
            //    pnlApproval.Visible = (App4User > 0 && App4User == UserId && App4Date == null);
            //    if (pnlApproval.Visible)
            //    {
            //        lblApprovalLevel.Text = "Approval 4";
            //        hfdAppLevel.Value = "4";
            //        hfdAppUserId.Value = App4User.ToString();

            //        //hfdPaymentUserId.Value = dtAccountUser.Rows[0][0].ToString();
            //        //lblForwardedToPaymentNew.Text = dtAccountUser.Rows[0][1].ToString();
            //    }

            //}

        }
        //if (Stage >= 2)
        //{
        //    btn_UpdateFile.Visible = false;
        //    btn_UpdatePONOView.Visible = false;

        //}
        //---------------------------------------------

        string sql = "select  CASE I.[StageId] WHEN 0 THEN 'Entry' WHEN 1 THEN 'Approval' WHEN 2 THEN 'Acct. Verify' WHEN 3 THEN 'Payment' ELSE 'NA' END AS Stage,UserName,Comments,CommentsOn from POS_Invoice_StageComments I where invoiceid=" + InvoiceId;
        rptComments.DataSource = Common.Execute_Procedures_Select_ByQuery(sql);
        rptComments.DataBind();
        //---------------------------------------------
    }
    public int GetAprpovalUserId(DataTable dt, int AppLevel)
    {
        int ret = 0;
        DataView dv = dt.DefaultView;
        dv.RowFilter = "ApprovalLevel=" + AppLevel;
        DataTable dt1 = dv.ToTable();

        if (dt1.Rows.Count <= 0)
            return 0;
        else
        {
            return Common.CastAsInt32(dt1.Rows[0]["APPUSERID"]);
        }
    }
    public DateTime? GetApprovalDate(DataTable dt, int AppLevel)
    {
        int ret = 0;
        DataView dv = dt.DefaultView;
        dv.RowFilter = "ApprovalLevel=" + AppLevel;
        DataTable dt1 = dv.ToTable();

        if (dt1.Rows.Count <= 0)
            return null;
        else
        {
            if (Convert.IsDBNull(dt1.Rows[0]["ApprovedOn"]))
                return null;
            else
                return DateTime.Parse(dt1.Rows[0]["ApprovedOn"].ToString());
        }
    }
    protected void btn_VerifySave_Click(object sender, EventArgs e)
    {
        try
        {
            Common.Execute_Procedures_Select_ByQuery("UPDATE dbo.Pos_Invoice SET VerficationOn=getdate(),Stage=3,StageComments='" + txtVerifyComments.Text.Trim().Replace("'", "`") + "',VerificationComments='" + txtVerifyComments.Text.Trim().Replace("'", "`") + "' where InvoiceId=" + InvoiceId);
            //Common.Execute_Procedures_Select_ByQuery("UPDATE dbo.Pos_Invoice SET VerficationOn=getdate(),PaidFwdTo=" + ddlPaymentForwardTo.SelectedValue + ",Stage=3,StageComments='" + txtVerifyComments.Text.Trim().Replace("'", "`") + "',VerificationComments='" + txtVerifyComments.Text.Trim().Replace("'", "`") + "' where InvoiceId=" + InvoiceId);
            lbl_inv_Message.Text = "Verified Successfully.";
            btn_VerifySave.Visible = false;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "", "Refresh();", true);
        }
        catch (Exception ex)
        { lbl_inv_Message.Text = "Unable to Verify. Error :" + ex.Message; }

    }
    
    //protected void ApprovalAmount_OnTextChanged(object sender, EventArgs e)
    //{
    //    lblApprovedUSDAmount.Text = getUSD(lblCurrency.Text, Common.CastAsDecimal(txt_InvAmount.Text), DateTime.Parse(lbl_InvDate.Text)).ToString();
    //}
    
    protected void btnApprovalSave_Click(object sender, EventArgs e)
    {
        if (txtApprovalRemarks.Text.Trim() == "")
        {
            lbl_inv_Message.Text = "Please enter comments to continue.";
            return;
        }

        int AppUserId = Common.CastAsInt32(hfdAppUserId.Value);
        //int PaymentUserId = Common.CastAsInt32(hfdPaymentUserId.Value);
        int Mode = Common.CastAsInt32(hfdAppLevel.Value);
        try
        {
            Common.Execute_Procedures_Select_ByQuery("exec dbo.POS_INVOICE_STAGE_APPROVAL " + InvoiceId + "," + AppUserId + "," + Mode + ",'" + txtApprovalRemarks.Text.Trim().Replace("'", "`") + "'");
            btnApprovalSave.Visible = false;
            //pnlApproval.Visible = false;
            ShowStageDetails();
        }
        catch
        {


        }
    }
    protected void btnSaveStageComments_Click(object sender, EventArgs e)
    {
        if (txtStageComments.Text.Trim() == "")
        {
            lbl_inv_Message.Text = "Please enter comments to continue.";
            return;
        }
        try
        {
            Common.Execute_Procedures_Select_ByQuery("exec dbo.POS_InvoiceStageComments " + InvoiceId + ",'" + Session["UserFullName"].ToString() + "','" + txtStageComments.Text.Trim().Replace("'", "`") + "'");
            lbl_inv_Message.Text = "Comments saved successfully.";

        }
        catch { }


    }
    protected void btn_AddNotes_Click(object sender, EventArgs e)
    {
        dvNotes.Visible = true;
    }
    protected void btnCloseStageComments_Click(object sender, EventArgs e)
    {
        ShowStageDetails();
        dvNotes.Visible = false;
    }
    protected void btn_AddDocuments_Click(object sender, EventArgs e)
    {
        dvDocument.Visible = true;
        frmUpload1.Attributes.Add("src", "UploadNewDocument.aspx?InvoiceId=" + InvoiceId);
    }
    protected void btn_DocumentClose_Click(object sender, EventArgs e)
    {
        dvDocument.Visible = false;
        BindInvoiceDetails();
        ShowDouments();
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        int Pk = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        Common.Execute_Procedures_Select_ByQuery("DELETE FROM POS_Invoice_Documents WHERE PK=" + Pk);
        ShowDouments();
    }
    protected void btnUpdateUserClick(object sender, EventArgs e)
    {
        dvUpdateUser.Visible = true;
        int Mode = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        ViewState["UpdateUserMode"] = Mode;
        switch (Mode)
        {
            case 1: // Entry
                break;
            case 2: // Processing
                string SQL = "select (FirstName + ' ' + LastName ) AS UserName, LoginId from dbo.usermaster where statusId='A' Order By UserName";
                ddlNewUser.DataSource = Common.Execute_Procedures_Select_ByQuery(SQL);
                break;
            case 3: // Approval1
                string SQL1 = "select (FirstName + ' ' + LastName ) AS UserName, LoginId from dbo.usermaster where loginid in (select userid from pos_invoice_mgmt where Approval=1) AND statusId='A' Order By UserName";
                ddlNewUser.DataSource = Common.Execute_Procedures_Select_ByQuery(SQL1);
                break;
            case 4: // Approval2
                string SQL2 = "select (FirstName + ' ' + LastName ) AS UserName, LoginId from dbo.usermaster where loginid in (select userid from pos_invoice_mgmt where Verification=1) AND statusId='A' Order By UserName";
                ddlNewUser.DataSource = Common.Execute_Procedures_Select_ByQuery(SQL2);
                break;
            case 5: // Approval3
                string SQL3 = "SELECT FirstName + ' ' + MiddleName + ' ' + FamilyName AS UserName,USERID as LoginId FROM DBO.Hr_PersonalDetails WHERE DRC IS NULL AND POSITION IN(4,89)";
                ddlNewUser.DataSource = Common.Execute_Procedures_Select_ByQuery(SQL3);
                break;
            case 6: // Approval4
                string SQL4 = "SELECT FirstName + ' ' + MiddleName + ' ' + FamilyName AS UserName,USERID as LoginId FROM DBO.Hr_PersonalDetails WHERE DRC IS NULL AND POSITION=1";
                ddlNewUser.DataSource = Common.Execute_Procedures_Select_ByQuery(SQL4);
                break;
            case 7: // Payment
                string SQL5 = "select (FirstName + ' ' + LastName ) AS UserName, LoginId from dbo.usermaster where loginid in (select userid from pos_invoice_mgmt where Payment=1) AND statusId='A' Order By UserName";
                ddlNewUser.DataSource = Common.Execute_Procedures_Select_ByQuery(SQL5);
                break;
        }

        ddlNewUser.DataValueField = "LoginId";
        ddlNewUser.DataTextField = "UserName";
        ddlNewUser.DataBind();
        ddlNewUser.Items.Insert(0, new ListItem("< Select User >", ""));
    }
    protected void btnUpdateUserSave_Click(object sender, EventArgs e)
    {
        if (ddlNewUser.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "fafasf", "alert('Please select New User.');", true);
            return;
        }

        switch (Common.CastAsInt32(ViewState["UpdateUserMode"]))
        {
            case 1: // Entry
                break;
            case 2: // Processing
                string SQL = "UPDATE DBO.POS_Invoice SET ApprovalFwdTo=" + ddlNewUser.SelectedValue + " WHERE INVOICEID=" + InvoiceId;
                Common.Execute_Procedures_Select_ByQuery(SQL);
                break;
            case 3: // Approval1
                string SQL1 = "UPDATE DBO.POS_Invoice_Approvals SET AppUserId=" + ddlNewUser.SelectedValue + " WHERE INVOICEID=" + InvoiceId + " AND ApprovalLevel=1";
                Common.Execute_Procedures_Select_ByQuery(SQL1);
                break;
            case 4: // Approval2
                string SQL2 = "UPDATE DBO.POS_Invoice_Approvals SET AppUserId=" + ddlNewUser.SelectedValue + " WHERE INVOICEID=" + InvoiceId + " AND ApprovalLevel=2";
                Common.Execute_Procedures_Select_ByQuery(SQL2);
                break;
            case 5: // Approval3
                string SQL3 = "UPDATE DBO.POS_Invoice_Approvals SET AppUserId=" + ddlNewUser.SelectedValue + " WHERE INVOICEID=" + InvoiceId + " AND ApprovalLevel=3";
                Common.Execute_Procedures_Select_ByQuery(SQL3);
                break;
            case 6: // Approval4
                string SQL4 = "UPDATE DBO.POS_Invoice_Approvals SET AppUserId=" + ddlNewUser.SelectedValue + " WHERE INVOICEID=" + InvoiceId + " AND ApprovalLevel=4";
                Common.Execute_Procedures_Select_ByQuery(SQL4);
                break;
            case 7: // Payment
                string SQL5 = "UPDATE DBO.POS_Invoice SET PaidFwdTo=" + ddlNewUser.SelectedValue + " WHERE INVOICEID=" + InvoiceId;
                Common.Execute_Procedures_Select_ByQuery(SQL5);
                break;
        }
        ShowStageDetails();
        dvUpdateUser.Visible = false;
    }
    protected void btnUpdateUserCancel_Click(object sender, EventArgs e)
    {
        dvUpdateUser.Visible = false;
    }
    protected void btnDownload_Click(object sender, EventArgs e)
    {
        int Pk = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT AttachmentName,Attachment FROM POS_Invoice_Documents WHERE PK=" + Pk);
        if (dt.Rows.Count > 0)
        {
            string FileName = dt.Rows[0]["AttachmentName"].ToString();
            if (FileName.Trim() != "")
            {
                byte[] buff = (byte[])dt.Rows[0]["Attachment"];
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileName);
                Response.BinaryWrite(buff);
                Response.Flush();
                Response.End();
            }
        }
    }
}