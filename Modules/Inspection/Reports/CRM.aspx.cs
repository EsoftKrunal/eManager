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

public partial class CRM : System.Web.UI.Page
{
    DataTable dtResult = new DataTable();
    public int ThreadID
    {
        set { ViewState["ThreadID"] = value; }
        get { return Common.CastAsInt32(ViewState["ThreadID"]); }
    }
    public string StatusID
    {
        set { ViewState["StatusID"] = value; }
        get { return Convert.ToString(ViewState["StatusID"]); }
    }

    int intLogin_Id;
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        lblmessage.Text = "";
        if (Session["loginid"] == null)
        {   Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Default.aspx';", true);  }
        else{    intLogin_Id = Convert.ToInt32(Session["loginid"].ToString());  }

        try
        {
            
            dtResult.Columns.Add("TableId");
            dtResult.Columns.Add("Icon");
            dtResult.Columns.Add("LevelMargin");
            dtResult.Columns.Add("Message");
            dtResult.Columns.Add("UserName");
            dtResult.Columns.Add("OnDate");
            dtResult.Columns.Add("FileVisibility");
            dtResult.Columns.Add("FileName");
            dtResult.Columns.Add("ParentThreadId");


            if (Session["sThreadIDPrint"] != null)
            {
                ThreadID = Common.CastAsInt32(Session["sThreadIDPrint"]);
                ShowDiscussionReport();
            }
            else
            {
                if (Page.Request.QueryString["Status"] != null)
                    StatusID = Convert.ToString(Page.Request.QueryString["Status"]);
                Show_Report();
            }
            
        }
        catch { }
        
    }
    private void Show_Report()
    {
        try
        {
            string sql = Session["sqlCRMDetails"].ToString();
            DataTable DT = Common.Execute_Procedures_Select_ByQuery(sql);
            // ------------------------------------------------------------------------------------------
            if (DT.Rows.Count > 0)
            {
                this.CrystalReportViewer1.Visible = true;
                CrystalReportViewer1.ReportSource = rpt;
                rpt.Load(Server.MapPath("CRM_Threads.rpt"));
                rpt.SetDataSource(DT);

                rpt.SetParameterValue("OwnerOrVessel", Page.Request.QueryString["Owner"].ToString());
                rpt.SetParameterValue("Status", ((StatusID=="O")?" Open ":" Close "));
               
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
    private void ShowDiscussionReport()
    {
        try
        {
            DataRow Drm;
            string sql = "Select Replace(Convert(varchar,CreatedOn,106),' ','-')CreatedOnTxt,Replace(Convert(varchar,DueDate,106),' ','-')DueDateTxt,Replace(Convert(varchar,ClosureDate,106),' ','-')ClosureDateTxt,* from dbo.vw_CRM_ThreadMaster where ThreadID=" + ThreadID.ToString() + "";
            DataTable DTMaster = Common.Execute_Procedures_Select_ByQuery(sql);
            if (DTMaster.Rows.Count > 0)
            {
                Drm = DTMaster.Rows[0];

                DataTable DT = ShowDiscussion();
                // ------------------------------------------------------------------------------------------
                if (DT.Rows.Count > 0)
                {
                    this.CrystalReportViewer1.Visible = true;
                    CrystalReportViewer1.ReportSource = rpt;
                    rpt.Load(Server.MapPath("CRM_ThreadDiscussion.rpt"));
                    rpt.SetDataSource(DT);

                    rpt.SetParameterValue("Vessel", Drm["VesselName"].ToString());
                    rpt.SetParameterValue("Origin", Drm["OriginName"].ToString());
                    rpt.SetParameterValue("ToDo", Drm["ToDo"].ToString());
                    rpt.SetParameterValue("StartDate", Drm["CreatedOnTxt"].ToString());
                    rpt.SetParameterValue("DueDate", Drm["DueDateTxt"].ToString());
                    rpt.SetParameterValue("ClosureDate", Drm["ClosureDateTxt"].ToString());
                    rpt.SetParameterValue("Status", Drm["Status"].ToString());
                    rpt.SetParameterValue("ClosedBy", Drm["ClosureBy"].ToString());
                    rpt.SetParameterValue("ClosureRemarks", Drm["ClosureRemarks"].ToString());
                    rpt.SetParameterValue("Owner", Drm["OwnerName"].ToString());

                }
                else
                {
                    lblmessage.Text = "No Data Found";
                    this.CrystalReportViewer1.Visible = false;
                }
            }

        }
        catch (Exception ex)
        {
            lblmessage.Text = "Error  -  " + ex.Message;
        }
    }

    public DataTable ShowDiscussion()
    {
        int Level = -1;
        string sql = "select * " +
                    " ,(Case when (Select FileName from  DBO.CRM_ThreadAttachments  T Where T.TableID=TD.TableID) is null Then 'False' else 'True' end)FileVisibility" +
                    " ,(Select FileName from  DBO.CRM_ThreadAttachments  T Where T.TableID=TD.TableID) FileName" +
                    " from DBO.crm_threaddetails TD where threadid=" + ThreadID.ToString() + " AND parentthreadid=0";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
        foreach (DataRow dr in dt.Rows)
        {
            DataRow dr1 = dtResult.NewRow();
            dr1["TableId"] = dr["TableId"].ToString();
            dr1["Icon"] = "add_16.gif";
            dr1["LevelMargin"] = Convert.ToString(((Level + 1) * 10) + 10) + "px";
            dr1["Message"] = dr["Details"].ToString();
            dr1["UserName"] = dr["EnteredBy"].ToString();
            dr1["OnDate"] = Common.ToDateString(dr["EnteredOn"]);
            dr1["FileVisibility"] = dr["FileVisibility"].ToString();
            dr1["FileName"] = dr["FileName"].ToString();
            dr1["ParentThreadId"] = dr["ParentThreadId"].ToString();

            dtResult.Rows.Add(dr1);
            LoadResult(ThreadID.ToString(), dr["TableId"].ToString(), Level + 1);
        }

        return dtResult;
    }
    protected void LoadResult(string ThreadId, string ParentThreadId, int Level)
    {
        string sql = "select * " +
                    " ,(Case when (Select FileName from  DBO.CRM_ThreadAttachments  T Where T.TableID=TD.TableID) is null Then 'False' else 'True' end)FileVisibility" +
                    " ,(Select FileName from  DBO.CRM_ThreadAttachments  T Where T.TableID=TD.TableID) FileName" +
                    " from DBO.crm_threaddetails TD where threadid=" + ThreadId.ToString() + " AND parentthreadid=" + ParentThreadId;

        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
        foreach (DataRow dr in dt.Rows)
        {
            DataRow dr1 = dtResult.NewRow();
            dr1["TableId"] = dr["TableId"].ToString();
            dr1["Icon"] = "add_16.gif";
            dr1["LevelMargin"] = Convert.ToString(((Level + 1) * 10) + 10) + "px";
            dr1["Message"] = dr["Details"].ToString();
            dr1["UserName"] = dr["EnteredBy"].ToString();
            dr1["OnDate"] = Common.ToDateString(dr["EnteredOn"]);
            dr1["FileVisibility"] = dr["FileVisibility"].ToString();
            dr1["FileName"] = dr["FileName"].ToString();
            dr1["ParentThreadId"] = dr["ParentThreadId"].ToString();

            dtResult.Rows.Add(dr1);
            LoadResult(ThreadId.ToString(), dr["TableId"].ToString(), Level + 1);
        }
    }

    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
}
