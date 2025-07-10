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


public partial class DD_OFC_PlanSettings : System.Web.UI.Page
{
    #region -------- PROPERTIES ------------------

   
    public int JobId
    {
        set { ViewState["JobId"] = value; }
        get { return Common.CastAsInt32(ViewState["JobId"]); }
    }
    public int SubJobId
    {
        set { ViewState["SubJobId"] = value; }
        get { return Common.CastAsInt32(ViewState["SubJobId"]); }
    }

    #endregion -----------------------------------
    Random rnd = new Random();
    protected void Page_Load(object sender, EventArgs e)
    {
        // -------------------------- SESSION CHECK ----------------------------------------------
        ProjectCommon.SessionCheck();
        // -------------------------- SESSION CHECK END ----------------------------------------------
        if (!IsPostBack)
        {
            Session["DDPageId"] = "DD_OFC_PlanSettings";
        }
    }
  }
