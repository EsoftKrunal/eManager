using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Text;

public partial class eReports_UploadDocuments : System.Web.UI.Page
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
            ReportId=Common.CastAsInt32(Request.QueryString["ReportId"]);
            VesselName=Session["CurrentShip"].ToString();
            UserName=Session["FullName"].ToString();
            txtNOF.Text = "";
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        //StringBuilder sb = new StringBuilder();
        int Files;
        Files = Common.CastAsInt32(txtNOF.Text);
        
        string savepath = Server.MapPath("~\\eReports\\" + FormNo + "\\Documents\\Ship"); 

        if (!Directory.Exists(savepath))
            Directory.CreateDirectory(savepath);

        for (int i = 1; i <= Files; i++)
        {
            HttpPostedFile ht=Request.Files[i-1];
            string filename = VesselName +"_" + FormNo + "_" + ReportId + "_" + DateTime.Now.ToString("ddmmyyyyhhmmssfff")+"@" + Path.GetFileName(ht.FileName);
            ht.SaveAs(savepath + "\\" + filename);
            Common.Execute_Procedures_Select_ByQuery("EXEC [DBO].[ER_S115_Insert_ER_Report_Documents_OfficeShip] '" + VesselName + "','" + FormNo + "'," + ReportId.ToString() + ",'" + Request.Form["txt" + (i - 1).ToString()] + "','" + filename + "','" + UserName + "','S'");
            txtNOF.Text = "";
        }
        Label1.Text = Files.ToString() + " Files(s) Uploaded successfully.";
    }
}