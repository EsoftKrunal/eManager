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

public partial class PopupHistoryAttachment : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // -------------------------- SESSION CHECK ----------------------------------------------
        ProjectCommon.SessionCheck();
        // -------------------------- SESSION CHECK END ----------------------------------------------
        if (!IsPostBack)
        {
            //if (Request.QueryString["Hid"].ToString() != "")
            //{
            //    ShowFile();
            //}

            if (Request.QueryString.GetKey(0) != null)
            {
                string Name = Request.QueryString.GetKey(0);
                switch (Name)
                {
                    case "Hid":
                        ShowFile();
                        break;
                    case "DRI":
                        ShowDefectAttachment();
                        break;
                    default:
                        break;
                }
            }
        }
            
    }
    private void ShowFile()
    {
        string strSQL = "SELECT [FileName],FileImage FROM VSL_VesselJobUpdateHistory " + "WHERE HistoryId=" + Request.QueryString["Hid"].ToString();
        DataTable dtFileDetails = Common.Execute_Procedures_Select_ByQuery(strSQL);
        byte[] buff = (byte[])dtFileDetails.Rows[0]["FileImage"];
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + dtFileDetails.Rows[0]["FileName"].ToString());
        Response.BinaryWrite(buff);
        Response.Flush();  
        Response.End(); 
    }
    private void ShowDefectAttachment()
    {
        string strSQL = "SELECT [FileName],[File] FROM VSL_DefectRemarks WHERE Vesselcode = '" + Request.QueryString["VC"].ToString() +"' AND DefectRemarkId = " + Request.QueryString["DRI"].ToString();
        DataTable dtRemarkDetails = Common.Execute_Procedures_Select_ByQuery(strSQL);
        byte[] buff = (byte[])dtRemarkDetails.Rows[0]["File"];
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + dtRemarkDetails.Rows[0]["FileName"].ToString());
        Response.BinaryWrite(buff);
        Response.Flush();
        Response.End();
    }
}
