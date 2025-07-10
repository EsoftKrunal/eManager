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

public partial class AddForms : System.Web.UI.Page
{
    public AuthenticationManager Auth;
    public string FormNo
    {
        get
        {
            return Convert.ToString(ViewState["_FormNo"]);
        }
        set
        {
            ViewState["_FormNo"] = value;
        }
    }
    //public string SearchChar
    //{
    //    get
    //    {
    //        return Convert.ToString(ViewState["_SearchChar"]);
    //    }
    //    set
    //    {
    //        ViewState["_SearchChar"] = value;
    //    }
    //}
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        int UserId = 0;
        UserId = int.Parse(Session["loginid"].ToString());

        Auth = new AuthenticationManager(1057, UserId, ObjectType.Page);
        if (!(Auth.IsView))
        {
            Response.Redirect("NotAuthorized.aspx");
        }

        lblMessage1.Text = "";
        if(!IsPostBack)
        {
            BindddlDepartment();
            BindddlFormsCategory();
            LoadForms();
           // SearchChar = "";
            btnAddNewForm.Visible = Auth.IsAdd;
        }
    }

    protected void LoadForms()
    {
        DataTable dt = Forms.getFormsList1(Convert.ToInt32(ddlFormsDepartment.SelectedValue), Convert.ToInt32(ddlFormsCategory.SelectedValue));
        if (dt.Rows.Count > 0)
        {
            rptForms.DataSource = dt;
            rptForms.DataBind();
        }
        else
        {
            rptForms.DataSource = null;
            rptForms.DataBind();
        }

    }
    public void ShowMessage1(string Messgae, bool Error)
    {
        if (Error)
            lblMessage1.ForeColor = System.Drawing.Color.Red;
        else
            lblMessage1.ForeColor = System.Drawing.Color.Green;

        lblMessage1.Text = Messgae;
    }
    
    protected void btnSubmit1_Click(object sender, EventArgs e)
    {
        try
        {
            if (flpFileUpload1.PostedFile.ContentLength > (1024 * 1024 * 3))
            {
                ShowMessage1("File size should be less than 3 MB.", true); return;
            }

            if (ddlDepartment.SelectedIndex == 0)
            {
                ShowMessage1("Please select Forms Department.", true);
            }

            if (ddlFormsCategory.SelectedIndex == 0)
            {
                ShowMessage1("Please select Forms Category.", true);
            }
            string fileName = Path.GetFileName(flpFileUpload1.PostedFile.FileName);
            string fileContent = flpFileUpload1.PostedFile.ContentType;
            Stream fs = flpFileUpload1.PostedFile.InputStream;
            BinaryReader br = new BinaryReader(fs);
            byte[] bytes = br.ReadBytes((Int32)fs.Length);
            if (Forms.AddNewForm(txtFromName1.Text, txtFromNo1.Text, txtVersion1.Text, fileName, bytes, txtRDte.Text, Convert.ToInt32(ddlDepartment.SelectedValue), Convert.ToInt32(ddlCategory.SelectedValue), fileContent))
            {
                ShowMessage1("Form Uploaded Successfully.", false);
                //string sel = lstForms.SelectedValue;
                //lstForms_SelectedIndexChanged(sender, e);
                //LoadForms();
                //if(sel.Trim()!="") 
                //    lstForms.SelectedValue = sel;
                LoadForms();
            }
            else
            {
                ShowMessage1("Unable to Uploaded Form.", true);
            }
        }
        catch (Exception ex)
        {
            ShowMessage1("Unable to Uploaded Form." + ex.Message, true);
        }
    }
    public void DownloadAttachment(int FormId,string FileName, string FormNo)
    {
        try
        {
            string extension = Path.GetExtension(FileName).Substring(1);
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", String.Format("attachment;filename={0}", FormNo+"_"+FileName));
            Response.ContentType = "application/" + extension;
            Response.BinaryWrite(Forms.getFormAttachment(FormId));
        }
        catch { }
    }
    protected void btnDownloadClick(object sender, EventArgs e)
    {
        int FormId =Common.CastAsInt32(((LinkButton)sender).CommandArgument);
        string FormName =((LinkButton)sender).ToolTip;
        string FormNo = "";
        string sql = "Select FormNo from SMS_Forms with(nolock) where FormId =  " + FormId;
        DataTable DT = Common.Execute_Procedures_Select_ByQuery(sql);
        if (DT.Rows.Count > 0)
        {
            FormNo = DT.Rows[0]["FormNo"].ToString();
        }
        DownloadAttachment(FormId, FormName, FormNo);
    }
    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        txtFromNo1.Text = "";
        txtVersion1.Text = "";
        txtFromName1.Text = "";
        txtRDte.Text = "";
        txtFromName1.Enabled = true;
        txtFromNo1.Enabled = true;
        dvAddEditForms.Visible = true;
    }
    
    //-------------
    protected void lnlViewVersion_OnClick(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        btnReleaseNewVersion.CommandArgument = lnk.CommandArgument;

        dvFormVersion.Visible = true;
        DataTable dt = Forms.getFormsList(lnk.CommandArgument);
        rptFormsVersion.DataSource = dt;
        
        rptFormsVersion.DataBind();

        btnReleaseNewVersion.Visible = Auth.IsAdd && ( lnk.CssClass.Trim()!="INACTIVE" );
        btnInActive.Visible = btnReleaseNewVersion.Visible;
    }
    protected void btnCloseFormVersion_OnClick(object sender, EventArgs e)
    {
        dvFormVersion.Visible = false;
    }

    protected void lnkReleaseNewVersion_OnClick(object sender, EventArgs e)
    {
        
        //LinkButton lnk = (LinkButton)sender;
        //DataTable dt = Forms.getFormsList(lnk.CommandArgument);
        DataTable dt = Forms.getFormsList(btnReleaseNewVersion.CommandArgument);

        txtFromNo1.Text = btnReleaseNewVersion.CommandArgument;
        txtFromNo1.Enabled = false;
        if (dt.Rows.Count > 0)
        {
            txtFromName1.Text = dt.Rows[0]["FormName"].ToString();
        }
        txtFromName1.Enabled = false;

        dvFormVersion.Visible = false;
        dvAddEditForms.Visible = true;
        
        txtVersion1.Text = "";
    }
    protected void btnInActive_OnClick(object sender, EventArgs e)
    {
        DataTable dt = Forms.getFormsList(btnReleaseNewVersion.CommandArgument);
        txtFromNo1.Text = btnReleaseNewVersion.CommandArgument;
        txtFromNo1.Enabled = false;
        if (dt.Rows.Count > 0)
        {
            byte[] fl= new byte[0];
            if (Forms.AddNewForm(dt.Rows[0]["FormName"].ToString(), btnReleaseNewVersion.CommandArgument.Trim(), "INACTIVE", "", fl, DateTime.Today.ToString("dd-MMM-yyyy"), Convert.ToInt32(ddlDepartment.SelectedValue), Convert.ToInt32(ddlCategory.SelectedValue),""))
            {
                LoadForms();
                dvFormVersion.Visible = false;
            }
        }
    }
    
    protected void btnCloseAddEditForm_Click(object sender, EventArgs e)
    {
        dvAddEditForms.Visible = false;        
    }

    //protected void SearchForms(object sender, EventArgs e)
    //{
    //    Button btn = (Button)sender;
    //    SearchChar = btn.CommandArgument;
    //    LoadForms();
    //    SetSearchBtnCSS(btn);
    //}
    //public void SetSearchBtnCSS(Button btn)
    //{
    //    LinkButton1.CssClass = "sbtn";
    //    LinkButton2.CssClass = "sbtn";
    //    LinkButton3.CssClass = "sbtn";
    //    LinkButton4.CssClass = "sbtn";
    //    LinkButton5.CssClass = "sbtn";
    //    LinkButton6.CssClass = "sbtn";
    //    LinkButton7.CssClass = "sbtn";
    //    LinkButton8.CssClass = "sbtn";
    //    LinkButton9.CssClass = "sbtn";
    //    LinkButton10.CssClass = "sbtn";
    //    LinkButton11.CssClass = "sbtn";
    //    LinkButton12.CssClass = "sbtn";
    //    LinkButton13.CssClass = "sbtn";
    //    LinkButton14.CssClass = "sbtn";
    //    LinkButton15.CssClass = "sbtn";
    //    LinkButton16.CssClass = "sbtn";
    //    LinkButton17.CssClass = "sbtn";
    //    LinkButton18.CssClass = "sbtn";
    //    LinkButton19.CssClass = "sbtn";
    //    LinkButton20.CssClass = "sbtn";
    //    LinkButton21.CssClass = "sbtn";
    //    LinkButton22.CssClass = "sbtn";
    //    LinkButton23.CssClass = "sbtn"; 
    //    LinkButton24.CssClass = "sbtn";
    //    LinkButton25.CssClass = "sbtn";
    //    LinkButton26.CssClass = "sbtn";
    //    LinkButtonAll.CssClass = "sbtn";

    //    btn.CssClass = "sel_sbtn";
    //}
    protected void BindddlDepartment()
    {
        DataTable dt = Forms.GetFormsDepartmentList();
        if (dt.Rows.Count > 0)
        {
            ddlDepartment.DataSource = dt;
            ddlDepartment.DataTextField = "DepartmentName";
            ddlDepartment.DataValueField = "DepartmentId";
            ddlDepartment.DataBind();
            ddlDepartment.Items.Insert(0, new ListItem("< Select >", ""));

            ddlFormsDepartment.DataSource = dt;
            ddlFormsDepartment.DataTextField = "DepartmentName";
            ddlFormsDepartment.DataValueField = "DepartmentId";
            ddlFormsDepartment.DataBind();
            ddlFormsDepartment.Items.Insert(0, new ListItem("< All >", "0"));
        }
    }

    protected void BindddlFormsCategory()
    {
        DataTable dt = Forms.GetFormsCategoryList();
        if (dt.Rows.Count > 0)
        {
            ddlCategory.DataSource = dt;
            ddlCategory.DataTextField = "FormsCatName";
            ddlCategory.DataValueField = "FormsCatId";
            ddlCategory.DataBind();
            ddlCategory.Items.Insert(0, new ListItem("< Select >", ""));

            ddlFormsCategory.DataSource = dt;
            ddlFormsCategory.DataTextField = "FormsCatName";
            ddlFormsCategory.DataValueField = "FormsCatId";
            ddlFormsCategory.DataBind();
            ddlFormsCategory.Items.Insert(0, new ListItem("< All >", "0"));
        }
    }


    protected void ddlFormsCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadForms();
    }

    protected void ddlFormsDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadForms();
    }
}
