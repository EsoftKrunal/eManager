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

public partial class Modules_HRD_Registers_PrejoiningDocsMapping : System.Web.UI.Page
{
    int id;
    Authority Auth;
    string Mode = "New";
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        lbl_Message.Text = "";
        lbl_PrejoingDoc_Msg.Text = "";
        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 10);
        if (chpageauth <= 0)
        {
            Response.Redirect("Dummy.aspx");

        }
        //******************* 
        ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 10);
        OBJ.Invoke();
        Session["Authority"] = OBJ.Authority;
        Auth = OBJ.Authority;
        if (!Page.IsPostBack)
        {
            loadPrejoingDocs();
            bindgridSearch();
            loadManningAgent();
            Alerts.HidePanel(pnl_PrejoingDocs);
            Alerts.HANDLE_AUTHORITY(1, btn_Add, btn_Save, btn_Cancel, btn_Print, Auth);
        }
    }

    public void bindgridSearch()
    {
        string sql = "Select mg.Manning_AgentId  As Manning_AgentId, mg.Manning_AgentName As Manning_AgentName  from Manning_Agent mg with(nolock) Inner Join PrejoiningDocsManningAgentMapping pds with(nolock) on mg.Manning_AgentId = pds.Manning_AgentId where mg.StatusId = 'A' and pds.StatusId = 'A' Group by mg.Manning_AgentId , mg.Manning_AgentName";
        DataTable dt2;
        dt2 = Budget.getTable(sql).Tables[0];
        Gv_PrejoinDocsMapping.DataSource = dt2;
        Gv_PrejoinDocsMapping.DataBind();
    }
    public void loadPrejoingDocs()
    {
        DataTable dt = Budget.getTable("Select DocumentId, DocumentName from PreJoiningDocuments with(nolock) where Status = 'A'").Tables[0];
        chklst_Docs.DataSource = dt;
        chklst_Docs.DataTextField = "DocumentName";
        chklst_Docs.DataValueField = "DocumentId";
        chklst_Docs.DataBind();
    }

    public void loadManningAgent()
    {
        DataTable dt = Budget.getTable("Select Manning_AgentId,Manning_AgentName from Manning_Agent with(nolock) where StatusId = 'A'").Tables[0];
        ddlManningAgent.DataSource = dt;
        ddlManningAgent.DataTextField = "Manning_AgentName";
        ddlManningAgent.DataValueField = "Manning_AgentId";
        ddlManningAgent.DataBind();
        ddlManningAgent.Items.Insert(0, new ListItem("< Select >", "0"));
    }

    protected void ddlManningAgent_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

        }
        catch(Exception ex)
        {

        }

    }

   

    protected void Gv_PrejoinDocsMapping_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {

    }

    protected void Gv_PrejoinDocsMapping_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Modify")
        {
            GridViewRow row = (GridViewRow)(((ImageButton)sender).NamingContainer);
            int Rowindx = row.RowIndex;
            Mode = "Edit";
            HiddenField hdnAgentId;
            hdnAgentId = (HiddenField)Gv_PrejoinDocsMapping.Rows[Rowindx].FindControl("hdnAgentId");
            id = Convert.ToInt32(hdnAgentId.Value.ToString());

            Show_Record_PreJoinDocs(id);
            Gv_PrejoinDocsMapping.SelectedIndex = Rowindx;
            for (int i = 0; i < chklst_Docs.Items.Count; i++)
            {
                Int32 DocId = Convert.ToInt32(chklst_Docs.Items[i].Value);
                chklst_Docs.Items[i].Selected = PrejoingingDocs.chkDocs_ManningAgent(id, DocId);
            }
            Alerts.ShowPanel(pnl_PrejoingDocs);
            Alerts.HANDLE_AUTHORITY(5, btn_Add, btn_Save, btn_Cancel, btn_Print, Auth);
        }
    }

    protected void btnEditPrejoinDocs_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow row = (GridViewRow)(((ImageButton)sender).NamingContainer);
        int Rowindx = row.RowIndex;
        Mode = "Edit";
        HiddenField hdnAgentId;
        hdnAgentId = (HiddenField)Gv_PrejoinDocsMapping.Rows[Rowindx].FindControl("hdnAgentId");
        id = Convert.ToInt32(hdnAgentId.Value.ToString());

        Show_Record_PreJoinDocs(id);
        Gv_PrejoinDocsMapping.SelectedIndex = Rowindx;
        for (int i = 0; i < chklst_Docs.Items.Count; i++)
        {
            Int32 DocId = Convert.ToInt32(chklst_Docs.Items[i].Value);
            chklst_Docs.Items[i].Selected = PrejoingingDocs.chkDocs_ManningAgent(id, DocId);
        }
        Alerts.ShowPanel(pnl_PrejoingDocs);
        Alerts.HANDLE_AUTHORITY(5, btn_Add, btn_Save, btn_Cancel, btn_Print, Auth);
    }

    protected void Gv_PrejoinDocsMapping_Row_Editing(object sender, GridViewEditEventArgs e)
    {
        HiddenField hdnAgentId;
        hdnAgentId = (HiddenField)Gv_PrejoinDocsMapping.Rows[e.NewEditIndex].FindControl("hdnAgentId");
        id = Convert.ToInt32(hdnAgentId.Value.ToString());
        Show_Record_PreJoinDocs(id);
        Gv_PrejoinDocsMapping.SelectedIndex = e.NewEditIndex;
        for (int i = 0; i < chklst_Docs.Items.Count; i++)
        {
            Int32 DocId = Convert.ToInt32(chklst_Docs.Items[i].Value);
            chklst_Docs.Items[i].Selected = PrejoingingDocs.chkDocs_ManningAgent(id, DocId);
        }
        Alerts.ShowPanel(pnl_PrejoingDocs);
        Alerts.HANDLE_AUTHORITY(4, btn_Add, btn_Save, btn_Cancel, btn_Print, Auth);
    }

    protected void Gv_PrejoinDocsMapping_SelectedIndexChanged(object sender, EventArgs e)
    {
        HiddenField hdnAgentId;
        hdnAgentId = (HiddenField)Gv_PrejoinDocsMapping.Rows[Gv_PrejoinDocsMapping.SelectedIndex].FindControl("hdnAgentId");
        id = Convert.ToInt32(hdnAgentId.Value.ToString());
        Show_Record_PreJoinDocs(id);
        for (int i = 0; i < chklst_Docs.Items.Count; i++)
        {
            Int32 DocId = Convert.ToInt32(chklst_Docs.Items[i].Value);
            chklst_Docs.Items[i].Selected = PrejoingingDocs.chkDocs_ManningAgent(id, DocId);
        }
        Alerts.ShowPanel(pnl_PrejoingDocs);
        Alerts.HANDLE_AUTHORITY(4, btn_Add, btn_Save, btn_Cancel, btn_Print, Auth);
    }

    protected void Gv_PrejoinDocsMapping_DataBound(object sender, EventArgs e)
    {
        try
        {
            Gv_PrejoinDocsMapping.Columns[1].Visible = Auth.isEdit;
        }
        catch
        {
            Gv_PrejoinDocsMapping.Columns[1].Visible = false;
        }
    }

    protected void Show_Record_PreJoinDocs(int ManningAgentId)
    {
        hdnManningAgentId.Value = ManningAgentId.ToString();
        ddlManningAgent.SelectedValue = hdnManningAgentId.Value;
    }

    protected void Gv_PrejoinDocsMapping_PreRender(object sender, EventArgs e)
    {
        if (Gv_PrejoinDocsMapping.Rows.Count <= 0) { lbl_PrejoingDoc_Msg.Text = "No Records Found..!"; }
    }

    protected void btn_Add_Click(object sender, EventArgs e)
    {
        ddlManningAgent.SelectedIndex = 0;
        hdnManningAgentId.Value = "";

        Alerts.ShowPanel(pnl_PrejoingDocs);
        Alerts.HANDLE_AUTHORITY(2, btn_Add, btn_Save, btn_Cancel, btn_Print, Auth);
    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        int Duplicate = 0;
        //foreach (GridViewRow dg in Gv_PrejoinDocsMapping.Rows)
        //{
        //    HiddenField hfdAgentName;
        //    HiddenField hfdAgentId;
        //    hfdAgentName = (HiddenField)dg.FindControl("hdnAgentName");
        //    hfdAgentId = (HiddenField)dg.FindControl("hdnAgentId");
        //    if (hfdAgentId.Value == ddlManningAgent.SelectedValue)
        //    {
        //        if (hdnManningAgentId.Value.Trim() == "")
        //        {

        //            lbl_PrejoingDoc_Msg.Text = "Already Entered.";
        //            Duplicate = 1;
        //            break;
        //        }
        //        else if (hdnManningAgentId.Value.Trim() != hfdAgentId.Value.ToString())
        //        {

        //            lbl_PrejoingDoc_Msg.Text = "Already Entered.";
        //            Duplicate = 1;
        //            break;
        //        }
        //    }
        //    else
        //    {

        //        lbl_PrejoingDoc_Msg.Text = "";
        //    }
        //}

        if (Duplicate == 0)
        {
            int AgentId = -1;
            int modifiedby = 0;
            if (hdnManningAgentId.Value == "")
            {
            }
            else
            {
                AgentId = Convert.ToInt32(hdnManningAgentId.Value);
                modifiedby = Convert.ToInt32(Session["loginid"].ToString());
            }
            Int32 ManningAgentId = Convert.ToInt32(ddlManningAgent.SelectedValue);
            //char statusid = 'A';
            try
            {
                
                if (ManningAgentId > 0)
                {
                    string datastr = "";
                    for (int count = 0; count < chklst_Docs.Items.Count; count++)
                    {
                        if (chklst_Docs.Items[count].Selected)
                        {
                            datastr = datastr + ',' + chklst_Docs.Items[count].Value;
                        }
                    }
                    if (datastr != "") { datastr = datastr.Substring(1); } else { lbl_PrejoingDoc_Msg.Text = "Please select atleast one Prejoining documents."; return; };
                    PrejoingingDocs.InsertPreJoiningDocs(datastr, ManningAgentId, ManningAgentId);
                }
                

                bindgridSearch();
                lbl_PrejoingDoc_Msg.Text = "Record Saved Successfully.";

                Alerts.HidePanel(pnl_PrejoingDocs);
                Alerts.HANDLE_AUTHORITY(3, btn_Add, btn_Save, btn_Cancel, btn_Print, Auth);

            }
            catch
            {
                bindgridSearch();
                lbl_PrejoingDoc_Msg.Text = "Record Can't Saved.";

                Alerts.HidePanel(pnl_PrejoingDocs);
                Alerts.HANDLE_AUTHORITY(3, btn_Add, btn_Save, btn_Cancel, btn_Print, Auth);

            }
        }
    }

    protected void btn_Cancel_Click(object sender, EventArgs e)
    {
        Gv_PrejoinDocsMapping.SelectedIndex = -1;

        Alerts.HidePanel(pnl_PrejoingDocs);
        Alerts.HANDLE_AUTHORITY(6, btn_Add, btn_Save, btn_Cancel, btn_Print, Auth);
    }

    protected void btn_Print_Click(object sender, EventArgs e)
    {

    }
}
   

