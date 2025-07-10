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

public partial class Reports_JobDoneBetweenPeriod : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();

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
        if (!Page.IsPostBack)
        {
            if (Session["UserType"].ToString() == "S")
            {
                //ddlVessels.Items.Insert(0, new ListItem("< SELECT >", "0"));
                //ddlVessels.Items.Insert(1, new ListItem(Session["CurrentShip"].ToString(), Session["CurrentShip"].ToString()));
                VessCode = Session["CurrentShip"].ToString();
                //ddlVessels.SelectedIndex = 1;
                //ddlVessels.Visible = false;
                //tdVessel.Visible = false;
                   
            }
            else
            {
                //tdVessel.Visible = true;
                //BindVessels();
              
            }
                //txtFromDt.Text = DateTime.Today.AddMonths(-1).ToString("dd-MMM-yyyy");    
                //txtToDt.Text=DateTime.Today.ToString("dd-MMM-yyyy");      
                ShowReport();
        }
    }
    //private void BindVessels()
    //{
    //    DataTable dtVessels = new DataTable();
    //    string strvessels = "SELECT VesselId,VesselCode,VesselName FROM Vessel VM WHERE EXISTS(SELECT TOP 1 COMPONENTID FROM  VSL_ComponentMasterForVessel EVM WHERE VM.VesselCode = EVM.VesselCode ) ORDER BY VesselName ";
    //    dtVessels = Common.Execute_Procedures_Select_ByQuery(strvessels);
    //    if (dtVessels.Rows.Count > 0)
    //    {
    //        ddlVessels.DataSource = dtVessels;
    //        ddlVessels.DataTextField = "VesselName";
    //        ddlVessels.DataValueField = "VesselCode";
    //        ddlVessels.DataBind();
    //    }
    //    else
    //    {
    //        ddlVessels.DataSource = null;
    //        ddlVessels.DataBind();
    //    }
    //    ddlVessels.Items.Insert(0, "< SELECT VESSEL >");
    //}
    protected void btnViewReport_Click(object sender, EventArgs e)
    {
        //if (ddlVessels.SelectedIndex == 0)
        //{
        //    lblMessage.Text = "Please select a vessel.";
        //    ddlVessels.Focus();
        //    return;
        //}
        if (txtFromDt.Text == "" && txtToDt.Text != "")
        {
            lblMessage.Text = "Please enter from date.";
            txtFromDt.Focus();
            return;
        }
        if (txtFromDt.Text != "" && txtToDt.Text == "")
        {
            lblMessage.Text = "Please enter to date.";
            txtToDt.Focus();
            return;
        }
        DateTime temp;
        if (txtFromDt.Text != "")
        {
            if (!DateTime.TryParse(txtFromDt.Text.Trim(), out temp))
            {
                lblMessage.Text = "Please enter valid date.";
                txtFromDt.Focus();
                return;
            }
        }
        
        DateTime temp1;
        if (txtToDt.Text != "")
        {
            if (!DateTime.TryParse(txtToDt.Text.Trim(), out temp1))
            {
                lblMessage.Text = "Please enter valid date.";
                txtToDt.Focus();
                return;
            }
        }
        ShowReport(); 
    }
    protected void ShowReport()
    {
        string WhereClause = " where Action <>'Postponed' ";
        

        if (txtHistorID.Text.Trim() != "")
            WhereClause = WhereClause + " And HistoryId =" + Common.CastAsInt32( txtHistorID.Text.Trim());

        if (txtCompCode.Text.Trim() != "")
            WhereClause = WhereClause + " And ComponentCode like '" + txtCompCode.Text.Trim() + "%'";
        if (txtCompName.Text.Trim() != "")
            WhereClause = WhereClause + " And ComponentName like '" + txtCompName.Text.Trim() + "%'";

        if (chkClass.Checked)
            WhereClause = WhereClause + " And ClassEquip =1 ";
        if (txtClassCode.Text != "")
            WhereClause = WhereClause + " AND  ClassEquipCode like '" + txtClassCode.Text.Trim() + "%'";

        if (ddlJobStatus.SelectedIndex != 0)
        {
            if (ddlJobStatus.SelectedIndex == 1)
            {
                if (Session["UserType"].ToString() == "S")
                    WhereClause = WhereClause + " And ISNULL(Verified,0) =1";
                else
                    WhereClause = WhereClause + " And Verified_Off =1";
            }
            else if (ddlJobStatus.SelectedIndex == 2)
            {
                if (Session["UserType"].ToString() == "S")
                    WhereClause = WhereClause + " And ISNULL(Verified,0) =0";
                else
                    WhereClause = WhereClause + " And Verified_Off =0";
            }
        }
        if (ddlIntType.SelectedIndex != 0)
            if (ddlIntType.SelectedIndex == 2)
                WhereClause = WhereClause + " And IntervalName ='H'";
            else
                WhereClause = WhereClause + " And IntervalName <>'H'";

        if (chkCritical.Checked)
        {
            WhereClause = WhereClause + " And Criticalequip  =1";
        }

        if (txtFromDt.Text.Trim() != "" && txtToDt.Text.Trim() != "")
        {

            WhereClause = WhereClause + " And VesselCode='" + VessCode + "' AND ((DoneDate BETWEEN '" + txtFromDt.Text.Trim() + "' AND '" + txtToDt.Text.Trim() + "'))";
        }
        else
        {
            WhereClause = WhereClause + " And VesselCode='" + VessCode + "'";
        }

        //string strSQL = "SELECT * from vw_GetJobUpdateDataByPeriod WHERE VesselCode = '" + ddlVessels.SelectedValue.Trim() + "' AND ((DoneDate BETWEEN '" + txtFromDt.Text.Trim() + "' AND '" + txtToDt.Text.Trim() + "') OR (PostPoneDate >= '" + txtFromDt.Text.Trim() + "' AND PostPoneDate < DATEADD(dd,1,CAST('" + txtToDt.Text.Trim() + "' AS DATETIME)))) ORDER BY DoneDate desc";

        string strSQL = "SELECT * from vw_GetJobUpdateDataByPeriod " + WhereClause + " ORDER BY DoneDate desc";
        

        DataTable dtReport = Common.Execute_Procedures_Select_ByQuery(strSQL);
        rptComponentUnits.DataSource = dtReport;
        lblCount.Text = dtReport.Rows.Count.ToString() + " Record(s) found.";  
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
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
   
}
