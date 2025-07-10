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
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing;
using ShipSoft.CrewManager.Operational;
using System.Data.SqlClient;

public partial class Reports_SafetyInsp_Report : System.Web.UI.Page
{
    int intInspId=0, intChildTblId=0;
    int mtmVslId = 0;
    int intLogin_Id;
    string mtmInsName = "", mtmDoneDate = "", mtmPortDone = "";
    //CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        if (Session["loginid"] == null)
        {
            //Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "window.parent.parent.location='../Default.aspx'", true);
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Default.aspx';", true);
        }
        else
        {
            intLogin_Id = Convert.ToInt32(Session["loginid"].ToString());
        }
        lblmessage.Text = "";
        try
        {
            intInspId = int.Parse(Page.Request.QueryString["InspId"].ToString());
        }
        catch { }
        if (!Page.IsPostBack)
        {
            //showreport();
            try
            {
                if (Page.Request.QueryString["MTMPub"].ToString() != "")
                {
                    if (Page.Request.QueryString["MTMVesselId"].ToString() != "")
                        mtmVslId = int.Parse(Page.Request.QueryString["MTMVesselId"].ToString());
                    if (Page.Request.QueryString["MTMInsp"].ToString() != "")
                        mtmInsName = Page.Request.QueryString["MTMInsp"].ToString();
                    if (Page.Request.QueryString["MTMDnDt"].ToString() != "")
                        mtmDoneDate = Page.Request.QueryString["MTMDnDt"].ToString();
                    if (Page.Request.QueryString["MTMPrDone"].ToString() != "")
                        mtmPortDone = Page.Request.QueryString["MTMPrDone"].ToString();
                    Button1_Click(sender, e);
                }
            }
            catch { }
        }
        else
        {
            //showreport();
        }
    }
    //private void showreport()
    //{
    //    string path = "", srnum = "", vslpath = "";
    //    int inspdueid = 0, intvslid = 0;
    //    SafetyInspectionReport.TruncateImages();
    //    DataTable dt10 = SafetyInspectionReport.SelectSafetyInspection_Report(intInspId);
    //    UtilityManager um = new UtilityManager();
    //    foreach (DataRow dr in dt10.Rows)
    //    {
    //        //if (dr["FilePath"].ToString() == "")
    //        //{
    //        //    path = um.DownloadFileFromServer("noimage.jpg", "TR");
    //        //    path = Server.MapPath(path);
    //        //}
    //        //else
    //        //{
    //            path = um.DownloadFileFromServer(dr["FilePath"].ToString(), "TR");
    //            srnum = dr["SrNo"].ToString();
    //            inspdueid = int.Parse(dr["InspectionDueId"].ToString());
    //            intChildTblId = int.Parse(dr["ChildTableId"].ToString());
    //            intvslid = int.Parse(dr["VslId"].ToString());
    //            path = Server.MapPath(path);
    //        //}
    //        try
    //        {
    //            vslpath = um.DownloadFileFromServer(ConfigurationManager.AppSettings["VslImgLink"].ToString() + "vessel_" + intvslid, "VI");
    //            try
    //            {
    //                SafetyInspectionReport.VslImageSave(vslpath);
    //            }
    //            catch
    //            {
    //                vslpath = um.DownloadFileFromServer("noimage.jpg", "NI");
    //                vslpath = Server.MapPath(vslpath);
    //                SafetyInspectionReport.VslImageSave(vslpath);
    //            }
    //            if (dr["FilePath"].ToString() != "")
    //            {
    //                SafetyInspectionReport.ImageSave(inspdueid, srnum, path, intChildTblId);
    //            }
    //        }
    //        catch { }
    //    }
        
    //    DataTable dt = SafetyInspectionReport.SelectSafetyInspectionReportDetails(intInspId);
    //    if (dt.Rows.Count > 0)
    //    {
    //        this.CrystalReportViewer1.Visible = true;
    //        CrystalReportViewer1.ReportSource = rpt;
    //        rpt.Load(Server.MapPath("RPT_SafetyInspection.rpt"));
    //        rpt.SetDataSource(dt);

    //        rpt.SetParameterValue("@Header", dt.Rows[0]["InspName"].ToString() + " - " + dt.Rows[0]["PortPlace"].ToString() + " - " + dt.Rows[0]["InspDontDt"].ToString());

    //        BlobFieldObject p;
    //        int m = 0;
    //        for (m = 0; m < rpt.ReportDefinition.Sections.Count; m++)
    //        {
    //            p = (BlobFieldObject)rpt.ReportDefinition.Sections[m].ReportObjects["ii1"];
    //            //p.Width = 1000;
    //            //p.Height = 1000;
    //        }

    //        BlobFieldObject p1;
    //        int m1 = 0;
    //        for (m1 = 0; m1 < rpt.ReportDefinition.Sections.Count; m1++)
    //        {
    //            p1 = (BlobFieldObject)rpt.ReportDefinition.Sections[m1].ReportObjects["ii12"];
    //            //p1.Width = 1000;
    //            //p1.Height = 1000;
    //        }

    //        BlobFieldObject p2;
    //        int m2 = 0;
    //        for (m2 = 0; m2 < rpt.ReportDefinition.Sections.Count; m2++)
    //        {
    //            p2 = (BlobFieldObject)rpt.ReportDefinition.Sections[m2].ReportObjects["VslImage1"];
    //            //p.Width = 1000;
    //            //p.Height = 1000;
    //        }
    //    }
    //    else
    //    {
    //        lblmessage.Text = "No Record Found.";
    //        this.CrystalReportViewer1.Visible = false;
    //    }
    //}
    //protected void Page_Unload(object sender, EventArgs e)
    //{
    //    rpt.Close();
    //    rpt.Dispose();
    //}
    //private ReportDocument getReportDocument()
    //{
    //    string path = "", srnum = "", vslpath = "";
    //    int inspdueid = 0, intvslid = 0;
    //    SafetyInspectionReport.TruncateImages();
    //    DataTable dt10 = SafetyInspectionReport.SelectSafetyInspection_Report(intInspId);
    //    UtilityManager um = new UtilityManager();
    //    foreach (DataRow dr in dt10.Rows)
    //    {
    //        path = um.DownloadFileFromServer(dr["FilePath"].ToString(), "TR");
    //        srnum = dr["SrNo"].ToString();
    //        inspdueid = int.Parse(dr["InspectionDueId"].ToString());
    //        intChildTblId = int.Parse(dr["ChildTableId"].ToString());
    //        intvslid = int.Parse(dr["VslId"].ToString());
    //        path = Server.MapPath(path);
    //        try
    //        {
    //            vslpath = um.DownloadFileFromServer(ConfigurationManager.AppSettings["VslImgLink"].ToString() + "vessel_" + intvslid, "VI");
    //            try
    //            {
    //                SafetyInspectionReport.VslImageSave(vslpath);
    //            }
    //            catch
    //            {
    //                vslpath = um.DownloadFileFromServer("noimage.jpg", "NI");
    //                vslpath = Server.MapPath(vslpath);
    //                SafetyInspectionReport.VslImageSave(vslpath);
    //            }
    //            if (dr["FilePath"].ToString() != "")
    //            {
    //                SafetyInspectionReport.ImageSave(inspdueid, srnum, path, intChildTblId);
    //            }
    //        }
    //        catch { }
    //    }

    //    DataTable dt = SafetyInspectionReport.SelectSafetyInspectionReportDetails(intInspId);
    //    if (dt.Rows.Count > 0)
    //    {
    //        //this.CrystalReportViewer1.Visible = true;
    //        CrystalReportViewer1.ReportSource = rpt;
    //        rpt.Load(Server.MapPath("RPT_SafetyInspection.rpt"));
    //        rpt.SetDataSource(dt);

    //        rpt.SetParameterValue("@Header", dt.Rows[0]["InspName"].ToString() + " - " + dt.Rows[0]["PortPlace"].ToString() + " - " + dt.Rows[0]["InspDontDt"].ToString());

    //        BlobFieldObject p;
    //        int m = 0;
    //        for (m = 0; m < rpt.ReportDefinition.Sections.Count; m++)
    //        {
    //            p = (BlobFieldObject)rpt.ReportDefinition.Sections[m].ReportObjects["ii1"];
    //        }

    //        BlobFieldObject p1;
    //        int m1 = 0;
    //        for (m1 = 0; m1 < rpt.ReportDefinition.Sections.Count; m1++)
    //        {
    //            p1 = (BlobFieldObject)rpt.ReportDefinition.Sections[m1].ReportObjects["ii12"];
    //        }

    //        BlobFieldObject p2;
    //        int m2 = 0;
    //        for (m2 = 0; m2 < rpt.ReportDefinition.Sections.Count; m2++)
    //        {
    //            p2 = (BlobFieldObject)rpt.ReportDefinition.Sections[m2].ReportObjects["VslImage1"];
    //        }
    //    }
    //    else
    //    {
    //        lblmessage.Text = "No Record Found.";
    //        this.CrystalReportViewer1.Visible = false;
    //    }
    //    return rpt;
    //}
    protected void Button1_Click(object sender, EventArgs e)
    {
        String connString;
        string filename = "";
        //ReportDocument repDoc = getReportDocument();
        Response.Buffer = false;
        Response.ClearContent();
        Response.ClearHeaders();
        try
        {
            filename = System.IO.Path.GetRandomFileName();
            connString = ConfigurationManager.ConnectionStrings["MTMConnString"].ToString();
            SqlConnection myConnection = new SqlConnection(connString);
            SqlCommand myCommand = new SqlCommand("MW_InsertTechnicalInsp", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;
            SqlParameter prmInspDueId = new SqlParameter("@InspDueId", SqlDbType.Int);
            SqlParameter prmVesselId = new SqlParameter("@VesselId", SqlDbType.Int);
            SqlParameter prmInspName = new SqlParameter("@InspName", SqlDbType.VarChar);
            SqlParameter prmDoneDate = new SqlParameter("@DoneDate", SqlDbType.VarChar);
            SqlParameter prmPortDone = new SqlParameter("@PortDone", SqlDbType.VarChar);
            SqlParameter prmFileName = new SqlParameter("@FileName", SqlDbType.VarChar);
            prmInspDueId.Value = intInspId;
            prmVesselId.Value = mtmVslId.ToString();
            prmInspName.Value = mtmInsName;
            prmDoneDate.Value = mtmDoneDate;
            prmPortDone.Value = mtmPortDone;
            prmFileName.Value = filename;
            myCommand.Parameters.Add(prmInspDueId);
            myCommand.Parameters.Add(prmVesselId);
            myCommand.Parameters.Add(prmInspName);
            myCommand.Parameters.Add(prmDoneDate);
            myCommand.Parameters.Add(prmPortDone);
            myCommand.Parameters.Add(prmFileName);
            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();
            string inspno = "";
            int OldId = 0;

            DataTable dt9 = Common.Execute_Procedures_Select_ByQuery("SELECT INSPECTIONNO FROM T_INSPECTIONDUE WHERE ID=" + intInspId.ToString());
            if (dt9.Rows.Count > 0)
            {
                inspno = dt9.Rows[0][0].ToString();
                dt9 = Common.Execute_Procedures_Select_ByQuery("SELECT top 1 ID FROM t_InspectionDocuments WHERE INSPECTIONDUEID=" + intInspId.ToString() + " AND UPPER(LTRIM(RTRIM(DOCUMENTNAME)))='" + inspno.Trim().ToUpper() + "'");
                if (dt9.Rows.Count > 0)
                {
                    OldId = Common.CastAsInt32(dt9.Rows[0][0]);   
                }
            }
            //-----------  UPLOADING FILE
            string strname= inspno.Replace("/","-") + ".pdf";
            string strpath= "~\\EMANAGERBLOB\\Inspection\\Inspection\\" + strname;
            string savepath = "EMANAGERBLOB/Inspection/Inspection/" + strname;
            //if (File.Exists(Server.MapPath(strpath)))
            //{
            //    File.Delete(Server.MapPath(strpath));  
            //}
            //repDoc.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath(strpath));
            //-----------  SAVING IN DOCUMENT
            if (OldId<=0)
            {
                DataTable dt1 = Inspection_Documents.DocumentDetails(intInspId, intLogin_Id, 6, inspno, savepath, intLogin_Id, intLogin_Id, "Add");
                //lblmessage.Text = "Document Added Successfully.";
            }
            else
            {
                DataTable dt1 = Inspection_Documents.DocumentDetails(OldId, intLogin_Id, 6, inspno, savepath, intLogin_Id, intLogin_Id, "MODIFY");
                //lblmessage.Text = "Document Updated Successfully.";
            }
            //----------- 
            //repDoc.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, filename);

            //ScriptManager.RegisterStartupScript(Page, this.GetType(), "a", "alert('Report published successfully.');Window.close();", true);
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "alert('Report published successfully.');window.close();;", true);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            ex = null;
        }
    }
}
