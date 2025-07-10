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

public partial class SMS_ManualMaster : System.Web.UI.Page
{
    public AuthenticationManager Auth;
    public int ManualID
    {
        get
        {
            return Common.CastAsInt32(ViewState["ManualID"]);
        }
        set
        {
            ViewState["ManualID"] = value;
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
            //ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 12);
            //OBJ.Invoke();
            //Session["Authority"] = OBJ.Authority;
            //Auth = OBJ.Authority;
        }
        if (!Page.IsPostBack)
        {
            BindVesselType();
            BindGrid();

            //btnAdd.Visible = Auth.IsAdd;
        }
    }
    //protected void GridView_RowEditing(object sender, GridViewEditEventArgs e)
    //{
    //    int ManualId=Common.CastAsInt32(GridView_Manuals.DataKeys[e.NewEditIndex].Value);
    //    GridView_Manuals.SelectedIndex = e.NewEditIndex;
    //    ShowRecord(ManualId);
    //}
    protected void btnEdit_OnClick(object sender, EventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        HiddenField hManualID = (HiddenField)btn.Parent.FindControl("Hidden_SubChapterId");
        ManualID = Common.CastAsInt32(hManualID.Value);
        ShowRecord();
        btnReleaseNewVersion.Visible = true;
        btnAdd.Visible = false;
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        p1.Visible = true;
        btnSave.Visible = true;
        btnCancel.Visible = true;
        clearControls();
        txtversion.Text = "0.0";
        ManualID = 0;
        btnReleaseNewVersion.Visible = false;
        
    }
    

    private void clearControls()
    {
        txtManualName.Text = "";
        txtversion.Text = "";        
        chkVType.ClearSelection(); 
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ManualID==0)
        {
            int MaxManualId = 0;
            DataTable DT = Common.Execute_Procedures_Select_ByQuery("SELECT ISNULL(MAX(MANUALID),0)+1 FROM DBO.SMS_ManualMaster");
            MaxManualId = Common.CastAsInt32(DT.Rows[0][0]);
            DataTable DT1 = Common.Execute_Procedures_Select_ByQuery("INSERT INTO DBO.SMS_ManualMaster VALUES(" + MaxManualId.ToString() + ",'" + txtManualName.Text.Trim().Replace("'", "`") + "','" + txtversion.Text.Trim().Replace("'", "`") + "',GETDATE(),'',NULL);SELECT MAX(MANUALID) FROM DBO.SMS_ManualMaster");
            if (DT1 != null)
                if (DT1.Rows.Count > 0)
                {   
                    BindGrid();
                }
            ManualID = Common.CastAsInt32(MaxManualId);
        }
        else 
        {
            //DataTable DT1 = Common.Execute_Procedures_Select_ByQuery("DELETE FROM DBO.SMS_ManualVesselType WHERE MANUALID=" + hfdManualId.Value + ";UPDATE DBO.SMS_ManualMaster SET MANUALNAME='" + txtManualName.Text.Trim().Replace("'", "`") + "',VERSIONNO='" + txtversion.Text.Trim().Replace("'", "`") + "' WHERE MANUALID=" + hfdManualId.Value + ";SELECT " + hfdManualId.Value);
            DataTable DT1 = Common.Execute_Procedures_Select_ByQuery("UPDATE DBO.SMS_ManualMaster SET MANUALNAME='" + txtManualName.Text.Trim().Replace("'", "`") + "',VERSIONNO='" + txtversion.Text.Trim().Replace("'", "`") + "' WHERE MANUALID=" + ManualID.ToString() + ";SELECT " + ManualID.ToString());
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
        btnReleaseNewVersion.Visible = false;
        btnReleaseNewVersion.CommandArgument = "";
        ManualID = 0;
    }

    protected void btnReleaseNewVersion_Click(object sender, EventArgs e)
    {
        if (!Manual.IsApproveWholeManual(ManualID))            
        {
            lblMsg.Text = "Unable to releas new version. Some changes are not approved or applied.";
            return;
        }
        if (Manual.ApproveManual(ManualID.ToString()))
        {
            lblMsg.Text = "New Version Released successfully.";
        }
        else
        {
            lblMsg.Text = "Unable to approve.";
        }
        
    }
    
    //----------- Popup
    protected void btnEditVessels_OnClick(object sender, EventArgs e)
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
        DataTable DT1 = Common.Execute_Procedures_Select_ByQuery("DELETE FROM DBO.SMS_ManualVesselType WHERE MANUALID=" + ManualID.ToString() + ";UPDATE DBO.SMS_ManualMaster SET MANUALNAME='" + txtManualName.Text.Trim().Replace("'", "`") + "',VERSIONNO='" + txtversion.Text.Trim().Replace("'", "`") + "' WHERE MANUALID=" + ManualID.ToString() + ";SELECT " + ManualID.ToString());
        if (DT1 != null)
            if (DT1.Rows.Count > 0)
            {
                foreach (ListItem li in chkVType.Items)
                {
                    if (li.Selected)
                    {
                        Common.Execute_Procedures_Select_ByQuery("INSERT INTO DBO.SMS_ManualVesselType VALUES(" + ManualID.ToString() + "," + li.Value + ")");
                    }
                }
                BindGrid();
                BindManualVesselGrd();
            }
    }
    
    //----------------   Function
    public void BindManualVesselGrd()
    {
        string sql = " SELECT Row_Number() over(order by VESSELNAME)RowNo,VesselName FROM DBO.VESSEL V Where V.VESSELSTATUSID<>2  " +
                     " And V.VESSELID IN(SELECT VesselTypeId FROM DBO.SMS_ManualVesselType where MANUALID =" + ManualID + ") " + 
                     " ORDER BY V.VESSELNAME";
        DataTable DT = Common.Execute_Procedures_Select_ByQuery(sql);
        grdManualVessel.DataSource = DT;
        grdManualVessel.DataBind();
    }
    private void ShowRecord()
    {
        BindManualVesselGrd();
        //-----------------
        DataTable DT = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.SMS_ManualMaster where MANUALID=" + ManualID.ToString());
        if (DT.Rows.Count > 0)
        {
            txtManualName.Text = DT.Rows[0]["ManualName"].ToString();
            txtversion.Text = DT.Rows[0]["VersionNo"].ToString();
        }
        chkVType.ClearSelection();
        DT = Common.Execute_Procedures_Select_ByQuery("SELECT VesselTypeId FROM DBO.SMS_ManualVesselType where MANUALID=" + ManualID.ToString());

        foreach (DataRow dr in DT.Rows)
        {
            ListItem li = chkVType.Items.FindByValue(dr["VesselTypeId"].ToString());
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
        GridView_Manuals.DataSource = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.SMS_MANUALMASTER order by ManualName");
        GridView_Manuals.DataBind();
    }
    protected void BindVesselType()
    {
        //chkVType.DataSource = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.VESSELTYPE ORDER BY VESSELTYPENAME");
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.VESSEL V Where V.VESSELSTATUSID<>2  ORDER BY V.VESSELNAME");
        chkVType.DataSource = dt;
        chkVType.DataBind();
    }
}
