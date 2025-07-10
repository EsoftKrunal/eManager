using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections.Generic;

public partial class PB_Publication : System.Web.UI.Page
{
    public int KeyId
    {
        set { ViewState["KeyId"] = value; }
        get { return Common.CastAsInt32(ViewState["KeyId"]); }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        if (!IsPostBack)
        {
            BindType();
            BindMode();
            BindPublisher();
            BindReqBy();
            KeyId = 0;
            BindRepeater();
            
        }
    }
    protected void BindType()
    {
        string sql = "SELECT * FROM DBO.PB_Publication_Type ORDER BY TypeNAME";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
        ddlType.DataSource = dt;
        ddlType.DataTextField = "TypeNAME";
        ddlType.DataValueField = "TypeID";
        ddlType.DataBind();
        ddlType.Items.Insert(0, new ListItem(" < -- SELECT -- > ", "0"));

        ddlType_F.DataSource = dt;
        ddlType_F.DataTextField = "TypeNAME";
        ddlType_F.DataValueField = "TypeID";
        ddlType_F.DataBind();
        ddlType_F.Items.Insert(0, new ListItem(" < -- All -- > ", "0"));
    }
    protected void BindMode()
    {
        string sql = "SELECT * FROM DBO.PB_Publication_Mode ORDER BY ModeNAME";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
        ddlMode.DataSource = dt;
        ddlMode.DataTextField = "ModeNAME";
        ddlMode.DataValueField = "ModeId";
        ddlMode.DataBind();
        ddlMode.Items.Insert(0, new ListItem(" < -- SELECT -- > ", "0"));

        ddlMode_F.DataSource = dt;
        ddlMode_F.DataTextField = "ModeNAME";
        ddlMode_F.DataValueField = "ModeId";
        ddlMode_F.DataBind();
        ddlMode_F.Items.Insert(0, new ListItem(" < -- All -- > ", "0"));
    }
    protected void BindPublisher()
    {
        string sql = "SELECT * FROM DBO.PB_Publisher ORDER BY PublisherNAME";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
        
        ddlPublisher.DataSource = dt;
        ddlPublisher.DataTextField = "PublisherNAME";
        ddlPublisher.DataValueField = "PublisherId";
        ddlPublisher.DataBind();
        ddlPublisher.Items.Insert(0, new ListItem(" < -- SELECT -- > ", "0"));

        ddl_Publisher_F.DataSource = dt;
        ddl_Publisher_F.DataTextField = "PublisherNAME";
        ddl_Publisher_F.DataValueField = "PublisherId";
        ddl_Publisher_F.DataBind();
        ddl_Publisher_F.Items.Insert(0, new ListItem(" < -- All -- > ", "0"));
    }
    protected void BindReqBy()
    {
        string sql = "SELECT * FROM DBO.PB_RequiredBy ORDER BY RequiredByName";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
        chkRequiredBy.DataSource = dt;
        chkRequiredBy.DataTextField = "RequiredByName";
        chkRequiredBy.DataValueField = "RequiredById";
        chkRequiredBy.DataBind();
    }
    protected void BindRepeater()
    {
        string WhereClause = " where 1=1 ";
        if (txtPubName_F.Text.Trim() != "")
        {
            WhereClause += " and PUBLICATIONNAME LIKE '%" + txtPubName_F.Text.Trim() + "%'";
        }
        if (ddlOfficeShip_F.SelectedIndex > 0)
        {
            WhereClause += " and PUB.OfficeShip in ('" + ddlOfficeShip_F.SelectedValue + "','B')";
        }
        if (ddlType_F.SelectedIndex > 0)
        {
            WhereClause += " and PUB.TYPEID =" + ddlType_F.SelectedValue;
        }
        if (ddlMode_F.SelectedIndex > 0)
        {
            WhereClause += " and PUB.MODEID =" + ddlMode_F.SelectedValue;
        }
        if (ddl_Publisher_F.SelectedIndex > 0)
        {
            WhereClause += " and PUB.PUBLISHERID =" + ddl_Publisher_F.SelectedValue;
        }
        string sql = "SELECT PUBLICATIONID,PUBLICATIONNAME,TYPENAME,MODENAME,PUBLISHERNAME,OfficeShip=(case when OfficeShip='O' then 'Office' when OfficeShip='S' then 'Ship' else 'Both' End),EditionYear,EditionNo,ValidityDate,CREATEDBY,CREATEDON " +
                   "FROM DBO.PB_PUBLICATIONS PUB  " +
                   "INNER JOIN DBO.PB_Publication_Type T ON T.TYPEID=PUB.TYPEID " +
                   "INNER JOIN DBO.PB_Publication_Mode D ON D.MODEID=PUB.MODEID " +
                   "LEFT JOIN DBO.PB_Publisher S ON S.PUBLISHERID=PUB.PUBLISHERID " + WhereClause;
                   
        rptData.DataSource = Common.Execute_Procedures_Select_ByQuery(sql);
        rptData.DataBind();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        ClearControls();
        dvPopUp.Visible = true;
        txtPublicationName.Focus();
    }
    public void ShowRecord()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.PB_PUBLICATIONS where PUBLICATIONID=" + KeyId.ToString());
        if (dt.Rows.Count > 0)
        {
            txtPublicationName.Text = dt.Rows[0]["PublicationName"].ToString();
            ddlType.SelectedValue = dt.Rows[0]["TypeId"].ToString();
            ddlMode.SelectedValue = dt.Rows[0]["ModeId"].ToString();
            ddlPublisher.SelectedValue = dt.Rows[0]["PublisherId"].ToString();
            txtEditionYear.Text = dt.Rows[0]["EditionYear"].ToString();
            txtEditionNo.Text = dt.Rows[0]["EditionNo"].ToString();
            txtValidityDate.Text = Common.ToDateString(dt.Rows[0]["ValidityDate"]);
            ddlOfficeShip.SelectedValue = dt.Rows[0]["OfficeShip"].ToString();
        }
        dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.PB_PublicationEditions where PUBLICATIONID=" + KeyId.ToString());
        if (dt.Rows.Count > 0)
        {
            txtEditionYear.Enabled = false;
            txtEditionNo.Enabled = false;
            txtValidityDate.Enabled = false;
        }
        chkRequiredBy.ClearSelection();
        dt = Common.Execute_Procedures_Select_ByQuery("select RequiredById from DBO.PB_Publications_RequiredBy WHERE PublicationId=" + KeyId.ToString());
        if (dt.Rows.Count > 0)
        {
            DataView dv=dt.DefaultView;
            foreach(ListItem li in chkRequiredBy.Items)
            {
                dv.RowFilter="RequiredById=" + li.Value;
                li.Selected = dv.ToTable().Rows.Count > 0; 
            }
        }
    }
    public void ClearControls()
    {
          KeyId = 0;
          txtPublicationName.Text = "";
          txtPublicationName.Text = "";
          ddlType.SelectedIndex=0;
          ddlMode.SelectedIndex=0;
          ddlPublisher.SelectedIndex=0;
          chkRequiredBy.ClearSelection();
          txtEditionYear.Text="";
          txtEditionNo.Text="";
          txtValidityDate.Text = "";
          ddlOfficeShip.SelectedIndex = 0;

          txtEditionYear.Enabled = true;
          txtEditionNo.Enabled = true;
          txtValidityDate.Enabled = true; 
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindRepeater();
    }
    //---------- Release new version
    protected void lnlRelease_OnClick(object sender, EventArgs e)
    {
        txtNewEditionYear.Text = "";
        txtNewEditionNo.Text = "";
        txtNewValidityDate.Text = "";
        //------------
        KeyId = Common.CastAsInt32(((LinkButton)sender).CommandArgument);
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.PB_PUBLICATIONS where PUBLICATIONID=" + KeyId.ToString());
        if (dt.Rows.Count > 0)
        {
            lblPubName.Text = dt.Rows[0]["PublicationName"].ToString();
        }
        dvPopUp1.Visible = true;
        txtNewEditionYear.Focus();
    }
    protected void btnClose1_Click(object sender, EventArgs e)
    {
        dvPopUp1.Visible = false;
    }
    protected void btnRelease_Click(object sender, EventArgs e)
    {
        try
        {
            string UserName = "";
            DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT FIRSTNAME + ' ' + LASTNAME FROM DBO.USERLOGIN WHERE LOGINID=" + Session["loginid"].ToString());
            UserName = dt.Rows[0][0].ToString();
            string sql = "INSERT INTO DBO.PB_PublicationEditions(PUBLICATIONID,EditionYear,EditionNo,ValidityDate,CreatedBy,CreatedOn) " +
                    "VALUES(" + KeyId + "," + Common.CastAsInt32(txtNewEditionYear.Text).ToString() + ",'" + txtNewEditionNo.Text + "','" + txtNewValidityDate.Text + "','" + UserName + "',getdate())";
            Common.Execute_Procedures_Select_ByQuery(sql);
            dvPopUp1.Visible = false;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "alert('New version released successfully.');", true);
        }
        catch
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "alert('Unable to release new version.');", true);
        }
    }
    //------------------------------
    protected void lnlEdit_OnClick(object sender, EventArgs e)
    {
        ClearControls();
        KeyId = Common.CastAsInt32(((LinkButton)sender).CommandArgument);
        dvPopUp.Visible = true;
        ShowRecord();
        txtPublicationName.Focus();
    }
    protected void lnlDelete_OnClick(object sender, EventArgs e)
    {
        //KeyId = Common.CastAsInt32(((LinkButton)sender).CommandArgument);
        //KeyId = 0;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

        string UserName = "";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT FIRSTNAME + ' ' + LASTNAME FROM DBO.USERLOGIN WHERE LOGINID=" + Session["loginid"].ToString());
        UserName=dt.Rows[0][0].ToString();
        dvPopUp.Visible = false;
        dt = Common.Execute_Procedures_Select_ByQuery("SELECT ISNULL(MAX(PublicationId),0)+1 FROM DBO.PB_Publications");
        try
        {
            if (KeyId > 0)
            {
                string sql = "UPDATE DBO.PB_Publications " +
                        "SET PublicationName='" + txtPublicationName.Text.Trim() + "'," +
                        "TypeId=" + Common.CastAsInt32(ddlType.SelectedValue).ToString() + "," +
                        "ModeId=" + Common.CastAsInt32(ddlMode.SelectedValue).ToString() + "," +
                        "PublisherId=" + Common.CastAsInt32(ddlPublisher.SelectedValue).ToString() + "," +
                        "EditionYear=" + txtEditionYear.Text.Trim() + "," +
                        "EditionNo='" + txtEditionNo.Text.Trim() + "'," +
                        "OfficeShip='" + ddlOfficeShip.SelectedValue + "'," +
                        "ValidityDate='" + txtValidityDate.Text.Trim() + "' " +
                        "WHERE PublicationId=" + KeyId.ToString();
                Common.Execute_Procedures_Select_ByQuery(sql);
            }
            else
            {
                int NewId = Common.CastAsInt32(dt.Rows[0][0]);
                string sql="INSERT INTO DBO.PB_Publications(PublicationId, " +
                           "PublicationName," +
                           "TypeId," +
                           "ModeId," +
                           "PublisherId," +
                           "OfficeShip," +
                           "EditionYear," +
                           "EditionNo," +
                           "ValidityDate," +
                           "CreatedBy," +
                           "CreatedOn) VALUES(" + NewId.ToString()  + "," +
                           "'" + txtPublicationName.Text.Trim() + "'," +
                           "" + Common.CastAsInt32(ddlType.SelectedValue).ToString() + "," +
                           "" + Common.CastAsInt32(ddlMode.SelectedValue).ToString() + "," +
                           "" + Common.CastAsInt32(ddlPublisher.SelectedValue).ToString() + "," +
                           "'" + ddlOfficeShip.SelectedValue + "'," +
                           "" + txtEditionYear.Text.Trim() + "," +
                           "'" + txtEditionNo.Text.Trim() + "'," +
                           "'" + txtValidityDate.Text.Trim() + "'," +
                           "'" + UserName.Trim() + "'," +
                           "GETDATE())";
                Common.Execute_Procedures_Select_ByQuery(sql);
                KeyId = NewId;
            }
            //--------------------------
            Common.Execute_Procedures_Select_ByQuery("DELETE FROM DBO.PB_Publications_RequiredBy WHERE PublicationId=" + KeyId.ToString());
            if (dt.Rows.Count > 0)
            {
                DataView dv = dt.DefaultView;
                foreach (ListItem li in chkRequiredBy.Items)
                {
                    if (li.Selected)
                    {
                        dt = Common.Execute_Procedures_Select_ByQuery("Insert Into DBO.PB_Publications_RequiredBy(PublicationId,RequiredById) values(" + KeyId.ToString() + "," + li.Value + ")");
                    }
                }
            }
            //--------------------------
            BindRepeater();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "alert('Record saved successfully.');", true);
        }
        catch
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "alert('Unable to save record.');", true);
        }
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        dvPopUp.Visible = false;
    }
    
}

