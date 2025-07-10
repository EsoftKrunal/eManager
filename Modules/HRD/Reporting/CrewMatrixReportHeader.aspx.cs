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

public partial class Reporting_CrewMatrixReportHeader : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        Label1.Text = "";
        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 84);
        if (chpageauth <= 0)
        {
            Response.Redirect("DummyReport.aspx");

        }
        //*******************
       
        if (!Page.IsPostBack)
        {
            Session["PageCodeEXP"] = "2";
        }
        else
        {
            Session.Remove("PageCodeEXP");
            Button1_Click(sender, e);
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        int crewid = 0;
        DataTable dt22 = ReportPrintCV.selectCrewIdCrewNumber(txt_Emp_number.Text.Trim());
        if (dt22.Rows.Count == 0 && txt_Emp_number.Text != "")
        {
            Label1.Text = "Invalid Emp#.";
            IFRAME1.Attributes.Add("src", "");
            return;
        }
        else
        {
            Label1.Text = "";

            foreach (DataRow dr in dt22.Rows)
            {
                crewid = Convert.ToInt32(dr["CrewId"].ToString());
            }
            IFRAME1.Attributes.Add("src", "CrewMatrixReportCrystal.aspx?CrewID=" + crewid);
        }

    }
}
