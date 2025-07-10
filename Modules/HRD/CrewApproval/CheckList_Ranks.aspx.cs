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

public partial class CrewApproval_CheckList_Ranks : System.Web.UI.Page
{
    Authority Auth;    
    public int ChecklistID
    {
        get { return Common.CastAsInt32(ViewState["_ChecklistID"]); }
        set { ViewState["_ChecklistID"] =value; }
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
   

    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------

        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 29);
        if (chpageauth <= 0)
        {
            Response.Redirect("../AuthorityError.aspx");
        }
        //**********
        ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 1);
        OBJ.Invoke();
        Session["Authority"] = OBJ.Authority;
        Auth = OBJ.Authority;

        if (!(IsPostBack))
        {
            ChecklistID = Convert.ToInt32(Request.QueryString["CheckListId"].ToString());
            DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("select CheckListName from tblCheckListMaster where id=" + ChecklistID);
            if (dt.Rows.Count > 0)
                lblCheckListName.Text = dt.Rows[0][0].ToString();

            BindRabkGroupList();
        }        
    }
    
    // Tab New Crew Checklist --------------------------
    protected void ddl_RankGroup_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        BindRabkGroupList();
        divChecklistRank.InnerHtml = "";
    }
    protected void chklistRankGroups_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        SetChecklistRankDiv();
    }
    protected void btnSave_OnClick(object sender, EventArgs e)
    {
        try
        {
            foreach (RepeaterItem itm in rptRanks.Items)
            {
                CheckBox chk = (CheckBox)itm.FindControl("chkApplyRanks");
                HiddenField RankID = (HiddenField)itm.FindControl("hfdRankID");

                Common.Set_Procedures("[DBO].sp_IU_tblCheckListRankMapping");
                Common.Set_ParameterLength(3);
                Common.Set_Parameters(
                    new MyParameter("@CheckListID", ChecklistID),
                    new MyParameter("@RankID", RankID.Value),
                    new MyParameter("@Action", ((chk.Checked)?"A":"D"))
                    );
                DataSet dsComponents = new DataSet();
                dsComponents.Clear();
                Boolean res;
                res = Common.Execute_Procedures_IUD(dsComponents);
            }
            lblMsg.Text = "Record updated successfully";
         }
        catch (Exception ex)
        {

        }
    }
    
    //******
    private void SetChecklistRankDiv()
    {

        DataTable dtChecklistname = Common.Execute_Procedures_Select_ByQueryCMS(" select ROW_NUMBER()over(order by ID desc)Sr, * from tblCheckListMaster where Status='A' and ID=" + ChecklistID);
        string selIDs = "";
        foreach (ListItem item in chklistRankGroups.Items)
        {
            if (item.Selected)
            {
                selIDs = selIDs + "," + item.Value;
            }
        }

        if (selIDs == "")
        {
            //divChecklistRank.InnerHtml = "";
            rptRanks.DataSource = null;
            rptRanks.DataBind();
        }
        else
        {
            selIDs = selIDs.Substring(1);
            string sql = " Select RankGroupId,RankID,RankCode,* from Rank Where StatusID='A' and RankGroupId in(" + selIDs + ") order by RankLevel ";
            DataTable dtRank = Common.Execute_Procedures_Select_ByQueryCMS(sql);
            rptRanks.DataSource = dtRank;
            rptRanks.DataBind();

        }
    }
    //private void SetChecklistRankDiv()
    //{

    //    DataTable dtChecklistname = Common.Execute_Procedures_Select_ByQueryCMS(" select ROW_NUMBER()over(order by ID desc)Sr, * from tblCheckListMaster where Status='A' and ID="+Type);


    //    string selIDs = "";
    //    foreach (ListItem item in chklistRankGroups.Items)
    //    {
    //        if (item.Selected)
    //        {
    //            selIDs = selIDs + "," + item.Value;
    //        }
    //    }

    //    if (selIDs == "")
    //    {
    //        divChecklistRank.InnerHtml = "";
    //    }
    //    else
    //    {
    //        selIDs = selIDs.Substring(1);
    //        string sql = " Select RankGroupId,RankID,RankCode,* from Rank Where StatusID='A' and RankGroupId in(" + selIDs + ") order by RankLevel ";
    //        DataTable dtRank = Common.Execute_Procedures_Select_ByQueryCMS(sql);

    //        StringBuilder strTbl = new StringBuilder();
    //        strTbl.Append("<div style='overflow-y:scroll;overflow-x:hidden;width:100%;  border:solid 0px gray;'>");
    //        strTbl.Append("<table width='100%' cellpadding='2' cellspacing='0' border='1' style='border-collapse:collapse; font-size:14px;  height:25px;background-color:#FFE6B2'>");
    //        // 1st tr
    //        strTbl.Append("<tr style='height: 25px;'>");
    //        strTbl.Append("<td>");
    //        strTbl.Append("</td>");
    //        foreach (DataRow dr in dtRank.Rows)
    //        {
    //            strTbl.Append("<td style='width:50px;'>" + dr["RankCode"] + "</td>");
    //        }
    //        strTbl.Append("</tr>");
    //        strTbl.Append("</table");
    //        strTbl.Append("</div");

    //        strTbl.Append("<div style='overflow - y:scroll; overflow - x:hidden; height: 300px; width: 100 %; border: solid 0px gray;'>");
    //        strTbl.Append("<table width='100%' cellpadding='2' cellspacing='0' border='1' style='border-collapse:collapse; font-size:14px;  height:25px;'>");
    //        // rest of the trs
    //        foreach (DataRow drchecklist in dtChecklistname.Rows)
    //        {
    //            strTbl.Append("<tr style='height: 25px;'>");
    //            strTbl.Append("<td style='text-align:left;'>" + drchecklist["CheckListname"] + "</td>");
    //            foreach (DataRow drRank in dtRank.Rows)
    //            {
    //                strTbl.Append("<td style='width:50px;'>" + "<input type='checkbox' class='listRanks' " + getChecklistRankMapingResult(Common.CastAsInt32(drchecklist["ID"]), Common.CastAsInt32(drRank["RankID"])) + "  checklistid=" + drchecklist["ID"].ToString() + " rankid=" + drRank["RankID"].ToString() + " ></input>" + "</td>");
    //            }
    //            strTbl.Append("</tr>");
    //        }

    //        strTbl.Append("</table>");
    //        strTbl.Append("</div>");

    //        divChecklistRank.InnerHtml = strTbl.ToString();
    //    }
    //}
    private void BindRabkGroupList()
    {
        string sql = "";
        sql = "Select RankGroupId,RankGroupName from  RankGroup rg where rg.RankGroupId in (Select distinct RankGroupId  from Rank Where StatusID='A' and Offcrew='" + ddlCategory.SelectedValue + "')";
        DataTable dtChecklistname = Common.Execute_Procedures_Select_ByQueryCMS(sql);

        chklistRankGroups.DataSource = dtChecklistname;
        chklistRankGroups.DataTextField = "RankGroupName";
        chklistRankGroups.DataValueField = "RankGroupId";
        chklistRankGroups.DataBind();
    }
    public bool getChecklistRankMapingResult(int RankID)
    {
        bool ret =false;
        string sql = " select 1 from tblCheckListRankMapping where CheckListID=" + ChecklistID + " and RankID=" + RankID;
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        if (dt.Rows.Count > 0)
            ret = true;

        return ret;
    }
    //private string getChecklistRankMapingResult(int CheckListID, int RankID)
    //{
    //    string ret = "";
    //    string sql = " select 1 from tblCheckListRankMapping where CheckListID=" + CheckListID + " and RankID=" + RankID ;
    //    DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
    //    if (dt.Rows.Count > 0)
    //        ret = "checked";

    //    return ret;
    //}


    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static string save_checklist_rank_mapping(int checklistid, int rankid,string action)
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
