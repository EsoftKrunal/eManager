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


public partial class Office_Admin_Delete : System.Web.UI.Page
{
    AuthenticationManager Auth;
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
        // -------------------------- SESSION CHECK ----------------------------------------------
        ProjectCommon.SessionCheck();
        // -------------------------- SESSION CHECK END ----------------------------------------------
        //***********Code to check page acessing Permission
        if (Session["UserType"].ToString() == "O")
        {
            Auth = new AuthenticationManager(1047, Convert.ToInt32(Session["loginid"].ToString()), ObjectType.Page);
            if (!(Auth.IsView))
            {
                Response.Redirect("~/AuthorityError.aspx");
            }
        }
        //***********
        if (!Page.IsPostBack)
        {
            //rdoOfficeComp.Checked = true;
            //rdoOfficeComp_CheckedChanged(sender, e);
            rdoVesselFinalize.Checked = true;
            rdoShipComp_CheckedChanged(sender, e);

            //DataTable dt = Common.Execute_Procedures_Select_ByQuery("select vesselcode,vesselname from dbo.Vessel where vesselcode in (select distinct vesselcode from vsl_componentmasterforvessel) order by vesselname");
            DataTable dt = Common.Execute_Procedures_Select_ByQuery("select vesselcode,vesselname from dbo.Vessel where vesselcode in (select distinct VM.vesselcode from vsl_componentmasterforvessel VSL INNER JOIN Vessel VM ON VM.VesselCode = VSL.VesselCode WHERE ISNULL(VM.IsExported,0) = 0) order by vesselname");
            ddlVessel.DataSource = dt; 
            ddlVessel.DataTextField = "Vesselname";
            ddlVessel.DataValueField= "Vesselcode";
            ddlVessel.DataBind();
            ddlVessel.Items.Insert(0, new ListItem("< Select >", ""));
            btnStart.Enabled = false;

            btnoffCompDelete.Visible = false;
            btnDeleteJobs.Visible = false;

            //BindVessels();
            //BindYears();

            //ddlYear.SelectedValue = DateTime.Today.Year.ToString();
            //ddlMonth.SelectedValue = DateTime.Today.Month.ToString();

            
        }
        if (Session["loginid"].ToString() == "1")
        {
            rdoOfficeComp.Visible = true;
            rdoOfficeJob.Visible = true;
            rdoMKPI.Visible = true;
            //rdoJobCorrection.Visible = true;
        }
    }
    protected void ddlVessel_SelectIndexChanged(object sender, EventArgs e)
    {
        if(ddlVessel.SelectedIndex >0)
        {
            DataTable dt = Common.Execute_Procedures_Select_ByQuery("select isnull(vm.IsExported,0) as IsExported,ul.FirstName + ' ' + ul.LastName as ExportedBy,vm.ExportDate from dbo.Vessel vm left join dbo.userlogin ul on ul.loginid=vm.exportedby where vesselcode='" + ddlVessel.SelectedValue + "'");
            if (dt.Rows.Count > 0)
            {
                lblExpoted.Text = (dt.Rows[0]["IsExported"].ToString().Trim().ToLower()=="true") ? "Yes" : "No";
                if (lblExpoted.Text.Trim() == "Yes")
                {
                    lblExportedBy.Text = dt.Rows[0]["ExportedBy"].ToString();
                    lblExportedOn.Text = Convert.ToDateTime(dt.Rows[0]["ExportDate"].ToString()).ToString("dd-MMM-yyyy");
                }
                else
                {
                    lblExportedBy.Text = "";
                    lblExportedOn.Text = "";
                }
                btnStart.Enabled = true;
            }
        }
        else
        {
            btnStart.Enabled=false;  
        }
    }
    protected void btnStart_OnClick(object sender, EventArgs e)
    {
        if (ddlVessel.SelectedValue.Trim() != "")
        {
            Common.Execute_Procedures_Select_ByQuery("UPDATE Vessel SET ISEXPORTED=1,EXPORTEDBY=" + Session["loginid"].ToString() + ",EXPORTDATE=GETDATE() WHERE VESSELCODE='" + ddlVessel.SelectedValue + "'");
            ddlVessel_SelectIndexChanged(sender, e); 
        }
    }
    #region -------------------- Events -----------------------------

    #region -------------------- Common -----------------------------
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "tr", "SetLastFocus('dvScroll');", true);
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "tr", "SetLastFocus('divComVessel');", true);
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "tr", "SetLastFocus('divJob');", true);
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "tr", "SetLastFocus('divJobVessel');", true);
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "tr", "SetLastFocus('dvKPI');", true);
    }

    protected void rdoOfficeComp_CheckedChanged(object sender, EventArgs e)
    {
        plOfficeComponent.Visible = true;
        plOfficeJob.Visible = false;
        plShipComponent.Visible = false;
        plShipJob.Visible = false;
        plMKPI.Visible = false;
        plByShip.Visible = false;
        pnlJobCorrection.Visible = false;

    }
    protected void rdoOfficeJob_CheckedChanged(object sender, EventArgs e)
    {
        plOfficeComponent.Visible = false;
        plOfficeJob.Visible = true;
        plShipComponent.Visible = false;
        plShipJob.Visible = false;
        plMKPI.Visible = false;
        plByShip.Visible = false;
        pnlJobCorrection.Visible = false;

    }
    protected void rdoShipComp_CheckedChanged(object sender, EventArgs e)
    {
        plOfficeComponent.Visible = false;
        plOfficeJob.Visible = false;
        plShipComponent.Visible = true;
        plShipJob.Visible = false;
        plMKPI.Visible = false;
        plByShip.Visible = false;
        pnlJobCorrection.Visible = false;

    }
    protected void rdoShipJob_CheckedChanged(object sender, EventArgs e)
    {
        plOfficeComponent.Visible = false;
        plOfficeJob.Visible = false;
        plShipComponent.Visible = false;
        plShipJob.Visible = true;
        plMKPI.Visible = false;
        plByShip.Visible = false;
        pnlJobCorrection.Visible = false;

    }
    protected void rdomkpi_CheckedChanged(object sender, EventArgs e)
    {
        plOfficeComponent.Visible = false;
        plOfficeJob.Visible = false;
        plShipComponent.Visible = false;
        plShipJob.Visible = false;
        plMKPI.Visible = true;
        plByShip.Visible = false;
        pnlJobCorrection.Visible = false;
    }
    protected void rdoByShip_CheckedChanged(object sender, EventArgs e)
    {
        plOfficeComponent.Visible = false;
        plOfficeJob.Visible = false;
        plShipComponent.Visible = false;
        plShipJob.Visible = false;
        plMKPI.Visible = false;
        plByShip.Visible = true;
        pnlJobCorrection.Visible = false;
    }
    protected void rdoJobCorrection_CheckedChanged(object sender, EventArgs e)
    {
        plOfficeComponent.Visible = false;
        plOfficeJob.Visible = false;
        plShipComponent.Visible = false;
        plShipJob.Visible = false;
        plMKPI.Visible = false;
        plByShip.Visible = false;
        pnlJobCorrection.Visible = true;
    }

    #endregion

    #region -------------------- Office Component ---------------------
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string strSQL = "SELECT CM.* FROM ComponentMaster CM ";

        string WhereClause = "WHERE 1=1 ";
        if (txtCompCode.Text.Trim() != "")
        {
            WhereClause = WhereClause + " AND CM.componentcode like '%" + txtCompCode.Text + "%'";

        }
        if (txtCopmpname.Text.Trim() != "")
        {
            WhereClause = WhereClause + " AND CM.componentname like '%" + txtCopmpname.Text + "%'";
        }

        string strSearch = strSQL + WhereClause + " ORDER BY componentcode ";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(strSearch);
        if (dt != null)
            if (dt.Rows.Count > 0)
            {
                rptCompoenents.DataSource = dt;
                rptCompoenents.DataBind();
            }
            else
            {
                rptCompoenents.DataSource = null;
                rptCompoenents.DataBind();
            }
        //btnoffCompDelete.Visible = false;
        if (SelectedCompId == 0)
        {
            rptOfficeDependencies.DataSource = null;
            rptOfficeDependencies.DataBind();
        }
    }
    protected void btnComponent_Click(object sender, EventArgs e)
    {
        if (sender != null)
        {
            hfOfficeCompId.Value = ((LinkButton)sender).CommandArgument;
            hfCompCode.Value = ((LinkButton)sender).Text.Trim();
            SelectedCompId = Common.CastAsInt32(hfOfficeCompId.Value);
        }

        string strSQL = "SELECT DISTINCT VM.VesselCode + ' - ' + VM.VesselName AS Vessel, VCM.Status,VM.VesselCode FROM Vessel VM " +
                        "LEFT JOIN VSL_ComponentMasterForVessel VCM ON VM.VesselCode = VCM.VesselCode AND VCM.ComponentId = " + hfOfficeCompId.Value.Trim() + " " +
                        "WHERE VM.VesselCode IN ( " +
                        "(SELECT Distinct VesselCode FROM VSL_ComponentMasterForVessel WHERE ComponentId =" + hfOfficeCompId.Value.Trim() + " ) UNION " +
                        "(SELECT Distinct VesselCode FROM ComponentMasterForVessel WHERE ComponentId = " + hfOfficeCompId.Value.Trim() + "))";
        DataTable dtvessels = Common.Execute_Procedures_Select_ByQuery(strSQL);
        if (dtvessels.Rows.Count > 0)
        {
            rptOfficeDependencies.DataSource = dtvessels;
            rptOfficeDependencies.DataBind();

        }
        else
        {
            rptOfficeDependencies.DataSource = null;
            rptOfficeDependencies.DataBind();
        }
        btnSearch_Click(sender, e);
        SelectedCompId = 0;
       // btnoffCompDelete.Visible = true;
    }
    protected void btnoffCompDelete_Click(object sender, EventArgs e)
    {
        if (hfCompCode.Value.Trim() == "")
        {
            MessageBox1.ShowMessage("Please Select a component.", true);
            return;
        }

        try
        {
            Common.Set_Procedures("sp_ActiveInactive_Admin_Office_Components");
            Common.Set_ParameterLength(2);
            Common.Set_Parameters(
                new MyParameter("@ComponentCode", hfCompCode.Value.Trim()),
                new MyParameter("@Mode", "I")
                );

            DataSet dsComponents = new DataSet();
            dsComponents.Clear();
            Boolean res;
            res = Common.Execute_Procedures_IUD(dsComponents);
            if (res)
            {
                MessageBox1.ShowMessage("Component deleted Successfully.", false);
            }
            else
            {
                MessageBox1.ShowMessage("Unable to delete component.Error :" + Common.getLastError(), true);
            }
        }
        catch (Exception ex)
        {
            MessageBox1.ShowMessage("Unable to delete component.Error :" + ex.Message + Common.getLastError() , true);
        }
    }

    protected void btnActiveComp_Click(object sender, EventArgs e)
    {
        string vesselcode = ((Button)sender).CommandArgument;
        try
        {
            Common.Set_Procedures("sp_ActiveInactive_Admin_Ship_Components");
            Common.Set_ParameterLength(3);
            Common.Set_Parameters(
                new MyParameter("@VesselCode", vesselcode),
                new MyParameter("@ComponentCode", hfCompCode.Value),
                new MyParameter("@Mode", "A")
                );

            DataSet dsActComponent = new DataSet();
            dsActComponent.Clear();
            Boolean res;
            res = Common.Execute_Procedures_IUD(dsActComponent);
            if (res)
            {
                MessageBox1.ShowMessage("Component Activated Successfully.", false);
                RefreshCompVesselGrid();
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
        string vesselcode = ((Button)sender).CommandArgument;
        try
        {
            Common.Set_Procedures("sp_ActiveInactive_Admin_Ship_Components");
            Common.Set_ParameterLength(3);
            Common.Set_Parameters(
                new MyParameter("@VesselCode", vesselcode),
                new MyParameter("@ComponentCode", hfCompCode.Value),
                new MyParameter("@Mode", "I")
                );

            DataSet dsInaComponent = new DataSet();
            dsInaComponent.Clear();
            Boolean res;
            res = Common.Execute_Procedures_IUD(dsInaComponent);
            if (res)
            {
                MessageBox1.ShowMessage("Component Inactiveted Successfully.", false);
                RefreshCompVesselGrid();
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

    public void RefreshCompVesselGrid()
    {
        string strSQL = "SELECT DISTINCT VM.VesselCode + ' - ' + VM.VesselName AS Vessel, VCM.Status,VM.VesselCode FROM Vessel VM " +
                        "LEFT JOIN VSL_ComponentMasterForVessel VCM ON VM.VesselCode = VCM.VesselCode AND VCM.ComponentId = " + hfOfficeCompId.Value.Trim() + " " +
                        "WHERE VM.VesselCode IN ( " +
                        "(SELECT Distinct VesselCode FROM VSL_ComponentMasterForVessel WHERE ComponentId =" + hfOfficeCompId.Value.Trim() + " ) UNION " +
                        "(SELECT Distinct VesselCode FROM ComponentMasterForVessel WHERE ComponentId = " + hfOfficeCompId.Value.Trim() + "))";
        DataTable dtvessels = Common.Execute_Procedures_Select_ByQuery(strSQL);
        if (dtvessels.Rows.Count > 0)
        {
            rptOfficeDependencies.DataSource = dtvessels;
            rptOfficeDependencies.DataBind();

        }
        else
        {
            rptOfficeDependencies.DataSource = null;
            rptOfficeDependencies.DataBind();
        }
    }

    #endregion

    #region -------------------- Office Component Jobs ---------------------

    protected void btnJobCompSearch_Click(object sender, EventArgs e)
    {
        if (txtJobCompCode.Text.Trim() == "")
        {
            MessageBox1.ShowMessage("Please enter component code." , true);
            txtJobCompCode.Focus(); 
            return;
        }
        string strSQL = "SELECT CM.ComponentCode,CJM.CompJobId,CJM.DescrSh,JM.JobCode FROM ComponentsJobMapping CJM " +
                        "INNER JOIN ComponentMaster CM ON CM.ComponentId = CJM.ComponentId " +
                        "INNER JOIN JobMaster JM ON JM.JobId = CJM.JobId " +
                        "WHERE LEFT(CM.ComponentCode,LEN(LTRIM(RTRIM('" + txtJobCompCode.Text.Trim() + "')))) = '" + txtJobCompCode.Text.Trim() + "' AND CJM.DescrSh LIKE '%" + txtKeyWord.Text.Trim() + "%'";

        //string WhereClause = "WHERE 1=1 ";
        //if (txtCompCode.Text.Trim() != "")
        //{
        //    WhereClause = WhereClause + " AND CM.componentcode like '%" + txtCompCode.Text + "%'";

        //}
        //if (txtCopmpname.Text.Trim() != "")
        //{
        //    WhereClause = WhereClause + " AND CM.componentname like '%" + txtCopmpname.Text + "%'";
        //}

        //string strSearch = strSQL + WhereClause + " ORDER BY componentcode ";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(strSQL);
        if (dt != null)
            if (dt.Rows.Count > 0)
            {
                rptJobs.DataSource = dt;
                rptJobs.DataBind();
            }
            else
            {
                rptJobs.DataSource = null;
                rptJobs.DataBind();
            }
       //btnDeleteJobs.Visible = false;
        if (SelectedJobId == 0)
        {
            rptjobDependencies.DataSource = null;
            rptjobDependencies.DataBind();
        }
    }
    protected void btnJob_Click(object sender, EventArgs e)
    {
        hfCompJobId.Value = ((LinkButton)sender).CommandArgument;
        //hfOffJobCompCode.Value = ((LinkButton)sender).Text.Trim();
        SelectedJobId = Common.CastAsInt32(hfCompJobId.Value);

        string strSQL = "SELECT DISTINCT VM.VesselCode + ' - ' + VM.VesselName AS Vessel, VCJM.Status,VM.VesselCode FROM Vessel VM " +
                        "LEFT JOIN VSL_VesselComponentJobMaster VCJM ON VM.VesselCode = VCJM.VesselCode AND VCJM.CompJobId = " + hfCompJobId.Value.Trim() + " " +
                        "WHERE VM.VesselCode IN ( " +
                        "(SELECT Distinct VesselCode FROM VSL_VesselComponentJobMaster WHERE CompJobId = " + hfCompJobId.Value.Trim() + " ) UNION " +
                        "(SELECT Distinct VesselCode FROM VesselComponentJobMaster WHERE CompJobId = " + hfCompJobId.Value.Trim() + " ))";
        
        DataTable dtvessels = Common.Execute_Procedures_Select_ByQuery(strSQL);
        if (dtvessels.Rows.Count > 0)
        {
            rptjobDependencies.DataSource = dtvessels;
            rptjobDependencies.DataBind();

        }
        else
        {
            rptjobDependencies.DataSource = null;
            rptjobDependencies.DataBind();
        }
        btnJobCompSearch_Click(sender, e);
        SelectedJobId = 0;
        //btnDeleteJobs.Visible = true;
    }
    protected void btnDeleteJobs_Click(object sender, EventArgs e)
    {
        if (hfCompJobId.Value.Trim() == "")
        {
            MessageBox1.ShowMessage("Please Select a Job.", true);
            return;
        }

        try
        {
            Common.Set_Procedures("sp_ActiveInactive_Admin_Office_Jobs");
            Common.Set_ParameterLength(2);
            Common.Set_Parameters(
                new MyParameter("@CompJobId", hfCompJobId.Value.Trim()),
                new MyParameter("@Mode", "I")
                );

            DataSet dsComponents = new DataSet();
            dsComponents.Clear();
            Boolean res;
            res = Common.Execute_Procedures_IUD(dsComponents);
            if (res)
            {
                MessageBox1.ShowMessage("Job deleted Successfully.", false);
            }
            else
            {
                MessageBox1.ShowMessage("Unable to delete Job.Error :" + Common.getLastError(), true);
            }
        }
        catch (Exception ex)
        {
            MessageBox1.ShowMessage("Unable to delete Job.Error :" + ex.Message + Common.getLastError(), true);
        }
    }

    protected void btnActiveJob_Click(object sender, EventArgs e)
    {
        string vesselcode = ((Button)sender).CommandArgument;
        try
        {
            Common.Set_Procedures("sp_ActiveInactive_Admin_Ship_Jobs");
            Common.Set_ParameterLength(3);
            Common.Set_Parameters(
                new MyParameter("@VesselCode", vesselcode),
                new MyParameter("@CompJobId", hfCompJobId.Value.Trim()),
                new MyParameter("@Mode", "A")
                );

            DataSet dsActJob = new DataSet();
            dsActJob.Clear();
            Boolean res;
            res = Common.Execute_Procedures_IUD(dsActJob);
            if (res)
            {
                MessageBox1.ShowMessage("Job Activated Successfully.", false);
                RefeshJobvesselGrid();
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
        string vesselcode = ((Button)sender).CommandArgument;
        try
        {
            Common.Set_Procedures("sp_ActiveInactive_Admin_Ship_Jobs");
            Common.Set_ParameterLength(3);
            Common.Set_Parameters(
                new MyParameter("@VesselCode", vesselcode),
                new MyParameter("@CompJobId", hfCompJobId.Value.Trim()),
                new MyParameter("@Mode", "I")
                );

            DataSet dsInaJob = new DataSet();
            dsInaJob.Clear();
            Boolean res;
            res = Common.Execute_Procedures_IUD(dsInaJob);
            if (res)
            {
                MessageBox1.ShowMessage("Job Inactiveted Successfully.", false);
                RefeshJobvesselGrid();
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

    public void RefeshJobvesselGrid()
    {
        string strSQL = "SELECT DISTINCT VM.VesselCode + ' - ' + VM.VesselName AS Vessel, VCJM.Status,VM.VesselCode FROM Vessel VM " +
                        "LEFT JOIN VSL_VesselComponentJobMaster VCJM ON VM.VesselCode = VCJM.VesselCode AND VCJM.CompJobId = " + hfCompJobId.Value.Trim() + " " +
                        "WHERE VM.VesselCode IN ( " +
                        "(SELECT Distinct VesselCode FROM VSL_VesselComponentJobMaster WHERE CompJobId = " + hfCompJobId.Value.Trim() + " ) UNION " +
                        "(SELECT Distinct VesselCode FROM VesselComponentJobMaster WHERE CompJobId = " + hfCompJobId.Value.Trim() + " ))";

        DataTable dtvessels = Common.Execute_Procedures_Select_ByQuery(strSQL);
        if (dtvessels.Rows.Count > 0)
        {
            rptjobDependencies.DataSource = dtvessels;
            rptjobDependencies.DataBind();

        }
        else
        {
            rptjobDependencies.DataSource = null;
            rptjobDependencies.DataBind();
        }
    }

    #endregion

    #endregion

    #region ---------------- MKPI ----------------------------

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
    //    ddlVessels.Items.Insert(0, new ListItem("< ALL >",""));
    //}
    //private void BindYears()
    //{
    //    for (int i = DateTime.Today.Year; i >= 2012; i--)
    //    {
    //        ddlYear.Items.Add(new ListItem(Convert.ToString(i), Convert.ToString(i)));
    //    }

    //    int CurrYear = DateTime.Today.Year;

    //    ddlYear.SelectedValue = CurrYear.ToString();

    //}
    ////private void BindMonths()
    ////{
    ////    ddlMonth.Items.Add(new ListItem("Jan", "1"));
    ////    ddlMonth.Items.Add(new ListItem("Feb", "2"));
    ////    ddlMonth.Items.Add(new ListItem("Mar", "3"));
    ////    ddlMonth.Items.Add(new ListItem("Apr", "4"));
    ////    ddlMonth.Items.Add(new ListItem("may", "5"));
    ////    ddlMonth.Items.Add(new ListItem("Jun", "6"));
    ////    ddlMonth.Items.Add(new ListItem("Jul", "7"));
    ////    ddlMonth.Items.Add(new ListItem("Aug", "8"));
    ////    ddlMonth.Items.Add(new ListItem("Sep", "9"));
    ////    ddlMonth.Items.Add(new ListItem("Oct", "10"));
    ////    ddlMonth.Items.Add(new ListItem("Nov", "11"));
    ////    ddlMonth.Items.Add(new ListItem("Dec", "12"));

    ////    ddlMonth.SelectedValue = "1";
    ////}

    //protected void btnView_Click(object sender, EventArgs e)
    //{
    //    BindKPI();
    //}
    //protected void btnPrintKPI_Click(object sender, EventArgs e)
    //{
    //    //if (ddlVessels.SelectedIndex == 0)
    //    //{
    //    //    MessageBox1.ShowMessage("Please select vessel.", true);
    //    //    ddlVessels.Focus();
    //    //    return;
    //    //}

    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "print", "openprintwindow('" + ddlVessels.SelectedValue.Trim() + "','" + ddlYear.SelectedValue.Trim() + "')", true);
    //}
    //public void BindKPI()
    //{
    //    Common.Set_Procedures("sp_CheckMaintenanceKPIData");
    //    Common.Set_ParameterLength(3);
    //    Common.Set_Parameters(
    //        new MyParameter("@VesselCode", ddlVessels.SelectedValue.Trim()),
    //        new MyParameter("@Year", ddlYear.SelectedValue.Trim()),
    //        new MyParameter("@Mnth", ddlMonth.SelectedValue.Trim())
    //        );

    //    DataSet dsKPI = new DataSet();
    //    dsKPI.Clear();
    //    Boolean res;
    //    res = Common.Execute_Procedures_IUD(dsKPI);
    //    if (res)
    //    {

    //        rptKPI.DataSource = dsKPI.Tables[0];
    //        rptKPI.DataBind();
    //    }
    //    else
    //    {

    //    }
    //}
    //protected void btnSaveKPI_Click(object sender, EventArgs e)
    //{

    //    int Month = Common.CastAsInt32(((Button)sender).CommandArgument);

    //    try
    //    {

    //        Common.Set_Procedures("sp_InsertUpdateMonthwiseMaintenanceKPI");
    //        Common.Set_ParameterLength(4);
    //        Common.Set_Parameters(
    //            new MyParameter("@shipid", ddlVessels.SelectedValue.Trim()),
    //            new MyParameter("@year", ddlYear.SelectedValue.Trim()),
    //            new MyParameter("@month", Month),
    //            new MyParameter("@PublishedBy", Common.CastAsInt32(Session["loginid"].ToString()))
    //            );

    //        DataSet dsKPI = new DataSet();
    //        dsKPI.Clear();
    //        Boolean res;
    //        res = Common.Execute_Procedures_IUD(dsKPI);
    //        if (res)
    //        {
    //            BindKPI();
    //            MessageBox1.ShowMessage("Record Saved Successfully.", false);
    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        MessageBox1.ShowMessage("Unable to save record. Error : " + ex.Message, true);
    //    }

    //}
    //protected void btnEditKPI_Click(object sender, EventArgs e)
    //{
    //   string VesselCode = ((Button)sender).CommandArgument.ToString().Split('@').GetValue(0).ToString();
    //   int Month = Common.CastAsInt32(((Button)sender).CommandArgument.ToString().Split('@').GetValue(1));

    //   ShowKPIDetails(VesselCode,Month);

    //}

    //public void ShowKPIDetails(string vesselcode, int Month)
    //{
    //    lblvessel.Text = vesselcode;
    //    switch(Month)
    //    {
    //        case 1:
    //            lblMonth.Text = "Jan";
    //            break;
    //        case 2:
    //            lblMonth.Text = "Feb";
    //            break;
    //        case 3:
    //            lblMonth.Text = "Mar";
    //            break;
    //        case 4:
    //            lblMonth.Text = "Apr";
    //            break;
    //        case 5:
    //            lblMonth.Text = "May";
    //            break;
    //        case 6:
    //            lblMonth.Text = "Jun";
    //            break;
    //        case 7:
    //            lblMonth.Text = "Jul";
    //            break;
    //        case 8:
    //            lblMonth.Text = "Aug";
    //            break;
    //        case 9:
    //            lblMonth.Text = "Sep";
    //            break;
    //        case 10:
    //            lblMonth.Text = "Oct";
    //            break;
    //        case 11:
    //            lblMonth.Text = "Nov";
    //            break;
    //        case 12:
    //            lblMonth.Text = "Dec";
    //            break;
    //        default:
    //            lblMonth.Text = "";
    //            break;
    //    }

    //    lblYear.Text = ddlYear.SelectedValue;

    //    string SQL = "SELECT TotalSystemJobs,DueJobs,OverDueJobs,OD1Week,OD2Week,ODMorethan2week,OutStandingJobs " +
    //                 "FROM VSL_MaintenanceKPI WHERE VesselCode = '" + vesselcode + "' AND [Year]= " + ddlYear.SelectedValue + " AND [Month]= " + Month;
    //    DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);

    //    if (dt.Rows.Count > 0)
    //    {
    //        int TotalJobs = Common.CastAsInt32(dt.Rows[0]["DueJobs"].ToString()) + Common.CastAsInt32(dt.Rows[0]["OverDueJobs"].ToString());
    //        int ODJobs = Common.CastAsInt32(dt.Rows[0]["OD1Week"].ToString()) + Common.CastAsInt32(dt.Rows[0]["OD2Week"].ToString()) + Common.CastAsInt32(dt.Rows[0]["ODMorethan2week"].ToString());

    //        txtSystemJobs.Text = dt.Rows[0]["TotalSystemJobs"].ToString();
    //        txtDueJobs.Text = dt.Rows[0]["DueJobs"].ToString();
    //        lblODJobs.Text = ODJobs.ToString();
    //        lblTotalJobs.Text = TotalJobs.ToString();
    //        txtOutstandingJobs.Text = dt.Rows[0]["OutStandingJobs"].ToString();
    //        txtOD1W.Text = dt.Rows[0]["OD1Week"].ToString();
    //        txtOD2W.Text = dt.Rows[0]["OD2Week"].ToString();
    //        txtODMore2W.Text = dt.Rows[0]["ODMorethan2week"].ToString();

    //        dvKPIEdit.Visible = true;
    //    }
    //}
    //protected void btnUpdateKPI_Click(object sender, EventArgs e)
    //{
    //    Common.Set_Procedures("sp_Update_MaintenanceKPI");        
    //    Common.Set_ParameterLength(9);
    //    Common.Set_Parameters(
    //        new MyParameter("@VesselCode", lblvessel.Text.Trim()),
    //        new MyParameter("@Year", lblYear.Text.Trim()),
    //        new MyParameter("@Month", ddlMonth.SelectedValue.Trim()),
    //        new MyParameter("@TotalSystemJobs", txtSystemJobs.Text.Trim()),
    //        new MyParameter("@DueJobs", txtDueJobs.Text.Trim()),
    //        new MyParameter("@OD1Week", txtOD1W.Text.Trim()),
    //        new MyParameter("@OD2Week", txtOD2W.Text.Trim()),
    //        new MyParameter("@ODMorethan2week", txtODMore2W.Text.Trim()),
    //        new MyParameter("@OutStandingJobs", txtOutstandingJobs.Text.Trim())
    //        );

    //    DataSet dsKPI = new DataSet();
    //    dsKPI.Clear();
    //    Boolean res;
    //    res = Common.Execute_Procedures_IUD(dsKPI);
    //    if (res)
    //    {
    //        BindKPI();
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "alert('Maintenance KPI updated successfully.')", true);

    //    }
    //    else
    //    {
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Failure", "alert('Unable to update Maintenance KPI.')", true);
    //    }


    //}
    //protected void btnClose_Click(object sender, EventArgs e)
    //{
    //    ClearKPIDetails();
    //    dvKPIEdit.Visible = false;
    //}
    //public void ClearKPIDetails()
    //{
    //    txtSystemJobs.Text = "";
    //    txtDueJobs.Text = "";
    //    lblODJobs.Text = "";
    //    lblTotalJobs.Text = "";
    //    txtOutstandingJobs.Text = "";
    //    txtOD1W.Text = "";
    //    txtOD2W.Text = "";
    //    txtODMore2W.Text = "";
    //}

    #endregion



}
