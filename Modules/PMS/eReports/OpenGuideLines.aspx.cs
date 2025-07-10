using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Text;

public partial class eReports_OpenGuideLines : System.Web.UI.Page
{
    public string ManualId
    {
        get { return ViewState["ManualId"].ToString(); }
        set { ViewState["ManualId"] = value; }
    }
    public string SectionNo
    {
        get { return ViewState["SectionNo"].ToString(); }
        set { ViewState["SectionNo"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ManualId = Request.QueryString["ManualId"];
            SectionNo = Request.QueryString["SectionNo"];
        }
        UploadFiles();
    }
    protected void UploadFiles()
    {
      
    }
}