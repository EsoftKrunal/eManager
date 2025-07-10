using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Text;

public partial class eReports_S115_AppletReceiver : System.Web.UI.Page
{
    public string FormNo
    {
        get { return ViewState["FormNo"].ToString(); }
        set { ViewState["FormNo"] = value; }
    }
    public string VesselName
    {
        get { return ViewState["VesselName"].ToString(); }
        set { ViewState["VesselName"] = value; }
    }
    public string UserName
    {
        get { return ViewState["UserName"].ToString(); }
        set { ViewState["UserName"] = value; }
    }
    public int ReportId
    {
        get { return Common.CastAsInt32(ViewState["ReportId"]); }
        set { ViewState["ReportId"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FormNo = Request.QueryString["Key"];
            ReportId = Common.CastAsInt32(Request.QueryString["ReportId"]);
            VesselName = Session["CurrentShip"].ToString();
            UserName = Session["FullName"].ToString();
        }
        UploadFiles();
    }
    protected void UploadFiles()
    {
        string savepath = Server.MapPath("~\\eReports\\" + FormNo + "\\Documents\\Ship");

        if (!Directory.Exists(savepath))
            Directory.CreateDirectory(savepath);

        for (int i = 1; i <= Request.Files.Count; i++)
        {

            HttpPostedFile ht = Request.Files[i - 1];
            string OrigFileName = Path.GetFileName(ht.FileName);
            string filename = VesselName + "_" + FormNo + "_" + ReportId + "_" + DateTime.Now.ToString("ddmmyyyyhhmmssfff") + "@" + OrigFileName;
            ht.SaveAs(savepath + "\\" + filename);
            Common.Execute_Procedures_Select_ByQuery("EXEC [DBO].[ER_S115_Insert_ER_Report_Documents_OfficeShip] '" + VesselName + "','" + FormNo + "'," + ReportId.ToString() + ",'" + Path.GetFileNameWithoutExtension(OrigFileName) + "','" + filename + "','" + UserName + "','S'");
        }
    }
}