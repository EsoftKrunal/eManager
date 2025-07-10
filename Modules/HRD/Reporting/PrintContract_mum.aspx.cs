using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class PrintContract_mum : System.Web.UI.Page
{
    public int contactid
    {
        get { return Common.CastAsInt32(ViewState["contractid"]); }
        set { ViewState["contractid"] = value; }
    }
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        contactid = Common.CastAsInt32(Request.QueryString["contactid"]);
        printcontract();
    }
    public void printcontract()
    {
       
        Dictionary<string,string> parameters = new Dictionary<string, string>();
        string sql = "select cch.ContractReferenceNumber,cch.wagescaleid,cch.crewid,vesselname,FlagStateName,WageScaleName,crewnumber,lastname,middlename,firstname, Address1 + ' ' + Address2 + ' ' + Address3 + ', ' + city + ', ' + state + ', ' + c1.countryname + ' - ' + pincode   as fulladdress " +
                    ", c.countryname as Nationality,dateofbirth,placeofbirth,Rankcode,(SELECT TOP 1 PORTNAME FROM DBO.PORT WHERE PORTID IN ( select signonport from DBO.crewonvesselhistory where contractid=cch.contractid and SignOnSignOff='I' )) as portjoining,localairportid as repat_airport, " +
                    "convert(varchar, datediff(month, cch.startdate, cch.EndDate)) + ' months' as duration,cch.startdate as contract_start_date,cch.NewRankId,r.offcrew,cch.OtherAmount " +
                    "from DBO.crewcontractheader cch " +
                    "inner join DBO.CrewPersonalDetails cpd on cch.CrewId = cpd.CrewId " +
                    "inner join DBO.vessel v on v.vesselid = cch.VesselId " +
                    "inner join DBO.rank r on r.rankid = cch.NewRankId " +
                    "left join DBO.FlagState fs on v.FlagStateId = fs.FlagStateId " +
                    "inner join DBO.wagescale w on w.wagescaleid = cch.WageScaleId " +
                    "left join DBO.CrewContactDetails ccd on ccd.crewid = cch.crewid and ccd.AddressType = 'P'  " +
                    "left join DBO.Country c1 on c1.CountryId = ccd.CountryId " +
                    "left join DBO.Country c on c.CountryId = cpd.NationalityId " +
                    "where cch.contractid = " + contactid;
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
        if (dt.Rows.Count > 0)
        {
            DataRow dr = dt.Rows[0];
            string crewtype = "";
            int crewid = Common.CastAsInt32(dr["crewid"]);
            int wagescaleid = Common.CastAsInt32(dr["wagescaleid"]);
            int NewRankId = Common.CastAsInt32(dr["NewRankId"]);
            string off_rat = dr["offcrew"].ToString();
            string contract_filename = "contract_rat.rpt";
            if (NewRankId==1 || NewRankId == 2 || NewRankId == 12 || NewRankId == 15)
            {
                contract_filename = "contract_obmt.rpt";
                crewtype = "OBMT";
            }
            else if(off_rat=="O")
            {
                contract_filename = "contract_jroff.rpt";
                crewtype = "Junior Officers";
            }
            else
            {
                contract_filename = "contract_rat.rpt";
                crewtype = "Ratings";
            }
            bool[] alowedheads = { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };
            DataTable dt111 = Common.Execute_Procedures_Select_ByQueryCMS("select wagescalecomponentid from wagescale_components_mapping where wagescaleid=" + wagescaleid);
            foreach(DataRow dr00 in dt111.Rows)
            {
                alowedheads[Common.CastAsInt32(dr00["wagescalecomponentid"]) - 1] = true;
            }
            
            parameters.Add("vessel_flag_cba", "<b><u>" + dr["vesselname"].ToString().ToUpper() + "</u></b> under the <b><u>" + dr["FlagStateName"].ToString().ToUpper() + "</u></b> flag & <b><u>" + dr["WageScaleName"].ToString().ToUpper() + "</u></b> CBA.");
            parameters.Add("surname", dr["lastname"].ToString());
            parameters.Add("contractno","Contract # :" + dr["ContractReferenceNumber"].ToString());
            parameters.Add("crewnum", "( Crew # : " + dr["crewnumber"].ToString() +  " ) ");
            parameters.Add("middlename", dr["middlename"].ToString());
            parameters.Add("firstname", dr["firstname"].ToString());
            parameters.Add("fulladdress", dr["fulladdress"].ToString());
            parameters.Add("nationality", dr["Nationality"].ToString());
            parameters.Add("date_place_birth", Common.ToDateString(dr["dateofbirth"]) + " ( " + dr["placeofbirth"].ToString() + " )");

            parameters.Add("rank_onboard", dr["Rankcode"].ToString());
            parameters.Add("port_joining", dr["portjoining"].ToString());
            parameters.Add("repat_airport", dr["repat_airport"].ToString());
            parameters.Add("contract_start_date", Common.ToDateString(dr["contract_start_date"]));
            parameters.Add("employment_period", dr["duration"].ToString() + " (+/-1 month)");
            parameters.Add("cbaname_crewcat", "("+ dr["WageScaleName"].ToString().ToUpper() + " ) ( " + crewtype.ToUpper() + " )");

            int total = 0;
            DataTable dt11 = Common.Execute_Procedures_Select_ByQuery("select w.WageScaleComponentId, w.ComponentName, w.ComponentType,convert(numeric(10,2),Amount) as Amount from DBO.wagescalecomponents w left join DBO.CrewContractDetails ccd on ccd.WagwScaleComponentId = w.WageScaleComponentId and w. and ccd.ContractId=" + contactid);
            for (int i = 1; i <= dt11.Rows.Count; i++)
            {
                string name = dt11.Rows[i - 1]["ComponentName"].ToString();
                string value = getComponentvalue(dt11, i);
                if (alowedheads[i - 1])
                {
                    parameters.Add("c" + i.ToString(), formatcurr(value));
                    parameters.Add("c" + i.ToString() + "_h", name);                   
                }
                else
                {
                    parameters.Add("c" + i.ToString(), "");
                    parameters.Add("c" + i.ToString() + "_h", "");                 
                }
                if (i != 12)
                {
                    total += Common.CastAsInt32(value);
                }

            }
            parameters.Add("total_wages", formatcurr(total));            
            int otheramount = Common.CastAsInt32(dr["OtherAmount"].ToString());
            parameters.Add("otheramount", formatcurr(otheramount));
            parameters.Add("grand_total", formatcurr(total + otheramount));

            DataTable dt1 = Common.Execute_Procedures_Select_ByQuery("select documentnumber from DBO.crewtraveldocument where documenttypeid = 0 and crewid = " + crewid + " order by expirydate desc");
            if(dt1.Rows.Count>0)
                parameters.Add("passport",dt1.Rows[0]["documentnumber"].ToString());
            else
                parameters.Add("passport","");

            DataTable dt2 = Common.Execute_Procedures_Select_ByQuery("select documentnumber from DBO.crewtraveldocument where documenttypeid = 2 and crewid = " + crewid + " order by expirydate desc");
            if (dt2.Rows.Count > 0)
                parameters.Add("seaman_book_no", dt2.Rows[0]["documentnumber"].ToString());
            else
                parameters.Add("seaman_book_no", "");
            
            rpt.Load(Server.MapPath(contract_filename));

            this.CrystalReportViewer1.Visible = true;
            CrystalReportViewer1.ReportSource = rpt;

            foreach(KeyValuePair<string, string> d in parameters)
            {
                rpt.SetParameterValue(d.Key, d.Value );
            }

            //rpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, "d:\\aa.pdf");
            rpt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "");
        }        
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
    public string getComponentvalue(DataTable dt,int componentid)
    {
        string ret = "";
        DataRow[] drs = dt.Select("WageScaleComponentId=" + componentid);
        if (drs.Length > 0)
            ret = string.Format(drs[0]["Amount"].ToString(),"{0:0.00}");
        return ret;             
    }
    string formatcurr(object input)
    {
        return string.Format("{0:0.00}",Common.CastAsDecimal(input));

    }
}