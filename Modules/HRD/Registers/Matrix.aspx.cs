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

public partial class Registers_Matrix : System.Web.UI.Page
{
    int id;
    Authority Auth;
    string Mode = "New";
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        this.lbl_GridView_VesselType.Text = "";
        this.lbl_message.Text = "";
        //*********************************
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 10);
        if (chpageauth <= 0)
        {
            Response.Redirect("Dummy.aspx");
        }
        //*********************************
        ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 10);
        OBJ.Invoke();
        Session["Authority"] = OBJ.Authority;
        Auth = OBJ.Authority;
        lbl_message.Text = "";
        if (Page.IsPostBack == false)
        {
            bindrank();
            bindstatus();
            bindheadermatrix();
            Alerts.HidePanel(this.pnl_Matrix);
            Alerts.HANDLE_AUTHORITY(1,this.btn_Add, this.btn_Save,this.btn_Cancel, this.btn_Print, Auth);     
        }
    }

    # region Page_Loaders

    private void bindrank()
    {
        DataSet dt = cls_SearchReliever.getMasterData("Rank", "RankCode", "Rankid");
        this.ddlrank.DataTextField = "RankCode";
        this.ddlrank.DataValueField = "RankId";
        this.ddlrank.DataSource = dt;
        this.ddlrank.DataBind();

        this.ddlrank2.DataTextField = "RankCode";
        this.ddlrank2.DataValueField = "RankId";
        this.ddlrank2.DataSource = dt;
        this.ddlrank2.DataBind();
    }

    public void bindstatus()
    {
        DataTable dt2 = Matrix.selectDataStatusDetails();
        this.ddlStatus.DataValueField = "StatusId";
        this.ddlStatus.DataTextField = "StatusName";
        this.ddlStatus.DataSource = dt2;
        this.ddlStatus.DataBind();
    }

    # endregion

    private void bindheadermatrix()
    {
        DataTable dt = Matrix.selectMatrixHeader(-1);
        this.gv_header.DataSource = dt;
        this.gv_header.DataBind();
    }

    private void bindgriddetails()
    {
        DataTable dt = ((DataTable)Session["dtdetails"]);
        this.gvdetails.DataSource = dt;
        this.gvdetails.DataBind(); 
    }

    protected void btn_Add_Click(object sender, EventArgs e)
    {        
        this.txtmatrixname.Text = "";
        this.ddlrank.SelectedIndex = 0;
        this.ddlrank2.SelectedIndex = 0;
        this.txtexperience.Text = "";
        this.txtexperience1.Text = "";
        this.txtexperience2.Text = "";
        this.ddlStatus.SelectedIndex = 0;
        this.txtCreatedBy.Text = "";
        this.txtCreatedOn.Text = "";
        this.txtModifiedBy.Text = "";
        this.txtModifiedOn.Text = "";
        DataTable dt = Matrix.selectMatrixDetail(-1);
        Session.Add("dtdetails", dt);
        bindgriddetails();
        hiddenmatrixid.Value = "";
        this.btanaddrank.Visible = true;
      
        Alerts.ShowPanel(pnl_Matrix);
        Alerts.HANDLE_AUTHORITY(2, btn_Add, btn_Save, btn_Cancel, btn_Print, Auth);    
    }

    protected void btanaddrank_Click(object sender, EventArgs e)
    {
        Boolean duplicate=false ;
        for (int i = 0; i < this.gvdetails.Rows.Count; i++)
        {
            if (gvdetails.DataKeys[i].Value.ToString() == this.ddlrank.SelectedValue.ToString())
            {
                lbl_message.Visible = true;
                lbl_message.Text = "This Rank Already Exists For This Matrix.";
                duplicate = true;
            }
        }
        if (duplicate == false)
        {
            DataTable dtt = ((DataTable)Session["dtdetails"]);
            DataRow dr = dtt.NewRow();
            dr["RankId"] = this.ddlrank.SelectedValue.ToString();
            dr["Rank1"] = this.ddlrank2.SelectedValue.ToString();
            dr["Experience"] = this.txtexperience.Text;
            dr["Experience1"] = this.txtexperience1.Text;
            dr["Experience2"] = this.txtexperience2.Text;
            dr["RankName"] = this.ddlrank.SelectedItem.Text.ToString();
            dr["Rank1_Name"] = this.ddlrank2.SelectedItem.Text.ToString();
            dtt.Rows.Add(dr);
            Session.Add("dtdetails", dtt);
            bindgriddetails();
        }
    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        try
        {
            int Duplicate=0;
        
            foreach (GridViewRow dg in gv_header.Rows)
            {
                HiddenField hfd;
                HiddenField hfd1;
                hfd = (HiddenField)dg.FindControl("HiddenMatrixName");
                hfd1 = (HiddenField)dg.FindControl("HiddenMatrixId");

                if (hfd.Value.ToString().ToUpper().Trim() == txtmatrixname.Text.ToUpper().Trim())
                {
                    if (hiddenmatrixid.Value.Trim() == "")
                    {
                        lbl_message.Text = "Already Entered.";
                        Duplicate = 1;
                        break;
                    }
                    else if (hiddenmatrixid.Value.Trim() != hfd1.Value.ToString())
                    {
                        lbl_message.Text = "Already Entered.";
                        Duplicate = 1;
                        break;
                    }
                }
                else
                {
                    lbl_message.Text = "";
                }
            }
            if (Duplicate == 0)
            {
                int matrixid = 0;
                int createdby = 0;
                int modifiedby = 0;
                int returnvalue = 0;
                int mode = 0;

                if (this.gvdetails.Rows.Count == 0)
                {
                    lbl_message.Text = "Enter Atleast One Rank For This Matrix.";
                }
                else
                {
                    if (this.hiddenmatrixid.Value == "")
                    {
                        matrixid = -1;
                        createdby = Convert.ToInt32(Session["loginId"].ToString());
                        mode = 0;
                    }
                    else
                    {
                        matrixid = Convert.ToInt32(this.hiddenmatrixid.Value);
                        modifiedby = Convert.ToInt32(Session["loginId"].ToString());
                        mode = 1;
                    }
                    Matrix.InsertMatrixHeader("InsertUpdateMatrixHeaders", matrixid, this.txtmatrixname.Text, Convert.ToChar(this.ddlStatus.SelectedValue.ToString()), createdby, modifiedby, out returnvalue);

                    for (int i = 0; i < this.gvdetails.Rows.Count; i++)
                    {
                        HiddenField hfd = (HiddenField)gvdetails.Rows[i].FindControl("hfd_Rank1");
                        int rank1 = Convert.ToInt32(hfd.Value);
                        Matrix.InsertUpdateMatrixDetails("InsertUpdateMatrixDetails", returnvalue, Convert.ToInt32(this.gvdetails.DataKeys[i].Value.ToString()), rank1, Convert.ToInt32(gvdetails.Rows[i].Cells[3].Text), Convert.ToInt32(gvdetails.Rows[i].Cells[4].Text), Convert.ToInt32(gvdetails.Rows[i].Cells[5].Text));
                    }
                    lbl_message.Text = "Record Saved Successfully.";
                    bindheadermatrix();
                   
                    this.btanaddrank.Visible = true;
                    btn_Add_Click(sender, e);
                    btn_Cancel_Click(sender, e);
                }            
            }
        }
        catch
        { 

        }

        Alerts.HidePanel(this.pnl_Matrix);
        Alerts.HANDLE_AUTHORITY(3, btn_Add, btn_Save, btn_Cancel, btn_Print, Auth);                                          
    }

    protected void gv_header_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int modifiedby = Convert.ToInt32(Session["loginid"].ToString());
           
            Matrix.deleteMatrixDetails("deleteMatrix", Convert.ToInt32(gv_header.DataKeys[e.RowIndex].Value.ToString()), modifiedby);
            bindheadermatrix();

            if (this.hiddenmatrixid.Value.Trim() == gv_header.DataKeys[e.RowIndex].Value.ToString())
            {
                btn_Add_Click(sender, e);
            }
            lbl_message.Text = "Record Deleted Successfully.";
        }
        catch
        { 

        }
    }

    protected void btn_Cancel_Click(object sender, EventArgs e)
    {
        gv_header.SelectedIndex = -1;
       
        Alerts.HidePanel(this.pnl_Matrix);
        Alerts.HANDLE_AUTHORITY(6, this.btn_Add, this.btn_Save, this.btn_Cancel, this.btn_Print, Auth);     
    }

    protected void gv_header_RowEditing(object sender, GridViewEditEventArgs e)
    {        
        showdata(e.NewEditIndex);
        this.hiddenmatrixid.Value = gv_header.DataKeys[e.NewEditIndex].Value.ToString();
        this.btanaddrank.Visible = true;
       
        Alerts.ShowPanel(this.pnl_Matrix);
        Alerts.HANDLE_AUTHORITY(5, this.btn_Add, btn_Save,  btn_Cancel, btn_Print, Auth);     
    }

    private void showdata(int i)
    {
        this.pnl_Matrix.Visible = true;
        DataTable dt = Matrix.selectMatrixHeader(Convert.ToInt32(gv_header.DataKeys[i].Value.ToString()) );
        foreach (DataRow dr in dt.Rows)
        {
            this.hiddenmatrixid.Value = dr["matrixid"].ToString();
            this.txtmatrixname.Text = dr["matrixname"].ToString();
            this.ddlrank.SelectedIndex  = 0;
            this.ddlrank2.SelectedIndex = 0;
            this.txtexperience.Text = "";
            this.txtCreatedBy.Text = dr["createdby"].ToString();
            this.txtCreatedOn.Text = dr["createdon"].ToString();
            this.txtModifiedBy.Text = dr["modifiedby"].ToString();
            this.txtModifiedOn.Text = dr["modifiedon"].ToString();
            if (dr["statusid"].ToString() == "Active")
            {
                this.ddlStatus.SelectedValue = "A";
            }
            else
            {
                this.ddlStatus.SelectedValue = "D";
            }
        }

        DataTable dtt = Matrix.selectMatrixDetail(Convert.ToInt32(gv_header.DataKeys[i].Value.ToString()));
        Session.Add("dtdetails", dtt);
        bindgriddetails();
        hiddenmatrixid.Value = gv_header.DataKeys[i].Value.ToString();
    }

    protected void gv_header_SelectedIndexChanged(object sender, EventArgs e)
    {
        showdata(Convert.ToInt32(gv_header.SelectedIndex));
        this.btanaddrank.Visible = false;
       
        Alerts.ShowPanel( pnl_Matrix);
        Alerts.HANDLE_AUTHORITY(4, btn_Add, btn_Save, btn_Cancel, btn_Print, Auth);     
    }

    protected void gv_header_PreRender(object sender, EventArgs e)
    {
        if (this.gv_header.Rows.Count <= 0)
        {
            lbl_message.Text = "No Record Found..!";
        }
        else
        {
            lbl_message.Text = "";
        }
    }

    protected void gvdetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DataTable dtt = ((DataTable)Session["dtdetails"]);
        DataRow dr = dtt.Rows[e.RowIndex];
        dr.Delete();
        Session.Add("dtdetails", dtt);
        bindgriddetails(); 
    }

    protected void gv_header_DataBound(object sender, EventArgs e)
    {
        Alerts.HANDLE_GRID(gv_header, Auth);  
    }

    protected void gv_header_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Modify")
        {
            GridViewRow row = (GridViewRow)(((ImageButton)sender).NamingContainer);
            int Rowindx = row.RowIndex;
            Mode = "Edit";
            HiddenField hfdpni;
            hfdpni = (HiddenField)gv_header.Rows[Rowindx].FindControl("hdnMatrixId");
            id = Convert.ToInt32(hfdpni.Value.ToString());
            showdata(id);
            this.hiddenmatrixid.Value = gv_header.DataKeys[Rowindx].Value.ToString();
            this.btanaddrank.Visible = true;

            Alerts.ShowPanel(this.pnl_Matrix);
            Alerts.HANDLE_AUTHORITY(5, this.btn_Add, btn_Save, btn_Cancel, btn_Print, Auth);
        }
    }

    protected void btnEditMatrix_click(object sender, EventArgs e)
    {
        GridViewRow row = (GridViewRow)(((ImageButton)sender).NamingContainer);
        int Rowindx = row.RowIndex;
        Mode = "Edit";
        HiddenField hfdpni;
        hfdpni = (HiddenField)gv_header.Rows[Rowindx].FindControl("hdnMatrixId");
        id = Convert.ToInt32(hfdpni.Value.ToString());
        showdata(id);
        this.hiddenmatrixid.Value = gv_header.DataKeys[Rowindx].Value.ToString();
        this.btanaddrank.Visible = true;

        Alerts.ShowPanel(this.pnl_Matrix);
        Alerts.HANDLE_AUTHORITY(5, this.btn_Add, btn_Save, btn_Cancel, btn_Print, Auth);
    }
    }
