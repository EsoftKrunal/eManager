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

public partial class Registers_IRM_SubGroupMaster : System.Web.UI.Page
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
            BindSubGroups();
        }
    }
    public void BindGroups()
    {
        try
        {
            DataTable dtGroups = Budget.getTable("SELECT GroupId,GroupName FROM IRM_Groups ORDER BY GroupName").Tables[0];
            ddlGroups.DataSource = dtGroups;
            ddlGroups.DataTextField = "GroupName";
            ddlGroups.DataValueField = "GroupId";
            ddlGroups.DataBind();
            ddlGroups.Items.Insert(0, new ListItem("< Select >", "0"));
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    public void BindSubGroups()
    {
        try
        {
            DataTable dtSubGroups = Budget.getTable("SELECT SG.SubGroupId,SG.GroupId,SG.SubGroupName,GM.GroupName FROM IRM_SubGroups SG INNER JOIN IRM_Groups GM ON GM.GroupId = SG.GroupId Order By GroupName,SubGroupName ").Tables[0];
            GrdView_SubGroups.DataSource = dtSubGroups;
            GrdView_SubGroups.DataBind();
            
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    protected void btnNewSubGroup_Click(object sender, EventArgs e)
    {
        ddlGroups.Focus();
        txtSubGroupName.Enabled = true;
        btnSave.Enabled = true;
        btnCancel.Visible = true;
        btnNewSubGroup.Visible = false;
        hfdSubGroupId.Value = "";
        ddlGroups.SelectedValue = "0";

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtSubGroupName.Enabled = false;
        btnSave.Enabled = false;
        btnCancel.Visible = false;
        btnNewSubGroup.Visible = true;
        txtSubGroupName.Text = "";
        hfdSubGroupId.Value = "";
        ddlGroups.SelectedValue = "0";        
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string SQL = "EXEC sp_IRM_InsertUpdateSubGroups '" + ddlGroups.SelectedValue.Trim() + "','" + hfdSubGroupId.Value.Trim() + "','" + txtSubGroupName.Text.Trim() + "'";
        DataTable dt = Budget.getTable(SQL).Tables[0];
        if (dt.Rows.Count > 0)
        {
            btnNewSubGroup_Click(sender, e);
            btnCancel_Click(sender, e);
            BindSubGroups();
            lblMessage.Text = "Record Successfully Saved.";
        }
        else
        {
            lblMessage.Text = "Transaction Failed.";
        }

    }
    protected void GrdView_SubGroups_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Modify")
        {
            ddlGroups.SelectedValue = e.CommandArgument.ToString().Split(',').GetValue(0).ToString();
            hfdSubGroupId.Value = e.CommandArgument.ToString().Split(',').GetValue(1).ToString();
            GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
            txtSubGroupName.Focus();
            txtSubGroupName.Enabled = true;
            btnSave.Enabled = true;
            btnCancel.Visible = true;
            btnNewSubGroup.Visible = false;
            txtSubGroupName.Text = row.Cells[2].Text.Trim();
        }
    }
    protected void GrdView_SubGroups_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridViewRow row = (GridViewRow)(((ImageButton)sender).NamingContainer);
        int Rowindx = row.RowIndex;
        Mode = "Edit";
        HiddenField hdInsSubGroupId;
        hdInsSubGroupId = (HiddenField)GrdView_SubGroups.Rows[Rowindx].FindControl("hdnInsSubGroupId");
        hfdSubGroupId.Value = hdInsSubGroupId.Value;
        Show_Record_InsuranceSubGroup(hfdSubGroupId.Value);
        GrdView_SubGroups.SelectedIndex = Rowindx;
        txtSubGroupName.Focus();
        txtSubGroupName.Enabled = true;
        btnSave.Enabled = true;
        btnCancel.Visible = true;
        btnNewSubGroup.Visible = false;
    }
    protected void GrdView_SubGroups_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GrdView_SubGroups.PageIndex = e.NewPageIndex;
        BindSubGroups();
    }
    protected void imgbtnEdit_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow row = (GridViewRow)(((ImageButton)sender).NamingContainer);
        int Rowindx = row.RowIndex;
        Mode = "Edit";
        HiddenField hdInsSubGroupId;
        hdInsSubGroupId = (HiddenField)GrdView_SubGroups.Rows[Rowindx].FindControl("hdnInsSubGroupId");
        hfdSubGroupId.Value = hdInsSubGroupId.Value;
        Show_Record_InsuranceSubGroup(hfdSubGroupId.Value);
        GrdView_SubGroups.SelectedIndex = Rowindx;
        txtSubGroupName.Focus();
        txtSubGroupName.Enabled = true;
        btnSave.Enabled = true;
        btnCancel.Visible = true;
        btnNewSubGroup.Visible = false;
    }
    protected void Show_Record_InsuranceSubGroup(string InsuranceSubGroupId)
    {
        //hfdGroupId.Value = InsuranceGroupId.ToString();
        DataTable dtInsSubGroups = Budget.getTable("SELECT SG.SubGroupId,SG.GroupId,SG.SubGroupName,GM.GroupName FROM IRM_SubGroups SG INNER JOIN IRM_Groups GM ON GM.GroupId = SG.GroupId  where SubGroupId = '" + InsuranceSubGroupId.ToString() + "' Order By GroupName,SubGroupName ").Tables[0];

        foreach (DataRow dr in dtInsSubGroups.Rows)
        {
            ddlGroups.SelectedValue = dr["GroupId"].ToString();
            txtSubGroupName.Text = dr["SubGroupName"].ToString().Trim();
           
        }
    }
}
