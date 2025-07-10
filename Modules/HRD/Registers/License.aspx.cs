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

public partial class Registers_License : System.Web.UI.Page
{
    int id;
    Authority Auth;
    string Mode = "New";
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        lbl_License_Message.Text = "";
        lblLicense.Text = "";
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
            BindOffCrewDropDown();
            BindOffGroupDropDown();
            BindStatusDropDown();
            BindGridLicense();
            
            Alerts.HidePanel(Licensepanel);
            Alerts.HANDLE_AUTHORITY(1, btn_License_add, btn_License_save, btn_License_Cancel, btn_Print_License, Auth);
        }
    }
    private void BindOffCrewDropDown()
    {
        DataTable dt1 =License.selectDataOffCrew();
        this.ddOffCrew_License.DataValueField = "OffCrewId";
        this.ddOffCrew_License.DataTextField = "OffCrewName";
        this.ddOffCrew_License.DataSource = dt1;
        this.ddOffCrew_License.DataBind();
    }
    private void BindOffGroupDropDown()
    {
        DataTable dt2 = License.selectDataOffGroup();
        this.ddOffGroup_License.DataValueField = "OffGroupId";
        this.ddOffGroup_License.DataTextField = "OffGroupName";
        this.ddOffGroup_License.DataSource = dt2;
        this.ddOffGroup_License.DataBind();
    }
    private void BindStatusDropDown()
    {
        DataTable dt3 = License.selectDataStatus();
        this.ddstatus_license.DataValueField = "StatusId";
        this.ddstatus_license.DataTextField = "StatusName";
        this.ddstatus_license.DataSource = dt3;
        this.ddstatus_license.DataBind();
    }
    private void BindRankDropDown(char Offcrew, char Offgroup)
    {
        DataTable dt21 = License.selectDataRank(Offcrew,Offgroup);
        this.ddl_Rank_License.DataValueField = "RankId";
        this.ddl_Rank_License.DataTextField = "RankCode";
        this.ddl_Rank_License.DataSource = dt21;
        this.ddl_Rank_License.DataBind();
    }
    private void BindGridLicense()
    {
        string s;
        s = txt_Licence.Text.Trim();  
        DataTable dt = License.selectDataLicenseDetails(s);
        this.GvLicense.DataSource = dt;
        this.GvLicense.DataBind();
    }
    protected void GvLicense_Row_Editing(object sender, GridViewEditEventArgs e)
    {
        HiddenField hfdLicense;
        hfdLicense = (HiddenField)GvLicense.Rows[e.NewEditIndex].FindControl("HiddenLicenseId");
        id = Convert.ToInt32(hfdLicense.Value.ToString());
        Show_Record_licence(id);
        GvLicense.SelectedIndex = e.NewEditIndex;
       
        Alerts.ShowPanel(Licensepanel);
        Alerts.HANDLE_AUTHORITY(5, btn_License_add, btn_License_save, btn_License_Cancel, btn_Print_License, Auth);
    }
    protected void GvLicense_Row_Deleting(object sender, GridViewDeleteEventArgs e)
    {
        int modifiedby = Convert.ToInt32(Session["loginid"].ToString());
        HiddenField hfddel;
        hfddel = (HiddenField)GvLicense.Rows[e.RowIndex].FindControl("HiddenLicenseId");
        id = Convert.ToInt32(hfddel.Value.ToString());
        License.deleteLicenceDetails("deleteLicense", id,modifiedby);
        BindGridLicense();
        if (HiddenLicensepk.Value.Trim() == hfddel.Value.ToString())
        {
            btn_License_add_Click(sender,e);
        }

    }
    protected void GvLicense_DataBound(object sender, EventArgs e)
    {
        Alerts.HANDLE_GRID(GvLicense, Auth);
    }
    protected void GvLicense_PreRender(object sender, EventArgs e)
    {
        if (this.GvLicense.Rows.Count <= 0)
        {
            lblLicense.Text = "No Records Found..!";
        }
        
    }
    protected void GvLicense_SelectIndexChanged(object sender, EventArgs e)
    {
      
        HiddenField hfdlicense;
        hfdlicense = (HiddenField)GvLicense.Rows[GvLicense.SelectedIndex].FindControl("HiddenLicenseId");
        id = Convert.ToInt32(hfdlicense.Value.ToString());
        Show_Record_licence(id);
        //----------------
        Alerts.ShowPanel(Licensepanel);
        Alerts.HANDLE_AUTHORITY(4, btn_License_add, btn_License_save, btn_License_Cancel, btn_Print_License, Auth);
    }
    protected void Show_Record_licence(int licenceid)
    {
        string Mess="";
        
       
        char a,b;
        //string b;
        HiddenLicensepk.Value = licenceid.ToString();
        DataTable dt3 =License.selectDataLicenceDetailsByLicenceId(licenceid);
        foreach (DataRow dr in dt3.Rows)
        {
          
            txtLicenseType.Text = dr["licensetype"].ToString();
            txtLicenseName.Text = dr["licensename"].ToString();
            ddOffCrew_License.SelectedValue = dr["offcrew"].ToString();
            ddOffGroup_License.SelectedValue = dr["offgroup"].ToString();
            BindRankDropDown(Convert.ToChar(ddOffCrew_License.SelectedValue), Convert.ToChar(ddOffGroup_License.SelectedValue));
            if (ddOffCrew_License.SelectedIndex <= 0 && ddOffGroup_License.SelectedIndex <= 0)
            {
                ddl_Rank_License.Items.Clear();
            }
            else
            {
                Mess = Mess + Alerts.Set_DDL_Value(ddl_Rank_License, dr["RankId"].ToString(), "Rank");
            }
            a = Convert.ToChar(dr["expires"].ToString());
            if (a == 'Y')
            {
                Chkexpires_License.Checked = true;
            }
            else
            {
                Chkexpires_License.Checked = false;
            }
            b = Convert.ToChar(dr["coc"].ToString());
            //b = dr["coc"].ToString();
            if (b == 'Y')
            {
                chk_COC.Checked = true;
            }
            else
            {
                chk_COC.Checked = false;
            }
            txtcreatedby_license.Text = dr["CreatedBy"].ToString();
            txtcreatedon_license.Text = dr["CreatedOn"].ToString();
            txtmodifiedby_license.Text = dr["ModifiedBy"].ToString();
            txtmodifiedon_license.Text = dr["ModifiedOn"].ToString();
            ddstatus_license.SelectedValue = dr["StatusId"].ToString();
        }
      
        if (Mess.Length > 0)
        {
            this.lbl_License_Message.Text = "The Status Of Following Has Been Changed To In-Active <br/>" + Mess.Substring(1);
           
        }
    }
    protected void btn_License_add_Click(object sender, EventArgs e)
    {
        HiddenLicensepk.Value = "";
        txtLicenseName.Text = "";
        txtLicenseType.Text = "";
        ddOffCrew_License.SelectedIndex = 0;
        ddOffGroup_License.SelectedIndex = 0;
        GvLicense.SelectedIndex = -1;
        txtcreatedby_license.Text = "";
        txtcreatedon_license.Text = "";
        txtmodifiedby_license.Text = "";
        txtmodifiedon_license.Text = "";
        ddstatus_license.SelectedIndex = 0;
        Chkexpires_License.Checked = false;
        chk_COC.Checked = false;
        ddl_Rank_License.Items.Clear();
       
        Alerts.ShowPanel(Licensepanel);
        Alerts.HANDLE_AUTHORITY(2, btn_License_add, btn_License_save, btn_License_Cancel, btn_Print_License, Auth);
      
    }
    protected void btn_License_save_Click(object sender, EventArgs e)
    {
        int Duplicate=0;
        if (HiddenLicensepk.Value == "0")
        {
            foreach (GridViewRow dg in GvLicense.Rows)
            {
                HiddenField hfd;
                HiddenField hfd1;
                hfd = (HiddenField)dg.FindControl("HiddenLicenseName");
                hfd1 = (HiddenField)dg.FindControl("HiddenLicenseId");

                if (hfd.Value.ToString().ToUpper().Trim() == txtLicenseName.Text.ToUpper().Trim())
                {
                    if (HiddenLicensepk.Value.Trim() == "")
                    {
                        Label2.Text = "Already Entered.";
                        Duplicate = 1;
                        break;
                    }
                    else if (HiddenLicensepk.Value.Trim() != hfd1.Value.ToString())
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
        }
            if (Duplicate == 0)
            {
                char exp;
                char coc;
                int licenseId = -1,rank;

                int createdby = 0, modifiedby = 0;
              
                string strlicensetype = txtLicenseType.Text;
                string strlicenseName = txtLicenseName.Text;
                char offcrew = Convert.ToChar(ddOffCrew_License.SelectedValue);
                char offgroup = Convert.ToChar(ddOffGroup_License.SelectedValue);
                char status = Convert.ToChar(ddstatus_license.SelectedValue);
                if (ddl_Rank_License.Text == "")
                {
                    rank = 0;
                }
                else
                {
                    rank = Convert.ToInt32(ddl_Rank_License.SelectedValue);
                }

                if (Chkexpires_License.Checked == true)
                {
                    exp = 'Y';
                }
                else
                {
                    exp = 'N';
                }
                if (chk_COC.Checked == true)
                {
                    coc = 'Y';
                }
                else
                {
                    coc='N';
                }
                if (HiddenLicensepk.Value.Trim() == "")
                {
                    createdby = Convert.ToInt32(Session["loginid"].ToString());
                }
                else
                {
                    licenseId = Convert.ToInt32(HiddenLicensepk.Value);
                    modifiedby = Convert.ToInt32(Session["loginid"].ToString());
                }
                License.insertUpdateLicenceDetails("InsertUpdateLicenseDetails",
                                                     licenseId,
                                                     strlicensetype,
                                                     strlicenseName,
                                                     offcrew,
                                                     offgroup,
                                                     exp,
                                                     coc,
                                                     rank,
                                                     createdby,
                                                     modifiedby,
                                                     status);
                BindGridLicense();
                lbl_License_Message.Text = "Record Successfully Saved.";
               
                Alerts.HidePanel(Licensepanel);
                Alerts.HANDLE_AUTHORITY(3, btn_License_add, btn_License_save, btn_License_Cancel, btn_Print_License, Auth);
            }
    }
    protected void btn_License_Cancel_Click(object sender, EventArgs e)
    {
        GvLicense.SelectedIndex = -1;
       
        Alerts.HidePanel(Licensepanel);
        Alerts.HANDLE_AUTHORITY(6, btn_License_add, btn_License_save, btn_License_Cancel, btn_Print_License, Auth);
    }
    protected void btn_Print_License_Click(object sender, EventArgs e)
    {

    }
    protected void ddOffCrew_License_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindRankDropDown(Convert.ToChar(ddOffCrew_License.SelectedValue),Convert.ToChar(ddOffGroup_License.SelectedValue));
    }
    protected void ddOffGroup_License_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindRankDropDown(Convert.ToChar(ddOffCrew_License.SelectedValue), Convert.ToChar(ddOffGroup_License.SelectedValue));
    }
    protected void btn_Search_Click(object sender, EventArgs e)
    {
        BindGridLicense();
    }

    protected void GvLicense_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Modify")
        {
            GridViewRow row = (GridViewRow)(((ImageButton)sender).NamingContainer);
            int Rowindx = row.RowIndex;
            Mode = "Edit";
            HiddenField hfdLicense;
            hfdLicense = (HiddenField)GvLicense.Rows[Rowindx].FindControl("hdnLicenseId");
            id = Convert.ToInt32(hfdLicense.Value.ToString());
            Show_Record_licence(id);
            GvLicense.SelectedIndex = Rowindx;

            Alerts.ShowPanel(Licensepanel);
            Alerts.HANDLE_AUTHORITY(5, btn_License_add, btn_License_save, btn_License_Cancel, btn_Print_License, Auth);
        }
    }
    protected void btnEditLicense_click(object sender, ImageClickEventArgs e)
    {
        GridViewRow row = (GridViewRow)(((ImageButton)sender).NamingContainer);
        int Rowindx = row.RowIndex;
        Mode = "Edit";
        HiddenField hfdLicense;
        hfdLicense = (HiddenField)GvLicense.Rows[Rowindx].FindControl("hdnLicenseId");
        id = Convert.ToInt32(hfdLicense.Value.ToString());
        Show_Record_licence(id);
        GvLicense.SelectedIndex = Rowindx;

        Alerts.ShowPanel(Licensepanel);
        Alerts.HANDLE_AUTHORITY(5, btn_License_add, btn_License_save, btn_License_Cancel, btn_Print_License, Auth);
    }
}
