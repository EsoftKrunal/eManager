using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


public partial class Invoice_OwnerAccount : System.Web.UI.Page
{
    public Int32 TableID
    {
        get { return Common.CastAsInt32(ViewState["TableID"]); }
        set { ViewState["TableID"] = value; }
    }
    public Int32 UserId
    {
        get { return Common.CastAsInt32(ViewState["UserId"]); }
        set { ViewState["UserId"] = value; }
    }
    public string Company
    {
        get { return Convert.ToString( ViewState["Company"]) ; }
        set { ViewState["Company"] = value; }
    }
    protected void BindOwner()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("select COMPANY,COMPANY + ' - ' + [COMPANY NAME] AS 'COMPANY NAME' ,dbo.GetAccountBalance(Company,'USD')Balance_USD,dbo.GetAccountBalance(Company,'SGD')Balance_SGD from [dbo].[AccountCompany] where active='Y' ORDER BY [COMPANY NAME]");
        //ddlF_Owner.DataSource = dt;
        //ddlF_Owner.DataValueField = "company";
        //ddlF_Owner.DataTextField = "Company Name";
        //ddlF_Owner.DataBind();
        //ddlF_Owner.Items.Insert(0, new ListItem("Select", "0"));

        rptOwnerList.DataSource = dt;
        rptOwnerList.DataBind();

