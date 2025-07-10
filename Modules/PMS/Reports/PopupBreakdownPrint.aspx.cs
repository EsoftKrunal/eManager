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

public partial class Reports_PopupBreakdownPrint : System.Web.UI.Page
{
    public string ComponentCode
    {
        set { ViewState["CC"] = value; }
        get { return ViewState["CC"].ToString(); }
    }
    public string DefectNo
    {
        set { ViewState["DN"] = value; }
        get { return ViewState["DN"].ToString(); }
    }
    public static DataTable dtSpareDetails;
    protected void Page_Load(object sender, EventArgs e)
    {
        // -------------------------- SESSION CHECK ----------------------------------------------
        ProjectCommon.SessionCheck();
        // -------------------------- SESSION CHECK END ----------------------------------------------
        if (!Page.IsPostBack)
        {
            if (Request.QueryString["DN"] != null)
            {
                DefectNo = Request.QueryString["DN"].ToString();
                BindBreakDownDetails();
            }
            
        }

    }
    public void BindBreakDownDetails()
    {
        string strSQL = "SELECT VesselCode,VDM.ComponentId,CM.ComponentCode,CM.ComponentName,CM.ClassEquip,CM.CriticalEquip,DefectNo,CASE REPLACE(CONVERT(VARCHAR(15), ReportDt,106),' ','-') WHEN '01-Jan-1900' THEN '' ELSE REPLACE(CONVERT(VARCHAR(15), ReportDt,106),' ','-') END  AS ReportDt , CASE REPLACE(CONVERT(VARCHAR(15), TargetDt,106),' ','-') WHEN '01-Jan-1900' THEN '' ELSE  REPLACE(CONVERT(VARCHAR(15), TargetDt,106),' ','-') END AS TargetDt, CASE REPLACE(CONVERT(VARCHAR(15), CompletionDt,106),' ','-') WHEN '01-Jan-1900' THEN '' ELSE REPLACE(CONVERT(VARCHAR(15), CompletionDt,106),' ','-') END AS CompletionDt,Vessel,Spares,ShoreAssist,Drydock,Guarentee,DefectDetails,RepairsDetails,SparesOnBoard,RqnNo,CASE REPLACE(CONVERT(VARCHAR(15),RqnDate,106),' ','-') WHEN '01-Jan-1900' THEN '' ELSE REPLACE(CONVERT(VARCHAR(15),RqnDate,106),' ','-') END AS  RqnDate,SparesOrdered,SparesRequired,Supdt,ChiefOfficer,ChiefEngg,Status,CompStatus " +
                        "FROM Vsl_DefectDetailsMaster VDM INNER JOIN ComponentMaster CM ON CM.ComponentId = VDM.ComponentId WHERE DefectNo = '" + DefectNo + "' ";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(strSQL);
        if (dt.Rows.Count > 0)
        {
            lblNo.Text = dt.Rows[0]["DefectNo"].ToString();
            lblCompCode.Text = dt.Rows[0]["ComponentCode"].ToString();
            lblCompName.Text = dt.Rows[0]["ComponentName"].ToString();
            chkCritical.SelectedValue = dt.Rows[0]["ClassEquip"].ToString();
            chkCritical.SelectedValue = (dt.Rows[0]["CriticalEquip"].ToString() == "Y" ? "1" : "");
            lblReportDt.Text = dt.Rows[0]["ReportDt"].ToString();
            lblTargetDt.Text = dt.Rows[0]["TargetDt"].ToString();
            lblCompletionDt.Text = dt.Rows[0]["CompletionDt"].ToString();
            chkVessel.Checked = (dt.Rows[0]["Vessel"].ToString() == "Y");
            chkSpares.Checked = (dt.Rows[0]["Spares"].ToString() == "Y");
            chkShAssist.Checked = (dt.Rows[0]["ShoreAssist"].ToString() == "Y");
            ChkDrydock.Checked = (dt.Rows[0]["Drydock"].ToString() == "Y");
            chkGuarantee.Checked = (dt.Rows[0]["Guarentee"].ToString() == "Y");
            lblDefectdetails.Text = dt.Rows[0]["DefectDetails"].ToString();
            lblRepairsCarriedout.Text = dt.Rows[0]["RepairsDetails"].ToString();
            lblCompStatus.Text = (dt.Rows[0]["CompStatus"].ToString() == "W" ? "Equipment Working" : "Equipment Not Working");
            lblSOB.Text = (dt.Rows[0]["SparesOnBoard"].ToString() == "Y" ? "Yes" : "No");
            lblRqnNo.Text = dt.Rows[0]["RqnNo"].ToString();
            lblRqnDt.Text = dt.Rows[0]["RqnDate"].ToString();
            lblSparesReqd.Text = (dt.Rows[0]["SparesRequired"].ToString() == "Y" ? "Yes" : "No");
            lblSupdt.Text = dt.Rows[0]["Supdt"].ToString();
            lblCE.Text = dt.Rows[0]["ChiefEngg"].ToString();
            lblCO.Text = dt.Rows[0]["ChiefOfficer"].ToString();
        }
    }
}
