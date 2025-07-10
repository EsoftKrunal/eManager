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

public partial class DryDock_JobTrackingRemarkEntry : System.Web.UI.Page
{
    public int DocketId
    {
        get { return Common.CastAsInt32(ViewState["DocketId"]); }
        set { ViewState["DocketId"] = value; }
    }
    public int RFQId
    {
        get { return Common.CastAsInt32(ViewState["RFQId"]); }
        set { ViewState["RFQId"] = value; }
    }
    public int CatId
    {
        set { ViewState["CatId"] = value; }
        get { return Common.CastAsInt32(ViewState["CatId"]); }
    }
    //public int LoginId
    //{
    //    get { return Common.CastAsInt32(Session["UID"]); }
    //}
    public DateTime ExecFromDate
    {
        get { return Convert.ToDateTime(ViewState["ExecFromDate"]); }
        set { ViewState["ExecFromDate"] = value; }
    }
    public DateTime ExecToDate
    {
        get { return Convert.ToDateTime(ViewState["ExecToDate"]); }
        set { ViewState["ExecToDate"] = value; }
    }
    //-------------------------
    protected void Page_Load(object sender, EventArgs e)
    {   
        lblMsgMain.Text = "";          
        if (!IsPostBack)
        {
            //DocketId = Common.CastAsInt32(Request.QueryString["DocketId"]);
            //RFQId = Common.CastAsInt32(Request.QueryString["RFQId"]);

            try
            {
                //-----------------------------
                DataTable DT_DATA = Common.Execute_Procedures_Select_ByQuery("SELECT D.DOCKETID,D.STATUS AS DOCKETSTATUS,R.RFQID,R.STATUS AS RFQSTATUS FROM DBO.DD_Docket_RFQ_Master R " +
                                                                       "INNER JOIN DBO.[DD_DocketMaster] D ON R.DOCKETID=D.DOCKETID " +
                                                                       "WHERE R.GUID='" + Request.QueryString["key"] + "'");
                //-----------------------------
                if (DT_DATA.Rows.Count > 0)
                {
                    if (DT_DATA.Rows[0]["RFQSTATUS"].ToString() == "P")
                    {
                        if (DT_DATA.Rows[0]["DOCKETSTATUS"].ToString() == "C")
                        {
                            string URL = ConfigurationManager.AppSettings["URL"] + "NOAccess.htm";
                            Response.Redirect(URL, true);
                        }
                        else
                        {
                            DocketId = Common.CastAsInt32(DT_DATA.Rows[0]["DocketId"]);
                            RFQId = Common.CastAsInt32(DT_DATA.Rows[0]["RFQId"]);
                        }
                    }
                    else
                    {
                        string URL = ConfigurationManager.AppSettings["URL"] + "NOAccess.htm";
                        Response.Redirect(URL, true);
                    }
                }
                else
                {
                    string URL = ConfigurationManager.AppSettings["URL"] + "NOAccess.htm";
                    Response.Redirect(URL, true);
                }
                //-----------------------------
            }
            catch
            {
                string URL = ConfigurationManager.AppSettings["URL"] + "NOAccess.htm";
                Response.Redirect(URL, true);
            }


            CatId = Common.CastAsInt32(Request.QueryString["CatId"]);
            txtDate.Text = Request.QueryString["ForDate"];
            ShowDocketSummary();
        }
    }

