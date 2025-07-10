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
using ShipSoft.CrewManager.BusinessObjects;
using ShipSoft.CrewManager.BusinessLogicLayer;
using ShipSoft.CrewManager.Operational;
using System.IO;

public partial class CrewOperation_ViewWageScaleHistory : System.Web.UI.Page
{
    public int WageScaleRankId
    {
        get { return Common.CastAsInt32(ViewState["WageScaleRankId"]); }
        set { ViewState["WageScaleRankId"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        if (!IsPostBack)
        {
            WageScaleRankId = Common.CastAsInt32(Request.QueryString["WageScaleRankId"]);
            BindRepeater();
        }
    }
    protected void BindRepeater()
    {
        //----------------------------
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("select WRH.Wagescaleid,WAGESCALENAME,SENIORITY,WEFDATE from WageScaleRankHistory WRH INNER JOIN WageScale W ON WRH.WageScaleId=W.WageScaleId WHERE WageScaleRankId=" + WageScaleRankId + " ORDER BY WEFDATE DESC");
        if (dt.Rows.Count > 0)
        {
            if (Convert.IsDBNull(dt.Rows[0]["WEFDate"]))
            {
                lblWageScaleName.Text=dt.Rows[0]["WAGESCALENAME"].ToString();
                lblSeniority.Text = dt.Rows[0]["SENIORITY"].ToString();
                lblEffectiveFrom.Text = "";
            }
            else
            {
                lblWageScaleName.Text = dt.Rows[0]["WAGESCALENAME"].ToString();
                lblSeniority.Text = dt.Rows[0]["SENIORITY"].ToString();
                lblEffectiveFrom.Text = Common.ToDateString(dt.Rows[0]["WEFDATE"]);
            }
        }
        else
        {
           lblWageScaleName.Text = "";
           lblSeniority.Text = "";
           lblEffectiveFrom.Text = "";
        }
        //----------------------------
        for (int i=1;i<=12;i++)
        {
            DataTable dt1 = wagecomponent.selectWageScaleDetailsById(i, Common.CastAsInt32(dt.Rows[0]["Wagescaleid"]), 0, Convert.ToInt32(dt.Rows[0]["SENIORITY"]));
            Label lb=((Label)this.FindControl("Label" + i.ToString()));
            if (lb != null)
            {
                lb.Text = dt1.Rows[0][1].ToString();
                if (dt1.Rows[0][0].ToString() == "N")
                    lb.CssClass = "invalidtext";
            }
        }

        dt = WagesMaster.WageComponents_FromHistory(WageScaleRankId);
        
        rptWages.DataSource = dt;
        rptWages.DataBind();
    }
    public string FormatCurr(object _in)
    {
        return string.Format("{0:0.00}", _in);
    }
}