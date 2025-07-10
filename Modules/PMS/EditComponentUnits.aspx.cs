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

public partial class EditComponentUnits : System.Web.UI.Page
{
    public static string VesselCode = "";
    public static string CompIds = "";
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
            if (Request.QueryString["CompCode"] != null && Request.QueryString["VC"] != null && Request.QueryString["CompIds"] != null)
            {
                VesselCode = Request.QueryString["VC"].ToString();
                CompIds = Request.QueryString["CompIds"].ToString();
                BindComponents(Request.QueryString["CompCode"].ToString());
            }
        }

    }

    #region ------------- USER DEFINED FUNCTIONS --------------------
    private void BindComponents(string Id)
    {
        int intLenght = Id.Length;
        DataTable dtComponents = new DataTable();
        //string strSelectComponents = "SELECT  ComponentId,(select ltrim(rtrim(ComponentCode))+' : '+ componentName from ComponentMaster grp where grp.ComponentCode=left(cmp.ComponentCode,2) and len(grp.ComponentCode)=2) as MainGroup," +
        //                             "(select ltrim(rtrim(ComponentCode))+' : '+componentName from ComponentMaster sym where sym.ComponentCode=left(cmp.ComponentCode,4) and len(sym.ComponentCode)=4) as System," +
        //                             "ltrim(rtrim(ComponentCode))+' : '+ ComponentName as Component" +
        //                             " FROM ComponentMaster cmp WHERE LEN(ComponentCode) = 6 AND LEFT(ComponentCode," + intLenght + ") = '" + Id + "' ORDER BY ComponentCode";
        string strSelectComponents = "SELECT  cmp.ComponentId,ltrim(rtrim(ComponentCode))+' : '+ cmp.ComponentName as Component, " +
                                     "Count(cmp.ComponentName) over(partition by cmp.ComponentId)AS ExistingUnits FROM ComponentMaster cmp " +
                                     "INNER JOIN VesselComponentMaster VCM ON VCM.ComponentId = cmp.ComponentId " +
                                     "WHERE LEN(cmp.ComponentCode) = 6 AND cmp.ComponentId IN (" + CompIds + ") AND LEFT(ComponentCode," + intLenght + ") = '" + Id + "' AND VesselCode = '" + VesselCode + "' ORDER BY ComponentCode";

        dtComponents = Common.Execute_Procedures_Select_ByQuery(strSelectComponents);
        if (dtComponents.Rows.Count > 0)
        {
            string strCompId = dtComponents.Rows[0]["ComponentId"].ToString();
            for (int i = 0; i <= dtComponents.Rows.Count - 1; i++)
            {
                if (i == 0)
                {
                }
                else
                {
                    if (dtComponents.Rows[i]["ComponentId"].ToString() == strCompId)
                    {
                        dtComponents.Rows[i].Delete();
                    }
                    else
                    {
                        strCompId = dtComponents.Rows[i]["ComponentId"].ToString();
                    }
                }
            }
            dtComponents.AcceptChanges();
        }
        if (dtComponents.Rows.Count > 0)
        {
            rptComponents.DataSource = dtComponents;
            rptComponents.DataBind();
            lblComponent.Text = dtComponents.Rows[0]["Component"].ToString();
        }
        else
        {
            rptComponents.DataSource = null;
            rptComponents.DataBind();
        }
    }
    private Boolean IsValidated()
    {
        foreach (RepeaterItem rptItem in rptComponents.Items)
        {
            TextBox txtUnits = (TextBox)rptItem.FindControl("txtUnits");
            Label lblExtUnits = (Label)rptItem.FindControl("lblExtUnits");
            if (Common.CastAsInt32(txtUnits.Text.Trim()) == 0)
            {
                lblMessage.Text = "Please enter valid Units.";
                txtUnits.Focus();
                return false;
            }
            //if (Common.CastAsInt32(txtUnits.Text.Trim()) > Common.CastAsInt32(lblExtUnits.Text.Trim()))
            //{
            //    lblMessage.Text = "Units can not be greater than existing units.";
            //    txtUnits.Focus();
            //    return false;
            //}
            if (Common.CastAsInt32(txtUnits.Text.Trim()) < 0)
            {
                if (Common.CastAsInt32(txtUnits.Text.Trim()) < Common.CastAsInt32("-"+lblExtUnits.Text.Trim()))
                {
                    lblMessage.Text = "Units can not be less than existing units to edit.";
                    txtUnits.Focus();
                    return false;
                }
            }
        }
        return true;
    }
    #endregion

    #region ------------- EVENTS ------------------------------------
    protected void btnAddCompUnits_Click(object sender, EventArgs e)
    {
        if (!IsValidated())
        {
            return;
        }
        string componentId = "";
        string Units = "";
        string[] components = lblComponent.Text.ToString().Split(':');
        string Compcode = components[0].Trim();
        foreach (RepeaterItem rptItem in rptComponents.Items)
        {
            HiddenField hfdComponentId = (HiddenField)rptItem.FindControl("hfComponentId");            
            TextBox txtUnits = (TextBox)rptItem.FindControl("txtUnits");
            if (txtUnits.Text != "")
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
                lblMessage.Text = "Component Units Edited Successfully.";
                BindComponents(Compcode);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "refreshonedit();", true);
            }
            else
            {
                lblMessage.Text = "Unable to Edit Units.Error :" + Common.getLastError();
            }
        }
    }
    #endregion
}
