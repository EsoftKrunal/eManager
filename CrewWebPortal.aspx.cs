using System;
using System.Data;
using System.Configuration;
using System.Reflection;
using System.Collections;
using System.Web;
using System.Data.Common;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;

public partial class CrewWebPortal : System.Web.UI.Page
{
    int UserId;
    int MaxHeight = 350, Minheight = 85;
    public void BindGridFiler()
    {
        string WhereClause = "";
        if (ddlsStatus.SelectedIndex == 1)
        {
            WhereClause = " and um.StatusId='A'";
        }
        if (ddlsStatus.SelectedIndex ==2)
        {
            WhereClause = " and um.StatusId='D'";
        }

        rpt_Data.DataSource = Common.Execute_Procedures_Select_ByQuery("select HRPD.EmpCode,LoginId,isnull(RoleName,'') as RoleName,UM.UserId,UM.FirstName+' ' +UM.lastname as Name,convert(Varchar,um.DateofBirth,101) as DOB,Email," +
                                                                             "case when statusid='A' then 'Active' else 'In-Active' end as Status " +
                                                                             "From usermaster UM left join RoleMaster RM on RM.RoleId=UM.RoleId left join DBO.Hr_PersonalDetails HRPD on HRPD.UserId=um.LoginId  Where ltrim(rtrim(um.userid)) like '%" + myTextBox.Text.Trim() + "%' "+ WhereClause + " order by UM.firstname,UM.lastname");
       rpt_Data.DataBind();
    }
    
   
    public void bindRoleNameDDL()
    {
        DataTable dt4 = Common.Execute_Procedures_Select_ByQuery("SELECT ROLEID,ROLENAME FROM ROLEMASTER");
        this.ddlRoleName.DataValueField = "ROLEID";
        this.ddlRoleName.DataTextField = "RoleName";
        this.ddlRoleName.DataSource = dt4;
        this.ddlRoleName.DataBind();
    }
    //public void bindDepartmentDDL()
    //{
    //    DataTable dt41 = Common.Execute_Procedures_Select_ByQuery_CMS("SELECT DEPARTMENTID,DEPARTMENTNAME FROM DEPARTMENT");
    //    this.ckldepartment.DataValueField = "DepartmentId";
    //    this.ckldepartment.DataTextField = "DepartmentName";
    //    this.ckldepartment.DataSource = dt41;
    //    this.ckldepartment.DataBind();
    //}
    public void bindRecruitingOfficeList()
    {
        DataTable dt67 = ECommon.Execute_Procedures_Select_ByQuery_CMS("SELECT * FROM recruitingoffice");
        this.chklstRecruitingOffice.DataValueField = "RecruitingOfficeId";
        this.chklstRecruitingOffice.DataTextField = "RecruitingOfficeName";
        this.chklstRecruitingOffice.DataSource = dt67;
        this.chklstRecruitingOffice.DataBind();
    }
    public void bindStatusDDL()
    {
        this.ddlStatus.Items.Add(new ListItem("Active", "A"));
        this.ddlStatus.Items.Add(new ListItem("In-Active", "D"));
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] == null)
        {
            Response.Redirect("Login.aspx");
        }
        Session["Home Page"] = "Home Page";
        UserId = int.Parse(Session["UserId"].ToString());
        if (!Page.IsPostBack)
        {
            //--------------
            ViewState["SelId"] = 0;
            
            bindRoleNameDDL();
            //bindDepartmentDDL();
            bindRecruitingOfficeList();
            bindStatusDDL();
            btn_Save.Visible = false;
            btn_Cancel.Visible = false;
            pnlList.Height = Unit.Pixel(MaxHeight);
            BindGridFiler();
        }
        txtPassword.Attributes.Add("value", txtPassword.Text);
        txtConfirmPassword.Attributes.Add("value", txtConfirmPassword.Text);
    }
    protected void EditClick(object sender, ImageClickEventArgs e)
    {
        int Recid=int.Parse(((ImageButton)sender).CommandArgument);
        Show_Record(Recid);
        ViewState["SelId"] = Recid.ToString();
        BindGridFiler(); 
        pnl_UserLogin.Visible = true;
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        if (! Validator.ValidateUserName(txtUserId.Text))
        {
            Msgbox.ShowMessage("Please enter a valid username (4-15) Chars.",true);
            txtUserId.Focus();  
            return; 
        }
        //if (txtPassword.Text.Trim().Contains("#"))
        //{
        //    Msgbox.ShowMessage("Invalid char in password. (#) not allowed.",true);
        //    txtPassword.Focus();  
        //    return; 
        //}

        if (!Validator.ValidatePassword(txtPassword.Text))
        {
            Msgbox.ShowMessage("Please enter a valid password (4-15) Chars.", true);
            txtPassword.Focus();
            return;
        }
        if (txtPassword.Text.Trim()!=txtConfirmPassword.Text.Trim())
        {
            Msgbox.ShowMessage("Password & confirm password are not matching.", true);
            txtConfirmPassword.Focus();
            return;
        }
        if (txtFirstName.Text.Trim()=="")
        {
            Msgbox.ShowMessage("Please enter Firstname.", true);
            txtFirstName.Focus();
            return;
        }
        if (!Validator.ValidateName(txtFirstName.Text))
        {
            Msgbox.ShowMessage("Please enter valid Firstname.", true);
            txtFirstName.Focus();
            return;
        }
        if (!Validator.ValidateName(txtFirstName.Text))
        {
            Msgbox.ShowMessage("Please enter valid Firstname.", true);
            txtFirstName.Focus();
            return;
        }
        if (!Validator.ValidateName(txtFirstName.Text))
        {
            Msgbox.ShowMessage("Please enter valid Firstname.", true);
            txtFirstName.Focus();
            return;
        }
        txtEmail.Text = txtEmail.Text.ToLower();
        if (!Validator.ValidateEmail(txtEmail.Text.ToLower() ))
        {
            Msgbox.ShowMessage("Please enter valid Email.", true);
            txtFirstName.Focus();
            return;
        }
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM USERMASTER WHERE LTRIM(RTRIM(USERID))='" + txtUserId.Text.Trim() + "' And LoginId<>" + HiddenUserLogin.Value); 
        if (dt!=null)
        {
            if (dt.Rows.Count > 0)
            {
                Msgbox.ShowMessage("Entered Username alredy exist in the database.", true);
                txtUserId.Focus();
                return;
            }
        }

        //string Department = "";
        //for (int i = 0; i < this.ckldepartment.Items.Count; i++)
        //{
        //    if (this.ckldepartment.Items[i].Selected)
        //    {

        //        if (Department == "")
        //        {
        //            Department = ckldepartment.Items[i].Value.ToString();
        //        }
        //        else
        //        {
        //            Department = Department + "," + ckldepartment.Items[i].Value.ToString();
        //        }
        //    }
        //}

        string RecruitingOfc = "";
        for (int r = 0; r < this.chklstRecruitingOffice.Items.Count; r++)
        {
            if (this.chklstRecruitingOffice.Items[r].Selected)
            {

                if (RecruitingOfc == "")
                {
                    RecruitingOfc = chklstRecruitingOffice.Items[r].Value.ToString();
                }
                else
                {
                    RecruitingOfc = RecruitingOfc + "," + chklstRecruitingOffice.Items[r].Value.ToString();
                }
            }
        }

        if (txtEmployeeCode.Text.Trim() == "")
        {
            Msgbox.ShowMessage("Please enter EmployeeCode.", true);
            txtEmployeeCode.Focus();
            return;
        }

        try
        {
            
            string DOB = "";
            if (txtDOB.Text.Trim() == "")
            {
                DOB = "NULL";
            }
            else
            {
                DOB = "'" + txtDOB.Text + "'";
            }

            if (HiddenUserLogin.Value.Trim() == "0")
            {
                Common.Execute_Procedures_Select_ByQuery("Insert Into UserMaster(UserId,RoleId,Password,FirstName,LastName,DateOfBirth,Email,LastLoginOn,SuperUser,CreatedBy,CreatedOn,StatusId,RecruitingOfficeId) values('" + txtUserId.Text + "'," + ddlRoleName.SelectedValue + ",'" + txtPassword.Text + "','" + txtFirstName.Text + "','" + txtLastName.Text + "'," + DOB + ",'" + txtEmail.Text + "',getdate(),'N'," + UserId.ToString() + ",getdate(),'" + ddlStatus.SelectedValue + "','" + RecruitingOfc + "')");
                Msgbox.ShowMessage("User added successfully.", false);

                DataTable dtUser=Common.Execute_Procedures_Select_ByQuery("SELECT MAX(LOGINID) FROM UserMaster");
                int USERLOGINID = 0;
                USERLOGINID = Common.CastAsInt32(dtUser.Rows[0][0].ToString());

                Common.Execute_Procedures_Select_ByQuery("INSERT INTO DBO.uservesselrelation(loginid,vesselid,createdby,createdon) SELECT " + USERLOGINID + ",VESSELID," + UserId.ToString() + ",GETDATE() FROM DBO.VESSEL WHERE VESSELSTATUSID<>2 ");
            }
            else
            {
                

                Common.Set_Procedures("HR_SetProfileUserId");
                Common.Set_ParameterLength(2);
                Common.Set_Parameters(new MyParameter("@EmpId", HiddenUserLogin.Value.Trim()),
                                      new MyParameter("@EmpCode", txtEmployeeCode.Text.Trim()));
                DataSet ds = new DataSet();

                if (Common.Execute_Procedures_IUD(ds))
                {
                    
                }
                else
                {
                    Msgbox.ShowMessage("Employee Code Already Exists.", true);
                    return ; 
                }

                Common.Execute_Procedures_Select_ByQuery("Update UserMaster set UserId='" + txtUserId.Text + "'," +
                "RoleId=" + ddlRoleName.SelectedValue + "," +
                "Password='" + txtPassword.Text + "'," +
                "FirstName='" + txtFirstName.Text + "'," +
                "LastName='" + txtLastName.Text + "'," +
                "DateOfBirth=" + DOB + "," +
                "Email='" + txtEmail.Text + "'," +
                "SuperUser='N'," +
                //"DepartmentId='" + Department + "'," +
                "StatusId='" + ddlStatus.SelectedValue + "'," +
                //"NWC='" + (chkNWC.Checked ? "Y" : "N") + "'," +
                "RecruitingOfficeId='" + RecruitingOfc + "', ModifiedBy=" + UserId.ToString()  +",ModifiedOn=getdate() Where LoginId=" + HiddenUserLogin.Value);

                
                Msgbox.ShowMessage("User updated successfully.", false);

            }
            BindGridFiler();
            pnl_UserLogin.Visible = false;  
            btn_Save .Visible =false;
            btn_Cancel.Visible = false;
            pnlList.Height = Unit.Pixel(MaxHeight); 
        }
        catch
        {
            Msgbox.ShowMessage("Unable to update user information.", true);
        }
    }
    protected void btn_Cancel_Click(object sender, EventArgs e)
    {
        pnl_UserLogin.Visible = false;
        btn_Save.Visible = false ;
        btn_Cancel.Visible = false ;
        pnlList.Height = Unit.Pixel(MaxHeight); 
    }
    protected void Show_Click(object sender, EventArgs e)
    {
        BindGridFiler();
    }
    
    
    
    protected void Show_Record(int Loginid)
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("select loginid,userid,roleid,password,firstname,lastname,convert(varchar,dateofbirth,101) as DOB,Email,SuperUser,DepartmentId, " +
                                                                "RecruitingOfficeId,StatusId,(select UserId from usermaster um where um.loginid=umm.createdby) as CreatedBy,isnull(nwc,'N') AS NWC, " +
                                                                "convert(varchar,createdon,101) as CreatedOn,convert(varchar,modifiedon,101) as modifiedOn, " +
                                                                "(select UserId from usermaster um where um.loginid=umm.modifiedby) as ModifiedBy from usermaster umm where umm.loginid=" + Loginid.ToString());
        if (dt != null)
        {
            if (dt.Rows.Count >0)
            {
                pnlList.Height =Unit.Pixel(Minheight); 
                DataRow dr = dt.Rows[0]; 
                HiddenUserLogin.Value = Loginid.ToString();
                ddlRoleName.SelectedValue = dr["RoleId"].ToString();
                txtUserId.Text = dr["UserId"].ToString();
                txtPassword.Text = dr["Password"].ToString();
                txtPassword.Attributes.Add("value", txtPassword.Text);
                txtConfirmPassword.Text = dr["Password"].ToString(); ;
                txtConfirmPassword.Attributes.Add("value", txtConfirmPassword.Text);
                txtFirstName.Text = dr["FirstName"].ToString();
                txtLastName.Text = dr["LastName"].ToString();
                //txtDOB.Text = dr["DOB"].ToString();
                txtEmail.Text = dr["Email"].ToString();
                //chkNWC.Checked = dr["NWC"].ToString()=="Y";

                chklstRecruitingOffice.ClearSelection();
                //ckldepartment.ClearSelection();
                txtCreatedBy.Text = dr["CreatedBy"].ToString();
                txtCreatedOn.Text = dr["CreatedOn"].ToString();
                txtModifiedBy.Text = dr["ModifiedBy"].ToString();
                txtModifiedOn.Text = dr["ModifiedOn"].ToString();
                pnl_UserLogin.Visible = true;

                string Departments = Convert.ToString(dr["DepartmentId"]);
                string[] arr;

                arr = Departments.Split(Convert.ToChar(","));
                //ckldepartment.ClearSelection();
                //for (int j = 0; j < arr.Length; j++)
                //{
                //    for (int i = 0; i < this.ckldepartment.Items.Count; i++)
                //    {
                //        if (this.ckldepartment.Items[i].Value == arr[j].ToString())
                //        {
                //            this.ckldepartment.Items[i].Selected = true;
                //        }
                //    }
                //}

                string RecruitingOffices = Convert.ToString(dr["RecruitingOfficeId"]);
                string[] arr1;

                arr1 = RecruitingOffices.Split(Convert.ToChar(","));
                chklstRecruitingOffice.ClearSelection();
                for (int m = 0; m < arr1.Length; m++)
                {
                    for (int n = 0; n < this.chklstRecruitingOffice.Items.Count; n++)
                    {
                        if (this.chklstRecruitingOffice.Items[n].Value == arr1[m].ToString())
                        {
                            this.chklstRecruitingOffice.Items[n].Selected = true;
                        }
                    }
                }
                btn_Save.Visible = true;
                btn_Cancel.Visible = true;
            }
        }

        DataTable dt1 = Common.Execute_Procedures_Select_ByQuery("select EmpCode from DBO.Hr_PersonalDetails where UserId=" + Loginid.ToString());
        if (dt1 != null)
        {
            if (dt1.Rows.Count > 0)
            {
                DataRow dr = dt1.Rows[0];
                txtEmployeeCode.Text = dr[0].ToString();
            }
            else
            {
                txtEmployeeCode.Text = "";
            }
        }
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("AdminDashBoard.aspx");
    }
    [System.Web.Services.WebMethod]
    public static string[] GetCompletionList(string prefixText, int count)
    {
        string[] Users;
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT USERID FROM USERMASTER WHERE USERID LIKE '" + prefixText + "%'");
        
        Users = new string[dt.Rows.Count];
        int i=0;
        for(i=0;i<=dt.Rows.Count -1;i++)
        {
            Users[i]=dt.Rows[i][0].ToString();   
        }
        return Users;
    }
}
