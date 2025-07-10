using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Configuration;

public partial class BudgetVarianceReportCrystal : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    string FID = "";
    string OwnerID = "";
    int Year, Month;
    protected void Page_Load(object sender, EventArgs e)
    {
        //---------------------------------------
        ProjectCommon.SessionCheck();
        //---------------------------------------
        if (Request.QueryString["FleetID"]!=null)
            FID = Request.QueryString["FleetID"].ToString();

        if (Request.QueryString["Owner"] != null)
            OwnerID = Request.QueryString["Owner"].ToString();

        Year = Common.CastAsInt32(Request.QueryString["Year"]);
        Month = Common.CastAsInt32(Request.QueryString["Month"]);

        if (Common.CastAsInt32(FID) > 0)
            Fir_Report(FID);

        if (Common.CastAsInt32(OwnerID) > 0)
            Fir_ReportOwnerWise(OwnerID);

    }
    public void Fir_Report(string FID)
    {
        // CHECK IF ALREADY EXISTS ------------
        
        try
        {
            DataTable Dt_Res = new DataTable();
            Dt_Res.Columns.Add("Vessel");
            Dt_Res.Columns.Add("Manning", typeof(float));
            Dt_Res.Columns.Add("Consumables", typeof(float));
            Dt_Res.Columns.Add("Lube Oils", typeof(float));
            Dt_Res.Columns.Add("Spares, Maintenance & Repair", typeof(float));
            Dt_Res.Columns.Add("General Expenses", typeof(float));
            Dt_Res.Columns.Add("Insurance", typeof(float));
            Dt_Res.Columns.Add("Management & Admin Fees", typeof(float));
            Dt_Res.Columns.Add("Variance", typeof(float));

            Dt_Res.Columns.Add("Manning_tot", typeof(float));
            Dt_Res.Columns.Add("Consumables_tot", typeof(float));
            Dt_Res.Columns.Add("Lube Oils_tot", typeof(float));
            Dt_Res.Columns.Add("Spares, Maintenance & Repair_tot", typeof(float));
            Dt_Res.Columns.Add("General Expenses_tot", typeof(float));
            Dt_Res.Columns.Add("Insurance_tot", typeof(float));
            Dt_Res.Columns.Add("Management & Admin Fees_tot", typeof(float));
            Dt_Res.Columns.Add("Var_tot", typeof(float));

            //-------------------------
            string Qry = "", InnerSql = "";
            Qry = "SELECT VESSELCODE,VESSELName FROM DBO.VESSEL WHERE FLEETID=" + FID.ToString() + " AND VesselStatusid<>2  ORDER BY VESSELCODE";
            DataTable ds_Ship = Common.Execute_Procedures_Select_ByQueryCMS(Qry);
                //-------------------------
                foreach (DataRow dr in ds_Ship.Rows)
                {
                    InnerSql = "EXEC DBO.getVariancePer " + Year.ToString() + "," + Month.ToString() + ",'" + dr["VESSELCODE"].ToString() + "'";
                    DataTable InnerDT  = Common.Execute_Procedures_Select_ByQuery(InnerSql);
                    Dt_Res.Rows.Add(Dt_Res.NewRow());   
                    for (int j = 0; j <= Dt_Res.Columns.Count - 1; j++)
                    {
                        Dt_Res.Rows[Dt_Res.Rows.Count - 1][j] = InnerDT.Rows[0][j];
                    }
                    //----------------
                }


            Dt_Res.Rows.Add(Dt_Res.NewRow());
            Dt_Res.Rows[Dt_Res.Rows.Count - 1][0] = "<b>Total : </b>";
            foreach (DataColumn dc in Dt_Res.Columns)
            {
                if (dc.ColumnName.Trim() != "Vessel")
                {
                    string colname = dc.ColumnName.Trim();

                    if (dc.ColumnName.EndsWith("_amt"))
                    {
                        string str = "SUM([" + colname + "])";
                        decimal amt = Common.CastAsDecimal(Dt_Res.Compute(str, ""));
                        Dt_Res.Rows[Dt_Res.Rows.Count - 1][dc] = String.Format("{0:0}", amt);
                    }
                    else if (dc.ColumnName.EndsWith("_tot"))
                    {

                    }
                    else
                    {
                        decimal amt = Common.CastAsDecimal(Dt_Res.Compute("SUM([" + colname + "_amt])", ""));
                        decimal totamt = 0;
                        try
                        {
                            totamt = Convert.ToDecimal(Dt_Res.Compute("SUM([" + colname + "_tot])", ""));
                        }
                        catch { }
                        if (totamt != 0)
                            Dt_Res.Rows[Dt_Res.Rows.Count - 1][dc] = String.Format("{0:0.00}", (amt * 100 / totamt));
                        else
                            Dt_Res.Rows[Dt_Res.Rows.Count - 1][dc] = 0;
                    }
                }
            }
            CrystalReportViewer1.ReportSource = rpt;
            rpt.Load(Server.MapPath("~/Report/BudgetVarianceReport.rpt"));
            rpt.SetDataSource(Dt_Res);
            rpt.SetParameterValue("Header", ProjectCommon.GetMonthName(Month.ToString()) + " - " + Year.ToString()); 
            
        }
        catch (System.Exception ex)
        {
           
        }

    }
    public void Fir_ReportOwnerWise(string OID)
    {
        // CHECK IF ALREADY EXISTS ------------

        try
        {
            
            DataTable Dt_Res = new DataTable();
            Dt_Res.Columns.Add("Vessel");
            Dt_Res.Columns.Add("Manning", typeof(float));
            Dt_Res.Columns.Add("Consumables", typeof(float));
            Dt_Res.Columns.Add("Lube Oils", typeof(float));
            Dt_Res.Columns.Add("Spares, Maintenance & Repair", typeof(float));
            Dt_Res.Columns.Add("General Expenses", typeof(float));
            Dt_Res.Columns.Add("Insurance", typeof(float));
            Dt_Res.Columns.Add("Management & Admin Fees", typeof(float));
            Dt_Res.Columns.Add("Variance", typeof(float));

            Dt_Res.Columns.Add("Manning_amt", typeof(float));
            Dt_Res.Columns.Add("Consumables_amt", typeof(float));
            Dt_Res.Columns.Add("Lube Oils_amt", typeof(float));            
            Dt_Res.Columns.Add("Spares, Maintenance & Repair_amt", typeof(float));            
            Dt_Res.Columns.Add("General Expenses_amt", typeof(float));            
            Dt_Res.Columns.Add("Insurance_amt", typeof(float));            
            Dt_Res.Columns.Add("Management & Admin Fees_amt", typeof(float));            
            Dt_Res.Columns.Add("Variance_amt", typeof(float));

            Dt_Res.Columns.Add("Manning_tot", typeof(float));
            Dt_Res.Columns.Add("Consumables_tot", typeof(float));
            Dt_Res.Columns.Add("Lube Oils_tot", typeof(float));
            Dt_Res.Columns.Add("Spares, Maintenance & Repair_tot", typeof(float));
            Dt_Res.Columns.Add("General Expenses_tot", typeof(float));
            Dt_Res.Columns.Add("Insurance_tot", typeof(float));
            Dt_Res.Columns.Add("Management & Admin Fees_tot", typeof(float));
            Dt_Res.Columns.Add("Variance_tot", typeof(float));

            //-------------------------
            string Qry = "", InnerSql = "";
            if (OID=="0")
                Qry = "SELECT VESSELCODE,VESSELName FROM DBO.VESSEL WHERE VesselStatusid<>2  ORDER BY VESSELCODE";
            else
                Qry = "SELECT VESSELCODE,VESSELName FROM DBO.VESSEL WHERE OwnerID =" + OID.ToString() + " AND VesselStatusid<>2  ORDER BY VESSELCODE";

            DataTable ds_Ship = Common.Execute_Procedures_Select_ByQueryCMS(Qry);
            //-------------------------
            foreach (DataRow dr in ds_Ship.Rows)
            {
                InnerSql = "EXEC DBO.getVariancePer " + Year.ToString() + "," + Month.ToString() + ",'" + dr["VESSELCODE"].ToString() + "'";
                DataTable InnerDT = Common.Execute_Procedures_Select_ByQuery(InnerSql);
                Dt_Res.Rows.Add(Dt_Res.NewRow());
                for (int j = 0; j <= Dt_Res.Columns.Count - 1; j++)
                {
                    Dt_Res.Rows[Dt_Res.Rows.Count - 1][j] = InnerDT.Rows[0][j];
                }
                //----------------
            }

            Dt_Res.Rows.Add(Dt_Res.NewRow());
            Dt_Res.Rows[Dt_Res.Rows.Count - 1][0] = "Total : ";
            foreach (DataColumn dc in Dt_Res.Columns)
            {
                if (dc.ColumnName.Trim() != "Vessel")
                {
                    string colname = dc.ColumnName.Trim();

                    if (dc.ColumnName.EndsWith("_amt"))
                    {
                        string str = "SUM([" + colname + "])";
                        decimal amt = Common.CastAsDecimal(Dt_Res.Compute(str, ""));
                        Dt_Res.Rows[Dt_Res.Rows.Count - 1][dc] = String.Format("{0:0}", amt);
                    }
                    else if (dc.ColumnName.EndsWith("_tot"))
                    {

                    }
                    else
                    {
                        decimal amt = Common.CastAsDecimal(Dt_Res.Compute("SUM([" + colname + "_amt])", ""));
                        decimal totamt = 0;
                        try
                        {
                            totamt = Convert.ToDecimal(Dt_Res.Compute("SUM([" + colname + "_tot])", ""));
                        }
                        catch { }
                        if (totamt != 0)
                            Dt_Res.Rows[Dt_Res.Rows.Count - 1][dc] = String.Format("{0:0.00}", (amt * 100 / totamt));
                        else
                            Dt_Res.Rows[Dt_Res.Rows.Count - 1][dc] = 0;
                    }
                }
            }


            CrystalReportViewer1.ReportSource = rpt;
            rpt.Load(Server.MapPath("~/Report/BudgetVarianceReport.rpt"));
            rpt.SetDataSource(Dt_Res);
            rpt.SetParameterValue("Header",ProjectCommon.GetMonthName(Month.ToString()) + " - " + Year.ToString()); 
        }
        catch (System.Exception ex)
        {

        }

    }
    protected void Page_UnLoad(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
}


