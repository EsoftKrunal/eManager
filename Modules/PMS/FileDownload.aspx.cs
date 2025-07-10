using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FileDownload : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string SaveTargetDir = Server.MapPath("~/TEMP/");
        string FilePath = "" + Request.QueryString["File"];
        Response.Clear();
        Response.ContentType = "application/zip";
        Response.AddHeader("Content-Type", "application/zip");
        Response.AddHeader("Content-Disposition", "inline;filename=" + FilePath);
        Response.WriteFile(SaveTargetDir +  FilePath);
        Response.End();
    }
}