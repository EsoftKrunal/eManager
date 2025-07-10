using System;
using System.Data;
using System.Configuration;
using System.Reflection;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;

using System.Linq;

public partial class MRV_Report : System.Web.UI.Page
{
    public string VesselCode
    {
        get { return Convert.ToString(ViewState["_VesselCode"]); }
        set { ViewState["_VesselCode"] = value; }
    }
    public int Year
    {
        get { return Common.CastAsInt32(ViewState["_Year"]); }
        set { ViewState["_Year"] = value; }
    }

    public string EUVoyage
    {
        get { return Convert.ToString(ViewState["EUVoyage"]); }
        set { ViewState["EUVoyage"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        VesselCode = Convert.ToString(Page.Request.QueryString["V"]);
        Year = Common.CastAsInt32(Page.Request.QueryString["Year"]);
        EUVoyage = Convert.ToString(Page.Request.QueryString["EUVoyage"]);
        GenrateReport();
    }

    public void GenrateReport()
    {
        //DataTable DT01 = GetTable01();
        //DataTable DT02 = GetTable02();
        //DataTable DT03 = GetTable03();

        //string sql = " exec dbo.MRV_Report_Print '"+VesselCode+"' ";
        //Common.Set_Procedures("dbo.MRV_Report_Print");
        //Common.Set_ParameterLength(1);
        //Common.Set_Parameters(
        //    new MyParameter("@VesselCode", VesselCode));
        //DataSet Ds= Common.Execute_Procedures_Select();

        Common.Set_Procedures("DBO.MRV_Report_Print");
        Common.Set_ParameterLength(3);
        Common.Set_Parameters(new MyParameter("@VesselCode", VesselCode), new MyParameter("@Year", Year), new MyParameter("@EUVoyage", EUVoyage));
        DataSet Ds = new DataSet();
        Ds = Common.Execute_Procedures_Select();

        DataTable DT01= Ds.Tables[0];
        DataTable DT02 = Ds.Tables[1];
        //DataTable DT03 = Ds.Tables[2];

        StringBuilder htmlTable = new StringBuilder();
        int VoyageID = 0;
        int FuelTypeID = 0;

        string Mass = "";
        string Emmision = "";
        string Co2Emmision = "";

        


        htmlTable.Append("<table border='1' cellpadding='5' cellspacing='0' >");

        #region Header Row I
        htmlTable.Append("<tr class='headerrow'>");
        htmlTable.Append(" <td rowspan='3'> No.</ td>");
        htmlTable.Append(" <td rowspan='3'> Company Voyage No. </ td>");
        htmlTable.Append(" <td colspan='3' rowspan='2'> Commence</ td>");
        htmlTable.Append(" <td colspan='3' rowspan='2'> End</ td>");
        
        htmlTable.Append(" <td colspan='"+ DT02.Rows.Count*3 + "'> Total Fuel Consumption and  CO<sub>2</sub> Emissions</ td>");

        htmlTable.Append(" <td colspan='2' rowspan='2'> Distance & Time</ td>");
        htmlTable.Append(" <td colspan='3' rowspan='2'> Cargo & Transport Work</ td>");
        htmlTable.Append("</tr>");

        #endregion

        #region Header Row II
        htmlTable.Append("<tr class='headerrow'>");
        foreach (DataRow Dr02 in DT02.Rows)
        {
            htmlTable.Append(" <td colspan='3' > " + Dr02 [1].ToString()+ "</ td>");
        }
        

        htmlTable.Append("</tr>");

        #endregion

        #region Header Row   III       
        htmlTable.Append("<tr class='headerrow'>");        
        htmlTable.Append(" <td> Port</ td>");
        htmlTable.Append(" <td> Date</ td>");
        htmlTable.Append(" <td> Time (UTC)</ td>");
        htmlTable.Append(" <td> Port</ td>");
        htmlTable.Append(" <td> Date</ td>");
        htmlTable.Append(" <td> Time (UTC)</ td>");

        foreach (DataRow Dr02 in DT02.Rows)
        {
            htmlTable.Append(" <td> Mass (MT)</ td>");
            htmlTable.Append(" <td> Emmission Factor</ td>");
            htmlTable.Append(" <td> Co2 Emmission (MT)</ td>");
        }
        htmlTable.Append(" <td> Distance Travelled (NM)</ td>");
        htmlTable.Append(" <td> Time Spent At Sea (hours)</ td>");
        htmlTable.Append(" <td> Cargo (MT)</ td>");
        htmlTable.Append(" <td> Transport Work (T-NM)</ td>");
        htmlTable.Append("</tr>");

        #endregion

        foreach (DataRow Dr01 in DT01.Rows)
        {
            VoyageID = Common.CastAsInt32(Dr01["VoyageId"]);
            htmlTable.Append("<tr>");
            htmlTable.Append("<td>" + Dr01["Sr"].ToString() + "</td>");
            htmlTable.Append("<td>" + Dr01["VoyageNo"].ToString() + "</td>");
            htmlTable.Append("<td>" + Dr01["FromPort"].ToString() + "</td>");

            if (Convert.IsDBNull(Dr01["EndDate"]))
            {
                htmlTable.Append("<td></td>");
                htmlTable.Append("<td></td>");
            }
            else
            {
                htmlTable.Append("<td>" + Common.ToDateString(Dr01["StartDate"]) + "</td>");
                htmlTable.Append("<td>" + Convert.ToDateTime(Dr01["StartDate"]).ToString("hh:mm") + " Hrs</td>");
            }
                
            htmlTable.Append("<td>" + Dr01["ToPort"].ToString() + "</td>");
            if (Convert.IsDBNull(Dr01["EndDate"]))
            {
                htmlTable.Append("<td></td>");
                htmlTable.Append("<td></td>");
            }
            else
            {
                htmlTable.Append("<td>" + Common.ToDateString(Dr01["EndDate"]) + "</td>");                
                htmlTable.Append("<td>" + Convert.ToDateTime(Dr01["EndDate"]).ToString("hh:mm") + " Hrs</td>");
            }

            foreach (DataRow Dr02 in DT02.Rows)
            {
                FuelTypeID = Common.CastAsInt32(Dr02["FuelTypeId"]);

                //var ss= DT03.AsEnumerable().Where(x => x.Field<int>("VoyageId") == VoyageID && x.Field<int>("FuelTypeId") == FuelTypeID).FirstOrDefault()["mass"];
                //var ss = DT03.AsEnumerable().Where(x => x.Field<int>("VoyageId") == 1);
                //ss = DT03.AsEnumerable().Where(x => x.Field<int>("VoyageId") == VoyageID && x.Field<int>("FuelTypeId") == FuelTypeID);

                //DataRow[] Drs = DT03.Select("VoyageId=" + VoyageID + " and FuelTypeId=" + FuelTypeID);

                //Mass = "";
                //Co2Emmision = "";
                //if (Drs.Count() > 0)
                //{
                //    Mass = Drs[0]["mass"].ToString();
                //    Co2Emmision = Drs[0]["CO2EMISSION"].ToString();
                //}
                //Emmision = Dr02["Co2EmissionPerMT"].ToString();
                //htmlTable.Append("<td style='text-align:right;'>" + Mass + "</td>");
                //htmlTable.Append("<td  style='text-align:right;'>" + Emmision + "</td>");
                //htmlTable.Append("<td style='text-align:right;'>" + Co2Emmision + "</td>");



                Emmision = Dr02["Co2EmissionPerMT"].ToString();
                htmlTable.Append("<td style='text-align:right;'>" + Dr02["Mass"].ToString() + "</td>");
                htmlTable.Append("<td  style='text-align:right;'>" + Emmision  + "</td>");
                htmlTable.Append("<td style='text-align:right;'>" + Dr02["CO2EMISSION"].ToString() + "</td>");
            }

            int Totmin = Common.CastAsInt32(Dr01["TotalSeaHours"]);
            int Hour = Totmin / 60;
            int Min = Totmin % 60;

            htmlTable.Append("<td style='text-align:right;'>" + Dr01["TotalDistanceMadeGoods"].ToString() + "</td>");
            htmlTable.Append("<td>" + Hour+" Hrs "+Min +" Min" + "</td>");
            htmlTable.Append("<td style='text-align:right;'>" + Dr01["TotalCargoCaried"].ToString() + "</td>");
            htmlTable.Append("<td style='text-align:right;'>" + Dr01["TotalTransportWork"].ToString() + "</td>");
            htmlTable.Append("</tr>");
        }

        //#region Footer Row
        //    htmlTable.Append("<tr >");
        //    htmlTable.Append(" <td> </ td>");
        //    htmlTable.Append(" <td> </ td>");
        //    htmlTable.Append(" <td> </ td>");
        //    htmlTable.Append(" <td> </ td>");
        //    htmlTable.Append(" <td> </ td>");
        //    htmlTable.Append(" <td> </ td>");
        //    htmlTable.Append(" <td> </ td>");
        //    htmlTable.Append(" <td> </ td>");

        //    foreach (DataRow Dr02 in DT02.Rows)
        //    {
        //        FuelTypeID = Common.CastAsInt32(Dr02["FuelTypeId"]);                

        //        htmlTable.Append(" <td style='text-align:right;'> " + DT03.Compute("sum(mass)", "FuelTypeID="+ FuelTypeID ) + " </ td>");
        //        htmlTable.Append(" <td> </ td>");
        //        htmlTable.Append(" <td style='text-align:right;'> " + DT03.Compute("sum(CO2EMISSION)", "FuelTypeID=" + FuelTypeID) + " </ td>");
        //    }
        //    htmlTable.Append(" <td style='text-align:right;'> " + DT01.Compute("sum(TotalDistanceMadeGoods)", "")+" </ td>");

        //    int sTotmin = Common.CastAsInt32( DT01.Compute("sum(TotalSeaHours)", "") );
        //    int sHour = sTotmin / 60;
        //    int sMin = sTotmin % 60;

        //    htmlTable.Append("<td>" + sHour + " Hrs " + sMin + " Min" + "</td>");
        //    htmlTable.Append(" <td style='text-align:right;'> " + DT01.Compute("sum(TotalCargoCaried)", "") + " </ td>");
        //    htmlTable.Append(" <td style='text-align:right;'> " + DT01.Compute("sum(TotalTransportWork)", "") + " </ td>");

        //htmlTable.Append("</tr>");
        //#endregion
        htmlTable.Append("</table>");
        divContainer.InnerHtml = htmlTable.ToString();


    }

    public DataTable GetTable01()
    {
        string sql = " select ROW_NUMBER()over(order by VoyageId)Sr, VoyageId,VoyageNo,FromPort,StartDate,ToPort,EndDate,TotalDistanceMadeGoods,TotaSteamingHour,TotalCargoCaried,CO2 " +
            " from  dbo.MRV_VW_MRV_Voyage where VesselCode='" + VesselCode+"' ORDER BY VoyageId ";
        return Common.Execute_Procedures_Select_ByQuery(sql);
    }
    public DataTable GetTable02()
    {
        string sql = " SELECT * FROM  dbo.MRV_FuelTypes ";
        return Common.Execute_Procedures_Select_ByQuery(sql);
    }
    public DataTable GetTable03()
    {
        string sql = " SELECT VoyageId,FuelTypeId,sum(FUELCONSUMED) as Mass,SUM(CO2EMISSION) as CO2EMISSION FROM  dbo.MRV_VoyageActivity_Sources  " +
                     "   WHERE VESSELCODE = '"+VesselCode+"' group by VoyageId,FuelTypeId ";
        return Common.Execute_Procedures_Select_ByQuery(sql);
    }
}
