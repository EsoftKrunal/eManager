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

public partial class CrewAccounting_PrintWageScale : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
  
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        int Wid=0, Sid=0, Nid=0;
        DataTable dtHeader = Budget.getTable("select ComponentName from wagescalecomponents order by wagescalecomponentid").Tables[0];
        DataTable dt= new DataTable();

        if ("" + Session["PressMode"] == "Show")
        {
            Wid=int.Parse("0" + Session["Show_WageScaleId"]);
            Sid = int.Parse("0" + Session["Show_Seniority"]);
            Nid = int.Parse("0" + Session["Show_NationalityId"]);
            dt = WagesMaster.WageComponents(Wid, Nid, Sid);
        }
        else if ("" + Session["PressMode"] == "Copy")
        {
            Wid = int.Parse("0" + Session["Copy_WageScaleId"]);
            Sid = int.Parse("0" + Session["Copy_Seniority"]);
            Nid = int.Parse("0" + Session["Copy_NationalityId"]);
            dt = WagesMaster.WageComponents(int.Parse("0" + Session["Copy_WageScaleId"]), int.Parse("0" + Session["Copy_NationalityId"]), int.Parse("0" + Session["Copy_Seniority"]));
        }
        else if ("" + Session["PressMode"] == "History")
        {
            DataTable dtname = Budget.getTable("select wagescaleid,nationalityid,seniority from wagescalerankhistory where wagescalerankid=" + "0" + Session["History_WageComponentsId"]).Tables[0];
            dt = WagesMaster.WageComponents_FromHistory(Convert.ToInt32("0" + Session["History_WageComponentsId"]));
            Wid = int.Parse("0" + dtname.Rows[0]["wagescaleid"]);
            Sid = int.Parse("0" + dtname.Rows[0]["seniority"]);
            Nid = int.Parse("0" + dtname.Rows[0]["nationalityid"]);
        }

        DataTable dt_wgname = Budget.getTable("SELECT WAGESCALENAME FROM WAGESCALE WHERE WAGESCALEID=" + Wid.ToString()).Tables[0];  
        Session["WageScale_Name"] = dt_wgname.Rows[0][0].ToString();
        dt_wgname = Budget.getTable("SELECT COUNTRYNAME FROM COUNTRY WHERE COUNTRYID=" + Nid.ToString()).Tables[0];
        Session["Nationality_Name"] = dt_wgname.Rows[0][0].ToString();
        Session["Seniority_Name"]=Sid.ToString();

        DataView dv = dt.DefaultView;
        dv.RowFilter = "Total > 0";
        dt=dv.ToTable(); 
        
        //CrystalReportViewer1.HasPrintButton = Alerts.Check_ReportPrintingAuthority(Convert.ToInt32(Session["loginid"].ToString()), 123);
        CrystalReportViewer1.DisplayGroupTree = false;   

            CrystalReportViewer1.Visible = true;
            CrystalReportViewer1.ReportSource = rpt;
            rpt.Load(Server.MapPath("Rpt_WageMaster.rpt"));
            rpt.SetDataSource(dt);
            for(int i=1;i<=12;i++)
            {
                rpt.SetParameterValue("Param" + i.ToString() , dtHeader.Rows[i-1][0].ToString());
            }
            rpt.SetParameterValue("WageScale","" + Session["WageScale_Name"] );
            rpt.SetParameterValue("Nationality", "" + Session["Nationality_Name"]);
            rpt.SetParameterValue("Seniority", "" + Session["Seniority_Name"]);
            rpt.SetParameterValue("WefDate", "" + "" + Session["EffDate"]);
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
}
