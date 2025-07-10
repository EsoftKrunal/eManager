using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Ionic.Zip;
using System.Data.SqlClient;
using System.Net.Mail;

public partial class MWCommunication1 : System.Web.UI.Page
{
    AuthenticationManager Auth;
    public int EmpId
    {
        get
        {
            return Common.CastAsInt32(ViewState["EmpId"]);
        }
        set
        {
            ViewState["EmpId"] = value;
        }
    }
    public bool Action
    {
        get
        {
            return Convert.ToBoolean(ViewState["Action"]);
        }
        set
        {
            ViewState["Action"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        int UserId = 0;
        UserId = int.Parse(Session["loginid"].ToString());

        Auth = new AuthenticationManager(1057, UserId, ObjectType.Page);

        Action = Auth.IsAdd;

        if (!(Auth.IsView))
        {
            Response.Redirect("NotAuthorized.aspx");
        }

        lblMessage.Text = "";
        lblMsg.Text = "";
        if (!Page.IsPostBack)
        {
            ddlVessel.Items.Clear();
            EmpId = Common.CastAsInt32(Session["loginid"]);
            DataTable DT = Common.Execute_Procedures_Select_ByQuery("select * FROM dbo.Vessel Where Vesselstatusid=1 and VesselId in (Select VesselId from UserVesselRelation with(nolock) where Loginid = " + Convert.ToInt32(Session["loginid"].ToString()) + ")  ORDER BY vesselname");
            ddlVessel.DataSource = DT;
            ddlVessel.DataTextField = "vesselname";
            ddlVessel.DataValueField = "vesselId";
            ddlVessel.DataBind();
            ddlVessel.Items.Insert(0, new ListItem("< - - SELECT VESSEL - - >", "0"));
            ddlVessel_OnSelectedIndexChanged(sender, e);
        }
    }
    
    
    //---------------------- Events 
    protected void lnkAction_OnClick(object sender, EventArgs e)
    {
        ImageButton lnk = (ImageButton)sender;
        //LinkButton lnk = (LinkButton)sender;
        HiddenField hfManualID = (HiddenField)lnk.Parent.FindControl("hfManualID");
        HiddenField hfSectionID = (HiddenField)lnk.Parent.FindControl("hfSectionID");

        // check - send manula to all vessel if not send
        string Sql = " Insert into DBO.SMS_APP_COM_ManualDetails(VesselID,ManualID,SectionID,SNO,Scheduled,AckStatus)   " +
                     " Select VesselTypeID," + hfManualID.Value + ",'" + hfSectionID.Value + "',0,0,'P' from DBO.SMS_ManualVesselType Where ManualID=" + hfManualID.Value + "  " +
                     " And VesselTypeID not in (Select VesselID from DBO.SMS_APP_COM_ManualDetails Where ManualID=" + hfManualID.Value + " And SectionID='" + hfSectionID.Value + "')";
        
        DataTable Tempdt = Common.Execute_Procedures_Select_ByQuery(Sql);



        dvPOPUPSendManuals.Visible = true;
        BindSendManual(Common.CastAsInt32(hfManualID.Value), hfSectionID.Value);
    }

    protected void btnScheduleFullManual_Click(object sender, EventArgs e)
    {
        if (ddlVessel.SelectedIndex <= 0)
        {
            lblMessage.Text = "Please select vessel.";
            ddlVessel.Focus();
            return;
        }
        if (ddlManuals.SelectedIndex <= 0)
        {
            lblMessage.Text = "Please select manual.";
            ddlManuals.Focus();
            return;
        }

        try
        {
            //foreach (RepeaterItem ri in rptPendingForApprovalRequest.Items)
            //{
            //    CheckBox ch = ((CheckBox)ri.FindControl("chkSelect"));
            //    if (ch.Checked)
            //    {
            //     }
            //}
            //int ManualId = Common.CastAsInt32(((HiddenField)ri.FindControl("hfdManualId")).Value);

            Common.Execute_Procedures_Select_ByQuery("EXEC DBO.SMS_SCHEDULE_WHOLE_MANUAL " + ddlVessel.SelectedValue + "," + ddlManuals.SelectedValue + ",'" + Session["UserName"].ToString() + "'");
            lblMessage.Text = "Scheduled Successfully.";
        }
        catch
        {
            lblMessage.Text = "Unable to schedule.";
        }
    }
    protected void btnScheduleChanges_Click(object sender, EventArgs e)
    {
        if (ddlVessel.SelectedIndex > 0 && ddlManuals.SelectedIndex > 0)
        {
            bool AnyChecked = false;
            foreach (RepeaterItem Itm in rptPendingForApprovalRequest.Items)
            {
                HiddenField hfVesselID = (HiddenField)Itm.FindControl("hfVesselID");
                HiddenField hfManualID = (HiddenField)Itm.FindControl("hfManualID");
                HiddenField hfSectionID = (HiddenField)Itm.FindControl("hfSectionID");
                CheckBox chk = (CheckBox)Itm.FindControl("chkSelect");
                if (chk.Checked)
                {
                    AnyChecked = true;
                    string Sql = "Update DBO.SMS_APP_COM_ManualDetails Set Scheduled=1,SendBy=NULL,SentDate=null,AckOn=null,AckBy=NULL " + " Where VesselID=" + hfVesselID.Value + " And ManualID=" + hfManualID.Value + " And SectionID='" + hfSectionID.Value + "'";
                    DataTable dt = Common.Execute_Procedures_Select_ByQuery(Sql);
                }
            }
            if (AnyChecked)
            {
                BindGrid();
                lblMessage.Text = "| Scheduled successfully.";
            }
            else
            {
                lblMessage.Text = "| Please select rows to schedule.";
            }
        }
    }
    protected void btnSaveScheduleData_SendApproval_OnClick(object sender, EventArgs e)
    {
        try
        {
            HiddenField hfVesselID =new HiddenField();
            HiddenField hfManualID = new HiddenField();
            HiddenField hfSectionID = new HiddenField();
            foreach (RepeaterItem Itm in rptSendManuals.Items)
            {
                hfVesselID = (HiddenField)Itm.FindControl("hfVesselID");
                hfManualID = (HiddenField)Itm.FindControl("hfManualID");
                hfSectionID = (HiddenField)Itm.FindControl("hfSectionID");
                CheckBox chk = (CheckBox)Itm.FindControl("chkScheduled");

                string Sql = "Update DBO.SMS_APP_COM_ManualDetails Set Scheduled=" + ((chk.Checked) ? 1 : 0) + ",SendBy=NULL,SentDate=null,AckOn=null,AckBy=NULL " + " Where VesselID=" + hfVesselID.Value + " And ManualID=" + hfManualID.Value + " And SectionID='" + hfSectionID.Value + "'";
                DataTable dt = Common.Execute_Procedures_Select_ByQuery(Sql);                
            }            
            lblMsg.Text = "Record updated successfully.";
            BindSendManual(Common.CastAsInt32(hfManualID.Value), hfSectionID.Value);
                        
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error while saving. "+ex.Message;
        }
    }

    protected void btnBack1_Click(object sender, EventArgs e)
    {

    }
    protected void btnClosePopup_SendApproval_OnClick(object sender, EventArgs e)
    {
        BindGrid();
        dvPOPUPSendManuals.Visible = false;
    }
    protected void ddlPendingForApprovalmanuals_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGrid();
    }

    protected void rad_mode_CheckChanged(object sender, EventArgs e)
    {
        BindGrid();
    }
    protected void ddlVessel_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        ddlManuals.Items.Clear();
        if (ddlVessel.SelectedIndex != 0)
        {
            DataTable DT = Common.Execute_Procedures_Select_ByQuery("select * FROM  dbo.SMS_ManualMaster WHERE MANUALID IN (SELECT MANUALID FROM DBO.SMS_ManualVesselType WHERE VESSELTYPEID=" + ddlVessel.SelectedValue + ") ORDER BY MANUALNAME");
            ddlManuals.DataSource = DT;
            ddlManuals.DataTextField = "ManualName";
            ddlManuals.DataValueField = "ManualId";
            ddlManuals.DataBind();
            ddlManuals.Items.Insert(0, new ListItem("< - - SELECT MANUAL - - >", "0"));
            ddlPendingForApprovalmanuals_SelectedIndexChanged(sender, e);
        }
        else
        {
            ddlManuals.Items.Clear();
            ddlManuals.Items.Insert(0, new ListItem("< - - SELECT MANUAL - - >", "0"));
        }

        BindGrid();
        
        //if (ddlPendingForApprovalmanuals.SelectedIndex != 0)
        //    dvVessel.Visible = true;
        //else
        //    dvVessel.Visible = false;
    }

