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

public partial class Modules_HRD_Registers_RankResponsibility : System.Web.UI.Page
{
    int id;
    Authority Auth;
    string Mode = "New";
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        lbl_RankResponsibility_Message.Text = "";
        lblRankResponsibility.Text = "";

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
            BindStatusDropDown();
            BindRankDropDown();
            BindGridRankResponsibilities();
            Alerts.HidePanel(RankResponsibilitiespanel);
            Alerts.HANDLE_AUTHORITY(1, btn_add, btn_save, btn_Cancel, btn_Print, Auth);

        }
    }
    private void BindStatusDropDown()
    {
        DataTable dt2 = RankGroup.selectDataStatus();
        this.ddstatus_rank_group.DataValueField = "StatusId";
        this.ddstatus_rank_group.DataTextField = "StatusName";
        this.ddstatus_rank_group.DataSource = dt2;
        this.ddstatus_rank_group.DataBind();
    }

    private void BindRankDropDown()
    {
        ProcessSelectRank obj = new ProcessSelectRank();
        obj.Invoke();
        ddl_Rank_Search.DataSource = obj.ResultSet.Tables[0];
        ddl_Rank_Search.DataTextField = "RankName";
        ddl_Rank_Search.DataValueField = "RankId";
        ddl_Rank_Search.DataBind();

    }
    private void BindGridRankResponsibilities()
    {
        DataTable dt = RankResponsibilities.selectDataRankResponsibilitiesDetails();
        this.GvRankResponsibilities.DataSource = dt;
        this.GvRankResponsibilities.DataBind();
    }
    protected void GvRankResponsibilities_SelectIndexChanged(object sender, EventArgs e)
    {
        HiddenField hdnCRRID;
        hdnCRRID = (HiddenField)GvRankResponsibilities.Rows[GvRankResponsibilities.SelectedIndex].FindControl("hdnCRRID");
        id = Convert.ToInt32(hdnCRRID.Value.ToString());
        Show_RecordRankResponsibilities(id);
        Alerts.ShowPanel(RankResponsibilitiespanel);
        Alerts.HANDLE_AUTHORITY(4, btn_add, btn_save, btn_Cancel, btn_Print, Auth);

    }
    protected void Show_RecordRankResponsibilities(int Crrid)
    {
        hdnCRRID.Value = Crrid.ToString();
        DataTable dt3 = RankResponsibilities.selectDataRankResponsibilitiesById(Crrid);
        foreach (DataRow dr in dt3.Rows)
        {
          ddl_Rank_Search.SelectedValue = dr["CRRRankId"].ToString();
            txtResponsibilities.Text = dr["CRRResponsibilities"].ToString();
            txtcreatedby_rank_group.Text = dr["CreatedBy"].ToString();
            txtcreatedon_rank_group.Text = dr["CreatedOn"].ToString();
            txtmodifiedby_rank_group.Text = dr["ModifiedBy"].ToString();
            txtmodifiedon_rank_group.Text = dr["ModifiedOn"].ToString();
            ddstatus_rank_group.SelectedValue = dr["StatusId"].ToString();
        }
    }
    protected void GvRankResponsibilities_Row_Editing(object sender, GridViewEditEventArgs e)
    {
        Mode = "Edit";
        HiddenField hdnCRRID;
        hdnCRRID = (HiddenField)GvRankResponsibilities.Rows[e.NewEditIndex].FindControl("hdnCRRID");
        id = Convert.ToInt32(hdnCRRID.Value.ToString());
        Show_RecordRankResponsibilities(id);
        GvRankResponsibilities.SelectedIndex = e.NewEditIndex;
        Alerts.ShowPanel(RankResponsibilitiespanel);
        Alerts.HANDLE_AUTHORITY(5, btn_add, btn_save, btn_Cancel, btn_Print, Auth);

    }
    protected void GvRankResponsibilities_Row_Deleting(object sender, GridViewDeleteEventArgs e)
    {
        int intModifiedBy = Convert.ToInt32(Session["loginid"].ToString());
        HiddenField hfddel;
        hfddel = (HiddenField)GvRankResponsibilities.Rows[e.RowIndex].FindControl("hdnCRRID");
        id = Convert.ToInt32(hfddel.Value.ToString());
        RankResponsibilities.deleteRankGroupDetails("deleteRankResponsibilities", id, intModifiedBy);
        BindGridRankResponsibilities();
        if (hdnCRRID.Value.Trim() == hfddel.Value.ToString())
        {
            btn_add_Click(sender, e);
        }
    }
    protected void GvRankResponsibilities_DataBound(object sender, EventArgs e)
    {
        Alerts.HANDLE_GRID(GvRankResponsibilities, Auth);
    }
    protected void GvRankResponsibilities_PreRender(object sender, EventArgs e)
    {
        if (this.GvRankResponsibilities.Rows.Count <= 0)
        {
            lblRankResponsibility.Text = "No Records Found..!";
        }

    }
    protected void btn_add_Click(object sender, EventArgs e)
    {
        hdnCRRID.Value = "";
        ddl_Rank_Search.SelectedIndex = 0;
        txtResponsibilities.Text = "";
        txtcreatedby_rank_group.Text = "";
        txtcreatedon_rank_group.Text = "";
        txtmodifiedby_rank_group.Text = "";
        txtmodifiedon_rank_group.Text = "";
        ddstatus_rank_group.SelectedIndex = 0;
        Alerts.ShowPanel(RankResponsibilitiespanel);
        Alerts.HANDLE_AUTHORITY(2, btn_add, btn_save, btn_Cancel, btn_Print, Auth);

    }
    protected void btn_save_Click(object sender, EventArgs e)
    {
        //int Duplicate = 0;

        //foreach (GridViewRow dg in GvRankResponsibilities.Rows)
        //{
        //   // HiddenField hfd;
        //    HiddenField hfd1;
        //  //  hfd = (HiddenField)dg.FindControl("Hiddenrankgroupname");
        //    hfd1 = (HiddenField)dg.FindControl("HiddenCRRId");

               
        //    if (hdnCRRID.Value.Trim() != hfd1.Value.ToString())
        //    {
        //        lbl_RankResponsibility_Message.Text = "Already Entered.";
        //        Duplicate = 1;
        //        break;
        //    }
        //    else
        //    {
        //        lbl_RankResponsibility_Message.Text = "";
        //    }
        //}
        //if (Duplicate == 0)
        //{
            int CRRId = -1;
            int createdby = 0, modifiedby = 0;

            string strResponsibilities = txtResponsibilities.Text;
            int RankId = Convert.ToInt32(ddl_Rank_Search.SelectedValue);

            char status = Convert.ToChar(ddstatus_rank_group.SelectedValue);
            if (hdnCRRID.Value.Trim() == "")
            {
                createdby = Convert.ToInt32(Session["loginid"].ToString());
            }
            else
            {
                CRRId = Convert.ToInt32(hdnCRRID.Value);
                modifiedby = Convert.ToInt32(Session["loginid"].ToString());
            }
            RankResponsibilities.insertUpdateRankResponsibilities("InsertUpdateRankResponsibilitiesDetails",
                                         CRRId,
                                          RankId,
                                          strResponsibilities,
                                          createdby,
                                          modifiedby,
                                          status);
            BindGridRankResponsibilities();
            lbl_RankResponsibility_Message.Text = "Record Successfully Saved.";
            Alerts.HidePanel(RankResponsibilitiespanel);
            Alerts.HANDLE_AUTHORITY(3, btn_add, btn_save, btn_Cancel, btn_Print, Auth);

        //}
    }
    protected void btn_Cancel_Click(object sender, EventArgs e)
    {
        GvRankResponsibilities.SelectedIndex = -1;
        Alerts.HidePanel(RankResponsibilitiespanel);
        Alerts.HANDLE_AUTHORITY(6, btn_add, btn_save, btn_Cancel, btn_Print, Auth);

    }
    protected void btn_Print_Click(object sender, EventArgs e)
    {

    }

    protected void GvRankResponsibilities_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Modify")
        {
            GridViewRow row = (GridViewRow)(((ImageButton)sender).NamingContainer);
            int Rowindx = row.RowIndex;
            Mode = "Edit";
            HiddenField HiddenCRRId;
            HiddenCRRId = (HiddenField)GvRankResponsibilities.Rows[Rowindx].FindControl("HiddenCRRId");
            id = Convert.ToInt32(HiddenCRRId.Value.ToString());
            Show_RecordRankResponsibilities(id);
            GvRankResponsibilities.SelectedIndex = Rowindx;
            Alerts.ShowPanel(RankResponsibilitiespanel);
            Alerts.HANDLE_AUTHORITY(5, btn_add, btn_save, btn_Cancel, btn_Print, Auth);
        }
    }
    protected void btnEditRankResponsibilities_click(object sender, ImageClickEventArgs e)
    {
        GridViewRow row = (GridViewRow)(((ImageButton)sender).NamingContainer);
        int Rowindx = row.RowIndex;
        Mode = "Edit";
        HiddenField hiddenCRRID;
        hiddenCRRID = (HiddenField)GvRankResponsibilities.Rows[Rowindx].FindControl("hdnCRRID");
        id = Convert.ToInt32(hiddenCRRID.Value.ToString());
        hdnCRRID.Value = id.ToString();
        Show_RecordRankResponsibilities(id);
        GvRankResponsibilities.SelectedIndex = Rowindx;
        Alerts.ShowPanel(RankResponsibilitiespanel);
        Alerts.HANDLE_AUTHORITY(5, btn_add, btn_save, btn_Cancel, btn_Print, Auth);
    }
}