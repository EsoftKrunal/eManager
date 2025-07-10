using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class emtm_Emtm_TravelDocs : System.Web.UI.Page
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
        lblMsg.Text = "";
        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 265);
        if (chpageauth <= 0)
        {
            Response.Redirect("../AuthorityError.aspx");
        }
        //*******************
        auth = new AuthenticationManager(265, Common.CastAsInt32(Session["loginid"]), ObjectType.Page);

        Session["CurrentModule"] = 4;
        Session["CurrentPage"] = 2;


        if (!Page.IsPostBack)
        {
            ControlLoader.LoadControl(ddlVesselType, DataName.VesselType, "Select", "");
            ShowRecord(SelectedId);
            setButtons("");
            BindGrid();
            BindExperianceGrid();
            lbl_EmpName.Text = Session["EmpName"].ToString();
        }
    }
    //-----------------------
    # region --- User Defined Functions ---
    //-- COMMON FUNCTIONS
    protected void setButtons(string Action)
    {
        //string EmpMode = Session["ProfileMode"].ToString();
        string EmpMode = Session["EmpMode"].ToString();
        

        if (EmpMode == "View")
        {
            switch (Action)
            {
                case "View":
                    tblview.Visible = true;
                    btnaddnew.Visible = false;
                    btnsave.Visible = false;
                    btncancel.Visible = true;
                    break;
                default:
                    tblview.Visible = false;
                    btnaddnew.Visible = false;
                    btnsave.Visible = false;
                    btncancel.Visible = false;
                    break;
            }
        }
        if (EmpMode == "Edit")
        {
            switch (Action)
            {
                case "View":
                    tblview.Visible = true;
                    divTraveldoc.Style.Add("height", "175px;");
                    DivTotExp.Style.Add("height", "175px;");

                    btnaddnew.Visible = false;
                    btnsave.Visible = false;
                    btncancel.Visible = true;
                    break;
                case "Add":
                    tblview.Visible = true;
                    divTraveldoc.Style.Add("height", "175px;");
                    DivTotExp.Style.Add("height", "175px;");

                    btnaddnew.Visible = false;
                    btnsave.Visible = true && auth.IsUpdate;
                    btncancel.Visible = true;
                    break;
                case "Edit":
                    tblview.Visible = true;
                    divTraveldoc.Style.Add("height", "175px;");
                    DivTotExp.Style.Add("height", "175px;");

                    btnaddnew.Visible = false;
                    btnsave.Visible = true && auth.IsUpdate;
                    btncancel.Visible = true;
                    break;
                default:
                    tblview.Visible = false;
                    divTraveldoc.Style.Add("height", "300px;");
                    DivTotExp.Style.Add("height", "300px;");

                    btnaddnew.Visible = true && auth.IsAdd;
                    btnsave.Visible = false;
                    btncancel.Visible = false;
                    break;
            }
        }
    }
    private void BindGrid()
    {
        int EmpId = Common.CastAsInt32(Session["EmpId"]);
        if (EmpId > 0)
        {
            string sql = "SELECT SE.ShipExpId " +
                            "  ,SE.EmpId " +
                             "  ,(select VesselTypeName from vesselType V where V.VesselTypeID=SE.VesselType)VesselType " +
                              " ,(case when SE.ExpType='1' then 'ShipBoard' else 'Shore Based' end)ExpTypeText,ExpType" +
                             "  ,SE.Experiance " +
                             "  ,SE.SignOffReason " +
                             "  ,SE.BHP " +
                          " FROM DBO.HR_ShipExperienceDetailsNew SE " +
                        " where SE.EmpId=" + EmpId.ToString() + "";

            //DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("select s.ShipExpId,s.EmpId,v.VesselTypeName as VesselType from HR_ShipExperienceDetails s left outer join vesselType v on s.VesselType=v.VesselTypeId left outer join Rank rnk on s.rank= rnk.RankId left outer join SignOffReason r on s.SignOffReason= r.SignOffReasonId WHERE  EMPID=" + EmpId.ToString());
            DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
            RptShipExpDoc.DataSource = dt;
            RptShipExpDoc.DataBind();
        }
    }
    public void BindExperianceGrid()
    {
        int EmpId = Common.CastAsInt32(Session["EmpId"]);
        string sql = "select (select VesselTypeName from vesselType V where V.VesselTypeID=SE.VesselType)VesselType ,sum(Experiance)as TotExp from DBO.HR_ShipExperienceDetailsNew SE where EmpID=" + EmpId.ToString() + " group by VesselType";
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        rptTotExp.DataSource = dt;
        rptTotExp.DataBind();
    }
    public void ShowRecord(int Id)
    {
        int EmpId = Common.CastAsInt32(Session["EmpId"]);
        if (EmpId > 0)
        {
            DataTable dtdata = Common.Execute_Procedures_Select_ByQueryCMS("select * from HR_ShipExperienceDetailsNew WHERE ShipExpId=" + Id.ToString());
            if (dtdata != null)
                if (dtdata.Rows.Count > 0)
                {
                    DataRow dr = dtdata.Rows[0];
                    ddlVesselType.SelectedValue = dr["VesselType"].ToString().Trim();
                    try
                    { ddlExpType.SelectedValue = dr["ExpType"].ToString().Trim(); }
                    catch (Exception ex)
                    { ddlExpType.SelectedIndex = 0; }

                    txtDuration.Text = dr["Experiance"].ToString().Trim();
                    txtResion.Text = dr["SignOffReason"].ToString().Trim();
                }
        }
    }
    protected void ClearControls()
    {
        ddlVesselType.SelectedIndex = 0;
    }
    #endregion

    #region --- Control Events ---
    protected void btnShipExpView_Click(object sender, ImageClickEventArgs e)
    {
        SelectedId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        BindGrid();
        ShowRecord(SelectedId);
        setButtons("View");
    }
    protected void btnShipExpedit_Click(object sender, ImageClickEventArgs e)
    {
        SelectedId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        BindGrid();
        ShowRecord(SelectedId);
        setButtons("Edit");
    }
    protected void btnShipExpDelete_Click(object sender, ImageClickEventArgs e)
    {
        SelectedId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        int EmpId = Common.CastAsInt32(Session["EmpId"]);
        if (EmpId > 0)
        {
            try
            {
                DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("delete from HR_ShipExperienceDetailsNew where ShipExpId=" + SelectedId);
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "error", "alert('Record Deleted Successfully');", true);
                BindGrid();
                BindExperianceGrid();
                setButtons("Cancel");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "error", "alert('Record Not Deleted');", true);
                return;
            }
        }
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {

        if (ddlVesselType.SelectedIndex == 0)
        {
            lblMsg.Text = "Please select Vessel Type.";
            ddlVesselType.Focus();           return;
        }
        if (ddlExpType.SelectedIndex == 0)
        {
            lblMsg.Text = "Please select Experience Type.";
            ddlVesselType.Focus(); return;
        }
        int EmpId = Common.CastAsInt32(Session["EmpId"]);
        Common.Set_Procedures("HR_InsertUpdateShipExperienceDetailsNew");
        Common.Set_ParameterLength(6);
        Common.Set_Parameters(
            new MyParameter("@ShipExpId", SelectedId),
            new MyParameter("@EmpId", EmpId),
            new MyParameter("@VesselType", ddlVesselType.SelectedValue.Trim()),
            new MyParameter("@ExpType", ddlExpType.SelectedValue.Trim()),
            new MyParameter("@Experiance", txtDuration.Text),
            new MyParameter("@SignOffReason", txtResion.Text)
            );
        DataSet ds = new DataSet();
        if (Common.Execute_Procedures_IUD_CMS(ds))
        {
            SelectedId = Common.CastAsInt32(ds.Tables[0].Rows[0][0]);
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Record saved successfully.');", true);
            BindGrid();
            setButtons("Cancel");
            BindExperianceGrid();
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Unable to save record.');", true);
        }
    }
    protected void btnaddnew_Click(object sender, EventArgs e)
    {
        SelectedId = 0;
        setButtons("Add");
        ClearControls();
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        SelectedId = 0;
        BindGrid();
        setButtons("Cancel");
    }
    #endregion
}

