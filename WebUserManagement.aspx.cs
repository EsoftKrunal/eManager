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

public partial class WebUserManagement : System.Web.UI.Page
{
    int UserId=0;
    int MaxHeight = 440, Minheight = 120;
    public void BindGrid()
    {
        rpt_Data.DataSource = ECommon.Execute_Procedures_Select_ByQuery_MTM("SELECT userid,username,isnull((select ownername from DBO.owner where ownerid=owner),'' ) as Owner FROM MW_USERLOGIN");
        rpt_Data.DataBind();
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
            BindGrid();
            dddlOwner.DataSource = ECommon.Execute_Procedures_Select_ByQuery_CMS("select Ownerid,Ownername from owner order by ownername");
            dddlOwner.DataTextField = "ownername";
            dddlOwner.DataValueField = "ownerId";
            dddlOwner.DataBind();
            
            DataTable dt = ECommon.Execute_Procedures_Select_ByQuery_CMS("select VesselId,VesselName from Vessel order by vesselname");
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

            DataTable dt1 = ECommon.Execute_Procedures_Select_ByQuery_MTM("select ModuleId,ModuleName from MW_ModuleMaster MM");
            chklstModules.DataSource = dt1;
            chklstModules.DataTextField = "ModuleName";
            chklstModules.DataValueField = "ModuleId";
            chklstModules.DataBind();

            btn_Save.Visible = false;
            btn_Cancel.Visible = false;
            pnlList.Height = Unit.Pixel(MaxHeight);    
        }
        txtPassword.Attributes.Add("value", txtPassword.Text);
        txtConfirmPassword.Attributes.Add("value", txtConfirmPassword.Text);
    }
    protected void EditClick(object sender, ImageClickEventArgs e)
    {
        int Recid=int.Parse(((ImageButton)sender).CommandArgument);
        Show_Record(Recid);
        ViewState["SelId"] = Recid.ToString();
        BindGrid(); 
        pnl_UserLogin.Visible = true;
    }
    protected void DeleteClick(object sender, ImageClickEventArgs e)
    {
        int Recid = int.Parse(((ImageButton)sender).CommandArgument);
        EMTMWeb.deleteUser(Recid);
        pnl_UserLogin.Visible = false;
        btn_Add.Visible = true;
        btn_Cancel.Visible = false;
        btn_Save.Visible = false;
        BindGrid();
    }
    protected void btn_Add_Click(object sender, EventArgs e)
    {
        HiddenUserLogin.Value = "0";
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
        txtEmails.Text = "";
        txtUserId.ReadOnly = false;
        ddlStatus.Enabled = true;
        chkLst_INS.ClearSelection();
        chkLst_Shipsoft.ClearSelection();
        chkLst_VIMS.ClearSelection();  

        pnl_UserLogin.Visible = true;
        btn_Save.Visible = true;
        btn_Cancel.Visible = true;
        ViewState["SelId"] = 0;
        pnlList.Height = Unit.Pixel(Minheight); 
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        if (! Validator.ValidateUserName(txtUserId.Text))
        {
            Msgbox.ShowMessage("Please enter a valid username (4-15) Chars.",true);
            txtUserId.Focus();  
            return; 
        }
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
       
        DataTable dt = ECommon.Execute_Procedures_Select_ByQuery_MTM("SELECT * FROM mw_userlogin WHERE LTRIM(RTRIM(username))='" + txtUserId.Text.Trim() + "' And userid<>" + HiddenUserLogin.Value); 
        if (dt!=null)
        {
            if (dt.Rows.Count > 0)
            {
                Msgbox.ShowMessage("Entered Username alredy exist in the database.", true);
                txtUserId.Focus();
                return;
            }
        }


        string LoginId = this.UserId.ToString();
        int UserId = 0;
        try
        {
            UserId = int.Parse(HiddenUserLogin.Value);
        }
        catch { }
        System.IO.StreamReader sr = new StreamReader(fileup.PostedFile.InputStream);
        string fileContent = sr.ReadToEnd();
        string v1 = "", v2 = "", v3 = "", v4="";
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
        foreach (ListItem Li in chklstModules.Items)
        {
            if (Li.Selected) { v4 = v4 + ((v4 == "") ? "" : ",") + Li.Value; }
        }
        if (EMTMWeb.InsertUpdateUser(UserId, txtUserId.Text, int.Parse(dddlOwner.SelectedValue), EMTMWeb.Encrypt(txtPassword.Text, "QWERTY12345"), int.Parse(LoginId), ddlStatus.SelectedValue, txt_PoUser.Text, txt_PoPassword.Text, v1, v2, v3,v4, fileContent,txtEmails.Text.Trim()))
        {
            BindGrid(); 
            Msgbox.ShowMessage("User Created Successfully.",false);
        }
        else
        {
            Msgbox.ShowMessage("Unable to create User.",true);
        }
    }
    protected void btn_Cancel_Click(object sender, EventArgs e)
    {
        pnl_UserLogin.Visible = false;
        btn_Save.Visible = false ;
        btn_Cancel.Visible = false ;
        pnlList.Height = Unit.Pixel(MaxHeight); 
    }
    protected void Show_Record(int Loginid)
    {
        HiddenUserLogin.Value = Loginid.ToString();
        DataTable dt3 = ECommon.Execute_Procedures_Select_ByQuery_MTM("select userid,username,password,owner,Status,pouser,popassword,(select firstname + ' ' + lastname from DBO.userlogin where loginid=mw_userlogin.createdby) as createdby, createdon,(select firstname + ' ' + lastname from DBO.userlogin where loginid=mw_userlogin.modifiedBy) as modifiedBy,modifiedon,EmailAddress from mw_userlogin where userid=" + Loginid.ToString());

        foreach (DataRow dr in dt3.Rows)
        {
            dddlOwner.SelectedValue = dr["owner"].ToString();
            ddlStatus.SelectedValue = dr["Status"].ToString();
            txtPassword.Attributes["Value"] = EMTMWeb.Decrypt(dr["Password"].ToString(), "QWERTY12345");
            txtConfirmPassword.Attributes["Value"] = txtPassword.Attributes["Value"];
            txt_PoUser.Text = dr["PoUser"].ToString();
            txt_PoPassword.Text = dr["PoPassword"].ToString();
            txtEmails.Text = dr["EmailAddress"].ToString();
            txtUserId.Text = dr["UserName"].ToString();
            txtCreatedBy.Text = dr["CreatedBy"].ToString();
            try
            {
                txtCreatedOn.Text = DateTime.Parse(dr["CreatedOn"].ToString()).ToString("MM/dd/yyyy");
            }
            catch { txtCreatedOn.Text = ""; }
            txtModifiedBy.Text = dr["ModifiedBy"].ToString();
            try
            {
                txtModifiedOn.Text = DateTime.Parse(dr["ModifiedOn"].ToString()).ToString("MM/dd/yyyy");
            }
            catch { txtModifiedOn.Text = ""; }
        }
        chkLst_VIMS.ClearSelection();
        chkLst_Shipsoft.ClearSelection();
        chkLst_INS.ClearSelection();
        foreach (ListItem Li in chkLst_VIMS.Items)
        {
            dt3 = ECommon.Execute_Procedures_Select_ByQuery_MTM("select * from MW_UserVesselAuthority where userid=" + Loginid.ToString() + " and vesselid=" + Li.Value + " and module='V'");
            if (dt3.Rows.Count > 0) { Li.Selected = true; }
        }
        foreach (ListItem Li in chkLst_Shipsoft.Items)
        {
            dt3 = ECommon.Execute_Procedures_Select_ByQuery_MTM("select * from MW_UserVesselAuthority where userid=" + Loginid.ToString() + " and vesselid=" + Li.Value + " and module='S'");
            if (dt3.Rows.Count > 0) { Li.Selected = true; }
        }
        foreach (ListItem Li in chkLst_INS.Items)
        {
            dt3 = ECommon.Execute_Procedures_Select_ByQuery_MTM("select * from MW_UserVesselAuthority where userid=" + Loginid.ToString() + " and vesselid=" + Li.Value + " and module='I'");
            if (dt3.Rows.Count > 0) { Li.Selected = true; }
        }
        foreach (ListItem Li in chklstModules.Items)
        {
            dt3 = ECommon.Execute_Procedures_Select_ByQuery_MTM("select * from MW_OWNERMODULERIGHTS where userid=" + Loginid.ToString() + " and MODULEID=" + Li.Value + "");
            if (dt3.Rows.Count > 0) { Li.Selected = true; }
        }

        ddlStatus.Enabled = (Loginid != 1);
        btn_Save.Visible = true;
        btn_Cancel.Visible = true;

    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("AdminDashBoard.aspx");
    }
}
