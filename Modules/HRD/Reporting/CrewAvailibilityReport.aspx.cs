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

public partial class Reporting_CrewAvaoilibilityReport : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();

    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        lblMessage.Text = "";
        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 179);
        if (chpageauth <= 0)
        {
            Response.Redirect("DummyReport.aspx");
        }
        //*******************
        if (!Page.IsPostBack)
        {
            DataTable dt = Budget.getTable("select * from RecruitingOffice").Tables[0];
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ddlRecOff.DataSource = dt;
                    ddlRecOff.DataTextField = "RecruitingOfficeName";
                    ddlRecOff.DataValueField = "RecruitingOfficeId";
                    ddlRecOff.DataBind();
                    ddlRecOff.Items.Insert(0, new ListItem("< All >", "0"));
                }
            }
        }
        else
        {
            
            Button1_Click(sender,e);
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string header = "";
        header = header + ((txt_from.Text.Trim() != "") ? " From Date : " + txt_from.Text : "");
        header = header + ((txt_to.Text.Trim() != "") ? " To Date : " + txt_to.Text : "");
        header = header + " Status : " + ddlCrewType.SelectedItem.Text;
        header = header + " Rec. Office : " + ddlRecOff.SelectedItem.Text;
        Session["header"] = header;  
        IFRAME1.Attributes.Add("src", "CrewAvalibilityReportContainer.aspx?fdt=" + txt_from.Text + "&tdt=" + txt_to.Text + "&status=" + ddlCrewType.SelectedValue + "&RecOff=" + ddlRecOff.SelectedValue );  
    }
}
