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
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Collections.Generic;
using System.Text;

public partial class Docket_JobTracking : System.Web.UI.Page
{
    public string EditMode
    {
        get { return ViewState["EditMode"].ToString(); }
        set { ViewState["EditMode"] = value; }
    }
    public string DR
    {
        get { return ViewState["DR"].ToString(); }
        set { ViewState["DR"] = value; }
    }
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
    public int Level
    {
        set { ViewState["Level"] = value; }
        get { return Common.CastAsInt32(ViewState["Level"]); }
    }
    public String ForDate
    {
        set { ViewState["ForDate"] = value; }
        get { return ViewState["ForDate"].ToString(); }
    }
    public int LoginId
    {
        get { return Common.CastAsInt32(Session["loginid"]); }
    }
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
        // -------------------------- SESSION CHECK ----------------------------------------------
        ProjectCommon.SessionCheck();
        // -------------------------- SESSION CHECK END ----------------------------------------------

        lbl_MsgRemarks.Text = "";
        lblMsgMain.Text = "";
        lblMsg_Exec.Text = "";
        if (LoginId <= 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "af", "alert('Session Expired.'); window.close();", true);
            EditMode = "N";
            DR = "N";
            return;
        }
        if (!IsPostBack)
        {
            EditMode = "N";
            DR = "N";
            ForDate = "";
            LoadCat();
            DocketId = Common.CastAsInt32(Request.QueryString["DocketId"]);
            RFQId = Common.CastAsInt32(Request.QueryString["RFQId"]);
            ShowDocketSummary();
            Level = 2;
            LoadCategory();
           
        }
    }
    protected void LoadCat()
    {
         DataTable dtCats = Common.Execute_Procedures_Select_ByQuery("SELECT CatId,CatCode  + ':'  + CatName AS CatName FROM DD_JobCategory Order By CatCode");
         ddlJObCat.DataSource = dtCats;
         ddlJObCat.DataTextField="CatName";
         ddlJObCat.DataValueField="CatId";
         ddlJObCat.DataBind();
         ddlJObCat.Items.Insert(0,new System.Web.UI.WebControls.ListItem("< Select Job Category >", "0"));
         try
         {
             ddlJObCat.SelectedIndex = 1;
         }
         catch { }
    }
    protected void LoadCat_SelectIndexChanged(object sender, EventArgs e)
    {
        LoadCategory();
    }
    //protected void btn_EditPlanning_Click(object sender, EventArgs e)
    //{
    //    EditMode = "Y";
        
    //    btn_Edit.Visible = false;
    //    btn_Save.Visible = true;
    //    btn_Cancel.Visible = true;
    //    btn_DailyReport.Visible = false;
    //    btnEnterComments.Visible = false;
    //}
    
    protected void LoadCategory()
    {
        string statusfilter = "";
        if (ddlStatus.SelectedIndex > 0)
        {
            switch(ddlStatus.SelectedValue)
            {
                case "NS" :
                    statusfilter = " AND PlanFrom IS NULL ";
                break;
                case "IP" :
                statusfilter = " AND PlanFrom IS NOT NULL and ExecPer<>100 ";
                break;
                case "CP" :
                    statusfilter = " AND ExecPer=100 ";
                break;
                default:
                    statusfilter = "";
                    break;
            }
        }

        if (txtUpdatedOn.Text.Trim() != "")
            statusfilter += " AND (SELECT TOP 1 JP.For_Date FROM DD_Docket_RFQ_Jobs_Planning JP WHERE JP.DocketJobId=J.DocketJobId AND JP.RFQID=" + RFQId + " AND JP.DOCKETID=" + DocketId + " ORDER BY JP.FOR_DATE DESC )='" + txtUpdatedOn.Text.Trim() + "' ";

        DateTime StartDate=new DateTime();
        DateTime EndDate = new DateTime();
        DataTable dtRFQ = Common.Execute_Procedures_Select_ByQuery("SELECT ExecFrom,ExecTo FROM DD_Docket_RFQ_Master Where RFQId=" + RFQId);
        if (dtRFQ.Rows.Count > 0)
        {
            StartDate = Convert.ToDateTime(dtRFQ.Rows[0]["ExecFrom"]);
            EndDate = Convert.ToDateTime(dtRFQ.Rows[0]["ExecTo"]);

            ExecFromDate = StartDate;
            ExecToDate = EndDate;
        }

        if (DateTime.Today > EndDate)
        {
            btn_DailyReport.Visible = false; 
        }


        int ScrollWidth = EndDate.Subtract(StartDate).Days * 50;

        StringBuilder sbleft = new StringBuilder();
        StringBuilder sbdata = new StringBuilder();

        DataTable dtCats = new DataTable();
        
        if(ddlJObCat.SelectedIndex <=0)
            dtCats=Common.Execute_Procedures_Select_ByQuery("SELECT CatId,CatCode,CatName FROM DD_JobCategory Order By CatCode");
        else
            dtCats = Common.Execute_Procedures_Select_ByQuery("SELECT CatId,CatCode,CatName FROM DD_JobCategory where CatId=" + ddlJObCat.SelectedValue + " Order By CatCode");

        DataTable dtRFQs = Common.Execute_Procedures_Select_ByQuery("SELECT RFQID,RFQNO,YARDNAME,(SELECT ISNULL(SUM(SJ.NetAmount_USD),0) FROM DD_Docket_RFQ_SubJobs SJ WHERE SJ.RFQID=DD_Docket_RFQ_Master.RFQID) AS NetAmount_USD FROM DD_Docket_RFQ_Master INNER JOIN [dbo].[DD_YardMaster] YM ON YM.YARDID=DD_Docket_RFQ_Master.YARDID WHERE DOCKETID=" + DocketId + "");
        DataTable dtJobs_All=Common.Execute_Procedures_Select_ByQuery("SELECT CatId,DocketJobId,JobId,JOBCODE,JOBNAME,RFQID, DocketId FROM DD_Docket_RFQ_Jobs J WHERE RFQID=" + RFQId + statusfilter + "  ORDER BY JOBCODE");
        
        //DataTable dtDocketSubJobs_All = Common.Execute_Procedures_Select_ByQuery("SELECT [DocketSubJobId],[DocketJobId],[DocketId],[SubJobCode],left([SubJobName],100) + '...' as SubJobName,AttachmentName,Unit FROM [dbo].[DD_DocketSubJobs] WHERE DOCKETID=" + DocketId + " ORDER BY SubJobCode");
        //DataTable dtRFQSubJobs_All = Common.Execute_Procedures_Select_ByQuery("SELECT RFQId,[DocketSubJobId],[DocketJobId],[DocketId],[SubJobCode],[SubJobName],AttachmentName,Unit,BidQty,QuoteQty,UnitPrice_USD,DiscountPer,NetAmount_USD FROM [dbo].[DD_Docket_RFQ_SubJobs] WHERE DOCKETID=" + DocketId + " ORDER BY SubJobCode");
        //---------------
        sbleft.Append("<table cellspacing='0' rules='all' border='1' cellpadding='3' style='border-collapse:collapse; width:100%;'>");

        foreach (DataRow drCat in dtCats.Rows)
        {
            sbleft.Append("<tr>");
            sbleft.Append("<td class='cat'>");
            if (DR == "Y")
            {
                sbleft.Append("<input type='checkbox' onclick='SelectAll(this)' style='margin-top:-2px;padding:0px;' catcode='" + drCat["CatCode"].ToString() + "'/>");
            }
            sbleft.Append("</td>");
            sbleft.Append("<td class='cat' colspan='7'>");
            sbleft.Append(drCat["CatCode"].ToString() + " : " + drCat["CatName"].ToString());
          
            sbleft.Append("</td>");
            sbleft.Append("</tr>");
            
            if (Level >= 2)//--------------------------------------------
            {
                sbleft.Append("<tr class='head_row'>");
                sbleft.Append("<td style='width:20px;'>&nbsp;</td>");
                sbleft.Append("<td style='width:500px;'>Job Details</td>");
                sbleft.Append("<td style='width:80px;'>Status</td>");
                sbleft.Append("<td style='width:80px;'>History</td>");
                sbleft.Append("<td style='width:120px;'>Commencement Dt.</td>");
                sbleft.Append("<td style='width:120px;'>Exp. Completion Dt.</td>");
                sbleft.Append("<td >Remarks</td>");
                sbleft.Append("<td style='width:80px;'>Updated On</td>");
                sbleft.Append("</tr>");

                DataTable dtJobs2 = getFilterdJobs(dtJobs_All, drCat["CatId"].ToString());
                foreach (DataRow drJob1 in dtJobs2.Rows)
                {
                    int MaxPer = 0;
                    DataTable dtMAXPER = Common.Execute_Procedures_Select_ByQuery("select MAX(cast(PER as int)) as MAXPER from [DD_Docket_RFQ_Jobs_Planning] WHERE RFQId=" + RFQId + " AND DocketId=" + DocketId + " AND DocketJobId=" + drJob1["DocketJobId"].ToString());
                    if (dtMAXPER.Rows.Count > 0)
                        MaxPer = Common.CastAsInt32(dtMAXPER.Rows[0][0]);

                    sbleft.Append("<tr>");
                    sbleft.Append("<td>");
                    if (MaxPer < 100 && DR == "Y")
                        sbleft.Append("<input type='checkbox' catcode='" + drCat["CatCode"].ToString() + "' class='subjobs' style='margin-top:-2px;padding:0px;' name='chk_" + drJob1["DocketJobId"].ToString() + "'/>");
                    else
                        sbleft.Append("&nbsp;");
                    sbleft.Append("</td>");

                    DataTable dt11 = Common.Execute_Procedures_Select_ByQuery("SELECT JOBCODE,PlanFrom,PlanTo,StartFrom,StartTo,ExecPer,( SELECT TOP 1 JP.For_Date FROM DD_Docket_RFQ_Jobs_Planning JP WHERE JP.DocketJobId=J.DocketJobId AND JP.RFQID=" + RFQId + " AND JP.DOCKETID=" + DocketId + " ORDER BY JP.FOR_DATE DESC ) AS ExecPerDate,( SELECT TOP 1 JP.REMARK FROM DD_Docket_RFQ_Jobs_Planning JP WHERE JP.DocketJobId=J.DocketJobId AND JP.RFQID=" + RFQId + " AND JP.DOCKETID=" + DocketId + " ORDER BY JP.FOR_DATE DESC ) AS EXECREMARKS FROM DD_Docket_RFQ_Jobs J  WHERE RFQId=" + RFQId + " AND DocketId=" + DocketId + " AND DocketJobId=" + drJob1["DocketJobId"].ToString());
                    sbleft.Append("<td>" + drJob1["JOBCODE"].ToString() + " : " + drJob1["JOBNAME"].ToString() + "</td>");
                    string status = (Common.CastAsInt32(dt11.Rows[0]["ExecPer"]) == 100 ? "Completed" : ((Common.ToDateString(dt11.Rows[0]["PlanFrom"]) == "") ? "" : "InProgress"));
                    sbleft.Append("<td class='" + status + "'><input type='text' name='ctl_" + dt11.Rows[0]["JobCode"] + "' id='ctl_" + dt11.Rows[0]["JobCode"] + "' class='tr_" + dt11.Rows[0]["JobCode"] + "' style='width:150px;display:none;' />  &nbsp; " + status + "<span style='display:none' id='cmt_" + dt11.Rows[0]["JobCode"] + "'>" + dt11.Rows[0]["ExecRemarks"] + "</span></td>");
                    
                    if (Common.ToDateString(dt11.Rows[0]["PlanFrom"]) == "")
                        sbleft.Append("<td>&nbsp;</td>");
                    else
                        sbleft.Append("<td><a href='#' docketjobid='" + drJob1["DocketJobId"].ToString() + "' linkid='" + drJob1["JOBCODE"].ToString() + "' JobName='" + drJob1["JOBNAME"].ToString().Replace("'", "`").Trim() + "' ExecRemarks='" + dt11.Rows[0]["ExecRemarks"].ToString() + "'  onclick='ShowHistory(this);'>view</a></td>");

                    sbleft.Append("<td>" + Common.ToDateString(dt11.Rows[0]["PlanFrom"]) + "</td>");
                    sbleft.Append("<td>" + Common.ToDateString(dt11.Rows[0]["PlanTo"]) + "</td>");
                    sbleft.Append("<td>" + dt11.Rows[0]["EXECREMARKS"].ToString() + "</td>");
                    sbleft.Append("<td>" + Common.ToDateString(dt11.Rows[0]["ExecPerDate"]) + "</td>");
                    sbleft.Append("</tr>");
                    
                }
            }
        }
        sbleft.Append("</table>");
        litData.Text = sbleft.ToString();
    }
    public DataTable getFilterdJobs(DataTable dt,string CatId)
    {
        DataView dv = dt.DefaultView;
        dv.RowFilter = "CatId=" + CatId;
        return dv.ToTable();
    }
    public DataTable getFilterdSubJobs(DataTable dt, string DocketJobId)
    {
        DataView dv = dt.DefaultView;
        dv.RowFilter = "DocketJobId=" + DocketJobId;
        return dv.ToTable();
    }
    public DataTable getFilterdSubJob_Single(DataTable dt,string RFQId, string DocketJobId,string DocketSubJobId)
    {
        DataView dv = dt.DefaultView;
        dv.RowFilter = "RFQId=" + RFQId + " And DocketJobId=" + DocketJobId + " And DocketSubJobId=" + DocketSubJobId;
        return dv.ToTable();
    }
    public DataTable BindJobs(Object CatId)
    {
        return Common.Execute_Procedures_Select_ByQuery("SELECT DocketJobId,JobId,JOBCODE,JOBNAME FROM DD_DocketJobs WHERE DOCKETID=" + DocketId + " And CatId=" + CatId + " ORDER BY JOBCODE");
    }
    public DataTable BindSubJobs(Object DocketJobId)
    {
        return Common.Execute_Procedures_Select_ByQuery("SELECT [DocketSubJobId],[DocketJobId],[DocketId],[SubJobCode],[SubJobName],AttachmentName,Unit FROM [dbo].[DD_DocketSubJobs] WHERE DOCKETID=" + DocketId + " And DocketJobId=" + DocketJobId + " ORDER BY SubJobCode");
    }
    protected void imgDownload_Click(object sender, EventArgs e)
    {
        //DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT AttachmentName,Attachment FROM DD_DocketSubJobs WHERE DOCKETID=" + DocketId.ToString() + " AND DOCKETJOBID=" + DocketJobId.ToString() + " AND DOCKETSUBJOBID=" + DocketSubJobId.ToString());
        //byte[] buff = (byte[])dt.Rows[0]["Attachment"];
        //Response.AppendHeader("Content-Disposition", "attachment; filename=" + dt.Rows[0]["AttachmentName"].ToString());
        //Response.BinaryWrite(buff);
        //Response.Flush();
        //Response.End();
    }
    protected void btnDocketView_Click(object sender, EventArgs e)
    {
        int TableId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT TOP 1 VersionNo,Attachment FROM DD_Docket_Publish_History WHERE DOCKETID=" + DocketId + " ORDER BY TABLEID DESC");
        if (dt.Rows.Count > 0)
        {
            string FileName = lblDocketNo.Text.Replace("/", "-") + "-" + dt.Rows[0]["VersionNo"].ToString() + ".pdf";
            byte[] buff = (byte[])dt.Rows[0]["Attachment"];
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileName);
            Response.BinaryWrite(buff);
            Response.Flush();
            Response.End();
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
        }
        
    }

    protected void btn_SavePlanning_Click(object sender, EventArgs e)
    {
        char[] sep={','};

        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT DocketJobId,JobId,JOBCODE FROM DD_Docket_RFQ_Jobs WHERE DOCKETID=" + DocketId + " AND RFQID=" + RFQId + " ORDER BY JOBCODE");

        foreach (DataRow dr in dt.Rows)
        {
            string JobCode = dr["JOBCODE"].ToString().Trim();
            string Dates=Convert.ToString(Request.Form["ctl_" + JobCode]);
            if(Dates!=null)                
            if (Dates.Trim() != "")
            {
                Dates = Dates.TrimStart(',');
                Common.Execute_Procedures_Select_ByQuery("dbo.DD_InsertUpdate_PlanningDate " + RFQId + "," + dr["DocketJobId"].ToString() + "," + DocketId + ",'" + Dates + "'");
            }
        }
        LoadCategory();

        EditMode = "N";
        btn_Save.Visible = false;
    }
    protected void btn_UpdateStart_Click(object sender, EventArgs e)
    {
        lbl_MsgRemarks.Text = "";

        string jobCode = hfdJObCode.Value.Trim();
        int Progress = Common.CastAsInt32(txtProgress.Text);
        if (Progress > 100 || Progress < 0)
        {
            lbl_MsgRemarks.Text = "Invalid Progress value. Must be between ( 0 - 100 ).";
            txtProgress.Focus();
            return; 
        }

        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT PlanFrom,PlanTo,StartFrom,ExecPerDate FROM DD_Docket_RFQ_Jobs  WHERE RFQId=" + RFQId + " AND DocketId=" + DocketId + " AND jobCode='" + jobCode + "'");
        DateTime? PlanDate = null;
        DateTime? PlanDate1 = null;
        DateTime? StartFrom = null;
        DateTime? ExecPerDate = null;

        if (dt.Rows.Count > 0)
        {
            try
            {
                PlanDate = Convert.ToDateTime(dt.Rows[0]["PlanFrom"]);
                PlanDate1 = Convert.ToDateTime(dt.Rows[0]["PlanTo"]);
                StartFrom = Convert.ToDateTime(dt.Rows[0]["StartFrom"]);
                ExecPerDate = Convert.ToDateTime(dt.Rows[0]["ExecPerDate"]);
            }
            catch (Exception ex) { }
        }
        DateTime dtStart;
        try
        {
            dtStart = Convert.ToDateTime(txtStartDate.Text);
        }
        catch
        {
            lbl_MsgRemarks.Text = "Please enter valid date.";
            txtStartDate.Focus();
            return;
        }

        if (dtStart < ExecPerDate)
        {
            lbl_MsgRemarks.Text = "Date must be more than last progress date.";
            return;
        }


        string ExecRemarks = txtRemarks.Text.Trim().Replace("'","`");
        Common.Execute_Procedures_Select_ByQuery("dbo.DD_InsertUpdate_Progress " + RFQId + "," + hfdDocketJobId.Value + "," + DocketId + ",'" + txtStartDate.Text + "','" + txtProgress.Text + "','" + ExecRemarks + "'");       

        lbl_MsgRemarks.Text = "Recors saved successfully.";
    }
    protected void btn_CloseStart_Click(object sender, EventArgs e)
    {
        //LoadCategory();
        lbl_MsgRemarks.Text = "";
    }
    protected void lnkSubJobs_Click(object sender, EventArgs e)
    {
        DataTable dtSubJobs = Common.Execute_Procedures_Select_ByQuery("SELECT [SubJobCode],[SubJobName],[Unit],LongDescr, CostCategory, OutsideRepair FROM [dbo].[DD_Docket_RFQ_SubJobs] WHERE DocketId = " + DocketId + " AND RFQID = " + RFQId + " AND LEFT(SubJobCode,6) = '" + txtSubJobCode.Text.Trim()  + "' ");
        rptSubJobs.DataSource = dtSubJobs;
        rptSubJobs.DataBind();
    }
    protected void btnReLoadData_Click(object sender, EventArgs e)
    {
        ForDate = txtForDate.Text.Trim();
        LoadCategory();
    }

    protected void lbUpdateDuration_Click(object sender, EventArgs e)
    {
        btnExecuteRFQ.Visible = true;
        dv_RFQExecution.Visible = true;

        DataTable dt= Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DD_Docket_RFQ_Master WHERE [RFQId]=" + RFQId);
        txtExecFrom.Text=Common.ToDateString(dt.Rows[0]["ExecFrom"]);
        txtExecTo.Text=Common.ToDateString(dt.Rows[0]["ExecTo"]);

    }    
    protected void btnExecuteRFQ_Click(object sender, EventArgs e)
    {


        DateTime dt;

        if (txtExecFrom.Text.Trim() == "")
        {
            lblMsg_Exec.Text = "Please enter From date.";
            txtExecFrom.Focus();
            return;
        }
        if (!DateTime.TryParse(txtExecFrom.Text.Trim(), out dt))
        {
            lblMsg_Exec.Text = "Please enter valid date.";
            txtExecFrom.Focus();
            return;
        }
        if (txtExecTo.Text.Trim() == "")
        {
            lblMsg_Exec.Text = "Please enter To date.";
            txtExecTo.Focus();
            return;
        }

        if (!DateTime.TryParse(txtExecTo.Text.Trim(), out dt))
        {
            lblMsg_Exec.Text = "Please enter valid date.";
            txtExecTo.Focus();
            return;
        }
        if (DateTime.Parse(txtExecFrom.Text.Trim()) > DateTime.Parse(txtExecTo.Text.Trim()))
        {
            lblMsg_Exec.Text = "From date should less than To date.";
            txtExecFrom.Focus();
            return;
        }
        try
        {
            Common.Execute_Procedures_Select_ByQuery("UPDATE DD_Docket_RFQ_Master SET [ExecFrom]= '" + txtExecFrom.Text.Trim() + "', [ExecTo]='" + txtExecTo.Text.Trim() + "', [Executed]='Y' WHERE [RFQId]=" + RFQId);
            Common.Execute_Procedures_Select_ByQuery("UPDATE DD_DocketMaster SET [Act_Startdate]= '" + txtExecFrom.Text.Trim() + "', [Act_EndDate]='" + txtExecTo.Text.Trim() + "', Status='E' WHERE [DocketId]=" + DocketId);
            Common.Execute_Procedures_Select_ByQuery("DELETE FROM DD_Docket_RFQ_Jobs_Planning WHERE [DocketId]=" + DocketId + " AND RFQID=" + RFQId + " AND (FOR_DATE NOT BETWEEN '" + txtExecFrom.Text.Trim() + "' AND '" + txtExecTo.Text.Trim() + "')");
            
            btnExecuteRFQ.Visible = false;
            lblMsg_Exec.Text = "Duration updated successfully.";
        }
        catch (Exception ex)
        {
            lblMsg_Exec.Text = "Unable to update duration. Error: " + ex.Message;
        }


    }
    protected void btnCloseExec_Click(object sender, EventArgs e)
    {
        //DocketId = 0;
        //RFQId = 0;
        txtExecFrom.Text = "";
        txtExecTo.Text = "";
        ShowDocketSummary();
        LoadCategory();
        //BindDockets();
        dv_RFQExecution.Visible = false;
    }

    protected void btn_SendMail_Click(object sender, EventArgs e)
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

            //GROUPEMAIL="pankaj.k@esoftech.com";
            //USERMAIL = "emanager@energiossolutions.com";
            //CreatePDF(DOCKETNO, dtSummary1, dtCats1, dtJobs1, d2, "Tomorrow.pdf");
            string[] To = { GROUPEMAIL };
            string[] CC = { USERMAIL };
            string err = "";
            string msg = "<br>Attached find the Drydock update.<br><br><br>" + "Thanks<br><br><b>" + FULLNAME + "</b><br><i>(" + POSITIION + ")</i>";
            SaveXLS();
            string FileName = Server.MapPath("~/DryDock/JobUpdateExcel.xls");
            EProjectCommon.SendeMail_MTM(LoginId, "emanager@energiossolutions.com", "emanager@energiossolutions.com", To, CC, CC, DOCKETNO + " - DD Update : " + DateTime.Today.ToString("dd-MMM-yyyy"), msg, out err, FileName);
            lbl_MsgRemarks.Text = "Mail send successfully.";
        }
    }
    protected void Refresh_Click(object sender,EventArgs e)
    {
        LoadCategory();
    }
    protected void SaveXLS()
    {
        //------------------------------------------
        StringBuilder sb = new StringBuilder();

        sb.Append("<table cellspacing='0' cellpadding='0' width='100%' border='1'>");


        DataTable dtSummary = Common.Execute_Procedures_Select_ByQuery("SELECT R.*,(SELECT VESSELNAME FROM DBO.VESSEL V WHERE V.VESSELID=D.VESSELID) AS VESSELNAME FROM DD_Docket_RFQ_Master R INNER JOIN DD_DocketMaster D ON D.DocketId = R.DocketId WHERE R.RFQId=" + RFQId + " AND R.DocketId=" + DocketId.ToString());

        DateTime ExecFrom = Convert.ToDateTime(dtSummary.Rows[0]["EXECFROM"]);
        DateTime ExecTo = Convert.ToDateTime(dtSummary.Rows[0]["EXECTO"]);

        DataTable dtCats = Common.Execute_Procedures_Select_ByQuery("SELECT DISTINCT CATID,CATCODE,CATNAME FROM DD_Docket_RFQ_Jobs WHERE RFQID=" + RFQId + " AND DOCKETID=" + DocketId);
        DataTable dtJobs = Common.Execute_Procedures_Select_ByQuery("SELECT CATID,DocketJobId,JOBCODE,JOBNAME,JobDesc,( SELECT TOP 1 JP.PER FROM DD_Docket_RFQ_Jobs_Planning JP WHERE JP.DocketJobId=J.DocketJobId AND JP.RFQID=" + RFQId + " AND JP.DOCKETID=" + DocketId + " AND ISNULL(JP.PER,'')<>'' ORDER BY JP.FOR_DATE DESC ) AS EXECPER,( SELECT TOP 1 JP.REMARK FROM DD_Docket_RFQ_Jobs_Planning JP WHERE JP.DocketJobId=J.DocketJobId AND JP.RFQID=" + RFQId + " AND JP.DOCKETID=" + DocketId + " AND ISNULL(JP.PER,'')<>'' ORDER BY JP.FOR_DATE DESC ) AS EXECREMARKS FROM DD_Docket_RFQ_Jobs J WHERE RFQID=" + RFQId + " AND DOCKETID=" + DocketId + " ORDER BY JOBCODE");
        DataTable dtJobsPer = Common.Execute_Procedures_Select_ByQuery("SELECT DocketJobId,FOR_DATE,PER,REMARK FROM DD_Docket_RFQ_Jobs_Planning JP WHERE JP.RFQID=" + RFQId + " AND JP.DOCKETID=" + DocketId);

        // Header -------------------------------------
        sb.Append("<tr>");
        sb.Append("<td style='background-color:grey;'><b>Job List</b></td>");
        DateTime tmp = ExecFrom;
        while (tmp <= ExecTo)
        {
            //-------------------
            sb.Append("<td style='background-color:grey;'><b>" + tmp.ToString("dd-MMM") + "</b></td>");
            //-------------------
            tmp = tmp.AddDays(1);
            //-------------------
        }
        sb.Append("<td>Remarks</td>");
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
                sb.Append("<td colspan='" + (Days + 1).ToString() + "'></td>");
                sb.Append("</tr>");
                //-----------------------------------------
                foreach (DataRow drIn in dtJobs_Filered.Rows)
                {
                    sb.Append("<tr>");
                    sb.Append("<td>" + drIn["JOBCODE"].ToString() + ":" + drIn["JOBNAME"].ToString() + "</td>");
                    tmp = ExecFrom;
                    while (tmp <= ExecTo)
                    {
                        //-------------------
                        DataView dv1 = dtJobsPer.DefaultView;
                        dv1.RowFilter = "DocketJobId=" + drIn["DocketJobId"].ToString() + " AND FOR_DATE='" + tmp.ToString("dd-MMM-yyyy") + "'";
                        DataTable dt1 = dv1.ToTable();

                        if (dt1.Rows.Count > 0)
                        {
                            if (dt1.Rows[0]["PER"].ToString().Trim() != "")
                                sb.Append("<td style='background-color:#70DB70'>" + dt1.Rows[0]["PER"].ToString() + "%</td>");
                            else
                                sb.Append("<td style='background-color:#FFFF80'>&nbsp;</td>");
                        }
                        else
                            sb.Append("<td>&nbsp;</td>");

                        //-------------------
                        tmp = tmp.AddDays(1);
                        //-------------------
                    }

                    sb.Append("<td>" + drIn["EXECREMARKS"].ToString() + "</td>");
                    sb.Append("</tr>");
                }
                //-----------------------------------------
            }
        }
        sb.Append("</table>");
        
        File.WriteAllText(Server.MapPath("~/DryDock/JobUpdateExcel.xls"),sb.ToString());

        //------------------------------------------
    }
    //Commoprivate void CreatePDF(string DDNO, DataTable dtSummary, DataTable dtCats, DataTable dtJobs, string ForDate, string FileName)
    //{
    //    try
    //    {

    //        Document document = new Document(PageSize.A4, 0f, 0f, 10f, 10f);
    //        System.IO.MemoryStream msReport = new System.IO.MemoryStream();
    //        PdfWriter writer = PdfWriter.GetInstance(document, msReport);
    //        document.Open();

    //        //------------ TABLE HEADER FONT 
    //        iTextSharp.text.Font fCapText_11_Reg = FontFactory.GetFont("ARIAL", 8, iTextSharp.text.Font.NORMAL);
    //        iTextSharp.text.Font fCapText_10_Reg = FontFactory.GetFont("ARIAL", 7, iTextSharp.text.Font.NORMAL);
    //        iTextSharp.text.Font fCapText_11 = FontFactory.GetFont("ARIAL", 8, iTextSharp.text.Font.BOLD);
    //        iTextSharp.text.Font fCapText_13 = FontFactory.GetFont("ARIAL", 10, iTextSharp.text.Font.BOLD);
    //        iTextSharp.text.Font fCapText_15 = FontFactory.GetFont("ARIAL", 12, iTextSharp.text.Font.BOLD);


    //        //=============================================================================
    //        // Page -1 
    //        PdfPTable tblPage1 = new PdfPTable(1);
    //        tblPage1.SplitLate = false;
    //        tblPage1.SplitRows = true;


    //        tblPage1.HorizontalAlignment = Element.ALIGN_CENTER;
    //        float[] wsCom = { 100 };
    //        tblPage1.SetWidths(wsCom);

    //        float[] wsCom_90 = { 90 };
    //        tblPage1.SetWidths(wsCom);

    //        // Heading -----------------
    //        PdfPCell cell = new PdfPCell(new Phrase("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\nDrydock Update\n\n", fCapText_15));
    //        cell.BorderColor = BaseColor.WHITE;
    //        cell.HorizontalAlignment = Element.ALIGN_CENTER;
    //        tblPage1.AddCell(cell);
    //        // Vessel Name -----------------
    //        cell = new PdfPCell(new Phrase(dtSummary.Rows[0]["VESSELNAME"].ToString() + "\n\n", fCapText_13));
    //        cell.BorderColor = BaseColor.WHITE;
    //        cell.HorizontalAlignment = Element.ALIGN_CENTER;
    //        tblPage1.AddCell(cell);
    //        // DocketNo -----------------
    //        cell = new PdfPCell(new Phrase(DDNO + " as on " + ForDate + "\n\n", fCapText_13));
    //        cell.BorderColor = BaseColor.WHITE;
    //        cell.HorizontalAlignment = Element.ALIGN_CENTER;
    //        tblPage1.AddCell(cell);

    //        document.Add(tblPage1);
    //        //=============================================================================
    //        // Page-2 
    //        document.NewPage();

    //        PdfPTable tblPage2 = new PdfPTable(1);
    //        tblPage2.SplitLate = false;
    //        tblPage2.SplitRows = true;

    //        tblPage2.HorizontalAlignment = Element.ALIGN_CENTER;
    //        tblPage2.SetWidths(wsCom);
    //        // Heading -----------------
    //        cell = new PdfPCell(new Phrase("CONTENTS", fCapText_15));
    //        cell.BorderColor = BaseColor.WHITE;
    //        cell.HorizontalAlignment = Element.ALIGN_CENTER;
    //        tblPage2.AddCell(cell);

    //        document.Add(tblPage2);

    //        PdfPTable tblPage2_1 = new PdfPTable(2);
    //        tblPage2_1.SplitLate = false;
    //        tblPage2_1.SplitRows = true;

    //        float[] wsCom_2_1 = { 5, 95 };
    //        tblPage2_1.HorizontalAlignment = Element.ALIGN_CENTER;
    //        tblPage2_1.SetWidths(wsCom_2_1);

    //        foreach (DataRow dr in dtCats.Rows)
    //        {
    //            cell = new PdfPCell(new Phrase(dr["CATCODE"].ToString(), fCapText_11));
    //            cell.BorderColor = BaseColor.BLACK;
    //            cell.HorizontalAlignment = Element.ALIGN_CENTER;
    //            tblPage2_1.AddCell(cell);

    //            cell = new PdfPCell(new Phrase(dr["CATNAME"].ToString(), fCapText_11));
    //            cell.BorderColor = BaseColor.BLACK;
    //            cell.HorizontalAlignment = Element.ALIGN_LEFT;
    //            tblPage2_1.AddCell(cell);
    //        }

    //        document.Add(tblPage2_1);

    //        //=============================================================================
    //        foreach (DataRow drC in dtCats.Rows)
    //        {
    //            document.NewPage();

    //            PdfPTable tblPage_Cats = new PdfPTable(1);
    //            tblPage_Cats.SplitLate = false;
    //            tblPage_Cats.SplitRows = true;

    //            tblPage_Cats.HorizontalAlignment = Element.ALIGN_CENTER;
    //            tblPage_Cats.SetWidths(wsCom);
    //            // Heading -----------------
    //            cell = new PdfPCell(new Phrase(drC["CATCODE"].ToString() + " :" + drC["CATNAME"].ToString(), fCapText_15));
    //            cell.BorderColor = BaseColor.WHITE;
    //            cell.HorizontalAlignment = Element.ALIGN_CENTER;

    //            tblPage_Cats.AddCell(cell);
    //            document.Add(tblPage_Cats);

    //            DataView dvJobs = dtJobs.DefaultView;
    //            dvJobs.RowFilter = "CATID=" + drC["CATID"].ToString();
    //            DataTable dtJobs_Temp = dvJobs.ToTable();

    //            PdfPTable tblPage_Jobs = new PdfPTable(2);
    //            tblPage_Jobs.SplitLate = false;
    //            tblPage_Jobs.SplitRows = true;

    //            float[] wsCom_Jobs = { 7, 93 };
    //            tblPage_Jobs.HorizontalAlignment = Element.ALIGN_CENTER;
    //            tblPage_Jobs.SetWidths(wsCom_Jobs);

    //            foreach (DataRow drJ in dtJobs_Temp.Rows)
    //            {
    //                cell = new PdfPCell(new Phrase(drJ["JOBCODE"].ToString(), fCapText_11));
    //                cell.BorderColor = BaseColor.BLACK;
    //                cell.HorizontalAlignment = Element.ALIGN_CENTER;
    //                tblPage_Jobs.AddCell(cell);

    //                PdfPTable tblPage_SubJobs = new PdfPTable(2);
    //                tblPage_SubJobs.SplitLate = false;
    //                tblPage_SubJobs.SplitRows = true;

    //                float[] wsCom_Jobs1 = { 93, 7 };

    //                tblPage_SubJobs.HorizontalAlignment = Element.ALIGN_CENTER;
    //                tblPage_SubJobs.SetWidths(wsCom_Jobs1);

    //                cell.AddElement(new Phrase(drJ["JOBNAME"].ToString() + "\n", fCapText_11));
    //                //cell.AddElement(new Phrase("Job Description : ", fCapText_11));
    //                //cell.AddElement(new Phrase(drJ["JobDesc"].ToString(), fCapText_10_Reg));
    //                cell.AddElement(new Phrase("Remarks : ", fCapText_11));
    //                cell.AddElement(new Phrase(drJ["EXECREMARKS"].ToString(), fCapText_10_Reg));
    //                cell.BorderColor = BaseColor.BLACK;
    //                cell.HorizontalAlignment = Element.ALIGN_LEFT;
    //                tblPage_SubJobs.AddCell(cell);

    //                cell = new PdfPCell(new Phrase(drJ["EXECPER"].ToString() + " %", fCapText_11));
    //                cell.BorderColor = BaseColor.BLACK;
    //                cell.HorizontalAlignment = Element.ALIGN_CENTER;
    //                tblPage_SubJobs.AddCell(cell);

    //                cell = new PdfPCell(tblPage_SubJobs);
    //                cell.BorderColor = BaseColor.BLACK;
    //                cell.HorizontalAlignment = Element.ALIGN_LEFT;

    //                tblPage_Jobs.AddCell(cell);
    //            }

    //            document.Add(tblPage_Jobs);
    //        }
    //        //=============================================================================

    //        document.Close();
    //        if (File.Exists(Server.MapPath("~/DryDock/" + FileName)))
    //        {
    //            File.Delete(Server.MapPath("~/DryDock/" + FileName));
    //        }
    //        FileStream fs = new FileStream(Server.MapPath("~/DryDock/" + FileName), FileMode.Create);
    //        byte[] bb = msReport.ToArray();
    //        fs.Write(bb, 0, bb.Length);
    //        fs.Flush();
    //        fs.Close();

    //        //ScriptManager.RegisterStartupScript(this, this.GetType(), "RFQ Print", "window.open('RFQ.pdf?rnd=" + rnd.Next() + "','','','');", true);

    //    }
    //    catch (System.Exception ex)
    //    {
    //        lblMsgMain.Text = "Unable to print. Error : " + ex.Message;
    //    }
    //}

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        EditMode = "N";
        DR = "N";
        spn_DR.Visible = false;
        //btn_Edit.Visible = true;
        tdStatus.Visible = true;
        btn_Save.Visible = false;
        btn_Cancel.Visible = false;
        btn_DailyReport.Visible = true;
        btnEnterComments.Visible = false;
        LoadCategory();
    }
    protected void btn_DailyReport_Click(object sender, EventArgs e)
    {
        EditMode = "N";
        spn_DR.Visible = true;
        DR = "Y";
        //btn_Edit.Visible = false;

        tdStatus.Visible = false;
        ddlStatus.SelectedIndex = 0;
        txtUpdatedOn.Text = "";

        btn_Save.Visible = false;
        btn_Cancel.Visible = true;
        btn_DailyReport.Visible = false;
        btnEnterComments.Visible = true;
        LoadCategory();
    }    
    protected void btnEnterComments_Click(object sender, EventArgs e)
    {
        if (txtSaveDate.Text.Trim() == "")
        {
            lblMsgMain.Text = "Please enter date for update to fill progress.";
            txtSaveDate.Focus();
            return;
        }
        else
        {
            DateTime Temp = Convert.ToDateTime(txtSaveDate.Text);
            if (Temp < ExecFromDate || Temp > ExecToDate)
            {
                lblMsgMain.Text = "Update date must be in DD execution date.";
                txtSaveDate.Focus();
                return;
            }
        
        }

        List<string> JObsList = new List<string>();
        DataTable dtJobs = Common.Execute_Procedures_Select_ByQuery("SELECT DocketJobId FROM DD_Docket_RFQ_Jobs WHERE RFQID=" + RFQId + " AND DOCKETID=" + DocketId + " AND CATID=" + ddlJObCat.SelectedValue + " ORDER BY JOBCODE");
        foreach (DataRow dr in dtJobs.Rows)
        {
            string ctlname = "chk_" + dr["DocketJobId"].ToString();
            if (Request.Form[ctlname] != null)
            {
                if (Request.Form[ctlname].ToString()=="on")
                {
                    JObsList.Add(dr["DocketJobId"].ToString());
                }
                
            }
        }
        if (JObsList.Count > 0)
        {
            Session.Add("DocketJobId", string.Join(",", JObsList.ToArray()));
            ScriptManager.RegisterStartupScript(this, this.GetType(), "fasd", "window.open('JobTrackingRemarkEntry.aspx?DocketId=" + DocketId + "&RFQId=" + RFQId + "&CatId=" + ddlJObCat.SelectedValue + "&ForDate=" + txtSaveDate.Text + "','');", true);
        }
    }
 }
