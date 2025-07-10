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

public partial class MWCommunication : System.Web.UI.Page
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
            DataTable DT = Common.Execute_Procedures_Select_ByQuery("select * FROM dbo.Vessel Where Vesselstatusid=1  ORDER BY vesselname");
            ddlVessel.DataSource = DT;
            ddlVessel.DataTextField = "vesselname";
            ddlVessel.DataValueField = "vesselId";
            ddlVessel.DataBind();
            ddlVessel.Items.Insert(0, new ListItem("< - - SELECT - - >", "0"));
            ddlVessel_OnSelectedIndexChanged(sender, e);

            if (EmpId == 19)
            {
                
            }
            else
            {
                ShowPendingForApprovalRequest();
            }
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

                string Sql = "Update DBO.SMS_APP_COM_ManualDetails Set Scheduled=" + ((chk.Checked) ? 1 : 0) + ",SendBy='" + Session["UserName"].ToString() + "',SentDate=null,AckOn=null,AckBy=''" + " Where VesselID=" + hfVesselID.Value + " And ManualID=" + hfManualID.Value + " And SectionID='" + hfSectionID.Value + "'";
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
        ShowPendingForApprovalRequest();
        dvPOPUPSendManuals.Visible = false;
    }
    protected void ddlPendingForApprovalmanuals_SelectedIndexChanged(object sender, EventArgs e)
    {
        ShowPendingForApprovalRequest();
    }
    protected void ddlVessel_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlVessel.SelectedIndex != 0)
        {
            ddlPendingForApprovalmanuals.Items.Clear();
            DataTable DT = Common.Execute_Procedures_Select_ByQuery("select * FROM  dbo.SMS_ManualMaster WHERE MANUALID IN (SELECT MANUALID FROM DBO.SMS_ManualVesselType WHERE VESSELTYPEID=" + ddlVessel.SelectedValue + ") ORDER BY MANUALNAME");
            ddlPendingForApprovalmanuals.DataSource = DT;
            ddlPendingForApprovalmanuals.DataTextField = "ManualName";
            ddlPendingForApprovalmanuals.DataValueField = "ManualId";
            ddlPendingForApprovalmanuals.DataBind();
            ddlPendingForApprovalmanuals.Items.Insert(0, new ListItem("< - - ALL - - >", "0"));
            ddlPendingForApprovalmanuals_SelectedIndexChanged(sender, e);
            ddlVessel.Items.Insert(0, new ListItem("< - - SELECT - - >", "0"));
        }
        else
        {
            ddlPendingForApprovalmanuals.Items.Clear();
            ddlPendingForApprovalmanuals.Items.Insert(0, new ListItem("< - - SELECT - - >", "0"));
        }

        ShowPendingForApprovalRequest();
        
        //if (ddlPendingForApprovalmanuals.SelectedIndex != 0)
        //    dvVessel.Visible = true;
        //else
        //    dvVessel.Visible = false;
    }

    protected void btnSchedulePending_Click(object sender, EventArgs e)
    {
        if (ddlPendingForApprovalmanuals.SelectedIndex <= 0)
        {
            ProjectCommon.ShowMessage("Please select Manual.");
            ddlPendingForApprovalmanuals.Focus();
            return;
        }

        if (ddlVessel.SelectedIndex <= 0)
        {
            ProjectCommon.ShowMessage("Please select Vessel.");
            ddlVessel.Focus();
            return;
        }

        string sql = "UPDATE DBO.SMS_APP_COM_ManualDetails " +
                   "SET Scheduled=1,SendBy='" + Session["UserName"].ToString() + "',SentDate=null,AckOn=null,AckBy=''" +
                   "Where VesselID=" + ddlVessel.SelectedValue + " And ManualID=" + ddlPendingForApprovalmanuals.SelectedValue + " And SectionID IN " +
                   "( " +
                   "SELECT CMD.SectionID FROM [dbo].[SMS_APP_COM_ManualDetails] CMD  " +
                   "Inner Join DBO.SMS_ManualDetails MD on MD.ManualID=CMD.ManualID And MD.SectionID=CMD.SectionID " +
                   "where CMD.ManualID=" + ddlPendingForApprovalmanuals.SelectedValue + " AND CMD.VESSELID=" + ddlVessel.SelectedValue + " AND AckOn is NULL  " +
                   ") ";

        try
        {
            DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
            ProjectCommon.ShowMessage("Manual scheduled successfully on Ship.");
        }
        catch 
        {
            ProjectCommon.ShowMessage("Unable to schedule.");
        }
    }
   
    //---------------------- Function
    protected void ShowPendingForApprovalRequest()
    {
        if (ddlVessel.SelectedIndex <= 0 || ddlPendingForApprovalmanuals.SelectedIndex <= 0)
        {
            rptPendingForApprovalRequest.DataSource = null;
            rptPendingForApprovalRequest.DataBind();
            return; 
        }
        string SQL = "";

        if (ddlPendingForApprovalmanuals.SelectedIndex == 0)
            SQL = "select ROW_NUMBER() OVER(ORDER BY MD.MANUALID,MD.SECTIONID) AS SNO,MD.MANUALID,MD.SECTIONID,MM.MANUALNAME,MD.SECTIONID,HEADING,[FILENAME],SVERSION,MODIFIEDbY,MODIFIEDON,APPROVED,ApprovedOn, " +
                  " (Select Top 1 SentDate from dbo.vw_SMS_GetSendManuals VW Where VW.ManualID=MD.ManualID And VW.SectionID=MD.SectionID Order by SentDate desc)LastSent ," +
                  " (Select Count(VesselID) from dbo.SMS_APP_COM_ManualDetails Where ManualID=MD.MANUALID And SectionID=MD.SECTIONID And Scheduled=1 )IsAnyScheduled ," +
                  "(CASE WHEN APPROVED='' THEN 'Heading Changed' " +
                  "WHEN APPROVED='R' THEN 'Awaiting Approval' " +
                  "WHEN APPROVED='J' THEN 'Rejected' END) AS Status, " +
                  "(CASE WHEN APPROVED='' THEN 'Submit for Approval' " +
                  "WHEN APPROVED='R' THEN 'Cancel' " +
                  "WHEN APPROVED='J' THEN 'Re-Submit for Approval' END) AS ACTION from dbo.SMS_ManualDetails MD INNER JOIN dbo.SMS_ManualMaster MM ON MM.MANUALID=MD.MANUALID where MD.APPROVED='A' ORDER BY MD.MANUALID,MD.SECTIONID";
        else
            SQL = "select ROW_NUMBER() OVER(ORDER BY MD.MANUALID,MD.SECTIONID) AS SNO,MD.MANUALID,MD.SECTIONID,MM.MANUALNAME,MD.SECTIONID,HEADING,[FILENAME],SVERSION,MODIFIEDbY,MODIFIEDON,APPROVED,ApprovedOn, " +
                  " (Select Top 1 SentDate from dbo.vw_SMS_GetSendManuals VW Where VW.ManualID=MD.ManualID And VW.SectionID=MD.SectionID Order by SentDate desc)LastSent," +
                  " (Select Count(VesselID) from dbo.SMS_APP_COM_ManualDetails Where ManualID=MD.MANUALID And SectionID=MD.SECTIONID And Scheduled=1)IsAnyScheduled ," +
                  "(CASE WHEN APPROVED='' THEN 'Heading Changed' " +
                  "WHEN APPROVED='R' THEN 'Awaiting Approval' " +
                  "WHEN APPROVED='J' THEN 'Rejected' END) AS Status, " +
                  "(CASE WHEN APPROVED='' THEN 'Submit for Approval' " +
                  "WHEN APPROVED='R' THEN 'Cancel' " +
                  "WHEN APPROVED='J' THEN 'Re-Submit for Approval' END) AS ACTION from dbo.SMS_ManualDetails MD INNER JOIN dbo.SMS_ManualMaster MM ON MM.MANUALID=MD.MANUALID where MD.APPROVED='A' AND MD.MANUALID=" + ddlPendingForApprovalmanuals.SelectedValue + " ORDER BY MD.MANUALID,MD.SECTIONID";

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

    protected void lnkSendMail_OnClick(object sender, EventArgs e)
    {
        //------------------------
        ImageButton li = (ImageButton)sender;
        int VesselId = Common.CastAsInt32(li.Attributes["VesselId"]);
        int ManualId = Common.CastAsInt32(li.CommandArgument);
        string SectionId = li.ToolTip.ToString();
        
        //------------------------ CREATE PACKET AND SEND MAIL
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT isnull(EMAIL,''),VESSELCODE FROM DBO.VESSEL WHERE VESSELID=" + VesselId);
        string LoginUserMail = ProjectCommon.getUserEmailByID( Session["loginid"].ToString());
        if(dt.Rows.Count >0)
        {
            string VesselMailAddress = dt.Rows[0][0].ToString();
            //VesselMailAddress = "pankaj.k@esoftech.com";
            if (VesselMailAddress.Trim() != "")
            { 
                // TRUNCATING TEMP FOLDER
                string TempFolder = Server.MapPath("~/HSSQE/Temp/");
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
                    if (SendMail.SendeMail("emanager@energiossolutions.com", "emanager@energiossolutions.com", ToAddress, CCAddress, NoAddress, "SMS Update", MailBody, out Error, FileName))
                    {
                        lblMsg.Text = "Mail sent successfully.";
                        Common.Execute_Procedures_Select_ByQuery("UPDATE DBO.SMS_APP_COM_ManualDetails SET SentDate=Getdate(),AckStatus='S',Scheduled=0,SendBy='" + Session["UserName"] + "' WHERE VESSELID=" + VesselId);
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
            SqlConnection MyConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["SNQ"].ToString().Replace("SNQ", "MTMPMS"));
            SqlCommand MyCommand = new SqlCommand();
            MyCommand.Connection = MyConnection;
            MyCommand.CommandType = CommandType.StoredProcedure;
            MyCommand.CommandText = "SMS_Get_CommunicationData_VSL_SECTION";
            MyCommand.Parameters.Add(new SqlParameter("@VesselCode", VesselCode));
            MyCommand.Parameters.Add(new SqlParameter("@ManualId", ManualId));
            MyCommand.Parameters.Add(new SqlParameter("@SectionId", SectionId));
            SqlDataAdapter adp = new SqlDataAdapter(MyCommand);
            adp.Fill(ds_Ret, "Data");

            if (ds_Ret.Tables[0].Rows.Count > 0 || ds_Ret.Tables[4].Rows.Count > 0)
            {

                ds_Ret.Tables[0].TableName = "SMS_ScheduledKeys";
                ds_Ret.Tables[1].TableName = "SMS_APP_ManualMaster";
                ds_Ret.Tables[2].TableName = "SMS_APP_ManualDetails";
                ds_Ret.Tables[3].TableName = "SMS_APP_ManualDetailsForms";
                ds_Ret.Tables[4].TableName = "SMS_Forms";
                ds_Ret.Tables[5].TableName = "SMS_PacketID";
                ds_Ret.Tables[6].TableName = "SMS_MaxCommentId";

                int PacketId = Convert.ToInt32(ds_Ret.Tables[5].Rows[0]["PacketId"]);

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
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "tr", "SetLastFocus('dvscroll_ApprovalRequest');", true);
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "tr1", "SetLastFocus('dvscroll_SendManuals');", true);
    }
    
}