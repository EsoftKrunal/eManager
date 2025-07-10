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

public partial class Registers_RankGroup : System.Web.UI.Page
{
    int id;
    Authority Auth;
    string Mode = "New";
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        lbl_RankGroup_Message.Text = "";
        lblRank.Text = "";

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
            BindGridRankGroup();
            Alerts.HidePanel(Rankgrouppanel);
            Alerts.HANDLE_AUTHORITY(1, btn_rank_group_add, btn_rank_group_save, btn_rank_group_Cancel, btn_Print_RankGroup, Auth);
       
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
    private void BindGridRankGroup()
    {
        DataTable dt = RankGroup.selectDataRankGroupDetails();
        this.GvRank_Group.DataSource = dt;
        this.GvRank_Group.DataBind();
    }
    protected void GvRank_Group_SelectIndexChanged(object sender, EventArgs e)
    {
        HiddenField hfdrank;
        hfdrank = (HiddenField)GvRank_Group.Rows[GvRank_Group.SelectedIndex].FindControl("HiddenrankGroupId");
        id = Convert.ToInt32(hfdrank.Value.ToString());
        Show_Record_rank_group(id);
        Alerts.ShowPanel(Rankgrouppanel);
        Alerts.HANDLE_AUTHORITY(4, btn_rank_group_add, btn_rank_group_save, btn_rank_group_Cancel, btn_Print_RankGroup, Auth);     
  
    }
    protected void Show_Record_rank_group(int rankgroupid)
    {
        HiddenRankGrouppk.Value = rankgroupid.ToString();
        DataTable dt3 = RankGroup.selectDataRankGroupDetailsByRankGroupId(rankgroupid);
        foreach (DataRow dr in dt3.Rows)
        {
            txtrankgroupname.Text=dr["RankGroupName"].ToString();
            txtcreatedby_rank_group.Text = dr["CreatedBy"].ToString();
            txtcreatedon_rank_group.Text = dr["CreatedOn"].ToString();
            txtmodifiedby_rank_group.Text = dr["ModifiedBy"].ToString();
            txtmodifiedon_rank_group.Text = dr["ModifiedOn"].ToString();
            ddstatus_rank_group.SelectedValue = dr["StatusId"].ToString();
        }
    }
    protected void GvRank_Group_Row_Editing(object sender, GridViewEditEventArgs e)
    {
        Mode = "Edit";
        HiddenField hfdrank;
        hfdrank = (HiddenField)GvRank_Group.Rows[e.NewEditIndex].FindControl("HiddenrankGroupId");
        id = Convert.ToInt32(hfdrank.Value.ToString());
        Show_Record_rank_group(id);
        GvRank_Group.SelectedIndex = e.NewEditIndex;
        Alerts.ShowPanel(Rankgrouppanel);
        Alerts.HANDLE_AUTHORITY(5, btn_rank_group_add, btn_rank_group_save, btn_rank_group_Cancel, btn_Print_RankGroup, Auth);     
     
    }
    protected void GvRank_Group_Row_Deleting(object sender, GridViewDeleteEventArgs e)
    {
        int intModifiedBy = Convert.ToInt32(Session["loginid"].ToString());
        HiddenField hfddel;
        hfddel = (HiddenField)GvRank_Group.Rows[e.RowIndex].FindControl("HiddenrankGroupId");
        id = Convert.ToInt32(hfddel.Value.ToString());
        RankGroup.deleteRankGroupDetails("deleterankGroup", id, intModifiedBy);
        BindGridRankGroup();
        if (HiddenRankGrouppk.Value.Trim() == hfddel.Value.ToString())
        {
            btn_rank_group_add_Click(sender, e);
        }
    }
    protected void GvRank_Group_DataBound(object sender, EventArgs e)
    {
        Alerts.HANDLE_GRID(GvRank_Group, Auth);  
    }
    protected void GvRank_Group_PreRender(object sender, EventArgs e)
    {
        if (this.GvRank_Group.Rows.Count <= 0)
        {
            lblRank.Text = "No Records Found..!";
        }
        
    }
    protected void btn_rank_group_add_Click(object sender, EventArgs e)
    {
        HiddenRankGrouppk.Value = "";
        txtrankgroupname.Text = "";
        txtcreatedby_rank_group.Text = "";
        txtcreatedon_rank_group.Text = "";
        txtmodifiedby_rank_group.Text = "";
        txtmodifiedon_rank_group.Text = "";
        ddstatus_rank_group.SelectedIndex = 0;
        Alerts.ShowPanel(Rankgrouppanel);
        Alerts.HANDLE_AUTHORITY(2, btn_rank_group_add, btn_rank_group_save, btn_rank_group_Cancel, btn_Print_RankGroup, Auth);    
 
    }
    protected void btn_rank_group_save_Click(object sender, EventArgs e)
    {
        int Duplicate=0;
        
            foreach (GridViewRow dg in GvRank_Group.Rows)
            {
                HiddenField hfd;
                HiddenField hfd1;
                hfd = (HiddenField)dg.FindControl("Hiddenrankgroupname");
                hfd1=(HiddenField)dg.FindControl("HiddenrankGroupId");

                if (hfd.Value.ToString().ToUpper().Trim() == txtrankgroupname.Text.ToUpper().Trim())
                {
                    if (HiddenRankGrouppk.Value.Trim() == "")
                    {
                        lbl_RankGroup_Message.Text = "Already Entered.";
                        Duplicate = 1;
                        break;
                    }
                    else if (HiddenRankGrouppk.Value.Trim() != hfd1.Value.ToString())
                    {
                        lbl_RankGroup_Message.Text = "Already Entered.";
                        Duplicate = 1;
                        break;
                    }
                }
                else
                {
                    lbl_RankGroup_Message.Text = "";
                }
            }
        if (Duplicate == 0)
        {
            int rankgroupId = -1;
            int createdby = 0, modifiedby = 0;
           
            string strrankgroupName = txtrankgroupname.Text;

            char status = Convert.ToChar(ddstatus_rank_group.SelectedValue);
            if (HiddenRankGrouppk.Value.Trim() == "")
            {
                createdby = Convert.ToInt32(Session["loginid"].ToString());
            }
            else
            {
                rankgroupId = Convert.ToInt32(HiddenRankGrouppk.Value);
                modifiedby = Convert.ToInt32(Session["loginid"].ToString());
            }
            RankGroup.insertUpdateRankGroupDetails("InsertUpdateRankGroupDetails",
                                          rankgroupId,
                                          strrankgroupName,
                                          createdby,
                                          modifiedby,
                                          status);
            BindGridRankGroup();
            lbl_RankGroup_Message.Text = "Record Successfully Saved.";
            Alerts.HidePanel(Rankgrouppanel);
            Alerts.HANDLE_AUTHORITY(3, btn_rank_group_add, btn_rank_group_save, btn_rank_group_Cancel, btn_Print_RankGroup, Auth);    
           
        }
    }
    protected void btn_rank_group_Cancel_Click(object sender, EventArgs e)
    {
        GvRank_Group.SelectedIndex = -1;
        Alerts.HidePanel(Rankgrouppanel);
        Alerts.HANDLE_AUTHORITY(6, btn_rank_group_add, btn_rank_group_save, btn_rank_group_Cancel, btn_Print_RankGroup, Auth);     
   
    }
    protected void btn_Print_RankGroup_Click(object sender, EventArgs e)
    {

    }

