using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CurrentYearBudget : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //---------------------------------------
        ProjectCommon.SessionCheck();
        //---------------------------------------
    }

    protected void btnUpdateBudget_Click(object sender, EventArgs e)
    {
        btnUpdateBudget.CssClass = "selbtn";
        btnReports.CssClass = "btn1";
        btnCurrentYearProjection.CssClass = "btn1";
        // btnPublish.CssClass = "btn1";

        frm1.Attributes.Add("src", "UpdateBudget_CY.aspx");
    }
    protected void btnReports_Click(object sender, EventArgs e)
    {
        btnUpdateBudget.CssClass = "btn1";
        btnReports.CssClass = "selbtn";
        btnCurrentYearProjection.CssClass = "btn1";
        // btnPublish.CssClass = "btn1";

        frm1.Attributes.Add("src", "CurrentYearBudgetReports.aspx");
    }
    //protected void btnPublish_Click(object sender, EventArgs e)
    //{
    //    btnUpdateBudget.CssClass = "btn1";
    //    btnReports.CssClass = "btn1";
    //   // btnPublish.CssClass = "selbtn";

    //    frm1.Attributes.Add("src", "PublishBudget_CY.aspx");
    //}

    protected void btnCurrentYearProjection_OnClick(object sender, EventArgs e)
    {
        btnUpdateBudget.CssClass = "btn1";
        btnReports.CssClass = "btn1";
        btnCurrentYearProjection.CssClass = "selbtn";
        // btnPublish.CssClass = "btn1";

        frm1.Attributes.Add("src", "CurrentYearProjection.aspx");
    }
}