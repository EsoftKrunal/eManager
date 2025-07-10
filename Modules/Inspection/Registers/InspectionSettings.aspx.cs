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

public partial class Registers_InspectionSettings : System.Web.UI.Page
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
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 158);
        if (chpageauth <= 0)
            Response.Redirect("blank.aspx");
        //------------------------------------------------------------------------------------------------------------
   
        if (Session["loginid"] == null)
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Default.aspx';", true);
        }
        lbl_GridView_InspectionSettings.Text = "";
        lbl_InspectionSettings_Message.Text = "";
        if (Session["loginid"] != null)
        {
            ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 12);
            OBJ.Invoke();
            Session["Authority"] = OBJ.Authority;
            Auth = OBJ.Authority;
        }
        if (!Page.IsPostBack)
        {
            bindInspectionSettingsGrid();
            try
            {
                Alerts.HANDLE_AUTHORITY(1, btn_New_InspectionSettings, btn_Save_InspectionSettings, btn_Cancel_InspectionSettings, btn_Print_InspectionSettings, Auth);
            }
            catch
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Default.aspx';", true);
            }
        }
    }
    public void bindInspectionSettingsGrid()
    {
        DataTable dt1 = Inspection_Settings.InspectionSettingsDetails(0, "", 0, "", "", "", 0, 0, "Select");
        this.GridView_InspSet.DataSource = dt1;
        this.GridView_InspSet.DataBind();
        if (dt1.Rows.Count > 0)
            HiddenFieldGridRowCount.Value = dt1.Rows.Count.ToString();
        else
            HiddenFieldGridRowCount.Value = "0";
    }
    protected void btn_New_InspectionSettings_Click(object sender, EventArgs e)
    {
        txtStatusName.Focus();
        txtStatusName.Enabled = true;
        txtStatusColor.Enabled = true;
        ImageButton2.Enabled = true;
        FileUpload_StatusIcon.Enabled = true;
        txtAlertPeriod.Enabled = true;
        txtMailTo.Enabled = true;
        btn_Save_InspectionSettings.Enabled = true;
        btn_Cancel_InspectionSettings.Visible = true;
        txtStatusName.Text = "";
        txtStatusColor.Text = "";
        txtAlertPeriod.Text = "";
        txtMailTo.Text = "";
        txtCreatedBy_InspectionSettings.Text = "";
        txtCreatedOn_InspectionSettings.Text = "";
        txtModifiedBy_InspectionSettings.Text = "";
        txtModifiedOn_InspectionSettings.Text = "";
        HiddenInspectionSettings.Value = "";
        GridView_InspSet.SelectedIndex = -1;
        try
        {
            Alerts.HANDLE_AUTHORITY(2, btn_New_InspectionSettings, btn_Save_InspectionSettings, btn_Cancel_InspectionSettings, btn_Print_InspectionSettings, Auth);
        }
        catch
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Default.aspx';", true);
        }
    }
    protected void btn_Save_InspectionSettings_Click(object sender, EventArgs e)
    {
        DataTable dt1;
        int intInspSettingsId = -1;
        int intCreatedBy = 0;
        int intModifiedBy = 0;

        if (HiddenInspectionSettings.Value.ToString().Trim() == "")
        {
            intCreatedBy = Convert.ToInt32(Session["loginid"].ToString());
        }
        else
        {
            intInspSettingsId = Convert.ToInt32(HiddenInspectionSettings.Value);
            intModifiedBy = Convert.ToInt32(Session["loginid"].ToString());
        }

        //string strStatusName = ddlStatusName.SelectedValue;
        string strStatusName = txtStatusName.Text;
        string strStatusColor = txtStatusColor.Text;

        //*******Image Saving Code
        string strStatusIcon = "";
        if(!(HiddenInspectionSettings.Value.ToString().Trim() == ""))
        {
            if ((HiddenFieldStatusIcon.Value.ToString().Trim() == "") && (!FileUpload_StatusIcon.HasFile))
            {
                //lbl_InspectionSettings_Message.Text="Status Icon Cannot Be Left Blank.";
                //FileUpload_StatusIcon.Focus();
                //return;
            }
            else
            {
                if(FileUpload_StatusIcon.HasFile)
                {
                    string strfilename = FileUpload_StatusIcon.FileName;
                    if(chk_FileExtension(Path.GetExtension(FileUpload_StatusIcon.FileName).ToLower())==true)
                    {
                        strStatusIcon= "EMANAGERBLOB/Inspection/Masters/" + FileUpload_StatusIcon.FileName.Trim();
                        FileUpload_StatusIcon.SaveAs(Server.MapPath("~/EMANAGERBLOB/Inspection/Masters/" + FileUpload_StatusIcon.FileName.Trim()));
                    }
                    else
                    {
                        lbl_InspectionSettings_Message.Text="Invalid File Type. (Valid Files Are .jpeg, .jpg, .gif, .png)";
                        FileUpload_StatusIcon.Focus();
                        return;
                    }
                }
                else
                {
                    strStatusIcon=HiddenFieldStatusIcon.Value;
                }
            }
        }
        else
        {
            if(FileUpload_StatusIcon.HasFile)
            {
                string strfilename=FileUpload_StatusIcon.FileName;
                if(chk_FileExtension(Path.GetExtension(FileUpload_StatusIcon.FileName).ToLower())==true)
                {
                    strStatusIcon= "EMANAGERBLOB/Inspection/Masters/" + FileUpload_StatusIcon.FileName.Trim();
                    FileUpload_StatusIcon.SaveAs(Server.MapPath("~/EMANAGERBLOB/Inspection/Masters/" + FileUpload_StatusIcon.FileName.Trim()));
                }
                else
                {
                    lbl_InspectionSettings_Message.Text="Invalid File Type. (Valid Files Are .jpeg, .jpg, .gif, .png)";
                    FileUpload_StatusIcon.Focus();
                    return;
                }
            }
            else
            {
                //lbl_InspectionSettings_Message.Text="Status Icon Cannot Be Left Blank.";
                //FileUpload_StatusIcon.Focus();
                //return;
            }
        }
        //**************************
        int intAlertPeriod = Convert.ToInt32(txtAlertPeriod.Text);
        string strMailTo = txtMailTo.Text;
        if (HiddenInspectionSettings.Value.ToString().Trim() == "")
        {
            dt1 = Inspection_Settings.InspectionSettingsDetails(intInspSettingsId, strStatusName, intAlertPeriod, strStatusColor, strStatusIcon, strMailTo, intCreatedBy, intModifiedBy, "Add");
        }
        else
        {
            dt1 = Inspection_Settings.InspectionSettingsDetails(intInspSettingsId, strStatusName, intAlertPeriod, strStatusColor, strStatusIcon, strMailTo, intCreatedBy, intModifiedBy, "Modify");
        }
        if (Inspection_Settings.ErrMsg == "")
        {
            //if (dt1.Rows.Count > 0)
            //{
            //    if (dt1.Rows[0][0].ToString().Substring(0, 9) == "There was")
            //        lbl_InspectionSettings_Message.Text = "Record Not Saved.";
            //}
            //else
            //{
                lbl_InspectionSettings_Message.Text = "Record Successfully Saved.";
            //}
        }
        else { lbl_InspectionSettings_Message.Text = "Transaction Failed."; }
        bindInspectionSettingsGrid();        
        btn_New_InspectionSettings_Click(sender, e);
        btn_Cancel_InspectionSettings_Click(sender, e);
        //Alerts.HidePanel(pnl_Charterer);
        try
        {
            Alerts.HANDLE_AUTHORITY(3, btn_New_InspectionSettings, btn_Save_InspectionSettings, btn_Cancel_InspectionSettings, btn_Print_InspectionSettings, Auth);
        }
        catch
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Default.aspx';", true);
        }
        //}
    }
    public bool chk_FileExtension(string str) 
    { 
        string extension = str; 
        string MIMEType = null; 
        switch (extension)
        { 
            case ".gif": 
                return true; 
            case ".jpg": 
            case ".jpeg": 
                return true; 
            case ".png": 
                return true; 
            default: 
                return false; 
            //return; 
            break; 
        } 
    }
    protected void GridView_InspSet_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            Alerts.HANDLE_GRID(GridView_InspSet, Auth);
        }
        catch
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Default.aspx';", true);
        }
    }
    protected void Show_Record_InspSet(int InspSettingsId)
    {
        HiddenInspectionSettings.Value = InspSettingsId.ToString();
        DataTable dt1 = Inspection_Settings.InspectionSettingsDetails(InspSettingsId, "", 0, "", "", "", 0, 0, "ById");
        foreach (DataRow dr in dt1.Rows)
        {
            //ddlStatusName.SelectedValue = dr["InspDueStatus"].ToString();
            txtStatusName.Text = dr["InspDueStatus"].ToString();
            txtStatusColor.Text = dr["StatusColor"].ToString();
            //FileUpload_StatusIcon.FileName=dr[""].ToString();
            txtAlertPeriod.Text = dr["AlertPeriod"].ToString();
            txtMailTo.Text = dr["MailTo"].ToString();
            txtCreatedBy_InspectionSettings.Text = dr["CreatedBy"].ToString();
            txtCreatedOn_InspectionSettings.Text = dr["CreatedOn"].ToString();
            txtModifiedBy_InspectionSettings.Text = dr["ModifiedBy"].ToString();
            txtModifiedOn_InspectionSettings.Text = dr["ModifiedOn"].ToString();
            HiddenFieldStatusName.Value = dr["InspDueStatus"].ToString();
        }
    }
    protected void GridView_InspSet_RowEditing(object sender, GridViewEditEventArgs e)
    {
        Mode = "Edit";
        HiddenField hfdInspSet,hfdStatusIcon;
        hfdInspSet = (HiddenField)GridView_InspSet.Rows[e.NewEditIndex].FindControl("Hidden_InspSettingsId");
        hfdStatusIcon=(HiddenField)GridView_InspSet.Rows[e.NewEditIndex].FindControl("Hidden_StatusIcon");
        HiddenFieldStatusIcon.Value=hfdStatusIcon.Value;
        id = Convert.ToInt32(hfdInspSet.Value.ToString());
        Show_Record_InspSet(id);
        GridView_InspSet.SelectedIndex = e.NewEditIndex;
        //btn_New_InspectionSettings.Visible = false;
        btn_Cancel_InspectionSettings.Visible = true;
        //ddlStatusName.Enabled = true;
        //txtStatusName.Enabled = true;
        txtStatusColor.Enabled = true;
        ImageButton2.Enabled = true;
        FileUpload_StatusIcon.Enabled = true;
        if (HiddenFieldStatusName.Value == "Due")
            txtAlertPeriod.Enabled = true;
        else
            txtAlertPeriod.Enabled = false;
        txtMailTo.Enabled = true;
        btn_Save_InspectionSettings.Enabled = true;
        //Alerts.ShowPanel(pnl_Charterer);
        try
        {
            Alerts.HANDLE_AUTHORITY(5, btn_New_InspectionSettings, btn_Save_InspectionSettings, btn_Cancel_InspectionSettings, btn_Print_InspectionSettings, Auth);
        }
        catch
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Default.aspx';", true);
        }
    }
    protected void GridView_InspSet_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DataTable dt1;
        int intModifiedBy = Convert.ToInt32(Session["loginid"].ToString());
        HiddenField hfdInspSet;
        hfdInspSet = (HiddenField)GridView_InspSet.Rows[e.RowIndex].FindControl("Hidden_InspSettingsId");
        id = Convert.ToInt32(hfdInspSet.Value.ToString());
        dt1 = Inspection_Settings.InspectionSettingsDetails(id, "", 0, "", "", "", 0, intModifiedBy, "Delete");
        if (Inspection_Settings.ErrMsg == "")
        {
            //if (dt1.Rows.Count > 0)
            //{
            //    if (dt1.Rows[0][0].ToString().Substring(0, 9) == "There was")
            //        lbl_InspectionSettings_Message.Text = "Record Not Deleted.";
            //}
            //else
            //{
                lbl_InspectionSettings_Message.Text = "Record Successfully Deleted.";
            //}
        }
        else { lbl_InspectionSettings_Message.Text = "Record Not Deleted."; }
        bindInspectionSettingsGrid();
        if (HiddenInspectionSettings.Value.ToString() == hfdInspSet.Value.ToString())
        {
            btn_New_InspectionSettings_Click(sender, e);
        }
    }
    protected void GridView_InspSet_PreRender(object sender, EventArgs e)
    {
        //if (GridView_InspSet.Rows.Count <= 0) { lbl_GridView_InspectionSettings.Text = ""; } else { lbl_GridView_InspectionSettings.Text = "No. of Records Found: " + GridView_InspSet.Rows.Count; }
        if (GridView_InspSet.Rows.Count <= 0) { lbl_GridView_InspectionSettings.Text = ""; } else { lbl_GridView_InspectionSettings.Text = "No. of Records Found: " + HiddenFieldGridRowCount.Value; }
    }
    protected void btn_Cancel_InspectionSettings_Click(object sender, EventArgs e)
    {
        //ddlStatusName.Enabled = false;
        txtStatusName.Enabled = false;
        txtStatusColor.Enabled = false;
        ImageButton2.Enabled = false;
        FileUpload_StatusIcon.Enabled = false;
        txtAlertPeriod.Enabled = false;
        txtMailTo.Enabled = false;
        //btn_Save_InspectionSettings.Enabled = false;
        btn_Save_InspectionSettings.Visible = false;
        btn_Cancel_InspectionSettings.Visible = false;
        btn_New_InspectionSettings.Visible = true;
        //btn_Save_InspectionSettings.Enabled = false;
        //ddlStatusName.SelectedIndex = 0;
        txtStatusName.Text = "";
        txtStatusColor.Text = "";
        //FileUpload_StatusIcon.HasFile = "";
        txtAlertPeriod.Text = "";
        txtMailTo.Text = "";
        txtCreatedBy_InspectionSettings.Text = "";
        txtCreatedOn_InspectionSettings.Text = "";
        txtModifiedBy_InspectionSettings.Text = "";
        txtModifiedOn_InspectionSettings.Text = "";
        HiddenInspectionSettings.Value = "";
        GridView_InspSet.SelectedIndex = -1;
    }
    protected void btn_Print_InspectionSettings_Click(object sender, EventArgs e)
    {

    }
    protected void GridView_InspSet_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView_InspSet.PageIndex = e.NewPageIndex;
        GridView_InspSet.SelectedIndex = -1;
        bindInspectionSettingsGrid();
    }

    protected void GridView_InspSet_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Modify")
        {
            GridViewRow row = (GridViewRow)(((ImageButton)sender).NamingContainer);
            int Rowindx = row.RowIndex;
            Mode = "Edit";
            HiddenField hfdInspSet, hfdStatusIcon;
            hfdInspSet = (HiddenField)GridView_InspSet.Rows[Rowindx].FindControl("Hidden_InspSettingsId");
            hfdStatusIcon = (HiddenField)GridView_InspSet.Rows[Rowindx].FindControl("Hidden_StatusIcon");
            HiddenFieldStatusIcon.Value = hfdStatusIcon.Value;
            id = Convert.ToInt32(hfdInspSet.Value.ToString());
            Show_Record_InspSet(id);
            GridView_InspSet.SelectedIndex = Rowindx;
            //btn_New_InspectionSettings.Visible = false;
            btn_Cancel_InspectionSettings.Visible = true;
            //ddlStatusName.Enabled = true;
            //txtStatusName.Enabled = true;
            txtStatusColor.Enabled = true;
            ImageButton2.Enabled = true;
            FileUpload_StatusIcon.Enabled = true;
            if (HiddenFieldStatusName.Value == "Due")
                txtAlertPeriod.Enabled = true;
            else
                txtAlertPeriod.Enabled = false;
            txtMailTo.Enabled = true;
            btn_Save_InspectionSettings.Enabled = true;
            //Alerts.ShowPanel(pnl_Charterer);
            try
            {
                Alerts.HANDLE_AUTHORITY(5, btn_New_InspectionSettings, btn_Save_InspectionSettings, btn_Cancel_InspectionSettings, btn_Print_InspectionSettings, Auth);
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
        HiddenField hfdInspSet, hfdStatusIcon;
        hfdInspSet = (HiddenField)GridView_InspSet.Rows[Rowindx].FindControl("Hidden_InspSettingsId");
        hfdStatusIcon = (HiddenField)GridView_InspSet.Rows[Rowindx].FindControl("Hidden_StatusIcon");
        HiddenFieldStatusIcon.Value = hfdStatusIcon.Value;
        id = Convert.ToInt32(hfdInspSet.Value.ToString());
        Show_Record_InspSet(id);
        GridView_InspSet.SelectedIndex = Rowindx;
        //btn_New_InspectionSettings.Visible = false;
        btn_Cancel_InspectionSettings.Visible = true;
        //ddlStatusName.Enabled = true;
        //txtStatusName.Enabled = true;
        txtStatusColor.Enabled = true;
        ImageButton2.Enabled = true;
        FileUpload_StatusIcon.Enabled = true;
        if (HiddenFieldStatusName.Value == "Due")
            txtAlertPeriod.Enabled = true;
        else
            txtAlertPeriod.Enabled = false;
        txtMailTo.Enabled = true;
        btn_Save_InspectionSettings.Enabled = true;
        //Alerts.ShowPanel(pnl_Charterer);
        try
        {
            Alerts.HANDLE_AUTHORITY(5, btn_New_InspectionSettings, btn_Save_InspectionSettings, btn_Cancel_InspectionSettings, btn_Print_InspectionSettings, Auth);
        }
        catch
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Login.aspx';", true);
        }
    }
    }
