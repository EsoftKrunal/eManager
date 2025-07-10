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
using System.Data.SqlClient;
using System.Xml;
using ShipSoft.CrewManager.BusinessLogicLayer;
using ShipSoft.CrewManager.BusinessObjects;
using ShipSoft.CrewManager.Operational;
using System.IO;

public partial class IssuedCircular : System.Web.UI.Page
{
    public string VesselCode, VesselName;
    string OfficeVesselID, SortBy = "";
    public int CID_Edit
    {
        set { ViewState["CID_Edit"] = value; }
        get { return int.Parse("0" + ViewState["CID_Edit"]); }
    }
    public string Status
    {
        set { ViewState["Status"] = value; }
        get { return ViewState["Status"].ToString(); }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        lblMsg.Text = "";
        lblMsgAppRej.Text = "";
        if (Page.Request.QueryString["VesselCode"] != null)
        {
            VesselCode = Page.Request.QueryString["VesselCode"].ToString();
            VesselName = Page.Request.QueryString["VesselName"].ToString();
        }
        if (!Page.IsPostBack)
        {
            txtFrom.Text = DateTime.Parse("01/01/" + DateTime.Today.Year.ToString()).ToString("dd-MMM-yyyy");//daysinmonth.ToString("dd-MMM-yyyy");
            txtTo.Text = DateTime.Today.ToString("dd-MMM-yyyy");   //daysinmonth.AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
            BindCategory();
            BindGrid();

        }
    }

