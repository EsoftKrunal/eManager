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

public partial class UserUploadedDocuments_VesselCertificate : System.Web.UI.Page
{
    Authority Auth;
    string Mode;
    public void BindFleet()
    {
        try
        {
            DataTable dtFleet = Budget.getTable("SELECT FleetId,FleetName as Name FROM dbo.FleetMaster").Tables[0];
            this.ddl_Fleet.DataSource = dtFleet;
            this.ddl_Fleet.DataValueField = "FleetId";
            this.ddl_Fleet.DataTextField = "Name";
            this.ddl_Fleet.DataBind();
            ddl_Fleet.Items.Insert(0, "< ALL Fleet >");
            ddl_Fleet.Items[0].Value = "0";
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public void BindVessel()
    {
        try
        {
            //DataSet dsVessel = Inspection_Master.getMasterDataforInspection("Vessel", "VesselId", "VesselName as Name" ,Convert.ToInt32(Session["loginid"].ToString()));
            DataSet dsVessel;
            if (ddl_Fleet.SelectedIndex <= 0)
            {
                //dsVessel = Budget.getTable("SELECT VesselId,VesselName as Name FROM dbo.Vessel WHERE VesselStatusId <> 2  order by VesselName");
                 dsVessel = SearchSignOff.getVessel(Convert.ToInt32(Session["loginid"].ToString()));
            }
            else
            {
                dsVessel = Budget.getTable("SELECT VesselId,VesselName as Name FROM dbo.Vessel WHERE FLEETID =" + ddl_Fleet.SelectedValue + " AND VesselStatusId <> 2 and  Vesselid in (Select VesselId from UserVesselRelation with(nolock) where LoginId = "+ Convert.ToInt32(Session["loginid"].ToString()) + ") order by VesselName");
            }

            ddlVessel.DataSource = dsVessel.Tables[0];
            ddlVessel.DataTextField = "Name";
            ddlVessel.DataValueField = "VesselId";
            ddlVessel.DataBind();
            //ddlVessel.Items.Insert(0,new ListItem("< Select >","0"));

            //this.rptvessel.DataSource = dsVessel.Tables[0];
            //this.rptvessel.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void BindCertificateCategory()
    {
        DataSet dsCertCategory;
        dsCertCategory = Budget.getTable("Select 0 As CertCatId, 'All' As CertCatName UNION Select CertCatId,CertCatName from CertCategory with(nolock) where StatusId = 'A'");
        if (dsCertCategory.Tables[0].Rows.Count > 0)
        {
            ddlCertCategory.DataSource = dsCertCategory.Tables[0];
            ddlCertCategory.DataTextField = "CertCatName";
            ddlCertCategory.DataValueField = "CertCatId";
            ddlCertCategory.DataBind();
        }
      
    }
    private void Bind_grid(string ExpOnly)
    {
        DataTable dt;
        string WhereClause = "";
        string sql = "";
        if (chkArchived.Checked)
        {
            sql = "select VesselCertId,vc.vesselid,(select vesselcode from dbo.vessel v where v.vesselid=vc.vesselid) as vesselcode,(Select CertCode from Dbo.Certmaster where certid=vc.Certid) as CertificateCode,(case when CertType='P' then 'Provisional' when CertType='I' then 'Interim' when CertType='F' then 'FullTerm' when CertType='C' then 'Conditional' when CertType='A' then 'Permanant' else '' end) as CertType,PlaceIssued,(Select CertName from dbo.Certmaster where certid=vc.Certid) as CertificateName,certnumber as CertificateNumber,replace(convert(varchar,issuedate,106),' ','-') as IssueDate,replace(convert(varchar,expirydate,106),' ','-') as ExpiryDate,Filename,CertType=case when CertType='P' then 'Provisional' when CertType='I' then 'Interim' when CertType='F' then 'Fullterm' when CertType='C' then 'Conditional' when CertType='P' then 'Permanent' else '' end,NextSurveyInterval as NSI,replace(convert(varchar,NextSurveyDue,106),' ','-') as NSD,BackColor=case when expirydate<=getdate() then '#FFCCCC' when dateadd(day,-cast(alertforexpiry as int),expirydate) <=getdate() then '#FFCC66' else 'Transparent' end, 1 As Archived from dbo.VesselCertificates_Archive vc Where 1=1 ";
            WhereClause = " And vc.vesselid='" + ddlVessel.SelectedValue + "'";
        }
        else
        {
            sql = "select VesselCertId,vc.vesselid,(select vesselcode from dbo.vessel v where v.vesselid=vc.vesselid) as vesselcode,(Select CertCode from Dbo.Certmaster where certid=vc.Certid) as CertificateCode,(case when CertType='P' then 'Provisional' when CertType='I' then 'Interim' when CertType='F' then 'FullTerm' when CertType='C' then 'Conditional' when CertType='A' then 'Permanant' else '' end) as CertType,PlaceIssued,(Select CertName from dbo.Certmaster where certid=vc.Certid) as CertificateName,certnumber as CertificateNumber,replace(convert(varchar,issuedate,106),' ','-') as IssueDate,replace(convert(varchar,expirydate,106),' ','-') as ExpiryDate,Filename,CertType=case when CertType='P' then 'Provisional' when CertType='I' then 'Interim' when CertType='F' then 'Fullterm' when CertType='C' then 'Conditional' when CertType='P' then 'Permanent' else '' end,NextSurveyInterval as NSI,replace(convert(varchar,NextSurveyDue,106),' ','-') as NSD,BackColor=case when expirydate<=getdate() then '#FFCCCC' when dateadd(day,-cast(alertforexpiry as int),expirydate) <=getdate() then '#FFCC66' else 'Transparent' end, 0 As Archived from dbo.VesselCertificates vc Where 1=1 ";
            WhereClause = " And vc.vesselid='" + ddlVessel.SelectedValue + "'";
        }

        if (ddlCType.SelectedIndex != 0)
            WhereClause =WhereClause + " And certtype='" + ddlCType .SelectedValue+"'";

        if (ddlCertCategory.SelectedIndex != 0)
            WhereClause = WhereClause + " And CertId in (Select CertId from CertMaster with(nolock) where CertCatId = " + Convert.ToInt32(ddlCertCategory.SelectedValue) + ")";

        //if (ExpOnly == "Y")
        //{
        //    WhereClause = WhereClause + " and ( expirydate<(dateadd(day,30,getdate()))  OR NextSurveyDue<(dateadd(day,30,getdate()))) ";
        //}

        sql = sql + WhereClause;
        sql += " order by vesselcode,vc.CertType ";
        dt = Budget.getTable(sql).Tables[0];

        rptVesselCertificate.DataSource = dt;
        rptVesselCertificate.DataBind();

        if (dt.Rows.Count > 0)
        {
            lblRecordCount.Text = " Record Count : " + dt.Rows.Count.ToString();
        }
        else
        {
            lblRecordCount.Text = " Record Count : (0) ";
        }
    }
    public string GetPath(string _path)
    {
        string res = "";
        string appname = ConfigurationManager.AppSettings["AppName"].ToString();
        if (_path.StartsWith("U"))
        {
            res = "../" + _path;
        }
        else
        {
            res = "/"+ appname + "/EMANAGERBLOB/LPSQE/Vessel_Certificates/" + _path + ".PDF";
        }
        if (!(File.Exists(Server.MapPath(res)))) { res = ""; }
        return res;
    }
    public string FileExists(string _path)
    {
        string res = "";
        string appname = ConfigurationManager.AppSettings["AppName"].ToString();
        if (_path.StartsWith("U"))
        {
            res = "../" + _path;
        }
        else
        {
            res = "/"+ appname + "/EMANAGERBLOB/LPSQE/Vessel_Certificates/" + _path + ".PDF";
        }
        if (!(File.Exists(Server.MapPath(res)))) { return "none"; }
        else { return "block"; }

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        if (!(IsPostBack))
        {
            BindFleet();
            BindVessel();
            BindCertificateCategory();
        }

        if (Session["loginid"] == null)
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Login.aspx';", true);
        }
        if (Session["loginid"] != null)
        {
            ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 6);
            OBJ.Invoke();
            Session["Authority"] = OBJ.Authority;
            Auth = OBJ.Authority;
            btnCert.Visible = Auth.isAdd;
            //btnUpload.Visible = Auth.isAdd;  
        }
        try
        {
            if (Session["VMode"] != null)
            {
                Mode = Session["VMode"].ToString();
            } 
            else
            {
                Mode = "New";
            }
        }
        catch { Mode = "New"; }
        btn_Print.Visible = Auth.isPrint;
        try
        {
            Bind_grid("Y");
        }
        catch { }
    }
    protected void gv_VDoc_SelectIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //HiddenField hfd = (HiddenField)gv_VDoc.Rows[gv_VDoc.SelectedIndex].FindControl("HiddenId");
            ImageButton img = (ImageButton)sender;
            HiddenField hfd = (HiddenField)img.FindControl("HiddenId");
            HiddenField hfdArchive = (HiddenField)img.FindControl("hfdArchive");


            ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "OpenPopup(" + ddlVessel.SelectedValue + "," + hfd.Value + ",'View',"+hfdArchive.Value+");", true);
        }
        catch { }
    }
    protected void gv_VDoc_Row_Editing(object sender, EventArgs e)
    {
        try
        {
            //HiddenField hfd = (HiddenField)gv_VDoc.Rows[e.NewEditIndex].FindControl("HiddenId");
            //HiddenField hfVID = (HiddenField)gv_VDoc.Rows[e.NewEditIndex].FindControl("hfVesselID");

            ImageButton img = (ImageButton)sender;
            HiddenField hfd = (HiddenField)img.FindControl("HiddenId");
            HiddenField hfVID = (HiddenField)img.FindControl("hfVesselID");
            HiddenField hfdArchive = (HiddenField)img.FindControl("hfdArchive");

            ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "OpenPopup(" + hfVID.Value + "," + hfd.Value + ",'Edit',"+hfdArchive.Value+");", true);
        }
        catch { }

        //try
        //{
        //    //HiddenField hfd;
        //    //hfd = (HiddenField)gv_VDoc.Rows[e.NewEditIndex].FindControl("HiddenId");
        //    //HiddenDocId.Value = hfd.Value;
        //    //Show_Record(int.Parse(hfd.Value));
        //    //btn_Add.Visible = ((Mode == "Edit") || (Mode == "New")) ? true : false;
        //    //btn_Save.Visible = true;
        //    //btn_Cancel.Visible = true;
        //    ////ddl_Vessel_SelectedIndexChanged(sender, e);
        //}
        //catch { }
    }
    protected void gv_VDoc_Row_Deleting(object sender, EventArgs e)
    {
        try
        {
            //lbl_message_documents.Visible = false;
            ImageButton img = (ImageButton)sender;
            HiddenField hfd = (HiddenField)img.FindControl("HiddenId");

            Budget.getTable("DELETE FROM DBO.VESSELCERTIFICATES WHERE VESSELCERTID=" + hfd.Value);
            Bind_grid("N");
        }
        catch { }
    }
    protected void AddCert(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "OpenPopup2(" + ddlVessel.SelectedValue + ",0,'Edit',0);", true);
    }
    protected void PrintCerts(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "abc", "OpenPrint(" + ddlVessel.SelectedValue + ",'" + ddlVessel.SelectedItem.Text + "');", true);   
    }
    protected void ddl_Fleet_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindVessel();        
    }
    // -------------- 
    protected void btnShow_OnClick(object sender, EventArgs e)
    {
        Bind_grid("N");
    }
    protected void ddlCT_SelectedIndexChanged(object sender, EventArgs e)
    {
        Bind_grid("N");
    }
    public string getColor(string ExpDate)
    {
        string retcolor = "none";
        try
        {
            DateTime dt = Convert.ToDateTime(ExpDate);
            DateTime dttoday = Convert.ToDateTime(DateTime.Today.ToString("dd-MMM-yyyy"));
            if (dt <= dttoday)
            {
                retcolor = "ared";
            }
            else if (dt <= dttoday.AddDays(30))
            {
                retcolor = "ayellow";
            }
            else
            {
                retcolor = "agreen";
            }
        }
        catch
        { }
        return retcolor;
    }

    public string getArchivedColor(int IsArchived)
    {
        string retcolor = "none";
        try
        {
            if (IsArchived == 1)
            {
                retcolor = "ared";
            }
            else
            {
                retcolor = "none";
            }
        }
        catch
        { }
        return retcolor;
    }

    protected void ddlCertCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Bind_grid("N");
        }
        catch(Exception ex)
        {

        }
    }

    protected void ArchiveCertificate(object sender, EventArgs e)
    {
        try
        {
            //lbl_message_documents.Visible = false;
            ImageButton img = (ImageButton)sender;
            HiddenField hfd = (HiddenField)img.FindControl("HiddenId");

            Budget.getTable("EXEC  InsertVesselCertificates_Archive " + hfd.Value);
            Bind_grid("N");
        }
        catch { }
    }

    protected void chkArchive_CheckedChanged(object sender, EventArgs e)
    {
        if (chkArchived.Checked)
        {
            btn_Print.Visible = false;
        }
        else
        {
            btn_Print.Visible = true;
        }
    }
}

