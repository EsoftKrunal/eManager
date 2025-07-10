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
using System.Text.RegularExpressions;
using OpenPop.Pop3;
using OpenPop.Mime.Header;
using OpenPop.Common;
using OpenPop;


public partial class Communication : System.Web.UI.Page
{
    class EH : OpenPop.IParsingErrorHandler
    {
        public void HandleParseError(ParseError error)
        {
            throw new NotImplementedException();
        }
    }

    public int SelectedTab
    {
        get { return Common.CastAsInt32(ViewState["SelectedTab"]); }
        set { ViewState["SelectedTab"] = value; }
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

    //string sqlConnectionString = @"Data Source=192.168.1.8\SQLDEV;Initial Catalog=Master;Persist Security Info=True;User Id=sa;Password=mtm-12345";
    string sqlConnectionString = @"Data Source=LOCALHOST\SQLEXPRESS;Initial Catalog=Master;Integrated Security=SSPI;Persist Security Info=False";
    //string sqlConnectionString = @"Data Source=LWDOT644;Initial Catalog=Master;User Id=sa;Password=Esoft1234;Persist Security Info=True";
    //string sqlConnectionString = @"Data Source=192.168.1.4\SQLEXPRESS;Initial Catalog=Master;User Id=sa;Password=mtm-123;Persist Security Info=True";

    protected void Page_Load(object sender, EventArgs e)
    {
        // -------------------------- SESSION CHECK ----------------------------------------------
        ProjectCommon.SessionCheck();
        // -------------------------- SESSION CHECK END ----------------------------------------------
        CurrentVessel = Session["CurrentShip"].ToString();
        lblMsg.Text = "";
        if (!IsPostBack)
        {
            CurrentVessel = Session["CurrentShip"].ToString();
            ClearTempFiles();
            SelectedTab = 1;
            RefreshMenu();
            ShowSMTPSettings();
            RedirectToPage();            
        }
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
    protected void menu_Click(object sender, EventArgs e)
    {
        SelectedTab = Common.CastAsInt32(((Button)sender).CommandArgument);
        RefreshMenu();
        RedirectToPage();
    }
    public void RefreshMenu()
    {
        btnBackUp.CssClass = "btnNormal";
        btnExport.CssClass = "btnNormal";
        btnImport.CssClass = "btnNormal";
        btnMaintenance.CssClass = "btnNormal";
        btnSmtpSettings.CssClass = "btnNormal";
        btnRestHour.CssClass = "btnNormal";

        pnlBackup.Visible = false;
        pnlExport.Visible = false;
        pnlImport.Visible = false;
        pnlMaintenance.Visible = false;
        pnlSmtpSettings.Visible = false;
        pnlRestHour.Visible = false;

        dvMSG.Visible = true;
        

        switch (SelectedTab)
        {
            case 1:
                btnBackUp.CssClass = "btnSelected";
                break;
            case 2:
                btnExport.CssClass = "btnSelected";
                dvMSG.Visible = false;
                break;
            case 3:
                btnImport.CssClass = "btnSelected";
                break;
            case 4:
                btnMaintenance.CssClass = "btnSelected";
                break;
            case 5:
                btnSmtpSettings.CssClass = "btnSelected";
                break;
            case 6:
                btnRestHour.CssClass = "btnSelected";
                break;
            default:
                break;
        }

    }
    public void RedirectToPage()
    {
        switch (SelectedTab)
        {
            case 1:
                btnBackUp.CssClass = "btnSelected";
                pnlBackup.Visible = true;
                break;
            case 2:
                btnExport.CssClass = "btnSelected";
                pnlExport.Visible = true;
                break;
            case 3:
                btnImport.CssClass = "btnSelected";
                pnlImport.Visible = true;
                break;
            case 4:
                btnMaintenance.CssClass = "btnSelected";
                pnlMaintenance.Visible = true;
                break;
            case 5:
                btnSmtpSettings.CssClass = "btnSelected";
                pnlSmtpSettings.Visible = true;
                ShowSMTPSettings();
                break;
            case 6:
                btnRestHour.CssClass = "btnSelected";
                pnlRestHour.Visible = true;
                ShowRestHourCrew();
                break;
            default:
                break;
        }
    }
    //------- Backup Section --------------
    protected void btnDBBackup_Click(object sender, EventArgs e)
    {
        if (ddlDB.SelectedValue == "")
        {
            ddlDB.Focus();
            lblMsg.Text = "Please select database.";
            return;
        }

        try
        {
            
            string _Path = Server.MapPath("Temp\\");
            string FileNameOnly = ddlDB.SelectedValue + "_" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss").Replace(":", "_").Replace(" ", "-");
            string FileName = _Path + FileNameOnly + ".BAK";
            TakeBackUp(ddlDB.SelectedValue, FileName);

            string ZipData = Server.MapPath("TEMP/" + CurrentVessel + "_DBBKP_" +  FileNameOnly + ".zip");
            if (File.Exists(ZipData)) { File.Delete(ZipData); }

            using (ZipFile zip = new ZipFile())
            {
                zip.AddFile(FileName);
                zip.Save(ZipData);
                ExportFileToResponse(ZipData);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Unable to create backup. Error : " + ex.Message;
        }
    }
    public bool TakeBackUp(string DataBaseName, string _FileName)
    {
        //------------------ BACKUP SQL -----------------------

        string ScriptContent = "BACKUP DATABASE " + DataBaseName + " TO DISK='" + _FileName + "'";         
        bool result = false;
        try
        {
            SqlConnection conn = new SqlConnection(sqlConnectionString);
            SqlCommand cmd = new SqlCommand(ScriptContent, conn);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            lblMsg.Text = "Backup created successfully.";
        }
        catch (Exception ex)
        {
            lblMsg.Text = "[" + DataBaseName + "] Unable to create backup. Error : " + ex.Message;
        }
        return result;
    }
    //------- Maintenance Section --------------
    protected void btnExecute_Click(object sender, EventArgs e)
    {
        //if (fup.HasFile)
        //{
        //    try
        //    {
        //        string FileName = fup.PostedFile.FileName;
        //        string ext = Path.GetExtension(FileName);
        //        if (ext != ".sql")
        //        {
        //            lblMsg.Text = "Please check! only sql files allowed.";
        //            return;
        //        }
             
        //        string FileText = (new StreamReader(fup.PostedFile.InputStream)).ReadToEnd();
        //        SqlConnection conn = new SqlConnection(sqlConnectionString);
        //        Server server = new Server(new ServerConnection(conn));
        //        server.ConnectionContext.ExecuteNonQuery(FileText);
        //        lblMsg.Text = "Maintenance done successfully.";
        //    }
        //    catch (Exception ex)
        //    {
        //        lblMsg.Text = "Unable to process maintenance. Error : " + ex.Message;
        //    }
        //}
        //else
        //{
        //    fup.Focus();
        //    lblMsg.Text = "Please select file to continue.";
        //}
    } 
    protected void btnDBMaintenance_Click(object sender, EventArgs e)
    {
        try
        {
            string FileName = Server.MapPath("Maintenance.sql");
            if (File.Exists(FileName))
            {
                SqlConnection conn = new SqlConnection(sqlConnectionString);
                Server server = new Server(new ServerConnection(conn));
                server.ConnectionContext.ExecuteNonQuery(File.ReadAllText(FileName));
                lblMsg.Text = "Maintenance done successfully.";
            }
            else
            {
                lblMsg.Text = "Maintenance file not found.";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Unable to process maintenance. Error : " + ex.Message;
        }
    }
    //------- Export Section --------------
    //------- Import Section --------------
    protected void btnImport1_Click(object sender, EventArgs e)
    {
        string PMSRootPath = Server.MapPath("~/");
        List<Stream> ZipFiles = new List<Stream>();
        
        string TEMPFOLDERPATH = Server.MapPath("~/TEMP/UPDATE_" + DateTime.Now.ToString("dd_MMM_yyyy_hh_mm_ss"));
        ClearTempFiles();
        if(!(Directory.Exists(TEMPFOLDERPATH)))
            Directory.CreateDirectory(TEMPFOLDERPATH);
        if (flp_Import.HasFile)
        {
            try
            {
                string ZipFileName = flp_Import.PostedFile.FileName;
                string ReplyPacketName = "REPLY_" + Path.GetFileName(ZipFileName);

                string ext = Path.GetExtension(ZipFileName);
                if (ext != ".zip")
                {
                    lblMsg.Text = "Please check! only zip files allowed.";
                    return;
                }
                ImportPacket(flp_Import.PostedFile.InputStream, ZipFileName);
            }
            catch (Exception ex)
            {
                lblMsg.Text = "Unable to import packet. Error : " + ex.Message;
            }
        }
        else
        {
            flp_Import.Focus();
            lblMsg.Text = "Please select packet to continue.";
        }
    }
    protected void btnImport2_Click(object sender, EventArgs e)
    {
        EH eh = new EH();
        List<String> seenUids = new List<String>();
        string PMSRootPath = Server.MapPath("~/");
        List<Stream> ZipFiles = new List<Stream>();
        string TEMPFOLDERPATH = Server.MapPath("~/TEMP/UPDATE_" + DateTime.Now.ToString("dd_MMM_yyyy_hh_mm_ss"));
        ClearTempFiles();
        if (!(Directory.Exists(TEMPFOLDERPATH)))
            Directory.CreateDirectory(TEMPFOLDERPATH);
        // The client disconnects from the server when being disposed
		Session["pop_importstatus"]="POP3 server connecting.";
        using (OpenPop.Pop3.Pop3Client client = new Pop3Client())
        {
		Session["pop_importstatus"]="POP3 server connected.";

            // Connect to the server
            client.Connect(txtMailClient.Text, 110,false);

            // Authenticate ourselves towards the server
            client.Authenticate("pms", "KWEkAGYV");

            // Fetch all the current uids seen
            List<string> uids = client.GetMessageUids();

            // Create a list we can return with all new messages
            List<OpenPop.Mime.Message> newMessages = new List<OpenPop.Mime.Message>();
            int MessgeCount = uids.Count;
            Session["pop_importstatus"]=" ( " + uids.Count.ToString() + " ) messages found on server.";
            // All the new messages not seen by the POP3 client
            for (int i = 0; i < uids.Count; i++)
            {
                string currentUidOnServer = uids[i];
                if (!seenUids.Contains(currentUidOnServer))
                {
                    // We have not seen this message before.
                    // Download it and add this new uid to seen uids

                    // the uids list is in messageNumber order - meaning that the first
                    // uid in the list has messageNumber of 1, and the second has 
                    // messageNumber 2. Therefore we can fetch the message using
                    // i + 1 since messageNumber should be in range [1, messageCount]
                    OpenPop.Mime.Message unseenMessage = client.GetMessage(i + 1,eh);

                    // find attachments
                    List<OpenPop.Mime.MessagePart> att = unseenMessage.FindAllAttachments();
                    Session["pop_importstatus"] = "Downloading mail packets " + (i + 1).ToString() + " of " + MessgeCount + " having ( " + att.Count.ToString() + " ) attachments from server.";
                    foreach (OpenPop.Mime.MessagePart ado in att)
                    {
                        string tempfilepath = TEMPFOLDERPATH + @"\" + ado.FileName;
                        if (System.IO.File.Exists(tempfilepath))
                            File.Delete(tempfilepath);
                        FileInfo fi = new FileInfo(tempfilepath);
                        ado.Save(fi);                        
                    }
                    // Add the message to the new messages
                    //newMessages.Add(unseenMessage);
                    // Add the uid to the seen uids, as it has now been seen
                    //seenUids.Add(currentUidOnServer);
                }
            }

            string[] files01 = Directory.GetFiles(TEMPFOLDERPATH);
            int c = 1;
            foreach(string f in files01)
            {
                using (FileStream fs = new FileStream(f, FileMode.Open))
                {
                    Session["pop_importstatus"] = "Processing packet " + c.ToString() + " of " + files01.Length + " with filename '" + Path.GetFileName(f) + "'";
                    try {
                        ImportPacket(fs, Path.GetFileName(f));
                    }catch(Exception ex)
                    {

                    }
                }
                c++;
            }
            for (int i = 1; i <= uids.Count; i++)
            {
                client.DeleteMessage(i);
            }
            // Return our new found messages
            Session["pop_importstatus"] = "Processed mail " + MessgeCount.ToString() + " of " + MessgeCount;
        }
    }
    protected void btnImportedLog_Refresh(object sender, EventArgs e)
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT TOP 100 *,row_number() over(order by TABLEID DESC) as sno FROM DBO.COMM_PACKET_IMPORT_LOG ORDER BY TABLEID DESC");
        rptpacketlog.DataSource = dt;
        rptpacketlog.DataBind();
    }
    protected void ImportPacket(Stream Stream1,String ZipFileName)
    {
        string db_packettype = "";
        string filenameonly = Path.GetFileName(ZipFileName);
        string PMSRootPath = Server.MapPath("~/");
        string ReplyPacketName = "REPLY_" + Path.GetFileName(ZipFileName);
        string TEMPFOLDERPATH = Server.MapPath("~/TEMP/UPDATE_" + DateTime.Now.ToString("dd_MMM_yyyy_hh_mm_ss"));
        using (ZipFile zip = ZipFile.Read(Stream1))
        {
            if (zip.ContainsEntry("PacketConfig.xml"))
            {
                zip.ExtractAll(TEMPFOLDERPATH, ExtractExistingFileAction.OverwriteSilently);
            }
            else
            {
                foreach (ZipEntry ex in zip.EntriesSorted)
                {
                    try
                    {
                        ex.FileName = Path.GetFileName(ex.FileName);
                        ex.Extract(TEMPFOLDERPATH, ExtractExistingFileAction.OverwriteSilently);
                    }
                    catch { continue; }
                }
            }
        }
        //---------------------- FILES ARE READY
        //---------------------- READ PACKET CONFIGURATION FILE
        string PacketConfigFilePath = TEMPFOLDERPATH + @"\PacketConfig.xml";
        if (File.Exists(PacketConfigFilePath)) // --   NEW PACKET TYPE WITH CONFIG FILE
        {
            string PacketName = "", PacketType = "", PacketDataType = "", Reply = "";
            XmlDocument xd = new XmlDocument();
            xd.Load(PacketConfigFilePath);

            XmlNodeList xm = xd.GetElementsByTagName("PacketName");
            if (xm.Count == 1)
                PacketName = xm[0].InnerText.Trim();

            xm = xd.GetElementsByTagName("PacketType");
            if (xm.Count == 1)
                PacketType = xm[0].InnerText.Trim();

            xm = xd.GetElementsByTagName("PacketDataType");
            if (xm.Count == 1)
                PacketDataType = xm[0].InnerText.Trim();

            xm = xd.GetElementsByTagName("Reply");
            if (xm.Count == 1)
                Reply = xm[0].InnerText.Trim();

            //---------------------- PACKET NAME VALIDATION -------------------------------

            if (Path.GetFileName(ZipFileName).ToUpper() != PacketName.ToUpper())
            {
                lblMsg.Text = "Can't Import. Packet configuration is not matching with its FileName.";
                return;
            }
            db_packettype = PacketType;

            if (PacketType == "FILES")
            {
                #region -------------- FILES ------------------------------
                //----------------------
                if (Reply == "Y")
                {
                    List<string> Files = new List<string>();
                    List<string> Folders = new List<string>();

                    // creating files and folder list need to reply
                    xm = xd.GetElementsByTagName("ReplyFiles");
                    if (xm.Count == 1)
                    {
                        foreach (XmlNode nd in xm[0].ChildNodes)
                        {
                            string InnerText = nd.InnerText;
                            if (InnerText.EndsWith("\\"))
                                Folders.Add(InnerText);
                            else
                                Files.Add(InnerText);
                        }
                    }

                    // create zip file need to reply

                    using (ZipFile zip = new ZipFile())
                    {

                        foreach (string fl in Files)
                        {
                            string TempName = Server.MapPath("~\\") + fl;
                            if (File.Exists(TempName))
                                zip.AddFile(TempName, Path.GetDirectoryName(fl));

                        }

                        foreach (string fld in Folders)
                        {
                            string TempName = Server.MapPath("~\\") + fld;
                            if (Directory.Exists(TempName))
                                zip.AddDirectory(TempName, fld);
                        }

                        string ReturnFileName = TEMPFOLDERPATH + "\\" + ReplyPacketName;
                        zip.Save(ReturnFileName);

                        ExportFileToResponse(ReturnFileName);
                    }

                }
                else
                {
                    // copyign all files
                    string[] Files = Directory.GetFiles(TEMPFOLDERPATH);
                    foreach (string fl in Files)
                    {
                        if (Path.GetFileName(fl) != "PacketConfig.xml")
                        {
                            if (File.Exists(PMSRootPath + "//" + Path.GetFileName(fl)))
                                File.Delete(PMSRootPath + "//" + Path.GetFileName(fl));

                            File.Copy(fl, PMSRootPath + "//" + Path.GetFileName(fl));
                        }
                    }

                    // copyign all folders
                    string[] folders = Directory.GetDirectories(TEMPFOLDERPATH);
                    foreach (string folder in folders)
                    {
                        MoveFiles(PMSRootPath, folder);
                    }
                }
                //----------------------
                #endregion
                lblMsg.Text = "Files Packet Imported successfully.";
            }
            if (PacketType == "SCRIPT")
            {
                #region -------------- SCRIPT ------------------------------
                string ScriptFileName = TEMPFOLDERPATH + @"\Script.sql";
                SqlConnection Con = new SqlConnection(sqlConnectionString.Replace("Master", "eMANAGER"));
                if (Reply == "Y")
                {
                    DataSet ds = new DataSet();
                    SqlCommand cmd = new SqlCommand(File.ReadAllText(ScriptFileName), Con);
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    adp.Fill(ds);

                    string TempSchemaFile = TEMPFOLDERPATH + "\\Schema.xml";
                    string TempDataFile = TEMPFOLDERPATH + "\\Data.xml";
                    string ReturnFileName = TEMPFOLDERPATH + "\\" + ReplyPacketName;
                    ds.WriteXmlSchema(TempSchemaFile);
                    ds.WriteXml(TempDataFile);

                    using (ZipFile zip = new ZipFile())
                    {
                        zip.AddFile(TempSchemaFile, "");
                        zip.AddFile(TempDataFile, "");
                        zip.Save(ReturnFileName);
                    }
                    ExportFileToResponse(ReturnFileName);
                }
                else
                {
                    Server server = new Server(new ServerConnection(Con));
                    server.ConnectionContext.ExecuteNonQuery(File.ReadAllText(ScriptFileName));
                }
                #endregion
                lblMsg.Text = "Script Packet Imported successfully.";
            }
            if (PacketType == "DATA")
            {
                if (PacketDataType == "RISKTEMPLATES")
                {
                    //===================================  Import Risk Templates
                    DataSet ds = new DataSet();
                    string _SchemaFile = TEMPFOLDERPATH + @"\RCA_Template_Schema.xml";
                    string _DataFile = TEMPFOLDERPATH + @"\RCA_Template_Data.xml";
                    ds.ReadXmlSchema(_SchemaFile);
                    ds.ReadXml(_DataFile);
                    ResetNULLDates(ref ds);
                    RiskTemplates_Import(ds, ZipFileName);
                    //===================================
                    //if (Reply == "Y")
                    //{
                    //    ds.Clear();
                    //    ds.Tables.Clear();

                    //    SqlConnection Con = new SqlConnection(sqlConnectionString.Replace("Master", "eMANAGER"));
                    //    SqlCommand cmd = new SqlCommand("SELECT '" +  CurrentVessel + "' as VESSELCODE,TEMPLATEID,ShipRecdOn from DBO.[EV_TemplateMaster] WHERE SHIPRECDON>=DATEADD(YEAR,-1,GETDATE())", Con);
                    //    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    //    adp.Fill(ds);

                    //    string TempSchemaFile = TEMPFOLDERPATH + "\\RA_TEMP_ACK_Schema.xml";
                    //    string TempDataFile = TEMPFOLDERPATH + "\\RA_TEMP_ACK_Data.xml";
                    //    string ReturnFileName = TEMPFOLDERPATH + "\\" + ReplyPacketName;
                    //    ds.WriteXmlSchema(TempSchemaFile);
                    //    ds.WriteXml(TempDataFile);

                    //    using (ZipFile zip = new ZipFile())
                    //    {
                    //        zip.AddFile(TempSchemaFile, "");
                    //        zip.AddFile(TempDataFile, "");
                    //        zip.Save(ReturnFileName);
                    //    }
                    //    ExportFileToResponse(ReturnFileName);
                    //}
                    try
                    {
                        Common.Set_Procedures("[DBO].[sp_Insert_Communication_Export]");
                        Common.Set_ParameterLength(5);
                        Common.Set_Parameters(
                            new MyParameter("@VesselCode", CurrentVessel),
                            new MyParameter("@RecordType", "RISKTEMPLATES-ACK"),
                            new MyParameter("@RecordId", 0),
                            new MyParameter("@RecordNo", "Risk Templates Ack."),
                            new MyParameter("@CreatedBy", Session["FullName"].ToString().Trim())
                        );

                        DataSet ds1 = new DataSet();
                        ds1.Clear();
                        Boolean res;
                        res = Common.Execute_Procedures_IUD(ds1);
                        if (res)
                        {
                            if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
                            {
                                lblMsg.Text = ds1.Tables[0].Rows[0][0].ToString();
                            }
                            else
                            {
                                lblMsg.Text = "RISKTEMPLATES Imported successfully. Acknowledgement sent for export successfully.";
                            }

                        }
                        else
                        {
                            lblMsg.Text = "RISKTEMPLATES Imported successfully. Unable to send acknowledgement for export.Error : " + Common.getLastError();
                        }
                    }
                    catch (Exception ex)
                    {
                        lblMsg.Text = "RISKTEMPLATES Imported successfully. Unable to send acknowledgement for export.Error : " + ex.Message;
                    }
                }
                if (PacketDataType == "RISKDATA")
                {
                    //===================================  Import Risk Templates
                    DataSet ds = new DataSet();
                    string _SchemaFile = TEMPFOLDERPATH + @"\RCA_Schema.xml";
                    string _DataFile = TEMPFOLDERPATH + @"\RCA_Data.xml";
                    ds.ReadXmlSchema(_SchemaFile);
                    ds.ReadXml(_DataFile);
                    ResetNULLDates(ref ds);
                    RiskData_Import(ds, ZipFileName);
                    //===================================
                    lblMsg.Text = "Packet Imported successfully.";
                }
                if (PacketDataType == "SMSFORMS")
                {
                    //===================================  Import SMSFORMS
                    string vslcode = Path.GetFileName(ZipFileName).Substring(0, 3);
                    if (CurrentVessel == vslcode.ToUpper())
                    {
                        DataSet ds = new DataSet();
                        string _SchemaFile = TEMPFOLDERPATH + @"\SMSFORMS_Schema.xml";
                        string _DataFile = TEMPFOLDERPATH + @"\SMSFORMS_Data.xml";
                        ds.ReadXmlSchema(_SchemaFile);
                        ds.ReadXml(_DataFile);
                        ResetNULLDates(ref ds);
                        SMSFORMS_Import(ds, ZipFileName);
                        lblMsg.Text = "Packet Imported successfully.";
                    }
                    else
                    {
                        lblMsg.Text = "Wrong packet ! Vessel code is not matching.";
                    }

                    //===================================
                }
            }
        }
        else // OLD PACKET TYPE
        {
            string ZipFileNameOnly = Path.GetFileName(ZipFileName).ToUpper();
            if (ZipFileNameOnly.StartsWith("PRM_ACK_O_"))
            {
                db_packettype = "Purchase Request Acknowledgement";
                String SpareSchemaFile = TEMPFOLDERPATH + @"\PR_ACK_SCHEMA.xml";
                String SpareDataFile = TEMPFOLDERPATH + @"\PR_ACK_DATA.xml";

                if (File.Exists(SpareSchemaFile) && File.Exists(SpareDataFile))
                {
                    DataSet ds = new DataSet();
                    ds.ReadXmlSchema(SpareSchemaFile);
                    ds.ReadXml(SpareDataFile);
                    ResetNULLDates(ref ds);

                    string VesselCode = Path.GetFileName(ZipFileName).Substring(9, 3);

                    //---------------- CHECKING PACKET IS FOR SAME VESSEL OR NOT
                    SqlConnection Con = new SqlConnection(sqlConnectionString.Replace("Master", "eMANAGER"));
                    SqlCommand cmdCheck = new SqlCommand("", Con);
                    SqlDataAdapter Sda = new SqlDataAdapter(cmdCheck);

                    Con.Open();
                    DataSet DsTemp = new DataSet();
                    cmdCheck.CommandText = "SELECT * FROM DBO.SETTINGS";
                    Sda.Fill(DsTemp, "Set");

                    Con.Close();

                    string In_VesselCode = DsTemp.Tables[0].Rows[0]["SHIPCODE"].ToString();
                    if (VesselCode != In_VesselCode)
                    {
                        lblMsg.Text = "Importing packet's VESSEL is not matching with VESSEL.";
                        return;
                    }

                    Purchase_Import(ds, ZipFileName);
                }
                else
                    lblMsg.Text = "Invalid Packet.";

            }
            else if ((ZipFileNameOnly).StartsWith("SMS_RANKS"))
            {
                db_packettype = "Purchase Request Acknowledgement";
                String SchemaFile = TEMPFOLDERPATH + @"\SMS_Rank_Schema" + ".xml";
                String DataFile = TEMPFOLDERPATH + @"\SMS_Rank_Data" + ".xml";

                if (File.Exists(SchemaFile) && File.Exists(DataFile))
                {
                    DataSet ds = new DataSet();
                    ds.ReadXmlSchema(SchemaFile);
                    ds.ReadXml(DataFile);
                    ResetNULLDates(ref ds);
                    SMS_RANKS_Import(ds, Path.GetFileName(ZipFileName));
                }
                else
                    lblMsg.Text = "Invalid Packet.";
            }
            else if ((ZipFileNameOnly).Contains("SMS"))
            {
                String SpareSchemaFile = TEMPFOLDERPATH + @"\SMS_Schema.xml";
                String SpareDataFile = TEMPFOLDERPATH + @"\SMS.xml";
                if (File.Exists(SpareSchemaFile) && File.Exists(SpareDataFile))
                {
                    DataSet ds = new DataSet();
                    ds.ReadXmlSchema(SpareSchemaFile);
                    ds.ReadXml(SpareDataFile);
                    ResetNULLDates(ref ds);
                    SMS_Import(ds, ZipFileName);
                }
                else
                    lblMsg.Text = "Invalid Packet.";

            }
            else if ((ZipFileNameOnly).StartsWith("PMS_CREW_TRAININGREQUIREMENT_"))
            {
                String SpareSchemaFile = TEMPFOLDERPATH + @"\SCHEMA_PMS_CREW_TRAININGREQUIREMENT.xml";
                String SpareDataFile = TEMPFOLDERPATH + @"\DATA_PMS_CREW_TRAININGREQUIREMENT.xml";
                if (File.Exists(SpareSchemaFile) && File.Exists(SpareDataFile))
                {
                    DataSet ds = new DataSet();
                    ds.ReadXmlSchema(SpareSchemaFile);
                    ds.ReadXml(SpareDataFile);
                    ResetNULLDates(ref ds);
                    PMS_CREW_Trainings_Import(ds, ZipFileName);
                }
                else
                    lblMsg.Text = "Invalid Packet.";

            }
            else if ((ZipFileNameOnly).StartsWith("STORE_ITEMS_"))
            {
                String StoreSchemaFile = TEMPFOLDERPATH + @"\STORE_ITEMS_Schema.xml";
                String StoreDataFile = TEMPFOLDERPATH + @"\STORE_ITEMS_Data.xml";
                if (File.Exists(StoreSchemaFile) && File.Exists(StoreDataFile))
                {
                    DataSet ds = new DataSet();
                    ds.ReadXmlSchema(StoreSchemaFile);
                    ds.ReadXml(StoreDataFile);
                    ResetNULLDates(ref ds);
                    STORE_ITEMS_MASTER_Import(ds, ZipFileName);
                }
                else
                    lblMsg.Text = "Invalid Packet.";
            }
            else if ((ZipFileNameOnly).StartsWith("ORDER_ITEMS_O_"))
            {
                String StoreSchemaFile = TEMPFOLDERPATH + @"\ORDER_ITEMS_Schema.xml";
                String StoreDataFile = TEMPFOLDERPATH + @"\ORDER_ITEMS_Data.xml";
                if (File.Exists(StoreSchemaFile) && File.Exists(StoreDataFile))
                {
                    DataSet ds = new DataSet();
                    ds.ReadXmlSchema(StoreSchemaFile);
                    ds.ReadXml(StoreDataFile);
                    ResetNULLDates(ref ds);
                    IMPORT_ORDER_ITEMS(ds, ZipFileName);
                }
                else
                    lblMsg.Text = "Invalid Packet.";
            }
            else if ((ZipFileNameOnly).StartsWith("MRV_O_"))
            {
                String StoreSchemaFile = TEMPFOLDERPATH + @"\MRV_Schema.xml";
                String StoreDataFile = TEMPFOLDERPATH + @"\MRV_Data.xml";
                if (File.Exists(StoreSchemaFile) && File.Exists(StoreDataFile))
                {
                    DataSet ds = new DataSet();
                    ds.ReadXmlSchema(StoreSchemaFile);
                    ds.ReadXml(StoreDataFile);
                    ResetNULLDates(ref ds);
                    IMPORT_OFFICE_MRV(ds, ZipFileName);
                }
                else
                    lblMsg.Text = "Invalid Packet.";
            }
            else if ((ZipFileNameOnly).StartsWith("JOBCOMM_O_"))
            {
                String StoreSchemaFile = TEMPFOLDERPATH + @"\SCHEMA_JobComments.xml";
                String StoreDataFile = TEMPFOLDERPATH + @"\DATA_JobComments.xml";
                if (File.Exists(StoreSchemaFile) && File.Exists(StoreDataFile))
                {
                    DataSet ds = new DataSet();
                    ds.ReadXmlSchema(StoreSchemaFile);
                    ds.ReadXml(StoreDataFile);
                    ResetNULLDates(ref ds);
                    IMPORT_JOB_COMM(ds, ZipFileName);
                }
                else
                    lblMsg.Text = "Invalid Packet.";
            }
            else if ((ZipFileNameOnly).Contains("MNPPACKET"))
            {
                String SchemaFile = TEMPFOLDERPATH + @"\MNP_Schema" + ".xml";
                String DataFile = TEMPFOLDERPATH + @"\MNP_Data" + ".xml";

                if (File.Exists(SchemaFile) && File.Exists(DataFile))
                {
                    DataSet ds = new DataSet();
                    ds.ReadXmlSchema(SchemaFile);
                    ds.ReadXml(DataFile);
                    ResetNULLDates(ref ds);
                    MNP_Import(ds, Path.GetFileName(ZipFileName));
                }
                else
                    lblMsg.Text = "Invalid Packet.";
            }
            else if ((ZipFileNameOnly).Contains("VNA_O_"))
            {
                String SchemaFile = TEMPFOLDERPATH + @"\VNASchema" + ".xml";
                String DataFile = TEMPFOLDERPATH + @"\VNAData" + ".xml";

                if (File.Exists(SchemaFile) && File.Exists(DataFile))
                {
                    DataSet ds = new DataSet();
                    ds.ReadXmlSchema(SchemaFile);
                    ds.ReadXml(DataFile);
                    ResetNULLDates(ref ds);
                    VNA_Import(ds, Path.GetFileName(ZipFileName));
                }
                else
                    lblMsg.Text = "Invalid Packet.";
            }
            else if (ZipFileNameOnly.StartsWith("ER_"))
            {
                if (ZipFileNameOnly.StartsWith("ER_RCA"))
                {
                    String SchemaFile = TEMPFOLDERPATH + @"\RCA_SCHEMA.xml";
                    String DataFile = TEMPFOLDERPATH + @"\RCA_DATA.xml";
                    if (File.Exists(SchemaFile) && File.Exists(DataFile))
                    {
                        DataSet ds = new DataSet();
                        ds.ReadXmlSchema(SchemaFile);
                        ds.ReadXml(DataFile);
                        ResetNULLDates(ref ds);
                        Import_Into_RCA(ds, Path.GetFileName(ZipFileName));
                    }
                    else
                        lblMsg.Text = "Invalid Packet.";
                }
                else if (ZipFileNameOnly.StartsWith("BD_RCA_"))
                {
                    String SchemaFile = TEMPFOLDERPATH + @"\BD_RCA_SCHEMA.xml";
                    String DataFile = TEMPFOLDERPATH + @"\BD_RCA_DATA.xml";
                    if (File.Exists(SchemaFile) && File.Exists(DataFile))
                    {
                        DataSet ds = new DataSet();
                        ds.ReadXmlSchema(SchemaFile);
                        ds.ReadXml(DataFile);
                        ResetNULLDates(ref ds);
                        Import_Into_BD_RCA(ds, Path.GetFileName(ZipFileName));
                    }
                    else
                        lblMsg.Text = "Invalid Packet.";
                }
                else if (ZipFileNameOnly.Contains("FILES"))
                {
                    //Import_Into_ER_FILES(ds, Path.GetFileName(ZipFileName));
                }
                else
                {
                    string FileNameWithoutExtension = Path.GetFileNameWithoutExtension(ZipFileName);
                    string ts = FileNameWithoutExtension.Substring(FileNameWithoutExtension.LastIndexOf("_") + 1);
                    int IndexofDash = ts.LastIndexOf("-");
                    string FormNo = ts.Substring(0, IndexofDash);

                    String SchemaFile = TEMPFOLDERPATH + @"\SCHEMA_" + FormNo + ".xml";
                    String DataFile = TEMPFOLDERPATH + @"\DATA_" + FormNo + ".xml";

                    if (File.Exists(SchemaFile) && File.Exists(DataFile))
                    {
                        DataSet ds = new DataSet();
                        ds.ReadXmlSchema(SchemaFile);
                        ds.ReadXml(DataFile);
                        ResetNULLDates(ref ds);
                        if (FormNo.ToUpper() == "S115")
                            Import_Into_ER_S115(ds, Path.GetFileName(ZipFileName), FormNo);
                        else
                            Import_Into_ER(ds, Path.GetFileName(ZipFileName), FormNo);
                    }
                    else
                        lblMsg.Text = "Invalid Packet.";
                }
            }
            else if (ZipFileNameOnly.ToUpper().StartsWith("VIQ_"))
            {
                String SchemaFile = TEMPFOLDERPATH + @"\VETTING_SCHEMA.xml";
                String DataFile = TEMPFOLDERPATH + @"\VETTING_DATA.xml";
                if (File.Exists(SchemaFile) && File.Exists(DataFile))
                {
                    DataSet ds = new DataSet();
                    ds.ReadXmlSchema(SchemaFile);
                    ds.ReadXml(DataFile);
                    ResetNULLDates(ref ds);
                    Import_Into_VIQ(ds, Path.GetFileName(ZipFileName));
                }
                else
                    lblMsg.Text = "Invalid Packet.";

            }
            else if (ZipFileNameOnly.ToUpper().StartsWith("INV_OPENING_BAL_"))
            {
                String SchemaFile = TEMPFOLDERPATH + @"\INV_OPENING_BAL_SCHEMA.xml";
                String DataFile = TEMPFOLDERPATH + @"\INV_OPENING_BAL_DATA.xml";
                if (File.Exists(SchemaFile) && File.Exists(DataFile))
                {
                    DataSet ds = new DataSet();
                    ds.ReadXmlSchema(SchemaFile);
                    ds.ReadXml(DataFile);
                    ResetNULLDates(ref ds);
                    Import_Into_Inventory_OpBal(ds, Path.GetFileName(ZipFileName));
                }
                else
                    lblMsg.Text = "Invalid Packet.";

            }
            else if (ZipFileNameOnly.ToUpper().StartsWith("DTM_")) // DRILL AND TRAINGING MATRIX
            {
                string SchemaFile = "";
                string DataFile = "";
                string PacketType = "";

                SchemaFile = TEMPFOLDERPATH + @"\DTM_Schema.xml";
                DataFile = TEMPFOLDERPATH + @"\DTM_Data.xml";
                PacketType = "ACK";

                ImportData_DTM(SchemaFile, DataFile, PacketType);
            }
            else if (ZipFileNameOnly.ToUpper().Trim() == "DRILLMATRIX_O.ZIP")
            {
                string SchemaFile = "";
                string DataFile = "";
                string PacketType = "";

                SchemaFile = TEMPFOLDERPATH + @"\SCHEMA_Matrix.xml";
                DataFile = TEMPFOLDERPATH + @"\DATA_Matrix.xml";
                PacketType = "MAT";

                ImportData_DTM(SchemaFile, DataFile, PacketType);
            }
            else if (ZipFileNameOnly.ToUpper().StartsWith("LFI") || ZipFileNameOnly.ToUpper().StartsWith("LET-")) // LFI
            {
                String SchemaFile = TEMPFOLDERPATH + @"\LFISchema.XML";
                String DataFile = TEMPFOLDERPATH + @"\LFIData.XML";
                if (File.Exists(SchemaFile) && File.Exists(DataFile))
                {
                    DataSet ds = new DataSet();
                    ds.ReadXmlSchema(SchemaFile);
                    ds.ReadXml(DataFile);
                    ResetNULLDates(ref ds);
                    Import_Into_LFI(ds, Path.GetFileName(ZipFileName));
                }
                else
                    lblMsg.Text = "Invalid Packet.";
            }
            else if (ZipFileNameOnly.ToUpper().StartsWith("DRILL_MAP_")) // DRILL_MAP_
            {
                String SchemaFile = TEMPFOLDERPATH + @"\SCHEMA_VSL_Drill.XML";
                String DataFile = TEMPFOLDERPATH + @"\DATA_VSL_Drill.XML";
                if (File.Exists(SchemaFile) && File.Exists(DataFile))
                {
                    DataSet ds = new DataSet();
                    ds.ReadXmlSchema(SchemaFile);
                    ds.ReadXml(DataFile);
                    ResetNULLDates(ref ds);
                    Import_Into_DRILL_MAP(ds, Path.GetFileName(ZipFileName));
                }
                else
                    lblMsg.Text = "Invalid Packet.";
            }
            else if (ZipFileNameOnly.ToUpper().StartsWith("TRAINING_MAP_")) // TRAINING_MAP_
            {
                String SchemaFile = TEMPFOLDERPATH + @"\SCHEMA_VSL_Training.XML";
                String DataFile = TEMPFOLDERPATH + @"\DATA_VSL_Training.XML";
                if (File.Exists(SchemaFile) && File.Exists(DataFile))
                {
                    DataSet ds = new DataSet();
                    ds.ReadXmlSchema(SchemaFile);
                    ds.ReadXml(DataFile);
                    ResetNULLDates(ref ds);
                    Import_Into_TRAINING_MAP(ds, Path.GetFileName(ZipFileName));
                }
                else
                    lblMsg.Text = "Invalid Packet.";
            }
            else if (ZipFileNameOnly.ToUpper().StartsWith("DRILLTRAININGPLANNER_")) // TRAINING_MAP_
            {
                String SchemaFile = TEMPFOLDERPATH + @"\SCHEMA_DrillTrainingPlanner.XML";
                String DataFile = TEMPFOLDERPATH + @"\DATA_DrillTrainingPlanner.XML";
                if (File.Exists(SchemaFile) && File.Exists(DataFile))
                {
                    DataSet ds = new DataSet();
                    ds.ReadXmlSchema(SchemaFile);
                    ds.ReadXml(DataFile);
                    ResetNULLDates(ref ds);
                    Import_Into_DrillTrainingPlanner(ds, Path.GetFileName(ZipFileName));
                }
                else
                    lblMsg.Text = "Invalid Packet.";
            }
            else if (ZipFileNameOnly.ToUpper().StartsWith("DRILLTRAININGPLANNERW_")) // TRAINING_MAP_
            {
                String SchemaFile = TEMPFOLDERPATH + @"\SCHEMA_DrillTrainingPlanner.XML";
                String DataFile = TEMPFOLDERPATH + @"\DATA_DrillTrainingPlanner.XML";
                if (File.Exists(SchemaFile) && File.Exists(DataFile))
                {
                    DataSet ds = new DataSet();
                    ds.ReadXmlSchema(SchemaFile);
                    ds.ReadXml(DataFile);
                    ResetNULLDates(ref ds);
                    Import_Into_DrillTrainingPlanner(ds, Path.GetFileName(ZipFileName));
                }
                else
                    lblMsg.Text = "Invalid Packet.";
            }
            else if (ZipFileNameOnly.ToUpper().StartsWith("FC")) // FC
            {
                String SchemaFile = TEMPFOLDERPATH + @"\FocusCampSchema.XML";
                String DataFile = TEMPFOLDERPATH + @"\FocusCampData.XML";
                if (File.Exists(SchemaFile) && File.Exists(DataFile))
                {
                    DataSet ds = new DataSet();
                    ds.ReadXmlSchema(SchemaFile);
                    ds.ReadXml(DataFile);
                    ResetNULLDates(ref ds);
                    Import_Into_FC(ds, Path.GetFileName(ZipFileName));
                }
                else
                    lblMsg.Text = "Invalid Packet.";
            }
            else if (ZipFileNameOnly.ToUpper().StartsWith("CMSCREWLIST_"))
            {
                String SchemaFile = TEMPFOLDERPATH + @"\CrewList_Schema.XML";
                String DataFile = TEMPFOLDERPATH + @"\CrewList_Data.XML";
                if (File.Exists(SchemaFile) && File.Exists(DataFile))
                {
                    DataSet ds = new DataSet();
                    ds.ReadXmlSchema(SchemaFile);
                    ds.ReadXml(DataFile);
                    ResetNULLDates(ref ds);
                    Import_Into_CrewList(ds, Path.GetFileName(ZipFileName));
                }
                else
                    lblMsg.Text = "Invalid Packet.";
            }
            else if (ZipFileNameOnly.ToUpper().StartsWith("CRIT_EQ_SH_REQ_O_")) // PMS CRITICAL EQUIPMENT SHUTDOWN REQUEST APPROVAL
            {
                String SchemaFile = TEMPFOLDERPATH + @"\CRITEQSHUTDOWNREQ_O_Schema.XML";
                String DataFile = TEMPFOLDERPATH + @"\CRITEQSHUTDOWNREQ_O_Data.XML";
                if (File.Exists(SchemaFile) && File.Exists(DataFile))
                {
                    DataSet ds = new DataSet();
                    ds.ReadXmlSchema(SchemaFile);
                    ds.ReadXml(DataFile);
                    ResetNULLDates(ref ds);
                    Import_Into_CRIT_EQ_SH_REQ_O_(ds, Path.GetFileName(ZipFileName));
                }
                else
                    lblMsg.Text = "Invalid Packet.";
            }
            else if (ZipFileNameOnly.ToUpper().StartsWith("REG")) // Regulation
            {
                String SchemaFile = TEMPFOLDERPATH + @"\REGSchema.XML";
                String DataFile = TEMPFOLDERPATH + @"\REGData.XML";
                if (File.Exists(SchemaFile) && File.Exists(DataFile))
                {
                    DataSet ds = new DataSet();
                    ds.ReadXmlSchema(SchemaFile);
                    ds.ReadXml(DataFile);
                    ResetNULLDates(ref ds);
                    Import_Into_REG(ds, Path.GetFileName(ZipFileName));
                }
                else
                    lblMsg.Text = "Invalid Packet.";
            }
            else if (ZipFileNameOnly.ToUpper().StartsWith("CIRCULAR")) // Circular
            {
                String SchemaFile = TEMPFOLDERPATH + @"\CircularSchema.XML";
                String DataFile = TEMPFOLDERPATH + @"\CircularData.XML";
                if (File.Exists(SchemaFile) && File.Exists(DataFile))
                {
                    DataSet ds = new DataSet();
                    ds.ReadXmlSchema(SchemaFile);
                    ds.ReadXml(DataFile);
                    ResetNULLDates(ref ds);
                    Import_Into_CIRCULAR(ds, Path.GetFileName(ZipFileName));
                }
                else
                    lblMsg.Text = "Invalid Packet.";
            }
            else if (ZipFileNameOnly.ToUpper().StartsWith("MWUC_")) // MWUC
            {
                if ((CurrentVessel != ZipFileNameOnly.Split('_').GetValue(1).ToString().Trim()) || (ZipFileNameOnly.Split('_').GetValue(2).ToString().Trim() != "O"))
                {
                    lblMsg.Text = "Please check! wrong packet selected. File Name : " + ZipFileNameOnly;
                    return;
                }

                String SchemaFile = TEMPFOLDERPATH + @"\MWUC_Schema.xml";
                String DataFile = TEMPFOLDERPATH + @"\MWUC_Data.xml";
                if (File.Exists(SchemaFile) && File.Exists(DataFile))
                {
                    DataSet ds = new DataSet();
                    ds.ReadXmlSchema(SchemaFile);
                    ds.ReadXml(DataFile);
                    ResetNULLDates(ref ds);
                    Import_Into_MWUC(ds, Path.GetFileName(ZipFileName));
                }
                else
                    lblMsg.Text = "Invalid Packet.";
            }
            else if (ZipFileNameOnly.ToUpper().StartsWith("SCM_O_")) // SCM
            {
                String SchemaFile = TEMPFOLDERPATH + @"\SCMSchema.xml";
                String DataFile = TEMPFOLDERPATH + @"\SCMData.xml";
                if (File.Exists(SchemaFile) && File.Exists(DataFile))
                {
                    DataSet ds = new DataSet();
                    ds.ReadXmlSchema(SchemaFile);
                    ds.ReadXml(DataFile);
                    ResetNULLDates(ref ds);
                    Import_Into_SCM(ds, Path.GetFileName(ZipFileName));
                }
                else
                    lblMsg.Text = "Invalid Packet.";
            }
            else
            {
                String SchemaFile = TEMPFOLDERPATH + @"\" + CurrentVessel + "_SchemaFile" + ".xml";
                String DataFile = TEMPFOLDERPATH + @"\" + CurrentVessel + "_DataFile" + ".xml";
                if (File.Exists(SchemaFile) && File.Exists(DataFile))
                {
                    DataSet ds = new DataSet();
                    ds.ReadXmlSchema(SchemaFile);
                    ds.ReadXml(DataFile);
                    ResetNULLDates(ref ds);
                    PMS_Import(ds, Path.GetFileName(ZipFileName));
                }
                else
                    lblMsg.Text = "Invalid Packet.";
            }
        }
        //------------------------
        Common.Execute_Procedures_Select_ByQuery("Insert into DBO.COMM_PACKET_IMPORT_LOG(PacketType,PacketName,ImportDate,ImportedBy) VALUES('" + filenameonly + "','" + filenameonly + "','" + ECommon.ToDateTimeString(DateTime.Now) + "','" + Session["FullName"].ToString() + "')");
        //------------------------
    }

    //---------------- DRILL PACKET IMPORT
    protected void ImportData_DTM(string SchemaFile, string DataFile, string PacketType)
    {
        string sqlConnectionString = ConfigurationManager.ConnectionStrings["eMANAGER"].ToString();
        SqlTransaction trans;
        SqlConnection Con = new SqlConnection(sqlConnectionString);
        Con.Open();
        trans = Con.BeginTransaction();
        DataSet ds = new DataSet();
        ds.ReadXmlSchema(SchemaFile);
        ds.ReadXml(DataFile);
        ResetNULLDates(ref ds);


        try
        {

            SqlCommand cmd = new SqlCommand("", Con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Transaction = trans;


            //------------------------------------------------      
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "TRUNCATE TABLE DT_MatrixMaster";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "TRUNCATE TABLE DT_MatrixDetails";
            cmd.ExecuteNonQuery();            
            cmd.CommandType = CommandType.StoredProcedure;
            //------------------------------------------------ 

            if (PacketType == "MAT")
            {

                if (ds.Tables["DT_TrainingMaster"].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables["DT_TrainingMaster"].Rows)
                    {
                        //-------------------------------
                        string[] CommandParameters = getCommandParameters(cmd, "DT_Import_TrainingMaster");
                        cmd.Parameters.Clear();
                        cmd.CommandText = "DT_Import_TrainingMaster";
                        cmd.CommandType = CommandType.StoredProcedure;
                        for (int i = 0; i <= CommandParameters.Length - 1; i++)
                        {
                            object data = DBNull.Value;
                            try
                            {
                                //if (i == 0)
                                //{
                                //    data = "S";
                                //}
                                //else
                                //{
                                data = dr[CommandParameters[i]];
                                //}
                            }
                            catch { data = DBNull.Value; }
                            cmd.Parameters.Add(new SqlParameter(CommandParameters[i], data));
                        }
                        //------------------------------
                        int result = cmd.ExecuteNonQuery();
                    }
                }

                if (ds.Tables["DT_DrillMaster"].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables["DT_DrillMaster"].Rows)
                    {
                        //-------------------------------
                        string[] CommandParameters = getCommandParameters(cmd, "DT_Import_DrillMaster");
                        cmd.Parameters.Clear();
                        cmd.CommandText = "DT_Import_DrillMaster";
                        cmd.CommandType = CommandType.StoredProcedure;
                        for (int i = 0; i <= CommandParameters.Length - 1; i++)
                        {
                            object data = DBNull.Value;
                            try
                            {
                                data = dr[CommandParameters[i]];
                            }
                            catch { data = DBNull.Value; }
                            cmd.Parameters.Add(new SqlParameter(CommandParameters[i], data));
                        }
                        //------------------------------
                        int result = cmd.ExecuteNonQuery();
                    }
                }

                if (ds.Tables["DT_MatrixMaster"].Rows.Count > 0)
                {
                    int MatrixId = Common.CastAsInt32(ds.Tables["DT_MatrixMaster"].Rows[0]["MatrixId"]);
                    //foreach (DataRow dr in ds.Tables["DT_MatrixMaster"].Rows)
                    {
                        //    //-------------------------------
                        string[] CommandParameters = getCommandParameters(cmd, "DT_Import_MatrixMaster");
                        cmd.Parameters.Clear();
                        cmd.CommandText = "DT_Import_MatrixMaster";
                        cmd.CommandType = CommandType.StoredProcedure;
                        for (int i = 0; i <= CommandParameters.Length - 1; i++)
                        {
                            object data = DBNull.Value;
                            try
                            {
                                data = ds.Tables["DT_MatrixMaster"].Rows[0][CommandParameters[i]];
                            }
                            catch { data = DBNull.Value; }
                            cmd.Parameters.Add(new SqlParameter(CommandParameters[i], data));
                        }
                        //------------------------------
                        int result = cmd.ExecuteNonQuery();
                    }

                    if (ds.Tables["DT_MatrixDetails"].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables["DT_MatrixDetails"].Rows)
                        {
                            //-------------------------------
                            string[] CommandParameters = getCommandParameters(cmd, "DT_Import_MatrixDetails");
                            cmd.Parameters.Clear();
                            cmd.CommandText = "DT_Import_MatrixDetails";
                            cmd.CommandType = CommandType.StoredProcedure;
                            for (int i = 0; i <= CommandParameters.Length - 1; i++)
                            {
                                object data = DBNull.Value;
                                try
                                {
                                    data = dr[CommandParameters[i]];
                                }
                                catch { data = DBNull.Value; }
                                cmd.Parameters.Add(new SqlParameter(CommandParameters[i], data));
                            }
                            //------------------------------
                            int result = cmd.ExecuteNonQuery();
                        }
                    }

                    cmd.CommandText = "DELETE FROM [dbo].[DT_MatrixDetailsAttachments] WHERE [MatrixId]=" + MatrixId;
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();

                    if (ds.Tables["DT_MatrixDetailsAttachments"].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables["DT_MatrixDetailsAttachments"].Rows)
                        {
                            //-------------------------------
                            string[] CommandParameters = getCommandParameters(cmd, "DT_Import_MatrixDetailsAttachments");
                            cmd.Parameters.Clear();
                            cmd.CommandText = "DT_Import_MatrixDetailsAttachments";
                            cmd.CommandType = CommandType.StoredProcedure;
                            for (int i = 0; i <= CommandParameters.Length - 1; i++)
                            {
                                object data = DBNull.Value;
                                try
                                {
                                    data = dr[CommandParameters[i]];
                                }
                                catch { data = DBNull.Value; }
                                cmd.Parameters.Add(new SqlParameter(CommandParameters[i], data));
                            }
                            //------------------------------
                            int result = cmd.ExecuteNonQuery();
                        }
                    }

                    cmd.Parameters.Clear();
                    cmd.CommandText = "DT_RESET_DRILL_TRAIINGS";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("VSLCODE", CurrentVessel));
                    cmd.Parameters.Add(new SqlParameter("MATRIXID", MatrixId));
                    cmd.ExecuteNonQuery();

                }
            }

            if (PacketType == "ACK")
            {

                if (ds.Tables["DT_VSL_DrillTrainingsHistory"].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables["DT_VSL_DrillTrainingsHistory"].Rows)
                    {
                        //-------------------------------
                        string[] CommandParameters = getCommandParameters(cmd, "DT_Import_OfficeAck");
                        cmd.Parameters.Clear();
                        cmd.CommandText = "DT_Import_OfficeAck";
                        cmd.CommandType = CommandType.StoredProcedure;
                        for (int i = 0; i <= CommandParameters.Length - 1; i++)
                        {
                            object data = DBNull.Value;
                            try
                            {
                                data = dr[CommandParameters[i]];
                            }
                            catch { data = DBNull.Value; }
                            cmd.Parameters.Add(new SqlParameter(CommandParameters[i], data));
                        }
                        //------------------------------
                        int result = cmd.ExecuteNonQuery();
                    }
                }

            }

