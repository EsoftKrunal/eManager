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


public partial class Registers_IRM_GroupMaster : System.Web.UI.Page
{
    string Mode = "New";
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        lblMessage.Text = "";
        if (!Page.IsPostBack)
        {
            BindGroups();
        }
    }

    public void BindGroups()
    {
        try
        {
            DataTable dtGroups = Budget.getTable("SELECT GroupId,GroupName,ShortName,Description FROM IRM_Groups ORDER BY GroupName").Tables[0];
            GrdView_Groups.DataSource = dtGroups;
            GrdView_Groups.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    protected void btnNewGroup_Click(object sender, EventArgs e)
    {
        txtGroupName.Focus();
        txtGroupName.Enabled = true;
        txtShname.Enabled = true;
        txtDescr.Enabled = true;
        btnSave.Enabled = true;
        btnCancel.Visible = true;
        btnNewGroup.Visible = false;
        hfdGroupId.Value = "";

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtGroupName.Enabled = false;
        txtShname.Enabled = false;
        txtDescr.Enabled = false;
        btnSave.Enabled = false;
        btnCancel.Visible = false;
        btnNewGroup.Visible = true;
        txtGroupName.Text = "";
        hfdGroupId.Value = "";
        txtShname.Text = "";
        txtDescr.Text = "";
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string SQL = "EXEC sp_IRM_InsertUpdateGroups '" + hfdGroupId.Value.Trim() + "','" + txtGroupName.Text.Trim() + "','" + txtShname.Text.Trim() + "','" + txtDescr.Text.Trim() + "' ";
        DataTable dt = Budget.getTable(SQL).Tables[0];
        if (dt.Rows.Count > 0)
        {
            btnNewGroup_Click(sender, e);
            btnCancel_Click(sender, e);
            BindGroups();
            lblMessage.Text = "Record Successfully Saved.";
        }
        else
        {
            lblMessage.Text = "Transaction Failed.";
        }
    }
    protected void GrdView_Groups_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Modify")
        {
            GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
            int Rowindx = row.RowIndex;
            Mode = "Edit";
            HiddenField hdGroupId;
            hdGroupId = (HiddenField)GrdView_Groups.Rows[Rowindx].FindControl("hdGroupId");
            hfdGroupId.Value = hdGroupId.Value.ToString();
            Show_Record_InsuranceGroup(hfdGroupId.Value);
            GrdView_Groups.SelectedIndex = Rowindx;
            txtShname.Focus();
            txtGroupName.Enabled = true;
            txtShname.Enabled = true;
            txtDescr.Enabled = true;
            btnSave.Enabled = true;
            btnCancel.Visible = true;
            btnNewGroup.Visible = false;
            //txtGroupName.Text = row.Cells[2].Text.Trim().Replace("&nbsp;", "").Replace("&amp;", "&");
            //txtShname.Text = row.Cells[1].Text.Trim().Replace("&nbsp;", "").Replace("&amp;","&");
            //HiddenField hdfDescr = (HiddenField)row.Cells[0].FindControl("hdfDescr");
            //txtDescr.Text = hdfDescr.Value.Trim().Replace("&nbsp;", "").Replace("&amp;", "&");
        }
    }
   
    protected void GrdView_Groups_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GrdView_Groups.PageIndex = e.NewPageIndex;
        BindGroups();
    }

    protected void GrdView_Groups_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {

    }

    protected void imgbtnEdit_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow row = (GridViewRow)(((ImageButton)sender).NamingContainer);
        int Rowindx = row.RowIndex;
        Mode = "Edit";
        HiddenField hdGroupId;
        hdGroupId =  (HiddenField)GrdView_Groups.Rows[Rowindx].FindControl("hdGroupId");
        hfdGroupId.Value = hdGroupId.Value.ToString();
        Show_Record_InsuranceGroup(hfdGroupId.Value);
        GrdView_Groups.SelectedIndex = Rowindx;
        // GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
        txtShname.Focus();
        txtGroupName.Enabled = true;
        txtShname.Enabled = true;
        txtDescr.Enabled = true;
        btnSave.Enabled = true;
        btnCancel.Visible = true;
        btnNewGroup.Visible = false;
        //txtGroupName.Text = row.Cells[2].Text.Trim().Replace("&nbsp;", "").Replace("&amp;", "&");
        //txtShname.Text = row.Cells[1].Text.Trim().Replace("&nbsp;", "").Replace("&amp;", "&");
        //HiddenField hdfDescr = (HiddenField)row.Cells[0].FindControl("hdfDescr");
        //txtDescr.Text = hdfDescr.Value.Trim().Replace("&nbsp;", "").Replace("&amp;", "&");
    }

    protected void GrdView_Groups_RowEditing(object sender, GridViewEditEventArgs e)
    {
        Mode = "Edit";
        HiddenField hdGroupId;
        hdGroupId = (HiddenField)GrdView_Groups.Rows[e.NewEditIndex].FindControl("hdGroupId");
        hfdGroupId.Value = hdGroupId.Value.ToString();
        Show_Record_InsuranceGroup(hfdGroupId.Value);
        //GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
        GrdView_Groups.SelectedIndex = e.NewEditIndex;
        txtShname.Focus();
        txtGroupName.Enabled = true;
        txtShname.Enabled = true;
        txtDescr.Enabled = true;
        btnSave.Enabled = true;
        btnCancel.Visible = true;
        btnNewGroup.Visible = false;
        //txtGroupName.Text = row.Cells[2].Text.Trim().Replace("&nbsp;", "").Replace("&amp;", "&");
        //txtShname.Text = row.Cells[1].Text.Trim().Replace("&nbsp;", "").Replace("&amp;", "&");   
        //HiddenField hdfDescr = (HiddenField)row.Cells[0].FindControl("hdfDescr");
        //txtDescr.Text = hdfDescr.Value.Trim().Replace("&nbsp;", "").Replace("&amp;", "&");
    }

    protected void Show_Record_InsuranceGroup(string InsuranceGroupId)
    {
        hfdGroupId.Value = InsuranceGroupId.ToString();
        DataTable dtInsGroups = Budget.getTable("SELECT GroupId,GroupName,ShortName,Description FROM IRM_Groups where GroupId = '" + hfdGroupId.Value + "' ").Tables[0];
        
        foreach (DataRow dr in dtInsGroups.Rows)
        {
            txtGroupName.Text = dr["GroupName"].ToString().Trim().Replace("&nbsp;", "").Replace("&amp;", "&");
            txtShname.Text = dr["ShortName"].ToString().Trim().Replace("&nbsp;", "").Replace("&amp;", "&");
            txtDescr.Text = dr["Description"].ToString().Trim().Replace("&nbsp;", "").Replace("&amp;", "&");
        }
    }
}
