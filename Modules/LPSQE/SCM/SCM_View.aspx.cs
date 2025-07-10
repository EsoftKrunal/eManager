using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;
using System.IO;
using Ionic.Zip;
using System.Configuration;
using System.Text;

public partial class VIMS_SCM_View : System.Web.UI.Page
{
    public int ReportsPk
    {
        get{ return Common.CastAsInt32(ViewState["ReportsPk"]); }
        set{ ViewState["ReportsPk"] = value;}
    }
    public string Mode
    {
        get { return Convert.ToString(ViewState["Mode"]); }
        set { ViewState["Mode"] = value; }
    }
    public DataTable dtPresent
    {
        get
        {
            object o = ViewState["dtTempP"];
            return (DataTable)o;
        }
        set
        {
            ViewState["dtTempP"] = value;
        }

    }
    public DataTable dtAbsent
    {
        get
        {
            object o = ViewState["dtTempA"];
            return (DataTable)o;
        }
        set
        {
            ViewState["dtTempA"] = value;
        }

    }

    public int Chairman
    {
        get { return Common.CastAsInt32(ViewState["Chairman"]); }
        set { ViewState["Chairman"] = value; }
    }

    public int SO
    {
        get { return Common.CastAsInt32(ViewState["SO"]); }
        set { ViewState["SO"] = value; }
    }

    public int CM
    {
        get { return Common.CastAsInt32(ViewState["CM"]); }
        set { ViewState["CM"] = value; }
    }

    public DataTable dtCrewDesignation
    {
        get
        {
            object o = ViewState["dtCrewDesignation"];
            return (DataTable)o;
        }
        set
        {
            ViewState["dtCrewDesignation"] = value;
        }

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        lblMsg.Text = "";
        lblMsg_1.Text = "";
        lblMsg_2.Text = "";
        lblMsg_10.Text = "";

        if (!IsPostBack)
        {
            ReportsPk = Common.CastAsInt32(Request.QueryString["pk"]);
            Mode = Request.QueryString["Mode"].ToString();
            if (Mode == "A")
            {
                lblOccasion.Text = (Request.QueryString["OC"].ToString().Trim() == "M" ? "Monthly" : "NON-Routine");
            }
            txtShip.Text = Request.QueryString["VC"].ToString();
            BindTime();
            CreateTable();
            if (ReportsPk > 0)
            {
                ShowSCMData();
            }
            else
            {
                BindNCR();
            }
        }
    }

    private void BindTime()
    {
        for (int i = 0; i < 24; i++)
        {
            ddlDTCommenced_H.Items.Add(new ListItem(i.ToString().PadLeft(2, '0'), i.ToString().PadLeft(2, '0')));
            ddlPlaceCommenced_H.Items.Add(new ListItem(i.ToString().PadLeft(2, '0'), i.ToString().PadLeft(2, '0')));
        }

        for (int j = 0; j < 60; j++)
        {
            ddlDTCommenced_M.Items.Add(new ListItem(j.ToString().PadLeft(2, '0'), j.ToString().PadLeft(2, '0')));
            ddlPlaceCommenced_M.Items.Add(new ListItem(j.ToString().PadLeft(2, '0'), j.ToString().PadLeft(2, '0')));
        }
    }
    private void BindAllRanks()
    {
    
    }
    private void BindNCR()
    {
        string SQL = "SELECT S.ReportNo AS NUMBER, S.NCRTargetCompDate AS CDATE, S.NonConformanceDes AS REMARKS FROM [dbo].[ER_G118_Report] S " +
                     "LEFT JOIN [dbo].[ER_G118_Report_Office] O ON S.[ReportId] = O.[ReportId] AND S.[VesselCode] = O.[VesselCode] " +
                     "WHERE O.[Is_Closed] IS NULL AND S.[VesselCode] = '" + txtShip.Text.Trim() + "' ";
        
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
        
        rptNCR.DataSource = dt;
        rptNCR.DataBind();
    }
    public void CreateTable()
    {
        dtPresent = new DataTable();
        dtPresent.Columns.Add("RANK", typeof(string));
        dtPresent.Columns.Add("NAME", typeof(string));
        dtPresent.Columns.Add("DESGINATION", typeof(string)); 
        dtPresent.AcceptChanges();

        
        dtAbsent = new DataTable();
        dtAbsent.Columns.Add("RANK", typeof(string));
        dtAbsent.Columns.Add("NAME", typeof(string));
        dtAbsent.AcceptChanges();

        dtCrewDesignation = new DataTable();
        dtCrewDesignation.Columns.Add("DesignationName", typeof(string));
        dtCrewDesignation.Columns.Add("DesignationValue", typeof(string));
        dtCrewDesignation.AcceptChanges();

    }

