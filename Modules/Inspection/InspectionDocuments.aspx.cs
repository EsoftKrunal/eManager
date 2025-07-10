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
using ShipSoft.CrewManager.BusinessLogicLayer;
using ShipSoft.CrewManager.BusinessObjects;
using ShipSoft.CrewManager.Operational;

public partial class Transactions_InspectionDocuments : System.Web.UI.Page
{
    /// <summary>
    /// Page Name            : InspectionDocuments.aspx
    /// Purpose              : This is the Documents page
    /// Author               : Shobhita
    /// Developed on         : 30-Oct-2009
    /// </summary>
    //Public Properties
    
    #region "Declarations"
    public static Random R = new Random();
    Authority Auth;
    int intLogin_Id;
    int intInspection_Id;
    public Boolean GridStatus = true;
    int id;
    #endregion

    #region "Page Load"
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        //--------------------------------- PAGE ACCESS AUTHORITY ----------------------------------------------------
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 1053);
        if (chpageauth <= 0)
            Response.Redirect("blank.aspx");
        //------------------------------------------------------------------------------------------------------------
        this.Form.DefaultButton = this.btn_Children.UniqueID.ToString();
        lblmessage.Text = "";
        if (Session["loginid"] == null)
        {
            //Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "window.parent.parent.location='../Default.aspx'", true);
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Login.aspx';", true);
        }
        else
        {
            intLogin_Id = Convert.ToInt32(Session["loginid"].ToString());
        }
        //if (Session["Mode"].ToString() != "Add")
        //{
            if (Session["Insp_Id"] == null) { Session.Add("PgFlag", 1); Response.Redirect("InspectionSearch.aspx"); }
        //}
        //else { if (Session["Insp_Id"] == null) { Session.Add("NwInspFlag", 2); Response.Redirect("InspectionSearch.aspx"); } }
        try { intInspection_Id = int.Parse(Session["Insp_Id"].ToString()); }
        catch { }
        if (Session["loginid"] != null)
        {
            ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 7);
            OBJ.Invoke();
            Session["Authority"] = OBJ.Authority;
            Auth = OBJ.Authority;
        }
        if (Page.IsPostBack == false)
        {
            try
            {
                Alerts.HANDLE_AUTHORITY(8, null, btn_Children, null, btn_Print, Auth);
            }
            catch
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Login.aspx';", true);
            }
            bindDocTypeDDL();
            //bindBlankGrid();
            if (Session["Insp_Id"] != null)
            {
                BindDocumentGrid();
               // Show_Header_Record(intInspection_Id);
            }
            else
            {
                GridStatus = false;
                bindBlankGrid();
            }
            //******Accessing UserOnBehalf/Subordinate Right******
            try
            {
                if (Session["Insp_Id"] != null)
                {
                    int useronbehalfauth = Alerts.UserOnBehalfRight(Convert.ToInt32(Session["loginid"].ToString()), Convert.ToInt32(Session["Insp_Id"].ToString()));
                    if (useronbehalfauth <= 0)
                    {
                        btn_Children.Enabled = false;
                        foreach (GridViewRow gr in Grid_Document.Rows)
                        {
                            ImageButton imgbtnedit = (ImageButton)gr.FindControl("ImageButton2");
                            ImageButton imgbtndel = (ImageButton)gr.FindControl("ImageButton1");
                            imgbtnedit.Enabled = false;
                            imgbtndel.Enabled = false;
                        }
                    }
                    else
                    {
                        btn_Children.Enabled = true;
                        foreach (GridViewRow gr in Grid_Document.Rows)
                        {
                            ImageButton imgbtnedit = (ImageButton)gr.FindControl("ImageButton2");
                            ImageButton imgbtndel = (ImageButton)gr.FindControl("ImageButton1");
                            imgbtnedit.Enabled = true;
                            imgbtndel.Enabled = true;
                        }
                    }
                }
            }
            catch
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Login.aspx';", true);
            }
            //****************************************************
        }
    }
    #endregion

    #region "User Defined Functions"
    //Show Observation Records By InspectionDueId
    //protected void Show_Header_Record(int intInspectionId)
    //{
    //    DataTable dt1 = Inspection_TravelSchedule.SelectInspectionDetailsByInspId(intInspectionId);
    //    if (dt1.Rows.Count > 0)
    //    {
    //        foreach (DataRow dr in dt1.Rows)
    //        {
    //            txt_InspNo.Text = dr["InspectionNo"].ToString();
    //            txt_VslName.Text = dr["VesselName"].ToString();
    //            txt_InspName.Text = dr["Name"].ToString();
    //            txt_Port.Text = dr["PortDone"].ToString();
    //            txt_DtDone.Text = dr["DoneDt"].ToString();
    //            txt_InspctrName.Text = dr["InspectorName"].ToString();
    //            txt_Supt.Text = dr["Supt"].ToString();
    //            txt_Status.Text = dr["Status"].ToString();
    //            //Inspector = dr["Inspector"].ToString();
    //            //txtCreatedBy_TravelShd.Text = dr["Created_By"].ToString();
    //            //txtCreatedOn_TravelShd.Text = dr["Created_On"].ToString();
    //            //txtModifiedBy_TravelShd.Text = dr["Modified_By"].ToString();
    //            //txtModifiedOn_TravelShd.Text = dr["Modified_On"].ToString();
    //        }
    //    }
    //    //DataTable dt11 = Inspection_Observation.UpdateInspectionObservation(Inspection_Id, InspectionNo, Master, ChiefOfficer, SecondOffice, ChiefEngineer, AssistantEngineer, Inspector, ResponseDueDate, ActualLocation, ActualDate, QuestionId, Deficiency, Comment, HighRisk, NCR, "SELECT", Login_Id, Login_Id);
    //    //if (dt11.Rows.Count > 0)
    //    //{
    //    //    foreach (DataRow dr1 in dt11.Rows)
    //    //    {
    //    //        txtCreatedBy_DocumentType.Text = dr1["Created_By"].ToString();
    //    //        txtCreatedOn_DocumentType.Text = dr1["Created_On"].ToString();
    //    //        txtModifiedBy_DocumentType.Text = dr1["Modified_By"].ToString();
    //    //        txtModifiedOn_DocumentType.Text = dr1["Modified_On"].ToString();
    //    //    }
    //    //}
    //}
    public void bindDocTypeDDL()
    {
        DataSet ds1 = Inspection_Master.getMasterData("m_DocumentType", "Id", "Type");
        if (ds1.Tables[0].Rows.Count > 0)
        {
            this.ddl_DocType.DataSource = ds1.Tables[0];
            this.ddl_DocType.DataValueField = "Id";
            this.ddl_DocType.DataTextField = "Type";
            this.ddl_DocType.DataBind();
            this.ddl_DocType.Items.Insert(0, new ListItem("<Select>", "0"));
        }
        else
        {
            this.ddl_DocType.Items.Insert(0, new ListItem("<Select>", "0"));
        }
    }
    public void BindDocumentGrid()
    {
        DataTable dt1 = Inspection_Documents.DocumentDetails(intInspection_Id, intLogin_Id, 0, "", "", 0, 0, "Select");
        if (dt1.Rows.Count > 0)
        {
            GridStatus = true;
            Grid_Document.DataSource = dt1;
            Grid_Document.DataBind();
        }
        else
        {
            GridStatus = false;
            bindBlankGrid();
        }
    }
    public void bindBlankGrid()
    {
        DataTable dt = new DataTable();
        DataRow dr;
        dt.Columns.Add("Id");
        dt.Columns.Add("Type");
        dt.Columns.Add("DocumentName");
        dt.Columns.Add("FilePath");
        for (int i = 0; i < 9; i++)
        {
            dr = dt.NewRow();
            dt.Rows.Add(dr);
            dt.Rows[dt.Rows.Count - 1][0] = "";
            dt.Rows[dt.Rows.Count - 1][1] = "";
            dt.Rows[dt.Rows.Count - 1][2] = "";
            dt.Rows[dt.Rows.Count - 1][3] = "";
        }
        Grid_Document.DataSource = dt;
        Grid_Document.DataBind();
    }
    public bool chk_FileExtension(string str)
    {
        string extension = str;
        string MIMEType = null;
        switch (extension)
        {
            case ".txt":
                return true;
            case ".doc":
                return true;
            case ".docx":
                return true;
            case ".xls":
                return true;
            case ".xlsx":
                return true;
            case ".pdf":
                return true;
            default:
                return false;
                //return; 
                break;
        }
    }
    protected void Show_Record_Documents(int InspDocId)
    {
        HiddenFieldInspDocument.Value = InspDocId.ToString();
        DataTable dt1 = Inspection_Documents.DocumentDetails(InspDocId, intLogin_Id, 0, "", "", 0, 0, "ById");
        if (dt1.Rows.Count > 0)
        {
            ddl_DocType.SelectedValue = dt1.Rows[0]["DocumentTypeId"].ToString();
            txt_DocName.Text = dt1.Rows[0]["DocumentName"].ToString();
            txtCreatedBy_Document.Text = dt1.Rows[0]["Created_By"].ToString();
            txtCreatedOn_Document.Text = dt1.Rows[0]["Created_On"].ToString();
            txtModifiedBy_Document.Text = dt1.Rows[0]["Modified_By"].ToString();
            txtModifiedOn_Document.Text = dt1.Rows[0]["Modified_On"].ToString();
        }
    }
    #endregion

    #region "Events"
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        if (Session["Insp_Id"] == null) { lblmessage.Text = "Please save Planning first."; return; }
        try
        {
            //*******Document Saving Code
            string strDoc = "";
            String FileName = "";
            if (!(HiddenFieldInspDocument.Value.ToString().Trim() == ""))
            {
                if ((HiddenField_Doc.Value.ToString().Trim() == "") && (!FileUpload_Doc.HasFile))
                {
                    lblmessage.Text = "Please Select a Document.";
                    FileUpload_Doc.Focus();
                    return;
                }
                else
                {
                    if (FileUpload_Doc.PostedFile != null && FileUpload_Doc.FileContent.Length > 0)
                    {
                        string strfilename = FileUpload_Doc.FileName;
                        HttpPostedFile file1 = FileUpload_Doc.PostedFile;
                        UtilityManager um = new UtilityManager();
                        if (chk_FileExtension(Path.GetExtension(FileUpload_Doc.FileName).ToLower()) == true)
                        {
                            strDoc = "EMANAGERBLOB/Inspection/Inspection/" + FileUpload_Doc.FileName.Trim();
                            FileName = um.UploadFileToServer(file1, strfilename, HiddenField_Doc.Value, "D");
                            if (FileName.StartsWith("?"))
                            {
                                lblmessage.Text = FileName.Substring(1);
                                return;
                            }
                        }
                        else
                        {
                            lblmessage.Text = "Invalid File Type. (Valid Files Are .txt, .doc, .docx, .xls, .xlsx, .pdf)";
                            FileUpload_Doc.Focus();
                            return;
                        }
                    }
                    //if (FileUpload_Doc.HasFile)
                    //{
                    //    string strfilename = FileUpload_Doc.FileName;
                    //    if (chk_FileExtension(Path.GetExtension(FileUpload_Doc.FileName).ToLower()) == true)
                    //    {
                    //        strDoc = "EMANAGERBLOB/Inspection/Inspection/" + FileUpload_Doc.FileName.Trim();
                    //        if (FileUpload_Doc.FileName.Trim().Length > 0)
                    //        {
                    //            FileInfo fi = new FileInfo(Server.MapPath("~/EMANAGERBLOB/Inspection/Inspection/" + FileUpload_Doc.FileName.Trim()));
                    //            if (fi.Exists) fi.Delete();
                    //        }
                    //        FileUpload_Doc.SaveAs(Server.MapPath("~/EMANAGERBLOB/Inspection/Inspection/" + FileUpload_Doc.FileName.Trim()));
                    //    }
                    //    else
                    //    {
                    //        lblmessage.Text = "Invalid File Type. (Valid Files Are .txt, .doc, .docx, .xls, .xlsx, .pdf)";
                    //        FileUpload_Doc.Focus();
                    //        return;
                    //    }
                    //}
                    else
                    {
                        strDoc = HiddenField_Doc.Value;
                    }
                }
            }
            else
            {
                if (FileUpload_Doc.PostedFile != null && FileUpload_Doc.FileContent.Length > 0)
                {
                    string strfilename = FileUpload_Doc.FileName;
                    HttpPostedFile file1 = FileUpload_Doc.PostedFile;
                    UtilityManager um = new UtilityManager();
                    if (chk_FileExtension(Path.GetExtension(FileUpload_Doc.FileName).ToLower()) == true)
                    {
                        strDoc = "EMANAGERBLOB/Inspection/Inspection/" + FileUpload_Doc.FileName.Trim();
                        FileName = um.UploadFileToServer(file1, strfilename, HiddenField_Doc.Value, "D");
                        if (FileName.StartsWith("?"))
                        {
                            lblmessage.Text = FileName.Substring(1);
                            return;
                        }
                    }
                    else
                    {
                        lblmessage.Text = "Invalid File Type. (Valid Files Are .txt, .doc, .docx, .xls, .xlsx, .pdf)";
                        FileUpload_Doc.Focus();
                        return;
                    }
                }
                //if(FileUpload_Doc.HasFile)
                //{
                //    string strfilename = FileUpload_Doc.FileName;
                //    if (chk_FileExtension(Path.GetExtension(FileUpload_Doc.FileName).ToLower()) == true)
                //    {
                //        strDoc = "EMANAGERBLOB/Inspection/Inspection/" + FileUpload_Doc.FileName.Trim();
                //        if (FileUpload_Doc.FileName.Trim().Length > 0)
                //        {
                //            FileInfo fi = new FileInfo(Server.MapPath("~/EMANAGERBLOB/Inspection/Inspection/" + FileUpload_Doc.FileName.Trim()));
                //            if (fi.Exists) fi.Delete();
                //        }
                //        FileUpload_Doc.SaveAs(Server.MapPath("~/EMANAGERBLOB/Inspection/Inspection/" + FileUpload_Doc.FileName.Trim()));
                //    }
                //    else
                //    {
                //        lblmessage.Text = "Invalid File Type. (Valid Files Are .txt, .doc, .docx, .xls, .xlsx, .pdf)";
                //        FileUpload_Doc.Focus();
                //        return;
                //    }
                //}
                else
                {
                    lblmessage.Text = "Please Select a Document.";
                    FileUpload_Doc.Focus();
                    return;
                }
            }
            //**************************
            if (HiddenFieldInspDocument.Value.ToString().Trim() == "")
            {
                DataTable dt1 = Inspection_Documents.DocumentDetails(intInspection_Id, intLogin_Id, Convert.ToInt32(ddl_DocType.SelectedValue), txt_DocName.Text, FileName, intLogin_Id, intLogin_Id, "Add");
                lblmessage.Text = "Document Added Successfully.";
            }
            else
            {
                DataTable dt1 = Inspection_Documents.DocumentDetails(Convert.ToInt32(HiddenFieldInspDocument.Value), 0, Convert.ToInt32(ddl_DocType.SelectedValue), txt_DocName.Text, FileName, intLogin_Id, intLogin_Id, "MODIFY");
                lblmessage.Text = "Document Updated Successfully.";
            }
            BindDocumentGrid();
        }
        catch (Exception ex) { lblmessage.Text = ex.StackTrace.ToString(); }
    }
    protected void btn_Print_Click(object sender, EventArgs e)
    {

    }
    #endregion

    #region "Grid Events"
    protected void Grid_Document_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            Alerts.HANDLE_GRID(Grid_Document, Auth);
        }
        catch
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Login.aspx';", true);
        }
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    ImageButton ImgBtnDelete = (ImageButton)e.Row.FindControl("ImageButton1");
        //    ImageButton ImgBtnEdit = (ImageButton)e.Row.FindControl("ImageButton2");
        //    //ImageButton ImgBtnView = (ImageButton)e.Row.FindControl("ImageButton3");
        //    //CommandField imgView=(CommandField)e.Row.FindControl("Image");
        //    if (GridStatus == false)
        //    {
        //        //imgView.ShowSelectButton = false;
        //        //ImgBtnView.Enabled = false;
        //        ImgBtnEdit.Enabled = false;
        //        ImgBtnDelete.Enabled = false;
        //    }
        //}
    }
    protected void Grid_Document_SelectedIndexChanged(object sender, EventArgs e)
    {
        //HiddenField hfdInspDocId;
        //hfdInspDocId = (HiddenField)Grid_Document.Rows[Grid_Document.SelectedIndex].FindControl("Hidden_DocumentId");
        //if (hfdInspDocId.Value == "")
        //{
        //    Grid_Document.SelectedIndex = -1;
        //    return;
        //}
        //id = Convert.ToInt32(hfdInspDocId.Value.ToString());
        //Show_Record_Documents(id);
        //btn_Children.Enabled = false;
    }
    protected void Grid_Document_RowEditing(object sender, GridViewEditEventArgs e)
    {
        btn_Children.Enabled = true;
        HiddenField hfdInspDocId, hfdDocument;
        hfdInspDocId = (HiddenField)Grid_Document.Rows[e.NewEditIndex].FindControl("Hidden_DocumentId");
        if (hfdInspDocId.Value == "")
        {
            Grid_Document.SelectedIndex = -1;
            return;
        }
        hfdDocument = (HiddenField)Grid_Document.Rows[e.NewEditIndex].FindControl("hfd_Document");
        HiddenField_Doc.Value = hfdDocument.Value;
        id = Convert.ToInt32(hfdInspDocId.Value.ToString());
        Show_Record_Documents(id);
        Grid_Document.SelectedIndex = e.NewEditIndex;
        //Alerts.ShowPanel(pnl_Charterer);
        //Alerts.HANDLE_AUTHORITY(5, btn_Add_Charterer, btn_Save_Charterer, btn_Cancel_Charterer, btn_Print_Charterer, Auth);
    }
    protected void Grid_Document_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        btn_Children.Enabled = true;
        HiddenField hfdInspDocId;
        hfdInspDocId = (HiddenField)Grid_Document.Rows[e.RowIndex].FindControl("Hidden_DocumentId");
        if (hfdInspDocId.Value == "")
        {
            Grid_Document.SelectedIndex = -1;
            return;
        }
        id = Convert.ToInt32(hfdInspDocId.Value.ToString());
        try
        {
            DataTable dt1 = Inspection_Documents.DocumentDetails(id, intLogin_Id, 0, "", "", 0, intLogin_Id, "Delete");
            lblmessage.Text = "Document Deleted Successfully.";
        }
        catch (Exception ex) { lblmessage.Text = ex.StackTrace.ToString(); }
        BindDocumentGrid();
    }
    protected void Grid_Document_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Grid_Document.PageIndex = e.NewPageIndex;
        BindDocumentGrid();
    }
    protected void Grid_Document_Sorted(object sender, EventArgs e)
    {

    }
    protected void Grid_Document_Sorting(object sender, GridViewSortEventArgs e)
    {
        
    }
    #endregion

    public string GetPath(string _path)
    {
        string res = "";
        string appname = ConfigurationManager.AppSettings["AppName"].ToString();
        string applicationurl = ConfigurationManager.AppSettings["ApplicationURL"].ToString() + "/" + appname;
        if (_path.StartsWith("U"))
        {
            res = applicationurl + "/" + _path + "?" + R.NextDouble().ToString();
        }
        else
        {
           
            res = applicationurl + "/" + "EMANAGERBLOB/Inspection/Inspection/" + _path + "?" + R.NextDouble().ToString();
           // res = Server.MapPath("~/EMANAGERBLOB/Inspection/Inspection/" + _path);
           
        }
        return res;
    }
}
