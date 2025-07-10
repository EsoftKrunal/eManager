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
using ShipSoft.CrewManager.BusinessObjects;
using ShipSoft.CrewManager.BusinessLogicLayer;
using ShipSoft.CrewManager.Operational;
using System.IO;
using Ionic.Zip;

public partial class Vetting_VIQPreparation : System.Web.UI.Page
{
    public AuthenticationManager Auth;
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        //--------------------------------- PAGE ACCESS AUTHORITY ----------------------------------------------------
        Auth = new AuthenticationManager(306, Convert.ToInt32(Session["loginid"].ToString()), ObjectType.Page);
        if (!Auth.IsView)
            Response.Redirect("blank.aspx");
        //------------------------------------------------------------------------------------------------------------
        
        lbl_Message.Text = "";
        if (!Page.IsPostBack)
        {
            BindFleet();
            BindVessel();
            btnShow_Click(sender, e);
            btnCreateNew.Visible = Auth.IsAdd;
        }
    }
    public void BindFleet()
    {
        string Query = "select * from dbo.FleetMaster";
        ddlFleet.DataSource = Budget.getTable(Query);
        ddlFleet.DataTextField = "FleetName";
        ddlFleet.DataValueField = "FleetID";
        ddlFleet.DataBind();
        ddlFleet.Items.Insert(0, new ListItem("<--Fleet-->", "0"));
    }
    protected void ddlFleet_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        BindVessel();
    }
    protected void chk_Inactive_OnCheckedChanged(object sender, EventArgs e)
    {
        BindVessel();
    }
    public void BindVessel()
    {
        string WhereClause = "";
        string sql = "SELECT VesselId,VesselCode,Vesselname FROM DBO.Vessel v Where 1=1 ";
        if (!chk_Inactive.Checked)
        {
            WhereClause = " and v.VesselStatusid<>2 and v.vesselid not in (41,43)";
        }
        if (ddlFleet.SelectedIndex != 0)
        {
            WhereClause = WhereClause + " and fleetID=" + ddlFleet.SelectedValue + "";
        }

        ddlVessel.DataSource = VesselReporting.getTable(sql + WhereClause + "ORDER BY VESSELNAME");
        ddlVessel.DataTextField = "Vesselname";
        ddlVessel.DataValueField = "VesselCode";
        ddlVessel.DataBind();
        ddlVessel.Items.Insert(0, new ListItem("<--Vessel-->", ""));


        ddlVesselAdd.DataSource = VesselReporting.getTable(sql + " and v.VesselStatusid<>2 and v.vesselid not in (41,43) ORDER BY VESSELNAME");
        ddlVesselAdd.DataTextField = "Vesselname";
        ddlVesselAdd.DataValueField = "VesselId";
        ddlVesselAdd.DataBind();
        ddlVesselAdd.Items.Insert(0, new ListItem("<--Vessel-->", ""));
        
    }
    public void BindGrid()
    {
        int MainGroup= (radSire.Checked)?1:2;

        string sql = "SELECT *,((DONEQ*100)/(CASE WHEN NOQ=0 THEN 1 ELSE NOQ END)) AS PERDONE FROM ( " + 
                     "SELECT Row_Number() Over(ORDER BY v.VESSELNAME,VIQID) AS SNO,VM.*,VESSELNAME, " +
                     "(SELECT COUNT(*) FROM [dbo].[vw_VIQQuestionRanks] D WHERE D.VESSELCODE=VM.VESSELCODE AND D.VIQID=VM.VIQID ) AS NOQ , " +
                     "(SELECT COUNT(*) FROM [dbo].[vw_VIQQuestionRanks] D WHERE D.VESSELCODE=VM.VESSELCODE AND D.VIQID=VM.VIQID AND  ISNULL(OfficeClosureStatus,0)>0 ) AS DONEQ  " +
                     "FROM [dbo].[VIQ_VIQMaster] VM INNER JOIN DBO.VESSEL V ON V.VESSELCODE=VM.VESSELCODE WHERE INSPGROUPID=" + MainGroup + " " +
                      " ) A WHERE 1=1 ";
        
        string whereclause = "";

        if(ddlVessel.SelectedIndex > 0)
            whereclause += " And VesselCode = '" + ddlVessel.SelectedValue + "'";
        else if (ddlFleet.SelectedIndex > 0)
            whereclause += " And FleetId=" + ddlFleet.SelectedValue;
        if(txtFromDate.Text.Trim()!="")
            whereclause += " And TargetDate >='" + txtFromDate.Text.Trim() + "'";
        if (txtToDate.Text.Trim() != "")
            whereclause += " And TargetDate <='" + txtToDate.Text.Trim() + "'";

        DataTable dt = Budget.getTable(sql + whereclause + " ORDER BY VESSELNAME,VIQNO ").Tables[0];
        rpt_Questions.DataSource = dt;
        rpt_Questions.DataBind();
    }
    protected void btnCreateNow_Click(object sender, EventArgs e)
    {
        int MainGroup = (radSireAdd.Checked) ? 1 : 2;
        if (ddlVesselAdd.SelectedIndex <= 0)
        {
            ProjectCommon.ShowMessage("Please select vessel.");
            ddlVesselAdd.Focus();
            return; 
        }
        if (txt_ClosureDate.Text.Trim() == "")
        {
            ProjectCommon.ShowMessage("Please enter Target Date.");
            txt_ClosureDate.Focus();
            return;
        }
        else
        {
            DataTable dt1 = Common.Execute_Procedures_Select_ByQuery("SELECT TOP 1 TARGETDATE FROM [dbo].[VIQ_VIQMaster] WHERE VESSELCODE IN ( SELECT VESSELCODE FROM DBO.VESSEL WHERE VESSELID=" + ddlVesselAdd.SelectedValue + ") AND INSPGROUPID=" + MainGroup + " ORDER BY TARGETDATE DESC");
            if(dt1.Rows.Count>0)
            {
                DateTime dt = Convert.ToDateTime(dt1.Rows[0][0]);
                if (Convert.ToDateTime(txt_ClosureDate.Text).Subtract(dt).Days <= 90)
                {
                    ProjectCommon.CallScript("$('#dvAdd').show();SetDate();");
                    ProjectCommon.ShowMessage("Next target date must be more than 90 days from last target date.");
                    txt_ClosureDate.Focus();
                    return; 
                }
            }
        }

        ProjectCommon.ExecuteQuery("EXEC DBO.CREATE_VIQ '" + ddlVesselAdd.SelectedValue + "'," + MainGroup + ",'" + txt_ClosureDate.Text.Trim() + "'");
        ddlVesselAdd.SelectedIndex = 0;
        radSireAdd.Checked = true;
        txt_ClosureDate.Text = "";
        BindGrid();
    }

    protected void btnDownloadAndMail_Click(object sender, ImageClickEventArgs e)
    {
        string UserId=Session["loginid"].ToString();

        string FolderPath = Server.MapPath("~/Vetting/TEMP");
        string[] Files = Directory.GetFiles(FolderPath,"*.zip");
        
        foreach (string fl in Files)
        {
            try
            {
                File.Delete(fl);
            }
            catch { }
        }

        string VIQNo=""; 
        int VIQId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        string VesselCode = ((ImageButton)sender).CssClass;

        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM [dbo].[VIQ_VIQMaster] WHERE VESSELCODE='" + VesselCode + "' AND VIQID=" + VIQId.ToString());
        DataTable dt1 = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM [dbo].[VIQ_VIQDetails] WHERE VESSELCODE='" + VesselCode + "' AND VIQID=" + VIQId.ToString());
        DataTable dt2 = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM [dbo].[VIQ_VIQDetailsRanks] WHERE VESSELCODE='" + VesselCode + "' AND VIQID=" + VIQId.ToString());
        DataTable dt21 = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM [dbo].[VIQ_VIQDetailsRanks_Inv] WHERE VESSELCODE='" + VesselCode + "' AND VIQID=" + VIQId.ToString());
        DataTable dt3 = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM [dbo].[VIQ_VIQDetailsRanksAttachments] WHERE VESSELCODE='" + VesselCode + "' AND VIQID=" + VIQId.ToString());
        

        dt.TableName = "VIQ_VIQMaster";
        dt1.TableName = "VIQ_VIQDetails";
        dt2.TableName = "VIQ_VIQDetailsRanks";
        dt21.TableName = "VIQ_VIQDetailsRanks_Inv";
        dt3.TableName = "VIQ_VIQDetailsRanksAttachments";

        VIQNo=dt.Rows[0]["ViqNo"].ToString();

        DataSet ds=new DataSet();
        ds.Tables.Add(dt.Copy());
        ds.Tables.Add(dt1.Copy());
        ds.Tables.Add(dt2.Copy());
        ds.Tables.Add(dt21.Copy());
        ds.Tables.Add(dt3.Copy());

        string SchemaFile = FolderPath + "\\VETTING_SCHEMA.XML";
        string DataFile = FolderPath + "\\VETTING_DATA.XML";

        string ZipFile = FolderPath + "\\VIQ_O_" + VIQNo.Replace("/","-") + ".zip";

        ds.WriteXmlSchema(SchemaFile);
        ds.WriteXml(DataFile);

        using (ZipFile zip = new ZipFile())
        {
            zip.AddFile(SchemaFile);
            zip.AddFile(DataFile);
            zip.Save(ZipFile);


            // SEND MAIL
            string VesselmailAddress = "";
            DataTable dtMail = Common.Execute_Procedures_Select_ByQuery("select email from dbo.vessel where vesselcode='" + VesselCode + "'");
            if (dtMail.Rows.Count > 0)
                VesselmailAddress = dtMail.Rows[0][0].ToString();

            if (VesselmailAddress.Trim() != "")
            {
                string result = SendMail.SendVIQMails(ProjectCommon.getUserEmailByID(UserId), VesselmailAddress, VIQNo, ProjectCommon.getUserNameByID(UserId), ZipFile, true);
                if (result == "SENT")
                {
                    string sql = "UPDATE DBO.VIQ_VIQMaster SET MailSent=1,MailSentOn=GetDate() WHERE VESSELCODE='" + VesselCode + "'AND VIQID=" + VIQId.ToString();
                    Common.Execute_Procedures_Select_ByQuery(sql);
                    ProjectCommon.ShowMessage("Mail sent successfully.");
                    BindGrid();
                }
                else
                {
                    ProjectCommon.ShowMessage("Unable to sent mail.");
                }
            }
            // DOWNLOAD 

            //Response.Clear();
            //Response.ContentType = "application/zip";
            //Response.AddHeader("Content-Type", "application/zip");
            //Response.AddHeader("Content-Disposition", "inline;filename=" + Path.GetFileName(ZipFile));
            //Response.WriteFile(ZipFile);
            //Response.End();
        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        BindGrid();
    }
}
