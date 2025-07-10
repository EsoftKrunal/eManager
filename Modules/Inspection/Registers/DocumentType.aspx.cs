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

public partial class Registers_DocumentType : System.Web.UI.Page
{
    int id;
    Authority Auth;
    string Mode = "New";
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        //--------------------------------- PAGE ACCESS AUTHORITY ----------------------------------------------------
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 157);
        if (chpageauth <= 0)
            Response.Redirect("blank.aspx");
        //------------------------------------------------------------------------------------------------------------
   
        if (Session["loginid"] == null)
        {
            //Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "window.parent.parent.location='../Default.aspx'", true);
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Default.aspx';", true);
        }
        lbl_GridView_DocumentType.Text = "";
        lbl_DocumentType_Message.Text = "";
        //int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 70);
        //if (chpageauth <= 0)
        //{
        //    Response.Redirect("Dummy.aspx");
        //}
        if (Session["loginid"] != null)
        {
            ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()),12);
            OBJ.Invoke();
            Session["Authority"] = OBJ.Authority;
            Auth = OBJ.Authority;
        }
        if (!Page.IsPostBack)
        {
            bindDocumentTypeGrid();
            //bindStatusDDL();
            //Alerts.HidePanel(pnl_Charterer);
            try
            {
                Alerts.HANDLE_AUTHORITY(1, btn_New_DocumentType, btn_Save_DocumentType, btn_Cancel_DocumentType, btn_Print_DocType, Auth);
            }
            catch
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Default.aspx';", true);
            }
        }
    }
    public void bindDocumentTypeGrid()
    {
        DataTable dt1 = Document_Type.DocumentTypeDetails(0, "", "", 0, 0, "Select");
        this.GridView_DocumentType.DataSource = dt1;
        this.GridView_DocumentType.DataBind();
        if (dt1.Rows.Count > 0)
            HiddenFieldGridRowCount.Value = dt1.Rows.Count.ToString();
        else
            HiddenFieldGridRowCount.Value = "0";
    }
    protected void btn_New_DocumentType_Click(object sender, EventArgs e)
    {
        txtDocumentType.Focus();
        txtDocumentType.Enabled = true;
        txtDescription.Enabled = true;
        btn_Save_DocumentType.Enabled = true;
        btn_Cancel_DocumentType.Visible = true;
        btn_New_DocumentType.Visible = false;
        txtDocumentType.Text = "";
        txtDescription.Text = "";
        txtCreatedBy_DocumentType.Text = "";
        txtCreatedOn_DocumentType.Text = "";
        txtModifiedBy_DocumentType.Text = "";
        txtModifiedOn_DocumentType.Text = "";
        HiddenDocumentType.Value = "";
        GridView_DocumentType.SelectedIndex = -1;
        //Alerts.ShowPanel(pnl_Charterer);
        try
        {
            Alerts.HANDLE_AUTHORITY(2, btn_New_DocumentType, btn_Save_DocumentType, btn_Cancel_DocumentType, btn_Print_DocType, Auth);
        }
        catch
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Default.aspx';", true);
        }
    }
    protected void btn_Save_DocumentType_Click(object sender, EventArgs e)
    {
        DataTable dt1;
        //int Duplicate = 0;

        //foreach (GridViewRow dg in GridView_InsGrp.Rows)
        //{
        //    HiddenField hfd;
        //    HiddenField hfd1;
        //    hfd = (HiddenField)dg.FindControl("Hidden_InspectionCode");
        //    hfd1 = (HiddenField)dg.FindControl("Hidden_InspectionGroupId");
        //    if (hfd.Value.ToString().ToUpper().Trim() == txtInspectionCode.Text.ToUpper().Trim())
        //    {
        //        if (HiddenInspectionGroup.Value.Trim() == "")
        //        {
        //            lbl_InspGrp_Message.Text = "Inspection Code Already Exists.";
        //            Duplicate = 1;
        //            break;
        //        }
        //        else if (HiddenInspectionGroup.Value.Trim() != hfd1.Value.ToString())
        //        {
        //            lbl_InspGrp_Message.Text = "Inspection Code Already Exists.";
        //            Duplicate = 1;
        //            break;
        //        }
        //    }
        //    else
        //    {
        //        lbl_InspGrp_Message.Text = "";
        //    }
        //}
        //if (Duplicate == 0)
        //{
        int intDocumentTypeId = -1;
        int intCreatedBy = 0;
        int intModifiedBy = 0;

        if (HiddenDocumentType.Value.ToString().Trim() == "")
        {
            intCreatedBy = Convert.ToInt32(Session["loginid"].ToString());
        }
        else
        {
            intDocumentTypeId = Convert.ToInt32(HiddenDocumentType.Value);
            intModifiedBy = Convert.ToInt32(Session["loginid"].ToString());
        }

        string strDocType = txtDocumentType.Text;
        string strDesc = txtDescription.Text;

        if (HiddenDocumentType.Value.ToString().Trim() == "")
        {
            dt1 = Document_Type.DocumentTypeDetails(intDocumentTypeId, strDocType, strDesc, intCreatedBy, intModifiedBy, "Add");
        }
        else
        {
            dt1 = Document_Type.DocumentTypeDetails(intDocumentTypeId, strDocType, strDesc, intCreatedBy, intModifiedBy, "Modify");
        }
        if (Document_Type.ErrMsg == "")
        {
            //if (dt1.Rows.Count > 0)
            //{
            //    if (dt1.Rows[0][0].ToString().Substring(0, 9) == "There was")
            //        lbl_DocumentType_Message.Text = "Record Not Saved.";
            //}
            //else
            //{
                lbl_DocumentType_Message.Text = "Record Successfully Saved.";
            //}
        }
        else { lbl_DocumentType_Message.Text = "Transaction Failed."; }        
        bindDocumentTypeGrid();        
        btn_New_DocumentType_Click(sender, e);
        btn_Cancel_DocumentType_Click(sender, e);
        //Alerts.HidePanel(pnl_Charterer);
        try
        {
            Alerts.HANDLE_AUTHORITY(3, btn_New_DocumentType, btn_Save_DocumentType, btn_Cancel_DocumentType, btn_Print_DocType, Auth);
        }
        catch
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Default.aspx';", true);
        }
        //}
    }
    protected void GridView_DocumentType_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            Alerts.HANDLE_GRID(GridView_DocumentType, Auth);
        }
        catch
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Default.aspx';", true);
        }
    }
    protected void Show_Record_DocType(int DocumentTypeId)
    {
        HiddenDocumentType.Value = DocumentTypeId.ToString();
        DataTable dt1 = Document_Type.DocumentTypeDetails(DocumentTypeId, "", "", 0, 0, "ById");
        foreach (DataRow dr in dt1.Rows)
        {
            txtDocumentType.Text = dr["Type"].ToString();
            txtDescription.Text = dr["Description"].ToString();
            txtCreatedBy_DocumentType.Text = dr["CreatedBy"].ToString();
            txtCreatedOn_DocumentType.Text = dr["CreatedOn"].ToString();
            txtModifiedBy_DocumentType.Text = dr["ModifiedBy"].ToString();
            txtModifiedOn_DocumentType.Text = dr["ModifiedOn"].ToString();
        }
    }
    protected void GridView_DocumentType_RowEditing(object sender, GridViewEditEventArgs e)
    {
        Mode = "Edit";
        HiddenField hfdDocType;
        hfdDocType = (HiddenField)GridView_DocumentType.Rows[e.NewEditIndex].FindControl("Hidden_DocumentTypeId");
        id = Convert.ToInt32(hfdDocType.Value.ToString());
        Show_Record_DocType(id);
        GridView_DocumentType.SelectedIndex = e.NewEditIndex;
        btn_New_DocumentType.Visible = false;
        btn_Cancel_DocumentType.Visible = true;
        txtDocumentType.Enabled = true;
        txtDescription.Enabled = true;
        btn_Save_DocumentType.Enabled = true;
        //Alerts.ShowPanel(pnl_Charterer);
        try
        {
            Alerts.HANDLE_AUTHORITY(5, btn_New_DocumentType, btn_Save_DocumentType, btn_Cancel_DocumentType, btn_Print_DocType, Auth);
        }
        catch
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Default.aspx';", true);
        }
    }
    protected void GridView_DocumentType_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DataTable dt1;
        int intModifiedBy = Convert.ToInt32(Session["loginid"].ToString());
        HiddenField hfdDocType;
        hfdDocType = (HiddenField)GridView_DocumentType.Rows[e.RowIndex].FindControl("Hidden_DocumentTypeId");
        id = Convert.ToInt32(hfdDocType.Value.ToString());
        dt1 = Document_Type.DocumentTypeDetails(id, "", "", 0, intModifiedBy, "Delete");
        if (Document_Type.ErrMsg == "")
        {
            //if (dt1.Rows.Count > 0)
            //{
            //    if (dt1.Rows[0][0].ToString().Substring(0, 9) == "There was")
            //        lbl_DocumentType_Message.Text = "Record Not Deleted.";
            //}
            //else
            //{
            lbl_DocumentType_Message.Text = "Record Successfully Deleted.";
            //}
        }
        else { lbl_DocumentType_Message.Text = "Record Not Deleted."; }
        bindDocumentTypeGrid();
        if (HiddenDocumentType.Value.ToString() == hfdDocType.Value.ToString())
        {
            btn_New_DocumentType_Click(sender, e);
        }
    }
    protected void GridView_DocumentType_PreRender(object sender, EventArgs e)
    {
        //if (GridView_DocumentType.Rows.Count <= 0) { lbl_GridView_DocumentType.Text = ""; } else { lbl_GridView_DocumentType.Text = "No. of Records Found: " + GridView_DocumentType.Rows.Count; }
        if (GridView_DocumentType.Rows.Count <= 0) { lbl_GridView_DocumentType.Text = ""; } else { lbl_GridView_DocumentType.Text = "No. of Records Found: " + HiddenFieldGridRowCount.Value; }
    }
    protected void btn_Cancel_DocumentType_Click(object sender, EventArgs e)
    {
        txtDocumentType.Enabled = false;
        txtDescription.Enabled = false;
        //btn_Save_DocumentType.Enabled = false;
        btn_Save_DocumentType.Visible = false;
        btn_Cancel_DocumentType.Visible = false;
        btn_New_DocumentType.Visible = true;
        btn_Save_DocumentType.Enabled = false;
        txtDocumentType.Text = "";
        txtDescription.Text = "";
        txtCreatedBy_DocumentType.Text = "";
        txtCreatedOn_DocumentType.Text = "";
        txtModifiedBy_DocumentType.Text = "";
        txtModifiedOn_DocumentType.Text = "";
        HiddenDocumentType.Value = "";
        GridView_DocumentType.SelectedIndex = -1;
    }
    protected void GridView_DocumentType_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView_DocumentType.PageIndex = e.NewPageIndex;
        GridView_DocumentType.SelectedIndex = -1;
        bindDocumentTypeGrid();
    }
}
