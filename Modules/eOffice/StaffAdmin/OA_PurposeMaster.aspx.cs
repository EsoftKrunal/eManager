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

public partial class emtm_OfficeAbsence_Emtm_OA_PurposeMaster : System.Web.UI.Page
{
    public int PID
    {
        get
        {
            return Common.CastAsInt32(ViewState["PID"]);
        }
        set
        {
            ViewState["PID"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        lblMsg.Text = "";

        if (!Page.IsPostBack)
        {
            BindPurpose();
        }     

    }

    public void BindPurpose()
    {
        string sql = "SELECT PurposeId,Purpose  FROM HR_LeavePurpose ORDER BY PurposeId";
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        rptPurpose.DataSource = dt;
        rptPurpose.DataBind();
    }


    protected void btnSave_OnClick(object sender, EventArgs e)
    {
        try
        {

            string checkDuplicate = "SELECT * FROM HR_LeavePurpose WHERE PurposeId <> " + PID.ToString() + " AND Purpose = '" + txtPurpose.Text.Trim() + "' ";
            DataTable dtDuplicate = Common.Execute_Procedures_Select_ByQueryCMS(checkDuplicate);

            if (dtDuplicate.Rows.Count > 0)
            {
                lblMsg.Text = "Purpose already exist.";
                txtPurpose.Focus();
                return;
            }

            Common.Set_Procedures("DBO.HR_InsertUpdatePurposeMaster");
            Common.Set_ParameterLength(2);
            Common.Set_Parameters(
                    new MyParameter("@PurposeId", PID),
                    new MyParameter("@Purpose", txtPurpose.Text)
                );
            Boolean Res;
            DataSet ds = new DataSet();
            Res = Common.Execute_Procedures_IUD(ds);
            if (Res)
            {
                lblMsg.Text = "Record saved successfully";
                
                tblPurpose.Visible = false;
                divPurpose.Style.Add("height", "380px");
                btnSave.Visible = false;
                btnCancel.Visible = false;
                btnAdd.Visible = true;
                PID = 0;
                BindPurpose();
                txtPurpose.Text = "";
            }
            else
            {
                lblMsg.Text = "Error while saving data.";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error while saving data." + ex.Message;
        }

    }
    protected void imgEdit_OnClick(object sender, EventArgs e)
    {
        tblPurpose.Visible = true;
        divPurpose.Style.Add("height", "360px");
        btnSave.Visible = true;
        btnCancel.Visible = true;
        btnAdd.Visible = false;
        //---------------------------

        ImageButton btn = (ImageButton)sender;
        HiddenField PurposeId = (HiddenField)btn.Parent.FindControl("hfPurposeId");
        Label lblPurpose = (Label)btn.Parent.FindControl("lblPurpose");
        PID = Common.CastAsInt32(PurposeId.Value);

        txtPurpose.Text = lblPurpose.Text.Trim();
        
        BindPurpose();
    }
    protected void imgDelete_OnClick(object sender, EventArgs e)
    {
        ImageButton btn = (ImageButton)sender;       
        HiddenField PurposeId = (HiddenField)btn.Parent.FindControl("hfPurposeId");

        string checkSQL = "SELECT * FROM HR_OfficeAbsence WHERE PurposeId = " + PurposeId.Value.Trim() + " ";
        DataTable dtCheck = Common.Execute_Procedures_Select_ByQueryCMS(checkSQL);

        if (dtCheck.Rows.Count > 0)
        {
            lblMsg.Text = "Unable to delete record.it is in use.";
            return;
        }
        else
        {
            string sql = "Delete from HR_LeavePurpose where PurposeId=" + PurposeId.Value.Trim() + "";
            DataTable dtQuestion = Common.Execute_Procedures_Select_ByQueryCMS(sql);

            lblMsg.Text = "Record deleted successfully.";
            BindPurpose();
        }
    }
    protected void btnAdd_OnClick(object sender, EventArgs e)
    {
        txtPurpose.Text = "";
        tblPurpose.Visible = true;
        divPurpose.Style.Add("height", "340px");
        btnSave.Visible = true;
        btnCancel.Visible = true;
        btnAdd.Visible = false;
    }
    protected void btnCancel_OnClick(object sender, EventArgs e)
    {
        txtPurpose.Text = "";
        tblPurpose.Visible = false;
        divPurpose.Style.Add("height", "380px");
        btnSave.Visible = false;
        btnCancel.Visible = false;
        btnAdd.Visible = true;
        PID = 0;
        BindPurpose();
    }
    
   
}