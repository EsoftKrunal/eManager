using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Emtm_Profile_CertificateDocs : System.Web.UI.Page
{
    public AuthenticationManager auth;
    //User Defined Properties
    public int SelectedId
    {
        get
        {
            return Common.CastAsInt32(ViewState["SelectedId"]);
        }
        set
        {
            ViewState["SelectedId"] = value;
        }
    }
    //-----------------------
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 263);
        if (chpageauth <= 0)
        {
            Response.Redirect("../AuthorityError.aspx");
        }
        //*******************
        auth = new AuthenticationManager(263, Common.CastAsInt32(Session["loginid"]), ObjectType.Page);


        Session["CurrentModule"] = 2;
        if (!Page.IsPostBack)
        {
            ShowRecord(SelectedId);
            setButtons("");
            BindGrid();
            lbl_EmpName.Text = Session["ProfileName"].ToString();
        }
    }
    //-----------------------
    # region --- User Defined Functions ---
    //-- COMMON FUNCTIONS
    protected void setButtons(string Action)
    {
        string EmpMode = Session["ProfileMode"].ToString();
       
            if (EmpMode == "View")
            {
                switch (Action)
                {
                    case "View":
                        tblview.Visible = true;
                        btnaddnew.Visible = false;
                        btnsave.Visible = false;
                        btncancel.Visible = true;
                        break;
                    default:
                        tblview.Visible = false;
                        btnaddnew.Visible = false;
                        btnsave.Visible = false;
                        btncancel.Visible = false;
                        break;
                }
            }
            if (EmpMode == "Edit")
            {
                switch (Action)
                {
                    case "View":
                        tblview.Visible = true;
                        divTraveldoc.Style.Add("height", "230px;");

                        btnaddnew.Visible = false;
                        btnsave.Visible = false;
                        btncancel.Visible = true;
                        break;
                    case "Add":
                        tblview.Visible = true;
                        divTraveldoc.Style.Add("height", "230px;");

                        btnaddnew.Visible = false;
                        btnsave.Visible = true && auth.IsUpdate;
                        btncancel.Visible = true;
                        break;
                    case "Edit":
                        tblview.Visible = true;
                        divTraveldoc.Style.Add("height", "230px;");

                        btnaddnew.Visible = false;
                        btnsave.Visible = true && auth.IsUpdate;
                        btncancel.Visible = true;
                        break;
                    default:
                        tblview.Visible = false;
                        divTraveldoc.Style.Add("height", "330px;");

                        btnaddnew.Visible = true && auth.IsAdd;
                        btnsave.Visible = false;
                        btncancel.Visible = false;
                        break;
                }
            }
    }
    private void BindGrid()
    {
        int EmpId = Common.CastAsInt32(Session["ProfileId"]);
        if (EmpId > 0)
        {
            DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("select CertificateDocId,EmpId,CertificateName,CertificateNo,replace(convert(varchar,IssueDate,106),' ','-') as IssueDate,replace(convert(varchar,ExpiryDate,106),' ','-') as ExpiryDate ,[FileName],FileImage from HR_CertificateDocumentDetails WHERE  EMPID=" + EmpId.ToString());
            RptTravelDoc.DataSource = dt;
            RptTravelDoc.DataBind();
        }
    }
    public void ShowRecord(int Id)
    {
        int EmpId = Common.CastAsInt32(Session["ProfileId"]);
        if (EmpId > 0)
        {
            DataTable dtdata = Common.Execute_Procedures_Select_ByQueryCMS("select * from HR_CertificateDocumentDetails WHERE CertificateDocId=" + Id.ToString());
            if (dtdata != null)
                if (dtdata.Rows.Count > 0)
                {
                    DataRow dr = dtdata.Rows[0];
                    txtCertificateName.Text = dr["CertificateName"].ToString().Trim();
                    txtCertificateNo.Text = dr["CertificateNo"].ToString().Trim(); 
                    txtIssuedate.Text = Convert.ToDateTime(dr["IssueDate"]).ToString("dd-MMM-yyyy").Trim();
                    if (dr["ExpiryDate"].ToString() != String.Empty)
                    {
                        txtExpirydate.Text = Convert.ToDateTime(dr["ExpiryDate"]).ToString("dd-MMM-yyyy").Trim();
                    }
                    else
                    {
                        txtExpirydate.Text = "";
                    }
                }
        }
    }
    protected void ClearControls()
    {
        txtCertificateName.Text = "";
        txtCertificateNo.Text = "";  
        txtIssuedate.Text = "";
        txtExpirydate.Text = "";
    }
    #endregion

    #region --- Control Events ---
    protected void btndocView_Click(object sender, ImageClickEventArgs e)
    {
        SelectedId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        BindGrid();
        ShowRecord(SelectedId);
        setButtons("View");
    }
    protected void btndocedit_Click(object sender, ImageClickEventArgs e)
    {
        SelectedId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        BindGrid();
        ShowRecord(SelectedId);
        setButtons("Edit");
    }
    protected void btndocDelete_Click(object sender, ImageClickEventArgs e)
    {
        SelectedId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        int EmpId = Common.CastAsInt32(Session["ProfileId"]);
        if (EmpId > 0)
        {
            try
            {
                DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("delete from HR_CertificateDocumentDetails where CertificateDocId=" + SelectedId);
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "error", "alert('Record Deleted Successfully');", true);
                BindGrid();
                setButtons("Cancel"); 
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "error", "alert('Record Not Deleted');", true);
                return;
            }
        }
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
            DateTime date_Issue, date_Expiry;
            if (!(DateTime.TryParse(txtIssuedate.Text, out date_Issue)))
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "error", "alert('Issue Date is Incorrect.');", true);
                return;
            }

            if (txtExpirydate.Text != "")
            {

                if (!(DateTime.TryParse(txtExpirydate.Text, out date_Expiry)))
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "error", "alert('Expiry Date is Incorrect.');", true);
                    return;
                }

                if (date_Issue > date_Expiry)
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Issue Date should Not be greater than Expiry Date.');", true);
                    return;
                }
            }
            object d1 = (txtIssuedate.Text.Trim() == "") ? DBNull.Value : (object)txtIssuedate.Text;
            object d2 = (txtExpirydate.Text.Trim() == "") ? DBNull.Value : (object)txtExpirydate.Text;

            FileUpload img = (FileUpload)fldocument;
            Byte[] imgByte = new Byte[0];
            string FileName = "";
            if (img.HasFile && img.PostedFile != null)
            {
                HttpPostedFile File = fldocument.PostedFile;
                FileName = fldocument.FileName.Trim();
                imgByte = new Byte[File.ContentLength];
                File.InputStream.Read(imgByte, 0, File.ContentLength);
            }
    
            int EmpId = Common.CastAsInt32(Session["ProfileId"]);
            Common.Set_Procedures("HR_InsertUpdateCertificateDocDetails");
            Common.Set_ParameterLength(8);
            Common.Set_Parameters(new MyParameter("@CertificateDocId", SelectedId),
                new MyParameter("@EmpId", EmpId),
                new MyParameter("@CertificateName", txtCertificateName.Text.Trim()),
                new MyParameter("@CertificateNo", txtCertificateNo.Text.Trim()),
                new MyParameter("@IssueDate", d1),
                new MyParameter("@ExpiryDate", d2),
                new MyParameter("@FileName", FileName),
                new MyParameter("@FileImage", imgByte));
            DataSet ds = new DataSet();
            if (Common.Execute_Procedures_IUD_CMS(ds))
            {
                SelectedId = Common.CastAsInt32(ds.Tables[0].Rows[0][0]);
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Record saved successfully.');", true);
                BindGrid();
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Unable to save record.');", true);
            }
    }
    protected void btnaddnew_Click(object sender, EventArgs e)
    {
        SelectedId = 0;
        setButtons("Add");
        ClearControls();
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        SelectedId = 0;
        BindGrid();
        setButtons("Cancel"); 
    }
    #endregion
}
