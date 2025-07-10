using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class emtm_Registers : System.Web.UI.Page
{
    public int SelectedId
    {
        get {return Common.CastAsInt32(hfdId1.Value); }
        set { hfdId1.Value=value.ToString();}
    }
    public int SelectedId2
    {
        get { return Common.CastAsInt32(hfdId2.Value); }
        set { hfdId2.Value = value.ToString(); }
    }
    # region --- User Defined Functions ---
    protected void setButtons(string Action)
    {
        switch (Action)
        {
            case "View":
                btnAddNew.Visible = false;
                break;
            case "Add":
                btnAddNew.Visible = false;
                break;
            case "Edit":
                btnAddNew.Visible = false;
                break;
            default:
                btnAddNew.Visible = true;
                break;
        }
    }
    public void ShowRecord(int Id)
    {
        DataTable dtdata = Common.Execute_Procedures_Select_ByQueryCMS("select PositionId,OfficeId,PositionCode,PositionName,PosLevel,IsManager,IsInspector from Position where PositionId =" + Id);
        if (dtdata != null)
            if (dtdata.Rows.Count > 0)
            {
                DataRow dr = dtdata.Rows[0];
                ddlOffice.SelectedValue = dr["OfficeId"].ToString().Trim();
            }
    }
    private void BindGrid()
    {
        int OfficeId = Common.CastAsInt32(ddlOffice.SelectedValue.Trim());
        if (OfficeId > 0)
        {
            DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("SELECT *,ManagerText=(case when isnull(IsManager,0)=1 then 'YES' else 'NO' end),InspectorText=(case when isnull(IsInspector,0)=1 then 'YES' else 'NO' end),(select PositionName from DBO.VesselPositions where VPId=VesselPositions) as VesselPositionName  FROM POSITION P INNER JOIN OFFICE O ON O.OFFICEID=P.OFFICEID WHERE P.OFFICEID=" + OfficeId + " ORDER BY POSITIONNAME");
            rptHolidayMaster.DataSource = dt;
            rptHolidayMaster.DataBind();
        }
        else
        {
            DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("SELECT *,ManagerText=(case when isnull(IsManager,0)=1 then 'YES' else 'NO' end),InspectorText=(case when isnull(IsInspector,0)=1 then 'YES' else 'NO' end),(select PositionName from DBO.VesselPositions where VPId=VesselPositions) as VesselPositionName FROM POSITION P INNER JOIN OFFICE O ON O.OFFICEID=P.OFFICEID ORDER BY O.OFFICENAME,POSITIONNAME");
            rptHolidayMaster.DataSource = dt;
            rptHolidayMaster.DataBind();
        }
    }
    private void BindGrid2()
    {

        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("SELECT * FROM VesselPositions ORDER BY POSITIONNAME");
        rptPositionGroup.DataSource = dt;
        rptPositionGroup.DataBind();
       
    }
    private void BindGrid3()
    {

        if (ddlPosGroup.SelectedIndex > 0)
        {

            DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("SELECT * FROM POSITION P INNER JOIN OFFICE O ON O.OFFICEID=P.OFFICEID WHERE isnull(VesselPositions,0)<>" + ddlPosGroup.SelectedValue + " and P.positionid in (select position from Hr_PersonalDetails p where p.drc is null) ORDER BY OFFICENAME,POSITIONNAME ");
            rpt_RemainingPositions.DataSource = dt;
            rpt_RemainingPositions.DataBind();


            dt = Common.Execute_Procedures_Select_ByQueryCMS("SELECT * FROM POSITION P INNER JOIN OFFICE O ON O.OFFICEID=P.OFFICEID WHERE VesselPositions=" + ddlPosGroup.SelectedValue + " ORDER BY OFFICENAME,POSITIONNAME ");
            rpt_Linked.DataSource = dt;
            rpt_Linked.DataBind();
        }
        else
        {
            rpt_RemainingPositions.DataSource = null;
            rpt_RemainingPositions.DataBind();

            rpt_Linked.DataSource = null;
            rpt_Linked.DataBind();
        }
    }
    #endregion

    # region --- Control Events ---
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        lblMessage1.Text = "";
        if (!IsPostBack)
        {
            ControlLoader.LoadControl(ddlOffice, DataName.Office, "Select", "");
            ControlLoader.LoadControl(ddlOffice1, DataName.Office, "Select", "");
            setButtons("");
            
            ddlVesselPosition.DataSource = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.VesselPositions ORDER BY PositionName");
            ddlVesselPosition.DataTextField = "PositionName";
            ddlVesselPosition.DataValueField = "VPId";
            ddlVesselPosition.DataBind();
            ddlVesselPosition.Items.Insert(0, new ListItem(" < Select > ", "0"));


            ddlPosGroup.DataSource = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.VesselPositions ORDER BY PositionName");
            ddlPosGroup.DataTextField = "PositionName";
            ddlPosGroup.DataValueField = "VPId";
            ddlPosGroup.DataBind();
            ddlPosGroup.Items.Insert(0, new ListItem(" < Select > ", "0"));


            BindGrid();
            BindGrid2();
            //BindGrid3();
        }
    }

    
    protected void ddlOffice_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGrid();
    }
    protected void btnhdn_Click(object sender, EventArgs e)
    {
        BindGrid();
    }
    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "PopUPWindowAdd('0','Add','" + Common.CastAsInt32(ddlOffice.SelectedValue.Trim()) + "');", true);
    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        //ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "PopUPWindowAdd('0','Add','" + Common.CastAsInt32(ddlOffice.SelectedValue.Trim()) + "');", true);
        if (txtPosName.Text.Trim() == "")
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Please enter Position Name.');", true);
            return;
        }

        Common.Set_Procedures("HR_InsertUpdatePositionMaster");
        Common.Set_ParameterLength(7);
        Common.Set_Parameters(new MyParameter("@PositionId", SelectedId),
            new MyParameter("@OfficeId", ddlOffice1.SelectedValue),
            new MyParameter("@PositionCode", txtPosCode.Text.Trim()),
            new MyParameter("@PositionName", txtPosName.Text.Trim()),
            new MyParameter("@IsManager", radmangaer.SelectedValue),
            new MyParameter("@IsInspector", radInspector.SelectedValue),
            new MyParameter("@VesselPositions", ddlVesselPosition.SelectedValue)
            );

        DataSet ds = new DataSet();
        try
        {
            if (Common.Execute_Procedures_IUD_CMS(ds))
            {
                lblMessage1.Text = "Record saved successfully.";
            }
            else
            {
                lblMessage1.Text = "Position already exits." + Common.ErrMsg;
            }
        }
        catch
        {
            lblMessage1.Text = "Position already exits.";
        }
    }
    protected void btnReload1_Click(object sender, EventArgs e)
    {
        BindGrid();
    }
    protected void btnReload11_Click(object sender, EventArgs e)
    {
        if (SelectedId > 0)
        {
            DataTable dtdata = Common.Execute_Procedures_Select_ByQueryCMS("select * from Position where PositionId =" + SelectedId);
            if (dtdata != null)
                if (dtdata.Rows.Count > 0)
                {
                    DataRow dr = dtdata.Rows[0];
                    ddlOffice1.SelectedValue = Common.CastAsInt32(dr["OfficeId"]).ToString();
                    txtPosCode.Text = dr["PositionCode"].ToString();
                    txtPosName.Text = dr["PositionName"].ToString();
                    ddlVesselPosition.SelectedValue = Common.CastAsInt32(dr["VesselPositions"]).ToString();
                    if (Convert.IsDBNull(dr["IsManager"]))
                    {
                        radmangaer.SelectedIndex = 1;
                    }
                    else
                    {
                        if (Convert.ToBoolean(dr["IsManager"].ToString()))
                        {
                            radmangaer.SelectedIndex = 0;
                        }
                        else
                        {
                            radmangaer.SelectedIndex = 1;
                        }
                    }


                    if (Convert.IsDBNull(dr["IsInspector"]))
                    {
                        radInspector.SelectedIndex = 1;
                    }
                    else
                    {
                        if (Convert.ToBoolean(dr["IsInspector"].ToString()))
                        {
                            radInspector.SelectedIndex = 0;
                        }
                        else
                        {
                            radInspector.SelectedIndex = 1;
                        }
                    }
                }
        }
        else
        {
            ddlOffice1.SelectedIndex = 0;
            txtPosCode.Text = "";
            txtPosName.Text = "";
            radmangaer.SelectedIndex = 0;
            radInspector.SelectedIndex = 0;
            ddlVesselPosition.SelectedIndex = 0;
        }
    }
    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        int Key = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        try
        {
            DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("delete from Position where PositionId=" + Key);
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "error", "alert('Record Deleted Successfully');", true);
            BindGrid();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "error", "alert('Record Not Deleted');", true);
            return;
        }
    }

    protected void btnsave2_Click(object sender, EventArgs e)
    {
        if (txtPosgrpName.Text.Trim() == "")
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Please enter Position Group Name.');", true);
            return;
        }

        Common.Set_Procedures("HR_InsertUpdatePositionGroup");
        Common.Set_ParameterLength(2);
        Common.Set_Parameters(new MyParameter("@VPId", SelectedId2),
            new MyParameter("@PositionName", txtPosgrpName.Text.Trim()));

        DataSet ds = new DataSet();
        try
        {
            if (Common.Execute_Procedures_IUD_CMS(ds))
            {
                lblMessage2.Text = "Record saved successfully.";
            }
            else
            {
                lblMessage2.Text = "Position Group already exits.";
            }
        }
        catch
        {
            lblMessage2.Text = "Position Group already exits.";
        }
    }
    protected void btnReload2_Click(object sender, EventArgs e)
    {
        BindGrid2();
    }
    protected void btnReload21_Click(object sender, EventArgs e)
    {
        if (SelectedId2 > 0)
        {
            DataTable dtdata = Common.Execute_Procedures_Select_ByQueryCMS("select * from VesselPositions where VPid =" + SelectedId2);
            if (dtdata != null)
                if (dtdata.Rows.Count > 0)
                {
                    DataRow dr = dtdata.Rows[0];
                    txtPosgrpName.Text = dr["PositionName"].ToString();
                }
        }
        else
        {
            txtPosgrpName.Text = "";
        }
    }
    protected void btnVPDelete_Click(object sender, ImageClickEventArgs e)
    {
        int Key = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        try
        {
            DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("delete from VesselPositions where VPId=" + Key);
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "error", "alert('Record Deleted Successfully');", true);
            BindGrid2();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "error", "alert('Record Not Deleted');", true);
            return;
        }
    }

    protected void ddlPosGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGrid3();
    }
    protected void btnAssign_Click(object sender, EventArgs e)
    {
        foreach (RepeaterItem ri in rpt_RemainingPositions.Items)
        {
            CheckBox ch = (CheckBox)ri.FindControl("chkSel");
            if (ch.Checked)
            {
                Common.Execute_Procedures_Select_ByQuery("UPDATE DBO.POSITION SET VesselPositions=" + ddlPosGroup.SelectedValue + " WHERE PositionId=" + ch.ToolTip);
            }
        }
        BindGrid3();
    }
    protected void btnRemove_Click(object sender, EventArgs e)
    {
        foreach (RepeaterItem ri in rpt_Linked.Items)
        {
            CheckBox ch = (CheckBox)ri.FindControl("chkSel");
            if (ch.Checked)
            {
                Common.Execute_Procedures_Select_ByQuery("UPDATE DBO.POSITION SET VesselPositions=NULL WHERE PositionId=" + ch.ToolTip);
            }
        }
        BindGrid3();
    }
    
    #endregion
}