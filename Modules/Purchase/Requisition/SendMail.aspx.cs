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
using DocumentFormat.OpenXml.Wordprocessing;
using System.Threading.Tasks;

public partial class Modules_Purchase_Requisition_SendMail : System.Web.UI.Page
{
    public int MailType
    {
        set { ViewState["MailType"] = value; }
        get { return Convert.ToInt32(ViewState["MailType"]); }
    }
    public int Param
    {
        set { ViewState["Param"] = value; }
        get { return Common.CastAsInt32(ViewState["Param"]); }
    }
    public Boolean UpdateBack
    {
        set { ViewState["UpdateBack"] = value; }
        get { return Convert.ToBoolean(ViewState["UpdateBack"]); }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //---------------------------------------
        ProjectCommon.SessionCheck();
        //---------------------------------------
        if (!(IsPostBack))
        {
            MailType = int.Parse("0" + Request.QueryString["MailType"]);
            Param = int.Parse("0" + Request.QueryString["Param"]);

            if (Common.CastAsInt32(Request.QueryString["UpdateBack"]) > 0)
            {
                UpdateBack = true;
            }
            else
            {
                UpdateBack = false;
            }

            if (MailType == 1) // RFQ MAIL FOR VENDOR
            {
              LoadRFQmail(Param);
            }
            if (MailType == 2) // PO MAIL FOR VENDOR
            {
                 LoadPOmail(Param);
            }
        }
    }
    private void LoadRFQmail(int BidId)
    {
        // Attachments -----------------------------------------------------------------------------------------------------------------------
        ReportDocument rpt = new ReportDocument();
        Common.Set_Procedures("sp_NewPR_getRFQMasterByBidId");
        Common.Set_ParameterLength(1);
        Common.Set_Parameters(
            new MyParameter("@BidId ", BidId)
            );
        DataSet DsRFQMaster;
        DsRFQMaster = Common.Execute_Procedures_Select();
        //Bidexchrate
        decimal Bidexchrate = 0;
        string RFQNo = "";

        if (DsRFQMaster != null)
        {
            if (DsRFQMaster.Tables[0].Rows.Count > 0)
            {
                Bidexchrate = Common.CastAsDecimal(DsRFQMaster.Tables[0].Rows[0]["Bidexchrate"]);
                RFQNo = DsRFQMaster.Tables[0].Rows[0]["RFQNO"].ToString();
                aFile.HRef = "~/EMANAGERBLOB/Purchase/TempPoFiles/" + "RFQ-" + RFQNo + ".pdf";
                ViewState["FilePath"] = Server.MapPath("~/EMANAGERBLOB/Purchase/TempPoFiles/" + "RFQ-" + RFQNo + ".pdf");
            }
        }
        decimal TotalLC = 0;
        decimal TotalGSTLC = 0;
        Common.Set_Procedures("sp_NewPR_getRFQDetailsByBidId");
        //Common.Set_Procedures("sp_NewPR_getRFQDetailsByBidId_ProductAccepted");
        Common.Set_ParameterLength(2);
        Common.Set_Parameters(
            new MyParameter("@BidId ", BidId),
            new MyParameter("@ExchRate ", Bidexchrate)
            );
        DataSet DsRFQDetail;
        DsRFQDetail = Common.Execute_Procedures_Select();

        if (DsRFQDetail != null)
        {
            foreach (DataRow dr in DsRFQDetail.Tables[0].Rows)
            {
                TotalLC = Common.CastAsDecimal(TotalLC + Common.CastAsDecimal(dr["LCPoTotal"]));
                TotalGSTLC = Common.CastAsDecimal(TotalGSTLC + Common.CastAsDecimal(dr["GSTTaxAmtLC"]));
            }
        }
        TotalLC = TotalLC + TotalGSTLC + Common.CastAsDecimal(DsRFQMaster.Tables[0].Rows[0]["ESTSHIPPINGFOR"]) - Common.CastAsDecimal(DsRFQMaster.Tables[0].Rows[0]["TotalDiscount"]);

        DataTable dtnew = new DataTable();
        dtnew.Load(DsRFQDetail.Tables[0].CreateDataReader());
        DsRFQMaster.Tables.Add(dtnew);


        rpt.Load(Server.MapPath("~/Modules/Purchase/Report/PrintQuotes.rpt"));
        DsRFQMaster.Tables[0].TableName = "sp_NewPR_getRFQMasterByBidId;1";
        DsRFQMaster.Tables[1].TableName = "sp_NewPR_getRFQDetailsByBidId;1";
        rpt.SetDataSource(DsRFQMaster);
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT SHIPNAME FROM VW_tblSMDVessels WHERE SHIPID='" + DsRFQMaster.Tables[0].Rows[0]["SHIPID"].ToString() + "'");
        string VslName = "";
        if (dt.Rows.Count > 0)
        {
            VslName = dt.Rows[0][0].ToString();
        }
        rpt.SetParameterValue("VSLName", VslName);
        rpt.SetParameterValue("QuoteTotal", TotalLC);
        //rpt.SetParameterValue("@BidId", BidId);
        //rpt.SetParameterValue("@ExchRate", Bidexchrate);
        
        rpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath("~/EMANAGERBLOB/Purchase/TempPoFiles/" + "RFQ-" + RFQNo + ".pdf"));
           
