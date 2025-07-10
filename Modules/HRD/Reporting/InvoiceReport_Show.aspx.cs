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

public partial class Reporting_InvoiceReport_Show : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        //========== Code to check report printing authority
        CrystalReportViewer1.HasPrintButton = Alerts.Check_ReportPrintingAuthority(Convert.ToInt32(Session["loginid"].ToString()), 98);
        //==========
        showreport();
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
    private void showreport()
    {
    
        string txtfromdate, txttodate="", rpttype, ddUser, ddvendor, ddvessel,ddinvtype;
        txtfromdate = Page.Request.QueryString["fromdate"].ToString();
        txttodate = Page.Request.QueryString["todate"].ToString();
        rpttype = Page.Request.QueryString["rbtype"].ToString();
        ddUser = Page.Request.QueryString["user"].ToString();
        ddvendor = Page.Request.QueryString["vendor"].ToString();
        ddvessel = Page.Request.QueryString["vessel"].ToString();
        ddinvtype = Page.Request.QueryString["invtype"].ToString();

        if (txtfromdate == "")
        {
            txtfromdate = System.DateTime.Today.Date.ToString("MM/dd/yyyy");
            txttodate = System.DateTime.Today.Date.ToString("MM/dd/yyyy");
        }
        if (txttodate == "")
        {
            txtfromdate = System.DateTime.Today.Date.ToString("MM/dd/yyyy");
            txttodate = System.DateTime.Today.Date.ToString("MM/dd/yyyy");
        }
        string whereclause = "";

        if (rpttype == "1")
        {
            int pmode = 0;
            whereclause = " where convert(smalldatetime,convert(char(10),createdon,101)) between '" + txtfromdate + "' and  '" + txttodate + "'";
            if (ddUser != "0")
            {
                pmode = 1;
                whereclause = whereclause + " and createdby='" + ddUser + "'";
            }
            if (ddvendor != "0")
            {
                whereclause = whereclause + " and vendorid='" + ddvendor + "' ";
            }
            if (ddvessel != "0")
            {
                    whereclause = whereclause + " and vesselid='" + ddvessel + "' ";
            }
            //else
            //{
            //    if (pmode==1)
            //        whereclause = whereclause + " and vesselid In (Select VesselId from UserVesselRelation Where Loginid=" + ddUser + ") ";
            //}

            whereclause = whereclause + " order by refno";
            DataTable dt = InvoiceReport.selectREceivedInvoiceDate(Convert.ToDateTime(txtfromdate), Convert.ToDateTime(txttodate), 0, whereclause);

            if (dt.Rows.Count > 0)
            {

                CrystalReportViewer1.Visible = true;
                CrystalReportViewer1.ReportSource = rpt;
                rpt.Load(Server.MapPath("RecivedInvoiceReport.rpt"));

                Session.Add("rptsource", dt);
                rpt.SetDataSource(dt);
                rpt.SetParameterValue("@FD", txtfromdate.ToString());
                rpt.SetParameterValue("@TD", txttodate.ToString());
                DataTable dt1 = PrintCrewList.selectCompanyDetails();
                foreach (DataRow dr in dt1.Rows)
                {
                    rpt.SetParameterValue("@Company", dr["CompanyName"].ToString());
                    //rpt.SetParameterValue("@Address", dr["Address"].ToString());
                    //rpt.SetParameterValue("@TelePhone", dr["TelephoneNumber"].ToString());
                    //rpt.SetParameterValue("@Fax", dr["Faxnumber"].ToString());
                    //rpt.SetParameterValue("@RegistrationNo", dr["RegistrationNo"].ToString());
                    //rpt.SetParameterValue("@Email", dr["Email1"].ToString());
                    //rpt.SetParameterValue("@Website", dr["Website"].ToString());

                }
                rpt.SetParameterValue("@HeaderText1", "Total Received  Invoice By Date");
            }
            else
            {
                lblMessage.Text = "No Record Found";
                CrystalReportViewer1.Visible = false;
            }
        }
        else if (rpttype==  "2")
        {
            int pmode = 0;
            whereclause = " where convert(smalldatetime,convert(char(10),approveddate,101)) between '" + txtfromdate + "' and '" + txttodate + "' and Statusid=1";
            if (ddUser != "0")
            {
                pmode = 1;
                whereclause = whereclause + " and approvedby='" + ddUser + "'";
            }
            if (ddvendor != "0")
            {
                whereclause = whereclause + " and vendorid='" + ddvendor + "' ";
            }
            if (ddvessel != "0")
            {
                whereclause = whereclause + " and vesselid='" + ddvessel + "' ";
            }
            //else
            //{
            //    if (pmode == 1)
            //        whereclause = whereclause + " and vesselid In (Select VesselId from UserVesselRelation Where Loginid=" + ddUser + ") ";
            //}

            DataTable dt = InvoiceReport.selectREceivedInvoiceDate(Convert.ToDateTime(txtfromdate), Convert.ToDateTime(txttodate), 1, whereclause);
            if (dt.Rows.Count > 0)
            {
                CrystalReportViewer1.Visible = true;
                //CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                CrystalReportViewer1.ReportSource = rpt;
                rpt.Load(Server.MapPath("ApprovedInvoiceReport.rpt"));

                Session.Add("rptsource", dt);
                rpt.SetDataSource(dt);
                rpt.SetParameterValue("@FD", "Period: " + txtfromdate.ToString() + " - " + txttodate.ToString());
                DataTable dt1 = PrintCrewList.selectCompanyDetails();
                foreach (DataRow dr in dt1.Rows)
                {
                    rpt.SetParameterValue("@Company", dr["CompanyName"].ToString());
                    //rpt.SetParameterValue("@Address", dr["Address"].ToString());
                    //rpt.SetParameterValue("@TelePhone", dr["TelephoneNumber"].ToString());
                    //rpt.SetParameterValue("@Fax", dr["Faxnumber"].ToString());
                    //rpt.SetParameterValue("@RegistrationNo", dr["RegistrationNo"].ToString());
                    //rpt.SetParameterValue("@Email", dr["Email1"].ToString());
                    //rpt.SetParameterValue("@Website", dr["Website"].ToString());
                }
                rpt.SetParameterValue("@HeaderText", "Total Approved Invoice By Date");
            }
            else
            {
                lblMessage.Text = "No Record Found";
                CrystalReportViewer1.Visible = false;
            }
        }

        else if (rpttype == "3")
        {
            int pmode = 0;
            string whereclause1, whereclause2;
            whereclause1 = " ";
            whereclause2 = "";
            whereclause2 = "  where convert(smalldatetime,convert(char(10),VoucherHeader.CreatedOn,101))  between '" + txtfromdate + "' and '" + txttodate + "'";
            if (ddUser != "0")
            {
                pmode = 1;
                //whereclause1 = whereclause1 + "  invoice.approvedby='" + ddUser + "' and ";
                whereclause1 = whereclause1 + "  VoucherHeader.CreatedBy='" + ddUser + "' and ";
            }
            if (ddvendor != "0")
            {
                whereclause1 = whereclause1 + "  invoice.vendorid='" + ddvendor + "' and ";
            }
            if (ddvessel != "0")
            {
                whereclause1 = whereclause1 + "  invoice.vesselid='" + ddvessel + "' and ";
            }
            //else
            //{
            //    if (pmode == 1)
            //        whereclause = whereclause + " and vesselid In (Select VesselId from UserVesselRelation Where Loginid=" + ddUser + ") ";
            //}
            DataTable dt = InvoiceReport.selectPayInvoiceDate(whereclause1, whereclause2);
            if (dt.Rows.Count > 0)
            {
                CrystalReportViewer1.Visible = true;
                //CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                CrystalReportViewer1.ReportSource = rpt;
                rpt.Load(Server.MapPath("InvoicePayByDateReport.rpt"));

                Session.Add("rptsource", dt);
                rpt.SetDataSource(dt);
                rpt.SetParameterValue("@FD", txtfromdate.ToString());
                rpt.SetParameterValue("@TD", txttodate.ToString());
                DataTable dt1 = PrintCrewList.selectCompanyDetails();
                foreach (DataRow dr in dt1.Rows)
                {
                    rpt.SetParameterValue("@Company", dr["CompanyName"].ToString());
                    //rpt.SetParameterValue("@Address", dr["Address"].ToString());
                    //rpt.SetParameterValue("@TelePhone", dr["TelephoneNumber"].ToString());
                    //rpt.SetParameterValue("@Fax", dr["Faxnumber"].ToString());
                    //rpt.SetParameterValue("@RegistrationNo", dr["RegistrationNo"].ToString());
                    //rpt.SetParameterValue("@Email", dr["Email1"].ToString());
                    //rpt.SetParameterValue("@Website", dr["Website"].ToString());
                }
                rpt.SetParameterValue("@HeaderText3", "Total Paid Invoice By Date");
            }
            else
            {
                lblMessage.Text = "No Record Found";
                CrystalReportViewer1.Visible = false;
            }
        }
        else if (rpttype == "4")
        {
            int pmode = 0;
            whereclause = "  where convert(smalldatetime,convert(char(10),duedate,101)) <= '" + txtfromdate + "' and Statusid<>3";// and (Statusid=2 or Statusid=1)";
            if (ddUser != "0")
            {
                pmode = 1;
                whereclause = whereclause + " and ForwardTo='" + ddUser + "'";
            }
            if (ddvendor != "0")
            {
                whereclause = whereclause + " and vendorid='" + ddvendor + "' ";
            }
            if (ddvessel != "0")
            {
                whereclause = whereclause + " and vesselid='" + ddvessel + "' ";
            }
            //else
            //{
            //    if (pmode == 1)
            //        whereclause = whereclause + " and vesselid In (Select VesselId from UserVesselRelation Where Loginid=" + ddUser + ") ";
            //}
            DataTable dt = InvoiceReport.selectREceivedInvoiceDate(Convert.ToDateTime(txtfromdate), Convert.ToDateTime(txttodate), 2, whereclause);
            if (dt.Rows.Count > 0)
            {
                CrystalReportViewer1.Visible = true;
                //CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                CrystalReportViewer1.ReportSource = rpt;
                rpt.Load(Server.MapPath("UnpaidInvoiceReport.rpt"));

                Session.Add("rptsource", dt);
                rpt.SetDataSource(dt);
                rpt.SetParameterValue("@FD", "Date: " + txtfromdate.ToString());
                DataTable dt1 = PrintCrewList.selectCompanyDetails();
                foreach (DataRow dr in dt1.Rows)
                {
                    rpt.SetParameterValue("@Company", dr["CompanyName"].ToString());
                    //rpt.SetParameterValue("@Address", dr["Address"].ToString());
                    //rpt.SetParameterValue("@TelePhone", dr["TelephoneNumber"].ToString());
                    //rpt.SetParameterValue("@Fax", dr["Faxnumber"].ToString());
                    //rpt.SetParameterValue("@RegistrationNo", dr["RegistrationNo"].ToString());
                    //rpt.SetParameterValue("@Email", dr["Email1"].ToString());
                    //rpt.SetParameterValue("@Website", dr["Website"].ToString());
                }
                rpt.SetParameterValue("@HeaderText", "Total Overdue Invoices By Date");
                rpt.SetParameterValue("@Email", "");
            }


            else
            {
                lblMessage.Text = "No Record Found";
                CrystalReportViewer1.Visible = false;
            }
        }
        else if (rpttype == "5")
        {
            string unApp = Request.QueryString["UnApp"].ToString();
            int pmode = 0;
            int days;
            try
            {
                days = int.Parse(Request.QueryString["days"]);
            }
            catch
            {
                days = 0;
            }
            if (unApp=="true")
                whereclause = "  where Statusid in (1,2) ";
            else
                whereclause = "  where Statusid in(0,1,2) ";

            whereclause = whereclause + " And convert(smalldatetime,convert(char(15),Invoice.Duedate,101)) <= '" + DateTime.Today.AddDays(days).ToString("MM/dd/yyyy") + "'";
            if (ddUser !="0")
            {
                pmode = 1;
                whereclause = whereclause + " and ForwardTo='" + ddUser + "'";
            }
            if (ddvendor != "0")
            {
                whereclause = whereclause + " and vendorid='" + ddvendor + "' ";
            }
            if (ddvessel != "0")
            {
                whereclause = whereclause + " and vesselid='" + ddvessel + "' ";
            }
            //else
            //{
            //    if (pmode == 1)
            //        whereclause = whereclause + " and vesselid In (Select VesselId from UserVesselRelation Where Loginid=" + ddUser + ") ";
            //}
            DataTable dt = InvoiceReport.selectREceivedInvoiceDate(Convert.ToDateTime(System.DateTime.Today.Date), Convert.ToDateTime(System.DateTime.Today.Date), 3, whereclause);
            if (dt.Rows.Count > 0)
            {
                CrystalReportViewer1.Visible = true;
                //CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                CrystalReportViewer1.ReportSource = rpt;
                rpt.Load(Server.MapPath("UnpaidInvoiceReport.rpt"));

                Session.Add("rptsource", dt);
                rpt.SetDataSource(dt);
                rpt.SetParameterValue("@FD", "");
                DataTable dt1 = PrintCrewList.selectCompanyDetails();
                foreach (DataRow dr in dt1.Rows)
                {
                    rpt.SetParameterValue("@Company", dr["CompanyName"].ToString());
                    //rpt.SetParameterValue("@Address", dr["Address"].ToString());
                    //rpt.SetParameterValue("@TelePhone", dr["TelephoneNumber"].ToString());
                    //rpt.SetParameterValue("@Fax", dr["Faxnumber"].ToString());
                    //rpt.SetParameterValue("@RegistrationNo", dr["RegistrationNo"].ToString());
                    //rpt.SetParameterValue("@Email", dr["Email1"].ToString());
                    //rpt.SetParameterValue("@Website", dr["Website"].ToString());
                }
                rpt.SetParameterValue("@HeaderText", "Total Unpaid invoice");
                rpt.SetParameterValue("@Email", "Due In: " + days.ToString() + " days ");
            }


            else
            {
                lblMessage.Text = "No Record Found";
                CrystalReportViewer1.Visible = false;
            }
        }
        else if (rpttype == "6")
        {
            int pmode = 0;
            DataTable dt = InvoiceReport.selectUserVesselInvoice(Convert.ToInt16(ddvessel), Convert.ToInt16(ddUser));
            if (dt.Rows.Count > 0)
            {
                CrystalReportViewer1.Visible = true;
                //CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                CrystalReportViewer1.ReportSource = rpt;
                rpt.Load(Server.MapPath("UserVesselInvoice.rpt"));

                Session.Add("rptsource", dt);
                rpt.SetDataSource(dt);
           
                DataTable dt1 = PrintCrewList.selectCompanyDetails();
                foreach (DataRow dr in dt1.Rows)
                {
                    rpt.SetParameterValue("@Company", dr["CompanyName"].ToString());
                    //rpt.SetParameterValue("@Address", dr["Address"].ToString());
                    //rpt.SetParameterValue("@TelePhone", dr["TelephoneNumber"].ToString());
                    //rpt.SetParameterValue("@Fax", dr["Faxnumber"].ToString());
                    //rpt.SetParameterValue("@RegistrationNo", dr["RegistrationNo"].ToString());
                    //rpt.SetParameterValue("@Email", dr["Email1"].ToString());
                    //rpt.SetParameterValue("@Website", dr["Website"].ToString());
                }
                rpt.SetParameterValue("@HeaderText6", "Total UnApproved Invoice");
            }


            else
            {
                lblMessage.Text = "No Record Found";
                CrystalReportViewer1.Visible = false;
            }
        }
        else if (rpttype == "7")
        {
            int pmode = 0;
            string whclause = "Where 1=1 ";
            if (ddvendor.Trim() != "0")
            {
                whclause = whclause + " And vendorId=" + ddvendor;  
            }
            if (Page.Request.QueryString["fromdate"].ToString().Trim() != "")
            {
                whclause = whclause + " And Invdate >='" + Page.Request.QueryString["fromdate"].ToString() + "' ";
            }
            if (Page.Request.QueryString["todate"].ToString().Trim() != "")
            {
                whclause = whclause + " And Invdate <='" + Page.Request.QueryString["todate"].ToString() + "' ";
            }
            DataTable dt = InvoiceReport.Report_SelectVendorInvoice(whclause);
            if (dt.Rows.Count > 0)
            {
                CrystalReportViewer1.Visible = true;
                //CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                CrystalReportViewer1.ReportSource = rpt;
                rpt.Load(Server.MapPath("VendorWiseInvoice.rpt"));

                Session.Add("rptsource", dt);
                rpt.SetDataSource(dt);
            
                DataTable dt1 = PrintCrewList.selectCompanyDetails();
                foreach (DataRow dr in dt1.Rows)
                {
                    rpt.SetParameterValue("@Company", dr["CompanyName"].ToString());
                    //rpt.SetParameterValue("@Address", dr["Address"].ToString());
                    //rpt.SetParameterValue("@TelePhone", dr["TelephoneNumber"].ToString());
                    //rpt.SetParameterValue("@Fax", dr["Faxnumber"].ToString());
                    //rpt.SetParameterValue("@RegistrationNo", dr["RegistrationNo"].ToString());
                    //rpt.SetParameterValue("@Email", dr["Email1"].ToString());
                    //rpt.SetParameterValue("@Website", dr["Website"].ToString());
                }
                rpt.SetParameterValue("@HeaderText7", "Total Invoice By Vendor");
            }


            else
            {
                lblMessage.Text = "No Record Found";
                CrystalReportViewer1.Visible = false;
            }


        }
        else if (rpttype == "8")
        {
            int pmode = 0;
            if (ddinvtype == "0")
            {
                whereclause = "  where convert(smalldatetime,convert(char(10),Verify1Date,101)) between '" + txtfromdate + "' and '" + txttodate + "' and VerifyStatus=1";
            }
            else
            {
                whereclause = "  where convert(smalldatetime,convert(char(10),Verify2Date,101)) between '" + txtfromdate + "' and '" + txttodate + "' and VerifyStatus=2";
            }
            if (ddUser != "0")
            {
                if (ddinvtype == "0")
                {
                    pmode = 1;
                    whereclause = whereclause + " and ForwardTo='" + ddUser + "'";
                }
                else
                {
                    pmode = 1;
                    whereclause = whereclause + " and PaymentTo='" + ddUser + "'";
                }
            }
            if (ddvendor != "0")
            {
                whereclause = whereclause + " and vendorid='" + ddvendor + "' ";
            }
            if (ddvessel != "0")
            {
                whereclause = whereclause + " and vesselid='" + ddvessel + "' ";
            }
            //else
            //{
            //    if (pmode == 1)
            //        whereclause = whereclause + " and vesselid In (Select VesselId from UserVesselRelation Where Loginid=" + ddUser + ") ";
            //}
            DataTable dt = InvoiceReport.selectVerifyInvoiceReport(Convert.ToInt32(ddinvtype) , whereclause);
            if (dt.Rows.Count > 0)
            {
                CrystalReportViewer1.Visible = true;
                //CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                CrystalReportViewer1.ReportSource = rpt;
                rpt.Load(Server.MapPath("InvoiceVerifyByDate.rpt"));

                Session.Add("rptsource", dt);
                rpt.SetDataSource(dt);
                rpt.SetParameterValue("@FD", txtfromdate.ToString());
                rpt.SetParameterValue("@TD", txttodate.ToString());
                DataTable dt1 = PrintCrewList.selectCompanyDetails();
                foreach (DataRow dr in dt1.Rows)
                {
                    rpt.SetParameterValue("@Company", dr["CompanyName"].ToString());
                    //rpt.SetParameterValue("@Address", dr["Address"].ToString());
                    //rpt.SetParameterValue("@TelePhone", dr["TelephoneNumber"].ToString());
                    //rpt.SetParameterValue("@Fax", dr["Faxnumber"].ToString());
                    //rpt.SetParameterValue("@RegistrationNo", dr["RegistrationNo"].ToString());
                    //rpt.SetParameterValue("@Email", dr["Email1"].ToString());
                    //rpt.SetParameterValue("@Website", dr["Website"].ToString());
                }
                //rpt.SetParameterValue("@HeaderText8", "Invoice Verify By Date ");
            }


            else
            {
                lblMessage.Text = "No Record Found";
                CrystalReportViewer1.Visible = false;
            }
        }

    }
}
