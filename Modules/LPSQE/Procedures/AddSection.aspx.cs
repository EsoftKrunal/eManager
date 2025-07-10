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
using System.IO;

public partial class AddSection : System.Web.UI.Page
{
    int ManualId
    {
        get
        {
            return Common.CastAsInt32(ViewState["ManualId"]);
        }
        set { ViewState["ManualId"] = value; }
    }
    string SectionId
    {
        get {
            return Convert.ToString(ViewState["SectionId"]); 
        }
        set { ViewState["SectionId"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        lblMessage.Text = "";
        if (!IsPostBack)
        {
            ManualId = Common.CastAsInt32(Request.QueryString["ManualId"]);
            SectionId = Convert.ToString(Request.QueryString["SectionId"]);
            LoadDDl();
            if (SectionId == "")
            {
                ddlParent.SelectedValue = "" + Request.QueryString["LastSectionId"];
                txtSearchTags.Enabled = true;
                btnEditST.Visible = false;
            }
            else
            {
                ddlParent.SelectedValue = SectionId;
                txtSearchTags.Enabled = false;
                btnEditST.Visible = true;
            }
            ShowSection(ManualId, SectionId);
            LoadAvailableForms();
            LoadAvailableRanks();
            LoadLinkedForm();
            LoadLinkedRanks();
        }
    }
    protected void ShowSection(int ManualId, string SectionId)
    {
        if (SectionId.Trim() != "" && SectionId.Trim() != "0")
        {
            Section s = new Section(ManualId, SectionId);
            ddlParent.SelectedValue = s.ParentSectionId;
            ddlParent.Enabled = false;
            txtHeading.Text = s.Heading;
            txtSearchTags.Text = s.SearchTags;
            hfSearchTag.Value = s.SearchTags;
            txtRevision.Text = s.Version;
            //frmImages.Attributes.Add("src", "ViewManualImages.aspx?ManualId=" + ManualId.ToString() + "&SectionId=" + SectionId);
        }
        else
        {
            txtRevision.Text = "0.0";
            //dvImages.Visible = false;
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string MaxSectionId = "";        
        //if (flpContent.PostedFile.ContentLength > 500 * 1024)
        //{
        //    lblMessage.Text = "File size should be less than 500 KB."; return;
        //}        

        if (SectionId.Trim() == "" || SectionId.Trim() == "0")
        {
            MaxSectionId = Manual.getNextSectionId(ManualId, ddlParent.SelectedValue);
        }
        else
        {
            MaxSectionId = SectionId.Trim();
        }
        
        DataTable dt = new DataTable();
        string SearchTags = txtSearchTags.Text;
        string Ret = Manual.SaveManualSection(ManualId, MaxSectionId, ddlParent.SelectedValue, txtHeading.Text, SearchTags, Path.GetExtension(flpContent.FileName), flpContent.FileBytes, Session["UserName"].ToString(), txtRevision.Text.Trim());
        lblMessage.Text = "Inserted Successfully.";
        if (SectionId.Trim() == "" || SectionId.Trim() == "0")
        {
            string Lastval = ddlParent.SelectedValue;
            LoadDDl();
            ddlParent.SelectedValue=Lastval;
            SectionId = MaxSectionId;
        }
    }

    protected void btnAddFormsToHeading(object sender, EventArgs e)
    {
        int FormId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        if (SectionId.Trim() == "")
        {
            ShowMessage1("Please save heading first.", true);
            return;
        }
        if (Forms.AddFormToHeading(ManualId, SectionId, FormId))
        {
            ShowMessage1("Form Added Successfully.", false);
            LoadLinkedForm();
        }
        else
        {
            ShowMessage1("Unable to add Form.", true);
        }
    }
    protected void btnRemoveFormsToHeading(object sender, EventArgs e)
    {
        int FormId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        if (Forms.RemoveFormFromHeading(ManualId, SectionId, FormId))
        {
            ShowMessage1("Form Removed Successfully.", false);
            LoadLinkedForm();
        }
        else
        {
            ShowMessage1("Unable to remove Form.", true);
        }
    }
    
    protected void btnFileDownload_OnClick(object sender, EventArgs e)
    {
        LinkButton BTN = (LinkButton)sender;
        HiddenField hfFileName = (HiddenField)BTN.Parent.FindControl("hfFileName");

        int FormId = Common.CastAsInt32(((LinkButton)sender).CommandArgument);
        string FormName = hfFileName.Value;
        DownloadAttachment(FormId, FormName);
    }
    protected void btnDownLoadFile(object sender, EventArgs e)
    {
        LinkButton BTN = (LinkButton)sender;
        HiddenField hfFileName = (HiddenField)BTN.Parent.FindControl("hfFileName");

        int FormId = Common.CastAsInt32(((LinkButton)sender).CommandArgument);
        string FormName = hfFileName.Value;
        DownloadAttachment(FormId, FormName);
    }
    
    //---------- Search Tages event
    protected void btnEditST_OnClick(object sender, EventArgs e)
    {
        txtSearchTags.Enabled = true;
        btnUpdateST.Visible = true;
        btnCancelST.Visible = true;
        btnEditST.Visible = false;
    }
    protected void btnUpdateST_OnClick(object sender, EventArgs e)
    {

        Boolean Res= Section.UpdateSearchTags(ManualId, SectionId, txtSearchTags.Text);

        hfSearchTag.Value = hfSearchTag.Value;
        txtSearchTags.Enabled = false;
        btnUpdateST.Visible = false;
        btnCancelST.Visible = false;
        btnEditST.Visible = true;
    }
    protected void btnCancelST_OnClick(object sender, EventArgs e)
    {
        txtSearchTags.Text = hfSearchTag.Value;
        txtSearchTags.Enabled = false;
        btnUpdateST.Visible = false;
        btnCancelST.Visible = false;
        btnEditST.Visible = true;
    }


    //----------  Funcation
    public void LoadDDl()
    {
        ManualBO m = new ManualBO(ManualId);
        m.LoadManualHeadings(); 
        ddlParent.DataSource = m.HeadingsList;
        ddlParent.DataTextField = "PadHeading";
        ddlParent.DataValueField = "SectionId";
        ddlParent.DataBind(); 
    }
    public void LoadAvailableForms()
    {

        rptAvailableForms.DataSource = Forms.getAvailableForms();
        rptAvailableForms.DataBind();
    }
    public void LoadLinkedForm()
    {
        rptLinkedForm.DataSource = Forms.getLinkedForms(ManualId,SectionId);
        rptLinkedForm.DataBind();
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
    public void ShowMessage1(string Messgae, bool Error)
    {
        if (Error)
            lblMessage.ForeColor = System.Drawing.Color.Red;
        else
            lblMessage.ForeColor = System.Drawing.Color.Green;

        lblMessage.Text = Messgae;
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("ViewManualSection.aspx?ManualId=" + ManualId.ToString() + "&SectionId=" + Request.QueryString["LastSectionId"] + "&AddSection=" + Request.QueryString["AddSection"]);
    }
    //protected void btnUpload_Click(object sender, EventArgs e)
    //{
    //    if(flp1.HasFile)
    //        if (flp1.FileBytes.Length > 0)
    //        {
    //            string filename=System.IO.Path.GetRandomFileName() + Path.GetExtension(flp1.FileName);
    //            try
    //            {
    //                flp1.SaveAs(Server.MapPath("~/SMS/Attachment/Images/" + filename));
    //                Manual.UploadImage(ManualId, SectionId, filename,txtDescr.Text.Trim());
    //            }
    //            catch 
    //            { }
    //        }
    //}
    protected void btnClearAttachment_Click(object sender, EventArgs e)
    {
        string MaxSectionId = "";         

        if (SectionId.Trim() == "" || SectionId.Trim() == "0")
        {
            MaxSectionId = Manual.getNextSectionId(ManualId, ddlParent.SelectedValue);
        }
        else
        {
            MaxSectionId = SectionId.Trim();
        }

        try
        {
            string SQL = "UPDATE [dbo].[SMS_MANUALDETAILS] SET [FileName]='',FileContent='byte[0]'  WHERE MANUALID=" + ManualId + " AND LTRIM(RTRIM(SECTIONID))=LTRIM(RTRIM('" + MaxSectionId + "')); SELECT -1";
            DataTable dtUpdate = Common.Execute_Procedures_Select_ByQuery(SQL);
            if (dtUpdate != null && dtUpdate.Rows.Count > 0)
            {
                lblMessage.Text = "Attachment Cleared Successfully.";
            }

        }
        catch (Exception ex)
        {
            lblMessage.Text = "Unable to clear attacement. Error : " + ex.Message.ToString();
        }
    }

    //-----------------------
    
    public void btnForms_OnClick(object sender, EventArgs e)
    {
        divForms.Visible = true;
        divRanks.Visible = false;

        btnForms.CssClass = "SelTab";
        btnRanks.CssClass = "Tab";

    }
    public void btnRanks_OnClick(object sender, EventArgs e)
    {
        divForms.Visible = false;
        divRanks.Visible = true;

        btnForms.CssClass = "Tab";
        btnRanks.CssClass = "SelTab";
    }

    //--For Ranks---------------------------------------------------
    public void LoadAvailableRanks()
    {

        rptAvailableRanks.DataSource = Forms.getAvailableRanks();
        rptAvailableRanks.DataBind();
    }
    public void LoadLinkedRanks()
    {
        rptLinkedRanks.DataSource = Forms.getLinkedRanks(ManualId, SectionId);
        rptLinkedRanks.DataBind();
    }

    protected void btnAddRanksToHeading(object sender, EventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        HiddenField hfRankCode=(HiddenField)btn.Parent.FindControl("hfRankCode");
        int RankID = Common.CastAsInt32(btn.CommandArgument);

        if (SectionId.Trim() == "")
        {
            ShowMessage1("Please save heading first.", true);
            return;
        }
        if (Forms.AddRanksToHeading(ManualId, SectionId, RankID, hfRankCode.Value))
        {
            ShowMessage1("Rank Added Successfully.", false);
            LoadLinkedRanks();
        }
        else
        {
            ShowMessage1("Unable to add Rank.", true);
        }
    }
    protected void btnRemoveRanksToHeading(object sender, EventArgs e)
    {
        int RankID = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        if (Forms.RemoveRanksFromHeading(ManualId, SectionId, RankID))
        {
            ShowMessage1("Form Removed Successfully.", false);
            LoadLinkedRanks();
        }
        else
        {
            ShowMessage1("Unable to remove rank.", true);
        }
    }

}
