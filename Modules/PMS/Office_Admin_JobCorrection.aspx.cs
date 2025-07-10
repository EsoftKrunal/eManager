using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Text;
using Ionic.Zip;
using System.Configuration;

public partial class Office_Admin_JobCorrection : System.Web.UI.Page
{
    public int CorrectionId
    {
        get { return Common.CastAsInt32( ViewState["CorrectionId"] ); }
        set { ViewState["CorrectionId"] = value; }
    }
    public int RecordCount
    {
        get { return Common.CastAsInt32( ViewState["RecordCount"] ); }
        set { ViewState["RecordCount"] = value; }
    }

    public string vesselcode
    {
        get { return Convert.ToString( ViewState["vesselcode"] ); }
        set { ViewState["vesselcode"] = value; }
    }
     public int historyid
    {
        get { return Common.CastAsInt32( ViewState["historyid"] ); }
        set { ViewState["historyid"] = value; }
    }
        protected void Page_Load(object sender, EventArgs e)
    {
        lblMsgCorrectionRemarks.Text = "";
        if (!Page.IsPostBack)
        {
            Debug.Write(Session["loginid"].ToString());
            Trace.Write(Session["loginid"].ToString());
            BindVessels();
            BindCorrections(sender,e);
        }
    }
    public void BindCorrections(object sender,EventArgs e)
    {
        string whereclause=" where 1=1 ";
        if(ddlVessels.SelectedIndex>0)
        {
            whereclause+=" and vesselcode='" + ddlVessels.SelectedValue + "'";
        }
        if(ddlStatus.SelectedValue=="O")
        {
            whereclause+=" and ShipRecdOn is null and ExportedOn is null";
        }
        if (ddlStatus.SelectedValue == "P")
        {
            whereclause += " and ExportedOn is not null and ShipRecdOn is null";
        }
        if (ddlStatus.SelectedValue=="C")
        {
            whereclause+=" and ShipRecdOn is not null";
        }
        rptCorrections.DataSource=Common.Execute_Procedures_Select_ByQuery("select TableId,VesselCode,HistoryId,CorrectionBy,CorrectioOn,(case when ShipRecdOn is not null then 'Closed' when ExportedOn is not null then 'Procesed' else 'Open' end) as Status  from VSL_VesselJobHistory_CorrectionMaster " + whereclause + " order by CorrectioOn desc");
        rptCorrections.DataBind();

        //CorrectionId = 0;
        historyid = 0;
        vesselcode = "";
        rptJobHistory.DataSource = null;
        rptJobHistory.DataBind();

        btnCorreection.Visible = false;
        btnCancelCorrection.Visible = false;
        btnExportToShip.Visible = false;
        lblheading.Text = "";
    } 
    private void BindVessels()
    {
        DataTable dtVessels = new DataTable();
        string strvessels = "SELECT VesselId,VesselCode,VesselName FROM Vessel VM WHERE EXISTS(SELECT TOP 1 COMPONENTID FROM  VSL_ComponentMasterForVessel EVM WHERE VM.VesselCode = EVM.VesselCode ) ORDER BY VesselName ";
        strvessels = " Select VesselId,VesselCode,VesselName  from dbo.Vessel where vesselstatusid=1 order by vesselname";
        dtVessels = Common.Execute_Procedures_Select_ByQuery(strvessels);
        if (dtVessels.Rows.Count > 0)
        {
            ddlVessels.DataSource = dtVessels;
            ddlVessels.DataTextField = "VesselName";
            ddlVessels.DataValueField = "VesselCode";
            ddlVessels.DataBind();
        }
        else
        {
            ddlVessels.DataSource = null;
            ddlVessels.DataBind();
        }
        ddlVessels.Items.Insert(0, new ListItem("< All Vessel >", ""));
    }
    protected void btnSelect_Click(object sender, EventArgs e)
    {
        btnCorreection.Visible=false;
        btnCancelCorrection.Visible = false;
        btnExportToShip.Visible = false;
        lblheading.Text = "";

        lblJobInterval.Text = "";
        lblLastDoneDate.Text = "";
        lblLastDoneHour.Text = "";
        lblNextDueDate.Text = "";
        lblNextDueHour.Text = "";
        lblRemarks.Text = "";

        rptJobHistory.DataSource = null;
        rptJobHistory.DataBind();
        lblComponentListCounter.Text = "0 records found.";

        try
        {
        CorrectionId =Common.CastAsInt32(((LinkButton)sender).CommandArgument);
        }catch{}

        BindCorrections(sender,e);
        
        if (CorrectionId>0)
        {
            string sql = "select vesselcode,historyid,ExportedOn,ShipRecdOn from VSL_VesselJobHistory_CorrectionMaster where tableid=" + CorrectionId;
            sql = "select vesselcode,historyid,ExportedOn,ShipRecdOn,cm.IntervalId,Remarks,Interval,lastdone,lastdonehr,nextdue,nextduehr,jm.IntervalName from VSL_VesselJobHistory_CorrectionMaster cm inner join JobIntervalMaster jm on cm.IntervalId=jm.IntervalId where tableid=" + CorrectionId;
            DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
            if(dt.Rows.Count>0)
            {
                vesselcode=Convert.ToString(dt.Rows[0]["vesselcode"]);
                historyid=Common.CastAsInt32(dt.Rows[0]["historyid"]);
                ShowDetails();
                btnCorreection.Visible=Convert.IsDBNull(dt.Rows[0]["ExportedOn"]);
                btnCancelCorrection.Visible = btnCorreection.Visible;

                btnExportToShip.Visible = Convert.IsDBNull(dt.Rows[0]["ShipRecdOn"]) && ( ! Convert.IsDBNull(dt.Rows[0]["ExportedOn"]));
                lblheading.Text = " Vessel : " + vesselcode + ", HistoryId : " + historyid;

                lblJobInterval.Text = Convert.ToString(dt.Rows[0]["Interval"])+ Convert.ToString(dt.Rows[0]["IntervalName"]);

                lblLastDoneDate.Text = Common.ToDateString(dt.Rows[0]["lastdone"]) ;
                lblLastDoneHour.Text =  Convert.ToString(dt.Rows[0]["lastdonehr"]);

                lblNextDueDate.Text = Common.ToDateString(dt.Rows[0]["nextdue"]) ;
                lblNextDueHour.Text = Convert.ToString(dt.Rows[0]["nextduehr"]);


                lblRemarks.Text = Convert.ToString(dt.Rows[0]["Remarks"]);

            }
        }

    }

