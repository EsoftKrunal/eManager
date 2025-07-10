using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Configuration;

public partial class BudgetCommentReport : System.Web.UI.Page
{
    public string SessionDeleimeter = ConfigurationManager.AppSettings["SessionDelemeter"];
    DataSet DsValue;
    protected void Page_Load(object sender, EventArgs e)
    {
        //---------------------------------------
        ProjectCommon.SessionCheck();
        //---------------------------------------
        //AuthenticationManager authRecInv;
        #region --------- USER RIGHTS MANAGEMENT -----------
        //try
        //{
        //    authRecInv = new AuthenticationManager(234, int.Parse(Session["loginid"].ToString()), ObjectType.Page);
        //    if (!(authRecInv.IsView))
        //    {
        //        Response.Redirect("~/NoPermission.aspx", false);
        //    }
        //}
        //catch (Exception ex)
        //{
        //    Response.Redirect("~/NoPermission.aspx?Message=" + ex.Message);
        //}

        #endregion ----------------------------------------
        
        try
        {
            if (!Page.IsPostBack)
            {
                BindYear();
                SetMonth();

                BindCompany();
                BindVessel(ddlCompany.SelectedValue);
            }

        }
        catch { }
    }
    // Event ----------------------------------------------------------------

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Search.aspx");
    }
    // Button
    protected void imgPrint_Click(object sender, EventArgs e)
    {
        if (ddlCompany.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "alert('Please select Company.')", true);
        }
        if (ddlVessel.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "b", "alert('Please select Vessel.')", true);
        }

        //if (ddlReportLevel.SelectedIndex == 4)
        //{
        //    string Query = "";
        //    Query = ddlyear.SelectedValue + "~" + ddlMonth.SelectedValue + "~" + ddlCompany.SelectedItem + "~" + ddlVessel.SelectedItem + "~" + ddlCompany.SelectedValue + "~" + ddlVessel.SelectedValue + "~Account Details~4";
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Print", "window.open('PrintVarianceReport.aspx?Query=" + Query + "');", true);
        //}

        //else if (ddlReportLevel.SelectedIndex == 1)
        //{
        //    int VessNum = GetVesselNum();
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Printt", "window.open('../Print.aspx?CoCode=" + ddlCompany.SelectedValue + "&VessNum=" + VessNum + "&CompName=" + ddlCompany.SelectedItem + "&VesselName=" + ddlVessel.SelectedItem + "&Month=" + ddlMonth.SelectedValue + "&Year=" + ddlyear.SelectedValue + "');", true);
        //    ddlReportLevel.SelectedIndex = 0;
        //    //Response.Redirect("Print.aspx?CoCode=" + ddlCompany.SelectedValue + "&VessNum=" + VessNum + "&CompName=" + ddlCompany.SelectedItem + "&VesselName=" + ddlVessel.SelectedItem + "&Month=" + ddlMonth.SelectedValue + "&Year=" + ddlyear.SelectedValue + "");
        //}
        //else if (ddlReportLevel.SelectedIndex == 2)
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Printtt", "window.open('../Print.aspx?LumpSum=1&CoCode=" + ddlCompany.SelectedValue + "&Period=" + ddlMonth.SelectedValue + "&Year=" + ddlyear.SelectedValue + "&VesselName=" + ddlVessel.SelectedItem + "&CompanyName=" + ddlCompany.SelectedItem.Text + "&VessCode=" + ddlVessel.SelectedValue + "');", true);
        //    ddlReportLevel.SelectedIndex = 0;
        //}
         
        
        
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Printtttt", "window.open('../Print.aspx?Comment=1&CoCode=" + ddlCompany.SelectedValue + "&Period=" + ddlMonth.SelectedValue + "&Year=" + ddlyear.SelectedValue + "&VesselName=" + ddlVessel.SelectedItem + "&CompanyName=" + ddlCompany.SelectedItem.Text + "&VessCode=" + ddlVessel.SelectedValue + "');", true);
        

        //if (ddlReportLevel.SelectedIndex == 4)
        //{
        //    string QueryInn = "";
        //    QueryInn = ddlyear.SelectedValue + "~" + ddlMonth.SelectedValue + "~" + ddlCompany.SelectedItem + "~" + ddlVessel.SelectedItem + "~" + ddlCompany.SelectedValue + "~" + ddlVessel.SelectedValue + "~Account Details~4";
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Print", "window.open('../PrintVarianceReport.aspx?Query=" + QueryInn + "');", true);
        //}
        //else
        //{
        //    string Query = "";
        //    if (ddlVessel.SelectedIndex == 0)
        //        Query = ddlyear.SelectedValue + "~" + ddlMonth.SelectedValue + "~" + ddlCompany.SelectedItem + "~" + "All" + "~" + ddlCompany.SelectedValue + "~" + ddlVessel.SelectedValue;
        //    else
        //        Query = ddlyear.SelectedValue + "~" + ddlMonth.SelectedValue + "~" + ddlCompany.SelectedItem + "~" + ddlVessel.SelectedItem + "~" + ddlCompany.SelectedValue + "~" + ddlVessel.SelectedValue + "~" + ddlReportLevel.SelectedItem + "~" + ddlReportLevel.SelectedIndex;
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Print", "window.open('../PrintVarianceReport.aspx?Query=" + Query + "');", true);
        //}
    }

    public void BindCompany()
    {
        string sql = "selECT cmp.Company, cmp.[Company Name] as CompanyName, cmp.Active, cmp.InAccts FROM vw_sql_tblSMDPRCompany cmp WHERE (((cmp.Active)='Y'))";
        DataTable DtCompany = Common.Execute_Procedures_Select_ByQuery(sql);
        if (DtCompany != null)
        {
            if (DtCompany.Rows.Count > 0)
            {
                ddlCompany.DataSource = DtCompany;
                ddlCompany.DataTextField = "Company";
                ddlCompany.DataValueField = "Company";
                ddlCompany.DataBind();
                ddlCompany.Items.Insert(0, new ListItem("<Select>", ""));
            }
        }
        //if (Session["NWC"].ToString() == "Y")
        //{
        //    ddlCompany.SelectedValue = "NWC";
        //    ddlCompany.Enabled = false;
        //}

    }
    public void BindVessel(string Comp)
    {
        string sql = "SELECT vsl.ShipID, vsl.ShipName, vsl.Company, vsl.VesselNo, vsl.Active FROM vw_sql_tblSMDPRVessels vsl WHERE (((vsl.Active)='A' ) and vsl.Company='" + Comp + "'";
        DataTable DtVessel = Common.Execute_Procedures_Select_ByQuery(sql);
        if (DtVessel != null)
        {
            ddlVessel.DataSource = DtVessel;
            ddlVessel.DataTextField = "ShipName";
            ddlVessel.DataValueField = "ShipID";
            ddlVessel.DataBind();
            ddlVessel.Items.Insert(0, new ListItem("<All>", ""));
        }

    }
    public void BindYear()
    {
        //ddlyear.Items.Add(new ListItem("Select","0"));
        for (int i = System.DateTime.Now.Year, j = 1; ; i--, j++)
        {
            ddlyear.Items.Add(i.ToString());
            if (j == 11)
                break;
        }
    }
    public void SetMonth()
    {
        ddlMonth.SelectedValue = (Common.CastAsInt32(System.DateTime.Now.Month) - 1).ToString();
    }

    protected void ddlCompany_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        string sql = "selECT cmp.[Company Name] as CompanyName FROM vw_sql_tblSMDPRCompany cmp where Company='" + ddlCompany.SelectedValue + "'";
        DataTable DtName = Common.Execute_Procedures_Select_ByQuery(sql);
        if (DtName != null)
        {
            if (DtName.Rows.Count > 0)
            {
                ddlCompany.ToolTip = DtName.Rows[0][0].ToString();
            }
        }
        BindVessel(ddlCompany.SelectedValue);
    }
    public int GetVesselNum()
    {
        string sql = "SELECT Vesselno FROM vw_sql_tblSMDPRVessels where shipid='" + ddlVessel.SelectedValue + "'";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
        if (dt != null)
        {
            if (dt.Rows.Count > 0)
            {
                return Common.CastAsInt32(dt.Rows[0][0]);
            }
        }
        return 0;
    }



    
}


