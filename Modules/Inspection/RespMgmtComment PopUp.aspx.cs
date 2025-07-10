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
using System.Text.RegularExpressions; 

public partial class Transactions_RespMgmtComment_PopUp : System.Web.UI.Page
{
    string RespText = "";
    int ObservationId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        try
        {
            lblmessage.Text = "";
            //RespText = Page.Request.QueryString["RspMgtText"].ToString();
            ObservationId = int.Parse(Page.Request.QueryString["ObsvId"].ToString());
            //RespText = Session["RespMgmtText"].ToString();
            if (!Page.IsPostBack)
            {
                // old code
                //--------------------------------------
                //DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT isnull(RESPONSE,'') FROM t_Observations WHERE ID=" + ObservationId.ToString());  
                //if(dt.Rows.Count >0)
                //    txtRespComment.Text = dt.Rows[0][0].ToString().Replace("`", "'");
                // new code
                //--------------------------------------
                DataTable dtQues = Inspection_Response.ResponseDetails(ObservationId, 0, "", "", 0, 0, 0, 0, 0, "", "", "QUESBYOID", "", "", "", "");
                if (dtQues.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtQues.Rows)
                    {
                        txtRespComment.Text = dr["Response"].ToString().Replace("`", "'");
                    }
                }
            }
        }
        catch { }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string s;
            s = Regex.Replace(txtRespComment.Text.Trim(), @"[^\u0000-\u007F]", "");
            Inspection_Response.ResponseDetails(ObservationId, int.Parse(Session["Insp_Id"].ToString()), "", s.Replace("'","`"), 0, 0, 0, 0, 0, "", "", "MODIFY", "N", "", "", "N");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "a", "SetParent();", true);    
            
            lblmessage.Text = "Response Saved Successfully.";
            btnSave.Enabled = false;
        }
        catch (Exception ex) { lblmessage.Text = ex.StackTrace.ToString(); }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        txtRespComment.Text = "";
        btnSave.Enabled = true;
    }
}
