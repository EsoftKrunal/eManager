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

public partial class Reporting_RankWiseCountingOnboardOnleave : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        this.CheckBoxList1.Attributes.Add("onkeydown", "javascript:if(event.keyCode==13){document.getElementById('" + btn_show.ClientID + "').focus();}");
        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 15);
        if (chpageauth <= 0)
        {
            Response.Redirect("DummyReport.aspx");

        }
        //*******************
        this.lblMessage.Text = "";
        if (Page.IsPostBack == false)
        {
            DataTable dt = RankWiseCountingOnboardOnleave.RankWiseCrewMemberCounting("-1");
            Session.Add("rptsource1", dt);
        }
        try
        {
            btn_show_Click(new object(), new EventArgs());   
        }
        catch
        {

        }
    }
    protected void btn_show_Click(object sender, EventArgs e)
    {
        string status = "";
        for (int i = 0; i < this.CheckBoxList1.Items.Count; i++)
        {
            if (this.CheckBoxList1.Items[i].Selected == true)
            {
                if (status == "")
                {
                    status = this.CheckBoxList1.Items[i].Value.ToString();
                }
                else
                {
                    status = status + "," + this.CheckBoxList1.Items[i].Value.ToString();
                }
            }
        }
        if (status == "")
        {
            this.lblMessage.Text = "Please select at least status.";
        }
        else
        {
            this.lblMessage.Text = "";
            DataTable dt = RankWiseCountingOnboardOnleave.RankWiseCrewMemberCounting(status);
            if (dt.Rows.Count > 0)
            {
                rpt.Load(Server.MapPath("RankWiseCountingOnboardOnleave.rpt"));

                Session.Add("rptsource1",dt);
                rpt.SetDataSource(dt);

                DataTable dt1 = PrintCrewList.selectCompanyDetails();
                rpt.Load(Server.MapPath("RankWiseCountingOnboardOnleave.rpt"));
                foreach (DataRow dr in dt1.Rows)
                {
                    rpt.SetParameterValue("@Company", dr["CompanyName"].ToString());
                }
                string chkname = "";
                for (int n = 0; n < this.CheckBoxList1.Items.Count; n++)
                {
                    if (this.CheckBoxList1.Items[n].Selected == true)
                    {
                        if (chkname == "")
                        {
                            chkname = this.CheckBoxList1.Items[n].Text;
                        }
                        else
                        {
                            chkname = chkname + "," + this.CheckBoxList1.Items[n].Text;
                        }
                    }
                }
                rpt.SetParameterValue("@Header", "Total Rank Counting : " + chkname);
                IFRAME1.Attributes.Add("src", "RankWiseContainer.aspx?status=" + status  + "&statusname=" + chkname);
            }
            else
            {
                this.lblMessage.Text = "No Crew Members Exists.";
            }
        }
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
}
