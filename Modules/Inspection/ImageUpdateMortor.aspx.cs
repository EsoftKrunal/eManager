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
using ShipSoft.CrewManager.BusinessLogicLayer;
using ShipSoft.CrewManager.BusinessObjects;
using ShipSoft.CrewManager.Operational;
using System.IO;

public partial class Transactions_ImageUpdateMortor : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        if (!IsPostBack)
        {
            ShowFile();
        }
    }
    protected void ShowFile()
    {
        int MPDId = Common.CastAsInt32(Request.QueryString["MPDID"]);
        if (MPDId > 0)
        {
            DataTable dt = Common.Execute_Procedures_Select_ByQuery("select filepath,piccaption from MortorPicDetails where MPDId=" + MPDId.ToString());
            if (dt != null)
                if (dt.Rows.Count > 0)
                {
                    string filepath = "~\\EMANAGERBLOB\\Inspection\\Mortor\\" + dt.Rows[0][0].ToString();
                    Image1.ImageUrl = (new LinkButton()).ResolveClientUrl(filepath);
                    ViewState["OldFile"] = filepath;
                    lblCaption.Text = dt.Rows[0][1].ToString().Replace("''", "'");
                }
                else
                {
                    Image1.ImageUrl = (new LinkButton()).ResolveClientUrl("~\\Images\\Noimage.jpg");
                    ViewState["OldFile"] = "";
                }
        }
    }
    public bool chk_FileExtension(string str)
    {
        string extension = str;
        string MIMEType = null;
        switch (extension)
        {
            case ".jpg":
                return true;
            default:
                return false;
                break;
        }
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (txtCap.Text.Trim() == "")
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Please enter a caption for file.');", true);
            return;
        }
        if (!FileUpload1.HasFile)
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Please browse a file to upload(.jpg only).');", true);
            return;
        }
        int MPDId = Common.CastAsInt32(Request.QueryString["MPDID"]);

        HttpPostedFile file1 = FileUpload1.PostedFile;
        //UtilityManager um = new UtilityManager();
        string strfilename = FileUpload1.FileName;

        if (chk_FileExtension(Path.GetExtension(FileUpload1.FileName).ToLower()) == true)
        {
            string strFilePath = "EMANAGERBLOB/Inspection/Mortor/" + FileUpload1.FileName.Trim();
            //string FileName1 = um.UploadFileToServer(file1, strfilename, "", "TR");
            string FileName1 = UploadFileToServer(file1, strfilename, "", "TR");
            if (FileName1.StartsWith("?"))
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('" + FileName1.Substring(1) + "');", true);
                return;
            }
            else
            {
                if (File.Exists(Server.MapPath(ViewState["OldFile"].ToString())))
                {
                    File.Delete(Server.MapPath(ViewState["OldFile"].ToString()));
                }
                Budget.getTable("UPDATE MortorPicDetails SET PICCAPTION='" + txtCap.Text.Trim().Replace("'", "''") + "',FILEPATH='" + FileName1 + "' WHERE MPDId=" + MPDId.ToString());
                ShowFile();
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "success1", "RefreshParent();", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Please upload only .jpg files');", true);
            FileUpload1.Focus();
        }

    }
    public string UploadFileToServer(HttpPostedFile fileToUpload, string uploadedFileName, string existingFileName, string fileType)
    {
        // fileType D=Transaction Documents

        string uploadFileName = "";
        string path;
        string fullPath;

        // VIMS


        if (fileType == "TR") // Upload Transaction_Reports Documents
        {
            if (fileToUpload.ContentLength > (1024 * 1024 * 10))
            {
                uploadFileName = "?File Size is Too big! Maximum Allowed is 10 MB...";
            }
            else
            {
                // set the path and file
                uploadFileName = "2012_" + fileType + "_" + System.IO.Path.GetRandomFileName() + System.IO.Path.GetExtension(uploadedFileName);
                //uploadFileName = uploadedFileName.Trim();
                path = HttpContext.Current.Server.MapPath("~/EMANAGERBLOB/Inspection/Mortor/");
                fullPath = Path.Combine(path, uploadFileName);
                fileToUpload.SaveAs(fullPath);
                // delete old file if exists
                if (existingFileName.Trim().Length > 0)
                {
                    fullPath = path + existingFileName;
                    FileInfo fi = new FileInfo(fullPath);
                    if (fi.Exists)
                    {
                        fi.Delete();
                    }
                }
            }
        }


        return uploadFileName;
    }
}
