using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Emtm_Profile_CompanyHoliday : System.Web.UI.Page
{
    public AuthenticationManager auth;
    //-----------------------
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        if (!Page.IsPostBack)
        {
            BindGrid(Common.CastAsInt32(Request.QueryString["Office"]),DateTime.Today.Year);
        }
    }
    //-----------------------
    # region --- User Defined Functions ---
    //-- MTM EXPERIENCE
    private void BindGrid(int OfficeId, int Year)
    {
        DataTable dt=Common.Execute_Procedures_Select_ByQueryCMS("select OfficeName from office where OfficeId=" + OfficeId.ToString());
        if (dt.Rows.Count > 0)
            lblPageheader.Text = dt.Rows[0][0].ToString() + " - " + Year.ToString();
        //------------------
        dt = Common.Execute_Procedures_Select_ByQueryCMS("select a.HolidayId,a.OfficeId,b.OfficeName,a.[Year],replace(convert(varchar,a.Holidayfrom,106),' ','-') as FromDate,replace(convert(varchar,a.HolidayTo,106),' ','-') as ToDate, a.HolidayReason from HR_HolidayMaster a left outer join office b on a.OfficeId= b.OfficeId where a.[Year]=" + Year.ToString() + " and  a.OfficeId= " + OfficeId.ToString() + " ORDER BY a.Holidayfrom");
        rptHoliday.DataSource = dt;
        rptHoliday.DataBind();
    }
    #endregion
    //-----------------------
 }
