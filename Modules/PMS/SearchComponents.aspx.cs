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

public partial class SearchComponents : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // -------------------------- SESSION CHECK ----------------------------------------------
        ProjectCommon.SessionCheck();
        // -------------------------- SESSION CHECK END ----------------------------------------------

        lblMessage.Text = "";
        lblSearchRecords.Text = "";
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
            tdSearchResult.Visible = false;
        }

    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "tr", "SetLastFocus('dvScroll');", true);
    }
    protected void SearchComponent_Click(object sender, EventArgs e)
    {
        //string strSQL = "select * from componentmaster ";
        string strSQL = "SELECT CM.*,dbo.getRootPath(CM.COMPONENTCODE) as Parent FROM ComponentMaster CM ";

        string WhereClause = "WHERE 1=1 ";
        if (txtCompCode.Text.Trim() != "")
        {
            WhereClause = WhereClause + " AND CM.componentcode like '%" + txtCompCode.Text + "%'";

        }
        if (txtCopmpname.Text.Trim() != "")
        {
            WhereClause = WhereClause + " AND CM.componentname like '%" + txtCopmpname.Text + "%'";
        }
        if (chkClass.Checked)
        {
            WhereClause = WhereClause + " AND CM.ClassEquip ='" + chkClass.Checked + "'";
        }
        //if (chkSms.Checked)
        //{
        //    WhereClause = WhereClause + " AND CM.SmsEquip = '" + chkSms.Checked + "'";
        //}
        if (chkCritical.Checked)
        {
            WhereClause = WhereClause + " AND CM.CriticalEquip = '" + chkCritical.Checked + "'";
        }
        //if (chkCSM.Checked)
        //{
        //    WhereClause = WhereClause + " AND CM.CSMItem = '" + chkCSM.Checked + "'";
        //}
        if (chkInactive.Checked)
        {
            WhereClause = WhereClause + " AND CM.Inactive = '" + chkInactive.Checked + "'";
        }
        //if (chkInhParent.Checked)
        //{
        //    WhereClause = WhereClause + " AND CM.InheritParentJobs = '" + chkInhParent.Checked + "'";
        //}
        string strSearch = strSQL + WhereClause;
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(strSearch);
        if (dt != null)
            if (dt.Rows.Count > 0)
            {
                rptCompoenents.DataSource = dt;
                rptCompoenents.DataBind();
                if (dt.Rows.Count > 1)
                {
                    lblSearchRecords.Text = dt.Rows.Count + " Components Found.";
                }
                else
                {
                    lblSearchRecords.Text = dt.Rows.Count + " Component Found.";
                }
            }
            else
            {
                rptCompoenents.DataSource = null;
                rptCompoenents.DataBind();
                lblSearchRecords.Text = "No Component Found.";
            }
        tdSearchResult.Visible = true;
    }
    protected void btnComponent_Click(object sender, EventArgs e)
    {
        string CompCode = ((LinkButton)sender).CommandArgument;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "refresh('"+ CompCode + "');", true);
    }
}
