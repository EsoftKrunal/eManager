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
using System.Activities.Expressions;

public partial class ReadForms : System.Web.UI.Page
{
    AuthenticationManager Auth;
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

        if(!IsPostBack)
        {
            BindddlDepartment();
            BindddlFormsCategory();
            LoadForms();
            //SearchChar = "";
        }
    }

    protected void LoadForms()
    {
        rptForms.DataSource = Forms.getFormsList1(Convert.ToInt32(ddlFormsDepartment.SelectedValue), Convert.ToInt32(ddlFormsCategory.SelectedValue));
        rptForms.DataBind();
        
    }
    public void DownloadAttachment(int FormId, string FileName, string FormNo)
    {
        try
        {
            string sql = "Select ContentType, LatestFileContent, FileContent from SMS_Forms with(nolock) where FormId = " + FormId;
            DataTable DT = Common.Execute_Procedures_Select_ByQuery(sql);
            if (DT.Rows.Count > 0)
            {
                string contentType = "";
                if (!string.IsNullOrWhiteSpace(DT.Rows[0]["ContentType"].ToString()))
                {
                    contentType = DT.Rows[0]["ContentType"].ToString();
                }
                if (!string.IsNullOrWhiteSpace(contentType))
                {

                    byte[] latestFileContent = (byte[])DT.Rows[0]["LatestFileContent"];
                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.ContentType = contentType;
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileName);
                    Response.BinaryWrite(latestFileContent);
                    Response.Flush();
                    Response.End();
                }
                else
                {
                    string sDocName = "";
                    sDocName = FileName;
                    byte[] DocFile = (byte[])DT.Rows[0]["FileContent"];
                    Response.Clear();
                    Response.ClearContent();
                    Response.ClearHeaders();

                    if (sDocName.EndsWith(".txt"))
                    {
                        Response.ContentType = "text/plain";
                    }
                    if (sDocName.EndsWith(".xls") || sDocName.EndsWith(".XLS"))
                    {
                        Response.ContentType = "application/vnd.ms-excel";
                    }
                    if (sDocName.EndsWith(".xlsx") || sDocName.EndsWith(".XLSX"))
                    {
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    }
                    if (sDocName.EndsWith(".doc") || sDocName.EndsWith(".DOC"))
                    {
                        Response.ContentType = "application/vnd.ms-word";
                    }
                    if (sDocName.EndsWith(".docx") || sDocName.EndsWith(".DOCX"))
                    {
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                    }
                    if (sDocName.EndsWith(".pfd"))
                    {
                        Response.ContentType = "application/pdf";
                    }
                    if (sDocName.EndsWith(".zip"))
                    {
                        Response.ContentType = "application/x-zip-compressed";
                    }
                    if (sDocName.EndsWith(".gif"))
                    {
                        Response.ContentType = "image/gif";
                    }
                    if (sDocName.EndsWith(".jpeg"))
                    {
                        Response.ContentType = "image/jpeg";
                    }
                    if (sDocName.EndsWith(".png"))
                    {
                        Response.ContentType = "image/png";
                    }
                    //if (sDocName.EndsWith(".png"))
                    //{
                    //    Response.ContentType = "text/xml";
                    //}


                    Response.AddHeader("Content-Disposition", "attachment; filename=" + sDocName);
                    //Response.AddHeader("Content-Length", sDocName.Length.ToString());
                    Response.OutputStream.Write(DocFile, 0, DocFile.Length - 1);
                    // Response.Flush();
                    Response.End();
                }
            }



            // string extension = Path.GetExtension(FileName).Substring(1);
            // Response.Clear();
            // Response.Buffer = true;

            //// Response.AddHeader("content-disposition", String.Format("attachment;filename={0}", FormNo + "_" + FileName));

            // Response.ContentType = "application/" + extension;
            // Response.AddHeader("content-disposition", "attachment;filename=" + FileName);
            // Response.BinaryWrite(Forms.getFormAttachment(FormId));
            // Response.Flush();
            // Response.End();




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
    //-------------
    protected void lnlViewVersion_OnClick(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        //dvFormVersion.Visible = true;
        //DataTable dt = Forms.getFormsList(lnk.CommandArgument);
        //rptFormsVersion.DataSource = dt;



        int FormId = Common.CastAsInt32(((LinkButton)sender).CommandArgument);
        string FormName = ((LinkButton)sender).ToolTip;
        string FormNo = "";
        string sql = "Select FormNo from SMS_Forms with(nolock) where FormId =  " + FormId;
        DataTable DT = Common.Execute_Procedures_Select_ByQuery(sql);
        if (DT.Rows.Count > 0)
        {
            FormNo = DT.Rows[0]["FormNo"].ToString();
        }
        DownloadAttachment(FormId, FormName, FormNo);


        rptFormsVersion.DataBind();
    }
    protected void btnCloseFormVersion_OnClick(object sender, EventArgs e)
    {
        dvFormVersion.Visible = false;
    }

    protected void lnkReleaseNewVersion_OnClick(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        DataTable dt = Forms.getFormsList(lnk.CommandArgument);
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
