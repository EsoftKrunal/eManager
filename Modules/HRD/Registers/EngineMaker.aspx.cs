using System;
using System.Data;
using System.Configuration;
using System.Reflection;
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

public partial class Registers_EngineMaker : System.Web.UI.Page
{
    int id;
    Authority Auth;
    string Mode = "New";
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        lbl_EngineMaker_Message.Text = "";
        lbl_GridView_EngineMaker.Text = "";

        
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
            bindEngineMakerGrid();
            bindStatusDDL();
            Alerts.HidePanel(pnl_EngineMaker);
            Alerts.HANDLE_AUTHORITY(1, btn_Add_EngineMaker, btn_Save_EngineMaker, btn_Cancel_EngineMaker, btn_Print_EngineMaker, Auth);
       
        }
    }
 
    public void bindEngineMakerGrid()
    {
        DataTable dt1 = EngineMaker.selectDataEngineMakerDetails();
        this.GridView_EngineMaker.DataSource = dt1;
        this.GridView_EngineMaker.DataBind();
    }
    public void bindStatusDDL()
    {
        DataTable dt2 = EngineMaker.selectDataStatusDetails();
        this.ddlStatus_EngineMaker.DataValueField = "StatusId";
        this.ddlStatus_EngineMaker.DataTextField = "StatusName";
        this.ddlStatus_EngineMaker.DataSource = dt2;
        this.ddlStatus_EngineMaker.DataBind();
    }
    protected void btn_Add_EngineMaker_Click(object sender, EventArgs e)
    {
        HiddenEngineMaker.Value = "";
        ddlEngineMakerType.SelectedIndex = 0;
        ddlStatus_EngineMaker.SelectedIndex = 0;
        GridView_EngineMaker.SelectedIndex = -1;
        txtCreatedBy_EngineMaker.Text = "";
        txtCreatedOn_EngineMaker.Text = "";
        txtEngineMakerName.Text = "";
        txtModifiedBy_EngineMaker.Text = "";
        txtModifiedOn_EngineMaker.Text = "";
        Alerts.ShowPanel(pnl_EngineMaker);
        Alerts.HANDLE_AUTHORITY(2, btn_Add_EngineMaker, btn_Save_EngineMaker, btn_Cancel_EngineMaker, btn_Print_EngineMaker, Auth);    
 
    }
    protected void btn_Save_EngineMaker_Click(object sender, EventArgs e)
    {
        int Duplicate=0;
        
            foreach (GridViewRow dg in GridView_EngineMaker.Rows)
            {
                HiddenField hfd;
                HiddenField hfd1;
                hfd = (HiddenField)dg.FindControl("HiddenEngineMakername");
                hfd1 = (HiddenField)dg.FindControl("HiddenEngineMakerId");

                if (hfd.Value.ToString().ToUpper().Trim() == txtEngineMakerName.Text.ToUpper().Trim())
                {
                    if (HiddenEngineMaker.Value.Trim() == "")
                    {

                        lbl_EngineMaker_Message.Text = "Already Entered.";
                        Duplicate = 1;
                        break;
                    }
                    else if (HiddenEngineMaker.Value.Trim() != hfd1.Value.ToString())
                    {

                        lbl_EngineMaker_Message.Text = "Already Entered.";
                        Duplicate = 1;
                        break;
                    }
                }
                else
                {

                    lbl_EngineMaker_Message.Text = "";
                }
            }
            if (Duplicate == 0)
            {
                int EngineMakerId = -1;
                int CreatedBy = 0;
                int ModifiedBy = 0;

                if (HiddenEngineMaker.Value.ToString().Trim() == "")
                {
                    CreatedBy = Convert.ToInt32(Session["loginid"].ToString());
                }
                else
                {
                    EngineMakerId = Convert.ToInt32(HiddenEngineMaker.Value);
                    ModifiedBy = Convert.ToInt32(Session["loginid"].ToString());
                }
                char EngineMakerTypeId = Convert.ToChar(ddlEngineMakerType.SelectedValue);
                string EngineMakerName = txtEngineMakerName.Text;
                char charStatusId = Convert.ToChar(ddlStatus_EngineMaker.SelectedValue);

                EngineMaker.insertUpdateEngineMakerDetails("InsertUpdateEngineMakerDetails",
                                                  EngineMakerId,
                                                  EngineMakerName,
                                                  EngineMakerTypeId,
                                                  CreatedBy,
                                                  ModifiedBy,
                                                  charStatusId);

                bindEngineMakerGrid();


                lbl_EngineMaker_Message.Text = "Record Successfully Saved.";
                Alerts.HidePanel(pnl_EngineMaker);
                Alerts.HANDLE_AUTHORITY(3, btn_Add_EngineMaker, btn_Save_EngineMaker, btn_Cancel_EngineMaker, btn_Print_EngineMaker, Auth);    
           
            }
    }
    protected void btn_Cancel_EngineMaker_Click(object sender, EventArgs e)
    {
       
        GridView_EngineMaker.SelectedIndex = -1;
        Alerts.HidePanel(pnl_EngineMaker);
        Alerts.HANDLE_AUTHORITY(6, btn_Add_EngineMaker, btn_Save_EngineMaker, btn_Cancel_EngineMaker, btn_Print_EngineMaker, Auth);     
   
    }
    protected void btn_Print_EngineMaker_Click(object sender, EventArgs e)
    {

    }
    protected void GridView_EngineMaker_DataBound(object sender, EventArgs e)
    {
        Alerts.HANDLE_GRID(GridView_EngineMaker, Auth);  
    }
    protected void Show_Record_EngineMaker(int EngineMakerid)
    {
       
        HiddenEngineMaker.Value = EngineMakerid.ToString();
        DataTable dt3 = EngineMaker.selectDataEngineMakerDetailsByEngineMakerId(EngineMakerid);
        foreach (DataRow dr in dt3.Rows)
        {
            txtEngineMakerName.Text=dr["EngineMakerName"].ToString();
            ddlEngineMakerType.SelectedValue=dr["EngineMakerType"].ToString();
            txtCreatedBy_EngineMaker.Text = dr["CreatedBy"].ToString();
            txtCreatedOn_EngineMaker.Text = dr["CreatedOn"].ToString();
            txtModifiedBy_EngineMaker.Text = dr["ModifiedBy"].ToString();
            txtModifiedOn_EngineMaker.Text = dr["ModifiedOn"].ToString();
            ddlStatus_EngineMaker.SelectedValue = dr["StatusId"].ToString();
        }
       
    }
   
    protected void GridView_EngineMaker_SelectedIndexChanged(object sender, EventArgs e)
    {
        HiddenField hfdEngineMaker;
        hfdEngineMaker = (HiddenField)GridView_EngineMaker.Rows[GridView_EngineMaker.SelectedIndex].FindControl("HiddenEngineMakerId");
        id = Convert.ToInt32(hfdEngineMaker.Value.ToString());
        Show_Record_EngineMaker(id);
        Alerts.ShowPanel(pnl_EngineMaker);
        Alerts.HANDLE_AUTHORITY(4, btn_Add_EngineMaker, btn_Save_EngineMaker, btn_Cancel_EngineMaker, btn_Print_EngineMaker, Auth);     
  
    }
    
    protected void GridView_EngineMaker_Row_Editing(object sender, GridViewEditEventArgs e)
    {
        Mode = "Edit";
        HiddenField hfdEngineMaker;
        hfdEngineMaker = (HiddenField)GridView_EngineMaker.Rows[e.NewEditIndex].FindControl("HiddenEngineMakerId");
        id = Convert.ToInt32(hfdEngineMaker.Value.ToString());
        Show_Record_EngineMaker(id);
        GridView_EngineMaker.SelectedIndex = e.NewEditIndex;
        Alerts.ShowPanel(pnl_EngineMaker);
        Alerts.HANDLE_AUTHORITY(5, btn_Add_EngineMaker, btn_Save_EngineMaker, btn_Cancel_EngineMaker, btn_Print_EngineMaker, Auth);     
     
    }
   
    protected void GridView_EngineMaker_Row_Deleting(object sender, GridViewDeleteEventArgs e)
    {
        int ModifiedBy = Convert.ToInt32(Session["loginid"].ToString());
        HiddenField hfdEngineMaker;
        hfdEngineMaker = (HiddenField)GridView_EngineMaker.Rows[e.RowIndex].FindControl("HiddenEngineMakerId");
        id = Convert.ToInt32(hfdEngineMaker.Value.ToString());
        EngineMaker.deleteEngineMakerDetailsById("DeleteEngineMakerDetailsById", id, ModifiedBy);
        bindEngineMakerGrid();
        if (HiddenEngineMaker.Value.ToString() == hfdEngineMaker.Value.ToString())
        {
            btn_Add_EngineMaker_Click(sender, e);
        }

    }
    protected void GridView_EngineMaker_PreRender(object sender, EventArgs e)
    {
        if (GridView_EngineMaker.Rows.Count <= 0) { lbl_GridView_EngineMaker.Text = "No Records Found..!"; }
    }

    protected void GridView_EngineMaker_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Modify")
        {
            GridViewRow row = (GridViewRow)(((ImageButton)sender).NamingContainer);
            int Rowindx = row.RowIndex;
            Mode = "Edit";
            HiddenField hfdEngineMaker;
            hfdEngineMaker = (HiddenField)GridView_EngineMaker.Rows[Rowindx].FindControl("hdnEngineMakerId");
            id = Convert.ToInt32(hfdEngineMaker.Value.ToString());
            Show_Record_EngineMaker(id);
            GridView_EngineMaker.SelectedIndex = Rowindx;
            Alerts.ShowPanel(pnl_EngineMaker);
            Alerts.HANDLE_AUTHORITY(5, btn_Add_EngineMaker, btn_Save_EngineMaker, btn_Cancel_EngineMaker, btn_Print_EngineMaker, Auth);
        }
        }

    protected void btnEditEngineMaker_click(object sender, ImageClickEventArgs e)
    {
        GridViewRow row = (GridViewRow)(((ImageButton)sender).NamingContainer);
        int Rowindx = row.RowIndex;
        Mode = "Edit";
        HiddenField hfdEngineMaker;
        hfdEngineMaker = (HiddenField)GridView_EngineMaker.Rows[Rowindx].FindControl("hdnEngineMakerId");
        id = Convert.ToInt32(hfdEngineMaker.Value.ToString());
        Show_Record_EngineMaker(id);
        GridView_EngineMaker.SelectedIndex = Rowindx;
        Alerts.ShowPanel(pnl_EngineMaker);
        Alerts.HANDLE_AUTHORITY(5, btn_Add_EngineMaker, btn_Save_EngineMaker, btn_Cancel_EngineMaker, btn_Print_EngineMaker, Auth);
    }
}
