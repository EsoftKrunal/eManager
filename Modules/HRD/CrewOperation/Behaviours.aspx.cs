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

public partial class HSSQE_KST_Behaviours : System.Web.UI.Page
{
    public int BehaviourId
    {
        set { ViewState["BehaviourId"] = value; }
        get { return Common.CastAsInt32(ViewState["BehaviourId"]); }
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
            BehaviourId = 0;
            bindgrid();
        }        
    }    
    public void bindgrid()
    {
        rptGrid.DataSource = Common.Execute_Procedures_Select_ByQueryCMS("select * from KST_BehaviourMaster  order by BehaviourName");
        rptGrid.DataBind();
    }
    protected void btn_add_Click(object sender, EventArgs e)
    {
        BehaviourId = 0;
        txtBehaviour.Text = "";
        divPanel.Visible = true;
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        int _key = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("select * from KST_BehaviourMaster where BehaviourId=" + _key);
        if (dt.Rows.Count > 0)
        {
            BehaviourId = _key;
            txtBehaviour.Text = dt.Rows[0]["BehaviourName"].ToString();
            divPanel.Visible = true;
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        int _key = Common.CastAsInt32(((ImageButton)sender).CommandArgument);

    }
    protected void btn_save_Click(object sender, EventArgs e)
    {
        Common.Set_Procedures("IU_KST_BehaviourMaster");
        Common.Set_ParameterLength(2);
        Common.Set_Parameters(
             new MyParameter("@BehaviourId", BehaviourId),
             new MyParameter("@BehaviourName", txtBehaviour.Text.Trim().Replace("'","`")));
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
        BehaviourId = 0;
        txtBehaviour.Text = "";
        divPanel.Visible = false;
    }

}