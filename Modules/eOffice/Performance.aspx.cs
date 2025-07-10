using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Text;

public partial class emtm_Emtm_Performance : System.Web.UI.Page
{
    public AuthenticationManager auth;
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        if (!IsPostBack)
        {
            int _USERID = 0;
            int _empid = Common.CastAsInt32(Request.QueryString["EmpId"]);
            DataTable dtuser = Common.Execute_Procedures_Select_ByQuery("SELECT USERID FROM DBO.Hr_PersonalDetails WHERE EMPID=" + _empid);
            if (dtuser.Rows.Count > 0)
            {
                _USERID = Common.CastAsInt32(dtuser.Rows[0][0]);
            }
            
            //ddlOffice.DataSource = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.Office order by officename");
            //ddlOffice.DataBind();
            //ddlOffice.Items.Insert(0,new ListItem(" < Select > ","0"));
            //BindDept();
            BindEmp();

            if (_USERID > 0)
                ddlEmp.SelectedValue = _USERID.ToString();
            else
                ddlEmp.SelectedValue = Session["loginid"].ToString();
                
            ShowData();
        }
    }
    public void BindDept()
    {
        //ddlDept.DataSource = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.EMTM_Department WHERE officeid=" + ddlOffice.SelectedValue);
        //ddlDept.DataBind();
        //ddlDept.Items.Insert(0, new ListItem(" < Select > ", "0"));
    }
    public void BindEmp()
    {
        object EMPID = Session["ProfileId"];
        if (EMPID == null )
            EMPID = "NULL";

        string SQL = "";
        if(ddlVPos.SelectedIndex>0)
            SQL = "SELECT USERID,FIRSTNAME + ' ' + MiddleName + ' ' + FamilyName AS EMPNAME FROM DBO.Hr_PersonalDetails WHERE DRC IS NULL AND USERID IN (SELECT " + ddlVPos.SelectedValue + " FROM DBO.VESSEL)  ORDER BY EMPNAME";
        else
            SQL = "SELECT USERID,FIRSTNAME + ' ' + MiddleName + ' ' + FamilyName AS EMPNAME FROM DBO.Hr_PersonalDetails WHERE DRC IS NULL ORDER BY EMPNAME";

        DataTable DT = Common.Execute_Procedures_Select_ByQuery(SQL);

        //DataTable DT = Common.Execute_Procedures_Select_ByQuery("EXEC DBO.GetChildsList " + EMPID.ToString());
        //DataView dv = DT.DefaultView;
        //if (ddlOffice.SelectedIndex > 0 && ddlDept.SelectedIndex > 0)
        //    dv.RowFilter = "OFFICEID=" + ddlOffice.SelectedValue + " and DEPIT=" + ddlDept.SelectedValue;
        //else if (ddlOffice.SelectedIndex > 0 && ddlDept.SelectedIndex <=0)
        //    dv.RowFilter = "OFFICEID=" + ddlOffice.SelectedValue;
        //else if (ddlOffice.SelectedIndex <= 0 && ddlDept.SelectedIndex > 0)
        //    dv.RowFilter = "DEPIT=" + ddlDept.SelectedValue;

        ddlEmp.DataSource = DT;
        ddlEmp.DataBind();
        ddlEmp.Items.Insert(0, new ListItem(" < Select > ", "0"));
    }
    protected void ddlOffice_OnSelectedIndexChanged(object sender,EventArgs e)
    {
        BindDept();
        BindEmp();
        ShowData();
    }
    protected void ddlDept_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        BindEmp();
        ShowData();
    }
    protected void ddlEmp_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        ShowData();
    }
    protected void lnkKpi_DetailsClick(object sender, EventArgs e)
    {
        int KPiId = Common.CastAsInt32(((LinkButton)sender).CommandArgument);
        int EmpId = Common.CastAsInt32(((LinkButton)sender).Attributes["EmpId"]);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "fsfa", "window.open('Performance_Details.aspx?KPIId=" + KPiId + "&EmpId=" + ddlEmp.SelectedValue + "&KPIYear=2015')", true);
    }
    public void ShowData()
    {
        if (ddlEmp.SelectedIndex > 0)
        {
            object EMPID = ddlEmp.SelectedValue;
            string SQL = "EXEC DBO.GET_KPI_PEROFRMANCE_BYCREW " + ddlEmp.SelectedValue + "," + DateTime.Today.Year.ToString() + ",0";
            DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(SQL);
            rptData.DataSource = dt;
            rptData.DataBind();
        }
        else
        {
            rptData.DataSource = null;
            rptData.DataBind();
        }

        
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "fassdfa", "window.open('../Reporting/Emtm_performance.aspx','');", true); 
    }
}