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
using System.Net.Mail;
using System.Net;
using System.EnterpriseServices;
using System.IO;
using ClosedXML.Excel;
using System.Linq;
using DocumentFormat.OpenXml.Spreadsheet;


public partial class Modules_HRD_CrewPayroll_SummaryReportMonthWise_aspx : System.Web.UI.Page
{
    Authority Auth;
    AuthenticationManager auth1;

    public int CPBAID
    {
        get { return Common.CastAsInt32(ViewState["CPBAID"]); }
        set { ViewState["CPBAID"] = value; }
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
            BindUserList();
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

    //public DataTable BindUserList(int Level)
    //{
    //    string sql = " Select l.LoginId,FirstName+' '+LastName as UserName from dbo.UserLogin l " +
    //                " inner join CrewPlanningApprovalAuthority a on a.LoginID=l.LoginId " +
    //                " where StatusID='A' and a.Approval" + Level + "=1 order by UserName ";
    //    DataTable dt = Budget.getTable(sql).Tables[0];
    //    DataRow dr = dt.NewRow();
    //    dr[0] = 0;
    //    dr[1] = "<Select>";
    //    dt.Rows.InsertAt(dr, 0);
    //    return dt;
    //}

    protected void BindUserList()
    {
        DataSet ds = Budget.getTable("Select l.LoginId,FirstName+' '+LastName as UserName from dbo.UserLogin l  where StatusID='A' ");
        ddlFirstApproval.DataSource = ds.Tables[0];
        ddlFirstApproval.DataValueField = "LoginId";
        ddlFirstApproval.DataTextField = "UserName";
        ddlFirstApproval.DataBind();
        ddlFirstApproval.Items.Insert(0, new ListItem("<Select>", "0"));

        ddlSecondApproval.DataSource = ds.Tables[0];
        ddlSecondApproval.DataValueField = "LoginId";
        ddlSecondApproval.DataTextField = "UserName";
        ddlSecondApproval.DataBind();
        ddlSecondApproval.Items.Insert(0, new ListItem("<Select>", "0"));
    }

    protected void SearchData(int VesselId, int Month, int Year)
    {
        DataTable dt_Data = Common.Execute_Procedures_Select_ByQueryCMS("EXEC DBO.GetPayrollSummaryReport " + VesselId + "," + Year.ToString() + "," + Month.ToString() + ",1");
        rptPersonal.DataSource = dt_Data;
        rptPersonal.DataBind();
        lblTotCrew.Text = " ( " + dt_Data.Rows.Count.ToString() + " ) Crew";
       // Session["dt_Data"] = dt_Data;
        if (dt_Data.Rows.Count > 0)
        {
            decimal TotalMonthlyWages = 0;
            decimal TotalExtraEarnings = 0;
            decimal TotalDeduction = 0;
            decimal TotalCurrentMonth = 0;
            decimal TotalPreviousMonth = 0;
            decimal TotalClosingBalance = 0;
            decimal TotalEarnings = 0;
            int verifiedCount = 0;
            int unverifiedCount = 0;
            int totalCount = 0;
            foreach (DataRow dr in dt_Data.Rows)
            {
                TotalMonthlyWages = TotalMonthlyWages + Math.Round(Common.CastAsDecimal(dr["MonthlyWages"].ToString()), 2);
                TotalExtraEarnings = TotalExtraEarnings + Math.Round(Common.CastAsDecimal(dr["ExtraEarings"].ToString()), 2);
                TotalDeduction = TotalDeduction + Math.Round(Common.CastAsDecimal(dr["TotalDeductions"].ToString()), 2);
                TotalCurrentMonth = TotalCurrentMonth + Math.Round(Common.CastAsDecimal(dr["CurMonBal"].ToString()), 2);
                TotalPreviousMonth = TotalPreviousMonth + Math.Round(Common.CastAsDecimal(dr["PrevMonBal"].ToString()), 2);
                TotalClosingBalance = TotalClosingBalance + Math.Round(Common.CastAsDecimal(dr["BalanceOfWages"].ToString()), 2);
                TotalEarnings = TotalEarnings + Math.Round(Common.CastAsDecimal(dr["Earnings"].ToString()), 2);

                if (dr["Verified"].ToString() == "True")
                {
                    verifiedCount = verifiedCount + 1;
                }
                else
                {
                    unverifiedCount = unverifiedCount + 1;
                }
                totalCount = totalCount + 1;
            }

            lblTotalMonthlyWages.Text = Math.Round(TotalMonthlyWages, 2).ToString();
            lblTotalExtraEarnings.Text = Math.Round(TotalExtraEarnings, 2).ToString();
            lblTotalDeduction.Text = Math.Round(TotalDeduction, 2).ToString();
            lblTotalCurrentMonth.Text = Math.Round(TotalCurrentMonth, 2).ToString();
            lblTotalPreviousMonth.Text = Math.Round(TotalPreviousMonth, 2).ToString();
            lblTotalClosingBalance.Text = Math.Round(TotalClosingBalance, 2).ToString();
            lblTotalEarnings.Text = Math.Round(TotalEarnings, 2).ToString();
            lblVerfiedCount.Text = verifiedCount.ToString();
            lblUnverfiedCount.Text = unverifiedCount.ToString();
            lblMonthYear.Text = ConvertMMMToM(Month) + " " + Year.ToString();
            if (totalCount - verifiedCount == 0)
            {
                PortageBillApproval(VesselId, Month, Year);
            }
        }
        
    }

    protected void PortageBillApproval(int VesselId, int Month, int Year )
    {

            DataTable dtApprovalDetail = Common.Execute_Procedures_Select_ByQueryCMS("EXEC DBO.GetCrewPortageBillApproval " + VesselId + "," + Year.ToString() + "," + Month.ToString() + "");
            if (dtApprovalDetail != null)
            {
                if (dtApprovalDetail.Rows.Count > 0)
                {
                    hdnCPBAID.Value = dtApprovalDetail.Rows[0]["CPBAID"].ToString();
                    CPBAID = Convert.ToInt32(hdnCPBAID.Value);
                    if (!string.IsNullOrWhiteSpace(dtApprovalDetail.Rows[0]["ApprovalSentRemark"].ToString()) && !string.IsNullOrWhiteSpace(dtApprovalDetail.Rows[0]["FirstApprovedRemark"].ToString()) &&  !string.IsNullOrWhiteSpace(dtApprovalDetail.Rows[0]["SecondApprovedRemark"].ToString()))
                    {
                        ddlFirstApproval.Enabled = false;
                        ddlSecondApproval.Enabled = false;
                        btnSubmitForApproval.Enabled = false;
                        btnSubmitForApproval.CssClass = "";
                        btnSubmit2.Enabled = false;
                        btnSubmit2.CssClass = "";
                    btnFirstApproval.Enabled = false;
                    btnFirstApproval.CssClass = "";
                    trSubmitforApproval.Visible = true;
                        trFirstApproval.Visible = true;
                        trSecondApproval.Visible = true;
                        lblSubmitforApproval.Text = dtApprovalDetail.Rows[0]["ApprovalSent"].ToString() + "/" + dtApprovalDetail.Rows[0]["ApprovalSentOn"].ToString();
                        lblFirstApprovalByOn.Text = dtApprovalDetail.Rows[0]["FirstApproval"].ToString() + "/" + dtApprovalDetail.Rows[0]["FirstApprovedon"].ToString();
                        lblSecondApprovedByOn.Text = dtApprovalDetail.Rows[0]["SecondApproval"].ToString() + "/" + dtApprovalDetail.Rows[0]["SecondApprovedon"].ToString();
                        txtSubmitApprovalRemarks.Text = dtApprovalDetail.Rows[0]["ApprovalSentRemark"].ToString();
                        txtFirstApprovalRemarks.Text = dtApprovalDetail.Rows[0]["FirstApprovedRemark"].ToString();
                        txtSecondApproval.Text = dtApprovalDetail.Rows[0]["SecondApprovedRemark"].ToString();
                        ddlFirstApproval.SelectedValue = dtApprovalDetail.Rows[0]["ApprovalSentBy"].ToString();
                        ddlSecondApproval.SelectedValue = dtApprovalDetail.Rows[0]["SecondApprovedBy"].ToString();
                    }
                    else if(!string.IsNullOrWhiteSpace(dtApprovalDetail.Rows[0]["ApprovalSentRemark"].ToString()) && !string.IsNullOrWhiteSpace(dtApprovalDetail.Rows[0]["FirstApprovedRemark"].ToString()) && string.IsNullOrWhiteSpace(dtApprovalDetail.Rows[0]["SecondApprovedRemark"].ToString()))
                    {
                        ddlFirstApproval.Enabled = false;
                        ddlSecondApproval.Enabled = false;
                        trSubmitforApproval.Visible = true;
                        trFirstApproval.Visible = true;
                        trSecondApproval.Visible = true;
                        btnSubmitForApproval.Enabled = false;
                        btnFirstApproval.Enabled = false;
                        btnSubmit2.Enabled = false;
                    btnSubmitForApproval.CssClass = "";
                    btnSubmit2.CssClass = "";
                    btnFirstApproval.CssClass = "";
                    if (Convert.ToInt32(ddlFirstApproval.SelectedValue) == Common.CastAsInt32(Session["loginid"]))
                        {
                            btnSubmit2.Enabled = true;
                        btnSubmit2.CssClass = "btn";
                    }

                    lblSubmitforApproval.Text = dtApprovalDetail.Rows[0]["ApprovalSent"].ToString() + "/" + dtApprovalDetail.Rows[0]["ApprovalSentOn"].ToString();
                        lblFirstApprovalByOn.Text = dtApprovalDetail.Rows[0]["FirstApproval"].ToString() + "/" + dtApprovalDetail.Rows[0]["FirstApprovedon"].ToString();
                       
                        txtSubmitApprovalRemarks.Text = dtApprovalDetail.Rows[0]["ApprovalSentRemark"].ToString();
                        txtFirstApprovalRemarks.Text = dtApprovalDetail.Rows[0]["FirstApprovedRemark"].ToString();
                  
                        ddlFirstApproval.SelectedValue = dtApprovalDetail.Rows[0]["ApprovalSentBy"].ToString();
                        ddlSecondApproval.SelectedValue = dtApprovalDetail.Rows[0]["SecondApprovedBy"].ToString();
                    }
                    else if (!string.IsNullOrWhiteSpace(dtApprovalDetail.Rows[0]["ApprovalSentRemark"].ToString()) && string.IsNullOrWhiteSpace(dtApprovalDetail.Rows[0]["FirstApprovedRemark"].ToString()) && string.IsNullOrWhiteSpace(dtApprovalDetail.Rows[0]["SecondApprovedRemark"].ToString()))
                    {
                    
                       ddlFirstApproval.Enabled = false;
                       ddlSecondApproval.Enabled = false;
                       trSubmitforApproval.Visible = true;
                        trFirstApproval.Visible = true;
                        trSecondApproval.Visible = false;
                        btnSubmitForApproval.Enabled = false;
                        btnFirstApproval.Enabled = false;
                        btnSubmit2.Enabled = false;
                    btnSubmitForApproval.CssClass = "";
                    btnSubmit2.CssClass = "";
                    btnFirstApproval.CssClass = "";
                    if (Convert.ToInt32(ddlFirstApproval.SelectedValue) == Common.CastAsInt32(Session["loginid"]))
                    {
                        btnFirstApproval.Enabled = true;
                        btnFirstApproval.CssClass = "btn";
                    }

                    lblSubmitforApproval.Text = dtApprovalDetail.Rows[0]["ApprovalSent"].ToString() + "/" + dtApprovalDetail.Rows[0]["ApprovalSentOn"].ToString();
                        

                        txtSubmitApprovalRemarks.Text = dtApprovalDetail.Rows[0]["ApprovalSentRemark"].ToString();
                        
                        ddlFirstApproval.SelectedValue = dtApprovalDetail.Rows[0]["ApprovalSentBy"].ToString();
                        ddlSecondApproval.SelectedValue = dtApprovalDetail.Rows[0]["SecondApprovedBy"].ToString();
                    }
                    else
                    {
                        trSubmitforApproval.Visible = true;
                        trFirstApproval.Visible = false;
                        trSecondApproval.Visible = false;
                        btnSubmitForApproval.Enabled = true;
                        btnFirstApproval.Enabled = false;
                        btnSubmit2.Enabled = false;
                        ddlFirstApproval.SelectedIndex = 0;
                        ddlSecondApproval.SelectedIndex = 0;
                    ddlFirstApproval.Enabled = true;
                    ddlSecondApproval.Enabled = true;
                    btnSubmitForApproval.CssClass = "btn";
                    btnSubmit2.CssClass = "";
                    btnFirstApproval.CssClass = "";
                }

                   
                }
            else
            {
                trSubmitforApproval.Visible = true;
                trFirstApproval.Visible = false;
                trSecondApproval.Visible = false;
                btnSubmitForApproval.Enabled = true;
                btnFirstApproval.Enabled = false;
                btnSubmit2.Enabled = false;
                ddlFirstApproval.SelectedIndex = 0;
                ddlSecondApproval.SelectedIndex = 0;
                ddlFirstApproval.Enabled = true;
                ddlSecondApproval.Enabled = true;
                btnSubmitForApproval.CssClass = "btn";
                btnSubmit2.CssClass = "";
                btnFirstApproval.CssClass = "";
            }

        }
            else
            {
            trSubmitforApproval.Visible = true;
            trFirstApproval.Visible = false;
            trSecondApproval.Visible = false;
            btnSubmitForApproval.Enabled = true;
            btnFirstApproval.Enabled = false;
            btnSubmit2.Enabled = false;
            ddlFirstApproval.SelectedIndex = 0;
            ddlSecondApproval.SelectedIndex = 0;
            ddlFirstApproval.Enabled = true;
            ddlSecondApproval.Enabled = true;
            btnSubmitForApproval.CssClass = "btn";
            btnSubmit2.CssClass = "";
            btnFirstApproval.CssClass = "";
        }
        
    }

    protected void btn_search_Click(object sender, EventArgs e)
    {

        if (ddl_Vessel.SelectedValue == "0")
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "ddd", "alert('Please select vessel.')", true);
            ddl_Vessel.Focus();
            return;
        }
        if (ddl_Month.SelectedValue == "0")
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "ddd", "alert('Please select month.')", true);
            ddl_Month.Focus();
            return;
        }
        rptPersonal.DataSource = null;
        rptPersonal.DataBind();
        int VesselId, Month, Year;
        VesselId = Convert.ToInt32(ddl_Vessel.SelectedValue);
        Month = Convert.ToInt32(ddl_Month.SelectedValue);
        Year = Convert.ToInt32(ddl_Year.SelectedValue);
        clearApprovalData();
        SearchData(VesselId, Month, Year);

    }

    public string ConvertMMMToM(int M)
    {
        switch (M)
        {
            
            case 1:
                return "JANUARY";
            case 2:
                return "FEBRUARY";
            case 3:
                return "MARCH";
            case 4:
                return "APRIL";
            case 5:
                return "MAY";
            case 6:
                return "JUNE";
            case 7:
                return "JULY";
            case 8:
                return "AUGUST";
            case 9:
                return "SEPTEMBER";
            case 10:
                return "OCTOBER";
            case 11:
                return "NOVEMBER";
            case 12:
                return "DECEMBER";
            default:
                return "";
        }
    }

    protected void btnSubmitForApproval_Click(object sender, EventArgs e)
    {
        if (ddlFirstApproval.SelectedValue == "0")
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "ddd", "alert('Please select First approval.')", true);
            ddlFirstApproval.Focus();
            return;
        }
        if (ddlSecondApproval.SelectedValue == "0")
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "ddd", "alert('Please select Second approval.')", true);
            ddlSecondApproval.Focus();
            return;
        }
        if (string.IsNullOrWhiteSpace(txtSubmitApprovalRemarks.Text.Trim()))
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "ddd", "alert('Please enter remarks.')", true);
            txtSubmitApprovalRemarks.Focus();
            return;
        }

        Common.Set_Procedures("InsertUpdateCrewPortageBillApproval");
        Common.Set_ParameterLength(8);
        Common.Set_Parameters(
                new MyParameter("@CPBAID", CPBAID),
                new MyParameter("@VesselId", ddl_Vessel.SelectedValue),
                new MyParameter("@payMonth", ddl_Month.SelectedValue),
                new MyParameter("@PayYear", ddl_Year.SelectedValue),
                new MyParameter("@ApprovalSentBy", Common.CastAsInt32(Session["loginid"])),
                new MyParameter("@FirstApproval", Convert.ToInt32(ddlFirstApproval.SelectedValue)),
                new MyParameter("@SecondApproval", Convert.ToInt32(ddlSecondApproval.SelectedValue)),
                new MyParameter("@ApprovalSentRemarks", txtSubmitApprovalRemarks.Text.Trim()));
                

        DataSet ds = new DataSet();
        bool res = Common.Execute_Procedures_IUD_CMS(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            CPBAID = Common.CastAsInt32(ds.Tables[0].Rows[0][0].ToString());
            hdnCPBAID.Value = ds.Tables[0].Rows[0][0].ToString();
            btnSubmitForApproval.Enabled = false;
            PortageBillApproval(Convert.ToInt32(ddl_Vessel.SelectedValue), Convert.ToInt32(ddl_Month.SelectedValue), Convert.ToInt32(ddl_Year.SelectedValue));
            DataTable dtEmail = Common.Execute_Procedures_Select_ByQuery("Select FA.FirstName + ' ' + FA.LastName As UserName, Email  from userLogin FA with(nolock) where FA.Loginid = " +   ddlFirstApproval.SelectedValue + " UNION ALL Select SA.FirstName + ' ' + SA.LastName As UserName, Email  from userLogin SA with(nolock) where SA.Loginid = " + ddlSecondApproval.SelectedValue);
            DataTable dtUserName = Common.Execute_Procedures_Select_ByQuery("Select FirstName + ' ' + LastName from userLogin FA with(nolock) where Loginid = " + Common.CastAsInt32(Session["loginid"]));
            String userName = String.Empty;
            if (dtUserName.Rows.Count > 0)
            {
                userName = dtUserName.Rows[0][0].ToString();
            }
            if (dtEmail.Rows.Count > 0)
            {
                foreach (DataRow dr in dtEmail.Rows)
                {
                    if (! string.IsNullOrWhiteSpace(dr["Email"].ToString()))
                    {
                        SendApprovalEmail(userName, dr["Email"].ToString(), dr["UserName"].ToString());
                    }
                }
            }
        }
    }

    protected void btnFirstApproval_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtFirstApprovalRemarks.Text.Trim()))
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "ddd", "alert('Please enter remarks.')", true);
            txtFirstApprovalRemarks.Focus();
            return;
        }
        string sql = "update CrewPortageBillApproval set FirstApprovedRemark='" + txtFirstApprovalRemarks.Text.Trim().Replace("'", "`") + "',FirstApprovedon=getdate() where CPBAID=" + CPBAID + "  select -1";
        DataSet Dt = Budget.getTable(sql);
        if (Dt != null)
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "ddd", "alert('Approval submitted successfully.')", true);
            btnFirstApproval.Enabled = false;
            PortageBillApproval(Convert.ToInt32(ddl_Vessel.SelectedValue), Convert.ToInt32(ddl_Month.SelectedValue), Convert.ToInt32(ddl_Year.SelectedValue));
        }
    }
    protected void btnSubmit2_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtSecondApproval.Text.Trim()))
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "ddd", "alert('Please enter remarks.')", true);
            txtSecondApproval.Focus();
            return;
        }
        string sql = "update CrewPortageBillApproval set SecondApprovedRemark='" + txtSubmitApprovalRemarks.Text.Trim().Replace("'", "`") + "',SecondApprovedon=getdate(),IsApprovalPending = 1 where CPBAID=" + CPBAID + "  select -1";
        DataSet Dt = Budget.getTable(sql);
        if (Dt != null)
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "ddd", "alert('Approval submitted successfully.')", true);
            btnSubmit2.Enabled = false;
            PortageBillApproval(Convert.ToInt32(ddl_Vessel.SelectedValue), Convert.ToInt32(ddl_Month.SelectedValue), Convert.ToInt32(ddl_Year.SelectedValue));
        }
    }

    private void SendApprovalEmail(String senderUserName, string Approvalmail, String ApprovalName)
    {
        string MailClient = ConfigurationManager.AppSettings["SMTPServerName"].ToString();
        int Port = Convert.ToInt32(ConfigurationManager.AppSettings["SMTPPort"]);
        string SenderAddress = ConfigurationManager.AppSettings["FromAddress"];
        string SenderUserName = ConfigurationManager.AppSettings["SMTPUserName"];
        string SenderPass = ConfigurationManager.AppSettings["SMTPUserPwd"];
        System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                                 | SecurityProtocolType.Tls11
                                 | SecurityProtocolType.Tls12;
        string constr = ConfigurationManager.ConnectionStrings["eMANAGER"].ConnectionString;
        string activationCode = Guid.NewGuid().ToString();
       
        using (MailMessage mm = new MailMessage(SenderAddress, Approvalmail))
        {

            mm.Subject = "Vessel: " + ddl_Vessel.SelectedItem.Text + " Portage Bill : " + lblMonthYear.Text + " - Approval Request";
            string body = "Hello " + ApprovalName + ",";
            body += "<br /><br />Vessel: " + ddl_Vessel.SelectedItem.Text + " Portage Bill : " + lblMonthYear.Text;
            body += "<br /><br />Above mentioned Portage Bill has been verified.";
            body += "<br /><br />Kindly approve it in the system for further Process.";
            body += "<br /><br />Thank You,";
            body += "<br /><br />" + senderUserName;
            mm.Body = body;
            mm.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = MailClient;
            smtp.EnableSsl = true;
            NetworkCredential NetworkCred = new NetworkCredential(SenderUserName, SenderPass);
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = NetworkCred;
            smtp.Port = Port;
            smtp.Send(mm);
        }
    }

    protected void btnDownload_Click(object sender, EventArgs e)
    {
        //Response.Redirect("~/Modules/HRD/CrewPayroll/PayrollSheetMonthWise.aspx");

        System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "openModal", "window.open('PayrollSheetMonthWise.aspx' ,'_blank');", true);
    }

    protected void btnImportShipData_Click(object sender, EventArgs e)
    {
        //Response.Redirect("~/Modules/HRD/CrewPayroll/PayrollDataImport.aspx");
        System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "openModal", "window.open('PayrollDataImport.aspx' ,'_blank');", true);
    }

    protected void clearApprovalData()
    {   trSubmitforApproval.Visible = false;
        trFirstApproval.Visible = false;
        trSecondApproval.Visible = false;
        txtSubmitApprovalRemarks.Text = "";
        txtFirstApprovalRemarks.Text = "";
        txtSecondApproval.Text = "";
        ddlFirstApproval.SelectedIndex = 0;
        ddlSecondApproval.SelectedIndex = 0;
    }

    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddl_Vessel.SelectedValue == "0")
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "ddd", "alert('Please select vessel.')", true);
                ddl_Vessel.Focus();
                return;
            }
            if (ddl_Month.SelectedValue == "0")
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "ddd", "alert('Please select month.')", true);
                ddl_Month.Focus();
                return;
            }
            

            int VesselId, Month, Year;
                string vesselcode = string.Empty;
                VesselId = Convert.ToInt32(ddl_Vessel.SelectedValue);
                Month = Convert.ToInt32(ddl_Month.SelectedValue);
                Year = Convert.ToInt32(ddl_Year.SelectedValue);
                DataTable dt_Vessel = Common.Execute_Procedures_Select_ByQueryCMS("Select VesselCode from Vessel with(nolock) Where VesselId= " + VesselId);
                if (dt_Vessel.Rows.Count > 0)
                {
                    vesselcode = dt_Vessel.Rows[0]["VesselCode"].ToString();
                }

                DataTable dt_Data = Common.Execute_Procedures_Select_ByQueryCMS("EXEC DBO.Get_PortageBillHeaderDetailsReport " + VesselId + "," + Year.ToString() + "," + Month.ToString());
                GridView1.DataSource = dt_Data;
                GridView1.DataBind();
            if (GridView1.Rows.Count > 0)
            {
                string Filename = vesselcode + "_PortageBill_Report_" + Month + "_" + Year + ".xlsx";
                DataTable dt = new DataTable("Sheet1");
                foreach (TableCell cell in GridView1.HeaderRow.Cells)
                {
                    dt.Columns.Add(cell.Text);
                }
                foreach (GridViewRow row in GridView1.Rows)
                {
                    dt.Rows.Add();
                    for (int i = 0; i < row.Cells.Count; i++)
                    {
                        if (row.Cells[i].Controls.Count > 0)
                        {
                            dt.Rows[dt.Rows.Count - 1][i] = (row.Cells[i].Controls[1] as Label).Text.Replace("&nbsp;", "");
                        }
                        else
                        {
                            dt.Rows[dt.Rows.Count - 1][i] = row.Cells[i].Text.Replace("&nbsp;", "");
                        }
                    }
                }

                using (XLWorkbook wb = new XLWorkbook())
                {
                    int rowcount = dt.Rows.Count + 2;
                    var ws = wb.Worksheets.Add("Sheet1");

                    ws.Cell("A1").Value = "Crew List";
                    ws.Cell("H1").Value = "Earnings";
                    ws.Cell("O1").Value = "Other Earnings";
                    ws.Cell("AA1").Value = "";
                    ws.Cell("AB1").Value = "Deductions";
                    ws.Cell("AH1").Value = "Other Deductions";
                    ws.Cell("AL1").Value = "";
                    ws.Cell("AM1").Value = "Balance of Wages";
                    var range = ws.Range("A1:G1");
                    var range1 = ws.Range("H1:N1");
                    var range2 = ws.Range("O1:Z1");
                    var range3 = ws.Range("AA1:AA1");
                    var range4 = ws.Range("AB1:AG1");
                    var range5 = ws.Range("AH1:AK1");
                    var range6 = ws.Range("AL1:AL1");
                    var range7 = ws.Range("AM1:AO1");

                    range.Merge().Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    range1.Merge().Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    range2.Merge().Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    range3.Merge().Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    range4.Merge().Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    range5.Merge().Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    range6.Merge().Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    range7.Merge().Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                    //range.Merge().Style.Font.SetBold().Font.SetFontName("Calibri").Font.FontSize = 12;
                    //range1.Merge().Style.Font.SetBold().Font.SetFontName("Calibri").Font.FontSize = 12;
                    //range2.Merge().Style.Font.SetBold().Font.SetFontName("Calibri").Font.FontSize = 12;
                    //range3.Merge().Style.Font.SetBold().Font.SetFontName("Calibri").Font.FontSize = 12;
                    //range4.Merge().Style.Font.SetBold().Font.SetFontName("Calibri").Font.FontSize = 12;
                    //range5.Merge().Style.Font.SetBold().Font.SetFontName("Calibri").Font.FontSize = 12;
                    //range6.Merge().Style.Font.SetBold().Font.SetFontName("Calibri").Font.FontSize = 12;
                    //range7.Merge().Style.Font.SetBold().Font.SetFontName("Calibri").Font.FontSize = 12;
                    //ws.Range("A1:G1,A25:G25").Style.Fill.BackgroundColor = XLColor.LightGreen;
                    //ws.Range("H1:N1,H25:N25").Style.Fill.BackgroundColor = XLColor.LightPink;

                    //var bg1 = ws.Ranges("A1:A" + rowcount + ", B1:B" + rowcount + ", C1:C" + rowcount + ", D1:D" + rowcount + ", E1:E" + rowcount + ", F1:F" + rowcount + ", G1:G" + rowcount + "");
                    //var bg2 = ws.Ranges("H1:H" + rowcount + ", I1:I" + rowcount + ", J1:J" + rowcount + ", K1:K" + rowcount + ", L1:L" + rowcount + ", M1:M" + rowcount + ", N1:N" + rowcount + "");
                    //var bg3 = ws.Ranges("O1:O" + rowcount + ", P1:P" + rowcount + ", Q1:Q" + rowcount + ", R1:R" + rowcount + ", S1:S" + rowcount + ", T1:T" + rowcount + ", U1:U" + rowcount + ",V1:V" + rowcount + ",W1:W" + rowcount + ",X1:X" + rowcount + ",Y1:Y" + rowcount + ",Z1:Z" + rowcount + ",AA1:AA" + rowcount + "");
                    //var bg4 = ws.Ranges("AB1:AB" + rowcount + ", AB1:AB" + rowcount + ", AC1:AC" + rowcount + ", AD1:AD" + rowcount + ", AE1:AE" + rowcount + ", AF1:AF" + rowcount + ", AG1:AG" + rowcount + ",AH1:AH" + rowcount + ", AI1:AI" + rowcount + ", AJ1:AJ" + rowcount + ", AK1:AK" + rowcount + ", AL1:AL" + rowcount + "");
                    //var bg5 = ws.Ranges("AM1:AM" + rowcount + ", AN1:AN" + rowcount + ", AO1:AO" + rowcount + "");
                    var bg1 = ws.Ranges("A1:G" + rowcount + "");
                    var bg2 = ws.Ranges("H1:N" + rowcount + "");
                    var bg3 = ws.Ranges("O1:AA" + rowcount + "");
                    var bg4 = ws.Ranges("AB1:AL" + rowcount + "");
                    var bg5 = ws.Ranges("AM1:AO" + rowcount + "");
                    bg1.Style.Fill.BackgroundColor = XLColor.LightGreen;
                    bg2.Style.Fill.BackgroundColor = XLColor.BabyPink;
                    bg3.Style.Fill.BackgroundColor = XLColor.PeachOrange;
                    bg4.Style.Fill.BackgroundColor = XLColor.PowderBlue;
                    bg5.Style.Fill.BackgroundColor = XLColor.PeachPuff;

                    //var rowBold = ws.LastRowUsed();
                    //var rowno = ws.LastRowUsed().RowNumber();
                    //rowBold.Style.Font.SetBold();


                    ws.Cell(2, 1).InsertTable(dt);
                    var row1 = ws.Row(1);
                    var row2 = ws.Row(2);
                    row1.Style.Font.SetBold().Font.SetFontName("Calibri").Font.FontSize = 12;
                    row2.Style.Font.SetBold().Font.SetFontName("Calibri").Font.FontSize = 12;

                    row1.Style.Font.FontColor = XLColor.FromHtml("#FF010101");
                    row2.Style.Font.FontColor = XLColor.FromHtml("#FF010101");

                    ws.Range("A1:AO" + rowcount + "").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    ws.Range("A1:AO" + rowcount + "").Style.Border.InsideBorder = XLBorderStyleValues.Dotted;
                    ws.Range("A1:AO" + rowcount + "").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    ws.Range("A1:AO" + rowcount + "").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    ws.Range("A1:AO" + rowcount + "").Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    ws.Range("A1:AO" + rowcount + "").Style.Border.TopBorder = XLBorderStyleValues.Thin;

                    ws.Ranges("A" + rowcount + ":AO" + rowcount + "").Style.Font.SetBold().Font.SetFontName("Calibri").Font.FontSize = 12;
                    ws.Columns().AdjustToContents();
                    ws.Tables.FirstOrDefault().ShowAutoFilter = false; // Disable AutoFilter.
                    ws.SheetView.FreezeRows(1);
                    ws.SheetView.FreezeColumns(2);
                    ws.SheetView.FreezeColumns(3);
                    ws.SheetView.FreezeColumns(4);
                    ws.SheetView.FreezeColumns(5);
                    ws.SheetView.FreezeColumns(6);
                    ws.SheetView.FreezeColumns(7);

                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=" + Filename);
                    using (MemoryStream MyMemoryStream = new MemoryStream())
                    {
                        wb.SaveAs(MyMemoryStream);
                        MyMemoryStream.WriteTo(Response.OutputStream);
                        Response.Flush();
                        Response.End();
                    }
                }

                //Response.ClearContent();
                //Response.Clear();
                //Response.Buffer = true;
                //Response.ClearHeaders();
                //Response.Charset = "";
                ////Response.ContentType = "application/vnd.ms-excel";
                //Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                //Response.AddHeader("Content-Disposition", "attachment;Filename=" + Filename);
                //StringWriter str = new StringWriter();
                //HtmlTextWriter htw = new HtmlTextWriter(str);
                //Response.Cache.SetCacheability(HttpCacheability.NoCache);
                //GridView1.AllowPaging = false;
                //GridView1.GridLines = GridLines.Both;
                //GridView1.HeaderStyle.Font.Bold = true;
                //GridView1.RenderControl(htw);
                //Response.Write(str.ToString());
                //Response.Flush();
                //Response.Close();
                //Response.End();
            }
        }
        catch (Exception ex)
        {
            lbl_Message.Text = ex.Message;
        }
    }
    public void HeaderBound(object sender, EventArgs e)
    {
        GridViewRow row = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);

        TableHeaderCell tec = new TableHeaderCell();
        tec = new TableHeaderCell();
        tec.ColumnSpan = 7;
        tec.Text = "Crew List";
        //   tec.Width = 600;
        tec.HorizontalAlign = HorizontalAlign.Center;
        row.Controls.Add(tec);

        tec = new TableHeaderCell();
        tec.ColumnSpan = 7;
        tec.HorizontalAlign = HorizontalAlign.Center;
        tec.Text = "Earnings";
        row.Controls.Add(tec);

        tec = new TableHeaderCell();
        tec.ColumnSpan = 12;
        tec.HorizontalAlign = HorizontalAlign.Center;
        tec.Text = "Other Earnings";
        row.Controls.Add(tec);

        tec = new TableHeaderCell();
        tec.ColumnSpan = 1;
        tec.HorizontalAlign = HorizontalAlign.Center;
        tec.Text = "";
        row.Controls.Add(tec);

        tec = new TableHeaderCell();
        tec.ColumnSpan = 6;
        tec.HorizontalAlign = HorizontalAlign.Center;
        tec.Text = "Deductions";
        row.Controls.Add(tec);

        tec = new TableHeaderCell();
        tec.ColumnSpan = 4;
        tec.HorizontalAlign = HorizontalAlign.Center;
        tec.Text = "Other Deductions";
        row.Controls.Add(tec);

        tec = new TableHeaderCell();
        tec.ColumnSpan = 1;
        tec.HorizontalAlign = HorizontalAlign.Center;
        tec.Text = "";
        row.Controls.Add(tec);

        tec = new TableHeaderCell();
        tec.ColumnSpan = 3;
        tec.HorizontalAlign = HorizontalAlign.Center;
        tec.Text = "Balance of Wages";
        row.Controls.Add(tec);
        GridView1.HeaderRow.Parent.Controls.AddAt(0, row);
    }
}
