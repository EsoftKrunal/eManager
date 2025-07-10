using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class StaffAdmin_HR_Experiance : System.Web.UI.Page
{
    public AuthenticationManager auth;
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
    //-----------------------
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 248);
        if (chpageauth <= 0)
        {
            Response.Redirect("../AuthorityError.aspx");
        }
        //*******************
        auth = new AuthenticationManager(248, Common.CastAsInt32(Session["loginid"]), ObjectType.Page);

        Session["CurrentModule"] = 4;
        Session["CurrentPage"] = 1;

        if (!Page.IsPostBack)
        {
            if (Session["EmpMode"].ToString() == "Add")
            {
                Response.Redirect("~/EMTM/StaffAdmin/HR_Personaldetail.aspx");
            }
            setButtons("");
            BindGrid_MTMExp();
            BindGrid_OtherExp();
            lbl_EmpName.Text = Session["EmpName"].ToString();
            btnaddnew.Visible = true && auth.IsUpdate; 
        }
    }
    //-----------------------
    # region --- User Defined Functions ---
    //-- COMMON FUNCTIONS
    protected void setButtons(string Action)
    {

    }
    //-- MTM EXPERIENCE
    private void BindGrid_MTMExp()
    {
        int EmpId = Common.CastAsInt32(Session["EmpId"]);
        if (EmpId > 0)
        {
            DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("select a.MtmOfficeExpId, a.EmpId, a.OfficeID, c.OfficeName,p.positionName as designation , replace(convert(varchar,a.FromDate,106),' ','-') as FromDate ,replace(convert(varchar,a.ToDate,106),' ','-') as ToDate from  HR_OfficeExperienceDetails a left outer join office c on a.officeId= c.officeid left outer join position p on p.positionId=a.designation WHERE EMPID=" + EmpId.ToString() + " order by a.fromdate desc");
            rptmtmexp.DataSource= dt;
            rptmtmexp.DataBind();
        }
    }
    //-- OTHER EXPERIENCE
    private void BindGrid_OtherExp()
    {
        int EmpId = Common.CastAsInt32(Session["EmpId"]);
        if (EmpId > 0)
        {
            DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("select ShoreId,EmpId,Company,Position,replace(convert(varchar,FromDate,106),' ','-') as FromDate ,replace(convert(varchar,ToDate,106),' ','-') as ToDate ,Location from HR_ShoreDetails WHERE EMPID=" + EmpId.ToString() + " order by HR_ShoreDetails.FromDate desc");
            rptotherexp.DataSource = dt;
            rptotherexp.DataBind();
        }
    }
    #endregion
    //-----------------------
    #region --- Control Events ---
    //-- MTM EXPERIENCE
    protected void btnmtmaddnew_Click(object sender, EventArgs e)
    {
        setButtons("Add");
    }
    protected void btnmtmcancel_Click(object sender, EventArgs e)
    {
        SelectedId = 0;
        BindGrid_MTMExp(); 
        setButtons("Cancel"); 
    }
    protected void btnMtmDelete_Click(object sender, EventArgs e)
    {
        SelectedId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);

        int EmpId = Common.CastAsInt32(Session["EmpId"]);
        if (EmpId > 0)
        {
            try
            {
                DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("delete from HR_OfficeExperienceDetails where MtmOfficeExpId=" + SelectedId);
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "error", "alert('Record Deleted Successfully');", true);
                BindGrid_MTMExp(); 
            }
            catch(Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "error", "alert('Record Not Deleted');", true);
                return;
            }
        }
    }
    //-- OTHER EXPERIENCE
    protected void btnhdn_Click(object sender, EventArgs e)
    {
        BindGrid_MTMExp();
        BindGrid_OtherExp();
    }
    protected void btnotherDelete_Click(object sender, EventArgs e)
    {
        SelectedId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        int EmpId = Common.CastAsInt32(Session["EmpId"]);
        if (EmpId > 0)
        {
            try
            {
                DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("delete from HR_ShoreDetails where ShoreId=" + SelectedId);
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "error", "alert('Record Deleted Successfully');", true);
                BindGrid_OtherExp();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "error", "alert('Record Not Deleted');", true);
                return;
            }
        }
    }
    #endregion
 }
