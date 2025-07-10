using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Text;

public partial class emtm_Emtm_Familydetails : System.Web.UI.Page
{
    public AuthenticationManager auth;
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

    # region User Functions
    //-- COMMON FUNCTIONS
    protected void setButtons(string Action)
    {
        string EmpMode = Session["EmpMode"].ToString();

        if (EmpMode == "View")
        {
            switch (Action)
            {
                case "View":
                    tblview.Visible = true;
                    divfamily.Style.Add("height", "95px;");

                    btnaddnew.Visible = false;
                    btnsave.Visible = false;
                    btncancel.Visible = true;
                    break;
                default:
                    tblview.Visible = false;
                    divfamily.Style.Add("height", "403px;");

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
                    divfamily.Style.Add("height", "95px;");
                    
                    btnaddnew.Visible = false;
                    btnsave.Visible = false;
                    btncancel.Visible = true;
                    break;
                case "Add":
                    tblview.Visible = true;
                    divfamily.Style.Add("height", "95px;");
                    
                    btnaddnew.Visible = false;
                    btnsave.Visible = true && auth.IsUpdate;  
                    btncancel.Visible = true;
                    break;
                case "Edit":
                    tblview.Visible = true;
                    divfamily.Style.Add("height", "95px;");
                    
                    btnaddnew.Visible = false;
                    btnsave.Visible = true && auth.IsUpdate;  
                    btncancel.Visible = true;
                    break;
                default:
                    tblview.Visible = false;
                    divfamily.Style.Add("height", "400px;");
                    
                    btnaddnew.Visible = true && auth.IsAdd;
                    btnsave.Visible = false;
                    btncancel.Visible = false;
                    break;
            }
        }
    }
    protected void BindGrid()
    {
        int EmpId = Common.CastAsInt32(Session["EmpId"]);
        if (EmpId > 0)
        {
            string sql="select a.FamilyId,a.FirstName +' ' + isnull(a.LastName,'') name, replace(convert(varchar,a.dateofbirth,106),' ','-') as dateofbirth , " +
            "case when a.gender='M' then 'Male' else 'Female' end as gender, b.RelationName ,a.PstFileName,a.PstFileImage,a.FinFileName,a.FinFileImgae  " +
            "from HR_familyDetails a Left Outer Join HR_Relation b " +
            "on a.relation= b.relationId WHERE EMPID=" + EmpId.ToString(); 
            DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
            rptRunningHour.DataSource = dt;
            rptRunningHour.DataBind();
        }
    }
    protected void ShowRecord(int ID)
    {
        int EmpId = Common.CastAsInt32(Session["EmpId"]);
        if (EmpId > 0)
        {
            DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("SELECT * FROM HR_familyDetails WHERE FamilyId=" + ID.ToString());
            if (dt != null)
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];

                    txtFirstName.Text = dr["FirstName"].ToString();
                    txtLastname.Text = dr["LastName"].ToString(); 

                    txtdob.Text = Convert.ToDateTime(dr["DateofBirth"]).ToString("dd-MMM-yyyy");
                    ddlgender.SelectedValue = dr["Gender"].ToString();

                    txtAddress1.Text = (dr["Address1"].ToString() == string.Empty) ? "" : dr["Address1"].ToString();
                    txtAddress2.Text = (dr["Address2"].ToString() == string.Empty) ? "" : dr["Address2"].ToString();
                    txtAddress3.Text = (dr["Address3"].ToString() == string.Empty) ? "" : dr["Address3"].ToString();
                    txtAddress4.Text = (dr["Address4"].ToString() == string.Empty) ? "" : dr["Address4"].ToString(); 

                    txtTelephone.Text = dr["Telephone"].ToString();
                    txtMobile.Text = dr["Mobile"].ToString();
                    txtEmail.Text = dr["Email"].ToString(); 
                    ddlRelation.SelectedValue = dr["Relation"].ToString();
                    txtpassport.Text = dr["PassportNo"].ToString();

                    txtpassportissuedate.Text = ReturnDate(dr["PassportIssueDate"]);
                    txtpassportexpdate.Text = ReturnDate(dr["PassportExpiryDate"]);

                    txtplaceofissue.Text = dr["PlaceofIssue"].ToString();
                    txtfinnric.Text = dr["Fin_NricNo"].ToString();
                    txtfinnrictype.Text = dr["Fin_NricType"].ToString();

                    txtfinissuedate.Text =  ReturnDate(dr["FinIssueDate"]);
                    txtfinexpirydate.Text = ReturnDate(dr["FinExpiryDate"]);

                    string File;
                    File="Family_" + ID + ".jpg";
                    img_Family.ImageUrl = "~/EMANAGERBLOB/HRD/Family" + "/" + File;
                }
        }
    }
    protected void ClearControls()
    {
        txtFirstName.Text = "";
        txtLastname.Text = ""; 
        txtdob.Text = "";
        ddlgender.SelectedIndex = 0;
        txtAddress1.Text = "";
        txtAddress2.Text = "";
        txtAddress3.Text = "";
        txtAddress4.Text = "";
        txtTelephone.Text = "";
        txtMobile.Text = "";
        txtEmail.Text = "";
        ddlRelation.SelectedIndex = 0;
        txtpassport.Text = "";
        txtpassportissuedate.Text = "";
        txtpassportexpdate.Text = "";
        txtplaceofissue.Text = "";
        txtfinnric.Text = "";
        txtfinnrictype.Text = "";  
        txtfinissuedate.Text = "";
        txtfinexpirydate.Text = "";
        //txtbankname.Text = "";
        //txtbranchname.Text = "";
        //txtaccountno.Text = "";
        //chkswift.Checked = false;
        //txtiban.Text = "";
        //txtaddress.Text = "";
    }
    protected bool CheckDate(String date)
    {
        if (date.Trim() == "")
        {
            return true;
        }
        try
        {
            DateTime dt = DateTime.Parse(date);
            return true;
        }
        catch
        {
            return false;

        }
    }
    protected string ReturnDate(Object date)
    {
        if (date == DBNull.Value || date==null)
        {
            return ""; 
        }
        try
        {
            DateTime dt = DateTime.Parse(date.ToString());
            return dt.ToString("dd-MMM-yyyy");
        }
        catch
        {
            return ""; 
        }
    }
    #endregion

    #region page Load and Events
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 247);
        if (chpageauth <= 0)
        {
            Response.Redirect("../AuthorityError.aspx");
        }
        //*******************
        auth = new AuthenticationManager(247, Common.CastAsInt32(Session["loginid"]), ObjectType.Page);

        this.img_Family.Attributes.Add("onclick", "javascript:Show_Image_Large(this);");

        Session["CurrentModule"] = 3;
        DataSet ds = new DataSet();

        BindGrid();

        if (!IsPostBack)
        {
            if (Session["EmpMode"].ToString() == "Add")
            {
                Response.Redirect("~/Modules/eOffice/StaffAdmin/HR_Personaldetail.aspx");
            }
            ControlLoader.LoadControl(ddlRelation, DataName.HR_Relation, "Select", "");
            ControlLoader.LoadControl(ddlgender, DataName.Gender, "Select", "");
            lbl_EmpName.Text = Session["EmpName"].ToString();
            setButtons("");
        }
    }
    protected void btnView_Click(object sender, ImageClickEventArgs e)
    {
        btnsave.Visible = false;
        SelectedId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        ShowRecord(SelectedId);
        // Again Binding because of Repeator Color Row
        BindGrid();
        setButtons("View");
    }
    protected void btnedit_Click(object sender, ImageClickEventArgs e)
    {
        btnsave.Visible = true;
        SelectedId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        ShowRecord(SelectedId);
        // Again Binding because of Repeator Color Row
        BindGrid();
        setButtons("Edit");
    }
    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        SelectedId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        int EmpId = Common.CastAsInt32(Session["EmpId"]);
        if (EmpId > 0)
        {
            try
            {
                DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("Delete From HR_familyDetails where FamilyId=" + SelectedId);
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
    protected void rptRunningHour_ItemCreated(object sender, RepeaterItemEventArgs e)
    {
        //if (e.Item.ItemType == ListItemType.Item | e.Item.ItemType == ListItemType.AlternatingItem)
        //{
        //    TableRow TR = (TableRow)e.Item.Controls[0];
        //    TableCell TC = TR.Cells[0];
        //    TC.Visible = false;
        //}
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        object d1 = (txtdob.Text.Trim() == "") ? DBNull.Value : (object)txtdob.Text;
        object d2 = (txtpassportissuedate.Text.Trim() == "") ? DBNull.Value : (object)txtpassportissuedate.Text;
        object d3 = (txtpassportexpdate.Text.Trim() == "") ? DBNull.Value : (object)txtpassportexpdate.Text;
        object d4 = (txtfinissuedate.Text.Trim() == "") ? DBNull.Value : (object)txtfinissuedate.Text;
        object d5 = (txtfinexpirydate.Text.Trim() == "") ? DBNull.Value : (object)txtfinexpirydate.Text;

        if (!CheckDate(txtdob.Text))
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('DateofBirth is Incorrect.');", true);
            return;
        }

        if (!CheckDate(txtpassportissuedate.Text))
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Passport Issue Date is Incorrect.');", true);
            return;
        }

        if (!CheckDate(txtpassportexpdate.Text))
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Passport Expiry Date is Incorrect.');", true);
            return;
        }

        if (!CheckDate(txtfinissuedate.Text))
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Fin Issue Date is Incorrect.');", true);
            return;
        }

        if (!CheckDate(txtfinissuedate.Text))
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Fin Expiry Date is Incorrect.');", true);
            return;
        }

         FileUpload img = (FileUpload)flPassportdocument;
            Byte[] PstimgByte = new Byte[0];
            string PstFileName = "";
            if (img.HasFile && img.PostedFile != null)
            {
                HttpPostedFile File = flPassportdocument.PostedFile;
                PstFileName = flPassportdocument.FileName.Trim();
                PstimgByte = new Byte[File.ContentLength];
                File.InputStream.Read(PstimgByte, 0, File.ContentLength);
            }


        FileUpload Fin_img = (FileUpload)flFinNricdocument;
            Byte[] FinimgByte = new Byte[0];
            string FinFileName = "";
            if (Fin_img.HasFile && Fin_img.PostedFile != null)
            {
                HttpPostedFile File = flFinNricdocument.PostedFile;
                FinFileName = flFinNricdocument.FileName.Trim();
                FinimgByte = new Byte[File.ContentLength];
                File.InputStream.Read(FinimgByte, 0, File.ContentLength);
            }

        int EmpId = Common.CastAsInt32(Session["EmpId"]);
        Common.Set_Procedures("HR_InsertUpdateFamilyDetails");
        Common.Set_ParameterLength(26);
        Common.Set_Parameters(new MyParameter("@FamilyId", SelectedId),
            new MyParameter("@EmpId", EmpId),
            new MyParameter("@FirstName", txtFirstName.Text.Trim()),
            new MyParameter("@LastName", txtLastname.Text.Trim()),
            new MyParameter("@DateofBirth", d1),
            new MyParameter("@Gender", ddlgender.SelectedValue.Trim()),
            new MyParameter("@Address1", txtAddress1.Text.Trim()),
            new MyParameter("@Address2", txtAddress2.Text.Trim()),
            new MyParameter("@Address3", txtAddress3.Text.Trim()),
            new MyParameter("@Address4", txtAddress4.Text.Trim()),
            new MyParameter("@Telephone", txtTelephone.Text.Trim()),
            new MyParameter("@Mobile", txtMobile.Text.Trim()),
            new MyParameter("@Email", txtEmail.Text.Trim()),
            new MyParameter("@Relation", ddlRelation.SelectedValue.Trim()),
            new MyParameter("@PassportNo", txtpassport.Text.Trim()),
            new MyParameter("@PassportIssueDate", d2),
            new MyParameter("@PassportExpiryDate", d3),
            new MyParameter("@PlaceofIssue", txtplaceofissue.Text.Trim()),
            new MyParameter("@PstFileName", PstFileName),
            new MyParameter("@PstFileImage", PstimgByte),
            new MyParameter("@Fin_NricNo", txtfinnric.Text.Trim()),
            new MyParameter("@Fin_NricType", txtfinnrictype.Text.Trim()),
            new MyParameter("@FinIssueDate", d4),
            new MyParameter("@FinExpiryDate", d5),
            new MyParameter("@FinFileName", FinFileName),
            new MyParameter("@FinFileImgae", FinimgByte));

        DataSet ds = new DataSet();

        if (Common.Execute_Procedures_IUD_CMS(ds))
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                string FileName,FamilyId;
                FamilyId = ds.Tables[0].Rows[0][0].ToString();
                if (FileUpload1.HasFile)
                {
                    FileName = "Family_" + FamilyId + Path.GetExtension(FileUpload1.PostedFile.FileName);
                    FileUpload1.SaveAs(Server.MapPath("~/EMANAGERBLOB/HRD/Family") + "/" + FileName);
                }
            }
            SelectedId = Common.CastAsInt32(ds.Tables[0].Rows[0]["FamilyId"]); 
            BindGrid();
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Record saved successfully.');", true);
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
    protected void imgbtnPassport_Click(object sender, ImageClickEventArgs e)
    {
        if (SelectedId > 0)
        {
            DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("SELECT PSTFILENAME,PSTFILEIMAGE,FINFILENAME,FINFILEIMGAE FROM HR_familyDetails WHERE FamilyId=" + SelectedId.ToString());
            if (dt != null)
                if (dt.Rows.Count > 0)
                {
                    byte[] buff = (byte[])dt.Rows[0]["PSTFILEIMAGE"];
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + dt.Rows[0]["PSTFILENAME"].ToString());
                    Response.BinaryWrite(buff);
                    Response.Flush();
                    Response.End();
                }
        }
    }
    protected void imgbtnFin_Click(object sender, ImageClickEventArgs e)
    {
        if (SelectedId > 0)
        {
            DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("SELECT PSTFILENAME,PSTFILEIMAGE,FINFILENAME,FINFILEIMGAE FROM HR_familyDetails WHERE FamilyId=" + SelectedId.ToString());
            if (dt != null)
                if (dt.Rows.Count > 0)
                {
                    byte[] buff = (byte[])dt.Rows[0]["FINFILEIMGAE"];
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + dt.Rows[0]["FINFILENAME"].ToString());
                    Response.BinaryWrite(buff);
                    Response.Flush();
                    Response.End();
                }
        }
    }
   }
