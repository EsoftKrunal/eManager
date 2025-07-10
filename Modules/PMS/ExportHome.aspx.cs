using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System.Xml;
using Ionic.Zip;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Collections.Generic;


public partial class ExportHome : System.Web.UI.Page
{
    public string ExportMode
    {
        set { ViewState["ExportMode"] = value; }
        get { return ViewState["ExportMode"].ToString(); }
    }
    public string CurrentVessel
    {
        get { return ViewState["CurrentVessel"].ToString(); }
        set { ViewState["CurrentVessel"] = value; }
    }
    public int Id
    {
        get { return Common.CastAsInt32(ViewState["Id"]); }
        set { ViewState["Id"] = value; }
    }
    public int DoneId
    {
        get { return Common.CastAsInt32(ViewState["DoneId"]); }
        set { ViewState["DoneId"] = value; }
    }
    public string DoneId_String
    {
        get { return Convert.ToString(ViewState["DoneId_String"]); }
        set { ViewState["DoneId_String"] = value; }
    }

    string sqlConnectionString = @"Data Source=LOCALHOST\SQLEXPRESS;Initial Catalog=Master;Integrated Security=SSPI;Persist Security Info=False";
    //string sqlConnectionString = @"Data Source=192.168.1.4\SQLEXPRESS;Initial Catalog=Master;User Id=sa;Password=mtm-123;Persist Security Info=True";
    //string sqlConnectionString = @"Data Source=192.168.1.8\SQLDEV;Initial Catalog=Master;User Id=sa;Password=mtm-12345;Persist Security Info=True";
    //string sqlConnectionString = @"Data Source=LOCALHOST;Initial Catalog=Master;Persist Security Info=False;Integrated Security=SSPI";

    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        lblMsgRH.Text = "";
        if (!IsPostBack)
        {
            CurrentVessel = Session["CurrentShip"].ToString();

            //ddlMonth.Items.Add(new ListItem(" Month ",""));
            //for(int i=1;i<=12;i++)
            //{
            //    ddlMonth.Items.Add(new ListItem(ProjectCommon.GetMonthName(i.ToString()),i.ToString()));
            //}
            //ddlYear.Items.Add(new ListItem(" Year ", ""));
            //for(int y=DateTime.Today.Year; y>=2015;y--)
            //{
            //    ddlYear.Items.Add(new ListItem(y.ToString(),y.ToString()));
            //}
            ClearTempFiles();

            string strConectionString = sqlConnectionString.Replace("Master", "eMANAGER");
            SqlConnection SqlCon = new SqlConnection(strConectionString);
            SqlCommand cmd = new SqlCommand("SELECT * FROM MailSettings", SqlCon);
            SqlDataAdapter AdpExport = new SqlDataAdapter(cmd);
            DataSet ds_Ship = new DataSet();
            AdpExport.Fill(ds_Ship);
            ExportMode="M";

            if(ds_Ship!=null)
                if(ds_Ship.Tables!=null)
                    if(ds_Ship.Tables.Count>0)
                        if(ds_Ship.Tables[0].Rows.Count>0)
                            ExportMode = ds_Ship.Tables[0].Rows[0]["PacketMode"].ToString();

            if (ExportMode == "A") // auto mail sent
            {
                btnExportPMS.ImageUrl = "~/Modules/PMS/Images/email.png";
                btnExportSMS.ImageUrl = "~/Modules/PMS/Images/email.png";
                btnExportMNP.ImageUrl = "~/Modules/PMS/Images/email.png";
                btnExportRH.ImageUrl = "~/Modules/PMS/Images/email.png";
            }
            else // manual download
            {
                btnExportPMS.ImageUrl = "~/Modules/PMS/Images/icon_zip.gif";
                btnExportSMS.ImageUrl = "~/Modules/PMS/Images/icon_zip.gif";
                btnExportMNP.ImageUrl = "~/Modules/PMS/Images/icon_zip.gif";
                btnExportRH.ImageUrl = "~/Modules/PMS/Images/icon_zip.gif";
            }

            //------------------------------------------------------------------------------

            DataSet ds_Routine = new DataSet();

            string sql = "select top 1 PACKETNAME,PACKETDATE from [dbo].[IE_PacketCreated] ORDER BY ROWID DESC";
            cmd.CommandText = sql;
            ds_Routine.Clear();
            AdpExport.Fill(ds_Routine);
            if (ds_Routine.Tables.Count == 1)
                if (ds_Routine.Tables[0].Rows.Count > 0)
                    lblPMSExportedOn.Text = Common.ToDateString(ds_Routine.Tables[0].Rows[0]["PACKETDATE"]);
            
            sql = "select top 1 PACKETNAME,PACKETDATE from [dbo].[IE_PacketCreated_MNP] ORDER BY ROWID DESC";
            cmd.CommandText = sql;
            ds_Routine.Clear();
            AdpExport.Fill(ds_Routine);
            if (ds_Routine.Tables.Count == 1)
                if (ds_Routine.Tables[0].Rows.Count > 0)
                    lblMNPExportedOn.Text = Common.ToDateString(ds_Routine.Tables[0].Rows[0]["PACKETDATE"]);

            sql = "select top 1 PACKETNAME,CreatedOn from [dbo].[VSL_SMS_PacketSent] ORDER BY TableID DESC ";
            cmd.CommandText = sql;
            ds_Routine.Clear();
            AdpExport.Fill(ds_Routine);
            if (ds_Routine.Tables.Count == 1)
                if (ds_Routine.Tables[0].Rows.Count > 0)
                    lblSMSExportedOn.Text = Common.ToDateString(ds_Routine.Tables[0].Rows[0]["PACKETDATE"]);

            BindOtherPackets();
            BindYear();
        }
    }
    protected void BindYear()
    {
        int i = DateTime.Today.Year-1;
        ddlYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
        i = DateTime.Today.Year;
        ddlYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
        ddlYear.Items.Insert(0, new ListItem("< Select >", ""));
    }
    protected void ClearTempFiles()
    {
        string[] files=Directory.GetFiles(Server.MapPath("~/TEMP"));
        foreach (string fl in files)
            try{File.Delete(fl);}catch { }

        string[] folders = Directory.GetDirectories(Server.MapPath("~/TEMP"));
        foreach (string folder in folders)
            try { Directory.Delete(folder, true); }
            catch { }

    }
    //------- Menu Section --------------

    //------- Backup Section --------------

    //------- Maintenance Section --------------

    //------- Export Section --------------

    public void BindOtherPackets()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM [dbo].[vw_Communication_Export] WHERE VESSELCODE='" + CurrentVessel + "' ORDER BY RecordType, CreatedOn DESC");

        rptOtherPackets.DataSource = dt;
        rptOtherPackets.DataBind();

    }
    protected void btnExport_Click(object sender, EventArgs e)
    { 
        DoneId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        DoneId_String = ((ImageButton)sender).CommandArgument.ToString();

        int TableId = Common.CastAsInt32(((ImageButton)sender).Attributes["TableId"]);
        string Type = ((ImageButton)sender).CssClass;
        
        if (Type == "Drill")
        {
            Export_From_DrillTrainings();
        }
        if (Type == "Provision")
        {
            Export_From_Provision();
        }
        if (Type == "Spare")
        {
            Export_From_Spare();
        }
        if (Type == "Store")
        {
            Export_From_Store();
        }
        if (Type == "StoreNew")
        {
            Export_From_StoreNew();
        }
        if (Type == "LFI")
        {
            Export_From_LFI();
        }
        if (Type == "JOB-COMMENTS")
        {
            Export_JOB_COMMENTS();
        }
        if (Type == "BREAKDOWN")
        {
            Export_BREAKDOWN();
        }
        if (Type == "BREAKDOWN-ATTACHMENTS")
        {
            Export_BREAKDOWN_ATTACHMENTS();
        }                
        if (Type == "Focus Campaign")
        {
            Export_From_FC();
        }
        if (Type == "Regulation")
        {
            Export_From_Regulation();
        }
        if (Type == "Circular")
        {
            Export_From_Circular();
        }
        if (Type == "SCM")
        {
            Export_From_SCM();
        }
        if (Type == "MWUC")
        {
            Export_From_MWUC();
        }
        if (Type == "eForm - G118")
        {
            Export_From_G118();
        }
        if (Type == "eForm - S115")
        {
            Export_From_S115();
        }
        if (Type == "eForm - S133")
        {
            Export_From_S133();
        }
        if (Type == "eForm - G113")
        {
            Export_From_G113();
        }
        if (Type == "MRV-VOYAGE")
        {
            Export_From_MRV_VOYAGE();
        }
        if (Type == "MRV-VOYAGE1")
        {
            Export_From_MRV_VOYAGE1();
        }
        if (Type == "POS-ORDER")
        {
            Export_From_POS_ORDER();
        }
        if (Type == "RESTHOUR-MONTH-PACKET")
        {
            Export_RESTHOUR_MONTH_PACKET();
        }
        if (Type == "RESTHOUR-CREW-MONTH-PACKET")
        {
            Export_RESTHOUR_CREW_MONTH_PACKET();
        }
        if (Type == "CREW TRAINING UPDATE")
        {
            Export_CREW_TRAINING_UPDATE();
        }        
        if (Type == "Inventory")
        {
            Export_From_Inventory();
        }
        if (Type == "Supply")
        {
            Export_From_Supply();
        }
        if (Type == "MealReport")
        {
            Export_From_MealReport();
        }
        if (Type == "MealReportAcc")
        {
            Export_From_MealReport_Acc();
        }
        if (Type == "Vessel Navigation Audit")
        {
            Export_From_VNA();
        }
        if (Type == "JOBHISTORY-ATTACHMENTS")
        {
            Export_JOBHISTORY_ATTACHMENTS();
        }
        if (Type == "DEFECT-ATTACHMENTS")
        {
            Export_DEFECT_ATTACHMENTS();
        }
        if (Type == "CRITICALEQ-SHUTDOWN-REQ")
        {
            Export_CRITICALEQ_SHUTDOWN_REQ();
        }
        if (Type == "RISKTEMPLATES-ACK")
        {
            Export_RISKTEMPLATES_ACK();
        }
        if (Type == "ER_S115_SHIPACK")
        {
            Export_ER_S115_SHIPACK();
        }
        if (Type == "Job-Correction")
        {
            Export_Job_Rejection();
        }
        if (Type == "Arrival" || Type == "Departure" || Type == "Noon" || Type == "Port-Anchorage" || Type == "Port-Berthing" || Type == "Port-Drift")
        {
            string TableName = "";
            switch (Type.Trim())
            {
                case "Arrival":
                    Type = "A";
                    TableName = "VSL_VPRArrivalReport";
                    break;
                case "Departure":
                    Type = "D";
                    TableName = "VSL_VPRDepartureReport";
                    break;
                case "Noon":
                    Type = "N";
                    TableName = "VSL_VPRNoonReport";
                    break;
                case "Port-Anchorage":
                    Type = "PA";
                    TableName = "VSL_VPRPortAnchorageReport";
                    break;
                case "Port-Berthing":
                    Type = "PB";
                    TableName = "VSL_VPRPortBerthingReport";
                    break;
                case "Port-Drift":
                    Type = "PD";
                    TableName = "VSL_VPRPortDriftReport";
                    break;
                default:
                    Type = "";
                    TableName = "";
                    break;

            }

            Export_From_PositionReport(Type, TableName);
        }

        if (Type.StartsWith("Position Report - "))
        {
            Export_From_PositionReportNew();
        }

        ////if (Type.StartsWith("Position Report"))
        ////{
        ////    string TableName = "";
        ////    //switch (Type.Trim())
        ////    //{
        ////    //    case "Arrival":
        ////    //        Type = "A";
        ////    //        TableName = "VSL_VPRArrivalReport";
        ////    //        break;
        ////    //    case "Departure":
        ////    //        Type = "D";
        ////    //        TableName = "VSL_VPRDepartureReport";
        ////    //        break;
        ////    //    case "Noon":
        ////    //        Type = "N";
        ////    //        TableName = "VSL_VPRNoonReport";
        ////    //        break;
        ////    //    case "Port-Anchorage":
        ////    //        Type = "PA";
        ////    //        TableName = "VSL_VPRPortAnchorageReport";
        ////    //        break;
        ////    //    case "Port-Berthing":
        ////    //        Type = "PB";
        ////    //        TableName = "VSL_VPRPortBerthingReport";
        ////    //        break;
        ////    //    case "Port-Drift":
        ////    //        Type = "PD";
        ////    //        TableName = "VSL_VPRPortDriftReport";
        ////    //        break;
        ////    //    default:
        ////    //        Type = "";
        ////    //        TableName = "";
        ////    //        break;

        ////    //}
        ////    Export_From_PositionReport(Type, TableName);
        ////}

        if (Type == "Risk Management")
        {
            Export_From_RCA();
        }

        Common.Execute_Procedures_Select_ByQuery("UPDATE tbl_Vessel_Communication SET [ExportedOn] = getdate(), [ExportedBy] = '" + Session["FullName"].ToString() + "' WHERE [VesselCode]='" + CurrentVessel + "' AND [TableId] = " + TableId);
        //-----------
        BindOtherPackets();
        
    }
    public void Export_From_DrillTrainings()
    {
        ClearTempFiles();
        string SaveTargetDir = Server.MapPath("~/TEMP/");

        string strConectionString = sqlConnectionString.Replace("Master", "eMANAGER");
        SqlConnection SqlCon = new SqlConnection(strConectionString);
        SqlCommand cmd = new SqlCommand("", SqlCon);
        SqlDataAdapter AdpExport = new SqlDataAdapter(cmd);
        DataSet ds_Ship = new DataSet();


        ///  ------------------- CHECKS 3.  GET DATA TO BE EXPORTED FROM DATABASE

        cmd.CommandText = ("select * from [dbo].[DT_VSL_DrillTrainings] WHERE VesselCode='" + CurrentVessel + "' AND DoneId=" + DoneId);
        DataSet ds = new DataSet();
        AdpExport.Fill(ds);
        ds.Tables[0].TableName = "DT_VSL_DrillTrainings";

        cmd.CommandText = ("select * from [dbo].[DT_VSL_DrillTrainingsHistory] WHERE VesselCode='" + CurrentVessel + "' AND DoneId=" + DoneId);
        DataSet ds1 = new DataSet();
        AdpExport.Fill(ds1);
        ds1.Tables[0].TableName = "DT_VSL_DrillTrainingsHistory";


        cmd.CommandText = ("select * from [dbo].[DT_VSL_DrillTrainingHistoryRanks] WHERE VesselCode='" + CurrentVessel + "' AND DoneId=" + DoneId);
        DataSet ds2 = new DataSet();
        AdpExport.Fill(ds2);
        ds2.Tables[0].TableName = "DT_VSL_DrillTrainingHistoryRanks";

        cmd.CommandText = ("select * from [dbo].[DT_VSL_DrillTrainingHistoryDetails] WHERE VesselCode='" + CurrentVessel + "' AND DoneId=" + DoneId);
        DataSet ds3 = new DataSet();
        AdpExport.Fill(ds3);
        ds3.Tables[0].TableName = "DT_VSL_DrillTrainingHistoryDetails";

        cmd.CommandText = ("select * from [dbo].[DT_VSL_DrillTrainingHistoryAttachments] WHERE VesselCode='" + CurrentVessel + "' AND DoneId=" + DoneId);
        DataSet ds4 = new DataSet();
        AdpExport.Fill(ds4);
        ds4.Tables[0].TableName = "DT_VSL_DrillTrainingHistoryAttachments";

        DataSet dsExport = new DataSet();
        dsExport.Tables.Add(ds.Tables[0].Copy());
        dsExport.Tables.Add(ds1.Tables[0].Copy());

        dsExport.Tables.Add(ds2.Tables[0].Copy());
        dsExport.Tables.Add(ds3.Tables[0].Copy());
        dsExport.Tables.Add(ds4.Tables[0].Copy());

        ///  ------------------- CHECKS 4.  IF SQL ERROR NO TABLE RETURNED 
        if (dsExport == null)
        {
            lblMsg.Text = "Database not exists OR some critical error.";
            return;
        }
        ///  ------------------- CHECKS 5.  IF SQL ERROR NO TABLE RETURNED 
        if (dsExport.Tables.Count <= 0)
        {
            lblMsg.Text = "Database not exists OR some critical error.";
            return;
        }

        ///  ------------------- CHECKS 7.  IF TABLES MATCHING BUT THERE IS NOTHING TO EXPORT
        bool DataExists = false;
        foreach (DataTable dt in dsExport.Tables)
        {
            if (dt.Rows.Count > 0)
            {
                DataExists = true;
                break;
            }
        }
        if (!DataExists)
        {
            lblMsg.Text = "No data available to send update.";
            return;
        }
        //===================================================================================
        //---------------- CHECKS DONE NOW GOING TO CREATE FILE 

        string SchemaFile = SaveTargetDir + "SCHEMA_Matrix.xml";
        string DataFile = SaveTargetDir + "DATA_Matrix.xml";

        try
        {
            string ZipData = SaveTargetDir + "DTM_" + CurrentVessel + "_S_" + DoneId.ToString().PadLeft(5, '0') + ".zip";
            dsExport.WriteXmlSchema(SchemaFile);
            dsExport.WriteXml(DataFile);
            bool FileDone = false;
            using (ZipFile zip = new ZipFile())
            {
                zip.AddFile(SchemaFile);
                zip.AddFile(DataFile);
                zip.Save(ZipData);
                FileDone = true;
            }
            //--------------------------------
            if (FileDone)
            {
                if (ExportMode == "M")
                {
                    ExportFileToResponse(ZipData);
                    lblMsg.Text = "Data exported successfully. File Name : " + Path.GetFileName(ZipData);
                }
                else
                {
                    if (SendPacketMail(ZipData, "PMS - Drill Packet"))
                    {
                        lblMsg.Text = "Mail sent successfully.";
                    }
                }
            }
            else
            {
                lblMsg.Text = "Unable to create Zip File.";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Unable to export data. " + ex.Message;
        }
    }
    public void Export_From_Provision()
    {
        ClearTempFiles();
        string SaveTargetDir = Server.MapPath("~/TEMP/");

        string strConectionString = sqlConnectionString.Replace("Master", "eMANAGER");
        SqlConnection SqlCon = new SqlConnection(strConectionString);
        SqlCommand cmd = new SqlCommand("", SqlCon);
        SqlDataAdapter AdpExport = new SqlDataAdapter(cmd);

        ///  ------------------- CHECKS 3.  GET DATA TO BE EXPORTED FROM DATABASE

        
        cmd.CommandText = ("SELECT * FROM [dbo].[MP_VSL_ProvisionMaster] WHERE VESSELCODE='" + CurrentVessel + "' AND ProvisionId=" + DoneId.ToString());
        DataSet ds = new DataSet();
        AdpExport.Fill(ds);
        ds.Tables[0].TableName = "MP_VSL_ProvisionMaster";

        string ReqNo = ds.Tables[0].Rows[0]["RequisitionNo"].ToString();

        cmd.CommandText = ("SELECT * FROM [dbo].[MP_VSL_ProvisionDetails] WHERE VESSELCODE='" + CurrentVessel + "' AND ProvisionId=" + DoneId.ToString());
        DataSet ds1 = new DataSet();
        AdpExport.Fill(ds1);
        ds1.Tables[0].TableName = "MP_VSL_ProvisionDetails";


        cmd.CommandText = ("SELECT * FROM [dbo].[MP_VSL_ProvisionNationalityWise] WHERE VESSELCODE='" + CurrentVessel + "' AND ProvisionId=" + DoneId.ToString());
        DataSet ds2 = new DataSet();
        AdpExport.Fill(ds2);
        ds2.Tables[0].TableName = "MP_VSL_ProvisionNationalityWise";

        cmd.CommandText = ("SELECT * FROM [dbo].[MP_VSL_ProvisionOtherItems] WHERE VESSELCODE='" + CurrentVessel + "' AND ProvisionId=" + DoneId.ToString());
        DataSet ds3 = new DataSet();
        AdpExport.Fill(ds3);
        ds3.Tables[0].TableName = "MP_VSL_ProvisionOtherItems";
        

        DataSet dsExport = new DataSet();
        dsExport.Tables.Add(ds.Tables[0].Copy());
        dsExport.Tables.Add(ds1.Tables[0].Copy());

        dsExport.Tables.Add(ds2.Tables[0].Copy());
        dsExport.Tables.Add(ds3.Tables[0].Copy());         

        ///  ------------------- CHECKS 4.  IF SQL ERROR NO TABLE RETURNED 
        if (dsExport == null)
        {
            lblMsg.Text = "Database not exists OR some critical error.";
            return;
        }
        ///  ------------------- CHECKS 5.  IF SQL ERROR NO TABLE RETURNED 
        if (dsExport.Tables.Count <= 0)
        {
            lblMsg.Text = "Database not exists OR some critical error.";
            return;
        }

        ///  ------------------- CHECKS 7.  IF TABLES MATCHING BUT THERE IS NOTHING TO EXPORT
        bool DataExists = false;
        foreach (DataTable dt in dsExport.Tables)
        {
            if (dt.Rows.Count > 0)
            {
                DataExists = true;
                break;
            }
        }
        if (!DataExists)
        {
            lblMsg.Text = "No data available to send update.";
            return;
        }
        //===================================================================================
        //---------------- CHECKS DONE NOW GOING TO CREATE FILE 

        string SchemaFile = SaveTargetDir + "PR_SCHEMA.XML";
        string DataFile = SaveTargetDir + "PR_DATA.XML";

        try
        {
            string ZipData = SaveTargetDir + "PRM_S_P_" + ReqNo.Replace("/", "-") + ".zip";
            dsExport.WriteXmlSchema(SchemaFile);
            dsExport.WriteXml(DataFile);
            bool FileDone = false;
            using (ZipFile zip = new ZipFile())
            {
                zip.AddFile(SchemaFile);
                zip.AddFile(DataFile);
                zip.Save(ZipData);
                FileDone = true;
            }
            //--------------------------------
            if (FileDone)
            {
                if (ExportMode == "M")
                {
                    ExportFileToResponse(ZipData);
                    lblMsg.Text = "Data exported successfully. File Name : " + Path.GetFileName(ZipData);
                }
                else
                {
                    if (SendPacketMail(ZipData, "PMS - Provision Packet"))
                    {
                        lblMsg.Text = "Mail sent successfully.";
                    }
                }
            }
            else
            {
                lblMsg.Text = "Unable to create Zip File.";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Unable to export data. " + ex.Message;
        }
    }
    public void Export_From_Spare()
    {
        ClearTempFiles();
        string SaveTargetDir = Server.MapPath("~/TEMP/");

        string strConectionString = sqlConnectionString.Replace("Master", "eMANAGER");
        SqlConnection SqlCon = new SqlConnection(strConectionString);
        SqlCommand cmd = new SqlCommand("", SqlCon);
        SqlDataAdapter AdpExport = new SqlDataAdapter(cmd);

        ///  ------------------- CHECKS 3.  GET DATA TO BE EXPORTED FROM DATABASE


        cmd.CommandText = ("SELECT * FROM [dbo].[MP_VSL_SparesReqMaster] WHERE VESSELCODE='" + CurrentVessel + "' AND SpareReqId=" + DoneId.ToString());
        DataSet ds = new DataSet();
        AdpExport.Fill(ds);
        ds.Tables[0].TableName = "MP_VSL_SparesReqMaster";

        string ReqNo = ds.Tables[0].Rows[0]["ReqnNo"].ToString();

        cmd.CommandText = ("SELECT * FROM [dbo].[MP_VSL_SparesReqDetails] WHERE VESSELCODE='" + CurrentVessel + "' AND SpareReqId=" + DoneId.ToString());
        DataSet ds1 = new DataSet();
        AdpExport.Fill(ds1);
        ds1.Tables[0].TableName = "MP_VSL_SparesReqDetails";

        DataSet dsExport = new DataSet();
        dsExport.Tables.Add(ds.Tables[0].Copy());
        dsExport.Tables.Add(ds1.Tables[0].Copy());


        ///  ------------------- CHECKS 4.  IF SQL ERROR NO TABLE RETURNED 
        if (dsExport == null)
        {
            lblMsg.Text = "Database not exists OR some critical error.";
            return;
        }
        ///  ------------------- CHECKS 5.  IF SQL ERROR NO TABLE RETURNED 
        if (dsExport.Tables.Count <= 0)
        {
            lblMsg.Text = "Database not exists OR some critical error.";
            return;
        }

        ///  ------------------- CHECKS 7.  IF TABLES MATCHING BUT THERE IS NOTHING TO EXPORT
        bool DataExists = false;
        foreach (DataTable dt in dsExport.Tables)
        {
            if (dt.Rows.Count > 0)
            {
                DataExists = true;
                break;
            }
        }
        if (!DataExists)
        {
            lblMsg.Text = "No data available to send update.";
            return;
        }
        //===================================================================================
        //---------------- CHECKS DONE NOW GOING TO CREATE FILE 

        string SchemaFile = SaveTargetDir + "PR_SCHEMA.XML";
        string DataFile = SaveTargetDir + "PR_DATA.XML";

        try
        {
            string ZipData = SaveTargetDir + "PRM_S_S_" + ReqNo.Replace("/", "-") + ".zip";
            dsExport.WriteXmlSchema(SchemaFile);
            dsExport.WriteXml(DataFile);
            bool FileDone = false;
            using (ZipFile zip = new ZipFile())
            {
                zip.AddFile(SchemaFile);
                zip.AddFile(DataFile);
                zip.Save(ZipData);
                FileDone = true;
            }
            //--------------------------------
            if (FileDone)
            {
                if (ExportMode == "M")
                {
                    ExportFileToResponse(ZipData);
                    lblMsg.Text = "Data exported successfully. File Name : " + Path.GetFileName(ZipData);
                }
                else
                {
                    if (SendPacketMail(ZipData, "PMS - Spare Packet"))
                    {
                        lblMsg.Text = "Mail sent successfully.";
                    }
                }
            }
            else
            {
                lblMsg.Text = "Unable to create Zip File.";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Unable to export data. " + ex.Message;
        }
    }
    public void Export_From_Store()
    {
        ClearTempFiles();
        string SaveTargetDir = Server.MapPath("~/TEMP/");

        string strConectionString = sqlConnectionString.Replace("Master", "eMANAGER");
        SqlConnection SqlCon = new SqlConnection(strConectionString);
        SqlCommand cmd = new SqlCommand("", SqlCon);
        SqlDataAdapter AdpExport = new SqlDataAdapter(cmd);

        ///  ------------------- CHECKS 3.  GET DATA TO BE EXPORTED FROM DATABASE


        cmd.CommandText = ("SELECT * FROM [dbo].[MP_VSL_StoreReqMaster] WHERE VESSELCODE='" + CurrentVessel + "' AND StoreReqId=" + DoneId.ToString());
        DataSet ds = new DataSet();
        AdpExport.Fill(ds);
        ds.Tables[0].TableName = "MP_VSL_StoreReqMaster";

        string ReqNo = ds.Tables[0].Rows[0]["ReqnNo"].ToString();

        cmd.CommandText = ("SELECT * FROM [dbo].[MP_VSL_StoreReqDetails] WHERE VESSELCODE='" + CurrentVessel + "' AND StoreReqId=" + DoneId.ToString());
        DataSet ds1 = new DataSet();
        AdpExport.Fill(ds1);
        ds1.Tables[0].TableName = "MP_VSL_StoreReqDetails";


        cmd.CommandText = ("SELECT * FROM [dbo].[MP_VSL_StoreItemMaster] WHERE VESSELCODE='" + CurrentVessel + "' AND ITEMID in (SELECT DISTINCT ITEMID FROM [dbo].[MP_VSL_StoreReqDetails] WHERE VESSELCODE='" + CurrentVessel + "' AND StoreReqId=" + DoneId.ToString() + ")");
        DataSet ds2 = new DataSet();
        AdpExport.Fill(ds2);
        ds2.Tables[0].TableName = "MP_VSL_StoreItemMaster";

        DataSet dsExport = new DataSet();
        dsExport.Tables.Add(ds.Tables[0].Copy());
        dsExport.Tables.Add(ds1.Tables[0].Copy());
        dsExport.Tables.Add(ds2.Tables[0].Copy());
        

        ///  ------------------- CHECKS 4.  IF SQL ERROR NO TABLE RETURNED 
        if (dsExport == null)
        {
            lblMsg.Text = "Database not exists OR some critical error.";
            return;
        }
        ///  ------------------- CHECKS 5.  IF SQL ERROR NO TABLE RETURNED 
        if (dsExport.Tables.Count <= 0)
        {
            lblMsg.Text = "Database not exists OR some critical error.";
            return;
        }

        ///  ------------------- CHECKS 7.  IF TABLES MATCHING BUT THERE IS NOTHING TO EXPORT
        bool DataExists = false;
        foreach (DataTable dt in dsExport.Tables)
        {
            if (dt.Rows.Count > 0)
            {
                DataExists = true;
                break;
            }
        }
        if (!DataExists)
        {
            lblMsg.Text = "No data available to send update.";
            return;
        }
        //===================================================================================
        //---------------- CHECKS DONE NOW GOING TO CREATE FILE 

        string SchemaFile = SaveTargetDir + "PR_SCHEMA.XML";
        string DataFile = SaveTargetDir + "PR_DATA.XML";

        try
        {
            string ZipData = SaveTargetDir + "PRM_S_T_" + ReqNo.Replace("/", "-") + ".zip";
            dsExport.WriteXmlSchema(SchemaFile);
            dsExport.WriteXml(DataFile);
            bool FileDone = false;
            using (ZipFile zip = new ZipFile())
            {
                zip.AddFile(SchemaFile);
                zip.AddFile(DataFile);
                zip.Save(ZipData);
                FileDone = true;
            }
            //--------------------------------
            if (FileDone)
            {
                if (ExportMode == "M")
                {
                    ExportFileToResponse(ZipData);
                    lblMsg.Text = "Data exported successfully. File Name : " + Path.GetFileName(ZipData);
                }
                else
                {
                    if (SendPacketMail(ZipData, "PMS - Store Packet"))
                    {
                        lblMsg.Text = "Mail sent successfully.";
                    }
                }
            }
            else
            {
                lblMsg.Text = "Unable to create Zip File.";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Unable to export data. " + ex.Message;
        }
    }
    public void Export_From_StoreNew()
    {
        ClearTempFiles();
        string SaveTargetDir = Server.MapPath("~/TEMP/");

        string strConectionString = sqlConnectionString.Replace("Master", "eMANAGER");
        SqlConnection SqlCon = new SqlConnection(strConectionString);
        SqlCommand cmd = new SqlCommand("", SqlCon);
        SqlDataAdapter AdpExport = new SqlDataAdapter(cmd);

        ///  ------------------- CHECKS 3.  GET DATA TO BE EXPORTED FROM DATABASE


        cmd.CommandText = ("SELECT * FROM [dbo].[MP_VSL_StoreReqMaster1] WHERE VESSELCODE='" + CurrentVessel + "' AND StoreReqId=" + DoneId.ToString());
        DataSet ds = new DataSet();
        AdpExport.Fill(ds);
        ds.Tables[0].TableName = "MP_VSL_StoreReqMaster1";

        string ReqNo = ds.Tables[0].Rows[0]["ReqnNo"].ToString();

        cmd.CommandText = ("SELECT * FROM [dbo].[MP_VSL_StoreReqDetails1] WHERE VESSELCODE='" + CurrentVessel + "' AND StoreReqId=" + DoneId.ToString());
        DataSet ds1 = new DataSet();
        AdpExport.Fill(ds1);
        ds1.Tables[0].TableName = "MP_VSL_StoreReqDetails1";

        cmd.CommandText = ("SELECT * FROM [dbo].[MP_VSL_StoreReqDetails1_Others] WHERE VESSELCODE='" + CurrentVessel + "' AND StoreReqId=" + DoneId.ToString());
        DataSet ds2 = new DataSet();
        AdpExport.Fill(ds2);
        ds2.Tables[0].TableName = "MP_VSL_StoreReqDetails1_Others";

        DataSet dsExport = new DataSet();
        dsExport.Tables.Add(ds.Tables[0].Copy());
        dsExport.Tables.Add(ds1.Tables[0].Copy());
        dsExport.Tables.Add(ds2.Tables[0].Copy());

        ///  ------------------- CHECKS 4.  IF SQL ERROR NO TABLE RETURNED 
        if (dsExport == null)
        {
            lblMsg.Text = "Database not exists OR some critical error.";
            return;
        }
        ///  ------------------- CHECKS 5.  IF SQL ERROR NO TABLE RETURNED 
        if (dsExport.Tables.Count <= 0)
        {
            lblMsg.Text = "Database not exists OR some critical error.";
            return;
        }

        ///  ------------------- CHECKS 7.  IF TABLES MATCHING BUT THERE IS NOTHING TO EXPORT
        bool DataExists = false;
        foreach (DataTable dt in dsExport.Tables)
        {
            if (dt.Rows.Count > 0)
            {
                DataExists = true;
                break;
            }
        }
        if (!DataExists)
        {
            lblMsg.Text = "No data available to send update.";
            return;
        }
        //===================================================================================
        //---------------- CHECKS DONE NOW GOING TO CREATE FILE 

        string SchemaFile = SaveTargetDir + "STORE_REQ_NEW_SCHEMA.XML";
        string DataFile = SaveTargetDir + "STORE_REQ_NEW_DATA.XML";

        try
        {
            string ZipData = SaveTargetDir + "PRM_NEW_S_T_" + ReqNo.Replace("/", "-") + ".zip";
            dsExport.WriteXmlSchema(SchemaFile);
            dsExport.WriteXml(DataFile);
            bool FileDone = false;
            using (ZipFile zip = new ZipFile())
            {
                zip.AddFile(SchemaFile);
                zip.AddFile(DataFile);
                zip.Save(ZipData);
                FileDone = true;
            }
            //--------------------------------
            if (FileDone)
            {
                if (ExportMode == "M")
                {
                    ExportFileToResponse(ZipData);
                    lblMsg.Text = "Data exported successfully. File Name : " + Path.GetFileName(ZipData);
                }
                else
                {
                    if (SendPacketMail(ZipData, "PMS - New Store Reqn Packet"))
                    {
                        lblMsg.Text = "Mail sent successfully.";
                    }
                }
            }
            else
            {
                lblMsg.Text = "Unable to create Zip File.";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Unable to export data. " + ex.Message;
        }
    }
    
    public void Export_From_LFI()
    {
        ClearTempFiles();
        string SaveTargetDir = Server.MapPath("~/TEMP/");

        string strConectionString = sqlConnectionString.Replace("Master", "eMANAGER");
        SqlConnection SqlCon = new SqlConnection(strConectionString);
        SqlCommand cmd = new SqlCommand("", SqlCon);
        SqlDataAdapter AdpExport = new SqlDataAdapter(cmd);

        ///  ------------------- CHECKS 3.  GET DATA TO BE EXPORTED FROM DATABASE


        cmd.CommandText = ("SELECT * FROM DBO.LFI_Vessel_Notifications WHERE LFIId = " + DoneId + " AND VesselCode='" + CurrentVessel + "'");
        DataSet ds = new DataSet();
        AdpExport.Fill(ds);
        ds.Tables[0].TableName = "LFI_Vessel_Notifications";

        DataTable dtNum = Common.Execute_Procedures_Select_ByQuery("SELECT [LFINumber] FROM [dbo].[LFI] WHERE LFIId = " + DoneId);

        string LFINumber = dtNum.Rows[0]["LFINumber"].ToString();

        DataSet dsExport = new DataSet();
        dsExport.Tables.Add(ds.Tables[0].Copy());

        ///  ------------------- CHECKS 4.  IF SQL ERROR NO TABLE RETURNED 
        if (dsExport == null)
        {
            lblMsg.Text = "Database not exists OR some critical error.";
            return;
        }
        ///  ------------------- CHECKS 5.  IF SQL ERROR NO TABLE RETURNED 
        if (dsExport.Tables.Count <= 0)
        {
            lblMsg.Text = "Database not exists OR some critical error.";
            return;
        }

        ///  ------------------- CHECKS 7.  IF TABLES MATCHING BUT THERE IS NOTHING TO EXPORT
        bool DataExists = false;
        foreach (DataTable dt in dsExport.Tables)
        {
            if (dt.Rows.Count > 0)
            {
                DataExists = true;
                break;
            }
        }
        if (!DataExists)
        {
            lblMsg.Text = "No data available to send update.";
            return;
        }
        //===================================================================================
        //---------------- CHECKS DONE NOW GOING TO CREATE FILE 

        string SchemaFile = SaveTargetDir + "LFINotificationSchema.xml";
        string DataFile = SaveTargetDir + "LFINotificationData.xml";
                
        try
        {
            string ZipData = SaveTargetDir + LFINumber + ".zip";
            dsExport.WriteXmlSchema(SchemaFile);
            dsExport.WriteXml(DataFile);
            bool FileDone = false;
            using (ZipFile zip = new ZipFile())
            {
                zip.AddFile(SchemaFile);
                zip.AddFile(DataFile);
                zip.Save(ZipData);
                FileDone = true;
            }
            //--------------------------------
            if (FileDone)
            {
                if (ExportMode == "M")
                {
                    ExportFileToResponse(ZipData);
                    lblMsg.Text = "Data exported successfully. File Name : " + Path.GetFileName(ZipData);
                }
                else
                {
                    if (SendPacketMail(ZipData, "PMS - LFI Packet"))
                    {
                        lblMsg.Text = "Mail sent successfully.";
                    }
                }
            }
            else
            {
                lblMsg.Text = "Unable to create Zip File.";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Unable to export data. " + ex.Message;
        }
    }
    public void Export_JOB_COMMENTS()
    {
        //------------------
        ClearTempFiles();
        string SaveTargetDir = Server.MapPath("~/TEMP/");

        string strConectionString = sqlConnectionString.Replace("Master", "eMANAGER");
        SqlConnection SqlCon = new SqlConnection(strConectionString);
        SqlCommand cmd = new SqlCommand("", SqlCon);
        SqlDataAdapter AdpExport = new SqlDataAdapter(cmd);

        //------------------- CHECKS 3.  GET DATA TO BE EXPORTED FROM DATABASE

        DataSet ds = new DataSet();
        cmd.CommandText = ("select distinct P.* from VSL_VesselComponentJobOfficePlanning P INNER JOIN VSL_VesselComponentJobOfficePlanning_Comments C ON p.vESSELCODE=C.VESSELCODE AND P.OFFICE_SHIP=C.OFFICE_SHIP AND P.PlanningId=C.PlanningId where P.vesselcode='" + CurrentVessel + "' AND P.Office_Ship='S' AND P.COMPJOBID=" + DoneId + " AND C.ReceivedOn IS NULL");        
        AdpExport.Fill(ds);
        ds.Tables[0].TableName = "VSL_VesselComponentJobOfficePlanning";

        DataSet ds1 = new DataSet();
        cmd.CommandText = ("select C.* from VSL_VesselComponentJobOfficePlanning P INNER JOIN VSL_VesselComponentJobOfficePlanning_Comments C ON p.vESSELCODE=C.VESSELCODE AND P.OFFICE_SHIP=C.OFFICE_SHIP AND P.PlanningId=C.PlanningId where P.vesselcode='" + CurrentVessel + "' AND P.Office_Ship='S' AND P.COMPJOBID=" + DoneId + " AND C.ReceivedOn IS NULL");
        AdpExport.Fill(ds1);
        ds1.Tables[0].TableName = "VSL_VesselComponentJobOfficePlanning_Comments";

        DataSet ds2 = new DataSet();
        cmd.CommandText = ("select P.VesselCode,P.Office_Ship,P.PlanningId,p.COMPJOBID,P.LastHistoryId,P.HistoryId FROM VSL_VesselComponentJobOfficePlanning P WHERE P.vesselcode='" + CurrentVessel + "' AND P.COMPJOBID=" + DoneId);
        AdpExport.Fill(ds2);
        ds2.Tables[0].TableName = "VSL_VesselComponentJobOfficePlanning_Log";

        DataSet ds3 = new DataSet();
        cmd.CommandText = ("select C.VesselCode,C.Office_Ship,C.PlanningId,C.CommentId,C.ReceivedOn FROM VSL_VesselComponentJobOfficePlanning P INNER JOIN VSL_VesselComponentJobOfficePlanning_Comments C ON p.vESSELCODE=C.VESSELCODE AND P.OFFICE_SHIP=C.OFFICE_SHIP AND P.PlanningId=C.PlanningId where P.vesselcode='" + CurrentVessel + "' AND P.Office_Ship='O' AND P.COMPJOBID=" + DoneId);
        AdpExport.Fill(ds3);
        ds3.Tables[0].TableName = "VSL_VesselComponentJobOfficePlanning_Comments_Log";
        
        //------------------- CHECKS 3.  GET DATA TO BE EXPORTED FROM DATABASE

        DataSet dsExport = new DataSet();
        dsExport.Tables.Add(ds.Tables[0].Copy());
        dsExport.Tables.Add(ds1.Tables[0].Copy());
        dsExport.Tables.Add(ds2.Tables[0].Copy());
        dsExport.Tables.Add(ds3.Tables[0].Copy());

        ///  ------------------- CHECKS 4.  IF SQL ERROR NO TABLE RETURNED 
        if (dsExport == null)
        {
            lblMsg.Text = "Database not exists OR some critical error.";
            return;
        }
        ///  ------------------- CHECKS 5.  IF SQL ERROR NO TABLE RETURNED 
        if (dsExport.Tables.Count <= 0)
        {
            lblMsg.Text = "Database not exists OR some critical error.";
            return;
        }

        ///  ------------------- CHECKS 7.  IF TABLES MATCHING BUT THERE IS NOTHING TO EXPORT
        bool DataExists = false;
        foreach (DataTable dt in dsExport.Tables)
        {
            if (dt.Rows.Count > 0)
            {
                DataExists = true;
                break;
            }
        }
        if (!DataExists)
        {
            lblMsg.Text = "No data available to send update.";
            return;
        }
        //===================================================================================
        //---------------- CHECKS DONE NOW GOING TO CREATE FILE 

        string SchemaFile = SaveTargetDir + "SCHEMA_JobComments.xml";
        string DataFile = SaveTargetDir + "DATA_JobComments.xml";

        try
        {
            string ZipData = SaveTargetDir + "JOBCOMM_S_" + CurrentVessel + "_" + DoneId + "_" + DateTime.Now.ToString("dd-MMM-yyyy") + ".zip";
            dsExport.WriteXmlSchema(SchemaFile);
            dsExport.WriteXml(DataFile);
            bool FileDone = false;
            using (ZipFile zip = new ZipFile())
            {
                zip.AddFile(SchemaFile);
                zip.AddFile(DataFile);
                zip.Save(ZipData);
                FileDone = true;
            }
            //--------------------------------
            if (FileDone)
            {
                if (ExportMode == "M")
                {
                    ExportFileToResponse(ZipData);
                    lblMsg.Text = "Data exported successfully. File Name : " + Path.GetFileName(ZipData);
                }
                else
                {
                    if (SendPacketMail(ZipData, "PMS - JOB Comments Packet"))
                    {
                        lblMsg.Text = "Mail sent successfully.";
                    }
                }
            }
            else
            {
                lblMsg.Text = "Unable to create Zip File.";
            }
            //--------------------------------
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Unable to export data. " + ex.Message;
        }
    }

    public void Export_BREAKDOWN()
    {
        ClearTempFiles();
        string SaveTargetDir = Server.MapPath("~/TEMP/");

        string strConectionString = sqlConnectionString.Replace("Master", "eMANAGER");
        SqlConnection SqlCon = new SqlConnection(strConectionString);
        SqlCommand cmd = new SqlCommand("", SqlCon);
        SqlDataAdapter AdpExport = new SqlDataAdapter(cmd);

        ///  ------------------- CHECKS 3.  GET DATA TO BE EXPORTED FROM DATABASE
        cmd.CommandText = ("SELECT * FROM DBO.VSL_BreakDownMaster WHERE BreakDownNo = '" + DoneId_String + "' AND VesselCode='" + CurrentVessel + "'");
        DataTable dt = new DataTable();
        AdpExport.Fill(dt);
        dt.TableName = "VSL_BreakDownMaster";

        cmd.CommandText = ("SELECT * FROM DBO.VSL_BreakDownSpareDetails WHERE BreakDownNo = '" + DoneId_String + "' AND VesselCode='" + CurrentVessel + "'");
        DataTable dt1 = new DataTable();
        AdpExport.Fill(dt1);
        dt1.TableName = "VSL_BreakDownSpareDetails";

        cmd.CommandText = ("SELECT * FROM DBO.VSL_BreakDown_Attachments WHERE BreakDownNo = '" + DoneId_String + "' AND VesselCode='" + CurrentVessel + "'");
        DataTable dt2 = new DataTable();
        AdpExport.Fill(dt2);
        dt2.TableName = "VSL_BreakDown_Attachments";

        DataSet dsExport = new DataSet();
        dsExport.Tables.Add(dt.Copy());
        dsExport.Tables.Add(dt1.Copy());
        dsExport.Tables.Add(dt2.Copy());


        ///  ------------------- CHECKS 4.  IF SQL ERROR NO TABLE RETURNED 
        if (dsExport == null)
        {
            lblMsg.Text = "Database not exists OR some critical error.";
            return;
        }
        ///  ------------------- CHECKS 5.  IF SQL ERROR NO TABLE RETURNED 
        if (dsExport.Tables.Count <= 0)
        {
            lblMsg.Text = "Database not exists OR some critical error.";
            return;
        }

        ///  ------------------- CHECKS 7.  IF TABLES MATCHING BUT THERE IS NOTHING TO EXPORT
        bool DataExists = false;
        foreach (DataTable dt001 in dsExport.Tables)
        {
            if (dt001.Rows.Count > 0)
            {
                DataExists = true;
                break;
            }
        }
        if (!DataExists)
        {
            lblMsg.Text = "No data available to send update.";
            return;
        }
        //===================================================================================
        //---------------- CHECKS DONE NOW GOING TO CREATE FILE 

        string SchemaFile = SaveTargetDir + "BreakDownReportSchema.xml";
        string DataFile = SaveTargetDir + "BreakDownReportData.xml";

        try
        {
            string ZipData = SaveTargetDir + "BREAKDOWN_" + DoneId_String.Replace("/","-") + ".zip";
            dsExport.WriteXmlSchema(SchemaFile);
            dsExport.WriteXml(DataFile);
            bool FileDone = false;
            using (ZipFile zip = new ZipFile())
            {
                zip.AddFile(SchemaFile);
                zip.AddFile(DataFile);
                zip.Save(ZipData);
                FileDone = true;
            }
            //--------------------------------
            if (FileDone)
            {
                if (ExportMode == "M")
                {
                    ExportFileToResponse(ZipData);
                    lblMsg.Text = "Data exported successfully. File Name : " + Path.GetFileName(ZipData);
                }
                else
                {
                    if (SendPacketMail(ZipData, "PMS - BREAKDOWN REPORT"))
                    {
                        lblMsg.Text = "Mail sent successfully.";
                    }
                }
            }
            else
            {
                lblMsg.Text = "Unable to create Zip File.";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Unable to export data. " + ex.Message;
        }
    }
    public void Export_BREAKDOWN_ATTACHMENTS()
    {
        ClearTempFiles();
        string SaveTargetDir = Server.MapPath("~/TEMP/");

        string strConectionString = sqlConnectionString.Replace("Master", "eMANAGER");
        SqlConnection SqlCon = new SqlConnection(strConectionString);
        SqlCommand cmd = new SqlCommand("", SqlCon);
        SqlDataAdapter AdpExport = new SqlDataAdapter(cmd);
        ///  ------------------- CHECKS 3.  GET DATA TO BE EXPORTED FROM DATABASE
        cmd.CommandText = ("SELECT ReportDt FROM DBO.VSL_BreakDownMaster WHERE BreakDownNo = '" + DoneId_String + "' AND VesselCode='" + CurrentVessel + "'");
        DataSet dshist = new DataSet();
        AdpExport.Fill(dshist);
        if (dshist.Tables[0].Rows.Count > 0)
        {
            DateTime doneDate = Convert.ToDateTime(dshist.Tables[0].Rows[0][0]);
            cmd.CommandText = ("SELECT * FROM VSL_BreakDown_Attachments a WHERE BreakDownNo = '" + DoneId_String + "' AND VesselCode='" + CurrentVessel + "'");
            DataSet ds = new DataSet();
            AdpExport.Fill(ds);
            ds.Tables[0].TableName = "VSL_BreakDown_Attachments";
            ds.Tables[0].Columns.Add("Attachment", typeof(System.Data.SqlTypes.SqlBinary));
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                string FilePath = Server.MapPath(ProjectCommon.getLinkFolder(doneDate) + dr["FileName"].ToString());
                if (File.Exists(FilePath))
                    dr["Attachment"] = File.ReadAllBytes(FilePath);
            }

            DataSet dsExport = new DataSet();
            dsExport.Tables.Add(ds.Tables[0].Copy());


            ///  ------------------- CHECKS 4.  IF SQL ERROR NO TABLE RETURNED 
            if (dsExport == null)
            {
                lblMsg.Text = "Database not exists OR some critical error.";
                return;
            }
            ///  ------------------- CHECKS 5.  IF SQL ERROR NO TABLE RETURNED 
            if (dsExport.Tables.Count <= 0)
            {
                lblMsg.Text = "Database not exists OR some critical error.";
                return;
            }

            ///  ------------------- CHECKS 7.  IF TABLES MATCHING BUT THERE IS NOTHING TO EXPORT
            bool DataExists = false;
            foreach (DataTable dt in dsExport.Tables)
            {
                if (dt.Rows.Count > 0)
                {
                    DataExists = true;
                    break;
                }
            }
            if (!DataExists)
            {
                lblMsg.Text = "No data available to send update.";
                return;
            }
            //===================================================================================
            //---------------- CHECKS DONE NOW GOING TO CREATE FILE 

            string SchemaFile = SaveTargetDir + "BreakDownAttachments_Schema.xml";
            string DataFile = SaveTargetDir + "BreakDownAttachments_Data.xml";

            try
            {
                string ZipData = SaveTargetDir + "BREAKDOWNATTACHMENTS_" + CurrentVessel + "_S_" + DoneId + "_" + DateTime.Now.ToString("dd-MMM-yyyy") + ".zip";
                dsExport.WriteXmlSchema(SchemaFile);
                dsExport.WriteXml(DataFile);
                bool FileDone = false;
                using (ZipFile zip = new ZipFile())
                {
                    zip.AddFile(SchemaFile);
                    zip.AddFile(DataFile);
                    zip.Save(ZipData);
                    FileDone = true;
                }
                //--------------------------------
                if (FileDone)
                {
                    if (ExportMode == "M")
                    {
                        ExportFileToResponse(ZipData);
                        lblMsg.Text = "Data exported successfully. File Name : " + Path.GetFileName(ZipData);
                    }
                    else
                    {
                        if (SendPacketMail(ZipData, "BREAKDOWN-ATTACHMENTS-Packet"))
                        {
                            lblMsg.Text = "Mail sent successfully.";
                        }
                    }
                    //-------------------------
                    //cmd.CommandText = ("EXEC DBO.VSL_VESSEL_EXPORT_JOBHISTORY_ATTACHMENTS '" + CurrentVessel + "'," + DoneId + ",'" + Session["UserName"].ToString() + "'");
                    //cmd.Parameters.Clear();
                    //cmd.CommandType = CommandType.Text;
                    //try
                    //{
                    //    SqlCon.Open();
                    //    cmd.ExecuteNonQuery();
                    //}
                    //catch
                    //{ }
                    //finally
                    //{
                    //    if (SqlCon.State != ConnectionState.Closed)
                    //        SqlCon.Close();
                    //}
                    //-------------------------
                }
                else
                {
                    lblMsg.Text = "Unable to create Zip File.";
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = "Unable to export data. " + ex.Message;
            }
        }
        else
            lblMsg.Text = "Unable to export data. Job history not found.";
    }

    public void Export_From_FC()
    {
        ClearTempFiles();
        string SaveTargetDir = Server.MapPath("~/TEMP/");

        string strConectionString = sqlConnectionString.Replace("Master", "eMANAGER");
        SqlConnection SqlCon = new SqlConnection(strConectionString);
        SqlCommand cmd = new SqlCommand("", SqlCon);
        SqlDataAdapter AdpExport = new SqlDataAdapter(cmd);

        ///  ------------------- CHECKS 3.  GET DATA TO BE EXPORTED FROM DATABASE


        cmd.CommandText = ("SELECT * FROM DBO.FocusCamp_Vessel_Notifications WHERE FocusCampID = " + DoneId + " AND VesselCode='" + CurrentVessel + "'");
        DataSet ds = new DataSet();
        AdpExport.Fill(ds);
        ds.Tables[0].TableName = "FocusCamp_Vessel_Notifications";

        DataTable dtNum = Common.Execute_Procedures_Select_ByQuery("SELECT [FocusCampNumber] FROM [dbo].[FocusCamp] WHERE FocusCampId = " + DoneId);

        string FCNumber = dtNum.Rows[0]["FocusCampNumber"].ToString();

        DataSet dsExport = new DataSet();
        dsExport.Tables.Add(ds.Tables[0].Copy());         

        ///  ------------------- CHECKS 4.  IF SQL ERROR NO TABLE RETURNED 
        if (dsExport == null)
        {
            lblMsg.Text = "Database not exists OR some critical error.";
            return;
        }
        ///  ------------------- CHECKS 5.  IF SQL ERROR NO TABLE RETURNED 
        if (dsExport.Tables.Count <= 0)
        {
            lblMsg.Text = "Database not exists OR some critical error.";
            return;
        }

        ///  ------------------- CHECKS 7.  IF TABLES MATCHING BUT THERE IS NOTHING TO EXPORT
        bool DataExists = false;
        foreach (DataTable dt in dsExport.Tables)
        {
            if (dt.Rows.Count > 0)
            {
                DataExists = true;
                break;
            }
        }
        if (!DataExists)
        {
            lblMsg.Text = "No data available to send update.";
            return;
        }
        //===================================================================================
        //---------------- CHECKS DONE NOW GOING TO CREATE FILE 

        string SchemaFile = SaveTargetDir + "FocusCampNotificationSchema.xml";
        string DataFile = SaveTargetDir + "FocusCampNotificationData.xml";

        

        try
        {
            string ZipData = SaveTargetDir + FCNumber + ".zip";
            dsExport.WriteXmlSchema(SchemaFile);
            dsExport.WriteXml(DataFile);
            bool FileDone = false;
            using (ZipFile zip = new ZipFile())
            {
                zip.AddFile(SchemaFile);
                zip.AddFile(DataFile);
                zip.Save(ZipData);
                FileDone = true;
            }
            //--------------------------------
            if (FileDone)
            {
                if (ExportMode == "M")
                {
                    ExportFileToResponse(ZipData);
                    lblMsg.Text = "Data exported successfully. File Name : " + Path.GetFileName(ZipData);
                }
                else
                {
                    if (SendPacketMail(ZipData, "PMS - Focus Campaign Packet"))
                    {
                        lblMsg.Text = "Mail sent successfully.";
                    }
                }
            }
            else
            {
                lblMsg.Text = "Unable to create Zip File.";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Unable to export data. " + ex.Message;
        }
    }
    public void Export_From_Regulation()
    {
        ClearTempFiles();
        string SaveTargetDir = Server.MapPath("~/TEMP/");

        string strConectionString = sqlConnectionString.Replace("Master", "eMANAGER");
        SqlConnection SqlCon = new SqlConnection(strConectionString);
        SqlCommand cmd = new SqlCommand("", SqlCon);
        SqlDataAdapter AdpExport = new SqlDataAdapter(cmd);

        ///  ------------------- CHECKS 3.  GET DATA TO BE EXPORTED FROM DATABASE


        cmd.CommandText = ("SELECT * FROM DBO.Reg_Vessel_Notifications WHERE RegId = " + DoneId + " AND VesselCode='" + CurrentVessel + "'");
        DataSet ds = new DataSet();
        AdpExport.Fill(ds);
        ds.Tables[0].TableName = "REG_Vessel_Notifications";

        DataTable dtNum = Common.Execute_Procedures_Select_ByQuery("SELECT [RegNumber] FROM [dbo].[Reg_Regulation] WHERE RegId = " + DoneId);

        string REGNumber = dtNum.Rows[0]["RegNumber"].ToString();

        DataSet dsExport = new DataSet();
        dsExport.Tables.Add(ds.Tables[0].Copy());

        ///  ------------------- CHECKS 4.  IF SQL ERROR NO TABLE RETURNED 
        if (dsExport == null)
        {
            lblMsg.Text = "Database not exists OR some critical error.";
            return;
        }
        ///  ------------------- CHECKS 5.  IF SQL ERROR NO TABLE RETURNED 
        if (dsExport.Tables.Count <= 0)
        {
            lblMsg.Text = "Database not exists OR some critical error.";
            return;
        }

        ///  ------------------- CHECKS 7.  IF TABLES MATCHING BUT THERE IS NOTHING TO EXPORT
        bool DataExists = false;
        foreach (DataTable dt in dsExport.Tables)
        {
            if (dt.Rows.Count > 0)
            {
                DataExists = true;
                break;
            }
        }
        if (!DataExists)
        {
            lblMsg.Text = "No data available to send update.";
            return;
        }
        //===================================================================================
        //---------------- CHECKS DONE NOW GOING TO CREATE FILE 

        string SchemaFile = SaveTargetDir + "REGNotificationSchema.xml";
        string DataFile = SaveTargetDir + "REGNotificationData.xml";

        try
        {
            string ZipData = SaveTargetDir + REGNumber + ".zip";
            dsExport.WriteXmlSchema(SchemaFile);
            dsExport.WriteXml(DataFile);
            bool FileDone = false;
            using (ZipFile zip = new ZipFile())
            {
                zip.AddFile(SchemaFile);
                zip.AddFile(DataFile);
                zip.Save(ZipData);
                FileDone = true;
            }
            //--------------------------------
            if (FileDone)
            {
                if (ExportMode == "M")
                {
                    ExportFileToResponse(ZipData);
                    lblMsg.Text = "Data exported successfully. File Name : " + Path.GetFileName(ZipData);
                }
                else
                {
                    if (SendPacketMail(ZipData, "PMS - Regulation Packet"))
                    {
                        lblMsg.Text = "Mail sent successfully.";
                    }
                }
            }
            else
            {
                lblMsg.Text = "Unable to create Zip File.";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Unable to export data. " + ex.Message;
        }
    }
    public void Export_From_Circular()
    {
        ClearTempFiles();
        string SaveTargetDir = Server.MapPath("~/TEMP/");

        string strConectionString = sqlConnectionString.Replace("Master", "eMANAGER");
        SqlConnection SqlCon = new SqlConnection(strConectionString);
        SqlCommand cmd = new SqlCommand("", SqlCon);
        SqlDataAdapter AdpExport = new SqlDataAdapter(cmd);

        ///  ------------------- CHECKS 3.  GET DATA TO BE EXPORTED FROM DATABASE


        cmd.CommandText = ("SELECT * FROM DBO.Cir_Vessel_Notifications WHERE CId = " + DoneId + " AND VesselCode='" + CurrentVessel + "'");
        DataSet ds = new DataSet();
        AdpExport.Fill(ds);
        ds.Tables[0].TableName = "Cir_Vessel_Notifications";

        DataTable dtNum = Common.Execute_Procedures_Select_ByQuery("SELECT [CircularNumber] FROM [dbo].[Cir_Circular] WHERE CId = " + DoneId);

        string CircularNumber = dtNum.Rows[0]["CircularNumber"].ToString();

        DataSet dsExport = new DataSet();
        dsExport.Tables.Add(ds.Tables[0].Copy());

        ///  ------------------- CHECKS 4.  IF SQL ERROR NO TABLE RETURNED 
        if (dsExport == null)
        {
            lblMsg.Text = "Database not exists OR some critical error.";
            return;
        }
        ///  ------------------- CHECKS 5.  IF SQL ERROR NO TABLE RETURNED 
        if (dsExport.Tables.Count <= 0)
        {
            lblMsg.Text = "Database not exists OR some critical error.";
            return;
        }

        ///  ------------------- CHECKS 7.  IF TABLES MATCHING BUT THERE IS NOTHING TO EXPORT
        bool DataExists = false;
        foreach (DataTable dt in dsExport.Tables)
        {
            if (dt.Rows.Count > 0)
            {
                DataExists = true;
                break;
            }
        }
        if (!DataExists)
        {
            lblMsg.Text = "No data available to send update.";
            return;
        }
        //===================================================================================
        //---------------- CHECKS DONE NOW GOING TO CREATE FILE 

        string SchemaFile = SaveTargetDir + "CircularNotificationSchema.xml";
        string DataFile = SaveTargetDir + "CircularNotificationData.xml";

       
        try
        {
            string ZipData = SaveTargetDir + CircularNumber.Replace("/", "-") + ".zip";
            dsExport.WriteXmlSchema(SchemaFile);
            dsExport.WriteXml(DataFile);
            bool FileDone = false;
            using (ZipFile zip = new ZipFile())
            {
                zip.AddFile(SchemaFile);
                zip.AddFile(DataFile);
                zip.Save(ZipData);
                FileDone = true;
            }
            //--------------------------------
            if (FileDone)
            {
                if (ExportMode == "M")
                {
                    ExportFileToResponse(ZipData);
                    lblMsg.Text = "Data exported successfully. File Name : " + Path.GetFileName(ZipData);
                }
                else
                {
                    if (SendPacketMail(ZipData, "PMS - Circular Packet"))
                    {
                        lblMsg.Text = "Mail sent successfully.";
                    }
                }
            }
            else
            {
                lblMsg.Text = "Unable to create Zip File.";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Unable to export data. " + ex.Message;
        }
    }
    public void Export_From_Inventory()
    {
        ClearTempFiles();
        string SaveTargetDir = Server.MapPath("~/TEMP/");

        string strConectionString = sqlConnectionString.Replace("Master", "eMANAGER");
        SqlConnection SqlCon = new SqlConnection(strConectionString);
        SqlCommand cmd = new SqlCommand("", SqlCon);
        SqlDataAdapter AdpExport = new SqlDataAdapter(cmd);

        ///  ------------------- CHECKS 3.  GET DATA TO BE EXPORTED FROM DATABASE


        DataSet ds = new DataSet();
        string SQL = "SELECT * FROM [dbo].[MP_VSL_InventoryMaster] WHERE VESSELCODE='" + CurrentVessel + "' AND INVENTORYID=" + DoneId;
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
        dt.TableName = "MP_VSL_InventoryMaster";
        ds.Tables.Add(dt.Copy());

        SQL = "SELECT * FROM [dbo].[MP_VSL_InventoryDetails] WHERE VESSELCODE='" + CurrentVessel + "' AND INVENTORYID=" + DoneId;
        dt = Common.Execute_Procedures_Select_ByQuery(SQL);
        dt.TableName = "MP_VSL_InventoryDetails";
        ds.Tables.Add(dt.Copy());


        string SchemaFile = Server.MapPath("~/TEMP/Inventory_Schema.xml");
        string DataFile = Server.MapPath("~/TEMP/Inventory_Data.xml");
        string ZipFile = Server.MapPath("~/TEMP/INVENTORY_REPORT_" + CurrentVessel + "_S_" + DoneId + "_" + DateTime.Now.ToString("dd-MMM-yyyy") + ".zip");

        ds.WriteXmlSchema(SchemaFile);
        ds.WriteXml(DataFile);

        using (ZipFile zip = new ZipFile())
        {
            zip.AddFile(SchemaFile);
            zip.AddFile(DataFile);
            zip.Save(ZipFile);
        }


        if (File.Exists(ZipFile))
        {
            if (ExportMode == "M")
            {
                ExportFileToResponse(ZipFile);
                lblMsg.Text = "Data exported successfully. File Name : " + Path.GetFileName(ZipFile);
            }
            else
            {
                if (SendPacketMail(ZipFile, "PMS - Inventory Report"))
                {
                    lblMsg.Text = "Mail sent successfully.";
                }
            }
        }
    }
    public void Export_From_Supply()
    {
        ClearTempFiles();
        string SaveTargetDir = Server.MapPath("~/TEMP/");

        string strConectionString = sqlConnectionString.Replace("Master", "eMANAGER");
        SqlConnection SqlCon = new SqlConnection(strConectionString);
        SqlCommand cmd = new SqlCommand("", SqlCon);
        SqlDataAdapter AdpExport = new SqlDataAdapter(cmd);

        ///  ------------------- CHECKS 3.  GET DATA TO BE EXPORTED FROM DATABASE


        DataSet ds = new DataSet();
        string SQL = "SELECT * FROM [dbo].[MP_VSL_SupplyMaster] WHERE [VesselCode]='" + CurrentVessel + "' AND SupplyId = " + DoneId;
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
        dt.TableName = "MP_VSL_SupplyMaster";
        ds.Tables.Add(dt.Copy());

        SQL = "SELECT * FROM [dbo].[MP_VSL_SupplyDetails] WHERE [VesselCode]='" + CurrentVessel + "'  AND SupplyId = " + DoneId;
        dt = Common.Execute_Procedures_Select_ByQuery(SQL);
        dt.TableName = "MP_VSL_SupplyDetails";
        ds.Tables.Add(dt.Copy());


        string SchemaFile = Server.MapPath("~/TEMP/Supply_Schema.xml");
        string DataFile = Server.MapPath("~/TEMP/Supply_Data.xml");
        string ZipFile = Server.MapPath("~/TEMP/Supply_REPORT_" + CurrentVessel + "_S_" + DoneId + "_" + DateTime.Now.ToString("dd-MMM-yyyy") + ".zip");

        ds.WriteXmlSchema(SchemaFile);
        ds.WriteXml(DataFile);

        using (ZipFile zip = new ZipFile())
        {
            zip.AddFile(SchemaFile);
            zip.AddFile(DataFile);
            zip.Save(ZipFile);
        }

        if (File.Exists(ZipFile))
        {
            if (ExportMode == "M")
            {
                ExportFileToResponse(ZipFile);
                lblMsg.Text = "Data exported successfully. File Name : " + Path.GetFileName(ZipFile);
            }
            else
            {
                if (SendPacketMail(ZipFile, "PMS - Supply Report"))
                {
                    lblMsg.Text = "Mail sent successfully.";
                }
            }
        }
    }
    public void Export_From_MealReport()
    {
        ClearTempFiles();
        string SaveTargetDir = Server.MapPath("~/TEMP/");

        string strConectionString = sqlConnectionString.Replace("Master", "eMANAGER");
        SqlConnection SqlCon = new SqlConnection(strConectionString);
        SqlCommand cmd = new SqlCommand("", SqlCon);
        SqlDataAdapter AdpExport = new SqlDataAdapter(cmd);

        //  ------------------- CHECKS 3.  GET DATA TO BE EXPORTED FROM DATABASE

        DataSet ds = new DataSet();
        string SQL = "SELECT * FROM [dbo].[MP_VSL_ShipStaffMaster] WHERE [VesselCode]='" + CurrentVessel + "' AND [ShipStaffId]=" + DoneId;
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
        dt.TableName = "MP_VSL_ShipStaffMaster";
        ds.Tables.Add(dt.Copy());

        SQL = "SELECT * FROM [dbo].[MP_VSL_ShipStaffDetails] WHERE [VesselCode]='" + CurrentVessel + "' AND [ShipStaffId]=" + DoneId;
        dt = Common.Execute_Procedures_Select_ByQuery(SQL);
        dt.TableName = "MP_VSL_ShipStaffDetails";
        ds.Tables.Add(dt.Copy());

        string SchemaFile = Server.MapPath("~/TEMP/Account_Schema.xml");
        string DataFile = Server.MapPath("~/TEMP/Account_Data.xml");
        string ZipFile = Server.MapPath("~/TEMP/MEAL_REPORT_" + Session["CurrentShip"].ToString().Trim() + "_S_" + DoneId.ToString() + "_" + DateTime.Now.ToString("dd-MMM-yyyy") + ".zip");

        ds.WriteXmlSchema(SchemaFile);
        ds.WriteXml(DataFile);

        using (ZipFile zip = new ZipFile())
        {
            zip.AddFile(SchemaFile);
            zip.AddFile(DataFile);
            zip.Save(ZipFile);
        }

        if (File.Exists(ZipFile))
        {
            if (ExportMode == "M")
            {
                ExportFileToResponse(ZipFile);
                lblMsg.Text = "Data exported successfully. File Name : " + Path.GetFileName(ZipFile);
            }
            else
            {
                if (SendPacketMail(ZipFile, "PMS - Meal Report"))
                {
                    lblMsg.Text = "Mail sent successfully.";
                }
            }
        }
    }
    public void Export_From_MealReport_Acc()
    {
        ClearTempFiles();
        string SaveTargetDir = Server.MapPath("~/TEMP/");

        string strConectionString = sqlConnectionString.Replace("Master", "eMANAGER");
        SqlConnection SqlCon = new SqlConnection(strConectionString);
        SqlCommand cmd = new SqlCommand("", SqlCon);
        SqlDataAdapter AdpExport = new SqlDataAdapter(cmd);

        //  ------------------- CHECKS 3.  GET DATA TO BE EXPORTED FROM DATABASE

        DataSet ds = new DataSet();
        string SQL = "SELECT * FROM [dbo].[MP_VSL_AccountMaster] WHERE [VesselCode]='" + CurrentVessel + "' AND [AccountId]=" + DoneId;
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
        dt.TableName = "MP_VSL_AccountMaster";
        ds.Tables.Add(dt.Copy());

        SQL = "SELECT * FROM [dbo].[MP_VSL_AccountDetails] WHERE [VesselCode]='" + CurrentVessel + "' AND [AccountId]=" + DoneId;
        dt = Common.Execute_Procedures_Select_ByQuery(SQL);
        dt.TableName = "MP_VSL_AccountDetails";
        ds.Tables.Add(dt.Copy());


        string SchemaFile = Server.MapPath("~/TEMP/Account_Schema.xml");
        string DataFile = Server.MapPath("~/TEMP/Account_Data.xml");
        string ZipFile = Server.MapPath("~/TEMP/MEAL_REPORT_" + Session["CurrentShip"].ToString().Trim() + "_S_" + DoneId.ToString() + "_" + DateTime.Now.ToString("dd-MMM-yyyy") + ".zip");

        ds.WriteXmlSchema(SchemaFile);
        ds.WriteXml(DataFile);

        using (ZipFile zip = new ZipFile())
        {
            zip.AddFile(SchemaFile);
            zip.AddFile(DataFile);
            zip.Save(ZipFile);
        }

        if (File.Exists(ZipFile))
        {
            if (ExportMode == "M")
            {
                ExportFileToResponse(ZipFile);
                lblMsg.Text = "Data exported successfully. File Name : " + Path.GetFileName(ZipFile);
            }
            else
            {
                if (SendPacketMail(ZipFile, "PMS - Meal Report"))
                {
                    lblMsg.Text = "Mail sent successfully.";
                }
            }
        }
    }
    public void Export_From_SCM()
    {
        ClearTempFiles();
        string SaveTargetDir = Server.MapPath("~/TEMP/");

        string strConectionString = sqlConnectionString.Replace("Master", "eMANAGER");
        SqlConnection SqlCon = new SqlConnection(strConectionString);
        SqlCommand cmd = new SqlCommand("", SqlCon);
        SqlDataAdapter AdpExport = new SqlDataAdapter(cmd);

        ///  ------------------- CHECKS 3.  GET DATA TO BE EXPORTED FROM DATABASE


        cmd.CommandText = ("SELECT * FROM DBO.SCM_Master WHERE ReportsPK = " + DoneId + " AND VesselCode='" + CurrentVessel + "'");
        DataSet ds = new DataSet();
        AdpExport.Fill(ds);
        ds.Tables[0].TableName = "SCM_Master";

        cmd.CommandText = ("SELECT * FROM DBO.SCM_RANKDETAILS WHERE ReportsPk=" + DoneId + " AND VesselCode='" + CurrentVessel + "'");
        DataSet ds1 = new DataSet();
        AdpExport.Fill(ds1);
        ds1.Tables[0].TableName = "SCM_RANKDETAILS";


        cmd.CommandText = ("SELECT * FROM [dbo].[SCM_NCRDETAILS] WHERE [ReportsPK] = " + DoneId + "  AND [VesselCode] = '" + CurrentVessel + "'");
        DataSet ds2 = new DataSet();
        AdpExport.Fill(ds2);
        ds2.Tables[0].TableName = "SCM_NCRDETAILS";

        DataSet dsExport = new DataSet();
        dsExport.Tables.Add(ds.Tables[0].Copy());
        dsExport.Tables.Add(ds1.Tables[0].Copy());
        dsExport.Tables.Add(ds2.Tables[0].Copy());


        ///  ------------------- CHECKS 4.  IF SQL ERROR NO TABLE RETURNED 
        if (dsExport == null)
        {
            lblMsg.Text = "Database not exists OR some critical error.";
            return;
        }
        ///  ------------------- CHECKS 5.  IF SQL ERROR NO TABLE RETURNED 
        if (dsExport.Tables.Count <= 0)
        {
            lblMsg.Text = "Database not exists OR some critical error.";
            return;
        }

        ///  ------------------- CHECKS 7.  IF TABLES MATCHING BUT THERE IS NOTHING TO EXPORT
        bool DataExists = false;
        foreach (DataTable dt in dsExport.Tables)
        {
            if (dt.Rows.Count > 0)
            {
                DataExists = true;
                break;
            }
        }
        if (!DataExists)
        {
            lblMsg.Text = "No data available to send update.";
            return;
        }
        //===================================================================================
        //---------------- CHECKS DONE NOW GOING TO CREATE FILE 

        string SchemaFile = SaveTargetDir + "SCMSchema.xml";
        string DataFile = SaveTargetDir + "SCMData.xml";
        

        try
        {
            string ZipData = SaveTargetDir + "SCM_S_" + CurrentVessel + "_" + DoneId + ".zip";
            dsExport.WriteXmlSchema(SchemaFile);
            dsExport.WriteXml(DataFile);
            bool FileDone = false;
            using (ZipFile zip = new ZipFile())
            {
                zip.AddFile(SchemaFile);
                zip.AddFile(DataFile);
                zip.Save(ZipData);
                FileDone = true;
            }
            //--------------------------------
            if (FileDone)
            {
                if (ExportMode == "M")
                {
                    ExportFileToResponse(ZipData);
                    lblMsg.Text = "Data exported successfully. File Name : " + Path.GetFileName(ZipData);
                }
                else
                {
                    if (SendPacketMail(ZipData, "PMS - SCM Packet"))
                    {
                        lblMsg.Text = "Mail sent successfully.";
                    }
                }
            }
            else
            {
                lblMsg.Text = "Unable to create Zip File.";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Unable to export data. " + ex.Message;
        }
    }
    public void Export_From_VNA()
    {
        ClearTempFiles();
        string SaveTargetDir = Server.MapPath("~/TEMP/");

        string strConectionString = sqlConnectionString.Replace("Master", "eMANAGER");
        SqlConnection SqlCon = new SqlConnection(strConectionString);
        SqlCommand cmd = new SqlCommand("", SqlCon);
        SqlDataAdapter AdpExport = new SqlDataAdapter(cmd);

        ///  ------------------- CHECKS 3.  GET DATA TO BE EXPORTED FROM DATABASE

        DataSet ds = new DataSet();

        string SQLEvt = "select * from DBO.VNA_MASTER where VESSELCODE='" + CurrentVessel + "' AND VNAID=" + DoneId;
        DataTable dt01 = Common.Execute_Procedures_Select_ByQuery(SQLEvt);
        dt01.TableName = "VNA_MASTER";
        ds.Tables.Add(dt01.Copy());

        SQLEvt = "select * from DBO.vna_details WHERE VESSELCODE='" + CurrentVessel + "' AND VNAID=" + DoneId.ToString();
        DataTable dt11 = Common.Execute_Procedures_Select_ByQuery(SQLEvt);
        dt11.TableName = "vna_details";
        ds.Tables.Add(dt11.Copy());

        string vnano=dt01.Rows[0]["VNANO"].ToString();

        ///  ------------------- CHECKS 4.  IF SQL ERROR NO TABLE RETURNED 
        if (ds == null)
        {
            lblMsg.Text = "Database not exists OR some critical error.";
            return;
        }
        ///  ------------------- CHECKS 5.  IF SQL ERROR NO TABLE RETURNED 
        if (ds.Tables.Count <= 0)
        {
            lblMsg.Text = "Database not exists OR some critical error.";
            return;
        }

        ///  ------------------- CHECKS 7.  IF TABLES MATCHING BUT THERE IS NOTHING TO EXPORT
        bool DataExists = false;
        foreach (DataTable dt in ds.Tables)
        {
            if (dt.Rows.Count > 0)
            {
                DataExists = true;
                break;
            }
        }
        if (!DataExists)
        {
            lblMsg.Text = "No data available to send update.";
            return;
        }
        //===================================================================================
        //---------------- CHECKS DONE NOW GOING TO CREATE FILE 

        string SchemaFile = SaveTargetDir + "SCMSchema.xml";
        string DataFile = SaveTargetDir + "SCMData.xml";


        try
        {
            string ZipData = SaveTargetDir + "VNA_S_" + vnano + ".zip";
            ds.WriteXmlSchema(SchemaFile);
            ds.WriteXml(DataFile);
            bool FileDone = false;
            using (ZipFile zip = new ZipFile())
            {
                zip.AddFile(SchemaFile);
                zip.AddFile(DataFile);
                zip.Save(ZipData);
                FileDone = true;
            }
            //--------------------------------
            if (FileDone)
            {
                if (ExportMode == "M")
                {
                    ExportFileToResponse(ZipData);
                    lblMsg.Text = "Data exported successfully. File Name : " + Path.GetFileName(ZipData);
                }
                else
                {
                    if (SendPacketMail(ZipData, "PMS - VNA Packet"))
                    {
                        lblMsg.Text = "Mail sent successfully.";
                    }
                }
            }
            else
            {
                lblMsg.Text = "Unable to create Zip File.";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Unable to export data. " + ex.Message;
        }
    }
    public void Export_From_MWUC()
    {
        ClearTempFiles();
        string SaveTargetDir = Server.MapPath("~/TEMP/");

        string strConectionString = sqlConnectionString.Replace("Master", "eMANAGER");
        SqlConnection SqlCon = new SqlConnection(strConectionString);
        SqlCommand cmd = new SqlCommand("", SqlCon);
        SqlDataAdapter AdpExport = new SqlDataAdapter(cmd);

        ///  ------------------- CHECKS 3.  GET DATA TO BE EXPORTED FROM DATABASE


        cmd.CommandText = ("SELECT * FROM MWUC_MASTER WHERE VESSELCODE = '" + CurrentVessel + "' AND TABLEID =" + DoneId);
        DataSet ds = new DataSet();
        AdpExport.Fill(ds);
        ds.Tables[0].TableName = "MWUC_MASTER";

        cmd.CommandText = ("SELECT * FROM MWUC_DETAILS WHERE VESSELCODE = '" + CurrentVessel + "' AND TABLEID =" + DoneId);
        DataSet ds1 = new DataSet();
        AdpExport.Fill(ds1);
        ds1.Tables[0].TableName = "MWUC_DETAILS";


        cmd.CommandText = ("SELECT * FROM MWUC_CATMASTER WHERE VESSELCODE = '" + CurrentVessel + "' ");
        DataSet ds2 = new DataSet();
        AdpExport.Fill(ds2);
        ds2.Tables[0].TableName = "MWUC_CATMASTER";

        DataSet dsExport = new DataSet();
        dsExport.Tables.Add(ds.Tables[0].Copy());
        dsExport.Tables.Add(ds1.Tables[0].Copy());
        dsExport.Tables.Add(ds2.Tables[0].Copy());


        ///  ------------------- CHECKS 4.  IF SQL ERROR NO TABLE RETURNED 
        if (dsExport == null)
        {
            lblMsg.Text = "Database not exists OR some critical error.";
            return;
        }
        ///  ------------------- CHECKS 5.  IF SQL ERROR NO TABLE RETURNED 
        if (dsExport.Tables.Count <= 0)
        {
            lblMsg.Text = "Database not exists OR some critical error.";
            return;
        }

        ///  ------------------- CHECKS 7.  IF TABLES MATCHING BUT THERE IS NOTHING TO EXPORT
        bool DataExists = false;
        foreach (DataTable dt in dsExport.Tables)
        {
            if (dt.Rows.Count > 0)
            {
                DataExists = true;
                break;
            }
        }
        if (!DataExists)
        {
            lblMsg.Text = "No data available to send update.";
            return;
        }
        //===================================================================================
        //---------------- CHECKS DONE NOW GOING TO CREATE FILE 

        string SchemaFile = SaveTargetDir + "MWUC_Schema.xml";
        string DataFile = SaveTargetDir + "MWUC_Data.xml";

        try
        {
            string ZipData = SaveTargetDir + "MWUC_" + CurrentVessel + "_S_" + DoneId + "_" + DateTime.Now.ToString("dd-MMM-yyyy") + ".zip";
            dsExport.WriteXmlSchema(SchemaFile);
            dsExport.WriteXml(DataFile);
            bool FileDone = false;
            using (ZipFile zip = new ZipFile())
            {
                zip.AddFile(SchemaFile);
                zip.AddFile(DataFile);
                zip.Save(ZipData);
                FileDone = true;
            }
            //--------------------------------
            if (FileDone)
            {
                if (ExportMode == "M")
                {
                    ExportFileToResponse(ZipData);
                    lblMsg.Text = "Data exported successfully. File Name : " + Path.GetFileName(ZipData);
                }
                else
                {
                    if (SendPacketMail(ZipData, "PMS - MWUC Packet"))
                    {
                        lblMsg.Text = "Mail sent successfully.";
                    }
                }
            }
            else
            {
                lblMsg.Text = "Unable to create Zip File.";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Unable to export data. " + ex.Message;
        }
    }
    public void Export_JOBHISTORY_ATTACHMENTS()
    {
        ClearTempFiles();
        string SaveTargetDir = Server.MapPath("~/TEMP/");

        string strConectionString = sqlConnectionString.Replace("Master", "eMANAGER");
        SqlConnection SqlCon = new SqlConnection(strConectionString);
        SqlCommand cmd = new SqlCommand("", SqlCon);
        SqlDataAdapter AdpExport = new SqlDataAdapter(cmd);
        ///  ------------------- CHECKS 3.  GET DATA TO BE EXPORTED FROM DATABASE
        cmd.CommandText = ("select donedate from VSL_VesselJobUpdateHistory h WHERE VESSELCODE = '" + CurrentVessel + "' AND HISTORYID =" + DoneId);
        DataSet dshist = new DataSet();
        AdpExport.Fill(dshist);
        if (dshist.Tables[0].Rows.Count > 0)
        {
            DateTime doneDate = Convert.ToDateTime(dshist.Tables[0].Rows[0][0]);
            cmd.CommandText = ("SELECT * FROM VSL_VesselJobUpdateHistoryAttachments a WHERE VESSELCODE = '" + CurrentVessel + "' AND HISTORYID =" + DoneId);
            DataSet ds = new DataSet();
            AdpExport.Fill(ds);
            ds.Tables[0].TableName = "VSL_VesselJobUpdateHistoryAttachments";
            ds.Tables[0].Columns.Add("Attachment", typeof(System.Data.SqlTypes.SqlBinary));
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                string FilePath = Server.MapPath(ProjectCommon.getLinkFolder(doneDate) + dr["FileName"].ToString());
                if(File.Exists(FilePath))
                    dr["Attachment"] = File.ReadAllBytes(FilePath);
            }

            DataSet dsExport = new DataSet();
            dsExport.Tables.Add(ds.Tables[0].Copy());


            ///  ------------------- CHECKS 4.  IF SQL ERROR NO TABLE RETURNED 
            if (dsExport == null)
            {
                lblMsg.Text = "Database not exists OR some critical error.";
                return;
            }
            ///  ------------------- CHECKS 5.  IF SQL ERROR NO TABLE RETURNED 
            if (dsExport.Tables.Count <= 0)
            {
                lblMsg.Text = "Database not exists OR some critical error.";
                return;
            }

            ///  ------------------- CHECKS 7.  IF TABLES MATCHING BUT THERE IS NOTHING TO EXPORT
            bool DataExists = false;
            foreach (DataTable dt in dsExport.Tables)
            {
                if (dt.Rows.Count > 0)
                {
                    DataExists = true;
                    break;
                }
            }
            if (!DataExists)
            {
                lblMsg.Text = "No data available to send update.";
                return;
            }
            //===================================================================================
            //---------------- CHECKS DONE NOW GOING TO CREATE FILE 

            string SchemaFile = SaveTargetDir + "JobHistoryAttachments_Schema.xml";
            string DataFile = SaveTargetDir + "JobHistoryAttachments_Data.xml";

            try
            {
                //string ZipData = SaveTargetDir + "JHAttachments_" + CurrentVessel + "_S_" + DoneId + "_" + DateTime.Now.ToString("dd-MMM-yyyy") + ".zip";
                string ZipData = SaveTargetDir + "JHAttachments_" + CurrentVessel + "_S_" + DoneId + ".zip";
                dsExport.WriteXmlSchema(SchemaFile);
                dsExport.WriteXml(DataFile);
                bool FileDone = false;
                using (ZipFile zip = new ZipFile())
                {
                    zip.AddFile(SchemaFile);
                    zip.AddFile(DataFile);
                    zip.Save(ZipData);
                    FileDone = true;
                }
                //--------------------------------
                if (FileDone)
                {
                    if (ExportMode == "M")
                    {
                        ExportFileToResponse(ZipData);
                        lblMsg.Text = "Data exported successfully. File Name : " + Path.GetFileName(ZipData);
                    }
                    else
                    {
                        if (SendPacketMail(ZipData, "JOBHISTORY-ATTACHMENTS-Packet"))
                        {
                            lblMsg.Text = "Mail sent successfully.";
                        }
                    }
                    //-------------------------
                    cmd.CommandText = ("EXEC DBO.VSL_VESSEL_EXPORT_JOBHISTORY_ATTACHMENTS '" + CurrentVessel + "'," + DoneId + ",'" + Session["UserName"].ToString() + "'");
                    cmd.Parameters.Clear();
                    cmd.CommandType = CommandType.Text;
                    try
                    {
                        SqlCon.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch
                    { }
                    finally
                    {
                        if(SqlCon.State!=ConnectionState.Closed)
                            SqlCon.Close();
                    }
                    //-------------------------
                }
                else
                {
                    lblMsg.Text = "Unable to create Zip File.";
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = "Unable to export data. " + ex.Message;
            }
        }
        else
            lblMsg.Text = "Unable to export data. Job history not found.";
    }
    public void Export_DEFECT_ATTACHMENTS()
    {
        ClearTempFiles();
        string SaveTargetDir = Server.MapPath("~/TEMP/");

        string strConectionString = sqlConnectionString.Replace("Master", "eMANAGER");
        SqlConnection SqlCon = new SqlConnection(strConectionString);
        SqlCommand cmd = new SqlCommand("", SqlCon);
        SqlDataAdapter AdpExport = new SqlDataAdapter(cmd);
        ///  ------------------- CHECKS 3.  GET DATA TO BE EXPORTED FROM DATABASE
        cmd.CommandText = ("select ReportDt from VSL_DefectDetailsMaster h WHERE VESSELCODE = '" + CurrentVessel + "' AND DEFECTNO ='" + DoneId_String + "'");
        DataSet dshist = new DataSet();
        AdpExport.Fill(dshist);
        if (dshist.Tables[0].Rows.Count > 0)
        {
            DateTime doneDate = Convert.ToDateTime(dshist.Tables[0].Rows[0][0]);
            cmd.CommandText = ("SELECT * FROM VSL_Defects_Attachments a WHERE VESSELCODE = '" + CurrentVessel + "' AND DEFECTNO='" + DoneId_String + "'");
            DataSet ds = new DataSet();
            AdpExport.Fill(ds);
            ds.Tables[0].TableName = "VSL_Defects_Attachments";
            ds.Tables[0].Columns.Add("Attachment", typeof(System.Data.SqlTypes.SqlBinary));
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                string FilePath = Server.MapPath(ProjectCommon.getLinkFolder(doneDate) + dr["FileName"].ToString());
                if(File.Exists(FilePath))
                    dr["Attachment"] = File.ReadAllBytes(FilePath);
            }

            DataSet dsExport = new DataSet();
            dsExport.Tables.Add(ds.Tables[0].Copy());


            ///  ------------------- CHECKS 4.  IF SQL ERROR NO TABLE RETURNED 
            if (dsExport == null)
            {
                lblMsg.Text = "Database not exists OR some critical error.";
                return;
            }
            ///  ------------------- CHECKS 5.  IF SQL ERROR NO TABLE RETURNED 
            if (dsExport.Tables.Count <= 0)
            {
                lblMsg.Text = "Database not exists OR some critical error.";
                return;
            }

            ///  ------------------- CHECKS 7.  IF TABLES MATCHING BUT THERE IS NOTHING TO EXPORT
            bool DataExists = false;
            foreach (DataTable dt in dsExport.Tables)
            {
                if (dt.Rows.Count > 0)
                {
                    DataExists = true;
                    break;
                }
            }
            if (!DataExists)
            {
                lblMsg.Text = "No data available to send update.";
                return;
            }
            //===================================================================================
            //---------------- CHECKS DONE NOW GOING TO CREATE FILE 

            string SchemaFile = SaveTargetDir + "DefectAttachments_Schema.xml";
            string DataFile = SaveTargetDir + "DefectAttachments_Data.xml";

            try
            {
                string ZipData = SaveTargetDir + "DefectAttachments_" + CurrentVessel + "_S_" + DoneId + "_" + DateTime.Now.ToString("dd-MMM-yyyy") + ".zip";
                dsExport.WriteXmlSchema(SchemaFile);
                dsExport.WriteXml(DataFile);
                bool FileDone = false;
                using (ZipFile zip = new ZipFile())
                {
                    zip.AddFile(SchemaFile);
                    zip.AddFile(DataFile);
                    zip.Save(ZipData);
                    FileDone = true;
                }
                //--------------------------------
                if (FileDone)
                {
                    if (ExportMode == "M")
                    {
                        ExportFileToResponse(ZipData);
                        lblMsg.Text = "Data exported successfully. File Name : " + Path.GetFileName(ZipData);
                    }
                    else
                    {
                        if (SendPacketMail(ZipData, "DEFECT-ATTACHMENTS-Packet"))
                        {
                            lblMsg.Text = "Mail sent successfully.";
                        }
                    }
                }
                else
                {
                    lblMsg.Text = "Unable to create Zip File.";
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = "Unable to export data. " + ex.Message;
            }
        }
        else
            lblMsg.Text = "Unable to export data. Job history not found.";
    }
    public void Export_CRITICALEQ_SHUTDOWN_REQ()
    {
        ClearTempFiles();
        string SaveTargetDir = Server.MapPath("~/TEMP/");

        string strConectionString = sqlConnectionString.Replace("Master", "eMANAGER");
        SqlConnection SqlCon = new SqlConnection(strConectionString);
        SqlCommand cmd = new SqlCommand("", SqlCon);
        SqlDataAdapter AdpExport = new SqlDataAdapter(cmd);

        ///  ------------------- CHECKS 3.  GET DATA TO BE EXPORTED FROM DATABASE


        cmd.CommandText = ("select * from VSL_CriticalEquipShutdownRequest WHERE VESSELCODE = '" + CurrentVessel + "' AND ShutdownId =" + DoneId);
        DataSet ds = new DataSet();
        AdpExport.Fill(ds);
        ds.Tables[0].TableName = "VSL_CriticalEquipShutdownRequest";

        cmd.CommandText = ("select * from VSL_CriticalEquipShutdownRequestAttachments WHERE VESSELCODE = '" + CurrentVessel + "' AND ShutdownId =" + DoneId);
        DataSet ds1 = new DataSet();
        AdpExport.Fill(ds1);
        ds1.Tables[0].TableName = "VSL_CriticalEquipShutdownRequestAttachments";

        cmd.CommandText = ("select * from dbo.VSL_CriticalShutdownExtensions WHERE VESSELCODE = '" + CurrentVessel + "'  AND ShutdownId=" + DoneId);
        DataSet ds2 = new DataSet();
        AdpExport.Fill(ds2);
        ds2.Tables[0].TableName = "VSL_CriticalShutdownExtensions";

        DataSet dsExport = new DataSet();
        dsExport.Tables.Add(ds.Tables[0].Copy());
        dsExport.Tables.Add(ds1.Tables[0].Copy());
        dsExport.Tables.Add(ds2.Tables[0].Copy());


        ///  ------------------- CHECKS 4.  IF SQL ERROR NO TABLE RETURNED 
        if (dsExport == null)
        {
            lblMsg.Text = "Database not exists OR some critical error.";
            return;
        }
        ///  ------------------- CHECKS 5.  IF SQL ERROR NO TABLE RETURNED 
        if (dsExport.Tables.Count <= 0)
        {
            lblMsg.Text = "Database not exists OR some critical error.";
            return;
        }

        ///  ------------------- CHECKS 7.  IF TABLES MATCHING BUT THERE IS NOTHING TO EXPORT
        bool DataExists = false;
        foreach (DataTable dt in dsExport.Tables)
        {
            if (dt.Rows.Count > 0)
            {
                DataExists = true;
                break;
            }
        }
        if (!DataExists)
        {
            lblMsg.Text = "No data available to send update.";
            return;
        }
        //===================================================================================
        //---------------- CHECKS DONE NOW GOING TO CREATE FILE 

        string SchemaFile = SaveTargetDir + "CRITEQSHUTDOWNREQ_Schema.xml";
        string DataFile = SaveTargetDir + "CRITEQSHUTDOWNREQ_Data.xml";

        try
        {
            string ZipData = SaveTargetDir + "CRIT_EQ_SH_REQ_" + CurrentVessel + "_S_" + DoneId + "_" + DateTime.Now.ToString("dd-MMM-yyyy") + ".zip";
            dsExport.WriteXmlSchema(SchemaFile);
            dsExport.WriteXml(DataFile);
            bool FileDone = false;
            using (ZipFile zip = new ZipFile())
            {
                zip.AddFile(SchemaFile);
                zip.AddFile(DataFile);
                zip.Save(ZipData);
                FileDone = true;
            }
            //--------------------------------
            if (FileDone)
            {
                if (ExportMode == "M")
                {
                    ExportFileToResponse(ZipData);
                    lblMsg.Text = "Data exported successfully. File Name : " + Path.GetFileName(ZipData);
                }
                else
                {
                    if (SendPacketMail(ZipData, "PMS - Critical Equipment Shutdown Request Packet"))
                    {
                        lblMsg.Text = "Mail sent successfully.";
                    }
                }
            }
            else
            {
                lblMsg.Text = "Unable to create Zip File.";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Unable to export data. " + ex.Message;
        }
    }

    public void Export_RISKTEMPLATES_ACK()
    {
        ClearTempFiles();
        string SaveTargetDir = Server.MapPath("~/TEMP/");

        string strConectionString = sqlConnectionString.Replace("Master", "eMANAGER");
        SqlConnection SqlCon = new SqlConnection(strConectionString);
        SqlCommand cmd = new SqlCommand("", SqlCon);
        SqlDataAdapter AdpExport = new SqlDataAdapter(cmd);

        ///  ------------------- CHECKS 3.  GET DATA TO BE EXPORTED FROM DATABASE

        cmd.CommandText = ("SELECT '" + CurrentVessel + "' as VESSELCODE,TEMPLATEID,ShipRecdOn from DBO.[EV_TemplateMaster] WHERE SHIPRECDON>=DATEADD(YEAR,-1,GETDATE())");
        DataSet ds = new DataSet();
        AdpExport.Fill(ds);
        ds.Tables[0].TableName = "EV_TemplateMaster";

        DataSet dsExport = new DataSet();
        dsExport.Tables.Add(ds.Tables[0].Copy());
      

        ///  ------------------- CHECKS 4.  IF SQL ERROR NO TABLE RETURNED 
        if (dsExport == null)
        {
            lblMsg.Text = "Database not exists OR some critical error.";
            return;
        }
        ///  ------------------- CHECKS 5.  IF SQL ERROR NO TABLE RETURNED 
        if (dsExport.Tables.Count <= 0)
        {
            lblMsg.Text = "Database not exists OR some critical error.";
            return;
        }

        ///  ------------------- CHECKS 7.  IF TABLES MATCHING BUT THERE IS NOTHING TO EXPORT
        bool DataExists = false;
        foreach (DataTable dt in dsExport.Tables)
        {
            if (dt.Rows.Count > 0)
            {
                DataExists = true;
                break;
            }
        }
        if (!DataExists)
        {
            lblMsg.Text = "No data available to send update.";
            return;
        }
        //===================================================================================
        //---------------- CHECKS DONE NOW GOING TO CREATE FILE 

        string SchemaFile = SaveTargetDir + "RA_TEMP_ACK_Schema.xml";
        string DataFile = SaveTargetDir + "RA_TEMP_ACK_Data.xml";

        try
        {
            string ZipData = SaveTargetDir + "RISKTEMPLATES_ACK_" + CurrentVessel + "_S_" + DateTime.Now.ToString("dd-MMM-yyyy") + ".zip";
            dsExport.WriteXmlSchema(SchemaFile);
            dsExport.WriteXml(DataFile);
            bool FileDone = false;
            using (ZipFile zip = new ZipFile())
            {
                zip.AddFile(SchemaFile);
                zip.AddFile(DataFile);
                zip.Save(ZipData);
                FileDone = true;
            }
            //--------------------------------
            if (FileDone)
            {
                if (ExportMode == "M")
                {
                    ExportFileToResponse(ZipData);
                    lblMsg.Text = "Data exported successfully. File Name : " + Path.GetFileName(ZipData);
                }
                else
                {
                    if (SendPacketMail(ZipData, "PMS - Risk Templates Acknowledgement Packet"))
                    {
                        lblMsg.Text = "Mail sent successfully.";
                    }
                }
            }
            else
            {
                lblMsg.Text = "Unable to create Zip File.";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Unable to export data. " + ex.Message;
        }
    }

    public void Export_ER_S115_SHIPACK()
    {
        ClearTempFiles();
        string SaveTargetDir = Server.MapPath("~/TEMP/");

        string strConectionString = sqlConnectionString.Replace("Master", "eMANAGER");
        SqlConnection SqlCon = new SqlConnection(strConectionString);
        SqlCommand cmd = new SqlCommand("", SqlCon);
        SqlDataAdapter AdpExport = new SqlDataAdapter(cmd);
        string[] parts = DoneId_String.Split('_');
        string _Vslcode = parts[0];
        string _ReportId = parts[1];

        ///  ------------------- CHECKS 3.  GET DATA TO BE EXPORTED FROM DATABASE

        cmd.CommandText = ("select '" + CurrentVessel + "' AS VesselCode, VesselCode as DataVesselCode,ReportId,getdate() as ExportedOn from DBO.ER_S115_Report where VESSELCODE = '" + _Vslcode + "' AND ReportId = " + _ReportId);
        DataSet ds = new DataSet();
        AdpExport.Fill(ds);
        ds.Tables[0].TableName = "ER_S115_SHIPACK";

        DataSet dsExport = new DataSet();
        dsExport.Tables.Add(ds.Tables[0].Copy());


        ///  ------------------- CHECKS 4.  IF SQL ERROR NO TABLE RETURNED 
        if (dsExport == null)
        {
            lblMsg.Text = "Database not exists OR some critical error.";
            return;
        }
        ///  ------------------- CHECKS 5.  IF SQL ERROR NO TABLE RETURNED 
        if (dsExport.Tables.Count <= 0)
        {
            lblMsg.Text = "Database not exists OR some critical error.";
            return;
        }

        ///  ------------------- CHECKS 7.  IF TABLES MATCHING BUT THERE IS NOTHING TO EXPORT
        bool DataExists = false;
        foreach (DataTable dt in dsExport.Tables)
        {
            if (dt.Rows.Count > 0)
            {
                DataExists = true;
                break;
            }
        }
        if (!DataExists)
        {
            lblMsg.Text = "No data available to send update.";
            return;
        }
        //===================================================================================
        //---------------- CHECKS DONE NOW GOING TO CREATE FILE 

        string SchemaFile = SaveTargetDir + "ER_S115_SHIPACK_SCHEMA.xml";
        string DataFile = SaveTargetDir + "ER_S115_SHIPACK_DATA.xml";

        try
        {
            string ZipData = SaveTargetDir + "ER_S115_SHIPACK_" + CurrentVessel + "_S_" + DateTime.Now.ToString("dd-MMM-yyyy") + ".zip";
            dsExport.WriteXmlSchema(SchemaFile);
            dsExport.WriteXml(DataFile);
            bool FileDone = false;
            using (ZipFile zip = new ZipFile())
            {
                zip.AddFile(SchemaFile);
                zip.AddFile(DataFile);
                zip.Save(ZipData);
                FileDone = true;
            }
            //--------------------------------
            if (FileDone)
            {
                if (ExportMode == "M")
                {
                    ExportFileToResponse(ZipData);
                    lblMsg.Text = "Data exported successfully. File Name : " + Path.GetFileName(ZipData);
                }
                else
                {
                    if (SendPacketMail(ZipData, "PMS - ER S115 SHIP Acknowledgement"))
                    {
                        lblMsg.Text = "Mail sent successfully.";
                    }
                }
            }
            else
            {
                lblMsg.Text = "Unable to create Zip File.";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Unable to export data. " + ex.Message;
        }
    }

    public void Export_Job_Rejection()
    {
        ClearTempFiles();
        string SaveTargetDir = Server.MapPath("~/TEMP/");

        string strConectionString = sqlConnectionString.Replace("Master", "eMANAGER");
        SqlConnection SqlCon = new SqlConnection(strConectionString);
        SqlCommand cmd = new SqlCommand("", SqlCon);
        SqlDataAdapter AdpExport = new SqlDataAdapter(cmd);
        string[] parts = DoneId_String.Split('_');
        //string _Vslcode = parts[0];
        //string _ReportId = parts[1];

        ///  ------------------- CHECKS 3.  GET DATA TO BE EXPORTED FROM DATABASE

        cmd.CommandText = ("select '" + CurrentVessel + "' AS VesselCode, " + DoneId + " as CorrectionId,replace(convert(Varchar,getdate(),106),' ','-') + ' ' + right(replace(convert(Varchar,getdate(),113),' ','-'),12) as ShipRecdOn");
        DataSet ds = new DataSet();
        AdpExport.Fill(ds);
        ds.Tables[0].TableName = "VSL_JobRejection";

        DataSet dsExport = new DataSet();
        dsExport.Tables.Add(ds.Tables[0].Copy());


        ///  ------------------- CHECKS 4.  IF SQL ERROR NO TABLE RETURNED 
        if (dsExport == null)
        {
            lblMsg.Text = "Database not exists OR some critical error.";
            return;
        }
        ///  ------------------- CHECKS 5.  IF SQL ERROR NO TABLE RETURNED 
        if (dsExport.Tables.Count <= 0)
        {
            lblMsg.Text = "Database not exists OR some critical error.";
            return;
        }

        ///  ------------------- CHECKS 7.  IF TABLES MATCHING BUT THERE IS NOTHING TO EXPORT
        bool DataExists = false;
        foreach (DataTable dt in dsExport.Tables)
        {
            if (dt.Rows.Count > 0)
            {
                DataExists = true;
                break;
            }
        }
        if (!DataExists)
        {
            lblMsg.Text = "No data available to send update.";
            return;
        }
        //===================================================================================
        //---------------- CHECKS DONE NOW GOING TO CREATE FILE 

        string SchemaFile = SaveTargetDir + "VSL_JobRejection_SCHEMA.xml";
        string DataFile = SaveTargetDir + "VSL_JobRejection_DATA.xml";

        try
        {
            string ZipData = SaveTargetDir + "VSL_JobCorrection_" + CurrentVessel + "_S_" + DoneId.ToString() + ".zip";
            dsExport.WriteXmlSchema(SchemaFile);
            dsExport.WriteXml(DataFile);
            bool FileDone = false;
            using (ZipFile zip = new ZipFile())
            {
                zip.AddFile(SchemaFile);
                zip.AddFile(DataFile);
                zip.Save(ZipData);
                FileDone = true;
            }
            //--------------------------------
            if (FileDone)
            {
                if (ExportMode == "M")
                {
                    ExportFileToResponse(ZipData);
                    lblMsg.Text = "Data exported successfully. File Name : " + Path.GetFileName(ZipData);
                }
                else
                {
                    if (SendPacketMail(ZipData, "PMS - Job Rejection Acknowledgement"))
                    {
                        lblMsg.Text = "Mail sent successfully.";
                    }
                }
            }
            else
            {
                lblMsg.Text = "Unable to create Zip File.";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Unable to export data. " + ex.Message;
        }
    }
    public void Export_CREW_TRAINING_UPDATE()
    {
        ClearTempFiles();
        string SaveTargetDir = Server.MapPath("~/TEMP/");

        string strConectionString = sqlConnectionString.Replace("Master", "eMANAGER");
        SqlConnection SqlCon = new SqlConnection(strConectionString);
        SqlCommand cmd = new SqlCommand("", SqlCon);
        SqlDataAdapter AdpExport = new SqlDataAdapter(cmd);
        
        //  ------------------- CHECKS 3.  GET DATA TO BE EXPORTED FROM DATABASE

        cmd.CommandText = ("Select * from PMS_CREW_TRAININGCOMPLETED where OfficeRecdOn is null and vesselCode='"+CurrentVessel+"'");
        DataSet ds = new DataSet();
        AdpExport.Fill(ds);
        ds.Tables[0].TableName = "PMS_CREW_TRAININGCOMPLETED";

        DataSet dsExport = new DataSet();
        dsExport.Tables.Add(ds.Tables[0].Copy());


        ///  ------------------- CHECKS 4.  IF SQL ERROR NO TABLE RETURNED 
        if (dsExport == null)
        {
            lblMsg.Text = "Database not exists OR some critical error.";
            return;
        }
        ///  ------------------- CHECKS 5.  IF SQL ERROR NO TABLE RETURNED 
        if (dsExport.Tables.Count <= 0)
        {
            lblMsg.Text = "Database not exists OR some critical error.";
            return;
        }
        //===================================================================================
        //---------------- CHECKS DONE NOW GOING TO CREATE FILE 

        string SchemaFile = SaveTargetDir + "PMS_CREW_TRAININGCOMPLETED_SCHEMA.xml";
        string DataFile = SaveTargetDir + "PMS_CREW_TRAININGCOMPLETED_DATA.xml";

        try
        {
            string ZipData = SaveTargetDir + "PMS_CREW_TRAININGCOMPLETED_" + CurrentVessel + "_S_" + DateTime.Now.ToString("dd-MMM-yyyy") + ".zip";
            dsExport.WriteXmlSchema(SchemaFile);
            dsExport.WriteXml(DataFile);
            bool FileDone = false;
            using (ZipFile zip = new ZipFile())
            {
                zip.AddFile(SchemaFile);
                zip.AddFile(DataFile);
                zip.Save(ZipData);
                FileDone = true;
            }
            //--------------------------------
            if (FileDone)
            {
                if (ExportMode == "M")
                {
                    ExportFileToResponse(ZipData);
                    lblMsg.Text = "Data exported successfully. File Name : " + Path.GetFileName(ZipData);
                }
                else
                {
                    if (SendPacketMail(ZipData, "PMS - Crew Training Update"))
                    {
                        lblMsg.Text = "Mail sent successfully.";
                    }
                }
            }
            else
            {
                lblMsg.Text = "Unable to create Zip File.";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Unable to export data. " + ex.Message;
        }
    }

    public void Export_From_G118()
    {
        ClearTempFiles();
        string SaveTargetDir = Server.MapPath("~/TEMP/");

        string strConectionString = sqlConnectionString.Replace("Master", "eMANAGER");
        SqlConnection SqlCon = new SqlConnection(strConectionString);
        SqlCommand cmd = new SqlCommand("", SqlCon);
        SqlDataAdapter AdpExport = new SqlDataAdapter(cmd);

        ///  ------------------- CHECKS 3.  GET DATA TO BE EXPORTED FROM DATABASE

        
        cmd.CommandText = ("select * from [dbo].[Accident_Report] WHERE REPORTID=" + DoneId.ToString());
        DataSet ds = new DataSet();
        AdpExport.Fill(ds);
        ds.Tables[0].TableName = "Accident_Report";

        cmd.CommandText = ("select * from [dbo].[ER_Report_UpdationDetails] WHERE FORMNO='G118' AND REPORTID=" + DoneId.ToString());
        DataSet ds1 = new DataSet();
        AdpExport.Fill(ds1);
        ds1.Tables[0].TableName = "ER_Report_UpdationDetails";

        DataSet dsExport = new DataSet();
        dsExport.Tables.Add(ds.Tables[0].Copy());
        dsExport.Tables.Add(ds1.Tables[0].Copy());


        ///  ------------------- CHECKS 4.  IF SQL ERROR NO TABLE RETURNED 
        if (dsExport == null)
        {
            lblMsg.Text = "Database not exists OR some critical error.";
            return;
        }
        ///  ------------------- CHECKS 5.  IF SQL ERROR NO TABLE RETURNED 
        if (dsExport.Tables.Count <= 0)
        {
            lblMsg.Text = "Database not exists OR some critical error.";
            return;
        }

        ///  ------------------- CHECKS 7.  IF TABLES MATCHING BUT THERE IS NOTHING TO EXPORT
        bool DataExists = false;
        foreach (DataTable dt in dsExport.Tables)
        {
            if (dt.Rows.Count > 0)
            {
                DataExists = true;
                break;
            }
        }
        if (!DataExists)
        {
            lblMsg.Text = "No data available to send update.";
            return;
        }
        //===================================================================================
        //---------------- CHECKS DONE NOW GOING TO CREATE FILE 

        string SchemaFile = SaveTargetDir + "SCHEMA_G118.xml";
        string DataFile = SaveTargetDir + "DATA_G118.xml";

        try
        {
            string ZipData = SaveTargetDir + "ER_S_" + CurrentVessel + "_G118" + DoneId.ToString().PadLeft(5, '0') + ".zip";
            dsExport.WriteXmlSchema(SchemaFile);
            dsExport.WriteXml(DataFile);
            bool FileDone = false;
            using (ZipFile zip = new ZipFile())
            {
                zip.AddFile(SchemaFile);
                zip.AddFile(DataFile);
                zip.Save(ZipData);
                FileDone = true;
            }
            //--------------------------------
            if (FileDone)
            {
                if (ExportMode == "M")
                {
                    ExportFileToResponse(ZipData);
                    lblMsg.Text = "Data exported successfully. File Name : " + Path.GetFileName(ZipData);
                }
                else
                {
                    if (SendPacketMail(ZipData, "PMS - eReport Packet"))
                    {
                        lblMsg.Text = "Mail sent successfully.";
                    }
                }
            }
            else
            {
                lblMsg.Text = "Unable to create Zip File.";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Unable to export data. " + ex.Message;
        }
    }
    public void Export_From_S115()
    {
        ClearTempFiles();
        string SaveTargetDir = Server.MapPath("~/TEMP/");

        string strConectionString = sqlConnectionString.Replace("Master", "eMANAGER");
        SqlConnection SqlCon = new SqlConnection(strConectionString);
        SqlCommand cmd = new SqlCommand("", SqlCon);
        SqlDataAdapter AdpExport = new SqlDataAdapter(cmd);

        ///  ------------------- CHECKS 3.  GET DATA TO BE EXPORTED FROM DATABASE
                
        cmd.CommandText = ("select * from [dbo].[ER_S115_Report] WHERE VESSELCODE='" + CurrentVessel + "' AND REPORTID=" + DoneId.ToString());
        DataSet ds = new DataSet();
        AdpExport.Fill(ds);
        ds.Tables[0].TableName = "ER_S115_Report";

        cmd.CommandText = ("select * from [dbo].[ER_S115_InjuryToPerson] WHERE VESSELCODE='" + CurrentVessel + "' AND REPORTID=" + DoneId.ToString());
        DataSet ds1 = new DataSet();
        AdpExport.Fill(ds1);
        ds1.Tables[0].TableName = "ER_S115_InjuryToPerson";

        cmd.CommandText = ("select * from [dbo].[ER_Report_Documents_Ship] WHERE VESSELCODE='" + CurrentVessel + "' AND REPORTID=" + DoneId.ToString());
        DataSet ds2 = new DataSet();
        AdpExport.Fill(ds2);
        ds2.Tables[0].TableName = "ER_Report_Documents_Ship";

        cmd.CommandText = ("select * from [dbo].[ER_Report_UpdationDetails] WHERE FORMNO='S115' AND REPORTID=" + DoneId.ToString());
        DataSet ds3 = new DataSet();
        AdpExport.Fill(ds3);
        ds3.Tables[0].TableName = "ER_Report_UpdationDetails";


        DataSet dsExport = new DataSet();
        dsExport.Tables.Add(ds.Tables[0].Copy());
        dsExport.Tables.Add(ds1.Tables[0].Copy());
        dsExport.Tables.Add(ds2.Tables[0].Copy());
        dsExport.Tables.Add(ds3.Tables[0].Copy());


        ///  ------------------- CHECKS 4.  IF SQL ERROR NO TABLE RETURNED 
        if (dsExport == null)
        {
            lblMsg.Text = "Database not exists OR some critical error.";
            return;
        }
        ///  ------------------- CHECKS 5.  IF SQL ERROR NO TABLE RETURNED 
        if (dsExport.Tables.Count <= 0)
        {
            lblMsg.Text = "Database not exists OR some critical error.";
            return;
        }

        ///  ------------------- CHECKS 7.  IF TABLES MATCHING BUT THERE IS NOTHING TO EXPORT
        bool DataExists = false;
        foreach (DataTable dt in dsExport.Tables)
        {
            if (dt.Rows.Count > 0)
            {
                DataExists = true;
                break;
            }
        }
        if (!DataExists)
        {
            lblMsg.Text = "No data available to send update.";
            return;
        }
        //===================================================================================
        //---------------- CHECKS DONE NOW GOING TO CREATE FILE 

        string SchemaFile = SaveTargetDir + "SCHEMA_S115.xml";
        string DataFile = SaveTargetDir + "DATA_S115.xml";
        
        try
        {
            string ZipData = SaveTargetDir + "ER_S_" + CurrentVessel + "_S115" + DoneId.ToString().PadLeft(5, '0') + ".zip";
            dsExport.WriteXmlSchema(SchemaFile);
            dsExport.WriteXml(DataFile);
            bool FileDone = false;
            using (ZipFile zip = new ZipFile())
            {
                zip.AddFile(SchemaFile);
                zip.AddFile(DataFile);
                zip.Save(ZipData);
                FileDone = true;
            }
            //--------------------------------
            if (FileDone)
            {
                Common.Execute_Procedures_Select_ByQuery("UPDATE [dbo].[ER_S115_Report] SET EXPORTEDBY='" + Session["FullName"].ToString().Trim() + "',EXPORTEDON=GETDATE() WHERE VESSELCODE='" + CurrentVessel + "' AND REPORTID=" + DoneId.ToString());

                if (ExportMode == "M")
                {
                    ExportFileToResponse(ZipData);
                    lblMsg.Text = "Data exported successfully. File Name : " + Path.GetFileName(ZipData);
                }
                else
                {
                    if (SendPacketMail(ZipData, "PMS - eReport Packet"))
                    {
                        lblMsg.Text = "Mail sent successfully.";
                    }
                }
            }
            else
            {
                lblMsg.Text = "Unable to create Zip File.";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Unable to export data. " + ex.Message;
        }
    }
    public void Export_From_S133()
    {
        ClearTempFiles();
        string SaveTargetDir = Server.MapPath("~/TEMP/");

        string strConectionString = sqlConnectionString.Replace("Master", "eMANAGER");
        SqlConnection SqlCon = new SqlConnection(strConectionString);
        SqlCommand cmd = new SqlCommand("", SqlCon);
        SqlDataAdapter AdpExport = new SqlDataAdapter(cmd);

        ///  ------------------- CHECKS 3.  GET DATA TO BE EXPORTED FROM DATABASE

        cmd.CommandText = ("select * from [dbo].[ER_S133_Report] WHERE REPORTID=" + DoneId.ToString());
        DataSet ds = new DataSet();
        AdpExport.Fill(ds);
        ds.Tables[0].TableName = "ER_S133_Report";

        cmd.CommandText = ("select * from [dbo].[ER_Report_UpdationDetails] WHERE FORMNO='S133' AND REPORTID=" + DoneId.ToString());
        DataSet ds1 = new DataSet();
        AdpExport.Fill(ds1);
        ds1.Tables[0].TableName = "ER_Report_UpdationDetails";

        DataSet dsExport = new DataSet();
        dsExport.Tables.Add(ds.Tables[0].Copy());
        dsExport.Tables.Add(ds1.Tables[0].Copy());

        ///  ------------------- CHECKS 4.  IF SQL ERROR NO TABLE RETURNED 
        if (dsExport == null)
        {
            lblMsg.Text = "Database not exists OR some critical error.";
            return;
        }
        ///  ------------------- CHECKS 5.  IF SQL ERROR NO TABLE RETURNED 
        if (dsExport.Tables.Count <= 0)
        {
            lblMsg.Text = "Database not exists OR some critical error.";
            return;
        }

        ///  ------------------- CHECKS 7.  IF TABLES MATCHING BUT THERE IS NOTHING TO EXPORT
        bool DataExists = false;
        foreach (DataTable dt in dsExport.Tables)
        {
            if (dt.Rows.Count > 0)
            {
                DataExists = true;
                break;
            }
        }
        if (!DataExists)
        {
            lblMsg.Text = "No data available to send update.";
            return;
        }
        //===================================================================================
        //---------------- CHECKS DONE NOW GOING TO CREATE FILE 

        string SchemaFile = SaveTargetDir + "SCHEMA_S133.xml";
        string DataFile = SaveTargetDir + "DATA_S133.xml";
        
        try
        {
            string ZipData = SaveTargetDir + "ER_S_" + CurrentVessel + "_S133" + DoneId.ToString().PadLeft(5, '0') + ".zip";
            dsExport.WriteXmlSchema(SchemaFile);
            dsExport.WriteXml(DataFile);
            bool FileDone = false;
            using (ZipFile zip = new ZipFile())
            {
                zip.AddFile(SchemaFile);
                zip.AddFile(DataFile);
                zip.Save(ZipData);
                FileDone = true;
            }
            //--------------------------------
            if (FileDone)
            {
                Common.Execute_Procedures_Select_ByQuery("UPDATE [dbo].[ER_S133_Report] SET EXPORTEDBY='" + Session["FullName"].ToString().Trim() + "',EXPORTEDON=GETDATE() WHERE REPORTID=" + DoneId.ToString());
                
                if (ExportMode == "M")
                {
                    ExportFileToResponse(ZipData);
                    lblMsg.Text = "Data exported successfully. File Name : " + Path.GetFileName(ZipData);
                }
                else
                {
                    if (SendPacketMail(ZipData, "PMS - eReport Packet"))
                    {
                        lblMsg.Text = "Mail sent successfully.";
                    }
                }
            }
            else
            {
                lblMsg.Text = "Unable to create Zip File.";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Unable to export data. " + ex.Message;
        }
    }
    public void Export_From_G113()
    {
        ClearTempFiles();
        string SaveTargetDir = Server.MapPath("~/TEMP/");

        string strConectionString = sqlConnectionString.Replace("Master", "eMANAGER");
        SqlConnection SqlCon = new SqlConnection(strConectionString);
        SqlCommand cmd = new SqlCommand("", SqlCon);
        SqlDataAdapter AdpExport = new SqlDataAdapter(cmd);

        ///  ------------------- CHECKS 3.  GET DATA TO BE EXPORTED FROM DATABASE

        cmd.CommandText = ("select * from [dbo].[ER_G113_Report] WHERE ASSMGNTID=" + DoneId.ToString());
        DataSet ds = new DataSet();
        AdpExport.Fill(ds);
        ds.Tables[0].TableName = "ER_G113_Report";

        cmd.CommandText = ("select * from [dbo].[ER_Report_UpdationDetails] WHERE FORMNO='G113' AND REPORTID=" + DoneId.ToString());
        DataSet ds1 = new DataSet();
        AdpExport.Fill(ds1);
        ds1.Tables[0].TableName = "ER_Report_UpdationDetails";

        DataSet dsExport = new DataSet();
        dsExport.Tables.Add(ds.Tables[0].Copy());
        dsExport.Tables.Add(ds1.Tables[0].Copy());

        ///  ------------------- CHECKS 4.  IF SQL ERROR NO TABLE RETURNED 
        if (dsExport == null)
        {
            lblMsg.Text = "Database not exists OR some critical error.";
            return;
        }
        ///  ------------------- CHECKS 5.  IF SQL ERROR NO TABLE RETURNED 
        if (dsExport.Tables.Count <= 0)
        {
            lblMsg.Text = "Database not exists OR some critical error.";
            return;
        }

        ///  ------------------- CHECKS 7.  IF TABLES MATCHING BUT THERE IS NOTHING TO EXPORT
        bool DataExists = false;
        foreach (DataTable dt in dsExport.Tables)
        {
            if (dt.Rows.Count > 0)
            {
                DataExists = true;
                break;
            }
        }
        if (!DataExists)
        {
            lblMsg.Text = "No data available to send update.";
            return;
        }
        //===================================================================================
        //---------------- CHECKS DONE NOW GOING TO CREATE FILE 

        string SchemaFile = SaveTargetDir + "SCHEMA_G113.xml";
        string DataFile = SaveTargetDir + "DATA_G113.xml";

        try
        {
            string ZipData = SaveTargetDir + "ER_S_" + CurrentVessel + "_G113" + DoneId.ToString().PadLeft(5, '0') + ".zip";
            dsExport.WriteXmlSchema(SchemaFile);
            dsExport.WriteXml(DataFile);
            bool FileDone = false;
            using (ZipFile zip = new ZipFile())
            {
                zip.AddFile(SchemaFile);
                zip.AddFile(DataFile);
                zip.Save(ZipData);
                FileDone = true;
            }
            //--------------------------------
            if (FileDone)
            {
                Common.Execute_Procedures_Select_ByQuery("UPDATE [dbo].[ER_G113_Report] SET EXPORTEDBY='" + Session["FullName"].ToString().Trim() + "',EXPORTEDON=GETDATE() WHERE ASSMGNTID=" + DoneId.ToString());

                if (ExportMode == "M")
                {
                    ExportFileToResponse(ZipData);
                    lblMsg.Text = "Data exported successfully. File Name : " + Path.GetFileName(ZipData);
                }
                else
                {
                    if (SendPacketMail(ZipData, "PMS - eReport Packet"))
                    {
                        lblMsg.Text = "Mail sent successfully.";
                    }
                }
            }
            else
            {
                lblMsg.Text = "Unable to create Zip File.";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Unable to export data. " + ex.Message;
        }
    }

    public void Export_From_MRV_VOYAGE()
    {
        ClearTempFiles();
        string SaveTargetDir = Server.MapPath("~/TEMP/");

        string strConectionString = sqlConnectionString.Replace("Master", "eMANAGER");
        SqlConnection SqlCon = new SqlConnection(strConectionString);
        SqlCommand cmd = new SqlCommand("", SqlCon);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter AdpExport = new SqlDataAdapter(cmd);
        cmd.CommandText = "MRV_Get_Ship_Export_Data";
        cmd.Parameters.Add(new SqlParameter("@VesselCode", CurrentVessel));
        cmd.Parameters.Add(new SqlParameter("@VoyageId", DoneId));
        DataSet ds = new DataSet();
        AdpExport.Fill(ds);
        ///  ------------------- CHECKS 4.  IF SQL ERROR NO TABLE RETURNED 
        if (ds == null)
        {
            lblMsg.Text = "Database not exists OR some critical error.";
            return;
        }
        ///  ------------------- CHECKS 5.  IF SQL ERROR NO TABLE RETURNED 
        if (ds.Tables.Count !=10 )
        {
            lblMsg.Text = "Database not exists OR some critical error.";
            return;
        }
        ds.Tables[0].TableName = "MRV_Voyage";
        ds.Tables[1].TableName = "MRV_Voyage_Activity";

        ds.Tables[2].TableName = "MRV_Voyage_Activity_FuelReadings_Tanks";
        ds.Tables[3].TableName = "MRV_Voyage_Activity_Bunkers_Tanks";
        ds.Tables[4].TableName = "MRV_Voyage_Activity_FlowMeterReadings";

        ds.Tables[5].TableName = "MRV_VoyageActivity_Sources";
        ds.Tables[6].TableName = "MRV_Voyage_Activity_Tank_Consumption";

        ds.Tables[7].TableName = "MRV_Fuel_Tanks_Status";
        ds.Tables[8].TableName = "MRV_FlowMeterStatus";

        ds.Tables[9].TableName = "MRV_FlowMeterDefectReporting";

        //===================================================================================
        //---------------- CHECKS DONE NOW GOING TO CREATE FILE 

        string SchemaFile = SaveTargetDir + "MRV_VOYAGE_Schema.xml";
        string DataFile = SaveTargetDir + "MRV_VOYAGE_Data.xml";

        try
        {
            string ZipData = SaveTargetDir + "MRV_VOYAGE_" + CurrentVessel + "_S_" + DoneId_String + "_" + DateTime.Now.ToString("dd-MMM-yyyy") + ".zip";
            ds.WriteXmlSchema(SchemaFile);
            ds.WriteXml(DataFile);
            bool FileDone = false;
            using (ZipFile zip = new ZipFile())
            {
                zip.AddFile(SchemaFile);
                zip.AddFile(DataFile);
                zip.Save(ZipData);
                FileDone = true;
            }
            //--------------------------------
            if (FileDone)
            {
                if (ExportMode == "M")
                {
                    ExportFileToResponse(ZipData);
                    lblMsg.Text = "Data exported successfully. File Name : " + Path.GetFileName(ZipData);
                }
                else
                {
                    if (SendPacketMail(ZipData, "PMS - Position Report ( New )"))
                    {
                        lblMsg.Text = "Mail sent successfully.";
                    }
                }
            }
            else
            {
                lblMsg.Text = "Unable to create Zip File.";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Unable to export data. " + ex.Message;
        }
    }
    public void Export_From_MRV_VOYAGE1()
    {
        ClearTempFiles();
        string SaveTargetDir = Server.MapPath("~/TEMP/");

        string strConectionString = sqlConnectionString.Replace("Master", "eMANAGER");
        SqlConnection SqlCon = new SqlConnection(strConectionString);
        SqlCommand cmd = new SqlCommand("", SqlCon);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter AdpExport = new SqlDataAdapter(cmd);
        cmd.CommandText = "MRV_Get_Ship_Export_Data1";
        cmd.Parameters.Add(new SqlParameter("@VesselCode", CurrentVessel));
        cmd.Parameters.Add(new SqlParameter("@VoyageId", DoneId));
        DataSet ds = new DataSet();
        AdpExport.Fill(ds);
        ///  ------------------- CHECKS 4.  IF SQL ERROR NO TABLE RETURNED 
        if (ds == null)
        {
            lblMsg.Text = "Database not exists OR some critical error.";
            return;
        }
        ///  ------------------- CHECKS 5.  IF SQL ERROR NO TABLE RETURNED 
        if (ds.Tables.Count != 5)
        {
            lblMsg.Text = "Database not exists OR some critical error.";
            return;
        }
        ds.Tables[0].TableName = "MRV_Voyage1";
        ds.Tables[1].TableName = "MRV_VoyageDetails_AtSea1";
        ds.Tables[2].TableName = "MRV_VoyageDetails_InPort1";
        ds.Tables[3].TableName = "MRV_Voyage1_Attachments";
        ds.Tables[4].TableName = "MRV_VoaygeDetails_Tank1";
        
        //===================================================================================
        //---------------- CHECKS DONE NOW GOING TO CREATE FILE 

        string SchemaFile = SaveTargetDir + "MRV_VOYAGE_Schema.xml";
        string DataFile = SaveTargetDir + "MRV_VOYAGE_Data.xml";
        try
        {
            string ZipData = SaveTargetDir + "MRV_VOYAGE1_" + CurrentVessel + "_S_" + DoneId_String + "_" + DateTime.Now.ToString("dd-MMM-yyyy") + ".zip";
            ds.WriteXmlSchema(SchemaFile);
            ds.WriteXml(DataFile);
            bool FileDone = false;
            using (ZipFile zip = new ZipFile())
            {
                zip.AddFile(SchemaFile);
                zip.AddFile(DataFile);
                zip.Save(ZipData);
                FileDone = true;
            }
            //--------------------------------
            if (FileDone)
            {
                if (ExportMode == "M")
                {
                    ExportFileToResponse(ZipData);
                    lblMsg.Text = "Data exported successfully. File Name : " + Path.GetFileName(ZipData);
                }
                else
                {
                    if (SendPacketMail(ZipData, "MRV ( New )"))
                    {
                        lblMsg.Text = "Mail sent successfully.";
                    }
                }
                //cmd.CommandType = CommandType.Text;
                //cmd.CommandText = "UPDATE DBO.MRV_Voyage1 SET VerifiedBy='" + Session["UserName"].ToString() + "',VerifiedOn=getdate() where VESSELCODE='" + CurrentVessel + "' AND VOYAGEID=" + DoneId;
                //SqlCon.Open();
                //cmd.ExecuteNonQuery();
                //SqlCon.Close();
            }
            else
            {
                lblMsg.Text = "Unable to create Zip File.";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Unable to export data. " + ex.Message;
        }
    }
    public void Export_From_POS_ORDER()
    {
        ClearTempFiles();
        string SaveTargetDir = Server.MapPath("~/TEMP/");

        string strConectionString = sqlConnectionString.Replace("Master", "eMANAGER");
        SqlConnection SqlCon = new SqlConnection(strConectionString);
        SqlCommand cmd = new SqlCommand("", SqlCon);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter AdpExport = new SqlDataAdapter(cmd);
        cmd.CommandText = "POS_ORDER_Get_Ship_Export_Data";
        cmd.Parameters.Add(new SqlParameter("@VesselCode", CurrentVessel));
        cmd.Parameters.Add(new SqlParameter("@BidId", DoneId));
        DataSet ds = new DataSet();
        AdpExport.Fill(ds);
        ///  ------------------- CHECKS 4.  IF SQL ERROR NO TABLE RETURNED 
        if (ds == null)
        {
            lblMsg.Text = "Database not exists OR some critical error.";
            return;
        }
        ///  ------------------- CHECKS 5.  IF SQL ERROR NO TABLE RETURNED 
        if (ds.Tables.Count != 2)
        {
            lblMsg.Text = "Database not exists OR some critical error.";
            return;
        }
        ds.Tables[0].TableName = "POS_OrderReceipt";
        ds.Tables[1].TableName = "POS_OrderReceiptDetails";

        //===================================================================================
        //---------------- CHECKS DONE NOW GOING TO CREATE FILE 

        string SchemaFile = SaveTargetDir + "POS_ORDER_Schema.xml";
        string DataFile = SaveTargetDir + "POS_ORDER_Data.xml";

        try
        {
            string ZipData = SaveTargetDir + "POS_ORDER_" + CurrentVessel + "_S_" + DoneId_String + "_" + DateTime.Now.ToString("dd-MMM-yyyy") + ".zip";
            ds.WriteXmlSchema(SchemaFile);
            ds.WriteXml(DataFile);
            bool FileDone = false;
            using (ZipFile zip = new ZipFile())
            {
                zip.AddFile(SchemaFile);
                zip.AddFile(DataFile);
                zip.Save(ZipData);
                FileDone = true;
            }
            //--------------------------------
            if (FileDone)
            {
                if (ExportMode == "M")
                {
                    ExportFileToResponse(ZipData);
                    lblMsg.Text = "Data exported successfully. File Name : " + Path.GetFileName(ZipData);
                }
                else
                {
                    if (SendPacketMail(ZipData, "POS - Order Receipt Report"))
                    {
                        lblMsg.Text = "Mail sent successfully.";
                    }
                }
            }
            else
            {
                lblMsg.Text = "Unable to create Zip File.";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Unable to export data. " + ex.Message;
        }
    }
    
    public void Export_RESTHOUR_MONTH_PACKET()
    {
        ClearTempFiles();
        string SaveTargetDir = Server.MapPath("~/TEMP/");

        string strConectionString = sqlConnectionString.Replace("Master", "eMANAGER");
        SqlConnection SqlCon = new SqlConnection(strConectionString);
        SqlCommand cmd = new SqlCommand("", SqlCon);
        SqlDataAdapter AdpExport = new SqlDataAdapter(cmd);
        DateTime dtcalc = DateTime.Parse("01-" + DoneId_String);

        ///  ------------------- CHECKS 3.  GET DATA TO BE EXPORTED FROM DATABASE

        cmd.CommandText = ("SELECT VesselCode, replace(CONVERT(VARCHAR, RPeriod, 106), ' ', '-') +' ' + CONVERT(VARCHAR, RPeriod, 108) AS RPeriod, DateLine, Location, CreatedBy, " +
            " replace(CONVERT(VARCHAR, CreatedOn, 106), ' ', '-') + ' ' + CONVERT(VARCHAR, CreatedOn, 108) AS CreatedOn, ModifiedBy, " +
            " replace(CONVERT(VARCHAR, ModifiedOn, 106), ' ', '-') + ' ' + CONVERT(VARCHAR, ModifiedOn, 108) AS ModifiedOn " +
            " FROM RH_DateLineSetting WHERE VESSELCODE = '" + CurrentVessel + "' AND MONTH(RPERIOD) = " + dtcalc.Month + "  AND YEAR(RPERIOD) = " + dtcalc.Year);

        DataSet ds = new DataSet();
        AdpExport.Fill(ds);
        ds.Tables[0].TableName = "RH_DateLineSetting";

        cmd.CommandText = ("SELECT VesselCode, CrewNumber, replace(CONVERT(VARCHAR, ForDate, 106), ' ', '-') + ' ' + CONVERT(VARCHAR, ForDate, 108) AS ForDate, " +
              " DateLine, WorkLog, WorkLogCount, RestLogCount, RestIn24, RestIn7, MinTop2Sum, Single6Found, Status, Remarks, CreatedBy, " +
              " replace(CONVERT(VARCHAR, CreatedOn, 106), ' ', '-') + ' ' + CONVERT(VARCHAR, CreatedOn, 108) AS CreatedOn, " +
              " ModifiedBy, " +
              " replace(CONVERT(VARCHAR, ModifiedOn, 106), ' ', '-') +' ' + CONVERT(VARCHAR, ModifiedOn, 108) AS ModifiedOn " +
              " FROM RH_CrewMonthData WHERE VESSELCODE = '" + CurrentVessel + "' AND MONTH(ForDate) = " + dtcalc.Month + "  AND YEAR(ForDate) = " + dtcalc.Year);

        DataSet ds1 = new DataSet();
        AdpExport.Fill(ds1);
        ds1.Tables[0].TableName = "RH_CrewMonthData";

        DataSet dsExport = new DataSet();
        dsExport.Tables.Add(ds.Tables[0].Copy());
        dsExport.Tables.Add(ds1.Tables[0].Copy());


        ///  ------------------- CHECKS 4.  IF SQL ERROR NO TABLE RETURNED 
        if (dsExport == null)
        {
            lblMsg.Text = "Database not exists OR some critical error.";
            return;
        }
        ///  ------------------- CHECKS 5.  IF SQL ERROR NO TABLE RETURNED 
        if (dsExport.Tables.Count <= 0)
        {
            lblMsg.Text = "Database not exists OR some critical error.";
            return;
        }

        ///  ------------------- CHECKS 7.  IF TABLES MATCHING BUT THERE IS NOTHING TO EXPORT
        bool DataExists = false;
        foreach (DataTable dt in dsExport.Tables)
        {
            if (dt.Rows.Count > 0)
            {
                DataExists = true;
                break;
            }
        }
        if (!DataExists)
        {
            lblMsg.Text = "No data available to send update.";
            return;
        }

        //------------- ADD SOME AMSTER DATA 

        cmd.CommandText = "SELECT '" + CurrentVessel + "' AS VesselCode," + dtcalc.Month + " as Month," + dtcalc.Year + " as Year";
        DataSet ds2 = new DataSet();
        AdpExport.Fill(ds2);
        ds2.Tables[0].TableName = "RH_MASTERDATA";
        dsExport.Tables.Add(ds2.Tables[0].Copy());

        //===================================================================================
        //---------------- CHECKS DONE NOW GOING TO CREATE FILE 

        string SchemaFile = SaveTargetDir + "RH_MONTH_Schema.xml";
        string DataFile = SaveTargetDir + "RH_MONTH_Data.xml";

        try
        {
            string ZipData = SaveTargetDir + "RH_MONTH_" + CurrentVessel + "_S_" + DoneId_String + "_" + DateTime.Now.ToString("dd-MMM-yyyy") + ".zip";
            dsExport.WriteXmlSchema(SchemaFile);
            dsExport.WriteXml(DataFile);
            bool FileDone = false;
            using (ZipFile zip = new ZipFile())
            {
                zip.AddFile(SchemaFile);
                zip.AddFile(DataFile);
                zip.Save(ZipData);
                FileDone = true;
            }
            //--------------------------------
            if (FileDone)
            {
                if (ExportMode == "M")
                {
                    ExportFileToResponse(ZipData);
                    lblMsg.Text = "Data exported successfully. File Name : " + Path.GetFileName(ZipData);
                }
                else
                {
                    if (SendPacketMail(ZipData, "PMS - REST HOUR MONTH Packet"))
                    {
                        lblMsg.Text = "Mail sent successfully.";
                    }
                }
            }
            else
            {
                lblMsg.Text = "Unable to create Zip File.";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Unable to export data. " + ex.Message;
        }
    }
    public void Export_RESTHOUR_CREW_MONTH_PACKET()
    {
        ClearTempFiles();
        string SaveTargetDir = Server.MapPath("~/TEMP/");

        string strConectionString = sqlConnectionString.Replace("Master", "eMANAGER");
        SqlConnection SqlCon = new SqlConnection(strConectionString);
        SqlCommand cmd = new SqlCommand("", SqlCon);
        SqlDataAdapter AdpExport = new SqlDataAdapter(cmd);
        string[] parts = DoneId_String.Split('_');
        string crewnumber = parts[0];        
        DateTime dtcalc = DateTime.Parse("01-" + parts[1]);

        ///  ------------------- CHECKS 3.  GET DATA TO BE EXPORTED FROM DATABASE

        cmd.CommandText = ("SELECT VesselCode, replace(CONVERT(VARCHAR, RPeriod, 106), ' ', '-') +' ' + CONVERT(VARCHAR, RPeriod, 108) AS RPeriod, DateLine, Location, CreatedBy, " +
            " replace(CONVERT(VARCHAR, CreatedOn, 106), ' ', '-') + ' ' + CONVERT(VARCHAR, CreatedOn, 108) AS CreatedOn, ModifiedBy, " +
            " replace(CONVERT(VARCHAR, ModifiedOn, 106), ' ', '-') + ' ' + CONVERT(VARCHAR, ModifiedOn, 108) AS ModifiedOn " +
            " FROM DBO.RH_DateLineSetting WHERE VESSELCODE = '" + CurrentVessel + "' AND MONTH(RPERIOD) = " + dtcalc.Month + "  AND YEAR(RPERIOD) = " + dtcalc.Year);

        DataSet ds = new DataSet();
        AdpExport.Fill(ds);
        ds.Tables[0].TableName = "RH_DateLineSetting";

        cmd.CommandText = ("SELECT VesselCode, CrewNumber, replace(CONVERT(VARCHAR, ForDate, 106), ' ', '-') + ' ' + CONVERT(VARCHAR, ForDate, 108) AS ForDate, " +
              " DateLine, WorkLog, WorkLogCount, RestLogCount, RestIn24, RestIn7, MinTop2Sum, Single6Found, Status, Remarks, CreatedBy, " +
              " replace(CONVERT(VARCHAR, CreatedOn, 106), ' ', '-') + ' ' + CONVERT(VARCHAR, CreatedOn, 108) AS CreatedOn, " +
              " ModifiedBy, " +
              " replace(CONVERT(VARCHAR, ModifiedOn, 106), ' ', '-') +' ' + CONVERT(VARCHAR, ModifiedOn, 108) AS ModifiedOn " +
              " FROM DBO.RH_CrewMonthData WHERE VESSELCODE = '" + CurrentVessel + "' AND MONTH(ForDate)  = " + dtcalc.Month + "  AND YEAR(ForDate) = " + dtcalc.Year) + " AND CrewNumber='" + crewnumber + "'";

        DataSet ds1 = new DataSet();
        AdpExport.Fill(ds1);
        ds1.Tables[0].TableName = "RH_CrewMonthData";

        DataSet dsExport = new DataSet();
        dsExport.Tables.Add(ds.Tables[0].Copy());
        dsExport.Tables.Add(ds1.Tables[0].Copy());


        ///  ------------------- CHECKS 4.  IF SQL ERROR NO TABLE RETURNED 
        if (dsExport == null)
        {
            lblMsg.Text = "Database not exists OR some critical error.";
            return;
        }
        ///  ------------------- CHECKS 5.  IF SQL ERROR NO TABLE RETURNED 
        if (dsExport.Tables.Count <= 0)
        {
            lblMsg.Text = "Database not exists OR some critical error.";
            return;
        }

        //------------- ADD SOME AMSTER DATA 

        cmd.CommandText = "SELECT '" + CurrentVessel + "' AS VesselCode," + dtcalc.Month + " as Month," + dtcalc.Year + " as Year,'" + crewnumber + "' as CrewNumber ";
        DataSet ds2 = new DataSet();
        AdpExport.Fill(ds2);
        ds2.Tables[0].TableName = "RH_CREWMASTERDATA";
        dsExport.Tables.Add(ds2.Tables[0].Copy());

        //===================================================================================
        //---------------- CHECKS DONE NOW GOING TO CREATE FILE 

        string SchemaFile = SaveTargetDir + "RH_CREW_MONTH_Schema.xml";
        string DataFile = SaveTargetDir + "RH_CREW_MONTH_Data.xml";

        try
        {
            string ZipData = SaveTargetDir + "RH_CREW_MONTH_" + CurrentVessel + "_S_" + DoneId_String + "_" + DateTime.Now.ToString("dd-MMM-yyyy") + ".zip";
            dsExport.WriteXmlSchema(SchemaFile);
            dsExport.WriteXml(DataFile);
            bool FileDone = false;
            using (ZipFile zip = new ZipFile())
            {
                zip.AddFile(SchemaFile);
                zip.AddFile(DataFile);
                zip.Save(ZipData);
                FileDone = true;
            }
            //--------------------------------
            if (FileDone)
            {
                if (ExportMode == "M")
                {
                    ExportFileToResponse(ZipData);
                    lblMsg.Text = "Data exported successfully. File Name : " + Path.GetFileName(ZipData);
                }
                else
                {
                    if (SendPacketMail(ZipData, "PMS - REST HOUR MONTH Packet"))
                    {
                        lblMsg.Text = "Mail sent successfully.";
                    }
                }
            }
            else
            {
                lblMsg.Text = "Unable to create Zip File.";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Unable to export data. " + ex.Message;
        }
    }

    public void Export_From_PositionReport(string Type, string TableName)
    {
        ClearTempFiles();
        string SaveTargetDir = Server.MapPath("~/TEMP/");

        string strConectionString = sqlConnectionString.Replace("Master", "eMANAGER");
        SqlConnection SqlCon = new SqlConnection(strConectionString);
        SqlCommand cmd = new SqlCommand("", SqlCon);
        SqlDataAdapter AdpExport = new SqlDataAdapter(cmd);

        ///  ------------------- CHECKS 3.  GET DATA TO BE EXPORTED FROM DATABASE




        cmd.CommandText = ("SELECT * FROM " + TableName + " WHERE ReportsPK=" + DoneId + " AND VesselID='" + CurrentVessel + "' ");
        DataSet ds = new DataSet();
        AdpExport.Fill(ds);
        ds.Tables[0].TableName = TableName;

        DataSet dsExport = new DataSet();
        dsExport.Tables.Add(ds.Tables[0].Copy());

        ///  ------------------- CHECKS 4.  IF SQL ERROR NO TABLE RETURNED 
        if (dsExport == null)
        {
            lblMsg.Text = "Database not exists OR some critical error.";
            return;
        }
        ///  ------------------- CHECKS 5.  IF SQL ERROR NO TABLE RETURNED 
        if (dsExport.Tables.Count <= 0)
        {
            lblMsg.Text = "Database not exists OR some critical error.";
            return;
        }

        ///  ------------------- CHECKS 7.  IF TABLES MATCHING BUT THERE IS NOTHING TO EXPORT
        bool DataExists = false;
        foreach (DataTable dt in dsExport.Tables)
        {
            if (dt.Rows.Count > 0)
            {
                DataExists = true;
                break;
            }
        }
        if (!DataExists)
        {
            lblMsg.Text = "No data available to send update.";
            return;
        }
        //===================================================================================
        //---------------- CHECKS DONE NOW GOING TO CREATE FILE 

        string SchemaFile = SaveTargetDir + "PREP_Schema.xml";
        string DataFile = SaveTargetDir + "PREP_Data.xml";

        try
        {
            string ZipData = SaveTargetDir + "PREP_" + CurrentVessel + "_" + Type + "_" + DoneId.ToString() + "_" + DateTime.Now.ToString("dd-MMM-yyyy").Replace(":", "") + ".zip";
            dsExport.WriteXmlSchema(SchemaFile);
            dsExport.WriteXml(DataFile);
            bool FileDone = false;
            using (ZipFile zip = new ZipFile())
            {
                zip.AddFile(SchemaFile);
                zip.AddFile(DataFile);
                zip.Save(ZipData);
                FileDone = true;
            }
            //--------------------------------
            if (FileDone)
            {
                if (ExportMode == "M")
                {
                    ExportFileToResponse(ZipData);
                    lblMsg.Text = "Data exported successfully. File Name : " + Path.GetFileName(ZipData);
                }
                else
                {
                    if (SendPacketMail(ZipData, "PMS - Position Report Packet"))
                    {
                        lblMsg.Text = "Mail sent successfully.";
                    }
                }
            }
            else
            {
                lblMsg.Text = "Unable to create Zip File.";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Unable to export data. " + ex.Message;
        }
    }
    public void Export_From_PositionReportNew()
    {
        ClearTempFiles();
        string SaveTargetDir = Server.MapPath("~/TEMP/");

        string strConectionString = sqlConnectionString.Replace("Master", "eMANAGER");
        SqlConnection SqlCon = new SqlConnection(strConectionString);
        SqlCommand cmd = new SqlCommand("", SqlCon);
        SqlDataAdapter AdpExport = new SqlDataAdapter(cmd);

        ///  ------------------- CHECKS 3.  GET DATA TO BE EXPORTED FROM DATABASE

        cmd.CommandText = ("select * from VSL_VPRNoonReport_New WHERE VesselID = '" + CurrentVessel + "' AND ReportsPK =" + DoneId);
        DataSet ds = new DataSet();
        AdpExport.Fill(ds);
        ds.Tables[0].TableName = "VSL_VPRNoonReport_New";

        cmd.CommandText = ("select * from VSL_VPRNoonReport_New_OpBal where VesselCode='" + CurrentVessel + "'");
        DataSet ds1 = new DataSet();
        AdpExport.Fill(ds1);
        ds1.Tables[0].TableName = "VSL_VPRNoonReport_New_OpBal";

        DataSet dsExport = new DataSet();
        dsExport.Tables.Add(ds.Tables[0].Copy());
        dsExport.Tables.Add(ds1.Tables[0].Copy());
     
        ///  ------------------- CHECKS 4.  IF SQL ERROR NO TABLE RETURNED 
        if (dsExport == null)
        {
            lblMsg.Text = "Database not exists OR some critical error.";
            return;
        }
        ///  ------------------- CHECKS 5.  IF SQL ERROR NO TABLE RETURNED 
        if (dsExport.Tables.Count <= 0)
        {
            lblMsg.Text = "Database not exists OR some critical error.";
            return;
        }

        ///  ------------------- CHECKS 7.  IF TABLES MATCHING BUT THERE IS NOTHING TO EXPORT
        bool DataExists = false;
        foreach (DataTable dt in dsExport.Tables)
        {
            if (dt.Rows.Count > 0)
            {
                DataExists = true;
                break;
            }
        }
        if (!DataExists)
        {
            lblMsg.Text = "No data available to send update.";
            return;
        }
        //===================================================================================
        //---------------- CHECKS DONE NOW GOING TO CREATE FILE 

        string SchemaFile = SaveTargetDir + "POS_REPORT_NEW_Schema.xml";
        string DataFile = SaveTargetDir + "POS_REPORT_NEW_Data.xml";

        try
        {
            string ZipData = SaveTargetDir + "POS_REPORT_NEW_" + CurrentVessel + "_S_" + DoneId + "_" + DateTime.Now.ToString("dd-MMM-yyyy") + ".zip";
            dsExport.WriteXmlSchema(SchemaFile);
            dsExport.WriteXml(DataFile);
            bool FileDone = false;
            using (ZipFile zip = new ZipFile())
            {
                zip.AddFile(SchemaFile);
                zip.AddFile(DataFile);
                zip.Save(ZipData);
                FileDone = true;
            }
            //--------------------------------
            if (FileDone)
            {
                if (ExportMode == "M")
                {
                    ExportFileToResponse(ZipData);
                    lblMsg.Text = "Data exported successfully. File Name : " + Path.GetFileName(ZipData);
                }
                else
                {
                    if (SendPacketMail(ZipData, "PMS - Position Report ( New )"))
                    {
                        lblMsg.Text = "Mail sent successfully.";
                    }
                }
            }
            else
            {
                lblMsg.Text = "Unable to create Zip File.";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Unable to export data. " + ex.Message;
        }
    }    

    //public void Export_From_PositionReport(string Type, string TableName)
    //{
    //    ClearTempFiles();
    //    string SaveTargetDir = Server.MapPath("~/TEMP/");

    //    string strConectionString = sqlConnectionString.Replace("Master", "eMANAGER");
    //    SqlConnection SqlCon = new SqlConnection(strConectionString);
    //    SqlCommand cmd = new SqlCommand("", SqlCon);
    //    SqlDataAdapter AdpExport = new SqlDataAdapter(cmd);

    //    ///  ------------------- CHECKS 3.  GET DATA TO BE EXPORTED FROM DATABASE



    //    TableName = "VSL_VPRNoonReport";

    //    cmd.CommandText = ("SELECT * FROM " + TableName + " WHERE ReportsPK=" + DoneId + " AND VesselID='" + CurrentVessel + "' ");
    //    DataSet ds = new DataSet();
    //    AdpExport.Fill(ds);
    //    ds.Tables[0].TableName = TableName;

    //    DataSet dsExport = new DataSet();
    //    dsExport.Tables.Add(ds.Tables[0].Copy());        

    //    ///  ------------------- CHECKS 4.  IF SQL ERROR NO TABLE RETURNED 
    //    if (dsExport == null)
    //    {
    //        lblMsg.Text = "Database not exists OR some critical error.";
    //        return;
    //    }
    //    ///  ------------------- CHECKS 5.  IF SQL ERROR NO TABLE RETURNED 
    //    if (dsExport.Tables.Count <= 0)
    //    {
    //        lblMsg.Text = "Database not exists OR some critical error.";
    //        return;
    //    }

    //    ///  ------------------- CHECKS 7.  IF TABLES MATCHING BUT THERE IS NOTHING TO EXPORT
    //    bool DataExists = false;
    //    foreach (DataTable dt in dsExport.Tables)
    //    {
    //        if (dt.Rows.Count > 0)
    //        {
    //            DataExists = true;
    //            break;
    //        }
    //    }
    //    if (!DataExists)
    //    {
    //        lblMsg.Text = "No data available to send update.";
    //        return;
    //    }
    //    //===================================================================================
    //    //---------------- CHECKS DONE NOW GOING TO CREATE FILE 

    //    string SchemaFile = SaveTargetDir + "PREP_Schema.xml";
    //    string DataFile = SaveTargetDir + "PREP_Data.xml";

    //    try
    //    {
    //        string ZipData = SaveTargetDir + "PREP_" + CurrentVessel + "_" + Type + "_" + DoneId.ToString() + "_" + DateTime.Now.ToString("dd-MMM-yyyy").Replace(":", "") + ".zip";
    //        dsExport.WriteXmlSchema(SchemaFile);
    //        dsExport.WriteXml(DataFile);
    //        bool FileDone = false;
    //        using (ZipFile zip = new ZipFile())
    //        {
    //            zip.AddFile(SchemaFile);
    //            zip.AddFile(DataFile);
    //            zip.Save(ZipData);
    //            FileDone = true;
    //        }
    //        //--------------------------------
    //        if (FileDone)
    //        {
    //            if (ExportMode == "M")
    //            {
    //                ExportFileToResponse(ZipData);
    //                lblMsg.Text = "Data exported successfully. File Name : " + Path.GetFileName(ZipData);
    //            }
    //            else
    //            {
    //                if (SendPacketMail(ZipData, "PMS - Position Report Packet"))
    //                {
    //                    lblMsg.Text = "Mail sent successfully.";
    //                }
    //            }
    //        }
    //        else
    //        {
    //            lblMsg.Text = "Unable to create Zip File.";
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = "Unable to export data. " + ex.Message;
    //    }
    //}
    public void Export_From_RCA()
    {
        ClearTempFiles();
        string SaveTargetDir = Server.MapPath("~/TEMP/");

        string strConectionString = sqlConnectionString.Replace("Master", "eMANAGER");
        SqlConnection SqlCon = new SqlConnection(strConectionString);
        SqlCommand cmd = new SqlCommand("", SqlCon);
        SqlDataAdapter AdpExport = new SqlDataAdapter(cmd);

        ///  ------------------- CHECKS 3.  GET DATA TO BE EXPORTED FROM DATABASE


        cmd.CommandText = ("SELECT * FROM [dbo].[EV_VSL_RiskMaster] WHERE VESSELCODE = '" + CurrentVessel + "' AND RISKID=" + DoneId);
        DataSet ds = new DataSet();
        AdpExport.Fill(ds);
        ds.Tables[0].TableName = "EV_VSL_RiskMaster";

        cmd.CommandText = ("SELECT * FROM DBO.EV_VSL_Risk_Tasks WHERE RiskId=" + DoneId + " AND VesselCode='" + CurrentVessel + "'");
        DataSet ds1 = new DataSet();
        AdpExport.Fill(ds1);
        ds1.Tables[0].TableName = "EV_VSL_Risk_Tasks";


        cmd.CommandText = ("SELECT * FROM [dbo].[EV_VSL_Risk_Details] WHERE RISKID=" + DoneId + "  AND VESSELCODE= '" + CurrentVessel + "'");
        DataSet ds2 = new DataSet();
        AdpExport.Fill(ds2);
        ds2.Tables[0].TableName = "EV_VSL_Risk_Details";

        DataSet dsExport = new DataSet();
        dsExport.Tables.Add(ds.Tables[0].Copy());
        dsExport.Tables.Add(ds1.Tables[0].Copy());
        dsExport.Tables.Add(ds2.Tables[0].Copy());


        ///  ------------------- CHECKS 4.  IF SQL ERROR NO TABLE RETURNED 
        if (dsExport == null)
        {
            lblMsg.Text = "Database not exists OR some critical error.";
            return;
        }
        ///  ------------------- CHECKS 5.  IF SQL ERROR NO TABLE RETURNED 
        if (dsExport.Tables.Count <= 0)
        {
            lblMsg.Text = "Database not exists OR some critical error.";
            return;
        }

        ///  ------------------- CHECKS 7.  IF TABLES MATCHING BUT THERE IS NOTHING TO EXPORT
        bool DataExists = false;
        foreach (DataTable dt in dsExport.Tables)
        {
            if (dt.Rows.Count > 0)
            {
                DataExists = true;
                break;
            }
        }
        if (!DataExists)
        {
            lblMsg.Text = "No data available to send update.";
            return;
        }
        //===================================================================================
        //---------------- CHECKS DONE NOW GOING TO CREATE FILE 

        string SchemaFile = SaveTargetDir + "RCASchema.xml";
        string DataFile = SaveTargetDir + "RCAData.xml";


        try
        {
            string ZipData = SaveTargetDir + "RCA_S_" + CurrentVessel + "_" + DoneId + ".zip";
            dsExport.WriteXmlSchema(SchemaFile);
            dsExport.WriteXml(DataFile);
            bool FileDone = false;
            using (ZipFile zip = new ZipFile())
            {
                zip.AddFile(SchemaFile);
                zip.AddFile(DataFile);
                zip.Save(ZipData);
                FileDone = true;
            }
            //--------------------------------
            if (FileDone)
            {
                if (ExportMode == "M")
                {
                    ExportFileToResponse(ZipData);
                    lblMsg.Text = "Data exported successfully. File Name : " + Path.GetFileName(ZipData);
                }
                else
                {
                    if (SendPacketMail(ZipData, "PMS - SCM Packet"))
                    {
                        lblMsg.Text = "Mail sent successfully.";
                    }
                }
            }
            else
            {
                lblMsg.Text = "Unable to create Zip File.";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Unable to export data. " + ex.Message;
        }
    }

    protected void btnPMS_Click(object sender, EventArgs e)
    {
        Export_From_SHIP();
    }
    private void Export_From_SHIP()
    {
        ClearTempFiles();
        string SaveTargetDir = Server.MapPath("~/TEMP/");

        string strConectionString = sqlConnectionString.Replace("Master", "eMANAGER");
        SqlConnection SqlCon = new SqlConnection(strConectionString);
        SqlCommand cmd = new SqlCommand("", SqlCon);
        SqlDataAdapter AdpExport = new SqlDataAdapter(cmd);
        DataSet ds_Ship = new DataSet();
        
        ///  -------------------------------------------------------------------------
        String SchemaFileName = SaveTargetDir + CurrentVessel + "_SchemaFile" + ".xml";
        String DataFileName = SaveTargetDir + CurrentVessel + "_" + "DataFile" + ".xml";
        ///  ------------------- CHECKS 3.  GET DATA TO BE EXPORTED FROM DATABASE
        cmd.CommandText = "sp_IE_CreatePacket 'S','" + CurrentVessel + "'";
        DataSet dsExport = new DataSet();
        AdpExport.Fill(dsExport);
        ///  ------------------- CHECKS 4.  IF SQL ERROR NO TABLE RETURNED 
        if (dsExport == null)
        {
            lblMsg.Text = "Database not exists OR some critical error.";
            return;
        }
        ///  ------------------- CHECKS 5.  IF SQL ERROR NO TABLE RETURNED 
        if (dsExport.Tables.Count <= 0)
        {
            lblMsg.Text = "Database not exists OR some critical error.";
            return;
        }
        ///  ------------------- CHECKS 6.  IF SQL PARTIAL ERROR AND NO OF TABLES ARE NOTO MATCHING
        try
        {
            string[] ShipTables = { "VSL_PlanMaster", "VSL_VesselRunningHourMaster", "VSL_VesselJobUpdateHistory", "VSL_DefectSpareDetails", "Vsl_DefectDetailsMaster", "VSL_VesselComponentJobMaster_Updates", "VSL_VesselJobUpdateHistoryAttachments", "VSL_VesselJobUpdateHistorySpareDetails", "VSL_StockInventory", "VSL_UnPlannedJobs", "VSL_UnPlannedJobs_Attachments", "VSL_UnPlannedJobs_SpareDetails", "VSL_CriticalEquipShutdownRequest", "VSL_CriticalShutdownExtensions", "VSL_Defects_Attachments", "VSL_VesselSpareMaster", "PACKETDETAILS" };
            for (int i = 0; i <= ShipTables.Length - 1; i++)
            {
                dsExport.Tables[i].TableName = ShipTables[i];
            }
            
        }
        catch (Exception ex)
        {
            lblMsg.Text="Database not exists OR some critical error. " + ex.Message;
            return;
        }
        ///  ------------------- CHECKS 7.  IF TABLES MATCHING BUT THERE IS NOTHING TO EXPORT
        bool DataExists = false;
        foreach (DataTable dt in dsExport.Tables)
        {
            if (dt.Rows.Count > 0)
            {
                DataExists = true;
                break;
            }
        }
        if (!DataExists)
        {
            lblMsg.Text ="No data available to send update.";
            return;
        }
        //===================================================================================
        //---------------- CHECKS DONE NOW GOING TO CREATE FILE 
        try
        {
            string zipFileName = dsExport.Tables["PACKETDETAILS"].Rows[0]["PacketName"].ToString() + ".zip";
            dsExport.WriteXmlSchema(SchemaFileName);
            dsExport.WriteXml(DataFileName);
            using (ZipFile zip = new ZipFile())
            {
                zip.AddFile(SchemaFileName);
                zip.AddFile(DataFileName);
                zip.Save(SaveTargetDir + zipFileName);
                if (ExportMode == "M")
                {
                    ExportFileToResponse(SaveTargetDir + zipFileName);
                    lblMsg.Text = "Data exported successfully. File Name : " + zipFileName;
                }
                else
                {
                    if (SendPacketMail(SaveTargetDir + zipFileName, "PMS packet"))
                    {
                        lblMsg.Text = "Mail sent successfully.";
                    }

                }

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text="Unable to export data. "+ ex.Message;
        }
    }
    public static void CopyStream(Stream input, Stream output)
    {
        byte[] buffer = new byte[32768];
        int read;
        while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
        {
            output.Write(buffer, 0, read);
        }
    }
    public bool SendPacketMail(string FilePath, string Type)
    {
        string FileName = Path.GetFileName(FilePath);
        //MemoryStream ms = new MemoryStream();
        //using(FileStream fs=new FileStream(FilePath,FileMode.Open))
        //{
        //    CopyStream(fs,ms);
        //} 

        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM MailSettings");
        if (dt.Rows.Count > 0)
        {
            string toaddress, senderaddress, senderusername, senderpass, mailclient;
            int port;
            bool encryption;
            toaddress = dt.Rows[0]["Toaddress"].ToString();
            senderaddress = dt.Rows[0]["SenderAddress"].ToString();
            senderusername = dt.Rows[0]["SenderUserName"].ToString();
            senderpass = dt.Rows[0]["senderPass"].ToString();
            mailclient = dt.Rows[0]["MailClient"].ToString();
            port = Common.CastAsInt32(dt.Rows[0]["Port"]);
            encryption = dt.Rows[0]["Mail_Encryption"].ToString() == "SSL";
            string[] CCAddress = { "emanager@energiossolutions.com" };
            string[] BCCAddress = { };
            string message = "Dear Receiver, <br><br> Please receive attached <b>" + Type + "</b> from ship email system.<br><br>Thanks <br />" + senderaddress;
            string Error = "";
            bool result = SendMailWithAttachment(senderaddress, toaddress, CCAddress, BCCAddress, "Ship Communication : " + Type, message, FilePath, FileName, senderusername, senderpass, mailclient, port, encryption, out Error);
            if (result)
            {
                lblMsg.Text = "Mail sent successfully.";
                
            }
            else
            {
                lblMsg.Text = "Unable to send mail. Error : " + Error;
            }

            return result;
        }
        else
        {
            lblMsg.Text = "Settings not found. Please save settings before send mail.";
            return false;
        }
    }
    public bool SendMailWithAttachment(string FromMail, string toAddress, string[] CCAddresses, string[] BCCAddresses, string Subject, string BodyText, string FilePath, string AttachmentFileName, string senderusername, string senderpass, string HostName, int Port, bool SSL, out string Error)
    {
        Error = "";
        try
        {
            MailMessage objMessage = new MailMessage();
            SmtpClient objSmtpClient = new SmtpClient(HostName, Port);
            MailAddress objfromAddress = new MailAddress(FromMail);
            StringBuilder msgFormat = new StringBuilder();
            objMessage.From = objfromAddress;
            objSmtpClient.Credentials = new NetworkCredential(senderusername, senderpass);
            try
            {
                objMessage.To.Add(toAddress);
                objMessage.Body = BodyText;
                objMessage.Subject = Subject;
                objMessage.IsBodyHtml = true;

                foreach (string CCadrs in CCAddresses)
                {
                    objMessage.CC.Add(CCadrs);
                }

                foreach (string adrs in BCCAddresses)
                {
                    objMessage.Bcc.Add(adrs);
                }
                if (AttachmentFileName != "")
                {
                    Attachment attachFile = new Attachment(FilePath);
                    objMessage.Attachments.Add(attachFile);
                }

                objMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                objSmtpClient.Send(objMessage);
                return true;
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                return false;
            }
        }
        catch (Exception ex)
        {
            Error = ex.Message;
            return false;
        }
    }
    protected void btnSMS_Click(object sender, EventArgs e)
    {
        Generate_SMS_Acknowledge();
    }
    public void Generate_SMS_Acknowledge()
    {
        ClearTempFiles();
        int PacketID = 0;
        string strConectionString = sqlConnectionString.Replace("Master", "eMANAGER");
        DataSet Ds = new DataSet();
        SqlConnection Con = new SqlConnection(strConectionString);
        SqlCommand cmd = new SqlCommand("", Con);
        SqlDataAdapter adp = new SqlDataAdapter();
        adp.SelectCommand = cmd;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "Ship_SMS_Get_CommunicationData";
        adp.Fill(Ds, "MD");

        if (Ds != null)
        {
            Ds.Tables[0].TableName = "SMS_APP_ManualDetails";
            Ds.Tables[1].TableName = "SMS_FORMS";
            Ds.Tables[2].TableName = "SMS_PacketID";
            Ds.Tables[3].TableName = "SMS_SHIPComments";


            if (Ds.Tables[0].Rows.Count <= 0 && Ds.Tables[1].Rows.Count <= 0 && Ds.Tables[3].Rows.Count <= 0)
            {
                lblMsg.Text="There is no SMS or Forms acknowledgement or Comments.";
                return;
            }

            PacketID = Convert.ToInt32(Ds.Tables["SMS_PacketID"].Rows[0]["PacketID"]);

            string BaseDir = Server.MapPath("~/TEMP/");
            string schemafile = BaseDir + "SMS_Acknowledge_Schema.xml";
            string datafile=BaseDir + "SMS_Acknowledge.xml";

            Ds.WriteXml(datafile);
            Ds.WriteXmlSchema(schemafile);

            string AckZipFileName = CurrentVessel + "_SMS_ACK" + PacketID.ToString() + ".zip";
            string AckZipFilePath = BaseDir + CurrentVessel + "_SMS_ACK" + PacketID.ToString() + ".zip";

            using (ZipFile zip = new ZipFile())
            {
                zip.AddFile(datafile);
                zip.AddFile(schemafile);
                zip.Save(AckZipFilePath);
            }
            
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SHIP_SMS_SENTACKNOWLEDGEMENT";

                SqlParameter param1 = new SqlParameter("VesselCode", SqlDbType.VarChar);
                param1.Value = CurrentVessel;

                SqlParameter param2 = new SqlParameter("PACKETID", SqlDbType.VarChar);
                param2.Value = PacketID;

                SqlParameter param3 = new SqlParameter("ACKFILE", SqlDbType.VarChar);
                param3.Value = AckZipFileName;

                cmd.Parameters.Add(param1);
                cmd.Parameters.Add(param2);
                cmd.Parameters.Add(param3);

                Con.Open();
                cmd.ExecuteNonQuery();
                Con.Close();

                if (ExportMode == "M")
                {
                    ExportFileToResponse(AckZipFilePath);
                    lblMsg.Text = "SMS Acknowledgement packet created successfully. File : " + AckZipFileName;
                }
                else
                {
                    if (SendPacketMail(AckZipFilePath, "PMS - SMS Packet"))
                    {
                        lblMsg.Text = "Mail sent successfully.";
                    }

                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = "SMS Acknowledge could not be sent." + ex.Message;
            }

        }
    }
    protected void btnMNP_Click(object sender, EventArgs e)
    {
        Export_From_MNP();
    }
    private void Export_From_MNP()
    {
        int version = 1;
        ClearTempFiles();
        string SaveTargetDir = Server.MapPath("~/TEMP/");

        string strConectionString = sqlConnectionString.Replace("Master", "eMANAGER");
        SqlConnection SqlCon = new SqlConnection(strConectionString);
        SqlCommand cmd = new SqlCommand("", SqlCon);
        SqlDataAdapter AdpExport = new SqlDataAdapter(cmd);
        DataSet ds_Ship = new DataSet();

        ///  -------------------------------------------------------------------------
        cmd.CommandText = "sp_IE_V1_MP_getDataForExport 'S','" + CurrentVessel + "','D'";
        DataSet dsExportContent = new DataSet();
        AdpExport.Fill(dsExportContent);
        bool DataFound = false;
        foreach (DataTable dt_1 in dsExportContent.Tables)
        {
            if (dt_1.Rows.Count > 0)
            {
                DataFound = true;
                break;
            }
        }
        ///  -------------------------------------------------------------------------
        if (!(DataFound))
        {
            lblMsg.Text = "Noting for export in Menu Planner Packet.";
            return;
        }
        ///  -------------------------------------------------------------------------
        String SchemaFileName = SaveTargetDir + CurrentVessel + "_SchemaFile" + ".xml";
        String DataFileName = SaveTargetDir + CurrentVessel + "_" + "DataFile" + ".xml";
        ///  ------------------- CHECKS 3.  GET DATA TO BE EXPORTED FROM DATABASE
        cmd.CommandText = "sp_IE_V1_MP_CreatePacket 'S','" + CurrentVessel + "'," + version.ToString();
        DataSet dsExport = new DataSet();
        AdpExport.Fill(dsExport);
        ///  ------------------- CHECKS 4.  IF SQL ERROR NO TABLE RETURNED 
        if (dsExport == null)
        {
            lblMsg.Text = "Database not exists OR some critical error.";
            return;
        }
        ///  ------------------- CHECKS 5.  IF SQL ERROR NO TABLE RETURNED 
        if (dsExport.Tables.Count <= 0)
        {
            lblMsg.Text = "Database not exists OR some critical error.";
            return;
        }
        ///  ------------------- CHECKS 6.  IF SQL PARTIAL ERROR AND NO OF TABLES ARE NOT MATCHING
        try
        {
            if (version == 1)
            {
                string[] ShipTablesMNP = { "PACKETHEADER", "MP_VSL_InventoryMaster", "MP_VSL_InventoryDetails", "MP_VSL_SupplyMaster", "MP_VSL_SupplyDetails", "MP_VSL_ShipStaffMaster", "MP_VSL_ShipStaffDetails", "MP_VSL_AccountMaster", "MP_VSL_AccountDetails" };
                for (int i = 0; i <= ShipTablesMNP.Length - 1; i++)
                {
                    dsExport.Tables[i].TableName = ShipTablesMNP[i];
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Database not exists OR some critical error."+ ex.Message;
            return;
        }
        ///  ------------------- CHECKS 7.  IF TABLES MATCHING BUT THERE IS NOTHING TO EXPORT
        bool DataExists = false;
        foreach (DataTable dt in dsExport.Tables)
        {
            if (dt.Rows.Count > 0)
            {
                DataExists = true;
                break;
            }
        }
        if (!DataExists)
        {
            lblMsg.Text = "No data available to send update.";
            return;
        }

        //===================================================================================
        //---------------- CHECKS DONE NOW GOING TO CREATE FILE 
        try
        {
            string zipFileName = dsExport.Tables["PACKETHEADER"].Rows[0]["PacketName"].ToString() + ".zip";
            string zipFilePath = SaveTargetDir + dsExport.Tables["PACKETHEADER"].Rows[0]["PacketName"].ToString() + ".zip";

            if (File.Exists(SaveTargetDir + zipFileName))
            {
                File.Delete(SaveTargetDir + zipFileName);
            }
            dsExport.WriteXmlSchema(SchemaFileName);
            dsExport.WriteXml(DataFileName);
            using (ZipFile zip = new ZipFile())
            {
                zip.AddFile(SchemaFileName);
                zip.AddFile(DataFileName);

                zip.Save(zipFilePath);                

                if (ExportMode == "M")
                {
                    ExportFileToResponse(zipFilePath);
                    lblMsg.Text = "Data exported successfully. File Name : " + zipFileName;
                }
                else
                {
                    if (SendPacketMail(zipFilePath, "PMS - MNP Packet"))
                    {
                        lblMsg.Text = "Mail sent successfully.";
                    }

                }
            }
            
        }
        catch (Exception ex)
        {
            lblMsg.Text="Unable to export data."+ ex.Message;
        }
    }
    protected void btnExportRH_Click(object sender, EventArgs e)
    {
        dv_RestHr.Visible = true;
    } 
    protected void btnExportRestHr_Click(object sender, EventArgs e)
    {
        if (ddlMonth.SelectedIndex == 0)
        {
            lblMsgRH.Text = "Please select month."; ddlMonth.Focus(); return;
        }
        if (ddlYear.SelectedIndex == 0)
        {
            lblMsgRH.Text = "Please select year."; ddlYear.Focus(); return;
        }

        ClearTempFiles();
        string SaveTargetDir = Server.MapPath("~/TEMP/");

        string strConectionString = sqlConnectionString.Replace("Master", "eShip");
        SqlConnection SqlCon = new SqlConnection(strConectionString);
        SqlCommand cmd = new SqlCommand("", SqlCon);
        SqlDataAdapter AdpExport = new SqlDataAdapter(cmd);        

        ///  -------------------------------------------------------------------------
        cmd.CommandText = "GET_DATA_PACKET '" + CurrentVessel + "'," + ddlMonth.SelectedValue + "," + ddlYear.SelectedValue;
        DataSet dsExport = new DataSet();
        AdpExport.Fill(dsExport);
        bool DataFound = false;

        dsExport.Tables[0].TableName = "WCPacketHeader";
        dsExport.Tables[1].TableName = "CP_VesselCrewSignOnOff";
        dsExport.Tables[2].TableName = "CP_CrewDailyLocation";
        dsExport.Tables[3].TableName = "CP_CrewDailyWorkRestHours";
        dsExport.Tables[4].TableName = "CP_CrewHoursLog";
        dsExport.Tables[5].TableName = "CP_NonConformance";
        dsExport.Tables[6].TableName = "CP_NonConformanceReason";

        foreach (DataTable dt_1 in dsExport.Tables)
        {
            if (dt_1.Rows.Count > 0)
            {
                DataFound = true;
                break;
            }
        }
        ///  -------------------------------------------------------------------------
        if (!(DataFound))
        {
            lblMsgRH.Text = "Noting for export in Rest Hour Packet.";
            return;
        }
        ///  -------------------------------------------------------------------------    

        dv_RestHr.Visible = false;

        String SchemaFileName = SaveTargetDir + CurrentVessel + "_RestHours_" + "SchemaFile" + ".xml";
        String DataFileName = SaveTargetDir + CurrentVessel + "_RestHours_" + "DataFile" + ".xml";

        //===================================================================================
        //---------------- CHECKS DONE NOW GOING TO CREATE FILE 
        try
        {
            string ZipData = SaveTargetDir + "WC_" + CurrentVessel + "_" + ddlMonth.SelectedItem.Text + "_" + ddlYear.SelectedValue + ".zip";
            dsExport.WriteXmlSchema(SchemaFileName);
            dsExport.WriteXml(DataFileName);
            bool FileDone = false;
            using (ZipFile zip = new ZipFile())
            {
                zip.AddFile(SchemaFileName);
                zip.AddFile(DataFileName);
                zip.Save(ZipData);
                FileDone = true;
            }
            //--------------------------------
            if (FileDone)
            {
                if (ExportMode == "M")
                {
                    ExportFileToResponse(ZipData);
                    lblMsg.Text = "Data exported successfully. File Name : " + Path.GetFileName(ZipData);
                }
                else
                {
                    if (SendPacketMail(ZipData, "Rest Hour Packet"))
                    {
                        lblMsg.Text = "Mail sent successfully.";
                    }
                }
                btnClose_Click(sender, e);
            }
            else
            {
                lblMsg.Text = "Unable to create Zip File.";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Unable to export data. " + ex.Message;
        }
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        ddlMonth.SelectedIndex = 0;
        ddlYear.SelectedIndex = 0;
        dv_RestHr.Visible = false;
    }
    protected void btnRestHr_Click(object sender, EventArgs e)
    {
        //ClearTempFiles();
        //string SaveTargetDir = Server.MapPath("~/TEMP/");
       
        //if (ddlMonth.SelectedIndex == 0)
        //{
        //    lblMsg.Text = "Please select month."; ddlMonth.Focus(); return;
        //}
        //if (ddlYear.SelectedIndex == 0)
        //{
        //    lblMsg.Text = "Please select year."; ddlYear.Focus(); return;
        //}

        ////GET VESSEL
        //try
        //{
        //    string strConectionString = sqlConnectionString.Replace("Master", "eShip");
        //    SqlConnection SqlCon = new SqlConnection(strConectionString);
        //    SqlCommand cmd = new SqlCommand("", SqlCon);
        //    SqlDataAdapter AdpExport = new SqlDataAdapter(cmd);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.CommandText = "GET_DATA_PACKET";
        //    cmd.Parameters.Add(new SqlParameter("@VESSCODE", (object)CurrentVessel));
        //    cmd.Parameters.Add(new SqlParameter("@MONTH", (object)ddlMonth.SelectedValue));
        //    cmd.Parameters.Add(new SqlParameter("@YEAR", (object)ddlYear.SelectedValue));
        //    DataSet dsSchema = new DataSet();
        //    AdpExport.Fill(dsSchema);

        //    dsSchema.Tables[0].TableName = "WCPacketHeader";
        //    dsSchema.Tables[1].TableName = "CP_VesselCrewSignOnOff";
        //    dsSchema.Tables[2].TableName = "CP_CrewDailyLocation";
        //    dsSchema.Tables[3].TableName = "CP_CrewDailyWorkRestHours";
        //    dsSchema.Tables[4].TableName = "CP_CrewHoursLog";
        //    dsSchema.Tables[5].TableName = "CP_NonConformance";
        //    dsSchema.Tables[6].TableName = "CP_NonConformanceReason";
       
       
        //    String SchemaFileName = SaveTargetDir + "WCSCHEMA_" + CurrentVessel + "_" + ddlMonth.SelectedItem.Text + "_" + ddlYear.SelectedValue + ".xml";
        //    String DataFileName = SaveTargetDir + "WC_" + CurrentVessel + "_" + ddlMonth.SelectedItem.Text + "_" + ddlYear.SelectedValue + ".xml";

        //    dsSchema.WriteXml(DataFileName);
        //    dsSchema.WriteXmlSchema(SchemaFileName);

        //    string zipFileName = "WC_" + CurrentVessel + "_" + ddlMonth.SelectedItem.Text + "_" + ddlYear.SelectedValue + ".zip";
        //    string zipFilePath = SaveTargetDir + zipFileName;

        //    using (ZipFile zip = new ZipFile())
        //    {
        //        zip.AddFile(SchemaFileName);
        //        zip.AddFile(DataFileName);

        //        zip.Save(zipFilePath);
        //        ExportFileToResponse(zipFilePath);
        //    }

        //    lblMsg.Text = "Data exported successfully. File Name : " + zipFileName;
        //}
        //catch (Exception ex)
        //{
        //     lblMsg.Text = "Unable to export data." + ex.Message;
        //}
    }
    protected void ExportFileToResponse(string FilePath)
    {
        //string SaveTargetDir = Server.MapPath("~/TEMP/");
        //Response.Clear();
        //Response.ContentType = "application/zip";
        //Response.AddHeader("Content-Type", "application/zip");
        //Response.AddHeader("Content-Disposition", "attachment;filename=" + Path.GetFileName(FilePath));
        //Response.WriteFile(SaveTargetDir + FilePath);
        //Response.BinaryWrite(File.ReadAllBytes(SaveTargetDir + FilePath));
        //Response.Close(); Response.End();
        
        ScriptManager.RegisterStartupScript(this, this.GetType(), "dfg", "window.open('FileDownload.aspx?File=" + Path.GetFileName(FilePath) + "','new');", true);
    }
}