    public void BindDesignation()
    {
        dtCrewDesignation.Clear();

        DataRow dr = dtCrewDesignation.NewRow();
        dr["DesignationName"] = "< SELECT >";
        dr["DesignationValue"] = "< SELECT >";

        dtCrewDesignation.Rows.Add(dr);

        if (Chairman == 0)
        {
            DataRow dr1 = dtCrewDesignation.NewRow();
            dr1["DesignationName"] = "Chairman";
            dr1["DesignationValue"] = "Chairman";

            dtCrewDesignation.Rows.Add(dr1);
        }
        if (SO == 0)
        {
            DataRow dr2 = dtCrewDesignation.NewRow();
            dr2["DesignationName"] = "Safety Officer";
            dr2["DesignationValue"] = "Safety Officer";

            dtCrewDesignation.Rows.Add(dr2);
        }
        if (CM == 0)
        {
            DataRow dr3 = dtCrewDesignation.NewRow();
            dr3["DesignationName"] = "Committee Member";
            dr3["DesignationValue"] = "Committee Member";

            dtCrewDesignation.Rows.Add(dr3);
        }

        DataRow dr4 = dtCrewDesignation.NewRow();
        dr4["DesignationName"] = "Elected Rep.(Officer)";
        dr4["DesignationValue"] = "Elected Rep.(Officer)";
        dtCrewDesignation.Rows.Add(dr4);

        DataRow dr5 = dtCrewDesignation.NewRow();
        dr5["DesignationName"] = "Elected Rep.(Rating)";
        dr5["DesignationValue"] = "Elected Rep.(Rating)";
        dtCrewDesignation.Rows.Add(dr5);

        DataRow dr6 = dtCrewDesignation.NewRow();
        dr6["DesignationName"] = "Attendee";
        dr6["DesignationValue"] = "Attendee";
        dtCrewDesignation.Rows.Add(dr6);
    }

    public DataTable BindCrewDesignation()
    {                            
        return dtCrewDesignation;
    }

    private void ShowNCR()
    {
        string SQL = "SELECT * FROM [dbo].[SCM_NCRDETAILS] WHERE [ReportsPK] = " + ReportsPk + "  AND [VesselCode] = '" + txtShip.Text.Trim() + "' ";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);

