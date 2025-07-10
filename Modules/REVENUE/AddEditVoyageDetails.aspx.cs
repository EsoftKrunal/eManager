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
using Ionic.Zip;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Globalization;

public partial class Modules_REVENUE_AddEditVoyageDetails : System.Web.UI.Page
{
    int Login_Id;
    int ContractId = 0;
    
   // int ContractTypeId = 0;
    public int ContractTypeId
    {
        get { return Convert.ToInt32(ViewState["ContractTypeId"].ToString()); }
        set { ViewState["ContractTypeId"] = value; }
    }

    public decimal AddComPercentage
    {
        get { return Convert.ToDecimal(ViewState["AddComPercentage"].ToString()); }
        set { ViewState["AddComPercentage"] = value; }
    }
    public decimal AddComAmount
    {
        get { return Convert.ToDecimal(ViewState["AddComAmount"].ToString()); }
        set { ViewState["AddComAmount"] = value; }
    }
    Authority Auth;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ProjectCommon.SessionCheck_New();
            int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 1094);
            if (chpageauth <= 0)
                Response.Redirect("blank.aspx");
            if (Session["loginid"] == null)
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Login.aspx';", true);
            }
            else
            {
                Login_Id = Convert.ToInt32(Session["loginid"].ToString());
            }

            if (Page.Request.QueryString["ContractId"] != null)
            {
                ContractId = Convert.ToInt32(Page.Request.QueryString["ContractId"]);
                hdnContractId.Value = Page.Request.QueryString["ContractId"];
            }

            if (Session["loginid"] != null)
            {
                ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 14);
                OBJ.Invoke();
                Session["Authority"] = OBJ.Authority;
                Auth = OBJ.Authority;
            }
            if (!Page.IsPostBack)
            {
                if (ContractId > 0)
                {
                    Show_Contract_Record(ContractId);
                    BindExpectedExpenses(ContractId, ContractTypeId);
                    BindVoyageDetails(ContractId);
                    GetTotalOffhireandActualExpenses();
                    DataTable dt88 = Revenue.AdVesselContractDetails(ContractId, 0, DateTime.Now, 0, 0, DateTime.Now, 0, 0, 0, 0, 0, 0, 0, 0, "SELECT", 0, 0, 0, 0, 0, 0, 0, "");
                    string ContractStatus = "";
                    if (dt88.Rows.Count > 0)
                    {
                        ContractStatus = dt88.Rows[0]["Status"].ToString();
                    }

                    if (ContractStatus != "Open")
                    {
                        btnSaveVoyage.Enabled = false;
                        btnSaveActExp.Enabled = false;
                        btnSaveOffHire.Enabled = false;
                        btnAddActualExpenses.Enabled = false;
                        btnAddOffHireDetails.Enabled = false;
                        btnAddVoyage.Enabled = false;
                        btnContractCloser.Visible = false;
                    }
                }
            }
        }
        catch(Exception ex)
        {

        }
    }

    protected void Show_Contract_Record(int ContractId)
    {
        DataTable dt = Revenue.AdVesselContractDetails(ContractId, 0, DateTime.Now,0,0, DateTime.Now,0,0,0, 0, 0, 0, 0, 0, "SELECT",0, 0, 0, 0, 0, 0, 0, "",0,0);
        if (dt.Rows.Count > 0)
        {
            foreach (DataRow dr in dt.Rows)
            {
                lblContractNo.Text = dr["ContractNo"].ToString();
                
                

                if (dr["VesselName"].ToString() != "")
                {
                    lblVessel.Text = dr["VesselName"].ToString();
                }
                if (dr["ContractType"].ToString() != "")
                {
                    lblContractType.Text = dr["ContractType"].ToString();
                }
                if (dr["ContractTypeId"].ToString() != "")
                {
                    ContractTypeId = Convert.ToInt32(dr["ContractTypeId"].ToString());
                }
                if (dr["ChartererName"].ToString() != "")
                {
                    lblCharterer.Text = dr["ChartererName"].ToString();
                }
                AddComPercentage = 0;
                AddComAmount = 0;
                AddComPercentage = Common.CastAsDecimal(dr["AddCOMPer"].ToString());
                AddComAmount = Common.CastAsDecimal(dr["AddCOMAmout"].ToString());
                if (ContractTypeId == 1)
                {
                    tblContractDtlsVC.Visible = false;
                    tblContractDtlsTC.Visible = true;
                    lblConStartDt.Text = DateTime.Parse(dr["FromDate"].ToString()).ToString("dd-MMM-yyyy");
                    lblConEndDt.Text = DateTime.Parse(dr["ToDate"].ToString()).ToString("dd-MMM-yyyy");
                    lblTotalHireAmountUSD.Text = Math.Round(Common.CastAsDecimal(dr["ContractAmount"].ToString()), 2).ToString("N", new CultureInfo("en-US"));
                    if (!(string.IsNullOrWhiteSpace(lblConStartDt.Text) && string.IsNullOrWhiteSpace(lblConEndDt.Text)))
                    {
                        GetToNoofDays();
                    }
                }
                else
                {
                    tblContractDtlsVC.Visible = true;
                    tblContractDtlsTC.Visible = false;
                    lblFromPortVC.Text = dr["FromPortName"].ToString();
                    lblToPortVC.Text = dr["ToPortName"].ToString();
                    lblVolume.Text = Math.Round(Common.CastAsDecimal(dr["Volume"].ToString()), 2).ToString("N", new CultureInfo("en-US"));
                    lblRate.Text = Math.Round(Common.CastAsDecimal(dr["Rate"].ToString()), 2).ToString("N", new CultureInfo("en-US"));
                    lblTotalHireAmtVC.Text = Math.Round(Common.CastAsDecimal(dr["TotalHireAmtVoyage"].ToString()), 2).ToString("N", new CultureInfo("en-US"));
                    lblExpVoyageDays.Text = dr["ExpectedVoyageDuration"].ToString();
                    lblCargoDetails.Text = dr["Cargodetails"].ToString();
                }
               
                if (dr["Status"].ToString() != "")
                {
                    lblContractStatus.Text = dr["Status"].ToString();
                }

               

                
                
            }
        }
    }
    protected void GetToNoofDays()
    {

        DateTime startDate = DateTime.Parse(lblConStartDt.Text);
        DateTime endDate = DateTime.Parse(lblConEndDt.Text);
        TimeSpan diff = endDate - startDate;
        double days = diff.TotalDays+1;
        //DateTime fromDate, toDate;
        //fromDate = Convert.ToDateTime(txtFromDate.Text);
        //toDate = Convert.ToDateTime(txtToDate.Text);

        //DateTime fromdate = DateTime.MinValue;
        //DateTime todate = DateTime.MaxValue;
        //TimeSpan noofdays = todate - fromdate;
        lblContractDuration.Text = days.ToString();

        if (!(string.IsNullOrWhiteSpace(lblTotalHireAmountUSD.Text) && string.IsNullOrWhiteSpace(lblContractDuration.Text)))
        {
            lblPerDayHireAmount.Text = Math.Round((Common.CastAsDecimal(lblTotalHireAmountUSD.Text) / Common.CastAsDecimal(lblContractDuration.Text)), 2).ToString("N", new CultureInfo("en-US"));
        }
    }

    protected void BindExpectedExpenses(int contractId, int ContractTypeId)
    {
        DataTable dtExpExpenses = Revenue.ContractExpectedExpenses(contractId, ContractTypeId);
        if (dtExpExpenses.Rows.Count > 0)
        {
            rptExpExpenses.Visible = true;
            rptExpExpenses.DataSource = dtExpExpenses;
            rptExpExpenses.DataBind();

            lblAddComPer.Text = AddComPercentage.ToString();
            txtAddComAmt.Text = AddComAmount.ToString();

        }
        else
        {
            rptExpExpenses.DataSource = null;
            rptExpExpenses.DataBind();
        }
        getTotalExpectedExpenses();
    }
    protected void getTotalExpectedExpenses()
    {
        divTotalExpExpense.Visible = true;
       
        txtTotalExpExpenses.Text = "0.00";
        txtTotalRevenue.Text = "0.00";
       

        Decimal totalExpExpenses = 0;


        foreach (RepeaterItem item in rptExpExpenses.Items)
        {
            TextBox txtRVCEEAmount = item.FindControl("txtRVCEEAmount") as TextBox;
            if (!string.IsNullOrWhiteSpace(txtRVCEEAmount.Text))
            {
                if (totalExpExpenses == 0)
                {
                    totalExpExpenses = Decimal.Parse(txtRVCEEAmount.Text);
                }
                else
                {
                    totalExpExpenses = totalExpExpenses + Decimal.Parse(txtRVCEEAmount.Text);
                }
            }
            
        }
        if (!string.IsNullOrWhiteSpace(txtAddComAmt.Text))
        {
            totalExpExpenses = totalExpExpenses + Common.CastAsDecimal(txtAddComAmt.Text);
        }
        txtTotalExpExpenses.Text = Math.Round(Common.CastAsDecimal(totalExpExpenses), 2).ToString("N", new CultureInfo("en-US"));
        if (ContractTypeId == 2)
        {
            txtContractAmount.Text = lblTotalHireAmtVC.Text;
            txtTotalRevenue.Text = Math.Round((Common.CastAsDecimal(lblTotalHireAmtVC.Text) - Common.CastAsDecimal(txtTotalExpExpenses.Text)), 2).ToString("N", new CultureInfo("en-US"));
        }
        else
        {
            txtContractAmount.Text = lblTotalHireAmountUSD.Text;
            txtTotalRevenue.Text = Math.Round((Common.CastAsDecimal(lblTotalHireAmountUSD.Text) - Common.CastAsDecimal(txtTotalExpExpenses.Text)), 2).ToString("N", new CultureInfo("en-US"));
        }
       
       

       
    }
    //public string FormatCurr(object _in)
    //{
    //    return string.Format("{0:0.00}", _in);
    //}

    protected void btnAddVoyage_Click(object sender, EventArgs e)
    {
        dvVoyage.Visible = true;
        txtVoyageNo.Text = "";
        lblPopError.Text = "";
        hdnVoyageId.Value = "";
        
        clearVoyageDetails();
        if (ContractTypeId == 2)
        {
            trVoyageCargo.Visible = true;
        }
        else
        {
            trVoyageCargo.Visible = false;
        }
    }

    


    protected void btnClose_OnClick(object sender, EventArgs e)
    {
        divViewVoyage.Visible = false;
    }
    protected void imgClosebtn2_OnClick(object sender, EventArgs e)
    {
        dvVoyage.Visible = false;
        clearVoyageDetails();
        BindVoyageDetails(ContractId);
    }

    protected void btnCancelVoyage_Click(object sender, EventArgs e)
    {
        dvVoyage.Visible = false;
        clearVoyageDetails();
        BindVoyageDetails(ContractId);
    }

    protected void clearVoyageDetails()
    {
        txtVoyageNo.Text = "";
        txtfromport.Text = "";
        txttoport.Text = "";
        txtFromDate.Text = "";
        txtToDate.Text = "";
        txtVoyageDurationDays.Text = "";
        txtCargoQty.Text = "";
        ddlFromMin.SelectedIndex = 0;
        ddlFromHrs.SelectedIndex = 0;
        ddlToHrs.SelectedIndex = 0;
        ddlToMins.SelectedIndex = 0;
    }

    protected void btnSaveVoyage_Click(object sender, EventArgs e)
    {
        try
        {
            int VoyageId = 0;
            int fromport = 0;
            int toport = 0;
            if (hdnVoyageId.Value != "")
            {
                VoyageId = Convert.ToInt32(hdnVoyageId.Value);
            }

            if (string.IsNullOrWhiteSpace(txtfromport.Text))
            {
                lblPopError.Text = "Please Enter From Port.";
                txtfromport.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txttoport.Text))
            {
                lblPopError.Text = "Please Enter To Port.";
                txttoport.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtFromDate.Text))
            {
                lblPopError.Text = "Please Enter From Date.";
                txtFromDate.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtToDate.Text))
            {
                lblPopError.Text = "Please Enter To Date.";
                txtToDate.Focus();
                return;
            }
            if (ddlFromHrs.SelectedIndex == 0)
            {
                lblPopError.Text = "Please Select From Hrs.";
                ddlFromHrs.Focus();
                return;
            }
            if (ddlFromMin.SelectedIndex == 0)
            {
                lblPopError.Text = "Please Select From Min.";
                ddlFromMin.Focus();
                return;
            }
            if (ddlToHrs.SelectedIndex == 0)
            {
                lblPopError.Text = "Please Select To Hrs.";
                ddlToHrs.Focus();
                return;
            }
            if (ddlToMins.SelectedIndex == 0)
            {
                lblPopError.Text = "Please Select To Min.";
                ddlToMins.Focus();
                return;
            }
            if (txtfromport.Text.Trim() != "")
            {
                DataTable dt1 = Inspection_Planning.CheckPort(txtfromport.Text);
                if (dt1.Rows[0][0].ToString() == "0")
                {
                    lblPopError.Text = "Please Enter Correct From Port Name.";
                    return;
                }
                else
                {
                    fromport = int.Parse(dt1.Rows[0][0].ToString());
                }
            }

            if (txttoport.Text.Trim() != "")
            {
                DataTable dt2 = Inspection_Planning.CheckPort(txttoport.Text);
                if (dt2.Rows[0][0].ToString() == "0")
                {
                    lblPopError.Text = "Please Enter Correct To Port Name.";
                    return;
                }
                else
                {
                    toport = int.Parse(dt2.Rows[0][0].ToString());
                }
            }

            DataTable dt;

            dt = Revenue.AddContractVoyageDetails(VoyageId, ContractId,txtVoyageNo.Text.Trim(), Login_Id, fromport,toport, DateTime.Parse(txtFromDate.Text), Convert.ToInt32(ddlFromHrs.SelectedValue), Convert.ToInt32(ddlFromMin.SelectedValue), DateTime.Parse(txtToDate.Text), Convert.ToInt32(ddlToHrs.SelectedValue), Convert.ToInt32(ddlToMins.SelectedValue), txtCargoQty.Text.Trim() != "" ? Common.CastAsDecimal(txtCargoQty.Text) : 0);

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0][0].ToString() == "YES")
                {
                    lblPopError.Text = "Voyage is already available.";
                    return;
                }
                else
                {
                    
                    hdnVoyageId.Value = dt.Rows[0][0].ToString();
                    VoyageId = Convert.ToInt32(hdnVoyageId.Value);
                    lblPopError.Text = "Voyage Created successfully";
                    
                }
                BindVoyageDetails(ContractId);
                GetTotalOffhireandActualExpenses();
            }
             
        }
        catch (Exception ex)
        {
            lblPopError.Text = ex.Message.ToString();
        }
    }

    protected void BindVoyageDetails(int ContractId)
    {
        string sql = "Select RCVD_ID,RCVD_VoyageNo,REPLACE(CONVERT(VARCHAR,RCVD_FromDt,106),' ','-') AS RCVD_FromDt,REPLACE(CONVERT(VARCHAR,RCVD_ToDt,106),' ','-') AS RCVD_ToDt,   (SELECT PortName FROM dbo.Port WHERE dbo.Port.PortId=RV_ContractVoyageDetails.RCVD_FromPort) AS FromPort,  (SELECT PortName FROM dbo.Port WHERE dbo.Port.PortId=RV_ContractVoyageDetails.RCVD_ToPort) AS ToPort, DATEDIFF(Day,  RCVD_FromDt , RCVD_ToDt)+1 As VoyageDuration,(Select convert(varchar,cast(ISNULL(SUM(OffHireAmount),0) as money),1)  from RV_VesselContractOffHireDetails vcof where vcof.ContractId = RCVD_ContractId and vcof.RCVD_Id = RV_ContractVoyageDetails.RCVD_ID and IsDeleted = 0) As TotalOffHireAmt,(Select convert(varchar,cast(ISNULL(SUM(Amount),0) as money),1)  from RV_VesselContractExpensesDetails vced where vced.ContractId = RCVD_ContractId and vced.RCVD_Id = RV_ContractVoyageDetails.RCVD_ID and IsDeleted = 0) As TotalActExpDetails  from RV_ContractVoyageDetails with(nolock) where RCVD_IsActive = 0 and RCVD_ContractId = " + ContractId;
        DataTable Dt = Common.Execute_Procedures_Select_ByQuery(sql);
        if (Dt.Rows.Count > 0)
        {
            rptVoyage.DataSource = Dt;
            rptVoyage.DataBind();
            //btnAddOffHireDetails.Visible = true;
            //btnAddActualExpenses.Visible = true;
        }
        else
        {
            rptVoyage.DataSource = null;
            rptVoyage.DataBind();
            //btnAddOffHireDetails.Visible = false;
            //btnAddActualExpenses.Visible = false;
        }
    }

    protected void GetTotalOffhireandActualExpenses()
    {
        txtTotalOffhireAmount.Text = "0.00";
        txtTotalActualExpectedAmount.Text = "0.00";

        Decimal totalActAmt = 0;
        Decimal totalOffhireAmt = 0;

        foreach (RepeaterItem item in rptVoyage.Items)
        {
            
            Label lblTotalOffHireAmt = item.FindControl("lblTotalOffHireAmt") as Label;
            Label lblTotalActExpDetails = item.FindControl("lblTotalActExpDetails") as Label;
            if (!string.IsNullOrWhiteSpace(lblTotalOffHireAmt.Text))
            {
                if (totalOffhireAmt == 0)
                {
                    totalOffhireAmt = Decimal.Parse(lblTotalOffHireAmt.Text);
                }
                else
                {
                    totalOffhireAmt = totalOffhireAmt + Decimal.Parse(lblTotalOffHireAmt.Text);
                }
            }
            if (!string.IsNullOrWhiteSpace(lblTotalActExpDetails.Text))
            {
                if (totalActAmt == 0)
                {
                    totalActAmt = Decimal.Parse(lblTotalActExpDetails.Text);
                }
                else
                {
                    totalActAmt = totalActAmt + Decimal.Parse(lblTotalActExpDetails.Text);
                }
            }

        }
        txtTotalActualExpectedAmount.Text = Math.Round(Common.CastAsDecimal(totalActAmt), 2).ToString("N", new CultureInfo("en-US"));

        txtTotalOffhireAmount.Text = Math.Round(Common.CastAsDecimal(totalOffhireAmt), 2).ToString("N", new CultureInfo("en-US"));

        

        txtTotalActualRevenue.Text = Math.Round(Common.CastAsDecimal(txtContractAmount.Text) - totalActAmt - totalOffhireAmt,2).ToString("N", new CultureInfo("en-US"));
    }

    protected void txtRVCEEAmount_TextChanged(object sender, EventArgs e)
    {
        try
        {
            getTotalExpectedExpenses();
        }
        catch (Exception ex)
        {  }

    }

    protected void imgEdit_Click(object sender, ImageClickEventArgs e)
    {
        lblPopError.Text = "";
        int editVoyageId = 0;
        editVoyageId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        if (editVoyageId > 0)
        {
            dvVoyage.Visible = true;
            hdnVoyageId.Value = editVoyageId.ToString();
            string sql = "EXEC SP_RV_GETVOYAGEDETAILSforContract " + ContractId + "," + editVoyageId;
            DataTable Dt = Common.Execute_Procedures_Select_ByQuery(sql);
            if (Dt.Rows.Count > 0)
            {
                txtVoyageNo.Text = Dt.Rows[0]["RCVD_VoyageNo"].ToString();
                if (!string.IsNullOrWhiteSpace(Dt.Rows[0]["RCVD_FromDt"].ToString()))
                {
                    txtFromDate.Text = DateTime.Parse(Dt.Rows[0]["RCVD_FromDt"].ToString()).ToString("dd-MMM-yyyy");
                }
                if (!string.IsNullOrWhiteSpace(Dt.Rows[0]["RCVD_FromHrs"].ToString()))
                {
                    ddlFromHrs.SelectedValue = Dt.Rows[0]["RCVD_FromHrs"].ToString().PadLeft(2, '0');
                }
                if (!string.IsNullOrWhiteSpace(Dt.Rows[0]["RCVD_FromMins"].ToString()))
                {
                    ddlFromMin.SelectedValue = Dt.Rows[0]["RCVD_FromMins"].ToString().PadLeft(2, '0');
                }
                if (!string.IsNullOrWhiteSpace(Dt.Rows[0]["RCVD_ToDt"].ToString()))
                {
                    txtToDate.Text = DateTime.Parse(Dt.Rows[0]["RCVD_ToDt"].ToString()).ToString("dd-MMM-yyyy");
                }
                if (!string.IsNullOrWhiteSpace(Dt.Rows[0]["RCVD_ToHrs"].ToString()))
                {
                    ddlToHrs.SelectedValue = Dt.Rows[0]["RCVD_ToHrs"].ToString().PadLeft(2, '0');
                }
                if (!string.IsNullOrWhiteSpace(Dt.Rows[0]["RCVD_ToMins"].ToString()))
                {
                    ddlToMins.SelectedValue = Dt.Rows[0]["RCVD_ToMins"].ToString().PadLeft(2, '0');
                }
                if (!string.IsNullOrWhiteSpace(Dt.Rows[0]["FromPort"].ToString()))
                {
                    txtfromport.Text = Dt.Rows[0]["FromPort"].ToString();

                }
                if (!string.IsNullOrWhiteSpace(Dt.Rows[0]["ToPort"].ToString()))
                {
                    txttoport.Text = Dt.Rows[0]["ToPort"].ToString();

                }
                if (ContractTypeId == 2)
                {
                    trVoyageCargo.Visible = true;
                    if (!string.IsNullOrWhiteSpace(Dt.Rows[0]["RCVD_CargoQty"].ToString()))
                    {
                        txtCargoQty.Text = Dt.Rows[0]["RCVD_CargoQty"].ToString();
                
                    }
                    else
                    {
                        txtCargoQty.Text = "";
                    }
                }
                else
                {
                    trVoyageCargo.Visible = false;
                    txtCargoQty.Text = "";
                }
                GetVoyageDurationDays();
            }
        }
    }

    protected void ImgVoyView_Click(object sender, ImageClickEventArgs e)
    {
       try
        {

            int viewVoyageId = 0;
            viewVoyageId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
            if (viewVoyageId > 0)
            {
                divViewVoyage.Visible = true;
                hdnVoyageId.Value = viewVoyageId.ToString();
                string sql = "Select RCVD_ID,RCVD_VoyageNo,REPLACE(CONVERT(VARCHAR,RCVD_FromDt,106),' ','-') AS RCVD_FromDt,REPLACE(CONVERT(VARCHAR,RCVD_ToDt,106),' ','-') AS RCVD_ToDt,   (SELECT PortName FROM dbo.Port WHERE dbo.Port.PortId=RV_ContractVoyageDetails.RCVD_FromPort) AS FromPort,  (SELECT PortName FROM dbo.Port WHERE dbo.Port.PortId=RV_ContractVoyageDetails.RCVD_ToPort) AS ToPort,DATEDIFF(Day,  RCVD_FromDt , RCVD_ToDt)+1 As VoyageDuration     from RV_ContractVoyageDetails with(nolock) where RCVD_IsActive = 0 and RCVD_ContractId = " + ContractId + " And RCVD_ID = " + viewVoyageId;
                DataTable Dt = Common.Execute_Procedures_Select_ByQuery(sql);
                if (Dt.Rows.Count > 0)
                {
                    lblVoyageNo.Text = Dt.Rows[0]["RCVD_VoyageNo"].ToString();
                    if (!string.IsNullOrWhiteSpace(Dt.Rows[0]["RCVD_FromDt"].ToString()))
                    {
                        lblVoyFromDt.Text = DateTime.Parse(Dt.Rows[0]["RCVD_FromDt"].ToString()).ToString("dd-MMM-yyyy");
                    }
                    if (!string.IsNullOrWhiteSpace(Dt.Rows[0]["RCVD_ToDt"].ToString()))
                    {
                        lblVoyToDt.Text = DateTime.Parse(Dt.Rows[0]["RCVD_ToDt"].ToString()).ToString("dd-MMM-yyyy");
                    }
                    if (!string.IsNullOrWhiteSpace(Dt.Rows[0]["FromPort"].ToString()))
                    {
                        lblFromPort.Text = Dt.Rows[0]["FromPort"].ToString();

                    }
                    if (!string.IsNullOrWhiteSpace(Dt.Rows[0]["ToPort"].ToString()))
                    {
                        lblToPort.Text = Dt.Rows[0]["ToPort"].ToString();

                    }
                    if (!string.IsNullOrWhiteSpace(Dt.Rows[0]["VoyageDuration"].ToString()))
                    {
                        lblVoyDuration.Text = Dt.Rows[0]["VoyageDuration"].ToString();
                    }
                    BindActExpDetails(ContractId, Convert.ToInt32(Dt.Rows[0]["RCVD_ID"].ToString()), 1);
                    if (ContractTypeId == 2)
                    {
                        btnAddOffHireDetails.Visible = false;
                        divOffHireDetails.Visible = false;
                        divOffHireheader.Visible = false;
                        rptOffhireDtls.DataSource = null;
                        rptOffhireDtls.DataBind();
                    }
                    else
                    {
                        btnAddOffHireDetails.Visible = true;
                        divOffHireDetails.Visible = true;
                        divOffHireheader.Visible = true;
                        BindOffhireDetails(ContractId, Convert.ToInt32(Dt.Rows[0]["RCVD_ID"].ToString()), 1);
                    }
                    
                }
            }
        }
        catch(Exception ex)
        {
            
        }
       
    }

    protected void imgDelete_Click(object sender, ImageClickEventArgs e)
    {
        int deleteVoyageId = 0;
        deleteVoyageId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        if (deleteVoyageId > 0)
        {
            Common.Execute_Procedures_Select_ByQuery("Update  RV_ContractVoyageDetails SET RCVD_IsActive =1 , RCVD_ModifiedBy = " +Login_Id+ ", RCVD_ModifiedOn = GETDATE() WHERE RCVD_ID=" + deleteVoyageId + " and RCVD_ContractId =" + ContractId);

            BindVoyageDetails(ContractId);
            GetTotalOffhireandActualExpenses();
        }
    }

    protected void txtFromDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (!(string.IsNullOrWhiteSpace(txtFromDate.Text) && string.IsNullOrWhiteSpace(txtToDate.Text)))
            {
                GetVoyageDurationDays();
            }

        }
        catch (Exception ex)
        {

        }
    }


    protected void txtToDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (!(string.IsNullOrWhiteSpace(txtFromDate.Text) && string.IsNullOrWhiteSpace(txtToDate.Text)))
            {
                GetVoyageDurationDays();
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void GetVoyageDurationDays()
    {
        DateTime startDate = DateTime.Parse(txtFromDate.Text);
        DateTime endDate = DateTime.Parse(txtToDate.Text);
        TimeSpan diff = endDate - startDate;
        double days = diff.TotalDays + 1; 
        //DateTime fromDate, toDate;
        //fromDate = Convert.ToDateTime(txtFromDate.Text);
        //toDate = Convert.ToDateTime(txtToDate.Text);

        //DateTime fromdate = DateTime.MinValue;
        //DateTime todate = DateTime.MaxValue;
        //TimeSpan noofdays = todate - fromdate;
        txtVoyageDurationDays.Text = days.ToString();     
    }

    #region 'Actual Expenses Dtls'
    protected void btnCancelActExp_Click(object sender, EventArgs e)
    {
        try
        {
            ClearActualExpensesDetails();
            BindActExpDetails(ContractId, Convert.ToInt32(hdnVoyageId.Value), 1);
        }
        catch (Exception ex)
        {
            lblActExpMsg.Text = ex.Message.ToString();
        }
    }
    protected void btnSaveActExp_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlActExpType.SelectedIndex == 0)
            {
                lblActExpMsg.Text = "Please select Actual Expense Type.";
                ddlActExpType.Focus();
                return;
            }
            //if (ddlActExpVoyages.SelectedIndex == 0)
            //{
            //    lblActExpMsg.Text = "Please select Voyage.";
            //    return;
            //}
            if (string.IsNullOrWhiteSpace(txtActExpAmount.Text) )
            {
                lblActExpMsg.Text = "Please enter Actual Expense Amount.";
                txtActExpAmount.Focus();
                return;
            }

            int intCreatedBy = 0;
            int ExpensesId = 0;
            intCreatedBy = Login_Id;
            if (hdnActualExpId.Value != "")
            {
                ExpensesId = Convert.ToInt32(hdnActualExpId.Value);
            }
            DataTable dtActExpDtls;
            if (ExpensesId == 0)
            {
                dtActExpDtls = Revenue.InsertUpdateActExpensesDtls(0, ContractId,  Convert.ToInt32(hdnVoyageId.Value), Convert.ToInt32(ddlActExpType.SelectedValue),  Math.Round(Common.CastAsDecimal(txtActExpAmount.Text), 2), txtActExpRemark.Text, intCreatedBy);

                if (dtActExpDtls.Rows.Count > 0)
                {

                    ExpensesId = Convert.ToInt32(dtActExpDtls.Rows[0][0].ToString());
                    hdnActualExpId.Value = Convert.ToString(ExpensesId);
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "dialog", "alert('Actual Voyage Expenses Saved Sucessfully.');", true);

                   
                }
            }
            else
            {
                dtActExpDtls = Revenue.InsertUpdateActExpensesDtls(ExpensesId, ContractId, Convert.ToInt32(hdnVoyageId.Value), Convert.ToInt32(ddlActExpType.SelectedValue), Math.Round(Common.CastAsDecimal(txtActExpAmount.Text), 2), txtActExpRemark.Text, intCreatedBy);
                if (dtActExpDtls.Rows.Count > 0)
                {

                    ExpensesId = Convert.ToInt32(dtActExpDtls.Rows[0][0].ToString());
                    hdnActualExpId.Value = Convert.ToString(ExpensesId);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "dialog", "alert('Actual Voyage Expenses updated Sucessfully.');", true);
                }
            }

            BindActExpDetails(ContractId,Convert.ToInt32(hdnVoyageId.Value),1);
        }
        catch(Exception ex)
        {
            lblActExpMsg.Text = ex.Message.ToString();
        }

    }
    protected void ibCloseActExpId_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ClearActualExpensesDetails();
            BindActExpDetails(ContractId, Convert.ToInt32(hdnVoyageId.Value), 1);
        }
        catch (Exception ex)
        {
            lblActExpMsg.Text = ex.Message.ToString();
        }
    }
    protected void ClearActualExpensesDetails()
    {
        divContractActualExpenses.Visible = false;
        hdnActualExpId.Value = "";
        lblActExpMsg.Text = "";
        //lblActExpContractId.Text = "";
        lblActExpVoyages.Text = "";
        ddlActExpType.SelectedIndex = 0;
        txtActExpRemark.Text = "";
        txtActExpAmount.Text = "";
    }
    protected void BindActExpDetails(int ContractId, int VoyageId, int flag)
    {
        try
        {
            DataTable dtActExpDtls = Revenue.GetActExpensesDtls(0, ContractId, VoyageId, 1);
            if (dtActExpDtls.Rows.Count > 0)
            {
                rptActExpDtls.DataSource = dtActExpDtls;
                rptActExpDtls.DataBind();
            }
            else
            {
                rptActExpDtls.DataSource = dtActExpDtls;
                rptActExpDtls.DataBind();
            }
        }
        catch(Exception ex)
        {
            
        }
    }
    protected void imgEditActExp_Click(object sender, ImageClickEventArgs e)
    {
        lblActExpMsg.Text = "";
        int editExpensesId = 0;
        editExpensesId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        if (editExpensesId > 0)
        {
            //dvVoyage.Visible = true;
            divContractActualExpenses.Visible = true;
            BindddlActExpType();
            hdnActualExpId.Value = editExpensesId.ToString();
            DataTable Dt = Revenue.GetActExpensesDtls(editExpensesId, ContractId, 0, 0);
            if (Dt.Rows.Count > 0)
            {
               // lblActExpContractId.Text = lblContractNo.Text;

                if (!string.IsNullOrWhiteSpace(Dt.Rows[0]["RCVD_VoyageNo"].ToString()))
                {
                    lblActExpVoyages.Text = Dt.Rows[0]["RCVD_VoyageNo"].ToString();
                }
                if (!string.IsNullOrWhiteSpace(Dt.Rows[0]["CategoryId"].ToString()))
                {
                    ddlActExpType.SelectedValue = Dt.Rows[0]["CategoryId"].ToString();
                }
                if (!string.IsNullOrWhiteSpace(Dt.Rows[0]["Amount"].ToString()))
                {
                    txtActExpAmount.Text =Math.Round(Convert.ToDecimal(Dt.Rows[0]["Amount"].ToString()),2).ToString("N", new CultureInfo("en-US"));

                }
                if (!string.IsNullOrWhiteSpace(Dt.Rows[0]["Remarks"].ToString()))
                {
                    txtActExpRemark.Text = Dt.Rows[0]["Remarks"].ToString();

                }


            }
        }
    }
    protected void imgDeleteActExp_Click(object sender, ImageClickEventArgs e)
    {
        int deleteExpensesId = 0;
        deleteExpensesId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        if (deleteExpensesId > 0)
        {
            Common.Execute_Procedures_Select_ByQuery("Update  [RV_VesselContractExpensesDetails] SET IsDeleted =1 , ModifiedBy = " + Login_Id + ", ModifiedOn = GETDATE() WHERE ExpensesId=" + deleteExpensesId + " and ContractId =" + ContractId);

            BindActExpDetails(ContractId,Convert.ToInt32(hdnVoyageId.Value),1);
        }
    }
    protected void BindddlActExpType()
    {
        string sql = "Select '0' As CategoryId,'Select' As CategoryName UNION Select CategoryId, CategoryName from RV_OffHireAndExpensesMaster with(nolock) where IsActive = 0 and IsExpenses = 1";
        DataTable Dt = Common.Execute_Procedures_Select_ByQuery(sql);
        if (Dt.Rows.Count > 0)
        {
            ddlActExpType.Controls.Clear();
            this.ddlActExpType.DataTextField = "CategoryName";
            this.ddlActExpType.DataValueField = "CategoryId";
            this.ddlActExpType.DataSource = Dt;
            this.ddlActExpType.DataBind();
            
        }

    }
    protected void btnAddActualExpenses_Click(object sender, EventArgs e)
    {
        divContractActualExpenses.Visible = true;
        BindddlActExpType();
        hdnActualExpId.Value = "";
        lblActExpVoyages.Text = lblVoyageNo.Text;
    }
    #endregion

    #region 'Off-hire details'
    protected void ibOffHire_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
         ClearOffHireDetails(); 
            BindOffhireDetails(ContractId, Convert.ToInt32(hdnVoyageId.Value), 1);

        }
        catch (Exception ex)
        {
         lblOffhiremsg.Text = ex.Message.ToString();
        }
    }
    protected void ClearOffHireDetails()
    {
        divContractOffHire.Visible = false;
        hdnOffHireId.Value = "";
        lblOffhiremsg.Text = "";
        //lblActExpContractId.Text = "";
        lblOffHireVoyageNo.Text = "";
        txtOffHireAmount.Text = "";
        txtOffhireDuration.Text = "";
        txtOffHireFromDt.Text = "";
        txtOffHireToDt.Text = "";
        txtOffhireLocation.Text = "";
        txtOffHireReason.Text = "";
        txtOffHireRemarks.Text = "";
        ddlOffHireCategory.SelectedIndex = 0;
        ddlOffhireToMins.SelectedIndex = 0;
        ddlOffhireToHrs.SelectedIndex = 0;
        ddlOffHireFromHrs.SelectedIndex = 0;
        ddlOffHireFromMins.SelectedIndex = 0;
    }
    protected void btnSaveOffHire_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlOffHireCategory.SelectedIndex == 0)
            {
                lblOffhiremsg.Text = "Please select Off-hire Type.";
                ddlOffHireCategory.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtOffhireLocation.Text))
            {
                lblOffhiremsg.Text = "Please eneter location.";
                txtOffhireLocation.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtOffHireFromDt.Text))
            {
                lblOffhiremsg.Text = "Please eneter From Date.";
                txtOffHireFromDt.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtOffHireToDt.Text))
            {
                lblOffhiremsg.Text = "Please eneter To Date.";
                txtOffHireToDt.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtOffHireReason.Text))
            {
                lblOffhiremsg.Text = "Please enter Off-hire reason.";
                txtOffHireReason.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtOffHireAmount.Text))
            {
                lblOffhiremsg.Text = "Please enter Off-hire Amount.";
                txtOffHireAmount.Focus();
                return;
            }

            int intCreatedBy = 0;
            int OffHireId = 0;
            intCreatedBy = Login_Id;
            if (hdnOffHireId.Value != "")
            {
                OffHireId = Convert.ToInt32(hdnOffHireId.Value);
            }
            DataTable dtOffhireDtls;
            if (OffHireId == 0)
            {
                dtOffhireDtls = Revenue.InsertUpdateOffHireDtls(0, ContractId, Convert.ToInt32(hdnVoyageId.Value), Convert.ToInt32(ddlOffHireCategory.SelectedValue),txtOffhireLocation.Text, DateTime.Parse(txtOffHireFromDt.Text), Convert.ToInt32(ddlOffHireFromHrs.SelectedValue), Convert.ToInt32(ddlOffHireFromMins.SelectedValue), DateTime.Parse(txtOffHireToDt.Text), Convert.ToInt32(ddlOffhireToHrs.SelectedValue), Convert.ToInt32(ddlOffhireToMins.SelectedValue) , txtOffHireReason.Text.Trim(),txtOffHireRemarks.Text.Trim(), Math.Round(Common.CastAsDecimal(txtOffHireAmount.Text), 2), Math.Round(Common.CastAsDecimal(txtOffhireAmountperday.Text), 2),Convert.ToInt32(txtOffhireDuration.Text), intCreatedBy);

                if (dtOffhireDtls.Rows.Count > 0)
                {
                    OffHireId = Convert.ToInt32(dtOffhireDtls.Rows[0][0].ToString());
                    hdnOffHireId.Value = Convert.ToString(OffHireId);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "dialog", "alert('Off-hire details Saved Sucessfully.');", true);
                }
            }
            else
            {
                dtOffhireDtls = Revenue.InsertUpdateOffHireDtls(OffHireId, ContractId, Convert.ToInt32(hdnVoyageId.Value), Convert.ToInt32(ddlOffHireCategory.SelectedValue), txtOffhireLocation.Text, DateTime.Parse(txtOffHireFromDt.Text), Convert.ToInt32(ddlOffHireFromHrs.SelectedValue), Convert.ToInt32(ddlOffHireFromMins.SelectedValue), DateTime.Parse(txtOffHireToDt.Text), Convert.ToInt32(ddlOffhireToHrs.SelectedValue), Convert.ToInt32(ddlOffhireToMins.SelectedValue), txtOffHireReason.Text.Trim(), txtOffHireRemarks.Text.Trim(), Math.Round(Common.CastAsDecimal(txtOffHireAmount.Text), 2), Math.Round(Common.CastAsDecimal(txtOffhireAmountperday.Text), 2), Convert.ToInt32(txtOffhireDuration.Text), intCreatedBy);
                if (dtOffhireDtls.Rows.Count > 0)
                {

                    OffHireId = Convert.ToInt32(dtOffhireDtls.Rows[0][0].ToString());
                    hdnOffHireId.Value = Convert.ToString(OffHireId);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "dialog", "alert('Off-hire Details updated Sucessfully.');", true);
                }
            }

            BindOffhireDetails(ContractId, Convert.ToInt32(hdnVoyageId.Value), 1);
        }
        catch (Exception ex)
        {
            lblActExpMsg.Text = ex.Message.ToString();
        }
    }
    protected void btnCancelOffhire_Click(object sender, EventArgs e)
    {
        try
        {
            ClearOffHireDetails();
            BindOffhireDetails(ContractId, Convert.ToInt32(hdnVoyageId.Value), 1);
        }
        catch (Exception ex)
        {
            lblOffhiremsg.Text = ex.Message.ToString();
        }
    }
    protected void btnAddOffHireDetails_Click(object sender, EventArgs e)
    {
        try
        {
            BindddlOffhireType();           
            ClearOffHireDetails();
            divContractOffHire.Visible = true;
            lblOffHireVoyageNo.Text = lblVoyageNo.Text;
            txtOffhireAmountperday.Text = lblPerDayHireAmount.Text;
        }
        catch (Exception ex)
        {
            lblOffhiremsg.Text = ex.Message.ToString();
        }
    }
    protected void BindOffhireDetails(int ContractId, int VoyageId, int flag)
    {
        try
        {
            DataTable dtOffhireDtls = Revenue.GetOffhireDtls(0, ContractId, VoyageId, 1);
            if (dtOffhireDtls.Rows.Count > 0)
            {
                rptOffhireDtls.DataSource = dtOffhireDtls;
                rptOffhireDtls.DataBind();
            }
            else
            {
                rptOffhireDtls.DataSource = dtOffhireDtls;
                rptOffhireDtls.DataBind();
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void imgEditOffhire_Click(object sender, ImageClickEventArgs e)
    {
        lblOffhiremsg.Text = "";
        int editOffhireId = 0;
        editOffhireId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        if (editOffhireId > 0)
        {
            //dvVoyage.Visible = true;
            divContractOffHire.Visible = true;
            BindddlOffhireType();
            hdnOffHireId.Value = editOffhireId.ToString();
            DataTable Dt = Revenue.GetOffhireDtls(editOffhireId, ContractId, 0, 0);
            if (Dt.Rows.Count > 0)
            {
                // lblActExpContractId.Text = lblContractNo.Text;

                if (!string.IsNullOrWhiteSpace(Dt.Rows[0]["RCVD_VoyageNo"].ToString()))
                {
                    lblOffHireVoyageNo.Text = Dt.Rows[0]["RCVD_VoyageNo"].ToString();
                }
                if (!string.IsNullOrWhiteSpace(Dt.Rows[0]["CategoryId"].ToString()))
                {
                    ddlOffHireCategory.SelectedValue = Dt.Rows[0]["CategoryId"].ToString();
                }
                if (!string.IsNullOrWhiteSpace(Dt.Rows[0]["Amount"].ToString()))
                {
                    txtOffHireAmount.Text = Math.Round(Convert.ToDecimal(Dt.Rows[0]["Amount"].ToString()), 2).ToString("N", new CultureInfo("en-US"));

                }
                if (!string.IsNullOrWhiteSpace(Dt.Rows[0]["Remark"].ToString()))
                {
                    txtOffHireRemarks.Text = Dt.Rows[0]["Remark"].ToString();

                }
                if (!string.IsNullOrWhiteSpace(Dt.Rows[0]["Reason"].ToString()))
                {
                    txtOffHireReason.Text = Dt.Rows[0]["Reason"].ToString();

                }
                if (!string.IsNullOrWhiteSpace(Dt.Rows[0]["OffHireAmountPerDay"].ToString()))
                {
                    txtOffhireAmountperday.Text = Math.Round(Convert.ToDecimal(Dt.Rows[0]["OffHireAmountPerDay"].ToString()), 2).ToString("N", new CultureInfo("en-US"));

                }
                if (!string.IsNullOrWhiteSpace(Dt.Rows[0]["TotalOffHireDays"].ToString()))
                {
                    txtOffhireDuration.Text = Dt.Rows[0]["TotalOffHireDays"].ToString();

                }
                if (!string.IsNullOrWhiteSpace(Dt.Rows[0]["FromDate"].ToString()))
                {
                    txtOffHireFromDt.Text = DateTime.Parse(Dt.Rows[0]["FromDate"].ToString()).ToString("dd-MMM-yyyy");
                }

                if (!string.IsNullOrWhiteSpace(Dt.Rows[0]["FromHrs"].ToString()))
                {
                    ddlOffHireFromHrs.SelectedValue = Dt.Rows[0]["FromHrs"].ToString().PadLeft(2, '0');
                }
                if (!string.IsNullOrWhiteSpace(Dt.Rows[0]["FromMins"].ToString()))
                {
                    ddlOffHireFromMins.SelectedValue = Dt.Rows[0]["FromMins"].ToString().PadLeft(2, '0');
                }
                if (!string.IsNullOrWhiteSpace(Dt.Rows[0]["ToMins"].ToString()))
                {
                    ddlOffhireToMins.SelectedValue = Dt.Rows[0]["ToMins"].ToString().PadLeft(2, '0');
                }
                if (!string.IsNullOrWhiteSpace(Dt.Rows[0]["ToHrs"].ToString()))
                {
                    ddlOffhireToHrs.SelectedValue = Dt.Rows[0]["ToHrs"].ToString().PadLeft(2, '0');
                }

                if (!string.IsNullOrWhiteSpace(Dt.Rows[0]["ToDate"].ToString()))
                {
                    txtOffHireToDt.Text = DateTime.Parse(Dt.Rows[0]["ToDate"].ToString()).ToString("dd-MMM-yyyy");
                }

                if (! string.IsNullOrWhiteSpace(Dt.Rows[0]["Location"].ToString()))
                {
                    txtOffhireLocation.Text = Dt.Rows[0]["Location"].ToString();
                }
            }
        }
    }
    protected void imgDeleteOffhire_Click(object sender, ImageClickEventArgs e)
    {
        int deleteOffhireId = 0;
        deleteOffhireId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        if (deleteOffhireId > 0)
        {
            Common.Execute_Procedures_Select_ByQuery("Update  [RV_VesselContractOffHireDetails] SET IsDeleted =1 , ModifiedBy = " + Login_Id + ", ModifiedOn = GETDATE() WHERE OffHireId=" + deleteOffhireId + " and ContractId =" + ContractId);

            BindOffhireDetails(ContractId, Convert.ToInt32(hdnVoyageId.Value), 1);
        }
    }
    protected void BindddlOffhireType()
    {
        string sql = "Select '0' As CategoryId,'Select' As CategoryName UNION Select CategoryId, CategoryName from RV_OffHireAndExpensesMaster with(nolock) where IsActive = 0 and IsOffHire = 1";
        DataTable Dt = Common.Execute_Procedures_Select_ByQuery(sql);
        if (Dt.Rows.Count > 0)
        {
            ddlOffHireCategory.Controls.Clear();
            this.ddlOffHireCategory.DataTextField = "CategoryName";
            this.ddlOffHireCategory.DataValueField = "CategoryId";
            this.ddlOffHireCategory.DataSource = Dt;
            this.ddlOffHireCategory.DataBind();

        }

    }
    protected void txtOffHireFromDt_TextChanged(object sender, EventArgs e)
    {
        try
        {
            
                GetOffhireDays();
            

        }
        catch (Exception ex)
        {
            lblOffhiremsg.Text = ex.Message.ToString();
        }
    }
    protected void txtOffHireToDt_TextChanged(object sender, EventArgs e)
    {
        try
        {
            
                GetOffhireDays();
           
        }
        catch (Exception ex)
        {
            lblOffhiremsg.Text = ex.Message.ToString();
        }
    }
    protected void GetOffhireDays()
    {
        if (string.IsNullOrWhiteSpace(txtOffHireFromDt.Text))
        {
            return;
        }
        if (string.IsNullOrWhiteSpace(txtOffHireToDt.Text))
        {
            return;
        }
        DateTime startDate = DateTime.Parse(txtOffHireFromDt.Text);
            DateTime endDate = DateTime.Parse(txtOffHireToDt.Text);
            TimeSpan diff = endDate - startDate;
            double days = diff.TotalDays + 1;

            txtOffhireDuration.Text = days.ToString();

            if (!(string.IsNullOrWhiteSpace(txtOffhireAmountperday.Text) && string.IsNullOrWhiteSpace(txtOffhireDuration.Text)))
            {
                txtOffHireAmount.Text = Math.Round((Common.CastAsDecimal(txtOffhireAmountperday.Text) * Common.CastAsDecimal(txtOffhireDuration.Text)), 2).ToString("N", new CultureInfo("en-US"));
            }
       
       
    }

    //protected void txtOffHireAmount_TextChanged(object sender, EventArgs e)
    //{
    //    try
    //    {

    //            GetOffhireDays();

    //    }
    //    catch (Exception ex)
    //    {
    //        lblOffhiremsg.Text = ex.Message.ToString();
    //    }
    //}

    #endregion

    protected void btnContractCloser_Click(object sender, EventArgs e)
    {
        try
        {
            Common.Execute_Procedures_Select_ByQuery("UPDATE RV_VesselContractDetails SET ContractStatus=2, ClosedDate = GETDATE(),ClosedBy = " + Login_Id + "  WHERE ContractId=" + ContractId + "");

           
            btnAddVoyage.Visible = false;
            btnAddActualExpenses.Visible = false;
            btnAddOffHireDetails.Visible = false;
            btnContractCloser.Visible = false;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "alert('Contract closed Sucessfully.');", true);
           // lbl.Text = "Contract closed Sucessfully.";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "refresh", "RefreshContractList();window.close();", true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "alert('"+ex.Message.ToString()+"');", true);
        }
    }
}
