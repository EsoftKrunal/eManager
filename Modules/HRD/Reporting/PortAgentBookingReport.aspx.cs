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

public partial class Reporting_PortAgentBookingReport : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();

    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        lblMessage.Text= "";  
        this.ddUser.Attributes.Add("onkeydown", "javascript:if(event.keyCode==13){document.getElementById('" + btnsearch.ClientID + "').focus();}");
        this.txtfromdate.Attributes.Add("onkeydown", "javascript:if(event.keyCode==13){document.getElementById('" + btnsearch.ClientID + "').focus();}");
        this.txttodate.Attributes.Add("onkeydown", "javascript:if(event.keyCode==13){document.getElementById('" + btnsearch.ClientID + "').focus();}");
        //========== Code to check report printing authority
        CrystalReportViewer1.HasPrintButton = Alerts.Check_ReportPrintingAuthority(Convert.ToInt32(Session["loginid"].ToString()), 97);
        //==========
        if (Page.IsPostBack == false)
        {
            Session["PageCode"] = "3";
            this.ddUser.DataTextField = "UserName";
            this.ddUser.DataValueField = "LoginId";
            this.ddUser.DataSource = InvoiceReport.getusername();
            this.ddUser.DataBind();

            this.txtfromdate.Text = System.DateTime.Today.Date.ToString("dd-MMM-yyyy");
            this.txttodate.Text = System.DateTime.Today.Date.ToString("dd-MMM-yyyy");
        }
        if (Page.IsPostBack)
        {
            Session.Remove("PageCode");
            showreport();
        }
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        showreport();
    }
    private void showreport()
    {
        DataTable dt = PortHeaderReport.selectportagentReportData(Convert.ToDateTime(this.txtfromdate.Text), Convert.ToDateTime(this.txttodate.Text), Convert.ToInt32(this.ddUser.SelectedValue));

        if (dt.Rows.Count > 0)
        {

            this.CrystalReportViewer1.Visible = true;
            CrystalReportViewer1.ReportSource = rpt;
            rpt.Load(Server.MapPath("PortAgentBookingHeaderReport.rpt"));

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
        }
        else
        {
            this.lblMessage.Text = "No records found.";
            this.CrystalReportViewer1.Visible = false;
        }
    }
    }

