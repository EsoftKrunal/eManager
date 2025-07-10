using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class UserControls_ChangeAccountCode : System.Web.UI.UserControl
{
    public int BidID
    {
        get { return Convert.ToInt32(ViewState["_BidID"]); }
        set { ViewState["_BidID"] = value; }
    }
    public int POId
    {
        get { return Convert.ToInt32(ViewState["POId"]); }
        set { ViewState["POId"] = value; }
    }
    public int Year
    {
        get { return Convert.ToInt32(ViewState["_Year"]); }
        set { ViewState["_Year"] = value; }
    }
    public int TaksIDToUpdate
    {
        get { return Convert.ToInt32(ViewState["_TaksIDToUpdate"]); }
        set { ViewState["_TaksIDToUpdate"] = value; }
    }
    public int prtype
    {
        get { return Convert.ToInt32(ViewState["_prtype"]); }
        set { ViewState["_prtype"] = value; }
    }
    public string CompCode
    {
        get { return Convert.ToString(ViewState["_CompCode"]); }
        set { ViewState["_CompCode"] = value; }
    }
    public string VesselCode
    {
        get { return Convert.ToString(ViewState["_VesselCode"]); }
        set { ViewState["_VesselCode"] = value; }
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        
        if (!IsPostBack)
        {

            ShowHeading();
            BindAccount();
        }
    }
    public void ShowHeading()
    {
        Year = 0;
        string sql = " select BidSMDLevel1ApprovalDate,(select prtype from DBO.vw_TBLSMDPOMASTER p where p.poid=b.poid) as prtype from DBO.vw_TBLSMDPOMASTERBid b where b.bidid=" + BidID;
        DataTable DT = Common.Execute_Procedures_Select_ByQuery(sql);
        if (DT.Rows.Count > 0)
        {
            prtype = Common.CastAsInt32(DT.Rows[0]["prtype"]);
            if (Convert.IsDBNull(DT.Rows[0][0]))
            {
                lblHeading.Text = "Select New Account Code";
                lblHeading1.Visible = false;
                btnOpenAddTaskPopup.Visible = false;
                divTaskList.Visible = false;
            }
            else
            {
                Year = Convert.ToDateTime(DT.Rows[0][0]).Year;
                if (Year >= 2017)
                {
                    //lblHeading.Text = "Select New Account Code & Task - " + Convert.ToDateTime(DT.Rows[0][0]).Year;
                    //lblHeading1.Visible = true;
                    //divTaskList.Visible = true;
                    lblHeading.Text = "Select New Account Code - " + Convert.ToDateTime(DT.Rows[0][0]).Year;
                    lblHeading1.Visible = true;
                    divTaskList.Visible = false;
                }
                else
                {
                    lblHeading.Text = "Select New Account Code";
                    lblHeading1.Visible = false;
                    btnOpenAddTaskPopup.Visible = false;
                    divTaskList.Visible = false;
                }
            }
        }
    }
    public void BindAccount()
    {
        //string sql = "select * from (select (select convert(varchar, AccountNumber)+'-'+AccountName from VW_sql_tblSMDPRAccounts PA where DA.AccountID=PA.AccountID and  AccountNumber not like '17%' and AccountNumber !=8590) AccountNumber  ,(select AccountName from  VW_sql_tblSMDPRAccounts PA where DA.AccountID=PA.AccountID and   AccountNumber not like '17%' and AccountNumber !=8590) AccountName  ,AccountID from tblSMDDeptAccounts DA where dept IN (SELECT DEPT FROM DBO.TBLSMDPOMASTER WHERE POID=" + POId + ") and prtype=" + prtype + ") dd where AccountNumber is not null";
        string sql = "select AccountID,convert(varchar, AccountNumber)+'-'+AccountName as AccountNumber from vw_sql_tblSMDPRAccounts order by AccountNumber";
        Common.Set_Procedures("ExecQuery");
        Common.Set_ParameterLength(1);
        Common.Set_Parameters(new MyParameter("@Query", sql));
        DataSet dsPrType = new DataSet();
        dsPrType.Clear();
        dsPrType = Common.Execute_Procedures_Select();
        ddlaccts.DataSource = dsPrType;
        ddlaccts.DataTextField = "AccountNumber";
        ddlaccts.DataValueField = "AccountID";
        ddlaccts.DataBind();
        ddlaccts.Items.Insert(0, new ListItem("<Select>", "0"));
        ddlaccts.SelectedIndex = 0;
    }

    public void ShowTaskList()
    {
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        string sql = "select *,(select SUM(poamount)as TotConsume from VW_TaskLinkedOrders where taskid=tbl_BudgetTracking.taskid) as TotConsume from tbl_BudgetTracking where Company='" + CompCode + "' and VesselCode='" + VesselCode + "' and BudgetYear=" + Year + " and AccountID=" + ddlaccts.SelectedValue + "";
        DataTable DT = Common.Execute_Procedures_Select_ByQuery(sql);
        rptTrackingTaskList.DataSource = DT;
        rptTrackingTaskList.DataBind();
    }
    
    protected void btnCloseAddTrackingTaskPopup_OnClick(object sender, EventArgs e)
    {
        dvAddTrackingTask.Visible = false;
    }
    protected void ddlaccts_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        ShowTaskList();
    }
    protected void btnSaveAccountCode_OnClick(object sender, EventArgs e)
    {
        if (Common.CastAsInt32(ddlaccts.SelectedValue) == 0)
        {
            lblMsg.Text = "Please select account.";
            return;
        }
        //if (Year >= 2017)
        //{
        //    if (TaksIDToUpdate == 0)
        //    {
        //        lblMsg.Text = "Please select Budget allocation.";
        //        return;
        //    }
        //}

        Common.Set_Procedures("sp_PR_IU_AccountTaskDetails");
        Common.Set_ParameterLength(3);
        Common.Set_Parameters
            (
                new MyParameter("@BidID", BidID),
                new MyParameter("@AccountID", ddlaccts.SelectedValue), 
                new MyParameter("@TaskID", TaksIDToUpdate)
            );
        DataSet ResDS = new DataSet();
        bool Res = false;
        Res = Common.Execute_Procedures_IUD(ResDS);
        if (Res)
        {
            lblMsg.Text = "Record saved successfully.";
            this.Visible = false;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "refe", "RefereshPage();", true);
        }
        else
        {
            lblMsg.Text = "Error while saving record.";
            
        }        
    }
    protected void btnCloseAccountCode_OnClick(object sender, EventArgs e)
    {

        this.Visible = false;
    }


    protected void btnOpenAddTaskPopup_OnClick(object sender, EventArgs e)
    {
        if (ddlaccts.SelectedIndex == 0)
        {
            lblMsg.Text = "Please select account";
            return;
        }
        dvAddTrackingTask.Visible = true;
        ClearTrackingControl();
    }
    protected void btnSaveTrackingTask_OnClick(object sender, EventArgs e)
    {
        if (ddlTaskType.SelectedIndex == 0)
        {
            lblMsgTrackingTask.Text = " Please select Budget allocation type.";
            ddlTaskType.Focus();
            return;
        }
        if (txtTtDescription.Text.Trim() == "")
        {
            lblMsgTrackingTask.Text = "Please enter description.";
            txtTtDescription.Focus();
            return;
        }
        if (txtTtAmount.Text.Trim() == "")
        {
            lblMsgTrackingTask.Text = "Please enter amount.";
            txtTtAmount.Focus();
            return;
        }

        if (txtTtDescription.Text.Trim().Length > 250)
        {
            lblMsgTrackingTask.Text = "Description should be within 250 character.";
            txtTtDescription.Focus();
            return;
        }
        Common.Set_Procedures("sp_IU_tbl_BudgetTracking");
        Common.Set_ParameterLength(21);
        Common.Set_Parameters(
            new MyParameter("@TaskID", 0),
            new MyParameter("@Company", CompCode),
            new MyParameter("@VesselCode", VesselCode),
            new MyParameter("@BudgetYear", Year),
            new MyParameter("@AccountID", Common.CastAsInt32(ddlaccts.SelectedValue)),
            new MyParameter("@TaskDescription", txtTtDescription.Text.Trim()),
            new MyParameter("@Amount", txtTtAmount.Text.Trim()),
            new MyParameter("@Jan", DBNull.Value),
            new MyParameter("@Feb", DBNull.Value),
            new MyParameter("@Mar", DBNull.Value),
            new MyParameter("@Apr", DBNull.Value),
            new MyParameter("@May", DBNull.Value),
            new MyParameter("@Jun", DBNull.Value),
            new MyParameter("@Jul", DBNull.Value),
            new MyParameter("@Aug", DBNull.Value),
            new MyParameter("@Sep", DBNull.Value),
            new MyParameter("@Oct", DBNull.Value),
            new MyParameter("@Nov", DBNull.Value),
            new MyParameter("@Dec", DBNull.Value),
            //new MyParameter("@Jan", (chkTtJan.Checked)),
            //new MyParameter("@Feb", (chkTtFeb.Checked)),
            //new MyParameter("@Mar", (chkTtMar.Checked)),
            //new MyParameter("@Apr", (chkTtApr.Checked)),
            //new MyParameter("@May", (chkTtMay.Checked)),
            //new MyParameter("@Jun", (chkTtJun.Checked)),
            //new MyParameter("@Jul", (chkTtJul.Checked)),
            //new MyParameter("@Aug", (chkTtAug.Checked)),
            //new MyParameter("@Sep", (chkTtSep.Checked)),
            //new MyParameter("@Oct", (chkTtOct.Checked)),
            //new MyParameter("@Nov", (chkTtNov.Checked)),
            //new MyParameter("@Dec", (chkTtDec.Checked)),
            new MyParameter("@budgeted", (ddlTaskType.SelectedValue == "1")),
            new MyParameter("@ModifiedBy", Session["UserName"].ToString())
            );
        DataSet Ds = new DataSet();
        Boolean res = false;
        res = Common.Execute_Procedures_IUD(Ds);
        if (res == true)
        {
            int c = Common.CastAsInt32(Ds.Tables[0].Rows[0][0]);
            if (c > 0)
            {
                lblMsgTrackingTask.Text = "Record saved successfully.";
                ClearTrackingControl();                
                ShowTaskList();
            }
            else
            {
                lblMsgTrackingTask.Text = "Please check this task causing total budget amount for this account more than annual budget. It is not allowed.";
            }
        }
        else
        {
            lblMsgTrackingTask.Text = "Record could not saved." + Common.ErrMsg;
        }

    }
    protected void ddlTaskType_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlTaskType.SelectedValue == "2")
        {
            txtTtAmount.Text = "0";
            txtTtAmount.Enabled = false;
        }
        else
        {
            txtTtAmount.Enabled = true;
        }
    }
    public void ClearTrackingControl()
    {
        txtTtAmount.Text = "";
        txtTtDescription.Text = "";
        //lblTaskModifiedByOn.Text = "";
        //chkTtJan.Checked = false;
        //chkTtFeb.Checked = false;
        //chkTtMar.Checked = false;
        //chkTtApr.Checked = false;
        //chkTtMay.Checked = false;
        //chkTtJun.Checked = false;
        //chkTtJul.Checked = false;
        //chkTtAug.Checked = false;
        //chkTtSep.Checked = false;
        //chkTtOct.Checked = false;
        //chkTtNov.Checked = false;
        //chkTtDec.Checked = false;
        ddlTaskType.SelectedIndex = 0;
    }
}
