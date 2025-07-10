using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Configuration;
using System.Text;
using System.Data.SqlClient;
using System.Runtime.InteropServices.ComTypes;

public partial class UpdateBudget_CY : System.Web.UI.Page
{
    public AuthenticationManager authRecInv;
    public string SessionDeleimeter = ConfigurationManager.AppSettings["SessionDelemeter"];
    DataSet DsValue;
    public decimal Month1, Month2, Month3, Month4, Month5, Month6, Month7, Month8, Month9, Month10, Month11, Month12;

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

    public string PreFinancialYr
    {
        get
        { return ViewState["PreFinancialYr"].ToString(); }
        set
        { ViewState["PreFinancialYr"] = value; }
    }
    public void Manage_Menu()
    {
        AuthenticationManager auth = new AuthenticationManager(5, int.Parse(Session["loginid"].ToString()), ObjectType.Module);
        //trCurrBudget.Visible = auth.IsView;
        //auth = new AuthenticationManager(28, int.Parse(Session["loginid"].ToString()), ObjectType.Module);
        //trAnalysis.Visible = auth.IsView;
        //auth = new AuthenticationManager(29, int.Parse(Session["loginid"].ToString()), ObjectType.Module);
        //trBudgetForecast.Visible = auth.IsView;
        //auth = new AuthenticationManager(30, int.Parse(Session["loginid"].ToString()), ObjectType.Module);
        //trPublish.Visible = auth.IsView;
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
            Manage_Menu();
            lblBudgetYear.Text = Convert.ToString(System.DateTime.Now.Year);  //$$$$ remove - 1
            FinancialYr = lblBudgetYear.Text;
            hdnCurrFinYr.Value = FinancialYr;
            PreFinancialYr = Convert.ToString(Convert.ToInt32(lblBudgetYear.Text) - 1);
            hdnPreFinYr.Value = PreFinancialYr;
            BindCompany();
            BindBudgetType();
            BindMidCategory();
            if (Request.QueryString["VSL"] != null)
            {
                string VSL = Request.QueryString["VSL"];
                DataTable dt = Common.Execute_Procedures_Select_ByQuery("Select AccontCompany As Company from Vessel with(nolock)  WHERE VesselCode='" + VSL + "' and VesselId in (Select VesselId from UserVesselRelation with(nolock) where Loginid = "+ Convert.ToInt32(Session["loginid"].ToString())+")");
                if (dt.Rows.Count > 0)
                {
                    ddlCompany.SelectedValue = dt.Rows[0][0].ToString();
                    ddlCompany.Enabled = false;
  
                    ddlCompany_OnSelectedIndexChanged(sender, e); 
                    ddlShip.SelectedValue = VSL;
                    ddlShip.Enabled = false;
                }
            }
            BindRepeater();
        }

        if (IsBudgetLocked())
        {
            imgLockBudget.Visible = false;
            //imgImport.Visible = false;
            imgSave.Visible = false;
	    imgbtnPublish.Visible = false;
        }
        else
        {
            imgLockBudget.Visible = authRecInv.IsVerify2;
            //imgImport.Visible = authRecInv.IsVerify;
            imgSave.Visible = false;
	    imgbtnPublish.Visible =authRecInv.IsVerify2;
        }

