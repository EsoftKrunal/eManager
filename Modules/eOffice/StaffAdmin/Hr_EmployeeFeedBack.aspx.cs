using System;
using System.Data;
using System.Configuration;
using System.Reflection;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;

using ShipSoft.CrewManager.BusinessLogicLayer;
using ShipSoft.CrewManager.BusinessObjects;
using ShipSoft.CrewManager.Operational;

public partial class emtm_StaffAdmin_Emtm_Hr_EmployeeFeedBack : System.Web.UI.Page
{
    public int TableId
    {
        get
        {
            return Common.CastAsInt32(ViewState["TableId"]);
        }
        set
        {
            ViewState["TableId"] = value;
        }
    }
    public int EmpId
    {
        get
        {
            return Common.CastAsInt32(ViewState["EmpId"]);
        }
        set
        {
            ViewState["EmpId"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        if (!IsPostBack)
        {
            EmpId = Common.CastAsInt32(Session["EmpId"]);
            if (EmpId > 0)
            {
                DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("SELECT * FROM Hr_PersonalDetails WHERE EMPID=" + EmpId.ToString());
                if (dt != null)
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];
                        lbl_EmpName.Text = "[ " + dr["EmpCode"].ToString() + " ] " + dr["FirstName"].ToString() + " " + dr["MiddleName"].ToString() + " " + dr["FamilyName"].ToString();
                        Session["EmpName"] = lbl_EmpName.Text.ToString();
                        
                    }

                BindFeedBack();
            }
        }
    }

    public void BindFeedBack()
    {
        string SQL = "SELECT TableId,EmpId,CASE WHEN LEN(FeedBack) > 60 THEN SUBSTRING(FeedBack,0,60)+ '...' ELSE FeedBack END AS FeedBack1,FeedBack, " +
                     "CASE Category WHEN 1 THEN 'Positive' WHEN 2 THEN 'Room for improvement' WHEN 3 THEN 'Critical' ELSE '' END AS Category1 , Category, " +
                     "(SELECT FirstName + ' ' +  LastName FROM userlogin PD WHERE PD.loginid = FB.EnteredBy ) AS EnteredBy, " +
                     "REPLACE(Convert(Varchar(11),EnteredOn,106),' ','-') AS EnteredOn, [FileName]  " +
                     "FROM HR_EmployeeFeedBack FB WHERE EmpId =" + EmpId ;

        DataTable dtFB = Common.Execute_Procedures_Select_ByQueryCMS(SQL);

        rptFeedBack.DataSource = dtFB;
        rptFeedBack.DataBind();
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        TableId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);

        HiddenField hfCategory = (HiddenField)((ImageButton)sender).FindControl("hfCategory");
        HiddenField hfFeedback = (HiddenField)((ImageButton)sender).FindControl("hfFeedback");

        ddlCategory.SelectedValue = hfCategory.Value;
        txtFeedBack.Text = hfFeedback.Value;

        BindFeedBack();

        btnAddNew_Click(sender, e);

    }

    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        TableId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);

        string deleteSQL = "DELETE FROM HR_EmployeeFeedBack WHERE TableId=" + TableId + "; SELECT -1";
        DataTable dtDelete = Common.Execute_Procedures_Select_ByQueryCMS(deleteSQL);

        if (dtDelete.Rows.Count > 0)
        {
            TableId = 0;
            BindFeedBack();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "alert('Record deleted successfully.');", true);
        }
        else
        {
            TableId = 0;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", "alert('Unable to delete record.');", true);
        }

    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        if (ddlCategory.SelectedValue == "0")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", "alert('Please select category.');", true);
            ddlCategory.Focus();
            return;
        }
        if (txtFeedBack.Text.Trim() == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", "alert('Please enter feedback.');", true);
            txtFeedBack.Focus();
            return;
        }

        FileUpload img = (FileUpload)FileUpload1;
        Byte[] imgByte = new Byte[0];
        string FileName = "";
        if (img.HasFile && img.PostedFile != null)
        {
            HttpPostedFile File = FileUpload1.PostedFile;
            FileName = "FeedBack_" + EmpId.ToString() + "_" + DateTime.Now.ToString("dd-MMM-yyy") + "_" + DateTime.Now.TimeOfDay.ToString().Replace(":", "-").ToString() + Path.GetExtension(File.FileName);

            imgByte = new Byte[File.ContentLength];
            File.InputStream.Read(imgByte, 0, File.ContentLength);
        }


        Common.Set_Procedures("HR_InsertUpdateEmployeeFeedBack");
        Common.Set_ParameterLength(7);
        Common.Set_Parameters(new MyParameter("@TableId", TableId),
            new MyParameter("@EmpId", EmpId),
            new MyParameter("@FeedBack", txtFeedBack.Text.Trim()),
            new MyParameter("@Category", ddlCategory.SelectedValue.Trim()),
            new MyParameter("@EnteredBy", Common.CastAsInt32(Session["loginid"])),
            new MyParameter("@FileName", FileName.Trim()),
            new MyParameter("@Attachment", imgByte)
            
            );
        DataSet ds = new DataSet();

        if (Common.Execute_Procedures_IUD_CMS(ds))
        {
            //TableId = 0;
            //BindFeedBack();
            btnCancel_Click(sender, e);
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Record saved successfully.');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "error", "alert('Unable to save record.');", true);
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        TableId = 0;
        BindFeedBack();

        ddlCategory.SelectedIndex = 0;
        txtFeedBack.Text = "";

        divAddEdit.Visible = false;
        trAddNew.Visible = true;
        dvFB.Style.Add("height", "370px");
    }
    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        divAddEdit.Visible = true;
        trAddNew.Visible = false;

        dvFB.Style.Add("height", "230px");
    }

    protected void btnAttachment_Click(object sender, EventArgs e)
    {
        int TableId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);

        ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "openwindow('" + TableId + "');", true);
    }
}