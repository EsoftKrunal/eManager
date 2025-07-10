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

public partial class Registers_IRM_UnderWriterMaster : System.Web.UI.Page
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
            BindUW();
        }
    }
    public void BindUW()
    {
        try
        {
            DataTable dtGroups = Budget.getTable("SELECT UWId,UWName,ShortName FROM IRM_UWMaster Order By UWName").Tables[0];
            GrdView_UW.DataSource = dtGroups;
            GrdView_UW.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    protected void btnNewUW_Click(object sender, EventArgs e)
    {
        txtShName.Focus();
        txtUWName.Enabled = true;
        txtShName.Enabled = true;
        btnSave.Enabled = true;
        btnCancel.Visible = true;
        btnNewUW.Visible = false;
        hfdUWId.Value = "";

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtUWName.Enabled = false;
        txtShName.Enabled = false;
        btnSave.Enabled = false;
        btnCancel.Visible = false;
        btnNewUW.Visible = true;
        txtShName.Text = "";
        txtUWName.Text = "";
        hfdUWId.Value = "";
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string SQL = "sp_IRM_InsertUpdateUW '" + hfdUWId.Value.Trim() + "','" + txtUWName.Text.Trim() + "','" + txtShName.Text.Trim() + "' ";
        DataTable dt = Budget.getTable(SQL).Tables[0];
        if (dt.Rows.Count > 0)
        {
            btnNewUW_Click(sender, e);
            btnCancel_Click(sender, e);
            BindUW();
            lblMessage.Text = "Record Successfully Saved.";
        }
        else
        {
            lblMessage.Text = "Transaction Failed.";
        }
    }
    protected void GrdView_UW_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Modify")
        {
            hfdUWId.Value = e.CommandArgument.ToString();
            GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
            txtShName.Focus();
            txtShName.Enabled = true;
            txtUWName.Enabled = true;
            btnSave.Enabled = true;
            btnCancel.Visible = true;
            btnNewUW.Visible = false;
            txtShName.Text = row.Cells[1].Text.Trim().Replace("&nbsp;", "").Replace("&amp;", "&");
            txtUWName.Text = row.Cells[2].Text.Trim().Replace("&nbsp;", "").Replace("&amp;", "&");
        }
    }
    protected void GrdView_UW_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridViewRow row = (GridViewRow)(((ImageButton)sender).NamingContainer);
        int Rowindx = row.RowIndex;
        Mode = "Edit";
        HiddenField hdnUWId;
        hdnUWId = (HiddenField)GrdView_UW.Rows[Rowindx].FindControl("hdnUWId");
        Show_Record_UWGroup(hdnUWId.Value);
        GrdView_UW.SelectedIndex = Rowindx;
        txtShName.Focus();
        txtShName.Enabled = true;
        txtUWName.Enabled = true;
        btnSave.Enabled = true;
        btnCancel.Visible = true;
        btnNewUW.Visible = false;
    }
    protected void GrdView_UW_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GrdView_UW.PageIndex = e.NewPageIndex;
        BindUW();
    }

    protected void imgbtnEdit_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow row = (GridViewRow)(((ImageButton)sender).NamingContainer);
        int Rowindx = row.RowIndex;
        Mode = "Edit";
        HiddenField hdnUWId;
        hdnUWId = (HiddenField)GrdView_UW.Rows[Rowindx].FindControl("hdnUWId");
        Show_Record_UWGroup(hdnUWId.Value);
        GrdView_UW.SelectedIndex = Rowindx;
        txtShName.Focus();
        txtShName.Enabled = true;
        txtUWName.Enabled = true;
        btnSave.Enabled = true;
        btnCancel.Visible = true;
        btnNewUW.Visible = false;
    }
    protected void Show_Record_UWGroup(string UWId)
    {
        //hfdGroupId.Value = InsuranceGroupId.ToString();
        DataTable dtInsSubGroups = Budget.getTable("SELECT UWId,UWName,ShortName FROM IRM_UWMaster  where UWId = '" + UWId.ToString() + "' ").Tables[0];

        foreach (DataRow dr in dtInsSubGroups.Rows)
        {
            txtShName.Text = dr["ShortName"].ToString().Trim().Replace("&nbsp;", "").Replace("&amp;", "&");
            txtUWName.Text = dr["UWName"].ToString().Replace("&nbsp;", "").Replace("&amp;", "&");

        }
    }
}
