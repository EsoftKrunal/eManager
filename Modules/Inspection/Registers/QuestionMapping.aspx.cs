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

public partial class Registers_QuestionMapping : System.Web.UI.Page
{
    int id;
    Authority Auth;
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
        if (Session["loginid"] == null)
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Login.aspx';", true);
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
            bindInspectionGroupDDL();
            ddl_InspGroup_SelectedIndexChanged(sender, e);
            ddl_InspGroup1_SelectedIndexChanged(sender, e);
            Bindgrid();
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

            this.ddl_InspGroup1.DataSource = ds1.Tables[0];
            this.ddl_InspGroup1.DataValueField = "Id";
            this.ddl_InspGroup1.DataTextField = "Name";
            this.ddl_InspGroup1.DataBind();
        }
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
        }
        catch { }
    }
    public void bindVersionsDDl1()
    {
        try
        {
            DataTable dt2 = Budget.getTable("SELECT VersionId,VersionName FROM m_InspGroupVersions WHERE GROUPID=" + Convert.ToInt32(ddl_InspGroup1.SelectedValue)).Tables[0];
            ddlVersions1.DataSource = dt2;
            ddlVersions1.DataValueField = "VersionId";
            ddlVersions1.DataTextField = "VersionName";
            ddlVersions1.DataBind();
        }
        catch { }
    }

    protected void ddl_InspGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindVersionsDDl();
    }
    protected void ddl_InspGroup1_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindVersionsDDl1();
    }

    protected void Bindgrid()
    {
        string proc = "select FromQid as ID, " +
                    "mq1.QuestionNo as QuestionNo, " +
                    "mq1.Question as Question, " +
                    "ToQid as ID1,  " +
                    "mq2.QuestionNo as QuestionNo1, " +
                    "mq2.Question as Question1 " +
                    "from m_questions_map " +
                    "inner join m_questions mq1 on mq1.id=m_questions_map.FromQid " +
                    "inner join m_questions mq2 on mq2.id=m_questions_map.ToQid ";

        GridView_InspectionCheckList.DataSource = Budget.getTable(proc);
        GridView_InspectionCheckList.DataBind(); 
    }
    protected void BindQuestions()
    {
        //DataTable dt=Budget.getTable("select Id,QuestionNo from m_Questions where versionid=" + ddlVersions.SelectedValue + " and subchapterid=" + ddlSubChapter.SelectedValue).Tables[0];
        //ddlQuestions.DataSource = dt;
        //ddlQuestions.DataTextField = "QuestionNo";
        //ddlQuestions.DataValueField = "Id";
        //ddlQuestions.DataBind();  
    }
    protected void BindQuestions1()
    {
        //DataTable dt = Budget.getTable("select Id,QuestionNo from m_Questions where versionid=" + ddlVersions1.SelectedValue + " and subchapterid=" + ddlSubChapter1.SelectedValue).Tables[0];
        //ddlQuestions1.DataSource = dt;
        //ddlQuestions1.DataTextField = "QuestionNo";
        //ddlQuestions1.DataValueField = "Id";
        //ddlQuestions1.DataBind();  
    }
    protected void GridView_InspectionCheckList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //int Id = 0, Id1 = 0;
        //HiddenField hfd = (HiddenField)GridView_InspectionCheckList.Rows[e.RowIndex].FindControl("Hidden_QId");
        //Id = int.Parse(hfd.Value);
        //hfd = (HiddenField)GridView_InspectionCheckList.Rows[e.RowIndex].FindControl("Hidden_QId1");
        //Id1 = int.Parse(hfd.Value);
    }
    protected void GridView_InspectionCheckList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string s = e.CommandArgument.ToString();
        char[] sep={'|'};
        string[] Ids = s.Split(sep); 
        int Id=int.Parse(Ids[0]),Id1=int.Parse(Ids[1]);
        Budget.getTable("Delete from m_questions_map where FromQid=" + Id.ToString() + "And ToQid=" + Id1.ToString());
        Bindgrid(); 
    }
    protected void Add_Mapping(object sender, EventArgs e)
    {
        int Id = 0;
        int Id1=0; 
        try
        {
            Id = int.Parse(hfdQno.Value);
            Id1 = int.Parse(hfdQno1.Value);
        }
        catch 
        {
            lbl_InspectionCheckList_Message.Text = "Plese select source & destination Questions #.";
            return; 
        }

        DataTable dtchk=Budget.getTable("select * from m_questions_map where FromQid=" + Id.ToString() + "And ToQid=" + Id1.ToString()).Tables[0];
        if (dtchk.Rows.Count > 0)
        {
            lbl_InspectionCheckList_Message.Text = "This mapping already exists in database.";
            return; 
        }
        else
        {
            try
            {
               Budget.getTable("Insert into m_questions_map(FromQid,ToQid) Values(" + Id.ToString() + "," + Id1.ToString() + ")");
               lbl_InspectionCheckList_Message.Text="Mapping added successfully.";
            }catch 
            {
            lbl_InspectionCheckList_Message.Text="Unable to add mapping.";  
            }
        }
        Bindgrid(); 
    }
    protected void txtQno_TextChanged(object sender, EventArgs e)
    {
        DataTable dt = Budget.getTable("select Id,QuestionNo,Question from m_Questions where QuestionNo='" + txtQno.Text + "' and VersionId=" + ddlVersions.SelectedValue + " and subchapterid in (select m_subchapters.Id from m_subchapters where m_subchapters.chapterId in (select m_chapters.Id from m_chapters where InspectionGroup=" + ddl_InspGroup.SelectedValue + "))").Tables[0];
        if (dt.Rows.Count > 0)
        {
            hfdQno.Value = dt.Rows[0]["Id"].ToString();   
            lblQno.Text = dt.Rows[0]["Question"].ToString();   
        }
        else
        {
            hfdQno.Value = "";
            lblQno.Text = "";   
        }
    }
    protected void txtQno1_TextChanged(object sender, EventArgs e)
    {
        DataTable dt = Budget.getTable("select Id,QuestionNo,Question from m_Questions where QuestionNo='" + txtQno1.Text + "' and VersionId=" + ddlVersions1.SelectedValue + " and subchapterid in (select m_subchapters.Id from m_subchapters where m_subchapters.chapterId in (select m_chapters.Id from m_chapters where InspectionGroup=" + ddl_InspGroup1.SelectedValue + "))").Tables[0];
        if (dt.Rows.Count > 0)
        {
            hfdQno1.Value = dt.Rows[0]["Id"].ToString();
            lblQno1.Text = dt.Rows[0]["Question"].ToString();   
        }
        else
        {
            hfdQno1.Value = "";
            lblQno1.Text = "";   
        }
    }
}