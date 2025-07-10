using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class emtm_StaffAdmin_Emtm_PopupRemark : System.Web.UI.Page
{
    public int PeapId
    {
        get
        {
            return Common.CastAsInt32(ViewState["PeapId"]);
        }
        set
        {
            ViewState["PeapId"] = value;
        }
    }
    public int JSID
    {
        get
        {
            return Common.CastAsInt32(ViewState["JSID"]);
        }
        set
        {
            ViewState["JSID"] = value;
        }
    }
    public int AID
    {
        get
        {
            return Common.CastAsInt32(ViewState["AID"]);
        }
        set
        {
            ViewState["AID"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        //lblMsg.Text = "";
        if (!IsPostBack)
        {
            if ((Request.QueryString["PID"] != null && Request.QueryString["PID"].ToString() != "") || (Request.QueryString["JSID"] != null && Request.QueryString["JSID"].ToString() != ""))
            {
                PeapId = Common.CastAsInt32(Request.QueryString["PID"].ToString());
                JSID = Common.CastAsInt32(Request.QueryString["JSID"].ToString());
                AID = Common.CastAsInt32(Request.QueryString["AID"].ToString());
                ShowRemark();
                
            }
        }
    }
    public void ShowRemark()
    {
        string strSQL = "SELECT Remark FROM HR_EmployeePeapJobResponsibility WHERE PeapId= " + PeapId + " AND  JSID =" + JSID + " AND  AppraiserByUser = " + AID + " ";

        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(strSQL);

        if (dt.Rows.Count > 0)
        {
            txtAnswerJR.Text = dt.Rows[0]["Remark"].ToString();
        }

        string strStatusSql = "SELECT [Status] FROM HR_EmployeePeapMaster WHERE PeapId = " + PeapId + " ";
        DataTable dtStatus = Common.Execute_Procedures_Select_ByQueryCMS(strStatusSql);

        //btnSave.Visible = Common.CastAsInt32(dtStatus.Rows[0]["Status"].ToString()) >= 3 ? false : true;


    }
    //protected void btnSave_Click(object sender, EventArgs e)
    //{
    //    if (txtAnswerJR.Text == "")
    //    {
    //        lblMsg.Text = "Please enter remark.";
    //        txtAnswerJR.Focus();
    //        return;
    //    }

    //    string SQL = "UPDATE HR_EmployeePeapJobResponsibility SET Remark = '" + txtAnswerJR.Text.Trim().Replace("'","''") + "' WHERE PeapId= " + PeapId + " AND  JSID =" + JSID + " AND  AppraiserByUser = " + AID + " ; SELECT -1 ";

    //    DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(SQL);

    //    if (dt.Rows.Count > 0)
    //    {
    //        lblMsg.Text = "Remark saved successfully.";
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "refresh", "refreshparent();", true);
    //    }
    //    else
    //    {
    //        lblMsg.Text = "Unable to save remark.";
    //    }
    //}
}