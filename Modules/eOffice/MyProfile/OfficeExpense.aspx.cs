using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.IO;

public partial class emtm_MyProfile_Emtm_OfficeExpense : System.Web.UI.Page
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
    public int CashId_1
    {
        get
        {
            return Common.CastAsInt32(ViewState["CashId_1"]);
        }
        set
        {
            ViewState["CashId_1"] = value;
        }
    }
    public int ExpenseId
    {
        get
        {
            return Common.CastAsInt32(ViewState["ExpenseId"]);
        }
        set
        {
            ViewState["ExpenseId"] = value;
        }
    }
    
    protected void btnCash_Click(object sender, EventArgs e)
    {
        btnCash.CssClass = "btn11";
        btnExp.CssClass = "btn11";

        pnlCash.Visible = true;
        pnlExp.Visible = false;
        pnlExp1.Visible = false; 

        btnCash.CssClass = "btn11sel";
    }
    protected void btnExp_Click(object sender, EventArgs e)
    {
        btnCash.CssClass = "btn11";
        btnExp.CssClass = "btn11";

        pnlCash.Visible = false;
        pnlExp.Visible = true;
        pnlExp1.Visible = true;

        btnExp.CssClass = "btn11sel";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        lblMsg.Text = "";
        lblUMsg.Text = "";
        lblMsg_Exp.Text = "";

        if (!Page.IsPostBack)
        {
            LoadCurrency();
            BindIncharge();
            if (Request.QueryString["id"] != null)
            {
                LeaveRequestId = Common.CastAsInt32(Request.QueryString["id"]);
                showSummery();
                ShowRecord();

                BindCashAdvGrid();
                BindCashAdvGrid_1();
                BindExpenseGrid();
                ShowRecord_View(LeaveRequestId);
            }
        }

    }
    public void ShowRecord_View(int Id)
    {
        string sql = "select * ,LocationText=(case when Location=1 THen 'Local' else 'International' end) ,PurposeText=(select Purpose from [HR_LeavePurpose] where purposeid=oa.purposeid),(Select VesselName from dbo.vessel v where v.vesselid=oa.Vesselid) as VesselName from HR_OfficeAbsence oa WHERE LeaveRequestId =" + Id.ToString() + " ";
        DataTable dtdata = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        if (dtdata != null)
            if (dtdata.Rows.Count > 0)
            {
                DataRow dr = dtdata.Rows[0];
                lblLocation.Text = "[ " + dr["LocationText"].ToString().Trim() + " Visit ]";
                lblPurpose.Text = dr["PurposeText"].ToString().Trim();
                lblPeriod.Text = "Duration : " + Common.ToDateString(dr["LeaveFrom"]) + " - " + Common.ToDateString(dr["LeaveTo"]);
                lblRemarks.Text = dr["Reason"].ToString().Trim();
                lblVesselName.Text = "";
                if (lblPurpose.Text.Contains("Vessel") || lblPurpose.Text.Contains("Docking"))
                {
                    lblVesselName.Text = "Vessel : " + dr["VesselName"].ToString().Trim();
                }
                lblPlannedInspections.Text = "";
                if (lblPurpose.Text.Contains("Vessel"))
                {
                    string Inspections = dr["Inspections"].ToString().Trim();
                    if (Inspections.Trim() != "")
                    {
                        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT CODE FROM DBO.m_Inspection WHERE ID IN (" + Inspections + ")");
                        Inspections = "";
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr1 in dt.Rows)
                            {
                                Inspections += "," + dr1["CODE"].ToString();
                            }
                            Inspections = Inspections.Substring(1);
                        }
                        lblPlannedInspections.Text = "Planned Inspections : " + Inspections;
                    }
                }
                if (lblPurpose.Text.Contains("Docking"))
                {
                    lblPlannedInspections.Text = "DD # : ";
                }



                int HalfDay = Common.CastAsInt32(dr["HalfDay"]);
                switch (HalfDay)
                {
                    case 1:
                        lblHalfDay.Text = "Halfday - First Half";
                        break;
                    case 2:
                        lblHalfDay.Text = "Halfday - Second Half";
                        break;
                    default:
                        lblHalfDay.Text = "";
                        break;
                }
            }
    }
    public void BindIncharge()
    {
        string SQL = "SELECT VesselName,VesselId FROM Vessel WHERE VesselStatusid<>2 ORDER BY VesselName ";                    

        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(SQL);

        ddlInchargeTo.DataSource = dt;
        ddlInchargeTo.DataTextField = "VesselName";
        ddlInchargeTo.DataValueField = "VesselId";
        ddlInchargeTo.DataBind();

        ddlInchargeTo.Items.Insert(0, new ListItem("< Select >", ""));
        ddlInchargeTo.Items.Insert(1, new ListItem("MTM", "0"));

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
            con = new System.Data.SqlClient.SqlConnection("Data Source=172.30.1.10;Initial Catalog=mtmm2000sql;Persist Security Info=True;User Id=sa;Password=Esoft^%$#@!");
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

        ddlCurr_1.DataSource = dt;
        ddlCurr_1.DataTextField = "for_curr";
        ddlCurr_1.DataValueField = "for_curr";
        ddlCurr_1.DataBind();
        ddlCurr_1.Items.Insert(0, new ListItem("< Select >", "0"));

        // ------------ for expense ---------------------

        ddlCurr_Exp.DataSource = dt;
        ddlCurr_Exp.DataTextField = "for_curr";
        ddlCurr_Exp.DataValueField = "for_curr";
        ddlCurr_Exp.DataBind();
        ddlCurr_Exp.Items.Insert(0, new ListItem("< Select >", "0"));
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
    
    public void ShowRecord()
    {
        string SQL = "SELECT  REPLACE(Convert(varchar(11), OA.ActFromDt, 106),' ','-') AS ActFromDt,REPLACE(Convert(varchar(11), OA.ActToDt, 106),' ','-') AS ActToDt, OA.ActFromDt AS Time,OA.ActToDt AS EndTime, LP.Purpose, REPLACE(Convert(varchar(11), OA.LeaveFrom, 106),' ','-') AS LeaveFrom, REPLACE(Convert(varchar(11), OA.LeaveTo, 106),' ','-') AS LeaveTo FROM HR_OfficeAbsence OA " +
                     "INNER JOIN HR_LeavePurpose LP ON OA.PurposeId = LP.PurposeId " +        
                     "WHERE OA.LeaveRequestId = " + LeaveRequestId;
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(SQL);

        if (dt.Rows.Count > 0)
        {
            string startdate = "", enddate="";
            startdate = dt.Rows[0]["ActFromDt"].ToString();
            enddate = dt.Rows[0]["ActToDt"].ToString() == "01-Jan-1900" ? "" : dt.Rows[0]["ActToDt"].ToString(); 

            if (dt.Rows[0]["Time"].ToString() != "")
            {
                string time = dt.Rows[0]["Time"].ToString();
                string [] str = time.Split(' ');
                startdate += str[1].ToString().Split(':').GetValue(0).ToString().Trim() + str[1].ToString().Split(':').GetValue(1).ToString().Trim();
            }
           
            if (dt.Rows[0]["EndTime"].ToString() != "")
            {
                string endtime = dt.Rows[0]["EndTime"].ToString();
                string[] str = endtime.Split(' ');

                enddate += str[1].ToString().Split(':').GetValue(0).ToString().Trim() + str[1].ToString().Split(':').GetValue(1).ToString().Trim();
            }
           
            lblStartDate.Text = startdate;
            lblEndDate.Text = enddate;

        }
    }
    public void showSummery()
    {
        lblTotalCashAdvance.Text = string.Format("{0:0.00}", Common.CastAsDecimal(lblActGivenSGD.Text));
        lblTotBal.Text = string.Format("{0:0.00}", (Common.CastAsDecimal(lblTotExp.Text) - Common.CastAsDecimal(lblTotalCashAdvance.Text)));
        if (Common.CastAsDecimal(lblTotBal.Text) < 0)
        {
            lblTotBal.ForeColor = System.Drawing.Color.Red;
        }
        else
        {
            lblTotBal.ForeColor = System.Drawing.Color.Black;
        }
        //-------------
        if (Common.CastAsDecimal(lblTotBal.Text) > 0)
        {
            lblRemarks.Text = "Pay to Employee SGD " + lblTotBal.Text;
        }
        if (Common.CastAsDecimal(lblTotBal.Text) < 0)
        {
            lblRemarks.Text = "Deduct From Employee SGD " +  string.Format("{0:0.00}",(Common.CastAsDecimal(lblTotBal.Text) * -1));
        }

    }
    
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "openreport('" + LeaveRequestId + "');", true);
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
        BindBalance();
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
        if (!decimal.TryParse(txtAmount.Text.Trim(), out d ))
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
        if (txtExchRate.Text.Trim() != "" && ddlCurr.SelectedIndex !=0)
        {                        
            lblCashAdv.Text = string.Format("{0:0.00}", (Math.Round(Common.CastAsDecimal(txtAmount.Text.Trim()) * Common.CastAsDecimal(txtExchRate.Text.Trim()), 8)));
        }
        
    }
    //---------------------------
    public void BindCashAdvGrid_1()
    {
        string SQL = " SELECT CashId,BizTravelId,CashAdvance,Currency,Amount,ExcRate FROM HR_OfficeAbsence_CashAdvance WHERE RecordType='R' and BizTravelId = " + LeaveRequestId;
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(SQL);
        rptCashAdv_1.DataSource = dt;
        rptCashAdv_1.DataBind();
        if (dt.Rows.Count > 0)
        {
            lblTotalRetdSGD.Text = string.Format("{0:0.00}", dt.Compute("Sum(CashAdvance)", string.Empty));
        }
        else
        {
            lblTotalRetdSGD.Text = "";
        }
        BindBalance();
    }
    protected void btnEditCashDetails_1_Click(object sender, ImageClickEventArgs e)
    {
        CashId_1 = Common.CastAsInt32(((ImageButton)sender).CommandArgument);

        string SQL = "SELECT BizTravelId,CashAdvance,Currency,Amount,ExcRate FROM HR_OfficeAbsence_CashAdvance WHERE CashId = " + CashId_1;
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(SQL);

        if (dt.Rows.Count > 0)
        {
            txtAmount_1.Text = string.Format("{0:0.00}", Common.CastAsDecimal(dt.Rows[0]["Amount"]));
            ddlCurr_1.SelectedValue = dt.Rows[0]["Currency"].ToString();
            txtExchRate_1.Text = string.Format("{0:0.00}", Common.CastAsDecimal(dt.Rows[0]["ExcRate"]));
            lblCashAdv_1.Text = string.Format("{0:0.00}", Common.CastAsDecimal(dt.Rows[0]["CashAdvance"]));
        }

        BindCashAdvGrid_1();
    }
    protected void ClearCV_1_Click(object sender, EventArgs e)
    {
        CashId_1 = 0;
        ClearControls_1();
        BindCashAdvGrid_1(); 
    }
    protected void btnSaveCV_1_Click(object sender, EventArgs e)
    {
        if (txtAmount_1.Text.Trim() == "")
        {
            lblMsg_1.Text = "Please enter amount.";
            txtAmount_1.Focus();
            return;
        }

        decimal d;
        if (!decimal.TryParse(txtAmount_1.Text.Trim(), out d))
        {
            lblMsg_1.Text = "Please enter valid amount.";
            txtAmount_1.Focus();
            return;
        }

        if (ddlCurr_1.SelectedIndex == 0)
        {
            lblMsg_1.Text = "Please select currency.";
            ddlCurr_1.Focus();
            return;
        }

        Common.Set_Procedures("HR_InsertUpdateCashAdv");
        Common.Set_ParameterLength(7);
        Common.Set_Parameters(new MyParameter("@CashId", CashId_1),
            new MyParameter("@BizTravelId", LeaveRequestId),
            new MyParameter("@CashAdvance", lblCashAdv_1.Text.Trim()),
            new MyParameter("@Currency", ddlCurr_1.SelectedItem.Text.Trim()),
            new MyParameter("@Amount", txtAmount_1.Text.Trim()),
            new MyParameter("@ExcRate", txtExchRate_1.Text.Trim()),
            new MyParameter("@RecordType", "R")
            );
        DataSet ds = new DataSet();

        if (Common.Execute_Procedures_IUD_CMS(ds))
        {
            CashId_1 = 0;
            lblMsg_1.Text = "Record saved seccessfully.";
            ClearControls_1();
            BindCashAdvGrid_1();
        }
        else
        {
            lblMsg.Text = "Unable to save record.";
        }

    }
    protected void btnDelete_1_Click(object sender, ImageClickEventArgs e)
    {
        int cashId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);

        string deleteSQL = "DELETE FROM HR_OfficeAbsence_CashAdvance WHERE CashId =" + cashId + "; SELECT -1";
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(deleteSQL);

        if (dt.Rows.Count > 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "alert('Record deleted successfully.');", true);
            BindCashAdvGrid_1();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", "alert('Unable to delete record.');", true);
        }
    }
    public void ClearControls_1()
    {
        lblCashAdv_1.Text = "";
        txtAmount_1.Text = "";
        ddlCurr_1.SelectedIndex = 0;
        txtExchRate_1.Text = "";
    }
    protected void txtExchRate_1_TextChanged(object sender, EventArgs e)
    {
        if (txtExchRate_1.Text.Trim() != "")
        {

            if (txtAmount_1.Text.Trim() == "")
            {
                lblMsg_1.Text = "Please enter amount.";
                txtAmount_1.Focus();
                return;
            }
            if (ddlCurr_1.SelectedIndex == 0)
            {
                lblMsg_1.Text = "Please select currency.";
                ddlCurr_1.Focus();
                return;
            }

            lblCashAdv_1.Text = string.Format("{0:0.00}", (Math.Round(Common.CastAsDecimal(txtAmount_1.Text.Trim()) * Common.CastAsDecimal(txtExchRate_1.Text.Trim()), 8)));
        }
        else
        {
            lblCashAdv_1.Text = "";

        }
    }
    protected void txtAmount_1_TextChanged(object sender, EventArgs e)
    {
        if (txtExchRate_1.Text.Trim() != "" && ddlCurr_1.SelectedIndex != 0)
        {
            lblCashAdv_1.Text = string.Format("{0:0.00}", (Math.Round(Common.CastAsDecimal(txtAmount_1.Text.Trim()) * Common.CastAsDecimal(txtExchRate_1.Text.Trim()), 8)));
        }

    }

    protected void BindBalance()
    {
        lblActGivenSGD.Text = string.Format("{0:0.00}", Common.CastAsDecimal(lblTotalgivenSGD.Text) - Common.CastAsDecimal(lblTotalRetdSGD.Text));
        showSummery();
    }

    // EXPENCE DETAILS
    public void BindExpenseGrid()
    {
        string SQL = " SELECT ROW_NUMBER() OVER(ORDER BY Date DESC) AS SrNo, ExpenseId,BizTravelId,Expense,Currency,Amount,ExcRate,REPLACE(CONVERT(VARCHAR(11), Date, 106) ,' ','-') AS ExpDt,Details,[FileName],ReceiptNo,CASE InchargeTo WHEN 0 THEN 'MTM' ELSE (SELECT VesselCode FROM Vessel WHERE VesselId=InchargeTo ) END AS chargeTo " +
                     "FROM HR_OfficeAbsence_Expense WHERE RECORDTYPE='C' AND BizTravelId = " + LeaveRequestId;
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(SQL);

        rptExpense.DataSource = dt;
        rptExpense.DataBind();
        decimal Sum = Common.CastAsDecimal(dt.Compute("Sum(Expense)", string.Empty)); 
        //----------------------

        SQL = " SELECT ROW_NUMBER() OVER(ORDER BY Date DESC) AS SrNo, ExpenseId,BizTravelId,Expense,Currency,Amount,ExcRate,REPLACE(CONVERT(VARCHAR(11), Date, 106) ,' ','-') AS ExpDt,Details,[FileName],ReceiptNo,CASE InchargeTo WHEN 0 THEN 'MTM' ELSE (SELECT VesselCode FROM Vessel WHERE VesselId=InchargeTo ) END AS chargeTo " +
                   "FROM HR_OfficeAbsence_Expense WHERE RECORDTYPE='R' AND BizTravelId = " + LeaveRequestId;
        dt = Common.Execute_Procedures_Select_ByQueryCMS(SQL);
        rptExpense_1.DataSource = dt;
        rptExpense_1.DataBind();
        decimal Sum1 = Common.CastAsDecimal(dt.Compute("Sum(Expense)", string.Empty)); 
        
        lblTotExp.Text = string.Format("{0:0.00}", Sum);
        lblSumNonCash.Text = string.Format("{0:0.00}", Sum1);
        lblGrossExp.Text = string.Format("{0:0.00}", Sum + Sum1);
        showSummery();
    }
    protected void btnClear_Exp_Click(object sender, EventArgs e)
    {
        dvAddExp.Visible = false;
        ExpenseId = 0;
        ClearControls_Exp();
        BindExpenseGrid();
    }
    protected void btnSave_Exp_Click(object sender, EventArgs e)
    {
        if (txtExpDt.Text.Trim() == "")
        {
            lblMsg_Exp.Text = "Please select date.";
            txtExpDt.Focus();
            return;
        }

        DateTime dt;
        if (!DateTime.TryParse(txtExpDt.Text.Trim(), out dt))
        {
            lblMsg_Exp.Text = "Please enter valid date.";
            txtExpDt.Focus();
            return;
        }

        if (txtExp_Details.Text.Trim() == "")
        {
            lblMsg_Exp.Text = "Please enter details.";
            txtExp_Details.Focus();
            return;
        }

        if (ddlInchargeTo.SelectedIndex == 0)
        {
            lblMsg_Exp.Text = "Please select charge to.";
            ddlInchargeTo.Focus();
            return;
        }
        
        if (txtExpAmt.Text.Trim() == "")
        {
            lblMsg_Exp.Text = "Please enter amount.";
            txtExpAmt.Focus();
            return;
        }

        decimal d;
        if (!decimal.TryParse(txtExpAmt.Text.Trim(), out d))
        {
            lblMsg_Exp.Text = "Please enter valid amount.";
            txtExpAmt.Focus();
            return;
        }

        if (ddlCurr_Exp.SelectedIndex == 0)
        {
            lblMsg_Exp.Text = "Please select currency.";
            ddlCurr_Exp.Focus();
            return;
        }
       

        FileUpload img = (FileUpload)FileUpload1;
        Byte[] imgByte = new Byte[0];
        string FileName = "";
        if (img.HasFile && img.PostedFile != null)
        {
            HttpPostedFile File = FileUpload1.PostedFile;
            FileName = LeaveRequestId.ToString() + "_" + DateTime.Now.ToString("dd-MMM-yyy") + "_" + DateTime.Now.TimeOfDay.ToString().Replace(":", "-").ToString() + Path.GetExtension(File.FileName);

            imgByte = new Byte[File.ContentLength];
            File.InputStream.Read(imgByte, 0, File.ContentLength);
        }

        string RecordType =Convert.ToString(ViewState["AddMode"]);

        Common.Set_Procedures("HR_InsertUpdateExpense");
        Common.Set_ParameterLength(14);
        Common.Set_Parameters(new MyParameter("@ExpenseId", ExpenseId),
            new MyParameter("@BizTravelId", LeaveRequestId),
            new MyParameter("@Expense", lblExpense.Text.Trim()),
            new MyParameter("@Currency", ddlCurr_Exp.SelectedItem.Text.Trim()),
            new MyParameter("@Amount", txtExpAmt.Text.Trim()),
            new MyParameter("@ExcRate", txtExchRate_Exp.Text.Trim()),
            new MyParameter("@Date", txtExpDt.Text.Trim()),
            new MyParameter("@Details", txtExp_Details.Text.Trim()),
            new MyParameter("@InchargeTo", ddlInchargeTo.SelectedValue.Trim()),
            new MyParameter("@FileName", FileName),
            new MyParameter("@Attachment", imgByte),
            new MyParameter("@AccountCode", txtAccCode.Text.Trim()),
            new MyParameter("@ReceiptNo", txtReceiptNo.Text.Trim()),
            new MyParameter("@RecordType", RecordType)
            );
        DataSet ds = new DataSet();

        if (Common.Execute_Procedures_IUD_CMS(ds))
        {
            ExpenseId = 0;
            
            lblMsg_Exp.Text = "Record saved seccessfully.";

            ClearControls_Exp();
            BindExpenseGrid();
            showSummery();
        }
        else
        {
            lblMsg_Exp.Text = "Unable to save record.";
        }

    }
    protected void ExpAdd_Click(object sender, EventArgs e)
    {
        lblheading.Text = (((Button)sender).CommandArgument == "C") ? " CASH " : " NON-CASH ";
        ViewState["AddMode"] = ((Button)sender).CommandArgument;
        dvAddExp.Visible = true;
    }
    protected void btnDelete_Exp_Click(object sender, ImageClickEventArgs e)
    {
        int _ExpenseId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);

        string deleteSQL = "DELETE FROM HR_OfficeAbsence_Expense WHERE ExpenseId =" + _ExpenseId + "; SELECT -1";
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(deleteSQL);

        if (dt.Rows.Count > 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "alert('Record deleted successfully.');", true);
            BindExpenseGrid();
            showSummery();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", "alert('Unable to delete record.');", true);
        }
    }    
    public void ClearControls_Exp()
    {
        lblExpense.Text = "";
        txtExpAmt.Text = "";
        //ddlCurr_Exp.SelectedIndex = 0;
        //txtExchRate_Exp.Text = "";
        //txtExpDt.Text = "";
        txtExp_Details.Text = "";
        //ddlInchargeTo.SelectedIndex = 0;
        //txtAccCode.Text = "";
        txtReceiptNo.Text = "";

    }
    protected void imgAttachment_Click(object sender, ImageClickEventArgs e)
    {
        int ExpId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);

        ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "openwindow('" + ExpId + "');", true);

    }
    protected void txtExchRate_Exp_TextChanged(object sender, EventArgs e)
    {
        if (txtExchRate_Exp.Text.Trim() != "")
        {

            if (txtExpAmt.Text.Trim() == "")
            {
                lblMsg_Exp.Text = "Please enter amount.";
                txtExpAmt.Focus();
                return;
            }
            if (ddlCurr_Exp.SelectedIndex == 0)
            {
                lblMsg_Exp.Text = "Please select currency.";
                ddlCurr_Exp.Focus();
                return;
            }

            lblExpense.Text = string.Format("{0:0.00}",(Math.Round(Common.CastAsDecimal(txtExpAmt.Text.Trim()) * Common.CastAsDecimal(txtExchRate_Exp.Text.Trim()), 8)));
        }
        else
        {
            lblExpense.Text = "";

        }
    }
    protected void btnEditExpenseDetails_Click(object sender, ImageClickEventArgs e)
    {
        ExpenseId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);

        string SQL = "SELECT ExpenseId,BizTravelId,Expense,Currency,Amount,ExcRate,REPLACE(CONVERT(VARCHAR(11), Date,106), ' ','-') AS Date,Details,InchargeTo,AccountCode,ReceiptNo FROM HR_OfficeAbsence_Expense WHERE ExpenseId = " + ExpenseId;
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(SQL);

        if (dt.Rows.Count > 0)
        {
            txtExpDt.Text = dt.Rows[0]["Date"].ToString();
            txtExpAmt.Text = string.Format("{0:0.00}", Common.CastAsDecimal(dt.Rows[0]["Amount"]));
            ddlCurr_Exp.SelectedValue = dt.Rows[0]["Currency"].ToString();
            txtExchRate_Exp.Text = string.Format("{0:0.00}", Common.CastAsDecimal(dt.Rows[0]["ExcRate"]));
            lblExpense.Text = string.Format("{0:0.00}", Common.CastAsDecimal(dt.Rows[0]["Expense"]));

            ddlInchargeTo.SelectedValue = dt.Rows[0]["InchargeTo"].ToString();
            txtExp_Details.Text = dt.Rows[0]["Details"].ToString();
            txtAccCode.Text = dt.Rows[0]["AccountCode"].ToString();
            txtReceiptNo.Text = dt.Rows[0]["ReceiptNo"].ToString();
        }
        dvAddExp.Visible = true;
        BindExpenseGrid();
    }
    protected void txtExpAmt_TextChanged(object sender, EventArgs e)
    {
        if (ddlCurr_Exp.SelectedIndex != 0 && txtExchRate_Exp.Text.Trim() != "")
        {
            lblExpense.Text = string.Format("{0:0.00}", (Math.Round(Common.CastAsDecimal(txtExpAmt.Text.Trim()) * Common.CastAsDecimal(txtExchRate_Exp.Text.Trim()), 8)));
        }

    }
}