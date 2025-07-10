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

public partial class Vetting_VIQ : System.Web.UI.Page
{
    public AuthenticationManager Auth;
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        //--------------------------------- PAGE ACCESS AUTHORITY ----------------------------------------------------
        Auth = new AuthenticationManager(305, Convert.ToInt32(Session["loginid"].ToString()), ObjectType.Page);
        if (! Auth.IsView)
            Response.Redirect("blank.aspx");
        //------------------------------------------------------------------------------------------------------------
        lbl_Message.Text = "";
        if (!Page.IsPostBack)
        {
            bindChapterDDl();
            bindVesselTypeDDl();
            chklstRanks.DataSource = Common.Execute_Procedures_Select_ByQuery("SELECT RANKCODE,RANKID FROM DBO.MP_ALLRANK WHERE LTRIM(RTRIM(RANKID)) IN(1,2,4,12,15)");
            chklstRanks.DataTextField = "RankCode";
            chklstRanks.DataValueField = "RankId";
            chklstRanks.DataBind();

            chklstRanks1.DataSource = Common.Execute_Procedures_Select_ByQuery("SELECT RANKCODE,RANKID FROM DBO.MP_ALLRANK WHERE LTRIM(RTRIM(RANKID)) IN(6,9,28,34,30,35,36,16,17,25,21,38,40,42,45)");
            chklstRanks1.DataTextField = "RankCode";
            chklstRanks1.DataValueField = "RankId";
            chklstRanks1.DataBind();
            btnShow_Click(sender, e);
            
        }
    }
    protected void radSireCdi_OnCheckedChanged(object sender, EventArgs e)
    {
        bindChapterDDl();
    }

    protected void btnClosePOP_Click(object sender, EventArgs e)
    {
        bindInspectionChecklistGrid();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (txtOfficeRemarks.Text.Trim() == "")
        {
            ProjectCommon.ShowMessage("Please enter office remarks.");
            return;
        }

        int QuestionId = Common.CastAsInt32(hfdQid.Value);
        
        string Ranks = "";
        foreach (ListItem chk in chklstRanks.Items)
        {
            if (chk.Selected)
                Ranks += "," + chk.Value;
        }
        if (Ranks.StartsWith(","))
        {
            Ranks = Ranks.Substring(1);

            Common.Execute_Procedures_Select_ByQuery("DELETE FROM m_Questions_Ranks WHERE QUESTIONID=" + QuestionId.ToString());
            Common.Execute_Procedures_Select_ByQuery("DELETE FROM m_Questions_Ranks_Inv WHERE QUESTIONID=" + QuestionId.ToString());
            Common.Execute_Procedures_Select_ByQuery("UPDATE m_Questions SET PreVetting=" + ((chkPreVetting.Checked) ? 1 : 0) + ",OfficeRemarks='" + txtOfficeRemarks.Text.Trim().Replace("'", "`") + "' WHERE ID=" + QuestionId.ToString());
            Common.Execute_Procedures_Select_ByQuery("INSERT INTO m_Questions_Ranks SELECT " + QuestionId + ",RANKID FROM DBO.MP_ALLRANK WHERE RANKID IN (" + Ranks + ") AND RANKCODE<>''");

            string Ranks1 = "";
            foreach (ListItem chk in chklstRanks1.Items)
            {
                if (chk.Selected)
                    Ranks1 += "," + chk.Value;
            }
            if (Ranks1.StartsWith(","))
            {
                Ranks1 = Ranks1.Substring(1);
                Common.Execute_Procedures_Select_ByQuery("INSERT INTO m_Questions_Ranks_Inv SELECT " + QuestionId + ",RANKID FROM DBO.MP_ALLRANK WHERE RANKID IN (" + Ranks1 + ") AND RANKCODE<>''");
                
            }

            bindInspectionChecklistGrid();
            ProjectCommon.ShowMessage("Saved successfully.");
        }
        else 
        {
            ProjectCommon.ShowMessage("Please select ranks to Update.");
        }
    }
    public void bindChapterDDl()
    {
        try
        {
            DataTable dt2 = Sub_Chapter.ChaptersNameByInspGrpId((radSire.Checked)?1:2);
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
    public void bindVesselTypeDDl()
    {
        DataSet ds2 = Inspection_Master.getMasterDataforInspection("VesselType", "VesselTypeId", "VesselTypeName");
        this.ddlVesselType.DataSource = ds2.Tables[0];
        this.ddlVesselType.DataValueField = "VesselTypeId";
        this.ddlVesselType.DataTextField = "VesselTypeName";
        this.ddlVesselType.DataBind();
        this.ddlVesselType.Items.Insert(0, new ListItem("All", "0"));
    }
    public void bindInspectionChecklistGrid()
    {
        int  MainGroup= (radSire.Checked)?1:2;
        int Filter = Common.CastAsInt32(ddlChapterName.SelectedValue);
        string sql ="SELECT Row_Number() Over(ORDER BY SORTORDER) AS SNO,Q.Id, " +
                    "ChapterId,SortOrder,[VslTypeName]=CASE WHEN [VesselType]=0 THEN 'All' ELSE (SELECT VesselTypeName FROM dbo.VesselType WHERE VesselTypeId=Q.VesselType) END,  " +
                    "(SELECT VersionName FROM m_InspGroupVersions mst WHERE mst.VersionId=Q.VersionId And mst.GroupId=dbo.GetInspectionGroupId(Q.Id)) AS [VersionName] " +
                    ",Q.[QuestionNo],[Question],dbo.getResponsibilites(Q.Id) as Responsibilites " + 
                    ",Desc_Visible=(case when ltrim(rtrim(Description))='' then 'none' else '' end) " +
                    ",OfficeRemarks_Visible=(case when ltrim(rtrim(isnull(OfficeRemarks,'')))='' then 'none' else '' end) " +
                    "FROM " +
                    "M_QUESTIONS Q  " +
                    "INNER JOIN M_SUBCHAPTERS SC ON Q.SUBCHAPTERID=SC.ID AND SC.CHAPTERID=" + ddlChapterName.SelectedValue + " " +
                    "INNER JOIN M_CHAPTERS C ON SC.CHAPTERID=C.ID AND C.INSPECTIONGROUP=" + MainGroup.ToString() + " " +
                    "WHERE Q.VERSIONID IN ( SELECT MAX(VERSIONID) FROM m_InspGroupVersions WHERE GROUPID=" + MainGroup.ToString() + ") ";

        string whereclause = "";
        if(ddlVesselType.SelectedIndex >0)
            whereclause += " And VesselType = " + ddlVesselType.SelectedValue;
        if(txtQno.Text.Trim()!="")
            whereclause += " AND QuestionNo like '%" + txtQno.Text.Trim() + "%'";

        DataTable dt = Budget.getTable(sql + whereclause).Tables[0];
        rpt_Questions.DataSource = dt;
        rpt_Questions.DataBind();
    }
    
    protected void ddl_InspGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindChapterDDl();
    }
    
    protected void btnShow_Click(object sender, EventArgs e)
    {
       bindInspectionChecklistGrid();
    }
}
