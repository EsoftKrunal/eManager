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

public partial class CrewApproval_CheckList : System.Web.UI.Page
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
    public int Type
    {
        get { return Common.CastAsInt32(ViewState["_Type"]); }
        set { ViewState["_Type"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsgChecklistItems.Text = "";
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------

        lblMSgChecklist.Text = "";

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
            bindRptChecklist();
           
        }
        
    }

   


    // Tab Checklist name ------------------
    protected void btnAddCheckListName_OnClick(object sender, EventArgs e)
    {
        if (txtCheckListName.Text.Trim() == "")
        {
            lblMSgChecklist.Text = "Please enter checklist name";
            txtCheckListName.Focus();
            return;
        }
        string sql = "";
        if(ChecklistID==0)
            sql = "select 1 from tblCheckListMaster where CheckListname='"+ txtCheckListName.Text.Trim() + "' ";
        else
            sql = "select 1 from tblCheckListMaster where CheckListname='" + txtCheckListName.Text.Trim() + "' and  ID!=" + ChecklistID;
        DataTable dt = Budget.getTable(sql).Tables[0];
        if (dt.Rows.Count > 0)
        {
            lblMSgChecklist.Text = "Checklist name already exists. Please enter another name.";
            txtCheckListName.Focus();
            return;
        }


        Common.Set_Procedures("sp_tblCheckListMaster");
        Common.Set_ParameterLength(3);
        Common.Set_Parameters(
            new MyParameter("@ID", ChecklistID),
            new MyParameter("@CheckListname", txtCheckListName.Text.Trim()),
            new MyParameter("@ModifiedBy", Session["UserName"].ToString()));
        DataSet ds = new DataSet();
        if (Common.Execute_Procedures_IUD_CMS(ds))
        {
            if(ChecklistID==0)
                lblMSgChecklist.Text = "Record Added Successfully";
            else
                lblMSgChecklist.Text = "Record Updated Successfully";

            bindRptChecklist();
            ClearData();
        }
        else
        {
            lblMSgChecklist.Text = "Record could not be saved.";
        }
    }
    protected void btnEditCheckListName_OnClick(object sender, EventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        ChecklistID = Common.CastAsInt32(btn.CommandArgument);
        Label lblCheckListname=(Label)btn.Parent.FindControl("lblCheckListname");

        txtCheckListName.Text = lblCheckListname.Text;

        btnAddCheckListName.Text = "Update Checklist";
    }
    protected void btnDeleteChecklist_OnClick(object sender, EventArgs e)
    {
        ImageButton btn = (ImageButton)sender;        
        DataSet ds = Budget.getTable("delete from tblCheckListMaster where id="+ btn.CommandArgument);

        lblMSgChecklist.Text = "Record deleted successfully.";
        bindRptChecklist();
    }

    protected void btnLinkRanks_OnClick(object sender, EventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "ope", "window.open('Checklist_Ranks.aspx?CheckListId=" + btn.CommandArgument + "','');", true);
    }

    
    protected void btnAddChecklistItempPopup_OnClick(object sender, EventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        ChecklistID = Common.CastAsInt32(btn.CommandArgument);
        divAddChecklistItems.Visible = true;
        bindRptChecklistItems();
    }
    protected void btn_CloseChecklistItem_Click(object sender, EventArgs e)
    {
        divAddChecklistItems.Visible = false;
    }
    protected void btnAddChecklistItem_OnClick(object sender, EventArgs e)
    {
        if (txtCheckListItemName.Text.Trim() == "")
        {
            lblMsgChecklistItems.Text = "Please enter checklist name";
            txtCheckListItemName.Focus();
            return;
        }
        string sql = "";
        if (ChecklistItemID == 0)
            sql = "select 1 from tblCheckListItems where CheckListItemName='" + txtCheckListItemName.Text.Trim() + "' ";
        else
            sql = "select 1 from tblCheckListItems where CheckListItemName='" + txtCheckListItemName.Text.Trim() + "' and  ID!=" + ChecklistItemID;
        DataTable dt = Budget.getTable(sql).Tables[0];
        if (dt.Rows.Count > 0)
        {
            lblMsgChecklistItems.Text = "Checklist item name already exists. Please enter another name.";
            txtCheckListItemName.Focus();
            return;
        }


        Common.Set_Procedures("sp_IU_tblCheckListItem");
        Common.Set_ParameterLength(4);
        Common.Set_Parameters(
            new MyParameter("@ID", ChecklistItemID),
            new MyParameter("@CheckListMasterID", ChecklistID),
            new MyParameter("@CheckListItemName", txtCheckListItemName.Text.Trim()),
            new MyParameter("@ModifiedBy", Session["UserName"].ToString()));
        DataSet ds = new DataSet();
        if (Common.Execute_Procedures_IUD_CMS(ds))
        {
            if (ChecklistItemID == 0)
                lblMsgChecklistItems.Text = "Record Added Successfully";
            else
                lblMsgChecklistItems.Text = "Record Updated Successfully";

            ChecklistItemID = 0;
            bindRptChecklistItems();
            ClearChecklistItemData();
        }
        else
        {
            lblMsgChecklistItems.Text = "Record could not be saved.";
        }
    }
    
    protected void btnEditCheckListItem_OnClick(object sender, EventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        Label lblCheckListname=(Label)btn.Parent.FindControl("lblCheckListname");

        ChecklistItemID = Common.CastAsInt32(btn.CommandArgument);
        txtCheckListItemName.Text = lblCheckListname.Text;

    }
    protected void btnDeleteChecklistItem_OnClick(object sender, EventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        string sql = " update tblCheckListItems set status=0 where id="+ btn.CommandArgument;
        DataSet ds = Budget.getTable(sql);
        bindRptChecklistItems();
    }
    


    //****
    protected void bindRptChecklist()
    {
        DataSet ds = Budget.getTable(" select ROW_NUMBER()over(order by ID desc)Sr, * from tblCheckListMaster where Status='A' ");
        rptCheckList.DataSource = ds;
        rptCheckList.DataBind();
    }
    protected void bindRptChecklistItems()
    {
        DataSet ds = Budget.getTable(" select ROW_NUMBER()over(order by ID desc)Sr, * from tblCheckListItems where Status=1 and CheckListMasterID="+ChecklistID);
        rptCheckListItems.DataSource = ds;
        rptCheckListItems.DataBind();
    }
    private void ClearData()
    {
        ChecklistID = 0;
        txtCheckListName.Text = "";
        btnAddCheckListName.Text = "Add Checklist";
    }
    private void ClearChecklistItemData()
    {
        ChecklistItemID = 0;
        txtCheckListItemName.Text = "";
        btnAddChecklistItem.Text = "Add Checklist Item";
    }
    
    // Tab Rank Approval --------------------------

    //**********
   


    // Approval Authority --------------------------
   
    
 





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
