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

public partial class CriticalComponentShutdownRequest_VSL : System.Web.UI.Page
{
     
    public string VessCode
    {
        get { return Convert.ToString(ViewState["VessCode"]); }
        set { ViewState["VessCode"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        // -------------------------- SESSION CHECK ----------------------------------------------
        ProjectCommon.SessionCheck();
        // -------------------------- SESSION CHECK END ----------------------------------------------
        lblMessage.Text = "";
        lblSelCompnentMsg.Text = "";
        
        if (!Page.IsPostBack)
        {
             VessCode = Session["CurrentShip"].ToString();
            txtDt1.Text = DateTime.Today.ToString("01-Jan-yyyy");
            txtDt2.Text = DateTime.Today.ToString("dd-MMM-yyyy");
            ShowReport();
            BindCriticalComponent();
        }
    }

    protected void btnSearch_Click(object sender,EventArgs e)
    {
        ShowReport();
    }
    protected void btnShowAdd_Click(object sender, EventArgs e)
    {
        dvAddNew.Visible = true;
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        dvAddNew.Visible = false;
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        string ComponentCode = "";
        //ComponentCode = ListCriticalComponent.SelectedValue;
        if (hfSelectedComponent.Value == "")
        {
            lblSelCompnentMsg.Text = "Please select any component.";
            return;
        }
        ComponentCode = hfSelectedComponent.Value;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "saf", "window.open('VSL_CriticalEqpShutdownReq.aspx?CompCode=" + ComponentCode + "&VC=" + VessCode + "&SD=0');", true);
        hfSelectedComponent.Value = "";
        dvAddNew.Visible = false;
    }
    public string ToDateTimeString(object inp)
    {
        try
        {
            DateTime dt = Convert.ToDateTime(inp);
            if (dt.ToString("dd-MMM-yyyy") == "01-Jan-1900")
            { return ""; }
            else
            { return dt.ToString("dd-MMM-yyyy hh:mm tt"); }
        }
        catch
        {
            return "";
        }

    }
    protected void ShowReport()
    {
        string strSQL = "";
        string WhereClause = " where VesselCode='" + VessCode + "'";

        if (txtCompCode.Text.Trim() != "")
            WhereClause = WhereClause + " And ComponentCode like '" + txtCompCode.Text.Trim() + "%'";

        if (txtCompName.Text.Trim() != "")
            WhereClause = WhereClause + " And ComponentName like '" + txtCompName.Text.Trim() + "%'";

        if (txtDt1.Text.Trim() != "")
            WhereClause = WhereClause + " And requestdate >='" + txtDt1.Text.Trim() + "'";

        if (txtDt2.Text.Trim() != "")
            WhereClause = WhereClause + " And requestdate <='" + txtDt2.Text.Trim() + "'";

        if (ddlAppStatus.SelectedValue=="A")
            WhereClause = WhereClause + " And ApprovedOn IS not NULL ";

        if (ddlAppStatus.SelectedValue == "P")
            WhereClause = WhereClause + " And ApprovedOn IS NULL ";

        strSQL = "SELECT * from vw_VSL_CriticalEquipShutdownRequest " + WhereClause + " ";

        DataTable dtReport = null;
        try
        {
            dtReport = Common.Execute_Procedures_Select_ByQuery(strSQL);
        }
        catch
        { }

        rptComponentUnits.DataSource = dtReport;
        rptComponentUnits.DataBind();
    }
    public string DateString(object dt)
    {
        if (Convert.IsDBNull(dt))
        {
            return "";
        }
        else
        {
            try
            {
                return Convert.ToDateTime(dt).ToString("dd-MMM-yyyy");
            }
            catch { return ""; }
        }
    }
    protected void chkVerifed_OnCheckedChanged(object sender, EventArgs e)
    {
        //CheckBox ch=((CheckBox)sender); 
        //string UserName = Session["UserName"].ToString();
        //string VesselCode = ch.Attributes["vsl"];
        //string HistoryId = ch.Attributes["historyid"];
        //Common.Set_Procedures("VerifyJobHistory");
        //Common.Set_ParameterLength(3);
        //Common.Set_Parameters(
        //    new MyParameter("@VesselCode", VesselCode),
        //    new MyParameter("@HistoryId", HistoryId),
        //    new MyParameter("@VerifiedBy", UserName));
        //DataSet ds = new DataSet();
        //if (Common.Execute_Procedures_IUD(ds))
        //{
        //    ProjectCommon.ShowMessage("Verifed Successfully.");
        //}
        //else
        //{
        //    ProjectCommon.ShowMessage("Unable to Verify.");
        //}
        //ShowReport();
    }
    
    protected void btnVerify_OnClick(object sender, EventArgs e)
    {
        Button btnVerify = (Button)sender;
        string VesselCode = btnVerify.Attributes["vsl"];
        string HistoryId = btnVerify.Attributes["historyid"];
        string CompName = btnVerify.Attributes["CompName"];

        ScriptManager.RegisterStartupScript(Page, this.GetType(), "a", "window.open('VerifyJobsPopUp.aspx?VesselCode=" + VesselCode + "&HistoryId=" + HistoryId + "&CompName=" + CompName + "','','status=1,scrollbars=0,toolbar=0,menubar=0,width=700,height=450,top=150,left=250')", true);

        //CheckBox ch=((CheckBox)sender); 
        //string UserName = Session["UserName"].ToString();
        //string VesselCode = ch.Attributes["vsl"];
        //string HistoryId = ch.Attributes["historyid"];
        //Common.Set_Procedures("VerifyJobHistory");
        //Common.Set_ParameterLength(3);
        //Common.Set_Parameters(
        //    new MyParameter("@VesselCode", VesselCode),
        //    new MyParameter("@HistoryId", HistoryId),
        //    new MyParameter("@VerifiedBy", UserName));
        //DataSet ds = new DataSet();
        //if (Common.Execute_Procedures_IUD(ds))
        //{
        //    ProjectCommon.ShowMessage("Verifed Successfully.");
        //}
        //else
        //{
        //    ProjectCommon.ShowMessage("Unable to Verify.");
        //}
        //ShowReport();
    }

    //--------------
    public void BindYear()
    {
        //for (int i = DateTime.Now.Year; i >= 2010; i--)
        //{
        //    ddlYear.Items.Add(i.ToString());
        //}
        ////ddlYear.Items.Insert(0,new ListItem(" < All > ", ""));
        //ddlYear.SelectedValue = DateTime.Today.Year.ToString();
    }
    public void BindCriticalComponent()
    {
        string strSQL = " select ComponentCode, ComponentName from componentMaster where criticalequip=1 order by ComponentCode ";
        DataTable dtReport = Common.Execute_Procedures_Select_ByQuery(strSQL);
        ListCriticalComponent.DataSource = dtReport;
        //ListCriticalComponent.DataTextField = "ComponentName";
        //ListCriticalComponent.DataValueField = "ComponentCode";
        ListCriticalComponent.DataBind();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "ComponentClick();", true);
    }

    protected void btnTemp_OnClick(object sender, EventArgs e)
    {
        int i = 10;
        ShowReport();
    }
}

