using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;

public partial class BudgetTracking : System.Web.UI.Page
{
    // PAGE PROPERTIES ------------------
    AuthenticationManager authRecInv;
    public string SelCompanyCode
    {
        get
        { return Convert.ToString(ViewState["SelCompanyCode"]); }
        set
        { ViewState["SelCompanyCode"] = value; }
    }
    public string SelVesselCode
    {
        get
        { return Convert.ToString(ViewState["SelVesselCode"]); }
        set
        { ViewState["SelVesselCode"] = value; }
    }
    public int BudgetYear
    {
        get
        { return int.Parse("0" + ViewState["_BudgetYear"].ToString()); }
        set
        { ViewState["_BudgetYear"] = value; }
    }
    public int SelAccountID
    {
        get
        { return int.Parse("0" + ViewState["SelAccountID"].ToString()); }
        set
        { ViewState["SelAccountID"] = value; }
    }



    public int MajCatID
    {
        get
        { return int.Parse("0" + ViewState["MajCatID"].ToString()); }
        set
        { ViewState["MajCatID"] = value; }
    }
    public int MidCatID
    {
        get
        { return int.Parse("0" + ViewState["MidCatID"].ToString()); }
        set
        { ViewState["MidCatID"] = value; }
    }


    public int TaskID
    {
        get
        { return int.Parse("0" + ViewState["_TaskID"].ToString()); }
        set
        { ViewState["_TaskID"] = value; }
    }
    public string SelClosedBy
    {
        get
        { return Convert.ToString(ViewState["SelClosedBy"]); }
        set
        { ViewState["SelClosedBy"] = value; }
    }

    public DataSet DsTrackingData;
    protected void Page_Load(object sender, EventArgs e)
    {
        //---------------------------------------
        ProjectCommon.SessionCheck();

        //---------------------------------------
        #region --------- USER RIGHTS MANAGEMENT -----------
        try
        {

            authRecInv = new AuthenticationManager(271, int.Parse(Session["loginid"].ToString()), ObjectType.Page);

            if (!(authRecInv.IsView))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "alert('You have not permissions to access this page.');window.close();", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "alert('Your session is expired.');window.close();", true);
        }

