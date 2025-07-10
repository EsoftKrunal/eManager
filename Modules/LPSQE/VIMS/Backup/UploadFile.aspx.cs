using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class VIMS_UploadFile : System.Web.UI.Page
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
    public bool AllowUpload
    {
        get { return Convert.ToBoolean(ViewState["AllowUpload"]); }
        set { ViewState["AllowUpload"] = value; }
    }
    public string AllowUploadText
    {
        get { return ViewState["AllowUploadText"].ToString(); }
        set { ViewState["AllowUploadText"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                VesselCode = Session["CurrentShip"].ToString();
            }
            catch {
                ProjectCommon.ShowMessage("Your session is expired. Please login again.");
                return; 
            }
            VIQId = Common.CastAsInt32(Request.QueryString["VIQId"]);
            Qid = Common.CastAsInt32(Request.QueryString["Qid"]);
            RankId = Common.CastAsInt32(Request.QueryString["RankId"]);
            bindgrid();
            AllowUpload = (Request.QueryString["Mode"].ToString() == "E");
            AllowUploadText = (AllowUpload) ? "display:block;" : "display:none;";
            dvUpload.Visible = AllowUpload;
            dvmargin.Visible = AllowUpload;
        }
      
    }
    protected void bindgrid()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("select  ROW_NUMBER() OVER(ORDER BY FILENAME) AS SNO,* from VIQ_VIQDetailsRanksAttachments WHERE VESSELCODE='" + VesselCode + "' AND VIQID=" + VIQId.ToString() + " AND QUESTIONID=" + Qid.ToString() + " AND RANKID=" + RankId.ToString());
        rptFiles.DataSource = dt;
        rptFiles.DataBind();
    }

    protected void imgDelete_Click(object sender, EventArgs e)
    {
        int Attid = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        string FileName = ((ImageButton)sender).CssClass;
        string FullPath=Server.MapPath("~/VIMS/Attachments/") + FileName;
        if(System.IO.File.Exists(FullPath))
            System.IO.File.Delete(FullPath);
        Common.Execute_Procedures_Select_ByQuery("DELETE FROM VIQ_VIQDetailsRanksAttachments WHERE VESSELCODE='" + VesselCode + "' AND VIQID=" + VIQId.ToString() + " AND QUESTIONID=" + Qid.ToString() + " AND RANKID=" + RankId.ToString() + " AND ATTACHMENTID=" + Attid);
        bindgrid();
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (dlpUpload.HasFile)
        {
            string filename = System.IO.Path.GetFileName(dlpUpload.FileName).Replace("'","`");
            string pkstring = "VIQ_" + VesselCode + "_" + VIQId + "_" + Qid + "_" + RankId +"_";
            Common.Execute_Procedures_Select_ByQuery("EXEC VIQ_VSL_IMPORT_VIQDetailsRanksAttachments '" + VesselCode + "'," + VIQId.ToString() + "," + Qid.ToString() + "," + RankId.ToString() + ",'" + txtDescr.Text.Trim().Replace("'", "`") + "','" + pkstring + filename + "'");
            dlpUpload.SaveAs(Server.MapPath("~/VIMS/Attachments/" + pkstring + filename));
            txtDescr.Text="";
            bindgrid();
        }
    }
}


