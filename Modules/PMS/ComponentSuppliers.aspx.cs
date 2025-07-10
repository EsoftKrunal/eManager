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

public partial class ComponentSuppliers : System.Web.UI.Page
{
    public string VesselCode
    {
        set { ViewState["VC"] = value; }
        get { return ViewState["VC"].ToString(); }
    }
    public string CompCode 
    {
        set { ViewState["CC"] = value; }
        get { return ViewState["CC"].ToString(); }
    }
    public int CompId
    {
        set { ViewState["CI"] = value; }
        get { return Common.CastAsInt32(ViewState["CI"]); }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        // -------------------------- SESSION CHECK ----------------------------------------------
        ProjectCommon.SessionCheck();
        // -------------------------- SESSION CHECK END ----------------------------------------------
        lblMessage.Text = "";                
        if (!Page.IsPostBack)
        {
            VesselCode = Request.QueryString["vsl"].ToString();
            lblSPVesselCode.Text = VesselCode;
            CompCode = Request.QueryString["ccode"].ToString();
            CompId = 0;
            ShowComponentDetails();
            ShowAddedSuppliers();
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        int _supplierid = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        string sql = " DELETE FROM VSL_VesselSpareMaster_Suppliers WHERE VesselCode='" + VesselCode + "' AND ComponentId=" + CompId + " AND SupplierID = " + _supplierid;
        Common.Execute_Procedures_Select_ByQuery(sql);
        ShowAddedSuppliers();
    }
    public void ShowComponentDetails()
    {
        DataTable dtSpec;
        string strSpecSQL = "SELECT CM.ComponentId,CM.ComponentCode ,(Select LTRIM(RTRIM(CM1.ComponentCode)) + ' - ' + CM1.ComponentName from ComponentMaster CM1 WHERE LEN(CM1.ComponentCode)= (LEN('" + CompCode + "')-3) AND  LEFT(CM1.ComponentCode,(LEN('" + CompCode + "')-3)) = LEFT('" + CompCode + "',(LEN('" + CompCode + "')-3)))As LinkedTo,CM.ComponentName,CMV.Descr,CAST( ISNULL(CMV.ClassEquip,0) AS BIT) AS ClassEquip,CMV.ACCOUNTCODES,CM.CriticalEquip,CM.Inactive,CMV.Maker,CMV.MakerType,CMV.ClassEquipCode,CM.CriticalType FROM ComponentMaster CM " +
                            "INNER JOIN VSL_ComponentMasterForVessel CMV ON CM.ComponentId = CMV.ComponentId AND CMV.VesselCode = '" + VesselCode + "' " +
                            "WHERE CM.ComponentCode ='" + CompCode + "'";
        dtSpec = Common.Execute_Procedures_Select_ByQuery(strSpecSQL);
        if (dtSpec.Rows.Count > 0)
        {
            CompId = Common.CastAsInt32(dtSpec.Rows[0]["ComponentId"]);
            lblSPComponentCode.Text = dtSpec.Rows[0]["ComponentCode"].ToString().Trim();
            lblSPComponentName.Text = dtSpec.Rows[0]["ComponentName"].ToString();
            lblSPMaker.Text = dtSpec.Rows[0]["Maker"].ToString();
            lblSPMakerType.Text = dtSpec.Rows[0]["MakerType"].ToString();
            lblSPAccountCodes.Text = dtSpec.Rows[0]["ACCOUNTCODES"].ToString();
            lblSPComponentDesc.Text = dtSpec.Rows[0]["Descr"].ToString();
        }
        else
        {
        }
    }
    public void ShowAddedSuppliers()
    {
        string sql = " SElect Row_Number() over(order by SupplierName) as srno,* from  " +
                     " VSL_VesselSpareMaster_Suppliers S " +
                     " Left join dbo.VW_ALL_VENDERS AV on AV.SupplierID = S.SupplierID WHERE VESSELCODE='" + VesselCode + "' AND ComponentId=" + CompId.ToString();

        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
        rptAvailableVendor.DataSource = dt;
        rptAvailableVendor.DataBind();
    }


    //--Add Vendors------------------------------------------------------------------------------------------------------------------    
    protected void btnaddVendor_OnClick(object sender, EventArgs e)
    {
        divAddSuppliers.Visible = true;
        BindCountry();
    }
    protected void btnCloseAddVendorPopup_OnClick(object sender, EventArgs e)
    {
        divAddSuppliers.Visible = false;
        rptVendor.DataSource = null;
        rptVendor.DataBind();
    }
    protected void btnSaveVendor_OnClick(object sender, EventArgs e)
    {
        bool VendorSelected = false;
        foreach (RepeaterItem item in rptVendor.Items)
        {
            CheckBox chk = (CheckBox)item.FindControl("chkVendor");
            if (chk.Checked)
            {
                VendorSelected = true;
                break;
            }
        }
        if (!VendorSelected)
        {
            lblMsgAddVendor.Text = "Please select vendor to add";
            return;
        }

        foreach (RepeaterItem item in rptVendor.Items)
        {
            CheckBox chk = (CheckBox)item.FindControl("chkVendor");
            if (chk.Checked)
            {
                HiddenField hfSupplierID = (HiddenField)item.FindControl("hfSupplierID");
                string sql = " exec sp_IU_VSL_VesselSpareMaster_Suppliers '" + VesselCode + "'," + Common.CastAsInt32(CompId) + "," + Common.CastAsInt32(hfSupplierID.Value) + " ";
                Common.Execute_Procedures_Select_ByQuery(sql);
            }
        }
        lblMsgAddVendor.Text = " Vendor added successfully.";
        ShowAddedSuppliers();

    }
    

    protected void OnClick_imgSearch(object sender, EventArgs e)
    {
        string WhereClause = " where isnull(supplierid,0)>0 and active=1 ";

        if (txtVendor.Text.Trim() != "")
        {
            WhereClause = WhereClause + " and SupplierName like '" + txtVendor.Text.Trim() + "%'";
        }
        if (txtPort.Text.Trim() != "")
        {
            WhereClause = WhereClause + " and SupplierPort ='" + txtPort.Text.Trim() + "'";
        }
        if (ddlApprovalType.SelectedIndex>0)
        {
            WhereClause = WhereClause + " and SecondApporvalType =" + ddlApprovalType.SelectedValue;
        }
        if (ddlCountry.SelectedIndex != 0)
            WhereClause = WhereClause + " And CountryID =" + ddlCountry.SelectedValue + " ";

        if (txtcity.Text.Trim() != "")
            WhereClause = WhereClause + " And City_State like '%" + txtcity.Text.Trim() + "%' ";
        
        DataTable Dt = GetSuplierData("", "", WhereClause);
        if (Dt != null)
        {
            rptVendor.DataSource = Dt;
            rptVendor.DataBind();
            lblTotRec.Text = "Total Suppliers : " + Dt.Rows.Count;
        }
    }

    public DataTable GetSuplierData(string SortBy, string SortType, string WhereClause)
    {
        string sql = "SELECT Row_Number() over(order by SupplierName) as srno,* FROM dbo.VW_ALL_VENDERS " + WhereClause;
        DataTable DTVendor = Common.Execute_Procedures_Select_ByQuery(sql + " ORDER BY SupplierName ");
        return DTVendor;
    }
    public void BindCountry()
    {
        string sql = " select CountryID,CountryName from dbo.country where StatusID='A' order by CountryName  ";
        DataTable dtPR = Common.Execute_Procedures_Select_ByQuery(sql);
        ddlCountry.DataSource = dtPR;
        ddlCountry.DataTextField = "CountryName";
        ddlCountry.DataValueField = "CountryID";
        ddlCountry.DataBind();
        ddlCountry.Items.Insert(0, new ListItem("Select", ""));
    }
}

