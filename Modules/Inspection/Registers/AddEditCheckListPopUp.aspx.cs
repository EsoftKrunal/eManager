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

public partial class AddEditCheckListPopUp : System.Web.UI.Page
{
    public Boolean GridStatus = true;
    int Qid;
    Authority Auth;
    string Mode = "New";
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        //--------------------------------- PAGE ACCESS AUTHORITY ----------------------------------------------------
            int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 1055);
        if (chpageauth <= 0)
            Response.Redirect("blank.aspx");
        //------------------------------------------------------------------------------------------------------------

        ddlVesselType.AutoPostBack = true;
        if (Session["loginid"] == null)
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Default.aspx';", true);
        }
        lbl_InspectionCheckList_Message.Text = "";
        if (Session["loginid"] != null)
        {
            ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 12);
            OBJ.Invoke();
            Session["Authority"] = OBJ.Authority;
            Auth = OBJ.Authority;
        }
        if (!Page.IsPostBack)
        {
            Qid = Int32.Parse("0" + Request.QueryString["Id"]);
            bindInspectionGroupDDL();
            ddl_InspGroup_SelectedIndexChanged(sender, e);
            bindChapterDDl();
            ddlChapterName_SelectedIndexChanged(sender, e);
            bindSubChapterDDl();
            bindVesselTypeDDl();

            string strGroup="", strMainChapter="", strSubChapter = "";
            strGroup = "" + Request.QueryString["GRP"];
            strMainChapter = "" + Request.QueryString["MC"];
            strSubChapter = "" + Request.QueryString["SC"];

            if (strGroup.Trim()!="")
            {
                ddl_InspGroup.SelectedValue = strGroup;
                ddl_InspGroup_SelectedIndexChanged(sender, e);  
            }
            if (strMainChapter.Trim() != "")
            {
                ddlChapterName.SelectedValue = strMainChapter;
                ddlChapterName_SelectedIndexChanged(sender, e);
            }
            if (strSubChapter.Trim() != "")
            {
                ddlSubChapter.SelectedValue = strSubChapter;
            }

            try
            {
                Alerts.HANDLE_AUTHORITY(1, btn_New_InspectionCheckList, btn_Save_InspectionCheckList,new Button() , new Button (), Auth);
            }
            catch
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Default.aspx';", true);
            }
            btn_New_InspectionCheckList_Click(sender, e); 
            if (Qid > 0)
                Show_Record_InsCheckList(Qid);
        }
    }
    protected void ddl_InspGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindChapterDDl();
        bindVersionsDDl();
        ddlChapterName_SelectedIndexChanged(sender, e);
        if (ddl_InspGroup.SelectedValue == "1")
        {
            txtShellScore.Enabled = true;
        }
        else { txtShellScore.Text = ""; txtShellScore.Enabled = false; }

    }
    protected void ddlChapterName_SelectedIndexChanged(object sender, EventArgs e)
    {
        HiddenField_ChapId.Value = ddlChapterName.SelectedValue;
        bindSubChapterDDl();
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
            ddlVersions.Items.Insert(0, new ListItem("<Select>", "0"));
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
    protected void btn_New_InspectionCheckList_Click(object sender, EventArgs e)
    {
        txtQuestionNo.Focus();
        txtQuestionNo.Enabled = true;
        txtQuestion.Enabled = true;
        txtRefNo.Enabled = true;
        txtDeficiencyCode.Enabled = true;
        txtDescription.Enabled = true;
        txtSortOrder.Enabled = true;
        btn_Save_InspectionCheckList.Enabled = true;
        btn_New_InspectionCheckList.Visible = false;
        txtQuestionNo.Text = "";
        txtQuestion.Text = "";
        txtRefNo.Text = "";
        txtDeficiencyCode.Text = "";
        txtDescription.Text = "";
        txtSortOrder.Text = "";
        txtShellScore.Text = "";
        HiddenInspectionCheckList.Value = "";
        try
        {
            Alerts.HANDLE_AUTHORITY(2, btn_New_InspectionCheckList, btn_Save_InspectionCheckList, new Button (), new Button (), Auth);
        }
        catch
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Default.aspx';", true);
        }
    }
    protected void btn_Save_InspectionCheckList_Click(object sender, EventArgs e)
    {
        DataTable dt2;
        int Duplicate = 0;
          if (HiddenInspectionCheckList.Value.ToString().Trim() == "")
          {
              DataTable dt = Budget.getTable("select * from m_questions where versionid=" + ddlVersions.SelectedValue + " and vesseltype=" + ddlVesselType.SelectedValue + " and questionno='" + txtQuestionNo.Text + "' and subchapterid in (select Id from m_subchapters where chapterid in (select id from m_chapters where inspectiongroup=" + ddl_InspGroup.SelectedValue + "))").Tables[0];
              if (dt.Rows.Count > 0)
              {
                  lbl_InspectionCheckList_Message.Text = "Question# already exists for same group & version.";
                  return; 
              }
          }
          else
          {
              DataTable dt = Budget.getTable("select * from m_questions where id<> " + HiddenInspectionCheckList.Value + " and vesseltype=" + ddlVesselType.SelectedValue + " and versionid=" + ddlVersions.SelectedValue + " and questionno='" + txtQuestionNo.Text + "' and subchapterid in (select Id from m_subchapters where chapterid in (select id from m_chapters where inspectiongroup=" + ddl_InspGroup.SelectedValue + "))").Tables[0];
              if (dt.Rows.Count > 0)
              {
                  lbl_InspectionCheckList_Message.Text = "Question# already exists for same group & version.";
                  return;
              }
          }
        if (Duplicate == 0)
        {
            int intInspectionChecklistId = -1;
            int intCreatedBy = 0;
            int intModifiedBy = 0;
            int intSortOrder = 1;

            if (ddlSubChapter.SelectedValue == "0")
            {
                lbl_InspectionCheckList_Message.Text = "Please Select Sub Chapter.";
                ddlSubChapter.Focus();
                return;
            }

            if (HiddenInspectionCheckList.Value.ToString().Trim() == "")
            {
                intCreatedBy = Convert.ToInt32(Session["loginid"].ToString());
            }
            else
            {
                intInspectionChecklistId = Convert.ToInt32(HiddenInspectionCheckList.Value);
                intModifiedBy = Convert.ToInt32(Session["loginid"].ToString());
            }

            int intInspectionGroup = Convert.ToInt32(ddl_InspGroup.SelectedValue);
            int intChapterId = Convert.ToInt32(ddlChapterName.SelectedValue);
            int intSubChapterId = Convert.ToInt32(ddlSubChapter.SelectedValue);
            int intVesselType = Convert.ToInt32(ddlVesselType.SelectedValue);
            string strQuestionNo = txtQuestionNo.Text;
            int intQuestionType = Convert.ToInt32(ddlQuestionType.SelectedValue);
            string strquestion = txtQuestion.Text;
            string srtRefNo = txtRefNo.Text;
            string strDefCode = txtDeficiencyCode.Text;
            string strdescription = txtDescription.Text;
            if (txtSortOrder.Text != "")
            {
                intSortOrder = int.Parse(txtSortOrder.Text);
            }

            if (HiddenInspectionCheckList.Value.ToString().Trim() == "")
            {
                dt2 = Inspection_Checklist.InspectionCheckListDetails(intInspectionChecklistId, intSubChapterId, strQuestionNo, srtRefNo, strquestion, intQuestionType, strdescription, "", intVesselType, strDefCode, intCreatedBy, intModifiedBy, "Add", intSortOrder, Convert.ToInt32(ddlVersions.SelectedValue),txtShellScore.Text);
            }
            else
            {
                string strolddesc = "";
                DataTable dt1 = Inspection_Checklist.InspectionCheckListDetails(Convert.ToInt32(HiddenInspectionCheckList.Value), 0, "", "", "", 0, "", "", 0, "", 0, 0, "ById", 0,0,"");
                foreach (DataRow dr1 in dt1.Rows)
                {
                    strolddesc = dr1["Description"].ToString();
                }
                dt2 = Inspection_Checklist.InspectionCheckListDetails(intInspectionChecklistId, intSubChapterId, strQuestionNo, srtRefNo, strquestion, intQuestionType, strdescription, strolddesc, intVesselType, strDefCode, intCreatedBy, intModifiedBy, "Modify", intSortOrder,Convert.ToInt32(ddlVersions.SelectedValue),txtShellScore.Text);
            }
            if (Inspection_Checklist.ErrMsg == "")
            {
                lbl_InspectionCheckList_Message.Text = "Record Successfully Saved.";
            }
            else { lbl_InspectionCheckList_Message.Text = "Transaction Failed."; }
            btn_New_InspectionCheckList_Click(sender, e);
            try
            {
                Alerts.HANDLE_AUTHORITY(3, btn_New_InspectionCheckList, btn_Save_InspectionCheckList, new Button (),new Button (), Auth);
            }
            catch
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Default.aspx';", true);
            }
        }
    }
    protected void Show_Record_InsCheckList(int InspCheckListId)
    {
        HiddenInspectionCheckList.Value = InspCheckListId.ToString();
        DataTable dt1 = Inspection_Checklist.InspectionCheckListDetails(InspCheckListId, 0, "", "", "", 0, "", "", 0, "", 0, 0, "ById", 0,0,"");
        foreach (DataRow dr in dt1.Rows)
        {
            HiddenInspectionCheckList.Value = InspCheckListId.ToString(); 
            ddl_InspGroup.SelectedValue = dr["InspGroup"].ToString();
            ddl_InspGroup_SelectedIndexChanged(new object(),new EventArgs());
            ddlChapterName.SelectedValue = dr["ChapId"].ToString();
            ddlChapterName_SelectedIndexChanged(new object(), new EventArgs());
            ddlSubChapter.SelectedValue = dr["SubChapterId"].ToString();
            ddlVesselType.SelectedValue = dr["VesselType"].ToString();
            txtQuestionNo.Text = dr["QuestionNo"].ToString();
            ddlQuestionType.SelectedValue = dr["QuestionType"].ToString();
            txtQuestion.Text = dr["Question"].ToString();
            txtRefNo.Text = dr["RefNo"].ToString();
            txtDeficiencyCode.Text = dr["DeficiencyCode"].ToString();
            txtDescription.Text = dr["Description"].ToString();
            txtSortOrder.Text = dr["SortOrder"].ToString();
            ddlVersions.SelectedValue = dr["VersionId"].ToString();
            txtShellScore.Text = dr["ShellScore"].ToString();
            
        }
    }
}
