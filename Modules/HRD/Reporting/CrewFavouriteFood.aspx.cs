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

public partial class CrewFavouriteFood : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    public int FoodId
    {
        get { return Common.CastAsInt32(ViewState["FoodId"]); }
        set { ViewState["FoodId"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        CrystalReportViewer1.HasPrintButton = Alerts.Check_ReportPrintingAuthority(Convert.ToInt32(Session["loginid"].ToString()),33);
        //==========
        lblMessage.Visible = true;
        lblMessage.Text = "";
        CrystalReportViewer1.Visible = false;
        FoodId = Common.CastAsInt32(Request.QueryString["FoodId"]);

        string sql = "select * from CrewCuisineDetails where MenuId=" + FoodId.ToString();
        DataTable dt_Data = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        dt_Data.TableName = "CrewCuisineDetails";
        sql = "select * from crewpersonaldetails where crewid in (select crewid from CrewCuisineDetails where MenuId=" + FoodId.ToString() + ")";
        DataTable dt_Data1= Common.Execute_Procedures_Select_ByQueryCMS(sql);
        dt_Data1.TableName = "crewpersonaldetails";
        sql = "select * from Rank where RankId in (select currentrankid from crewpersonaldetails where crewid in (select crewid from CrewCuisineDetails where MenuId=" + FoodId.ToString() + "))";
        DataTable dt_Data2 = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        dt_Data2.TableName = "Rank";


        DataSet ds = new DataSet();

        ds.Tables.Add(dt_Data.Copy());
        ds.Tables.Add(dt_Data1.Copy());
        ds.Tables.Add(dt_Data2.Copy());

        if (dt_Data.Rows.Count > 0)
        {
            CrystalReportViewer1.Visible = true;
            CrystalReportViewer1.ReportSource = rpt;
            rpt.Load(Server.MapPath("CrewFavouriteFood.rpt"));
            rpt.SetDataSource(ds);
        }
        else
        {
            lblMessage.Text = "No Record Found";
            CrystalReportViewer1.Visible = false;
        }
    }
    
}
