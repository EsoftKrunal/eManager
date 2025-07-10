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

public partial class EmpContractReport : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    public int ContractIId
    {
        get { return Convert.ToInt32("0" + ViewState["ContractIId"].ToString()); }
        set { ViewState["ContractIId"] = value.ToString(); }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        if (!Page.IsPostBack)
        {
            ContractIId = Common.CastAsInt32(Page.Request.QueryString["Cont_ID"]);
        }
        Showreport();
    }
    public void Showreport()
    {
        //decimal TotalAmount = 0; 
        //string Sql = "Exec get_EmployeeContract "+Cont_Id+"";
        //DataTable DtTraining = Budget.getTable(Sql).Tables[0];

        ////--------------------------------------
        //DataTable dtOtherAmt = Budget.getTable("select OtherAmount from crewcontractheader where contractid=" + Cont_Id).Tables[0];
        //DtTraining.Rows.InsertAt(DtTraining.NewRow(), DtTraining.Rows.Count-1);
        ////--------------------------------------
        //foreach (DataColumn dc in DtTraining.Columns)
        //    DtTraining.Rows[DtTraining.Rows.Count - 2][dc] = DtTraining.Rows[DtTraining.Rows.Count - 1][dc];
        ////--------------------------------------
       
        //DtTraining.Rows[DtTraining.Rows.Count - 2]["ComponentName"] = "MTM ALLOW.";
        //DtTraining.Rows[DtTraining.Rows.Count - 2]["Amount"] = Common.CastAsDecimal(dtOtherAmt.Rows[0][0]);

        //foreach (DataRow dr in DtTraining.Rows)
        //{
        //    if (dr["ComponentName"].ToString().Trim() != "EXTRA OT RATE")
        //        TotalAmount += Common.CastAsDecimal(dr["Amount"].ToString());

        //    if (dr["ComponentName"].ToString().Trim() == "EXTRA OT RATE")
        //        dr["ComponentName"] = "EXTRA OT RATE (Per Hr.)";
        //}

        //this.CrystalReportViewer1.Visible = true;
        //rpt.Load(Server.MapPath("EmpContractReport.rpt"));
        //CrystalReportViewer1.ReportSource = rpt;
        //DtTraining.TableName = "get_EmployeeContract;1";
        //rpt.SetDataSource(DtTraining);
        //DataTable dt1 = PrintCrewList.selectCompanyDetails();
     
        
        //rpt.SetParameterValue("TotalAmount", TotalAmount);


        //*******************
        //========== Code to check report printing authority
        CrystalReportViewer1.HasPrintButton = Alerts.Check_ReportPrintingAuthority(Convert.ToInt32(Session["loginid"].ToString()), 19);
        //==========
        DataSet ds = new DataSet();
        DataTable dt = PrintContract.PrintContractDetails(ContractIId);
        DataTable dt2 = PrintContract.PrintDetails(ContractIId);
        DataTable dt11 = PrintContract.LoginDetails(Convert.ToInt32(Session["LoginId"].ToString()));
        DataTable dt21 = PrintCrewList.selectCompanyDetails();
        if (dt.Rows.Count > 0)
        {
            this.CrystalReportViewer1.Visible = true;
            //CrystalReportViewer1.ReportSource = rpt;
            rpt.Load(Server.MapPath("PrintContract_MUMBAI.rpt"));
            dt.TableName = "printcontract;1";
            dt2.TableName = "Wages_Assigned_By_StartDate;1";
            dt2.Columns["WageScaleComponentValue"].ReadOnly = false; 
            ds.Tables.Add(dt);
           
            DataRow[] drs1 = dt2.Select("ComponentName='EXTRA OT RATE'");
            decimal ExtraOTRate = 0;
            if (drs1.Length > 0)
            {
                ExtraOTRate = Common.CastAsDecimal(drs1[0]["WageScaleComponentValue"]);
            }

            DataRow[] drs = dt2.Select("ComponentName='Net Earning'");

            decimal TotalAmount = 0;
            if (drs.Length > 0)
            {
                TotalAmount = Common.CastAsDecimal(drs[0]["WageScaleComponentValue"]);
                TotalAmount = TotalAmount - ExtraOTRate;
            }
            dt2.Rows[dt2.Rows.Count - 1]["WageScaleComponentValue"] = TotalAmount;
            dt2.Rows[dt2.Rows.Count - 2]["WageScaleComponentValue"] = TotalAmount;
            ds.Tables.Add(dt2);

            rpt.SetDataSource(ds);
            CrystalReportViewer1.ReportSource = rpt;

            foreach (DataRow dr2 in dt.Rows)
            {
                ViewState.Add("CrewName", dr2["firstname"].ToString());
                ViewState.Add("SeamanBookNo", dr2["SeamanBook"].ToString());
                ViewState.Add("StartDate", dr2["StartDate"].ToString());
            }

            CrystalDecisions.CrystalReports.Engine.ReportDocument rptsubreport1 = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            rptsubreport1 = rpt.OpenSubreport("ContractPrint2.rpt");
            rptsubreport1.SetDataSource(dt21);

            CrystalDecisions.CrystalReports.Engine.ReportDocument rptsubreport2 = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            rptsubreport2 = rpt.OpenSubreport("ContractPrint3.rpt");
            rptsubreport2.SetDataSource(dt11);

            

            foreach (DataRow dr in dt21.Rows)
            {
                rpt.SetParameterValue("@Company", dr["CompanyName"].ToString());
                //rpt.SetParameterValue("@Address", dr["Address"].ToString());
                //rpt.SetParameterValue("@TelePhone", dr["TelephoneNumber"].ToString());
                //rpt.SetParameterValue("@Fax", dr["Faxnumber"].ToString());
                //rpt.SetParameterValue("@RegistrationNo", dr["RegistrationNo"].ToString());
                //rpt.SetParameterValue("@Email", dr["Email1"].ToString());
                //rpt.SetParameterValue("@Website", dr["Website"].ToString());
            }
            foreach (DataRow dr1 in dt11.Rows)
            {
                rpt.SetParameterValue("@CrewName", ViewState["CrewName"].ToString());
                rpt.SetParameterValue("@SeamanBookNo", ViewState["SeamanBookNo"].ToString());
                rpt.SetParameterValue("@Date", ViewState["StartDate"].ToString());
                rpt.SetParameterValue("TotalAmount",TotalAmount);
            }
        }
        else
        {
            this.CrystalReportViewer1.Visible = false;
        }
    }
}
