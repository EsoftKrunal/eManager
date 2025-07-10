using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class UserControls_VendorSearchByDescription : System.Web.UI.UserControl
{
    public int PageNo
    {
        get { return Convert.ToInt32("0" + ViewState["PageNo"]); }
        set { ViewState["PageNo"] = value.ToString(); }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //txtDesc.Focus();
        if (!Page.IsPostBack)
        {
            PageNo = 1;
        }
    }

    protected void btnSearch_OnClick(object sender, EventArgs e)
    {
        PageNo = 1;
        Search();
    }
    protected void btnPaging_OnClick(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        int Page = Common.CastAsInt32(btn.Text);
        PageNo = Page;
        Search();
        btn.ForeColor = System.Drawing.Color.White;
    }

    //--------------------------
    public void Search()
    {
        MainDiv.Visible = true;
        dvPaging.Visible = true;
        //string sql = "select distinct top 15 bidDescription,BidID  from vw_tblSmdPOdetailbid where bidDescription like '%" + txtDesc.Text.Trim() + "%'";
        //DataTable Dt= Common.Execute_Procedures_Select_ByQuery(sql);
        DataSet ResDS = new DataSet();
        if (txtDesc.Text.Trim() != "")
        {
            Common.Set_Procedures("sp_Search_BidDescription");
            Common.Set_ParameterLength(2);
            Common.Set_Parameters(
                    new MyParameter("@TextToSearch", txtDesc.Text.Trim()),
                    new MyParameter("@PageNo", PageNo - 1)
                );
            ResDS = Common.Execute_Procedures_Select();
            rptDesc.DataSource = ResDS.Tables[0];
            rptDesc.DataBind();

            rptPaging.DataSource = ResDS.Tables[1];
            rptPaging.DataBind();

            if (ResDS.Tables[0].Rows.Count == 0)
            {
                MainDiv.Visible = false;
                dvPaging.Visible = false;
            }
        }
        else
        {
            rptDesc.DataSource = null;
            rptDesc.DataBind();

            rptPaging.DataSource = null;
            rptPaging.DataBind();

            MainDiv.Visible = false;
            dvPaging.Visible = false;
        }
    }
    public void Paging()
    {

    }
}