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

public partial class CrewOperation_LeaveRule : System.Web.UI.Page
{
    GridViewRow dg;
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        if (!Page.IsPostBack)
        {
            Bind_grid_rank_leave();
        }
    }
    private void Bind_grid_rank_leave()
    {
        DataTable dt = LeaveRule.selectRankLeaveDetails();
        this.gvleave.DataSource = dt;
        this.gvleave.DataBind();
    }
    public void DoEdit(object sender, GridViewEditEventArgs e)
    {
        gvleave.EditIndex = e.NewEditIndex;
        Bind_grid_rank_leave();
    }
    public void DoUpdate(object sender, GridViewUpdateEventArgs e)
    {
        double leave;
        int rankid;
        TextBox t1 = new TextBox();
        dg = gvleave.Rows[e.RowIndex];
        t1=(TextBox)dg.Cells[6].FindControl("lbloffgroup1");
        leave =Convert.ToDouble(t1.Text);
        rankid =Convert.ToInt32(gvleave.DataKeys[e.RowIndex].Value.ToString());
        LeaveRule.UpdaterankLeaveDetails("Insert_LeaveRule",
                                          rankid,
                                          leave);
        gvleave.EditIndex = -1;
        Bind_grid_rank_leave();
    }
    public void DoCancel(object sender, GridViewCancelEditEventArgs e)
    {
        gvleave.EditIndex = -1;
        Bind_grid_rank_leave();
    }
   
}
