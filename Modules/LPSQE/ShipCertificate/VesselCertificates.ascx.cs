using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.Security;
using System.Data.Common;
using System.Transactions;
using System.Web.UI.WebControls;
using ShipSoft.CrewManager.BusinessObjects;
using ShipSoft.CrewManager.BusinessLogicLayer;
using ShipSoft.CrewManager.Operational;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.IO;

public partial class VesselCertificates : System.Web.UI.UserControl
{
    Authority Auth;
    string Mode;

    public int RecId
    {
        set { ViewState["RecId"] = value ; }
        get { return int.Parse("0" + ViewState["RecId"]); }
    }
    public int Archived
    {
        set { ViewState["Archived"] = value; }
        get { return int.Parse("0" + ViewState["Archived"]); }
    }
    public void BindVessel()
    {
        try
        {
            DataSet dsVessel;
            //dsVessel = Budget.getTable("SELECT VesselId,VesselName as Name FROM dbo.Vessel WHERE VesselStatusId <> 2  order by VesselName");
            dsVessel = SearchSignOff.getVessel(Convert.ToInt32(Session["loginid"].ToString()));
            ddlVessel.DataSource = dsVessel.Tables[0];
            ddlVessel.DataTextField = "Name";
            ddlVessel.DataValueField = "VesselId";
            ddlVessel.DataBind();
            ddlVessel.Items.Insert(0, new ListItem("< Select >", "0"));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["loginid"] == null)
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Default.aspx';", true);
        }