    #region -------------------- Office Component ---------------------
    private void ShowDetails()
    {
        string strSQL = " ";
        strSQL = "SELECT JH.ACTION AS rtype,JH.HistoryId,CM.ComponentName,JH._ComponentCode,JM.JobId,JH.VesselCode,convert(varchar,JH.HistoryId) as PK,CASE JH.[Action] WHEN 'R' THEN 'REPORT' WHEN 'P' THEN 'POSTPONE' WHEN 'C' THEN 'CANCEL' END AS [Action],CASE JH.[Action] WHEN 'R' THEN REPLACE(Convert(Varchar,JH.DoneDate,106),' ','-') WHEN 'P' THEN REPLACE(Convert(Varchar, JH.PostPoneDate,106),' ','-') WHEN 'C' THEN REPLACE(Convert(Varchar, JH.CancellationDate,106),' ','-') END AS ACTIONDATE,CASE JH.[Action] WHEN 'R' THEN DoneHour ELSE '' END  AS DoneHour,CASE JH.[Action] WHEN 'R' THEN CASE REPLACE(CONVERT(VARCHAR(11), ISNULL(JH.LastDueDate,''),106),' ','-') WHEN '01-Jan-1900' THEN '' ELSE DATEDiff(dd,JH.DoneDate,JH.LastDueDate) END ELSE '' END AS [Difference],CASE [Action] WHEN 'R' THEN RM.RankCode WHEN 'P' THEN RMP.RankCode WHEN 'C' THEN RMC.RankCode END AS DoneBy,CASE [Action] WHEN 'R' THEN DoneBy_Code WHEN 'P' THEN P_DoneBy_Code WHEN 'C' THEN C_DoneBy_Code END AS EmpNo, "+ 
		"CASE [Action] WHEN 'R' THEN DoneBy_Name WHEN 'P' THEN P_DoneBy_Name WHEN 'C' THEN C_DoneBy_Name END AS EmpName,ISNULL(REPLACE(Convert(Varchar, LastDueDate,106),' ','-'),'') AS DueDate,CASE JH.[Action] WHEN 'R' THEN LastDueHours ELSE '' END AS DueHour,CASE JH.[Action] WHEN 'R' THEN CASE UpdateRemarks WHEN 1 THEN 'Planned Job' WHEN 2 THEN CASE WHEN LEN(Specify) > 45 THEN SUBSTRING(Specify,0,45) + '...' ELSE Specify END WHEN 3 THEN 'BREAK DOWN' END WHEN 'P' THEN CASE PostPoneReason WHEN 1 THEN 'Equipment in working condition' WHEN 2 THEN 'Waiting for spares' WHEN 3 THEN 'Dry docking' END WHEN 'C' THEN CASE WHEN LEN(CancellationReason)> 45 THEN SUBSTRING(CancellationReason,0,45) + '...' ELSE CancellationReason END END AS Remarks,ConditionAfter ,JobCode,DESCRSH AS JobDesc  " +
                "FROM   " +
                "VSL_VesselJobUpdateHistory JH   " +
                "INNER JOIN VSL_VesselComponentJobMaster CJM1 ON JH.VESSELCODE=CJM1.VESSELCODE AND JH.COMPJOBID=CJM1.COMPJOBID AND CJM1.STATUS='A' " +
                "INNER JOIN VSL_ComponentMasterForVessel CMV ON JH.VESSELCODE=CMV.VESSELCODE AND JH.COMPONENTID=CMV.COMPONENTID AND CMV.STATUS='A' " +
                "INNER JOIN jobmaster jm on jm.jobid=JH.jobid " +
                "INNER JOIN componentmaster cm on cm.componentid=jh.componentid " +
                "LEFT JOIN ComponentsJobMapping CJM ON CJM.COMPJOBID=JH.COMPJOBID " +
                "LEFT JOIN Rank RM ON RM.RankId = JH.DoneBy   " +
                "LEFT JOIN Rank RMP ON RMP.RankId = JH.P_DoneBy   " +
                "LEFT JOIN Rank RMC ON RMC.RankId = JH.C_DoneBy   " +
                "WHERE JH.VesselCode = '"+ vesselcode + "' AND HistoryId >=" + historyid + " AND jh.CompJobId in (SELECT COMPJOBID FROM VSL_VesselJobUpdateHistory WHERE VesselCode='"+ vesselcode + "' AND HistoryId=" + historyid + ") ";
        
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(strSQL);
        
        rptJobHistory.DataSource = dt;
        rptJobHistory.DataBind();

        RecordCount = dt.Rows.Count;
        lblComponentListCounter.Text = RecordCount + " records found.";       

    }
    #endregion
    
