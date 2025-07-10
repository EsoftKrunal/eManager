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

public partial class Registers_ChaptersEntry : System.Web.UI.Page
{
    public Boolean GridStatus=true;
    int id;
    Authority Auth;
    string Mode = "New";
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        //--------------------------------- PAGE ACCESS AUTHORITY ----------------------------------------------------
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 154);
        if (chpageauth <= 0)
            Response.Redirect("blank.aspx");
        //------------------------------------------------------------------------------------------------------------
   
        if (Session["loginid"] == null)
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Default.aspx';", true);
        }
        lbl_GridView_ChaptersEntry.Text = "";
        lbl_ChaptersEntry_Message.Text = "";
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
                bindChaptersGrid(Convert.ToInt32(ddl_InspGroup.SelectedValue));
            }
            catch { }
            try
            {
                Alerts.HANDLE_AUTHORITY(1, btn_New_ChaptersEntry, btn_Save_ChaptersEntry, btn_Cancel_ChaptersEntry, btn_Print_ChpEntry, Auth);
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
        this.ddl_InspGroup.DataSource = ds1.Tables[0];
        if (ds1.Tables[0].Rows.Count > 0)
        {
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
    public void bindChaptersGrid(int Filter)
    {
        DataTable dt1 = Chapters_Entry.ChaptersDetails(0, Filter, 0, "", 0, 0, "Inpgrptype");
        if (dt1.Rows.Count > 0)
        {
            GridStatus = true;
            this.GridView_ChaptersEntry.DataSource = dt1;
            this.GridView_ChaptersEntry.DataBind();
            HiddenFieldGridRowCount.Value = dt1.Rows.Count.ToString();
        }
        else
        {
            GridStatus = false;
            bindBlankGrid();
            HiddenFieldGridRowCount.Value = "0";
        }
    }
    protected void btn_New_ChaptersEntry_Click(object sender, EventArgs e)
    {
        //ddl_InspGroup.Focus();
        txtChapterNo.Focus();
        //ddl_InspGroup.Enabled = true;
        txtChapterNo.Enabled = true;
        txtChapterName.Enabled = true;
        btn_Save_ChaptersEntry.Enabled = true;
        btn_Cancel_ChaptersEntry.Visible = true;
        btn_New_ChaptersEntry.Visible = false;
        //ddl_InspGroup.SelectedIndex = 0;
        txtChapterNo.Text = "";
        txtChapterName.Text = "";
        txtCreatedBy_ChaptersEntry.Text = "";
        txtCreatedOn_ChaptersEntry.Text = "";
        txtModifiedBy_ChaptersEntry.Text = "";
        txtModifiedOn_ChaptersEntry.Text = "";
        HiddenChaptersEntry.Value = "";
        GridView_ChaptersEntry.SelectedIndex = -1;
        //Alerts.ShowPanel(pnl_Charterer);
        try
        {
            Alerts.HANDLE_AUTHORITY(2, btn_New_ChaptersEntry, btn_Save_ChaptersEntry, btn_Cancel_ChaptersEntry, btn_Print_ChpEntry, Auth);
        }
        catch
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Default.aspx';", true);
        }
    }
    protected void btn_Save_ChaptersEntry_Click(object sender, EventArgs e)
    {
        DataTable dt1;
        int Duplicate = 0;

        foreach (GridViewRow dg in GridView_ChaptersEntry.Rows)
        {
            HiddenField hfd;
            HiddenField hfd1, hfd2;
            hfd = (HiddenField)dg.FindControl("Hidden_ChapterNo");
            hfd1 = (HiddenField)dg.FindControl("Hidden_ChapterId");
            hfd2 = (HiddenField)dg.FindControl("Hidden_InspGrp");
            if ((hfd.Value.ToString().ToUpper().Trim() == txtChapterNo.Text.ToUpper().Trim()) && (hfd2.Value.ToString().ToUpper().Trim()==ddl_InspGroup.SelectedValue))
            {
                if (HiddenChaptersEntry.Value.Trim() == "")
                {
                    lbl_ChaptersEntry_Message.Text = "Inspection Group & Chapter# Already Exists.";
                    Duplicate = 1;
                    break;
                }
                else if (HiddenChaptersEntry.Value.Trim() != hfd1.Value.ToString())
                {
                    lbl_ChaptersEntry_Message.Text = "Inspection Group & Chapter# Already Exists.";
                    Duplicate = 1;
                    break;
                }
            }
            else
            {
                lbl_ChaptersEntry_Message.Text = "";
            }
        }
        if (Duplicate == 0)
        {
            int intChapterId = -1;
            int intCreatedBy = 0;
            int intModifiedBy = 0;

            if (HiddenChaptersEntry.Value.ToString().Trim() == "")
            {
                intCreatedBy = Convert.ToInt32(Session["loginid"].ToString());
            }
            else
            {
                intChapterId = Convert.ToInt32(HiddenChaptersEntry.Value);
                intModifiedBy = Convert.ToInt32(Session["loginid"].ToString());
            }

            int intInspectionGroup = Convert.ToInt32(ddl_InspGroup.SelectedValue);
            int intChapterNum = Convert.ToInt32(txtChapterNo.Text);
            string strChapterName = txtChapterName.Text;

            if (HiddenChaptersEntry.Value.ToString().Trim() == "")
            {
                dt1 = Chapters_Entry.ChaptersDetails(intChapterId, intInspectionGroup, intChapterNum, strChapterName, intCreatedBy, intModifiedBy, "Add");
            }
            else
            {
                dt1 = Chapters_Entry.ChaptersDetails(intChapterId, intInspectionGroup, intChapterNum, strChapterName, intCreatedBy, intModifiedBy, "Modify");
            }
            if (Chapters_Entry.ErrMsg == "")
            {
                //if (dt1.Rows.Count > 0)
                //{
                //    //if (dt1.Rows[0][0].ToString().Substring(0, 9) == "There was")
                //    lbl_ChaptersEntry_Message.Text = "Record Not Saved.";
                //}
                //else
                //{
                    lbl_ChaptersEntry_Message.Text = "Record Successfully Saved.";
                //}
            }
            else
            { lbl_ChaptersEntry_Message.Text = "Transaction Failed."; }
            bindChaptersGrid(Convert.ToInt32(ddl_InspGroup.SelectedValue));            
            btn_New_ChaptersEntry_Click(sender, e);
            btn_Cancel_ChaptersEntry_Click(sender, e);
            //Alerts.HidePanel(pnl_Charterer);
            try
            {
                Alerts.HANDLE_AUTHORITY(3, btn_New_ChaptersEntry, btn_Save_ChaptersEntry, btn_Cancel_ChaptersEntry, btn_Print_ChpEntry, Auth);
            }
            catch
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Default.aspx';", true);
            }
        }
    }
    protected void GridView_ChaptersEntry_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            Alerts.HANDLE_GRID(GridView_ChaptersEntry, Auth);
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
    protected void Show_Record_Chapters(int ChapterId)
    {
        HiddenChaptersEntry.Value = ChapterId.ToString();
        DataTable dt1 = Chapters_Entry.ChaptersDetails(ChapterId, 0, 0, "", 0, 0, "ById");
        foreach (DataRow dr in dt1.Rows)
        {
            ddl_InspGroup.SelectedValue = dr["InspectionGroup"].ToString();
            txtChapterNo.Text = dr["ChapterNo"].ToString();
            txtChapterName.Text = dr["ChapterName"].ToString();
            txtCreatedBy_ChaptersEntry.Text = dr["CreatedBy"].ToString();
            txtCreatedOn_ChaptersEntry.Text = dr["CreatedOn"].ToString();
            txtModifiedBy_ChaptersEntry.Text = dr["ModifiedBy"].ToString();
            txtModifiedOn_ChaptersEntry.Text = dr["ModifiedOn"].ToString();
        }
    }
    protected void GridView_ChaptersEntry_RowEditing(object sender, GridViewEditEventArgs e)
    {
        Mode = "Edit";
        HiddenField hfdChapters;
        hfdChapters = (HiddenField)GridView_ChaptersEntry.Rows[e.NewEditIndex].FindControl("Hidden_ChapterId");
        id = Convert.ToInt32(hfdChapters.Value.ToString());
        Show_Record_Chapters(id);
        GridView_ChaptersEntry.SelectedIndex = e.NewEditIndex;
        btn_New_ChaptersEntry.Visible = false;
        btn_Cancel_ChaptersEntry.Visible = true;
        //ddl_InspGroup.Enabled = true;
        txtChapterNo.Enabled = true;
        txtChapterName.Enabled = true;
        btn_Save_ChaptersEntry.Enabled = true;
        //Alerts.ShowPanel(pnl_Charterer);
        try
        {
            Alerts.HANDLE_AUTHORITY(5, btn_New_ChaptersEntry, btn_Save_ChaptersEntry, btn_Cancel_ChaptersEntry, btn_Print_ChpEntry, Auth);
        }
        catch
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Login.aspx';", true);
        }
    }
    protected void GridView_ChaptersEntry_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DataTable dt1;
        int intModifiedBy = Convert.ToInt32(Session["loginid"].ToString());
        HiddenField hfdChapters;
        hfdChapters = (HiddenField)GridView_ChaptersEntry.Rows[e.RowIndex].FindControl("Hidden_ChapterId");
        id = Convert.ToInt32(hfdChapters.Value.ToString());
        dt1 = Chapters_Entry.ChaptersDetails(id, 0, 0, "", 0, intModifiedBy, "Delete");
        if (Chapters_Entry.ErrMsg == "")
        {
            //if (dt1.Rows.Count > 0)
            //{
            //    if (dt1.Rows[0][0].ToString().Substring(0, 9) == "There was")
            //        lbl_ChaptersEntry_Message.Text = "The Chapter cannot be deleted! It is in use.";
            //}
            //else
            //{
            lbl_ChaptersEntry_Message.Text = "Record Successfully Deleted.";
            //}
        }
        else { lbl_ChaptersEntry_Message.Text = "The Chapter cannot be deleted! It is in use."; }        
        bindChaptersGrid(Convert.ToInt32(ddl_InspGroup.SelectedValue));
        if (HiddenChaptersEntry.Value.ToString() == hfdChapters.Value.ToString())
        {
            btn_New_ChaptersEntry_Click(sender, e);
        }
    }
    protected void GridView_ChaptersEntry_PreRender(object sender, EventArgs e)
    {
        //if (GridView_ChaptersEntry.Rows.Count <= 0) { lbl_GridView_ChaptersEntry.Text = ""; } else { if (GridStatus == false) lbl_GridView_ChaptersEntry.Text = "No. of Records Found: 0"; else lbl_GridView_ChaptersEntry.Text = "No. of Records Found: " + GridView_ChaptersEntry.Rows.Count; }
        if (GridView_ChaptersEntry.Rows.Count <= 0) { lbl_GridView_ChaptersEntry.Text = ""; } else { if (GridStatus == false) lbl_GridView_ChaptersEntry.Text = "No. of Records Found: 0"; else lbl_GridView_ChaptersEntry.Text = "No. of Records Found: " + HiddenFieldGridRowCount.Value; }
    }
    protected void btn_Cancel_ChaptersEntry_Click(object sender, EventArgs e)
    {
        //ddl_InspGroup.Enabled = false;
        txtChapterNo.Enabled = false;
        txtChapterName.Enabled = false;
        //btn_Save_ChaptersEntry.Enabled = false;
        btn_Save_ChaptersEntry.Visible = false;
        btn_Cancel_ChaptersEntry.Visible = false;
        btn_New_ChaptersEntry.Visible = true;
        //ddl_InspGroup.SelectedIndex = 0;
        txtChapterNo.Text = "";
        txtChapterName.Text = "";
        txtCreatedBy_ChaptersEntry.Text = "";
        txtCreatedOn_ChaptersEntry.Text = "";
        txtModifiedBy_ChaptersEntry.Text = "";
        txtModifiedOn_ChaptersEntry.Text = "";
        HiddenChaptersEntry.Value = "";
        GridView_ChaptersEntry.SelectedIndex = -1;
        //ddl_InspGroup_SelectedIndexChanged(sender, e);
    }
    protected void ddl_InspGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindChaptersGrid(Convert.ToInt32(ddl_InspGroup.SelectedValue));
        GridView_ChaptersEntry.SelectedIndex = -1;
    }
    public void bindBlankGrid()
    {
        DataTable dt = new DataTable();
        DataRow dr;
        dt.Columns.Add("ChapterNo");
        dt.Columns.Add("Id");
        dt.Columns.Add("InspectionGroup");
        dt.Columns.Add("ChapterName");

        for (int i = 0; i < 9; i++)
        {
            dr = dt.NewRow();
            dt.Rows.Add(dr);
            dt.Rows[dt.Rows.Count - 1][0] = "";
            dt.Rows[dt.Rows.Count - 1][1] = "";
            dt.Rows[dt.Rows.Count - 1][2] = "";
            dt.Rows[dt.Rows.Count - 1][3] = "";
        }
        
        GridView_ChaptersEntry.DataSource = dt;
        GridView_ChaptersEntry.DataBind();
        GridView_ChaptersEntry.SelectedIndex = -1;
    }
    protected void GridView_ChaptersEntry_RowCreated(object sender, GridViewRowEventArgs e)
    {
        //GridView_ChaptersEntry.SelectedIndex = -1;
    }
    protected void GridView_ChaptersEntry_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView_ChaptersEntry.PageIndex = e.NewPageIndex;
        GridView_ChaptersEntry.SelectedIndex = -1;
        bindChaptersGrid(Convert.ToInt32(ddl_InspGroup.SelectedValue));
    }

    protected void GridView_ChaptersEntry_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {

    }

    protected void GridView_ChaptersEntry_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Modify")
        {
            GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
            int Rowindx = row.RowIndex;
            Mode = "Edit";
            HiddenField hfdChapters;
            hfdChapters = (HiddenField)GridView_ChaptersEntry.Rows[Rowindx].FindControl("Hidden_ChapterId");
            id = Convert.ToInt32(hfdChapters.Value.ToString());
            Show_Record_Chapters(id);
            GridView_ChaptersEntry.SelectedIndex = Rowindx;
            btn_New_ChaptersEntry.Visible = false;
            btn_Cancel_ChaptersEntry.Visible = true;
            //ddl_InspGroup.Enabled = true;
            txtChapterNo.Enabled = true;
            txtChapterName.Enabled = true;
            btn_Save_ChaptersEntry.Enabled = true;
            //Alerts.ShowPanel(pnl_Charterer);
            try
            {
                Alerts.HANDLE_AUTHORITY(5, btn_New_ChaptersEntry, btn_Save_ChaptersEntry, btn_Cancel_ChaptersEntry, btn_Print_ChpEntry, Auth);
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
        HiddenField hfdChapters;
        hfdChapters = (HiddenField)GridView_ChaptersEntry.Rows[Rowindx].FindControl("Hidden_ChapterId");
        id = Convert.ToInt32(hfdChapters.Value.ToString());
        Show_Record_Chapters(id);
        GridView_ChaptersEntry.SelectedIndex = Rowindx;
        btn_New_ChaptersEntry.Visible = false;
        btn_Cancel_ChaptersEntry.Visible = true;
        //ddl_InspGroup.Enabled = true;
        txtChapterNo.Enabled = true;
        txtChapterName.Enabled = true;
        btn_Save_ChaptersEntry.Enabled = true;
        //Alerts.ShowPanel(pnl_Charterer);
        try
        {
            Alerts.HANDLE_AUTHORITY(5, btn_New_ChaptersEntry, btn_Save_ChaptersEntry, btn_Cancel_ChaptersEntry, btn_Print_ChpEntry, Auth);
        }
        catch
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Login.aspx';", true);
        }
    }
  }
