using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;

public partial class NextYearBudgetForecastEntry : System.Web.UI.Page
{
    public AuthenticationManager authRecInv;
    public string SessionDeleimeter = ConfigurationManager.AppSettings["SessionDelemeter"];
    DataSet DsValue;
    int d11, d22;

    public decimal Mon1, Mon2, Mon3, Mon4, Mon5, Mon6, Mon7, Mon8, Mon9, Mon10, Mon11, Mon12;
   // public decimal Month1, Month2, Month3, Month4, Month5, Month6, Month7, Month8, Month9, Month10, Month11, Month12;
    public int IsIndianFinacialYr
    {
        get
        { return int.Parse("0" + ViewState["IsIndianFinacialYr"]); }
        set
        { ViewState["IsIndianFinacialYr"] = value; }
    }

    public int IsBudgetDistributeByDays
    {
        get
        { return int.Parse("0" + ViewState["IsBudgetDistributeByDays"]); }
        set
        { ViewState["IsBudgetDistributeByDays"] = value; }
    }

    public string FinancialYr
    {
        get
        { return ViewState["FinancialYr"].ToString(); }
        set
        { ViewState["FinancialYr"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //---------------------------------------
       ProjectCommon.SessionCheck();
        //---------------------------------------
        #region --------- USER RIGHTS MANAGEMENT -----------
        try
        {
            authRecInv = new AuthenticationManager(1091, int.Parse(Session["loginid"].ToString()), ObjectType.Page);
            if (!(authRecInv.IsView))
            {
                Response.Redirect("~/Unauthorized.aspx", false);
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("~/Unauthorized.aspx");
        }

        #endregion ----------------------------------------
        lblmsg.Text = "";
        if (!IsPostBack)
        {
            lblBudgetYear.Text = Convert.ToString(System.DateTime.Now.Year+1);  //$$$$ remove - 1
            lblYr.Text = " " + System.DateTime.Now.Year.ToString();
            lblYr1.Text = " " + System.DateTime.Now.Year.ToString();
            lblYr3.Text = " " + System.DateTime.Now.Year.ToString();

            lblYr1_.Text = Convert.ToString(System.DateTime.Now.Year + 1);
            
            lblYrNext.Text = " " + lblBudgetYear.Text;

            lblYrr11.Text = Convert.ToString(System.DateTime.Now.Year);
            lblYrr12.Text = Convert.ToString(System.DateTime.Now.Year);
            lblYrr13.Text = Convert.ToString(System.DateTime.Now.Year);
            FinancialYr = lblBudgetYear.Text;
            BindCompany();
            BindBudgetType();
            if (Page.PreviousPage != null)
            {
                DropDownList ddl=(DropDownList)Page.PreviousPage.FindControl("ddlCompany");
                ddlCompany.SelectedIndex = ddl.SelectedIndex;
                ddlCompany_OnSelectedIndexChanged(sender, e);  
                ddl = (DropDownList)Page.PreviousPage.FindControl("ddlShip");
                ddlShip.SelectedIndex = ddl.SelectedIndex;
                ddlShip_OnSelectedIndexChanged(sender, e);
            }
            if (Request.QueryString["Vsl"] != null)
            {
                DataTable dt=Common.Execute_Procedures_Select_ByQuery("select company from VW_sql_tblSMDPRVessels where ShipId='" + Request.QueryString["Vsl"] + "' and VesselNo in (Select VesselId from UserVesselRelation with(nolock) where Loginid = " + Convert.ToInt32(Session["loginid"].ToString())+") ");
                if (dt.Rows.Count > 0)
                {
                    string Company = dt.Rows[0][0].ToString().Trim();
                    ddlCompany.SelectedValue = Company;
                    ddlCompany_OnSelectedIndexChanged(sender, e);
                    ddlShip.SelectedValue = Request.QueryString["Vsl"];
                    ddlShip_OnSelectedIndexChanged(sender, e);
                }
            }
            BindRepeater();
        }
        Print.Visible = authRecInv.IsPrint;
        imgApprove.Visible = authRecInv.IsVerify2;
        //imgbtnPublish.Visible = authRecInv.IsVerify2;  
    }

    // Event ----------------------------------------------------------------
    
    // Button
    protected void imgSave_OnClick(object sender, EventArgs e)
    {
        if (ddlCompany.SelectedIndex == 0)
        {
            lblmsg.Text = "Select the company.";
            ddlCompany.Focus();
            return;
        }
        if (ddlShip.SelectedIndex == 0)
        {
            lblmsg.Text = "Select the vessel.";
            ddlShip.Focus();
            return;
        }
        DataSet Ds = new DataSet();
        Boolean res;
        decimal dCYAmt;
        Ds = new DataSet();
        res = false;
        TextBox txtAnnAmt = new TextBox();
        TimeSpan TS = Convert.ToDateTime(txtEndDate.Text).Subtract(Convert.ToDateTime(txtStartDate.Text));
        for (int i = 0; i <= rptBudget.Items.Count - 1; i++)
        {
            RepeaterItem RptItm = rptBudget.Items[i];
            //Master Data
            txtAnnAmt = (TextBox)RptItm.FindControl("txtAnnAmt");
            //  get monthly values
            if (IsIndianFinacialYr == 1)
            {
                if (IsBudgetDistributeByDays == 1)
                {
                    dCYAmt = Common.CastAsDecimal(ResetNewValuesforIndianFinacialYearBudgetbyDays(Common.CastAsDecimal(txtAnnAmt.Text)));
                }
                else
                {
                    dCYAmt = Common.CastAsDecimal(ResetNewValuesforIndianFinacialYearBudgetbyMonthly(Common.CastAsDecimal(txtAnnAmt.Text)));
                }
            }
            else
            {
                if (IsBudgetDistributeByDays == 1)
                {
                    dCYAmt = Common.CastAsDecimal(ResetNewValuesBudgetbyDays(Common.CastAsDecimal(txtAnnAmt.Text))); ;
                }
                else
                {
                    dCYAmt = Common.CastAsDecimal(ResetNewValuesBudgetbyMonthly(Common.CastAsDecimal(txtAnnAmt.Text)));
                }
            }
            
            HiddenField hfAccID = (HiddenField)RptItm.FindControl("hfAccID");
            //-------------------------------------------------------------------------------------
            //Details Data
            HiddenField hfAccountID = (HiddenField)RptItm.FindControl("hfAccountID");
            HiddenField hfAccountNumber = (HiddenField)RptItm.FindControl("hfAccountNumber");

            string MonthlyBDG = Mon1.ToString() + "," + Mon2.ToString() + "," + Mon3.ToString() + "," + Mon4.ToString() + "," + Mon5.ToString() + "," + Mon6.ToString() + "," + Mon7.ToString() + "," + Mon8.ToString() + "," + Mon9.ToString() + "," + Mon10.ToString() + "," + Mon11.ToString() + "," + Mon12.ToString();
            //-------------------------------------------------------------------------------------
            Common.Set_Procedures("sp_NewPR_UpdateBudgetDetails_NextYear");
            Common.Set_ParameterLength(13);
            Common.Set_Parameters(
                                new MyParameter("@AcctID", hfAccID.Value),
                                new MyParameter("@VessStart", txtStartDate.Text),
                                new MyParameter("@VessEnd", txtEndDate.Text),
                                new MyParameter("@Forecast", txtAnnAmt.Text),
                                new MyParameter("@CYAmt", dCYAmt),
                                new MyParameter("@CoCode", ddlCompany.SelectedValue),
                                new MyParameter("@ShipID", ddlShip.SelectedValue),
                                new MyParameter("@Year", lblBudgetYear.Text),        // $$$$ what year would be here --------------
                                new MyParameter("@YearDays", TS.Days),
                                new MyParameter("@UpdatedBy", Session["FullName"].ToString()),
                                new MyParameter("@MonthlyBDG", MonthlyBDG),
                                new MyParameter("@AccountID", hfAccountID.Value),
                                new MyParameter("@Accountnumber", hfAccountNumber.Value)

                );
            Ds = new DataSet();
            res = Common.Execute_Procedures_IUD(Ds);
            if (res)
            {
                lblmsg.Text = "Record saved successfully.";
            }
            else
            {
                lblmsg.Text = Common.ErrMsg;
            }
        }
        if (res)
            BindRepeater();
    }
    protected void imgApprove_OnClick(object sender, EventArgs e)
    {
        string SQL = "UPDATE [dbo].tblSMDBudgetForecastYear set Budget=ForeCast,ApprovedBy='" + Session["FullName"].ToString() + "',ApprovedOn=getdate() " +
                   "where CoCode='" + ddlCompany.SelectedValue + "' AND CurFinYear='" + FinancialYr.ToString() + "' And ShipID='" + ddlShip.SelectedValue + "'; select 'True' ";
        string SQL1 = "UPDATE [dbo].tblSMDBudgetForecast set Budget=ForeCast,ApprovedBy='" + Session["FullName"].ToString() + "',ApprovedOn=getdate() " +
                   "where CoCode='" + ddlCompany.SelectedValue + "' AND CurFinYear='" + FinancialYr.ToString() + "' And RIGHT(AcctID,4)=(SELECT REPLACE(STR(VesselId,4),' ','0') FROM Vessel with(nolock) WHERE VesselCode='" + ddlShip.SelectedValue + "'); select 'True' ";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
        DataTable dt1 = Common.Execute_Procedures_Select_ByQuery(SQL1);

        if (dt != null || dt1 != null)
        {
            lblmsg.Text = "Record updated successfully.";
        }
        else
        {
            lblmsg.Text = "Record could not updated . "+Common.ErrMsg;
        }
    }
    protected void imgImport_OnClick(object sender, EventArgs e)
    {
        if (ddlCompany.SelectedIndex == 0)
        {
            lblmsg.Text = "Select the company.";
            ddlCompany.Focus();
            return;
        }
        if (ddlShip.SelectedIndex == 0)
        {
            lblmsg.Text = "Select the vessel.";
            ddlShip.Focus();
            return;
        }

        string sql = "dbo.SP_NEW_ImportBudget '" + ddlCompany.SelectedValue + "','" + ddlShip.SelectedValue + "'," + lblBudgetYear.Text + ",'" + Session["UserName"].ToString() + "'";
        try
        {
            DataTable dt=Common.Execute_Procedures_Select_ByQuery(sql);
            string recds = "0";
            if (dt != null)
                if (dt.Rows.Count > 0)
                {
                    recds = dt.Rows[0][0].ToString();  
                }
            lblmsg.Text ="[ " + recds +  " ] records updated.";
        }
        catch (Exception ex) {
            lblmsg.Text = "Unable to Import Budget : " + ex.Message ;
        } 
    }
    protected void btnreload_OnClick(object sender, EventArgs e)
    {
        BindRepeater();
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Search.aspx");
    }
    protected void imgClear_Click(object sender, EventArgs e)
    {


    }
    protected void imgBudgetForcasting_Click(object sender, EventArgs e)
    {
        Response.Redirect("BudgetForecasting.aspx");
    }
    protected void imgReportingAndAnalysis_Click(object sender, EventArgs e)
    {
        Response.Redirect("ReportingAndAnalysis.aspx");
    }

    // DropDown
    protected void ddlCompany_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        ValidateCompanyforIndianFinanceYr(ddlCompany.SelectedValue);
        BindVessel();
        BindRepeater();
    }
    public void ddlShip_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        BindRepeater();
    }
    public void ddlBudgetType_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        BindRepeater();
        //if (ddlBudgetType.SelectedValue == "14" || ddlBudgetType.SelectedValue == "16")
        //{
        //    imgSave.Visible = false;
        //}
        //else
        //{
        //    imgSave.Visible = true;
        //}
    }

