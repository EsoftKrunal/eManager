using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class emtm_StaffAdmin_Emtm_Documents : System.Web.UI.Page
{
    DateTime ToDay;
    //User Defined Properties
    public int SelectedId
    {
        get
        {
            return Common.CastAsInt32(ViewState["SelectedId"]);
        }
        set
        {
            ViewState["SelectedId"] = value;
        }
    }
    public int EmpId
    {
        get
        {
            return Common.CastAsInt32(ViewState["_EmpId"]);
        }
        set
        {
            ViewState["_EmpId"] = value;
        }
    }
    public int LoginOfficeID
    {
        get
        {
            return Common.CastAsInt32(ViewState["_LoginOfficeID"]);
        }
        set
        {
            ViewState["_LoginOfficeID"] = value;
        }
    }

    //--Page Load Events---------------------
    public AuthenticationManager auth;
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        int UserID = Common.CastAsInt32(Session["loginid"]);
        auth = new AuthenticationManager(336, UserID, ObjectType.Page);
        if (!(auth.IsView))
        {
            Response.Redirect("NoPermission.aspx");
        }

        if (Page.Request.QueryString["EmpID"] != null)
        {
            EmpId = Common.CastAsInt32(Page.Request.QueryString["EmpID"].ToString());
            GetLoginOfficeID();
            if (LoginOfficeID != 3)
            {
                string strSQL = " select 1 from DBO.Hr_PersonalDetails where EmpID = "+ EmpId + " and Office = "+ LoginOfficeID + " ";
                DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(strSQL);
                if(dt.Rows.Count==0)
                    Response.Redirect("NoPermission.aspx");
            }
            
        }
        else
        {
            return;
        }
        
        Session["CurrentPage"] = 1;
        ToDay = DateTime.Today;
        if (!IsPostBack)
        {
            ShowHeaderData();
            BindGrid();
        }
        
    }
    
    public string FormatNumber(object Data)
    {
        return Math.Round(Convert.ToDecimal(Data), 1).ToString("##0.0"); 
    }
    # region --- User Defined Functions ---
    protected void ShowHeaderData()
    {
        string sql = " select EmpCode,FirstName+' '+MiddleName+' '+FamilyName as UserName,p.PositionName  from DBO.Hr_PersonalDetails  PD "+
                    " left join DBOposition p on p.PositionId = PD.Position " +
                    " where EmpID = "+EmpId+" ";
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        if (dt.Rows.Count > 0)
        {
            lblEmpCode.Text = dt.Rows[0]["EmpCode"].ToString();
            lblEmpName.Text = dt.Rows[0]["UserName"].ToString();
            lblEmpPosition.Text = dt.Rows[0]["PositionName"].ToString();
        }
        else
        {
            lblEmpCode.Text = "";
            lblEmpName.Text = "";
            lblEmpPosition.Text = "";
        }
    }
    protected void BindGrid()
    {
        string WhereClause = " where D.EmpId="+EmpId;
        string sql= " select D.OtherDocId,D.EmpId,PD.FirstName+' '+PD.FamilyName as EmpName,PD.EmpCode,P.PositionName, "+
                    "     D.DocumentName ,D.DocumentNo,D.PlaceOfIssue,replace(convert(varchar, D.IssueDate, 106), ' ', '-') as IssueDate, " +
                    "    replace(convert(varchar, D.ExpiryDate, 106), ' ', '-') as ExpiryDate,[FileName],FileImage " +
                    "    from Emtm_Hr_OtherDocsDetails D " +
                    "    inner join Hr_PersonalDetails PD on PD.EmpId=D.EmpId " +
                    "    Left Join Position P on p.PositionId= PD.Position ";
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql+ WhereClause);

        RptLeaveSearch.DataSource = dt;
        RptLeaveSearch.DataBind();
        EmpCount.Text =  RptLeaveSearch.Items.Count.ToString()+ " records found ";

        
    }
    #endregion

    #region --- Control Events ---
    

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        SelectedId = 0;
        dvAddDocuments.Visible = true;
        btnsave.Visible = true;
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        dvAddDocuments.Visible = false;
        ClearControls();
        BindGrid();
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        DateTime date_Issue = System.DateTime.Today, date_Expiry;

        if (txtIssuedate.Text.Trim() != "")
        {
            if (!(DateTime.TryParse(txtIssuedate.Text, out date_Issue)))
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "error", "alert('Issue Date is Incorrect.');", true);
                return;
            }
        }

        if (txtExpirydate.Text != "")
        {

            if (!(DateTime.TryParse(txtExpirydate.Text, out date_Expiry)))
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "error", "alert('Expiry Date is Incorrect.');", true);
                return;
            }

            if (date_Issue > date_Expiry && txtIssuedate.Text.Trim() != "")
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Issue Date should Not be greater than Expiry Date.');", true);
                return;
            }

        }
        object d1 = (txtIssuedate.Text.Trim() == "") ? DBNull.Value : (object)txtIssuedate.Text;
        object d2 = (txtExpirydate.Text.Trim() == "") ? DBNull.Value : (object)txtExpirydate.Text;

        FileUpload img = (FileUpload)fldocument;
        Byte[] imgByte = new Byte[0];
        string FileName = "";
        if (img.HasFile && img.PostedFile != null)
        {
            HttpPostedFile File = fldocument.PostedFile;
            FileName = fldocument.FileName.Trim();
            imgByte = new Byte[File.ContentLength];
            File.InputStream.Read(imgByte, 0, File.ContentLength);
        }

        Common.Set_Procedures("HR_InsertUpdateOtherDocsDetails");
        Common.Set_ParameterLength(9);
        Common.Set_Parameters(new MyParameter("@OtherDocId", SelectedId),
            new MyParameter("@EmpId", EmpId),
            new MyParameter("@DocumentName", txtOtherDocName.Text.Trim()),
            new MyParameter("@DocumentNo", txtDocumentNo.Text.Trim()),
            new MyParameter("@PlaceOfIssue", ""),
            new MyParameter("@IssueDate", d1),
            new MyParameter("@ExpiryDate", d2),
            new MyParameter("@FileName", FileName),
            new MyParameter("@FileImage", imgByte));
        DataSet ds = new DataSet();
        if (Common.Execute_Procedures_IUD_CMS(ds))
        {
            SelectedId = Common.CastAsInt32(ds.Tables[0].Rows[0][0]);
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Record saved successfully.');", true);
            ClearControls();
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Unable to save record.');", true);
        }
    }
    #endregion

    //- 21 Jun 2016------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    protected void btndocedit_Click(object sender, ImageClickEventArgs e)
    {
        SelectedId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        BindGrid();
        ShowRecord(SelectedId);
        dvAddDocuments.Visible = true;
        btnsave.Visible = true;
    }
    protected void btndocDelete_Click(object sender, ImageClickEventArgs e)
    {
        int ID = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        if (EmpId > 0)
        {
            try
            {
                DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("delete from HR_OtherDocsDetails where OtherDocId=" + ID);
                BindGrid();                
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "error", "alert('Record Deleted Successfully');", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "error", "alert('Record Not Deleted');", true);
                return;
            }
        }
    }

    public void ShowRecord(int Id)
    {
        if (EmpId > 0)
        {
            DataTable dtdata = Common.Execute_Procedures_Select_ByQueryCMS("select * from HR_OtherDocsDetails WHERE OtherDocId=" + Id.ToString());
            if (dtdata != null)
                if (dtdata.Rows.Count > 0)
                {
                    DataRow dr = dtdata.Rows[0];
                    txtOtherDocName.Text = dr["DocumentName"].ToString().Trim();
                    txtDocumentNo.Text = dr["DocumentNo"].ToString().Trim();
                    if (dr["IssueDate"].ToString() != "")
                    {
                        txtIssuedate.Text = Convert.ToDateTime(dr["IssueDate"]).ToString("dd-MMM-yyyy").Trim();
                    }
                    else
                    {
                        txtIssuedate.Text = "";
                    }
                    if (dr["ExpiryDate"].ToString() != String.Empty)
                    {
                        txtExpirydate.Text = Convert.ToDateTime(dr["ExpiryDate"]).ToString("dd-MMM-yyyy").Trim();
                    }
                    else
                    {
                        txtExpirydate.Text = "";
                    }
                    //txtPlaceOfIssue.Text = dr["PlaceOfIssue"].ToString().Trim();
                }
        }
    }
    protected void ClearControls()
    {
        txtOtherDocName.Text = "";
        txtDocumentNo.Text = "";
        //txtPlaceOfIssue.Text = "";  
        txtIssuedate.Text = "";
        txtExpirydate.Text = "";
    }
    public void GetLoginOfficeID()
    {
        string strSQL = "select Office from Hr_PersonalDetails where EmpId=" + Session["ProfileId"].ToString();
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(strSQL);

        if (dt.Rows.Count > 0)
        {
            LoginOfficeID = Common.CastAsInt32(dt.Rows[0]["Office"].ToString());
            
        }
    }
}
