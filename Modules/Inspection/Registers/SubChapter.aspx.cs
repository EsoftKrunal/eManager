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

public partial class Registers_SubChapter : System.Web.UI.Page
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
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 155);
        if (chpageauth <= 0)
            Response.Redirect("blank.aspx");
        //------------------------------------------------------------------------------------------------------------
   
        if (Session["loginid"] == null)
        {
            //Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "window.parent.parent.location='../Default.aspx'", true);
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Default.aspx';", true);
        }
        lbl_GridView_SubChapter.Text = "";
        lbl_SubChapter_Message.Text = "";
        //int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 70);
        //if (chpageauth <= 0)
        //{
        //    Response.Redirect("Dummy.aspx");
        //}
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
            ddl_InspGroup_SelectedIndexChanged(sender, e);
            bindChapterNameDDL();
            ddlChapterName_SelectedIndexChanged(sender, e);
            try
            {
                bindSubChaptersGrid(Convert.ToInt32(ddlChapterName.SelectedValue));
            }
            catch { }
            try
            {
                Alerts.HANDLE_AUTHORITY(1, btn_New_SubChapter, btn_Save_SubChapter, btn_Cancel_SubChapter, btn_Print_SubChap, Auth);
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
            this.ddl_InspGroup.DataSource = ds1.Tables[0];
            this.ddl_InspGroup.DataValueField = "Id";
            this.ddl_InspGroup.DataTextField = "Name";
            this.ddl_InspGroup.DataBind();
        }
        else
        {
            this.ddl_InspGroup.Items.Insert(0, new ListItem("<Select>", "0"));
        }
        //this.ddl_InspGroup.Items.Insert(0, new ListItem("<Select>", "0"));
    }
    public void bindChapterNameDDL()
    {
        try
        {
            DataTable dt2 = Sub_Chapter.ChaptersNameByInspGrpId(Convert.ToInt32(ddl_InspGroup.SelectedValue));
            ddlChapterName.DataSource = dt2;
            ddlChapterName.DataValueField = "Id";
            ddlChapterName.DataTextField = "ChapterNoName";
            if (dt2.Rows.Count > 0)
            {
                ddlChapterName.DataBind();
            }
            if (dt2.Rows.Count <= 0)
            {
                ddlChapterName.Items.Clear();
                ddlChapterName.Items.Insert(0, new ListItem("<Select>", "0"));
            }
        }
        catch { }
    }
    public void bindSubChaptersGrid(int Filter)
    {
        DataTable dt1 = Sub_Chapter.SubChaptersDetails(0, Filter, "", "", 0, 0, "ChapName");
        if (dt1.Rows.Count > 0)
        {
            GridStatus = true;
            this.GridView_SubChapter.DataSource = dt1;
            this.GridView_SubChapter.DataBind();
            HiddenFieldGridRowCount.Value = dt1.Rows.Count.ToString();
        }
        else
        {
            GridStatus = false;
            bindBlankGrid();
            HiddenFieldGridRowCount.Value = "0";
        }
    }
    protected void ddl_InspGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindChapterNameDDL();
        ddlChapterName_SelectedIndexChanged(sender, e);
        GridView_SubChapter.SelectedIndex = -1;
    }
    protected void btn_New_SubChapter_Click(object sender, EventArgs e)
    {
        //ddl_InspGroup.Focus();
        txtSubChapterNo.Focus();
        //ddl_InspGroup.Enabled = true;
        //ddlChapterName.Enabled = true;
        txtSubChapterNo.Enabled = true;
        txtSubChapterName.Enabled = true;
        btn_Save_SubChapter.Enabled = true;
        btn_Cancel_SubChapter.Visible = true;
        btn_New_SubChapter.Visible = false;
        //ddl_InspGroup.SelectedIndex = 0;
        //ddlChapterName.SelectedIndex = 0;
        txtSubChapterNo.Text = "";
        txtSubChapterName.Text = "";
        txtCreatedBy_SubChapter.Text = "";
        txtCreatedOn_SubChapter.Text = "";
        txtModifiedBy_SubChapter.Text = "";
        txtModifiedOn_SubChapter.Text = "";
        HiddenSubChapter.Value = "";
        GridView_SubChapter.SelectedIndex = -1;
        //Alerts.ShowPanel(pnl_Charterer);
        try
        {
            Alerts.HANDLE_AUTHORITY(2, btn_New_SubChapter, btn_Save_SubChapter, btn_Cancel_SubChapter, btn_Print_SubChap, Auth);
        }
        catch
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Default.aspx';", true);
        }
    }
    protected void btn_Save_SubChapter_Click(object sender, EventArgs e)
    {
        DataTable dt1;
        ////int Duplicate = 0;

        //foreach (GridViewRow dg in GridView_InsGrp.Rows)
        //{
        //    HiddenField hfd;
        //    HiddenField hfd1;
        //    hfd = (HiddenField)dg.FindControl("Hidden_InspectionCode");
        //    hfd1 = (HiddenField)dg.FindControl("Hidden_InspectionGroupId");
        //    if (hfd.Value.ToString().ToUpper().Trim() == txtInspectionCode.Text.ToUpper().Trim())
        //    {
        //        if (HiddenInspectionGroup.Value.Trim() == "")
        //        {
        //            lbl_InspGrp_Message.Text = "Inspection Code Already Exists.";
        //            Duplicate = 1;
        //            break;
        //        }
        //        else if (HiddenInspectionGroup.Value.Trim() != hfd1.Value.ToString())
        //        {
        //            lbl_InspGrp_Message.Text = "Inspection Code Already Exists.";
        //            Duplicate = 1;
        //            break;
        //        }
        //    }
        //    else
        //    {
        //        lbl_InspGrp_Message.Text = "";
        //    }
        //}
        //if (Duplicate == 0)
        //{
        int intSubChapterId = -1;
        int intCreatedBy = 0;
        int intModifiedBy = 0;

        if (HiddenSubChapter.Value.ToString().Trim() == "")
        {
            intCreatedBy = Convert.ToInt32(Session["loginid"].ToString());
        }
        else
        {
            intSubChapterId = Convert.ToInt32(HiddenSubChapter.Value);
            intModifiedBy = Convert.ToInt32(Session["loginid"].ToString());
        }

        int intChapterId = Convert.ToInt32(ddlChapterName.SelectedValue);
        string strSubChapterNum = txtSubChapterNo.Text;
        string strSubChapterName = txtSubChapterName.Text;

        if (HiddenSubChapter.Value.ToString().Trim() == "")
        {
            dt1 = Sub_Chapter.SubChaptersDetails(intSubChapterId, intChapterId, strSubChapterNum, strSubChapterName, intCreatedBy, intModifiedBy, "Add");
        }
        else
        {
            dt1 = Sub_Chapter.SubChaptersDetails(intSubChapterId, intChapterId, strSubChapterNum, strSubChapterName, intCreatedBy, intModifiedBy, "Modify");
        }
        if (Sub_Chapter.ErrMsg == "")
        {
            //if (dt1.Rows.Count > 0)
            //{
            //    if (dt1.Rows[0][0].ToString().Substring(0, 9) == "There was")
            //        lbl_SubChapter_Message.Text = "Record Not Saved.";
            //}
            //else
            //{
                lbl_SubChapter_Message.Text = "Record Successfully Saved.";
            //}
        }
        else { lbl_SubChapter_Message.Text = "Transaction Failed."; }
        bindSubChaptersGrid(Convert.ToInt32(ddlChapterName.SelectedValue));        
        btn_New_SubChapter_Click(sender, e);
        btn_Cancel_SubChapter_Click(sender, e);
        //Alerts.HidePanel(pnl_Charterer);
        try
        {
            Alerts.HANDLE_AUTHORITY(3, btn_New_SubChapter, btn_Save_SubChapter, btn_Cancel_SubChapter, btn_Print_SubChap, Auth);
        }
        catch
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Default.aspx';", true);
        }
        //}
    }
    protected void GridView_SubChapter_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            Alerts.HANDLE_GRID(GridView_SubChapter, Auth);
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
    protected void Show_Record_SubChapters(int SubChapterId, object sender, EventArgs e)
    {
        HiddenSubChapter.Value = SubChapterId.ToString();
        DataTable dt1 = Sub_Chapter.SubChaptersDetails(SubChapterId, 0, "", "", 0, 0, "ById");
        foreach (DataRow dr in dt1.Rows)
        {
            //ddl_InspGroup.SelectedValue = dr["InspGrp"].ToString();
            //ddl_InspGroup_SelectedIndexChanged(sender, e);
            ddlChapterName.SelectedValue = dr["ChapterId"].ToString();
            txtSubChapterNo.Text = dr["SubChapterNo"].ToString();
            txtSubChapterName.Text = dr["SubChapterName"].ToString();
            txtCreatedBy_SubChapter.Text = dr["CreatedBy"].ToString();
            txtCreatedOn_SubChapter.Text = dr["CreatedOn"].ToString();
            txtModifiedBy_SubChapter.Text = dr["ModifiedBy"].ToString();
            txtModifiedOn_SubChapter.Text = dr["ModifiedOn"].ToString();
        }
    }
    protected void GridView_SubChapter_RowEditing(object sender, GridViewEditEventArgs e)
    {
        Mode = "Edit";
        HiddenField hfdSubChapters;
        hfdSubChapters = (HiddenField)GridView_SubChapter.Rows[e.NewEditIndex].FindControl("Hidden_SubChapterId");
        id = Convert.ToInt32(hfdSubChapters.Value.ToString());
        Show_Record_SubChapters(id, sender,e);
        GridView_SubChapter.SelectedIndex = e.NewEditIndex;
        btn_New_SubChapter.Visible = false;
        btn_Cancel_SubChapter.Visible = true;
        //ddl_InspGroup.Enabled = true;
        //ddlChapterName.Enabled = true;
        txtSubChapterNo.Enabled = true;
        txtSubChapterName.Enabled = true;
        btn_Save_SubChapter.Enabled = true;
        //Alerts.ShowPanel(pnl_Charterer);
        try
        {
            Alerts.HANDLE_AUTHORITY(5, btn_New_SubChapter, btn_Save_SubChapter, btn_Cancel_SubChapter, btn_Print_SubChap, Auth);
        }
        catch
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Default.aspx';", true);
        }
    }
    protected void GridView_SubChapter_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DataTable dt1;
        int intModifiedBy = Convert.ToInt32(Session["loginid"].ToString());
        HiddenField hfdSubChapters;
        hfdSubChapters = (HiddenField)GridView_SubChapter.Rows[e.RowIndex].FindControl("Hidden_SubChapterId");
        id = Convert.ToInt32(hfdSubChapters.Value.ToString());
        dt1 = Sub_Chapter.SubChaptersDetails(id, 0, "", "", 0, intModifiedBy, "Delete");
        if (Sub_Chapter.ErrMsg == "")
        {
            //if (dt1.Rows.Count > 0)
            //{
            //    if (dt1.Rows[0][0].ToString().Substring(0, 9) == "There was")
            //        lbl_SubChapter_Message.Text = "The Sub Chapter cannot be deleted! It is in use.";
            //}
            //else
            //{
            lbl_SubChapter_Message.Text = "Record Successfully Deleted.";
            //}
        }
        else { lbl_SubChapter_Message.Text = "The Sub Chapter cannot be deleted! It is in use."; }
        bindSubChaptersGrid(Convert.ToInt32(ddlChapterName.SelectedValue));
        if (HiddenSubChapter.Value.ToString() == hfdSubChapters.Value.ToString())
        {
            btn_New_SubChapter_Click(sender, e);
        }
    }
    protected void GridView_SubChapter_PreRender(object sender, EventArgs e)
    {
        //if (GridView_SubChapter.Rows.Count <= 0) { lbl_GridView_SubChapter.Text = ""; } else { if (GridStatus == false) lbl_GridView_SubChapter.Text = "No. of Records Found: 0"; else lbl_GridView_SubChapter.Text = "No. of Records Found: " + GridView_SubChapter.Rows.Count; }
        if (GridView_SubChapter.Rows.Count <= 0) { lbl_GridView_SubChapter.Text = ""; } else { if (GridStatus == false) lbl_GridView_SubChapter.Text = "No. of Records Found: 0"; else lbl_GridView_SubChapter.Text = "No. of Records Found: " + HiddenFieldGridRowCount.Value; }
    }
    protected void btn_Cancel_SubChapter_Click(object sender, EventArgs e)
    {
        //ddl_InspGroup.Enabled = false;
        //ddlChapterName.Enabled = false;
        txtSubChapterNo.Enabled = false;
        txtSubChapterName.Enabled = false;
        //btn_Save_SubChapter.Enabled = false;
        btn_Save_SubChapter.Visible = false;
        btn_Cancel_SubChapter.Visible = false;
        btn_New_SubChapter.Visible = true;
        //ddl_InspGroup.SelectedIndex = 0;
        //ddlChapterName.SelectedIndex = 0;
        txtSubChapterNo.Text = "";
        txtSubChapterName.Text = "";
        txtCreatedBy_SubChapter.Text = "";
        txtCreatedOn_SubChapter.Text = "";
        txtModifiedBy_SubChapter.Text = "";
        txtModifiedOn_SubChapter.Text = "";
        HiddenSubChapter.Value = "";
        GridView_SubChapter.SelectedIndex = -1;
        //ddl_InspGroup_SelectedIndexChanged(sender, e);
        //ddlChapterName_SelectedIndexChanged(sender, e);
    }
    protected void ddlChapterName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            bindSubChaptersGrid(Convert.ToInt32(ddlChapterName.SelectedValue));
            GridView_SubChapter.SelectedIndex = -1;
        }
        catch { }
    }
    public void bindBlankGrid()
    {
        DataTable dt = new DataTable();
        DataRow dr;
        dt.Columns.Add("SubChapterNo");
        dt.Columns.Add("Id");
        dt.Columns.Add("SubChapterName");

        for (int i = 0; i < 8; i++)
        {
            dr = dt.NewRow();
            dt.Rows.Add(dr);
            dt.Rows[dt.Rows.Count - 1][0] = "";
            dt.Rows[dt.Rows.Count - 1][1] = "";
            dt.Rows[dt.Rows.Count - 1][2] = "";
        }

        GridView_SubChapter.DataSource = dt;
        GridView_SubChapter.DataBind();
        GridView_SubChapter.SelectedIndex = -1;
    }
    protected void GridView_SubChapter_RowCreated(object sender, GridViewRowEventArgs e)
    {
        //GridView_SubChapter.SelectedIndex = -1;
    }
    protected void GridView_SubChapter_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView_SubChapter.PageIndex = e.NewPageIndex;
        GridView_SubChapter.SelectedIndex = -1;
        bindSubChaptersGrid(Convert.ToInt32(ddlChapterName.SelectedValue));
    }

    protected void GridView_SubChapter_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {

    }

    protected void GridView_SubChapter_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Modify")
        {
            GridViewRow row = (GridViewRow)(((ImageButton)sender).NamingContainer);
            int Rowindx = row.RowIndex;
            Mode = "Edit";
            HiddenField hfdSubChapters;
            hfdSubChapters = (HiddenField)GridView_SubChapter.Rows[Rowindx].FindControl("Hidden_SubChapterId");
            id = Convert.ToInt32(hfdSubChapters.Value.ToString());
            Show_Record_SubChapters(id, sender, e);
            GridView_SubChapter.SelectedIndex = Rowindx;
            btn_New_SubChapter.Visible = false;
            btn_Cancel_SubChapter.Visible = true;
            //ddl_InspGroup.Enabled = true;
            //ddlChapterName.Enabled = true;
            txtSubChapterNo.Enabled = true;
            txtSubChapterName.Enabled = true;
            btn_Save_SubChapter.Enabled = true;
            //Alerts.ShowPanel(pnl_Charterer);
            try
            {
                Alerts.HANDLE_AUTHORITY(5, btn_New_SubChapter, btn_Save_SubChapter, btn_Cancel_SubChapter, btn_Print_SubChap, Auth);
            }
            catch
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Login.aspx';", true);
            }
        }
    }

    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow row = (GridViewRow)(((ImageButton)sender).NamingContainer);
        int Rowindx = row.RowIndex;
        Mode = "Edit";
        HiddenField hfdSubChapters;
        hfdSubChapters = (HiddenField)GridView_SubChapter.Rows[Rowindx].FindControl("Hidden_SubChapterId");
        id = Convert.ToInt32(hfdSubChapters.Value.ToString());
        Show_Record_SubChapters(id, sender, e);
        GridView_SubChapter.SelectedIndex = Rowindx;
        btn_New_SubChapter.Visible = false;
        btn_Cancel_SubChapter.Visible = true;
        //ddl_InspGroup.Enabled = true;
        //ddlChapterName.Enabled = true;
        txtSubChapterNo.Enabled = true;
        txtSubChapterName.Enabled = true;
        btn_Save_SubChapter.Enabled = true;
        //Alerts.ShowPanel(pnl_Charterer);
        try
        {
            Alerts.HANDLE_AUTHORITY(5, btn_New_SubChapter, btn_Save_SubChapter, btn_Cancel_SubChapter, btn_Print_SubChap, Auth);
        }
        catch
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Login.aspx';", true);
        }
    }
    }
