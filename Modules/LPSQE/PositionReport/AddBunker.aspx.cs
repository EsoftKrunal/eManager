using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
 using System.Text;
using Ionic.Zip;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;
using System.Runtime.InteropServices.WindowsRuntime;
using ShipSoft.CrewManager.BusinessLogicLayer;
using ShipSoft.CrewManager.BusinessObjects;
using CrystalDecisions.ReportAppServer;

public partial class AddBunker : System.Web.UI.Page
{
    Authority Auth;

    public long Reportpk
    {
        get { return Common.CastAsInt32(ViewState["Reportpk"]); }
        set { ViewState["Reportpk"] = value; }
    }

    public string vesselId
    {
        get { return ViewState["vesselId"].ToString(); }
        set { ViewState["vesselId"] = value; }
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
       try
        {
            if (Reportpk <= 0)
            {
                lblMessage.Text = "Please first save the form to export.";
                return;
            }

            DataTable dtVesselEmail = Common.Execute_Procedures_Select_ByQuery("select EMAIL, VesselEmailNew from DBO.VESSEL WHERE VESSELCODE='" + vesselId + "'");
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

                DataTable dtLoginUser = Common.Execute_Procedures_Select_ByQuery("Select Email from UserLogin with(nolock) where LoginId = " + Convert.ToInt32(Session["LoginId"].ToString()) + "");
                if (dtLoginUser.Rows.Count > 0 && !string.IsNullOrWhiteSpace(dtVesselEmail.Rows[0]["Email"].ToString()))
                {
                    CCMails.Add(dtVesselEmail.Rows[0]["Email"].ToString());
                }
            }

            if (EmailAddress.Trim() != "")
            {

                string ReportNoSerial = txtVoyageNumber.Text.Trim();
                DataTable BunkerDetailOffice = Common.Execute_Procedures_Select_ByQuery("select * from [dbo].[VSL_BunkerDetails] with(nolock) WHERE VesselId='" + vesselId + "' AND ReportPk=" + Reportpk.ToString());
                if (BunkerDetailOffice.Rows.Count <= 0)
                {
                    ProjectCommon.ShowMessage("Nothing to export. Please save record first.");
                    return;
                }

                //DataTable dtRN = Common.Execute_Procedures_Select_ByQuery("SELECT ReportNo FROM [DBO].[VSL_BunkerDetails] WHERE VesselCode='" + vesselId + "' AND ReportPk=" + Reportpk.ToString());

                //string ReportNoSerial = dtRN.Rows[0]["ReportNo"].ToString().Trim().Substring(9);
                BunkerDetailOffice.TableName = "VSL_BunkerDetails";

                DataSet ds = new DataSet();
                ds.Tables.Add(BunkerDetailOffice.Copy());

                string SchemaFile = Server.MapPath("~/Modules/LPSQE/Temp/BKR_REPORT_SCHEMA.xml");
                string DataFile = Server.MapPath("~/Modules/LPSQE/Temp/BKR_REPORT_DATA.xml");

                ds.WriteXml(DataFile);
                ds.WriteXmlSchema(SchemaFile);

                string ZipData = Server.MapPath("~/Modules/LPSQE/Temp/BKR_REPORT_O_" + vesselId + "_" + Reportpk.ToString() + ".zip");
                if (File.Exists(ZipData)) { File.Delete(ZipData); }

                using (ZipFile zip = new ZipFile())
                {
                    zip.AddFile(SchemaFile);
                    zip.AddFile(DataFile);
                    zip.Save(ZipData);
                }

                string Subject = "BUNKER REPORT - [ " + ReportNoSerial.ToString() + " ] - Office Reply";

                StringBuilder sb = new StringBuilder();
                sb.Append("Dear Captain,<br /><br />");
                sb.Append("Attached please find the office reply for your Bunker Report.<br /><br />");
                //sb.Append("Please import it in the ship system from PMS communication Tools.<br /><br /><br />");
                sb.Append("Thank You,");

                string fromAddress = ConfigurationManager.AppSettings["FromAddress"].ToString();

                string result = SendMail.SendSimpleMail(fromAddress, EmailAddress, CCMails.ToArray(), BCCMails.ToArray(), Subject, sb.ToString(), ZipData);
                if (result == "SENT")
                {
                    Common.Execute_Procedures_Select_ByQuery("UPDATE [DBO].[VSL_BunkerDetails] SET ExportBy='" + Session["FullName"].ToString() + "', ExportOn = getdate() WHERE VesselId='" + vesselId + "' AND ReportPk=" + Reportpk.ToString());
                    // BindList();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "alert('Exported successfully.');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "abort", "alert('Unable to export. Error : " + result + "');", true);
                }

                //using (ZipFile zip = new ZipFile())
                //{
                //    zip.AddFile(SchemaFile);
                //    zip.AddFile(DataFile);
                //    zip.Save(ZipData);
                //    Response.Clear();
                //    Response.ContentType = "application/zip";
                //    Response.AddHeader("Content-Type", "application/zip");
                //    Response.AddHeader("Content-Disposition", "inline;filename=" + Path.GetFileName(ZipData));
                //    Response.WriteFile(ZipData);
                //    Response.End();
                //}
            }

        }
        catch(Exception ex)
        {
            lblMessage.Text = ex.Message.ToString();
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //--------------------------------- PAGE ACCESS AUTHORITY ----------------------------------------------------
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 1089);
        if (chpageauth <= 0)
            Response.Redirect("blank.aspx");
        //------------------------------------------------------------------------------------------------------------
        //VesselPositionReporting mp = (VesselPositionReporting)this.Master;
        //mp.ShowMenu = false;
        //mp.ShowHeaderbar = false;
        if (Session["loginid"] != null)
        {
            ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 1089);
            OBJ.Invoke();
            Session["Authority"] = OBJ.Authority;
            Auth = OBJ.Authority;
        }
        if (!Page.IsPostBack)
        {
            //try
            //{
            //  var  CurrentVessel = Session["CurrentShip"].ToString();
              

            //}
            //catch
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "exp", "alert('Session Expired !. Please login again.'); window.close();", true);
            //    btnSave.Visible = false;
            //    return;
            //}
            BindddlType();
            
            if (Request.QueryString["ReportPk"] != null)
            {
                hfReportPk.Value = Request.QueryString["ReportPk"].ToString();
                long ReportPk = 0;
                long.TryParse(hfReportPk.Value, out ReportPk);
                Reportpk = ReportPk;
                vesselId = Request.QueryString["VesselId"].ToString();
                getDataAndPopulateForm(ReportPk, Request.QueryString["FuelType"].ToString(), Request.QueryString["VesselId"].ToString());
                ShowAttachment(ReportPk, Request.QueryString["FuelType"].ToString(), Request.QueryString["VesselId"].ToString());
                btnAddDoc.Visible = false;
                btnSave.Visible = (Request.QueryString["Type"].ToString() == "E");
            }
            else
            {
               // btnApprove.Visible = false;
                btnAddDoc.Visible = false;
            }
           // txtVesselId.Text = Session["CurrentShip"].ToString();
            Bindgrid();
            ShowLockUnlock();
            //else
            //{

            //}
        }
        //ManageControlStates();


    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        //int ReportId = Common.CastAsInt32(((ImageButton)sender).CommandArgument.Split('~').GetValue(0));
        string _selectedfueltype = ((ImageButton)sender).CommandArgument.Split('~').GetValue(1).ToString();
        //string ReportType = ((ImageButton)sender).CommandArgument.Split('~').GetValue(2).ToString();
        long reportid = 0;
        long.TryParse(hfReportPk.Value, out reportid);
        if (reportid > 0)//Form is in edit mode
        {
            //string _selectedfueltype = ddlType.SelectedItem.Value;
            getDataAndPopulateForm(reportid, _selectedfueltype, txtVesselId.Text);
            ddlType.ClearSelection();
            ddlType.Items.FindByValue(_selectedfueltype).Selected = true;
            hdnFuelType.Value = _selectedfueltype;
        }
    }
        protected void lnkSummaryReport_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "fas", "window.open('PositionSummaryReport.aspx','');", true);
    }
    protected void Bindgrid()
    {
        long reportid = 0;
        long.TryParse(hfReportPk.Value, out reportid);
        if (reportid > 0)
        {
            string SQL = "select ReportPk,FuelType,LocalDate,UTCDate,BDNNumber,price,BunkerReceivedACC,ActualBunkerReceived from VSL_BunkerDetails with(nolock) where ReportPk=" + hfReportPk.Value + "";
            DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL + " ORDER BY FuelType Desc ");
            rptPR.DataSource = dt;
            rptPR.DataBind();
        }
        else
        {
            rptPR.DataSource = null;
            rptPR.DataBind();
        }
    }

    private void getDataAndPopulateForm(long ReportPk,string FuelType,string VesselId)
    {
        string SQL = "select *  from VSL_BunkerDetails with(nolock) where  ReportPk = " + ReportPk+" and FuelType = '"+FuelType+"' and VesselId = '"+VesselId+"'";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
        if (dt.Rows.Count>0)
        {
            
            hfReportStatus.Value = dt.Rows[0]["ReportStatus"].ToString();
            txtVoyageNumber.Text = dt.Rows[0]["VoyageNumber"].ToString();
            txtVesselId.Text = VesselId.ToString();
            txtPort.Text= dt.Rows[0]["Port"].ToString();
            ddlLocation.ClearSelection();
            ddlLocation.Items.FindByValue(dt.Rows[0]["Location"].ToString()).Selected = true;
            txtLocalDate.Text = Common.ToDateString(dt.Rows[0]["LocalDate"].ToString());
            ddlLocalTimeHours.ClearSelection();
            
            int _LocalDateHrs = 0;
            int.TryParse(dt.Rows[0]["LocalDateHrs"].ToString(), out _LocalDateHrs);
            if (_LocalDateHrs<10)
            { ddlLocalTimeHours.Items.FindByValue("0"+dt.Rows[0]["LocalDateHrs"].ToString()).Selected = true; }
            else { ddlLocalTimeHours.Items.FindByValue(dt.Rows[0]["LocalDateHrs"].ToString()).Selected = true; }
            
            ddlLocalTimeMin.ClearSelection();
            int _LocalDateMins = 0;
            int.TryParse(dt.Rows[0]["LocalDateHrs"].ToString(), out _LocalDateMins);
            if (_LocalDateMins < 10)
            { ddlLocalTimeMin.Items.FindByValue("0"+dt.Rows[0]["LocalDateMins"].ToString()).Selected = true; }
            else { ddlLocalTimeMin.Items.FindByValue(dt.Rows[0]["LocalDateMins"].ToString()).Selected = true; }

            
            txtUTCDate.Text = Common.ToDateString(dt.Rows[0]["UTCDate"].ToString());
            
            ddlUTCTimeHours.ClearSelection();
            int _UTCDateHrs = 0;
            int.TryParse(dt.Rows[0]["UTCDateHrs"].ToString(), out _UTCDateHrs);
            if (_UTCDateHrs<10)
            { ddlUTCTimeHours.Items.FindByValue("0"+dt.Rows[0]["UTCDateHrs"].ToString()).Selected = true; }
            else { ddlUTCTimeHours.Items.FindByValue(dt.Rows[0]["UTCDateHrs"].ToString()).Selected = true; }
            
            ddlUTCTimeMins.ClearSelection();
            int _UTCDateMins = 0;
            int.TryParse(dt.Rows[0]["UTCDateMins"].ToString(), out _UTCDateMins);
            if (_UTCDateMins < 10)

            { ddlUTCTimeMins.Items.FindByValue("0"+dt.Rows[0]["UTCDateMins"].ToString()).Selected = true; }
            else
            { ddlUTCTimeMins.Items.FindByValue(dt.Rows[0]["UTCDateMins"].ToString()).Selected = true; }
            ddlType.ClearSelection();
            if (dt.Rows[0]["FuelType"].ToString() != "")
            {
                ddlType.Items.FindByValue(dt.Rows[0]["FuelType"].ToString()).Selected = true;
                lblBunkerFuelType.Text = "( " + dt.Rows[0]["FuelType"].ToString() + " )";
                hdnFuelType.Value = dt.Rows[0]["FuelType"].ToString().Trim();
            }
           
            txtBDNNumber.Text = dt.Rows[0]["BDNNumber"].ToString();
            txtSulpherPercent.Text = dt.Rows[0]["Sulphar"].ToString();
            txtDensity.Text = dt.Rows[0]["Density"].ToString();
            txtLCV.Text = dt.Rows[0]["LCV"].ToString();
            txtPrice.Text = dt.Rows[0]["Price"].ToString();
            txtBunkerReceivedacctoBDNmt.Text = dt.Rows[0]["BunkerReceivedACC"].ToString();
            txtActualBunkerReceivedmt.Text = dt.Rows[0]["ActualBunkerReceived"].ToString();
            txtRemarks.Text = dt.Rows[0]["Remarks"].ToString();
            txtSampleSentToCompany.Text = dt.Rows[0]["SampleSentTo"].ToString();
            txtSealNumber.Text = dt.Rows[0]["SealNumber"].ToString();
            txtAirwayBillNumber.Text = dt.Rows[0]["AirwayBillNumber"].ToString();
            txtForwardingInstructionNumber.Text = dt.Rows[0]["ForwardindInsNumber"].ToString();
            txtName.Text = dt.Rows[0]["Name"].ToString();
            txtPhone.Text = dt.Rows[0]["Phone"].ToString();
            txtFax.Text = dt.Rows[0]["Fax"].ToString();
            txtEmail.Text = dt.Rows[0]["Email"].ToString();
            int _IsAnyReceivedFromLab = 0;
           int.TryParse(dt.Rows[0]["IsAnyReceivedFromLab"].ToString(),out _IsAnyReceivedFromLab);
            cbFuelTestingReceived.Checked = _IsAnyReceivedFromLab ==1?  true:   false;

        }
        else
        { clearForm(); }

    }


    private void ManageControlStates()
    {
        //MASTER
        //CHENG
        long reportid = 0;
        long.TryParse(hfReportPk.Value, out reportid);
        string reportstatus = hfReportStatus.Value;
        if (reportid==0)//matlab new report hai
        {
            txtVoyageNumber.ReadOnly = false;
            txtVesselId.ReadOnly = true;
            txtPort.ReadOnly = false;
            ddlLocation.ClearSelection();
            ddlLocation.Items.FindByValue("0").Selected = true;
           // btnApprove.Visible = false;
            btnSave.Visible = true;
            btnExport.Visible = false;

        }
        
       else if (reportid > 0 && reportstatus=="0")//Form is in edit mode lakin abhi approve nahi hui hai
        {
            txtVoyageNumber.ReadOnly = true;
            txtVesselId.ReadOnly = true;
            txtPort.ReadOnly = true;
            //if (Session["UserName"].ToString() == "MASTER" || Session["UserName"].ToString() == "CHENG")
            //{
            //    btnApprove.Visible = true;
            //}
            //else
            //{ btnApprove.Visible = false; }
        }
        else
        {
            btnSave.Visible = false;
           // btnApprove.Visible = false;
            btnExport.Visible = true;

        }




    }
    private void clearForm()
    {
        long reportid = 0;
        long.TryParse(hfReportPk.Value, out reportid);
        if (reportid == 0)//Form is in edit mode
        {
            txtVoyageNumber.Text = "";
            txtPort.Text = "";
            ddlLocation.ClearSelection();
            ddlLocation.Items.FindByValue("0").Selected = true;

        }
        txtLocalDate.Text = "";
        ddlLocalTimeHours.ClearSelection();
        ddlLocalTimeHours.Items.FindByValue("").Selected = true;
        ddlLocalTimeMin.ClearSelection();
        ddlLocalTimeMin.Items.FindByValue("").Selected = true;
        txtUTCDate.Text = "";
        ddlUTCTimeHours.ClearSelection();
        ddlUTCTimeHours.Items.FindByValue("").Selected = true;
        ddlUTCTimeMins.ClearSelection();
        ddlUTCTimeMins.Items.FindByValue("").Selected = true;
        ddlType.ClearSelection();
        ddlType.Items.FindByValue("").Selected = true;
        txtBDNNumber.Text = "";
        txtSulpherPercent.Text = "";
        txtDensity.Text = "";
        txtLCV.Text = "";
        txtPrice.Text = "";
        txtBunkerReceivedacctoBDNmt.Text = "";
        txtActualBunkerReceivedmt.Text = "";
        txtRemarks.Text = "";
        txtSampleSentToCompany.Text = "";
        txtSealNumber.Text = "";
        txtAirwayBillNumber.Text = "";
        txtForwardingInstructionNumber.Text = "";
        txtName.Text = "";
        txtPhone.Text = "";
        txtFax.Text = "";
        txtEmail.Text = "";
        cbFuelTestingReceived.Checked = false;

    }


    private void BindddlType()
    {
        string sql = "SELECT    [FuelTypeName] ,[ShortName] FROM [MRV_FuelTypes] with (nolock) order by [ShortName]";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
        ddlType.Items.Clear();
        ddlType.Items.Add(new ListItem { Text = "", Value = "" });
        foreach (DataRow dr in dt.Rows)
        {
            ddlType.Items.Add(new ListItem { Text = dr["FuelTypeName"].ToString(), Value = dr["ShortName"].ToString() });
        }
    }

    //protected void btnApprove_Click(object sender, EventArgs e)
    //{
    //    long reportPk = 0;
    //    long.TryParse(hfReportPk.Value, out reportPk);
    //    if (reportPk > 0)
    //    {
           
    //        string vesselId = Session["CurrentShip"].ToString();
    //        string addedBy = Session["UID"].ToString();
    //        UpdateBunkerReportStatus(reportPk, vesselId, "1", addedBy);
    //    }
    //   // ManageControlStates();


    //}
        protected void btnSaveClick(object sender, EventArgs e)
    {
        // Get the values from form fields
        DateTime localDate ;
        DateTime.TryParse(txtLocalDate.Text, out localDate);
        int localTimeHours = 0;
        int.TryParse(ddlLocalTimeHours.SelectedValue, out localTimeHours);
        int localTimeMin = 0;
        int.TryParse( ddlLocalTimeMin.SelectedValue,out localTimeMin);

        DateTime utcDate;
        DateTime.TryParse(txtUTCDate.Text,out utcDate);
        int utcTimeHours = 0;
        int.TryParse(ddlUTCTimeHours.SelectedValue,out utcTimeHours);

        int utcTimeMins = 0;
        int.TryParse(ddlUTCTimeMins.SelectedValue,out utcTimeMins);
        
        string port = txtPort.Text;
        //bool anchorage = cbAnchorage.Checked;
        string fuelType = ddlType.SelectedItem.Value;
        // Add other form fields as needed

        // Sample parameters for Bunker details
        string bdnNumber = txtBDNNumber.Text;
        decimal sulpherPercent = 0;
        decimal.TryParse(txtSulpherPercent.Text,out sulpherPercent);
        string density = txtDensity.Text;
        decimal lcv = 0;
        decimal.TryParse(txtLCV.Text, out lcv);
        // Add other parameters for Bunker details
        decimal price = 0;
        decimal.TryParse(txtPrice.Text, out price);
        // Sample parameters for Sample details
        string sampleSentToCompany = txtSampleSentToCompany.Text;//ddlSampleSentToCompany.SelectedValue;
        string sealNumber = txtSealNumber.Text;
        string airwayBillNumber = txtAirwayBillNumber.Text;
        string forwardingInstructionNumber = txtForwardingInstructionNumber.Text;
        string name = txtName.Text;
        string phone = txtPhone.Text;
        string fax = txtFax.Text;
        string email = txtEmail.Text;

        // Add other parameters for Sample details

        // Sample parameters for Quantity Unit

        // Add other parameters for Quantity Unit

        // Sample parameters for Bunker Received according to BDN
        decimal bunkerReceivedAccToBDNmt = 0;
        decimal.TryParse(txtBunkerReceivedacctoBDNmt.Text,out bunkerReceivedAccToBDNmt);
        //string bunkerReceivedAccToBDNm3 = txtBunkerReceivedacctoBDNm3.Text;
        // Add other parameters for Bunker Received according to BDN

        // Sample parameters for Actual Bunker Received
        decimal actualBunkerReceivedmt = 0;
        decimal.TryParse(txtActualBunkerReceivedmt.Text,out actualBunkerReceivedmt);
        //string actualBunkerReceivedm3 = txtActualBunkerReceivedm3.Text;
        // Add other parameters for Actual Bunker Received

        // Sample parameter for Remarks
        string remarks = txtRemarks.Text;
        // Add other parameters for Remarks

        // Sample parameters for Fuel Testing
        bool fuelTestingReceived = cbFuelTestingReceived.Checked;
        // Add other parameters for Fuel Testing
        string vesselId = txtVesselId.Text.Trim();
        string addedBy = Session["UID"].ToString();
        string voyageNumber=txtVoyageNumber.Text;
        string _location = "";
        _location = ddlLocation.SelectedValue;
        int location = 0;
        int.TryParse(_location,out location);//need to discuss
        string quantityUnit = "MT";//rblQuantityUnit.SelectedValue;//need to discuss
        byte bunkerQtyUnit = 1;//need to discuss
        char reportStatus = '0';//ned to discuss
        long reportPk = 0;
        long.TryParse(hfReportPk.Value, out reportPk);
        if (reportPk == 0 && cbFuelTestingReceived.Checked==true && FU.HasFile==false)
        {
            ProjectCommon.ShowMessage("Please Attach the report.");
            return;
        }
        else if (CountAttachments(reportPk,vesselId,fuelType)==0   && cbFuelTestingReceived.Checked == true && FU.HasFile == false)
        {
            ProjectCommon.ShowMessage("Please Attach the report.");
            return;
        }
        InsertOrUpdateVSLBunkerDetails(reportPk, vesselId, localDate, localTimeHours, localTimeMin, port, location, fuelType, bdnNumber
          , sulpherPercent, density, bunkerQtyUnit, lcv, price, bunkerReceivedAccToBDNmt, actualBunkerReceivedmt, remarks, null, reportStatus
          , addedBy, DateTime.Now, addedBy, DateTime.Now, sampleSentToCompany, sealNumber, airwayBillNumber, forwardingInstructionNumber, name
          , phone, fax, email, fuelTestingReceived ? '1' : '0', null,utcDate,utcTimeHours,utcTimeMins, voyageNumber
            );
        uploadfile();
        //Bindgrid();
        //clearForm();
        string ReportPage = "Voyage.aspx";

        ScriptManager.RegisterStartupScript(Page, this.GetType(), "Show Report", "window.open('" + ReportPage + "', '');", true);

    }


    private void UpdateBunkerReportStatus(long reportPk, string vesselId, string reportStatus, string modifiedBy)
    {
        using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["EMANAGER"].ToString()))
        {
            string query = @"UPDATE [dbo].[VSL_BunkerDetails]
                             SET 
                                [ReportStatus] = @ReportStatus,
                                [ModifiedBy] = @ModifiedBy,
                                [ModifiedOn] = GETDATE()
                             WHERE [ReportPk] = @ReportPk
                                AND [VesselId] = @VesselId";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ReportPk", reportPk);
            command.Parameters.AddWithValue("@VesselId", vesselId);
            command.Parameters.AddWithValue("@ReportStatus", reportStatus);
            command.Parameters.AddWithValue("@ModifiedBy", modifiedBy);

            try
            {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                
            }
            catch (Exception ex)
            {
                ProjectCommon.ShowMessage(ex.Message.ToString());
            }
        }
    }




        public bool InsertOrUpdateVSLBunkerDetails(
        long reportPk,
        string vesselId,
        DateTime localDate,
        int localDateHrs,
        int localDateMins,
        string port,
        int location,
        string fuelType,
        string bdnNumber,
        decimal sulphar,
        string density,
        byte bunkerQtyUnit,
        decimal lcv,
        decimal price,
        decimal bunkerReceivedACC,
        decimal actualBunkerReceived,
        string remarks,
        byte[] attachment1,
        char reportStatus,
        string addedBy,
        DateTime addedOn,
        string modifiedBy,
        DateTime modifiedOn,
        string sampleSentTo,
        string sealNumber,
        string airwayBillNumber,
        string forwardingInsNumber,
        string name,
        string phone,
        string fax,
        string email,
        char isAnyReceivedFromLab,
        byte[] officeAttachment,
        DateTime utcDate,
        int utcDateHrs,
        int utcDateMins,
        string voyageNumber
        )
    {
        bool res = false;

        try
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["EMANAGER"].ToString()))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("DBO.usp_InsertOrUpdateVSL_BunkerDetails", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Set parameters for the stored procedure
                    cmd.Parameters.Add(new SqlParameter("@ReportPk", SqlDbType.BigInt)).Value = reportPk;
                    cmd.Parameters.Add(new SqlParameter("@VesselId", SqlDbType.Char, 3)).Value = vesselId;
                    cmd.Parameters.Add(new SqlParameter("@LocalDate", SqlDbType.SmallDateTime)).Value = localDate;
                    cmd.Parameters.Add(new SqlParameter("@LocalDateHrs", SqlDbType.Int)).Value = localDateHrs;
                    cmd.Parameters.Add(new SqlParameter("@LocalDateMins", SqlDbType.Int)).Value = localDateMins;
                    cmd.Parameters.Add(new SqlParameter("@Port", SqlDbType.VarChar, 50)).Value = port;
                    cmd.Parameters.Add(new SqlParameter("@Location", SqlDbType.Int)).Value = location;
                    cmd.Parameters.Add(new SqlParameter("@FuelType", SqlDbType.VarChar, 50)).Value = fuelType;
                    cmd.Parameters.Add(new SqlParameter("@BDNNumber", SqlDbType.VarChar, 50)).Value = bdnNumber;
                    cmd.Parameters.Add(new SqlParameter("@Sulphar", SqlDbType.Decimal)).Value = sulphar;
                    cmd.Parameters.Add(new SqlParameter("@Density", SqlDbType.NVarChar, 20)).Value = density;
                    cmd.Parameters.Add(new SqlParameter("@BunkerQtyUnit", SqlDbType.TinyInt)).Value = bunkerQtyUnit;
                    cmd.Parameters.Add(new SqlParameter("@LCV", SqlDbType.Decimal)).Value = lcv;
                    cmd.Parameters.Add(new SqlParameter("@Price", SqlDbType.Decimal)).Value = price;
                    cmd.Parameters.Add(new SqlParameter("@BunkerReceivedACC", SqlDbType.Decimal)).Value = bunkerReceivedACC;
                    cmd.Parameters.Add(new SqlParameter("@ActualBunkerReceived", SqlDbType.Decimal)).Value = actualBunkerReceived;
                    cmd.Parameters.Add(new SqlParameter("@Remarks", SqlDbType.NVarChar, 500)).Value = remarks;
                    //cmd.Parameters.Add(new SqlParameter("@Attachment1", SqlDbType.VarBinary, -1)).Value = attachment1;
                    cmd.Parameters.Add(new SqlParameter("@ReportStatus", SqlDbType.Char, 1)).Value = reportStatus;
                    cmd.Parameters.Add(new SqlParameter("@AddedBy", SqlDbType.VarChar, 50)).Value = addedBy;
                    cmd.Parameters.Add(new SqlParameter("@AddedOn", SqlDbType.SmallDateTime)).Value = addedOn;
                    cmd.Parameters.Add(new SqlParameter("@ModifiedBy", SqlDbType.VarChar, 50)).Value = modifiedBy;
                    cmd.Parameters.Add(new SqlParameter("@ModifiedOn", SqlDbType.SmallDateTime)).Value = modifiedOn;
                    cmd.Parameters.Add(new SqlParameter("@SampleSentTo", SqlDbType.VarChar, 50)).Value = sampleSentTo;
                    cmd.Parameters.Add(new SqlParameter("@SealNumber", SqlDbType.VarChar, 20)).Value = sealNumber;
                    cmd.Parameters.Add(new SqlParameter("@AirwayBillNumber", SqlDbType.VarChar, 20)).Value = airwayBillNumber;
                    cmd.Parameters.Add(new SqlParameter("@ForwardingInsNumber", SqlDbType.VarChar, 20)).Value = forwardingInsNumber;
                    cmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.VarChar, 50)).Value = name;
                    cmd.Parameters.Add(new SqlParameter("@Phone", SqlDbType.VarChar, 20)).Value = phone;
                    cmd.Parameters.Add(new SqlParameter("@Fax", SqlDbType.VarChar, 20)).Value = fax;
                    cmd.Parameters.Add(new SqlParameter("@Email", SqlDbType.VarChar, 50)).Value = email;
                    cmd.Parameters.Add(new SqlParameter("@IsAnyReceivedFromLab", SqlDbType.Char, 1)).Value = isAnyReceivedFromLab;
                    //cmd.Parameters.Add(new SqlParameter("@OfficeAttachment", SqlDbType.VarBinary, -1)).Value = officeAttachment;
                    cmd.Parameters.Add(new SqlParameter("@UTCDate", SqlDbType.SmallDateTime)).Value = utcDate;
                    cmd.Parameters.Add(new SqlParameter("@UTCDateHrs", SqlDbType.Int)).Value = utcDateHrs;
                    cmd.Parameters.Add(new SqlParameter("@UTCDateMins", SqlDbType.Int)).Value = utcDateMins;
                    cmd.Parameters.Add(new SqlParameter("@voyageNumber", SqlDbType.VarChar, 50)).Value = voyageNumber;
                    SqlParameter outputReportPk = new SqlParameter("@ReportID", SqlDbType.BigInt);
                    outputReportPk.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outputReportPk);
                    cmd.ExecuteNonQuery();
                    long insertedReportPk = (long)outputReportPk.Value;
                    hfReportPk.Value = insertedReportPk.ToString();
                    res = true;
                }
            }
        }
        catch (Exception ex)
        {
            // Handle exceptions as needed
            // Console.WriteLine("Error: " + ex.Message);
            ProjectCommon.ShowMessage(ex.Message.ToString());
            res = false;
        }

        return res;
    }



    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        //long reportid = 0;
        //long.TryParse(hfReportPk.Value, out reportid);
        //if (reportid > 0)//Form is in edit mode
        //{
        //    string _selectedfueltype = ddlType.SelectedItem.Value;
        //    getDataAndPopulateForm(reportid, _selectedfueltype, txtVesselId.Text);
        //    ddlType.ClearSelection();
        //    ddlType.Items.FindByValue(_selectedfueltype).Selected= true;
        //}
                
    }

    protected void ImgDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            long ReportPk = 0;
            long.TryParse(hfReportPk.Value, out ReportPk);
            string vesselid=txtVesselId.Text;
                int DocId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
            string sql = "";
            if (ReportPk > 0)
            {
                sql = "Delete from VSL_BunkerAttachments  WHERE [Vesselid] = '" + vesselid + "' AND  ReportPk =" + ReportPk + " AND DocId = " + DocId;
                DataTable DT = Common.Execute_Procedures_Select_ByQuery(sql);
            }
             
            ShowAttachment(ReportPk,"",vesselid);
        }
        catch (Exception ex)
        {
            ProjectCommon.ShowMessage(ex.Message.ToString());
        }

    }


    private int CountAttachments(long ReportPk,   string VesselId,string FuelType)
    {
        int retvar=0;
        //select count(1) from VSL_BunkerAttachments where ReportPk = 13 and VesselId = 'PIN' and FuelType = 'LSMsadsGO'
        string sql = "SELECT * FROM [VSL_BunkerAttachments] where   ReportPk=" + ReportPk + " and VesselId='" + VesselId.Trim() + "'";
        DataTable DT = Common.Execute_Procedures_Select_ByQuery(sql);
        if (DT.Rows.Count > 0)
        { int.TryParse(DT.Rows[0][0].ToString(), out retvar); }
        
        return retvar;
    }

    public void ShowAttachment(long ReportPk, string DocId, string VesselId)
    {
        string sql = "";
       

        if (ReportPk > 0)
        {
            sql = "SELECT * FROM [VSL_BunkerAttachments] where   ReportPk=" + ReportPk + " and VesselId='"+ VesselId.Trim()+"'";
            DataTable DT = Common.Execute_Procedures_Select_ByQuery(sql);
            rptDocuments.DataSource = DT;
            rptDocuments.DataBind();
        }
        else
        {
            
            rptDocuments.DataSource = null;
            rptDocuments.DataBind();
        }

    }

    protected void btnAddDoc_Click(object sender, EventArgs e)
    {
        uploadfile();
    }
    private void uploadfile()
    {
        lblMessage.Text = "";
        try
        {
            if (rptDocuments.Items.Count >= 10)
            {
                lblMessage.Text = "Maximum 10 documents allowed, 400KB each.";
                return;
            }

            if (FU.HasFile)
            {
                string fileContent = FU.PostedFile.ContentType;
                if (!fileContent.Contains("pdf"))
                {
                    lblMessage.Text = "only pdf files are allowed";
                    FU.Focus();
                    return;
                }

                if (FU.PostedFile.ContentLength > (1024 * 1024 * 0.4))
                {
                    lblMessage.Text = "File Size is Too big! Maximum Allowed is 400KB...";
                    FU.Focus();
                    return;
                }



                string FileName = Path.GetFileName(FU.PostedFile.FileName);
                Stream fs = FU.PostedFile.InputStream;
                BinaryReader br = new BinaryReader(fs);
                byte[] bytes = br.ReadBytes((Int32)fs.Length);
                long _ReportPk = 0;
                long.TryParse(hfReportPk.Value, out _ReportPk);

                Common.Set_Procedures("[dbo].[InsertUpdate_VSL_BunkerAttachments]");
                Common.Set_ParameterLength(8);
                Common.Set_Parameters(
                    new MyParameter("@DocId", 0),
                    new MyParameter("@ReportPk", _ReportPk),
                    new MyParameter("@vesselId", txtVesselId.Text),
                    new MyParameter("@FuelType", ddlType.SelectedItem.Value),
                    new MyParameter("@FileName", FileName),
                    new MyParameter("@Description", txtfileDescription.Text),
                    new MyParameter("@Attachment", bytes),
                    new MyParameter("@ContentType", fileContent)

                    );
                DataSet ds = new DataSet();
                ds.Clear();
                Boolean res;
                res = Common.Execute_Procedures_IUD(ds);
                if (res)
                {
                    ProjectCommon.ShowMessage("Document saved Successfully.");
                    ShowAttachment(_ReportPk, ddlType.SelectedItem.Value, txtVesselId.Text);
                }
                else
                {
                    lblMessage.Text = "Unable to add Document.Error :" + Common.getLastError();
                }


            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message.ToString();
        }
    }

    protected void ValidateSulpherPercent(object source, ServerValidateEventArgs args)
    {
        if (!string.IsNullOrEmpty(args.Value))
        {
            decimal percent;
            if (decimal.TryParse(args.Value, out percent))
            {
                args.IsValid = percent >= 0 && percent <= 100;
            }
            else
            {
                args.IsValid = false;
            }
        }
    }
    protected void ValidateDensity(object source, ServerValidateEventArgs args)
    {
        if (!string.IsNullOrEmpty(args.Value))
        {
            decimal density;
            if (decimal.TryParse(args.Value, out density))
            {
                args.IsValid = density >= 0;
            }
            else
            {
                args.IsValid = false;
            }
        }
    }

    protected void btnLockUnlock_Click(object sender, EventArgs e)
    {
        if (btnLockUnlock.Text.Trim().ToLower().Contains("unlock"))
        {
            Common.Execute_Procedures_Select_ByQuery("UPDATE [dbo].[VSL_BunkerDetails] SET Locked='N' WHERE VesselId='" + vesselId + "' AND ReportPk=" + Reportpk.ToString());
        }
        else
        {
            Common.Execute_Procedures_Select_ByQuery("UPDATE [dbo].[VSL_BunkerDetails] SET Locked='Y' WHERE VesselId='" + vesselId + "' AND ReportPk=" + Reportpk.ToString());
        }
        ShowLockUnlock();
    }
    protected void ShowLockUnlock()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM [DBO].[VSL_BunkerDetails] WHERE VesselId='" + vesselId + "' AND [ReportPk] = " + Reportpk.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            if (Convert.IsDBNull(dt.Rows[0]["Locked"]))
                btnLockUnlock.Text = "Lock for Ship";
            else
                btnLockUnlock.Text = (dt.Rows[0]["Locked"].ToString() == "Y") ? "Unlock for Ship" : "Lock for Ship";
        }
    }


}
