using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Net.Mail;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;

public partial class Modules_Purchase_Invoice_SendMailforPaymentReceipt : System.Web.UI.Page
{


    public int PaymentId
    {
        set { ViewState["PaymentId"] = value; }
        get { return Convert.ToInt32(ViewState["PaymentId"]); }
    }
    public string pvno
    {
        set { ViewState["pvno"] = value; }
        get { return ViewState["pvno"].ToString(); }
    }
    public int DocId
    {
        set { ViewState["DocId"] = value; }
        get { return Convert.ToInt32(ViewState["DocId"]); }
    }
    public Boolean UpdateBack
    {
        set { ViewState["UpdateBack"] = value; }
        get { return Convert.ToBoolean(ViewState["UpdateBack"]); }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
       try
        {
            //---------------------------------------
            ProjectCommon.SessionCheck();
            //---------------------------------------
            if (!(IsPostBack))
            {
                PaymentId = int.Parse(Request.QueryString["PaymentId"]);
                pvno = Request.QueryString["pvno"];
                hdnPVno.Value = pvno.ToString();

                if (Common.CastAsInt32(Request.QueryString["UpdateBack"]) > 0)
                {
                    UpdateBack = true;
                }
                else
                {
                    UpdateBack = false;
                }
                LoadPVDetails(pvno);
                ShowAttachment();
            }
        }
       catch(Exception ex)
        {
            lblMessage.Text = ex.Message.ToString();
        }
    }

    protected void ShowAttachment()
    {
        string sql = "";
        if (pvno != "")
        {
            sql = "Select ID,PVNo,DocName,DocType,Doc from POS_Invoice_Payment_Document with(nolock) where PVNo = '" + pvno + "'";
            DataTable DT = Common.Execute_Procedures_Select_ByQuery(sql);
           if (DT.Rows.Count > 0)
            {
                DocId = Convert.ToInt32(DT.Rows[0]["ID"]);
                hdnDocId.Value = DocId.ToString();
            }
        }
    }

