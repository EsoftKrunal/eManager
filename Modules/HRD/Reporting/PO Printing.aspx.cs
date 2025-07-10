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

public partial class Reporting_PO_Printing : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        ////***********Code to check page acessing Permission
        //int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 114);
        //if (chpageauth <= 0)
        //{
        //    Response.Redirect("DummyReport.aspx");

        //}
        ////*******************
        ////========== Code to check report printing authority
        //DataTable dtcheck = Alerts.Check_ReportPrintingAuthority(Convert.ToInt32(Session["loginid"].ToString()));
        //foreach (DataRow dr in dtcheck.Rows)
        //{
        //    if (Convert.ToInt32(dr[0].ToString()) > 0)
        //    {
        //        CrystalReportViewer1.HasPrintButton = false;
        //    }
        //}
        ////==========
        this.lblmessage.Text = "";
        show_report();
    }
    private void show_report()
    {
        DataSet ds = new DataSet();
        DataTable dt1 = PrintPO.selectPOHeader(Convert.ToInt32(Session["POId_Print"].ToString()));
        DataTable dt2 = PrintPO.selectPODetails(Convert.ToInt32(Session["POId_Print"].ToString()), Convert.ToInt32(Session["VendorId_Print"].ToString()), Convert.ToInt32(Session["VesselId_Print"].ToString()), Convert.ToDateTime(Session["PoDate_Print"].ToString()));
        DataTable dt3 = PrintPO.getCrewDetails(Convert.ToInt32(Session["POId_Print"].ToString()));
        DataTable dt21 = PrintCrewList.selectCompanyDetails();
        foreach (DataRow dr in dt1.Rows)
        {
            if (dt1.Rows.Count > 0)
            {
                this.CrystalReportViewer1.Visible = true;
                rpt.Load(Server.MapPath("PrintPO.rpt"));
                dt1.TableName = "Print_Po_Header;1";
                dt2.TableName = "Print_PO_Details;1";
                ds.Tables.Add(dt1);
                ds.Tables.Add(dt2);
                rpt.SetDataSource(ds);
                CrystalReportViewer1.ReportSource = rpt;

                CrystalDecisions.CrystalReports.Engine.ReportDocument rptsub3 = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                rptsub3 = rpt.OpenSubreport("CrewDetails_PO.rpt");
                rptsub3.SetDataSource(dt3);
                int cc = dt3.Rows.Count;
                
                if (dr["VendorType"].ToString()=="Travel")
                {
                    rpt.SetParameterValue("@Header","Travel Booking -- Route " + dr["FromAirport"].ToString() + " To " + dr["ToAirport"].ToString() + " -- Total No. of Crew : " + cc);
                }
                else
                {
                    rpt.SetParameterValue("@Header", "Port Agency -- " + dr["CompanyName"].ToString() + " -- Total No. of Crew : " + cc);
                }

                foreach (DataRow dr1 in dt21.Rows)
                {
                    rpt.SetParameterValue("@Company", dr1["CompanyName"].ToString());
                }
            }
            else
            {
                this.CrystalReportViewer1.Visible = false;
                lblmessage.Text = "No Record Found.";
            }
        }
        
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
    protected void btn_show_Click(object sender, EventArgs e)
    {

    }
}