    //---------------------- Function
    protected void BindGrid()
    {
        string SQL = "SELECT ROW_NUMBER() OVER(ORDER BY MD.MANUALID,MD.SECTIONID) AS SNO,MD.MANUALID,MD.SECTIONID,MM.MANUALNAME,MD.SECTIONID,HEADING,[FILENAME],SVERSION,MODIFIEDbY,MODIFIEDON,APPROVED,ApprovedOn, " +
                  "COMD.Scheduled, " +
                  "COMD.SendBy, " +
                  "COMD.SentDate, " +
                  "(Case COMD.AckStatus When 'P' then ''  When 'S' then 'Packet Sent' else 'Ack. Recd' end) AckStatus, " +
                  "COMD.AckBy, " +
                  "COMD.AckOn AS AckOn," + ddlVessel.SelectedValue  + " AS VesselID " +
                  "from  " +
                  "dbo.SMS_ManualDetails MD  " +
                  "INNER JOIN dbo.SMS_ManualMaster MM ON MM.MANUALID=MD.MANUALID  " +
                  "LEFT JOIN dbo.SMS_APP_COM_ManualDetails COMD ON MD.MANUALID=COMD.ManualId AND MD.SECTIONID=COMD.SectionId AND COMD.VesselID=" + ddlVessel.SelectedValue +
                  "where MD.APPROVED='A' and MM.MANUALID=" + ddlManuals.SelectedValue + ((rad_Selected.Checked)?" and ( AckOn is null OR ACKON < SENTDATE) " :" ")+ " ORDER BY MD.MANUALID,MD.SECTIONID"; 

        dvPendingForApprovalRequest.Visible = true;
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
        rptPendingForApprovalRequest.DataSource = dt;
        rptPendingForApprovalRequest.DataBind();
    }
    protected void BindSendManual( int MID, string SecID)
    {

        //-----  BIND HEADING
        string Sql = "Select MM.MANUALNAME,MD.SECTIONID,MD.HEADING from DBO.SMS_ManualMaster MM Inner Join DBO.SMS_ManualDetails MD on MM.ManualID=MD.ManualID Where MM.ManualID=" + MID + " And MD.SectionID='" + SecID + "'";
        DataTable dtH = Common.Execute_Procedures_Select_ByQuery(Sql);
        if (dtH.Rows.Count > 0)
        {
            lblManual.Text = dtH.Rows[0]["ManualName"].ToString();
            lblSection.Text = dtH.Rows[0]["SectionID"].ToString();
            lblHeading.Text = dtH.Rows[0]["Heading"].ToString();
        }


        //-----  BIND DETILAS
        Sql = "Select Row_number() over(order by VesselName)RowNo,* from dbo.vw_SMS_GetSendManuals VW Where VW.ManualID=" + MID + " And VW.SectionID='" + SecID + "' ";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(Sql);
        rptSendManuals.DataSource = dt;
        rptSendManuals.DataBind();

        if (dt.Rows.Count > 0)
        {
            int SendManualCount = 0;
            int IsAnyAckSend = 0;
            foreach (RepeaterItem Itm in rptSendManuals.Items)
            {
                Label lblSentDate = (Label)Itm.FindControl("lblSentDate");
                Label lblAckOn = (Label)Itm.FindControl("lblAckOn");
                CheckBox chkScheduled = (CheckBox)Itm.FindControl("chkScheduled");
                if (lblAckOn.Text.Trim() != "")
                {
                    chkScheduled.Enabled = false;
                    chkScheduled.Checked = false;
                    IsAnyAckSend = 1;
                    SendManualCount = SendManualCount + 1;
                }
                else
                {
                    if (lblSentDate.Text.Trim() != "")
                    {
                        SendManualCount = SendManualCount + 1;
                    }
                }
                
            }

            if (IsAnyAckSend == 0)
            {
                if (SendManualCount == 0)
                {
                    foreach (RepeaterItem Itm in rptSendManuals.Items)
                    {
                        CheckBox chkScheduled = (CheckBox)Itm.FindControl("chkScheduled");
                        chkScheduled.Checked = true;
                        chkScheduled.Enabled = false;
                    }
                }
            }
        }
    }

