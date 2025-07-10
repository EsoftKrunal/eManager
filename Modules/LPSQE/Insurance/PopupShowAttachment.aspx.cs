using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using ShipSoft.CrewManager.BusinessObjects;
using ShipSoft.CrewManager.BusinessLogicLayer;
using ShipSoft.CrewManager.Operational;
using System.IO;

public partial class InsuranceRecordManagement_PopupShowAttachment : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        if (!IsPostBack)
        {
            if (Request.QueryString["DId"].ToString() != "")
            {
                ShowFile();
            }
        }
    }

    private void ShowFile()
    {
        string strSQL = "SELECT [FileName],Attachment FROM IRM_PolicyDocuments WHERE DocId=" + Request.QueryString["DId"].ToString();
        DataTable dtFileDetails = Budget.getTable(strSQL).Tables[0];
        byte[] buff = (byte[])dtFileDetails.Rows[0]["Attachment"];
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + dtFileDetails.Rows[0]["FileName"].ToString());
        Response.BinaryWrite(buff);
        Response.Flush();
        Response.End();
    }
}