    // Function ----------------------------------------------------------------------------------------
    public string ResetNewValuesBudgetbyMonthly(decimal AnnualAmt)
    {
        Decimal DBudget = 0;
        if (ddlBudgetType.SelectedValue == "6" || ddlBudgetType.SelectedValue == "25") // this case is when selected Dry Docking or Pre Delivery Expenses
        {
            int DBMonth = Common.CastAsInt32(GetMonthFromDB().ToString());
            if (DBMonth <= 0)
            {
                Mon1 = AnnualAmt;
                Mon2 = 0;
                Mon3 = 0;
                Mon4 = 0;
                Mon5 = 0;
                Mon6 = 0;
                Mon7 = 0;
                Mon8 = 0;
                Mon9 = 0;
                Mon10 = 0;
                Mon11 = 0;
                Mon12 = 0;
                return AnnualAmt.ToString();

            }
            else
            {
                Mon1 = 0;
                Mon2 = 0;
                Mon3 = 0;
                Mon4 = 0;
                Mon5 = 0;
                Mon6 = 0;
                Mon7 = 0;
                Mon8 = 0;
                Mon9 = 0;
                Mon10 = 0;
                Mon11 = 0;
                Mon12 = 0;
                switch (Common.CastAsInt32(DBMonth))
                {
                    case 1:
                        Mon1 = AnnualAmt;
                        break;
                    case 2:
                        Mon2 = AnnualAmt;
                        break;
                    case 3:
                        Mon3 = AnnualAmt;
                        break;
                    case 4:
                        Mon4 = AnnualAmt;
                        break;
                    case 5:
                        Mon5 = AnnualAmt;
                        break;
                    case 6:
                        Mon6 = AnnualAmt;
                        break;
                    case 7:
                        Mon7 = AnnualAmt;
                        break;
                    case 8:
                        Mon8 = AnnualAmt;
                        break;
                    case 9:
                        Mon9 = AnnualAmt;
                        break;
                    case 10:
                        Mon10 = AnnualAmt;
                        break;
                    case 11:
                        Mon11 = AnnualAmt;
                        break;
                    case 12:
                        Mon12 = AnnualAmt;
                        break;
                }
                return AnnualAmt.ToString();
            }
        }
        else
        {
            DBudget = Math.Round(AnnualAmt / 12, 0);
            if (txtStartDate.Text == "" && txtEndDate.Text == "")
            {
                Mon1 = DBudget;
                Mon2 = DBudget;
                Mon3 = DBudget;
                Mon4 = DBudget;
                Mon5 = DBudget;
                Mon6 = DBudget;
                Mon7 = DBudget;
                Mon8 = DBudget;
                Mon9 = DBudget;
                Mon10 = DBudget;
                Mon11 = DBudget;
                Mon12 = DBudget;
                string CalculatedBudget = (Mon1 + Mon2 + Mon3 + Mon4 + Mon5 + Mon6 + Mon7 + Mon8 + Mon9 + Mon10 + Mon11 + Mon12).ToString();
                return CalculatedBudget;

            }
            else
            {
                int Month = Convert.ToDateTime(txtStartDate.Text).Month;
                string val = "0.00";
                ViewState.Add("TotalBudget", "0");
                for (int i = 1; i <= 12; i++)
                {
                    if (i >= Month + 1)
                    {
                        val = DBudget.ToString();
                    }
                    else if (i == Month)
                    {
                        int startDay = Convert.ToDateTime(txtStartDate.Text).Day;
                        if (startDay == 1)
                        {
                            val = DBudget.ToString();
                        }
                        else
                        {
                            int Dateyear = Convert.ToDateTime(txtStartDate.Text).Year;
                            int DateMonth = Convert.ToDateTime(txtStartDate.Text).Month;
                            int DaysInMonth = DateTime.DaysInMonth(Dateyear, DateMonth);
                            int TotalBudgetDays = DaysInMonth - startDay + 1;
                            decimal PerDayBudget = DBudget / DaysInMonth;
                            val = Math.Round(Common.CastAsDecimal(PerDayBudget * TotalBudgetDays), 0).ToString();
                        }
                    }

                    switch (i)
                    {
                        case 1:
                            Mon1 = Common.CastAsDecimal(val);
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 2:
                            Mon2 = Common.CastAsDecimal(val);
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 3:
                            Mon3 = Common.CastAsDecimal(val);
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 4:
                            Mon4 = Common.CastAsDecimal(val);
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 5:
                            Mon5 = Common.CastAsDecimal(val);
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 6:
                            Mon6 = Common.CastAsDecimal(val);
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 7:
                            Mon7 = Common.CastAsDecimal(val);
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 8:
                            Mon8 = Common.CastAsDecimal(val);
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 9:
                            Mon9 = Common.CastAsDecimal(val);
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 10:
                            Mon10 = Common.CastAsDecimal(val);
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 11:
                            Mon11 = Common.CastAsDecimal(val);
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 12:
                            Mon12 = Common.CastAsDecimal(val);
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                    }
                }
                //CalculateTotal();
                string CalculatedBudget = (Mon1 + Mon2 + Mon3 + Mon4 + Mon5 + Mon6 + Mon7 + Mon8 + Mon9 + Mon10 + Mon11 + Mon12).ToString();
                return CalculatedBudget;
            }
        }

    }

