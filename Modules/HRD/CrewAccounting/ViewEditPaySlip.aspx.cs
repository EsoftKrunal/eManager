using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class CrewAccounting_ViewEditPaySlip : System.Web.UI.Page
{
    AuthenticationManager auth;
    public int Vesselid 
    {
        get { return Common.CastAsInt32(ViewState["Vesselid"]); }
        set { ViewState["Vesselid"] = value; }
    }
    public int Month
    {
        get { return Common.CastAsInt32(ViewState["Month"]); }
        set { ViewState["Month"] = value; }
    }
    public int Year
    {
        get { return Common.CastAsInt32(ViewState["Year"]); }
        set { ViewState["Year"] = value; }
    }
    public int PayRollID
    {
        get { return Common.CastAsInt32(ViewState["PayRollID"]); }
        set { ViewState["PayRollID"] = value; }
    }
    public int CONTRACTID
    {
        get { return Common.CastAsInt32(ViewState["CONTRACTID"]); }
        set { ViewState["CONTRACTID"] = value; }
    }
    public string CrewNo
    {
        get { return (Convert.ToString( ViewState["CrewNo"])); }
        set { ViewState["CrewNo"] = value; }
    }
    public string CrewName
    {
        get { return (Convert.ToString(ViewState["CrewName"])); }
        set { ViewState["CrewName"] = value; }
    }
    public string RankName
    {
        get { return (Convert.ToString(ViewState["RankName"])); }
        set { ViewState["RankName"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        auth = new AuthenticationManager(32,Common.CastAsInt32(Session["loginid"]), ObjectType.Page);   
        PayRollID = Common.CastAsInt32(Request.QueryString["PayrollId"]);
        if (!Page.IsPostBack)
        {
            ShowData();
        }
    }
    //Events
    protected void ReCalCulateWages(object sender, EventArgs e)
    {
        string sql = "SELECT " +
	                "(select cast(Amount as numeric(10,2)) From crewcontractdetails ccd where ccd.ContractId=" + CONTRACTID + " And WagwScaleComponentId=1) As Cont_Comp_1, " +
	                "(select cast(Amount as numeric(10,2)) From crewcontractdetails ccd where ccd.ContractId=" + CONTRACTID + " And WagwScaleComponentId=2) As Cont_Comp_2, " +
	                "(select cast(Amount as numeric(10,2)) From crewcontractdetails ccd where ccd.ContractId=" + CONTRACTID + " And WagwScaleComponentId=3) As Cont_Comp_3, " +
	                "(select cast(Amount as numeric(10,2)) From crewcontractdetails ccd where ccd.ContractId=" + CONTRACTID + " And WagwScaleComponentId=4) As Cont_Comp_4, " +
	                "(select cast(Amount as numeric(10,2)) From crewcontractdetails ccd where ccd.ContractId=" + CONTRACTID + " And WagwScaleComponentId=5) As Cont_Comp_5, " +
	                "(select cast(Amount as numeric(10,2)) From crewcontractdetails ccd where ccd.ContractId=" + CONTRACTID + " And WagwScaleComponentId=6) As Cont_Comp_6, " +
	                "(select cast(Amount as numeric(10,2)) From crewcontractdetails ccd where ccd.ContractId=" + CONTRACTID + " And WagwScaleComponentId=7) As Cont_Comp_7, " +
	                "(select cast(Amount as numeric(10,2)) From crewcontractdetails ccd where ccd.ContractId=" + CONTRACTID + " And WagwScaleComponentId=8) As Cont_Comp_8, " +
	                "(select cast(Amount as numeric(10,2)) From crewcontractdetails ccd where ccd.ContractId=" + CONTRACTID + " And WagwScaleComponentId=9) As Cont_Comp_9, " + 
	                "(select cast(Amount as numeric(10,2)) From crewcontractdetails ccd where ccd.ContractId=" + CONTRACTID + " And WagwScaleComponentId=10) As Cont_Comp_10, " +
	                "(select cast(Amount as numeric(10,2)) From crewcontractdetails ccd where ccd.ContractId=" + CONTRACTID + " And WagwScaleComponentId=11) As Cont_Comp_11, " +
	                "(select cast(Amount as numeric(10,2)) From crewcontractdetails ccd where ccd.ContractId=" + CONTRACTID + " And WagwScaleComponentId=12) As Cont_Comp_12 , " +
	                "(select cast(OtherAmount as numeric(10,2)) from crewcontractheader cch1 where cch1.contractid=" + CONTRACTID + ") as MTMAllowance";
        DataSet DS = Budget.getTable(sql);
        if (DS != null)
        {
            if (DS.Tables[0].Rows.Count > 0)
            {
                DataRow Dr = DS.Tables[0].Rows[0];
                int DiffDays = Common.CastAsInt32(txtTD.Text) - Common.CastAsInt32(txtFD.Text) + 1;
                int MonthDays = 30;
                int OtHrs = Common.CastAsInt32(txtOT.Text);


                if (DateTime.DaysInMonth(Year, Month) == 31)
                {
                    if (DiffDays == 30 || DiffDays == 31)
                        MonthDays = DiffDays;
                }

                txtBasicPay.Text = Math.Round((DiffDays * Common.CastAsDecimal(Dr["Cont_Comp_1"])) / MonthDays, 2).ToString();
                txtFixedOT.Text = Math.Round((DiffDays * Common.CastAsDecimal(Dr["Cont_Comp_2"])) / MonthDays, 2).ToString();
                txtBonus.Text = Math.Round((DiffDays * Common.CastAsDecimal(Dr["Cont_Comp_3"])) / MonthDays, 2).ToString();
                txtLeavePay.Text = Math.Round((DiffDays * Common.CastAsDecimal(Dr["Cont_Comp_4"])) / MonthDays, 2).ToString();
                txtSeniorityAllowance.Text = Math.Round((DiffDays * Common.CastAsDecimal(Dr["Cont_Comp_5"])) / MonthDays, 2).ToString();
                txtTradeAllowance.Text = Math.Round((DiffDays * Common.CastAsDecimal(Dr["Cont_Comp_6"])) / MonthDays, 2).ToString();
                txtSuperiorCertificateAllowance.Text = Math.Round((DiffDays * Common.CastAsDecimal(Dr["Cont_Comp_7"])) / MonthDays, 2).ToString();
                txtCommAllowance.Text = Math.Round((DiffDays * Common.CastAsDecimal(Dr["Cont_Comp_8"])) / MonthDays, 2).ToString();
                txtSubAllowance.Text = Math.Round((DiffDays * Common.CastAsDecimal(Dr["Cont_Comp_9"])) / MonthDays, 2).ToString();
                txtUAllowance.Text = Math.Round((DiffDays * Common.CastAsDecimal(Dr["Cont_Comp_10"])) / MonthDays, 2).ToString();
                txtGmdss.Text = Math.Round((DiffDays * Common.CastAsDecimal(Dr["Cont_Comp_11"])) / MonthDays, 2).ToString();
                
                txtAdditioinalOT.Text = Math.Round((OtHrs * Common.CastAsDecimal(Dr["Cont_Comp_12"])), 2).ToString();

                txtMTMAllowance.Text = Math.Round((DiffDays * Common.CastAsDecimal(Dr["MTMAllowance"])) / MonthDays, 2).ToString();

                txtOtherPayments.Text = "0";
                txtAdditionalPayment.Text = "0";
            }
        }
        CalculateWages();
    }
    protected void CalCulateWages(object sender, EventArgs e)
    {
        CalculateWages();
    }
    protected void btnSave_OnClick(object sender, EventArgs e)
    {
        Common.Set_Procedures("DBO.sp_InsertUpdateCrewPortageBill");
        Common.Set_ParameterLength(43);
        Common.Set_Parameters(
            new MyParameter("@PAYROLLID", PayRollID),
            new MyParameter("@VESSELID", Vesselid),
            new MyParameter("@PAYMONTH", Month),
            new MyParameter("@PAYYEAR", Year),
            new MyParameter("@CREWNUMBER",CrewNo),
            new MyParameter("@CREWNAME", CrewName),
            new MyParameter("@RANK", RankName),
            new MyParameter("@FD", txtFD.Text),
            new MyParameter("@TD", txtTD.Text),
            new MyParameter("@OT", txtOT.Text),
            new MyParameter("@CONTRACTID", CONTRACTID),
            new MyParameter("@CONT_COMP_1", Common.CastAsDecimal( txtBasicPay.Text)),
            new MyParameter("@CONT_COMP_2", Common.CastAsDecimal(txtFixedOT.Text)),
            new MyParameter("@CONT_COMP_3", Common.CastAsDecimal(txtBonus.Text)),
            new MyParameter("@CONT_COMP_4", Common.CastAsDecimal(txtLeavePay.Text)),
            new MyParameter("@CONT_COMP_5", Common.CastAsDecimal(txtSeniorityAllowance.Text)),
            new MyParameter("@CONT_COMP_6", Common.CastAsDecimal(txtTradeAllowance.Text)),
            new MyParameter("@CONT_COMP_7", Common.CastAsDecimal(txtSuperiorCertificateAllowance.Text)),
            new MyParameter("@CONT_COMP_8", Common.CastAsDecimal(txtCommAllowance.Text)),
            new MyParameter("@CONT_COMP_9", Common.CastAsDecimal(txtSubAllowance.Text)),
            new MyParameter("@CONT_COMP_10", Common.CastAsDecimal(txtUAllowance.Text)),
            new MyParameter("@CONT_COMP_11", Common.CastAsDecimal(txtGmdss.Text)),
            new MyParameter("@CONT_COMP_12", Common.CastAsDecimal(txtAdditioinalOT.Text)),
            new MyParameter("@MTMAllowance", Common.CastAsDecimal(txtMTMAllowance.Text)),
            new MyParameter("@OTHERPAYMENTS", Common.CastAsDecimal(txtOtherPayments.Text)),
            new MyParameter("@ADDITIONALPAYMENT", Common.CastAsDecimal(txtAdditionalPayment.Text)),
            new MyParameter("@TOTALEARNINGS", Common.CastAsDecimal(lblTotalEarnings.Text)),
            new MyParameter("@ALLOTMENT", Common.CastAsDecimal(txtAllotment.Text)),
            new MyParameter("@CASHADVANCE", Common.CastAsDecimal(txtCashAdvance.Text)),
            new MyParameter("@BONDEDSTORE", Common.CastAsDecimal(txtBondStore.Text)),
            new MyParameter("@RADIOTELECALL", Common.CastAsDecimal(txtRadioAccount.Text)),
            new MyParameter("@OTHERDEDUCTIONS", Common.CastAsDecimal(txtOtherDeductions.Text)),
            new MyParameter("@UNIONFEE", Common.CastAsDecimal(txtUnionFee.Text)),


            new MyParameter("@FBOWPAIDONBOARD", Common.CastAsDecimal(txtFBOWPaidOnBoard.Text)),
            new MyParameter("@TOTALDEDUCTIONS", Common.CastAsDecimal(lblTotalDeductions.Text)),
            new MyParameter("@CURMONBAL", Common.CastAsDecimal(lblWagesForValue.Text)),
            new MyParameter("@PrevMonBal", Common.CastAsDecimal(lblBowFromValue.Text)),
            new MyParameter("@PAID", Common.CastAsDecimal(txtPaidOffWages.Text)),
            new MyParameter("@BALANCEOFWAGES", Common.CastAsDecimal(lblBalanceOfWages.Text)),
            new MyParameter("@VERIFIED", "0"),
            new MyParameter("@VERIFIEDBY", ""),
            new MyParameter("@VERIFIEDON", DateTime.Now.ToString("dd-MMM-yyyy")),
            new MyParameter("@AUTOSAVED", "0"));

        DataSet ds=new DataSet();
        bool res;
        res=Common.Execute_Procedures_IUD(ds);
        if (res)
        {
            lblMsg.Text = "Record saved successfully.";
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "ss", "RefereshParentPage()", true);
        }
        else
        {
            lblMsg.Text = "Record could not be saved.";
        }
    }
    protected void btnCancel_OnClick(object sender, EventArgs e)
    {
    }
    protected void btnVerify_OnClick(object sender, EventArgs e)
    {
        if (txtRemark.Text.Trim() == "")
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "ddd", "alert('Please fill Verified by.')", true);
            txtRemark.Focus(); return;
        }
        string sql = "update CrewPortageBill set Verified=1 ,Verifiedby=" + Common.CastAsInt32(Session["loginid"]) + ",Remarks='" + txtRemark.Text.Trim().Replace("'", "`") + "',VerifiedOn=getdate() where PayrollId=" + PayRollID + "  select -1";
        DataSet Dt = Budget.getTable(sql);
        if (Dt != null)
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "ddd", "alert('Record update successully.')", true);
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "ss", "RefereshParentPage()", true);
            btnSave.Visible = false;
            imgUpdateBowFrom.Visible = false;
            ShowData();
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "ddd", "alert('Record could not be updated.')", true);
        }
    }
    protected void imgUpdateBowFrom_OnClick(object sender, EventArgs e)
    {
        if (imgUpdateBowFrom.ImageUrl == "~/Modules/HRD/Images/check.png")
        {
            lblBowFromValue.Visible = true;
            txtBowFromValue.Visible = false;
            imgUpdateBowFrom.ImageUrl = "~/Modules/HRD/Images/icon_pencil.gif";

            string sql = "Update DBO.CrewPortageBill set PrevMonBal="+Common.CastAsDecimal( txtBowFromValue.Text)+" where PayrollID="+PayRollID+"";
            DataSet DS = Budget.getTable(sql);
            ShowData();
        }
        else
        {
            lblBowFromValue.Visible = false;
            txtBowFromValue.Visible = true;
            txtBowFromValue.Text = lblBowFromValue.Text;
            imgUpdateBowFrom.ImageUrl = "~/Modules/HRD/Images/check.png";
        }
        CalculateWages();
    }
    //Function
    public void ShowData()
    {
        string StrMonthYear = "";
        //string sql = "SELECT * FROM DBO.CrewPortageBill WHERE VesselId=" + Vesselid + " AND PayMonth=" + Month + " AND PayYear=" + Year + " AND CrewNumber='" + CrewNo + "'";
        string sql = "SELECT (select VesselName from DBO.Vessel V where V.VesselID=CP.VesselID) VesselName "+
            " ,(select FirstName+' '+LastName from UserLogin U where U.LoginID=CP.Verifiedby)VerifiedbyText" +
            " ,replace(convert(varchar,verifiedOn ,106),' ','-')verifiedOnText " +
            ",* FROM DBO.CrewPortageBill  CP WHERE CP.PayRollID=" + PayRollID + "";
        DataSet DS = Budget.getTable(sql);
        if (DS != null)
        {
            if (DS.Tables[0].Rows.Count > 0)
            {
                DataRow Dr = DS.Tables[0].Rows[0];

                PayRollID = Common.CastAsInt32(Dr["PAYROLLID"]);
                CrewName = Dr["CREWNAME"].ToString();
                RankName = Dr["RANK"].ToString();
                CrewNo = Dr["CREWNUMBER"].ToString();
                CONTRACTID = Common.CastAsInt32(Dr["CONTRACTID"]);
                Month = Common.CastAsInt32(Dr["PAYMONTH"]);
                Year = Common.CastAsInt32(Dr["PAYYEAR"]);
                Vesselid = Common.CastAsInt32(Dr["VESSELID"]);

                ShowLastMonthData(Vesselid, Month, Year, CrewNo);

                StrMonthYear = ConvertMMMToM(Month) + " " + Year.ToString();

                lblThisMonthWages.Text = ConvertMMMToM(Month) + " " + Year.ToString();
                lblLastMonthWages.Text = Convert.ToDateTime( "1-"+ ConvertMMMToM(Month) + "-" + Year.ToString()).AddMonths(-1).ToString("MMM yyyy").ToUpper();

                lblWagesForDuration.Text = StrMonthYear;

                if (Month == 1)
                {
                    lblBowFromDuration.Text = ConvertMMMToM(12) + " " +Convert.ToString( Year-1);
                }
                else
                    lblBowFromDuration.Text = ConvertMMMToM(Month-1) + " " + Year.ToString();

                // Master Date
                lblName.Text = Dr["CREWNAME"].ToString();
                lblrank.Text = Dr["RANK"].ToString();
                txtFD.Text = Dr["FD"].ToString();
                txtTD.Text = Dr["TD"].ToString();
                txtOT.Text = Dr["OT"].ToString();
                lblVesselName.Text = Dr["VesselName"].ToString();
                lblCrewNo.Text = Dr["CREWNUMBER"].ToString();


                txtBasicPay.Text = Dr["Cont_Comp_1"].ToString();
                txtFixedOT.Text = Dr["Cont_Comp_2"].ToString();
                txtBonus.Text = Dr["Cont_Comp_3"].ToString();
                txtLeavePay.Text = Dr["Cont_Comp_4"].ToString();
                txtSeniorityAllowance.Text = Dr["Cont_Comp_5"].ToString();
                txtTradeAllowance.Text = Dr["Cont_Comp_6"].ToString(); 
                txtSuperiorCertificateAllowance.Text = Dr["Cont_Comp_7"].ToString();
                txtCommAllowance.Text = Dr["Cont_Comp_8"].ToString();
                txtSubAllowance.Text = Dr["Cont_Comp_9"].ToString();
                txtUAllowance.Text = Dr["Cont_Comp_10"].ToString();
                txtGmdss.Text = Dr["Cont_Comp_11"].ToString();
                txtAdditioinalOT.Text = Dr["Cont_Comp_12"].ToString();
                txtMTMAllowance.Text = Dr["MTMAllowance"].ToString();
                txtOtherPayments.Text = Dr["OtherPayments"].ToString();
                txtAdditionalPayment.Text = Dr["AdditionalPayment"].ToString();
                lblTotalEarnings.Text = Dr["TotalEarnings"].ToString();                

                txtAllotment.Text = Dr["Allotment"].ToString();
                txtCashAdvance.Text = Dr["CashAdvance"].ToString();
                txtBondStore.Text = Dr["BondedStore"].ToString();
                txtRadioAccount.Text = Dr["RadioTeleCall"].ToString();
                txtOtherDeductions.Text = Dr["OtherDeductions"].ToString();
                txtUnionFee.Text = Dr["UNIONFEE"].ToString();

                txtFBOWPaidOnBoard.Text = Dr["FbowPaidOnBoard"].ToString();
                lblTotalDeductions.Text = Dr["TotalDeductions"].ToString();
                TotDedThisMonth.Text = lblTotalDeductions.Text;

                lblWagesForValue.Text = lblTotalEarnings.Text;
                lblBowFromValue.Text = Dr["PrevMonBal"].ToString();
                TotWagesPayAbleOnBoard.Text = Common.CastAsDecimal(Common.CastAsDecimal(Dr["CurMonBal"]) + Common.CastAsDecimal(Dr["PrevMonBal"])).ToString();
                txtPaidOffWages.Text = Dr["Paid"].ToString();
                lblBalanceOfWages.Text = Dr["BalanceOfWages"].ToString();
                if (Dr["Verified"].ToString() == "True")
                {
                    lblVerifiedByOn.Text = Dr["VerifiedbyText"].ToString() + " / " + Dr["verifiedOnText"].ToString();
                    txtRemark.Text = Dr["Remarks"].ToString();
                    //btnVerify.Visible = false;
                    //trVerified.Visible = true;
                    //btnSave.Visible = false;
                    //imgUpdateBowFrom.Visible = false;
                }
                else
                {
                    //lblVerifiedByOn.Text = "";
                    //txtRemark.Text = "";
                    //btnVerify.Visible = true;
                    //trVerified.Visible = false ;
                    //imgUpdateBowFrom.Visible = true && auth.IsVerify;
                }
                    //// Button Vesibility

                string sqlVesibility = "SELECT * FROM DBO.CrewPortageBillClosure WHERE VessId=" + Vesselid + " AND [Month]=" + Month + " AND [Year]=" + Year;
                DataSet DSVesibility = Budget.getTable(sqlVesibility);
                if (DSVesibility != null)
                {
                    if (DSVesibility.Tables[0].Rows.Count > 0)
                    {
                        btnSave.Visible = false;
                        imgUpdateBowFrom.Visible = false;
                    }
                    else
                    {
                        btnSave.Visible = true && auth.IsUpdate;
                        imgUpdateBowFrom.Visible = true && auth.IsUpdate;
                    }
                }
           
            }
        }

        
    }
    public void ShowLastMonthData(int VessleID,int Month,int Year,string CrewNumber)
    {
        DateTime sDate = Convert.ToDateTime("1-" + ConvertMMMToM( Month )+ "-" + Year.ToString()).AddMonths(-1);
        Month = sDate.Month;
        Year = sDate.Year;
        string sql = "SELECT (select VesselName from DBO.Vessel V where V.VesselID=CP.VesselID) VesselName " +
            " ,(select FirstName+' '+LastName from UserLogin U where U.LoginID=CP.Verifiedby)VerifiedbyText" +
            " ,replace(convert(varchar,verifiedOn ,106),' ','-')verifiedOnText " +
            ",* FROM DBO.CrewPortageBill  CP WHERE CP.CREWNUMBER='" + CrewNumber + "' AND CP.VesselId=" + VessleID + " and CP.PayMonth=" + Month + " and CP.PayYear="+Year+"";
        DataSet DS = Budget.getTable(sql);
        if (DS != null)
        {
            if (DS.Tables[0].Rows.Count > 0)
            {
                DataRow Dr = DS.Tables[0].Rows[0];
                lblBasicPay.Text = Dr["Cont_Comp_1"].ToString();
                lblFixedOT.Text = Dr["Cont_Comp_2"].ToString();
                lblBonus.Text = Dr["Cont_Comp_3"].ToString();
                lblLeavePay.Text = Dr["Cont_Comp_4"].ToString();
                lblSeniorityAllowance.Text = Dr["Cont_Comp_5"].ToString();
                lblTradeAllowance.Text = Dr["Cont_Comp_6"].ToString();
                lblSuperiorCertificateAllowance.Text = Dr["Cont_Comp_7"].ToString();
                lblCommAllowance.Text = Dr["Cont_Comp_8"].ToString();
                lblSubAllowance.Text = Dr["Cont_Comp_9"].ToString();
                lblUAllowance.Text = Dr["Cont_Comp_10"].ToString();
                lblGmdss.Text = Dr["Cont_Comp_11"].ToString();
                lblAdditioinalOT.Text = Dr["Cont_Comp_12"].ToString();
                lblMTMAllowance.Text = Dr["MTMAllowance"].ToString();
                lblOtherPayments.Text = Dr["OtherPayments"].ToString();
                lblAdditionalPayment.Text = Dr["AdditionalPayment"].ToString();
                lblTotalEarningsLastMonth.Text = Dr["TotalEarnings"].ToString();
            }
        }

    }
    public void CalculateWages()
    {
        lblTotalEarnings.Text = 
                Common.CastAsDecimal(
                Common.CastAsDecimal(txtBasicPay.Text.Trim()) + 
                Common.CastAsDecimal(txtFixedOT.Text.Trim()) +
                Common.CastAsDecimal(txtBonus.Text.Trim()) +
                Common.CastAsDecimal(txtLeavePay.Text.Trim()) +
                Common.CastAsDecimal(txtSeniorityAllowance.Text.Trim()) +
                Common.CastAsDecimal(txtTradeAllowance.Text.Trim()) +
                Common.CastAsDecimal(txtSuperiorCertificateAllowance.Text.Trim()) +
                Common.CastAsDecimal(txtCommAllowance.Text.Trim()) +
                Common.CastAsDecimal(txtSubAllowance.Text.Trim()) +
                Common.CastAsDecimal(txtUAllowance.Text.Trim()) +
                Common.CastAsDecimal(txtGmdss.Text.Trim()) + 
                Common.CastAsDecimal(txtAdditioinalOT.Text.Trim()) +
                Common.CastAsDecimal(txtMTMAllowance.Text.Trim()) +
                Common.CastAsDecimal(txtOtherPayments.Text.Trim()) +
                Common.CastAsDecimal(txtAdditionalPayment.Text.Trim())
                ).ToString();
        lblWagesForValue.Text = lblTotalEarnings.Text;
        lblTotalDeductions.Text = 
                Common.CastAsDecimal(
                Common.CastAsDecimal(txtAllotment.Text.Trim()) +
                Common.CastAsDecimal(txtCashAdvance.Text.Trim()) + 
                Common.CastAsDecimal(txtBondStore.Text.Trim()) +
                Common.CastAsDecimal(txtRadioAccount.Text.Trim()) +
                Common.CastAsDecimal(txtOtherDeductions.Text.Trim()) +
                Common.CastAsDecimal(txtUnionFee.Text.Trim()) +
                Common.CastAsDecimal(txtFBOWPaidOnBoard.Text.Trim()) +
                Common.CastAsDecimal(txtPaidOffWages.Text.Trim())
                ).ToString();
        TotDedThisMonth.Text = lblTotalDeductions.Text;

        TotWagesPayAbleOnBoard.Text = 
                Common.CastAsDecimal(
                Common.CastAsDecimal(lblTotalEarnings.Text) -
                Common.CastAsDecimal(lblTotalDeductions.Text) + 
                Common.CastAsDecimal(lblBowFromValue.Text)
                ).ToString();
        lblBalanceOfWages.Text=
                Common.CastAsDecimal(
                Common.CastAsDecimal(TotWagesPayAbleOnBoard.Text) 
                
                ).ToString();
 
    }
    public decimal SetPrevMonthBal()
    {
        int PrevMonth;
        int LocalYear;
        if (Month == 1)
        {
            PrevMonth = 12;
            LocalYear = Year - 1;
        }
        else
        {
            PrevMonth = Month - 1;
            LocalYear = Year;
        }
        string sql = "select BalanceofWages from CrewPortageBill where VesselId=" + Vesselid + " and PayMonth=" + PrevMonth + " and PayYear=" + LocalYear + " and CrewNumber='" + CrewNo + "' ";
        DataTable dsDED = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        if (dsDED != null)
            if (dsDED.Rows.Count > 0)
                return Convert.ToDecimal((dsDED.Rows[0][0].ToString() == "") ? "" : dsDED.Rows[0][0].ToString());
            else
                return 0;
        return 0;
    }
    public string ConvertMMMToM(int M)
    {
        switch (M)
        {
            case 1:
                return "JAN";
            case 2:
                return "FEB";
            case 3:
                return "MAR";
            case 4:
                return "APR";
            case 5:
                return "MAY";
            case 6:
                return "JUN";
            case 7:
                return "JUL";
            case 8:
                return "AUG";
            case 9:
                return "SEP";
            case 10:
                return "OCT";
            case 11:
                return "NOV";
            case 12:
                return "DEC";
            default:
                return "";


        }
    }
    protected void btnReImport_OnClick(object sender, EventArgs e)
    {
        string sqlDED = "SELECT FM,TD,EXTRAOTHOURS,Allotment,CashAdvance,BondedStore,RadioTelecall,OtherDeductions,FbowPaidOnBoard,0 as UnionFee,TotalDeductions,Paid FROM DBO.PortageBillImported where EmpNumber='" + CrewNo + "' and Month=" + Month + " and year=" + Year + " and Vessel='" + GetVesselCodeById(Vesselid.ToString()) + "'";
        DataTable dt_DEDImported = Budget.getTable(sqlDED).Tables[0];
        if (dt_DEDImported.Rows.Count > 0)
        {
            DataRow Dr = dt_DEDImported.Rows[0];
            txtFD.Text = Dr["FM"].ToString();
            txtTD.Text = Dr["TD"].ToString();
            txtOT.Text = Dr["EXTRAOTHOURS"].ToString();

            txtAllotment.Text = Dr["Allotment"].ToString();
            txtCashAdvance.Text = Dr["CashAdvance"].ToString();
            txtBondStore.Text = Dr["BondedStore"].ToString();
            txtRadioAccount.Text = Dr["RadioTeleCall"].ToString();
            txtOtherDeductions.Text = Dr["OtherDeductions"].ToString();
            txtFBOWPaidOnBoard.Text = Dr["FbowPaidOnBoard"].ToString();
            CalculateWages();
        }
    }
    public string GetVesselCodeById(string VessID)
    {
        string sql = "select VesselCode from DBO.Vessel where VesselID=" + VessID + "";
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        if (dt != null)
        {
            if (dt.Rows.Count > 0)
                return dt.Rows[0][0].ToString();
        }
        return "";
    }
}
