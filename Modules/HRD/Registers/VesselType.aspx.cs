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

public partial class Registers_Vessel_Type : System.Web.UI.Page
{
    int id;
    Authority Auth;
    string Mode = "New";
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        lbl_GridView_VesselType.Text = "";
        lbl_VesselType_Message.Text = "";
        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 10);
        if (chpageauth <= 0)
        {
            Response.Redirect("Dummy.aspx");

        }
        //******************* 

        ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 10);
        OBJ.Invoke();
        Session["Authority"] = OBJ.Authority;
        Auth = OBJ.Authority;
        if (!Page.IsPostBack)
        {
            bindVesselTypeNameGrid();
            bindStatusDDL();
            Alerts.HidePanel(pnl_VesselType);
            Alerts.HANDLE_AUTHORITY(1, btn_Add_VesselType, btn_Save_VesselType, btn_Cancel_VesselType, btn_Print_VesselType, Auth);
        }
    }
    public void bindVesselTypeNameGrid()
    {
        DataTable dt1 = VesselType.selectDataVesselTypeDetails();
        this.GridView_VesselType.DataSource = dt1;
        this.GridView_VesselType.DataBind();
    }
    public void bindStatusDDL()
    {
        DataTable dt2 = Port.selectDataStatusDetails();
        this.ddlStatus_VesselType.DataValueField = "StatusId";
        this.ddlStatus_VesselType.DataTextField = "StatusName";
        this.ddlStatus_VesselType.DataSource = dt2;
        this.ddlStatus_VesselType.DataBind();
    }
    protected void btn_Add_VesselType_Click(object sender, EventArgs e)
    {
        txtVesselTypeName.Text = "";
        txtCreatedBy_VesselType.Text = "";
        txtCreatedOn_VesselType.Text = "";
        txtModifiedBy_VesselType.Text = "";
        txtModifiedOn_VesselType.Text = "";
        this.chkistanker.Checked = false;
        ddlStatus_VesselType.SelectedIndex = 0;
        GridView_VesselType.SelectedIndex = -1;
        HiddenVesselType.Value = "";
        
        Alerts.ShowPanel(pnl_VesselType);
        Alerts.HANDLE_AUTHORITY(2, btn_Add_VesselType, btn_Save_VesselType, btn_Cancel_VesselType, btn_Print_VesselType, Auth);
    }
    protected void btn_Save_VesselType_Click(object sender, EventArgs e)
    {
        int Duplicate=0;
        
            foreach (GridViewRow dg in GridView_VesselType.Rows)
            {
                HiddenField hfd;
                HiddenField hfd1;
                hfd = (HiddenField)dg.FindControl("HiddenVesselTypeName");
                hfd1 = (HiddenField)dg.FindControl("HiddenVesselTypeId");

                if (hfd.Value.ToString().ToUpper().Trim() == txtVesselTypeName.Text.ToUpper().Trim())
                {
                    if (HiddenVesselType.Value.Trim() == "")
                    {
                        lbl_VesselType_Message.Text = "Already Entered.";
                        Duplicate = 1;
                        break;
                    }
                    else if (HiddenVesselType.Value.Trim() != hfd1.Value.ToString())
                    {
                        lbl_VesselType_Message.Text = "Already Entered.";
                        Duplicate = 1;
                        break;
                    }
                }
                else
                {
                    lbl_VesselType_Message.Text = "";
                }
            }
            if (Duplicate == 0)
            {
                int intvesselTypeId = -1;
                int intCreatedBy = 0;
                int intModifiedBy = 0;
                if (HiddenVesselType.Value.ToString().Trim() == "")
                {
                    intCreatedBy = Convert.ToInt32(Session["loginid"].ToString());
                }
                else
                {
                    intvesselTypeId = Convert.ToInt32(HiddenVesselType.Value);
                    intModifiedBy = Convert.ToInt32(Session["loginid"].ToString());
                }
                string strVesselTypeName = txtVesselTypeName.Text;
                char charStatusId = Convert.ToChar(ddlStatus_VesselType.SelectedValue);
                string isTanker = (chkistanker.Checked) ? "Y" : "N";
                VesselType.insertUpdateVesselTypeDetails("InsertUpdateVesselTypeDetails",
                                              intvesselTypeId,
                                              strVesselTypeName,
                                              isTanker,
                                              intCreatedBy,
                                              intModifiedBy,
                                              charStatusId);

                bindVesselTypeNameGrid();
                lbl_VesselType_Message.Text = "Record Successfully Saved.";
               
                Alerts.HidePanel(pnl_VesselType);
                Alerts.HANDLE_AUTHORITY(3, btn_Add_VesselType, btn_Save_VesselType, btn_Cancel_VesselType, btn_Print_VesselType, Auth);
            }
       
    }
    protected void btn_Cancel_VesselType_Click(object sender, EventArgs e)
    {
        GridView_VesselType.SelectedIndex = -1;
      
        Alerts.HidePanel(pnl_VesselType);
        Alerts.HANDLE_AUTHORITY(6, btn_Add_VesselType, btn_Save_VesselType, btn_Cancel_VesselType, btn_Print_VesselType, Auth);
    }
    protected void GridView_VesselType_DataBound(object sender, EventArgs e)
    {
        Alerts.HANDLE_GRID(GridView_VesselType, Auth);
    }
    protected void Show_Record_VesselType(int vesseltypeid)
    {
        HiddenVesselType.Value = vesseltypeid.ToString();
        DataTable dt3 = VesselType.selectDataVesselTypeDetailsByVesselTypeId(vesseltypeid);
        foreach (DataRow dr in dt3.Rows)
        {
            txtVesselTypeName.Text = dr["VesselTypeName"].ToString();
            txtCreatedBy_VesselType.Text = dr["CreatedBy"].ToString();
            txtCreatedOn_VesselType.Text = dr["CreatedOn"].ToString();
            txtModifiedBy_VesselType.Text = dr["ModifiedBy"].ToString();
            txtModifiedOn_VesselType.Text = dr["ModifiedOn"].ToString();
            ddlStatus_VesselType.SelectedValue = dr["StatusId"].ToString();
            if (dr["IsTanker"].ToString() == "Yes")
            {
                this.chkistanker.Checked = true;
            }
            else
            {
                this.chkistanker.Checked = false;
            }
        }
    }
    // VIEW THE RECORD
    protected void GridView_VesselType_SelectedIndexChanged(object sender, EventArgs e)
    {
     
        HiddenField hfdvesseltype;
        hfdvesseltype = (HiddenField)GridView_VesselType.Rows[GridView_VesselType.SelectedIndex].FindControl("HiddenVesselTypeId");
        id = Convert.ToInt32(hfdvesseltype.Value.ToString());
        Show_Record_VesselType(id);
       
        Alerts.ShowPanel(pnl_VesselType);
        Alerts.HANDLE_AUTHORITY(4, btn_Add_VesselType, btn_Save_VesselType, btn_Cancel_VesselType, btn_Print_VesselType, Auth);
    }
    //EDIT THE RECORD
    protected void GridView_VesselType_Row_Editing(object sender, GridViewEditEventArgs e)
    {
        HiddenField hfdvesseltype;
        hfdvesseltype = (HiddenField)GridView_VesselType.Rows[e.NewEditIndex].FindControl("HiddenVesselTypeId");
        id = Convert.ToInt32(hfdvesseltype.Value.ToString());
        Show_Record_VesselType(id);
        GridView_VesselType.SelectedIndex = e.NewEditIndex;
      
        Alerts.ShowPanel(pnl_VesselType);
        Alerts.HANDLE_AUTHORITY(5, btn_Add_VesselType, btn_Save_VesselType, btn_Cancel_VesselType, btn_Print_VesselType, Auth);
    }
    // DELETE THE RECORD
    protected void GridView_VesselType_Row_Deleting(object sender, GridViewDeleteEventArgs e)
    {
        int intModifiedBy = Convert.ToInt32(Session["loginid"].ToString());
        HiddenField hfdvesseltype;
        hfdvesseltype = (HiddenField)GridView_VesselType.Rows[e.RowIndex].FindControl("HiddenVesselTypeId");
        id = Convert.ToInt32(hfdvesseltype.Value.ToString());
        VesselType.deleteVesselTypeDetailsById("DeleteVesselTypeDetailsById", id, intModifiedBy);
        bindVesselTypeNameGrid();
        if (HiddenVesselType.Value.ToString() == hfdvesseltype.Value.ToString())
        {
            btn_Add_VesselType_Click(sender, e);
        }
    }
    protected void GridView_VesselType_PreRender(object sender, EventArgs e)
    {
        if (GridView_VesselType.Rows.Count <= 0) { lbl_GridView_VesselType.Text = "No Records Found..!"; }
    }
    protected void btn_Print_VesselType_Click(object sender, EventArgs e)
    {

    }

    protected void GridView_VesselType_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Modify")
        {
            GridViewRow row = (GridViewRow)(((ImageButton)sender).NamingContainer);
            int Rowindx = row.RowIndex;
            Mode = "Edit";
            HiddenField hfdvesseltype;
            hfdvesseltype = (HiddenField)GridView_VesselType.Rows[Rowindx].FindControl("hdnVesselTypeId");
            id = Convert.ToInt32(hfdvesseltype.Value.ToString());
            Show_Record_VesselType(id);
            GridView_VesselType.SelectedIndex = Rowindx;

            Alerts.ShowPanel(pnl_VesselType);
            Alerts.HANDLE_AUTHORITY(5, btn_Add_VesselType, btn_Save_VesselType, btn_Cancel_VesselType, btn_Print_VesselType, Auth);
        }
    }

    protected void btnEditVesselType_click(object sender, ImageClickEventArgs e)
    {
        GridViewRow row = (GridViewRow)(((ImageButton)sender).NamingContainer);
        int Rowindx = row.RowIndex;
        Mode = "Edit";
        HiddenField hfdvesseltype;
        hfdvesseltype = (HiddenField)GridView_VesselType.Rows[Rowindx].FindControl("hdnVesselTypeId");
        id = Convert.ToInt32(hfdvesseltype.Value.ToString());
        Show_Record_VesselType(id);
        GridView_VesselType.SelectedIndex = Rowindx;

        Alerts.ShowPanel(pnl_VesselType);
        Alerts.HANDLE_AUTHORITY(5, btn_Add_VesselType, btn_Save_VesselType, btn_Cancel_VesselType, btn_Print_VesselType, Auth);
    }
    }
