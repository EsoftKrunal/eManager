using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.IO;

public partial class emtm_MyProfile_Emtm_OfficeCashAdvance : System.Web.UI.Page
{
    public int LeaveRequestId
    {
        get
        {
            return Common.CastAsInt32(ViewState["LeaveRequestId"]);
        }
        set
        {
            ViewState["LeaveRequestId"] = value;
        }
    }
    public int CashId
    {
        get
        {
            return Common.CastAsInt32(ViewState["CashId"]);
        }
        set
        {
            ViewState["CashId"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        lblMsg.Text = "";
 
        if (!Page.IsPostBack)
        {
            LoadCurrency();
            if (Request.QueryString["id"] != null)
            {
                LeaveRequestId = Common.CastAsInt32(Request.QueryString["id"]);
                BindCashAdvGrid();
            }
        }
    }

    protected void LoadCurrency()
    {

        System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["eMANAGER"].ToString());
        try
        {
            con.Open();
        }
        catch
        {
            con = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["eMANAGER"].ToString());
        }
        finally
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("select distinct for_curr from XCHANGEDAILY order by for_curr", con);
        System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);

        ddlCurr.DataSource = dt;
        ddlCurr.DataTextField = "for_curr";
        ddlCurr.DataValueField = "for_curr";
        ddlCurr.DataBind();
        ddlCurr.Items.Insert(0, new ListItem("< Select >", "0"));

        //ddlCurr_1.DataSource = dt;
        //ddlCurr_1.DataTextField = "for_curr";
        //ddlCurr_1.DataValueField = "for_curr";
        //ddlCurr_1.DataBind();
        //ddlCurr_1.Items.Insert(0, new ListItem("< Select >", "0"));

        // ------------ for expense ---------------------

        
    }
    protected decimal getExchangeRates(string Curr)
    {
        decimal rate = 0;
        //System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection("Data Source=172.30.1.10;Initial Catalog=mtmm2000sql;Persist Security Info=True;User Id=sa;Password=Esoft^%$#@!");
        System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["eMANAGER"].ToString());
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("select TOP 1 EXC_RATE from XCHANGEDAILY where for_curr='" + Curr + "' AND RATEDATE <='" + DateTime.Today.ToString("dd-MMM-yyyy") + "' order by ratedate desc", con);
        System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        if (dt.Rows.Count > 0)
            rate = Common.CastAsDecimal(dt.Rows[0][0]);
        return rate;
    }


    // CASH ADVANCE
    public void BindCashAdvGrid()
    {
        string SQL = " SELECT CashId,BizTravelId,CashAdvance,Currency,Amount,ExcRate FROM HR_OfficeAbsence_CashAdvance WHERE RecordType='G' and BizTravelId = " + LeaveRequestId;
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(SQL);
        rptCashAdv.DataSource = dt;
        rptCashAdv.DataBind();
        if (dt.Rows.Count > 0)
        {
            lblTotalgivenSGD.Text = string.Format("{0:0.00}", dt.Compute("Sum(CashAdvance)", string.Empty));
        }
        else
        {
            lblTotalgivenSGD.Text = "";
        }
    }
    protected void btnEditCashDetails_Click(object sender, ImageClickEventArgs e)
    {
        CashId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);

        string SQL = "SELECT BizTravelId,CashAdvance,Currency,Amount,ExcRate FROM HR_OfficeAbsence_CashAdvance WHERE CashId = " + CashId;
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(SQL);

        if (dt.Rows.Count > 0)
        {
            txtAmount.Text = string.Format("{0:0.00}", Common.CastAsDecimal(dt.Rows[0]["Amount"]));
            ddlCurr.SelectedValue = dt.Rows[0]["Currency"].ToString();
            txtExchRate.Text = string.Format("{0:0.00}", Common.CastAsDecimal(dt.Rows[0]["ExcRate"]));
            lblCashAdv.Text = string.Format("{0:0.00}", Common.CastAsDecimal(dt.Rows[0]["CashAdvance"]));
        }

        BindCashAdvGrid();
    }
    protected void ClearCV_Click(object sender, EventArgs e)
    {
        CashId = 0;
        ClearControls();
        BindCashAdvGrid();
    }
    protected void btnSaveCV_Click(object sender, EventArgs e)
    {
        if (txtAmount.Text.Trim() == "")
        {
            lblMsg.Text = "Please enter amount.";
            txtAmount.Focus();
            return;
        }

        decimal d;
        if (!decimal.TryParse(txtAmount.Text.Trim(), out d))
        {
            lblMsg.Text = "Please enter valid amount.";
            txtAmount.Focus();
            return;
        }

        if (ddlCurr.SelectedIndex == 0)
        {
            lblMsg.Text = "Please select currency.";
            ddlCurr.Focus();
            return;
        }

        Common.Set_Procedures("HR_InsertUpdateCashAdv");
        Common.Set_ParameterLength(7);
        Common.Set_Parameters(new MyParameter("@CashId", CashId),
            new MyParameter("@BizTravelId", LeaveRequestId),
            new MyParameter("@CashAdvance", lblCashAdv.Text.Trim()),
            new MyParameter("@Currency", ddlCurr.SelectedItem.Text.Trim()),
            new MyParameter("@Amount", txtAmount.Text.Trim()),
            new MyParameter("@ExcRate", txtExchRate.Text.Trim()),
            new MyParameter("@RecordType", "G")
            );
        DataSet ds = new DataSet();

        if (Common.Execute_Procedures_IUD_CMS(ds))
        {
            CashId = 0;
            lblMsg.Text = "Record saved seccessfully.";
            ClearControls();
            BindCashAdvGrid();
        }
        else
        {
            lblMsg.Text = "Unable to save record.";
        }

    }
    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        int cashId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);

        string deleteSQL = "DELETE FROM HR_OfficeAbsence_CashAdvance WHERE CashId =" + cashId + "; SELECT -1";
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(deleteSQL);

        if (dt.Rows.Count > 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "alert('Record deleted successfully.');", true);
            BindCashAdvGrid();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", "alert('Unable to delete record.');", true);
        }
    }
    public void ClearControls()
    {
        lblCashAdv.Text = "";
        txtAmount.Text = "";
        ddlCurr.SelectedIndex = 0;
        txtExchRate.Text = "";

    }
    protected void txtExchRate_TextChanged(object sender, EventArgs e)
    {
        if (txtExchRate.Text.Trim() != "")
        {

            if (txtAmount.Text.Trim() == "")
            {
                lblMsg.Text = "Please enter amount.";
                txtAmount.Focus();
                return;
            }
            if (ddlCurr.SelectedIndex == 0)
            {
                lblMsg.Text = "Please select currency.";
                ddlCurr.Focus();
                return;
            }

            lblCashAdv.Text = string.Format("{0:0.00}", (Math.Round(Common.CastAsDecimal(txtAmount.Text.Trim()) * Common.CastAsDecimal(txtExchRate.Text.Trim()), 8)));
        }
        else
        {
            lblCashAdv.Text = "";

        }
    }
    protected void txtAmount_TextChanged(object sender, EventArgs e)
    {
        if (txtExchRate.Text.Trim() != "" && ddlCurr.SelectedIndex != 0)
        {
            lblCashAdv.Text = string.Format("{0:0.00}", (Math.Round(Common.CastAsDecimal(txtAmount.Text.Trim()) * Common.CastAsDecimal(txtExchRate.Text.Trim()), 8)));
        }

    }
}