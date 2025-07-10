using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Configuration;

public partial class BudgetForecastingNextYear : System.Web.UI.Page
{
    public AuthenticationManager authRecInv;
    public string SessionDeleimeter = ConfigurationManager.AppSettings["SessionDelemeter"];
    DataSet DsValue;
    int d11, d22;

    public decimal dJan, dFeb, dmar, dApr, dMay, dJun, dJul, dAug, dSep, dOct, dnev, dDec;
    public void Manage_Menu()
    {
        AuthenticationManager auth = new AuthenticationManager(27, int.Parse(Session["loginid"].ToString()), ObjectType.Module);
        trCurrBudget.Visible = auth.IsView;
        auth = new AuthenticationManager(28, int.Parse(Session["loginid"].ToString()), ObjectType.Module);
        trAnalysis.Visible = auth.IsView;
        auth = new AuthenticationManager(29, int.Parse(Session["loginid"].ToString()), ObjectType.Module);
        trBudgetForecast.Visible = auth.IsView;
        auth = new AuthenticationManager(30, int.Parse(Session["loginid"].ToString()), ObjectType.Module);
        trPublish.Visible = auth.IsView;
    }
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
                Response.Redirect("~/NoPermissionBudget.aspx", false);
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("~/NoPermissionBudget.aspx?Message=" + ex.Message);
        }

        #endregion ----------------------------------------
        lblmsg.Text = "";
        if (!IsPostBack)
        {
            Manage_Menu();
            lblBudgetYear.Text = Convert.ToString(System.DateTime.Now.Year+1);  //$$$$ remove - 1
            lblYr.Text = " " + System.DateTime.Now.Year.ToString();
            lblYr1.Text = " " + System.DateTime.Now.Year.ToString();
            lblYr3.Text = " " + System.DateTime.Now.Year.ToString();

            lblYr1_.Text = Convert.ToString(System.DateTime.Now.Year + 1);
            
            lblYrNext.Text = " " + lblBudgetYear.Text;

            lblYrr11.Text = Convert.ToString(System.DateTime.Now.Year);
            lblYrr12.Text = Convert.ToString(System.DateTime.Now.Year);
            lblYrr13.Text = Convert.ToString(System.DateTime.Now.Year);

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
                DataTable dt=Common.Execute_Procedures_Select_ByQuery("select company from VW_sql_tblSMDPRVessels where ShipId='" + Request.QueryString["Vsl"] + "'");
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
        imgApprove.Visible = authRecInv.IsVerify;
        imgbtnPublish.Visible = authRecInv.IsVerify2;  
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
            dCYAmt= Common.CastAsDecimal( ResetNewValues(Common.CastAsDecimal(txtAnnAmt.Text)));
            HiddenField hfAccID = (HiddenField)RptItm.FindControl("hfAccID");
            //-------------------------------------------------------------------------------------
            //Details Data
            HiddenField hfAccountID = (HiddenField)RptItm.FindControl("hfAccountID");
            HiddenField hfAccountNumber = (HiddenField)RptItm.FindControl("hfAccountNumber");

            string MonthlyBDG = dJan.ToString() + "," + dFeb.ToString() + "," + dmar.ToString() + "," + dApr.ToString() + "," + dMay.ToString() + "," + dJun.ToString() + "," + dJul.ToString() + "," + dAug.ToString() + "," + dSep.ToString() + "," + dOct.ToString() + "," + dnev.ToString() + "," + dDec.ToString();
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
                   "where CoCode='" + ddlCompany.SelectedValue + "' AND Year=" + (Common.CastAsInt32(lblBudgetYear.Text) - 1).ToString() + " And ShipID='" + ddlShip.SelectedValue + "'; select 'True' ";
        string SQL1 = "UPDATE [dbo].tblSMDBudgetForecast set Budget=ForeCast,ApprovedBy='" + Session["FullName"].ToString() + "',ApprovedOn=getdate() " +
                   "where CoCode='" + ddlCompany.SelectedValue + "' AND Year=" + (Common.CastAsInt32(lblBudgetYear.Text) - 1).ToString() + " And RIGHT(AcctID,4)=(Select REPLACE(STR(VesselId,4),' ','0') from Vessel with(nolock) where VesselCode='" + ddlShip.SelectedValue + "'); select 'True' ";
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
    public string ResetNewValues(decimal AnnualAmt)
    {
        Decimal DBudget = 0;
        if (ddlBudgetType.SelectedValue == "6" || ddlBudgetType.SelectedValue == "25") // this case is when selected Dry Docking or Pre Delivery Expenses
        {
            int DBMonth = Common.CastAsInt32(GetMonthFromDB().ToString());
            if (DBMonth <= 0)
            {
                dJan = AnnualAmt;
                dFeb = 0;
                dmar = 0;
                dApr = 0;
                dMay = 0;
                dJun = 0;
                dJul = 0;
                dAug = 0;
                dSep = 0;
                dAug = 0;
                dnev = 0;
                dDec = 0;
                return AnnualAmt.ToString();

            }
            else
            {
                dJan = 0;
                dFeb = 0;
                dmar = 0;
                dApr = 0;
                dMay = 0;
                dJun = 0;
                dJul = 0;
                dAug = 0;
                dSep = 0;
                dOct = 0;
                dnev = 0;
                dDec = 0;
                switch (Common.CastAsInt32(DBMonth))
                {
                    case 1:
                        dJan = AnnualAmt;
                        break;
                    case 2:
                        dFeb = AnnualAmt;
                        break;
                    case 3:
                        dmar = AnnualAmt;
                        break;
                    case 4:
                        dApr = AnnualAmt;
                        break;
                    case 5:
                        dMay = AnnualAmt;
                        break;
                    case 6:
                        dJun = AnnualAmt;
                        break;
                    case 7:
                        dJul = AnnualAmt;
                        break;
                    case 8:
                        dAug = AnnualAmt;
                        break;
                    case 9:
                        dSep = AnnualAmt;
                        break;
                    case 10:
                        dOct = AnnualAmt;
                        break;
                    case 11:
                        dnev = AnnualAmt;
                        break;
                    case 12:
                        dDec = AnnualAmt;
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
                dJan = DBudget;
                dFeb = DBudget;
                dmar = DBudget;
                dApr = DBudget;
                dMay = DBudget;
                dJun = DBudget;
                dJul = DBudget;
                dAug = DBudget;
                dSep = DBudget;
                dOct = DBudget;
                dnev = DBudget;
                dDec = DBudget;
                string CalculatedBudget = (dJan + dFeb + dmar + dApr + dMay + dJun + dJul + dAug + dSep + dOct + dnev + dDec).ToString();
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
                            dJan = Common.CastAsDecimal(val);
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 2:
                            dFeb = Common.CastAsDecimal(val);
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 3:
                            dmar = Common.CastAsDecimal(val);
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 4:
                            dApr = Common.CastAsDecimal(val);
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 5:
                            dMay = Common.CastAsDecimal(val);
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 6:
                            dJun = Common.CastAsDecimal(val);
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 7:
                            dJul = Common.CastAsDecimal(val);
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 8:
                            dAug = Common.CastAsDecimal(val);
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 9:
                            dSep = Common.CastAsDecimal(val);
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 10:
                            dOct = Common.CastAsDecimal(val);
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 11:
                            dnev = Common.CastAsDecimal(val);
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 12:
                            dDec = Common.CastAsDecimal(val);
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                    }
                }
                //CalculateTotal();
                string CalculatedBudget = (dJan + dFeb + dmar + dApr + dMay + dJun + dJul + dAug + dSep + dOct + dnev + dDec).ToString();
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
            usecalc = (usecalc /d11 ) * d22;
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
                    " WHERE (((VW_sql_tblSMDPRVessels.Company)='" + ddlCompany.SelectedValue + "')) ";
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
        string sql = "select * from [dbo].tblAccountsMajor where MajSeqNo in(25,30,100,300,400,450,500)";
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
        string sql = "SELECT v_BudgetForecastYear.ApprovedBy,replace(convert(varchar(15), v_BudgetForecastYear.ApprovedOn,106),' ','-')as ApprovedOn,v_BudgetForecastYear.ImportedBy,replace(convert(varchar(15), v_BudgetForecastYear.ImportedOn,106),' ','-')as ImportedOn,v_BudgetForecastYear.UpdatedBy, " +
                    " replace(convert(varchar(15), v_BudgetForecastYear.UpdatedOn,106),' ','-')as  UpdatedOn, "+
                    " v_BudgetForecastYear.AccountID,v_BudgetForecastYear.AccountNumber,v_BudgetForecastYear.AcctID,v_BudgetForecastYear.MajCatID,v_BudgetForecastYear.mincatid, "+
                    " v_BudgetForecastYear.[AccountName] AS AccountName1,round(isnull(v_BudgetForecastYear.Forecast,0),0) as Forecast,replace( convert(varchar(100), "+
                    " v_BudgetForecastYear.VessStart,106),' ','-')as  VessStart,replace( convert(varchar(100), v_BudgetForecastYear.VessEnd,106),' ','-') as VessEnd, "+
                    " v_BudgetForecastYear.YearDays,  (select midcat from dbo.tblaccountsmid G2 where G2.midcatid=v_BudgetForecastYear.midcatid) as Group1, "+
                    " v_BudgetForecastYear.midcatid,  (select minorcat from dbo.tblaccountsminor G3 where G3.mincatid=v_BudgetForecastYear.mincatid) as Group2, "+
                    " v_BudgetForecastYear.AccountNumber, "+
                    " AnnAmt=isnull((select Amount from Add_v_BudgetForecastYear Addt where Addt.cocode=v_BudgetForecastYear.cocode and Addt.AcctId=v_BudgetForecastYear.AcctId and Addt.Byear=" + lblBudgetYear.Text + "),0)  , " +
                    " ForeCastComment=isnull((select YearComment from [dbo].v_BudgetForecastYear V1 where V1.CoCode='" + ddlCompany.SelectedValue + "' AND V1.Vess='" + ddlShip.SelectedValue + "' and V1.AcctId=v_BudgetForecastYear.AcctId and V1.Year=" + (Common.CastAsInt32(lblBudgetYear.Text) - 1).ToString() + "),'')  , " +
                    " AnnAmtCurrentYear=isnull((select Amount from Add_v_BudgetForecastYear Addt where Addt.cocode=v_BudgetForecastYear.cocode and Addt.AcctId=v_BudgetForecastYear.AcctId and Addt.Byear=2011),0), "+
                    " (SELECT  Max(tblPeriodMaint.rptPeriod) AS maxPer FROM [dbo].tblPeriodMaint WHERE (((tblPeriodMaint.perClosed)=1) AND ((tblPeriodMaint.rptYear)>=" + (Common.CastAsInt32(lblBudgetYear.Text)-1).ToString() + ")) and cocode='0') as maxPer, " +
                    " isnull(( SELECT round(isnull(v_BudgetForecastYearNext.Forecast,0),0) as Forecast FROM  [dbo].v_BudgetForecastYear as  v_BudgetForecastYearNext INNER JOIN (select result as CY from CSVToTablestr('" + (Common.CastAsInt32(lblBudgetYear.Text) - 1).ToString() + "',','))tempYear ON v_BudgetForecastYearNext.Year = tempYear.CY WHERE v_BudgetForecastYearNext.CoCode='" + ddlCompany.SelectedValue + "' AND v_BudgetForecastYearNext.Vess='" + ddlShip.SelectedValue + "' and v_BudgetForecastYearNext.accountID=v_BudgetForecastYear.accountID),0)as ForecastNext," +
                    " round(isnull(v_BudgetForecastYear.Forecast,0),0) as Budget, "+
                    " isnull(v_BudgetForecastYear.Actual,0)as Actual " +
                    " ,0 as NewActualAmount " +
                    " ,0 as NewActualCal " +
                    " ,(case when month(v_BudgetForecastYear.VessStart)>1 or day(v_BudgetForecastYear.VessStart) >1 then  "+
                      " cast(((v_BudgetForecastYear.Budget/datediff(day,v_BudgetForecastYear.VessStart,v_BudgetForecastYear.VessEnd))*365) as dec(12,2))"+
	                    " else isnull(v_BudgetForecastYear.Budget ,0)end) as NewBudgetCal  "+

                    " FROM  [dbo].v_BudgetForecastYear as  v_BudgetForecastYear   "+
                    " INNER JOIN (select result as CY from CSVToTablestr('" + (Common.CastAsInt32(lblBudgetYear.Text) - 2).ToString() + "',','))tempYear ON v_BudgetForecastYear.Year = tempYear.CY  " +
                    " WHERE  v_BudgetForecastYear.CoCode='"+ddlCompany.SelectedValue+"' AND v_BudgetForecastYear.Vess='"+ddlShip.SelectedValue+"'  "+
                    " ORDER BY v_BudgetForecastYear.MajSeqNo, v_BudgetForecastYear.MidSeqNo, v_BudgetForecastYear.MinSeqNo, v_BudgetForecastYear.AccountNumber";

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
        //              " cast(((v_BudgetForecastYear.Budget/datediff(day,v_BudgetForecastYear.VessStart,v_BudgetForecastYear.VessEnd))*365) as dec(12,2))" +
        //                " else isnull(v_BudgetForecastYear.Budget ,0)end) as NewBudgetCal  " +

        //            " FROM  [dbo].v_BudgetForecastYear as  v_BudgetForecastYear   " +
        //            " INNER JOIN (select result as CY from CSVToTablestr('" + (Common.CastAsInt32(lblBudgetYear.Text) - 2).ToString() + "',','))tempYear ON v_BudgetForecastYear.Year = tempYear.CY  " +

        //            " left join (SELECT * FROM DBO.getConsumedByAccountId('" + ddlCompany.SelectedValue + "','" + ddlShip.SelectedValue + "','12/31/" + (Common.CastAsInt32(lblBudgetYear.Text) - 1).ToString() + "')) TblAmt on TblAmt.accountID=v_BudgetForecastYear.accountID " +

        //            " WHERE  v_BudgetForecastYear.CoCode='" + ddlCompany.SelectedValue + "' AND v_BudgetForecastYear.Vess='" + ddlShip.SelectedValue + "'  " +
        //            " ORDER BY v_BudgetForecastYear.MajSeqNo, v_BudgetForecastYear.MidSeqNo, v_BudgetForecastYear.MinSeqNo, v_BudgetForecastYear.AccountNumber";

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

                    string Qry = "select YearDays,approvedby,replace(convert(varchar,approvedon,106),' ','-') as ApprovedOn,UpdatedBy, " +
                               "Importedby,replace(convert(varchar,ImportedOn,106),' ','-') as ImportedOn,replace(convert(varchar,UpdatedOn,106),' ','-') as UpdatedOn," +
                               "replace(convert(varchar,vessstart,106),' ','-') as vessstart,replace(convert(varchar,vessend,106),' ','-') as vessend " +
                               "from dbo.tblsmdbudgetforecastyear where cocode='" + ddlCompany.SelectedValue + "' and shipid='" + ddlShip.SelectedValue + "' and [year]=" + (Common.CastAsInt32(lblBudgetYear.Text) - 1).ToString();
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
                        DateTime d1 = DateTime.Parse("01-Jan-" + lblBudgetYear.Text);
                        DateTime d2 = DateTime.Parse("31-Dec-" + lblBudgetYear.Text);

                        txtStartDate.Text = d1.ToString("dd-MMM-yyyy");//DtRpt.Rows[0]["VessStart"].ToString();
                        txtEndDate.Text = d2.ToString("dd-MMM-yyyy");//DtRpt.Rows[0]["VessEnd"].ToString();
                        lblDays.Text = d2.Subtract(d1).Days.ToString();//DtRpt.Rows[0]["YearDays"].ToString();

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
            }
            catch (Exception ex)
            {
                lblmsg.Text = "Unable to show data. : " + ex.Message;
            }
        }
        else
        {
            lblmsg.Text = "No data found for this vessel. : " + Common.ErrMsg;
        }
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
}



