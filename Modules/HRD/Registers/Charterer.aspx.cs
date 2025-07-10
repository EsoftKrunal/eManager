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

public partial class Registers_Charterer : System.Web.UI.Page
{
    int id;
    Authority Auth;
    string Mode = "New";
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        lbl_Charterer_Message.Text = "";
        lbl_GridView_Charterer.Text = "";
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
            bindChartererGrid();
            bindStatusDDL();
            Alerts.HidePanel(pnl_Charterer);
            Alerts.HANDLE_AUTHORITY(1, btn_Add_Charterer, btn_Save_Charterer, btn_Cancel_Charterer, btn_Print_Charterer, Auth);
       
        }
    }
   
    public void bindChartererGrid()
    {
        DataTable dt1 = Charterer.selectDataChartererDetails();
        this.GridView_Charterer.DataSource = dt1;
        this.GridView_Charterer.DataBind();
    }
    public void bindStatusDDL()
    {
        DataTable dt2 = Charterer.selectDataStatusDetails();
        this.ddlStatus_Charterer.DataValueField = "StatusId";
        this.ddlStatus_Charterer.DataTextField = "StatusName";
        this.ddlStatus_Charterer.DataSource = dt2;
        this.ddlStatus_Charterer.DataBind();
    }
    protected void btn_Add_Charterer_Click(object sender, EventArgs e)
    {
        txtChartererName.Text = "";
        txtCreatedBy_Charterer.Text = "";
        txtCreatedOn_Charterer.Text = "";
        txtModifiedBy_Charterer.Text = "";
        txtModifiedOn_Charterer.Text = "";
        ddlStatus_Charterer.SelectedIndex = 0;
        GridView_Charterer.SelectedIndex = -1;
        HiddenCharterer.Value = "";
        Alerts.ShowPanel(pnl_Charterer);
        Alerts.HANDLE_AUTHORITY(2, btn_Add_Charterer, btn_Save_Charterer, btn_Cancel_Charterer, btn_Print_Charterer, Auth);    
 
    }
    protected void btn_Save_Charterer_Click(object sender, EventArgs e)
    {
        int Duplicate=0;
        
            foreach (GridViewRow dg in GridView_Charterer.Rows)
            {
                HiddenField hfd;
                HiddenField hfd1;
                hfd = (HiddenField)dg.FindControl("HiddenChartererName");
                hfd1 = (HiddenField)dg.FindControl("HiddenChartererId");

                if (hfd.Value.ToString().ToUpper().Trim() == txtChartererName.Text.ToUpper().Trim())
                {
                    if (HiddenCharterer.Value.Trim() == "")
                    {

                        lbl_Charterer_Message.Text = "Already Entered.";
                        Duplicate = 1;
                        break;
                    }
                    else if (HiddenCharterer.Value.Trim() != hfd1.Value.ToString())
                    {

                        lbl_Charterer_Message.Text = "Already Entered.";
                        Duplicate = 1;
                        break;
                    }
                }
                else
                {

                    lbl_Charterer_Message.Text = "";
                }
            }
            if (Duplicate == 0)
            {
                int intChartererId = -1;
                int intCreatedBy = 0;
                int intModifiedBy = 0;

                if (HiddenCharterer.Value.ToString().Trim() == "")
                {
                    intCreatedBy = Convert.ToInt32(Session["loginid"].ToString());
                }
                else
                {
                    intChartererId = Convert.ToInt32(HiddenCharterer.Value);
                    intModifiedBy = Convert.ToInt32(Session["loginid"].ToString());
                }
                string strChartererName = txtChartererName.Text;
                char charStatusId = Convert.ToChar(ddlStatus_Charterer.SelectedValue);

                Charterer.insertUpdateChartererDetails("InsertUpdateChartererDetails",
                                              intChartererId,
                                              strChartererName,
                                              intCreatedBy,
                                              intModifiedBy,
                                              charStatusId);

                bindChartererGrid();
                lbl_Charterer_Message.Text = "Record Successfully Saved.";
                Alerts.HidePanel(pnl_Charterer);
                Alerts.HANDLE_AUTHORITY(3, btn_Add_Charterer, btn_Save_Charterer, btn_Cancel_Charterer, btn_Print_Charterer, Auth);    
           
            }
      
    }
    protected void btn_Cancel_Charterer_Click(object sender, EventArgs e)
    {
        
        GridView_Charterer.SelectedIndex = -1;
        Alerts.HidePanel(pnl_Charterer);
        Alerts.HANDLE_AUTHORITY(6, btn_Add_Charterer, btn_Save_Charterer, btn_Cancel_Charterer, btn_Print_Charterer, Auth);     
   
    }
    protected void btn_Print_Charterer_Click(object sender, EventArgs e)
    {

    }
    protected void GridView_Charterer_DataBound(object sender, EventArgs e)
    {
        Alerts.HANDLE_GRID(GridView_Charterer, Auth);  
    }
    protected void Show_Record_Charterer(int Chartererid)
    {

        HiddenCharterer.Value = Chartererid.ToString();
        DataTable dt3 = Charterer.selectDataChartererDetailsByChartererId(Chartererid);
        foreach (DataRow dr in dt3.Rows)
        {
            txtChartererName.Text = dr["ChartererName"].ToString();
            txtCreatedBy_Charterer.Text = dr["CreatedBy"].ToString();
            txtCreatedOn_Charterer.Text = dr["CreatedOn"].ToString();
            txtModifiedBy_Charterer.Text = dr["ModifiedBy"].ToString();
            txtModifiedOn_Charterer.Text = dr["ModifiedOn"].ToString();
            ddlStatus_Charterer.SelectedValue = dr["StatusId"].ToString();
        }

    }
    protected void GridView_Charterer_SelectedIndexChanged(object sender, EventArgs e)
    {

        HiddenField hfdCharterer;
        hfdCharterer = (HiddenField)GridView_Charterer.Rows[GridView_Charterer.SelectedIndex].FindControl("HiddenChartererId");
        id = Convert.ToInt32(hfdCharterer.Value.ToString());
        Show_Record_Charterer(id);
        Alerts.ShowPanel(pnl_Charterer);
        Alerts.HANDLE_AUTHORITY(4, btn_Add_Charterer, btn_Save_Charterer, btn_Cancel_Charterer, btn_Print_Charterer, Auth);     
  
    }
    protected void GridView_Charterer_Row_Editing(object sender, GridViewEditEventArgs e)
    {
        Mode = "Edit";
        HiddenField hfdCharterer;
        hfdCharterer = (HiddenField)GridView_Charterer.Rows[e.NewEditIndex].FindControl("HiddenChartererId");
        id = Convert.ToInt32(hfdCharterer.Value.ToString());
        Show_Record_Charterer(id);
        GridView_Charterer.SelectedIndex = e.NewEditIndex;
        Alerts.ShowPanel(pnl_Charterer);
        Alerts.HANDLE_AUTHORITY(5, btn_Add_Charterer, btn_Save_Charterer, btn_Cancel_Charterer, btn_Print_Charterer, Auth);     
     
    }
    protected void GridView_Charterer_Row_Deleting(object sender, GridViewDeleteEventArgs e)
    {
        int intModifiedBy = Convert.ToInt32(Session["loginid"].ToString());
        HiddenField hfdCharterer;
        hfdCharterer = (HiddenField)GridView_Charterer.Rows[e.RowIndex].FindControl("HiddenChartererId");
        id = Convert.ToInt32(hfdCharterer.Value.ToString());
        Charterer.deleteChartererDetailsById("DeleteChartererDetailsById", id, intModifiedBy);
        bindChartererGrid();
        if (HiddenCharterer.Value.ToString() == hfdCharterer.Value.ToString())
        {
            btn_Add_Charterer_Click(sender, e);
        }

    }
    protected void GridView_Charterer_PreRender(object sender, EventArgs e)
    {
        if (GridView_Charterer.Rows.Count <= 0) { lbl_GridView_Charterer.Text = "No Records Found..!"; }
    }

    protected void GridView_Charterer_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Modify")
        {
            GridViewRow row = (GridViewRow)(((ImageButton)sender).NamingContainer);
            int Rowindx = row.RowIndex;
            Mode = "Edit";
            HiddenField hfdCharterer;
            hfdCharterer = (HiddenField)GridView_Charterer.Rows[Rowindx].FindControl("hdnChartererId");
            id = Convert.ToInt32(hfdCharterer.Value.ToString());
            Show_Record_Charterer(id);
            GridView_Charterer.SelectedIndex = Rowindx;
            Alerts.ShowPanel(pnl_Charterer);
            Alerts.HANDLE_AUTHORITY(5, btn_Add_Charterer, btn_Save_Charterer, btn_Cancel_Charterer, btn_Print_Charterer, Auth);
        }
    }

    protected void btnEditCharterer_click(object sender, ImageClickEventArgs e)
    {
        GridViewRow row = (GridViewRow)(((ImageButton)sender).NamingContainer);
        int Rowindx = row.RowIndex;
        Mode = "Edit";
        HiddenField hfdCharterer;
        hfdCharterer = (HiddenField)GridView_Charterer.Rows[Rowindx].FindControl("hdnChartererId");
        id = Convert.ToInt32(hfdCharterer.Value.ToString());
        Show_Record_Charterer(id);
        GridView_Charterer.SelectedIndex = Rowindx;
        Alerts.ShowPanel(pnl_Charterer);
        Alerts.HANDLE_AUTHORITY(5, btn_Add_Charterer, btn_Save_Charterer, btn_Cancel_Charterer, btn_Print_Charterer, Auth);
    }
}
