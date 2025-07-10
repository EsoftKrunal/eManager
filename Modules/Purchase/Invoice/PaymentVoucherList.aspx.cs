using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


public partial class Invoice_PaymentVoucherList : System.Web.UI.Page
{

    public int PaymentId
    {
        get { return Convert.ToInt32(ViewState["PaymentId"]); }
        set { ViewState["PaymentId"] = value; }
    }
    public int TableId
    {
        get { return Convert.ToInt32(ViewState["TableId"]); }
        set { ViewState["TableId"] = value; }
    }
    public int UserId
    {
        get { return Convert.ToInt32(ViewState["UserId"]); }
        set { ViewState["UserId"] = value; }
    }
    
    public DataTable dtRows
    {
        get {
            if (ViewState["Data"] == null)
            {
                DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT *,'A' as Status FROM POS_Payment_Details WHERE PAYMENTID=" + PaymentId);
                ViewState["Data"] = dt;
            }
            return (DataTable)ViewState["Data"];
            
        }
        set { ViewState["Data"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //---------------------------------------
        ProjectCommon.SessionCheck();
        //---------------------------------------
        lblMsgMain.Text = "";
        lblMsgPOP.Text="";
        lblmsg1.Text = "";
        if (!Page.IsPostBack)
        {
            UserId = Common.CastAsInt32(Session["loginid"]);
            DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT Payment FROM POS_Invoice_mgmt where USERID=" + UserId);

            if ((dt.Rows.Count > 0 && dt.Rows[0]["Payment"].ToString() == "True") || UserId==1)
            {
                string SQL = "select (FirstName + ' ' + LastName ) AS UserName, LoginId  from dbo.usermaster where loginid in (select userid from pos_invoice_mgmt where payment=1) AND statusId='A' Order By UserName";
                DataTable dtPUsers = Common.Execute_Procedures_Select_ByQuery(SQL);

                this.ddlPaidUsers.DataValueField = "LoginId";
                this.ddlPaidUsers.DataTextField = "UserName";
                this.ddlPaidUsers.DataSource = dtPUsers;
                this.ddlPaidUsers.DataBind();
                ddlPaidUsers.Items.Insert(0, new ListItem("< All Users >", "0"));

                ddlPaidUsers.SelectedValue = UserId.ToString();
                
                bindOwnerddl();
                btn_Search_Click(sender, e);
            }
            else
            {
                Response.Redirect("~/NoPermission.aspx");
            }
        }
    }
    protected void btnDownloadFile_Click(object sender, EventArgs e)
    {
        int InvId = Common.CastAsInt32(hfInvId.Value);
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT AttachmentName,Attachment FROM POS_Invoice WHERE InvoiceId=" + InvId.ToString());
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

    //protected void bindCurrencyddl()
    //{
    //    DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT Curr FROM [dbo].[VW_tblWebCurr]");
    //    this.ddCurrency.DataValueField = "Curr";
    //    this.ddCurrency.DataTextField = "Curr";
    //    this.ddCurrency.DataSource = dt;
    //    this.ddCurrency.DataBind();
    //    ddCurrency.Items.Insert(0, new ListItem("< Currency >", "0"));
    //}
    protected void bindOwnerddl()
    {
        //DataTable dt = Common.Execute_Procedures_Select_ByQuery("select OwnerId,OwnerName from dbo.Owner where statusid='A' order by OwnerCode ");
        //ddlF_Owner.DataSource = dt;
        //ddlF_Owner.DataValueField = "OwnerId";
        //ddlF_Owner.DataTextField = "OwnerName";
        //ddlF_Owner.DataBind();
        //ddlF_Owner.Items.Insert(0, new ListItem("All", "0"));

        
        //ddlOwner1.DataSource = dt;
        //ddlOwner1.DataValueField = "OwnerId";
        //ddlOwner1.DataTextField = "OwnerName";
        //ddlOwner1.DataBind();
        //ddlOwner1.Items.Insert(0, new ListItem("All", ""));

        DataTable dt = Common.Execute_Procedures_Select_ByQuery("select COMPANY,COMPANY + ' - ' + [COMPANY NAME] AS 'COMPANY NAME'from [dbo].[AccountCompany] where active='Y' ORDER BY [COMPANY NAME]");
        ddlF_Owner.DataSource = dt;
        ddlF_Owner.DataValueField = "company";
        ddlF_Owner.DataTextField = "Company Name";
        ddlF_Owner.DataBind();
        ddlF_Owner.Items.Insert(0, new ListItem("All", "0"));

        ddlOwner1.DataSource = dt;
        ddlOwner1.DataValueField = "company";
        ddlOwner1.DataTextField = "Company Name";
        ddlOwner1.DataBind();
        ddlOwner1.Items.Insert(0, new ListItem("All", ""));


    }

    
    protected void ShowRecord()
    {
        btnSaveVoucher.Visible = false;
        btnCreatePV.Visible = false;

        string SQL = "select P.*,(case when P.SupplierId>0 then SupplierName eLSE p.VendorName END) AS SupplierName,TravID from POS_Payment P inner join [dbo].[VW_tblSMDSuppliers] S on S.SupplierId=P.SupplierId WHERE P.PAYMENTID=" + PaymentId;
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
        if (dt.Rows.Count > 0)
        {
            DataRow dr = dt.Rows[0];
            txtVendorName.Text = dr["SupplierName"].ToString();
            hfdSupplierId.Value = dr["SupplierId"].ToString();
            chkNotinList.Checked = (Common.CastAsInt32(hfdSupplierId.Value) == 0);
            lblPVNo.Text = dr["PVNo"].ToString();
            rad_SGD.Checked = dr["PaymentType"].ToString() == "S";
            rad_USD.Checked = dr["PaymentType"].ToString() == "U";
            rad_INR.Checked = dr["PaymentType"].ToString() == "R";
            ddlOwner1.SelectedValue = dr["POSOwnerId"].ToString();
            txtBankName.Text = dr["BankName"].ToString();
            txtCreditActNo.Text = dr["CreditActNo"].ToString();
            txtCreditActName.Text = dr["CreditActName"].ToString();
            txtVoucherNo.Text = dr["VoucherNo"].ToString();
            txtChequeTTNo.Text = dr["ChequeTTNo"].ToString();
            txtChequeTTAmt.Text = dr["ChequeTTAmount"].ToString();
            txtChequeTTDate.Text = Common.ToDateString(dr["ChequeTTDt"]);
            txtBankCharges.Text = dr["BankCharges"].ToString();
            txtComments.Text = dr["Remarks"].ToString();
            
            btnSaveVoucher.Visible = Convert.IsDBNull(dr["ApprovedBy"]); 
            btnCreatePV.Visible = (lblPVNo.Text.Trim()=="");
        }
        else 
        {
            txtVendorName.Text = "";
            hfdSupplierId.Value = "";
            chkNotinList.Checked = false;
            lblPVNo.Text = "";
            rad_SGD.Checked = false;
            rad_USD.Checked = false;
            ddlOwner1.SelectedIndex = 0;
            txtBankName.Text = "";
            txtCreditActNo.Text = "";
            txtCreditActName.Text = "";
            txtVoucherNo.Text = "";
            txtChequeTTNo.Text = "";
            txtChequeTTAmt.Text = "";
            txtChequeTTDate.Text = "";
            txtBankCharges.Text = "";
            txtComments.Text = "";

            btnSaveVoucher.Visible = true;
            btnCreatePV.Visible = false;
        }
        dtRows = null;        
        BindGrid();
    }
    protected void btn_Add_Click(object sender, EventArgs e)
    {
        PaymentId = 0;
        txtSRNo.Text = "1";
        ddlOwner1.Enabled = true;
        rad_USD.Enabled = true;
        rad_SGD.Enabled = true;
        rad_INR.Enabled = true;
        ShowRecord();
        btnSaveVoucher.Visible = true;
        btnVoucherPrint.Visible = false;
        dv_NewPV.Visible = true;
        txtVendorName.Focus();
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            PaymentId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        }
        catch
        {
            PaymentId = Common.CastAsInt32(((LinkButton)sender).CommandArgument);
        }

        ddlOwner1.Enabled = false;
        rad_USD.Enabled = false;
        rad_SGD.Enabled = false;
        rad_INR.Enabled = false;
        ShowRecord();
        btnSaveVoucher.Visible = true;
        btnVoucherPrint.Visible = true;
        dv_NewPV.Visible = true;
        txtVendorName.Focus();
    }
    protected void ShowMessage(Label l1, string Msg, bool error)
    {
        l1.Text = Msg;
        l1.ForeColor = (error) ? System.Drawing.Color.Red : System.Drawing.Color.Green;
    }
    protected void btnSaveVoucher_Click(object sender, EventArgs e)
    {
        if (!(rad_SGD.Checked || rad_USD.Checked || rad_INR.Checked))
        {
            ShowMessage(lblMsgPOP, "Please select payment mode.", true);
            rad_SGD.Focus();
            return;
        }
        if (txtVendorName.Text.Trim() == "")
        {
            ShowMessage(lblMsgPOP,"Please select vendor.",true);
            txtVendorName.Focus();
            return;
        }

        if (!chkNotinList.Checked && Common.CastAsInt32(hfdSupplierId.Value) <= 0)
        {
            ShowMessage(lblMsgPOP, "Please select not in list.", true);
            chkNotinList.Focus();
            return;
        }

        try
        {
            Common.Set_Procedures("Inv_InsertPayment_1_001");
            Common.Set_ParameterLength(15);
            Common.Set_Parameters(
                new MyParameter("@PaymentId", PaymentId),
                new MyParameter("@SupplierId", Common.CastAsInt32(hfdSupplierId.Value)),
                new MyParameter("@SupplierName", txtVendorName.Text.Trim()),                 
                new MyParameter("@BankName", txtBankName.Text.Trim()),
                new MyParameter("@CreditActNo", txtCreditActNo.Text.Trim()),
                new MyParameter("@CreditActName", txtCreditActName.Text.Trim()),
                new MyParameter("@ChequeTTNo", txtChequeTTNo.Text.Trim()),
                new MyParameter("@ChequeTTDt", txtChequeTTDate.Text.Trim()),
                new MyParameter("@ChequeTTAmount", Common.CastAsDecimal(txtChequeTTAmt.Text)),
                new MyParameter("@BankCharges", Common.CastAsDecimal(txtBankCharges.Text)),
                new MyParameter("@VoucherNo", txtVoucherNo.Text.Trim()),
                new MyParameter("@Remarks", txtComments.Text.Trim()),
                new MyParameter("@PaidBy", UserId),
                new MyParameter("@PaymentType", ((rad_SGD.Checked)?"S":((rad_USD.Checked)?"U":"R"))),
                new MyParameter("@POSOwnerId", ddlOwner1.SelectedValue)
                );
            
            DataSet ds = new DataSet();
            ds.Clear();
            Boolean res;
            res = Common.Execute_Procedures_IUD(ds);
            if (res)
            {

                PaymentId = Common.CastAsInt32(ds.Tables[0].Rows[0][0]);
                foreach(DataRow dr in dtRows.Rows)
                {
                    Common.Set_Procedures("Inv_InsertPayment_1_Details_001");
                    Common.Set_ParameterLength(9);
                    Common.Set_Parameters(
                        new MyParameter("@TableId",dr["TableId"]),
                        new MyParameter("@SrNo", dr["SrNo"]),
                        new MyParameter("@PaymentId", PaymentId),
                        new MyParameter("@Description", dr["Description"]),
                        new MyParameter("@InvoiceNo", dr["InvoiceNo"]),
                        new MyParameter("@OnDate", dr["Ondate"]),
                        new MyParameter("@AccountNo", dr["AccountNo"]),
                        new MyParameter("@Amount", dr["Amount"]),
                        new MyParameter("@Status", dr["Status"]));
                    DataSet ds1=new DataSet();
                    Common.Execute_Procedures_IUD(ds1);
                }

                ShowMessage(lblMsgMain, "Record Successfully Saved.", false);
                //btnSaveVoucher.Visible = false;
                btnCreatePV.Visible = true;
                btnVoucherPrint.Visible = true;
                //dv_NewPV.Visible = false;
                btn_Search_Click(sender, e);
            }
            else
            {
                ShowMessage(lblMsgPOP, "Unable to save record. Error : " + Common.ErrMsg, true);
            }
        }
        catch (Exception ex)
        {
            ShowMessage(lblMsgPOP, "Unable to save record." + ex.Message + Common.getLastError(), true);
        }


        //dv_NewPV.Visible = false;
    }
    protected void btnClosePOP_Click(object sender, EventArgs e)
    {
        dv_NewPV.Visible = false;
    }
    
    protected void btnAskCancel_Click(object sender, EventArgs e)
    {
        int _PaymentId = 0;
        string _VoucherType = "";
        try
        {
            _PaymentId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
            _VoucherType = ((ImageButton)sender).Attributes["VoucherType"];
        }
        catch
        {
            _PaymentId = Common.CastAsInt32(((LinkButton)sender).CommandArgument);
            _VoucherType = ((LinkButton)sender).Attributes["VoucherType"];
        }
        //-------------------------
        if(_PaymentId>0)
        {
           
            string sql= "SELECT ISNULL(PVNO,'') FROM VW_PAYMENTVOUCHERS_001 WHERE PaymentId=" + _PaymentId.ToString();
            DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
            if (dt.Rows.Count > 0)
            {
                string pvno = dt.Rows[0][0].ToString();
                if (pvno.Trim() == "") // PERMANANT DELETION
                {
                    if (_VoucherType == "N")
                    {
                        Common.Execute_Procedures_Select_ByQuery("DELETE FROM DBO.POS_Payment WHERE PAYMENTID=" + _PaymentId.ToString());
                    }
                    else
                    {
                        Common.Execute_Procedures_Select_ByQuery("DELETE FROM DBO.POS_Invoice_Payment WHERE PAYMENTID=" + _PaymentId.ToString());
                    }
                    btn_Search_Click(sender, e);
                }
                else // virtual deletion
                {
                    ViewState["_PaymentId"] = _PaymentId;
                    ViewState["_VoucherType"] = _VoucherType;

                    dvCancel.Visible = true;
                    lblCVno.Text = pvno;
                }
            }
        }
    }
    
    protected void btnCancelNow_Click(object sender, EventArgs e)
    {
        int _PaymentId=Common.CastAsInt32(ViewState["_PaymentId"]);
        string _VoucherType = ViewState["_VoucherType"].ToString();
        ViewState["_VoucherType"] = _PaymentId;
        Common.Set_Procedures("Cancel_PV");
        Common.Set_ParameterLength(4);
        Common.Set_Parameters(
                new MyParameter("@VoucherType", _VoucherType),
                new MyParameter("@PaymentId", _PaymentId),
                new MyParameter("@CancelledBy", Session["UserName"].ToString()),
                new MyParameter("@Reason", txtCReason.Text.Trim()));
        DataSet ds = new DataSet();
        if (Common.Execute_Procedures_IUD(ds))
        {
            btn_Search_Click(sender, e);
            dvCancel.Visible = false;
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "fdsafsdf", "alert('" + Common.getLastError() +"');", true);
        }
    }

    protected void btnCloseNow_Click(object sender, EventArgs e)
    {
        dvCancel.Visible = false;
    }
    protected void btn_Search_Click(object sender, EventArgs e)
    {
        string SQL = "";
        string Where = "";

        SQL = "select * FROM VW_PAYMENTVOUCHERS_001 ";

        if (UserId == 1 || ddlPaidUsers.SelectedIndex==0)
        {
            SQL += " WHERE 1=1 AND PaymentStage = 2 ";
        }
        else
        {

            SQL += " WHERE 1=1 AND PaymentStage = 2 "; //PaidById = " + ddlPaidUsers.SelectedValue;
        }
       
        if (txtF_Vendor.Text.Trim() != "")
        {
            Where += " AND SupplierName LIKE '%" + txtF_Vendor.Text.Trim() + "%' ";
        }
        

        if (txtF_PVNo.Text.Trim() != "")
        {
            Where += " AND PVNo ='" + txtF_PVNo.Text.Trim() + "' ";
        }

        string StatusClause = "";
        string StatusClause1 = "";

        if (ddlStatus.SelectedValue=="P")
        {
            StatusClause = " ( isnull(PVNO,'') <> '' ) ";
        }
        else
        {
            if (ddlStatus.SelectedIndex > 0)
                StatusClause = " ( isnull(STATUS,'P') ='" + ddlStatus.SelectedValue + "' ) ";
        }
        if (ddlStatus1.SelectedIndex > 0)
        {
            if (ddlStatus1.SelectedValue == "P")
                StatusClause1 = " isnull(STATUS,'P') = 'A' AND BankConfirmedOn is NULL";
            else
                StatusClause1 = " isnull(STATUS,'P') = 'A' AND BankConfirmedOn is NOT NULL";
        }
        if (StatusClause != "" && StatusClause1 != "")
            Where += " AND ( " + StatusClause + " OR " + StatusClause1 + " ) ";
        else if (StatusClause == "" && StatusClause1 == "")
            Where += "";
        else if (StatusClause != "" || StatusClause1 != "")
            Where += " AND ( " + StatusClause + StatusClause1 + " ) ";

        if (ddlF_Owner.SelectedIndex > 0)
        {
            Where += " AND POSOwnerId ='" + ddlF_Owner.SelectedValue + "' ";
        }
        
        if (txtF_D1.Text!="")
        {
            Where += " AND PAIDON >='" + txtF_D1.Text + "' ";
        }

        if (txtF_D2.Text!="")
        {
            Where += " AND PAIDON <='" + Convert.ToDateTime(txtF_D2.Text).AddDays(1).ToString("dd-MMM-yyyy") + "' ";
        }
        //if (ddlF_Vessel.SelectedIndex > 0)
        //{
        //    Where += " AND VesselId =" + ddlF_Vessel.SelectedValue + " ";
        //}
      
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL + Where + " Order By PaymentId Desc");

        RptMyInvoices.DataSource = dt;
        RptMyInvoices.DataBind();
    }
    
