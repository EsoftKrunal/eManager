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
 
public partial class Transactions_ImageUpdate : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        if(!IsPostBack)
        {
            ShowFile();
        }
    }
    protected void ShowFile()
    {
        int TableId=Common.CastAsInt32(Request.QueryString["TableId"]);
        if(TableId>0)
        {
            DataTable dt = Common.Execute_Procedures_Select_ByQuery("select filepath,piccaption from InspReport_Child where tableid=" + TableId.ToString());
            if(dt!=null)
            if (dt.Rows.Count > 0)
            {
                string filepath = "~\\EMANAGERBLOB\\Inspection\\Transaction_Reports\\" + dt.Rows[0][0].ToString();
                Image1.ImageUrl = (new LinkButton()).ResolveClientUrl(filepath);
                ViewState["OldFile"] = filepath;  
                lblCaption.Text=  dt.Rows[0][1].ToString().Replace("''","'");  
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
        int TableId = Common.CastAsInt32(Request.QueryString["TableId"]);

        HttpPostedFile file1 = FileUpload1.PostedFile;
        UtilityManager um = new UtilityManager();
        string strfilename = FileUpload1.FileName;

        if (chk_FileExtension(Path.GetExtension(FileUpload1.FileName).ToLower()) == true)
        {
            string strFilePath = "EMANAGERBLOB/Inspection/Transaction_Reports/" + FileUpload1.FileName.Trim();
            string FileName1 = um.UploadFileToServer(file1, strfilename, "", "TR");
            if (FileName1.StartsWith("?"))
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('"  + FileName1.Substring(1) + "');", true);
                return;
            }
            else
            {
                if (File.Exists(Server.MapPath(ViewState["OldFile"].ToString())))
                {
                    File.Delete(Server.MapPath(ViewState["OldFile"].ToString()));
                }
                Budget.getTable("UPDATE InspReport_Child SET PICCAPTION='"  + txtCap.Text.Trim().Replace("'","''") + "',FILEPATH='" + FileName1 + "' WHERE TABLEID=" + TableId.ToString());
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
}
