using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class Transactions_UploadObservationDocs : System.Web.UI.Page
{
    public int DocID
    {
        set { ViewState["DocID"] = value; }
        get { return Common.CastAsInt32(ViewState["DocID"]); }
    }
    public int ObservationId
    {
        set { ViewState["ObservationId"] = value; }
        get { return Common.CastAsInt32(ViewState["ObservationId"]); }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        if (!IsPostBack)
        {
            ObservationId =Common.CastAsInt32(Request.QueryString["ObservationId"]);
            BindDocs();
        }
    }
    //PopUp Documents
    
    protected void btnSaveDoc_Click(object sender, EventArgs e)
    {
        if (DocID == 0)
            if (!flAttachDocs.HasFile)
            {
                ScriptManager.RegisterStartupScript(this,this.GetType(),"fd","alert('Please select a file.');",true); 
                flAttachDocs.Focus();
                return;
            }
        FileUpload img = (FileUpload)flAttachDocs;
        Byte[] imgByte = new Byte[0];
        string FileName = "";

        if (img.HasFile && img.PostedFile != null)
        {
            HttpPostedFile File = flAttachDocs.PostedFile;

            if (Path.GetExtension(File.FileName).ToString() == ".pdf" || Path.GetExtension(File.FileName).ToString() == ".txt" || Path.GetExtension(File.FileName).ToString() == ".doc" || Path.GetExtension(File.FileName).ToString() == ".docx" || Path.GetExtension(File.FileName).ToString() == ".xls" || Path.GetExtension(File.FileName).ToString() == ".xlsx" || Path.GetExtension(File.FileName).ToString() == ".ppt" || Path.GetExtension(File.FileName).ToString() == ".pptx")
            {
                FileName = "Observation" + "_" + DateTime.Now.ToString("dd-MMM-yyyy") + "_" + DateTime.Now.TimeOfDay.ToString().Replace(":", "-").ToString() + Path.GetExtension(File.FileName);


                string path = "../EMANAGERBLOB/Inspection/Observation/";
                flAttachDocs.SaveAs(Server.MapPath(path) + FileName);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "fd", "alert('File type not supported. Only pdf and microsoft office files accepted.');", true); 
                
            }


        }
        string strSQL = "EXEC sp_InsertUpdatet_ObservationDocs " + DocID + "," + ObservationId + ",'" + txt_Desc.Text.Trim().Replace("'", "`") + "','" + FileName + "','" + Convert.ToInt32(Session["loginid"].ToString()) + "'";
        DataTable dtDocs = Budget.getTable(strSQL).Tables[0];
        if (dtDocs.Rows.Count > 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "fd", "alert('Record Successfully Saved.');", true); 
            txt_Desc.Text = "";
            BindDocs();
            DocID = 0;
        }
    }
    protected void imgEditDoc_OnClick(object sender, EventArgs e)
    {
        ImageButton ImgEdit = (ImageButton)sender;
        HiddenField hfDocID = (HiddenField)ImgEdit.Parent.FindControl("hfDocID");
        DocID = Common.CastAsInt32(hfDocID.Value);
        Label lblDesc = (Label)ImgEdit.Parent.FindControl("lblDesc");
        txt_Desc.Text = lblDesc.Text;

    }
    protected void imgDelDoc_OnClick(object sender, EventArgs e)
    {
        ImageButton ImgDel = (ImageButton)sender;
        HiddenField hfDocID = (HiddenField)ImgDel.Parent.FindControl("hfDocID");
        DocID = Common.CastAsInt32(hfDocID.Value);

        string sql = "delete from t_ObservationDocs  where DocID=" + hfDocID.Value + "";
        DataSet dtGroups = Budget.getTable(sql);
        BindDocs();
        DocID = 0;
    }
    public void BindDocs()
    {
        string strSQL = "select replace(convert(varchar,UploadedOn ,106) ,' ','-')UploadedDate,(select Firstname+' '+Lastname from dbo.UserLogin UL where UL.LoginID= CS.UploadedBy)UploadedBy,* from t_ObservationDocs CS where ObservationID=" + ObservationId;
        DataTable dtDocs = Budget.getTable(strSQL).Tables[0];
        if (dtDocs.Rows.Count > 0)
        {
            rptDocs.DataSource = dtDocs;
            rptDocs.DataBind();
        }
        else
        {
            rptDocs.DataSource = null;
            rptDocs.DataBind();
        }
    }
    public string GetPSCCodeByID(string PSCID)
    {
        try
        {
            string sql = "select PscCode from dbo.m_psccode Where ID =" + PSCID;
            DataTable Dt = Common.Execute_Procedures_Select_ByQuery(sql);
            if (Dt.Rows.Count > 0)
            {
                return Dt.Rows[0][0].ToString();
            }
            else
                return "";
        }
        catch
        {
            return "";
        }
    }
}