using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Office_Admin_ByShip : System.Web.UI.Page
{
    public int SelectedCompId
    {
        set { ViewState["SelectedCompId"] = value; }
        get { return Common.CastAsInt32(ViewState["SelectedCompId"]); }
    }
    public int SelectedJobId
    {
        set { ViewState["SelectedJobId"] = value; }
        get { return Common.CastAsInt32(ViewState["SelectedJobId"]); }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsgUpdatemaker.Text = "";
        if (!Page.IsPostBack)
        {
            rdoOfficeComp_CheckedChanged(sender, e);
            BindVessels();
            BindJobType();
            BindJobInterval();
        }
    }

    protected void rdoOfficeComp_CheckedChanged(object sender, EventArgs e)
    {
        
    }
    protected void rdoOfficeJob_CheckedChanged(object sender, EventArgs e)
    {
        plOfficeComponent.Visible = false;
        plOfficeJob.Visible = true;
    }
    protected void RedirectToPage(object sender, EventArgs e)
    {
        btnplanning.CssClass = "btn1";
        btnrunninghr.CssClass = "btn1";
        Button btn = (Button)sender;
        btn.CssClass = "selbtn";
        string Mode = btn.CommandArgument.ToString();
        plOfficeComponent.Visible = (Mode=="C");
        plOfficeJob.Visible = (Mode == "J");
            
    }
    private void BindVessels()
    {
        DataTable dtVessels = new DataTable();
        string strvessels = "SELECT VesselId,VesselCode,VesselName FROM Vessel VM WHERE EXISTS(SELECT TOP 1 COMPONENTID FROM  VSL_ComponentMasterForVessel EVM WHERE VM.VesselCode = EVM.VesselCode ) ORDER BY VesselName ";
        strvessels = " Select VesselId,VesselCode,VesselName  from dbo.Vessel where vesselstatusid=1 ";
        dtVessels = Common.Execute_Procedures_Select_ByQuery(strvessels);
        if (dtVessels.Rows.Count > 0)
        {
            ddlVessels.DataSource = dtVessels;
            ddlVessels.DataTextField = "VesselName";
            ddlVessels.DataValueField = "VesselCode";
            ddlVessels.DataBind();
        }
        else
        {
            ddlVessels.DataSource = null;
            ddlVessels.DataBind();
        }
        ddlVessels.Items.Insert(0, new ListItem("< All >", ""));
    }

    #region -------------------- Office Component ---------------------
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        SearchComponentList(false);
    }

    private void SearchComponentList(bool download)
    {
        //string strSQL = "SELECT CM.* FROM ComponentMaster CM ";

        //if (ddlVessels.SelectedIndex == 0)
        //{
        //    MessageBox1.ShowMessage("Please select vessel.", true);
        //    ddlVessels.Focus();
        //    return;
        //}

        string strSQL = "SELECT CM.*,VCM.Status,VCM.Maker,VCM.MakerType,VCM.VesselCode FROM ComponentMaster CM " +
                            "INNER JOIN VSL_ComponentMasterForVessel VCM ON VCM.ComponentId = CM.ComponentId ";
        if (ddlVessels.SelectedIndex == 0)
        {
            strSQL = strSQL + " and VCM.VesselCode in(Select VesselCode from Vessel where vesselstatusid=1) ";

        }
        else
        {
            strSQL = strSQL + " and VCM.VesselCode = '" + ddlVessels.SelectedValue.Trim() + "'  ";
        }


        string WhereClause = "WHERE 1=1 ";
        if (txtCompCode.Text.Trim() != "")
        {
            WhereClause = WhereClause + " AND CM.componentcode like '%" + txtCompCode.Text + "%'";

        }
        if (txtCopmpname.Text.Trim() != "")
        {
            WhereClause = WhereClause + " AND CM.componentname like '%" + txtCopmpname.Text + "%'";
        }
        if (txtMaker.Text.Trim() != "")
        {
            WhereClause = WhereClause + " AND VCM.Maker like '%" + txtMaker.Text + "%'";
        }


        string strSearch = strSQL + WhereClause + " ORDER BY componentcode ";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(strSearch);

        if (dt != null && dt.Rows.Count > 0)
        {
            rptCompoenents.DataSource = dt;
            rptCompoenents.DataBind();
            lblComponentListCounter.Text = dt.Rows.Count.ToString() + " records found.";
            if (download)
            {
                ECommon.ExportDatatable(Response, dt.DataSet);
            }
        }
        else
        {
            rptCompoenents.DataSource = null;
            rptCompoenents.DataBind();
            lblComponentListCounter.Text = "0 records found.";
        }
    }
    protected void btnDownloadComponentList_Click(object sender, EventArgs e)
    {
        SearchComponentList(true);
    }

    protected void chkSelectAllCompontnt_OnCheckedChanged(object sender, EventArgs e)
    {
        CheckBox chk = (CheckBox)sender;
        foreach (RepeaterItem itm in rptCompoenents.Items)
        {
            CheckBox chkSelectCompontnt = (CheckBox)itm.FindControl("chkSelectCompontnt");
            chkSelectCompontnt.Checked = chk.Checked;
        }
    }
    
    protected void btnUpdateMaker_Click(object sender, EventArgs e)
    {
        bool chkCheecked = false;
        txtMakerPopup.Text = "";
        foreach (RepeaterItem itm in rptCompoenents.Items)
        {
            CheckBox chkSelectCompontnt = (CheckBox)itm.FindControl("chkSelectCompontnt");
            if (chkSelectCompontnt.Checked)
            {
                chkCheecked = true;
            }
        }

        if (chkCheecked)
            divUpdateMaker.Visible = true;
        else
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "56", "alert('Please select checkbox to update maker.')", true);
        }
    }
    protected void btnCloseUpdateMakerPopup_OnClick(object sender, EventArgs e)
    {
        divUpdateMaker.Visible = false;
    }
    protected void btnSave_UpdateMaker_OnClick(object sender, EventArgs e)
    {
        if (txtMakerPopup.Text.Trim() == "")
        {
            lblMsgUpdatemaker.Text = "Please enter maker.";
            txtMakerPopup.Focus();
            return;
        }
        foreach (RepeaterItem itm in rptCompoenents.Items)
        {
            CheckBox chkSelectCompontnt = (CheckBox)itm.FindControl("chkSelectCompontnt");
            
            if (chkSelectCompontnt.Checked)
            {
                HiddenField hfdCompid = (HiddenField)itm.FindControl("hfdCompid");
                HiddenField hfdVesselCode = (HiddenField)itm.FindControl("hfdVesselCode");
                string sqll = " update VSL_ComponentMasterForVessel set maker='" + txtMakerPopup.Text.Replace("'","`").Trim() + "',updated=1,UpdatedOn=getdate() where VesselCode='"+ hfdVesselCode.Value + "' and ComponentId="+ hfdCompid .Value+ "";
                Common.Execute_Procedures_Select_ByQuery(sqll);
            }
        }
        lblMsgUpdatemaker.Text = "Record updated successfully.";
        SearchComponentList(false);
        txtMakerPopup.Text = "";
    }
    


    protected void btnActiveComp_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        HiddenField hfdVesselCode = (HiddenField)btn.Parent.FindControl("hfdVesselCode");

        string ComponentCode = ((Button)sender).CommandArgument;
        try
        {
            Common.Set_Procedures("sp_ActiveInactive_Admin_Ship_Components");
            Common.Set_ParameterLength(3);
            Common.Set_Parameters(
                new MyParameter("@VesselCode", hfdVesselCode.Value),
                new MyParameter("@ComponentCode", ComponentCode),
                new MyParameter("@Mode", "A")
                );

            DataSet dsActComponent = new DataSet();
            dsActComponent.Clear();
            Boolean res;
            res = Common.Execute_Procedures_IUD(dsActComponent);
            if (res)
            {
                MessageBox1.ShowMessage("Component Activated Successfully.", false);
                btnSearch_Click(sender, e);
            }
            else
            {
                MessageBox1.ShowMessage("Unable to active component.Error :" + Common.getLastError(), true);
            }
        }
        catch (Exception ex)
        {
            MessageBox1.ShowMessage("Unable to active component.Error :" + ex.Message + Common.getLastError(), true);
        }
    }   
    protected void btnInactiveComp_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        HiddenField hfdVesselCode = (HiddenField)btn.Parent.FindControl("hfdVesselCode");

        string ComponentCode = ((Button)sender).CommandArgument;
        try
        {
            Common.Set_Procedures("sp_ActiveInactive_Admin_Ship_Components");
            Common.Set_ParameterLength(3);
            Common.Set_Parameters(
                new MyParameter("@VesselCode", hfdVesselCode.Value),
                new MyParameter("@ComponentCode", ComponentCode),
                new MyParameter("@Mode", "I")
                );

            DataSet dsInaComponent = new DataSet();
            dsInaComponent.Clear();
            Boolean res;
            res = Common.Execute_Procedures_IUD(dsInaComponent);
            if (res)
            {
                MessageBox1.ShowMessage("Component Inactiveted Successfully.", false);
                btnSearch_Click(sender, e);
            }
            else
            {
                MessageBox1.ShowMessage("Unable to inactive component.Error :" + Common.getLastError(), true);
            }
        }
        catch (Exception ex)
        {
            MessageBox1.ShowMessage("Unable to inactive component.Error :" + ex.Message + Common.getLastError(), true);
        }
    }


    #endregion

    #region -------------------- Office Component Jobs ---------------------
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "tr", "SetLastFocus('dvScroll1');", true);
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "tr", "SetLastFocus('divJob1');", true);
        
    }
    protected void btnJobCompSearch_Click(object sender, EventArgs e)
    {
        SearchJobList(false);
    }    

    protected void btnActiveJob_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        HiddenField hfdVesselCode = (HiddenField)btn.Parent.FindControl("hfdVesselCode");
        string CompJobId = ((Button)sender).CommandArgument;
        try
        {
            Common.Set_Procedures("sp_ActiveInactive_Admin_Ship_Jobs");
            Common.Set_ParameterLength(3);
            Common.Set_Parameters(
                new MyParameter("@VesselCode", hfdVesselCode.Value),
                new MyParameter("@CompJobId", CompJobId.Trim()),
                new MyParameter("@Mode", "A")
                );

            DataSet dsActJob = new DataSet();
            dsActJob.Clear();
            Boolean res;
            res = Common.Execute_Procedures_IUD(dsActJob);
            if (res)
            {
                MessageBox1.ShowMessage("Job Activated Successfully.", false);
                btnJobCompSearch_Click(sender, e);
                
            }
            else
            {
                MessageBox1.ShowMessage("Unable to active job.Error :" + Common.getLastError(), true);
            }
        }
        catch (Exception ex)
        {
            MessageBox1.ShowMessage("Unable to active job.Error :" + ex.Message + Common.getLastError(), true);
        }
    }  
    protected void btnInactiveJob_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        HiddenField hfdVesselCode = (HiddenField)btn.Parent.FindControl("hfdVesselCode");
        string CompJobId = ((Button)sender).CommandArgument;
        try
        {
            Common.Set_Procedures("sp_ActiveInactive_Admin_Ship_Jobs");
            Common.Set_ParameterLength(3);
            Common.Set_Parameters(
                new MyParameter("@VesselCode", hfdVesselCode.Value),
                new MyParameter("@CompJobId", CompJobId.Trim()),
                new MyParameter("@Mode", "I")
                );

            DataSet dsInaJob = new DataSet();
            dsInaJob.Clear(); 
            Boolean res;
            res = Common.Execute_Procedures_IUD(dsInaJob);
            if (res)
            {
                MessageBox1.ShowMessage("Job Inactiveted Successfully.", false);
                btnJobCompSearch_Click(sender, e);
            }
            else
            {
                MessageBox1.ShowMessage("Unable to inactive job.Error :" + Common.getLastError(), true);
            }
        }
        catch (Exception ex)
        {
            MessageBox1.ShowMessage("Unable to inactive job.Error :" + ex.Message + Common.getLastError(), true);
        }
    }
    protected void btnDownloadJobList_Click(object sender, EventArgs e)
    {
        SearchJobList(true);
    }


    private void SearchJobList(bool download)
    {
        string strSQL = "SELECT VCM.VesselCode,CM.ComponentCode,CM.ComponentName,VCM.Maker,CJM.CompJobId,JM.JobCode,CJM.DescrSh as JobDesc, ji.IntervalName,VCJM.Interval, VCJM.Status FROM ComponentsJobMapping CJM " +
                           "INNER JOIN ComponentMaster CM ON CM.ComponentId = CJM.ComponentId " +
                           "INNER JOIN JobMaster JM ON JM.JobId = CJM.JobId " +
                           "INNER JOIN VSL_ComponentMasterForVessel VCM ON  VCM.ComponentId = CM.ComponentId  " +
                           "INNER JOIN VSL_VesselComponentJobMaster VCJM ON VCJM.VesselCode = VCM.VesselCode AND VCJM.CompJobId = CJM.CompJobId AND VCJM.VesselCode IN ( (SELECT Distinct VesselCode FROM VSL_VesselComponentJobMaster WHERE CompJobId = VCJM.CompJobId ) UNION (SELECT Distinct VesselCode FROM VesselComponentJobMaster WHERE CompJobId = VCJM.CompJobId ))  " +
                           " Inner join JobIntervalMaster ji on ji.IntervalId=VCJM.IntervalId " +
                           "WHERE  CJM.DescrSh LIKE '%" + txtKeyWord.Text.Trim() + "%' ";
        //"WHERE LEFT(CM.ComponentCode,LEN(LTRIM(RTRIM('" + txtJobCompCode.Text.Trim() + "')))) = '" + txtJobCompCode.Text.Trim() + "' AND CJM.DescrSh LIKE '%" + txtKeyWord.Text.Trim() + "%' ";
        if (ddlVessels.SelectedIndex == 0)
        {
            strSQL = strSQL + " and VCM.VesselCode in(Select VesselCode from Vessel where vesselstatusid=1) ";
        }
        else
        {
            strSQL = strSQL + " and  VCM.VesselCode = '" + ddlVessels.SelectedValue.Trim() + " ' ";
        }


        string WhereClause = "";
        if (txtJobCompCode.Text.Trim() != "")
        {
            WhereClause = WhereClause + " and CM.ComponentCode= '" + txtJobCompCode.Text.Trim() + "' ";
        }

        if (txtComponentName.Text.Trim() != "")
        {
            WhereClause = WhereClause + " and CM.ComponentName like '%" + txtComponentName.Text.Trim() + "%' ";
        }

        if (txtMakerJL.Text.Trim() != "")
        {
            WhereClause = WhereClause + " and VCM.Maker like '%" + txtMakerJL.Text.Trim() + "%' ";
        }
        if (ddlJobCode.SelectedIndex > 0)
        {
            WhereClause = WhereClause + " and JM.JobCode ='" + ddlJobCode.SelectedValue + "' ";
        }

        string JobIntTypes = "";
        if (ddlIntervalType.SelectedValue != "")
        {
            if (ddlIntervalType.SelectedValue == "99")
                JobIntTypes = "2,3,4,5";
            else
                JobIntTypes = ddlIntervalType.SelectedValue;
            WhereClause = WhereClause + " and  VCJM.IntervalId in(" + JobIntTypes + ")";
        }

        strSQL = strSQL + WhereClause + "  ORDER BY CM.ComponentCode";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(strSQL);


        if (dt != null && dt.Rows.Count > 0)
        {
            rptJobs.DataSource = dt;
            rptJobs.DataBind();
            lblJonListCount.Text = dt.Rows.Count.ToString() +" records found.";
            if (download)
            {
                dt.Columns.Remove("CompJobID");
                ECommon.ExportDatatable(Response, dt.DataSet);
            }
        }
        else
        {
            rptJobs.DataSource = null;
            rptJobs.DataBind();
            lblJonListCount.Text = "0 records found.";
        }
    }
    private void BindJobType()
    {
        DataTable dtVessels = new DataTable();
        string strvessels = " select  JobCode from JobMaster ";
        
        dtVessels = Common.Execute_Procedures_Select_ByQuery(strvessels);
        if (dtVessels.Rows.Count > 0)
        {
            ddlJobCode.DataSource = dtVessels;
            ddlJobCode.DataTextField = "JobCode";
            ddlJobCode.DataValueField = "JobCode";
            ddlJobCode.DataBind();
        }
        else
        {
            ddlJobCode.DataSource = null;
            ddlJobCode.DataBind();
        }
        ddlJobCode.Items.Insert(0, new ListItem("< All >", ""));
    }
    private void BindJobInterval()
    {
        DataTable dtinterval = new DataTable();
        string strinterval= " select  IntervalId,IntervalName from JobIntervalMaster ";

        dtinterval = Common.Execute_Procedures_Select_ByQuery(strinterval);
        if (dtinterval.Rows.Count > 0)
        {
            ddlIntervalType.DataSource = dtinterval;
            ddlIntervalType.DataTextField = "IntervalName";
            ddlIntervalType.DataValueField = "IntervalId";
            ddlIntervalType.DataBind();
        }
        else
        {
            ddlIntervalType.DataSource = null;
            ddlIntervalType.DataBind();
        }
        ddlIntervalType.Items.Insert(0, new ListItem("< All >", ""));
        ddlIntervalType.Items.Insert(1, new ListItem("Calender Jobs", "99"));
    }


    #endregion

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetMakerList(string prefixText, int count)
    {
        List<string> PortList = new List<string>();
        string sql = " SElect distinct top " + count + "  Maker from dbo.VSL_ComponentMasterForVessel where Maker like '" + prefixText + "%' order by Maker ";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);

        foreach (DataRow dr in dt.Rows)
        {
            PortList.Add(dr["Maker"].ToString());
        }
        return PortList;
    }

    
}