using System;
using System.Data;


public partial class PaySlip_New : System.Web.UI.Page
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
        CrystalReportViewer1.HasPrintButton = Alerts.Check_ReportPrintingAuthority(Convert.ToInt32(Session["loginid"].ToString()),4);
        //==========
        lblMessage.Visible = true;
        lblMessage.Text = "";
        CrystalReportViewer1.Visible = false;
        PayrollId = Common.CastAsInt32(Request.QueryString["PayrollId"]);


        string sql = "EXEC GetPaySlipDetail " + PayrollId.ToString();

        DataTable dt_Data = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        if (dt_Data.Rows.Count > 0)
        {
            DataRow dr_data = dt_Data.Rows[0];

            int Month = Common.CastAsInt32(dr_data["PayMonth"]);
            int Year = Common.CastAsInt32(dr_data["PayYear"]);
            
            string sql1 = "EXEC Get_EarningDecuctionComponetsforPaySlip " + PayrollId.ToString();
            DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql1);
            CrystalReportViewer1.Visible = true;
            rpt.Load(Server.MapPath("PayRoll_New.rpt"));
            rpt.SetDataSource(dt);
            rpt.SetParameterValue("MonthandYear", ConvertMMMToM(Month )+ " " + Year.ToString());
            rpt.SetParameterValue("EmpNo", dr_data["EmpNo"]);
            rpt.SetParameterValue("EmpName", dr_data["EmpName"]);
            rpt.SetParameterValue("Vessel", dr_data["Vessel"]);
            rpt.SetParameterValue("Rank", dr_data["Rank"]);
            rpt.SetParameterValue("Remarks", dr_data["Remarks"].ToString());
            rpt.SetParameterValue("VerifiedOn", dr_data["VerifiedBytxt"].ToString()+" / "+dr_data["VerifiedOntxt"].ToString());
           // rpt.SetParameterValue("VerifiedBy", "");
            //rpt.SetParameterValue("FBOWPaidOnBoard", Common.CastAsDecimal(dr_data["FbowPaidOnBoard"]));
            rpt.SetParameterValue("FD", Common.CastAsInt32(dr_data["FD"]));
            rpt.SetParameterValue("TD", Common.CastAsInt32(dr_data["TD"]));
            rpt.SetParameterValue("OTHours", Common.CastAsDecimal(dr_data["OT"]));
            rpt.SetParameterValue("TotalEarnings", Common.CastAsDecimal(dr_data["TotalEarnings"]));
            rpt.SetParameterValue("TotalDeductions", Common.CastAsDecimal(dr_data["TotalDeductions"]));
            rpt.SetParameterValue("BalanceOfWages", Common.CastAsDecimal(dr_data["BalanceOfWages"]));
            rpt.SetParameterValue("BOW", Common.CastAsDecimal(dr_data["PrevMonBal"]));
            rpt.SetParameterValue("CurMonBal", Common.CastAsDecimal(dr_data["CurMonBal"]));
            rpt.SetParameterValue("LastMonthAndYear", ConvertMMMToM(Month - 1) + " " + Year.ToString());
            rpt.SetParameterValue("ExtraOT", Common.CastAsDecimal(dr_data["ExtraOTAmount"]));
            rpt.SetParameterValue("TravelPay", Common.CastAsDecimal(dr_data["TravelPayAmount"]));
            rpt.SetParameterValue("@PayrollId", Convert.ToInt32(PayrollId));
            rpt.SetParameterValue("Currency", dr_data["Currency"]); 


            CrystalReportViewer1.ReportSource = rpt;
            CrystalReportViewer1.DataBind();
            

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
