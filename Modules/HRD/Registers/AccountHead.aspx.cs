using System;
using System.Data;
using System.Configuration;
using System.Reflection;
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

public partial class Registers_AccountHead : System.Web.UI.Page
{
    int id;
    Authority Auth;
    string Mode = "New";
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        lbl_GridView_AccountHead.Text = "";
        lbl_AccountHead_Message.Text = "";
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 10);
        if (chpageauth <= 0)
        {
            Response.Redirect("Dummy.aspx");
        }
        ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 10);
        OBJ.Invoke();
        Session["Authority"] = OBJ.Authority;
        Auth = OBJ.Authority;
        if (!Page.IsPostBack)
        {
            bindAccountHeadGrid();
            bindAccountHeadTypeDDL();
            bindStatusDDL();
            Alerts.HidePanel(pnl_AccountHead);
            Alerts.HANDLE_AUTHORITY(1, btn_Add_AccountHead, btn_Save_AccountHead, btn_Cancel_AccountHead, btn_Print_AccountHead,Auth);     
        }
    }
    public void bindAccountHeadGrid()
    {
        string s;
        s = txt_Licence.Text.Trim();  

        DataTable dt1 = AccountHead.selectDataAccountHeadDetails(s);
        this.GridView_AccountHead.DataSource = dt1;
        this.GridView_AccountHead.DataBind();
    }
    
    public void bindAccountHeadTypeDDL()
    {
        FieldInfo[] thisEnumFields = typeof(AccountType).GetFields();
        foreach (FieldInfo thisField in thisEnumFields)
        {
            if (!thisField.IsSpecialName && thisField.Name.ToLower() != "notset")
            {
                int thisValue = (int)thisField.GetValue(0);
                ddlAccountHeadType.Items.Add(new ListItem(thisField.Name,thisValue.ToString()));
            }
        }
    }
    public void bindStatusDDL()
    {
        DataTable dt2 = AccountHead.selectDataStatusDetails();
        this.ddlStatus_AccountHead.DataValueField = "StatusId";
        this.ddlStatus_AccountHead.DataTextField = "StatusName";
        this.ddlStatus_AccountHead.DataSource = dt2;
        this.ddlStatus_AccountHead.DataBind();
    }
    protected void btn_Add_AccountHead_Click(object sender, EventArgs e)
    {
        ddlAccountHeadType.SelectedIndex = 0;
        txtAccountHeadName.Text = "";
        txt_cls.Text = "";
        txtAccountHeadNumber.Text = "";
        chkIncludeInBudget.Checked = false;
        txtCreatedBy_AccountHead.Text = "";
        txtCreatedOn_AccountHead.Text = "";
        txtModifiedBy_AccountHead.Text = "";
        txtModifiedOn_AccountHead.Text = "";
        chkrecoverablecost.Checked = false;
        GridView_AccountHead.SelectedIndex = -1;
        ddlStatus_AccountHead.SelectedIndex = 0;
        HiddenAccountHead.Value = "";
        //----------------------
        Alerts.ShowPanel(pnl_AccountHead);
        Alerts.HANDLE_AUTHORITY(2, btn_Add_AccountHead, btn_Save_AccountHead, btn_Cancel_AccountHead, btn_Print_AccountHead, Auth);    
    }
    protected void btn_Save_AccountHead_Click(object sender, EventArgs e)
    {
        int Duplicate=0;
        
            for ( int i=0;i<= GridView_AccountHead.Rows.Count -1;i++ )
            {
                GridViewRow dg = GridView_AccountHead.Rows[i];   
                HiddenField hfd;
                Label l1;
                HiddenField hfd1;
                hfd = (HiddenField)dg.FindControl("HiddenAccountHeadname");
                hfd1 = (HiddenField)dg.FindControl("HiddenAccountHeadId");
                l1 = (Label)dg.FindControl("lblcostcentrename");
                if (HiddenAccountHead.Value.Trim() != hfd1.Value.Trim())
                {
                    if (hfd.Value.ToString().ToUpper().Trim() == txtAccountHeadName.Text.ToUpper().Trim())
                    {
                        lbl_AccountHead_Message.Text = "A/c Name Already Entered.";
                        Duplicate = 1;
                        break;
                    }
                    else if (txt_cls.Text.Trim() == l1.Text.Trim() )
                    {
                        lbl_AccountHead_Message.Text = "A/c No. Already Entered(CLS).";
                        Duplicate = 1;
                        break;
                    }
                    else if (txtAccountHeadNumber.Text.Trim() == dg.Cells[5].Text.Trim())
                    {
                        lbl_AccountHead_Message.Text = "A/c Code Already Entered(Cost +).";
                        Duplicate = 1;
                        break;
                    }
                }
                else
                {
                    lbl_AccountHead_Message.Text = "";
                }
            }
            if (Duplicate == 0)
            {
                int intAccountHeadId = -1;
                int intCreatedBy = 0;
                int intModifiedBy = 0;
                char charIncludeInBudgets;
                char recoverablecost;
                if (HiddenAccountHead.Value.ToString().Trim() == "")
                {
                    intCreatedBy = Convert.ToInt32(Session["loginid"].ToString());
                }
                else
                {
                    intAccountHeadId = Convert.ToInt32(HiddenAccountHead.Value);
                    intModifiedBy = Convert.ToInt32(Session["loginid"].ToString());
                }
               
                string strAccountHeadNumberCLS = txt_cls.Text;
                string strAccountHeadName = txtAccountHeadName.Text;
                string strAccountHeadNumber = txtAccountHeadNumber.Text;
                char charAccountHeadType = Convert.ToChar(ddlAccountHeadType.SelectedValue);
                if (chkIncludeInBudget.Checked == true)
                {
                    charIncludeInBudgets = 'Y';
                }
                else
                {
                    charIncludeInBudgets = 'N';
                }
                if (chkrecoverablecost.Checked == true)
                {
                    recoverablecost = 'Y';

                }
                else
                {
                    recoverablecost = 'N';
                }
                char charStatusId = Convert.ToChar(ddlStatus_AccountHead.SelectedValue);

                AccountHead.insertUpdateAccountHeadDetails("InsertUpdateAccountHeadDetails",
                                              intAccountHeadId,
                                              strAccountHeadNumberCLS,
                                              strAccountHeadNumber,
                                              strAccountHeadName,
                                              charAccountHeadType,
                                              charIncludeInBudgets,
                                              recoverablecost, 
                                              intCreatedBy,
                                              intModifiedBy,
                                              charStatusId);

                bindAccountHeadGrid();
                lbl_AccountHead_Message.Text = "Record Successfully Saved.";
             
                Alerts.HidePanel(pnl_AccountHead);
                Alerts.HANDLE_AUTHORITY(3, btn_Add_AccountHead, btn_Save_AccountHead, btn_Cancel_AccountHead, btn_Print_AccountHead, Auth);    
            }
    }
    protected void btn_Cancel_AccountHead_Click(object sender, EventArgs e)
    {
        GridView_AccountHead.SelectedIndex = -1;
       
        Alerts.HidePanel(pnl_AccountHead);
        Alerts.HANDLE_AUTHORITY(6, btn_Add_AccountHead, btn_Save_AccountHead, btn_Cancel_AccountHead, btn_Print_AccountHead, Auth);     
    }
    protected void btn_Print_AccountHead_Click(object sender, EventArgs e)
    {

    }
    protected void GridView_AccountHead_DataBound(object sender, EventArgs e)
    {
        Alerts.HANDLE_GRID(GridView_AccountHead, Auth);  
    }
    protected void Show_Record_AccountHead(int AccountHeadid)
    {
        HiddenAccountHead.Value = AccountHeadid.ToString();
        DataTable dt3 = AccountHead.selectDataAccountHeadDetailsByAccountHeadId(AccountHeadid);
        foreach (DataRow dr in dt3.Rows)
        {
            txt_cls.Text = dr["AccountHeadnumberCLS"].ToString();
            txtAccountHeadNumber.Text = dr["AccountHeadnumberCostPlus"].ToString();
            txtAccountHeadName.Text = dr["AccountHeadName"].ToString();
            ddlAccountHeadType.SelectedValue = dr["AccountHeadType"].ToString();
            if (Convert.ToChar(dr["IncludeInBudgets"]) == 'Y')
            {
                chkIncludeInBudget.Checked = true;
            }
            else
            {
                chkIncludeInBudget.Checked = false;
            }

            if (Convert.ToChar(dr["RecoverableCost"]) == 'Y')
            {
                chkrecoverablecost.Checked = true;
            }
            else
            {
                chkrecoverablecost.Checked = false;
            }

            txtCreatedBy_AccountHead.Text = dr["CreatedBy"].ToString();
            txtCreatedOn_AccountHead.Text = dr["CreatedOn"].ToString();
            txtModifiedBy_AccountHead.Text = dr["ModifiedBy"].ToString();
            txtModifiedOn_AccountHead.Text = dr["ModifiedOn"].ToString();
            ddlStatus_AccountHead.SelectedValue = dr["StatusId"].ToString();
        }
    }
   
    protected void GridView_AccountHead_SelectedIndexChanged(object sender, EventArgs e)
    {
        HiddenField hfdAccountHead;
        hfdAccountHead = (HiddenField)GridView_AccountHead.Rows[GridView_AccountHead.SelectedIndex].FindControl("HiddenAccountHeadId");
        id = Convert.ToInt32(hfdAccountHead.Value.ToString());
        Show_Record_AccountHead(id);
      
        Alerts.ShowPanel(pnl_AccountHead);
        Alerts.HANDLE_AUTHORITY(4, btn_Add_AccountHead, btn_Save_AccountHead, btn_Cancel_AccountHead, btn_Print_AccountHead, Auth);     
  
    }
    
    protected void GridView_AccountHead_Row_Editing(object sender, GridViewEditEventArgs e)
    {
        HiddenField hfdAccountHead;
        hfdAccountHead = (HiddenField)GridView_AccountHead.Rows[e.NewEditIndex].FindControl("HiddenAccountHeadId");
        id = Convert.ToInt32(hfdAccountHead.Value.ToString());
        Show_Record_AccountHead(id);
        GridView_AccountHead.SelectedIndex = e.NewEditIndex;
       
        Alerts.ShowPanel(pnl_AccountHead);
        Alerts.HANDLE_AUTHORITY(5, btn_Add_AccountHead, btn_Save_AccountHead, btn_Cancel_AccountHead, btn_Print_AccountHead, Auth);     
      }
 
    protected void GridView_AccountHead_Row_Deleting(object sender, GridViewDeleteEventArgs e)
    {
        int intModifiedBy = Convert.ToInt32(Session["loginid"].ToString());
        HiddenField hfdAccountHead;
        hfdAccountHead = (HiddenField)GridView_AccountHead.Rows[e.RowIndex].FindControl("HiddenAccountHeadId");
        id = Convert.ToInt32(hfdAccountHead.Value.ToString());
        AccountHead.deleteAccountHeadDetailsById("DeleteAccountHeadDetailsById", id, intModifiedBy);
        bindAccountHeadGrid();
        if (HiddenAccountHead.Value.ToString() == hfdAccountHead.Value.ToString())
        {
            btn_Add_AccountHead_Click(sender, e);
        }
    }
    protected void GridView_AccountHead_PreRender(object sender, EventArgs e)
    {
        if (GridView_AccountHead.Rows.Count <= 0) { lbl_GridView_AccountHead.Text = "No Records Found..!"; }
    }
    protected void btn_Search_Click(object sender, EventArgs e)
    {
        bindAccountHeadGrid();
    }

    protected void GridView_AccountHead_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        btn_Cancel_AccountHead_Click(btn_Cancel_AccountHead, null);
    }

    protected void GridView_AccountHead_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Modify")
        {
            GridViewRow row = (GridViewRow)(((ImageButton)sender).NamingContainer);
            int Rowindx = row.RowIndex;
            Mode = "Edit";
            HiddenField hfdAccountHead;
            hfdAccountHead = (HiddenField)GridView_AccountHead.Rows[Rowindx].FindControl("hdnAccountHeadId");
            id = Convert.ToInt32(hfdAccountHead.Value.ToString());
            Show_Record_AccountHead(id);

            Alerts.ShowPanel(pnl_AccountHead);
            Alerts.HANDLE_AUTHORITY(4, btn_Add_AccountHead, btn_Save_AccountHead, btn_Cancel_AccountHead, btn_Print_AccountHead, Auth);
        }
            
    }
    protected void btnEditAccountHead_click(object sender, ImageClickEventArgs e)
    {
        GridViewRow row = (GridViewRow)(((ImageButton)sender).NamingContainer);
        int Rowindx = row.RowIndex;
        Mode = "Edit";
        HiddenField hfdAccountHead;
        hfdAccountHead = (HiddenField)GridView_AccountHead.Rows[Rowindx].FindControl("hdnAccountHeadId");
        id = Convert.ToInt32(hfdAccountHead.Value.ToString());
        Show_Record_AccountHead(id);

        Alerts.ShowPanel(pnl_AccountHead);
        Alerts.HANDLE_AUTHORITY(4, btn_Add_AccountHead, btn_Save_AccountHead, btn_Cancel_AccountHead, btn_Print_AccountHead, Auth);
    }
    }
