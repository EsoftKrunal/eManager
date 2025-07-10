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

public partial class SiteAdministration_UserVesselRelation : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string id = Session["loginid"].ToString();
        if (!Page.IsPostBack)
        {
            BindUserNamesDropDown();
            BindGriduservessel();
        }
    }
    private void BindUserNamesDropDown()
    {
        DataTable dt = UserVesselRelation.selectDataLoginId();
        this.dd_usernames.DataValueField = "LoginId";
        this.dd_usernames.DataTextField = "UserName";
        this.dd_usernames.DataSource = dt;
        this.dd_usernames.DataBind();
    }
    private void BindGriduservessel()
    {
        DataTable dt1 = UserVesselRelation.selectDataUserVesselRelationDetails();
        this.GVVesselUsers.DataSource = dt1;
        this.GVVesselUsers.DataBind();
    }
    protected void btn_Authority_save_Click(object sender, EventArgs e)
    {
        int userid = Convert.ToInt32(dd_usernames.SelectedValue);
        UserVesselRelation.deleteUserVesselRelationDetails("deleteUserVesselRelation", userid);

        CheckBox chk = new CheckBox();
        foreach (GridViewRow dg in GVVesselUsers.Rows)
        {
            chk = (CheckBox)dg.FindControl("chkSelect");
            
                if (chk.Checked == true)
                {
                    HiddenField hfd;
                    hfd = (HiddenField)dg.FindControl("HiddenVesselId");
                    int vesselid = Convert.ToInt32(hfd.Value.ToString());
                    UserVesselRelation.insertUpdateUserVesselRelationDetails("InsertUpdateUserVesselRelation",
                                                                             userid,
                                                                             vesselid);
                }
            
        }
        dd_usernames_SelectedIndexChanged(sender, e);
        lblupdation.Visible = true;
    }
    protected void btn_Authority_Reset_Click(object sender, EventArgs e)
    {
        lblupdation.Visible = false;
        dd_usernames.SelectedIndex = 0;
        foreach (GridViewRow dg in GVVesselUsers.Rows)
        {
            CheckBox chk = new CheckBox();
            chk = (CheckBox)dg.FindControl("chkSelect");
            chk.Checked = false;
            
        }
    }
    protected void dd_usernames_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblupdation.Visible = false;
        BindGriduservessel();
        int _LoginId;
        _LoginId =Convert.ToInt32(dd_usernames.SelectedValue);
        DataTable dt2 = UserVesselRelation.selectDataUserVessel(_LoginId);
        if (dt2.Rows.Count != 0)
        {
            foreach (DataRow dr in dt2.Rows)
            {
                foreach (GridViewRow dg in GVVesselUsers.Rows)
                {
                    
                    CheckBox chk = new CheckBox();
                    chk = (CheckBox)dg.FindControl("chkSelect");
                    if (dr["LoginId"].ToString() == _LoginId.ToString())
                    {
                        HiddenField hid;
                        hid = ((HiddenField)dg.FindControl("HiddenVesselId"));
                        
                        if (hid.Value.ToString() == dr["VesselId"].ToString())
                        {
                            chk.Checked = true;
                        }
                        
                       
                    }
                    else
                    {
                        chk.Checked = false;
                    }
                }
            }
        }
        
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("AdminDashBoard.aspx");
    }
}
