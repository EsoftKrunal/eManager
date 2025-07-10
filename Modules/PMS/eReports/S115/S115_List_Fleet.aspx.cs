using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class eReports_S115_S115_List_Fleet : System.Web.UI.Page
{
    string FormNo = "S115";
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["MHSS"] = 1;
        if (!IsPostBack)
        {
            txtFd.Text = DateTime.Today.ToString("01-JAN-yyyy");
            txtTD.Text = DateTime.Today.ToString("dd-MMM-yyyy");
            BindFormDetails();
            BindList();
        }
    }
    public void BindFormDetails()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM [DBO].[ER_Master] WHERE [FormNo]='" + FormNo + "'");
        //lblReportName.Text = dt.Rows[0]["FormName"].ToString()+" [ "+ dt.Rows[0]["VersionNo"].ToString() +" ]";
        lblReportName.Text = "Fleet Accident Report [ " + dt.Rows[0]["VersionNo"].ToString() + " ]";
        

    }
    protected void BindList()
    {
        string sqL = "SELECT ROW_NUMBER() OVER(Order By REPORTDATE DESC,R.ReportId DESC) AS SrNo,R.VesselCode,(CASE WHEN ISNULL(IS_CLOSED,'N')='Y' THEN 'N' WHEN ISNULL(LOCKED,'N')='Y' THEN 'N' ELSE 'Y' END) as Edit_ALLOWED,R.[ReportId],[ReportNo],AccidentSeverity=(CASE WHEN AccidentSeverity=1 THEN 'Minor' WHEN AccidentSeverity=2 THEN 'Major' WHEN AccidentSeverity=3 THEN 'Severe' ELSE '' END) ,PORT,INCIDENTDATE,REPORTDATE,R.CreatedBy, R.CreatedOn,(CASE WHEN ISNULL(O.IS_CLOSED,'N')='N' THEN 'Open' ELSE 'Closed' END) as Status,(select count(1) from [DBO].[ER_S115_Report_RCA] where ReportId=R.ReportId and VesselCode=R.VesselCode )as RcaRecordCount  FROM [DBO].[ER_" + FormNo + "_Report] R " +
                     "LEFT JOIN [DBO].[ER_S115_Report_Office] O ON O.ReportId = R.ReportId AND O.VesselCode = R.VesselCode " +
                     "WHERE ISNULL(O.Status,'A') = 'A' and R.VesselCode<>'"+ Session["CurrentShip"].ToString() + "'";
        if (txtFd.Text.Trim() != "")
        {
            sqL += " AND INCIDENTDATE >='" + txtFd.Text.Trim() + "'";
        }
        if (txtTD.Text.Trim() != "")
        {
            sqL += " AND INCIDENTDATE <='" + txtTD.Text.Trim() + "'";
        }
        //if (txtPort.Text.Trim() != "")
        //{
        //    sqL += " AND Port  LIKE '" + txtPort.Text.Trim() + "%'";
        //}
        //if (RadioButton1.Checked)
        //{
        //    sqL += " AND AccidentSeverity=" + 1;
        //}
        //if (RadioButton2.Checked)
        //{
        //    sqL += " AND AccidentSeverity=" + 2;
        //}
        //if (RadioButton3.Checked)
        //{
        //    sqL += " AND AccidentSeverity=" + 3;
        //}

        string selectedValues = "";
        Int16 checkedCount = 0;
        foreach (ListItem li in cblSeverity.Items)
        {
            //if (li.Selected && li.Value == "0")
            //{
            //    break;
            //}
            //else
            //{
            if (li.Selected)
            {
                selectedValues = selectedValues + li.Value + ",";
                checkedCount++;
            }
            //}
        }
        if (checkedCount == cblSeverity.Items.Count)
            selectedValues = "";
        if (selectedValues.Trim() != "")
        {
            selectedValues = selectedValues.TrimEnd(',');
        }

        if (selectedValues.Trim() != "")
        {
            sqL += " AND AccidentSeverity IN (" + selectedValues + ") ";
        }

        if (ddlStatus.SelectedValue.Trim() != "0")
        {
            sqL += " AND ISNULL(O.Is_Closed,'N') = '" + ddlStatus.SelectedValue.Trim() + "' ";
        }


        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sqL + " ORDER BY REPORTDATE DESC");
        if (dt != null)
        {
            rptReports.DataSource = dt;
            rptReports.DataBind();
        }
    }
    protected void lnkReport_OnClick(object sender, EventArgs e)
    {

    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        BindList();
    }
    protected void btnDownloadRCA_OnClick(object sender, EventArgs e)
    {
        int ReportID = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        string VesselCode = Session["CurrentShip"].ToString();
        string ReportNo = "";
        DataTable dt1 = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM [DBO].[ER_S115_Report] WHERE VESSELCODE='" + VesselCode + "' AND [ReportId] = " + ReportID);
        if (dt1.Rows.Count > 0)
        {
            ReportNo = dt1.Rows[0]["ReportNo"].ToString();
        }
        string filename = "RCA_" + ReportNo.Replace("/", "-") + ".pdf";

        string sql = " select RcaDocument from dbo.ER_S115_Report_RCA where ReportID=" + ReportID + " and VesselCode='" + VesselCode + "' ";
        DataTable dtfile = Common.Execute_Procedures_Select_ByQuery(sql);
        if (dtfile.Rows.Count > 0)
        {
            byte[] fileBytes = (byte[])dtfile.Rows[0][0];
            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Type", "application/pdf");
            Response.AddHeader("Content-Disposition", "attachment;filename=" + filename);
            Response.BinaryWrite(fileBytes);
            Response.Flush();
            Response.End();
        }
    }

}