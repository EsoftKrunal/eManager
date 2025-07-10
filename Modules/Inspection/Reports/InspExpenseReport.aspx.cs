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

public partial class Reports_InspExpenseReport : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    public int InspId
    {
        get { return Common.CastAsInt32(ViewState["InspId"]); }
        set { ViewState["InspId"] = value; }
    }
    public int MaxMode
    {
        get { return Common.CastAsInt32(ViewState["MaxMode"]); }
        set { ViewState["MaxMode"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        InspId = Common.CastAsInt32(Request.QueryString["InspId"]);
        MaxMode = Common.CastAsInt32(Request.QueryString["MaxMode"]);
        Show_Report();
    }
    protected void Show_Report()
    {
        this.CrystalReportViewer1.Visible = true;
        CrystalReportViewer1.ReportSource = rpt;
        rpt.Load(Server.MapPath("InspExpenseReport.rpt"));
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("exec dbo.get_RPT_InspectionBonusMaster " + InspId.ToString() + "," + MaxMode.ToString());
        dt.TableName = "get_RPT_InspectionBonusMaster;1";
        DataTable dt1 = Common.Execute_Procedures_Select_ByQuery("exec dbo.get_RPT_InspectionBonusDetails " + InspId.ToString() + "," + MaxMode.ToString());
        dt1.TableName = "get_RPT_InspectionBonusDetails;1";

        DataSet ds=new DataSet();
        ds.Tables.Add(dt.Copy());
        ds.Tables.Add(dt1.Copy());
        rpt.SetDataSource(ds);

        DataTable dt2 = Common.Execute_Procedures_Select_ByQuery("select ( case when Mode=2 then 'Approval' when Mode=3 then 'Approval-2' else 'Review' end )  + ' Done by ' + (firstname + ' ' + lastname ) + ' on ' + convert(Varchar,i.CreatedOn,106) as Expr1000" +
                                                                       " from InspectionBonusMaster i inner join dbo.userlogin u on u.loginid=i.createdby where Mode >0 and i.inspectionid=" + InspId.ToString() +  " order by Mode ");
        rpt.Subreports[0].SetDataSource(dt2);
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
}
