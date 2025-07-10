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


public partial class Registers_OtherDocuments : System.Web.UI.Page
{
    int id;
    Authority Auth;
    string Mode = "New";
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        this.lbl_OtherDocuments_Message.Text = "";
        this.lblOtherDocuments.Text = "";
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
            BindOffCrewDropDown();
            BindOffGroupDropDown();
            BindStatusDropDown();
            BindGridOtherDocuments();
            
            
            Alerts.HidePanel(OtherDocumentspanel);
            Alerts.HANDLE_AUTHORITY(1, this.btn_other_Document_add,  this.btn_other_Document_save, this.btn_other_Document_Cancel,btn_Print_OtherDocuments, Auth);     
        }
    }
   
    private void BindOffCrewDropDown()
    {
        DataTable dt1 = OtherDocuments.selectDataOffCrew();
        this.ddOffCrew_Document.DataValueField = "OffCrewId";
        this.ddOffCrew_Document.DataTextField = "OffCrewName";
        this.ddOffCrew_Document.DataSource = dt1;
        this.ddOffCrew_Document.DataBind();
    }
    private void BindOffGroupDropDown()
    {
        DataTable dt2 = OtherDocuments.selectDataOffGroup();
        this.ddOffGroup_Document.DataValueField = "OffGroupId";
        this.ddOffGroup_Document.DataTextField = "OffGroupName";
        this.ddOffGroup_Document.DataSource = dt2;
        this.ddOffGroup_Document.DataBind();
    }
    private void BindStatusDropDown()
    {
        DataTable dt3 = OtherDocuments.selectDataStatus();
        this.ddstatus_Document.DataValueField = "StatusId";
        this.ddstatus_Document.DataTextField = "StatusName";
        this.ddstatus_Document.DataSource = dt3;
        this.ddstatus_Document.DataBind();
    }
    private void BindGridOtherDocuments()
    {
        DataTable dt = OtherDocuments.selectDataOtherDocumentsDetails();
        this.GvOtherDocuments.DataSource = dt;
        this.GvOtherDocuments.DataBind();
    }
    protected void GvOtherDocuments_SelectIndexChanged(object sender, EventArgs e)
    {
        HiddenField hfddocuments;
        hfddocuments = (HiddenField)GvOtherDocuments.Rows[GvOtherDocuments.SelectedIndex].FindControl("HiddenOtherDocumentId");
        id = Convert.ToInt32(hfddocuments.Value.ToString());
        Show_Record_OtherDocuments(id);
       
      
        Alerts.ShowPanel(this.OtherDocumentspanel);
        Alerts.HANDLE_AUTHORITY(4,this.btn_other_Document_add,this.btn_other_Document_save,this.btn_other_Document_Cancel, this.btn_Print_OtherDocuments, Auth);     
    }
    protected void Show_Record_OtherDocuments(int otherdocumentsid)
    {
        char a, b;
        HiddenOtherDocumentspk.Value = otherdocumentsid.ToString();
        DataTable dt3 = OtherDocuments.selectDataOtherDocumentsDetailsByOtherDocumentsId(otherdocumentsid);
        foreach (DataRow dr in dt3.Rows)
        {
            txtDocumentType.Text = dr["documenttype"].ToString();
            txtDocumentName.Text = dr["documentname"].ToString();
            ddOffCrew_Document.SelectedValue = dr["offcrew"].ToString();
            ddOffGroup_Document.SelectedValue = dr["offgroup"].ToString();
            a = Convert.ToChar(dr["expires"].ToString());
            b = Convert.ToChar(dr["mandatory"].ToString());
            if (a == 'Y')
            {
                Chkexpires_Document.Checked = true;
            }
            else
            {
                Chkexpires_Document.Checked = false;
            }
            if (b == 'Y')
            {
                Chkmandatory_Document.Checked = true;
            }
            else
            {
                Chkmandatory_Document.Checked = false;
            }
            txtcreatedby_Document.Text = dr["CreatedBy"].ToString();
            txtcreatedon_Document.Text = dr["CreatedOn"].ToString();
            txtmodifiedby_Document.Text = dr["ModifiedBy"].ToString();
            txtmodifiedon_Document.Text = dr["ModifiedOn"].ToString();
            ddstatus_Document.SelectedValue = dr["StatusId"].ToString();
        }
       
    }
    protected void GvOtherDocuments_Row_Editing(object sender, GridViewEditEventArgs e)
    {
        
        HiddenField hfddocument;
        hfddocument = (HiddenField)GvOtherDocuments.Rows[e.NewEditIndex].FindControl("HiddenOtherDocumentId");
        id = Convert.ToInt32(hfddocument.Value.ToString());
        OtherDocumentspanel.Visible = true;
        Show_Record_OtherDocuments(id);
        GvOtherDocuments.SelectedIndex = e.NewEditIndex;
      
        Alerts.ShowPanel(OtherDocumentspanel);
        Alerts.HANDLE_AUTHORITY(5, btn_other_Document_add, btn_other_Document_save, btn_other_Document_Cancel, btn_Print_OtherDocuments, Auth);     
    }
    protected void GvOtherDocuments_Row_Deleting(object sender, GridViewDeleteEventArgs e)
    {
        int modifiedBy = Convert.ToInt32(Session["loginid"].ToString());
        HiddenField hfddel;
        hfddel = (HiddenField)GvOtherDocuments.Rows[e.RowIndex].FindControl("HiddenOtherDocumentId");
        id = Convert.ToInt32(hfddel.Value.ToString());
        OtherDocuments.deleteOtherDocumentsDetails("deleteOtherDocuments", id, modifiedBy);
        BindGridOtherDocuments();
        if (HiddenOtherDocumentspk.Value.Trim() == hfddel.Value.ToString())
        {
            btn_other_Document_add_Click(sender, e);
        }
    }
    protected void GvOtherDocuments_DataBound(object sender, EventArgs e)
    {
        Alerts.HANDLE_GRID(GvOtherDocuments, Auth); 
    }
    protected void GvOtherDocuments_PreRender(object sender, EventArgs e)
    {
        if (this.GvOtherDocuments.Rows.Count <= 0)
        {
            lblOtherDocuments.Text = "No Records Found..!";
        }
       
    }
    protected void btn_other_Document_add_Click(object sender, EventArgs e)
    {

        HiddenOtherDocumentspk.Value = "";
        txtDocumentName.Text = "";
        txtDocumentType.Text = "";
        ddOffCrew_Document.SelectedIndex = 0;
        ddOffGroup_Document.SelectedIndex = 0;
        txtcreatedby_Document.Text = "";
        txtcreatedon_Document.Text = "";
        txtmodifiedby_Document.Text = "";
        txtmodifiedon_Document.Text = "";
        ddstatus_Document.SelectedIndex = 0;
        GvOtherDocuments.SelectedIndex = -1;
        Chkexpires_Document.Checked = false;
        Chkmandatory_Document.Checked = false;

        Alerts.ShowPanel(this.OtherDocumentspanel);
        Alerts.HANDLE_AUTHORITY(2, this.btn_other_Document_add,this.btn_other_Document_save,this.btn_other_Document_Cancel, this.btn_Print_OtherDocuments, Auth);    
    }
    protected void btn_other_Document_save_Click(object sender, EventArgs e)
    {
        int Duplicate=0;
        
            foreach (GridViewRow dg in GvOtherDocuments.Rows)
            {
                HiddenField hfd;
                HiddenField hfd1;
                hfd = (HiddenField)dg.FindControl("HiddenOtherDocumentName");
                hfd1 = (HiddenField)dg.FindControl("HiddenOtherDocumentId");

                if (hfd.Value.ToString().ToUpper().Trim() == txtDocumentName.Text.ToUpper().Trim())
                {
                    if (HiddenOtherDocumentspk.Value.Trim() == "")
                    {
                        lbl_OtherDocuments_Message.Text = "Already Entered.";
                        Duplicate = 1;
                        break;
                    }
                    else if (HiddenOtherDocumentspk.Value.Trim() != hfd1.Value.ToString())
                    {
                        lbl_OtherDocuments_Message.Text = "Already Entered.";
                        Duplicate = 1;
                        break;
                    }
                }
                else
                {
                    lbl_OtherDocuments_Message.Text = "";
                }
            }
            if (Duplicate == 0)
            {
                char exp, man;
                int otherdocumentsId = -1;
                int createdby = 0, modifiedby = 0;
                string strdocumenttype = txtDocumentType.Text;
                string strdocumentName = txtDocumentName.Text;
                char offcrew = Convert.ToChar(ddOffCrew_Document.SelectedValue);
                char offgroup = Convert.ToChar(ddOffGroup_Document.SelectedValue);
                char status = Convert.ToChar(ddstatus_Document.SelectedValue);
                if (Chkexpires_Document.Checked == true)
                {
                    exp = 'Y';
                }
                else
                {
                    exp = 'N';
                }
                if (Chkmandatory_Document.Checked == true)
                {
                    man = 'Y';
                }
                else
                {
                    man = 'N';
                }
                if (HiddenOtherDocumentspk.Value.Trim() == "")
                {
                    createdby = Convert.ToInt32(Session["loginid"].ToString());
                }
                else
                {
                    otherdocumentsId = Convert.ToInt32(HiddenOtherDocumentspk.Value);
                    modifiedby = Convert.ToInt32(Session["loginid"].ToString());
                }
                OtherDocuments.insertUpdateOtherDocumentsDetails("InsertUpdateOtherDocumentsDetails",
                                                                       otherdocumentsId,
                                                                        strdocumenttype,
                                                                        strdocumentName,
                                                                        offcrew,
                                                                        offgroup,
                                                                        exp,
                                                                        man,
                                                                        createdby,
                                                                        modifiedby,
                                                                        status);
                BindGridOtherDocuments();
                lbl_OtherDocuments_Message.Text = "Record Successfully Saved.";

                
                Alerts.HidePanel(this.OtherDocumentspanel);
                Alerts.HANDLE_AUTHORITY(3, this.btn_other_Document_add,this.btn_other_Document_save,this.btn_other_Document_Cancel,this.btn_Print_OtherDocuments, Auth);    
            }
    }
    protected void btn_other_Document_Cancel_Click(object sender, EventArgs e)
    {
       
        GvOtherDocuments.SelectedIndex = -1;
     
        Alerts.HidePanel(this.OtherDocumentspanel);
        Alerts.HANDLE_AUTHORITY(6,this.btn_other_Document_add,this.btn_other_Document_save,this.btn_other_Document_Cancel,this.btn_Print_OtherDocuments, Auth);     
    }
    protected void btn_Print_OtherDocuments_Click(object sender, EventArgs e)
    {

    }

    protected void GvOtherDocuments_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Modify")
        {
            GridViewRow row = (GridViewRow)(((ImageButton)sender).NamingContainer);
            int Rowindx = row.RowIndex;
            Mode = "Edit";
            HiddenField hfddocument;
            hfddocument = (HiddenField)GvOtherDocuments.Rows[Rowindx].FindControl("hdnOtherDocumentId");
            id = Convert.ToInt32(hfddocument.Value.ToString());
            OtherDocumentspanel.Visible = true;
            Show_Record_OtherDocuments(id);
            GvOtherDocuments.SelectedIndex = Rowindx;

            Alerts.ShowPanel(OtherDocumentspanel);
            Alerts.HANDLE_AUTHORITY(5, btn_other_Document_add, btn_other_Document_save, btn_other_Document_Cancel, btn_Print_OtherDocuments, Auth);
        }
        }

    protected void btnEditOtherDocument_click(object sender, ImageClickEventArgs e)
    {
        GridViewRow row = (GridViewRow)(((ImageButton)sender).NamingContainer);
        int Rowindx = row.RowIndex;
        Mode = "Edit";
        HiddenField hfddocument;
        hfddocument = (HiddenField)GvOtherDocuments.Rows[Rowindx].FindControl("hdnOtherDocumentId");
        id = Convert.ToInt32(hfddocument.Value.ToString());
        OtherDocumentspanel.Visible = true;
        Show_Record_OtherDocuments(id);
        GvOtherDocuments.SelectedIndex = Rowindx;

        Alerts.ShowPanel(OtherDocumentspanel);
        Alerts.HANDLE_AUTHORITY(5, btn_other_Document_add, btn_other_Document_save, btn_other_Document_Cancel, btn_Print_OtherDocuments, Auth);
    }
}
