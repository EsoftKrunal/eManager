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

using ShipSoft.CrewManager.BusinessLogicLayer;
using ShipSoft.CrewManager.BusinessObjects;
using ShipSoft.CrewManager.Operational;

public partial class VesselRecord_VesselSearch : System.Web.UI.Page
{
    Authority obj;
    int id;
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        Alerts.SetHelp(imgHelp);  
        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 43);
        if (chpageauth <= 0)
        {
            Response.Redirect("../AuthorityError.aspx");

        }
        //*******************
        ProcessCheckAuthority Auth = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 4);
        Auth.Invoke();
        Session["Authority"] = Auth.Authority;
        obj = (Authority)Session["Authority"];
        lbl_GridView_VesselSearch.Text = "";
        try
        {
            if (Session["UserName"] == "")
            {
                Response.Redirect("default.aspx");
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("default.aspx");
            return;
        }
        if (!(IsPostBack))
        {
            bindVesselTypeDDL();
            BindMgmtTypeDropDown();
            BindOwnerDropDown();
            BindStatusDropDown();
            BindOwnerPoolDropDown();
            bindVesselGrid(GridView_VesselSearch.Attributes["MySort"]);
            btn_Add_VesselSearch.Visible = obj.isAdd;
            //---------------------
            if (!IsPostBack)
            {
                    int VesselId = 0;
                    try
                    {
                        VesselId = int.Parse(Convert.ToString(Session["VesselId"]));
                        if (VesselId > 0)
                        {
                            Response.Redirect("~/Modules/HRD/VesselRecord/VesselDetails.aspx");
                        }
                    }
                    catch { }
            }
        }
    }
    public void bindVesselTypeDDL()
    {
        DataTable dt1 = VesselSearch.selectDataVesselTypeName();
        this.ddlVesselTypeName.DataValueField = "VesselTypeId";
        this.ddlVesselTypeName.DataTextField = "VesselTypeName";
        this.ddlVesselTypeName.DataSource = dt1;
        this.ddlVesselTypeName.DataBind();
    }
    private void BindMgmtTypeDropDown()
    {
        DataTable dt2 = VesselDetailsGeneral.selectDataMgmtType();
        this.ddlMgmtType.DataValueField = "ManagementTypeId";
        this.ddlMgmtType.DataTextField = "ManagementTypeName";
        this.ddlMgmtType.DataSource = dt2;
        this.ddlMgmtType.DataBind();
    }
    private void BindOwnerDropDown()
    {
        DataTable dt3 = VesselDetailsGeneral.selectDataOwnerName();
        this.ddlOwner.DataValueField = "OwnerId";
        this.ddlOwner.DataTextField = "OwnerShortName";
        this.ddlOwner.DataSource = dt3;
        this.ddlOwner.DataBind();
    }
    private void BindOwnerPoolDropDown()
    {
        DataTable dt3 = VesselDetailsGeneral.selectDataOwnerPoolName();
        this.ddlOwnerPool.DataValueField = "OwnerPoolId";
        this.ddlOwnerPool.DataTextField = "OwnerPoolName";
        this.ddlOwnerPool.DataSource = dt3;
        this.ddlOwnerPool.DataBind();
    }
    private void BindStatusDropDown()
    {
        DataTable dt9 = VesselDetailsGeneral.selectDataVesselStatus();
        this.ddlVesselStatus.DataValueField = "VesselStatusId";
        this.ddlVesselStatus.DataTextField = "VesselStatusName";
        this.ddlVesselStatus.DataSource = dt9;
        this.ddlVesselStatus.DataBind();
    }
    public void bindVesselGrid(String Sort)
    {
        int _VesselType,_VesselStatus,_MgmtType,_OwnerId,_OwnerPoolId;
        _VesselType = Convert.ToInt32(ddlVesselTypeName.SelectedValue);
        _VesselStatus = Convert.ToInt32(ddlVesselStatus.SelectedValue);
        _MgmtType = Convert.ToInt32(ddlMgmtType.SelectedValue);
        _OwnerId = Convert.ToInt32(ddlOwner.SelectedValue);
        _OwnerPoolId = Convert.ToInt32(ddlOwnerPool.SelectedValue);  
        DataTable dt2 = VesselSearch.selectDataVesselDetails(Convert.ToInt32(Session["loginid"].ToString()),_VesselType,_VesselStatus,_MgmtType,_OwnerId,_OwnerPoolId);
        dt2.DefaultView.Sort = Sort;
        this.GridView_VesselSearch.DataSource = dt2;
        this.GridView_VesselSearch.DataBind();
        GridView_VesselSearch.Attributes.Add("MySort", Sort);
    }
    protected void btn_Add_VesselSearch_Click(object sender, EventArgs e)
    {
        Session["VesselId"] = "";
        Session["VMode"] = "New";
        Response.Redirect("~/Modules/HRD/VesselRecord/VesselDetails.aspx?Mode=New");
    }
    protected void btn_Search_VesselSearch_Click(object sender, EventArgs e)
    {

    }
    protected void btn_Clear_VesselSearch_Click(object sender, EventArgs e)
    {

    }
    // VIEW ONLY
    protected void GridView_VesselSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        HiddenField hfdvesselsearch;
        hfdvesselsearch = (HiddenField)GridView_VesselSearch.Rows[GridView_VesselSearch.SelectedIndex].FindControl("HiddenVesselId");
        id = Convert.ToInt32(hfdvesselsearch.Value.ToString());
        Session["VMode"] = "View";
        Session["VesselId"] = hfdvesselsearch.Value.ToString();
        Response.Redirect("~/Modules/HRD/VesselRecord/VesselDetails.aspx");
    }
    // EDIT THE RECORD
    protected void GridView_VesselSearch_Row_Editing(object sender, GridViewEditEventArgs e)
    {
        HiddenField hfdvesselsearch;
        hfdvesselsearch = (HiddenField)GridView_VesselSearch.Rows[e.NewEditIndex].FindControl("HiddenVesselId");
        id = Convert.ToInt32(hfdvesselsearch.Value.ToString());
        GridView_VesselSearch.SelectedIndex = e.NewEditIndex;
        Session["VMode"] = "Edit";
        Session["VesselId"] = hfdvesselsearch.Value.ToString();
        Response.Redirect("~/Modules/HRD/VesselRecord/VesselDetails.aspx");
    }
    // DELETE THE RECORD
    protected void GridView_VesselSearch_Row_Deleting(object sender, GridViewDeleteEventArgs e)
    {
        HiddenField hfdvesselsearch;
        hfdvesselsearch = (HiddenField)GridView_VesselSearch.Rows[e.RowIndex].FindControl("HiddenVesselId");
        id = Convert.ToInt32(hfdvesselsearch.Value.ToString());
        VesselSearch.deleteVesselDetailsById("DeleteVesselDetailsById", id, Convert.ToInt32(Session["loginid"].ToString()));
        bindVesselGrid(GridView_VesselSearch.Attributes["MySort"]);
       
    }
    protected void GridView_VesselSearch_DataBound(object sender, EventArgs e)
    {
        // Can Modify
        GridView_VesselSearch.Columns[1].Visible = obj.isEdit;
        // Can Delete
        GridView_VesselSearch.Columns[2].Visible = false;// obj.isDelete;
        // Can Print

    }
    protected void GridView_VesselSearch_PreRender(object sender, EventArgs e)
    {
        if (GridView_VesselSearch.Rows.Count <= 0) { lbl_GridView_VesselSearch.Text = "No Records Found..!"; }
    }
    protected void ddlVesselTypeName_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindVesselGrid(GridView_VesselSearch.Attributes["MySort"]);
    }
    protected void on_Sorting(object sender, GridViewSortEventArgs e)
    {
        bindVesselGrid(e.SortExpression);
    }
    protected void on_Sorted(object sender, EventArgs e)
    {

    }
}
    