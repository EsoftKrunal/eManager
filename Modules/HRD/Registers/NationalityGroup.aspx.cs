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

public partial class Registers_NationalityGroup : System.Web.UI.Page
{
    int id;
    Authority Auth;
    string Mode = "New";
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        lbl_Nationality_Message.Text = "";
        lblNationalityGroup.Text = "";
        Label2.Text = "";
        
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
            BindStatusDropDown();
            BindGridNationalityGroup();
            BindGridNatioanlityGroupNationalityRelation();
            Alerts.HidePanel(Nationalitygrouppanel);
            Alerts.HANDLE_AUTHORITY(1, btn_Nationality_group_add, btn_Nationality_group_save, btn_Nationality_group_Cancel, btn_Print_NationalityGroup, Auth);
        }
    }
    private void BindStatusDropDown()
    {
        DataTable dt2 = NationalityGroup.selectDataStatus();
        this.ddstatus_Nationality_group.DataValueField = "StatusId";
        this.ddstatus_Nationality_group.DataTextField = "StatusName";
        this.ddstatus_Nationality_group.DataSource = dt2;
        this.ddstatus_Nationality_group.DataBind();
    }
    private void BindGridNationalityGroup()
    {
        DataTable dt = NationalityGroup.selectDataNationalityGroupDetails();
        this.GvNationality_Group.DataSource = dt;
        this.GvNationality_Group.DataBind();
    }
    private void BindGridNatioanlityGroupNationalityRelation()
    {
        DataTable dt11 = NationalityGroup.selectDataNationalityNameDetails();
        this.GvRelation.DataSource = dt11;
        this.GvRelation.DataBind();
        
    }
    protected void GvNationality_Group_SelectIndexChanged(object sender, EventArgs e)
    {
        HiddenField hfdNationality;
        hfdNationality = (HiddenField)GvNationality_Group.Rows[GvNationality_Group.SelectedIndex].FindControl("HiddenNationalityGroupId");
        id = Convert.ToInt32(hfdNationality.Value.ToString());
        Show_Record_Nationality_group(id);
       
        Alerts.ShowPanel(Nationalitygrouppanel);
        Alerts.HANDLE_AUTHORITY(4, btn_Nationality_group_add, btn_Nationality_group_save, btn_Nationality_group_Cancel, btn_Print_NationalityGroup, Auth);
    }
    protected void Show_Record_Nationality_group(int Nationalitygroupid)
    {
        HiddenNationalityGrouppk.Value = Nationalitygroupid.ToString();
        DataTable dt3 = NationalityGroup.selectDataNationalityGroupDetailsByNationalityGroupId(Nationalitygroupid);
        foreach (DataRow dr in dt3.Rows)
        {
            txtNationalitygroupname.Text = dr["NationalityGroupName"].ToString();
            txtcreatedby_Nationality_group.Text = dr["CreatedBy"].ToString();
            txtcreatedon_Nationality_group.Text = dr["CreatedOn"].ToString();
            txtmodifiedby_Nationality_group.Text = dr["ModifiedBy"].ToString();
            txtmodifiedon_Nationality_group.Text = dr["ModifiedOn"].ToString();
            ddstatus_Nationality_group.SelectedValue = dr["StatusId"].ToString();
        }

        BindGridNatioanlityGroupNationalityRelation();
        DataTable dt12 = NationalityGroup.selectDataNationalityGroupNationalityRelationDetailsByNationalityGroupId(Nationalitygroupid);
        foreach (DataRow dr in dt12.Rows)
        {
            foreach (GridViewRow dg in GvRelation.Rows)
            {
                HiddenField hid;
                hid = ((HiddenField)dg.FindControl("hfdCountryId"));

                if (dr["NationalityId"].ToString() == hid.Value)
                {
                    CheckBox chk = new CheckBox();
                    chk = (CheckBox)dg.FindControl("chkNationalityName");
                    chk.Checked = true;
                }
            }
        }
    }
    protected void GvNationality_Group_Row_Editing(object sender, GridViewEditEventArgs e)
    {
        HiddenField hfdNationality;
        hfdNationality = (HiddenField)GvNationality_Group.Rows[e.NewEditIndex].FindControl("HiddenNationalityGroupId");
        id = Convert.ToInt32(hfdNationality.Value.ToString());
        Show_Record_Nationality_group(id);
        GvNationality_Group.SelectedIndex = e.NewEditIndex;
    
        Alerts.ShowPanel(Nationalitygrouppanel);
        Alerts.HANDLE_AUTHORITY(5, btn_Nationality_group_add, btn_Nationality_group_save, btn_Nationality_group_Cancel, btn_Print_NationalityGroup, Auth);
    }
    protected void GvNationality_Group_Row_Deleting(object sender, GridViewDeleteEventArgs e)
    {
        int modifiedby = Convert.ToInt32(Session["loginid"].ToString());
        HiddenField hfddel;
        hfddel = (HiddenField)GvNationality_Group.Rows[e.RowIndex].FindControl("HiddenNationalityGroupId");
        id = Convert.ToInt32(hfddel.Value.ToString());
        NationalityGroup.deleteNationalityGroupDetails("deleteNationalityGroup", id,modifiedby);
        BindGridNationalityGroup();
        if (HiddenNationalityGrouppk.Value.Trim() == hfddel.Value.ToString())
        {
            btn_Nationality_group_add_Click(sender, e);
        }
    }
    protected void GvNationality_Group_DataBound(object sender, EventArgs e)
    {
        Alerts.HANDLE_GRID(GvNationality_Group, Auth);
    }
    protected void GvNationality_Group_PreRender(object sender, EventArgs e)
    {
        if (this.GvNationality_Group.Rows.Count <= 0)
        {
            lblNationalityGroup.Text = "No Records Found..!";
        }
        else
        {
            lblNationalityGroup.Text = "";
        }
    }
    protected void btn_Nationality_group_add_Click(object sender, EventArgs e)
    {
        HiddenNationalityGrouppk.Value = "";
        txtNationalitygroupname.Text = "";
        CheckBox chk = new CheckBox();
        foreach (GridViewRow dg in GvRelation.Rows)
        {
            chk = (CheckBox)dg.FindControl("chkNationalityName");
            chk.Checked = false;
        }
        txtcreatedby_Nationality_group.Text = "";
        txtcreatedon_Nationality_group.Text = "";
        txtmodifiedby_Nationality_group.Text = "";
        txtmodifiedon_Nationality_group.Text = "";
        ddstatus_Nationality_group.SelectedIndex = 0;
        GvNationality_Group.SelectedIndex = -1;
       
        Alerts.ShowPanel(Nationalitygrouppanel);
        Alerts.HANDLE_AUTHORITY(2, btn_Nationality_group_add, btn_Nationality_group_save, btn_Nationality_group_Cancel, btn_Print_NationalityGroup, Auth);
    }
    protected void btn_Nationality_group_save_Click(object sender, EventArgs e)
    {
        int Duplicate=0;

        foreach (GridViewRow dg in GvNationality_Group.Rows)
            {
                HiddenField hfd;
                HiddenField hfd1;
                hfd = (HiddenField)dg.FindControl("HiddenNationalityGroupName");
                hfd1 = (HiddenField)dg.FindControl("HiddenNationalityGroupId");

                if (hfd.Value.ToString().ToUpper().Trim() == txtNationalitygroupname.Text.ToUpper().Trim())
                {
                    if (HiddenNationalityGrouppk.Value.Trim() == "")
                    {
                        Label2.Text = "Already Entered.";
                        Duplicate = 1;
                        break;
                    }
                    else if (HiddenNationalityGrouppk.Value.Trim() != hfd1.Value.ToString())
                    {
                        Label2.Text = "Already Entered.";
                        Duplicate = 1;
                        break;
                    }
                }
                else
                {
                    Label2.Text = "";
                }
            }
            if (Duplicate == 0)
            {
                int NationalitygroupId = -1;
                int createdby = 0, modifiedby = 0;
                int rowId = 0;
                string strNationalitygroupName = txtNationalitygroupname.Text;

                char status = Convert.ToChar(ddstatus_Nationality_group.SelectedValue);
                if (HiddenNationalityGrouppk.Value.Trim() == "")
                {
                    createdby = Convert.ToInt32(Session["loginid"].ToString());
                }
                else
                {
                    NationalitygroupId = Convert.ToInt32(HiddenNationalityGrouppk.Value);
                    modifiedby = Convert.ToInt32(Session["loginid"].ToString());
                    NationalityGroup.deleteNationalityGroupNationalityRelationDetails("deleteNationalityGroupNationalityRelation", NationalitygroupId);
                }
                NationalityGroup.insertUpdateNationalityGroupDetails("InsertUpdateNationalityGroupDetails",
                                                                      NationalitygroupId,
                                                                      strNationalitygroupName,
                                                                      createdby,
                                                                      modifiedby,
                                                                      status,
                                                                      out rowId);




                CheckBox chk = new CheckBox();
                foreach (GridViewRow dg in GvRelation.Rows)
                {
                    chk = (CheckBox)dg.FindControl("chkNationalityName");
                    if (chk.Checked == true)
                    {
                        HiddenField hfd;
                        hfd = (HiddenField)dg.FindControl("hfdCountryId");
                        int countryid = Convert.ToInt32(hfd.Value.ToString());
                        NationalityGroup.insertUpdateNationalityGroupNationalityRelationDetails("InsertUpdateNationalityGroupNationalityRelation",
                                                                                                                    rowId,
                                                                                                                    countryid);
                    }
                }


                BindGridNationalityGroup();
                lbl_Nationality_Message.Text = "Record Successfully Saved.";
               
                Alerts.HidePanel(Nationalitygrouppanel);
                Alerts.HANDLE_AUTHORITY(3, btn_Nationality_group_add, btn_Nationality_group_save, btn_Nationality_group_Cancel, btn_Print_NationalityGroup, Auth);
            }
    }
    protected void btn_Nationality_group_Cancel_Click(object sender, EventArgs e)
    {
        GvNationality_Group.SelectedIndex = -1;
      
        Alerts.HidePanel(Nationalitygrouppanel);
        Alerts.HANDLE_AUTHORITY(6, btn_Nationality_group_add, btn_Nationality_group_save, btn_Nationality_group_Cancel, btn_Print_NationalityGroup, Auth);
    }
    protected void btn_Print_NationalityGroup_Click(object sender, EventArgs e)
    {
        
    }

    protected void GvNationality_Group_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Modify")
        {
            GridViewRow row = (GridViewRow)(((ImageButton)sender).NamingContainer);
            int Rowindx = row.RowIndex;
            Mode = "Edit";
            HiddenField hfdNationality;
            hfdNationality = (HiddenField)GvNationality_Group.Rows[Rowindx].FindControl("hdnNationalityGroupId");
            id = Convert.ToInt32(hfdNationality.Value.ToString());
            Show_Record_Nationality_group(id);
            GvNationality_Group.SelectedIndex = Rowindx;

            Alerts.ShowPanel(Nationalitygrouppanel);
            Alerts.HANDLE_AUTHORITY(5, btn_Nationality_group_add, btn_Nationality_group_save, btn_Nationality_group_Cancel, btn_Print_NationalityGroup, Auth);
        }
    }
    protected void btnEditNationalityGroup_click(object sender, EventArgs e)
    {
        GridViewRow row = (GridViewRow)(((ImageButton)sender).NamingContainer);
        int Rowindx = row.RowIndex;
        Mode = "Edit";
        HiddenField hfdNationality;
        hfdNationality = (HiddenField)GvNationality_Group.Rows[Rowindx].FindControl("hdnNationalityGroupId");
        id = Convert.ToInt32(hfdNationality.Value.ToString());
        Show_Record_Nationality_group(id);
        GvNationality_Group.SelectedIndex = Rowindx;

        Alerts.ShowPanel(Nationalitygrouppanel);
        Alerts.HANDLE_AUTHORITY(5, btn_Nationality_group_add, btn_Nationality_group_save, btn_Nationality_group_Cancel, btn_Print_NationalityGroup, Auth);
    }
}
