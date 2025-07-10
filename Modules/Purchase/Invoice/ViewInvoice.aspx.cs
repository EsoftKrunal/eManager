using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Configuration;
using DocumentFormat.OpenXml.ExtendedProperties;
using System.Net.Mime;
using DocumentFormat.OpenXml.VariantTypes;
using System.Diagnostics.Contracts;
using System.Collections;


public partial class Invoice_ViewInvoice : System.Web.UI.Page
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
                dt.Columns.Add(new DataColumn("BidStatusID", typeof(int))); 
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
    public int Stage
    {
        get { return Convert.ToInt32(ViewState["Stage"]); }
        set { ViewState["Stage"] = value; }
    }
    public int PoCount
    {
        get { return Convert.ToInt32(ViewState["PoCount"]); }
        set { ViewState["PoCount"] = value; }
    }

    public string Status
    {
        get { return Convert.ToString(ViewState["Status"]); }
        set { ViewState["Status"] = value; }
    }

    public int SupplierId
    {
        get { return Convert.ToInt32(ViewState["SupplierId"]); }
        set { ViewState["SupplierId"] = value; }
    }

    public int NonPoId
    {
        get { return Convert.ToInt32(ViewState["NonPoId"]); }
        set { ViewState["NonPoId"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //---------------------------------------
        this.Page.Form.Enctype = "multipart/form-data";
        ProjectCommon.SessionCheck();

        authRecInv = new AuthenticationManager(1072, int.Parse(Session["loginid"].ToString()), ObjectType.Page);
        
       
        lbl_inv_Message.Text = "";
        lbl_popinv_Message.Text = "";
       
        if (!Page.IsPostBack)
        {
            //---------------------------------------
            string appname = ConfigurationManager.AppSettings["AppName"].ToString();
            string[] Files = Directory.GetFiles(Server.MapPath("/" + appname + "/EMANAGERBLOB/Purchase/Invoice/"));
            //string[] Files = Directory.GetFiles(Server.MapPath("/EMANAGERBLOB/Purchase/Invoice/"));
            foreach (string f in Files)
                //{
                //    try { File.Delete(f); }
                //    catch { }
                //}
                UserId = Common.CastAsInt32(Session["loginid"]);
            InvoiceId = Common.CastAsInt32(Request.QueryString["InvoiceId"]);
            bindPayment_ForwardToddl();
            BindInvoiceDetails();
        }
    }
    protected void BindInvoiceDetails()
    {
        //btnClip.Visible = false;
        btnClipText.Visible = false;
       // lbkExport.Visible = false;
        lnkPrint.Visible = false;

        //btnSubmitInvoice.Visible = false;
        //-------------------------
        DataTable dt101 = Common.Execute_Procedures_Select_ByQuery("SELECT Entry,Payment FROM POS_Invoice_mgmt where USERID=" + UserId);
        if (dt101.Rows.Count > 0 && dt101.Rows[0]["Payment"].ToString() == "True")
        {
            lnkPrint.Visible = true;
            //lbkExport.Visible = true;
        }
        
        //-------------------------
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("select * from dbo.vw_Pos_Invoices_001 where InvoiceId=" + InvoiceId);
        if (dt.Rows.Count > 0)
        {
            lblRefNo.Text = dt.Rows[0]["RefNo"].ToString();
            lblSupplier.Text = dt.Rows[0]["Vendor"].ToString();
            SupplierId = Convert.ToInt32(dt.Rows[0]["SupplierId"]);
            lblVendorCode.Text = dt.Rows[0]["VendorCode"].ToString();
            lbl_InvNo.Text = dt.Rows[0]["InvNo"].ToString();
            lbl_InvDate.Text = Common.ToDateString(dt.Rows[0]["InvDate"]);
            lbl_DueDate.Text = Common.ToDateString(dt.Rows[0]["DueDate"]);
            lbl_InvAmount.Text = dt.Rows[0]["InvoiceAmount"].ToString();
            lblInvAmt.Text = lbl_InvAmount.Text;
            lbl_ApprovedAmount.Text = dt.Rows[0]["ApprovalAmount"].ToString();        
            lblCurrency.Text = dt.Rows[0]["Currency"].ToString();
            lbl_Vessel.Text = dt.Rows[0]["invVesselCode"].ToString();
            //lblPoNo.Text = dt.Rows[0]["PONo"].ToString();
            //txtPoNo.Text = lblPoNo.Text;
            Status = dt.Rows[0]["Status"].ToString();
            lblStatus.Text = dt.Rows[0]["Status"].ToString();
            //lblStageRemarks.Text = dt.Rows[0]["StageComments"].ToString();
            chkBooked.Checked = (Convert.ToString(dt.Rows[0]["Booked"])=="True");
            chkAdvPayment.Checked = (Convert.ToString(dt.Rows[0]["IsAdvPayment"]) == "True");
            chkNonPO.Checked = (Convert.ToString(dt.Rows[0]["IsNonPo"]) == "True");
            //chkCLSInvoice.Checked = (Convert.ToString(dt.Rows[0]["CLSInvoice"]) == "True");
            //chkCLSInvoice.Enabled = false;
            if (chkAdvPayment.Checked)
            {
                divAdvPay1.Visible = true;
               // divAdvPay2.Visible = true;
            }

            if (chkNonPO.Checked)
            {
                divNonPo1.Visible = true;
               // divNonPo2.Visible = true;
                trNonpoAccount.Visible = true;
                trNonpoRemarks.Visible = true;
                BindDepartment();
                if (Convert.ToInt32(dt.Rows[0]["NonPoId"].ToString()) > 0)
                {
                    NonPoId = Convert.ToInt32(dt.Rows[0]["NonPoId"].ToString());
                }
                if (NonPoId > 0)
                {
                    if (Convert.ToInt32(dt.Rows[0]["DepartmentId"].ToString()) > 0)
                    {
                        ddldepartment.SelectedValue = dt.Rows[0]["DepartmentId"].ToString();
                    }
                    if (Convert.ToInt32(dt.Rows[0]["AccountId"].ToString()) > 0)
                    {
                        BindAccount();
                        ddlAccount.SelectedValue = dt.Rows[0]["AccountId"].ToString();
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["NonPoDesc"].ToString()))
                    {
                        txtNonPoRemarks.Text = dt.Rows[0]["NonPoDesc"].ToString();
                    }
                }
            }
            btn_UpdateFile.Visible = (dt.Rows[0]["Status"].ToString() == "UnPaid");
            trBooked.Disabled = ! btn_UpdateFile.Visible;
            if (chkNonPO.Checked)
            {
                lbAddCreditNotes.Visible = btn_UpdatePONOView.Visible = lnkPrint.Visible = false;
                dvPoDtls.Visible = false;
                dvPoTotal.Visible = false;
            }
            else
            {
                btn_UpdatePONOView.Visible = lbAddCreditNotes.Visible = btn_UpdateFile.Visible;
                dvPoDtls.Visible = true;
                dvPoTotal.Visible = true;
            }
            tr_Frm.Visible = false;
            if (dt.Rows[0]["AttachmentName"].ToString().Trim() != "")
            {
                tr_Frm.Visible = true;
                //btnClip.Visible = true;
                btnClipText.Visible = true;
                btnClipText.Text = dt.Rows[0]["AttachmentName"].ToString();
                DataTable dtFile = Common.Execute_Procedures_Select_ByQuery("select Attachment from dbo.POS_Invoice where InvoiceId=" + InvoiceId);
                {
                    string TempFileName = "file-" + InvoiceId +".pdf";
                    string appname = ConfigurationManager.AppSettings["AppName"].ToString();
                    string TempFilePath = "/" + appname + "/EMANAGERBLOB/Purchase/Invoice/";
                    //string TempFilePath = "/EMANAGERBLOB/Purchase/Invoice/";

                    File.WriteAllBytes(Server.MapPath(TempFilePath + TempFileName), (byte[])dtFile.Rows[0]["Attachment"]);
                    frmInvoice.Attributes.Add("src", TempFilePath + TempFileName + "?" + r.NextDouble().ToString() );
                }
            }

            string sql="SELECT * FROM DBO.VW_iNVOICE_POLINK WHERE INVOICEID=" + InvoiceId;
            PoCount = 0;
            DataTable dtPoNos = Common.Execute_Procedures_Select_ByQuery(sql);
            rprPOS.DataSource = dtPoNos;
            PoCount = dtPoNos.Rows.Count;          
            rprPOS.DataBind();
            if (dtPoNos.Rows.Count > 0)
            {
                if (Convert.ToInt32(dtPoNos.Rows[0]["BidStatusID"]) < 6)
                {
                    btnAddPO.Visible = true;
                }
                else
                {
                    btnAddPO.Visible = false;
                }
            }
           
            Stage = Common.CastAsInt32(dt.Rows[0]["Stage"]);
            dt = Common.Execute_Procedures_Select_ByQuery("select EntertedBy,ApprovalFwdTo,VerificationFwdTo,PaidFwdTo,CancelledBy,ISNULL(CLSINVOICE,0) As CLSINVOICE from dbo.Pos_Invoice where InvoiceId=" + InvoiceId);
            int EnteredBy = Common.CastAsInt32(dt.Rows[0]["EntertedBy"]);
            int ApprovalFwdTo = Common.CastAsInt32(dt.Rows[0]["ApprovalFwdTo"]);
            int VerificationFwdTo = Common.CastAsInt32(dt.Rows[0]["VerificationFwdTo"]);

            int PaidFwdTo = Common.CastAsInt32(dt.Rows[0]["PaidFwdTo"]);
            bool IsCloseInvoice = false;
            IsCloseInvoice = Convert.ToBoolean(dt.Rows[0]["CLSINVOICE"]);

            if (pnl_Approval.Visible)
            {
                try
                {
                    if (IsCloseInvoice)
                        ddlAccountUser.SelectedValue = "52";
                    else
                        ddlAccountUser.SelectedIndex = 0;

                }
                catch { }
            }

            if (Status.Trim() != "Cancelled")
            {
                switch (Stage)
                {

                    case 1:
                        if (ApprovalFwdTo == UserId)
                        {
                            pnl_Approval.Visible = true;
                            txt_InvAmount.Text = lbl_InvAmount.Text;
                            lblApprovedUSDAmount.Text = getUSD(lblCurrency.Text, Common.CastAsDecimal(txt_InvAmount.Text), DateTime.Parse(lbl_InvDate.Text)).ToString();
                        }

                        break;


                    default:
                        break;
                }
            }

            if (pnl_Approval.Visible && VerificationFwdTo == 0)
            {
                DataTable dt1 = Common.Execute_Procedures_Select_ByQuery("select AcctOfficer from dbo.vessel where vesselid in ( SELECT vesselid FROM dbo.vw_Pos_Invoices WHERE [InvoiceId] = " + InvoiceId + " )");
                try
                { ddlVerifyForwardTo.SelectedValue = dt1.Rows[0][0].ToString(); }
                catch { }
            }

            //if (Status.Trim() != "Cancelled" && Stage > 1)
            //{
            //    btnSubmitInvoice.Visible = true;
            //}
            //else
            //{
            //    btnSubmitInvoice.Visible = false;
            //}

            //if (pnl_Approval.Visible)
            //{
            //    if (dtPoNos.Rows.Count > 0)
            //    {
            //        if (Convert.ToInt32(dtPoNos.Rows[0]["BidStatusID"]) >= 5)
            //        {
            //            pnl_Approval.Visible = true;
                        
            //        }
            //        else
            //        {
            //            pnl_Approval.Visible = false;                        
            //        }
            //    }
            //    else
            //    {
            //        pnl_Approval.Visible = true;                   
            //    }
            //}
            
            lblTotalPoAmount.Text = dtPoNos.Compute("SUM(AMT)", "").ToString();

            decimal TotalInvAmount=Common.CastAsDecimal(lbl_InvAmount.Text);
            decimal TotalPoAmount=Common.CastAsDecimal(lblTotalPoAmount.Text);

            decimal diff = TotalInvAmount - TotalPoAmount;
            
            //if (TotalInvAmount != 0)
            //    diff = diff * 100 / TotalInvAmount;
            //else
            //    diff = 0;

            tdPoTotal.Visible = false;
            if (dtPoNos.Rows.Count > 0 && TotalInvAmount > 0)
            {
                if (Math.Abs(diff) > 5)
                {
                    tdPoTotal.Visible = true;
                }
            }  
            
            if (pnl_Approval.Visible &&  (tdPoTotal.Visible || (! tdPoTotal.Visible && dtPoNos.Rows.Count == 0)))
            {
                Decimal ApprvedUSDAmount = Common.CastAsDecimal(lblApprovedUSDAmount.Text);
               if (ApprvedUSDAmount <= 500)
                {
                    trAppl1.Visible = true;
                    trAppl2.Visible = false;
                }
               else if (ApprvedUSDAmount > 500)
                {
                    trAppl1.Visible = true;
                    trAppl2.Visible = true;
                }
             
                //trAppl3.Visible = true;
                //trAppl4.Visible = true;
            }
        //---------
        string sql1 = "SELECT PONO,BidId, " +
                    "CONVERT(VARCHAR,(SELECT TOP 1 INVOICEID FROM DBO.POS_Invoice_Payment_PO P WHERE P.BidId=I.BIDID AND ISNULL(P.BidId,0)<>0 AND  P.InvoiceId<>" + InvoiceId + ")) AS OTHEROTHERINVID, " +
                   "(SELECT REFNO FROM DBO.POS_Invoice I1 WHERE I1.INVOICEID IN (SELECT TOP 1 INVOICEID FROM DBO.POS_Invoice_Payment_PO P WHERE P.BidId=I.BIDID AND ISNULL(P.BidId,0)<>0 AND P.InvoiceId<>" + InvoiceId + ")) AS OTHERREFNO, " +
                   "(Select BidStatusID from tblSMDPOMasterBid a with(nolock) where a.BidID = I.BIDID ) AS BidStatusID " +
                   "FROM " +
                   "DBO.POS_Invoice_Payment_PO I with(nolock) " +
                   "WHERE INVOICEID=" + InvoiceId;
        PoNos = Common.Execute_Procedures_Select_ByQuery(sql1);
            BindPoList();
            ShowDouments();
            ShowStageDetails();
            BindCreditNotedetails(InvoiceId);
            BindAdvancePaymentDetails();
        }

       
    }
    protected void BindDepartment()
    {
        //string sql = "select dept,deptname from  VW_sql_tblSMDPRDept";
        string sql = "Select mid.MidCatID,mid.MidCat from tblAccountsMid mid with(nolock) Inner join sql_tblSMDPRAccounts acc with(nolock) on mid.MidCatID = acc.MidCatID \r\nwhere  acc.Active = 'Y'  Group by mid.MidCatID,mid.MidCat  order by Midcat asc";
        Common.Set_Procedures("ExecQuery");
        Common.Set_ParameterLength(1);
        Common.Set_Parameters(new MyParameter("@Query", sql));
        DataSet dsPrType = new DataSet();
        dsPrType.Clear();
        dsPrType = Common.Execute_Procedures_Select();

        ddldepartment.DataSource = dsPrType;
        //ddldepartment.DataTextField = "deptname";
        //ddldepartment.DataValueField = "dept";
        ddldepartment.DataTextField = "MidCat";
        ddldepartment.DataValueField = "MidCatID";
        ddldepartment.DataBind();
        ddldepartment.Items.Insert(0, new ListItem("<Select>", "0"));
        ddldepartment.SelectedIndex = 0;
    }

    protected void BindAccount()
    {

        string sql = "select * from (select (select convert(varchar, AccountNumber)+'-'+AccountName from VW_sql_tblSMDPRAccounts PA where DA.AccountID=PA.AccountID ) AccountNumber  ,(select AccountName from  VW_sql_tblSMDPRAccounts PA where DA.AccountID=PA.AccountID ) AccountName  ,AccountID from VW_sql_tblSMDPRAccounts DA where DA.MidCatID='" + ddldepartment.SelectedValue + "') dd where AccountNumber is not null";
        Common.Set_Procedures("ExecQuery");
        Common.Set_ParameterLength(1);
        Common.Set_Parameters(new MyParameter("@Query", sql));
        DataSet dsPrType = new DataSet();
        dsPrType.Clear();
        dsPrType = Common.Execute_Procedures_Select();

        ddlAccount.DataSource = dsPrType;
        ddlAccount.DataTextField = "AccountNumber";
        ddlAccount.DataValueField = "AccountID";
        ddlAccount.DataBind();
        ddlAccount.Items.Insert(0, new ListItem("<Select>", "0"));
        ddlAccount.SelectedIndex = 0;


    }
    protected void chkBooked_OnCheckedChanged(object sender, EventArgs e)
    {
        Common.Execute_Procedures_Select_ByQuery("UPDATE DBO.POS_INVOICE SET BOOKED=" + ((chkBooked.Checked)?"1":"0") + " WHERE INVOICEID=" + InvoiceId);
    }

    //protected void chkCLSInvoice_OnCheckedChanged(object sender, EventArgs e)
    //{
    //    Common.Execute_Procedures_Select_ByQuery("UPDATE DBO.POS_INVOICE SET CLSINVOICE=" + ((chkCLSInvoice.Checked) ? "1" : "0") + " WHERE INVOICEID=" + InvoiceId);
        
    //}

    
   
    public decimal getUSD(string Curr, decimal Amount, DateTime dtFor)
    {
        decimal xchangerate=0;
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT TOP 1  EXC_RATE FROM DBO.XCHANGEDAILY WHERE FOR_CURR='" + Curr + "' AND RATEDATE <='" + dtFor.ToString("dd-MMM-yyyy") + "' ORDER BY RATEDATE DESC");
        if (dt.Rows.Count > 0)
            xchangerate = Common.CastAsDecimal(dt.Rows[0][0]);

        lblExchRate.Text = xchangerate.ToString();

        if(xchangerate!=0)
            return Math.Round(Amount / xchangerate,2);
        else
            return 0;
    }
    
    
    protected void lnkPrint_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "safsdaf", "window.open('PrintInvoice.aspx?InvoiceId=" + InvoiceId + "','')", true);
    }
    protected void lnkExport_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "safsdaf", "window.open('ApExport.aspx?InvoiceId=" + InvoiceId + "','')", true);
    }

    
    protected void btnClip_Click(object sender, ImageClickEventArgs e)
    {
        DownloadFile();
    }
    protected void btnClipText_Click(object sender, EventArgs e)
    {
        DownloadFile();
    }
    protected void DownloadFile()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT AttachmentName,Attachment FROM POS_Invoice WHERE InvoiceId=" + InvoiceId);
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
    public void setImageUrl(Image I,int Mode)
    {
        I.Visible = true;
        switch(Mode)
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
    public void setStageByOn(DataTable dt,int Mode, Label by, Label on, Label Rem)
    {
        int StageMode=getStageMode(dt, Mode);
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
    public int getStageMode(DataTable dt,int Mode)
    {
        DataView dv=dt.DefaultView;
        dv.RowFilter="ApprovalLevel=" + Mode;
        DataTable dt1=dv.ToTable();

        if (dt1.Rows.Count <= 0)
            return 1;
        else
        {
         if( Common.CastAsInt32(dt1.Rows[0]["AppUserId"]) >0)
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
                     "PaidFwdTo,(SELECT FirstName + ' ' AS LASTNAME FROM  dbo.UserMaster WHERE LoginId =  I.PaidFwdTo) AS PaidBY, PaidOn,PaidComments, Stage, Status, (Select Count(*) from POS_Invoice_RFP pi with(nolock) Inner Join POS_Invoice_RFP_Mapping pir with(nolock) on pi.RFPId = pir.RFPId where pir.InvoiceId = I.InvoiceId and Isnull(RFPSubmittedBy,0) > 0 )  As RFPCount  " +
                     "FROM POS_Invoice I WHERE InvoiceId =" + InvoiceId;
        DataTable dtStages = Common.Execute_Procedures_Select_ByQuery(Sql);

        int Stage = Common.CastAsInt32(dtStages.Rows[0]["Stage"]);
        int RFPCount = Common.CastAsInt32(dtStages.Rows[0]["RFPCount"]);
        string Status = dtStages.Rows[0]["Status"].ToString();

        if (Status.ToString().ToUpper() == "U" && RFPCount == 0)
        {
            lbkSendBacktoEntryStage.Visible = true;
        }
        else
        {
            lbkSendBacktoEntryStage.Visible = false;

        }

        EnteredBy = Common.CastAsInt32(dtStages.Rows[0]["EntertedById"]);
        ProcessedBy = Common.CastAsInt32(dtStages.Rows[0]["ApprovalFwdTo"]);
        PaymentBy = Common.CastAsInt32(dtStages.Rows[0]["PaidFwdTo"]);


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

       DataTable dt=Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.POS_INVOICE_APPROVALS WHERE INVOICEID=" + InvoiceId);

       EntryMode = 2;

       ProcessingMode = (Convert.IsDBNull(dtStages.Rows[0]["ApprovalFwdTo"])) ? 0 : ((Convert.IsDBNull(dtStages.Rows[0]["ApprovalOn"])) ? 3 : 2);
       
       if (ProcessingMode == 2)
       {
           Approval1Mode = getStageMode(dt, 1);
           Approval2Mode = getStageMode(dt, 2);
           //Approval3Mode = getStageMode(dt, 3);
           //Approval4Mode = getStageMode(dt, 4);

           ApprovalFinalMode = (Convert.IsDBNull(dtStages.Rows[0]["VerficationOn"])) ? 3 : 2;

           if(ApprovalFinalMode==2 )
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

            DataTable dtAccountUser=Common.Execute_Procedures_Select_ByQuery("exec DBO.POS_INV_getPaymentUser " + InvoiceId);

            if (!pnlApproval.Visible)
            {
                pnlApproval.Visible = (App1User > 0 && App1User == UserId && App1Date == null);
                if (pnlApproval.Visible)
                {
                    lblApprovalLevel.Text = "Approval 1";
                    hfdAppLevel.Value = "1";
                    hfdAppUserId.Value = App1User.ToString();
                    btnBackToInvProcess.Visible = true;
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
        }
        if (Stage >= 2)
        {
           if (chkAdvPayment.Checked)
            {
                btn_UpdatePONOView.Visible = true;
            }
           else
            {
                btn_UpdatePONOView.Visible = false;
            }
            btn_UpdateFile.Visible = false;
            lbAddCreditNotes.Visible = false;
        }

        string sql = "select  CASE I.[StageId] WHEN 0 THEN 'Entry' WHEN 1 THEN 'Approval' WHEN 2 THEN 'Acct. Verify' WHEN 3 THEN Case when (ISNULL((Select Count(*) from POS_Invoice_RFP_Mapping pirm with(nolock) inner join POS_Invoice_RFP pir with(nolock) on  pirm.RFPId = pir.RFPId where pirm.InvoiceId =I.INVOICEID AND pir.RFPSubmittedOn is not null),0) = 0 )  THEN 'RFP Processing' WHEN  (ISNULL((Select Count(*) from POS_Invoice_RFP_Mapping pirm with(nolock) inner join POS_Invoice_RFP pir with(nolock) on  pirm.RFPId = pir.RFPId where pirm.InvoiceId =I.INVOICEID AND pir.RFPApprovedOn is not null),0) = 0 ) then 'RFP Approval' ELSE 'Payment' END ELSE 'NA' END AS Stage,UserName,Comments,CommentsOn from POS_Invoice_StageComments I where invoiceid=" + InvoiceId;
        rptComments.DataSource = Common.Execute_Procedures_Select_ByQuery(sql);
        rptComments.DataBind();
        //---------------------------------------------
    }
    public int GetAprpovalUserId(DataTable dt, int AppLevel)
    {
        int ret=0;
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
    
    protected void btn_UpdateFile_Click(object sender, EventArgs e)
    {
        dv_UpdateAttachment.Visible = true;
        frmUpload.Attributes.Add("src", "UploadInvoice.aspx?InvoiceId=" + InvoiceId);
    }
    protected void btn_DocumentClose_Click(object sender, EventArgs e)
    {
        dvDocument.Visible = false;
        BindInvoiceDetails();
        ShowDouments();
    }
    protected void btn_UpdatePONOView_Click(object sender, EventArgs e)
    {
        dvPO.Visible = true;
    }
    protected void btn_UpdatePONO_Click(object sender, EventArgs e)
    {
        try
        {
            Common.Execute_Procedures_Select_ByQuery("UPDATE DBO.POS_INVOICE SET PONO='" + txtPoNo.Text + "' WHERE INVOICEID=" + InvoiceId);
            //lblPoNo.Text = txtPoNo.Text;
            lbl_inv_Message.Text = "PO# Updated Successfully.";
            //btnClip.Visible = true;
            dvPO.Visible = false;
        }
        catch (Exception ex)
        {
            lbl_inv_Message.Text = "Unable to update PO#. Error :" + Common.ErrMsg;
        }
    }
    protected void btn_PONOClose_Click(object sender, EventArgs e)
    {
        string sql = "SELECT * FROM DBO.VW_iNVOICE_POLINK WHERE INVOICEID=" + InvoiceId;
        PoCount = 0;
        DataTable dtPoNos = Common.Execute_Procedures_Select_ByQuery(sql);
        rprPOS.DataSource = dtPoNos;
        PoCount = dtPoNos.Rows.Count;
        rprPOS.DataBind();
        dvPO.Visible = false;
       
    }
    protected void btnPO_delete_Click(object sender, EventArgs e)
    {
        int BIDID = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        //------------- validate before deletion po must not be exported to traverse
        DataTable dt005 = Common.Execute_Procedures_Select_ByQuery("select 1 from dbo.[tblApEntries] b where b.intrav = 1 and b.bidid=" + BIDID);
        if (dt005.Rows.Count > 0)
        {
            ProjectCommon.ShowMessage("Sorry ! PO can not unlink for Invoice. Invoice has been Closed.");
            return;
        }
        else
        {
            //-----------------------------
            DataTable dt=Common.Execute_Procedures_Select_ByQuery("exec dbo.UnlinkPO " + BIDID + "," + InvoiceId);
            if (dt.Rows.Count > 0)
            {
                int result = Common.CastAsInt32(dt.Rows[0]["result"]);
                string msg = dt.Rows[0]["message"].ToString();
                if (result == 0)
                {
                    DataRow[] drs = PoNos.Select("BIDID='" + BIDID + "'");
                    foreach (DataRow dr in drs)
                    {
                        PoNos.Rows.Remove(dr);
                    }
                    BindPoList();
                    BindAdvancePaymentDetails();
                }
                else
                {
                    ProjectCommon.ShowMessage(msg);
                    return;
                }
            }
        }
    }
    protected void btn_PONOSave_Click(object sender, EventArgs e)
    {
        string bids = "";
        int LinkedPoCount =Common.CastAsInt32(Common.Execute_Procedures_Select_ByQuery("SELECT COUNT(BIDID) FROM POS_Invoice_Payment_PO WHERE InvoiceId=" + InvoiceId).Rows[0][0]);
        if (LinkedPoCount > 0)
        {
            lbl_popinv_Message.Text = "Sorry ! There is already a PO linked with this Invoice. Please remove that to link with other PO.";
            return;
        }
        else
        {
            if (PoNos.Rows.Count == 1)
            {
                int valbidid = 0;
                foreach (DataRow dr in PoNos.Rows)
                {
                    bids += "," + dr["BIDID"].ToString();
                    valbidid = Common.CastAsInt32(dr["BIDID"].ToString());
                }
                if (bids.StartsWith(","))
                    bids = bids.Substring(1);
                
                DataTable dtval = Common.Execute_Procedures_Select_ByQuery("select 1 from dbo.[tblApEntries] where intrav = 1 and bidid = " + valbidid);
                if (dtval.Rows.Count > 0)
                {
                    lbl_popinv_Message.Text = "Sorry ! Invoice has been Closed.";
                    return;
                }
                else
                {
                    //Common.Execute_Procedures_Select_ByQuery("DELETE FROM POS_Invoice_Payment_PO WHERE InvoiceId=" + InvoiceId);
                    if (bids.Trim() != "")
                    {
                        Common.Execute_Procedures_Select_ByQuery("UPDATE [DBO].TBLSMDPOMASTERBID SET InvoiceNo='" + lbl_InvNo.Text  + "',BidInvoiceDate='" + lbl_InvDate.Text + "' WHERE BidID=" + valbidid);
                        Common.Execute_Procedures_Select_ByQuery("INSERT INTO POS_Invoice_Payment_PO SELECT " + InvoiceId + ",BidPoNum,BIDID FROM DBO.TBLSMDPOMASTERBID WHERE BIDID IN (SELECT RESULT FROM DBO.CSVtoTable('" + bids + "',','))");
                        BindInvoiceDetails();
                    }
                }
            }
            else
            {
                lbl_popinv_Message.Text = "Sorry ! Only Single PO is allowed to link with one invoice.";
                return;
            }
        }
    }
    protected void btn_POClose_Click(object sender, EventArgs e)
    {
        dvPO.Visible = false;
    }
    protected void btnAddPO_Click(object sender, EventArgs e)
    {

        if (txtPoNo.Text.Trim() != "")
        {
            string povsl = txtPoNo.Text.Substring(0, 3);
            if (Common.CastAsInt32(povsl) == 0)
            {
                if (lbl_Vessel.Text.Substring(0, 3) != povsl)
                {
                    lbl_popinv_Message.Text = "PO vessel not matching with invoice vessel.";
                    txtPoNo.Focus();
                    return;
                }
            }

            if (PoNos.Select("PONO='" + txtPoNo.Text.Trim() + "'").Length <= 0)
            {
                int BidId = Common.CastAsInt32(hfdbidid.Value);

                if (BidId <= 0)
                {
                    lbl_popinv_Message.Text = "Invalid PO#.";
                    txtPoNo.Focus();
                    return;
                }

                PoNos.Rows.Add(PoNos.NewRow());
                PoNos.Rows[PoNos.Rows.Count - 1]["PONO"] = txtPoNo.Text;
                PoNos.Rows[PoNos.Rows.Count - 1]["BIDID"] = BidId.ToString();
                string InvoiceRefNo = "";
                string OTHEROTHERINVID = "";
                //int BidStatusId = 0;
                //bool IsAdvPayment = false;
                if (BidId > 0)
                {
                    DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT InvoiceId,RefNo, ISNULL(IsAdvPayment,0) As IsAdvPayment FROM [dbo].[POS_Invoice] I WHERE I.InvoiceId IN ( SELECT INVOICEID FROM POS_Invoice_Payment_PO WHERE BIDID=" + BidId + " AND INVOICEID<>" + InvoiceId + ") ");
                    if (dt.Rows.Count > 0)
                    {
                        OTHEROTHERINVID = dt.Rows[0][0].ToString();
                        InvoiceRefNo = dt.Rows[0][1].ToString();
                        //-------------------
                        if (Convert.ToBoolean(dt.Rows[0][2]) == false)
                        {
                            lbl_popinv_Message.Text = "Sorry this PO is already linked with some other invoice ( Ref No : " + InvoiceRefNo + ").";
                            return;
                        }
                    }
                    //DataTable dt1 = Common.Execute_Procedures_Select_ByQuery("Select BidStatusID from tblSMDPOMasterBid with(nolock) where Bidid = " + BidId + " ");
                    //if (dt1.Rows.Count > 0)
                    //{
                    //    BidStatusId = Convert.ToInt32(dt.Rows[0]["BidStatusID"]);
                    //}
                }

                PoNos.Rows[PoNos.Rows.Count - 1]["OTHEROTHERINVID"] = OTHEROTHERINVID;
                PoNos.Rows[PoNos.Rows.Count - 1]["OTHERREFNO"] = InvoiceRefNo;
               // PoNos.Rows[PoNos.Rows.Count - 1]["BidStatusID"] = BidStatusId;

                txtPoNo.Text = "";
                hfdbidid.Value = "0";
                BindPoList();
                BindAdvancePaymentDetails();
            }
        }
    }
    public void BindPoList()
    {
        rptPo.DataSource = PoNos;
        rptPo.DataBind();
        if (PoNos.Rows.Count > 0)
        {
            if (Convert.ToInt32(PoNos.Rows[0]["BidStatusID"]) > 5)
            {
                btnAddPO.Visible = false;
            }
            else
            {
                btnAddPO.Visible = true;
            }
        }
        else
        {
            btnAddPO.Visible = true;
        }
    }

    protected void btn_uploadClose_Click(object sender, EventArgs e)
    {
        BindInvoiceDetails();
         dv_UpdateAttachment.Visible = false;
       // Response.Redirect("ViewInvoice.aspx?InvoiceId=" + InvoiceId);
    }


    public int GetBidStatus(string _BidID)
    {
        string sql = "select  MAX(BidStatusID)  from vw_tblsmdpomasterbid where BidID In ('" + _BidID + "')";
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



    //protected void btnSubmitInvoice_Click(object sender, EventArgs e)
    //{
    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "safsdaf", "window.open('InvoiceSubmitforApproval.aspx?InvoiceId=" + InvoiceId + "','')", true);
    //}

    protected void ApprovalAmount_OnTextChanged(object sender, EventArgs e)
    {
        lblApprovedUSDAmount.Text = getUSD(lblCurrency.Text, Common.CastAsDecimal(txt_InvAmount.Text), DateTime.Parse(lbl_InvDate.Text)).ToString();
    }
    protected void btn_AppSave_Click(object sender, EventArgs e)
    {
        if (chkNonPO.Checked)
        {
            if (ddldepartment.SelectedValue == "0")
            {
                lbl_inv_Message.Text = "Please select Department for Non PO Invoice.";
                ddldepartment.Focus();
                return;
            }

            if (ddlAccount.SelectedValue == "0")
            {
                lbl_inv_Message.Text = "Please select Account for Non PO Invoice.";
                ddlAccount.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtNonPoRemarks.Text.Trim()))
            {
                lbl_inv_Message.Text = "Please enter Description for Non PO Invoice.";
                txtNonPoRemarks.Focus();
                return;
            }
        }

        //----------------------------
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT AttachmentName,Attachment FROM POS_Invoice WHERE InvoiceId=" + InvoiceId);
        if (dt.Rows.Count <= 0)
        {
            lbl_inv_Message.Text = "Please upload invoice to continue.";
            return;
        }
        if (ddlAccountUser.SelectedIndex <= 0)
        {
            lbl_inv_Message.Text = "Please select account user to continue.";
            return;
        }
        //----------------------------

        

        // int bidCount = 0;
        if (! (chkNonPO.Checked))
        {
            string bids = "";
            if (PoNos.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('PO is Missing. For Further Invoice process, Please add PO from Update PO.');", true);
                btn_UpdatePONOView.Focus();
                return;
            }
           
            if (!(chkAdvPayment.Checked))
            {
                foreach (DataRow dr in PoNos.Rows)
                {
                    bids += "," + dr["BIDID"].ToString();
                }
                if (bids.StartsWith(","))
                    bids = bids.Substring(1);
                int BidStatusId = GetBidStatus(bids);
                if (BidStatusId >= 3 && BidStatusId <= 4)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Good Received and PO Verification with Invoices not completed.');", true);
                    //  lbl_inv_Message.Text = "";
                    return;
                }
                if (BidStatusId == 5)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alet", "alert('PO Verification with Invoices not completed.');", true);
                    //lbl_inv_Message.Text = "PO Verification with Invoices not completed.";
                    return;
                }
            }
           
        }
       
        
        
        try
        {
            bool success = false;
            DropDownList[] ctlApprovals = { ddlVerifyForwardTo, ddlApproval2
                    //, ddlApproval3, ddlApproval4 
            };
            decimal ApprovalAmount = Common.CastAsDecimal(lblApprovedUSDAmount.Text);

            if (PoCount <= 0) // NON PO LINKED TRANSACTIONS
            {
                success = true;
                if (trAppl1.Visible && (!trAppl2.Visible))
                {
                    int StartApproval = 1;
                    for (int i = 1; i <= StartApproval; i++)
                    {
                        success = success && ((DropDownList)ctlApprovals[i - 1]).SelectedIndex > 0;
                    }
                }
                    if (trAppl1.Visible && trAppl2.Visible)
                {
                    int StartApproval = 2;
                    //if (ApprovalAmount < 5000)
                    //{ StartApproval = 1; }
                    //else if (ApprovalAmount >= 5000)
                    //{ StartApproval = 2; }
                    // else if (ApprovalAmount < 100000)
                    //{ StartApproval = 3; }
                    //else
                    //{ StartApproval = 2; }


                    for (int i = StartApproval; i <= 2; i++)
                    {
                        success = success || ((DropDownList)ctlApprovals[i - 1]).SelectedIndex > 0;
                    }
                }
            }
            else // PO LINKED TRANSACTIONS
            {
                success = true;
                if (trAppl1.Visible && (! trAppl2.Visible))
                {
                    int MaxApproval = 1;
                    //if (ApprovalAmount < 5000)
                    //{ MaxApproval = 1; }
                    //else if (ApprovalAmount >= 5000)
                    //{ MaxApproval = 2; }
                    ////else if (ApprovalAmount < 100000)
                    ////{ MaxApproval = 3; }
                    //else
                    //{ MaxApproval = 2; }

                    for (int i = 1; i <= MaxApproval; i++)
                    {
                        success = success && ((DropDownList)ctlApprovals[i - 1]).SelectedIndex > 0;
                    }
                }
                else if (trAppl1.Visible && trAppl2.Visible)
                {
                    int MaxApproval = 2;
                    //if (ApprovalAmount < 5000)
                    //{ MaxApproval = 1; }
                    //else if (ApprovalAmount >= 5000)
                    //{ MaxApproval = 2; }
                    ////else if (ApprovalAmount < 100000)
                    ////{ MaxApproval = 3; }
                    //else
                    //{ MaxApproval = 2; }

                    for (int i = 1; i <= MaxApproval; i++)
                    {
                        success = success && ((DropDownList)ctlApprovals[i - 1]).SelectedIndex > 0;
                    }
                }


            }

            if (success)
            {
                if (trAppl1.Visible && (! trAppl2.Visible))
                {
                    int App1 = Common.CastAsInt32(ddlVerifyForwardTo.SelectedValue);
                    //int App2 = Common.CastAsInt32(ddlApproval2.SelectedValue);
                    //int App3 = Common.CastAsInt32(ddlApproval3.SelectedValue);
                    //int App4 = Common.CastAsInt32(ddlApproval4.SelectedValue);
                    //int MaxAppUser = (App4 > 0) ? App4 : ((App3 > 0) ? App3 : ((App2 > 0) ? App2 : App1));
                    int MaxAppUser = App1;

                    //Common.Execute_Procedures_Select_ByQuery("EXEC POS_INVOICE_PROCESS_SENDFORAPPROVAL " + InvoiceId + "," + App1 + "," + App2 + "," + App3 + "," + App4);
                    Common.Execute_Procedures_Select_ByQuery("EXEC POS_INVOICE_PROCESS_SENDFORAPPROVAL " + InvoiceId + "," + App1 );
                    Common.Execute_Procedures_Select_ByQuery("UPDATE dbo.Pos_Invoice SET ApprovalOn=getdate(),VerificationFwdTo=" + MaxAppUser + ",PaidFwdTo=" + ddlAccountUser.SelectedValue + ",Stage=2,StageComments='" + txt_AppRemarks.Text.Trim().Replace("'", "`") + "',ApprovalComments='" + txt_AppRemarks.Text.Trim().Replace("'", "`") + "', ApprovalAmount=" + txt_InvAmount.Text.Trim() + " where InvoiceId=" + InvoiceId);

                    foreach (RepeaterItem ri in rptAdvancePayment.Items)
                    {
                        int APId = Common.CastAsInt32(((HiddenField)ri.FindControl("hdnAPId")).Value);
                        Common.Execute_Procedures_Select_ByQuery("UPDATE dbo.POS_Invoice_AdvancePayment SET  IsPaymentSettle = 1, PaymentSettleDt = getdate(), paymentSettleby = "+ Common.CastAsInt32(Session["loginid"]) + ", SettleInvoiceId = "+InvoiceId+" where APId = " + APId);
                    }
                    
                }
                else if (trAppl1.Visible && trAppl2.Visible)
                {
                    int App1 = Common.CastAsInt32(ddlVerifyForwardTo.SelectedValue);
                    int App2 = Common.CastAsInt32(ddlApproval2.SelectedValue);
                    //int App3 = Common.CastAsInt32(ddlApproval3.SelectedValue);
                    //int App4 = Common.CastAsInt32(ddlApproval4.SelectedValue);
                    //int MaxAppUser = (App4 > 0) ? App4 : ((App3 > 0) ? App3 : ((App2 > 0) ? App2 : App1));
                    int MaxAppUser = ((App2 > 0) ? App2 : App1);

                    //Common.Execute_Procedures_Select_ByQuery("EXEC POS_INVOICE_PROCESS_SENDFORAPPROVAL " + InvoiceId + "," + App1 + "," + App2 + "," + App3 + "," + App4);
                    Common.Execute_Procedures_Select_ByQuery("EXEC POS_INVOICE_PROCESS_SENDFORAPPROVAL " + InvoiceId + "," + App1 + "," + App2);
                    Common.Execute_Procedures_Select_ByQuery("UPDATE dbo.Pos_Invoice SET ApprovalOn=getdate(),VerificationFwdTo=" + MaxAppUser + ",PaidFwdTo=" + ddlAccountUser.SelectedValue + ",Stage=2,StageComments='" + txt_AppRemarks.Text.Trim().Replace("'", "`") + "',ApprovalComments='" + txt_AppRemarks.Text.Trim().Replace("'", "`") + "', ApprovalAmount=" + txt_InvAmount.Text.Trim() + " where InvoiceId=" + InvoiceId);

                    foreach (RepeaterItem ri in rptAdvancePayment.Items)
                    {
                        int APId = Common.CastAsInt32(((HiddenField)ri.FindControl("hdnAPId")).Value);
                        Common.Execute_Procedures_Select_ByQuery("UPDATE dbo.POS_Invoice_AdvancePayment SET  IsPaymentSettle = 1, PaymentSettleDt = getdate(), paymentSettleby = " + Common.CastAsInt32(Session["loginid"]) + ", SettleInvoiceId = "+InvoiceId+" where APId = " + APId);
                    }
                }
                else
                {
                    Common.Execute_Procedures_Select_ByQuery("UPDATE dbo.Pos_Invoice SET ApprovalOn=getdate(),PaidFwdTo=" + ddlAccountUser.SelectedValue + ",Stage=3,StageComments='" + txt_AppRemarks.Text.Trim().Replace("'", "`") + "',ApprovalComments='" + txt_AppRemarks.Text.Trim().Replace("'", "`") + "', ApprovalAmount=" + txt_InvAmount.Text.Trim() + " where InvoiceId=" + InvoiceId);

                    foreach (RepeaterItem ri in rptAdvancePayment.Items)
                    {
                        int APId = Common.CastAsInt32(((HiddenField)ri.FindControl("hdnAPId")).Value);
                        Common.Execute_Procedures_Select_ByQuery("UPDATE dbo.POS_Invoice_AdvancePayment SET  IsPaymentSettle = 1, PaymentSettleDt = getdate(), paymentSettleby = " + Common.CastAsInt32(Session["loginid"]) + ", SettleInvoiceId = "+InvoiceId+" where APId = " + APId);
                    }
                }
               try
                {
                    if (trNonpoAccount.Visible && chkNonPO.Checked)
                    {
                        bool IsnonPoUpdate = InsertApEntirsforNonPo(InvoiceId, NonPoId, Convert.ToInt32(ddlAccount.SelectedValue), txtNonPoRemarks.Text.Trim());
                        if (!IsnonPoUpdate)
                        {
                            lbl_inv_Message.Text = "Unable to Add AP Entires for Selected Invoice.";
                            return;
                        }
                    }
                }
                catch(Exception ex)
                {
                    lbl_inv_Message.Text = "Unable to Add AP Entires Error :" + ex.Message;
                }
               
                btn_AppSave.Visible = false;
                pnl_Approval.Visible = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alet", "alert('Invoice send for approval successfully.');", true);
                BindInvoiceDetails();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alet", "alert('Please select approval person to continue.');", true);
            }

        }
        catch (Exception ex)
        { lbl_inv_Message.Text = "Unable to Approve. Error :" + ex.Message; }
    }

    protected void bindPayment_ForwardToddl()
    {
        string SQL = "select (FirstName + ' ' + LastName ) AS UserName, LoginId  from dbo.usermaster where loginid in (select userid from pos_invoice_mgmt where payment=1) AND statusId='A' Order By UserName";

        DataTable dt1;//= Common.Execute_Procedures_Select_ByQuery("");
        //this.ddlPaymentForwardTo.DataValueField = "LoginId";
        //this.ddlPaymentForwardTo.DataTextField = "UserName";
        //this.ddlPaymentForwardTo.DataSource = dt1;
        //this.ddlPaymentForwardTo.DataBind();
        //ddlPaymentForwardTo.Items.Insert(0, new ListItem("< Select User >", "0"));

        SQL = "select (FirstName + ' ' + LastName ) AS UserName, LoginId from dbo.usermaster where loginid in (select userid from pos_invoice_mgmt where Approval=1) AND statusId='A' Order By UserName";
        dt1 = Common.Execute_Procedures_Select_ByQuery(SQL);
        this.ddlVerifyForwardTo.DataValueField = "LoginId";
        this.ddlVerifyForwardTo.DataTextField = "UserName";
        this.ddlVerifyForwardTo.DataSource = dt1;
        this.ddlVerifyForwardTo.DataBind();
        ddlVerifyForwardTo.Items.Insert(0, new ListItem("< Select User >", "0"));

        SQL = "select (FirstName + ' ' + LastName ) AS UserName, LoginId from dbo.usermaster where loginid in (select userid from pos_invoice_mgmt where Verification=1) AND statusId='A' Order By UserName";
        dt1 = Common.Execute_Procedures_Select_ByQuery(SQL);
        this.ddlApproval2.DataValueField = "LoginId";
        this.ddlApproval2.DataTextField = "UserName";
        this.ddlApproval2.DataSource = dt1;
        this.ddlApproval2.DataBind();
        ddlApproval2.Items.Insert(0, new ListItem("< Select User >", "0"));

        //SQL = "SELECT (FirstName + ' ' + LastName ) AS UserName,LoginId from dbo.usermaster where loginid in (select userid from pos_invoice_mgmt where Approval3=1) AND statusId='A' Order By UserName";
        //dt1 = Common.Execute_Procedures_Select_ByQuery(SQL);
        //this.ddlApproval3.DataValueField = "LoginId";
        //this.ddlApproval3.DataTextField = "UserName";
        //this.ddlApproval3.DataSource = dt1;
        //this.ddlApproval3.DataBind();
        //ddlApproval3.Items.Insert(0, new ListItem("< Select User >", "0"));

        //SQL = "SELECT (FirstName + ' ' + LastName ) AS UserName,LoginId from dbo.usermaster where loginid in (select userid from pos_invoice_mgmt where Approval4=1) AND statusId='A' Order By UserName";
        //dt1 = Common.Execute_Procedures_Select_ByQuery(SQL);
        //this.ddlApproval4.DataValueField = "LoginId";
        //this.ddlApproval4.DataTextField = "UserName";
        //this.ddlApproval4.DataSource = dt1;
        //this.ddlApproval4.DataBind();
        //ddlApproval4.Items.Insert(0, new ListItem("< Select User >", "0"));

        SQL = "select (FirstName + ' ' + LastName ) AS UserName, LoginId from dbo.usermaster where loginid in (select userid from pos_invoice_mgmt where Payment=1) AND statusId='A' Order By UserName";
        dt1 = Common.Execute_Procedures_Select_ByQuery(SQL);
        this.ddlAccountUser.DataValueField = "LoginId";
        this.ddlAccountUser.DataTextField = "UserName";
        this.ddlAccountUser.DataSource = dt1;
        this.ddlAccountUser.DataBind();
        ddlAccountUser.Items.Insert(0, new ListItem("< Select User >", "0"));
        DataTable dtAccountUser = Common.Execute_Procedures_Select_ByQuery("exec DBO.POS_INV_getPaymentUser " + InvoiceId);
        if (dtAccountUser.Rows.Count > 0)
        {
            ddlAccountUser.SelectedValue = dtAccountUser.Rows[0][0].ToString();
        }

    }

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
    protected void btnDocClose_Click(object sender, EventArgs e)
    {
        dvDocument.Visible = false;
        BindInvoiceDetails();
        ShowDouments();
    }
    protected void ShowDouments()
    {
        string DeleteAllowed = "N";
        DeleteAllowed = (Stage <= 1) ? "Y" : "N";
        string sql2 = "SELECT *,'" + DeleteAllowed + "' as DeleteAllowed, '" +lblStatus.Text.ToUpper()+"' As Status  FROM DBO.POS_Invoice_Documents WHERE INVOICEID=" + InvoiceId;
        Repeater1.DataSource = Common.Execute_Procedures_Select_ByQuery(sql2);
        Repeater1.DataBind();
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        int Pk = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        Common.Execute_Procedures_Select_ByQuery("DELETE FROM POS_Invoice_Documents WHERE PK=" + Pk);
        ShowDouments();
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
    protected void btnDownload_Click(object sender, EventArgs e)
    {
        int Pk = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT AttachmentName,Attachment FROM POS_Invoice_Documents WHERE PK=" + Pk);
        if (dt.Rows.Count > 0)
        {
            string FileName = dt.Rows[0]["AttachmentName"].ToString();
            
            if (FileName.Trim() != "")
            {
                string ExtFileName = Path.GetExtension(FileName).Substring(1);
                //Response.ContentType = "application/" + ExtFileName;
                //Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileName);
                //byte[] buffer = (byte[])dt.Rows[0]["Attachment"];
                //Response.BinaryWrite(buffer);
                //Response.Flush();

                byte[] bytes = (byte[])dt.Rows[0]["Attachment"];
                Response.Clear();
                Response.ClearHeaders();
                Response.Buffer = true;
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = "application/" + ExtFileName; ;
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileName);
                Response.BinaryWrite(bytes); //
                Response.BufferOutput = true;
                Response.OutputStream.Write(bytes, 0, bytes.Length);
               // Response.Flush();
                Response.End();
                //byte[] buff = (byte[])dt.Rows[0]["Attachment"];
                //Response.ContentType = "application/" + ExtFileName;
                //Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileName);
                //Response.BinaryWrite(buff);
                //Response.Flush();
                //Response.End();
            }
        }
    }

    protected void btnViewAppHistory_Click(object sender, EventArgs e)
    {
        divAppHistory.Visible = true;
        GetApprovalHistory();
    }

    protected void GetApprovalHistory()
    {
        //string Sql = "SELECT (SELECT FirstName + ' ' AS LASTNAME FROM  dbo.UserMaster WHERE LoginId =  I.EntertedBy) AS EnteredBy,I.EntertedBy as EntertedById, EnteredOn,EntryComments, " +
        //            "ApprovalFwdTo,(SELECT FirstName + ' ' AS LASTNAME FROM  dbo.UserMaster WHERE LoginId =  I.ApprovalFwdTo) AS ApprovalBy,I.ApprovalFwdTo, ApprovalOn, ApprovalComments,  " +
        //            "VerificationFwdTo,(SELECT FirstName + ' ' AS LASTNAME FROM  dbo.UserMaster WHERE LoginId =  I.VerificationFwdTo) AS VerifiedBy,VerficationOn, VerificationComments,  " +
        //            "PaidFwdTo,(SELECT FirstName + ' ' AS LASTNAME FROM  dbo.UserMaster WHERE LoginId =  I.PaidFwdTo) AS PaidBY, PaidOn,PaidComments, Stage, Status  " +
        //            "FROM POS_Invoice I WHERE InvoiceId =" + InvoiceId;
        string Sql = "EXEC GetInvoicePaymentHistory "+ InvoiceId + "";
        DataTable dtStages = Common.Execute_Procedures_Select_ByQuery(Sql);

        lblEnteredBy.Text = dtStages.Rows[0]["EnteredBy"].ToString();
        lblEnteredOn.Text = Common.ToDateString(dtStages.Rows[0]["EnteredOn"]);
        lblEntryComments.Text = dtStages.Rows[0]["EntryComments"].ToString();

        lblProcessingTo.Text = dtStages.Rows[0]["ApprovalBy"].ToString();
        lblProcessingOn.Text = Common.ToDateString(dtStages.Rows[0]["ApprovalOn"]);
        lblProcessingComments.Text = dtStages.Rows[0]["ApprovalComments"].ToString();

        lblRFPSubmitBy.Text = dtStages.Rows[0]["RFPSubmitted"].ToString();
        lblRFPSubmitOn.Text = Common.ToDateString(dtStages.Rows[0]["RFPSubmittedOn"]);
        lblRFPComments.Text = dtStages.Rows[0]["RFPSubmmitedComments"].ToString();

        lblRFPApproveBy.Text = dtStages.Rows[0]["RFPApproved"].ToString();
        lblRFPApproveOn.Text = Common.ToDateString(dtStages.Rows[0]["RFPApprovedOn"]);
        lblRFPApprovalComments.Text = dtStages.Rows[0]["RFPApprovedComments"].ToString();

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
        int RFPProcessingMode = 0;
        int RFPApprovalMode = 0;

        // 0: DO NOT WORRY
        // 1: STAGE - NOR REQUIRED 
        // 2: DONE
        // 3: PENDING

        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.POS_INVOICE_APPROVALS WHERE INVOICEID=" + InvoiceId);

        EntryMode = 2;

        ProcessingMode = (Convert.IsDBNull(dtStages.Rows[0]["ApprovalFwdTo"])) ? 0 : ((Convert.IsDBNull(dtStages.Rows[0]["ApprovalOn"])) ? 3 : 2);

        RFPProcessingMode = (Convert.IsDBNull(dtStages.Rows[0]["RFPSubmitted"])) ? 0 : ((Convert.IsDBNull(dtStages.Rows[0]["RFPSubmittedOn"])) ? 3 : 2);

        RFPApprovalMode = (Convert.IsDBNull(dtStages.Rows[0]["RFPApproved"])) ? 0 : ((Convert.IsDBNull(dtStages.Rows[0]["RFPApprovedOn"])) ? 3 : 2);

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
        setImageUrl(imgRFPSubmit, RFPProcessingMode);
        setImageUrl(imgRFPApprove, RFPApprovalMode);
        setImageUrl(imgPayment, PaymentMode);
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
            //case 5: // Approval3
            //    string SQL3 = "SELECT FirstName + ' ' + MiddleName + ' ' + FamilyName AS UserName,USERID as LoginId FROM DBO.Hr_PersonalDetails WHERE DRC IS NULL AND POSITION IN(4,89)";
            //    ddlNewUser.DataSource = Common.Execute_Procedures_Select_ByQuery(SQL3);
            //   break;
            //case 6: // Approval4
            //    string SQL4 = "SELECT FirstName + ' ' + MiddleName + ' ' + FamilyName AS UserName,USERID as LoginId FROM DBO.Hr_PersonalDetails WHERE DRC IS NULL AND POSITION=1";
            //    ddlNewUser.DataSource = Common.Execute_Procedures_Select_ByQuery(SQL4);
            //    break;
            case 6: // RFP Approval
                string SQL4 = "select(FirstName + ' ' + LastName) AS UserName, LoginId from dbo.usermaster where loginid in (select userid from pos_invoice_mgmt where Approval4 = 1) AND statusId = 'A' Order By UserName";
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
            //case 5: // Approval3
            //    string SQL3 = "UPDATE DBO.POS_Invoice_Approvals SET AppUserId=" + ddlNewUser.SelectedValue + " WHERE INVOICEID=" + InvoiceId + " AND ApprovalLevel=3";
            //    Common.Execute_Procedures_Select_ByQuery(SQL3);
            //    break;
            //case 6: // Approval4
            //    string SQL4 = "UPDATE DBO.POS_Invoice_Approvals SET AppUserId=" + ddlNewUser.SelectedValue + " WHERE INVOICEID=" + InvoiceId + " AND ApprovalLevel=4";
            //    Common.Execute_Procedures_Select_ByQuery(SQL4);
            //    break;
            case 6: // RFP Approval
                string SQL4 = "UPDATE DBO.POS_Invoice_RFP_Approvals SET AppUserId=" + ddlNewUser.SelectedValue + " WHERE RFPID = (Select RFPID from POS_Invoice_RFP_Mapping with(nolock) where INVOICEID=" + InvoiceId + ") AND ApprovalLevel=1" ;
                Common.Execute_Procedures_Select_ByQuery(SQL4);
                string SQL6 = "UPDATE DBO.POS_Invoice_RFP SET AppUserId=" + ddlNewUser.SelectedValue + " WHERE RFPID = (Select RFPID from POS_Invoice_RFP_Mapping with(nolock) where INVOICEID=" + InvoiceId + ")";
                Common.Execute_Procedures_Select_ByQuery(SQL6);
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

    protected void Button4_Click(object sender, EventArgs e)
    {
        divAppHistory.Visible = false;
    }

    protected void btnBackToInvProcess_Click(object sender, EventArgs e)
    {
        Common.Execute_Procedures_Select_ByQuery("UPDATE dbo.Pos_Invoice SET ApprovalOn=getdate(),Stage=1,StageComments='" + txt_AppRemarks.Text.Trim().Replace("'", "`") + "',ApprovalComments='" + txt_AppRemarks.Text.Trim().Replace("'", "`") + "' where InvoiceId=" + InvoiceId);
        btnApprovalSave.Visible = false;
        btnBackToInvProcess.Visible = false;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Invoice rejected successfully and pending with Invoice Processer.');", true);
        
    }

    protected void lbkSendBacktoEntryStage_Click(object sender, EventArgs e)
    {
        Common.Execute_Procedures_Select_ByQuery("UPDATE dbo.Pos_Invoice SET ApprovalOn=getdate(),Stage=1,StageComments='Invoice send back to Entry Level by user.',ApprovalComments='" + txt_AppRemarks.Text.Trim().Replace("'", "`") + "' where InvoiceId=" + InvoiceId);
        lbkSendBacktoEntryStage.Visible = false;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Invoice Send Back to Entry level successfully and pending with Invoice Processer.');Refresh();window.close();", true);
    }

    //protected void bindCurrencyddl()
    //{
    //    DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT Curr FROM [dbo].[VW_tblWebCurr]");
    //    this.ddCurrency.DataValueField = "Curr";
    //    this.ddCurrency.DataTextField = "Curr";
    //    this.ddCurrency.DataSource = dt;
    //    this.ddCurrency.DataBind();
    //    ddCurrency.Items.Insert(0, new ListItem("< Select Currency >", "0"));
    //}

    protected void btnSaveCreditNotes_Click(object sender, EventArgs e)
    {
        try
        {
            int creditnoteid = 0;
            if (hdnCreditNotesId.Value != "" && Convert.ToInt32(hdnCreditNotesId.Value) > 0)
            {
                creditnoteid = Convert.ToInt32(hdnCreditNotesId.Value);
            }
            if (string.IsNullOrEmpty(txtCreditNoteNo.Text.Trim()))
            {
                lblCreditNoteMsg.Text = "Please enter Credit Note #.";
                txtCreditNoteNo.Focus();
                return;
            }
            //if (ddCurrency.SelectedIndex == 0)
            //{
            //    lblCreditNoteMsg.Text = "Please select Credit Note Currency.";
            //    ddCurrency.Focus();
            //    return;
            //}
            if (string.IsNullOrEmpty(txtcreditNoteAmt.Text.Trim()))
            {
                lblCreditNoteMsg.Text = "Please enter Credit Note Amount.";
                txtcreditNoteAmt.Focus();
                return;
            }
            if (Convert.ToDecimal(txtcreditNoteAmt.Text.Trim()) <= 0)
            {
                lblCreditNoteMsg.Text = "Please enter Credit Note Amount greater than or equal Zero.";
                txtcreditNoteAmt.Text = "";
                txtcreditNoteAmt.Focus();
                return;
            }
            if (fuAttachment.HasFile)
            {
                if (fuAttachment.PostedFile.ContentLength > (1024 * 1024 * 2))
                {
                    lblCreditNoteMsg.Text = "File Size is Too big! Maximum Allowed is 2 MB...";
                    //fuAttachment.Focus();
                    return;
                }
            }
            else
            {
                lblCreditNoteMsg.Text = "Please upload Credit note document.";
                //fuAttachment.Focus();
                return;
            }
            if (PoNos.Rows.Count == 1)
            {
                //string sql = "Select top 1 pi.RefNo from InvoiceCreditNotesDetails I with(nolock) Inner Join Pos_Invoice pi  with(nolock) on I.InvoiceId = pi.InvoiceId where LTRIM(RTRIM(I.CreditNoteNo)) = '" + txtCreditNoteNo.Text.Trim()+ "' and pi.SupplierId = " + SupplierId + " And I.InvoiceId != " + InvoiceId+" ";

                //DataTable dtCreditNoteExits = Common.Execute_Procedures_Select_ByQuery(sql);
                //if (dtCreditNoteExits.Rows.Count > 0)
                //{
                //    lblCreditNoteMsg.Text = "Credit Note is already linked to Invoice Ref No :" + dtCreditNoteExits.Rows[0]["RefNo"].ToString(); 
                //    return;
                //}

                string FileName = Path.GetFileName(fuAttachment.PostedFile.FileName);
                string fileContent = fuAttachment.PostedFile.ContentType;
                Stream fs = fuAttachment.PostedFile.InputStream;
                BinaryReader br = new BinaryReader(fs);
                byte[] bytes = br.ReadBytes((Int32)fs.Length);
                Common.Set_Procedures("InsertUpdateInvoiceCreditNotesDetails");
                Common.Set_ParameterLength(8);
                Common.Set_Parameters(
                    new MyParameter("@CreditNoteId", creditnoteid),
                    new MyParameter("@InvocieId", InvoiceId),
                    new MyParameter("@CreditNoteNo", txtCreditNoteNo.Text.Trim()),
                    new MyParameter("@CreditNoteCurrency", lblCreditNoteCurrency.Text.Trim()),
                    new MyParameter("@CreditNoteAmount", Common.CastAsDecimal(txtcreditNoteAmt.Text.Trim())),
                    new MyParameter("@DocName", FileName),
                    new MyParameter("@Attachment", bytes),
                    new MyParameter("@ContentType", fileContent)
                    );
                DataSet ds = new DataSet();
                ds.Clear();
                Boolean res;
                res = Common.Execute_Procedures_IUD(ds);
                if (res)
                {
                    hdnCreditNotesId.Value = ds.Tables[0].Rows[0][0].ToString();
                    BindCreditNotedetails(InvoiceId);
                    Common.Execute_Procedures_Select_ByQuery("UPDATE dbo.Pos_Invoice SET ApprovalAmount=" + txt_InvAmount.Text.Trim() + " where InvoiceId=" + InvoiceId);                  
                    //BindInvoiceDetails();
                    ClearCreditNoteDetails();
                    lblCreditNoteMsg.Text = "Credit Note saved Successfully.";
                    // dVCreditNotes.Visible = false;
                }
                else
                {
                    lbl_popinv_Message.Text = "Sorry ! Only Single PO is allowed to link with one invoice.";
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            lbl_popinv_Message.Text = ex.Message.ToString();
        }
     }
    protected void btnCloseCreditNotes_Click(object sender, EventArgs e)
    {
        dVCreditNotes.Visible = false;
    }

    protected void lbAddCreditNotes_Click(object sender, EventArgs e)
    {
        dVCreditNotes.Visible = true;
        //bindCurrencyddl();
        ClearCreditNoteDetails();
        lblCreditNoteCurrency.Text = lblCurrency.Text;
        
    }
    protected void btnDeleteCreditNote_Click(object sender, EventArgs e)
    {
        int creditnoteId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        Common.Execute_Procedures_Select_ByQuery("DELETE FROM InvoiceCreditNotesDetails WHERE CreditNoteId=" + creditnoteId);
        BindCreditNotedetails(InvoiceId);
    }
    protected void BindCreditNotedetails(int InvocieId)
    {
        string sql = "SELECT * FROM DBO.InvoiceCreditNotesDetails WHERE INVOICEID=" + InvoiceId + "";
        DataTable dtCreditNotes = Common.Execute_Procedures_Select_ByQuery(sql);
        if (dtCreditNotes.Rows.Count > 0)
        {
            divCreditNote.Visible = true;
            rptCreditNoteDetails.DataSource = dtCreditNotes;
            rptCreditNoteDetails.DataBind();

            // Decimal CreditNoteAmount = Common.CastAsDecimal(dtCreditNotes.Compute("SUM(CreditNoteAmount)", ""));
            hdnTotalCreditNoteAmt.Value = Common.CastAsDecimal(dtCreditNotes.Compute("SUM(CreditNoteAmount)", "")).ToString();
            lblTotalCreditnoteamt.Text = hdnTotalCreditNoteAmt.Value;
            Decimal PayableAmount = 0;
            if (chkAdvPayment.Checked)
            {
              PayableAmount = Common.CastAsDecimal(lbl_InvAmount.Text) - Common.CastAsDecimal(hdnTotalCreditNoteAmt.Value) ;
            }
            else
            {
                PayableAmount = Common.CastAsDecimal(lbl_InvAmount.Text) - Common.CastAsDecimal(hdnTotalCreditNoteAmt.Value) - Common.CastAsDecimal(hdnTotalAdvPayment.Value);
            }
            
            txt_InvAmount.Text = PayableAmount.ToString();
            lblCreditNoteAmt.Text = hdnTotalCreditNoteAmt.Value;
            lblApprovedUSDAmount.Text = getUSD(lblCurrency.Text, Common.CastAsDecimal(txt_InvAmount.Text), DateTime.Parse(lbl_InvDate.Text)).ToString();
            lbl_ApprovedAmount.Text = txt_InvAmount.Text;
        }
        else
        {
            divCreditNote.Visible = false;
            rptCreditNoteDetails.DataSource = null;
            rptCreditNoteDetails.DataBind();
        }
    }

    protected void ClearCreditNoteDetails()
    {
        txtCreditNoteNo.Text = "";
        txtcreditNoteAmt.Text = "";
        lblCreditNoteCurrency.Text = "";
    }

    protected void imgEdit_Click(object sender, ImageClickEventArgs e)
    {
        lblCreditNoteMsg.Text = "";
        int editCreditNoteId = 0;
        editCreditNoteId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        if (editCreditNoteId > 0)
        {
            dVCreditNotes.Visible = true;
            hdnCreditNotesId.Value = editCreditNoteId.ToString();
           
            string sql = "Select * from InvoiceCreditNotesDetails with(nolock) where CreditNoteId  =  " + editCreditNoteId ;
            DataTable Dt = Common.Execute_Procedures_Select_ByQuery(sql);
            if (Dt.Rows.Count > 0)
            {
                
                if (!string.IsNullOrWhiteSpace(Dt.Rows[0]["CreditNoteNo"].ToString()))
                {
                    txtCreditNoteNo.Text = Dt.Rows[0]["CreditNoteNo"].ToString();
                }
                if (!string.IsNullOrWhiteSpace(Dt.Rows[0]["CreditNoteCurrency"].ToString()))
                {
                    lblCreditNoteCurrency.Text = Dt.Rows[0]["CreditNoteCurrency"].ToString();
                }
                if (!string.IsNullOrWhiteSpace(Dt.Rows[0]["CreditNoteAmount"].ToString()))
                {
                    txtcreditNoteAmt.Text = Dt.Rows[0]["CreditNoteAmount"].ToString();
                }
               
               
            }
        }
    }

    protected void BindAdvancePaymentDetails()
    {
        hdnTotalAdvPayment.Value = "0";
        string bids = "";
        foreach (DataRow dr in PoNos.Rows)
        {
            bids += "," + dr["BIDID"].ToString();
        }
        if (bids.StartsWith(","))
            bids = bids.Substring(1);
        string sql = "Select pia.APid,pi.InvNo,pi.InvDate, pia.PaidAmount,pipp.BidId,pi.Attachment,pi.AttachmentName,pip.Currency,pi.RefNo,pia.InvoiceId from POS_Invoice_AdvancePayment pia with(nolock) Inner join POS_Invoice pi with(nolock) on pia.InvoiceId = Pi.InvoiceId inner join POS_Invoice_Payment_PO pipp with(nolock) on pi.InvoiceId = pipp.InvoiceId  Inner join POS_Invoice_Payment pip with(nolock) on pip.PaymentId = pia.paymentid where Isnull(pi.IsAdvPayment,0) = 1 and pipp.BidId in ('" + bids + "' ) " ;

        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
        if (dt.Rows.Count >0)
        {
            divAdvPayment.Visible = true;
            rptAdvancePayment.DataSource = dt;
            rptAdvancePayment.DataBind();
            string sql1 = "Select SUM(pia.PaidAmount) As PaidAmount from POS_Invoice_AdvancePayment pia with(nolock) Inner join POS_Invoice pi with(nolock) on pia.InvoiceId = Pi.InvoiceId inner join POS_Invoice_Payment_PO pipp with(nolock) on pi.InvoiceId = pipp.InvoiceId  Inner join POS_Invoice_Payment pip with(nolock) on pip.PaymentId = pia.paymentid where Isnull(pi.IsAdvPayment,0) = 1 and pipp.BidId in ('" + bids + "') and IsPaymentSettle = 0 ";
            DataTable dtAdvAmt = Common.Execute_Procedures_Select_ByQuery(sql1);

            if (dtAdvAmt.Rows.Count > 0)
            {
                hdnTotalAdvPayment.Value = dtAdvAmt.Rows[0]["PaidAmount"].ToString();
            }

            //  Decimal TotalAdvPayAmount = Common.CastAsDecimal(dt.Compute("SUM(PaidAmount)", ""));
            Decimal PayableAmount = 0;
          if (chkAdvPayment.Checked)
            {
                PayableAmount = Common.CastAsDecimal(lbl_InvAmount.Text) - Common.CastAsDecimal(hdnTotalCreditNoteAmt.Value);
                lblAdvPayment.Text = "0";
            }
          else
            {
                 PayableAmount = Common.CastAsDecimal(lbl_InvAmount.Text) - Common.CastAsDecimal(hdnTotalCreditNoteAmt.Value) - Common.CastAsDecimal(hdnTotalAdvPayment.Value);
                lblAdvPayment.Text = hdnTotalAdvPayment.Value;
            }
            txt_InvAmount.Text = PayableAmount.ToString();
            lblApprovedUSDAmount.Text = getUSD(lblCurrency.Text, Common.CastAsDecimal(txt_InvAmount.Text), DateTime.Parse(lbl_InvDate.Text)).ToString();
            lbl_ApprovedAmount.Text = txt_InvAmount.Text;
        }
        else
        {
            divAdvPayment.Visible = false; 
            rptAdvancePayment.DataSource = null;
            rptAdvancePayment.DataBind();
        }
    }

    protected void btnViewGoodsRcv_OnClick(object sender, EventArgs e)
    {
        try
        {
            // Response.Redirect("ReceivePO.aspx?BidId=" + BidId.ToString() + ""); 
            //Button _btnViewGoodsRcv = (Button)sender;

            int PoBidiD = Common.CastAsInt32(((Button)sender).Attributes["BidId"].ToString());
        ScriptManager.RegisterStartupScript(this, this.GetType(), "RecieveGoods", "window.open('../Requisition/ReceivePO.aspx?BidId=" + PoBidiD.ToString() + "&Page=INV&InvoiceId="+InvoiceId.ToString()+"');", true);
        }
        catch (Exception ex)
        {
            lbl_inv_Message.Text = ex.Message.ToString();
        }
    }

    protected void btnInvoice_OnClick(object sender, EventArgs e)
    {
        try
        {
            int PoBidiD = Common.CastAsInt32(((Button)sender).Attributes["BidId"].ToString());
            //  Response.Redirect("~/Modules/Purchase/Invoice/InvoiceEntry.aspx?BidId=" + BidId.ToString()+ "");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PO-Invoice", "window.open('../Invoice/InvoiceEntry.aspx?BidId=" + PoBidiD.ToString() + "&Page=INV&InvoiceId="+InvoiceId.ToString()+"');", true);
        }
        catch(Exception ex)
        {
            lbl_inv_Message.Text = ex.Message.ToString();
        }
        //Response.Redirect("InvoiceEntry.aspx?BidId=" + BidId.ToString() + ""); 
    }
    protected void ddldepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindAccount();
    }

    protected bool InsertApEntirsforNonPo(int invoiceid, int nonpoid, int accountid, string description)
    {
        bool status = false;
        if (nonpoid > 0)
        {
            Common.Set_Procedures("Sp_InsertUpdateNonPoApEntries");
            Common.Set_ParameterLength(4);
            Common.Set_Parameters(
                new MyParameter("@NonPoId", nonpoid),
                new MyParameter("@InvoiceId", invoiceid),
                new MyParameter("@AccountId", accountid),
                new MyParameter("@Remarks", description.Trim())
                );

            DataSet ds = new DataSet();
            ds.Clear();
            Boolean res;
            res = Common.Execute_Procedures_IUD(ds);
            if (res)
            {
                status =  true;
            }
           
        }
        return status;
    }


  
}