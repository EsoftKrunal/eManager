using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Invoice_ExpenseClaim : System.Web.UI.Page
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
    
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsgMain.Text = "";
        lblMsgPOP.Text="";

        if (!Page.IsPostBack)
        {
            //txtF_D1.Text = DateTime.Today.ToString("01-MMM-yyyy");
            //txtF_D2.Text = DateTime.Today.ToString("dd-MMM-yyyy");

            UserId=Common.CastAsInt32(Session["loginid"]);
            //bindCurrencyddl();
            //bindOwnerddl();
            btn_Search_Click(sender, e);
        }
    }
    protected void btnDownloadFile_Click(object sender, EventArgs e)
    {
        //int LeaveRequestedId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        //DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT FileName,Attachment FROM [dbo].[HR_OfficeAbsence] WHERE LeaveRequestedId=" + LeaveRequestedId);
        //if (dt.Rows.Count > 0)
        //{
        //    string FileName = dt.Rows[0]["FileName"].ToString();
        //    if (FileName.Trim() != "")
        //    {
        //        byte[] buff = (byte[])dt.Rows[0]["Attachment"];
        //        Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileName);
        //        Response.BinaryWrite(buff);
        //        Response.Flush();
        //        Response.End();
        //    }
        //}
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
    //protected void bindOwnerddl()
    //{
    //    DataTable dt = Common.Execute_Procedures_Select_ByQuery("select OwnerId,OwnerName from dbo.Owner where statusid='A' order by OwnerCode ");
    //    ddlF_Owner.DataSource = dt;
    //    ddlF_Owner.DataValueField = "OwnerId";
    //    ddlF_Owner.DataTextField = "OwnerName";
    //    ddlF_Owner.DataBind();
    //    ddlF_Owner.Items.Insert(0, new ListItem("All", "0"));

        
    //    ddlOwner1.DataSource = dt;
    //    ddlOwner1.DataValueField = "OwnerId";
    //    ddlOwner1.DataTextField = "OwnerName";
    //    ddlOwner1.DataBind();
    //    ddlOwner1.Items.Insert(0, new ListItem("All", ""));
        
    //}

    
    //protected void ShowRecord()
    //{
    //    string SQL = "select P.*,(case when P.SupplierId>0 then SupplierName eLSE p.VendorName END) AS SupplierName,TravID from POS_Payment P inner join [dbo].[VW_tblSMDSuppliers] S on S.SupplierId=P.SupplierId WHERE P.PAYMENTID=" + PaymentId;
    //    DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
    //    if (dt.Rows.Count > 0)
    //    {
    //        DataRow dr = dt.Rows[0];
    //        txtVendorName.Text = dr["SupplierName"].ToString();
    //        hfdSupplierId.Value = dr["SupplierId"].ToString();
    //        chkNotinList.Checked = (Common.CastAsInt32(hfdSupplierId.Value) == 0);
    //        lblPVNo.Text = dr["PVNo"].ToString();
    //        rad_SGD.Checked = dr["PaymentType"].ToString() == "S";
    //        rad_USD.Checked = dr["PaymentType"].ToString() == "U";
    //        rad_INR.Checked = dr["PaymentType"].ToString() == "R";
    //        ddlOwner1.SelectedValue = dr["OwnerId"].ToString();
    //        txtBankName.Text = dr["BankName"].ToString();
    //        txtCreditActNo.Text = dr["CreditActNo"].ToString();
    //        txtCreditActName.Text = dr["CreditActName"].ToString();
    //        txtVoucherNo.Text = dr["VoucherNo"].ToString();
    //        txtChequeTTNo.Text = dr["ChequeTTNo"].ToString();
    //        txtChequeTTAmt.Text = dr["ChequeTTAmount"].ToString();
    //        txtChequeTTDate.Text = Common.ToDateString(dr["ChequeTTDt"]);
    //        txtBankCharges.Text = dr["BankCharges"].ToString();
    //        txtComments.Text = dr["Remarks"].ToString();
    //    }
    //    else 
    //    {
    //        txtVendorName.Text = "";
    //        hfdSupplierId.Value = "";
    //        chkNotinList.Checked = false;
    //        lblPVNo.Text = "";
    //        rad_SGD.Checked = false;
    //        rad_USD.Checked = false;
    //        ddlOwner1.SelectedIndex = 0;
    //        txtBankName.Text = "";
    //        txtCreditActNo.Text = "";
    //        txtCreditActName.Text = "";
    //        txtVoucherNo.Text = "";
    //        txtChequeTTNo.Text = "";
    //        txtChequeTTAmt.Text = "";
    //        txtChequeTTDate.Text = "";
    //        txtBankCharges.Text = "";
    //        txtComments.Text = "";
    //    }
    //    dtRows = null;        
    //    BindGrid();
    //}
    //protected void btn_Add_Click(object sender, EventArgs e)
    //{
    //    PaymentId = 0;
    //    txtSRNo.Text = "1";
    //    ddlOwner1.Enabled = true;
    //    rad_USD.Enabled = true;
    //    rad_SGD.Enabled = true;
    //    rad_INR.Enabled = true;
    //    ShowRecord();
    //    btnSaveVoucher.Visible = true;
    //    btnVoucherPrint.Visible = false;
    //    dv_NewPV.Visible = true;
    //    txtVendorName.Focus();
    //}
    //protected void btnEdit_Click(object sender, EventArgs e)
    //{
    //    PaymentId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
    //    ddlOwner1.Enabled = false;
    //    rad_USD.Enabled = false;
    //    rad_SGD.Enabled = false;
    //    rad_INR.Enabled = false;
    //    ShowRecord();
    //    btnSaveVoucher.Visible = true;
    //    btnVoucherPrint.Visible = true;
    //    dv_NewPV.Visible = true;
    //    txtVendorName.Focus();
    //}
    protected void ShowMessage(Label l1, string Msg, bool error)
    {
        l1.Text = Msg;
        l1.ForeColor = (error) ? System.Drawing.Color.Red : System.Drawing.Color.Green;
    }
    protected void btnSaveVoucher_Click(object sender, EventArgs e)
    {
        //if (!(rad_SGD.Checked || rad_USD.Checked || rad_INR.Checked))
        //{
        //    ShowMessage(lblMsgPOP, "Please select payment mode.", true);
        //    rad_SGD.Focus();
        //    return;
        //}
        //if (txtVendorName.Text.Trim() == "")
        //{
        //    ShowMessage(lblMsgPOP,"Please select vendor.",true);
        //    txtVendorName.Focus();
        //    return;
        //}

        //if (!chkNotinList.Checked && Common.CastAsInt32(hfdSupplierId.Value) <= 0)
        //{
        //    ShowMessage(lblMsgPOP, "Please select not in list.", true);
        //    chkNotinList.Focus();
        //    return;
        //}

        //try
        //{
        //    Common.Set_Procedures("Inv_InsertPayment_1");
        //    Common.Set_ParameterLength(15);
        //    Common.Set_Parameters(
        //        new MyParameter("@PaymentId", PaymentId),
        //        new MyParameter("@SupplierId", Common.CastAsInt32(hfdSupplierId.Value)),
        //        new MyParameter("@SupplierName", txtVendorName.Text.Trim()),                 
        //        new MyParameter("@OwnerId", ddlOwner1.SelectedValue),
        //        new MyParameter("@BankName", txtBankName.Text.Trim()),
        //        new MyParameter("@CreditActNo", txtCreditActNo.Text.Trim()),
        //        new MyParameter("@CreditActName", txtCreditActName.Text.Trim()),
        //        new MyParameter("@ChequeTTNo", txtChequeTTNo.Text.Trim()),
        //        new MyParameter("@ChequeTTDt", txtChequeTTDate.Text.Trim()),
        //        new MyParameter("@ChequeTTAmount", Common.CastAsDecimal(txtChequeTTAmt.Text)),
        //        new MyParameter("@BankCharges", Common.CastAsDecimal(txtBankCharges.Text)),
        //        new MyParameter("@MTMVoucherNo", txtVoucherNo.Text.Trim()),
        //        new MyParameter("@Remarks", txtComments.Text.Trim()),
        //        new MyParameter("@PaidBy", UserId),
        //        new MyParameter("@PaymentType", ((rad_SGD.Checked)?"S":((rad_USD.Checked)?"U":"R")))
        //        );
            
        //    DataSet ds = new DataSet();
        //    ds.Clear();
        //    Boolean res;
        //    res = Common.Execute_Procedures_IUD(ds);
        //    if (res)
        //    {

        //        PaymentId = Common.CastAsInt32(ds.Tables[0].Rows[0][0]);
        //        foreach(DataRow dr in dtRows.Rows)
        //        {
        //            Common.Set_Procedures("Inv_InsertPayment_1_Details");
        //            Common.Set_ParameterLength(9);
        //            Common.Set_Parameters(
        //                new MyParameter("@TableId",dr["TableId"]),
        //                new MyParameter("@SrNo", dr["SrNo"]),
        //                new MyParameter("@PaymentId", PaymentId),
        //                new MyParameter("@Description", dr["Description"]),
        //                new MyParameter("@InvoiceNo", dr["InvoiceNo"]),
        //                new MyParameter("@OnDate", dr["Ondate"]),
        //                new MyParameter("@AccountNo", dr["AccountNo"]),
        //                new MyParameter("@Amount", dr["Amount"]),
        //                new MyParameter("@Status", dr["Status"]));
        //            DataSet ds1=new DataSet();
        //            Common.Execute_Procedures_IUD(ds1);
        //        }

        //        ShowMessage(lblMsgMain, "Record Successfully Saved.", false);
        //        btnSaveVoucher.Visible = false;
        //        btnVoucherPrint.Visible = true;
        //        //dv_NewPV.Visible = false;
        //        btn_Search_Click(sender, e);
        //    }
        //    else
        //    {
        //        ShowMessage(lblMsgPOP, "Unable to save record. Error : " + Common.ErrMsg, true);
        //    }
        //}
        //catch (Exception ex)
        //{
        //    ShowMessage(lblMsgPOP, "Unable to save record." + ex.Message + Common.getLastError(), true);
        //}


        //dv_NewPV.Visible = false;
    }
    protected void btnClosePOP_Click(object sender, EventArgs e)
    {
        txtVoucherNo.Text = "";
        txtPaymentDate.Text = "";

        dv_Pay.Visible = false;
    }
    
    protected void btn_Search_Click(object sender, EventArgs e)
    {
        string SQL = "";
        string Where = "";

        SQL = "SELECT OA.LeaveRequestId,(PD.FirstName + ' ' + PD.MiddleName + ' ' + PD.FamilyName) As EmpName, OA.LeaveFrom, OA.LeaveTo,LocationText=(case when OA.Location=1 THen 'Local' else 'International' end), CASE WHEN OA.PaymentStatus = 'P' THEN 'Paid' WHEN OA.PaymentStatus = 'R' THEN 'Requested' WHEN OA.PaymentStatus = 'V' THEN 'Received' ELSE '' END AS Status, OA.PaymentVoucherNo, OA.FileName, V.VesselId,V.VesselCode,V.VesselName " +
              "FROM  [dbo].[HR_OfficeAbsence] OA  " +
              "INNER JOIN [dbo].[Hr_PersonalDetails] PD ON Pd.EmpId = OA.EmpId " +
              "LEFT OUTER JOIN [dbo].Vessel V ON V.VesselId = OA.VesselId " +
              "WHERE OA.paymentstatus in ('P','R','V') ";
       
        //if (txtF_Vendor.Text.Trim() != "")
        //{
        //    Where += " AND SupplierName LIKE '%" + txtF_Vendor.Text.Trim() + "%' ";
        //}
        

        //if (txtF_PVNo.Text.Trim() != "")
        //{
        //    Where += " AND PVNo ='" + txtF_PVNo.Text.Trim() + "' ";
        //}
       
      
        //if (ddlF_Owner.SelectedIndex > 0)
        //{
        //    Where += " AND OwnerId =" + ddlF_Owner.SelectedValue + " ";
        //}

        //if (txtF_D1.Text!="")
        //{
        //    Where += " AND PAIDON >='" + txtF_D1.Text + "' ";
        //}

        //if (txtF_D2.Text!="")
        //{
        //    Where += " AND PAIDON <='" + txtF_D2.Text + "' ";
        //}
        //if (ddlF_Vessel.SelectedIndex > 0)
        //{
        //    Where += " AND VesselId =" + ddlF_Vessel.SelectedValue + " ";
        //}
      
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL + Where + " Order By LeaveFrom Desc");

        RptExpense.DataSource = dt;
        RptExpense.DataBind();
    }
    protected void btnDownLoad_Click(object sender, EventArgs e)
    {
        int LeaveRequestId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT FileName,Attachment FROM [dbo].[HR_OfficeAbsence] WHERE LeaveRequestId=" + LeaveRequestId);
        if (dt.Rows.Count > 0)
        {
            string FileName = dt.Rows[0]["FileName"].ToString();
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
    protected void btnView_Click(object sender, EventArgs e)
    {
    }
    protected void chkReceived_CheckedChanged(object sender, EventArgs e)
    {
        int LeaveRequestId = Common.CastAsInt32(((CheckBox)sender).Attributes["LeaveReqId"]);

        try
        {
            string SQL = "UPDATE [dbo].[HR_OfficeAbsence] SET ReceivedDate = getdate(), ReceivedBy = '" + Session["UserFullName"].ToString() + "', PaymentStatus='V'  WHERE LeaveRequestId=" + LeaveRequestId;
            DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
            btn_Search_Click(sender, e);
            ShowMessage(lblMsgMain, "Received successfully.", false);
        }
        catch (Exception ex)
        {
            ShowMessage(lblMsgMain, "Unable to receive. Error : " + ex.Message + Common.getLastError(), true);
        } 
    }
    
    protected void btnPay_Click(object sender, EventArgs e)
    {
        int LeaveRequestId = Common.CastAsInt32(((LinkButton)sender).CommandArgument);
        hfLeaveRequestId.Value = LeaveRequestId.ToString();
        dv_Pay.Visible = true;
    }
    protected void btnSavePayment_Click(object sender, EventArgs e)
    {
        if (txtVoucherNo.Text.Trim() == "")
        {
            ShowMessage(lblMsgPOP, "Please enter voucher #.", true);
            txtVoucherNo.Focus();
            return;
        }
        if (txtPaymentDate.Text.Trim() == "")
        {
            ShowMessage(lblMsgPOP, "Please enter payment date.", true);
            txtPaymentDate.Focus();
            return;
        }
        try
        {
            string SQL = "UPDATE [dbo].[HR_OfficeAbsence] SET PaymentVoucherNo = '" + txtVoucherNo.Text.Trim() + "', PaymentBy = '" + Session["UserFullName"].ToString() + "', PaymentDoneDate='" + txtPaymentDate.Text.Trim() + "', PaymentStatus='P'  WHERE LeaveRequestId=" + hfLeaveRequestId.Value.Trim();
            DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
            btn_Search_Click(sender, e);
            ShowMessage(lblMsgPOP, "Record saved successfully.", false);
        }
        catch (Exception ex)
        {
            ShowMessage(lblMsgPOP, "Unable to save record. Error : " + ex.Message + Common.getLastError(), true);
        }
        

    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        //txtF_InvNo.Text = "";
        //txtF_PVNo.Text = "";
        //txtF_Vendor.Text = "";

        //ddlF_Owner.SelectedIndex = 0;
    }
    protected void imgBtnShowUpdateUser_Click(object sender, EventArgs e)
    {

    }
    protected void btnVoucherPrint_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(),"fas", "PrintVoucherN(" + PaymentId + ");", true);
    }
   
}