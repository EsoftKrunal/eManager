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
using System.Data.OleDb;
using System.IO;

public partial class Transactions_CheckList_PopUp : System.Web.UI.Page
{
    int intInspDueId = 0, intInspId = 0;
    Authority Auth;
    string strInspNo="";
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        if (Session["loginid"] != null)
        {
            ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 10);
            OBJ.Invoke();
            Session["Authority"] = OBJ.Authority;
            Auth = OBJ.Authority;
        }
        try
        {
            btn_generate.Enabled = Auth.isEdit;
        }
        catch { }
        try
        {
            intInspDueId = int.Parse(Page.Request.QueryString["InsDueId"].ToString());
            intInspId = int.Parse(Page.Request.QueryString["InsId"].ToString());
            if (!Page.IsPostBack)
            {
                bindInspectionGroupDDL();
                string InspGrpId = GetInspGroup();
                ddl_InspGroup.SelectedValue = InspGrpId;
                ddl_InspGroup_SelectedIndexChanged(sender, e);
            }
        }
        catch { }
    }
    public void bindInspectionGroupDDL()
    {
        DataSet ds1 = Inspection_Master.getMasterData("m_InspectionGroup", "Id", "(Code + ' - ' +Name) as Name");
            this.ddl_InspGroup.DataSource = ds1.Tables[0];
            this.ddl_InspGroup.DataValueField = "Id";
            this.ddl_InspGroup.DataTextField = "Name";
            this.ddl_InspGroup.DataBind();
            this.ddl_InspGroup.Items.Insert(0, new ListItem("<Select>", ""));
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
            ddlChapterName.Items.Insert(0, new ListItem("<Select>", ""));
        }
        catch { }
    }
    protected void ddl_InspGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindChapterDDl();
    }
    protected string GetInspGroup()
    {
        try
        {
            string strInspectionId = "";
            DataTable dt1 = Inspection_TravelSchedule.GetInspGrpFromInspDueId(intInspDueId);
            if (dt1.Rows.Count > 0)
            {
                strInspectionId = dt1.Rows[0]["InspectionGroup"].ToString();
            }
            return strInspectionId;
        }
        catch (Exception ex) { throw ex; }
    }
    protected void btn_generate_Click(object sender, EventArgs e)
    {
        DataTable dt11 = Inspection_TravelSchedule.SelectInspectionDetailsByInspId(intInspDueId);
        if (dt11.Rows.Count > 0)
        {
            strInspNo = dt11.Rows[0]["InspectionNo"].ToString();
        }

        FileInfo fi = new FileInfo(Server.MapPath("~/EMANAGERBLOB/Inspection/CheckLists/InspectionCheckList-[" + strInspNo.Replace("/", "-") + "].htm"));

        if (fi.Exists)
        {
            fi.Delete();
        }
        string SupId="0", SupName="";
        DataTable DtSup = Common.Execute_Procedures_Select_ByQuery("SELECT SUPERINTENDENTID AS SUPID,FIRSTNAME + ' ' + LASTNAME AS SUPNAME FROM t_InspSupt INNER JOIN DBO.USERLOGIN UL ON UL.LOGINID=SUPERINTENDENTID WHERE INSPECTIONDUEID=" + intInspDueId.ToString());
        if (DtSup.Rows.Count > 0)
        {
            SupId = DtSup.Rows[0][0].ToString();
            SupName = DtSup.Rows[0][1].ToString();
        }

        string sourcepath = Server.MapPath("~/EMANAGERBLOB/Inspection/TemplateCheckLists/InspectionCheckList.htm");
        string destpath = Server.MapPath("~/EMANAGERBLOB/Inspection/CheckLists/InspectionCheckList-[" + strInspNo.Replace("/", "-") + "].htm");

        string Content = File.ReadAllText(sourcepath);
        int StartIndex = Content.IndexOf("<!--TemplateSection");
        string SectionTemplate = Content.Substring(StartIndex, Content.IndexOf("TemplateSection-->") - StartIndex).Replace("<!--TemplateSection", "");

        string LeftPart = Content.Substring(0, Content.IndexOf("<!--PALCEHOLDER-->"));
        string RightPart = Content.Substring(Content.IndexOf("<!--PALCEHOLDER-->")).Replace("<!--PALCEHOLDER-->", "");
        string MiddleSection = "";

        //String sConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source= " + path + ";Extended Properties='Excel 8.0;HDR=NO'";
        //OleDbConnection objConn = new OleDbConnection(sConnectionString);
        //OleDbCommand objCmdSelect = new OleDbCommand();
        //objConn.Open();
        LeftPart = LeftPart.Replace("VAL_INSDUEID", intInspDueId.ToString());
        LeftPart = LeftPart.Replace("<!--INSPNUMBER-->", strInspNo);

        LeftPart = LeftPart.Replace("VAL_SUPID", SupId);
        LeftPart = LeftPart.Replace("<!--SUPNAME-->", SupName);
        DataTable dtchklst = Inspection_TravelSchedule.GetCheckListDetails(int.Parse(ddlChapterName.SelectedValue));
        if (dtchklst.Rows.Count > 0)
        {
            for (int i = 0; i < dtchklst.Rows.Count; i++)
            {
                string SectionContent = SectionTemplate.Replace("lblID", "lblID_" + Convert.ToString(i + 1));
                SectionContent = SectionContent.Replace("VAL_QNO", dtchklst.Rows[i]["QuestionNo"].ToString());
                SectionContent = SectionContent.Replace("VAL_QID", dtchklst.Rows[i]["QuestionId"].ToString());
                SectionContent = SectionContent.Replace("lblQuestion", "lblQuestion_" + Convert.ToString(i + 1));
                SectionContent = SectionContent.Replace("VAL_QUEST", dtchklst.Rows[i]["Question"].ToString().Replace("'", "''"));
                SectionContent = SectionContent.Replace("ddlStatus", "ddlStatus_" + Convert.ToString(i + 1));
                SectionContent = SectionContent.Replace("txtText", "txtText_" + Convert.ToString(i + 1));
                MiddleSection = MiddleSection + SectionContent;
            }

            File.WriteAllText(destpath, LeftPart + MiddleSection + RightPart);
            aFile.Visible = true;
            aFile.HRef = "../EMANAGERBLOB/Inspection/CheckLists/" + Path.GetFileName(destpath);
            //objConn.Close();

            //string fileName = AppDomain.CurrentDomain.BaseDirectory.Replace("/", @"\");
            //fileName += "EMANAGERBLOB/Inspection/CheckLists/InspectionCheckList-[" + strInspNo.Replace("/", "-") + "].htm";
            //Response.Clear();
            //Response.ClearContent();
            //Response.ClearHeaders();
            //Response.Charset = "";
            //Response.ContentType = "application/vnd.ms-excel";
            //Response.AddHeader("Content-Disposition", "attachment;filename=\"CheckList-[" + strInspNo.Replace("/", "-") + "].xls\";");
            //Response.WriteFile(fileName);
            //Response.Flush();
            //Response.End();
        }
    }
}
