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

public partial class ReadViewRanks : System.Web.UI.Page
{
    public ReadSection ob_Section;
    public string RankMode    
    {
        get { return ViewState["_RankMode"].ToString(); }
        set { ViewState["_RankMode"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        int ManualId = Common.CastAsInt32("" + Request.QueryString["ManualId"]);
        string SectionId = Convert.ToString("" + Request.QueryString["SectionId"]);
        int HistoryID = Common.CastAsInt32(Request.QueryString["HistoryID"]);
        if (HistoryID > 0)
        {
            RankMode = "H";
            LoadLinkedForm_History(HistoryID);
        }
        else
        {
            RankMode = "N";
            ob_Section = new ReadSection(ManualId, SectionId);
            LoadLinkedForm();
        }
    }
    public void LoadLinkedForm()
    {
        rptLinkedRanks.DataSource = Forms.Read_getLinkedRanks(ob_Section.ManualId, ob_Section.SectionId);
        rptLinkedRanks.DataBind();
    }
    public void LoadLinkedForm_History(int HistoryID )
    {
        rptLinkedRanks.DataSource = Forms.Read_getLinkedForms_History(HistoryID);
        rptLinkedRanks.DataBind();
    }
    protected void btnDownLoadFile(object sender, EventArgs e)
    {
        LinkButton BTN = (LinkButton)sender;
        HiddenField hfFileName = (HiddenField)BTN.Parent.FindControl("hfFileName");

        int FormId = Common.CastAsInt32(((LinkButton)sender).CommandArgument);
        string FormName = hfFileName.Value;
        DownloadAttachment(FormId, FormName);
    }

    public void DownloadAttachment(int FormId, string FileName)
    {
        if (FileName.Trim() != "")
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", String.Format("attachment;filename={0}", FileName));
            Response.ContentType = "application/" + Path.GetExtension(FileName).Substring(1);
            if (RankMode == "H")
            {
                Response.BinaryWrite(Forms.getFormAttachment_For_History(FormId));
            }
            else
            {
                Response.BinaryWrite(Forms.getFormAttachment(FormId));
            }
        }
    }
   
}