        //----------------------------------------------------------------------------------------------------------------------------------------
        char[] MailSep = { ';' };
        lblHeader.Text = "Quotation Inquiry";
        txtFrom.Text = ConfigurationManager.AppSettings["FromAddress"];//ProjectCommon.gerUserEmail(Session["loginid"].ToString());
        DataTable dsEmail = Common.Execute_Procedures_Select_ByQuery("SELECT BidVenEmail,BidPass FROM dbo.VW_tblSMDPOMasterBid Where BidId=" + BidId);
        string[] VendorEmails = dsEmail.Rows[0]["BidVenEmail"].ToString().Split(MailSep);
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
        txtBCC.Text = "";
        txtSubject.Text = DsRFQMaster.Tables[0].Rows[0]["SHIPID"].ToString() + " - " + DsRFQMaster.Tables[0].Rows[0]["PRNUM"].ToString() + " - " + DsRFQMaster.Tables[0].Rows[0]["BIDGROUP"].ToString() + " - Request for Quote";

        string MailContent = System.IO.File.ReadAllText(Server.MapPath("~/Modules/Purchase/Requisition/RFQMail.htm"));
        string RFQMailURL = ConfigurationManager.AppSettings["RFQMail"];

        string URl = RFQMailURL + BidId + "&validate=" + dsEmail.Rows[0]["BidPass"].ToString();

