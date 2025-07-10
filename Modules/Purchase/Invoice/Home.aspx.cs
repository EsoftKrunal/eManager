using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Invoice_Home : System.Web.UI.Page
{
    public int UserId
    {
        get { return Convert.ToInt32(ViewState["UserId"]); }
        set { ViewState["UserId"] = value; }
    }
    public bool EditAllowed
    {
        get { return Convert.ToBoolean(ViewState["EditAllowed"]); }
        set { ViewState["EditAllowed"] = value; }
    }
    public AuthenticationManager auth;
    protected void Page_Load(object sender, EventArgs e)
    {
        
        //---------------------------------------
        ProjectCommon.SessionCheck();
        //---------------------------------------       
        try
        {
            AuthenticationManager auth = new AuthenticationManager(1072, int.Parse(Session["loginid"].ToString()), ObjectType.Page);
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

        if (!IsPostBack)
        {
            UserId = Common.CastAsInt32(Session["loginid"]);
            auth = new AuthenticationManager(1072, UserId, ObjectType.Page);
            ddlF_Status.SelectedIndex = 2;
            bindCurrdl();
            bindOwnerddl();
            bindVesselNameddl();
            ShowSummary();
            //trAdmin.Visible = UserId == 1;
            EditAllowed = false;
            trPayment.Visible = false;
            trBankDetails.Visible = false;
            trVendorLedger.Visible = false;
            trRFP.Visible = false;
            trAdd.Visible = false;
            btnPay.Visible = false;
            trClaim.Visible = false;
            trNonPoData.Visible = false;
            //trExport.Visible = false;
            DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT Entry,Payment FROM POS_Invoice_mgmt where USERID=" + UserId);
			if(dt.Rows.Count>0)
			{
                if (dt.Rows[0][0].ToString() == "True")
                {
                    trAdd.Visible = true;
                    EditAllowed = true;
                }

                if (dt.Rows[0]["Payment"].ToString() == "True")
                {
                    trPayment.Visible = true;
                    trBankDetails.Visible = true;
                    trRFP.Visible = true;
                    btnPay.Visible = true;
                    trVendorLedger.Visible = true;
                    //  trClaim.Visible = true;
                    //trExport.Visible = true;
                    //trNonPoData.Visible = true;
                }
            }
            

            if(UserId==1)
            {
                EditAllowed = true;
                trPayment.Visible = true;
                trBankDetails.Visible = true;
                trVendorLedger.Visible = true;
                trRFP.Visible = true;
                trAdd.Visible = true;
                btnPay.Visible = true;
                //   trClaim.Visible = true;
                //trExport.Visible = true;
                //trNonPoData.Visible = true;
            }

            ShowMyInvoices(sender, e);
            //trOwnerBalance.Visible = auth.IsView;
        }
    }
    protected void ddlF_Owner_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        bindVesselNameddl();
    }

    protected void ShowSummary()
    {
        string SQL = "select COUNT(INVOICEID) from vw_POS_Invoices_RFP where isnull(approvalfwdto,0)>0 and stage=1 and approvalon is null AND STATUS<>'C'";  //and approvalon is null
        DataTable dt=Common.Execute_Procedures_Select_ByQuery(SQL);
        lblC1.Text = dt.Rows[0][0].ToString();
       
        SQL = "select COUNT(INVOICEID) from pos_invoice where stage=2 and isnull(VerificationFwdTo,0)>0 and VerficationOn is null and status='U' and INVOICEID in (Select InvoiceId from POS_Invoice_Approvals I with(nolock) where I.AppUserId>0 and I.ApprovedOn IS NULL and I.AppUserId = "+ UserId + ")";
        dt=Common.Execute_Procedures_Select_ByQuery(SQL);
        lblC2.Text=dt.Rows[0][0].ToString();

        SQL="select COUNT(INVOICEID) from pos_invoice where stage=3 AND status='U'";
        dt=Common.Execute_Procedures_Select_ByQuery(SQL);
        lblC3.Text = dt.Rows[0][0].ToString();

        SQL = "select COUNT(INVOICEID) from pos_invoice where status='U' AND DueDate <= '" + DateTime.Today.ToString("dd-MMM-yyyy") +"' AND ApprovalFwdTo IS NOT null ";
        dt=Common.Execute_Procedures_Select_ByQuery(SQL);
        lblC4.Text = dt.Rows[0][0].ToString();
    }
    protected void bindVesselNameddl()
    {
        string sql = "";
        if(ddlF_Owner.SelectedIndex==0)
            sql= "SELECT * from VW_ACTIVEVESSELS where VesselNo in (Select VesselId from UserVesselRelation with(nolock) where Loginid = "+ Convert.ToInt32(Session["loginid"].ToString())+") ORDER BY SHIPNAME";
        else
            sql = "SELECT * from VW_ACTIVEVESSELS WHERE COMPANY='" + ddlF_Owner.SelectedValue + "' and VesselNo in (Select VesselId from UserVesselRelation with(nolock) where Loginid = "+ Convert.ToInt32(Session["loginid"].ToString())+") ORDER BY SHIPNAME";

        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
        ddlF_Vessel.DataSource = dt;
        ddlF_Vessel.DataValueField = "SHIPID";
        ddlF_Vessel.DataTextField = "SHIPNAME";
        ddlF_Vessel.DataBind();
        ddlF_Vessel.Items.Insert(0, new ListItem("All", "0"));
    }
    protected void bindOwnerddl()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("select * from [dbo].[AccountCompany] where active='Y' ORDER BY [COMPANY NAME]");
        ddlF_Owner.DataSource = dt;
        ddlF_Owner.DataValueField = "Company";
        ddlF_Owner.DataTextField = "Company Name";
        ddlF_Owner.DataBind();
        ddlF_Owner.Items.Insert(0, new ListItem("All", "0"));
    }
    protected void bindCurrdl()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT Curr FROM [dbo].[VW_tblWebCurr]");
        this.ddCurrency.DataValueField = "Curr";
        this.ddCurrency.DataTextField = "Curr";
        this.ddCurrency.DataSource = dt;
        this.ddCurrency.DataBind();
        ddCurrency.Items.Insert(0, new ListItem(" All ", "0"));
    }
    protected void btnPay_Click(object sender, EventArgs e)
    { 
        // check for same vendor and same currency
        //-----
        bool IsChecked = false;
        string InvIds = "";
        foreach (RepeaterItem ri in RptMyInvoices.Items)
        {
            CheckBox chk = (CheckBox)ri.FindControl("chkpay");
            if (chk.Checked)
            {
                IsChecked = true;
                InvIds += "," + chk.CssClass;
            }
        }

        if (!IsChecked)
        {
            ProjectCommon.ShowMessage("Please select an invoice to pay");
            return;
        }
        else
        {
            InvIds = InvIds.Substring(1);

            string sqlCheck = "SELECT distinct SupplierId FROM  vw_POS_Invoices_001 WHERE [InvoiceId] IN (" + InvIds + ")";
            DataTable dtVendor = Common.Execute_Procedures_Select_ByQuery(sqlCheck);

            if (dtVendor.Rows.Count != 1)
            {
                ProjectCommon.ShowMessage("Please select invoices for same vendor to pay.");
                return;
            }

            sqlCheck = "SELECT distinct Currency FROM  vw_POS_Invoices_001 WHERE [InvoiceId] IN (" + InvIds + ")";
            dtVendor = Common.Execute_Procedures_Select_ByQuery(sqlCheck);
            if (dtVendor.Rows.Count != 1)
            {
                ProjectCommon.ShowMessage("Please select invoices for same currency to pay.");
                return;
            }

            sqlCheck = "SELECT distinct COMPANY FROM vw_POS_Invoices_001 where invoiceid IN (" + InvIds + ")";
            dtVendor = Common.Execute_Procedures_Select_ByQuery(sqlCheck);
            if (dtVendor.Rows.Count != 1)
            {
                ProjectCommon.ShowMessage("Please select invoices for same owner to pay.");
                return;
            }
            sqlCheck = "SELECT distinct isnull(ClsInvoice,0) FROM  vw_POS_Invoices_001 WHERE [InvoiceId] IN (" + InvIds + ")";
            dtVendor = Common.Execute_Procedures_Select_ByQuery(sqlCheck);
            if (dtVendor.Rows.Count != 1)
            {
                ProjectCommon.ShowMessage("Please select invoices in same category ( CLS Invoice / NON CLS Invoice ).");
                return;
            }

            sqlCheck = "SELECT distinct ISNULL(IsAdvPayment,0) As IsAdvPayment FROM  vw_POS_Invoices_001 WHERE [InvoiceId] IN (" + InvIds + ")";
            dtVendor = Common.Execute_Procedures_Select_ByQuery(sqlCheck);
            if (dtVendor.Rows.Count != 1)
            {
                ProjectCommon.ShowMessage("Please make separate RFP for advance payments.");
                return;
            }

            sqlCheck = "SELECT distinct ISNULL(IsNonPo,0) As IsNonPo FROM  vw_POS_Invoices_001 WHERE [InvoiceId] IN (" + InvIds + ")";
            dtVendor = Common.Execute_Procedures_Select_ByQuery(sqlCheck);
            if (dtVendor.Rows.Count != 1)
            {
                ProjectCommon.ShowMessage("Please make separate RFP for Non-PO payments.");
                return;
            }
            Session.Add("InvoiceIds", InvIds);
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "", "window.open('InvoicePayment.aspx', '', '');", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "", "window.open('CreateRFP.aspx', '', '');", true);
        }
    }
    public void ShowMyInvoices(object sender, EventArgs e)
    {
        string SQL = "";
        string Where = "";

        SQL = "SELECT * FROM vw_POS_Invoices_RFP I " + 
              "WHERE ( " +
              "( " +
              "CASE " +
              "WHEN I.[Stage]=0 AND EntertedBy=" + UserId + " THEN 1 " +
              "WHEN I.[Stage]=1 AND ( ApprovalFwdTo=" + UserId + " OR EntertedBy=" + UserId + " ) THEN 1 " + 
              "WHEN I.[Stage]=2 AND VerificationFwdTo=" + UserId + " THEN 1 " +
              "WHEN I.[Stage]=3 AND PaidFwdTo=" + UserId + " THEN 1 " + 
              "ELSE 0 END " +
              ") =1 OR EXISTS(SELECT APPUSERID FROM POS_Invoice_RFP_Approvals A Inner Join POS_Invoice_RFP_Mapping b on a.RFPId = b.RFPId WHERE b.InvoiceId=I.InvoiceId AND APPUSERID=" + UserId + " AND ApprovedOn IS NULL) )  And Status<>'Cancelled' and I.PaymentStage = 0 ";
        if (txtF_RefNo.Text.Trim() != "")
        {
            Where += " AND RefNo ='" + txtF_RefNo.Text.Trim() + "' "; 
        }
        if (txtF_Vendor.Text.Trim() != "")
        {
            Where += " AND Vendor LIKE '%" + txtF_Vendor.Text.Trim() + "%' ";
        }
        if (txtF_InvNo.Text.Trim() != "")
        {
            Where += " AND InvNo ='" + txtF_InvNo.Text.Trim() + "' ";
        }
        if (ddlF_Vessel.SelectedIndex > 0)
        {
            Where += " AND INVVesselCode ='" + ddlF_Vessel.SelectedValue + "' ";
        }
        if (ddCurrency.SelectedIndex > 0)
        {
            Where += " AND Currency ='" + ddCurrency.SelectedValue + "' ";
        }
        if (ddlF_Owner.SelectedIndex > 0)
        {
            Where += " AND COMPANY ='" + ddlF_Owner.SelectedValue + "' ";
        }
        if (ddlF_Status.SelectedIndex > 0)
        {
            Where += " AND Status ='" + ddlF_Status.SelectedValue + "' ";
        }
        if (ddlF_Stage.SelectedIndex > 0)
        {
            Where += " AND Stage =" + ddlF_Stage.SelectedValue + " ";
        }
        if (chkNonPo.Checked)
        {
            Where += " AND I.IsNonPo = 1 ";
        }
        
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL + Where + " Order By InvoiceId Desc");

        if (dt != null && dt.Rows.Count > 0)
        {
            RptMyInvoices.DataSource = dt;
            RptMyInvoices.DataBind();
        }
        else
        {
            RptMyInvoices.DataSource = null;
            RptMyInvoices.DataBind();
        }
    }

    protected void btnAddInvoice_Click(object sender, EventArgs e)
    {

    }

    protected void btnFindInvoice_Click(object sender, EventArgs e)
    {

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

    protected void btnReset_Click(object sender, ImageClickEventArgs e)
    {
        txtF_RefNo.Text = "";
        txtF_Vendor.Text="";
        txtF_InvNo.Text = "";
        ddlF_Owner.SelectedIndex = 0;
        ddlF_Owner_OnSelectedIndexChanged(sender, e);
        ddlF_Vessel.SelectedIndex = 0;
        ddlF_Status.SelectedIndex = 0;
        ddlF_Stage.SelectedIndex = 0;
        ShowMyInvoices(sender,e);
    }
}