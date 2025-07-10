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

public partial class Registers_Inspection : System.Web.UI.Page
{
    public Boolean GridStatus = true;
    int id;
    Authority Auth;
    string Mode = "New";
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        //--------------------------------- PAGE ACCESS AUTHORITY ----------------------------------------------------
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 153);
        if (chpageauth <= 0)
            Response.Redirect("blank.aspx");
        //------------------------------------------------------------------------------------------------------------
   
        if (Session["loginid"] == null)
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Default.aspx';", true);
        }
        lbl_GridView_Inspection.Text = "";
        lbl_Inspection_Message.Text = "";
        if (Session["loginid"] != null)
        {
            ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 12);
            OBJ.Invoke();
            Session["Authority"] = OBJ.Authority;
            Auth = OBJ.Authority;
        }
        if (!Page.IsPostBack)
        {
            bindInspectionGroupDDL();
            try
            {
                bindInspectionGrid(Convert.ToInt32(ddlInspectionGrp.SelectedValue));
            }
            catch { }
            try
            {
                Alerts.HANDLE_AUTHORITY(1, btn_New_Inspection, btn_Save_Inspection, btn_Cancel_Inspection, btn_Print_Inspection, Auth);
            }
            catch
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Default.aspx';", true);
            }
        }
    }
    public void bindInspectionGroupDDL()
    {
        DataSet ds1 = Inspection_Master.getMasterData("m_InspectionGroup", "Id", "(Code+ ' - ' +Name) as Name");
        if (ds1.Tables[0].Rows.Count > 0)
        {
            this.ddlInspectionGrp.DataSource = ds1.Tables[0];
            this.ddlInspectionGrp.DataValueField = "Id";
            this.ddlInspectionGrp.DataTextField = "Name";
            this.ddlInspectionGrp.DataBind();
        }
        else
        {
            this.ddlInspectionGrp.Items.Insert(0, new ListItem("<Select>", "0"));
        }
    }
    public void bindInspectionGrid(int Filter)
    {
        DataTable dt1 = Inspection_Master.InspectionDetails(0, "", "", Filter, 0, 0, 0, "", 0, 0, "Inpgrptype", 0,"");
        if (dt1.Rows.Count > 0)
        {
            GridStatus = true;
            this.GridView_Insp.DataSource = dt1;
            this.GridView_Insp.DataBind();
            HiddenFieldGridRowCount.Value = dt1.Rows.Count.ToString();
        }
        else
        {
            GridStatus = false;
            bindBlankGrid();
            HiddenFieldGridRowCount.Value = "0";
        }        
    }
    protected void btn_New_Inspection_Click(object sender, EventArgs e)
    {
        //ddlInspectionGrp.Focus();
        txtInspectionCode.Focus();
        //ddlInspectionGrp.Enabled = true;
        txtInspectionCode.Enabled = true;
        txtInspectionName.Enabled = true;
        txtInterval.Enabled = true;
        txtTolerance.Enabled = true;
        rdbtnTechnical.Enabled = true;
        rdbtnMarine.Enabled = true;
        rdbtnRandmInsp.Enabled = true;
        ddl_FollowUpCat.Enabled = true;
        //rdbtnYes.Enabled = true;
        //rdbtnNo.Enabled = true;
        btn_Save_Inspection.Enabled = true;
        btn_Cancel_Inspection.Visible = true;
        btn_New_Inspection.Visible = false;
        //ddlInspectionGrp.SelectedIndex = 0;
        txtInspectionCode.Text = "";
        txtInspectionName.Text = "";
        rdbtnTechnical.Checked = false;
        rdbtnMarine.Checked = false;
        rdbtnRandmInsp.Checked = false;
        //rdbtnYes.Checked = false;
        //rdbtnNo.Checked = false;
        txtInterval.Text = "";
        txtTolerance.Text = "";
        txtCreatedBy_Inspection.Text = "";
        txtCreatedOn_Inspection.Text = "";
        txtModifiedBy_Inspection.Text = "";
        txtModifiedOn_Inspection.Text = "";
        HiddenInspection.Value = "";
        GridView_Insp.SelectedIndex = -1;
        ddl_FollowUpCat.SelectedIndex = 0;
        //Alerts.ShowPanel(pnl_Charterer);
        try
        {
            Alerts.HANDLE_AUTHORITY(2, btn_New_Inspection, btn_Save_Inspection, btn_Cancel_Inspection, btn_Print_Inspection, Auth);
        }
        catch
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Default.aspx';", true);
        }
    }
    protected void btn_Save_Inspection_Click(object sender, EventArgs e)
    {
        DataTable dt1;
        int intInspectionId = -1;
        int intCreatedBy = 0;
        int intModifiedBy = 0;

        if (HiddenInspection.Value.ToString().Trim() == "")
        {
            intCreatedBy = Convert.ToInt32(Session["loginid"].ToString());
        }
        else
        {
            intInspectionId = Convert.ToInt32(HiddenInspection.Value);
            intModifiedBy = Convert.ToInt32(Session["loginid"].ToString());
        }

        int intInspectionGroup = Convert.ToInt32(ddlInspectionGrp.SelectedValue);
        string strInspCode = txtInspectionCode.Text;
        string strInspName = txtInspectionName.Text;
        int intInspectionDept=0;
        //--------Inspection Department:- 1-Technical; 2-Marine; 3-Technical & Marine
        if (rdbtnTechnical.Checked && rdbtnMarine.Checked)
            intInspectionDept = 3;
        else if (rdbtnMarine.Checked)
            intInspectionDept = 2;
        else if(rdbtnTechnical.Checked)
            intInspectionDept = 1;
        int intInterval=0;
        if(txtInterval.Text=="")
            intInterval=0;
        else
            intInterval = Convert.ToInt32(txtInterval.Text);
        int intTolerance=0;
        if(txtTolerance.Text=="")
            intTolerance=0;
        else
            intTolerance = Convert.ToInt32(txtTolerance.Text);
        string strRndmInsp;
        if (rdbtnRandmInsp.Checked)
            strRndmInsp = "Y";
        else
            strRndmInsp = "N";

        if (HiddenInspection.Value.ToString().Trim() == "")
        {
            dt1 = Inspection_Master.InspectionDetails(intInspectionId, strInspCode, strInspName, intInspectionGroup, intInterval, intTolerance, intInspectionDept, strRndmInsp, intCreatedBy, intModifiedBy, "Add", int.Parse(ddl_FollowUpCat.SelectedValue), txtColor.Text);
        }
        else
        {
            dt1 = Inspection_Master.InspectionDetails(intInspectionId, strInspCode, strInspName, intInspectionGroup, intInterval, intTolerance, intInspectionDept, strRndmInsp, intCreatedBy, intModifiedBy, "Modify", int.Parse(ddl_FollowUpCat.SelectedValue), txtColor.Text);
        }
        if (Inspection_Master.ErrMsg == "")
        {
            lbl_Inspection_Message.Text = "Record Successfully Saved.";
        }
        else { lbl_Inspection_Message.Text = "Transaction Failed."; }
        bindInspectionGrid(Convert.ToInt32(ddlInspectionGrp.SelectedValue));
        btn_New_Inspection_Click(sender, e);
        btn_Cancel_Inspection_Click(sender, e);
        try
        {
            Alerts.HANDLE_AUTHORITY(3, btn_New_Inspection, btn_Save_Inspection, btn_Cancel_Inspection, btn_Print_Inspection, Auth);
        }
        catch
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Default.aspx';", true);
        }
    }
    protected void GridView_Insp_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            Alerts.HANDLE_GRID(GridView_Insp, Auth);
        }
        catch
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Default.aspx';", true);
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton ImgBtnDelete = (ImageButton)e.Row.FindControl("ImageButton1");
            ImageButton ImgBtnEdit = (ImageButton)e.Row.FindControl("ImageButton2");
            if (GridStatus == false)
            {
                ImgBtnEdit.Enabled = false;
                ImgBtnDelete.Enabled = false;
            }
        }
    }
    protected void Show_Record_Inspection(int InspectionId)
    {
        HiddenInspection.Value = InspectionId.ToString();
        DataTable dt1 = Inspection_Master.InspectionDetails(InspectionId, "", "", 0, 0, 0, 0, "", 0, 0, "ById", 0,"");
        foreach (DataRow dr in dt1.Rows)
        {
            ddlInspectionGrp.SelectedValue = dr["InspectionGroup"].ToString();
            txtInspectionCode.Text = dr["Code"].ToString();
            txtInspectionName.Text = dr["Name"].ToString();
            txtColor.Text = dr["Color"].ToString();
            dv_color.Style.Add("background-color", "#" + txtColor.Text);
            if (dr["InspectionDept"].ToString() == "1")
            {
                rdbtnTechnical.Checked = true;
                rdbtnMarine.Checked = false;
            }
            else if (dr["InspectionDept"].ToString() == "2")
            {
                rdbtnMarine.Checked = true;
                rdbtnTechnical.Checked = false;
            }
            else if (dr["InspectionDept"].ToString() == "3")
            {
                rdbtnTechnical.Checked = true;
                rdbtnMarine.Checked = true;
            }
            else
            {
                if ((dr["InspectionDept"].ToString() == "0") || (dr["RandomInspection"].ToString() == "Y"))
                    rdbtnRandmInsp.Checked = true;
            }
            if (dr["RandomInspection"].ToString() == "Y")
            {
                rdbtnRandmInsp.Checked = true;
            }
            else
            {
                rdbtnRandmInsp.Checked = false;
            }   
            txtInterval.Text = dr["Interval"].ToString();
            txtTolerance.Text = dr["Tolerance"].ToString();
            txtCreatedBy_Inspection.Text = dr["CreatedBy"].ToString();
            txtCreatedOn_Inspection.Text = dr["CreatedOn"].ToString();
            txtModifiedBy_Inspection.Text = dr["ModifiedBy"].ToString();
            txtModifiedOn_Inspection.Text = dr["ModifiedOn"].ToString();
            if (dr["FollowUpCategory"].ToString() != "")
                ddl_FollowUpCat.SelectedValue = dr["FollowUpCategory"].ToString();
            else
                ddl_FollowUpCat.SelectedValue = "0";
        }
    }
    //protected void GridView_Insp_RowEditing(object sender, GridViewEditEventArgs e)
    //{
    //    Mode = "Edit";
    //    HiddenField hfdInspection;
    //    hfdInspection = (HiddenField)GridView_Insp.Rows[e.NewEditIndex].FindControl("Hidden_InspectionId");
    //    id = Convert.ToInt32(hfdInspection.Value.ToString());
    //    Show_Record_Inspection(id);
    //    GridView_Insp.SelectedIndex = e.NewEditIndex;
    //    btn_New_Inspection.Visible = false;
    //    btn_Cancel_Inspection.Visible = true;
    //    //ddlInspectionGrp.Enabled = true;
    //    txtInspectionCode.Enabled = true;
    //    txtInspectionName.Enabled = true;
    //    txtInterval.Enabled = true;
    //    txtTolerance.Enabled = true;
    //    rdbtnTechnical.Enabled = true;
    //    rdbtnMarine.Enabled = true;
    //    rdbtnRandmInsp.Enabled = true;
    //    ddl_FollowUpCat.Enabled = true;
    //    //rdbtnYes.Enabled = true;
    //    //rdbtnNo.Enabled = true;
    //    btn_Save_Inspection.Enabled = true;
    //    //Alerts.ShowPanel(pnl_Charterer);
    //    try
    //    {
    //        Alerts.HANDLE_AUTHORITY(5, btn_New_Inspection, btn_Save_Inspection, btn_Cancel_Inspection, btn_Print_Inspection, Auth);
    //    }
    //    catch
    //    {
    //        Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Default.aspx';", true);
    //    }
    //}
    protected void GridView_Insp_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DataTable dt1;
        int intModifiedBy = Convert.ToInt32(Session["loginid"].ToString());
        HiddenField hfdInspection;
        hfdInspection = (HiddenField)GridView_Insp.Rows[e.RowIndex].FindControl("Hidden_InspectionId");
        id = Convert.ToInt32(hfdInspection.Value.ToString());
        dt1 = Inspection_Master.InspectionDetails(id, "", "", 0, 0, 0, 0, "", 0, intModifiedBy, "Delete", 0,"");
        if (Inspection_Master.ErrMsg == "")
        {
            if (dt1.Rows.Count > 0)
            {
                if (dt1.Rows[0][0].ToString() == "0")
                {
                    lbl_Inspection_Message.Text = "Inspection cannot be deleted! It is in use.";
                    return;
                }
            }
            else
            {
                lbl_Inspection_Message.Text = "Record Successfully Deleted.";
            }
        }
        else { lbl_Inspection_Message.Text = "Record Not Deleted."; }
        bindInspectionGrid(Convert.ToInt32(ddlInspectionGrp.SelectedValue));
        if (HiddenInspection.Value.ToString() == hfdInspection.Value.ToString())
        {
            btn_New_Inspection_Click(sender, e);
        }
    }
    protected void GridView_Insp_PreRender(object sender, EventArgs e)
    {
        //if (GridView_Insp.Rows.Count <= 0) { lbl_GridView_Inspection.Text = ""; } else { if (GridStatus == false) lbl_GridView_Inspection.Text = "No. of Records Found: 0"; else lbl_GridView_Inspection.Text = "No. of Records Found: " + GridView_Insp.Rows.Count; }
        if (GridView_Insp.Rows.Count <= 0) { lbl_GridView_Inspection.Text = ""; } else { if (GridStatus == false) lbl_GridView_Inspection.Text = "No. of Records Found: 0"; else lbl_GridView_Inspection.Text = "No. of Records Found: " + HiddenFieldGridRowCount.Value; }
    }
    protected void btn_Cancel_Inspection_Click(object sender, EventArgs e)
    {
        //ddlInspectionGrp.Enabled = false;
        txtInspectionCode.Enabled = false;
        txtInspectionName.Enabled = false;
        rdbtnTechnical.Enabled = false;
        rdbtnMarine.Enabled = false;
        rdbtnRandmInsp.Enabled = false;
        ddl_FollowUpCat.Enabled = false;
        //rdbtnYes.Enabled = false;
        //rdbtnNo.Enabled = false;
        //btn_Save_Inspection.Enabled = false;
        btn_Save_Inspection.Visible = false;
        btn_Cancel_Inspection.Visible = false;
        btn_New_Inspection.Visible = true;
        //ddlInspectionGrp.SelectedIndex = 0;
        txtInspectionCode.Text = "";
        txtInspectionName.Text = "";
        rdbtnTechnical.Checked = false;
        rdbtnMarine.Checked = false;
        rdbtnRandmInsp.Checked = false;
        //rdbtnYes.Checked = false;
        //rdbtnNo.Checked = false;
        txtInterval.Text = "";
        txtTolerance.Text = "";
        txtCreatedBy_Inspection.Text = "";
        txtCreatedOn_Inspection.Text = "";
        txtModifiedBy_Inspection.Text = "";
        txtModifiedOn_Inspection.Text = "";
        HiddenInspection.Value = "";
        GridView_Insp.SelectedIndex = -1;
        ddl_FollowUpCat.SelectedIndex = 0;
        //ddlInspectionGrp_SelectedIndexChanged(sender, e);
    }
    protected void ddlInspectionGrp_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindInspectionGrid(Convert.ToInt32(ddlInspectionGrp.SelectedValue));
        GridView_Insp.SelectedIndex = -1;
    }
    public void bindBlankGrid()
    {
        DataTable dt = new DataTable();
        DataRow dr;
        dt.Columns.Add("Code");
        dt.Columns.Add("Id");
        dt.Columns.Add("Name");
        dt.Columns.Add("Interval");
        dt.Columns.Add("Tolerance");
        dt.Columns.Add("InspectionDept");

        for (int i = 0; i < 7; i++)
        {
            dr = dt.NewRow();
            dt.Rows.Add(dr);
            dt.Rows[dt.Rows.Count - 1][0] = "";
            dt.Rows[dt.Rows.Count - 1][1] = "";
            dt.Rows[dt.Rows.Count - 1][2] = "";
            dt.Rows[dt.Rows.Count - 1][3] = "";
            dt.Rows[dt.Rows.Count - 1][4] = "";
            dt.Rows[dt.Rows.Count - 1][5] = "";
        }

        GridView_Insp.DataSource = dt;
        GridView_Insp.DataBind();
        GridView_Insp.SelectedIndex = -1;
    }
    protected void GridView_Insp_RowCreated(object sender, GridViewRowEventArgs e)
    {
        //GridView_Insp.SelectedIndex = -1;
    }
    protected void GridView_Insp_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView_Insp.PageIndex = e.NewPageIndex;
        GridView_Insp.SelectedIndex = -1;
        bindInspectionGrid(Convert.ToInt32(ddlInspectionGrp.SelectedValue));
    }

    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow row = (GridViewRow)(((ImageButton)sender).NamingContainer);
        int Rowindx = row.RowIndex;
        Mode = "Edit";
        HiddenField hfdInspection;
        hfdInspection = (HiddenField)GridView_Insp.Rows[Rowindx].FindControl("Hidden_InspectionId");
        id = Convert.ToInt32(hfdInspection.Value.ToString());
        Show_Record_Inspection(id);
        GridView_Insp.SelectedIndex = Rowindx;
        btn_New_Inspection.Visible = false;
        btn_Cancel_Inspection.Visible = true;
        //ddlInspectionGrp.Enabled = true;
        txtInspectionCode.Enabled = true;
        txtInspectionName.Enabled = true;
        txtInterval.Enabled = true;
        txtTolerance.Enabled = true;
        rdbtnTechnical.Enabled = true;
        rdbtnMarine.Enabled = true;
        rdbtnRandmInsp.Enabled = true;
        ddl_FollowUpCat.Enabled = true;
        //rdbtnYes.Enabled = true;
        //rdbtnNo.Enabled = true;
        btn_Save_Inspection.Enabled = true;
        //Alerts.ShowPanel(pnl_Charterer);
        try
        {
            Alerts.HANDLE_AUTHORITY(5, btn_New_Inspection, btn_Save_Inspection, btn_Cancel_Inspection, btn_Print_Inspection, Auth);
        }
        catch
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Login.aspx';", true);
        }
    }

    protected void GridView_Insp_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Modify")
        {
            GridViewRow row = (GridViewRow)(((ImageButton)sender).NamingContainer);
            int Rowindx = row.RowIndex;
            Mode = "Edit";
            HiddenField hfdInspection;
            hfdInspection = (HiddenField)GridView_Insp.Rows[Rowindx].FindControl("Hidden_InspectionId");
            id = Convert.ToInt32(hfdInspection.Value.ToString());
            Show_Record_Inspection(id);
            GridView_Insp.SelectedIndex = Rowindx;
            btn_New_Inspection.Visible = false;
            btn_Cancel_Inspection.Visible = true;
            //ddlInspectionGrp.Enabled = true;
            txtInspectionCode.Enabled = true;
            txtInspectionName.Enabled = true;
            txtInterval.Enabled = true;
            txtTolerance.Enabled = true;
            rdbtnTechnical.Enabled = true;
            rdbtnMarine.Enabled = true;
            rdbtnRandmInsp.Enabled = true;
            ddl_FollowUpCat.Enabled = true;
            //rdbtnYes.Enabled = true;
            //rdbtnNo.Enabled = true;
            btn_Save_Inspection.Enabled = true;
            //Alerts.ShowPanel(pnl_Charterer);
            try
            {
                Alerts.HANDLE_AUTHORITY(5, btn_New_Inspection, btn_Save_Inspection, btn_Cancel_Inspection, btn_Print_Inspection, Auth);
            }
            catch
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Login.aspx';", true);
            }
        }
    }

    protected void GridView_Insp_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {

    }
}
