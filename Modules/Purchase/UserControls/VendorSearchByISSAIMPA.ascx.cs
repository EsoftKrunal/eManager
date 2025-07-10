using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class VendorSearchByISSAIMPA : System.Web.UI.UserControl
{
    public int PageNo
    {
        get { return Convert.ToInt32("0" + ViewState["PageNo"]); }
        set { ViewState["PageNo"] = value.ToString(); }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //txtVendorName.Focus();
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
        MainDiv_ISSA.Visible = true;
        dvPaging_ISSA.Visible = true;
        DataSet ResDS = new DataSet();
        if (txtVendorISSA.Text.Trim() != "")
        {
            Common.Set_Procedures("sp_Search_VendorByISSAIMPA");
            Common.Set_ParameterLength(2);
            Common.Set_Parameters(
                    new MyParameter("@TextToSearch", txtVendorISSA.Text.Trim()),
                    new MyParameter("@PageNo", PageNo - 1)
                );
            ResDS = Common.Execute_Procedures_Select();
            rptDesc.DataSource = ResDS.Tables[0];
            rptDesc.DataBind();

            rptPaging.DataSource = ResDS.Tables[1];
            rptPaging.DataBind();

            if (ResDS.Tables[0].Rows.Count == 0)
            {
                MainDiv_ISSA.Visible = false;
                dvPaging_ISSA.Visible = false;
            }
        }
        else
        {
            rptDesc.DataSource = null;
            rptDesc.DataBind();

            rptPaging.DataSource = null;
            rptPaging.DataBind();

            MainDiv_ISSA.Visible = false;
            dvPaging_ISSA.Visible = false;
        }
    }    
}