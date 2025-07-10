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

public partial class PB_PublicationType : System.Web.UI.Page
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
            BindVessels();
            BindRepeater();
        }
    }
    protected void BindVessels()
    {
        chkVessels.DataSource = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.VESSEL WHERE VESSELSTATUSID=1  ORDER BY VESSELNAME");
        chkVessels.DataTextField = "VESSELNAME";
        chkVessels.DataValueField = "VESSELID";
        chkVessels.DataBind();
    }

    protected void BindRepeater()
    {
        rptData.DataSource = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.PB_Publication_Type order by TypeId");
        rptData.DataBind();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        ClearControls();
        dvPopUp.Visible = true;
        txtPublicationType.Focus();
    }
    public void ShowRecord()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.PB_Publication_Type where TypeId=" + KeyId.ToString());
        if (dt.Rows.Count > 0)
        {
            txtPublicationType.Text = dt.Rows[0]["TypeName"].ToString();
        }
        chkVessels.ClearSelection();
        dt = Common.Execute_Procedures_Select_ByQuery("select VesselId from DBO.PB_Publication_Type_Vessels WHERE TypeId=" + KeyId.ToString());
        if (dt.Rows.Count > 0)
        {
            DataView dv = dt.DefaultView;
            foreach (ListItem li in chkVessels.Items)
            {
                dv.RowFilter = "VesselId=" + li.Value;
                li.Selected = dv.ToTable().Rows.Count > 0;
            }
        }
        
    }
    public void ClearControls()
    {
        KeyId = 0;
        txtPublicationType.Text = "";
        chkVessels.ClearSelection();
    }
    protected void lnlEdit_OnClick(object sender, EventArgs e)
    {
        KeyId = Common.CastAsInt32(((LinkButton)sender).CommandArgument);
        dvPopUp.Visible = true;
        ShowRecord();
        txtPublicationType.Focus();
    }
    protected void lnlDelete_OnClick(object sender, EventArgs e)
    {
        //KeyId = Common.CastAsInt32(((LinkButton)sender).CommandArgument);
        //KeyId = 0;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        dvPopUp.Visible = false;
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT ISNULL(MAX(TYPEID),0)+1 FROM DBO.PB_Publication_Type");
        try
        {
            if (KeyId > 0)
            {
                Common.Execute_Procedures_Select_ByQuery("UPDATE DBO.PB_Publication_Type SET TYPENAME='" + txtPublicationType.Text.Trim() + "' WHERE TYPEID=" + KeyId.ToString());
            }
            else
            {
                int NewId = Common.CastAsInt32(dt.Rows[0][0]);
                Common.Execute_Procedures_Select_ByQuery("INSERT INTO DBO.PB_Publication_Type(TYPEID,TYPENAME) VALUES(" + NewId.ToString() + ",'" + txtPublicationType.Text.Trim() + "')");
                KeyId = NewId;
            }
            //--------------------------
            Common.Execute_Procedures_Select_ByQuery("DELETE FROM DBO.PB_Publication_Type_Vessels WHERE TypeId=" + KeyId.ToString());
            if (dt.Rows.Count > 0)
            {
                DataView dv = dt.DefaultView;
                foreach (ListItem li in chkVessels.Items)
                {
                    if (li.Selected)
                    {
                        dt = Common.Execute_Procedures_Select_ByQuery("Insert Into DBO.PB_Publication_Type_Vessels(TypeId,VesselId) values(" + KeyId.ToString() + "," + li.Value + ")");
                    }
                }
            }
            //--------------------------

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
