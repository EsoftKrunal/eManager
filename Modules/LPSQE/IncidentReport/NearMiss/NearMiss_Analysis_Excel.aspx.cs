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
using System.Text;
using System.IO;

public partial class ER_S133_Analysis_Excel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        BindGrid();
        ExportDatatable();
    }
    private void BindGrid()
    {
        string HumanJobFactors = "select OptionID,OptionText from [dbo].[ER_RegisterOptions] where RegisterId in (15,16) ORDER BY OptionText";
        DataTable dtHumanJobFactors = Budget.getTable(HumanJobFactors).Tables[0];

        string SQl = "Select Year(reportdate) as ReportYear,R.vesselcode,datename(mm,DateOfOccurence) as NMonth,R.EventDescr,O.NMSeverity,O.RC_HUMANFACTORS,O.RC_HUMANFACTORS_OTHER,O.RC_JOBFACTORS,O.RC_JOBFACTORS_OTHER," + 
            "replace(replace(replace(category,153,'Injury'),154,'Pollution'),155,'Property Damage') as Categories " +
            " from [dbo].[ER_S133_Report] R INNER JOIN [DBO].[ER_S133_Report_Office] O ON O.ReportId = R.ReportId AND O.VesselCode = R.VesselCode ";
        string WC = "";

        string VesselCode = Request.QueryString["VesselCode"].Trim();
        string NMType = Request.QueryString["NMType"].Trim();
        string AccCat = Request.QueryString["AccCat"].Trim();

        string Fdt = Request.QueryString["Fdt"].Trim();
        string Tdt = Request.QueryString["Tdt"].Trim();

        if (VesselCode != "")
            WC += "# R.VesselCode='" + VesselCode + "'";

        if (NMType != "")
            WC += "# O.NMSeverity='" + NMType + "'";

        if (AccCat!="")
        {
            WC += "# R.CATEGORY + ',' LIKE '%" + NMType + "%'";
        }
        if (Fdt.Trim() != "")
        {
            WC += "# R.reportdate >='" + Fdt.Trim() + "'";
        }
        if (Tdt.Trim() != "")
        {
            //WC += "# R.reportdate <= '" + Convert.ToDateTime(Tdt).AddDays(1).ToString("dd-MMM-yyyy").Trim() + "'";
            WC += "# R.reportdate <= '" + Tdt.Trim() + "'";
        }
        if (WC.Contains("#"))
        {
            if (WC.StartsWith("#"))
                WC = WC.Substring(1);

            WC = WC.Replace("#", "And");
            WC = " Where " + WC;
        }

        DataTable DT = Budget.getTable(SQl + WC).Tables[0];

        StringBuilder sb = new StringBuilder();
        sb.Append("<table border='1' cellspacing='0' cellpadding='0'>");
        sb.Append("<tr style='background-color:#3385FF;color:white;'>");
        sb.Append("<td>Report Year</td>");
        sb.Append("<td>Vessel</td>");
        sb.Append("<td>NM Month</td>");
        sb.Append("<td>Category</td>");
        sb.Append("<td>Event Descr.</td>");
        for (int j = 0; j <= dtHumanJobFactors.Rows.Count - 1; j++)
        {
            sb.Append("<td>" + dtHumanJobFactors.Rows[j]["OptionText"].ToString() + "</td>");
        }
        sb.Append("</tr>");
        for (int i = 0; i <= DT.Rows.Count-1; i++)
        {
            string RootCause1 = DT.Rows[i]["RC_HUMANFACTORS"].ToString();
            string RootCause1Other = DT.Rows[i]["RC_HUMANFACTORS_OTHER"].ToString();
            string RootCause2 = DT.Rows[i]["RC_JOBFACTORS"].ToString();
            string RootCause2Other = DT.Rows[i]["RC_JOBFACTORS_OTHER"].ToString();

            sb.Append("<tr>"); 
            // ------------------- Main Table ----------------
            sb.Append("<td>" + DT.Rows[i]["ReportYear"].ToString() + "</td>");
            sb.Append("<td>" + DT.Rows[i]["vesselcode"].ToString() + "</td>");
            sb.Append("<td>" + DT.Rows[i]["NMonth"].ToString() + "</td>");
            sb.Append("<td>" + DT.Rows[i]["Categories"].ToString() + "</td>");
            sb.Append("<td>" + DT.Rows[i]["EventDescr"].ToString() + "</td>");

            for (int j = 0; j <= dtHumanJobFactors.Rows.Count-1; j++)
            {
                string Optionid = dtHumanJobFactors.Rows[j]["OptionId"].ToString();
                if (("," + RootCause1 + ",").Contains("," + Optionid.ToString() + ",") || ("," + RootCause2 + ",").Contains("," + Optionid.ToString() + ","))
                {
                    sb.Append("<td style='text-align:center'>X</td>");
                }
                else
                {
                    sb.Append("<td></td>");
                }
            }
            sb.Append("</tr>");
        }
        sb.Append("</table>");
        ltr.Text = sb.ToString();
    }
    public void ExportDatatable()
    {
        Response.Clear();
        Response.AddHeader("content-disposition", "attachment;filename=" + "NMAnalysis" + ".xls");
        Response.ContentType = "application/vnd.xls";

    }
}
