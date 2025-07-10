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

public partial class Registers_RecruitingOffice : System.Web.UI.Page
{
    int id;
    Authority Auth;
    string Mode = "New";
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        lbl_RecruitingOffice_Message.Text = "";
        lbl_GridView_RecruitingOffice.Text = "";
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
            bindRecruitingOfficeGrid();
            bindStatusDDL();
            Alerts.HidePanel(pnl_RecruitingOffice);
            Alerts.HANDLE_AUTHORITY(1, btn_Add_RecruitingOffice, btn_Save_RecruitingOffice, btn_Cancel_RecruitingOffice, btn_Print_RecruitingOffice, Auth);
       
        }
    }
   
    public void bindRecruitingOfficeGrid()
    {
        DataTable dt1 = RecruitingOffice.selectDataRecruitingOfficeDetails();
        this.GridView_RecruitingOffice.DataSource = dt1;
        this.GridView_RecruitingOffice.DataBind();
    }
    public void bindStatusDDL()
    {
        DataTable dt2 = RecruitingOffice.selectDataStatusDetails();
        this.ddlStatus_RecruitingOffice.DataValueField = "StatusId";
        this.ddlStatus_RecruitingOffice.DataTextField = "StatusName";
        this.ddlStatus_RecruitingOffice.DataSource = dt2;
        this.ddlStatus_RecruitingOffice.DataBind();
    }
    protected void btn_Add_RecruitingOffice_Click(object sender, EventArgs e)
    {
        txtRecruitingOfficeName.Text = "";
        txtCreatedBy_RecruitingOffice.Text = "";
        txtCreatedOn_RecruitingOffice.Text = "";
        txtModifiedBy_RecruitingOffice.Text = "";
        txtModifiedOn_RecruitingOffice.Text = "";
        ddlStatus_RecruitingOffice.SelectedIndex = 0;
        GridView_RecruitingOffice.SelectedIndex = -1;
        HiddenRecruitingOffice.Value = "";
        Alerts.ShowPanel(pnl_RecruitingOffice);
        Alerts.HANDLE_AUTHORITY(2, btn_Add_RecruitingOffice, btn_Save_RecruitingOffice, btn_Cancel_RecruitingOffice, btn_Print_RecruitingOffice, Auth);    
 
    }
    protected void btn_Print_RecruitingOffice_Click(object sender, EventArgs e)
    {

    }
    protected void btn_Save_RecruitingOffice_Click(object sender, EventArgs e)
    {
        int Duplicate=0;

        foreach (GridViewRow dg in GridView_RecruitingOffice.Rows)
            {
                HiddenField hfd;
                HiddenField hfd1;
                hfd = (HiddenField)dg.FindControl("HiddenRecruitingOfficeName");
                hfd1 = (HiddenField)dg.FindControl("HiddenRecruitingOfficeId");

                if (hfd.Value.ToString().ToUpper().Trim() == txtRecruitingOfficeName.Text.ToUpper().Trim())
                {
                    if (HiddenRecruitingOffice.Value.Trim() == "")
                    {

                        lbl_RecruitingOffice_Message.Text = "Already Entered.";
                        Duplicate = 1;
                        break;
                    }
                    else if (HiddenRecruitingOffice.Value.Trim() != hfd1.Value.ToString())
                    {
                      
                        lbl_RecruitingOffice_Message.Text = "Already Entered.";
                        Duplicate = 1;
                        break;
                    }
                }
                else
                {
                    
                    lbl_RecruitingOffice_Message.Text = "";
                }
            }
            if (Duplicate == 0)
            {
                int intRecruitingOfficeId = -1;
                int intCreatedBy = 0;
                int intModifiedBy = 0;
                if (HiddenRecruitingOffice.Value.ToString().Trim() == "")
                {
                    intCreatedBy = Convert.ToInt32(Session["loginid"].ToString());
                }
                else
                {
                    intRecruitingOfficeId = Convert.ToInt32(HiddenRecruitingOffice.Value);
                    intModifiedBy = Convert.ToInt32(Session["loginid"].ToString());
                }
                string strRecruitingOfficeName = txtRecruitingOfficeName.Text;
                char charStatusId = Convert.ToChar(ddlStatus_RecruitingOffice.SelectedValue);

                RecruitingOffice.insertUpdateRecruitingOfficeDetails("InsertUpdateRecruitingOfficeDetails",
                                              intRecruitingOfficeId,
                                              strRecruitingOfficeName,
                                              intCreatedBy,
                                              intModifiedBy,
                                              charStatusId);

                bindRecruitingOfficeGrid();

            
                lbl_RecruitingOffice_Message.Text = "Record Successfully Saved.";
                Alerts.HidePanel(pnl_RecruitingOffice);
                Alerts.HANDLE_AUTHORITY(3, btn_Add_RecruitingOffice, btn_Save_RecruitingOffice, btn_Cancel_RecruitingOffice, btn_Print_RecruitingOffice, Auth);    
           
            }
       
    }
    protected void btn_Cancel_RecruitingOffice_Click(object sender, EventArgs e)
    {
        
        GridView_RecruitingOffice.SelectedIndex = -1;
        Alerts.HidePanel(pnl_RecruitingOffice);
        Alerts.HANDLE_AUTHORITY(6, btn_Add_RecruitingOffice, btn_Save_RecruitingOffice, btn_Cancel_RecruitingOffice, btn_Print_RecruitingOffice, Auth);     
   
    }
    protected void GridView_RecruitingOffice_DataBound(object sender, EventArgs e)
    {
        Alerts.HANDLE_GRID(GridView_RecruitingOffice, Auth);  
    }
    protected void Show_Record_RecruitingOffice(int RecruitingOfficeid)
    {

        HiddenRecruitingOffice.Value = RecruitingOfficeid.ToString();
        DataTable dt3 = RecruitingOffice.selectDataRecruitingOfficeDetailsByRecruitingOfficeId(RecruitingOfficeid);
        foreach (DataRow dr in dt3.Rows)
        {
            txtRecruitingOfficeName.Text = dr["RecruitingOfficeName"].ToString();
            txtCreatedBy_RecruitingOffice.Text = dr["CreatedBy"].ToString();
            txtCreatedOn_RecruitingOffice.Text = dr["CreatedOn"].ToString();
            txtModifiedBy_RecruitingOffice.Text = dr["ModifiedBy"].ToString();
            txtModifiedOn_RecruitingOffice.Text = dr["ModifiedOn"].ToString();
            ddlStatus_RecruitingOffice.SelectedValue = dr["StatusId"].ToString();
        }

    }
    // VIEW THE RECORD
    protected void GridView_RecruitingOffice_SelectedIndexChanged(object sender, EventArgs e)
    {
       
        HiddenField hfdRecruitingOffice;
        hfdRecruitingOffice = (HiddenField)GridView_RecruitingOffice.Rows[GridView_RecruitingOffice.SelectedIndex].FindControl("HiddenRecruitingOfficeId");
        id = Convert.ToInt32(hfdRecruitingOffice.Value.ToString());
        Show_Record_RecruitingOffice(id);
        Alerts.ShowPanel(pnl_RecruitingOffice);
        Alerts.HANDLE_AUTHORITY(4, btn_Add_RecruitingOffice, btn_Save_RecruitingOffice, btn_Cancel_RecruitingOffice, btn_Print_RecruitingOffice, Auth);     
  
    }
    //EDIT THE RECORD
    protected void GridView_RecruitingOffice_Row_Editing(object sender, GridViewEditEventArgs e)
    {
        Mode = "Edit";
        HiddenField hfdRecruitingOffice;
        hfdRecruitingOffice = (HiddenField)GridView_RecruitingOffice.Rows[e.NewEditIndex].FindControl("HiddenRecruitingOfficeId");
        id = Convert.ToInt32(hfdRecruitingOffice.Value.ToString());
        Show_Record_RecruitingOffice(id);
        GridView_RecruitingOffice.SelectedIndex = e.NewEditIndex;
        Alerts.ShowPanel(pnl_RecruitingOffice);
        Alerts.HANDLE_AUTHORITY(5, btn_Add_RecruitingOffice, btn_Save_RecruitingOffice, btn_Cancel_RecruitingOffice, btn_Print_RecruitingOffice, Auth);     
     
    }
    // DELETE THE RECORD
    protected void GridView_RecruitingOffice_Row_Deleting(object sender, GridViewDeleteEventArgs e)
    {
        int intModifiedBy = Convert.ToInt32(Session["loginid"].ToString());
        HiddenField hfdRecruitingOffice;
        hfdRecruitingOffice = (HiddenField)GridView_RecruitingOffice.Rows[e.RowIndex].FindControl("HiddenRecruitingOfficeId");
        id = Convert.ToInt32(hfdRecruitingOffice.Value.ToString());
        RecruitingOffice.deleteRecruitingOfficeDetailsById("DeleteRecruitingOfficeById", id, intModifiedBy);
        bindRecruitingOfficeGrid();
        if (HiddenRecruitingOffice.Value.ToString() == hfdRecruitingOffice.Value.ToString())
        {
            btn_Add_RecruitingOffice_Click(sender, e);
        }
     
    }
    protected void GridView_RecruitingOffice_PreRender(object sender, EventArgs e)
    {
        if (GridView_RecruitingOffice.Rows.Count <= 0) { lbl_GridView_RecruitingOffice.Text = "No Records Found..!"; }
    }

    protected void GridView_RecruitingOffice_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Modify")
        {
            GridViewRow row = (GridViewRow)(((ImageButton)sender).NamingContainer);
            int Rowindx = row.RowIndex;
            Mode = "Edit";
            HiddenField hfdRecruitingOffice;
            hfdRecruitingOffice = (HiddenField)GridView_RecruitingOffice.Rows[Rowindx].FindControl("hdnRecruitingOfficeId");
            id = Convert.ToInt32(hfdRecruitingOffice.Value.ToString());
            Show_Record_RecruitingOffice(id);
            GridView_RecruitingOffice.SelectedIndex = Rowindx;
            Alerts.ShowPanel(pnl_RecruitingOffice);
            Alerts.HANDLE_AUTHORITY(5, btn_Add_RecruitingOffice, btn_Save_RecruitingOffice, btn_Cancel_RecruitingOffice, btn_Print_RecruitingOffice, Auth);
        }
    }

    protected void btnEditRecruitingOffice_click(object sender, ImageClickEventArgs e)
    {
        GridViewRow row = (GridViewRow)(((ImageButton)sender).NamingContainer);
        int Rowindx = row.RowIndex;
        Mode = "Edit";
        HiddenField hfdRecruitingOffice;
        hfdRecruitingOffice = (HiddenField)GridView_RecruitingOffice.Rows[Rowindx].FindControl("hdnRecruitingOfficeId");
        id = Convert.ToInt32(hfdRecruitingOffice.Value.ToString());
        Show_Record_RecruitingOffice(id);
        GridView_RecruitingOffice.SelectedIndex = Rowindx;
        Alerts.ShowPanel(pnl_RecruitingOffice);
        Alerts.HANDLE_AUTHORITY(5, btn_Add_RecruitingOffice, btn_Save_RecruitingOffice, btn_Cancel_RecruitingOffice, btn_Print_RecruitingOffice, Auth);
    }
}
