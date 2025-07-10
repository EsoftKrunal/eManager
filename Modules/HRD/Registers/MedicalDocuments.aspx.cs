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
using ShipSoft.CrewManager.BusinessObjects;
using ShipSoft.CrewManager.BusinessLogicLayer;
using ShipSoft.CrewManager.Operational;
using System.IO;

public partial class Registers_MedicalDocuments : System.Web.UI.Page
{
    int id;
    Authority Auth;
    string Mode = "New";
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        lblDepartment.Text = "";
        lbl_Department_Message.Text = "";
        Label2.Text = "";
       
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 10);
        if (chpageauth <= 0)
        {
            Response.Redirect("Dummy.aspx");

        }
       
        ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 10);
        OBJ.Invoke();
        Session["Authority"] = OBJ.Authority;
        Auth = OBJ.Authority;
        if (!Page.IsPostBack)
        {
            BindStatusDropDown();
            BindGridMedicalDocuments();
          
            Alerts.HidePanel(Departmentpanel);
            Alerts.HANDLE_AUTHORITY(1, btn_Document_add, btn_Document_save, btn_Document_Cancel, btn_Print_Document, Auth);
        }
    }
    private void BindStatusDropDown()
    {
        DataTable dt2 = Department.selectDataStatus();
        this.ddstatus_Document.DataValueField = "StatusId";
        this.ddstatus_Document.DataTextField = "StatusName";
        this.ddstatus_Document.DataSource = dt2;
        this.ddstatus_Document.DataBind();
    }
    private void BindGridMedicalDocuments()
    {
        DataTable dt = MedicalDocuments.selectDataMedicalDocumentsDetails() ;
        this.GvDepartment.DataSource = dt;
        this.GvDepartment.DataBind();
    }
    protected void GvDepartment_SelectIndexChanged(object sender, EventArgs e)
    {
        HiddenField hfdMedicalDocument;
        hfdMedicalDocument = (HiddenField)GvDepartment.Rows[GvDepartment.SelectedIndex].FindControl("HiddenMedicalDocumentId");
        id = Convert.ToInt32(hfdMedicalDocument.Value.ToString());
        Show_Record_Medical_Documents(id);
       
        Alerts.ShowPanel(Departmentpanel);
        Alerts.HANDLE_AUTHORITY(4, btn_Document_add, btn_Document_save, btn_Document_Cancel, btn_Print_Document, Auth);
    }
    protected void Show_Record_Medical_Documents(int documentid)
    {
        HiddenDocumentpk.Value = documentid.ToString();
        DataTable dt3 = MedicalDocuments.selectDataMedicalDocumentsDetailsById(documentid);
        foreach (DataRow dr in dt3.Rows)
        {
            txtDocumentname.Text = dr["MedicalDocumentName"].ToString();
            txtcreatedby_Document.Text = dr["CreatedBy"].ToString();
            txtcreatedon_Document.Text = dr["CreatedOn"].ToString();
            txtmodifiedby_Document.Text = dr["ModifiedBy"].ToString();
            txtmodifiedon_Document.Text = dr["ModifiedOn"].ToString();
            ddstatus_Document.SelectedValue = dr["StatusId"].ToString();
        }
    }
    protected void GvDepartment_Row_Editing(object sender, GridViewEditEventArgs e)
    {
        GvDepartment.SelectedIndex = e.NewEditIndex;
        HiddenField hfdMedicalDocument;
        hfdMedicalDocument = (HiddenField)GvDepartment.Rows[GvDepartment.SelectedIndex].FindControl("HiddenMedicalDocumentId");
        id = Convert.ToInt32(hfdMedicalDocument.Value.ToString());
        Show_Record_Medical_Documents(id);
       
        Alerts.ShowPanel(Departmentpanel);
        Alerts.HANDLE_AUTHORITY(5, btn_Document_add, btn_Document_save, btn_Document_Cancel, btn_Print_Document, Auth);
    }
    protected void GvDepartment_Row_Deleting(object sender, GridViewDeleteEventArgs e)
    {
        GvDepartment.SelectedIndex = e.RowIndex;
        int intModifiedBy = Convert.ToInt32(Session["loginid"].ToString());
        HiddenField hfdMedicalDocument;
        hfdMedicalDocument = (HiddenField)GvDepartment.Rows[GvDepartment.SelectedIndex].FindControl("HiddenMedicalDocumentId");
        id = Convert.ToInt32(hfdMedicalDocument.Value.ToString());
        MedicalDocuments.deleteMedicalDocumentsDetails("deleteMedicalDocumentsDetails", id, intModifiedBy);
        BindGridMedicalDocuments();
        if (HiddenDocumentpk.Value.Trim() == hfdMedicalDocument.Value.ToString())
        {
            btn_Department_add_Click(sender, e);
        }
    }

    protected void GvDepartment_DataBound(object sender, EventArgs e)
    {
        Alerts.HANDLE_GRID(GvDepartment, Auth);
    }
    protected void GvDepartment_PreRender(object sender, EventArgs e)
    {
        if (this.GvDepartment.Rows.Count <= 0)
        {
            lblDepartment.Text = "No Records Found..!";
        }
       
    }
    protected void btn_Department_add_Click(object sender, EventArgs e)
    {
        HiddenDocumentpk.Value = "";
        txtDocumentname.Text = "";
        txtcreatedby_Document.Text = "";
        GvDepartment.SelectedIndex = -1;
        txtcreatedon_Document.Text = "";
        txtmodifiedby_Document.Text = "";
        txtmodifiedon_Document.Text = "";
        ddstatus_Document.SelectedIndex = 0;
     
        Alerts.ShowPanel(Departmentpanel);
        Alerts.HANDLE_AUTHORITY(2, btn_Document_add, btn_Document_save, btn_Document_Cancel, btn_Print_Document, Auth);
    }
    protected void btn_Department_save_Click(object sender, EventArgs e)
    {
        int Duplicate=0;
        
            foreach (GridViewRow dg in GvDepartment.Rows)
            {
                Label hfd;
                HiddenField hfd1;
                hfd = (Label)dg.FindControl("lblMedicalDocumentName");
                hfd1 = (HiddenField)dg.FindControl("HiddenMedicalDocumentId");

                if (hfd.Text.ToString().ToUpper().Trim() == txtDocumentname.Text.ToUpper().Trim())
                {
                    if (HiddenDocumentpk.Value.Trim() == "")
                    {
                        Label2.Text = "Already Entered.";
                        Duplicate = 1;
                        break;
                    }
                    else if (HiddenDocumentpk.Value.Trim() != hfd1.Value.ToString())
                    {
                        Label2.Text = "Already Entered.";
                        Duplicate = 1;
                        break;
                    }
                }
                else
                {
                    Label2.Text = "";
                }
            }
            if (Duplicate == 0)
            {
                int DocumentId = -1;
                int createdby = 0, modifiedby = 0;
                string DocumentName = txtDocumentname.Text;

                char status = Convert.ToChar(ddstatus_Document.SelectedValue);
                if (HiddenDocumentpk.Value.Trim() == "")
                {
                    createdby = Convert.ToInt32(Session["loginid"].ToString());
                }
                else
                {
                    DocumentId = Convert.ToInt32(HiddenDocumentpk.Value);
                    modifiedby = Convert.ToInt32(Session["loginid"].ToString());
                }
                MedicalDocuments.insertUpdateMedicalDocumentsDetails("InsertUpdateMedicalDocumentDetails",
                                                          DocumentId,
                                                          DocumentName,
                                                          createdby,
                                                          modifiedby,
                                                          status);
                BindGridMedicalDocuments();
                lbl_Department_Message.Text = "Record Successfully Saved.";
             
                Alerts.HidePanel(Departmentpanel);
                Alerts.HANDLE_AUTHORITY(3, btn_Document_add, btn_Document_save, btn_Document_Cancel, btn_Print_Document, Auth);
            }
    }
    protected void btn_Department_Cancel_Click(object sender, EventArgs e)
    {
        GvDepartment.SelectedIndex = -1;
        
        Alerts.HidePanel(Departmentpanel);
        Alerts.HANDLE_AUTHORITY(6, btn_Document_add, btn_Document_save, btn_Document_Cancel, btn_Print_Document, Auth);
    }
    protected void btn_Print_Department_Click(object sender, EventArgs e)
    {

    }

    protected void GvDepartment_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Modify")
        {
            GridViewRow row = (GridViewRow)(((ImageButton)sender).NamingContainer);
            int Rowindx = row.RowIndex;
            Mode = "Edit";
            GvDepartment.SelectedIndex = Rowindx;
            HiddenField hfdMedicalDocument;
            hfdMedicalDocument = (HiddenField)GvDepartment.Rows[Rowindx].FindControl("hdnMedicalDocumentId");
            id = Convert.ToInt32(hfdMedicalDocument.Value.ToString());
            Show_Record_Medical_Documents(id);

            Alerts.ShowPanel(Departmentpanel);
            Alerts.HANDLE_AUTHORITY(5, btn_Document_add, btn_Document_save, btn_Document_Cancel, btn_Print_Document, Auth);
        }
    }

    protected void btnEditMedicalDocument_click(object sender, EventArgs e)
    {
        GridViewRow row = (GridViewRow)(((ImageButton)sender).NamingContainer);
        int Rowindx = row.RowIndex;
        Mode = "Edit";
        GvDepartment.SelectedIndex = Rowindx;
        HiddenField hfdMedicalDocument;
        hfdMedicalDocument = (HiddenField)GvDepartment.Rows[Rowindx].FindControl("hdnMedicalDocumentId");
        id = Convert.ToInt32(hfdMedicalDocument.Value.ToString());
        Show_Record_Medical_Documents(id);

        Alerts.ShowPanel(Departmentpanel);
        Alerts.HANDLE_AUTHORITY(5, btn_Document_add, btn_Document_save, btn_Document_Cancel, btn_Print_Document, Auth);
    }
}