        rptNCR.DataSource = dt;
        rptNCR.DataBind();
    }

    protected void btnMenu_Click(object sender, EventArgs e)
    {
        int Id = Common.CastAsInt32(((Button)sender).CommandArgument);

        ClearMenuSelection();

        switch (Id)
        {
            case 1 : 
                      DPerformance.Visible = true;                      
                      //btnMGT1.CssClass = "color_tab_sel";
                      break;
            case 2 : 
                      DAssessmentofCompetencies.Visible = true;                      
                      //btnMGT2.CssClass = "color_tab_sel";
                      break;
            case 3 : 
                      DPotAssesment.Visible = true;
                      //btnMGT3.CssClass = "color_tab_sel";
                      break;
            case 4 : 
                      DAppraiseeRemarks.Visible = true;
                      //btnMGT4.CssClass = "color_tab_sel";
                      break;
            case 5 : 
                      DCompliance.Visible = true;
                      //btnMGT5.CssClass = "color_tab_sel";
                      break;
            case 6 : 
                      DNCR.Visible = true;
                      //btnMGT6.CssClass = "color_tab_sel";
                      break;
            case 7 : 
                      DMooringNOther.Visible = true;
                      //btnMGT7.CssClass = "color_tab_sel";
                      break;
            
            case 9 : 
                      DHome.Visible = true;
                      //btnMGT9.CssClass = "color_tab_sel";
                      break;
            case 10 : 
                      DBestPractice.Visible = true;
                      //btnMGT10.CssClass = "color_tab_sel";
                      break;
            case 11:
                      DBOfficeComments.Visible = true;
                      btnMGT11.CssClass = "color_tab_sel";
                      break;

        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        //if (!ValidateSections(9) || !ValidateSections(1) || !ValidateSections(2))
        //{
        //    return;
        //}

        if (txtOfficeComments.Text.Trim() == "")
        {
            lblMsg_10.Text = "Please enter office comments.";
            txtOfficeComments.Focus();
            return;
        }


        try
        {
            Common.Execute_Procedures_Select_ByQuery("UPDATE dbo.SCM_Master SET OfficeComments = '" + txtOfficeComments.Text.Replace("'", "`").Trim() + "',UpdatedBy='" + Session["UserName"].ToString() + "',UpdatedOn='" + DateTime.Today.ToString("dd-MMM-yyyy") + "'  WHERE ReportsPk=" + ReportsPk + " AND VesselCode='" + txtShip.Text + "' ");
            ShowSCMData();
            lblMsg_10.Text = "Record saved successfully.";
        }
        catch (Exception ex)
        {
            lblMsg_10.Text = "Unable to save record.Error : " + ex.Message;
        }
    }

    protected void btnNext_Click(object sender, EventArgs e)
    { 
        int Id = Common.CastAsInt32(((Button)sender).CommandArgument);

        if (!ValidateSections(Id))
        {
            return;
        }

        ClearMenuSelection();

        switch (Id)
        {
            case 1:
                DBOfficeComments.Visible = true;
                btnMGT11.CssClass = "color_tab_sel";
                //DAssessmentofCompetencies.Visible = true;
                //dv_Safety.Attributes.Add("class", "color_tab_sel");
                //btnMGT2.CssClass = "color_tab_sel";
                break;
            case 2:
                    DPotAssesment.Visible = true;
                    dv_Health.Attributes.Add("class", "color_tab_sel");
                    //btnMGT3.CssClass = "color_tab_sel";
                    break;
                
            case 3:
                    DAppraiseeRemarks.Visible = true;
                    dv_Security.Attributes.Add("class", "color_tab_sel");
                    //btnMGT4.CssClass = "color_tab_sel";
                    break;
                
            case 4:
                    DCompliance.Visible = true;
                    dv_Quality.Attributes.Add("class", "color_tab_sel");
                    //btnMGT5.CssClass = "color_tab_sel";
                    break;
                
            case 5:
                    DNCR.Visible = true;
                    dv_Env.Attributes.Add("class", "color_tab_sel");
                    //btnMGT6.CssClass = "color_tab_sel";
                    break;
                
            case 6:
                    DMooringNOther.Visible = true;
                    dv_AOB.Attributes.Add("class", "color_tab_sel");
                    //btnMGT7.CssClass = "color_tab_sel";
                    break;
                
            case 7:
                    DBestPractice.Visible = true;
                    dv_BP.Attributes.Add("class", "color_tab_sel");
                    //btnMGT10.CssClass = "color_tab_sel";
                    break;            
            case 9:
                //DPerformance.Visible = true;
                //dv_Start.Attributes.Add("class", "color_tab_sel");
                DBOfficeComments.Visible = true;
                btnMGT11.CssClass = "color_tab_sel";
                //btnMGT1.CssClass = "color_tab_sel";
                break;
            case 10:
                    DBOfficeComments.Visible = true;
                    btnMGT11.CssClass = "color_tab_sel";
                    break;
        }
    }
    
    public void ShowSCMData()
    {
        string sql = "select [ReportsPK],[VesselCode],replace(convert(varchar, SDate,106),' ','-') AS SDate,[ShipPosFrom],[ShipPosTo],[CommTime],[CompTime], " +
                     "[Ocassion],[ShipPosition],[Str_MinOfPrevSaFeCom],[Str_AbsPrevSageCom],[Str_OffCommPrevSafeCom],[Sef_AvailableOnBoard],[Sef_AccidentNarMiss], " +
                     "[Sef_ReviewOfMooring],[Sef_CrewNotFamilierWithAll],[Hel_ReviewHelth],[Sec_ReviewAnyImmediateSecurity],[Qul_ReviewOfRegulatory],[Qul_ReviewOfQuality], " +
                     "[Qul_KPI],[Qul_CrewWelfare],[Env_EnvironmentalKPIs],[AnyOtherIssues], [OUTSTANDINGITEMS],[ReceviedOn],[OfficeComments],[BestPractice],UpdatedBy +' / '+ Replace( convert(varchar,UpdatedOn,106),' ','-') UpdateByOn, DocName " +
                     "from dbo.SCM_Master with(nolock) where ReportsPk=" + ReportsPk + " AND VesselCode='" + txtShip.Text + "' ";

        DataTable DT = Common.Execute_Procedures_Select_ByQuery(sql);

        if (DT.Rows.Count > 0)
        {
            lblOccasion.Text = (DT.Rows[0]["Ocassion"].ToString().Trim() == "M" ? "Routine" : "NON-Routine");
            txtDate.Text = DT.Rows[0]["SDate"].ToString();
            ddlShipPosition.SelectedValue = DT.Rows[0]["ShipPosition"].ToString();
            ddlShipPosition_Click(new object(), new EventArgs());
            txtShipPosFrom.Text = DT.Rows[0]["ShipPosFrom"].ToString();
            txtShipPosTo.Text = DT.Rows[0]["ShipPosTo"].ToString();
            ddlDTCommenced_H.SelectedValue = DT.Rows[0]["CommTime"].ToString().Split(':').GetValue(0).ToString();
            ddlDTCommenced_M.SelectedValue = DT.Rows[0]["CommTime"].ToString().Split(':').GetValue(1).ToString();
            ddlPlaceCommenced_H.SelectedValue = DT.Rows[0]["CompTime"].ToString().Split(':').GetValue(0).ToString();
            ddlPlaceCommenced_M.SelectedValue = DT.Rows[0]["CompTime"].ToString().Split(':').GetValue(1).ToString();
            
            
            rMinutesofPreviousYes.Checked = (DT.Rows[0]["Str_MinOfPrevSaFeCom"].ToString() == "True");
            rMinutesofPreviousNo.Checked = (DT.Rows[0]["Str_MinOfPrevSaFeCom"].ToString() == "False");
            rAbsenteesYes.Checked = (DT.Rows[0]["Str_AbsPrevSageCom"].ToString() == "True");
            rAbsenteesNo.Checked = (DT.Rows[0]["Str_AbsPrevSageCom"].ToString() == "False");
            rOfficeCommentsYes.Checked = (DT.Rows[0]["Str_OffCommPrevSafeCom"].ToString() == "True");
            rOfficeCommentsNo.Checked = (DT.Rows[0]["Str_OffCommPrevSafeCom"].ToString() == "False");
            rOutstandingItemYes.Checked = (DT.Rows[0]["OUTSTANDINGITEMS"].ToString() != "");
            rOutstandingItemNo.Checked = (DT.Rows[0]["OUTSTANDINGITEMS"].ToString() == "");
            txtOutstandingItems.Text = DT.Rows[0]["OUTSTANDINGITEMS"].ToString();
            OutstandingItem_OnCheckedChanged(new object(), new EventArgs());             

            rAvailableYes.Checked = (DT.Rows[0]["Sef_AvailableOnBoard"].ToString() == "");
            rAvailableNo.Checked = (DT.Rows[0]["Sef_AvailableOnBoard"].ToString() != "");
            txtCrewAvailable.Text = DT.Rows[0]["Sef_AvailableOnBoard"].ToString();
            CrewAvailableOnBoard_OnCheckedChanged(new object(), new EventArgs()); 
            //txtCrewAvailable.Style.Add("display", (rAvailableNo.Checked ? "block" : "none"));
            
            txtAnyAccident.Text = DT.Rows[0]["Sef_AccidentNarMiss"].ToString();
            txtAnyNearMiss.Text = DT.Rows[0]["Sef_ReviewOfMooring"].ToString();

            rFamiliarYes.Checked = (DT.Rows[0]["Sef_CrewNotFamilierWithAll"].ToString() == "");
            rFamiliarNo.Checked = (DT.Rows[0]["Sef_CrewNotFamilierWithAll"].ToString() != "");
            txtCrewFamiliar.Text = DT.Rows[0]["Sef_CrewNotFamilierWithAll"].ToString();
            //txtCrewFamiliar.Style.Add("display", (rFamiliarNo.Checked ? "block" : "none"));
            CrewFamiliarWithAll_OnCheckedChanged(new object(), new EventArgs()); 

            txtReviewHelth.Text = DT.Rows[0]["Hel_ReviewHelth"].ToString();
            txtReviewAnyImmediateSecurity.Text = DT.Rows[0]["Sec_ReviewAnyImmediateSecurity"].ToString();
            txtReviewOfRegulatoryCompliance.Text = DT.Rows[0]["Qul_ReviewOfRegulatory"].ToString();
            txtReviewOfQuality.Text = DT.Rows[0]["Qul_ReviewOfQuality"].ToString();
            txtReviewAllCompanyKPI.Text = DT.Rows[0]["Qul_KPI"].ToString();
            txtReviewCrewWelfare.Text = DT.Rows[0]["Qul_CrewWelfare"].ToString();
            ShowNCR(); 
            txtReviewEnvironmentalKPIs.Text = DT.Rows[0]["Env_EnvironmentalKPIs"].ToString();
            txtAnyOtherIssues.Text = DT.Rows[0]["AnyOtherIssues"].ToString();
            txtBestPractice.Text = DT.Rows[0]["BestPractice"].ToString();
            txtOfficeComments.Text = DT.Rows[0]["OfficeComments"].ToString();
            if (Convert.ToString(DT.Rows[0]["DocName"]) != "")
            {
                ImgAttachment.Visible = true;
            }
            else
            {
                ImgAttachment.Visible = false;
            }
            lblUpdatedByOn.Text = (DT.Rows[0]["UpdateByOn"].ToString().Trim() == "" ? DT.Rows[0]["UpdateByOn"].ToString() : " [ " + DT.Rows[0]["UpdateByOn"].ToString() + " ]");


            sql = "select RankName AS RANK,Name,Remarks AS DESGINATION from dbo.SCM_rankdetails where ReportsPk=" + ReportsPk + " AND VesselCode='" + txtShip.Text + "'  and Absent=0 ";

            dtPresent = Common.Execute_Procedures_Select_ByQuery(sql);

            rptPresent.DataSource = dtPresent;
            rptPresent.DataBind();


            sql = "select RankName AS RANK,Name from dbo.SCM_rankdetails where ReportsPk=" + ReportsPk + " AND VesselCode='" + txtShip.Text + "'  and Absent=1 ";

            dtAbsent = Common.Execute_Procedures_Select_ByQuery(sql);

            rptAbsent.DataSource = dtAbsent;
            rptAbsent.DataBind();

            btnSave.Visible = (DT.Rows[0]["UpdateByOn"].ToString() == "");
            btnExport.Visible = (DT.Rows[0]["UpdateByOn"].ToString() != "");
        }
    }

    protected void ddlShipPosition_Click(object sender, EventArgs e)
    {
        if (ddlShipPosition.SelectedIndex == 0)
        {
            lblPosition.Text = "Voy From/To :";
            txtShipPosTo.Visible = true;
            txtShipPosTo.Text = "";
        }
        else
        {
            lblPosition.Text = "Port/Anchorage :";
            txtShipPosTo.Visible = false;
            txtShipPosTo.Text = "";
        }
    }
    
    public bool ValidateSections(int SectionId)
    {
        return true;
        int cnt = 0;

        if (SectionId == 1)
        {
            if (rMinutesofPreviousYes.Checked || rMinutesofPreviousNo.Checked)
                cnt = cnt + 1;
            if (rAbsenteesYes.Checked || rAbsenteesNo.Checked)
                cnt = cnt + 1;
            if (rOfficeCommentsYes.Checked || rOfficeCommentsNo.Checked)
                cnt = cnt + 1;
            if (rOutstandingItemYes.Checked || rOutstandingItemNo.Checked)
                cnt = cnt + 1;

            if (cnt < 4)
            {
                lblMsg_1.Text = "Please fill all the fields.";
                //MoveToTab(SectionId);
                return false;
            }
        }

        if (SectionId == 2)
        {
          cnt = 0;
          if (rAvailableYes.Checked || rAvailableNo.Checked)
              cnt = cnt + 1;
          if (rFamiliarYes.Checked || rFamiliarNo.Checked)
              cnt = cnt + 1;

          if (cnt < 2) {
              lblMsg_2.Text = "Please fill all the fields.";               
              //MoveToTab(SectionId);
              return false;
          }
        }

        if (SectionId == 9)
        {
            if (txtDate.Text == "")
            {
                lblMsg.Text = "Please enter report date.";
                txtDate.Focus();
                //MoveToTab(SectionId);
                return false;
            }

            DateTime dt;

            if (!DateTime.TryParse(txtDate.Text.Trim(), out dt))
            {
                lblMsg.Text = "Please enter valid report date.";
                txtDate.Focus();
                //MoveToTab(SectionId);
                return false;
            }

            if ((Convert.ToDateTime(txtDate.Text.Trim()).Month != DateTime.Today.Month) || Convert.ToDateTime(txtDate.Text.Trim()).Year != DateTime.Today.Year)
            {
                lblMsg.Text = "Report date must be in current month.";
                txtDate.Focus();
                //MoveToTab(SectionId);
                return false;
            }

            if (Convert.ToDateTime(txtDate.Text.Trim()).Date >  DateTime.Today.Date)
            {
                lblMsg.Text = "Report date can not be more than today.";
                txtDate.Focus();
                //MoveToTab(SectionId);
                return false;
            }

            if (ddlDTCommenced_H.SelectedIndex == 0)
            {
                lblMsg.Text = "Please select commenced hour.";
                ddlDTCommenced_H.Focus();
                //MoveToTab(SectionId);
                return false;
            }

            if (ddlDTCommenced_M.SelectedIndex == 0)
            {
                lblMsg.Text = "Please select commenced Minutes.";
                ddlDTCommenced_M.Focus();
                //MoveToTab(SectionId);
                return false;
            }

            if (ddlPlaceCommenced_H.SelectedIndex == 0)
            {
                lblMsg.Text = "Please select completed hour.";
                ddlPlaceCommenced_H.Focus();
                //MoveToTab(SectionId);
                return false;
            }

            if (ddlPlaceCommenced_M.SelectedIndex == 0)
            {
                lblMsg.Text = "Please select completed Minutes.";
                ddlPlaceCommenced_M.Focus();
                //MoveToTab(SectionId);
                return false;
            }

            if (rptPresent.Items.Count < 5)
            {
                lblMsg.Text = "Please import crew list.";
                //MoveToTab(SectionId);
                return false;
            }

            //int CheckedCounter = 0;
            //for (int i = 0; i <= 24; i++)
            //{
            //    DropDownList ddlHRank = (DropDownList)tblPresent.Rows[0].FindControl("ddlHRank" + (i + 1));
            //    TextBox txtHName = (TextBox)tblPresent.Rows[0].FindControl("txtHName" + (i + 1));

            //    if (ddlHRank.SelectedIndex != 0 && txtHName.Text.Trim() != "")
            //    {
            //        CheckedCounter = CheckedCounter + 1;
            //    }

            //    if (i > 5)
            //    {
            //        break;
            //    }
            //}
            //if (CheckedCounter < 5)
            //{
            //    lblMsg.Text = "Top 5 Persons (Designation) must be present in SCM meeting.";
            //    return false;
            //}
        }

        return true;
    }


    public void MoveToTab(int TabId)
    {
        ClearMenuSelection();

        switch (TabId)
        {
            case 1:
                DPerformance.Visible = true;
                //btnMGT1.CssClass = "color_tab_sel";
                break;
            case 2:
                DAssessmentofCompetencies.Visible = true;
                //btnMGT2.CssClass = "color_tab_sel";
                break;
            case 9:                
                DHome.Visible = true;                
                //btnMGT9.CssClass = "color_tab_sel";
                break;
        }
    }
    public void ClearMenuSelection()
    {
        //btnMGT1.CssClass = "color_tab";
        //btnMGT2.CssClass = "color_tab";
        //btnMGT3.CssClass = "color_tab";
        //btnMGT4.CssClass = "color_tab";
        //btnMGT5.CssClass = "color_tab";
        //btnMGT6.CssClass = "color_tab";
        //btnMGT7.CssClass = "color_tab";
        //btnMGT8.CssClass = "color_tab";
        //btnMGT9.CssClass = "color_tab";
        //dv_Home.Attributes.Add("class", "color_tab");
        //btnMGT10.CssClass = "color_tab";
        //btnMGT11.CssClass = "color_tab";

        dv_Home.Attributes.Add("class", "color_tab");
        dv_Start.Attributes.Add("class", "color_tab");
        dv_Safety.Attributes.Add("class", "color_tab");
        dv_Health.Attributes.Add("class", "color_tab");
        dv_Security.Attributes.Add("class", "color_tab");
        dv_Quality.Attributes.Add("class", "color_tab");
        dv_Env.Attributes.Add("class", "color_tab");
        dv_AOB.Attributes.Add("class", "color_tab");
        dv_BP.Attributes.Add("class", "color_tab");
        btnMGT11.CssClass = "color_tab";

        DPerformance.Visible = false;
        DPotAssesment.Visible = false;
        DAppraiseeRemarks.Visible = false;
        DAssessmentofCompetencies.Visible = false;
        DCompliance.Visible = false;
        DNCR.Visible = false;
        DMooringNOther.Visible = false;
        DSUPTD.Visible = false;
        DHome.Visible = false;
        DBestPractice.Visible = false;
        DBOfficeComments.Visible = false;
    }

    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        int Id = Common.CastAsInt32(((Button)sender).CommandArgument);

        ClearMenuSelection();

        switch (Id)
        {
            case 1:
                DPerformance.Visible = true;
                dv_Start.Attributes.Add("class", "color_tab_sel");
                //btnMGT1.CssClass = "color_tab_sel";
                break;
            case 2:
                DAssessmentofCompetencies.Visible = true;
                dv_Safety.Attributes.Add("class", "color_tab_sel");
                //btnMGT2.CssClass = "color_tab_sel";
                break;
            case 3:
                DPotAssesment.Visible = true;
                dv_Health.Attributes.Add("class", "color_tab_sel");
                //btnMGT3.CssClass = "color_tab_sel";
                break;
            case 4:
                DAppraiseeRemarks.Visible = true;
                dv_Security.Attributes.Add("class", "color_tab_sel");
                //btnMGT4.CssClass = "color_tab_sel";
                break;
            case 5:
                DCompliance.Visible = true;
                dv_Quality.Attributes.Add("class", "color_tab_sel");
                //btnMGT5.CssClass = "color_tab_sel";
                break;
            case 6:
                DNCR.Visible = true;
                dv_Env.Attributes.Add("class", "color_tab_sel");
                //btnMGT6.CssClass = "color_tab_sel";
                break;
            case 7:
                DMooringNOther.Visible = true;
                dv_AOB.Attributes.Add("class", "color_tab_sel");
                //btnMGT7.CssClass = "color_tab_sel";
                break;
            case 9:
                DHome.Visible = true;
                //btnMGT9.CssClass = "color_tab_sel";
                dv_Home.Attributes.Add("class", "color_tab_sel");
                break;
            case 10:
                DHome.Visible = true;
                //btnMGT9.CssClass = "color_tab_sel";
                dv_Home.Attributes.Add("class", "color_tab_sel");
                //DBestPractice.Visible = true;
                //dv_BP.Attributes.Add("class", "color_tab_sel");
                //btnMGT10.CssClass = "color_tab_sel";
                break;
            case 11:
                DBOfficeComments.Visible = true;
                btnMGT11.CssClass = "color_tab_sel";
                break;

        }
    }

    protected void OutstandingItem_OnCheckedChanged(object sender, EventArgs e)
    {
        if (rOutstandingItemYes.Checked)
        {
            OutstandingItem.Visible = true;
        }
        else
        {
            OutstandingItem.Visible = false;
        }
    }

    protected void CrewAvailableOnBoard_OnCheckedChanged(object sender, EventArgs e)
    {
        if (rAvailableYes.Checked)
        {
            txtCrewAvailable.Text = "";
            txtCrewAvailable.Visible = false;
        }
        else
        {
            txtCrewAvailable.Visible = true;
        }
    }

    protected void CrewFamiliarWithAll_OnCheckedChanged(object sender, EventArgs e)
    {
        if (rFamiliarYes.Checked)
        {
            txtCrewFamiliar.Text = "";
            txtCrewFamiliar.Visible = false;
        }
        else
        {
            txtCrewFamiliar.Visible = true;
        }
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        string SQL = "SELECT * FROM DBO.SCM_Master with(nolock) WHERE ReportsPK = " + ReportsPk + " AND VesselCode='" + txtShip.Text.Trim() + "'";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
        dt.TableName = "SCM_Master";
        ds.Tables.Add(dt.Copy());

        string SchemaFile = Server.MapPath("~/Modules/LPSQE/TEMP/SCMSchema.xml");
        string DataFile = Server.MapPath("~/Modules/LPSQE/TEMP/SCMData.xml");
        string ZipFile = Server.MapPath("~/Modules/LPSQE/TEMP/SCM_O_" + txtShip.Text.Trim() + "_" + ReportsPk + ".zip");

        ds.WriteXmlSchema(SchemaFile);
        ds.WriteXml(DataFile);

        using (ZipFile zip = new ZipFile())
        {
            zip.AddFile(SchemaFile);
            zip.AddFile(DataFile);
            zip.Save(ZipFile);
        }

        DataTable dtEMAIL = Common.Execute_Procedures_Select_ByQuery("SELECT Email,VesselEmailNew,VesselCode FROM [dbo].[Vessel] with(nolock) WHERE VesselCode='" + txtShip.Text.Trim() + "'");
        if (dtEMAIL.Rows.Count > 0)
        {
            //byte[] buff = System.IO.File.ReadAllBytes(ZipFile);
            //Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(ZipFile));
            //Response.BinaryWrite(buff);
            //Response.Flush();
            //Response.End();

            string ToEmail = ((dtEMAIL != null & dtEMAIL.Rows.Count > 0) ? dtEMAIL.Rows[0]["VesselEmailNew"].ToString() : "");
            string selfemail=ProjectCommon.getUserEmailByID(Session["loginid"].ToString());

            
            List<string> CCEmails = new List<string>();
            string CCEmail = ((dtEMAIL != null & dtEMAIL.Rows.Count > 0) ? dtEMAIL.Rows[0]["Email"].ToString() : "");
            if (!string.IsNullOrEmpty(CCEmail))
            {
                CCEmails.Add(CCEmail);
            }
            if (!string.IsNullOrEmpty(selfemail))
            {
                CCEmails.Add(selfemail);
            }

            string[] NoEmails = { };
            string Subject = "SCM Report";
            string error = "";
            string fromAddress = ConfigurationManager.AppSettings["FromAddress"];
            StringBuilder sb = new StringBuilder();
            sb.Append("Dear Captain, ");
            sb.Append("***********************************************************************");
            sb.Append("<br/>");
            sb.Append("<br/>");
            sb.Append("<b>Please import the attached file for Office reply for SCM " + DateTime.Parse(txtDate.Text).ToString("MMM-yyyy") + ".</b>");
            sb.Append("<br/>");
            sb.Append("<br/>");
            sb.Append("If you find any discrepancy please inform to emanager@energiossolutions.com");
            sb.Append("<br/>");
            sb.Append("Thank You, <br/>");
            sb.Append("***********************************************************************");
            sb.Append("<br/>");
            sb.Append("<i>Do not reply to this email as we do not monitor it.</i><br/>");
            string result = SendMail.SendSimpleMail(fromAddress, ToEmail, CCEmails.ToArray(), NoEmails.ToArray(), Subject, sb.ToString(), ZipFile);

            if (result == "SENT")
            {
                lblMsg_10.Text = "Mail sent successfully";
            }
            else
            {
                lblMsg_10.Text = "Unable to send mail. Error" + error;
            }
        }
    }
    protected void ImgAttachment_Click(object sender, EventArgs e)
    {
        if (ReportsPk > 0)
        {
            string sql = "SELECT top 1 DocName As FileName,Attachment,ContentType FROM [SCM_MASTER] with(nolock) WHERE [VesselCode] = '" + txtShip.Text.Trim() + "' AND  ReportsPK =" + ReportsPk;
            DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);

            if (dt.Rows.Count > 0)
            {
                try
                {
                    string contentType = "";
                    string FileName = "";

                    if (!string.IsNullOrWhiteSpace(dt.Rows[0]["ContentType"].ToString()))
                    {
                        contentType = dt.Rows[0]["ContentType"].ToString();
                    }
                    if (!string.IsNullOrWhiteSpace(dt.Rows[0]["FileName"].ToString()))
                    {
                        FileName = dt.Rows[0]["FileName"].ToString();
                    }
                    if (!string.IsNullOrWhiteSpace(contentType))
                    {

                        byte[] latestFileContent = (byte[])dt.Rows[0]["Attachment"];
                        Response.Clear();
                        Response.Buffer = true;
                        Response.Charset = "";
                        Response.Cache.SetCacheability(HttpCacheability.NoCache);
                        Response.ContentType = contentType;
                        Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileName);
                        Response.BinaryWrite(latestFileContent);
                        Response.Flush();
                        Response.End();
                    }

                }
                catch (Exception ex)
                {

                    Response.Clear();
                    Response.Write("<center> Invalid File !</center>");
                    Response.End();
                }
            }
        }
    }
}