    // Events ----------------------------------------------------------------------------------------------------------------------------
    protected void imgEditTopic_OnClick(object sender, EventArgs e)
    {
        
        ImageButton imgEditTopic = (ImageButton)sender;
        int rowIndex = Convert.ToInt32(imgEditTopic.Attributes["RowIndex"]);
        HiddenField hfCID = (HiddenField)imgEditTopic.Parent.FindControl("hfCID");

        ScriptManager.RegisterStartupScript(Page,this.GetType(),"ss","CreatCircular("+hfCID .Value+")",true);
        grdNCRDetails.SelectedIndex = rowIndex;

        
        
    }
    protected void grdNCRDetails_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HiddenField hfcss = (HiddenField)e.Row.FindControl("hfcss");
            e.Row.CssClass = hfcss.Value;
        }
    }
    protected void grdNCRDetails_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdNCRDetails.PageIndex = e.NewPageIndex;
        BindGrid(); ;
    }
    protected void imgDel_OnClick(object sender, EventArgs e)
    {
        ImageButton imgDel = (ImageButton)sender;
        HiddenField hfPID = (HiddenField)imgDel.Parent.FindControl("hfPID");
        string sqlDel = "delete from Circular where id=" + hfPID .Value+ "";
        DataSet DsDel = Budget.getTable(sqlDel);
        BindGrid();
    }
    protected void imgAppCir_OnClick(object sender, EventArgs e)
    {

    }
    protected void grdNCRDetails_OnSorting(object sender, GridViewSortEventArgs e)
    {
        //GridViewSortEventHandler
        //grdNCRDetails.Sort(e.SortExpression,SortDirection.Ascending);
        string sql = "", WhereClause = " where 1=1 and C.Status=2 ";
        sql = " select CID,CircularDate,Category,Topic,Details,CFileName" +
            " ,(select CirName from dbo.CircularCategory CC where CC.CID=C.Category)CircularCatName" +
            " ,( case when len( convert(varchar(7000),C.Details))>95 then substring(C.Details,1,90)+'............'else  C.Details end ) as ShortDetails" +
            " ,(case when C.CFileName='' then 'none' else 'block' end)ClipVisibility " +

            " ,Source " +
            //" ,(select  (FirstName+' '+MiddleName+' '+FamilyName) from  dbo.Hr_PersonalDetails U where U.Empid=C.Source)SourceName" +

            " ,CreatedBy " +
            " ,(select  UserID from  dbo.userlogin U where U.Loginid=C.CreatedBy )CreatedByName " +
            " ,CreatedOn " +
            " ,replace(convert(varchar,CreatedOn,106),' ','-') CreatedOnText " +

            ",(select (FirstName+' '+MiddleName+''+FamilyName)EmpName  from dbo.Hr_PersonalDetails HR where HR.EmpID=c.SubmittedForApproval)SubmittedForApproval" +
            " ,replace(convert(varchar,SubmittedForApprovalOn,106),' ','-') SubmittedForApprovalOn " +

            " ,Status, 'True' as Visibility " +
            " ,(case when C.Status=1 then 'Requested' when Status='2' then 'Accepted' when Status='3' then 'Rejected' end) StatusText " +
            " from CreateCircular C";



        if (txtFrom.Text.Trim() != "")
        {
            WhereClause = WhereClause + " and C.CircularDate>='" + txtFrom.Text.Trim() + "'";
        }
        if (txtTo.Text.Trim() != "")
        {
            WhereClause = WhereClause + " and C.CircularDate<'" + Convert.ToDateTime(txtTo.Text.Trim()).AddDays(1).ToString("dd-MMM-yyyy") + "'";
        }
        if (ddlCategory.SelectedIndex != 0)
        {
            WhereClause = WhereClause + " and C.Category=" + ddlCategory.SelectedValue + "";
        }
        if (txtSearchText.Text.Trim() != "")
        {
            WhereClause = WhereClause + " and T.Details like '%" + txtSearchText.Text.Trim() + "%'";
        }

        sql = sql + WhereClause + " ";
        DataSet DS = Budget.getTable(sql);

        if (DS != null)
        {
            DataView dataView = new DataView(DS.Tables[0]);
            dataView.Sort = e.SortExpression + " " + ConvertSortDirectionToSql(e.SortDirection);

            grdNCRDetails.DataSource = dataView;
            grdNCRDetails.DataBind();
        }
    }
    private string ConvertSortDirectionToSql(SortDirection sortDirection)
    {
        string newSortDirection = String.Empty;

        switch (sortDirection)
        {
            case SortDirection.Ascending:
                newSortDirection = "ASC";
                break;

            case SortDirection.Descending:
                newSortDirection = "DESC";
                break;
        }

        return newSortDirection;
    }

    protected void btnSearch_OnClick(object sender, EventArgs e)
    {
        BindGrid();
        grdNCRDetails.SelectedIndex = -1;

    }
    protected void btnClear_OnClick(object sender, EventArgs e)
    {
        txtFrom.Text = DateTime.Parse("01/01/" + DateTime.Today.Year.ToString()).ToString("dd-MMM-yyyy");//daysinmonth.ToString("dd-MMM-yyyy");
        txtTo.Text = DateTime.Today.ToString("dd-MMM-yyyy");   //daysinmonth.AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
        txtSearchText.Text = "";
        ddlCategory.SelectedIndex = 0;
        grdNCRDetails.SelectedIndex = -1;
    }
    
    protected void lnlDateSort_OnClick(object sender, EventArgs e)
    {
        SortBy = "date";
        BindGrid();
    }
    protected void lnkNameSort_OnClick(object sender, EventArgs e)
    {
        SortBy = "name";
        BindGrid();
    }
    protected void BtnReferesh_OnClick(object sender, EventArgs e)
    {
        BindGrid();
    }
   
    //Events for approval or rejection
    protected void imgAppOrReject_OnClick(object sender, EventArgs e)
    {
        ImageButton imgApprejTopic = (ImageButton)sender;
        HiddenField hfCID = (HiddenField)imgApprejTopic.Parent.FindControl("hfCID");
        CID_Edit = Common.CastAsInt32( hfCID.Value);


        txtAppOrRejComments.Text = "";
        DivApproveReject.Visible = true;
    }
    protected void btnApproveComments_OnClick(object sender, EventArgs e)
    {
        if (txtAppOrRejComments.Text.Trim() == "")
        {
            lblMsgAppRej.Text = "Please enter comments.";
            lblMsgAppRej.Focus();
            return;
        }
        string sql = "sp_UpdateCreateCircularAppRejComm " + CID_Edit + "," + Common.CastAsInt32(Session["loginid"].ToString()) + ",'"+txtAppOrRejComments.Text.Trim()+"',2";
        DataSet ds = Budget.getTable(sql);
        if (ds != null)
        {
            BindGrid();
            DivApproveReject.Visible = false;            
        }
        else
        {
            lblMsgAppRej.Text = "Data could not be updated.";
        }

    }
    protected void btnRejectComments_OnClick(object sender, EventArgs e)
    {
        if (txtAppOrRejComments.Text.Trim() == "")
        {
            lblMsgAppRej.Text = "Please enter comments.";
            lblMsgAppRej.Focus();
            return;
        }
        string sql = "sp_UpdateCreateCircularAppRejComm " + CID_Edit + "," + Common.CastAsInt32(Session["loginid"].ToString()) + ",'" + txtAppOrRejComments.Text.Trim() + "',3";
        DataSet ds = Budget.getTable(sql);
        if (ds != null)
        {
            BindGrid();
            DivApproveReject.Visible = false;
        }
        else
        {
            lblMsgAppRej.Text = "Data could not be updated.";
        }
    }
    protected void btnCancelComments_OnClick(object sender, EventArgs e)
    {
        DivApproveReject.Visible = false;
        CID_Edit = 0;
    }
   
    // Function ----------------------------------------------------------------------------------------------------------------------------
    public void BindGrid()
    {
        string sql = "", WhereClause = " where 1=1 and C.Status=2 ";
        sql = " select CID,CircularDate,Category,Topic,Details,CFileName" +
            " ,(select CirName from dbo.CircularCategory CC where CC.CID=C.Category)CircularCatName" +
            " ,( case when len( convert(varchar(7000),C.Details))>95 then substring(C.Details,1,90)+'............'else  C.Details end ) as ShortDetails" +
            " ,(case when C.CFileName='' then 'none' else 'block' end)ClipVisibility " +

            " ,Source " +
            //" ,(select  (FirstName+' '+MiddleName+' '+FamilyName) from  dbo.Hr_PersonalDetails U where U.Empid=C.Source)SourceName" +
            
            " ,CreatedBy " +
            " ,(select  UserID from  dbo.userlogin U where U.Loginid=C.CreatedBy )CreatedByName " +
            " ,CreatedOn " +
            " ,replace(convert(varchar,CreatedOn,106),' ','-') CreatedOnText " +

            ",(select (FirstName+' '+MiddleName+''+FamilyName)EmpName  from dbo.Hr_PersonalDetails HR where HR.EmpID=c.SubmittedForApproval)SubmittedForApproval" +
            " ,replace(convert(varchar,SubmittedForApprovalOn,106),' ','-') SubmittedForApprovalOn " +
            
            " ,Status, 'True' as Visibility "+
            " ,(case when C.Status=1 then 'block' else 'none' end) CommVisibility " +
            " ,(case when C.Status=1 then 'Awaiting Approval' when Status='2' then 'Approved' when Status='3' then 'Rejected' end) StatusText " +
            " from CreateCircular C";



        if (txtFrom.Text.Trim() != "")
        {
            WhereClause = WhereClause + " and C.CircularDate>='" + txtFrom.Text.Trim() + "'";
        }
        if (txtTo.Text.Trim() != "")
        {
            WhereClause = WhereClause + " and C.CircularDate<'" + Convert.ToDateTime(txtTo.Text.Trim()).AddDays(1).ToString("dd-MMM-yyyy") + "'";
        }
        if (ddlCategory.SelectedIndex != 0)
        {
            WhereClause = WhereClause + " and C.Category=" + ddlCategory.SelectedValue + "";
        }
        if (txtSearchText.Text.Trim() != "")
        {
            WhereClause = WhereClause + " and T.Details like '%" + txtSearchText.Text.Trim() + "%'";
        }
        
        sql = sql + WhereClause + " ";
        DataSet DS = Budget.getTable(sql);


        if (DS != null)
        {   
            grdNCRDetails.DataSource = DS;
            grdNCRDetails.DataBind();
            lblTotalRec.Text = "Total No of Record : " + DS.Tables[0].Rows.Count;

        }
        else
        {
            lblTotalRec.Text = "Total No of Record : 0";
        }
    }
    public void BindCategory()
    {
        string sql = "select CID,CirName from CircularCategory";
        DataSet DS = Budget.getTable(sql);
        if (DS != null)
        {
            ddlCategory.DataSource = DS;
            ddlCategory.DataTextField = "CirName";
            ddlCategory.DataValueField = "CID";
            ddlCategory.DataBind();
            ddlCategory.Items.Insert(0,new ListItem(" All ","0"));
        }
    }
    public bool chk_FileExtension(string str)
    {
        return true;
        string extension = str;
        switch (extension)
        {
            case ".xml":
                return true;
            default:
                return false;
                break;
        }
    }
     
}
