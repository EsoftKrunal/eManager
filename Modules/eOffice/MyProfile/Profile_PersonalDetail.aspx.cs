using System;
using System.Data;
using System.Configuration;
using System.Reflection;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;

using ShipSoft.CrewManager.BusinessLogicLayer;
using ShipSoft.CrewManager.BusinessObjects;
using ShipSoft.CrewManager.Operational;


public partial class Emtm_Profile_PersonalDetail : System.Web.UI.Page
{
    public AuthenticationManager auth; 
    # region User Functions
    private void BindShirtSizeDropDown()
    {
        FieldInfo[] thisEnumFields = typeof(ShirtSize).GetFields();
        foreach (FieldInfo thisField in thisEnumFields)
        {
            if (!thisField.IsSpecialName && thisField.Name.ToLower() != "notset")
            {
                int thisValue = (int)thisField.GetValue(0);
                ddlShirtSize.Items.Add(new ListItem(thisField.Name, thisValue.ToString()));
            }
        }
        ddlShirtSize.Items.Insert(0, new ListItem("< Select > ", ""));
    }
    public void BindReportingmanager()
    {
        int EmpId = Common.CastAsInt32(Session["ProfileId"]);
        if (EmpId > 0)
        {
            DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("SELECT (FirstName + ' ' + FamilyName) AS EmpName ,* FROM Hr_PersonalDetails WHERE EMPID <>" + EmpId.ToString());
            if (dt != null)
                if (dt.Rows.Count > 0)
                {
                    ddlReportingTo.DataSource = dt;
                    ddlReportingTo.DataTextField = "EmpName";
                    ddlReportingTo.DataValueField = "EmpId";
                    ddlReportingTo.DataBind();
                }
        }

        ddlReportingTo.Items.Insert(0, new ListItem("< Select >", "0"));

    }
    public void Clear_Controls()
    {
        txt_FirstName.Text = "";
        txt_Middlename.Text = "";
        txt_familyName.Text = "";
        txt_Passport.Text = "";
        txt_Status.Text = "";
        txt_DOB.Text = "";
        txt_Age.Text = "";
        txt_placeofbirth.Text = "";
        ddlcob.SelectedIndex = 0;
        ddlgender.SelectedIndex = 0;
        ddlcivilstatus.SelectedIndex = 0;
        ddlbloodgroup.SelectedIndex = 0;
        txtheight.Text = "";
        txtweight.Text = "";
        txt_Bmi.Text = "";
        txtdatefirstjoin.Text = "";
        ddlposition.SelectedIndex = 0;
        ddloffice.SelectedIndex = 0;
    }
    protected void ShowRecord()
    {
        int EmpId = Common.CastAsInt32(Session["ProfileId"]);
        if (EmpId > 0)
        {
           DataTable dt=Common.Execute_Procedures_Select_ByQueryCMS("SELECT * FROM Hr_PersonalDetails WHERE EMPID=" + EmpId.ToString()); 
            if(dt!=null )
                if (dt.Rows.Count > 0)
                {
                    DataRow dr=dt.Rows[0];
                    lbl_EmpName.Text = "[ " + dr["EmpCode"].ToString() + " ] " + dr["FirstName"].ToString() + " " + dr["MiddleName"].ToString() + " " + dr["FamilyName"].ToString();
                    Session["ProfileName"] = (lbl_EmpName.Text.ToString() == string.Empty) ? "" : lbl_EmpName.Text.ToString();
                    // temp
                    Session["ProfileMode"] = "Edit";

                    txt_FirstName.Text = dr["FirstName"].ToString();
                    txt_Middlename.Text = dr["MiddleName"].ToString();
                    txt_familyName.Text = dr["FamilyName"].ToString();

                    txt_Passport.Text = dr["PassportNo"].ToString();
                    txt_nricfin.Text = dr["NRIC_FIN"].ToString();

                    if (dr["Status"].ToString() == "P")
                    {
                        txt_Status.Text = "PERMANENT";
                    }
                    else if (dr["Status"].ToString() == "R")
                    {
                        txt_Status.Text = "PROBATION"; 
                    }
                    else if (dr["Status"].ToString() == "I")
                    {
                        txt_Status.Text = "INACTIVE";
                    }

                    txt_DOB.Text =Common.ToDateString(dr["DateOfBirth"]);

                    DateTime date_today = System.DateTime.Now.Date;
                    TimeSpan t1 = date_today - Convert.ToDateTime(txt_DOB.Text);
                    int cal_age = (Convert.ToInt32(t1.TotalDays) / 365);
                    txt_Age.Text = cal_age.ToString();

                    txt_placeofbirth.Text = dr["POB"].ToString();
                    ddlcob.SelectedValue = dr["COB"].ToString();
                    ddlnationality.SelectedValue = dr["Nationality"].ToString();
                    ddlgender.SelectedValue = dr["Gender"].ToString();
                    ddlcivilstatus.SelectedValue = dr["CivilStatus"].ToString();

                    ddlbloodgroup.SelectedValue = dr["BloodGroup"].ToString();
                    ddloffice.SelectedValue = dr["Office"].ToString();
                    ControlLoader.LoadControl(ddlposition, DataName.Position, "Select", "0", "officeid=" + ddloffice.SelectedValue);
                    ControlLoader.LoadControl(ddldepartment, DataName.HR_Department, "Select", "0", "OfficeId=" + ddloffice.SelectedValue);     

                    txtheight.Text = dr["Height"].ToString();
                    txtweight.Text = dr["Weight"].ToString();
                    txt_Bmi.Text = dr["BMI"].ToString();

                    txtdatefirstjoin.Text = Common.ToDateString(dr["DJC"]);
                    txtdatefirstjoin.Text =  Common.ToDateString(dr["DJC"]); 
                    ddlposition.SelectedValue=dr["Position"].ToString();

                    ddldepartment.SelectedValue = dr["Department"].ToString();
                    chkHOD.Checked = dr["HOD"].ToString() == "True";

                    ddlPeapCat.SelectedValue = dr["PID"].ToString() == "" ? "0" : dr["PID"].ToString();
                    lblPeapCat.Text = ddlPeapCat.SelectedValue.ToString() == "0" ? "" : ddlPeapCat.SelectedItem.Text.ToString();

                    ddlReportingTo.SelectedValue = dr["ReportingTo"].ToString() == "" ? "0" : dr["ReportingTo"].ToString();
                    lblReportingTo.Text = ddlReportingTo.SelectedValue.ToString() == "0" ? "" : ddlReportingTo.SelectedItem.Text.ToString();

                    ddlShirtSize.SelectedValue = dr["ShirtSize"].ToString() == "" ? "0" : dr["ShirtSize"].ToString();

                    if (System.IO.File.Exists(Server.MapPath("../EmpImage/Emtm_" + EmpId.ToString() + ".jpg")))
                        img_Crew.ImageUrl = "../EmpImage/Emtm_" + EmpId.ToString() + ".jpg";
                    else
                        img_Crew.ImageUrl = "../../Images/emtm/noimage.jpg";
                }
        }
    }
    protected void Update_BMI(object sender, EventArgs e)
    {
        decimal height = Common.CastAsInt32(txtheight.Text);
        int weight = Common.CastAsInt32(txtweight.Text);
        decimal BMI = 0;
        if (height > 0)
        {
            BMI = Math.Round(weight / ((height / 100) * (height / 100)), 2);
        }
        txt_Bmi.Text = BMI.ToString();
    }
    #endregion
    //-------------
    # region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 255);
        if (chpageauth <= 0)
        {
            Response.Redirect("../AuthorityError.aspx");
        }
        //*******************
        auth = new AuthenticationManager(255, Common.CastAsInt32(Session["loginid"]), ObjectType.Page);


