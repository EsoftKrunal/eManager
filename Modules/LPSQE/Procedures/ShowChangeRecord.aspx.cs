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
using System.Text;

public partial class ShowChangeRecord : System.Web.UI.Page
{
    public int _ManualId;
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        _ManualId = Common.CastAsInt32("" + Request.QueryString["ManualId"]);
        if(!IsPostBack)
        {
            ShowHeader();
            ShowRevisions();
        }
    }
    public void ShowHeader()
    {
        try
        {
            ManualBO mb = new ManualBO(_ManualId);
            lblManualName.Text = mb.ManualName;
            lblMVersion.Text = "[" + mb.VersionNo + "]";
        }
        catch { } 
    }
    public void ShowRevisions()
    {
        DataTable dt = Manual.getRevisions(_ManualId);
        gvChangeRecords.DataSource = dt;
        gvChangeRecords.DataBind();
    }
}
