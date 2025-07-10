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
using System.IO;
using Ionic.Zip;

public partial class DryDock_PublishDocket : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();

    protected void Page_Load(object sender, EventArgs e)
    {
        // -------------------------- SESSION CHECK ----------------------------------------------
        ProjectCommon.SessionCheck();
        // -------------------------- SESSION CHECK END ----------------------------------------------
        ShowReport();
    }
    protected void ShowReport()
    {
        //SELECT * FROM DBO.VW_DD_DOCKET_PRINT WHERE COSTCATEGORY='Shipyard Supply Costs'
        //SELECT * FROM DBO.VW_DD_DOCKET_PRINT WHERE COSTCATEGORY='Owner’s Supply Shipyard Costs'
        string oy = "";
        if(Request.QueryString["Cost"]!=null)
            if(Request.QueryString["Cost"]=="Y")
                oy= " AND COSTCATEGORY='Shipyard Supply Costs'";
            else
                oy = " AND COSTCATEGORY='Owner’s Supply Shipyard Costs'";


        DataTable dtReport = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.VW_DD_DOCKET_PRINT WHERE DOCKETID=" + Request.QueryString["DocketId"].ToString() + oy);
        DataTable dtcat = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.DD_JobCategory order by catcode");

        CrystalReportViewer1.ReportSource = rpt;
        rpt.Load(Server.MapPath("PrintDocket.rpt"));
        rpt.SetDataSource(dtReport);
        rpt.Subreports[0].SetDataSource(dtcat);
        if (Request.QueryString["Publish"].ToString() == "Y")
        {
            CrystalReportViewer1.Visible = false;

            if (File.Exists(Server.MapPath("~/Modules/PMS/DryDock/" + "Docket.pdf")))
            {
                File.Delete(Server.MapPath("~/Modules/PMS/DryDock/" + "Docket.pdf"));
            }

            string FileName = Server.MapPath("~/Modules/PMS/DryDock/" + "Docket.pdf");
            rpt.ExportToDisk( CrystalDecisions.Shared.ExportFormatType.PortableDocFormat ,FileName);

            byte[] FileContent = File.ReadAllBytes(FileName);

            Common.Set_Procedures("[dbo].[DD_PublishDocket]");
            Common.Set_ParameterLength(3);
            Common.Set_Parameters(
                new MyParameter("@DocketId", Request.QueryString["DocketId"].ToString()),
                new MyParameter("@PublishedBy", Session["FullName"].ToString()),
                new MyParameter("@Attachment", FileContent)
                );
            DataSet ds = new DataSet();
            ds.Clear();
            Boolean res;
            res = Common.Execute_Procedures_IUD(ds);
            if (res)
            {
                lblMsg.Text = "Published successfully.";
            }
            else
            {
                lblMsg.Text = "Unable to Publish. Error :" + Common.ErrMsg;
            }
        }
        else
        {
            CrystalReportViewer1.Visible = true;
        }

    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
}
