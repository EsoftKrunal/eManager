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

public partial class VesselCertificates : System.Web.UI.UserControl
{
    Authority Auth;
    string Mode;
    private int _VesselId;
    public int VesselId
    {
        get { return _VesselId; }
        set { _VesselId = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 4);
        OBJ.Invoke();
        Session["Authority"] = OBJ.Authority;
        Auth = OBJ.Authority;
        try
        {
            Mode = Session["VMode"].ToString();
        }
        catch { Mode = "New"; }
        VesselId = int.Parse(Session["VesselId"].ToString()); 
        lbl_VesselDoc.Text = ""; 
        if (!(IsPostBack))
        {
            BindFlagNameDropDown();
            pnl_Documents.Visible = false;
            btn_Save.Visible = false;
            btn_Cancel.Visible = false;
            btn_Add.Visible = ((Mode == "Edit") || (Mode == "New")) ? true : false;
            btn_Print.Visible = Auth.isPrint;
            Bind_grid(VesselId);  
        }
        try
        {
            txtVesselName.Text = Session["VesselName"].ToString();
            txtFormerVesselName.Text = Session["FormerName"].ToString();
            ddlFlagStateName.SelectedValue = Session["FlagStateId"].ToString();
        }
        catch { }
    }
    private void BindFlagNameDropDown()
    {
        DataTable dt1 = VesselDetailsGeneral.selectDataFlag();
        this.ddlFlagStateName.DataValueField = "FlagStateId";
        this.ddlFlagStateName.DataTextField = "FlagStateName";
        this.ddlFlagStateName.DataSource = dt1;
        this.ddlFlagStateName.DataBind();
    }
    private void Show_Record(int id)
    {
        DataTable dt = Budget.getTable("select vesselcertid,(select certname from certmaster cm where cm.certid=vc.certid) as certname,(select certcatid from certmaster cm where cm.certid=vc.certid) as cat, " + 
                                    "(select certsubcatid from certmaster cm where cm.certid=vc.certid) as subcat, " + 
                                    "certid,certnumber,certnumber as CertificateNumber, Convert(Varchar,Issuedate,101) as Issuedate, " +  
                                    "Convert(Varchar,ExpiryDate,101) as ExpiryDate, " + 
                                    "IssuedBy,PlaceIssued,CertType,NextSurveyInterval, " + 
                                    "Convert(Varchar,NextSurveyDue,101) as NextSurveyDue, " + 
                                    "AlertForSurvey, " +
                                    "AlertForExpiry " + 
                                    "from vesselcertificates vc Where vc.vesselcertid=" + id.ToString()).Tables[0];
        if(dt.Rows.Count>0)
        {
            pnl_Documents.Visible = true;
            HiddenDocId.Value = dt.Rows[0]["vesselcertid"].ToString();
            txt_CertNumber.Text = dt.Rows[0]["CertificateNumber"].ToString();
            txt_Cert.Text = dt.Rows[0]["certname"].ToString();
            txt_IssueDate.Text = dt.Rows[0]["issuedate"].ToString();
            txt_ExpDate.Text = dt.Rows[0]["expirydate"].ToString();
            txt_IssueBy.Text = dt.Rows[0]["IssuedBy"].ToString();
            txt_PlaceIssued.Text = dt.Rows[0]["PlaceIssued"].ToString();
            ddl_CertType.SelectedValue = dt.Rows[0]["CertType"].ToString();
            txt_NextSurInt.Text  = dt.Rows[0]["NextSurveyInterval"].ToString();
            txt_NSD.Text = dt.Rows[0]["NextSurveyDue"].ToString();
            txt_AFS.Text=dt.Rows[0]["AlertForSurvey"].ToString();
            txt_AFE.Text = dt.Rows[0]["AlertForExpiry"].ToString();
        }
    }
    private void Bind_grid(int VesselId)
    {
        DataTable dt;
        dt = Budget.getTable("select VesselCertId,(Select CertName from Certmaster where certid=vc.Certid) as CertificateName,certnumber as CertificateNumber,convert(varchar,issuedate,101) as IssueDate,convert(varchar,expirydate,101) as ExpiryDate from VesselCertificates vc Where vc.vesselid=" + VesselId).Tables[0] ;
        gv_VDoc.DataSource = dt;
        gv_VDoc.DataBind(); 
    }
    protected void gv_VDoc_SelectIndexChanged(object sender, EventArgs e)
    {
        HiddenField hfd;
        hfd = (HiddenField)gv_VDoc.Rows[gv_VDoc.SelectedIndex].FindControl("HiddenId");
        HiddenDocId.Value = hfd.Value;
        Show_Record(int.Parse(hfd.Value));  
        btn_Add.Visible = ((Mode == "Edit") || (Mode == "New")) ? true : false;
        btn_Save.Visible = false;
        btn_Cancel.Visible = true;
        
    }
    protected void gv_VDoc_Row_Editing(object sender, GridViewEditEventArgs e)
    {
        HiddenField hfd;
        hfd = (HiddenField)gv_VDoc.Rows[e.NewEditIndex].FindControl("HiddenId");
        HiddenDocId.Value = hfd.Value;
        Show_Record( int.Parse (hfd.Value));
        btn_Add.Visible = ((Mode == "Edit") || (Mode == "New")) ? true : false;
        btn_Save.Visible = true;
        btn_Cancel.Visible = true;
    }
    protected void gv_VDoc_Row_Deleting(object sender, GridViewDeleteEventArgs e)
    {
        lbl_message_documents.Visible = false;
        HiddenField hfd;
        hfd = (HiddenField)gv_VDoc.Rows[e.RowIndex].FindControl("HiddenId");
        Budget.getTable("DELETE FROM VESSELCERTIFICATES WHERE VESSELCERTID=" + hfd.Value);  
        Bind_grid(VesselId);
    }
    protected void gv_VDoc_PreRender(object sender, EventArgs e)
    {
        if (gv_VDoc.Rows.Count <= 0)
        {
            lbl_VesselDoc.Text = "No Records Found..!";
        }
    }
    protected void btn_Cancel_Click(object sender, EventArgs e)
    {
        pnl_Documents.Visible = false;
        btn_Save.Visible = false;
        btn_Add.Visible = ((Mode == "Edit") || (Mode == "New")) ? true : false;
        btn_Cancel.Visible = false;
        gv_VDoc.SelectedIndex = -1;
        lbl_message_documents.Visible = false;
    }
    protected void btn_Add_Click(object sender, EventArgs e)
    {
        HiddenDocId.Value = "";
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
  
        pnl_Documents.Visible = true;
        btn_Add.Visible = false;
        btn_Save.Visible = true;
        btn_Cancel.Visible = true;
        ViewState["certid"] = "0";

    }
    public int getCertId(string CertName)
    {
        DataTable dt=Budget.getTable("select certid from certmaster where ltrim(rtrim(certmaster.certname))='" + CertName + "'").Tables[0];
        if (dt.Rows.Count > 0)
        {
            return int.Parse(dt.Rows[0][0].ToString());
        }
        else { return 0; }
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        int certid=getCertId(txt_Cert.Text.Trim ());
        if (certid <= 0)
        {
            lbl_message_documents.Text = "Certificate not exists.";  
            return; 
        }
        string idt,nsi,ct, edt, ib, pi, nsd, afs, afe;

        idt= (txt_IssueDate.Text.Trim() == "") ? "NULL" : "'" + txt_IssueDate.Text.Trim() + "'";
        edt = (txt_ExpDate.Text.Trim() == "") ? "NULL" : "'" + txt_ExpDate.Text.Trim() + "'";
        ib = txt_IssueBy.Text.Trim();  
        pi=txt_PlaceIssued.Text.Trim();   
        ct=ddl_CertType.SelectedValue; 
        nsi=txt_NextSurInt.Text.Trim();  
        nsd = (txt_NSD.Text.Trim() == "") ? "NULL" : "'" + txt_NSD.Text.Trim() + "'";
        afs = txt_AFS.Text.Trim();
        afe = txt_AFE.Text.Trim();
        try
        {
            if (HiddenDocId.Value == "")
            {
                Budget.getTable("INSERT INTO VESSELCERTIFICATES (VESSELID,CERTID,CERTNUMBER,ISSUEDATE,EXPIRYDATE,ISSUEDBY,PLACEISSUED,CERTTYPE,NEXTSURVEYINTERVAL,NEXTSURVEYDUE,ALERTFORSURVEY,ALERTFOREXPIRY)" +
                "VALUES(" + VesselId + "," + certid.ToString() + ",'" + txt_CertNumber.Text.Trim() + "'," + idt + "," + edt + ",'" + ib + "','" + pi + "','" + ct + "','" + nsi + "'," + nsd + ",'" + afs + "','" + afe + "')");
            }
            else
            {
                Budget.getTable("UPDATE VESSELCERTIFICATES" +
                " SET VESSELID=" + VesselId +
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
                ",ALERTFOREXPIRY='" + afe + "' WHERE VESSELCERTIFICATES.VESSELCeRTID=" + int.Parse(HiddenDocId.Value));
            }
            lbl_message_documents.Text="Record saved successfully";  
        }
        catch
        {
            lbl_message_documents.Text="Unable to save the record.";  
        }
        Bind_grid(VesselId);
        btn_Add_Click(sender, e);
        lbl_message_documents.Visible = true;
    }
    protected void txt_NextSurInt_TextChanged(object sender, EventArgs e)
    {
        try
        {
            txt_NSD.Text = Convert.ToDateTime(txt_IssueDate.Text).AddMonths(int.Parse(txt_NextSurInt.Text)).ToString("MM/dd/yyyy");
        }
        catch { txt_NSD.Text = ""; }
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        return default(string[]);
    }
    ////protected void txt_Cert_TextChanged(object sender, EventArgs e)
    ////{
    ////    //if (txt_Cert.Text.Trim() != "")
    ////    //{ 
    ////    //    DataTable dt1=
    ////    //}
    ////}
}