    protected void GvRank_Group_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Modify")
        {
            GridViewRow row = (GridViewRow)(((ImageButton)sender).NamingContainer);
            int Rowindx = row.RowIndex;
            Mode = "Edit";
            HiddenField hfdrank;
            hfdrank = (HiddenField)GvRank_Group.Rows[Rowindx].FindControl("hdnRankGroupId");
            id = Convert.ToInt32(hfdrank.Value.ToString());
            Show_Record_rank_group(id);
            GvRank_Group.SelectedIndex = Rowindx;
            Alerts.ShowPanel(Rankgrouppanel);
            Alerts.HANDLE_AUTHORITY(5, btn_rank_group_add, btn_rank_group_save, btn_rank_group_Cancel, btn_Print_RankGroup, Auth);
        }
    }
    protected void btnEditRankGroup_click(object sender, ImageClickEventArgs e)
    {
        GridViewRow row = (GridViewRow)(((ImageButton)sender).NamingContainer);
        int Rowindx = row.RowIndex;
        Mode = "Edit";
        HiddenField hfdrank;
        hfdrank = (HiddenField)GvRank_Group.Rows[Rowindx].FindControl("hdnRankGroupId");
        id = Convert.ToInt32(hfdrank.Value.ToString());
        Show_Record_rank_group(id);
        GvRank_Group.SelectedIndex = Rowindx;
        Alerts.ShowPanel(Rankgrouppanel);
        Alerts.HANDLE_AUTHORITY(5, btn_rank_group_add, btn_rank_group_save, btn_rank_group_Cancel, btn_Print_RankGroup, Auth);
    }
}
