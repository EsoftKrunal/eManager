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

public partial class PB_PublicationMode : System.Web.UI.Page
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
            KeyId = 0;
            BindRepeater();
        }
    }
    protected void BindRepeater()
    {
        rptData.DataSource = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.PB_Publication_Mode order by ModeId");
        rptData.DataBind();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        ClearControls();
        dvPopUp.Visible = true;
        txtPublicationMode.Focus();
    }
    public void ShowRecord()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.PB_Publication_Mode where ModeId=" + KeyId.ToString());
        if (dt.Rows.Count > 0)
        {
            txtPublicationMode.Text = dt.Rows[0]["ModeName"].ToString();
        }
    }
    public void ClearControls()
    {
        KeyId = 0;
        txtPublicationMode.Text = "";
    }
    protected void lnlEdit_OnClick(object sender, EventArgs e)
    {
        KeyId = Common.CastAsInt32(((LinkButton)sender).CommandArgument);
        dvPopUp.Visible = true;
        ShowRecord();
        txtPublicationMode.Focus();
    }
    protected void lnlDelete_OnClick(object sender, EventArgs e)
    {
        //KeyId = Common.CastAsInt32(((LinkButton)sender).CommandArgument);
        //KeyId = 0;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        dvPopUp.Visible = false;
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT ISNULL(MAX(ModeId),0)+1 FROM DBO.PB_Publication_Mode");
        try
        {
            if (KeyId > 0)
            {
                Common.Execute_Procedures_Select_ByQuery("UPDATE DBO.PB_Publication_Mode SET ModeName='" + txtPublicationMode.Text.Trim() + "' WHERE ModeId=" + KeyId.ToString());
            }
            else
            {
                int NewId = Common.CastAsInt32(dt.Rows[0][0]);
                Common.Execute_Procedures_Select_ByQuery("INSERT INTO DBO.PB_Publication_Mode(ModeId,ModeName) VALUES(" + NewId.ToString() + ",'" + txtPublicationMode.Text.Trim() + "')");
                KeyId = NewId;
            }
            BindRepeater();
            ScriptManager.RegisterStartupScript(this,this.GetType(),"a","alert('Record saved successfully.');",true); 
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