    protected void btnSendVessselSelected_Click(object sender, EventArgs e)
    {
        if (ddlVessel.SelectedIndex > 0 && ddlManuals.SelectedIndex > 0)
        {
            int VesselId=Common.CastAsInt32(ddlVessel.SelectedValue);
            int ManualId=Common.CastAsInt32(ddlManuals.SelectedValue);
            string SectionIds = "";

            bool AnyChecked = false;
            foreach (RepeaterItem Itm in rptPendingForApprovalRequest.Items)
            {
                HiddenField hfSectionID = (HiddenField)Itm.FindControl("hfSectionID");
                CheckBox chk = (CheckBox)Itm.FindControl("chkSelect");
                if (chk.Checked)
                {
                    AnyChecked = true;
                    SectionIds += "," + hfSectionID.Value;
                }
            }
            //----------------------------------------------
            if (AnyChecked)
            {
                SectionIds = SectionIds.Substring(1);

                DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT isnull(EMAIL,''),VESSELCODE FROM DBO.VESSEL WHERE VESSELID=" + ddlVessel.SelectedValue);
                string LoginUserMail = ProjectCommon.getUserEmailByID(Session["loginid"].ToString());
                if (dt.Rows.Count > 0)
                {
                    string VesselMailAddress = dt.Rows[0][0].ToString();
                    //string VesselMailAddress = "emanager@energiossolutions.com";
                    if (VesselMailAddress.Trim() != "")
                    {
                        // TRUNCATING TEMP FOLDER
                        string appname = ConfigurationManager.AppSettings["AppName"].ToString();
                        string TempFolder = Server.MapPath("/" + appname + "/EMANAGERBLOB/LPSQE/Procedures/");
                        foreach (string fl in System.IO.Directory.GetFiles(TempFolder))
                        {
                            try
                            {
                                System.IO.File.Delete(fl);
                            }
                            catch { }
                        }

                        string MailBody = "Dear Captain,<br><br>" + "Attached please find the SMS update for your ship system.<br><br>" + "Please import it from the PMS Communication Tool Program on PMS server.<br><br>" + "After successful  import export  the sms packet and send to `emanager@energiossolutions.com`. <br><br>" + "Thank You,<br><br>HSSQE DEPT<br><br>";
                        // CREATE PACKET
                        string FileName = "";
                        if (Export_SMS_Vessel_Headings(VesselId, ManualId, SectionIds, dt.Rows[0]["VESSELCODE"].ToString(), TempFolder, TempFolder, ref FileName))
                        {

                            string[] ToAddress = { VesselMailAddress };
                            string[] CCAddress = { LoginUserMail };
                            string[] NoAddress = { };
                            string Error = "";
                            if (SendMail.SendeMail("emanager@energiossolutions.com", "emanager@energiossolutions.com", ToAddress, CCAddress, NoAddress, "SMS Update", MailBody, out Error, FileName))
                            {
                                lblMessage.Text = "Mail sent successfully.";
                                Common.Execute_Procedures_Select_ByQuery("UPDATE DBO.SMS_APP_COM_ManualDetails SET SentDate=Getdate(),AckStatus='S',Scheduled=0,ACKBY=NULL,ACKON=NULL,SendBy='" + Session["UserName"] + "' WHERE VESSELID=" + VesselId + " AND ManualId=" + ManualId + " AND SectionId IN (SELECT RESULT FROM DBO.CSVtoTableString('" + SectionIds + "',','))");
                                BindGrid();
                            }
                            else
                            {

                                lblMessage.Text = "Unable to sent mail.";
                            }
                        }
                        else
                        {
                            lblMessage.Text = "Unable to create packet.";
                        }

                        //SENT MAIL
                    }
                }
            }
            else
            {
                lblMessage.Text = "| Please select headings to send.";
            }
        }
        
    }
    protected void lnkSendMail_OnClick(object sender, EventArgs e)
    {
        //------------------------
        ImageButton li = (ImageButton)sender;
        int VesselId = Common.CastAsInt32(li.Attributes["VesselId"]);
        int ManualId = Common.CastAsInt32(li.CommandArgument);
        string SectionId = li.ToolTip.ToString();
        
        //------------------------ CREATE PACKET AND SEND MAIL
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT isnull(VesselEmailNew,'') As VesselEmailNew,VESSELCODE FROM DBO.VESSEL with(nolock) WHERE VESSELID=" + VesselId);
        string LoginUserMail = ProjectCommon.getUserEmailByID( Session["loginid"].ToString());
        if(dt.Rows.Count >0)
        {
            string VesselMailAddress = dt.Rows[0]["VesselEmailNew"].ToString();
            //string VesselMailAddress = "emanager@energiossolutions.com";
            if (VesselMailAddress.Trim() != "")
            {
                // TRUNCATING TEMP FOLDER
                string appname = ConfigurationManager.AppSettings["AppName"].ToString();
                string TempFolder = Server.MapPath("/" + appname + "/EMANAGERBLOB/LPSQE/Procedures/");
                foreach (string fl in System.IO.Directory.GetFiles(TempFolder))
                {
                    try
                    {
                        System.IO.File.Delete(fl);
                    }
                    catch { }
                }

                string MailBody = "Dear Captain,<br><br>" + "Attached please find the SMS update for your ship system.<br><br>" + "Please import it from the PMS Communication Tool Program on PMS server.<br><br>" + "After successful  import export  the sms packet and send to `emanager@energiossolutions.com`. <br><br>" + "Thank You,<br><br>HSSQE DEPT<br><br>";
                // CREATE PACKET
                string FileName="";
                if (Export_SMS_Vessel(VesselId, ManualId, SectionId, dt.Rows[0]["VESSELCODE"].ToString(), TempFolder, TempFolder, ref FileName))
                {

                    string[] ToAddress = { VesselMailAddress };
                    string[] CCAddress = { LoginUserMail };
                    string[] NoAddress = { };
                    string Error = "";
                    string _FromAdd = ConfigurationManager.AppSettings["FromAddress"];
                    if (SendMail.SendeMail(_FromAdd, _FromAdd, ToAddress, CCAddress, NoAddress, "SMS Update", MailBody, out Error, FileName))
                    {
                        lblMsg.Text = "Mail sent successfully.";
                        Common.Execute_Procedures_Select_ByQuery("UPDATE dbo.SMS_APP_COM_ManualDetails SET Scheduled = 0,SENTDATE = GETDATE(),SENDBY = '" + Session["UserName"] + "',ACKBY = NULL,ACKON = NULL Where VesselID =" + VesselId + " AND ManualId =" + ManualId + " AND SectionId = '" + SectionId + "'");
                        BindSendManual(ManualId,SectionId);
                    }
                    else
                    {
                        
                        lblMsg.Text = "Unable to sent mail.";
                    }
                }
                else
                {
                    lblMsg.Text = "Unable to create packet.";
                }
                
                //SENT MAIL
            }
        }
    }
    public bool Export_SMS_Vessel(int VesselId,int ManualId,string SectionId,  string VesselCode, string VesselLocalTemp, string VesselLocalPacketFolder, ref string RetFile)
    {
        try
        {
            GC.Collect();
            DataSet ds_Ret = new DataSet();
            SqlConnection MyConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["EMANAGER"].ToString());
            SqlCommand MyCommand = new SqlCommand();
            MyCommand.Connection = MyConnection;
            MyCommand.CommandType = CommandType.StoredProcedure;
            MyCommand.CommandText = "SMS_Get_CommunicationData_VSL_SECTION";
            MyCommand.Parameters.Add(new SqlParameter("@VesselCode", VesselCode));
            MyCommand.Parameters.Add(new SqlParameter("@ManualId", ManualId));
            MyCommand.Parameters.Add(new SqlParameter("@SectionId", SectionId));
            SqlDataAdapter adp = new SqlDataAdapter(MyCommand);
            adp.Fill(ds_Ret, "Data");

            if (ds_Ret.Tables[0].Rows.Count > 0 || ds_Ret.Tables[6].Rows.Count > 0)
            {

                ds_Ret.Tables[0].TableName = "SMS_ScheduledKeys";
                ds_Ret.Tables[1].TableName = "SMS_APP_ManualMaster";
                ds_Ret.Tables[2].TableName = "SMS_APP_ManualDetails";
                ds_Ret.Tables[3].TableName = "SMS_APP_ManualDetailsForms";
                ds_Ret.Tables[4].TableName = "SMS_APP_ManualDetailsRanks";
                ds_Ret.Tables[5].TableName = "SMS_Forms";
                ds_Ret.Tables[6].TableName = "SMS_PacketID";
                ds_Ret.Tables[7].TableName = "SMS_MaxCommentId";
                ds_Ret.Tables[8].TableName = "SMS_ManualCategory";
                ds_Ret.Tables[9].TableName = "SMS_ManualCatMappings";

                int PacketId = Convert.ToInt32(ds_Ret.Tables[6].Rows[0]["PacketId"]);

                string SchemaFile = VesselLocalTemp + "SMS_Schema.xml";
                string DataFile = VesselLocalTemp + "SMS.xml";

                ds_Ret.WriteXmlSchema(SchemaFile);
                ds_Ret.WriteXml(DataFile);


                string zipFileName = VesselCode + "_SMS_" + PacketId.ToString() + ".zip";
                using (ZipFile zip = new ZipFile())
                {
                    zip.AddFile(SchemaFile);
                    zip.AddFile(DataFile);
                    RetFile=VesselLocalPacketFolder + zipFileName;
                    zip.Save(RetFile);

                    MyCommand.CommandType = CommandType.Text;
                    MyCommand.Parameters.Clear();
                    MyCommand.CommandText = "INSERT INTO SMS_PACKETSENT (VESSELCODE,TABLEID,PACKETNAME,CREATEDON) VALUES('" + VesselCode + "'," + PacketId.ToString() + ",'" + zipFileName + "',Getdate())";
                    MyConnection.Open();
                    int Res = MyCommand.ExecuteNonQuery();
                    if (Res <= 0)
                    {
                        throw new Exception("Unable to save record in table ( SMS_PACKETRECD ).");
                    }

                    MyConnection.Close();
                    lblMsg.Text="SMS Packet has been created successfully. File Name : " + zipFileName;

                }

                ds_Ret.Dispose();
                return true;
            }
            else
            {
                ds_Ret.Dispose();
                return false;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error : " + ex.Message;
            return false;
        }
    }
    public bool Export_SMS_Vessel_Headings(int VesselId, int ManualId, string SectionIds, string VesselCode, string VesselLocalTemp, string VesselLocalPacketFolder, ref string RetFile)
    {
        try
        {
            GC.Collect();
            DataSet ds_Ret = new DataSet();
            SqlConnection MyConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["EMANAGER"].ToString());
            SqlCommand MyCommand = new SqlCommand();
            MyCommand.Connection = MyConnection;
            MyCommand.CommandType = CommandType.StoredProcedure;
            MyCommand.CommandText = "SMS_Get_CommunicationData_VSL_SECTION_ManyHeadings";
            MyCommand.Parameters.Add(new SqlParameter("@VesselCode", VesselCode));
            MyCommand.Parameters.Add(new SqlParameter("@ManualId", ManualId));
            MyCommand.Parameters.Add(new SqlParameter("@SectionIds", SectionIds));
            SqlDataAdapter adp = new SqlDataAdapter(MyCommand);
            adp.Fill(ds_Ret, "Data");

            if (ds_Ret.Tables[0].Rows.Count > 0 || ds_Ret.Tables[6].Rows.Count > 0)
            {

                ds_Ret.Tables[0].TableName = "SMS_ScheduledKeys";
                ds_Ret.Tables[1].TableName = "SMS_APP_ManualMaster";
                ds_Ret.Tables[2].TableName = "SMS_APP_ManualDetails";
                ds_Ret.Tables[3].TableName = "SMS_APP_ManualDetailsForms";
                ds_Ret.Tables[4].TableName = "SMS_APP_ManualDetailsRanks";
                ds_Ret.Tables[5].TableName = "SMS_Forms";
                ds_Ret.Tables[6].TableName = "SMS_PacketID";
                ds_Ret.Tables[7].TableName = "SMS_MaxCommentId";
                ds_Ret.Tables[8].TableName = "SMS_ManualCategory";
                ds_Ret.Tables[9].TableName = "SMS_ManualCatMappings";

                int PacketId = Convert.ToInt32(ds_Ret.Tables[6].Rows[0]["PacketId"]);

                string SchemaFile = VesselLocalTemp + "SMS_Schema.xml";
                string DataFile = VesselLocalTemp + "SMS.xml";

                ds_Ret.WriteXmlSchema(SchemaFile);
                ds_Ret.WriteXml(DataFile);


                string zipFileName = VesselCode + "_SMS_" + PacketId.ToString() + ".zip";
                using (ZipFile zip = new ZipFile())
                {
                    zip.AddFile(SchemaFile);
                    zip.AddFile(DataFile);
                    RetFile = VesselLocalPacketFolder + zipFileName;
                    zip.Save(RetFile);

                    MyCommand.CommandType = CommandType.Text;
                    MyCommand.Parameters.Clear();
                    MyCommand.CommandText = "INSERT INTO SMS_PACKETSENT (VESSELCODE,TABLEID,PACKETNAME,CREATEDON) VALUES('" + VesselCode + "'," + PacketId.ToString() + ",'" + zipFileName + "',Getdate())";
                    MyConnection.Open();
                    int Res = MyCommand.ExecuteNonQuery();
                    if (Res <= 0)
                    {
                        throw new Exception("Unable to save record in table ( SMS_PACKETRECD ).");
                    }

                    MyConnection.Close();
                    lblMsg.Text = "SMS Packet has been created successfully. File Name : " + zipFileName;

                }

                ds_Ret.Dispose();
                return true;
            }
            else
            {
                ds_Ret.Dispose();
                return false;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error : " + ex.Message;
            return false;
        }
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "tr", "SetLastFocus('dvscroll_ApprovalRequest');", true);
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "tr1", "SetLastFocus('dvscroll_SendManuals');", true);
    }
    
}