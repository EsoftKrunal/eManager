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


public partial class Reporting_PrintContract : System.Web.UI.Page
{
    Int32 ContractIId = 0;
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    DataSet ds = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
       

        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 26);
        if (chpageauth <= 0)
        {
            Response.Redirect("DummyReport.aspx");
        }
        if (Request.QueryString["ContractId"] != null)
        {
            ContractIId = Convert.ToInt32(Request.QueryString["ContractId"]);  
        }
        else
        {
            try
            {
                ContractIId = Convert.ToInt32(Session["Cont_id"]);
            }
            catch { }
        }
        if (!IsPostBack)
        {
            // ViewState["ReportFile"] = "CrystalReport2.rpt";
            BindCrewContractTemplate();

        }
        

//Response.Redirect("PrintContract_mum.aspx?contactid=" + ContractIId);
//        return;

        //if (RadCtype.SelectedValue == "M")
        //    Showreport_Mumbai();
        //    else
        //        ShowReport();
    }
    public void BindCrewContractTemplate()
    {
        String sql;
        sql = "Select 0 as CCT_Id, ' < Select > ' As CCT_Name Union  Select CCT_Id, CCT_Name from CrewContract_Template with(nolock) where CCT_StatusId = 'A' order by CCT_Id ";

        DataTable dtCrewContractTemplate = Common.Execute_Procedures_Select_ByQueryCMS(sql);

        if (dtCrewContractTemplate.Rows.Count > 0)
        {
            ddl_ContractTemplate.DataValueField = "CCT_Id";
            ddl_ContractTemplate.DataTextField = "CCT_Name";
            ddl_ContractTemplate.DataSource = dtCrewContractTemplate;
            ddl_ContractTemplate.DataBind();
        }
    }

    public void ShowReport()
    {
        string Reportfile = ViewState["ReportFile"].ToString(); ;
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
            rpt.Load(Server.MapPath(Reportfile));
            dt.TableName = "printcontract;1";
            dt2.TableName = "Wages_Assigned_By_StartDate;1";
            ds.Tables.Add(dt);
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
            }
        }
        else
        {
            this.CrystalReportViewer1.Visible = false;
            lblmessage.Text = "No Record Found.";
        }

    }
    public void Showreport_Mumbai()
    {
        CrystalReportViewer1.HasPrintButton = Alerts.Check_ReportPrintingAuthority(Convert.ToInt32(Session["loginid"].ToString()), 19);
        //==========
        DataSet ds = new DataSet();
        DataTable dt = PrintContract.PrintContractDetails(ContractIId);
        DataTable dt2 = PrintContract.PrintDetails(ContractIId);
        DataTable dt11 = PrintContract.LoginDetails(Convert.ToInt32(Session["LoginId"].ToString()));
        DataTable dt21 = PrintCrewList.selectCompanyDetails();
        if (dt.Rows.Count > 0)
        {
            string ExtraOtRateValue = "";

            this.CrystalReportViewer1.Visible = true;
            //CrystalReportViewer1.ReportSource = rpt;
            rpt.Load(Server.MapPath("PrintContract_MUMBAI.rpt"));
            dt.TableName = "printcontract;1";
            dt2.TableName = "Wages_Assigned_By_StartDate;1";
            dt2.Columns["WageScaleComponentValue"].ReadOnly = false;
            ds.Tables.Add(dt);

            DataRow[] drs1 = dt2.Select("WagwScaleComponentId=12");
            decimal ExtraOTRate = 0;
            if (drs1.Length > 0)
            {
                ExtraOTRate = Common.CastAsDecimal(drs1[0]["WageScaleComponentValue"]);
            }
            DataView dv = dt2.DefaultView;
            dv.RowFilter = "WagwScaleComponentId<>12";
            DataTable dtDetails= dv.ToTable();

            DataRow[] drs = dt2.Select("ComponentName='Net Earning'");

            //decimal TotalAmount = 0;
            //if (drs.Length > 0)
            //{
            //    TotalAmount = Common.CastAsDecimal(drs[0]["WageScaleComponentValue"]);
            //    TotalAmount = TotalAmount - ExtraOTRate;
            //}
            //dt2.Rows[dt2.Rows.Count - 1]["WageScaleComponentValue"] = TotalAmount;
            //dt2.Rows[dt2.Rows.Count - 2]["WageScaleComponentValue"] = TotalAmount;
            //ds.Tables.Add(dt2);
            ds.Tables.Add(dtDetails);            

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
                rpt.SetParameterValue("TotalAmount", "0");
            }
            
            //if(ExtraOTRate>0)
            rpt.SetParameterValue("EOTNAME", "EXTRA OT RATE");
            rpt.SetParameterValue("EOTVALUE",String.Format("{0:0.00}",ExtraOTRate));
            

        }
        else
        {
            this.CrystalReportViewer1.Visible = false;
        }
    }
    protected void btnGo_Click(object sender, EventArgs e)
    {
       try
        {
            if (ddl_ContractTemplate.SelectedIndex == 0)
            {
                ddl_ContractTemplate.Focus();
                lblmessage.Text = "Select Any Contract To Print";
                rpt.Close();
                rpt.Dispose();
                return;
            }

            if (ddl_ContractTemplate.SelectedValue != null && Convert.ToInt32(ddl_ContractTemplate.SelectedValue) > 0)
            {
                if (Convert.ToInt32(ddl_ContractTemplate.SelectedValue) > 0 && ContractIId > 0)
                {
                    PrintCrewContract();
                }

            }

        }
        catch(Exception ex)
        {
            lblmessage.Text = ex.Message.ToString();
        }
        //if(RadCtype.SelectedValue=="Y")
        //{
        //    ViewState["ReportFile"] = "PrintContract_YANGOON.rpt";
        //    ShowReport();
        //}
        //else if (RadCtype.SelectedValue == "M")
        //{
        //    Showreport_Mumbai();
        //    //Response.Redirect("EmpContractReport.aspx?Cont_ID=" + ContractIId.ToString());
        //}
        //else 
        //{
        //    ViewState["ReportFile"] = "CrystalReport2.rpt";
        //    ShowReport();
        //}

    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }

    public void EnergiosTemplate()
    {
        string sql = "EXEC printcontract " + ContractIId.ToString();
        DataTable dt_Data = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        if (dt_Data.Rows.Count > 0)
        {
            string imgURL = string.Empty;
            string sql2 = "Select CCT_ImageURL from CrewContract_Template with(nolock) where CCT_Id =" + ddl_ContractTemplate.SelectedValue;
            DataTable dtTemplate = Common.Execute_Procedures_Select_ByQueryCMS(sql2);
            if (dtTemplate.Rows.Count > 0)
            {
                imgURL = dtTemplate.Rows[0]["CCT_ImageURL"].ToString();
            }
            DataRow dr_data = dt_Data.Rows[0];
            string sql1 = "EXEC Get_EarningDeductionComponentsforContract " + ContractIId.ToString();
            DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql1);
            CrystalReportViewer1.Visible = true;
            rpt.Load(Server.MapPath("CrewContractDetails.rpt"));
            rpt.SetDataSource(dt);
            rpt.SetParameterValue("FullName", dr_data["firstname"].ToString());
            rpt.SetParameterValue("DateofBirth", dr_data["dateofbirth"].ToString());
            rpt.SetParameterValue("PlaceOfBirth", dr_data["PlaceOfBirth"].ToString());
            rpt.SetParameterValue("Passport", dr_data["passport"].ToString());
            rpt.SetParameterValue("PIssueDt", dr_data["PIssueDt"].ToString());
            rpt.SetParameterValue("PIssuePlace", dr_data["PIssuePlace"].ToString());
            rpt.SetParameterValue("SeamenBook", dr_data["SeamanBook"].ToString());
            rpt.SetParameterValue("SIssueDt", dr_data["SIssueDt"].ToString());
            rpt.SetParameterValue("SIssuePlace", dr_data["SIssuePlace"].ToString());
            rpt.SetParameterValue("In_Port", dr_data["IN_PORT"].ToString());
            rpt.SetParameterValue("Vessel", dr_data["vesselname"].ToString());
            rpt.SetParameterValue("VesselFlag", dr_data["VesselFlag"].ToString());
            rpt.SetParameterValue("ImoNr", dr_data["ImoNo"].ToString());
            rpt.SetParameterValue("VesselType", dr_data["VesselTypeName"].ToString());
            rpt.SetParameterValue("Rank", dr_data["NewRankId"].ToString());
            rpt.SetParameterValue("TotalEarnings", Common.CastAsDecimal(dr_data["TotalEarnings"]));
            rpt.SetParameterValue("TotalDeductions", Common.CastAsDecimal(dr_data["TotalDeductions"]));
            rpt.SetParameterValue("ExtraOTRate", Common.CastAsDecimal(dr_data["ExtraOTRate"]));
            rpt.SetParameterValue("PEngagement", dr_data["PEngagement"].ToString());
            rpt.SetParameterValue("Duration", Common.CastAsInt32(dr_data["duration"]));
            rpt.SetParameterValue("StartDt", dr_data["StartDate"].ToString());
            rpt.SetParameterValue("EndDt", dr_data["EndDate"].ToString());
            // rpt.SetParameterValue("TravelPay", Common.CastAsDecimal(dr_data["TravelPayAmount"]));
            rpt.SetParameterValue("@ContractId", Convert.ToInt32(ContractIId));
            rpt.SetParameterValue("Currency", dr_data["Currency"].ToString());
            rpt.SetParameterValue("ManningAgent", dr_data["ManningAgent"].ToString());
            rpt.SetParameterValue("RegNo", dr_data["RegNo"].ToString());
            rpt.SetParameterValue("RSPL", dr_data["RSPL"].ToString());
            rpt.SetParameterValue("RSPLValidityDt", dr_data["RPSLValidityDt"].ToString());
            rpt.SetParameterValue("MainingAgentContact", dr_data["ManningAgentContact"].ToString());
            rpt.SetParameterValue("ManningAgentEmail", dr_data["ManningAgentEmail"].ToString());
            rpt.SetParameterValue("OwnerAgent", dr_data["OwnerAgent"].ToString());
            rpt.SetParameterValue("GraphicLocation", imgURL);
            rpt.SetParameterValue("WageScale", dr_data["ScaleName"].ToString());
            rpt.SetParameterValue("ShipManager", dr_data["ShipManager"].ToString());
            rpt.SetParameterValue("TodayDt", System.DateTime.Now.Date.ToString("dd/MM/yyyy"));
            rpt.SetParameterValue("ContractRefNo", dr_data["ContractReferenceNumber"].ToString());
            CrystalReportViewer1.ReportSource = rpt;
            CrystalReportViewer1.DataBind();
        }
    }

    public void POEATemplate()
    {
        string CurrentDay = DateTime.Now.Day.ToString(); 
        string CurrentMonth = String.Format("{0:MMMM}", DateTime.Now).ToUpper();
        string currentYear = DateTime.Now.Year.ToString();
        string sql = "EXEC printcontract " + ContractIId.ToString();
        DataTable dt_Data = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        if (dt_Data.Rows.Count > 0)
        {
            string imgURL = string.Empty;
            string sql2 = "Select CCT_ImageURL from CrewContract_Template with(nolock) where CCT_Id =" + ddl_ContractTemplate.SelectedValue;
            DataTable dtTemplate = Common.Execute_Procedures_Select_ByQueryCMS(sql2);
            if (dtTemplate.Rows.Count > 0)
            {
                imgURL = dtTemplate.Rows[0]["CCT_ImageURL"].ToString();
            }
            DataRow dr_data = dt_Data.Rows[0];
            string sql1 = "EXEC Get_EarningDeductionComponentsforContract " + ContractIId.ToString();
            DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql1);
            CrystalReportViewer1.Visible = true;
            rpt.Load(Server.MapPath("CrewContractDetailsPOEA.rpt"));
            rpt.SetDataSource(dt);
            rpt.SetParameterValue("FullName", dr_data["firstname"].ToString());
            rpt.SetParameterValue("DateofBirth", dr_data["dateofbirth"].ToString());
            rpt.SetParameterValue("PlaceOfBirth", dr_data["PlaceOfBirth"].ToString());
            rpt.SetParameterValue("Passport", dr_data["passport"].ToString());
            rpt.SetParameterValue("PIssueDt", dr_data["PIssueDt"].ToString());
            rpt.SetParameterValue("PIssuePlace", dr_data["PIssuePlace"].ToString());
            rpt.SetParameterValue("SeamenBook", dr_data["SeamanBook"].ToString());
            rpt.SetParameterValue("SIssueDt", dr_data["SIssueDt"].ToString());
            rpt.SetParameterValue("SIssuePlace", dr_data["SIssuePlace"].ToString());
            rpt.SetParameterValue("In_Port", dr_data["IN_PORT"].ToString());
            rpt.SetParameterValue("Vessel", dr_data["vesselname"].ToString());
            rpt.SetParameterValue("VesselFlag", dr_data["VesselFlag"].ToString());
            rpt.SetParameterValue("ImoNr", dr_data["ImoNo"].ToString());
            rpt.SetParameterValue("VesselType", dr_data["VesselTypeName"].ToString());
            rpt.SetParameterValue("Rank", dr_data["NewRankId"].ToString());
            rpt.SetParameterValue("TotalEarnings", Common.CastAsDecimal(dr_data["TotalEarnings"]));
            rpt.SetParameterValue("TotalDeductions", Common.CastAsDecimal(dr_data["TotalDeductions"]));
            rpt.SetParameterValue("ExtraOTRate", Common.CastAsDecimal(dr_data["ExtraOTRate"]));
            rpt.SetParameterValue("PEngagement", dr_data["PEngagement"].ToString());
            rpt.SetParameterValue("Duration", Common.CastAsInt32(dr_data["duration"]));
            rpt.SetParameterValue("StartDt", dr_data["StartDate"].ToString());
            rpt.SetParameterValue("EndDt", dr_data["EndDate"].ToString());
            // rpt.SetParameterValue("TravelPay", Common.CastAsDecimal(dr_data["TravelPayAmount"]));
            rpt.SetParameterValue("@ContractId", Convert.ToInt32(ContractIId));
            rpt.SetParameterValue("Currency", dr_data["Currency"].ToString());
            rpt.SetParameterValue("ManningAgent", dr_data["ManningAgent"].ToString());
            rpt.SetParameterValue("RegNo", dr_data["RegNo"].ToString());
            rpt.SetParameterValue("RSPL", dr_data["RSPL"].ToString());
            rpt.SetParameterValue("RSPLValidityDt", dr_data["RPSLValidityDt"].ToString());
            rpt.SetParameterValue("MainingAgentContact", dr_data["ManningAgentContact"].ToString());
            rpt.SetParameterValue("ManningAgentEmail", dr_data["ManningAgentEmail"].ToString());
            rpt.SetParameterValue("OwnerAgent", dr_data["OwnerAgent"].ToString());
            rpt.SetParameterValue("GraphicLocation", imgURL);
            rpt.SetParameterValue("WageScale", dr_data["ScaleName"].ToString());
            rpt.SetParameterValue("ShipManager", dr_data["ShipManager"].ToString());
            rpt.SetParameterValue("TodayDt", System.DateTime.Now.Date.ToString("dd/MM/yyyy"));
            rpt.SetParameterValue("CrewAddress", dr_data["CrewAddress"].ToString());
            rpt.SetParameterValue("YearBuilt", dr_data["YearBuilt"].ToString());
            rpt.SetParameterValue("MLCOwner", dr_data["MLCOwner"].ToString());
            rpt.SetParameterValue("MLCOwnerAddress", dr_data["MLCOwnerAddress"].ToString());
            rpt.SetParameterValue("GRT", dr_data["GRT"].ToString());
            rpt.SetParameterValue("BasicSalary", dr_data["BasicSalary"].ToString());
            rpt.SetParameterValue("FixedOvertime", dr_data["FixedOvertime"].ToString());
            rpt.SetParameterValue("LeavePay", dr_data["LeavePay"].ToString());
            rpt.SetParameterValue("HoursofWork", dr_data["HoursofWork"].ToString());
            rpt.SetParameterValue("ClassSociety", dr_data["ClassSociety"].ToString());
            rpt.SetParameterValue("ERegistrationNo", dr_data["ERegistrationNo"].ToString());
            rpt.SetParameterValue("WageScaleCurrency", dr_data["WageScaleCurrency"].ToString());
            rpt.SetParameterValue("CurrentDate", CurrentDay);
            rpt.SetParameterValue("CurrentMonth", CurrentMonth);
            rpt.SetParameterValue("CurrentYear", currentYear);
            CrystalReportViewer1.ReportSource = rpt;
            CrystalReportViewer1.DataBind();
        }
    }

    public void EnergiosAppointmentLetterTemplate()
    {
        string sql = "EXEC printcontract " + ContractIId.ToString();
        DataTable dt_Data = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        if (dt_Data.Rows.Count > 0)
        {
            string imgURL = string.Empty;
            string LoginName = string.Empty;
            string Position = string.Empty;
            string sql2 = "Select CCT_ImageURL from CrewContract_Template with(nolock) where CCT_Id =" + ddl_ContractTemplate.SelectedValue;
            DataTable dtTemplate = Common.Execute_Procedures_Select_ByQueryCMS(sql2);
            if (dtTemplate.Rows.Count > 0)
            {
                imgURL = dtTemplate.Rows[0]["CCT_ImageURL"].ToString();
            }
            string sql3 = "Select um.FirstName+' '+um.LastName As LoginName, ISNULL(p.PositionName,'') As Position from UserMaster um with(nolock) left join Position p with(nolock) on um.PositionId = p.PositionId where um.LoginId = " + Convert.ToInt32(Session["loginid"].ToString());
            DataTable dtUser = Common.Execute_Procedures_Select_ByQueryCMS(sql3);
            if (dtUser.Rows.Count > 0)
            {
                LoginName = dtUser.Rows[0]["LoginName"].ToString();
                Position = dtUser.Rows[0]["Position"].ToString();
            }
            DataRow dr_data = dt_Data.Rows[0];
            string sql1 = "EXEC Get_EarningDeductionComponentsforContract " + ContractIId.ToString();
            DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql1);
            CrystalReportViewer1.Visible = true;
            rpt.Load(Server.MapPath("CrewContractAppointmentLetter.rpt"));
            rpt.SetDataSource(dt);
            rpt.SetParameterValue("FullName", dr_data["firstname"].ToString());
            //rpt.SetParameterValue("DateofBirth", dr_data["dateofbirth"].ToString());
            //rpt.SetParameterValue("PlaceOfBirth", dr_data["PlaceOfBirth"].ToString());
            //rpt.SetParameterValue("Passport", dr_data["passport"].ToString());
            //rpt.SetParameterValue("PIssueDt", dr_data["PIssueDt"].ToString());
            //rpt.SetParameterValue("PIssuePlace", dr_data["PIssuePlace"].ToString());
            //rpt.SetParameterValue("SeamenBook", dr_data["SeamanBook"].ToString());
            //rpt.SetParameterValue("SIssueDt", dr_data["SIssueDt"].ToString());
            //rpt.SetParameterValue("SIssuePlace", dr_data["SIssuePlace"].ToString());
            //rpt.SetParameterValue("In_Port", dr_data["IN_PORT"].ToString());
            rpt.SetParameterValue("Vessel", dr_data["vesselname"].ToString());
            //rpt.SetParameterValue("VesselFlag", dr_data["VesselFlag"].ToString());
            //rpt.SetParameterValue("ImoNr", dr_data["ImoNo"].ToString());
            //rpt.SetParameterValue("VesselType", dr_data["VesselTypeName"].ToString());
            rpt.SetParameterValue("Rank", dr_data["NewRankId"].ToString());
            rpt.SetParameterValue("TotalEarnings", Common.CastAsDecimal(dr_data["TotalEarnings"]));
            //rpt.SetParameterValue("TotalDeductions", Common.CastAsDecimal(dr_data["TotalDeductions"]));
            rpt.SetParameterValue("ExtraOTRate", Common.CastAsDecimal(dr_data["ExtraOTRate"]));
            //rpt.SetParameterValue("PEngagement", dr_data["PEngagement"].ToString());
            rpt.SetParameterValue("Duration", Common.CastAsInt32(dr_data["duration"]));
            rpt.SetParameterValue("StartDt", dr_data["StartDate"].ToString());
            //rpt.SetParameterValue("EndDt", dr_data["EndDate"].ToString());
            // rpt.SetParameterValue("TravelPay", Common.CastAsDecimal(dr_data["TravelPayAmount"]));
            rpt.SetParameterValue("@ContractId", Convert.ToInt32(ContractIId));
            rpt.SetParameterValue("Currency", dr_data["Currency"].ToString());
            //rpt.SetParameterValue("ManningAgent", dr_data["ManningAgent"].ToString());
            //rpt.SetParameterValue("RegNo", dr_data["RegNo"].ToString());
            //rpt.SetParameterValue("RSPL", dr_data["RSPL"].ToString());
            //rpt.SetParameterValue("RSPLValidityDt", dr_data["RPSLValidityDt"].ToString());
            //rpt.SetParameterValue("MainingAgentContact", dr_data["ManningAgentContact"].ToString());
            //rpt.SetParameterValue("ManningAgentEmail", dr_data["ManningAgentEmail"].ToString());
            //rpt.SetParameterValue("OwnerAgent", dr_data["OwnerAgent"].ToString());
            //rpt.SetParameterValue("GraphicLocation", imgURL);
            //rpt.SetParameterValue("WageScale", dr_data["ScaleName"].ToString());
            rpt.SetParameterValue("ShipManager", dr_data["ShipManager"].ToString());
            rpt.SetParameterValue("TodayDt", System.DateTime.Now.Date.ToString("dd/MM/yyyy"));
            rpt.SetParameterValue("ContractRefNo", dr_data["ContractReferenceNumber"].ToString());
            rpt.SetParameterValue("Resposinsiblities", dr_data["CRRResponsibilities"].ToString());
            rpt.SetParameterValue("MLCOwner", dr_data["MLCOwner"].ToString());
            rpt.SetParameterValue("LoginName", LoginName.ToString());
            rpt.SetParameterValue("Position", Position.ToString());
            CrystalReportViewer1.ReportSource = rpt;
            CrystalReportViewer1.DataBind();
        }
    }
    //Response.Redirect("PrintContract_mum.aspx?contactid=" + ContractIId);
    //        return;

    public void StandardTemplate()
    {
        string sql = "EXEC printcontract " + ContractIId.ToString();
        DataTable dt_Data = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        if (dt_Data.Rows.Count > 0)
        {
            string imgURL = string.Empty;
            string sql2 = "Select CCT_ImageURL from CrewContract_Template with(nolock) where CCT_Id =" + ddl_ContractTemplate.SelectedValue;
            DataTable dtTemplate = Common.Execute_Procedures_Select_ByQueryCMS(sql2);
            if (dtTemplate.Rows.Count > 0)
            {
                imgURL = dtTemplate.Rows[0]["CCT_ImageURL"].ToString();
            }
            DataRow dr_data = dt_Data.Rows[0];
            string sql1 = "EXEC Get_EarningDeductionComponentsforContract " + ContractIId.ToString();
            DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql1);
            CrystalReportViewer1.Visible = true;
            CrystalReportViewer1.ReportSource = rpt;
            rpt.Load(Server.MapPath("CrewContractDetails_Standard.rpt"));
            rpt.SetDataSource(dt);
            rpt.SetParameterValue("FullName", dr_data["firstname"].ToString());
            rpt.SetParameterValue("DateofBirth", dr_data["dateofbirth"].ToString());
            rpt.SetParameterValue("PlaceOfBirth", dr_data["PlaceOfBirth"].ToString());
            rpt.SetParameterValue("Passport", dr_data["passport"].ToString());
            rpt.SetParameterValue("PIssueDt", dr_data["PIssueDt"].ToString());
            rpt.SetParameterValue("PIssuePlace", dr_data["PIssuePlace"].ToString());
            rpt.SetParameterValue("SeamenBook", dr_data["SeamanBook"].ToString());
            rpt.SetParameterValue("SIssueDt", dr_data["SIssueDt"].ToString());
            rpt.SetParameterValue("SIssuePlace", dr_data["SIssuePlace"].ToString());
            rpt.SetParameterValue("In_Port", dr_data["IN_PORT"].ToString());
            rpt.SetParameterValue("Vessel", dr_data["vesselname"].ToString());
            rpt.SetParameterValue("VesselFlag", dr_data["VesselFlag"].ToString());
            rpt.SetParameterValue("ImoNr", dr_data["ImoNo"].ToString());
            rpt.SetParameterValue("VesselType", dr_data["VesselTypeName"].ToString());
            rpt.SetParameterValue("Rank", dr_data["NewRankId"].ToString());
            rpt.SetParameterValue("TotalEarnings", Common.CastAsDecimal(dr_data["TotalEarnings"]));
            rpt.SetParameterValue("TotalDeductions", Common.CastAsDecimal(dr_data["TotalDeductions"]));
            rpt.SetParameterValue("ExtraOTRate", Common.CastAsDecimal(dr_data["ExtraOTRate"]));
            rpt.SetParameterValue("PEngagement", dr_data["PEngagement"].ToString());
            rpt.SetParameterValue("Duration", Common.CastAsInt32(dr_data["duration"]));
            rpt.SetParameterValue("StartDt", dr_data["StartDate"].ToString());
            rpt.SetParameterValue("EndDt", dr_data["EndDate"].ToString());
            // rpt.SetParameterValue("TravelPay", Common.CastAsDecimal(dr_data["TravelPayAmount"]));
            rpt.SetParameterValue("@ContractId", Convert.ToInt32(ContractIId));
            rpt.SetParameterValue("Currency", dr_data["Currency"].ToString());
            rpt.SetParameterValue("ManningAgent", dr_data["ManningAgent"].ToString());
            rpt.SetParameterValue("RegNo", dr_data["RegNo"].ToString());
            rpt.SetParameterValue("RSPL", dr_data["RSPL"].ToString());
            rpt.SetParameterValue("RSPLValidityDt", dr_data["RPSLValidityDt"].ToString());
            rpt.SetParameterValue("MainingAgentContact", dr_data["ManningAgentContact"].ToString());
            rpt.SetParameterValue("ManningAgentEmail", dr_data["ManningAgentEmail"].ToString());
            rpt.SetParameterValue("OwnerAgent", dr_data["OwnerAgent"].ToString());
            rpt.SetParameterValue("GraphicLocation", imgURL);
            rpt.SetParameterValue("WageScale", dr_data["ScaleName"].ToString());
            rpt.SetParameterValue("ShipManager", dr_data["ShipManager"].ToString());
            rpt.SetParameterValue("TodayDt", System.DateTime.Now.Date.ToString("dd/MM/yyyy"));
            rpt.SetParameterValue("ContractRefNo", dr_data["ContractReferenceNumber"].ToString());
            //CrystalReportViewer1.ReportSource = rpt;
            //CrystalReportViewer1.DataBind();
        }
    }

    protected void PrintCrewContract()
    {
        if (Convert.ToInt32(ddl_ContractTemplate.SelectedValue) > 0)
        {
            String sql;
            sql = "Select CCT_PageURL from CrewContract_Template with(nolock) where CCT_Id = " + ddl_ContractTemplate.SelectedValue + "";
            DataTable dtCrewContractTemplate = Common.Execute_Procedures_Select_ByQueryCMS(sql);
            if (dtCrewContractTemplate.Rows.Count > 0)
            {
                string ImageURL;
                DataRow row = dtCrewContractTemplate.Rows[0];
                ImageURL = row["CCT_PageURL"].ToString();
                DataTable dt;
                dt = CrewContract.Select_Contract_Details1(Convert.ToInt32(ContractIId));
                //Session.Add("Cont_id", ContractIId.ToString());
                //Session.Add("NewRankId", Convert.ToInt32(dt.Rows[0]["NewRankId"].ToString()));
                //Session.Add("ContractStartDate", txt_StartDate.Text);
                //Session.Add("ContractSeniority", Txt_Seniority.Text);
                //Session.Add("ContractNationality", Convert.ToInt32(dt.Rows[0]["nationalityid"].ToString()));
                //Session.Add("ContractWageScaleId", Convert.ToInt32(dt.Rows[0]["wagescaleid"].ToString()));
                if (Convert.ToInt32(ddl_ContractTemplate.SelectedValue) == 1)
                {
                    // ShowReport();
                    StandardTemplate();
                }
                else if (Convert.ToInt32(ddl_ContractTemplate.SelectedValue) == 2)
                {
                    EnergiosTemplate();
                }
                else if (Convert.ToInt32(ddl_ContractTemplate.SelectedValue) == 3)
                {
                    POEATemplate();
                }
                else if (Convert.ToInt32(ddl_ContractTemplate.SelectedValue) == 4)
                {
                    EnergiosAppointmentLetterTemplate();
                }
            }
        }
        else
        {
            this.lblmessage.Text = "Select any Contract template for Print.";
            ddl_ContractTemplate.Focus();
        }
    }
}
