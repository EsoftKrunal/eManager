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

public partial class Reporting_SignOnOffActivityTracking : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        this.txt_from.Attributes.Add("onkeydown", "javascript:if(event.keyCode==13){document.getElementById('" + btn_show.ClientID + "').focus();}");
        this.txt_to.Attributes.Add("onkeydown", "javascript:if(event.keyCode==13){document.getElementById('" + btn_show.ClientID + "').focus();}");
        this.rd_lst.Attributes.Add("onkeydown", "javascript:if(event.keyCode==13){document.getElementById('" + btn_show.ClientID + "').focus();}");
        //========== Code to check report printing authority
        CrystalReportViewer1.HasPrintButton = Alerts.Check_ReportPrintingAuthority(Convert.ToInt32(Session["loginid"].ToString()), 97);
        //==========
        lblmessage.Text = "";
        if (!Page.IsPostBack)
        {
            Session["PageCode"] = "5";
        }
        else
        {
            Session.Remove("PageCode");
            btn_show_Click(sender, e);
        }
        
    }
    protected void btn_show_Click(object sender, EventArgs e)
    {
        DataTable dt1;
        
        if (txt_from.Text == "" || txt_to.Text == "")
        {
            this.CrystalReportViewer1.Visible = false;
        }
        else
        {
            dt1 = SignOnOffActivityTracking.selectSignOnOffActivityTracking(Convert.ToDateTime(txt_from.Text), Convert.ToDateTime(txt_to.Text), Convert.ToInt32(rd_lst.SelectedValue));
            if (dt1.Rows.Count > 0)
            {
                this.CrystalReportViewer1.Visible = true;
                DataTable dt2 = PrintCrewList.selectCompanyDetails();
                CrystalReportViewer1.ReportSource = rpt;
                rpt.Load(Server.MapPath("SignOnOffActivityTracking.rpt"));
                rpt.SetDataSource(dt1);


                foreach (DataRow dr in dt2.Rows)
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
                this.CrystalReportViewer1.Visible = false;
                lblmessage.Text = "No Records Found.";
            }
        }
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
}
