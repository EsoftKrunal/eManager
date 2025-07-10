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
using ShipSoft.CrewManager.BusinessObjects;
using ShipSoft.CrewManager.BusinessLogicLayer;
using ShipSoft.CrewManager.Operational;

public partial class Case : System.Web.UI.Page
{
    public Authority Auth;
    Button btn = new Button();
    //string SessionDeleimeter = ",";
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        //--------------------------------- PAGE ACCESS AUTHORITY ----------------------------------------------------
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 312);
        if (chpageauth <= 0)
            Response.Redirect("blank.aspx");
        //------------------------------------------------------------------------------------------------------------

        if (Session["loginid"] == null)
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Default.aspx';", true);
        }

        lblmessage.Text = "";
        if (Session["loginid"] != null)
        {
            ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 235);
            OBJ.Invoke();
            Session["Authority"] = OBJ.Authority;
            Auth = OBJ.Authority;
            //btnNewVisit.Visible = Auth.isAdd;
        }
        if (!Page.IsPostBack)
        {
            BindOwner();
            BindGroups();
            BindVessel();
            BindUW();
            chkAllGroups.Items[0].Selected = true;
            chkallgp1(null, null);
            
            btnSearch_Click(null, null);
        }
        //try
        //{
        //    Alerts.HANDLE_AUTHORITY(1, btn, btn, btn, btn, Auth);
        //}
        //catch
        //{
        //    Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Default.aspx';", true);
        //}


    }
    #region ----------------- UDF -----------------------

    public void BindGroups()
    {
        try
        {
            DataTable dtGroups = Budget.getTable("SELECT GroupId,GroupName,ShortName FROM IRM_Groups ORDER BY GroupName").Tables[0];
            this.chkGroups.DataSource = dtGroups;
            this.chkGroups.DataValueField = "GroupId";
            this.chkGroups.DataTextField = "ShortName";
            this.chkGroups.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    public void BindSubGroups()
    {
        string GroupIds = "";
        foreach (ListItem lst in chkGroups.Items)
        {
            if (lst.Selected)
            {
                GroupIds = GroupIds + lst.Value + ",";
            }
        }
        if (GroupIds.Trim().Length > 1)
        {
            try
            {
                GroupIds = GroupIds.Remove(GroupIds.Length - 1);
                DataTable dtSubGroups = Budget.getTable("SELECT SubGroupId,SubGroupName FROM IRM_SubGroups WHERE GroupId IN (" + GroupIds.Trim() + ")").Tables[0];
                this.chkSubGroups.DataSource = dtSubGroups;
                this.chkSubGroups.DataValueField = "SubGroupId";
                this.chkSubGroups.DataTextField = "SubGroupName";
                this.chkSubGroups.DataBind();

                foreach (ListItem lst in chkSubGroups.Items)
                {
                    lst.Selected = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        else
        {
            chkSubGroups.Items.Clear();
        }

    }
    
    public void BindVessel()
    {
        try
        {
            DataTable dt=new DataTable();
            if (rdoOwnerORFleet.SelectedIndex == 0)
            {
                if (ddlFleet.SelectedIndex != 0)
                {
                    if (chkInactiveVessels.Checked)
                        dt = Common.Execute_Procedures_Select_ByQuery("select VESSELNAME,VESSELID FROM dbo.vessel where FleetID=" + ddlFleet.SelectedValue + " order by vesselname");
                    else
                        dt = Common.Execute_Procedures_Select_ByQuery("select VESSELNAME,VESSELID FROM dbo.vessel where FleetID=" + ddlFleet.SelectedValue + "  and VesselStatusid<>2  order by vesselname");
                }
                else
                {
                    if (chkInactiveVessels.Checked)
                        dt = Common.Execute_Procedures_Select_ByQuery("select VESSELNAME,VESSELID FROM dbo.vessel order by vesselname");
                    else
                        dt = Common.Execute_Procedures_Select_ByQuery("select VESSELNAME,VESSELID FROM dbo.vessel where VesselStatusid<>2  order by vesselname");
                }
            }
            else
            {
                if (ddlOwner.SelectedIndex != 0)
                {
                    if (chkInactiveVessels.Checked)
                        dt = Common.Execute_Procedures_Select_ByQuery("select VESSELNAME,VESSELID FROM dbo.vessel where OwnerID=" + ddlOwner.SelectedValue + " order by vesselname");
                    else
                        dt = Common.Execute_Procedures_Select_ByQuery("select VESSELNAME,VESSELID FROM dbo.vessel where OwnerID=" + ddlOwner.SelectedValue + "  and VesselStatusid<>2  order by vesselname");
                }
                else
                {
                    if (chkInactiveVessels.Checked)
                        dt = Common.Execute_Procedures_Select_ByQuery("select VESSELNAME,VESSELID FROM dbo.vessel order by vesselname");
                    else
                        dt = Common.Execute_Procedures_Select_ByQuery("select VESSELNAME,VESSELID FROM dbo.vessel where VesselStatusid<>2  order by vesselname");
                }
            }
            

            ddlvessels.Controls.Clear();
            this.ddlvessels.DataTextField = "VesselName";
            this.ddlvessels.DataValueField = "VesselId";
            this.ddlvessels.DataSource = dt;
            this.ddlvessels.DataBind();
            this.ddlvessels.Items.Insert(0, new ListItem("All", "0"));
            this.ddlvessels.Items[0].Value = "0";
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public void BindUW()
    {
        string strUW = "SELECT UWId,UWName,ShortName FROM IRM_UWMaster ORDER BY UWName ";
        DataTable dtUW = Budget.getTable(strUW).Tables[0];
        ddlAllUW.DataSource = dtUW;
        ddlAllUW.DataTextField = "ShortName";
        ddlAllUW.DataValueField = "UWId";
        ddlAllUW.DataBind();
        ddlAllUW.Items.Insert(0,new ListItem("All","0"));

    }
    protected void BindOwner()
    {
        try
        {
            this.ddlOwner.DataTextField = "OwnerName";
            this.ddlOwner.DataValueField = "OwnerId";
            this.ddlOwner.DataSource = Inspection_Master.getMasterDataforInspection("Owner", "OwnerId", "OwnerName");
            this.ddlOwner.DataBind();
            this.ddlOwner.Items.Insert(0, new ListItem("All", "0"));
            this.ddlOwner.Items[0].Value = "0";
            this.ddlvessels.Items.Insert(0, new ListItem("All", "0"));
            this.ddlvessels.Items[0].Value = "0";
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion ---------------------------------------------

    #region ----------------- EVENTS -----------------------

    protected void chkallgp1(object sender, EventArgs e)
    {
        int i = 0;
        for (i = 0; i < chkGroups.Items.Count; i++)
        {
            if (chkAllGroups.Items[0].Selected == true)            
                chkGroups.Items[i].Selected = true;
            else
                chkGroups.Items[i].Selected = false;
           chkAllSubGroups.Items[0].Selected = false;
           BindSubGroups();
        }
    }
    protected void chakallinsp(object sender, EventArgs e)
    {
        int i = 0;
        for (i = 0; i < chkSubGroups.Items.Count; i++)
        {
            if (chkAllSubGroups.Items[0].Selected == true)
                chkSubGroups.Items[i].Selected = true;
            else
                chkSubGroups.Items[i].Selected = false;
        }
    }
    protected void chkInactiveVessels_CheckedChanged(object sender, EventArgs e)
    {
        BindVessel();
    }
    protected void chkGroups_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindSubGroups();
    }
    
    //protected void chkDue_CheckedChanged(object sender, EventArgs e)
    //{
    //    if (chkDue.Checked)
    //    {
    //        if (txtDueDays.Text.Trim() == "")
    //            txtDueDays.Text = "60";
           
    //    }
    //    else
    //        txtDueDays.Text = "";


    //    txtFromDt.Text = "";
    //    txtToDt.Text = "";
    //}
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string SearchSQL = " SELECT CM.CaseID " +
                          " ,(SELECT VesselName FROM dbo.Vessel WHERE VesselId = CM.VesselId)VesselName " +
                          " ,CM.PolicyNo " +
                          " ,CM.GroupID " +
                          " ,GP.GroupName " +
                          " ,UWM.UWID " +
                          " ,UWM.ShortName" +
                          " ,substring(CM.IncidentDate,0,12)IncidentDate1 " +
                          " ,CM.IncidentDesc " +
                          " ,CM.ClaimAmount " +
                          " ,CM.ExpDetail " +
                          " ,CM.ExpAmount " +
                          " ,CM.ExpLoacalCurr " +
                          " ,CM.ExpTotalUSDoler " +
                          " ,CM.ExpClaimed " +
                          " ,CM.ClaimDate " +
                          " ,CM.CaseNumber " +
                          " ,CM.CompanyCaseNumber " +
                          //" ,(select sum(TotalUSDoler) from IRM_CaseExpenceDetails CD where CD.caseid=CM.caseid and CD.claimed=0)TotClaimedAmount" +
                          " ,(select isnull(sum(TotalAmountUS$),0) from IRM_CaseExpensesClaimed where caseID=CM.CaseID)TotClaimedAmount" +
                        " ,(select isnull(sum(TotalRecoveredAmount),0) from IRM_CaseExpensesClaimed where caseID=CM.CaseID) RecoveredAmount" +


                          " ,(case when CM.CaseStatus=1 then 'Open' when CM.CaseStatus=2 then 'Closed' end)CaseStatus " +
                          " FROM dbo.IRM_CaseMaster CM " +
                          " INNER JOIN IRM_Groups GP ON GP.GroupId = CM.GroupId  " +
                          " INNER JOIN IRM_UWMaster UWM ON UWM.UWId = CM.UWId ";
                        

        string WhereCond = "WHERE 1=1 ";
        
        // CHECK FOR GROUP
        //string sGroups = "";
        //for (int i = 0; i < chkGroups.Items.Count; i++)
        //{
        //    if (chkGroups.Items[i].Selected)
        //        sGroups = sGroups + "," + chkGroups.Items[i].Value;
        //}
        //if (sGroups != "")
        //{
        //    sGroups = sGroups.Substring(1);
        //    WhereCond = WhereCond + " AND CM.GroupID in (" + sGroups + ")";
        //}
        // CHECK FOR SUB GROUP
        string sSubGroups = "";
        for (int i = 0; i < chkSubGroups.Items.Count; i++)
        {
            if (chkSubGroups.Items[i].Selected)
                sSubGroups = sSubGroups + "," + chkSubGroups.Items[i].Value;
        }
        if (sSubGroups == "") { sSubGroups = "-1"; } 
        if (sSubGroups != "")
        {
            sSubGroups = sSubGroups.Substring(1);
            WhereCond = WhereCond + " AND CM.SubGroupID in (" + sSubGroups + ")";
        }
        //---------------------------------------------------------------------------------------------------------

        if (ddlvessels.SelectedValue == "0")
        {
            string VesselIds = "";
            foreach (ListItem lst in ddlvessels.Items)
            {
                VesselIds = VesselIds + lst.Value + ",";
            }
            VesselIds = VesselIds.Remove(VesselIds.Length - 1);
            WhereCond = WhereCond + "AND CM.VesselId IN (" + VesselIds + ") ";
        }
        else
        {
            WhereCond = WhereCond + "AND CM.VesselId = " + ddlvessels.SelectedValue.Trim() + " ";

        }

        if (ddlAllUW.SelectedValue != "0")
        {
            WhereCond = WhereCond + "AND CM.UWId = " + ddlAllUW.SelectedValue.Trim() + " ";
        }
        if (txtCaseNo.Text.Trim()!="")
        {
            WhereCond = WhereCond + "AND CM.CaseNumber like '%" + txtCaseNo.Text.Trim() + "%' ";
        }
        if (txtMTMCaseNo.Text.Trim() != "")
        {
            WhereCond = WhereCond + "AND CM.CompanyCaseNumber LIKE '%" + txtMTMCaseNo.Text.Trim() + "%' ";
        }
         if (txtFromDt.Text.Trim() != "" && txtToDt.Text.Trim() != "")
        {
            WhereCond = WhereCond + " AND (Convert(datetime,substring(CM.IncidentDate,0,12)) >= '" + txtFromDt.Text.Trim() + "' AND Convert(datetime,substring(CM.IncidentDate,0,12)) <= '" + txtToDt.Text.Trim() + "') ";
        }
        else
        {
            if (txtFromDt.Text != "")
            {
                WhereCond = WhereCond + " AND Convert(datetime,substring(CM.IncidentDate,0,12)) >= '" + txtFromDt.Text.Trim() + "' ";
            }
            if (txtToDt.Text != "")
            {
                WhereCond = WhereCond + " AND Convert(datetime,substring(CM.IncidentDate,0,12))<= '" + txtToDt.Text.Trim() + "' ";
            }
        }
        if (ddlpolicyStatus.SelectedIndex != 0)
        {
            WhereCond = WhereCond + "AND CM.CaseStatus ="+ddlpolicyStatus.SelectedValue+"";
        }


        string strSQL = SearchSQL + WhereCond;
        DataTable dtSearchResult = Budget.getTable(strSQL).Tables[0];

        lblrecord.Text = "Total Records Found: " + dtSearchResult.Rows.Count.ToString();

        rptCases.DataSource = dtSearchResult;
        rptCases.DataBind();
                            
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        chkAllGroups.Items[0].Selected = false;
        //chkAllSubGroups.Items[0].Selected = false;
        chkallgp1(sender, e);        
        ddlvessels.SelectedIndex = 0;
        chkInactiveVessels.Checked = false;
        ddlAllUW.SelectedIndex = 0;
        txtMTMCaseNo.Text = "";
        txtFromDt.Text = "";
        txtToDt.Text = "";
        ddlpolicyStatus.SelectedIndex = 0;
        //chkOverdue.Checked = false;
        //chkDue.Checked = false;
        //txtDueDays.Text = "";
        //ddlRDC.SelectedIndex = 0;
    }
    protected void GrdSearch_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "View")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "tr", "opennewCase('V','" + e.CommandArgument.ToString().Trim() + "');", true);

        }
        if (e.CommandName == "Edit")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "tr", "opennewCase('E','" + e.CommandArgument.ToString().Trim() + "');", true);
        }
    }
    protected void btnNewPolicy_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "tr", "opennewCase('A','0');", true);
    }
    
    protected void rdoOwnerORFleet_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdoOwnerORFleet.SelectedIndex == 0)
        {
            lblOwnerORFleet.Text = "Fleet :";
            ddlFleet.Visible = true;
            ddlOwner.Visible = false;
        }
        else
        {
            lblOwnerORFleet.Text = "Owner :";
            ddlFleet.Visible = false;
            ddlOwner.Visible = true;
        }
    }
    protected void ddlFleet_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        BindVessel();
    }
    protected void ddlOwner_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        BindVessel();
    }


    #endregion -------------------------------------------




    protected void btnPrint_Click(object sender, EventArgs e)
    {

    }
}
