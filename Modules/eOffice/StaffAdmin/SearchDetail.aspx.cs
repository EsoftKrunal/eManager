using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
  
public partial class SearchDetail : System.Web.UI.Page
{
    public AuthenticationManager auth; 
    //-----------------------------
    public string SessionDeleimeter = ConfigurationManager.AppSettings["SessionDelemeter"];
    //-----------------------------
    # region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        //***********Code to check page acessing Permission
            int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 244);
        if (chpageauth <= 0)
        {
            Response.Redirect("../AuthorityError.aspx");
        }
        //*******************
        auth = new AuthenticationManager(244,Common.CastAsInt32(Session["loginid"]), ObjectType.Page);
        lblmessage.Text = "";
        if (!IsPostBack)
        {
            ControlLoader.LoadControl(ddlgender, DataName.Gender, "Select", "");
            ControlLoader.LoadControl(ddlNationality, DataName.country, "Select", "0");
            ControlLoader.LoadControl(ddloffice, DataName.Office, "Select", "0");
            ControlLoader.LoadControl(ddlemployeestatus, DataName.EmployeeStatus, "Select", "");
            ControlLoader.LoadControl(ddlnewoffice, DataName.Office, "Select", "0");
            ddloffice_SelectedIndexChanged(sender, e); 
            ControlLoader.LoadControl(ddlnewstatus, DataName.EmployeeStatus, "Select", "0");
            ddlnewstatus.Items.Remove(ddlnewstatus.Items.FindByValue("A"));
            ddlnewstatus.Items.Remove(ddlnewstatus.Items.FindByValue("I"));
            LoadSession();

            if (Session["loginid"].ToString() != "1")
            {
                DisableOffice();
            }
            BindGrid();            
        }
    }
    protected void btn_Search_Click(object sender, EventArgs e)
    {
        WriteSession();
        BindGrid();
    }
    protected void btn_Clear_Click(object sender, EventArgs e)
    {
        ClearSession();
        ClearControls();
    }
    protected void btn_add_Click(object sender, EventArgs e)
    {
        dvConfirmCancel.Visible = true;
    }
    protected void btn_Print_Click(object sender, EventArgs e)
    {
        string sql = "EXEC DBO.HR_SearchEmployee '" + txtempno.Text.Trim() + "','" + txt_FirstName_Search.Text.Trim() + "','" + ddlgender.SelectedValue + "'," + ddlNationality.SelectedValue + ",'" + txt_djc_from.Text.Trim() + "','" + txt_djc_to.Text.Trim() + "'," + Common.CastAsInt32(txt_Age_From.Text) + "," + Common.CastAsInt32(txt_Age_To.Text) + ",'" + txt_PassportNo.Text.Trim() + "','" + txt_NricFin.Text.Trim() + "'," + ddloffice.SelectedValue + ",'" + ddlemployeestatus.SelectedValue + "'," + ddldepartment.SelectedValue;
        Session.Add("EmplyeeReport", sql);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "fasd", "window.open('EmployeeReport.aspx');", true);
    }
    protected void btn_Print_Experience_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "fasd", "window.open('EmployeeExperienceReport.aspx');", true);
    }
    
    protected void btnAddNewSubmit_Click(object sender, EventArgs e)
    {
        Session["EmpMode"] = "Add";
        Session["EmpId"] = "0";
        Session["NewEmpOfficeId"] = ddlnewoffice.SelectedValue;

        if (ddlnewstatus.SelectedIndex > 0)
        {
            Session["NewStatus"] = ddlnewstatus.SelectedItem.Text;
        }
        else
        {
            Session["NewStatus"] = "";
        }
        Response.Redirect("HR_PersonalDetail.aspx");
    }
    protected void btnAddNewCancel_Click(object sender, EventArgs e)
    {
        dvConfirmCancel.Visible = false;  
    }
    protected void btnView_Click(object sender, ImageClickEventArgs e)
    {
        SelectedId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        Session["EmpMode"] = "View";
        Session["EmpId"] = SelectedId;
        Response.Redirect("HR_PersonalDetail.aspx");
    }
    protected void btnedit_Click(object sender, ImageClickEventArgs e)
    {
        SelectedId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        Session["EmpMode"] = "Edit";
        Session["EmpId"] = SelectedId;
        Response.Redirect("HR_PersonalDetail.aspx");
    }
    protected void lnkSort_Click(object sender, EventArgs e)
    {
        string LastKey=""+ViewState["SortKey"];
        string NewKey = ((LinkButton)(sender)).CommandArgument;
        string LastMode = "" + ViewState["SortMode"];
        string NewMode="Asc";  
        if (LastKey == NewKey)
        {
            NewMode = (LastMode == "Asc") ? "Desc" : "Asc";  
        }

        ViewState["SortKey"] = NewKey;
        ViewState["SortMode"] = NewMode; 
        BindGrid(); 
    }
    protected void ddloffice_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddloffice.SelectedIndex == 0)
        {
            ddldepartment.Items.Clear();
            ddldepartment.Items.Add(new ListItem("<Select>", "0"));   
        }
        else
        {
            ControlLoader.LoadControl(ddldepartment, DataName.HR_Department, "Select", "0"," officeid=" + ddloffice.SelectedValue);
        }
    }
    #endregion
    //-----------------------------

    #region User Defined Functions
    protected void BindGrid()
    {
        Common.Set_Procedures("HR_SearchEmployee");
        Common.Set_ParameterLength(13);
        Common.Set_Parameters(new MyParameter("@EmpNo", txtempno.Text.Trim()),
        new MyParameter("@FirstName", txt_FirstName_Search.Text.Trim()),
        //new MyParameter("@FamilyName", txt_LastName_Search.Text.Trim()),
        new MyParameter("@Gender", ddlgender.SelectedValue),
        new MyParameter("@Nationality", ddlNationality.SelectedValue),
        new MyParameter("@JoiningDtFrom", txt_djc_from.Text.Trim()),
        new MyParameter("@JoiningDtTo", txt_djc_to.Text.Trim()),
        new MyParameter("@AgeFrom", txt_Age_From.Text),
        new MyParameter("@AgeTo", txt_Age_To.Text),
        new MyParameter("@Passport", txt_PassportNo.Text.Trim()),
        new MyParameter("@NRIC_FIN", txt_NricFin.Text.Trim() ),
        new MyParameter("@Office", ddloffice.SelectedValue),
        new MyParameter("@EmpStatus", ddlemployeestatus.SelectedValue),
        new MyParameter("@Department", ddldepartment.SelectedValue));

        string SortKey=""+ViewState["SortKey"];
        string SortMode=""+ViewState["SortMode"];
        SortKey = (SortKey.Trim() == "") ? "EMPName" : SortKey;
        SortMode = (SortMode.Trim() == "") ? "Asc" : SortMode;  

        DataTable dt=Common.Execute_Procedures_Select_CMS().Tables[0];
        DataView dv=dt.DefaultView;
        dv.Sort= SortKey + " " + SortMode;
        rptData.DataSource = dv.ToTable();
        rptData.DataBind();
        EmpCount.Text = rptData.Items.Count.ToString();  
    }
    protected void ClearControls()
    {
        txtempno.Text = "";
        txt_FirstName_Search.Text = "";
        //txt_LastName_Search.Text = "";
        ddlgender.SelectedValue = "";
        ddlNationality.SelectedIndex  = 0;
        txt_djc_from.Text = "";
        txt_djc_to.Text = "";
        txt_Age_From.Text = "";
        txt_Age_To.Text = "";
        txt_PassportNo.Text = "";
        ddloffice.SelectedIndex = 0;
        ddlemployeestatus.SelectedIndex = 0;

        if (Session["loginid"].ToString() != "1")
        {
            DisableOffice();
        }
    }
    protected void LoadSession()
    {
        {
            string[] Delemeters = { SessionDeleimeter };
            string values = "" + Session["EMEM_SEARCH"];
            string[] ValueList = values.Split(Delemeters, StringSplitOptions.None);
            try
            {
                txtempno.Text = ValueList[0];
                txt_FirstName_Search.Text = ValueList[1];
                //txt_LastName_Search.Text = ValueList[2];
                ddlgender.SelectedValue  = ValueList[2];
                ddlNationality.SelectedValue = ValueList[3];
                txt_djc_from.Text  = ValueList[4];
                txt_djc_to.Text = ValueList[5];
                txt_Age_From.Text = ValueList[6];
                txt_Age_To.Text = ValueList[7];
                txt_PassportNo.Text = ValueList[8];
                ddloffice.SelectedValue = ValueList[9];
                ddlemployeestatus.SelectedValue = ValueList[10];
                ddloffice_SelectedIndexChanged(new object(), new EventArgs()); 
            }
            catch { }
        }
    }
    protected void WriteSession()
    {
        string values = txtempno.Text + SessionDeleimeter +
                        txt_FirstName_Search.Text + SessionDeleimeter +
                        //txt_LastName_Search.Text + SessionDeleimeter +
                        ddlgender.SelectedValue + SessionDeleimeter +
                        ddlNationality.SelectedValue + SessionDeleimeter +
                        txt_djc_from.Text + SessionDeleimeter +
                        txt_djc_to.Text + SessionDeleimeter +
                        txt_Age_From.Text + SessionDeleimeter +
                        txt_Age_To.Text + SessionDeleimeter +
                        txt_PassportNo.Text + SessionDeleimeter +
                        ddloffice.SelectedValue + SessionDeleimeter +
                        ddlemployeestatus.SelectedValue;
        Session["EMEM_SEARCH"] = values;
    }
    protected void ClearSession()
    {
        Session["EMEM_SEARCH"] = null;
    }
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

    protected void DisableOffice()
    {
        string strSQL = "select Office from Hr_PersonalDetails where EmpId=" + Session["ProfileId"].ToString();
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(strSQL);

        if(dt.Rows.Count > 0)
        {
            ddloffice.SelectedValue = dt.Rows[0]["Office"].ToString();
            if(dt.Rows[0]["Office"].ToString() != "3")
            {
                ddloffice.Enabled = false;
            }
        }
    }

    #endregion
    //-----------------------------
}
