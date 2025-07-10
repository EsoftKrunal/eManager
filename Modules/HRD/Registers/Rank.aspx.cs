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

public partial class Registers_Rank : System.Web.UI.Page
{
    int id;
    Authority Auth;
    string Mode = "New";
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        lbl_Rank_Message.Text = "";
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
            BindRankGroupIdDropDown();
            BindOffCrewDropDown();
            BindOffGroupDropDown();
            BindStatusDropDown();
            BindGridRank();
            Alerts.HidePanel(Rankpanel);
            Alerts.HANDLE_AUTHORITY(1, btn_rank_add, btn_rank_save, btn_rank_Cancel, btn_Print_Rank, Auth);
            ddOffCrew_SireRank.Items.Add(new ListItem("< Select >", "")); 
        }
    }
    private void BindOffCrewDropDown()
    {
        DataTable dt1 = Rank.selectDataOffCrew();
        this.ddOffCrew_rank.DataValueField = "OffCrewId";
        this.ddOffCrew_rank.DataTextField = "OffCrewName";
        this.ddOffCrew_rank.DataSource = dt1;
        this.ddOffCrew_rank.DataBind();
    }
    private void BindOffGroupDropDown()
    {
        DataTable dt2 = Rank.selectDataOffGroup();
        this.ddOffGroup_rank.DataValueField = "OffGroupId";
        this.ddOffGroup_rank.DataTextField = "OffGroupName";
        this.ddOffGroup_rank.DataSource = dt2;
        this.ddOffGroup_rank.DataBind();
    }
    private void BindStatusDropDown()
    {
        DataTable dt3 = Rank.selectDataStatus();
        this.ddstatus_rank.DataValueField = "StatusId";
        this.ddstatus_rank.DataTextField = "StatusName";
        this.ddstatus_rank.DataSource = dt3;
        this.ddstatus_rank.DataBind();
    }
    private void BindRankGroupIdDropDown()
    {
        DataTable dt4 = Rank.selectDataRankGroupId();
        this.ddrankgroupid.DataValueField = "RankGroupId";
        this.ddrankgroupid.DataTextField = "RankGroupName";
        this.ddrankgroupid.DataSource = dt4;
        this.ddrankgroupid.DataBind();
    }
    private void BindGridRank()
    {
        DataTable dt = Rank.selectDataRankDetails();
        this.GvRank.DataSource = dt;
        this.GvRank.DataBind();
    }
    protected void GvRank_SelectIndexChanged(object sender, EventArgs e)
    {
      
        HiddenField hfdrank;
        hfdrank = (HiddenField)GvRank.Rows[GvRank.SelectedIndex].FindControl("HiddenrankId");
        id = Convert.ToInt32(hfdrank.Value.ToString());
        Show_Record_rank(id);
        Alerts.ShowPanel(Rankpanel);
        Alerts.HANDLE_AUTHORITY(4, btn_rank_add, btn_rank_save, btn_rank_Cancel, btn_Print_Rank, Auth);     
  
    }
    protected void Show_Record_rank(int rankid)
    {
        string Mess;
        Mess = "";
        HiddenRankpk.Value = rankid.ToString();
        DataTable dt3 = Rank.selectDataRankDetailsByRankId(rankid);
        foreach (DataRow dr in dt3.Rows)
        {         
            Mess = Mess + Alerts.Set_DDL_Value(ddrankgroupid, dr["RankGroupId"].ToString(), "Rank Group");
           
            txtrankcode.Text = dr["RankCode"].ToString();
            txtrankname.Text = dr["RankName"].ToString();
          
            Mess = Mess + Alerts.Set_DDL_Value(ddOffCrew_rank, dr["offcrew"].ToString(), "Off Crew");
          
            Mess = Mess + Alerts.Set_DDL_Value(ddOffGroup_rank, dr["offgroup"].ToString(), "Off Group");
            txtcreatedby_rank.Text = dr["CreatedBy"].ToString();
            txtcreatedon_rank.Text = dr["CreatedOn"].ToString();
            txtmodifiedby_rank.Text = dr["ModifiedBy"].ToString();
            txtmodifiedon_rank.Text = dr["ModifiedOn"].ToString();
            ddstatus_rank.SelectedValue = dr["StatusId"].ToString();
            ddOffCrew_rank.SelectedValue = dr["OffCrew"].ToString();  

            ddOffCrew_SireRankType.SelectedValue = dr["SireRankType"].ToString();
            ddOffCrew_SireRankType_SelectedIndexChanged(new object(), new EventArgs()); 
            ddOffCrew_SireRank.SelectedValue = dr["SireRank"].ToString();

            txtRankMum.Text = dr["Rank_Mum"].ToString(); 
            txt_RankLevel.Text = dr["RankLevel"].ToString();
            txtSMOUCode.Text = dr["SMOU_Code"].ToString();
        }
        if (Mess.Length > 0)
        {
            this.lbl_Rank_Message.Text = "The Status Of Following Has Been Changed To In-Active <br/>" + Mess.Substring(1);
        }
    }
    protected void btn_Print_Rank_Click(object sender, EventArgs e)
    {

    }
    protected void GvRank_Row_Editing(object sender, GridViewEditEventArgs e)
    {
        Mode = "Edit";
        HiddenField hfdrank;
        hfdrank = (HiddenField)GvRank.Rows[e.NewEditIndex].FindControl("HiddenrankId");
        id = Convert.ToInt32(hfdrank.Value.ToString());
        Show_Record_rank(id);
        GvRank.SelectedIndex = e.NewEditIndex;
        Alerts.ShowPanel(Rankpanel);
        Alerts.HANDLE_AUTHORITY(5, btn_rank_add, btn_rank_save, btn_rank_Cancel, btn_Print_Rank, Auth);     
     
    }
    protected void GvRank_Row_Deleting(object sender, GridViewDeleteEventArgs e)
    {
        int intModifiedBy = Convert.ToInt32(Session["loginid"].ToString());
        HiddenField hfddel;
        hfddel = (HiddenField)GvRank.Rows[e.RowIndex].FindControl("HiddenrankId");
        id = Convert.ToInt32(hfddel.Value.ToString());
        Rank.deleteRankDetails("deleterank", id, intModifiedBy);
        BindGridRank();
        if (HiddenRankpk.Value.Trim() == hfddel.Value.ToString())
        {
            btn_rank_add_Click(sender, e);
        }
    }
    protected void GvRank_DataBound(object sender, EventArgs e)
    {
        Alerts.HANDLE_GRID(GvRank, Auth);  
    }
    protected void GvRank_PreRender(object sender, EventArgs e)
    {
        if (this.GvRank.Rows.Count <= 0)
        {
            lblRank.Text = "No Records Found..!";
        }
    }
    protected void btn_rank_add_Click(object sender, EventArgs e)
    {
        HiddenRankpk.Value = "";
       
        txtrankcode.Text = "";

        txtrankname.Text = "";
        ddrankgroupid.SelectedIndex=0;
        ddOffCrew_rank.SelectedIndex = 0;
        ddOffGroup_rank.SelectedIndex = 0;
        txtcreatedby_rank.Text = "";
        txt_RankLevel.Text = "";
        txtcreatedon_rank.Text = "";
        txtmodifiedby_rank.Text = "";
        txtmodifiedon_rank.Text = "";
        txtRankMum.Text = "";
        txtSMOUCode.Text = "";  
        GvRank.SelectedIndex = -1;
        ddstatus_rank.SelectedIndex = 0;

        Alerts.ShowPanel(Rankpanel);
        Alerts.HANDLE_AUTHORITY(2, btn_rank_add, btn_rank_save, btn_rank_Cancel, btn_Print_Rank, Auth);    
 
    }
    protected void btn_rank_save_Click(object sender, EventArgs e)
    {
        int Duplicate=0;

        foreach (GridViewRow dg in GvRank.Rows)
            {
                HiddenField hfd;
                HiddenField hfd1;
                hfd = (HiddenField)dg.FindControl("HiddenRankName");
                hfd1 = (HiddenField)dg.FindControl("HiddenrankId");

                if (hfd.Value.ToString().ToUpper().Trim() == txtrankname.Text.ToUpper().Trim())
                {
                    if (HiddenRankpk.Value.Trim() == "")
                    {
                        lbl_Rank_Message.Text = "Already Entered.";
                        Duplicate = 1;
                        break;
                    }
                    else if (HiddenRankpk.Value.Trim() != hfd1.Value.ToString())
                    {
                        lbl_Rank_Message.Text = "Already Entered.";
                        Duplicate = 1;
                        break;
                    }
                }
                else
                {
                    lbl_Rank_Message.Text = "";
                }
            }
            if (Duplicate == 0)
            {
                int rankId = -1;
                int createdby = 0, modifiedby = 0;
              

                int rankgroupid = Convert.ToInt32(ddrankgroupid.SelectedValue);
                int ranklevel = Convert.ToInt32(txt_RankLevel.Text);

                string strrankcode = txtrankcode.Text;
                string strrankName = txtrankname.Text;
                char offcrew = Convert.ToChar(ddOffCrew_rank.SelectedValue);
                char offgroup = Convert.ToChar(ddOffGroup_rank.SelectedValue);
                char status = Convert.ToChar(ddstatus_rank.SelectedValue);
                if (HiddenRankpk.Value.Trim() == "")
                {
                    createdby = Convert.ToInt32(Session["loginid"].ToString());
                }
                else
                {
                    rankId = Convert.ToInt32(HiddenRankpk.Value);
                    modifiedby = Convert.ToInt32(Session["loginid"].ToString());
                }
                Rank.insertUpdateRankDetails("InsertUpdateRankDetails",
                                              rankId,
                                              rankgroupid,
                                              strrankcode,
                                              strrankName,
                                              offcrew,
                                              offgroup,
                                              createdby,
                                              modifiedby,
                                              status,
                                              ranklevel, txtRankMum.Text.Trim(), txtSMOUCode.Text.Trim(),ddOffCrew_SireRankType.SelectedValue,ddOffCrew_SireRank.SelectedValue);
                BindGridRank();
                lbl_Rank_Message.Text = "Record Successfully Saved.";
                Alerts.HidePanel(Rankpanel);
                Alerts.HANDLE_AUTHORITY(3, btn_rank_add, btn_rank_save, btn_rank_Cancel, btn_Print_Rank, Auth);    
           
            }
    }
    protected void btn_rank_Cancel_Click(object sender, EventArgs e)
    {
       
        GvRank.SelectedIndex = -1;
        Alerts.HidePanel(Rankpanel);
        Alerts.HANDLE_AUTHORITY(6, btn_rank_add, btn_rank_save, btn_rank_Cancel, btn_Print_Rank, Auth);     
   
    }
    protected void ddOffCrew_SireRankType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddOffCrew_SireRank.Items.Clear();
        ddOffCrew_SireRank.Items.Add(new ListItem("< Select >", ""));  
        
        if (ddOffCrew_SireRankType.SelectedIndex == 1) //Officer
        {
            ddOffCrew_SireRank.Items.Add(new ListItem("Master", "Master"));
            ddOffCrew_SireRank.Items.Add(new ListItem("Chief Officer", "Chief Officer"));
            ddOffCrew_SireRank.Items.Add(new ListItem("2nd Officer", "2nd Officer"));
            ddOffCrew_SireRank.Items.Add(new ListItem("3rd Officer", "3rd Officer"));
            ddOffCrew_SireRank.Items.Add(new ListItem("4th Officer ", "4th Officer"));  
        }
        else if (ddOffCrew_SireRankType.SelectedIndex == 2)
        {
            ddOffCrew_SireRank.Items.Add(new ListItem("Chief Engineer", "Chief Engineer"));
            ddOffCrew_SireRank.Items.Add(new ListItem("1st Engineer", "1st Engineer"));
            ddOffCrew_SireRank.Items.Add(new ListItem("2nd Engineer", "2nd Engineer"));
            ddOffCrew_SireRank.Items.Add(new ListItem("3rd Engineer", "3rd Engineer"));
            ddOffCrew_SireRank.Items.Add(new ListItem("4th Engineer", "4th Engineer"));
            ddOffCrew_SireRank.Items.Add(new ListItem("5th Engineer", "5th Engineer"));
            ddOffCrew_SireRank.Items.Add(new ListItem("Gas/Cargo Engineer", "Gas/Cargo Engineer"));
            ddOffCrew_SireRank.Items.Add(new ListItem("Electrical Engineer", "Electrical Engineer"));
            ddOffCrew_SireRank.Items.Add(new ListItem("ETO", "ETO"));
            ddOffCrew_SireRank.Items.Add(new ListItem("Junior Engineer", "Junior Engineer"));
            ddOffCrew_SireRank.Items.Add(new ListItem("Assistant Engineer", "Assistant Engineer"));
            ddOffCrew_SireRank.Items.Add(new ListItem("Electrical Officer", "Electrical Officer"));
        }
        ddOffCrew_SireRank.SelectedIndex = 0;   
    }

    protected void GvRank_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Modify")
        {
            GridViewRow row = (GridViewRow)(((ImageButton)sender).NamingContainer);
            int Rowindx = row.RowIndex;
            Mode = "Edit";
            HiddenField hfdrank;
            hfdrank = (HiddenField)GvRank.Rows[Rowindx].FindControl("hdnRankId");
            id = Convert.ToInt32(hfdrank.Value.ToString());
            Show_Record_rank(id);
            GvRank.SelectedIndex = Rowindx;
            Alerts.ShowPanel(Rankpanel);
            Alerts.HANDLE_AUTHORITY(5, btn_rank_add, btn_rank_save, btn_rank_Cancel, btn_Print_Rank, Auth);
        }
    }
    protected void btnEditRank_click(object sender, ImageClickEventArgs e)
    {
        GridViewRow row = (GridViewRow)(((ImageButton)sender).NamingContainer);
        int Rowindx = row.RowIndex;
        Mode = "Edit";
        HiddenField hfdrank;
        hfdrank = (HiddenField)GvRank.Rows[Rowindx].FindControl("hdnRankId");
        id = Convert.ToInt32(hfdrank.Value.ToString());
        Show_Record_rank(id);
        GvRank.SelectedIndex = Rowindx;
        Alerts.ShowPanel(Rankpanel);
        Alerts.HANDLE_AUTHORITY(5, btn_rank_add, btn_rank_save, btn_rank_Cancel, btn_Print_Rank, Auth);
    }
}
