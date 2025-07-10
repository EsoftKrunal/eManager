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

    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
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
            Show_Record_company(id);
            btn_Print_Company.Visible = Auth.isPrint;
            btn_company_save.Visible = Auth.isEdit;

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
        companyid = 1;
        lbl_Company_Message.Visible = false;
        DataTable dt3 = Company.selectDataCompanyDetailsByCompanyId(companyid);
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
        }
        btn_Print_Company.Visible = Auth.isPrint;
    }
    protected void GvCompany_DataBound(object sender, EventArgs e)
    {
       
    }
    protected void btn_company_save_Click(object sender, EventArgs e)
    {
        string str1 = FileUpload_CompanyLogo.PostedFile.FileName;
        Boolean str_correct = false;
        if (str1.EndsWith(".jpg") || str1.EndsWith(".png"))
        {
            str_correct = true;
        }
        
        string str3 = FileUpload_ReportLogo.PostedFile.FileName;
        Boolean str_report = str3.EndsWith(".jpg");
        if (FileUpload_CompanyLogo.FileName=="" && FileUpload_ReportLogo.FileName=="")
        {
            save(sender,e);
        }
        else if (FileUpload_CompanyLogo.FileName.Length > 0 && FileUpload_ReportLogo.FileName.Length > 0)
        {
            if (str_correct == true)
            {
                string str2 = System.IO.Path.GetFileName(str1);
               // file_upload_company();
                save(sender, e);
            }
            else
            {
                lbl_Company_Message.Visible = true;
                lbl_Company_Message.Text = "Upload only JPG/PNG files in Company Logo.";
            }
            if (str_report == true)
            {
                string str4 = System.IO.Path.GetFileName(str3);
                file_upload_report();
                save(sender, e);
            }
            else
            {
                lbl_report_message.Visible = true;
                lbl_report_message.Text = "Upload only JPG files in Report Logo.";
                return;
            }
        }
        else if (FileUpload_CompanyLogo.FileName.Length > 0 && FileUpload_ReportLogo.FileName=="")
        {
            if (str_correct == true)
            {
                string str2 = System.IO.Path.GetFileName(str1);
               // file_upload_company();
                save(sender, e);
            }
            else
            {
                lbl_Company_Message.Visible = true;
                lbl_Company_Message.Text = "Upload only JPG/PNG files in Company Logo.";
                return;
            }
        }
        else if (FileUpload_CompanyLogo.FileName == "" && FileUpload_ReportLogo.FileName.Length > 0)
        {
            if (str_report == true)
            {
                string str4 = System.IO.Path.GetFileName(str3);
                file_upload_report();
                save(sender, e);
            }
            else
            {
                lbl_report_message.Visible = true;
                lbl_report_message.Text = "Upload only JPG files in Report Logo.";
                return;
            }
           
        }
        
    }
    protected void save(object sender, EventArgs e)
    {
        int companyId = 1;
        int createdby = 0, modifiedby = 0;
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
        char status = Convert.ToChar(ddstatus_company.SelectedValue);
        createdby = Convert.ToInt32(Session["loginid"].ToString());
        modifiedby = Convert.ToInt32(Session["loginid"].ToString());
        string str1 = System.IO.Path.GetFileName(FileUpload_CompanyLogo.PostedFile.FileName);
        if (str1 != "")
        {
            string path = "~//Modules/HRD//Images//Logo//";
            FileInfo fi = new FileInfo(path + str1);
            if (fi.Exists)
            {
                fi.Delete();
            }
            FileUpload_CompanyLogo.SaveAs(Server.MapPath(path) + str1);
        }
        

        

        Company.insertUpdateCompanyDetails("InsertUpdateCompanyDetails",
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
                                           str1);
        btn_company_Cancel_Click(sender, e);
        lbl_Company_Message.Visible = true;
        lbl_Company_Message.Text = "Record Successfully Saved.";
        btn_company_Cancel.Visible = false;
        btn_company_save.Visible = false;
        btn_Print_Company.Visible = false;
    }
    protected void btn_company_Cancel_Click(object sender, EventArgs e)
    {
        btn_company_Cancel.Visible = false;
        companypanel.Visible = false;
        btn_company_save.Visible = false;
        btn_Print_Company.Visible = false;
        lbl_Company_Message.Visible = false;
        lbl_report_message.Visible = false;
    }
    protected void btn_Print_Company_Click(object sender, EventArgs e)
    {

    }
    
    protected string file_upload_company()
    {
       // string file = "";
       
       // string existingFileName = "";
        string str1 = System.IO.Path.GetFileName(FileUpload_CompanyLogo.PostedFile.FileName);
        string path = "~//Modules/HRD//Images//Logo//";
        FileInfo fi = new FileInfo(path + str1);
        if (fi.Exists)
        {
            fi.Delete();
        }

        FileUpload_CompanyLogo.SaveAs(Server.MapPath(path) + str1);

        return (Server.MapPath(path) + str1);
        //if (str1.EndsWith(".jpg"))
        //{
        //    path = Server.MapPath("~//Modules/HRD//Images//Logo//CompanyLogo.jpg");
        //}

        //if (str1.EndsWith(".png"))
        //{
        //    path = Server.MapPath("~//Modules/HRD//Images//Logo//CompanyLogo.png");
        //}


        //string fullPath;
        //if (File.Exists(Server.MapPath(path)))
        //{
        //    fullPath = path + existingFileName;
        //    FileInfo fi = new FileInfo(fullPath);
        //    if (fi.Exists)
        //    {
        //        fi.Delete();
        //    }
        //    FileUpload_CompanyLogo.SaveAs(Server.MapPath(path) + file);
        //}
    }
    protected void file_upload_report()
    {
        string file2 = "";
        string existingFileName = "";
        string path1 = Server.MapPath("~//Modules/HRD//Images//Logo//ReportLogo.jpg");
        string fullPath;
        if (File.Exists(Server.MapPath("~//Modules/HRD//Images//Logo//ReportLogo.jpg")))
        {
            fullPath = path1 + existingFileName;
            FileInfo fif = new FileInfo(fullPath);
            if (fif.Exists)
            {
                fif.Delete();
            }
            FileUpload_ReportLogo.SaveAs(Server.MapPath("~//Modules/HRD//Images//Logo//ReportLogo.jpg") + file2);
        }
    }
   
}
