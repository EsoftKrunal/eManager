using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Modules_Purchase_Invoice_InvoiceApprovalPendingList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       try
        {
            //---------------------------------------
            ProjectCommon.SessionCheck();
            //---------------------------------------
            if (!Page.IsPostBack)
            {
                BindList();
            }
        }
        catch(Exception ex)
        {

        }


    }
    
    protected void btn_Search_Click(object sender, EventArgs e)
    {
        BindList();
    }
   
    protected void btnPost_Click(object sender, EventArgs e)
    {
        BindList();
    }
    protected void BindList()
    {
        int Mode = Common.CastAsInt32(Request.QueryString["Mode"]);
        string SQL = "";
        switch (Mode)
        {

            case 1:
                SQL = "EXEC  GetInvoiceApprovalPendingList " + Convert.ToInt32(Session["loginid"]) + ", " + Mode;
                lblHeading.Text = "Invoice Approval Stage 1";
                break;
            case 2:
                SQL = "EXEC  GetInvoiceApprovalPendingList " + Convert.ToInt32(Session["loginid"]) + ", " + Mode;
                lblHeading.Text = "Invoice Approval Stage 2";
                break;
                //case 4:
                //    SQL = "select * from vw_POS_Invoices_001 where status='UnPaid' AND DueDate <= '" + DateTime.Today.ToString("dd-MMM-yyyy") + "' AND ApprovalFwdTo IS NOT null";
                //    lblHeading.Text = "Overdue";
                //    break;
        }

        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
        lblCount.Text = " (" + dt.Rows.Count.ToString() + " ) Records Found.";
        RptMyInvoices.DataSource = dt;
        RptMyInvoices.DataBind();
    }
}