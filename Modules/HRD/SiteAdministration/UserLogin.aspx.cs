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
using ShipSoft.CrewManager.BusinessObjects;
using ShipSoft.CrewManager.BusinessLogicLayer;
using ShipSoft.CrewManager.Operational;
using System.IO;

public partial class SiteAdministration_UserLogin : System.Web.UI.Page
{
    int id;
    Authority Auth;
    string Mode = "New";
    protected void Page_Load(object sender, EventArgs e)
    {
       
        ProcessCheckAuthority OBJ = new ProcessCheckAuthority(1, 7);
        OBJ.Invoke();
        Session["Authority"] = OBJ.Authority;
        Auth = OBJ.Authority;
        if (!Page.IsPostBack)
        {
            bindUserLoginGrid();
            bindRoleNameDDL();
            bindDepartmentDDL();
            bindStatusDDL();
            bindRecruitingOfficeList();
            HANDLE_AUTHORITY();
            DataTable dsadmin=UserLogin.selectadminrole(Convert.ToInt32(Session["loginid"].ToString()));
            ViewState.Add("AdminRole", dsadmin.Rows[0][0].ToString());
            DataTable ds12 = UserLogin.selectsuperuser(Convert.ToInt32(Session["loginid"].ToString()));
            ViewState.Add("SuperUser", ds12.Rows[0][0].ToString());
        }
    }
    private void HANDLE_AUTHORITY()
    {

        if (Mode == "New")
        {
            btn_Add.Visible = ((Auth.isEdit || Auth.isAdd) && (Mode == "New" || Mode == "Edit"));
            btn_Save.Visible = false;
            btn_Cancel.Visible = false;
            pnl_UserLogin.Visible = false;
        }
        else if (Mode == "Edit")
        {
            btn_Add.Visible = ((Auth.isEdit || Auth.isAdd) && (Mode == "New" || Mode == "Edit"));
            btn_Save.Visible = false;
            btn_Cancel.Visible = false;
            pnl_UserLogin.Visible = false;
        }
        else // Mode=View
        {
            btn_Add.Visible = false;
            btn_Save.Visible = false;
            btn_Cancel.Visible = false;
            pnl_UserLogin.Visible = false;
        }
    }
    public void bindUserLoginGrid()
    {
        DataTable dt1 = UserLogin.selectDataUserLoginDetails();
        this.GridView_UserLogin.DataSource = dt1;
        this.GridView_UserLogin.DataBind();
        HiddenField1.Value = "";
    }
    public void bindRoleNameDDL()
    {
        DataTable dt4 = UserLogin.selectDataRoleNameDetails();
        this.ddlRoleName.DataValueField = "RoleId";
        this.ddlRoleName.DataTextField = "RoleName";
        this.ddlRoleName.DataSource = dt4;
        this.ddlRoleName.DataBind();
    }
    public void bindDepartmentDDL()
    {
        DataTable dt41 = UserLogin.selectDataDepartmentNameDetails();
        this.ckldepartment.DataValueField = "DepartmentId";
        this.ckldepartment.DataTextField = "DepartmentName";
        this.ckldepartment.DataSource = dt41;
        this.ckldepartment.DataBind();
    }
    public void bindRecruitingOfficeList()
    {
        DataTable dt67 = PlanTraining.selectDataTrainingZoneName();
        this.chklstRecruitingOffice.DataValueField = "RecruitingOfficeId";
        this.chklstRecruitingOffice.DataTextField = "RecruitingOfficeName";
        this.chklstRecruitingOffice.DataSource = dt67;
        this.chklstRecruitingOffice.DataBind();
        this.chklstRecruitingOffice.Items.RemoveAt(0);
    }
    public void bindStatusDDL()
    {
        DataTable dt2 = UserLogin.selectDataStatusDetails();
        this.ddlStatus.DataValueField = "StatusId";
        this.ddlStatus.DataTextField = "StatusName";
        this.ddlStatus.DataSource = dt2;
        this.ddlStatus.DataBind();
    }
    protected void btn_Add_Click(object sender, EventArgs e)
    {
        HiddenField1.Value = "";
        pnl_UserLogin.Visible = true;
        HiddenUserLogin.Value = "";
        ddlRoleName.SelectedIndex = 0;
        ckldepartment.ClearSelection();
        chklstRecruitingOffice.ClearSelection();
        
        ddlStatus.SelectedIndex = 0;
        txtPassword.Attributes["Value"] = "";
        txtConfirmPassword.Attributes["Value"] = "";
        txtCreatedBy.Text = "";
        txtCreatedOn.Text = "";
        txtDOB.Text = "";
        txtEmail.Text = "";
        txtFirstName.Text = "";
        txtLastName.Text = "";
        txtModifiedBy.Text = "";
        txtModifiedOn.Text = "";
        txtPassword.Text = "";
        txtUserId.Text = "";
        txtUserId.CssClass = "required_box";
        ddlRoleName.Enabled = true;
        txtUserId.ReadOnly = false;
        ddlStatus.Enabled = true;
        btn_Save.Visible = true;
        btn_Cancel.Visible = true;
        btn_Add.Visible = false;
        lbl_UserLogin_Message.Visible = false;
        btn_Print.Visible = true;
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        if (ddlRoleName.SelectedItem.Text.ToUpper() == txtUserId.Text.ToUpper())
        {
            Label1.Visible = true;
        }
        else
        {
            Label1.Visible = false;
            char SystemUser;
            int loginId = -1;
            int createdby = 0, modifiedby = 0;
            string struserid = txtUserId.Text;
            string passwd = txtPassword.Text;
            string firstname = txtFirstName.Text;
            string lastname = txtLastName.Text;
            string dob = txtDOB.Text;
            string email = txtEmail.Text;
            int roleid = Convert.ToInt32(ddlRoleName.SelectedValue);
            
            char statusid = Convert.ToChar(ddlStatus.SelectedValue);
            if (HiddenField1.Value == "")
            {
                SystemUser = 'N';
            }
            else
            {
                SystemUser = Convert.ToChar(HiddenField1.Value);
            }


            string Department = "";
            for (int i = 0; i < this.ckldepartment.Items.Count; i++)
            {
                if (this.ckldepartment.Items[i].Selected)
                {
                   
                    if (Department == "")
                    {
                        Department = ckldepartment.Items[i].Value.ToString();
                    }
                    else
                    {
                        Department = Department + "," + ckldepartment.Items[i].Value.ToString();
                    }
                }
            }

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


            if (HiddenUserLogin.Value.Trim() == "")
            {
                createdby = 1;
            }
            else
            {
                loginId = Convert.ToInt32(HiddenUserLogin.Value);
                modifiedby = 53;
            }
            {
                btn_Add.Visible = true;
            }

            UserLogin.insertUpdateUserLoginDetails("InsertUpdateUserLoginDetails",
                                              loginId,
                                              struserid,
                                              roleid,
                                              passwd,
                                              firstname,
                                              lastname,
                                              dob,
                                              email,
                                              Department,
                                              RecruitingOfc,
                                              createdby,
                                              modifiedby,
                                              statusid,
                                              SystemUser);
            bindUserLoginGrid();
            btn_Add.Visible = false;
            btn_Add_Click(sender, e);
            btn_Cancel_Click(sender, e);
            lbl_UserLogin_Message.Visible = true;
            
            lbl_UserLogin_Message.Text = "Record Successfully Saved.";
            
        }
    }
    protected void btn_Cancel_Click(object sender, EventArgs e)
    {
        if (Mode == "New")
        {
            btn_Add.Visible = (Auth.isAdd || Auth.isEdit);
        }
        else if (Mode == "Edit")
        {
            btn_Add.Visible = (Auth.isAdd || Auth.isEdit);
        }
        pnl_UserLogin.Visible = false;
        btn_Save.Visible = false;
        btn_Cancel.Visible = false;
        btn_Add.Visible = ((Mode == "Edit") || (Mode == "New")) ? true : false;
        GridView_UserLogin.SelectedIndex = -1;
        lbl_UserLogin_Message.Visible = false;
        btn_Print.Visible = false;
    }
    protected void btn_Print_Click(object sender, EventArgs e)
    {
        lbl_UserLogin_Message.Visible = false;
    }
    protected void GridView_UserLogin_DataBound(object sender, EventArgs e)
    {
        try
        {
          
            // Can Add
            if (Auth.isAdd)
            {
            }

            // Can Modify

            this.GridView_UserLogin.Columns[1].Visible = Auth.isEdit;

            this.GridView_UserLogin.Columns[2].Visible = Auth.isDelete;

            // Can Print
            if (Auth.isPrint)
            {
            }

        }
        catch
        {
           
        }
    }
    protected void Show_Record_UserLogin(int Loginid)
    {
        lbl_UserLogin_Message.Visible = false;
        HiddenUserLogin.Value = Loginid.ToString();
        DataTable dt3 = UserLogin.selectDataUserLoginDetailsByUserLoginId(Loginid);
       
        foreach (DataRow dr in dt3.Rows)
        {

            HiddenField2.Value = ddlRoleName.SelectedValue = dr["RoleId"].ToString();
            ddlStatus.SelectedValue=dr["StatusId"].ToString();
            txtPassword.Attributes["Value"] = dr["Password"].ToString();
            txtConfirmPassword.Attributes["Value"] = dr["Password"].ToString();
            txtDOB.Text=dr["DateOfBirth"].ToString();
            txtEmail.Text=dr["Email"].ToString();
          
            txtFirstName.Text=dr["FirstName"].ToString();
            txtLastName.Text=dr["LastName"].ToString();
            txtUserId.Text = dr["UserId"].ToString();
            HiddenField1.Value = dr["SuperUser"].ToString();
            txtCreatedBy.Text = dr["CreatedBy"].ToString();
            txtCreatedOn.Text = dr["CreatedOn"].ToString();
            txtModifiedBy.Text = dr["ModifiedBy"].ToString();
            txtModifiedOn.Text = dr["ModifiedOn"].ToString();

            string Departments = Convert.ToString(dr["DepartmentId"]);
            string[] arr;

            arr = Departments.Split(Convert.ToChar(","));
            ckldepartment.ClearSelection();
            for (int j = 0; j < arr.Length; j++)
            {
                for (int i = 0; i < this.ckldepartment.Items.Count; i++)
                {
                    if (this.ckldepartment.Items[i].Value == arr[j].ToString())
                    {
                        this.ckldepartment.Items[i].Selected = true;
                    }
                }
            }

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
        }
       
        if (ViewState["AdminRole"].ToString() == "Y")//For AdminRole
        {
            //For System user other than self
            if (Convert.ToInt32(HiddenField2.Value) == 1 && Session["loginid"].ToString() != HiddenUserLogin.Value)
            {
                txtUserId.CssClass = "readonly_box";
                ddlRoleName.Enabled = false;
                txtUserId.ReadOnly = true;
                
                ddlStatus.Enabled = false;
               
            }
            else if (Session["loginid"].ToString() == HiddenUserLogin.Value)//For Admin i.e. Super User
            {
                txtUserId.CssClass = "readonly_box";
                ddlRoleName.Enabled = false;
                txtUserId.ReadOnly = true;
                ddlStatus.Enabled = false;
                this.txtEmail.Enabled = true;
               
            }
            else if (Convert.ToInt32(HiddenField2.Value) >= 2)
            {
                txtUserId.CssClass = "readonly_box";
               
                txtUserId.ReadOnly = true;
                ddlStatus.Enabled = true;
                
            }
            
        }
       
        if (ViewState["SuperUser"].ToString() == "Y")
        {
            if (HiddenField1.Value == "Y" && Session["loginid"].ToString() == HiddenUserLogin.Value)
            {
                ddlRoleName.Enabled = false;
                ddlStatus.Enabled = false;
            }
            else
            {
                ddlRoleName.Enabled = true;
                ddlStatus.Enabled = true;
            }
        }
        else
        {
            if (HiddenField1.Value == "N" && Session["loginid"].ToString() != HiddenUserLogin.Value)
            {
                ddlRoleName.Enabled = true;
                ddlStatus.Enabled = true;
                if (HiddenField1.Value == "Y")
                {
                    btn_Save.Enabled = false;
                }
                else
                {
                    btn_Save.Enabled = true;
                }
            }
            else
            {
                if (HiddenField1.Value == "Y")
                {
                    btn_Save.Enabled = false;
                }
                else
                {
                    btn_Save.Enabled = true;
                }
                ddlRoleName.Enabled = false;
                ddlStatus.Enabled = false;
            }            
        }
        btn_Print.Visible = true;
        lbl_UserLogin_Message.Visible = false;
    }
    // VIEW THE RECORD
    protected void GridView_UserLogin_SelectedIndexChanged(object sender, EventArgs e)
    {
        lbl_UserLogin_Message.Visible = false;
        HiddenField hfdUserLogin;
        hfdUserLogin = (HiddenField)GridView_UserLogin.Rows[GridView_UserLogin.SelectedIndex].FindControl("HiddenLoginId");
        id = Convert.ToInt32(hfdUserLogin.Value.ToString());
        pnl_UserLogin.Visible = true;
        Show_Record_UserLogin(id);
        btn_Add.Visible = ((Mode == "Edit") || (Mode == "New")) ? true : false;
        btn_Save.Visible = false;
        btn_Cancel.Visible = true;
    }
    //EDIT THE RECORD
    protected void GridView_UserLogin_Row_Editing(object sender, GridViewEditEventArgs e)
    {
        lbl_UserLogin_Message.Visible = false;
        Mode = "Edit";
        HiddenField hfdUserLogin;
        hfdUserLogin = (HiddenField)GridView_UserLogin.Rows[e.NewEditIndex].FindControl("HiddenLoginId");
        id = Convert.ToInt32(hfdUserLogin.Value.ToString());
        Show_Record_UserLogin(id);
        GridView_UserLogin.SelectedIndex = e.NewEditIndex;
        pnl_UserLogin.Visible = true;
        btn_Add.Visible = ((Mode == "Edit") || (Mode == "New")) ? true : false;
        btn_Save.Visible = true;
        btn_Cancel.Visible = true;
    }
   
    protected void GridView_UserLogin_PreRender(object sender, EventArgs e)
    {
        if (GridView_UserLogin.Rows.Count <= 0) { lbl_GridView_UserLogin.Text = "No Records Found..!"; }
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("AdminDashBoard.aspx");
    }
}
