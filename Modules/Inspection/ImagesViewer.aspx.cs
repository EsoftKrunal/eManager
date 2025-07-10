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

public partial class Transactions_ImagesViewer : System.Web.UI.Page
{
    int intInspDueId = 0;// intSrNum = 0;
    string strSrNum = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        try
        {
            intInspDueId = int.Parse(Page.Request.QueryString["InspId"].ToString());
            //intSrNum = int.Parse(Page.Request.QueryString["SrNumb"].ToString());
            strSrNum = Page.Request.QueryString["SrNumb"].ToString();
        }
        catch { }
        if (!Page.IsPostBack)
        {
            BindImages();
        }
    }
    protected void BindImages()
    {
        try
        {
            DataTable dt1 = Safety_Inspection.InsertSafetyInspChildDetails(0, intInspDueId, strSrNum, "", "", 0, "ById");
            lb_desc.Text=dt1.Rows[0]["PicCaption"].ToString();
            lb_tot.Text = "Total Images : " + dt1.Rows.Count.ToString();
            GridView1.DataSource = dt1;
            GridView1.DataBind();
            GridView2.DataSource = dt1;
            GridView2.DataBind();
        }
        catch { }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        BindImages();
    }
    protected void GridView2_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            Image img1 = (Image)e.Item.FindControl("img2");
            img1.Attributes.Add("onmouseover", "javascript:imageChng(this.id);");
            img1.Attributes.Add("onMouseout", "javascript:imageChng1(this.id);");
        }
    }
}
