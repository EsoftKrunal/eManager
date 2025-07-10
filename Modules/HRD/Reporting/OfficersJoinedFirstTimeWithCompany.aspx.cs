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

public partial class Reporting_OfficersJoinedFirstTimeWithCompany : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();

    DataTable dt1;
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        this.txt_from.Attributes.Add("onkeydown", "javascript:if(event.keyCode==13){document.getElementById('" + btn_show.ClientID + "').focus();}");
        this.txt_to.Attributes.Add("onkeydown", "javascript:if(event.keyCode==13){document.getElementById('" + btn_show.ClientID + "').focus();}");
        //this.rd_lst.Attributes.Add("onkeydown", "javascript:if(event.keyCode==13){document.getElementById('" + btn_show.ClientID + "').focus();}");
        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 15);
        if (chpageauth <= 0)
        {
            Response.Redirect("DummyReport.aspx");
        }
        //==========
        lblmessage.Text = "";
        if (Page.IsPostBack == false)
        {
            //Navin-Need to fix this-added fourth param as blank
            dt1 = OfficersJoinedFirstTimeWithCompany.selectOfficersJoinedFirstTimeWithCompany(-1,"","", "");
            Session.Add("rptsource5", dt1);
        }
        try
        {
            if (Convert.ToDateTime(txt_from.Text) > Convert.ToDateTime(txt_to.Text))
            {
                this.lblmessage.Text = "From Date Should be less than To Date.";
            }
            else
            {
                IFRAME1.Attributes.Add("src", "NewJoinCompanyContainer.aspx?selindex=" + rd_lst.SelectedValue + "&fdt=" + txt_from.Text + "&tdt=" + txt_to.Text);
            }
        }
        catch
        {}
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
    protected void btn_show_Click(object sender, EventArgs e)
    {
        if (Convert.ToDateTime(txt_from.Text) > Convert.ToDateTime(txt_to.Text))
        {
            this.lblmessage.Text = "From Date Should be less than To Date.";
        }
        else
        {
            IFRAME1.Attributes.Add("src", "NewJoinCompanyContainer.aspx?or="+ ddlor.SelectedValue + "&cs=" + rd_lst.SelectedValue + "&fdt=" + txt_from.Text + "&tdt=" + txt_to.Text);

        }
    }
}