        //imgImport.Visible=(Session["loginid"].ToString()=="1");
        //imgLockBudget.Visible=imgImport.Visible;
        imgLockBudget.Visible = (Session["loginid"].ToString() == "1");
        Print.Visible = authRecInv.IsPrint; 
    }
    // Event ----------------------------------------------------------------
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
        BindMidCategory();
        BindRepeater();
        if (ddlBudgetType.SelectedValue == "14" || ddlBudgetType.SelectedValue == "16")
        {
            imgSave.Visible = false;
        }
        else
        {
            imgSave.Visible = true;
        }

    }
    // Button
    //protected void imgImport_OnClick(object sender, EventArgs e)
    //{
    //    if (ddlCompany.SelectedIndex == 0)
    //    {
    //        lblmsg.Text = "Select the company.";
    //        ddlCompany.Focus();
    //        return;
    //    }
    //    if (ddlShip.SelectedIndex == 0)
    //    {
    //        lblmsg.Text = "Select the vessel.";
    //        ddlShip.Focus();
    //        return;
    //    }

    //    string sql = "dbo.SP_NEW_ImportBudget '" + ddlCompany.SelectedValue + "','" + ddlShip.SelectedValue + "'," + lblBudgetYear.Text + ",'" + Session["FullName"].ToString() + "'";
    //    try
    //    {
    //        DataTable dt=Common.Execute_Procedures_Select_ByQuery(sql);
    //        string recds = "0";
    //        if (dt != null)
    //            if (dt.Rows.Count > 0)
    //            {
    //                recds = dt.Rows[0][0].ToString();  
    //            }
    //        lblmsg.Text ="[ " + recds +  " ] records updated.";
    //    }
    //    catch (Exception ex) {
    //        lblmsg.Text = "Unable to Import Budget : " + ex.Message ;
    //    } 
    //}
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
        DataSet Ds =new DataSet();
        Boolean res;
        res = false;
        TimeSpan TS = Convert.ToDateTime(txtEndDate.Text).Subtract(Convert.ToDateTime(txtStartDate.Text));
        for(int i=0;i<=rptBudget.Items.Count -1;i++ )
        {
            RepeaterItem RptItm = rptBudget.Items[i];  
            //Master Data
            TextBox txtAnnAmt=(TextBox)RptItm.FindControl("txtAnnAmt");
            //  get monthly values
            decimal dCYAmt;
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
            //decimal dCYAmt = dJan  +dFeb+ dmar+ dApr+ dMay+ dJun+ dJul+ dAug+ dSep+ dOct+ dnev+ dDec;
            string MonthlyBDG = Month1.ToString() + "," + Month2.ToString() + "," + Month3.ToString() + "," + Month4.ToString() + "," + Month5.ToString() + "," + Month6.ToString() + "," + Month7.ToString() + "," + Month8.ToString() + "," + Month9.ToString() + "," + Month10.ToString() + "," + Month11.ToString() + "," + Month12.ToString();
            //-------------------------------------------------------------------------------------
            Common.Set_Procedures("sp_NewPR_UpdateBudgetDetails");
            Common.Set_ParameterLength(13);
            Common.Set_Parameters(
                                new MyParameter("@AcctID", hfAccID.Value),
                                new MyParameter("@VessStart",txtStartDate.Text),
                                new MyParameter("@VessEnd",txtEndDate.Text),
                                new MyParameter("@Forecast", txtAnnAmt.Text),
                                new MyParameter("@CYAmt", dCYAmt),                                
                                new MyParameter("@CoCode",ddlCompany.SelectedValue),
                                new MyParameter("@ShipID",ddlShip.SelectedValue),
                                new MyParameter("@Year", lblBudgetYear.Text),        // $$$$ what year would be here --------------
                                new MyParameter("@YearDays", TS.Days+1),
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
        {
            BindRepeater();
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
    protected void LockBudget_OnClick(object sender, EventArgs e)
    {
        Common.Set_Procedures("sp_NewPR_LockBudget");
        Common.Set_ParameterLength(5);
        Common.Set_Parameters(
                        new MyParameter("@Company", ddlCompany.SelectedValue),
                        new MyParameter("@Vessel", ddlShip.SelectedValue),
                        new MyParameter("@Year",lblBudgetYear.Text),
                        new MyParameter("@LockedBy", Session["FullName"].ToString()),
                        new MyParameter("@CurFinYear", FinancialYr)
            );
        DataSet ds = null;
        ds = Common.Execute_Procedures_Select();
        if (ds != null)
        {
            lblmsg.Text = "Saved successfully.";
            imgLockBudget.Visible = false;
           // imgImport.Visible = false;
            imgSave.Visible = false;
        }
        else
        {
            lblmsg.Text = "could not save.";
        }
    }
    // Function ----------------------------------------------------------------
    public void BindCompany()
    {
        string sql = "SELECT VW_sql_tblSMDPRCompany.Company, VW_sql_tblSMDPRCompany.ReportCo "+
            " ,(VW_sql_tblSMDPRCompany.Company + '-' + VW_sql_tblSMDPRCompany.[Company Name]) as CompName" +
        " FROM VW_sql_tblSMDPRCompany WHERE (((VW_sql_tblSMDPRCompany.InAccts)=1)) and (((VW_sql_tblSMDPRCompany.Active)='Y'))";
        DataTable DtCompany = Common.Execute_Procedures_Select_ByQuery(sql);
        if (DtCompany != null)
        {
            ddlCompany.DataSource = DtCompany;
            ddlCompany.DataTextField = "CompName";
            ddlCompany.DataValueField = "Company";
            ddlCompany.DataBind();
            ddlCompany.Items.Insert(0, new ListItem("< Select Company >", "0"));
            ddlCompany.SelectedIndex = 0;
            BindVessel();
        }
    }
    public void BindVessel()
    {
        string sql = "SELECT VW_sql_tblSMDPRVessels.ShipID, VW_sql_tblSMDPRVessels.Company, VW_sql_tblSMDPRVessels.ShipName, " +
                    " (VW_sql_tblSMDPRVessels.ShipID+' - '+VW_sql_tblSMDPRVessels.ShipName)as ShipNameCode" +
                    " FROM VW_sql_tblSMDPRVessels " +
                    " WHERE VW_sql_tblSMDPRVessels.Company='"+ddlCompany.SelectedValue+ "' AND Active = 'A' AND VesselNo in (Select VesselId from UserVesselRelation with(nolock) where Loginid = " + Convert.ToInt32(Session["loginid"].ToString())+") ";
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
            ddlBudgetType.Items.Insert(0, new ListItem("< All >", "0"));

        }
    }
    public void BindMidCategory()
    {
        string str = string.Empty;
        if (ddlBudgetType.SelectedIndex > 0)
        {
            str = "Select mid.MidCatID,mid.MidCat from tblAccountsMid mid with(nolock) Inner join sql_tblSMDPRAccounts acc with(nolock) on mid.MidCatID = acc.MidCatID where acc.MajCatID = "+Convert.ToInt32(ddlBudgetType.SelectedValue)+ " and acc.Active = 'Y' Group by mid.MidCatID,mid.MidCat  order by Midcat asc";
        }
        else
        {
            str = "Select mid.MidCatID,mid.MidCat from tblAccountsMid mid with(nolock) Inner join sql_tblSMDPRAccounts acc with(nolock) on mid.MidCatID = acc.MidCatID where  acc.Active = 'Y'  Group by mid.MidCatID,mid.MidCat  order by Midcat asc";
        }
         
        DataTable dtMinCat = Common.Execute_Procedures_Select_ByQuery(str);
        if (dtMinCat != null)
        {
            ddlMidCategory.DataSource = dtMinCat;
            ddlMidCategory.DataTextField = "MidCat";
            ddlMidCategory.DataValueField = "MidCatid";
            ddlMidCategory.DataBind();
            ddlMidCategory.Items.Insert(0, new ListItem("< All >", "0"));
        }
    }
    public void BindRepeater()
    {
        string SelComp, SelVess, SelYear, SelYearNext;
        SelComp = ddlCompany.SelectedValue;
        SelVess = ddlShip.SelectedValue;
        SelYear = lblBudgetYear.Text.ToString();
        SelYearNext = lblBudgetYear.Text.ToString();

	    string VessNo = "";
        DataTable DtVessNo = Common.Execute_Procedures_Select_ByQuery("select vesselno from dbo.VW_sql_tblSMDPRVessels where shipid='" + ddlShip.SelectedValue + "' and VesselNo in (Select VesselId from UserVesselRelation with(nolock) where Loginid = " + Convert.ToInt32(Session["loginid"].ToString())+")");
        if (DtVessNo.Rows.Count > 0)
        {
            VessNo = DtVessNo.Rows[0][0].ToString();   
        }

        //string sql = "SELECT v_BudgetForecastYear.YearComment,v_BudgetForecastYear.ImportedBy,replace(convert(varchar(15), v_BudgetForecastYear.ImportedOn,106),' ','-')as ImportedOn,v_BudgetForecastYear.UpdatedBy,replace(convert(varchar(15), v_BudgetForecastYear.UpdatedOn,106),' ','-')as  UpdatedOn,v_BudgetForecastYear.Budget, v_BudgetForecastYear.AccountID,v_BudgetForecastYear.AccountNumber,v_BudgetForecastYear.AcctID,v_BudgetForecastYear.MajCatID,v_BudgetForecastYear.mincatid,v_BudgetForecastYear.[AccountName] AS AccountName1,round(isnull(v_BudgetForecastYear.Forecast,0),0) as Forecast,replace( convert(varchar(100),v_BudgetForecastYear.VessStart,106),' ','-')as  VessStart,replace( convert(varchar(100), v_BudgetForecastYear.VessEnd,106),' ','-') as VessEnd,v_BudgetForecastYear.YearDays, " +
        //    " (select midcat from dbo.tblaccountsmid G2 where G2.midcatid=v_BudgetForecastYear.midcatid) as Group1,v_BudgetForecastYear.midcatid, " +
        //    " (select minorcat from dbo.tblaccountsminor G3 where G3.mincatid=v_BudgetForecastYear.mincatid) as Group2, " +
        //            "v_BudgetForecastYear.AccountNumber,AnnAmt=isnull((select Amount from Add_v_BudgetForecastYear Addt where Addt.cocode=v_BudgetForecastYear.cocode and Addt.AcctId=v_BudgetForecastYear.AcctId and Addt.Byear=" + System.DateTime.Now.Year.ToString() + "),0) " +
        //            "FROM  " +
        //            "[dbo].v_BudgetForecastYear as  v_BudgetForecastYear  " +
        //            "INNER JOIN (select result as CY from CSVToTablestr('" + (int.Parse(lblBudgetYear.Text) - 1).ToString() + "',','))tempYear " + // $$$$ Static value---------------
        //            "ON v_BudgetForecastYear.Year = tempYear.CY " +
        //            "WHERE  " +
        //            "v_BudgetForecastYear.CoCode='" + ddlCompany.SelectedValue + "' AND v_BudgetForecastYear.Vess='" + ddlShip.SelectedValue + "'" +
        //            " ORDER BY v_BudgetForecastYear.MajSeqNo, v_BudgetForecastYear.MidSeqNo, v_BudgetForecastYear.MinSeqNo, v_BudgetForecastYear.AccountNumber";

      
	
        string sql = "SELECT VBFY.YearComment,VBFY.ImportedBy, " +
                     "replace(convert(varchar(15), VBFY.ImportedOn,106),' ','-')as ImportedOn,VBFY.UpdatedBy, " +
                     "replace(convert(varchar(15), VBFY.UpdatedOn,106),' ','-')as UpdatedOn,ISNULL(VBFY.Budget,0) AS Budget,  " +
                     "ACCOUNTSMAIN.AccountID,ACCOUNTSMAIN.AccountNumber, " +
                     "(SELECT STR(ACCOUNTSMAIN.AccountNumber,4) + REPLACE(STR(VESSELNO,4),' ','0') FROM dbo.VW_sql_tblSMDPRVessels M WHERE M.SHIPID='" + SelVess + "') AS ACCTID, " +
                     "ACCOUNTSMAIN.MajCatID,ACCOUNTSMAIN.mincatid, " +
                     "ACCOUNTSMAIN.[AccountName] AS AccountName1, " +
                     "round(isnull(VBFY.Forecast,0),0) as Forecast, " +
                     "(SELECT TOP 1 replace(convert(varchar,VSTART.VESSSTART,106),' ','-') FROM [dbo].tblSMDBudgetForecastYear as VSTART WHERE VSTART.CurFinYear ='" + FinancialYr.ToString() + "' AND VSTART.CoCode='" + SelComp + "' AND VSTART.ShipId='" + SelVess + "' ORDER BY VSTART.VESSSTART DESC) AS VessStart, " +
                     "(SELECT TOP 1 replace(convert(varchar,VEND.VessEnd,106),' ','-') FROM [dbo].tblSMDBudgetForecastYear as VEND WHERE VEND.CurFinYear ='" + FinancialYr.ToString() + "' AND VEND.CoCode='" + SelComp + "' AND VEND.ShipId='" + SelVess + "' ORDER BY VEND.VESSEND DESC) AS VessEnd, " +
                     "(SELECT TOP 1 VDAYS.YEARDAYS FROM [dbo].tblSMDBudgetForecastYear as VDAYS WHERE VDAYS.CurFinYear ='" + FinancialYr.ToString() + "' AND VDAYS.CoCode='" + SelComp + "' AND VDAYS.ShipId='" + SelVess + "' ORDER BY VDAYS.YEARDAYS DESC) AS YearDays, " +
                     "ACCOUNTSMAIN.Group1, " +
                     "ACCOUNTSMAIN.midcatid, " +
                     "ACCOUNTSMAIN.Group2, " +
                     "ACCOUNTSMAIN.AccountNumber, " +
                     "AnnAmt=isnull((select Amount from Add_v_BudgetForecastYear Addt where Addt.cocode=VBFY.cocode and Addt.AcctId=VBFY.AcctId and Addt.CurFinYear='" + FinancialYr.ToString() + "'),0)  " +
                     "FROM  " +
                     "( " +
                     "    SELECT DISTINCT AccountID,ACCOUNTNAME,MAJCATID,MIDCATID,MINCATID,AccountNumber,(select midcat from dbo.tblaccountsmid G2 where G2.midcatid=v_BudgetForecastYear.midcatid) as Group1,(select minorcat from dbo.tblaccountsminor G3 where G3.mincatid=v_BudgetForecastYear.mincatid) as Group2 " +
                     "    FROM [dbo].v_BudgetForecastYear_all as v_BudgetForecastYear " +
                     ")	 " +
                     "    ACCOUNTSMAIN " +
                     "LEFT JOIN  " +
                     "[dbo].v_BudgetForecastYear as VBFY ON VBFY.ACCOUNTID=ACCOUNTSMAIN.ACCOUNTID AND VBFY.CurFinYear ='" + FinancialYr.ToString() + "' AND VBFY.CoCode='" + SelComp + "' AND VBFY.Vess='" + SelVess + "' " +
                     "ORDER BY VBFY.MajSeqNo, VBFY.MidSeqNo, VBFY.MinSeqNo, VBFY.AccountNumber ";

//Response.Write(sql);
        DataTable DtRpt = Common.Execute_Procedures_Select_ByQuery(sql);

        if (DtRpt != null)
        {
            try
            {
                //Set Start and End Date
                if (DtRpt.Rows.Count > 0)
                {
                    txtStartDate.Text = DtRpt.Rows[0]["VessStart"].ToString();
                    txtEndDate.Text = DtRpt.Rows[0]["VessEnd"].ToString();
                    lblDays.Text = DtRpt.Rows[0]["YearDays"].ToString();

                    lblUppByAndOn.Text = DtRpt.Rows[0]["UpdatedBy"].ToString() + " / " + DtRpt.Rows[0]["UpdatedOn"].ToString();
                    lblUppByAndOn.Text = (lblUppByAndOn.Text == " / ") ? "" : lblUppByAndOn.Text;
                    lblExportedBy.Text = DtRpt.Rows[0]["ImportedBy"].ToString() + " / " + DtRpt.Rows[0]["ImportedOn"].ToString();
                    lblExportedBy.Text = (lblExportedBy.Text == " / ") ? "" : lblExportedBy.Text;
                }
                DataView dtView = DtRpt.DefaultView;
                if (ddlBudgetType.SelectedIndex == 0)
                {
                    if (ddlMidCategory.SelectedIndex > 0)
                    {
                        dtView.RowFilter = "midcatid="+ ddlMidCategory.SelectedValue +"";
                    }
                    //else
                    //{
                    //    dtView.RowFilter = "MajCatID not in(14,16)";
                    //}
                       
                }
                else
                {
                    if (ddlMidCategory.SelectedIndex > 0)
                    {
                        dtView.RowFilter = "MajCatID=" + ddlBudgetType.SelectedValue + " AND midcatid="+ ddlMidCategory.SelectedValue +"";
                    }
                    else
                    {
                        dtView.RowFilter = "MajCatID=" + ddlBudgetType.SelectedValue + "";
                    }
                    
                }
                //if (ddlMidCategory.SelectedIndex > 0)
                //{
                //    dtView.RowFilter = "
                //}

                rptBudget.DataSource = dtView;
                rptBudget.DataBind();
                GetTotalAmount();
                lblCompany.Text = ddlCompany.SelectedValue + " - " + ddlShip.SelectedValue + " - " + lblBudgetYear.Text;
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
	
        SetTotalAnnAmount();
        BindVesselSummary();
    }
    protected void BindVesselSummary()
    {
        int ForYear = DateTime.Today.Year;

        string VesselCode = ddlShip.SelectedValue;
        if (ddlShip.SelectedIndex <= 0)
            return;
        DataTable dt_Result = new DataTable();
        int ColumnSum;
        int VesselDays;
        ColumnSum = 0;
        VesselDays = Common.CastAsInt32(lblDays.Text);
        StringBuilder sb = new StringBuilder();
        DataTable dtAccts = Common.Execute_Procedures_Select_ByQuery("select distinct midcatid,midcat,MajSeqNo,MidSeqNo from dbo.v_New_CurrYearBudgetHome WHERE MajCatId=6 ORDER BY MajSeqNo,MidSeqNo");
        DataTable dtAccts1 = Common.Execute_Procedures_Select_ByQuery("select distinct midcatid,midcat,MajSeqNo,MidSeqNo from dbo.v_New_CurrYearBudgetHome WHERE MajCatId<>6 ORDER BY MajSeqNo,MidSeqNo");
        sb.Append("<table border='1' width='100%' cellpadding='1' cellspacing='0' class='newformat' height='220px'");
        sb.Append("<tr>");
        //sb.Append("<td class='header' style='font-size:10px; width:20px;'>&nbsp;</td>");
        sb.Append("<td class='header' style='font-size:10px; text-align:left; font-weight:bold;'>&nbsp;Budget Summary<br/></td>");
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
            //sb.Append("<td style='text-align:center'><a target='_blank' href='ForeCastComments.aspx?Comp=" + ddlCompany.SelectedValue + "&Ship=" + VesselCode + "&Fyear=" + lblBudgetYear.Text + "&MidCat=" + dtAccts.Rows[i][0].ToString() + "&HeadName=" + dtAccts.Rows[i][1].ToString() + "' title='Remarks' ><img src='Images/icon_comment.gif' /></a></td>");
            sb.Append("<td style='text-align:left'>&nbsp;" + dtAccts.Rows[i][1].ToString() + "</td>");
            dr[0] = dtAccts.Rows[i][1].ToString();

            DataTable dtShip = Common.Execute_Procedures_Select_ByQuery("select budget from dbo.v_New_CurrYearBudgetHome where shipid='" + VesselCode + "' AND CurFinYear='" + FinancialYr.ToString() + "' and  midcatid=" + dtAccts.Rows[i][0].ToString());
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
                    sb.Append("<td>&nbsp;</td>");
                    dr[VesselCode] = "0";
                }
            }
            else
            {
                sb.Append("<td>&nbsp;</td>");
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
        //sb.Append("<td>&nbsp;</td>");
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
        //sb.Append("<td>&nbsp;</td>");
        sb.Append("<td style='font-size:10px;text-align:right;font-weight:bold;'>Avg Daily Cost(US$)</td>");
        dr2[0] = "Avg Daily Cost(US$)";
        //int GrossSum = 0;

        try
        {
            sb.Append("<td style='font-size:10px;text-align:right;font-weight:bold;'>" + FormatCurrency((ColumnSum / VesselDays)) + "</td>");
            dr2[0] = FormatCurrency((ColumnSum / VesselDays));
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
            //sb.Append("<td style='text-align:center'><a target='_blank' href='ForeCastComments.aspx?Comp=" + ddlCompany.SelectedValue + "&Ship=" + VesselCode + "&Fyear=" + lblBudgetYear.Text + "&MidCat=" + dtAccts1.Rows[i][0].ToString() + "&HeadName=" + dtAccts1.Rows[i][1].ToString() + "' title='Remarks'> <img src='Images/icon_comment.gif' /></a></td>");
            sb.Append("<td style='text-align:left;'>&nbsp;" + dtAccts1.Rows[i][1].ToString() + "</td>");
            dr[0] = dtAccts1.Rows[i][1].ToString();

            DataTable dtShip = Common.Execute_Procedures_Select_ByQuery("select budget from dbo.v_New_CurrYearBudgetHome where shipid='" + VesselCode + "' AND CurFinYear='" + FinancialYr.ToString() + "' and  midcatid=" + dtAccts1.Rows[i][0].ToString());
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
                    sb.Append("<td>&nbsp;</td>");
                    dr[VesselCode] = "0";
                }
            }
            else
            {
                sb.Append("<td>&nbsp;</td>");
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
        //sb.Append("<td style='text-align:center'><a target='_blank' href='ForeCastComments.aspx?Comp=" + ddlCompany.SelectedValue + "&Ship=" + VesselCode + "&Fyear=" + lblBudgetYear.Text + "&MidCat=0&HeadName=Budget Coments' title='Remarks' ><img src='Images/icon_comment.gif' /></a></td>");
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
    public void GetTotalAmount()
    {
        int TotAmt = 0;
        int TotAmt1 = 0;
        foreach (RepeaterItem itm in rptBudget.Items)
        {
            HiddenField hfd = (HiddenField)itm.FindControl("hfAccountNumber");
            if(!(hfd.Value.Trim().StartsWith("17")))
            {
                Label lblActAmt = (Label)itm.FindControl("lblActAmt");
                TotAmt = TotAmt + Common.CastAsInt32(lblActAmt.Text);

                TextBox lblAnnAmt = (TextBox)itm.FindControl("txtAnnAmt");
                TotAmt1 = TotAmt1 + Common.CastAsInt32(lblAnnAmt.Text);
            }
        }
        lblTotal.Text = string.Format("{0:C}", TotAmt).Replace("$", "").Replace(".00", "");
        //if (Common.CastAsInt32(lblDays.Text) == 0)
        //{
        //    lblavgDailyCost.Text = "0";
        //}
        //else
        //{
        //    decimal amt=Math.Round(Convert.ToDecimal(Common.CastAsDecimal( TotAmt )/ Common.CastAsInt32(lblDays.Text)), 2);
        //    decimal amt1 = Math.Round(Convert.ToDecimal(Common.CastAsDecimal(TotAmt1) / Common.CastAsInt32(lblDays.Text)), 2);
        //    lblavgDailyCost.Text = string.Format("{0:C}", amt).Replace("$", "").Replace(".00", "");
        //    lblavgDailyCost1.Text = string.Format("{0:C}", amt1).Replace("$", "").Replace(".00", "");
        //}
    }

    protected void CalculateSum(object sender, EventArgs e)
    {
        GetTotalAmount();
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "tr", "SetLastFocus('dvscroll_BF');", true);
    }

    public string ResetNewValuesBudgetbyDays(decimal AnnualAmt)
    {
        Decimal DBudget = 0;
        if (ddlBudgetType.SelectedValue == "6" || ddlBudgetType.SelectedValue == "25") // this case is when selected Dry Docking or Pre Delivery Expenses
        {
            int DBMonth = Common.CastAsInt32(GetMonthFromDB().ToString());
            if (DBMonth <= 0)
            {
                Month1 = AnnualAmt;
                Month2 = 0;
                Month3 = 0;
                Month4 = 0;
                Month5 = 0;
                Month6 = 0;
                Month7 = 0;
                Month8 = 0;
                Month9 = 0;
                Month10 = 0;
                Month11 = 0;
                Month12 = 0;
                return AnnualAmt.ToString();

            }
            else
            {
                Month1 = 0;
                Month2 = 0;
                Month3 = 0;
                Month4 = 0;
                Month5 = 0;
                Month6 = 0;
                Month7 = 0;
                Month8 = 0;
                Month9 = 0;
                Month10 = 0;
                Month11 = 0;
                Month12 = 0;
                switch (Common.CastAsInt32(DBMonth))
                {
                    case 1:
                        Month1 = AnnualAmt;
                        break;
                    case 2:
                        Month2 = AnnualAmt;
                        break;
                    case 3:
                        Month3 = AnnualAmt;
                        break;
                    case 4:
                        Month4 = AnnualAmt;
                        break;
                    case 5:
                        Month5 = AnnualAmt;
                        break;
                    case 6:
                        Month6 = AnnualAmt;
                        break;
                    case 7:
                        Month7 = AnnualAmt;
                        break;
                    case 8:
                        Month8 = AnnualAmt;
                        break;
                    case 9:
                        Month9 = AnnualAmt;
                        break;
                    case 10:
                        Month10 = AnnualAmt;
                        break;
                    case 11:
                        Month11 = AnnualAmt;
                        break;
                    case 12:
                        Month12 = AnnualAmt;
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
                Month1 = DBudget;
                Month2 = DBudget;
                Month3 = DBudget;
                Month4 = DBudget;
                Month5 = DBudget;
                Month6 = DBudget;
                Month7 = DBudget;
                Month8 = DBudget;
                Month9 = DBudget;
                Month10 = DBudget;
                Month11 = DBudget;
                Month12 = DBudget;
                string CalculatedBudget = (Month1 + Month2 + Month3 + Month4 + Month5 + Month6 + Month7 + Month8 + Month9 + Month10 + Month11 + Month12).ToString();
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
                            Month1 = Common.CastAsDecimal(val);
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 2:
                            Month2 = Common.CastAsDecimal(val);
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 3:
                            Month3 = Common.CastAsDecimal(val);
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 4:
                            Month4 = Common.CastAsDecimal(val);
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 5:
                            Month5 = Common.CastAsDecimal(val);
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 6:
                            Month6 = Common.CastAsDecimal(val);
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 7:
                            Month7 = Common.CastAsDecimal(val);
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 8:
                            Month8 = Common.CastAsDecimal(val);
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 9:
                            Month9 = Common.CastAsDecimal(val);
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 10:
                            Month10 = Common.CastAsDecimal(val);
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 11:
                            Month11 = Common.CastAsDecimal(val);
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 12:
                            Month12 = Common.CastAsDecimal(val);
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                    }
                }
                //CalculateTotal();
                string CalculatedBudget = (Month1 + Month2 + Month3 + Month4 + Month5 + Month6 + Month7 + Month8 + Month9 + Month10 + Month11 + Month12).ToString();
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
                Month1 = AnnualAmt;
                Month2 = 0;
                Month3 = 0;
                Month4 = 0;
                Month5 = 0;
                Month6 = 0;
                Month7 = 0;
                Month8 = 0;
                Month9 = 0;
                Month10 = 0;
                Month11 = 0;
                Month12 = 0;
                return AnnualAmt.ToString();

            }
            else
            {
                Month1 = 0;
                Month2 = 0;
                Month3 = 0;
                Month4 = 0;
                Month5 = 0;
                Month6 = 0;
                Month7 = 0;
                Month8 = 0;
                Month9 = 0;
                Month10 = 0;
                Month11 = 0;
                Month12 = 0;
                switch (Common.CastAsInt32(DBMonth))
                {
                    case 1:
                        Month1 = AnnualAmt;
                        break;
                    case 2:
                        Month2 = AnnualAmt;
                        break;
                    case 3:
                        Month3 = AnnualAmt;
                        break;
                    case 4:
                        Month4 = AnnualAmt;
                        break;
                    case 5:
                        Month5 = AnnualAmt;
                        break;
                    case 6:
                        Month6 = AnnualAmt;
                        break;
                    case 7:
                        Month7 = AnnualAmt;
                        break;
                    case 8:
                        Month8 = AnnualAmt;
                        break;
                    case 9:
                        Month9 = AnnualAmt;
                        break;
                    case 10:
                        Month10 = AnnualAmt;
                        break;
                    case 11:
                        Month11 = AnnualAmt;
                        break;
                    case 12:
                        Month12 = AnnualAmt;
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
                Month1 = DBudget;
                Month2 = DBudget;
                Month3 = DBudget;
                Month4 = DBudget;
                Month5 = DBudget;
                Month6 = DBudget;
                Month7 = DBudget;
                Month8 = DBudget;
                Month9 = DBudget;
                Month10 = DBudget;
                Month11 = DBudget;
                Month12 = DBudget;
                string CalculatedBudget = (Month1 + Month2 + Month3 + Month4 + Month5 + Month6 + Month7 + Month8 + Month9 + Month10 + Month11 + Month12).ToString();
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
                            Month1 = Common.CastAsDecimal(val);
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 2:
                            Month2 = Common.CastAsDecimal(val);
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 3:
                            Month3 = Common.CastAsDecimal(val);
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 4:
                            Month4 = Common.CastAsDecimal(val);
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 5:
                            Month5 = Common.CastAsDecimal(val);
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 6:
                            Month6 = Common.CastAsDecimal(val);
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 7:
                            Month7 = Common.CastAsDecimal(val);
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 8:
                            Month8 = Common.CastAsDecimal(val);
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 9:
                            Month9 = Common.CastAsDecimal(val);
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 10:
                            Month10 = Common.CastAsDecimal(val);
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 11:
                            Month11 = Common.CastAsDecimal(val);
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 12:
                            Month12 = Common.CastAsDecimal(val);
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                    }
                }
                //CalculateTotal();
                string CalculatedBudget = (Month1 + Month2 + Month3 + Month4 + Month5 + Month6 + Month7 + Month8 + Month9 + Month10 + Month11 + Month12).ToString();
                return CalculatedBudget;
            }
        }

    }

    public string ResetNewValuesBudgetbyMonthly(decimal AnnualAmt)
    {
        Decimal DBudget = 0;
        if (ddlBudgetType.SelectedValue == "6" || ddlBudgetType.SelectedValue == "25" || ddlBudgetType.SelectedValue == "26") // this case is when selected Dry Docking or Pre Delivery Expenses
        {
            int DBMonth = Common.CastAsInt32(GetMonthFromDB().ToString());
            if (DBMonth <= 0)
            {
                Month1 = AnnualAmt;
                Month2 = 0;
                Month3 = 0;
                Month4 = 0;
                Month5 = 0;
                Month6 = 0;
                Month7 = 0;
                Month8 = 0;
                Month9 = 0;
                Month10 = 0;
                Month11 = 0;
                Month12 = 0;
                return AnnualAmt.ToString();

            }
            else
            {
                Month1 = 0;
                Month2 = 0;
                Month3 = 0;
                Month4 = 0;
                Month5 = 0;
                Month6 = 0;
                Month7 = 0;
                Month8 = 0;
                Month9 = 0;
                Month10 = 0;
                Month11 = 0;
                Month12 = 0;
                switch (Common.CastAsInt32(DBMonth))
                {
                    case 1:
                        Month1= AnnualAmt;
                        break;
                    case 2:
                        Month2 = AnnualAmt;
                        break;
                    case 3:
                        Month3 = AnnualAmt;
                        break;
                    case 4:
                        Month4 = AnnualAmt;
                        break;
                    case 5:
                        Month5 = AnnualAmt;
                        break;
                    case 6:
                        Month6 = AnnualAmt;
                        break;
                    case 7:
                        Month7 = AnnualAmt;
                        break;
                    case 8:
                        Month8 = AnnualAmt;
                        break;
                    case 9:
                        Month9 = AnnualAmt;
                        break;
                    case 10:
                        Month10 = AnnualAmt;
                        break;
                    case 11:
                        Month11 = AnnualAmt;
                        break;
                    case 12:
                        Month12 = AnnualAmt;
                        break;
                }
                return AnnualAmt.ToString();
            }
        }
        else
        {
            DBudget = Math.Round(AnnualAmt / 12, 2);
            if (txtStartDate.Text == "" && txtEndDate.Text == "")
            {
                Month1= DBudget;
                Month2= DBudget;
                Month3= DBudget;
                Month4= DBudget;
                Month5= DBudget;
                Month6= DBudget;
                Month7=DBudget;
                Month8= DBudget;
                Month9= DBudget;
                Month10= DBudget;
                Month11= DBudget;
                Month12= DBudget;
                string CalculatedBudget = Math.Round((Month1 + Month2 + Month3 + Month4 + Month5 + Month6 + Month7 + Month8 + Month9 + Month10 + Month11 + Month12),0).ToString();
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
                            val = Math.Round(Common.CastAsDecimal(PerDayBudget * TotalBudgetDays), 2).ToString();
                        }
                    }

                    switch (i)
                    {
                        case 1:
                            Month1 = Common.CastAsDecimal( val);
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 2:
                            Month2 = Common.CastAsDecimal(val);
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 3:
                            Month3 = Common.CastAsDecimal(val);
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 4:
                            Month4 = Common.CastAsDecimal(val);
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 5:
                            Month5 = Common.CastAsDecimal(val);
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 6:
                            Month6 = Common.CastAsDecimal(val);
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 7:
                            Month7 = Common.CastAsDecimal(val);
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 8:
                            Month8 = Common.CastAsDecimal(val);
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 9:
                            Month9 = Common.CastAsDecimal(val);
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 10:
                            Month10 = Common.CastAsDecimal(val);
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 11:
                            Month11 = Common.CastAsDecimal(val);
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 12:
                            Month12 = Common.CastAsDecimal(val);
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                    }
                }
                //CalculateTotal();
                string CalculatedBudget = Math.Round((Month1 + Month2 + Month3 + Month4 + Month5 + Month6 + Month7 + Month8 + Month9 + Month10 + Month11 + Month12),0).ToString();
                return CalculatedBudget;
            }
        }
         
    }

    public string ResetNewValuesforIndianFinacialYearBudgetbyMonthly(decimal AnnualAmt)
    {
        Decimal DBudget = 0;
        if (ddlBudgetType.SelectedValue == "6" || ddlBudgetType.SelectedValue == "25" || ddlBudgetType.SelectedValue == "26") // this case is when selected Dry Docking or Pre Delivery Expenses
        {
            int DBMonth = Common.CastAsInt32(GetMonthFromDB().ToString());
            if (DBMonth <= 0)
            {
                Month1 = AnnualAmt;
                Month2 = 0;
                Month3 = 0;
                Month4 = 0;
                Month5 = 0;
                Month6 = 0;
                Month7 = 0;
                Month8 = 0;
                Month9 = 0;
                Month10 = 0;
                Month11 = 0;
                Month12 = 0;
                return AnnualAmt.ToString();

            }
            else
            {
                Month1 = 0;
                Month2 = 0;
                Month3 = 0;
                Month4 = 0;
                Month5 = 0;
                Month6 = 0;
                Month7 = 0;
                Month8 = 0;
                Month9 = 0;
                Month10 = 0;
                Month11 = 0;
                Month12 = 0;
                switch (Common.CastAsInt32(DBMonth))
                {
                    case 1:
                        Month1 = AnnualAmt;
                        break;
                    case 2:
                        Month2 = AnnualAmt;
                        break;
                    case 3:
                        Month3 = AnnualAmt;
                        break;
                    case 4:
                        Month4 = AnnualAmt;
                        break;
                    case 5:
                        Month5 = AnnualAmt;
                        break;
                    case 6:
                        Month6 = AnnualAmt;
                        break;
                    case 7:
                        Month7 = AnnualAmt;
                        break;
                    case 8:
                        Month8 = AnnualAmt;
                        break;
                    case 9:
                        Month9 = AnnualAmt;
                        break;
                    case 10:
                        Month10 = AnnualAmt;
                        break;
                    case 11:
                        Month11 = AnnualAmt;
                        break;
                    case 12:
                        Month12 = AnnualAmt;
                        break;
                }
                return AnnualAmt.ToString();
            }
        }
        else
        {
            DBudget = Math.Round(AnnualAmt / 12, 2);
            if (txtStartDate.Text == "" && txtEndDate.Text == "")
            {
                Month1 = DBudget;
                Month2 = DBudget;
                Month3 = DBudget;
                Month4 = DBudget;
                Month5 = DBudget;
                Month6 = DBudget;
                Month7 = DBudget;
                Month8 = DBudget;
                Month9 = DBudget;
                Month10 = DBudget;
                Month11 = DBudget;
                Month12 = DBudget;
                string CalculatedBudget = Math.Round((Month1 + Month2 + Month3 + Month4 + Month5 + Month6 + Month7 + Month8 + Month9 + Month10 + Month11 + Month12),0).ToString();
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
                            val = Math.Round(Common.CastAsDecimal(PerDayBudget * TotalBudgetDays), 2).ToString();
                        }
                    }

                    switch (i)
                    {
                        case 1:
                            Month1 = Common.CastAsDecimal(val);
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 2:
                            Month2 = Common.CastAsDecimal(val);
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 3:
                            Month3 = Common.CastAsDecimal(val);
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 4:
                            Month4 = Common.CastAsDecimal(val);
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 5:
                            Month5 = Common.CastAsDecimal(val);
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 6:
                            Month6 = Common.CastAsDecimal(val);
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 7:
                            Month7 = Common.CastAsDecimal(val);
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 8:
                            Month8 = Common.CastAsDecimal(val);
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 9:
                            Month9 = Common.CastAsDecimal(val);
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 10:
                            Month10 = Common.CastAsDecimal(val);
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 11:
                            Month11 = Common.CastAsDecimal(val);
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                        case 12:
                            Month12 = Common.CastAsDecimal(val);
                            ViewState["TotalBudget"] = Common.CastAsDecimal(ViewState["TotalBudget"]) + Common.CastAsDecimal(val);
                            break;
                    }
                }
                //CalculateTotal();
                string CalculatedBudget = Math.Round((Month1 + Month2 + Month3 + Month4 + Month5 + Month6 + Month7 + Month8 + Month9 + Month10 + Month11 + Month12),0).ToString();
                return CalculatedBudget;
            }
        }

    }
    private int GetMonthFromDB()
    {
        int DBMonth = 0;
        string curFinyr = "";
        string preFinyr = "";
        string nextyr = "";
        int Month = System.DateTime.Now.Month;
       
        if (IsIndianFinacialYr == 1)
        {
            if (Month >=1 && Month <= 3)
            {
                nextyr = Convert.ToString(Common.CastAsInt32(lblBudgetYear.Text));
                curFinyr = Convert.ToString(Common.CastAsInt32(lblBudgetYear.Text) - 1) + "-" + nextyr.Substring(nextyr.Length - 2);
                preFinyr = Convert.ToString(Common.CastAsInt32(lblBudgetYear.Text) - 2) + "-" + lblBudgetYear.Text.Substring(lblBudgetYear.Text.Length - 2);
            }
           else
            {
                nextyr = Convert.ToString(Common.CastAsInt32(lblBudgetYear.Text) + 1);
                curFinyr = Convert.ToString(Common.CastAsInt32(lblBudgetYear.Text)) + "-" + nextyr.Substring(nextyr.Length - 2);
                preFinyr = Convert.ToString(Common.CastAsInt32(lblBudgetYear.Text) - 1) + "-" + lblBudgetYear.Text.Substring(lblBudgetYear.Text.Length - 2);
            }
        }
        else
        {
            curFinyr = Convert.ToString(Common.CastAsInt32(lblBudgetYear.Text));
            preFinyr = Convert.ToString(Common.CastAsInt32(lblBudgetYear.Text)-1);
        }
        string sql = "select top 1 forecast as actforecast,period,round(forecast,0)as forecast  from [dbo].tblSMDBudgetForecast  " +
                    " where CoCode='" + ddlCompany.SelectedValue + "'  " +
                    " AND AccountID in (select AccountID from [dbo].sql_tblSMDPRAccounts where MajCatID=" + ddlBudgetType.SelectedValue + ") " +
                    " AND forecast>0 AND VESSNO=" + GetVesselNo(ddlShip.SelectedValue) + " AND CurFinYear=" + preFinyr.ToString() + " order by period";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
        if (dt != null)
            if (dt.Rows.Count > 0)
            {
                DBMonth = Common.CastAsInt32(dt.Rows[0]["period"]);
            }
        return DBMonth;
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
    public void SetTotalAnnAmount()
    {
        TextBox txtAnnAmt = new TextBox();
        decimal dAnnAmt = 0;
        foreach (RepeaterItem RptItm in rptBudget.Items)
        {
            HiddenField hfd = (HiddenField)RptItm.FindControl("hfAccountNumber");
            if (!(hfd.Value.Trim().StartsWith("17")))
            {
                txtAnnAmt = (TextBox)RptItm.FindControl("txtAnnAmt");
                dAnnAmt = dAnnAmt + Common.CastAsDecimal(txtAnnAmt.Text);
            }
        }
        lblTotalAnnAmt.Text = string.Format("{0:C}", dAnnAmt).Replace("$", "").Replace(".00", "");
    }
    public Boolean IsBudgetLocked()
    {
        string sql = "select * from Add_tblBudgetLocking where Company='"+ddlCompany.SelectedValue+"' and Vessel='"+ddlShip.SelectedValue+"' and Year="+lblBudgetYear.Text+"";
        DataTable DT = Common.Execute_Procedures_Select_ByQuery(sql);
        if (DT.Rows.Count > 0)
            return true;
        else
            return false;
    }
    public string getCommentString(object comment)
    {
       string result="";
        if (comment.ToString().Length > 60)
            result = "<span title='" + comment.ToString().Replace("'","`") + "'>" + comment.ToString().Substring(0, 60) + " ...</span>";
        else
            result = "<span title='" + comment + "'>" + comment + "</span>";

        return result;
    }
    protected void imgbtnPublish_Click(object sender, EventArgs e)
    {
        string Query = "PrintCurrBudget.aspx?Comp=" + ddlCompany.SelectedItem.Text + "&Vessel=" + ddlShip.SelectedItem.Text + "&StartDate=" + txtStartDate.Text + "&EndDate=" + txtEndDate.Text + "&year=" + lblBudgetYear.Text + "&YearDays=" + lblDays.Text + "&MajCatID=6";
        //ScriptManager.RegisterStartupScript(Page, this.GetType(), "pub",Query, true);
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "pub","window.open('" + Query + "');", true);
    }

    protected void ddlMidCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindRepeater();
        }
        catch(Exception ex) { }
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
        PreFinancialYr = ""; 
        string nextyr = "";
        string curryr = "";
        lblBudgetYear.Text = Convert.ToString(System.DateTime.Now.Year);
        int year = Convert.ToInt32(lblBudgetYear.Text);
        int Month = System.DateTime.Now.Month;
       
        if (IsIndianFinacialYr == 1)
        {
           if (Month >=1 && Month <= 3)
            {
                nextyr = Convert.ToString(year);
                curryr = Convert.ToString(year-1);
                FinancialYr = Convert.ToString(year - 1) + "-" + nextyr.Substring(nextyr.Length - 2);
                PreFinancialYr = Convert.ToString(year - 2) + "-" + curryr.Substring(curryr.Length - 2);
                hdnCurrFinYr.Value = FinancialYr;
                hdnPreFinYr.Value = PreFinancialYr;
                lblBudgetYear.Text = hdnCurrFinYr.Value;
            }
           else
            {
                nextyr = Convert.ToString(year + 1);
                curryr = Convert.ToString(year);
                FinancialYr = Convert.ToString(year) + "-" + nextyr.Substring(nextyr.Length - 2);
                PreFinancialYr = Convert.ToString(year - 1) + "-" + curryr.Substring(curryr.Length - 2);
                hdnCurrFinYr.Value = FinancialYr;
                hdnPreFinYr.Value = PreFinancialYr;
                lblBudgetYear.Text = hdnCurrFinYr.Value;
            }
           
            
        }
        else
        {
            FinancialYr = Convert.ToString(year);
            PreFinancialYr = Convert.ToString(year-1);
            hdnCurrFinYr.Value = FinancialYr;
            hdnPreFinYr.Value = PreFinancialYr;
            lblBudgetYear.Text = hdnCurrFinYr.Value;
        }
    }



    
}