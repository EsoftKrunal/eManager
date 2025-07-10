using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Xml;
using System.IO;
using System.Text;
using Ionic.Zip;

public partial class AddEditSeminar : System.Web.UI.Page
{
    public int SeminarId
    {
        get
        {return Common.CastAsInt32(ViewState["SeminarId"]);}
        set { ViewState["SeminarId"] = value; }
    }
    public int TableId
    {
        get
        { return Common.CastAsInt32(ViewState["TableId"]); }
        set { ViewState["TableId"] = value; }
    }
    public int UserId
    {
        get
        { return Common.CastAsInt32(ViewState["UserId"]); }
        set { ViewState["UserId"] = value; }
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMessage.Text= "";
        //------------------------------------
        SessionManager.SessionCheck_New();
        //------------------------------------
        if (!Page.IsPostBack)
        {
            SeminarId = Common.CastAsInt32(Request.QueryString["SeminarId"]);
            UserId =Common.CastAsInt32(Session["LoginId"]);
            BindSeminarCategory();
            LoadRecruitingOffice();
            LoadEmployees();
            ShowRecord();
        }
    }
    public void LoadRecruitingOffice()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.RecruitingOffice ORDER BY RecruitingOfficeName");
        ddloffice.DataSource = dt;
        ddloffice.DataTextField = "RecruitingOfficeName";
        ddloffice.DataValueField = "RecruitingOfficeId";
        ddloffice.DataBind();
        ddloffice.Items.Insert(0, new ListItem(" -- Select --- ", ""));
    }
    private void BindSeminarCategory()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.tbl_SeminarCat ORDER BY SeminarCatName");
        ddlCategory.DataSource = dt;
        ddlCategory.DataTextField = "SeminarCatName";
        ddlCategory.DataValueField = "SeminarCatId";
        ddlCategory.DataBind();
        ddlCategory.Items.Insert(0, new ListItem(" -- Select --- ", ""));
    }
    public void LoadEmployees()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT USERID,FIRSTNAME + ' ' + MIDDLENAME + ' ' + FAMILYNAME AS EMPNAME FROM DBO.Hr_PersonalDetails WHERE POSITION IN (select P.positionid from DBO.position P where isinspector=1) AND DRC IS NULL and USERID is NOT NULL ORDER BY EMPNAME");
        chkPresenters.DataSource = dt;
        chkPresenters.DataTextField = "EMPNAME";
        chkPresenters.DataValueField = "USERID";
        chkPresenters.DataBind();
    }
    
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if(ddlCategory.SelectedIndex<=0)
        {
            ShowMessage("Please select seminar category.",true);
            ddlCategory.Focus();
            return ;
        }
        if(ddloffice.SelectedIndex<=0)
        {
            ShowMessage("Please select office.",true);
            ddloffice.Focus();
            return ;
        }
        DateTime d1=new DateTime();
        DateTime d2 = new DateTime();

        if (!(DateTime.TryParse(txtFdt.Text, out d1)))
        {

            ShowMessage("Please enter valid from date.", true);
            txtFdt.Focus();
            return;
        }
        if (!(DateTime.TryParse(txtTdt.Text, out d2)))
        {
            ShowMessage("Please enter valid to date.", true);
            txtTdt.Focus();
            return;

        }
        if (d1 > d2)
        {
            ShowMessage("Start date should be less than end date.", true);
            txtTdt.Focus();
            return;
        }

        if(txtTopic.Text.Trim()=="")
        {
            ShowMessage("Please enter seminar topic.",true);
            txtTopic.Focus();
            return;
        }

        string Presenters = "";
        foreach (ListItem li in chkPresenters.Items)
        {
            if (li.Selected)
                Presenters += "," + li.Value;
        }
        if (Presenters.StartsWith(","))
            Presenters = Presenters.Substring(1);

        Common.Set_Procedures("DBO.InsertUpdateSeminar");
        Common.Set_ParameterLength(9);
        Common.Set_Parameters(
            new MyParameter("@SeminarId", SeminarId),
            new MyParameter("@Topic", txtTopic.Text.Trim()),
            new MyParameter("@CategoryId", ddlCategory.SelectedValue),
            new MyParameter("@OfficeId", ddloffice.SelectedValue),
            new MyParameter("@Location", txtLocation.Text.Trim()),
            new MyParameter("@StartDate", txtFdt.Text.Trim()),
            new MyParameter("@EndDate", txtTdt.Text.Trim()),
            new MyParameter("@Presenters", Presenters),
            new MyParameter("@ModifiedBy", UserId));

        DataSet ds = new DataSet();
        if (Common.Execute_Procedures_IUD(ds))
        {
            SeminarId = Common.CastAsInt32(ds.Tables[0].Rows[0][0]);
            ShowMessage("Record saved sucessfully.", false);
        }
        else
        {
            ShowMessage("Unable to save record.", true);
        }

    }
    public void ShowRecord()
    {
        DataTable DT = Common.Execute_Procedures_Select_ByQuery("select * from DBO.tbl_Seminar where SeminarId=" + SeminarId );
        if (DT.Rows.Count > 0)
        {
            ddlCategory.SelectedValue = DT.Rows[0]["CategoryId"].ToString();
            ddloffice.SelectedValue = DT.Rows[0]["OfficeId"].ToString();
            txtFdt.Text = Common.ToDateString(DT.Rows[0]["StartDate"]);
            txtTdt.Text = Common.ToDateString(DT.Rows[0]["EndDate"]);
            txtLocation.Text = DT.Rows[0]["Location"].ToString();
            txtTopic.Text = DT.Rows[0]["Topic"].ToString();
            
            //----------------------------------

            DT = Common.Execute_Procedures_Select_ByQuery("select * from dbo.tbl_SeminarPresenters where SeminarId=" + SeminarId);
            foreach (ListItem li in chkPresenters.Items)
            {
                if (DT.Select("PresenterId=" + li.Value).Length > 0)
                    li.Selected = true;
            }
        }
    }
    public void ShowMessage(string Message, bool Error)
    {
        lblMessage.Text = Message;
        lblMessage.ForeColor = (Error) ? System.Drawing.Color.Red : System.Drawing.Color.Green;
    }
}