        if (Session["loginid"] != null)
        {
            ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 6);
            OBJ.Invoke();
            Session["Authority"] = OBJ.Authority;
            Auth = OBJ.Authority;
        }
        try
        {
            if (!(IsPostBack))
            {
                //----------------
                btn_Save.Enabled  = false;
                btn_Cancel.Enabled = false;
                btn_Add.Visible = true;
                
                try{Alerts.HANDLE_AUTHORITY(14, btn_Add, btn_Save, btn_Cancel, new Button(), Auth);}catch{Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Default.aspx';", true);}
                //----------------
                BindVessel();
                //----------------
                try
                {
                    ddlVessel.SelectedValue = Request.QueryString["VId"].ToString();
                }
                catch { }
                RecId = int.Parse(Request.QueryString["RecId"].ToString());
                Mode = Request.QueryString["Mode"].ToString() ;
                Archived = int.Parse(Request.QueryString["IsArchive"].ToString());
                if (RecId > 0)
                {
                    btn_Add.Visible = false; 
                    if (Archived == 0)
                    {
                        Show_Record(RecId);
                    }
                    else
                    {
                        Show_RecordArchived(RecId);
                    }
                     
                }
                //----------------
            }
            if (Mode == "View") { btn_Save.Visible = false; btn_Add.Visible = false;}  
        }
        catch { }
    }
    private void Show_Record(int id)
    {
        DataTable dt = Budget.getTable("select vesselcertid,vc.vesselid,(select certname from dbo.certmaster cm where cm.certid=vc.certid) as certname,(select certcatid from dbo.certmaster cm where cm.certid=vc.certid) as cat, " +
                                    "(select certsubcatid from dbo.certmaster cm where cm.certid=vc.certid) as subcat, " + 
                                    "certid,certnumber,certnumber as CertificateNumber, Replace(Convert(Varchar,Issuedate,106),' ','-') as Issuedate, " +
                                    "Replace(Convert(Varchar,ExpiryDate,106),' ','-') as ExpiryDate, " + 
                                    "IssuedBy,PlaceIssued,CertType,NextSurveyInterval, " +
                                    "Replace(Convert(Varchar,NextSurveyDue,106),' ','-') as NextSurveyDue,Replace(Convert(Varchar,lastannsurvey,106),' ','-') as lastannsurvey, " + 
                                    "AlertForSurvey, " +
                                    "AlertForExpiry,FileName " +
                                    "from dbo.vesselcertificates vc Where vc.vesselcertid=" + id.ToString()).Tables[0];
        if(dt.Rows.Count>0)
        {
            pnl_Documents.Visible = true;
            ddlVessel.SelectedValue = dt.Rows[0]["vesselid"].ToString();
            txt_CertNumber.Text = dt.Rows[0]["CertificateNumber"].ToString();
            txt_Cert.Text = dt.Rows[0]["certname"].ToString();
            txt_IssueDate.Text = dt.Rows[0]["issuedate"].ToString();
            txt_ExpDate.Text = dt.Rows[0]["expirydate"].ToString();
            txt_IssueBy.Text = dt.Rows[0]["IssuedBy"].ToString();
            txt_PlaceIssued.Text = dt.Rows[0]["PlaceIssued"].ToString();
            ddl_CertType.SelectedValue = dt.Rows[0]["CertType"].ToString();
            txt_NextSurInt.Text  = dt.Rows[0]["NextSurveyInterval"].ToString();
            txt_NSD.Text = dt.Rows[0]["NextSurveyDue"].ToString();
            txtLastAnnSurvey.Text = dt.Rows[0]["lastannsurvey"].ToString();
            txt_AFS.Text=dt.Rows[0]["AlertForSurvey"].ToString();
            txt_AFE.Text = dt.Rows[0]["AlertForExpiry"].ToString();
            hfdFileName.Value = dt.Rows[0]["FileName"].ToString();
        }
    }

    private void Show_RecordArchived(int id)
    {
        DataTable dt = Budget.getTable("select vesselcertid,vc.vesselid,(select certname from dbo.certmaster cm where cm.certid=vc.certid) as certname,(select certcatid from dbo.certmaster cm where cm.certid=vc.certid) as cat, " +
                                    "(select certsubcatid from dbo.certmaster cm where cm.certid=vc.certid) as subcat, " +
                                    "certid,certnumber,certnumber as CertificateNumber, Replace(Convert(Varchar,Issuedate,106),' ','-') as Issuedate, " +
                                    "Replace(Convert(Varchar,ExpiryDate,106),' ','-') as ExpiryDate, " +
                                    "IssuedBy,PlaceIssued,CertType,NextSurveyInterval, " +
                                    "Replace(Convert(Varchar,NextSurveyDue,106),' ','-') as NextSurveyDue,Replace(Convert(Varchar,lastannsurvey,106),' ','-') as lastannsurvey, " +
                                    "AlertForSurvey, " +
                                    "AlertForExpiry,FileName " +
                                    "from dbo.VesselCertificates_Archive vc with(nolock) Where vc.vesselcertid=" + id.ToString()).Tables[0];
        if (dt.Rows.Count > 0)
        {
            pnl_Documents.Visible = true;
            ddlVessel.SelectedValue = dt.Rows[0]["vesselid"].ToString();
            txt_CertNumber.Text = dt.Rows[0]["CertificateNumber"].ToString();
            txt_Cert.Text = dt.Rows[0]["certname"].ToString();
            txt_IssueDate.Text = dt.Rows[0]["issuedate"].ToString();
            txt_ExpDate.Text = dt.Rows[0]["expirydate"].ToString();
            txt_IssueBy.Text = dt.Rows[0]["IssuedBy"].ToString();
            txt_PlaceIssued.Text = dt.Rows[0]["PlaceIssued"].ToString();
            ddl_CertType.SelectedValue = dt.Rows[0]["CertType"].ToString();
            txt_NextSurInt.Text = dt.Rows[0]["NextSurveyInterval"].ToString();
            txt_NSD.Text = dt.Rows[0]["NextSurveyDue"].ToString();
            txtLastAnnSurvey.Text = dt.Rows[0]["lastannsurvey"].ToString();
            txt_AFS.Text = dt.Rows[0]["AlertForSurvey"].ToString();
            txt_AFE.Text = dt.Rows[0]["AlertForExpiry"].ToString();
            hfdFileName.Value = dt.Rows[0]["FileName"].ToString();
        }
    }
    protected void btn_Cancel_Click(object sender, EventArgs e)
    {
        btn_Save.Visible = false;
        btn_Add.Visible = ((Mode == "Edit") || (Mode == "New")) ? true : false;
        btn_Cancel.Visible = false;
        lbl_message_documents.Visible = false;
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "Fsadfa", "javascript:window.opener.location.reload(true);self.close();", true);
        // Page.ClientScript.RegisterStartupScript(this.GetType(), "abc", "closeWindow();", true);
    }
    protected void btn_Add_Click(object sender, EventArgs e)
    {
        RecId = 0; 
        txt_Cert.Text = "";
        txt_CertNumber.Text = "";
        txt_IssueDate.Text = "";
        txt_ExpDate.Text="";
        txt_IssueBy.Text = "";
        txt_PlaceIssued.Text = "";  
        txt_NextSurInt.Text = "0";
        txt_NSD.Text = "";
        txt_AFS.Text = "";
        txt_AFE.Text = "";
        txtLastAnnSurvey.Text = "";  
        pnl_Documents.Visible = true;
        btn_Add.Visible = false;
        btn_Save.Visible = true;
        btn_Cancel.Visible = true;
        ViewState["certid"] = "0";
        hfdFileName.Value = "";
    }
    public int getCertId(string CertName)
    {
        DataTable dt = Budget.getTable("select certid from dbo.certmaster where ltrim(rtrim(certmaster.certname))='" + CertName + "'").Tables[0];
        if (dt.Rows.Count > 0)
        {
            return int.Parse(dt.Rows[0][0].ToString());
        }
        else { return 0; }
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        if (ddlVessel.SelectedIndex <= 0)
        {
            lbl_message_documents.Text = "Please select vessel.";
            return;
        }

        if ((txt_IssueDate.Text.Trim() != "") && (txt_ExpDate.Text.Trim() != ""))
        {
            if (DateTime.Parse(txt_ExpDate.Text.Trim()) < DateTime.Parse(txt_IssueDate.Text.Trim()))
            {
                lbl_message_documents.Text = "Expiry Date cannot less than Issue Date.";
                txt_ExpDate.Focus();
                return;
            }
        }
        int certid=getCertId(txt_Cert.Text.Trim ());
        if (certid <= 0)
        {
            lbl_message_documents.Text = "Certificate not exists.";  
            return; 
        }

        string idt,nsi,ct, edt, ib, pi, nsd, afs, afe , las;
        string strDoc = "", FileName = "";
        string FName = hfdFileName.Value;
        int a = 0;
        if (FileUpload1.PostedFile != null && FileUpload1.FileContent.Length > 0)
        {
            string strfilename = FileUpload1.FileName;
            HttpPostedFile file1 = FileUpload1.PostedFile;
            UtilityManager um = new UtilityManager();
            if (chk_FileExtension(Path.GetExtension(FileUpload1.FileName).ToLower()) == true)
            {
                strDoc = "EMANAGERBLOB/LPSQE/Vessel_Certificates/" + FileUpload1.FileName.Trim();
                FileName = um.UploadFileToServer(file1, strfilename, "", "VC");
                a = FileName.LastIndexOf(".");
                FName = FileName.Substring(0, a);
                if (FileName.StartsWith("?"))
                {
                    lbl_message_documents.Text = FileName.Substring(1);
                    return;
                }
            }
            else
            {
                lbl_message_documents.Text = "Invalid File Type. (Valid Files Are .pdf)";
                FileUpload1.Focus();
                return;
            }
        }

        idt= (txt_IssueDate.Text.Trim() == "") ? "NULL" : "'" + txt_IssueDate.Text.Trim() + "'";
        edt = (txt_ExpDate.Text.Trim() == "") ? "NULL" : "'" + txt_ExpDate.Text.Trim() + "'";
        ib = txt_IssueBy.Text.Trim();  
        pi=txt_PlaceIssued.Text.Trim();   
        ct=ddl_CertType.SelectedValue; 
        nsi=txt_NextSurInt.Text.Trim();  
        nsd = (txt_NSD.Text.Trim() == "") ? "NULL" : "'" + txt_NSD.Text.Trim() + "'";
        las = (txtLastAnnSurvey.Text.Trim() == "") ? "NULL" : "'" + txtLastAnnSurvey.Text.Trim() + "'";
        afs = txt_AFS.Text.Trim();
        afe = txt_AFE.Text.Trim();
        try
        {
            if (RecId <=0)
            {
                Budget.getTable("INSERT INTO DBO.VESSELCERTIFICATES (VESSELID,CERTID,CERTNUMBER,ISSUEDATE,EXPIRYDATE,ISSUEDBY,PLACEISSUED,CERTTYPE,NEXTSURVEYINTERVAL,NEXTSURVEYDUE,ALERTFORSURVEY,ALERTFOREXPIRY,FILENAME,LASTANNSURVEY)" +
                "VALUES(" + ddlVessel.SelectedValue + "," + certid.ToString() + ",'" + txt_CertNumber.Text.Trim() + "'," + idt + "," + edt + ",'" + ib + "','" + pi + "','" + ct + "','" + nsi + "'," + nsd + ",'" + afs + "','" + afe + "','" + FName + "'," + las + ")");
            }
            else
            {
                Budget.getTable("UPDATE DBO.VESSELCERTIFICATES" +
                " SET VESSELID=" + ddlVessel.SelectedValue +
                ",CERTID=" + certid.ToString() +
                ",CERTNUMBER='" + txt_CertNumber.Text.Trim() + "'" +
                ",ISSUEDATE=" + idt +
                ",EXPIRYDATE=" + edt +
                ",ISSUEDBY='" + ib + "'" +
                ",PLACEISSUED='" + pi + "'" +
                ",CERTTYPE='" + ct + "'" +
                ",NEXTSURVEYINTERVAL='" + nsi + "'" +
                ",NEXTSURVEYDUE=" + nsd +
                ",ALERTFORSURVEY='" + afs + "'" +
                ",ALERTFOREXPIRY='" + afe + "'" +
                ",LASTANNSURVEY=" + las +
                ",FILENAME='" + FName + "' WHERE VESSELCERTIFICATES.VESSELCeRTID=" + RecId.ToString());
            }
            lbl_message_documents.Text="Record saved successfully.";  
        }
        catch
        {
            lbl_message_documents.Text="Unable to save the record.";  
        }
        btn_Add_Click(sender, e);
        lbl_message_documents.Visible = true;
    }
    protected void txt_NextSurInt_TextChanged(object sender, EventArgs e)
    {
        try
        {
            txt_NSD.Text = Convert.ToDateTime(txtLastAnnSurvey.Text).AddMonths(int.Parse(txt_NextSurInt.Text)).ToString("dd-MMM-yyyy");
        }
        catch { txt_NSD.Text = ""; }
    }
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        return default(string[]);
    }
    public bool chk_FileExtension(string str)
    {
        string extension = str;
        string MIMEType = null;
        switch (extension)
        {
            case ".txt":
                return true;
            case ".doc":
                return true;
            case ".docx":
                return true;
            case ".xls":
                return true;
            case ".xlsx":
                return true;
            case ".pdf":
                return true;
            default:
                return false;
                break;
        }
    }
    public string GetPath(string _path)
    {
        string res = "";
        if (_path.StartsWith("U"))
        {
            res = "../" + _path;
        }
        else
        {
            res = "../EMANAGERBLOB/LPSQE/Vessel_Certificates/" + _path + ".PDF";
        }
        return res;
    }
}
