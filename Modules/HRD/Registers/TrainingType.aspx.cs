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

public partial class Registers_TrainingType : System.Web.UI.Page
{
    int id;
    Authority Auth;
    AuthenticationManager Auth1;
    string Mode = "New";
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        lbl_TrainingType_Message.Text = "";
        lbl_GridView_TrainingType.Text = "";
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 136);
        if (chpageauth <= 0)
        {
            Response.Redirect("~/CrewOperation/Dummy_Training1.aspx");
        }
        Auth = new Authority();
        Auth1 = new AuthenticationManager(136, Convert.ToInt32(Session["loginid"].ToString()), ObjectType.Page);
        Auth.isAdd = Auth1.IsAdd;
        Auth.isDelete = Auth1.IsDelete;
        Auth.isEdit = Auth1.IsUpdate;
        Auth.isPrint = Auth1.IsPrint;
        Auth.isVerify = Auth1.IsVerify;
        Auth.isVerify2 = Auth1.IsVerify2;

        //btn_Add_TrainingType.Visible = Auth1.IsAdd;

        if (!Page.IsPostBack)
        {
            bindTrainingTypeGrid();
            bindStatusDDL();
            Alerts.HidePanel(pnl_TrainingType);
            Alerts.HANDLE_AUTHORITY(1, btn_Add_TrainingType, btn_Save_TrainingType, btn_Cancel_TrainingType, btn_Print_TrainingType, Auth);
        }
    }
    public void bindTrainingTypeGrid()
    {
        DataTable dt1 = TrainingType.selectDataTrainingTypeDetails();
        this.GridView_TrainingType.DataSource = dt1;
        this.GridView_TrainingType.DataBind();
    }
    public void bindStatusDDL()
    {
        DataTable dt2 = TrainingType.selectDataStatusDetails();
        this.ddlStatus_TrainingType.DataValueField = "StatusId";
        this.ddlStatus_TrainingType.DataTextField = "StatusName";
        this.ddlStatus_TrainingType.DataSource = dt2;
        this.ddlStatus_TrainingType.DataBind();
    }
    protected void btn_Add_TrainingType_Click(object sender, EventArgs e)
    {
        txtTrainingTypeName.Text = "";
        txtCreatedBy_TrainingType.Text = "";
        txtCreatedOn_TrainingType.Text = "";
        txtModifiedBy_TrainingType.Text = "";
        txtModifiedOn_TrainingType.Text = "";
        ddlStatus_TrainingType.SelectedIndex = 0;
        GridView_TrainingType.SelectedIndex = -1;
        HiddenTrainingType.Value = "";
        Alerts.ShowPanel(pnl_TrainingType);
        Alerts.HANDLE_AUTHORITY(2, btn_Add_TrainingType, btn_Save_TrainingType, btn_Cancel_TrainingType, btn_Print_TrainingType, Auth);
    }
    protected void btn_Save_TrainingType_Click(object sender, EventArgs e)
    {
        int Duplicate = 0;

        foreach (GridViewRow dg in GridView_TrainingType.Rows)
        {
            HiddenField hfd;
            HiddenField hfd1;
            hfd = (HiddenField)dg.FindControl("HiddenTrainingTypeName");
            hfd1 = (HiddenField)dg.FindControl("HiddenTrainingTypeId");

            if (hfd.Value.ToString().ToUpper().Trim() == txtTrainingTypeName.Text.ToUpper().Trim())
            {
                if (HiddenTrainingType.Value.Trim() == "")
                {

                    lbl_TrainingType_Message.Text = "Already Entered.";
                    Duplicate = 1;
                    break;
                }
                else if (HiddenTrainingType.Value.Trim() != hfd1.Value.ToString())
                {

                    lbl_TrainingType_Message.Text = "Already Entered.";
                    Duplicate = 1;
                    break;
                }
            }
            else
            {

                lbl_TrainingType_Message.Text = "";
            }
        }
        if (Duplicate == 0)
        {
            int intTrainingTypeId = -1;
            int intCreatedBy = 0;
            int intModifiedBy = 0;

            if (HiddenTrainingType.Value.ToString().Trim() == "")
            {
                intCreatedBy = Convert.ToInt32(Session["loginid"].ToString());
            }
            else
            {
                intTrainingTypeId = Convert.ToInt32(HiddenTrainingType.Value);
                intModifiedBy = Convert.ToInt32(Session["loginid"].ToString());
            }
            string strTrainingTypeName = txtTrainingTypeName.Text;
            char charStatusId = Convert.ToChar(ddlStatus_TrainingType.SelectedValue);

            TrainingType.insertUpdateTrainingTypeDetails("InsertUpdateTrainingTypeDetails",
                                          intTrainingTypeId,
                                          strTrainingTypeName,
                                          intCreatedBy,
                                          intModifiedBy,
                                          charStatusId);

            bindTrainingTypeGrid();
            lbl_TrainingType_Message.Text = "Record Successfully Saved.";
            Alerts.HidePanel(pnl_TrainingType);
            Alerts.HANDLE_AUTHORITY(3, btn_Add_TrainingType, btn_Save_TrainingType, btn_Cancel_TrainingType, btn_Print_TrainingType, Auth);
        }
    }
    protected void btn_Cancel_TrainingType_Click(object sender, EventArgs e)
    {
        GridView_TrainingType.SelectedIndex = -1;
        Alerts.HidePanel(pnl_TrainingType);
        Alerts.HANDLE_AUTHORITY(6, btn_Add_TrainingType, btn_Save_TrainingType, btn_Cancel_TrainingType, btn_Print_TrainingType, Auth);
    }
    protected void btn_Print_TrainingType_Click(object sender, EventArgs e)
    {

    }
    protected void GridView_TrainingType_DataBound(object sender, EventArgs e)
    {
        Alerts.HANDLE_GRID(GridView_TrainingType, Auth);
    }
    protected void Show_Record_TrainingType(int TrainingTypeid)
    {
        HiddenTrainingType.Value = TrainingTypeid.ToString();
        DataTable dt3 = TrainingType.selectDataTrainingTypeDetailsByTrainingTypeId(TrainingTypeid);
        foreach (DataRow dr in dt3.Rows)
        {
            txtTrainingTypeName.Text = dr["TrainingTypeName"].ToString();
            txtCreatedBy_TrainingType.Text = dr["CreatedBy"].ToString();
            txtCreatedOn_TrainingType.Text = dr["CreatedOn"].ToString();
            txtModifiedBy_TrainingType.Text = dr["ModifiedBy"].ToString();
            txtModifiedOn_TrainingType.Text = dr["ModifiedOn"].ToString();
            ddlStatus_TrainingType.SelectedValue = dr["StatusId"].ToString();
        }
    }
    protected void GridView_TrainingType_SelectedIndexChanged(object sender, EventArgs e)
    {
        HiddenField hfdTrainingType;
        hfdTrainingType = (HiddenField)GridView_TrainingType.Rows[GridView_TrainingType.SelectedIndex].FindControl("HiddenTrainingTypeId");
        id = Convert.ToInt32(hfdTrainingType.Value.ToString());
        Show_Record_TrainingType(id);
        Alerts.ShowPanel(pnl_TrainingType);
        Alerts.HANDLE_AUTHORITY(4, btn_Add_TrainingType, btn_Save_TrainingType, btn_Cancel_TrainingType, btn_Print_TrainingType, Auth);
    }
    protected void GridView_TrainingType_Row_Editing(object sender, GridViewEditEventArgs e)
    {
        Mode = "Edit";
        HiddenField hfdTrainingType;
        hfdTrainingType = (HiddenField)GridView_TrainingType.Rows[e.NewEditIndex].FindControl("HiddenTrainingTypeId");
        id = Convert.ToInt32(hfdTrainingType.Value.ToString());
        Show_Record_TrainingType(id);
        GridView_TrainingType.SelectedIndex = e.NewEditIndex;
        Alerts.ShowPanel(pnl_TrainingType);
        Alerts.HANDLE_AUTHORITY(5, btn_Add_TrainingType, btn_Save_TrainingType, btn_Cancel_TrainingType, btn_Print_TrainingType, Auth);
    }
    protected void GridView_TrainingType_Row_Deleting(object sender, GridViewDeleteEventArgs e)
    {
        int intModifiedBy = Convert.ToInt32(Session["loginid"].ToString());
        HiddenField hfdTrainingType;
        hfdTrainingType = (HiddenField)GridView_TrainingType.Rows[e.RowIndex].FindControl("HiddenTrainingTypeId");
        id = Convert.ToInt32(hfdTrainingType.Value.ToString());
        TrainingType.deleteTrainingTypeDetailsById("DeleteTrainingTypeDetailsById", id, intModifiedBy);
        bindTrainingTypeGrid();
        if (HiddenTrainingType.Value.ToString() == hfdTrainingType.Value.ToString())
        {
            btn_Add_TrainingType_Click(sender, e);
        }
    }
    protected void GridView_TrainingType_PreRender(object sender, EventArgs e)
    {
        if (GridView_TrainingType.Rows.Count <= 0) { lbl_GridView_TrainingType.Text = "No Records Found..!"; }
    }
}
