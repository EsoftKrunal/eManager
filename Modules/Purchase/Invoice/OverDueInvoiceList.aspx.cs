using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Invoice_OverDueInvoiceList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //---------------------------------------
        ProjectCommon.SessionCheck();
        //---------------------------------------
        if (!Page.IsPostBack)
        {
            BindList();
        }
    }
    protected void btn_Search_Click(object sender, EventArgs e)
    {
        BindList();
    }
    protected void BindList()
    {
        string SQL = "select * from vw_POS_Invoices_001 where status='UnPaid' AND DueDate <= '" + DateTime.Today.ToString("dd-MMM-yyyy") + "' AND ApprovalFwdTo IS NOT null";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
        lblCount.Text = " (" + dt.Rows.Count.ToString() + " ) Records Found.";
        RptMyInvoices.DataSource = dt;
        RptMyInvoices.DataBind();
    }


    public string showblankifZero(object val)
    {
        if (Common.CastAsInt32(val) == 0)
            return "";
        else
            return val.ToString();

    }
}