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

public partial class SiteAdministration_UserPageRelation : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string id = Session["loginid"].ToString();
        if (!Page.IsPostBack)
        {
            getroles();
            BindUserNamesDropDown();
            ddmodule.Items.Insert(0, new ListItem("<Select>", "-1"));
            Session["PageRoleRights"] = "2";
        }
        else
        {
            Session.Remove("PageRoleRights");
        }
    }
    private void getroles()
    {
        this.ddrole.DataTextField = "RoleName";
        this.ddrole.DataValueField = "RoleId";
        DataTable dt = UserRights.selectRoleDetails();
        this.ddrole.DataSource = dt;
        this.ddrole.DataBind();
       
    }
    private void BindUserNamesDropDown()
    {
        DataTable dt = UserPageRelation.selectDataLoginId(Convert.ToInt32(this.ddrole.SelectedValue.ToString()));
        this.dd_usernames.DataValueField = "LoginId";
        this.dd_usernames.DataTextField = "UserName";
        this.dd_usernames.DataSource = dt;
        this.dd_usernames.DataBind();
        //dd_usernames.Items.Insert(1,new ListItem ("Select","0"));
    }
    private void BindModule()
    {
        DataTable dt = UserPageRelation.selectModuleName();
      
        this.ddmodule.DataTextField = "ApplicationModule";
        this.ddmodule.DataValueField = "ApplicationModuleId";
        this.ddmodule.DataSource = dt;
        this.ddmodule.DataBind();

        ddmodule.Items.Insert(0,new ListItem("<Select>","-1"));
        //ddmodule.Items.Insert(1, new ListItem("All", "0"));
        
        

        
    }
    private void BindGriduserpage()
    {
        DataTable dt1 = UserPageRelation.selectUserPage(Convert.ToInt32(this.ddmodule.SelectedValue),Convert.ToInt32(this.ddrole.SelectedValue),Convert.ToInt32(this.dd_usernames.SelectedValue));
        this.Gvuserpages.DataSource = dt1;
        this.Gvuserpages.DataBind();
    }
    protected void btn_Authority_save_Click(object sender, EventArgs e)
    {
        if (dd_usernames.SelectedValue != "0")
        {
            int userid = Convert.ToInt32(dd_usernames.SelectedValue);
            UserPageRelation.deleteUserPageRelationDetails("deleteUserPageRelation", userid, Convert.ToInt32(ddmodule.SelectedValue));
            CheckBox chk = new CheckBox();
            int i = 0;
            foreach (GridViewRow dg in Gvuserpages.Rows)
            {
                chk = (CheckBox)dg.FindControl("chkSelect");

                if (chk.Checked == true)
                {
                    HiddenField hfd;
                    hfd = (HiddenField)dg.FindControl("HiddenVesselId");
                    int moduleid = Convert.ToInt32(hfd.Value.ToString());
                    int pageid = Convert.ToInt16(this.Gvuserpages.DataKeys[i].Value.ToString());
                    UserPageRelation.insertUpdateUserVesselRelationDetails("InsertUpdateUserPageRelation",
                                                                             userid,
                                                                             pageid);
                }
                i = i + 1;

            }
        }
        else
        {
            for (int j = 2; j < this.dd_usernames.Items.Count; j++)
            {
                UserPageRelation.deleteUserPageRelationDetails("deleteUserPageRelation", Convert.ToInt32(dd_usernames.Items[j].Value) , Convert.ToInt32(ddmodule.SelectedValue));
            }
            CheckBox chk = new CheckBox();
            int i = 0;
            foreach (GridViewRow dg in Gvuserpages.Rows)
            {
                int userid;
                
                chk = (CheckBox)dg.FindControl("chkSelect");

                if (chk.Checked == true)
                {
                    HiddenField hfdlog = ((HiddenField)dg.FindControl("hfdlogin"));
                    userid = Convert.ToInt32(hfdlog.Value);
                    HiddenField hfd;
                    hfd = (HiddenField)dg.FindControl("HiddenVesselId");
                    int moduleid = Convert.ToInt32(hfd.Value.ToString());
                    int pageid = Convert.ToInt16(this.Gvuserpages.DataKeys[i].Value.ToString());
                    UserPageRelation.insertUpdateUserVesselRelationDetails("InsertUpdateUserPageRelation",
                                                                             userid,
                                                                             pageid);
                }
                i = i + 1;

            }


        }
        dd_usernames_SelectedIndexChanged(sender, e);
        lblupdation.Visible = true;
    }
    protected void btn_Authority_Reset_Click(object sender, EventArgs e)
    {
        lblupdation.Visible = false;
        dd_usernames.SelectedIndex = 0;
        foreach (GridViewRow dg in Gvuserpages.Rows)
        {
            CheckBox chk = new CheckBox();
            chk = (CheckBox)dg.FindControl("chkSelect");
            chk.Checked = false;
            
        }
    }
    protected void dd_usernames_SelectedIndexChanged(object sender, EventArgs e)
    {

        lblupdation.Visible = false;
        if (this.dd_usernames.SelectedIndex == 0)
        {
            this.ddmodule.Items.Clear();
            ddmodule.Items.Insert(0, new ListItem("<Select>", "-1"));
            BindGriduserpage();
        }
        else
        {
            BindModule();
            BindGriduserpage();
        }
       
        
       
    }

    protected void ddmodule_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGriduserpage();
    }
    protected void Gvuserpages_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int _LoginId;
            int _pageid;
            HiddenField hfdlog = ((HiddenField)e.Row.FindControl("hfdlogin"));
            _LoginId = Convert.ToInt32(hfdlog.Value);
            _pageid = Convert.ToInt32(Gvuserpages.DataKeys[e.Row.RowIndex].Value);
            // _LoginId = Convert.ToInt32(dd_usernames.SelectedValue);

            int i;

            DataTable dt2 = UserPageRelation.selectDataUserPage(_LoginId,_pageid);
            CheckBox chk = new CheckBox();
            chk = (CheckBox)e.Row.FindControl("chkSelect");
            if (dt2.Rows.Count > 0)
            {
                chk.Checked = true;
            }
            else
            {
                chk.Checked = false;
            }
            //if (dt2.Rows.Count != 0)
            //{
            //    foreach (DataRow dr in dt2.Rows)
            //    {
            //        i = 0;
            //        foreach (GridViewRow dg in Gvuserpages.Rows)
            //        {

            //            CheckBox chk = new CheckBox();
            //            chk = (CheckBox)dg.FindControl("chkSelect");
            //            if (dr["Pageid"].ToString() == Gvuserpages.DataKeys[i].Value.ToString())
            //            {

            //                chk.Checked = true;



            //            }
            //            else
            //            {

            //            }
            //            i = i + 1;
            //        }
            //    }
            //}
        }
    }
    
    protected void ddrole_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindUserNamesDropDown();
        dd_usernames_SelectedIndexChanged(sender, e);
    }
    public void docheck(object sender, EventArgs e)
    {
        CheckBox chk1 = ((CheckBox)sender);
        if (chk1.Checked == true)
        {
            for (int k = 0; k < Gvuserpages.Rows.Count; k++)
            {
                CheckBox chk = new CheckBox();
                chk = (CheckBox)Gvuserpages.Rows[k].FindControl("chkSelect");
                chk.Checked = true;
            }
        }
        else
        {
            for (int k = 0; k < Gvuserpages.Rows.Count; k++)
            {
                CheckBox chk = new CheckBox();
                chk = (CheckBox)Gvuserpages.Rows[k].FindControl("chkSelect");
                chk.Checked = false;
            }
        }
    }
}
