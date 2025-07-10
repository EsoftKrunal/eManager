using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.IO;

public partial class emtm_Contracts_Emtm_Contracts : System.Web.UI.Page
{
    public AuthenticationManager auth;
    
    public int ContractID
    {
        get
        {
            return Common.CastAsInt32(ViewState["_ContractID"]);
        }
        set
        {
            ViewState["_ContractID"] = value;
        }
    }
 
    
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 245);
        if (chpageauth <= 0)
        {
            Response.Redirect("../AuthorityError.aspx");
            return;
        }
        auth = new AuthenticationManager(342, Common.CastAsInt32(Session["loginid"]), ObjectType.Page);
        if (!auth.IsView)
        {
            Response.Redirect("../AuthorityError.aspx");
            return;
        }
        //-----------------------------
        lblMsgAddCategory.Text = "";
        lblMsgAddContract.Text = "";
        lblMsgDocuments.Text = "";
        if (!Page.IsPostBack)
        {
            btnAddContractPopup.Visible = auth.IsAdd;
            BindContractCategory();
            BindSupplier();
            ShowContractList();
        }
    }
    protected void btn_Search_Click(object sender, EventArgs e)
    {
        ShowContractList();
    }
    protected void btn_Clear_Click(object sender, EventArgs e)
    {
        ddlContractCategory.SelectedIndex = 0;
        ddlSupplierFilter.SelectedIndex = 0;
        txtTopicFilter.Text = "";
        chkOffice_s.Checked=false;
        chkShip_s.Checked = false;
        txtDateFrom.Text = "";
        txtDateTo.Text = "";
        txtExpiredInDays.Text = "";

        ShowContractList();
    }
    
    public void ShowContractList()
    {
        string whereclause = "";
        string whereclause_inner = "";
        if (chkOffice_s.Checked)
        {
            whereclause_inner = whereclause_inner + "or Location='O' ";
        }
        if (chkShip_s.Checked)
        {
            whereclause_inner = whereclause_inner + "or Location='S' ";
        }
        if (whereclause_inner != "")
        {
            whereclause_inner = whereclause_inner.Substring(2);
            whereclause_inner = " and (" + whereclause_inner + " )";
        }

        string sql = " WITH cte AS " +
                     "   ( " +
                     "   select C.*, s.SupplierName " +
                     "   , (select COUNT(1) from HR_ContractsLocation where ContractID = C.ContractID " + whereclause_inner + ")LocationExists,CM.ContractCatName " +
                     "           from HR_Contracts C " +
                     "   left join dbo.tblSMDSuppliers S on S.SupplierID = C.SupplierID " +
                     "   inner join HR_ContractCatMaster CM on C.ContractCatID=CM.ContractCatID " +
                     "   where Status = 'A' " +
                     "   ) " +
                     "   select *,row_number() over (order by SupplierName) as sno from cte " + ((whereclause_inner != "") ? " where LocationExists > 0 " : " where 1=1 ") + "  ";
        

        if (txtTopicFilter.Text.Trim() != "")
        {
            whereclause = whereclause + " and Topic like '%" + txtTopicFilter.Text.Trim() + "%'";
        }
        if (ddlContractCategory.SelectedIndex != 0)
        {
            whereclause = whereclause + " and ContractCatID=" +ddlContractCategory.SelectedValue;
        }
        if (ddlSupplierFilter.SelectedIndex != 0)
        {
            whereclause = whereclause + " and SupplierID=" + ddlSupplierFilter.SelectedValue;
        }
        if (txtDateFrom.Text.Trim() != "")
        {
            whereclause = whereclause + " and StartDate>='"+txtDateFrom.Text+"' ";
        }
        if (txtDateTo.Text.Trim() != "")
        {
            whereclause = whereclause + " and StartDate<='" + txtDateTo.Text + "' ";
        }
        if (txtExpiredInDays.Text.Trim() != "")
        {
            whereclause = whereclause + " and EndDate<='" + DateTime.Today.AddDays(Common.CastAsInt32(txtExpiredInDays.Text)).ToString("dd-MMM-yyyy") + "' ";
        }
        sql = sql + whereclause;
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql + " order by SupplierName");

        rptContractList.DataSource = dt;
        rptContractList.DataBind();
    }
    public void BindSupplier()
    {
        string sql = " select SupplierID,SupplierName from dbo.tblSMDSuppliers where SupplierName is not null order by SupplierName ";
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        ddlSupplier.DataSource = dt;
        ddlSupplier.DataTextField = "SupplierName";
        ddlSupplier.DataValueField = "SupplierID";
        ddlSupplier.DataBind();
        ddlSupplier.Items.Insert(0, new ListItem("Select", ""));

        ddlSupplierFilter.DataSource = dt;
        ddlSupplierFilter.DataTextField = "SupplierName";
        ddlSupplierFilter.DataValueField = "SupplierID";
        ddlSupplierFilter.DataBind();
        ddlSupplierFilter.Items.Insert(0, new ListItem("All", ""));



    }
    public void BindContractCategory()
    {
        string sql = " select ContractCatID,ContractCatName from HR_ContractCatMaster order by ContractCatName";
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        ddlContractCategory.DataSource = dt;
        ddlContractCategory.DataTextField = "ContractCatName";
        ddlContractCategory.DataValueField = "ContractCatID";
        ddlContractCategory.DataBind();
        ddlContractCategory.Items.Insert(0, new ListItem("All", ""));

        ddlCategoryEntry.DataSource = dt;
        ddlCategoryEntry.DataTextField = "ContractCatName";
        ddlCategoryEntry.DataValueField = "ContractCatID";
        ddlCategoryEntry.DataBind();
        ddlCategoryEntry.Items.Insert(0, new ListItem("Select", ""));

        
    }

    // Add Contract---------------------------------------------------
    protected void btnAddContractPopup_Click(object sender, EventArgs e)
    {
        BindOffice();
        BindShip();
        BindApprovedByUser();
        ContractID = 0;
        dvAddContract.Visible = true;
    }
    protected void btnCloseContractPopup_Click(object sender, EventArgs e)
    {
        dvAddContract.Visible = false;
        ClearControls();
    }
    protected void btnSvaeContract_Click(object sender, EventArgs e)
    {
        
        
        if (txtTopic.Text.Trim() == "")
        {
            lblMsgAddContract.Text = "Please enter topic";
            txtTopic.Focus();
            return;
        }
        if (ddlCategoryEntry.SelectedIndex==0)
        {
            lblMsgAddContract.Text = "Please select catogry";
            ddlCategoryEntry.Focus();
            return;
        }
        if (txtStartDate.Text.Trim() == "")
        {
            lblMsgAddContract.Text = "Please enter start date";
            txtStartDate.Focus();
            return;
        }
        if (txtEndDate.Text.Trim() == "")
        {
            lblMsgAddContract.Text = "Please enter end date";
            txtEndDate.Focus();
            return;
        }
        if (txtRenewal.Text.Trim() == "")
        {
            lblMsgAddContract.Text = "Please enter renewal date";
            txtRenewal.Focus();
            return;
        }
        if (ddlSupplier.SelectedIndex == 0)
        {
            lblMsgAddContract.Text = "Please select supplier";
            ddlSupplier.Focus();
            return;
        }
       
        if (txtProposedBy.Text.Trim() == "")
        {
            lblMsgAddContract.Text = "Please enter Proposed By";
            txtProposedBy.Focus();
            return;
        }
        if (ddlApprovedBy.SelectedIndex == 0)
        {
            lblMsgAddContract.Text = "Please select approved by";
            ddlApprovedBy.Focus();
            return;
        }

        string Office; string Ship; string Other;
        GetOffice_Ship_Values(out Office, out Ship, out Other);
        if (Office == "" && Ship == "" && Other == "")
        {
            lblMsgAddContract.Text = "Please selcet any certificate applied on";            
            return;
        }

        Common.Set_Procedures("HR_InsertUpdate_Contracts");
        Common.Set_ParameterLength(15);
        Common.Set_Parameters(
            new MyParameter("@ContractID", ContractID),
            new MyParameter("@ContractCatID", ddlCategoryEntry.SelectedValue),
            new MyParameter("@RefeNo", ""),
            new MyParameter("@Topic", txtTopic.Text.Trim()),
            new MyParameter("@StartDate", txtStartDate.Text.Trim()),
            new MyParameter("@EndDate", txtEndDate.Text.Trim()),
            new MyParameter("@SupplierID", ddlSupplier.SelectedValue),

            new MyParameter("@Location_Office", Office),
            new MyParameter("@Location_Ship", Ship),
            new MyParameter("@Location_Other", Other),

            new MyParameter("@CreatedBy", Session["UserName"].ToString()),
            new MyParameter("@ModifiedBy", Session["UserName"].ToString()),

            new MyParameter("@RenewalDate", txtRenewal.Text),
            new MyParameter("@ProposedBy", txtProposedBy.Text),
            new MyParameter("@ApprovedBy", ddlApprovedBy.SelectedValue)


            );

        DataSet ds = new DataSet();
        try
        {
            if (Common.Execute_Procedures_IUD_CMS(ds))
            {
                //ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Record saved successfully.');", true);                
                lblMsgAddContract.Text = "Record saved successfully.";
                ShowContractList();
                ClearControls();
            }
            else
            {
                //ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Error while saving record.');", true);
                lblMsgAddContract.Text = "Error while saving record."; 
            }
        }
        catch
        {
            //ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Error while saving record.');", true);
            lblMsgAddContract.Text = "Error while saving record.";
        }
    }
    protected void btnEditContract_OnClick(object sender, EventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        ContractID = Common.CastAsInt32(btn.CommandArgument);
        

        BindOffice();
        BindShip();
        ShowContractInformation();
        dvAddContract.Visible = true;
    }
    protected void btnDeleteContract_OnClick(object sender, EventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        ContractID = Common.CastAsInt32(btn.CommandArgument);
        string sql = " update HR_Contracts set Status='D',ModifiedBy='" + Session["UserName"].ToString() + "',ModifiedOn=getdate() where ContractID=" + ContractID;
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        ShowContractList();
    }
    

    protected void chkOffice_OnCheckedChanged(object sender, EventArgs e)
    {
        if (chkOffice.Checked)
        {
            chklistOffice.ClearSelection();
            divchklistOffice.Visible = true;
        }
        else
            divchklistOffice.Visible = false;
    }
    protected void chkShip_OnCheckedChanged(object sender, EventArgs e)
    {
        if (chkShip.Checked)
        {
            chklistShip.ClearSelection();
            divchklistShip.Visible = true;
        }
        else
            divchklistShip.Visible = false;
    }
    
    public void ShowContractInformation()
    {
        string sql = " select * from HR_Contracts  where ContractID=" + ContractID;
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);

        sql = " select Location, LocationID, LocationName from HR_ContractsLocation Where ContractID =" + ContractID;
        DataTable dtLocaiont = Common.Execute_Procedures_Select_ByQueryCMS(sql);

        if (dt.Rows.Count > 0)
        {
            DataRow dr = dt.Rows[0];
            txtTopic.Text = dr["Topic"].ToString();
            ddlCategoryEntry.SelectedValue = dr["ContractCatID"].ToString();
            txtStartDate.Text = Common.ToDateString(dr["StartDate"].ToString());
            txtEndDate.Text = Common.ToDateString(dr["EndDate"].ToString());
            txtRenewal.Text = Common.ToDateString(dr["RenewalDate"].ToString());
            ddlSupplier.SelectedValue = dr["SupplierID"].ToString();

            txtProposedBy.Text = dr["ProposedBy"].ToString();
            ddlApprovedBy.SelectedValue = dr["ApprovedBy"].ToString();
        }

        DataView DvLocation = dtLocaiont.DefaultView;
        DvLocation.RowFilter = "Location='O'";
        DataTable TmpDb = DvLocation.ToTable();
        if (TmpDb.Rows.Count > 0)
        {
            chkOffice.Checked = true;
            divchklistOffice.Visible = true;
            foreach (DataRow dr in TmpDb.Rows)
            {
                int id = Common.CastAsInt32(dr["LocationID"]);
                foreach (ListItem li in chklistOffice.Items)
                {
                    if (li.Value == id.ToString())
                    {
                        li.Selected = true;
                    }
                }
            }
        }


        DvLocation.RowFilter = "Location='S'";
        TmpDb = DvLocation.ToTable();
        if (TmpDb.Rows.Count > 0)
        {
            chkShip.Checked = true;
            divchklistShip.Visible = true;
            foreach (DataRow dr in TmpDb.Rows)
            {
                int id = Common.CastAsInt32(dr["LocationID"]);
                foreach (ListItem li in chklistShip.Items)
                {
                    if (li.Value == id.ToString())
                    {
                        li.Selected = true;
                    }
                }
            }
        }

        
    }
    public void GetOffice_Ship_Values(out string _Office, out string _Ship, out string _Other)
    {
         _Office = "";
         _Ship = "";
         _Other = "";
        foreach (ListItem li in chklistOffice.Items)
        {
            if (li.Selected)
                _Office = _Office+ "," + li.Value;
        }
        if (_Office != "")
            _Office = _Office.Substring(1);

        foreach (ListItem li in chklistShip.Items)
        {
            if (li.Selected)
                _Ship = _Ship+"," + li.Value;
        }
        if (_Ship != "")
            _Ship = _Ship.Substring(1);

    }
    public void BindOffice()
    {
        string sql = " select OfficeId, OfficeName from Office where OfficeName is not null order by OfficeName ";
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        chklistOffice.DataSource = dt;
        chklistOffice.DataTextField = "OfficeName";
        chklistOffice.DataValueField = "OfficeId";
        chklistOffice.DataBind();

    }
    public void BindShip()
    {
        string sql = " select VesselId,VesselCode from vessel  order by VesselCode ";
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        chklistShip.DataSource = dt;
        chklistShip.DataTextField = "VesselCode";
        chklistShip.DataValueField = "VesselId";
        chklistShip.DataBind();
    }
    public void BindApprovedByUser()
    {
        string sql = " select EmpCode,FirstName+' '+FamilyName as UserName from Hr_PersonalDetails order by FirstName+' '+FamilyName ";
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        ddlApprovedBy.DataSource = dt;
        ddlApprovedBy.DataTextField = "UserName";
        ddlApprovedBy.DataValueField = "EmpCode";
        ddlApprovedBy.DataBind();
        ddlApprovedBy.Items.Insert(0, new ListItem("Select", ""));
    }
    
    public void ClearControls()
    {
        ContractID = 0;
        txtTopic.Text = "";
        txtStartDate.Text = "";
        txtEndDate.Text = "";
        ddlCategoryEntry.SelectedIndex = 0;
        ddlSupplier.SelectedIndex = 0;
        chkOffice.Checked = false;
        chkShip.Checked = false;
      
        chklistOffice.ClearSelection();
        chklistShip.ClearSelection();
      
        divchklistOffice.Visible = false;
        divchklistShip.Visible = false;
      
        txtRenewal.Text = "";
        txtProposedBy.Text = "";
        ddlApprovedBy.SelectedIndex = 0;

    }

    // Add Category---------------------------------------------------
    protected void ddlAddCategoryPopup_OnClick(object sender, EventArgs e)
    {
        txtCategory.Text = "";
        divAddCagegory.Visible = true;
    }
    protected void btnCloseCategoryPopup_Click(object sender, EventArgs e)
    {
        divAddCagegory.Visible = false;
    }
    protected void btnAddCategory_Click(object sender, EventArgs e)
    {
        if (txtCategory.Text.Trim() == "")
        {
            lblMsgAddCategory.Text = "Please enter category.";
            txtCategory.Focus();
            return;
        }
        string sql = " select 1 from DBO.HR_ContractCatMaster where ContractCatName='" + txtCategory.Text.Trim() + "' ";
        DataTable dtc = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        if (dtc.Rows.Count > 0)
        {
            lblMsgAddCategory.Text = "This category already exists.";
            txtCategory.Focus();
            return;

        }

        sql = " insert into HR_ContractCatMaster(ContractCatName) values('" + txtCategory.Text.Trim().Replace("'","`")+"') ";
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        lblMsgAddCategory.Text = "Category added successfully.";
        txtCategory.Text = "";
        BindContractCategory();

    }

    // Add Documents---------------------------------------------------

    protected void btnOpenDocumentPopup_OnClick(object sender, EventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        ContractID = Common.CastAsInt32(btn.CommandArgument);
        ShowDocumentsList();
        divDocuments.Visible = true;
    }
    protected void btnCloseDocumentsPopup_Click(object sender, EventArgs e)
    {
        divDocuments.Visible = false;
        txtDocumentName.Text = "";
    }
    protected void btnSaveDocuments_OnClick(object sender, EventArgs e)
    {
        if (txtDocumentName.Text.Trim() == "")
        {
            lblMsgDocuments.Text = "Please enter document name";
            txtDocumentName.Focus();
            return;
        }
        if (!fuDocuments.HasFile)
        {
            lblMsgDocuments.Text = "Please choose a pdf file";
            fuDocuments.Focus();
            return;
        }
        else
        {
            if (Path.GetExtension(fuDocuments.FileName).ToUpper() != ".PDF")
            {
                lblMsgDocuments.Text = "Please choose a pdf file";
                fuDocuments.Focus();
                return;
            }
        }

        byte[] Image = fuDocuments.FileBytes;
        string FileName = fuDocuments.FileName;


        Common.Set_Procedures("HR_InsertUPdate_ContractsDocuments");
        Common.Set_ParameterLength(4);
        Common.Set_Parameters(
            new MyParameter("@ContractID", ContractID),
            new MyParameter("@DocumentName", txtDocumentName.Text.Trim()),
            new MyParameter("@FileName", FileName),            
            new MyParameter("@Attachment", Image));

        DataSet ds = new DataSet();
        try
        {
            if (Common.Execute_Procedures_IUD_CMS(ds))
            {
                lblMsgDocuments.Text = "Record saved successfully.";
                txtDocumentName.Text = "";
                ShowDocumentsList();
            }
            else
            {
                lblMsgDocuments.Text = "Error while saving record.";
            }
        }
        catch
        {
            lblMsgDocuments.Text = "Error while saving record.";
        }
    }
    protected void btnDeleteDocument_OnClick(object sender, EventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        int Documentid = Common.CastAsInt32(btn.CommandArgument);
        string sql = " update HR_ContractsDocuments set Status='D' where documentID=" + Documentid;
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        ShowDocumentsList();
    }
    
    protected void lnkDownloadDocuments_OnClick(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        int DocumentID = Common.CastAsInt32(btn.CommandArgument);

        string sql = " select FileName,Attachment from  HR_ContractsDocuments where DocumentID=" + DocumentID;
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        if (dt.Rows.Count > 0)
        {
            string FileName = dt.Rows[0][0].ToString();
            byte[] ImageBytes=(byte[])dt.Rows[0][1];
            string Path = Server.MapPath("~\\emtm\\TempDocs\\");
         

            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", String.Format("attachment;filename={0}", FileName));
            Response.ContentType = "application/" + "pdf";
            Response.BinaryWrite(ImageBytes);
            Response.Flush();
            Response.End();

        }
    }
    

    public void ShowDocumentsList()
    {
        string sql = " select ROW_NUMBER()over(order by documentID)RowNo,* from  HR_ContractsDocuments where isnull(Status,'')='A' and  ContractID=" + ContractID ;
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        rptDocuments.DataSource = dt;
        rptDocuments.DataBind();
    }
    






}
