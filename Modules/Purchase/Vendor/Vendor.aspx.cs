using System;
using System.Collections;
using System.Collections.Specialized; 
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Xml;
     
//using Microsoft.Office.Interop.Outlook;    
/// <summary>
/// Page Name            : ReqFromVessels.aspx
/// Purpose              : Listing Of Files Received From Vessel
/// Author               : Shobhita Singh
/// Developed on         : 15 September 2010
/// </summary>

public partial class Vendor : System.Web.UI.Page
{
    public int SelectedPoId
    {
        get { return Convert.ToInt32("0" + hfSID.Value); }
        set { hfSID.Value = value.ToString(); }
    }
    public int VenSortBy
    {
        get { return Convert.ToInt32("0" + ViewState["VenSortBy"]); }
        set { ViewState["VenSortBy"] = value.ToString(); }
    }
    public int PortSortBy
    {
        get { return Convert.ToInt32("0" + ViewState["PortSortBy"]); }
        set { ViewState["PortSortBy"] = value.ToString(); }
    }
    public AuthenticationManager auth = new AuthenticationManager(0,0,ObjectType.Page);
           
    
    #region ---------- PageLoad ------------    
    protected void Page_Load(object sender, EventArgs e)
    {
        //---------------------------------------
        ProjectCommon.SessionCheck();
        //---------------------------------------
        #region --------- USER RIGHTS MANAGEMENT -----------
        try
        {
            auth = new AuthenticationManager(1065, int.Parse(Session["loginid"].ToString()), ObjectType.Page);
            if (!(auth.IsView))
            {
                Response.Redirect("~/NoPermission.aspx", false);
            }

        }
        catch (Exception ex)
        {
            Response.Redirect("~/NoPermission.aspx?Message=" + ex.Message);
        }
        #endregion ----------------------------------------         

        if (!Page.IsPostBack)
        {
            BindCountry();

            ddlMgr.DataSource = Common.Execute_Procedures_Select_ByQuery("select firstname + ' ' + middlename + ' ' + familyname as UserName,userid from dbo.Hr_PersonalDetails where drc is null and Position in (select positionid from dbo.Position where vesselpositions=3) order by username");
            ddlMgr.DataTextField = "UserName";
            ddlMgr.DataValueField = "userid";
            ddlMgr.DataBind();
            ddlMgr.Items.Insert(0,new ListItem("< Select Manager >",""));

            ddlMgmt.DataSource = Common.Execute_Procedures_Select_ByQuery("select firstname + ' ' + middlename + ' ' + familyname as UserName,userid from dbo.Hr_PersonalDetails where drc is null and position in (1,3,4) order by username");
            ddlMgmt.DataTextField = "UserName";
            ddlMgmt.DataValueField = "userid";
            ddlMgmt.DataBind();
            ddlMgmt.Items.Insert(0,new ListItem("< Select Management >",""));

            BindBusinessType();
            OnClick_imgSearch(sender, e);
        }

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
    #endregion

    // Events ----------------------------------------------------------
    protected void OnClick_btnEdit(object sender, EventArgs e)
    {
        Button btnEdit = (Button)sender;
        HiddenField hfSupplierID = (HiddenField)btnEdit.Parent.FindControl("hfSupplierID");
        Response.Redirect("AddNewVendor.aspx?SupplierID=" + hfSupplierID .Value+ "");
    }
    //added by laxmi on 24-sep-2015
    protected void rptVendor_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "emailToVendor")
        {
            int SuppliesID = Common.CastAsInt32(e.CommandArgument);
            ViewState["CopyToVendrorRequest"] = SuppliesID;

            Common.Set_Procedures("sp_CopySuppliertoVendorRequest");
            Common.Set_ParameterLength(3);
            Common.Set_Parameters(
            new MyParameter("@SuppliersId", SuppliesID),
            new MyParameter("@Eval1FwdTo", 0),
            new MyParameter("@Eval2FwdTo", 0)
            );
            DataSet ds = new DataSet();
            if (Common.Execute_Procedures_IUD(ds))
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string GUID = ds.Tables[0].Rows[0]["GUID"].ToString();
                    string EMAIL = ds.Tables[0].Rows[0]["EMAIL"].ToString();
                    //EMAIL = "pankaj.k@esoftech.com";
                    SendMail(GUID, EMAIL);
                    Common.Execute_Procedures_Select_ByQuery("UPDATE DBO.tbl_VenderRequest SET AllowToEditByMail='Y' WHERE supplierid=" + e.CommandArgument.ToString());
                    ShowMessage("Mail sent successfully.", false);
                    //ShowMessage("Transferred successfully.", false);
                }
            }

        }
        if (e.CommandName == "AllowResubmit")
        {
            //string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
            //string guid = commandArgs[0];
            //string vrid = commandArgs[1];
            Common.Execute_Procedures_Select_ByQuery("UPDATE DBO.tbl_VenderRequest SET AllowToEditByMail='Y' WHERE VRID=" + e.CommandArgument.ToString());
            ShowMessage("Allowed successfully.", false);
        }
    }
    public void SendMail(string guid, string emailid)
    {
        string[] ToAdd = { emailid };
        string[] NoAdd = { "" };
        string useremail = ProjectCommon.getUserEmailByID(Session["loginid"].ToString());
        string[] CcAdd = { useremail };
        string[] BccAdd = { "emanager@energiossolutions.com" };
        // string LinkText = "http://localhost:50192/public/VendorManagement/ModifyVendorProfile.aspx?_key=" + guid;
        string LinkText = ConfigurationManager.AppSettings["UpdateVendorProfile"].ToString() + guid;
        string space = "<br/></br/>";
        string spacesingle = "<br/>";
        string Message = "Dear Sirs/Madam," + space +
                         "We’re in the midst of refreshing, re-evaluating & verifying our existing vendors in our approved vendors list." + space +
                         "As part of our commitment to continually improve our vendor management process and keeping the latest update of all our vendor’s profile is just one of them. " + space +
                         "In line with this policy, we request you furnish the latest details of your esteemed company via the link below:- " + space +
                         "<a href='" + LinkText + "' target='_blank'>Click Here</a>" + space +
                         "OR" + space +
                         "copy the link " + space +
                         LinkText +
                         " and paste in a browser new window to update the profile." + space +
                         "We take this opportunity to thank you for your continued support to our common interests & our goal of achieving excellence in Vendor Management. " + space + space +
                         "Thank you with best regards" + space +
                         "<b>Vendor Management<b>" + spacesingle +
                         "";

        //string Message = "Dear Vendor/Supplier,<br><br>Your request successfully received by us.<br><br>Below is the link given to update your full profile.<br><br> <a href='" + LinkText + "' target='_blank'>Click Here</a> <br>OR<br>copy the link <br>" + LinkText + "<br><br>and paste in a browser new window to update the profile.<br><br> Once you submit your profile to use we will continue your approval process.<br><br><br>Thanks<br>";
       
        if (useremail.Trim() != "")
            NoAdd[0] = useremail;
        ProjectCommon.SendMail(ToAdd, CcAdd, BccAdd , "Vendor/Supplier Registeration Verfication", Message, NoAdd);
    }
    //end
    protected void OnClick_btnPageReload(object sender, EventArgs e)
    {
        BindVendor();
    }
    protected void btn_closemodel_Click(object sender, EventArgs e)
    {
        //ddlApprovalType.SelectedIndex = 0;
        //modalBox.Visible = false;
        //modalframeVendroProfile.Visible = false;
    }
    //for  Delist after second approval 
    protected void btn_SaveRequestStatus_Dlist_Click(object sender, EventArgs e)
    {
        if (txt_Name_ForDlist.Text != "")
        {
            //Common.Set_Procedures("dbo.sp_RequestApprvalDlist");
            //Common.Set_ParameterLength(3);
            //Common.Set_Parameters(new MyParameter("@VRID", Common.CastAsInt32(hdn_VRID.Value)),
            //    new MyParameter("@CurrentStatus", Common.CastAsInt32(rd_ForDlist.SelectedValue)),
            //    new MyParameter("@CurrentStatusBy", txt_Name_ForDlist.Text.Trim().Replace("'", "`"))
            //    );
            //DataSet DT = new DataSet();
            //if (Common.Execute_Procedures_IUD(DT))
            //{
            //    rd_ForDlist.SelectedIndex = 0;
            //    //modalBox.Visible = false;
            //    modalframeVendroProfile.Visible = false;
            //    BindVendor();
            //}
        }
        else
        {
            ShowMessage("Please enter all details", true);
        }
    }
    public void ShowMessage(string Msg, bool Error)
    {
        lblMessage.Text = Msg;
        lblMessage.ForeColor = (Error) ? System.Drawing.Color.Green : System.Drawing.Color.Red;
    }
    protected void OnClick_imgSearch(object sender, EventArgs e)
    {
        string WhereClause=" where 1=1 ";
        if (txtVendor.Text.Trim() != "")
        {
            WhereClause = WhereClause + " and SupplierName like '"+txtVendor.Text.Trim()+"%'";
        }
        if (txtPort.Text.Trim() != "")
        {
            WhereClause = WhereClause + " and SupplierPort ='" + txtPort.Text.Trim() + "'";
        }
        if (ddlActive.SelectedIndex != 0)
        {
            WhereClause = WhereClause + " and active=" + ddlActive.SelectedValue + "";
            if (ddlActive.SelectedValue == "0")
            {
                if(chkBlackList.Checked)
                    WhereClause = WhereClause + " and Blacklist2=1";
            }
        }
        //if (txtApprovalType.Text.Trim() != "")
        //{
        //    WhereClause = WhereClause + " and ApprovalType ='" + txtApprovalType.Text.Trim() + "'";
        //}
        if (ddlCountry.SelectedIndex != 0)
            WhereClause = WhereClause + " And CountryID =" + ddlCountry.SelectedValue + " ";

        if (txtcity.Text.Trim() != "")
            WhereClause = WhereClause + " And City_State like '%" + txtcity.Text.Trim() + "%' ";

        if (hfSelBusinessType.Value != "")
        {
            WhereClause = WhereClause + " and  (";
            string[] arrData = hfSelBusinessType.Value.ToString().Split('~');
            int indx = 0;
            foreach (string val in arrData)
            {
                //WhereClause = WhereClause + " And COMPANYBUSINESSES like '%," + val+ ",%' or SUBSTRING(COMPANYBUSINESSES,0, CHARINDEX(',',COMPANYBUSINESSES,1))='"+ val + "' or SUBSTRING(REVERSE(COMPANYBUSINESSES),0, CHARINDEX(',',REVERSE(COMPANYBUSINESSES),1))='"+ val + "' ";
                WhereClause = WhereClause + " ',' + COMPANYBUSINESSES + ',' like '%," + val + ",%' ";
                if (indx < arrData.Length - 1)
                    WhereClause = WhereClause + " or ";
                indx++;
            }
            WhereClause = WhereClause + " ) ";
        }

        DataTable Dt = GetSuplierData("", "", WhereClause);
        if (Dt != null)
        {
            rptVendor.DataSource = Dt;
            rptVendor.DataBind();
            lblTotRec.Text = "Total Suppliers : " + Dt.Rows.Count;
        }
    }
    protected void OnClick_imgCancel(object sender, EventArgs e)
    {
        txtVendor.Text = "";
        txtPort.Text = "";
        ddlActive.SelectedIndex = 0;
      //  txtApprovalType.Text = "";
    }
    // Sorting Events -----------------------------------------------------
    protected void OnClick_lnkSortVendor(object sender, EventArgs e)
    {
        string SortBy="";
        if (VenSortBy == 0)
        {
            SortBy = "asc";
            VenSortBy = 1;
        }
        else
        {
            SortBy = "desc";
            VenSortBy = 0;
        }
        DataTable DT = GetSuplierData("SupplierName", SortBy,"");
        rptVendor.DataSource = DT;
        rptVendor.DataBind();


    }
    protected void OnClick_lnkSorVendor(object sender, EventArgs e)
    {
        string SortBy = "";
        if (PortSortBy == 0)
        {
            SortBy = "asc";
            PortSortBy = 1;
        }
        else
        {
            SortBy = "desc";
            PortSortBy = 0;
        }
        DataTable DT = GetSuplierData("SupplierPort", SortBy,"");
        rptVendor.DataSource = DT;
        rptVendor.DataBind();
    }

    // Function ----------------------------------------------------------
    public void BindVendor()
    {
        DataTable DTVendor = GetSuplierData("","","");
        if (DTVendor != null)
        {
            rptVendor.DataSource = DTVendor;
            rptVendor.DataBind();
            lblTotRec.Text = "Total Suppliers : " + DTVendor.Rows.Count; ;
        }

    }
    public DataTable GetSuplierData( string SortBy,string SortType , string WhereClause)
    {
        string sql = "SELECT Row_Number() over(order by SupplierName) as srno,* FROM VW_ALL_VENDERS ";
            
        if (WhereClause != "")
            sql = sql + WhereClause;
        else
            sql = sql + "WHERE Active=1 ";

        
        DataTable DTVendor = Common.Execute_Procedures_Select_ByQuery(sql + " ORDER BY SupplierName ");
        return DTVendor;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        int SuppliesID=Common.CastAsInt32(ViewState["CopyToVendrorRequest"]);
        Common.Set_Procedures("sp_CopySuppliertoVendorRequest");
        Common.Set_ParameterLength(3);
        Common.Set_Parameters(
        new MyParameter("@SuppliersId", SuppliesID),
        new MyParameter("@Eval1FwdTo", ddlMgr.SelectedValue),
        new MyParameter("@Eval2FwdTo", ddlMgmt.SelectedValue)
        );
        DataSet dsCopySuppliers = new DataSet();
        if (Common.Execute_Procedures_IUD(dsCopySuppliers))
        {
            dvCopyToVendorRequest.Visible = false; 
        }
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        dvCopyToVendorRequest.Visible = false;
    }

    public void BindBusinessType()
    {
        DataTable dtCountry = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.COUNTRY ORDER BY COUNTRYNAME");
        DataTable dtVendorList = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.tblVendorBusinessesList ORDER BY Vendorlistname");
        chkVendorbusinesseslist.DataSource = dtVendorList;
        chkVendorbusinesseslist.DataTextField = "Vendorlistname";
        chkVendorbusinesseslist.DataValueField = "Vendorlistid";
        chkVendorbusinesseslist.DataBind();
    }
    protected void lnkOpenBusinessType_OnClick(object sender, EventArgs e)
    {
        divAddBusinessType.Visible = true;
        chkVendorbusinesseslist.ClearSelection();
        foreach (ListItem itm in chkVendorbusinesseslist.Items)
        {
            foreach (string val in hfSelBusinessType.Value.ToString().Split('~'))
            {
                if (itm.Value == val)
                {
                    itm.Selected = true;
                    break;
                }
            }
        }

    }
    protected void btnCloseAddBusinessTypePopup_OnClick(object sender, EventArgs e)
    {
        divAddBusinessType.Visible = false;
    }
    protected void btnAddBusinessType_OnClick(object sender, EventArgs e)
    {
        lblBusinessType.Text = "";
        hfSelBusinessType.Value = "";

        foreach (ListItem itm in chkVendorbusinesseslist.Items)
        {
            if (itm.Selected)
            {
                lblBusinessType.Text = lblBusinessType.Text + ", " + itm.Text;
                hfSelBusinessType.Value = hfSelBusinessType.Value + "~" + itm.Value;
            }
        }
        if (lblBusinessType.Text.Trim() != "")
        {
            lblBusinessType.Text = lblBusinessType.Text.Substring(1);
            hfSelBusinessType.Value = hfSelBusinessType.Value.Substring(1);
        }
    }
    protected void btnCleaerBusinessType_OnClick(object sender, EventArgs e)
    {
        chkVendorbusinesseslist.ClearSelection();
        hfSelBusinessType.Value = "";
        lblBusinessType.Text = "";
    }



    protected void ddlActive_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        chkBlackList.Checked = false;
        if (ddlActive.SelectedIndex == 2)
            chkBlackList.Visible = true;
        else
            chkBlackList.Visible = false;

        
    }

    
}

   