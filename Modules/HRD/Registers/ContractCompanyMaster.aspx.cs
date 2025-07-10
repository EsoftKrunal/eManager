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

public partial class Registers_Company : System.Web.UI.Page
{

    int id;
    Authority Auth;
    string Mode = "New";

    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        lbl_GridView_ContractCompany.Text = "";
        lbl_ContractCompany_Message.Text = "";
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 10);
        if (chpageauth <= 0)
        {
            Response.Redirect("Dummy.aspx");

        }
        ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 10);
        OBJ.Invoke();
        Session["Authority"] = OBJ.Authority;
        Auth = OBJ.Authority;
        if (!Page.IsPostBack)
        {
            BindStatusDropDown();
            bindCompanyContractGrid();
            //   Show_Record_company(id);
            //btn_Print_Company.Visible = Auth.isPrint;
            //btn_company_save.Visible = Auth.isEdit;
            Alerts.HidePanel(Panel_Company);
            Alerts.HANDLE_AUTHORITY(1, btn_Add_CompanyContract, btn_company_save, btn_company_Cancel, btn_Print_Company, Auth);

        }
    }
    private void BindStatusDropDown()
    {
        DataTable dt3 = Company.selectDataStatus();
        this.ddstatus_company.DataValueField = "StatusId";
        this.ddstatus_company.DataTextField = "StatusName";
        this.ddstatus_company.DataSource = dt3;
        this.ddstatus_company.DataBind();
    }
    protected void Show_Record_company(int companyid)
    {
        //companyid = 1;
        //string Mess;
        //Mess = "";
        Hiddencompanypk.Value = companyid.ToString();
        lbl_Company_Message.Visible = false;
        DataTable dt3 = Company.selectDataContractCompanyDetailsByCompanyId(companyid);
        foreach (DataRow dr in dt3.Rows)
        {
            txtcompanyname.Text = dr["companyname"].ToString();
            txtCompanyabbr.Text = dr["companyabbr"].ToString();
            txtcompanyaddress1.Text = dr["Address1"].ToString();
            txtcompanyaddress2.Text = dr["address2"].ToString();
            txtcompanyaddress3.Text = dr["address3"].ToString();
            txttelno1.Text = dr["telephonenumber1"].ToString();
            txttelno2.Text = dr["telephonenumber2"].ToString();
            txtfaxno.Text = dr["faxnumber"].ToString();
            txtemail1.Text = dr["email1"].ToString();
            txtemail2.Text = dr["email2"].ToString();
            txtwebsite.Text = dr["Website"].ToString();
            txtcreatedby_company.Text = dr["CreatedBy"].ToString();
            txtcreatedon_company.Text = dr["CreatedOn"].ToString();
            txtmodifiedby_company.Text = dr["ModifiedBy"].ToString();
            txtmodifiedon_company.Text = dr["ModifiedOn"].ToString();
            ddstatus_company.SelectedValue = dr["StatusId"].ToString();
            txtRegNo.Text = dr["RegistrationNo"].ToString();
            txtRPSLValidity.Text = dr["RPSLValidityDt"].ToString();
            txtContactPerson.Text = dr["ContactPerson"].ToString();
            txtRPSL.Text = dr["RPSL"].ToString();
            if (dr["IsShipManager"].ToString() == "1")
            {
                chkIsShipManager.Checked = true;
            }
            else
            {
                chkIsShipManager.Checked = false;
            }
            if (dr["IsOwnerAgent"].ToString() == "1")
            {
                chkIsOwnerAgent.Checked = true;
            }
            else
            {
                chkIsOwnerAgent.Checked = false;
            }
            if (dr["IsMLCAgent"].ToString() == "1")
            {
                chkIsMLCAgent.Checked = true;
            }
            else
            {
                chkIsMLCAgent.Checked = false;
            }
            if (dr["IsManningAgent"].ToString() == "1")
            {
                chkIsManningAgent.Checked = true;
            }
            else
            {
                chkIsManningAgent.Checked = false;
            }
        }
        btn_Print_Company.Visible = Auth.isPrint;
    }
    protected void GvCompany_DataBound(object sender, EventArgs e)
    {
       
    }
    protected void btn_company_save_Click(object sender, EventArgs e)
    {
        //string str1 = FileUpload_CompanyLogo.PostedFile.FileName;
        //Boolean str_correct = str1.EndsWith(".jpg");
        //string str3 = FileUpload_ReportLogo.PostedFile.FileName;
       // Boolean str_report = str3.EndsWith(".jpg");
        //if (FileUpload_CompanyLogo.FileName=="" && FileUpload_ReportLogo.FileName=="")
        //{
            save(sender,e);
        //}
        //else if (FileUpload_CompanyLogo.FileName.Length > 0 && FileUpload_ReportLogo.FileName.Length > 0)
        //{
        //    if (str_correct == true)
        //    {
        //        string str2 = System.IO.Path.GetFileName(str1);
        //        file_upload_company();
        //        save(sender, e);
        //    }
        //    else
        //    {
        //        lbl_Company_Message.Visible = true;
        //        lbl_Company_Message.Text = "Upload only JPG files in Company Logo.";
        //    }
        //    if (str_report == true)
        //    {
        //        string str4 = System.IO.Path.GetFileName(str3);
        //        file_upload_report();
        //        save(sender, e);
        //    }
        //    else
        //    {
        //        lbl_report_message.Visible = true;
        //        lbl_report_message.Text = "Upload only JPG files in Report Logo.";
        //        return;
        //    }
        //}
        //else if (FileUpload_CompanyLogo.FileName.Length > 0 && FileUpload_ReportLogo.FileName=="")
        //{
        //    if (str_correct == true)
        //    {
        //        string str2 = System.IO.Path.GetFileName(str1);
        //        file_upload_company();
        //        save(sender, e);
        //    }
        //    else
        //    {
        //        lbl_Company_Message.Visible = true;
        //        lbl_Company_Message.Text = "Upload only JPG files in Company Logo.";
        //        return;
        //    }
        //}
        //else if (FileUpload_CompanyLogo.FileName == "" && FileUpload_ReportLogo.FileName.Length > 0)
        //{
        //    if (str_report == true)
        //    {
        //        string str4 = System.IO.Path.GetFileName(str3);
        //        file_upload_report();
        //        save(sender, e);
        //    }
        //    else
        //    {
        //        lbl_report_message.Visible = true;
        //        lbl_report_message.Text = "Upload only JPG files in Report Logo.";
        //        return;
        //    }
           
        //}
        
    }
    protected void save(object sender, EventArgs e)
    {
        // int companyId = 1;

        int Duplicate = 0;

        foreach (GridViewRow dg in GridView_ContractCompany.Rows)
        {
            HiddenField hfd;
            HiddenField hfd1;
            hfd = (HiddenField)dg.FindControl("hdnCompanyName");
            hfd1 = (HiddenField)dg.FindControl("HiddenCompanyId");

            if (hfd.Value.ToString().ToUpper().Trim() == txtcompanyname.Text.ToUpper().Trim())
            {
                if (Hiddencompanypk.Value.Trim() == "")
                {

                    lbl_ContractCompany_Message.Text = "Already Entered.";
                    Duplicate = 1;
                    break;
                }
                else if (Hiddencompanypk.Value.Trim() != hfd1.Value.ToString())
                {

                    lbl_ContractCompany_Message.Text = "Already Entered.";
                    Duplicate = 1;
                    break;
                }
            }
            else
            {

                lbl_ContractCompany_Message.Text = "";
            }
        }
        if (Duplicate == 0)
        {
            int companyId = -1;
            int createdby = 0, modifiedby = 0;
            if (Hiddencompanypk.Value.ToString().Trim() == "")
            {
                createdby = Convert.ToInt32(Session["loginid"].ToString());
            }
            else
            {
                companyId = Convert.ToInt32(Hiddencompanypk.Value);
                modifiedby = Convert.ToInt32(Session["loginid"].ToString());
            }
            string strcompanyname = txtcompanyname.Text;
            string strcompanyabbr = txtCompanyabbr.Text;
            string straddress1 = txtcompanyaddress1.Text;
            string straddress2 = txtcompanyaddress2.Text;
            string straddress3 = txtcompanyaddress3.Text;
            string strtelno1 = txttelno1.Text;
            string strtelno2 = txttelno2.Text;
            string strfaxno = txtfaxno.Text;
            string stremail1 = txtemail1.Text;
            string stremail2 = txtemail2.Text;
            string strwebsite = txtwebsite.Text;
            string regno = txtRegNo.Text;
            string strRPSL = txtRPSL.Text;
            string strRPSLValidity = txtRPSLValidity.Text;
            string strContactPerson = txtContactPerson.Text;
           // string str2 = System.IO.Path.GetFileName(str1);
            char status = Convert.ToChar(ddstatus_company.SelectedValue);
            createdby = 
            modifiedby = Convert.ToInt32(Session["loginid"].ToString());
            int IsShipManager, IsMLCAgent, IsManningAgent, IsOwnerAgent;
            if (chkIsShipManager.Checked)
            {
                IsShipManager = 1;
            }
            else
            {
                IsShipManager = 0;
            }
            if (chkIsMLCAgent.Checked)
            {
                IsMLCAgent = 1;
            }
            else
            {
                IsMLCAgent = 0;
            }
            if (chkIsOwnerAgent.Checked)
            {
                IsOwnerAgent = 1;
            }
            else
            {
                IsOwnerAgent = 0;
            }
            if (chkIsManningAgent.Checked)
            {
                IsManningAgent = 1;
            }
            else
            {
                IsManningAgent = 0;
            }
            Company.insertUpdateContractCompanyMaster("InsertUpdateContractCompanyMaster",
                                               companyId,
                                               strcompanyname,
                                               strcompanyabbr,
                                               straddress1,
                                               straddress2,
                                               straddress3,
                                               strtelno1,
                                               strtelno2,
                                               strfaxno,
                                               stremail1,
                                               stremail2,
                                               strwebsite,
                                               createdby,
                                               modifiedby,
                                               status,
                                                regno,
                                              strRPSL,
                                               IsShipManager,
                                               IsOwnerAgent, IsMLCAgent, IsManningAgent,strRPSLValidity,strContactPerson
                                               );
            btn_company_Cancel_Click(sender, e);
            lbl_Company_Message.Visible = true;
            lbl_Company_Message.Text = "Record Successfully Saved.";
            bindCompanyContractGrid();
            Alerts.HidePanel(Panel_Company);
            Alerts.HANDLE_AUTHORITY(3, btn_Add_CompanyContract, btn_company_save, btn_company_Cancel, btn_Print_Company, Auth);
            //btn_company_Cancel.Visible = false;
            //btn_company_save.Visible = false;
            //btn_Print_Company.Visible = false;
        }
    }
    protected void btn_company_Cancel_Click(object sender, EventArgs e)
    {
        //btn_company_Cancel.Visible = false;
        //companypanel.Visible = false;
        //btn_company_save.Visible = false;
        //btn_Print_Company.Visible = false;
        //lbl_Company_Message.Visible = false;
        //lbl_report_message.Visible = false;
        GridView_ContractCompany.SelectedIndex = -1;
        Alerts.HidePanel(Panel_Company);
        Alerts.HANDLE_AUTHORITY(3, btn_Add_CompanyContract, btn_company_save, btn_company_Cancel, btn_Print_Company, Auth);
    }
    protected void btn_Print_Company_Click(object sender, EventArgs e)
    {

    }
    
    //protected void file_upload_company()
    //{
    //    string file = "";
       
    //    string existingFileName = "";
    //    string path = Server.MapPath("~//Images//Logo//CompanyLogo.jpg");
        
    //    string fullPath;
    //    if (File.Exists(Server.MapPath("~//Images//Logo//CompanyLogo.jpg")))
    //    {
    //        fullPath = path + existingFileName;
    //        FileInfo fi = new FileInfo(fullPath);
    //        if (fi.Exists)
    //        {
    //            fi.Delete();
    //        }
    //        FileUpload_CompanyLogo.SaveAs(Server.MapPath("~//Images//Logo//CompanyLogo.jpg") + file);
    //    }
    //}
    //protected void file_upload_report()
    //{
    //    string file2 = "";
    //    string existingFileName = "";
    //    string path1 = Server.MapPath("~//Images//Logo//ReportLogo.jpg");
    //    string fullPath;
    //    if (File.Exists(Server.MapPath("~//Images//Logo//MTMMLogo.jpg")))
    //    {
    //        fullPath = path1 + existingFileName;
    //        FileInfo fif = new FileInfo(fullPath);
    //        if (fif.Exists)
    //        {
    //            fif.Delete();
    //        }
    //        FileUpload_ReportLogo.SaveAs(Server.MapPath("~//Images//Logo//MTMMLogo.jpg") + file2);
    //    }
    //}


    protected void GridView_ContractCompany_DataBound(object sender, EventArgs e)
    {
        Alerts.HANDLE_GRID(GridView_ContractCompany, Auth);
    }
    public void bindCompanyContractGrid()
    {
        DataTable dt1 = Company.selectDataCompanyContractDetails();
        this.GridView_ContractCompany.DataSource = dt1;
        this.GridView_ContractCompany.DataBind();
    }
    protected void GridView_ContractCompany_PreRender(object sender, EventArgs e)
    {
        if (GridView_ContractCompany.Rows.Count <= 0) { lbl_GridView_ContractCompany.Text = "No Records Found..!"; }
    }

    protected void GridView_ContractCompany_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Modify")
        {
            GridViewRow row = (GridViewRow)(((ImageButton)sender).NamingContainer);
            int Rowindx = row.RowIndex;
            Mode = "Edit";
            HiddenField hfdOwner;
            hfdOwner = (HiddenField)GridView_ContractCompany.Rows[Rowindx].FindControl("hdnContractCompanyId");
            id = Convert.ToInt32(hfdOwner.Value.ToString());
            Show_Record_company(id);
            GridView_ContractCompany.SelectedIndex = Rowindx;
            Alerts.ShowPanel(Panel_Company);
            Alerts.HANDLE_AUTHORITY(5, btn_Add_CompanyContract, btn_company_save, btn_company_Cancel, btn_Print_Company, Auth);
        }
    }

    protected void GridView_ContractCompany_Row_Deleting(object sender, GridViewDeleteEventArgs e)
    {
        int ModifiedBy = Convert.ToInt32(Session["loginid"].ToString());
        HiddenField hfdCompanyContract;
        hfdCompanyContract = (HiddenField)GridView_ContractCompany.Rows[e.RowIndex].FindControl("hdnContractCompanyId");
        id = Convert.ToInt32(hfdCompanyContract.Value.ToString());
        Company.deleteContractCompanyById("DeleteContractCompanyById", id, ModifiedBy);
        bindCompanyContractGrid();
        if (hfdCompanyContract.Value.ToString() == Hiddencompanypk.Value.ToString())
        {
            btn_Add_CompanyContract_Click(sender, e);
        }
    }

    protected void GridView_ContractCompany_Row_Editing(object sender, GridViewEditEventArgs e)
    {
        Mode = "Edit";
        HiddenField hfdCompanyContract;
        hfdCompanyContract = (HiddenField)GridView_ContractCompany.Rows[e.NewEditIndex].FindControl("hdnContractCompanyId");
        id = Convert.ToInt32(hfdCompanyContract.Value.ToString());
        Show_Record_company(id);
        GridView_ContractCompany.SelectedIndex = e.NewEditIndex;
        Alerts.ShowPanel(Panel_Company);
        Alerts.HANDLE_AUTHORITY(4, btn_Add_CompanyContract, btn_company_save, btn_company_Cancel, btn_Print_Company, Auth);
    }

    protected void GridView_ContractCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        HiddenField hfdCompanyContract;
        hfdCompanyContract = (HiddenField)GridView_ContractCompany.Rows[GridView_ContractCompany.SelectedIndex].FindControl("hdnContractCompanyId");
        id = Convert.ToInt32(hfdCompanyContract.Value.ToString());
        Show_Record_company(id);
        Alerts.ShowPanel(Panel_Company);
        Alerts.HANDLE_AUTHORITY(4, btn_Add_CompanyContract, btn_company_save, btn_company_Cancel, btn_Print_Company, Auth);
    }

    protected void btn_Add_CompanyContract_Click(object sender, EventArgs e)
    {
        Hiddencompanypk.Value = "";
        
        ddstatus_company.SelectedIndex = 0;
        txtCompanyabbr.Text = "";
        txtcompanyname.Text = "";
        txtRegNo.Text = "";
        txttelno1.Text = "";
        txttelno2.Text = "";
        txtemail1.Text = "";
        txtemail2.Text = "";
        txtcreatedby_company.Text = "";
        txtcreatedon_company.Text = "";
        txtmodifiedby_company.Text = "";
        txtmodifiedon_company.Text = "";
        txtwebsite.Text = "";
        txtcompanyaddress1.Text = "";
        txtcompanyaddress2.Text = "";
        txtcompanyaddress3.Text = "";
        txtContactPerson.Text = "";
        txtRPSLValidity.Text = "";
        chkIsShipManager.Checked = false;
        chkIsOwnerAgent.Checked = false;
        chkIsMLCAgent.Checked = false;
        chkIsManningAgent.Checked = false;
        txtRPSL.Text = "";
        GridView_ContractCompany.SelectedIndex = -1;
        Alerts.ShowPanel(Panel_Company);
        Alerts.HANDLE_AUTHORITY(2, btn_Add_CompanyContract, btn_company_save, btn_company_Cancel, btn_Print_Company, Auth);
    }
    protected void btnEditCompnayContract_click(object sender, ImageClickEventArgs e)
    {
        GridViewRow row = (GridViewRow)(((ImageButton)sender).NamingContainer);
        int Rowindx = row.RowIndex;
        Mode = "Edit";
        HiddenField hfdContractCompany;
        hfdContractCompany = (HiddenField)GridView_ContractCompany.Rows[Rowindx].FindControl("hdnContractCompanyId");
        id = Convert.ToInt32(hfdContractCompany.Value.ToString());
        Show_Record_company(id);
        GridView_ContractCompany.SelectedIndex = Rowindx;
        Alerts.ShowPanel(Panel_Company);
        Alerts.HANDLE_AUTHORITY(5, btn_Add_CompanyContract, btn_company_save, btn_company_Cancel, btn_Print_Company, Auth);
    }
}
