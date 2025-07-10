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

public partial class CrewOperation_CreateBatches : System.Web.UI.Page
{
    int temp;
    Authority Auth;
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        lbl_CreateBatch_Message.Text = ""; 
        //***********Code to check page acessing Permission
        temp = 0;
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 30);
        if (chpageauth <= 0)
        {
            Response.Redirect("Dummy2.aspx");

        }
        //*******************
        ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 2);
        OBJ.Invoke();
        Session["Authority"] = OBJ.Authority;
        Auth = OBJ.Authority;
        if (!Page.IsPostBack)
        {
            bindPlanTrainingGrid();
        }
    }
    public void bindPlanTrainingGrid()
    {
        DataTable dt = CreateBatches.selectPlanTrainingDetails();
        this.GridView_PlanTraining.DataSource = dt;
        this.GridView_PlanTraining.DataBind();
    }
    protected void GridView_PlanTraining_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index, index1, batches;
        HiddenField hfd, hfd1, hfd2;
        GridViewRow row = (GridViewRow)((Control)e.CommandSource).Parent.Parent;
        hfd = (HiddenField)GridView_PlanTraining.Rows[row.RowIndex].FindControl("HiddenTrainingId");
        hfd1 = (HiddenField)GridView_PlanTraining.Rows[row.RowIndex].FindControl("Hiddenbatches");
        hfd2 = (HiddenField)GridView_PlanTraining.Rows[row.RowIndex].FindControl("HiddenTrainingPlanningId");

        //*********** CODE TO CHECK FOR BRANCHID FOR PLANNING ***********
        string pt = Alerts.Check_BranchIdforPlanning(Convert.ToInt32(hfd2.Value));
        if (pt.Trim() != "")
        {
            GridView_PlanTraining.SelectedIndex = -1;
            temp = 1;
            lbl_CreateBatch_Message.Text = pt;
            this.btn_save.Enabled = false;
            //return;
        }
        else
        {
            this.btn_save.Enabled = true && Auth.isAdd;
        }
        //************

        GridView_PlanTraining.SelectedIndex = row.RowIndex;
        index = Convert.ToInt32(hfd.Value.ToString());
        index1 = Convert.ToInt32(hfd2.Value.ToString());
        ViewState.Add("Trainingid", index.ToString());
        ViewState.Add("Trainingplanningid", index1.ToString());
        batches = Convert.ToInt32(hfd1.Value.ToString());
        bindbatchGrid(index, index1);

        DropDownList d1 = new DropDownList();
        foreach (GridViewRow gr in gvbatch.Rows)
        {
            d1 = (DropDownList)gr.FindControl("dd_batch");
            for (int i = 1; i <= batches; i++)
            {
                d1.Items.Add(new ListItem(("Batch " + i),(i.ToString())));
            }
        }
        int login1id;
        DataTable dt1 = CreateBatches.selectBatchesGridDetails(index,index1);
        foreach (DataRow dr in dt1.Rows)
        {
            foreach (GridViewRow dg in gvbatch.Rows)
            {
                HiddenField hfdLogin1Id;
                hfdLogin1Id = (HiddenField)dg.FindControl("HiddenTrainingRequirementId");
                login1id = Convert.ToInt32(hfdLogin1Id.Value.ToString());

                if (Convert.ToInt32(dr["TrainingRequirementId"].ToString()) == login1id)
                {
                    if (dr["Confirmed"].ToString() == 'Y'.ToString())
                    {
                        CheckBox chk = new CheckBox();
                        chk = (CheckBox)dg.FindControl("chk_confirm");
                        chk.Checked = true;
                    }
                    if (Convert.ToInt32(dr["BatchNumber"].ToString()) != 0)
                    {
                        DropDownList dd = new DropDownList();
                        dd = (DropDownList)dg.FindControl("dd_batch");
                        int a = Convert.ToInt32(dr["BatchNumber"].ToString());
                        dd.SelectedIndex = a;
                    }
                }
            }
        }
        panel_batch.Visible = true;
    }
    public void bindbatchGrid(int id, int id1)
    {
        
        DataTable dt1 = CreateBatches.selectBatchesGridDetails(id,id1);
        this.gvbatch.DataSource = dt1;
        this.gvbatch.DataBind();
        
    }
    protected void btn_save_Click(object sender, EventArgs e)
    {
        char confirm;
        int batchno;

        foreach (GridViewRow dg in gvbatch.Rows)
        {
            DropDownList dd = new DropDownList();
            dd = (DropDownList)dg.FindControl("dd_batch");
            batchno = Convert.ToInt32(dd.SelectedValue);

            CheckBox chk = new CheckBox();
            chk = (CheckBox)dg.FindControl("chk_confirm");
            if (chk.Checked == true)
            {
                confirm = 'Y';
            }
            else
            {
                confirm = 'N';
            }
            HiddenField hfd;
            hfd = (HiddenField)dg.FindControl("Hiddencrewid");
            int crewid = Convert.ToInt32(hfd.Value.ToString());
            int trainingid = Convert.ToInt32(ViewState["Trainingid"].ToString());
            int Trainingplanningid = Convert.ToInt32(ViewState["Trainingplanningid"].ToString());
            CreateBatches.UpdatebatchesDetails("UpdateBatches",
                                                crewid,
                                                trainingid,
                                                Trainingplanningid,
                                                batchno,
                                                confirm);
        }
    }
    protected void gvbatch_PreRender(object sender, EventArgs e)
    {
        if (this.gvbatch.Rows.Count <= 0)
        {
            Label1.Text = "No Records Found..!";
        }
        else
        {
            Label1.Text = "";
        }
        if (temp == 1)
        {
            gvbatch.SelectedIndex = -1;
        }
    }
}
