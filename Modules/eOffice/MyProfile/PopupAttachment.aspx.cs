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

public partial class PopupAttachment : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        if (!IsPostBack)
        {
            if (Request.QueryString.GetKey(0) != null)
            {
                string Name= Request.QueryString.GetKey(0);  
                switch (Name)
                {
                    case "TvlDocID":
                        ShowTravelDocFile();
                        break;
                    case "TvlVisaID" :
                        ShowTravelVisaFile();
                        break;
                    case "CertifctDocId" :
                        ShowCertificateDocs();
                        break;
                    case "MedicalDocId":
                        ShowMedicalDocs();
                        break;
                    case "OtherDocId":
                        ShowOtherDocs();
                        break;

                    default :
                        break;
                }
            }
        }
    }

    #region ---- User Defined Function -----
    private void ShowTravelDocFile()
    {
        string strSQL = "SELECT [FileName],FileImage FROM HR_TravelDocumentDetails " + "WHERE TravelDocId=" + Request.QueryString["TvlDocID"].ToString();
        DataTable dtFileDetails = Common.Execute_Procedures_Select_ByQueryCMS(strSQL);
        byte[] buff = (byte[])dtFileDetails.Rows[0]["FileImage"];
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + dtFileDetails.Rows[0]["FileName"].ToString());
        Response.BinaryWrite(buff);
        Response.Flush();  
        Response.End(); 
    }
    private void ShowTravelVisaFile()
    {
        string strSQL = "SELECT [FileName],FileImage FROM HR_TravelVisaDetails " + "WHERE TravelVisaId=" + Request.QueryString["TvlVisaID"].ToString();
        DataTable dtFileDetails = Common.Execute_Procedures_Select_ByQueryCMS(strSQL);
        byte[] buff = (byte[])dtFileDetails.Rows[0]["FileImage"];
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + dtFileDetails.Rows[0]["FileName"].ToString());
        Response.BinaryWrite(buff);
        Response.Flush();
        Response.End();
    }
    private void ShowCertificateDocs()
    {
        string strSQL = "SELECT [FileName],FileImage FROM HR_CertificateDocumentDetails " + "WHERE CertificateDocId=" + Request.QueryString["CertifctDocId"].ToString();
        DataTable dtFileDetails = Common.Execute_Procedures_Select_ByQueryCMS(strSQL);
        byte[] buff = (byte[])dtFileDetails.Rows[0]["FileImage"];
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + dtFileDetails.Rows[0]["FileName"].ToString());
        Response.BinaryWrite(buff);
        Response.Flush();
        Response.End();
    }
    private void ShowMedicalDocs()
    {
        string strSQL = "SELECT [FileName],FileImage FROM HR_MedicalDocsDetails " + "WHERE MedicalDocId=" + Request.QueryString["MedicalDocId"].ToString();
        DataTable dtFileDetails = Common.Execute_Procedures_Select_ByQueryCMS(strSQL);
        byte[] buff = (byte[])dtFileDetails.Rows[0]["FileImage"];
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + dtFileDetails.Rows[0]["FileName"].ToString());
        Response.BinaryWrite(buff);
        Response.Flush();
        Response.End();
    }
    private void ShowOtherDocs()
    {
        string strSQL = "SELECT [FileName],FileImage FROM HR_OtherDocsDetails " + "WHERE OtherDocId=" + Request.QueryString["OtherDocId"].ToString();
        DataTable dtFileDetails = Common.Execute_Procedures_Select_ByQueryCMS(strSQL);
        byte[] buff = (byte[])dtFileDetails.Rows[0]["FileImage"];
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + dtFileDetails.Rows[0]["FileName"].ToString());
        Response.BinaryWrite(buff);
        Response.Flush();
        Response.End();
    }
    #endregion
}
