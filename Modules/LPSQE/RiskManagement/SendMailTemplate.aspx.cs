using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using Ionic.Zip;
using System.IO;
using System.Configuration;
using System.Activities.Expressions;
using System.Data.SqlTypes;

public partial class HSSQE_RiskManagement_SendMailTemplate : System.Web.UI.Page
{
    public delegate void LongTimeTask_Delegate(DataTable dt);

    public DataTable VesselMailStatus
    {
        set { Session["VesselMailStatus"] = value; }
        get { return (DataTable)Session["VesselMailStatus"] ; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        
        if(!IsPostBack)
        {

            if (Request.QueryString["Mode"] == "RiskAssesmentTemplateMail")
            {
                Response.ContentType = "text/plain";
                int VesselId = Common.CastAsInt32(Request.QueryString["VesselId"]);
                
                DataRow[] drs = VesselMailStatus.Select("VesselId=" + VesselId);
                string Reply = "";
                if (drs.Length > 0)
                    Reply="[{'Result':'" + drs[0]["MailSent"].ToString() + "','Message':'"+ drs[0]["Message"].ToString().Replace("'","`") + "'}]";
                else
                    Reply = "[{'Result':'E','Message':'No Records Found.'}]";

                Reply = Reply.Replace("'", "\"");
                Response.Write(Reply);
                Response.Flush();
                Response.End();
                return; 
                
            }
            else
            {
                //-------------------------
                string SQL="";
                if (Request.QueryString["Mode"].ToString() == "ALL")
                    SQL = "Select VesselId,VesselCode,VesselName,'N' as MailSent,Email,'' AS Message,VesselEmailNew from dbo.vessel where vesselstatusid=1  order by vesselname";
                else
                    SQL = "Select VesselId,VesselCode,VesselName,'N' as MailSent,Email,'' AS Message,VesselEmailNew from dbo.vessel where vesselstatusid=1 and vesselid=" + Request.QueryString["Mode"].ToString() + "  order by vesselname";

                DataSet dt = Budget.getTable(SQL);
                VesselMailStatus = dt.Tables[0];
                rptVsls.DataSource = dt;
                rptVsls.DataBind();

                LongTimeTask_Delegate d = null;
                d = new LongTimeTask_Delegate(SendMailToVessels);
                IAsyncResult R = null;
                R = d.BeginInvoke(dt.Tables[0], null, null); 
            }
        }

        
    }
    public void SendMailToVessels(DataTable dt)
    {
        foreach (DataRow dr in dt.Rows)
        {
            bool MailSent=false;
            int VesselId = Common.CastAsInt32(dr["VesselId"]);
            string VesselCode=dr["VesselCode"].ToString();
            string MailAddress=dr["VesselEmailNew"].ToString();
            string Message = "";
            if (MailAddress != "")
            {
                //----------------------------------
                try
                {
                    DataSet ds = new DataSet();

                    string SQL = "SELECT * FROM DBO.EV_TemplateMaster WHERE TemplateId IN ( SELECT VL.[TemplateId] FROM [dbo].[EV_TemplateMaster] M INNER JOIN [dbo].[EV_Template_Vessel_Linking] VL ON VL.TemplateId = M.TemplateId WHERE M.[STATUS]='A' AND VL.VesselId=" + VesselId.ToString() + " )";
                   
                    dt = Common.Execute_Procedures_Select_ByQuery(SQL);
                    dt.TableName = "EV_TemplateMaster";
                    ds.Tables.Add(dt.Copy());

                    SQL = "SELECT * FROM DBO.RA_Template_Hazards WHERE TemplateId IN ( SELECT VL.[TemplateId] FROM [dbo].[EV_TemplateMaster] M INNER JOIN [dbo].[EV_Template_Vessel_Linking] VL ON VL.TemplateId = M.TemplateId WHERE M.[STATUS]='A' AND VL.VesselId=" + VesselId.ToString() + " )";
                    dt = Common.Execute_Procedures_Select_ByQuery(SQL);
                    dt.TableName = "RA_Template_Hazards";
                    ds.Tables.Add(dt.Copy());

                    SQL = "SELECT * FROM DBO.RA_TemplateDetails WHERE TemplateId IN ( SELECT VL.[TemplateId] FROM [dbo].[EV_TemplateMaster] M INNER JOIN [dbo].[EV_Template_Vessel_Linking] VL ON VL.TemplateId = M.TemplateId WHERE M.[STATUS]='A' AND VL.VesselId=" + VesselId.ToString() + " )";
                    dt = Common.Execute_Procedures_Select_ByQuery(SQL);
                    dt.TableName = "RA_TemplateDetails";
                    ds.Tables.Add(dt.Copy());

                    SQL = "Select * from EV_EventMaster with(nolock)";
                    dt = Common.Execute_Procedures_Select_ByQuery(SQL);
                    dt.TableName = "EV_EventMaster";
                    ds.Tables.Add(dt.Copy());

                    SQL = "Select * from EV_HazardMaster with(nolock)";
                    dt = Common.Execute_Procedures_Select_ByQuery(SQL);
                    dt.TableName = "EV_HazardMaster";
                    ds.Tables.Add(dt.Copy());

                    //SQL = SQL = "Select * from EV_VSL_RISKMGMT_MASTER with(nolock) where VESSELCODE = '" + VesselCode.ToString() + "'";
                    //dt = Common.Execute_Procedures_Select_ByQuery(SQL);
                    //dt.TableName = "EV_VSL_RISKMGMT_MASTER";
                    //ds.Tables.Add(dt.Copy());
                    //----------------------------------
                    string ZipFileName = "RCA_TEMPLATE_" + VesselCode + "_" + DateTime.Now.ToString("dd-MMM-yyyy-HH-mm-ss") + ".zip";
                    string SchemaFile = Server.MapPath("~/Modules/LPSQE/TEMP/RCA_Template_Schema.xml");
                    string DataFile = Server.MapPath("~/Modules/LPSQE/TEMP/RCA_Template_Data.xml");
                    string ConfigFile = Server.MapPath("~/Modules/LPSQE/TEMP/PacketConfig.xml");
                    string Contents = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>" +
                                        "<configuration>" +
                                        "    <PacketName>" + ZipFileName + "</PacketName>" +
                                        "    <PacketType>DATA</PacketType>" +
                                        "    <PacketDataType>RISKTEMPLATES</PacketDataType>" +
                                        "    <Reply>Y</Reply>" +
                                        "</configuration>";
                    File.WriteAllText(ConfigFile, Contents);
                    string ZipFile = Server.MapPath("~/Modules/LPSQE/TEMP/" + ZipFileName);

                    if (File.Exists(ZipFile))
                    {
                        File.Delete(ZipFile);
                    }
                    ds.WriteXmlSchema(SchemaFile);
                    ds.WriteXml(DataFile);

                    using (ZipFile zip = new ZipFile())
                    {
                        zip.AddFile(SchemaFile);
                        zip.AddFile(DataFile);
                        zip.AddFile(ConfigFile);
                        zip.Save(ZipFile);
                    }

                    //MailAddress = "ajay.singh@mtmsm.com";
                    string CCAddress = ProjectCommon.getUserEmailByID(Session["loginid"].ToString());
                    string _FromAdd = ConfigurationManager.AppSettings["FromAddress"];
                    MailSent = (SendMail.SendRiskAssesmentTemplateMailToVesssel(_FromAdd, MailAddress, CCAddress, ZipFile) == "SENT");
                    MailSent = true;

                    Common.Execute_Procedures_Select_ByQuery("update [dbo].[EV_Template_Vessel_Linking] set sentby = '" + Session["UserName"].ToString() + "', senton = getdate(),AckRecd=NULL,AckRecdOn=NULL where vesselid =" + VesselId + " and EV_Template_Vessel_Linking.TEMPLATEID IN( " +
                                                        "SELECT VL.TEMPLATEID FROM [dbo].[EV_TemplateMaster] M " +
                                                        "INNER JOIN [dbo].[EV_Template_Vessel_Linking] VL ON VL.TemplateId = M.TemplateId " +
                                                        "WHERE M.[STATUS] = 'A' AND VL.VesselId = " + VesselId.ToString() + ")");
                    Message = "Email sent successfully.";
                }
                catch (Exception ex)
                {
                    Message = ex.Message;
                }
            }
            else
            {
                Message = "Vessel email address not found.";
            }
            //====================================
            DataRow[] drs = VesselMailStatus.Select("VesselId=" + VesselId.ToString());
            if (drs.Length > 0)
            {
                drs[0]["MailSent"] = (MailSent)?"Y":"E";
                drs[0]["Message"] = Message;
            }
            VesselMailStatus.AcceptChanges();

            //SQL="UPDATE [dbo].[EV_Template_Vessel_Linking] SET [SentBy]='" + Session["UserName"].ToString() + "', [SentOn]=getdate() WHERE [TemplateId] IN ( " + TemplateIds + ") AND VesselId = " + ddlVessel.SelectedValue;
            //Common.Execute_Procedures_Select_ByQuery(SQL);
        }
    }
}