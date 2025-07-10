using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using Ionic.Zip;

public partial class VIMS_AddSubVIQDetails : System.Web.UI.Page
{
    public AuthenticationManager Auth;

    public string VesselCode
    {
        get { return ViewState["VesselCode"].ToString(); }
        set { ViewState["VesselCode"] = value; }
    }
    public int PVIQId
    {
        get { return Common.CastAsInt32(ViewState["PVIQId"]); }
        set { ViewState["PVIQId"] = value; }
    }
    public int UserId
    {
        get { return Common.CastAsInt32(ViewState["UserId"]); }
        set { ViewState["UserId"] = value; }
    }
    public int VIQId
    {
        get { return Common.CastAsInt32(ViewState["VIQId"]); }
        set { ViewState["VIQId"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        //--------------------------------- PAGE ACCESS AUTHORITY ----------------------------------------------------
        Auth = new AuthenticationManager(306, Convert.ToInt32(Session["loginid"].ToString()), ObjectType.Page);
        if (!Auth.IsView)
            Response.Redirect("blank.aspx");
        //------------------------------------------------------------------------------------------------------------
       
        if (!Page.IsPostBack)
        {
            VesselCode = Request.QueryString["VSL"];
            UserId = Convert.ToInt32(Session["loginid"].ToString());
            PVIQId = Common.CastAsInt32(Request.QueryString["PVIQId"]);
            VIQId = Common.CastAsInt32(Request.QueryString["VIQId"]);

            string sql = "SELECT * FROM [dbo].VIQ_VIQMaster WHERE VESSELCODE='" + VesselCode + "' AND VIQId=" + VIQId.ToString() + " AND VIQStatus=0";
            DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
            btnLock.Visible = (dt.Rows.Count > 0) && Auth.IsUpdate;

            if (VIQId > 0)
            {
                tr_Create.Visible = false;
                tr_Edit.Visible = true; 
                BindChaptersEdit();
            }
            else
            {
                tr_Create.Visible = true;
                tr_Edit.Visible = false; 
                BindChapters();
            }
        }
    }
    public void BindChapters()
    {

        string sql = "SELECT viqno,targetdate FROM [dbo].VIQ_VIQMaster WHERE VESSELCODE='" + VesselCode + "' AND VIQId=" + VIQId.ToString();
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
        if(dt.Rows.Count>0)
            lblheader.Text = dt.Rows[0]["VIQNO"].ToString();

        sql = "SELECT " + PVIQId +" as PVIQId,*,((DONEQ*100)/(CASE WHEN NOQ=0 THEN 1 ELSE NOQ END)) AS PERDONE FROM " +
                    "( " +
                    "SELECT DISTINCT m.VESSELCODE,M.VIQID,CHAPTERID,CHAPTERNO,CHAPTERNAME, " +
                    "(SELECT COUNT(QUESTIONID) FROM [dbo].[vw_VIQQuestionRanks] WHERE VESSELCODE='" + VesselCode + "' AND CHAPTERID=M.CHAPTERID And VIQID=" + PVIQId.ToString() + ") AS NOQ , " +
                    "(SELECT COUNT(*) FROM TEMP_OFFICE_VIQ D WHERE USERID=" + UserId + " AND D.QuestionId IN (SELECT QUESTIONID FROM [dbo].[VIQ_VIQDetails] WHERE CHAPTERID=M.CHAPTERID)) AS DONEQ  " +
                    "FROM [dbo].[VIQ_VIQDetails] M WHERE VESSELCODE='" + VesselCode + "' AND VIQID=" + PVIQId.ToString() + " " +
                    ") C " +
                    "ORDER BY cast(CHAPTERNO as int)";
        dt = Common.Execute_Procedures_Select_ByQuery(sql);
        rpt_Chapters.DataSource = dt;
        rpt_Chapters.DataBind();
    }
    public void BindChaptersEdit()
    {

        string sql = "SELECT viqno,targetdate FROM [dbo].VIQ_VIQMaster WHERE VESSELCODE='" + VesselCode + "' AND VIQId=" + VIQId.ToString();
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
        if (dt.Rows.Count > 0)
            lblheader.Text = dt.Rows[0]["VIQNO"].ToString();

        sql = "SELECT " + PVIQId + " as PVIQId,*,((DONEQ*100)/(CASE WHEN NOQ=0 THEN 1 ELSE NOQ END)) AS PERDONE FROM " +
                    "( " +
                    "SELECT DISTINCT m.VESSELCODE,M.VIQID,CHAPTERID,CHAPTERNO,CHAPTERNAME, " +
                    "(SELECT COUNT(QUESTIONID) FROM [dbo].[vw_VIQQuestionRanks] WHERE VESSELCODE='" + VesselCode + "' AND CHAPTERID=M.CHAPTERID And VIQID=" + PVIQId.ToString() + ") AS NOQ , " +
                    "(SELECT COUNT(*) FROM [dbo].[VIQ_VIQDetailsRanks] D WHERE VESSELCODE='" + VesselCode + "' AND VIQID=" + VIQId + " AND D.QuestionId IN (SELECT QUESTIONID FROM [dbo].[VIQ_VIQDetails] WHERE CHAPTERID=M.CHAPTERID)) AS DONEQ  " +
                    "FROM [dbo].[VIQ_VIQDetails] M WHERE VESSELCODE='" + VesselCode + "' AND VIQID=" + PVIQId.ToString() + " " +
                    ") C " +
                    "ORDER BY cast(CHAPTERNO as int)";
        dt = Common.Execute_Procedures_Select_ByQuery(sql);
        rpt_Chapters.DataSource = dt;
        rpt_Chapters.DataBind();
    }
    protected void btnCreateQ_Click(object sender, EventArgs e)
    {
        //Common.Set_Procedures("EXECQUERY");
        //Common.Set_ParameterLength(1);
        //Common.Set_Parameters(new MyParameter("@QUERY","[dbo].[CREATE_VIQ_CHILD] " + PVIQId + ",'" + txtFromDate.Text.Trim() + "'"));
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("[dbo].[CREATE_VIQ_CHILD] '" +VesselCode+ "'," + PVIQId + ",'" + txtFromDate.Text.Trim() + "'," + UserId );
        if (dt.Rows.Count > 0)
        {
            lblheader.Text = "Questionnaire created successfully.";
            btnCreateQ.Visible = false; 
            //Response.Redirect("AddSubVIQDetails.aspx?VSL=" + VesselCode + "&PVIQId=" + PVIQId + "&VIQId=" + dsResult.Tables[0].Rows[0].ToString()+"&Message=VIQ Created Successfully.");
        }
    }
    private void ResetNULLDates(ref DataSet ds_IN)
    {
        DateTime dt_ref = new DateTime(1900, 1, 1);
        foreach (DataTable dt in ds_IN.Tables)
        {
            List<String> DateTimeCols = new List<String>();
            foreach (DataColumn dc in dt.Columns)
            {
                if (dc.DataType == System.Type.GetType("System.DateTime"))
                {
                    DateTimeCols.Add(dc.ColumnName);
                }
            }
            if (DateTimeCols.Count > 0 && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    foreach (string cName in DateTimeCols)
                    {
                        if (!Convert.IsDBNull(dr[cName]))
                        {
                            DateTime dt_test = Convert.ToDateTime(dr[cName]);
                            if (dt_test <= dt_ref)
                            {
                                dr[cName] = DBNull.Value;
                            }
                        }
                    }
                }
            }
        }
    }
    protected void btnLock_Click(object sender, EventArgs e)
    {
        string sql = "UPDATE [dbo].VIQ_VIQMaster SET VIQStatus=1 WHERE VESSELCODE='" + VesselCode + "' AND VIQId=" + VIQId.ToString();
        Common.Execute_Procedures_Select_ByQuery(sql);
        ProjectCommon.ShowMessage("VIQ Locked successfully.");
    }
}