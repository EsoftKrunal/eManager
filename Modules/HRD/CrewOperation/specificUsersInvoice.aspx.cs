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

public partial class CrewAccounting_specificUsersInvoice : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        if (!Page.IsPostBack)
        {
            Bindgrid();
        }
    }
    private void Bindgrid()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("Ref.No.");
        dt.Columns.Add("Vendor");
        dt.Columns.Add("ChqTTAmt");
        dt.Columns.Add("ChqTTDt.");
        dt.Columns.Add("Status");
        for (int i = 0; i <= 5; i++)
        {
            dt.Rows.Add(dt.NewRow());
        }
        gvinvoice.DataSource = dt;
        gvinvoice.DataBind();
    }
    
    protected void btn_printVoucher_Click(object sender, EventArgs e)
    {

    }
}
