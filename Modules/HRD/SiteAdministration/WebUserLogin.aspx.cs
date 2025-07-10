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

public partial class SiteAdministration_WebUserLogin : System.Web.UI.Page
{
    int id;
    Authority Auth;
    string Mode = "New";
    protected void Page_Load(object sender, EventArgs e)
    {
        txtPassword.Attributes["Value"] = txtPassword.Text;
        txtConfirmPassword.Attributes["Value"] = txtPassword.Text;
        lbl_UserLogin_Message.Text = "";
        ProcessCheckAuthority OBJ = new ProcessCheckAuthority(1, 7);
        OBJ.Invoke();
        Session["Authority"] = OBJ.Authority;
        Auth = OBJ.Authority;
        if (!Page.IsPostBack)
        {
            bindUserLoginGrid();
            dddlOwner.DataSource = Budget.getTable("select Ownerid,Ownername from owner order by ownername");
            dddlOwner.DataTextField = "ownername";
            dddlOwner.DataValueField= "ownerId";
            dddlOwner.DataBind();
            // dddlOwner.Items.Insert(0,new ListItem("ALL", "0"));

            DataTable dt = Budget.getTable("select VesselId,VesselName from Vessel order by vesselname").Tables[0];
            chkLst_VIMS.DataSource = dt;
            chkLst_VIMS.DataTextField = "VesselName";
            chkLst_VIMS.DataValueField = "VesselId";
            chkLst_VIMS.DataBind();

            chkLst_Shipsoft.DataSource = dt;
            chkLst_Shipsoft.DataTextField = "VesselName";
            chkLst_Shipsoft.DataValueField = "VesselId";
            chkLst_Shipsoft.DataBind();

            chkLst_INS.DataSource = dt;
            chkLst_INS.DataTextField = "VesselName";
            chkLst_INS.DataValueField = "VesselId";
            chkLst_INS.DataBind();

            HANDLE_AUTHORITY();
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
        DataSet ds = MTMWeb.getTable("SELECT userid,username,isnull((select ownername from DBO.owner where ownerid=owner),'' ) as Owner FROM MW_USERLOGIN");
        this.GridView_UserLogin.DataSource = ds.Tables[0];
        this.GridView_UserLogin.DataBind();
        HiddenField1.Value = "";
    }
    protected void btn_Add_Click(object sender, EventArgs e)
    {
        HiddenField1.Value = "";
        pnl_UserLogin.Visible = true;
        HiddenUserLogin.Value = "";
        ddlStatus.SelectedIndex = 0;
        txtPassword.Attributes["Value"] = "";
        txtConfirmPassword.Attributes["Value"] = "";
        txtCreatedBy.Text = "";
        txtCreatedOn.Text = "";
        txtModifiedBy.Text = "";
        txtModifiedOn.Text = "";
        txtPassword.Text = "";
        txtUserId.Text = "";
        txtUserId.CssClass = "required_box";
        txtUserId.ReadOnly = false;
        ddlStatus.Enabled = true;
        btn_Save.Visible = true;
        btn_Cancel.Visible = true;
        btn_Add.Visible = false;
        lbl_UserLogin_Message.Visible = false;
        btn_Print.Visible = true;
        chkLst_INS.ClearSelection();
        chkLst_Shipsoft.ClearSelection();
        chkLst_VIMS.ClearSelection();  
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        string LoginId = Session["loginid"].ToString();
        int UserId = 0;
        try
        {
            UserId = int.Parse(HiddenUserLogin.Value);
        }
        catch { }
        System.IO.StreamReader sr = new StreamReader(fileup.PostedFile.InputStream);
        string fileContent = sr.ReadToEnd();
        string v1 = "", v2 = "",v3="";
        foreach (ListItem Li in chkLst_VIMS.Items)
        {
            if (Li.Selected) { v1 = v1 + ((v1 == "") ? "" : ",") + Li.Value; }
        }
        foreach (ListItem Li in chkLst_Shipsoft.Items)
        {
            if (Li.Selected) { v2 = v2 + ((v2 == "") ? "" : ",") + Li.Value; }
        } 
        foreach (ListItem Li in chkLst_INS.Items)
        {
            if (Li.Selected) { v3 = v3 + ((v3 == "") ? "" : ",") + Li.Value; }
        }
        if (MTMWeb.InsertUpdateUser(UserId, txtUserId.Text, int.Parse(dddlOwner.SelectedValue), MTMWeb.Encrypt(txtPassword.Text, "QWERTY12345"), int.Parse(LoginId), ddlStatus.SelectedValue,txt_PoUser.Text,txt_PoPassword.Text, v1, v2,v3, fileContent))
        {
            lbl_UserLogin_Message.Text = "User Created Successfully.";
        }
        else
        {
            lbl_UserLogin_Message.Text = "Unable to create User.";
        }

        bindUserLoginGrid();
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
        btn_Print.Visible = false;
    }
    protected void btn_Print_Click(object sender, EventArgs e)
    {
    }
    protected void Show_Record_UserLogin(int Loginid)
    {
        HiddenUserLogin.Value = Loginid.ToString();
        DataTable dt3 = MTMWeb.getTable("select userid,username,password,owner,Status,pouser,popassword,(select firstname + ' ' + lastname from DBO.userlogin where loginid=mw_userlogin.createdby) as createdby, createdon,(select firstname + ' ' + lastname from DBO.userlogin where loginid=mw_userlogin.modifiedBy) as modifiedBy,modifiedon from mw_userlogin where userid=" + Loginid.ToString()).Tables[0];
       
        foreach (DataRow dr in dt3.Rows)
        {
            ddlStatus.SelectedValue=dr["Status"].ToString();
            txtPassword.Attributes["Value"] = MTMWeb.Decrypt(dr["Password"].ToString(), "QWERTY12345");
            txtConfirmPassword.Attributes["Value"] = txtPassword.Attributes["Value"];
            txt_PoUser.Text = dr["PoUser"].ToString();
            txt_PoPassword.Text = dr["PoPassword"].ToString();
          
            txtUserId.Text = dr["UserName"].ToString();
            txtCreatedBy.Text = dr["CreatedBy"].ToString();
            try
            {
                txtCreatedOn.Text = DateTime.Parse(dr["CreatedOn"].ToString()).ToString("MM/dd/yyyy");
            }
            catch { txtCreatedOn.Text = ""; }
            txtModifiedBy.Text = dr["ModifiedBy"].ToString();
            try{
            txtModifiedOn.Text = DateTime.Parse(dr["ModifiedOn"].ToString()).ToString("MM/dd/yyyy");
            }catch {txtModifiedOn.Text="";}  
        }
        chkLst_VIMS.ClearSelection();
        chkLst_Shipsoft.ClearSelection();
        foreach (ListItem Li in chkLst_VIMS.Items)
        {
            dt3 = MTMWeb.getTable("select * from MW_UserVesselAuthority where userid=" + Loginid.ToString() + " and vesselid=" + Li.Value + " and module='V'").Tables[0];
            if (dt3.Rows.Count > 0) { Li.Selected = true; }
        }
        foreach (ListItem Li in chkLst_Shipsoft.Items)
        {
            dt3 = MTMWeb.getTable("select * from MW_UserVesselAuthority where userid=" + Loginid.ToString() + " and vesselid=" + Li.Value + " and module='S'").Tables[0];
            if (dt3.Rows.Count > 0) { Li.Selected = true; }
        }
        foreach (ListItem Li in chkLst_INS.Items)
        {
            dt3 = MTMWeb.getTable("select * from MW_UserVesselAuthority where userid=" + Loginid.ToString() + " and vesselid=" + Li.Value + " and module='I'").Tables[0];
            if (dt3.Rows.Count > 0) { Li.Selected = true; }
        }
        ddlStatus.Enabled = (Loginid != 1);  
        btn_Print.Visible = true;
    }
    protected void GridView_UserLogin_SelectedIndexChanged(object sender, EventArgs e)
    {
        HiddenField hfdUserLogin;
        hfdUserLogin = (HiddenField)GridView_UserLogin.Rows[GridView_UserLogin.SelectedIndex].FindControl("HiddenLoginId");
        id = Convert.ToInt32(hfdUserLogin.Value.ToString());
        pnl_UserLogin.Visible = true;
        Show_Record_UserLogin(id);
        btn_Add.Visible = ((Mode == "Edit") || (Mode == "New")) ? true : false;
        btn_Save.Visible = false;
        btn_Cancel.Visible = true;
    }
    protected void GridView_UserLogin_Row_Editing(object sender, GridViewEditEventArgs e)
    {
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
    protected void GridView_UserLogin_Row_Deleting(object sender, GridViewDeleteEventArgs e)
    {
        Mode = "Edit";
        HiddenField hfdUserLogin;
        hfdUserLogin = (HiddenField)GridView_UserLogin.Rows[e.RowIndex].FindControl("HiddenLoginId");
        id = Convert.ToInt32(hfdUserLogin.Value.ToString());
        MTMWeb.deleteUser(id);  
        pnl_UserLogin.Visible = false;
        btn_Add.Visible = true ;
        btn_Cancel.Visible= false;
        btn_Save.Visible = false;
        bindUserLoginGrid(); 
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
