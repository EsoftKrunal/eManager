using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ShipSoft.CrewManager.BusinessObjects;
using ShipSoft.CrewManager.BusinessLogicLayer;
using ShipSoft.CrewManager.Operational;
using Org.BouncyCastle.Asn1.X509;
using System.Diagnostics.Contracts;
using System.EnterpriseServices;

public partial class Modules_HRD_CrewPayroll_Payroll : System.Web.UI.Page
{
    Authority Auth;
    AuthenticationManager auth1;

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
        get { return (Convert.ToString(ViewState["CrewNo"])); }
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

    public decimal finaltotalEarnings
    {
        get { return Common.CastAsDecimal(ViewState["finaltotalEarnings"]); }
        set { ViewState["finaltotalEarnings"] = value; }
    }
    public decimal finaltotalDeductions
    {
        get { return Common.CastAsDecimal(ViewState["finaltotalDeductions"]); }
        set { ViewState["finaltotalDeductions"] = value; }
    }

    public int TravelPayCriteria
    {
        get { return Common.CastAsInt32(ViewState["TravelPayCriteria"]); }
        set { ViewState["TravelPayCriteria"] = value; }
    }

    public decimal BasicPay
    {
        get { return Common.CastAsDecimal(ViewState["BasicPay"]); }
        set { ViewState["BasicPay"] = value; }
    }

    public decimal OtherAmount
    {
        get { return Common.CastAsDecimal(ViewState["OtherAmount"]); }
        set { ViewState["OtherAmount"] = value; }
    }

    public int isFinalWages
    {
        get { return Common.CastAsInt32(ViewState["IsFinalWages"]); }
        set { ViewState["IsFinalWages"] = value; }
    }

    public string Currency
    {
        get { return (Convert.ToString(ViewState["Currency"])); }
        set { ViewState["Currency"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        SessionManager.SessionCheck_New();
        //-----------------------------
        auth1 = new AuthenticationManager(4, Common.CastAsInt32(Session["loginid"]), ObjectType.Page);

        Session["PageName"] = " - Portrage bill";
        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 4);
        if (chpageauth <= 0)
        {
            Response.Redirect("Dummy.aspx");
        }
        //*******************
        lbl_Message.Text = "";
        ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 1);
        OBJ.Invoke();
        Session["Authority"] = OBJ.Authority;
        Auth = OBJ.Authority;
        if (!Page.IsPostBack)
        {
            Session.Remove("vPayrollID");
            bindVesselNameddl();
            for (int i = DateTime.Today.Year; i >= 2009; i--)
                ddl_Year.Items.Add(new ListItem(i.ToString(), i.ToString()));
            //  btnPrint.Visible = Auth.isPrint;
            //-------------------
            int currentMonth = DateTime.Now.Month;
            int currentYear = DateTime.Now.Year;
            int PreviousMonth = 0;
            int previousYear = 0;
            if (currentMonth == 1)
            {
                PreviousMonth = 12;
                previousYear = currentYear - 1;
            }
            else
            {
                PreviousMonth = currentMonth - 1;
                previousYear = currentYear;
            }
            
            ddl_Vessel.SelectedIndex = 0;
            ddl_Month.SelectedValue = PreviousMonth.ToString();
            ddl_Year.SelectedValue = previousYear.ToString();
      
        }
    }
    protected void bindVesselNameddl()
    {
        //DataSet ds = Budget.getTable("select VesselID,VesselName as Name from dbo.Vessel where VesselStatusid<>2  ORDER BY VESSELNAME");
        DataSet ds = SearchSignOff.getVessel(Convert.ToInt32(Session["loginid"].ToString()));
        ddl_Vessel.DataSource = ds.Tables[0];
        ddl_Vessel.DataValueField = "VesselId";
        ddl_Vessel.DataTextField = "Name";
        ddl_Vessel.DataBind();
        ddl_Vessel.Items.Insert(0, new ListItem("<Select>", "0"));
    }

    protected void SearchData(int VesselId, int Month, int Year)
    {
        DataTable dt_Data = Common.Execute_Procedures_Select_ByQueryCMS("EXEC DBO.getContract_WagesHeader " + VesselId + "," + Year.ToString() + "," + Month.ToString());
        rptPersonal.DataSource = dt_Data;
        rptPersonal.DataBind();
        lblTotCrew.Text = " ( " + dt_Data.Rows.Count.ToString() + " ) Crew";
        Session["dt_Data"] = dt_Data;
        EnableControl("textlabel", false);
    }

