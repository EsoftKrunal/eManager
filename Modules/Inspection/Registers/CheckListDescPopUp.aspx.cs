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

public partial class Registers_CheckListDescPopUp : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        int ChkId = Convert.ToInt32(Page.Request.QueryString["ChkLstId"].ToString());
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT Question,Description,isnull(OfficeRemarks,'') as OfficeRemarks FROM m_Questions WHERE ID=" + ChkId.ToString());
        lblG.Text= dt.Rows[0]["Description"].ToString().Replace("\n","<br />");
        lblO.Text = dt.Rows[0]["OfficeRemarks"].ToString().Replace("\n", "<br />");

        pnlO.Visible=((""+Request.QueryString["Mode"])=="O");
        pnlG.Visible = !pnlO.Visible;
    }
}
