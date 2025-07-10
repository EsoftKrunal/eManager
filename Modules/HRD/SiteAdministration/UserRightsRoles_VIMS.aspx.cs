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
            BindGridModulesList();
            //DataTable dsadmin = UserRights.selectadminroleId(Convert.ToInt32(Session["User_id"].ToString()));
            //ViewState.Add("RoleId", dsadmin.Rows[0][0].ToString());
            //DataTable ds12 = UserRights.selectsuperuser(Convert.ToInt32(Session["User_id"].ToString()));
            //ViewState.Add("SuperUser", ds12.Rows[0][0].ToString());
            Session["PageCodeRights"] = "1";
        }
        else
        {
            Session.Remove("PageCodeRights");
        }
    }
    private void BindGridModulesList()
    {
        DataTable dt = UserRights_VIMS.selectDataModulesDetails_ForVIMS();
        this.GvmoduleList.DataSource = dt;
        this.GvmoduleList.DataBind();
    }
    private void BindGridAuthorityDetails(int _id)
    {
        DataTable dt1 = UserRights_VIMS.selectDataAuthorityDetails_ForVIMS(_id);
        this.GVAuthority.DataSource = dt1;
        this.GVAuthority.DataBind();
    }
    protected void GvmoduleList_doselect(object sender, EventArgs e)
    {
        int login1id, roleid;
        HiddenField hfdapplicationmoduleId;
        hfdapplicationmoduleId = (HiddenField)GvmoduleList.Rows[GvmoduleList.SelectedIndex].FindControl("HiddenModuleId");
        id = Convert.ToInt32(hfdapplicationmoduleId.Value.ToString());
        BindGridAuthorityDetails(id);
        lblupdation.Visible = false;
        DataTable dt1 = UserRights_VIMS.selectDataAuthorityDetails_ForVIMS(id);
        foreach (DataRow dr in dt1.Rows)
        {
            foreach (GridViewRow dg in GVAuthority.Rows)
            {
                HiddenField hfdLogin1Id;
                hfdLogin1Id = (HiddenField)dg.FindControl("HiddenLogin1Id");
                login1id = Convert.ToInt32(hfdLogin1Id.Value.ToString());

                if (Convert.ToInt32(dr["LoginId"].ToString()) == login1id)
                {
                    if (dr["CanAdd"].ToString() == 'Y'.ToString())
                    {
                        CheckBox chk = new CheckBox();
                        chk = (CheckBox)dg.FindControl("chkCanAdd");
                        chk.Checked = true;
                    }
                    if (dr["CanModify"].ToString() == 'Y'.ToString())
                    {
                        CheckBox chk = new CheckBox();
                        chk = (CheckBox)dg.FindControl("chkCanModify");
                        chk.Checked = true;
                    }
                    if (dr["CanDelete"].ToString() == 'Y'.ToString())
                    {
                        CheckBox chk = new CheckBox();
                        chk = (CheckBox)dg.FindControl("chkCanDelete");
                        chk.Checked = true;
                    }
                    if (dr["CanPrint"].ToString() == 'Y'.ToString())
                    {
                        CheckBox chk = new CheckBox();
                        chk = (CheckBox)dg.FindControl("chkCanPrint");
                        chk.Checked = true;
                    }
                    if (dr["Approval1"].ToString() == 'Y'.ToString())
                    {
                        CheckBox chk = new CheckBox();
                        chk = (CheckBox)dg.FindControl("chkFrstApp");
                        chk.Checked = true;
                    }
                    if (dr["Approval2"].ToString() == 'Y'.ToString())
                    {
                        CheckBox chk = new CheckBox();
                        chk = (CheckBox)dg.FindControl("chkScndApp");
                        chk.Checked = true;
                    }
                }
                //if (ViewState["SuperUser"].ToString() == "N")
                //{
                //    HiddenField hfdroleId;
                //    hfdroleId = (HiddenField)dg.FindControl("HiddenRoleId");
                //    roleid = Convert.ToInt32(hfdroleId.Value.ToString());
                //    if (Convert.ToInt32(ViewState["RoleId"].ToString()) != roleid)
                //    {
                //        CheckBox chk = new CheckBox();
                //        chk = (CheckBox)dg.FindControl("chkCanAdd");
                //        chk.Enabled = false;
                //        CheckBox chk1 = new CheckBox();
                //        chk1 = (CheckBox)dg.FindControl("chkCanModify");
                //        chk1.Enabled = false;
                //        CheckBox chk2 = new CheckBox();
                //        chk2 = (CheckBox)dg.FindControl("chkCanDelete");
                //        chk2.Enabled = false;
                //        CheckBox chk3 = new CheckBox();
                //        chk3 = (CheckBox)dg.FindControl("chkCanPrint");
                //        chk3.Enabled = false;
                //        CheckBox chk4 = new CheckBox();
                //        chk4 = (CheckBox)dg.FindControl("chkCanVerify");
                //        chk4.Enabled = false;
                //    }
                //}
            }
        }
    }
    protected void btn_Authority_save_Click(object sender, EventArgs e)
    {
        HiddenField hfdapplicationmoduleId;
        hfdapplicationmoduleId = (HiddenField)GvmoduleList.Rows[GvmoduleList.SelectedIndex].FindControl("HiddenModuleId");
        int ApplicationModuleid = Convert.ToInt32(hfdapplicationmoduleId.Value.ToString());
        char add, modify, delete, print, frstapp, scndapp;
        foreach (GridViewRow dg in GVAuthority.Rows)
        {
            CheckBox chk = new CheckBox();
            chk = (CheckBox)dg.FindControl("chkCanAdd");
            if (chk.Checked == true)
            {
                add = 'Y';
            }
            else
            {
                add = 'N';
            }
            CheckBox chk1 = new CheckBox();
            chk1 = (CheckBox)dg.FindControl("chkCanModify");
            if (chk1.Checked == true)
            {
                modify = 'Y';
            }
            else
            {
                modify = 'N';
            }
            CheckBox chk2 = new CheckBox();
            chk2 = (CheckBox)dg.FindControl("chkCanDelete");
            if (chk2.Checked == true)
            {
                delete = 'Y';
            }
            else
            {
                delete = 'N';
            }
            CheckBox chk3 = new CheckBox();
            chk3 = (CheckBox)dg.FindControl("chkCanPrint");
            if (chk3.Checked == true)
            {
                print = 'Y';
            }
            else
            {
                print = 'N';
            }
            CheckBox chk4 = new CheckBox();
            chk4 = (CheckBox)dg.FindControl("chkFrstApp");
            if (chk4.Checked == true)
            {
                frstapp = 'Y';
            }
            else
            {
                frstapp = 'N';
            }
            CheckBox chk5 = new CheckBox();
            chk5 = (CheckBox)dg.FindControl("chkScndApp");
            if (chk5.Checked == true)
            {
                scndapp = 'Y';
            }
            else
            {
                scndapp = 'N';
            }
            HiddenField hfd;
            hfd = (HiddenField)dg.FindControl("HiddenLogin1Id");
            int userid = Convert.ToInt32(hfd.Value.ToString());
            UserRights_VIMS.InsertUpdateAuthorityDetails_ForVIMS("InsertUpdateApplicationModuleUserRelation_VIMS",
                                                       ApplicationModuleid,
                                                       userid,
                                                       add,
                                                       modify,
                                                       delete,
                                                       print,
                                                       frstapp,
                                                       scndapp);

        }
        lblupdation.Visible = true;
        lblupdation.Text = "Record Updated.";
    }
    protected void btn_Authority_Reset_Click(object sender, EventArgs e)
    {
        lblupdation.Visible = false;
        foreach (GridViewRow dg in GVAuthority.Rows)
        {
            CheckBox chk = new CheckBox();
            chk = (CheckBox)dg.FindControl("chkCanAdd");
            chk.Checked = false;
            CheckBox chk1 = new CheckBox();
            chk1 = (CheckBox)dg.FindControl("chkCanModify");
            chk1.Checked = false;
            CheckBox chk2 = new CheckBox();
            chk2 = (CheckBox)dg.FindControl("chkCanDelete");
            chk2.Checked = false;
            CheckBox chk3 = new CheckBox();
            chk3 = (CheckBox)dg.FindControl("chkCanPrint");
            chk3.Checked = false;
            CheckBox chk4 = new CheckBox();
            chk4 = (CheckBox)dg.FindControl("chkFrstApp");
            chk4.Checked = false;
            CheckBox chk5 = new CheckBox();
            chk5 = (CheckBox)dg.FindControl("chkScndApp");
            chk5.Checked = false;
        }
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("AdminDashBoard.aspx");
    }
}
