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

public partial class Register_InternalInspectionsQuestions : System.Web.UI.Page
{
    public int VersionId
    {
        set { ViewState["VersionId"] = value; }
        get { return Common.CastAsInt32(ViewState["VersionId"]); }
    }
    public int CatId
    {
        set { ViewState["CatId"] = value; }
        get { return Common.CastAsInt32(ViewState["CatId"]); }
    }
    public int QuestionId
    {
        set { ViewState["QuestionId"] = value; }
        get { return Common.CastAsInt32(ViewState["QuestionId"]); }
    }

    Authority Auth;
    string Mode = "New";
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        //--------------------------------- PAGE ACCESS AUTHORITY ----------------------------------------------------
        //int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 152);
        //if (chpageauth <= 0)
        //    Response.Redirect("blank.aspx");
        //------------------------------------------------------------------------------------------------------------
   
        if (Session["loginid"] == null)
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Default.aspx';", true);
        }
        if (Session["loginid"] != null)
        {
            //ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 12);
            //OBJ.Invoke();
            //Session["Authority"] = OBJ.Authority;
            //Auth = OBJ.Authority;
        }
        lblMessege.Text = "";
        if (!Page.IsPostBack)
        {

            BindInspections();
            BindVersions();
            BindCategory();            
            BindCheckList();
        }
    }
    //-------------------------------------
    protected void BindInspections()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("select * from dbo.m_inspection where inspectiongroup=3 order by Name");
        ddlInspection.DataSource = dt;
        ddlInspection.DataTextField = "Name";
        ddlInspection.DataValueField = "Id";
        ddlInspection.DataBind();
        ddlInspection.Items.Insert(0, new ListItem("< Select >", "0"));
    }
    protected void BindVersions()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("select * from dbo.tblInternalInspectionCheckListversion where inspectionid=" + ddlInspection.SelectedValue + " order by versionid desc");
        ddlVersion.DataSource = dt;
        ddlVersion.DataTextField = "VersionName";
        ddlVersion.DataValueField = "VersionId";
        ddlVersion.DataBind();
	ddlVersion.Items.Insert(0, new ListItem("< Select >", "0"));
    }
    protected void BindCategory()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("select CategoryId, isnull(CategoryCode,'')+' : '+isnull(CategoryName,'') as CategoryName  from dbo.tblInternalInspectionCheckListCategory where inspectionid=" + ddlInspection.SelectedValue + " order by categoryname");
        ddlCategory.DataSource = dt;
        ddlCategory.DataTextField = "categoryname";
        ddlCategory.DataValueField = "categoryId";
        ddlCategory.DataBind();
        ddlCategory.Items.Insert(0, new ListItem("< Select >", "0"));
    }    
    protected void BindCheckList()
    {
        string whereclause=" where InspectionId=" + ddlInspection.SelectedValue;
        if (ddlCategory.SelectedIndex > 0)
        {
            whereclause += " AND CategoryId=" + ddlCategory.SelectedValue;
        }
        if (ddlVersion.SelectedIndex > 0)
        {
            whereclause += " AND VersionId=" + ddlVersion.SelectedValue;
        }
        if (txtFQText.Text.Trim()!="")
        {
            whereclause += " AND QuestionName like '%" + txtFQText.Text + "%'";
        }
        
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("select row_number() over(order by InspectionName,CategoryName,QuestionNo) as SrNo,* from dbo.vwInternalInspectionCheckList " + whereclause + " order by InspectionName,CategoryName,QuestionNo");
        rptQuestions.DataSource = dt;
        rptQuestions.DataBind();
    }
    protected void ddlInspection_OnSelectedIndexChanged(object sender,EventArgs e)
    {
	BindVersions();
        BindCategory();

    }
    protected void ddlVersion_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        btnEditVersion.Visible = ddlVersion.SelectedIndex > 0;
    }
    protected void ddlCategory_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        btnEditCat.Visible = ddlCategory.SelectedIndex > 0;
    }
    //-------------------------------------
    protected void btnAddCat_OnClick(object sender, EventArgs e)
    {
        if (ddlInspection.SelectedIndex <= 0)
        {
            lblMessege.Text = "Please select inspection.";
            return;
        }
        CatId = 0;
        txtCategoryCode.Text = "";
        txtCategory.Text = "";
        dvAddCategory.Visible = true;
    }
    protected void btnEditCat_OnClick(object sender, EventArgs e)
    {
        if (ddlCategory.SelectedIndex <= 0)
        {
            lblMessege.Text = "Please select category.";
            return;
        }
        CatId = Common.CastAsInt32(ddlCategory.SelectedValue);
        txtCategoryCode.Text = ddlCategory.SelectedItem.ToString().Split(':')[0].ToString().Trim();
        txtCategory.Text = ddlCategory.SelectedItem.ToString().Split(':')[1].ToString().Trim();
        dvAddCategory.Visible = true;
    }
    protected void btnSaveCategory_OnClick(object sender, EventArgs e)
    {
        if (txtCategory.Text.Trim() == "")
        {
            lblMsgAddCat.Text = "Please enter category name";
            return;
        }
        string sql = " SELECT * FROM DBO.tblInternalInspectionCheckListCategory WHERE CategoryName='" + txtCategory.Text.Trim() + "' and inspectionid=" + ddlInspection.SelectedValue + " and CategoryId<>" + CatId;
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
        if (dt.Rows.Count > 0)
        {
            lblMsgAddCat.Text = "Category name already exists";
            return;
        }

        if (txtCategoryCode.Text.Trim() == "")
        {
            lblMsgAddCat.Text = "Please enter category code";
            return;
        }
        sql = " SELECT * FROM DBO.tblInternalInspectionCheckListCategory WHERE CategoryCode='" + txtCategoryCode.Text.Trim() + "' and inspectionid=" + ddlInspection.SelectedValue + " and CategoryId<>" + CatId;
        DataTable dt1 = Common.Execute_Procedures_Select_ByQuery(sql);
        if (dt1.Rows.Count > 0)
        {
            lblMsgAddCat.Text = "Category code already exists";
            return;
        }
        if(CatId<=0)
            Common.Execute_Procedures_Select_ByQuery("INSERT INTO DBO.tblInternalInspectionCheckListCategory(CategoryName,CategoryCode,inspectionid) values('" + txtCategory.Text.Trim().Replace("'", "''") + "','" + txtCategoryCode.Text.Trim().Replace("'", "''") + "'," + ddlInspection.SelectedValue +")");
        else
            Common.Execute_Procedures_Select_ByQuery("UPDATE DBO.tblInternalInspectionCheckListCategory SET CategoryName='" + txtCategory.Text.Trim().Replace("'", "''") + "',CategoryCode='" + txtCategoryCode.Text.Trim().Replace("'", "''") + "' where CategoryId=" + CatId);

        dvAddCategory.Visible = false;
        string s = ddlCategory.SelectedValue;
        BindCategory();
        ddlCategory.SelectedValue=s;
    }
    //-------------------------------------
    protected void btnAddVersion_OnClick(object sender, EventArgs e)
    {
        VersionId = 0;
        txtVersion.Text = "";
        dvAddVersion.Visible = true;
    }
    protected void btnEditVersion_OnClick(object sender, EventArgs e)
    {
        VersionId = Common.CastAsInt32(ddlVersion.SelectedValue);
        txtVersion.Text = ddlVersion.SelectedItem.ToString();
        dvAddVersion.Visible = true;
    }
    protected void btnSaveVersion_OnClick(object sender, EventArgs e)
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.tblInternalInspectionCheckListVersion WHERE VersionName='" + txtVersion.Text.Trim() + "'");
        if (txtVersion.Text.Trim() != "" && dt.Rows.Count <= 0)
        {
            if(VersionId<=0)
                Common.Execute_Procedures_Select_ByQuery("INSERT INTO DBO.tblInternalInspectionCheckListVersion(VersionName,Inspectionid) values('" + txtVersion.Text.Trim().Replace("'", "''") + "'," + ddlInspection.SelectedValue +")");
            else
                Common.Execute_Procedures_Select_ByQuery("UPDATE DBO.tblInternalInspectionCheckListVersion SET VersionName='" + txtVersion.Text.Trim().Replace("'", "''") + "' WHERE VERSIONID=" +  VersionId);

            dvAddVersion.Visible = false;
            BindVersions();
        }
    }
    //-------------------------------------
    protected void btnClose_OnClick(object sender, EventArgs e)
    {
        dvAddVersion.Visible = false;
        dvAddCategory.Visible = false;
        dvQuestion.Visible = false;
    }
    //-------------------------------------

    protected void btnAddQ_OnClick(object sender, EventArgs e)
    {
        
        if (ddlInspection.SelectedIndex <= 0)
        {
            lblMessege.Text = "Please select inspection.";
            return;
        }

        if (ddlVersion.SelectedIndex <= 0)
        {
            lblMessege.Text = "Please select version.";
            return;
        }

        if (ddlCategory.SelectedIndex <= 0)
        {
            lblMessege.Text = "Please select category.";
            return;
        }

        CatId = Common.CastAsInt32(ddlCategory.SelectedValue);
        VersionId = Common.CastAsInt32(ddlVersion.SelectedValue);

        QuestionId = 0;
        txtQno.Text = "";
        txtQuestionText.Text = "";
        dvQuestion.Visible = true;
        lblInspectionName.Text = ddlInspection.SelectedItem.Text;
        lblVersion.Text = ddlVersion.SelectedItem.Text;
        lblCategory.Text = ddlCategory.SelectedItem.Text;
        ddlRating.SelectedIndex = 0;
        lblCatCode.Text = ddlCategory.SelectedItem.Text.Split(':')[0].ToString();
	txtGuidance.Text = "";
    }
    
    protected void btnSaveQuestion_OnClick(object sender, EventArgs e)
    {
        if (txtQno.Text.Trim()=="")
        {
            lblMessege.Text = "Please enter question no./code.";
            return;
        }
        if (txtQuestionText.Text.Trim()=="")
        {
            lblMessege.Text = "Please enter question text.";
            return;
        }
        if (ddlRating.SelectedIndex<=0)
        {
            lblMessege.Text = "Please select ratings requirement.";
            return;
        }
        //-----------------------------
        string SQL = "SELECT 1 FROM  dbo.tblInternalInspectionCheckList WHERE inspectionid=" + ddlInspection.SelectedValue + " and VersionId=" + VersionId + " and (questionno='" + lblCatCode.Text.Trim() + "." + txtQno.Text.Trim().Replace("'", "`") + "' OR questionname='" + txtQuestionText.Text.Trim().Replace("'", "`") + "') AND QuestionId<>" + QuestionId;
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
        if(dt.Rows.Count<=0)
        {
            //--------------------------
            SQL = "EXEC dbo.InsertUpdate_tblInternalInspectionCheckList " + QuestionId + "," + VersionId + "," + ddlInspection.SelectedValue + "," + CatId + ",'" + lblCatCode.Text.Trim() + "." + txtQno.Text.Trim() + "','" + txtQuestionText.Text.Trim().Replace("'", "`") + "','" + ddlRating.SelectedValue + "','" + txtGuidance.Text.Trim().Replace("'", "`") + "'";
            try
            {
                Common.Execute_Procedures_Select_ByQuery(SQL);
                lblMessege.Text = "Question saved successfully.";
                BindCheckList();

                QuestionId = 0;
                txtQno.Text= "";
                txtQuestionText.Text = "";
                ddlRating.SelectedIndex = 0;
txtGuidance.Text = "";

            }
            catch (Exception ex)
            {
                lblMessege.Text = "Unable to save question. Error : " + ex.Message;
            }

            //--------------------------
        }
        else
        {
            lblMessege.Text = "This question is already exists in the database.";

        }

    }
    
    protected void btnSearch_OnClick(object sender, EventArgs e)
    {
        BindCheckList();
    }

    protected void btnEditQuestion_OnClick(object sender, EventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        HiddenField hfdQuestionID = (HiddenField)btn.Parent.FindControl("hfdQuestionID");
        QuestionId = Common.CastAsInt32(hfdQuestionID.Value);
        ShowQuestionDataforEdit();
    }

    public void ShowQuestionDataforEdit()
    {
        string sql = " Select c.*,cat.categorycode,cat.categoryname,v.VersionName,m.name,guidance from dbo.tblInternalInspectionCheckList c " +
                    " inner join dbo.tblInternalInspectionCheckListCategory cat on cat.Categoryid = c.Categoryid " +
                    " inner join dbo.m_inspection m on m.id = c.inspectionid " +
                    " inner join dbo.tblInternalInspectionCheckListVersion v on c.VersionId = v.VersionId " +
                    " where QuestionID = " + QuestionId;
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
        if (dt.Rows.Count > 0)
        {
            DataRow dr = dt.Rows[0];
            dvQuestion.Visible = true;

            CatId = Common.CastAsInt32(dr["Categoryid"].ToString());
            VersionId = Common.CastAsInt32(dr["VersionId"].ToString());

            lblInspectionName.Text = dr["name"].ToString();
            lblVersion.Text = dr["VersionName"].ToString();
            lblCategory.Text = dr["categorycode"].ToString() + " : " + dr["categoryname"].ToString();
            lblCatCode.Text = dr["categorycode"].ToString();
            txtQno.Text = dr["questionno"].ToString().Substring(dr["questionno"].ToString().Split('.')[0].Length+1);
            txtQuestionText.Text =  dr["questionname"].ToString();
            ddlRating.SelectedValue = dr["RatingNeeded"].ToString();
		txtGuidance.Text= dr["guidance"].ToString();
        }
    }

}
