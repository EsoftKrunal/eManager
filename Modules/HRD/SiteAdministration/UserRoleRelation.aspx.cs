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

public partial class SiteAdministration_UserRoleRelation : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string id = Session["loginid"].ToString();
        lblupdation.Text = "";
        if (!Page.IsPostBack)
        {
            get_roles();
            Bind_Module();
            Session["PageRoleRights"] = "1";
            //ddmodule.Items.Insert(0, new ListItem("<Select>", "-1"));
        }
        else
        {
            Session.Remove("PageRoleRights");
        }
    }
    private void get_roles()
    {
        this.ddrole.DataTextField = "RoleName";
        this.ddrole.DataValueField = "RoleId";
        DataTable dt = UserRights.selectRoleDetails();
        this.ddrole.DataSource = dt;
        this.ddrole.DataBind();
    }
    private void Bind_Module()
    {
        DataTable dt = UserPageRelation.selectModuleName();
        this.ddmodule.DataTextField = "ApplicationModule";
        this.ddmodule.DataValueField = "ApplicationModuleId";
        this.ddmodule.DataSource = dt;
        this.ddmodule.DataBind();
        ddmodule.Items.Insert(0, new ListItem("<Select>", "-1"));
    }
    private void BindGrid()
    {
        DataTable dt1 = UserRoleRelation.selectUserPage(Convert.ToInt32(this.ddmodule.SelectedValue));
        this.GvRoles.DataSource = dt1;
        this.GvRoles.DataBind();
    }
    protected void ddrole_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.ddrole.SelectedIndex == 0)
        {
            this.ddmodule.Items.Clear();
            ddmodule.Items.Insert(0, new ListItem("<Select>", "-1"));
            BindGrid();
        }
        else
        {
            Bind_Module();
            BindGrid();
        }
        Bind_Module();

    }
    protected void ddmodule_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGrid();
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        int roleid = Convert.ToInt32(ddrole.SelectedValue);
            UserRoleRelation.deleteUserRoleRelationDetails("deleteUserRoleRelation", roleid, Convert.ToInt32(ddmodule.SelectedValue));
            CheckBox chk = new CheckBox();
            int i = 0;
            foreach (GridViewRow dg in GvRoles.Rows)
            {
                chk = (CheckBox)dg.FindControl("chkSelect");

                if (chk.Checked == true)
                {
                    HiddenField hfd;
                    int pageid = Convert.ToInt32(GvRoles.DataKeys[dg.RowIndex].Value.ToString());
                    UserRoleRelation.insertUpdateUserRoleRelationDetails("InsertUpdateUserRoleRelation",
                                                                             roleid,
                                                                             pageid);
                }
                i = i + 1;

            } 
        ddrole_SelectedIndexChanged(sender, e);
        lblupdation.Text = "Record Updated..";
    }
    protected void btn_Reset_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow dg in GvRoles.Rows)
        {
            CheckBox chk = new CheckBox();
            chk = (CheckBox)dg.FindControl("chkSelect");
            chk.Checked = false;

        }
    }
    public void do_check(object sender, EventArgs e)
    {
        CheckBox chk1 = ((CheckBox)sender);
        if (chk1.Checked == true)
        {
            for (int k = 0; k < GvRoles.Rows.Count; k++)
            {
                CheckBox chk = new CheckBox();
                chk = (CheckBox)GvRoles.Rows[k].FindControl("chkSelect");
                chk.Checked = true;
            }
        }
        else
        {
            for (int k = 0; k < GvRoles.Rows.Count; k++)
            {
                CheckBox chk = new CheckBox();
                chk = (CheckBox)GvRoles.Rows[k].FindControl("chkSelect");
                chk.Checked = false;
            }
        }
    }
    protected void GvRoles_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int i;
            int _LoginId;
            int _pageid;
            _pageid = Convert.ToInt32(GvRoles.DataKeys[e.Row.RowIndex].Value);
            DataTable dt2 = UserRoleRelation.selectData_UserPage(Convert.ToInt32(ddrole.SelectedValue), _pageid);
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
        }
    }
}