    public string ResetNewValuesforIndianFinacialYearBudgetbyMonthly(decimal AnnualAmt)
    {
        Decimal DBudget = 0;
        if (ddlBudgetType.SelectedValue == "6" || ddlBudgetType.SelectedValue == "25") // this case is when selected Dry Docking or Pre Delivery Expenses
        {
            int DBMonth = Common.CastAsInt32(GetMonthFromDB().ToString());
            if (DBMonth <= 0)
            {
                Mon1 = AnnualAmt;
                Mon2 = 0;
                Mon3 = 0;
                Mon4 = 0;
                Mon5 = 0;
                Mon6 = 0;
                Mon7 = 0;
                Mon8 = 0;
                Mon9 = 0;
                Mon10 = 0;
                Mon11 = 0;
                Mon12 = 0;
                return AnnualAmt.ToString();

            }
            else
            {
                Mon1 = 0;
                Mon2 = 0;
                Mon3 = 0;
                Mon4 = 0;
                Mon5 = 0;
                Mon6 = 0;
                Mon7 = 0;
                Mon8 = 0;
                Mon9 = 0;
                Mon10 = 0;
                Mon11 = 0;
                Mon12 = 0;
                switch (Common.CastAsInt32(DBMonth))
                {
                    case 1:
                        Mon1 = AnnualAmt;
                        break;
                    case 2:
                        Mon2 = AnnualAmt;
                        break;
                    case 3:
                        Mon3 = AnnualAmt;
                        break;
                    case 4:
                        Mon4 = AnnualAmt;
                        break;
                    case 5:
                        Mon5 = AnnualAmt;
                        break;
                    case 6:
                        Mon6 = AnnualAmt;
                        break;
                    case 7:
                        Mon7 = AnnualAmt;
                        break;
                    case 8:
                        Mon8 = AnnualAmt;
                        break;
                    case 9:
                        Mon9 = AnnualAmt;
                        break;
                    case 10:
                        Mon10 = AnnualAmt;
                        break;
                    case 11:
                        Mon11 = AnnualAmt;
                        break;
                    case 12:
                        Mon12 = AnnualAmt;
                        break;
                }
                return AnnualAmt.ToString();
            }
        }
        else
        {
            DBudget = Math.Round(AnnualAmt / 12, 0);
            if (txtStartDate.Text == "" && txtEndDate.Text == "")
            {
                Mon1 = DBudget;
                Mon2 = DBudget;
                Mon3 = DBudget;
                Mon4 = DBudget;
                Mon5 = DBudget;
                Mon6 = DBudget;
                Mon7 = DBudget;
                Mon8 = DBudget;
                Mon9 = DBudget;
                Mon10 = DBudget;
                Mon11 = DBudget;
                Mon12 = DBudget;
                string CalculatedBudget = (Mon1 + Mon2 + Mon3 + Mon4 + Mon5 + Mon6 + Mon7 + Mon8 + Mon9 + Mon10 + Mon11 + Mon12).ToString();
                return CalculatedBudget;

            }
            else
            {
                int Month = Convert.ToDateTime(txtStartDate.Text).Month;
                string val = "0.00";
                ViewState.Add("TotalBudget", "0");
                if (Month >= 4 && Month <= 12)
                {
                    Month = Month - 3;
                }
                else
                {
                    Month = Month + 9;
                }
                for (int i = 1; i <= 12; i++)
                {
                    if (i >= Month + 1)
                    {
                        val = DBudget.ToString();
                    }
                    else if (i == Month)
                    {
                        int startDay = Convert.ToDateTime(txtStartDate.Text).Day;
                        if (startDay == 1)
                        {
                            val = DBudget.ToString();
                        }
                        else
                        {
                            int Dateyear = Convert.ToDateTime(txtStartDate.Text).Year;
                            int DateMonth = Convert.ToDateTime(txtStartDate.Text).Month;
                            int DaysInMonth = DateTime.DaysInMonth(Dateyear, DateMonth);
                            int TotalBudgetDays = DaysInMonth - startDay + 1;
                            decimal PerDayBudget = DBudget / DaysInMonth;
                            val = Math.Round(Common.CastAsDecimal(PerDayBudget * TotalBudgetDays), 0).ToString();
                        }
                    }

                    switch (i)
                    {
                        case 1:
                            Mon1 = Common.CastAsDecimal(val);
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 2:
                            Mon2 = Common.CastAsDecimal(val);
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 3:
                            Mon3 = Common.CastAsDecimal(val);
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 4:
                            Mon4 = Common.CastAsDecimal(val);
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 5:
                            Mon5 = Common.CastAsDecimal(val);
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 6:
                            Mon6 = Common.CastAsDecimal(val);
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 7:
                            Mon7 = Common.CastAsDecimal(val);
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 8:
                            Mon8 = Common.CastAsDecimal(val);
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 9:
                            Mon9 = Common.CastAsDecimal(val);
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 10:
                            Mon10 = Common.CastAsDecimal(val);
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 11:
                            Mon11 = Common.CastAsDecimal(val);
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 12:
                            Mon12 = Common.CastAsDecimal(val);
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                    }
                }
                //CalculateTotal();
                string CalculatedBudget = (Mon1 + Mon2 + Mon3 + Mon4 + Mon5 + Mon6 + Mon7 + Mon8 + Mon9 + Mon10 + Mon11 + Mon12).ToString();
                return CalculatedBudget;
            }
        }

    }

    public string ResetNewValuesBudgetbyDays(decimal AnnualAmt)
    {
        Decimal DBudget = 0;
        if (ddlBudgetType.SelectedValue == "6" || ddlBudgetType.SelectedValue == "25") // this case is when selected Dry Docking or Pre Delivery Expenses
        {
            int DBMonth = Common.CastAsInt32(GetMonthFromDB().ToString());
            if (DBMonth <= 0)
            {
                Mon1 = AnnualAmt;
                Mon2 = 0;
                Mon3 = 0;
                Mon4 = 0;
                Mon5 = 0;
                Mon6 = 0;
                Mon7 = 0;
                Mon8 = 0;
                Mon9 = 0;
                Mon10 = 0;
                Mon11 = 0;
                Mon12 = 0;
                return AnnualAmt.ToString();

            }
            else
            {
                Mon1 = 0;
                Mon2 = 0;
                Mon3 = 0;
                Mon4 = 0;
                Mon5 = 0;
                Mon6 = 0;
                Mon7 = 0;
                Mon8 = 0;
                Mon9 = 0;
                Mon10 = 0;
                Mon11 = 0;
                Mon12 = 0;
                switch (Common.CastAsInt32(DBMonth))
                {
                    case 1:
                        Mon1 = AnnualAmt;
                        break;
                    case 2:
                        Mon2 = AnnualAmt;
                        break;
                    case 3:
                        Mon3 = AnnualAmt;
                        break;
                    case 4:
                        Mon4 = AnnualAmt;
                        break;
                    case 5:
                        Mon5 = AnnualAmt;
                        break;
                    case 6:
                        Mon6 = AnnualAmt;
                        break;
                    case 7:
                        Mon7 = AnnualAmt;
                        break;
                    case 8:
                        Mon8 = AnnualAmt;
                        break;
                    case 9:
                        Mon9 = AnnualAmt;
                        break;
                    case 10:
                        Mon10 = AnnualAmt;
                        break;
                    case 11:
                        Mon11 = AnnualAmt;
                        break;
                    case 12:
                        Mon12 = AnnualAmt;
                        break;
                }
                return AnnualAmt.ToString();
            }
        }
        else
        {
            DBudget = Math.Round(AnnualAmt / 12, 0);
            if (txtStartDate.Text == "" && txtEndDate.Text == "")
            {
                Mon1 = DBudget;
                Mon2 = DBudget;
                Mon3 = DBudget;
                Mon4 = DBudget;
                Mon5 = DBudget;
                Mon6 = DBudget;
                Mon7 = DBudget;
                Mon8 = DBudget;
                Mon9 = DBudget;
                Mon10 = DBudget;
                Mon11 = DBudget;
                Mon12 = DBudget;
                string CalculatedBudget = (Mon1 + Mon2 + Mon3 + Mon4 + Mon5 + Mon6 + Mon7 + Mon8 + Mon9 + Mon10 + Mon11 + Mon12).ToString();
                return CalculatedBudget;

            }
            else
            {
                int Month = Convert.ToDateTime(txtStartDate.Text).Month;
                string val = "0.00";
                ViewState.Add("TotalBudget", "0");
                int inputYear = Convert.ToDateTime(txtStartDate.Text).Year;
                int DaysinYear = DateTime.IsLeapYear(inputYear) ? 366 : 365;
                decimal PerDayBudget = AnnualAmt / DaysinYear;
                for (int i = 1; i <= 12; i++)
                {
                    if (i >= Month + 1)
                    {
                        // val = DBudget.ToString();
                        int Dateyear = Convert.ToDateTime(txtStartDate.Text).Year;
                        int DateMonth = i;
                        int DaysInMonth = DateTime.DaysInMonth(Dateyear, DateMonth);
                        val = Math.Round(Common.CastAsDecimal(PerDayBudget * DaysInMonth), 0).ToString();
                    }
                    else if (i == Month)
                    {
                        int startDay = Convert.ToDateTime(txtStartDate.Text).Day;
                        if (startDay == 1)
                        {
                            // val = DBudget.ToString();
                            int Dateyear = Convert.ToDateTime(txtStartDate.Text).Year;
                            int DateMonth = Convert.ToDateTime(txtStartDate.Text).Month;
                            int DaysInMonth = DateTime.DaysInMonth(Dateyear, DateMonth);
                            int TotalBudgetDays = DaysInMonth - startDay + 1;
                            val = Math.Round(Common.CastAsDecimal(PerDayBudget * TotalBudgetDays), 0).ToString();
                        }
                        else
                        {
                            int Dateyear = Convert.ToDateTime(txtStartDate.Text).Year;
                            int DateMonth = Convert.ToDateTime(txtStartDate.Text).Month;
                            int DaysInMonth = DateTime.DaysInMonth(Dateyear, DateMonth);
                            int TotalBudgetDays = DaysInMonth - startDay + 1;
                            // decimal PerDayBudget = DBudget / DaysInMonth;
                            val = Math.Round(Common.CastAsDecimal(PerDayBudget * TotalBudgetDays), 0).ToString();
                        }
                    }

                    switch (i)
                    {
                        case 1:
                            Mon1 = Common.CastAsDecimal(val);
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 2:
                            Mon2 = Common.CastAsDecimal(val);
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 3:
                            Mon3 = Common.CastAsDecimal(val);
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 4:
                            Mon4 = Common.CastAsDecimal(val);
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 5:
                            Mon5 = Common.CastAsDecimal(val);
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 6:
                            Mon6 = Common.CastAsDecimal(val);
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 7:
                            Mon7 = Common.CastAsDecimal(val);
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 8:
                            Mon8 = Common.CastAsDecimal(val);
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 9:
                            Mon9 = Common.CastAsDecimal(val);
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 10:
                            Mon10 = Common.CastAsDecimal(val);
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 11:
                            Mon11 = Common.CastAsDecimal(val);
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 12:
                            Mon12 = Common.CastAsDecimal(val);
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                    }
                }
                //CalculateTotal();
                string CalculatedBudget = (Mon1 + Mon2 + Mon3 + Mon4 + Mon5 + Mon6 + Mon7 + Mon8 + Mon9 + Mon10 + Mon11 + Mon12).ToString();
                return CalculatedBudget;
            }
        }

    }

