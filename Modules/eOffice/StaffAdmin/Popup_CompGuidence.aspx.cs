using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class emtm_StaffAdmin_Emtm_Popup_CompGuidence : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        if (!IsPostBack)
        {
            if(Request.QueryString["CID"] != null && Request.QueryString["CID"].ToString() != "")
            {
                ShowGuidenance(Common.CastAsInt32(Request.QueryString["CID"].ToString()));
            }

        }
    }

    public void ShowGuidenance (int CID)
    {
        string strSQL = "SELECT Competency,Descr AS Descr FROM HR_Competency WHERE CID = " + CID + " ";

        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(strSQL);

        if (dt.Rows.Count > 0)
        {
            lblComp.Text = dt.Rows[0]["Competency"].ToString();
            lblGuidence.Text = dt.Rows[0]["Descr"].ToString();
        }


    }
}