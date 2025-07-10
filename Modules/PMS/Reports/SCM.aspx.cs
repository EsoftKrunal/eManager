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

public partial class Reports_SCM : System.Web.UI.Page
{
    public int ReportsPk
    {
        get { return Common.CastAsInt32(ViewState["ReportsPk"]); }
        set { ViewState["ReportsPk"] = value; }
    }

    public string CurrentVessel
    {
        get { return ViewState["CurrentVessel"].ToString(); }
        set { ViewState["CurrentVessel"] = value; }
    }
    
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        

        lblmessage.Text = "";
        try
        {
            ReportsPk = Common.CastAsInt32(Page.Request.QueryString["pk"]);
            CurrentVessel = Session["CurrentShip"].ToString();
            
        }
        catch { }
        
        Show_Report();
    }
    private void Show_Report()
    {
        try
        {
            DataSet ds = new DataSet();
            DataTable dtCase = Common.Execute_Procedures_Select_ByQuery("Select * from dbo.vw_GetSCM_Master Where ReportsPk=" + ReportsPk + " AND VesselCode='" + CurrentVessel + "' ");
            dtCase.TableName = "vw_GetSCM_Master";

            DataTable dtPresentList = Common.Execute_Procedures_Select_ByQuery("Select * from dbo.SCM_RankDetails Where Absent=0 and ReportsPk=" + ReportsPk + " AND VesselCode='" + CurrentVessel + "'");
            dtPresentList.TableName = "SCM_RANKDETAILS";

            DataTable dtAbsenteeList = Common.Execute_Procedures_Select_ByQuery("Select * from dbo.vw_SCM_RankDetails Where Absent=1 and ReportsPk=" + ReportsPk + " AND VesselCode='" + CurrentVessel + "' ");
            dtAbsenteeList.TableName = "vw_SCM_RANKDETAILS";

            DataTable dtNCR = Common.Execute_Procedures_Select_ByQuery("Select * from dbo.SCM_NCRDETAILS Where ReportsPk=" + ReportsPk + " AND VesselCode='" + CurrentVessel + "' ");
            dtNCR.TableName = "SCM_NCRDETAILS";

            string Ocassion = dtCase.Rows[0]["Ocassion"].ToString();

            //------------------------------
            ds.Tables.Add(dtCase.Copy());
            ds.Tables.Add(dtPresentList.Copy());
            ds.Tables.Add(dtAbsenteeList.Copy());
            ds.Tables.Add(dtNCR.Copy());

            DataSet ds1 = new DataSet();

            ds1.Tables.Add(dtCase.Copy());
            ds1.Tables.Add(dtAbsenteeList.Copy());

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds != null)
                {
                    this.CrystalReportViewer1.Visible = true;
                    CrystalReportViewer1.ReportSource = rpt;

                    if (Ocassion.Contains("SUPTD"))
                    {
                        rpt.Load(Server.MapPath("SCM_SUPTD.rpt"));
                    }
                    else
                    {
                        rpt.Load(Server.MapPath("SCM.rpt"));
                    }
                    
                    rpt.SetDataSource(ds);

                    if (Ocassion.Contains("SUPTD"))
                    {
                        rpt.Subreports["SCM_SUB_AbsenteeList_SUPTD.rpt"].SetDataSource(ds1);
                    }
                    else
                    {
                        rpt.Subreports["SCM_SUB_AbsenteeList.rpt"].SetDataSource(ds1);
                        rpt.Subreports["SCM_SUBSafety.rpt"].SetDataSource(ds);
                        rpt.Subreports["SCM_SUB_Quality.rpt"].SetDataSource(ds);                    
                    }
                    rpt.SetParameterValue("Heading", "SAFETY COMMITTEE MEETING REPORT");
                    rpt.SetParameterValue("ShipPositionLable", ds.Tables[0].Rows[0]["ShipPositionLable"].ToString());
                }
            }
            else
            {
                lblmessage.Text = "No Data Found";
                this.CrystalReportViewer1.Visible = false;
            }
        }
        catch (Exception ex)
        {
            lblmessage.Text = "Error  -  " + ex.Message;
        }
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
}
