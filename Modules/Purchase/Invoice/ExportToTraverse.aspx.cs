using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


public partial class Invoice_ExportToTraverse : System.Web.UI.Page
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
         if (!Page.IsPostBack)
        {
            UserId = Common.CastAsInt32(Session["loginid"]);
            DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT Payment FROM POS_Invoice_mgmt where USERID=" + UserId);
            if (dt.Rows.Count > 0 && dt.Rows[0]["Payment"].ToString() == "True")
            {
                lblUserName.Text = " Vouchers List Paid By : " + Session["UserFullName"].ToString();
                UserId = Common.CastAsInt32(Session["loginid"]);
                bindOwnerddl();
                btn_Search_Click(sender, e);
            }
            else
            {
                Response.Redirect("~/NoPermission.aspx");
            }
        }
    }


    protected void bindOwnerddl()
    {

        DataTable dt = Common.Execute_Procedures_Select_ByQuery("select COMPANY,COMPANY + ' - ' + [COMPANY NAME] AS 'COMPANY NAME'from [dbo].[AccountCompany] where active='Y' ORDER BY [COMPANY NAME]");
        ddlF_Owner.DataSource = dt;
        ddlF_Owner.DataValueField = "company";
        ddlF_Owner.DataTextField = "Company Name";
        ddlF_Owner.DataBind();
        ddlF_Owner.Items.Insert(0, new ListItem("All", "0"));

    }

    protected void ShowMessage(Label l1, string Msg, bool error)
    {
        l1.Text = Msg;
        l1.ForeColor = (error) ? System.Drawing.Color.Red : System.Drawing.Color.Green;
    }

   
    protected void btn_Search_Click(object sender, EventArgs e)
    {
        string SQL = "";
        string Where = "";

        SQL = "select * FROM VW_PAYMENTVOUCHERS_001 WHERE isnull(STATUS,'P') = 'A' and PTYPE='O' AND POSOWNERID NOT IN('MVI','OTH','LOC') AND BankConfirmedOn is NOT NULL AND PAIDON>='01-AUG-2016' AND PaidById=" + Session["loginid"].ToString();

        if (txtF_Vendor.Text.Trim() != "")
        {
            Where += " AND SupplierName LIKE '%" + txtF_Vendor.Text.Trim() + "%' ";
        }
        if (txtF_PVNo.Text.Trim() != "")
        {
            Where += " AND PVNo ='" + txtF_PVNo.Text.Trim() + "' ";
        }
        if (ddlStatus.SelectedIndex > 0)
        {
            Where += " AND isnull(Exported,0)=" + ddlStatus.SelectedValue;
        }
        if (ddlF_Owner.SelectedIndex > 0)
        {
            Where += " AND POSOwnerId ='" + ddlF_Owner.SelectedValue + "' ";
        }
        if (txtF_D1.Text != "")
        {
            Where += " AND PAIDON >='" + txtF_D1.Text + "' ";
        }
        if (txtF_D2.Text != "")
        {
            Where += " AND PAIDON <='" + Convert.ToDateTime(txtF_D2.Text).AddDays(1).ToString("dd-MMM-yyyy") + "' ";
        }


        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL + Where + " Order By PaymentId Desc");

        RptMyInvoices.DataSource = dt;
        RptMyInvoices.DataBind();
    }
    protected void btnCloseNow_Click(object sender, EventArgs e)
    {
        dvExport.Visible = false;
    }

    protected void btn_Export_Click(object sender, EventArgs e)
    {
        PaymentId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        DataTable dt1 = Common.Execute_Procedures_Select_ByQuery("SELECT CONVERT(BIT,ISNULL(EXPORTED,0)) AS EXPORTED FROM DBO.POS_Invoice_Payment WHERE PAYMENTID=" + PaymentId);
        if (dt1.Rows.Count > 0)
        {
            if (dt1.Rows[0][0].ToString() == "False") // Not Exported
            {
                //--------------------------
                String PType = "";
                PType = ((ImageButton)sender).CssClass;
                frm1.Attributes.Add("src","ExportPopUp.aspx?PaymentId=" + PaymentId);
                dvExport.Visible = true;
            }
        }
    }

 
    protected void imgBtnShowUpdateUser_Click(object sender, EventArgs e)
    {

    }
    protected void btnVoucherPrint_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(),"fas", "PrintVoucherN(" + PaymentId + ");", true);
    }
    
}