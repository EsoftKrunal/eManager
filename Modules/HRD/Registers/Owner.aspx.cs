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
using System.ComponentModel;
using CrystalDecisions.ReportAppServer;

public partial class Registers_Owner : System.Web.UI.Page
{
    int id;
    Authority Auth;
    string Mode = "New";
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        lbl_GridView_Owner.Text = "";
        lbl_Owner_Message.Text = "";

       
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
            bindOwnerGrid();
            bindOwnerPoolNameDDL();
            bindStatusDDL();
            Alerts.HidePanel(pnl_Owner);
            Alerts.HANDLE_AUTHORITY(1, btn_Add_Owner, btn_Save_Owner, btn_Cancel_Owner, btn_Print_Owner, Auth);
       
        }
    }
   
    public void bindOwnerGrid()
    {
        DataTable dt1 = Owner.selectDataOwnerDetails();
        this.GridView_Owner.DataSource = dt1;
        this.GridView_Owner.DataBind();
    }
    public void bindOwnerPoolNameDDL()
    {
        DataTable dt4 = Owner.selectDataOwnerPoolName();
        this.ddlOwnerPoolName.DataValueField = "OwnerPoolId";
        this.ddlOwnerPoolName.DataTextField = "OwnerPoolName";
        this.ddlOwnerPoolName.DataSource = dt4;
        this.ddlOwnerPoolName.DataBind();
    }
    public void bindStatusDDL()
    {
        DataTable dt2 = Owner.selectDataStatusDetails();
        this.ddlStatus_Owner.DataValueField = "StatusId";
        this.ddlStatus_Owner.DataTextField = "StatusName";
        this.ddlStatus_Owner.DataSource = dt2;
        this.ddlStatus_Owner.DataBind();
    }
    protected void btn_Add_Owner_Click(object sender, EventArgs e)
    {
        HiddenOwner.Value = "";
        ddlOwnerPoolName.SelectedIndex = 0;
        ddlStatus_Owner.SelectedIndex = 0;
        txt_OwnerName.Text = "";
        txtCreatedBy_Owner.Text = "";
        txtCreatedOn_Owner.Text = "";
        txtModifiedBy_Owner.Text = "";
        txtModifiedOn_Owner.Text = "";
        txtOwnerCode.Text = "";
        txtOwnerShortName.Text = "";
        txtOwnerContact.Text = "";
        txtPrimaryMailId.Text = "";
        txtSecondaryMailId.Text = "";
        txtThirdEmailId.Text = "";
        GridView_Owner.SelectedIndex = -1;
        Alerts.ShowPanel(pnl_Owner);
        Alerts.HANDLE_AUTHORITY(2, btn_Add_Owner, btn_Save_Owner, btn_Cancel_Owner, btn_Print_Owner, Auth);    
 
    }
    protected void btn_Save_Owner_Click(object sender, EventArgs e)
    {
        try
        {
            int Duplicate = 0;
            int TempOwnerId = 0;

            foreach (GridViewRow dg in GridView_Owner.Rows)
            {
                HiddenField hfd;
                HiddenField hfd1;
                hfd = (HiddenField)dg.FindControl("HiddenOwnerName");
                hfd1 = (HiddenField)dg.FindControl("HiddenOwnerId");
                TempOwnerId = Convert.ToInt32(hfd1.Value);
                if (hfd.Value.ToString().ToUpper().Trim() == txt_OwnerName.Text.ToUpper().Trim())
                {
                    if (HiddenOwner.Value.Trim() == "")
                    {

                        // lbl_Owner_Message.Text = "Already Entered.";
                        Duplicate = 1;
                        break;
                    }
                    else if (HiddenOwner.Value.Trim() != hfd1.Value.ToString())
                    {

                        //lbl_Owner_Message.Text = "Already Entered.";
                        Duplicate = 1;
                        break;
                    }
                }
                else
                {

                    lbl_Owner_Message.Text = "";
                }
            }
            if (Duplicate == 0)
            {
                int OwnerId = -1;
                int CreatedBy = 0;
                int ModifiedBy = 0;

                if (HiddenOwner.Value.ToString().Trim() == "")
                {
                    CreatedBy = Convert.ToInt32(Session["loginid"].ToString());
                }
                else
                {
                    OwnerId = Convert.ToInt32(HiddenOwner.Value);
                    ModifiedBy = Convert.ToInt32(Session["loginid"].ToString());
                }

                int OwnerPoolId = Convert.ToInt32(ddlOwnerPoolName.SelectedValue);
                string OwnerName = txt_OwnerName.Text;
                string OwnerShortName = txtOwnerShortName.Text;
                string OwnerCode = txtOwnerCode.Text;
                string primaryMailId = txtPrimaryMailId.Text.Trim();
                string secondaryEmailId = txtSecondaryMailId.Text.Trim();
                string thirdEmailId = txtThirdEmailId.Text.Trim();
                string contactNo = txtOwnerContact.Text;
                char charStatusId = Convert.ToChar(ddlStatus_Owner.SelectedValue);

                Owner.insertUpdateOwnerDetails("InsertUpdateOwnerDetails",
                                                 OwnerId,
                                                 OwnerPoolId,
                                                 OwnerName,
                                                 OwnerShortName,
                                                 OwnerCode,
                                                 CreatedBy,
                                                 ModifiedBy,
                                                 charStatusId,
                                                 contactNo,
                                                 primaryMailId,
                                                 secondaryEmailId,
                                                 thirdEmailId);
                bindOwnerGrid();
                lbl_Owner_Message.Text = "Record Saved Successfully.";
                Alerts.HidePanel(pnl_Owner);
                Alerts.HANDLE_AUTHORITY(3, btn_Add_Owner, btn_Save_Owner, btn_Cancel_Owner, btn_Print_Owner, Auth);

            }
            else
            {
                int OwnerId = Convert.ToInt32(HiddenOwner.Value);
                int ModifiedBy = 0;
                int CreatedBy = 0;
                ModifiedBy = Convert.ToInt32(Session["loginid"].ToString());
                int OwnerPoolId = Convert.ToInt32(ddlOwnerPoolName.SelectedValue);
                string OwnerName = txt_OwnerName.Text;
                string OwnerShortName = txtOwnerShortName.Text;
                string OwnerCode = txtOwnerCode.Text;
                string primaryMailId = txtPrimaryMailId.Text.Trim();
                string secondaryEmailId = txtSecondaryMailId.Text.Trim();
                string thirdEmailId = txtThirdEmailId.Text.Trim();
                string contactNo = txtOwnerContact.Text;
                char charStatusId = Convert.ToChar(ddlStatus_Owner.SelectedValue);

                Owner.insertUpdateOwnerDetails("InsertUpdateOwnerDetails",
                                                 OwnerId,
                                                 OwnerPoolId,
                                                 OwnerName,
                                                 OwnerShortName,
                                                 OwnerCode,
                                                 CreatedBy,
                                                 ModifiedBy,
                                                 charStatusId,
                                                 contactNo,
                                                 primaryMailId,
                                                 secondaryEmailId,
                                                 thirdEmailId);
                bindOwnerGrid();
                lbl_Owner_Message.Text = "Record updated Successfully.";
                Alerts.HidePanel(pnl_Owner);
                Alerts.HANDLE_AUTHORITY(3, btn_Add_Owner, btn_Save_Owner, btn_Cancel_Owner, btn_Print_Owner, Auth);
            }
        }
        catch(Exception ex)
        {
            lbl_Owner_Message.Text = ex.Message.ToString();
        }

    }
    protected void btn_Cancel_Owner_Click(object sender, EventArgs e)
    {
        GridView_Owner.SelectedIndex = -1;
        Alerts.HidePanel(pnl_Owner);
        Alerts.HANDLE_AUTHORITY(6, btn_Add_Owner, btn_Save_Owner, btn_Cancel_Owner, btn_Print_Owner, Auth);     
   
    }
    protected void btn_Print_Owner_Click(object sender, EventArgs e)
    {

    }
    protected void GridView_Owner_DataBound(object sender, EventArgs e)
    {
        Alerts.HANDLE_GRID(GridView_Owner, Auth);  
    }
    protected void Show_Record_Owner(int Ownerid)
    {
        string Mess;
        Mess = "";
        HiddenOwner.Value = Ownerid.ToString();
        DataTable dt3 = Owner.selectDataOwnerDetailsByOwnerId(Ownerid);
        foreach (DataRow dr in dt3.Rows)
        {
           
            Mess = Mess + Alerts.Set_DDL_Value(ddlOwnerPoolName, dr["OwnerPoolId"].ToString(), "Owner Pool");
            txt_OwnerName.Text=dr["OwnerName"].ToString();
            txtOwnerShortName.Text=dr["OwnerShortName"].ToString();
            txtOwnerCode.Text=dr["OwnerCode"].ToString();
            txtCreatedBy_Owner.Text = dr["CreatedBy"].ToString();
            txtCreatedOn_Owner.Text = dr["CreatedOn"].ToString();
            txtModifiedBy_Owner.Text = dr["ModifiedBy"].ToString();
            txtModifiedOn_Owner.Text = dr["ModifiedOn"].ToString();
            ddlStatus_Owner.SelectedValue = dr["StatusId"].ToString();
            txtOwnerContact.Text = dr["ContactNo"].ToString();
            txtPrimaryMailId.Text = dr["Mail1"].ToString();
            txtSecondaryMailId.Text = dr["Mail2"].ToString();
            txtThirdEmailId.Text = dr["Mail3"].ToString();
        }

        if (Mess.Length > 0)
        {
            this.lbl_Owner_Message.Text = "The Status Of Following Has Been Changed To In-Active <br/>" + Mess.Substring(1);
        }
    }
    
    protected void GridView_Owner_SelectedIndexChanged(object sender, EventArgs e)
    {
        HiddenField hfdOwner;
        hfdOwner = (HiddenField)GridView_Owner.Rows[GridView_Owner.SelectedIndex].FindControl("HiddenOwnerId");
        id = Convert.ToInt32(hfdOwner.Value.ToString());
        Show_Record_Owner(id);
        Alerts.ShowPanel(pnl_Owner);
        Alerts.HANDLE_AUTHORITY(4, btn_Add_Owner, btn_Save_Owner, btn_Cancel_Owner, btn_Print_Owner, Auth);     
  
    }
   
    protected void GridView_Owner_Row_Editing(object sender, GridViewEditEventArgs e)
    {
        Mode = "Edit";
        HiddenField hfdOwner;
        hfdOwner = (HiddenField)GridView_Owner.Rows[e.NewEditIndex].FindControl("HiddenOwnerId");
        id = Convert.ToInt32(hfdOwner.Value.ToString());
        Show_Record_Owner(id);
        GridView_Owner.SelectedIndex = e.NewEditIndex;
        Alerts.ShowPanel(pnl_Owner);
        Alerts.HANDLE_AUTHORITY(5, btn_Add_Owner, btn_Save_Owner, btn_Cancel_Owner, btn_Print_Owner, Auth);     
     
    }
 
    protected void GridView_Owner_Row_Deleting(object sender, GridViewDeleteEventArgs e)
    {
        int ModifiedBy = Convert.ToInt32(Session["loginid"].ToString());
        HiddenField hfdOwner;
        hfdOwner = (HiddenField)GridView_Owner.Rows[e.RowIndex].FindControl("HiddenOwnerId");
        id = Convert.ToInt32(hfdOwner.Value.ToString());
        Owner.deleteOwnerDetailsById("DeleteOwnerDetailsById", id, ModifiedBy);
        bindOwnerGrid();
        if (HiddenOwner.Value.ToString() == hfdOwner.Value.ToString())
        {
            btn_Add_Owner_Click(sender, e);
        }

    }
    protected void GridView_Owner_PreRender(object sender, EventArgs e)
    {
        if (GridView_Owner.Rows.Count <= 0) { lbl_GridView_Owner.Text = "No Records Found..!"; }
    }

    protected void GridView_Owner_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Modify")
        {
            GridViewRow row = (GridViewRow)(((ImageButton)sender).NamingContainer);
            int Rowindx = row.RowIndex;
            Mode = "Edit";
            HiddenField hfdOwner;
            hfdOwner = (HiddenField)GridView_Owner.Rows[Rowindx].FindControl("hdnOwnerId");
            id = Convert.ToInt32(hfdOwner.Value.ToString());
            Show_Record_Owner(id);
            GridView_Owner.SelectedIndex = Rowindx;
            Alerts.ShowPanel(pnl_Owner);
            Alerts.HANDLE_AUTHORITY(5, btn_Add_Owner, btn_Save_Owner, btn_Cancel_Owner, btn_Print_Owner, Auth);
        }
    }
    protected void btnEditOwner_click(object sender, ImageClickEventArgs e)
    {
        GridViewRow row = (GridViewRow)(((ImageButton)sender).NamingContainer);
        int Rowindx = row.RowIndex;
        Mode = "Edit";
        HiddenField hfdOwner;
        hfdOwner = (HiddenField)GridView_Owner.Rows[Rowindx].FindControl("hdnOwnerId");
        id = Convert.ToInt32(hfdOwner.Value.ToString());
        Show_Record_Owner(id);
        GridView_Owner.SelectedIndex = Rowindx;
        Alerts.ShowPanel(pnl_Owner);
        Alerts.HANDLE_AUTHORITY(5, btn_Add_Owner, btn_Save_Owner, btn_Cancel_Owner, btn_Print_Owner, Auth);
    }
}
