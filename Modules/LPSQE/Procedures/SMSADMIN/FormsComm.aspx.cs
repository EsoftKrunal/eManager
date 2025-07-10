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
using System.IO;
using Ionic.Zip;
using System.Data.SqlClient;

public partial class FormsComm : System.Web.UI.Page
{
    public AuthenticationManager Auth;
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        int UserId = 0;
        UserId = int.Parse(Session["loginid"].ToString());

        Auth = new AuthenticationManager(1057, UserId, ObjectType.Page);
        if (!(Auth.IsView))
        {
            Response.Redirect("NotAuthorized.aspx");
        }

        if(!IsPostBack)
        {
            LoadVessel();
        }
    }
    //protected void btnFormComm_OnClick(object sender, EventArgs e)
    //{
    //    if (ddlVessel.SelectedIndex <= 0)
    //    {
    //        ddlVessel.Focus();
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "alert('Please select vessel first.');", true); 
    //        return;
    //    }

    //    string SQl = "INSERT INTO DBO.SMS_APP_COM_Forms(VESSELID,FORMNO,FORMID,SCHEDULED,SCHEDULEDON,ACK_RECD,ACK_RECDON) SELECT " + ddlVessel.SelectedValue + ",A.FORMNO,MX,1,GETDATE(),0,NULL " +
    //                           "FROM " +
    //                           "( " +
    //                           "SELECT M.FORMNO,MAX(FORMID) AS MX " +
    //                           "FROM DBO.SMS_FORMS M GROUP BY FORMNO " +
    //                           ") A " +
    //                           "INNER JOIN DBO.SMS_FORMS F ON F.FORMID=A.MX " +
    //                           "WHERE F.VERSIONNO<>'INACTIVE' AND " +
    //                           "A.FORMNO NOT IN  " +
    //                           "(SELECT DISTINCT FORMNO FROM DBO.SMS_APP_COM_Forms V WHERE V.VESSELID=" + ddlVessel.SelectedValue + ") ";
    //    try
    //    {
    //        Common.Execute_Procedures_Select_ByQuery(SQl);
    //        ShowVesselForms();
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "alert('Forms assigned successfully.');", true);
    //    }
    //    catch 
    //    {
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "alert('Unable to assign forms.');", true);
    //    }
    //}

    protected void btnSendSelected_OnClick(object sender, EventArgs e)
    {
        if (ddlVessel.SelectedIndex > 0)
        {
            string SQL = " Select VesselCode,VesselEmailNew,VesselID from Dbo.Vessel Where VESSELID='" + ddlVessel.SelectedValue + "'";
            DataTable dt1 = Common.Execute_Procedures_Select_ByQuery(SQL);
            string ShipEmailAddress = "emanager@energiossolutions.com";
            //if (dt1.Rows.Count > 0)
            //{
            //    ShipEmailAddress = dt1.Rows[0]["VesselEmailNew"].ToString();
            //}
            string vesselcode = "";
            if (dt1.Rows.Count > 0)
            {
                vesselcode = dt1.Rows[0]["VesselCode"].ToString();
                if (! string.IsNullOrWhiteSpace(dt1.Rows[0]["VesselEmailNew"].ToString()))
                {
                    ShipEmailAddress = dt1.Rows[0]["VesselEmailNew"].ToString();
                }
                
                string FormsIds = "";
                foreach (RepeaterItem ri in rptForms.Items)
                {
                    CheckBox ch = (CheckBox)ri.FindControl("chkSelect");
                    if (ch.Checked)
                    {
                        int MX = Common.CastAsInt32(ch.CssClass);
                        FormsIds += "," + MX;
                    }
                }

                if (FormsIds.Trim() != "")
                {
                    FormsIds = FormsIds.Substring(1);
                    ExportForms(vesselcode,ShipEmailAddress,  ddlVessel.SelectedValue, FormsIds);
               }
            }
        }
    }
    protected void btnSendRanks_OnClick(object sender, EventArgs e)
    {
        if (ddlVessel.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "send", "alert('Please select the vessel code.')", true);
            return;
        }
        string appname = ConfigurationManager.AppSettings["AppName"].ToString();
        string TempFolder = Server.MapPath("/" + appname + "/EMANAGERBLOB/LPSQE/Procedures/");

        string SQL = " Select VesselCode,VesselEmailNew,VesselID from Dbo.Vessel Where VESSELID='" + ddlVessel.SelectedValue + "'";
        DataTable dt1 = Common.Execute_Procedures_Select_ByQuery(SQL);
        string ShipEmailAddress = "emanager@energiossolutions.com";



        string VesselCode = "";
        if (dt1.Rows.Count > 0)
        {
            VesselCode = dt1.Rows[0]["VesselCode"].ToString();
            ShipEmailAddress = dt1.Rows[0]["VesselEmailNew"].ToString();
            SQL = " SElect * from DBO.SMS_APP_ManualDetailsRanks ";
            DataTable dt2 = Common.Execute_Procedures_Select_ByQuery(SQL);
            DataSet ds_Ret = new DataSet();
            ds_Ret.Tables.Add(dt2.Copy());
            ds_Ret.Tables[0].TableName = "SMS_APP_ManualDetailsRanks";

            string SchemaFile = TempFolder + "SMS_Rank_Schema.xml";
            string DataFile = TempFolder + "SMS_Rank_Data.xml";

            ds_Ret.WriteXmlSchema(SchemaFile);
            ds_Ret.WriteXml(DataFile);

            string RetFile = "";

            string zipFileName = "SMS_Ranks.zip";
            using (ZipFile zip = new ZipFile())
            {
                zip.AddFile(SchemaFile);
                zip.AddFile(DataFile);
                RetFile = TempFolder + zipFileName;
                zip.Save(RetFile);

                
                string[] VesselEmail = { ShipEmailAddress};
                string[] NoEmail = { };
                string error;

                SendMail.SendeMail("emanager@energiossolutions.com", "emanager@energiossolutions.com", VesselEmail, NoEmail, NoEmail, "SMS RANK UPDATE", "Dear captain, <br><br>Please download the attachment and import in PMS Communication module in PMS System.<br><br>Thanks<br><br> ", out error, RetFile);
                
            }
        }
    }
    public bool ExportForms(string VesselCode,string Email, string VesselId,string FormsIds)
    {
        string appname = ConfigurationManager.AppSettings["AppName"].ToString();
        string TempFolder = Server.MapPath("/" + appname + "/EMANAGERBLOB/LPSQE/Procedures/");
        if (Email.Trim() != "")
        {
            try
            {
                GC.Collect();
                DataSet ds_Ret = new DataSet();
                SqlConnection MyConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["EMANAGER"].ToString());
                SqlCommand MyCommand = new SqlCommand();
                MyCommand.Connection = MyConnection;
                MyCommand.CommandType = CommandType.StoredProcedure;
                MyCommand.CommandText = "SMS_Get_CommunicationData_VSL_SECTION_ManyForms";
                MyCommand.Parameters.Add(new SqlParameter("@VesselId", VesselId));
                MyCommand.Parameters.Add(new SqlParameter("@FormIds", FormsIds));
                SqlDataAdapter adp = new SqlDataAdapter(MyCommand);
                adp.Fill(ds_Ret, "Data");

                if (ds_Ret.Tables[0].Rows.Count > 0 || ds_Ret.Tables[5].Rows.Count > 0)
                {

	                ds_Ret.Tables[0].TableName = "SMS_ScheduledKeys";
                	ds_Ret.Tables[1].TableName = "SMS_APP_ManualMaster";
	                ds_Ret.Tables[2].TableName = "SMS_APP_ManualDetails";
        	        ds_Ret.Tables[3].TableName = "SMS_APP_ManualDetailsForms";
	                ds_Ret.Tables[4].TableName = "SMS_APP_ManualDetailsRanks";
	                ds_Ret.Tables[5].TableName = "SMS_Forms";
	                ds_Ret.Tables[6].TableName = "SMS_PacketID";
        	        ds_Ret.Tables[7].TableName = "SMS_MaxCommentId";

                    int PacketId = Convert.ToInt32(ds_Ret.Tables[6].Rows[0]["PacketId"]);

                    string SchemaFile = TempFolder + "SMS_Schema.xml";
                    string DataFile = TempFolder + "SMS.xml";

                    ds_Ret.WriteXmlSchema(SchemaFile);
                    ds_Ret.WriteXml(DataFile);

                    string RetFile = "";

                    string zipFileName = VesselCode + "_SMS_" + PacketId.ToString() + ".zip";
                    using (ZipFile zip = new ZipFile())
                    {
                        zip.AddFile(SchemaFile);
                        zip.AddFile(DataFile);
                        RetFile = TempFolder + zipFileName;
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
                        //lblMsg.Text = "SMS Packet has been created successfully. File Name : " + zipFileName;

                        string[] VesselEmail = { Email };
                        string[] NoEmail = { };
                        string error;

                        if (SendMail.SendeMail("emanager@energiossolutions.com", "emanager@energiossolutions.com", VesselEmail, NoEmail, NoEmail, "SMS FORMS UPDATE", "Dear captain, <br><br>Please download the attachment and import in PMS > Communication module in PMS System.<br><br>Thanks<br><b></b><br>", out error, RetFile))
                        {
                            Common.Execute_Procedures_Select_ByQuery("UPDATE DBO.SMS_APP_COM_Forms SET ScheduledOn=Getdate(),Ack_Recd=0,Ack_RecdOn=NULL WHERE VESSELID=" + VesselId + " AND FORMID in (" + FormsIds + ")");
                            ddlVessel_OnSelectIndexChanged(new object(), new EventArgs());
                        }
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
                // lblMsg.Text = "Error : " + ex.Message;
                return false;
            }
        }
        else
            return false;
    }
   
    protected void btnAssignSelected_OnClick(object sender, EventArgs e)
    {
        foreach (RepeaterItem ri in rptForms.Items)
        {
            CheckBox ch = (CheckBox)ri.FindControl("chkSelect");
            if (ch.Checked)
            {
                int MX =Common.CastAsInt32(ch.CssClass);
                string SQl = "UPDATE DBO.SMS_APP_COM_Forms SET SCHEDULED=1,SCHEDULEDON=GETDATE(),ACK_RECD=0,Ack_RecdOn=NULL WHERE VESSELID=" + ddlVessel.SelectedValue + " AND FORMID=" + MX;
                Common.Execute_Procedures_Select_ByQuery(SQl);
            }
        }
        ddlVessel_OnSelectIndexChanged(sender, e);
    }
    protected void btnDelete_OnClick(object sender, EventArgs e)
    {
        int MX =Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        Common.Execute_Procedures_Select_ByQuery("UPDATE DBO.SMS_APP_COM_Forms SET SCHEDULED=0,SCHEDULEDON=NULL WHERE VESSELID=" + ddlVessel.SelectedValue + " AND FORMID=" + MX);
        ddlVessel_OnSelectIndexChanged(sender, e);
    }

    //protected void btnFormCommAll_OnClick(object sender, EventArgs e)
    //{
    //    if (ddlVessel.SelectedIndex <= 0)
    //    {
    //        ddlVessel.Focus();
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "alert('Please select vessel first.');", true); 
    //        return;
    //    }

    //    string SQl = "UPDATE DBO.SMS_APP_COM_Forms SET SCHEDULED=1,SCHEDULEDON=GETDATE(),ACK_RECD=0,Ack_RecdOn=NULL WHERE VESSELID=" + ddlVessel.SelectedValue;
    //    try
    //    {
    //        Common.Execute_Procedures_Select_ByQuery(SQl);
    //        ShowVesselForms();
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "alert('All Forms assigned successfully.');", true);
    //    }
    //    catch 
    //    {
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "alert('Unable to assign all forms.');", true);
    //    }
    //}

    protected void ddlVessel_OnSelectIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlVessel.SelectedIndex > 0)
            {
                rptForms.DataSource = null;
                rptForms.DataBind();

                string SQl = "INSERT INTO DBO.SMS_APP_COM_Forms(VESSELID,FORMNO,FORMID,SCHEDULED,SCHEDULEDON,ACK_RECD,ACK_RECDON) SELECT " + ddlVessel.SelectedValue + ",A.FORMNO,MX,0,NULL,0,NULL " +
                                               "FROM " +
                                               "( " +
                                               "SELECT M.FORMNO,MAX(FORMID) AS MX " +
                                               "FROM DBO.SMS_FORMS M with(nolock) GROUP BY FORMNO " +
                                               ") A " +
                                               "INNER JOIN DBO.SMS_FORMS F with(nolock) ON F.FORMID=A.MX " +
                                               "INNER JOIN DBO.SMS_FormsCategoryVesselMapping SF with(nolock) ON F.FormsCatId=SF.FormsCatId " +
                                               "WHERE F.VERSIONNO<>'INACTIVE' AND " +
                                               "A.FORMNO NOT IN  " +
                                               "(SELECT DISTINCT FORMNO FROM DBO.SMS_APP_COM_Forms V with(nolock) WHERE V.VESSELID=" + ddlVessel.SelectedValue + ") AND SF.VesselId = " + ddlVessel.SelectedValue + " ";

                Common.Execute_Procedures_Select_ByQuery(SQl);

                //string sql1 = "SELECT A.FORMNO,F.FORMNAME,MX,V_FROMS.FORMNO AS ONVESSEL, " +
                //                                                                    "(SELECT TOP 1 VersionNo FROM DBO.SMS_Forms INDATA WHERE FORMID=MX) AS VersionNo, " +
                //                                                                    "(SELECT TOP 1 CreatedOn FROM DBO.SMS_Forms INDATA WHERE FORMID=MX) AS CreatedOn, " +
                //                                                                    "(SELECT SCHEDULED FROM DBO.SMS_APP_COM_Forms INDATA WHERE INDATA.VESSELID=" + ddlVessel.SelectedValue + " AND FORMID=MX) AS SCHEDULED, " +
                //                                                                    "(SELECT SCHEDULEDON FROM DBO.SMS_APP_COM_Forms INDATA WHERE INDATA.VESSELID=" + ddlVessel.SelectedValue + " AND FORMID=MX) AS SCHEDULEDON, " +
                //                                                                    "(SELECT ACK_RECD FROM DBO.SMS_APP_COM_Forms INDATA WHERE INDATA.VESSELID=" + ddlVessel.SelectedValue + " AND FORMID=MX) AS ACK_RECD, " +
                //                                                                    "(SELECT ACK_RECDON FROM DBO.SMS_APP_COM_Forms INDATA WHERE INDATA.VESSELID=" + ddlVessel.SelectedValue + " AND FORMID=MX) AS ACK_RECDON " +
                //                                                                    "FROM  " +
                //                                                                    "(  " +
                //                                                                    "SELECT M.FORMNO,MAX(FORMID) AS MX " +
                //                                                                    "FROM DBO.SMS_FORMS M GROUP BY FORMNO  " +
                //                                                                    ") A  " +
                //                                                                    "INNER JOIN DBO.SMS_FORMS F ON F.FORMID=A.MX  " +
                //                                                                    " INNER JOIN DBO.SMS_FormsCategoryVesselMapping SF with(nolock) ON F.FormsCatId=SF.FormsCatId " +
                //                                                                    "LEFT JOIN   " +
                //                                                                    "(SELECT DISTINCT FORMNO FROM DBO.SMS_APP_COM_Forms V WHERE V.VESSELID=" + ddlVessel.SelectedValue + ") AS V_FROMS  " +
                //                                                                    "ON F.FORMNO=V_FROMS.FORMNO " +
                //                                                                    "WHERE F.VERSIONNO<>'INACTIVE' AND SF.VesselId = " + ddlVessel.SelectedValue + " ORDER BY A.FORMNO";

             string    sql1 = " select * from  " +
                          " ( " +
                          "     SELECT A.FORMNO, F.FORMNAME, MX, V_FROMS.FORMNO AS ONVESSEL, " +
                          "     (SELECT TOP 1 VersionNo FROM DBO.SMS_Forms INDATA with(nolock) WHERE FORMID = MX) AS VersionNo,   " +
                          "       (SELECT TOP 1 CreatedOn FROM DBO.SMS_Forms INDATA with(nolock) WHERE FORMID = MX) AS CreatedOn,     " +
                          "         (SELECT TOP 1 SCHEDULED FROM DBO.SMS_APP_COM_Forms INDATA with(nolock) WHERE INDATA.VESSELID = " + ddlVessel.SelectedValue + " AND FORMID = MX) AS SCHEDULED,         " +
                          "             (SELECT TOP 1 SCHEDULEDON FROM DBO.SMS_APP_COM_Forms INDATA with(nolock) WHERE INDATA.VESSELID = " + ddlVessel.SelectedValue + " AND FORMID = MX) AS SCHEDULEDON,     " +
                          "                 (SELECT TOP 1 ACK_RECD FROM DBO.SMS_APP_COM_Forms INDATA with(nolock) WHERE INDATA.VESSELID = " + ddlVessel.SelectedValue + " AND FORMID = MX) AS ACK_RECD,           " +
                          "                     (SELECT TOP 1 ACK_RECDON FROM DBO.SMS_APP_COM_Forms INDATA with(nolock) WHERE INDATA.VESSELID = " + ddlVessel.SelectedValue + " AND FORMID = MX) AS ACK_RECDON " +
                          "      FROM " +
                          "     ( " +
                          "         SELECT M.FORMNO, MAX(FORMID) AS MX FROM DBO.SMS_FORMS M with(nolock) GROUP BY FORMNO " +
                          "     ) A " +
                          "     INNER JOIN DBO.SMS_FORMS F with(nolock) ON F.FORMID = A.MX " +
                          "     INNER JOIN DBO.SMS_FormsCategoryVesselMapping SF with(nolock) ON F.FormsCatId=SF.FormsCatId " +
                          "     LEFT JOIN (SELECT DISTINCT FORMNO FROM DBO.SMS_APP_COM_Forms V with(nolock) WHERE V.VESSELID = " + ddlVessel.SelectedValue + ") AS V_FROMS  ON F.FORMNO = V_FROMS.FORMNO " +
                          "     WHERE F.VERSIONNO <> 'INACTIVE' AND SF.VesselId = " + ddlVessel.SelectedValue + " " +
                          " ) t " +
                          " where t.ACK_RECD = " + ((chkAckNotRevieved.Checked) ? "0" : "1") + "    " +
                          " ORDER BY T.FORMNO ";

                rptForms.DataSource = Common.Execute_Procedures_Select_ByQuery(sql1);
                rptForms.DataBind();


            }
            else
            {

                rptForms.DataSource = null;
                rptForms.DataBind();


            }
        }
        catch(Exception ex)
        {

        }
        
    }

    protected void chkAckNotRevieved_OnCheckedChanged(object sender, EventArgs e)
    {
        ddlVessel_OnSelectIndexChanged(sender, e);
    }
    
    protected void ShowVesselForms()
    {

        // string SQl = "SELECT A.FORMNO " +
        //                      "FROM " +
        //                      "( " +
        //                      "SELECT M.FORMNO,MAX(FORMID) AS MX " +
        //                      "FROM DBO.SMS_FORMS M GROUP BY FORMNO " +
        //                      ") A " +
        //                      "INNER JOIN DBO.SMS_FORMS F ON F.FORMID=A.MX " +
        //                      "WHERE F.VERSIONNO<>'INACTIVE' AND " +
        //                      "A.FORMNO IN  " +
        //                      "(SELECT DISTINCT FORMNO FROM DBO.SMS_APP_COM_Forms V WHERE V.VESSELID=" + ddlVessel.SelectedValue + ") ";
        //DataTable DT = Common.Execute_Procedures_Select_ByQuery(SQl);
        //if (ddlVessel.SelectedIndex > 0)
        //    lblVForms.Text = DT.Rows.Count.ToString();
        //else
        //    lblVForms.Text = "0";
    }
    protected void LoadVessel()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.VESSEL WHERE VESSELSTATUSID=1 ORDER BY VesselName");
        ddlVessel.DataSource = dt;
        ddlVessel.DataTextField = "VesselName";
        ddlVessel.DataValueField = "VesselId";
        ddlVessel.DataBind();
        ddlVessel.Items.Insert(0,new ListItem("< SELECT >",""));
    }
}
