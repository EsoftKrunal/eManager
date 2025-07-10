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

public partial class Registers_Designation : System.Web.UI.Page
{
    int id;
    Authority Auth;
    string Mode = "New";
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        if (Session["loginid"] == null)
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "window.parent.parent.location='../Default.aspx'", true);
        }
        lbl_GridView_Designation.Text = "";
        lbl_Designation_Message.Text = "";
        if (Session["loginid"] != null)
        {
            ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 12);
            OBJ.Invoke();
            Session["Authority"] = OBJ.Authority;
            Auth = OBJ.Authority;
        }
        if (!Page.IsPostBack)
        {
            bindDesignationGrid();
            //bindStatusDDL();
            //Alerts.HidePanel(pnl_Charterer);
            //Alerts.HANDLE_AUTHORITY(1, btn_Add_Charterer, btn_Save_Charterer, btn_Cancel_Charterer, btn_Print_Charterer, Auth);
        }
    }
    public void bindDesignationGrid()
    {
        DataTable dt1 = Designation_Master.DesignationDetails(0, "", "", 0, 0, "Select");
        this.GridView_Designation.DataSource = dt1;
        this.GridView_Designation.DataBind();
    }
    protected void btn_New_Designation_Click(object sender, EventArgs e)
    {
        txtDesignation.Focus();
        txtDesignation.Enabled = true;
        txtDescription.Enabled = true;
        btn_Save_Designation.Enabled = true;
        btn_Cancel_Designation.Visible = true;
        btn_New_Designation.Visible = false;
        txtDesignation.Text = "";
        txtDescription.Text = "";
        txtCreatedBy_Designation.Text = "";
        txtCreatedOn_Designation.Text = "";
        txtModifiedBy_Designation.Text = "";
        txtModifiedOn_Designation.Text = "";
        HiddenDesignation.Value = "";
        GridView_Designation.SelectedIndex = -1;
        //Alerts.ShowPanel(pnl_Charterer);
        //Alerts.HANDLE_AUTHORITY(2, btn_Add_Charterer, btn_Save_Charterer, btn_Cancel_Charterer, btn_Print_Charterer, Auth);
    }
    protected void btn_Save_Designation_Click(object sender, EventArgs e)
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
            int intDesignationId = -1;
            int intCreatedBy = 0;
            int intModifiedBy = 0;

            if (HiddenDesignation.Value.ToString().Trim() == "")
            {
                intCreatedBy = Convert.ToInt32(Session["loginid"].ToString());
            }
            else
            {
                intDesignationId = Convert.ToInt32(HiddenDesignation.Value);
                intModifiedBy = Convert.ToInt32(Session["loginid"].ToString());
            }

            string strDesgName = txtDesignation.Text;
            string strDesc = txtDescription.Text;

            if (HiddenDesignation.Value.ToString().Trim() == "")
            {
                dt1 = Designation_Master.DesignationDetails(intDesignationId, strDesgName, strDesc, intCreatedBy, intModifiedBy, "Add");
            }
            else
            {
                dt1 = Designation_Master.DesignationDetails(intDesignationId, strDesgName, strDesc, intCreatedBy, intModifiedBy, "Modify");
            }
            if (Designation_Master.ErrMsg == "")
            {
                //if (dt1.Rows.Count > 0)
                //{
                //    if (dt1.Rows[0][0].ToString().Substring(0, 9) == "There was")
                //        lbl_Designation_Message.Text = "Record Not Saved.";
                //}
                //else
                //{
                    lbl_Designation_Message.Text = "Record Successfully Saved.";
                //}
            }
            else { lbl_Designation_Message.Text = "Transaction Failed."; }
            bindDesignationGrid();            
            btn_New_Designation_Click(sender, e);
            btn_Cancel_Designation_Click(sender, e);
            //Alerts.HidePanel(pnl_Charterer);
            //Alerts.HANDLE_AUTHORITY(3, btn_Add_Charterer, btn_Save_Charterer, btn_Cancel_Charterer, btn_Print_Charterer, Auth);
        //}
    }
    protected void GridView_Designation_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //Alerts.HANDLE_GRID(GridView_Charterer, Auth);
    }
    protected void Show_Record_Designation(int DesignationId)
    {
        HiddenDesignation.Value = DesignationId.ToString();
        DataTable dt1 = Designation_Master.DesignationDetails(DesignationId, "", "", 0, 0, "ById");
        foreach (DataRow dr in dt1.Rows)
        {
            txtDesignation.Text = dr["Designation"].ToString();
            txtDescription.Text = dr["Description"].ToString();
            txtCreatedBy_Designation.Text = dr["CreatedBy"].ToString();
            txtCreatedOn_Designation.Text = dr["CreatedOn"].ToString();
            txtModifiedBy_Designation.Text = dr["ModifiedBy"].ToString();
            txtModifiedOn_Designation.Text = dr["ModifiedOn"].ToString();
        }
    }
    protected void GridView_Designation_RowEditing(object sender, GridViewEditEventArgs e)
    {
        Mode = "Edit";
        HiddenField hfdDesignation;
        hfdDesignation = (HiddenField)GridView_Designation.Rows[e.NewEditIndex].FindControl("Hidden_DesignationId");
        id = Convert.ToInt32(hfdDesignation.Value.ToString());
        Show_Record_Designation(id);
        GridView_Designation.SelectedIndex = e.NewEditIndex;
        btn_New_Designation.Visible = false;
        btn_Cancel_Designation.Visible = true;
        txtDesignation.Enabled = true;
        txtDescription.Enabled = true;
        btn_Save_Designation.Enabled = true;
        //Alerts.ShowPanel(pnl_Charterer);
        //Alerts.HANDLE_AUTHORITY(5, btn_Add_Charterer, btn_Save_Charterer, btn_Cancel_Charterer, btn_Print_Charterer, Auth);
    }
    protected void GridView_Designation_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DataTable dt1;
        int intModifiedBy = Convert.ToInt32(Session["loginid"].ToString());
        HiddenField hfdDesignation;
        hfdDesignation = (HiddenField)GridView_Designation.Rows[e.RowIndex].FindControl("Hidden_DesignationId");
        id = Convert.ToInt32(hfdDesignation.Value.ToString());
        dt1 = Designation_Master.DesignationDetails(id, "", "", 0, intModifiedBy, "Delete");
        if (Designation_Master.ErrMsg == "")
        {
            //if (dt1.Rows.Count > 0)
            //{
            //    if (dt1.Rows[0][0].ToString().Substring(0, 9) == "There was")
            //        lbl_Designation_Message.Text = "Record Not Deleted.";
            //}
            //else
            //{
            lbl_Designation_Message.Text = "Record Successfully Deleted.";
            //}
        }
        else { lbl_Designation_Message.Text = "Transaction Failed."; }
        bindDesignationGrid();
        if (HiddenDesignation.Value.ToString() == hfdDesignation.Value.ToString())
        {
            btn_New_Designation_Click(sender, e);
        }
    }
    protected void GridView_Designation_PreRender(object sender, EventArgs e)
    {
        if (GridView_Designation.Rows.Count <= 0) { lbl_GridView_Designation.Text = ""; } else { lbl_GridView_Designation.Text = "No. of Records Found: " + GridView_Designation.Rows.Count; }
    }
    protected void btn_Cancel_Designation_Click(object sender, EventArgs e)
    {
        txtDesignation.Enabled = false;
        txtDescription.Enabled = false;
        btn_Save_Designation.Enabled = false;
        btn_Cancel_Designation.Visible = false;
        btn_New_Designation.Visible = true;
        btn_Save_Designation.Enabled = false;
        txtDesignation.Text = "";
        txtDescription.Text = "";
        txtCreatedBy_Designation.Text = "";
        txtCreatedOn_Designation.Text = "";
        txtModifiedBy_Designation.Text = "";
        txtModifiedOn_Designation.Text = "";
        HiddenDesignation.Value = "";
        GridView_Designation.SelectedIndex = -1;
    }
}
