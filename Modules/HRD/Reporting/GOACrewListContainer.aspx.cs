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
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing;
using ShipSoft.CrewManager.Operational;

public partial class GOACrewListContainer : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        string fdt = Request.QueryString["fdt"], vessel = Request.QueryString["vessel"];  
        //========== Code to check report printing authority
        CrystalReportViewer1.HasPrintButton = Alerts.Check_ReportPrintingAuthority(Convert.ToInt32(Session["loginid"].ToString()),184);
        //----------
        DataTable dt1;
        if (fdt=="")
        {
            this.CrystalReportViewer1.Visible = false;
        }
        else
        {
            string whereclause = "1=1 and (gch.signondate='" + fdt + "' or gch.singoffdate='" + fdt + "' or (gch.signondate <'" + fdt + "' and gch.singoffdate >'" + fdt + "') or (gch.signondate <'" + fdt + "' and gch.singoffdate is null))";
            if (vessel != "0")
                whereclause = whereclause + " And gch.vesselid=" + vessel;
            dt1 = Budget.getTable("select ROW_NUMBER() over (order by ranklevel) AS SNo,Cpd.CrewNumber,FirstName + ' ' + Middlename + ' ' + LastName as FullName,RankCode,CountryName, " +
                                    "(select top 1 documentnumber from crewtraveldocument ct where ct.crewid=cpd.crewid and documenttypeid=0) as PassportNo, " +
                                    "(select top 1 documentnumber from crewtraveldocument ct where ct.crewid=cpd.crewid and documenttypeid=2) as CDCNo, " +
                                    "left(convert(varchar,dateofbirth,106),11) as DOB, " +
                                    "(select Address1 + ' '  + Address2 + ' ' + Address3 + ' ' + City + (case when ltrim(rtrim(pinCode))='' then '' else '-' + pinCode end) + ', ' + (select countryname from country where country.countryid=ccd.countryid) from crewContactdetails ccd where addresstype='C' and ccd.crewid=cpd.crewid) as Address, " +
                                    "(select TelephoneNumber from crewContactdetails ccd where addresstype='C' and ccd.crewid=cpd.crewid) as TelNo, " +
                                    "isnull((select top 1 FirstName + ' ' + LastName from crewfamilydetails cfd where cfd.crewid=cpd.crewid and isNOK='Y'),'') as NOK, " +
                                    "isnull((select top 1 case " +
                                    "when RelationshipId=1 then 'Father' " +
                                    "when RelationshipId=2 then 'Mother' " +
                                    "when RelationshipId=3 then 'Wife' " +
                                    "when RelationshipId=4 then 'Husband' " +
                                    "when RelationshipId=5 then 'Child' " +
                                    "when RelationshipId=6 then 'Brother' " +
                                    "when RelationshipId=7 then 'Sister' " +
                                    "when RelationshipId=435 then 'Aunty' " +
                                    "when RelationshipId=436 then 'BrotherInLaw' " +
                                    "when RelationshipId=440 then 'Cousin' " +
                                    "when RelationshipId=40 then 'Daughter' " +
                                    "when RelationshipId=441 then 'DaughterInLaw' " +
                                    "when RelationshipId=439 then 'FatherInLaw' " +
                                    "when RelationshipId=446 then 'Friend' " +
                                    "when RelationshipId=433 then 'GrandFather' " +
                                    "when RelationshipId=42 then 'GrandMother' " +
                                    "when RelationshipId=438 then 'MotherInLaw' " +
                                    "when RelationshipId=449 then 'Niece' " +
                                    "when RelationshipId=448 then 'Nephew' " +
                                    "when RelationshipId=384 then 'Self' " +
                                    "when RelationshipId=437 then 'SisterInLaw' " +
                                    "when RelationshipId=45 then 'Son' " +
                                    "when RelationshipId=434 then 'Uncle'  " +
                                    "else '-' end from crewfamilydetails cfd where cfd.crewid=cpd.crewid and isNOK='Y'),'') as Relation " +
                                    " from get_crew_history gch inner join crewpersonaldetails cpd on cpd.crewid=gch.crewid " +
                                    "inner join rank on rank.rankid=cpd.currentrankid " +
                                    "inner join country on country.countryid=cpd.nationalityid Where " + whereclause + " Order By ranklevel").Tables[0];
            if (dt1.Rows.Count > 0)
            {
                this.CrystalReportViewer1.Visible = true;
                DataTable dt2 = PrintCrewList.selectCompanyDetails();
                CrystalReportViewer1.ReportSource = rpt;
                rpt.Load(Server.MapPath("GOACrewList.rpt"));
                rpt.SetDataSource(dt1);
                foreach (DataRow dr in dt2.Rows)
                {
                    rpt.SetParameterValue("@Company", dr["CompanyName"].ToString());
                    rpt.SetParameterValue("@Email", "GOA Crew List");
                    rpt.SetParameterValue("@FmDt", Session["Vessel"].ToString() );
                    rpt.SetParameterValue("@TDt", Session["AsOn"].ToString());
                }
            }
            else
            {
                this.CrystalReportViewer1.Visible = false;
            }
        }
    }

    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
}
