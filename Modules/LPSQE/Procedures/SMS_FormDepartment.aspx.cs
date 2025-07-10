using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modules_LPSQE_Procedures_SMS_FormDepartment : System.Web.UI.Page

{
    public AuthenticationManager Auth;
    public int DepartmentId
    {
        get
        {
            return Common.CastAsInt32(ViewState["DepartmentId"]);
        }
        set
        {
            ViewState["DepartmentId"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        { 
            //------------------------------------
            ProjectCommon.SessionCheck_New();
            //------------------------------------
            int UserId = 0;
            UserId = int.Parse(Session["loginid"].ToString());

            Auth = new AuthenticationManager(1057, UserId, ObjectType.Page);
            if (!(Auth.IsView))
            {
                Response.Redirect("NotAuthorized.aspx");
            }

            lblMsg.Text = "";
            if (Session["loginid"] != null)
            {
               
            }
            if (!Page.IsPostBack)
            {
                BindGrid();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message.ToString();
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        p1.Visible = true;
        btnSave.Visible = true;
        btnCancel.Visible = true;

        DepartmentId = 0;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (DepartmentId == 0)
            {
                int MaxDepartmentId = 0;
                DataTable DT = Common.Execute_Procedures_Select_ByQuery("SELECT ISNULL(MAX(DepartmentId),0)+1 FROM DBO.[SMS_FormDepartment] with(nolock) ");
                MaxDepartmentId = Common.CastAsInt32(DT.Rows[0][0]);
                DataTable DT1 = Common.Execute_Procedures_Select_ByQuery("INSERT INTO DBO.SMS_FormDepartment (DepartmentId,DepartmentName,DepartmentShortName,DepartmentAddedBy,DepartmentAddedOn,DepartmentStatusId)  VALUES(" + MaxDepartmentId.ToString() + ",'" + txtFormDepartmentName.Text.Trim().Replace("'", "`") + "','" + txtFormDepartmentShortName.Text.Trim().Replace("'", "`") + "'," + int.Parse(Session["loginid"].ToString()) + ",GETDATE(),0);SELECT MAX(DepartmentId) FROM DBO.SMS_FormDepartment with(nolock) ");
                if (DT1 != null)
                    if (DT1.Rows.Count > 0)
                    {
                        BindGrid();
                    }
                DepartmentId = Common.CastAsInt32(MaxDepartmentId);
                lblMsg.Text = "Forms Department saved successfully.";
            }
            else
            {
                DataTable DT1 = Common.Execute_Procedures_Select_ByQuery("UPDATE DBO.SMS_FormDepartment SET [DepartmentName]='" + txtFormDepartmentName.Text.Trim().Replace("'", "`") + "', [DepartmentShortName]='" + txtFormDepartmentShortName.Text.Trim().Replace("'", "`") + "',DepartmentModifiedBy = " + int.Parse(Session["loginid"].ToString()) + ",DepartmentModifiedOn = GETDATE()  WHERE DepartmentId=" + DepartmentId.ToString() + ";SELECT " + DepartmentId.ToString());
                if (DT1 != null)
                    if (DT1.Rows.Count > 0)
                    {
                        BindGrid();
                    }
                lblMsg.Text = "Forms Department updated successfully.";
            }
        }
       catch(Exception ex)
        {
            lblMsg.Text = ex.Message.ToString();
        }
    }



    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            p1.Visible = false;
        btnSave.Visible = false;
        btnAdd.Visible = true;
        btnCancel.Visible = false;
        DepartmentId = 0;
        txtFormDepartmentName.Text = "";
        txtFormDepartmentShortName.Text = "";
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message.ToString();
        }
    }

    protected void BindGrid()
    {
        rptFormDepartment.DataSource = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.[SMS_FormDepartment] where DepartmentStatusId = 0 order by DepartmentName");
        rptFormDepartment.DataBind();
    }

    protected void btnEdit_OnClick(object sender, EventArgs e)
    {
        try
        {
            ImageButton btn = (ImageButton)sender;
        HiddenField hDepartmentID = (HiddenField)btn.Parent.FindControl("hdnDepartmentId");
        DepartmentId = Common.CastAsInt32(hDepartmentID.Value);
        ShowRecord();
        btnAdd.Visible = false;
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message.ToString();
        }
    }
    private void ShowRecord()
    {
       
        //-----------------
        DataTable DT = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.SMS_FormDepartment with(nolock) where DepartmentId=" + DepartmentId.ToString());
        if (DT.Rows.Count > 0)
        {
            txtFormDepartmentName.Text = DT.Rows[0]["DepartmentName"].ToString();
            txtFormDepartmentShortName.Text = DT.Rows[0]["DepartmentShortName"].ToString();
        }
        p1.Visible = true;
       
        btnSave.Visible = true;
        btnCancel.Visible = true;
    }
}