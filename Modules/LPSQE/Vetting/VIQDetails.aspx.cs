using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using Ionic.Zip;

public partial class VIMS_VIQDetails : System.Web.UI.Page
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
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        if (!Page.IsPostBack)
        {
            VesselCode = Request.QueryString["VSLCODE"];
            VIQId = Common.CastAsInt32(Request.QueryString["VIQId"]);
            BindChapters();
        }
    }
    public void BindChapters()
    {
        string sql = "SELECT viqno,targetdate FROM [dbo].VIQ_VIQMaster WHERE VIQId=" + VIQId.ToString();
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
        lblheader.Text = dt.Rows[0]["VIQNO"].ToString();

        sql = "SELECT *,((DONEQ*100)/(CASE WHEN NOQ=0 THEN 1 ELSE NOQ END)) AS PERDONE FROM " +
                    "( " +
                    "SELECT DISTINCT m.VESSELCODE,M.VIQID,CHAPTERID,CHAPTERNO,CHAPTERNAME, " +
                    "(SELECT COUNT(*) FROM [dbo].[vw_VIQQuestionRanks] D WHERE D.VESSELCODE=M.VESSELCODE AND D.VIQID=M.VIQID AND D.CHAPTERID=M.CHAPTERID) AS NOQ , " +
                    "(SELECT COUNT(*) FROM [dbo].[vw_VIQQuestionRanks] D WHERE D.VESSELCODE=M.VESSELCODE AND D.VIQID=M.VIQID AND D.CHAPTERID=M.CHAPTERID AND ISNULL(OfficeClosureStatus,0)>0 ) AS DONEQ  " +
                    "FROM [dbo].[VIQ_VIQDetails] M WHERE VESSELCODE='" + VesselCode + "' AND VIQID=" + VIQId.ToString() + " " +
                    ") C " +
                    "ORDER BY cast(CHAPTERNO as int)";
        dt = Common.Execute_Procedures_Select_ByQuery(sql);
        rpt_Chapters.DataSource = dt;
        rpt_Chapters.DataBind();
    }
    
   
    private void ResetNULLDates(ref DataSet ds_IN)
    {
        DateTime dt_ref = new DateTime(1900, 1, 1);
        foreach (DataTable dt in ds_IN.Tables)
        {
            List<String> DateTimeCols = new List<String>();
            foreach (DataColumn dc in dt.Columns)
            {
                if (dc.DataType == System.Type.GetType("System.DateTime"))
                {
                    DateTimeCols.Add(dc.ColumnName);
                }
            }
            if (DateTimeCols.Count > 0 && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    foreach (string cName in DateTimeCols)
                    {
                        if (!Convert.IsDBNull(dr[cName]))
                        {
                            DateTime dt_test = Convert.ToDateTime(dr[cName]);
                            if (dt_test <= dt_ref)
                            {
                                dr[cName] = DBNull.Value;
                            }
                        }
                    }
                }
            }
        }
    }
   
}