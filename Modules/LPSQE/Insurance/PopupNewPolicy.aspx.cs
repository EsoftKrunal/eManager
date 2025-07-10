using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using ShipSoft.CrewManager.BusinessObjects;
using ShipSoft.CrewManager.BusinessLogicLayer;
using ShipSoft.CrewManager.Operational;
using System.IO;

public partial class InsuranceRecordManagement_PopupNewPolicy : System.Web.UI.Page
{
    public int PolicyId
    {
        set { ViewState["PolicyId"] = value; }
        get { return Common.CastAsInt32(ViewState["PolicyId"].ToString()); }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        lblMessage.Text = "";
        if (!Page.IsPostBack)
        {
            BindGroups();
            BindVessels();
            BindUW();
            BindTime();
            BindCurrency();
            if (Request.QueryString["Mode"] != null && Request.QueryString["PId"] != null)
            {
                if (Request.QueryString["Mode"].ToString() == "A")
                {
                    lblPageName.Text = "New Policy Entry";
                    PolicyId = 0;
                    //GetNextPolicyNo();
                    txt_PolicyNo.ReadOnly = false;

                }
                if (Request.QueryString["Mode"].ToString() == "V")
                {
                    lblPageName.Text = "View Policy";
                    PolicyId = Convert.ToInt32(Request.QueryString["PId"].ToString());
                    ViewPolicyDetails();
                    btnSave.Visible = false;
                    btnSaveDoc.Visible = false;
                    btnShowSchedule.Visible = false;
                    //txt_PolicyNo.ReadOnly = true;
                }
                if (Request.QueryString["Mode"].ToString() == "E")
                {
                    lblPageName.Text = "Edit Policy";
                    PolicyId = Convert.ToInt32(Request.QueryString["PId"].ToString());
                    ViewPolicyDetails();
                    //txt_PolicyNo.ReadOnly = true;
                }
            }
        }
    }
    #region -------------- UDF ---------------------------------

