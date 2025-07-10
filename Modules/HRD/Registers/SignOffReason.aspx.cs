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

public partial class Registers_SignOff_Reason : System.Web.UI.Page
{
    int id;
    Authority Auth;
    string Mode = "New";
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        lbl_GridView_SignOffReason.Text = "";
        lbl_SignOffReason_Message.Text = "";
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
            bindSignOffReasonGrid();
            bindStatusDDL();
            Alerts.HidePanel(pnl_SignOffReason);
            Alerts.HANDLE_AUTHORITY(1, btn_Add_SignOffReason, btn_Save_SignOffReason, btn_Cancel_SignOffReason, btn_Print_SignOffReason, Auth);
        }
    }
    public void bindSignOffReasonGrid()
    {
        DataTable dt1 = SignOffReason.selectDataSignOffReasonDetails();
        this.GridView_SignOffReason.DataSource = dt1;
        this.GridView_SignOffReason.DataBind();
    }
    public void bindStatusDDL()
    {
        DataTable dt2 = SignOffReason.selectDataStatusDetails();
        this.ddlStatus_SignOffReason.DataValueField = "StatusId";
        this.ddlStatus_SignOffReason.DataTextField = "StatusName";
        this.ddlStatus_SignOffReason.DataSource = dt2;
        this.ddlStatus_SignOffReason.DataBind();
    }
    protected void btn_Add_SignOffReason_Click(object sender, EventArgs e)
    {
        txtSignOffReason.Text = "";
        txtSignOffReasonAbbreviation.Text = "";
        txtCreatedBy_SignOffReason.Text = "";
        txtCreatedOn_SignOffReason.Text = "";
        txtModifiedBy_SignOffReason.Text = "";
        txtModifiedOn_SignOffReason.Text = "";
        ddlStatus_SignOffReason.SelectedIndex = 0;
        GridView_SignOffReason.SelectedIndex = -1;
        HiddenSignOffReason.Value = "";
       
        Alerts.ShowPanel(pnl_SignOffReason);
        Alerts.HANDLE_AUTHORITY(2, btn_Add_SignOffReason, btn_Save_SignOffReason, btn_Cancel_SignOffReason, btn_Print_SignOffReason, Auth);
    }
    protected void btn_Save_SignOffReason_Click(object sender, EventArgs e)
    {
        int Duplicate=0;

        foreach (GridViewRow dg in GridView_SignOffReason.Rows)
            {
                HiddenField hfd;
                HiddenField hfd1;
                hfd = (HiddenField)dg.FindControl("HiddenSignOffReasonName");
                hfd1 = (HiddenField)dg.FindControl("HiddenSignOffReasonId");

                if (hfd.Value.ToString().ToUpper().Trim() == txtSignOffReason.Text.ToUpper().Trim())
                {
                    if (HiddenSignOffReason.Value.Trim() == "")
                    {
                        lbl_SignOffReason_Message.Text = "Already Entered.";
                        Duplicate = 1;
                        break;
                    }
                    else if (HiddenSignOffReason.Value.Trim() != hfd1.Value.ToString())
                    {
                        lbl_SignOffReason_Message.Text = "Already Entered.";
                        Duplicate = 1;
                        break;
                    }
                }
                else
                {
                    lbl_SignOffReason_Message.Text = "";
                }
            }
            if (Duplicate == 0)
            {
                int intSignOffReasonId = -1;
                int intCreatedBy = 0;
                int intModifiedBy = 0;
                if (HiddenSignOffReason.Value.ToString().Trim() == "")
                {
                    intCreatedBy = Convert.ToInt32(Session["loginid"].ToString());
                }
                else
                {
                    intSignOffReasonId = Convert.ToInt32(HiddenSignOffReason.Value);
                    intModifiedBy = Convert.ToInt32(Session["loginid"].ToString());
                }
                string strSignOffReason = txtSignOffReason.Text;
                string strSignOffReasonAggreviation = txtSignOffReasonAbbreviation.Text; ;
                char charStatusId = Convert.ToChar(ddlStatus_SignOffReason.SelectedValue);

                SignOffReason.insertUpdateSignOffReasonDetails("InsertUpdateSignOffReasonDetails",
                                              intSignOffReasonId,
                                              strSignOffReason,
                                              strSignOffReasonAggreviation,
                                              intCreatedBy,
                                              intModifiedBy,
                                              charStatusId);

                bindSignOffReasonGrid();
                lbl_SignOffReason_Message.Text = "Record Successfully Saved.";
               
                Alerts.HidePanel(pnl_SignOffReason);
                Alerts.HANDLE_AUTHORITY(3, btn_Add_SignOffReason, btn_Save_SignOffReason, btn_Cancel_SignOffReason, btn_Print_SignOffReason, Auth);
            }
    }
    protected void btn_Cancel_SignOffReason_Click(object sender, EventArgs e)
    {
        GridView_SignOffReason.SelectedIndex = -1;
       
        Alerts.HidePanel(pnl_SignOffReason);
        Alerts.HANDLE_AUTHORITY(6, btn_Add_SignOffReason, btn_Save_SignOffReason, btn_Cancel_SignOffReason, btn_Print_SignOffReason, Auth);
    }
    protected void btn_Print_SignOffReason_Click(object sender, EventArgs e)
    {

    }
    protected void GridView_SignOffReason_DataBound(object sender, EventArgs e)
    {
        Alerts.HANDLE_GRID(GridView_SignOffReason, Auth);
    }
    protected void Show_Record_SignOffReason(int signoffreasonid)
    {
        HiddenSignOffReason.Value = signoffreasonid.ToString();
        DataTable dt3 = SignOffReason.selectDataSignOffReasonDetailsBySignOffReasonId(signoffreasonid);
        foreach (DataRow dr in dt3.Rows)
        {
            txtSignOffReason.Text = dr["SignOffReason"].ToString();
            txtSignOffReasonAbbreviation.Text = dr["SignOffReasonAbbr"].ToString();
            txtCreatedBy_SignOffReason.Text = dr["CreatedBy"].ToString();
            txtCreatedOn_SignOffReason.Text = dr["CreatedOn"].ToString();
            txtModifiedBy_SignOffReason.Text = dr["ModifiedBy"].ToString();
            txtModifiedOn_SignOffReason.Text = dr["ModifiedOn"].ToString();
            ddlStatus_SignOffReason.SelectedValue = dr["StatusId"].ToString();
        }
    }
    // VIEW THE RECORD
    protected void GridView_SignOffReason_SelectedIndexChanged(object sender, EventArgs e)
    {
        HiddenField hfdSignOffReason;
        hfdSignOffReason = (HiddenField)GridView_SignOffReason.Rows[GridView_SignOffReason.SelectedIndex].FindControl("HiddenSignOffReasonId");
        id = Convert.ToInt32(hfdSignOffReason.Value.ToString());
        Show_Record_SignOffReason(id);
       
        Alerts.ShowPanel(pnl_SignOffReason);
        Alerts.HANDLE_AUTHORITY(4, btn_Add_SignOffReason, btn_Save_SignOffReason, btn_Cancel_SignOffReason, btn_Print_SignOffReason, Auth);
    }
    //EDIT THE RECORD
    protected void GridView_SignOffReason_Row_Editing(object sender, GridViewEditEventArgs e)
    {
        HiddenField hfdSignOffReason;
        hfdSignOffReason = (HiddenField)GridView_SignOffReason.Rows[e.NewEditIndex].FindControl("HiddenSignOffReasonId");
        id = Convert.ToInt32(hfdSignOffReason.Value.ToString());
        Show_Record_SignOffReason(id);
        GridView_SignOffReason.SelectedIndex = e.NewEditIndex;
       
        Alerts.ShowPanel(pnl_SignOffReason);
        Alerts.HANDLE_AUTHORITY(5, btn_Add_SignOffReason, btn_Save_SignOffReason, btn_Cancel_SignOffReason, btn_Print_SignOffReason, Auth);
    }
    // DELETE THE RECORD
    protected void GridView_SignOffReason_Row_Deleting(object sender, GridViewDeleteEventArgs e)
    {
        int intModifiedBy = Convert.ToInt32(Session["loginid"].ToString());
        HiddenField hfdSignOffReason;
        hfdSignOffReason = (HiddenField)GridView_SignOffReason.Rows[e.RowIndex].FindControl("HiddenSignOffReasonId");
        id = Convert.ToInt32(hfdSignOffReason.Value.ToString());
        SignOffReason.deleteSignOffReasonDetailsById("DeleteSignOffReasonDetailsById", id, intModifiedBy);
        bindSignOffReasonGrid();
        if (HiddenSignOffReason.Value.ToString() == hfdSignOffReason.Value.ToString())
        {
            btn_Add_SignOffReason_Click(sender, e);
        }
    }
    protected void GridView_SignOffReason_PreRender(object sender, EventArgs e)
    {
        if (GridView_SignOffReason.Rows.Count <= 0) { lbl_GridView_SignOffReason.Text = "No Records Found..!"; }
    }

    protected void GridView_SignOffReason_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Modify")
        {
            GridViewRow row = (GridViewRow)(((ImageButton)sender).NamingContainer);
            int Rowindx = row.RowIndex;
            Mode = "Edit";
            HiddenField hfdSignOffReason;
            hfdSignOffReason = (HiddenField)GridView_SignOffReason.Rows[Rowindx].FindControl("hdnSignOffReasonId");
            id = Convert.ToInt32(hfdSignOffReason.Value.ToString());
            Show_Record_SignOffReason(id);
            GridView_SignOffReason.SelectedIndex = Rowindx;

            Alerts.ShowPanel(pnl_SignOffReason);
            Alerts.HANDLE_AUTHORITY(5, btn_Add_SignOffReason, btn_Save_SignOffReason, btn_Cancel_SignOffReason, btn_Print_SignOffReason, Auth);
        }
        }
    protected void btnEditSignOffReason_click(object sender, ImageClickEventArgs e)
    {
        GridViewRow row = (GridViewRow)(((ImageButton)sender).NamingContainer);
        int Rowindx = row.RowIndex;
        Mode = "Edit";
        HiddenField hfdSignOffReason;
        hfdSignOffReason = (HiddenField)GridView_SignOffReason.Rows[Rowindx].FindControl("hdnSignOffReasonId");
        id = Convert.ToInt32(hfdSignOffReason.Value.ToString());
        Show_Record_SignOffReason(id);
        GridView_SignOffReason.SelectedIndex = Rowindx;

        Alerts.ShowPanel(pnl_SignOffReason);
        Alerts.HANDLE_AUTHORITY(5, btn_Add_SignOffReason, btn_Save_SignOffReason, btn_Cancel_SignOffReason, btn_Print_SignOffReason, Auth);
    }
}