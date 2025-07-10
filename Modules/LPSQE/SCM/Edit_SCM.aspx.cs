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
using System.Data.SqlClient;
using System.Xml;
using ShipSoft.CrewManager.BusinessLogicLayer;
using ShipSoft.CrewManager.BusinessObjects;
using ShipSoft.CrewManager.Operational;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

public partial class HSSQE_Edit_SCM : System.Web.UI.Page
{
    public Authority Auth;
    public int SCMID
    {
        set { ViewState["SCMID"] = value; }
        get { return int.Parse("0" + ViewState["SCMID"]); }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------

        if (Page.Request.QueryString["SCMID"] != null)
            SCMID = Common.CastAsInt32(Page.Request.QueryString["SCMID"].ToString());
        else
            return;

        if (!Page.IsPostBack)
        {
            DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT [ReportsPK],[VesselCode],[Ocassion]  FROM [dbo].[SCM_MASTER] WHERE [SCMId] = " + SCMID);

            if (dt.Rows.Count > 0)
            {
                if (!Convert.IsDBNull(dt.Rows[0]["ReportsPK"]))
                {
                    int ReportsPk = Common.CastAsInt32(dt.Rows[0]["ReportsPK"]);
                    string VesselCode = dt.Rows[0]["VesselCode"].ToString();
                    string Ocassion = dt.Rows[0]["Ocassion"].ToString();

                    if (Ocassion.Trim() == "S")
                    {
                        Response.Redirect("SCM_SupdtVisit.aspx?VC=" + VesselCode + "&Mode=A&pk=" + ReportsPk);
                    }
                    else
                    {
                        Response.Redirect("SCM_View.aspx?VC=" + VesselCode + "&Mode=V&pk=" + ReportsPk);
                    }
                }
            }

            ShowSCMData();
        }
    }

