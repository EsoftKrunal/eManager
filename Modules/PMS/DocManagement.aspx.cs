using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;

public partial class DocManagement : System.Web.UI.Page
{
    public int CompJobId
    {
        set { ViewState["CompJobId"] = value; }
        get { return Common.CastAsInt32(ViewState["CompJobId"]); }

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        // -------------------------- SESSION CHECK ----------------------------------------------
        ProjectCommon.SessionCheck();
        // -------------------------- SESSION CHECK END ----------------------------------------------
        lblMSG.Text = "";
        if (Page.Request.QueryString["CJID"] != null)
            CompJobId = Common.CastAsInt32(Page.Request.QueryString["CJID"]);
        if (!Page.IsPostBack)
        {
            BindFormList();
            BindRepeater();
        }
    }

    private void BindFormList(string formNo = "")
    {
        string sql = "";
        if (!String.IsNullOrWhiteSpace(formNo))
        {
            sql = "Select FormId, FormNo + ' - ' + FormName + ' ('+VersionNo+')' As FormName from SMS_FORMS with(nolock) where FormNo Like '" + formNo  + "%' order by FormNo ";
        }
        else
        {
            sql = "Select FormId, FormNo + ' - ' + FormName + ' ('+VersionNo+')' As FormName from SMS_FORMS with(nolock) order by FormNo ";
        }
        
        DataTable dtChecklistname = Common.Execute_Procedures_Select_ByQueryCMS(sql);

        chkFormList.DataSource = dtChecklistname;
        chkFormList.DataTextField = "FormName";
        chkFormList.DataValueField = "FormId";
        chkFormList.DataBind();
    }

    protected void btnAddFiles_OnClick(object sender, EventArgs e)
    {
        String FileType = "";
        String FileName = "";
        int FileID =0;


        string selFormIDs = "";
        foreach (ListItem item in chkFormList.Items)
        {
            if (item.Selected)
            {
                selFormIDs = selFormIDs + "," + item.Value;
            }
        }
        //if (txtDescription.Text.Trim() == "")
        //{
        //    lblMSG.Text = "Please enter description.";
        //    txtDescription.Focus(); return;
        //}
        //if (!fupFile.HasFile)
        //{
        //    lblMSG.Text = "Please select file.";
        //    fupFile.Focus(); return;
        //}
        //else
        //{
        //    FileType = Path.GetExtension(fupFile.FileName);
        //    FileType = FileType.Substring(1);
        //}


        if (! string.IsNullOrWhiteSpace(selFormIDs))
        {
            Common.Set_Procedures("sp_InsertUpdateComponentsJobMapping_Attachments");
            Common.Set_ParameterLength(2);
            Common.Set_Parameters(
                    new MyParameter("@CompJobId", CompJobId),
                    new MyParameter("@FormId", selFormIDs)
                //new MyParameter("@Descr", txtDescription.Text.Replace("'", "~")),
                //new MyParameter("@DocumentType", FileType)
                );
            DataSet ds = new DataSet();
            Boolean Res;
            Res = Common.Execute_Procedures_IUD(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                BindRepeater();
            }
        }


        //    FileID = Common.CastAsInt32(ds.Tables[0].Rows[0][0]);
        //if (fupFile.HasFile)
        //{
        //    FileName = "OfficeDoc_" + CompJobId.ToString() + "_" + FileID + "." + FileType;
        //    fupFile.SaveAs(Server.MapPath("UploadFiles/AttachmentForm/" + FileName));
        //}
        chkFormList.ClearSelection();

        //txtDescription.Text = "";
    }
    protected void imgDel_OnClick(object sender, EventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        int Id = Common.CastAsInt32(btn.CommandArgument);
        HiddenField hfcOMPjOBid = (HiddenField)btn.FindControl("hfcOMPjOBid");

        //string sql = "delete from ComponentsJobMapping_attachments where TableID=" + Id .ToString()+ "";
        //DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);

        Common.Set_Procedures("sp_DeleteComponentsJobMapping_Attachments");
        Common.Set_ParameterLength(2);
        Common.Set_Parameters(
                new MyParameter("@TableID", Id),
                new MyParameter("@CompJobId", hfcOMPjOBid.Value)
            );
        DataSet ds = new DataSet();
        Common.Execute_Procedures_IUD(ds);
        BindRepeater();
    }
    
    public void BindRepeater()
    {
        string sql = "select row_number() over(order by TableId) as Sno,*, FileName As UpFileName from ComponentsJobMapping_attachments cj with(nolock) inner join SMS_FORMS sf with(nolock) on cj.FormId = sf.FormId where cj.CompJobID=" + CompJobId.ToString() + " and Status='A' order by TableId";
        DataTable Dt = Common.Execute_Procedures_Select_ByQuery(sql);
        rptFiles.DataSource = Dt;
        rptFiles.DataBind();
    }

    protected void lnlViewVersion_OnClick(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        //dvFormVersion.Visible = true;
        //DataTable dt = Forms.getFormsList(lnk.CommandArgument);
        //rptFormsVersion.DataSource = dt;



        int FormId = Common.CastAsInt32(((LinkButton)sender).CommandArgument);
        string FormName = ((LinkButton)sender).ToolTip;
       
        DownloadAttachment(FormId, FormName);


        rptFiles.DataBind();
    }
    public void DownloadAttachment(int FormId, string FileName)
    {
        try
        {
            string extension = Path.GetExtension(FileName).Substring(1);
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", String.Format("attachment;filename={0}", FileName));
            Response.ContentType = "application/" + extension;
            Response.BinaryWrite(Forms.getFormAttachment(FormId));
            Response.End();
        }
        catch { }
    }
    protected void txtFormNo_TextChanged(object sender, EventArgs e)
    {
        int length = txtFormNo.Text.Length;
        if (length > 0)
        {
            chkFormList.ClearSelection();
            BindFormList(txtFormNo.Text);
        }
    }

}

