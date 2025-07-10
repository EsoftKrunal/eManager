using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Ionic.Zip;
using System.IO;
using System.Text;
using System.Configuration;
using System.Net.Mail;

public partial class RiskManagement_RiskAnalysis : System.Web.UI.Page
{
    public int UserId
    {
        get { return Common.CastAsInt32(ViewState["UserId"]); }
        set { ViewState["UserId"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        if (!IsPostBack)
        {
            Session["RM"] = "R";
            UserId = Common.CastAsInt32(Session["loginid"]);
            BindVessel();
            BindOffice();
            BindGrid();
        }
    }
    protected void BindVessel()
    {
        DataTable dtVessels = new DataTable();
        string strvessels = "SELECT VesselId,VesselCode,VesselName FROM DBO.Vessel VM with(nolock) WHERE VM.VesselId in (Select v.VesselId from UserVesselRelation v with(nolock) where v.Loginid = " + Convert.ToInt32(Session["loginid"].ToString()) + ")  AND VM.StatusId = 'A'  ORDER BY VesselName ";
        dtVessels = Common.Execute_Procedures_Select_ByQuery(strvessels);
        if (dtVessels.Rows.Count > 0)
        {
            ddlVessel.DataSource = dtVessels;
            ddlVessel.DataTextField = "VesselName";
            ddlVessel.DataValueField = "VesselCode";
            ddlVessel.DataBind();

            ddlVessel.Items.Insert(0,new ListItem("< All >","0"));
            ddlVessel.SelectedIndex = 0;
        }
    }
    public void BindOffice()
    {
        //DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM dbo.Office");

        //if (dt.Rows.Count > 0)
        //{
        //    ddlOffice.DataSource = dt;
        //    ddlOffice.DataTextField = "OfficeName";
        //    ddlOffice.DataValueField = "OfficeId";
        //    ddlOffice.DataBind();

        //    ddlOffice.Items.Insert(0, new ListItem("< All >", "0"));
        //}
    }
    protected void BindGrid()
    {
        string WhereClause = "WHERE 1=1 AND r.VESSELCODE in (Select v.VesselCode from Vessel v with(nolock) inner join UserVesselRelation uvr with(nolock) on v.VesselId = uvr.VesselId where v.StatusId = 'A' AND uvr.LoginId = " + Convert.ToInt32(Session["loginid"].ToString()) + " ) ";

        if (ddlStatus.SelectedValue == "C")
            WhereClause += " AND r.[STATUS] = 'C' ";
        if (ddlStatus.SelectedValue == "O")
            WhereClause += " AND r.[STATUS] = 'O' ";
        //WhereClause += " AND  " +
        //"(CASE  " +
        //"WHEN R.OfficeExportedOn IS NOT NULL THEN 'C'  " +
        //"WHEN OfficeApprovalRequired='Y' THEN 'O' " +
        //"WHEN R.Verify_Needed='Y' THEN 'O' ELSE 'C' END)='C' " ;


        //if (ddlVessel.SelectedIndex > 0 && ddlOffice.SelectedIndex > 0)
        //    WhereClause += " AND (VESSELCODE='" + ddlVessel.SelectedValue + "'  OR OfficeId=" + ddlOffice.SelectedValue + " ) ";

        if (ddlVessel.SelectedIndex > 0)
            WhereClause += " AND R.VESSELCODE='" + ddlVessel.SelectedValue + "' ";

        //if (ddlOffice.SelectedIndex > 0 && ddlVessel.SelectedIndex == 0)
        //    WhereClause += " AND OfficeId=" + ddlOffice.SelectedValue + " ";

        if (txtEventDate.Text.Trim() != "")
            WhereClause += " AND R.EventDate >='" + txtEventDate.Text.Trim() + "' ";

        if (txtEventDate1.Text.Trim() != "")
            WhereClause += " AND R.EventDate <='" + txtEventDate1.Text.Trim() + "' ";

        //string SQL = "SELECT * FROM ( SELECT *,(SELECT OfficeApprovalRequired FROM [dbo].[EV_TemplateMaster] WHERE TemplateId=R1.TemplateId ) AS OfficeApprovalRequired, HOD_NAME + ' / ' + POSITION As HOD_POSITION FROM DBO.vw_RA_VSL_RiskMaster r1 )  r ";
        string SQL = "SELECT * FROM ( SELECT Distinct vesselname,RISKID,VESSELCODE,EVENTID,EVENTDATE,EventName,REFNO,GeographLocation,PTWRequired,PTWType,PersonReqTask,NoofPersonReqTask,RiskDescr,ALTERNATEMETHODS,HOD_NAME,SAF_OFF_NAME,Details,MASTER_NAME,STATUS,Verified,dbo.fn_GetRisk_VerifyNeeded(RISKID,VESSELCODE,1) As Verify_Needed, dbo.fn_GetRisk_VerifyNeeded(RISKID,VESSELCODE,0) As O_Verified,VerifiedOn,OfficeRecdOn, HOD_NAME + ' / ' + POSITION As HOD_POSITION FROM DBO.vw_RA_VSL_RiskMaster r1 )  r ";
        rptRisks.DataSource = Common.Execute_Procedures_Select_ByQuery(SQL + WhereClause);
        rptRisks.DataBind();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindGrid();
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        int _RiskId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        string VesselCode = ((ImageButton)sender).Attributes["VesselCode"];

        ScriptManager.RegisterStartupScript(Page, this.GetType(), "edit", "window.open('AddRisk.aspx?VesselCode=" + VesselCode + "&RiskId=" + _RiskId + "','');", true);
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        int RiskId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        string VesselCode = ((ImageButton)sender).Attributes["VesselCode"];
        try
        {
            DataSet ds = new DataSet();
            string SQL = "SELECT * FROM DBO.RA_VSL_RISKMGMT_MASTER with(nolock) WHERE VESSELCODE='" + VesselCode+ "' AND RISKID=" +RiskId.ToString();
            DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
            dt.TableName = "RA_VSL_RISKMGMT_MASTER";
            ds.Tables.Add(dt.Copy());
            if(dt.Rows.Count>0)
            {
                string RefNo = dt.Rows[0]["REFNO"].ToString();
                string ZipFileName ="OFFICE-" + RefNo + ".zip";
                string SQL1 = "SELECT EventName FROM DBO.EV_EventMaster with(nolock) WHERE EventId = " + dt.Rows[0]["EVENTID"].ToString();
                DataTable dt1 = Common.Execute_Procedures_Select_ByQuery(SQL1);
                string SchemaFile = Server.MapPath("~/Modules/LPSQE/TEMP/RA_Schema.xml");
                string DataFile = Server.MapPath("~/Modules/LPSQE/TEMP/RA_Data.xml");
                string ConfigFile = Server.MapPath("~/Modules/LPSQE/TEMP/PacketConfig.xml");
                string Contents = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>" +
                                    "<configuration>" +
                                    "    <PacketName>" + ZipFileName + "</PacketName>" +
                                    "    <PacketType>DATA</PacketType>" +
                                    "    <PacketDataType>RISKDATA</PacketDataType>" +
                                    "    <Reply>N</Reply>" +
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

                

                StringBuilder sb = new StringBuilder();
                sb.Append("<br/><br/>");
                sb.Append("***********************************************************************");
                sb.Append("<br/>");
                sb.Append("<br/>");
               // sb.Append("<b>Template # : </b>" + dt.Rows[0]["TemplateCode_Revision"].ToString());
                sb.Append("<br/>");
                sb.Append("<b>RA # : </b>" + dt.Rows[0]["REFNO"].ToString());
                sb.Append("<br/>");
                sb.Append("<br/>");
                sb.Append(dt1.Rows[0]["EventName"].ToString());
                sb.Append("<br/>");
                sb.Append("<br/>");
                sb.Append("***********************************************************************");
                sb.Append("<br/>");
                sb.Append("<br/>");

                string Subject = dt.Rows[0]["REFNO"].ToString();
                List<string> CCMails = new List<string>();
                List<string> BCCMails = new List<string>();

                SQL = "SELECT Email,VesselEmailNew FROM [dbo].[Vessel] with(nolock)  WHERE VesselCode='" + VesselCode + "'" ;
                dt = Common.Execute_Procedures_Select_ByQuery(SQL);
                string VesselEmail = ((dt != null & dt.Rows.Count > 0) ? dt.Rows[0]["VesselEmailNew"].ToString() : "");
                string CCEmail = ((dt!=null & dt.Rows.Count > 0) ? dt.Rows[0]["Email"].ToString() : "");
                if (! string.IsNullOrEmpty(CCEmail))
                {
                    CCMails.Add(CCEmail);
                }

                string fromAddress =  ConfigurationManager.AppSettings["FromAddress"];
            //    string UserEmail = ProjectCommon.getUserEmailByID(UserId.ToString());
                string result = SendMail.SendSimpleMail(fromAddress, VesselEmail, CCMails.ToArray(), BCCMails.ToArray(), Subject, sb.ToString(), ZipFile);
                if (result == "SENT")
                {
                    SQL = "INSERT INTO DBO.RA_VSL_RISKMGMT_EXPORT_DETAILS (RiskId,OfficeExportedBy,OfficeExportedOn) VALUES (" + RiskId + ",'" + Session["UserName"].ToString() + "',getdate())";
                    Common.Execute_Procedures_Select_ByQuery(SQL);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "succ", "alert('Exported to ship successfully.');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "err", "alert('Unable to export.Error : " + Common.getLastError() + "');", true);
                }
            }            
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "err", "alert('Unable to export.Error : " + ex.Message + "');", true);
        }
    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        BindGrid();
    }

}