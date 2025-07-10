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
        CrystalReportViewer1.HasPrintButton = Alerts.Check_ReportPrintingAuthority(Convert.ToInt32(Session["loginid"].ToString()),33);
        //==========
        lblMessage.Visible = true;
        lblMessage.Text = "";
        PayrollId = Common.CastAsInt32(Request.QueryString["PayrollId"]);
        CrystalReportViewer1.Visible = false;
        string sql = "select * from CrewPortageBill where PayrollId=" + PayrollId.ToString();
        DataTable dt_Data = Common.Execute_Procedures_Select_ByQueryCMS(sql);

        if (dt_Data.Rows.Count > 0)
        {
            DataRow Dr = dt_Data.Rows[0];            

            int Month = Common.CastAsInt32(Dr["PayMonth"]);
            int Year = Common.CastAsInt32(Dr["PayYear"]);
            string CrewNo = Dr["CrewNumber"].ToString();
            int VesselId = Common.CastAsInt32(Dr["VesselId"]);

            CrystalReportViewer1.Visible = true;
            CrystalReportViewer1.ReportSource = rpt;
            rpt.Load(Server.MapPath("FinalWagesAccount.rpt"));

            rpt.SetParameterValue("BasicPay", Dr["Cont_Comp_1"].ToString());
            rpt.SetParameterValue("FixedOT",Common.CastAsDecimal(  Dr["Cont_Comp_2"] ));
            rpt.SetParameterValue("AdditionalOT", Common.CastAsDecimal( Dr["Cont_Comp_12"] ));
            rpt.SetParameterValue("SeniorityAllowance", Common.CastAsDecimal( Dr["Cont_Comp_5"]));            
            rpt.SetParameterValue("TradeAllowance", Common.CastAsDecimal(Dr["Cont_Comp_6"]));
            rpt.SetParameterValue("Bonus", Common.CastAsDecimal(Dr["Cont_Comp_3"]));
            rpt.SetParameterValue("OtherPayments", Common.CastAsDecimal(Dr["OtherPayments"]));
            rpt.SetParameterValue("LeavePay", Common.CastAsDecimal(Dr["Cont_Comp_4"]));
            rpt.SetParameterValue("AdditionalPayment", Common.CastAsDecimal(Dr["AdditionalPayment"]));


            rpt.SetParameterValue("SignOffDate", "");
            rpt.SetParameterValue("Port", "");
            

            rpt.SetParameterValue("Allotment", Common.CastAsDecimal(Dr["Allotment"]));
            rpt.SetParameterValue("CashAdvance", Common.CastAsDecimal(Dr["CashAdvance"]));
            rpt.SetParameterValue("BondedStore", Common.CastAsDecimal(Dr["BondedStore"]));
            rpt.SetParameterValue("RadioTeleCall", Common.CastAsDecimal(Dr["RadioTeleCall"]));
            rpt.SetParameterValue("OtherDeduction", Common.CastAsDecimal(Dr["OtherDeductions"]));
            
            rpt.SetParameterValue("Vessel", getVesselName(VesselId));
            rpt.SetParameterValue("Date", DateTime.Now.ToString("dd-MMM-yyyy"));
            
            rpt.SetParameterValue("CrewNo", CrewNo);
            rpt.SetParameterValue("SrNbr", ""); 

            rpt.SetParameterValue("Name", Dr["CrewName"].ToString());
            rpt.SetParameterValue("Rank", Dr["rank"].ToString());
            rpt.SetParameterValue("EmpNo", CrewNo);

            rpt.SetParameterValue("OtherDeducton1", "0.0");
            rpt.SetParameterValue("OtherDeducton2", "0.0");
            rpt.SetParameterValue("OtherDeducton3", "0.0");
            rpt.SetParameterValue("BalanceOfWagesFromLastMonth", Common.CastAsDecimal(Dr["PrevMonBal"]));

            rpt.SetParameterValue("Paid", Convert.ToDecimal(Dr["Paid"]));
            rpt.SetParameterValue("MTMAllowance", Convert.ToDecimal(Dr["MTMAllowance"]));

            rpt.SetParameterValue("UnionFee", Convert.ToDecimal(Dr["UnionFee"]));
            rpt.SetParameterValue("SUPCERTALLOW", Convert.ToDecimal(Dr["Cont_Comp_7"]));
            rpt.SetParameterValue("COMMALLOW", Convert.ToDecimal(Dr["Cont_Comp_8"]));
            rpt.SetParameterValue("SUBALLOW", Convert.ToDecimal(Dr["Cont_Comp_9"]));
            rpt.SetParameterValue("UALLOW", Convert.ToDecimal(Dr["Cont_Comp_10"]));
            rpt.SetParameterValue("GMDSS", Convert.ToDecimal(Dr["Cont_Comp_11"]));
            rpt.SetParameterValue("FBOW", Convert.ToDecimal(Dr["FbowPaidOnBoard"]));
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
