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
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Net.Mail;


public partial class SafetyAlert : System.Web.UI.Page
{
    public string VesselCode, VesselName;
    string OfficeVesselID, SortBy = "";
    public int SortDirection
    {
        set { ViewState["SortDirection"] = value; }
        get { return int.Parse("0" + ViewState["SortDirection"]); }
    }
    public static Random rnd=new Random();
    
    public Authority Auth;
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        //--------------------------------- PAGE ACCESS AUTHORITY ----------------------------------------------------
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 1081);
        if (chpageauth <= 0)
            Response.Redirect("blank.aspx");
        //------------------------------------------------------------------------------------------------------------
        if (Session["loginid"] == null)
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Default.aspx';", true);
        }
        if (Session["loginid"] != null)
        {
            ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 6);
            OBJ.Invoke();
            Session["Authority"] = OBJ.Authority;
            Auth = OBJ.Authority;            
        }

        btnAlert.Visible = Auth.isAdd;        
        lblMsg.Text = "";
        lblMsgAppRej.Text = "";
        if (Page.Request.QueryString["VesselCode"] != null)
        {
            VesselCode = Page.Request.QueryString["VesselCode"].ToString();
            VesselName = Page.Request.QueryString["VesselName"].ToString();
        }
        if (!Page.IsPostBack)
        {
            txtFrom.Text = DateTime.Parse("01/01/" + DateTime.Today.Year.ToString()).ToString("dd-MMM-yyyy");
            txtTo.Text = DateTime.Today.ToString("dd-MMM-yyyy");  
            BindGrid();

        }
    }

    // Events ----------------------------------------------------------------------------------------------------------------------------
    protected void imgEditTopic_OnClick(object sender, EventArgs e)
    {
        
        ImageButton imgEditTopic = (ImageButton)sender;
        int rowIndex = Convert.ToInt32(imgEditTopic.Attributes["RowIndex"]);
        HiddenField hfSAID = (HiddenField)imgEditTopic.Parent.FindControl("hfSAID");

        ScriptManager.RegisterStartupScript(Page, this.GetType(), "ss", "CreatAlert(" + hfSAID.Value + ")", true);
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


    //protected void grdNCRDetails_OnSorted(object sender, EventArgs e)
    //{
    //}
    //protected void grdNCRDetails_OnSorting(object sender, GridViewSortEventArgs e)  //
    //{
    //    string sql = "", WhereClause = " where 1=1 ";
    //    sql = " select CID,CType,substring(CircularNumber,len(CircularNumber)-7,8)CNForSorting,CircularNumber,CircularDate,Category,Details,StateStatus" +
    //        " ,(case when Status<>2 then CFileName else 'CircularFile_'+ convert(varchar(10),Cid)+'.pdf' end)CFileName " +
    //        " ,(select CirName from dbo.CircularCategory CC where CC.CID=C.Category)CircularCatName" +
    //        " ,( case when len( convert(varchar(7000),C.Details))>55 then substring(C.Details,1,50)+'............'else  C.Details end ) as ShortDetails" +
    //        " ,(case when len( convert(varchar(7000),C.Topic))>60 then substring(C.Topic,1,60)+'............'else  C.Topic end )Topic" +
    //        " ,( case when Status<>2 then ( case when C.CFileName='' then 'none' else 'block' end) else 'block' end )ClipVisibility  " +

    //        " ,Source " +
    //        //" ,(select  (FirstName+' '+MiddleName+' '+FamilyName) from  dbo.Hr_PersonalDetails U where U.Empid=C.Source)SourceName" +

    //        " ,CreatedBy " +
    //        " ,(select  UserID from  dbo.userlogin U where U.Loginid=C.CreatedBy )CreatedByName " +
    //        " ,CreatedOn " +
    //        " ,replace(convert(varchar,CreatedOn,106),' ','-') CreatedOnText " +

    //        ",(select (FirstName+' '+MiddleName+''+FamilyName)EmpName  from dbo.Hr_PersonalDetails HR where HR.EmpID=c.SubmittedForApproval)SubmittedForApproval" +
    //        " ,replace(convert(varchar,SubmittedForApprovalOn,106),' ','-') SubmittedForApprovalOn " +

    //        " ,Status, 'True' as Visibility " +
    //        " ,(case when C.Status<>1 then 'none' else (case when SubmittedForApproval <> 0 then 'block' else 'none'end ) end) CommVisibility  " +
    //        " ,(case when C.Status<>1 then 'block' else (case when SubmittedForApproval <> 0 then 'none' else 'block'end ) end) StatusLableVisibility  " +
    //        //" ,(case when C.Status=1 then 'Awaiting Approval' when Status='2' then 'Approved' when Status='3' then 'Rejected' end) StatusText " +

    //        " ,(select count(cid) from createCircular where status=1)AwaitingCount " +
    //        " ,(select count(cid) from createCircular where status=3)RejectedCount " +

    //        " ,(case when C.Status=1 then 'Awaiting Approval' when Status='2' then (case when C.NextReviewDate>=getdate() then 'Active' when C.NextReviewDate < getdate() then 'Inactive' end) when Status='3' then 'Rejected' end) StatusText, newid() as rand  " +
    //        //" ,(case when C.Status=1 then 'Awaiting Approval' when Status='2' then (case when C.StateStatus=1 then 'Active' when C.StateStatus=2 then 'Inactive' when C.StateStatus=3 then 'In SMS' end) when Status='3' then 'Rejected' end) StatusText  "+
    //        " from CreateCircular C";



    //    if (txtFrom.Text.Trim() != "")
    //    {
    //        WhereClause = WhereClause + " and C.CircularDate>='" + txtFrom.Text.Trim() + "'";
    //    }
    //    if (txtTo.Text.Trim() != "")
    //    {
    //        WhereClause = WhereClause + " and C.CircularDate<'" + Convert.ToDateTime(txtTo.Text.Trim()).AddDays(1).ToString("dd-MMM-yyyy") + "'";
    //    }
        
    //    if (txtSearchText.Text.Trim() != "")
    //    {
    //        WhereClause = WhereClause + " and C.Details like '%" + txtSearchText.Text.Trim() + "%'";
    //    }
    //    //if (chkAwaitingApproval.Checked || chkRejected.Checked)
    //    //{
    //    //    if (chkRejected.Checked && chkAwaitingApproval.Checked)
    //    //        WhereClause = WhereClause + " and C.Status in(1,3) ";
    //    //    else if (chkAwaitingApproval.Checked)
    //    //        WhereClause = WhereClause + " and C.Status=1 ";
    //    //    else if (chkRejected.Checked)
    //    //        WhereClause = WhereClause + " and C.Status=3 ";

    //    //}
    //    //else
        
    //    sql = sql + WhereClause + " ";
    //    DataSet DS = Budget.getTable(sql);

    //    if (DS != null)
    //    {
    //        if (SortDirection == 0)
    //        {
    //            e.SortDirection = System.Web.UI.WebControls.SortDirection.Ascending;
    //            SortDirection = 1;
    //        }
    //        else
    //        {
    //            e.SortDirection = System.Web.UI.WebControls.SortDirection.Descending;
    //            SortDirection = 0;
    //        }
    //        DataView dataView = new DataView(DS.Tables[0]);
    //        dataView.Sort = e.SortExpression + " " + ConvertSortDirectionToSql(e.SortDirection);

    //        grdNCRDetails.DataSource = dataView;
    //        grdNCRDetails.DataBind();
    //    }
    //}
    private string ConvertSortDirectionToSql(SortDirection sortDirection)
    {
        string newSortDirection = String.Empty;

        switch (sortDirection)
        {
            case System.Web.UI.WebControls.SortDirection.Ascending:
                newSortDirection = "ASC";
                break;

            case System.Web.UI.WebControls.SortDirection.Descending:
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

    // Function ----------------------------------------------------------------------------------------------------------------------------
    public void BindGrid()
    {
        string sql = "", WhereClause = " where 1=1 ";
        sql = " select SAID,Date,Details, ('SA-'+CONVERT(VARCHAR,YEAR(DATE))+'-'+RIGHT ('000'+ CAST (SANumber AS varchar), 3)) SANumber," +
            "('SafetyAlert_SA-'+CONVERT(VARCHAR,YEAR(DATE))+'-'+RIGHT ('000'+ CAST (SANumber AS int), 3)+'.pdf') as CFileName " +

            " ,(case when len( convert(varchar(7000),SA.Details))>55 then substring(SA.Details,1,50)+'............'else  SA.Details end ) as ShortDetails" +
            " ,(case when len( convert(varchar(7000),SA.Topic))>60 then substring(SA.Topic,1,60)+'............'else  SA.Topic end )Topic" +
            " , 'block' AS ClipVisibility  " +
            " , Source " +            
            
            " , CreatedBy " +
            " ,(select  UserID from  dbo.userlogin U where U.Loginid=SA.CreatedBy )CreatedByName " +
            " , CreatedOn " +
            " , replace(convert(varchar,CreatedOn,106),' ','-') CreatedOnText " +
            " , newid() as rand    " +
            " from SA_SafetyAlert SA";



        if (txtFrom.Text.Trim() != "")
        {
            WhereClause = WhereClause + " and SA.Date>='" + txtFrom.Text.Trim() + "'";
        }
        if (txtTo.Text.Trim() != "")
        {
            WhereClause = WhereClause + " and SA.Date<'" + Convert.ToDateTime(txtTo.Text.Trim()).AddDays(1).ToString("dd-MMM-yyyy") + "'";
        }
        if (txtSearchText.Text.Trim() != "")
        {
            WhereClause = WhereClause + " and SA.Details like '%" + txtSearchText.Text.Trim() + "%'";
        }
        sql = sql + WhereClause + " order by Year(Date) desc,  cast(SANumber as int)asc";
        DataSet DS = Budget.getTable(sql);

        if (DS != null)
        {   
            grdNCRDetails.DataSource = DS;
            grdNCRDetails.DataBind();
            lblTotalRec.Text = "Total No of Record : " + DS.Tables[0].Rows.Count;                

            //string SqlCnt = "select (select count(cid) from createCircular where status=1)AwaitingCount  ,(select count(cid) from createCircular where status=3)RejectedCount  ";
            //DataSet Dscnt = Budget.getTable(SqlCnt);
            //if (Dscnt != null)
            //{
            //    lblTotalRec.Text = "Total No of Record : " + DS.Tables[0].Rows.Count;                
            //}
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
    public int GetEmpID(int LoginID)
    {
        DataSet ds = Budget.getTable("select EmpID from dbo.Hr_PersonalDetails where UserID="+LoginID+"");
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
                return Common.CastAsInt32(ds.Tables[0].Rows[0][0]);
        }
        return 0;
    }
    
}
