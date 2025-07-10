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

public partial class Modules_LPSQE_Procedures_SMS_FormCategory : System.Web.UI.Page
{
    public AuthenticationManager Auth;
    public int FormsCatID
    {
        get
        {
            return Common.CastAsInt32(ViewState["FormsCatID"]);
        }
        set
        {
            ViewState["FormsCatID"] = value;
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
    protected void btnEdit_OnClick(object sender, EventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        HiddenField hFormsCatID = (HiddenField)btn.Parent.FindControl("hdnFormsCatId");
        FormsCatID = Common.CastAsInt32(hFormsCatID.Value);
        ShowRecord();
        //btnReleaseNewVersion.Visible = true;
        btnAdd.Visible = false;
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        p1.Visible = true;
        btnSave.Visible = true;
        btnCancel.Visible = true;
        clearControls();
        //txtversion.Text = "0.0";
        FormsCatID = 0;
        //btnReleaseNewVersion.Visible = false;

    }


    private void clearControls()
    {
        txtFormsCatName.Text = "";
        //txtversion.Text = "";
        chkVType.ClearSelection();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (FormsCatID == 0)
            {
                int MaxFormsCatID = 0;
                DataTable DT = Common.Execute_Procedures_Select_ByQuery("SELECT ISNULL(MAX(FormsCatId),0)+1 FROM DBO.SMS_FormsCategoryMaster with(nolock) ");
                MaxFormsCatID = Common.CastAsInt32(DT.Rows[0][0]);
                DataTable DT1 = Common.Execute_Procedures_Select_ByQuery("INSERT INTO DBO.SMS_FormsCategoryMaster (FormsCatId,FormsCatName,CreatedBy,CreatedOn ) VALUES(" + MaxFormsCatID.ToString() + ",'" + txtFormsCatName.Text.Trim().Replace("'", "`") + "'," + int.Parse(Session["loginid"].ToString()) + ",GETDATE());SELECT MAX(FormsCatId) FROM DBO.SMS_FormsCategoryMaster with(nolock)");
                if (DT1 != null)
                    if (DT1.Rows.Count > 0)
                    {
                        BindGrid();
                    }
                FormsCatID = Common.CastAsInt32(MaxFormsCatID);
                lblMsg.Text = "Forms Category saved successfully.";
            }
            else
            {

                DataTable DT1 = Common.Execute_Procedures_Select_ByQuery("UPDATE DBO.SMS_FormsCategoryMaster SET FormsCatName='" + txtFormsCatName.Text.Trim().Replace("'", "`") + "',ModifiedBy = " + int.Parse(Session["loginid"].ToString()) + ", ModifiedOn = GETDATE() WHERE FormsCatId=" + FormsCatID.ToString() + ";SELECT " + FormsCatID.ToString());
                if (DT1 != null)
                    BindGrid();
                lblMsg.Text = "Forms Category updated successfully.";
            }
        }
      catch(Exception ex)
        {
            lblMsg.Text = ex.Message.ToString();
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        p1.Visible = false;
        divFormsCatVesselGrid.Visible = false;

        btnSave.Visible = false;
        btnAdd.Visible = true;
        btnCancel.Visible = false;
        //btnReleaseNewVersion.Visible = false;
        //btnReleaseNewVersion.CommandArgument = "";
        FormsCatID = 0;
    }

    //protected void btnReleaseNewVersion_Click(object sender, EventArgs e)
    //{
    //    if (!Manual.IsApproveWholeManual(ManualID))
    //    {
    //        lblMsg.Text = "Unable to releas new version. Some changes are not approved or applied.";
    //        return;
    //    }
    //    if (Manual.ApproveManual(ManualID.ToString()))
    //    {
    //        lblMsg.Text = "New Version Released successfully.";
    //    }
    //    else
    //    {
    //        lblMsg.Text = "Unable to approve.";
    //    }

    //}

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
        DataTable DT1 = Common.Execute_Procedures_Select_ByQuery("DELETE FROM DBO.SMS_FormsCategoryVesselMapping WHERE FormsCatId=" + FormsCatID.ToString() + ";UPDATE DBO.SMS_FormsCategoryMaster SET FormsCatName='" + txtFormsCatName.Text.Trim().Replace("'", "`") + "',ModifiedBy = "+ int.Parse(Session["loginid"].ToString()) + ", ModifiedOn = GETDATE()  WHERE FormsCatId=" + FormsCatID.ToString() + ";SELECT " + FormsCatID.ToString());
        if (DT1 != null)
            if (DT1.Rows.Count > 0)
            {
                foreach (ListItem li in chkVType.Items)
                {
                    if (li.Selected)
                    {
                        Common.Execute_Procedures_Select_ByQuery("INSERT INTO DBO.SMS_FormsCategoryVesselMapping (FormsCatId, VesselId)VALUES(" + FormsCatID.ToString() + "," + li.Value + ")");
                    }
                }
                BindGrid();
                BindManualVesselGrd();
            }
    }

    //----------------   Function
    public void BindManualVesselGrd()
    {
        string sql = " SELECT Row_Number() over(order by VESSELNAME) As RowNo,VesselName FROM DBO.VESSEL V Where V.VESSELSTATUSID<>2  " +
                     " And V.VESSELID IN (SELECT VesselId FROM DBO.SMS_FormsCategoryVesselMapping where FormsCatId =" + FormsCatID + ") " +
                     " ORDER BY V.VESSELNAME";
        DataTable DT = Common.Execute_Procedures_Select_ByQuery(sql);
        grdManualVessel.DataSource = DT;
        grdManualVessel.DataBind();
    }
    private void ShowRecord()
    {
        BindManualVesselGrd();
        //-----------------
        DataTable DT = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.SMS_FormsCategoryMaster where FormsCatId=" + FormsCatID.ToString());
        if (DT.Rows.Count > 0)
        {
            txtFormsCatName.Text = DT.Rows[0]["FormsCatName"].ToString();
           // txtversion.Text = DT.Rows[0]["VersionNo"].ToString();
        }
        chkVType.ClearSelection();
        DT = Common.Execute_Procedures_Select_ByQuery("SELECT VesselId FROM DBO.SMS_FormsCategoryVesselMapping where FormsCatId=" + FormsCatID.ToString());

        foreach (DataRow dr in DT.Rows)
        {
            ListItem li = chkVType.Items.FindByValue(dr["VesselId"].ToString());
            if (li != null)
                li.Selected = true;
        }

        p1.Visible = true;
        divFormsCatVesselGrid.Visible = true;
        btnSave.Visible = true;
        btnCancel.Visible = true;
    }
    protected void BindGrid()
    {
        RptFormsCategory.DataSource = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.SMS_FormsCategoryMaster order by FormsCatName");
        RptFormsCategory.DataBind();
    }
    protected void BindVesselType()
    {
        //chkVType.DataSource = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.VESSELTYPE ORDER BY VESSELTYPENAME");
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.VESSEL V with(nolock) Where V.VESSELSTATUSID<>2  ORDER BY V.VESSELNAME");
        chkVType.DataSource = dt;
        chkVType.DataBind();
    }
}