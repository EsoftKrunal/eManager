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


public partial class CircularForApproval : System.Web.UI.Page
{
    public string VesselCode, VesselName;
    string OfficeVesselID, SortBy = "";
    public int CID_Edit
    {
        set { ViewState["CID_Edit"] = value; }
        get { return int.Parse("0" + ViewState["CID_Edit"]); }
    }

    public int SortDirection
    {
        set { ViewState["SortDirection"] = value; }
        get { return int.Parse("0" + ViewState["SortDirection"]); }
    }

    public string Status
    {
        set { ViewState["Status"] = value; }
        get { return ViewState["Status"].ToString(); }
    }
    public string FileName
    {
        set { ViewState["FileName"] = value; }
        get { return ViewState["FileName"].ToString(); }
    }

    public string vCircularNumber
    {
        set { ViewState["CircularNumber"] = value; }
        get { return ViewState["CircularNumber"].ToString(); }
    }
    public string vApprejBy
    {
        set { ViewState["ApprejBy"] = value; }
        get { return ViewState["ApprejBy"].ToString(); }
    }
    public string vApprejOn
    {
        set { ViewState["ApprejOn"] = value; }
        get { return ViewState["ApprejOn"].ToString(); }
    }

    public string vCreatedBy
    {
        set { ViewState["vCreatedBy"] = value; }
        get { return ViewState["vCreatedBy"].ToString(); }
    }
    public string vCreatedOn
    {
        set { ViewState["vCreatedOn"] = value; }
        get { return ViewState["vCreatedOn"].ToString(); }
    }
    public Authority Auth;
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        //--------------------------------- PAGE ACCESS AUTHORITY ----------------------------------------------------
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 273);
        if (chpageauth <= 0)
            Response.Redirect("blank.aspx");
        //------------------------------------------------------------------------------------------------------------
        if (Session["loginid"] == null)
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Default.aspx';", true);
        }
        if (Session["loginid"] != null)
        {
            ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 273);
            OBJ.Invoke();
            Session["Authority"] = OBJ.Authority;
            Auth = OBJ.Authority;
        }
        lblCreateNewCircular.Visible = Auth.isAdd;
        



        lblMsg.Text = "";
        lblMsgAppRej.Text = "";
        if (Page.Request.QueryString["VesselCode"] != null)
        {
            VesselCode = Page.Request.QueryString["VesselCode"].ToString();
            VesselName = Page.Request.QueryString["VesselName"].ToString();
        }
        if (!Page.IsPostBack)
        {
            ddlStatus.SelectedValue = "0";
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


    protected void grdNCRDetails_OnSorted(object sender, EventArgs e)
    {
    }
    protected void grdNCRDetails_OnSorting(object sender, GridViewSortEventArgs e)  //
    {
        string sql = "", WhereClause = " where 1=1 ";
        sql = " select CID,CType,substring(CircularNumber,len(CircularNumber)-7,8)CNForSorting,CircularNumber,CircularDate,Category,Details,StateStatus" +
            " ,(case when Status<>2 then CFileName else 'CircularFile_'+ convert(varchar(10),Cid)+'.pdf' end)CFileName " +
            " ,(select CirName from dbo.CircularCategory CC where CC.CID=C.Category)CircularCatName" +
            " ,( case when len( convert(varchar(7000),C.Details))>55 then substring(C.Details,1,50)+'............'else  C.Details end ) as ShortDetails" +
            " ,(case when len( convert(varchar(7000),C.Topic))>60 then substring(C.Topic,1,60)+'............'else  C.Topic end )Topic" +
            " ,( case when Status<>2 then ( case when C.CFileName='' then 'none' else 'block' end) else 'block' end )ClipVisibility  " +

            " ,Source " +
            //" ,(select  (FirstName+' '+MiddleName+' '+FamilyName) from  dbo.Hr_PersonalDetails U where U.Empid=C.Source)SourceName" +

            " ,CreatedBy " +
            " ,(select  UserID from  dbo.userlogin U where U.Loginid=C.CreatedBy )CreatedByName " +
            " ,CreatedOn " +
            " ,replace(convert(varchar,CreatedOn,106),' ','-') CreatedOnText " +

            ",(select (FirstName+' '+MiddleName+''+FamilyName)EmpName  from dbo.Hr_PersonalDetails HR where HR.EmpID=c.SubmittedForApproval)SubmittedForApproval" +
            " ,replace(convert(varchar,SubmittedForApprovalOn,106),' ','-') SubmittedForApprovalOn " +

            " ,Status, 'True' as Visibility " +
            " ,(case when C.Status<>1 then 'none' else (case when SubmittedForApproval <> 0 then 'block' else 'none'end ) end) CommVisibility  " +
            " ,(case when C.Status<>1 then 'block' else (case when SubmittedForApproval <> 0 then 'none' else 'block'end ) end) StatusLableVisibility  " +
            //" ,(case when C.Status=1 then 'Awaiting Approval' when Status='2' then 'Approved' when Status='3' then 'Rejected' end) StatusText " +

            " ,(select count(cid) from createCircular where status=1)AwaitingCount " +
            " ,(select count(cid) from createCircular where status=3)RejectedCount " +

            " ,(case when C.Status=1 then 'Awaiting Approval' when Status='2' then (case when C.NextReviewDate>=getdate() then 'Active' when C.NextReviewDate < getdate() then 'Inactive' end) when Status='3' then 'Rejected' end) StatusText, newid() as rand  " +
            //" ,(case when C.Status=1 then 'Awaiting Approval' when Status='2' then (case when C.StateStatus=1 then 'Active' when C.StateStatus=2 then 'Inactive' when C.StateStatus=3 then 'In SMS' end) when Status='3' then 'Rejected' end) StatusText  "+
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
            WhereClause = WhereClause + " and C.Details like '%" + txtSearchText.Text.Trim() + "%'";
        }
        //if (chkAwaitingApproval.Checked || chkRejected.Checked)
        //{
        //    if (chkRejected.Checked && chkAwaitingApproval.Checked)
        //        WhereClause = WhereClause + " and C.Status in(1,3) ";
        //    else if (chkAwaitingApproval.Checked)
        //        WhereClause = WhereClause + " and C.Status=1 ";
        //    else if (chkRejected.Checked)
        //        WhereClause = WhereClause + " and C.Status=3 ";

        //}
        //else
        {
            if (ddlStatus.SelectedIndex != 0)
            {
                if (ddlStatus.SelectedValue == "1")
                {
                    WhereClause = WhereClause + " and C.Status=2  and C.StateStatus=1 ";
                }
                else if (ddlStatus.SelectedValue == "2")
                    WhereClause = WhereClause + " and C.Status=2 and C.NextReviewDate < getdate() ";
                else if (ddlStatus.SelectedValue == "3")
                    WhereClause = WhereClause + " and C.Status=2 and C.StateStatus=3 ";

            }
            else
            {
                WhereClause = WhereClause + " and C.Status=2 ";
            }
        }
        sql = sql + WhereClause + " ";
        DataSet DS = Budget.getTable(sql);

        if (DS != null)
        {
            if (SortDirection == 0)
            {
                e.SortDirection = System.Web.UI.WebControls.SortDirection.Ascending;
                SortDirection = 1;
            }
            else
            {
                e.SortDirection = System.Web.UI.WebControls.SortDirection.Descending;
                SortDirection = 0;
            }
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
        ddlStatus.SelectedIndex = 0;
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
        LinkButton imgApprejTopic = (LinkButton)sender;
        HiddenField hfCID = (HiddenField)imgApprejTopic.Parent.FindControl("hfCID");
        HiddenField hfFileName = (HiddenField)imgApprejTopic.Parent.FindControl("hfFileName");
        
        CID_Edit = Common.CastAsInt32( hfCID.Value);
        FileName = hfFileName.Value;


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
        string sql = "sp_UpdateCreateCircularAppRejComm " + CID_Edit + "," + Common.CastAsInt32(Session["loginid"].ToString()) + ",'" + txtAppOrRejComments.Text.Trim().Replace("'","`") + "',2";
        DataSet ds = Budget.getTable(sql);
        if (ds != null)
        {
            BindGrid();
            DivApproveReject.Visible = false;
            CreatePDF();
            int NoOfFile=0;
            if (FileName != "")
                NoOfFile = 2;
            else
                NoOfFile = 1;

            string[] SourceFile = new string[NoOfFile];
            if (FileName != "")
            {
                SourceFile[0] = Server.MapPath("~/UserUploadedDocuments/Circular/CircularDataFile.pdf");
                SourceFile[1] = Server.MapPath("~/UserUploadedDocuments/Circular/"+FileName+"");
            }
            else
            {
                SourceFile[0] = Server.MapPath("~/UserUploadedDocuments/Circular/CircularDataFile.pdf");
            }
            string Destination=Server.MapPath("~/UserUploadedDocuments/Circular/CircularFile_"+CID_Edit+".pdf");

            ExportToPDF(SourceFile, Destination, "", " ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- Created By/On    : " + vCreatedBy + " / " + vCreatedOn + "                                                                                                               Circular Number : " + vCircularNumber + "\n Approved By/On : " + vApprejBy + " / " + vApprejOn + "                                                                                                                                                                       Page ");
            //WritePDFFooter();

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


            //CreatePDF();
            //int NoOfFile = 0;
            //if (FileName != "")
            //    NoOfFile = 2;
            //else
            //    NoOfFile = 1;

            //string[] SourceFile = new string[NoOfFile];
            //if (FileName != "")
            //{
            //    SourceFile[0] = Server.MapPath("~/UserUploadedDocuments/Circular/CircularDataFile.pdf");
            //    SourceFile[1] = Server.MapPath("~/UserUploadedDocuments/Circular/" + FileName + "");
            //}
            //else
            //{
            //    SourceFile[0] = Server.MapPath("~/UserUploadedDocuments/Circular/CircularDataFile.pdf");
            //}
            //string Destination = Server.MapPath("~/UserUploadedDocuments/Circular/MeargedCircularFile_" + CID_Edit + ".pdf");

            //ExportToPDF(SourceFile, Destination, "", "Page ");
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
    protected void OnRowDataBound_grdNCRDetails(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblStauts = (Label)e.Row.FindControl("lblStauts");
            if (lblStauts.Text == "Rejected" || lblStauts.Text == "Inactive")
                e.Row.CssClass = "Pink";
            else
                e.Row.CssClass = "";

        }
    }

    //Searching Events
    protected void lnkShowAwatingApproval_OnClick(object sender, EventArgs e)
    {
        string sql = "", WhereClause = " where 1=1 ";
        sql = " select CID,CType,CircularNumber,substring(CircularNumber,len(CircularNumber)-7,8)CNForSorting,CircularDate,Category,Details,StateStatus" +
            " ,(case when Status<>2 then CFileName else 'CircularFile_'+ convert(varchar(10),Cid)+'.pdf' end)CFileName " +
            " ,(select CirName from dbo.CircularCategory CC where CC.CID=C.Category)CircularCatName" +
            " ,( case when len( convert(varchar(7000),C.Details))>55 then substring(C.Details,1,50)+'............'else  C.Details end ) as ShortDetails" +
            " ,(case when len( convert(varchar(7000),C.Topic))>60 then substring(C.Topic,1,60)+'............'else  C.Topic end )Topic" +
            " ,( case when Status<>2 then ( case when C.CFileName='' then 'none' else 'block' end) else 'block' end )ClipVisibility  " +

            " ,Source " +
            //" ,(select  (FirstName+' '+MiddleName+' '+FamilyName) from  dbo.Hr_PersonalDetails U where U.Empid=C.Source)SourceName" +

            " ,CreatedBy " +
            " ,(select  UserID from  dbo.userlogin U where U.Loginid=C.CreatedBy )CreatedByName " +
            " ,CreatedOn " +
            " ,replace(convert(varchar,CreatedOn,106),' ','-') CreatedOnText " +

            ",(select (FirstName+' '+MiddleName+''+FamilyName)EmpName  from dbo.Hr_PersonalDetails HR where HR.EmpID=c.SubmittedForApproval)SubmittedForApproval" +
            " ,replace(convert(varchar,SubmittedForApprovalOn,106),' ','-') SubmittedForApprovalOn " +

            " ,Status, 'True' as Visibility " +
            " ,(case when C.Status<>1 then 'none' else (case when SubmittedForApproval <> 0 then 'block' else 'none'end ) end) CommVisibility  " +
            " ,(case when C.Status<>1 then 'block' else (case when SubmittedForApproval <> 0 then 'none' else 'block'end ) end) StatusLableVisibility  " +
            //" ,(case when C.Status=1 then 'Awaiting Approval' when Status='2' then 'Approved' when Status='3' then 'Rejected' end) StatusText " +

            " ,(select count(cid) from createCircular where status=1)AwaitingCount " +
            " ,(select count(cid) from createCircular where status=3)RejectedCount " +

            " ,(case when C.Status=1 then 'Awaiting Approval' when Status='2' then (case when C.NextReviewDate>=getdate() then 'Active' when C.NextReviewDate < getdate() then 'Inactive' end) when Status='3' then 'Rejected' end) StatusText , newid() as rand   " +
            //" ,(case when C.Status=1 then 'Awaiting Approval' when Status='2' then (case when C.StateStatus=1 then 'Active' when C.StateStatus=2 then 'Inactive' when C.StateStatus=3 then 'In SMS' end) when Status='3' then 'Rejected' end) StatusText  "+
            " from CreateCircular C";

        
        WhereClause = WhereClause + " and C.Status=1 ";        
        sql = sql + WhereClause + " ";
        DataSet DS = Budget.getTable(sql);


        if (DS != null)
        {
            grdNCRDetails.DataSource = DS;
            grdNCRDetails.DataBind();

            if (DS.Tables[0].Rows.Count > 0)
            {
                lblTotalRec.Text = "Total No of Record : " + DS.Tables[0].Rows.Count;
                spanAwaitingApp.InnerHtml = "(" + DS.Tables[0].Rows[0]["AwaitingCount"].ToString() + ")";
                spanrejected.InnerHtml = "(" + DS.Tables[0].Rows[0]["RejectedCount"].ToString() + ")";
            }

        }
        else
        {
            lblTotalRec.Text = "Total No of Record : 0";
        }
    }
    protected void lnkShowRejected_OnClick(object sender,EventArgs e)
    {
        string sql = "", WhereClause = " where 1=1 ";
        sql = " select CID,CType,CircularNumber,substring(CircularNumber,len(CircularNumber)-7,8)CNForSorting,CircularDate,Category,Details,StateStatus" +
            " ,(case when Status<>2 then CFileName else 'CircularFile_'+ convert(varchar(10),Cid)+'.pdf' end)CFileName " +
            " ,(select CirName from dbo.CircularCategory CC where CC.CID=C.Category)CircularCatName" +
            " ,( case when len( convert(varchar(7000),C.Details))>55 then substring(C.Details,1,50)+'............'else  C.Details end ) as ShortDetails" +
            " ,(case when len( convert(varchar(7000),C.Topic))>60 then substring(C.Topic,1,60)+'............'else  C.Topic end )Topic" +
            " ,( case when Status<>2 then ( case when C.CFileName='' then 'none' else 'block' end) else 'block' end )ClipVisibility  " +

            " ,Source " +
            //" ,(select  (FirstName+' '+MiddleName+' '+FamilyName) from  dbo.Hr_PersonalDetails U where U.Empid=C.Source)SourceName" +

            " ,CreatedBy " +
            " ,(select  UserID from  dbo.userlogin U where U.Loginid=C.CreatedBy )CreatedByName " +
            " ,CreatedOn " +
            " ,replace(convert(varchar,CreatedOn,106),' ','-') CreatedOnText " +

            ",(select (FirstName+' '+MiddleName+''+FamilyName)EmpName  from dbo.Hr_PersonalDetails HR where HR.EmpID=c.SubmittedForApproval)SubmittedForApproval" +
            " ,replace(convert(varchar,SubmittedForApprovalOn,106),' ','-') SubmittedForApprovalOn " +

            " ,Status, 'True' as Visibility " +
            " ,(case when C.Status<>1 then 'none' else (case when SubmittedForApproval <> 0 then 'block' else 'none'end ) end) CommVisibility  " +
            " ,(case when C.Status<>1 then 'block' else (case when SubmittedForApproval <> 0 then 'none' else 'block'end ) end) StatusLableVisibility  " +
            //" ,(case when C.Status=1 then 'Awaiting Approval' when Status='2' then 'Approved' when Status='3' then 'Rejected' end) StatusText " +

            " ,(select count(cid) from createCircular where status=1)AwaitingCount " +
            " ,(select count(cid) from createCircular where status=3)RejectedCount " +

            " ,(case when C.Status=1 then 'Awaiting Approval' when Status='2' then (case when C.NextReviewDate>=getdate() then 'Active' when C.NextReviewDate < getdate() then 'Inactive' end) when Status='3' then 'Rejected' end) StatusText  " +
            //" ,(case when C.Status=1 then 'Awaiting Approval' when Status='2' then (case when C.StateStatus=1 then 'Active' when C.StateStatus=2 then 'Inactive' when C.StateStatus=3 then 'In SMS' end) when Status='3' then 'Rejected' end) StatusText  "+
            " from CreateCircular C";


        WhereClause = WhereClause + " and C.Status=3 ";
        sql = sql + WhereClause + " ";
        DataSet DS = Budget.getTable(sql);


        if (DS != null)
        {
            grdNCRDetails.DataSource = DS;
            grdNCRDetails.DataBind();

            if (DS.Tables[0].Rows.Count > 0)
            {
                lblTotalRec.Text = "Total No of Record : " + DS.Tables[0].Rows.Count;
                spanAwaitingApp.InnerHtml = "(" + DS.Tables[0].Rows[0]["AwaitingCount"].ToString() + ")";
                spanrejected.InnerHtml = "(" + DS.Tables[0].Rows[0]["RejectedCount"].ToString() + ")";
            }

        }
        else
        {
            lblTotalRec.Text = "Total No of Record : 0";
        }
    }
    // Function ----------------------------------------------------------------------------------------------------------------------------
    public void BindGrid()
    {
        string sql = "", WhereClause = " where 1=1 ";
        sql = " select CID,CType,CircularNumber,substring(CircularNumber,len(CircularNumber)-7,8)CNForSorting,CircularDate,Category,Details,StateStatus" +
            " ,(case when Status<>2 then CFileName else 'CircularFile_'+ convert(varchar(10),Cid)+'.pdf' end)CFileName " +
            " ,(select CirName from dbo.CircularCategory CC where CC.CID=C.Category)CircularCatName" +
            " ,(case when len( convert(varchar(7000),C.Details))>55 then substring(C.Details,1,50)+'............'else  C.Details end ) as ShortDetails" +
            " ,(case when len( convert(varchar(7000),C.Topic))>60 then substring(C.Topic,1,60)+'............'else  C.Topic end )Topic" +
            " ,( case when Status<>2 then ( case when C.CFileName='' then 'none' else 'block' end) else 'block' end )ClipVisibility  "+

            " ,Source " +
            //" ,(select  (FirstName+' '+MiddleName+' '+FamilyName) from  dbo.Hr_PersonalDetails U where U.Empid=C.Source)SourceName" +
            
            " ,CreatedBy " +
            " ,(select  UserID from  userlogin U where U.Loginid=C.CreatedBy )CreatedByName " +
            " ,CreatedOn " +
            " ,replace(convert(varchar,CreatedOn,106),' ','-') CreatedOnText " +

            ",(select (FirstName+' '+MiddleName+''+FamilyName)EmpName  from dbo.Hr_PersonalDetails HR where HR.EmpID=c.SubmittedForApproval)SubmittedForApproval" +
            " ,replace(convert(varchar,SubmittedForApprovalOn,106),' ','-') SubmittedForApprovalOn " +
            
            " ,Status, 'True' as Visibility "+
            " ,(case when C.Status<>1 then 'none' else (case when SubmittedForApproval <> 0 then 'block' else 'none'end ) end) CommVisibility  "+
            " ,(case when C.Status<>1 then 'block' else (case when SubmittedForApproval <> 0 then 'none' else 'block'end ) end) StatusLableVisibility  " +
            //" ,(case when C.Status=1 then 'Awaiting Approval' when Status='2' then 'Approved' when Status='3' then 'Rejected' end) StatusText " +

            " ,(select count(cid) from createCircular where status=1)AwaitingCount "+
            " ,(select count(cid) from createCircular where status=3)RejectedCount "+

            " ,(case when C.Status=1 then 'Awaiting Approval' when Status='2' then (case when C.NextReviewDate>=getdate() then 'Active' when C.NextReviewDate < getdate() then 'Inactive' end) when Status='3' then 'Rejected' end) StatusText , newid() as rand    " +
            //" ,(case when C.Status=1 then 'Awaiting Approval' when Status='2' then (case when C.StateStatus=1 then 'Active' when C.StateStatus=2 then 'Inactive' when C.StateStatus=3 then 'In SMS' end) when Status='3' then 'Rejected' end) StatusText  "+
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
            WhereClause = WhereClause + " and C.Details like '%" + txtSearchText.Text.Trim() + "%'";
        }
        //if (chkAwaitingApproval.Checked || chkRejected.Checked)
        //{
        //    if(chkRejected.Checked && chkAwaitingApproval.Checked )
        //        WhereClause = WhereClause + " and C.Status in(1,3) ";
        //    else if (chkAwaitingApproval.Checked)
        //        WhereClause = WhereClause + " and C.Status=1 ";
        //    else if(chkRejected.Checked)
        //        WhereClause = WhereClause + " and C.Status=3 ";
            
        //}
        //else
        
        if (ddlStatus.SelectedIndex != 0)
        {
            if (ddlStatus.SelectedValue == "1")
            {
                WhereClause = WhereClause + " and C.Status=2  and C.NextReviewDate >= getdate()";  //C.StateStatus=1 
            }
            else if (ddlStatus.SelectedValue == "2")
                WhereClause = WhereClause + " and C.Status=2 and C.NextReviewDate < getdate() ";
            else if (ddlStatus.SelectedValue == "3")
                WhereClause = WhereClause + " and C.Status=2 and C.StateStatus=3 ";
            
        }
        else
        {
            WhereClause = WhereClause +" and C.Status=2 ";
        }
        if (ddlType.SelectedIndex != 0)
        {
            WhereClause = WhereClause + " and C.CType='"+ddlType.SelectedValue+"'";
        }
        
        //if (ddlStatus.SelectedIndex != 0)
        //{
        //    string StatesWhereClause = "";
        //        StatesWhereClause = " and C.Status=2 and C.StateStatus=" + ddlStatus.SelectedValue + "";

        //    if(chkAwaitingApproval.Checked)
        //        StatesWhereClause = " and C.StateStatus=" + ddlStatus.SelectedValue + " or C.Status=1 ";
        //    if (chkRejected.Checked)
        //        StatesWhereClause = " and C.StateStatus=" + ddlStatus.SelectedValue + " or C.Status=3 ";
        //    if ((chkAwaitingApproval.Checked && chkRejected.Checked))
        //        StatesWhereClause = " and C.StateStatus=" + ddlStatus.SelectedValue + " or C.Status in (1,3) ";
        //    WhereClause = WhereClause + StatesWhereClause;
        //}
        //else
        //{
        //    string StatesWhereClause = "";

        //    StatesWhereClause = " and C.Status=2";
        //    if (chkAwaitingApproval.Checked)
        //        StatesWhereClause = " and C.Status=2 or C.Status=1 ";
        //    if (chkRejected.Checked)
        //        StatesWhereClause = " and C.Status=2 or C.Status=3 ";
        //    if ((chkAwaitingApproval.Checked && chkRejected.Checked))
        //        StatesWhereClause = " and C.Status=2 or C.Status in (1,3) ";
        //    WhereClause = WhereClause + StatesWhereClause;
        //}

        //if (chkAwaitingApproval.Checked)
        //{
        //    WhereClause = WhereClause + " and C.Status=1";
        //}
        //if (chkRejected.Checked)
        //{
        //    WhereClause = WhereClause + " and C.Status=3";
        //}
        sql = sql + WhereClause + " order by substring(CircularNumber,len(CircularNumber)-7,8) Asc";
        DataSet DS = Budget.getTable(sql);

        
        if (DS != null)
        {   
            grdNCRDetails.DataSource = DS;
            grdNCRDetails.DataBind();



            string SqlCnt = "select (select count(cid) from createCircular where status=1)AwaitingCount  ,(select count(cid) from createCircular where status=3)RejectedCount  ";
            DataSet Dscnt = Budget.getTable(SqlCnt);
            if (Dscnt != null)
            {
                lblTotalRec.Text = "Total No of Record : " + DS.Tables[0].Rows.Count;
                spanAwaitingApp.InnerHtml = "(" + Dscnt.Tables[0].Rows[0]["AwaitingCount"].ToString() + ")";
                spanrejected.InnerHtml = "(" + Dscnt.Tables[0].Rows[0]["RejectedCount"].ToString() + ")";
            }
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
            ddlCategory.Items.Insert(0,new System.Web.UI.WebControls.ListItem(" All ","0"));
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
    private void ExportToPDF(string[] SourceFiles, string DestFile, string Header, string Footer)
    {
        MemoryStream MemStream = new MemoryStream();
        iTextSharp.text.Document doc = new iTextSharp.text.Document();
        iTextSharp.text.pdf.PdfReader reader;
        Int32 numberOfPages;
        Int32 currentPageNumber;
        iTextSharp.text.pdf.PdfWriter writer = iTextSharp.text.pdf.PdfWriter.GetInstance(doc, MemStream);
        //-----------------------
        //Adding Header in document
        Phrase p1 = new Phrase();
        p1.Add(new Phrase(Header, FontFactory.GetFont("ARIAL", 9, iTextSharp.text.Font.BOLD)));
        HeaderFooter header = new HeaderFooter(p1, false);
        doc.Header = header;
        header.Alignment = Element.ALIGN_CENTER;
        header.Border = iTextSharp.text.Rectangle.NO_BORDER;
        //Adding Footer in document
        HeaderFooter footer = new HeaderFooter(new Phrase(Footer, FontFactory.GetFont("ARIAL", 7, iTextSharp.text.Font.NORMAL)), true);
        footer.Border = iTextSharp.text.Rectangle.NO_BORDER;
        footer.Alignment = Element.ALIGN_LEFT;
        doc.Footer = footer;

        //-----------------------
        doc.Open();
        iTextSharp.text.pdf.PdfContentByte cb = writer.DirectContent;
        iTextSharp.text.pdf.PdfImportedPage page;
        int rotation;
        //BaseFont baseFont = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, Encoding.ASCII.EncodingName, false);
        BaseFont baseFont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
        for (int i = 0; i <= SourceFiles.Length - 1; i++)
        {
            Byte[] sqlbytes = File.ReadAllBytes(SourceFiles[i]);
            reader = new iTextSharp.text.pdf.PdfReader(sqlbytes);
            numberOfPages = reader.NumberOfPages;
            currentPageNumber = 0;
            while (currentPageNumber < numberOfPages)
            {
                currentPageNumber += 1;
                doc.SetPageSize(PageSize.A4);
                doc.NewPage();
                page = writer.GetImportedPage(reader, currentPageNumber);
                rotation = reader.GetPageRotation(currentPageNumber);
                if ((rotation == 90) || (rotation == 270))
                    cb.AddTemplate(page, 0, -1.0F, 1.0F, 0, 0, reader.GetPageSizeWithRotation(currentPageNumber).Height);
                else
                    cb.AddTemplate(page, 1.0F, 0, 0, 1.0F, 0, 0);
            }
        }
        if (MemStream == null)
        {
            // error message
        }
        else
        {
            doc.Close();
            FileStream fs = new FileStream(DestFile, FileMode.OpenOrCreate, FileAccess.Write);
            fs.Write(MemStream.GetBuffer(), 0, MemStream.GetBuffer().Length);
            fs.Flush();
            fs.Close();
            MemStream.Close();
        }
    }
    private void CreatePDF()
    {
        try
        {
            string sql = "select Topic,Details,SuperSedes ,CircularNumber,Source" +
               " ,(select CirName from dbo.CircularCategory CC where CC.CID=C.Category)CircularCatName " +
               " ,replace(convert(varchar, CircularDate ,106),' ','-')CircularDate" +
               //",(select UserID EmpName from Hr_PersonalDetails HR where HR.EmpID=c.ApprejBy)ApprejBy" +

               " ,(select FirstName+' '+LastName from UserLogin U where U.LoginID =c.ApprejBy)ApprejBy " +

               " ,replace(convert(varchar,ApprejOn,106),' ','-') ApprejOn " +

               " ,(select FirstName+' '+LastName from UserLogin U where U.LoginID=C.CreatedBy)CreatedByName " +
               " ,replace(convert(varchar,CreatedOn,106),' ','-')CreatedOnText "+

               " from createCircular C where CID=" + CID_Edit + "";
            DataSet ds = Budget.getTable(sql);

            string Topic = ds.Tables[0].Rows[0]["Topic"].ToString();
            string CircularCatName = ds.Tables[0].Rows[0]["CircularCatName"].ToString();
            string Details = ds.Tables[0].Rows[0]["Details"].ToString();
            string CircularDate = ds.Tables[0].Rows[0]["CircularDate"].ToString();
            string SuperSedes = ds.Tables[0].Rows[0]["SuperSedes"].ToString();
            string CircularNumber = ds.Tables[0].Rows[0]["CircularNumber"].ToString();
            vCircularNumber = ds.Tables[0].Rows[0]["CircularNumber"].ToString();

            string ApprejBy = ds.Tables[0].Rows[0]["ApprejBy"].ToString();
            vApprejBy = ds.Tables[0].Rows[0]["ApprejBy"].ToString();

            string ApprejOn = ds.Tables[0].Rows[0]["ApprejOn"].ToString();
            vApprejOn = ds.Tables[0].Rows[0]["ApprejOn"].ToString();
            string Source = ds.Tables[0].Rows[0]["Source"].ToString();

            vCreatedBy = ds.Tables[0].Rows[0]["CreatedByName"].ToString();
            vCreatedOn = ds.Tables[0].Rows[0]["CreatedOnText"].ToString();

            Document document = new Document(PageSize.A4, 10, 10, 30, 40);
            System.IO.MemoryStream msReport = new System.IO.MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(document, msReport);
            //document.AddAuthor("MTMSM");
            //document.AddSubject("Follow Up Sheet");

            //'Adding Header in Document --------------------------------------------------

            //iTextSharp.text.Image logoImg = default(iTextSharp.text.Image);
            //logoImg = iTextSharp.text.Image.GetInstance(System.Windows.Forms.Application.StartupPath + "\\Images\\MTMMlogo.jpg");
            //logoImg.SetAbsolutePosition(0, 5);
            //logoImg.ScalePercent(59);
            //Chunk chk = new Chunk(logoImg, 10, 10, true);

            Phrase p1 = new Phrase();
            // Adding Image --------------------------------------------------
            //p1.Add(chk);

            // Adding Vessel Name 
            //p1.Add(new Phrase("            " + Source, FontFactory.GetFont("ARIAL", 14, iTextSharp.text.Font.BOLD)));
            //Adding As On Date
            //Phrase ph1 = new Phrase("                                                                    ", FontFactory.GetFont("ARIAL", 6, iTextSharp.text.Font.ITALIC));
            //p1.Add(ph1);


            HeaderFooter header = new HeaderFooter(p1, false);
            document.Header = header;
            header.Alignment = Element.ALIGN_LEFT;
            header.Border = iTextSharp.text.Rectangle.NO_BORDER;
            //'Adding Footer in document
            string foot_Txt = "";
            foot_Txt = foot_Txt + "";
            foot_Txt = foot_Txt + "";
            //foot_Txt = foot_Txt + "Approved By  : " + ApprejBy + "              Approved On : " + ApprejOn + "";
            HeaderFooter footer = new HeaderFooter(new Phrase(foot_Txt, FontFactory.GetFont("VERDANA", 8, iTextSharp.text.Color.DARK_GRAY)),false);
            footer.Border = iTextSharp.text.Rectangle.NO_BORDER;
            footer.Alignment = Element.ALIGN_LEFT;
            document.Footer = footer;
            //'-----------------------------------

            document.Open();
            //------------ TABLE HEADER FONT 
            iTextSharp.text.Font fCapText = FontFactory.GetFont("ARIAL", 11, iTextSharp.text.Font.BOLD);
            iTextSharp.text.Font fCapTextTitle = FontFactory.GetFont("ARIAL", 11, iTextSharp.text.Font.BOLD);
            iTextSharp.text.Font fCapTextDetails = FontFactory.GetFont("ARIAL", 10, iTextSharp.text.Font.NORMAL);
            iTextSharp.text.Font fCapTextCirNum = FontFactory.GetFont("ARIAL", 10, iTextSharp.text.Font.ITALIC);
            iTextSharp.text.Font fCapTextHeading = FontFactory.GetFont("ARIAL", 15, iTextSharp.text.Font.BOLD);
            iTextSharp.text.Font fCapTextCompheading = FontFactory.GetFont("ARIAL", 16, iTextSharp.text.Font.BOLD);
            //iTextSharp.text.Font fNoCellBorder = FontFactory.GetFont("ARIAL", 2, iTextSharp.text.Font.BOLD,);

            //------------ TABLE HEADER FIRST ROW 
            //iTextSharp.text.Table tb1 = new iTextSharp.text.Table(2);
            //tb1.BackgroundColor = iTextSharp.text.Color.LIGHT_GRAY;
            //tb1.Width = 90;
            //float[] ws = { 50 , 50 };
            //tb1.Widths = ws;
            //tb1.Alignment = Element.ALIGN_CENTER;
            //tb1.Border = iTextSharp.text.Rectangle.NO_BORDER;
            //tb1.DefaultHorizontalAlignment = Element.ALIGN_CENTER;
            //tb1.BorderColor = iTextSharp.text.Color.WHITE;
            //tb1.Cellspacing = 1;
            //tb1.Cellpadding = 1;
            //tb1.AddCell(new Phrase("Sr#", fCapText));
            //tb1.AddCell(new Phrase("Description", fCapText));
            //tb1.AddCell(new Phrase("Due Date", fCapText));
            //tb1.AddCell(new Phrase("Completion Date", fCapText));
            //tb1.AddCell(new Phrase("Responsibility", fCapText));
            //document.Add(tb1);




            //------------Company Table
            iTextSharp.text.Table tbCom = new iTextSharp.text.Table(1);
            tbCom.DefaultCellBackgroundColor = iTextSharp.text.Color.WHITE;
            tbCom.Width = 90;
            float[] wsCom = { 100 };
            tbCom.Widths = wsCom;
            tbCom.Alignment = Element.ALIGN_CENTER;
            tbCom.Border = iTextSharp.text.Rectangle.NO_BORDER;
            tbCom.DefaultCellBorder = iTextSharp.text.Rectangle.NO_BORDER;            
            tbCom.DefaultHorizontalAlignment = Element.ALIGN_CENTER;
            tbCom.BorderColor = iTextSharp.text.Color.WHITE;
            tbCom.Cellspacing = 1;
            tbCom.Cellpadding = 1;
            tbCom.AddCell(new Phrase("M.T.M. SHIP MANAGEMENT PTE. LTD.", fCapTextCompheading));
            document.Add(tbCom);
            document.Add(new Phrase("\n"));

            //------------First TABLE 
            iTextSharp.text.Table tb1 = new iTextSharp.text.Table(2);
            tb1.DefaultCellBackgroundColor = iTextSharp.text.Color.WHITE;
            tb1.Width = 90;
            float[] ws1 = { 50, 50 };
            tb1.Widths = ws1;
            tb1.Alignment = Element.ALIGN_CENTER;
            tb1.Border = iTextSharp.text.Rectangle.NO_BORDER;
            tb1.DefaultHorizontalAlignment = Element.ALIGN_LEFT;
            tb1.BorderColor = iTextSharp.text.Color.WHITE;
            tb1.Cellspacing = 1;
            tb1.Cellpadding = 1;
            tb1.AddCell(new Phrase("Category : " + CircularCatName + "", fCapText));
            tb1.AddCell(new Phrase("Date : " + CircularDate + "", fCapText));
            //tb1.AddCell(new Phrase("Source : " + Source + "", fCapText));
            //tb1.AddCell(new Phrase(""));
            document.Add(tb1);
            //document.Add(new Phrase("\n"));

            //------------Source Table
            iTextSharp.text.Table tbS = new iTextSharp.text.Table(1);
            tbS.DefaultCellBackgroundColor = iTextSharp.text.Color.WHITE;
            tbS.Width = 90;
            float[] wsS = { 50};
            tbS.Widths = wsS;
            tbS.Alignment = Element.ALIGN_CENTER;
            tbS.Border = iTextSharp.text.Rectangle.NO_BORDER;
            tbS.DefaultCellBorder = iTextSharp.text.Rectangle.BOX;
            tbS.DefaultHorizontalAlignment = Element.ALIGN_LEFT;
            tbS.BorderColor = iTextSharp.text.Color.WHITE;
            tbS.Cellspacing = 1;
            tbS.Cellpadding = 1;
            tbS.AddCell(new Phrase("Source : " + Source + "", fCapText));
            document.Add(tbS);
            document.Add(new Phrase("\n"));


            //------------Second TABLE 
            iTextSharp.text.Table tb2 = new iTextSharp.text.Table(1);
            tb2.BackgroundColor = iTextSharp.text.Color.WHITE;
            tb2.Width = 90;
            float[] ws2 = { 100 };
            tb2.Widths = ws2;
            tb2.Alignment = Element.ALIGN_CENTER;
            tb2.Border = iTextSharp.text.Rectangle.NO_BORDER;
            tb2.DefaultCellBorder = iTextSharp.text.Rectangle.NO_BORDER;
            tb2.DefaultHorizontalAlignment = Element.ALIGN_LEFT;
            tb2.BorderColor = iTextSharp.text.Color.WHITE;
            tb2.Cellspacing = 1;
            tb2.Cellpadding = 1;
            tb2.AddCell(new Phrase(" ", fCapTextHeading));
            tb2.AddCell(new Phrase("Circular Letter :                           " + CircularNumber + "                         \n", fCapTextHeading));
            //tb2.AddCell(new Phrase("" + CircularNumber + "", fCapTextCirNum));
            document.Add(tb2);
            //document.Add(new Phrase("\n"));

            //------------third TABLE 
            iTextSharp.text.Table tb3 = new iTextSharp.text.Table(1);
            tb3.BackgroundColor = iTextSharp.text.Color.LIGHT_GRAY;
            tb3.Width = 90;
            float[] ws3 = { 100 };
            tb3.Widths = ws3;
            tb3.Alignment = Element.ALIGN_CENTER;
            tb3.Border = iTextSharp.text.Rectangle.NO_BORDER;
            tb3.DefaultHorizontalAlignment = Element.ALIGN_LEFT;
            tb3.DefaultVerticalAlignment = Element.ALIGN_TOP;
            tb3.BorderColor = iTextSharp.text.Color.WHITE;
            tb3.Cellspacing = 1;
            tb3.Cellpadding = 1;
            tb3.AddCell(new Phrase("Topic - " + Topic, fCapTextTitle));
            document.Add(tb3);

            //------------ Fourth TABLE 
            iTextSharp.text.Table tb4 = new iTextSharp.text.Table(1);
            tb4.BackgroundColor = iTextSharp.text.Color.WHITE;
            tb4.Width = 90;
            float[] ws4 = { 100 };
            tb4.Widths = ws4;
            tb4.Alignment = Element.ALIGN_CENTER;
            tb4.Border = iTextSharp.text.Rectangle.NO_BORDER;
            tb4.DefaultCellBorder = iTextSharp.text.Rectangle.NO_BORDER;
            tb4.DefaultHorizontalAlignment = Element.ALIGN_LEFT;
            tb4.BorderColor = iTextSharp.text.Color.WHITE;
            tb4.Cellspacing = 1;
            tb4.Cellpadding = 1;
            tb4.AddCell(new Phrase(Details, fCapTextDetails));
            document.Add(tb4);



            document.Close();
            if (File.Exists(Server.MapPath("~/UserUploadedDocuments/Circular/" + "CircularDataFile.pdf")))
            {
                File.Delete(Server.MapPath("~/UserUploadedDocuments/Circular/" + "CircularDataFile.pdf"));
            }
            FileStream fs = new FileStream(Server.MapPath("~/UserUploadedDocuments/Circular/CircularDataFile.pdf"), FileMode.Create);
            byte[] bb = msReport.ToArray();
            fs.Write(bb, 0, bb.Length);
            fs.Flush();
            fs.Close();
            //AddMessage("Pdf file(" + FileName + ") created successfully.");
        }
        catch (System.Exception ex)
        {
            //AddMessage("Error while creating file(" + FileName + "). ", ex.Message);
        }
    }
    public void WritePDFFooter()
    {
        // step 1: creation of a document-object
        Document document = new Document();

        try
        {

            // step 2:
            // we create a writer that listens to the document
            // and directs a PDF-stream to a file

            //PdfWriter writer = PdfWriter.getInstance(document, new FileStream("Chap1002.pdf", FileMode.Create));

            iTextSharp.text.pdf.PdfWriter writer = iTextSharp.text.pdf.PdfWriter.GetInstance(document, new FileStream(Server.MapPath("~/UserUploadedDocuments/Circular/CircularFile_" + CID_Edit + ".pdf"), FileMode.Append));
            
            // step 3: we open the document
            document.Open();

            // step 4: we grab the ContentByte and do some stuff with it
            //PdfContentByte cb = writer.DirectContent;
            iTextSharp.text.pdf.PdfContentByte cb = writer.DirectContent;
            
            // we tell the ContentByte we're ready to draw text
            cb.BeginText();
            BaseFont mybf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            // we draw some text on a certain position
            //add text and images
            document.Add(new Paragraph(document.BottomMargin, "footer text footer text footer text footer text"));

            cb.SetFontAndSize(mybf, 10);
            cb.SetTextMatrix(100,15);
            
            //cb.ShowText("Text at position 100,400.");

            // we tell the contentByte, we've finished drawing text
            cb.EndText();
        }
        catch (DocumentException de)
        {
            Console.Error.WriteLine(de.Message);
        }
        catch (IOException ioe)
        {
            Console.Error.WriteLine(ioe.Message);
        }

        // step 5: we close the document
        document.Close();
    }
    public int GetEmpID(int LoginID)
    {
        DataSet ds = Budget.getTable("select EmpID from Hr_PersonalDetails where UserID="+LoginID+"");
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
                return Common.CastAsInt32(ds.Tables[0].Rows[0][0]);
        }
        return 0;
    }
    
}
