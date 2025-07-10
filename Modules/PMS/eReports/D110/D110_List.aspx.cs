using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class eReports_D110_D110_List : System.Web.UI.Page
{
    string FormNo = "D110";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtFd.Text = DateTime.Today.ToString("01-JAN-yyyy");
            txtTD.Text = DateTime.Today.ToString("dd-MMM-yyyy");
            BindFormDetails();
            BindRegisters();
            BindList();
        }
    }
    public void BindFormDetails()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM [DBO].[ER_Master] WHERE [FormNo]='" + FormNo + "'");
        if (dt != null && dt.Rows.Count > 0)
        {
            lblReportName.Text = dt.Rows[0]["FormName"].ToString();
        }

    }
    private void BindRegisters()
    {
        ProjectCommon.LoadRegisters(cblCategory, FormNo, 20);
    }
    protected void BindList()
    {
        string sqL = "SELECT ROW_NUMBER() OVER(Order By REPORTDATE DESC,R.ReportId DESC) AS SrNo,(CASE WHEN ISNULL(IS_CLOSED,'N')='Y' THEN 'N' WHEN ISNULL(LOCKED,'N')='Y' THEN 'N' ELSE 'Y' END) as Edit_ALLOWED,R.[ReportId],[ReportNo], " +
                     "[dbo].[ER_S115_getOptionTextCSV]('" + FormNo + "',20,[Category], ',') As Category, DateOfOccurence,REPORTDATE,R.CreatedBy, R.CreatedOn,(CASE WHEN ISNULL(O.IS_CLOSED,'N')='N' THEN 'Open' ELSE 'Closed' END) as Status FROM [DBO].[ER_" + FormNo + "_Report] R " +
                     "LEFT JOIN [DBO].[ER_S133_Report_Office] O ON O.ReportId = R.ReportId AND O.VesselCode = R.VesselCode " +
                     "WHERE ISNULL(O.Status,'A') = 'A'";
        sqL = " select ROW_NUMBER() OVER(Order By R.ReportId DESC) AS SrNo,*  " +
              "  from ER_D110_Report R ";
              

        //if (txtFd.Text.Trim() != "")
        //{
        //    sqL += " AND DateOfOccurence >='" + txtFd.Text.Trim() + "'";
        //}
        //if (txtTD.Text.Trim() != "")
        //{
        //    sqL += " AND DateOfOccurence <='" + txtTD.Text.Trim() + "'";
        //}
        //string InternalClause = ""; 
        //foreach (string cat in getCheckedItemstoArray(cblCategory))
        //{
        //    if (cat.Trim() != "")
        //    {
        //        InternalClause += ((InternalClause.Trim()=="")?"":" OR ") + " ( ',' + Category + ',' Like '%" + cat.Trim() + "%' ) ";
        //    }
        //}
        //if(InternalClause.Trim()!="")
        //    sqL += " AND ( " + InternalClause + " )";

    //if (ddlStatus.SelectedValue.Trim() != "0")
    //{
    //    sqL += " AND ISNULL(O.Is_Closed,'N') = '" + ddlStatus.SelectedValue.Trim() + "' ";
    //}
    sqL = sqL + " ORDER BY R.ReportId DESC ";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sqL);
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

    private string[] getCheckedItemstoArray(CheckBoxList chk)
    {
        List<string> arrList=new List<string>();
        foreach (ListItem li in chk.Items)
        {
            if (li.Selected)
            {
                arrList.Add(li.Value);
            }
        }

        return arrList.ToArray();
    }
}