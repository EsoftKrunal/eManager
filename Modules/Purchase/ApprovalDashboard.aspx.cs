using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Activities.Expressions;
using System.Web.UI.HtmlControls;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using DocumentFormat.OpenXml.Wordprocessing;

public partial class ApprovalDashboard : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            //---------------------------------------
            ProjectCommon.SessionCheck();
            //---------------------------------------

            if (!IsPostBack)
            {
                GetPendingCount();
            }
        }
       catch(Exception ex)
        {
            lblMsg.Text = ex.Message.ToString();
        }   
    }

    protected void GetPendingCount()
    {
        string strSQL = "EXEC GetPendingApprovalCountForDashboard " + Convert.ToInt32(Session["loginid"]) ;
        DataTable dtPendingCount = Common.Execute_Procedures_Select_ByQuery(strSQL);
        if (dtPendingCount.Rows.Count > 0)
        {
            lbPendingPoStage1.Text = dtPendingCount.Rows[0]["PendingPoStage1"].ToString();
            if (Convert.ToInt32(dtPendingCount.Rows[0]["PendingPoStage1"]) == 0)
            {
                lbPendingPoStage1.Enabled = false;
            }
            lbPendingPoStage2.Text = dtPendingCount.Rows[0]["PendingPoStage2"].ToString();
            if (Convert.ToInt32(dtPendingCount.Rows[0]["PendingPoStage2"]) == 0)
            {
                lbPendingPoStage2.Enabled = false;
            }
            lbPendingPoStage3.Text = dtPendingCount.Rows[0]["PendingPoStage3"].ToString();
            if (Convert.ToInt32(dtPendingCount.Rows[0]["PendingPoStage3"]) == 0)
            {
                lbPendingPoStage3.Enabled = false;
            }
            lbPendingPoStage4.Text = dtPendingCount.Rows[0]["PendingPoStage4"].ToString();
            if (Convert.ToInt32(dtPendingCount.Rows[0]["PendingPoStage4"]) == 0)
            {
                lbPendingPoStage4.Enabled = false;
            }
            lbPendingPoStage5.Text = dtPendingCount.Rows[0]["PendingPoStage5"].ToString();
            if (Convert.ToInt32(dtPendingCount.Rows[0]["PendingPoStage5"]) == 0)
            {
                lbPendingPoStage5.Enabled = false;
            }
            lbPendingInvoiceStage1.Text = dtPendingCount.Rows[0]["PendingInvoiceStage1"].ToString();
            if (Convert.ToInt32(dtPendingCount.Rows[0]["PendingInvoiceStage1"]) == 0)
            {
                lbPendingInvoiceStage1.Enabled = false;
            }
            lbPendingInvoiceStage2.Text = dtPendingCount.Rows[0]["PendingInvoiceStage2"].ToString();
            if (Convert.ToInt32(dtPendingCount.Rows[0]["PendingInvoiceStage2"]) == 0)
            {
                lbPendingInvoiceStage2.Enabled = false;
            }
            lblPendingVendorStage1.Text = dtPendingCount.Rows[0]["PendingVendorStage1"].ToString();
            if (Convert.ToInt32(dtPendingCount.Rows[0]["PendingVendorStage1"]) == 0)
            {
                lblPendingVendorStage1.Enabled = false;
            }
            lblPendingVendorStage2.Text = dtPendingCount.Rows[0]["PendingVendorStage2"].ToString();
            if (Convert.ToInt32(dtPendingCount.Rows[0]["PendingVendorStage2"]) == 0)
            {
                lblPendingVendorStage2.Enabled = false;
            }
            lblRFPApprovalCount.Text = dtPendingCount.Rows[0]["PendingRFP"].ToString();
            if (Convert.ToInt32(dtPendingCount.Rows[0]["PendingRFP"]) == 0)
            {
                lblRFPApprovalCount.Enabled = false;
            }

            GetAccessRight();
        }
    }

    protected void GetAccessRight()
    {
        string strSQL = "Select * from POS_Invoice_Mgmt with(nolock) where UserId = " + Convert.ToInt32(Session["loginid"]);
        DataTable dtAccessRight = Common.Execute_Procedures_Select_ByQuery(strSQL);
        if (dtAccessRight.Rows.Count > 0)
        {
            if (Convert.ToBoolean(dtAccessRight.Rows[0]["Approval"]))
            {
                if (Convert.ToInt32(lbPendingPoStage1.Text) > 0)
                    divPoStg1.Visible = true;
            }
            if (Convert.ToBoolean(dtAccessRight.Rows[0]["Verification"]))
            {
                if (Convert.ToInt32(lbPendingPoStage2.Text) > 0)
                    divPoStg2.Visible = true;
            }
            if (Convert.ToBoolean(dtAccessRight.Rows[0]["Approval3"]))
            {
                if (Convert.ToInt32(lbPendingPoStage3.Text) > 0)
                    divPoStg3.Visible = true;
                if (Convert.ToInt32(lblPendingVendorStage1.Text) > 0)
                    divVenStg1.Visible = true;
            }
            if (Convert.ToBoolean(dtAccessRight.Rows[0]["Approval4"]))
            {
                if (Convert.ToInt32(lbPendingPoStage4.Text) > 0)
                    divPoStg4.Visible = true;
                if (Convert.ToInt32(lblRFPApprovalCount.Text) > 0)
                    divRFP.Visible = true;
                if (Convert.ToInt32(lblPendingVendorStage2.Text) > 0)
                    divVenStg2.Visible = true;
            }

            if (Convert.ToInt32(lbPendingInvoiceStage1.Text) > 0)
            {
                divInvStg1.Visible = true;
            }
            if (Convert.ToInt32(lbPendingInvoiceStage2.Text) > 0)
            {
                divInvStg2.Visible = true;
            }
            string strSQL1 = "EXEC GetReadyPOCount " + Convert.ToInt32(Session["loginid"]);
            DataTable dtReadyPOCount = Common.Execute_Procedures_Select_ByQuery(strSQL1);
            if (Convert.ToInt32(lbPendingPoStage5.Text) > 0 && dtReadyPOCount.Rows.Count > 0 && Convert.ToInt32(dtReadyPOCount.Rows[0][0]) > 0)
            {
                divPoStg5.Visible = true;
            }
        }
        
    }

    protected void lbPendingPoStage1_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Modules/Purchase/Requisition/ApprovalList1.aspx?Key=1&Stage=1",true);
    }

    protected void lbPendingPoStage2_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Modules/Purchase/Requisition/ApprovalList1.aspx?Key=1&Stage=2", true);
    }

    protected void lbPendingPoStage3_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Modules/Purchase/Requisition/ApprovalList1.aspx?Key=1&Stage=3", true);
    }

    protected void lbPendingPoStage4_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Modules/Purchase/Requisition/ApprovalList1.aspx?Key=1&Stage=4", true);
    }

    protected void lbPendingPoStage5_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Modules/Purchase/Requisition/ApprovalList1.aspx?Key=1&Stage=5", true);
    }

    protected void lbPendingInvoiceStage1_Click(object sender, EventArgs e)
    {
        string appname = ConfigurationManager.AppSettings["AppName"].ToString();
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "openModal", "window.open('/"+ appname + "/Modules/Purchase/Invoice/InvoiceApprovalPendingList.aspx?Mode=1' ,'_blank');", true);
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "openModal", "window.open('/" + appname + "/Modules/Purchase/Invoice/InvoiceApprovalPendingList.aspx?Mode=1' ,'_blank');", true);

    }

    protected void lbPendingInvoiceStage2_Click(object sender, EventArgs e)
    {
        string appname = ConfigurationManager.AppSettings["AppName"].ToString();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "openModal", "window.open('/"+ appname + "/Modules/Purchase/Invoice/InvoiceApprovalPendingList.aspx?Mode=2' ,'_blank');", true);
    }

    protected void lblPendingVendorStage1_Click(object sender, EventArgs e)
    {
        string appname = ConfigurationManager.AppSettings["AppName"].ToString();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "openModal", "window.open('/"+ appname + "/Modules/Purchase/Vendor/VendorMgmtDetails.aspx?Mode=2' ,'_blank');", true);
    }

    protected void lblPendingVendorStage2_Click(object sender, EventArgs e)
    {
        string appname = ConfigurationManager.AppSettings["AppName"].ToString();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "openModal", "window.open('/"+ appname + "/Modules/Purchase/Vendor/VendorMgmtDetails.aspx?Mode=3' ,'_blank');", true);
    }



    protected void lblRFPApprovalCount_Click(object sender, EventArgs e)
    {
        string appname = ConfigurationManager.AppSettings["AppName"].ToString();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "openModal", "window.open('/" + appname + "/Modules/Purchase/Invoice/RFPRequestList.aspx' ,'_blank');", true);
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "openModal", "window.open('/Modules/Purchase/Invoice/RFPRequestList.aspx' ,'_blank');", true);

    }
}