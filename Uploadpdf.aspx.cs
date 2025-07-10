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

public partial class Uploadpdf : System.Web.UI.Page
{
    int UserId;
    int MaxHeight = 410, Minheight = 410;
    public void BindGrid()
    {
        rpt_Data.DataSource = Common.Execute_Procedures_Select_ByQuery("select ApplicationId,ApplicationName From ApplicationMaster order By ApplicationName");
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
        rpt_Data1.DataSource = Common.Execute_Procedures_Select_ByQuery("select ModuleId,ModuleName from ModuleMaster Where ApplicationId=" + HiddenUserId.Value);
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
                for (int i = 0; i <= rpt_Data1.Items.Count - 1; i++)
                {
                    HiddenField hfd = ((HiddenField)rpt_Data1.Items[i].FindControl("hfdModuleId"));
                    FileUpload fld = ((FileUpload)rpt_Data1.Items[i].FindControl("flp1"));
                    if (fld.HasFile)
                        fld.SaveAs(Server.MapPath("~/manuals/Module_" + hfd.Value + ".pdf"));  
                }
                Msgbox.ShowMessage("Manual uploaded successfully.", false);
            }
            LoadVessel();
            pnlList.Height = Unit.Pixel(MaxHeight);
        }
        catch(Exception ex)
        {
            Msgbox.ShowMessage(ex.Message, true);
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
    public bool HasFile(string ModuleId)
    {
        if (System.IO.File.Exists(Server.MapPath("~/manuals/" + "Module_" + ModuleId + ".pdf")))
           return true;
        else
           return false; 
    }
    public string getFilePath(string ModuleId)
    {
        return "window.open('./manuals/Module_" + ModuleId + ".pdf');" ;
    }
}
