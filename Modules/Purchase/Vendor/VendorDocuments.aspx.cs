using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class VendorDocuments : System.Web.UI.Page
{
    public int PK
    {
        set{ViewState["_PK"] = value;}
        get{return Common.CastAsInt32(ViewState["_PK"]);}
    }
    public int VRID
    {
        set { ViewState["_VRID"] = value; }
        get { return Common.CastAsInt32(ViewState["_VRID"]); }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        VRID = Common.CastAsInt32( Page.Request.QueryString["KeyId"].ToString());
        if(!Page.IsPostBack)
        {
            BindData();
        }
    }
    protected void btnSave_OnClick(object sender,EventArgs e)
    {
        if (txtDescription.Text.Trim() == "")
        {
            lblMsg.Text = "Please enter description.";
            txtDescription.Focus();
            return;
        }
        if (!fpAttachment.HasFile)
        {
            lblMsg.Text = "Please upload any file.";
            fpAttachment.Focus();
            return;
        }
        string FileName = "";
        byte[] File;

        FileName = fpAttachment.FileName;
        File = fpAttachment.FileBytes;

        Common.Set_Procedures("sp_VenderRequest_Attachments");
        Common.Set_ParameterLength(6);
        Common.Set_Parameters
            (
                new MyParameter("@id", PK),
                new MyParameter("@VRID", VRID),
                new MyParameter("@Attachmentname", FileName),
                new MyParameter("@AttachmentDescription", txtDescription.Text.Trim()),
                new MyParameter("@Attachment", File),                
                new MyParameter("@UpdatedBy", Session["UserName"].ToString())
            );
        DataSet ResDS = new DataSet();
        Boolean Res = false;
        Res = Common.Execute_Procedures_IUD(ResDS);
        if(Res)
        {
            lblMsg.Text = "File uploaded successfully.";
            txtDescription.Text = "";
            BindData();
        }
        else
        {
            lblMsg.Text = "Error while uploading file.";
        }
    }
    
    protected void lnkFileName_OnClick(object sender, EventArgs e)
    {
        int id=0;
        LinkButton btn = (LinkButton)sender;
        id = Common.CastAsInt32(btn.CommandArgument);
        DownloadFile(id);
    }
    protected void btnDeleteFile_OnClick(object sender, EventArgs e)
    {
        int id = 0;
        ImageButton btn = (ImageButton)sender;
        id = Common.CastAsInt32(btn.CommandArgument);

        string SQL = " delete from tbl_VenderRequest_Attachments where id=" + id;
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
        BindData();
    }
    
    // Funtion -------------------------------------------------------------------------------------
    public void BindData()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(" select * from tbl_VenderRequest_Attachments where VRID="+VRID+" order by id desc");
        rptAttachments.DataSource = dt;
        rptAttachments.DataBind();
    }
    protected void DownloadFile(int id)
    {
        string SQL = " select Attachmentname,Attachment from tbl_VenderRequest_Attachments where id=" + id;

        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);

        if (dt.Rows.Count > 0)
        {
            string FileName = dt.Rows[0]["Attachmentname"].ToString();

            if (FileName.Trim() != "")
            {
                byte[] buff = (byte[])dt.Rows[0]["Attachment"];
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileName);
                Response.BinaryWrite(buff);
                Response.Flush();
                Response.End();
            }
        }
    }
}