    public string ResetNewValuesforIndianFinacialYearBudgetbyDays(decimal AnnualAmt)
    {
        Decimal DBudget = 0;
        if (ddlBudgetType.SelectedValue == "6" || ddlBudgetType.SelectedValue == "25") // this case is when selected Dry Docking or Pre Delivery Expenses
        {
            int DBMonth = Common.CastAsInt32(GetMonthFromDB().ToString());
            if (DBMonth <= 0)
            {
                Mon1 = AnnualAmt;
                Mon2 = 0;
                Mon3 = 0;
                Mon4 = 0;
                Mon5 = 0;
                Mon6 = 0;
                Mon7 = 0;
                Mon8 = 0;
                Mon9 = 0;
                Mon10 = 0;
                Mon11 = 0;
                Mon12 = 0;
                return AnnualAmt.ToString();

            }
            else
            {
                Mon1 = 0;
                Mon2 = 0;
                Mon3 = 0;
                Mon4 = 0;
                Mon5 = 0;
                Mon6 = 0;
                Mon7 = 0;
                Mon8 = 0;
                Mon9 = 0;
                Mon10 = 0;
                Mon11 = 0;
                Mon12 = 0;
                switch (Common.CastAsInt32(DBMonth))
                {
                    case 1:
                        Mon1 = AnnualAmt;
                        break;
                    case 2:
                        Mon2 = AnnualAmt;
                        break;
                    case 3:
                        Mon3 = AnnualAmt;
                        break;
                    case 4:
                        Mon4 = AnnualAmt;
                        break;
                    case 5:
                        Mon5 = AnnualAmt;
                        break;
                    case 6:
                        Mon6 = AnnualAmt;
                        break;
                    case 7:
                        Mon7 = AnnualAmt;
                        break;
                    case 8:
                        Mon8 = AnnualAmt;
                        break;
                    case 9:
                        Mon9 = AnnualAmt;
                        break;
                    case 10:
                        Mon10 = AnnualAmt;
                        break;
                    case 11:
                        Mon11 = AnnualAmt;
                        break;
                    case 12:
                        Mon12 = AnnualAmt;
                        break;
                }
                return AnnualAmt.ToString();
            }
        }
        else
        {
            DBudget = Math.Round(AnnualAmt / 12, 0);
            if (txtStartDate.Text == "" && txtEndDate.Text == "")
            {
                Mon1 = DBudget;
                Mon2 = DBudget;
                Mon3 = DBudget;
                Mon4 = DBudget;
                Mon5 = DBudget;
                Mon6 = DBudget;
                Mon7 = DBudget;
                Mon8 = DBudget;
                Mon9 = DBudget;
                Mon10 = DBudget;
                Mon11 = DBudget;
                Mon12 = DBudget;
                string CalculatedBudget = (Mon1 + Mon2 + Mon3 + Mon4 + Mon5 + Mon6 + Mon7 + Mon8 + Mon9 + Mon10 + Mon11 + Mon12).ToString();
                return CalculatedBudget;

            }
            else
            {
                int Month = Convert.ToDateTime(txtStartDate.Text).Month;
                string val = "0.00";
                ViewState.Add("TotalBudget", "0");
                if (Month >= 4 && Month <= 12)
                {
                    Month = Month - 3;
                }
                else
                {
                    Month = Month + 9;
                }
                int inputYear = Convert.ToDateTime(txtStartDate.Text).Year;
                int DaysinYear = DateTime.IsLeapYear(inputYear + 1) ? 366 : 365;
                decimal PerDayBudget = AnnualAmt / DaysinYear;
                int tempMonth = Convert.ToDateTime(txtStartDate.Text).Month;
                int tempMonth2 = 0;
                for (int i = 1; i <= 12; i++)
                {
                    if (i >= Month + 1)
                    {
                        //val = DBudget.ToString();
                        int Dateyear = Convert.ToDateTime(txtStartDate.Text).Year;
                        tempMonth = tempMonth + 1;
                        tempMonth2 = 0;
                        if (tempMonth >= 4 && tempMonth <= 12)
                        {
                            tempMonth2 = tempMonth;
                        }
                        else
                        {
                            tempMonth2 = tempMonth - 12;
                            Dateyear = Dateyear + 1;
                        }
                        int DaysInMonth = DateTime.DaysInMonth(Dateyear, tempMonth2);
                        val = Math.Round(Common.CastAsDecimal(PerDayBudget * DaysInMonth), 0).ToString();
                    }
                    else if (i == Month)
                    {
                        int startDay = Convert.ToDateTime(txtStartDate.Text).Day;
                        if (startDay == 1)
                        {
                            //val = DBudget.ToString();
                            int Dateyear = Convert.ToDateTime(txtStartDate.Text).Year;
                            int DateMonth = Convert.ToDateTime(txtStartDate.Text).Month;
                            int DaysInMonth = DateTime.DaysInMonth(Dateyear, DateMonth);
                            int TotalBudgetDays = DaysInMonth - startDay + 1;
                            val = Math.Round(Common.CastAsDecimal(PerDayBudget * TotalBudgetDays), 0).ToString();
                        }
                        else
                        {
                            int Dateyear = Convert.ToDateTime(txtStartDate.Text).Year;
                            int DateMonth = Convert.ToDateTime(txtStartDate.Text).Month;
                            int DaysInMonth = DateTime.DaysInMonth(Dateyear, DateMonth);
                            int TotalBudgetDays = DaysInMonth - startDay + 1;
                            //decimal PerDayBudget = DBudget / DaysInMonth;
                            val = Math.Round(Common.CastAsDecimal(PerDayBudget * TotalBudgetDays), 0).ToString();
                        }
                    }

                    switch (i)
                    {
                        case 1:
                            Mon1 = Common.CastAsDecimal(val);
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 2:
                            Mon2 = Common.CastAsDecimal(val);
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 3:
                            Mon3 = Common.CastAsDecimal(val);
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 4:
                            Mon4 = Common.CastAsDecimal(val);
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 5:
                            Mon5 = Common.CastAsDecimal(val);
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 6:
                            Mon6 = Common.CastAsDecimal(val);
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 7:
                            Mon7 = Common.CastAsDecimal(val);
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 8:
                            Mon8 = Common.CastAsDecimal(val);
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 9:
                            Mon9 = Common.CastAsDecimal(val);
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 10:
                            Mon10 = Common.CastAsDecimal(val);
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 11:
                            Mon11 = Common.CastAsDecimal(val);
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 12:
                            Mon12 = Common.CastAsDecimal(val);
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                    }
                }
                //CalculateTotal();
                string CalculatedBudget = (Mon1 + Mon2 + Mon3 + Mon4 + Mon5 + Mon6 + Mon7 + Mon8 + Mon9 + Mon10 + Mon11 + Mon12).ToString();
                return CalculatedBudget;
            }
        }

    }
    public void GetTotalAmount()
    {
        int AccoutId = 0;
        int TotAmt = 0;
        int ActTotal = 0;
        int ActCalTotal = 0;
        int BudgetTotal = 0;

        DataTable dtData = Common.Execute_Procedures_Select_ByQuery("EXEC DBO.[fn_NEW_GETCMBUDGETACTUAL_ACCOUNTWISE] '" + ddlCompany.SelectedValue + "'," + DateTime.Today.Month.ToString() + "," + DateTime.Today.Year.ToString() + ",'" + ddlShip.SelectedValue + "'");
        foreach (RepeaterItem itm in rptBudget.Items)
        {
            AccoutId = Common.CastAsInt32(((HiddenField)itm.FindControl("hfAccountID")).Value);
            DataView dv = dtData.DefaultView;
            dv.RowFilter = "AccountID=" + AccoutId.ToString();
            DataTable dt1 = dv.ToTable();
            //-----------------------------
            int ActAmt = 0, ActAmtCal = 0;
            if (dt1.Rows.Count > 0)
            {
                ActAmt = Common.CastAsInt32(dt1.Rows[0][1].ToString());
                ActAmtCal = Common.CastAsInt32(dt1.Rows[0][2].ToString());
            }
            //-----------------------------
            ((Label)itm.FindControl("lblActAmt")).Text = ActAmt.ToString();
            ((Label)itm.FindControl("lblActAmtCal")).Text = ActAmtCal.ToString();
            //-----------------------------
            TextBox lblActAmt = (TextBox)itm.FindControl("txtAnnAmt");
            HiddenField hfd = (HiddenField)itm.FindControl("hfAccountNumber");
            Label lblBudget = (Label)itm.FindControl("lblBudgetCal");
            Label lblt = (Label)itm.FindControl("lblBudgetCal");

            if (!(hfd.Value.Trim().StartsWith("17")))
            {
                ActTotal = ActTotal + ActAmt;
                ActCalTotal = ActCalTotal + ActAmtCal;
                BudgetTotal = BudgetTotal + Common.CastAsInt32(lblBudget.Text);
                TotAmt = TotAmt + Common.CastAsInt32(lblActAmt.Text);
            }

            Label l1 = (Label)itm.FindControl("lblVarAC");
            l1.Text = getVariance(lblBudget.Text, ActAmtCal.ToString());

            l1 = (Label)itm.FindControl("lblVarProj");
            l1.Text = getVariance(ActAmtCal.ToString(), lblActAmt.Text);

            l1 = (Label)itm.FindControl("lblVarBug");
            l1.Text = getVariance(lblBudget.Text, lblActAmt.Text);

            decimal usecalc = Common.CastAsDecimal(lblBudget.Text);
            if (d11 > 0)
            {
                usecalc = (usecalc / d11) * d22;
            }
            l1.Text = getVariance(usecalc.ToString(), lblActAmt.Text);
        }
        lblTotal.Text = string.Format("{0:C}", TotAmt).Replace("$", "").Replace(".00", "");
        lblActTotal.Text = string.Format("{0:C}", ActTotal).Replace("$", "").Replace(".00", "");
        lblActCalTotal.Text = string.Format("{0:C}", ActCalTotal).Replace("$", "").Replace(".00", "");
        lblBudgetTotal.Text = string.Format("{0:C}", BudgetTotal).Replace("$", "").Replace(".00", "");
        
        


        //if (Common.CastAsInt32(lblDays.Text) == 0)
        //{
        //    lblavgDailyCost.Text = "0";
        //}
        //else
        //{
        //    decimal amt = Math.Round(Convert.ToDecimal(Common.CastAsDecimal(TotAmt) / Common.CastAsInt32(lblDays.Text)), 2);
        //    lblavgDailyCost.Text = string.Format("{0:C}", amt).Replace("$", "").Replace(".00", "");
        //}

        //if(Common.CastAsInt32(ViewState["LastDays"])<=0)
        //    lblavgDailyCost1.Text = string.Format("{0:C}", BudgetTotal / 1.0).Replace("$", "").Replace(".00", "");
        //else
        //    lblavgDailyCost1.Text = string.Format("{0:C}", Convert.ToDecimal(BudgetTotal)/Common.CastAsInt32(ViewState["LastDays"])).Replace("$", "").Replace(".00", "");
    }
    protected void CalculateSum(object sender, EventArgs e)
    {
        GetTotalAmount();
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "tr", "SetLastFocus('dvscroll_BF');", true);
    }
    public string getVariance(string Actvalue,string BaseValue)
    {
        decimal dAct = Common.CastAsDecimal(Actvalue);
        decimal dBase = Common.CastAsDecimal(BaseValue);
        if (dAct > 0)
            return Convert.ToString(Math.Round(((dBase-dAct)/dAct) * 100, 0)) + " %";
        else
            return "0";
    }
    //Binding Function
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
                    " WHERE VW_sql_tblSMDPRVessels.Company='" + ddlCompany.SelectedValue + "' and VesselNo in (Select VesselId from UserVesselRelation with(nolock) where Loginid = " + Convert.ToInt32(Session["loginid"].ToString())+")  ";
        DataTable DtVessel = Common.Execute_Procedures_Select_ByQuery(sql);
        if (DtVessel != null)
        {
            ddlShip.DataSource = DtVessel;
            ddlShip.DataTextField = "ShipNameCode";
            ddlShip.DataValueField = "ShipID";
            ddlShip.DataBind();
            ddlShip.Items.Insert(0, new ListItem("<Select>", "0"));
        }

    }
    public void BindBudgetType()
    {
        string sql = "select * from [dbo].tblAccountsMajor where MajSeqNo in(25,30,100,300,400,450,500,550)";
        DataTable DtBudgetType = Common.Execute_Procedures_Select_ByQuery(sql);
        if (DtBudgetType != null)
        {
            ddlBudgetType.DataSource = DtBudgetType;
            ddlBudgetType.DataTextField = "MajorCat";
            ddlBudgetType.DataValueField = "MajCatID";
            ddlBudgetType.DataBind();
            //ddlBudgetType.Items.Insert(0, new ListItem("< All >", "0"));
        }
        ddlBudgetType.SelectedIndex = 1;  
    }
    private int GetMonthFromDB()
    {
        int DBMonth = 0;
        string sql = "select top 1 forecast as actforecast,period,round(forecast,0)as forecast  from [dbo].tblSMDBudgetForecast  " +
                    " where CoCode='" + ddlCompany.SelectedValue + "'  " +
                    " AND AccountID in (select AccountID from [dbo].sql_tblSMDPRAccounts where MajCatID=" + ddlBudgetType.SelectedValue + ") " +
                    " AND forecast>0 AND VESSNO=" +  (ddlShip.SelectedValue) + " AND Year=" + (Common.CastAsInt32(lblBudgetYear.Text) - 1).ToString() + " order by period";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
        if (dt != null)
            if (dt.Rows.Count > 0)
            {
                DBMonth = Common.CastAsInt32(dt.Rows[0]["period"]);
            }
        return DBMonth;
    }
    public void BindRepeater()
    {
        string sql;
        #region Code Comments
        //sql = "SELECT v_BudgetForecastYear.ApprovedBy,replace(convert(varchar(15), v_BudgetForecastYear.ApprovedOn,106),' ','-')as ApprovedOn,v_BudgetForecastYear.ImportedBy,replace(convert(varchar(15), v_BudgetForecastYear.ImportedOn,106),' ','-')as ImportedOn,v_BudgetForecastYear.UpdatedBy, " +
        //            " replace(convert(varchar(15), v_BudgetForecastYear.UpdatedOn,106),' ','-')as  UpdatedOn, "+
        //            " v_BudgetForecastYear.AccountID,v_BudgetForecastYear.AccountNumber,v_BudgetForecastYear.AcctID,v_BudgetForecastYear.MajCatID,v_BudgetForecastYear.mincatid, "+
        //            " v_BudgetForecastYear.[AccountName] AS AccountName1,round(isnull(v_BudgetForecastYear.Forecast,0),0) as Forecast,replace( convert(varchar(100), "+
        //            " v_BudgetForecastYear.VessStart,106),' ','-')as  VessStart,replace( convert(varchar(100), v_BudgetForecastYear.VessEnd,106),' ','-') as VessEnd, "+
        //            " v_BudgetForecastYear.YearDays,  (select midcat from dbo.tblaccountsmid G2 where G2.midcatid=v_BudgetForecastYear.midcatid) as Group1, "+
        //            " v_BudgetForecastYear.midcatid,  (select minorcat from dbo.tblaccountsminor G3 where G3.mincatid=v_BudgetForecastYear.mincatid) as Group2, "+
        //            " v_BudgetForecastYear.AccountNumber, "+
        //            " AnnAmt=isnull((select Amount from Add_v_BudgetForecastYear Addt where Addt.cocode=v_BudgetForecastYear.cocode and Addt.AcctId=v_BudgetForecastYear.AcctId and Addt.Byear=" + lblBudgetYear.Text + "),0)  , " +
        //            " ForeCastComment=isnull((select YearComment from [dbo].v_BudgetForecastYear V1 where V1.CoCode='" + ddlCompany.SelectedValue + "' AND V1.Vess='" + ddlShip.SelectedValue + "' and V1.AcctId=v_BudgetForecastYear.AcctId and V1.Year=" + (Common.CastAsInt32(lblBudgetYear.Text) - 1).ToString() + "),'')  , " +
        //            " AnnAmtCurrentYear=isnull((select Amount from Add_v_BudgetForecastYear Addt where Addt.cocode=v_BudgetForecastYear.cocode and Addt.AcctId=v_BudgetForecastYear.AcctId and Addt.Byear=2011),0), "+
        //            " (SELECT  Max(tblPeriodMaint.rptPeriod) AS maxPer FROM [dbo].tblPeriodMaint WHERE (((tblPeriodMaint.perClosed)=1) AND ((tblPeriodMaint.rptYear)>=" + (Common.CastAsInt32(lblBudgetYear.Text)-1).ToString() + ")) and cocode='0') as maxPer, " +
        //            " isnull(( SELECT round(isnull(v_BudgetForecastYearNext.Forecast,0),0) as Forecast FROM  [dbo].v_BudgetForecastYear as  v_BudgetForecastYearNext INNER JOIN (select result as CY from CSVToTablestr('" + (Common.CastAsInt32(lblBudgetYear.Text) - 1).ToString() + "',','))tempYear ON v_BudgetForecastYearNext.Year = tempYear.CY WHERE v_BudgetForecastYearNext.CoCode='" + ddlCompany.SelectedValue + "' AND v_BudgetForecastYearNext.Vess='" + ddlShip.SelectedValue + "' and v_BudgetForecastYearNext.accountID=v_BudgetForecastYear.accountID),0)as ForecastNext," +
        //            " round(isnull(v_BudgetForecastYear.Forecast,0),0) as Budget, "+
        //            " isnull(v_BudgetForecastYear.Actual,0)as Actual " +
        //            " ,0 as NewActualAmount " +
        //            " ,0 as NewActualCal " +
        //            " ,(case when month(v_BudgetForecastYear.VessStart)>1 or day(v_BudgetForecastYear.VessStart) >1 then  "+
        //              " cast(((v_BudgetForecastYear.Budget/(datediff(day,v_BudgetForecastYear.VessStart,v_BudgetForecastYear.VessEnd)+1))*365) as dec(12,2))"+
        //             " else isnull(v_BudgetForecastYear.Budget ,0)end) as NewBudgetCal  "+

        //            " FROM  [dbo].v_BudgetForecastYear as  v_BudgetForecastYear   "+
        //            " INNER JOIN (select result as CY from CSVToTablestr('" + (Common.CastAsInt32(lblBudgetYear.Text) - 2).ToString() + "',','))tempYear ON v_BudgetForecastYear.Year = tempYear.CY  " +
        //            " WHERE  v_BudgetForecastYear.CoCode='"+ddlCompany.SelectedValue+"' AND v_BudgetForecastYear.Vess='"+ddlShip.SelectedValue+"'  "+
        //            " ORDER BY v_BudgetForecastYear.MajSeqNo, v_BudgetForecastYear.MidSeqNo, v_BudgetForecastYear.MinSeqNo, v_BudgetForecastYear.AccountNumber";
        #endregion
        //string sql = "SELECT v_BudgetForecastYear.ApprovedBy,replace(convert(varchar(15), v_BudgetForecastYear.ApprovedOn,106),' ','-')as ApprovedOn,v_BudgetForecastYear.ImportedBy,replace(convert(varchar(15), v_BudgetForecastYear.ImportedOn,106),' ','-')as ImportedOn,v_BudgetForecastYear.UpdatedBy, " +
        //            " replace(convert(varchar(15), v_BudgetForecastYear.UpdatedOn,106),' ','-')as  UpdatedOn, " +
        //            " v_BudgetForecastYear.AccountID,v_BudgetForecastYear.AccountNumber,v_BudgetForecastYear.AcctID,v_BudgetForecastYear.MajCatID,v_BudgetForecastYear.mincatid, " +
        //            " v_BudgetForecastYear.[AccountName] AS AccountName1,round(isnull(v_BudgetForecastYear.Forecast,0),0) as Forecast,replace( convert(varchar(100), " +
        //            " v_BudgetForecastYear.VessStart,106),' ','-')as  VessStart,replace( convert(varchar(100), v_BudgetForecastYear.VessEnd,106),' ','-') as VessEnd, " +
        //            " v_BudgetForecastYear.YearDays,  (select midcat from dbo.tblaccountsmid G2 where G2.midcatid=v_BudgetForecastYear.midcatid) as Group1, " +
        //            " v_BudgetForecastYear.midcatid,  (select minorcat from dbo.tblaccountsminor G3 where G3.mincatid=v_BudgetForecastYear.mincatid) as Group2, " +
        //            " v_BudgetForecastYear.AccountNumber, " +
        //            " AnnAmt=isnull((select Amount from Add_v_BudgetForecastYear Addt where Addt.cocode=v_BudgetForecastYear.cocode and Addt.AcctId=v_BudgetForecastYear.AcctId and Addt.Byear=2012),0)  , " +
        //            " AnnAmtCurrentYear=isnull((select Amount from Add_v_BudgetForecastYear Addt where Addt.cocode=v_BudgetForecastYear.cocode and Addt.AcctId=v_BudgetForecastYear.AcctId and Addt.Byear=2011),0), " +
        //            " (SELECT  Max(tblPeriodMaint.rptPeriod) AS maxPer FROM [dbo].tblPeriodMaint WHERE (((tblPeriodMaint.perClosed)=1) AND ((tblPeriodMaint.rptYear)>=2011)) and cocode='0') as maxPer, " +
        //            " isnull(( SELECT round(isnull(v_BudgetForecastYearNext.Forecast,0),0) as Forecast FROM  [dbo].v_BudgetForecastYear as  v_BudgetForecastYearNext INNER JOIN (select result as CY from CSVToTablestr('2011',','))tempYear ON v_BudgetForecastYearNext.Year = tempYear.CY WHERE v_BudgetForecastYearNext.CoCode='" + ddlCompany.SelectedValue + "' AND v_BudgetForecastYearNext.Vess='" + ddlShip.SelectedValue + "' and v_BudgetForecastYearNext.accountID=v_BudgetForecastYear.accountID),0)as ForecastNext," +
        //            " isnull(v_BudgetForecastYear.Budget,0)as Budget,  " +
        //            " isnull(v_BudgetForecastYear.Actual,0)as Actual " +
        //            " ,isnull(TblAmt.amount ,0) as NewActualAmount " +

        //            " ,round(TblAmt.amount + ((v_BudgetForecastYear.Budget/12)*(12-(datediff(month,convert(smalldatetime,'01/01/" + (Common.CastAsInt32(lblBudgetYear.Text) - 1).ToString() + "'),getdate())+1))),2) as NewActualCal " +

        //            " ,(case when month(v_BudgetForecastYear.VessStart)>1 or day(v_BudgetForecastYear.VessStart) >1 then  " +
        //              " cast(((v_BudgetForecastYear.Budget/(datediff(day,v_BudgetForecastYear.VessStart,v_BudgetForecastYear.VessEnd)+1))*365) as dec(12,2))" +
        //                " else isnull(v_BudgetForecastYear.Budget ,0)end) as NewBudgetCal  " +

        //            " FROM  [dbo].v_BudgetForecastYear as  v_BudgetForecastYear   " +
        //            " INNER JOIN (select result as CY from CSVToTablestr('" + (Common.CastAsInt32(lblBudgetYear.Text) - 2).ToString() + "',','))tempYear ON v_BudgetForecastYear.Year = tempYear.CY  " +

        //            " left join (SELECT * FROM DBO.getConsumedByAccountId('" + ddlCompany.SelectedValue + "','" + ddlShip.SelectedValue + "','12/31/" + (Common.CastAsInt32(lblBudgetYear.Text) - 1).ToString() + "')) TblAmt on TblAmt.accountID=v_BudgetForecastYear.accountID " +

        //            " WHERE  v_BudgetForecastYear.CoCode='" + ddlCompany.SelectedValue + "' AND v_BudgetForecastYear.Vess='" + ddlShip.SelectedValue + "'  " +
        //            " ORDER BY v_BudgetForecastYear.MajSeqNo, v_BudgetForecastYear.MidSeqNo, v_BudgetForecastYear.MinSeqNo, v_BudgetForecastYear.AccountNumber";


        //Response.Write(sql);
        //Response.End();

        sql = "EXEC DBO.GETNEXTYEARFORECAST '" + ddlCompany.SelectedValue + "','" + ddlShip.SelectedValue + "'," + lblBudgetYear.Text; 

DataTable DtRpt = Common.Execute_Procedures_Select_ByQuery(sql);

        if (DtRpt != null)
        {
            try
            {
                if (DtRpt.Rows.Count > 0)
                {
                    ViewState["LastDays"] = DtRpt.Rows[0]["YearDays"].ToString();  
                    lblYr.Text = " " + System.DateTime.Now.Year.ToString() + "[" + DtRpt.Rows[0]["YearDays"].ToString() + "/365]"  ;

                    d11 = Common.CastAsInt32(DtRpt.Rows[0]["YearDays"].ToString());
                    d22 = 365;

                    string Qry="select YearDays,approvedby,replace(convert(varchar,approvedon,106),' ','-') as ApprovedOn,UpdatedBy, " +
                               "Importedby,replace(convert(varchar,ImportedOn,106),' ','-') as ImportedOn,replace(convert(varchar,UpdatedOn,106),' ','-') as UpdatedOn," +
                               "replace(convert(varchar,vessstart,106),' ','-') as vessstart,replace(convert(varchar,vessend,106),' ','-') as vessend " +
                               "from dbo.tblsmdbudgetforecastyear where cocode='" + ddlCompany.SelectedValue + "' and shipid='" + ddlShip.SelectedValue + "' and CurFinYear='" + FinancialYr.ToString() +"'";

                    DataTable dtheader = Common.Execute_Procedures_Select_ByQuery(Qry);
                    if (dtheader.Rows.Count > 0)
                    {
                        txtStartDate.Text = dtheader.Rows[0]["VessStart"].ToString();
                        txtEndDate.Text = dtheader.Rows[0]["VessEnd"].ToString();
                        lblDays.Text = dtheader.Rows[0]["YearDays"].ToString();

                        lblUppByAndOn.Text = dtheader.Rows[0]["ApprovedBy"].ToString() + " / " + dtheader.Rows[0]["ApprovedOn"].ToString();
                        lblUppByAndOn.Text = (lblUppByAndOn.Text == " / ") ? "" : lblUppByAndOn.Text;
                        lblCreatedBy.Text = dtheader.Rows[0]["UpdatedBy"].ToString() + " / " + dtheader.Rows[0]["UpdatedOn"].ToString();
                        lblCreatedBy.Text = (lblCreatedBy.Text == " / ") ? "" : lblCreatedBy.Text;
                    }
                    else
                    {
                        DateTime d1 = DateTime.Parse("01-Apr-" + lblBudgetYear.Text);
                        DateTime d2 = DateTime.Parse("31-Mar-" + Convert.ToString(Convert.ToInt32(lblBudgetYear.Text) + 1));

                        txtStartDate.Text = d1.ToString("dd-MMM-yyyy"); //DtRpt.Rows[0]["VessStart"].ToString();
                        txtEndDate.Text = d2.ToString("dd-MMM-yyyy"); //DtRpt.Rows[0]["VessEnd"].ToString();
                        lblDays.Text = d2.Subtract(d1).Days.ToString(); //DtRpt.Rows[0]["YearDays"].ToString();

                        lblUppByAndOn.Text = "";
                        lblUppByAndOn.Text = "";
                        lblCreatedBy.Text = "";
                        lblCreatedBy.Text = "";
                    }
                }
                DataView dtView = DtRpt.DefaultView;
                dtView.RowFilter = "MajCatID=" + ddlBudgetType.SelectedValue + "";
                rptBudget.DataSource = dtView.ToTable();
                rptBudget.DataBind();
                //GetTotalAmount();
                lblCompany.Text = ddlCompany.SelectedValue + " - " + ddlShip.SelectedValue + " - " + lblBudgetYear.Text;
                GetTotalAmount();
                BindVesselSummary();
            }
            catch (Exception ex)
            {
                lblmsg.Text = "Unable to show data. : " + ex.Message;
		lit1.Text = "";
            }
        }
        else
        {
            lblmsg.Text = "No data found for this vessel. : " + Common.ErrMsg;
	    lit1.Text = "";
        }
    }
    protected void BindVesselSummary()
    {
        string VesselCode = ddlShip.SelectedValue;
        if (ddlShip.SelectedIndex <= 0)
            return; 
        DataTable dt_Result = new DataTable();
        int ColumnSum;
        int VesselDays;
        ColumnSum = 0;
        VesselDays =Common.CastAsInt32(lblDays.Text);
        StringBuilder sb = new StringBuilder();
        DataTable dtAccts = Common.Execute_Procedures_Select_ByQuery("select distinct midcatid,midcat,MajSeqNo,MidSeqNo from dbo.v_New_CurrYearBudgetHome WHERE MajCatId=6 ORDER BY MajSeqNo,MidSeqNo");
        DataTable dtAccts1 = Common.Execute_Procedures_Select_ByQuery("select distinct midcatid,midcat,MajSeqNo,MidSeqNo from dbo.v_New_CurrYearBudgetHome WHERE MajCatId<>6 ORDER BY MajSeqNo,MidSeqNo");
        sb.Append("<table border='1' width='100%' cellpadding='1' cellspacing='0' class='newformat' height='220px'");
        sb.Append("<tr>");
        sb.Append("<td class='header' style='font-size:10px; width:20px;'>&nbsp;</td>");
        sb.Append("<td class='header' style='font-size:10px; text-align:left; font-weight:bold;'>Budget Summary<br/></td>");
        dt_Result.Columns.Add("BudgetHead");
        string Ships = "";
        Ships = Ships + ((Ships.Trim() == "") ? "" : ",") + VesselCode;

        sb.Append("<td class='header' style='font-size:10px; width:60px; text-align:left; font-weight:bold;'>Amt ( $ )");
        //sb.Append("<td class='header' style='font-size:10px;'><input type='radio' name='radVSL' value='" + VesselCode + "' id='rad" + VesselCode + "'><label for='rad" + VesselCode + "'>" + VesselCode + "</label>");
        DataTable dtDays = Common.Execute_Procedures_Select_ByQuery("select top 1 Days From dbo.v_New_CurrYearBudgetHome Where shipid='" + VesselCode + "' AND CurFinYear='" + FinancialYr.ToString() + "' order by days desc");
        if (dtDays != null)
        {
            if (dtDays.Rows.Count > 0)
            {
                VesselDays = Common.CastAsInt32(dtDays.Rows[0][0]);
            }
        }

        DataTable dtYB = Common.Execute_Procedures_Select_ByQueryCMS("select yearbuilt from dbo.vessel Where vesselcode='" + VesselCode + "'");
        if (dtYB != null)
        {
            if (dtYB.Rows.Count > 0)
            {
                if (dtYB.Rows[0][0].ToString().Trim() != "")
                {
                    sb.Append("</br> [" + dtYB.Rows[0][0].ToString() + "]");
                }
            }
        }

        sb.Append("</td>");
        dt_Result.Columns.Add(VesselCode);
        
        //sb.Append("<td class='header' style='font-size:10px;'>Total</td>");
        dt_Result.Columns.Add("Total");
        sb.Append("</tr>");
        // YEAR BUILT
        //-----------------
        DataRow dr_yb = dt_Result.NewRow();
        dr_yb[0] = "";
        
        DataTable dt_yb = Common.Execute_Procedures_Select_ByQueryCMS("select yearbuilt from vessel where vesselcode='" + VesselCode + "'");
        if (dt_yb.Rows.Count > 0)
        {
            dr_yb[VesselCode] = "[ " + dt_yb.Rows[0][0].ToString() + " ]";
        }

        dr_yb[dt_Result.Columns.Count - 1] = "";
        dt_Result.Rows.Add(dr_yb);
        //-----------------
        // DATA ROWS 
        for (int i = 0; i <= dtAccts.Rows.Count - 1; i++)
        {
            int RowSum = 0;
            DataRow dr = dt_Result.NewRow();
            sb.Append("<tr>");
            sb.Append("<td style='text-align:center'><a target='_blank' href='ForeCastComments.aspx?Comp=" + ddlCompany.SelectedValue + "&Ship=" + VesselCode + "&Fyear=" + lblBudgetYear.Text + "&MidCat=" + dtAccts.Rows[i][0].ToString() + "&HeadName=" + dtAccts.Rows[i][1].ToString() + "' title='Remarks' ><img src='../HRD/Images/icon_comment.gif' /></a></td>");
            sb.Append("<td style='text-align:left'>" + dtAccts.Rows[i][1].ToString() + "</td>");
            dr[0] = dtAccts.Rows[i][1].ToString();
            
            DataTable dtShip = Common.Execute_Procedures_Select_ByQuery("select NEXTYEARFORECASTAMOUNT from dbo.v_New_CurrYearBudgetHome where shipid='" + VesselCode + "' AND CurFinYear='" + FinancialYr.ToString() + "' and  midcatid=" + dtAccts.Rows[i][0].ToString());
            if (dtShip != null)
            {
                if (dtShip.Rows.Count > 0)
                {
                    sb.Append("<td style='text-align:right'>" + FormatCurrency(dtShip.Rows[0][0]) + "</td>");
                    ColumnSum += Common.CastAsInt32(dtShip.Rows[0][0]);
                    RowSum += Common.CastAsInt32(dtShip.Rows[0][0]);
                    dr[VesselCode] = FormatCurrency(dtShip.Rows[0][0]);
                }
                else
                {
                    sb.Append("<td></td>");
                    dr[VesselCode] = "0";
                }
            }
            else
            {
                sb.Append("<td></td>");
                dr[VesselCode] = "0";
            }

            //sb.Append("<td style='text-align:right'>" + FormatCurrency(RowSum) + "</td>");
            //dr[dt_Result.Columns.Count - 1] = FormatCurrency(RowSum);
            //sb.Append("</tr>");
            dt_Result.Rows.Add(dr);
        }
        //---------------
        // TOTAL
        int VSLhaving_TotalMoreThan0 = 0;
        DataRow dr1 = dt_Result.NewRow();
        sb.Append("<tr class='header' style='background-color :#C2C2C2;color:Black'>");
        sb.Append("<td>&nbsp;</td>");
        sb.Append("<td style='font-size:10px;text-align:right; font-weight:bold;'>Total(US$)</td>");
        dr1[0] = "Total(US$)";
        int GrossSum = 0;

        sb.Append("<td style='font-size:10px;text-align:right; font-weight:bold;'>" + FormatCurrency(ColumnSum) + "</td>");
        GrossSum += ColumnSum;
        dr1[0] = FormatCurrency(ColumnSum);
        if (ColumnSum > 0)
        {
            VSLhaving_TotalMoreThan0++;
        }
        
        //sb.Append("<td style='font-size:10px;text-align:right'>" + FormatCurrency(GrossSum) + "</td>");
        dr1[dt_Result.Columns.Count - 1] = FormatCurrency(GrossSum);
        sb.Append("</tr>");
        dt_Result.Rows.Add(dr1);

        // PER DAY CALC
        DataRow dr2 = dt_Result.NewRow();
        sb.Append("<tr class='header' style='background-color :#C2C2C2;color:Black'>");
        sb.Append("<td>&nbsp;</td>");
        sb.Append("<td style='font-size:10px;text-align:right;font-weight:bold;'>Avg Daily Cost(US$)</td>");
        dr2[0] = "Avg Daily Cost(US$)";
        //int GrossSum = 0;
       
        try
        {
            sb.Append("<td style='font-size:10px;text-align:right;font-weight:bold;'>" + FormatCurrency((ColumnSum / VesselDays)) + "</td>");
            dr2[0] = FormatCurrency((ColumnSum/ VesselDays));
        }
        catch (DivideByZeroException ex)
        {
            sb.Append("<td style='font-size:10px;text-align:right;font-weight:bold;'>0</td>");
            dr2[0] = "0";
        }
        catch (Exception ex)
        {
            throw ex;
        }
        //GrossSum += ColumnSum[i - 1];
           
        //if (VSLhaving_TotalMoreThan0 > 0)
        //{
        //    sb.Append("<td style='font-size:10px;text-align:right'>" + FormatCurrency((GrossSum / 366) / VSLhaving_TotalMoreThan0) + "</td>");
        //    dr2[dt_Result.Columns.Count - 1] = FormatCurrency((GrossSum / 366) / VSLhaving_TotalMoreThan0);
        //}
        //else
        //{
        //    sb.Append("<td style='font-size:10px;text-align:right'>" + FormatCurrency((GrossSum / 366)) + "</td>");
        //    dr2[dt_Result.Columns.Count - 1] = FormatCurrency((GrossSum / 366));
        //}

        sb.Append("</tr>");
        dt_Result.Rows.Add(dr2);

        // DATA ROWS  - 2
        for (int i = 0; i <= dtAccts1.Rows.Count - 1; i++)
        {
            int RowSum = 0;
            DataRow dr = dt_Result.NewRow();
            sb.Append("<tr>");
            sb.Append("<td style='text-align:center'><a target='_blank' href='ForeCastComments.aspx?Comp=" + ddlCompany.SelectedValue + "&Ship=" + VesselCode + "&Fyear=" + lblBudgetYear.Text + "&MidCat=" + dtAccts1.Rows[i][0].ToString() + "&HeadName=" + dtAccts1.Rows[i][1].ToString() + "' title='Remarks'> <img src='../HRD/Images/icon_comment.gif' /></a></td>");
            sb.Append("<td style='text-align:left;'>" + dtAccts1.Rows[i][1].ToString() + "</td>");
            dr[0] = dtAccts1.Rows[i][1].ToString();
            
            DataTable dtShip = Common.Execute_Procedures_Select_ByQuery("select NEXTYEARFORECASTAMOUNT from dbo.v_New_CurrYearBudgetHome where shipid='" + VesselCode + "' AND CurFinYear='" + FinancialYr.ToString() +"' and  midcatid=" + dtAccts1.Rows[i][0].ToString());
            if (dtShip != null)
            {
                if (dtShip.Rows.Count > 0)
                {
                    sb.Append("<td style='text-align:right'>" + FormatCurrency(dtShip.Rows[0][0]) + "</td>");
                    ColumnSum += Common.CastAsInt32(dtShip.Rows[0][0]);
                    RowSum += Common.CastAsInt32(dtShip.Rows[0][0]);
                    dr[VesselCode] = FormatCurrency(dtShip.Rows[0][0]);
                }
                else
                {
                    sb.Append("<td></td>");
                    dr[VesselCode] = "0";
                }
            }
            else
            {
                sb.Append("<td></td>");
                dr[VesselCode] = "0";
            }
               
            //sb.Append("<td style='text-align:right'>" + FormatCurrency(RowSum) + "</td>");
            sb.Append("</tr>");
            dr[dt_Result.Columns.Count - 1] = FormatCurrency(RowSum);
            dt_Result.Rows.Add(dr);
        }

        // GROSS TOTAL
        DataRow dr3 = dt_Result.NewRow();
        sb.Append("<tr class='header' style='background-color :#C2C2C2;color:Black;'>");
        sb.Append("<td style='text-align:center'><a target='_blank' href='ForeCastComments.aspx?Comp=" + ddlCompany.SelectedValue + "&Ship=" + VesselCode + "&Fyear=" + lblBudgetYear.Text + "&MidCat=0&HeadName=Budget Coments' title='Remarks' ><img src='../HRD/Images/icon_comment.gif' /></a></td>");
        sb.Append("<td style='font-size:10px;text-align:right;padding:4px;font-weight:bold;'>Gross Total(US$)</td>");
        dr3[0] = "Gross Total(US$)";
        GrossSum = 0;

        sb.Append("<td style='font-size:10px;text-align:right;padding:4px;font-weight:bold;'>" + FormatCurrency(ColumnSum) + "</td>");
        GrossSum += ColumnSum;
        dr3[0] = FormatCurrency(ColumnSum);
        
        //sb.Append("<td style='font-size:10px;text-align:right'>" + FormatCurrency(GrossSum) + "</td>");
        dr3[dt_Result.Columns.Count - 1] = FormatCurrency(GrossSum);
        sb.Append("</tr>");
        dt_Result.Rows.Add(dr3);
        sb.Append("</table>");
        //--------------
        lit1.Text = sb.ToString();
    }
    public string FormatCurrency(object InValue)
    {
        string StrIn = InValue.ToString();
        string OutValue = "";
        int Len = StrIn.Length;
        while (Len > 3)
        {
            if (OutValue.Trim() == "")
                OutValue = StrIn.Substring(Len - 3);
            else
                OutValue = StrIn.Substring(Len - 3) + "," + OutValue;

            StrIn = StrIn.Substring(0, Len - 3);
            Len = StrIn.Length;
        }
        OutValue = StrIn + "," + OutValue;
        if (OutValue.EndsWith(",")) { OutValue = OutValue.Substring(0, OutValue.Length - 1); }
        return OutValue;
    }
    public string GetColII(decimal Col1,decimal maxPer)
    {
        if (maxPer == 0)
        {
            return "0";
        }
        string val =Math.Round( ((Col1 / maxPer) * 12),2).ToString();
        return val;
    }
    public int GetVesselNo(string _VesselCode)
    {
        string sql = "Select VesselId As vESSELNO from Vessel with(nolock) where VesselCode='" + _VesselCode + "'";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
        if (dt != null)
        {
            if (dt.Rows.Count > 0)
            {
                return Common.CastAsInt32(dt.Rows[0][0]);
            }
        }
        return 0;

    }
    public string getCommentString(object comment)
    {
        string result = "";
        if (comment.ToString().Length > 25)
            result = "<span title='" + comment.ToString().Replace("'", "`") + "'>" + comment.ToString().Substring(0,25) + " ...</span>";
        else
            result = "<span title='" + comment + "'>" + comment + "</span>";

        return result;
    }
    protected void imgbtnPublish_Click(object sender, ImageClickEventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "pub", "window.open('Print2.aspx?Mode=PUB&VSL=" + ddlShip.SelectedValue + "');", true);
    }

    protected void ValidateCompanyforIndianFinanceYr(string CoCode)
    {

        IsIndianFinacialYr = 0;
        IsBudgetDistributeByDays = 0;
        string constr = ConfigurationManager.ConnectionStrings["eMANAGER"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Select Isnull(IsIndianFinacialYear,0) As IsIndianFinacialYear, Isnull(BudgetDistributebyDays,0) As BudgetDistributebyDays from AccountCompany with(nolock) where Company = @CoCode"))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@CoCode", CoCode.Trim().ToString());

                    cmd.Connection = con;
                    con.Open();
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        IsIndianFinacialYr = Convert.ToInt32(dt.Rows[0]["IsIndianFinacialYear"]);
                        IsBudgetDistributeByDays = Convert.ToInt32(dt.Rows[0]["BudgetDistributebyDays"]);
                    }

                    con.Close();
                }
            }


        }

        FinancialYr = "";
        string nextyr = "";
        int year = Convert.ToInt32(lblBudgetYear.Text);
        nextyr = Convert.ToString(year + 1);
        if (IsIndianFinacialYr == 1)
        {
            FinancialYr = Convert.ToString(year) + "-" + nextyr.Substring(nextyr.Length - 2);
        }
        else
        {
            FinancialYr = Convert.ToString(year);
        }
    }
}



