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
using System.IO;

public partial class emtm_MyProfile_Emtm_PopupAttachment : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        if (!IsPostBack)
        {
            if (Request.QueryString["expid"] != null)
            {
                int Expid = Common.CastAsInt32(Request.QueryString["expid"].ToString());
                ShowFile(Expid);
               
            }
        }

    }

    private void ShowFile(int Expid)
    {
        string strSQL = "SELECT [FileName],Attachment FROM HR_OfficeAbsence_Expense WHERE ExpenseId=" + Expid.ToString();
        DataTable dtFileDetails = Common.Execute_Procedures_Select_ByQueryCMS(strSQL);
        byte[] buff = (byte[])dtFileDetails.Rows[0]["Attachment"];
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + dtFileDetails.Rows[0]["FileName"].ToString());
        Response.BinaryWrite(buff);
        Response.Flush();
        Response.End();
    }
}