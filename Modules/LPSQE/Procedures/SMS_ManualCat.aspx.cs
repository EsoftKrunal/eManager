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

public partial class SMS_ManualCat : System.Web.UI.Page
{
    public AuthenticationManager Auth;
    public int ManualCatId
    {
        get
        {
            return Common.CastAsInt32(ViewState["ManualCatId"]);
        }
        set
        {
            ViewState["ManualCatId"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
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
            BindVesselType();
            BindGrid();
        }
    }
    protected void btnEdit_OnClick(object sender, EventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        HiddenField hManualID = (HiddenField)btn.Parent.FindControl("Hidden_ManualCatId");
        ManualCatId = Common.CastAsInt32(hManualID.Value);
        ShowRecord();
        btnAdd.Visible = false;
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        p1.Visible = true;
        btnSave.Visible = true;
        btnCancel.Visible = true;
        clearControls();
        ManualCatId = 0;
}
    private void clearControls()
    {
        txtManualCatName.Text = "";
        chkManusls.ClearSelection(); 
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ManualCatId == 0)
        {
            int MaxManualCatId = 0;
            DataTable DT = Common.Execute_Procedures_Select_ByQuery("SELECT ISNULL(MAX(ManualCatId),0)+1 FROM DBO.SMS_ManualCategory");
            MaxManualCatId = Common.CastAsInt32(DT.Rows[0][0]);
            DataTable DT1 = Common.Execute_Procedures_Select_ByQuery("INSERT INTO DBO.SMS_ManualCategory VALUES(" + MaxManualCatId.ToString() + ",'" + txtManualCatName.Text.Trim().Replace("'", "`") +"');SELECT MAX(MANUALID) FROM DBO.SMS_ManualMaster");
            if (DT1 != null)
                if (DT1.Rows.Count > 0)
                {   
                    BindGrid();
                }
            ManualCatId = Common.CastAsInt32(MaxManualCatId);
        }
        else 
        {
            DataTable DT1 = Common.Execute_Procedures_Select_ByQuery("UPDATE DBO.SMS_ManualCategory SET ManualCatName='" + txtManualCatName.Text.Trim().Replace("'", "`") + "' WHERE ManualCatId=" + ManualCatId.ToString() + ";SELECT " + ManualCatId.ToString());
            if (DT1 != null)
                  BindGrid();
            lblMsg.Text = "Data saved successfully.";
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        p1.Visible = false;
        divManualVesselGrid.Visible = false;
        
        btnSave.Visible = false;
        btnAdd.Visible = true;
        btnCancel.Visible = false;
        ManualCatId = 0;
    }
    //----------- Popup
    protected void btnEditManuals_OnClick(object sender, EventArgs e)
    {
        dvPopupEditVessel.Visible = true;
        ShowRecord();
    }
    protected void btnClosePopupEditVessel_OnClick(object sender, EventArgs e)
    {
        dvPopupEditVessel.Visible = false;
    }
    protected void btnSavePopupEditVessel_OnClick(object sender, EventArgs e)
    {
        DataTable DT1 = Common.Execute_Procedures_Select_ByQuery("DELETE FROM DBO.SMS_ManualCatMappings WHERE ManualCatId=" + ManualCatId.ToString() + ";UPDATE DBO.SMS_ManualCategory SET ManualCatName='" + txtManualCatName.Text.Trim().Replace("'", "`") + "' WHERE ManualCatId=" + ManualCatId.ToString() + ";SELECT " + ManualCatId.ToString());
        if (DT1 != null)
            if (DT1.Rows.Count > 0)
            {
                foreach (ListItem li in chkManusls.Items)
                {
                    if (li.Selected)
                    {
                        Common.Execute_Procedures_Select_ByQuery("DELETE FROM DBO.SMS_ManualCatMappings WHERE MANUALID=" + li.Value);
                        Common.Execute_Procedures_Select_ByQuery("INSERT INTO DBO.SMS_ManualCatMappings VALUES(" + ManualCatId.ToString() + "," + li.Value + ")");
                    }
                }
                BindGrid();
                BindManualVesselGrd();
            }
    }
    
    //----------------   Function
    public void BindManualVesselGrd()
    {
        string sql = " SELECT Row_Number() over(order by ManualName)RowNo,ManualName FROM DBO.SMS_ManualMaster M Where " +
                     " M.ManualId IN(SELECT ManualId FROM DBO.SMS_ManualCatMappings where ManualCatId =" + ManualCatId + ") " +
                     " ORDER BY M.ManualName";
        DataTable DT = Common.Execute_Procedures_Select_ByQuery(sql);
        grdManualVessel.DataSource = DT;
        grdManualVessel.DataBind();
    }
    private void ShowRecord()
    {
        BindManualVesselGrd();
        //-----------------
        DataTable DT = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.SMS_ManualCategory where ManualCatId=" + ManualCatId.ToString());
        if (DT.Rows.Count > 0)
        {
            txtManualCatName.Text = DT.Rows[0]["ManualCatName"].ToString();
        }

        chkManusls.ClearSelection();
        DT = Common.Execute_Procedures_Select_ByQuery("SELECT ManualId FROM DBO.SMS_ManualCatMappings where ManualCatId=" + ManualCatId.ToString());

        foreach (DataRow dr in DT.Rows)
        {
            ListItem li = chkManusls.Items.FindByValue(dr["ManualId"].ToString());
            if (li != null)
                li.Selected = true;
        }

        p1.Visible = true;
        divManualVesselGrid.Visible = true;
        btnSave.Visible = true;
        btnCancel.Visible = true;
    }
    protected void BindGrid()
    {
        GridView_ManualsCat.DataSource = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.SMS_ManualCategory order by ManualCatName");
        GridView_ManualsCat.DataBind();
    }
    protected void BindVesselType()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.SMS_ManualMaster ORDER BY ManualName");
        chkManusls.DataSource = dt;
        chkManusls.DataBind();
    }
}
