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

public partial class Registers_ManningAgent : System.Web.UI.Page
{
    int id;
    Authority Auth;
    string Mode = "New";
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        lbl_SeminarCat_Message.Text = "";
        lblSeminarCat.Text = "";


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
           
            BindGridMinningAgent();
            Alerts.HidePanel(SeminarCatpanel);
            Alerts.HANDLE_AUTHORITY(1, btn_add, btn_save, btn_Cancel, btn_Print, Auth);

        }
    }
   
    private void BindGridMinningAgent()
    {
        string sql = "Select SeminarCatId, SeminarCatName from DBO.tbl_SeminarCat with(nolock) ORDER BY SeminarCatName";
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        this.GvSeminarCat.DataSource = dt;
        this.GvSeminarCat.DataBind();
    }

    protected void btn_save_Click(object sender, EventArgs e)
    {
        int Duplicate = 0;

        foreach (GridViewRow dg in GvSeminarCat.Rows)
        {
            HiddenField hfd;
            HiddenField hfd1;
            hfd = (HiddenField)dg.FindControl("hdnSeminarCatName");
            hfd1 = (HiddenField)dg.FindControl("hdnSeminarCatId");

            if (hfd.Value.ToString().ToUpper().Trim() == txtSeminarCat.Text.ToUpper().Trim())
            {
                if (hdnSeminarCatpk.Value.Trim() == "")
                {
                    lbl_SeminarCat_Message.Text = "Already Entered.";
                    Duplicate = 1;
                    break;
                }
                else if (hdnSeminarCatpk.Value.Trim() != hfd1.Value.ToString())
                {
                    lbl_SeminarCat_Message.Text = "Already Entered.";
                    Duplicate = 1;
                    break;
                }
            }
            else
            {
                lbl_SeminarCat_Message.Text = "";
            }
        }
        if (Duplicate == 0)
        {
            int SeminarcatId = -1;
            

            string strSeminarCat = txtSeminarCat.Text.Trim();
           
            if (hdnSeminarCatpk.Value.Trim() != "")
            {
                SeminarcatId = Convert.ToInt32(hdnSeminarCatpk.Value);
            }

           string sql = "EXEC DBO.InsertUpdateSeminarCategory " + SeminarcatId + ",'"  + txtSeminarCat.Text.Trim() + "'";
            Common.Execute_Procedures_Select_ByQueryCMS(sql);

            
            BindGridMinningAgent();
            lbl_SeminarCat_Message.Text = "Record Successfully Saved.";
            Alerts.HidePanel(SeminarCatpanel);
            Alerts.HANDLE_AUTHORITY(3, btn_add, btn_save, btn_Cancel, btn_Print, Auth);

        }
    }

    protected void btn_add_Click(object sender, EventArgs e)
    {
        hdnSeminarCatpk.Value = "";
        txtSeminarCat.Text = "";
       
        GvSeminarCat.SelectedIndex = -1;
        
        Alerts.ShowPanel(SeminarCatpanel);
        Alerts.HANDLE_AUTHORITY(2, btn_add, btn_save, btn_Cancel, btn_Print, Auth);
    }

    protected void GvSeminarCat_SelectIndexChanged(object sender, EventArgs e)
    {
        HiddenField hfdSeminarCat;
        hfdSeminarCat = (HiddenField)GvSeminarCat.Rows[GvSeminarCat.SelectedIndex].FindControl("hdnSeminarCatId");
        id = Convert.ToInt32(hfdSeminarCat.Value.ToString());
        Show_Record_SeminarCat(id);
        Alerts.ShowPanel(SeminarCatpanel);
        Alerts.HANDLE_AUTHORITY(4, btn_add, btn_save, btn_Cancel,btn_Print, Auth);
    }

    protected void Show_Record_SeminarCat(int SeminarCatId)
    {

        hdnSeminarCatpk.Value = SeminarCatId.ToString();
        string sql = "Select SeminarCatId, SeminarCatName from DBO.tbl_SeminarCat with(nolock) where SeminarCatId = " + SeminarCatId + " ORDER BY SeminarCatName";
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        foreach (DataRow dr in dt.Rows)
        {
            txtSeminarCat.Text = dr["SeminarCatName"].ToString();
            
            
        }

    }

    protected void btn_Print_Click(object sender, EventArgs e)
    {

    }

    protected void btn_Cancel_Click(object sender, EventArgs e)
    {
        GvSeminarCat.SelectedIndex = -1;
        Alerts.HidePanel(SeminarCatpanel);
        Alerts.HANDLE_AUTHORITY(6, btn_add, btn_save, btn_Cancel, btn_Print, Auth);
    }

    protected void GvSeminarCat_Row_Deleting(object sender, GridViewDeleteEventArgs e)
    {
        
        HiddenField hfdSeminarCatId;
        hfdSeminarCatId = (HiddenField)GvSeminarCat.Rows[e.RowIndex].FindControl("hdnSeminarCatId");
        id = Convert.ToInt32(hfdSeminarCatId.Value.ToString());
        string sql = "Delete from tbl_SeminarCat where SeminarCatId = "+ id + "";
        Common.Execute_Procedures_Select_ByQueryCMS(sql);
        BindGridMinningAgent();
        if (hdnSeminarCatpk.Value.Trim() == hfdSeminarCatId.Value.ToString())
        {
            btn_add_Click(sender, e);
        }
    }

    //protected void GvManningAgent_Row_Editing(object sender, GridViewEditEventArgs e)
    //{
    //    Mode = "Edit";
    //    HiddenField hfdrank;
    //    hfdrank = (HiddenField)GvManningAgent.Rows[e.NewEditIndex].FindControl("HiddenManningAgentId");
    //    id = Convert.ToInt32(hfdrank.Value.ToString());

    //    Show_Record_ManningAgent(id);
    //    GvManningAgent.SelectedIndex = e.NewEditIndex;
    //    Alerts.ShowPanel(Departmentpanel);
    //    Alerts.HANDLE_AUTHORITY(5, btn_add, btn_save, btn_Cancel, btn_Print, Auth);
    //}

    protected void GvSeminarCat_DataBound(object sender, EventArgs e)
    {
        Alerts.HANDLE_GRID(GvSeminarCat, Auth);
    }

    protected void GvSeminarCat_PreRender(object sender, EventArgs e)
    {
        if (this.GvSeminarCat.Rows.Count <= 0)
        {
            lblSeminarCat.Text = "No Records Found..!";
        }
    }

    protected void GvSeminarCat_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Modify")
        {
            GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
            int Rowindx = row.RowIndex;
            Mode = "Edit";
            HiddenField hfdrank;
            hfdrank = (HiddenField)GvSeminarCat.Rows[Rowindx].FindControl("hdnSeminarCatId");
            id = Convert.ToInt32(hfdrank.Value.ToString());

            Show_Record_SeminarCat(id);
            GvSeminarCat.SelectedIndex = Rowindx;
            Alerts.ShowPanel(SeminarCatpanel);
            Alerts.HANDLE_AUTHORITY(5, btn_add, btn_save, btn_Cancel, btn_Print, Auth);
        }
    }

    protected void GvSeminarCat_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {

    }

    protected void btnEditSeminarCat_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow row = (GridViewRow)(((ImageButton)sender).NamingContainer);
        int Rowindx = row.RowIndex;
        Mode = "Edit";
        HiddenField hfdrank;
        hfdrank = (HiddenField)GvSeminarCat.Rows[Rowindx].FindControl("hdnSeminarCatId");
        id = Convert.ToInt32(hfdrank.Value.ToString());

        Show_Record_SeminarCat(id);
        GvSeminarCat.SelectedIndex = Rowindx;
        // Alerts.ShowPanel(Departmentpanel);
        // Alerts.HANDLE_AUTHORITY(5, btn_add, btn_save, btn_Cancel, btn_Print, Auth);

        SeminarCatpanel.Visible = true;
        btn_Cancel.Visible = true;
        btn_save.Visible = true;
        btn_add.Visible = false;
    }
}