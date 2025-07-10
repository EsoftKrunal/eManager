using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class AddNewVendor : System.Web.UI.Page
{
    public int SupplierID
    {
        get { return Common.CastAsInt32("0" + ViewState["SupplierID"]); }
        set { ViewState["SupplierID"] = value.ToString(); }
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        //---------------------------------------
        ProjectCommon.SessionCheck();
        //---------------------------------------
        lblmsg.Text = "";
        if (!Page.IsPostBack)
        {
            if (Page.Request.QueryString["SupplierID"] != null)
                SupplierID = Common.CastAsInt32(Page.Request.QueryString["SupplierID"]);
            if (Page.Request.QueryString["View"] != null)
            {
                btnSave.Visible = false;
            }
            BindTravVendor();

            if (SupplierID != 0)
                SetData();
        }
    }

    //Events ---------------------------------------------------------------------------------------
    protected void OnClick_btnSave(object sender, EventArgs e)
    {
        if (txtVendor.Text.Trim() == "")
        {
            lblmsg.Text = "Please enter vendor name.";
            txtVendor.Focus(); return ;
        }
        if (ddlTravVendor.SelectedIndex==0)
        {
            lblmsg.Text = "Please select traverse vendor.";
            ddlTravVendor.Focus(); return;
        }
        if (ddlMultiCurr.SelectedIndex == 0)
        {
            lblmsg.Text = "Please select multi currency.";
            ddlMultiCurr.Focus(); return;
        }

        if (txtPort.Text.Trim() == "")
        {
            lblmsg.Text = "Please enter port name.";
            txtPort.Focus(); return;
        }
        //if (txtTelephone.Text.Trim() == "")
        //{
        //    lblmsg.Text = "Please telephone number.";
        //    txtTelephone.Focus(); return;
        //}
        //if (txtFax.Text.Trim() == "")
        //{
        //    lblmsg.Text = "Please enter fax number.";
        //    txtFax.Focus(); return;
        //}
        //if (txtContact.Text.Trim() == "")
        //{
        //    lblmsg.Text = "Please enter contact number.";
        //    txtContact.Focus(); return;
        //}
        if (txtEmail.Text.Trim() == "")
        {
            lblmsg.Text = "Please enter email id.";
            txtEmail.Focus(); return;
        }
        if (ddlActive.SelectedIndex == 0)
        {
            lblmsg.Text = "Please select active.";
            ddlActive.Focus(); return;
        }
        if (ddlPreferred.SelectedIndex == 0)
        {
            lblmsg.Text = "Please select preferred.";
            ddlPreferred.Focus(); return;
        }
        
        Common.Set_Procedures("InsertIntotblSMDSuppliers");
        Common.Set_ParameterLength(18);
        Common.Set_Parameters
            (

                new MyParameter("@SupplierID", SupplierID.ToString()),
                new MyParameter("@SupplierName", txtVendor.Text.Trim()),
                new MyParameter("@SupplierPort", txtPort.Text.Trim()),
                new MyParameter("@ApprovalType", txtApprovalType.Text.Trim()),
                new MyParameter("@ServiceType", txtService.Text.Trim()),
                new MyParameter("@SupplierTel", txtTelephone.Text.Trim()),
                new MyParameter("@SupplierFax", txtFax.Text.Trim()),
                new MyParameter("@SupplierTelex", ""),
                new MyParameter("@SupplierAOH", ""),
                new MyParameter("@SupplierContact", txtContact.Text.Trim()),            
                new MyParameter("@Comments", ""),
                new MyParameter("@Active", ddlActive.SelectedValue),
                new MyParameter("@SupplierEmail", txtEmail.Text.Trim()),
                new MyParameter("@Preferred", ddlPreferred.SelectedValue),
                new MyParameter("@TravID", ddlTravVendor.SelectedValue),
                new MyParameter("@MultiCurr", ddlMultiCurr.SelectedValue),
                new MyParameter("@SGD_ID", DBNull.Value),
                new MyParameter("@USD_ID", DBNull.Value)
            );
        DataSet ResDS = new DataSet();
        Boolean Res = false;
        Res = Common.Execute_Procedures_IUD(ResDS);
        if (Res)
        {
            if(SupplierID<=0)
                lblmsg.Text = "Record saved successfully.";
            else
                lblmsg.Text = "Record updated successfully.";

            ScriptManager.RegisterStartupScript(Page, this.GetType(), "ss", "ReloadParentPage();", true);
            
        }
        else
            lblmsg.Text = "Data could not be saved.";
    }
    protected void OnClick_Cancel(object sender, EventArgs e)
    {
        Response.Redirect("Vendor.aspx");
    }
    
    protected void OnSelectedIndexChanged_ddlTravVendor(object sender, EventArgs e)
    {
        lblTravVenID.Text = ddlTravVendor.SelectedValue;
    }
    //Function ---------------------------------------------------------------------------------------
    public void BindTravVendor()
    {
        string sql = "SELECT DISTINCT v_vendors.vendorid, [name] + ' (' + [vendorid] + ')' AS Expr1, v_vendors.vendorid  "+
               " FROM dbo.v_vendors  " +
                " ORDER BY [name] + ' (' + [vendorid] + ')' ";
        DataTable DT = Common.Execute_Procedures_Select_ByQuery(sql);
        if (DT != null)
        {
            ddlTravVendor.DataSource = DT;
            ddlTravVendor.DataTextField = "Expr1";
            ddlTravVendor.DataValueField = "vendorid";
            ddlTravVendor.DataBind();
            ddlTravVendor.Items.Insert(0, new ListItem("< Select >", "0"));
        }
                
    }
    public void SetData()
    {
        string sql = " SELECT tblSMDSuppliers.SupplierID, tblSMDSuppliers.SupplierName, tblSMDSuppliers.SupplierPort, tblSMDSuppliers.SupplierTel "+
                " , tblSMDSuppliers.SupplierFax, tblSMDSuppliers.SupplierContact, tblSMDSuppliers.Active, tblSMDSuppliers.ApprovalType, tblSMDSuppliers.ServiceType "+
                " ,tblSMDSuppliers.SupplierEmail, tblSMDSuppliers.Preferred,tblSMDSuppliers.TravID, tblSMDSuppliers.MultiCurr "+
                " , tblSMDSuppliers.SGD_ID, tblSMDSuppliers.USD_ID "+
                " FROM DBO.tblSMDSuppliers " +
                " WHERE tblSMDSuppliers.SupplierID="+SupplierID+" " +
                " ORDER BY tblSMDSuppliers.SupplierName;";
        DataTable DT = Common.Execute_Procedures_Select_ByQuery(sql);
        if (DT != null)
            if (DT.Rows.Count > 0)
            {
                DataRow Dr=DT.Rows[0];
                txtVendor.Text = Dr["SupplierName"].ToString();
                ddlTravVendor.SelectedValue = Dr["TravID"].ToString();
                ddlMultiCurr.SelectedValue = (Dr["MultiCurr"].ToString()=="True")?"1":"0";

                txtPort.Text = Dr["SupplierPort"].ToString();
                txtTelephone.Text = Dr["SupplierTel"].ToString();
                txtFax.Text = Dr["SupplierFax"].ToString();
                txtEmail.Text = Dr["SupplierEmail"].ToString();
                txtContact.Text = Dr["SupplierContact"].ToString();

                ddlActive.SelectedValue = (Dr["Active"].ToString()=="True")?"1":"0";
                ddlPreferred.SelectedValue = (Dr["Preferred"].ToString()=="True")?"1":"0";

                txtApprovalType.Text = Dr["ApprovalType"].ToString();
                txtService.Text = Dr["ServiceType"].ToString();
                lblTravVenID.Text = Dr["TravID"].ToString();

            }
        
    }
}
