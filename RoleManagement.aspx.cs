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
using System.Text; 
using System.IO;

public partial class RoleManagement : System.Web.UI.Page
{
    int UserId;
    int MaxHeight = 310, Minheight = 310;
    public void BindGrid()
    {
        rpt_Data.DataSource = Common.Execute_Procedures_Select_ByQuery("select RoleId,RoleName From RoleMaster Order By RoleName");
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
            LoadApplications();
            LoadModules( int.Parse(ddlApplication.SelectedValue)); 
            BindGrid();
            btn_Save.Visible = false;
            btn_Cancel.Visible = false;
            pnlList.Height = Unit.Pixel(MaxHeight);    
            btn_Add_Click(sender,e);
        }
    }
    public void LoadApplications()
    {
        ddlApplication.DataSource = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM APPLICATIONMASTER");
        ddlApplication.DataTextField = "ApplicationName";
        ddlApplication.DataValueField= "ApplicationId";
        ddlApplication.DataBind();  
    }
    public void LoadModules(int ApplicationId)
    {
        ddlModule.DataSource = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM MODULEMASTER WHERE APPLICATIONID=" + ApplicationId.ToString());
        ddlModule.DataTextField = "ModuleName";
        ddlModule.DataValueField = "ModuleId";
        ddlModule.DataBind();
    }
    public void LoadPages(int ModuleId)
    {
        rpt_Data1.DataSource = Common.Execute_Procedures_Select_ByQuery("select pm.PageId,pagename,isnull(isview,0) as IsView,isnull(isAdd,0) as IsAdd,isnull(isUpdate,0) as IsUpdate,isnull(isdelete,0) as IsDelete,isnull(isprint,0) as IsPrint,isnull(isverify,0) as IsVerify,isnull(isverify2,0) as IsVerify2 from pagemaster pm left join rolepagerelation rpr on pm.pageid=rpr.pageid And rpr.RoleId=" + HiddenRoleName.Value + " Where pm.moduleid=" + ModuleId.ToString());
        rpt_Data1.DataBind();

        rprUsers.DataSource = Common.Execute_Procedures_Select_ByQuery("select loginid,userid,Permission=convert(bit,case when roleid=" + HiddenRoleName.Value + " then 1 else 0 end) from usermaster Where StatusId='A' order by Userid");
        rprUsers.DataBind(); 
    }
    protected void EditClick(object sender, ImageClickEventArgs e)
    {
        int Recid=int.Parse(((ImageButton)sender).CommandArgument);
        Show_Record(Recid);
        ViewState["SelId"] = Recid.ToString();
        BindGrid(); 
        pnl_UserLogin.Visible = true;
    }
    protected void btn_Add_Click(object sender, EventArgs e)
    {
        HiddenRoleName.Value = "0";
        txtRoleName.Text = "";
        pnl_UserLogin.Visible = true;
        btn_Save.Visible = true;
        btn_Cancel.Visible = true;
        ViewState["SelId"] = 0;
        pnlList.Height = Unit.Pixel(Minheight); 
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        if (!Validator.ValidateName(txtRoleName.Text))
        {
            Msgbox.ShowMessage("Please enter a valid Role Name (4-15) Chars.",true);
            txtRoleName.Focus();  
            return; 
        }
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM ROLEMASTER WHERE LTRIM(RTRIM(ROLENAME))='" + txtRoleName.Text.Trim() + "' And ROLEID<>" + HiddenRoleName.Value);
        if (dt != null)
        {
            if (dt.Rows.Count > 0)
            {
                Msgbox.ShowMessage("Entered Rolename alredy exist in the database.", true);
                txtRoleName.Focus();
                return;
            }
        }
        try
        {
            
            if (HiddenRoleName.Value.Trim() == "0")
            {
                Common.Execute_Procedures_Select_ByQuery("Insert Into RoleMaster(RoleName) values('" + txtRoleName.Text + "')");
                DataTable dt1=Common.Execute_Procedures_Select_ByQuery("SELECT MAX(ROLEID) FROM RoleMaster");
                HiddenRoleName.Value=dt1.Rows[0][0].ToString();
                for (int i = 0; i <= rpt_Data1.Items.Count - 1; i++)
                {
                    HiddenField hfd = ((HiddenField)rpt_Data1.Items[i].FindControl("pageid"));
                    CheckBox ch = ((CheckBox)rpt_Data1.Items[i].FindControl("chkView"));
                    CheckBox ch1 = ((CheckBox)rpt_Data1.Items[i].FindControl("chkAdd"));
                    CheckBox ch2 = ((CheckBox)rpt_Data1.Items[i].FindControl("chkUpdate"));
                    CheckBox ch3 = ((CheckBox)rpt_Data1.Items[i].FindControl("chkDelete"));
                    CheckBox ch4 = ((CheckBox)rpt_Data1.Items[i].FindControl("chkPrint"));
                    CheckBox ch5 = ((CheckBox)rpt_Data1.Items[i].FindControl("chkVerify"));
                    CheckBox ch6 = ((CheckBox)rpt_Data1.Items[i].FindControl("chkVerify2"));
                    Common.Execute_Procedures_Select_ByQuery("INSERT INTO rolepagerelation(pageid,roleid,isview,isadd,isupdate,isdelete,isprint,isverify,isverify2,createdby,createdon) values(" + hfd.Value + "," + HiddenRoleName.Value + "," + ((ch.Checked) ? "1" : "0") + "," + ((ch1.Checked) ? "1" : "0") + "," + ((ch2.Checked) ? "1" : "0") + "," + ((ch3.Checked) ? "1" : "0") + "," + ((ch4.Checked) ? "1" : "0") + "," + ((ch5.Checked) ? "1" : "0") + "," + ((ch6.Checked) ? "1" : "0") + "," + UserId.ToString() + ",getdate())");
                }
                for (int i = 0; i <= rprUsers.Items.Count - 1; i++)
                {
                    HiddenField hfd = ((HiddenField)rpt_Data1.Items[i].FindControl("hfdLoginId"));
                    CheckBox ch = ((CheckBox)rpt_Data1.Items[i].FindControl("chkPer"));
                    if (ch.Checked) 
                        Common.Execute_Procedures_Select_ByQuery("Update userMaster set RoleId=" + HiddenRoleName.Value + " Where Loginid=" + hfd.Value);  
                    else
                        Common.Execute_Procedures_Select_ByQuery("Update userMaster set RoleId=0 Where Loginid=" + hfd.Value + " And RoleId=" + HiddenRoleName.Value);  
                }
                Msgbox.ShowMessage("Role added successfully.", false);
            }
            else
            {
                Common.Execute_Procedures_Select_ByQuery("Update RoleMaster set RoleName='" + txtRoleName.Text + "' Where RoleId=" + HiddenRoleName.Value);
                Common.Execute_Procedures_Select_ByQuery("Delete from rolepagerelation where RoleId=" + HiddenRoleName.Value + " And PageId in(Select pageid from pagemaster pm where pm.moduleid=" + ddlModule.SelectedValue + ")");
                for (int i = 0; i <= rpt_Data1.Items.Count - 1; i++)
                {
                    HiddenField hfd = ((HiddenField)rpt_Data1.Items[i].FindControl("pageid"));
                    CheckBox ch= ((CheckBox)rpt_Data1.Items[i].FindControl("chkView"));
                    CheckBox ch1 = ((CheckBox)rpt_Data1.Items[i].FindControl("chkAdd"));
                    CheckBox ch2 = ((CheckBox)rpt_Data1.Items[i].FindControl("chkUpdate"));
                    CheckBox ch3 = ((CheckBox)rpt_Data1.Items[i].FindControl("chkDelete"));
                    CheckBox ch4 = ((CheckBox)rpt_Data1.Items[i].FindControl("chkPrint"));
                    CheckBox ch5 = ((CheckBox)rpt_Data1.Items[i].FindControl("chkVerify"));
                    CheckBox ch6 = ((CheckBox)rpt_Data1.Items[i].FindControl("chkVerify2"));

                    Common.Execute_Procedures_Select_ByQuery("INSERT INTO rolepagerelation(pageid,roleid,isview,isadd,isupdate,isdelete,isprint,isverify,isverify2,createdby,createdon) values(" + hfd.Value + "," + HiddenRoleName.Value + "," + ((ch.Checked) ? "1" : "0") + "," + ((ch1.Checked) ? "1" : "0") + "," + ((ch2.Checked) ? "1" : "0") + "," + ((ch3.Checked) ? "1" : "0") + "," + ((ch4.Checked) ? "1" : "0") + "," + ((ch5.Checked) ? "1" : "0") + "," + ((ch6.Checked) ? "1" : "0") + "," + UserId.ToString() + ",getdate())");
                }
                for (int i = 0; i <= rprUsers.Items.Count - 1; i++)
                {
                    HiddenField hfd = ((HiddenField)rprUsers.Items[i].FindControl("hfdLoginId"));
                    CheckBox ch = ((CheckBox)rprUsers.Items[i].FindControl("chkPer"));
                    if (ch.Checked)
                        Common.Execute_Procedures_Select_ByQuery("Update userMaster set RoleId=" + HiddenRoleName.Value + " Where Loginid=" + hfd.Value);
                    else
                        Common.Execute_Procedures_Select_ByQuery("Update userMaster set RoleId=0 Where Loginid=" + hfd.Value + " And RoleId=" + HiddenRoleName.Value);
                }
                Msgbox.ShowMessage("Role updated successfully.", false);
            }
            BindGrid();
            pnlList.Height = Unit.Pixel(MaxHeight); 
        }
        catch
        {
            Msgbox.ShowMessage("Unable to update Role information.", true);
        }
    }
    protected void btn_Cancel_Click(object sender, EventArgs e)
    {
        //pnl_UserLogin.Visible = false;
        btn_Save.Visible = false ;
        btn_Cancel.Visible = false ;
        pnlList.Height = Unit.Pixel(MaxHeight); 
    }
    protected void Show_Record(int Roleid)
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("select Roleid,RoleName from RoleMaster where Roleid=" + Roleid.ToString());
        if (dt != null)
        {
            if (dt.Rows.Count >0)
            {
                pnlList.Height =Unit.Pixel(Minheight); 
                DataRow dr = dt.Rows[0]; 
                HiddenRoleName.Value = Roleid.ToString();
                txtRoleName.Text = dr["RoleName"].ToString();
                pnl_UserLogin.Visible = true;
                btn_Save.Visible = true;
                btn_Cancel.Visible = true;
                LoadPages(int.Parse(ddlModule.SelectedValue));   
            }
        }
        
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("AdminDashBoard.aspx");
    }
    //public void createtreeview()
    //{
    //    StringBuilder sb = new StringBuilder();
    //    DataTable objdt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM APPLICATIONMASTER");
    //    sb.Append("<ul id='example'>");
    //    if ((objdt != null & objdt.Rows.Count > 0))
    //    {
    //        foreach (DataRow dr in objdt.Rows)
    //        {
    //            DataTable dtmoduls = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM MODULEMASTER WHERE APPLICATIONID=" + Convert.ToString(dr["ApplicationId"]));
    //            sb.Append("<li style='color:Red;'><span style='cursor:pointer;' onclick='ExpandCollaps(this);'>" + (dtmoduls.Rows.Count > 0 ? "+" : "&nbsp;") + "</span><input title='Vendor' type='checkbox' name='' onclick='checkchild(this);'></input>" + Convert.ToString(dr["ApplicationName"]));
    //            if ((dtmoduls != null & dtmoduls.Rows.Count > 0))
    //            {
    //                sb.Append("<ul style='display:none'>");
    //                foreach (DataRow drmodules in dtmoduls.Rows)
    //                {
    //                    DataTable dtpages = Common.Execute_Procedures_Select_ByQuery("SELECT PAGEID,PAGENAME FROM PAGEMASTER WHERE MODULEID=" + drmodules["ModuleId"].ToString() );
    //                    sb.Append("<li style='color:Blue;'><span style='cursor:pointer;' onclick='ExpandCollaps(this);'>" + (dtpages.Rows.Count > 0 ? "+" : "&nbsp;") + "</span><input title='Brand' type='checkbox' name='' onclick='checkchild(this);'/>" + Convert.ToString(drmodules["ModuleName"]));
    //                    if ((dtpages != null & dtpages.Rows.Count > 0))
    //                    {
    //                        sb.Append("<ul style='display:none'>");
    //                        foreach (DataRow drpages in dtpages.Rows)
    //                        {
    //                            sb.Append("<li style='color:green;'><input title='Product' type='checkbox' name='" + Convert.ToString(drpages["Pageid"]) + "' onclick='checkchild(this);'/>" + Convert.ToString(drpages["PageName"]) + "</li>");
    //                        }
    //                        sb.Append("</ul>");
    //                    }
    //                    sb.Append("</li>");

    //                }
    //                sb.Append("</ul>");

    //            }
    //            sb.Append("</li>");
    //        }
    //    }
    //    sb.Append("</ul>");
    //    ltrlTreeView.Text = sb.ToString();
    //}
    protected void ddlApplication_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadModules(int.Parse(ddlApplication.SelectedValue));
        LoadPages(int.Parse(ddlModule.SelectedValue));   
    }
    protected void ddlModule_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadPages(int.Parse(ddlModule.SelectedValue));   
    }
}
