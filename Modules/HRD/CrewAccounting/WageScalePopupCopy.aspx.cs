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

public partial class Registers_WageScalePopupCopy : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        if (!IsPostBack)
        {
            if ("" + Request.QueryString["Mode"] == "History")
            {
                LoadHistory();
                dvHistory.Visible = true;
                dvCopy.Visible = false;
            }
            else
            {   Load_WageScale();
                Load_Country();
                dvCopy.Visible = true;
                dvHistory.Visible = false;
            }
        }
    }
    private void Load_WageScale()
    {
        DataSet ds = cls_SearchReliever.getMasterData("WageScale", "WageScaleID", "WageScaleName");
        ddcopywage.DataSource = ds.Tables[0];
        ddcopywage.DataTextField = "WageScaleName";
        ddcopywage.DataValueField = "WageScaleID";
        ddcopywage.DataBind();
        ddcopywage.Items.Insert(0, new ListItem("< Select >", "0"));
    }
    private void Load_Country()
    {
        DataSet ds = cls_SearchReliever.getMasterData("Country", "CountryID", "CountryName");
        ddcopynationality.DataSource = ds.Tables[0];
        ddcopynationality.DataTextField = "CountryName";
        ddcopynationality.DataValueField = "CountryID";
        ddcopynationality.DataBind();
        ddcopynationality.Items.Insert(0, new ListItem("< Select >", "0"));
    }
    public void LoadHistory()
    {
        DataTable dt;
        int WageScaleId, Seniority, NationalityId;
        WageScaleId = Convert.ToInt32("0" + Request.QueryString["WCId"]);
        Seniority = Convert.ToInt32("0" + Request.QueryString["Sen"]);
        NationalityId = Convert.ToInt32("0" + Request.QueryString["Nat"]);
        dt = WagesMaster.Get_Wage_Master_History(WageScaleId, NationalityId, Seniority);
        if (dt.Rows.Count > 0)
        {
            lblHistory.Text = "";
            rpt_History.DataSource = dt;
            rpt_History.DataBind();
        }
        else
        {
            lblHistory.Text = "History not exists for selected Wage Scale.";  
        }
    }
}
