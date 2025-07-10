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


public partial class DD_OFC_VesselDockets : System.Web.UI.Page
{
    #region -------- PROPERTIES ------------------
    public int VesselId
    {
        set { ViewState["VesselId"] = value; }
        get { return Common.CastAsInt32(ViewState["VesselId"]); }
    }
    #endregion -----------------------------------
    protected void Page_Load(object sender, EventArgs e)
    {
        // -------------------------- SESSION CHECK ----------------------------------------------
        ProjectCommon.SessionCheck();
        // -------------------------- SESSION CHECK END ----------------------------------------------
        if (!IsPostBack)
        {
            VesselId=Common.CastAsInt32(Request.QueryString["VesselId"]);
            lblVesselName.Text = Request.QueryString["VesselName"].ToString();
            BindDockets();
        }
    }
    protected void BindDockets()
    {
        DataTable dt = new DataTable();
        string strSQL = "SELECT * FROM [dbo].[DD_DocketMaster] WHERE VESSELID=" + VesselId.ToString();
        dt = Common.Execute_Procedures_Select_ByQuery(strSQL);
        rptDocket.DataSource = dt;
        rptDocket.DataBind();
    }
   
   }
