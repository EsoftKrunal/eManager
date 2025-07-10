using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class eReports_G113_List : System.Web.UI.Page
{
    string FormNo = "G113";
    
    protected void Page_Load(object sender, EventArgs e)
    {

        

        
        if (!IsPostBack)
        {
            txtFd.Text = DateTime.Today.ToString("01-JAN-yyyy");
            txtTD.Text = DateTime.Today.ToString("dd-MMM-yyyy");
            BindFormDetails();
            BindList();
        }
    }
    protected void BindList()
    {
        string sqL = " select Row_number()over(order by AssMgntID)as SrNo,AppraisalFromDate,AppraisalToDate,case when Occasion=101 then 'EOC' when Occasion=102 then 'ON DEMAND' when Occasion=103 then 'INTERIM' else '' end as occasion , Assmgntid,CrewNo,AssName+' '+Asslname as Name,rank, (case when isnull(ExportedBy,'')='' then 'block' else 'none' end )img_view  from ER_G113_Report Ass where  1=1 ";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sqL);
        if (dt != null)
        {
            rptReports.DataSource = dt;
            rptReports.DataBind();
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
    protected void btnAddNewReport_OnClick(object sender, EventArgs e)
    {
        BindCrewDetails();
        dv_CrewList.Visible = true;
    }
    protected void btnGoToAppraisal_Click(object sender, EventArgs e)
    {
        if (hfSelectedCrewNumber.Value == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ddd", "alert('Please select any crew.');", true);
            return;
        }
        string CrewNumber = hfSelectedCrewNumber.Value;
        hfSelectedCrewNumber.Value = "";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "dd", "window.open('ER_G113_Report.aspx?CrewNumber=" + CrewNumber + "')", true);
    }

    protected void btn_Close_Click(object sender, EventArgs e)
    {
        dv_CrewList.Visible = false;
    }
    public void BindCrewDetails()
    {
        string sqL = " select * from PMS_CREW_HISTORY CH inner join MP_AllRank R on r.rankid=CH.rankid " +
                     " where(SignOffDate is null or SignOffDate >= getdate())  and r.RankId<>1 order by r.RankId";

        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sqL);
        rptCrewList.DataSource = dt;
        rptCrewList.DataBind();
    }
}