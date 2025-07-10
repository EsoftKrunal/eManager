using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class emtm_StaffAdmin_Emtm_Hr_HolidayMaster : System.Web.UI.Page
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
        DataTable dtdata = Common.Execute_Procedures_Select_ByQueryCMS("select HolidayId,OfficeId, [Year],replace(convert(varchar,Holidayfrom,106),' ','-') as FromDate,replace(convert(varchar,HolidayTo,106),' ','-') as ToDate, HolidayReason from HR_HolidayMaster where HolidayId =" + Id);
        if (dtdata != null)
            if (dtdata.Rows.Count > 0)
            {
                DataRow dr = dtdata.Rows[0];
                ddlOffice.SelectedValue = dr["OfficeId"].ToString().Trim();
                ddlYear.SelectedValue = dr["Year"].ToString().Trim();
            }
    }
    private void BindGrid(int OfficeId)
    {
        if (OfficeId > 0)
        {
            if (Common.CastAsInt32(ddlYear.SelectedValue) > 0)
            {
                DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("select a.HolidayId,a.OfficeId,b.OfficeName,a.[Year],replace(convert(varchar,a.Holidayfrom,106),' ','-') as FromDate,replace(convert(varchar,a.HolidayTo,106),' ','-') as ToDate, a.HolidayReason from HR_HolidayMaster a left outer join office b on a.OfficeId= b.OfficeId where a.OfficeId=" + OfficeId + "  and a.[year]= "+ ddlYear.SelectedValue.Trim());
                rptHolidayMaster.DataSource = dt;
                rptHolidayMaster.DataBind();
            }
            else
            {
                DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("select a.HolidayId,a.OfficeId,b.OfficeName,a.[Year],replace(convert(varchar,a.Holidayfrom,106),' ','-') as FromDate,replace(convert(varchar,a.HolidayTo,106),' ','-') as ToDate, a.HolidayReason from HR_HolidayMaster a left outer join office b on a.OfficeId= b.OfficeId where a.OfficeId=" + OfficeId);
                rptHolidayMaster.DataSource = dt;
                rptHolidayMaster.DataBind();
            }
        }
        else
        {
            DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("select a.HolidayId,a.OfficeId,b.OfficeName,a.[Year],replace(convert(varchar,a.Holidayfrom,106),' ','-') as FromDate,replace(convert(varchar,a.HolidayTo,106),' ','-') as ToDate, a.HolidayReason from HR_HolidayMaster a left outer join office b on a.OfficeId= b.OfficeId");
            rptHolidayMaster.DataSource = dt;
            rptHolidayMaster.DataBind();
        }
    }
    private void BindGrid(int OfficeId,int Year)
    {
        if (Year > 0)
        {
            if (Common.CastAsInt32(ddlOffice.SelectedValue) > 0)
            {
                DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("select a.HolidayId,a.OfficeId,b.OfficeName,a.[Year],replace(convert(varchar,a.Holidayfrom,106),' ','-') as FromDate,replace(convert(varchar,a.HolidayTo,106),' ','-') as ToDate, a.HolidayReason from HR_HolidayMaster a left outer join office b on a.OfficeId= b.OfficeId where a.[Year]=" + Year + " and  a.OfficeId= " + ddlOffice.SelectedValue.Trim());
                rptHolidayMaster.DataSource = dt;
                rptHolidayMaster.DataBind();
            }
            else
            {
                DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("select a.HolidayId,a.OfficeId,b.OfficeName,a.[Year],replace(convert(varchar,a.Holidayfrom,106),' ','-') as FromDate,replace(convert(varchar,a.HolidayTo,106),' ','-') as ToDate, a.HolidayReason from HR_HolidayMaster a left outer join office b on a.OfficeId= b.OfficeId where a.[Year]=" + Year);
                rptHolidayMaster.DataSource = dt;
                rptHolidayMaster.DataBind();
            }
        }
        else
        {
            DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("select a.HolidayId,a.OfficeId,b.OfficeName,a.[Year],replace(convert(varchar,a.Holidayfrom,106),' ','-') as FromDate,replace(convert(varchar,a.HolidayTo,106),' ','-') as ToDate, a.HolidayReason from HR_HolidayMaster a left outer join office b on a.OfficeId= b.OfficeId");
            rptHolidayMaster.DataSource = dt;
            rptHolidayMaster.DataBind();
        }
    }
    protected void FillCurrentYear()
    {
        for (int i = ToDay.Year +1; i >= ToDay.Year - 5; i--)
        {
            ddlYear.Items.Add(i.ToString());
        }
    }
    #endregion

    # region --- Control Events ---
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        Session["CurrentPage"] = 2;
        ToDay = DateTime.Today;
        if (!IsPostBack)
        {
            ControlLoader.LoadControl(ddlOffice, DataName.Office, "Select", "");
            FillCurrentYear();
            ddlYear.SelectedValue = ToDay.Year.ToString();   
            setButtons("");
            BindGrid(Common.CastAsInt32(ddlOffice.SelectedValue.Trim()), Common.CastAsInt32(ddlYear.SelectedValue.Trim()));
        }
    }
    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        SelectedId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
            try
            {
                DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("delete from HR_HolidayMaster where HolidayId=" + SelectedId);
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
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGrid(Common.CastAsInt32(ddlOffice.SelectedValue),Common.CastAsInt32(ddlYear.SelectedValue));   
    }
    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "PopUPWindowAdd('0','Add','" + Common.CastAsInt32(ddlOffice.SelectedValue.Trim()) + "','"+ Common.CastAsInt32(ddlYear.SelectedValue.Trim()) +"');", true);
    }
    #endregion
}