    protected void btnCorrection_Click(object sender, EventArgs e)
    {
        if (RecordCount <= 0)
        {
            MessageBox1.ShowMessage("No records available for rejection", true);
            return;
        }
        litrecordCount.Text = RecordCount.ToString();
        divCorrection.Visible = true;
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("select remarks from VSL_VesselJobHistory_CorrectionMaster where tableid=" + CorrectionId);
        if(dt.Rows.Count>0)
            lblRRemarks.Text =dt.Rows[0]["remarks"].ToString();
        else
            lblRRemarks.Text ="";
    }
    protected void btnSave_Correction_OnClick(object sender, EventArgs e)
    {
        if (historyid <= 0 || Convert.ToString(vesselcode)=="")
        {
            lblMsgCorrectionRemarks.Text= "Please select rejection request to continue.";
            return;
        }
        // if (txtRemarks.Text=="")
        // {
        //     lblMsgCorrectionRemarks.Text = "Please enter remarks.";
        //     txtRemarks.Focus();
        //     return;
        // }

        Common.Set_Procedures("DBO.JobCorrection");
        Common.Set_ParameterLength(9);
        Common.Set_Parameters(
	    new MyParameter("@VesselCode", vesselcode),
            new MyParameter("@HistoryId", historyid),
            new MyParameter("@UserName", Session["FullName"].ToString()),
            new MyParameter("@Remarks", txtRemarks.Text.Trim()),

            new MyParameter("@LastDoneDate", lblLastDoneDate.Text.Trim()),
            new MyParameter("@LastDoneHr", lblLastDoneHour.Text.Trim()),
            new MyParameter("@NextDueDate", lblNextDueDate.Text.Trim()),
            new MyParameter("@NextDueHr", lblNextDueHour.Text.Trim()),

            new MyParameter("@Mode", 'O')
            );
        DataSet ds = new DataSet();
        if (Common.Execute_Procedures_IUD(ds))
        {
            btnExportToShip.Visible = true;
            btnCancelCorrection.Visible = false;
            lblMsgCorrectionRemarks.Text = "Rejection made successfully. Please send update to ship.";
        }
        else
        {
            lblMsgCorrectionRemarks.Text = "Unable to save record.Error : " + Common.getLastError();
        }
    }
    protected void btnClose_CorrectionPopup_OnClick(object sender, EventArgs e)
    {
        divCorrection.Visible = false;
        txtRemarks.Text = "";
    }
    protected void btnCancelCorrection_Click(object sender, EventArgs e)
    {
        Common.Execute_Procedures_Select_ByQuery("DELETE FROM dbo.VSL_VesselJobHistory_CorrectionMaster WHERE tableid=" + CorrectionId );
        BindCorrections(sender,e);
        CorrectionId=0;
        btnSelect_Click(sender,e);

        btnCorreection.Visible=false;
        btnCancelCorrection.Visible = false;
        btnExportToShip.Visible = false;
        lblheading.Text = "";
        //-----------------
    }
    
