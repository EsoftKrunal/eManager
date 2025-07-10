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

public partial class Popup_Ship_AddComponents : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // -------------------------- SESSION CHECK ----------------------------------------------
        ProjectCommon.SessionCheck();
        // -------------------------- SESSION CHECK END ----------------------------------------------
        if (!Page.IsPostBack)
        {
            lblMessage.Text = "";
            if (Request.QueryString["VC"] != null)
            {
                BindComponents();
            }
        }
    }

    public void BindComponents()
    {
        string strSQL = "";
        if (Request.QueryString["CC"].ToString() != "")
        {
            strSQL = "SELECT CM.*,(SELECT TOP 1 ComponentName FROM ComponentMaster CMP WHERE CMP.ComponentCode = LEFT(CM.COMPONENTCODE,LEN(CM.COMPONENTCODE)-3) ) as Parent " +
                     "FROM ComponentMaster CM WHERE ComponentId NOT IN ( SELECT ComponentId FROM VSL_ComponentMasterForVessel WHERE VesselCode = '" + Request.QueryString["VC"].ToString() + "' " +
                     "AND ComponentId IN (SELECT ComponentId FROM ComponentMaster WHERE LEFT(ComponentCode,LEN(RTRIM('" + Request.QueryString["CC"].ToString() + "'))) = '" + Request.QueryString["CC"].ToString() + "'))  AND LEFT(CM.ComponentCode,LEN(RTRIM('" + Request.QueryString["CC"].ToString() + "'))) = '" + Request.QueryString["CC"].ToString() + "'";

        }
        else
        {
            strSQL = "SELECT CM.*,(SELECT TOP 1 ComponentName FROM ComponentMaster CMP WHERE CMP.ComponentCode = LEFT(CM.COMPONENTCODE,LEN(CM.COMPONENTCODE)-3) ) as Parent " +
                     "FROM ComponentMaster CM WHERE ComponentId NOT IN (SELECT ComponentId FROM VSL_ComponentMasterForVessel WHERE VesselCode = '" + Request.QueryString["VC"].ToString() + "' " +
                     "AND ComponentId IN (SELECT ComponentId FROM ComponentMaster WHERE LEN(ComponentCode) = 3)) AND LEN(CM.ComponentCode) = 3 ";
        }
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(strSQL);
        if (dt != null)
            if (dt.Rows.Count > 0)
            {
                rptCompoenents.DataSource = dt;
                rptCompoenents.DataBind();
                lblMessage.Text = dt.Rows.Count + " Components found to add.";
            }
            else
            {
                rptCompoenents.DataSource = null;
                rptCompoenents.DataBind();
                lblMessage.Text = "No component found to add.";
            }
    }

    protected void btnAddComponents_Click(object sender, EventArgs e)
    {
        string CompCode = ((Button)sender).CommandArgument;

        try
        {
            Common.Set_Procedures("sp_Insert_Ship_Components_AutoSpecFromParent");
            Common.Set_ParameterLength(2);
            Common.Set_Parameters(
                new MyParameter("@VesselCode", Request.QueryString["VC"].ToString()),
                new MyParameter("@ComponentCode", CompCode.Trim())
                );

            DataSet dsComponent = new DataSet();
            dsComponent.Clear();
            Boolean res;
            res = Common.Execute_Procedures_IUD(dsComponent);
            if (res)
            {


                MessageBox1.ShowMessage("Component added Successfully.", false);
                BindComponents();
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "PP", "refresh('" + Request.QueryString["CC"].ToString() + "');", true);
            }
            else
            {
                MessageBox1.ShowMessage("Unable to add component.Error :" + Common.getLastError(), true);
            }
        }
        catch (Exception ex)
        {
            MessageBox1.ShowMessage("Unable to add component.Error :" + ex.Message + Common.getLastError(), true);
        }

    }
}
