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
using System.Collections.Generic;
using Ionic.Zip;
using System.IO;

public partial class PB_PublicationCommunication : System.Web.UI.Page
{
    public int KeyId
    {
        set { ViewState["KeyId"] = value; }
        get { return Common.CastAsInt32(ViewState["KeyId"]); }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        if (!IsPostBack)
        {
            BindVessels();
            BindRepeater();
        }
    }
    protected void BindVessels()
    {
        string sql = "SELECT * FROM DBO.VESSEL WHERE VESSELSTATUSID=1  ORDER BY VESSELNAME";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
        ddlVessel_F.DataSource = dt;
        ddlVessel_F.DataTextField = "VESSELNAME";
        ddlVessel_F.DataValueField = "VESSELID";
        ddlVessel_F.DataBind();
        ddlVessel_F.Items.Insert(0, new ListItem(" < -- SELECT -- > ", "0"));
    }
    protected void BindRepeater()
    {
        string WhereClause = " WHERE PUB.TYPEID IN (select TypeId from dbo.PB_Publication_Type_Vessels where vesselid=" + ddlVessel_F.SelectedValue + ")";
        string sql="SELECT PUBLICATIONID,PUBLICATIONNAME,TYPENAME,MODENAME,PUBLISHERNAME,OfficeShip=(case when OfficeShip='O' then 'Office' when OfficeShip='S' then 'Ship' else 'Both' End),EditionYear,EditionNo,ValidityDate,CREATEDBY,CREATEDON " +
                   "FROM DBO.PB_PUBLICATIONS PUB  " +
                   "INNER JOIN DBO.PB_Publication_Type T ON T.TYPEID=PUB.TYPEID " +
                   "INNER JOIN DBO.PB_Publication_Mode D ON D.MODEID=PUB.MODEID " +
                   "LEFT JOIN DBO.PB_Publisher S ON S.PUBLISHERID=PUB.PUBLISHERID " + WhereClause;
                   
        rptData.DataSource = Common.Execute_Procedures_Select_ByQuery(sql);
        rptData.DataBind();
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        if(ddlVessel_F.SelectedIndex<=0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "alert('Please select vessel to export.');", true);
            ddlVessel_F.Focus();
            return; 
        }
        try
        {
            int Month = DateTime.Today.Month;
            int Year = DateTime.Today.Year;

            string VesselCode = "";
            DataTable dt_Vsl = Common.Execute_Procedures_Select_ByQuery("SELECT VESSELCODE FROM DBO.VESSEL WHERE VESSELID=" + ddlVessel_F.SelectedValue);
            if (dt_Vsl.Rows.Count > 0)
            {
                VesselCode = dt_Vsl.Rows[0][0].ToString();
            }

            string WhereClause = " WHERE PUB.TYPEID IN (select TypeId from dbo.PB_Publication_Type_Vessels where vesselid=" + ddlVessel_F.SelectedValue + ")";
            string sql = "SELECT '" + VesselCode + "' as VesselCode," + Month.ToString() + " as PUBMONTH," + Year.ToString() + "as PUBYEAR,PUBLICATIONID,PUBLICATIONNAME,TYPENAME,MODENAME,PUBLISHERNAME,OfficeShip=(case when OfficeShip='O' then 'Office' when OfficeShip='S' then 'Ship' else 'Both' End), " +
                       "COALESCE((SELECT EDITIONYEAR FROM DBO.PB_PublicationEditions ED WHERE ED.PUBLICATIONID=PUB.PUBLICATIONID) ,EditionYear) AS EditionYear, " +
                       "COALESCE((SELECT EditionNo FROM DBO.PB_PublicationEditions ED WHERE ED.PUBLICATIONID=PUB.PUBLICATIONID) ,EditionNo) AS EditionNo, " +
                       "COALESCE((SELECT ValidityDate FROM DBO.PB_PublicationEditions ED WHERE ED.PUBLICATIONID=PUB.PUBLICATIONID) ,ValidityDate) AS ValidityDate " +
                       "FROM DBO.PB_PUBLICATIONS PUB  " +
                       "INNER JOIN DBO.PB_Publication_Type T ON T.TYPEID=PUB.TYPEID  " +
                       "INNER JOIN DBO.PB_Publication_Mode D ON D.MODEID=PUB.MODEID  " +
                       "LEFT JOIN DBO.PB_Publisher S ON S.PUBLISHERID=PUB.PUBLISHERID " + WhereClause;

            DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
            string SchemaPath = Server.MapPath("~/Temp/PBSchema.xml");
            string DataPath = Server.MapPath("~/Temp/PBData.xml");

            dt.DataSet.WriteXmlSchema(SchemaPath);
            dt.DataSet.WriteXml(DataPath);


            string ZipFile = Server.MapPath("~/Temp/PB_Packet_" + Month + "_" + Year + ".zip");
            if (File.Exists(ZipFile)) { File.Delete(ZipFile); }
            using (ZipFile zip = new ZipFile())
            {
                zip.AddFile(SchemaPath);
                zip.AddFile(DataPath);
                zip.Save(ZipFile);
            }
            Response.AppendHeader("content-disposition", "attachment; filename=" + Path.GetFileName(ZipFile));
            Response.WriteFile(ZipFile);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "alert('Exported successfully.');", true);
        }
        catch 
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "alert('Unable to export.');", true);
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindRepeater();
    }
    //---------- Release new version
  }