        ddlRecivingOwner.DataSource = dt;
        ddlRecivingOwner.DataValueField = "company";
        ddlRecivingOwner.DataTextField = "Company Name";
        ddlRecivingOwner.DataBind();
        ddlRecivingOwner.Items.Insert(0, new ListItem("Select", ""));
          
    }
    public string FormatCurrency(object InAmt)
    {
        decimal d = Convert.ToDecimal(InAmt);
        return d.ToString("N", new System.Globalization.CultureInfo("en-US"));
    }
    AuthenticationManager auth;
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsgPopup_A.Text = "";
        //---------------------------------------
        ProjectCommon.SessionCheck();
        //---------------------------------------
       if(!IsPostBack)
        {
            UserId = Common.CastAsInt32(Session["loginid"]);
            auth = new AuthenticationManager(1097, UserId, ObjectType.Page);
            if (!auth.IsView)
            {
                Response.Redirect("");
            }
            else
            {
                bindpaymentuser();
                txtPeriodFrom.Text = DateTime.Today.ToString("01-MMM-yyyy");
                txtPeriodTo.Text = DateTime.Today.ToString("dd-MMM-yyyy");
                BindOwner();
                ShowAccountSummary();
                lblToday.Text = DateTime.Today.ToString("dd-MMM-yyyy");
                btnOpenAddStatement.Visible = auth.IsAdd;
                btnPrint.Visible = auth.IsPrint;
            }
        }
    }
    protected void bindpaymentuser()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("select distinct modifiedby from vw_tbl_OwnerAccountsTransactions where modifiedby is not null order by ModifiedBy");
        ddlpaymentuser.DataTextField = "modifiedby";
        ddlpaymentuser.DataValueField= "modifiedby";
        ddlpaymentuser.DataSource = dt;
        ddlpaymentuser.DataBind();
        ddlpaymentuser.Items.Insert(0, new ListItem("ALL", ""));
    }
    // Function -------------------------------------------------------------------------------------------------------------------
    protected void ShowAccountSummary( )
    {
        string SQL = "select DBO.GetAccountBalance('" + Company + "','" + rdoAccount.SelectedValue + "')AS 'Balance' ";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
        if (dt.Rows.Count > 0)
        {
            lblAccountBalance.Text = dt.Rows[0]["Balance"].ToString();
        }
       
        upAcSummary.Update();
    }
    protected void ShowAccountStatement()
    {
        


    }
    // Events -------------------------------------------------------------------------------------------------------------------
    protected void tempBtn_OnClick(object sender, EventArgs e)
    {
        Company = hfCompany.Value;

        divContent.Visible = true;
        ShowAccountSummary();
        ShowAccountStatement();
        
        
        rptAccountStatement.DataSource = null;
        rptAccountStatement.DataBind();
    }
    
    protected void rdoAccount_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        spanCurr.InnerText = (rdoAccount.SelectedIndex == 0) ? "SG$" : "US$";
        ShowAccountSummary();
    }
    protected void TransactionType_OnCheckedChanged(object sender, EventArgs e)
    {
        trRecivingOwner.Visible = false;
        
        if (rad_transfer.Checked)
        {
            trRecivingOwner.Visible = true;
            lblParty1.Text = "Transfer From :";
            lblParty1.ForeColor = System.Drawing.Color.Red;

            lblParty2.Text = "Transfer To :";
            lblParty2.ForeColor = System.Drawing.Color.Green;
        }
        else if (rad_credit_A.Checked)
        {
            lblParty1.Text = "Credit Account Detais :";
            lblParty1.ForeColor = System.Drawing.Color.Green;
        }
        else if (rad_debit_A.Checked)
        {
            lblParty1.Text = "Debit Account Detais :";
            lblParty1.ForeColor = System.Drawing.Color.Red;
        }

    }
    protected void btnShow_OnClick(object sender, EventArgs e)
    {
        string SQL = " select * from vw_tbl_OwnerAccountsTransactions T";
        string WhereClause = " where ComPany='" + Company + "' AND currency='" + rdoAccount.SelectedValue + "'";
        string OPWhereClause = WhereClause;
        string CloseWhereClause = WhereClause;

        if (rdoTransactionType.SelectedIndex != 0)
            WhereClause = WhereClause + " and TransactionType='" + rdoTransactionType.SelectedValue + "'";
        if (txtPeriodFrom.Text != "")
        {
            WhereClause = WhereClause + " and transDate >='" + txtPeriodFrom.Text.Trim() + "'";
            OPWhereClause = OPWhereClause + " and transDate <'" + txtPeriodFrom.Text.Trim() + "'";
        }
        if (txtPeriodTo.Text != "")
        { 
            WhereClause = WhereClause + " and transDate <='" + Convert.ToDateTime(txtPeriodTo.Text.Trim()).AddDays(1) + "'";
            CloseWhereClause = CloseWhereClause + " and transDate < '" + Convert.ToDateTime(txtPeriodTo.Text.Trim()).AddDays(1) + "'";
        }
        if(ddlpaymentuser.SelectedIndex>0)
        {
            WhereClause = WhereClause + " and modifiedby='" + ddlpaymentuser.SelectedValue + "'";
        }
        string final_sql = SQL + WhereClause + " order by TransDate desc";


             
        DataTable dtOp = Common.Execute_Procedures_Select_ByQuery("select sum(CreditAmount) - sum(DebitAmount) from vw_tbl_OwnerAccountsTransactions " + OPWhereClause);
        DataTable dtCl = Common.Execute_Procedures_Select_ByQuery("select sum(CreditAmount) - sum(DebitAmount) from vw_tbl_OwnerAccountsTransactions " + CloseWhereClause);

        decimal OpBal = 0, CloseBal = 0;
        if (dtOp.Rows.Count > 0)
            OpBal = Common.CastAsDecimal(dtOp.Rows[0][0]);
        if (dtOp.Rows.Count > 0)
            CloseBal = Common.CastAsDecimal(dtCl.Rows[0][0]);

        Session["sqlOwnerAccountBalance"] = final_sql+"~"+ Company+"~"+txtPeriodFrom.Text+"~"+txtPeriodTo.Text+"~"+rdoTransactionType.SelectedItem.Text+"~"+rdoAccount.SelectedItem.Text+"~"+OpBal+"~"+CloseBal;
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(final_sql);
        rptAccountStatement.DataSource = dt;
        rptAccountStatement.DataBind();
    }
    protected void btnPrint_OnClick(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "window.open('../Report/OwnerAccountBalance.aspx')", true);
    }
    
    //------------------------------------

    protected void btnOpenAddStatement_OnClick(object sender, EventArgs e)
    {
        if (Company.Trim() != "")
        {
            div_AddTransaction.Visible = true;
            lblOwnerCode.Text = Company;
            lblAccount_A.Text = rdoAccount.SelectedValue;
            TransactionType_OnCheckedChanged(sender, e);
        }
    }
    protected void btnClosePopup_OnClick(object sender, EventArgs e)
    {

        
        div_AddTransaction.Visible = false;
    }
    protected void btnAddStatement_OnClick(object sender, EventArgs e)
    {
        if (rad_credit_A.Checked == false && rad_debit_A.Checked == false && rad_transfer.Checked == false)
        {
            lblMsgPopup_A.Text = "Please select transaction type.";
            return;
        }
        if (txtTransactionDate_A.Text.Trim() == "")
        {
            lblMsgPopup_A.Text = "Please enter transaction date.";
            txtTransactionDate_A.Focus();
            return;
        }
        if (Convert.ToDateTime(txtTransactionDate_A.Text.Trim()) > DateTime.Today)
        {
            lblMsgPopup_A.Text = "Transaction date should not be more than current date.";
            txtTransactionDate_A.Focus();
            return;
        }
        if (txtRemarks_A.Text.Trim() == "")
        {
            lblMsgPopup_A.Text = "Please enter remarks.";
            txtRemarks_A.Focus();
            return;
        }
        if (txtAmount_A.Text.Trim() == "")
        {
            lblMsgPopup_A.Text = "Please enter amount.";
            txtAmount_A.Focus();
            return;
        }
        if (Common.CastAsDecimal(txtAmount_A.Text)<=0)
        {
            lblMsgPopup_A.Text = "Please enter valid amount.";
            txtAmount_A.Focus();
            return;
        }
        if (rad_transfer.Checked)
        {
            if (ddlRecivingOwner.SelectedValue == Company && lblAccount_A.Text==radRecCurr.Text)
            {
                lblMsgPopup_A.Text = "Can not transfer to same account.";
                ddlRecivingOwner.Focus();
                return;
            }
        }
        try
        {
            if (rad_transfer.Checked)
            {
                Common.Set_Procedures("sp_FundTransfer");
                Common.Set_ParameterLength(9);
                Common.Set_Parameters(
                    new MyParameter("@Company_d", lblOwnerCode.Text),
                    new MyParameter("@Currency", lblAccount_A.Text),
                    new MyParameter("@TransDate", txtTransactionDate_A.Text.Trim()),
                    new MyParameter("@Amount", txtAmount_A.Text.Trim()),
                    new MyParameter("@Narration", txtRemarks_A.Text.Trim()),
                    new MyParameter("@OtherCompany", ddlRecivingOwner.SelectedValue),
                    new MyParameter("@T_Amount", txtAmount_B.Text.Trim()),
                    new MyParameter("@T_Currency", radRecCurr.SelectedValue),
                    new MyParameter("@ModifiedBy", Session["UserName"].ToString())
                    );
            }
            else
            {
                Common.Set_Procedures("sp_IU_OwnerAccountsTransactions");
                Common.Set_ParameterLength(12);
                Common.Set_Parameters(
                    new MyParameter("@TableID", 0),
                    new MyParameter("@Company", lblOwnerCode.Text),
                    new MyParameter("@Currency", lblAccount_A.Text),
                    new MyParameter("@TransDate", txtTransactionDate_A.Text.Trim()),
                    new MyParameter("@TransactionType", ((rad_credit_A.Checked) ? "C" : "D")),
                    new MyParameter("@Amount", txtAmount_A.Text.Trim()),
                    new MyParameter("@Balance", DBNull.Value),
                    new MyParameter("@Narration", txtRemarks_A.Text.Trim()),
                    new MyParameter("@Transfer", DBNull.Value),
                    new MyParameter("@OtherCompany", DBNull.Value),
                    new MyParameter("@LinkedTableIDForTransfer", DBNull.Value),
                    new MyParameter("@ModifiedBy", Session["UserName"].ToString())
                    );
            }
            DataSet ds = new DataSet();
            ds.Clear();
            Boolean res;
            res = Common.Execute_Procedures_IUD(ds);
            if (res)
            {
                lblMsgPopup_A.Text = "Record Successfully Saved.";
                ShowAccountSummary();
                ClearControls_A();
                UPOwnerList.Update();
                BindOwner();
                btnShow_OnClick(sender, e);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "Refresh();", true);

            }
            else
            {
                lblMsgPopup_A.Text = "Unable to save record. Error : " + Common.ErrMsg;
            }
        }
        catch (Exception ex)
        {
            lblMsgPopup_A.Text = "Unable to save record." + ex.Message + Common.getLastError();
        }
    }
    public void ClearControls_A()
    {
        rad_credit_A.Checked = false;
        rad_debit_A.Checked = false;
        txtTransactionDate_A.Text = "";
        txtRemarks_A.Text = "";
        txtAmount_A.Text = "";

        ddlRecivingOwner.SelectedIndex = 0;
        radRecCurr.SelectedIndex = -1;
        txtAmount_B.Text = "";
    }
}