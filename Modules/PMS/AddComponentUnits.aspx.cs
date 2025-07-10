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

public partial class AddComponentUnits : System.Web.UI.Page
{
    public static string VesselCode = "";
    public static string CompId = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        // -------------------------- SESSION CHECK ----------------------------------------------
        ProjectCommon.SessionCheck();
        // -------------------------- SESSION CHECK END ----------------------------------------------
        lblMessage.Text = "";
        #region --------- USER RIGHTS MANAGEMENT -----------
        try
        {
            AuthenticationManager auth = new AuthenticationManager(206, int.Parse(Session["loginid"].ToString()), ObjectType.Page);
            if (!(auth.IsView))
            {
                Response.Redirect("~/NoPermission.aspx", false);
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("~/NoPermission.aspx?Message=" + ex.Message);
        }
        #endregion ----------------------------------------

        if (!Page.IsPostBack)
        {
            if (Request.QueryString["CompCode"] != null && Request.QueryString["VC"] != null)
            {
                VesselCode = Request.QueryString["VC"].ToString();
                GetComponent();
                BindComponents(Request.QueryString["CompCode"].ToString());
            }
        }
    }
    #region ------------- USER DEFINED FUNCTIONS --------------------
    private void BindComponents(string Id)
    {
        int intLenght = Id.Length;
        DataTable dtComponents = new DataTable();
        string strSelectComponents = "SELECT * FROM ( SELECT ROW_NUMBER() OVER(PARTITION BY CM.ComponentId ORDER BY CM.ComponentId) AS RANK," +
                                     "CM.ComponentId,ltrim(rtrim(CM.ComponentCode))+' : '+ CM.ComponentName as Component " +
                                     "FROM ComponentMaster CM INNER JOIN VesselComponentMaster VCM ON CM.ComponentId != VCM.ComponentId " +
                                     "WHERE LEN(CM.ComponentCode) = 6 AND LEFT(CM.ComponentCode," + intLenght + ") = '" + Id + "' AND VCM.VesselCode='" + VesselCode + "') AS COMP " +
                                     "WHERE COMP.RANK = 1 AND COMP.ComponentId NOT IN (SELECT ComponentId from VesselComponentMaster where VesselCode='" + VesselCode + "')";

        dtComponents = Common.Execute_Procedures_Select_ByQuery(strSelectComponents);
        if (dtComponents.Rows.Count > 0)
        {
            rptComponents.DataSource = dtComponents;
            rptComponents.DataBind();
        }
        else
        {
            DataTable dt = new DataTable();
            DataColumn dc;
            dc = new DataColumn("ComponentId");
            dt.Columns.Add(dc);
            dc = new DataColumn("Component");            
            dt.Columns.Add(dc);           
            dt.Rows.Add(CompId, lblComponent.Text.ToString());
            rptComponents.DataSource = dt;
            rptComponents.DataBind();
        }
    }
    private void GetComponent()
    {
        DataTable dtCompId = new DataTable();
        string strSQL = "SELECT ComponentId,ComponentCode,ComponentName FROM ComponentMaster WHERE ComponentCode = '" + Request.QueryString["CompCode"].ToString().Trim() + "' ";
        dtCompId = Common.Execute_Procedures_Select_ByQuery(strSQL);
        CompId = dtCompId.Rows[0]["ComponentId"].ToString();
        lblComponent.Text = dtCompId.Rows[0]["ComponentCode"].ToString() + " : " + dtCompId.Rows[0]["ComponentName"].ToString();
    }
    private Boolean IsValidated()
    {
        foreach (RepeaterItem rptItem in rptComponents.Items)
        {
            TextBox txtUnits = (TextBox)rptItem.FindControl("txtUnits");

            if (Common.CastAsInt32(txtUnits.Text.Trim()) < 0)
            {
                lblMessage.Text = "Please enter valid Units.";
                txtUnits.Focus();
                return false;
            }
        }
        return true;
    }
    #endregion

    #region ------------- EVENTS ------------------------------------
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "tr", "SetLastFocus('dvScroll');", true);
    }
    protected void btnAddCompUnits_Click(object sender, EventArgs e)
    {
        if (!IsValidated())
        {
            return;
        }
        string componentId = "";
        string Units = "";
        foreach (RepeaterItem rptItem in rptComponents.Items)
        {
            HiddenField hfdComponentId = (HiddenField)rptItem.FindControl("hfComponentId");
            Label lblComponent = (Label)rptItem.FindControl("lblComponent");
            TextBox txtUnits = (TextBox)rptItem.FindControl("txtUnits");
            if (Common.CastAsInt32(txtUnits.Text) > 0)
            {
                componentId = componentId + hfdComponentId.Value + ",";
                Units = Units + txtUnits.Text.Trim() + ",";
            }
        }
        if (componentId != "" || Units != "")
        {
            string strComponentIds = componentId.Remove(componentId.Length - 1, 1);
            string strUnits = Units.Remove(Units.Length - 1, 1);
            Common.Set_Procedures("sp_InsertGenerateVesselComponents");
            Common.Set_ParameterLength(3);
            Common.Set_Parameters(
                new MyParameter("@VesselCode", VesselCode),
                new MyParameter("@ComponentIds", strComponentIds),
                new MyParameter("@Units", strUnits)
                );
            DataSet dsComponents = new DataSet();
            dsComponents.Clear();
            Boolean res;
            res = Common.Execute_Procedures_IUD(dsComponents);
            if (res)
            {
                lblMessage.Text = "Component Units Generated Successfully.";
            }
            else
            {
                lblMessage.Text = "Unable to Generate Componenet Units.Error :" + Common.getLastError();
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "", "refreshonadd();", true);
        }
    }
    #endregion
}
