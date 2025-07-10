using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CrystalDecisions.Shared.Json;
using System.Activities.Expressions;

public partial class Invoice_InvoiceList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //---------------------------------------
        ProjectCommon.SessionCheck();
        //---------------------------------------
        if (!Page.IsPostBack)
        {
            bindOffice();
            BindSummary();
        }
    }
    protected void bindOffice()
    {
        string SQL = "select * from dbo.office order by officename";
        DataTable dt1 = Common.Execute_Procedures_Select_ByQuery(SQL);
        ddlOffice.DataValueField = "officeId";
        ddlOffice.DataTextField = "officename";
        ddlOffice.DataSource = dt1;
        ddlOffice.DataBind();
        ddlOffice.Items.Insert(0, new ListItem("< Select Office >", "0"));
    }
    protected void btn_Search_Click(object sender, EventArgs e)
    {
        BindSummary();
    }
    protected void BindSummary()
    {
        string OfficeFilter = "";

        int Mode = Common.CastAsInt32(Request.QueryString["Mode"]);
        string sql = "";
        if (Mode == 1) // process
        {
            if (ddlOffice.SelectedIndex > 0)
                OfficeFilter = " and ApprovalFwdTo in (select LoginId from DBO.UserLogin pp where pp.RecruitingOfficeId like '%" + ddlOffice.SelectedValue + "%' )";

            sql = "select ApprovalFwdTo as userid,COUNT(*) AS NOOFINV , " +
            "(SELECT FIRSTNAME + ' ' + LastName FROM DBO.UserLogin P WHERE P.LoginId  = I.ApprovalFwdTo) AS USERNAME, " +
            "(select COUNT(*) from pos_invoice I1 where I1.approvalfwdto=I.approvalfwdto and stage=1  AND STATUS='U' and datediff(day,I1.EnteredOn,getdate()) BETWEEN 0 AND 5 ) as DAYS_5, " +
            "(select COUNT(*) from pos_invoice I1 where I1.approvalfwdto=I.approvalfwdto and stage=1 AND STATUS='U' and datediff(day,I1.EnteredOn,getdate()) BETWEEN 6 AND 10 ) as DAYS_10, " +
            "(select COUNT(*) from pos_invoice I1 where I1.approvalfwdto=I.approvalfwdto and stage=1 and approvalon is null AND STATUS='U' and datediff(day,I1.EnteredOn,getdate()) BETWEEN 11 AND 30 ) as DAYS_15, " +
            "(select COUNT(*) from pos_invoice I1 where I1.approvalfwdto=I.approvalfwdto and stage=1 and approvalon is null AND STATUS='U' and datediff(day,I1.EnteredOn,getdate()) > 30 ) as DAYS_30, " +
             " 0 As ApprovalLevel" +
            " from pos_invoice I where isnull(approvalfwdto,0)>0 and stage=1 " + OfficeFilter + " AND STATUS='U' GROUP BY ApprovalFwdTo ORDER BY USERNAME";
            //and approvalon is null 
        }
        else if (Mode == 2) // Approval
        {
            if (ddlOffice.SelectedIndex > 0)
                OfficeFilter = " and I.AppUserId in (select LoginId from DBO.UserLogin pp where pp.RecruitingOfficeId like '%" + ddlOffice.SelectedValue + "%' )";

            //sql = "select APPUSERID as userid,COUNT(INVOICEID) AS NOOFINV ,  " +
            //"(SELECT FIRSTNAME + ' ' + LastName FROM DBO.UserLogin P WHERE P.LoginId  = I.APPUSERID) AS USERNAME, " +
            //"(SELECT COUNT(*) FROM POS_Invoice_Approvals A INNER JOIN POS_INVOICE I1 ON A.INVOICEID=I1.INVOICEID WHERE A.ApprovedOn IS NULL AND A.AppUserId=I.APPUSERID and stage=2 and PaidOn is null AND STATUS='U' and datediff(day,I1.EnteredOn,getdate()) BETWEEN 0 AND 5 ) as DAYS_5,  " +
            //"(SELECT COUNT(*) FROM POS_Invoice_Approvals A INNER JOIN POS_INVOICE I1 ON A.INVOICEID=I1.INVOICEID WHERE A.ApprovedOn IS NULL AND A.AppUserId=I.APPUSERID and stage=2 and PaidOn is null AND STATUS='U' and datediff(day,I1.EnteredOn,getdate()) BETWEEN 6 AND 10 ) as DAYS_10,  " +
            //"(SELECT COUNT(*) FROM POS_Invoice_Approvals A INNER JOIN POS_INVOICE I1 ON A.INVOICEID=I1.INVOICEID WHERE A.ApprovedOn IS NULL AND A.AppUserId=I.APPUSERID and stage=2 and PaidOn is null AND STATUS='U' and datediff(day,I1.EnteredOn,getdate()) BETWEEN 11 AND 30 ) as DAYS_15,  " +
            //"(SELECT COUNT(*) FROM POS_Invoice_Approvals A INNER JOIN POS_INVOICE I1 ON A.INVOICEID=I1.INVOICEID WHERE A.ApprovedOn IS NULL AND A.AppUserId=I.APPUSERID and stage=2 and PaidOn is null AND STATUS='U' and datediff(day,I1.EnteredOn,getdate()) > 30 ) as DAYS_30 " +
            //"from POS_Invoice_Approvals I where I.AppUserId>0 " + OfficeFilter + " AND  I.AppUserId = " + int.Parse(Session["loginid"].ToString()) + " AND i.InvoiceId in ( select invoiceid from pos_invoice where STATUS='U' ) and ApprovedOn IS NULL GROUP BY APPUSERID ORDER BY USERNAME";

            sql = "Select A.* from (   SELECT APPUSERID as userid,COUNT(INVOICEID) AS NOOFINV , (SELECT FIRSTNAME + ' ' + LastName + ' (Approval Stage 1)' FROM DBO.UserLogin P WHERE P.LoginId  = I.APPUSERID) AS USERNAME,             (SELECT COUNT(*) FROM POS_Invoice_Approvals A  INNER JOIN POS_INVOICE I1 ON A.INVOICEID=I1.INVOICEID WHERE A.ApprovedOn IS NULL AND A.ApprovalLevel = 1 AND A.AppUserId=I.APPUSERID and stage=2 and PaidOn is null AND STATUS='U' and datediff(day,I1.EnteredOn,getdate()) BETWEEN 0 AND 5 ) as DAYS_5,              (SELECT COUNT(*) FROM POS_Invoice_Approvals A INNER JOIN POS_INVOICE I1 ON A.INVOICEID=I1.INVOICEID WHERE A.ApprovedOn IS NULL AND A.ApprovalLevel = 1 AND A.AppUserId=I.APPUSERID and stage=2 and PaidOn is null AND STATUS='U' and datediff(day,I1.EnteredOn,getdate()) BETWEEN 6 AND 10 ) as DAYS_10,           (SELECT COUNT(*) FROM POS_Invoice_Approvals A INNER JOIN POS_INVOICE I1 ON A.INVOICEID=I1.INVOICEID WHERE A.ApprovedOn IS NULL AND A.ApprovalLevel = 1 AND A.AppUserId=I.APPUSERID and stage=2 and PaidOn is null AND STATUS='U' and datediff(day,I1.EnteredOn,getdate()) BETWEEN 11 AND 30 ) as DAYS_15,              (SELECT COUNT(*) FROM POS_Invoice_Approvals A INNER JOIN POS_INVOICE I1 ON A.INVOICEID=I1.INVOICEID WHERE A.ApprovedOn IS NULL AND A.ApprovalLevel = 1 AND A.AppUserId=I.APPUSERID and stage=2 and PaidOn is null AND STATUS='U' and datediff(day,I1.EnteredOn,getdate()) > 30 ) as DAYS_30 , 1 As ApprovalLevel  from POS_Invoice_Approvals I with(nolock)   where I.AppUserId>0  " + OfficeFilter + " AND  I.AppUserId = " + int.Parse(Session["loginid"].ToString()) + "  AND i.InvoiceId in (select invoiceid from pos_invoice where STATUS='U' and stage=2  )    and I.ApprovedOn IS NULL AND I.ApprovalLevel = 1 GROUP BY APPUSERID  UNION ALL              SELECT APPUSERID as userid  ,COUNT(INVOICEID) AS NOOFINV    , (SELECT FIRSTNAME + ' ' + LastName + ' (Approval Stage 2)' FROM DBO.UserLogin P WHERE P.LoginId  = I.APPUSERID) AS USERNAME,  (SELECT COUNT(*) FROM POS_Invoice_Approvals A  INNER JOIN POS_INVOICE I1 ON A.INVOICEID=I1.INVOICEID WHERE A.ApprovedOn IS NULL AND A.ApprovalLevel = 2 AND A.AppUserId=I.APPUSERID and stage=2 and PaidOn is null AND STATUS='U' and datediff(day,I1.EnteredOn,getdate()) BETWEEN 0 AND 5 AND A.InvoiceId Not In (Select Invoiceid from POS_Invoice_Approvals with(nolock) where A.ApprovedOn IS NOT NULL AND A.ApprovalLevel = 1 )) as DAYS_5,   (SELECT COUNT(*) FROM POS_Invoice_Approvals A INNER JOIN POS_INVOICE I1 ON A.INVOICEID=I1.INVOICEID WHERE A.ApprovedOn IS NULL AND A.ApprovalLevel = 2 AND A.AppUserId=I.APPUSERID and stage=2 and PaidOn is null AND STATUS='U' and datediff(day,I1.EnteredOn,getdate()) BETWEEN 6 AND 10 AND A.InvoiceId Not In (Select Invoiceid from POS_Invoice_Approvals with(nolock) where A.ApprovedOn IS NOT NULL AND A.ApprovalLevel = 1 )) as DAYS_10, (SELECT COUNT(*) FROM POS_Invoice_Approvals A INNER JOIN POS_INVOICE I1 ON A.INVOICEID=I1.INVOICEID WHERE A.ApprovedOn IS NULL AND A.ApprovalLevel = 2 AND A.AppUserId=I.APPUSERID and stage=2 and PaidOn is null AND STATUS='U' and datediff(day,I1.EnteredOn,getdate()) BETWEEN 11 AND 30 AND A.InvoiceId Not In (Select Invoiceid from POS_Invoice_Approvals with(nolock) where A.ApprovedOn IS NOT NULL AND A.ApprovalLevel = 1 )) as DAYS_15, (SELECT COUNT(*) FROM POS_Invoice_Approvals A INNER JOIN POS_INVOICE I1 ON A.INVOICEID=I1.INVOICEID WHERE A.ApprovedOn IS NULL AND A.ApprovalLevel = 2 AND A.AppUserId=I.APPUSERID and stage=2 and PaidOn is null AND STATUS='U' and datediff(day,I1.EnteredOn,getdate()) > 30 AND A.InvoiceId Not In (Select Invoiceid from POS_Invoice_Approvals with(nolock) where A.ApprovedOn IS NOT NULL AND A.ApprovalLevel = 1 )) as DAYS_30 , 2 As ApprovalLevel from POS_Invoice_Approvals I with(nolock)where I.AppUserId>0  " + OfficeFilter + " AND  I.AppUserId = " + int.Parse(Session["loginid"].ToString()) + "   AND i.InvoiceId in (select invoiceid from pos_invoice where STATUS='U' and stage=2)    and I.ApprovedOn IS NULL AND I.ApprovalLevel = 2 GROUP BY APPUSERID UNION ALL  SELECT APPUSERID as userid  ,COUNT(INVOICEID) AS NOOFINV    , (SELECT FIRSTNAME + ' ' + LastName + ' (Approval Stage 3)' FROM DBO.UserLogin P WHERE P.LoginId  = I.APPUSERID) AS USERNAME,  (SELECT COUNT(*) FROM POS_Invoice_Approvals A  INNER JOIN POS_INVOICE I1 ON A.INVOICEID=I1.INVOICEID WHERE A.ApprovedOn IS NULL AND A.ApprovalLevel = 3 AND A.AppUserId=I.APPUSERID and stage=2 and PaidOn is null AND STATUS='U' and datediff(day,I1.EnteredOn,getdate()) BETWEEN 0 AND 5 AND A.InvoiceId Not In (Select Invoiceid from POS_Invoice_Approvals with(nolock) where A.ApprovedOn IS NOT NULL AND A.ApprovalLevel in (1,2) )) as DAYS_5,   (SELECT COUNT(*) FROM POS_Invoice_Approvals A INNER JOIN POS_INVOICE I1 ON A.INVOICEID=I1.INVOICEID WHERE A.ApprovedOn IS NULL AND A.ApprovalLevel = 3 AND A.AppUserId=I.APPUSERID and stage=2 and PaidOn is null AND STATUS='U' and datediff(day,I1.EnteredOn,getdate()) BETWEEN 6 AND 10 AND A.InvoiceId Not In (Select Invoiceid from POS_Invoice_Approvals with(nolock) where A.ApprovedOn IS NOT NULL AND A.ApprovalLevel in (1,2) )) as DAYS_10, (SELECT COUNT(*) FROM POS_Invoice_Approvals A INNER JOIN POS_INVOICE I1 ON A.INVOICEID=I1.INVOICEID WHERE A.ApprovedOn IS NULL AND A.ApprovalLevel = 3 AND A.AppUserId=I.APPUSERID and stage=2 and PaidOn is null AND STATUS='U' and datediff(day,I1.EnteredOn,getdate()) BETWEEN 11 AND 30 AND A.InvoiceId Not In (Select Invoiceid from POS_Invoice_Approvals with(nolock) where A.ApprovedOn IS NOT NULL AND A.ApprovalLevel in (1,2) )) as DAYS_15, (SELECT COUNT(*) FROM POS_Invoice_Approvals A INNER JOIN POS_INVOICE I1 ON A.INVOICEID=I1.INVOICEID WHERE A.ApprovedOn IS NULL AND A.ApprovalLevel = 3 AND A.AppUserId=I.APPUSERID and stage=2 and PaidOn is null AND STATUS='U' and datediff(day,I1.EnteredOn,getdate()) > 30 AND A.InvoiceId Not In (Select Invoiceid from POS_Invoice_Approvals with(nolock) where A.ApprovedOn IS NOT NULL AND A.ApprovalLevel in (1,2) )) as DAYS_30, 3 As ApprovalLevel from POS_Invoice_Approvals I with(nolock)where I.AppUserId>0  " + OfficeFilter + " AND  I.AppUserId = " + int.Parse(Session["loginid"].ToString()) + "   AND i.InvoiceId in (select invoiceid from pos_invoice where STATUS='U' and stage=2)    and I.ApprovedOn IS NULL AND I.ApprovalLevel = 3 GROUP BY APPUSERID UNION ALL SELECT APPUSERID as userid  ,COUNT(INVOICEID) AS NOOFINV    , (SELECT FIRSTNAME + ' ' + LastName + ' (Approval Stage 4)' FROM DBO.UserLogin P WHERE P.LoginId  = I.APPUSERID) AS USERNAME,  (SELECT COUNT(*) FROM POS_Invoice_Approvals A  INNER JOIN POS_INVOICE I1 ON A.INVOICEID=I1.INVOICEID WHERE A.ApprovedOn IS NULL AND A.ApprovalLevel = 3 AND A.AppUserId=I.APPUSERID and stage=2 and PaidOn is null AND STATUS='U' and datediff(day,I1.EnteredOn,getdate()) BETWEEN 0 AND 5 AND A.InvoiceId Not In (Select Invoiceid from POS_Invoice_Approvals with(nolock) where A.ApprovedOn IS NOT NULL AND A.ApprovalLevel in (1,2,3) )) as DAYS_5,   (SELECT COUNT(*) FROM POS_Invoice_Approvals A INNER JOIN POS_INVOICE I1 ON A.INVOICEID=I1.INVOICEID WHERE A.ApprovedOn IS NULL AND A.ApprovalLevel = 3 AND A.AppUserId=I.APPUSERID and stage=2 and PaidOn is null AND STATUS='U' and datediff(day,I1.EnteredOn,getdate()) BETWEEN 6 AND 10 AND A.InvoiceId Not In (Select Invoiceid from POS_Invoice_Approvals with(nolock) where A.ApprovedOn IS NOT NULL AND A.ApprovalLevel in (1,2,3) )) as DAYS_10, (SELECT COUNT(*) FROM POS_Invoice_Approvals A INNER JOIN POS_INVOICE I1 ON A.INVOICEID=I1.INVOICEID WHERE A.ApprovedOn IS NULL AND A.ApprovalLevel = 3 AND A.AppUserId=I.APPUSERID and stage=2 and PaidOn is null AND STATUS='U' and datediff(day,I1.EnteredOn,getdate()) BETWEEN 11 AND 30 AND A.InvoiceId Not In (Select Invoiceid from POS_Invoice_Approvals with(nolock) where A.ApprovedOn IS NOT NULL AND A.ApprovalLevel in (1,2,3) )) as DAYS_15, (SELECT COUNT(*) FROM POS_Invoice_Approvals A INNER JOIN POS_INVOICE I1 ON A.INVOICEID=I1.INVOICEID WHERE A.ApprovedOn IS NULL AND A.ApprovalLevel = 3 AND A.AppUserId=I.APPUSERID and stage=2 and PaidOn is null AND STATUS='U' and datediff(day,I1.EnteredOn,getdate()) > 30 AND A.InvoiceId Not In (Select Invoiceid from POS_Invoice_Approvals with(nolock) where A.ApprovedOn IS NOT NULL AND A.ApprovalLevel in (1,2,3) )) as DAYS_30, 4 As ApprovalLevel from POS_Invoice_Approvals I with(nolock)where I.AppUserId>0  " + OfficeFilter + " AND  I.AppUserId = " + int.Parse(Session["loginid"].ToString()) + "   AND i.InvoiceId in (select invoiceid from pos_invoice where STATUS='U' and stage=2)    and I.ApprovedOn IS NULL AND I.ApprovalLevel = 4 GROUP BY APPUSERID) A ORDER BY A.USERNAME";
        }
        else if (Mode == 3) // Payment
        {
            if (ddlOffice.SelectedIndex > 0)
                OfficeFilter = " and PaidFwdTo in (select LoginId from DBO.UserLogin pp where pp.RecruitingOfficeId like '%" + ddlOffice.SelectedValue + "%' )";

            sql = "select PaidFwdTo as userid,COUNT(*) AS NOOFINV , " +
           "(SELECT FIRSTNAME + ' ' + LastName FROM DBO.UserLogin P WHERE P.LoginId  = I.PaidFwdTo) AS USERNAME, " +
           "(select COUNT(*) from pos_invoice I1 where I1.PaidFwdTo=I.PaidFwdTo and stage=3 and PaidOn is null AND STATUS='U' and datediff(day,I1.EnteredOn,getdate()) BETWEEN 0 AND 5 ) as DAYS_5, " +
           "(select COUNT(*) from pos_invoice I1 where I1.PaidFwdTo=I.PaidFwdTo and stage=3 and PaidOn is null AND STATUS='U' and datediff(day,I1.EnteredOn,getdate()) between 6 AND 10 ) as DAYS_10, " +
           "(select COUNT(*) from pos_invoice I1 where I1.PaidFwdTo=I.PaidFwdTo and stage=3 and PaidOn is null AND STATUS='U' and datediff(day,I1.EnteredOn,getdate()) between 11 AND 30 ) as DAYS_15, " +
           "(select COUNT(*) from pos_invoice I1 where I1.PaidFwdTo=I.PaidFwdTo and stage=3 and PaidOn is null AND STATUS='U' and datediff(day,I1.EnteredOn,getdate()) > 30 ) as DAYS_30, " +
           " 0 As ApprovalLevel" +
           " from pos_invoice I where PaidFwdTo in (select userid from pos_invoice_mgmt where payment=1) " + OfficeFilter + " and stage=3 and PaidOn is null AND STATUS='U' GROUP BY PaidFwdTo ORDER BY USERNAME";
        }
        else if (Mode == 4) // OverDue
        {
            if (ddlOffice.SelectedIndex > 0)
                OfficeFilter = " and PaidFwdTo in (select LoginId from DBO.UserLogin pp where pp.RecruitingOfficeId like '%" + ddlOffice.SelectedValue + "%' )";

            sql = "select PaidFwdTo as userid,COUNT(*) AS NOOFINV , " +
           "(SELECT FIRSTNAME + ' ' + LastName FROM DBO.UserLogin P WHERE P.LoginId  = I.PaidFwdTo) AS USERNAME, " +
           "(select COUNT(*) from pos_invoice I1 where I1.PaidFwdTo=I.PaidFwdTo and stage=3 and PaidOn is null AND STATUS='U' and datediff(day,I1.EnteredOn,getdate()) BETWEEN 0 AND 5 ) as DAYS_5, " +
           "(select COUNT(*) from pos_invoice I1 where I1.PaidFwdTo=I.PaidFwdTo and stage=3 and PaidOn is null AND STATUS='U' and datediff(day,I1.EnteredOn,getdate()) between 6 AND 10 ) as DAYS_10, " +
           "(select COUNT(*) from pos_invoice I1 where I1.PaidFwdTo=I.PaidFwdTo and stage=3 and PaidOn is null AND STATUS='U' and datediff(day,I1.EnteredOn,getdate()) between 11 AND 30 ) as DAYS_15, " +
           "(select COUNT(*) from pos_invoice I1 where I1.PaidFwdTo=I.PaidFwdTo and stage=3 and PaidOn is null AND STATUS='U' and datediff(day,I1.EnteredOn,getdate()) > 30 ) as DAYS_30, " +
            " 0 As ApprovalLevel" +
           " from pos_invoice I where PaidFwdTo in (select userid from pos_invoice_mgmt where payment=1) " + OfficeFilter + " and stage=3 and PaidOn is null AND STATUS='U' GROUP BY PaidFwdTo ORDER BY USERNAME";
        }
        RptInvoicesSummary.DataSource = Common.Execute_Procedures_Select_ByQuery(sql);
        RptInvoicesSummary.DataBind();
    }
    protected void btnPost_Click(object sender, EventArgs e)
    {
        BindList();
    }
    public string DateFilter(int days)
    {
        switch (days)
        {
            case 5:
                return " and datediff(day,I1.EnteredOn,getdate()) BETWEEN 0 AND 5 ";
                break;
            case 10:
                return " and datediff(day,I1.EnteredOn,getdate()) BETWEEN 6 AND 10 ";
                break;
            case 15:
                return " and datediff(day,I1.EnteredOn,getdate()) BETWEEN 11 AND 30 ";
                break;
             case 30:
                return " and datediff(day,I1.EnteredOn,getdate()) > 30 ";
                break;
                default:
                return "";
                break;
        }
    }
    protected void BindList()
    {
        int Mode = Common.CastAsInt32(Request.QueryString["Mode"]);
        string SQL = "";
        switch (Mode)
        {

            case 1:
                SQL = "select * from vw_POS_Invoices_001 P where P.InvoiceId In (select I1.InvoiceId from pos_invoice I1 where I1.approvalfwdto=" + hfdUserId.Value + " and stage=1  AND STATUS='U' " + DateFilter(Common.CastAsInt32(hfdDays.Value)) + " )";
                lblHeading.Text = "Processing Stage Invoices";
                break;
            case 2:
                int ApprovalLevel = Convert.ToInt32(hfdApprovalLevel.Value);
                if (ApprovalLevel == 1)
                {
                    SQL = " select * from vw_POS_Invoices_001 P with(nolock) where P.InvoiceId In (SELECT DISTINCT A.InvoiceId FROM POS_Invoice_Approvals A with(nolock) INNER JOIN POS_INVOICE I1 with(nolock) ON A.INVOICEID=I1.INVOICEID WHERE A.ApprovedOn IS NULL AND A.AppUserId=" + hfdUserId.Value + " and stage=2 and PaidOn is null AND STATUS='U' and Stage = 2 AND A.ApprovalLevel = 1 " + DateFilter(Common.CastAsInt32(hfdDays.Value)) + " ) ";
                }
                else if (ApprovalLevel == 2)
                {
                    SQL = " select * from vw_POS_Invoices_001 P with(nolock) where P.InvoiceId In (SELECT DISTINCT A.InvoiceId FROM POS_Invoice_Approvals A with(nolock) INNER JOIN (select InvoiceId,count(*) As Approvalcount from POS_Invoice_Approvals where ISNULL(ApprovedOn,'') <> '' group by InvoiceId having count(*) = 1 ) b on A.InvoiceId  = b.InvoiceId WHERE  A.AppUserId=" + hfdUserId.Value + " AND A.InvoiceId in (Select InvoiceId from POS_INVOICE I1 with(nolock) where stage=2 and PaidOn is null AND STATUS='U'  ) " + DateFilter(Common.CastAsInt32(hfdDays.Value)) + " )";
                }
                else if (ApprovalLevel == 3)
                {
                    SQL = " select * from vw_POS_Invoices_001 P with(nolock) where P.InvoiceId In (SELECT DISTINCT A.InvoiceId FROM POS_Invoice_Approvals A with(nolock) INNER JOIN (select InvoiceId,count(*) As Approvalcount from POS_Invoice_Approvals where ISNULL(ApprovedOn,'') <> '' group by InvoiceId having count(*) = 2 ) b on A.InvoiceId  = b.InvoiceId WHERE  A.AppUserId=" + hfdUserId.Value + " AND A.InvoiceId in (Select InvoiceId from POS_INVOICE I1 with(nolock) where stage=2 and PaidOn is null AND STATUS='U'  ) " + DateFilter(Common.CastAsInt32(hfdDays.Value)) + " )";
                }
                else
                {
                    SQL = " select * from vw_POS_Invoices_001 P with(nolock) where P.InvoiceId In (SELECT DISTINCT A.InvoiceId FROM POS_Invoice_Approvals A with(nolock) INNER JOIN (select InvoiceId,count(*) As Approvalcount from POS_Invoice_Approvals where ISNULL(ApprovedOn,'') <> '' group by InvoiceId having count(*) = 3 ) b on A.InvoiceId  = b.InvoiceId WHERE  A.AppUserId=" + hfdUserId.Value + " AND A.InvoiceId in (Select InvoiceId from POS_INVOICE I1 with(nolock) where stage=2 and PaidOn is null AND STATUS='U'  ) " + DateFilter(Common.CastAsInt32(hfdDays.Value)) + " )";
                }
                lblHeading.Text = "Approval Stage Invoices";
                break;
            case 3:
                SQL = "select * from vw_POS_Invoices_001 P where P.InvoiceId In (select I1.InvoiceId from pos_invoice I1 where I1.PaidFwdTo=" + hfdUserId.Value + " and stage=3 and PaidOn is null AND STATUS='U' " + DateFilter(Common.CastAsInt32(hfdDays.Value)) + " )";
                lblHeading.Text = "Payment Stage Invoices";
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


    public string showblankifZero(object val)
    {
        if (Common.CastAsInt32(val) == 0)
            return "";
        else
            return val.ToString();

    }
}