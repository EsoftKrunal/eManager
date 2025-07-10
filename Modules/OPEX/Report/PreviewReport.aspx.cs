using Ionic.Zip;
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class PreviewReport : System.Web.UI.Page
{
    public string ReportMonth = "", printQuery = "", printQuery1 = "", printQuery2="",printQuery5 = "", printQuery6 = "", printQuery7 = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        //---------------------------------------
        SessionManager.SessionCheck_New();
        //---------------------------------------
        lblMessage.Text = "";
        DataTable dt = new DataTable();
        dt = getReportData("M");
        if (dt.Rows.Count > 0)
        {
            rptdata.DataSource = dt;
            rptdata.DataBind();
        }
        dt = null;
        dt = getReportData("Y");
        if (dt.Rows.Count > 0)
        {
            rptYTDReport.DataSource = dt;
            rptYTDReport.DataBind();
        }
        dt = null;
        dt = getReportData("V");
        if (dt.Rows.Count > 0)
        {
            rptVarianceReport.DataSource = dt;
            rptVarianceReport.DataBind();
        }
       
    }

    protected DataTable getReportData(string ReportType)
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("select REPORTID,FLDREPORTTITLE,FLDREPORTSUBTITLE,FLDREPORTTIMEPERIOD,FLDREPORTSHIPNAME,EXCELFILE,ReportType from dbo.tblownerreportsv2 with(nolock) where fldreportcocode='" + Request.QueryString["cocode"] + "' AND FLDREPORTYEAR=" + Request.QueryString["yr"] + " AND FLDREPORTMONTH=" + Request.QueryString["mnth"] + " AND CurFinYear = '" + Request.QueryString["CurFinYear"] + "' AND ReportType = '"+ ReportType + "'");
        return dt;
    }
    protected void btnExcel_Click(object sender, ImageClickEventArgs e)
    {
        //DataSet dSMonth;
        //Common.Set_Procedures("POS_ExportVarianceReport_Account");
        //Common.Set_ParameterLength(4);
        //Common.Set_Parameters(
        //   new MyParameter("@COMPCODE ", Request.QueryString["CoCode"]),
        //   new MyParameter("@MNTH ", Request.QueryString["Period"]),
        //   new MyParameter("@YR ", Request.QueryString["Year"]),
        //   new MyParameter("@VSLCODE ", "EXL")
        //   );
        //dSMonth = Common.Execute_Procedures_Select();
    }
    protected void Report_Click(object sender,EventArgs e) 
    {
        string ReportId = ((Button)sender).CommandArgument;
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("select FLDREPORTTITLE,FLDREPORTSUBTITLE,EXCELFILE,REPORTPDF from dbo.tblownerreportsv2 with(nolock) where REPORTID=" + ReportId);
        string FilePath = Server.MapPath("~\\Modules\\OPEX\\Publish\\Download." + ((dt.Rows[0]["EXCELFILE"].ToString().Trim() == "True") ? "xls" : "pdf"));
        Response.Clear();
        System.IO.File.WriteAllBytes(FilePath, (byte[])dt.Rows[0]["REPORTPDF"]);
        Response.AddHeader("Content-Disposition", "attachment; filename=" + dt.Rows[0]["FLDREPORTTITLE"].ToString().Replace("       ", " ") + "-" + dt.Rows[0]["FLDREPORTSUBTITLE"].ToString() + System.IO.Path.GetExtension(FilePath));
        Response.Clear();  
        Response.WriteFile(FilePath);
        Response.End(); 
    }

    protected void ibDownloadMultipleFile_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = Common.Execute_Procedures_Select_ByQuery("select REPORTID,FLDREPORTTITLE,FLDREPORTSUBTITLE,FLDREPORTTIMEPERIOD,FLDREPORTSHIPNAME,EXCELFILE,ReportType,REPORTPDF from dbo.tblownerreportsv2 with(nolock) where fldreportcocode='" + Request.QueryString["cocode"] + "' AND FLDREPORTYEAR=" + Request.QueryString["yr"] + " AND FLDREPORTMONTH=" + Request.QueryString["mnth"] + " AND CurFinYear = '" + Request.QueryString["CurFinYear"] + "' Order by REPORTID Asc");
            if (dt != null && dt.Rows.Count > 0)
            {
                var downloadFileName = string.Format("{0}.zip", "Published_Report_"+Request.QueryString["cocode"]+"_"+ Request.QueryString["CurFinYear"] +"_"+ DateTime.Now.ToString("yyyy-MM-dd-HH_mm_ss"));
                //Response.ContentType = "application/zip";
                //Response.AddHeader("Content-Disposition", "filename=" + downloadFileName);


                string filePath = string.Empty;
                using (ZipFile zip = new ZipFile())
                {
                    zip.AlternateEncodingUsage = ZipOption.AsNecessary;
                    zip.AddDirectoryByName("Reports");
                    string appname = ConfigurationManager.AppSettings["AppName"].ToString();
                    string[] Files = Directory.GetFiles(Server.MapPath("/" + appname + "/Modules/OPEX/Publish/Reports/"));
                    foreach (string f in Files)
                    {
                        try { File.Delete(f); }
                        catch { }
                    }
                    foreach (DataRow dr in dt.Rows)
                    {
                        string fileName = dr["FLDREPORTTITLE"].ToString().Trim().Replace("       ", "_") + "-" + dr["FLDREPORTSUBTITLE"].ToString().Trim().Replace(" ", "_") + "_" + dr["FLDREPORTSHIPNAME"].ToString().Trim().Replace(" ", "_");
                        filePath = Server.MapPath("~\\Modules\\OPEX\\Publish\\Reports\\"+ fileName + "." + ((dr["EXCELFILE"].ToString().Trim() == "True") ? "xls" : "pdf"));
                        System.IO.File.WriteAllBytes(filePath, (byte[])dr["REPORTPDF"]);
                        //Response.AddHeader("Content-Disposition", "attachment; filename=" + dr["FLDREPORTTITLE"].ToString().Replace("       ", " ") + "-" + dr["FLDREPORTSUBTITLE"].ToString() + System.IO.Path.GetExtension(filePath));
                        zip.AddFile(filePath, "Reports");
                        //Response.Clear();
                    }

                    Response.Clear();
                    Response.BufferOutput = false;
                    string zipName = String.Format("Zip_{0}.zip", downloadFileName);
                    Response.ContentType = "application/zip";
                    Response.AddHeader("content-disposition", "attachment; filename=" + zipName);
                    zip.Save(Response.OutputStream);
                    Response.End();
                }
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message.ToString();
        }
    }

    

   

   
}