    protected void btnHourG_Click(object sender, EventArgs e)
    {
        div1.Visible = true;
        int Index = Common.CastAsInt32(((ImageButton)sender).CommandArgument) - 1;
        DataRow dr_data = ((DataTable)Session["dt_Data"]).Rows[Index];
        //DataRow dr_calc = ((DataTable)Session["dt_Calc"]).Rows[Index];
        //DataRow dr_ded = ((DataTable)Session["dt_Ded"]).Rows[Index];
        //DataRow dr_Bal = ((DataTable)Session["dt_Bal"]).Rows[Index];
        int PayrollId = Common.CastAsInt32(dr_data["PayrollId"]);
        
        isFinalWages = Common.CastAsInt32(dr_data["IsFinalWages"]);
        Currency = dr_data["Currency"].ToString();
        if (PayrollId <= 0)
        {
            
            //string sql = "Select ws.Currency from CrewContractHeader_WageScaleRank cc with(nolock) Inner Join WageScaleRank wsr  with(nolock) on cc.WageScaleRankId = wsr.WageScaleRankId Inner Join WageScale ws with(nolock) on wsr.WageScaleId = ws.WageScaleId where cc.ContractId = " + Convert.ToInt32(dr_data["CONTRACTID"]);
            //DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
            //if (dt.Rows.Count > 0)
            //{
            //    Currency = dt.Rows[0]["Currency"].ToString();
            //}
            Common.Set_Procedures("sp_InsertUpdateCrewPortageBillHeader");
            Common.Set_ParameterLength(17);
            Common.Set_Parameters(
                    new MyParameter("@PAYROLLID", dr_data["PayrollId"]),
                    new MyParameter("@VESSELID", ddl_Vessel.SelectedValue),
                    new MyParameter("@PAYMONTH", ddl_Month.SelectedValue),
                    new MyParameter("@PAYYEAR", ddl_Year.SelectedValue),
                    new MyParameter("@CREWNUMBER", dr_data["CREWNUMBER"]),
                    new MyParameter("@CREWNAME", dr_data["CREWNAME"]),
                    new MyParameter("@RANK", dr_data["RANKCODE"]),
                    new MyParameter("@FD", dr_data["STARTDAY"]),
                    new MyParameter("@TD", dr_data["ENDDAY"]),
                    new MyParameter("@ExtraOTRate", dr_data["ExtraOTRate"]),
                    new MyParameter("@CONTRACTID", dr_data["CONTRACTID"]),
                    new MyParameter("@UNIONFEE", dr_data["UNIONFEE"]),
                    new MyParameter("@PreMonthBal", dr_data["PreMonthBal"]),
                    //new MyParameter("@OtherAmount",OtherAmount.ToString()),
                    new MyParameter("@VERIFIED", "0"),
                    new MyParameter("@VERIFIEDBY", ""),
                    new MyParameter("@VERIFIEDON", DateTime.Today.ToString("dd/MMM/yyyy")),
                    new MyParameter("@AUTOSAVED", "1")
                    //,new MyParameter("@Currency", Currency)
                    );

            DataSet ds = new DataSet();
            bool res = Common.Execute_Procedures_IUD_CMS(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                PayrollId = Common.CastAsInt32(ds.Tables[0].Rows[0][0].ToString());
                dr_data["PayrollId"] = PayrollId.ToString();
            }
        }
        Session.Add("vPayrollID", PayrollId.ToString());
        int VesselId, Month, Year;
        VesselId = Convert.ToInt32(ddl_Vessel.SelectedValue);
        Month = Convert.ToInt32(ddl_Month.SelectedValue);
        Year = Convert.ToInt32(ddl_Year.SelectedValue);
        ShowData(PayrollId);
        //btnModify.Visible = true;    
        //btn_Save.Visible = false;
        //btnCancel.Visible = false;
        EnableControl("textlabel", false);
        ShowAttachment();
        //rptPayollDoc.DataSource = Session["dt_Data"];
        //rptPayollDoc.DataBind();
        // SearchData(VesselId, Month, Year);

        // ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "OpenPaySlipReport(" + PayrollId + ")", true);
    }
    public void ShowData(int Payrollno)
    {
        string StrMonthYear = "";
        //string sql = "SELECT * FROM DBO.CrewPortageBill WHERE VesselId=" + Vesselid + " AND PayMonth=" + Month + " AND PayYear=" + Year + " AND CrewNumber='" + CrewNo + "'";
        string sql = "Select cpb.*,Isnull(replace(convert(varchar,ch.SignOnDate ,106),' ','-'),'-')  As SignOnDate, Isnull(replace(convert(varchar,ch.SingOffDate ,106),' ','-'),'-') As  SingOffDate, Isnull(replace(convert(varchar,cch.StartDate ,106),' ','-'),'-')  As  ContractStartDate, Isnull(replace(convert(varchar,cch.EndDate ,106),' ','-'),'-')  As  ContractEndDate, Isnull((Select PortName + ' (' + c.CountryName + ') ' from Port p with(nolock) inner join Country c with(nolock) on p.CountryId = c.CountryId where p.PortId = ch.SignOnPort),'-') As SignonPort, Isnull((Select PortName + ' (' + c.CountryName + ') ' from Port p with(nolock) inner join Country c with(nolock) on p.CountryId = c.CountryId where p.PortId = ch.SingOffPort),'-') As SingOffPort,V.VesselName As VesselName,(select FirstName + ' ' + LastName from UserLogin U where U.LoginID = CPb.Verifiedby) As VerifiedbyText, replace(convert(varchar, cpb.verifiedOn, 106), ' ', '-') As verifiedOnText, R.RankName As RankName,ISNULL(AUTOSAVED,0) As AUTOSAVED, Month(ch.SignOnDate) As MonthOfSignonDate, ISNUll(TravelPayCriteria,0) As  TravelPayCriteria, Case when Isnull(cpb.OtherAmount,0) > 0 then Isnull(cpb.OtherAmount,0) Else Isnull(cch.OtherAmount,0) END As  ExtraPayment,(Select Case when LTRIM(RTRIM(f.FlagStateName)) = 'INDIA' Then 'INR' ELSE 'USD'  END As Currency from FlagState f with(nolock) where f.FlagStateId = V.FlagStateId) As Currency from CrewPortageBillHeader cpb with(nolock) Inner join  CrewPersonalDetails cpd with(nolock) on cpb.CrewNumber = cpd.CrewNumber Inner join Vessel V with(nolock) On V.VesselID = cpb.VesselID Left outer join get_Crew_History ch with(nolock)  on cpd.CrewId = ch.CrewId and cpb.ContractID = ch.ContractId left outer join CrewContractHeader cch with(nolock) on cpd.CrewId = cch.CrewId and cpb.ContractID = cch.ContractId left join Rank R with(nolock) on LTRIM(RTRIM(cpb.Rank)) = LTRIM(RTRIM(R.RankCode)) where cpb.PayrollId=" + Payrollno + "";
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
                hdnCrewId.Value = CrewNo;
                Currency = Dr["Currency"].ToString(); 
                //ShowLastMonthData(Vesselid, Month, Year, CrewNo);

                StrMonthYear = ConvertMMMToM(Month) + " " + Year.ToString();

                //  lblThisMonthWages.Text = ConvertMMMToM(Month) + " " + Year.ToString();
                //  lblLastMonthWages.Text = Convert.ToDateTime("1-" + ConvertMMMToM(Month) + "-" + Year.ToString()).AddMonths(-1).ToString("MMM yyyy").ToUpper();

                lblWagesForDuration.Text = StrMonthYear;

                //if (Month == 1)
                //{
                //    lblBowFromDuration.Text = ConvertMMMToM(12) + " " + Convert.ToString(Year - 1);
                //}
                //else
                //    lblBowFromDuration.Text = ConvertMMMToM(Month - 1) + " " + Year.ToString();

                // Master Date
                lblName.Text = Dr["CREWNAME"].ToString();
                lblrank.Text = Dr["RankName"].ToString();
                txtFD.Text = Dr["FD"].ToString();
                txtTD.Text = Dr["TD"].ToString();
                txtExtrOTRate.Text = Dr["ExtraOTRate"].ToString();
                txtExtraOT.Text = Dr["ExtraOTdays"].ToString();
                lblVesselName.Text = Dr["VesselName"].ToString();
                lblCrewNo.Text = Dr["CREWNUMBER"].ToString();
                lblSignOnDt.Text = Dr["SignOnDate"].ToString();
                if (Dr["SingOffDate"].ToString() != "")
                {
                    lblSignOffDt.Text = Dr["SingOffDate"].ToString();
                }
                if (Dr["SingOffPort"].ToString() != "")
                {
                    lblSignOffPort.Text = Dr["SingOffPort"].ToString();
                }
                if (Dr["SignonPort"].ToString() != "")
                {
                    lblSignOnPort.Text = Dr["SignonPort"].ToString();
                }
                if (Dr["ContractStartDate"].ToString() != "")
                {
                    lblContractStartDt.Text = Dr["ContractStartDate"].ToString();
                }
                if (Dr["ContractEndDate"].ToString() != "")
                {
                    lblContractEndDt.Text = Dr["ContractEndDate"].ToString();
                }
                TravelPayCriteria = Convert.ToInt32(Dr["TravelPayCriteria"]);
               
                //if (Convert.ToInt32(ddl_Month.SelectedValue) == Convert.ToInt32(Dr["MonthOfSignonDate"]))
                //{
                if (TravelPayCriteria == 3)
                    {
                        divTavelPay.Visible = true;
                    }
                    else
                    {
                        divTavelPay.Visible = false;
                    }
                    trtravelPay.Visible = true;
                    lblTotalTraveldays.Visible = true;
                    txtTotalTraveldays.Visible = true;
                //}
                //else
                //{
                //    divTavelPay.Visible = false;
                //}
                // lblWagesForValue.Text = lblTotalEarnings.Text;
                OtherAmount = Common.CastAsDecimal(Dr["ExtraPayment"]);
                hdnOtherAmount.Value = Dr["ExtraPayment"].ToString();
                lblBowFromValue.Text = Dr["PrevMonBal"].ToString();
                TotWagesPayAbleOnBoard.Text = Common.CastAsDecimal(Common.CastAsDecimal(Dr["CurMonBal"]) + Common.CastAsDecimal(Dr["PrevMonBal"])).ToString();
               // txtPaidOffWages.Text = Dr["Paid"].ToString();
                lblBalanceOfWages.Text = Dr["BalanceOfWages"].ToString();
                lbltravelPayAmount.Text = Common.CastAsDecimal(Common.CastAsDecimal(Dr["TravelPayAmount"])).ToString();
                txtTotalTraveldays.Text = Dr["TravelPayDays"].ToString();
                if (Convert.ToInt32(Dr["AUTOSAVED"]) == 1)
                {
                    getEarningDeductionsCrewPortageBillHeaderDetails(PayRollID, CONTRACTID,2);
                }
                else
                {
                    getEarningDeductionsCrewPortageBillHeaderDetails(PayRollID, CONTRACTID, 1);
                }
                getTotalEarnings();
                if (Dr["Verified"].ToString() == "True")
                {
                    lblVerifiedByOn.Text = Dr["VerifiedbyText"].ToString() + " / " + Dr["verifiedOnText"].ToString();
                    txtRemark.Text = Dr["Remarks"].ToString();
                    btnVerify.Visible = false;
                    trVerified.Visible = true;
                    btn_Save.Visible = false;
                    btnModify.Visible = false;
                    imgUpdateBowFrom.Visible = false;
                    btnPrint.Visible = true;
                    if (isFinalWages == 1)
                    {
                        btnFinalWages.Visible = true;
                    }
                    else
                    {
                        btnFinalWages.Visible = false;
                    }
                    
                }
                else
                {
                    lblVerifiedByOn.Text = "";
                    txtRemark.Text = "";
                    btnVerify.Visible = true;
                    trVerified.Visible = false ;
                    btnModify.Visible = true;
                    if (Common.CastAsInt32(Session["loginid"]) == 1)
                    {
                        imgUpdateBowFrom.Visible = true && Auth.isVerify;
                    }
                    
                    btnPrint.Visible = false;
                    btnFinalWages.Visible = false;
                }
                //// Button Vesibility

                //string sqlVesibility = "SELECT * FROM DBO.CrewPortageBillClosure WHERE VessId=" + Vesselid + " AND [Month]=" + Month + " AND [Year]=" + Year;
                //DataSet DSVesibility = Budget.getTable(sqlVesibility);
                //if (DSVesibility != null)
                //{
                //    if (DSVesibility.Tables[0].Rows.Count > 0)
                //    {
                //        btn_Save.Visible = false;
                //        imgUpdateBowFrom.Visible = false;
                //    }
                //    else
                //    {
                //        btn_Save.Visible = true && Auth.isEdit;
                //        if (Common.CastAsInt32(Session["loginid"]) == 1)
                //        {
                //            imgUpdateBowFrom.Visible = true && Auth.isEdit;
                //        }
                //    }
                //}
            }
        }
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
    protected void btn_search_Click(object sender, EventArgs e)
    {
        div1.Visible = false;
        int VesselId, Month, Year;
        VesselId = Convert.ToInt32(ddl_Vessel.SelectedValue);
        Month = Convert.ToInt32(ddl_Month.SelectedValue);
        Year = Convert.ToInt32(ddl_Year.SelectedValue);
        Common.Execute_Procedures_Select_ByQueryCMS("DELETE FROM CREWPORTAGEBILLHEADERDetails WHERE PayrollId in (Select PayrollId FROM CREWPORTAGEBILLHEADER WHERE VESSELID=" + VesselId.ToString() + " AND PAYMONTH=" + Month.ToString() + " AND PAYYEAR=" + Year.ToString() + " AND AUTOSAVED=1)");
        Common.Execute_Procedures_Select_ByQueryCMS("DELETE FROM CREWPORTAGEBILLHEADER WHERE VESSELID=" + VesselId.ToString() + " AND PAYMONTH=" + Month.ToString() + " AND PAYYEAR=" + Year.ToString() + " AND AUTOSAVED=1");

        SearchData(VesselId, Month, Year);
        //string currency = string.Empty;
        //string sql = "Select Case when LTRIM(RTRIM(f.FlagStateName)) = 'INDIA' Then 'INR' ELSE 'USD'  END As Currency  from Vessel v with(nolock) Inner Join FlagState f with(nolock) on v.FlagStateId = f.FlagStateId where v.VesselId = " + VesselId;

        //DataTable dtcurrency = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        //if (dtcurrency != null && dtcurrency.Rows.Count > 0)
        //{
        //    currency = dtcurrency.Rows[0]["Currency"].ToString();
        //}
    }

    public string FormatCurr(object _in)
    {
        return string.Format("{0:0.00}", _in);
    }

    protected void btnModify_Click(object sender, EventArgs e)
    {
        try
        {
            bool isEarningflag = false;
            bool isdeductflag = false;

            if (hdnCrewId.Value == "")
            {
                lbl_Message.Text = "Please select Crew name."; return;
            }
            //txtFD.CssClass = "ctltext";
            //txtTD.CssClass = "ctltext";
            //txtExtraOT.CssClass = "ctltext";
            //txtExtrOTRate.CssClass = "ctltext";
            //txtTotalTraveldays.CssClass = "ctltext";
            //txtTravelPay.CssClass = "ctltext";
            //txtFD.Enabled = true;
            //txtTD.Enabled = true;
            //txtExtraOT.Enabled = true;
            //txtExtrOTRate.Enabled = true;
            //txtTotalTraveldays.Enabled = true;
            //txtTravelPay.Enabled = true;
            EnableControl("ctltext", true);
            foreach (RepeaterItem ri in rptEaringWages.Items)
            {
                Label Componenttype = (Label)ri.FindControl("lblComponentType");
                if (Componenttype.Text.Trim() != "Earning")
                {
                    foreach (Control c in ri.Controls)
                    {
                        isEarningflag = true;
                        if (c.GetType() == typeof(TextBox))
                        {
                            ((TextBox)c).Enabled = true;
                            ((TextBox)c).CssClass = "ctltext";
                        }
                    }
                }
            }

            foreach (RepeaterItem ri in rptDeductionWages.Items)
            {
                foreach (Control c in ri.Controls)
                {

                    isdeductflag = true;
                    if (c.GetType() == typeof(TextBox))
                    {
                        ((TextBox)c).Enabled = true;
                        ((TextBox)c).CssClass = "ctltext";
                    }
                }
            }
            if (!isEarningflag)
            {
                lblEarningWages_Message.Text = "No Record found !";
                lblEarningWages_Message.Visible = true;
            }
            if (!isdeductflag)
            {
                lblDeductionWage_Message.Text = "No Record found !";
                lblDeductionWage_Message.Visible = true;
            }
            btn_Save.Visible = true;
            btnCancel.Visible = true;
        }
        catch (Exception ex)
        {
            lbl_Message.Text = ex.Message.ToString();
        }
    }

    //protected void BindEarningDeductionWages(string rankCode, string componentType)
    //{
    //    DataTable dt = WagesMaster.WageComponentsDetails(Common.CastAsInt32(dp_WSname.SelectedValue), 0, Common.CastAsInt32(txt_Seniority.Text), rankCode);
    //    DataView dv = new DataView(dt);
    //    dv.RowFilter = " ComponentType = '" + componentType + "'";

    //    DataTable dtEarningComponent = dv.ToTable();

    //    if (dtEarningComponent.Rows.Count > 0)
    //    {
    //        rptEaringWages.Visible = true;
    //        rptEaringWages.DataSource = dtEarningComponent;
    //        rptEaringWages.DataBind();
    //    }
    //    else
    //    {
    //        lblEarningWages_Message.Text = "No record found !";
    //        rptEaringWages.DataSource = null;
    //        rptEaringWages.DataBind();
    //    }
    //}

    //protected void BindDeductionWages(string rankCode, string componentType)
    //{
    //    DataTable dt = WagesMaster.WageComponentsDetails(Common.CastAsInt32(dp_WSname.SelectedValue), 0, Common.CastAsInt32(txt_Seniority.Text), rankCode);
    //    DataView dv = new DataView(dt);
    //    dv.RowFilter = " ComponentType = '" + componentType + "'";

    //    DataTable dtDeductionComponent = dv.ToTable();

    //    if (dtDeductionComponent.Rows.Count > 0)
    //    {
    //        rptDeductionWages.Visible = true;
    //        rptDeductionWages.DataSource = dtDeductionComponent;
    //        rptDeductionWages.DataBind();
    //    }
    //    else
    //    {
    //        lblDeductionWage_Message.Text = "No record found !";
    //        rptDeductionWages.DataSource = null;
    //        rptDeductionWages.DataBind();
    //    }
    //}

    protected void getEarningDeductionsCrewPortageBillHeaderDetails(int payrollid, int contractid, int flag)
    {

        DataTable dt = WagesMaster.CrewPortCallBillHeaderDetails(Common.CastAsInt32(payrollid),  Common.CastAsInt32(contractid), flag);
        DataView dvEarnings = new DataView(dt);
        DataView dvDeducts = new DataView(dt);
        dvEarnings.RowFilter = " ComponentType = 'Earning' OR ComponentType = 'Other Earning' ";
        dvDeducts.RowFilter = " ComponentType = 'Deduction' OR ComponentType = 'Other Deduction'";

        DataTable dtEarningComponent = dvEarnings.ToTable();
        DataTable dtDeductionComponent = dvDeducts.ToTable();
        if (dtEarningComponent.Rows.Count > 0)
        {
            rptEaringWages.Visible = true;
            rptEaringWages.DataSource = dtEarningComponent;
            rptEaringWages.DataBind();
            Control HeaderTemplate = rptEaringWages.Controls[0].Controls[0];
            Label lblEarnHeader = HeaderTemplate.FindControl("lblEarnCurrency") as Label;
            lblEarnHeader.Text = Currency;
            lblEarningWages_Message.Visible = false; 
        }
        else
        {
            lblEarningWages_Message.Visible = true;
            lblEarningWages_Message.Text = "No record found !";
            rptEaringWages.DataSource = null;
            rptEaringWages.DataBind();
        }

        if (dtDeductionComponent.Rows.Count > 0)
        {
            rptDeductionWages.Visible = true;
            rptDeductionWages.DataSource = dtDeductionComponent;
            rptDeductionWages.DataBind();
            Control HeaderTemplate = rptDeductionWages.Controls[0].Controls[0];
            Label lblDeductHeader = HeaderTemplate.FindControl("lblDeductCurrency") as Label;
            lblDeductHeader.Text = Currency;
            lblDeductionWage_Message.Visible = false;
        }
        else
        {
            lblDeductionWage_Message.Visible = true;
            lblDeductionWage_Message.Text = "No record found !";
            rptDeductionWages.DataSource = null;
            rptDeductionWages.DataBind();
        }
    }

    //public void CalculateWages()
    //{
    //    lblTotalEarnings.Text =
    //            Common.CastAsDecimal(
    //            Common.CastAsDecimal(txtBasicPay.Text.Trim()) +
    //            Common.CastAsDecimal(txtFixedOT.Text.Trim()) +
    //            Common.CastAsDecimal(txtBonus.Text.Trim()) +
    //            Common.CastAsDecimal(txtLeavePay.Text.Trim()) +
    //            Common.CastAsDecimal(txtSeniorityAllowance.Text.Trim()) +
    //            Common.CastAsDecimal(txtTradeAllowance.Text.Trim()) +
    //            Common.CastAsDecimal(txtSuperiorCertificateAllowance.Text.Trim()) +
    //            Common.CastAsDecimal(txtCommAllowance.Text.Trim()) +
    //            Common.CastAsDecimal(txtSubAllowance.Text.Trim()) +
    //            Common.CastAsDecimal(txtUAllowance.Text.Trim()) +
    //            Common.CastAsDecimal(txtGmdss.Text.Trim()) +
    //            Common.CastAsDecimal(txtAdditioinalOT.Text.Trim()) +
    //            Common.CastAsDecimal(txtMTMAllowance.Text.Trim()) +
    //            Common.CastAsDecimal(txtOtherPayments.Text.Trim()) +
    //            Common.CastAsDecimal(txtAdditionalPayment.Text.Trim())
    //            ).ToString();
    //    lblWagesForValue.Text = lblTotalEarnings.Text;
    //    lblTotalDeductions.Text =
    //            Common.CastAsDecimal(
    //            Common.CastAsDecimal(txtAllotment.Text.Trim()) +
    //            Common.CastAsDecimal(txtCashAdvance.Text.Trim()) +
    //            Common.CastAsDecimal(txtBondStore.Text.Trim()) +
    //            Common.CastAsDecimal(txtRadioAccount.Text.Trim()) +
    //            Common.CastAsDecimal(txtOtherDeductions.Text.Trim()) +
    //            Common.CastAsDecimal(txtUnionFee.Text.Trim()) +
    //            Common.CastAsDecimal(txtFBOWPaidOnBoard.Text.Trim()) +
    //            Common.CastAsDecimal(txtPaidOffWages.Text.Trim())
    //            ).ToString();
    //    TotDedThisMonth.Text = lblTotalDeductions.Text;

    //    TotWagesPayAbleOnBoard.Text =
    //            Common.CastAsDecimal(
    //            Common.CastAsDecimal(lblTotalEarnings.Text) -
    //            Common.CastAsDecimal(lblTotalDeductions.Text) +
    //            Common.CastAsDecimal(lblBowFromValue.Text)
    //            ).ToString();
    //    lblBalanceOfWages.Text =
    //            Common.CastAsDecimal(
    //            Common.CastAsDecimal(TotWagesPayAbleOnBoard.Text)

    //            ).ToString();

    //}

    protected void ReCalCulateWages(object sender, EventArgs e)
    {
        DataTable dt = WagesMaster.CrewPortCallBillHeaderDetails(PayRollID, CONTRACTID, 2);
        if (dt != null)
        {
            if (dt.Rows.Count > 0)
            {
                DataRow Dr = dt.Rows[0];
                int DiffDays = Common.CastAsInt32(txtTD.Text) - Common.CastAsInt32(txtFD.Text) + 1;
                int MonthDays = 30;
                decimal OtHrs = Common.CastAsDecimal(txtExtraOT.Text);


                if (DateTime.DaysInMonth(Year, Month) == 31)
                {
                    if (DiffDays == 30 || DiffDays == 31)
                        MonthDays = DiffDays;
                }
                else if (DateTime.DaysInMonth(Year, Month) == 29 || DateTime.DaysInMonth(Year, Month) == 28)
                {
                    MonthDays = DateTime.DaysInMonth(Year, Month);
                }

                //if (DateTime.DaysInMonth(Year, Month) == 29 || DateTime.DaysInMonth(Year, Month) == 28)
                //{
                //    if (DiffDays == 28 || DiffDays == 29)
                //        MonthDays = DiffDays;
                //}

                DataTable dtReCalculate = new DataTable();
                dtReCalculate.Columns.AddRange(new DataColumn[6] { 
                            new DataColumn("WageScaleComponentId", typeof(int)),
                            new DataColumn("ComponentName", typeof(string)),
                            new DataColumn("WageScaleComponentvalue",typeof(Double)),
                            new DataColumn("ComponentType",typeof(string)),
                            new DataColumn("CountOfCompnentType",typeof(int)),
                            new DataColumn("IsFirstRow",typeof(string))});
                int componentid, CountOfCompnentType;
                decimal WageScaleComponentvalue;
                string ComponentName, ComponentType, IsFirstRow;

                foreach (DataRow item in dt.Rows)
                {
                    
                    if (item["ComponentType"].ToString() == "Earning" || item["ComponentType"].ToString() == "Deduction")
                    {
                         componentid = Convert.ToInt32(item["WageScaleComponentId"].ToString());
                         ComponentName = item["ComponentName"].ToString();
                         WageScaleComponentvalue = Math.Round((DiffDays * Common.CastAsDecimal(item["WageScaleComponentvalue"])) / MonthDays, 2);
                         ComponentType = item["ComponentType"].ToString();
                         CountOfCompnentType = Convert.ToInt32(item["CountOfCompnentType"].ToString());
                         IsFirstRow = item["IsFirstRow"].ToString();
                    }
                    else
                    {
                        componentid = Convert.ToInt32(item["WageScaleComponentId"].ToString());
                        ComponentName = item["ComponentName"].ToString();
                        WageScaleComponentvalue = Math.Round(Common.CastAsDecimal(item["WageScaleComponentvalue"]), 2);
                        ComponentType = item["ComponentType"].ToString();
                        CountOfCompnentType = Convert.ToInt32(item["CountOfCompnentType"].ToString());
                        IsFirstRow = item["IsFirstRow"].ToString();
                    }
                   
                    dtReCalculate.Rows.Add(componentid, ComponentName, WageScaleComponentvalue, ComponentType, CountOfCompnentType, IsFirstRow);
                }

                if (dtReCalculate.Rows.Count > 0)
                {
                    DataView dvEarnings = new DataView(dtReCalculate);
                    DataView dvDeducts = new DataView(dtReCalculate);
                    dvEarnings.RowFilter = " ComponentType = 'Earning' OR ComponentType = 'Other Earning' ";
                    dvDeducts.RowFilter = " ComponentType = 'Deduction' OR ComponentType = 'Other Deduction'";
                    if (dvEarnings.Count > 0)
                    {
                        rptEaringWages.Visible = true;
                        rptEaringWages.DataSource = dvEarnings;
                        rptEaringWages.DataBind();
                        lblEarningWages_Message.Visible = false;
                        foreach (RepeaterItem ri in rptEaringWages.Items)
                        {
                            Label Componenttype = (Label)ri.FindControl("lblComponentType");
                            if (Componenttype.Text.Trim() != "Earning")
                            {
                                foreach (Control c in ri.Controls)
                                {
                                    if (c.GetType() == typeof(TextBox))
                                    {
                                        ((TextBox)c).Enabled = true;
                                        ((TextBox)c).CssClass = "ctltext";
                                    }
                                }
                            }
                        }

                       
                    }
                    else
                    {
                        rptEaringWages.Visible = false;
                        lblEarningWages_Message.Text = "No record found !";
                        lblEarningWages_Message.Visible = true;
                        rptEaringWages.DataSource = null;
                        rptEaringWages.DataBind();
                    }
                    if (dvDeducts.Count > 0)
                    {
                        rptDeductionWages.Visible = true;
                        rptDeductionWages.DataSource = dvDeducts;
                        rptDeductionWages.DataBind();
                        lblDeductionWage_Message.Visible = false;
                        foreach (RepeaterItem ri in rptDeductionWages.Items)
                        {
                            foreach (Control c in ri.Controls)
                            {
                                if (c.GetType() == typeof(TextBox))
                                {
                                    ((TextBox)c).Enabled = true;
                                    ((TextBox)c).CssClass = "ctltext";
                                }
                            }
                        }
                    }
                    else
                    {
                        rptDeductionWages.Visible = false;
                        lblDeductionWage_Message.Text = "No record found !";
                        lblDeductionWage_Message.Visible = true;
                        rptDeductionWages.DataSource = null;
                        rptDeductionWages.DataBind();
                    }
                   
                }
                else
                {
                    lblEarningWages_Message.Text = "No record found !";
                    lblEarningWages_Message.Visible = true;
                    rptEaringWages.DataSource = null;
                    rptEaringWages.DataBind();
                    lblDeductionWage_Message.Text = "No record found !";
                    lblDeductionWage_Message.Visible = true;
                    rptDeductionWages.DataSource = null;
                    rptDeductionWages.DataBind();
                }
            }
        }
        
        //CalculateWages();
        getTotalEarnings();                                                              
    }                                                                                                         
    protected void TxtComponentAmount_TextChanged(object sender, EventArgs e)
    {
        try
        {
            getTotalEarnings();
        }
        catch (Exception ex)
        { ShowMessage(ex.Message, true); }

    }

    protected void TxtdeductComponentAmount_TextChanged(object sender, EventArgs e)
    {
        try
        {
            getTotalEarnings();
        }
        catch (Exception ex)
        { ShowMessage(ex.Message, true); }
    }

    protected void getTotalEarnings()
        {
        //divEarnings.Visible = true;
        //txtTotalEarnings.Text = "0.00";
        Decimal totalEarning = 0;
        Decimal totalDeductions = 0;
        Decimal total = 0;
        Decimal totalExtraOTAmount = 0;
        Decimal travelPayAmount = 0;
       // Decimal basicWages = 0;
        finaltotalEarnings = 0;
        finaltotalDeductions = 0;

        string sql1 = "Select  Isnull(cc.Amount,0) As BasicAmount from CrewContractDetails cc with(nolock)  inner join WageScaleComponents w with(nolock) on cc.WagwScaleComponentId = w.WageScaleComponentId where cc.ContractId = "+ CONTRACTID + " and w.ComponentType  in ('E') and W.ComponentName Like 'Basic'";
        DataTable DT = Common.Execute_Procedures_Select_ByQueryCMS(sql1);
        if (DT.Rows.Count > 0)
        {
            BasicPay = Common.CastAsDecimal(DT.Rows[0]["BasicAmount"].ToString());
        }
        foreach (RepeaterItem item in rptEaringWages.Items)
        {
            TextBox txtComponentValue = item.FindControl("txtComponentAmount") as TextBox;
            Label lblComponentType = item.FindControl("lblComponentType") as Label;
            Label lblComponentName = item.FindControl("lblComponentName") as Label;
            //if (! string.IsNullOrWhiteSpace(lblComponentType.Text) && lblComponentType.Text.Trim().ToUpper() == "EARNING" && lblComponentName.Text.Trim().ToUpper() == "BASIC")
            //{
            //    if (!string.IsNullOrWhiteSpace(txtComponentValue.Text))
            //    {
            //        BasicPay = Decimal.Parse(txtComponentValue.Text);
            //    }
            //}
            if (!string.IsNullOrWhiteSpace(txtComponentValue.Text))
            {
                if (totalEarning == 0)
                {
                    totalEarning = Decimal.Parse(txtComponentValue.Text);
                }
                else
                {
                    totalEarning = totalEarning + Decimal.Parse(txtComponentValue.Text);
                }
            }
        }

        foreach (RepeaterItem item in rptDeductionWages.Items)
        {
            TextBox txtdeductionComponentValue = item.FindControl("txtdeductComponentAmount") as TextBox;
            if (!string.IsNullOrWhiteSpace(txtdeductionComponentValue.Text))
            {
                if (totalDeductions == 0)
                {
                    totalDeductions = Decimal.Parse(txtdeductionComponentValue.Text);
                }
                else
                {
                    totalDeductions = totalDeductions + Decimal.Parse(txtdeductionComponentValue.Text);
                }
            }
        }
        if (!string.IsNullOrWhiteSpace(txtExtraOT.Text) && !string.IsNullOrWhiteSpace(txtExtrOTRate.Text))
        {
            totalExtraOTAmount = Decimal.Parse(txtExtraOT.Text) * Decimal.Parse(txtExtrOTRate.Text);
        }
        if (! string.IsNullOrWhiteSpace(txtTotalTraveldays.Text))
        {
            
            if (TravelPayCriteria == 1)
            {
                travelPayAmount = Common.CastAsDecimal(txtTotalTraveldays.Text) * ((totalEarning + totalExtraOTAmount - totalDeductions) / 30);
            }
            if (TravelPayCriteria == 2)
            {
                travelPayAmount = Common.CastAsDecimal(txtTotalTraveldays.Text) * ((BasicPay) /30);
            }
            if (TravelPayCriteria == 3 && !string.IsNullOrWhiteSpace(txtTravelPay.Text))
            {
                travelPayAmount = Common.CastAsDecimal(txtTotalTraveldays.Text) * (Common.CastAsDecimal(txtTotalTraveldays.Text));
            }

            lbltravelPayAmount.Text = FormatCurr(travelPayAmount);
        }


        total = totalEarning + totalExtraOTAmount - totalDeductions + Common.CastAsDecimal(lblBowFromValue.Text) + travelPayAmount;
        finaltotalEarnings = totalEarning + totalExtraOTAmount + travelPayAmount;
        finaltotalDeductions = totalDeductions;
        txtExtraOTAmount.Text = FormatCurr(totalExtraOTAmount);
        TotWagesPayAbleOnBoard.Text = FormatCurr(total);
        lblWagesForValue.Text = FormatCurr(totalEarning + totalExtraOTAmount + travelPayAmount);
        TotDedThisMonth.Text = FormatCurr(totalDeductions);
        lblBalanceOfWages.Text = Common.CastAsDecimal(Common.CastAsDecimal(TotWagesPayAbleOnBoard.Text)).ToString();
    }

    public void ShowMessage(string Message, bool Error)
    {
        lbl_Message.Text = Message;
        lbl_Message.ForeColor = (Error) ? System.Drawing.Color.Red : System.Drawing.Color.Green;
    }

    protected void Save_Click(object sender, EventArgs e)
    {
        try
        {
            if (PayRollID <= 0)
            {
                ShowMessage("Please select crew name.", true);
                return;
            }
            if (PayRollID > 0)
            {
                savePayroll();
               // ShowMessage("Record Saved Successfully.", false);
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "ddd", "alert('Record Saved Successfully.')", true);
            }

        }
        catch (Exception ex) {
            // ShowMessage("Record Can't Saved. Error :" + ex.Message, true); sss
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "ddd", "alert('Record Can't Saved. Error :" + ex.Message + "')", true);
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        div1.Visible = false;
        PayRollID = 0;
        CONTRACTID = 0;

    }



    //protected void rptEaringWages_ItemDataBound(object sender, RepeaterItemEventArgs e)
    //{
    //    int RowSpan = 2;
    //    var curre

    //    if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
    //    {
    //        Label lblSlase = e.Item.FindControl("lblComponentType") as Label;

    //    }
    //    //    for (int i = grd_popup_details.Count - 2; i >= 0; i--)
    //    //{
    //    //    GridViewRow currRow = grd_popup_details.Rows[i];
    //    //    GridViewRow prevRow = grd_popup_details.Rows[i + 1];
    //    //    if (currRow.Cells[0].Text == prevRow.Cells[0].Text)
    //    //    {
    //    //        currRow.Cells[0].RowSpan = RowSpan;
    //    //        prevRow.Cells[0].Visible = false;
    //    //        RowSpan += 1;
    //    //    }
    //    //    else
    //    //    {
    //    //        RowSpan = 2;
    //    //    }
    //    //}
    //}

    protected void imgUpdateBowFrom_OnClick(object sender, ImageClickEventArgs e)
    {

        if (imgUpdateBowFrom.ImageUrl == "~/Modules/HRD/Images/check.png")
        {
            lblBowFromValue.Visible = true;
            txtBowFromValue.Visible = false;
            imgUpdateBowFrom.ImageUrl = "~/Modules/HRD/Images/icon_pencil.gif";

            string sql = "Update DBO.CrewPortageBillHeader set PrevMonBal=" + Common.CastAsDecimal(txtBowFromValue.Text) + " where PayrollID=" + PayRollID + "";
            DataSet DS = Budget.getTable(sql);
            ShowData(PayRollID);
            ShowAttachment();
        }
        else
        {
            lblBowFromValue.Visible = false;
            txtBowFromValue.Visible = true;
            txtBowFromValue.Text = lblBowFromValue.Text;
            imgUpdateBowFrom.ImageUrl = "~/Modules/HRD/Images/check.png";
        }
        getTotalEarnings();
    }

    protected void btnVerify_OnClick(object sender, EventArgs e)
    {
        try
        {
            if (txtRemark.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "ddd", "alert('Please enter remarks.')", true);
                txtRemark.Focus(); return;
            }
            savePayroll();
            updateBowforNextMonth(Convert.ToInt32(ddl_Vessel.SelectedValue), Convert.ToInt32(ddl_Month.SelectedValue), Convert.ToInt32(ddl_Year.SelectedValue), Common.CastAsDecimal(lblBalanceOfWages.Text), PayRollID);
            string sql = "update CrewPortageBillHeader set Verified=1 ,Verifiedby=" + Common.CastAsInt32(Session["loginid"]) + ",Remarks='" + txtRemark.Text.Trim().Replace("'", "`") + "',VerifiedOn=getdate() where PayrollId=" + PayRollID + "  select -1";
            DataSet Dt = Budget.getTable(sql);
            if (Dt != null)
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "ddd", "alert('Record update successully.')", true);
                ShowData(PayRollID);
                ShowAttachment();
                btn_Save.Visible = false;
                btnModify.Visible = false;
                imgUpdateBowFrom.Visible = false;
                btnPrint.Visible = true;
                if (isFinalWages == 1)
                {
                    btnFinalWages.Visible = true;
                }
                else
                {
                    btnFinalWages.Visible = false;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "ddd", "alert('Record could not be updated.')", true);
            }
        }
        catch (Exception ex)
        {
            // ShowMessage("Record Can't Saved. Error :" + ex.Message, true); sss
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "ddd", "alert('Record Can't verified. Error :" + ex.Message + "')", true);
        }
    }

    protected void EnableControl(string css, bool enabled)
    {
        txtFD.CssClass = css;
        txtTD.CssClass = css;
        txtExtraOT.CssClass = css;
        txtExtrOTRate.CssClass = css;
        txtTotalTraveldays.CssClass = css;
        txtTravelPay.CssClass = css;
        txtFD.Enabled = enabled;
        txtTD.Enabled = enabled;
        txtExtraOT.Enabled = enabled;
        txtExtrOTRate.Enabled = enabled;
        txtTotalTraveldays.Enabled = enabled;
        txtTravelPay.Enabled = enabled;
    }

    

    
    //protected void rptPayollDoc_ItemDataBound(object sender, RepeaterItemEventArgs e)
    //{
    //    HiddenField hfPayrollID = (HiddenField)e.Item.FindControl("hfPayrollID");
    //    Panel Panel1 = (Panel)e.Item.FindControl("Panel");
    //    HtmlContainerControl HtmlDIv = (HtmlContainerControl)e.Item.FindControl("div");

    //    string sql = "select * from CrewPayrollDocuments where PayrollID=" + hfPayrollID.Value + "";
    //    DataTable DT = Common.Execute_Procedures_Select_ByQueryCMS(sql);
    //    StringBuilder sImages = new StringBuilder();
    //    //foreach (DataRow dr in DT.Rows)
    //    //{
    //    //    sImages.Append("<a onclick=OpenDocument(" + dr["TableID"].ToString() + ") style='cursor:pointer'>  ");
    //    //    sImages.Append("<img src='../Images/paperclip12.gif' title='" + dr["DocumentName"].ToString() + "'/>" + dr["DocumentName"].ToString());
    //    //    sImages.Append("</a>");
    //    //    sImages.Append("&nbsp;");
    //    //}

    //    if (DT.Rows.Count > 0)
    //    {
    //        sImages.Append("<a onclick=AddDocuments('V'," + hfPayrollID.Value + ") style='cursor:pointer'>  ");
    //        sImages.Append("<img src='../Images/paperclip12.gif' title='Click to view files'/>");
    //        sImages.Append("</a>");
    //        sImages.Append("&nbsp;");
    //    }

    //    if (auth1.IsUpdate)
    //    {
    //        sImages.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
    //        sImages.Append("<a target='_blank' onclick=AddDocuments('E'," + hfPayrollID.Value + ") style='cursor:pointer'>  ");
    //        sImages.Append("<img src='../Images/Add12.gif'> ");
    //        sImages.Append("</a>");
    //    }

    //    HtmlDIv.InnerHtml = sImages.ToString();
    //}

    public void ShowAttachment()
    {
        string sql = "select * from CrewPayrollDocuments where PayrollID=" + PayRollID + "";
        DataTable DT = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        rptDocuments.DataSource = DT;
        rptDocuments.DataBind();
    }

    protected void btnAddDoc_Click(object sender, ImageClickEventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "fas dfsaf", "window.open('../CrewAccounting/AddDocuments.aspx?Mode=E&PayrollID=" + PayRollID + "','','width=800,height=600');", true);
    }

    protected void savePayroll()
    {

        string datastr, headerstr;
        datastr = "";
        headerstr = "";
        if (PayRollID > 0 && CONTRACTID > 0)
        {
            hdnOtherAmount.Value = "0";
            foreach (RepeaterItem item in rptEaringWages.Items)
            {
                Label lblComponentName = item.FindControl("lblComponentName") as Label;
                TextBox txtComponentValue = item.FindControl("txtComponentAmount") as TextBox;
                HiddenField hdnComponentId = item.FindControl("hdnComponentId") as HiddenField;
                HiddenField hdnComponentType = item.FindControl("hdnComponentType") as HiddenField;
                if (!string.IsNullOrWhiteSpace(lblComponentName.Text))
                {
                    if (Convert.ToInt32(hdnComponentId.Value) != 999)
                    {
                        headerstr = headerstr + "," + lblComponentName.Text;
                        datastr = datastr + "," + txtComponentValue.Text;
                    }
                    else
                    {
                        if (lblComponentName.Text.ToUpper() == "EXTRA ALLOW")
                        {
                            hdnOtherAmount.Value = txtComponentValue.Text;
                        }
                        
                    }

                }
            }

            foreach (RepeaterItem item in rptDeductionWages.Items)
            {
                Label lbldeductComponentName = item.FindControl("lbldeductComponentName") as Label;
                TextBox txtdeductComponentValue = item.FindControl("txtdeductComponentAmount") as TextBox;
                HiddenField hdnComponentId = item.FindControl("hdndeductComponentId") as HiddenField;
                HiddenField hdnComponentType = item.FindControl("hdndeductComponentType") as HiddenField;
                if (!string.IsNullOrWhiteSpace(lbldeductComponentName.Text))
                {
                    headerstr = headerstr + "," + lbldeductComponentName.Text;
                    datastr = datastr + "," + txtdeductComponentValue.Text;
                }
            }

            if (!string.IsNullOrWhiteSpace(headerstr))
            {
                headerstr = headerstr.Substring(1);
                datastr = datastr.Substring(1);
                getTotalEarnings();

                int DiffDays = Common.CastAsInt32(txtTD.Text) - Common.CastAsInt32(txtFD.Text) + 1;
                int MonthDays = 30;
                if (DateTime.DaysInMonth(Year, Month) == 31)
                {
                    if (DiffDays == 30 || DiffDays == 31)
                        MonthDays = DiffDays;
                }

                Decimal ExtraAmount = 0;
                ExtraAmount = Math.Round(Common.CastAsDecimal(hdnOtherAmount.Value), 2);

                WagesMaster.UpdateCrewPortageBillHeaderDetails(PayRollID, CONTRACTID, Convert.ToDecimal(txtExtraOT.Text), Convert.ToDecimal(txtExtrOTRate.Text), Convert.ToDecimal(txtExtraOTAmount.Text), Convert.ToDecimal(finaltotalEarnings), Common.CastAsDecimal(TotDedThisMonth.Text), headerstr, datastr, Common.CastAsInt32(Session["loginid"]), Convert.ToInt32(txtFD.Text), Convert.ToInt32(txtTD.Text), Common.CastAsDecimal(TotWagesPayAbleOnBoard.Text), Common.CastAsDecimal(lblBowFromValue.Text), Common.CastAsDecimal(lblBalanceOfWages.Text), Common.CastAsInt32(txtTotalTraveldays.Text), Common.CastAsDecimal(lbltravelPayAmount.Text), Common.CastAsDecimal(ExtraAmount));
            }
        }
       
    }

    protected void updateBowforNextMonth(int vesselId, int paymonth, int payyear, decimal Bow,int payrollid)
    {
        Common.Set_Procedures("UpdateNextMonthBOWforCrew");
        Common.Set_ParameterLength(5);
        Common.Set_Parameters(
                new MyParameter("@vesselId", vesselId),
                new MyParameter("@paymonth", paymonth),
                new MyParameter("@payyear", payyear),
                new MyParameter("@BOW", Bow),
                new MyParameter("@PayRollId", payrollid)
                );

        DataSet ds = new DataSet();
        bool res = Common.Execute_Procedures_IUD_CMS(ds);
    }
}