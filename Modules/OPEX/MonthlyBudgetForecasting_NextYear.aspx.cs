using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;

public partial class MonthlyBudgetForecasting_NextYear : System.Web.UI.Page
{
    // PAGE PROPERTIES ------------------
    AuthenticationManager authRecInv;
    public string CompanyCode
    {
        get
        {return ViewState["Comp"].ToString();}
        set
        {ViewState["Comp"] = value;}
    }    
    public string VesselCode
    {
        get
        { return ViewState["Vess"].ToString(); }
        set
        { ViewState["Vess"] = value; }
    }
    public string StartDate
    {
        get
        { return ViewState["SD"].ToString(); }
        set
        { ViewState["SD"] = value; }
    }
    public string EndDate
    {
        get
        { return ViewState["ED"].ToString(); }
        set
        { ViewState["ED"] = value; }
    }
    public int AccountNumber
    {
        get
        { return int.Parse("0"+ViewState["AccountNumber"].ToString()); }
        set
        { ViewState["AccountNumber"] = value; }
    }
    public int AcctID
    {
        get
        { return int.Parse("0" + ViewState["AcctID"].ToString()); }
        set
        { ViewState["AcctID"] = value; }
    }
    public int AccountID
    {
        get
        { return int.Parse("0" + ViewState["AccountID"].ToString()); }
        set
        { ViewState["AccountID"] = value; }
    }
    public int BYear
    {
        get
        { return int.Parse("0" + ViewState["Year"].ToString()); }
        set
        { ViewState["Year"] = value; }
    }
    public int BYearDays
    {
        get
        { return int.Parse("0" + ViewState["BYearDays"].ToString()); }
        set
        { ViewState["BYearDays"] = value; }
    }
    public decimal NewEstBudget
    {
        get
        { return Common.CastAsDecimal("0" + ViewState["Budget"].ToString()); }
        set
        { ViewState["Budget"] = value; }
    }
    public decimal AnnualAmount
    {
        get
        { return Common.CastAsDecimal("0" + ViewState["AnnualAmount"].ToString()); }
        set
        { ViewState["AnnualAmount"] = value; }
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
        { return int.Parse("0" + ViewState["TaskID"]); }
        set
        { ViewState["TaskID"] = value; }
    }

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
                ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "alert('You have not permissions to access this page.');window.close();", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "alert('Your session is expired.');window.close();", true);
        }

        #endregion ----------------------------------------
        
        lblMsgTrackingTask.Text = "";
        lblMsgExport.Text = "";
        if (!Page.IsPostBack)
        {
            // DATA FROM QUERY STRING ---------
            CompanyCode = Page.Request.QueryString["Co"].ToString();
            VesselCode = Page.Request.QueryString["Vess"].ToString();
            StartDate = Page.Request.QueryString["StartDate"].ToString();
            EndDate = Page.Request.QueryString["EndDate"].ToString();
            AccountNumber = int.Parse("0" + Page.Request.QueryString["AccountNumber"].ToString());
            AcctID = Common.CastAsInt32(Page.Request.QueryString["AcctID"]);
            AccountID = Common.CastAsInt32(Page.Request.QueryString["AccountID"]);
            BYear = Common.CastAsInt32(Page.Request.QueryString["year"]);
            NewEstBudget = Common.CastAsDecimal(Page.Request.QueryString["Budget"]);
            AnnualAmount = Common.CastAsDecimal(Page.Request.QueryString["AnnAmt"]);
            BYearDays = (Convert.ToDateTime(EndDate).Subtract(Convert.ToDateTime(StartDate))).Days + 1;
            MajCatID = Common.CastAsInt32(Page.Request.QueryString["MajCatID"]);
            MidCatID = Common.CastAsInt32(Page.Request.QueryString["MidCatID"]);
            lblfyear.Text = BYear.ToString();
            //litTotalAmount.Text = Common.CastAsDecimal(Page.Request.QueryString["Budget"]).ToString();

            // DATA FOR DISPLAY ONLY  ---------
            lblAccNum.Text = AccountNumber.ToString();
            lblAccName.Text = GetAccountName(AccountNumber);
            lblBudgetAmt.Text = NewEstBudget.ToString();

            lblComp.Text = CompanyCode ;
            lblVessel.Text = VesselCode;

            lblAnnBdg.Text = AnnualAmount.ToString();
            SetComments();
            hfSelMonth.Value = GetMonthFromDB().ToString();
            // CALL LOADING FUNCTION
            ValidateCompanyforIndianFinanceYr(CompanyCode);
            GetFinancialMonthYear(CompanyCode, BYear);
            BindGrid();
            BindTrackingTaskList();
            BindVessel();
        }
        if (IsBudgetLocked())
        {
            btnSave.Visible = false;
        }
        else
        {
            btnReAllocate.Visible = authRecInv.IsUpdate;
            btnSave.Visible = authRecInv.IsUpdate;
        }
    }
    // Event -----------------------------------------------------------------------
    protected void btnSave_OnClick(object sender, EventArgs e)
    {

	DateTime stdt=Convert.ToDateTime(StartDate);
	DateTime endt=Convert.ToDateTime(EndDate);
        //if (IsIndianFinacialYr == 1)
        //{
        //}
        //else
        //{ 
        //}
        if(stdt.ToString("dd-MMM").ToUpper()=="01-JAN" && endt.ToString("dd-MMM").ToUpper()=="31-DEC")
        if(Common.CastAsDecimal(lblBudgetAmt.Text) != Common.CastAsDecimal(lblTotBdgDB.Text))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", "alert('Error: Proposed annual budget and monthly distribution total is not matching.');", true);
            return;
        }

        Common.Set_Procedures("sp_NewPR_UpdateBudgetDetailsMonthWise_NextYear");
        Common.Set_ParameterLength(25);
        Common.Set_Parameters(
            new MyParameter("@CoCode", CompanyCode),
            new MyParameter("@AccountID", AccountID),
            new MyParameter("@Accountnumber", AccountNumber),
            new MyParameter("@AcctID",AcctID ),
            new MyParameter("@Year", BYear),

            new MyParameter("@totalBudget", lblTotBdgDB.Text),
            new MyParameter("@UpdatedBy", Session["UserName"].ToString()),
            new MyParameter("@YearDays", BYearDays),
            new MyParameter("@VessStart", StartDate),
            new MyParameter("@VessEnd", EndDate),
            new MyParameter("@ShipID", VesselCode),

            new MyParameter("@1stMonAmt", txtMonth1.Text),
            new MyParameter("@2ndMonAmt", txtMonth2.Text),
            new MyParameter("@3rdMonAmt", txtMonth3.Text),
            new MyParameter("@4thMonAmt", txtMonth4.Text),
            new MyParameter("@5thMonAmt", txtMonth5.Text),
            new MyParameter("@6thMonAmt", txtMonth6.Text),
            new MyParameter("@7thMonAmt", txtMonth7.Text),
            new MyParameter("@8thMonAmt", txtMonth8.Text),
            new MyParameter("@9thMonAmt", txtMonth9.Text),
            new MyParameter("@10thMonAmt", txtMonth10.Text),
            new MyParameter("@11thMonAmt", txtMonth11.Text),
            new MyParameter("@12thMonAmt", txtMonth12.Text),
            new MyParameter("@AnnAmt", lblAnnBdg.Text),
            new MyParameter("@BudgetComments", txtComments.Text) //txtComments.Text
            );
        DataSet Ds = new DataSet();
        Boolean res = false;
        res = Common.Execute_Procedures_IUD(Ds);
        if (res == true)
        {
            lblmsg.Text = "Record saved successfully.";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "dialog", "window.opener.RefereshPage();window.close();", true); 
        }
        else
        {
            lblmsg.Text = "Record could not saved."+Common.ErrMsg;
        }
    }
    protected void Amount_OnTextChanged(object sender, EventArgs e)
    {
        CalculateTotal(); 
    }
    protected void btnReload_OnClick(object sender, EventArgs e)
    {
        if (IsIndianFinacialYr == 1)
        {
            if (IsBudgetDistributeByDays == 1)
            {
                ResetNewValuesforIndianFinacialYearBudgetbyDays();
            }
            else
            {
                ResetNewValuesforIndianFinacialYearBudgetbyMonthly();
            }
        }
        else
        {
            if (IsBudgetDistributeByDays == 1)
            {
                ResetNewValuesBudgetbyDays();
            }
            else
            {
                ResetNewValuesBudgetbyMonthly();
            }
        }
    }
    // Function ------------------------------------------------------li---------------
    private void BindGrid()
    {
        string Financeyr = "";
        string nextyr = "";
        nextyr = Convert.ToString(BYear + 1);
        if (IsIndianFinacialYr == 1)
        {
            Financeyr = Convert.ToString(BYear) + "-" + nextyr.Substring(nextyr.Length - 2);
        }
        else
        {
            Financeyr = Convert.ToString(BYear);
        }
        DataTable DT = Common.Execute_Procedures_Select_ByQuery("SELECT FORECAST FROM dbo.tblSMDBudgetForecast v Inner Join AccountCompanyBudgetMonthyear a with(nolock) on v.CoCode = a.Cocode and v.Period = a.Month and v.Year = a.Year and v.CurFinYear = a.CurFinYear WHERE v.[CurFinYear]='" + Financeyr.ToString() + "' AND v.COCODE='" + CompanyCode + "' AND v.ACCTID=(SELECT '" + AccountNumber + "'  + REPLACE(STR(VesselId,4),' ','0') FROM Vessel with(nolock) WHERE VesselCode='" + VesselCode + "')"); 
        if (DT.Rows.Count > 0)
        {
            txtMonth1.Text = DT.Rows[0][0].ToString();
            txtMonth2.Text = DT.Rows[1][0].ToString();
            txtMonth3.Text = DT.Rows[2][0].ToString();
            txtMonth4.Text = DT.Rows[3][0].ToString();
            txtMonth5.Text = DT.Rows[4][0].ToString();
            txtMonth6.Text = DT.Rows[5][0].ToString();
            txtMonth7.Text = DT.Rows[6][0].ToString();
            txtMonth8.Text = DT.Rows[7][0].ToString();
            txtMonth9.Text = DT.Rows[8][0].ToString();
            txtMonth10.Text = DT.Rows[9][0].ToString();
            txtMonth11.Text = DT.Rows[10][0].ToString();
            txtMonth12.Text = DT.Rows[11][0].ToString();
        }

        CalculateTotal();
    }
    private int GetMonthFromDB()
    {
        int DBMonth = 0;
        string sql = "select top 1 forecast as actforecast,period,round(forecast,0)as forecast  from [dbo].tblSMDBudgetForecast  " +
                    " where CoCode='" + CompanyCode + "'  " +
                    " AND AccountID in (select AccountID from [dbo].sql_tblSMDPRAccounts where MajCatID=" + MajCatID + ") " +
                    " AND forecast>0 AND VESSNO=" + GetVesselNo(VesselCode) + " AND Year=" + (BYear - 1).ToString() + " order by period";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
        if (dt!= null)
        if (dt.Rows.Count > 0)
        {
            DBMonth = Common.CastAsInt32(dt.Rows[0]["period"]);
        }
        return DBMonth;
    }
    public void ResetNewValuesBudgetbyMonthly()
    {
        Decimal DBudget = 0;
        if (MidCatID == 6 || MidCatID == 25 || MidCatID == 26) // this case is when selected Dry Docking or Pre Delivery Expenses
        {
            int DBMonth = Common.CastAsInt32(hfSelMonth.Value);    
            if (DBMonth <= 0)
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "xx", "CheckMidCat();", true);
                dbAskMonth.Visible =true ;
                dvAskMonthBox.Visible = true; 
                return;
            }
            else
            {
                txtMonth1.Text = "0";
                txtMonth2.Text = "0";
                txtMonth3.Text = "0";
                txtMonth4.Text = "0";
                txtMonth5.Text = "0";
                txtMonth6.Text = "0";
                txtMonth7.Text = "0";
                txtMonth8.Text = "0";
                txtMonth9.Text = "0";
                txtMonth10.Text = "0";
                txtMonth11.Text = "0";
                txtMonth12.Text = "0";
                switch (Common.CastAsInt32(hfSelMonth.Value))
                {
                    case 1:
                        txtMonth1.Text = NewEstBudget.ToString();
                        break;
                    case 2:
                        txtMonth2.Text = NewEstBudget.ToString();
                        break;
                    case 3:
                        txtMonth3.Text = NewEstBudget.ToString();
                        break;
                    case 4:
                        txtMonth4.Text = NewEstBudget.ToString();
                        break;
                    case 5:
                        txtMonth5.Text = NewEstBudget.ToString();
                        break;
                    case 6:
                        txtMonth6.Text = NewEstBudget.ToString();
                        break;
                    case 7:
                        txtMonth7.Text = NewEstBudget.ToString();
                        break;
                    case 8:
                        txtMonth8.Text = NewEstBudget.ToString();
                        break;
                    case 9:
                        txtMonth9.Text = NewEstBudget.ToString();
                        break;
                    case 10:
                        txtMonth10.Text = NewEstBudget.ToString();
                        break;
                    case 11:
                        txtMonth11.Text = NewEstBudget.ToString();
                        break;
                    case 12:
                        txtMonth12.Text = NewEstBudget.ToString();
                        break;
                }
            }
        }
        else
        {
            DBudget = Math.Round(NewEstBudget / 12, 0);
            if (StartDate == "" && EndDate == "")
            {
                txtMonth1.Text = DBudget.ToString();
                txtMonth2.Text = DBudget.ToString();
                txtMonth3.Text = DBudget.ToString();
                txtMonth4.Text = DBudget.ToString();
                txtMonth5.Text = DBudget.ToString();
                txtMonth6.Text = DBudget.ToString();
                txtMonth7.Text = DBudget.ToString();
                txtMonth8.Text = DBudget.ToString();
                txtMonth9.Text = DBudget.ToString();
                txtMonth10.Text = DBudget.ToString();
                txtMonth11.Text = DBudget.ToString();
                txtMonth12.Text = DBudget.ToString();
            }
            else
            {
                int Month = Convert.ToDateTime(StartDate).Month;
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
                        int startDay = Convert.ToDateTime(StartDate).Day;
                        if (startDay == 1)
                        {
                            val = DBudget.ToString();
                        }
                        else
                        {
                            int Dateyear = Convert.ToDateTime(StartDate).Year;
                            int DateMonth = Convert.ToDateTime(StartDate).Month;
                            int DaysInMonth = DateTime.DaysInMonth(Dateyear, DateMonth);
                            int TotalBudgetDays = DaysInMonth - startDay + 1;
                            decimal PerDayBudget = DBudget / DaysInMonth;
                            val = Math.Round(Common.CastAsDecimal(PerDayBudget * TotalBudgetDays), 0).ToString();
                        }
                    }

                    switch (i)
                    {
                        case 1:
                            txtMonth1.Text = val.ToString();
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 2:
                            txtMonth2.Text = val.ToString();
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 3:
                            txtMonth3.Text = val.ToString();
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 4:
                            txtMonth4.Text = val.ToString();
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 5:
                            txtMonth5.Text = val.ToString();
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 6:
                            txtMonth6.Text = val.ToString();
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 7:
                            txtMonth7.Text = val.ToString();
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 8:
                            txtMonth8.Text = val.ToString();
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 9:
                            txtMonth9.Text = val.ToString();
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 10:
                            txtMonth10.Text = val.ToString();
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 11:
                            txtMonth11.Text = val.ToString();
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 12:
                            txtMonth12.Text = val.ToString();
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                    }
                }
            }
        }
        CalculateTotal();
    }

    public void ResetNewValuesforIndianFinacialYearBudgetbyMonthly()
    {
        Decimal DBudget = 0;
        if (MidCatID == 6 || MidCatID == 25 || MidCatID == 26) // this case is when selected Dry Docking or Pre Delivery Expenses
        {
            int DBMonth = Common.CastAsInt32(hfSelMonth.Value);
            if (DBMonth <= 0)
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "xx", "CheckMidCat();", true);
                dbAskMonth.Visible = true;
                dvAskMonthBox.Visible = true;
                return;
            }
            else
            {
                txtMonth1.Text = "0";
                txtMonth2.Text = "0";
                txtMonth3.Text = "0";
                txtMonth4.Text = "0";
                txtMonth5.Text = "0";
                txtMonth6.Text = "0";
                txtMonth7.Text = "0";
                txtMonth8.Text = "0";
                txtMonth9.Text = "0";
                txtMonth10.Text = "0";
                txtMonth11.Text = "0";
                txtMonth12.Text = "0";
                switch (Common.CastAsInt32(hfSelMonth.Value))
                {
                    case 1:
                        txtMonth1.Text = NewEstBudget.ToString();
                        break;
                    case 2:
                        txtMonth2.Text = NewEstBudget.ToString();
                        break;
                    case 3:
                        txtMonth3.Text = NewEstBudget.ToString();
                        break;
                    case 4:
                        txtMonth4.Text = NewEstBudget.ToString();
                        break;
                    case 5:
                        txtMonth5.Text = NewEstBudget.ToString();
                        break;
                    case 6:
                        txtMonth6.Text = NewEstBudget.ToString();
                        break;
                    case 7:
                        txtMonth7.Text = NewEstBudget.ToString();
                        break;
                    case 8:
                        txtMonth8.Text = NewEstBudget.ToString();
                        break;
                    case 9:
                        txtMonth9.Text = NewEstBudget.ToString();
                        break;
                    case 10:
                        txtMonth10.Text = NewEstBudget.ToString();
                        break;
                    case 11:
                        txtMonth11.Text = NewEstBudget.ToString();
                        break;
                    case 12:
                        txtMonth12.Text = NewEstBudget.ToString();
                        break;
                }
            }
        }
        else
        {
            DBudget = Math.Round(NewEstBudget / 12, 0);
            if (StartDate == "" && EndDate == "")
            {
                txtMonth1.Text = DBudget.ToString();
                txtMonth2.Text = DBudget.ToString();
                txtMonth3.Text = DBudget.ToString();
                txtMonth4.Text = DBudget.ToString();
                txtMonth5.Text = DBudget.ToString();
                txtMonth6.Text = DBudget.ToString();
                txtMonth7.Text = DBudget.ToString();
                txtMonth8.Text = DBudget.ToString();
                txtMonth9.Text = DBudget.ToString();
                txtMonth10.Text = DBudget.ToString();
                txtMonth11.Text = DBudget.ToString();
                txtMonth12.Text = DBudget.ToString();
            }
            else
            {
                int Month = Convert.ToDateTime(StartDate).Month;
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
                        int startDay = Convert.ToDateTime(StartDate).Day;
                        if (startDay == 1)
                        {
                            val = DBudget.ToString();
                        }
                        else
                        {
                            int Dateyear = Convert.ToDateTime(StartDate).Year;
                            int DateMonth = Convert.ToDateTime(StartDate).Month;
                            int DaysInMonth = DateTime.DaysInMonth(Dateyear, DateMonth);
                            int TotalBudgetDays = DaysInMonth - startDay + 1;
                            decimal PerDayBudget = DBudget / DaysInMonth;
                            val = Math.Round(Common.CastAsDecimal(PerDayBudget * TotalBudgetDays), 0).ToString();
                        }
                    }

                    switch (i)
                    {
                        case 1:
                            txtMonth1.Text = val.ToString();
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 2:
                            txtMonth2.Text = val.ToString();
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 3:
                            txtMonth3.Text = val.ToString();
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 4:
                            txtMonth4.Text = val.ToString();
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 5:
                            txtMonth5.Text = val.ToString();
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 6:
                            txtMonth6.Text = val.ToString();
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 7:
                            txtMonth7.Text = val.ToString();
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 8:
                            txtMonth8.Text = val.ToString();
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 9:
                            txtMonth9.Text = val.ToString();
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 10:
                            txtMonth10.Text = val.ToString();
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 11:
                            txtMonth11.Text = val.ToString();
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 12:
                            txtMonth12.Text = val.ToString();
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                    }
                }
            }
        }
        CalculateTotal();
    }

    public void ResetNewValuesBudgetbyDays()
    {
        Decimal DBudget = 0;

        if (MidCatID == 6 || (MidCatID == 25 || MidCatID == 26)) // this case is when selected Dry Docking or Pre Delivery Expenses
        {
            int DBMonth = Common.CastAsInt32(hfSelMonth.Value);
            if (DBMonth <= 0)
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "xx", "CheckMidCat();", true);
                dbAskMonth.Visible = true;
                dvAskMonthBox.Visible = true;
                return;
            }
            else
            {
                txtMonth1.Text = "0";
                txtMonth2.Text = "0";
                txtMonth3.Text = "0";
                txtMonth4.Text = "0";
                txtMonth5.Text = "0";
                txtMonth6.Text = "0";
                txtMonth7.Text = "0";
                txtMonth8.Text = "0";
                txtMonth9.Text = "0";
                txtMonth10.Text = "0";
                txtMonth11.Text = "0";
                txtMonth12.Text = "0";
                switch (Common.CastAsInt32(hfSelMonth.Value))
                {
                    case 1:
                        txtMonth1.Text = NewEstBudget.ToString();
                        break;
                    case 2:
                        txtMonth2.Text = NewEstBudget.ToString();
                        break;
                    case 3:
                        txtMonth3.Text = NewEstBudget.ToString();
                        break;
                    case 4:
                        txtMonth4.Text = NewEstBudget.ToString();
                        break;
                    case 5:
                        txtMonth5.Text = NewEstBudget.ToString();
                        break;
                    case 6:
                        txtMonth6.Text = NewEstBudget.ToString();
                        break;
                    case 7:
                        txtMonth7.Text = NewEstBudget.ToString();
                        break;
                    case 8:
                        txtMonth8.Text = NewEstBudget.ToString();
                        break;
                    case 9:
                        txtMonth9.Text = NewEstBudget.ToString();
                        break;
                    case 10:
                        txtMonth10.Text = NewEstBudget.ToString();
                        break;
                    case 11:
                        txtMonth11.Text = NewEstBudget.ToString();
                        break;
                    case 12:
                        txtMonth12.Text = NewEstBudget.ToString();
                        break;
                }
            }
        }
        else
        {

            DBudget = Math.Round(NewEstBudget / 12, 0);
            if (StartDate == "" && EndDate == "")
            {
                txtMonth1.Text = DBudget.ToString();
                txtMonth2.Text = DBudget.ToString();
                txtMonth3.Text = DBudget.ToString();
                txtMonth4.Text = DBudget.ToString();
                txtMonth5.Text = DBudget.ToString();
                txtMonth6.Text = DBudget.ToString();
                txtMonth7.Text = DBudget.ToString();
                txtMonth8.Text = DBudget.ToString();
                txtMonth9.Text = DBudget.ToString();
                txtMonth10.Text = DBudget.ToString();
                txtMonth11.Text = DBudget.ToString();
                txtMonth12.Text = DBudget.ToString();
            }
            else
            {
                int Month = Convert.ToDateTime(StartDate).Month;
                string val = "0.00";
                ViewState.Add("TotalBudget", "0");
                int inputYear = Convert.ToDateTime(StartDate).Year;
                int DaysinYear = DateTime.IsLeapYear(inputYear) ? 366 : 365;
                decimal PerDayBudget = NewEstBudget / DaysinYear;
                for (int i = 1; i <= 12; i++)
                {
                    if (i >= Month + 1)
                    {
                        // val = DBudget.ToString();
                        int Dateyear = Convert.ToDateTime(StartDate).Year;
                        // int DateMonth = Convert.ToDateTime(StartDate).Month;
                        int DateMonth = i;
                        int DaysInMonth = DateTime.DaysInMonth(Dateyear, DateMonth);
                        // int TotalBudgetDays = DaysInMonth - startDay + 1;
                        //  decimal PerDayBudget = DBudget / DaysInMonth;
                        val = Math.Round(Common.CastAsDecimal(PerDayBudget * DaysInMonth), 0).ToString();
                    }
                    else if (i == Month)
                    {
                        int startDay = Convert.ToDateTime(StartDate).Day;
                        if (startDay == 1)
                        {
                            //val = DBudget.ToString();
                            int Dateyear = Convert.ToDateTime(StartDate).Year;
                            int DateMonth = Convert.ToDateTime(StartDate).Month;
                            int DaysInMonth = DateTime.DaysInMonth(Dateyear, DateMonth);
                            int TotalBudgetDays = DaysInMonth - startDay + 1;
                            // decimal PerDayBudget = DBudget / DaysInMonth;
                            val = Math.Round(Common.CastAsDecimal(PerDayBudget * TotalBudgetDays), 0).ToString();
                        }
                        else
                        {
                            int Dateyear = Convert.ToDateTime(StartDate).Year;
                            int DateMonth = Convert.ToDateTime(StartDate).Month;
                            int DaysInMonth = DateTime.DaysInMonth(Dateyear, DateMonth);
                            int TotalBudgetDays = DaysInMonth - startDay + 1;
                            // decimal PerDayBudget = DBudget / DaysInMonth;
                            val = Math.Round(Common.CastAsDecimal(PerDayBudget * TotalBudgetDays), 0).ToString();
                        }
                    }

                    switch (i)
                    {
                        case 1:
                            txtMonth1.Text = val.ToString();
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 2:
                            txtMonth2.Text = val.ToString();
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 3:
                            txtMonth3.Text = val.ToString();
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 4:
                            txtMonth4.Text = val.ToString();
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 5:
                            txtMonth5.Text = val.ToString();
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 6:
                            txtMonth6.Text = val.ToString();
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 7:
                            txtMonth7.Text = val.ToString();
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 8:
                            txtMonth8.Text = val.ToString();
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 9:
                            txtMonth9.Text = val.ToString();
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 10:
                            txtMonth10.Text = val.ToString();
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 11:
                            txtMonth11.Text = val.ToString();
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 12:
                            txtMonth12.Text = val.ToString();
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                    }
                }
            }
        }
        CalculateTotal();
    }

    public void ResetNewValuesforIndianFinacialYearBudgetbyDays()
    {
        Decimal DBudget = 0;

        if (MidCatID == 6 || (MidCatID == 25 || MidCatID == 26)) // this case is when selected Dry Docking or Pre Delivery Expenses
        {
            int DBMonth = Common.CastAsInt32(hfSelMonth.Value);
            if (DBMonth <= 0)
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "xx", "CheckMidCat();", true);
                dbAskMonth.Visible = true;
                dvAskMonthBox.Visible = true;
                return;
            }
            else
            {
                txtMonth1.Text = "0";
                txtMonth2.Text = "0";
                txtMonth3.Text = "0";
                txtMonth4.Text = "0";
                txtMonth5.Text = "0";
                txtMonth6.Text = "0";
                txtMonth7.Text = "0";
                txtMonth8.Text = "0";
                txtMonth9.Text = "0";
                txtMonth10.Text = "0";
                txtMonth11.Text = "0";
                txtMonth12.Text = "0";
                switch (Common.CastAsInt32(hfSelMonth.Value))
                {
                    case 1:
                        txtMonth1.Text = NewEstBudget.ToString();
                        break;
                    case 2:
                        txtMonth2.Text = NewEstBudget.ToString();
                        break;
                    case 3:
                        txtMonth3.Text = NewEstBudget.ToString();
                        break;
                    case 4:
                        txtMonth4.Text = NewEstBudget.ToString();
                        break;
                    case 5:
                        txtMonth5.Text = NewEstBudget.ToString();
                        break;
                    case 6:
                        txtMonth6.Text = NewEstBudget.ToString();
                        break;
                    case 7:
                        txtMonth7.Text = NewEstBudget.ToString();
                        break;
                    case 8:
                        txtMonth8.Text = NewEstBudget.ToString();
                        break;
                    case 9:
                        txtMonth9.Text = NewEstBudget.ToString();
                        break;
                    case 10:
                        txtMonth10.Text = NewEstBudget.ToString();
                        break;
                    case 11:
                        txtMonth11.Text = NewEstBudget.ToString();
                        break;
                    case 12:
                        txtMonth12.Text = NewEstBudget.ToString();
                        break;
                }
            }
        }
        else
        {
            DBudget = Math.Round(NewEstBudget / 12, 0);
            if (StartDate == "" && EndDate == "")
            {
                txtMonth1.Text = DBudget.ToString();
                txtMonth2.Text = DBudget.ToString();
                txtMonth3.Text = DBudget.ToString();
                txtMonth4.Text = DBudget.ToString();
                txtMonth5.Text = DBudget.ToString();
                txtMonth6.Text = DBudget.ToString();
                txtMonth7.Text = DBudget.ToString();
                txtMonth8.Text = DBudget.ToString();
                txtMonth9.Text = DBudget.ToString();
                txtMonth10.Text = DBudget.ToString();
                txtMonth11.Text = DBudget.ToString();
                txtMonth12.Text = DBudget.ToString();
            }
            else
            {
                int Month = Convert.ToDateTime(StartDate).Month;
                // int year = Convert.ToDateTime(StartDate).Year;
                //  int EndMonth = Convert.ToDateTime(EndDate).Month;
                // int EndYear = Convert.ToDateTime(EndDate).Year;
                // int totalMonthinYr = 12;
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
                int inputYear = Convert.ToDateTime(StartDate).Year;
                int DaysinYear = DateTime.IsLeapYear(inputYear + 1) ? 366 : 365;
                decimal PerDayBudget = NewEstBudget / DaysinYear;
                int tempMonth = Convert.ToDateTime(StartDate).Month;
                int tempMonth2 = 0;
                for (int i = 1; i <= 12; i++)
                {
                    if (i >= Month + 1)
                    {
                        // val = DBudget.ToString();
                        int Dateyear = Convert.ToDateTime(StartDate).Year;
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
                        int startDay = Convert.ToDateTime(StartDate).Day;
                        if (startDay == 1)
                        {
                            //  val = DBudget.ToString();
                            int Dateyear = Convert.ToDateTime(StartDate).Year;
                            int DateMonth = Convert.ToDateTime(StartDate).Month;
                            int DaysInMonth = DateTime.DaysInMonth(Dateyear, DateMonth);
                            int TotalBudgetDays = DaysInMonth - startDay + 1;
                            val = Math.Round(Common.CastAsDecimal(PerDayBudget * TotalBudgetDays), 0).ToString();
                        }
                        else
                        {
                            int Dateyear = Convert.ToDateTime(StartDate).Year;
                            int DateMonth = Convert.ToDateTime(StartDate).Month;
                            int DaysInMonth = DateTime.DaysInMonth(Dateyear, DateMonth);
                            int TotalBudgetDays = DaysInMonth - startDay + 1;
                            //  decimal PerDayBudget = DBudget / DaysInMonth;
                            val = Math.Round(Common.CastAsDecimal(PerDayBudget * TotalBudgetDays), 0).ToString();
                        }
                    }

                    switch (i)
                    {
                        case 1:
                            txtMonth1.Text = val.ToString();
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 2:
                            txtMonth2.Text = val.ToString();
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 3:
                            txtMonth3.Text = val.ToString();
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 4:
                            txtMonth4.Text = val.ToString();
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 5:
                            txtMonth5.Text = val.ToString();
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 6:
                            txtMonth6.Text = val.ToString();
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 7:
                            txtMonth7.Text = val.ToString();
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 8:
                            txtMonth8.Text = val.ToString();
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 9:
                            txtMonth9.Text = val.ToString();
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 10:
                            txtMonth10.Text = val.ToString();
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 11:
                            txtMonth11.Text = val.ToString();
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 12:
                            txtMonth12.Text = val.ToString();
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                    }

                    // totalMonthinYr = totalMonthinYr - 1;
                }
            }
        }
        CalculateTotal();
    }
    private void CalculateTotal()
    {
        //==============
        lblTotBdgDB.Text = (Common.CastAsDecimal(txtMonth1.Text) + Common.CastAsDecimal(txtMonth2.Text) + Common.CastAsDecimal(txtMonth3.Text) + Common.CastAsDecimal(txtMonth4.Text) + Common.CastAsDecimal(txtMonth5.Text) + Common.CastAsDecimal(txtMonth6.Text) + Common.CastAsDecimal(txtMonth7.Text) + Common.CastAsDecimal(txtMonth8.Text) + Common.CastAsDecimal(txtMonth9.Text) + Common.CastAsDecimal(txtMonth10.Text) + Common.CastAsDecimal(txtMonth11.Text) + Common.CastAsDecimal(txtMonth12.Text)).ToString();
        if (NewEstBudget == Common.CastAsDecimal(lblTotBdgDB.Text))
        {
            trnewBdg.Attributes.Add("color", "green");
        }
        else
        {
            trnewBdg.Attributes.Add("class", "error");
        }

        lblAnnBdg.Text = NewEstBudget.ToString();
    }
    //----------
    public string GetAccountName(int AccountNumber)
    {
        string sql = "select accountName  from VW_sql_tblSMDPRAccounts where accountNumber=" + AccountNumber + "";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
        if (dt != null)
        {
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0][0].ToString();
            }
        }

        return "";
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
    public void SetComments()
    {
        string sql = "SELECT tblSMDBudgetForecastYear.*, Lk_tblSMDPRAccounts.AccountName, Lk_tblSMDPRVessels.ShipName " +
                    " FROM ([dbo].tblSMDBudgetForecastYear INNER JOIN VW_sql_tblSMDPRAccounts as Lk_tblSMDPRAccounts ON  " +
                    " tblSMDBudgetForecastYear.AccountID=Lk_tblSMDPRAccounts.AccountID) INNER JOIN VW_sql_tblSMDPRVessels as Lk_tblSMDPRVessels ON  " +
                    " tblSMDBudgetForecastYear.ShipID=Lk_tblSMDPRVessels.ShipID " +
                    " where tblSMDBudgetForecastYear.Year=" + (BYear - 1).ToString() + "  and tblSMDBudgetForecastYear.shipid='" + VesselCode + "' and tblSMDBudgetForecastYear.AccountID=" + AccountID + "";

        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
        if (dt != null)
        {
            if (dt.Rows.Count > 0)
            {
               txtComments.Text = dt.Rows[0]["yearComment"].ToString();
            }
        }
    }

    public Boolean IsBudgetLocked()
    {
        string sql = "select * from Add_tblBudgetLocking where Company='" + CompanyCode + "' and Vessel='" + VesselCode + "' and Year=" + BYear .ToString()+ "";
        DataTable DT = Common.Execute_Procedures_Select_ByQuery(sql);
        if (DT.Rows.Count > 0)
            return true;
        else
            return false;
    }
    protected void btnSet_Click(object sender, EventArgs e)
    {
        dbAskMonth.Visible = false ;
        dvAskMonthBox.Visible = false;
        if (IsIndianFinacialYr == 1)
        {
            int Month = Convert.ToInt32(txtMonth.Text);
            if (Month >= 4 && Month <= 12)
            {
                Month = Month - 3;
            }
            else
            {
                Month = Month + 9;
            }

            hfSelMonth.Value = Month.ToString();
            if (IsBudgetDistributeByDays == 1)
            {
                ResetNewValuesforIndianFinacialYearBudgetbyDays();
            }
            else
            {
                ResetNewValuesforIndianFinacialYearBudgetbyMonthly();
            }
        }
        else
        {
            hfSelMonth.Value = txtMonth.Text;
            if (IsBudgetDistributeByDays == 1)
            {
                ResetNewValuesBudgetbyDays();
            }
            else
            {
                ResetNewValuesBudgetbyMonthly();
            }
        }
    }


    // Tracking Task ---------------------------------------------------------------------
    protected void btnSaveTrackingTask_OnClick(object sender, EventArgs e)
    {
        if (txtTtAmount.Text.Trim() == "")
        {
            lblMsgTrackingTask.Text = "Please enter amount.";
            txtTtAmount.Focus();
            return;
        }
        if (txtTtDescription.Text.Trim() == "")
        {
            lblMsgTrackingTask.Text = "Please enter description.";
            txtTtDescription.Focus();
            return;
        }
        if (txtTtDescription.Text.Trim().Length >250)
        {
            lblMsgTrackingTask.Text = "Description should be within 250 character.";
            txtTtDescription.Focus();
            return;
        }
        if (ddlTaskType.SelectedIndex == 0)
        {
            lblMsgTrackingTask.Text = " Please select task type.";
            ddlTaskType.Focus();
            return;
        }

        Common.Set_Procedures("sp_IU_tbl_BudgetTracking");
        Common.Set_ParameterLength(21);
        Common.Set_Parameters(
            new MyParameter("@TaskID", TaskID),
            new MyParameter("@Company", CompanyCode),
            new MyParameter("@VesselCode", VesselCode),
            new MyParameter("@BudgetYear", BYear),
            new MyParameter("@AccountID", AccountID),
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
                BindTrackingTaskList();
                UpTask.Update();
                dvAddTrackingTask.Visible = false;
            }
            else
            {
                lblMsgTrackingTask.Text = "Total breakdown amount is not matching with annual budget (" + (-c) + "). It is not allowed.";
            }
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "dialog", "window.opener.RefereshPage();window.close();", true);
        }
        else
        {
            lblMsgTrackingTask.Text = "Record could not saved." + Common.ErrMsg;
        }

    }
    protected void btnAddTrackingTaskPopup_OnClick(object sender, EventArgs e)
    {
        dvAddTrackingTask.Visible = true;
    }
    protected void btnEditTrackingTask_OnClick(object sender, EventArgs e)
    {
        //ImageButton btn = (ImageButton)sender;
        //HiddenField hfTaskID = (HiddenField)btn.Parent.FindControl("hfTaskID");
        TaskID = Common.CastAsInt32(hfSelectedTaskID.Value);
        dvAddTrackingTask.Visible = true;
        ShowTrackingTaskForEditing();
    }
    
     protected void btnDeleteTrackingTask_OnClick(object sender, EventArgs e)
    {
        //ImageButton btn = (ImageButton)sender;
        //HiddenField hfTaskID = (HiddenField)btn.Parent.FindControl("hfTaskID");

        string sql = " delete from tbl_BudgetTracking where TaskID="+ hfSelectedTaskID.Value;
        DataTable DT = Common.Execute_Procedures_Select_ByQuery(sql);
        BindTrackingTaskList();
        Response.Redirect(Request.RawUrl);

    }
    protected void btnCloseAddTrackingTaskPopup_OnClick(object sender, EventArgs e)
    {
        dvAddTrackingTask.Visible = false;
        ClearTrackingControl();
        TaskID = 0;
        Response.Redirect(Request.RawUrl);
    }

    public void BindTrackingTaskList()
    {
        string sql = "select * from tbl_BudgetTracking where Company='" + CompanyCode + "' and VesselCode='" + VesselCode + "' and BudgetYear=" + BYear + " and AccountID=" + AccountID + "";
        DataTable DT = Common.Execute_Procedures_Select_ByQuery(sql);
        rptTrackingTaskList.DataSource = DT;
        rptTrackingTaskList.DataBind();
        lblTotalTaskAmount.Text= DT.Compute("Sum(Amount)", "").ToString();
        if (Common.CastAsInt32(DT.Compute("Sum(Amount)", "")) > Common.CastAsInt32(lblBudgetAmt.Text))
            lblTotalTaskAmount.ForeColor = System.Drawing.Color.Red;

    }
    public void ClearTrackingControl()
    {
        TaskID = 0;
        txtTtAmount.Text = "";
        txtTtDescription.Text = "";
        lblTaskModifiedByOn.Text = "";
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
        //ddlTaskType.SelectedIndex = 0;
    }
    public void ShowTrackingTaskForEditing()
    {
        string sql = "select * from tbl_BudgetTracking where TaskID="+TaskID.ToString() ;
        DataTable DT = Common.Execute_Procedures_Select_ByQuery(sql);
        if (DT.Rows.Count > 0)
        {
            DataRow dr = DT.Rows[0];
            txtTtAmount.Text = dr["Amount"].ToString();
            txtTtDescription.Text = dr["TaskDescription"].ToString();
            lblTaskModifiedByOn.Text = dr["ModifiedBy"].ToString()+" ( "+ Common.ToDateString(dr["ModifiedOn"])+" )";

            //chkTtJan.Checked = (dr["Jan"].ToString() == "True");
            //chkTtFeb.Checked = (dr["Feb"].ToString() == "True");
            //chkTtMar.Checked = (dr["Mar"].ToString() == "True");
            //chkTtApr.Checked = (dr["Apr"].ToString() == "True");
            //chkTtMay.Checked = (dr["May"].ToString() == "True");
            //chkTtJun.Checked = (dr["Jun"].ToString() == "True");
            //chkTtJul.Checked = (dr["Jul"].ToString() == "True");
            //chkTtAug.Checked = (dr["Aug"].ToString() == "True");
            //chkTtSep.Checked = (dr["Sep"].ToString() == "True");
            //chkTtOct.Checked = (dr["Oct"].ToString() == "True");
            //chkTtNov.Checked = (dr["Nov"].ToString() == "True");
            //chkTtDec.Checked = (dr["Dec"].ToString() == "True");
            ddlTaskType.SelectedValue = (dr["budgeted"].ToString() == "True")?"1":"2";
            ddlTaskType_OnSelectedIndexChanged(new object(), new EventArgs());
        }
    }

    protected void btnTemp_OnClick(object sender, EventArgs e)
    {
        btnEditTrackingTask.Visible = true;
        btnDeleteTrackingTask.Visible = true;
    }
    //--Export Popup------------------------------------------------------------------------

    protected void btnExportTaskPopup_OnClick(object sender, EventArgs e)
    {
        dvExportTask.Visible = true;
    }
    
    protected void btnCloseExportPopup_OnClick(object sender, EventArgs e)
    {
        dvExportTask.Visible = false;
    }
    protected void btnExport_OnClick(object sender, EventArgs e)
    {
        if (ddlVesselExport.SelectedIndex == 0)
        {
            lblMsgExport.Text = "Please select vessel";
            return;
        }
        Common.Set_Procedures("sp_ExportTask");
        Common.Set_ParameterLength(5);
        Common.Set_Parameters(
            new MyParameter("@fromVessel", ddlVesselExport.SelectedValue ),
            new MyParameter("@ToVessel", VesselCode),
            new MyParameter("@AcctID", AccountID),
            new MyParameter("@yr", BYear),
            new MyParameter("@ModifiedBy", Session["UserName"].ToString())           
            );
        DataSet Ds = new DataSet();
        Boolean res = false;
        res = Common.Execute_Procedures_IUD(Ds);
        if (res == true)
        {
            lblMsgExport.Text = "Record imported successfully.";
            BindTrackingTaskList();
            ddlVesselExport.SelectedIndex = 0;
            UpTask.Update();
        }
        else
        {
            lblMsgExport.Text = "Record could not be imported." + Common.ErrMsg;
        }
    }


    public void BindVessel()
    {
        string sql = "SELECT VW_sql_tblSMDPRVessels.ShipID, VW_sql_tblSMDPRVessels.Company, VW_sql_tblSMDPRVessels.ShipName, " +
                    " (VW_sql_tblSMDPRVessels.ShipID+' - '+VW_sql_tblSMDPRVessels.ShipName)as ShipNameCode" +
                    " FROM VW_sql_tblSMDPRVessels "+
                    " WHERE VW_sql_tblSMDPRVessels.ShipID<>'"+VesselCode+ "'  and VW_sql_tblSMDPRVessels.Active='A' and VesselNo in (Select VesselId from UserVesselRelation with(nolock) where Loginid = "+ Convert.ToInt32(Session["loginid"].ToString())+") ";
        DataTable DtVessel = Common.Execute_Procedures_Select_ByQuery(sql);
        if (DtVessel != null)
        {
            ddlVesselExport.DataSource = DtVessel;
            ddlVesselExport.DataTextField = "ShipNameCode";
            ddlVesselExport.DataValueField = "ShipID";
            ddlVesselExport.DataBind();
            ddlVesselExport.Items.Insert(0, new ListItem("<Select>", "0"));
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

    protected void GetFinancialMonthYear(string companycode, int year)
    {
        string sql = "EXEC GetFinancialMonthYearforCompany '" + companycode.ToString() + "'," + year.ToString() + "  ";
        DataTable DT = Common.Execute_Procedures_Select_ByQuery(sql);
        if (DT.Rows.Count > 0)
        {
            lblMonth1.Text = DT.Rows[0]["MonYear"].ToString();
            lblMonth2.Text = DT.Rows[1]["MonYear"].ToString();
            lblMonth3.Text = DT.Rows[2]["MonYear"].ToString();
            lblMonth4.Text = DT.Rows[3]["MonYear"].ToString();
            lblMonth5.Text = DT.Rows[4]["MonYear"].ToString();
            lblMonth6.Text = DT.Rows[5]["MonYear"].ToString();
            lblMonth7.Text = DT.Rows[6]["MonYear"].ToString();
            lblMonth8.Text = DT.Rows[7]["MonYear"].ToString();
            lblMonth9.Text = DT.Rows[8]["MonYear"].ToString();
            lblMonth10.Text = DT.Rows[9]["MonYear"].ToString();
            lblMonth11.Text = DT.Rows[10]["MonYear"].ToString();
            lblMonth12.Text = DT.Rows[11]["MonYear"].ToString();
        }
    }

    protected void ValidateCompanyforIndianFinanceYr(string companycode)
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
                    cmd.Parameters.AddWithValue("@CoCode", CompanyCode.Trim().ToString());

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
    }

}
