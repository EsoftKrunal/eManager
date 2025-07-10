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

public partial class Registers_PerfBonus : System.Web.UI.Page
{
    int id;
    Authority Auth;
    string Mode = "New";
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        lbl_GridView_PB.Text = "";
        lbl_PB_Message.Text = "";
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
            bindPerformanceGrid();             
            Alerts.HidePanel(pnl_PB);
            Alerts.HANDLE_AUTHORITY(1, btn_Add_PB, btn_Save_PB, btn_Cancel_PB, btn_Print_PB,Auth);     
        }
    }
    public void bindPerformanceGrid()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("SELECT TableId, AppDate,Bonus1,Bonus2,Bonus3 FROM PerformanceBonus ORDER BY AppDate DESC ");
        this.GridView_PB.DataSource = dt;
        this.GridView_PB.DataBind();
    }
    
    
    protected void btn_Add_PB_Click(object sender, EventArgs e)
    {
        txtStartDt.Text = "";
        txtBonus1.Text = "";
        txtBonus2.Text = "";
        txtBonus3.Text = "";
        GridView_PB.SelectedIndex = -1;
        HiddenPB.Value = "";
        //----------------------
        Alerts.ShowPanel(pnl_PB);
        Alerts.HANDLE_AUTHORITY(2, btn_Add_PB, btn_Save_PB, btn_Cancel_PB, btn_Print_PB, Auth);    
    }
    protected void btn_Save_PB_Click(object sender, EventArgs e)
    {
        DateTime s;
        if (!DateTime.TryParse(txtStartDt.Text.Trim(), out s))
        {
            lbl_PB_Message.Text = "Please enter valid date.";
            txtStartDt.Focus();
            return;
        }

        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("SELECT * FROM PerformanceBonus WHERE AppDate = '" + txtStartDt.Text.Trim() + "'");

        if (dt != null && dt.Rows.Count > 0)
        {
            lbl_PB_Message.Text = "Record already exists for this date.";
            txtStartDt.Focus();
            return;
        }

        try
        {
            Common.Set_Procedures("InsertUpdatePerformanceBonusDetails");
            Common.Set_ParameterLength(5);
            Common.Set_Parameters(new MyParameter("@TableId", Common.CastAsInt32(HiddenPB.Value)),
                new MyParameter("@AppDate", txtStartDt.Text.Trim()),
                new MyParameter("@Bonus1", txtBonus1.Text.Trim()),
                new MyParameter("@Bonus2", txtBonus2.Text.Trim()),
                new MyParameter("@Bonus3", txtBonus3.Text.Trim())
                );
            DataSet ds = new DataSet();
            if (Common.Execute_Procedures_IUD_CMS(ds))
            {
                bindPerformanceGrid();
                lbl_PB_Message.Text = "Record Successfully Saved.";

                Alerts.HidePanel(pnl_PB);
                Alerts.HANDLE_AUTHORITY(3, btn_Add_PB, btn_Save_PB, btn_Cancel_PB, btn_Print_PB, Auth); 
            }
            else
            {
                lbl_PB_Message.Text = "Unable to save record. Error : " + Common.getLastError();
            }            
        }
        catch (Exception ex)
        {
            lbl_PB_Message.Text = "Unable to save record. Error : " + ex.Message;
        }

        
    }
    protected void btn_Cancel_PB_Click(object sender, EventArgs e)
    {
        GridView_PB.SelectedIndex = -1;
       
        Alerts.HidePanel(pnl_PB);
        Alerts.HANDLE_AUTHORITY(6, btn_Add_PB, btn_Save_PB, btn_Cancel_PB, btn_Print_PB, Auth);     
    }   
    protected void GridView_PB_DataBound(object sender, EventArgs e)
    {
        Alerts.HANDLE_GRID(GridView_PB, Auth);  
    }
    protected void Show_Record_PB(int PBid)
    {
        HiddenPB.Value = PBid.ToString();
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("SELECT AppDate,Bonus1,Bonus2,Bonus3 FROM PerformanceBonus WHERE TableId = " + PBid);
        foreach (DataRow dr in dt.Rows)
        {
            txtStartDt.Text = Common.ToDateString(dr["AppDate"]);
            txtBonus1.Text = dr["Bonus1"].ToString();
            txtBonus2.Text = dr["Bonus2"].ToString();
            txtBonus3.Text = dr["Bonus3"].ToString();            
        }
    }
   
    protected void GridView_PB_SelectedIndexChanged(object sender, EventArgs e)
    {
        int id = Common.CastAsInt32(GridView_PB.DataKeys[GridView_PB.SelectedIndex].Value);
        Show_Record_PB(id);
      
        Alerts.ShowPanel(pnl_PB);
        Alerts.HANDLE_AUTHORITY(4, btn_Add_PB, btn_Save_PB, btn_Cancel_PB, btn_Print_PB, Auth);     
  
    }
    
    protected void GridView_PB_Row_Editing(object sender, GridViewEditEventArgs e)
    {
        int id = Common.CastAsInt32(GridView_PB.DataKeys[e.NewEditIndex].Value);  
        Show_Record_PB(id);
        //GridView_PB.SelectedIndex = e.NewEditIndex;
        GridView_PB.EditIndex = -1;
       
        Alerts.ShowPanel(pnl_PB);
        Alerts.HANDLE_AUTHORITY(5, btn_Add_PB, btn_Save_PB, btn_Cancel_PB, btn_Print_PB, Auth);     
      }

    protected void GridView_PB_Row_Deleting(object sender, GridViewDeleteEventArgs e)
    {
        int id = Common.CastAsInt32(GridView_PB.DataKeys[e.RowIndex].Value); 

        try
        {
            Common.Execute_Procedures_Select_ByQueryCMS("DELETE FROM PerformanceBonus WHERE TableId = " + id);
            lbl_PB_Message.Text = "Record deleted successfully.";
            bindPerformanceGrid();
            btn_Add_PB_Click(sender, e);
        }
        catch (Exception ex)
        {
            lbl_PB_Message.Text = "Unable to delete record. Error : " + ex.Message;
        }
        //if (HiddenPB.Value.ToString() == hfdAccountHead.Value.ToString())
        //{
        //    btn_Add_PB_Click(sender, e);
        //}
    }
    protected void GridView_PB_PreRender(object sender, EventArgs e)
    {
        if (GridView_PB.Rows.Count <= 0) { lbl_GridView_PB.Text = "No Records Found..!"; }
    }
    protected void btn_Search_Click(object sender, EventArgs e)
    {
        bindPerformanceGrid();
    }
    protected void btn_Print_PB_Click(object sender, EventArgs e)
    {

    }

    protected void GridView_PB_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Modify")
        {
            GridViewRow row = (GridViewRow)(((ImageButton)sender).NamingContainer);
            int Rowindx = row.RowIndex;
            Mode = "Edit";
            int id = Common.CastAsInt32(GridView_PB.DataKeys[Rowindx].Value);
            Show_Record_PB(id);

            Alerts.ShowPanel(pnl_PB);
            Alerts.HANDLE_AUTHORITY(4, btn_Add_PB, btn_Save_PB, btn_Cancel_PB, btn_Print_PB, Auth);
        }
    }
    protected void btnEditPerfBonus_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow row = (GridViewRow)(((ImageButton)sender).NamingContainer);
        int Rowindx = row.RowIndex;
        Mode = "Edit";
        int id = Common.CastAsInt32(GridView_PB.DataKeys[Rowindx].Value);
        Show_Record_PB(id);

        Alerts.ShowPanel(pnl_PB);
        Alerts.HANDLE_AUTHORITY(4, btn_Add_PB, btn_Save_PB, btn_Cancel_PB, btn_Print_PB, Auth);
    }
}
