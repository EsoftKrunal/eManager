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
using ShipSoft.CrewManager.BusinessObjects;
using ShipSoft.CrewManager.BusinessLogicLayer;
using ShipSoft.CrewManager.Operational;
using System.IO;

public partial class Register_VesselReports : System.Web.UI.Page
{
    public int ReportId
    {
        set { ViewState["ReportId"] = value; }
        get { return Common.CastAsInt32(ViewState["ReportId"]); }
    }

    Authority Auth;
    string Mode = "New";
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        //--------------------------------- PAGE ACCESS AUTHORITY ----------------------------------------------------
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 152);
        if (chpageauth <= 0)
            Response.Redirect("blank.aspx");
        //------------------------------------------------------------------------------------------------------------
   
        if (Session["loginid"] == null)
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Default.aspx';", true);
        }
        if (Session["loginid"] != null)
        {
            ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 12);
            OBJ.Invoke();
            Session["Authority"] = OBJ.Authority;
            Auth = OBJ.Authority;
        }
        lblMessege.Text = "";
        if (!Page.IsPostBack)
        {
            LoadGroup();
            LoadFrequency();
            bindReports();
            ShowAllList();
        }
    }

    // --------  Events
    protected void btnSaveReport_OnClick(object sender, EventArgs e)
    {

        Common.Set_Procedures("dbo.sp_IU_VesselReportMaster");
        Common.Set_ParameterLength(4);
        Common.Set_Parameters(
            new MyParameter("@ReportId", ReportId),
            new MyParameter("@ReportName", txtReportName.Text.Trim()),
            new MyParameter("@GroupId", ddlGroup.SelectedValue),
            new MyParameter("@Frequency", ddlFreq.SelectedValue)
        );
        Boolean Res;
        DataSet Ds = new DataSet();
        Res = Common.Execute_Procedures_IUD(Ds);
        if (Res)
        {
            ReportId = Common.CastAsInt32(Ds.Tables[0].Rows[0][0]);
            //Common.Execute_Procedures_Select_ByQuery("DELETE FROM m_VesselReports_Vessels WHERE REPORTID=" + ReportId.ToString());
            //foreach (RepeaterItem ri in rpt_AllVessels.Items)
            //{
            //    CheckBox ch =(CheckBox )ri.FindControl("chkvsl");
            //    if (ch.Checked)
            //    {

            //        Common.Execute_Procedures_Select_ByQuery("INSERT INTO m_VesselReports_Vessels(VESSELID,REPORTID) values(" + ch.CssClass + "," + ReportId.ToString() + ")");
            //    }
            //}
            //ShowActualList();
            // trVessels.Visible = true;
            trVessels.Style.Add("display", "block"); 
            lblMessege.Text = "Data saved successfully.";
        }
        else
        {
            lblMessege.Text = "Data could not be saved.";
        }
   }
    protected void LoadFrequency()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("select * from [dbo].[KPI_Period]");
        ddlFreq.DataSource = dt;
        ddlFreq.DataTextField = "PeriodName";
        ddlFreq.DataValueField = "PeriodId";
        ddlFreq.DataBind();
        ddlFreq.Items.Insert(0, new ListItem("< Select >","0"));
    }
    protected void LoadGroup()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM [dbo].[m_VesselReportGroups]");
        ddlGroup.DataSource = dt;
        ddlGroup.DataTextField = "GroupName";
        ddlGroup.DataValueField = "GroupId";
        ddlGroup.DataBind();
        ddlGroup.Items.Insert(0, new ListItem("< Select >", "0"));
    }
    protected void btnAddReport_OnClick(object sender, EventArgs e)
    {
        ReportId = 0;
        txtReportName.Text = "";
        ddlFreq.SelectedIndex = 0;
        dvAddReport.Visible = true;
        ShowActualList();
    }
    protected void btnCloseAddReport_Click(object sender, EventArgs e)
    {
        bindReports();
        //trVessels.Visible = false;
        trVessels.Style.Add("display", "none"); //  block
        dvAddReport.Visible = false;
    }

    protected void Delete_Vessel(object sender, EventArgs e)
    {
        if (ReportId <= 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ade", "alert('Please save report first.');", true);
            return; 
        }

        int VesselId = Common.CastAsInt32(((ImageButton)(sender)).CommandArgument);
        Common.Execute_Procedures_Select_ByQuery("DELETE FROM m_VesselReports_Vessels where reportid=" + ReportId.ToString() + " AND VESSELID=" + VesselId.ToString());
        ShowActualList();
    }
    protected void Add_Vessel(object sender, EventArgs e)
    {
        if (ReportId <= 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ade", "alert('Please save report first.');", true);
            return;
        }

        int VesselId = Common.CastAsInt32(((ImageButton)(sender)).CommandArgument);
        Common.Execute_Procedures_Select_ByQuery("INSERT INTO m_VesselReports_Vessels(VESSELID,REPORTID) VALUES(" + VesselId.ToString() + "," + ReportId.ToString() + ")");
        ShowActualList();
    }
    protected void btnRemoveAll_OnClick(object sender, EventArgs e)
    {
        if (ReportId <= 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ade", "alert('Please save report first.');", true);
            return;
        }

        Common.Execute_Procedures_Select_ByQuery("DELETE FROM m_VesselReports_Vessels Where ReportId=" + ReportId.ToString());
        ShowActualList();
    }
    protected void btnApplytoAll_OnClick(object sender, EventArgs e)
    {
        if (ReportId <= 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ade", "alert('Please save report first.');", true);
            return;
        }

        Common.Execute_Procedures_Select_ByQuery("INSERT INTO m_VesselReports_Vessels(VESSELID,REPORTID) SELECT VESSELID," + ReportId.ToString() + " FROM DBO.VESSEL WHERE VESSELSTATUSID=1  AND VESSELID NOT IN (SELECT VESSELID FROM m_VesselReports_Vessels WHERE REPORTID=" + ReportId.ToString() +")");
        ShowActualList();
    }
    protected void ShowActualList()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("select VESSELID,VESSELNAME FROM DBO.VESSEL V WHERE V.VESSELID IN (SELECT VESSELID from m_VesselReports_Vessels where reportid=" + ReportId.ToString() + ") order by VESSELNAME");
        rptVList.DataSource = dt;
        rptVList.DataBind();
   
        ShowAllList();
     }
    protected void ShowAllList()
    {
        if (ReportId <= 0)
        {
            DataTable dt = Common.Execute_Procedures_Select_ByQuery("select VESSELID,VESSELNAME,'N' AS USED FROM DBO.VESSEL V WHERE VESSELSTATUSID=1 order by VESSELNAME");
            rpt_AllVessels.DataSource = dt;
            rpt_AllVessels.DataBind();
        }
        else 
        {
            DataTable dt = Common.Execute_Procedures_Select_ByQuery("select VESSELID,VESSELNAME FROM DBO.VESSEL V WHERE VESSELSTATUSID=1  AND V.VESSELID NOT IN (SELECT VESSELID from m_VesselReports_Vessels where reportid=" + ReportId.ToString() + ") order by VESSELNAME");
            rpt_AllVessels.DataSource = dt;
            rpt_AllVessels.DataBind();
        }
    }
    protected void BtnEdit_OnClick(object sender, EventArgs e)
    {
        ReportId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM m_VESSELREPORTS WHERE REPORTID=" + ReportId.ToString());
        if(dt.Rows.Count >0)
        {
            txtReportName.Text = dt.Rows[0]["ReportName"].ToString();
            ddlFreq.SelectedValue = dt.Rows[0]["Frequency"].ToString();
            ddlGroup.SelectedValue = dt.Rows[0]["GroupId"].ToString();
        }
        ShowActualList();
        //trVessels.Visible = true;
        trVessels.Style.Add("display", "block"); // none
        dvAddReport.Visible = true;
    }
    protected void btnDelete_OnClick(object sender, EventArgs e)
    {
        //ReportId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);

        //string sql = "DELETE FROM m_VESSELREPORTS WHERE ReportID=" + ReportId;
        //DataTable Dt = Common.Execute_Procedures_Select_ByQuery(sql);

        //lblMessege.Text = "Data deleted successfully.";
        //bindReports();
    }

    protected void GridView_InsGrp_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdReportsCode.PageIndex = e.NewPageIndex;
        grdReportsCode.SelectedIndex = -1;
        bindReports();
    }
    // --------  Finction
    public void bindReports()
    {
        string sql = "select row_number() over(order by ReportId )Row, ReportID, ReportName , PeriodName, GroupName from m_VESSELREPORTS R inner join KPI_PERIOD P ON P.PERIODID=R.FREQUENCY inner join m_VesselReportGroups G ON G.GROUPID=R.GROUPID ORDER BY GroupName,REPORTNAME ";

        DataTable Dt = Common.Execute_Procedures_Select_ByQuery(sql);
        if (Dt.Rows.Count > 0)
        {
            grdReportsCode.DataSource = Dt;
            grdReportsCode.DataBind();
        }

   
    }
}
