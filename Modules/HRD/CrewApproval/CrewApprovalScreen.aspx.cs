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
using System.Text;
using System.Xml;

public partial class CrewApproval_CrewApprovalScreen : System.Web.UI.Page
{
    Authority Auth;
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        //Alerts.SetHelp(imgHelp);  
        ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 1);
        OBJ.Invoke();
        Session["Authority"] = OBJ.Authority;
        Auth = OBJ.Authority;
        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 1037);
        if (chpageauth <= 0)
        {
            Response.Redirect("Dummy.aspx");
        }
        //*******************
        lbl_Msg.Text = ""; 
        lbl_GridView_Search.Text = "";
        if (!Page.IsPostBack)
        {
            Load_Vessel();
            Load_Owner();
            Load_Rank();
            //Load_RankGroup();
            
            ddl_ApprovalStatus.SelectedValue = "N";
            //btn_Approve.Visible = Auth.isVerify;
            //btn_Reject.Visible = Auth.isVerify;
            //btn_VesselMatrix.Visible = Auth.isPrint;
            //-------------------------
            if (Session["owner"] != null)
            {
                ddl_Owner.SelectedValue = Session["owner"].ToString();
                ddl_Owner_SelectedIndexChanged(sender, e);
                if (Session["vessel"] != null)
                {
                    ddl_Vessel.SelectedValue = Convert.ToString(Session["vessel"]);
                }
                if (Session["appstat"] != null)
                {
                    ddl_ApprovalStatus.SelectedValue = Convert.ToString(Session["appstat"]);
                }
                if (Session["rankg"] != null)
                {
                    ddl_RankGroup.SelectedValue = Convert.ToString(Session["rankg"]);
                    ddl_RankGroup_SelectedIndexChanged(sender, e);
                }
                if (Session["rank"] != null)
                {
                    ddl_Rank.SelectedValue = Convert.ToString(Session["rank"]);
                }

                if (Session["stat"] != null)
                {
                    ddl_CrewStatus.SelectedValue = Convert.ToString(Session["stat"]);
                }


            }
            //-------------------------
            btn_Search_Click(sender, e);
            if (Session["si"] != null)
            {
                gv_CrewApproval.SelectedIndex = Convert.ToInt32(Session["si"].ToString());
            }
            //-------------------------
            

            Load_Counters();
        }
    }
    # region Page Loader
    private void Load_Vessel()
    {
        DataSet dtvsl = cls_SearchReliever.getMasterData("Vessel", "VesselId", "VesselName", Convert.ToInt32(Session["loginid"].ToString()));
        ddl_Vessel.DataTextField = "VesselName";
        ddl_Vessel.DataValueField = "VesselId";
        ddl_Vessel.DataSource = dtvsl;
        ddl_Vessel.DataBind();
        ddl_Vessel.Items.Insert(0, new ListItem("All", "0"));
    }
    private void Load_Owner()
    {
        DataSet dtownr = cls_SearchReliever.getMasterData("Owner", "OwnerId", "OwnerName");
        ddl_Owner.DataTextField = "OwnerName";
        ddl_Owner.DataValueField = "OwnerId";
        ddl_Owner.DataSource = dtownr;
        ddl_Owner.DataBind();
        ddl_Owner.Items.Insert(0, new ListItem("All", "0"));
    }
    private void Load_Rank()
    {
        DataSet dtrk = cls_SearchReliever.getMasterData("Rank", "RankId", "RankCode");
        ddl_Rank.DataTextField = "RankCode";
        ddl_Rank.DataValueField = "RankId";
        ddl_Rank.DataSource = dtrk;
        ddl_Rank.DataBind();
        ddl_Rank.Items.Insert(0, new ListItem("All", "0"));
    }
    private void Load_RankGroup()
    {
        DataSet dtrkgrp = cls_SearchReliever.getMasterData("RankGroup", "RankGroupId", "RankGroupName");
        ddl_RankGroup.DataTextField = "RankGroupName";
        ddl_RankGroup.DataValueField = "RankGroupId";
        ddl_RankGroup.DataSource = dtrkgrp;
        ddl_RankGroup.DataBind();
        ddl_RankGroup.Items.Insert(0, new ListItem("All", "0"));
    }
    private void Load_Counters()
    {
        DataTable dtgrid = CrewApproval.Bind_CrewApprovalGrid(0, 0, 'A', 0, 'A', 0, "", Convert.ToInt32(Session["loginid"].ToString()));
        btnApprovedCnt.Text = dtgrid.Rows.Count.ToString();

        DataTable dtR= CrewApproval.Bind_CrewApprovalGrid(0, 0, 'R', 0, 'A', 0, "", Convert.ToInt32(Session["loginid"].ToString()));
        btnRejectedCnt.Text = dtR.Rows.Count.ToString();

        DataTable dtS = CrewApproval.Bind_CrewApprovalGrid(0, 0, 'S', 0, 'A', 0, "", Convert.ToInt32(Session["loginid"].ToString()));
        btnSubmittedForApprovalCnt.Text = dtS.Rows.Count.ToString();

        DataTable dtN = CrewApproval.Bind_CrewApprovalGrid(0, 0, 'N', 0, 'A', 0, "", Convert.ToInt32(Session["loginid"].ToString()));
        btnPendingSubmissionCnt.Text = dtN.Rows.Count.ToString();


    }
    # endregion
    protected void ddl_Owner_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_Owner.SelectedIndex > 0)
        {
            DataTable dt9 = VarianceReport.getVesselAccordingtoOwner(Convert.ToInt32(ddl_Owner.SelectedValue));
            ddl_Vessel.DataTextField = "VesselName";
            ddl_Vessel.DataValueField = "VesselId";
            ddl_Vessel.DataSource = dt9;
            ddl_Vessel.DataBind();
            ddl_Vessel.Items.Insert(0, new ListItem("All", "0"));
        }
        else
        {
            ddl_Vessel.Items.Clear();
            Load_Vessel();
        }
    }
    protected void ddl_RankGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_RankGroup.SelectedIndex > 0)
        {
            DataTable dt5 = CrewApproval.getRankAccordingtoRankGroup(Convert.ToChar(ddl_RankGroup.SelectedValue));
            ddl_Rank.DataTextField = "RankCode";
            ddl_Rank.DataValueField = "RankId";
            ddl_Rank.DataSource = dt5;
            ddl_Rank.DataBind();
            ddl_Rank.Items.Insert(0, new ListItem("All", "0"));
        }
        else
        {
            ddl_Rank.Items.Clear();
            Load_Rank();
        }
    }
    protected void btn_Search_Click(object sender, EventArgs e)
    {
        Bind_Grid(gv_CrewApproval.Attributes["MySort"],Convert.ToInt32(ddl_Vessel.SelectedValue),Convert.ToInt32(ddl_Owner.SelectedValue),Convert.ToChar(ddl_ApprovalStatus.SelectedValue),Convert.ToInt32(ddl_Rank.SelectedValue),Convert.ToChar(ddl_RankGroup.SelectedValue),Convert.ToInt32(ddl_CrewStatus.SelectedValue), txtCrewNo.Text.Trim(), Convert.ToInt32(Session["loginid"].ToString()));
        if (gv_CrewApproval.Rows.Count > 0)
        {
            btn_ExportToExcel.Visible = true;
            //lbl_Total.Text = gv_CrewApproval.Rows.Count.ToString();
        }
        else
        {
            btn_ExportToExcel.Visible = false;
            //lbl_Total.Text = "0";
        }
        gv_CrewApproval.SelectedIndex = -1;
    }
    protected void btn_Clear_Click(object sender, EventArgs e)
    {
        ddl_Vessel.Items.Clear();
        Load_Vessel();
        ddl_Rank.Items.Clear();
        Load_Rank();
        ddl_Vessel.SelectedIndex = 0;
        ddl_Owner.SelectedIndex = 0;
        ddl_ApprovalStatus.SelectedValue = "N";
        ddl_Rank.SelectedIndex = 0;
        ddl_RankGroup.SelectedIndex = 0;
        ddl_CrewStatus.SelectedIndex = 0;
        gv_CrewApproval.SelectedIndex = -1;
    }
    private void Bind_Grid(String sort, int VesselId, int OwnerId, char ApprovalStatus, int Rank, char RankGroup, int CrewStatus,string CrewNo, int LoginId)
    {
        DataTable dtgrid = CrewApproval.Bind_CrewApprovalGrid(VesselId, OwnerId, ApprovalStatus, Rank, RankGroup, CrewStatus, CrewNo, LoginId);
        dtgrid.DefaultView.Sort = sort;
        gv_CrewApproval.DataSource = dtgrid;
        gv_CrewApproval.DataBind();
        gv_CrewApproval.Attributes.Add("MySort", sort);
    }
    protected void On_Sorted(object sender, EventArgs e)
    {

    }
    protected void On_Sorting(object sender, GridViewSortEventArgs e)
    {
        Bind_Grid(e.SortExpression, Convert.ToInt32(ddl_Vessel.SelectedValue), Convert.ToInt32(ddl_Owner.SelectedValue), Convert.ToChar(ddl_ApprovalStatus.SelectedValue), Convert.ToInt32(ddl_Rank.SelectedValue), Convert.ToChar(ddl_RankGroup.SelectedValue), Convert.ToInt32(ddl_CrewStatus.SelectedValue),txtCrewNo.Text.Trim(), Convert.ToInt32(Session["loginid"].ToString()));
    }
    protected void gv_CrewApproval_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //ImageButton imgremark = ((ImageButton)e.Row.FindControl("img_plannerremarks"));
            //if (DataBinder.Eval(e.Row.DataItem, "Remark").ToString() != "")
            //    imgremark.Visible = true;
            //else
            //    imgremark.Visible = false;

            //ImageButton imgappremark = ((ImageButton)e.Row.FindControl("img_approverremarks"));
            //if(DataBinder.Eval(e.Row.DataItem, "ApproverRemark").ToString() != "")
            //    imgappremark.Visible = true;
            //else
            //    imgappremark.Visible = false;
        }
    }
    protected void gv_CrewApproval_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "CrewDetails")
            {
                GridViewRow row = (GridViewRow)((Control)e.CommandSource).Parent.Parent;
                HiddenField hfd11 = (HiddenField)gv_CrewApproval.Rows[row.RowIndex].FindControl("hfd_CrewId");
                Session["Mode"] = "View";
                Session["CrewId"] = hfd11.Value;
                Session["AP"] = 1;
                //----------------
                Session["owner"] = ddl_Owner.SelectedValue;
                Session["vessel"] = ddl_Vessel.SelectedValue;
                Session["appstat"] = ddl_ApprovalStatus.SelectedValue;
                Session["rankg"] = ddl_RankGroup.SelectedValue;
                Session["rank"] = ddl_Rank.SelectedValue;
                Session["stat"] = ddl_CrewStatus.SelectedValue;
                Session["si"] = gv_CrewApproval.SelectedIndex;
                //----------------
                ScriptManager.RegisterStartupScript(this, this.GetType(), "b", "window.parent.location='../SiteFrame.aspx?showcrewdetails';", true);  
                //Response.Redirect("~/SiteFrame.aspx?showcrewdetails=1");
            }
        }
        catch { }
    }
    protected void btn_Submit_Click(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToInt32(Session["temp"].ToString()) == 0)
            {
                CrewApproval.UpdateCrewVesselPlanningHistory("Update_CVPH_CrewApproval", Convert.ToInt32(Session["Plng_Id"].ToString()), 'A', txt_Remarks.Text.Trim(), Convert.ToInt32(Session["loginid"].ToString()));
                btn_Search_Click(sender, e);
                lbl_Msg.Text = "Record Saved Successfully.";
            }
            else if (Convert.ToInt32(Session["temp"].ToString()) == 1)
            {
                CrewApproval.UpdateCrewVesselPlanningHistory("Update_CVPH_CrewApproval", Convert.ToInt32(Session["Plng_Id"].ToString()), 'R', txt_Remarks.Text.Trim(), Convert.ToInt32(Session["loginid"].ToString()));
                btn_Search_Click(sender, e);
                lbl_Msg.Text = "Record Saved Successfully.";
            }
        }
        catch { lbl_Msg.Text = "Record Not Saved."; }
    }
    protected void btn_Approve_Click(object sender, EventArgs e)
    {
        if (gv_CrewApproval.SelectedIndex < 0)
        {
            lbl_Msg.Text = "Please Select Atleast One Row.";
            return;
        }
        else
        {
            HiddenField hfd;
            int Pln_Id;
            hfd = (HiddenField)gv_CrewApproval.SelectedRow.FindControl("hfd_PlanningId");
            Pln_Id = Convert.ToInt32(hfd.Value);
            Session["Plng_Id"] = Pln_Id;
            Session["temp"] = 0;
            lblRemarksHeading.Text = " Approval Remarks ";
            mdl_popup.Show();
        }
    }    
    protected void btn_Reject_Click(object sender, EventArgs e)
    {
        if (gv_CrewApproval.SelectedIndex < 0)
        {
            lbl_Msg.Text = "Please Select Atleast One Row.";
            return;
        }
        else
        {
            HiddenField hfd;
            int Pln_Id;
            hfd = (HiddenField)gv_CrewApproval.SelectedRow.FindControl("hfd_PlanningId");
            Pln_Id = Convert.ToInt32(hfd.Value);
            Session["Plng_Id"] = Pln_Id;
            Session["temp"] = 1;
            lblRemarksHeading.Text = " Rejection Remarks ";
            mdl_popup.Show();
        }
    }
    protected void btn_VesselMatrix_Click(object sender, EventArgs e)
    {
        //Response.Redirect("~/Reporting/VesselCrewMatrix.aspx");
    }
    protected void btn_ExportToExcel_Click(object sender, EventArgs e)
    {
        //PrepareGridViewForExport(gv_CrewApproval);
        ExportGridView();
    }
    private void ExportGridView()
    {
        string attachment = "attachment; filename=CrewApprovalDetails.xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        try
        {
            btn_Search_Click(new object(), new EventArgs());
            gv_CrewApproval.Columns[4].Visible = false;
            gv_CrewApproval.Columns[5].Visible = false;
            gv_CrewApproval.Columns[8].Visible = false;
            gv_CrewApproval.Columns[9].Visible = true;
            gv_CrewApproval.Columns[12].Visible = false;
            gv_CrewApproval.Columns[13].Visible = true;
            gv_CrewApproval.RenderControl(htw);
        }
        catch { }
        Response.Write(sw.ToString());
        Response.End();
    }
    //private void PrepareGridViewForExport(Control gv)
    //{
    //    LinkButton lb = new LinkButton();
    //    Literal l = new Literal();
    //    string name = String.Empty;
    //    for (int i = 0; i < gv.Controls.Count; i++)
    //    {
    //        if (gv.Controls[i].GetType() == typeof(LinkButton))
    //        {
    //            l.Text = (gv.Controls[i] as LinkButton).Text;
    //            gv.Controls.Remove(gv.Controls[i]);
    //            gv.Controls.AddAt(i, l);
    //        }
    //        else if (gv.Controls[i].GetType() == typeof(DropDownList))
    //        {
    //            l.Text = (gv.Controls[i] as DropDownList).SelectedItem.Text;
    //            gv.Controls.Remove(gv.Controls[i]);
    //            gv.Controls.AddAt(i, l);
    //        }
    //        else if (gv.Controls[i].GetType() == typeof(CheckBox))
    //        {
    //            l.Text = (gv.Controls[i] as CheckBox).Checked ? "True" : "False";
    //            gv.Controls.Remove(gv.Controls[i]);
    //            gv.Controls.AddAt(i, l);
    //        }
    //        if (gv.Controls[i].HasControls())
    //        {
    //            PrepareGridViewForExport(gv.Controls[i]);
    //        }
    //    }
    //}
    public override void VerifyRenderingInServerForm(Control control)
    {
        
    }
    protected void lnk_Confirm_Click(object sender, EventArgs e)
    {

    }
    protected void gv_CrewApproval_PreRender(object sender, EventArgs e)
    {
        if (gv_CrewApproval.Rows.Count <= 0)
            lbl_GridView_Search.Text = "No Record Found.";
    }
    protected void gv_CrewApproval_SelectedIndexChanged(object sender, EventArgs e)
    {
        //HiddenField hfd=(HiddenField)gv_CrewApproval.Rows[gv_CrewApproval.SelectedIndex].FindControl("hfd_AppStatus");
        //if( hfd!=null)
        //{
        //    Session["Ind"] = gv_CrewApproval.SelectedIndex;
        //    if (hfd.Value.Trim() == "A" || hfd.Value.Trim() == "R")
        //    {
        //        btn_Approve.Enabled = false;
        //        btn_Reject.Enabled = false;
        //    }
        //    else
        //    {
        //        btn_Approve.Enabled = true ;
        //        btn_Reject.Enabled = true;
        //    }
        //}
    }
    protected void btnCL_Click(object sender, EventArgs e)
    {
        string PlanningId = ((ImageButton)sender).CommandArgument;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "c", "window.open('../CrewOperation/ViewCrewCheckList.aspx?_P=" + PlanningId + "');", true);
    }
    protected void Legent_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        btn_Clear_Click(sender, e);
        ddl_ApprovalStatus.SelectedValue = btn.CommandArgument;
        btn_Search_Click(sender, e);
    }
    
}
