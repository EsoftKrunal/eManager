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


public partial class Registers_Owner_Pool : System.Web.UI.Page
{
    int id;
    Authority Auth;
    string Mode = "New";
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        lbl_GridView_OwnerPool.Text = "";
        lbl_OwnerPool_Message.Text = "";
       
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
            bindOwnerPoolNameGrid();
            bindStatusDDL();
            Alerts.HidePanel(pnl_OwnerPool);
            Alerts.HANDLE_AUTHORITY(1, btn_Add_OwnerPool, btn_Save_OwnerPool, btn_Cancel_OwnerPool, btn_Print_OwnerPool, Auth);
        }
    }
    public void bindOwnerPoolNameGrid()
    {
        DataTable dt1 = OwnerPool.selectDataOwnerPoolDetails();
        this.GridView_OwnerPool.DataSource = dt1;
        this.GridView_OwnerPool.DataBind();
    }
    public void bindStatusDDL()
    {
        DataTable dt2 = Port.selectDataStatusDetails();
        this.ddlStatus_OwnerPool.DataValueField = "StatusId";
        this.ddlStatus_OwnerPool.DataTextField = "StatusName";
        this.ddlStatus_OwnerPool.DataSource = dt2;
        this.ddlStatus_OwnerPool.DataBind();
    }
    protected void btn_Add_OwnerPool_Click(object sender, EventArgs e)
    {
        txtOwnerPoolName.Text = "";
        txtCreatedBy_OwnerPool.Text = "";
        txtCreatedOn_OwnerPool.Text = "";
        txtModifiedBy_OwnerPool.Text = "";
        txtModifiedOn_OwnerPool.Text = "";
        ddlStatus_OwnerPool.SelectedIndex = 0;
        HiddenOwnerPool.Value = "";
        GridView_OwnerPool.SelectedIndex = -1;
      
        Alerts.ShowPanel(pnl_OwnerPool);
        Alerts.HANDLE_AUTHORITY(2, btn_Add_OwnerPool, btn_Save_OwnerPool, btn_Cancel_OwnerPool, btn_Print_OwnerPool, Auth);
    }
    protected void btn_Save_OwnerPool_Click(object sender, EventArgs e)
    {
        int Duplicate=0;
        
            foreach (GridViewRow dg in GridView_OwnerPool.Rows)
            {
                HiddenField hfd;
                HiddenField hfd1;
                hfd = (HiddenField)dg.FindControl("HiddenOwnerPoolName");
                hfd1 = (HiddenField)dg.FindControl("HiddenOwnerPoolId");

                if (hfd.Value.ToString().ToUpper().Trim() == txtOwnerPoolName.Text.ToUpper().Trim())
                {
                    if (HiddenOwnerPool.Value.Trim() == "")
                    {
                        lbl_OwnerPool_Message.Text = "Already Entered.";
                        Duplicate = 1;
                        break;
                    }
                    else if (HiddenOwnerPool.Value.Trim() != hfd1.Value.ToString())
                    {
                        lbl_OwnerPool_Message.Text = "Already Entered.";
                        Duplicate = 1;
                        break;
                    }
                }
                else
                {
                    lbl_OwnerPool_Message.Text = "";
                }
            }
            if (Duplicate == 0)
            {
                int intOwnerPoolId = -1;
                int intCreatedBy = 0;
                int intModifiedBy = 0;
                if (HiddenOwnerPool.Value.ToString().Trim() == "")
                {
                    intCreatedBy = Convert.ToInt32(Session["loginid"].ToString());
                }
                else
                {
                    intOwnerPoolId = Convert.ToInt32(HiddenOwnerPool.Value);
                    intModifiedBy = Convert.ToInt32(Session["loginid"].ToString());
                }
                string strOwnerPoolName = txtOwnerPoolName.Text;
                char charStatusId = Convert.ToChar(ddlStatus_OwnerPool.SelectedValue);

                OwnerPool.insertUpdateOwnerPoolDetails("InsertUpdateOwnerPoolDetails",
                                              intOwnerPoolId,
                                              strOwnerPoolName,
                                              intCreatedBy,
                                              intModifiedBy,
                                              charStatusId);

                bindOwnerPoolNameGrid();
                lbl_OwnerPool_Message.Text = "Record Successfully Saved.";
                
                Alerts.HidePanel(pnl_OwnerPool);
                Alerts.HANDLE_AUTHORITY(3, btn_Add_OwnerPool, btn_Save_OwnerPool, btn_Cancel_OwnerPool, btn_Print_OwnerPool, Auth);
            }
      
    }
    protected void btn_Cancel_OwnerPool_Click(object sender, EventArgs e)
    {
        GridView_OwnerPool.SelectedIndex = -1;
       
        Alerts.HidePanel(pnl_OwnerPool);
        Alerts.HANDLE_AUTHORITY(6, btn_Add_OwnerPool, btn_Save_OwnerPool, btn_Cancel_OwnerPool, btn_Print_OwnerPool, Auth);
    }
    protected void btn_Print_OwnerPool_Click(object sender, EventArgs e)
    {

    }
    protected void GridView_OwnerPool_DataBound(object sender, EventArgs e)
    {
        Alerts.HANDLE_GRID(GridView_OwnerPool, Auth);
    }
    protected void Show_Record_OwnerPool(int ownerpoolid)
    {
        HiddenOwnerPool.Value = ownerpoolid.ToString();
        DataTable dt3 = OwnerPool.selectDataOwnerPoolDetailsByOwnerPoolId(ownerpoolid);
        foreach (DataRow dr in dt3.Rows)
        {
            txtOwnerPoolName.Text = dr["OwnerPoolName"].ToString();
            txtCreatedBy_OwnerPool.Text = dr["CreatedBy"].ToString();
            txtCreatedOn_OwnerPool.Text = dr["CreatedOn"].ToString();
            txtModifiedBy_OwnerPool.Text = dr["ModifiedBy"].ToString();
            txtModifiedOn_OwnerPool.Text = dr["ModifiedOn"].ToString();
            ddlStatus_OwnerPool.SelectedValue = dr["StatusId"].ToString();
        }
    }
  
    protected void GridView_OwnerPool_SelectedIndexChanged(object sender, EventArgs e)
    {
       
        HiddenField hfdownerpool;
        hfdownerpool = (HiddenField)GridView_OwnerPool.Rows[GridView_OwnerPool.SelectedIndex].FindControl("HiddenOwnerPoolId");
        id = Convert.ToInt32(hfdownerpool.Value.ToString());
        Show_Record_OwnerPool(id);
        
        Alerts.ShowPanel(pnl_OwnerPool);
        Alerts.HANDLE_AUTHORITY(4, btn_Add_OwnerPool, btn_Save_OwnerPool, btn_Cancel_OwnerPool, btn_Print_OwnerPool, Auth);
    }
    
    protected void GridView_OwnerPool_Row_Editing(object sender, GridViewEditEventArgs e)
    {
        HiddenField hfdownerpool;
        hfdownerpool = (HiddenField)GridView_OwnerPool.Rows[e.NewEditIndex].FindControl("HiddenOwnerPoolId");
        id = Convert.ToInt32(hfdownerpool.Value.ToString());
        Show_Record_OwnerPool(id);
        GridView_OwnerPool.SelectedIndex = e.NewEditIndex;
      
        Alerts.ShowPanel(pnl_OwnerPool);
        Alerts.HANDLE_AUTHORITY(5, btn_Add_OwnerPool, btn_Save_OwnerPool, btn_Cancel_OwnerPool, btn_Print_OwnerPool, Auth);
    }
   
    protected void GridView_OwnerPool_Row_Deleting(object sender, GridViewDeleteEventArgs e)
    {
        int modifiedBy = Convert.ToInt32(Session["loginid"].ToString());
        HiddenField hfdownerpool;
        hfdownerpool = (HiddenField)GridView_OwnerPool.Rows[e.RowIndex].FindControl("HiddenOwnerPoolId");
        id = Convert.ToInt32(hfdownerpool.Value.ToString());
        OwnerPool.deleteOwnerPoolDetailsById("DeleteOwnerPoolDetailsById", id, modifiedBy);
        bindOwnerPoolNameGrid();
        if (HiddenOwnerPool.Value.ToString() == hfdownerpool.Value.ToString())
        {
            btn_Add_OwnerPool_Click(sender, e);
        }
    }
    protected void GridView_OwnerPool_PreRender(object sender, EventArgs e)
    {
        if (GridView_OwnerPool.Rows.Count <= 0) { lbl_GridView_OwnerPool.Text = "No Records Found..!"; }
    }

    protected void GridView_OwnerPool_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Modify")
        {
            GridViewRow row = (GridViewRow)(((ImageButton)sender).NamingContainer);
            int Rowindx = row.RowIndex;
            Mode = "Edit";
            HiddenField hfdownerpool;
            hfdownerpool = (HiddenField)GridView_OwnerPool.Rows[Rowindx].FindControl("hdnOwnerPoolId");
            id = Convert.ToInt32(hfdownerpool.Value.ToString());
            Show_Record_OwnerPool(id);
            GridView_OwnerPool.SelectedIndex = Rowindx;

            Alerts.ShowPanel(pnl_OwnerPool);
            Alerts.HANDLE_AUTHORITY(5, btn_Add_OwnerPool, btn_Save_OwnerPool, btn_Cancel_OwnerPool, btn_Print_OwnerPool, Auth);
        }
    }

    protected void btnEditOwnerPool_click(object sender, ImageClickEventArgs e)
    {
        GridViewRow row = (GridViewRow)(((ImageButton)sender).NamingContainer);
        int Rowindx = row.RowIndex;
        Mode = "Edit";
        HiddenField hfdownerpool;
        hfdownerpool = (HiddenField)GridView_OwnerPool.Rows[Rowindx].FindControl("hdnOwnerPoolId");
        id = Convert.ToInt32(hfdownerpool.Value.ToString());
        Show_Record_OwnerPool(id);
        GridView_OwnerPool.SelectedIndex = Rowindx;

        Alerts.ShowPanel(pnl_OwnerPool);
        Alerts.HANDLE_AUTHORITY(5, btn_Add_OwnerPool, btn_Save_OwnerPool, btn_Cancel_OwnerPool, btn_Print_OwnerPool, Auth);
    }
}
