using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class StaffAdmin_Hr_PositionMaster : System.Web.UI.Page
{
    DateTime ToDay;
    //User Defined Properties
    public int SelectedId
    {
        get
        {
            return Common.CastAsInt32(ViewState["SelectedId"]);
        }
        set
        {
            ViewState["SelectedId"] = value;
        }
    }
    # region --- User Defined Functions ---
    protected void setButtons(string Action)
    {
            switch (Action)
            {
                case "View":
                    divinfo.Style.Add("height", "200px;");

                    btnAddNew.Visible = false;
                    break;
                case "Add":
                    divinfo.Style.Add("height", "200px;");

                    btnAddNew.Visible = false;
                    break;
                case "Edit":
                    divinfo.Style.Add("height", "200px;");

                    btnAddNew.Visible = false;
                    break;
                default:
                    divinfo.Style.Add("height", "345px;");

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
    private void BindGrid(int OfficeId)
    {
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
    #endregion

    # region --- Control Events ---
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        Session["CurrentPage"] = 9;
        ToDay = DateTime.Today;
        if (!IsPostBack)
        {
            ControlLoader.LoadControl(ddlOffice, DataName.Office, "Select", "");
            
            setButtons("");
            BindGrid(Common.CastAsInt32(ddlOffice.SelectedValue.Trim()));
        }
    }
    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        SelectedId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
            try
            {
                DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("delete from Position where PositionId=" + SelectedId);
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "error", "alert('Record Deleted Successfully');", true);
                BindGrid(Common.CastAsInt32(ddlOffice.SelectedValue.Trim()));
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "error", "alert('Record Not Deleted');", true);
                return;
            }
    }
    protected void ddlOffice_SelectedIndexChanged(object sender, EventArgs e)
    {
       BindGrid(Common.CastAsInt32(ddlOffice.SelectedValue.Trim())); 
    }
    protected void btnhdn_Click(object sender, EventArgs e)
    {
        BindGrid(Common.CastAsInt32(ddlOffice.SelectedValue.Trim()));
    }
    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "PopUPWindowAdd('0','Add','" + Common.CastAsInt32(ddlOffice.SelectedValue.Trim()) + "');", true);
    }
    #endregion
}