    protected void btnSend_Click(object sender, EventArgs e)
    {
        try
        {
            int UserId = Common.CastAsInt32(Session["loginid"]);
            string UserName = Convert.ToString(Session["FullName"]);

            string str = hfdMessage.Value;
            litMessage.Text = str;
            char[] Sep = { ';' };
            string[] ToAdds = txtTo.Text.Split(Sep);
            //  string[] ToAdds = {"purchase@panbulk.co.in"};
            string[] CCAdds = txtCC.Text.Split(Sep);
            string[] BCCAdds = txtBCC.Text.Split(Sep);
            //------------------
            string ErrMsg = "";
            string AttachmentFilePath = "";

            AttachmentFilePath = ViewState["FilePath"].ToString();
            string sql = "SELECT top 1 DocName As FileName,Doc As Attachment,DocType As ContentType FROM [POS_Invoice_Payment_Document] WHERE  ID =" + DocId + " and PVNo = '" + pvno + "' ";
            DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
            if (dt.Rows.Count > 0)
            {
                try
                {
                    string contentType = "";
                    string FileName = "";
                    if (!string.IsNullOrWhiteSpace(dt.Rows[0]["ContentType"].ToString()))
                    {
                        contentType = dt.Rows[0]["ContentType"].ToString();
                    }
                    if (!string.IsNullOrWhiteSpace(dt.Rows[0]["FileName"].ToString()))
                    {
                        FileName = dt.Rows[0]["FileName"].ToString();
                    }
                    if (!string.IsNullOrWhiteSpace(contentType) && !string.IsNullOrWhiteSpace(FileName))
                    {

                        byte[] latestFileContent = (byte[])dt.Rows[0]["Attachment"];
                        if (ProjectCommon.SendeMailforPV(txtFrom.Text, txtFrom.Text, ToAdds, CCAdds, BCCAdds, txtSubject.Text, str, out ErrMsg, AttachmentFilePath, latestFileContent, FileName))
                        {
                            ////--------- REFRESH BACK PAGE ---------
                            //if (UpdateBack)
                            //{
                            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "dialog", "window.opener.ReloadPage();", true);
                            //}
                            lblMessage.Text = "Mail sent successfully.";
                            ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Mail sent successfully.');window.close();", true);
                        }
                        else
                        {
                            lblMessage.Text = "Unable to send Mail. Error : " + ErrMsg;
                        }
                    }
                    else
                    {
                        if (ProjectCommon.SendeMail(txtFrom.Text, txtFrom.Text, ToAdds, CCAdds, BCCAdds, txtSubject.Text, str, out ErrMsg, AttachmentFilePath))
                        {
                            ////--------- REFRESH BACK PAGE ---------
                            //if (UpdateBack)
                            //{
                            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "dialog", "window.opener.ReloadPage();", true);
                            //}
                            lblMessage.Text = "Mail sent successfully.";
                            ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Mail sent successfully.');window.close();", true);
                        }
                        else
                        {
                            lblMessage.Text = "Unable to send Mail. Error : " + ErrMsg;
                        }
                    }

                }
                catch (Exception ex)
                {

                }
            }
           else 
            {
                if (ProjectCommon.SendeMail(txtFrom.Text, txtFrom.Text, ToAdds, CCAdds, BCCAdds, txtSubject.Text, str, out ErrMsg, AttachmentFilePath))
                {
                    ////--------- REFRESH BACK PAGE ---------
                    //if (UpdateBack)
                    //{
                    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "dialog", "window.opener.ReloadPage();", true);
                    //}
                    lblMessage.Text = "Mail sent successfully.";
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Mail sent successfully.');window.close();", true);
                }
                else
                {
                    lblMessage.Text = "Unable to send Mail. Error : " + ErrMsg;
                }
            }
        }
        catch(Exception ex)
        {
            lblMessage.Text = "Unable to send Mail. Error : " + ex.Message.ToString();
        }
        
    }
    private void LoadPVDetails(string pvno)
    {
        string SQL = "SELECT * FROM vw_POS_Invoice_Print WHERE [PVNo] ='" + pvno.ToString() + "'";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
        if (dt.Rows.Count > 0)
        {
            DataTable dt1 = Common.Execute_Procedures_Select_ByQuery("SELECT ACCOUNTNUMBER FROM [dbo].[VW_sql_tblSMDPRAccounts]  WHERE ACCOUNTID IN (SELECT ACCOUNTID FROM [dbo].[VW_tblSMDPOMaster] WHERE POID IN (SELECT POID FROM VW_TBLSMDPOMASTERBID WHERE BIDPONUM='" + dt.Rows[0]["PONO"] + "'))");
            if (dt1.Rows.Count > 0)
            {
                dt.Rows[0]["AccountCode"] = dt1.Rows[0][0];
            }
            aFile.HRef = "~/EMANAGERBLOB/Purchase/Invoice/PaymentVoucher/" + pvno + ".pdf";
            ViewState["FilePath"] = Server.MapPath("~/EMANAGERBLOB/Purchase/Invoice/PaymentVoucher/" + pvno + ".pdf");
            ReportDocument rpt = new ReportDocument();
            rpt.Load(Server.MapPath("~/Modules/Purchase/Invoice/PaymentVoucher.rpt"));          
            rpt.SetDataSource(dt);
          

            rpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath("~/EMANAGERBLOB/Purchase/Invoice/PaymentVoucher/" + pvno + ".pdf"));

            //==============================================
            char[] MailSep = { ';' };
            lblHeader.Text = "Payment Detail mail to Vendor";
            txtFrom.Text = ConfigurationManager.AppSettings["FromAddress"]; 
         
            string[] VendorEmails = dt.Rows[0]["SupplierEmail"].ToString().Split(MailSep);
            txtTo.Text = "";
            for (int cnt = 0; cnt <= VendorEmails.Length - 1; cnt++)
            {
                MailAddress ma;
                try
                {
                    ma = new MailAddress(VendorEmails[cnt]);
                    txtTo.Text = txtTo.Text.Trim() + ";" + VendorEmails[cnt].Trim();
                }
                catch { };
            }
            if (txtTo.Text.StartsWith(";")) { txtTo.Text = txtTo.Text.Substring(1); }

            txtCC.Text = ProjectCommon.gerUserEmail(Session["loginid"].ToString());
            string MailContent = "";
            string strPaymentDetails = ConfigurationManager.AppSettings["PaymentdetailMail"];
            string sql1 = "Select top 1 SMC_Frommail, SMC_BCC, SMC_Body, SMC_Subject from SendMailConfiguration s with(nolock) WHERE SMC_Statusid = 'A' AND SMC_ProcessId = (Select EmailProId from EmailProcess with(nolock) WHERE LTRIM(RTRIM(UPPER(EmailProName))) = LTRIM(RTRIM(UPPER('"+ strPaymentDetails + "'))))";
            DataTable dt2 = Common.Execute_Procedures_Select_ByQuery(sql1);
            if (dt2.Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(dt2.Rows[0]["SMC_Frommail"].ToString()))
                {
                    txtFrom.Text = dt2.Rows[0]["SMC_Frommail"].ToString();
                }
                if (!string.IsNullOrEmpty(dt2.Rows[0]["SMC_BCC"].ToString()))
                {
                    txtBCC.Text = dt2.Rows[0]["SMC_BCC"].ToString();
                }
                if (!string.IsNullOrEmpty(dt2.Rows[0]["SMC_Subject"].ToString()))
                {
                    txtSubject.Text = dt2.Rows[0]["SMC_Subject"].ToString() + ":" + pvno.ToString();
                }
                if (!string.IsNullOrEmpty(dt2.Rows[0]["SMC_Body"].ToString()))
                {
                    MailContent = dt2.Rows[0]["SMC_Body"].ToString();
                }
            }

            //  txtBCC.Text = "";
            // txtSubject.Text = "Payment Detail : " + pvno.ToString();
            // string MailContent = System.IO.File.ReadAllText(Server.MapPath("~/Modules/Purchase/Invoice/PaymentVoucherMail.htm"));

            litMessage.Text = MailContent;
        }
    }
 }