    public void ShowDocketSummary()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT *,(SELECT VESSELNAME FROM DBO.VESSEL V WHERE V.VESSELID=D.VESSELID) AS VESSELNAME,ISNULL((SELECT R.RFQID FROM DD_Docket_RFQ_Master R WHERE R.DOCKETID=D.DOCKETID AND R.STATUS='P'),0) AS PORFQId,(SELECT ExecFrom FROM DD_Docket_RFQ_Master Where DOCKETID=" + DocketId + " AND RFQId=" + RFQId + ") As ExecFrom,(SELECT ExecTo FROM DD_Docket_RFQ_Master Where DOCKETID=" + DocketId + " AND RFQId=" + RFQId + ") As ExecTo FROM DD_DocketMaster D WHERE DOCKETID=" + DocketId.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            lblDocketNo.Text = dt.Rows[0]["DocketNo"].ToString();
            lblVessel.Text = dt.Rows[0]["VESSELNAME"].ToString();
            lblType.Text = dt.Rows[0]["DocketType"].ToString();
            lblPlanDuration.Text = Common.ToDateString(dt.Rows[0]["ExecFrom"]) + " To " + Common.ToDateString(dt.Rows[0]["ExecTo"]);
            ExecFromDate = Convert.ToDateTime(dt.Rows[0]["ExecFrom"]);
            ExecToDate = Convert.ToDateTime(dt.Rows[0]["ExecTo"]);
        }

        DataTable dtCat = Common.Execute_Procedures_Select_ByQuery("SELECT CatCode, CatName FROM DD_JobCategory Where CatId=" + CatId);
        lblJObCat.Text = " [ " + dtCat.Rows[0]["CatCode"].ToString() + " ] " + dtCat.Rows[0]["CatName"].ToString();

        
        //string SQL = "SELECT [DocketJobId],[JobCode],[JobName],[JobDesc],[ExecPerDate],[ExecPer],[ExecRemarks], " +
        //             "(SELECT DateAdd(dd, 1, getdate())) AS NextPD1,(SELECT DateAdd(dd, 2, getdate())) AS NextPD2,(SELECT DateAdd(dd, 3, getdate())) AS NextPD3, " +
        //             "CASE WHEN EXISTS( SELECT For_Date FROM [dbo].[DD_Docket_RFQ_Jobs_Planning] WHERE [RFQId] = RJ.[RFQId] AND DOCKETID=RJ.DOCKETID AND [DocketJobId]=RJ.[DocketJobId] AND Replace(Convert(varchar, For_Date, 106),' ', '-') = Replace(Convert(varchar, DateAdd(dd, 1, getdate()), 106),' ', '-')) THEN 1 ELSE 0 END AS Checked1, " +
        //             "CASE WHEN EXISTS( SELECT For_Date FROM [dbo].[DD_Docket_RFQ_Jobs_Planning] WHERE [RFQId] = RJ.[RFQId] AND DOCKETID=RJ.DOCKETID AND [DocketJobId]=RJ.[DocketJobId] AND Replace(Convert(varchar, For_Date, 106),' ', '-') = Replace(Convert(varchar, DateAdd(dd, 2, getdate()), 106),' ', '-')) THEN 1 ELSE 0 END AS Checked2, " +
        //             "CASE WHEN EXISTS( SELECT For_Date FROM [dbo].[DD_Docket_RFQ_Jobs_Planning] WHERE [RFQId] = RJ.[RFQId] AND DOCKETID=RJ.DOCKETID AND [DocketJobId]=RJ.[DocketJobId] AND Replace(Convert(varchar, For_Date, 106),' ', '-') = Replace(Convert(varchar, DateAdd(dd, 3, getdate()), 106),' ', '-')) THEN 1 ELSE 0 END AS Checked3  " +
        //             "FROM DD_Docket_RFQ_Jobs RJ WHERE [RFQId] = " + RFQId + " AND [DocketId] = " + DocketId + " AND [CatId] = " + CatId + "  AND [DocketJobId]  IN (" + Session["DocketJobId"].ToString() + ") ORDER BY JobCode";

        //string SQL = "SELECT RJ.[DocketJobId],[JobCode],[JobName],[JobDesc],[ExecPerDate],[ExecPer],[ExecRemarks],[PlanFrom],[PlanTo], JP.[Remark] " +
        //             "FROM DD_Docket_RFQ_Jobs RJ  " +
        //             "LEFT JOIN DD_Docket_RFQ_Jobs_Planning JP ON JP.RfqId = RJ.RFQId AND JP.DocketJobId = RJ.DocketJobId AND JP.DocketId = RJ.DocketId  " +
        //             "WHERE RJ.[RFQId] = " + RFQId + " AND RJ.[DocketId] = " + DocketId + " AND RJ.[CatId] = " + CatId + "  AND RJ.[DocketJobId]  IN (" + Session["DocketJobId"].ToString() + ") ORDER BY JobCode";

        string SQL = "SELECT RJ.[DocketJobId],[JobCode],[JobName],[JobDesc],[ExecPerDate],[ExecPer],[ExecRemarks],[PlanFrom],[PlanTo]," +
                     //"(Select top 1 JP.[Remark] from DD_Docket_RFQ_Jobs_Planning jp where JP.RfqId = RJ.RFQId AND JP.DocketJobId = RJ.DocketJobId AND JP.DocketId = RJ.DocketId order by For_Date desc) as Remark" +
                     "'' as Remark" +
                     " FROM DD_Docket_RFQ_Jobs RJ WHERE RJ.[RFQId] = " + RFQId + " AND RJ.[DocketId] = " + DocketId + " AND RJ.[CatId] = " + CatId + "  AND RJ.[DocketJobId]  IN (" + Session["DocketJobId"].ToString() + ") ORDER BY JobCode";



        DataTable dtRFQJobs = Common.Execute_Procedures_Select_ByQuery(SQL);

        rptRFQJobs.DataSource = dtRFQJobs;
        rptRFQJobs.DataBind();

    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        DateTime ForDate;
        if (!(DateTime.TryParse(txtDate.Text.Trim(), out ForDate)))
        {
            lblMsgMain.Text = "Please enter Date for update.";
            txtDate.Focus();
            return;
        }
        
        //if (!(ForDate == DateTime.Today || ForDate == DateTime.Today.AddDays(-1) || ForDate == DateTime.Today.AddDays(+1)))
        //{
        //    lblMsgMain.Text = "Invalid Date entered.";
        //    txtDate.Focus();
        //    return;
        //}
        //========================================
        foreach (RepeaterItem itm in rptRFQJobs.Items)
        {
            TextBox txtCommencedDate = (TextBox)itm.FindControl("txtCommencedDate");
            TextBox txtEstCompDate = (TextBox)itm.FindControl("txtEstCompDate");

            if (txtCommencedDate.Text.Trim() == "")
            {
                lblMsgMain.Text = "Please enter commenced date.";
                txtCommencedDate.Focus();
                return;
            }
            else
            { 
                DateTime Temp=Convert.ToDateTime(txtCommencedDate.Text);
                if (Temp < ExecFromDate || Temp > ExecToDate)
                {
                    lblMsgMain.Text = "Commenced date must be in DD execution period.";
                    txtCommencedDate.Focus();
                    return;
                }
            }

            if (txtEstCompDate.Text.Trim() == "")
            {
                lblMsgMain.Text = "Please enter est. completion date.";
                txtEstCompDate.Focus();
                return;
            }
            else
            {
                DateTime Temp = Convert.ToDateTime(txtEstCompDate.Text);
                if (Temp < ExecFromDate || Temp > ExecToDate)
                {
                    lblMsgMain.Text = "Completion date must be in DD execution period.";
                    txtCommencedDate.Focus();
                    return;
                }
            }

        }

        try
        {

            foreach (RepeaterItem rpt in rptRFQJobs.Items)
            {
                HiddenField hfDocketjobId = (HiddenField)rpt.FindControl("hfDocketjobId");

                TextBox txtCommencedDate = (TextBox)rpt.FindControl("txtCommencedDate");
                TextBox txtEstCompDate = (TextBox)rpt.FindControl("txtEstCompDate");
                TextBox txtRemarks = (TextBox)rpt.FindControl("txtRemarks");
                CheckBox chk_Select = (CheckBox)rpt.FindControl("chk_Select");
                
               Common.Execute_Procedures_Select_ByQuery("dbo.DD_InsertUpdate_Progress " + RFQId + "," + hfDocketjobId.Value + "," + DocketId + ",'" + ForDate.ToString("dd-MMM-yyyy") + "','','" + txtRemarks.Text.Trim().Replace("'", "`") + "',0");
               Common.Execute_Procedures_Select_ByQuery("UPDATE DD_Docket_RFQ_Jobs SET [PlanFrom]='" + txtCommencedDate.Text.Trim() + "', [PlanTo]='" + txtEstCompDate.Text.Trim() + "', [ExecPer]=" + (chk_Select.Checked ? 100 : 0) + " WHERE [RFQId] = " + RFQId + " AND [DocketId] = " + DocketId + " AND [CatId] = " + CatId + " AND [DocketJobId] = " + hfDocketjobId.Value);
            }

            lblMsgMain.Text = "Saved successfully.";
        }
        catch (Exception ex)
        {
            lblMsgMain.Text = "Unable to save. Error : " + ex.Message;
        }

    }

    public bool EnablePlanningDt(int DocketJobId, string For_Date)
    {
        bool Enable = false;
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM [dbo].[DD_Docket_RFQ_Jobs_Planning] WHERE [RFQId] = " + RFQId + " AND DOCKETID=" + DocketId + " AND [DocketJobId]=" + DocketJobId + " AND DBO.getDatePart(For_Date) = DBO.getDatePart('" + For_Date + "')");

        if (dt != null && dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["Per"].ToString().Trim() == "")
            {
                Enable = true;
            }
        }

        return Enable;
    }
}