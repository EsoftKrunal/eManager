using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class Print_ShipMasterSpecification : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // -------------------------- SESSION CHECK ----------------------------------------------
        ProjectCommon.SessionCheck();
        // -------------------------- SESSION CHECK END ----------------------------------------------

        if (Session["sSqlForPrint"] != null)
        {
            DataTable dtSpec;
            string strSpecSQL = "";
            strSpecSQL = Session["sSqlForPrint"].ToString();

            Session.Add("sSqlForPrint", strSpecSQL);
            dtSpec = Common.Execute_Procedures_Select_ByQuery(strSpecSQL);
            if (dtSpec.Rows.Count > 0)
            {
                hfCompId.Value = dtSpec.Rows[0]["ComponentId"].ToString().Trim();
                lblComponentCode.Text = dtSpec.Rows[0]["ComponentCode"].ToString().Trim();
                lblLinkedto.Text = dtSpec.Rows[0]["LinkedTo"].ToString().Trim();
                lblComponentName.Text = dtSpec.Rows[0]["ComponentName"].ToString();
                lblComponentDesc.Text = dtSpec.Rows[0]["Descr"].ToString();
                lblMaker.Text = dtSpec.Rows[0]["Maker"].ToString();
                lblMakerType.Text = dtSpec.Rows[0]["MakerType"].ToString();
                
                //chkClass.Checked = Convert.ToBoolean(dtSpec.Rows[0]["ClassEquip"].ToString());
                lblClass.Text = ((dtSpec.Rows[0]["ClassEquip"].ToString() == "True") ? "✔" : "X");

                lblClassCode.Text = dtSpec.Rows[0]["ClassEquipCode"].ToString();

                
                //chkCritical.Checked = Convert.ToBoolean(dtSpec.Rows[0]["CriticalEquip"].ToString());
                lblCritical.Text = ((dtSpec.Rows[0]["CriticalEquip"].ToString() == "True") ? "✔" : "X");
                
                //chkInactive.Checked = Convert.ToBoolean(dtSpec.Rows[0]["Inactive"].ToString());
                lblInactive.Text = ((dtSpec.Rows[0]["Inactive"].ToString() == "True") ? "✔" : "X");
                
                
            }
            else
            {
            }
        }
    }
}
