using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class VIMS_ViewUploadFile : System.Web.UI.Page
{
    public string VesselCode
    {
        get { return ViewState["VesselCode"].ToString(); }
        set { ViewState["VesselCode"] = value; }
    }
    public int VIQId
    {
        get { return Common.CastAsInt32(ViewState["VIQId"]); }
        set { ViewState["VIQId"] = value; }
    }
    public int Qid
    {
        get{return Common.CastAsInt32(ViewState["Qid"]);}
        set{ViewState["Qid"]=value;} 
    }
    public int RankId
    {
        get { return Common.CastAsInt32(ViewState["RankId"]); }
        set { ViewState["RankId"] = value; }
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        if (!IsPostBack)
        {
            try
            {
                VesselCode = Request.QueryString["VSL"];
            }
            catch {
                ProjectCommon.ShowMessage("Your session is expired. Please login again.");
                return; 
            }
            VIQId = Common.CastAsInt32(Request.QueryString["VIQId"]);
            Qid = Common.CastAsInt32(Request.QueryString["Qid"]);
            RankId = Common.CastAsInt32(Request.QueryString["RankId"]);
            bindgrid();
        }
      
    }
    protected void bindgrid()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("select  ROW_NUMBER() OVER(ORDER BY FILENAME) AS SNO,* from DBO.VIQ_VIQDetailsRanksAttachments WHERE VESSELCODE='" + VesselCode + "' AND VIQID=" + VIQId.ToString() + " AND QUESTIONID=" + Qid.ToString() + " AND RANKID=" + RankId.ToString());
        rptFiles.DataSource = dt;
        rptFiles.DataBind();
    }
}


