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

public partial class UserVesselManagement : System.Web.UI.Page
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
            btn_Add_Click(sender,e);
        }
    }
    public void LoadVessel()
    {
        rpt_Data1.DataSource = ECommon.Execute_Procedures_Select_ByQuery_CMS("select vs.vesselid,vesselname,convert(bit,(case when uv.loginid is null then 0 else 1 end)) as Permission from vessel vs left join uservesselrelation uv on vs.vesselid=uv.vesselid and uv.loginid=" + HiddenUserId.Value + " where vs.vesselstatusid<>2  order by vesselname");
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
                ECommon.Execute_Procedures_Select_ByQuery_CMS("Delete from uservesselrelation where LoginId=" + HiddenUserId.Value);
                for (int i = 0; i <= rpt_Data1.Items.Count - 1; i++)
                {
                    HiddenField hfd = ((HiddenField)rpt_Data1.Items[i].FindControl("hfdVesselId"));
                    CheckBox ch = ((CheckBox)rpt_Data1.Items[i].FindControl("chkView"));
                    if (ch.Checked )
                        ECommon.Execute_Procedures_Select_ByQuery_CMS("INSERT INTO uservesselrelation(loginid,vesselid,createdby,createdon) values(" + HiddenUserId.Value + "," + hfd.Value + "," + UserId.ToString() + ",getdate())");
                }
                Msgbox.ShowMessage("Vessel assigned successfully.", false);
            }
            BindGrid();
            pnlList.Height = Unit.Pixel(MaxHeight);
        }
        catch
        {
            Msgbox.ShowMessage("Unable to assign vessel.", true);
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
