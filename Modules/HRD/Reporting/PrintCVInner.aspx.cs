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

public partial class PrintCVInner : System.Web.UI.Page
{
    int crewid = 0;
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        CrystalReportViewer1.HasPrintButton = Alerts.Check_ReportPrintingAuthority(Convert.ToInt32(Session["loginid"].ToString()), 82); 
        //==========
        try
        {
            crewid = Convert.ToInt32(Request.QueryString["CrewID"]);
            if (crewid>0)
            {
                PrintCrewCV(crewid);
            }
        }
        catch
        {
        }
    }
    
    private void PrintCrewCV(int crewid)
    {
        Session["crewnew"] = crewid;
        if (crewid != 0)
        {
            string path = "";
            DataTable dt = ReportPrintCV.selectCVDetails(crewid);
            UtilityManager um = new UtilityManager();

            foreach (DataRow dr in dt.Rows)
            {
                if (dr["PhotoPath"].ToString() == "")
                {
                    path = um.DownloadFileFromServer("noImage.jpg", "C");
                    path = Server.MapPath(path);
                }
                else
                {
                    path = um.DownloadFileFromServer(dr["PhotoPath"].ToString(), "C");
                    path = Server.MapPath(path);
                }
            }
            try
            {
                ReportPrintCV.ImageSave(path,Server.MapPath("~/EMANAGERBLOB/HRD/CrewPhotos/noImage.jpg"));
            }
            catch
            { 

            }
            DataTable dtsub1 = ReportPrintCV.selectCVEducationalDetails(crewid);
            DataTable dtsub2 = ReportPrintCV.selectCVLicencesDetails(crewid);
            DataTable dtsub5 = ReportPrintCV.selectCVDCEDetails(crewid);
            DataTable dtsub3 = ReportPrintCV.selectCVCourseDetails(crewid);
            DataTable dtsub4 = ReportPrintCV.selectCVExperienceDetails(crewid);

            DataTable dt10 = ReportPrintCV.selectCVDetails(crewid);
            DataTable dt1 = PrintCrewList.selectCompanyDetails();
            CrystalReportViewer1.ReportSource = rpt;
            rpt.Load(Server.MapPath("CrystalReportPrintCV.rpt"));
            rpt.SetDataSource(dt10);

            BlobFieldObject p;
            p = (BlobFieldObject)rpt.ReportDefinition.Sections[0].ReportObjects["ii1"];
            p.Width = 900;
            p.Height = 1000;

            rpt.Refresh();
            this.CrystalReportViewer1.Visible = true;
            CrystalDecisions.CrystalReports.Engine.ReportDocument rptsub1 = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            rptsub1 = rpt.OpenSubreport("CrystalReportEducation.rpt");
            rptsub1.SetDataSource(dtsub1);

            CrystalDecisions.CrystalReports.Engine.ReportDocument rptsub2 = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            rptsub2 = rpt.OpenSubreport("CrystalReportLicense.rpt");
            rptsub2.SetDataSource(dtsub2);

            CrystalDecisions.CrystalReports.Engine.ReportDocument rptsub5 = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            rptsub5 = rpt.OpenSubreport("CrystalReportDCE.rpt");
            rptsub5.SetDataSource(dtsub5);

            CrystalDecisions.CrystalReports.Engine.ReportDocument rptsub3 = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            rptsub3 = rpt.OpenSubreport("CrystalReportCourse.rpt");
            rptsub3.SetDataSource(dtsub3);

            CrystalDecisions.CrystalReports.Engine.ReportDocument rptsub4 = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            rptsub4 = rpt.OpenSubreport("CrystalReportExperience.rpt");
            rptsub4.SetDataSource(dtsub4);


            foreach (DataRow dr in dt1.Rows)
            {
                rpt.SetParameterValue("@Company", dr["CompanyName"].ToString());
            }
            
        }
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose(); 
    }
}