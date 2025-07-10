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
using System.Globalization;



public partial class Transactions_InspectionPlanning : System.Web.UI.Page
{
    int Login_Id;
    int ContractId;
    int temp = 0;

    public string ContractStatus
    {
        get { return ViewState["ContractStatus"].ToString(); }
        set { ViewState["ContractStatus"] = value; }
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
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        //--------------------------------- PAGE ACCESS AUTHORITY ----------------------------------------------------
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 1094);
        if (chpageauth <= 0)
            Response.Redirect("blank.aspx");
        //------------------------------------------------------------------------------------------------------------
        //this.Form.DefaultButton = this.btnsave.UniqueID.ToString();
        if (Session["loginid"] == null)
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Login.aspx';", true);
        }
        else
        {
            Login_Id = Convert.ToInt32(Session["loginid"].ToString());
        }
        lblmessage.Text = "";
        if (Session["Mode"] != null)
        {
            if (Session["Mode"].ToString() != "Add")
            {
                if ((Session["ContractId"] == null))
                { 
                    Session.Add("PgFlag", 1); Response.Redirect("~/Modules/REVENUE/VesselContractSearch.aspx"); 
                }
                else
                {
                        ContractId = int.Parse(Session["ContractId"].ToString());   
                        Session.Add("ContractId", ContractId.ToString());
                }
            }
            else
            {
               
            }
        }
        //try { Inspection_Id = int.Parse(Session["Insp_Id"].ToString()); } catch { }
        if (Session["loginid"] != null)
        {
            ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 14);
            OBJ.Invoke();
            Session["Authority"] = OBJ.Authority;
            Auth = OBJ.Authority;
        }
        if (!Page.IsPostBack)
        {
            try
            {
                BindVessel();
                BindContractType();
                BindChartererDropDown();
                try
                {
                    Alerts.HANDLE_AUTHORITY(15, null, btnsave, btnCancelContract, null, Auth);
                }
                catch
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Default.aspx';", true);
                }
                
                //******************************************************************************
                if (Session["ContractId"] != null)
                {
                    ContractId = int.Parse(Session["ContractId"].ToString());
                    
                    DataTable dt88 = Revenue.AdVesselContractDetails(int.Parse(Session["ContractId"].ToString()), 0, DateTime.Now,0,0, DateTime.Now,0,0, 0, 0, 0, 0, 0, 0, "SELECT",0, 0, 0, 0, 0, 0, 0, "",0,0);
                    
                    if (dt88.Rows.Count > 0)
                    {
                        ContractStatus = dt88.Rows[0]["Status"].ToString();
                    }
                    if (ContractStatus != "Open")
                    {
                        btnsave.Visible = false;
                       // btnContractCloser.Visible = false;
                        temp = 1;  
                    }
                    else
                    {
                        btnsave.Visible = true;
                       // btnContractCloser.Visible = true;
                    }
                    Show_Contract_Record(ContractId);
                    tblExpExpenses.Visible = true;
                    BindExpectedExpenses(ContractId, Convert.ToInt32(ddlContractType.SelectedValue));
                    //DataTable dts = Common.Execute_Procedures_Select_ByQuery("select v.VesselCode +'-'+CAST(Year(R.CreatedOn) As varchar(4))+'-'+replace(str(ISNULL(convert(int,right(ContractId,3)),0),3),' ','0') as New from RV_VesselContractDetails R Inner join Vessel v with(nolock) on R.VesselId = v.VesselId WHERE ContractId=" + ContractId.ToString());
                    //if (dts.Rows.Count > 0)
                    //{
                    //    lblContractNo.Text = dts.Rows[0][0].ToString();
                    //}
                }   
            }
            catch (Exception ex)
            {
                lblmessage.Text = ex.StackTrace.ToString();
            }
       }
        // mode to check enable & disable cancel planning button
   
    }
    public void BindVessel()
    {
        try
        {
            DataSet dsVessel = Revenue.getMasterDataforRevenue("Vessel", "VesselId", "VesselName as Name",Convert.ToInt32(Session["loginid"].ToString()));
            this.ddlvessel.DataSource = dsVessel;
            this.ddlvessel.DataValueField = "VesselId";
            this.ddlvessel.DataTextField = "Name";
            this.ddlvessel.DataBind();
            ddlvessel.Items.Insert(0, "<Select>");
            ddlvessel.Items[0].Value = "0";
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public void BindContractType()
    {      
        try
        {
            DataSet dsContractType = Revenue.getMasterDataforRevenue("RV_ContractTypeMaster", "ContractTypeId", "ContractType");
            this.ddlContractType.DataSource = dsContractType;
            this.ddlContractType.DataValueField = "ContractTypeId";
            this.ddlContractType.DataTextField = "ContractType";
            this.ddlContractType.DataBind();
            ddlContractType.Items.Insert(0, "<Select>");
            ddlContractType.Items[0].Value = "0";

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void BindChartererDropDown()
    {
        DataTable dt4 = Revenue.selectDataCharterer();
        this.ddcharterer.DataValueField = "ChartererId";
        this.ddcharterer.DataTextField = "ChartererName";
        this.ddcharterer.DataSource = dt4;
        this.ddcharterer.DataBind();
    }
    //****Show Planning Record From InspectionDueId
    protected void Show_Contract_Record(int ContractId)
    {
        DataTable dt = Revenue.AdVesselContractDetails(ContractId, 0, DateTime.Now, 0, 0, DateTime.Now, 0, 0, 0, 0, 0, 0, 0, 0, "SELECT", 0, 0,0,0,0,0,0,"");
        if (dt.Rows.Count > 0)
        {
            //if (ContractStatus != "Open")
            //{
            //    btnContractCloser.Visible = false;
            //}
            //else
            //{
            //    btnContractCloser.Visible = true;
            //}
                
            foreach (DataRow dr in dt.Rows)
            {
                txtFromDate.Text = DateTime.Parse(dr["FromDate"].ToString()).ToString("dd-MMM-yyyy");
                txtToDate.Text = DateTime.Parse(dr["ToDate"].ToString()).ToString("dd-MMM-yyyy");
                if (Convert.ToInt32(dr["VesselId"].ToString()) > 0)
                {
                    ddlvessel.SelectedValue = dr["VesselId"].ToString();
                }
                if (Convert.ToInt32(dr["ContractTypeId"].ToString()) > 0)
                {
                    ddlContractType.SelectedValue = dr["ContractTypeId"].ToString();
                }
                if (Convert.ToInt32(dr["ChartererId"].ToString()) > 0)
                {
                    ddcharterer.SelectedValue = dr["ChartererId"].ToString();
                }
                AddComPercentage = 0;
                AddComAmount = 0;
                if (! string.IsNullOrEmpty(dr["AddCOMPer"].ToString()))
                    {
                    AddComPercentage = Common.CastAsDecimal(dr["AddCOMPer"].ToString());
                }
                if (!string.IsNullOrEmpty(dr["AddCOMAmout"].ToString()))
                {
                    AddComAmount = Common.CastAsDecimal(dr["AddCOMAmout"].ToString());
                }
                //    if (Convert.ToInt32(dr["ContractStatus"].ToString()) > 0)
                //{
                //    ddlStatus.SelectedValue = dr["ContractStatus"].ToString();
                //}
                if (Convert.ToInt32(ddlContractType.SelectedValue) == 1)
                {
                    tblTimeCharter.Visible = true;
                    tblVoyageCharter.Visible = false;
                    txtPerDayAmount.Enabled = true;
                    txtPerDayAmount.ReadOnly = false;
                    txtHireAmount.Enabled = false;
                    
                    if (!string.IsNullOrWhiteSpace(dr["ContractAmount"].ToString()))
                    {
                        txtHireAmount.Text = Math.Round(Common.CastAsDecimal(dr["ContractAmount"].ToString()), 2).ToString("N", new CultureInfo("en-US"));
                    }
                    if (!string.IsNullOrWhiteSpace(dr["FromHrs"].ToString()))
                    {
                        ddlFromHrs.SelectedValue = dr["FromHrs"].ToString().PadLeft(2, '0');
                    }
                    if (!string.IsNullOrWhiteSpace(dr["FromMins"].ToString()))
                    {
                        ddlFromMin.SelectedValue = dr["FromMins"].ToString().PadLeft(2, '0');
                    }
                    if (!string.IsNullOrWhiteSpace(dr["ToMins"].ToString()))
                    {
                        ddlToMins.SelectedValue = dr["ToMins"].ToString().PadLeft(2, '0');
                    }
                    if (!string.IsNullOrWhiteSpace(dr["ToHrs"].ToString()))
                    {
                        ddlToHrs.SelectedValue = dr["ToHrs"].ToString().PadLeft(2, '0');
                    }
                    if (!string.IsNullOrWhiteSpace(dr["ContractDuration"].ToString()))
                    {
                        txtTotalNoofDays.Text = dr["ContractDuration"].ToString();
                    }
                    if (!string.IsNullOrWhiteSpace(dr["ContractAmountperDay"].ToString()))
                    {
                        txtPerDayAmount.Text = Math.Round(Common.CastAsDecimal(dr["ContractAmountperDay"].ToString()), 2).ToString("N", new CultureInfo("en-US"));
                    }
                }
                else
                {
                    tblTimeCharter.Visible = false;
                    tblVoyageCharter.Visible = true;
                    if (!string.IsNullOrWhiteSpace(dr["TotalHireAmtVoyage"].ToString()))
                    {
                        txtTotalHireAmountforVoyage.Text = Math.Round(Common.CastAsDecimal(dr["TotalHireAmtVoyage"].ToString()), 2).ToString("N", new CultureInfo("en-US"));
                    }
                    if (!string.IsNullOrWhiteSpace(dr["Volume"].ToString()))
                    {
                        txtVolume.Text = Math.Round(Common.CastAsDecimal(dr["Volume"].ToString()), 2).ToString("N", new CultureInfo("en-US"));
                    }
                    if (!string.IsNullOrWhiteSpace(dr["Rate"].ToString()))
                    {
                        txtRateinTon.Text = Math.Round(Common.CastAsDecimal(dr["Rate"].ToString()), 2).ToString("N", new CultureInfo("en-US"));
                    }

                    if (!string.IsNullOrWhiteSpace(dr["ExpectedVoyageDuration"].ToString()))
                    {
                        txtExpVoyageDays.Text = Common.CastAsInt32(dr["ExpectedVoyageDuration"]).ToString();
                    }
                    if (!string.IsNullOrWhiteSpace(dr["FromPortName"].ToString()))
                    {
                        txtfromport.Text = dr["FromPortName"].ToString();
                    }
                    if (!string.IsNullOrWhiteSpace(dr["ToPortName"].ToString()))
                    {
                        txttoport.Text = dr["ToPortName"].ToString();
                    }
                    if (!string.IsNullOrWhiteSpace(dr["Cargodetails"].ToString()))
                    {
                        txtCargoDetails.Text = dr["Cargodetails"].ToString();
                    }
                }
                

               
                //if (!(string.IsNullOrWhiteSpace(txtFromDate.Text) && string.IsNullOrWhiteSpace(txtToDate.Text)))
                //{
                //    GetToNoofDays();
                //}

                //if (Convert.ToInt32(ddlContractType.SelectedValue) == 1)
                //{
                    
                //}
                //else
                //{
                //    txtPerDayAmount.Enabled = false;
                //    txtPerDayAmount.ReadOnly = true;
                //    txtHireAmount.Enabled = true;
                //}



                txtCreatedBy_DocumentType.Text = dr["Created_By"].ToString();
                txtCreatedOn_DocumentType.Text = dr["Created_On"].ToString();
                txtModifiedBy_DocumentType.Text = dr["Modified_By"].ToString();
                txtModifiedOn_DocumentType.Text = dr["Modified_On"].ToString();               
            }
        }        
    } 
    protected void btnsave_Click(object sender, EventArgs e)
    {
        try
        {
            lblmessage.Text = "";
            if (Convert.ToInt32(ddlvessel.SelectedIndex) == 0)
            {
                lblmessage.Text = "Please select Vessel";
                ddlvessel.Focus();
                return;
            }
            if (ddlContractType.SelectedIndex == 0)
            {
                lblmessage.Text = "Please select Contract Type";
                ddlContractType.Focus();
                return;
            }
            if (ddcharterer.SelectedIndex == 0)
            {
                lblmessage.Text = "Please select Charterer";
                ddcharterer.Focus();
                return;
            }

            if (ddlContractType.SelectedValue == "1")
            {
                if (string.IsNullOrWhiteSpace(txtFromDate.Text) && string.IsNullOrWhiteSpace(txtToDate.Text))
                {
                    lblmessage.Text = "Please select Contract Start and End Date";
                    txtFromDate.Focus();
                    txtToDate.Focus();
                    return;
                }
                else if ((!string.IsNullOrWhiteSpace(txtFromDate.Text)) && string.IsNullOrWhiteSpace(txtToDate.Text))
                {
                    lblmessage.Text = "Please select End Date";
                    txtToDate.Focus();
                    return;
                }
                else if (string.IsNullOrWhiteSpace(txtFromDate.Text) && (!string.IsNullOrWhiteSpace(txtToDate.Text)))
                {
                    lblmessage.Text = "Please select Start Date";
                    txtFromDate.Focus();
                    return;
                }
               
                if (ddlFromMin.SelectedIndex == 0)
                {
                    lblmessage.Text = "Please select Contract Start Mins";
                    ddlFromHrs.Focus();
                    return;
                }

                if (ddlFromHrs.SelectedIndex == 0)
                {
                    lblmessage.Text = "Please select Contract Start Hrs";
                    ddlFromHrs.Focus();
                }

                if (ddlToMins.SelectedIndex == 0)
                {
                    lblmessage.Text = "Please select Contract End Mins";
                    ddlToMins.Focus();
                    return;
                }

                if (ddlToHrs.SelectedIndex == 0)
                {
                    lblmessage.Text = "Please select Contract End Hrs";
                    ddlToHrs.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtHireAmount.Text))
                {
                    lblmessage.Text = "Please enter Contract Hire Amount";
                    txtHireAmount.Focus();
                    return;
                }
                else if (!string.IsNullOrWhiteSpace(txtHireAmount.Text) && Convert.ToDecimal(txtHireAmount.Text) <= 0)
                {
                    lblmessage.Text = "System should not allowed Zero or less amount of Contract Hire.";
                    txtHireAmount.Focus();
                    return;
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(txtfromport.Text))
                {
                    lblmessage.Text = "Please enter From Port";
                    txtfromport.Focus();
                    return;
                }
                if (string.IsNullOrWhiteSpace(txttoport.Text))
                {
                    lblmessage.Text = "Please enter To Port";
                    txttoport.Focus();
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtVolume.Text))
                {
                    lblmessage.Text = "Please enter Volume";
                    txtVolume.Focus();
                    return;
                }
                else if (!string.IsNullOrWhiteSpace(txtVolume.Text) && Convert.ToDecimal(txtVolume.Text) <= 0)
                {
                    lblmessage.Text = "System should not allowed Zero or less amount of Volume.";
                    txtVolume.Focus();
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtRateinTon.Text))
                {
                    lblmessage.Text = "Please enter Rate";
                    txtRateinTon.Focus();
                    return;
                }
                else if (!string.IsNullOrWhiteSpace(txtRateinTon.Text) && Convert.ToDecimal(txtRateinTon.Text) <= 0)
                {
                    lblmessage.Text = "System should not allowed Zero or less amount of Rate.";
                    txtRateinTon.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtExpVoyageDays.Text))
                {
                    lblmessage.Text = "Please enter Expected Voyage Days";
                    txtExpVoyageDays.Focus();
                    return;
                }
                else if (!string.IsNullOrWhiteSpace(txtExpVoyageDays.Text) && Convert.ToDecimal(txtExpVoyageDays.Text) <= 0)
                {
                    lblmessage.Text = "System should not allowed Zero or less amount of Expected Voyage Days";
                    txtRateinTon.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtTotalHireAmountforVoyage.Text))
                {
                    lblmessage.Text = "Please enter Total Hire Amount";
                    txtTotalHireAmountforVoyage.Focus();
                    return;
                }
                else if (!string.IsNullOrWhiteSpace(txtTotalHireAmountforVoyage.Text) && Convert.ToDecimal(txtTotalHireAmountforVoyage.Text) <= 0)
                {
                    lblmessage.Text = "System should not allowed Zero or less amount of Total Hire Amount";
                    txtTotalHireAmountforVoyage.Focus();
                    return;
                }
            }
           

            int ContractId = 0;
            if (Session["ContractId"] != null)
            {
                ContractId = Convert.ToInt32(Session["ContractId"].ToString());
            }

            int intCreatedBy = 0;
            int intModifiedBy = 0;
            intCreatedBy = Login_Id;
            intModifiedBy = Login_Id;

            int fromport = 0;
            int toport = 0;
            

            if (txtfromport.Text.Trim() != "")
            {
                DataTable dt1 = Inspection_Planning.CheckPort(txtfromport.Text);
                if (dt1.Rows[0][0].ToString() == "0")
                {
                    lblmessage.Text = "Please Enter Correct From Port Name.";
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
                    lblmessage.Text = "Please Enter Correct To Port Name.";
                    return;
                }
                else
                {
                    toport = int.Parse(dt2.Rows[0][0].ToString());
                }
            }
            //DataTable dtg = null;
            //try
            //{
            //    dtg = (DataTable)ViewState["GridData"];
            //}
            //catch { }

            DataTable dt;
            if (ContractId == 0)
            {
                DateTime fromDt = DateTime.Now;
                DateTime toDt = DateTime.Now;
                if (! string.IsNullOrWhiteSpace(txtFromDate.Text))
                {
                    fromDt = DateTime.Parse(txtFromDate.Text);
                }
                if (!string.IsNullOrWhiteSpace(txtToDate.Text))
                {
                    toDt = DateTime.Parse(txtToDate.Text);
                }
                dt = Revenue.AdVesselContractDetails(0, int.Parse(ddlvessel.SelectedValue), fromDt, ddlFromHrs.SelectedValue != "" ? Convert.ToInt32(ddlFromHrs.SelectedValue) : 0, ddlFromMin.SelectedValue != "" ?  Convert.ToInt32(ddlFromMin.SelectedValue) : 0, toDt, ddlToHrs.SelectedValue != "" ? Convert.ToInt32(ddlToHrs.SelectedValue) : 0, ddlToMins.SelectedValue != "" ? Convert.ToInt32(ddlToMins.SelectedValue) : 0, int.Parse(ddlContractType.SelectedValue), txtHireAmount.Text != "" ? Math.Round(Common.CastAsDecimal(txtHireAmount.Text), 2) : 0, txtPerDayAmount.Text != "" ? Math.Round(Common.CastAsDecimal(txtPerDayAmount.Text), 2) : 0 , txtTotalNoofDays.Text != "" ? Convert.ToInt32(txtTotalNoofDays.Text) : 0, 1, intCreatedBy, "ADD", int.Parse(ddcharterer.SelectedValue), fromport,toport, txtVolume.Text != "" ? Math.Round(Convert.ToDecimal(txtVolume.Text),2) : 0, txtRateinTon.Text != "" ? Math.Round(Convert.ToDecimal(txtRateinTon.Text), 2) : 0, txtTotalHireAmountforVoyage.Text != "" ? Math.Round(Convert.ToDecimal(txtTotalHireAmountforVoyage.Text), 2) : 0, txtExpVoyageDays.Text != "" ? int.Parse(txtExpVoyageDays.Text) : 0, txtCargoDetails.Text.Trim());
                if (dt.Rows.Count > 0)
                {
                    
                    if (dt.Rows[0][0].ToString() == "YES")
                    {
                        lblmessage.Text = "This Contract has already available.";
                        return;
                    }
                    else
                    {
                        
                        ContractId = Convert.ToInt32(dt.Rows[0][0].ToString());
                        Session.Add("ContractId", ContractId.ToString());
                                               
                        tblExpExpenses.Visible = true;
                        AddComPercentage = 0;
                        AddComAmount = 0;
                        BindExpectedExpenses(ContractId, Convert.ToInt32(ddlContractType.SelectedValue));
                        
                    }
                }
            }
            else
            {
                DateTime fromDt = DateTime.Now;
                DateTime toDt = DateTime.Now;
                if (!string.IsNullOrWhiteSpace(txtFromDate.Text))
                {
                    fromDt = DateTime.Parse(txtFromDate.Text);
                }
                if (!string.IsNullOrWhiteSpace(txtToDate.Text))
                {
                    toDt = DateTime.Parse(txtToDate.Text);
                }
                dt = Revenue.AdVesselContractDetails(ContractId, int.Parse(ddlvessel.SelectedValue), fromDt, ddlFromHrs.SelectedValue != "" ? Convert.ToInt32(ddlFromHrs.SelectedValue) : 0, ddlFromMin.SelectedValue != "" ? Convert.ToInt32(ddlFromMin.SelectedValue) : 0, toDt, ddlToHrs.SelectedValue != "" ? Convert.ToInt32(ddlToHrs.SelectedValue) : 0, ddlToMins.SelectedValue != "" ? Convert.ToInt32(ddlToMins.SelectedValue) : 0, int.Parse(ddlContractType.SelectedValue), txtHireAmount.Text != "" ? Math.Round(Common.CastAsDecimal(txtHireAmount.Text), 2) : 0, txtPerDayAmount.Text != "" ? Math.Round(Common.CastAsDecimal(txtPerDayAmount.Text), 2) : 0, txtTotalNoofDays.Text != "" ? Convert.ToInt32(txtTotalNoofDays.Text) : 0, 1, intCreatedBy, "MODIFY", int.Parse(ddcharterer.SelectedValue), fromport, toport, txtVolume.Text != "" ? Math.Round(Convert.ToDecimal(txtVolume.Text), 2) : 0, txtRateinTon.Text != "" ? Math.Round(Convert.ToDecimal(txtRateinTon.Text), 2) : 0, txtTotalHireAmountforVoyage.Text != "" ? Math.Round(Convert.ToDecimal(txtTotalHireAmountforVoyage.Text), 2) : 0, txtExpVoyageDays.Text != "" ? int.Parse(txtExpVoyageDays.Text) : 0, txtCargoDetails.Text.Trim(), txtAddComPer.Text != "" ? Math.Round(Convert.ToDecimal(txtAddComPer.Text), 2) : 0, txtAddComAmt.Text != "" ? Math.Round(Convert.ToDecimal(txtAddComAmt.Text), 2) : 0);

                string datastr, headerstr;
                datastr = "";
                headerstr = "";
                foreach (RepeaterItem item in rptExpExpenses.Items)
                {
                    Label lblCategoryName = item.FindControl("lblCategoryName") as Label;
                    TextBox txtRVCEEAmount = item.FindControl("txtRVCEEAmount") as TextBox;
                    HiddenField hdnCategoryId = item.FindControl("hdnCategoryId") as HiddenField;

                    if (!string.IsNullOrWhiteSpace(lblCategoryName.Text))
                    {
                        headerstr = headerstr + "," + lblCategoryName.Text;
                        datastr = datastr + "," + txtRVCEEAmount.Text.Replace(",","");
                    }
                }
                if (!string.IsNullOrWhiteSpace(headerstr))
                {
                    headerstr = headerstr.Substring(1);
                    datastr = datastr.Substring(1);
                    Revenue.UpdateExpExpensesDetails(ContractId, headerstr, datastr, Convert.ToInt32(Session["loginid"]));

                }
                btnsave.Enabled = false;
            }
            lblmessage.Text = "Contract details saved Sucessfully.";
            lblmessage.Visible = true;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "refresh", "OpenVesselContract();", true);
        }
        catch (Exception ex)
        {
            lblmessage.Text = ex.Message.ToString();
        }     
    }
    //protected string GetInspectionNumber(int VesselId)
    //{
    //    try
    //    {
    //        string inspNum = "0";
    //        DataTable Dt = Inspection_Planning.CreateInspectionNo(VesselId);
    //        if (Dt.Rows.Count == 0)
    //        {
    //            inspNum = "001";
    //        }
    //        else if (int.Parse(Dt.Rows[0][0].ToString()) <=8)
    //        {
    //            inspNum = "00" + (int.Parse(Dt.Rows[0][0].ToString()) + 1).ToString();
    //        }
    //        else if (int.Parse(Dt.Rows[0][0].ToString()) <=98)
    //        {
    //            inspNum = "0" + (int.Parse(Dt.Rows[0][0].ToString()) + 1).ToString();
    //        }
    //        else
    //        {
    //            inspNum = (int.Parse(Dt.Rows[0][0].ToString()) + 1).ToString();
    //        }
    //        string[] str;
    //        char ch ='-';
    //        str= ddlinspection.SelectedItem.Text.Split(ch) ;
    //        DataTable dt1 = Inspection_Planning.GetVesselCode(VesselId);
    //        DataTable dt_pankaj=Common.Execute_Procedures_Select_ByQuery("select replace(str(ISNULL(MAX(convert(int,right(InspectionNo,3))),0)+1,3),' ','0') as NewInspNo from t_inspectiondue where vesselid=" + VesselId.ToString()); 
    //        if(dt_pankaj.Rows.Count>0)
    //        {
    //            inspNum=dt_pankaj.Rows[0][0].ToString();
    //        }

    //        string InspectionNumber = dt1.Rows[0][0].ToString() + "/" + str[0].ToString().Trim() + "/" + inspNum;           //VSL Code + Year (4)+Month(2)+2 Digits(Number)
    //        return InspectionNumber;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}


    protected void txtFromDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (! (string.IsNullOrWhiteSpace(txtFromDate.Text) && string.IsNullOrWhiteSpace(txtToDate.Text)))
            {
                GetToNoofDays();
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
                GetToNoofDays();
            }
        }
        catch(Exception ex)
        {

        }
    }

    protected void GetToNoofDays()
    {

        DateTime startDate = DateTime.Parse(txtFromDate.Text);
        DateTime endDate = DateTime.Parse(txtToDate.Text);
        TimeSpan diff = endDate - startDate;
        double days = diff.TotalDays+1;
        //DateTime fromDate, toDate;
        //fromDate = Convert.ToDateTime(txtFromDate.Text);
        //toDate = Convert.ToDateTime(txtToDate.Text);

        //DateTime fromdate = DateTime.MinValue;
        //DateTime todate = DateTime.MaxValue;
        //TimeSpan noofdays = todate - fromdate;
        txtTotalNoofDays.Text = days.ToString();

        if (! (string.IsNullOrWhiteSpace(txtHireAmount.Text) && string.IsNullOrWhiteSpace(txtTotalNoofDays.Text)))
        {
            txtPerDayAmount.Text = Math.Round((Common.CastAsDecimal(txtHireAmount.Text) / Common.CastAsDecimal(txtTotalNoofDays.Text)), 2).ToString("N", new CultureInfo("en-US"));
        }
    }

    protected void txtHireAmount_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (!(string.IsNullOrWhiteSpace(txtFromDate.Text) && string.IsNullOrWhiteSpace(txtToDate.Text)))
            {
                GetToNoofDays();
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void btnCancelContract_Click(object sender, EventArgs e)
    {
        Session["ContractId"] = null;
        Session["Mode"] = null;
        //Response.Redirect("VesselContractSearch.aspx");
        string jScript = "window.top.location.href = 'VesselContractSearch.aspx';";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "forceParentLoad", jScript, true);
    }
    public string FormatCurr(object _in)
    {
        return string.Format("{0:0.00}", _in);
    }

    protected void txtRVCEEAmount_TextChanged(object sender, EventArgs e)
    {
        try
        {
            getTotalExpectedExpenses();
        }
        catch (Exception ex)
        { ShowMessage(ex.Message, true); }

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
            foreach (Control c in item.Controls)
            {
                
                if (c.GetType() == typeof(TextBox))
                {
                    ((TextBox)c).Enabled = true;
                    ((TextBox)c).CssClass = "ctltext";
                }
            }
        }
        if (! string.IsNullOrWhiteSpace(txtAddComAmt.Text))
        {
            totalExpExpenses = totalExpExpenses + Common.CastAsDecimal(txtAddComAmt.Text);
        }
        
        txtTotalExpExpenses.Text = totalExpExpenses.ToString("N", new CultureInfo("en-US"));
        if (Convert.ToInt32(ddlContractType.SelectedValue) == 1)
        {
            txtTotalRevenue.Text = Math.Round((Common.CastAsDecimal(txtHireAmount.Text) - totalExpExpenses), 2).ToString("N", new CultureInfo("en-US"));
        }
        else
        {
            txtTotalRevenue.Text = Math.Round((Common.CastAsDecimal(txtTotalHireAmountforVoyage.Text) - totalExpExpenses), 2).ToString("N", new CultureInfo("en-US"));
        }
       
    }

    protected void BindExpectedExpenses(int contractId, int ContractTypeId)
    {
        DataTable dtExpExpenses = Revenue.ContractExpectedExpenses(contractId, ContractTypeId);
        if (dtExpExpenses.Rows.Count > 0)
        {
            rptExpExpenses.DataSource = dtExpExpenses;
            rptExpExpenses.DataBind();
            if (AddComPercentage <= 0 )
            {
                txtAddComPer.Text = "0.00";
            }
            else
            {
                txtAddComPer.Text = AddComPercentage.ToString();
            }
            if (AddComAmount <= 0)
            {
                txtAddComAmt.Text = "0.00";
            }
            else
            {
                txtAddComAmt.Text = AddComAmount.ToString();
            }

           

        }
        else
        {
            rptExpExpenses.DataSource = null;
            rptExpExpenses.DataBind();
        }
        getTotalExpectedExpenses(); 
    }
    public void ShowMessage(string Message, bool Error)
    {
        lblmessage.Text = Message;
        lblmessage.ForeColor = (Error) ? System.Drawing.Color.Red : System.Drawing.Color.Green;
    }
    protected void txtPerDayAmount_TextChanged(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtTotalNoofDays.Text) )
        {
            return;
        }

        if (! string.IsNullOrEmpty(txtTotalNoofDays.Text) && Convert.ToInt32(txtTotalNoofDays.Text) <= 0)
        {
            return;
        }

        if (string.IsNullOrEmpty(txtPerDayAmount.Text))
        {
            return;
        }

        if (!string.IsNullOrEmpty(txtPerDayAmount.Text) && Convert.ToDecimal(txtPerDayAmount.Text) <= 0)
        {
            return;
        }
        txtHireAmount.Text = Math.Round((Common.CastAsDecimal(txtPerDayAmount.Text) * Common.CastAsDecimal(txtTotalNoofDays.Text)), 2).ToString("N", new CultureInfo("en-US"));
        CalculateAddCompercentage();
    }

    protected void ddlContractType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlContractType.SelectedValue) == 1)
        {
            txtPerDayAmount.Enabled = true;
            txtPerDayAmount.ReadOnly = false;
            txtHireAmount.Enabled = false;
            tblTimeCharter.Visible = true;
            tblVoyageCharter.Visible = false;
        }
        else
        {
            tblTimeCharter.Visible = false;
            tblVoyageCharter.Visible = true;
        }
    }

   

    protected void txtVolume_TextChanged(object sender, EventArgs e)
    {
        try
        {
            txtTotalHireAmountforVoyage.Text = "";
            if (string.IsNullOrWhiteSpace(txtVolume.Text))
            {
                return;
            }
            if (!string.IsNullOrWhiteSpace(txtVolume.Text) && Convert.ToDecimal(txtVolume.Text) <= 0)
            {
                return;
            }

           txtVolume.Text =  Math.Round(Common.CastAsDecimal(txtVolume.Text),2).ToString("N", new CultureInfo("en-US"));
            CalculateTotalHireAmountforVoyage();
            CalculateAddCompercentage();

        }
        catch(Exception ex)
        {
            lblmessage.Text = ex.Message.ToString();
        }
    }

    protected void txtRateinTon_TextChanged(object sender, EventArgs e)
    {
        try
        {
            txtTotalHireAmountforVoyage.Text = "";
            if (string.IsNullOrWhiteSpace(txtRateinTon.Text))
            {
                return;
            }
            if (!string.IsNullOrWhiteSpace(txtRateinTon.Text) && Convert.ToDecimal(txtRateinTon.Text) <= 0)
            {
                return;
            }

            txtRateinTon.Text = Math.Round(Common.CastAsDecimal(txtRateinTon.Text), 2).ToString("N", new CultureInfo("en-US"));
            CalculateTotalHireAmountforVoyage();
            CalculateAddCompercentage();

        }
        catch (Exception ex)
        {
            lblmessage.Text = ex.Message.ToString();
        }
    }

    protected void CalculateTotalHireAmountforVoyage()
    {
        
        if (string.IsNullOrWhiteSpace(txtVolume.Text))
        {
            return;
        }
        if (string.IsNullOrWhiteSpace(txtRateinTon.Text))
        {
            return;
        }

        if (! string.IsNullOrWhiteSpace(txtVolume.Text) && Convert.ToDecimal(txtVolume.Text) <= 0)
        {
            return; 
        }

        if (!string.IsNullOrWhiteSpace(txtRateinTon.Text) && Convert.ToDecimal(txtRateinTon.Text) <= 0)
        {
            return;
        }

        Decimal volume = Common.CastAsDecimal(txtVolume.Text);
        Decimal rate = Common.CastAsDecimal(txtRateinTon.Text);
        Decimal totalHireAmountforVoyage = Math.Round(Common.CastAsDecimal(volume * rate), 2);
        txtTotalHireAmountforVoyage.Text = totalHireAmountforVoyage.ToString("N", new CultureInfo("en-US"));
    }

    protected void txtAddComPer_TextChanged(object sender, EventArgs e)
    {
        try
        {
            CalculateAddCompercentage();
        }
        catch(Exception ex)
        {
            lblmessage.Text = ex.Message.ToString();
        }
    }

    protected void CalculateAddCompercentage()
    {
        if (string.IsNullOrWhiteSpace(txtAddComPer.Text))
        {
            return;
        }
        if (!string.IsNullOrWhiteSpace(txtAddComPer.Text) && Common.CastAsDecimal(txtAddComPer.Text) <= 0)
        {
            return;
        }
        decimal addComPer = 0;
        addComPer = Math.Round(Common.CastAsDecimal(txtAddComPer.Text), 2);
        txtAddComPer.Text = addComPer.ToString("N", new CultureInfo("en-US"));
        if (Convert.ToInt32(ddlContractType.SelectedValue) == 2)
        {
            txtAddComAmt.Text = Math.Round((Common.CastAsDecimal(txtTotalHireAmountforVoyage.Text) * addComPer) / 100, 2).ToString("N", new CultureInfo("en-US"));
        }
        else
        {
            txtAddComAmt.Text = Math.Round((Common.CastAsDecimal(txtHireAmount.Text) * addComPer)  / 100, 2).ToString("N", new CultureInfo("en-US"));
        }
        getTotalExpectedExpenses();
    }
}