        Session["CurrentModule"] = 1;
        this.img_Crew.Attributes.Add("onclick", "javascript:Show_Image_Large(this);");
        //---------------------------
        if (!IsPostBack)
        {
            BindShirtSizeDropDown();
            BindReportingmanager();
            ControlLoader.LoadControl(ddlgender,DataName.Gender, "Select", "");
            ControlLoader.LoadControl(ddlbloodgroup, DataName.BloodGroup, "Select", "");
            ControlLoader.LoadControl(ddlcivilstatus, DataName.CivilStatus, "Select", "");
            ControlLoader.LoadControl(ddlnationality, DataName.country, "Select", "0");
            ControlLoader.LoadControl(ddlcob, DataName.country, "Select", "0");
            ControlLoader.LoadControl(ddloffice, DataName.Office, "Select", "0");
            ControlLoader.LoadControl(ddlPeapCat, DataName.HR_PeapCategory, "Select", "0");


            ShowRecord();

            //btnsave.Visible = true && auth.IsUpdate;
           
                //ControlLoader.LoadControl(ddlposition, DataName.Position, "Select", "0", "officeid=" + ddloffice.SelectedValue);
                //ControlLoader.LoadControl(ddldepartment, DataName.HR_Department, "Select", "0", "OfficeId=" + ddloffice.SelectedValue);     
        }
        //---------------------------
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        DateTime date_DOB, date_DJC;
        DateTime date_today = System.DateTime.Now.Date;
        TimeSpan t1 = date_today - Convert.ToDateTime(txt_DOB.Text);
        int cal_age = (Convert.ToInt32(t1.TotalDays) / 365);

