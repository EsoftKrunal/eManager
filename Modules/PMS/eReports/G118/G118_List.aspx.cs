using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class eReports_G118_G118_List : System.Web.UI.Page
{
    string FormNo = "G118";
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["MHSS"] = 3;
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
        //ProjectCommon.LoadRegisters(cblCategory, FormNo, 20);
    }
    protected void BindList()
    {
        string sqL = "SELECT ROW_NUMBER() OVER(Order By NCRCreatedDate DESC,R.ReportId DESC) AS SrNo,(CASE WHEN ISNULL(IS_CLOSED,'N')='Y' THEN 'N' WHEN ISNULL(LOCKED,'N')='Y' THEN 'N' ELSE 'Y' END) as Edit_ALLOWED,R.[ReportId],[ReportNo], " +
                     "NCRCreatedDate, (SELECT [OptionText] FROM ER_RegisterOptions WHERE RegisterId = 21 AND OptionId = R.AreaAudited) AS AreaAudited,R.CreatedBy, R.CreatedOn,(CASE WHEN ISNULL(O.IS_CLOSED,'N')='N' THEN 'Open' ELSE 'Closed' END) as Status FROM [DBO].[ER_" + FormNo + "_Report] R " +
                     "LEFT JOIN [DBO].[ER_" + FormNo + "_Report_Office] O ON O.ReportId = R.ReportId AND O.VesselCode = R.VesselCode " +
                     "WHERE ISNULL(O.Status,'A') = 'A'";
        if (txtFd.Text.Trim() != "")
        {
            sqL += " AND NCRCreatedDate >='" + txtFd.Text.Trim() + "'";
        }
        if (txtTD.Text.Trim() != "")
        {
            sqL += " AND NCRCreatedDate <='" + txtTD.Text.Trim() + "'";
        }
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

        if (ddlStatus.SelectedValue.Trim() != "0")
        {
            sqL += " AND ISNULL(O.Is_Closed,'N') = '" + ddlStatus.SelectedValue.Trim() + "' ";
        }

        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sqL + " ORDER BY NCRCreatedDate DESC,R.ReportId DESC");
        if (dt != null)
        {
            rptReports.DataSource = dt;
            rptReports.DataBind();
        }
    }
    protected void lnkReport_OnClick(object sender, EventArgs e)
    {

    }
	protected void btnNew_Click(object sender, EventArgs e)
    {
		ScriptManager.RegisterStartupScript(this,this.GetType(),"abc","window.open('" + this.Page.ResolveClientUrl(@"~\eReports\G118\eReport_G118.aspx?Type=E") + "','');",true);
        
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