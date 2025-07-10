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

public partial class UserOnBehalfManagement : System.Web.UI.Page
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
            BindGrid();
            btn_Save.Visible = false;
            btn_Cancel.Visible = false;
            pnlList.Height = Unit.Pixel(MaxHeight);    
        }
    }
    public void LoadVessel()
    {
        rpt_Data1.DataSource = Common.Execute_Procedures_Select_ByQuery("select loginid,userid,convert(bit,(case when exists(select assigneduserid from useronbehalfright ub where ub.assigneduserid=" + HiddenUserId.Value + " and ub.onbehalfuserid=um.loginid) then 1 else 0 end)) as Permission from usermaster um where statusid='A' Order By Userid");
        rpt_Data1.DataBind(); 
    }
    protected void EditClick(object sender, ImageClickEventArgs e)
    {
        int Recid=int.Parse(((ImageButton)sender).CommandArgument);
        Show_Record(Recid);
        ViewState["SelId"] = Recid.ToString();
        BindGrid(); 
        pnl_UserLogin.Visible = true;
        btn_Save.Visible = true;   
    }
    protected void btn_Add_Click(object sender, EventArgs e)
    {
        HiddenUserId.Value = "0";
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
            if (HiddenUserId.Value.Trim() != "0")
            {
                Common.Execute_Procedures_Select_ByQuery("Delete from useronbehalfright where assigneduserid=" + HiddenUserId.Value);
                for (int i = 0; i <= rpt_Data1.Items.Count - 1; i++)
                {
                    HiddenField hfd = ((HiddenField)rpt_Data1.Items[i].FindControl("hfdUserId"));
                    CheckBox ch = ((CheckBox)rpt_Data1.Items[i].FindControl("chkView"));
                    if (ch.Checked )
                        Common.Execute_Procedures_Select_ByQuery("INSERT INTO useronbehalfright(assigneduserid,onbehalfuserid) values(" + HiddenUserId.Value + "," + hfd.Value +")");
                }
                Msgbox.ShowMessage("Rights assigned successfully.", false);
            }
            BindGrid();
            pnlList.Height = Unit.Pixel(MaxHeight);
        }
        catch
        {
            Msgbox.ShowMessage("Unable to assign Rights.", true);
        }
    }
    protected void btn_Cancel_Click(object sender, EventArgs e)
    {
        btn_Save.Visible = false ;
        btn_Cancel.Visible = false ;
        pnlList.Height = Unit.Pixel(MaxHeight); 
    }
    protected void Show_Record(int LoginId)
    {
        HiddenUserId.Value = LoginId.ToString();    
        LoadVessel();   
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("AdminDashBoard.aspx");
    }
}
