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

public partial class Registers_InspectionChecklist : System.Web.UI.Page
{
    public Boolean GridStatus = true;
    int id;
    Authority Auth;
    string Mode = "New";
    Button btn = new Button(); 
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        //--------------------------------- PAGE ACCESS AUTHORITY ----------------------------------------------------
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 156);
        if (chpageauth <= 0)
            Response.Redirect("blank.aspx");
        //------------------------------------------------------------------------------------------------------------
   
        ddlVesselType.AutoPostBack = true;
        if (Session["loginid"] == null)
        {
            //Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "window.parent.parent.location='../Default.aspx'", true);
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Default.aspx';", true);
        }
        lbl_GridView_InspectionCheckList.Text = "";
        lbl_InspectionCheckList_Message.Text = "";
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
            bindChapterDDl();
            ddlChapterName_SelectedIndexChanged(sender, e);
            bindSubChapterDDl();
            ddlSubChapter_SelectedIndexChanged(sender, e);
            bindVesselTypeDDl();
            try
            {
                bindInspectionChecklistGrid(Convert.ToInt32(ddlSubChapter.SelectedValue));
            }
            catch { }
            try
            {
                Alerts.HANDLE_AUTHORITY(1, btn, btn, btn, btn, Auth);
            }
            catch
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Default.aspx';", true);
            }
        }
    }
    public void bindInspectionGroupDDL()
    {
        DataSet ds1 = Inspection_Master.getMasterData("m_InspectionGroup", "Id", "(Code + ' - ' +Name) as Name");
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
    }
    public void bindChapterDDl()
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
    public void bindVersionsDDl()
    {
        try
        {
            DataTable dt2 = Budget.getTable("SELECT VersionId,VersionName FROM m_InspGroupVersions WHERE GROUPID=" + Convert.ToInt32(ddl_InspGroup.SelectedValue)).Tables[0];
            ddlVersions.DataSource = dt2;
            ddlVersions.DataValueField = "VersionId";
            ddlVersions.DataTextField = "VersionName";
            ddlVersions.DataBind();
            ddlVersions.Items.Insert(0, new ListItem("< All >", "0"));
        }
        catch { }
    }
    public void bindSubChapterDDl()
    {
        try
        {
            DataTable dt2 = Inspection_Checklist.selectSubChapterDetailsByChapId(Convert.ToInt32(ddlChapterName.SelectedValue));
            this.ddlSubChapter.DataValueField = "Id";
            //this.ddlSubChapter.DataTextField = "SubChapterName";
            this.ddlSubChapter.DataTextField = "SubChapter";
            this.ddlSubChapter.DataSource = dt2;
            if (dt2.Rows.Count > 0)
            {
                this.ddlSubChapter.DataBind();
            }
            if (dt2.Rows.Count <= 0)
            {
                this.ddlSubChapter.Items.Clear();
                this.ddlSubChapter.Items.Insert(0, new ListItem("<Select>", "0"));
            }
        }
        catch { }
    }
    public void bindVesselTypeDDl()
    {
        DataSet ds2 = Inspection_Master.getMasterDataforInspection("VesselType", "VesselTypeId", "VesselTypeName");
        this.ddlVesselType.DataSource = ds2.Tables[0];
        this.ddlVesselType.DataValueField = "VesselTypeId";
        this.ddlVesselType.DataTextField = "VesselTypeName";
        this.ddlVesselType.DataBind();
        //this.ddlVesselType.Items.Insert(0, new ListItem("<Select>", "0"));
        this.ddlVesselType.Items.Insert(0, new ListItem("All", "0"));
    }
    public void bindBlankGrid()
    {
        DataTable dt = new DataTable();
        DataRow dr;
        dt.Columns.Add("VslTypeName");
        dt.Columns.Add("Id");
        dt.Columns.Add("QuestionNo");
        dt.Columns.Add("Question");
        dt.Columns.Add("Description");
        dt.Columns.Add("QuestionType");
        dt.Columns.Add("RefNo");
        dt.Columns.Add("DeficiencyCode");
        dt.Columns.Add("VesselType");
        dt.Columns.Add("SortOrder");
        dt.Columns.Add("VersionName");

        for (int i = 0; i < 5; i++)
        {
            dr = dt.NewRow();
            dt.Rows.Add(dr);
            dt.Rows[dt.Rows.Count - 1][0] = "";
            dt.Rows[dt.Rows.Count - 1][1] = "";
            dt.Rows[dt.Rows.Count - 1][2] = "";
            dt.Rows[dt.Rows.Count - 1][3] = "";
            dt.Rows[dt.Rows.Count - 1][4] = "";
            dt.Rows[dt.Rows.Count - 1][5] = "";
            dt.Rows[dt.Rows.Count - 1][6] = "";
            dt.Rows[dt.Rows.Count - 1][7] = "";
            dt.Rows[dt.Rows.Count - 1][8] = "";
            dt.Rows[dt.Rows.Count - 1][9] = "";
            dt.Rows[dt.Rows.Count - 1][10] = "";
        }

        GridView_InspectionCheckList.DataSource = dt;
        GridView_InspectionCheckList.DataBind();
        GridView_InspectionCheckList.SelectedIndex = -1;
    }
    public void bindInspectionChecklistGrid(int Filter)
    {
        DataTable dt1;
        if (ViewState["Flag"] == "VSL")
        {
            dt1 = Inspection_Checklist.InspectionCheckListDetails(0, Filter, txtQno.Text, "", "", 0, "", "", Convert.ToInt32(ddlVesselType.SelectedValue), "", 0, 0, "VslType", 0, Convert.ToInt32(ddlVersions.SelectedValue),"");
            if (dt1.Rows.Count > 0)
            {
                GridStatus = true;
                this.GridView_InspectionCheckList.DataSource = dt1;
                this.GridView_InspectionCheckList.DataBind();
            }
            else
            {
                GridStatus = false;
                bindBlankGrid();
            }
        }
        if (ViewState["Flag"] == "QNO")
        {
            dt1 = Inspection_Checklist.InspectionCheckListDetails(0, Filter, txtQno.Text + "%", "", "", 0, "", "", 0, "", 0, 0, "Qno", 0, Convert.ToInt32(ddlVersions.SelectedValue),"");
            if (dt1.Rows.Count > 0)
            {
                GridStatus = true;
                this.GridView_InspectionCheckList.DataSource = dt1;
                this.GridView_InspectionCheckList.DataBind();
            }
            else
            {
                GridStatus = false;
                bindBlankGrid();
            }
        }
        else
        {
            dt1 = Inspection_Checklist.InspectionCheckListDetails(0, Filter, "", "", "", 0, "", "", 0, "", 0, 0, "SubChapName", 0, Convert.ToInt32(ddlVersions.SelectedValue),"");
            if (dt1.Rows.Count > 0)
            {
                GridStatus = true;
                this.GridView_InspectionCheckList.DataSource = dt1;
                //if (ViewState["Grid_QuesId"] != null)
                //{
                //    this.GridView_InspectionCheckList.SelectedIndex = Convert.ToInt32(ViewState["Grid_QuesId"].ToString());
                //}
                this.GridView_InspectionCheckList.DataBind();
            }
            else
            {
                GridStatus = false;
                bindBlankGrid();
            }
        }
        if (dt1.Rows.Count > 0)
            HiddenFieldGridRowCount.Value = dt1.Rows.Count.ToString();
        else
            HiddenFieldGridRowCount.Value = "0";
    }
    protected void ddl_InspGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindChapterDDl();
        bindVersionsDDl();
        ddlChapterName_SelectedIndexChanged(sender, e);
        GridView_InspectionCheckList.SelectedIndex = -1;
    }
    protected void ddlChapterName_SelectedIndexChanged(object sender, EventArgs e)
    {
        HiddenField_ChapId.Value = ddlChapterName.SelectedValue;
        bindSubChapterDDl();
        ddlSubChapter_SelectedIndexChanged(sender, e);
        GridView_InspectionCheckList.SelectedIndex = -1;
    }
    protected void GridView_InspectionCheckList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            Alerts.HANDLE_GRID(GridView_InspectionCheckList, Auth);
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
            //if (HiddenInspectionCheckList.Value.ToString().Trim() == "")
            //{
            //    HiddenField hfdId = (HiddenField)e.Row.FindControl("Hidden_InspChlLstId");
            //    int SelIndex = Convert.ToInt32(hfdId.Value);
            //    if (Convert.ToInt32(ViewState["Quest_Id"]) == SelIndex)
            //    {
            //        ViewState["Grid_QuesId"] = SelIndex.ToString();
            //        GridView_InspectionCheckList.SelectedIndex = 3;
            //    }
            //}
        }
    }
    protected void GridView_InspectionCheckList_RowEditing(object sender, GridViewEditEventArgs e)
    {
        Mode = "Edit";
        HiddenField hfdInspCheckList;
        hfdInspCheckList = (HiddenField)GridView_InspectionCheckList.Rows[e.NewEditIndex].FindControl("Hidden_InspChlLstId");
        id = Convert.ToInt32(hfdInspCheckList.Value.ToString());
        ddlVesselType.AutoPostBack = false;
        GridView_InspectionCheckList.SelectedIndex = e.NewEditIndex;
        try
        {
            ScriptManager.RegisterStartupScript(this, Page.GetType(), "test", "EditCheckList(" + id.ToString() + ");", true);
            Alerts.HANDLE_AUTHORITY(5, btn, btn, btn, btn, Auth);
        }
        catch
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Default.aspx';", true);
        }
    }
    protected void GridView_InspectionCheckList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DataTable dt1;
        int intModifiedBy = Convert.ToInt32(Session["loginid"].ToString());
        HiddenField hfdInspCheckList;
        hfdInspCheckList = (HiddenField)GridView_InspectionCheckList.Rows[e.RowIndex].FindControl("Hidden_InspChlLstId");
        id = Convert.ToInt32(hfdInspCheckList.Value.ToString());
        dt1 = Inspection_Checklist.InspectionCheckListDetails(id, 0, "", "", "", 0, "", "", 0, "", 0, intModifiedBy, "Delete", 0,0,"");
        if (Inspection_Checklist.ErrMsg == "")
        {
            if (dt1.Rows.Count > 0)
            {
                if (dt1.Rows[0][0].ToString() == "0")
                {
                    lbl_InspectionCheckList_Message.Text = "Question cannot be deleted! It is in use.";
                    return;
                }
            }
            else
            {
                lbl_InspectionCheckList_Message.Text = "Record Successfully Deleted.";
            }
        }
        else { lbl_InspectionCheckList_Message.Text = "Record Not Deleted."; }
        bindInspectionChecklistGrid(Convert.ToInt32(ddlSubChapter.SelectedValue));        
        if (HiddenInspectionCheckList.Value.ToString() == hfdInspCheckList.Value.ToString())
        {
            //btn_New_InspectionCheckList_Click(sender, e);
        }
    }
    protected void GridView_InspectionCheckList_PreRender(object sender, EventArgs e)
    {
        //if (GridView_InspectionCheckList.Rows.Count <= 0) { lbl_GridView_InspectionCheckList.Text = ""; } else { if (GridStatus == false)lbl_GridView_InspectionCheckList.Text = "No. of Records Found: 0"; else lbl_GridView_InspectionCheckList.Text = "No. of Records Found: " + GridView_InspectionCheckList.Rows.Count; }
        if (GridView_InspectionCheckList.Rows.Count <= 0) { lbl_GridView_InspectionCheckList.Text = ""; } else { if (GridStatus == false)lbl_GridView_InspectionCheckList.Text = "No. of Records Found: 0"; else lbl_GridView_InspectionCheckList.Text = "No. of Records Found: " + HiddenFieldGridRowCount.Value; }
    }
    
    protected void btn_Print_InspectionCheckList_Click(object sender, EventArgs e)
    {
        
    }
    protected void ddlSubChapter_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            bindInspectionChecklistGrid(Convert.ToInt32(ddlSubChapter.SelectedValue));
            GridView_InspectionCheckList.SelectedIndex = -1;
        }
        catch { }
    }
   
    protected void GridView_InspectionCheckList_RowCreated(object sender, GridViewRowEventArgs e)
    {
        //GridView_InspectionCheckList.SelectedIndex = -1;
    }
    protected void ddlVesselType_SelectedIndexChanged(object sender, EventArgs e)
    {
        //bindInspectionChecklistGrid(Convert.ToInt32(ddlSubChapter.SelectedValue));
        if (ddlVesselType.SelectedIndex > 0)
        {
            ViewState.Add("Flag", "VSL");
            bindInspectionChecklistGrid(Convert.ToInt32(ddlSubChapter.SelectedValue));
        }
        else
        {
            ViewState["Flag"] = null;
            bindInspectionChecklistGrid(Convert.ToInt32(ddlSubChapter.SelectedValue));
        }
    }
    protected void GridView_InspectionCheckList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView_InspectionCheckList.PageIndex = e.NewPageIndex;
        GridView_InspectionCheckList.SelectedIndex = -1;
        if ((ViewState["Flag"] != null) && (ddlVesselType.SelectedIndex > 0))
        {
            ViewState["Flag"] = "VSL";
            bindInspectionChecklistGrid(Convert.ToInt32(ddlSubChapter.SelectedValue));
        }
        else
        {
            ViewState["Flag"] = null;
            bindInspectionChecklistGrid(Convert.ToInt32(ddlSubChapter.SelectedValue));
        }
    }
    protected void Qno_Changed(object sender, EventArgs e)
    {
        if (txtQno.Text.Trim() == "")
        {
            ViewState["Flag"] = null;
            bindInspectionChecklistGrid(Convert.ToInt32(ddlSubChapter.SelectedValue));
        }
        else
        {
            ViewState["Flag"] = "QNO";
            bindInspectionChecklistGrid(Convert.ToInt32(ddlSubChapter.SelectedValue));
        }
    }
    protected void ddlVersion_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlSubChapter_SelectedIndexChanged(sender, e);
    }
}
