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

public partial class Registers_CurrentClass : System.Web.UI.Page
{
    int id;
    Authority Auth;
    string Mode = "New";
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        lbl_CurrentClass_Message.Text = "";
        lbl_GridView_CurrentClass.Text = "";
       
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
            bindCurrentClassGrid();
            bindStatusDDL();
            Alerts.HidePanel(pnl_CurrentClass);
            Alerts.HANDLE_AUTHORITY(1, btn_Add_CurrentClass, btn_Save_CurrentClass, btn_Cancel_CurrentClass, btn_Print_CurrentClass, Auth);
       
        }
    }
   
    public void bindCurrentClassGrid()
    {
        DataTable dt1 = CurrentClass.selectDataCurrentClassDetails();
        this.GridView_CurrentClass.DataSource = dt1;
        this.GridView_CurrentClass.DataBind();
    }
    public void bindStatusDDL()
    {
        DataTable dt2 = CurrentClass.selectDataStatusDetails();
        this.ddlStatus_CurrentClass.DataValueField = "StatusId";
        this.ddlStatus_CurrentClass.DataTextField = "StatusName";
        this.ddlStatus_CurrentClass.DataSource = dt2;
        this.ddlStatus_CurrentClass.DataBind();
    }
    protected void btn_Add_CurrentClass_Click(object sender, EventArgs e)
    {
        txtCurrentClassName.Text = "";
        txtCreatedBy_CurrentClass.Text = "";
        txtCreatedOn_CurrentClass.Text = "";
        txtModifiedBy_CurrentClass.Text = "";
        txtModifiedOn_CurrentClass.Text = "";
        ddlStatus_CurrentClass.SelectedIndex = 0;
        GridView_CurrentClass.SelectedIndex = -1;
        HiddenCurrentClass.Value = "";
        Alerts.ShowPanel(pnl_CurrentClass);
        Alerts.HANDLE_AUTHORITY(2, btn_Add_CurrentClass, btn_Save_CurrentClass, btn_Cancel_CurrentClass, btn_Print_CurrentClass, Auth);    
 
    }
    protected void btn_Save_CurrentClass_Click(object sender, EventArgs e)
    {
        int Duplicate=0;
        
            foreach (GridViewRow dg in GridView_CurrentClass.Rows)
            {
                HiddenField hfd;
                HiddenField hfd1;
                hfd = (HiddenField)dg.FindControl("HiddenCurrentClassName");
                hfd1 = (HiddenField)dg.FindControl("HiddenCurrentClassId");

                if (hfd.Value.ToString().ToUpper().Trim() == txtCurrentClassName.Text.ToUpper().Trim())
                {
                    if (HiddenCurrentClass.Value.Trim() == "")
                    {
                        lbl_CurrentClass_Message.Text = "ALready Entered.";
                        Duplicate = 1;
                        break;
                    }
                    else if (HiddenCurrentClass.Value.Trim() != hfd1.Value.ToString())
                    {
                        lbl_CurrentClass_Message.Text = "ALready Entered.";
                        Duplicate = 1;
                        break;
                    }
                }
                else
                {
                    lbl_CurrentClass_Message.Text = "";
                }
            }
            if (Duplicate == 0)
            {
                int intCurrentClassId = -1;
                int intCreatedBy = 0;
                int intModifiedBy = 0;

                if (HiddenCurrentClass.Value.ToString().Trim() == "")
                {
                    intCreatedBy = Convert.ToInt32(Session["loginid"].ToString());
                }
                else
                {
                    intCurrentClassId = Convert.ToInt32(HiddenCurrentClass.Value);
                    intModifiedBy = Convert.ToInt32(Session["loginid"].ToString());
                }
                string strCurrentClassName = txtCurrentClassName.Text;
                char charStatusId = Convert.ToChar(ddlStatus_CurrentClass.SelectedValue);

                CurrentClass.insertUpdateCurrentClassDetails("InsertUpdateCurrentClassDetails",
                                              intCurrentClassId,
                                              strCurrentClassName,
                                              intCreatedBy,
                                              intModifiedBy,
                                              charStatusId);

                bindCurrentClassGrid();
                lbl_CurrentClass_Message.Text = "Record Successfully Saved.";
                Alerts.HidePanel(pnl_CurrentClass);
                Alerts.HANDLE_AUTHORITY(3, btn_Add_CurrentClass, btn_Save_CurrentClass, btn_Cancel_CurrentClass, btn_Print_CurrentClass, Auth);    
           
            }
    }
    protected void btn_Cancel_CurrentClass_Click(object sender, EventArgs e)
    {
       
        GridView_CurrentClass.SelectedIndex = -1;
        Alerts.HidePanel(pnl_CurrentClass);
        Alerts.HANDLE_AUTHORITY(6, btn_Add_CurrentClass, btn_Save_CurrentClass, btn_Cancel_CurrentClass, btn_Print_CurrentClass, Auth);     
   
    }
    protected void btn_Print_CurrentClass_Click(object sender, EventArgs e)
    {

    }
    protected void GridView_CurrentClass_DataBound(object sender, EventArgs e)
    {
        Alerts.HANDLE_GRID(GridView_CurrentClass, Auth);  
    }
    protected void Show_Record_CurrentClass(int CurrentClassid)
    {
        HiddenCurrentClass.Value = CurrentClassid.ToString();
        DataTable dt3 = CurrentClass.selectDataCurrentClassDetailsByCurrentClassId(CurrentClassid);
        foreach (DataRow dr in dt3.Rows)
        {
            txtCurrentClassName.Text = dr["CurrentClassName"].ToString();
            txtCreatedBy_CurrentClass.Text = dr["CreatedBy"].ToString();
            txtCreatedOn_CurrentClass.Text = dr["CreatedOn"].ToString();
            txtModifiedBy_CurrentClass.Text = dr["ModifiedBy"].ToString();
            txtModifiedOn_CurrentClass.Text = dr["ModifiedOn"].ToString();
            ddlStatus_CurrentClass.SelectedValue = dr["StatusId"].ToString();
        }

    }
 
    protected void GridView_CurrentClass_SelectedIndexChanged(object sender, EventArgs e)
    {

        HiddenField hfdCurrentClass;
        hfdCurrentClass = (HiddenField)GridView_CurrentClass.Rows[GridView_CurrentClass.SelectedIndex].FindControl("HiddenCurrentClassId");
        id = Convert.ToInt32(hfdCurrentClass.Value.ToString());
        Show_Record_CurrentClass(id);
        Alerts.ShowPanel(pnl_CurrentClass);
        Alerts.HANDLE_AUTHORITY(4, btn_Add_CurrentClass, btn_Save_CurrentClass, btn_Cancel_CurrentClass, btn_Print_CurrentClass, Auth);     
  
    }
   
    protected void GridView_CurrentClass_Row_Editing(object sender, GridViewEditEventArgs e)
    {
        Mode = "Edit";
        HiddenField hfdCurrentClass;
        hfdCurrentClass = (HiddenField)GridView_CurrentClass.Rows[e.NewEditIndex].FindControl("HiddenCurrentClassId");
        id = Convert.ToInt32(hfdCurrentClass.Value.ToString());
        Show_Record_CurrentClass(id);
        GridView_CurrentClass.SelectedIndex = e.NewEditIndex;
        Alerts.ShowPanel(pnl_CurrentClass);
        Alerts.HANDLE_AUTHORITY(5, btn_Add_CurrentClass, btn_Save_CurrentClass, btn_Cancel_CurrentClass, btn_Print_CurrentClass, Auth);     
     
    }
   
    protected void GridView_CurrentClass_Row_Deleting(object sender, GridViewDeleteEventArgs e)
    {
        int intModifiedBy = Convert.ToInt32(Session["loginid"].ToString());
        HiddenField hfdCurrentClass;
        hfdCurrentClass = (HiddenField)GridView_CurrentClass.Rows[e.RowIndex].FindControl("HiddenCurrentClassId");
        id = Convert.ToInt32(hfdCurrentClass.Value.ToString());
        CurrentClass.deleteCurrentClassDetailsById("DeleteCurrentClassDetailsById", id, intModifiedBy);
        bindCurrentClassGrid();
        if (HiddenCurrentClass.Value.ToString() == hfdCurrentClass.Value.ToString())
        {
            btn_Add_CurrentClass_Click(sender, e);
        }
    }
    protected void GridView_CurrentClass_PreRender(object sender, EventArgs e)
    {
        if (GridView_CurrentClass.Rows.Count <= 0) { lbl_GridView_CurrentClass.Text = "No Records Found..!"; }
    }

    protected void GridView_CurrentClass_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Modify")
        {
            GridViewRow row = (GridViewRow)(((ImageButton)sender).NamingContainer);
            int Rowindx = row.RowIndex;
            Mode = "Edit";
            HiddenField hfdCurrentClass;
            hfdCurrentClass = (HiddenField)GridView_CurrentClass.Rows[Rowindx].FindControl("hdnCurrentClassId");
            id = Convert.ToInt32(hfdCurrentClass.Value.ToString());
            Show_Record_CurrentClass(id);
            GridView_CurrentClass.SelectedIndex = Rowindx;
            Alerts.ShowPanel(pnl_CurrentClass);
            Alerts.HANDLE_AUTHORITY(5, btn_Add_CurrentClass, btn_Save_CurrentClass, btn_Cancel_CurrentClass, btn_Print_CurrentClass, Auth);
        }
    }

    protected void btnEditCurrentClass_click(object sender, ImageClickEventArgs e)
    {
        GridViewRow row = (GridViewRow)(((ImageButton)sender).NamingContainer);
        int Rowindx = row.RowIndex;
        Mode = "Edit";
        HiddenField hfdCurrentClass;
        hfdCurrentClass = (HiddenField)GridView_CurrentClass.Rows[Rowindx].FindControl("hdnCurrentClassId");
        id = Convert.ToInt32(hfdCurrentClass.Value.ToString());
        Show_Record_CurrentClass(id);
        GridView_CurrentClass.SelectedIndex = Rowindx;
        Alerts.ShowPanel(pnl_CurrentClass);
        Alerts.HANDLE_AUTHORITY(5, btn_Add_CurrentClass, btn_Save_CurrentClass, btn_Cancel_CurrentClass, btn_Print_CurrentClass, Auth);
    }
    }
