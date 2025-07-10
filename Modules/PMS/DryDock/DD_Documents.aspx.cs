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
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Text; 

public partial class Docket_DD_Documents : System.Web.UI.Page
{
    public int DocketId
    {
        get { return Common.CastAsInt32(ViewState["DocketId"]); }
        set { ViewState["DocketId"] = value; }
    }
    public int DocumentId
    {
        get { return Common.CastAsInt32(ViewState["DocumentId"]); }
        set { ViewState["DocumentId"] = value; }
    }
   
    //-------------------------
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsgMain.Text = "";
        if (!IsPostBack)
        {
            DocketId = Common.CastAsInt32(Request.QueryString["DocketId"]);
            ShowDocketSummary();
            BindDocuments();
        }
    }
    public void ShowDocketSummary()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT *,(SELECT VESSELNAME FROM DBO.VESSEL V WHERE V.VESSELID=D.VESSELID) AS VESSELNAME,ISNULL((SELECT R.RFQID FROM DD_Docket_RFQ_Master R WHERE R.DOCKETID=D.DOCKETID AND R.STATUS='P'),0) AS PORFQId FROM DD_DocketMaster D WHERE DOCKETID=" + DocketId.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            lblDocketNo.Text = dt.Rows[0]["DocketNo"].ToString();
            lblVessel.Text = dt.Rows[0]["VESSELNAME"].ToString();
            lblType.Text = dt.Rows[0]["DocketType"].ToString();
        }
    }
        
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        DocumentId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT DocumentName FROM DD_DocketDocuments WHERE DocumentId=" + DocumentId.ToString());
        if (dt.Rows.Count>0)
        {
            dv_A_SubJobs.Visible = true;
            txt_DocName.Text = dt.Rows[0]["DocumentName"].ToString();
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        DocumentId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("DELETE FROM DD_DocketDocuments WHERE DocumentId=" + DocumentId.ToString());
        BindDocuments();
    }
    protected void btnDocumentDownload_Click(object sender, EventArgs e)
    {
        DocumentId =Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT FileName,Attachment FROM DD_DocketDocuments WHERE DocumentId=" + DocumentId.ToString());
            if(dt.Rows[0]["FileName"].ToString().Trim()!="")
        {
        byte[] buff = (byte[])dt.Rows[0]["Attachment"];
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + dt.Rows[0]["FileName"].ToString());
        Response.BinaryWrite(buff);
        Response.Flush();
        Response.End();
        }
    }
    protected void btnAddDoc_Click(object sender, EventArgs e)
    {
        dv_A_SubJobs.Visible = true;
    }
    protected void btn_SaveDoc_Click(object sender, EventArgs e)
    {
        if (txt_DocName.Text.Trim() == "")
        {
            lbl_A_MsgSubJob.Text = "Please enter Document Name.";
            return;
        }

        try
        {
            string FileName = "";
            byte[] FileContent = new byte[0];
            if (ftp_A_Upload.HasFile)
                if (ftp_A_Upload.PostedFile.ContentLength > 0)
                {
                    FileName = System.IO.Path.GetFileName(ftp_A_Upload.PostedFile.FileName);
                    FileContent = ftp_A_Upload.FileBytes;
                }

            Common.Set_Procedures("[dbo].[DD_UploadDocument]");
            Common.Set_ParameterLength(5);
            Common.Set_Parameters(
                new MyParameter("@DocumentId", DocumentId),
                new MyParameter("@DocketId", DocketId),
                new MyParameter("@DocumentName", txt_DocName.Text.Trim()),
                new MyParameter("@FileName", FileName),
                new MyParameter("@Attachment", FileContent));
            DataSet ds = new DataSet();
            ds.Clear();
            Boolean res;
            res = Common.Execute_Procedures_IUD(ds);
            if (res)
            {
                txt_DocName.Text = "";
                lbl_A_MsgSubJob.Text = "Document uploaded successfully.";
            }
            else
            {
                lbl_A_MsgSubJob.Text = "Unable to upload document. Error :" + Common.ErrMsg;
            }
        }
        catch (Exception ex)
        {
            lbl_A_MsgSubJob.Text = "Unable to upload document. Error :" + ex.Message;
        }
    }

    
    protected void btn_CloseDoc_Click(object sender, EventArgs e)
    {
        BindDocuments();
        dv_A_SubJobs.Visible = false;
    }
    protected void BindDocuments()
    {
        DataTable dtRFQ = Common.Execute_Procedures_Select_ByQuery("SELECT row_number() over(order by documentname) as SNo,* from DD_DocketDocuments Where DocketId=" + DocketId);
       rptDocuments.DataSource = dtRFQ;
       rptDocuments.DataBind();
    }
 }