        #endregion ----------------------------------------
        lblMsgClosure.Text = "";
        lblMsgTrackingTask.Text = "";
        if (!Page.IsPostBack)
        {
            BudgetYear = DateTime.Today.Year;
            BudgetYear = 2010;
            lblBudgetYear.Text = BudgetYear.ToString();


            BindCompany();
            BindMidCategory();
        }

    }
    // Event -----------------------------------------------------------------------
    protected void ddlCompany_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        BindVessel();
        //BindRptMinorCategory();
    }
    public void ddlShip_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        BindRptMinorCategory();
    }
    public void ddlBudgetType_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        BindRptMinorCategory();
    }
    public void btnShow_OnClick(object sender, EventArgs e)
    {
        BindRptMinorCategory();
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Search.aspx");
    }
    // Function ---------------------------------------------------------------------
    public void BindRptMinorCategory()
    {
        //string GroupBy = " group by MinCatID,MinorCat order by MinorCat ";
        //string WhereClause = " where MidCatID= "+ddlBudgetType.SelectedValue;
        //string sql = "  SELECT top 3 MinCatID,MinorCat,(select SUM(isnull(amount,0.0)) from dbo.tbl_BudgetTracking where AccountID in(select distinct AccountID from [dbo].v_BudgetForecastYear where MidCatID=v_BudgetForecastYear.MidCatID )) as TotAmount_midcat " +
        //             "   FROM [dbo].v_BudgetForecastYear as v_BudgetForecastYear " +
        //             "     INNER JOIN " +
        //             "     ( " +
        //             "         select result as CY from CSVToTablestr('" + (DateTime.Today.Year - 6) + "', ',')  " +
        //             "     )  " +
        //             "     tempYear ON v_BudgetForecastYear.Year = tempYear.CY ";

        //if (ddlCompany.SelectedIndex != 0)
        //    WhereClause = WhereClause + " and CoCode='" + ddlCompany.SelectedValue + "'";

        //if (ddlShip.SelectedIndex != 0)
        //    WhereClause = WhereClause + " and Vess='" + ddlShip.SelectedValue + "'";

        //sql = sql + WhereClause+ GroupBy;
        //DataTable DtRpt = Common.Execute_Procedures_Select_ByQuery(sql);

        Common.Set_Procedures("sp_ShowBudgetTrackingData");
        Common.Set_ParameterLength(4);
        Common.Set_Parameters(
            new MyParameter("@Company", ddlCompany.SelectedValue),
            new MyParameter("@VesselCode", ddlShip.SelectedValue),
            new MyParameter("@BudgetYear", lblBudgetYear.Text),
            new MyParameter("@MidCatID", ddlBudgetType.SelectedValue)
            );
        DataSet Ds = new DataSet();
        DsTrackingData = Common.Execute_Procedures_Select();

        rptMinorCategory.DataSource = DsTrackingData.Tables[0];
        rptMinorCategory.DataBind();


        lblTotalAmount.Text = Common.CastAsDecimal(DsTrackingData.Tables[0].Compute("sum(BudgetAmount)", "")).ToString();
        lblTotalConsumedAmount.Text = Common.CastAsDecimal(DsTrackingData.Tables[0].Compute("sum(ConsumedAmount)", "")).ToString();
        lblTotalVarianceAmount.Text = Convert.ToString(Common.CastAsDecimal(DsTrackingData.Tables[0].Compute("sum(BudgetAmount)", "")) - Common.CastAsDecimal(DsTrackingData.Tables[0].Compute("sum(ConsumedAmount)", "")));
        lblTotalVariancePercentage.Text = GetBudgetVariance(DsTrackingData.Tables[0].Compute("sum(BudgetAmount)", ""), DsTrackingData.Tables[0].Compute("sum(ConsumedAmount)", ""));



        lblTotalBudgetdAmount.Text = Common.CastAsDecimal(DsTrackingData.Tables[2].Compute("sum(BudgetAmount)", "Budgeted=true")).ToString();
        lblTotalUnbudgetdAmount.Text = Common.CastAsDecimal(DsTrackingData.Tables[2].Compute("sum(BudgetAmount)", "Budgeted=false")).ToString();

        lblShipTotal.Text = Common.CastAsDecimal(DsTrackingData.Tables[3].Compute("sum(BudgetAmount)", "")).ToString();
    }
    public DataTable BindRptAccountDetails(string CoCode, string Vessle, int MinorCatID)
    {
        //string WhereClause = " where MinCatID="+ MinorCatID;
        //string sql = " SELECT top 3 ROW_NUMBER()over(order by CoCode,AcctID)as RowNo,*,(select SUM(isnull(amount,0.0)) from dbo.tbl_BudgetTracking where AccountID=v_BudgetForecastYear.AccountID) as TotAmount " +
        //             " FROM  [dbo].v_BudgetForecastYear as  v_BudgetForecastYear    " +
        //             "   INNER JOIN  " +
        //             "   (  " +
        //             "       select result as CY from CSVToTablestr('"+(DateTime.Today.Year-6)+"', ',')  " +
        //             "   )  " +
        //             "   tempYear ON v_BudgetForecastYear.Year = tempYear.CY  ";                   

        //if (ddlCompany.SelectedIndex != 0)
        //    WhereClause = WhereClause + " and CoCode='"+ddlCompany.SelectedValue+"'";

        //if (ddlShip.SelectedIndex != 0)
        //    WhereClause = WhereClause + " and Vess='" + ddlShip.SelectedValue + "'";

        //sql = sql + WhereClause;
        //return Common.Execute_Procedures_Select_ByQuery(sql);
        DataView dv = DsTrackingData.Tables[1].DefaultView;
        dv.RowFilter = "CoCode='" + CoCode + "' and Vess='" + Vessle + "' and MincatID=" + MinorCatID;
        return dv.ToTable();




    }
    public DataTable BindTaskDetails(string CoCode, string Vessle, int MinorCatID, int AccountID)
    {
        //string OrderBy = " Order by taskID ";
        //string WhereClause = " where AccountID = " + AccountID + " and BudgetYear=Year(Getdate()) ";
        //string sql = " select top 4 ROW_NUMBER()over(order by TaskID)as RowNo,TaskDescription,Amount,dbo.fn_BT_GetConsumedAmount(bt.TaskID) as ActualAmount,0 as VarianceAmount,0 as VariancePercentage from tbl_BudgetTracking bt " +
        //            "  where 1=1 ";
        //            //" where 1=1 ";

        //if (ddlCompany.SelectedIndex != 0)
        //    WhereClause = WhereClause + " and Company='" + ddlCompany.SelectedValue + "'";

        //if (ddlShip.SelectedIndex != 0)
        //    WhereClause = WhereClause + " and VesselCode='" + ddlShip.SelectedValue + "'";

        ////sql = sql + WhereClause + OrderBy;
        //return Common.Execute_Procedures_Select_ByQuery(sql);

        DataView dv = DsTrackingData.Tables[2].DefaultView;
        dv.RowFilter = dv.RowFilter = "CoCode='" + CoCode + "' and Vess='" + Vessle + "' and MincatID=" + MinorCatID + " and AccountID=" + AccountID + " AND TASKID IS NOT NULL ";
        return dv.ToTable();
    }


    public void BindCompany()
    {
        string sql = "SELECT VW_sql_tblSMDPRCompany.Company, VW_sql_tblSMDPRCompany.ReportCo " +
            " ,(VW_sql_tblSMDPRCompany.Company + '-' + VW_sql_tblSMDPRCompany.[Company Name]) as CompName" +
        " FROM VW_sql_tblSMDPRCompany WHERE (((VW_sql_tblSMDPRCompany.InAccts)=1)) and (((VW_sql_tblSMDPRCompany.Active)='Y'))";
        DataTable DtCompany = Common.Execute_Procedures_Select_ByQuery(sql);
        if (DtCompany != null)
        {
            ddlCompany.DataSource = DtCompany;
            ddlCompany.DataTextField = "CompName";
            ddlCompany.DataValueField = "Company";
            ddlCompany.DataBind();
            ddlCompany.Items.Insert(0, new ListItem("<Select>", "0"));
            ddlCompany.SelectedIndex = 0;
            BindVessel();
        }
    }
    public void BindVessel()
    {
        string sql = "SELECT VW_sql_tblSMDPRVessels.ShipID, VW_sql_tblSMDPRVessels.Company, VW_sql_tblSMDPRVessels.ShipName, " +
                    " (VW_sql_tblSMDPRVessels.ShipID+' - '+VW_sql_tblSMDPRVessels.ShipName)as ShipNameCode" +
                    " FROM VW_sql_tblSMDPRVessels " +
                    " WHERE (((VW_sql_tblSMDPRVessels.Company)='" + ddlCompany.SelectedValue + "')) ";
        DataTable DtVessel = Common.Execute_Procedures_Select_ByQuery(sql);
        if (DtVessel != null)
        {
            ddlShip.DataSource = DtVessel;
            ddlShip.DataTextField = "ShipNameCode";
            ddlShip.DataValueField = "ShipID";
            ddlShip.DataBind();
            ddlShip.Items.Insert(0, new ListItem("<Select>", ""));
        }

    }
    public void BindMidCategory()
    {
        string sql = "select MidCatID,MidCat from [dbo].tblAccountsMid order by MidCat";
        DataTable DtBudgetType = Common.Execute_Procedures_Select_ByQuery(sql);
        if (DtBudgetType != null)
        {
            ddlBudgetType.DataSource = DtBudgetType;
            ddlBudgetType.DataTextField = "MidCat";
            ddlBudgetType.DataValueField = "MidCatID";
            ddlBudgetType.DataBind();
            //ddlBudgetType.Items.Insert(0, new ListItem("< All >", "0"));
        }
        ddlBudgetType.SelectedIndex = 1;
    }

    //-----------------------------------------------------------------
    protected void btnOpenTaskDetailsPopup_OnClick(object sender, EventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        HiddenField hfTaskID = (HiddenField)btn.Parent.FindControl("hfTaskID");
        HiddenField hfClosedBy = (HiddenField)btn.Parent.FindControl("hfClosedBy");
        HiddenField hfAccountNoName = (HiddenField)btn.Parent.FindControl("hfAccountNoName");
        HiddenField hfTaskConsumedAmount = (HiddenField)btn.Parent.FindControl("hfTaskConsumedAmount");

        lblConsAmount.Text = Common.CastAsDecimal(hfTaskConsumedAmount.Value).ToString("0.00");
        TaskID = Common.CastAsInt32(hfTaskID.Value);
        //SelClosedBy = hfClosedBy.Value;
        if (hfClosedBy.Value == "")
        {
            btnOpenClosuerPopup.Visible = true;
            lblTaskClosedBy.Text = "";
        }
        else
        {
            btnOpenClosuerPopup.Visible = false;
            lblTaskClosedBy.Text = hfClosedBy.Value;
        }
        lblAccountNoName.Text = hfAccountNoName.Value;
        ShowTaskPopupMasterDetails();
        ShowTaskPopupDetails();
        dvTaskDetails.Visible = true;

    }

    protected void btnClosedetails_OnClick(object sender, EventArgs e)
    {
        dvTaskDetails.Visible = false;
        SelClosedBy = "";
    }

    public void ShowTaskPopupMasterDetails()
    {
        string sql = " select * from dbo.tbl_BudgetTracking where TaskID=" + TaskID + " ";
        DataTable Dt = Common.Execute_Procedures_Select_ByQuery(sql);
        if (Dt.Rows.Count > 0)
        {
            DataRow Dr = Dt.Rows[0];
            lblTaskDetails.Text = Dr["TaskDescription"].ToString();
            lblTaskAmount.Text = Common.CastAsDecimal(Dr["Amount"]).ToString("0.00");
            lblProjAmount.Text = "0.00";
            lblVarAmount.Text = Convert.ToString(Common.CastAsDecimal(Dr["Amount"]) - Common.CastAsDecimal(lblConsAmount.Text));
            lblVarPer.Text = GetBudgetVariance(lblTaskAmount.Text, lblConsAmount.Text);
            lblTaskClosedBy.Text = Dr["ClosedBy"].ToString() + " / " + Dr["CloseOn"].ToString();

            if (Dr["Jan"].ToString() == "True") imgJan.Visible = true;
            if (Dr["Feb"].ToString() == "True") imgFeb.Visible = true;
            if (Dr["Mar"].ToString() == "True") imgMar.Visible = true;
            if (Dr["Apr"].ToString() == "True") imgApr.Visible = true;
            if (Dr["May"].ToString() == "True") imgMay.Visible = true;
            if (Dr["Jun"].ToString() == "True") imgJun.Visible = true;
            if (Dr["Jul"].ToString() == "True") imgJul.Visible = true;
            if (Dr["Aug"].ToString() == "True") imgAug.Visible = true;
            if (Dr["Sep"].ToString() == "True") imgSep.Visible = true;
            if (Dr["Oct"].ToString() == "True") imgOct.Visible = true;
            if (Dr["Nov"].ToString() == "True") imgNov.Visible = true;
            if (Dr["Dec"].ToString() == "True") imgDec.Visible = true;

        }
    }
    public void ShowTaskPopupDetails()
    {
        string sql = " select BidID,BidPoNum,s.SupplierName,m.BidSMDLevel1ApprovalDate as PoDate " +
            "  ,isnull(EstShippingUSD,0)+isnull(CreditUSD,0)+ (select sum(isnull(USDTotal,0)) from dbo.tblSMDPODetailBid db where db.BidID=m.BidID ) as PoAmount " +
            "  from dbo.tblsmdpomasterbid m " +
               "  inner join dbo.tblsmdSuppliers s on s.SupplierID = m.SupplierID " +
               " where BidID in( select BidID from dbo.tbl_BudgetTracking_Orders where ItemID=" + TaskID + " ) ";
        DataTable Dt = Common.Execute_Procedures_Select_ByQuery(sql);
        rptTaskDetails.DataSource = Dt;
        rptTaskDetails.DataBind();
    }

    //--Closure---------------------------------------------------------------
    protected void btnOpenClosuerPopup_OnClick(object sender, EventArgs e)
    {
        dvClosure.Visible = true;
        txtClosureRemarks.Text = "";
    }
    protected void btnCloseClosurePopup_OnClick(object sender, EventArgs e)
    {
        dvClosure.Visible = false;
    }
    protected void btnSaveClosure_OnClick(object sender, EventArgs e)
    {
        if (txtClosureRemarks.Text.Trim() == "")
        {
            lblMsgClosure.Text = "Please enter closure remark";
            return;
        }
        Common.Set_Procedures("sp_TaskClosure");
        Common.Set_ParameterLength(3);
        Common.Set_Parameters(
            new MyParameter("@TaskID", TaskID),
            new MyParameter("@ClosedBy", Session["UserName"].ToString()),
            new MyParameter("@ClosureRemarks", txtClosureRemarks.Text.Trim())
            );
        DataSet Ds = new DataSet();
        Boolean res = false;
        res = Common.Execute_Procedures_IUD(Ds);
        if (res == true)
        {
            lblMsgClosure.Text = "Record saved successfully.";
            BindRptMinorCategory();
            btnOpenClosuerPopup.Visible = false;
        }
        else
            lblMsgClosure.Text = "Record could not saved." + Common.ErrMsg;
    }

    //--Add Task---------------------------------------------------------------    
    protected void btnOpenAddTaskPopup_OnClick(object sender, EventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        HiddenField hfCompany = (HiddenField)btn.Parent.FindControl("hfCompany");
        HiddenField hfVessel = (HiddenField)btn.Parent.FindControl("hfVessel");
        HiddenField hfAccountID = (HiddenField)btn.Parent.FindControl("hfAccountID");

        SelCompanyCode = hfCompany.Value;
        SelVesselCode = hfVessel.Value;
        SelAccountID = Common.CastAsInt32(hfAccountID.Value);

        dvAddTrackingTask.Visible = true;
    }


    protected void btnCloseAddTrackingTaskPopup_OnClick(object sender, EventArgs e)
    {
        dvAddTrackingTask.Visible = false;
        ClearTrackingControl();
        TaskID = 0;

        SelCompanyCode = "";
        SelVesselCode = "";
    }
    protected void btnSaveTrackingTask_OnClick(object sender, EventArgs e)
    {
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
            new MyParameter("@Company", SelCompanyCode),
            new MyParameter("@VesselCode", SelVesselCode),
            new MyParameter("@BudgetYear", BudgetYear),
            new MyParameter("@AccountID", SelAccountID),
            new MyParameter("@TaskDescription", txtTtDescription.Text.Trim()),
            new MyParameter("@Amount", txtTtAmount.Text.Trim()),
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
            new MyParameter("@budgeted", 0),
            new MyParameter("@ModifiedBy", Session["UserName"].ToString())
            );
        DataSet Ds = new DataSet();
        Boolean res = false;
        res = Common.Execute_Procedures_IUD(Ds);
        if (res == true)
        {
            lblMsgTrackingTask.Text = "Record saved successfully.";
            ClearTrackingControl();
            BindRptMinorCategory();
            //BindTrackingTaskList();
            //UpTask.Update();

        }
        else
        {
            lblMsgTrackingTask.Text = "Record could not saved." + Common.ErrMsg;
        }

    }
    public void ClearTrackingControl()
    {
        TaskID = 0;
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
    }

    public string GetBudgetVariance(object b, object c)
    {
        decimal bb = Common.CastAsDecimal(b);
        decimal cc = Common.CastAsDecimal(c);
        if (cc == 0)
            return "0.00";
        else
            return Common.CastAsDecimal(((bb - cc) * 100) / bb).ToString("0.00");

    }

}
