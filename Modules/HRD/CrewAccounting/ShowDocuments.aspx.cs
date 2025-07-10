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

public partial class CrewAccounting_ShowDocuments : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        int TableID = Common.CastAsInt32(Page.Request.QueryString["TableID"]);

        string sql = "select * from CrewPayrollDocuments where TableID=" + TableID .ToString()+ "";
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        string sDocName = "";
        if (dt.Rows.Count > 0)
        {
            try
            {
                sDocName = dt.Rows[0]["FileName"].ToString();
                byte[] DocFile = (byte[])dt.Rows[0]["Attachment"];

                Response.Clear();
                Response.ClearContent();
                Response.ClearHeaders();
                
                if(sDocName.EndsWith(".txt")) 
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
                Response.OutputStream.Write(DocFile, 0, DocFile.Length-1);
                //Response.End();
                
            }
            catch(Exception ex) {

                Response.Clear();
                Response.Write("<center> Invalid File !</center>");
                Response.End();
            }
        }

    }
}
