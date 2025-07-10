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

public partial class Registers_TrainingInstitute : System.Web.UI.Page
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
        lbl_TrainingInstitute_Message.Text = "";
        lbl_GridView_TrainingInstitute.Text = "";
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 135);
        if (chpageauth <= 0)
        {
            Response.Redirect("~/CrewOperation/Dummy_Training1.aspx");
        }

        Auth = new Authority();
        Auth1 = new AuthenticationManager(135, Convert.ToInt32(Session["loginid"].ToString()), ObjectType.Page);
        Auth.isAdd = Auth1.IsAdd;
        Auth.isDelete = Auth1.IsDelete;
        Auth.isEdit = Auth1.IsUpdate;
        Auth.isPrint = Auth1.IsPrint;
        Auth.isVerify = Auth1.IsVerify;
        Auth.isVerify2 = Auth1.IsVerify2;


        if (!Page.IsPostBack)
        {
            bindTrainingInstituteGrid();
            bindStatusDDL();
            Alerts.HidePanel(pnl_TrainingInstitute);
            Alerts.HANDLE_AUTHORITY(1, btn_Add_TrainingInstitute, btn_Save_TrainingInstitute, btn_Cancel_TrainingInstitute, btn_Print_TrainingInstitute, Auth);
        }
    }
    public void bindTrainingInstituteGrid()
    {
        DataTable dt1 = TrainingInstitute.selectDataTrainingInstituteDetails();
        this.GridView_TrainingInstitute.DataSource = dt1;
        this.GridView_TrainingInstitute.DataBind();
    }
    public void bindStatusDDL()
    {
        DataTable dt2 = TrainingInstitute.selectDataStatusDetails();
        this.ddlStatus_TrainingInstitute.DataValueField = "StatusId";
        this.ddlStatus_TrainingInstitute.DataTextField = "StatusName";
        this.ddlStatus_TrainingInstitute.DataSource = dt2;
        this.ddlStatus_TrainingInstitute.DataBind();
    }
    protected void btn_Add_TrainingInstitute_Click(object sender, EventArgs e)
    {
        txtTrainingInstituteName.Text = "";
        txtCreatedBy_TrainingInstitute.Text = "";
        txtCreatedOn_TrainingInstitute.Text = "";
        txtModifiedBy_TrainingInstitute.Text = "";
        txtModifiedOn_TrainingInstitute.Text = "";
        ddlStatus_TrainingInstitute.SelectedIndex = 0;
        GridView_TrainingInstitute.SelectedIndex = -1;
        HiddenTrainingInstitute.Value = "";
        Alerts.ShowPanel(pnl_TrainingInstitute);
        Alerts.HANDLE_AUTHORITY(2, btn_Add_TrainingInstitute, btn_Save_TrainingInstitute, btn_Cancel_TrainingInstitute, btn_Print_TrainingInstitute, Auth);
    }
    protected void btn_Save_TrainingInstitute_Click(object sender, EventArgs e)
    {
        int Duplicate = 0;

        foreach (GridViewRow dg in GridView_TrainingInstitute.Rows)
        {
            HiddenField hfd;
            HiddenField hfd1;
            hfd = (HiddenField)dg.FindControl("HiddenTrainingInstituteName");
            hfd1 = (HiddenField)dg.FindControl("HiddenTrainingInstituteId");

            if (hfd.Value.ToString().ToUpper().Trim() == txtTrainingInstituteName.Text.ToUpper().Trim())
            {
                if (HiddenTrainingInstitute.Value.Trim() == "")
                {

                    lbl_TrainingInstitute_Message.Text = "Already Entered.";
                    Duplicate = 1;
                    break;
                }
                else if (HiddenTrainingInstitute.Value.Trim() != hfd1.Value.ToString())
                {

                    lbl_TrainingInstitute_Message.Text = "Already Entered.";
                    Duplicate = 1;
                    break;
                }
            }
            else
            {

                lbl_TrainingInstitute_Message.Text = "";
            }
        }
        if (Duplicate == 0)
        {
            int intTrainingInstituteId = -1;
            int intCreatedBy = 0;
            int intModifiedBy = 0;

            if (HiddenTrainingInstitute.Value.ToString().Trim() == "")
            {
                intCreatedBy = Convert.ToInt32(Session["loginid"].ToString());
            }
            else
            {
                intTrainingInstituteId = Convert.ToInt32(HiddenTrainingInstitute.Value);
                intModifiedBy = Convert.ToInt32(Session["loginid"].ToString());
            }
            string strTrainingInstituteName = txtTrainingInstituteName.Text;
            char charStatusId = Convert.ToChar(ddlStatus_TrainingInstitute.SelectedValue);

            TrainingInstitute.insertUpdateTrainingInstituteDetails("InsertUpdateTrainingInstituteDetails",
                                          intTrainingInstituteId,
                                          strTrainingInstituteName,
                                          intCreatedBy,
                                          intModifiedBy,
                                          charStatusId);

            bindTrainingInstituteGrid();
            lbl_TrainingInstitute_Message.Text = "Record Successfully Saved.";
            Alerts.HidePanel(pnl_TrainingInstitute);
            Alerts.HANDLE_AUTHORITY(3, btn_Add_TrainingInstitute, btn_Save_TrainingInstitute, btn_Cancel_TrainingInstitute, btn_Print_TrainingInstitute, Auth);
        }
    }
    protected void btn_Cancel_TrainingInstitute_Click(object sender, EventArgs e)
    {
        GridView_TrainingInstitute.SelectedIndex = -1;
        Alerts.HidePanel(pnl_TrainingInstitute);
        Alerts.HANDLE_AUTHORITY(6, btn_Add_TrainingInstitute, btn_Save_TrainingInstitute, btn_Cancel_TrainingInstitute, btn_Print_TrainingInstitute, Auth);
    }
    protected void btn_Print_TrainingInstitute_Click(object sender, EventArgs e)
    {

    }
    protected void GridView_TrainingInstitute_DataBound(object sender, EventArgs e)
    {
        Alerts.HANDLE_GRID(GridView_TrainingInstitute, Auth);
    }
    protected void Show_Record_TrainingInstitute(int TrainingInstituteid)
    {
        HiddenTrainingInstitute.Value = TrainingInstituteid.ToString();
        DataTable dt3 = TrainingInstitute.selectDataTrainingInstituteDetailsByTrainingInstituteId(TrainingInstituteid);
        foreach (DataRow dr in dt3.Rows)
        {
            txtTrainingInstituteName.Text = dr["InstituteName"].ToString();
            txtCreatedBy_TrainingInstitute.Text = dr["CreatedBy"].ToString();
            txtCreatedOn_TrainingInstitute.Text = dr["CreatedOn"].ToString();
            txtModifiedBy_TrainingInstitute.Text = dr["ModifiedBy"].ToString();
            txtModifiedOn_TrainingInstitute.Text = dr["ModifiedOn"].ToString();
            ddlStatus_TrainingInstitute.SelectedValue = dr["StatusId"].ToString();
        }
    }
    protected void GridView_TrainingInstitute_SelectedIndexChanged(object sender, EventArgs e)
    {
        HiddenField hfdTrainingInstitute;
        hfdTrainingInstitute = (HiddenField)GridView_TrainingInstitute.Rows[GridView_TrainingInstitute.SelectedIndex].FindControl("HiddenTrainingInstituteId");
        id = Convert.ToInt32(hfdTrainingInstitute.Value.ToString());
        Show_Record_TrainingInstitute(id);
        Alerts.ShowPanel(pnl_TrainingInstitute);
        Alerts.HANDLE_AUTHORITY(4, btn_Add_TrainingInstitute, btn_Save_TrainingInstitute, btn_Cancel_TrainingInstitute, btn_Print_TrainingInstitute, Auth);
    }
    protected void GridView_TrainingInstitute_Row_Editing(object sender, GridViewEditEventArgs e)
    {
        Mode = "Edit";
        HiddenField hfdTrainingInstitute;
        hfdTrainingInstitute = (HiddenField)GridView_TrainingInstitute.Rows[e.NewEditIndex].FindControl("HiddenTrainingInstituteId");
        id = Convert.ToInt32(hfdTrainingInstitute.Value.ToString());
        Show_Record_TrainingInstitute(id);
        GridView_TrainingInstitute.SelectedIndex = e.NewEditIndex;
        Alerts.ShowPanel(pnl_TrainingInstitute);
        Alerts.HANDLE_AUTHORITY(5, btn_Add_TrainingInstitute, btn_Save_TrainingInstitute, btn_Cancel_TrainingInstitute, btn_Print_TrainingInstitute, Auth);
    }
    protected void GridView_TrainingInstitute_Row_Deleting(object sender, GridViewDeleteEventArgs e)
    {
        int intModifiedBy = Convert.ToInt32(Session["loginid"].ToString());
        HiddenField hfdTrainingInstitute;
        hfdTrainingInstitute = (HiddenField)GridView_TrainingInstitute.Rows[e.RowIndex].FindControl("HiddenTrainingInstituteId");
        id = Convert.ToInt32(hfdTrainingInstitute.Value.ToString());
        TrainingInstitute.deleteTrainingInstituteDetailsById("DeleteTrainingInstituteDetailsById", id, intModifiedBy);
        bindTrainingInstituteGrid();
        if (HiddenTrainingInstitute.Value.ToString() == hfdTrainingInstitute.Value.ToString())
        {
            btn_Add_TrainingInstitute_Click(sender, e);
        }
    }
    protected void GridView_TrainingInstitute_PreRender(object sender, EventArgs e)
    {
        if (GridView_TrainingInstitute.Rows.Count <= 0) { lbl_GridView_TrainingInstitute.Text = "No Records Found..!"; }
    }
}
