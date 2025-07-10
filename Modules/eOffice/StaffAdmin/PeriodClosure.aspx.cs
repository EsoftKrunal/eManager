using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class emtm_StaffAdmin_Emtm_LeaveSearch : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        lblClosureYear.Text = (System.DateTime.Now.Year-1).ToString();
        btnClosure.Enabled = DateTime.Today.Month == 1;    
    }
    public string FormatNumber(object Data)
    {
        return Math.Round(Convert.ToDecimal(Data), 1).ToString("##0.0"); 
    }
    protected void btnClosure_Click(object sender, EventArgs e)
    {
        Common.Execute_Procedures_Select_ByQueryCMS("exec dbo.HR_PeriodClosure " + lblClosureYear.Text);
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Closure done successfully.');", true);
    }
}
