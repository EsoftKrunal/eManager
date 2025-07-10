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
    DataTable dtDetails;
    public int VIQId
    {
        get { return Common.CastAsInt32(ViewState["VIQId"]); }
        set { ViewState["VIQId"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            VIQId = Common.CastAsInt32(Request.QueryString["VIQId"]);
            if (VIQId > 0)
            {
                BindChapters();
            }
        }
        
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        string FolderPath = Server.MapPath("~/VIMS/TEMP");
        string[] Files = Directory.GetFiles(FolderPath, "*.zip");

        foreach (string fl in Files)
        {
            try
            {
                File.Delete(fl);
            }
            catch { }
        }

        string VIQNo = "";
        string VesselCode = Session["CurrentShip"].ToString();

        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM [dbo].[VIQ_VIQMaster] WHERE VESSELCODE='" + VesselCode + "' AND VIQID=" + VIQId.ToString());
        DataTable dt1 = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM [dbo].[VIQ_VIQDetails] WHERE VESSELCODE='" + VesselCode + "' AND VIQID=" + VIQId.ToString());
        DataTable dt2 = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM [dbo].[VIQ_VIQDetailsRanks] WHERE VESSELCODE='" + VesselCode + "' AND VIQID=" + VIQId.ToString());
        DataTable dt3 = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM [dbo].[VIQ_VIQDetailsRanksAttachments] WHERE VESSELCODE='" + VesselCode + "' AND VIQID=" + VIQId.ToString());

        dt.TableName = "VIQ_VIQMaster";
        dt1.TableName = "VIQ_VIQDetails";
        dt2.TableName = "VIQ_VIQDetailsRanks";
        dt3.TableName = "VIQ_VIQDetailsRanksAttachments";

        VIQNo = dt.Rows[0]["ViqNo"].ToString();

        DataSet ds = new DataSet();
        ds.Tables.Add(dt.Copy());
        ds.Tables.Add(dt1.Copy());
        ds.Tables.Add(dt2.Copy());
        ds.Tables.Add(dt3.Copy());

        string SchemaFile = FolderPath + "/VETTING_SCHEMA.XML";
        string DataFile = FolderPath + "/VETTING_DATA.XML";

        string ZipFile = FolderPath + "/VIQ_S_" + VIQNo.Replace("/", "-") + ".zip";

        ds.WriteXmlSchema(SchemaFile);
        ds.WriteXml(DataFile);

        using (ZipFile zip = new ZipFile())
        {
            zip.AddFile(SchemaFile);
            zip.AddFile(DataFile);
            zip.Save(ZipFile);

            Response.Clear();
            Response.ContentType = "application/zip";
            Response.AddHeader("Content-Type", "application/zip");
            Response.AddHeader("Content-Disposition", "inline;filename=" + Path.GetFileName(ZipFile));
            Response.WriteFile(ZipFile);
            Response.End();
        }
    }
    protected void BindChapters()
    {
        string VesselCode = Session["CurrentShip"].ToString();
        string sql = "SELECT viqno,targetdate,VIQTYPE FROM VIQ_VIQMaster WHERE VIQId=" + VIQId.ToString();
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
        lblheader.Text = dt.Rows[0]["VIQNO"].ToString() +  " / " + Common.ToDateString(dt.Rows[0]["TARGETDATE"]);
        //tr_Export.Visible = dt.Rows[0]["VIQTYPE"].ToString() == "1";

        sql = "SELECT *,((DONEQ*100)/(CASE WHEN NOQ=0 THEN 1 ELSE NOQ END)) AS PERDONE FROM " +
                    "( " +
                    "SELECT DISTINCT M.VIQID,CHAPTERID,CHAPTERNO,CHAPTERNAME, " +
                    "(SELECT COUNT(*) FROM [dbo].[vw_VIQQuestionRanks] D WHERE D.VESSELCODE=M.VESSELCODE AND D.VIQID=M.VIQID AND D.CHAPTERID=M.CHAPTERID) AS NOQ , " +
                    "(SELECT COUNT(*) FROM [dbo].[vw_VIQQuestionRanks] D WHERE D.VESSELCODE=M.VESSELCODE AND D.VIQID=M.VIQID AND D.CHAPTERID=M.CHAPTERID AND ISNULL(OfficeClosureStatus,0)>0 ) AS DONEQ  " +
                    "FROM [dbo].[VIQ_VIQDetails] M WHERE VESSELCODE='" + VesselCode + "' AND VIQID=" + VIQId.ToString() + " " +
                    ") C " +
                    "ORDER BY cast(CHAPTERNO as int)";
        
        dt = Common.Execute_Procedures_Select_ByQuery(sql);
        rpt_Chapters.DataSource = dt;
        rpt_Chapters.DataBind();
    }
}