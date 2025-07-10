using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using ShipSoft.CrewManager.BusinessLogicLayer;
using ShipSoft.CrewManager.BusinessObjects;
using ShipSoft.CrewManager.Operational;

public partial class emtm_MyProfile_Emtm_Profile_TrainingRecords : System.Web.UI.Page
{
    public int TrainingPlanningId
    {
        get
        {
            return Common.CastAsInt32(ViewState["TrainingPlanningId"]);
        }
        set
        {
            ViewState["TrainingPlanningId"] = value;
        }
    }    
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        lblMsg.Text = "";        
        Session["CurrentModule"] = 5;
        if (!IsPostBack)
        {
           TrainingPlanningId = -1;
           ShowRecord();
            
        }
    }

    protected void ShowRecord()
    {
        int EmpId = Common.CastAsInt32(Session["ProfileId"]);
        if (EmpId > 0)
        {
            DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("SELECT * FROM Hr_PersonalDetails WHERE EMPID=" + EmpId.ToString());
            if (dt != null)
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    lbl_EmpName.Text = "[ " + dr["EmpCode"].ToString() + " ] " + dr["FirstName"].ToString() + " " + dr["MiddleName"].ToString() + " " + dr["FamilyName"].ToString();
                    Session["EmpName"] = lbl_EmpName.Text.ToString();                    
                }
            BindTrainingRecords(EmpId);
        }
    }

    protected void BindTrainingRecords(int EmpId)
    {
        string strSQL = "SELECT M.TrainingPlanningId,TM.TRAININGID,TM.TRAININGNAME, " +
                        "CASE WHEN M.STATUS IS NULL THEN 'ASSIGN' " +
                        "WHEN M.STATUS='A' THEN 'PLANNED' " +
                        "WHEN M.STATUS='C' THEN 'CANCELLED' " +
                        "WHEN M.STATUS='E' THEN 'DONE' END " +
                        "AS STATUS " +
                        ",CASE WHEN R.RECOMMSOURCE='A' THEN 'ASSIGNED' WHEN R.RECOMMSOURCE='P' THEN 'PEAP' ELSE 'OTHER' END AS SOURCE , " +
                        "CASE WHEN M.STATUS='E' THEN  " +
                        "CONVERT(VARCHAR,STARTDATE1,106) + ' - ' + CONVERT(VARCHAR,ENDDATE1,106)  " +
                        "ELSE  " +
                        "CONVERT(VARCHAR,STARTDATE,106) + ' - ' + CONVERT(VARCHAR,ENDDATE,106) " +
                        "END AS DURATION, " +
                        "CASE WHEN M.STATUS='E' THEN LOCATION1 ELSE LOCATION END AS LOCATION,D.FileName " +
                        "FROM HR_TrainingRecommended R " +
                        "LEFT JOIN  " +
                        "( " +
                        " HR_TrainingPlanningDetails D  " +
                        " INNER JOIN HR_TrainingPlanning M ON M.TRAININGPLANNINGID=D.TRAININGPLANNINGID " +
                        ") " +
                        "ON D.TRAININGRECOMMID=R.TRAININGRECOMMID AND R.EMPID=D.EMPID  " +
                        "INNER JOIN HR_TrainingMaster TM ON TM.TrainingId=R.TrainingId " +
                        "WHERE R.EMPID=" + EmpId.ToString() + " ";
        
        DataTable dtRecords = Common.Execute_Procedures_Select_ByQueryCMS(strSQL);
        if (dtRecords != null)
        {
            rptRecordList.DataSource = dtRecords;
            rptRecordList.DataBind();
        }

    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        TrainingPlanningId = Common.CastAsInt32(((ImageButton)sender).CommandArgument.ToString());
        ShowRecord();
        divUploadDocs.Visible = true;
    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        
        if (FileUpload1.HasFile)
        {
            int EmpId = Common.CastAsInt32(Session["ProfileId"]);
            if (this.FileUpload1 != null && this.FileUpload1.FileContent.Length > 0)
            {
                HttpPostedFile file = FileUpload1.PostedFile;

                string ext = Path.GetExtension(FileUpload1.FileName);
                if (ext.ToUpper() == ".JPG" || ext.ToUpper() == ".PDF")
                {

                }
                else
                {
                    lblMsg.Text = "Uploading file type should be jpg and pdf only.";
                    return;
                }

                try
                {
                    string FileName = "Emtm_TR_" + TrainingPlanningId.ToString() + "_" + EmpId.ToString() + ext;

                    string SQL = "UPDATE HR_TrainingPlanningDetails SET [FileName] = '" + FileName + "' WHERE  TrainingPlanningId = " + TrainingPlanningId.ToString() + " AND EmpId = " + EmpId.ToString() + " ; SELECT -1 ";

                    DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(SQL);
                    if (dt.Rows.Count > 0)
                    {

                        if (this.FileUpload1 != null && this.FileUpload1.FileContent.Length > 0)
                        {
                            if (File.Exists(Server.MapPath("~/EMANAGERBLOB/HRD/EmpTrainingRecord/" + FileName)))
                            {
                                File.Delete(Server.MapPath("~/EMANAGERBLOB/HRDs/EmpTrainingRecord/" + FileName));
                            }

                            FileUpload1.SaveAs(Server.MapPath("~/EMANAGERBLOB/HRD/EmpTrainingRecord/" + FileName));
                            btncancel_Click(sender, e);
                            ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Document uploaded successfully.');", true);
                        }
                    }
                }
                catch (Exception ex)
                {
                    lblMsg.Text = "Unable to upload document. " + ex.Message.ToString();
                }
            }
        }
        else
        {
            lblMsg.Text = "Please select a file to upload.";
            FileUpload1.Focus();
        }

    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        TrainingPlanningId = -1;
        ShowRecord();
        this.FileUpload1 = null;
        divUploadDocs.Visible = false;
    }
}