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

public partial class Reports_InspMisc : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        if (Session["Data_Table"] != null)
        {
            ExportDatatableToXLS(Response, (DataTable)Session["Data_Table"], "InspectionMiscellaneous");
        }
    }
    public void ExportDatatableToXLS(HttpResponse Response, DataTable dt, String FileName)
    {

        Response.Clear();
        Response.AddHeader("content-disposition", "attachment;filename=" + FileName + ".xls");
        Response.Charset = "";
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.xls";
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
        System.Web.UI.WebControls.DataGrid dg = new System.Web.UI.WebControls.DataGrid();
        dg.HeaderStyle.Font.Bold = true;
        dg.DataSource = dt;
        dg.DataBind();
        dg.RenderControl(htmlWrite);
        Response.Write(stringWrite.ToString());
        Response.End();
    }
}