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



public partial class Circular_CircularForm : System.Web.UI.Page
{
    public int CID
    {
        set { ViewState["CID"] = value; }
        get { return int.Parse("0" + ViewState["CID"]); }
    }
    public int Source
    {
        set { ViewState["Source"] = value; }
        get { return int.Parse("0" + ViewState["Source"]); }
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
        lblApprovalMsg.Text = "";

        if (!Page.IsPostBack)
        {
            if (Page.Request.QueryString["CID"] != null)
                CID = Common.CastAsInt32(Page.Request.QueryString["CID"]);
            //else
                //return;

            txtCirDate.Text = System.DateTime.Now.ToString("dd-MMM-yyyy");
            BindApprovingPerson();
            BindCategory();
            if (Page.Request.QueryString["CreateCIR"] != null)
            {
                if (CID == 0)
                    SetFormDesignAndData();
                else
                {
                    SetDataFromCreateCircularTable();
                   
                }
            }
            else
            {
                SetData();
            }
            
        }
        //BindApprovingPerson();
    }


    protected void btnSave_OnClick(object sender, EventArgs e)
    {
        if (txtCirDate.Text.Trim() == "")
        {
            lblMsg.Text = "Please enter approval date.";
            txtCirDate.Focus();
            return;
        }
        if (ddlCategory.SelectedIndex == 0)
        {
            lblMsg.Text = "Please select category.";
            ddlCategory.Focus();
            return;
        }
        if (txtCirCularTopic.Text.Trim() == "")
        {
            lblMsg.Text = "Please enter circular topic.";
            txtCirCularTopic.Focus();
            return;
        }
        if (txtCircularDetails.Text.Trim() == "")
        {
            lblMsg.Text = "Please enter circular details.";
            txtCircularDetails.Focus();return;
        }
        if (txtNextReviewDate.Text.Trim() == "")
        {
            lblMsg.Text = "Please enter next review date.";
            txtNextReviewDate.Focus();
            return;
        }

        if (Session["loginid"] != null)
        {
            string FileName = "";
            if (chk_FileExtension(fuAddFile.FileName.ToLower()) == true)
            {
                FileName = fuAddFile.FileName;
            }
            else
            {
                lblMsg.Text = "Only PDF file will be acceppted.";
                return;
            }
            
            string sql ="";
            if(CID==0)
                sql = "sp_InsertIntoCreateCircular '" + lblSource.Text.Trim().Replace("'", "''") + "','" + txtCirDate.Text.Trim() + "'," + ddlCategory.SelectedValue + ",'" + txtCirCularTopic.Text.Trim().Replace("'", "''") + "','" + txtCircularDetails.Text.Trim().Replace("'", "''") + "','" + txtNextReviewDate.Text.Trim() + "','" + FileName + "'," + Common.CastAsInt32(Session["loginid"].ToString()) + "," + ddlSubAppTo.SelectedValue + ",'','','" + ddlType.SelectedValue + "'";
            else
                sql = "sp_UpdateCreateCircular " + CID + ",'" + lblSource.Text.Trim().Replace("'", "''") + "','" + txtCirDate.Text.Trim() + "'," + ddlCategory.SelectedValue + ",'" + txtCirCularTopic.Text.Trim().Replace("'", "''") + "','" + txtCircularDetails.Text.Trim().Replace("'", "''") + "','" + txtNextReviewDate.Text.Trim() + "','" + FileName + "','','" + ddlType.SelectedValue + "'";

            DataSet Ds = Budget.getTable(sql);
            if (Ds != null)
            {

                if (CID == 0)
                {
                    lblMsg.Text = "Record Saved Successfully.";
                    trApprovalSection.Visible = true;
                    txtCircularDetails.Height = 310;
                    //Get max id n upload file
                    string maxID = "", sqlID = "";
                    sqlID = "select max(CID) from CreateCircular";
                    DataSet DsID = Budget.getTable(sqlID);
                    if (DsID != null)
                    {
                        if (DsID.Tables[0].Rows.Count > 0)
                        {
                            maxID = DsID.Tables[0].Rows[0][0].ToString();
                            CID = Common.CastAsInt32(maxID);


                            if (fuAddFile.HasFile)
                            {
                                FileName = FileName.Insert((FileName.LastIndexOf('.')), "_CIR" + maxID);

                                string UpDateSql = "update CreateCircular set CFileName  ='" + FileName + "' where CID=" + maxID + " select 11";
                                DataSet DsIDUpdate = Budget.getTable(UpDateSql);

                                fuAddFile.SaveAs(Server.MapPath("~/EMANAGERBLOB/LPSQE/Circular/" + FileName));

                            }
                        }
                    }
                }
                else
                {
                    lblMsg.Text = "Record Updated Successfully.";
                    if (fuAddFile.HasFile)
                    {
                        FileName = FileName.Insert((FileName.LastIndexOf('.')), "_CIR" + CID);

                        string UpDateSql = "update CreateCircular set CFileName  ='" + FileName + "' where CID=" + CID + " select 11";
                        DataSet DsIDUpdate = Budget.getTable(UpDateSql);

                        fuAddFile.SaveAs(Server.MapPath("~/EMANAGERBLOB/LPSQE/Circular/" + FileName));

                    }
                }
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "ddd", "RefereshParentPage();", true);
            }
        }
        else
        {
            lblMsg.Text = "Record Not Saved.";
        }
    }
    protected void btnCancel_OnClick(object sender, EventArgs e)
    {
        ddlCategory.SelectedIndex = 0;
        txtCircularDetails.Text = "";
        txtCirCularTopic.Text = "";
        txtNextReviewDate.Text = "";
        ScriptManager.RegisterStartupScript(Page,this.GetType(),"aa","CloseThisWindow();",true);

    }

    protected void btnApprovalSave_OnClick(object sender, EventArgs e)
    {
        if (ddlSubAppTo.SelectedIndex == 0)
        {
            lblApprovalMsg.Text = "Please select the person to approve.";
            lblApprovalMsg.Focus();
            return;
        }
        

        string sql = "sp_UpdateCreateCircularApproval " + CID + "," + ddlSubAppTo .SelectedValue+ ",'"+System.DateTime.Now.ToString("dd-MMM-yyyy") +"'";
        DataSet Ds = Budget.getTable(sql);
        if (Ds != null)
        {
            lblApprovalMsg.Text = "Record updated successfully.";
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "ddd", "RefereshParentPage();", true);
        }
        else
        {
            lblApprovalMsg.Text = "Record not be updated.";
        }
    }


    protected void sdfdfdfdf(object sender, EventArgs e)
    {
        //ExportToPDF();
    }
    
    // Function ----------------------------------------------------------------------------------------------------------------------------------------------------------------
    public void SetData()
    {
        string sql = "select "+
                  " ID "+
                  " ,CircularCat "+
                  " ,Circular "+
                  " ,CFileName ,CreatedBy" +
                  " ,(select  UserID from  dbo.userlogin U where U.Loginid=C.CreatedBy )CreatedByName "+
                  " ,replace(convert(varchar,C.CreatedOn ,106),' ','-') CreatedOnText "+
                  " ,AppOrRejectedBy "+
                  " ,AppOrRejectedDate "+
                  " ,AppOrRejectedComments "+
                  " ,Status " +
                  
                " from Circular C where id=" + CID + "";
        DataSet Ds = Budget.getTable(sql);
        if (Ds != null)
        {
            if (Ds.Tables[0].Rows.Count > 0)
            { 
                DataRow Dr=Ds.Tables[0].Rows[0];
                txtCirDate.Enabled = false;
                ImageButton1.Visible = false;
                ddlCategory.SelectedValue = Dr["CircularCat"].ToString();
                lblSource.Text = Dr["CreatedByName"].ToString().Replace("''", "'");
                Source = Common.CastAsInt32( Dr["CreatedBy"]);
                //txtCirCularTopic.Text = Dr["Circular"].ToString();
                txtCircularDetails.Text = Dr["Circular"].ToString();
                
            }
        }

    }
    public void SetDataFromCreateCircularTable()
    {
        string sql = "";
        sql = " select CID,CircularDate,CType,Category,Topic,Details,CFileName,SuperSedes,AppRejComments,SubmittedForApproval as SubmittedForApprovalID" +
            " ,(select CirName from dbo.CircularCategory CC where CC.CID=C.Category)CircularCatName" +
            " ,( case when len( convert(varchar(7000),C.Details))>95 then substring(C.Details,1,90)+'............'else  C.Details end ) as ShortDetails" +
            " ,(case when C.CFileName='' then 'none' else 'block' end)ClipVisibility " +
            " ,replace(convert(varchar,C.CircularDate,106),' ','-') CircularDateText " +
            " ,Source " +
            //" ,(select  (FirstName+' '+MiddleName+' '+FamilyName) from  dbo.Hr_PersonalDetails U where U.Empid=C.Source)SourceName" +
            " ,CreatedBy " +
            " ,(select  U.FirstName+' '+U.Lastname  from  dbo.userlogin U where U.Loginid=C.CreatedBy )CreatedByName " +
            " ,CreatedOn " +
            " ,replace(convert(varchar,CreatedOn,106),' ','-') CreatedOnText " +

            " ,replace(convert(varchar,C.NextReviewDate,106),' ','-') NextReviewDateText " +

            ",(select (FirstName+' '+MiddleName+''+FamilyName)EmpName  from dbo.Hr_PersonalDetails HR where HR.EmpID=c.SubmittedForApproval)SubmittedForApproval" +
            " ,replace(convert(varchar,SubmittedForApprovalOn,106),' ','-') SubmittedForApprovalOn " +



            ",(select  U.FirstName+' '+U.Lastname  from  dbo.userlogin U where U.Loginid=c.ApprejBy)ApprejBy" +
            " ,replace(convert(varchar,ApprejOn,106),' ','-') ApprejOn " +



            " ,Status, 'True' as Visibility " +
            " ,(case when C.Status=1 then 'Avaiting for Approve' when Status='2' then 'Approved' when Status='3' then 'Rejected' end) StatusText " +
            " from CreateCircular C where C.CID=" + CID + "";

        DataSet DS = Budget.getTable(sql);
        if (DS != null)
        {
            if (DS.Tables[0].Rows.Count > 0)
            {
                DataRow DR = DS.Tables[0].Rows[0];
                txtCirDate.Text = DR["CircularDateText"].ToString();
                ddlCategory.SelectedValue = DR["Category"].ToString();
                lblSource.Text = DR["Source"].ToString().Replace("''", "'");
                txtCirCularTopic.Text = DR["Topic"].ToString().Replace("''", "'");
                txtCircularDetails.Text = DR["Details"].ToString().Replace("''", "'");
                txtNextReviewDate.Text = DR["NextReviewDateText"].ToString();
                //txtSuperSedes.Text = DR["SuperSedes"].ToString();
                if (DR["CType"].ToString() != "")
                    ddlType.SelectedValue = DR["CType"].ToString();


                if (DR["Status"].ToString() != "1")
                {
                    aFile.Visible = true;
                    aFile.HRef = "../EMANAGERBLOB/LPSQE/Circular/CircularFile_" + CID+".pdf";
                }
                else if (DR["CFileName"].ToString()!="")
                {
                    aFile.Visible = true;
                    aFile.HRef = "../EMANAGERBLOB/LPSQE/Circular/" + DR["CFileName"].ToString();
                }
                else
                {
                    aFile.Visible = false;
                    aFile.HRef = "";
                }

                if (DR["Status"].ToString() != "1")
                {
                    if (DR["Status"].ToString() == "2")
                    {
                        lblAppRejByText.Text = "Approved By :";
                        lblAppRejOnText.Text = "Approved On :";
                        lblApprovalCommentsText.Text= "Approval Comments :";

                    }
                    else
                    {
                        lblAppRejByText.Text = "Rejected By :";
                        lblAppRejOnText.Text = "Rejected On :";
                        lblApprovalCommentsText.Text = "Rejection Comments :";
                    }
                    trApprovalComments.Visible = true;

                    lblRequestedBy.Text = DR["CreatedByName"].ToString();
                    lblRequestedOn.Text = DR["CreatedOnText"].ToString();


                    lblSubmitedForApp.Text = DR["SubmittedForApproval"].ToString();
                    lblSubmitedAppOn.Text = DR["SubmittedForApprovalOn"].ToString();
                    lblApprovedBy.Text = DR["ApprejBy"].ToString();
                    lblApprovedOn.Text = DR["ApprejOn"].ToString();
                    lblApprovalComments.Text = DR["AppRejComments"].ToString();

                    txtCirDate.Enabled = false;
                    ImageButton1.Visible = false;
                    trApprovalSection.Visible = false;
                    trViewApprovedComm.Visible = true;
                    btnCancel.Visible = false;
                    btnSave.Visible = false;
                    txtCircularDetails.Height = 210;
                }
                else
                {
                    if (DR["SubmittedForApprovalID"].ToString() == "0")
                    {
                        txtCirDate.Enabled = true;
                        ImageButton1.Visible = true;
                        trApprovalSection.Visible = true;
                        txtCircularDetails.Height = 310;
                    }
                    else
                    {
                        lblAppRejByText.Text = "";
                        lblAppRejOnText.Text = "";

                        lblRequestedBy.Text = DR["CreatedByName"].ToString();
                        lblRequestedOn.Text = DR["CreatedOnText"].ToString();

                        lblSubmitedForApp.Text = DR["SubmittedForApproval"].ToString();
                        lblSubmitedAppOn.Text = DR["SubmittedForApprovalOn"].ToString();


                        txtCirDate.Enabled = false;
                        ImageButton1.Visible = false;
                        trApprovalSection.Visible = false;
                        btnCancel.Visible = false;
                        trViewApprovedComm.Visible = true;
                        btnSave.Visible = false;
                        txtCircularDetails.Height = 310;
                    }
                }
                //trApprovalSection.Visible = true;
                //txtCircularDetails.Height = 320;
            }
        }
    }
    public void SetFormDesignAndData()
    {
        //lblSource.Text = Session["UserName"].ToString();
        Source = Common.CastAsInt32(Session["loginid"].ToString());
        txtCirDate.Enabled = true;
        ImageButton1.Visible = true;
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
            ddlCategory.Items.Insert(0, new System.Web.UI.WebControls.ListItem(" Select ", "0"));            
        }
    }
    public void BindApprovingPerson()
    {
        string sql = "select EmpID,(FirstName+' '+MiddleName+' '+FamilyName)EmpName from dbo.Hr_PersonalDetails where Status='p' "+
        " and UserID in (select UserID from dbo.UserPageRelation WHERE PageID=273 and IsVerify2=1 union select LoginID from dbo.UserMaster where roleid in (select RoleID from dbo.RolePageRelation where PageID=273 and IsVerify2=1)) " +
        " order by FirstName asc ";
        DataSet DS = Budget.getTable(sql);
        if (DS != null)
        {
            ddlSubAppTo.DataSource = DS;
            ddlSubAppTo.DataTextField = "EmpName";
            ddlSubAppTo.DataValueField = "EmpID";
            ddlSubAppTo.DataBind();
            ddlSubAppTo.Items.Insert(0, new System.Web.UI.WebControls.ListItem(" Select ", "0"));
        }
    }
    public bool chk_FileExtension(string str)
    {
        string extension = "";
        if (str != "")
            extension = str.Substring(str.Length - 4, 4);
        else
            return true;
        switch (extension)
        {
            case ".pdf":
                return true;
            default:
                return false;
                break;
        }
    }

    
}

