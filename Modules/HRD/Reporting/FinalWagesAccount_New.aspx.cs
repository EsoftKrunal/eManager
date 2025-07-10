using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class FinalWagesAccount : System.Web.UI.Page
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
        PayrollId = Common.CastAsInt32(Request.QueryString["PayrollId"]);
        CrystalReportViewer1.Visible = false;
        string sql = "EXEC GetPaySlipDetail " + PayrollId.ToString();
        DataTable dt_Data = Common.Execute_Procedures_Select_ByQueryCMS(sql);

        if (dt_Data.Rows.Count > 0)
        {
            DataRow dr_data = dt_Data.Rows[0];

            int Month = Common.CastAsInt32(dr_data["PayMonth"]);
            int Year = Common.CastAsInt32(dr_data["PayYear"]);
            string CrewNo = dr_data["EmpNo"].ToString();
           // int VesselId = Common.CastAsInt32(dr_data["VesselId"]);
            string sql1 = "EXEC Get_EarningDecuctionComponetsforPaySlip " + PayrollId.ToString();
            DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql1);
            CrystalReportViewer1.Visible = true;
            CrystalReportViewer1.ReportSource = rpt;
            rpt.Load(Server.MapPath("FinalWagesAccount_New.rpt"));
            rpt.SetDataSource(dt);
            //string sqlCompany = "Select top 1 CompanyName from Company with(nolock) where StatusId = 'A' And DefaultCompany = 'Y'";
            //DataTable dtCompany = Common.Execute_Procedures_Select_ByQueryCMS(sqlCompany);
            if (! string.IsNullOrWhiteSpace(dr_data["CompanyName"].ToString()))
            {
                rpt.SetParameterValue("CompanyName", dr_data["CompanyName"].ToString());
            }
            else
            {
                rpt.SetParameterValue("CompanyName", "");
            }        
            rpt.SetParameterValue("MonthandYear", ConvertMMMToM(Month) + " " + Year.ToString());
            rpt.SetParameterValue("EmpNo", dr_data["EmpNo"]);
            rpt.SetParameterValue("EmpName", dr_data["EmpName"]);
            rpt.SetParameterValue("Vessel", dr_data["Vessel"]);
            rpt.SetParameterValue("Rank", dr_data["Rank"]);

            rpt.SetParameterValue("SignOffDate", dr_data["SingOffDate"]);
            rpt.SetParameterValue("Port", dr_data["PortName"]);
            rpt.SetParameterValue("Date", DateTime.Now.ToString("dd-MMM-yyyy"));

            rpt.SetParameterValue("TotalEarnings", Common.CastAsDecimal(dr_data["TotalEarnings"]));
            rpt.SetParameterValue("TotalDeductions", Common.CastAsDecimal(dr_data["TotalDeductions"]));
            rpt.SetParameterValue("BalanceOfWages", Common.CastAsDecimal(dr_data["BalanceOfWages"]));
            rpt.SetParameterValue("BOW", Common.CastAsDecimal(dr_data["PrevMonBal"]));
            rpt.SetParameterValue("CurMonBal", Common.CastAsDecimal(dr_data["CurMonBal"]));
            rpt.SetParameterValue("LastMonthAndYear", ConvertMMMToM(Month - 1) + " " + Year.ToString());
            rpt.SetParameterValue("ExtraOT", Common.CastAsDecimal(dr_data["ExtraOTAmount"]));
            rpt.SetParameterValue("TravelPay", Common.CastAsDecimal(dr_data["TravelPayAmount"]));
            rpt.SetParameterValue("@PayrollId", Convert.ToInt32(PayrollId));
            rpt.SetParameterValue("Total", Common.CastAsDecimal(dr_data["BalanceOfWages"]));
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
    public string getVesselName(int vesselid)
    {
        DataTable dt = Budget.getTable("SELECT VESSELNAME FROM DBO.VESSEL WHERE VESSELID=" + vesselid.ToString()).Tables[0];
        if (dt.Rows.Count > 0)
            return dt.Rows[0][0].ToString();
        else
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
}
