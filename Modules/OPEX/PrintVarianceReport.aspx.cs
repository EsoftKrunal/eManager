using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;

public partial class PrintVarianceReport : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    #region Properties ****************************************************
    public string CompanyCode
    {
        set { ViewState["CompanyCode"] = value; }
        get { return ViewState["CompanyCode"].ToString(); }
    }
    public string VesselCode
    {
        set { ViewState["VesselCode"] = value; }
        get { return ViewState["VesselCode"].ToString(); }
    }

    public bool IndianFinYear
    {
        get
        { return Convert.ToBoolean(ViewState["IndianFinYear"]); }
        set
        { ViewState["IndianFinYear"] = value; }
    }
    #endregion
    public int ReportLevelIndex = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        //---------------------------------------
        ProjectCommon.SessionCheck();
        //---------------------------------------
        if (!Page.IsPostBack)
        {
            if (Page.Request.QueryString["Query"] != null )
            {
                string[] Vals = Page.Request.QueryString["Query"].ToString().Split('~');
                
                lblYear.Text = Vals[0].ToString();
                lblMonth.Text = Vals[1].ToString();
                lblCompany.Text = Vals[2].ToString();
                lblVessel.Text=Vals[3].ToString();
                CompanyCode = Vals[4].ToString();
                VesselCode = Vals[5].ToString();
                IndianFinYear = Convert.ToBoolean(Vals[6].ToString());
                try
                {
                    
                    ddlReportLevel.SelectedIndex = Common.CastAsInt32(Vals[8]);
                    ReportLevelIndex = ddlReportLevel.SelectedIndex;
                    ShowReport();      
                }
                catch { }
                
            }
        }

    }
    public void ShowReport()
    {
        Session["PrintData"] = null;
        Session["TUtil"] = null;
        if (ReportLevelIndex != 0)
        {
            string Query = "Month=" + lblMonth.Text + "&Year=" + lblYear.Text + "&Company=" + lblCompany.Text + "&Vessel=" + lblVessel.Text + "&ReportLevel=" + ReportLevelIndex + "&CompCode=" + CompanyCode + "&VesselCode=" + VesselCode + "&IndianFinYear=" + IndianFinYear;
            IFRAME1.Attributes.Add("src", "PrintVarianceReportCrystal.aspx?" + Query);
        }
        else
        {
            IFRAME1.Attributes.Add("src", "");
        }

    }

    protected void ddlReportLevel_OnSelectedIndexChanged(object sener, EventArgs e)
    {
        ReportLevelIndex = ddlReportLevel.SelectedIndex;
        ShowReport();
        //if (ddlReportLevel.SelectedIndex == 0)
        //    return;
        //if (ddlCompany.SelectedIndex == 0)
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "alert('Please select Company.')", true);
        //    ddlReportLevel.SelectedIndex = 0;
        //    return;
        //}
        //if (ddlVessel.SelectedIndex == 0)
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "b", "alert('Please select Vessel.')", true);
        //    ddlReportLevel.SelectedIndex = 0;
        //    return;
        //}

        //if (ddlReportLevel.SelectedIndex == 4)
        //{
        //    string Query = "";
        //    Query = ddlyear.SelectedValue + "~" + ddlMonth.SelectedValue + "~" + ddlCompany.SelectedItem + "~" + ddlVessel.SelectedItem + "~" + ddlCompany.SelectedValue + "~" + ddlVessel.SelectedValue + "~Account Details~4";
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Print", "window.open('PrintVarianceReport.aspx?Query=" + Query + "');", true);
        //}
        //else if (ddlReportLevel.SelectedIndex == 5)
        //{
        //    int VessNum = GetVesselNum();
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Printt", "window.open('Print.aspx?CoCode=" + ddlCompany.SelectedValue + "&VessNum=" + VessNum + "&CompName=" + ddlCompany.SelectedItem + "&VesselName=" + ddlVessel.SelectedItem + "&Month=" + ddlMonth.SelectedValue + "&ToMonth=" + ddlMonth.SelectedValue + "&Year=" + ddlyear.SelectedValue + "');", true);
        //    ddlReportLevel.SelectedIndex = 0;
        //    //Response.Redirect("Print.aspx?CoCode=" + ddlCompany.SelectedValue + "&VessNum=" + VessNum + "&CompName=" + ddlCompany.SelectedItem + "&VesselName=" + ddlVessel.SelectedItem + "&Month=" + ddlMonth.SelectedValue + "&Year=" + ddlyear.SelectedValue + "");
        //}
        //else if (ddlReportLevel.SelectedIndex == 6)
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Printtt", "window.open('Print.aspx?LumpSum=1&CoCode=" + ddlCompany.SelectedValue + "&Period=" + ddlMonth.SelectedValue + "&Year=" + ddlyear.SelectedValue + "&VesselName=" + ddlVessel.SelectedItem + "&CompanyName=" + ddlCompany.SelectedItem.Text + "&VessCode=" + ddlVessel.SelectedValue + "');", true);
        //    ddlReportLevel.SelectedIndex = 0;
        //}
        
    }
}