    public void BindGroups()
    {
        try
        {
            DataTable dtGroups = Budget.getTable("SELECT GroupId,ShortName FROM IRM_Groups ORDER BY GroupName").Tables[0];
            ddl_Groups.DataSource = dtGroups;
            ddl_Groups.DataTextField = "ShortName";
            ddl_Groups.DataValueField = "GroupId";
            ddl_Groups.DataBind();
            ddl_Groups.Items.Insert(0, new ListItem("< Select >", "0"));
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    public void BindSubGroups()
    {
        //try
        //{
        //    this.ddl_SubGroups.Items.Clear();
        //    DataTable dtSubGroups = Budget.getTable("SELECT SubGroupId,SubGroupName FROM IRM_SubGroups WHERE GroupId = " + ddl_Groups.SelectedValue.Trim() + " ORDER BY SubGroupName").Tables[0];
        //    this.ddl_SubGroups.DataSource = dtSubGroups;
        //    this.ddl_SubGroups.DataValueField = "SubGroupId";
        //    this.ddl_SubGroups.DataTextField = "SubGroupName";
        //    this.ddl_SubGroups.DataBind();
        //    this.ddl_SubGroups.Items.Insert(0, new ListItem("< Select >", "0"));
        //}
        //catch (Exception ex)
        //{
        //    throw ex;
        //}
    }
    public void BindVessels()
    {
        try
        {
            DataTable dtVessels = Budget.getTable("select VESSELNAME,VESSELID FROM dbo.vessel v where v.VesselStatusid<>2  order by vesselname").Tables[0];
            this.ddl_Vessels.DataSource = dtVessels;
            this.ddl_Vessels.DataValueField = "VESSELID";
            this.ddl_Vessels.DataTextField = "VESSELNAME";
            this.ddl_Vessels.DataBind();

            this.ddl_Vessels.Items.Insert(0, new ListItem("< Select >", "0"));
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    public void BindUW()
    {
        string strUW = "SELECT UWId,UWName,ShortName FROM IRM_UWMaster ORDER BY UWName ";
        DataTable dtUW = Budget.getTable(strUW).Tables[0];
        ddl_UW.DataSource = dtUW;
        ddl_UW.DataTextField = "ShortName";
        ddl_UW.DataValueField = "UWId";
        ddl_UW.DataBind();
        ddl_UW.Items.Insert(0, new ListItem("< Select >", "0"));

    }
    public void BindTime()
    {
        for (int i = 0; i < 24; i++)
        {
            if (i < 10)
            {
                ddlFromHour.Items.Add("0" + i);
                ddlToHour.Items.Add("0" + i);
            }
            else
            {
                ddlFromHour.Items.Add(new ListItem(Convert.ToString(i), Convert.ToString(i)));
                ddlToHour.Items.Add(new ListItem(Convert.ToString(i), Convert.ToString(i)));
            }
        }

        for (int j = 0; j < 60; j++)
        {
            if (j < 10)
            {
                ddlFromMin.Items.Add("0" + j);
                ddlToMin.Items.Add("0" + j);
            }
            else
            {
                ddlFromMin.Items.Add(new ListItem(Convert.ToString(j), Convert.ToString(j)));
                ddlToMin.Items.Add(new ListItem(Convert.ToString(j), Convert.ToString(j)));
            }
        }
    }
    public void GetNextPolicyNo()
    {
        string strPolicyNo = "SELECT SUBSTRING(MAX(PolicyNo),0,3) + REPLACE(STR(ISNULL(CONVERT(int, SUBSTRING(MAX(PolicyNo),3,LEN(MAX(PolicyNo)))),0) + 1, 6 ),' ','0' ) AS PolicyNo FROM IRM_PolicyMaster ";
        DataTable dtGroups = Budget.getTable(strPolicyNo).Tables[0];
        txt_PolicyNo.Text = dtGroups.Rows[0]["PolicyNo"].ToString().Trim();
        //txt_PolicyNo.ReadOnly = true;
    }
    public void BindDocs()
    {
        //DataTable dt = new DataTable();
        //DataRow dr = null;
        //dt.Columns.Add("DocNo");
        //dt.Columns.Add("DocName");

        //for (int i = 0; i < 5; i++)
        //{
        //    dr = dt.NewRow();
        //    dt.Rows.Add(dr);
        //    dt.Rows[dt.Rows.Count - 1][0] = "";
        //    dt.Rows[dt.Rows.Count - 1][1] = "";
        //}

        //rptDocs.DataSource = dt;
        //rptDocs.DataBind();

        string strSQL = "Select DocId,PolicyId,DocNumber,DocName,FileName,Attachment FROM IRM_PolicyDocuments WHERE PolicyId = " + PolicyId;
        DataTable dtDocs = Budget.getTable(strSQL).Tables[0];
        if (dtDocs.Rows.Count > 0)
        {
            rptDocs.DataSource = dtDocs;
            rptDocs.DataBind();
        }
        else
        {
            rptDocs.DataSource = null;
            rptDocs.DataBind();
        }
    }
    public Boolean IsValidated()
    {
        DateTime dt;
        if (txt_IssuedDt.Text.Trim() != "")
        {
            if (!DateTime.TryParse(txt_IssuedDt.Text.Trim(), out dt))
            {
                lblMessage.Text = "Please enter valid date.";
                txt_IssuedDt.Focus();
                return false;
            }
        }
        if (ddlCurrInsuredValue.SelectedIndex == 0)
        {
            lblMessage.Text = "Please select currency.";
            ddlCurrInsuredValue.Focus();return false;
        }
        if (txt_InceptionDt.Text.Trim() != "")
        {
            if (!DateTime.TryParse(txt_InceptionDt.Text.Trim(), out dt))
            {
                lblMessage.Text = "Please enter valid date.";
                txt_InceptionDt.Focus();
                return false;
            }
        }
        if (txt_ExpiryDt.Text.Trim() != "")
        {
            if (!DateTime.TryParse(txt_ExpiryDt.Text.Trim(), out dt))
            {
                lblMessage.Text = "Please enter valid date.";
                txt_ExpiryDt.Focus();
                return false;
            }
        }
        if (txt_InceptionDt.Text.Trim() != "" && txt_ExpiryDt.Text.Trim() != "")
        {
            if (Convert.ToDateTime(txt_InceptionDt.Text.Trim()) == Convert.ToDateTime(txt_ExpiryDt.Text.Trim()))
            {
                lblMessage.Text = "From date and expiry date can not be same.";
                txt_InceptionDt.Focus();
                return false;
            }
        }
        if (txt_InceptionDt.Text.Trim() != "" && txt_ExpiryDt.Text.Trim() != "")
        {
            if (Convert.ToDateTime(txt_ExpiryDt.Text.Trim()) < Convert.ToDateTime(txt_InceptionDt.Text.Trim()))
            {
                lblMessage.Text = "Expiry date can not be less than from date.";
                txt_InceptionDt.Focus();
                return false;
            }
        }
        if (txt_AssuredS.Text.Trim() != "")
        {
            if (txt_AssuredS.Text.Trim().Length > 500)
            {
                lblMessage.Text = "Assured(S) can not be more than 500 characters.";
                txt_AssuredS.Focus();
                return false;
            }
        }
        //if (txt_MatterInsured.Text.Trim() != "")
        //{
        //    if (txt_MatterInsured.Text.Trim().Length > 500)
        //    {
        //        lblMessage.Text = "Subject-Matter insured can not be more than 500 characters.";
        //        txt_MatterInsured.Focus();
        //        return false;
        //    }
        //}
        //foreach (RepeaterItem item in rptDocs.Items)
        //{
        //    TextBox txt_DocNo = (TextBox)item.FindControl("txt_DocNo");
        //    TextBox txt_DocName = (TextBox)item.FindControl("txt_DocName");
        //    FileUpload flAttachDocs = (FileUpload)item.FindControl("flAttachDocs");

        //    if (flAttachDocs.HasFile)
        //    {
        //        if (txt_DocNo.Text.Trim() == "")
        //        {
        //            lblMessage.Text = "Please enter document no.";
        //            txt_DocNo.Focus();
        //            return false;
        //        }
        //        if (txt_DocName.Text.Trim() == "")
        //        {
        //            lblMessage.Text = "Please enter document name.";
        //            txt_DocName.Focus();
        //            return false;
        //        }
        //    }
        //    //else
        //    //{
        //    //    if (txt_DocNo.Text.Trim() != "" && txt_DocName.Text.Trim() == "")
        //    //    {
        //    //        lblMessage.Text = "Please enter document name.";
        //    //        txt_DocName.Focus();
        //    //        return false;
        //    //    } 
        //    //    if (txt_DocNo.Text.Trim() == "" && txt_DocName.Text.Trim() != "")
        //    //    {
        //    //        lblMessage.Text = "Please enter document no.";
        //    //        txt_DocNo.Focus();
        //    //        return false;
        //    //    }

        //    //}
        //}
        return true;
    }

    public void ViewPolicyDetails()
    {
        string strPolicyMaster = "SELECT PolicyNo,VesselId,HullLC,MachinaryLC, isnull(Mortgagee,0)Mortgagee,PaymentByMtm,HMLC,CargoLC,CrewLC,OthersLC,REPLACE(CONVERT(VARCHAR,IssuedDt,106),' ','-') AS IssuedDt,PlaceIssued, REPLACE(CONVERT(VARCHAR,InceptionDt,106),' ','-') AS InceptionDt,InceptionDt AS incepTime,REPLACE(CONVERT(VARCHAR,ExpiryDt,106),' ','-') AS ExpiryDt,ExpiryDt As ExpTime,InsuredAmount,Rate,DeductibleAmount,TotalPremium,ArrangedBy,Broker,BrokerName,UWId,GroupId,Material,Assured,SubjectMatterInsured,Hull,Machinary,HM,Cargo,Crew, Others,RDC,InsuranceCurr,InsuranceAmountUSD,PremiumCurr,RateUSD,TotalPremiumUSD,OtherCurr,DeductibleUSD FROM IRM_PolicyMaster WHERE PolicyId = " + PolicyId;
        DataTable dtPM = Budget.getTable(strPolicyMaster).Tables[0];

        if (dtPM.Rows.Count > 0)
        {
            ddl_Groups.SelectedValue = dtPM.Rows[0]["GroupId"].ToString();
            ddl_Groups_SelectedIndexChanged(null, null);
            ddl_Vessels.SelectedValue = dtPM.Rows[0]["VesselId"].ToString();
            //txt_Material.Text = dtPM.Rows[0]["Material"].ToString();
            //lbl_GrossTonnage.Text = dtPM.Rows[0]["GrossTonnage"].ToString();
            ddlArrangedBy.SelectedValue = dtPM.Rows[0]["ArrangedBy"].ToString();
            ddl_UW.SelectedValue = dtPM.Rows[0]["UWId"].ToString();
            ddlBroker.SelectedValue = dtPM.Rows[0]["Broker"].ToString();
            ddlBroker_SelectedIndexChanged(new object(), new EventArgs());
            txtBrokername.Text = dtPM.Rows[0]["BrokerName"].ToString();
            txt_IssuedPlace.Text = dtPM.Rows[0]["PlaceIssued"].ToString();
            txt_PolicyNo.Text = dtPM.Rows[0]["PolicyNo"].ToString();
            txt_IssuedDt.Text = dtPM.Rows[0]["IssuedDt"].ToString();
            txt_InsuredAmount.Text = FormatCurrency( dtPM.Rows[0]["InsuredAmount"]);
            ddlPaymentByMTM.SelectedValue = Common.CastAsInt32( dtPM.Rows[0]["PaymentByMtm"]).ToString();
            chkMortagee.Checked = (dtPM.Rows[0]["Mortgagee"].ToString() == "1") ? true : false;

            //txt_InsuredAmount_TextChanged(null, null);
            txt_InceptionDt.Text = dtPM.Rows[0]["InceptionDt"].ToString();
            DateTime dtIncT = Convert.ToDateTime(dtPM.Rows[0]["incepTime"]);
            string FromTime = dtIncT.ToString("HH:mm");
            ddlFromHour.SelectedValue = FromTime.Split(':').GetValue(0).ToString();
            ddlFromMin.SelectedValue = FromTime.Split(':').GetValue(1).ToString();
            DateTime dtExpT = Convert.ToDateTime(dtPM.Rows[0]["ExpTime"]);
            string ToTime = dtExpT.ToString("HH:mm");
            ddlToHour.SelectedValue = ToTime.Split(':').GetValue(0).ToString();
            ddlToMin.SelectedValue = ToTime.Split(':').GetValue(1).ToString();
            txt_ExpiryDt.Text = dtPM.Rows[0]["ExpiryDt"].ToString();
            //txt_Deductible.Text = dtPM.Rows[0]["DeductibleAmount"].ToString();
            txt_Rate.Text = dtPM.Rows[0]["Rate"].ToString();
            txt_Rate_TextChanged(null, null);
            txt_Premium.Text = FormatCurrency( dtPM.Rows[0]["TotalPremium"]);
            txt_Premium_TextChanged(null, null);
            //ddl_PremiumTerms.SelectedValue = dtPM.Rows[0]["PremiumTerms"].ToString();
            txt_AssuredS.Text = dtPM.Rows[0]["Assured"].ToString();
            //txt_MatterInsured.Text = dtPM.Rows[0]["SubjectMatterInsured"].ToString();
            ddl_Vessels_SelectedIndexChanged(null, null);
            if (ddl_Groups.SelectedValue == "9")
            {
                txt_Hull.Text = dtPM.Rows[0]["Hull"].ToString();
                txt_Machinery.Text = dtPM.Rows[0]["Machinary"].ToString();
                txt_HM.Text = dtPM.Rows[0]["HM"].ToString();
                txt_Hull_TextChanged(null, null);
                txt_Machinery_TextChanged(null, null);
                txt_HM_TextChanged(null, null);
                
            }
            else if (ddl_Groups.SelectedValue == "7")
            {
                txt_Cargo.Text = dtPM.Rows[0]["Cargo"].ToString();
                txt_Crew.Text = dtPM.Rows[0]["Crew"].ToString();
                txt_Others.Text = dtPM.Rows[0]["Others"].ToString();
                txt_Cargo_TextChanged(null, null);
                txt_Crew_TextChanged(null, null);
                txt_Others_TextChanged(null, null);
            }
            else
            {
                txt_Deductible.Text = dtPM.Rows[0]["DeductibleAmount"].ToString();
                txt_Deductible_TextChanged(null, null);
            }
            ddlRDC.SelectedValue = dtPM.Rows[0]["RDC"].ToString();

            //------------------------------------------------------------------------------------------
            if (dtPM.Rows[0]["InsuranceCurr"].ToString() != "")
                ddlCurrInsuredValue.SelectedValue = dtPM.Rows[0]["InsuranceCurr"].ToString();
            else
                ddlCurrInsuredValue.SelectedValue = "";

            txt_InsuredAmountUSDoler.Text = FormatCurrency( dtPM.Rows[0]["InsuranceAmountUSD"]);

            //if (dtPM.Rows[0]["PremiumCurr"].ToString() != "")
            //    ddlCurrPremium.SelectedValue = dtPM.Rows[0]["PremiumCurr"].ToString();
            //else
            //    ddlCurrPremium.SelectedValue = "";

            //txtRateUSDoller.Text = dtPM.Rows[0]["RateUSD"].ToString();
            txtPremiumUSDoller.Text = FormatCurrency( dtPM.Rows[0]["TotalPremiumUSD"]);

            //if (dtPM.Rows[0]["OtherCurr"].ToString() != null)
            //    ddlCurrDeductible.SelectedValue = dtPM.Rows[0]["OtherCurr"].ToString();
            //else
            //    ddlCurrDeductible.SelectedValue = "";
            //txt_DeductibleDoller.Text = dtPM.Rows[0]["DeductibleUSD"].ToString();
        }

        //---------------------------
        txtHalLC.Text = dtPM.Rows[0]["HullLC"].ToString();
        txtMachineryLC.Text = dtPM.Rows[0]["MachinaryLC"].ToString();
        txtHMLC.Text = dtPM.Rows[0]["HMLC"].ToString();

        txtCargoLC.Text = dtPM.Rows[0]["CargoLC"].ToString();
        txtCrewLC.Text = dtPM.Rows[0]["CrewLC"].ToString();
        txtOthersLC.Text = dtPM.Rows[0]["OthersLC"].ToString();


        string strPolicyDetails = "SELECT Id,PolicyId,InstallmentDetails,REPLACE(CONVERT(VARCHAR,InstallmentDt,106),' ','-') AS InstallmentDt,PremiumAmount AS Amount,PremiumAmountLC as AmountLC FROM IRM_PolicyDetails WHERE PolicyId = " + PolicyId;
        DataTable dtPD = Budget.getTable(strPolicyDetails).Tables[0];

        if (dtPD.Rows.Count > 0)
        {
            rptInstallment.DataSource = dtPD;
            rptInstallment.DataBind();
            txtNoInstallment.Text = dtPD.Rows.Count.ToString();
            foreach (RepeaterItem it in rptInstallment.Items)
            {
                TextBox txtAmount = (TextBox)it.FindControl("txtAmount");
                TextBox txtAmountLC = (TextBox)it.FindControl("txtAmountLC");
                
                decimal amount = Common.CastAsDecimal(txtAmount.Text.Trim());
                decimal amountLC = Common.CastAsDecimal(txtAmountLC.Text.Trim());

                txtAmount.Text = string.Format("{0:C}", amount).Trim().Replace("Rs.", "").Trim();
                txtAmountLC.Text = string.Format("{0:C}", amountLC).Trim().Replace("Rs.", "").Trim();
            }
        }

        string strPolicyDocs = "SELECT DocId,PolicyId,DocNumber,DocName,FileName,Attachment FROM IRM_PolicyDocuments WHERE PolicyId = " + PolicyId;
        DataTable dtPDocs = Budget.getTable(strPolicyDocs).Tables[0];

        if (dtPDocs.Rows.Count > 0)
        {
            rptDocs.DataSource = dtPDocs;
            rptDocs.DataBind();
        }
        
    }

    //public static string FormatCurrency(object Amount)
    //{
    //    return "$" + string.Format("{0:C}", Amount).Replace("$", "");
    //}
    public static string FormatCurrency2(object Amount)
    {
        decimal amt = Common.CastAsInt32(Amount);
        if (amt < 0)
            return "-" + amt.ToString("C3", System.Globalization.CultureInfo.CurrentCulture).Replace(".000", "").Replace("$", "").Replace("(", "").Replace(")", "").Replace("Rs.", "");
        else
            return amt.ToString("C3", System.Globalization.CultureInfo.CurrentCulture).Replace(".000", "").Replace("$", "").Replace("(", "").Replace(")", "").Replace("Rs.", "");
    }
    public static string FormatCurrencyWithoutSign(object Amount)
    {
        return string.Format("{0:C}", Amount).Replace("$", "").Replace("(", "").Replace(")", "");
    }

    public void BindCurrency()
    {
        DataSet ds = new DataSet();

        try
        {
            // To run on client side
            string strSQL = "SELECT Curr FROM DBO.VW_tblWebCurr ORDER BY Curr";
            ds = Budget.getTable(strSQL);
        }
        catch (Exception ex)
        {
            // To run My side
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = "Data Source=172.30.1.10;Initial Catalog=MTMPOS;Persist Security Info=True;User Id=sa;Password=Esoft^%$#@!;Connection Timeout=300;";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = myConnection;
            SqlDataAdapter Adp = new SqlDataAdapter();
            Adp.SelectCommand = cmd;
            cmd.CommandText = "SELECT Curr FROM VW_tblWebCurr ORDER BY Curr";
            Adp.Fill(ds, "tbl");
        }


        //ddlCurrDeductible.DataSource = ds;
        //ddlCurrDeductible.DataTextField = "Curr";
        //ddlCurrDeductible.DataValueField = "Curr";
        //ddlCurrDeductible.DataBind();
        //ddlCurrDeductible.Items.Insert(0, new ListItem("< Select >", "0"));
        //ddlCurrDeductible.SelectedIndex = 0;

        ddlCurrInsuredValue.DataSource = ds;
        ddlCurrInsuredValue.DataTextField = "Curr";
        ddlCurrInsuredValue.DataValueField = "Curr";
        ddlCurrInsuredValue.DataBind();
        ddlCurrInsuredValue.Items.Insert(0, new ListItem("< Select >", ""));
        ddlCurrInsuredValue.SelectedIndex = 0;

        //ddlCurrPremium.DataSource = ds;
        //ddlCurrPremium.DataTextField = "Curr";
        //ddlCurrPremium.DataValueField = "Curr";
        //ddlCurrPremium.DataBind();
        //ddlCurrPremium.Items.Insert(0, new ListItem("< Select >", "0"));
        //ddlCurrPremium.SelectedIndex = 0;

        //   
    }
    public string FormatCurrency(object InValue)
    {
        string DecimalValue = "";
        string StrIn = InValue.ToString();
         int Index = StrIn.IndexOf('.');
        if (Index != -1)
            DecimalValue = StrIn.Substring(Index);
        if (DecimalValue != "")
            DecimalValue = DecimalValue.PadRight(3, '0').Substring(0, 3);

        if(StrIn.IndexOf('.')!=-1)
            StrIn = StrIn.Substring(0, StrIn.IndexOf('.'));
        StrIn = Convert.ToInt64(StrIn).ToString();
        string OutValue = "";
        int Len = StrIn.Length;
        while (Len > 3)
        {
            if (OutValue.Trim() == "")
                OutValue = StrIn.Substring(Len - 3);
            else
                OutValue = StrIn.Substring(Len - 3) + "," + OutValue;

            StrIn = StrIn.Substring(0, Len - 3);
            Len = StrIn.Length;
        }
        OutValue = StrIn + "," + OutValue;
        if (OutValue.EndsWith(","))
        {
            OutValue = OutValue.Substring(0, OutValue.Length - 1);
        }
        return OutValue + ((DecimalValue == "") ? ".00" : DecimalValue);
    }
    public string FormatCurrency3Decimal(object InValue)
    {
        string DecimalValue = "";
        string StrIn = InValue.ToString();
        int Index = StrIn.IndexOf('.');
        if (Index != -1)
            DecimalValue = StrIn.Substring(Index);
        if (DecimalValue!="")
            DecimalValue=DecimalValue.PadRight(4, '0').Substring(0, 4);

        StrIn = Common.CastAsInt32(StrIn).ToString();
        string OutValue = "";
        int Len = StrIn.Length;
        while (Len > 3)
        {
            if (OutValue.Trim() == "")
                OutValue = StrIn.Substring(Len - 3);
            else
                OutValue = StrIn.Substring(Len - 3) + "," + OutValue;

            StrIn = StrIn.Substring(0, Len - 3);
            Len = StrIn.Length;
        }
        OutValue = StrIn + "," + OutValue;
        if (OutValue.EndsWith(","))
        {
            OutValue = OutValue.Substring(0, OutValue.Length - 1);
        }
        return OutValue + ((DecimalValue == "") ? ".000" : DecimalValue);
    }
    public string CurrencyConversion(decimal Amount,DropDownList DDLCurr)
    {
        DataSet ds = new DataSet();

        // To run on client side
        try
        {
            string sql = "select top 1 exc_rate from DBO.XCHANGEDAILY where RateDate<='" + DateTime.Today.ToString("dd-MMM-yyyy") + "' and For_Curr='" + DDLCurr.SelectedValue + "' order by RateDate desc";
            ds = Budget.getTable(sql);
        }
        catch (Exception ex)
        {
            //To run on my side
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["eMANAGER"].ToString();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = myConnection;
            SqlDataAdapter Adp = new SqlDataAdapter();
            string sql = "select top 1 exc_rate from DBO.XCHANGEDAILY where RateDate<='" + DateTime.Today.ToString("dd-MMM-yyyy") + "' and For_Curr='" + DDLCurr.SelectedValue + "' order by RateDate desc";
            Adp.SelectCommand = cmd;
            cmd.CommandText = sql;
            Adp.Fill(ds, "tbl");
        }


        if (ds != null)
        {
            DataTable Dt = ds.Tables[0];
            decimal CurrRate = 0;
            if (Dt != null)
            {
                if (Dt.Rows.Count > 0)
                {
                    CurrRate = Math.Round(Common.CastAsDecimal(Dt.Rows[0][0]), 4);
                }
            }
            if (CurrRate != 0)
                return Convert.ToString(Math.Round( (Convert.ToDecimal(Amount) / CurrRate),4));
            else
                return "0";
        }
        return "0";
    }
    #endregion -------------------------------------------------

    #region -------------- EVENTS ---------------------------------

    protected void ddl_Groups_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_Groups.SelectedValue == "9")
        {
            tr_Deductible.Visible = false;
            tr_HM.Visible = true;
            tr_PNI.Visible = false;
        }
        else if (ddl_Groups.SelectedValue == "7")
        {
            tr_Deductible.Visible = false;
            tr_HM.Visible = false;
            tr_PNI.Visible = true;            
        }        
        else
        {
            tr_Deductible.Visible = true;
            tr_HM.Visible = false;
            tr_PNI.Visible = false;            
        }        
    }
    protected void ddl_Vessels_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_Vessels.SelectedIndex != 0)
        {
            string SQL = "SELECT FS.FlagStateName, VM.YearBuilt, VM.LDT ,VT.VesselTypeName FROM dbo.Vessel VM " +
                         "INNER JOIN dbo.VesselType VT ON VT.VesselTypeId = VM.VesselTypeId " +
                         "INNER JOIN dbo.FlagState FS ON FS.FlagStateId = VM.FlagStateId " +
                         "WHERE  VM.VesselId =" + ddl_Vessels.SelectedValue.Trim();
            DataTable dt = Budget.getTable(SQL).Tables[0];
            if (dt.Rows.Count > 0)
            {
                lbl_VesselType.Text = dt.Rows[0]["VesselTypeName"].ToString();
                lbl_Flag.Text = dt.Rows[0]["FlagStateName"].ToString();
                lbl_YearBuild.Text = dt.Rows[0]["YearBuilt"].ToString();
                lbl_GrossTonnage.Text = dt.Rows[0]["LDT"].ToString();
            }
            else
            {
                lbl_VesselType.Text = "";
                lbl_Flag.Text = "";
                lbl_YearBuild.Text = "";
                lbl_GrossTonnage.Text = "";
            }
        }
        else
        {
            lbl_VesselType.Text = "";
            lbl_Flag.Text = "";
            lbl_YearBuild.Text = "";
            lbl_GrossTonnage.Text = "";
        }
    }
    protected void ddlBroker_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBroker.SelectedValue == "1")
        {
            txtBrokername.Visible = true;
        }
        else
        {
            txtBrokername.Text = "";
            txtBrokername.Visible = false;
        }
    }
    protected void txt_ExpiryDt_TextChanged(object sender, EventArgs e)
    {
        if (txt_InceptionDt.Text.Trim() != "" && txt_ExpiryDt.Text.Trim() != "")
        {
            DateTime dt;
            if (!DateTime.TryParse(txt_InceptionDt.Text.Trim(), out dt) && !DateTime.TryParse(txt_ExpiryDt.Text.Trim(), out dt))
            {
                lbl_InsurancePeriod.Text = "";

            }
            else
            {
                DateTime fromDt = DateTime.Parse(txt_InceptionDt.Text.Trim().Split(' ').GetValue(0).ToString() + " " + ddlFromHour.SelectedItem.Text + " : " + ddlFromMin.SelectedItem.Text);
                DateTime ToDt = DateTime.Parse(txt_ExpiryDt.Text.Trim().Split(' ').GetValue(0).ToString() + " " + ddlToHour.SelectedItem.Text + " : " + ddlToMin.SelectedItem.Text);

                TimeSpan ts = ToDt.Subtract(fromDt);
                lbl_InsurancePeriod.Text = ts.Days + " Days " + ts.Hours + " Hours And " + ts.Minutes + " Minutes From " + ddlFromHour.SelectedItem.Text + " : " + ddlFromMin.SelectedItem.Text + " " + txt_InceptionDt.Text.Trim() + " To " + ddlToHour.SelectedItem.Text + " : " + ddlToMin.SelectedItem.Text + " " + txt_ExpiryDt.Text;

            }
        }
        else
        {
            lbl_InsurancePeriod.Text = "";
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (!IsValidated())
        {
            return;
        }

        if (Request.QueryString["Mode"].ToString() != "E")
        {
            string strDuplicateCheck = "SELECT * FROM IRM_PolicyMaster WHERE PolicyNo = '" + txt_PolicyNo.Text.Trim() + "' AND VesselId = " + ddl_Vessels.SelectedValue.Trim() + " AND GroupId = " + ddl_Groups.SelectedValue.Trim() + " AND UWId = " + ddl_UW.SelectedValue.Trim() + " ";
            DataTable dtDuplicate = Budget.getTable(strDuplicateCheck).Tables[0];
            if (dtDuplicate.Rows.Count > 0)
            {
                lblMessage.Text = "Policy already exists.";
                return;
            }
        }

        string inceptionDt;
        if(txt_InceptionDt.Text.Trim() != "")
        {
            inceptionDt = txt_InceptionDt.Text.Trim() + " " + ddlFromHour.SelectedValue + ":" + ddlFromMin.SelectedValue;
        }
        else
        {
            inceptionDt = "";
        }
        string ExpiryDt;
        if (txt_ExpiryDt.Text.Trim() != "")
        {
            ExpiryDt = txt_ExpiryDt.Text.Trim() + " " + ddlToHour.SelectedValue + ":" + ddlToMin.SelectedValue;
        }
        else
        {
            ExpiryDt = "";
        }
        decimal deductibles ,Hull, Machinery, HM, Cargo, Crew, Others;
        if (ddl_Groups.SelectedValue == "9")
        {
            deductibles = 0;
            if (txt_Hull.Text.Trim() != "")
            {
                Hull = Common.CastAsDecimal(txt_Hull.Text.Trim().Replace(",", "").Replace("$", "").Trim());
            }
            else
            {
                Hull = 0;
            }
            if (txt_Machinery.Text.Trim() != "")
            {
                Machinery = Common.CastAsDecimal(txt_Machinery.Text.Trim().Replace(",", "").Replace("$", "").Trim());
            }
            else
            {
                Machinery = 0;
            }
            if (txt_HM.Text.Trim() != "")
            {
                HM = Common.CastAsDecimal(txt_HM.Text.Trim().Replace(",", "").Replace("$", "").Trim());
            }
            else
            {
                HM = 0;
            }
            Cargo = 0;
            Crew = 0;
            Others = 0;
        }
        else if (ddl_Groups.SelectedValue == "7")
        {
            deductibles = 0;
            Hull = 0;
            Machinery = 0;
            HM = 0;
            if (txt_Cargo.Text.Trim() != "")
            {
                Cargo = Common.CastAsDecimal(txt_Cargo.Text.Trim().Replace(",", "").Replace("$", "").Trim());
            }
            else
            {
                Cargo = 0;
            }
            if (txt_Crew.Text.Trim() != "")
            {
                Crew = Common.CastAsDecimal(txt_Crew.Text.Trim().Replace(",", "").Replace("$", "").Trim());
            }
            else
            {
                Crew = 0;
            }
            if (txt_Others.Text.Trim() != "")
            {
                Others = Common.CastAsDecimal(txt_Others.Text.Trim().Replace(",", "").Replace("$", "").Trim());
            }
            else
            {
                Others = 0;
            }
        }
        else
        {
            if (txt_Deductible.Text.Trim() != "")
            {
                deductibles = Common.CastAsDecimal(txt_Deductible.Text.Trim().Replace(",", "").Replace("$", "").Trim());
            }
            else
            {
                deductibles = 0;
            }
            Hull = 0;
            Machinery = 0;
            HM = 0;
            Cargo = 0;
            Crew = 0;
            Others = 0;
        }
        string InsuredValue = (txt_InsuredAmount.Text.Trim().Replace(",", "").Replace("$", "").Trim() == "") ? "0" : txt_InsuredAmount.Text.Trim().Replace(",", "").Replace("$", "").Trim();
        string Rate = (txt_Rate.Text.Trim().Replace(",", "").Replace("$", "").Trim() == "") ? "0" : txt_Rate.Text.Trim().Replace(",", "").Replace("$", "").Trim();
        string Premium = (txt_Premium.Text.Trim().Replace(",", "").Replace("$", "").Trim() == "") ? "0" : txt_Premium.Text.Trim().Replace(",", "").Replace("$", "").Trim();

        if (Request.QueryString["Mode"].ToString() != "E")
        {

            string SQL = "EXEC sp_IRM_InsertPolicies '" + txt_PolicyNo.Text.Trim() + "'," + ddl_Vessels.SelectedValue + ",'" + txt_IssuedDt.Text.Trim() + "','" + txt_IssuedPlace.Text.Trim() + "','" + inceptionDt + "','" + ExpiryDt + "', " + InsuredValue + ","+ddlPaymentByMTM.SelectedValue+","+((chkMortagee.Checked)?"1":"0")+"," + Rate + "," + deductibles + ",'" + Premium + "'," + ddlArrangedBy.SelectedValue + "," + ddlBroker.SelectedValue + ",'" + txtBrokername.Text.Trim() + "'," + ddl_UW.SelectedValue + "," + ddl_Groups.SelectedValue + ",'" + string.Empty + "','" + txt_AssuredS.Text.Trim() + "','" + string.Empty + "'," + Hull + "," + Machinery + "," + HM + "," + Cargo + "," + Crew + "," + Others + "," + ddlRDC.SelectedValue.Trim() + ",'" + ddlCurrInsuredValue.SelectedValue + "'," + Common.CastAsDecimal(txt_InsuredAmountUSDoler.Text) + ",'" + ddlCurrInsuredValue.SelectedValue + "',0," + Common.CastAsDecimal(txtPremiumUSDoller.Text) + ",'" + ddlCurrInsuredValue.SelectedValue + "'," + Common.CastAsDecimal(txt_DeductibleDoller.Text) + " ," + Common.CastAsDecimal(txtHalLC.Text )+ "," + Common.CastAsDecimal(txtMachineryLC.Text )+ "," + Common.CastAsDecimal(txtHMLC.Text )+ "," + Common.CastAsDecimal(txtCargoLC.Text )+ "," + Common.CastAsDecimal(txtCrewLC.Text )+ "," + Common.CastAsDecimal(txtOthersLC.Text )+ "";
            DataTable dt = Budget.getTable(SQL).Tables[0];
            if (dt.Rows.Count > 0)
            {
                PolicyId = Convert.ToInt32(dt.Rows[0]["PolicyId"].ToString());
                if (rptInstallment.Items.Count > 0)
                {
                    foreach (RepeaterItem it in rptInstallment.Items)
                    {
                        TextBox txtInstDetails = (TextBox)it.FindControl("txtInstDetails");

                        TextBox txtAmount = (TextBox)it.FindControl("txtAmount");
                        TextBox txtAmountLC = (TextBox)it.FindControl("txtAmountLC");
                        TextBox txtInstDate = (TextBox)it.FindControl("txtInstDate");

                        string AmountLC = (txtAmountLC.Text.Trim().Replace(",", "").Replace("$", "").Trim() == "") ? "0" : txtAmountLC.Text.Trim().Replace(",", "").Replace("$", "").Trim();
                        string Amount = (txtAmount.Text.Trim().Replace(",", "").Replace("$", "").Trim() == "") ? "0" : txtAmount.Text.Trim().Replace(",", "").Replace("$", "").Trim();

                        string strSQL = "EXEC sp_IRM_InsertPolicyDetails " + dt.Rows[0]["PolicyId"].ToString() + ",'" + txtInstDetails.Text.Trim() + "','" + AmountLC + "','" + Amount + "','" + txtInstDate.Text.Trim() + "' ";
                        DataTable dtDetails = Budget.getTable(strSQL).Tables[0];
                        if (dtDetails.Rows.Count > 0)
                        {
                            lblMessage.Text = "Record Successfully Saved.";
                        }
                    }
                }
                else
                {
                    lblMessage.Text = "Record Successfully Saved.";
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "tr", "refreshparent();", true);
            }
            else
            {
                lblMessage.Text = "Transaction Failed.";
            }
        }
        else
        {
            string SQL = "EXEC sp_IRM_UpdatePolicies " + PolicyId + ",'" + txt_PolicyNo.Text.Trim() + "'," + ddl_Vessels.SelectedValue + ",'" + txt_IssuedDt.Text.Trim() + "','" + txt_IssuedPlace.Text.Trim() + "','" + inceptionDt + "','" + ExpiryDt + "', " + InsuredValue + "," + ddlPaymentByMTM.SelectedValue + "," + ((chkMortagee.Checked) ? "1" : "0") + "," + Rate + "," + deductibles + ",'" + Premium + "'," + ddlArrangedBy.SelectedValue + "," + ddlBroker.SelectedValue + ",'" + txtBrokername.Text.Trim() + "'," + ddl_UW.SelectedValue + "," + ddl_Groups.SelectedValue + ",'" + string.Empty + "','" + txt_AssuredS.Text.Trim() + "','" + string.Empty + "'," + Hull + "," + Machinery + "," + HM + "," + Cargo + "," + Crew + "," + Others + "," + ddlRDC.SelectedValue.Trim() + ",'" + ddlCurrInsuredValue.SelectedValue + "'," + Common.CastAsDecimal(txt_InsuredAmountUSDoler.Text) + ",'" + ddlCurrInsuredValue.SelectedValue + "',0," + Common.CastAsDecimal(txtPremiumUSDoller.Text) + ",'" + ddlCurrInsuredValue.SelectedValue + "'," + Common.CastAsDecimal(txt_DeductibleDoller.Text) + " ," + Common.CastAsDecimal(txtHalLC.Text) + "," + Common.CastAsDecimal(txtMachineryLC.Text) + "," + Common.CastAsDecimal(txtHMLC.Text) + "," + Common.CastAsDecimal(txtCargoLC.Text) + "," + Common.CastAsDecimal(txtCrewLC.Text) + "," + Common.CastAsDecimal(txtOthersLC.Text) + "";
            DataTable dt = Budget.getTable(SQL).Tables[0];
            if (dt.Rows.Count > 0)
            {
                if (rptInstallment.Items.Count > 0)
                {
                    foreach (RepeaterItem it in rptInstallment.Items)
                    {
                        TextBox txtInstDetails = (TextBox)it.FindControl("txtInstDetails");
                        TextBox txtAmount = (TextBox)it.FindControl("txtAmount");
                        TextBox txtAmountLC = (TextBox)it.FindControl("txtAmountLC");
                        TextBox txtInstDate = (TextBox)it.FindControl("txtInstDate");

                        string AmountLC = (txtAmountLC.Text.Trim().Replace(",", "").Replace("$", "").Trim() == "") ? "0" : txtAmountLC.Text.Trim().Replace(",", "").Replace("$", "").Trim();
                        string Amount = (txtAmount.Text.Trim().Replace(",", "").Replace("$", "").Trim() == "") ? "0" : txtAmount.Text.Trim().Replace(",", "").Replace("$", "").Trim();

                        string strSQL = "EXEC sp_IRM_InsertPolicyDetails " + PolicyId + ",'" + txtInstDetails.Text.Trim() + "','" + AmountLC + "','" + Amount + "','" + txtInstDate.Text.Trim() + "' ";
                        DataTable dtDetails = Budget.getTable(strSQL).Tables[0];
                        if (dtDetails.Rows.Count > 0)
                        {
                            lblMessage.Text = "Record Successfully Saved.";
                            
                        }
                        
                    }
                }
                else
                {
                    lblMessage.Text = "Record Successfully Saved.";
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "tr", "refreshparent();", true);
            }
            else
            {
                lblMessage.Text = "Transaction Failed.";
            }
        }
     }

    protected void btnShowSchedule_Click(object sender, EventArgs e)
    {
        if (txtNoInstallment.Text == "")
        {
            lblMessage.Text = "Please enter no. of installments.";
            txtNoInstallment.Focus();
            return;
        }

        DataTable dt = new DataTable();
        DataRow dr = null;
        dt.Columns.Add("InstallmentDetails");
        dt.Columns.Add("InstallmentDt");
        dt.Columns.Add("Amount");
        dt.Columns.Add("AmountLC");
        int Noinstallment = Convert.ToInt32(txtNoInstallment.Text.Trim());

        for (int i = 1; i < Noinstallment + 1; i++)
        {
            dr = dt.NewRow();
            dt.Rows.Add(dr);
            dt.Rows[dt.Rows.Count - 1][0] = "";
            dt.Rows[dt.Rows.Count - 1][1] = "";
            dt.Rows[dt.Rows.Count - 1][2] = ""; //(ddlArrangedBy.SelectedValue == "1") ? "" : "As Arranged";
            dt.Rows[dt.Rows.Count - 1][3] = ""; //(ddlArrangedBy.SelectedValue == "1") ? "" : "As Arranged";
        }

        rptInstallment.DataSource = dt;
        rptInstallment.DataBind();


    }
    protected void btnSaveDoc_Click(object sender, EventArgs e)
    {
        if (PolicyId == 0)
        {
            lblMessage.Text = "Please save the policy first.";
            return;
        }
        if (txt_DocNo.Text == "")
        {
            lblMessage.Text = "Please enter doc no.";
            txt_DocNo.Focus();
            return;
        }
        if (txt_DocName.Text == "")
        {
            lblMessage.Text = "Please enter doc name.";
            txt_DocName.Focus();
            return;
        }
        if (!flAttachDocs.HasFile)
        {
            lblMessage.Text = "Please select a file.";
            flAttachDocs.Focus();
            return;
        }
        FileUpload img = (FileUpload)flAttachDocs;
        Byte[] imgByte = new Byte[0];
        string FileName = "";

        if (img.HasFile && img.PostedFile != null)
        {
            HttpPostedFile File = flAttachDocs.PostedFile;

            if (Path.GetExtension(File.FileName).ToString() == ".pdf" || Path.GetExtension(File.FileName).ToString() == ".txt" || Path.GetExtension(File.FileName).ToString() == ".doc" || Path.GetExtension(File.FileName).ToString() == ".docx" || Path.GetExtension(File.FileName).ToString() == ".xls" || Path.GetExtension(File.FileName).ToString() == ".xlsx" || Path.GetExtension(File.FileName).ToString() == ".ppt" || Path.GetExtension(File.FileName).ToString() == ".pptx")
            {
                //FileName = txt_PolicyNo.Text.Trim() + "_" + Path.GetFileName(File.FileName);
                FileName = txt_DocName.Text.Trim() + "_" + DateTime.Now.ToString("dd-MMM-yyyy") + "_" + DateTime.Now.TimeOfDay.ToString().Replace(":", "-").ToString() + Path.GetExtension(File.FileName);
                imgByte = new Byte[File.ContentLength];
                File.InputStream.Read(imgByte, 0, File.ContentLength);
                string path = "/EMANAGERBLOB/LPSQE/Insurance/";
                flAttachDocs.SaveAs(Server.MapPath(path) + FileName);

                string strSQL = "EXEC sp_IRM_InsertPolicieDocuments " + PolicyId + ",'" + txt_DocNo.Text.Trim() + "','" + txt_DocName.Text.Trim() + "','" + FileName.Trim() + "','" + imgByte + "' ";
                DataTable dtDocs = Budget.getTable(strSQL).Tables[0];
                if (dtDocs.Rows.Count > 0)
                {
                    lblMessage.Text = "Record Successfully Saved.";
                    BindDocs();
                }
            }
            else
            {
                lblMessage.Text = "File type not supported. Only pdf and microsoft ofiice files accepted.";
            }

            
        }
    }
    protected void ddlArrangedBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddlArrangedBy.SelectedValue == "2")
        //{
        //    txt_Premium.Text = "As arranged";
        //}
        //else
        //{
        //    txt_Premium.Text = "";
        //}
    }

    protected void btnDeleteDoc_Click(object sender, EventArgs e)
    {
        ImageButton btnDeleteDoc = (ImageButton)sender;
        int DocId = Common.CastAsInt32(btnDeleteDoc.CommandArgument);

        string strSQL = "SELECT [FileName] FROM IRM_PolicyDocuments WHERE DocId = " + DocId;
        DataTable dtFileName = Budget.getTable(strSQL).Tables[0];
        string DocFile = dtFileName.Rows[0]["FileName"].ToString();

        string strDelete = "DELETE FROM IRM_PolicyDocuments WHERE DocId = " + DocId + " ; SELECT -1";
        DataTable dtDelete =  Budget.getTable(strDelete).Tables[0];
        if(dtDelete.Rows.Count > 0)
        {
            string path = "/EMANAGERBLOB/LPSQE/Insurance/";
            System.IO.File.Delete(Server.MapPath(path) + DocFile);
        }
        BindDocs();
    }

    #endregion -------------------------------------------------

    #region ---------- TEXT CHANGE -----------------------------
    
    //protected void txt_Rate_TextChanged(object sender, EventArgs e)
    //{
    //    if (txt_Rate.Text.Trim() != "")
    //    {
    //        decimal amount = Common.CastAsDecimal(txt_Rate.Text.Trim());
    //        txt_Rate.Text = string.Format("{0:C}", amount).Trim().Replace("Rs.", "").Trim();
    //    }

    //}
    //protected void txt_Premium_TextChanged(object sender, EventArgs e)
    //{
    //    if (txt_Premium.Text.Trim() != "")
    //    {
    //        decimal amount = Common.CastAsDecimal(txt_Premium.Text.Trim());
    //        txt_Premium.Text = string.Format("{0:C}", amount).Trim().Replace("Rs.", "").Trim();
    //    }

    //}
    //protected void txt_Deductible_TextChanged(object sender, EventArgs e)
    //{
    //    if (txt_Deductible.Text.Trim() != "")
    //    {
    //        decimal amount = Common.CastAsDecimal(txt_Deductible.Text.Trim());
    //        txt_Deductible.Text = string.Format("{0:C}", amount).Trim().Replace("Rs.", "").Trim();
    //    }

    //}
    protected void txt_Hull_TextChanged(object sender, EventArgs e)
    {
        if (txt_Hull.Text.Trim() != "")
        {
            decimal amount = Common.CastAsDecimal(txt_Hull.Text.Trim());
            txt_Hull.Text = string.Format("{0:C}", amount).Trim().Replace("Rs.", "").Trim();
        }

    }
    protected void txt_Machinery_TextChanged(object sender, EventArgs e)
    {
        if (txt_Machinery.Text.Trim() != "")
        {
            decimal amount = Common.CastAsDecimal(txt_Machinery.Text.Trim());
            txt_Machinery.Text = string.Format("{0:C}", amount).Trim().Replace("Rs.", "").Trim();
        }

    }
    protected void txt_HM_TextChanged(object sender, EventArgs e)
    {
        if (txt_HM.Text.Trim() != "")
        {
            decimal amount = Common.CastAsDecimal(txt_HM.Text.Trim());
            txt_HM.Text = string.Format("{0:C}", amount).Trim().Replace("Rs.", "").Trim();
        }

    }
    protected void txt_Cargo_TextChanged(object sender, EventArgs e)
    {
        if (txt_Cargo.Text.Trim() != "")
        {
            decimal amount = Common.CastAsDecimal(txt_Cargo.Text.Trim());
            txt_Cargo.Text = string.Format("{0:C}", amount).Trim().Replace("Rs.", "").Trim();
        }

    }
    protected void txt_Crew_TextChanged(object sender, EventArgs e)
    {
        if (txt_Crew.Text.Trim() != "")
        {
            decimal amount = Common.CastAsDecimal(txt_Crew.Text.Trim());
            txt_Crew.Text = string.Format("{0:C}", amount).Trim().Replace("Rs.", "").Trim();
        }

    }
    protected void txt_Others_TextChanged(object sender, EventArgs e)
    {
        if (txt_Others.Text.Trim() != "")
        {
            decimal amount = Common.CastAsDecimal(txt_Others.Text.Trim());
            txt_Others.Text = string.Format("{0:C}", amount).Trim().Replace("Rs.", "").Trim();
        }

    }
    protected void txtAmount_TextChanged(object sender, EventArgs e)
    {
        TextBox txtAmount = (TextBox)sender;
        decimal amount = Common.CastAsDecimal(txtAmount.Text.Trim());
        txtAmount.Text = string.Format("{0:C}", amount).Trim().Replace("Rs.", "").Trim();
    }

    protected void txttxtAmountLC_OnTextChanged(object sender, EventArgs e)
    {
        TextBox txtAmountLC = (TextBox)sender;
        TextBox txtAmount = (TextBox)txtAmountLC.Parent.FindControl("txtAmount");
        txtAmount .Text= CurrencyConversion(Common.CastAsDecimal(txtAmountLC.Text), ddlCurrInsuredValue);
    }


    //-- H&M Group
    protected void txtHalLC_TextChanged(object sender, EventArgs e)
    {
        txt_Hull.Text = CurrencyConversion(Common.CastAsDecimal(txtHalLC.Text), ddlCurrInsuredValue);
        
    }
    protected void txtMachineryLC_TextChanged(object sender, EventArgs e)
    {
        txt_Machinery.Text = CurrencyConversion(Common.CastAsDecimal(txtMachineryLC.Text), ddlCurrInsuredValue);
    }
    protected void txtHMLC_TextChanged(object sender, EventArgs e)
    {

        txt_HM.Text = CurrencyConversion(Common.CastAsDecimal(txtHMLC.Text), ddlCurrInsuredValue);
    }
    
    //-- H&M Group
    protected void txtCargoLC_TextChanged(object sender, EventArgs e)
    {
        txt_Cargo.Text = CurrencyConversion(Common.CastAsDecimal(txtCargoLC.Text), ddlCurrInsuredValue);
    }
    protected void txtCrewLC_TextChanged(object sender, EventArgs e)
    {
        txt_Crew.Text = CurrencyConversion(Common.CastAsDecimal(txtCrewLC.Text), ddlCurrInsuredValue);
    }
    protected void txtOthersLC_TextChanged(object sender, EventArgs e)
    {
        txt_Others.Text = CurrencyConversion(Common.CastAsDecimal(txtOthersLC.Text), ddlCurrInsuredValue);
    }
    
    
    // For Currency
    protected void txt_InsuredAmount_TextChanged(object sender, EventArgs e)
    {
        //if (txt_InsuredAmount.Text.Trim() != "")
        //{
        //    decimal amount = Common.CastAsDecimal(txt_InsuredAmount.Text.Trim());
        //    txt_InsuredAmount.Text = string.Format("{0:C}", amount).Trim().Replace("Rs.", "").Trim();
        //}
        //txt_InsuredAmountUSDoler.Text = CurrencyConversion(Common.CastAsDecimal(txt_InsuredAmount.Text), ddlCurrInsuredValue);
        txt_Rate_TextChanged(sender, e);
    }
    protected void txt_Rate_TextChanged(object sender, EventArgs e)
    {
        //txtRateUSDoller.Text = CurrencyConversion(Common.CastAsDecimal(txt_Rate.Text), ddlCurrInsuredValue);
        txt_Rate.Text = FormatCurrency3Decimal(txt_Rate.Text);
        txt_Premium.Text = Convert.ToString( FormatCurrency((Common.CastAsDecimal(txt_InsuredAmount.Text) * Common.CastAsDecimal(txt_Rate.Text)) / 100));
        //txtPremiumUSDoller.Text = CurrencyConversion(Common.CastAsDecimal(txt_Premium.Text), ddlCurrInsuredValue);
    }
    protected void txt_Premium_TextChanged(object sender, EventArgs e)
    {
        txtPremiumUSDoller.Text = CurrencyConversion(Common.CastAsDecimal(txt_Premium.Text), ddlCurrInsuredValue);
    }
    protected void txt_Deductible_TextChanged(object sender, EventArgs e)
    {
        txt_DeductibleDoller.Text = CurrencyConversion(Common.CastAsDecimal(txt_Deductible.Text), ddlCurrInsuredValue);
    }

    protected void ddlCurrInsuredValue_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        //txt_InsuredAmountUSDoler.Text = CurrencyConversion(Common.CastAsDecimal(txt_InsuredAmount.Text), ddlCurrInsuredValue);
        txt_Rate_TextChanged(sender, e);
        txt_Deductible_TextChanged(sender, e);

        foreach (RepeaterItem Itm in rptInstallment.Items)
        {
            TextBox txtAmount = (TextBox)Itm.FindControl("txtAmount");
            txtAmount.Text = txtAmount.Text.Replace("$","");
            TextBox txtAmountLC = (TextBox)Itm.FindControl("txtAmountLC");
            txtAmountLC.Text = txtAmountLC.Text.Replace("$", "");
            txtAmount.Text = CurrencyConversion(Common.CastAsDecimal(txtAmountLC.Text), ddlCurrInsuredValue);
        }
    }
    protected void ddlCurrPremium_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        //txtRateUSDoller.Text = CurrencyConversion(Common.CastAsDecimal(txt_Rate.Text), ddlCurrInsuredValue);
        txtPremiumUSDoller.Text = CurrencyConversion(Common.CastAsDecimal(txt_Premium.Text), ddlCurrInsuredValue);
    }
    protected void ddlCurrDeductible_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        txt_DeductibleDoller.Text = CurrencyConversion(Common.CastAsDecimal(txt_Deductible.Text), ddlCurrInsuredValue);
    }
    #endregion
}
 