            trans.Commit();
            ProjectCommon.ShowMessage("Imported successfully.");
        }
        catch (Exception ex)
        {
            trans.Rollback();
            ProjectCommon.ShowMessage("Unable to import. Error : " + ex.Message);
        }
        finally
        {
            if (Con.State == ConnectionState.Open) { Con.Close(); }
        }
    }
    //---------------- DRILL MAPPING IMPORT
    private void Import_Into_DRILL_MAP(DataSet ds, string FileToImport)
    {
        if (FileToImport != "")
        {
            SqlTransaction trans;
            SqlConnection Con = new SqlConnection(sqlConnectionString.Replace("Master", "eMANAGER"));
            SqlCommand cmdCheck = new SqlCommand("", Con);
            SqlDataAdapter Sda = new SqlDataAdapter(cmdCheck);
            Con.Open();
            trans = Con.BeginTransaction();
            ////------------------------ 
            try
            {
                SqlCommand cmd = new SqlCommand("", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = trans;
                //------------------------------------------------      
                cmd.CommandType = CommandType.StoredProcedure;
                //------------------------------------------------      
                if (ds.Tables["DT_VSL_DrillMaster"].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables["DT_VSL_DrillMaster"].Rows)
                    {
                        //-------------------------------
                        string[] CommandParameters = getCommandParameters(cmd, "DT_VSL_Import_VSL_DrillMaster");
                        cmd.Parameters.Clear();
                        cmd.CommandText = "DT_VSL_Import_VSL_DrillMaster";
                        cmd.CommandType = CommandType.StoredProcedure;
                        for (int i = 0; i <= CommandParameters.Length - 1; i++)
                        {
                            object data = DBNull.Value;
                            try
                            { data = dr[CommandParameters[i]]; }
                            catch { data = DBNull.Value; }
                            cmd.Parameters.Add(new SqlParameter(CommandParameters[i], data));
                        }
                        //------------------------------
                        int result = cmd.ExecuteNonQuery();
                    }
                }

                trans.Commit();
                lblMsg.Text = "Drill Vessel imported successfully. File Name : " + FileToImport;
            }
            catch (Exception ex)
            {
                trans.Rollback();
                lblMsg.Text = "Unable to import Drill Vessel. File Name : " + FileToImport + ". Error : " + ex.Message;
            }
            finally
            {
                if (Con.State == ConnectionState.Open) { Con.Close(); }
            }
        }
    }
    //---------------- TRAINING MAPPING IMPORT
    private void Import_Into_TRAINING_MAP(DataSet ds, string FileToImport)
    {
        if (FileToImport != "")
        {
            SqlTransaction trans;
            SqlConnection Con = new SqlConnection(sqlConnectionString.Replace("Master", "eMANAGER"));
            SqlCommand cmdCheck = new SqlCommand("", Con);
            SqlDataAdapter Sda = new SqlDataAdapter(cmdCheck);
            Con.Open();
            trans = Con.BeginTransaction();
            ////------------------------ 
            try
            {
                SqlCommand cmd = new SqlCommand("", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = trans;
                //------------------------------------------------      
                cmd.CommandType = CommandType.StoredProcedure;
                //------------------------------------------------      
                if (ds.Tables["DT_VSL_TrainingMaster"].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables["DT_VSL_TrainingMaster"].Rows)
                    {
                        //-------------------------------
                        string[] CommandParameters = getCommandParameters(cmd, "DT_VSL_Import_VSL_TrainingMaster");
                        cmd.Parameters.Clear();
                        cmd.CommandText = "DT_VSL_Import_VSL_TrainingMaster";
                        cmd.CommandType = CommandType.StoredProcedure;
                        for (int i = 0; i <= CommandParameters.Length - 1; i++)
                        {
                            object data = DBNull.Value;
                            try
                            { data = dr[CommandParameters[i]]; }
                            catch { data = DBNull.Value; }
                            cmd.Parameters.Add(new SqlParameter(CommandParameters[i], data));
                        }
                        //------------------------------
                        int result = cmd.ExecuteNonQuery();
                    }
                }

                trans.Commit();
                lblMsg.Text = "Training Vessel imported successfully. File Name : " + FileToImport;

            }
            catch (Exception ex)
            {
                trans.Rollback();
                lblMsg.Text = "Unable to import Training Vessel. File Name : " + FileToImport + ". Error : " + ex.Message;
            }
            finally
            {
                if (Con.State == ConnectionState.Open) { Con.Close(); }
            }
        }
    }
    //---------------- DRILL TRAINING PLANNER
    private void Import_Into_DrillTrainingPlanner(DataSet ds, string FileToImport)
    {
        if (FileToImport != "")
        {
            SqlTransaction trans;
            SqlConnection Con = new SqlConnection(sqlConnectionString.Replace("Master", "eMANAGER"));
            SqlCommand cmdCheck = new SqlCommand("", Con);
            SqlDataAdapter Sda = new SqlDataAdapter(cmdCheck);
            Con.Open();
            trans = Con.BeginTransaction();
            ////------------------------ 
            try
            {
                SqlCommand cmd = new SqlCommand("", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = trans;
                //------------------------------------------------      
                string FileNameOnly = Path.GetFileName(FileToImport);
                string VesselCode = Path.GetFileNameWithoutExtension(FileToImport);
                VesselCode = VesselCode.Substring(VesselCode.Length - 3);
                if (VesselCode!=CurrentVessel)
                {
                    trans.Rollback();
                    lblMsg.Text = "Invalid packet. Vessel not matching. File Name : " + FileToImport;
                    return;
                }

                //int Year=Common.CastAsInt32(FileNameOnly.Substring(21,4));
                //if (Year <= 0)
                //{
                //    trans.Rollback();
                //    lblMsg.Text = "Invalid packet. File Name : " + FileToImport;
                //    return;
                //}

                if (ds.Tables.Contains("DT_DrillMatrixPlanner"))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "DELETE FROM DT_DrillMatrixPlanner WHERE VESSELCODE='" + CurrentVessel + "' ";
                    cmd.ExecuteNonQuery();
                    //------------------------------------------------      
                    cmd.CommandType = CommandType.StoredProcedure;
                    //------------------------------------------------      
                    if (ds.Tables["DT_DrillMatrixPlanner"].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables["DT_DrillMatrixPlanner"].Rows)
                        {
                            //-------------------------------
                            string[] CommandParameters = getCommandParameters(cmd, "DT_VSL_Import_DT_DrillMatrixPlanner");
                            cmd.Parameters.Clear();
                            cmd.CommandText = "DT_VSL_Import_DT_DrillMatrixPlanner";
                            cmd.CommandType = CommandType.StoredProcedure;
                            for (int i = 0; i <= CommandParameters.Length - 1; i++)
                            {
                                object data = DBNull.Value;
                                try
                                { data = dr[CommandParameters[i]]; }
                                catch { data = DBNull.Value; }
                                cmd.Parameters.Add(new SqlParameter(CommandParameters[i], data));
                            }
                            //------------------------------
                            int result = cmd.ExecuteNonQuery();
                        }
                    }
                }
                if (ds.Tables.Contains("DT_DrillMatrixPlanner_Weekly"))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "DELETE FROM DT_DrillMatrixPlanner_Weekly WHERE VESSELCODE='" + CurrentVessel + "' ";
                    cmd.ExecuteNonQuery();
                    //------------------------------------------------      
                    cmd.CommandType = CommandType.StoredProcedure;
                    //------------------------------------------------      
                    if (ds.Tables["DT_DrillMatrixPlanner_Weekly"].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables["DT_DrillMatrixPlanner_Weekly"].Rows)
                        {
                            //-------------------------------
                            string[] CommandParameters = getCommandParameters(cmd, "DT_VSL_Import_DT_DrillMatrixPlanner_Weekly");
                            cmd.Parameters.Clear();
                            cmd.CommandText = "DT_VSL_Import_DT_DrillMatrixPlanner_Weekly";
                            cmd.CommandType = CommandType.StoredProcedure;
                            for (int i = 0; i <= CommandParameters.Length - 1; i++)
                            {
                                object data = DBNull.Value;
                                try
                                { data = dr[CommandParameters[i]]; }
                                catch { data = DBNull.Value; }
                                cmd.Parameters.Add(new SqlParameter(CommandParameters[i], data));
                            }
                            //------------------------------
                            int result = cmd.ExecuteNonQuery();
                        }
                    }
                }
                trans.Commit();
                lblMsg.Text = "Planner imported successfully. File Name : " + FileToImport;

            }
            catch (Exception ex)
            {
                trans.Rollback();
                lblMsg.Text = "Unable to import Planner. File Name : " + FileToImport + ". Error : " + ex.Message;
            }
            finally
            {
                if (Con.State == ConnectionState.Open) { Con.Close(); }
            }
        }
    }
    
    //---------------- PMS PACKET IMPORT
    private void PMS_Import(DataSet ds, string FileToImport)
    {
        string Imp_VesselCode = "";
        string PacketName = "", LastPacketAck = "";
        
        Imp_VesselCode = Path.GetFileName(FileToImport).Substring(0, 3);
        int intPacketNo = Convert.ToInt32(Path.GetFileName(FileToImport).Substring(14, 5));

        string strConectionString = sqlConnectionString.Replace("Master", "eMANAGER");

        SqlTransaction trans;
        SqlConnection Con = new SqlConnection(strConectionString.Replace("Master", "eMANAGER"));
        Con.Open();
        trans = Con.BeginTransaction();
     
        ResetNULLDates(ref ds);
        try
        {
            SqlCommand cmd = new SqlCommand("", Con);
            cmd.CommandType = CommandType.Text;
            cmd.Transaction = trans;
            cmd.CommandTimeout = 600;
            //---------------- CHECKING PACKET RECEIVED NO. IS GREATER THAN DB PACKET RECEIVED
            cmd.CommandText = "SELECT COUNT(*) FROM dbo.IE_PacketReceived WHERE VesselCode ='" + Imp_VesselCode + "' AND CAST( SUBSTRING(PacketName,15,5) AS INT) >= " + intPacketNo.ToString();
            if (Convert.ToInt32(cmd.ExecuteScalar()) > 0)
            {
                lblMsg.Text = "Please Check. Heigher or Same version packet is already updated in the database.";
                return;

            }
            int PacketVersion = 1;
            try
            {
                PacketVersion = int.Parse(ds.Tables["PACKETDETAILS"].Rows[0]["Version"].ToString());
            }
            catch { }
            //-------------------------
            cmd.CommandType = CommandType.StoredProcedure;
            if (PacketVersion == 1)
            {
                if (ds.Tables.Count != 11)
                {
                    lblMsg.Text = "Please check. Packet data is invalid.";
                    return;
                }
            }
            else if (PacketVersion == 2)
            {
                if (ds.Tables.Count != 15)
                {
                    lblMsg.Text = "Please check. Packet data is invalid.";
                    return;
                }
            }
            else if (PacketVersion == 3)
            {
                if (ds.Tables.Count != 16)
                {
                    lblMsg.Text = "Please check. Packet data is invalid.";
                    return;
                }
            }
            else
            {
                lblMsg.Text = "Please check. Packet data is invalid. Invalid packet version.";
                return;
            }

            PacketName = ds.Tables["PACKETDETAILS"].Rows[0]["PacketName"].ToString().Trim();
            LastPacketAck = ds.Tables["PACKETDETAILS"].Rows[0]["LASTPACKETACK"].ToString().Trim();
            //-------------------------
            if (PacketName != Path.GetFileNameWithoutExtension(FileToImport).Trim())
            {
                lblMsg.Text = "Please check. Packet is tempared.";
                return;
            }



            //------------------------ IMPORTING AT SHIP
            if (PacketVersion == 1)
            {
                # region -------------- VERSION 1 ----------------
                if (ds.Tables["ComponentMaster"].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables["ComponentMaster"].Rows)
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandText = "sp_IE_ComponentMaster";

                        cmd.Parameters.Add(MyParameter("COMPONENTID", SqlDbType.BigInt, dr[0]));
                        cmd.Parameters.Add(MyParameter("COMPONENTCODE", SqlDbType.VarChar, dr[1]));
                        cmd.Parameters.Add(MyParameter("COMPONENTNAME", SqlDbType.VarChar, dr[2]));
                        cmd.Parameters.Add(MyParameter("DESCR", SqlDbType.VarChar, dr[3]));
                        cmd.Parameters.Add(MyParameter("CLASSEQUIP", SqlDbType.Bit, dr[4]));
                        cmd.Parameters.Add(MyParameter("CRITICALEQUIP", SqlDbType.Bit, dr[5]));
                        cmd.Parameters.Add(MyParameter("INACTIVE", SqlDbType.Bit, dr[6]));
                        cmd.Parameters.Add(MyParameter("CriticalType", SqlDbType.VarChar, dr[7]));

                        int result = cmd.ExecuteNonQuery();
                    }
                }
                if (ds.Tables["VSL_ComponentMasterForVessel"].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables["VSL_ComponentMasterForVessel"].Rows)
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandText = "sp_IE_VSL_ComponentMasterForVessel";

                        cmd.Parameters.Add(MyParameter("VESSELCODE", SqlDbType.VarChar, dr[0]));
                        cmd.Parameters.Add(MyParameter("COMPONENTID", SqlDbType.BigInt, dr[1]));
                        cmd.Parameters.Add(MyParameter("MAKER", SqlDbType.VarChar, dr[2]));
                        cmd.Parameters.Add(MyParameter("MAKERTYPE", SqlDbType.VarChar, dr[3]));
                        cmd.Parameters.Add(MyParameter("CLASSEQUIPCODE", SqlDbType.VarChar, dr[4]));
                        cmd.Parameters.Add(MyParameter("STATUS", SqlDbType.VarChar, dr[5]));
                        cmd.Parameters.Add(MyParameter("UPDATED", SqlDbType.Bit, dr[6]));
                        cmd.Parameters.Add(MyParameter("UPDATEDON", SqlDbType.SmallDateTime, dr[7]));
                        cmd.Parameters.Add(MyParameter("DESCR", SqlDbType.VarChar, dr[8]));
                        cmd.Parameters.Add(MyParameter("CLASSEQUIP", SqlDbType.Bit, dr[9]));
                        cmd.Parameters.Add(MyParameter("ACCOUNTCODES", SqlDbType.VarChar, dr[10]));

                        int result = cmd.ExecuteNonQuery();
                    }
                }
                if (ds.Tables["JobMaster"].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables["JobMaster"].Rows)
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandText = "sp_IE_JobMaster";

                        cmd.Parameters.Add(MyParameter("JOBID", SqlDbType.BigInt, dr[0]));
                        cmd.Parameters.Add(MyParameter("JOBCODE", SqlDbType.VarChar, dr[1]));
                        cmd.Parameters.Add(MyParameter("JOBNAME", SqlDbType.VarChar, dr[2]));

                        int result = cmd.ExecuteNonQuery();
                    }
                }
                if (ds.Tables["ComponentsJobMapping"].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables["ComponentsJobMapping"].Rows)
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandText = "sp_IE_ComponentsJobMapping";

                        cmd.Parameters.Add(MyParameter("COMPONENTID", SqlDbType.BigInt, dr[0]));
                        cmd.Parameters.Add(MyParameter("JOBID", SqlDbType.BigInt, dr[1]));
                        cmd.Parameters.Add(MyParameter("COMPJOBID", SqlDbType.BigInt, dr[2]));
                        cmd.Parameters.Add(MyParameter("ASSIGNTO", SqlDbType.Int, dr[3]));
                        cmd.Parameters.Add(MyParameter("DEPTID", SqlDbType.Int, dr[4]));
                        cmd.Parameters.Add(MyParameter("INTERVALID", SqlDbType.Int, dr[5]));
                        cmd.Parameters.Add(MyParameter("ISFIXED", SqlDbType.SmallInt, dr[6]));
                        cmd.Parameters.Add(MyParameter("DESCRSH", SqlDbType.VarChar, dr[7]));
                        cmd.Parameters.Add(MyParameter("DESCRM", SqlDbType.VarChar, dr[8]));
                        cmd.Parameters.Add(MyParameter("AttachmentForm", SqlDbType.VarChar, dr[9]));
                        cmd.Parameters.Add(MyParameter("RiskAssessment", SqlDbType.VarChar, dr[10]));
                        cmd.Parameters.Add(MyParameter("RatingRequired", SqlDbType.Bit, dr[11]));

                        int result = cmd.ExecuteNonQuery();
                    }
                }
                if (ds.Tables["VSL_VesselComponentJobMaster"].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables["VSL_VesselComponentJobMaster"].Rows)
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandText = "sp_IE_VSL_VesselComponentJobMaster";

                        cmd.Parameters.Add(MyParameter("VESSELCODE", SqlDbType.VarChar, dr[0]));
                        cmd.Parameters.Add(MyParameter("COMPJOBID", SqlDbType.BigInt, dr[1]));
                        cmd.Parameters.Add(MyParameter("COMPONENTID", SqlDbType.BigInt, dr[2]));
                        cmd.Parameters.Add(MyParameter("JOBID", SqlDbType.BigInt, dr[3]));
                        cmd.Parameters.Add(MyParameter("ASSIGNTO", SqlDbType.Int, dr[4]));
                        cmd.Parameters.Add(MyParameter("INTERVALID", SqlDbType.Int, dr[5]));
                        cmd.Parameters.Add(MyParameter("INTERVAL", SqlDbType.Float, dr[6]));
                        cmd.Parameters.Add(MyParameter("ISFIXED", SqlDbType.SmallInt, dr[7]));
                        cmd.Parameters.Add(MyParameter("STARTDATE", SqlDbType.SmallDateTime, dr[8]));
                        cmd.Parameters.Add(MyParameter("DESCRM", SqlDbType.VarChar, dr[9]));
                        cmd.Parameters.Add(MyParameter("INTERVAL_H", SqlDbType.Float, dr[10]));
                        cmd.Parameters.Add(MyParameter("INTERVALID_H", SqlDbType.Int, dr[11]));
                        cmd.Parameters.Add(MyParameter("Guidelines", SqlDbType.VarChar, dr[12]));
                        cmd.Parameters.Add(MyParameter("STATUS", SqlDbType.VarChar, dr[13]));
                        cmd.Parameters.Add(MyParameter("UPDATED", SqlDbType.Bit, dr[14]));
                        cmd.Parameters.Add(MyParameter("UPDATEDON", SqlDbType.SmallDateTime, dr[15]));

                        int result = cmd.ExecuteNonQuery();
                    }
                }
                if (ds.Tables["VSL_VesselComponentJobMaster_Updates"].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables["VSL_VesselComponentJobMaster_Updates"].Rows)
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandText = "sp_IE_VSL_VesselComponentJobMaster_Updates";

                        cmd.Parameters.Add(MyParameter("LOCATION", SqlDbType.VarChar, "S"));
                        cmd.Parameters.Add(MyParameter("VESSELCODE", SqlDbType.VarChar, dr[0]));
                        cmd.Parameters.Add(MyParameter("COMPJOBID", SqlDbType.BigInt, dr[1]));
                        cmd.Parameters.Add(MyParameter("COMPONENTID", SqlDbType.BigInt, dr[2]));
                        cmd.Parameters.Add(MyParameter("LASTDONE", SqlDbType.SmallDateTime, dr[3]));
                        cmd.Parameters.Add(MyParameter("LASTHOUR", SqlDbType.Int, dr[4]));
                        cmd.Parameters.Add(MyParameter("NEXTDUEDATE", SqlDbType.SmallDateTime, dr[5]));
                        cmd.Parameters.Add(MyParameter("NEXTHOUR", SqlDbType.Int, dr[6]));
                        cmd.Parameters.Add(MyParameter("UPDATED", SqlDbType.Bit, dr[7]));
                        cmd.Parameters.Add(MyParameter("UPDATEDON", SqlDbType.SmallDateTime, dr[8]));

                        int result = cmd.ExecuteNonQuery();
                    }
                }
                if (ds.Tables["VSL_VesselComponentJobMaster_Attachments"].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables["VSL_VesselComponentJobMaster_Attachments"].Rows)
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandText = "sp_IE_VSL_VesselComponentJobMaster_Attachments";

                        cmd.Parameters.Add(MyParameter("VESSELCODE", SqlDbType.VarChar, dr[0]));
                        cmd.Parameters.Add(MyParameter("TABLEID", SqlDbType.BigInt, dr[1]));
                        cmd.Parameters.Add(MyParameter("COMPJOBID", SqlDbType.BigInt, dr[2]));
                        cmd.Parameters.Add(MyParameter("DESCR", SqlDbType.VarChar, dr[3]));
                        cmd.Parameters.Add(MyParameter("DOCUMENTTYPE", SqlDbType.VarChar, dr[4]));
                        cmd.Parameters.Add(MyParameter("STATUS", SqlDbType.VarChar, dr[5]));

                        int result = cmd.ExecuteNonQuery();
                    }
                }
                if (ds.Tables["ComponentsJobMapping_Attachments"].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables["ComponentsJobMapping_Attachments"].Rows)
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandText = "sp_IE_ComponentsJobMapping_Attachments";

                        cmd.Parameters.Add(MyParameter("TABLEID", SqlDbType.BigInt, dr[0]));
                        cmd.Parameters.Add(MyParameter("COMPJOBID", SqlDbType.BigInt, dr[1]));
                        cmd.Parameters.Add(MyParameter("DESCR", SqlDbType.VarChar, dr[2]));
                        cmd.Parameters.Add(MyParameter("DOCUMENTTYPE", SqlDbType.VarChar, dr[3]));
                        cmd.Parameters.Add(MyParameter("STATUS", SqlDbType.VarChar, dr[4]));

                        int result = cmd.ExecuteNonQuery();
                    }
                }
                if (ds.Tables["VSL_VesselJobUpdateHistory_OfficeComments"].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables["VSL_VesselJobUpdateHistory_OfficeComments"].Rows)
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandText = "sp_IE_VSL_VesselJobUpdateHistory_OfficeComments";

                        cmd.Parameters.Add(MyParameter("VESSELCODE", SqlDbType.VarChar, dr[0]));
                        cmd.Parameters.Add(MyParameter("HISTORYID", SqlDbType.BigInt, dr[1]));
                        cmd.Parameters.Add(MyParameter("COMMENTS", SqlDbType.VarChar, dr[2]));
                        cmd.Parameters.Add(MyParameter("VERIFIED", SqlDbType.Bit, dr[3]));
                        cmd.Parameters.Add(MyParameter("VERIFIEDBY", SqlDbType.VarChar, dr[4]));
                        cmd.Parameters.Add(MyParameter("VERIFIEDON", SqlDbType.SmallDateTime, dr[5]));
                        cmd.Parameters.Add(MyParameter("UPDATED", SqlDbType.Bit, dr[6]));
                        cmd.Parameters.Add(MyParameter("UPDATEDON", SqlDbType.SmallDateTime, dr[7]));

                        int result = cmd.ExecuteNonQuery();
                    }
                }

                if (ds.Tables["VSL_VesselSpareMaster"].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables["VSL_VesselSpareMaster"].Rows)
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandText = "sp_IE_VSL_VesselSpareMaster";

                        cmd.Parameters.Add(MyParameter("VESSELCODE", SqlDbType.VarChar, dr[0]));
                        cmd.Parameters.Add(MyParameter("COMPONENTID", SqlDbType.BigInt, dr[1]));
                        cmd.Parameters.Add(MyParameter("OFFICE_SHIP", SqlDbType.VarChar, dr[2]));
                        cmd.Parameters.Add(MyParameter("SPAREID", SqlDbType.BigInt, dr[3]));
                        cmd.Parameters.Add(MyParameter("SPARENAME", SqlDbType.VarChar, dr[4]));
                        cmd.Parameters.Add(MyParameter("MAKER", SqlDbType.VarChar, dr[5]));
                        cmd.Parameters.Add(MyParameter("MAKERTYPE", SqlDbType.VarChar, dr[6]));
                        cmd.Parameters.Add(MyParameter("PARTNO", SqlDbType.VarChar, dr[7]));
                        cmd.Parameters.Add(MyParameter("ALTPARTNO", SqlDbType.VarChar, dr[8]));
                        cmd.Parameters.Add(MyParameter("LOCATION", SqlDbType.VarChar, dr[9]));
                        cmd.Parameters.Add(MyParameter("MINQTY", SqlDbType.Int, dr[10]));
                        cmd.Parameters.Add(MyParameter("MAXQTY", SqlDbType.Int, dr[11]));
                        cmd.Parameters.Add(MyParameter("STATUTORYQTY", SqlDbType.Int, dr[12]));
                        cmd.Parameters.Add(MyParameter("WEIGHT", SqlDbType.Float, dr[13]));
                        cmd.Parameters.Add(MyParameter("STOCK", SqlDbType.Int, dr[14]));
                        cmd.Parameters.Add(MyParameter("SPECIFICATION", SqlDbType.VarChar, dr[15]));
                        cmd.Parameters.Add(MyParameter("DRAWINGNO", SqlDbType.VarChar, dr[16]));
                        cmd.Parameters.Add(MyParameter("Attachment", SqlDbType.VarChar, dr[17]));
                        cmd.Parameters.Add(MyParameter("STATUS", SqlDbType.VarChar, dr[18]));
                        cmd.Parameters.Add(MyParameter("UPDATED", SqlDbType.Bit, dr[19]));
                        cmd.Parameters.Add(MyParameter("UPDATEDON", SqlDbType.SmallDateTime, dr[20]));
                        cmd.Parameters.Add(MyParameter("Critical", SqlDbType.VarChar, dr[21]));
                        cmd.Parameters.Add(MyParameter("CriticalUpdatedBy", SqlDbType.VarChar, dr[22]));
                        cmd.Parameters.Add(MyParameter("CriticalUpdatedByOn", SqlDbType.SmallDateTime, dr[23]));
                        cmd.Parameters.Add(MyParameter("isOR", SqlDbType.Bit, dr[24]));
                        cmd.Parameters.Add(MyParameter("PartName", SqlDbType.VarChar, dr[25]));
                        cmd.Parameters.Add(MyParameter("StockLocationId", SqlDbType.Int, dr[26]));
                        int result = cmd.ExecuteNonQuery();
                    }
                }
                #endregion
            }
            else if (PacketVersion == 2)
            {
                # region -------------- VERSION 2 ----------------
                if (ds.Tables["ComponentMaster"].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables["ComponentMaster"].Rows)
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandText = "sp_IE_ComponentMaster";

                        cmd.Parameters.Add(MyParameter("COMPONENTID", SqlDbType.BigInt, dr[0]));
                        cmd.Parameters.Add(MyParameter("COMPONENTCODE", SqlDbType.VarChar, dr[1]));
                        cmd.Parameters.Add(MyParameter("COMPONENTNAME", SqlDbType.VarChar, dr[2]));
                        cmd.Parameters.Add(MyParameter("DESCR", SqlDbType.VarChar, dr[3]));
                        cmd.Parameters.Add(MyParameter("CLASSEQUIP", SqlDbType.Bit, dr[4]));
                        cmd.Parameters.Add(MyParameter("CRITICALEQUIP", SqlDbType.Bit, dr[5]));
                        cmd.Parameters.Add(MyParameter("INACTIVE", SqlDbType.Bit, dr[6]));
                        cmd.Parameters.Add(MyParameter("CriticalType", SqlDbType.VarChar, dr[7]));

                        int result = cmd.ExecuteNonQuery();
                    }
                }
                if (ds.Tables["VSL_ComponentMasterForVessel"].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables["VSL_ComponentMasterForVessel"].Rows)
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandText = "sp_IE_VSL_ComponentMasterForVessel";

                        cmd.Parameters.Add(MyParameter("VESSELCODE", SqlDbType.VarChar, dr[0]));
                        cmd.Parameters.Add(MyParameter("COMPONENTID", SqlDbType.BigInt, dr[1]));
                        cmd.Parameters.Add(MyParameter("MAKER", SqlDbType.VarChar, dr[2]));
                        cmd.Parameters.Add(MyParameter("MAKERTYPE", SqlDbType.VarChar, dr[3]));
                        cmd.Parameters.Add(MyParameter("CLASSEQUIPCODE", SqlDbType.VarChar, dr[4]));
                        cmd.Parameters.Add(MyParameter("STATUS", SqlDbType.VarChar, dr[5]));
                        cmd.Parameters.Add(MyParameter("UPDATED", SqlDbType.Bit, dr[6]));
                        cmd.Parameters.Add(MyParameter("UPDATEDON", SqlDbType.SmallDateTime, dr[7]));
                        cmd.Parameters.Add(MyParameter("DESCR", SqlDbType.VarChar, dr[8]));
                        cmd.Parameters.Add(MyParameter("CLASSEQUIP", SqlDbType.Bit, dr[9]));
                        cmd.Parameters.Add(MyParameter("ACCOUNTCODES", SqlDbType.VarChar, dr[10]));
			cmd.Parameters.Add(MyParameter("isOR", SqlDbType.Bit, dr[11]));
                        int result = cmd.ExecuteNonQuery();
                    }
                }
                if (ds.Tables["JobMaster"].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables["JobMaster"].Rows)
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandText = "sp_IE_JobMaster";

                        cmd.Parameters.Add(MyParameter("JOBID", SqlDbType.BigInt, dr[0]));
                        cmd.Parameters.Add(MyParameter("JOBCODE", SqlDbType.VarChar, dr[1]));
                        cmd.Parameters.Add(MyParameter("JOBNAME", SqlDbType.VarChar, dr[2]));

                        int result = cmd.ExecuteNonQuery();
                    }
                }
                if (ds.Tables["ComponentsJobMapping"].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables["ComponentsJobMapping"].Rows)
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandText = "sp_IE_ComponentsJobMapping";

                        cmd.Parameters.Add(MyParameter("COMPONENTID", SqlDbType.BigInt, dr[0]));
                        cmd.Parameters.Add(MyParameter("JOBID", SqlDbType.BigInt, dr[1]));
                        cmd.Parameters.Add(MyParameter("COMPJOBID", SqlDbType.BigInt, dr[2]));
                        cmd.Parameters.Add(MyParameter("ASSIGNTO", SqlDbType.Int, dr[3]));
                        cmd.Parameters.Add(MyParameter("DEPTID", SqlDbType.Int, dr[4]));
                        cmd.Parameters.Add(MyParameter("INTERVALID", SqlDbType.Int, dr[5]));
                        cmd.Parameters.Add(MyParameter("ISFIXED", SqlDbType.SmallInt, dr[6]));
                        cmd.Parameters.Add(MyParameter("DESCRSH", SqlDbType.VarChar, dr[7]));
                        cmd.Parameters.Add(MyParameter("DESCRM", SqlDbType.VarChar, dr[8]));
                        cmd.Parameters.Add(MyParameter("AttachmentForm", SqlDbType.VarChar, dr[9]));
                        cmd.Parameters.Add(MyParameter("RiskAssessment", SqlDbType.VarChar, dr[10]));
                        cmd.Parameters.Add(MyParameter("RatingRequired", SqlDbType.Bit, dr[11]));

                        int result = cmd.ExecuteNonQuery();
                    }
                }
                if (ds.Tables["VSL_VesselComponentJobMaster"].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables["VSL_VesselComponentJobMaster"].Rows)
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandText = "sp_IE_VSL_VesselComponentJobMaster";

                        cmd.Parameters.Add(MyParameter("VESSELCODE", SqlDbType.VarChar, dr[0]));
                        cmd.Parameters.Add(MyParameter("COMPJOBID", SqlDbType.BigInt, dr[1]));
                        cmd.Parameters.Add(MyParameter("COMPONENTID", SqlDbType.BigInt, dr[2]));
                        cmd.Parameters.Add(MyParameter("JOBID", SqlDbType.BigInt, dr[3]));
                        cmd.Parameters.Add(MyParameter("ASSIGNTO", SqlDbType.Int, dr[4]));
                        cmd.Parameters.Add(MyParameter("INTERVALID", SqlDbType.Int, dr[5]));
                        cmd.Parameters.Add(MyParameter("INTERVAL", SqlDbType.Float, dr[6]));
                        cmd.Parameters.Add(MyParameter("ISFIXED", SqlDbType.SmallInt, dr[7]));
                        cmd.Parameters.Add(MyParameter("STARTDATE", SqlDbType.SmallDateTime, dr[8]));
                        cmd.Parameters.Add(MyParameter("DESCRM", SqlDbType.VarChar, dr[9]));
                        cmd.Parameters.Add(MyParameter("INTERVAL_H", SqlDbType.Float, dr[10]));
                        cmd.Parameters.Add(MyParameter("INTERVALID_H", SqlDbType.Int, dr[11]));
                        cmd.Parameters.Add(MyParameter("Guidelines", SqlDbType.VarChar, dr[12]));
                        cmd.Parameters.Add(MyParameter("STATUS", SqlDbType.VarChar, dr[13]));
                        cmd.Parameters.Add(MyParameter("UPDATED", SqlDbType.Bit, dr[14]));
                        cmd.Parameters.Add(MyParameter("UPDATEDON", SqlDbType.SmallDateTime, dr[15]));

                        int result = cmd.ExecuteNonQuery();
                    }
                }
                if (ds.Tables["VSL_VesselComponentJobMaster_Updates"].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables["VSL_VesselComponentJobMaster_Updates"].Rows)
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandText = "sp_IE_VSL_VesselComponentJobMaster_Updates";

                        cmd.Parameters.Add(MyParameter("LOCATION", SqlDbType.VarChar, "S"));
                        cmd.Parameters.Add(MyParameter("VESSELCODE", SqlDbType.VarChar, dr[0]));
                        cmd.Parameters.Add(MyParameter("COMPJOBID", SqlDbType.BigInt, dr[1]));
                        cmd.Parameters.Add(MyParameter("COMPONENTID", SqlDbType.BigInt, dr[2]));
                        cmd.Parameters.Add(MyParameter("LASTDONE", SqlDbType.SmallDateTime, dr[3]));
                        cmd.Parameters.Add(MyParameter("LASTHOUR", SqlDbType.Int, dr[4]));
                        cmd.Parameters.Add(MyParameter("NEXTDUEDATE", SqlDbType.SmallDateTime, dr[5]));
                        cmd.Parameters.Add(MyParameter("NEXTHOUR", SqlDbType.Int, dr[6]));
                        cmd.Parameters.Add(MyParameter("UPDATED", SqlDbType.Bit, dr[7]));
                        cmd.Parameters.Add(MyParameter("UPDATEDON", SqlDbType.SmallDateTime, dr[8]));

                        int result = cmd.ExecuteNonQuery();
                    }
                }
                if (ds.Tables["VSL_VesselComponentJobMaster_Attachments"].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables["VSL_VesselComponentJobMaster_Attachments"].Rows)
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandText = "sp_IE_VSL_VesselComponentJobMaster_Attachments";

                        cmd.Parameters.Add(MyParameter("VESSELCODE", SqlDbType.VarChar, dr[0]));
                        cmd.Parameters.Add(MyParameter("TABLEID", SqlDbType.BigInt, dr[1]));
                        cmd.Parameters.Add(MyParameter("COMPJOBID", SqlDbType.BigInt, dr[2]));
                        cmd.Parameters.Add(MyParameter("DESCR", SqlDbType.VarChar, dr[3]));
                        cmd.Parameters.Add(MyParameter("DOCUMENTTYPE", SqlDbType.VarChar, dr[4]));
                        cmd.Parameters.Add(MyParameter("STATUS", SqlDbType.VarChar, dr[5]));

                        int result = cmd.ExecuteNonQuery();
                    }
                }
                if (ds.Tables["ComponentsJobMapping_Attachments"].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables["ComponentsJobMapping_Attachments"].Rows)
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandText = "sp_IE_ComponentsJobMapping_Attachments";

                        cmd.Parameters.Add(MyParameter("TABLEID", SqlDbType.BigInt, dr[0]));
                        cmd.Parameters.Add(MyParameter("COMPJOBID", SqlDbType.BigInt, dr[1]));
                        cmd.Parameters.Add(MyParameter("DESCR", SqlDbType.VarChar, dr[2]));
                        cmd.Parameters.Add(MyParameter("DOCUMENTTYPE", SqlDbType.VarChar, dr[3]));
                        cmd.Parameters.Add(MyParameter("STATUS", SqlDbType.VarChar, dr[4]));

                        int result = cmd.ExecuteNonQuery();
                    }
                }
                if (ds.Tables["VSL_VesselJobUpdateHistory_OfficeComments"].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables["VSL_VesselJobUpdateHistory_OfficeComments"].Rows)
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandText = "sp_IE_VSL_VesselJobUpdateHistory_OfficeComments";

                        cmd.Parameters.Add(MyParameter("VESSELCODE", SqlDbType.VarChar, dr[0]));
                        cmd.Parameters.Add(MyParameter("HISTORYID", SqlDbType.BigInt, dr[1]));
                        cmd.Parameters.Add(MyParameter("COMMENTS", SqlDbType.VarChar, dr[2]));
                        cmd.Parameters.Add(MyParameter("VERIFIED", SqlDbType.Bit, dr[3]));
                        cmd.Parameters.Add(MyParameter("VERIFIEDBY", SqlDbType.VarChar, dr[4]));
                        cmd.Parameters.Add(MyParameter("VERIFIEDON", SqlDbType.SmallDateTime, dr[5]));
                        cmd.Parameters.Add(MyParameter("UPDATED", SqlDbType.Bit, dr[6]));
                        cmd.Parameters.Add(MyParameter("UPDATEDON", SqlDbType.SmallDateTime, dr[7]));

                        int result = cmd.ExecuteNonQuery();
                    }
                }

                if (ds.Tables["VSL_VesselSpareMaster"].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables["VSL_VesselSpareMaster"].Rows)
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandText = "sp_IE_VSL_VesselSpareMaster";

                        cmd.Parameters.Add(MyParameter("VESSELCODE", SqlDbType.VarChar, dr[0]));
                        cmd.Parameters.Add(MyParameter("COMPONENTID", SqlDbType.BigInt, dr[1]));
                        cmd.Parameters.Add(MyParameter("OFFICE_SHIP", SqlDbType.VarChar, dr[2]));
                        cmd.Parameters.Add(MyParameter("SPAREID", SqlDbType.BigInt, dr[3]));
                        cmd.Parameters.Add(MyParameter("SPARENAME", SqlDbType.VarChar, dr[4]));
                        cmd.Parameters.Add(MyParameter("MAKER", SqlDbType.VarChar, dr[5]));
                        cmd.Parameters.Add(MyParameter("MAKERTYPE", SqlDbType.VarChar, dr[6]));
                        cmd.Parameters.Add(MyParameter("PARTNO", SqlDbType.VarChar, dr[7]));
                        cmd.Parameters.Add(MyParameter("ALTPARTNO", SqlDbType.VarChar, dr[8]));
                        cmd.Parameters.Add(MyParameter("LOCATION", SqlDbType.VarChar, dr[9]));
                        cmd.Parameters.Add(MyParameter("MINQTY", SqlDbType.Int, dr[10]));
                        cmd.Parameters.Add(MyParameter("MAXQTY", SqlDbType.Int, dr[11]));
                        cmd.Parameters.Add(MyParameter("STATUTORYQTY", SqlDbType.Int, dr[12]));
                        cmd.Parameters.Add(MyParameter("WEIGHT", SqlDbType.Float, dr[13]));
                        cmd.Parameters.Add(MyParameter("STOCK", SqlDbType.Int, dr[14]));
                        cmd.Parameters.Add(MyParameter("SPECIFICATION", SqlDbType.VarChar, dr[15]));
                        cmd.Parameters.Add(MyParameter("DRAWINGNO", SqlDbType.VarChar, dr[16]));
                        cmd.Parameters.Add(MyParameter("Attachment", SqlDbType.VarChar, dr[17]));
                        cmd.Parameters.Add(MyParameter("STATUS", SqlDbType.VarChar, dr[18]));
                        cmd.Parameters.Add(MyParameter("UPDATED", SqlDbType.Bit, dr[19]));
                        cmd.Parameters.Add(MyParameter("UPDATEDON", SqlDbType.SmallDateTime, dr[20]));
                        cmd.Parameters.Add(MyParameter("Critical", SqlDbType.VarChar, dr[21]));
                        cmd.Parameters.Add(MyParameter("CriticalUpdatedBy", SqlDbType.VarChar, dr[22]));
                        cmd.Parameters.Add(MyParameter("CriticalUpdatedByOn", SqlDbType.SmallDateTime, dr[23]));
			            cmd.Parameters.Add(MyParameter("isOR", SqlDbType.Bit, dr[24]));
                        cmd.Parameters.Add(MyParameter("PartName", SqlDbType.VarChar, dr[25]));
                        cmd.Parameters.Add(MyParameter("StockLocationId", SqlDbType.Int, dr[26]));
                        int result = cmd.ExecuteNonQuery();
                    }
                }

                if (ds.Tables["VSL_DefectRemarks"].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables["VSL_DefectRemarks"].Rows)
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandText = "sp_IE_VSL_DefectRemarks";

                        cmd.Parameters.Add(MyParameter("DefectRemarkId", SqlDbType.BigInt, dr[0]));
                        cmd.Parameters.Add(MyParameter("VESSELCODE", SqlDbType.VarChar, dr[1]));
                        cmd.Parameters.Add(MyParameter("DefectNo", SqlDbType.VarChar, dr[2]));
                        cmd.Parameters.Add(MyParameter("Remarks", SqlDbType.VarChar, dr[3]));
                        cmd.Parameters.Add(MyParameter("FileName", SqlDbType.VarChar, dr[4]));
                        cmd.Parameters.Add(MyParameter("File", SqlDbType.Image, dr[5]));
                        cmd.Parameters.Add(MyParameter("UPDATED", SqlDbType.Bit, dr[6]));
                        cmd.Parameters.Add(MyParameter("UPDATEDON", SqlDbType.SmallDateTime, dr[7]));
                        cmd.Parameters.Add(MyParameter("EnteredBy", SqlDbType.VarChar, dr[8]));
                        cmd.Parameters.Add(MyParameter("EnteredOn", SqlDbType.SmallDateTime, dr[9]));

                        int result = cmd.ExecuteNonQuery();
                    }
                }
                if (ds.Tables["VSL_CriticalShutdownApproval"].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables["VSL_CriticalShutdownApproval"].Rows)
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandText = "sp_IE_VSL_CriticalShutdownApproval";

                        cmd.Parameters.Add(MyParameter("ShutdownApproveId", SqlDbType.BigInt, dr[0]));
                        cmd.Parameters.Add(MyParameter("VESSELCODE", SqlDbType.VarChar, dr[1]));
                        cmd.Parameters.Add(MyParameter("ShutdownId", SqlDbType.BigInt, dr[2]));
                        cmd.Parameters.Add(MyParameter("Approver_Name", SqlDbType.VarChar, dr[3]));
                        cmd.Parameters.Add(MyParameter("Approver_Position", SqlDbType.VarChar, dr[4]));
                        cmd.Parameters.Add(MyParameter("Approver_Remarks", SqlDbType.VarChar, dr[5]));
                        cmd.Parameters.Add(MyParameter("ApprovedOn", SqlDbType.SmallDateTime, dr[6]));
                        cmd.Parameters.Add(MyParameter("UPDATED", SqlDbType.Bit, dr[7]));
                        cmd.Parameters.Add(MyParameter("UPDATEDON", SqlDbType.SmallDateTime, dr[8]));

                        int result = cmd.ExecuteNonQuery();
                    }
                }
                if (ds.Tables["VSL_CriticalShutdownExtensionApproval"].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables["VSL_CriticalShutdownExtensionApproval"].Rows)
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandText = "sp_IE_VSL_CriticalShutdownExtensionApproval";


                        cmd.Parameters.Add(MyParameter("ExtensionApproveId", SqlDbType.BigInt, dr[0]));
                        cmd.Parameters.Add(MyParameter("VESSELCODE", SqlDbType.VarChar, dr[1]));
                        cmd.Parameters.Add(MyParameter("ExtensionId", SqlDbType.BigInt, dr[2]));
                        cmd.Parameters.Add(MyParameter("Approver_Name", SqlDbType.VarChar, dr[3]));
                        cmd.Parameters.Add(MyParameter("Approver_Position", SqlDbType.VarChar, dr[4]));
                        cmd.Parameters.Add(MyParameter("Approver_Remarks", SqlDbType.VarChar, dr[5]));
                        cmd.Parameters.Add(MyParameter("ApprovedOn", SqlDbType.SmallDateTime, dr[6]));
                        cmd.Parameters.Add(MyParameter("UPDATED", SqlDbType.Bit, dr[7]));
                        cmd.Parameters.Add(MyParameter("UPDATEDON", SqlDbType.SmallDateTime, dr[8]));

                        int result = cmd.ExecuteNonQuery();
                    }
                }
                if (ds.Tables.Contains("ComponentJobMapping_OtherRanks"))
                {
                    if (ds.Tables["ComponentJobMapping_OtherRanks"].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables["ComponentJobMapping_OtherRanks"].Rows)
                        {
                            cmd.Parameters.Clear();
                            cmd.CommandText = "sp_IE_VSL_ComponentJobMapping_OtherRanks";

                            cmd.Parameters.Add(MyParameter("CompJobId", SqlDbType.BigInt, dr[0]));
                            cmd.Parameters.Add(MyParameter("RankId", SqlDbType.BigInt, dr[1]));

                            int result = cmd.ExecuteNonQuery();
                        }
                    }
                }

                #endregion
            }
            else if (PacketVersion == 3)
            {
                # region -------------- VERSION 3 ----------------
                if (ds.Tables["ComponentMaster"].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables["ComponentMaster"].Rows)
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandText = "sp_IE_ComponentMaster";

                        cmd.Parameters.Add(MyParameter("COMPONENTID", SqlDbType.BigInt, dr[0]));
                        cmd.Parameters.Add(MyParameter("COMPONENTCODE", SqlDbType.VarChar, dr[1]));
                        cmd.Parameters.Add(MyParameter("COMPONENTNAME", SqlDbType.VarChar, dr[2]));
                        cmd.Parameters.Add(MyParameter("DESCR", SqlDbType.VarChar, dr[3]));
                        cmd.Parameters.Add(MyParameter("CLASSEQUIP", SqlDbType.Bit, dr[4]));
                        cmd.Parameters.Add(MyParameter("CRITICALEQUIP", SqlDbType.Bit, dr[5]));
                        cmd.Parameters.Add(MyParameter("INACTIVE", SqlDbType.Bit, dr[6]));
                        cmd.Parameters.Add(MyParameter("CriticalType", SqlDbType.VarChar, dr[7]));

                        int result = cmd.ExecuteNonQuery();
                    }
                }
                if (ds.Tables["VSL_ComponentMasterForVessel"].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables["VSL_ComponentMasterForVessel"].Rows)
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandText = "sp_IE_VSL_ComponentMasterForVessel";

                        cmd.Parameters.Add(MyParameter("VESSELCODE", SqlDbType.VarChar, dr[0]));
                        cmd.Parameters.Add(MyParameter("COMPONENTID", SqlDbType.BigInt, dr[1]));
                        cmd.Parameters.Add(MyParameter("MAKER", SqlDbType.VarChar, dr[2]));
                        cmd.Parameters.Add(MyParameter("MAKERTYPE", SqlDbType.VarChar, dr[3]));
                        cmd.Parameters.Add(MyParameter("CLASSEQUIPCODE", SqlDbType.VarChar, dr[4]));
                        cmd.Parameters.Add(MyParameter("STATUS", SqlDbType.VarChar, dr[5]));
                        cmd.Parameters.Add(MyParameter("UPDATED", SqlDbType.Bit, dr[6]));
                        cmd.Parameters.Add(MyParameter("UPDATEDON", SqlDbType.SmallDateTime, dr[7]));
                        cmd.Parameters.Add(MyParameter("DESCR", SqlDbType.VarChar, dr[8]));
                        cmd.Parameters.Add(MyParameter("CLASSEQUIP", SqlDbType.Bit, dr[9]));
                        cmd.Parameters.Add(MyParameter("ACCOUNTCODES", SqlDbType.VarChar, dr[10]));
                        cmd.Parameters.Add(MyParameter("isOR", SqlDbType.Bit, dr[11]));
                        int result = cmd.ExecuteNonQuery();
                    }
                }
                if (ds.Tables["JobMaster"].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables["JobMaster"].Rows)
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandText = "sp_IE_JobMaster";

                        cmd.Parameters.Add(MyParameter("JOBID", SqlDbType.BigInt, dr[0]));
                        cmd.Parameters.Add(MyParameter("JOBCODE", SqlDbType.VarChar, dr[1]));
                        cmd.Parameters.Add(MyParameter("JOBNAME", SqlDbType.VarChar, dr[2]));

                        int result = cmd.ExecuteNonQuery();
                    }
                }
                if (ds.Tables["ComponentsJobMapping"].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables["ComponentsJobMapping"].Rows)
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandText = "sp_IE_ComponentsJobMapping";

                        cmd.Parameters.Add(MyParameter("COMPONENTID", SqlDbType.BigInt, dr[0]));
                        cmd.Parameters.Add(MyParameter("JOBID", SqlDbType.BigInt, dr[1]));
                        cmd.Parameters.Add(MyParameter("COMPJOBID", SqlDbType.BigInt, dr[2]));
                        cmd.Parameters.Add(MyParameter("ASSIGNTO", SqlDbType.Int, dr[3]));
                        cmd.Parameters.Add(MyParameter("DEPTID", SqlDbType.Int, dr[4]));
                        cmd.Parameters.Add(MyParameter("INTERVALID", SqlDbType.Int, dr[5]));
                        cmd.Parameters.Add(MyParameter("ISFIXED", SqlDbType.SmallInt, dr[6]));
                        cmd.Parameters.Add(MyParameter("DESCRSH", SqlDbType.VarChar, dr[7]));
                        cmd.Parameters.Add(MyParameter("DESCRM", SqlDbType.VarChar, dr[8]));
                        cmd.Parameters.Add(MyParameter("AttachmentForm", SqlDbType.VarChar, dr[9]));
                        cmd.Parameters.Add(MyParameter("RiskAssessment", SqlDbType.VarChar, dr[10]));
                        cmd.Parameters.Add(MyParameter("RatingRequired", SqlDbType.Bit, dr[11]));

                        int result = cmd.ExecuteNonQuery();
                    }
                }
                if (ds.Tables["VSL_VesselComponentJobMaster"].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables["VSL_VesselComponentJobMaster"].Rows)
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandText = "sp_IE_VSL_VesselComponentJobMaster";

                        cmd.Parameters.Add(MyParameter("VESSELCODE", SqlDbType.VarChar, dr[0]));
                        cmd.Parameters.Add(MyParameter("COMPJOBID", SqlDbType.BigInt, dr[1]));
                        cmd.Parameters.Add(MyParameter("COMPONENTID", SqlDbType.BigInt, dr[2]));
                        cmd.Parameters.Add(MyParameter("JOBID", SqlDbType.BigInt, dr[3]));
                        cmd.Parameters.Add(MyParameter("ASSIGNTO", SqlDbType.Int, dr[4]));
                        cmd.Parameters.Add(MyParameter("INTERVALID", SqlDbType.Int, dr[5]));
                        cmd.Parameters.Add(MyParameter("INTERVAL", SqlDbType.Float, dr[6]));
                        cmd.Parameters.Add(MyParameter("ISFIXED", SqlDbType.SmallInt, dr[7]));
                        cmd.Parameters.Add(MyParameter("STARTDATE", SqlDbType.SmallDateTime, dr[8]));
                        cmd.Parameters.Add(MyParameter("DESCRM", SqlDbType.VarChar, dr[9]));
                        cmd.Parameters.Add(MyParameter("INTERVAL_H", SqlDbType.Float, dr[10]));
                        cmd.Parameters.Add(MyParameter("INTERVALID_H", SqlDbType.Int, dr[11]));
                        cmd.Parameters.Add(MyParameter("Guidelines", SqlDbType.VarChar, dr[12]));
                        cmd.Parameters.Add(MyParameter("STATUS", SqlDbType.VarChar, dr[13]));
                        cmd.Parameters.Add(MyParameter("UPDATED", SqlDbType.Bit, dr[14]));
                        cmd.Parameters.Add(MyParameter("UPDATEDON", SqlDbType.SmallDateTime, dr[15]));
                        cmd.Parameters.Add(MyParameter("JobCost", SqlDbType.Float, dr[16]));

                        int result = cmd.ExecuteNonQuery();
                    }
                }
                if (ds.Tables["VSL_VesselComponentJobMaster_Updates"].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables["VSL_VesselComponentJobMaster_Updates"].Rows)
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandText = "sp_IE_VSL_VesselComponentJobMaster_Updates";

                        cmd.Parameters.Add(MyParameter("LOCATION", SqlDbType.VarChar, "S"));
                        cmd.Parameters.Add(MyParameter("VESSELCODE", SqlDbType.VarChar, dr[0]));
                        cmd.Parameters.Add(MyParameter("COMPJOBID", SqlDbType.BigInt, dr[1]));
                        cmd.Parameters.Add(MyParameter("COMPONENTID", SqlDbType.BigInt, dr[2]));
                        cmd.Parameters.Add(MyParameter("LASTDONE", SqlDbType.SmallDateTime, dr[3]));
                        cmd.Parameters.Add(MyParameter("LASTHOUR", SqlDbType.Int, dr[4]));
                        cmd.Parameters.Add(MyParameter("NEXTDUEDATE", SqlDbType.SmallDateTime, dr[5]));
                        cmd.Parameters.Add(MyParameter("NEXTHOUR", SqlDbType.Int, dr[6]));
                        cmd.Parameters.Add(MyParameter("UPDATED", SqlDbType.Bit, dr[7]));
                        cmd.Parameters.Add(MyParameter("UPDATEDON", SqlDbType.SmallDateTime, dr[8]));

                        int result = cmd.ExecuteNonQuery();
                    }
                }
                if (ds.Tables["VSL_VesselComponentJobMaster_Attachments"].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables["VSL_VesselComponentJobMaster_Attachments"].Rows)
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandText = "sp_IE_VSL_VesselComponentJobMaster_Attachments";

                        cmd.Parameters.Add(MyParameter("VESSELCODE", SqlDbType.VarChar, dr[0]));
                        cmd.Parameters.Add(MyParameter("TABLEID", SqlDbType.BigInt, dr[1]));
                        cmd.Parameters.Add(MyParameter("COMPJOBID", SqlDbType.BigInt, dr[2]));
                        cmd.Parameters.Add(MyParameter("DESCR", SqlDbType.VarChar, dr[3]));
                        cmd.Parameters.Add(MyParameter("DOCUMENTTYPE", SqlDbType.VarChar, dr[4]));
                        cmd.Parameters.Add(MyParameter("STATUS", SqlDbType.VarChar, dr[5]));

                        int result = cmd.ExecuteNonQuery();
                    }
                }
                if (ds.Tables["ComponentsJobMapping_Attachments"].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables["ComponentsJobMapping_Attachments"].Rows)
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandText = "sp_IE_ComponentsJobMapping_Attachments";

                        cmd.Parameters.Add(MyParameter("TABLEID", SqlDbType.BigInt, dr[0]));
                        cmd.Parameters.Add(MyParameter("COMPJOBID", SqlDbType.BigInt, dr[1]));
                        cmd.Parameters.Add(MyParameter("DESCR", SqlDbType.VarChar, dr[2]));
                        cmd.Parameters.Add(MyParameter("DOCUMENTTYPE", SqlDbType.VarChar, dr[3]));
                        cmd.Parameters.Add(MyParameter("STATUS", SqlDbType.VarChar, dr[4]));

                        int result = cmd.ExecuteNonQuery();
                    }
                }
                if (ds.Tables["VSL_VesselJobUpdateHistory_OfficeComments"].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables["VSL_VesselJobUpdateHistory_OfficeComments"].Rows)
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandText = "sp_IE_VSL_VesselJobUpdateHistory_OfficeComments";

                        cmd.Parameters.Add(MyParameter("VESSELCODE", SqlDbType.VarChar, dr[0]));
                        cmd.Parameters.Add(MyParameter("HISTORYID", SqlDbType.BigInt, dr[1]));
                        cmd.Parameters.Add(MyParameter("COMMENTS", SqlDbType.VarChar, dr[2]));
                        cmd.Parameters.Add(MyParameter("VERIFIED", SqlDbType.Bit, dr[3]));
                        cmd.Parameters.Add(MyParameter("VERIFIEDBY", SqlDbType.VarChar, dr[4]));
                        cmd.Parameters.Add(MyParameter("VERIFIEDON", SqlDbType.SmallDateTime, dr[5]));
                        cmd.Parameters.Add(MyParameter("UPDATED", SqlDbType.Bit, dr[6]));
                        cmd.Parameters.Add(MyParameter("UPDATEDON", SqlDbType.SmallDateTime, dr[7]));

                        int result = cmd.ExecuteNonQuery();
                    }
                }

                if (ds.Tables["VSL_VesselSpareMaster"].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables["VSL_VesselSpareMaster"].Rows)
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandText = "sp_IE_VSL_VesselSpareMaster";

                        cmd.Parameters.Add(MyParameter("VESSELCODE", SqlDbType.VarChar, dr[0]));
                        cmd.Parameters.Add(MyParameter("COMPONENTID", SqlDbType.BigInt, dr[1]));
                        cmd.Parameters.Add(MyParameter("OFFICE_SHIP", SqlDbType.VarChar, dr[2]));
                        cmd.Parameters.Add(MyParameter("SPAREID", SqlDbType.BigInt, dr[3]));
                        cmd.Parameters.Add(MyParameter("SPARENAME", SqlDbType.VarChar, dr[4]));
                        cmd.Parameters.Add(MyParameter("MAKER", SqlDbType.VarChar, dr[5]));
                        cmd.Parameters.Add(MyParameter("MAKERTYPE", SqlDbType.VarChar, dr[6]));
                        cmd.Parameters.Add(MyParameter("PARTNO", SqlDbType.VarChar, dr[7]));
                        cmd.Parameters.Add(MyParameter("ALTPARTNO", SqlDbType.VarChar, dr[8]));
                        cmd.Parameters.Add(MyParameter("LOCATION", SqlDbType.VarChar, dr[9]));
                        cmd.Parameters.Add(MyParameter("MINQTY", SqlDbType.Int, dr[10]));
                        cmd.Parameters.Add(MyParameter("MAXQTY", SqlDbType.Int, dr[11]));
                        cmd.Parameters.Add(MyParameter("STATUTORYQTY", SqlDbType.Int, dr[12]));
                        cmd.Parameters.Add(MyParameter("WEIGHT", SqlDbType.Float, dr[13]));
                        cmd.Parameters.Add(MyParameter("STOCK", SqlDbType.Int, dr[14]));
                        cmd.Parameters.Add(MyParameter("SPECIFICATION", SqlDbType.VarChar, dr[15]));
                        cmd.Parameters.Add(MyParameter("DRAWINGNO", SqlDbType.VarChar, dr[16]));
                        cmd.Parameters.Add(MyParameter("Attachment", SqlDbType.VarChar, dr[17]));
                        cmd.Parameters.Add(MyParameter("STATUS", SqlDbType.VarChar, dr[18]));
                        cmd.Parameters.Add(MyParameter("UPDATED", SqlDbType.Bit, dr[19]));
                        cmd.Parameters.Add(MyParameter("UPDATEDON", SqlDbType.SmallDateTime, dr[20]));
                        cmd.Parameters.Add(MyParameter("Critical", SqlDbType.VarChar, dr[21]));
                        cmd.Parameters.Add(MyParameter("CriticalUpdatedBy", SqlDbType.VarChar, dr[22]));
                        cmd.Parameters.Add(MyParameter("CriticalUpdatedByOn", SqlDbType.SmallDateTime, dr[23]));
                        cmd.Parameters.Add(MyParameter("isOR", SqlDbType.Bit, dr[24]));
                        cmd.Parameters.Add(MyParameter("PartName", SqlDbType.VarChar, dr[25]));
                        cmd.Parameters.Add(MyParameter("StockLocationId", SqlDbType.Int, dr[26]));
                        int result = cmd.ExecuteNonQuery();
                    }
                }

                if (ds.Tables["VSL_DefectRemarks"].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables["VSL_DefectRemarks"].Rows)
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandText = "sp_IE_VSL_DefectRemarks";

                        cmd.Parameters.Add(MyParameter("DefectRemarkId", SqlDbType.BigInt, dr[0]));
                        cmd.Parameters.Add(MyParameter("VESSELCODE", SqlDbType.VarChar, dr[1]));
                        cmd.Parameters.Add(MyParameter("DefectNo", SqlDbType.VarChar, dr[2]));
                        cmd.Parameters.Add(MyParameter("Remarks", SqlDbType.VarChar, dr[3]));
                        cmd.Parameters.Add(MyParameter("FileName", SqlDbType.VarChar, dr[4]));
                        cmd.Parameters.Add(MyParameter("File", SqlDbType.Image, dr[5]));
                        cmd.Parameters.Add(MyParameter("UPDATED", SqlDbType.Bit, dr[6]));
                        cmd.Parameters.Add(MyParameter("UPDATEDON", SqlDbType.SmallDateTime, dr[7]));
                        cmd.Parameters.Add(MyParameter("EnteredBy", SqlDbType.VarChar, dr[8]));
                        cmd.Parameters.Add(MyParameter("EnteredOn", SqlDbType.SmallDateTime, dr[9]));

                        int result = cmd.ExecuteNonQuery();
                    }
                }
                if (ds.Tables["VSL_CriticalShutdownApproval"].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables["VSL_CriticalShutdownApproval"].Rows)
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandText = "sp_IE_VSL_CriticalShutdownApproval";

                        cmd.Parameters.Add(MyParameter("ShutdownApproveId", SqlDbType.BigInt, dr[0]));
                        cmd.Parameters.Add(MyParameter("VESSELCODE", SqlDbType.VarChar, dr[1]));
                        cmd.Parameters.Add(MyParameter("ShutdownId", SqlDbType.BigInt, dr[2]));
                        cmd.Parameters.Add(MyParameter("Approver_Name", SqlDbType.VarChar, dr[3]));
                        cmd.Parameters.Add(MyParameter("Approver_Position", SqlDbType.VarChar, dr[4]));
                        cmd.Parameters.Add(MyParameter("Approver_Remarks", SqlDbType.VarChar, dr[5]));
                        cmd.Parameters.Add(MyParameter("ApprovedOn", SqlDbType.SmallDateTime, dr[6]));
                        cmd.Parameters.Add(MyParameter("UPDATED", SqlDbType.Bit, dr[7]));
                        cmd.Parameters.Add(MyParameter("UPDATEDON", SqlDbType.SmallDateTime, dr[8]));

                        int result = cmd.ExecuteNonQuery();
                    }
                }
                if (ds.Tables["VSL_CriticalShutdownExtensionApproval"].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables["VSL_CriticalShutdownExtensionApproval"].Rows)
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandText = "sp_IE_VSL_CriticalShutdownExtensionApproval";


                        cmd.Parameters.Add(MyParameter("ExtensionApproveId", SqlDbType.BigInt, dr[0]));
                        cmd.Parameters.Add(MyParameter("VESSELCODE", SqlDbType.VarChar, dr[1]));
                        cmd.Parameters.Add(MyParameter("ExtensionId", SqlDbType.BigInt, dr[2]));
                        cmd.Parameters.Add(MyParameter("Approver_Name", SqlDbType.VarChar, dr[3]));
                        cmd.Parameters.Add(MyParameter("Approver_Position", SqlDbType.VarChar, dr[4]));
                        cmd.Parameters.Add(MyParameter("Approver_Remarks", SqlDbType.VarChar, dr[5]));
                        cmd.Parameters.Add(MyParameter("ApprovedOn", SqlDbType.SmallDateTime, dr[6]));
                        cmd.Parameters.Add(MyParameter("UPDATED", SqlDbType.Bit, dr[7]));
                        cmd.Parameters.Add(MyParameter("UPDATEDON", SqlDbType.SmallDateTime, dr[8]));

                        int result = cmd.ExecuteNonQuery();
                    }
                }
                if (ds.Tables.Contains("ComponentJobMapping_OtherRanks"))
                {
                    if (ds.Tables["ComponentJobMapping_OtherRanks"].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables["ComponentJobMapping_OtherRanks"].Rows)
                        {
                            cmd.Parameters.Clear();
                            cmd.CommandText = "sp_IE_VSL_ComponentJobMapping_OtherRanks";

                            cmd.Parameters.Add(MyParameter("CompJobId", SqlDbType.BigInt, dr[0]));
                            cmd.Parameters.Add(MyParameter("RankId", SqlDbType.BigInt, dr[1]));

                            int result = cmd.ExecuteNonQuery();
                        }
                    }
                }
                if (ds.Tables.Contains("VSL_VesselComponentJobMaster_Spares"))
                {
                    if (ds.Tables["VSL_VesselComponentJobMaster_Spares"].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables["VSL_VesselComponentJobMaster_Spares"].Rows)
                        {
                            cmd.Parameters.Clear();
                            cmd.CommandText = "sp_IE_VSL_VesselComponentJobMaster_Spares";

                            cmd.Parameters.Add(MyParameter("VESSELCODE", SqlDbType.VarChar, dr[0]));
                            cmd.Parameters.Add(MyParameter("ForCompJobId", SqlDbType.BigInt, dr[1]));
                            cmd.Parameters.Add(MyParameter("ComponentId", SqlDbType.BigInt, dr[2]));
                            cmd.Parameters.Add(MyParameter("Office_Ship", SqlDbType.VarChar, dr[3]));
                            cmd.Parameters.Add(MyParameter("SpareId", SqlDbType.BigInt, dr[4]));
                            cmd.Parameters.Add(MyParameter("Qty", SqlDbType.BigInt, dr[5]));
                            cmd.Parameters.Add(MyParameter("Comments", SqlDbType.VarChar, dr[6]));
                            cmd.Parameters.Add(MyParameter("AssignedBy", SqlDbType.VarChar, dr[7]));

                            int result = cmd.ExecuteNonQuery();
                        }
                    }
                }

                #endregion
            }
            //------------------------ AFTER IMPORTING DATA PACKET WE NEED TO FLUSH THE LOG OF LAST ACK PACKET 
            if (LastPacketAck.Trim() != "")
            {
                cmd.Parameters.Clear();
                cmd.CommandText = "sp_IE_FlushPacketLog_2";
                cmd.Parameters.Add(MyParameter("VesselCode", SqlDbType.VarChar, Imp_VesselCode));
                cmd.Parameters.Add(MyParameter("@PacketName", SqlDbType.VarChar, LastPacketAck));
                cmd.ExecuteNonQuery();
            }
            //------------------------ RECEIVING PACKET DATA INSERTING LOG TO TABLE
            cmd.Parameters.Clear();
            cmd.CommandText = "sp_IE_ReceivePacket";
            cmd.Parameters.Add(MyParameter("VesselCode", SqlDbType.VarChar, Imp_VesselCode));
            cmd.Parameters.Add(MyParameter("@PacketName", SqlDbType.VarChar, PacketName));
            cmd.ExecuteNonQuery();
            //--------------------------------------------
            trans.Commit();
            lblMsg.Text = "Data imported successfully.";
        }
        catch (Exception ex)
        {
            trans.Rollback();
            lblMsg.Text = "Unable to import data. Error :" + ex.Message;
        }
        finally
        {
            if (Con.State == ConnectionState.Open) { Con.Close(); }
        }
    }
    private void ResetNULLDates(ref DataSet ds_IN)
    {
        DateTime dt_ref = new DateTime(1900, 1, 1);
        foreach (DataTable dt in ds_IN.Tables)
        {
            List<String> DateTimeCols = new List<String>();
            foreach (DataColumn dc in dt.Columns)
            {
                if (dc.DataType == System.Type.GetType("System.DateTime"))
                {
                    DateTimeCols.Add(dc.ColumnName);
                }
            }
            if (DateTimeCols.Count > 0 && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    foreach (string cName in DateTimeCols)
                    {
                        if (!Convert.IsDBNull(dr[cName]))
                        {
                            DateTime dt_test = Convert.ToDateTime(dr[cName]);
                            if (dt_test <= dt_ref)
                            {
                                dr[cName] = DBNull.Value;
                            }
                        }
                    }
                }
            }
        }
    }
    public SqlParameter MyParameter(string _Name, SqlDbType _SD, object _Value)
    {
        SqlParameter sq = new SqlParameter(_Name, _SD);
        if (_Value == null)
            sq.Value = DBNull.Value;
        else
            sq.Value = _Value;
        return sq;
    }
    public void MoveFiles(string DestFolder,string SourceFolder)
    {
        string DirectoryName = new DirectoryInfo(SourceFolder).Name;
        string CopyPath = DestFolder + "\\" + DirectoryName;

        // --- creating directory if not exists
        if (!(Directory.Exists(CopyPath)))
            Directory.CreateDirectory(CopyPath);
        
        // --- copying all files
        string[] Files = Directory.GetFiles(SourceFolder);
        foreach (string fl in Files)
        {
            if (File.Exists(CopyPath + "//" + Path.GetFileName(fl)))
                File.Delete(CopyPath + "//" + Path.GetFileName(fl));

            File.Copy(fl, CopyPath + "//" + Path.GetFileName(fl));
        }

        // --- copying all Inner folder

        string[] folders = Directory.GetDirectories(SourceFolder);
        foreach (string folder in folders)
        {
            MoveFiles(CopyPath, folder);
        }
    }
    
    //---------------- PMS_CREW_Trainings_Import
    private void PMS_CREW_Trainings_Import(DataSet ds, string FileToImport)
    {
        SqlTransaction trans;
        SqlConnection Con = new SqlConnection(sqlConnectionString.Replace("Master", "eMANAGER"));
        SqlCommand cmdCheck = new SqlCommand("", Con);
        SqlDataAdapter Sda = new SqlDataAdapter(cmdCheck);
        string filenameonly = Path.GetFileNameWithoutExtension(FileToImport);
        string packet_Vesselname= filenameonly.Substring(filenameonly.Length-5,3);

            
        Con.Open();
        trans = Con.BeginTransaction();
        cmdCheck.Transaction = trans;
        //---------------- CHECKING PACKET IS FOR SAME VESSEL OR NOT
        DataSet DsTemp = new DataSet();
        cmdCheck.CommandText = "SELECT * FROM DBO.SETTINGS";
        Sda.Fill(DsTemp, "Set");

        //string In_VesselCode = DsTemp.Tables[0].Rows[0]["SHIPCODE"].ToString();
        //if (packet_Vesselname != In_VesselCode)
        //{
        //    lblMsg.Text = "Importing packet's VESSEL is not matching with VESSEL.";
        //    return;
        //}
        //if (!filenameonly.EndsWith("O"))
        //{
        //    lblMsg.Text = "Looks this packet doesn't send by office. Please confirm.";
        //    return;
        //}

        //---------------- CHECKING PACKET RECEIVED NO. IS GREATER THAN DB PACKET RECEIVED

       
        #region         
            try
            {
                //------------------------
                cmdCheck.CommandType = CommandType.Text;
                cmdCheck.CommandText = "TRUNCATE TABLE PMS_CREW_TRAININGREQUIREMENT";
                cmdCheck.ExecuteNonQuery();

                //------------------------
                SqlCommand cmd = new SqlCommand("", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = trans;

                if (ds.Tables["PMS_CREW_TRAININGREQUIREMENT"].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables["PMS_CREW_TRAININGREQUIREMENT"].Rows)
                    {
                        string[] CommandParameters = getCommandParameters(cmd, "SHIP_IMPORT_PMS_CREW_TRAININGREQUIREMENT");
                        cmd.Parameters.Clear();
                        cmd.CommandText = "SHIP_IMPORT_PMS_CREW_TRAININGREQUIREMENT";
                        cmd.CommandType = CommandType.StoredProcedure;

                        for (int i = 0; i <= CommandParameters.Length - 1; i++)
                        {
                            object data = DBNull.Value;
                            try
                            { data = dr[CommandParameters[i]]; }
                            catch { data = DBNull.Value; }
                            cmd.Parameters.Add(new SqlParameter(CommandParameters[i], data));
                        }
                        int result = cmd.ExecuteNonQuery();
                    }
                }
             
                trans.Commit();
                lblMsg.Text = "PMS Crew Training imported successfully. File Name : " + FileToImport;
            }
            catch (Exception ex)
            {
                trans.Rollback();
                lblMsg.Text = "Unable to import PMS Crew Training. File Name : " + FileToImport + ", Error : " + ex.Message;
            }
            finally
            {
                if (Con.State == ConnectionState.Open) { Con.Close(); }
            }
        #endregion
    }
    //---------------- SMS PACKET IMPORT
    private void SMS_Import(DataSet ds, string FileToImport)
    {
        SqlTransaction trans;
        SqlConnection Con = new SqlConnection(sqlConnectionString.Replace("Master", "eMANAGER"));
        SqlCommand cmdCheck = new SqlCommand("", Con);
        SqlDataAdapter Sda = new SqlDataAdapter(cmdCheck);

        Con.Open();
        trans = Con.BeginTransaction();
        
        string SMS_VesselCode = ds.Tables["SMS_PacketID"].Rows[0]["VesselCode"].ToString();
        int PacketId = Convert.ToInt32(ds.Tables["SMS_PacketID"].Rows[0]["PacketId"]);
        int Version = Convert.ToInt32(ds.Tables["SMS_PacketID"].Rows[0]["Version"]);

        cmdCheck.Transaction = trans;
        cmdCheck.CommandType = CommandType.Text;
        //---------------- CHECKING PACKET IS FOR SAME VESSEL OR NOT
        DataSet DsTemp = new DataSet();
        cmdCheck.CommandText = "SELECT * FROM DBO.SETTINGS";
        Sda.Fill(DsTemp, "Set");

        string In_VesselCode = DsTemp.Tables[0].Rows[0]["SHIPCODE"].ToString();
        if (SMS_VesselCode != In_VesselCode)
        {
            lblMsg.Text = "Importing packet's VESSEL is not matching with VESSEL.";
            return;
        }
        
        //---------------- CHECKING PACKET RECEIVED NO. IS GREATER THAN DB PACKET RECEIVED

        cmdCheck.CommandText = "SELECT count(*) FROM dbo.VSL_SMS_PacketRecd WHERE VesselCode ='" + SMS_VesselCode + "' AND PacketID >= " + PacketId.ToString();
        if (Convert.ToInt32(cmdCheck.ExecuteScalar()) > 0)
        {
            lblMsg.Text = "Please Check. Heigher or Same version SMS packet is already updated in the database.";
            return;
        }

        #region version 1
        if (Version == 1)// ------- Version 1 Start
        {
            try
            {
                SqlCommand cmd = new SqlCommand("", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = trans;

                if (ds.Tables["SMS_APP_ManualMaster"].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables["SMS_APP_ManualMaster"].Rows)
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandText = "Ship_IU_SMS_APP_ManualMaster";

                        cmd.Parameters.Add(MyParameter("MANUALID", SqlDbType.BigInt, dr[0]));
                        cmd.Parameters.Add(MyParameter("MANUALNAME", SqlDbType.VarChar, dr[1]));
                        cmd.Parameters.Add(MyParameter("VERSIONNO", SqlDbType.VarChar, dr[2]));
                        cmd.Parameters.Add(MyParameter("CREATEDON", SqlDbType.DateTime, dr[3]));
                        cmd.Parameters.Add(MyParameter("MAPPROVED", SqlDbType.VarChar, dr[4]));
                        cmd.Parameters.Add(MyParameter("MAPPROVEDON", SqlDbType.DateTime, dr[5]));

                        int result = cmd.ExecuteNonQuery();
                    }
                }
                //---
                if (ds.Tables["SMS_APP_ManualDetails"].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables["SMS_APP_ManualDetails"].Rows)
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandText = "SHIP_IU_SMS_APP_ManualDetails";

                        cmd.Parameters.Add(MyParameter("MANUALID", SqlDbType.BigInt, dr[0]));
                        cmd.Parameters.Add(MyParameter("SECTIONID", SqlDbType.VarChar, dr[1]));
                        cmd.Parameters.Add(MyParameter("PARENTSECTIONID", SqlDbType.VarChar, dr[2]));
                        cmd.Parameters.Add(MyParameter("HEADING", SqlDbType.VarChar, dr[3]));
                        cmd.Parameters.Add(MyParameter("SEARCHTAGS", SqlDbType.VarChar, dr[4]));
                        cmd.Parameters.Add(MyParameter("FILENAME", SqlDbType.VarChar, dr[5]));
                        cmd.Parameters.Add(MyParameter("FILECONTENT", SqlDbType.Image, dr[6]));
                        cmd.Parameters.Add(MyParameter("APPROVED", SqlDbType.VarChar, dr[7]));
                        cmd.Parameters.Add(MyParameter("APPROVEDON", SqlDbType.DateTime, dr[8]));
                        cmd.Parameters.Add(MyParameter("STATUS", SqlDbType.VarChar, dr[9]));
                        cmd.Parameters.Add(MyParameter("SVERSION", SqlDbType.VarChar, dr[10]));
                        cmd.Parameters.Add(MyParameter("MODIFIEDBY", SqlDbType.VarChar, dr[11]));
                        cmd.Parameters.Add(MyParameter("MODIFIEDON", SqlDbType.DateTime, dr[12]));
                        cmd.Parameters.Add(MyParameter("REQREMARKS", SqlDbType.VarChar, dr[13]));
                        cmd.Parameters.Add(MyParameter("APPREMARKS", SqlDbType.VarChar, dr[14]));
                        cmd.Parameters.Add(MyParameter("REJREMARKS", SqlDbType.VarChar, dr[15]));

                        int result = cmd.ExecuteNonQuery();
                    }
                }
                //------------------
                if (ds.Tables["SMS_APP_ManualDetailsForms"].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables["SMS_APP_ManualDetailsForms"].Rows)
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandText = "SHIP_UP_SMS_APP_ManualDetailsForms";

                        cmd.Parameters.Add(MyParameter("MANUALID", SqlDbType.BigInt, dr[0]));
                        cmd.Parameters.Add(MyParameter("SECTIONID", SqlDbType.VarChar, dr[1]));
                        cmd.Parameters.Add(MyParameter("FORMID", SqlDbType.BigInt, dr[2]));

                        int result = cmd.ExecuteNonQuery();
                    }
                }
                //------------------
                if (ds.Tables["SMS_APP_ManualDetailsRanks"].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables["SMS_APP_ManualDetailsRanks"].Rows)
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandText = "SHIP_UP_SMS_APP_ManualDetailsRanks";

                        cmd.Parameters.Add(MyParameter("MANUALID", SqlDbType.BigInt, dr[0]));
                        cmd.Parameters.Add(MyParameter("SECTIONID", SqlDbType.VarChar, dr[1]));
                        cmd.Parameters.Add(MyParameter("RANKID", SqlDbType.BigInt, dr[2]));
                        cmd.Parameters.Add(MyParameter("RANKCODE", SqlDbType.VarChar, dr[3]));

                        int result = cmd.ExecuteNonQuery();
                    }
                }
                //------------------
                if (ds.Tables["SMS_Forms"].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables["SMS_Forms"].Rows)
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandText = "SHIP_IU_SMS_Forms";

                        cmd.Parameters.Add(MyParameter("FORMID", SqlDbType.BigInt, dr[0]));
                        cmd.Parameters.Add(MyParameter("FORMNAME", SqlDbType.VarChar, dr[1]));
                        cmd.Parameters.Add(MyParameter("FORMNO", SqlDbType.VarChar, dr[2]));
                        cmd.Parameters.Add(MyParameter("VERSIONNO", SqlDbType.VarChar, dr[3]));
                        cmd.Parameters.Add(MyParameter("FILENAME", SqlDbType.VarChar, dr[4]));
                        cmd.Parameters.Add(MyParameter("FILECONTENT", SqlDbType.Binary, dr[5]));
                        cmd.Parameters.Add(MyParameter("CREATEDON", SqlDbType.DateTime, dr[6]));
                        cmd.Parameters.Add(MyParameter("UPDATED", SqlDbType.Bit, dr[7]));
                        cmd.Parameters.Add(MyParameter("UPDATEDON", SqlDbType.DateTime, dr[8]));

                        int result = cmd.ExecuteNonQuery();
                    }
                }
                //------------------
                if (ds.Tables["SMS_ScheduledKeys"].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables["SMS_ScheduledKeys"].Rows)
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandText = "Ship_IU_SMS_APP_COM_ManualDetails";

                        cmd.Parameters.Add(MyParameter("VESSELID", SqlDbType.SmallInt, dr[0]));
                        cmd.Parameters.Add(MyParameter("MANUALID", SqlDbType.BigInt, dr[1]));
                        cmd.Parameters.Add(MyParameter("SECTIONID", SqlDbType.VarChar, dr[2]));
                        cmd.Parameters.Add(MyParameter("SNO", SqlDbType.BigInt, dr[3]));
                        cmd.Parameters.Add(MyParameter("SCHEDULED", SqlDbType.Bit, dr[4]));
                        cmd.Parameters.Add(MyParameter("SENTDATE", SqlDbType.DateTime, dr[5]));
                        cmd.Parameters.Add(MyParameter("SENDBY", SqlDbType.VarChar, dr[6]));
                        cmd.Parameters.Add(MyParameter("ACKSTATUS", SqlDbType.VarChar, dr[7]));
                        cmd.Parameters.Add(MyParameter("ACKBY", SqlDbType.VarChar, dr[8]));
                        cmd.Parameters.Add(MyParameter("ACKON", SqlDbType.DateTime, dr[9]));

                        int result = cmd.ExecuteNonQuery();
                    }
                }
                //------------------
                if (ds.Tables["SMS_MaxCommentId"].Rows.Count > 0)
                {
                    int MaxCommentId = 0;
                    if (!(Convert.IsDBNull(ds.Tables["SMS_MaxCommentId"].Rows[0]["MaxCommentId"])))
                    {
                        MaxCommentId = Convert.ToInt32(ds.Tables["SMS_MaxCommentId"].Rows[0]["MaxCommentId"]);
                    }

                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Clear();
                    cmd.CommandText = "UPDATE SMS_SHIPCOMMENTS SET ReceivedInOffice='Y' WHERE VESSELCODE='" + SMS_VesselCode + "' AND COMMENTID<=" + MaxCommentId.ToString();
                    cmd.ExecuteNonQuery();
                }
                //------------------
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Clear();
                cmd.CommandText = "INSERT INTO VSL_SMS_PACKETRECD (VESSELCODE,PACKETID,PACKETNAME,RECDON) VALUES('" + SMS_VesselCode + "'," + PacketId.ToString() + ",'" + Path.GetFileNameWithoutExtension(FileToImport) + "',Getdate())";
                int Res1 = cmd.ExecuteNonQuery();
                if (Res1 <= 0)
                {
                    throw new Exception("Unable to save record in table ( VSL_SMS_PACKETRECD ).");
                }
                // End
                trans.Commit();
                lblMsg.Text = "SMS packet imported successfully. File Name : " + FileToImport;
            }
            catch (Exception ex)
            {
                trans.Rollback();
                lblMsg.Text="Unable to import SMS data. File Name : " + FileToImport  + ", Error : " + ex.Message;
            }
            finally
            {
                if (Con.State == ConnectionState.Open) { Con.Close(); }
            }
        } 
        #endregion
         
    }
    //---------------- INSPECTION IMPORT
    private void INSPECTION_OBSERVATION_Import(DataSet ds, string FileToImport)
    {
        SqlTransaction trans;
        SqlConnection Con = new SqlConnection(sqlConnectionString.Replace("Master", "SNQ"));
        SqlCommand cmdCheck = new SqlCommand("", Con);
        SqlDataAdapter Sda = new SqlDataAdapter(cmdCheck);

        Con.Open();
        trans = Con.BeginTransaction();

        StreamReader sr = new StreamReader(FileToImport);
        string FileText = sr.ReadToEnd();
        XmlDocument xd = new XmlDocument();
        xd.LoadXml(FileText);
        try
        {
            foreach (XmlNode xn in xd.DocumentElement.SelectNodes("Inspection"))
            {
                string InspectionNo = xn.ChildNodes[0].InnerText;
                string PlanDate = xn.ChildNodes[1].InnerText;
                string PortName = xn.ChildNodes[2].InnerText;
                DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM VIMS_Inspections WHERE LTRIM(RTRIM(INSPECTIONNO))='" + InspectionNo + "'");
                if (dt.Rows.Count <= 0)
                {
                    Common.Execute_Procedures_Select_ByQuery("INSERT INTO VIMS_Inspections(INSPECTIONNO,PLANDATE,PORTNAME) VALUES('" + InspectionNo + "','" + PlanDate + "','" + PortName + "')");
                }
                ProjectCommon.ShowMessage("Inspection No : " + InspectionNo + " Imported successfully.");
            }
            
            trans.Commit();
        }
        catch
        {
            trans.Rollback();
            ProjectCommon.ShowMessage("Invalid inspection file to import.");
        }
        finally 
        {
            Con.Close();
        }
    }
    //---------------- SMS RANKS IMPORT
    private void SMS_RANKS_Import(DataSet ds, string FileToImport)
    {
        SqlTransaction trans;
        SqlConnection Con = new SqlConnection(sqlConnectionString.Replace("Master", "eMANAGER"));
        Con.Open();
        trans = Con.BeginTransaction();
        try
        {
            SqlCommand cmd = new SqlCommand("", Con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Transaction = trans;
            
            cmd.CommandType=CommandType.Text;
            cmd.CommandText = "TRUNCATE TABLE SMS_APP_ManualDetailsRanks";
            cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            //------------------------------------------------      
            cmd.CommandType = CommandType.StoredProcedure;
            //------------------------------------------------      
            
            if (ds.Tables["SMS_APP_ManualDetailsRanks"].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables["SMS_APP_ManualDetailsRanks"].Rows)
                {
                    string[] CommandParameters = getCommandParameters(cmd, "SHIP_UP_SMS_APP_ManualDetailsRanks");
                    cmd.Parameters.Clear();
                    cmd.CommandText = "SHIP_UP_SMS_APP_ManualDetailsRanks";
                    cmd.CommandType = CommandType.StoredProcedure;
                    for (int i = 0; i <= CommandParameters.Length - 1; i++)
                    {
                        object data = DBNull.Value;
                        try
                        { data = dr[CommandParameters[i]]; }
                        catch { data = DBNull.Value; }
                        cmd.Parameters.Add(new SqlParameter(CommandParameters[i], data));
                    }
                    //------------------------------
                    int result = cmd.ExecuteNonQuery();
                }
            }
            trans.Commit();
            lblMsg.Text = "SMS RANKS Imported successfully.";

        }
        catch (Exception ex)
        {
            trans.Rollback();
            lblMsg.Text = "Unable to import SMS RANKS. Error : " + ex.Message;
        }
        finally
        {
            if (Con.State == ConnectionState.Open) { Con.Close(); }
        }
    }
    //---------------- VNA PACKET IMPORT
    private void VNA_Import(DataSet ds, string FileToImport)
    {
        SqlTransaction trans;
        SqlConnection Con = new SqlConnection(sqlConnectionString.Replace("Master", "eMANAGER"));
        Con.Open();
        trans = Con.BeginTransaction();
        try
        {
            SqlCommand cmd = new SqlCommand("", Con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Transaction = trans;
            //------------------------------------------------      
            cmd.CommandType = CommandType.StoredProcedure;
            //------------------------------------------------      
            if (ds.Tables["VNA_MASTER"].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables["VNA_MASTER"].Rows)
                {
                    //-------------------------------
                    string[] CommandParameters = getCommandParameters(cmd, "Import_S_VNA_Master");
                    cmd.Parameters.Clear();
                    cmd.CommandText = "Import_S_VNA_Master";
                    cmd.CommandType = CommandType.StoredProcedure;
                    for (int i = 0; i <= CommandParameters.Length - 1; i++)
                    {
                        object data = DBNull.Value;
                        try
                        { data = dr[CommandParameters[i]]; }
                        catch { data = DBNull.Value; }
                        cmd.Parameters.Add(new SqlParameter(CommandParameters[i], data));
                    }
                    //------------------------------
                    int result = cmd.ExecuteNonQuery();
                }
            }

            if (ds.Tables["vna_details"].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables["vna_details"].Rows)
                {
                    //-------------------------------
                    string[] CommandParameters = getCommandParameters(cmd, "Import_S_VNA_Details");
                    cmd.Parameters.Clear();
                    cmd.CommandText = "Import_S_VNA_Details";
                    cmd.CommandType = CommandType.StoredProcedure;
                    for (int i = 0; i <= CommandParameters.Length - 1; i++)
                    {
                        object data = DBNull.Value;
                        try
                        { data = dr[CommandParameters[i]]; }
                        catch { data = DBNull.Value; }
                        cmd.Parameters.Add(new SqlParameter(CommandParameters[i], data));
                    }
                    //------------------------------
                    int result = cmd.ExecuteNonQuery();
                }
            }

            trans.Commit();
            lblMsg.Text = "VNA Imported successfully.";

        }
        catch (Exception ex)
        {
            trans.Rollback();
            lblMsg.Text = "Unable to import VNA. Error : " + ex.Message;
        }
        finally
        {
            if (Con.State == ConnectionState.Open) { Con.Close(); }
        }
    }
    //---------------- IMPORT ORDER ITEMS -----------
    private void IMPORT_ORDER_ITEMS(DataSet ds, string FileToImport)
    {
        SqlTransaction trans;
        SqlConnection Con = new SqlConnection(sqlConnectionString.Replace("Master", "eMANAGER"));
        SqlCommand cmd = new SqlCommand("", Con);

        Con.Open();
        trans = Con.BeginTransaction();
        cmd.Transaction = trans;

        try
        {
            DataTable dt = ds.Tables["POS_OrderReceipt"];
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    //-------------------------------
                    string[] CommandParameters = getCommandParameters(cmd, "sp_Import_" + dt.TableName);
                    cmd.Parameters.Clear();
                    cmd.CommandText = "sp_Import_" + dt.TableName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    for (int i = 0; i <= CommandParameters.Length - 1; i++)
                    {
                        object data = DBNull.Value;
                        try
                        { data = dr[CommandParameters[i]]; }
                        catch { data = DBNull.Value; }
                        cmd.Parameters.Add(new SqlParameter(CommandParameters[i], data));
                    }
                    //------------------------------
                    int result12 = cmd.ExecuteNonQuery();
                }
            }

            dt = ds.Tables["POS_OrderReceiptDetails"];
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    //-------------------------------
                    string[] CommandParameters = getCommandParameters(cmd, "sp_Import_" + dt.TableName);
                    cmd.Parameters.Clear();
                    cmd.CommandText = "sp_Import_" + dt.TableName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    for (int i = 0; i <= CommandParameters.Length - 1; i++)
                    {
                        object data = DBNull.Value;
                        try
                        { data = dr[CommandParameters[i]]; }
                        catch { data = DBNull.Value; }
                        cmd.Parameters.Add(new SqlParameter(CommandParameters[i], data));
                    }
                    //------------------------------
                    int result12 = cmd.ExecuteNonQuery();
                }
            }
            trans.Commit();
            lblMsg.Text = "Office Order Receipt  packet imported successfully. File Name : " + FileToImport;
        }
        catch (Exception ex)
        {
            trans.Rollback();
            lblMsg.Text = "Unable to import Office Order Receipt packet. File Name : " + FileToImport + " Error : " + ex.Message;
        }
        finally
        {
            if (Con.State == ConnectionState.Open) { Con.Close(); }
        }
    }
    //---------------- MRV OFFICE PACKET IMPORT -----------
    private void IMPORT_OFFICE_MRV(DataSet ds, string FileToImport)
    {
        SqlTransaction trans;
        SqlConnection Con = new SqlConnection(sqlConnectionString.Replace("Master", "eMANAGER"));
        SqlCommand cmd = new SqlCommand("", Con);

        Con.Open();
        trans = Con.BeginTransaction();
        cmd.Transaction = trans;

        try
        {
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "DELETE FROM DBO.MRV_FuelTank";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "DELETE FROM DBO.MRV_FuelTypes";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "DELETE FROM DBO.MRV_EmissionSource";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "DELETE FROM DBO.MRV_EmissionSource_FlowMeters_Calc";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "DELETE FROM DBO.MRV_EmissionSource_FlowMeters";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "DELETE FROM DBO.MRV_FlowMeters";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "DELETE FROM DBO.MRV_Activity";
            cmd.ExecuteNonQuery();

            foreach (DataTable dt in ds.Tables)
            {
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        //-------------------------------
                        string[] CommandParameters = getCommandParameters(cmd, "sp_IE_" + dt.TableName);
                        cmd.Parameters.Clear();
                        cmd.CommandText = "sp_IE_" + dt.TableName;
                        cmd.CommandType = CommandType.StoredProcedure;
                        for (int i = 0; i <= CommandParameters.Length - 1; i++)
                        {
                            object data = DBNull.Value;
                            try
                            { data = dr[CommandParameters[i]]; }
                            catch { data = DBNull.Value; }
                            cmd.Parameters.Add(new SqlParameter(CommandParameters[i], data));
                        }
                        //------------------------------
                        int result12 = cmd.ExecuteNonQuery();
                    }
                }
            }
            trans.Commit();
            lblMsg.Text = "Office MRV packet imported successfully. File Name : " + FileToImport;
        }
        catch (Exception ex)
        {
            trans.Rollback();
            lblMsg.Text = "Unable to import Office MRV  packet. File Name : " + FileToImport + " Error : " + ex.Message;
        }
        finally
        {
            if (Con.State == ConnectionState.Open) { Con.Close(); }
        }
    }
    //---------------- JOB COMMUNICATION OFFICE PACKET IMPORT -----------
    private void IMPORT_JOB_COMM(DataSet ds, string FileToImport)
    {
        SqlTransaction trans;
        SqlConnection Con = new SqlConnection(sqlConnectionString.Replace("Master", "eMANAGER"));
        SqlCommand cmd = new SqlCommand("", Con);

        Con.Open();
        trans = Con.BeginTransaction();
        cmd.Transaction = trans;

        try
        {
            foreach (DataTable dt in ds.Tables)
            {
                if (dt.Rows.Count > 0)
                {
                    if (dt.TableName != "VSL_VesselComponentJobOfficePlanning_Log") // SHIP SIDE THIS NOT NEEDED
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            //-------------------------------
                            string[] CommandParameters = getCommandParameters(cmd, "sp_IE_" + dt.TableName);
                            cmd.Parameters.Clear();
                            cmd.CommandText = "sp_IE_" + dt.TableName;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add(new SqlParameter("LOCATION", "S"));
                            for (int i = 1; i <= CommandParameters.Length - 1; i++)
                            {
                                object data = DBNull.Value;
                                try
                                { data = dr[CommandParameters[i]]; }
                                catch { data = DBNull.Value; }
                                cmd.Parameters.Add(new SqlParameter(CommandParameters[i], data));
                            }
                            //------------------------------
                            int result12 = cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            trans.Commit();
            lblMsg.Text = "Office PMS JOB Communication packet imported successfully. File Name : " + FileToImport;
        }
        catch (Exception ex)
        {
            trans.Rollback();
            lblMsg.Text = "Unable to import Office PMS JOB Communication packet. File Name : " + FileToImport + " Error : " + ex.Message;
        }
        finally
        {
            if (Con.State == ConnectionState.Open) { Con.Close(); }
        }
    }
    

    //---------------- STORE_ITEMS_MASTER_Import
    private void STORE_ITEMS_MASTER_Import(DataSet ds, string FileToImport)
    {
        SqlTransaction trans;
        SqlConnection Con = new SqlConnection(sqlConnectionString.Replace("Master", "eMANAGER"));
        SqlCommand cmd = new SqlCommand("", Con);

        Con.Open();
        trans = Con.BeginTransaction();
        cmd.Transaction = trans;

        try
        {
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "DELETE FROM DBO.tbl_StoreUnitMaster";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "DELETE FROM DBO.tblStoreItems";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "DELETE FROM DBO.tblStoreItemsVA";
            cmd.ExecuteNonQuery();

            if (ds.Tables["tbl_StoreUnitMaster"].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables["tbl_StoreUnitMaster"].Rows)
                {
                    //-------------------------------
                    string[] CommandParameters = getCommandParameters(cmd, "sp_IE_tbl_StoreUnitMaster");
                    cmd.Parameters.Clear();
                    cmd.CommandText = "sp_IE_tbl_StoreUnitMaster";
                    cmd.CommandType = CommandType.StoredProcedure;
                    for (int i = 0; i <= CommandParameters.Length - 1; i++)
                    {
                        object data = DBNull.Value;
                        try
                        { data = dr[CommandParameters[i]]; }
                        catch { data = DBNull.Value; }
                        cmd.Parameters.Add(new SqlParameter(CommandParameters[i], data));
                    }
                    //------------------------------
                    int result12 = cmd.ExecuteNonQuery();
                }
            }

            if (ds.Tables["tblStoreItems"].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables["tblStoreItems"].Rows)
                {

                    //-------------------------------
                    string[] CommandParameters = getCommandParameters(cmd, "sp_IE_tblStoreItems");
                    cmd.Parameters.Clear();
                    cmd.CommandText = "sp_IE_tblStoreItems";
                    cmd.CommandType = CommandType.StoredProcedure;
                    for (int i = 0; i <= CommandParameters.Length - 1; i++)
                    {
                        object data = DBNull.Value;
                        try
                        { data = dr[CommandParameters[i]]; }
                        catch { data = DBNull.Value; }
                        cmd.Parameters.Add(new SqlParameter(CommandParameters[i], data));
                    }
                    //------------------------------
                    int result12 = cmd.ExecuteNonQuery();
                }
            }

            if (ds.Tables["tblStoreItemsVA"].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables["tblStoreItemsVA"].Rows)
                {

                    //-------------------------------
                    string[] CommandParameters = getCommandParameters(cmd, "sp_IE_tblStoreItemsVA");
                    cmd.Parameters.Clear();
                    cmd.CommandText = "sp_IE_tblStoreItemsVA";
                    cmd.CommandType = CommandType.StoredProcedure;
                    for (int i = 0; i <= CommandParameters.Length - 1; i++)
                    {
                        object data = DBNull.Value;
                        try
                        { data = dr[CommandParameters[i]]; }
                        catch { data = DBNull.Value; }
                        cmd.Parameters.Add(new SqlParameter(CommandParameters[i], data));
                    }
                    //------------------------------
                    int result12 = cmd.ExecuteNonQuery();
                }
            }

            trans.Commit();
            lblMsg.Text = "Store item master packet imported successfully. File Name : " + FileToImport;
        }
        catch (Exception ex)
        {
            trans.Rollback();
            lblMsg.Text = "Unable to import Store item master packet. File Name : " + FileToImport + " Error : " + ex.Message;
        }
        finally
        {
            if (Con.State == ConnectionState.Open) { Con.Close(); }
        }
    }
    //---------------- MNP PACKET IMPORT
    private void MNP_Import(DataSet ds, string FileToImport)
    {
        SqlTransaction trans;
        SqlConnection Con = new SqlConnection(sqlConnectionString.Replace("Master", "eMANAGER"));
        SqlCommand cmdCheck = new SqlCommand("", Con);
        SqlDataAdapter Sda = new SqlDataAdapter(cmdCheck);

        Con.Open();
        trans = Con.BeginTransaction();
      
        string MNP_VesselCode = ds.Tables["PACKETHEADER"].Rows[0]["VesselCode"].ToString();
        int PacketId = Convert.ToInt32(ds.Tables["PACKETHEADER"].Rows[0]["PacketId"]);
        int Version = Convert.ToInt32(ds.Tables["PACKETHEADER"].Rows[0]["Version"]);
        int LastUpdatedPacketId = 0;
        try
        {
            LastUpdatedPacketId = Convert.ToInt32(ds.Tables["PACKETHEADER"].Rows[0]["LastUpdatedPacketId"]);
        }
        catch { }


        cmdCheck.Transaction = trans;
        cmdCheck.CommandType = CommandType.Text;
        //---------------- CHECKING PACKET IS FOR SAME VESSEL OR NOT
        DataSet DsTemp = new DataSet();
        cmdCheck.CommandText = "SELECT * FROM DBO.SETTINGS";
        Sda.Fill(DsTemp, "Set");

        string In_VesselCode = DsTemp.Tables[0].Rows[0]["SHIPCODE"].ToString();
        if (MNP_VesselCode != In_VesselCode)
        {
            lblMsg.Text = "Importing packet's VESSEL is not matching with VESSEL.";
            return;
        }
        
        //---------------- CHECKING PACKET RECEIVED NO. IS GREATER THAN DB PACKET RECEIVED

        cmdCheck.CommandText = "SELECT count(*) FROM dbo.VSL_MNP_PacketRecd WHERE VesselCode ='" + MNP_VesselCode + "' AND PacketID >= " + PacketId.ToString();
        if (Convert.ToInt32(cmdCheck.ExecuteScalar()) > 0)
        {
            lblMsg.Text = "Please Check. Heigher or Same version MNP packet is already updated in the database.";
            return;
        }

        if (Version == 1)// ------- Version 1 Start
        {
            try
            {
                SqlCommand cmd = new SqlCommand("", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = trans;

                //-----------------Remove existing Data
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Delete FROM MP_AllRank";
                cmd.ExecuteNonQuery();
                cmd.CommandType = CommandType.StoredProcedure;

                //-----------------
                if (ds.Tables["MP_AllRank"].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables["MP_AllRank"].Rows)
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandText = "sp_IE_V1_MP_AllRank";

                        cmd.Parameters.Add(MyParameter("RANKID", SqlDbType.BigInt, dr[0]));
                        cmd.Parameters.Add(MyParameter("RANKCODE", SqlDbType.VarChar, dr[1]));
                        cmd.Parameters.Add(MyParameter("RANKNAME", SqlDbType.VarChar, dr[2]));

                        int result = cmd.ExecuteNonQuery();
                    }
                }
                //-----------------

                //-----------------Remove existing Data
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Delete FROM MP_MainCategory";
                cmd.ExecuteNonQuery();
                cmd.CommandType = CommandType.StoredProcedure;

                //-----------------

                //-----------------
                if (ds.Tables["MP_MainCategory"].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables["MP_MainCategory"].Rows)
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandText = "sp_IE_V1_MP_MainCategory";

                        cmd.Parameters.Add(MyParameter("CATEGORYID", SqlDbType.BigInt, dr[0]));
                        cmd.Parameters.Add(MyParameter("CATEGORYNAME", SqlDbType.VarChar, dr[1]));
                        cmd.Parameters.Add(MyParameter("CATEGORYCODE", SqlDbType.VarChar, dr[2]));
                        cmd.Parameters.Add(MyParameter("PARENTCATEGORYID", SqlDbType.BigInt, dr[3]));
                        cmd.Parameters.Add(MyParameter("ORDERUNITID", SqlDbType.Int, dr[4]));
                        cmd.Parameters.Add(MyParameter("CONSUMPTIONUNITID", SqlDbType.Int, dr[5]));
                        cmd.Parameters.Add(MyParameter("CONVERSIONFACTOR", SqlDbType.Float, dr[6]));
                        cmd.Parameters.Add(MyParameter("LEVEL", SqlDbType.VarChar, dr[7]));
                        cmd.Parameters.Add(MyParameter("INACTIVE", SqlDbType.Bit, dr[8]));

                        int result1 = cmd.ExecuteNonQuery();
                    }
                }
                //-----------------

                //-----------------Remove existing Data
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Delete FROM MP_MealsTypeMaster";
                cmd.ExecuteNonQuery();
                cmd.CommandType = CommandType.StoredProcedure;

                //-----------------

                //-----------------
                if (ds.Tables["MP_MealsTypeMaster"].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables["MP_MealsTypeMaster"].Rows)
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandText = "sp_IE_V1_MP_MealsTypeMaster";

                        cmd.Parameters.Add(MyParameter("MEALID", SqlDbType.BigInt, dr[0]));
                        cmd.Parameters.Add(MyParameter("MEALTYPE", SqlDbType.VarChar, dr[1]));

                        int result2 = cmd.ExecuteNonQuery();
                    }
                }
                //-----------------

                //-----------------Remove existing Data
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Delete FROM MP_NationalityMaster";
                cmd.ExecuteNonQuery();
                cmd.CommandType = CommandType.StoredProcedure;

                //-----------------

                //-----------------
                if (ds.Tables["MP_NationalityMaster"].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables["MP_NationalityMaster"].Rows)
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandText = "sp_IE_V1_MP_NationalityMaster";

                        cmd.Parameters.Add(MyParameter("NATIONALITYID", SqlDbType.BigInt, dr[0]));
                        cmd.Parameters.Add(MyParameter("NATIONALITYNAME", SqlDbType.VarChar, dr[1]));

                        int result3 = cmd.ExecuteNonQuery();
                    }
                }
                //-----------------

                //-----------------Remove existing Data
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Delete FROM MP_UnitMaster";
                cmd.ExecuteNonQuery();
                cmd.CommandType = CommandType.StoredProcedure;

                //-----------------

                //-----------------
                if (ds.Tables["MP_UnitMaster"].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables["MP_UnitMaster"].Rows)
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandText = "sp_IE_V1_MP_UnitMaster";

                        cmd.Parameters.Add(MyParameter("UNITID", SqlDbType.BigInt, dr[0]));
                        cmd.Parameters.Add(MyParameter("UNITNAME", SqlDbType.VarChar, dr[1]));

                        int result4 = cmd.ExecuteNonQuery();
                    }
                }
                //-----------------

                //-----------------Remove existing Data
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Delete FROM MP_VictualingMaster";
                cmd.ExecuteNonQuery();
                cmd.CommandType = CommandType.StoredProcedure;

                //-----------------

                //-----------------
                if (ds.Tables["MP_VictualingMaster"].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables["MP_VictualingMaster"].Rows)
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandText = "sp_IE_V1_MP_VictualingMaster";

                        cmd.Parameters.Add(MyParameter("VID", SqlDbType.BigInt, dr[0]));
                        cmd.Parameters.Add(MyParameter("VESSELCODE", SqlDbType.VarChar, dr[1]));
                        cmd.Parameters.Add(MyParameter("EFFECTIVEDATE", SqlDbType.SmallDateTime, dr[2]));
                        cmd.Parameters.Add(MyParameter("VESSELACCOUNT", SqlDbType.Float, dr[3]));
                        cmd.Parameters.Add(MyParameter("OWNERACCOUNT", SqlDbType.Float, dr[4]));
                        cmd.Parameters.Add(MyParameter("CHARTERACCOUNT", SqlDbType.Float, dr[5]));

                        int result5 = cmd.ExecuteNonQuery();
                    }
                }
                //-----------------

                if (ds.Tables["MP_Products"].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables["MP_Products"].Rows)
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandText = "sp_IE_V1_MP_Products";

                        cmd.Parameters.Add(MyParameter("CategoryId", SqlDbType.BigInt, dr[0]));
                        cmd.Parameters.Add(MyParameter("ProductId", SqlDbType.BigInt, dr[1]));
                        cmd.Parameters.Add(MyParameter("ProductCode", SqlDbType.VarChar, dr[2]));
                        cmd.Parameters.Add(MyParameter("ProductName", SqlDbType.VarChar, dr[3]));
                        cmd.Parameters.Add(MyParameter("SupplyUnitId", SqlDbType.Int, dr[4]));
                        cmd.Parameters.Add(MyParameter("IMPACode", SqlDbType.VarChar, dr[5]));
                        cmd.Parameters.Add(MyParameter("Inactive", SqlDbType.Bit, dr[6]));

                        int result6 = cmd.ExecuteNonQuery();
                    }
                }

                if (ds.Tables["MP_Products_Packaging"].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables["MP_Products_Packaging"].Rows)
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandText = "sp_IE_V1_MP_Products_Packaging";

                        cmd.Parameters.Add(MyParameter("ProductId", SqlDbType.BigInt, dr[0]));
                        cmd.Parameters.Add(MyParameter("Qty", SqlDbType.Int, dr[1]));
                        cmd.Parameters.Add(MyParameter("UnitId", SqlDbType.Int, dr[2]));
                        cmd.Parameters.Add(MyParameter("OrderQty", SqlDbType.Float, dr[3]));

                        int result7 = cmd.ExecuteNonQuery();
                    }
                }


                if (ds.Tables["MP_CrewComplementMaster"].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables["MP_CrewComplementMaster"].Rows)
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandText = "sp_IE_V1_MP_CrewComplementMaster";

                        cmd.Parameters.Add(MyParameter("CCId", SqlDbType.BigInt, dr[0]));
                        cmd.Parameters.Add(MyParameter("VesselCode", SqlDbType.VarChar, dr[1]));
                        cmd.Parameters.Add(MyParameter("EffectiveFrom", SqlDbType.DateTime, dr[2]));
                        cmd.Parameters.Add(MyParameter("Updated", SqlDbType.Bit, dr[3]));
                        cmd.Parameters.Add(MyParameter("UpdatedOn", SqlDbType.SmallDateTime, dr[4]));
                        cmd.Parameters.Add(MyParameter("AckRecd", SqlDbType.Bit, dr[5]));
                        cmd.Parameters.Add(MyParameter("AckRecdOn", SqlDbType.SmallDateTime, dr[6]));

                        int result8 = cmd.ExecuteNonQuery();
                    }
                }

                if (ds.Tables["MP_CrewComplementDetails"].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables["MP_CrewComplementDetails"].Rows)
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandText = "sp_IE_V1_MP_CrewComplementDetails";

                        cmd.Parameters.Add(MyParameter("CCId", SqlDbType.BigInt, dr[0]));
                        cmd.Parameters.Add(MyParameter("VesselCode", SqlDbType.VarChar, dr[1]));
                        cmd.Parameters.Add(MyParameter("RankId", SqlDbType.Int, dr[2]));
                        cmd.Parameters.Add(MyParameter("BudgetManning", SqlDbType.Int, dr[3]));
                        cmd.Parameters.Add(MyParameter("BudgetNationality", SqlDbType.Int, dr[4]));

                        int result9 = cmd.ExecuteNonQuery();
                    }
                }

                if (ds.Tables["MP_MealConsumptionMaster"].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables["MP_MealConsumptionMaster"].Rows)
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandText = "sp_IE_V1_MP_MealConsumptionMaster";

                        cmd.Parameters.Add(MyParameter("MealConsumptionId", SqlDbType.BigInt, dr[0]));
                        cmd.Parameters.Add(MyParameter("NationalityId", SqlDbType.BigInt, dr[1]));
                        cmd.Parameters.Add(MyParameter("MealId", SqlDbType.BigInt, dr[2]));
                        cmd.Parameters.Add(MyParameter("Version", SqlDbType.VarChar, dr[3]));
                        cmd.Parameters.Add(MyParameter("EffectiveFrom", SqlDbType.DateTime, dr[4]));

                        int result10 = cmd.ExecuteNonQuery();
                    }
                }

                if (ds.Tables["MP_MealConsumptionDetails"].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables["MP_MealConsumptionDetails"].Rows)
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandText = "sp_IE_V1_MP_MealConsumptionDetails";

                        cmd.Parameters.Add(MyParameter("MealConsumptionDetailId", SqlDbType.BigInt, dr[0]));
                        cmd.Parameters.Add(MyParameter("MealConsumptionId", SqlDbType.BigInt, dr[1]));
                        cmd.Parameters.Add(MyParameter("CategoryId", SqlDbType.BigInt, dr[2]));
                        cmd.Parameters.Add(MyParameter("UnitId", SqlDbType.BigInt, dr[3]));
                        cmd.Parameters.Add(MyParameter("StdConsumption", SqlDbType.Float, dr[4]));

                        int result11 = cmd.ExecuteNonQuery();
                    }
                }

                //------------------ Flushing Log on the basis of last imported packet at other end
                if (LastUpdatedPacketId > 0)
                {
                    cmd.Parameters.Clear();
                    cmd.CommandText = "sp_IE_V1_MP_FlushPacketLog";

                    cmd.Parameters.Add(MyParameter("VesselCode", SqlDbType.VarChar, MNP_VesselCode));
                    cmd.Parameters.Add(MyParameter("PacketID", SqlDbType.Int, LastUpdatedPacketId));

                    int res = cmd.ExecuteNonQuery();
                }

                //------------------

                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Clear();
                cmd.CommandText = "INSERT INTO VSL_MNP_PACKETRECD (VESSELCODE,PACKETID,PACKETNAME,RECDON) VALUES('" + MNP_VesselCode + "'," + PacketId.ToString() + ",'" + Path.GetFileNameWithoutExtension(FileToImport) + "',Getdate())";
                int Res1 = cmd.ExecuteNonQuery();
                if (Res1 <= 0)
                {
                    throw new Exception("Unable to save record in table ( VSL_MNP_PACKETRECD ).");
                }
                // End
                trans.Commit();
                lblMsg.Text="MNP packet imported successfully. File Name : " + FileToImport;
            }
            catch (Exception ex)
            {
                trans.Rollback();
                lblMsg.Text="Unable to import MNP data. File Name : " + FileToImport + ", Error : + " + ex.Message;
            }
            finally
            {
                if (Con.State == ConnectionState.Open) { Con.Close(); }
            }
        }
    }
    //---------------- Purchase Packet Import
    private void Purchase_Import(DataSet ds, string FileToImport)
    {
        SqlTransaction trans;
        SqlConnection Con = new SqlConnection(sqlConnectionString.Replace("Master", "eMANAGER"));
        SqlCommand cmd = new SqlCommand("", Con);

        Con.Open();
        trans = Con.BeginTransaction();
        cmd.Transaction = trans; 

        try
        {

            if (ds.Tables["PURCHSEREQ_ACK_P"].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables["PURCHSEREQ_ACK_P"].Rows)
                {
                    //-------------------------------
                    string[] CommandParameters = getCommandParameters(cmd, "sp_IE_V1_MP_Pro_Rec_Ack");
                    cmd.Parameters.Clear();
                    cmd.CommandText = "sp_IE_V1_MP_Pro_Rec_Ack";
                    cmd.CommandType = CommandType.StoredProcedure;
                    for (int i = 0; i <= CommandParameters.Length - 1; i++)
                    {
                        object data = DBNull.Value;
                        try
                        { data = dr[CommandParameters[i]]; }
                        catch { data = DBNull.Value; }
                        cmd.Parameters.Add(new SqlParameter(CommandParameters[i], data));
                    }
                    //------------------------------
                    int result12 = cmd.ExecuteNonQuery();
                }
            }

            if (ds.Tables["PURCHSEREQ_ACK_S"].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables["PURCHSEREQ_ACK_S"].Rows)
                {
                    //-------------------------------
                    string[] CommandParameters = getCommandParameters(cmd, "sp_IE_V1_MP_Spare_Rec_Ack");
                    cmd.Parameters.Clear();
                    cmd.CommandText = "sp_IE_V1_MP_Spare_Rec_Ack";
                    cmd.CommandType = CommandType.StoredProcedure;
                    for (int i = 0; i <= CommandParameters.Length - 1; i++)
                    {
                        object data = DBNull.Value;
                        try
                        { data = dr[CommandParameters[i]]; }
                        catch { data = DBNull.Value; }
                        cmd.Parameters.Add(new SqlParameter(CommandParameters[i], data));
                    }
                    //------------------------------
                    int result12 = cmd.ExecuteNonQuery();
                }
            }

            if (ds.Tables["PURCHSEREQ_ACK_T"].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables["PURCHSEREQ_ACK_T"].Rows)
                {

                    //-------------------------------
                    string[] CommandParameters = getCommandParameters(cmd, "sp_IE_V1_MP_Store_Rec_Ack");
                    cmd.Parameters.Clear();
                    cmd.CommandText = "sp_IE_V1_MP_Store_Rec_Ack";
                    cmd.CommandType = CommandType.StoredProcedure;
                    for (int i = 0; i <= CommandParameters.Length - 1; i++)
                    {
                        object data = DBNull.Value;
                        try
                        { data = dr[CommandParameters[i]]; }
                        catch { data = DBNull.Value; }
                        cmd.Parameters.Add(new SqlParameter(CommandParameters[i], data));
                    }
                    //------------------------------
                    int result12 = cmd.ExecuteNonQuery();
                }
            }

            trans.Commit();
            lblMsg.Text = "Purchase request acknowledgement packet imported successfully. File Name : " + FileToImport;
        }
        catch (Exception ex)
        {
            trans.Rollback();
            lblMsg.Text = "Unable to import purchase request acknowledgement packet. File Name : " + FileToImport + " Error : " + ex.Message;
        }
        finally
        {
            if (Con.State == ConnectionState.Open) { Con.Close(); }
        }
    } 
    protected string[] getCommandParameters(SqlCommand cmd, string ProcName)
    {
        string[] result;
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.Text;
        //---------------- CHECKING PACKET RECEIVED NO. IS GREATER THAN DB PACKET RECEIVED
        cmd.CommandText = "select replace(parameter_name,'@','') as parameter_name from information_schema.parameters where specific_name='" + ProcName + "' and ltrim(rtrim(parameter_name))<>''";
        DataTable dt = new DataTable();
        SqlDataAdapter adp = new SqlDataAdapter();
        adp.SelectCommand = cmd;
        adp.Fill(dt);
        result = new string[dt.Rows.Count];
        for (int i = 0; i <= dt.Rows.Count - 1; i++)
        {
            result[i] = dt.Rows[i][0].ToString();
        }
        return result;
    }
    //---------------- ER PACKET IMPORT
    private void Import_Into_ER_FILES(DataSet ds, string FileToImport)
    {
        //string IMPORT_FILENAME = Path.GetFileName(FileToImport);

        //string TargetDir = AppDomain.CurrentDomain.BaseDirectory + "Temp_ER";
        //if (Directory.Exists(TargetDir))
        //    Directory.Delete(TargetDir, true);
        //Directory.CreateDirectory(TargetDir);

        //string FileNameWithoutExtension = Path.GetFileNameWithoutExtension(IMPORT_FILENAME);
        //string ts = FileNameWithoutExtension.Substring(FileNameWithoutExtension.LastIndexOf("_") + 1);
        //int IndexofDash = ts.LastIndexOf("-");
        //string FormNo = ts.Substring(0, IndexofDash);

        //if (FileToImport != "")
        //{
        //    string ZipData = FileToImport;
        //    if (File.Exists(ZipData))
        //    {
        //        using (ZipFile zip = ZipFile.Read(ZipData))
        //        {
        //            foreach (ZipEntry ex in zip.EntriesSorted)
        //            {
        //                try
        //                {
        //                    ex.FileName = Path.GetFileName(ex.FileName);
        //                    ex.Extract(TargetDir, ExtractExistingFileAction.OverwriteSilently);
        //                }
        //                catch { continue; }
        //            }
        //        }


        //        string VesselCode = IMPORT_FILENAME.Substring(0, 8).Substring(5);
        //        string strConectionString = sqlConnectionString.Replace("Master", "eMANAGER");
        //        SqlConnection Con = new SqlConnection(strConectionString);
        //        SqlCommand cmdCheck = new SqlCommand("", Con);
        //        SqlDataAdapter Sda = new SqlDataAdapter(cmdCheck);

        //        //---------------- CHECKING PACKET IS FOR SAME VESSEL OR NOT

        //        DataSet DsTemp = new DataSet();
        //        cmdCheck.CommandText = "SELECT * FROM DBO.SETTINGS";
        //        Sda.Fill(DsTemp, "Set");

        //        if (DsTemp.Tables[0].Rows.Count != 1)
        //        {
        //            AddMessage("Settings table data is invalid.");
        //            return;
        //        }
        //        else
        //        {
        //            string In_VesselCode = DsTemp.Tables[0].Rows[0]["SHIPCODE"].ToString();
        //            if (VesselCode != In_VesselCode)
        //            {
        //                lblMsg.Text = "Importing packet's VESSEL is not matching with VESSEL.";
        //                return;
        //            }
        //        }

        //        try
        //        {
        //            //------------------
        //            if (txteReportsPath.Text.Trim() == "")
        //            {
        //                MessageBox.Show("Invalid path selected for eReports. Please update eReports path first in 'Communication Settings'.", "Please Check..", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                return;
        //            }
        //            else
        //            {
        //                string Destpath = txteReportsPath.Text.Trim() + "\\" + FormNo + "\\Documents\\Office\\";
        //                if (!(Directory.Exists(Destpath)))
        //                    Directory.CreateDirectory(Destpath);

        //                foreach (string f in Directory.GetFiles(TargetDir))
        //                {
        //                    string FinalLocation = Destpath + Path.GetFileName(f);
        //                    if (File.Exists(FinalLocation))
        //                        File.Delete(FinalLocation);

        //                    File.Move(f, FinalLocation);
        //                }

        //                lblMsg.Text = "eReport(FILES) imported successfully. File Name : " + FileToImport;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            lblMsg.Text = "Unable to import eReport(FILES). File Name : " + FileToImport + ". Error : " + ex.Message;
        //        }
        //    }
        //}
    }
    private void Import_Into_ER(DataSet ds, string FileToImport, string FormNo)
    {
        
            SqlTransaction trans;
            SqlConnection Con = new SqlConnection(sqlConnectionString.Replace("Master", "eMANAGER"));
            SqlCommand cmdCheck = new SqlCommand("", Con);
            SqlDataAdapter Sda = new SqlDataAdapter(cmdCheck);

            Con.Open();
            trans = Con.BeginTransaction();

            string VesselCode = ds.Tables["ER_" + FormNo + "_Report_Office"].Rows[0]["VesselCode"].ToString();
            int ReportId = Convert.ToInt32(ds.Tables["ER_" + FormNo + "_Report_Office"].Rows[0]["ReportId"]); 

            cmdCheck.Transaction = trans;
            cmdCheck.CommandType = CommandType.Text;

            //---------------- CHECKING PACKET IS FOR SAME VESSEL OR NOT

            DataSet DsTemp = new DataSet();
            cmdCheck.CommandText = "SELECT * FROM DBO.SETTINGS";
            Sda.Fill(DsTemp, "Set");


            string In_VesselCode = DsTemp.Tables[0].Rows[0]["SHIPCODE"].ToString();
            if (VesselCode != In_VesselCode)
            {
                lblMsg.Text = "Importing packet's VESSEL is not matching with VESSEL.";
                return;
            }

        ////------------------------ 

        try
            {
                SqlCommand cmd = new SqlCommand("", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = trans;

                // -------------------------- S133 ----------------------------------
                if (FormNo == "S133")
                {
                    //----------------

                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "DELETE FROM [dbo].[ER_S133_Report_Office] WHERE REPORTID=" + ReportId.ToString();
                    cmd.ExecuteNonQuery();

                    //----------------

                    cmd.CommandType = CommandType.StoredProcedure;


                    if (ds.Tables["ER_S133_Report_Office"].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables["ER_S133_Report_Office"].Rows)
                        {

                            //-------------------------------
                            string[] CommandParameters = getCommandParameters(cmd, "ER_S133_IMPORT_ER_S133_Report_Office");
                            cmd.Parameters.Clear();
                            cmd.CommandText = "ER_S133_IMPORT_ER_S133_Report_Office";
                            cmd.CommandType = CommandType.StoredProcedure;
                            for (int i = 0; i <= CommandParameters.Length - 1; i++)
                            {
                                object data = DBNull.Value;
                                try
                                { data = dr[CommandParameters[i]]; }
                                catch { data = DBNull.Value; }
                                cmd.Parameters.Add(new SqlParameter(CommandParameters[i], data));
                            }
                            //------------------------------
                            int result = cmd.ExecuteNonQuery();
                        }
                    }
                }
                //------------------------------

                // -------------------------- G118 ----------------------------------
                if (FormNo == "G118")
                {
                    //----------------

                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "DELETE FROM [dbo].[Accident_Report_Office] WHERE REPORTID=" + ReportId.ToString();
                    cmd.ExecuteNonQuery();

                    //----------------

                    cmd.CommandType = CommandType.StoredProcedure;


                    if (ds.Tables["Accident_Report_Office"].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables["Accident_Report_Office"].Rows)
                        {

                            //-------------------------------
                            string[] CommandParameters = getCommandParameters(cmd, "Accident_IMPORT_Accident_Report_Office");
                            cmd.Parameters.Clear();
                            cmd.CommandText = "Accident_IMPORT_Accident_Report_Office";
                            cmd.CommandType = CommandType.StoredProcedure;
                            for (int i = 0; i <= CommandParameters.Length - 1; i++)
                            {
                                object data = DBNull.Value;
                                try
                                { data = dr[CommandParameters[i]]; }
                                catch { data = DBNull.Value; }
                                cmd.Parameters.Add(new SqlParameter(CommandParameters[i], data));
                            }
                            //------------------------------
                            int result = cmd.ExecuteNonQuery();
                        }
                    }
                }
                //------------------------------

                trans.Commit();
                lblMsg.Text = "eReport imported successfully. File Name : " + FileToImport;
            }
            catch (Exception ex)
            {
                trans.Rollback();
                lblMsg.Text = "Unable to import eReport data. File Name : " + FileToImport + ". Error : " + ex.Message;
            }
            finally
            {
                if (Con.State == ConnectionState.Open) { Con.Close(); }
            }
        
    }
    private void Import_Into_ER_S115(DataSet ds, string FileToImport, string FormNo)
    {

        SqlTransaction trans;
        SqlConnection Con = new SqlConnection(sqlConnectionString.Replace("Master", "eMANAGER"));
        SqlCommand cmdCheck = new SqlCommand("", Con);
        SqlDataAdapter Sda = new SqlDataAdapter(cmdCheck);

        Con.Open();
        trans = Con.BeginTransaction();

        string VesselCode = ds.Tables["ER_S115_Report" ].Rows[0]["VesselCode"].ToString();
        string ReportNo = ds.Tables["ER_S115_Report"].Rows[0]["ReportNo"].ToString();
        int ReportId = Convert.ToInt32(ds.Tables["ER_S115_Report"].Rows[0]["ReportId"]);

        cmdCheck.Transaction = trans;
        cmdCheck.CommandType = CommandType.Text;

        //---------------- CHECKING PACKET IS FOR SAME VESSEL OR NOT

        DataSet DsTemp = new DataSet();
        cmdCheck.CommandText = "SELECT * FROM DBO.SETTINGS";
        Sda.Fill(DsTemp, "Set");
        try
        {
            SqlCommand cmd = new SqlCommand("", Con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Transaction = trans;

            // -------------------------- S115 ----------------------------------            
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "DELETE FROM [dbo].[ER_S115_Report_Office] WHERE VESSELCODE='" + VesselCode + "' AND REPORTID=" + ReportId.ToString();
                cmd.CommandText = "DELETE FROM [dbo].[ER_Report_Documents_Office] WHERE VESSELCODE='" + VesselCode + "' AND REPORTID=" + ReportId.ToString();
                cmd.ExecuteNonQuery();

                //----------------

                cmd.CommandType = CommandType.StoredProcedure;

                if (ds.Tables["ER_S115_Report"].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables["ER_S115_Report"].Rows)
                    {
                        //-------------------------------
                        string[] CommandParameters = getCommandParameters(cmd, "ER_S115_IMPORT_ER_S115_Report");
                        cmd.Parameters.Clear();
                        cmd.CommandText = "ER_S115_IMPORT_ER_S115_Report";
                        cmd.CommandType = CommandType.StoredProcedure;
                        for (int i = 0; i <= CommandParameters.Length - 1; i++)
                        {
                            object data = DBNull.Value;
                            try
                            { data = dr[CommandParameters[i]]; }
                            catch { data = DBNull.Value; }
                            cmd.Parameters.Add(new SqlParameter(CommandParameters[i], data));
                        }
                        //------------------------------
                        int result = cmd.ExecuteNonQuery();
                    }
                }
                if (ds.Tables["ER_S115_InjuryToPerson"].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables["ER_S115_InjuryToPerson"].Rows)
                    {
                        //-------------------------------
                        string[] CommandParameters = getCommandParameters(cmd, "ER_S115_IMPORT_ER_S115_InjuredCrewDetails");
                        cmd.Parameters.Clear();
                        cmd.CommandText = "ER_S115_IMPORT_ER_S115_InjuredCrewDetails";
                        cmd.CommandType = CommandType.StoredProcedure;
                        for (int i = 0; i <= CommandParameters.Length - 1; i++)
                        {
                            object data = DBNull.Value;
                            try
                            { data = dr[CommandParameters[i]]; }
                            catch { data = DBNull.Value; }
                            cmd.Parameters.Add(new SqlParameter(CommandParameters[i], data));
                        }
                        //------------------------------
                        int result = cmd.ExecuteNonQuery();
                    }

                }
                /////////////////////////////////////////////////////////////////////////////////
                if (ds.Tables["ER_S115_Report_Office"].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables["ER_S115_Report_Office"].Rows)
                    {

                        //-------------------------------
                        string[] CommandParameters = getCommandParameters(cmd, "ER_S115_IMPORT_ER_S115_Report_Office");
                        cmd.Parameters.Clear();
                        cmd.CommandText = "ER_S115_IMPORT_ER_S115_Report_Office";
                        cmd.CommandType = CommandType.StoredProcedure;
                        for (int i = 0; i <= CommandParameters.Length - 1; i++)
                        {
                            object data = DBNull.Value;
                            try
                            { data = dr[CommandParameters[i]]; }
                            catch { data = DBNull.Value; }
                            cmd.Parameters.Add(new SqlParameter(CommandParameters[i], data));
                        }
                        //------------------------------
                        int result = cmd.ExecuteNonQuery();
                    }
                }
                //---
                if (ds.Tables["ER_Report_Documents_Office"].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables["ER_Report_Documents_Office"].Rows)
                    {
                        //-------------------------------
                        string[] CommandParameters = getCommandParameters(cmd, "ER_IMPORT_Report_Documents_OfficeShip");
                        cmd.Parameters.Clear();
                        cmd.CommandText = "ER_IMPORT_Report_Documents_OfficeShip";
                        cmd.CommandType = CommandType.StoredProcedure;
                        for (int i = 0; i <= CommandParameters.Length - 1; i++)
                        {
                            object data = DBNull.Value;
                            try
                            { data = dr[CommandParameters[i]]; }
                            catch { data = DBNull.Value; }

                            if (CommandParameters[i].Trim() == "OFFICE_SHIP")
                                cmd.Parameters.Add(new SqlParameter(CommandParameters[i], "O"));
                            else
                                cmd.Parameters.Add(new SqlParameter(CommandParameters[i], data));
                        }
                        //------------------------------
                        int result = cmd.ExecuteNonQuery();
                    }
                }


                if (VesselCode != CurrentVessel)
                {
                    try
                    {
                        Common.Set_Procedures("[DBO].[sp_Insert_Communication_Export]");
                        Common.Set_ParameterLength(5);
                        Common.Set_Parameters(
                            new MyParameter("@VesselCode", CurrentVessel),
                            new MyParameter("@RecordType", "ER_S115_SHIPACK"),
                            new MyParameter("@RecordId", VesselCode + "_" + ReportId.ToString()),
                            new MyParameter("@RecordNo", "Email Blast ACK ( " + ReportNo + " )"),
                            new MyParameter("@CreatedBy", Session["FullName"].ToString().Trim())
                        );

                        DataSet ds1 = new DataSet();
                        ds1.Clear();
                        Boolean res;
                        res = Common.Execute_Procedures_IUD(ds1);
                        if (res)
                        {
                            if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
                            {
                                lblMsg.Text = ds1.Tables[0].Rows[0][0].ToString();
                            }
                            else
                            {
                                lblMsg.Text = "Email Blast Imported successfully. Acknowledgement sent for export successfully.";
                            }

                        }
                        else
                        {
                            lblMsg.Text = "Email Blast Imported successfully. Unable to send acknowledgement for export.Error : " + Common.getLastError();
                        }
                    }
                    catch (Exception ex)
                    {
                        lblMsg.Text = "Email Blast Imported successfully. Unable to send acknowledgement for export.Error : " + ex.Message;
                    }
                }
            

            trans.Commit();
            lblMsg.Text = "eReport-S115/Email Blast imported successfully. File Name : " + FileToImport;
        }
        catch (Exception ex)
        {
            trans.Rollback();
            lblMsg.Text = "Unable to import eReport-S115/Email Blast data. File Name : " + FileToImport + ". Error : " + ex.Message;
        }
        finally
        {
            if (Con.State == ConnectionState.Open) { Con.Close(); }
        }

    }
   
    // RCA PACKET IMPORT 
    private void Import_Into_RCA(DataSet ds, string FileToImport)
    {
        if (FileToImport != "")
        {
            SqlTransaction trans;
            SqlConnection Con = new SqlConnection(sqlConnectionString.Replace("Master", "eMANAGER"));
            SqlCommand cmdCheck = new SqlCommand("", Con);
            SqlDataAdapter Sda = new SqlDataAdapter(cmdCheck);

            Con.Open();
            trans = Con.BeginTransaction();

            string VesselCode = ds.Tables["ER_S115_Report_RCA"].Rows[0]["VesselCode"].ToString();
            int ReportId = Convert.ToInt32(ds.Tables["ER_S115_Report_RCA"].Rows[0]["ReportId"]);

            cmdCheck.Transaction = trans;
            cmdCheck.CommandType = CommandType.Text;

            //---------------- CHECKING PACKET IS FOR SAME VESSEL OR NOT

            //DataSet DsTemp = new DataSet();
            //cmdCheck.CommandText = "SELECT * FROM DBO.SETTINGS";
            //Sda.Fill(DsTemp, "Set");

            //string In_VesselCode = DsTemp.Tables[0].Rows[0]["SHIPCODE"].ToString();
            //if (VesselCode != In_VesselCode)
            //{
            //    lblMsg.Text = "Importing packet's VESSEL is not matching with VESSEL.";
            //    return;
            //}


            ////------------------------ 

            try
            {
                SqlCommand cmd = new SqlCommand("", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = trans;
                //------------------------------------------------      
                cmd.CommandType = CommandType.StoredProcedure;
                //------------------------------------------------      
                if (ds.Tables["ER_S115_Report_RCA"].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables["ER_S115_Report_RCA"].Rows)
                    {
                        //-------------------------------
                        string[] CommandParameters = getCommandParameters(cmd, "SP_SHIPIMPORT_ER_S115_Report_RCA");
                        cmd.Parameters.Clear();
                        cmd.CommandText = "SP_SHIPIMPORT_ER_S115_Report_RCA";
                        cmd.CommandType = CommandType.StoredProcedure;
                        for (int i = 0; i <= CommandParameters.Length - 1; i++)
                        {
                            object data = DBNull.Value;
                            try
                            { data = dr[CommandParameters[i]]; }
                            catch { data = DBNull.Value; }
                            cmd.Parameters.Add(new SqlParameter(CommandParameters[i], data));
                        }
                        //------------------------------
                        int result = cmd.ExecuteNonQuery();
                    }
                }
                trans.Commit();
                lblMsg.Text = "RCA imported successfully. File Name : " + FileToImport;
            }
            catch (Exception ex)
            {
                trans.Rollback();
                lblMsg.Text = "Unable to import RCA data. File Name : " + FileToImport + ". Error : " + ex.Message;
            }
            finally
            {
                if (Con.State == ConnectionState.Open) { Con.Close(); }
            }
        }

    }

    // BD RCA PACKET IMPORT 
    private void Import_Into_BD_RCA(DataSet ds, string FileToImport)
    {
        if (FileToImport != "")
        {
            SqlTransaction trans;
            SqlConnection Con = new SqlConnection(sqlConnectionString.Replace("Master", "eMANAGER"));
            SqlCommand cmdCheck = new SqlCommand("", Con);
            SqlDataAdapter Sda = new SqlDataAdapter(cmdCheck);

            Con.Open();
            trans = Con.BeginTransaction();

            string VesselCode = ds.Tables["VSL_BreakDownRemarks"].Rows[0]["VesselCode"].ToString();
            int ReportId = Convert.ToInt32(ds.Tables["VSL_BreakDownRemarks"].Rows[0]["ReportId"]);

            cmdCheck.Transaction = trans;
            cmdCheck.CommandType = CommandType.Text;

            //---------------- CHECKING PACKET IS FOR SAME VESSEL OR NOT

            //DataSet DsTemp = new DataSet();
            //cmdCheck.CommandText = "SELECT * FROM DBO.SETTINGS";
            //Sda.Fill(DsTemp, "Set");

            //string In_VesselCode = DsTemp.Tables[0].Rows[0]["SHIPCODE"].ToString();
            //if (VesselCode != In_VesselCode)
            //{
            //    lblMsg.Text = "Importing packet's VESSEL is not matching with VESSEL.";
            //    return;
            //}


            ////------------------------ 

            try
            {
                SqlCommand cmd = new SqlCommand("", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = trans;
                //------------------------------------------------      
                cmd.CommandType = CommandType.StoredProcedure;
                //------------------------------------------------      
                if (ds.Tables["VSL_BreakDownRemarks"].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables["VSL_BreakDownRemarks"].Rows)
                    {
                        //-------------------------------
                        string[] CommandParameters = getCommandParameters(cmd, "sp_Ship_Import_VSL_BreakDownRemarks");
                        cmd.Parameters.Clear();
                        cmd.CommandText = "sp_Ship_Import_VSL_BreakDownRemarks";
                        cmd.CommandType = CommandType.StoredProcedure;
                        for (int i = 0; i <= CommandParameters.Length - 1; i++)
                        {
                            object data = DBNull.Value;
                            try
                            { data = dr[CommandParameters[i]]; }
                            catch { data = DBNull.Value; }
                            cmd.Parameters.Add(new SqlParameter(CommandParameters[i], data));
                        }
                        //------------------------------
                        int result = cmd.ExecuteNonQuery();
                    }
                }
                trans.Commit();
                lblMsg.Text = "Breakdown RCA imported successfully. File Name : " + FileToImport;
            }
            catch (Exception ex)
            {
                trans.Rollback();
                lblMsg.Text = "Unable to import Breakdown RCA data. File Name : " + FileToImport + ". Error : " + ex.Message;
            }
            finally
            {
                if (Con.State == ConnectionState.Open) { Con.Close(); }
            }
        }

    }
    
    //---------------- VIQ PACKET IMPORT
    private void Import_Into_VIQ(DataSet ds, string FileToImport)
    {
        if (FileToImport != "")
        {

            SqlTransaction trans;
            SqlConnection Con = new SqlConnection(sqlConnectionString.Replace("Master", "eMANAGER"));
            SqlCommand cmdCheck = new SqlCommand("", Con);
            SqlDataAdapter Sda = new SqlDataAdapter(cmdCheck);

            Con.Open();
            trans = Con.BeginTransaction();

            string VesselCode = ds.Tables["VIQ_VIQMaster"].Rows[0]["VesselCode"].ToString();
            int VIQId = Convert.ToInt32(ds.Tables["VIQ_VIQMaster"].Rows[0]["VIQId"]);

            cmdCheck.Transaction = trans;
            cmdCheck.CommandType = CommandType.Text;

            //---------------- CHECKING PACKET IS FOR SAME VESSEL OR NOT

            DataSet DsTemp = new DataSet();
            cmdCheck.CommandText = "SELECT * FROM DBO.SETTINGS";
            Sda.Fill(DsTemp, "Set");

            string In_VesselCode = DsTemp.Tables[0].Rows[0]["SHIPCODE"].ToString();
            if (VesselCode != In_VesselCode)
            {
                lblMsg.Text = "Importing packet's VESSEL is not matching with VESSEL.";
                return;
            }


            ////------------------------ 

            try
            {
                SqlCommand cmd = new SqlCommand("", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = trans;
                //------------------------------------------------      
                cmd.CommandType = CommandType.StoredProcedure;
                //------------------------------------------------      
                if (ds.Tables["VIQ_VIQMaster"].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables["VIQ_VIQMaster"].Rows)
                    {
                        //-------------------------------
                        string[] CommandParameters = getCommandParameters(cmd, "VIQ_VSL_IMPORT_VIQMaster");
                        cmd.Parameters.Clear();
                        cmd.CommandText = "VIQ_VSL_IMPORT_VIQMaster";
                        cmd.CommandType = CommandType.StoredProcedure;
                        for (int i = 0; i <= CommandParameters.Length - 1; i++)
                        {
                            object data = DBNull.Value;
                            try
                            { data = dr[CommandParameters[i]]; }
                            catch { data = DBNull.Value; }
                            cmd.Parameters.Add(new SqlParameter(CommandParameters[i], data));
                        }
                        //------------------------------
                        int result = cmd.ExecuteNonQuery();
                    }
                }
                //------------------------------------------------      
                if (ds.Tables["VIQ_VIQDetails"].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables["VIQ_VIQDetails"].Rows)
                    {
                        //-------------------------------
                        string[] CommandParameters = getCommandParameters(cmd, "VIQ_VSL_IMPORT_VIQDetails");
                        cmd.Parameters.Clear();
                        cmd.CommandText = "VIQ_VSL_IMPORT_VIQDetails";
                        cmd.CommandType = CommandType.StoredProcedure;
                        for (int i = 0; i <= CommandParameters.Length - 1; i++)
                        {
                            object data = DBNull.Value;
                            try
                            { data = dr[CommandParameters[i]]; }
                            catch { data = DBNull.Value; }
                            cmd.Parameters.Add(new SqlParameter(CommandParameters[i], data));
                        }
                        //------------------------------
                        int result = cmd.ExecuteNonQuery();
                    }
                }
                //------------------------------------------------      
                if (ds.Tables["VIQ_VIQDetailsRanks"].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables["VIQ_VIQDetailsRanks"].Rows)
                    {
                        //-------------------------------
                        string[] CommandParameters = getCommandParameters(cmd, "VIQ_VSL_IMPORT_VIQDetailsRanks");
                        cmd.Parameters.Clear();
                        cmd.CommandText = "VIQ_VSL_IMPORT_VIQDetailsRanks";
                        cmd.CommandType = CommandType.StoredProcedure;
                        for (int i = 0; i <= CommandParameters.Length - 1; i++)
                        {
                            object data = DBNull.Value;
                            try
                            { data = dr[CommandParameters[i]]; }
                            catch { data = DBNull.Value; }
                            cmd.Parameters.Add(new SqlParameter(CommandParameters[i], data));
                        }
                        //------------------------------
                        int result = cmd.ExecuteNonQuery();
                    }
                }
                //------------------------------------------------      
                if (ds.Tables["VIQ_VIQDetailsRanks_Inv"].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables["VIQ_VIQDetailsRanks_Inv"].Rows)
                    {
                        //-------------------------------
                        string[] CommandParameters = getCommandParameters(cmd, "VIQ_VSL_IMPORT_VIQDetailsRanks_Inv");
                        cmd.Parameters.Clear();
                        cmd.CommandText = "VIQ_VSL_IMPORT_VIQDetailsRanks_Inv";
                        cmd.CommandType = CommandType.StoredProcedure;
                        for (int i = 0; i <= CommandParameters.Length - 1; i++)
                        {
                            object data = DBNull.Value;
                            try
                            { data = dr[CommandParameters[i]]; }
                            catch { data = DBNull.Value; }
                            cmd.Parameters.Add(new SqlParameter(CommandParameters[i], data));
                        }
                        //------------------------------
                        int result = cmd.ExecuteNonQuery();
                    }
                }
                //------------------------------------------------      

                trans.Commit();
                lblMsg.Text = "VIQ imported successfully. File Name : " + FileToImport;
            }
            catch (Exception ex)
            {
                trans.Rollback();
                lblMsg.Text = "Unable to import VIQ data. File Name : " + FileToImport + ". Error : " + ex.Message;
            }
            finally
            {
                if (Con.State == ConnectionState.Open) { Con.Close(); }
            }
        }
        
    }
    //---------------- INVENTORY OPENING BALANCE IMPORT
    private void Import_Into_Inventory_OpBal(DataSet ds, string FileToImport)
    {
        if (FileToImport != "")
        {

            SqlTransaction trans;
            SqlConnection Con = new SqlConnection(sqlConnectionString.Replace("Master", "eMANAGER"));
            SqlCommand cmdCheck = new SqlCommand("", Con);
            SqlDataAdapter Sda = new SqlDataAdapter(cmdCheck);

            Con.Open();
            trans = Con.BeginTransaction();

            string VesselCode = ds.Tables["OpeningInventory_Master"].Rows[0]["VesselCode"].ToString();
            int Month =Common.CastAsInt32( ds.Tables["OpeningInventory_Master"].Rows[0]["Month"]);
            int Year = Common.CastAsInt32(ds.Tables["OpeningInventory_Master"].Rows[0]["Year"]);

            cmdCheck.Transaction = trans;
            cmdCheck.CommandType = CommandType.Text;

            //---------------- CHECKING PACKET IS FOR SAME VESSEL OR NOT

            DataSet DsTemp = new DataSet();
            cmdCheck.CommandText = "SELECT * FROM DBO.SETTINGS";
            Sda.Fill(DsTemp, "Set");

            string In_VesselCode = DsTemp.Tables[0].Rows[0]["SHIPCODE"].ToString();
            if (VesselCode != In_VesselCode)
            {
                lblMsg.Text = "Importing packet's VESSEL is not matching with VESSEL.";
                return;
            }
            ////------------------------ 
            try
            {
                SqlCommand cmd = new SqlCommand("", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = trans;
                //------------------------------------------------      
                cmd.CommandType = CommandType.StoredProcedure;
                //------------------------------------------------      
                if (ds.Tables["OpeningInventory_Master"].Rows.Count > 0)
                {
                    //-------------------------------
                    cmd.Parameters.Clear();
                    cmd.CommandText = "INV_OPBAL_IMPORT_VSL_OpeningInventory_Master";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("VesselCode", VesselCode));
                    cmd.Parameters.Add(new SqlParameter("Month", Month));
                    cmd.Parameters.Add(new SqlParameter("Year", Year));
                    SqlParameter pp=new SqlParameter("InventoryId", 0);
                    pp.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(pp);
                    //------------------------------
                    cmd.ExecuteNonQuery();
                    int InventoryId = Common.CastAsInt32(pp.Value);
                    if(InventoryId>0)
                    {
                        //------------------------------------------------      
                        if (ds.Tables["OpeningInventory_Details"].Rows.Count > 0)
                        {
                            foreach (DataRow dr in ds.Tables["OpeningInventory_Details"].Rows)
                            {
                                object ProductId = dr["ProductId"];
                                object Quantity = dr["Quantity"];
                                //-------------------------------
                                cmd.Parameters.Clear();
                                cmd.CommandText = "INV_OPBAL_IMPORT_VSL_OpeningInventory_Details";
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.Add(new SqlParameter("VesselCode", VesselCode));
                                cmd.Parameters.Add(new SqlParameter("InventoryId", InventoryId));
                                cmd.Parameters.Add(new SqlParameter("ProductId", ProductId));
                                cmd.Parameters.Add(new SqlParameter("Quantity", Quantity));
                                //------------------------------
                                cmd.ExecuteNonQuery();
                            }
                        }
                        trans.Commit();
                    }
                }
              
                //------------------------------------------------      

               
                lblMsg.Text = "Inventory Opening Balance imported successfully. File Name : " + FileToImport;
            }
            catch (Exception ex)
            {
                trans.Rollback();
                lblMsg.Text = "Unable to import Inventory Opening Balance. File Name : " + FileToImport + ". Error : " + ex.Message;
            }
            finally
            {
                if (Con.State == ConnectionState.Open) { Con.Close(); }
            }
        }
        
    }
    
    //---------------- LFI PACKET IMPORT
    private void Import_Into_LFI(DataSet ds, string FileToImport)
    {
        if (FileToImport != "")
        {
            SqlTransaction trans;
            SqlConnection Con = new SqlConnection(sqlConnectionString.Replace("Master", "eMANAGER"));
            SqlCommand cmdCheck = new SqlCommand("", Con);
            SqlDataAdapter Sda = new SqlDataAdapter(cmdCheck);

            Con.Open();
            trans = Con.BeginTransaction();


            ////------------------------ 

            try
            {

                SqlCommand cmd = new SqlCommand("", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = trans;
                //------------------------------------------------      
                cmd.CommandType = CommandType.StoredProcedure;
                //------------------------------------------------      
                if (ds.Tables["LFI"].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables["LFI"].Rows)
                    {
                        //-------------------------------
                        string[] CommandParameters = getCommandParameters(cmd, "InsertUpdateLFI_VSL");
                        cmd.Parameters.Clear();
                        cmd.CommandText = "InsertUpdateLFI_VSL";
                        cmd.CommandType = CommandType.StoredProcedure;
                        for (int i = 0; i <= CommandParameters.Length - 1; i++)
                        {
                            object data = DBNull.Value;
                            try
                            { data = dr[CommandParameters[i]]; }
                            catch { data = DBNull.Value; }
                            cmd.Parameters.Add(new SqlParameter(CommandParameters[i], data));
                        }
                        //------------------------------
                        int result = cmd.ExecuteNonQuery();
                    }
                }

                trans.Commit();
                lblMsg.Text = "LFI imported successfully. File Name : " + FileToImport;
                
            }
            catch (Exception ex)
            {
                trans.Rollback();
                lblMsg.Text = "Unable to import LFI. File Name : " + FileToImport + ". Error : " + ex.Message;
            }
            finally
            {
                if (Con.State == ConnectionState.Open) { Con.Close(); }
            }
           
        }

    }
    
    //---------------- CRITICAL EQUIPMENT SHUTDOWN REQUEST APPROVAL
    private void Import_Into_CRIT_EQ_SH_REQ_O_(DataSet ds, string FileToImport)
    {
        if (FileToImport != "")
        {
            SqlTransaction trans;
            SqlConnection Con = new SqlConnection(sqlConnectionString.Replace("Master", "eMANAGER"));
            SqlCommand cmdCheck = new SqlCommand("", Con);
            SqlDataAdapter Sda = new SqlDataAdapter(cmdCheck);

            Con.Open();
            trans = Con.BeginTransaction();
            ////------------------------ 

            try
            {

                SqlCommand cmd = new SqlCommand("", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = trans;
                //------------------------------------------------      
                cmd.CommandType = CommandType.StoredProcedure;
                //------------------------------------------------      
                if (ds.Tables["VSL_CriticalShutdownApproval"].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables["VSL_CriticalShutdownApproval"].Rows)
                    {
                        //-------------------------------
                        string[] CommandParameters = getCommandParameters(cmd, "sp_IE_VSL_CriticalShutdownApproval");
                        cmd.Parameters.Clear();
                        cmd.CommandText = "sp_IE_VSL_CriticalShutdownApproval";
                        cmd.CommandType = CommandType.StoredProcedure;
                        for (int i = 0; i <= CommandParameters.Length - 1; i++)
                        {
                            object data = DBNull.Value;
                            try
                            { data = dr[CommandParameters[i]]; }
                            catch { data = DBNull.Value; }
                            cmd.Parameters.Add(new SqlParameter(CommandParameters[i], data));
                        }
                        //------------------------------
                        int result = cmd.ExecuteNonQuery();
                    }
                }

                trans.Commit();
                lblMsg.Text = "Critical equipment shutdown request approval imported successfully. File Name : " + FileToImport;
                
            }
            catch (Exception ex)
            {
                trans.Rollback();
                lblMsg.Text = "Unable to import critical equipment shutdown request approval. File Name : " + FileToImport + ". Error : " + ex.Message;
            }
            finally
            {
                if (Con.State == ConnectionState.Open) { Con.Close(); }
            }

        }

    }
    //---------------- FC PACKET IMPORT
    private void Import_Into_FC(DataSet ds, string FileToImport)
    {
        if (FileToImport != "")
        {
            SqlTransaction trans;
            SqlConnection Con = new SqlConnection(sqlConnectionString.Replace("Master", "eMANAGER"));
            SqlCommand cmdCheck = new SqlCommand("", Con);
            SqlDataAdapter Sda = new SqlDataAdapter(cmdCheck);

            Con.Open();
            trans = Con.BeginTransaction();


            ////------------------------ 

            try
            {

                SqlCommand cmd = new SqlCommand("", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = trans;
                //------------------------------------------------      
                cmd.CommandType = CommandType.StoredProcedure;
                //------------------------------------------------      
                if (ds.Tables["FocusCamp"].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables["FocusCamp"].Rows)
                    {
                        //-------------------------------
                        string[] CommandParameters = getCommandParameters(cmd, "InsertUpdateFocusCamp_VSL");
                        cmd.Parameters.Clear();
                        cmd.CommandText = "InsertUpdateFocusCamp_VSL";
                        cmd.CommandType = CommandType.StoredProcedure;
                        for (int i = 0; i <= CommandParameters.Length - 1; i++)
                        {
                            object data = DBNull.Value;
                            try
                            { data = dr[CommandParameters[i]]; }
                            catch { data = DBNull.Value; }
                            cmd.Parameters.Add(new SqlParameter(CommandParameters[i], data));
                        }
                        //------------------------------
                        int result = cmd.ExecuteNonQuery();
                    }
                }

                trans.Commit();
                lblMsg.Text = "FocusCamp imported successfully. File Name : " + FileToImport;
                
            }
            catch (Exception ex)
            {
                trans.Rollback();
                lblMsg.Text = "Unable to import FocusCamp. File Name : " + FileToImport + ". Error : " + ex.Message;
            }
            finally
            {
                if (Con.State == ConnectionState.Open) { Con.Close(); }
            }

        }

    }

    //---------------- CMS CREW LIST PACKET IMPORT
    private void Import_Into_CrewList(DataSet ds, string FileToImport)
    {
        if (FileToImport != "")
        {
            SqlTransaction trans;
            SqlConnection Con = new SqlConnection(sqlConnectionString.Replace("Master", "eMANAGER"));
            SqlCommand cmdCheck = new SqlCommand("", Con);
            SqlDataAdapter Sda = new SqlDataAdapter(cmdCheck);

            Con.Open();
            trans = Con.BeginTransaction();
            ////------------------------ 

            try
            {

                SqlCommand cmd = new SqlCommand("", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = trans;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "truncate table PMS_CREW_HISTORY";
                cmd.ExecuteNonQuery();
                //------------------------------------------------      
                cmd.CommandType = CommandType.StoredProcedure;
                //------------------------------------------------      
                if(ds.Tables.Contains("PMS_CREW_HISTORY"))
                    if (ds.Tables["PMS_CREW_HISTORY"].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables["PMS_CREW_HISTORY"].Rows)
                        {
                            //-------------------------------
                            string[] CommandParameters = getCommandParameters(cmd, "sp_ship_import_PMS_CREW_HISTORY");
                            cmd.Parameters.Clear();
                            cmd.CommandText = "sp_ship_import_PMS_CREW_HISTORY";
                            cmd.CommandType = CommandType.StoredProcedure;
                            for (int i = 0; i <= CommandParameters.Length - 1; i++)
                            {
                                object data = DBNull.Value;
                                try
                                { data = dr[CommandParameters[i]]; }
                                catch { data = DBNull.Value; }
                                cmd.Parameters.Add(new SqlParameter(CommandParameters[i], data));
                            }
                            //------------------------------
                            int result = cmd.ExecuteNonQuery();
                        }
                    }
                //------------------------------------------------ 
                if (ds.Tables.Contains("MP_VSL_SparesReqMaster"))
                    if (ds.Tables["MP_VSL_SparesReqMaster"].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables["MP_VSL_SparesReqMaster"].Rows)
                        {
                            //-------------------------------
                            string[] CommandParameters = getCommandParameters(cmd, "sp_ship_import_MP_VSL_SparesReqMaster");
                            cmd.Parameters.Clear();
                            cmd.CommandText = "sp_ship_import_MP_VSL_SparesReqMaster";
                            cmd.CommandType = CommandType.StoredProcedure;
                            for (int i = 0; i <= CommandParameters.Length - 1; i++)
                            {
                                object data = DBNull.Value;
                                try
                                { data = dr[CommandParameters[i]]; }
                                catch { data = DBNull.Value; }
                                cmd.Parameters.Add(new SqlParameter(CommandParameters[i], data));
                            }
                            //------------------------------
                            int result = cmd.ExecuteNonQuery();
                        }
                    }
                //------------------------------------------------ 
                if (ds.Tables.Contains("MP_VSL_StoreReqMaster"))
                    if (ds.Tables["MP_VSL_StoreReqMaster"].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables["MP_VSL_StoreReqMaster"].Rows)
                        {
                            //-------------------------------
                            string[] CommandParameters = getCommandParameters(cmd, "sp_ship_import_MP_VSL_StoreReqMaster");
                            cmd.Parameters.Clear();
                            cmd.CommandText = "sp_ship_import_MP_VSL_StoreReqMaster";
                            cmd.CommandType = CommandType.StoredProcedure;
                            for (int i = 0; i <= CommandParameters.Length - 1; i++)
                            {
                                object data = DBNull.Value;
                                try
                                { data = dr[CommandParameters[i]]; }
                                catch { data = DBNull.Value; }
                                cmd.Parameters.Add(new SqlParameter(CommandParameters[i], data));
                            }
                            //------------------------------
                            int result = cmd.ExecuteNonQuery();
                        }
                    }
                //------------------------------------------------ 
                if (ds.Tables.Contains("MP_VSL_StoreReqMaster1"))
                    if (ds.Tables["MP_VSL_StoreReqMaster1"].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables["MP_VSL_StoreReqMaster1"].Rows)
                        {
                            //-------------------------------
                            string[] CommandParameters = getCommandParameters(cmd, "sp_ship_import_MP_VSL_StoreReqMaster1");
                            cmd.Parameters.Clear();
                            cmd.CommandText = "sp_ship_import_MP_VSL_StoreReqMaster1";
                            cmd.CommandType = CommandType.StoredProcedure;
                            for (int i = 0; i <= CommandParameters.Length - 1; i++)
                            {
                                object data = DBNull.Value;
                                try
                                { data = dr[CommandParameters[i]]; }
                                catch { data = DBNull.Value; }
                                cmd.Parameters.Add(new SqlParameter(CommandParameters[i], data));
                            }
                            //------------------------------
                            int result = cmd.ExecuteNonQuery();
                        }
                    }
                //------------------------------------------------ 
                if (ds.Tables.Contains("MP_VSL_ProvisionMaster"))
                    if (ds.Tables["MP_VSL_ProvisionMaster"].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables["MP_VSL_ProvisionMaster"].Rows)
                        {
                            //-------------------------------
                            string[] CommandParameters = getCommandParameters(cmd, "sp_ship_import_MP_VSL_ProvisionMaster");
                            cmd.Parameters.Clear();
                            cmd.CommandText = "sp_ship_import_MP_VSL_ProvisionMaster";
                            cmd.CommandType = CommandType.StoredProcedure;
                            for (int i = 0; i <= CommandParameters.Length - 1; i++)
                            {
                                object data = DBNull.Value;
                                try
                                { data = dr[CommandParameters[i]]; }
                                catch { data = DBNull.Value; }
                                cmd.Parameters.Add(new SqlParameter(CommandParameters[i], data));
                            }
                            //------------------------------
                            int result = cmd.ExecuteNonQuery();
                        }
                    }
                //------------------------------------------------ 
                cmd.Parameters.Clear();
                cmd.CommandText = "sp_ship_import_PMS_CREW_HISTORY_RESETLOG";
                cmd.ExecuteNonQuery();
                //------------------------------------------------ 
                trans.Commit();
                lblMsg.Text = "CMS Crew List imported successfully. File Name : " + FileToImport;

            }
            catch (Exception ex)
            {
                trans.Rollback();
                lblMsg.Text = "Unable to import CMS Crew List. File Name : " + FileToImport + ". Error : " + ex.Message;
            }
            finally
            {
                if (Con.State == ConnectionState.Open) { Con.Close(); }
            }

        }

    }

    

    //---------------- REG PACKET IMPORT
    private void Import_Into_REG(DataSet ds, string FileToImport)
    {
        if (FileToImport != "")
        {
            SqlTransaction trans;
            SqlConnection Con = new SqlConnection(sqlConnectionString.Replace("Master", "eMANAGER"));
            SqlCommand cmdCheck = new SqlCommand("", Con);
            SqlDataAdapter Sda = new SqlDataAdapter(cmdCheck);

            Con.Open();
            trans = Con.BeginTransaction();


            ////------------------------ 

            try
            {
                SqlCommand cmd = new SqlCommand("", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = trans;
                //------------------------------------------------      
                cmd.CommandType = CommandType.StoredProcedure;
                //------------------------------------------------      
                if (ds.Tables["Reg_Regulation"].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables["Reg_Regulation"].Rows)
                    {
                        //-------------------------------
                        string[] CommandParameters = getCommandParameters(cmd, "REG_InsertUpdateRegulation_VSL");
                        cmd.Parameters.Clear();
                        cmd.CommandText = "REG_InsertUpdateRegulation_VSL";
                        cmd.CommandType = CommandType.StoredProcedure;
                        for (int i = 0; i <= CommandParameters.Length - 1; i++)
                        {
                            object data = DBNull.Value;
                            try
                            { data = dr[CommandParameters[i]]; }
                            catch { data = DBNull.Value; }
                            cmd.Parameters.Add(new SqlParameter(CommandParameters[i], data));
                        }
                        //------------------------------
                        int result = cmd.ExecuteNonQuery();
                    }
                }

                trans.Commit();
                lblMsg.Text = "Regulation imported successfully. File Name : " + FileToImport;

            }
            catch (Exception ex)
            {
                trans.Rollback();
                lblMsg.Text = "Unable to import Regulation. File Name : " + FileToImport + ". Error : " + ex.Message;
            }
            finally
            {
                if (Con.State == ConnectionState.Open) { Con.Close(); }
            }

        }

    }
    //---------------- CIRCULAR PACKET IMPORT
    private void Import_Into_CIRCULAR(DataSet ds, string FileToImport)
    {
        if (FileToImport != "")
        {
            SqlTransaction trans;
            SqlConnection Con = new SqlConnection(sqlConnectionString.Replace("Master", "eMANAGER"));
            SqlCommand cmdCheck = new SqlCommand("", Con);
            SqlDataAdapter Sda = new SqlDataAdapter(cmdCheck);

            Con.Open();
            trans = Con.BeginTransaction();


            ////------------------------ 

            try
            {

                SqlCommand cmd = new SqlCommand("", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = trans;
                //------------------------------------------------      
                cmd.CommandType = CommandType.StoredProcedure;
                //------------------------------------------------      
                if (ds.Tables["Cir_Circular"].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables["Cir_Circular"].Rows)
                    {
                        //-------------------------------
                        string[] CommandParameters = getCommandParameters(cmd, "CIR_InsertUpdateCircular_VSL");
                        cmd.Parameters.Clear();
                        cmd.CommandText = "CIR_InsertUpdateCircular_VSL";
                        cmd.CommandType = CommandType.StoredProcedure;
                        for (int i = 0; i <= CommandParameters.Length - 1; i++)
                        {
                            object data = DBNull.Value;
                            try
                            { data = dr[CommandParameters[i]]; }
                            catch { data = DBNull.Value; }
                            cmd.Parameters.Add(new SqlParameter(CommandParameters[i], data));
                        }
                        //------------------------------
                        int result = cmd.ExecuteNonQuery();
                    }
                }

                trans.Commit();
                lblMsg.Text = "Circular imported successfully. File Name : " + FileToImport;

            }
            catch (Exception ex)
            {
                trans.Rollback();
                lblMsg.Text = "Unable to import circular. File Name : " + FileToImport + ". Error : " + ex.Message;
            }
            finally
            {
                if (Con.State == ConnectionState.Open) { Con.Close(); }
            }

        }

    }
    //---------------- MWUC PACKET IMPORT
    private void Import_Into_MWUC(DataSet ds, string FileToImport)
    {
        if (FileToImport != "")
        {
            SqlTransaction trans;
            SqlConnection Con = new SqlConnection(sqlConnectionString.Replace("Master", "eMANAGER"));
            SqlCommand cmdCheck = new SqlCommand("", Con);
            SqlDataAdapter Sda = new SqlDataAdapter(cmdCheck);

            Con.Open();
            trans = Con.BeginTransaction();


            ////------------------------ 

            try
            {

                SqlCommand cmd = new SqlCommand("", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = trans;
                //------------------------------------------------      
                cmd.CommandType = CommandType.StoredProcedure;
                //------------------------------------------------      
                if (ds.Tables["MWUC_MASTER"].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables["MWUC_MASTER"].Rows)
                    {
                        //-------------------------------
                        string[] CommandParameters = getCommandParameters(cmd, "MWUC_ImportMaster");
                        cmd.Parameters.Clear();
                        cmd.CommandText = "MWUC_ImportMaster";
                        cmd.CommandType = CommandType.StoredProcedure;
                        for (int i = 0; i <= CommandParameters.Length - 1; i++)
                        {
                            object data = DBNull.Value;
                            try
                            {
                                if (i == 0)
                                {
                                    data = "S";
                                }
                                else
                                {
                                    data = dr[CommandParameters[i]];
                                }
                            }
                            catch { data = DBNull.Value; }
                            cmd.Parameters.Add(new SqlParameter(CommandParameters[i], data));
                        }
                        //------------------------------
                        int result = cmd.ExecuteNonQuery();
                    }
                }

                if (ds.Tables["MWUC_DETAILS"].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables["MWUC_DETAILS"].Rows)
                    {
                        //-------------------------------
                        string[] CommandParameters = getCommandParameters(cmd, "MWUC_ImportDetails");
                        cmd.Parameters.Clear();
                        cmd.CommandText = "MWUC_ImportDetails";
                        cmd.CommandType = CommandType.StoredProcedure;
                        for (int i = 0; i <= CommandParameters.Length - 1; i++)
                        {
                            object data = DBNull.Value;
                            try
                            {
                                if (i == 0)
                                {
                                    data = "S";
                                }
                                else
                                {
                                    data = dr[CommandParameters[i]];
                                }
                            }
                            catch { data = DBNull.Value; }
                            cmd.Parameters.Add(new SqlParameter(CommandParameters[i], data));
                        }
                        //------------------------------
                        int result = cmd.ExecuteNonQuery();
                    }
                }

                trans.Commit();
                lblMsg.Text = "MWUC imported successfully. File Name : " + FileToImport;

            }
            catch (Exception ex)
            {
                trans.Rollback();
                lblMsg.Text = "Unable to import MWUC. File Name : " + FileToImport + ". Error : " + ex.Message;
            }
            finally
            {
                if (Con.State == ConnectionState.Open) { Con.Close(); }
            }

        }

    }
    //---------------- SCM PACKET IMPORT
    private void Import_Into_SCM(DataSet ds, string FileToImport)
    {
        if (FileToImport != "")
        {
            SqlTransaction trans;
            SqlConnection Con = new SqlConnection(sqlConnectionString.Replace("Master", "eMANAGER"));
            SqlCommand cmdCheck = new SqlCommand("", Con);
            SqlDataAdapter Sda = new SqlDataAdapter(cmdCheck);

            Con.Open();
            trans = Con.BeginTransaction();


            ////------------------------ 

            try
            {

                SqlCommand cmd = new SqlCommand("", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = trans;
                //------------------------------------------------      
                cmd.CommandType = CommandType.StoredProcedure;
                //------------------------------------------------      
                if (ds.Tables["SCM_MASTER"].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables["SCM_MASTER"].Rows)
                    {
                        //-------------------------------
                        string[] CommandParameters = getCommandParameters(cmd, "SCM_IMPORT_SCM_MASTER");
                        cmd.Parameters.Clear();
                        cmd.CommandText = "SCM_IMPORT_SCM_MASTER";
                        cmd.CommandType = CommandType.StoredProcedure;
                        for (int i = 0; i <= CommandParameters.Length - 1; i++)
                        {
                            object data = DBNull.Value;
                            try
                            {
                                if (i == 0)
                                {
                                    data = "S";
                                }
                                else
                                {
                                    data = dr[CommandParameters[i]];
                                }
                            }
                            catch { data = DBNull.Value; }
                            cmd.Parameters.Add(new SqlParameter(CommandParameters[i], data));
                        }
                        //------------------------------
                        int result = cmd.ExecuteNonQuery();
                    }
                }

                trans.Commit();
                lblMsg.Text = "SCM imported successfully. File Name : " + FileToImport;

            }
            catch (Exception ex)
            {
                trans.Rollback();
                lblMsg.Text = "Unable to import SCM. File Name : " + FileToImport + ". Error : " + ex.Message;
            }
            finally
            {
                if (Con.State == ConnectionState.Open) { Con.Close(); }
            }

        }

    }
    //---------------- RISK TEMPLATE IMPORT
    private void RiskTemplates_Import(DataSet ds, string FileToImport)
    {
        if (FileToImport != "")
        {
            SqlTransaction trans;
            SqlConnection Con = new SqlConnection(sqlConnectionString.Replace("Master", "eMANAGER"));
            SqlCommand cmdCheck = new SqlCommand("", Con);
            SqlDataAdapter Sda = new SqlDataAdapter(cmdCheck);

            Con.Open();
            trans = Con.BeginTransaction();


            ////------------------------ 

            try
            {

                SqlCommand cmd = new SqlCommand("", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = trans;
                //------------------------------------------------      
                cmd.CommandType = CommandType.StoredProcedure;
                //------------------------------------------------      
                if (ds.Tables["EV_TemplateMaster"].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables["EV_TemplateMaster"].Rows)
                    {
                        //-------------------------------
                        string[] CommandParameters = getCommandParameters(cmd, "EV_Import_TEMPLATE_MASTER");
                        cmd.Parameters.Clear();
                        cmd.CommandText = "EV_Import_TEMPLATE_MASTER";
                        cmd.CommandType = CommandType.StoredProcedure;
                        for (int i = 0; i <= CommandParameters.Length - 1; i++)
                        {
                            object data = DBNull.Value;
                            try
                            {
                                data = dr[CommandParameters[i]];
                            }
                            catch { data = DBNull.Value; }
                            cmd.Parameters.Add(new SqlParameter(CommandParameters[i], data));
                        }
                        //------------------------------
                        int result = cmd.ExecuteNonQuery();
                    }
                }
                if (ds.Tables["EV_Template_Tasks"].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables["EV_Template_Tasks"].Rows)
                    {
                        //-------------------------------
                        string[] CommandParameters = getCommandParameters(cmd, "EV_Import_TEMPLATE_TASKS");
                        cmd.Parameters.Clear();
                        cmd.CommandText = "EV_Import_TEMPLATE_TASKS";
                        cmd.CommandType = CommandType.StoredProcedure;
                        for (int i = 0; i <= CommandParameters.Length - 1; i++)
                        {
                            object data = DBNull.Value;
                            try
                            {
                                data = dr[CommandParameters[i]];
                            }
                            catch { data = DBNull.Value; }
                            cmd.Parameters.Add(new SqlParameter(CommandParameters[i], data));
                        }
                        //------------------------------
                        int result = cmd.ExecuteNonQuery();
                    }
                }
                if (ds.Tables["EV_TemplateDetails"].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables["EV_TemplateDetails"].Rows)
                    {
                        //-------------------------------
                        string[] CommandParameters = getCommandParameters(cmd, "EV_Import_TEMPLATE_DETAILS");
                        cmd.Parameters.Clear();
                        cmd.CommandText = "EV_Import_TEMPLATE_DETAILS";
                        cmd.CommandType = CommandType.StoredProcedure;
                        for (int i = 0; i <= CommandParameters.Length - 1; i++)
                        {
                            object data = DBNull.Value;
                            try
                            {
                                data = dr[CommandParameters[i]];
                            }
                            catch { data = DBNull.Value; }
                            cmd.Parameters.Add(new SqlParameter(CommandParameters[i], data));
                        }
                        //------------------------------
                        int result = cmd.ExecuteNonQuery();
                    }
                }

                trans.Commit();
                lblMsg.Text = "RCA Template imported successfully. File Name : " + FileToImport;

            }
            catch (Exception ex)
            {
                trans.Rollback();
                lblMsg.Text = "Unable to import RCA Template. File Name : " + FileToImport + ". Error : " + ex.Message;
            }
            finally
            {
                if (Con.State == ConnectionState.Open) { Con.Close(); }
            }
        }

    }
    //---------------- RISK DATA IMPORT
    private void RiskData_Import(DataSet ds, string FileToImport)
    {
        if (FileToImport != "")
        {
            SqlTransaction trans;
            SqlConnection Con = new SqlConnection(sqlConnectionString.Replace("Master", "eMANAGER"));
            SqlCommand cmdCheck = new SqlCommand("", Con);
            SqlDataAdapter Sda = new SqlDataAdapter(cmdCheck);

            Con.Open();
            trans = Con.BeginTransaction();


            ////------------------------ 

            try
            {

                SqlCommand cmd = new SqlCommand("", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = trans;
                //------------------------------------------------      
                cmd.CommandType = CommandType.StoredProcedure;
                //------------------------------------------------      
                if (ds.Tables["EV_VSL_RiskMaster"].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables["EV_VSL_RiskMaster"].Rows)
                    {
                        //-------------------------------
                        string[] CommandParameters = getCommandParameters(cmd, "EV_Import_VSL_RiskMaster");
                        cmd.Parameters.Clear();
                        cmd.CommandText = "EV_Import_VSL_RiskMaster";
                        cmd.CommandType = CommandType.StoredProcedure;
                        for (int i = 0; i <= CommandParameters.Length - 1; i++)
                        {
                            object data = DBNull.Value;
                            try
                            {
                                data = dr[CommandParameters[i]];
                            }
                            catch { data = DBNull.Value; }
                            cmd.Parameters.Add(new SqlParameter(CommandParameters[i], data));
                        }
                        //------------------------------
                        int result = cmd.ExecuteNonQuery();
                    }
                }

                trans.Commit();
                lblMsg.Text = "RCA Data imported successfully. File Name : " + FileToImport;

            }
            catch (Exception ex)
            {
                trans.Rollback();
                lblMsg.Text = "Unable to import RCA Data. File Name : " + FileToImport + ". Error : " + ex.Message;
            }
            finally
            {
                if (Con.State == ConnectionState.Open) { Con.Close(); }
            }
        }

    }
    //---------------- SMSFORMS Import 
    private void SMSFORMS_Import(DataSet ds, string FileToImport)
    {
        if (FileToImport != "")
        {
            SqlTransaction trans;
            SqlConnection Con = new SqlConnection(sqlConnectionString.Replace("Master", "eMANAGER"));
            SqlCommand cmdCheck = new SqlCommand("", Con);
            SqlDataAdapter Sda = new SqlDataAdapter(cmdCheck);

            Con.Open();
            trans = Con.BeginTransaction();
            ////------------------------ 
            try
            {

                SqlCommand cmd = new SqlCommand("", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = trans;
                //------------------------------------------------      
                cmd.CommandType = CommandType.StoredProcedure;
                //------------------------------------------------      
                if (ds.Tables["SMS_Forms"].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables["SMS_Forms"].Rows)
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandText = "SHIP_IU_SMS_Forms";

                        cmd.Parameters.Add(MyParameter("FORMID", SqlDbType.BigInt, dr[0]));
                        cmd.Parameters.Add(MyParameter("FORMNAME", SqlDbType.VarChar, dr[1]));
                        cmd.Parameters.Add(MyParameter("FORMNO", SqlDbType.VarChar, dr[2]));
                        cmd.Parameters.Add(MyParameter("VERSIONNO", SqlDbType.VarChar, dr[3]));
                        cmd.Parameters.Add(MyParameter("FILENAME", SqlDbType.VarChar, dr[4]));
                        cmd.Parameters.Add(MyParameter("FILECONTENT", SqlDbType.Binary, dr[5]));
                        cmd.Parameters.Add(MyParameter("CREATEDON", SqlDbType.DateTime, dr[6]));
                        cmd.Parameters.Add(MyParameter("UPDATED", SqlDbType.Bit, dr[7]));
                        cmd.Parameters.Add(MyParameter("UPDATEDON", SqlDbType.DateTime, dr[8]));

                        int result = cmd.ExecuteNonQuery();
                    }
                }

                trans.Commit();
                lblMsg.Text = "SMS FORMS Data imported successfully. File Name : " + FileToImport;

            }
            catch (Exception ex)
            {
                trans.Rollback();
                lblMsg.Text = "Unable to import SMS FORMS Data. File Name : " + FileToImport + ". Error : " + ex.Message;
            }
            finally
            {
                if (Con.State == ConnectionState.Open) { Con.Close(); }
            }
        }

    }
    
    //------- SMTP Settings Section --------------
    public void ShowSMTPSettings()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM [MailSettings] WHERE ID=1");
        if (dt != null && dt.Rows.Count > 0)
        {
            Id = Common.CastAsInt32(dt.Rows[0]["id"]);
            txtToAddress.Text = dt.Rows[0]["ToAddress"].ToString();
            txtSenderAddress.Text = dt.Rows[0]["SenderAddress"].ToString();
            txtSenderUserName.Text = dt.Rows[0]["SenderUserName"].ToString();
            txtSenderPassword.Attributes.Add("value", dt.Rows[0]["senderPass"].ToString());
            txtMailClient.Text = dt.Rows[0]["MailClient"].ToString();
            ddlEncryption.SelectedValue = dt.Rows[0]["Mail_Encryption"].ToString();
            txtPort.Text = dt.Rows[0]["Port"].ToString();
            ddlMode.SelectedValue = dt.Rows[0]["PacketMode"].ToString();
            btnImport2.Visible = (ddlMode.SelectedValue == "A");                
        }
    }
    protected void btnSaveSettings_Click(object sender, EventArgs e)
    {
        try
        {
            Common.Set_Procedures("sp_InsertUpdateMailSettings");
            Common.Set_ParameterLength(8);
            Common.Set_Parameters(new MyParameter("Id", (Id==0 ? 1 : Id)),
                                    new MyParameter("SenderAddress", txtSenderAddress.Text.Trim()),
                                    new MyParameter("SenderUserName", txtSenderUserName.Text.Trim()),
                                    new MyParameter("senderPass", txtSenderPassword.Text.Trim()),
                                    new MyParameter("MailClient", txtMailClient.Text.Trim()),
                                    new MyParameter("Port", txtPort.Text.Trim()),
                                    new MyParameter("Mail_Encryption", ddlEncryption.SelectedValue),
                                    new MyParameter("PacketMode", ddlMode.SelectedValue));
            DataSet ds = new DataSet();
            if (Common.Execute_Procedures_IUD(ds))
            {
                ShowSMTPSettings();
                lblMsg.Text = "SMTP settings saved successfully";
            }
            else
            {
                lblMsg.Text = "Unable to save data. Error : " + Common.getLastError();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Unable to save data. Error : " + ex.Message;
        }
    }
    protected void btnSendTestMail_Click(object sender, EventArgs e)
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM MailSettings");
        if (dt.Rows.Count > 0)
        {
                string toaddress,senderaddress, senderusername, senderpass, mailclient;
                int port;
                bool encryption;
                toaddress = dt.Rows[0]["Toaddress"].ToString();
                senderaddress = dt.Rows[0]["SenderAddress"].ToString();
                senderusername = dt.Rows[0]["SenderUserName"].ToString();
                senderpass = dt.Rows[0]["senderPass"].ToString();
                mailclient = dt.Rows[0]["MailClient"].ToString();
                port = Common.CastAsInt32(dt.Rows[0]["Port"]);
                encryption = dt.Rows[0]["Mail_Encryption"].ToString()=="SSL";
                string[] CCAddress={ "emanager@energiossolutions.com" };
                string[] BCCAddress={ };
                string message="Dear Receiver, <br><br>This is a test email from PMS system to check the settings applied by you.<br><br>If it received to you successfully you mails settings are working fine.<br><br>Thanks<br><b>SHIP PMS System</b>";
                string Error = "";
                bool result=SendMail(senderaddress, toaddress, CCAddress, BCCAddress, "PMS Application SMTP Test Mail", message, senderusername, senderpass, mailclient, port, encryption,out Error);
                if (result)
                {
                    lblMsg.Text = "Mail sent successfully. Please check your mail for confirmation.";
                }
                else
                {
                    lblMsg.Text = "Unable to send mail. Error : " + Error;
                }
        }
        else
        {
            lblMsg.Text = "Settings not found. Please save settings before send mail.";
        }
    }

    //------- Rest Hour Settings Section --------------
    protected void chkIna_OnCheckedChanged(object sender, EventArgs e)
    {
        ShowRestHourCrew();
    }
    public void ShowRestHourCrew()
    {
        string _connectionstring = sqlConnectionString.Replace("Master", "eShip");
        SqlConnection conn = new SqlConnection(_connectionstring);
        string sql = "";
        if(chkIna.Checked)
            sql="select C.CREWNUMBER,C.CREWNUMBER + ' : ' + CREWNAME AS FULLNAME from [dbo].[CP_VesselCrewList] C";
        else
            sql = "select C.CREWNUMBER,C.CREWNUMBER + ' : ' + CREWNAME + ' - ' + R.RANKCODE AS FULLNAME from [dbo].[CP_VesselCrewList] C INNER JOIN [CP_VesselCrewSignOnOff] S ON C.CrewNumber=S.CrewNumber AND S.SignOffDt is null INNER JOIN [dbo].[CP_Rank] R ON S.RANKID=R.RANKID";
        
        SqlCommand cmd = new SqlCommand(sql, conn);
        SqlDataAdapter adp = new SqlDataAdapter();
        adp.SelectCommand = cmd;
        DataTable dt = new DataTable();
        adp.Fill(dt);
        ddlCrew.DataSource = dt;
        ddlCrew.DataTextField = "FULLNAME";
        ddlCrew.DataValueField= "CREWNUMBER";
        ddlCrew.DataBind();
        ddlCrew.Items.Insert(0,new ListItem("< Select Crew > ", ""));
    }
    protected void btnbtnUpdateCrewNumber_Click(object sender, EventArgs e)
    {
        Regex rv=new Regex(@"^[SY]\d\d\d\d\d$");
        txtCrew.Text = txtCrew.Text.ToUpper();

        if (ddlCrew.SelectedIndex <= 0)
        {
            lblMsg.Text = "Please select Crew# to continue.";
            return;
        }
        try
        {
            if (rv.IsMatch(txtCrew.Text))
            {

                

                string _connectionstring = sqlConnectionString.Replace("Master", "eShip");
                SqlConnection conn = new SqlConnection(_connectionstring);
                SqlDataAdapter adp = new SqlDataAdapter();
                SqlTransaction trans;
                SqlCommand cmd = new SqlCommand("SELECT CREWNUMBER FROM CP_VesselCrewList WHERE CrewNumber='" + txtCrew.Text.Trim() + "'", conn);
                //----------------------------------
                adp.SelectCommand = cmd;
                cmd.CommandType = CommandType.Text;
                DataTable dtcrew=new DataTable();
                adp.Fill(dtcrew);
                if (dtcrew.Rows.Count > 0)
                {
                    lblMsg.Text = "Same Crew# is already exists.";
                    return;
                }
                //----------------------------------
                cmd.CommandText="UpdateCrewNumber";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CrewNumber", (object)ddlCrew.SelectedValue));
                cmd.Parameters.Add(new SqlParameter("@ToCrewNumber", (object)txtCrew.Text));
                conn.Open();
                trans = conn.BeginTransaction();
                cmd.Transaction = trans;
                try
                {
                    cmd.ExecuteNonQuery();
                    txtCrew.Text = "";
                    trans.Commit();
                    ShowRestHourCrew();
                    lblMsg.Text = "Crew# updated successfully";
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    lblMsg.Text = "Unable to update Crew#. Error : " + ex.Message;
                }
                finally 
                {
                    conn.Close();
                }
            }
            else
            {
                lblMsg.Text = "Please enter a valid Crew#";
                return;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Unable to update Crew#. Error : " + ex.Message;
        }
    }
   
    public bool SendMail(string FromMail, string toAddress, string[] CCAddresses, string[] BCCAddresses, string Subject, string BodyText,string senderusername,string senderpass,string HostName,int Port,bool SSL,out string Error)
    {
        Error = "";
        try
        {
            MailMessage objMessage = new MailMessage();
            SmtpClient objSmtpClient = new SmtpClient(HostName,Port);
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
    public bool SendMailWithAttachment(string FromMail, string toAddress, string[] CCAddresses, string[] BCCAddresses, string Subject, string BodyText, Stream AttachmentStream,string AttachmentFileName, string senderusername, string senderpass, string HostName, int Port, bool SSL, out string Error)
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
                if (AttachmentStream!=null)
                {
                    Attachment attachFile = new Attachment(AttachmentStream,AttachmentFileName);
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
    protected void ExportFileToResponse(string FilePath)
    {
        Response.Clear();
        Response.ContentType = "application/zip";
        Response.AddHeader("Content-Type", "application/zip");
        Response.AddHeader("Content-Disposition", "inline;filename=" + Path.GetFileName(FilePath));
        Response.WriteFile(FilePath);
        Response.End();
    }

}