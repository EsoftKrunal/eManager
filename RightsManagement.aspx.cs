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

public partial class RightsManagement : System.Web.UI.Page
{
    int UserId;
    int MaxHeight = 410, Minheight = 410;
    public void BindGrid()
    {
        rpt_Data.DataSource = Common.Execute_Procedures_Select_ByQuery("select LoginId,UserId as UserName From UserMaster where StatusId='A' Order By UserName");
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
        rpt_Data1.DataSource = Common.Execute_Procedures_Select_ByQuery("select pm.PageId,isnull((SELECT roleid from usermaster um1 where um1.loginid=" + HiddenUserId.Value + "),0) as RoleId,pagename,isnull(isview,0) as IsView,isnull(isAdd,0) as IsAdd,isnull(isUpdate,0) as IsUpdate,isnull(isdelete,0) as IsDelete,isnull(isprint,0) as IsPrint,isnull(isverify,0) as IsVerify,isnull(isverify2,0) as IsVerify2 from pagemaster pm left join userpagerelation upr on pm.pageid=upr.pageid And upr.UserId=" + HiddenUserId.Value + " Where pm.moduleid=" + ModuleId.ToString());
        rpt_Data1.DataBind(); 
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
        HiddenUserId.Value = "0";
        lblRoleName.Text = "";
        pnl_UserLogin.Visible = true;
        btn_Save.Visible = false;
        btn_Cancel.Visible = true;
        ViewState["SelId"] = 0;
        pnlList.Height = Unit.Pixel(Minheight); 
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        try
        {
            if (HiddenUserId.Value.Trim()!= "0")
            {
                Common.Execute_Procedures_Select_ByQuery("Delete from userpagerelation where UserId=" + HiddenUserId.Value + " And PageId in(Select pageid from pagemaster pm where pm.moduleid=" + ddlModule.SelectedValue + ")");
                for (int i = 0; i <= rpt_Data1.Items.Count - 1; i++)
                {
                    bool View = false, Add = false, Update = false, Delete = false, Print = false, Verify = false, Verify2 = false;
                    HiddenField hfd = ((HiddenField)rpt_Data1.Items[i].FindControl("pageid"));
                    CheckBox ch= ((CheckBox)rpt_Data1.Items[i].FindControl("chkView"));
                    CheckBox ch1 = ((CheckBox)rpt_Data1.Items[i].FindControl("chkAdd"));
                    CheckBox ch2 = ((CheckBox)rpt_Data1.Items[i].FindControl("chkUpdate"));
                    CheckBox ch3 = ((CheckBox)rpt_Data1.Items[i].FindControl("chkDelete"));
                    CheckBox ch4 = ((CheckBox)rpt_Data1.Items[i].FindControl("chkPrint"));
                    CheckBox ch5 = ((CheckBox)rpt_Data1.Items[i].FindControl("chkVerify"));
                    CheckBox ch6 = ((CheckBox)rpt_Data1.Items[i].FindControl("chkVerify2"));

                    View = ch.Visible && ch.Checked;
                    Add = ch1.Visible && ch1.Checked;
                    Update = ch2.Visible && ch2.Checked;
                    Delete = ch3.Visible && ch3.Checked;
                    Print = ch4.Visible && ch4.Checked;
                    Verify = ch5.Visible && ch5.Checked;
                    Verify2 = ch6.Visible && ch6.Checked;
 
                    Common.Execute_Procedures_Select_ByQuery("INSERT INTO userpagerelation(pageid,userid,isview,isadd,isupdate,isdelete,isprint,isverify,isverify2,createdby,createdon) values(" + hfd.Value + "," + HiddenUserId.Value + "," + ((View) ? "1" : "0") + "," + ((Add) ? "1" : "0") + "," + ((Update) ? "1" : "0") + "," + ((Delete) ? "1" : "0") + "," + ((Print) ? "1" : "0") + "," + ((Verify) ? "1" : "0") + "," + ((Verify2) ? "1" : "0") + "," + UserId.ToString() + ",getdate())");
                }
                Msgbox.ShowMessage("Rights updated successfully.", false);
            }
            BindGrid();
            pnlList.Height = Unit.Pixel(MaxHeight); 
        }
        catch
        {
            Msgbox.ShowMessage("Unable to update Rights information.", true);
        }
    }
    protected void btn_Cancel_Click(object sender, EventArgs e)
    {
        //pnl_UserLogin.Visible = false;
        btn_Save.Visible = false ;
        btn_Cancel.Visible = false ;
        pnlList.Height = Unit.Pixel(MaxHeight); 
    }
    protected void Show_Record(int LoginId)
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("select LoginId,UserId,isnull((select rolename from rolemaster rm where rm.roleid=um.roleid),'') as RoleName from UserMaster um where loginId=" + LoginId.ToString());
        if (dt != null)
        {
            if (dt.Rows.Count >0)
            {
                pnlList.Height =Unit.Pixel(Minheight); 
                DataRow dr = dt.Rows[0]; 
                HiddenUserId.Value = LoginId.ToString();
                lblUserName.Text = dr["UserId"].ToString();
                lblRoleName.Text = dr["RoleName"].ToString();
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
    public bool IsRoleRight(int _RoleId,int _PageId,int RightType)
    {
        Common.Set_Procedures("getRolePageRights");
        Common.Set_ParameterLength(2);
        Common.Set_Parameters(new MyParameter("PageId", _PageId), new MyParameter("RoleId", _RoleId));
        DataTable dt = Common.Execute_Procedures_Select().Tables[0];
        if (dt != null)
        {
            switch (RightType)
            {
                case 1:
                return Convert.ToBoolean(dt.Rows[0]["IsView"]);
                break;
                case 2:
                return Convert.ToBoolean(dt.Rows[0]["IsAdd"]);
                break;
                case 3:
                return Convert.ToBoolean(dt.Rows[0]["IsUpdate"]);
                break;
                case 4:
                return Convert.ToBoolean(dt.Rows[0]["IsDelete"]);
                break;
                case 5:
                return Convert.ToBoolean(dt.Rows[0]["IsPrint"]);
                break;
                case 6:
                return Convert.ToBoolean(dt.Rows[0]["IsVerify"]);
                break;
                case 7:
                return Convert.ToBoolean(dt.Rows[0]["IsVerify2"]);
                break; 
                default:
                return false;
                break; 
            }
        }
        else
        {
            return false;
        }
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
