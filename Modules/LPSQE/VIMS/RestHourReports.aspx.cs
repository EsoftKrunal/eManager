using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

public partial class Vims_RestHourReports : System.Web.UI.Page
{
    AuthenticationManager Auth;
    public string CurrentShip
    {
        get { return ViewState["CurrentShip"].ToString(); }
        set { ViewState["CurrentShip"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        if (!IsPostBack)
        {
            CurrentShip=Session["CurrentShip"].ToString();
            ProjectCommon.LoadMonth(ddlMonth);
            ProjectCommon.LoadYear(ddlYear);
           
        }
    }

    protected void ddlMonthYear_Changed(object sender, EventArgs e)
    {
        loadcrew();
    }
    public void loadcrew()
    {
       // DataTable dtcrew = Common.Execute_Procedures_Select_ByQuery("SELECT count(*) from RH_NCList WHERE VESSELCODE='" + CurrentShip + "' AND MONTH(FORDATE)=" + ddlMonth.SelectedValue + " AND YEAR(FORDATE)=" + ddlYear.SelectedValue + " AND LTRIM(RTRIM(ISNULL(REMARKS,'')))=''");
       // int NCwithoutRemarks = Common.CastAsInt32(dtNCwithoutRemarks.Rows[0][0]);

    }
    protected void btnprint_Click(object sender, EventArgs e)
    {

    }
    protected void btnShow_Click(object sender, EventArgs e)
    {

    }

}

