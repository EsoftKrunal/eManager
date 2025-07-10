using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using Ionic.Zip;

public partial class HSSQE_KST_MTMValueMaster : System.Web.UI.Page
{
    public int MTMValueId
    {
        set { ViewState["MTMValueId"] = value; }
        get { return Common.CastAsInt32(ViewState["MTMValueId"]); }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        SessionManager.SessionCheck_New();
        //------------------------------------
        AuthenticationManager au = new AuthenticationManager(317,Common.CastAsInt32(Session["loginid"]),ObjectType.Page);
        if(!au.IsView)
        {
            Response.Redirect("Blank.aspx",true);
        }
        if (!IsPostBack)
        {
            MTMValueId = 0;
            ddlBehaviour.DataSource= Common.Execute_Procedures_Select_ByQueryCMS("select * from KST_BehaviourMaster order by BehaviourName");
            ddlBehaviour.DataTextField = "BehaviourName";
            ddlBehaviour.DataValueField = "BehaviourId";
            ddlBehaviour.DataBind();
            ddlBehaviour.Items.Insert(0, new ListItem("< Select >", ""));
            bindgrid();
        }        
    }    
    public void bindgrid()
    {
        rptGrid.DataSource = Common.Execute_Procedures_Select_ByQueryCMS("select v.*,b.BehaviourName from KST_MTMValueMaster v inner join KST_BehaviourMaster b on v.BehaviourId=b.BehaviourId order by BehaviourName, MTMValueName");
        rptGrid.DataBind();
    }
    protected void btn_add_Click(object sender, EventArgs e)
    {
        MTMValueId = 0;
        ddlBehaviour.SelectedIndex = 0;
        txtMTMValue.Text = "";
        divPanel.Visible = true;
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        int _key = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("select * from KST_MTMValueMaster where MTMValueId=" + _key);
        if (dt.Rows.Count > 0)
        {
            MTMValueId = _key;
            ddlBehaviour.SelectedValue = dt.Rows[0]["BehaviourId"].ToString();
            txtMTMValue.Text = dt.Rows[0]["MTMValueName"].ToString();
            divPanel.Visible = true;
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        int _key = Common.CastAsInt32(((ImageButton)sender).CommandArgument);

    }
    protected void btn_save_Click(object sender, EventArgs e)
    {
        Common.Set_Procedures("IU_KST_MTMValueMaster");
        Common.Set_ParameterLength(3);
        Common.Set_Parameters(
             new MyParameter("@MTMValueId", MTMValueId),
             new MyParameter("@BehaviourId", ddlBehaviour.SelectedValue),
             new MyParameter("@MTMValueName", txtMTMValue.Text.Trim().Replace("'","`")));
        Boolean res;
        DataSet Ds = new DataSet();
        res = Common.Execute_Procedures_IUD_CMS(Ds);
        if (res)
        {
            bindgrid();
            divPanel.Visible = false;
        }
        else
        {

        }       
       
    }
    protected void btn_Cancel_Click(object sender, EventArgs e)
    {
        MTMValueId = 0;
        ddlBehaviour.SelectedIndex = 0;
        txtMTMValue.Text = "";
        divPanel.Visible = false;
    }

}