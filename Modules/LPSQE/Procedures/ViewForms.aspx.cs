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

public partial class ViewForms : System.Web.UI.Page
{
    public Section ob_Section;
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        int ManualId = Common.CastAsInt32("" + Request.QueryString["ManualId"]);
        string SectionId = Convert.ToString("" + Request.QueryString["SectionId"]);
        ob_Section = new Section(ManualId, SectionId);
        LoadLinkedForm();
    }
    public void LoadLinkedForm()
    {
        rptLinkedForm.DataSource = Forms.getLinkedForms(ob_Section.ManualId, ob_Section.SectionId);
        rptLinkedForm.DataBind();
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
            Response.BinaryWrite(Forms.getFormAttachment(FormId));
        }
    }
   
}
