using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
using System.Text;
using Ionic.Zip;

public partial class JobPlanningComments : System.Web.UI.Page
{
    //public string VesselCode{get;set;}
    //public int CompJobID { get; set; }

    public string VesselCode
    {
        set { ViewState["VC"] = value; }
        get { return ViewState["VC"].ToString(); }
    }
    public int CompJobID
    {
        set { ViewState["_CompJobID"] = value; }
        get { return Common.CastAsInt32(ViewState["_CompJobID"]); }
    }
    public int ThreadID
    {
        get { return Common.CastAsInt32(ViewState["ThreadID"]); }
        set { ViewState["ThreadID"] = value; }
    }
    public int ReplyCommentIdId
    {
        get { return Common.CastAsInt32(ViewState["_ReplyCommentIdId"]); }
        set { ViewState["_ReplyCommentIdId"] = value; }
    }
    public bool IsReply
    {
        get { return Convert.ToBoolean(ViewState["IsReply"]); }
        set { ViewState["IsReply"] = value; }
    }
    public bool Editable
    {
        get { return Convert.ToBoolean(ViewState["Editable"]); }
        set { ViewState["Editable"] = value; }
    }
    

    string[] TextColorOptions = { "#000000", "#FFA500", "#A52A2A", "#800000", "#008000", "#808000", "#FF00FF", "#800080", "#0000A0", "#0000FF", "#FF0000" };
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        string key= ""+Page.Request.QueryString["key"];
        string[] parts= key.Split('|');
        try
        {
            if (!Page.IsPostBack)
            {
                Editable = false;
                hfdReplyCommentID.Value = "";
            }
            VesselCode = parts[0];
            CompJobID = Common.CastAsInt32(parts[1]);
            lblVesselCode.Text = VesselCode.ToString();
            ShowDetials();
            //---------------------------
            btnPost.Visible = Editable;
        }
        catch(Exception ex)
        {
            form1.Visible = false;
        }
    }
    protected void ShowDetials()
    {
        
        string sql = "Select ComponentCode,ComponentName,JobCode,JobName,LastDone,NextHour,VCJMU.NextDueDate,cjm.DescrSh,VCJM.Descrm,NextHour,CASE WHEN VCJMU.NextDueDate IS NULL THEN '' ELSE datediff(day,getdate(),VCJMU.NextDueDate) END AS Difference " +

                   " FROM VSL_VesselComponentJobMaster VCJM " +
                   " INNER JOIN VSL_VesselComponentJobMaster_Updates VCJMU ON VCJM.VesselCode = VCJMU.VesselCode AND VCJM.ComponentId = VCJMU.ComponentId AND VCJM.CompJobId = VCJMU.CompJobId And VCJM.[Status]='A' " +
                   " INNER JOIN VSL_ComponentMasterForVessel VCM ON VCJM.VesselCode = VCM.VesselCode AND VCJM.ComponentId = VCM.ComponentId And VCM.[Status]= 'A' " +
                   " INNER JOIN ComponentsJobMapping CJM ON VCJM.CompjobId= CJM.CompjobId " +
                   " INNER JOIN ComponentMaster CM ON CM.ComponentId = VCJM.ComponentId " +
                   " INNER JOIN JobMaster JM  ON JM.JobId = VCJM.JobId " +
                   " INNER JOIN JobIntervalMaster JIM ON JIM.IntervalId = VCJM.IntervalId " +
                   " LEFT JOIN VSL_PlanMaster PM ON PM.VesselCode = VCJM.VesselCode AND PM.ComponentId = VCJM.ComponentId AND PM.CompJobId = VCJM.CompJobId " +
                   " LEFT JOIN Rank RM ON RM.RankId = PM.AssignedTo " +
                   " where VCJM.VesselCode= '" +VesselCode + "' and VCJM.CompJobId= " + CompJobID+ " ";
        DataTable dtJobs = Common.Execute_Procedures_Select_ByQuery(sql);
        if (dtJobs.Rows.Count > 0)
        {
            DataRow dr = dtJobs.Rows[0];
            lblCompCode.Text = dr["ComponentCode"].ToString();
            lblCompName.Text = dr["ComponentName"].ToString();
            lblJobName.Text = dr["JobName"].ToString();
            lblLastDoneDate.Text = Common.ToDateString(dr["LastDone"].ToString());
            lblDueInDays.Text = Common.ToDateString(dr["NextDueDate"].ToString()) + " [ " + dr["Difference"].ToString() + " Days]";
            lblNextHour.Text = dr["NextHour"].ToString();
            lblShortDesc.Text = dr["DescrSh"].ToString();
            lblLongDesc.Text = dr["Descrm"].ToString();
            string jobcode = dr["JobCode"].ToString().Trim().ToLower();
            Editable = (jobcode == "ren" || jobcode == "ovh" || jobcode == "sur" || jobcode == "cha");
            BindSpares();
            ShowDiscussion();
        }



    }
    protected void BindSpares()
    {
        //---------
        string sql = "SELECT *,dbo.[getROB](VesselCode,ComponentId,Office_Ship,SpareId,Getdate()) as ROB FROM [dbo].vw_JobSpareRequirement WHERE Vesselcode='" + VesselCode + "' and ForCompJobId=" + CompJobID + " order by sparename";
        DataTable dtSpares = Common.Execute_Procedures_Select_ByQuery(sql);
        rptSpares.DataSource = dtSpares;
        rptSpares.DataBind();
    }
    protected void btnPost_OnClick(object sender, EventArgs e)
    {
        IsReply = false;
        dvReply.Visible = true;
        Label1.Text = "New Comments";
        hfdReplyCommentID.Value = "";
    }
    protected void ClearTempFiles()
    {
        string[] files = Directory.GetFiles(Server.MapPath("~/Modules/PMS/TEMP"));
        foreach (string fl in files)
            try { File.Delete(fl); } catch { }

        string[] folders = Directory.GetDirectories(Server.MapPath("~/Modules/PMS/TEMP"));
        foreach (string folder in folders)
            try { Directory.Delete(folder, true); }
            catch { }

    }
    protected void btnExport_OnClick(object sender, EventArgs e)
    {
        if (Session["UserType"].ToString().Trim() == "S") // export thru communication
        {
            try
            {
                DataTable dt114 = Common.Execute_Procedures_Select_ByQuery("DBO.GETJOBCOMMENTS_SHIP '" + VesselCode + "'," + CompJobID + "");
                //----------------------------------------
                string row_count = dt114.Rows[0][0].ToString();
                dt114 = null;
                int allrows = Common.CastAsInt32(row_count);
                if (allrows <= 0)
                {
                    lblMsg.Text = "Nothing to export.";
                    return;
                }
                //----------------------------------------
                Common.Set_Procedures("[DBO].[sp_Insert_Communication_Export]");
                Common.Set_ParameterLength(5);
                Common.Set_Parameters(
                    new MyParameter("@VesselCode", VesselCode),
                    new MyParameter("@RecordType", "JOB-COMMENTS"),
                    new MyParameter("@RecordId", CompJobID),
                    new MyParameter("@RecordNo", VesselCode + "-" + lblCompCode.Text + "-" + CompJobID.ToString()),
                    new MyParameter("@CreatedBy", Session["FullName"].ToString().Trim())
                );
                //----------------------------------------
                DataSet ds11 = new DataSet();
                ds11.Clear();
                Boolean res;
                ds11 = Common.Execute_Procedures_Select();
                lblMsg.Text = "Sent for export successfully.";
                //----------------------------------------
            }
            catch (Exception ex)
            {
                lblMsg.Text = "Unable to send for export.Error : " + ex.Message;
            }
        }
        else // direct export to ship
        {
            //------------------
            ClearTempFiles();
            string SaveTargetDir = Server.MapPath("~/Modules/PMS/TEMP/");
            //------------------- CHECKS 3.  GET DATA TO BE EXPORTED FROM DATABASE

            DataTable dt1 = Common.Execute_Procedures_Select_ByQuery("select distinct P.* from VSL_VesselComponentJobOfficePlanning P INNER JOIN VSL_VesselComponentJobOfficePlanning_Comments C ON p.vESSELCODE=C.VESSELCODE AND P.OFFICE_SHIP=C.OFFICE_SHIP AND P.PlanningId=C.PlanningId where P.vesselcode='" + VesselCode + "' AND P.Office_Ship='O' AND P.COMPJOBID=" + CompJobID + " AND C.ReceivedOn IS NULL");            
            dt1.TableName = "VSL_VesselComponentJobOfficePlanning";

            DataTable dt2 = Common.Execute_Procedures_Select_ByQuery("select C.* from VSL_VesselComponentJobOfficePlanning P INNER JOIN VSL_VesselComponentJobOfficePlanning_Comments C ON p.vESSELCODE=C.VESSELCODE AND P.OFFICE_SHIP=C.OFFICE_SHIP AND P.PlanningId=C.PlanningId where P.vesselcode='" + VesselCode + "' AND P.Office_Ship='O' AND P.COMPJOBID=" + CompJobID + " AND C.ReceivedOn IS NULL");
            dt2.TableName = "VSL_VesselComponentJobOfficePlanning_Comments";

            DataTable dt3 = Common.Execute_Procedures_Select_ByQuery("select C.VesselCode,C.Office_Ship,C.PlanningId,C.CommentId,C.ReceivedOn FROM VSL_VesselComponentJobOfficePlanning P INNER JOIN VSL_VesselComponentJobOfficePlanning_Comments C ON p.vESSELCODE=C.VESSELCODE AND P.OFFICE_SHIP=C.OFFICE_SHIP AND P.PlanningId=C.PlanningId where P.vesselcode='" + VesselCode + "' AND P.Office_Ship='S' AND P.COMPJOBID=" + CompJobID);
            dt3.TableName = "VSL_VesselComponentJobOfficePlanning_Comments_Log";

            //------------------- CHECKS 3.  GET DATA TO BE EXPORTED FROM DATABASE

            DataSet dsExport = new DataSet();
            dsExport.Tables.Add(dt1.Copy());
            dsExport.Tables.Add(dt2.Copy());
            dsExport.Tables.Add(dt3.Copy());

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
                string ZipData = SaveTargetDir + "JOBCOMM_O_" + VesselCode + "_" + CompJobID + "_" + DateTime.Now.ToString("dd-MMM-yyyy") + ".zip";
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
                DataTable dtvenail = Common.Execute_Procedures_Select_ByQuery("SELECT EMAIL FROM DBO.VESSEL WHERE VESSELCODE='" + VesselCode + "'");
                if (dtvenail.Rows.Count == 1)
                {
                    try
                    {
                        string email = dtvenail.Rows[0][0].ToString();                   
                        if (email.Trim() != "")
                        {
                            string[] toadd = { email };
                            string[] bccadd = { "emanager@energiossolutions.com" };
                            string[] noadd = { };
                            string error = "";
                            string Message = "Dear Captain,<br/> You have new job planning comments in PMS Job Planning.<br/>please import the attached packed in PMS communication to review and reply.<br/><br/>";
                            Message += "<br/>Component : [ " + lblCompCode.Text + " ] " + lblCompName.Text;
                            Message += "<br/>Job Name : " + lblJobName.Text;
                            Message += "<br/>Job Description : " + lblShortDesc.Text;
                            if (EProjectCommon.SendeMail_MTM(Common.CastAsInt32(Session["loginid"]), "emanager@energiossolutions.com", "emanager@energiossolutions.com", toadd, noadd, bccadd, "PMS JOB Communication Packet", Message, out error, ZipData))
                            {
                                lblMsg.Text = "Mail sent successfully.";
                            }
                            else
                            {
                                lblMsg.Text = "Unable to send mail. Error : " + error;
                            }
                        }
                    }
                    catch
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = "Unable to export data. " + ex.Message;
            }
        }
    }    
    protected void btnReply_OnClick(object sender, EventArgs e)
    {
        int CommentId = Common.CastAsInt32(hfdReplyCommentID.Value);
        dvReply.Visible = true;
        Label1.Text = "Reply Comments";
        IsReply = true;
        txtReply.Focus();
        DataTable RetValue = Common.Execute_Procedures_Select_ByQuery("select Comment from VSL_VesselComponentJobOfficePlanning_Comments where CommentId=" + CommentId + "");
        if (RetValue.Rows.Count > 0)
        {
            dvOrgComment.InnerHtml = RetValue.Rows[0]["Comment"].ToString();
            txtReply.Text = "";
        }
    }
    protected void btnCloseCommentsPopup_OnClick(object sender, EventArgs e)
    {
     
    }
    
    protected void btnSave_Reply_OnClick(object sender, EventArgs e)
    {
        object AttachmentName=DBNull.Value;
        byte []Attachment = new byte[0];
        if (flpReply.HasFile)
        {
            if (!flpReply.PostedFile.FileName.ToLower().EndsWith("zip"))  // zip only
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "hh", "alert('Only zip file is allowed to upload.');", true);
                return;
            }
            if (flpReply.PostedFile.ContentLength > (100*1024))  // 100 kb
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "hh", "alert('File size is too large. Max 100KB is allowed.');",true);
                return;
            }
            AttachmentName = flpReply.FileName;
            Attachment = flpReply.FileBytes;
        }
        if (txtReply.Text.Trim() == "")
            return;
        Common.Set_Procedures("Insert_Planning_VSL_VesselComponentJobOfficePlanning_Comments");
        Common.Set_ParameterLength(8);
        Common.Set_Parameters(
            new MyParameter("@VesselCode", VesselCode),
            new MyParameter("@OFFICE_SHIP", Session["UserType"].ToString()),
            new MyParameter("@COMPJOBID", CompJobID),
            new MyParameter("@Comment", txtReply.Text.Trim()),
            new MyParameter("@CommentBy", Session["UserName"].ToString()),
            //new MyParameter("@ReplyCommentIdId", Common.CastAsInt32(hfdReplyCommentID.Value)),
            new MyParameter("@ReplyCommentIdId",0),
            new MyParameter("@AttachmentName", AttachmentName),
            new MyParameter("@Attachment", Attachment)
            );
        DataSet ds = new DataSet();
        ds.Clear();
        Boolean res;
        res = Common.Execute_Procedures_IUD(ds);
        //BindComments();
        ShowDiscussion();
        hfdReplyCommentID.Value = "";
        txtReply.Text = "";
        dvReply.Visible = false;

    }
    protected void btnClose_Reply_OnClick(object sender, EventArgs e)
    {
        dvReply.Visible = false;
        hfdReplyCommentID.Value = "";

    }
    protected void btnClip_OnClick(object sender, EventArgs e)
    {
        int CommentId = Common.CastAsInt32(hfdReplyCommentID.Value);
        DataTable RetValue = Common.Execute_Procedures_Select_ByQuery("select * from VSL_VesselComponentJobOfficePlanning_Comments where CommentId=" + CommentId + "");
        if (RetValue.Rows.Count > 0)
        {
            string FileName = RetValue.Rows[0]["AttachmentName"].ToString();
            byte[] ret = (byte[])RetValue.Rows[0]["Attachment"];
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", String.Format("attachment;filename={0}", FileName));
            Response.ContentType = "application/" + Path.GetExtension(FileName).Substring(1);
            Response.BinaryWrite(ret);
        }
    }

    //protected void BindComments()
    //{

    //    string sql = " select  c.VesselCode,c.Office_Ship,c.PlanningId,CommentId,Comment,CommentBy,CommentOn,c.Updated,c.UpdatedOn, "+
    //                 "   case when c.Office_Ship = 'O' then 'office' else 'ship'end as OfficeShipCss,ReceivedOn " +
    //                 "   from VSL_VesselComponentJobOfficePlanning_Comments c " +
    //                 "   inner join VSL_VesselComponentJobOfficePlanning p on c.vesselcode = p.vesselcode and c.Office_Ship = p.Office_Ship and c.PlanningId = p.PlanningId " +
    //                 "   where p.vesselcode = '"+VesselCode+"' and p.compjobid = "+ CompJobID + " and p.historyid is null " +
    //                 "   order by commentid desc ";
    //    DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
    //    rptComments.DataSource = dt;
    //    rptComments.DataBind();
    //}

    public void ShowDiscussion()
    {
        StringBuilder sb = new StringBuilder();
        bool ThreadOpen = false;

        string sql = " select c.VesselCode,c.Office_Ship,c.PlanningId,CommentId,Comment,CommentBy,CommentOn,c.Updated,c.UpdatedOn,AttachmentName,Attachment, " +
                     "   case when c.Office_Ship = 'O' then 'office' else 'ship'end as OfficeShipCss,ReceivedOn " +
                     "   from VSL_VesselComponentJobOfficePlanning_Comments c " +
                     "   inner join VSL_VesselComponentJobOfficePlanning p on c.vesselcode = p.vesselcode and c.Office_Ship = p.Office_Ship and c.PlanningId = p.PlanningId " +
                     "   where p.vesselcode = '" + VesselCode + "' and p.compjobid = " + CompJobID + " and p.historyid is null and  Isnull(ReplyCommentId,0)=0 " +
                     "   order by CommentOn desc ";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
        foreach (DataRow dr in dt.Rows)
        {
            int CommentId = Common.CastAsInt32(dr["CommentId"]);
            StartCommentBox(sb, CommentId, ThreadOpen, dr["CommentBy"], dr["Comment"], dr["CommentOn"], 1, dr["AttachmentName"], (dr["Office_Ship"].ToString() == "O") ? "Office" : "Ship");
            LoadChildComments(sb, ThreadID, CommentId, 1, ThreadOpen);
            EndCommentBox(sb);
        }
        litComments.Text = sb.ToString();
    }

    public void StartCommentBox(StringBuilder sb, int CommentId, bool ThreadOpen, object CommentBy, object CommentText, object CommentOn, int Level, object AttachmentName,string Office_Ship)
    {
        //TextColorOptions[Common.CastAsInt32((TextColorOptions.Length - 1) * r.NextDouble())]
        string Data = "";
        Data = @"<div class='Comment_Box' style='background-color:hsla(236,45%," + (100 - Level * 10) + "%,0.10);color:" + TextColorOptions[8] + "'>" +
                "<div class='Comment_header'>" +
                        ((AttachmentName.ToString().Trim() != "") ? "<img class='attachment' src='./Images/PaperClip.png' alt='FileName' commentid='" + CommentId + "' />" : "") +
                        ((ThreadOpen) ? "<span class='reply' commentid='" + CommentId + "'> <img src='./Images/reply.png' style='float:left; margin-right:5px;' />Reply</span>" : "") +
                        "<span class='commentby'><img src='./Images/user.png' style='float:left'/>" + Office_Ship+" - "+ CommentBy.ToString() + "</span>" +
                        "<span class='commenton'>" + Convert.ToDateTime(CommentOn).ToString("dd-MMM-yyyy hh:mm tt") + "</span>" +
                        //((ThreadOpen) ? "<span class='addmember'><img src='./Images/friend_add_small.png' style='float:left; margin-left:30px;' />Invite Member(s)</span>" : "") +
                "</div>" +
                "<div class='Comment_content'>" +
                    "<img src='./Images/comments.png' style='float:left; margin-right:10px;' />" +
                    "<span class='commenttext' >" + CommentText.ToString() + "</span>" +
                    
                "</div>";


        sb.Append(Data);
    }
    public void EndCommentBox(StringBuilder sb)
    {
        string Data = "";
        Data = @"</div>";
        sb.Append(Data);
    }
    protected void LoadChildComments(StringBuilder sb, int _ThreadId, int _CommentId, int Level, bool ThreadOpen)
    {
        Level++;
        //DataTable dtChilds = Common.Execute_Procedures_Select_ByQuery("select COMMENTID,COMMENTTEXT,COMMENTON,ATTACHMENTNAME,U.FIRSTNAME + ' ' + U.LASTNAME AS CommentBy from [dbo].[HR_ThreadComments] TC  left join dbo.Userlogin u on TC.UserId=LoginId Where ThreadId=" + _ThreadId + " and PrevCommentId=" + _CommentId);

        string sql = " select  c.VesselCode,c.Office_Ship,c.PlanningId,CommentId,Comment,CommentBy,CommentOn,c.Updated,c.UpdatedOn,AttachmentName,Attachment, " +
                     "   case when c.Office_Ship = 'O' then 'office' else 'ship'end as OfficeShipCss,ReceivedOn " +
                     "   from VSL_VesselComponentJobOfficePlanning_Comments c " +
                     "   inner join VSL_VesselComponentJobOfficePlanning p on c.vesselcode = p.vesselcode and c.Office_Ship = p.Office_Ship and c.PlanningId = p.PlanningId " +
                     "   where p.vesselcode = '" + VesselCode + "' and p.compjobid = " + CompJobID + " and p.historyid is null and  Isnull(ReplyCommentId,0)="+ _CommentId + " " +
                     "   order by commentid desc ";
        DataTable dtChilds = Common.Execute_Procedures_Select_ByQuery(sql);

        foreach (DataRow dr in dtChilds.Rows)
        {
            int CommentId = Common.CastAsInt32(dr["CommentId"]);
            StartCommentBox(sb, CommentId, ThreadOpen, dr["CommentBy"], dr["Comment"], dr["CommentOn"], Level, dr["AttachmentName"], (dr["Office_Ship"].ToString()=="O")?"Office":"Ship");
            LoadChildComments(sb, _ThreadId, CommentId, Level, ThreadOpen);
            EndCommentBox(sb);
        }
    }
}
