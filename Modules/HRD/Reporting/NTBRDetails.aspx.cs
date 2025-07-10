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

public partial class Reporting_NTBRDetails : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();

    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        this.ddl_UserName.Attributes.Add("onkeydown", "javascript:if(event.keyCode==13){document.getElementById('" + btn_show.ClientID + "').focus();}");
        this.txt_from.Attributes.Add("onkeydown", "javascript:if(event.keyCode==13){document.getElementById('" + btn_show.ClientID + "').focus();}");
        this.txt_to.Attributes.Add("onkeydown", "javascript:if(event.keyCode==13){document.getElementById('" + btn_show.ClientID + "').focus();}");
        //========== Code to check report printing authority
        CrystalReportViewer1.HasPrintButton = Alerts.Check_ReportPrintingAuthority(Convert.ToInt32(Session["loginid"].ToString()), 97);
        //==========
        this.lblmessage.Text = "";
        if (!(IsPostBack))
        {
            Session["PageCode"] = "7";
            bindVesselDDL();
        }
        else
        {
            Session.Remove("PageCode");
            btn_show_Click(sender, e);
        }
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
    public void bindVesselDDL()
    {
        DataTable dt = PromotioDetails.selectUserNames();
        this.ddl_UserName.DataValueField = "LoginId";
        this.ddl_UserName.DataTextField = "UserName";
        this.ddl_UserName.DataSource = dt;
        this.ddl_UserName.DataBind();
    }
    protected void btn_show_Click(object sender, EventArgs e)
    {
        this.CrystalReportViewer1.Visible = true;
       
        int userid = Convert.ToInt32(ddl_UserName.SelectedValue);
      
        if (txt_from.Text != "" && txt_to.Text != "")
        {
            DataTable dt1 = NTBRDetails.selectNTBRDetails(userid, Convert.ToDateTime(txt_from.Text), Convert.ToDateTime(txt_to.Text));
            DataTable dt2 = PrintCrewList.selectCompanyDetails();
            if (dt1.Rows.Count > 0)
            {
                CrystalReportViewer1.ReportSource = rpt;
                rpt.Load(Server.MapPath("NTBRDetails.rpt"));
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
                this.lblmessage.Text = "No Records Found.";
                this.CrystalReportViewer1.Visible = false;
            }
        }
    }
}
