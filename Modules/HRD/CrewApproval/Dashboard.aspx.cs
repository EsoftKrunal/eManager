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
using System.Text;
using System.Xml;
public partial class CrewApproval_Dashboard : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        ShowCounter();
        if (!Page.IsPostBack)
        {
            BindRank();
            BindVessel();
        }
        
    }
    protected void btnSearch1_Click(object sender, EventArgs e)
    {
        lnkfind.Visible = false;
        lnkfind.Attributes.Remove("onclick");
        if (txtcrewsearch.Text.Trim() == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "fsadf", "alert('Please enter crew# to search');", true);
        }
        else
        {
            string sql = "SELECT TOP 1 PlanningId FROM DBO.CrewVesselPlanningHistory WHERE Status='A' AND ( AppStatus<>'A' and AppStatus<>'R') and RelieverId In(select crewid from DBO.crewpersonaldetails where crewnumber='" + txtcrewsearch.Text.Trim() + "')";
            if(rad_type.SelectedValue=="A")
            {
                sql = "SELECT TOP 1 PlanningId FROM DBO.CrewVesselPlanningHistory WHERE Status='A' AND AppStatus='A' and RelieverId In(select crewid from DBO.crewpersonaldetails where crewnumber='" + txtcrewsearch.Text.Trim() + "') ORDER BY PlanningId  DESC";
            }
            DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
            if (dt.Rows.Count > 0)
            {
                lnkfind.Visible = true;
                lnkfind.Text ="View";
                lnkfind.Attributes.Add("onclick", "window.open('../CrewOperation/CrewPlanningApproval.aspx?_P=" + dt.Rows[0][0].ToString() + "','');");
            }
            else
            {
                lnkfind.Visible = true;
                lnkfind.Text = "Sorry ! this crew# not available in crew approval.";
            }
        }
    }
    protected void btnCouterClick(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        string Mode = btn.CommandArgument;
        switch (Mode)
        {
            case "PendingSubmission": litHeading.Text = "Submission Pending"; break;
            case "AwaitingApprovalCrew": litHeading.Text = "Manning Approval Pending"; break;
            case "AwaitingApprovalTechnical": litHeading.Text = "Technical Approval Pending"; break;
            case "AwaitingApprovalMarine": litHeading.Text = "Marine Approval Pending"; break;
            case "AwaitingApprovalFleetManager": litHeading.Text = "FleetManager Approval Pending"; break;
            case "AwaitingApprovalOR": litHeading.Text = "Owner Approval Pending"; break;
            case "AwaitingApprovalManagement": litHeading.Text = "Management Approval Pending"; break;
            case "Approved": litHeading.Text = "Approved"; break;
            case "Rejected": litHeading.Text = "Rejected"; break;
            case "PendingSubmissionAllOther": litHeading.Text = "Submission Pending"; break;
            case "AwaitingApprovalAllOther": litHeading.Text = "Approval Pending"; break;
            case "ApprovedAllOther": litHeading.Text = "Approved"; break;
            case "RejectedAllOther": litHeading.Text = "Rejected"; break;
        }
        Bind_Grid(btn.CommandArgument);
        divCounterList.Visible = true;
    }

    //----------------------------------------------------------------------------------------
    protected void btnCloseCounterList_Click(object sender, EventArgs e)
    {
        divCounterList.Visible = false;
    }

    protected void btnCL_Click(object sender, EventArgs e)
    {
        string PlanningId = ((ImageButton)sender).CommandArgument;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "c", "window.open('../CrewOperation/ViewCrewCheckList.aspx?_P=" + PlanningId + "');", true);
    }
    public void BindRank()
    {
        string sql = "Select RankCode from dbo.Rank order by RankLevel ";
        DataTable DT = Common.Execute_Procedures_Select_ByQuery(sql);
        ddlPlannedRank.DataSource = DT;
        ddlPlannedRank.DataTextField = "RankCode";
        ddlPlannedRank.DataValueField = "RankCode";
        ddlPlannedRank.DataBind();
        ddlPlannedRank.Items.Insert(0,new ListItem("All Rank", ""));

    }
    private void BindVessel()
    {
        DataSet ds = cls_SearchReliever.getMasterData("Vessel", "VesselCode", "VesselName", Convert.ToInt32(Session["loginid"].ToString()));
        ddlPlannedVessel.DataValueField = "VesselCode";
        ddlPlannedVessel.DataTextField = "VesselName";
        ddlPlannedVessel.DataSource = ds;
        ddlPlannedVessel.DataBind();
        ddlPlannedVessel.Items.Insert(0, new ListItem(" All Vessel", "0"));
    }
    // Function ---------------------------------------------------------------------------
    private void Bind_Grid(string Mode)
    {
        ViewState["_mode"] = Mode;
        string sql = " exec DBO.sp_CrewApprovalCouterList '"+ Mode + "','" + txtcrewno.Text.Trim() + "'";
        DataTable Dt = Common.Execute_Procedures_Select_ByQuery(sql);
        DataView filterData=Dt.DefaultView;

        string Filter = "";
        if (ddlPlannedRank.SelectedIndex>0)
        {
            //filterData.RowFilter = "PlannedRankCode='" + ddlPlannedRank.SelectedValue + "'";
            Filter= "PlannedRankCode='" + ddlPlannedRank.SelectedValue + "'";
        }
        if (ddlPlannedVessel.SelectedIndex > 0)
        {
            //filterData.RowFilter = "VesselCode='" + ddlPlannedVessel.SelectedValue + "'";
            if (Filter == "")
                Filter = "VesselCode='" + ddlPlannedVessel.SelectedValue + "'";
            else
            {
                Filter= Filter+" and VesselCode='" + ddlPlannedVessel.SelectedValue + "'";
            }
        }
        filterData.RowFilter = Filter;
        gv_CrewApproval.DataSource = filterData;
        gv_CrewApproval.DataBind();
    }

    private void ShowCounter()
    {
        string sql = " exec DBO.DashBoard_CrewApproval  ";
        DataTable Dt = Common.Execute_Procedures_Select_ByQuery(sql);

        if (Dt.Rows.Count > 0)
        {
            DataRow Dr = Dt.Rows[0];
            lblPendingSubmission.Text = Dr["PendingSubmission"].ToString();
            lblAwaitingApprovalCrew.Text = Dr["Crew"].ToString();
            lblAwaitingApprovalTechnical.Text = Dr["Technical"].ToString();
            AwaitingApprovalMarine.Text = Dr["Marine"].ToString();
            lblAwaitingApprovalFleetManager.Text = Dr["Fleet"].ToString();
            lblAwaitingApprovalOR.Text = Dr["OR"].ToString();
            lblAwaitingApprovalManagement.Text = Dr["Management"].ToString();
            lblApproved.Text = Dr["Approved"].ToString();
            lblRejected.Text = Dr["Rejected"].ToString();

            lblPendingSubmissionAllOther.Text = Dr["OPendingSubmission"].ToString();
            lblAwaitingApprovalAllOther.Text = Dr["OAwaitingApproval"].ToString();
            lblApprovedAllOther.Text = Dr["OApproved"].ToString();
            lblRejectedAllOther.Text = Dr["ORejected"].ToString();
        }
        else
        {
            lblPendingSubmission.Text = "";
            lblAwaitingApprovalCrew.Text = "";
            lblAwaitingApprovalTechnical.Text = "";
            AwaitingApprovalMarine.Text = "";
            lblAwaitingApprovalFleetManager.Text = "";
            lblAwaitingApprovalOR.Text = "";
            lblAwaitingApprovalManagement.Text = "";
            lblApproved.Text = "";
            lblRejected.Text = "";



            lblPendingSubmissionAllOther.Text = "";
            lblAwaitingApprovalAllOther.Text = "";
            lblApprovedAllOther.Text = "";
            lblRejectedAllOther.Text = "";
        }
    }
    public string getCssStatus(int Level,int PlanningID)
    {
        string css = "";
        //string sql = " select case when ApprovedOn is null then '../Images/red_circle.png' else '../Images/green_circle.gif' end css,Result from DBO.CrewPlanningApprovalComments where PlanningId=" + PlanningID + " and ApprovalLevel="+ Level + " ";
        string sql = " select ApprovedOn,Result from DBO.CrewPlanningApprovalComments where PlanningId=" + PlanningID + " and ApprovalLevel=" + Level + " ";
        DataTable Dt = Common.Execute_Procedures_Select_ByQuery(sql);
        if (Dt.Rows.Count > 0)
        {
            if( Dt.Rows[0][1].ToString()=="A")
                css = "<img src='" + "../Images/green_circle.gif" + "' />";
            else if (Dt.Rows[0][1].ToString() == "R")
                css = "<img src='" + "../Images/exclamation-mark-yellow.png" + "' />";
            else
                css = "<img src='" + "../Images/red_circle.png" + "' />";
        }
        else
        {
            //css = "<img src='" + "../Images/exclamation-mark-yellow.png" + "' />" ;
            //css = "<img src='" + "../Images/red_circle.png" + "' />";
        }
        return css;
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Bind_Grid(ViewState["_mode"].ToString());
    }
}
