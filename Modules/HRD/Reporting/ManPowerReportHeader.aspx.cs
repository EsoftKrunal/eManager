using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Text;  
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class Reporting_ManPowerReportHeader : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 103);
        if (chpageauth <= 0)
        {
            Response.Redirect("DummyReport.aspx");

        }
        //*******************
    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        DataTable Res=new DataTable();
 
        String OffCrew="";
        int CrewType = 0;
        string RankQuery,NatQuery,CrewQuery="";

        OffCrew =DropDownList2.SelectedValue;
        CrewType = int.Parse(DropDownList1.SelectedValue);
        //---------------
        if (OffCrew == "A")
            RankQuery = "SELECT RANKID from rank";
        else
            RankQuery = "SELECT RANKID from rank where OffCrew='" + OffCrew + "'";
        //---------------
        if (CrewType == 0)
            CrewQuery = "select DISTINCT NATIONALITYID from CrewPersonalDetails where CrewStatusId=1 AND RANKAPPLIEDID IN(" + RankQuery + ")";
        else if (CrewType == 1)
            CrewQuery = "select DISTINCT NATIONALITYID from CrewPersonalDetails where CrewStatusId=2 AND CURRENTRANKID IN(" + RankQuery + ")";
        else if (CrewType == 2)
            CrewQuery = "select DISTINCT NATIONALITYID from CrewPersonalDetails where CrewStatusId=3 AND CURRENTRANKID IN(" + RankQuery + ")";
        else
            CrewQuery = "select DISTINCT NATIONALITYID from CrewPersonalDetails where CrewStatusId=1 AND RANKAPPLIEDID IN(" + RankQuery + ") union select DISTINCT NATIONALITYID from CrewPersonalDetails where (CrewStatusId=2 or CrewStatusId=3) AND CURRENTRANKID IN(" + RankQuery + ")";
        //---------------
        NatQuery = "SELECT COUNTRYID,NATIONALITYCODE FROM COUNTRY WHERE COUNTRYID IN (" + CrewQuery + ")";

        DataSet dscols = Budget.getTable(NatQuery);
        DataSet dsrank = Budget.getTable(RankQuery.Replace("RANKID", "RANKID,RANKCODE") + " ORDER BY RANKLEVEL");
        DataTable cols = dscols.Tables[0];
        DataTable ranks = dsrank.Tables[0];
        Res.Columns.Add("RankCode");  
        for (int i = 0; i <= cols.Rows.Count - 1; i++)
        {
            Res.Columns.Add(cols.Rows[i]["NATIONALITYCODE"].ToString(), Type.GetType("System.Int32"));
        }
        Res.Columns.Add("Total", Type.GetType("System.Int32"));  
        for (int i = 0; i <= ranks.Rows.Count - 1; i++)
        {
            Res.Rows.Add(Res.NewRow());
            Res.Rows[i][0] = ranks.Rows[i]["RANKCODE"];
            int SumRow = 0; 
            for (int j = 0; j <= cols.Rows.Count - 1; j++)
            {
                DataSet tmp = Budget.getTable("SELECT COUNT(*) FROM CREWPERSONALDETAILS WHERE NATIONALITYID=" + cols.Rows[j]["COUNTRYID"] + " AND CrewStatusId=1 AND RANKAPPLIEDID=" + ranks.Rows[i]["RANKID"] );
                DataSet tmp1 = Budget.getTable("SELECT COUNT(*) FROM CREWPERSONALDETAILS WHERE NATIONALITYID=" + cols.Rows[j]["COUNTRYID"] + " AND CrewStatusId=2 AND CURRENTRANKID=" + ranks.Rows[i]["RANKID"]);
                DataSet tmp2 = Budget.getTable("SELECT COUNT(*) FROM CREWPERSONALDETAILS WHERE NATIONALITYID=" + cols.Rows[j]["COUNTRYID"] + " AND CrewStatusId=3 AND CURRENTRANKID=" + ranks.Rows[i]["RANKID"]);
                if (CrewType==0)
                    Res.Rows[i][j+1] = int.Parse (tmp.Tables[0].Rows[0][0].ToString());
                else if (CrewType==1)
                    Res.Rows[i][j+1] = int.Parse(tmp1.Tables[0].Rows[0][0].ToString()) ;
                else if (CrewType==2)
                    Res.Rows[i][j+1] = int.Parse(tmp2.Tables[0].Rows[0][0].ToString()) ;
                else
                    Res.Rows[i][j + 1] = int.Parse(tmp.Tables[0].Rows[0][0].ToString()) + int.Parse(tmp1.Tables[0].Rows[0][0].ToString()) + int.Parse(tmp2.Tables[0].Rows[0][0].ToString());

                SumRow = SumRow + int.Parse(Res.Rows[i][j + 1].ToString());
            }
            Res.Rows[i][cols.Rows.Count+1] = SumRow.ToString();
        }

        Res.Rows.Add(Res.NewRow());
        Res.Rows[Res.Rows.Count-1][0] = "TOTAL";
        for (int j = 1; j <= Res.Columns.Count - 1; j++)
        {
            Res.Rows[Res.Rows.Count - 1][j] = Res.Compute("SUM([" + Res.Columns[j].ColumnName + "])", "").ToString();
        }
        ExportDatatable(Response, Res, "Manpowerreport"); 
    }
    public static void ExportDatatable(HttpResponse Response, DataTable dt, String FileName)
    {
        Response.Clear();
        Response.AddHeader("content-disposition", "attachment;filename=" + FileName + ".xls");
        Response.Charset = "";
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.xls";
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
        System.Web.UI.WebControls.DataGrid dg = new System.Web.UI.WebControls.DataGrid();
        dg.HeaderStyle.Font.Bold = true;
        dg.DataSource = dt;
        dg.DataBind();
        dg.RenderControl(htmlWrite);
        Response.Write(stringWrite.ToString());
        Response.End();
    }
}
