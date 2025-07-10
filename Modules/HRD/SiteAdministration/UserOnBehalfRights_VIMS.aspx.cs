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

public partial class SiteAdministration_UserRightsRoles_VIMS : System.Web.UI.Page
{
    int id;
    protected void Page_Load(object sender, EventArgs e)
    {
        Session.Add("User_id", Session["loginid"].ToString());
        if (!Page.IsPostBack)
        {
            BindGridAssgnUser();
            //DataTable dsadmin = UserRights.selectadminroleId(Convert.ToInt32(Session["User_id"].ToString()));
            //ViewState.Add("RoleId", dsadmin.Rows[0][0].ToString());
            //DataTable ds12 = UserRights.selectsuperuser(Convert.ToInt32(Session["User_id"].ToString()));
            //ViewState.Add("SuperUser", ds12.Rows[0][0].ToString());
            Session["PageCodeRights"] = "2";
        }
        else
        {
            Session.Remove("PageCodeRights");
        }
    }
    private void BindGridAssgnUser()
    {
        DataTable dtu = UserRights_VIMS.selectUserNames_ForVIMS();
        dtu.Rows[0].Delete();
        this.GvAssgUser.DataSource = dtu;
        this.GvAssgUser.DataBind();
    }
    private void BindGridOnbehalfUser()
    {
        DataTable dtu = UserRights_VIMS.selectUserNames_ForVIMS();
        dtu.Rows[0].Delete();
        this.GVOnbehalfUser.DataSource = dtu;
        this.GVOnbehalfUser.DataBind();
    }
    protected void GvAssgUser_doselect(object sender, EventArgs e)
    {
        int login1id;
        HiddenField hfdassgnuserId;
        hfdassgnuserId = (HiddenField)GvAssgUser.Rows[GvAssgUser.SelectedIndex].FindControl("HiddenLoginId");
        id = Convert.ToInt32(hfdassgnuserId.Value.ToString());
        BindGridOnbehalfUser();
        lblupdation.Visible = false;
        DataTable dto = UserRights_VIMS.selectUserOnBehalf_ForVIMS(id);
        foreach (DataRow dr in dto.Rows)
        {
            foreach (GridViewRow dg in GVOnbehalfUser.Rows)
            {
                HiddenField hfdLogin1Id;
                hfdLogin1Id = (HiddenField)dg.FindControl("HiddenLogin1Id");
                login1id = Convert.ToInt32(hfdLogin1Id.Value.ToString());
                if (Convert.ToInt32(dr["OnBehalfUserId"].ToString()) == login1id)
                {
                    //if (dr["AssignedUser"].ToString() == 'Y'.ToString())
                    //{
                        CheckBox chk = new CheckBox();
                        chk = (CheckBox)dg.FindControl("chkAssign");
                        chk.Checked = true;
                    //}
                }
            }
        }
    }
    protected void btn_Authority_save_Click(object sender, EventArgs e)
    {
        HiddenField hfdassgnuserId;
        HiddenField hfd;
        hfdassgnuserId = (HiddenField)GvAssgUser.Rows[GvAssgUser.SelectedIndex].FindControl("HiddenLoginId");
        int AssgnUserid = Convert.ToInt32(hfdassgnuserId.Value.ToString());
        int OnbehalfUserid;
        UserRights_VIMS.deleteUserOnbehalf("DeleteUserOnBehalf_VIMS", AssgnUserid);

        foreach (GridViewRow dg in GVOnbehalfUser.Rows)
        {
            CheckBox chk = new CheckBox();
            chk = (CheckBox)dg.FindControl("chkAssign");
            if (chk.Checked == true)
            {
                hfd = (HiddenField)dg.FindControl("HiddenLogin1Id");
                OnbehalfUserid = Convert.ToInt32(hfd.Value.ToString());

                UserRights_VIMS.InsertUpdateUserOnbehalfDetails_ForVIMS("InsertUpdateUserOnbehalfRights_VIMS",
                                                           AssgnUserid,
                                                           OnbehalfUserid);
            }
        }
        lblupdation.Visible = true;
        lblupdation.Text = "Record Updated.";
    }
    protected void btn_Authority_Reset_Click(object sender, EventArgs e)
    {
        lblupdation.Visible = false;
        foreach (GridViewRow dg in GVOnbehalfUser.Rows)
        {
            CheckBox chk = new CheckBox();
            chk = (CheckBox)dg.FindControl("chkAssign");
            chk.Checked = false;
        }
    }
}
