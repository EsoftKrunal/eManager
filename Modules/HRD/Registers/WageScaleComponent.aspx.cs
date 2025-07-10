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

public partial class CrewOperation_WageScaleComponent : System.Web.UI.Page
{
    int id;
    Authority Auth;
    string Mode = "New";
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        lbl_WageScaleComponent_Message.Text = "";
        lbl_GridView_WageScaleComponent.Text = "";
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
            bindWageScaleComponentGrid();
            bindStatusDDL();
            ddl_Account.DataSource=cls_SearchReliever.getAccountHead(); 
            ddl_Account.DataValueField= "AccountID";
            ddl_Account.DataTextField = "AccountName";
            ddl_Account.DataBind(); 
            ddl_Account.Items.Insert (0, new ListItem("< Select >","0"));
            btn_Add.Visible = Auth.isAdd;
            btn_Cancel.Visible = false;
            btn_Print.Visible = false;
            btn_Save.Visible = false;
            
        }
    }
  
    public void bindWageScaleComponentGrid()
    {
        DataTable dt1 = WageScaleComponent.selectWageScaleComponentDetails();
        this.GridView_WageScaleComponent.DataSource = dt1;
        this.GridView_WageScaleComponent.DataBind();
    }
    public void bindStatusDDL()
    {
        DataTable dt2 = WageScaleComponent.selectDataStatusDetails();
        this.ddlStatus_WageScaleComponent.DataValueField = "StatusId";
        this.ddlStatus_WageScaleComponent.DataTextField = "StatusName";
        this.ddlStatus_WageScaleComponent.DataSource = dt2;
        this.ddlStatus_WageScaleComponent.DataBind();
    }
    protected void btn_Add_Click(object sender, EventArgs e)
    {
        txtComponentName.Text = "";
        ddl_ComponentType.SelectedIndex = 0;
        txtCreatedBy_WageScaleComponent.Text = "";
        txtCreatedOn_WageScaleComponent.Text = "";
        txtModifiedBy_WageScaleComponent.Text = "";
        txtModifiedOn_WageScaleComponent.Text = "";
        ddlStatus_WageScaleComponent.SelectedIndex = 0;
        HiddenWageScaleComponent.Value = "";
        pnl_WageScaleComponent.Visible = true;
        btn_Save.Visible = Auth.isEdit;
        btn_Cancel.Visible = true;
      
        btn_Print.Visible = Auth.isPrint;
        btn_Add.Visible = false;
        
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        int Duplicate=0;
        //**** if condition to check account head when status is active
        if (this.ddlStatus_WageScaleComponent.SelectedValue == "A" & this.ddl_Account.SelectedValue=="0")
        {
           
            this.lbl_WageScaleComponent_Message.Text = "Select Any Account Head";
            return;

        }

      foreach (GridViewRow dg in GridView_WageScaleComponent.Rows)
            {
                HiddenField hfd;
                HiddenField hfd1;
                hfd = (HiddenField)dg.FindControl("HiddenWageScaleComponentName");
                hfd1 = (HiddenField)dg.FindControl("HiddenWageScaleComponentId");

                if (hfd.Value.ToString().ToUpper().Trim() == txtComponentName.Text.ToUpper().Trim())
                {
                    if (HiddenWageScaleComponent.Value.Trim() == "")
                    {
                        
                        lbl_WageScaleComponent_Message.Text = "Already Entered.";
                        Duplicate = 1;
                        break;
                    }
                    else if (HiddenWageScaleComponent.Value.Trim() != hfd1.Value.ToString())
                    {
                       
                        lbl_WageScaleComponent_Message.Text = "Already Entered.";
                        Duplicate = 1;
                        break;
                    }
                }
                else
                {
                   
                    lbl_WageScaleComponent_Message.Text = "";
                }
            }
        //************ To Check Wheter Comonent not use anywhere if its staus is changed to be deleted
            //DataTable dt3=WageScaleComponent.selectCheckWageScaleComponentId(Convert.ToInt32(HiddenWageScaleComponent.Value));
            //if (dt3.Rows.Count > 0)
            //{
              
            //    lbl_WageScaleComponent_Message.Text = "Status Can't be changed because it used for some wage scale";
            //    Duplicate = 1;
            
            //}
        //************
            if (Duplicate == 0)
            {
                int intWageScaleComponentId = -1;
                int AccountCode = 0; ;
                int intCreatedBy = 0;
                int intModifiedBy = 0;
                if (HiddenWageScaleComponent.Value.ToString().Trim() == "")
                {
                    intCreatedBy = Convert.ToInt32(Session["loginid"].ToString());
                }
                else
                {
                    intWageScaleComponentId = Convert.ToInt32(HiddenWageScaleComponent.Value);
                    intModifiedBy = Convert.ToInt32(Session["loginid"].ToString());
                }
                string strComponentName = txtComponentName.Text;
                char charStatusId = Convert.ToChar(ddlStatus_WageScaleComponent.SelectedValue);
                string charComponentType = Convert.ToString(ddl_ComponentType.SelectedValue);
                if (this.ddlStatus_WageScaleComponent.SelectedValue == "D" || this.ddlStatus_WageScaleComponent.SelectedValue == "OD" || this.ddlStatus_WageScaleComponent.SelectedValue == "OE")
                {
                    AccountCode = 0;
                }
                else
                {
                    AccountCode = Convert.ToInt32(ddl_Account.SelectedValue);
                }
                
                WageScaleComponent.insertUpdateWageScaleComponentDetails("InsertUpdateWageScaleComponentDetails",
                                              AccountCode,
                                              intWageScaleComponentId,
                                              strComponentName,
                                              charComponentType,
                                              intCreatedBy,
                                              intModifiedBy,
                                              charStatusId);

                bindWageScaleComponentGrid();
           
                btn_Add_Click(sender, e);
                btn_Cancel_Click(sender, e);
             
                lbl_WageScaleComponent_Message.Text = "Record Successfully Saved.";
                btn_Save.Visible = false;
                btn_Cancel.Visible = false;
                btn_Print.Visible = false;
            btn_Add.Visible = true;
            }
    }
    protected void btn_Cancel_Click(object sender, EventArgs e)
    {
      
        pnl_WageScaleComponent.Visible = false;
        btn_Save.Visible = false;
        btn_Cancel.Visible = false;
     
        GridView_WageScaleComponent.SelectedIndex = -1;
       
        btn_Print.Visible = false;
    }
    protected void btn_Print_Click(object sender, EventArgs e)
    {

    }
    protected void GridView_WageScaleComponent_DataBound(object sender, EventArgs e)
    {
        try
        {
            GridView_WageScaleComponent.Columns[1].Visible = Auth.isEdit;
        }
        catch
        {
            GridView_WageScaleComponent.Columns[1].Visible = false;
        }
    }
    protected void Show_Record_WageScaleComponent(int WageScaleComponentid)
    {
        string Mess = "";
       
        HiddenWageScaleComponent.Value = WageScaleComponentid.ToString();
        DataTable dt3 = WageScaleComponent.selectDataWageScaleComponentDetailsByWageScaleComponentId(WageScaleComponentid);
        foreach (DataRow dr in dt3.Rows)
        {
            txtComponentName.Text=dr["ComponentName"].ToString();
         
            Mess = Mess + Alerts.Set_DDL_Value(ddl_Account, dr["AccountHeadId"].ToString(), "Account Head");
            ddl_ComponentType.SelectedValue = dr["ComponentType"].ToString().Trim();
            
            txtCreatedBy_WageScaleComponent.Text=dr["CreatedBy"].ToString();
            txtCreatedOn_WageScaleComponent.Text=dr["CreatedOn"].ToString();
            txtModifiedBy_WageScaleComponent.Text=dr["ModifiedBy"].ToString();
            txtModifiedOn_WageScaleComponent.Text=dr["ModifiedOn"].ToString();
            ddlStatus_WageScaleComponent.SelectedValue=dr["StatusId"].ToString();
        }
        btn_Print.Visible = Auth.isPrint;
       
        
    }
    // VIEW THE RECORD
    protected void GridView_WageScaleComponent_SelectedIndexChanged(object sender, EventArgs e)
    {

        HiddenField hfdWageScaleComponent;
        hfdWageScaleComponent = (HiddenField)GridView_WageScaleComponent.Rows[GridView_WageScaleComponent.SelectedIndex].FindControl("HiddenWageScaleComponentId");
        id = Convert.ToInt32(hfdWageScaleComponent.Value.ToString());
        pnl_WageScaleComponent.Visible = true;
        Show_Record_WageScaleComponent(id);
     
        btn_Save.Visible = false;
        btn_Cancel.Visible = true;
    }
    //EDIT THE RECORD
    protected void GridView_WageScaleComponent_Row_Editing(object sender, GridViewEditEventArgs e)
    {
      
        HiddenField hfdWageScaleComponent;
        hfdWageScaleComponent = (HiddenField)GridView_WageScaleComponent.Rows[e.NewEditIndex].FindControl("HiddenWageScaleComponentId");
        id = Convert.ToInt32(hfdWageScaleComponent.Value.ToString());
        Show_Record_WageScaleComponent(id);
        GridView_WageScaleComponent.SelectedIndex = e.NewEditIndex;
        pnl_WageScaleComponent.Visible = true;
     
        btn_Save.Visible = false;
        btn_Cancel.Visible = true;
    }
    // DELETE THE RECORD
    protected void GridView_WageScaleComponent_Row_Deleting(object sender, GridViewDeleteEventArgs e)
    {
        int modifiedBy = Convert.ToInt32(Session["loginid"].ToString());
        HiddenField hfdWageScaleComponent;
        hfdWageScaleComponent = (HiddenField)GridView_WageScaleComponent.Rows[e.RowIndex].FindControl("HiddenWageScaleComponentId");
        id = Convert.ToInt32(hfdWageScaleComponent.Value.ToString());
        WageScaleComponent.deleteWageScaleComponentDetailsById("DeleteWageScaleComponentDetailsById", id, modifiedBy);
        bindWageScaleComponentGrid();
        if (HiddenWageScaleComponent.Value.ToString() == hfdWageScaleComponent.Value.ToString())
        {
            btn_Add_Click(sender, e);
        }
     
    }
    protected void GridView_WageScaleComponent_PreRender(object sender, EventArgs e)
    {
        if (GridView_WageScaleComponent.Rows.Count <= 0) { lbl_GridView_WageScaleComponent.Text = "No Records Found..!"; }
    }

    protected void btnEditWageScaleComponent_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow row = (GridViewRow)(((ImageButton)sender).NamingContainer);
        int Rowindx = row.RowIndex;
        Mode = "Edit";
        HiddenField hfdWageScaleComponent;
        hfdWageScaleComponent = (HiddenField)GridView_WageScaleComponent.Rows[Rowindx].FindControl("HiddenWageScaleComponentId");
        id = Convert.ToInt32(hfdWageScaleComponent.Value.ToString());
        pnl_WageScaleComponent.Visible = true;
        Show_Record_WageScaleComponent(id);

        btn_Save.Visible = Auth.isEdit;
        btn_Cancel.Visible = true;
    }

    protected void GridView_WageScaleComponent_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Modify")
        {
            GridViewRow row = (GridViewRow)(((ImageButton)sender).NamingContainer);
            int Rowindx = row.RowIndex;
            Mode = "Edit";
            HiddenField hfdWageScaleComponent;
            hfdWageScaleComponent = (HiddenField)GridView_WageScaleComponent.Rows[Rowindx].FindControl("HiddenWageScaleComponentId");
            id = Convert.ToInt32(hfdWageScaleComponent.Value.ToString());
            pnl_WageScaleComponent.Visible = true;
            Show_Record_WageScaleComponent(id);

            btn_Save.Visible =  Auth.isEdit;
            btn_Cancel.Visible = true;
        }
     }
}
