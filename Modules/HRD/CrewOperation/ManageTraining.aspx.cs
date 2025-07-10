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

public partial class ManageTraining : System.Web.UI.Page
{
    int temp;
    Authority Auth;
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        temp = 0;
        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 31);
        if (chpageauth <= 0)
        {
            Response.Redirect("Dummy2.aspx");

        }
        //*******************
        ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 2);
        OBJ.Invoke();
        Session["Authority"] = OBJ.Authority;
        Auth = OBJ.Authority;
        lb_msg.Text = "";
        if (!(IsPostBack))
        {
            bindgrid();
        }
    }
    public void bindgrid()
    {
        DataTable dt1;
        dt1 = trainingbatch.Training();
        gvbatch.DataSource = dt1;
        gvbatch.DataBind();
    }
    protected void gvsearch_RowCreated(Object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lnk_batch = (LinkButton)e.Row.FindControl("lnk_batch");  //e.Row.Cells[0].Controls[0];
            lnk_batch.CommandArgument = e.Row.RowIndex.ToString();
        }

    }
    protected void gvsearch_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        Label lb_id, Lb_btch,lb_plan;

        if (e.CommandName == "lnk")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            gvbatch.SelectedIndex = index;
            GridViewRow r = gvbatch.Rows[index];

            lb_id = (Label)r.FindControl("lb_id");
            Lb_btch = (Label)r.FindControl("Lb_btch");
            lb_plan = (Label)r.FindControl("Lbplan");

            DataTable dta;
            dta = trainingbatch.crew_training(Convert.ToInt32(lb_id.Text), Convert.ToInt32(Lb_btch.Text), Convert.ToInt32(lb_plan.Text));
            GdCrew.DataSource = dta;
            GdCrew.DataBind();
            //*********** CODE TO CHECK FOR BRANCHID FOR PLANNING ***********
            string pt = Alerts.Check_BranchIdforPlanning(Convert.ToInt32(lb_plan.Text));
            if (pt.Trim() != "")
            {
                gvbatch.SelectedIndex = -1;
                temp = 1;
                lb_msg.Text = pt;
                this.btnvisasave.Enabled = false;
                return;
            }
            else
            {
                this.btnvisasave.Enabled = true && Auth.isAdd;
            }
            //************


        }
    }
    protected void gdCrew_Rowbound(object sender, GridViewRowEventArgs e)
    {

        foreach (GridViewRow grid1 in GdCrew.Rows)
        {

            Label Lb_attended = new Label();

            Lb_attended = (Label)grid1.FindControl("Lb_attended");

            CheckBox chkk = new CheckBox();
            chkk = (CheckBox)grid1.FindControl("chkattended");

            if (Lb_attended.Text == "Y")
            {
                chkk.Checked = true;

            }
            else
            {
                chkk.Checked = false;
            }
        }

    }
    protected void btnvisasave_Click(object sender, EventArgs e)
    {
        string Attend;
        foreach (GridViewRow dg in GdCrew.Rows)
        {
            Label lb_TrainingRequirementID = (Label)dg.FindControl("lb_TrainingRequirementID");
            Label lb_crewid = (Label)dg.FindControl("Lbcrewid");
            TextBox txtperhead = (TextBox)dg.FindControl("txtperhead");
            CheckBox chk = new CheckBox();
            chk = (CheckBox)dg.FindControl("chkattended");
            TextBox txtfromdate = (TextBox)dg.FindControl("txt_from");
            TextBox txttodate = (TextBox)dg.FindControl("txt_to");
            Label lbplan, lb_id;
            lbplan = (Label)gvbatch.SelectedRow.FindControl("Lbplan");
            int planid =Convert.ToInt32(lbplan.Text);
            lb_id = (Label)gvbatch.SelectedRow.FindControl("lb_id");
            int trainingid = Convert.ToInt32(lb_id.Text);
            if (chk.Checked == true)
            {
                Attend = "Y";
            }
            else
            {
                Attend = "N";
            }

            try
            {
                trainingbatch.updateCrewTraining(planid,trainingid, Convert.ToInt32(lb_crewid.Text), Convert.ToInt32(lb_TrainingRequirementID.Text), Convert.ToDouble(txtperhead.Text), Convert.ToString(txtfromdate.Text), Convert.ToString(txttodate.Text), Attend);
            }
            catch { lb_msg.Text = "Record Can't Saved."; }
            lb_msg.Text = ("Record Successfully Saved.");
        }
    }

    protected void GdCrew_PreRender(object sender, EventArgs e)
    {
        if (temp == 1)
        {
            GdCrew.SelectedIndex = -1;
        }
    }
}
