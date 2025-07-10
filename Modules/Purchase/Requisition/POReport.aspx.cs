using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;

public partial class POReport : System.Web.UI.Page
{
    #region Declarations
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        //---------------------------------------
        ProjectCommon.SessionCheck();
        //---------------------------------------
        lblmsg.Text = "";
        if (!Page.IsPostBack)
        {
            BindCompany();
            BindVessel(ddlCompany.SelectedValue);
            BindAccount();
            BindVendor();
            BindYear();
        }
    }
    // Function   
    public void BindCompany()
    {
        string sql = "selECT cmp.Company, cmp.[Company Name] as CompanyName, cmp.Active, cmp.InAccts FROM vw_sql_tblSMDPRCompany cmp WHERE (((cmp.Active)='Y'))";
        DataTable DtCompany = Common.Execute_Procedures_Select_ByQuery(sql);
        if (DtCompany != null)
        {
            if (DtCompany.Rows.Count > 0)
            {
                ddlCompany.DataSource = DtCompany;
                ddlCompany.DataTextField = "CompanyName";
                ddlCompany.DataValueField = "Company";
                ddlCompany.DataBind();
                ddlCompany.Items.Insert(0, new ListItem("< Select >", ""));
            }

        }
        //if (Session["NWC"].ToString() == "Y")
        //{
        //    ddlCompany.SelectedValue = "NWC";
        //    ddlCompany.Enabled = false;
        //}

    }
    public void BindVessel(string Comp)
    {
        string sql = "SELECT vsl.ShipID, vsl.ShipName, vsl.Company, vsl.VesselNo, vsl.Active FROM vw_sql_tblSMDPRVessels vsl WHERE ((vsl.Active)='A' ) and vsl.Company='" + Comp + "' and vsl.VesselNo in (Select VesselId from UserVesselRelation with(nolock) where Loginid = "+ Convert.ToInt32(Session["loginid"].ToString())+")";
        DataTable DtVessel = Common.Execute_Procedures_Select_ByQuery(sql);
        if (DtVessel != null)
        {
            ddlVessel.DataSource = DtVessel;
            ddlVessel.DataTextField = "ShipName";
            ddlVessel.DataValueField = "ShipID";
            ddlVessel.DataBind();
        }
        ddlVessel.Items.Insert(0, new ListItem("< Select >", ""));    
    }
    public void BindVendor()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("select row_number() over (order by SupplierName) as Sno,SupplierId,SupplierName,SupplierPort,SupplierEmail,TravId from VW_tblSMDSuppliers Order By SupplierName"); //Where SupplierName like '" + txtVendor.Text + "%' 
        if (dt != null)
        {
            ddlVendor.DataSource = dt;
            ddlVendor.DataTextField = "SupplierName";
            ddlVendor.DataValueField = "SupplierId";
            ddlVendor.DataBind();
            ddlVendor.Items.Insert(0, new ListItem("< All Vendor >", "0"));
        }
    }
    public void BindAccount()
    {
        string sql = "SELECT Lk_tblSMDPRAccounts.AccountNumber, (convert(varchar, Lk_tblSMDPRAccounts.AccountNumber)+'   '+Lk_tblSMDPRAccounts.AccountName) as AccountName, Lk_tblSMDPRAccounts.AccountName FROM VW_sql_tblSMDPRAccounts as Lk_tblSMDPRAccounts WHERE Lk_tblSMDPRAccounts.Active='Y' ORDER BY Lk_tblSMDPRAccounts.AccountNumber";
        Common.Set_Procedures("ExecQuery");
        Common.Set_ParameterLength(1);
        Common.Set_Parameters(new MyParameter("@Query", sql));
        DataSet dsPrType = new DataSet();
        dsPrType.Clear();
        dsPrType = Common.Execute_Procedures_Select();

        ddlAccountCodeFrom.DataSource = dsPrType;
        ddlAccountCodeFrom.DataTextField = "AccountName";
        ddlAccountCodeFrom.DataValueField = "AccountNumber";
        ddlAccountCodeFrom.DataBind();
        ddlAccountCodeFrom.Items.Insert(0, new ListItem("< Select Account Code >", "0"));
        ddlAccountCodeFrom.SelectedIndex = 0;


        ddlAccountCodeTo.DataSource = dsPrType;
        ddlAccountCodeTo.DataTextField = "AccountName";
        ddlAccountCodeTo.DataValueField = "AccountNumber";
        ddlAccountCodeTo.DataBind();
        ddlAccountCodeTo.Items.Insert(0, new ListItem("< Select Account Code >", "9999"));
        ddlAccountCodeTo.SelectedIndex = 0;
    }
    public void BindYear()
    {
        for (int i = 2023; i <= System.DateTime.Now.Year; i++)
        {
            ddlFromyear.Items.Add(i.ToString());
            ddlToyear.Items.Add(i.ToString());
        }
        ddlFromyear.Items.Insert(0, new ListItem("Year", "0"));
        ddlFromyear.SelectedValue = DateTime.Today.Year.ToString();
        ddlFromMonth.SelectedValue = "1";

        ddlToyear.Items.Insert(0, new ListItem("Year", "0"));
        ddlToyear.SelectedValue = DateTime.Today.Year.ToString();
        ddlToMonth.SelectedValue = DateTime.Today.Month.ToString();   
    }
    protected void ddlCompany_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        BindVessel(ddlCompany.SelectedValue);
    }
    protected void rdoSearchBy_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdoSearchBy.SelectedIndex == 0)
        {
            tblVendor.Visible = false;
            tblAcc.Visible = false;

            ddlVendor.SelectedIndex = 0;
            ddlAccountCodeFrom.SelectedIndex = 0;
            ddlAccountCodeTo.SelectedIndex = 0;
        }
        else if (rdoSearchBy.SelectedIndex == 1)
        {
            tblVendor.Visible = true;
            tblAcc.Visible = false;
            
            ddlAccountCodeFrom.SelectedIndex = 0;
            ddlAccountCodeTo.SelectedIndex = 0;
        }
        else if (rdoSearchBy.SelectedIndex == 2)
        {
            tblVendor.Visible = false;
            tblAcc.Visible = true;

            
            ddlVendor.SelectedIndex = 0;
        }
        //else if (rdoSearchBy.SelectedIndex == 3)
        //{
        //    tblVendor.Visible = false;
        //    tblAcc.Visible = false;

        //    ddlVendor.SelectedIndex = 0;
        //    ddlAccountCodeFrom.SelectedIndex = 0;
        //    ddlAccountCodeTo.SelectedIndex = 0;
        //}
        //else if (rdoSearchBy.SelectedIndex == 4)
        //{
        //    tblVendor.Visible = false;
        //    tblAcc.Visible = false;

        //    ddlVendor.SelectedIndex = 0;
        //    ddlAccountCodeFrom.SelectedIndex = 0;
        //    ddlAccountCodeTo.SelectedIndex = 0;
        //}
        
    }
    protected void imgReport_OnClick(object sender, EventArgs e)
    {
        //validation
        if (ddlCompany.SelectedIndex == 0)
        {
            lblmsg.Text = "Please select the company.";
            ddlCompany.Focus();
            return;
        }
        if (ddlVessel.SelectedIndex == 0)
        {
            lblmsg.Text = "Please select the Vessel.";
            ddlVessel.Focus();
            return;
        }
        if (ddlFromyear.SelectedIndex == 0)
        {
            lblmsg.Text = "Please select form year.";
            ddlFromyear.Focus();
            return;
        }
        if (ddlToyear.SelectedIndex == 0)
        {
            lblmsg.Text = "Please select To year.";
            ddlToyear.Focus();
            return;
        }
        if (rdoReportBy.SelectedIndex == 0)
        {
            string Query = "Company=" + ddlCompany.SelectedValue + "&VesselID=" + ddlVessel.SelectedValue + "&RequestType=" + ddlPrtype.SelectedValue + "&Acc=" + ddlAccountCodeFrom.SelectedValue + "&Acc1=" + ddlAccountCodeTo.SelectedValue + "&SearchBy=" + rdoSearchBy.SelectedIndex + "&FromMonth=" + ddlFromMonth.SelectedValue + "&FromYear=" + ddlFromyear.SelectedValue + "&ToMonth=" + ddlToMonth.SelectedValue + "&ToYear=" + ddlToyear.SelectedValue + "&SupplierID=" + ddlVendor.SelectedValue + "&VSLName=" + ddlVessel.SelectedItem.Text+"&BreakDown="+((chkBreakDown.Checked)?"1":"0")+"";
            Ifram.Attributes.Add("src", "POReportCrystal.aspx?" + Query);
        }
        else
        {
            string Query = "Company=" + ddlCompany.SelectedValue + "&VesselID=" + ddlVessel.SelectedValue + "&RequestType=" + ddlPrtype.SelectedValue + "&Acc=" + ddlAccountCodeFrom.SelectedValue + "&Acc1=" + ddlAccountCodeTo.SelectedValue + "&SearchBy=" + rdoSearchBy.SelectedIndex + "&FromMonth=" + ddlFromMonth.SelectedValue + "&FromYear=" + ddlFromyear.SelectedValue + "&ToMonth=" + ddlToMonth.SelectedValue + "&ToYear=" + ddlToyear.SelectedValue + "&SupplierID=" + ddlVendor.SelectedValue + "&VSLName=" + ddlVessel.SelectedItem.Text + "&BreakDown=" + ((chkBreakDown.Checked) ? "1" : "0") + "";
            Ifram.Attributes.Add("src", "POReportSuplierListingCrystal.aspx?" + Query);
        }
    }
}
