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

public partial class Reporting_InvoiceReport : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();

    protected void Page_Load(object sender, EventArgs e)
       {
           //-----------------------------
           SessionManager.SessionCheck_New();
           //-----------------------------
           Session["PageName"] = " - Invoice Reports"; 
           this.lblMessage.Text = "";
           this.CrystalReportViewer1.Visible = true;
           //***********Code to check page acessing Permission
           int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 98);
           if (chpageauth <= 0)
           {
               Response.Redirect("~/CrewOperation/Dummy3.aspx");

           }
           //*******************
        //========== Code to check report printing authority
        CrystalReportViewer1.HasPrintButton =Alerts.Check_ReportPrintingAuthority(Convert.ToInt32(Session["loginid"].ToString()),98);
        //==========
        this.lblMessage.Text = "";
        if (Page.IsPostBack == false)
        { 
            this.ddvessel.DataTextField="VesselName";
            this.ddvessel.DataValueField="VesselId";
            this.ddvessel.DataSource = cls_SearchReliever.getMasterData("Vessel", "VesselId", "VesselName");
            this.ddvessel.DataBind();
            this.ddvessel.Items.Insert(0, new ListItem("All", "0"));

            this.ddUser.DataTextField = "UserName";
            this.ddUser.DataValueField = "LoginId";
            this.ddUser.DataSource = InvoiceReport.getusername();
            this.ddUser.DataBind();

            this.ddvendor.DataTextField="VendorName";
            this.ddvendor.DataValueField="Vendorid";
            this.ddvendor.DataSource=InvoiceReport.selectVendor();
            this.ddvendor.DataBind ();

            this.trdate.Visible = false;
            this.truser.Visible = false;
            this.trvendor.Visible = false;
            trverify.Visible = false;
        }
        
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
    private void showreport()
    {
        if (this.txtfromdate.Text == "")
        {
            this.txtfromdate.Text = System.DateTime.Today.Date.ToString("dd-MMM-yyyy");
            this.txttodate.Text = System.DateTime.Today.Date.ToString("dd-MMM-yyyy");
        }
        string whereclause = "";
      
        if (this.rpttype.SelectedValue == "1")
        {
            whereclause = " where convert(smalldatetime,convert(char(10),createdon,101)) between '" + this.txtfromdate.Text + "' and  '" + this.txttodate.Text + "'";
                    if (ddUser.SelectedIndex != 0)
                    {
                        whereclause = whereclause + " and createdby='" + this.ddUser.SelectedValue.ToString() + "'"; 
                    }
                    if (ddvendor.SelectedIndex != 0)
                    {
                        whereclause = whereclause + " and vendorid='" + this.ddvendor.SelectedValue.ToString() + "' ";
                    }
                    if (ddvessel.SelectedIndex != 0)
                    {
                        whereclause = whereclause + " and vesselid='" + this.ddvessel.SelectedValue.ToString() + "' ";
                    }

                    
               whereclause=whereclause + "order by "   + "refno" ;
                
            DataTable dt = InvoiceReport.selectREceivedInvoiceDate(Convert.ToDateTime(this.txtfromdate.Text), Convert.ToDateTime(this.txttodate.Text), 0,whereclause);

            if (dt.Rows.Count > 0)
            {

                this.CrystalReportViewer1.Visible = true;
                CrystalReportViewer1.ReportSource = rpt;
                rpt.Load(Server.MapPath("RecivedInvoiceReport.rpt"));

                Session.Add("rptsource", dt);
                rpt.SetDataSource(dt);
                rpt.SetParameterValue("@FD", this.txtfromdate.Text.ToString());
                rpt.SetParameterValue("@TD", this.txttodate.Text.ToString());
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
                this.lblMessage.Text = "No Record Found";
                this.CrystalReportViewer1.Visible = false;
            }
        }
        if (this.rpttype.SelectedValue == "2")
        {
            whereclause = " where convert(smalldatetime,convert(char(10),approveddate,101)) between '" + this.txtfromdate.Text + "' and '" + this.txttodate.Text + "' and Statusid=1";
            if (ddUser.SelectedIndex != 0)
            {
                whereclause = whereclause + " and approvedby='" + this.ddUser.SelectedValue.ToString() + "'";
            }
            if (ddvendor.SelectedIndex != 0)
            {
                whereclause = whereclause + " and vendorid='" + this.ddvendor.SelectedValue.ToString() + "' ";
            }
            if (ddvessel.SelectedIndex != 0)
            {
                whereclause = whereclause + " and vesselid='" + this.ddvessel.SelectedValue.ToString() + "' ";
            }
            DataTable dt = InvoiceReport.selectREceivedInvoiceDate(Convert.ToDateTime(this.txtfromdate.Text), Convert.ToDateTime(this.txttodate.Text), 1,whereclause );
            if (dt.Rows.Count > 0)
            {
                this.CrystalReportViewer1.Visible = true;
                CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                CrystalReportViewer1.ReportSource = rpt;
                rpt.Load(Server.MapPath("ApprovedInvoiceReport.rpt"));

                Session.Add("rptsource", dt);
                rpt.SetDataSource(dt);
                rpt.SetParameterValue("@FD", "Period:     From:   " + txtfromdate.ToString() + "    To:   " + txttodate.ToString());

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
                this.lblMessage.Text = "No Record Found";
                this.CrystalReportViewer1.Visible = false;
            }
        }

        else if (this.rpttype.SelectedValue == "3")
        {
            string whereclause1, whereclause2;
            whereclause1 = " ";
            whereclause2 = "";
            whereclause2 = "  where convert(smalldatetime,convert(char(10),createdon,101))  between '" + this.txtfromdate.Text + "' and '" + txttodate.Text + "'";
            if (ddUser.SelectedIndex != 0)
            {
                whereclause1 = whereclause1 + "  invoice.approvedby='" + this.ddUser.SelectedValue.ToString() + "' and ";
            }
            if (ddvendor.SelectedIndex != 0)
            {
                whereclause1 = whereclause1 + "  invoice.vendorid='" + this.ddvendor.SelectedValue.ToString() + "' and ";
            }
            if (ddvessel.SelectedIndex != 0)
            {
                whereclause1 = whereclause1 + "  invoice.vesselid='" + this.ddvessel.SelectedValue.ToString() + "' and ";
            }
            
            DataTable dt = InvoiceReport.selectPayInvoiceDate(whereclause1, whereclause2 );
            if (dt.Rows.Count > 0)
            {
                this.CrystalReportViewer1.Visible = true;
                CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                CrystalReportViewer1.ReportSource = rpt;
                rpt.Load(Server.MapPath("InvoicePayByDateReport.rpt"));

                Session.Add("rptsource", dt);
                rpt.SetDataSource(dt);
                rpt.SetParameterValue("@FD", this.txtfromdate.Text.ToString());
                rpt.SetParameterValue("@TD", this.txttodate.Text.ToString());
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
                this.lblMessage.Text = "No Record Found";
                this.CrystalReportViewer1.Visible = false;
            }
        }
        else if (this.rpttype.SelectedValue == "4")
        {
            whereclause = "  where convert(smalldatetime,convert(char(10),duedate,101)) between '"+ this.txtfromdate.Text +"' and '" + this.txttodate.Text +"' and Statusid=2";
            if (ddUser.SelectedIndex != 0)
            {
                whereclause = whereclause + " and createdby='" + this.ddUser.SelectedValue.ToString() + "'";
            }
            if (ddvendor.SelectedIndex != 0)
            {
                whereclause = whereclause + " and vendorid='" + this.ddvendor.SelectedValue.ToString() + "' ";
            }
            if (ddvessel.SelectedIndex != 0)
            {
                whereclause = whereclause + " and vesselid='" + this.ddvessel.SelectedValue.ToString() + "' ";
            }
            DataTable dt = InvoiceReport.selectREceivedInvoiceDate(Convert.ToDateTime(this.txtfromdate.Text), Convert.ToDateTime(this.txttodate.Text), 2,whereclause );
            if (dt.Rows.Count > 0)
            {
                this.CrystalReportViewer1.Visible = true;
                CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                CrystalReportViewer1.ReportSource = rpt;
                rpt.Load(Server.MapPath("ApprovedInvoiceReport.rpt"));

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
            }


            else
            {
                this.lblMessage.Text = "No Record Found";
                this.CrystalReportViewer1.Visible = false;
            }
        }
        else if (this.rpttype.SelectedValue == "5")
        {

            whereclause = "  where Statusid=1";
            if (ddUser.SelectedIndex != 0)
            {
                whereclause = whereclause + " and createdby='" + this.ddUser.SelectedValue.ToString() + "'";
            }
            if (ddvendor.SelectedIndex != 0)
            {
                whereclause = whereclause + " and vendorid='" + this.ddvendor.SelectedValue.ToString() + "' ";
            }
            if (ddvessel.SelectedIndex != 0)
            {
                whereclause = whereclause + " and vesselid='" + this.ddvessel.SelectedValue.ToString() + "' ";
            }
            DataTable dt = InvoiceReport.selectREceivedInvoiceDate(Convert.ToDateTime(System.DateTime.Today.Date), Convert.ToDateTime(System.DateTime.Today.Date), 3,whereclause );
            if (dt.Rows.Count > 0)
            {
                this.CrystalReportViewer1.Visible = true;
                CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                CrystalReportViewer1.ReportSource = rpt;
                rpt.Load(Server.MapPath("ApprovedInvoiceReport.rpt"));

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
            }


            else

            {
                this.lblMessage.Text = "No Record Found";
                this.CrystalReportViewer1.Visible = false;
            }
        }
        else if (this.rpttype.SelectedValue == "6")
        {
            DataTable dt = InvoiceReport.selectUserVesselInvoice(Convert.ToInt16(this.ddvessel.SelectedValue), Convert.ToInt16(this.ddUser.SelectedValue));
            if (dt.Rows.Count > 0)
            {
                this.CrystalReportViewer1.Visible = true;
                CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
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
            }


            else
            {
                this.lblMessage.Text = "No Record Found";
                this.CrystalReportViewer1.Visible = false;
            }
        }
        else if (this.rpttype.SelectedValue == "7")
        {
            DataTable dt = InvoiceReport.selectVendorInvoice(Convert.ToInt16(this.ddvendor.SelectedValue));
            if (dt.Rows.Count > 0)
            {
                this.CrystalReportViewer1.Visible = true;
                CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
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
            }


            else
            {
                this.lblMessage.Text = "No Record Found";
                this.CrystalReportViewer1.Visible = false;
            }


        }
        
    }
    protected void rpttype_SelectedIndexChanged(object sender, EventArgs e)
    {

        this.txtfromdate.Text = System.DateTime.Today.Date.ToString("dd-MMM-yyyy");
        this.txttodate.Text = System.DateTime.Today.Date.ToString("dd-MMM-yyyy");
        this.lblfrom.Text = "From Date :";
        this.lblto.Text = "To Date :";
        this.lblto.Visible = true;
        this.txttodate.Visible = true;
        this.imgto.Visible = true;
        lblRange.Visible = false;
        txtdays.Visible = false; 
        chk_Unapp.Visible = false;

        if (this.rpttype.SelectedValue == "0")
        {
            this.trdate.Visible = false;
            this.truser.Visible = false;
            this.trvendor.Visible = false;
        }
        else if (this.rpttype.SelectedValue == "1" || this.rpttype.SelectedValue == "2" || this.rpttype.SelectedValue == "4" || this.rpttype.SelectedValue == "3")
        {
            this.trdate.Visible = true;
            this.truser.Visible = true;
            this.trvendor.Visible = true;
            trverify.Visible = false;
        }
        else if (this.rpttype.SelectedValue == "3")
        {
            this.trdate.Visible = true;
            trverify.Visible = false;
        }
        else if (this.rpttype.SelectedValue == "5")
        {
            this.trdate.Visible = false;
            this.truser.Visible = true;
            this.trvendor.Visible = true;
            trverify.Visible = false;
            trverify.Visible = false;
            lblRange.Visible = true;
            txtdays.Visible= true;
            chk_Unapp.Visible = true;
        }
        else if (this.rpttype.SelectedValue == "6")
        {
            this.trdate.Visible = false;
            this.truser.Visible = true;
            this.trvendor.Visible = false;
                trverify.Visible = false;;
        }
        else if (this.rpttype.SelectedValue == "7")
        {
            this.trdate.Visible = true;
            this.truser.Visible = false;
            this.trvendor.Visible = true;
            trverify.Visible = false;
        }
        else if (this.rpttype.SelectedValue == "8")
        {
            this.trdate.Visible = true;
            this.truser.Visible = true;
            this.trvendor.Visible = true;
            trverify.Visible = true;
        }

        if (this.rpttype.SelectedValue == "4")
        {
            this.lblfrom.Text = "Date";
            this.lblto.Visible = false;
            this.txttodate.Visible = false;
            this.imgto.Visible = false;
        }
    }
    protected void btnsearch_Click(object sender, EventArgs e)
    {
    }
}

