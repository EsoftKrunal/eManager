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

public partial class Modules_HRD_CrewApproval_ApprovalManagement : System.Web.UI.Page
{
    Authority Auth;
    public int ChecklistID
    {
        get { return Common.CastAsInt32(ViewState["_ChecklistID"]); }
        set { ViewState["_ChecklistID"] = value; }
    }
    public int ChecklistItemID
    {
        get { return Common.CastAsInt32(ViewState["_ChecklistItemID"]); }
        set { ViewState["_ChecklistItemID"] = value; }
    }
    public int SelChecklistItemID
    {
        get { return Common.CastAsInt32(ViewState["_SelChecklistItemID"]); }
        set { ViewState["_SelChecklistItemID"] = value; }
    }
    public int Type
    {
        get { return Common.CastAsInt32(ViewState["_Type"]); }
        set { ViewState["_Type"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsgAuthoity.Text = "";
        
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------

        

        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 1076);
        if (chpageauth <= 0)
        {
            Response.Redirect("../AuthorityError.aspx");
        }
        //**********
        ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 9);
        OBJ.Invoke();
        Session["Authority"] = OBJ.Authority;
        Auth = OBJ.Authority;

        if (!(IsPostBack))
        {
           // bindRptChecklist();
            bindRankNoOfApproval();
            BindApprovalAuthority();
        }

    }

    protected void rbl_Type_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        
        pnlApprovalLevel.Visible = false;
        pnlApprovalAuthority.Visible = false;
        
