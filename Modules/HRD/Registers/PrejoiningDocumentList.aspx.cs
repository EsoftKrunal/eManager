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

public partial class Modules_HRD_Registers_PrejoiningDocumentList : System.Web.UI.Page
{
    int id;
    Authority Auth;
    string Mode = "New";
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        lbl_PrejoingDocs_Message.Text = "";
        lblPrejoingDocs.Text = "";


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
            BindGridDocs();
            Alerts.HidePanel(PrejoinDocpanel);
            Alerts.HANDLE_AUTHORITY(1, btn_add, btn_save, btn_Cancel, btn_Print, Auth);

        }
    }
    private void BindStatusDropDown()
    {
        DataTable dt2 = PrejoingingDocs.selectDataStatus();
        this.ddstatus.DataValueField = "StatusId";
        this.ddstatus.DataTextField = "StatusName";
        this.ddstatus.DataSource = dt2;
        this.ddstatus.DataBind();
    }
    private void BindGridDocs()
    {
        DataTable dt = PrejoingingDocs.selectDataPrejoingDocsDetails();
        this.GvPrejoningDocs.DataSource = dt;
        this.GvPrejoningDocs.DataBind();
    }
    protected void btn_save_Click(object sender, EventArgs e)
    {
        int Duplicate = 0;

        foreach (GridViewRow dg in GvPrejoningDocs.Rows)
        {
            HiddenField hfd;
            HiddenField hfd1;
            hfd = (HiddenField)dg.FindControl("hdnDocName");
            hfd1 = (HiddenField)dg.FindControl("hdnDocumentId");

            if (hfd.Value.ToString().ToUpper().Trim() == txtPrejoingDocs.Text.ToUpper().Trim())
            {
                if (hdnPrejoingDocsId.Value.Trim() == "")
                {
                    lbl_PrejoingDocs_Message.Text = "Already Entered.";
                    Duplicate = 1;
                    break;
                }
                else if (hdnPrejoingDocsId.Value.Trim() != hfd1.Value.ToString())
                {
                    lbl_PrejoingDocs_Message.Text = "Already Entered.";
                    Duplicate = 1;
                    break;
                }
            }
            else
            {
                lbl_PrejoingDocs_Message.Text = "";
            }
        }
        if (Duplicate == 0)
        {
            int DocId = -1;
            int createdby = 0;

            string strDocName = txtPrejoingDocs.Text.Trim();
            
            char status = Convert.ToChar(ddstatus.SelectedValue);
            if (hdnPrejoingDocsId.Value.Trim() == "")
            {
                createdby = Convert.ToInt32(Session["loginid"].ToString());
            }
            else
            {
                DocId = Convert.ToInt32(hdnPrejoingDocsId.Value);
               // modifiedby = Convert.ToInt32(Session["loginid"].ToString());
            }
            PrejoingingDocs.insertUpdatePrejoingDocDetails("InsertUpdatePreJoiningDocuments",
                                                      DocId,
                                                      strDocName,
                                                      createdby,
                                                      status);
            BindGridDocs();
            lbl_PrejoingDocs_Message.Text = "Record Successfully Saved.";
            Alerts.HidePanel(PrejoinDocpanel);
            Alerts.HANDLE_AUTHORITY(3, btn_add, btn_save, btn_Cancel, btn_Print, Auth);

        }
    }

    protected void btn_Cancel_Click(object sender, EventArgs e)
    {
        GvPrejoningDocs.SelectedIndex = -1;
        Alerts.HidePanel(PrejoinDocpanel);
        Alerts.HANDLE_AUTHORITY(6, btn_add, btn_save, btn_Cancel, btn_Print, Auth);
    }

    protected void btn_add_Click(object sender, EventArgs e)
    {
        hdnPrejoingDocsId.Value = "";
        txtPrejoingDocs.Text = "";
        txtcreatedby.Text = "";
        GvPrejoningDocs.SelectedIndex = -1;
        txtcreatedon.Text = "";
        //txtmodifiedby_Department.Text = "";
        //txtmodifiedon_Department.Text = "";
        ddstatus.SelectedIndex = 0;

        Alerts.ShowPanel(PrejoinDocpanel);
        Alerts.HANDLE_AUTHORITY(2, btn_add, btn_save, btn_Cancel, btn_Print, Auth);
    }

    protected void btn_Print_Click(object sender, EventArgs e)
    {

    }

    protected void GvPrejoningDocs_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {

    }

    protected void GvPrejoningDocs_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Modify")
        {
            GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
            int Rowindx = row.RowIndex;
            Mode = "Edit";
            HiddenField hfdDocId;
            hfdDocId = (HiddenField)GvPrejoningDocs.Rows[Rowindx].FindControl("hdnDocumentId");
            id = Convert.ToInt32(hfdDocId.Value.ToString());

            Show_Record_PrejoinginDocs(id);
            GvPrejoningDocs.SelectedIndex = Rowindx;
            Alerts.ShowPanel(PrejoinDocpanel);
            Alerts.HANDLE_AUTHORITY(5, btn_add, btn_save, btn_Cancel, btn_Print, Auth);
        }
    }

    protected void GvPrejoningDocs_Row_Deleting(object sender, GridViewDeleteEventArgs e)
    {
        int intModifiedBy = Convert.ToInt32(Session["loginid"].ToString());
        HiddenField hfdDocId;
        hfdDocId = (HiddenField)GvPrejoningDocs.Rows[e.RowIndex].FindControl("HiddenManningAgentId");
        id = Convert.ToInt32(hfdDocId.Value.ToString());
        PrejoingingDocs.deletePrejoingDocDetails("deleteMinningAgent", id);
        BindGridDocs();
        if (hdnPrejoingDocsId.Value.Trim() == hfdDocId.Value.ToString())
        {
            btn_add_Click(sender, e);
        }
    }

    protected void GvPrejoningDocs_SelectIndexChanged(object sender, EventArgs e)
    {
        HiddenField hfdDocId;
        hfdDocId = (HiddenField)GvPrejoningDocs.Rows[GvPrejoningDocs.SelectedIndex].FindControl("hdnDocumentId");
        id = Convert.ToInt32(hfdDocId.Value.ToString());
        Show_Record_PrejoinginDocs(id);
        Alerts.ShowPanel(PrejoinDocpanel);
        Alerts.HANDLE_AUTHORITY(4, btn_add, btn_save, btn_Cancel, btn_Print, Auth);
    }
    protected void Show_Record_PrejoinginDocs(int docId)
    {

        hdnPrejoingDocsId.Value = docId.ToString();
        DataTable dt3 = PrejoingingDocs.SelectPreJoingDocsDetailsByDocId(docId);
        foreach (DataRow dr in dt3.Rows)
        {
            txtPrejoingDocs.Text = dr["DocumentName"].ToString();
            txtcreatedby.Text = dr["CreatedBy"].ToString();
            txtcreatedon.Text = dr["CreatedOn"].ToString();
            //txtmodifiedby_Department.Text = dr["ModifiedBy"].ToString();
            //txtmodifiedon_Department.Text = dr["ModifiedOn"].ToString();
            ddstatus.SelectedValue = dr["Status"].ToString().Trim();
          
        }

    }
    protected void GvPrejoningDocs_DataBound(object sender, EventArgs e)
    {
        Alerts.HANDLE_GRID(GvPrejoningDocs, Auth);
    }

    protected void GvPrejoningDocs_PreRender(object sender, EventArgs e)
    {
        if (this.GvPrejoningDocs.Rows.Count <= 0)
        {
            lblPrejoingDocs.Text = "No Records Found..!";
        }
    }

    protected void btnEditDocs_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow row = (GridViewRow)(((ImageButton)sender).NamingContainer);
        int Rowindx = row.RowIndex;
        Mode = "Edit";
        HiddenField hfdDocId;
        hfdDocId = (HiddenField)GvPrejoningDocs.Rows[Rowindx].FindControl("hdnDocumentId");
        id = Convert.ToInt32(hfdDocId.Value.ToString());

        Show_Record_PrejoinginDocs(id);
        GvPrejoningDocs.SelectedIndex = Rowindx;
        // Alerts.ShowPanel(Departmentpanel);
        // Alerts.HANDLE_AUTHORITY(5, btn_add, btn_save, btn_Cancel, btn_Print, Auth);

        PrejoinDocpanel.Visible = true;
        btn_Cancel.Visible = true;
        btn_save.Visible = true;
        btn_add.Visible = false;
    }
}