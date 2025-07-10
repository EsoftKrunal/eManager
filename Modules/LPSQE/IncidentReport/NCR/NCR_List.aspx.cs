using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using Ionic.Zip;
using System.Text;
using System.Configuration;

public partial class eReports_G118_G118_List : System.Web.UI.Page
{
    string FormNo = "NCR";
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        if (!IsPostBack)
        {
            BindFormDetails();
            BindFleet();
            BindVessels();
            BindList();
        }
    }
    public void BindFormDetails()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM [DBO].[ER_Master] WHERE [FormNo]='" + FormNo + "'");
        if (dt != null && dt.Rows.Count > 0)
        {
            lblReportName.Text = dt.Rows[0]["FormName"].ToString();
        }

    }
    public void BindFleet()
    {
        try
        {
            DataTable dtFleet = Common.Execute_Procedures_Select_ByQuery("SELECT FleetId,FleetName as Name FROM dbo.FleetMaster");
            this.ddlFleet.DataSource = dtFleet;
            this.ddlFleet.DataValueField = "FleetId";
            this.ddlFleet.DataTextField = "Name";
            this.ddlFleet.DataBind();
            ddlFleet.Items.Insert(0, new ListItem("< All Fleet >", "0"));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void BindVessels()
    {
        DataTable dtVessels = new DataTable();
        string strvessels = "SELECT VesselId,VesselCode,VesselName FROM [dbo].[Vessel] WHERE VesselStatusid<>2 and VesselId in (Select VesselId from UserVesselRelation with(nolock) where Loginid = " + Convert.ToInt32(Session["loginid"].ToString()) + ") ORDER BY VesselName";
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
    private void BindRegisters()
    {
        //ProjectCommon.LoadRegisters(cblCategory, FormNo, 20);
    }
    protected void BindList()
    {
        string sqL = "SELECT ROW_NUMBER() OVER(Order By R.ReportId) AS SrNo,(CASE WHEN ISNULL(IS_CLOSED,'N')='Y' THEN 'N' WHEN ISNULL(O.IS_CLOSED,'N')='Y' THEN 'N' ELSE 'Y' END) as Edit_ALLOWED,R.[ReportId],[ReportNo], R.VesselCode, " +
                     "NCRCreatedDate, (SELECT [OptionText] FROM [DBO].ER_RegisterOptions WHERE RegisterId = 21 AND OptionId = R.AreaAudited) AS AreaAudited,R.CreatedBy, R.CreatedOn,(CASE WHEN ISNULL(O.IS_CLOSED,'N')='N' THEN 'Open' WHEN ISNULL(O.IS_CLOSED,'N')='' THEN 'Open' ELSE 'Closed' END) as Status, O.[ExportedBy] FROM [DBO].[ER_G118_Report] R " +
                     "LEFT JOIN [DBO].[ER_G118_Report_Office] O ON O.ReportId = R.ReportId AND O.VesselCode = R.VesselCode " +
                     "WHERE ISNULL(O.Status,'A') = 'A'";
        if (txtFd.Text.Trim() != "")
        {
            sqL += " AND NCRCreatedDate >='" + txtFd.Text.Trim() + "'";
        }
        if (txtTD.Text.Trim() != "")
        {
            sqL += " AND NCRCreatedDate <='" + txtTD.Text.Trim() + "'";
        }
        //string InternalClause = ""; 
        //foreach (string cat in getCheckedItemstoArray(cblCategory))
        //{
        //    if (cat.Trim() != "")
        //    {
        //        InternalClause += ((InternalClause.Trim()=="")?"":" OR ") + " ( ',' + Category + ',' Like '%" + cat.Trim() + "%' ) ";
        //    }
        //}
        //if(InternalClause.Trim()!="")
        //    sqL += " AND ( " + InternalClause + " )";

        if (ddlStatus.SelectedValue.Trim() != "0")
        {
            sqL += " AND ISNULL(O.Is_Closed,'N') = '" + ddlStatus.SelectedValue.Trim() + "' ";
        }

        string WhereCondition = " ";
        if (ddlFleet.SelectedIndex != 0)
        {
            if (ddlVessels.SelectedIndex == 0)
            {
                WhereCondition = WhereCondition + "AND R.VesselCode IN (SELECT VesselCode FROM  dbo.Vessel WHERE FleetId = " + ddlFleet.SelectedValue + ")";
            }
            else
            {
                WhereCondition = WhereCondition + "AND R.VesselCode = '" + ddlVessels.SelectedValue.ToString() + "' ";
            }
        }
        else if (ddlVessels.SelectedIndex != 0)
        {
            WhereCondition = WhereCondition + "AND R.VesselCode = '" + ddlVessels.SelectedValue.ToString() + "' ";
        }

        sqL += WhereCondition;

        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sqL + " order by NCRCreatedDate desc");
        if (dt != null)
        {
            rptReports.DataSource = dt;
            rptReports.DataBind();
        }
    }
    protected void lnkReport_OnClick(object sender, EventArgs e)
    {

    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        int Key = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        string VesselCode = ((ImageButton)sender).Attributes["VesselCode"];

        DataTable dtVesselEmail = Common.Execute_Procedures_Select_ByQuery("select EMAIL, VesselEmailNew from DBO.VESSEL WHERE VESSELCODE='" + VesselCode + "'");
        string EmailAddress = "";
        List<string> CCMails = new List<string>();
        List<string> BCCMails = new List<string>();
        if (dtVesselEmail.Rows.Count > 0)
        {
            EmailAddress = dtVesselEmail.Rows[0]["VesselEmailNew"].ToString();
            if (!string.IsNullOrWhiteSpace(dtVesselEmail.Rows[0]["EMAIL"].ToString()))
            {
                BCCMails.Add(dtVesselEmail.Rows[0]["EMAIL"].ToString());
            }

            DataTable dtLoginUser = Common.Execute_Procedures_Select_ByQuery("Select Email from UserLogin with(nolock) where LoginId = =" + Convert.ToInt32(Session["LoginId"].ToString()) + "");
            if (dtLoginUser.Rows.Count > 0 && !string.IsNullOrWhiteSpace(dtVesselEmail.Rows[0]["Email"].ToString()))
            {
                CCMails.Add(dtVesselEmail.Rows[0]["Email"].ToString());
            }
        }
        if (EmailAddress.Trim() != "")
        {
            // EXPORT INTO FOLDER ( ~/HSSQE/TEMP )  , 

            DataTable Accident_Report_Office = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM [dbo].[Accident_Report_Office] WHERE VesselCode='" + VesselCode + "' AND REPORTID=" + Key.ToString());
            if (Accident_Report_Office.Rows.Count <= 0)
            {
                ProjectCommon.ShowMessage("Nothing to export. Please save record first.");
                return;
            }


            DataTable dtRN = Common.Execute_Procedures_Select_ByQuery("SELECT ReportNo FROM [DBO].[Accident_Report] WHERE VesselCode='" + VesselCode + "' AND REPORTID=" + Key.ToString());
            string ReportNoSerial = dtRN.Rows[0]["ReportNo"].ToString().Trim().Substring(9);

            Accident_Report_Office.TableName = "Accident_Report_Office";


            DataSet ds = new DataSet();
            ds.Tables.Add(Accident_Report_Office.Copy());

            string SchemaFile = Server.MapPath("../../Temp/SCHEMA_" + FormNo + ".xml");
            string DataFile = Server.MapPath("../../Temp/DATA_" + FormNo + ".xml");

            ds.WriteXml(DataFile);
            ds.WriteXmlSchema(SchemaFile);

            string ZipData = Server.MapPath("../../Temp/ER_O_" + VesselCode + "_" + ReportNoSerial + ".zip");
            if (File.Exists(ZipData)) { File.Delete(ZipData); }

            using (ZipFile zip = new ZipFile())
            {
                zip.AddFile(SchemaFile);
                zip.AddFile(DataFile);
                zip.Save(ZipData);
            }


            // SEND MAIL TO EMAIL ADDRESS 

            
            string Subject = "[ " + dtRN.Rows[0]["ReportNo"].ToString() + " ] - Office Reply";

            StringBuilder sb = new StringBuilder();
            sb.Append("Dear Captain,<br /><br />");
            sb.Append("Attached please find the office reply for your NCR.<br /><br />");
            sb.Append("Please import it in the ship system from PMS communication Tools.<br /><br /><br />");
            sb.Append("Thank You,");
            string fromAddress = ConfigurationManager.AppSettings["FromAddress"].ToString();
            string result = SendMail.SendSimpleMail(fromAddress, EmailAddress, CCMails.ToArray(), BCCMails.ToArray(), Subject, sb.ToString(), ZipData);
            if (result == "SENT")
            {
                Common.Execute_Procedures_Select_ByQuery("UPDATE [DBO].[Accident_Report_Office] SET ExportedBy='" + Session["FullName"].ToString() + "', ExportedOn = getdate() WHERE VesselCode='" + VesselCode + "' AND REPORTID=" + Key.ToString());
                BindList();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "alert('Exported successfully.');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "abort", "alert('Unable to export. Error : " + result + "');", true);
            }
        }

    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        BindList();
    }
    protected void ddlFleet_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlFleet.SelectedIndex != 0)
        {
            DataTable dtVessels = new DataTable();
            string strvessels = "SELECT VesselId,VesselCode,VesselName FROM [dbo].[Vessel] WHERE FleetId = " + ddlFleet.SelectedValue + " AND VesselStatusid<>2  ORDER BY VesselName";
            dtVessels = Common.Execute_Procedures_Select_ByQuery(strvessels);
            ddlVessels.Items.Clear();
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
            ddlVessels.Items.Insert(0, "< All >");
        }
        else
        {
            ddlVessels.Items.Clear();
            BindVessels();
        }
    }
}