        if (rbl_Type.SelectedIndex == 0)
        {
            pnlApprovalLevel.Visible = true;
        }
        else if (rbl_Type.SelectedIndex == 1)
        {
            pnlApprovalAuthority.Visible = true;
        }
    }


    // Tab Checklist name ------------------


    protected void btnLinkRanks_OnClick(object sender, EventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "ope", "window.open('Checklist_Ranks.aspx?CheckListId=" + btn.CommandArgument + "','');", true);
    }


   
    
    



    //****

  
 

    // Tab Rank Approval --------------------------
    protected void btnSaveNoOfApprovalRequired_OnClick(object sender, EventArgs e)
    {
        string rabks = "";
        string noOfApprovals = "";
        string ApprovalLevels = "";


        foreach (RepeaterItem item in rptRankNoOfApproval.Items)
        {
            int CheckCount = 0;
            string CheckedValues = "";

            HiddenField hfdRankID = (HiddenField)item.FindControl("hfdRankID");
            DropDownList ddlRankApprovalLevel = (DropDownList)item.FindControl("ddlRankApprovalLevel");

            CheckBox chkCrew = (CheckBox)item.FindControl("chkCrew");
            CheckBox chkTechnical = (CheckBox)item.FindControl("chkTechnical");
            CheckBox chkMarine = (CheckBox)item.FindControl("chkMarine");
            CheckBox chkFleetManager = (CheckBox)item.FindControl("chkFleetManager");
            CheckBox chkOR = (CheckBox)item.FindControl("chkOR");

            if (chkCrew.Checked)
            {
                CheckCount++;
                CheckedValues = CheckedValues + "`" + "1";
            }
            if (chkTechnical.Checked)
            {
                CheckCount++;
                CheckedValues = CheckedValues + "`" + "2";
            }
            if (chkMarine.Checked)
            {
                CheckCount++;
                CheckedValues = CheckedValues + "`" + "3";
            }
            if (chkFleetManager.Checked)
            {
                CheckCount++;
                CheckedValues = CheckedValues + "`" + "4";
            }
            if (chkOR.Checked)
            {
                CheckCount++;
                CheckedValues = CheckedValues + "`" + "5";
            }
            if (CheckCount == 0)
            {
                lblMsgApproval.Text = "Please select at least one approval level for all the rank.";
                return;
            }
            rabks = rabks + "," + hfdRankID.Value;
            noOfApprovals = noOfApprovals + "," + CheckCount;
            ApprovalLevels = ApprovalLevels + "," + CheckedValues;
        }
        rabks = rabks.Substring(1);
        noOfApprovals = noOfApprovals.Substring(1);
        ApprovalLevels = ApprovalLevels.Substring(1);

        try
        {
            Common.Set_Procedures("[DBO].sp_updateNoOfApprovalForRank");
            Common.Set_ParameterLength(3);
            Common.Set_Parameters(
                new MyParameter("@Ranks", rabks),
                new MyParameter("@Approval", noOfApprovals),
                new MyParameter("@ApprovalLevels", ApprovalLevels)
                );
            DataSet dsComponents = new DataSet();
            dsComponents.Clear();
            Boolean res;
            res = Common.Execute_Procedures_IUD(dsComponents);
            if (res)
            {
                lblMsgApproval.Text = "Record updated successfully.";
            }
            else
            {
                lblMsgApproval.Text = "Error while updating the record. Error :" + Common.getLastError();
            }
        }
        catch (Exception ex)
        {
            lblMsgApproval.Text = "Error while updating the record. Error :" + ex.Message;
        }

    }
    //**********
    private void bindRankNoOfApproval()
    {
        string sql = " Select ROW_NUMBER()over(order by RankLevel)Sr,RankGroupId,RankID,RankCode,RankName,isnull(NoOfApproval,0)NoOfApproval,ApprovalLevels from Rank Where StatusID='A' and rankcode not in('SUPT') order by RankLevel ";
        DataTable dtRank = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        rptRankNoOfApproval.DataSource = dtRank;
        rptRankNoOfApproval.DataBind();
    }


    // Approval Authority --------------------------
    protected void btnSaveAuthoity_OnClick(object sender, EventArgs e)
    {
        try
        {
            foreach (RepeaterItem itm in rptApprovalAuthority.Items)
            {
                HiddenField hfdLoginID = (HiddenField)itm.FindControl("hfdLoginID");
                CheckBox chlapp1 = (CheckBox)itm.FindControl("chlapp1");
                CheckBox chlapp2 = (CheckBox)itm.FindControl("chlapp2");
                CheckBox chlapp3 = (CheckBox)itm.FindControl("chlapp3");
                CheckBox chlapp4 = (CheckBox)itm.FindControl("chlapp4");
                CheckBox chlapp5 = (CheckBox)itm.FindControl("chlapp5");

                string sql = " exec dbo.sp_IU_CrewPlanningApprovalAuthority " + hfdLoginID.Value + "," + ((chlapp1.Checked) ? 1 : 0) + "," + ((chlapp2.Checked) ? 1 : 0) + "," + ((chlapp3.Checked) ? 1 : 0) + "," + ((chlapp4.Checked) ? 1 : 0) + "," + ((chlapp5.Checked) ? 1 : 0);

                Common.Execute_Procedures_Select_ByQueryCMS(sql);

            }
            lblMsgAuthoity.Text = "Record saved successfully.";
        }
        catch
        {
            lblMsgAuthoity.Text = "Error while saving record.";
        }
    }

    public void BindApprovalAuthority()
    {
        string sql = " Select ROW_NUMBER()over(order by FirstName+' '+LastName)as Rowno, u.LoginID,FirstName+' '+LastName as UserName , isnull(A.Approval1,0) as Approval1, isnull(A.Approval2,0) as Approval2, isnull(A.Approval3,0) as Approval3, isnull(A.Approval4,0) as Approval4, isnull(A.Approval5,0) as Approval5 " +
                     "  from dbo.UserLogin U " +
                     "  Left Join CrewPlanningApprovalAuthority A on u.LoginId = a.loginid " +
                     "   where StatusID = 'A' Order by FirstName + ' ' + LastName ";
        DataTable dtRank = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        rptApprovalAuthority.DataSource = dtRank;
        rptApprovalAuthority.DataBind();
    }





    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static string save_checklist_rank_mapping(int checklistid, int rankid, string action)
    {
        string ret = "error";
        try
        {
            Common.Set_Procedures("[DBO].sp_IU_tblCheckListRankMapping");
            Common.Set_ParameterLength(3);
            Common.Set_Parameters(
                new MyParameter("@CheckListID", checklistid),
                new MyParameter("@RankID", rankid),
                new MyParameter("@Action", action)
                );
            DataSet dsComponents = new DataSet();
            dsComponents.Clear();
            Boolean res;
            res = Common.Execute_Procedures_IUD(dsComponents);
            if (res)
            {
                ret = "OK";
            }
        }
        catch (Exception ex)
        {

        }

        return ret;
    }
}