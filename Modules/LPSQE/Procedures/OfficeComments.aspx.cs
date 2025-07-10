using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Text;
using System.IO;

public partial class SMS_OfficeComments : System.Web.UI.Page
{
    public Section ob_Section;
    public ManualBO mb;    

    public int ManualId
    {
        set { ViewState["ManualId"] = value; }
        get { return Common.CastAsInt32(ViewState["ManualId"]); }
    }
    public string SectionId
    {
        set { ViewState["SectionId"] = value; }
        get { return ViewState["SectionId"].ToString(); }
    }
    public string MVersion
    {
        set { ViewState["MVersion"] = value; }
        get { return ViewState["MVersion"].ToString(); }
    }
    public string SVersion
    {
        set { ViewState["SVersion"] = value; }
        get { return ViewState["SVersion"].ToString(); }
    }   

    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        lblMessage.Text = "";
        if (!IsPostBack)
        {
            ManualId = Common.CastAsInt32("" + Request.QueryString["ManualId"]);
            SectionId = Convert.ToString("" + Request.QueryString["SectionId"]);
            MVersion =  Request.QueryString["ManVersion"].ToString();
            SVersion = Request.QueryString["SecVersion"].ToString();

            ob_Section = new Section(ManualId, SectionId);

            ShowHeader();
            ShowComments();
        }

    }
    public void ShowHeader()
    {
        try
        {
            ManualBO mb = new ManualBO(ob_Section.ManualId);
            lblManualName.Text = mb.ManualName;
            lblMVersion.Text = "[" + mb.VersionNo + "]";
            lblSVersion.Text = "[" + ob_Section.Version + "]";
            if (ob_Section.Status == "A")
            {
                lblHeading.Text = ob_Section.SectionId + " : " + ob_Section.Heading;
                lblContent.Text = "[ " + ob_Section.SearchTags + " ]";
            }
            else
            {
                lblHeading.Text = ob_Section.SectionId + " : " + ob_Section.Heading;
                lblContent.Text = "[ NA ]";
            }

        }
        catch { }
    }
    public void ShowComments()
    {
        string SQL = "SELECT * FROM " +
        "( " +
        "SELECT [CommentId],isnull(OfficeName,'Office') as Location,[MVersion] ,[SVersion] , [CommentText],U.firstname + ' ' + U.lastname as CommentBy,PositionName, [CommentOn],ReqDate FROM [dbo].[SMS_OfficeComments] S " +
        "LEFT JOIN DBO.OFFICE O ON O.OFFICEID=S.OFFICEID " +
        "LEFT JOIN DBO.POSITION P ON P.POSITIONID=S.POSITIONID " +
        "LEFT JOIN DBO.USERLOGIN U ON U.LOGINID=S.COMMENTBY " +
        "WHERE  [ManualId] = " + ManualId + " AND [SectionId] = '" + SectionId.Trim() + "' AND [MVersion] = '" + MVersion + "' AND [SVersion] = '" + SVersion + "' " +
        "UNION " +
        "SELECT  [CommentId],[VesselCode] AS [Location],[MVersion] ,[SVersion] , [CommentText],[CommentBy],ComentByRank,[CommentOn],ReqDate FROM [dbo].[SMS_ShipComments] " +
        "WHERE  [ManualId] = " + ManualId + " AND [SectionId] = '" + SectionId.Trim() + "' AND [MVersion] = '" + MVersion + "' AND [SVersion] = '" + SVersion + "' " +
        "UNION " +
        "SELECT [CommentId],'OFFICE' AS [Location], [ManualVersion] AS [MVersion],[SVersion],[Comments] AS [CommentText],[EnteredBy] AS [CommentBy],'' as Position, [EnteredOn] AS [CommentOn],NULL as ReqDate FROM [dbo].[SMS_ManualDetails_Comments] " +
        "WHERE  [ManualId] = " + ManualId + " AND [SectionId] = '" + SectionId.Trim() + "' AND [ManualVersion] = '" + MVersion + "' AND [SVersion] = '" + SVersion + "' " +
        ") " +
        "a ORDER BY [CommentOn] DESC ";


        //string SQL = "SELECT [Location],[CommentId],[MVersion],[SVersion],[CommentText],[CommentBy],REPLACE(CONVERT(VARCHAR(12),[CommentOn],106),' ','-') AS [CommentOn] FROM ( " +
        //             "SELECT 'Office' AS [Location], [CommentId],[MVersion] ,[SVersion] , [CommentText],[CommentBy], [CommentOn] FROM [dbo].[SMS_OfficeComments] " +
        //             "WHERE  [ManualId] = " + ManualId + " AND [SectionId] = '" + SectionId.Trim() + "' AND [MVersion] = '" + MVersion + "' AND [SVersion] = '" + SVersion + "' " +
        //             "UNION " +
        //             "SELECT [VesselCode] AS [Location], [CommentId],[MVersion] ,[SVersion] , [CommentText],[CommentBy], [CommentOn] FROM [dbo].[SMS_ShipComments] " +
        //             "WHERE  [ManualId] = " + ManualId + " AND [SectionId] = '" + SectionId.Trim() + "' AND [MVersion] = '" + MVersion + "' AND [SVersion] = '" + SVersion + "' " +
        //             "UNION " +
        //             "SELECT 'Office' AS [Location], [CommentId],[ManualVersion] AS [MVersion],[SVersion],[Comments] AS [CommentText],[EnteredBy] AS [CommentBy], [EnteredOn] AS [CommentOn] FROM [dbo].[SMS_ManualDetails_Comments] " +
        //             "WHERE  [ManualId] = " + ManualId + " AND [SectionId] = '" + SectionId.Trim() + "' AND [ManualVersion] = '" + MVersion + "' AND [SVersion] = '" + SVersion + "' " +
        //             " ) a ORDER BY [CommentOn] DESC ";
                    
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);

        if (dt != null && dt.Rows.Count > 0)
        {
            rptComments.DataSource = dt;
            rptComments.DataBind();
        }
        else
        {
            rptComments.DataSource = null;
            rptComments.DataBind();
        }

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (txtComment.Text.Trim() == "")
        {
            lblMessage.Text = "Please enter comment.";
            txtComment.Focus();
            return;
        }
        
        if (!rdoCRYes.Checked && !rdoCRNo.Checked)
        {
            lblMessage.Text = "Please select change requested.";
            rdoCRYes.Focus();
            return;
        }

        try
        {
            Common.Set_Procedures("[dbo].[SMS_INSERT_OfficeComments]");
            Common.Set_ParameterLength(5);
            Common.Set_Parameters(               
               new MyParameter("@ManualId", ManualId),
               new MyParameter("@SectionId", SectionId),
               new MyParameter("@CommentText", txtComment.Text.Trim()),
               new MyParameter("@CommentBy", Common.CastAsInt32(Session["loginid"].ToString())),
               new MyParameter("@ChangeRequested", rdoCRYes.Checked ? "Y" : "N")
               );
            DataSet ds = new DataSet();
            ds.Clear();
            Boolean res;
            res = Common.Execute_Procedures_IUD(ds);
            if (res)
            {
                lblMessage.Text = "Comment saved successfully.";
                ShowComments();
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = "Unable to add data.Error :" + ex.Message.ToString();
        }

    }
    protected void lnlComments_Click(object sender, EventArgs e)
    {
        lnlComments.Visible = false;
        dvComments.Visible = true;
        dvscroll_Supply.Style.Add("height", "290px");
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        lnlComments.Visible = true;
        dvComments.Visible = false;
        dvscroll_Supply.Style.Add("height", "500px");
    }
}