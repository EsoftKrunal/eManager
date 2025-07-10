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
using System.IO;
using System.Text;


public partial class DD_OFC_Docket : System.Web.UI.Page
{
    #region -------- PROPERTIES ------------------
    public int DocketId
    {
        set { ViewState["DocketId"] = value; }
        get { return Common.CastAsInt32(ViewState["DocketId"]); }
    }
    public int RFQId
    {
        set { ViewState["RFQId"] = value; }
        get { return Common.CastAsInt32(ViewState["RFQId"]); }
    }
    public int LoginId
    {
        get { return Common.CastAsInt32(Session["loginid"]); }
    }
    
    #endregion -----------------------------------
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        lblMsg_Closure.Text = "";
        lblMsg_Exec.Text = "";
        lblMsg_Mail.Text = "";
        // -------------------------- SESSION CHECK ----------------------------------------------
        ProjectCommon.SessionCheck();
        // -------------------------- SESSION CHECK END ----------------------------------------------
        if (!IsPostBack)
        {
            Session["DDPageId"] = 2;
            Session["DDPageId"] = "DD_OFC_Docket";
            BindYear();
            BindFleet();
            BindVessel();
            BindDockets();
        }
    }
    protected void ddlFleet_Search_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        BindVessel();
    }
    protected void btnAction_Click(object sender, EventArgs e)
    {
        string Action = txtAction.Text.Trim();
        if (Action == "PLAN")
            btnAddDocket_Click(sender, e);
        if (Action == "US")
            btnUpdateSpecification_Click(sender, e);
        if (Action == "YC")
            btnYardConfirmation_Click(sender, e);
        if (Action == "JT")
            btnJobTracking_Click(sender, e);
        if (Action == "CT")
            btnCostTracking_Click(sender, e);
        if (Action == "DC")
            btnDocuments_Click(sender, e);
        if (Action == "DR")
            btnReports_Click(sender, e);
        if (Action == "CD")
            btnClosure_Click(sender, e);
        if (Action == "EX")
            btnExecute_Click(sender, e);
        if (Action == "DM")
            btnDocketMail_Click(sender, e);
    }
    
    private void BindVessel()
    {

        DataTable dt;
        if(ddlFleet_Search.SelectedIndex<=0)
            dt= Common.Execute_Procedures_Select_ByQuery("SELECT VESSELID,VESSELNAME FROM DBO.VESSEL WHERE VESSELSTATUSID=1  ORDER BY VESSELNAME");
        else
            dt = Common.Execute_Procedures_Select_ByQuery("SELECT VESSELID,VESSELNAME FROM DBO.VESSEL WHERE VESSELSTATUSID=1 AND FLEETID=" + ddlFleet_Search.SelectedValue + " ORDER BY VESSELNAME");

        ddlVessel.DataSource = dt;
        ddlVessel.DataTextField = "VesselName";
        ddlVessel.DataValueField = "VesselId";
        ddlVessel.DataBind();
        ddlVessel.Items.Insert(0,new ListItem("Select", ""));

        ddlVessel_Search.DataSource = dt;
        ddlVessel_Search.DataTextField = "VesselName";
        ddlVessel_Search.DataValueField = "VesselId";
        ddlVessel_Search.DataBind();
        ddlVessel_Search.Items.Insert(0, new ListItem("All", ""));
    }
    private void BindFleet()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.FLEETMASTER");
        ddlFleet_Search.DataSource = dt;
        ddlFleet_Search.DataTextField = "FleetName";
        ddlFleet_Search.DataValueField = "FleetId";
        ddlFleet_Search.DataBind();
        ddlFleet_Search.Items.Insert(0, new ListItem("All", ""));
    }
    private void BindYear()
    {
	   ddlYear.Items.Add(new ListItem(" All ", "0"));
        for (int i = 2014; i <= DateTime.Today.Year+3; i++)
        {
            ddlYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
        }
        ddlYear.SelectedValue = DateTime.Today.Year.ToString();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        //LoadDockets();
        BindDockets();
    }
    //protected void LoadDockets()
    //{
    //    DataTable dt = new DataTable();
    //    string strSQL = "SELECT VesselId, VesselCode, VesselName, " +
    //                    "( SELECT TOP 1 [Status] FROM  [DD_DocketMaster] WHERE VesselId = V.VesselId AND Month([Startdate]) = 1 AND Year([Startdate]) = " + ddlYear.SelectedValue.Trim() + " ORDER BY [Startdate] Desc ) AS Jan, " +
    //                    "( SELECT TOP 1 [Status] FROM  [DD_DocketMaster] WHERE VesselId = V.VesselId AND Month([Startdate]) = 2 AND Year([Startdate]) = " + ddlYear.SelectedValue.Trim() + " ORDER BY [Startdate] Desc) AS Feb, " +
    //                    "( SELECT TOP 1 [Status] FROM  [DD_DocketMaster] WHERE VesselId = V.VesselId AND Month([Startdate]) = 3 AND Year([Startdate]) = " + ddlYear.SelectedValue.Trim() + " ORDER BY [Startdate] Desc) AS Mar, " +
    //                    "( SELECT TOP 1 [Status] FROM  [DD_DocketMaster] WHERE VesselId = V.VesselId AND Month([Startdate]) = 4 AND Year([Startdate]) = " + ddlYear.SelectedValue.Trim() + " ORDER BY [Startdate] Desc) AS Apr, " +
    //                    "( SELECT TOP 1 [Status] FROM  [DD_DocketMaster] WHERE VesselId = V.VesselId AND Month([Startdate]) = 5 AND Year([Startdate]) = " + ddlYear.SelectedValue.Trim() + " ORDER BY [Startdate] Desc) AS May, " +
    //                    "( SELECT TOP 1 [Status] FROM  [DD_DocketMaster] WHERE VesselId = V.VesselId AND Month([Startdate]) = 6 AND Year([Startdate]) = " + ddlYear.SelectedValue.Trim() + " ORDER BY [Startdate] Desc) AS Jun, " +
    //                    "( SELECT TOP 1 [Status] FROM  [DD_DocketMaster] WHERE VesselId = V.VesselId AND Month([Startdate]) = 7 AND Year([Startdate]) = " + ddlYear.SelectedValue.Trim() + " ORDER BY [Startdate] Desc) AS Jul, " +
    //                    "( SELECT TOP 1 [Status] FROM  [DD_DocketMaster] WHERE VesselId = V.VesselId AND Month([Startdate]) = 8 AND Year([Startdate]) = " + ddlYear.SelectedValue.Trim() + " ORDER BY [Startdate] Desc) AS Aug, " +
    //                    "( SELECT TOP 1 [Status] FROM  [DD_DocketMaster] WHERE VesselId = V.VesselId AND Month([Startdate]) = 9 AND Year([Startdate]) = " + ddlYear.SelectedValue.Trim() + " ORDER BY [Startdate] Desc) AS Sep, " +
    //                    "( SELECT TOP 1 [Status] FROM  [DD_DocketMaster] WHERE VesselId = V.VesselId AND Month([Startdate]) = 10 AND Year([Startdate]) = " + ddlYear.SelectedValue.Trim() + " ORDER BY [Startdate] Desc) AS Oct, " +
    //                    "( SELECT TOP 1 [Status] FROM  [DD_DocketMaster] WHERE VesselId = V.VesselId AND Month([Startdate]) = 11 AND Year([Startdate]) = " + ddlYear.SelectedValue.Trim() + " ORDER BY [Startdate] Desc) AS Nov, " +
    //                    "( SELECT TOP 1 [Status] FROM  [DD_DocketMaster] WHERE VesselId = V.VesselId AND Month([Startdate]) = 12 AND Year([Startdate]) = " + ddlYear.SelectedValue.Trim() + " ORDER BY [Startdate] Desc) AS [Dec] " +
    //                    "FROM dbo.vessel V WHERE VesselStatusId <> 2  order by VESSELNAME";
    //    dt = Common.Execute_Procedures_Select_ByQuery(strSQL);
    //    rptDocket.DataSource = dt;
    //    rptDocket.DataBind();
    //}
   
    //------------- Add/ Edit Docket
    protected void BindDockets()
    {
        DataTable dt = new DataTable();
        string strSQL = "SELECT DM.*,CASE WHEN ACT_STARTDATE IS NULL THEN '' ELSE CONVERT(VARCHAR,DATEDIFF(DAY,ACT_STARTDATE,ACT_ENDDATE)) + ' Days' END AS DAYSGAP, VesselCode, VesselName,CASE DM.[Status] WHEN 'P' THEN 'Planned'  WHEN 'E' THEN 'Executed' WHEN 'C' THEN 'Closed' ELSE '' END AS DocketStatus,CASE  WHEN DM.[Status] <> 'C' AND (SELECT RFQId FROM DD_Docket_RFQ_Master WHERE DOCKETID=DM.DocketId AND [status]='P' ) > 0 THEN 'true' ELSE 'false' END  AS [ActionVisible] FROM [dbo].[DD_DocketMaster] DM INNER JOIN dbo.Vessel V ON DM.VesselId = V.VesselId AND v.vesselstatusid=1  WHERE 1=1 ";

	if (ddlYear.SelectedIndex>0)
        {
            strSQL = strSQL + " AND ( YEAR(Startdate) = " + ddlYear.SelectedValue.Trim() + " or year(Act_EndDate)=" + ddlYear.SelectedValue.Trim() + " or year(Act_StartDate)=" + ddlYear.SelectedValue.Trim() + ") ";
        }      
	else if (ddlVessel_Search.SelectedIndex != 0)
        {
            strSQL = strSQL + " AND DM.VesselId= " + ddlVessel_Search.SelectedValue.Trim();
        }
        else if (ddlFleet_Search.SelectedIndex != 0)
        {
            strSQL = strSQL + " AND V.FLEETID= " + ddlFleet_Search.SelectedValue.Trim();
        }
        dt = Common.Execute_Procedures_Select_ByQuery(strSQL + " ORDER BY VesselName ");
        rptDocket.DataSource = dt;
        rptDocket.DataBind();
    }
    protected void btnAddDocket_Click(object sender, EventArgs e)
    {
        DocketId = 0;
        
        dvbtnUpdateSpecification.Visible = false;
        dvbtnYardConfirmation.Visible = false;
        dvbtnJobTracking.Visible = false;
        dvbtnCostTracking.Visible = false;
        dvbtnDocuments.Visible = false; 

        BindDockets();
        ddlVessel.SelectedIndex = 0;
        ddlDrydockType.SelectedIndex = 0;
        ddlDDType.Items.Clear();
        txtStartDate.Text = "";
        txtEndDate.Text = "";
        rdoMaster.Checked= true;
        rdoLastDocking.Checked = false;
        dv_AddEditDocket.Visible = true;
    }
    protected void btnSaveDocket_Click(object sender, EventArgs e)
    {
        if (txtEndDate.Text.Trim() != "")
        {
            if (Convert.ToDateTime(txtEndDate.Text.Trim()) < Convert.ToDateTime(txtStartDate.Text.Trim()))
            {
                txtEndDate.Focus();
                lblMsg.Text = "Please check! End Date can not be less than start date.";
                return;
            }
        }
        
        if (rdoLastDocking.Checked)
        {
            if (txtDocketNo.Text.Trim() == "")
            {
                txtDocketNo.Focus();
                lblMsg.Text = "Please enter valid docket # to copy jobs.";
                return;
            }
            else
            {
                DataTable dt1 = Common.Execute_Procedures_Select_ByQuery("SELECT DocketId FROM [dbo].[DD_DocketMaster] WHERE LTRIM(RTRIM(DocketNo))=LTRIM(RTRIM('" + txtDocketNo.Text.Trim() + "'))");
                if (dt1.Rows.Count <= 0)
                {
                    txtDocketNo.Focus();
                    lblMsg.Text = "Please enter valid docket # to copy jobs.";
                    return;
                }
            }
        }

       
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT DocketId FROM [dbo].[DD_DocketMaster] WHERE [VesselId] =" + ddlVessel.SelectedValue.Trim() + " AND (  ([Startdate] BETWEEN '" + txtStartDate.Text.Trim() + "' AND '" + txtEndDate.Text.Trim() + "') OR ([EndDate] BETWEEN '" + txtStartDate.Text.Trim() + "' AND '" + txtEndDate.Text.Trim() + "') OR ( [Startdate] >= '" + txtStartDate.Text.Trim() + "' AND [EndDate] <= '" + txtEndDate.Text.Trim() + "' ) ) ");

        if(dt.Rows.Count > 0)
        {
            ddlVessel.Focus();
            lblMsg.Text = "Please check! Docket already planned for same period.";
            return;
        }


        try
        {
            Common.Set_Procedures("[dbo].[DD_InsertUpdateDocketMaster]");
            Common.Set_ParameterLength(9);
            Common.Set_Parameters(
               new MyParameter("@DocketId", 0),
               new MyParameter("@VesselId", ddlVessel.SelectedValue.Trim()),
               new MyParameter("@DocketType", ddlDrydockType.SelectedValue.Trim()),
               new MyParameter("@DDType", ddlDDType.SelectedValue.Trim()),
               new MyParameter("@Startdate", txtStartDate.Text.Trim()),
               new MyParameter("@EndDate", txtEndDate.Text.Trim()),
               new MyParameter("@Status", ""),
               new MyParameter("@JobsFrom", rdoMaster.Checked ? 1 : 2),
               new MyParameter("@JobsFromDocketNo", txtDocketNo.Text.Trim()));
            DataSet ds = new DataSet();
            ds.Clear();
            Boolean res;
            res = Common.Execute_Procedures_IUD(ds);
            if (res)
            {
                lblMsg.Text = "Docket added successfully.";
            }
            else
            {
                lblMsg.Text = "Unable to add docket.Error : " + Common.getLastError();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Unable to add docket.Error :" + ex.Message.ToString();
        }
    }
    protected void btnCancelDocket_Click(object sender, EventArgs e)
    {
        //LoadDockets();
        BindDockets();
        dv_AddEditDocket.Visible = false;
    }
    protected void btnSelectDocket_Click(object sender, EventArgs e)
    {
        //DocketId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        DocketId = Common.CastAsInt32(((RadioButton)sender).Attributes["DocketId"]);

        dvbtnUpdateSpecification.Visible = true;
        dvbtnYardConfirmation.Visible = true;
        dvbtnJobTracking.Visible = true;
        dvbtnCostTracking.Visible = true;
        dvbtnDocuments.Visible = true;
        dvbtnReports.Visible = true;
        dvbtnExecute.Visible = true;
        dvbtnClosure.Visible = true;
        dvbtnDocketMail.Visible = true;

        dvbtnAddDocket.Visible = false; 

        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT RFQId,STATUS,isnull(EXECUTED,'N') AS EXECUTED FROM DD_Docket_RFQ_Master WHERE DocketId=" + DocketId + " AND [Status]='P' AND Executed='Y'");
        dvbtnJobTracking.Visible = (dt.Rows.Count > 0);
        dvbtnCostTracking.Visible = (dt.Rows.Count > 0);

        string SQL = "SELECT [Status],CASE [Status] WHEN 'P' THEN 'Planned'  WHEN 'E' THEN 'Executed' WHEN 'C' THEN 'Closed' ELSE '' END AS DocketStatus, " +
                     "CASE  WHEN [Status] <> 'C' AND (SELECT RFQId FROM DD_Docket_RFQ_Master WHERE DOCKETID=DM.DocketId AND [status]='P' ) > 0 THEN 'true' ELSE 'false' END  AS [ActionVisible]  " +
                     "FROM [dbo].[DD_DocketMaster] DM WHERE DocketId = " + DocketId;

        DataTable dtClosure = Common.Execute_Procedures_Select_ByQuery(SQL);
        dvbtnClosure.Visible = (dtClosure.Rows[0]["Status"].ToString() == "E" && dtClosure.Rows[0]["ActionVisible"].ToString() == "true");
        dvbtnExecute.Visible = (dtClosure.Rows[0]["Status"].ToString() == "P" && dtClosure.Rows[0]["ActionVisible"].ToString() == "true");
        dvbtnDocketMail.Visible = (dtClosure.Rows[0]["Status"].ToString() == "E");
        
        BindDockets();
    }
    protected void btnUpdateSpecification_Click(object sender, EventArgs e)
    {
        if (DocketId > 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "afe", "window.open('Docket_Details.aspx?DDId=" + DocketId + "' ,'_blank','','');", true);
        }
        
    }
    protected void btnYardConfirmation_Click(object sender, EventArgs e)
    {
        if (DocketId > 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "fs", "window.open('Docket_rfq.aspx?DDId=" + DocketId + "' ,'_blank','','');", true);
        }
    }
    protected void btnJobTracking_Click(object sender, EventArgs e)
    {
        if (DocketId > 0)
        {
            DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT RFQId FROM DD_Docket_RFQ_Master WHERE DocketId=" + DocketId + " AND [Status]='P' AND Executed='Y'");
            int RFQId = Common.CastAsInt32(dt.Rows[0]["RFQId"]);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "window.open('JobTracking.aspx?DocketId=" + DocketId + "&RFQId=" + RFQId + "','_blank', '', '');", true);
        }
    }
    protected void btnCostTracking_Click(object sender, EventArgs e)
    {
        if (DocketId > 0)
        {
            DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT RFQId FROM DD_Docket_RFQ_Master WHERE DocketId=" + DocketId + " AND [Status]='P' AND Executed='Y'");
            int RFQId = Common.CastAsInt32(dt.Rows[0]["RFQId"]);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "faafwe", "window.open('CostTracking.aspx?DocketId=" + DocketId + "&RFQId=" + RFQId + "','_blank', '', '');", true);
        }
    }
    protected void btnDocuments_Click(object sender, EventArgs e)
    {
        if (DocketId > 0)
        {
            //DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT DoId FROM DD_Docket_RFQ_Master WHERE DocketId=" + DocketId);
            //int RFQId = Common.CastAsInt32(dt.Rows[0]["RFQId"]);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "fsfasfe", "window.open('DD_Documents.aspx?DocketId=" + DocketId + "','_blank', '', '');", true);
        }
    }

    protected void btnClosure_Click(object sender, EventArgs e)
    {
        if (DocketId > 0)
        {
            DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT FinalPublishedOn FROM DD_DocketMaster WHERE DocketId=" + DocketId);
            if (Convert.IsDBNull(dt.Rows[0]["FinalPublishedOn"]))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "fsfasfe", "alert('Docket need to be published before closure.');", true);
            }
            else
            {
                txtClosingRemarks.Text = "";
                txtNextDDDate.Text = "";
                rdoReportSent_Y.Checked = false;
                rdoReportSent_N.Checked = false;
                txtSentDate.Text = "";
                div_DocketClosure.Visible = true;
            }
        }
        //else
        //{
        //    dvbtnUpdateSpecification.Visible = false;
        //    dvbtnYardConfirmation.Visible = false;
        //    dvbtnJobTracking.Visible = false;
        //    dvbtnCostTracking.Visible = false;
        //    dvbtnDocuments.Visible = false;
        //    //dvbtnClosure.Visible = false;
        //}
    }
    protected void btnSaveDocketClosure_Click(object sender, EventArgs e)
    {
        if (txtNextDDDate.Text.Trim() == "")
        {
            txtNextDDDate.Focus();
            lblMsg_Closure.Text = "Please enter Next DryDock Date.";
            return;
        }
        if (Convert.ToDateTime(txtNextDDDate.Text.Trim()) <= DateTime.Today)
        {
            txtNextDDDate.Focus();  
            lblMsg_Closure.Text = "Next DryDock Date should be more than today.";
            return;
        }

        if (!rdoReportSent_Y.Checked && !rdoReportSent_N.Checked)
        {
            rdoReportSent_Y.Focus();
            lblMsg_Closure.Text = "Please select report sent to ship.";
            return;
        }

        if (rdoReportSent_Y.Checked && txtSentDate.Text.Trim() == "")
        {
            txtSentDate.Focus();
            lblMsg_Closure.Text = "Please enter report sent date.";
            return;
        }

        if (rdoReportSent_Y.Checked && (Convert.ToDateTime(txtSentDate.Text.Trim()) > DateTime.Today))
        {
            txtSentDate.Focus();
            lblMsg_Closure.Text = "Sent date can not be more than today.";
            return;
        }

        try
        {
            string UpdateSQL = "EXEC DBO.DD_DOCKET_CLOSURE " + DocketId + "," + Session["loginid"].ToString() + ",'" + txtClosingRemarks.Text.Replace("'", "`").Trim() + "','" + txtNextDDDate.Text.Trim() + "','" + (rdoReportSent_Y.Checked ? "Y" : "N") + "','" + txtSentDate.Text.Trim() + "'";
            Common.Execute_Procedures_Select_ByQuery(UpdateSQL);
            lblMsg_Closure.Text = "Docket Closed Successfully"; 
        }
        catch (Exception ex)
        {
            lblMsg_Closure.Text = "Unable to save. Error: " + ex.Message;
        }



    }
    protected void btnCancelDocketClosure_Click(object sender, EventArgs e)
    {
        DocketId = 0;
        BindDockets();
        dvbtnUpdateSpecification.Visible = false;
        dvbtnYardConfirmation.Visible = false;
        dvbtnJobTracking.Visible = false;
        dvbtnCostTracking.Visible = false;
        dvbtnDocuments.Visible = false;
        dvbtnReports.Visible = false;
        dvbtnExecute.Visible = false;
        dvbtnClosure.Visible = false;
        dvbtnDocketMail.Visible = false;
        dvbtnAddDocket.Visible = true;

        div_DocketClosure.Visible = false;
    }

    protected void btnExecute_Click(object sender, EventArgs e)
    {
        DataTable dtRFQ = Common.Execute_Procedures_Select_ByQuery("SELECT RFQId FROM DD_Docket_RFQ_Master WHERE DOCKETID=" + DocketId + " AND [status]='P' ");
        RFQId = (dtRFQ.Rows.Count > 0 ? Common.CastAsInt32(dtRFQ.Rows[0]["RFQId"]) : 0);

        dv_RFQExecution.Visible = true;

    }
    protected void btnExecuteRFQ_Click(object sender, EventArgs e)
    {
        DateTime dt;

        if (txtDocArrivalDt.Text.Trim() == "")
        {
            lblMsg_Exec.Text = "Please enter docking arrival date.";
            txtDocArrivalDt.Focus();
            return;
        }
        if (!DateTime.TryParse(txtDocArrivalDt.Text.Trim(), out dt))
        {
            lblMsg_Exec.Text = "Please enter valid docking arrival  date.";
            txtDocArrivalDt.Focus();
            return;
        }

        //if (DateTime.Parse(txtDocArrivalDt.Text.Trim()) >= DateTime.Today.Date)
        //{
        //    lblMsg_Exec.Text = "Docking arrival date should be less than today.";
        //    txtDocArrivalDt.Focus();
        //    return;
        //}

        if (txtExecFrom.Text.Trim() == "")
        {
            lblMsg_Exec.Text = "Please enter repair commenced date.";
            txtExecFrom.Focus();
            return;
        }
        if (!DateTime.TryParse(txtExecFrom.Text.Trim(), out dt))
        {
            lblMsg_Exec.Text = "Please enter valid repair commenced date.";
            txtExecFrom.Focus();
            return;
        }
        if (txtExecTo.Text.Trim() == "")
        {
            lblMsg_Exec.Text = "Please enter estimated completion date.";
            txtExecTo.Focus();
            return;
        }

        if (!DateTime.TryParse(txtExecTo.Text.Trim(), out dt))
        {
            lblMsg_Exec.Text = "Please enter valid estimated completion date.";
            txtExecTo.Focus();
            return;
        }
        if (DateTime.Parse(txtExecFrom.Text.Trim()) < DateTime.Parse(txtDocArrivalDt.Text.Trim()))
        {
            lblMsg_Exec.Text = "Repair commenced date can not be less than arrival date.";
            txtExecFrom.Focus();
            return;
        }
        if (DateTime.Parse(txtExecFrom.Text.Trim()) > DateTime.Parse(txtExecTo.Text.Trim()))
        {
            lblMsg_Exec.Text = "Repair commenced date should be less than estimated completion date.";
            txtExecFrom.Focus();
            return;
        }
        try
        {
            Common.Execute_Procedures_Select_ByQuery("UPDATE DD_Docket_RFQ_Master SET [DockingArrivalDate]= '" + txtDocArrivalDt.Text.Trim() + "', [ExecFrom]= '" + txtExecFrom.Text.Trim() + "', [ExecTo]='" + txtExecTo.Text.Trim() + "', [Executed]='Y' WHERE [RFQId]=" + RFQId);
            Common.Execute_Procedures_Select_ByQuery("UPDATE DD_DocketMaster SET [Act_Startdate]= '" + txtExecFrom.Text.Trim() + "', [Act_EndDate]='" + txtExecTo.Text.Trim() + "' WHERE [DocketId]=" + DocketId);
            Common.Execute_Procedures_Select_ByQuery("UPDATE DD_DocketMaster SET Status='E' WHERE [DocketId]=" + DocketId);
            Common.Execute_Procedures_Select_ByQuery("DELETE FROM DD_Docket_RFQ_Jobs_Planning WHERE [DocketId]=" + DocketId + " AND RFQID=" + RFQId + " AND FOR_DATE NOT BETWEEN '" + txtExecFrom.Text.Trim() + "' AND '" + txtExecTo.Text.Trim() + "'");

            btnExecuteRFQ.Visible = false;
            lblMsg_Exec.Text = "RFQ Executed successfully.";
        }
        catch (Exception ex)
        {
            lblMsg_Exec.Text = "Unable to execute RFQ. Error: " + ex.Message;
        }


    }
    protected void btnCloseExec_Click(object sender, EventArgs e)
    {
        DocketId = 0;        
        RFQId = 0;
        txtDocArrivalDt.Text = "";
        txtExecFrom.Text = "";
        txtExecTo.Text = "";
        BindDockets();
        dvbtnUpdateSpecification.Visible = false;
        dvbtnYardConfirmation.Visible = false;
        dvbtnJobTracking.Visible = false;
        dvbtnCostTracking.Visible = false;
        dvbtnDocuments.Visible = false;
        dvbtnReports.Visible = false;
        dvbtnExecute.Visible = false;
        dvbtnClosure.Visible = false;
        dvbtnDocketMail.Visible = false;
        dvbtnAddDocket.Visible = true;
        dv_RFQExecution.Visible = false;
    }

    protected void btnExecClose_Click(object sender, EventArgs e)
    {
        string[] str = ((LinkButton)sender).CommandArgument.Split('~');

        DocketId = Common.CastAsInt32(str[0]);
        string Status = str[1].Trim();

        if (Status == "E")
        {
            btnClosure_Click(sender, e);
        }
        else
        {
            DataTable dtRFQ = Common.Execute_Procedures_Select_ByQuery("SELECT RFQId FROM DD_Docket_RFQ_Master WHERE DOCKETID=" + DocketId + " AND [status]='P' ");
            RFQId = (dtRFQ.Rows.Count > 0 ? Common.CastAsInt32(dtRFQ.Rows[0]["RFQId"]) : 0);

            btnExecute_Click(sender, e);

        }
    }
    protected void btnReports_Click(object sender, EventArgs e)
    {
        if (DocketId > 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "fs", "window.open('CreateReports.aspx?DocketId=" + DocketId + "' ,'_blank','','');", true);
        }
    }

    protected void btnDocketMail_Click(object sender, EventArgs e)
    {
        //DocketId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        DataTable dtRFQ = Common.Execute_Procedures_Select_ByQuery("SELECT RFQId FROM DD_Docket_RFQ_Master WHERE DOCKETID=" + DocketId + " AND [status]='P' ");
        RFQId = (dtRFQ.Rows.Count > 0 ? Common.CastAsInt32(dtRFQ.Rows[0]["RFQId"]) : 0);
        
        dv_SendMail.Visible = true;
    }
    protected void btnSendMail_Click(object sender, EventArgs e)
    {
        if (!chkjobTrtacking.Checked && !chkCostTracking.Checked)
        {
            lblMsg_Mail.Text = "Please select update for.";
            chkjobTrtacking.Focus();
            return;
        }         
       SendMail();        
    }
    protected void btnCloseMail_Click(object sender, EventArgs e)
    {
        DocketId = 0;
        RFQId = 0;
        chkjobTrtacking.Checked = false;
        chkCostTracking.Checked = false;

        BindDockets();
        dvbtnUpdateSpecification.Visible = false;
        dvbtnYardConfirmation.Visible = false;
        dvbtnJobTracking.Visible = false;
        dvbtnCostTracking.Visible = false;
        dvbtnDocuments.Visible = false;
        dvbtnReports.Visible = false;
        dvbtnExecute.Visible = false;
        dvbtnClosure.Visible = false;
        dvbtnDocketMail.Visible = false;
        dvbtnAddDocket.Visible = true;

        dv_SendMail.Visible = false;
    }

    private void SendMail()
    {

        DataTable dtOther = Common.Execute_Procedures_Select_ByQuery("SELECT DOCKETNO,GROUPEMAIL, " +
                                                "(SELECT FIRSTNAME + '  ' + MIDDLENAME + ' ' + FAMILYNAME FROM DBO.Hr_PersonalDetails E LEFT JOIN DBO.POSITION P ON P.POSITIONID=E.POSITION WHERE USERID=" + LoginId + ") AS FULLNAME, " +
                                                "(SELECT POSITIONNAME FROM DBO.Hr_PersonalDetails E LEFT JOIN DBO.POSITION P ON P.POSITIONID=E.POSITION WHERE USERID=1) AS POSITIION, " +
                                                "(SELECT EMAIL from DBO.USERLOGIN WHERE LOGINID=" + LoginId + ") AS USERMAIL " +
                                                "FROM DD_DOCKETMASTER D INNER JOIN DBO.VESSEL V  ON D.VESSELID=V.VESSELID WHERE DOCKETID=" + DocketId.ToString());

        if (dtOther.Rows.Count > 0)
        {
            string DOCKETNO = dtOther.Rows[0]["DOCKETNO"].ToString();
            string GROUPEMAIL = dtOther.Rows[0]["GROUPEMAIL"].ToString();
            string FULLNAME = dtOther.Rows[0]["FULLNAME"].ToString();
            string POSITIION = dtOther.Rows[0]["POSITIION"].ToString();
            string USERMAIL = dtOther.Rows[0]["USERMAIL"].ToString();

            //GROUPEMAIL="pankaj.v@esoftech.com";
            //USERMAIL = "pankaj.k@esoftech.com";

            //CreatePDF(DOCKETNO, dtSummary1, dtCats1, dtJobs1, d2, "Tomorrow.pdf");
            string[] To = { USERMAIL };
            string[] CC = {  };
            string err = "";
            string msg = "<br>Attached find the Drydock update.<br><br><br>" + "Thanks<br><br><b>" + FULLNAME + "</b><br><i>(" + POSITIION + ")</i>";
            string[] FileName = {"",""};

            if (chkjobTrtacking.Checked && chkCostTracking.Checked)
            {
                SaveXLS_JobTracking();
                SaveXLS_CostTracking();

                FileName[0] = Server.MapPath("~/DryDock/JobUpdateExcel.xls");
                FileName[1] = Server.MapPath("~/DryDock/CostUpdateExcel.xls");
            }
            else if(chkjobTrtacking.Checked)
            {
                SaveXLS_JobTracking();
                FileName[0] = Server.MapPath("~/DryDock/JobUpdateExcel.xls");
            }
            else if (chkCostTracking.Checked)
            {
                SaveXLS_CostTracking();
                FileName[0] = Server.MapPath("~/DryDock/CostUpdateExcel.xls");
            }
            
            
            //ProjectCommon.SendeMail_MTM(LoginId, "emanager@energiossolutions.com", "emanager@energiossolutions.com", To, CC, CC, DOCKETNO + " - DD Update : " + DateTime.Today.ToString("dd-MMM-yyyy"), msg, out err, FileName);
            EProjectCommon.SendeMail_MTM_ATT(LoginId, "emanager@energiossolutions.com", "emanager@energiossolutions.com", To, CC, CC, DOCKETNO + " - DD Update : " + DateTime.Today.ToString("dd-MMM-yyyy"), msg, out err, FileName);
            lblMsg_Mail.Text = "Mail send successfully.";
        }
    }
    private void SaveXLS_CostTracking()
    {
        //------------------------------------------
        StringBuilder sb = new StringBuilder();

        sb.Append("<table cellspacing='0' cellpadding='0' width='100%' border='1'>");


        //DataTable dtSummary = Common.Execute_Procedures_Select_ByQuery("SELECT R.*,(SELECT VESSELNAME FROM DBO.VESSEL V WHERE V.VESSELID=D.VESSELID) AS VESSELNAME FROM DD_Docket_RFQ_Master R INNER JOIN DD_DocketMaster D ON D.DocketId = R.DocketId WHERE R.RFQId=" + RFQId + " AND R.DocketId=" + DocketId.ToString());

        //DateTime ExecFrom = Convert.ToDateTime(dtSummary.Rows[0]["EXECFROM"]);
        //DateTime ExecTo = Convert.ToDateTime(dtSummary.Rows[0]["EXECTO"]);

        DataTable dtCats = Common.Execute_Procedures_Select_ByQuery("SELECT DISTINCT CATID,CATCODE,CATNAME FROM DD_Docket_RFQ_Jobs WHERE RFQID=" + RFQId + " AND DOCKETID=" + DocketId);
        DataTable dtJobs = Common.Execute_Procedures_Select_ByQuery("SELECT CATID,DocketJobId,JOBCODE,JOBNAME,JobDesc,( SELECT TOP 1 JP.PER FROM DD_Docket_RFQ_Jobs_Planning JP WHERE JP.DocketJobId=J.DocketJobId AND JP.RFQID=" + RFQId + " AND JP.DOCKETID=" + DocketId + " AND ISNULL(JP.PER,'')<>'' ORDER BY JP.FOR_DATE DESC ) AS EXECPER,( SELECT TOP 1 JP.REMARK FROM DD_Docket_RFQ_Jobs_Planning JP WHERE JP.DocketJobId=J.DocketJobId AND JP.RFQID=" + RFQId + " AND JP.DOCKETID=" + DocketId + " AND ISNULL(JP.PER,'')<>'' ORDER BY JP.FOR_DATE DESC ) AS EXECREMARKS FROM DD_Docket_RFQ_Jobs J WHERE RFQID=" + RFQId + " AND DOCKETID=" + DocketId + " ORDER BY JOBCODE");
        DataTable dtJobsPer = Common.Execute_Procedures_Select_ByQuery("SELECT RFQId,[DocketSubJobId],[DocketJobId],[DocketId],[SubJobCode],[SubJobName],AttachmentName,Unit,POQty,POUnitPrice_USD,PODiscountPer,CASE WHEN PONetAmount_USD = 0.00 THEN 0 ELSE PONetAmount_USD END AS PONetAmount_USD  ,EstAmount_USD,ROUND((ISNULL(EstAmount_USD,0)-ISNULL(PONetAmount_USD,0)),2) as VarAmt,ROUND(((ISNULL(EstAmount_USD,0)-ISNULL(PONetAmount_USD,0)) * 100 / ( CASE WHEN ISNULL(PONetAmount_USD,0)=0 THEN 1 ELSE PONetAmount_USD END)),2) As VarPer, EstQty, Remarks FROM [dbo].[DD_Docket_RFQ_SubJobs] WHERE DOCKETID=" + DocketId + " And RFQId=" + RFQId + " ORDER BY SubJobCode");

        // Header -------------------------------------
        sb.Append("<tr>");
        sb.Append("<td style='background-color:grey;'><b>Job List</b></td>"); 
        sb.Append("<td><b>PO Qty</b></td>");
        sb.Append("<td><b>Unit Price</b></td>");
        sb.Append("<td><b>Disc(%)</b></td>");
        sb.Append("<td><b>Net Amount</b></td>");
        sb.Append("<td><b>Est. Qty</b></td>");
        sb.Append("<td><b>Estimated Amt.</b></td>");
        sb.Append("<td><b>Var(Amt)</b></td>");
        sb.Append("<td><b>Var(%)</b></td>");
        sb.Append("<td><b>Remarks</b></td>");
        sb.Append("</tr>");

        foreach (DataRow dr in dtCats.Rows)
        {
            
            //-----------------------------------------
            DataView dv = dtJobs.DefaultView;
            dv.RowFilter = "CATID=" + dr["CATID"].ToString();
            DataTable dtJobs_Filered = dv.ToTable();

            // Data -------------------------------------
            if (dtJobs_Filered.Rows.Count > 0)
            {
                sb.Append("<tr>");
                sb.Append("<td><b>" + dr["CATCODE"].ToString() + ":" + dr["CATNAME"].ToString() + "</b></td>");
                sb.Append("<td >&nbsp;</td>");
                sb.Append("<td >&nbsp;</td>");
                sb.Append("<td >&nbsp;</td>");
                sb.Append("<td >&nbsp;</td>");
                sb.Append("<td >&nbsp;</td>");
                sb.Append("<td >&nbsp;</td>");
                sb.Append("<td >&nbsp;</td>");
                sb.Append("<td >&nbsp;</td>");
                sb.Append("<td >&nbsp;</td>");
                sb.Append("</tr>");
                //-----------------------------------------
                foreach (DataRow drIn in dtJobs_Filered.Rows)
                {
                    sb.Append("<tr>");
                    sb.Append("<td>" + drIn["JOBCODE"].ToString() + ":" + drIn["JOBNAME"].ToString() + "</td>");
                    sb.Append("<td >&nbsp;</td>");
                    sb.Append("<td >&nbsp;</td>");
                    sb.Append("<td >&nbsp;</td>");
                    sb.Append("<td >&nbsp;</td>");
                    sb.Append("<td >&nbsp;</td>");
                    sb.Append("<td >&nbsp;</td>");
                    sb.Append("<td >&nbsp;</td>");
                    sb.Append("<td >&nbsp;</td>");
                    sb.Append("<td >&nbsp;</td>");
                    sb.Append("</tr>");
                        //-------------------
                        DataView dv1 = dtJobsPer.DefaultView;
                        dv1.RowFilter = "DocketJobId=" + drIn["DocketJobId"].ToString();
                        DataTable dt1 = dv1.ToTable();

                        foreach(DataRow dr1 in dt1.Rows)
                        { 
                            sb.Append("<tr>");
                            sb.Append("<td>" + dr1["SubJobCode"].ToString() + ":" + dr1["SubJobName"].ToString() + "</td>");
                            sb.Append("<td>" + dr1["POQty"].ToString() + "</td>");
                            sb.Append("<td>" + dr1["POUnitPrice_USD"].ToString() + "</td>");
                            sb.Append("<td>" + dr1["PODiscountPer"].ToString() + "</td>");
                            sb.Append("<td>" + dr1["PONetAmount_USD"].ToString() + "</td>");
                            sb.Append("<td>" + dr1["EstQty"].ToString() + "</td>");
                            sb.Append("<td>" + dr1["EstAmount_USD"].ToString() + "</td>");
                            sb.Append("<td>" + dr1["VarAmt"].ToString() + "</td>");
                            sb.Append("<td>" + dr1["VarPer"].ToString() + "</td>");
                            sb.Append("<td>" + dr1["Remarks"].ToString() + "</td>");
                            sb.Append("</tr>");

                        }
                        
                    }
                }
                //-----------------------------------------
            }
        
        sb.Append("</table>");

        File.WriteAllText(Server.MapPath("~/DryDock/CostUpdateExcel.xls"), sb.ToString());

        //------------------------------------------
    }
    protected void SaveXLS_JobTracking()
    {
        //------------------------------------------
        StringBuilder sb = new StringBuilder();
        sb.Append("<table cellspacing='0' cellpadding='0' width='100%' border='1'>");

        DataTable dtSummary = Common.Execute_Procedures_Select_ByQuery("SELECT R.*,(SELECT VESSELNAME FROM DBO.VESSEL V WHERE V.VESSELID=D.VESSELID) AS VESSELNAME FROM DD_Docket_RFQ_Master R INNER JOIN DD_DocketMaster D ON D.DocketId = R.DocketId WHERE R.RFQId=" + RFQId + " AND R.DocketId=" + DocketId.ToString());
        DateTime ExecFrom = Convert.ToDateTime(dtSummary.Rows[0]["EXECFROM"]);
        DateTime ExecTo = Convert.ToDateTime(dtSummary.Rows[0]["EXECTO"]);

      
        DataTable dtCats = Common.Execute_Procedures_Select_ByQuery("SELECT DISTINCT CATID,CATCODE,CATNAME FROM DD_Docket_RFQ_Jobs WHERE RFQID=" + RFQId + " AND DOCKETID=" + DocketId);
        DataTable dtJobs = Common.Execute_Procedures_Select_ByQuery("SELECT CATID,DocketJobId,JOBCODE,JOBNAME,JobDesc,EXECPER,PlanFrom,PlanTo,( SELECT TOP 1 JP.For_Date FROM DD_Docket_RFQ_Jobs_Planning JP WHERE JP.DocketJobId=J.DocketJobId AND JP.RFQID=" + RFQId + " AND JP.DOCKETID=" + DocketId + " ORDER BY JP.FOR_DATE DESC ) AS ExecPerDate,( SELECT TOP 1 JP.REMARK FROM DD_Docket_RFQ_Jobs_Planning JP WHERE JP.DocketJobId=J.DocketJobId AND JP.RFQID=" + RFQId + " AND JP.DOCKETID=" + DocketId + " ORDER BY JP.FOR_DATE DESC ) AS EXECREMARKS FROM DD_Docket_RFQ_Jobs J WHERE RFQID=" + RFQId + " AND DOCKETID=" + DocketId + " ORDER BY JOBCODE");
      
        // Header -------------------------------------
        sb.Append("<tr>");
        sb.Append("<td style='background-color:grey;'><b>Job List</b></td>");
        sb.Append("<td>Status</td>");
        sb.Append("<td>Commencement Dt.</td>");
        sb.Append("<td>Exp. Completion</td>");
        sb.Append("<td>Remarks</td>");
        sb.Append("<td>Updated On</td>");
        sb.Append("</tr>");

        foreach (DataRow dr in dtCats.Rows)
        {
            int Days = ExecTo.Subtract(ExecFrom).Days;
            //-----------------------------------------
            DataView dv = dtJobs.DefaultView;
            dv.RowFilter = "CATID=" + dr["CATID"].ToString();
            DataTable dtJobs_Filered = dv.ToTable();

            // Data -------------------------------------
            if (dtJobs_Filered.Rows.Count > 0)
            {
                sb.Append("<tr>");
                sb.Append("<td><b>" + dr["CATCODE"].ToString() + ":" + dr["CATNAME"].ToString() + "</b></td>");
                sb.Append("<td colspan='5'></td>");
                sb.Append("</tr>");
                //-----------------------------------------
                foreach (DataRow drIn in dtJobs_Filered.Rows)
                {
                    sb.Append("<tr>");
                    sb.Append("<td>" + drIn["JOBCODE"].ToString() + ":" + drIn["JOBNAME"].ToString() + "</td>");
                    sb.Append("<td>" + (Common.CastAsInt32(drIn["ExecPer"]) == 100 ? "Completed" : ((Common.ToDateString(drIn["PlanFrom"]) == "") ? "" : "In Progress")) + "</td>");
                    sb.Append("<td>" + Common.ToDateString(drIn["PlanFrom"]) + "</td>");
                    sb.Append("<td>" + Common.ToDateString(drIn["PlanTo"]) + "</td>");
                    sb.Append("<td>" + drIn["EXECREMARKS"].ToString() + "</td>");
                    sb.Append("<td>" + Common.ToDateString(drIn["ExecPerDate"]) + "</td>");
                    sb.Append("</tr>");
                }
                //-----------------------------------------
            }
        }
        sb.Append("</table>");

        File.WriteAllText(Server.MapPath("~/DryDock/JobUpdateExcel.xls"), sb.ToString());

        //------------------------------------------
    }

    protected void ddlDrydockType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlDDType.Items.Clear();
        ddlDDType.Items.Add(new ListItem("< Select >", "0"));
        
        if (ddlDrydockType.SelectedValue == "Planned")
        {
            ddlDDType.DataSource = Common.Execute_Procedures_Select_ByQuery("select * from DBO.DD_DDTYPE ORDER BY DDTYPENAME");
            ddlDDType.DataTextField = "DDTYPENAME";
            ddlDDType.DataValueField = "DDTYPEID";
            ddlDDType.DataBind();
        }
        if (ddlDrydockType.SelectedValue == "Unplanned")
        {
            ddlDDType.Items.Add(new ListItem("DAMAGE  REPAIR", "DAMAGE  REPAIR"));
            ddlDDType.Items.Add(new ListItem("OWNER REQUEST", "OWNER REQUEST"));
        }
        
    }
    
}