    protected void btnAddRow_Click(object sender, EventArgs e)
    {
        if (TableId != 0)
        {
            DataRow[] drs = dtRows.Select("TableId=" + TableId);
            if (drs.Length > 0)
            {
                DataRow dr = drs[0];
                dr["SRNo"] = Common.CastAsInt32(txtSRNo.Text);
                dr["Description"] = txtDescr.Text;
                dr["InvoiceNo"] = txtInvoiceNo.Text;
                dr["OnDate"] = txtOnDate.Text;
                dr["AccountNo"] = txtAcNo.Text;
                dr["Amount"] = Common.CastAsDecimal(txtAmount.Text);

                txtDescr.Text = "";
                txtOnDate.Text = "";
                txtAmount.Text = "";
                txtInvoiceNo.Text = "";
                txtAcNo.Text = "";
                txtSRNo.Text = "";
            }
        }
        else
        {
            DataRow dr = dtRows.NewRow();
            dr["TableId"] = -(dtRows.Rows.Count + 1);
            dr["SRNo"] = Common.CastAsInt32(txtSRNo.Text);
            dr["Description"] = txtDescr.Text;
            dr["InvoiceNo"] = txtInvoiceNo.Text;
            dr["OnDate"] = txtOnDate.Text;
            dr["AccountNo"] = txtAcNo.Text;
            dr["Amount"] = Common.CastAsDecimal(txtAmount.Text);
            dr["Status"] = "A";

            dtRows.Rows.Add(dr);

            txtDescr.Text = "";
            txtOnDate.Text = "";
            txtAmount.Text = "";
            txtInvoiceNo.Text = "";
            txtAcNo.Text = "";
            txtSRNo.Text = (Common.CastAsInt32(txtSRNo.Text) + 1).ToString();
        }
        TableId = 0;
        BindGrid();
    }
    protected void btnEditRow_Click(object sender, EventArgs e)
    {
        int _TableId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        DataRow[] dr = dtRows.Select("TableId=" + _TableId);
        if (dr.Length > 0)
        {
            txtDescr.Text = dr[0]["Description"].ToString();
            txtOnDate.Text = Common.ToDateString(dr[0]["OnDate"]);
            txtAmount.Text = dr[0]["Amount"].ToString();
            txtInvoiceNo.Text = dr[0]["InvoiceNo"].ToString();
            txtAcNo.Text = dr[0]["AccountNo"].ToString();
            txtSRNo.Text = dr[0]["SRNo"].ToString();
            TableId = _TableId;
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        int _TableId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        DataRow[] drs = dtRows.Select("TableId=" + _TableId);
        foreach (DataRow dr in drs)
        {
            dr["Status"] = "D";
        }
        dtRows.AcceptChanges();
        BindGrid();
    }
    protected void BindGrid()
    {
        DataView dv = dtRows.DefaultView;
        dv.RowFilter = "Status='A'";
        dv.Sort = "SRNO";
        RptDetails.DataSource = dtRows;
        RptDetails.DataBind();

        lbltotal.Text = dtRows.Compute("sum(Amount)", "").ToString();
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtF_InvNo.Text = "";
        txtF_PVNo.Text = "";
        txtF_Vendor.Text = "";

        ddlF_Owner.SelectedIndex = 0;
    }
    protected void imgBtnShowUpdateUser_Click(object sender, EventArgs e)
    {

    }
    protected void btnVoucherPrint_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(),"fas", "PrintVoucherN(" + PaymentId + ");", true);
    }
    
    protected void btnCreatePV_Click(object sender, EventArgs e)
    {
        Common.Set_Procedures("Inv_InsertPayment_1_001_Approval_001");
        Common.Set_ParameterLength(4);
        Common.Set_Parameters(
                new MyParameter("@PaymentId", PaymentId),
                new MyParameter("@ApprovedBy", Session["UserFullName"].ToString()),
                new MyParameter("@ApprovedOn", DateTime.Today),
                new MyParameter("@Remarks", txtComments.Text.Trim()));
       

        DataSet ds = new DataSet();
        if(Common.Execute_Procedures_IUD(ds))
        {
            btn_Search_Click(sender, e);
            ShowRecord();
            lblMsgPOP.Text = "Payment voucher no generated successfully.";
        }
        
    }

    //------------------
    //[System.Web.Services.WebMethod]
    //public static string UpdateAmount(string RecordType,int Paymentid,decimal BankCharges, decimal BankAmount,string BankConfirmedOn)
    //{
    //    try
    //    {
    //        if (RecordType == "N")
    //            Common.Execute_Procedures_Select_ByQuery("UPDATE DBO.POS_Payment SET BankCharges=" + BankCharges + " ,BankAmount=" + BankAmount + ",BankConfirmedOn='"+ BankConfirmedOn + "' WHERE PAYMENTID=" + Paymentid);
    //        else
    //            Common.Execute_Procedures_Select_ByQuery("UPDATE DBO.POS_Invoice_Payment  SET BankCharges=" + BankCharges + " ,BankAmount=" + BankAmount + " ,BankConfirmedOn='" + BankConfirmedOn + "' WHERE PAYMENTID=" + Paymentid);

    //        return "Y";
    //    }
    //    catch (Exception ex)        
    //    {
    //        return "N";
    //    }

    //}

    protected void imgUpdateAmountOpenPop_OnClick(object sender, EventArgs e)
    {
        dvUpdate.Visible = true;
        ImageButton btn = (ImageButton)sender;
        HiddenField hfBankCharges = (HiddenField)btn.Parent.FindControl("hfBankCharges");
        HiddenField hfBankAmount = (HiddenField)btn.Parent.FindControl("hfBankAmount");
        HiddenField hfRecordType = (HiddenField)btn.Parent.FindControl("hfRecordType");
        HiddenField hfPaymentId = (HiddenField)btn.Parent.FindControl("hfPaymentId");
        hfhfRecordType_popup.Value = hfRecordType.Value;
        PaymentId = Common.CastAsInt32(hfPaymentId.Value);

        BankConfirmedOnPopup.Text = "";
        txtBankChargesPopup.Text = hfBankCharges.Value;
        txtBankChargesPopup_OnTextChanged(sender,e);
        BindRptAmount(hfRecordType.Value, PaymentId);

        txtCreditAccountCode.Text = "";
        txtDebitAccountCode.Text = "";
        BankConfirmedOnPopup.Text = "";
        btnOpenExport.Visible = false;
        btnUpdateAmount.Visible = true;
    }
    protected void btnCloseUpdateAmountPop_OnClick(object sender, EventArgs e)
    {
        dvUpdate.Visible = false;
        PaymentId = 0;
        btn_Search_Click(sender, e);
    }
    protected void btnUpdateAmount_OnClick(object sender, EventArgs e)
    {
        if (BankConfirmedOnPopup.Text.Trim() == "")
        {
            lblmsg1.Text = "Please enter bank confirmation date.";
            BankConfirmedOnPopup.Focus();
            return;
        }
        else if (txtBankChargesPopup.Text.Trim() == "")
        {
            lblmsg1.Text = "Please enter bank charges. If no charges you can fill zero.";
            txtBankChargesPopup.Focus();
            return;
        }
        else if (Common.CastAsInt32(txtBankChargesPopup.Text.Trim()) != 0)
        {
            if (txtCreditAccountCode.Text.Trim().Length != 4)
            {
                lblmsg1.Text = "Please enter valid account code for credit account.";
                txtCreditAccountCode.Focus();
                return;
            }
            else if (txtDebitAccountCode.Text.Trim().Length != 4)
            {
                lblmsg1.Text = "Please enter valid account code for debit account.";
                txtDebitAccountCode.Focus();
                return;
            }
        }

        try
        {
            Common.Execute_Procedures_Select_ByQuery("UPDATE DBO.POS_Invoice_Payment SET BankCharges=" + Common.CastAsDecimal(txtBankChargesPopup.Text.Trim()) + " ,BankConfirmedOn='" + BankConfirmedOnPopup.Text.Trim() + "',BankChargesPayableTo='" + ddlVessels.SelectedValue + "',CreditAccountCode='" + txtCreditAccountCode.Text + txtCVesselNo.Text + "',DebitAccountCode='" + txtDebitAccountCode.Text + txtDVesselNo.Text + "' WHERE PAYMENTID=" + PaymentId);
            foreach (RepeaterItem itm in rptAmount.Items)
            {
                TextBox txtAmount = (TextBox)itm.FindControl("txtAmount");
                HiddenField hfdtableid = (HiddenField)itm.FindControl("hfdtableid");
                int TableId = Common.CastAsInt32(hfdtableid.Value);

                Common.Execute_Procedures_Select_ByQuery("UPDATE DBO.POS_Invoice_Payment_Invoices SET BankAmount=" + Common.CastAsDecimal(txtAmount.Text.Trim()) + " WHERE PAYMENTID=" + PaymentId + " AND INVOICEID=" + TableId);
              
            }
            lblmsg1.Text = "Updated successfully.";
            btnOpenExport.Visible = true;
            btnUpdateAmount.Visible = false;
        }
        catch (Exception ex)
        {
            lblmsg1.Text = ex.Message;
        }

    }
    public void BindRptAmount(string RecordType, int PaymentID)
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("select * from VW_PAYMENTVOUCHERS_001 where PTYPE='" + RecordType + "' and PAYMENTID=" + PaymentId);
        if (dt.Rows.Count > 0)
        {
            lblPVNO1.Text = dt.Rows[0]["PVNO"].ToString();

            lblVendorName.Text = dt.Rows[0]["SupplierName"].ToString();
            lblVendorCode.Text = dt.Rows[0]["Travid"].ToString();

            lblOwner.Text = dt.Rows[0]["OwnerName"].ToString();
            lblOwnerCode.Text = dt.Rows[0]["POSOWNERID"].ToString();

            lblpaycurr.Text = (dt.Rows[0]["PaymentType"].ToString() == "U") ? "USD" : ((dt.Rows[0]["PaymentType"].ToString() == "S") ? "SGD" : "INR");
            lblpaycurr1.Text = lblpaycurr.Text;
            lblpaycurr2.Text = lblpaycurr.Text;
        }

        string SQL = "";
        SQL = " SELECT I.INVVesselCode,P.InvoiceId as TableId,I.InvNo as InvoiceNo,I.InvDate as OnDate,I.ApprovalAmount as Amount,P.BankAmount,i.Currency FROM POS_Invoice_Payment_Invoices P  inner  join POS_Invoice I on P.InvoiceId=I.InvoiceId  where PaymentID=" + PaymentID;
        dt = Common.Execute_Procedures_Select_ByQuery(SQL);
        if (dt.Rows.Count > 0)
        {
            lblinvcurr.Text = dt.Rows[0]["Currency"].ToString();
        }
        
        
        rptAmount.DataSource = dt;
        rptAmount.DataBind();
        lblTotalInvAmount.Text = dt.Compute("sum(Amount)", "").ToString();
        lblTotalBankAmount.Text = dt.Compute("sum(BankAmount)","").ToString();

        ddlVessels.DataTextField = "INVVesselCode";
        ddlVessels.DataValueField = "INVVesselCode";
        ddlVessels.DataSource = dt;
        ddlVessels.DataBind();
        ddlVessels_OnSelectedIndexChanged(new object(), new EventArgs());
    }
    protected void txtBankChargesPopup_OnTextChanged(object sender, EventArgs e)
    {
        lblCreditAmount.Text = txtBankChargesPopup.Text;
        lblDebitAmount.Text = txtBankChargesPopup.Text;
        
    }
    protected void ddlVessels_OnSelectedIndexChanged(object sender,EventArgs e)
    {
        if (ddlVessels.SelectedValue != "")
        {
            DataTable dt = Common.Execute_Procedures_Select_ByQuery("select REPLACE(STR(VESSELId,4),' ' ,'0') from Vessel with(nolock) WHERE VesselCode='" + ddlVessels.SelectedValue + "'");
            if (dt.Rows.Count > 0)
            {
                //txtCVesselNo.Text = dt.Rows[0][0].ToString();
                txtDVesselNo.Text = dt.Rows[0][0].ToString();

                up11.Update();
                up12.Update();
            }
        }
    }
    protected void btnOpenExport_OnClick(object sender, EventArgs e)
    {
        DataTable dt1 = Common.Execute_Procedures_Select_ByQuery("SELECT CONVERT(BIT,ISNULL(EXPORTED,0)) AS EXPORTED FROM DBO.POS_Invoice_Payment WHERE PAYMENTID=" + PaymentId);
        if (dt1.Rows.Count > 0)
        {
            if (dt1.Rows[0][0].ToString() == "False") // Not Exported
            {
                //--------------------------
                frm1.Attributes.Add("src", "ExportPopUp.aspx?PaymentId=" + PaymentId);
                dvExport.Visible = true;
            }
        }

    }

    protected void btnCloseNow1_Click(object sender, EventArgs e)
    {
        dvExport.Visible = false;
    }


    protected void btnChangeVendor_Click(object sender, EventArgs e)
    {
        dvChangeVendor.Visible = true;
        //---------------------
        int _PaymentId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("select * from VW_PAYMENTVOUCHERS_001 where PTYPE='O' and PAYMENTID=" + _PaymentId);
        ViewState["_PaymentId"] = _PaymentId;
        if (dt.Rows.Count > 0)
        {
            lblVendorName2.Text = dt.Rows[0]["SupplierName"].ToString();
            lblVendorCode2.Text = dt.Rows[0]["Travid"].ToString();

            txtSupplier.Text = dt.Rows[0]["SupplierName"].ToString();
            hfdSupplier.Value = dt.Rows[0]["SupplierId"].ToString();
        }

        //------------------

    }
    protected void btnchangevendorsave_Click(object sender, EventArgs e)
    {
        int sid = Common.CastAsInt32(hfdSupplier.Value);
        if (sid > 0)
        {
            Common.Set_Procedures("DBO.UpdatePVVendor");
            Common.Set_ParameterLength(2);
            Common.Set_Parameters(
                new MyParameter("@PaymentId", Common.CastAsInt32(ViewState["_PaymentId"])),
                new MyParameter("@SupplierId", sid));

            DataSet ds = new DataSet();
            ds.Clear();
            Boolean res;
            res = Common.Execute_Procedures_IUD(ds);
            if (res)
            {
                lblmsg2.Text = "Vendor updated successfully.";
            }
            else
            {
                lblmsg2.Text = Common.getLastError();
            }
        }
    }
    protected void btnCloseNow2_Click(object sender, EventArgs e)
    {
        btn_Search_Click(sender, e);
        dvChangeVendor.Visible = false;
    }
}