        MailContent = MailContent.Replace("$RFQLINK$", URl);
        litMessage.Text = MailContent;
        if (rpt != null)
        {
            rpt.Close();
            rpt.Dispose();
        }

    }
    private void LoadPOmail(int BidId)
    {
        ReportDocument rpt = new ReportDocument();
        Common.Set_Procedures("sp_NewPR_getRFQMasterByBidId");
        Common.Set_ParameterLength(1);
        Common.Set_Parameters(
            new MyParameter("@BidId ", BidId)
            );
        DataSet DsRFQMaster;
        DsRFQMaster = Common.Execute_Procedures_Select();
        //Bidexchrate
        decimal Bidexchrate = 0;
        string RFQNo = "";
        string POAccountCompany = "";
        string ReportName = "";
        if (DsRFQMaster != null)
        {
            if (DsRFQMaster.Tables[0].Rows.Count > 0)
            {
                Bidexchrate = Common.CastAsDecimal(DsRFQMaster.Tables[0].Rows[0]["Bidexchrate"]);
                RFQNo = DsRFQMaster.Tables[0].Rows[0]["RFQNO"].ToString();
                if (! string.IsNullOrEmpty(DsRFQMaster.Tables[0].Rows[0]["POAccountCompany"].ToString()))
                {
                    POAccountCompany = DsRFQMaster.Tables[0].Rows[0]["POAccountCompany"].ToString();
                }
                if (!string.IsNullOrEmpty(DsRFQMaster.Tables[0].Rows[0]["ReportName"].ToString()))
                {
                    ReportName = DsRFQMaster.Tables[0].Rows[0]["ReportName"].ToString();
                }
                    aFile.HRef = "~/EMANAGERBLOB/Purchase/TempPoFiles/" + RFQNo + ".pdf";
                ViewState["FilePath"] = Server.MapPath("~/EMANAGERBLOB/Purchase/TempPoFiles/" + RFQNo + ".pdf");
            }
        }
        decimal TotalLC = 0;
        decimal TotalUSD = 0;
        decimal TotalGSTLC = 0;
        decimal TotalGSTUSD = 0;
        // Common.Set_Procedures("sp_NewPR_getRFQDetailsByBidId");
        Common.Set_Procedures("sp_NewPR_getRFQDetailsByBidId_ProductAccepted"); 
        Common.Set_ParameterLength(2);
        Common.Set_Parameters(
            new MyParameter("@BidId ", BidId),
            new MyParameter("@ExchRate ", Bidexchrate)
            );
        DataSet DsRFQDetail;
        DsRFQDetail = Common.Execute_Procedures_Select();

        if (DsRFQDetail != null)
        {
            foreach (DataRow dr in DsRFQDetail.Tables[0].Rows)
            {
                TotalLC = Common.CastAsDecimal(TotalLC + Common.CastAsDecimal(dr["LCPoTotal"]));
                TotalUSD = Common.CastAsDecimal(TotalUSD + Common.CastAsDecimal(dr["UsdPoTotal"]));
                TotalGSTLC = Common.CastAsDecimal(TotalGSTLC + Common.CastAsDecimal(dr["GSTTaxAmtLC"]));
                TotalGSTUSD = Common.CastAsDecimal(TotalGSTUSD + Common.CastAsDecimal(dr["GSTTaxAmtUSD"]));
            }
        }
        TotalLC = TotalLC + TotalGSTLC + Common.CastAsDecimal(DsRFQMaster.Tables[0].Rows[0]["ESTSHIPPINGFOR"]) - Common.CastAsDecimal(DsRFQMaster.Tables[0].Rows[0]["TotalDiscount"]);
        TotalUSD = TotalUSD + TotalGSTUSD + Common.CastAsDecimal(DsRFQMaster.Tables[0].Rows[0]["ESTSHIPPINGUSD"]) - Common.CastAsDecimal(DsRFQMaster.Tables[0].Rows[0]["TotalDiscountUSD"]);

        DataTable dtnew = new DataTable();
        dtnew.Load(DsRFQDetail.Tables[0].CreateDataReader());

        //remove row wher order qty is 0
        DataView dv = dtnew.DefaultView;
        dv.RowFilter = "QTYPO<>0";

        DsRFQMaster.Tables.Add(dv.ToTable());
        string dbname = "";
        dbname = ConfigurationManager.AppSettings["DBName"].ToUpper().ToString();
        switch (dbname)
        {
            case "RPLEMANAGER":
                 rpt.Load(Server.MapPath("~/Modules/Purchase/Report/PrintRFQForMailRPL.rpt"));
                break;
            case "NSIPLEMANAGER":
                rpt.Load(Server.MapPath("~/Modules/Purchase/Report/PrintRFQForMailNSIPL.rpt"));
                break;
            case "BRAVOEMANAGER":

                //if (! string.IsNullOrEmpty(POAccountCompany) && POAccountCompany == "BRV")
                // {
                //     rpt.Load(Server.MapPath("~/Modules/Purchase/Report/PrintRFQForMailBRV.rpt"));
                // }
                //else
                // {
                //     rpt.Load(Server.MapPath("~/Modules/Purchase/Report/PrintRFQForMailRMS.rpt"));
                // }
                string ReportPath = "~/Modules/Purchase/Report/" + ReportName + "";
                rpt.Load(Server.MapPath(ReportPath));
                break;
            default:
                rpt.Load(Server.MapPath("~/Modules/Purchase/Report/PrintRFQForMail.rpt"));
                break;
        }
        DsRFQMaster.Tables[0].TableName = "sp_NewPR_getRFQMasterByBidId;1";
        DsRFQMaster.Tables[1].TableName = "sp_NewPR_getRFQDetailsByBidId;1";
        rpt.SetDataSource(DsRFQMaster);
        string ImageURL = "";
        string Address = "";
        string company = "";
        string email = "";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT SHIPNAME FROM VW_tblSMDVessels WHERE SHIPID='" + DsRFQMaster.Tables[0].Rows[0]["SHIPID"].ToString() + "'");
        string VslName = "";
        if (dt.Rows.Count > 0)
        {
            VslName = dt.Rows[0][0].ToString();
        }
        DataTable dt1 = new DataTable();
        if (dbname == "BRAVOEMANAGER" || dbname == "NSIPLEMANAGER")
        {
            string AccountCompany = "";
            string AccountComAddress = "";
            DataTable dt2 = new DataTable();
            if (!string.IsNullOrEmpty(POAccountCompany))
            {
                dt1 = Common.Execute_Procedures_Select_ByQuery("Select a.Address,a.[Company Name] As Company, a.ImageURL,a.Email from AccountCompanyHeader a with(nolock) where a.Company ='" + POAccountCompany.Trim() + "'");
                if (dt1.Rows.Count > 0)
                {
                    ImageURL = dt1.Rows[0]["ImageURL"].ToString();
                    Address = dt1.Rows[0]["Address"].ToString();
                    company = dt1.Rows[0]["Company"].ToString();
                    email = dt1.Rows[0]["Email"].ToString();
                }
                
                dt2 = Common.Execute_Procedures_Select_ByQuery("Select a.Address,a.[Company Name] As Company, a.ImageURL,a.Email from Accountcompany a with(nolock) Inner Join Vessel v with(nolock) on a.Company = v.AccontCompany where VesselCode ='" + DsRFQMaster.Tables[0].Rows[0]["SHIPID"].ToString() + "'");
                if (dt2.Rows.Count > 0)
                {
                    AccountCompany = dt2.Rows[0]["Company"].ToString();
                    AccountComAddress = dt2.Rows[0]["Address"].ToString();
                }
            }
            else
            {
               
                dt2 = Common.Execute_Procedures_Select_ByQuery("Select a.Address,a.[Company Name] As Company, a.ImageURL,a.Email from AccountCompanyHeader a with(nolock) where a.CompanyId = (Select top 1 v.POIssuingCompanyId from vessel v with(nolock) where v.VesselCode ='" + DsRFQMaster.Tables[0].Rows[0]["SHIPID"].ToString() + "')");
                if (dt2.Rows.Count > 0)
                {
                    AccountCompany = dt2.Rows[0]["Company"].ToString();
                    AccountComAddress = dt2.Rows[0]["Address"].ToString();
                    ImageURL = dt2.Rows[0]["ImageURL"].ToString();
                    Address = dt2.Rows[0]["Address"].ToString();
                    company = dt2.Rows[0]["Company"].ToString();
                    email = dt2.Rows[0]["Email"].ToString();
                }
            }
            string applicationurl = ConfigurationManager.AppSettings["ApplicationURL"].ToString();
            string ImageLocation = applicationurl + ImageURL;
            rpt.SetParameterValue("@BidId", BidId);
            rpt.SetParameterValue("@ExchRate", Bidexchrate);
            rpt.SetParameterValue("VSLName", VslName);
            rpt.SetParameterValue("QuoteTotal", TotalLC);
            rpt.SetParameterValue("ImageURL", ImageLocation);
            rpt.SetParameterValue("Address", Address);
            rpt.SetParameterValue("Company", company);
            rpt.SetParameterValue("Email", email);
            rpt.SetParameterValue("AccCompany", AccountCompany);
            rpt.SetParameterValue("AccComAddress", AccountComAddress);
            rpt.SetParameterValue("QuoteTotalUSD", TotalUSD);
        } 
        else
        {
            dt1 = Common.Execute_Procedures_Select_ByQuery("Select a.Address,a.[Company Name] As Company, a.ImageURL,a.Email from AccountCompanyHeader a with(nolock) where a.CompanyId = (Select top 1 v.POIssuingCompanyId from vessel v with(nolock) where v.VesselCode ='" + DsRFQMaster.Tables[0].Rows[0]["SHIPID"].ToString() + "')");
            if (dt1.Rows.Count > 0)
            {
                ImageURL = dt1.Rows[0]["ImageURL"].ToString();
                Address = dt1.Rows[0]["Address"].ToString();
                company = dt1.Rows[0]["Company"].ToString();
                email = dt1.Rows[0]["Email"].ToString();
            }
            string applicationurl = ConfigurationManager.AppSettings["ApplicationURL"].ToString();
            string ImageLocation = applicationurl + ImageURL;
            rpt.SetParameterValue("@BidId", BidId);
            rpt.SetParameterValue("@ExchRate", Bidexchrate);
            rpt.SetParameterValue("VSLName", VslName);
            rpt.SetParameterValue("QuoteTotal", TotalLC);
            rpt.SetParameterValue("ImageURL", ImageLocation);
            rpt.SetParameterValue("Address", Address);
            rpt.SetParameterValue("Company", company);
            rpt.SetParameterValue("Email", email);
        }
        rpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath("~/EMANAGERBLOB/Purchase/TempPoFiles/" + RFQNo + ".pdf"));
       
        //==============================================
        char[] MailSep = { ';' };
        lblHeader.Text = "PO mail to Vendor";
        txtFrom.Text = ConfigurationManager.AppSettings["FromAddress"];// ProjectCommon.gerUserEmail(Session["loginid"].ToString());
        DataTable dsEmail = Common.Execute_Procedures_Select_ByQuery("SELECT BidVenEmail,BidPass,BidPONum FROM dbo.VW_tblSMDPOMasterBid Where BidId=" + BidId);
        string[] VendorEmails = dsEmail.Rows[0]["BidVenEmail"].ToString().Split(MailSep);
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
        txtBCC.Text = "";

        DataTable dsvEmail = Common.Execute_Procedures_Select_ByQuery("select v.VesselEmailNew, isnull((select email from dbo.userlogin u where u.loginid=v.techsupdt),'') as techemail from dbo.vessel v where v.vesselcode = '" + DsRFQMaster.Tables[0].Rows[0]["SHIPID"].ToString() + "'");
        if(dsvEmail.Rows.Count >0)
        {
            string vesselemail = dsvEmail.Rows[0]["VesselEmailNew"].ToString();
            string techemail = dsvEmail.Rows[0]["techemail"].ToString();
            if(vesselemail.Trim()!="")
            {
                if (txtBCC.Text.Trim() == "")
                    txtBCC.Text = vesselemail;
                else
                    txtBCC.Text += ";" + vesselemail;
            }
            if (techemail.Trim() != "")
            {
                if (txtCC.Text.Trim() == "")
                    txtCC.Text = techemail;
                else
                    txtCC.Text += ";" + techemail;
            }
        }

        
        txtSubject.Text = "Purchase Order: " + dsEmail.Rows[0]["BidPoNUm"].ToString();

        string MailContent = System.IO.File.ReadAllText(Server.MapPath("~/Modules/Purchase/Requisition/POMail.htm"));

        //string POMailURL = ConfigurationManager.AppSettings["POMail"];

        //string URl = POMailURL + BidId + "&p=" + dsEmail.Rows[0]["BidPass"].ToString();

        //MailContent = MailContent.Replace("$RFQLINK$", URl);
        litMessage.Text = MailContent;

        if (rpt != null)
        {
            rpt.Close();
            rpt.Dispose();
        }
    }
    protected void btnSend_Click(object sender, EventArgs e)
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

        if (MailType == 1) // RFQ MAIL FOR VENDOR
        {
            AttachmentFilePath = ViewState["FilePath"].ToString();
        }
        if (MailType == 2)  // PO MAIL FOR VENDOR
        {

            AttachmentFilePath = ViewState["FilePath"].ToString();
        }

        if (ProjectCommon.SendeMail(txtFrom.Text, txtFrom.Text, ToAdds, CCAdds, BCCAdds, txtSubject.Text, str, out ErrMsg, AttachmentFilePath))
        {
            if (MailType == 1)
            {
                Common.Execute_Procedures_Select_ByQuery("EXEC sp_NewPR_UpdateBidStatus " + Param + ",1");
                Common.Execute_Procedures_Select_ByQuery("EXEC dbo.UpdateBidHistory " + Param + "," + UserId + ",'" + UserName + "','RFQ/Bid update request mail to vendor sent successfully.'");
            }
            //--------- REFRESH BACK PAGE ---------
            if (UpdateBack)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "dialog", "window.opener.ReloadPage();", true);
            }
            lblMessage.Text = "Mail sent successfully.";
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Mail sent successfully.');window.close();", true);
        }
        else
        {
            lblMessage.Text = "Unable to send Mail. Error : " + ErrMsg;
        }
    }
}