    protected void btnExportToShip_OnClick(object sender, EventArgs e)
    {
        DataTable dt11=Common.Execute_Procedures_Select_ByQuery("SELECT (select remarks from VSL_VesselJobHistory_CorrectionMaster where tableid=" + CorrectionId + ") as remarks,DescrSh,JobCode=(SELECT JObCode FROM JobMaster WHERE JobMaster.JobId=ComponentsJobMapping.JOBID),email=(select VesselEmailNew from dbo.vessel where vesselcode='" + vesselcode + "'),ComponentName = (SELECT rtrim(COMPONENTCODE) + ' - ' + COMPONENTNAME FROM ComponentMaster CM WHERE CM.ComponentId=ComponentsJobMapping.ComponentId) FROM ComponentsJobMapping WHERE COMPJOBID IN ( select TOP 1 COMPJOBID from VSL_VesselJobHistory_CorrectionDetails where correctionid=" + CorrectionId + " and vesselcode='" + vesselcode + "')");
        String email="",remarks="",compcode="",job="",jobcode="";
        if(dt11.Rows.Count>0)
        {
            email=Convert.ToString(dt11.Rows[0]["email"]);
            remarks=Convert.ToString(dt11.Rows[0]["remarks"]);
            compcode=Convert.ToString(dt11.Rows[0]["componentname"]);
            jobcode=Convert.ToString(dt11.Rows[0]["jobcode"]);
            job=Convert.ToString(dt11.Rows[0]["descrsh"]);
        }

        if(email.Trim()!="")
        {
            bool FileDone = false;
            string SaveTargetDir = Server.MapPath("~/Modules/PMS/TEMP/");
            string ConfigFile = SaveTargetDir + "PacketConfig.xml";
            string ScriptFile = SaveTargetDir + "Script.sql";
            String _FileName= vesselcode +"-JobCorrrection-" + historyid.ToString() + ".zip";
            string ZipData = SaveTargetDir + _FileName ;

            if (File.Exists(ConfigFile))
                File.Delete(ConfigFile);
            if (File.Exists(ScriptFile))
                File.Delete(ScriptFile);
            if (File.Exists(ZipData))
                File.Delete(ZipData);


            string text= "<?xml version='1.0' encoding='UTF-8'?>" +
                        "<configuration>" +
                        "<PacketName>" + _FileName + "</PacketName>" +
                        "<PacketType>SCRIPT</PacketType>" +
                        "<PacketDataType>PMS</PacketDataType>" +
                        "<Reply>N</Reply>" +
                        "</configuration>";
            
            File.WriteAllText(ConfigFile, text);
            string dbName = ConfigurationManager.AppSettings["DBName"].ToString();
            text = "USE ["+ dbName + "] " + Environment.NewLine+
                "GO " + Environment.NewLine +
                "BEGIN TRY " + Environment.NewLine +
                "    BEGIN TRAN " + Environment.NewLine +
                "    EXEC DBO.[JobCorrection] '@VesselCode',@HistoryId,'@UserName','@Remarks',@LastDoneDate,@LastDoneHr,@NextDueDate,@NextDueHr,'S' " + Environment.NewLine +
                "    EXEC DBO.[sp_Insert_Communication_Export] @VesselCode,'Job-Correction',@CorrectionId,'@VesselCode-@HistoryId','AUTO'" + Environment.NewLine +
                "    COMMIT" + Environment.NewLine +
                "END TRY " + Environment.NewLine +
                "BEGIN CATCH " + Environment.NewLine +
                "    ROLLBACK " + Environment.NewLine +
                "END CATCH " + Environment.NewLine +
                "GO ";
            text = text.Replace("@VesselCode", vesselcode );
            text = text.Replace("@HistoryId", historyid.ToString());
            text = text.Replace("@CorrectionId", CorrectionId.ToString());
            //--------------------------------
            if(lblLastDoneDate.Text.Trim()=="")
                text = text.Replace("@LastDoneDate", "NULL");
            else
                text = text.Replace("@LastDoneDate", "'" + lblLastDoneDate.Text.Trim() + "'");
            //--------------------------------
            if (lblLastDoneHour.Text.Trim() == "")
                text = text.Replace("@LastDoneHr", "0");
            else
                text = text.Replace("@LastDoneHr",lblLastDoneHour.Text.Trim());
            //--------------------------------
            if (lblNextDueDate.Text.Trim() == "")
                text = text.Replace("@NextDueDate", "NULL");
            else
                text = text.Replace("@NextDueDate", "'" + lblNextDueDate.Text.Trim() + "'");
            //--------------------------------
            if (lblNextDueHour.Text.Trim() == "")
                text = text.Replace("@NextDueHr", "0");
            else
                text = text.Replace("@NextDueHr", lblNextDueHour.Text.Trim());
            //--------------------------------
            text = text.Replace("@UserName", "Auto");
            text = text.Replace("@Remarks", txtRemarks.Text.Trim());
            File.WriteAllText(ScriptFile, text);
            try
            {
                using (ZipFile zip = new ZipFile())
                {
                    zip.AddFile(ConfigFile);
                    zip.AddFile(ScriptFile);
                    zip.Save(ZipData);
                    FileDone = true;
                }
                //--------------------------------
                if (FileDone)
                {
                    //email="emanager@energiossolutions.com";
                    string FromAdd = ConfigurationManager.AppSettings["FromAddress"];
                    string CCMail = "asingh@energiossolutions.com";
                    string[] ToAdd ={ email };
                    string[] CCAdd ={ CCMail };
                    string[] NOAdd ={ };
                    
                    StringBuilder sb = new StringBuilder();
                    sb.Append("Dear Captain,<br /><br />");
                    sb.Append("Please be advised that following job history is rejected by your Tech Super. Please plan the job once again and execute it.<br /><br />");
                    sb.Append("<b>Component Code : " + compcode + ".<br /><br />");                    
                    sb.Append("Job Code : " + jobcode + ".<br /><br />");
                    sb.Append("Job : " + job + ".<br /><br />");
                    sb.Append("Remarks : " + remarks + ".<br /><br /></b>");
                    sb.Append("Please import the attached file and send the acknowledgement packet from export screen.<br /><br />");
                    sb.Append("Please note these PMS update files must not be ignored or skipped..<br /><br />");
                    sb.Append("Thank You.<br />********************");
                    sb.Append("<br /><br /><br />");
                    String error="";
                    bool result = EProjectCommon.SendeMail_MTM(0,FromAdd, FromAdd, ToAdd, CCAdd, NOAdd, "Job Correction Update : " + vesselcode , sb.ToString(), out error, ZipData);
                    if(result)
                        MessageBox1.ShowMessage("Update sent successfully.", false);
                    else
                        MessageBox1.ShowMessage("Unable to send update." +error , true);
                }
                else
                {
                    lblMsgCorrectionRemarks.Text = "Unable to create Zip File.";
                }
            }
            catch (Exception ex)
            {
                lblMsgCorrectionRemarks.Text = "Unable to export data. " + ex.Message;
            }
        }
    }
    
}