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

public partial class Registers_EngineDesign : System.Web.UI.Page
{
    int id;
    Authority Auth;
    string Mode = "New";
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 5);
        OBJ.Invoke();
        Session["Authority"] = OBJ.Authority;
        Auth = OBJ.Authority;
        if (!Page.IsPostBack)
        {
            bindEngineDesignGrid();
            bindStatusDDL();
            HANDLE_AUTHORITY();
        }
    }
    private void HANDLE_AUTHORITY()
    {
        if (Mode == "New")
        {
            btn_Add_EngineDesign.Visible = ((Auth.isEdit || Auth.isAdd) && (Mode == "New" || Mode == "Edit"));
            btn_Save_EngineDesign.Visible = false;
            btn_Cancel_EngineDesign.Visible = false;
            pnl_EngineDesign.Visible = false;
        }
        else if (Mode == "Edit")
        {
            btn_Add_EngineDesign.Visible = ((Auth.isEdit || Auth.isAdd) && (Mode == "New" || Mode == "Edit"));
            btn_Save_EngineDesign.Visible = false;
            btn_Cancel_EngineDesign.Visible = false;
            pnl_EngineDesign.Visible = false;
        }
        else // Mode=View
        {
            btn_Add_EngineDesign.Visible = false;
            btn_Save_EngineDesign.Visible = false;
            btn_Cancel_EngineDesign.Visible = false;
            pnl_EngineDesign.Visible = false;
        }
    }
    public void bindEngineDesignGrid()
    {
        DataTable dt1 = EngineDesign.selectDataEngineDesignDetails();
        this.GridView_EngineDesign.DataSource = dt1;
        this.GridView_EngineDesign.DataBind();
    }
    public void bindStatusDDL()
    {
        DataTable dt2 = EngineDesign.selectDataStatusDetails();
        this.ddlStatus_EngineDesign.DataValueField = "StatusId";
        this.ddlStatus_EngineDesign.DataTextField = "StatusName";
        this.ddlStatus_EngineDesign.DataSource = dt2;
        this.ddlStatus_EngineDesign.DataBind();
    }
    protected void btn_Add_EngineDesign_Click(object sender, EventArgs e)
    {
        txtEngineDesignName.Text = "";
        txtCreatedBy_EngineDesign.Text = "";
        txtCreatedOn_EngineDesign.Text = "";
        txtModifiedBy_EngineDesign.Text = "";
        txtModifiedOn_EngineDesign.Text = "";
        ddlStatus_EngineDesign.SelectedIndex = 0;
        HiddenEngineDesign.Value = "";
        pnl_EngineDesign.Visible = true;
        btn_Save_EngineDesign.Visible = true;
        btn_Cancel_EngineDesign.Visible = true;
        btn_Add_EngineDesign.Visible = false;
        lbl_EngineDesign_Message.Visible = false;
        btn_Print_EngineDesign.Visible = true;
    }
    protected void btn_Save_EngineDesign_Click(object sender, EventArgs e)
    {
        int Duplicate=0;
        
            foreach (GridViewRow dg in GridView_EngineDesign.Rows)
            {
                HiddenField hfd;
                HiddenField hfd1;
                hfd = (HiddenField)dg.FindControl("HiddenEngineDesignName");
                hfd1 = (HiddenField)dg.FindControl("HiddenEngineDesignId");

                if (hfd.Value.ToString().ToUpper().Trim() == txtEngineDesignName.Text.ToUpper().Trim())
                {
                    if (HiddenEngineDesign.Value.Trim() == "")
                    {
                        lbl_EngineDesign_Message.Visible = true;
                        lbl_EngineDesign_Message.Text = "Already Entered.";
                        Duplicate = 1;
                        break;
                    }
                    else if (HiddenEngineDesign.Value.Trim() != hfd1.Value.ToString())
                    {
                        lbl_EngineDesign_Message.Visible = true;
                        lbl_EngineDesign_Message.Text = "Already Entered.";
                        Duplicate = 1;
                        break;
                    }
                }
                else
                {
                    lbl_EngineDesign_Message.Visible = false;
                    lbl_EngineDesign_Message.Text = "";
                }
            }
            if (Duplicate == 0)
            {
                int intEngineDesignId = -1;
                int intCreatedBy = 0;
                int intModifiedBy = 0;
                if (HiddenEngineDesign.Value.ToString().Trim() == "")
                {
                    intCreatedBy = Convert.ToInt32(Session["loginid"].ToString());
                }
                else
                {
                    intEngineDesignId = Convert.ToInt32(HiddenEngineDesign.Value);
                    intModifiedBy = Convert.ToInt32(Session["loginid"].ToString());
                }
                string strEngineDesignName = txtEngineDesignName.Text;
                char charStatusId = Convert.ToChar(ddlStatus_EngineDesign.SelectedValue);

                EngineDesign.insertUpdateEngineDesignDetails("InsertUpdateEngineDesignDetails",
                                              intEngineDesignId,
                                              strEngineDesignName,
                                              intCreatedBy,
                                              intModifiedBy,
                                              charStatusId);

                bindEngineDesignGrid();

                btn_Add_EngineDesign.Visible = false;
                btn_Add_EngineDesign_Click(sender, e);
                btn_Cancel_EngineDesign_Click(sender, e);
                lbl_EngineDesign_Message.Visible = true;
                lbl_EngineDesign_Message.Text = "Record Successfully Saved.";
            }
    }
    protected void btn_Cancel_EngineDesign_Click(object sender, EventArgs e)
    {
        if (Mode == "New")
        {
            btn_Add_EngineDesign.Visible = (Auth.isAdd || Auth.isEdit);
        }
        else if (Mode == "Edit")
        {
            btn_Add_EngineDesign.Visible = (Auth.isAdd || Auth.isEdit);
        }
        pnl_EngineDesign.Visible = false;
        btn_Save_EngineDesign.Visible = false;
        btn_Cancel_EngineDesign.Visible = false;
        btn_Add_EngineDesign.Visible = ((Mode == "Edit") || (Mode == "New")) ? true : false;
        GridView_EngineDesign.SelectedIndex = -1;
        lbl_EngineDesign_Message.Visible = false;
        btn_Print_EngineDesign.Visible = false;
    }
    protected void btn_Print_EngineDesign_Click(object sender, EventArgs e)
    {

    }
    protected void GridView_EngineDesign_DataBound(object sender, EventArgs e)
    {
        try
        {
            //GridView_EngineDesign.Columns[1].Visible = false;
            //GridView_EngineDesign.Columns[2].Visible = false;
            // Can Add
            if (Auth.isAdd)
            {
            }

            // Can Modify

            this.GridView_EngineDesign.Columns[1].Visible = Auth.isEdit;

            this.GridView_EngineDesign.Columns[2].Visible = Auth.isDelete;


            // Can Print
            if (Auth.isPrint)
            {
            }

        }
        catch
        {
            //GridView_EngineDesign.Columns[1].Visible = false;
            //GridView_EngineDesign.Columns[2].Visible = false;
        }
    }
    protected void Show_Record_EngineDesign(int EngineDesignid)
    {
        lbl_EngineDesign_Message.Visible = false;
        HiddenEngineDesign.Value = EngineDesignid.ToString();
        DataTable dt3 = EngineDesign.selectDataEngineDesignDetailsByEngineDesignId(EngineDesignid);
        foreach (DataRow dr in dt3.Rows)
        {
            txtEngineDesignName.Text = dr["EngineDesignName"].ToString();
            txtCreatedBy_EngineDesign.Text = dr["CreatedBy"].ToString();
            txtCreatedOn_EngineDesign.Text = dr["CreatedOn"].ToString();
            txtModifiedBy_EngineDesign.Text = dr["ModifiedBy"].ToString();
            txtModifiedOn_EngineDesign.Text = dr["ModifiedOn"].ToString();
            ddlStatus_EngineDesign.SelectedValue = dr["StatusId"].ToString();
        }
        btn_Print_EngineDesign.Visible = true;
    }
    // VIEW THE RECORD
    protected void GridView_EngineDesign_SelectedIndexChanged(object sender, EventArgs e)
    {

        HiddenField hfdEngineDesign;
        hfdEngineDesign = (HiddenField)GridView_EngineDesign.Rows[GridView_EngineDesign.SelectedIndex].FindControl("HiddenEngineDesignId");
        id = Convert.ToInt32(hfdEngineDesign.Value.ToString());
        pnl_EngineDesign.Visible = true;
        Show_Record_EngineDesign(id);
        btn_Add_EngineDesign.Visible = ((Mode == "Edit") || (Mode == "New")) ? true : false;
        btn_Save_EngineDesign.Visible = false;
        btn_Cancel_EngineDesign.Visible = true;
    }
    //EDIT THE RECORD
    protected void GridView_EngineDesign_Row_Editing(object sender, GridViewEditEventArgs e)
    {
        Mode = "Edit";
        HiddenField hfdEngineDesign;
        hfdEngineDesign = (HiddenField)GridView_EngineDesign.Rows[e.NewEditIndex].FindControl("HiddenEngineDesignId");
        id = Convert.ToInt32(hfdEngineDesign.Value.ToString());
        Show_Record_EngineDesign(id);
        GridView_EngineDesign.SelectedIndex = e.NewEditIndex;
        pnl_EngineDesign.Visible = true;
        btn_Add_EngineDesign.Visible = ((Mode == "Edit") || (Mode == "New")) ? true : false;
        btn_Save_EngineDesign.Visible = true;
        btn_Cancel_EngineDesign.Visible = true;
    }
    // DELETE THE RECORD
    protected void GridView_EngineDesign_Row_Deleting(object sender, GridViewDeleteEventArgs e)
    {
        int intModifiedBy = Convert.ToInt32(Session["loginid"].ToString());
        HiddenField hfdEngineDesign;
        hfdEngineDesign = (HiddenField)GridView_EngineDesign.Rows[e.RowIndex].FindControl("HiddenEngineDesignId");
        id = Convert.ToInt32(hfdEngineDesign.Value.ToString());
        EngineDesign.deleteEngineDesignDetailsById("DeleteEngineDesignDetailsById", id, intModifiedBy);
        bindEngineDesignGrid();
        if (HiddenEngineDesign.Value.ToString() == hfdEngineDesign.Value.ToString())
        {
            btn_Add_EngineDesign_Click(sender, e);
        }
        lbl_EngineDesign_Message.Visible = false;
    }
    protected void GridView_EngineDesign_PreRender(object sender, EventArgs e)
    {
        if (GridView_EngineDesign.Rows.Count <= 0) { lbl_GridView_EngineDesign.Text = "No Records Found..!"; }
    }
}
