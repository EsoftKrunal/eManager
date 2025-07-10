using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Invoice_ApExport : System.Web.UI.Page
{
    public string UserCode
    {
        get { return Convert.ToString(Session["UserCode"]).ToUpper(); }
        set { Session["UserCode"] = value; }
    }
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

    protected void Page_Load(object sender, EventArgs e)
    {
        //---------------------------------------
        ProjectCommon.SessionCheck();
       
        //---------------------------------------
        validAccountMess.Text = "";
        validCurrencyMess.Text = "";
        if (!Page.IsPostBack)
        {
            UserId = Common.CastAsInt32(Session["loginid"]);

            //-------------------------
            DataTable dt101 = Common.Execute_Procedures_Select_ByQuery("SELECT Entry,Payment FROM POS_Invoice_mgmt where USERID=" + UserId);
            if (dt101.Rows[0]["Payment"].ToString() != "True")
            {
                Response.Redirect("~/UnAuthorizedAccess.aspx?Message=You are not authorized to access this page.");
            }
            //-------------------------

            InvoiceId = Common.CastAsInt32(Request.QueryString["InvoiceId"]);
            BindInvoiceDetails();

            BindVendor();
            BindCurrency();
            Bind();
        }

    }
    public void Bind()
    {
        Common.Set_Procedures("sp_NewPR_GetApEntries_ByInvoiceId");
        Common.Set_ParameterLength(1);
        Common.Set_Parameters(new MyParameter("@InvoiceId", InvoiceId));
        DataSet DsRFQMaster;
        DsRFQMaster = Common.Execute_Procedures_Select();
        if (DsRFQMaster != null)
        {
            rptApEntriesDetails.DataSource = DsRFQMaster;
            rptApEntriesDetails.DataBind();
        }

    }
    protected void BindInvoiceDetails()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("select * from dbo.vw_Pos_Invoices where InvoiceId=" + InvoiceId);
        if (dt.Rows.Count > 0)
        {
            lblRefNo.Text = dt.Rows[0]["RefNo"].ToString();
            lblSupplier.Text = dt.Rows[0]["Vendor"].ToString();
            lblVendorCode.Text = dt.Rows[0]["VendorCode"].ToString();
            lbl_InvNo.Text = dt.Rows[0]["InvNo"].ToString();
            lbl_InvDate.Text = Common.ToDateString(dt.Rows[0]["InvDate"]);
            lbl_DueDate.Text = Common.ToDateString(dt.Rows[0]["DueDate"]);
            lbl_InvAmount.Text = dt.Rows[0]["InvoiceAmount"].ToString();
            lbl_ApprovedAmount.Text = dt.Rows[0]["ApprovalAmount"].ToString();
            lblCurrency.Text = dt.Rows[0]["Currency"].ToString();
            lbl_Vessel.Text = dt.Rows[0]["VesselCode"].ToString();
            lblStatus.Text = dt.Rows[0]["Status"].ToString();

            int Stage = Common.CastAsInt32(dt.Rows[0]["Stage"]);
            if (Stage != 3)
            {
                Response.Redirect("~/UnAuthorizedAccess.aspx?Message=Can not export. Invoice is not approved.");
            }
        }
    }
    public void BindAccount(Int32 JeId)
    {
        ddlNewAcc.Items.Clear();
        string sql = "select AccountID,convert(varchar, AccountNumber)+'-'+AccountName as AccountNumber from vw_sql_tblSMDPRAccounts order by AccountNumber";
        Common.Set_Procedures("ExecQuery");
        Common.Set_ParameterLength(1);
        Common.Set_Parameters(new MyParameter("@Query", sql));
        DataSet dsPrType = new DataSet();
        dsPrType.Clear();
        dsPrType = Common.Execute_Procedures_Select();

        ddlNewAcc.DataSource = dsPrType;
        ddlNewAcc.DataTextField = "AccountNumber";
        ddlNewAcc.DataValueField = "AccountID";
        ddlNewAcc.DataBind();
        ddlNewAcc.Items.Insert(0, new ListItem("<Select>", ""));
        ddlNewAcc.SelectedIndex = 0;
    }
    public void BindCurrency()
    {
        ddlCurrency.DataSource = Common.Execute_Procedures_Select_ByQuery("SELECT Curr FROM VW_tblWebCurr ORDER BY Curr");
        ddlCurrency.DataTextField = "Curr";
        ddlCurrency.DataValueField = "Curr";
        ddlCurrency.DataBind();
        ddlCurrency.Items.Insert(0, new ListItem("<Select>", ""));
        ddlCurrency.SelectedIndex = 0;
    }
    // Events -----------------------------------------------------------------------------
    protected void imgUpdateInvDate_OnClick(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnUpdate = (ImageButton)sender;
            HiddenField hfBidID = (HiddenField)btnUpdate.Parent.FindControl("hfBidID");
            CheckBox chkSelect = (CheckBox)btnUpdate.Parent.FindControl("chkSelect");

            TextBox txtinvDate = (TextBox)btnUpdate.Parent.FindControl("txtinvDate");
            string strdate = ConvertForSave(txtinvDate.Text);
            if (strdate == "error")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "errmsg", "alert('Invalid Date .')", true);
                //chkSelect.Checked = false;
                txtinvDate.Focus();
                return;
            }
            // date validation ------------------
            string sql_1 = "select BidSmdLevel1ApprovalDate,BidstatusDate from [DBO].tblsmdpomasterbid where bidid=" + hfBidID.Value + "";
            DataTable dtInvDateCheck = Common.Execute_Procedures_Select_ByQuery(sql_1);
            if (dtInvDateCheck.Rows.Count > 0)
            {
                try
                {
                    DateTime dt_1 = Convert.ToDateTime(dtInvDateCheck.Rows[0][0].ToString());
                    dt_1 = Convert.ToDateTime(dt_1.ToString("dd-MMM-yyyy"));
                    DateTime dtInvDate = Convert.ToDateTime(txtinvDate.Text);
                    if (dtInvDate < dt_1)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "errmsg", "alert('Invoice Date must me more than PO approval date.')", true);
                        txtinvDate.Focus();
                        return;
                    }
                }
                catch { }

                try
                {
                    DateTime dt_2 = Convert.ToDateTime(dtInvDateCheck.Rows[0][1].ToString());
                    dt_2 = Convert.ToDateTime(dt_2.ToString("dd-MMM-yyyy"));
                    DateTime dtInvDate = Convert.ToDateTime(txtinvDate.Text);
                    if (dtInvDate < dt_2)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "errmsg", "alert('Invoice Date must me more than PO approval date.')", true);
                        txtinvDate.Focus();
                        return;
                    }
                }
                catch { }
            }
            // date validation ------------------

            string sql = "update [dbo].tblSMDPOMasterBid set BidInvoiceDate='" + strdate + "' where bidid=" + hfBidID.Value + " ";
            DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
            txtinvDate.Focus();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "ReSelect();alert('Invoice date update successfully.')", true);
        }
        catch (Exception ex)
        {
            return;
        }
    }
    protected void imgSearch_OnClick(object sender, EventArgs e)
    {
        Bind();
        ShowPostedUnPostedTransaction();
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Search.aspx");
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        ModalPopupExtender1.Visible = false;
    }
    protected void btnClose2_Click(object sender, EventArgs e)
    {
        dvAccountBox.Visible = false;
    }
    // Udpate Vendor
    protected void imgUpdateVendor_OnClick(object sender, EventArgs e)
    {
        ViewState["VendorJEId"] = ((ImageButton)sender).CommandArgument;
        ModalPopupExtender1.Visible = true;
        txtVendor.Focus();
    }
    protected void btnNewVendor_Click(object sender, EventArgs e)
    {
        Int32 JeId = Common.CastAsInt32(ViewState["VendorJEId"]);
        Int32 NewVendorId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        ModalPopupExtender1.Visible = false;

        if (Update_Vendor_Account_Currency(JeId, NewVendorId, 0, ""))
        {
            imgSearch_OnClick(sender, e);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('New vendor updated succesfully.')", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Unable to update vendor. Please try again.')", true);
        }
    }
    // Udpate Account
    protected void imgUpdateAccCode_OnClick(object sender, EventArgs e)
    {
        ViewState["AccountJEId"] = ((ImageButton)sender).CommandArgument;
        BindAccount(Common.CastAsInt32(ViewState["AccountJEId"]));
        dvAccountBox.Visible = true;
        ddlNewAcc.Focus();
    }
    protected void btnNewAccount_click(object sender, EventArgs e)
    {
        if (ddlNewAcc.SelectedIndex <= 0)
        {
            validAccountMess.Text = "Please select Account.";
            return;
        }
        Int32 JeId = Common.CastAsInt32(ViewState["AccountJEId"]);
        Int32 NewAccountId = Common.CastAsInt32(ddlNewAcc.SelectedValue);
        dvAccountBox.Visible = false;

        if (Update_Vendor_Account_Currency(JeId, 0, NewAccountId, ""))
        {
            imgSearch_OnClick(sender, e);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Account Code updated succesfully.')", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Unable to update Account Code. Please try again.')", true);
        }
    }
    protected void btnNewCurrency_click(object sender, EventArgs e)
    {
        if (ddlCurrency.SelectedIndex <= 0)
        {
            validCurrencyMess.Text = "Please select Currency.";
            return;
        }
        Int32 JeId = Common.CastAsInt32(ViewState["AccountJEId"]);
        string NewCurrency = ddlCurrency.SelectedValue;
        dvAccountBox.Visible = false;

        if (Update_Vendor_Account_Currency(JeId, 0, 0, NewCurrency))
        {
            imgSearch_OnClick(sender, e);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Currency updated succesfully.')", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Unable to update Currency. Please try again.')", true);
        }
    }
    protected void btnFind_Click(object sender, EventArgs e)
    {
        BindVendor();
    }
    
    // Function ---------------------------------------------------------------------------
    public bool Update_Vendor_Account_Currency(int JeId, int VendorId, int AccountId, string Curr)
    {
        try
        {
            Common.Execute_Procedures_Select_ByQuery("EXEC DBO.Change_AccountCode_Vendor " + JeId.ToString() + "," + AccountId.ToString() + "," + VendorId.ToString() + ",'" + Curr + "'");
            return true;
        }
        catch
        {
            return false;
        }
    }
    public void BindVendor()
    {
        string Filter = "";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("select row_number() over (order by SupplierName) as Sno,SupplierId,SupplierName,SupplierPort,SupplierEmail,TravId from VW_tblSMDSuppliers Where SupplierName like '" + txtVendor.Text + "%' Order By SupplierName");
        rptVendors.DataSource = dt;
        rptVendors.DataBind();
    }
  
    public string BindTravVenID(string VenID)
    {
        string sql = "SELECT v_vendors.vendorid, name + ',' + country AS Expr1, v_vendors.currencyid, v_vendors.vendorid " +
                            " FROM [dbo].v_vendors " +
                             " WHERE v_vendors.cocode in (select company from vw_sql_tblSMDPRVessels where shipid='" + lbl_Vessel.Text + "') and VendorID='" + VenID + "'" +
                             " ORDER BY name + ',' + country";
        DataTable DT = Common.Execute_Procedures_Select_ByQuery(sql);
        if (DT != null)
        {
            if (DT.Rows.Count > 0)
                return DT.Rows[0][1].ToString();
        }
        return "";

    }
    protected void imgExport_OnClick(object sender, EventArgs e)
    {

        bool NoRecrods = true;
        //----------
        foreach (RepeaterItem ri in rptApEntriesDetails.Items)
        {
            CheckBox ck = (CheckBox)ri.FindControl("chkSelect");
            if (ck.Checked)
            {
                NoRecrods = false;
                break;
            }
        }
        //----------
        if (NoRecrods)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "a", "alert('Please select at least one row to Export.');", true);
            return;
        }
        //----------
        // IsRecordLocked --------------------
        bool IsRecordLocked = false;
        foreach (RepeaterItem ri in rptApEntriesDetails.Items)
        {
            CheckBox ck = (CheckBox)ri.FindControl("chkSelect");
            if (ck.Checked)
            {
                string CoCode = "";
                DateTime InvoiceDate;
                HiddenField hfd = (HiddenField)ri.FindControl("hfdShipId");
                HiddenField hfBidID = (HiddenField)ri.FindControl("hfBidID");

                TextBox txtinvDate = (TextBox)ri.FindControl("txtinvDate");
                Label lblInvoiceNo = (Label)ri.FindControl("lblInvoiceNo");


                DataTable dt = Common.Execute_Procedures_Select_ByQuery("select company from dbo.VW_sql_tblSMDPRVessels where shipid='" + hfd.Value + "'");
                if (dt.Rows.Count > 0)
                {
                    CoCode = dt.Rows[0][0].ToString();
                }
                string[] Date = txtinvDate.Text.Trim().Split('-');

                string sql = " SELECT PERCLOSED FROM DBO.TBLPERIODMAINT WHERE COCODE='" + CoCode + "' AND [RPTYEAR]=" + Date[2] + " AND RPTPERIOD=" + Date[1];
                DataTable dt1 = Common.Execute_Procedures_Select_ByQuery(sql);
                if (dt1.Rows.Count > 0)
                {
                    if (dt1.Rows[0][0].ToString() == "True")
                    {
                        //IsRecordLocked = true;
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "a", "alert('Data can not be exported. Invoice period is locked for invoice# " + lbl_InvNo.Text + " .');", true);
                        return;
                    }
                }
                //--------------------
                int stageid = 0;
                string sql1 = "select stage from POS_Invoice i where i.invoiceid in (select invoiceid from dbo.pos_invoice_payment_po where bidid=" + hfBidID.Value + ")";
                DataTable dt001 = Common.Execute_Procedures_Select_ByQuery(sql1);
                if (dt001.Rows.Count > 0)
                    stageid = Common.CastAsInt32(dt001.Rows[0][0]);

                if (stageid != 3)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "a", "alert('PO can not be exported. Invoice is not approved.');", true);
                    return;
                }
            }
        }
        //----------
        string Error = "";
        string UserName = Session["UserName"].ToString();
        //----------------------------------------
        string SuccessJeId = "", ErrorJeId = "";
        foreach (RepeaterItem ri in rptApEntriesDetails.Items)
        {
            int BidId = Common.CastAsInt32(((HiddenField)ri.FindControl("hfBidID")).Value);
            DataTable dt11 = Common.Execute_Procedures_Select_ByQuery("EXEC DBO.IS_APEXPORT " + BidId + "");
            if (dt11.Rows.Count > 0)
            {
                int Res = Common.CastAsInt32(dt11.Rows[0][0]);
                if (Res != 1)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "a", "alert('Some of the selected po(s) are not linked with any task. Please link po(s) to task list in order to export.');", true);
                    return;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "a", "alert('Some of the selected po(s) are not linked with any task. Please link po(s) to task list in order to export.');", true);
                return;
            }
        }
        //-------------------------------------------
        foreach (RepeaterItem ri in rptApEntriesDetails.Items)
        {
            CheckBox ck = (CheckBox)ri.FindControl("chkSelect");
            if (ck.Checked)
            {
                string CoCode = "";
                HiddenField hfd = (HiddenField)ri.FindControl("hfdShipId");
                DataTable dt = Common.Execute_Procedures_Select_ByQuery("select company from VW_sql_tblSMDPRVessels where shipid='" + hfd.Value + "'");
                if (dt.Rows.Count > 0)
                {
                    CoCode = dt.Rows[0][0].ToString();
                }
                char[] sep = { '`' };
                string[] res = ck.Attributes["MyDetails"].Split(sep);
                //string Data = "<JeIdList><JeRecord><Jeid>" + res[0] + "</Jeid><JeType>" + res[1] + "</JeType></JeRecord></JeIdList>";
                if (ECommon.APEXPORT(CoCode, hfd.Value, UserName, int.Parse(res[0]), res[1], out Error))
                {
                    SuccessJeId = SuccessJeId + "," + res[0];
                    //------------ EXPORT TO GP
                    if (ExportGPAllowed(CoCode))
                    {
                        DataTable dtgp = Common.Execute_Procedures_Select_ByQuery("exec dbo.GP_Export_Data " + res[0]);
                        if (dtgp.Rows.Count > 0)
                        {
                            string bidcurr = dtgp.Rows[0]["bidcurr"].ToString();
                            string invoiceno = dtgp.Rows[0]["invoiceno"].ToString();
                            if (invoiceno.Length > 20)
                                invoiceno = invoiceno.Substring(0, 20);
                            DateTime invoicedate = Convert.ToDateTime(dtgp.Rows[0]["bidinvoicedate"]);
                            string aptravvenid = dtgp.Rows[0]["aptravvenid"].ToString();
                            string bidvenname = dtgp.Rows[0]["bidvenname"].ToString();
                            string accountnumber = dtgp.Rows[0]["accountnumber"].ToString();
                            string gpaccountnumber = dtgp.Rows[0]["gpaccountnumber"].ToString();
                            string shipid = dtgp.Rows[0]["shipid"].ToString();
                            string gpshipid = dtgp.Rows[0]["gpshipid"].ToString();
                            decimal approvalamount = Common.CastAsDecimal(dtgp.Rows[0]["approvalamount"]);
                            string bidponum = dtgp.Rows[0]["bidponum"].ToString();
                            DateTime datecreated = DateTime.Now;
                            DateTime OriginalInvoiceDate = Convert.ToDateTime(dtgp.Rows[0]["OriginalInvoiceDate"]);
                            int invoiceid = Common.CastAsInt32(dtgp.Rows[0]["invoiceid"]);

                            int importid = -1;
                            string error = "";
                            using (SqlConnection myConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["EMANAGER"].ToString()))
                            {
                                myConnection.Open();
                                SqlTransaction transaction = myConnection.BeginTransaction();
                                try
                                {
                                    SqlCommand cmd = new SqlCommand("update dbo.tblapentries set GPExportStart=getdate() where jeid=" + res[0], myConnection, transaction);
                                    cmd.CommandType = CommandType.Text;
                                    int nor = cmd.ExecuteNonQuery();
                                    if (nor == 1)
                                    {

                                        if (ECommon.GP_EXPORT(CoCode, UserCode, bidcurr, invoiceno, invoicedate, aptravvenid, bidvenname, accountnumber, gpaccountnumber, shipid, gpshipid, approvalamount, bidponum, datecreated, OriginalInvoiceDate, invoiceid, out importid, out error))
                                        {

                                            if (importid > 0)
                                            {
                                                nor = -1;
                                                cmd.CommandText = "update dbo.tblapentries set inGP=1,GPExportSuccess=getdate(),ImportId=" + importid + " where jeid=" + res[0];
                                                nor = cmd.ExecuteNonQuery();
                                                if (nor == 1)
                                                    transaction.Commit();
                                                else
                                                    transaction.Rollback();
                                            }

                                        }

                                    }
                                }
                                catch (Exception ex)
                                {
                                    transaction.Rollback();
                                    Error = ex.Message;

                                }
                                finally
                                {
                                    if (myConnection.State != ConnectionState.Closed)
                                        myConnection.Close();
                                }
                            }
                        }
                    }
                    //-------------
                }
                else
                {
                    ErrorJeId = ErrorJeId + "" + res[0] + " Error : " + Error + "</br>";
                }
            }
        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "a", "alert('Record exported to Traverse');", true);
        lblError.Text = ErrorJeId;
        Bind();
    }
    public void ShowPostedUnPostedTransaction()
    {
        CheckBox chkSelect = new CheckBox();
        ImageButton imgUpdateInvDate = new ImageButton();

        ImageButton imgPOSVendor = new ImageButton();
        ImageButton imgAccCode = new ImageButton();

        Label lblBatchNTran = new Label();
        Label lblJeID = new Label();
        foreach (RepeaterItem RptItm in rptApEntriesDetails.Items)
        {
            chkSelect = (CheckBox)RptItm.FindControl("chkSelect");
            imgUpdateInvDate = (ImageButton)RptItm.FindControl("imgUpdateInvDate");

            imgPOSVendor = (ImageButton)RptItm.FindControl("imgPOSVendor");
            imgAccCode = (ImageButton)RptItm.FindControl("imgAccCode");

            lblBatchNTran = (Label)RptItm.FindControl("lblBatchNTran");
            lblJeID = (Label)RptItm.FindControl("lblJeID");

            chkSelect.Visible = true;
            lblHeaderText.Text = "JeID";
            imgUpdateInvDate.Visible = true;
            //lblBatchNTran.Visible = false;
            lblJeID.Visible = true;

            imgAccCode.Visible = true;
            imgPOSVendor.Visible = true;
            
        }
    }
    public string ConvertForSave(string date)
    {
        try
        {
            string[] str = date.Split('-');
            string srtDate = Convert.ToString(str[0] + "/" + str[1] + "/" + str[2]);
            DateTime chkdate = new DateTime(Common.CastAsInt32(str[2]), Common.CastAsInt32(str[1]), Common.CastAsInt32(str[0]));
            return chkdate.ToString("dd-MMM-yyyy");
        }
        catch (Exception e)
        {
            return "error";
        }

    }
    protected void rptApEntriesDetails_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        ImageButton Error = (ImageButton)e.Item.FindControl("imgError");
        CheckBox Chk = (CheckBox)e.Item.FindControl("chkSelect");
        HiddenField hfd = (HiddenField)e.Item.FindControl("hfdCheckTrav");
        char[] sep = { '`' };
        string[] res = hfd.Value.Split(sep);

        if (res[0].Trim() != res[1].Trim()) // VENDOR SHOULD BE MATCH
        {
            Error.Visible = true;
            Error.ToolTip = "Vendor Not Matching.";
            Chk.Enabled = false;
            return;
        }
        if (res[2].Trim() == "") // BIDCURR CHECK
        {
            Error.Visible = true;
            Error.ToolTip = "Bid Currency Not Available.";
            Chk.Enabled = false;
            return;
        }
        if (Common.CastAsDecimal(res[3]) == 0) // Transaction Amount
        {
            Error.Visible = true;
            Error.ToolTip = "Incorrect Transaction Amount.";
            Chk.Enabled = false;
            return;
        }
        if (Common.CastAsDecimal(res[4]) <= 0) // Echange Rate
        {
            Error.Visible = true;
            Error.ToolTip = "Incorrect Exchange Rate.";
            Chk.Enabled = false;
            return;
        }
        if (res[5].Trim() == "") // Echange Date
        {
            Error.Visible = true;
            Error.ToolTip = "Incorrect Exchange Date.";
            Chk.Enabled = false;
            return;
        }
        if (res[6].Trim() != "S$" && res[6].Trim() != "US$") // Vendor Currency
        {
            Error.Visible = true;
            Error.ToolTip = "Vendor Currency Not Available.";
            Chk.Enabled = false;
            return;
        }
        //("VenCurr") BETWEEN S$ OR US$
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
       // ScriptManager.RegisterStartupScript(Page, this.GetType(), "tr", "SetLastFocus('divScroll');", true);
    }
    protected bool ExportGPAllowed(string CompanyCode)
    {
        bool res = false;
        List<String> comps = new List<string>();
        comps.Add("000");
        comps.Add("SMD");
        comps.Add("CHT");
        if (comps.Contains(CompanyCode))
            res = true;
        return res;
    }

}