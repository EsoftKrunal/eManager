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

public partial class Registers_InspectionGroup : System.Web.UI.Page
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
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 152);
        if (chpageauth <= 0)
            Response.Redirect("blank.aspx");
        //------------------------------------------------------------------------------------------------------------
   
        if (Session["loginid"] == null)
        {
            //Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "window.parent.parent.location='../Default.aspx'", true);
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Default.aspx';", true);
        }
        lbl_GridView_InspGrp.Text = "";
        lbl_InspGrp_Message.Text = "";
        //int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 70);
        //if (chpageauth <= 0)
        //{
        //    Response.Redirect("Dummy.aspx");
        //}
        if (Session["loginid"] != null)
        {
            ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 12);
            OBJ.Invoke();
            Session["Authority"] = OBJ.Authority;
            Auth = OBJ.Authority;
        }
        if (!Page.IsPostBack)
        {
            bindInspectionGroupGrid();
            //Alerts.HidePanel(pnl_Charterer);
            try
            {
                Alerts.HANDLE_AUTHORITY(1, btn_New_InspGrp, btn_Save_InspGrp, btn_Cancel_InspGrp, btn_Print_InspGrp, Auth);
            }
            catch
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Default.aspx';", true);
            }
        }
    }
    public void bindInspectionGroupGrid()
    {
        DataTable dt1 = Inspection_Group.InspectionGroupDetails(0, "", "", "", 0, 0, "Select");
        if (dt1.Rows.Count > 0)
        {
            this.GridView_InsGrp.DataSource = dt1;
            this.GridView_InsGrp.DataBind();
            HiddenFieldGridRowCount.Value = dt1.Rows.Count.ToString();
        }
        else
        {
            HiddenFieldGridRowCount.Value = "0";
        }
    }
    protected void btn_New_InspGrp_Click(object sender, EventArgs e)
    {
        txtInspectionCode.Focus();
        txtInspectionCode.Enabled = true;
        txtInspectionGrpName.Enabled = true;
        rdInspGrpExternal.Enabled = true;
        rdInspGrpInternal.Enabled = true;
        btn_Save_InspGrp.Enabled = true;
        btn_Cancel_InspGrp.Visible = true;
        btn_New_InspGrp.Visible = false;
        txtInspectionCode.Text = "";
        txtInspectionGrpName.Text = "";
        rdInspGrpExternal.Checked=false;
        rdInspGrpInternal.Checked = true;
        txtCreatedBy_InspGrp.Text = "";
        txtCreatedOn_InspGrp.Text = "";
        txtModifiedBy_InspGrp.Text = "";
        txtModifiedOn_InspGrp.Text = "";
        HiddenInspectionGroup.Value = "";
        GridView_InsGrp.SelectedIndex = -1;
        //Alerts.ShowPanel(pnl_Charterer);
        try
        {
            Alerts.HANDLE_AUTHORITY(2, btn_New_InspGrp, btn_Save_InspGrp, btn_Cancel_InspGrp, btn_Print_InspGrp, Auth);
        }
        catch
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Default.aspx';", true);
        }
    }
    protected void btn_Save_InspGrp_Click(object sender, EventArgs e)
    {
        DataTable dt1;
        int Duplicate = 0;

        foreach (GridViewRow dg in GridView_InsGrp.Rows)
        {
            HiddenField hfd;
            HiddenField hfd1;
            hfd = (HiddenField)dg.FindControl("Hidden_InspectionCode");
            hfd1 = (HiddenField)dg.FindControl("Hidden_InspectionGroupId");
            if (hfd.Value.ToString().ToUpper().Trim() == txtInspectionCode.Text.ToUpper().Trim())
            {
                if (HiddenInspectionGroup.Value.Trim() == "")
                {
                    lbl_InspGrp_Message.Text = "Inspection Code Already Exists.";
                    Duplicate = 1;
                    break;
                }
                else if (HiddenInspectionGroup.Value.Trim() != hfd1.Value.ToString())
                {
                    lbl_InspGrp_Message.Text = "Inspection Code Already Exists.";
                    Duplicate = 1;
                    break;
                }
            }
            else
            {
                lbl_InspGrp_Message.Text = "";
            }
        }
        if (Duplicate == 0)
        {
            int intInspectionGroupId = -1;
            int intCreatedBy = 0;
            int intModifiedBy = 0;

            if (HiddenInspectionGroup.Value.ToString().Trim() == "")
            {
                intCreatedBy = Convert.ToInt32(Session["loginid"].ToString());
            }
            else
            {
                intInspectionGroupId = Convert.ToInt32(HiddenInspectionGroup.Value);
                intModifiedBy = Convert.ToInt32(Session["loginid"].ToString());
            }

            string strInspGrpCode = txtInspectionCode.Text;
            string strInspGrpName = txtInspectionGrpName.Text;
            string strInspType;
            if (rdInspGrpExternal.Checked)
                strInspType = "External";
            else
                strInspType = "Internal";

            if (HiddenInspectionGroup.Value.ToString().Trim() == "")
            {
                dt1 = Inspection_Group.InspectionGroupDetails(intInspectionGroupId, strInspGrpCode, strInspGrpName, strInspType, intCreatedBy, intModifiedBy, "Add");
            }
            else
            {
                dt1 = Inspection_Group.InspectionGroupDetails(intInspectionGroupId, strInspGrpCode, strInspGrpName, strInspType, intCreatedBy, intModifiedBy, "Modify");
            }
            if (Inspection_Group.ErrMsg == "")
            {
                //if (dt1.Rows.Count > 0)
                //{
                //    if (dt1.Rows[0][0].ToString().Substring(0, 9) == "There was")
                //        lbl_InspGrp_Message.Text = "Record Not Saved.";
                //}
                //else
                //{
                    lbl_InspGrp_Message.Text = "Record Successfully Saved.";
                //}
            }
            else { lbl_InspGrp_Message.Text = "Transaction Failed."; }
            bindInspectionGroupGrid();            
            btn_New_InspGrp_Click(sender, e);
            btn_Cancel_InspGrp_Click(sender, e);
            //Alerts.HidePanel(pnl_Charterer);
            try
            {
                Alerts.HANDLE_AUTHORITY(3, btn_New_InspGrp, btn_Save_InspGrp, btn_Cancel_InspGrp, btn_Print_InspGrp, Auth);
            }
            catch
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Login.aspx';", true);
            }
        }    
    }
    protected void GridView_InsGrp_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            Alerts.HANDLE_GRID(GridView_InsGrp, Auth);
        }
        catch
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Login.aspx';", true);
        }
    }
    protected void Show_Record_InspectionGroup(int InspectionGroupId)
    {
        HiddenInspectionGroup.Value = InspectionGroupId.ToString();
        DataTable dt1 = Inspection_Group.InspectionGroupDetails(InspectionGroupId,"","","",0,0,"ById");
        foreach (DataRow dr in dt1.Rows)
        {
            txtInspectionCode.Text = dr["Code"].ToString();
            txtInspectionGrpName.Text = dr["Name"].ToString();
            if (dr["InspectionType"].ToString() == "External")
            {
                rdInspGrpExternal.Checked = true;
                rdInspGrpInternal.Checked = false;
            }
            else
            {
                rdInspGrpInternal.Checked = true;
                rdInspGrpExternal.Checked = false;
            }
            txtCreatedBy_InspGrp.Text = dr["CreatedBy"].ToString();
            txtCreatedOn_InspGrp.Text = dr["CreatedOn"].ToString();
            txtModifiedBy_InspGrp.Text = dr["ModifiedBy"].ToString();
            txtModifiedOn_InspGrp.Text = dr["ModifiedOn"].ToString();
        }
    }
    protected void GridView_InsGrp_RowEditing(object sender, GridViewEditEventArgs e)
    {
        Mode = "Edit";
        HiddenField hfdInspectionGroup;
        hfdInspectionGroup = (HiddenField)GridView_InsGrp.Rows[e.NewEditIndex].FindControl("Hidden_InspectionGroupId");
        id = Convert.ToInt32(hfdInspectionGroup.Value.ToString());
        Show_Record_InspectionGroup(id);
        GridView_InsGrp.SelectedIndex = e.NewEditIndex;
        btn_New_InspGrp.Visible = false;
        btn_Cancel_InspGrp.Visible = true;
        txtInspectionCode.Enabled = true;
        txtInspectionGrpName.Enabled = true;
        rdInspGrpExternal.Enabled = true;
        rdInspGrpInternal.Enabled = true;
        btn_Save_InspGrp.Enabled = true;
        //Alerts.ShowPanel(pnl_Charterer);
        try
        {
            Alerts.HANDLE_AUTHORITY(5, btn_New_InspGrp, btn_Save_InspGrp, btn_Cancel_InspGrp, btn_Print_InspGrp, Auth);
        }
        catch
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Default.aspx';", true);
        }
    }
    protected void GridView_InsGrp_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DataTable dt1;
        int intModifiedBy = Convert.ToInt32(Session["loginid"].ToString());
        HiddenField hfdInspectionGroup;
        hfdInspectionGroup = (HiddenField)GridView_InsGrp.Rows[e.RowIndex].FindControl("Hidden_InspectionGroupId");
        id = Convert.ToInt32(hfdInspectionGroup.Value.ToString());
        dt1 = Inspection_Group.InspectionGroupDetails(id, "", "", "", 0, intModifiedBy, "Delete");
        if (Inspection_Group.ErrMsg == "")
        {
            //if (dt1.Rows.Count > 0)
            //{
            //    if (dt1.Rows[0][0].ToString().Substring(0, 9) == "There was")
            //        lbl_InspGrp_Message.Text = " The Inspection group cannot be deleted! It is in use.";
            //}
            //else
            //{
            lbl_InspGrp_Message.Text = "Record Successfully Deleted.";
            //}
        }
        else { lbl_InspGrp_Message.Text = "The Inspection group cannot be deleted! It is in use."; }
        bindInspectionGroupGrid();
        if (HiddenInspectionGroup.Value.ToString() == hfdInspectionGroup.Value.ToString())
        {
            btn_New_InspGrp_Click(sender, e);
        }
    }
    protected void GridView_InsGrp_PreRender(object sender, EventArgs e)
    {
        //if (GridView_InsGrp.Rows.Count <= 0) { lbl_GridView_InspGrp.Text = ""; } else { lbl_GridView_InspGrp.Text = "No. of Records Found: " + GridView_InsGrp.Rows.Count; }
        if (GridView_InsGrp.Rows.Count <= 0) { lbl_GridView_InspGrp.Text = ""; } else { lbl_GridView_InspGrp.Text = "No. of Records Found: " + HiddenFieldGridRowCount.Value; }
    }
    protected void btn_Cancel_InspGrp_Click(object sender, EventArgs e)
    {
        txtInspectionCode.Enabled = false;
        txtInspectionGrpName.Enabled = false;
        rdInspGrpExternal.Enabled = false;
        rdInspGrpInternal.Enabled = false;
        //btn_Save_InspGrp.Enabled = false;
        btn_Save_InspGrp.Visible = false;
        btn_Cancel_InspGrp.Visible = false;
        btn_New_InspGrp.Visible = true;
        btn_Save_InspGrp.Enabled = false;
        txtInspectionCode.Text = "";
        txtInspectionGrpName.Text = "";
        rdInspGrpExternal.Checked = false;
        rdInspGrpInternal.Checked = true;
        txtCreatedBy_InspGrp.Text = "";
        txtCreatedOn_InspGrp.Text = "";
        txtModifiedBy_InspGrp.Text = "";
        txtModifiedOn_InspGrp.Text = "";
        HiddenInspectionGroup.Value = "";
        GridView_InsGrp.SelectedIndex = -1;
    }
    protected void GridView_InsGrp_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView_InsGrp.PageIndex = e.NewPageIndex;
        GridView_InsGrp.SelectedIndex = -1;
        bindInspectionGroupGrid();
    }

    protected void GridView_InsGrp_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {

    }

    protected void GridView_InsGrp_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Modify")
        {
            GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
            int Rowindx = row.RowIndex;
            Mode = "Edit";
            HiddenField hfdInspectionGroup;
            hfdInspectionGroup = (HiddenField)GridView_InsGrp.Rows[Rowindx].FindControl("Hidden_InspectionGroupId");
            id = Convert.ToInt32(hfdInspectionGroup.Value.ToString());
            Show_Record_InspectionGroup(id);
            GridView_InsGrp.SelectedIndex = Rowindx;
            btn_New_InspGrp.Visible = false;
            btn_Cancel_InspGrp.Visible = true;
            txtInspectionCode.Enabled = true;
            txtInspectionGrpName.Enabled = true;
            rdInspGrpExternal.Enabled = true;
            rdInspGrpInternal.Enabled = true;
            btn_Save_InspGrp.Enabled = true;
            //Alerts.ShowPanel(pnl_Charterer);
            try
            {
                Alerts.HANDLE_AUTHORITY(5, btn_New_InspGrp, btn_Save_InspGrp, btn_Cancel_InspGrp, btn_Print_InspGrp, Auth);
            }
            catch
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Login.aspx';", true);
            }
        }
    }

    protected void btnEditInsGroup_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow row = (GridViewRow)(((ImageButton)sender).NamingContainer);
        int Rowindx = row.RowIndex;
        Mode = "Edit";
        HiddenField hfdInspectionGroup;
        hfdInspectionGroup = (HiddenField)GridView_InsGrp.Rows[Rowindx].FindControl("Hidden_InspectionGroupId");
        id = Convert.ToInt32(hfdInspectionGroup.Value.ToString());
        Show_Record_InspectionGroup(id);
        GridView_InsGrp.SelectedIndex = Rowindx;
        btn_New_InspGrp.Visible = false;
        btn_Cancel_InspGrp.Visible = true;
        txtInspectionCode.Enabled = true;
        txtInspectionGrpName.Enabled = true;
        rdInspGrpExternal.Enabled = true;
        rdInspGrpInternal.Enabled = true;
        btn_Save_InspGrp.Enabled = true;
        //Alerts.ShowPanel(pnl_Charterer);
        try
        {
            Alerts.HANDLE_AUTHORITY(5, btn_New_InspGrp, btn_Save_InspGrp, btn_Cancel_InspGrp, btn_Print_InspGrp, Auth);
        }
        catch
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Default.aspx';", true);
        }
    }
}
