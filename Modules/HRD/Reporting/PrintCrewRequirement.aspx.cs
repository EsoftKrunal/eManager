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
using System.Collections.Generic;  

public partial class Reporting_PrintCrewRequirement : System.Web.UI.Page
{

    public int[] MonthSum = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,0 };
    private bool ColumnEqual(object A, object B)
    {

        if (A == DBNull.Value && B == DBNull.Value)
            return true;
        if (A == DBNull.Value || B == DBNull.Value)
            return false;
        return (A.Equals(B));

    }
    public List<string> SelectDistinct(DataTable SourceTable, string FieldName)
    {
        List<string> dt = new List<string>();
        foreach(DataRow dr in SourceTable.Rows)
        {
            if (!dt.Contains(dr[FieldName].ToString()))
            {   
                dt.Add(dr[FieldName].ToString());  
            }
        }
        return dt;
    }
    public DataTable getVesselData(string Vsl)
    {
        DataTable dt = (DataTable)Session["dtRes"];
        DataView dv = dt.DefaultView;
        dv.RowFilter = "VesselName='"  + Vsl+  "'"; ;
        DataTable dtb = dv.ToTable();
        dtb.Columns.RemoveAt(0);   
        return dtb;
        //Sum = Sum + dtb.Rows.Count;
        //MonthSum[i - 1] = MonthSum[i - 1] + dtb.Rows.Count;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        DataTable dt = (DataTable)Session["dtRes"];
        if((""+Request.QueryString["Mode"]) == "R") // By Rank
        {
            dv_Rank.Visible = true;  
            DataTable dtOutput = new DataTable();
            dtOutput.Columns.Add("Sno");
            dtOutput.Columns.Add("Rank");
            dtOutput.Columns.Add("Mon1");
            dtOutput.Columns.Add("Mon2");
            dtOutput.Columns.Add("Mon3");
            dtOutput.Columns.Add("Mon4");
            dtOutput.Columns.Add("Mon5");
            dtOutput.Columns.Add("Mon6");
            dtOutput.Columns.Add("Mon7");
            dtOutput.Columns.Add("Mon8");
            dtOutput.Columns.Add("Mon9");
            dtOutput.Columns.Add("Mon10");
            dtOutput.Columns.Add("Mon11");
            dtOutput.Columns.Add("Mon12");
            dtOutput.Columns.Add("Total");
            List<string> dtRank = SelectDistinct(dt, "RankName");
            IEnumerator rns=dtRank.GetEnumerator();
            while(rns.MoveNext())
            {
                int Sum = 0;
                dtOutput.Rows.Add(dtOutput.NewRow());
                dtOutput.Rows[dtOutput.Rows.Count-1]["Sno"] = dtOutput.Rows.Count.ToString();
                dtOutput.Rows[dtOutput.Rows.Count-1]["Rank"] = rns.Current.ToString();

                for (int i = 1; i <= 12; i++)
                {
                    DataView dv = dt.DefaultView;
                    dv.RowFilter = "RankName='" + rns.Current.ToString() + "' And ( Mon" + i.ToString() + "='Coral' OR Mon" + i.ToString() + "='Green' OR Mon" + i.ToString() + "='cornflowerblue' )"; ;
                    DataTable dtb = dv.ToTable();
                    dtOutput.Rows[dtOutput.Rows.Count - 1]["Mon" + i.ToString()] = dtb.Rows.Count.ToString();
                    Sum = Sum + dtb.Rows.Count;
                    MonthSum[i - 1] = MonthSum[i - 1] + dtb.Rows.Count;
                }
                dtOutput.Rows[dtOutput.Rows.Count - 1]["Total"] = Sum.ToString();
                MonthSum[12] = MonthSum[12] + Sum;
            }
            rptByRank.DataSource = dtOutput;
            rptByRank.DataBind(); 
        }
        if (("" + Request.QueryString["Mode"]) == "V") // By Vessel
        {
            dv_Vessel.Visible = true; 
            rptVessels.DataSource= SelectDistinct(dt, "VesselName");
            rptVessels.DataBind();  
        }

    }
    protected void ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataTable dt = (DataTable)Session["dtRes"];
        Label l=(Label)e.Item.FindControl("lblVSL");
        Repeater r = (Repeater)e.Item.FindControl("rptByVessel");
        r.DataSource = getVesselData(l.Text);
        MonthSum[0] = MonthSum[0] + 1; 
        r.DataBind();
        for (int i=1; i <= 12; i++)
        {
            DataView dv = dt.DefaultView;
            dv.RowFilter = "VesselName='" + l.Text + "' And ( Mon" + i.ToString() + "='Coral' OR Mon" + i.ToString() + "='Green' OR Mon" + i.ToString() + "='cornflowerblue' )"; ;
            DataTable dtb = dv.ToTable();
            Label l1 = (Label)e.Item.FindControl("lblSum" + i.ToString());
            l1.Text = dtb.Rows.Count.ToString();    
        }
    }
    public string getPlanned(string _Relievers)
    {
        bool planned = false;
        if (int.Parse(_Relievers) > 0)
        {
            planned = true;
        }
        if (planned)
            return "style='background-color:Yellow'";
        else
            return "style='background-color:Auto'";
    }
}