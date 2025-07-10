using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.SessionState;

public partial class Purchase_ShowDocuments : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int PoId = 0;
        string PRType = "", VesselCode = "", PVNo = "";
        int DocId = 0;
        int CreditNoteId = 0;
        int InvoiceId = 0;
        int AdvPaymentId = 0;
        int GLId = 0;

        if (Request.QueryString["VesselCode"] != null)
        {
            VesselCode = Request.QueryString["VesselCode"];
        }
        int UserId = Common.CastAsInt32(Session["LoginId"]);
        if (Request.QueryString["PRType"] != null || Request.QueryString["PRType"] != "")
        {
            PRType = Request.QueryString["PRType"];
        }
        if (Request.QueryString["PoId"] != null || Request.QueryString["PoId"] != "")
        {
            PoId = Common.CastAsInt32(Request.QueryString["PoId"]);
        }
        if (Request.QueryString["DocId"] != null || Request.QueryString["DocId"] != "")
        {
            DocId = Common.CastAsInt32(Request.QueryString["DocId"]);
        }
        if (Request.QueryString["CreditNoteId"] != null || Request.QueryString["CreditNoteId"] != "")
        {
            CreditNoteId = Common.CastAsInt32(Request.QueryString["CreditNoteId"]);
        }
        if (Request.QueryString["InvoiceId"] != null || Request.QueryString["InvoiceId"] != "")
        {
            InvoiceId = Common.CastAsInt32(Request.QueryString["InvoiceId"]);
        }
        if (Request.QueryString["PVNo"] != null || Request.QueryString["PVNo"] != "")
        {
            PVNo = Request.QueryString["PVNo"];
        }

        if (Request.QueryString["AdvPaymentId"] != null || Request.QueryString["AdvPaymentId"] != "")
        {
            AdvPaymentId = Common.CastAsInt32(Request.QueryString["AdvPaymentId"]);
        }

        if (Request.QueryString["GLId"] != null || Request.QueryString["GLId"] != "")
        {
            GLId = Common.CastAsInt32(Request.QueryString["GLId"]);
        }

        string sql = "";
        
        DataTable dt;
        if (PoId > 0)
        {

            sql = "SELECT top 1 DocName As FileName,Attachment,ContentType,OldAttachment FROM [tblSMDPODocuments] WHERE [VesselCode] = '" + VesselCode + "' AND  PoId =" + PoId + " and DocId = " + DocId;

            dt = Common.Execute_Procedures_Select_ByQuery(sql);

            if (dt.Rows.Count > 0)
            {
                try
                {

                    string contentType = "";
                    string FileName = "";
                    if (!string.IsNullOrWhiteSpace(dt.Rows[0]["ContentType"].ToString()))
                    {
                        contentType = dt.Rows[0]["ContentType"].ToString();
                    }
                    if (!string.IsNullOrWhiteSpace(dt.Rows[0]["FileName"].ToString()))
                    {
                        FileName = dt.Rows[0]["FileName"].ToString();
                    }
                    if (!string.IsNullOrWhiteSpace(contentType))
                    {

                        byte[] latestFileContent = (byte[])dt.Rows[0]["Attachment"];
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
                        sDocName = dt.Rows[0]["FileName"].ToString();
                        byte[] DocFile = (byte[])dt.Rows[0]["OldAttachment"];

                        Response.Clear();
                        Response.ClearContent();
                        Response.ClearHeaders();

                        if (sDocName.EndsWith(".txt"))
                        {
                            Response.ContentType = "text/plain";
                        }
                        if (sDocName.EndsWith(".xls"))
                        {
                            Response.ContentType = "application/vnd.xls";
                        }
                        if (sDocName.EndsWith(".doc"))
                        {
                            Response.ContentType = "application/ms-word";
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
                        if (sDocName.EndsWith(".png"))
                        {
                            Response.ContentType = "text/xml";
                        }


                        Response.AddHeader("Content-Disposition", "attachment; filename=" + sDocName);
                        //Response.AddHeader("Content-Length", sDocName.Length.ToString());
                        Response.OutputStream.Write(DocFile, 0, DocFile.Length - 1);
                        //Response.End();
                    }


                }
                catch (Exception ex)
                {

                    Response.Clear();
                    Response.Write("<center> Invalid File !</center>");
                    Response.End();
                }
            }

        }
        else if (PRType == "CreditNote" && CreditNoteId > 0)
        {
            sql = "SELECT top 1 DocName As FileName,Attachment,ContentType FROM [InvoiceCreditNotesDetails] WHERE  CreditNoteId =" + CreditNoteId + " and InvoiceId = " + InvoiceId + " ";
            dt = Common.Execute_Procedures_Select_ByQuery(sql);
            if (dt.Rows.Count > 0)
            {
                try
                {
                    string contentType = "";
                    string FileName = "";
                    if (!string.IsNullOrWhiteSpace(dt.Rows[0]["ContentType"].ToString()))
                    {
                        contentType = dt.Rows[0]["ContentType"].ToString();
                    }
                    if (!string.IsNullOrWhiteSpace(dt.Rows[0]["FileName"].ToString()))
                    {
                        FileName = dt.Rows[0]["FileName"].ToString();
                    }
                    if (!string.IsNullOrWhiteSpace(contentType))
                    {

                        byte[] latestFileContent = (byte[])dt.Rows[0]["Attachment"];
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
                }
                catch (Exception ex)
                {

                    Response.Clear();
                    Response.Write("<center> Invalid File !</center>");
                    Response.End();
                }
            }
        }
        else if (PRType == "PaymentDocument" )
        {
            if (DocId > 0 && PVNo != "")
            {
                sql = "SELECT top 1 DocName As FileName,Doc As Attachment,DocType As ContentType FROM [POS_Invoice_Payment_Document] WHERE  ID =" + DocId + " and PVNo = '" + PVNo + "' ";
                dt = Common.Execute_Procedures_Select_ByQuery(sql);
                if (dt.Rows.Count > 0)
                {
                    try
                    {
                        string contentType = "";
                        string FileName = "";
                        if (!string.IsNullOrWhiteSpace(dt.Rows[0]["ContentType"].ToString()))
                        {
                            contentType = dt.Rows[0]["ContentType"].ToString();
                        }
                        if (!string.IsNullOrWhiteSpace(dt.Rows[0]["FileName"].ToString()))
                        {
                            FileName = dt.Rows[0]["FileName"].ToString();
                        }
                        if (!string.IsNullOrWhiteSpace(contentType))
                        {

                            byte[] latestFileContent = (byte[])dt.Rows[0]["Attachment"];
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
                    }
                    catch (Exception ex)
                    {

                        Response.Clear();
                        Response.Write("<center> Invalid File !</center>");
                        Response.End();
                    }
                }
            }
            else
            {
                Response.Clear();
                Response.Write("<center> No Document Found !</center>");
                Response.End();
            }
            
            
        }
        else if (PRType == "AdvancePayment" && AdvPaymentId > 0)
            {

                    sql = "SELECT top 1 AttachmentName,Attachment,AttachmentType FROM [POS_Invoice_AdvancePayment] with(nolock) WHERE  AdvPaymentId =" + AdvPaymentId + " ";
                    dt = Common.Execute_Procedures_Select_ByQuery(sql);
                    if (dt.Rows.Count > 0)
                    {
                        try
                        {
                            string contentType = "";
                            string FileName = "";
                            if (!string.IsNullOrWhiteSpace(dt.Rows[0]["AttachmentType"].ToString()))
                            {
                                contentType = dt.Rows[0]["AttachmentType"].ToString();
                            }
                            if (!string.IsNullOrWhiteSpace(dt.Rows[0]["AttachmentName"].ToString()))
                            {
                                FileName = dt.Rows[0]["AttachmentName"].ToString();
                            }
                            if (!string.IsNullOrWhiteSpace(contentType))
                            {

                                byte[] latestFileContent = (byte[])dt.Rows[0]["Attachment"];
                                Response.Clear();
                                Response.Buffer = true;
                                Response.Charset = "";
                                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                                Response.ContentType = contentType;
                                Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileName);
                                Response.BinaryWrite(latestFileContent);
                                Response.Flush();
                              // HttpContext.Current.Response.End();
                    }
                        }
                        catch (Exception ex)
                        {

                            Response.Clear();
                            Response.Write("<center> Invalid File !</center>");
                            Response.End();
                        }
                    }
                

            }
        else if (PRType == "GLEntry" && GLId > 0)
        {
             sql = "SELECT top 1 DocName As AttachmentName,Attachment As Attachment,ContentType As AttachmentType FROM [tblGLEntry_Documents] with(nolock) WHERE  GLId =" + GLId + "  AND DocId = " + DocId + " AND VesselCode = '" + VesselCode + "'";
            dt = Common.Execute_Procedures_Select_ByQuery(sql);
            if (dt.Rows.Count > 0)
            {
                try
                {
                    string contentType = "";
                    string FileName = "";
                    if (!string.IsNullOrWhiteSpace(dt.Rows[0]["AttachmentType"].ToString()))
                    {
                        contentType = dt.Rows[0]["AttachmentType"].ToString();
                    }
                    if (!string.IsNullOrWhiteSpace(dt.Rows[0]["AttachmentName"].ToString()))
                    {
                        FileName = dt.Rows[0]["AttachmentName"].ToString();
                    }
                    if (!string.IsNullOrWhiteSpace(contentType))
                    {

                        byte[] latestFileContent = (byte[])dt.Rows[0]["Attachment"];
                        Response.Clear();
                        Response.Buffer = true;
                        Response.Charset = "";
                        Response.Cache.SetCacheability(HttpCacheability.NoCache);
                        Response.ContentType = contentType;
                        Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileName);
                        Response.BinaryWrite(latestFileContent);
                        Response.Flush();
                        // HttpContext.Current.Response.End();
                    }
                }
                catch (Exception ex)
                {

                    Response.Clear();
                    Response.Write("<center> Invalid File !</center>");
                    Response.End();
                }
            }


        }
        else
        {
            sql = "SELECT top 1 DocName As FileName,Attachment,ContentType FROM [MP_VSL_TEMP_RequisitionDocuments] WHERE [VesselCode] = '" + VesselCode + "' AND  LoginId =" + UserId + " and DocId = " + DocId + " and PRType = '" + PRType + "'";
            dt = Common.Execute_Procedures_Select_ByQuery(sql);

            if (dt.Rows.Count > 0)
            {
                try
                {
                    string contentType = "";
                    string FileName = "";
                    if (!string.IsNullOrWhiteSpace(dt.Rows[0]["ContentType"].ToString()))
                    {
                        contentType = dt.Rows[0]["ContentType"].ToString();
                    }
                    if (!string.IsNullOrWhiteSpace(dt.Rows[0]["FileName"].ToString()))
                    {
                        FileName = dt.Rows[0]["FileName"].ToString();
                    }
                    if (!string.IsNullOrWhiteSpace(contentType))
                    {

                        byte[] latestFileContent = (byte[])dt.Rows[0]["Attachment"];
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

                }
                catch (Exception ex)
                {

                    Response.Clear();
                    Response.Write("<center> Invalid File !</center>");
                    Response.End();
                }
            }
        }

    }
}