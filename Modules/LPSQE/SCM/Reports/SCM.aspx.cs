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

public partial class Report_SCM : System.Web.UI.Page
{
    public int SCMID
    {
        set { ViewState["SCMID"] = value; }
        get { return Common.CastAsInt32(ViewState["SCMID"]); }
    }
    int intLogin_Id;
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
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
            if (Page.Request.QueryString["SCMID"].ToString() != "")
                SCMID = Common.CastAsInt32(Page.Request.QueryString["SCMID"]);
            
        }
        catch { }
        
        Show_Report();
    }
    private void Show_Report()
    {
        try
        {
            DataSet dtCase = Budget.getTable("Select * from vw_GetSCM_Master Where SCMID=" + SCMID + " ; Select * from SCM_RankDetails Where Absent=0 and SCMID=" + SCMID );

            DataSet dtAbsenteeList = Budget.getTable("Select * from SCM_RankDetails Where Absent=1 and SCMID=" + SCMID );
            dtAbsenteeList.Tables[0].TableName = "vw_SCM_RANKDETAILS";
            
            DataSet dtNCR = Budget.getTable("Select * from SCM_NCRDETAILS Where SCMID=" + SCMID);
            dtNCR.Tables[0].TableName = "SCM_NCRDETAILS";

            string Ocassion = dtCase.Tables[0].Rows[0]["Ocassion"].ToString();

            //------------------------------
            dtCase.Tables.Add( dtAbsenteeList.Tables[0].Copy());
            dtCase.Tables.Add(dtNCR.Tables[0].Copy());

            if (dtCase.Tables[0].Rows.Count > 0)
            {
                if (dtCase != null)
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
                    dtCase.Tables[0].TableName = "vw_GetSCM_Master";
                    dtCase.Tables[1].TableName = "SCM_RANKDETAILS";
                    rpt.SetDataSource(dtCase);

                    if (Ocassion.Contains("SUPTD"))
                    {
                        rpt.Subreports["SCM_SUB_AbsenteeList_SUPTD.rpt"].SetDataSource(dtCase);
                        //rpt.Subreports["SCM_SUBSafety.rpt"].SetDataSource(dtCase);
                        //rpt.Subreports["SCM_SUB_Quality.rpt"].SetDataSource(dtCase);
                    }
                    else
                    {
                        rpt.Subreports["SCM_SUB_AbsenteeList.rpt"].SetDataSource(dtCase);
                        rpt.Subreports["SCM_SUBSafety.rpt"].SetDataSource(dtCase);
                        rpt.Subreports["SCM_SUB_Quality.rpt"].SetDataSource(dtCase);                    
                    }
                    rpt.SetParameterValue("Heading", "SAFETY COMMITTEE MEETING REPORT");
                    rpt.SetParameterValue("ShipPositionLable", dtCase.Tables[0].Rows[0]["ShipPositionLable"].ToString());
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
