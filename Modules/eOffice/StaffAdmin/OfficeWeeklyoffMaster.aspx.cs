using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class emtm_StaffAdmin_Emtm_OfficeWeeklyoffMaster : System.Web.UI.Page
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

    #region -- User Defined Functions
    protected void FillCurrentYear()
    {
        for (int i = ToDay.Year +1 ; i >= ToDay.Year - 5; i--)
        {
            ddlYear.Items.Add(i.ToString());
        }
    }
    protected void Bindgrid(int Year)
    {
        string sql = "select a.OfficeId,a.OfficeName, " + 
            " isnull(b.Sun,0) as Sun,isnull(b.Mon,0)as Mon,isnull(b.Tue,0)as Tue,isnull(b.Wed,0)as Wed,isnull(b.Thur,0)as Thur,isnull(b.Fri,0)as Fri,isnull(b.Sat,0)as Sat " +
            " from Office a Left outer join HR_OfficeWeeklyoffMaster b on a.Officeid=b.officeId and b.[Year]=" + ddlYear.SelectedValue.Trim();
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        rptWeeklyOff.DataSource = dt;
        rptWeeklyOff.DataBind();
    }
    #endregion

    # region --- Control Events ---
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        Session["CurrentPage"] = 3;
        ToDay = DateTime.Today;
        if (!IsPostBack)
        {
            FillCurrentYear();
            ddlYear.SelectedValue = ToDay.Year.ToString();  
            Bindgrid(Common.CastAsInt32(ddlYear.SelectedValue.Trim()));
        }
    }
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        Bindgrid(Common.CastAsInt32(ddlYear.SelectedValue.Trim()));
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        foreach (RepeaterItem rptJobItem in rptWeeklyOff.Items)
        {
            CheckBox chkSunday = (CheckBox)rptJobItem.FindControl("chkSunday");
            CheckBox chkMonday = (CheckBox)rptJobItem.FindControl("chkMonday");
            CheckBox chkTuesday = (CheckBox)rptJobItem.FindControl("chkTuesday");
            CheckBox chkWednesday = (CheckBox)rptJobItem.FindControl("chkWednesday");
            CheckBox chkThrusday = (CheckBox)rptJobItem.FindControl("chkThursday");
            CheckBox chkFriday = (CheckBox)rptJobItem.FindControl("chkFriday");
            CheckBox chkSaturday = (CheckBox)rptJobItem.FindControl("chkSaturday");

            HiddenField hdnOfficeId = (HiddenField)rptJobItem.FindControl("hdnOfficeId");

            Common.Set_Procedures("HR_InsertUpdateWeeklyOffMaster");
            Common.Set_ParameterLength(9);
            Common.Set_Parameters(new MyParameter("@OfficeId", hdnOfficeId.Value.Trim()),
                new MyParameter("@Year", ddlYear.SelectedValue.Trim()),
                new MyParameter("@Sunday", chkSunday.Checked),
                new MyParameter("@Monday", chkMonday.Checked),
                new MyParameter("@Tuesday", chkTuesday.Checked),
                new MyParameter("@Wednesday", chkWednesday.Checked),
                new MyParameter("@Thursday", chkThrusday.Checked),
                new MyParameter("@Friday", chkFriday.Checked),
                new MyParameter("@Saturday", chkSaturday.Checked));

            DataSet ds = new DataSet();
            try
            {
                if (Common.Execute_Procedures_IUD_CMS(ds))
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Record saved successfully.');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Unable To Save Record. Error :" + Common.getLastError() + "');", true);
                }
            }
            catch
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Holiday Already Exist');", true);
            }
        }
    }
    #endregion
}
