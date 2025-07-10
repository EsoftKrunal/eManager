using System;
using System.Data;


public partial class PaySlip : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    public int PayrollId
    {
        get { return Common.CastAsInt32(ViewState["PayrollId"]); }
        set { ViewState["PayrollId"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        CrystalReportViewer1.HasPrintButton = Alerts.Check_ReportPrintingAuthority(Convert.ToInt32(Session["loginid"].ToString()),33);
        //==========
        lblMessage.Visible = true;
        lblMessage.Text = "";
        CrystalReportViewer1.Visible = false;
        PayrollId = Common.CastAsInt32(Request.QueryString["PayrollId"]);


        string sql = "select CPB.*,(select FirstName+' '+ LastName from UserLogin L where L.LoginID=CPB.VerifiedBy)VerifiedBytxt ,replace(convert(varchar,CPB.VerifiedOn,106),' ','-')VerifiedOntxt from CrewPortageBill CPB where PayrollId=" + PayrollId.ToString();
        DataTable dt_Data = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        if (dt_Data.Rows.Count > 0)
        {
            DataRow dr_data = dt_Data.Rows[0];

            int Month = Common.CastAsInt32(dr_data["PayMonth"]);
            int Year = Common.CastAsInt32(dr_data["PayYear"]);
            string CrewNo = dr_data["CrewNumber"].ToString();
            int VesselId = Common.CastAsInt32(dr_data["VesselId"]);

            CrystalReportViewer1.Visible = true;
            CrystalReportViewer1.ReportSource = rpt;
            rpt.Load(Server.MapPath("PaySlip.rpt"));

            rpt.SetParameterValue("BasicPay", dr_data["Cont_Comp_1"].ToString());
            rpt.SetParameterValue("FixedOT", Common.CastAsDecimal(dr_data["Cont_Comp_2"]));
            rpt.SetParameterValue("Bonus", Common.CastAsDecimal(dr_data["Cont_Comp_3"]));
            rpt.SetParameterValue("LeavePay", Common.CastAsDecimal(dr_data["Cont_Comp_4"]));
            rpt.SetParameterValue("SeniorityAllowance", Common.CastAsDecimal(dr_data["Cont_Comp_5"]));
            rpt.SetParameterValue("TradeAllowance", Common.CastAsDecimal(dr_data["Cont_Comp_6"]));
            rpt.SetParameterValue("SuperiorCertificateAllowance", Common.CastAsDecimal(dr_data["Cont_Comp_7"]));
            rpt.SetParameterValue("OtherPayments", Common.CastAsDecimal(dr_data["OtherPayments"]));
            rpt.SetParameterValue("AdditionalOT", Common.CastAsDecimal(dr_data["Cont_Comp_12"])); //************** 
            
            //Deduction Values --------------------------------

            rpt.SetParameterValue("Allotment", Common.CastAsDecimal(dr_data["Allotment"]));
            rpt.SetParameterValue("CashAdvance", Common.CastAsDecimal(dr_data["CashAdvance"]));
            rpt.SetParameterValue("BondStore", Common.CastAsDecimal(dr_data["BondedStore"]));
            rpt.SetParameterValue("RadioAccount", Common.CastAsDecimal(dr_data["RadioTeleCall"]));
            rpt.SetParameterValue("OtherDeductions", Common.CastAsDecimal(dr_data["OtherDeductions"]));
           
            rpt.SetParameterValue("MonthandYear", ConvertMMMToM(Month )+ " " + Year.ToString());
            rpt.SetParameterValue("LastMonthAndYear", ConvertMMMToM(Month - 1) + " " + Year.ToString());//Common.CastAsDecimal(dr_data["PrevMonBal"])
            
            rpt.SetParameterValue("PBSrNo", "");
            rpt.SetParameterValue("EmpNo", CrewNo);
            rpt.SetParameterValue("BOW", Common.CastAsDecimal(dr_data["PrevMonBal"]));

            rpt.SetParameterValue("Paid", Convert.ToDecimal(dr_data["Paid"]));
            rpt.SetParameterValue("Vessel", getVesselName(VesselId));
            rpt.SetParameterValue("Rank", dr_data["Rank"]);

            rpt.SetParameterValue("EmpName", dr_data["Crewname"]);
            rpt.SetParameterValue("Remarks", dr_data["Remarks"].ToString());
            rpt.SetParameterValue("MTMAllowances", Common.CastAsDecimal(dr_data["MTMAllowance"]));
            rpt.SetParameterValue("UnionFee", Common.CastAsDecimal(dr_data["UnionFee"]));

            rpt.SetParameterValue("VerifiedOn", dr_data["VerifiedBytxt"].ToString()+" / "+dr_data["VerifiedOntxt"].ToString());
            rpt.SetParameterValue("VerifiedBy", "");


            rpt.SetParameterValue("CommAllowance", Common.CastAsDecimal(dr_data["Cont_Comp_8"]));
            rpt.SetParameterValue("SubAllowance", Common.CastAsDecimal(dr_data["Cont_Comp_9"]));
            rpt.SetParameterValue("UAllowance", Common.CastAsDecimal(dr_data["Cont_Comp_10"]));
            rpt.SetParameterValue("Gmdss", Common.CastAsDecimal(dr_data["Cont_Comp_11"]));
            rpt.SetParameterValue("AdditionalPayments", Common.CastAsDecimal(dr_data["AdditionalPayment"]));
            rpt.SetParameterValue("FBOWPaidOnBoard", Common.CastAsDecimal(dr_data["FbowPaidOnBoard"]));

            rpt.SetParameterValue("FD", Common.CastAsDecimal(dr_data["FD"]));
            rpt.SetParameterValue("TD", Common.CastAsDecimal(dr_data["TD"]));
            rpt.SetParameterValue("OTHOURS", Common.CastAsDecimal(dr_data["OT"]));
        }
        else
        {
            lblMessage.Text = "No Record Found";
            CrystalReportViewer1.Visible = false;
        }
    }
    // Function 
    public string getVesselName(int vesselid)
    {
        DataTable dt = Budget.getTable("SELECT VESSELNAME FROM DBO.VESSEL WHERE VESSELID=" + vesselid.ToString()).Tables[0];
        if (dt.Rows.Count > 0)
            return dt.Rows[0][0].ToString();
        else
            return "";
    }
    public string GetVesselIDByCode(string VessID)
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
    public decimal SetPrevMonthBal(int VessID, int Month, int year, string CrewNo)
    {
        int PrevMonth;
        if (Month == 1)
        {
            PrevMonth = 12;
            year = year - 1;
        }
        else
        {
            PrevMonth = Month - 1;
        }
        string sql = "select BalanceofWages from CrewPortageBill where VesselId=" + VessID + " and PayMonth=" + PrevMonth + " and PayYear=" + year + " and CrewNumber='" + CrewNo + "' ";
        DataTable dsDED = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        if (dsDED != null)
            if (dsDED.Rows.Count > 0)
                return Convert.ToDecimal((dsDED.Rows[0][0].ToString() == "") ? "" : dsDED.Rows[0][0].ToString());
            else
                return 0;
        return 0;
    }
}
