using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Configuration;

public partial class BudgetForecasting : System.Web.UI.Page
{
    public AuthenticationManager authRecInv;
    public string SessionDeleimeter = ConfigurationManager.AppSettings["SessionDelemeter"];
    DataSet DsValue;
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
            authRecInv = new AuthenticationManager(269, int.Parse(Session["loginid"].ToString()), ObjectType.Page);
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
            lblBudgetYear.Text = Convert.ToString(System.DateTime.Now.Year);  //$$$$ remove - 1
            BindCompany();
            BindBudgetType();
            if (Request.QueryString["VSL"] != null)
            {
                string VSL = Request.QueryString["VSL"];
                DataTable dt = Common.Execute_Procedures_Select_ByQuery("Select AccontCompany As Company from Vessel with(nolock)  where VesselCode='" + VSL + "'");
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
            imgImport.Visible = false;
            imgSave.Visible = false;
        }
        else
        {
            imgLockBudget.Visible = authRecInv.IsVerify2;
            imgImport.Visible = authRecInv.IsVerify;
            imgSave.Visible = false;
        }

        Print.Visible = authRecInv.IsPrint; 
    }
    // Event ----------------------------------------------------------------
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

        string sql = "dbo.SP_NEW_ImportBudget '" + ddlCompany.SelectedValue + "','" + ddlShip.SelectedValue + "'," + lblBudgetYear.Text + ",'" + Session["FullName"].ToString() + "'";
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
            decimal dCYAmt = Common.CastAsDecimal( ResetNewValues(Common.CastAsDecimal( txtAnnAmt.Text)));    
            HiddenField hfAccID = (HiddenField)RptItm.FindControl("hfAccID");
            //-------------------------------------------------------------------------------------
            //Details Data
            HiddenField hfAccountID = (HiddenField)RptItm.FindControl("hfAccountID");
            HiddenField hfAccountNumber = (HiddenField)RptItm.FindControl("hfAccountNumber");
            //decimal dCYAmt = dJan  +dFeb+ dmar+ dApr+ dMay+ dJun+ dJul+ dAug+ dSep+ dOct+ dnev+ dDec;
            string MonthlyBDG = dJan.ToString() + "," + dFeb.ToString() + "," + dmar.ToString() + "," + dApr.ToString() + "," + dMay.ToString() + "," + dJun.ToString() + "," + dJul.ToString() + "," + dAug.ToString() + "," + dSep.ToString() + "," + dOct.ToString() + "," + dnev.ToString() + "," + dDec.ToString();
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
        Common.Set_ParameterLength(4);
        Common.Set_Parameters(
                        new MyParameter("@Company", ddlCompany.SelectedValue),
                        new MyParameter("@Vessel", ddlShip.SelectedValue),
                        new MyParameter("@Year",lblBudgetYear.Text),
                        new MyParameter("@LockedBy", Session["FullName"].ToString())
            );
        DataSet ds = null;
        ds = Common.Execute_Procedures_Select();
        if (ds != null)
        {
            lblmsg.Text = "Saved successfully.";
            imgLockBudget.Visible = false;
            imgImport.Visible = false;
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
                    " WHERE (((VW_sql_tblSMDPRVessels.Company)='"+ddlCompany.SelectedValue+"')) ";
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
    public void BindRepeater()
    {
        string SelComp, SelVess, SelYear, SelYearNext;
        SelComp = ddlCompany.SelectedValue;
        SelVess = ddlShip.SelectedValue;
        SelYear = (int.Parse(lblBudgetYear.Text) - 1).ToString();
        SelYearNext = (int.Parse(lblBudgetYear.Text)).ToString();

	string VessNo = "";
        DataTable DtVessNo = Common.Execute_Procedures_Select_ByQuery("select vesselno from dbo.VW_sql_tblSMDPRVessels where shipid='" + ddlShip.SelectedValue + "'");
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
                     "(SELECT TOP 1 replace(convert(varchar,VSTART.VESSSTART,106),' ','-') FROM [dbo].tblSMDBudgetForecastYear as VSTART WHERE VSTART.Year =" + SelYear + " AND VSTART.CoCode='" + SelComp + "' AND VSTART.ShipId='" + SelVess + "' ORDER BY VSTART.VESSSTART DESC) AS VessStart, " +
                     "(SELECT TOP 1 replace(convert(varchar,VEND.VessEnd,106),' ','-') FROM [dbo].tblSMDBudgetForecastYear as VEND WHERE VEND.Year =" + SelYear + " AND VEND.CoCode='" + SelComp + "' AND VEND.ShipId='" + SelVess + "' ORDER BY VEND.VESSEND DESC) AS VessEnd, " +
                     "(SELECT TOP 1 VDAYS.YEARDAYS FROM [dbo].tblSMDBudgetForecastYear as VDAYS WHERE VDAYS.Year =" + SelYear + " AND VDAYS.CoCode='" + SelComp + "' AND VDAYS.ShipId='" + SelVess + "' ORDER BY VDAYS.YEARDAYS DESC) AS YearDays, " +
                     "ACCOUNTSMAIN.Group1, " +
                     "ACCOUNTSMAIN.midcatid, " +
                     "ACCOUNTSMAIN.Group2, " +
                     "ACCOUNTSMAIN.AccountNumber, " +
                     "AnnAmt=isnull((select Amount from Add_v_BudgetForecastYear Addt where Addt.cocode=VBFY.cocode and Addt.AcctId=VBFY.AcctId and Addt.Byear=" + SelYearNext + "),0)  " +
                     "FROM  " +
                     "( " +
                     "    SELECT DISTINCT AccountID,ACCOUNTNAME,MAJCATID,MIDCATID,MINCATID,AccountNumber,(select midcat from dbo.tblaccountsmid G2 where G2.midcatid=v_BudgetForecastYear.midcatid) as Group1,(select minorcat from dbo.tblaccountsminor G3 where G3.mincatid=v_BudgetForecastYear.mincatid) as Group2 " +
                     "    FROM [dbo].v_BudgetForecastYear " +
                     ")	 " +
                     "    ACCOUNTSMAIN " +
                     "LEFT JOIN  " +
                     "[dbo].v_BudgetForecastYear as VBFY ON VBFY.ACCOUNTID=ACCOUNTSMAIN.ACCOUNTID AND VBFY.Year =" + SelYear + " AND VBFY.CoCode='" + SelComp + "' AND VBFY.Vess='" + SelVess + "' " +
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
                    dtView.RowFilter = "MajCatID not in(14,16)";
                }
                else
                {
                    dtView.RowFilter = "MajCatID=" + ddlBudgetType.SelectedValue + "";
                }

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
                        dJan= AnnualAmt;
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
                dJan= DBudget;
                dFeb= DBudget;
                dmar= DBudget;
                dApr= DBudget;
                dMay= DBudget;
                dJun= DBudget;
                dJul=DBudget;
                dAug= DBudget;
                dSep= DBudget;
                dOct= DBudget;
                dnev= DBudget;
                dDec= DBudget;
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
                            dJan = Common.CastAsDecimal( val);
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
    private int GetMonthFromDB()
    {
        int DBMonth = 0;
        string sql = "select top 1 forecast as actforecast,period,round(forecast,0)as forecast  from [dbo].tblSMDBudgetForecast  " +
                    " where CoCode='" + ddlCompany.SelectedValue + "'  " +
                    " AND AccountID in (select AccountID from [dbo].sql_tblSMDPRAccounts where MajCatID=" + ddlBudgetType.SelectedValue + ") " +
                    " AND forecast>0 AND VESSNO=" + GetVesselNo(ddlShip.SelectedValue) + " AND Year=" + (Common.CastAsInt32( lblBudgetYear.Text )- 1).ToString() + " order by period";
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
    protected void imgbtnPublish_Click(object sender, ImageClickEventArgs e)
    {
        string Query = "PrintCurrBudget.aspx?Comp=" + ddlCompany.SelectedItem.Text + "&Vessel=" + ddlShip.SelectedItem.Text + "&StartDate=" + txtStartDate.Text + "&EndDate=" + txtEndDate.Text + "&year=" + lblBudgetYear.Text + "&YearDays=" + lblDays.Text + "&MajCatID=6";
        //ScriptManager.RegisterStartupScript(Page, this.GetType(), "pub",Query, true);
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "pub","window.open('" + Query + "');", true);
    }
}