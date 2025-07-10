using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.SessionState;

public partial class LPSQE_ShowDocuments : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int ReportPk = 0;
        int DocId = Common.CastAsInt32(Page.Request.QueryString["DocId"]);
        string VesselId = Request.QueryString["VesselId"];
        
        ReportPk = Common.CastAsInt32(Request.QueryString["ReportPk"]);

        string sql = "";
        string sDocName = "";
        DataTable dt;
        if (ReportPk > 0)
        {
            sql = "SELECT top 1  FileName,Attachment,ContentType FROM [VSL_BunkerAttachments] WHERE [VesselId] = '" + VesselId + "' AND  ReportPk =" + ReportPk + " and DocId = " + DocId;
            //if (PRType == "SS")
            //sql = "SELECT top 1 DocName As FileName,Attachment,ContentType FROM [MP_VSL_StoreReqDocuments] WHERE [VesselCode] = '" + VesselCode + "' AND  StoreReqId =" + RequisitionId + " and DocId = " + DocId;
            //if (PRType == "SP")
            //    sql = "SELECT top 1 DocName As FileName,Attachment,ContentType FROM [MP_VSL_SpareReqDocuments] WHERE [VesselCode] = '" + VesselCode + "' AND  SpareReqId =" + RequisitionId + " and DocId = " + DocId;
            //if (PRType == "PR")
            //    sql = "SELECT top 1 DocName As FileName,Attachment,ContentType FROM [MP_VSL_ProvisionDocuments] WHERE [VesselCode] = '" + VesselCode + "' AND  ProvisionId =" + RequisitionId + " and DocId = " + DocId;
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
                    //sDocName = dt.Rows[0]["FileName"].ToString();
                    //byte[] DocFile = (byte[])dt.Rows[0]["Attachment"];

                    //Response.Clear();
                    //Response.ClearContent();
                    //Response.ClearHeaders();

                    //if (sDocName.EndsWith(".txt"))
                    //{
                    //    Response.ContentType = "text/plain";
                    //}
                    //if (sDocName.EndsWith(".xls"))
                    //{
                    //    Response.ContentType = "application/vnd.xls";
                    //}
                    //if (sDocName.EndsWith(".doc"))
                    //{
                    //    Response.ContentType = "application/ms-word";
                    //}
                    //if (sDocName.EndsWith(".pfd"))
                    //{
                    //    Response.ContentType = "application/pdf";
                    //}
                    //if (sDocName.EndsWith(".zip"))
                    //{
                    //    Response.ContentType = "application/x-zip-compressed";
                    //}
                    //if (sDocName.EndsWith(".gif"))
                    //{
                    //    Response.ContentType = "image/gif";
                    //}
                    //if (sDocName.EndsWith(".jpeg"))
                    //{
                    //    Response.ContentType = "image/jpeg";
                    //}
                    //if (sDocName.EndsWith(".png"))
                    //{
                    //    Response.ContentType = "image/png";
                    //}
                    //if (sDocName.EndsWith(".png"))
                    //{
                    //    Response.ContentType = "text/xml";
                    //}


                    //Response.AddHeader("Content-Disposition", "attachment; filename=" + sDocName);
                    ////Response.AddHeader("Content-Length", sDocName.Length.ToString());
                    //Response.OutputStream.Write(DocFile, 0, DocFile.Length - 1);
                    ////Response.End();

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