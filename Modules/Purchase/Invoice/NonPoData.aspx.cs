using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Windows.Forms;

public partial class Modules_Purchase_Invoice_AddNonPoData : System.Web.UI.Page
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
        if (!IsPostBack)
        {
            UserId = Common.CastAsInt32(Session["loginid"]);
            auth = new AuthenticationManager(1072, UserId, ObjectType.Page);
            ddlF_Status.SelectedIndex = 2;
            bindCurrdl();
            bindOwnerddl();
            bindVesselNameddl();
        }
    }

    protected void ddlF_Owner_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        bindVesselNameddl();
    }

    protected void bindVesselNameddl()
    {
        string sql = "";
        if (ddlF_Owner.SelectedIndex == 0)
            sql = "SELECT * from VW_ACTIVEVESSELS where VesselNo in (Select VesselId from UserVesselRelation with(nolock) where Loginid = " + Convert.ToInt32(Session["loginid"].ToString()) + ") ORDER BY SHIPNAME";
        else
            sql = "SELECT * from VW_ACTIVEVESSELS WHERE COMPANY='" + ddlF_Owner.SelectedValue + "' and VesselNo in (Select VesselId from UserVesselRelation with(nolock) where Loginid = " + Convert.ToInt32(Session["loginid"].ToString()) + ") ORDER BY SHIPNAME";

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

    public void BindDepartment()
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

    public void BindAccount()
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

    public void ShowMyInvoices(object sender, EventArgs e)
    {
        string SQL = "";
        string Where = "";

        SQL = "SELECT * FROM Vw_Pos_Invoice_NonPo I " +
              "WHERE  Status<>'Cancelled'  ";
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
        txtF_Vendor.Text = "";
        txtF_InvNo.Text = "";
        ddlF_Owner.SelectedIndex = 0;
        ddlF_Owner_OnSelectedIndexChanged(sender, e);
        ddlF_Vessel.SelectedIndex = 0;
        ddlF_Status.SelectedIndex = 0;
        ddlF_Stage.SelectedIndex = 0;
        ShowMyInvoices(sender, e);
    }

    protected void lbApEntries_Click(object sender, EventArgs e)
    {
        try
        {
            int NonPoId = Common.CastAsInt32(((LinkButton)sender).CommandArgument);
            if (NonPoId > 0)
            {
                dvNonPoEntry.Visible = true;
                BindDepartment();
                GetNonPoData(NonPoId);
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message.ToString();
        }
    }
    protected void GetNonPoData(int nonpoId)
    {
        string SQL = "";
        SQL = "SELECT * FROM Vw_Pos_Invoice_NonPo I " +
              "WHERE  Status<>'Cancelled' AND NonPoId = "+ nonpoId + " ";

        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
        if (dt.Rows.Count > 0)
        {
            lblInvRefNo.Text = dt.Rows[0]["RefNo"].ToString();
            lblInvoiceNo.Text = dt.Rows[0]["InvNo"].ToString();
            lblInvAmount.Text = dt.Rows[0]["InvAmount"].ToString();
            lblInvCurrency.Text = dt.Rows[0]["Currency"].ToString();
            lblSupplierName.Text = dt.Rows[0]["Vendor"].ToString();
            lblVesselName.Text = dt.Rows[0]["ShipName"].ToString();
            hdnNonPoId.Value = dt.Rows[0]["NonPoId"].ToString();
            hdnInvoiceId.Value = dt.Rows[0]["InvoiceId"].ToString();
            lblNonPoId.Text = dt.Rows[0]["NonPoNum"].ToString();
        }
    }

    protected void btnNonPo_ClosePopup_Click(object sender, ImageClickEventArgs e)
    {
        dvNonPoEntry.Visible = false;
        lblMsgApEntries.Text = "";
        ClearControls();
    }

    protected void ClearControls()
    {
        lblInvRefNo.Text = "";
        lblInvoiceNo.Text = "";
        lblInvAmount.Text = "";
        lblInvCurrency.Text = "";
        lblSupplierName.Text = "";
        lblVesselName.Text = "";
        hdnNonPoId.Value = "0";
        hdnInvoiceId.Value = "0";
        lblNonPoId.Text = "";
        ddldepartment.SelectedIndex = 0;
        ddlAccount.SelectedIndex = 0;
    }

    protected void btnCloseApEntries_Click(object sender, EventArgs e)
    {
        dvNonPoEntry.Visible = false;
        lblMsgApEntries.Text = "";
        ClearControls();
    }

    protected void btnSaveApEntries_Click(object sender, EventArgs e)
    {
        try
        {
            if (hdnNonPoId.Value != "0")
            {
                int nonpoId = Common.CastAsInt32(hdnNonPoId.Value);
                int invoiceId = Common.CastAsInt32(hdnInvoiceId.Value);
                if (nonpoId > 0)
                {
                    Common.Set_Procedures("Sp_InsertUpdateNonPoApEntries");
                    Common.Set_ParameterLength(4);
                    Common.Set_Parameters(
                        new MyParameter("@NonPoId", nonpoId),
                        new MyParameter("@InvoiceId", invoiceId),
                        new MyParameter("@AccountId",ddlAccount.SelectedValue.Trim()),
                        new MyParameter("@Remarks", txtNonPoRemarks.Text.Trim())
                        );

                    DataSet ds = new DataSet();
                    ds.Clear();
                    Boolean res;
                    res = Common.Execute_Procedures_IUD(ds);
                    if (res)
                    {
                       
                        lblMsgApEntries.Text = "Record Successfully Saved.";
                        dvNonPoEntry.Visible = false;
                        ShowMyInvoices(sender, e);
                    }
                    else
                    {
                        lblMsgApEntries.Text = "Unable to save record." + Common.getLastError();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblMsgApEntries.Text = "Unable to save record." + ex.Message.ToString();
        }
    }

    protected void ddldepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindAccount();
    }
}