        if (cal_age <= 18)
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Employee Must be at least 18 Years Old.');", true);
            return;
        }

        date_DOB =Convert.ToDateTime(txt_DOB.Text);
        date_DJC = Convert.ToDateTime(txtdatefirstjoin.Text);
        txt_Age.Text = cal_age.ToString();

        TimeSpan ts = date_DOB - date_DJC;

        int days = ts.Days;


        if (days > 0)
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('DJC can not be less than DOB');", true);
            return;
        }
        if (this.FileUpload1 != null && this.FileUpload1.FileContent.Length > 0)
        {
            HttpPostedFile file = FileUpload1.PostedFile;

            string ext = Path.GetExtension(FileUpload1.FileName);
            if (ext == ".jpg" )
            {}
            else
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "zz", "alert('Uploading file type should be jpg only.');", true);
                return;
            }

        }
         
       
        int EmpId = Common.CastAsInt32(Session["ProfileId"]);
        Common.Set_Procedures("HR_InsertUpdatePersonalDetails");
        Common.Set_ParameterLength(26);
        Common.Set_Parameters(new MyParameter("@EmpId", EmpId),
            new MyParameter("@FirstName", txt_FirstName.Text.Trim()),
            new MyParameter("@MiddleName", txt_Middlename.Text.Trim()),
            new MyParameter("@FamilyName", txt_familyName.Text.Trim()),
            new MyParameter("@PassportNo", txt_Passport.Text.Trim()),
            new MyParameter("@DateOfBirth", txt_DOB.Text.Trim()),
            new MyParameter("@Age", cal_age),
            new MyParameter("@POB", txt_placeofbirth.Text.Trim()),
            new MyParameter("@COB", ddlcob.SelectedValue.Trim()),
            new MyParameter("@Nationality", ddlnationality.SelectedValue.Trim()),
            new MyParameter("@Gender", ddlgender.SelectedValue.Trim()),
            new MyParameter("@CivilStatus", ddlcivilstatus.SelectedValue.Trim()),
            new MyParameter("@BloodGroup", ddlbloodgroup.SelectedValue.Trim()),
            new MyParameter("@Height", txtheight.Text.Trim()),
            new MyParameter("@Weight", txtweight.Text.Trim()),
            new MyParameter("@BMI", txt_Bmi.Text.Trim()),
            new MyParameter("@DJC ", txtdatefirstjoin.Text.Trim()),
            new MyParameter("@Position ", ddlposition.Text.Trim()),
            new MyParameter("@Office", ddloffice.SelectedValue.Trim()),
            new MyParameter("@NRIC_FIN ", txt_nricfin.Text.Trim()),
            new MyParameter("@Department ", ddldepartment.SelectedValue.Trim()),
            new MyParameter("@PID ", ddlPeapCat.SelectedValue.Trim()),
            new MyParameter("@ShirtSize ", ddlShirtSize.SelectedValue.Trim()),
            new MyParameter("@ReportingTo ", ddlReportingTo.SelectedValue.Trim()),
            new MyParameter("@Status ", ""),
            new MyParameter("@HOD ", (chkHOD.Checked)?1:0));
        DataSet ds = new DataSet();

        if (Common.Execute_Procedures_IUD_CMS(ds))
        {
            Session["ProfileMode"]= "Edit";
            Session["ProfileId"]=ds.Tables[0].Rows[0]["EmpId"];
            lbl_EmpName.Text = "[ " + ds.Tables[0].Rows[0]["EmpCode"].ToString() + " ] " + txt_FirstName.Text.Trim() + " " + txt_Middlename.Text.Trim() + " " + txt_familyName.Text.Trim();

            if (this.FileUpload1 != null && this.FileUpload1.FileContent.Length > 0)
            {
                HttpPostedFile file = FileUpload1.PostedFile;
                FileUpload1.SaveAs(Server.MapPath("~/emtm/EmpImage/Emtm_" + EmpId.ToString() + ".jpg"));
                img_Crew.ImageUrl = "../EmpImage/Emtm_" + EmpId.ToString() + ".jpg&rand=" + (new Random()).Next(1000);
                ShowRecord();
            }
            ScriptManager.RegisterStartupScript(Page,this.GetType(),"success","alert('Record saved successfully.');",true);   
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Unable to save record.');", true);   
        }
    }
    protected void txt_DOB_TextChanged(object sender, EventArgs e)
    {
        try
        {
            DateTime dt = Convert.ToDateTime(txt_DOB.Text);
            txt_Age.Text=Convert.ToInt32((DateTime.Today.Subtract(dt)).Days / 365).ToString(); 
        }
        catch
        {
        txt_Age.Text="";
        }

    }
    #endregion
  
}

    