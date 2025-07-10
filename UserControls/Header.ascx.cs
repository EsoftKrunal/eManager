using System;
using System.Collections;
using System.Configuration;
using System.Data;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;


public partial class UserControls_Header : System.Web.UI.UserControl
{
    int UserId = 0;
    public bool _HeaderVisible = false;
    public string CurrentApplicationImage
    {
        get
        {
            switch (CurrentModule)
            {
                case 1:
                    return "cms.png";
                    break;
                case 2:
                    return "vims.png";
                    break;
                case 3:
                    return "pms.png";
                    break;
                case 4:
                    return "pos.png";
                    break;
                case 5:
                    return "admin.png";
                    break;
                case 6:
                    return "admin.png";
                    break;
                case 11:
                    return "accounts.png";
                    break;
                default:
                    return "application.png";
                    break;
            }
        }
    }
    public int CurrentModule = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (CurrentModule == 0)
            {
                //CurrentModule = 10;
                CurrentModule = 1;
            }           
        }
        try
        {
            txtUserName.Text = "You have logged in as:   " + Session["FullName"].ToString();
            UserId = int.Parse(Session["UserId"].ToString());
            if (UserId != 1)
            {
                ((HtmlControl)btn_ADMIN.Parent).Visible = false;
                //((HtmlControl)btn_ADMIN.Parent).Style.Add("display", "none"); 
            }
        }
        catch
        {
            Response.Redirect("Login.aspx");
        }
        SetButtons();
    }
    private void SetButtons()
    {
        btn_DB.Attributes.Add("onclick", "DoPost(10)");
        btn_CMS.Attributes.Add("onclick", "DoPost(1)");
        btn_VIMS.Attributes.Add("onclick", "DoPost(2)");
        btn_POS.Attributes.Add("onclick", "DoPost(4)");
        btn_PMS.Attributes.Add("onclick", "DoPost(3)");
        btn_ADMIN.Attributes.Add("onclick", "DoPost(5)");
        btn_Analytics.Attributes.Add("onclick", "DoPost(6)");
        btn_eMTM.Attributes.Add("onclick", "DoPost(7)");
        btn_Hssqe.Attributes.Add("onclick", "DoPost(8)");
        btn_Accounts.Attributes.Add("onclick", "DoPost(11)");

        btn_ChPwd.Attributes.Add("onclick", "DoPost(99)");
    }
    protected void btn_LogOut_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Redirect("Login.aspx");
    }
    protected void btn_POST_Click(object sender, EventArgs e)
    {
        btn_DB.Attributes.Remove("class");
        ((HtmlControl)btn_DB.Parent).Attributes.Add("class", "");

        btn_CMS.Attributes.Remove("class");
        ((HtmlControl)btn_CMS.Parent).Attributes.Add("class", "");

        btn_VIMS.Attributes.Remove("class");
        ((HtmlControl)btn_VIMS.Parent).Attributes.Add("class", "");

        btn_POS.Attributes.Remove("class");
        ((HtmlControl)btn_POS.Parent).Attributes.Add("class", "");

        btn_PMS.Attributes.Remove("class");
        ((HtmlControl)btn_PMS.Parent).Attributes.Add("class", "");

        btn_Accounts.Attributes.Remove("class");
        ((HtmlControl)btn_Accounts.Parent).Attributes.Add("class", "");

        btn_ADMIN.Attributes.Remove("class");
        ((HtmlControl)btn_ADMIN.Parent).Attributes.Add("class", "");

        btn_Analytics.Attributes.Remove("class");
        ((HtmlControl)btn_Analytics.Parent).Attributes.Add("class", "");

        btn_eMTM.Attributes.Remove("class");
        ((HtmlControl)btn_eMTM.Parent).Attributes.Add("class", "");

        btn_Hssqe.Attributes.Remove("class");
        ((HtmlControl)btn_Hssqe.Parent).Attributes.Add("class", "");


        CurrentModule = int.Parse(hfd_CB.Value);
        switch (CurrentModule)
        {
            case 10:
                btn_DB.Attributes.Add("class", "new");
                ((HtmlControl)btn_DB.Parent).Attributes.Add("class", "current");
                break;
            case 1:
                btn_CMS.Attributes.Add("class", "new");
                ((HtmlControl)btn_CMS.Parent).Attributes.Add("class", "current");
                break;
            case 2:
                btn_VIMS.Attributes.Add("class", "new");
                ((HtmlControl)btn_VIMS.Parent).Attributes.Add("class", "current");
                break;
            case 3:
                btn_PMS.Attributes.Add("class", "new");
                ((HtmlControl)btn_PMS.Parent).Attributes.Add("class", "current");
                break;
            case 4:
                btn_POS.Attributes.Add("class", "new");
                ((HtmlControl)btn_POS.Parent).Attributes.Add("class", "current");
                break;
            case 5:
                btn_ADMIN.Attributes.Add("class", "new");
                ((HtmlControl)btn_ADMIN.Parent).Attributes.Add("class", "current");
                break;
            case 6:
                btn_Analytics.Attributes.Add("class", "new");
                ((HtmlControl)btn_Analytics.Parent).Attributes.Add("class", "current");
                break;
            case 7:
                btn_eMTM.Attributes.Add("class", "new");
                ((HtmlControl)btn_eMTM.Parent).Attributes.Add("class", "current");
                break;
            case 8:
                btn_Hssqe.Attributes.Add("class", "new");
                ((HtmlControl)btn_Hssqe.Parent).Attributes.Add("class", "current");
                break;
            case 11:
                btn_Accounts.Attributes.Add("class", "new");
                ((HtmlControl)btn_Accounts.Parent).Attributes.Add("class", "current");
                break;
            default:
                break;

        }
    }
    protected void btn_ChPwd_Click(object sender, EventArgs e)
    {
        //  divChPwd.Visible = true;
    }
}
