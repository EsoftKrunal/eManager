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

public partial class Reporting_CrewExperienceSummaryMast : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 84);
        if (chpageauth <= 0)
        {
            Response.Redirect("DummyReport.aspx");

        }
        //*******************

        if (Session["PageCodeEXP"] == "1")
        {
            CheckBoxList1.SelectedValue = "1";
        }
        if (Session["PageCodeEXP"] == "2")
        {
            CheckBoxList1.SelectedValue = "2";
        }
    }
    protected void CheckBoxList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (CheckBoxList1.SelectedValue == "1")
        {
            Response.Redirect("CrewsExperienceReport.aspx");
        }
        if (CheckBoxList1.SelectedValue == "2")
        {
            Response.Redirect("CrewMatrixReportHeader.aspx");
        }
    }
}
