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

public partial class CircularData : System.Web.UI.Page
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
        lblMsgAddTopic.Text = "";
        lblAppRejTxtEntry.Text = "";
        lblAccepetedOrRejectText.Text = "";
        lblMsgAppRej.Text = "";
        
        if (Page.Request.QueryString["VesselCode"] != null)
        {
            VesselCode = Page.Request.QueryString["VesselCode"].ToString();
            VesselName = Page.Request.QueryString["VesselName"].ToString();
        }
        if (!Page.IsPostBack)
        {
            ddlStatus.SelectedValue = "1";
            txtFrom.Text = DateTime.Parse("01/01/" + DateTime.Today.Year.ToString()).ToString("dd-MMM-yyyy");//daysinmonth.ToString("dd-MMM-yyyy");
            txtTo.Text = DateTime.Today.ToString("dd-MMM-yyyy");   //daysinmonth.AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
            BindCategory();
            BindGrid();

        }
    }

    // Events ----------------------------------------------------------------------------------------------------------------------------
    protected void Save_OnClick(object sender, EventArgs e)
    {
        try
        {
            if (ddlSavingCat.SelectedIndex == 0)
            {
                lblMsgAddTopic.Text = "Please select category.";
                lblMsg.Focus();
                return;
            }
            if (txtComments.Text.Trim() == "")
            {
                lblMsgAddTopic.Text = "Please Enter Comments.";
                lblMsg.Focus();
                return;
            }
            if (Session["loginid"] != null)
            {
                // For FileUpLoad
                string FileName = "";
                //if (fuFile.PostedFile != null && fuFile.FileContent.Length > 0)
                {
                    if (chk_FileExtension(Path.GetExtension(fuFile.FileName).ToLower()) == true)
                    {
                        FileName = fuFile.FileName;
                    }
                }
                //---------------------------------------------------------------------------------------------------------------

                string sql = "";
                if (CID_Edit == 0)
                {
                    sql = "sp_InsertCircularData " + ddlSavingCat.SelectedValue + ",'" + txtComments.Text.Trim() + "','" + FileName + "'," + Common.CastAsInt32(Session["loginid"].ToString()) + "";
                }
                else
                {
                    sql = "sp_UpdateCircularData "+CID_Edit+", " + ddlSavingCat.SelectedValue + ",'" + txtComments.Text.Trim() + "','" + FileName + "'";
                }

                DataSet Ds = Budget.getTable(sql);

                if (CID_Edit == 0)
                {
                    lblMsg.Text = "Record Saved Successfully.";
                    dvConfirmCancel.Visible = false;
                    
                    //Get max id n upload file
                    if (fuFile.HasFile)
                    {
                        if (CID_Edit == 0)
                        {
                            string maxID = "", sqlID = "";
                            sqlID = "select max(id) from Circular";
                            DataSet DsID = Budget.getTable(sqlID);
                            if (DsID != null)
                            {
                                if (DsID.Tables[0].Rows.Count > 0)
                                {
                                    maxID = DsID.Tables[0].Rows[0][0].ToString();
                                    FileName = FileName.Insert((FileName.LastIndexOf('.')), "_" + maxID);

                                    string UpDateSql = "update Circular set CFileName  ='" + FileName + "' where id=" + maxID + " select 11";
                                    DataSet DsIDUpdate = Budget.getTable(UpDateSql);

                                    fuFile.SaveAs(Server.MapPath("~/UserUploadedDocuments/Circular/" + FileName));

                                }
                            }
                        }
                        else
                        {
                            
                        }
                    }
                    //----------------------------------------------------------------------------------------------------------
                }

                else
                {
                    lblMsg.Text = "Record Updated Successfully.";
                    dvConfirmCancel.Visible = false;
                    if (fuFile.HasFile)
                    {
                        FileName = FileName.Insert((FileName.LastIndexOf('.')), "_" + CID_Edit);
                        string UpDateSql = "update Circular set CFileName  ='" + FileName + "' where id=" + CID_Edit + " select 11";
                        DataSet DsIDUpdate = Budget.getTable(UpDateSql);

                        fuFile.SaveAs(Server.MapPath("~/UserUploadedDocuments/Circular/" + FileName));

                        grdNCRDetails.SelectedIndex = -1;
                    }


                }
                tblEnterNotes.Visible = false;
                //ScriptManager.RegisterStartupScript(Page, this.GetType(), "Reload", "RefereshParrentPage();", true);
                BindGrid();
                CID_Edit = 0;

            }
            else
            {
                lblMsg.Text = "Record Not Saved.";

            }
        }
        catch (Exception ex) { throw ex; }
    }
    protected void lnlAddnewCircular_OnClick(object sender, EventArgs e)
    {
        tblEnterNotes.Visible = true;
        dvConfirmCancel.Visible = true;
        tblViewData.Visible = false;
        aAddFile.Visible = false;
        lnkClearFile.Visible = false;
        btnCancel.Visible = false;

        txtComments.Text = "";
        ddlSavingCat.SelectedIndex = 0;
    }
    protected void imgApprove_OnClick(object sender, EventArgs e)
    {
        dvConfirmCancel.Visible = true;
    }
    protected void lnkClearFile_OnClick(object sender, EventArgs e)
    {
        if (CID_Edit != 0)
        {
            string sql = "update Circular set CFileName='' where id=" + CID_Edit + " select 1111";
            DataSet ds = Budget.getTable(sql);
            if (ds != null)
            {
                aAddFile.Visible = false;
                BindGrid();
            }
        }
    }
    protected void btnEditCircular_OnClick(object sender, EventArgs e)
    {
        tblEnterNotes.Visible = true;
        tblViewData.Visible = false;
        btnCancel.Visible = true;
        
    }
    protected void btnApprove_OnClick(object sender, EventArgs e)
    {
        divViewDesc.Style.Add(HtmlTextWriterStyle.Height, "150px");
        lblAppRejTxtEntry.Text = "Enter Approval Comments";
        trAppRej.Visible = true;
        Status = "2";
    }
    protected void btnReject_OnClick(object sender, EventArgs e)
    {
        divViewDesc.Style.Add(HtmlTextWriterStyle.Height, "150px");
        lblAppRejTxtEntry.Text = "Enter Rejection Comments";
        trAppRej.Visible = true;
        Status = "3";
    }
    protected void btnCancelAppRejComm_OnClick(object sender, EventArgs e)
    {
        divViewDesc.Style.Add(HtmlTextWriterStyle.Height, "300px");
        lblAppRejTxtEntry.Text = "";
        trAppRej.Visible = false; 
    }
    protected void btnSaveAppRejComm_OnClick(object sender, EventArgs e)
    {
        
        if (txtAppRejComments.Text.Trim() == "")
        {
            lblMsgAppRej.Text = "Please enter comments.";
            lblMsgAppRej.Focus();
            return;
        }
        string sql = "sp_UpdataApprovalRejectionComments " + CID_Edit + "," + Common.CastAsInt32(Session["loginid"].ToString()) + ",'" + txtAppRejComments.Text.Trim() + "'," + Status + "";
        DataSet ds = Budget.getTable(sql);
        if (ds != null)
        {
            lblMsg.Text = "Record Updated Successfully.";
            trAppRej.Visible = false;
            btnApprove.Visible = false;
            btnReject.Visible = false;
            btnEditCircular.Visible = false;
            BindGrid();

            if (Status == "2")
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "CreateCir", "CreatCircular(" + CID_Edit + ")", true);
        }
    }
    protected void btnCreateCircular_OnClick(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "CreateCir", "CreatCircular1(" + CID_Edit + ")", true);
    }


    protected void imgEditTopic_OnClick(object sender, EventArgs e)
    {
        trAppRej.Visible = false;
        divViewDesc.Style.Add(HtmlTextWriterStyle.Height, "300px;");
        ImageButton imgEditTopic = (ImageButton)sender;
        int rowIndex = Convert.ToInt32(imgEditTopic.Attributes["RowIndex"]);

        HiddenField hfCID = (HiddenField)imgEditTopic.Parent.FindControl("hfCID");
        HiddenField hfUploadedFile = (HiddenField)imgEditTopic.Parent.FindControl("hfUploadedFile");
        HiddenField hfCatID = (HiddenField)imgEditTopic.Parent.FindControl("hfCatID");

        Label lblCategory = (Label)imgEditTopic.Parent.FindControl("lblCategory");
        Label lblStauts = (Label)imgEditTopic.Parent.FindControl("lblStauts");
        Label lblCreatedBy = (Label)imgEditTopic.Parent.FindControl("lblCreatedBy");
        Label lblCreatedOn = (Label)imgEditTopic.Parent.FindControl("lblCreatedOn");

        HiddenField hfAccBy = (HiddenField)imgEditTopic.Parent.FindControl("hfAccBy");
        HiddenField hfAccOn = (HiddenField)imgEditTopic.Parent.FindControl("hfAccOn");

        
        HiddenField hfStatusID = (HiddenField)imgEditTopic.Parent.FindControl("hfStatusID");
        

        if (hfUploadedFile.Value == "")
        {
            aAddFile.Visible = false;
            aviewUploadedFile.Visible = false;
            lnkClearFile.Visible = false;
        }
        else
        {
            aAddFile.Visible = true;
            aviewUploadedFile.Visible = true;
            lnkClearFile.Visible = true;
            aAddFile.HRef = "../UserUploadedDocuments/Circular/" + hfUploadedFile.Value;
            aviewUploadedFile.HRef = "../UserUploadedDocuments/Circular/" + hfUploadedFile.Value;
        }
        
        Label lblFullCirculer = (Label)imgEditTopic.Parent.FindControl("lblFullCirculer");
        CID_Edit = Common.CastAsInt32(hfCID.Value);

        // Data for View
        if (hfAccBy.Value != "")
        {

            if (hfStatusID.Value == "1")
            {
                lblAccepetedOrRejectText.Text = "";
                btnEditCircular.Visible = true;
            }
            else if (hfStatusID.Value == "2")
            {
                lblAccepetedOrRejectText.Text = "Accepted By / On :";
                btnApprove.Visible = false;
                btnReject.Visible = false;
                btnEditCircular.Visible = false;
                btnCreateCircular.Visible = true;
            }
            else if (hfStatusID.Value == "3")
            {
                lblAccepetedOrRejectText.Text = "Rejected By / On :";
                btnApprove.Visible = false;
                btnReject.Visible = false;
                btnEditCircular.Visible = false;
            }
            lblviewAccBy.Text = hfAccBy.Value;
            lblviewAccOn.Text = "  /  " + hfAccOn.Value;


        }
        else
        {
            btnApprove.Visible = true;
            btnReject.Visible = true;
            btnEditCircular.Visible = true;
        }

        lblviewCreatedBy.Text = lblCreatedBy.Text +"  /  ";
        lblviewCreatedOn.Text = lblCreatedOn.Text;
        lblviewCategory.Text = lblCategory.Text;
        lblviewStatus.Text = lblStauts.Text;

        
        lblviewDesc.Text = lblFullCirculer.Text;
            
        // Data for Edit
        //tblEnterNotes.Visible = true;
        txtComments.Text = lblFullCirculer.Text;
        ddlSavingCat.SelectedValue = hfCatID.Value;

        grdNCRDetails.SelectedIndex = rowIndex;
        dvConfirmCancel.Visible = true;
        //selectedtowstyle

        tblEnterNotes.Visible = false;
        tblViewData.Visible = true;

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
        string sql = "", WhereClause = " where 1=1 ";
        sql = " select ID ,CircularCat" +
            " ,(select CirName from dbo.CircularCategory CC where CC.CID=C.CircularCat)CircularCatName" +
            " ,Circular " +
            " ,( case when len( convert(varchar(7000),C.Circular ))>95 then substring(C.Circular,1,90)+'............'else  C.Circular end ) as ShortCircular" +
            " ,CFileName " +
            " ,(case when C.CFileName='' then 'none' else 'block' end)ClipVisibility " +
            " ,CreatedBy " +
            " ,(select  UserID from  dbo.userlogin U where U.Loginid=C.CreatedBy )CreatedByName " +

            " ,replace(convert(varchar,CreatedOn ,106),' ','-') CreatedOnText " +
            " ,CreatedOn " +

            " ,replace(convert(varchar,AppOrRejectedDate,106),' ','-')AppOrRejectedDate" +
            " ,(select  UserID from  dbo.userlogin U where U.Loginid=C.AppOrRejectedBy )AppOrRejectedByText " +


            " ,AppOrRejectedBy   " +
            " ,AppOrRejectedComments " +
            " ,Status, 'True' as Visibility ,(case when C.Status=1 then 'Requested' when Status='2' then 'Accepted' when Status=3 then 'Rejected' end) StatusText from Circular C";


        if (txtFrom.Text.Trim() != "")
        {
            WhereClause = WhereClause + " and C.CreatedOn>='" + txtFrom.Text.Trim() + "'";
        }
        if (txtTo.Text.Trim() != "")
        {
            WhereClause = WhereClause + " and C.CreatedOn<='" + Convert.ToDateTime(txtTo.Text.Trim()).AddDays(1).ToString() + "'";
        }
        if (ddlCategory.SelectedIndex != 0)
        {
            WhereClause = WhereClause + " and C.CircularCat=" + ddlCategory.SelectedValue + "";
        }
        if (txtSearchText.Text.Trim() != "")
        {
            WhereClause = WhereClause + " and T.Circular like '%" + txtSearchText.Text.Trim() + "%'";
        }
        if (ddlStatus.SelectedIndex != 0)
        {
            WhereClause = WhereClause + " and C.Status=" + ddlStatus.SelectedValue + "";
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
        ddlStatus.SelectedIndex = 0;
        txtSearchText.Text = "";
        ddlCategory.SelectedIndex = 0;
        grdNCRDetails.SelectedIndex = -1;
    }
    protected void Cancel_OnClick(object sender, EventArgs e)
    {
        tblEnterNotes.Visible = false;
        tblViewData.Visible = true;
        //CID_Edit = 0;
        grdNCRDetails.SelectedIndex = -1;
        //txtComments.Text = "";
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
    protected void btnOpen_OnClick(object sender, EventArgs e)
    {

    }
    protected void btnClosure_OnClick(object sender, EventArgs e)
    {

    }
    protected void imgCalcel_OnClick(object sender, EventArgs e)
    {
        //dvConfirmCancel.Visible = false;
        tblEnterNotes.Visible = false;
        tblViewData.Visible = true;
    }
    protected void btnClose_OnClick(object sender, EventArgs e)
    {
        dvConfirmCancel.Visible = false;
        CID_Edit = 0;
    }

    // Function ----------------------------------------------------------------------------------------------------------------------------
    public void BindGrid()
    {
        string sql = "", WhereClause = " where 1=1 ";
        sql = " select ID ,CircularCat" +
            " ,(select CirName from dbo.CircularCategory CC where CC.CID=C.CircularCat)CircularCatName" +
            " ,Circular " +
            " ,( case when len( convert(varchar(7000),C.Circular ))>95 then substring(C.Circular,1,90)+'............'else  C.Circular end ) as ShortCircular" +
            " ,CFileName " +
            " ,(case when C.CFileName='' then 'none' else 'block' end)ClipVisibility "+
            " ,CreatedBy " +
            " ,(select  UserID from  dbo.userlogin U where U.Loginid=C.CreatedBy )CreatedByName " +

            " ,replace(convert(varchar,CreatedOn ,106),' ','-') CreatedOnText " +
            " ,CreatedOn " +
                        
            " ,replace(convert(varchar,AppOrRejectedDate,106),' ','-')AppOrRejectedDate" +
            " ,(select  UserID from  dbo.userlogin U where U.Loginid=C.AppOrRejectedBy )AppOrRejectedByText " +


            " ,AppOrRejectedBy   " +
            " ,AppOrRejectedComments " +
            " ,Status, 'True' as Visibility ,(case when C.Status=1 then 'Requested' when Status='2' then 'Accepted' when Status=3 then 'Rejected' end) StatusText from Circular C";



        if (txtFrom.Text.Trim() != "")
        {
            WhereClause = WhereClause + " and C.CreatedOn>='" + txtFrom.Text.Trim() + "'";
        }
        if (txtTo.Text.Trim() != "")
        {
            WhereClause = WhereClause + " and C.CreatedOn<='" + Convert.ToDateTime(txtTo.Text.Trim()).AddDays(1).ToString() + "'";
        }
        if (ddlCategory.SelectedIndex != 0)
        {
            WhereClause = WhereClause + " and C.CircularCat=" + ddlCategory.SelectedValue + "";
        }
        if (txtSearchText.Text.Trim() != "")
        {
            WhereClause = WhereClause + " and T.Circular like '%" + txtSearchText.Text.Trim() + "%'";
        }
        if (ddlStatus.SelectedIndex != 0)
        {
            WhereClause = WhereClause + " and C.Status=" + ddlStatus.SelectedValue + "";
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
    public void BindGridOffice()
    {
        string sql = "select OfficeName,NCRmaker,replace(convert(varchar,NCRCreatedDate,106),' ','-')NCRCreatedDate,(case when audit='ext' then 'External' else 'Internal' end)audit " +
        " ,(case when audit='ext' then (select extvalue from tbl_External E where E.EXTID=NCR.Auditvalue) else (select intvalue from tbl_Internal I where  I.INTID=NCR.Auditvalue) end)AuditedText " +
        " from tbl_NCR NCR where reportFor='OFF'";
        DataSet DS = Budget.getTable(sql);
        if (DS != null)
        {
            //grdOffice.DataSource = DS;
            //grdOffice.DataBind();
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
            ddlCategory.Items.Insert(0,new ListItem(" Select ","0"));

            ddlSavingCat.DataSource = DS;
            ddlSavingCat.DataTextField = "CirName";
            ddlSavingCat.DataValueField = "CID";
            ddlSavingCat.DataBind();
            ddlSavingCat.Items.Insert(0, new ListItem(" Select ", "0"));
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

    //protected void chkClosure_OnCheckedChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        CheckBox chkClosure = (CheckBox)sender;
    //        if (chkClosure.Checked)
    //        {
    //            HiddenField hfTIDGrd = (HiddenField)chkClosure.Parent.FindControl("hfTIDGrd");
    //            string sql = "update dbo.tbl_Topic set ClosedBy=" + Common.CastAsInt32(Session["loginid"].ToString()) + ", closedOn='" + System.DateTime.Now.ToString() + "',Status=2 where TID=" + hfTIDGrd.Value + "  select 1";
    //            DataSet DS = Budget.getTable(sql);
    //            if (DS != null)
    //            {
    //                lblMsg.Text = "Record updated successfully.";
    //                BindGrid();
    //                btnSearch_OnClick(sender, e);
    //            }
    //            else
    //            {
    //                lblMsg.Text = "Record could not be updated.";
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = "Record could not be updated.";
    //    }
    //}
     
}