    protected void btnSaveOffComments_OnClick(object sender, EventArgs e)
    {

        try
        {
            if (txtOfficeComments.Text.Trim() == "")
            {
                lblCommentsMSG.Text = "Please enter comments.";
                lblCommentsMSG.Focus(); return;
            }

            string sql = "Update SCM_Master Set OfficeComments='" + txtOfficeComments.Text.Trim().Replace("'", "`") + "',UpdatedBy='" + Session["UserName"].ToString() + "',UpdatedOn='" + DateTime.Today.ToString("dd-MMM-yyyy") + "' where SCMID=" + SCMID + "";
            DataTable DT = Common.Execute_Procedures_Select_ByQuery(sql);
            lblCommentsMSG.Text = "Comments update successfully.";
            ShowSCMData();
        }
        catch (Exception ex)
        {
            lblCommentsMSG.Text = "Comments could not be updated.";
        }
    }
    protected void btnPrint_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("~/Reports/SCM.aspx?SCMID=" + SCMID.ToString() + "");
    }
    // ------------ Function
    public void ShowSCMData()
    {
        //OutStandingItems 
        string sql = "select *,Year(SDate )SCMYear ,upper(VesselCode +'-'+right( Replace(convert(varchar,SDate ,106),' ','-'),8))SCMNo,replace(convert(varchar, SDate,106),' ','-') SDate1,(case Ocassion when 'M' then 'Monthly' else 'SUPTD Visit' end ) OccasionName " +
                " ,(select VesselName from dbo.Vessel V where V.VesselCode=SCM_Master.VesselCode)VesselName " +
                " ,(Case ShipPosition when 'S' then 'At Sea' else 'In Port' end)ShipPosition1 ,UpdatedBy +' / '+ Replace( convert(varchar,UpdatedOn,106),' ','-') UpdateByOn " +
                " from dbo.SCM_Master  where SCMID=" + SCMID;
        DataTable DT = Common.Execute_Procedures_Select_ByQuery(sql);
        if (DT.Rows.Count > 0)
        {
            DataRow Dr = DT.Rows[0];


            lblNANo.Text = Dr["SCMNo"].ToString();
            lblSCMYear.Text = Dr["SCMYear"].ToString();


            lblVessel.Text = Dr["VesselCode"].ToString();
            lblVesselName.Text = Dr["VesselName"].ToString();
            lblOccasion.Text = Dr["OccasionName"].ToString();
            if (Dr["OccasionName"].ToString().ToUpper() == "SUPTD VISIT")
            {
                TabPanel1.Visible = false;
                TabPanel2.Visible = false;
                TabPanel3.Visible = false;
                TabPanel4.Visible = false;
                TabPanel5.Visible = false;
                TabPanel6.Visible = false;
                TabPanel7.Visible = false;
                TabPanel8.Visible = false;
                TabPanel10.Visible = true;
            }
            else
            {
                TabPanel1.Visible = true;
                TabPanel2.Visible = true;
                TabPanel3.Visible = true;
                TabPanel4.Visible = true;
                TabPanel5.Visible = true;
                TabPanel6.Visible = true;
                TabPanel7.Visible = true;
                TabPanel8.Visible = true;
                TabPanel10.Visible = false;
            }
            lblDate.Text = Dr["SDate1"].ToString();
            lblTimeCommenced.Text = Dr["CommTime"].ToString();

            lblShipPosition.Text = Dr["ShipPosition1"].ToString();
            if (Dr["ShipPosition"].ToString() == "S")
            {
                lblPlaceLabel.Text = "Voy From/To ";

                lblShipFromTo.Text = Dr["ShipPosFrom"].ToString() + " To " + Dr["ShipPosTo"].ToString();

            }
            else
            {
                lblPlaceLabel.Text = "Port Anchorage ";
                lblShipFromTo.Text = Dr["ShipPosFrom"].ToString();
            }

            lblTimeCompleted.Text = Dr["CompTime"].ToString();


            lblMinutesOfPreviousSAFECOM.Text = (Dr["Str_MinOfPrevSaFeCom"].ToString() == "True") ? "Yes" : "No";
            lblAbsenteesInPreviousSAFECOM.Text = (Dr["Str_AbsPrevSageCom"].ToString() == "True") ? "Yes" : "No";
            lblOfficeCommentsToPreviousSAFECOM.Text = (Dr["Str_OffCommPrevSafeCom"].ToString() == "True") ? "Yes" : "No";

            lblOutStandingItemyesNo.Text = (Dr["OutStandingItems"].ToString() == "") ? "No" : "Yes";
            txtOutStandingItems.Text = Dr["OutStandingItems"].ToString();


            lblCrewAvailableOnBoard.Text = Dr["Sef_AvailableOnBoard"].ToString();
            lblCrewAvailableOnBoardYesNo.Text = ((Dr["Sef_AvailableOnBoard"].ToString().Trim() == "") ? "Yes" : "No");
            lblCrewfamilierWithAll.Text = Dr["Sef_CrewNotFamilierWithAll"].ToString();
            lblCrewfamilierWithAllYesNo.Text = ((Dr["Sef_CrewNotFamilierWithAll"].ToString().Trim() == "") ? "Yes" : "No");



            lblAccidentNearMiss.Text = Dr["Sef_AccidentNarMiss"].ToString();
            lblMooring.Text = Dr["Sef_ReviewOfMooring"].ToString();
            lblBestPracticeSafety.Text = Dr["Sef_BestPractice"].ToString();

            // Health
            lblReviewHealth.Text = Dr["Hel_ReviewHelth"].ToString();
            lblBestPracticeHealth.Text = Dr["Hel_BestPractice"].ToString();

            // Security
            lblReviewSecurity.Text = Dr["Sec_ReviewAnyImmediateSecurity"].ToString();
            lblBestPracticeSecurity.Text = Dr["Sec_BestPractices"].ToString();

            // Quality
            lblReviewOfRegulatory.Text = Dr["Qul_ReviewOfRegulatory"].ToString();
            lblReviewOfQuality.Text = Dr["Qul_ReviewOfQuality"].ToString();
            lblReviewQualityKPI.Text = Dr["Qul_KPI"].ToString();
            lblReviewCrewWelfare.Text = Dr["Qul_CrewWelfare"].ToString();
            lblBestPracticeQuality.Text = Dr["Qul_BestPractices"].ToString();

            // Environment
            lblReviewEnvironmentalKPI.Text = Dr["Env_EnvironmentalKPIs"].ToString();
            lblBestPracticesEnvironment.Text = Dr["Env_BestPractices"].ToString();

            // AOB
            lblAnyOtherIssues.Text = Dr["AnyOtherIssues"].ToString();


            if (Dr["UpdatedBy"].ToString() != "")
            {
                tblUpdatedbyOn.Visible = true;
                tblupdatePannel.Visible = false;
                txtOfficeComments.ReadOnly = true;

                lblUpdatedByOn.Text = Dr["UpdateByOn"].ToString();
                txtOfficeComments.Text = Dr["OfficeComments"].ToString();
            }
            else
            {
                tblUpdatedbyOn.Visible = false;
                tblupdatePannel.Visible = true;
                txtOfficeComments.ReadOnly = false;
            }
            // SUPTD ------------------------------------------------
            txtSUPTD_CompliancewithRegulations.Text = Dr["SUPTD_CompliancewithRegulations"].ToString();
            txtSUPTD_DeviationsfromSafety.Text = Dr["SUPTD_DeviationsfromSafety"].ToString();
            txtSUPTD_DetailsofTraining.Text = Dr["SUPTD_DetailsofTraining"].ToString();
            txtSUPTD_HealthSafetyMeasures.Text = Dr["SUPTD_HealthSafetyMeasures"].ToString();
            txtSUPTD_Suggestions.Text = Dr["SUPTD_Suggestions"].ToString();
            txtSUPTD_BPI.Text = Dr["SUPTD_BPI"].ToString();
            txtSUPTD_AnyOtherTopic.Text = Dr["SUPTD_AnyOtherTopic"].ToString();
        }

        sql = "select RankName,Name,Remarks  from dbo.SCM_rankdetails where SCMID=" + SCMID + " and Absent=0 ";
        rptAttendeeRank.DataSource = Common.Execute_Procedures_Select_ByQuery(sql);
        rptAttendeeRank.DataBind();

        sql = "select RankName,Name from dbo.SCM_rankdetails where SCMID=" + SCMID + " and Absent=1 ";
        rptAbsenteeRank.DataSource = Common.Execute_Procedures_Select_ByQuery(sql);
        rptAbsenteeRank.DataBind();

        //rpt_Notify.DataSource = Common.Execute_Procedures_Select_VIMS_ByQuery("select * from dbo.SCM_Notificationdetails where SCMID=" + SCMID + "");
        //rpt_Notify.DataBind();

        sql = "SELECT * FROM dbo.SCM_SAFETY WHERE SCMID=" + SCMID + "";
        rpt_Safety.DataSource = Common.Execute_Procedures_Select_ByQuery(sql);//"select * from dbo.SCM_incidentdetails where SCMID=" + SCMID + ""
        rpt_Safety.DataBind();

        rptNCR.DataSource = Common.Execute_Procedures_Select_ByQuery("select Number,replace(convert(varchar,Cdate,106),' ','-')Cdate,Remarks from dbo.SCM_NCRDETAILS where SCMID=" + SCMID + "");
        rptNCR.DataBind();

    }
    protected void btnModify_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("~/HSSQE/Add_SCM.aspx?SCMID=" + SCMID.ToString() + "");
    }
}

