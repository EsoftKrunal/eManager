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
//using ApplicationFormWebService;

public partial class PeapRegister : System.Web.UI.Page
{
    public int QidSelfAssesment
    {
        get
        {
            return Common.CastAsInt32(ViewState["QidSelfAssesment"]);
        }
        set
        {
            ViewState["QidSelfAssesment"] = value;
        }
    }
    public int JRID
    {
        get
        {
            return Common.CastAsInt32(ViewState["JRID"]);
        }
        set
        {
            ViewState["JRID"] = value;
        }
    }
    public int CID
    {
        get
        {
            return Common.CastAsInt32(ViewState["CID"]);
        }
        set
        {
            ViewState["CID"] = value;
        }
    }
    public int KPIId
    {
        get
        {
            return Common.CastAsInt32(ViewState["KPIId"]);
        }
        set
        {
            ViewState["KPIId"] = value;
        }
    }    
    
    // ------ Job Responsibility
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "ad", "RegisterAutoComplete();", true);
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        lblMsgJR.Text = "";
        lblMsgSelfAppraisal.Text = "";
        lblMsgCP.Text = "";
        lblKPIMsg.Text = "";

        Session["CurrentModule"] = "PEAP";
        Session["CurrentPage"] = 2;

        if (!Page.IsPostBack)
        {
            BindCategoty();
            // ------ Job Responsibility
            BindPosition();
            // ------ Competency
            BindOfficeCP();
            BindPositionCP();
            BindKRACat();
        }
        
    }

    // ===========================================================================  Function

    // ------ Self Assessment
    public void BindSelfAssessmentQuestion()
    {
        string sql = "Select Row_Number() over(order by QID)SR,QID,QCat,Question,DBO.GetCategoryCSV(CONVERT(VARCHAR, QCAT),',')CATagory, CASE ISNULL(Status,'A') WHEN 'A' THEN 'Active' ELSE 'Inactive' END AS Status1 from HR_SelfAppraisal WHERE Qcat = " + ddlCategory.SelectedValue.Trim() + " order by QID ";
        DataTable dtQuestion = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        rptSelfAssessment.DataSource = dtQuestion;
        rptSelfAssessment.DataBind();
    }
    public void BindCategoty()
    {
        string sql = "select PID,Category from HR_PeapCategory";
        DataTable dtPosition = Common.Execute_Procedures_Select_ByQueryCMS(sql);

        ddlCategory.DataSource = dtPosition;
        ddlCategory.DataTextField = "Category";
        ddlCategory.DataValueField = "PID";
        ddlCategory.DataBind();
        ddlCategory.Items.Insert(0, new ListItem("< Select >", "0"));

        ddlCategory1.DataSource = dtPosition;
        ddlCategory1.DataTextField = "Category";
        ddlCategory1.DataValueField = "PID";
        ddlCategory1.DataBind();
        ddlCategory1.Items.Insert(0, new ListItem("< Select >", "0"));

        ddlCategory2.DataSource = dtPosition;
        ddlCategory2.DataTextField = "Category";
        ddlCategory2.DataValueField = "PID";
        ddlCategory2.DataBind();
        ddlCategory2.Items.Insert(0, new ListItem("< Select >", "0"));


        //chkCategory.DataSource = dtPosition;
        //chkCategory.DataTextField = "Category";
        //chkCategory.DataValueField = "PID";
        //chkCategory.DataBind();

        //chkCategotyJR.DataSource = dtPosition;
        //chkCategotyJR.DataTextField = "Category";
        //chkCategotyJR.DataValueField = "PID";
        //chkCategotyJR.DataBind();

        //chkCategoryCP.DataSource = dtPosition;
        //chkCategoryCP.DataTextField = "Category";
        //chkCategoryCP.DataValueField = "PID";
        //chkCategoryCP.DataBind();
    }
    public void ClearSelfAssesmentData()
    {
        //foreach (ListItem Itm in chkCategory.Items)
        //{
        //    Itm.Selected = false;
        //}
        txtQuestionAPP.Text = "";
    }
    // ------ Job Responsibility
    protected void ddlCategory2_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindJobResponsibility();
    }
    public void BindPosition()
    {
        string sql = "Select * from VesselPositions order by PositionName";
        DataTable dtPosition = Common.Execute_Procedures_Select_ByQueryCMS(sql);

        ddlPositionJR.DataSource = dtPosition;
        ddlPositionJR.DataTextField = "PositionName";
        ddlPositionJR.DataValueField = "VPId";
        ddlPositionJR.DataBind();
        //ddlPositionJR.Items.Insert(0,new ListItem("< Select >","0"));
    }
    public void BindKRACat()
    {
        string sql = "Select * from HR_KRA order by kra_groupname";
        DataTable dtPosition = Common.Execute_Procedures_Select_ByQueryCMS(sql);

        ddlKRACat.DataSource = dtPosition;
        ddlKRACat.DataTextField = "kra_groupname";
        ddlKRACat.DataValueField = "kra_groupid";
        ddlKRACat.DataBind();
        ddlKRACat.Items.Insert(0,new ListItem("< Select >",""));
    }

    public void BindJobResponsibility()
    {
        int PositionId = ddlPositionJR.SelectedValue.Trim() == "" ? 0 : Common.CastAsInt32(ddlPositionJR.SelectedValue);

        string sql = "Select Row_Number() over(order by JR.JSID)SR,JR.JSID " +
                     " ,JR.PositionID,JR.QCAT,P.PositionName,JR.JobResponsibility,DBO.GetCategoryCSV(CONVERT(VARCHAR, JR.QCAT),',')CATagory, CASE ISNULL(JR.Status,'A') WHEN 'A' THEN 'Active' ELSE 'Inactive' END AS Status1, JR.Waitage, KRA_GroupName,jr.kra_groupid " +
                    " from HR_JobResponsibility JR " +
                    " inner join VesselPositions P on P.VPId= JR.PositionID " +
                    " left join HR_KRA k on k.KRA_GroupId= JR.KRA_GroupId " +
                    " WHERE JR.QCAT = " + ddlCategory2.SelectedValue.Trim() + " ";

        string WhereCond = " AND JR.PositionID = " + PositionId + " ";

        sql = sql + WhereCond + " ORDER BY JSID ";

        DataTable dtQuestion = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        rptJobResponsibility.DataSource = dtQuestion;
        rptJobResponsibility.DataBind();
    }
    public void ClearJobResponsibility()
    {
        //foreach (ListItem Itm in chkCategotyJR.Items)
        //{
        //    Itm.Selected = false;
        //}
        txtJobResponsibility.Text = "";
        txtWaitage.Text = "";
        ddlKRACat.SelectedIndex = 0;
        //ddlOfficeJR.SelectedIndex = 0;
        //ddlOfficeJR_OnSelectedIndexChanged(new object(), new EventArgs());
    }
    
    // ------ Competency
    public void BindCompetency()
    {
        string sql = "Select Row_Number() over(order by CP.CID)SR,CP.CID " +
                     " ,CP.PositionID,CP.QCAT,CP.Descr, CP.Competency,DBO.GetCategoryCSV(CONVERT(VARCHAR, CP.QCAT),',')CATagory, CASE ISNULL(CP.Status,'A') WHEN 'A' THEN 'Active' ELSE 'Inactive' END AS Status1 " +
                    " from HR_Competency CP " +
                    " WHERE CP.QCAT = " + ddlCategory1.SelectedValue.Trim() + " order by CID ";
        DataTable dtQuestion = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        rptCompetency.DataSource = dtQuestion;
        rptCompetency.DataBind();
    }
    public void ClearCompetency()
    {
        //foreach (ListItem Itm in chkCategoryCP.Items)
        //{
        //    Itm.Selected = false;
        //}
        txtCompetency.Text = "";
        txtCompDescr.Text = "";
        //ddlOfficeCP.SelectedIndex = 0;
        //ddlOfficeCP_OnSelectedIndexChanged(new object() , new EventArgs() );
    }
    public void BindOfficeCP()
    {
        //string sql = "Select OfficeID,OfficeName from Office  order by OfficeName";
        //DataTable dtOffice = Common.Execute_Procedures_Select_ByQueryCMS(sql);

        //ddlOfficeCP.DataSource = dtOffice;
        //ddlOfficeCP.DataTextField = "OfficeName";
        //ddlOfficeCP.DataValueField = "OfficeID";
        //ddlOfficeCP.DataBind();
        //ddlOfficeCP.Items.Insert(0, new ListItem(" Select ", ""));
    }
    public void BindPositionCP()
    {
        //string sql = "Select PositionID,PositionName from Position where OfficeID=" + Common.CastAsInt32(ddlOfficeCP.SelectedValue) + " order by PositionName";
        //DataTable dtPosition = Common.Execute_Procedures_Select_ByQueryCMS(sql);

        //ddlPositionCP.DataSource = dtPosition;
        //ddlPositionCP.DataTextField = "PositionName";
        //ddlPositionCP.DataValueField = "PositionID";
        //ddlPositionCP.DataBind();
        //ddlPositionCP.Items.Insert(0, new ListItem(" Select ", ""));
    }

    // ------ Job Responsibility
    // ===========================================================================  Events
    // ------ Self Assessment
    protected void btnSaveAppraisalMaster_OnClick(object sender, EventArgs e)
    {
        try
        {
            string PeapCategory = ddlCategory.SelectedValue.Trim();

            if (PeapCategory == "0")
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "", "alert('Please select the category.')", true);
                return;
            }

            string checkDuplicate = "SELECT * FROM HR_SelfAppraisal WHERE QID <> " + QidSelfAssesment.ToString() + " AND QCat = " + PeapCategory + " AND Question = '" + txtQuestionAPP.Text.Replace("'","''").Trim() + "' ";
            DataTable dtDuplicate = Common.Execute_Procedures_Select_ByQueryCMS(checkDuplicate);

            if (dtDuplicate.Rows.Count > 0)
            {
                lblMsgSelfAppraisal.Text = "Question already exist.";
                txtQuestionAPP.Focus();
                return;
            }

            Common.Set_Procedures("DBO.HR_InsertSelfAppraisal");
            Common.Set_ParameterLength(3);
            Common.Set_Parameters(
                    new MyParameter("@QID", QidSelfAssesment),
                    new MyParameter("@QUESTION", txtQuestionAPP.Text),
                    new MyParameter("@QCAT", PeapCategory)
                );
            Boolean Res;
            DataSet ds=new DataSet();
            Res = Common.Execute_Procedures_IUD(ds);
            if (Res)
            {
                lblMsgSelfAppraisal.Text = "Data saved successfully";
                ClearSelfAssesmentData();
                //----------
                tblSelfAssessment.Visible = false;
                btnSaveAppraisalMaster.Visible = false;
                btnCancel.Visible = false;
                btnAddSelfAssessment.Visible = true;
                QidSelfAssesment = 0;
                BindSelfAssessmentQuestion();
            }
            else
            {
                lblMsgSelfAppraisal.Text = "Error while saving data.";
            }
        }
        catch (Exception ex)
        {
            lblMsgSelfAppraisal.Text = "Error while saving data." + ex.Message;
        }

    }
    protected void imgEditSelfAppraisal_OnClick(object sender, EventArgs e)
    {
        tblSelfAssessment.Visible = true;
        btnSaveAppraisalMaster.Visible = true;
        btnCancel.Visible = true;
        btnAddSelfAssessment.Visible = false;
        //---------------------------

        ImageButton btn = (ImageButton)sender;
        HiddenField QuestionID = (HiddenField)btn.Parent.FindControl("hfQuestionID");
        HiddenField Category = (HiddenField)btn.Parent.FindControl("hfCategory");
        Label Question = (Label)btn.Parent.FindControl("lblQuestion");
        QidSelfAssesment = Common.CastAsInt32(QuestionID.Value);

        txtQuestionAPP.Text = Question.Text;
        //foreach (ListItem itm in chkCategory.Items)
        //{
        //    itm.Selected = false;
        //    if ((Category.Value.IndexOf(itm.Value)) != -1)
        //    {
        //        itm.Selected = true;
        //    }
        //}
        BindSelfAssessmentQuestion();
    }
    protected void imgDeleteSelfAppraisal_OnClick(object sender, EventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        HiddenField QuestionID = (HiddenField)btn.Parent.FindControl("hfQuestionID");

        //string checkSQL = "SELECT * FROM HR_EmployeePeapSADetails WHERE QId = " + QuestionID.Value.Trim() + " AND ISNULL(Answer,'') <> ''";
        //DataTable dtCheck = Common.Execute_Procedures_Select_ByQueryCMS(checkSQL);

        //if (dtCheck.Rows.Count > 0)
        //{
        //    string sqlVirDelete = "UPDATE HR_SelfAppraisal SET [Status] = 'I' where QID=" + QuestionID.Value.Trim() + " ; SELECT -1";
        //    DataTable dtVirDelete = Common.Execute_Procedures_Select_ByQueryCMS(sqlVirDelete);

        //    if (dtVirDelete.Rows.Count > 0)
        //    {
        //        lblMsgSelfAppraisal.Text = "Question deleted Successfully.";
        //        BindSelfAssessmentQuestion();
        //    }
        //    else
        //    {
        //        lblMsgSelfAppraisal.Text = "Unable to delete question.";
        //    }

        //}
        //else
        //{
            string sql = "Delete from HR_SelfAppraisal where QID=" + QuestionID.Value.Trim() + "";
            DataTable dtQuestion = Common.Execute_Procedures_Select_ByQueryCMS(sql);

            lblMsgSelfAppraisal.Text = "Data deleted successfully.";
            BindSelfAssessmentQuestion();
        //}
    }
    protected void btnAddSelfAssessment_OnClick(object sender, EventArgs e)
    {
        ClearSelfAssesmentData();
        tblSelfAssessment.Visible = true;
        btnSaveAppraisalMaster.Visible = true;
        btnCancel.Visible = true;
        btnAddSelfAssessment.Visible = false;
    }
    protected void btnCancel_OnClick(object sender, EventArgs e)
    {
        tblSelfAssessment.Visible = false;
        btnSaveAppraisalMaster.Visible = false;
        btnCancel.Visible = false;
        btnAddSelfAssessment.Visible = true;
        QidSelfAssesment = 0;
        BindSelfAssessmentQuestion();
    }

    // ------ Job Responsibility
    protected void btnSaveJR_OnClick(object sender, EventArgs e)
    {
        try
        {
            string PeapCategory = ddlCategory2.SelectedValue.Trim();

            if (PeapCategory == "0")
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "", "alert('Please select the category.')", true);
                return;
            }

            if (ddlPositionJR.SelectedIndex < 0)
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "", "alert('Please select position.')", true);
                return;
            }

            if (ddlKRACat.SelectedIndex < 0)
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "", "alert('Please select KRA Category.')", true);
                return;
            }

            int waitage = 0;

            foreach (RepeaterItem rpt in rptJobResponsibility.Items)
            {
                HiddenField JSID = (HiddenField)rpt.FindControl("hfJSID");
                Label lblWaitage = (Label)rpt.FindControl("lblWaitage");

                if (Common.CastAsInt32(JSID.Value.Trim()) != JRID)
                {
                    waitage = waitage + (lblWaitage.Text.Trim() == "" ? 0 : Convert.ToInt32(lblWaitage.Text.Trim().Remove(lblWaitage.Text.Trim().Length-1)));
                }
            }

            waitage = waitage + Convert.ToInt32(txtWaitage.Text.Trim());

            if (waitage > 100)
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "", "alert('Sum of weightage can not be greater than 100.')", true);
                return;
            }


            string checkDuplicate = "SELECT *  FROM HR_JobResponsibility WHERE JSID <> " + JRID.ToString() + " AND PositionID = " + ddlPositionJR.SelectedValue.Trim() + " AND QCat = " + PeapCategory.ToString() + " AND JobResponsibility = '" + txtJobResponsibility.Text.Replace("'", "''").Trim() + "' ";
            DataTable dtDuplicate = Common.Execute_Procedures_Select_ByQueryCMS(checkDuplicate);

            if (dtDuplicate.Rows.Count > 0)
            {
                lblMsgJR.Text = "KRA already exist.";
                txtJobResponsibility.Focus();
                return;
            }

            Common.Set_Procedures("DBO.HR_InsertJobResponsibility");
            Common.Set_ParameterLength(6);
            Common.Set_Parameters(
                    new MyParameter("@JRID", JRID),
                    new MyParameter("@POSITIONID", ddlPositionJR.SelectedValue),
                    new MyParameter("@JOBRESPONSIBILITY", txtJobResponsibility.Text.Trim()),
                    new MyParameter("@QCAT",PeapCategory),
                    new MyParameter("@Waitage", Common.CastAsInt32(txtWaitage.Text.Trim())),
                    new MyParameter("@KRA_GroupId", ddlKRACat.SelectedValue)
                );
            Boolean Res;
            DataSet ds = new DataSet();
            Res = Common.Execute_Procedures_IUD(ds);
            if (Res)
            {
                lblMsgJR.Text = "Data saved successfully";
                ClearJobResponsibility();
                //----------
                tblJobResponsibility.Visible = false;
                btnSaveJR.Visible = false;
                btnCancelJR.Visible = false;
                btnAddJR.Visible = true;
                JRID = 0;
                BindJobResponsibility();
            }
            else
            {
                lblMsgJR.Text = "Error while saving data.";
            }
        }
        catch (Exception ex)
        {
            lblMsgJR.Text = "Error while saving data." + ex.Message;
        }

    }
    protected void imgEditJobResponsibility_OnClick(object sender, EventArgs e)
    {
        tblJobResponsibility.Visible = true;
        btnSaveJR.Visible = true;
        btnCancelJR.Visible = true;
        btnAddJR.Visible = false;
        //---------------------------

        ImageButton btn = (ImageButton)sender;
        HiddenField JSID = (HiddenField)btn.Parent.FindControl("hfJSID");
        HiddenField Category = (HiddenField)btn.Parent.FindControl("hfCategory");
        HiddenField Position = (HiddenField)btn.Parent.FindControl("ddlPosition");
        
        Label JobResponsibility = (Label)btn.Parent.FindControl("lblJobResponsibility");
        Label lblWaitage = (Label)btn.Parent.FindControl("lblWaitage");
        HiddenField hfdKRA = (HiddenField)btn.Parent.FindControl("hfdKRA");

        JRID = Common.CastAsInt32(JSID.Value);
        ddlOfficeJR_OnSelectedIndexChanged(sender, e);
        txtJobResponsibility.Text = JobResponsibility.Text;
        try
        {
            ddlKRACat.SelectedValue = Common.CastAsInt32(hfdKRA.Value).ToString();
        }
        catch { }
        
        if (lblWaitage.Text.Trim() != "")
        {
            txtWaitage.Text = lblWaitage.Text.Trim().Remove(lblWaitage.Text.Trim().Length-1).ToString();
        }

        ddlOfficeJR_OnSelectedIndexChanged(new object(), new EventArgs());

        ddlPositionJR.SelectedValue = Common.CastAsInt32( Position.Value).ToString();
        
        //foreach (ListItem itm in chkCategotyJR.Items)
        //{
        //    itm.Selected = false;
        //    if ((Category.Value.IndexOf(itm.Value)) != -1)
        //    {
        //        itm.Selected = true;
        //    }
        //}
        BindJobResponsibility();
    }
    protected void imgDeleteJobResponsibility_OnClick(object sender, EventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        HiddenField JSID = (HiddenField)btn.Parent.FindControl("hfJSID");

        //string checkSQL = "SELECT * FROM HR_EmployeePeapJobResponsibility WHERE JSId = " + JSID.Value.Trim() + "  AND ISNULL(Answer, 0) <> 0 ";
        //DataTable dtCheck = Common.Execute_Procedures_Select_ByQueryCMS(checkSQL);

        //if (dtCheck.Rows.Count > 0)
        //{
        //    string sqlVirDelete = "UPDATE HR_JobResponsibility SET [Status] = 'I' WHERE JSID=" + JSID.Value.Trim() + " ; SELECT -1";
        //    DataTable dtVirDelete = Common.Execute_Procedures_Select_ByQueryCMS(sqlVirDelete);

        //    if (dtVirDelete.Rows.Count > 0)
        //    {
        //        lblMsgJR.Text = "Job responsibility deleted Successfully.";
        //        BindJobResponsibility();
        //    }
        //    else
        //    {
        //        lblMsgJR.Text = "Unable to delete Job responsibility.";
        //    }

        //}
        //else
        //{
        string sql = "DELETE FROM HR_JobResponsibility_KPI WHERE JSID =" + JSID.Value.Trim() + " ;DELETE FROM HR_JobResponsibility WHERE JSID=" + JSID.Value.Trim() + "; SELECT -1";
            DataTable dtJR = Common.Execute_Procedures_Select_ByQueryCMS(sql);

            if (dtJR.Rows.Count > 0)
            {
                lblMsgJR.Text = "Data deleted successfully.";
                BindJobResponsibility();
            }
            else
            {
                lblMsgJR.Text = "Unable to delete KRA.";
            }
        //}
    }
    protected void btnAddJR_OnClick(object sender, EventArgs e)
    {
        ClearJobResponsibility();
        tblJobResponsibility.Visible = true;
        btnSaveJR.Visible = true;
        btnCancelJR.Visible = true;
        btnAddJR.Visible = false;
    }
    protected void btnCancelJR_OnClick(object sender, EventArgs e)
    {
        tblJobResponsibility.Visible = false;
        btnSaveJR.Visible = false;
        btnCancelJR.Visible = false;
        btnAddJR.Visible = true;
        JRID = 0;
        BindJobResponsibility();
    }
    protected void ddlOfficeJR_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        BindJobResponsibility();
    }
    protected void ddlPositionJR_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindJobResponsibility();
    }

    // ------ Competency    
    protected void btnSaveCP_OnClick(object sender, EventArgs e)
    {
        try
        {
            string PeapCategory = ddlCategory1.SelectedValue.Trim();            

            if (PeapCategory == "0")
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "", "alert('Please select the category.')", true);
                return;
            }

            char[] delimiters = new char[] { ' ', '\r', '\n' };
            string[] words = txtCompetency.Text.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

            if (words.Length > 50)
            {
                lblMsgCP.Text = "Competency text can not be more than 50 words.";
                return;
            }

            string checkDuplicate = "SELECT * FROM HR_Competency WHERE CID <> " + CID.ToString() + "  AND QCat = " + PeapCategory.ToString() + " AND Competency = '" + txtCompetency.Text.Replace("'", "''").Trim() + "' ";
            DataTable dtDuplicate = Common.Execute_Procedures_Select_ByQueryCMS(checkDuplicate);

            if (dtDuplicate.Rows.Count > 0)
            {
                lblMsgCP.Text = "Competency already exist.";
                txtCompetency.Focus();
                return;
            }

            Common.Set_Procedures("DBO.HR_InsertCompetency");
            Common.Set_ParameterLength(5);
            Common.Set_Parameters(
                    new MyParameter("@CID", CID),
                    new MyParameter("@POSITIONID", "0"),
                    new MyParameter("@COMPETENCY", txtCompetency.Text.Trim()),
                    new MyParameter("@QCAT", PeapCategory),
                    new MyParameter("@Descr", txtCompDescr.Text.Trim())
                );
            Boolean Res;
            DataSet ds = new DataSet();
            Res = Common.Execute_Procedures_IUD(ds);
            if (Res)
            {
                lblMsgCP.Text = "Data saved successfully";
                ClearCompetency();
                //----------
                tblCompetency.Visible = false;
                btnSaveCP.Visible = false;
                btnCancelCP.Visible = false;
                btnAddCP.Visible = true;
                CID = 0;
                BindCompetency();
            }
            else
            {
                lblMsgCP.Text = "Error while saving data.";
            }
        }
        catch (Exception ex)
        {
            lblMsgCP.Text = "Error while saving data." + ex.Message;
        }

    }
    protected void imgEditCompetency_OnClick(object sender, EventArgs e)
    {
        tblCompetency.Visible = true;
        btnSaveCP.Visible = true;
        btnCancelCP.Visible = true;
        btnAddCP.Visible = false;
        //---------------------------

        ImageButton btn = (ImageButton)sender;
        HiddenField hfCID = (HiddenField)btn.Parent.FindControl("hfCID");
        HiddenField Category = (HiddenField)btn.Parent.FindControl("hfCategory");
        //HiddenField Position = (HiddenField)btn.Parent.FindControl("ddlPosition");
        //HiddenField OfficeID = (HiddenField)btn.Parent.FindControl("hfOfficeID");
        HiddenField Descr = (HiddenField)btn.Parent.FindControl("hfDescr");

        Label lblCompetency = (Label)btn.Parent.FindControl("lblCompetency");
        CID = Common.CastAsInt32(hfCID.Value);
        //ddlOfficeCP.SelectedValue = OfficeID.Value;
        //ddlOfficeCP_OnSelectedIndexChanged(sender, e);

        txtCompetency.Text = lblCompetency.Text;
        //ddlOfficeCP.SelectedValue = OfficeID.Value;

        //ddlOfficeCP_OnSelectedIndexChanged(new object(), new EventArgs());

        //ddlPositionCP.SelectedValue = Common.CastAsInt32(Position.Value).ToString();
        txtCompDescr.Text = Descr.Value;

        //foreach (ListItem itm in chkCategoryCP.Items)
        //{
        //    itm.Selected = false;
        //    if ((Category.Value.IndexOf(itm.Value)) != -1)
        //    {
        //        itm.Selected = true;
        //    }
        //}
        BindCompetency();
    }
    protected void imgDeleteCompetency_OnClick(object sender, EventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        HiddenField hfCID = (HiddenField)btn.Parent.FindControl("hfCID");

        //string checkSQL = "SELECT * FROM HR_EmployeeCompetency WHERE CId = " + hfCID.Value.Trim() + " AND ISNULL(Answer, 0) <> 0 ";
        //DataTable dtCheck = Common.Execute_Procedures_Select_ByQueryCMS(checkSQL);

        //if (dtCheck.Rows.Count > 0)
        //{
        //    string sqlVirDelete = "UPDATE HR_Competency SET [Status] = 'I' WHERE CID=" + hfCID.Value.Trim() + " ; SELECT -1";
        //    DataTable dtVirDelete = Common.Execute_Procedures_Select_ByQueryCMS(sqlVirDelete);

        //    if (dtVirDelete.Rows.Count > 0)
        //    {
        //        lblMsgCP.Text = "Competency deleted Successfully.";
        //        BindCompetency();
        //    }
        //    else
        //    {
        //        lblMsgCP.Text = "Unable to delete Competency.";
        //    }
        //}
        //else
        //{
        string sql = "DELETE FROM HR_Competency WHERE CID=" + hfCID.Value.Trim() + "";
            DataTable dtCP = Common.Execute_Procedures_Select_ByQueryCMS(sql);

            lblMsgCP.Text = "Data deleted successfully.";
            BindCompetency();
        //}

        
    }
    protected void btnAddCP_OnClick(object sender, EventArgs e)
    {
        ClearCompetency();
        tblCompetency.Visible = true;
        btnSaveCP.Visible = true;
        btnCancelCP.Visible = true;
        btnAddCP.Visible = false;
    }
    protected void btnCancelCP_OnClick(object sender, EventArgs e)
    {
        tblCompetency.Visible = false;
        btnSaveCP.Visible = false;
        btnCancelCP.Visible = false;
        btnAddCP.Visible = true;
        CID = 0;        
        BindCompetency();
    }
    //protected void ddlOfficeCP_OnSelectedIndexChanged(object sender, EventArgs e)
    //{
    //    BindPositionCP();
    //}
    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindSelfAssessmentQuestion();
    }
    protected void ddlCategory1_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindCompetency();
    }
    protected void TabContainer1_ActiveTabChanged(object sender, EventArgs e)
    {
        if (TabContainer1.ActiveTabIndex == 2)
        {
            //trCompFields.Visible = true;
            
            ddlPositionJR.SelectedIndex = -1;

            rptJobResponsibility.DataSource = null;
            rptJobResponsibility.DataBind();
        }
    }
    public void ClearFields()
    {
        txtQuestionAPP.Text = "";
        txtJobResponsibility.Text = "";
        txtCompetency.Text = "";
        txtCompDescr.Text = "";
        ddlKRACat.SelectedIndex = 0;
    }


    #region ------------- KPI SECTION ----------------------

    protected void btnAssignKPI_Click(object sender, EventArgs e)
    {

        JRID = Common.CastAsInt32(((Button)sender).CommandArgument);
        string JRName = ((Button)sender).CssClass;
        Label lblKRAGroup =(Label)((Button)sender).Parent.FindControl("lblKRAGroup");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "akc", "window.open('AssignKPI.aspx?JRId=" + JRID + "&JRName=" + JRName + "&PositionName=" + ddlPositionJR.SelectedItem.Text + "&PeapLevelName=" + ddlCategory2.SelectedItem.Text + "&Cat=" + lblKRAGroup.Text + "','');", true);
        //BindKPI();
        //dvAssignKPI.Visible = true;
        //btnCancel_MFB.Text = "Cancel";
        
    }

    protected void btnCancelKPI_Click(object sender, EventArgs e)
    {
        KPIId = 0;
        txtKPIName.Text = "";
        txtKPIValue.Text = "";
        txtlinkkpiname.Text = "";
        hfdlinkkpiid.Value = "";
        BindKPI();
        
    }
    protected void btnCloseKPI_Click(object sender, EventArgs e)
    {
        JRID = 0;
        KPIId = 0;
        txtKPIName.Text = "";
        txtKPIValue.Text = "";
        txtlinkkpiname.Text = "";
        hfdlinkkpiid.Value = "";

        dvAssignKPI.Visible = false;
    }
    

    protected void BindKPI()
    {
        string Query = "SELECT KPIId,JSID,k.KPIName,KPIValue, KPIKey, e.kpiname as linkname FROM HR_JobResponsibility_KPI k left join dbo.kpi_entry e on k.kpikey=e.entryid  WHERE JSID = " + JRID + " ORDER BY k.KPIName";

        DataTable dtKPI = Common.Execute_Procedures_Select_ByQueryCMS(Query);

        rptKPI.DataSource = dtKPI;
        rptKPI.DataBind();
    }
    
    protected void btnSelectKPI_Click(object sender, EventArgs e)
    {

    }
    protected void btnSaveKPI_Click(object sender, EventArgs e)
    {
        if (txtKPIName.Text.Trim() == "")
        {
            lblKPIMsg.Text = "Please enter kpi name.";
            txtKPIName.Focus();
            return;
        }
        else if (txtKPIValue.Text.Trim() == "")
        {
            lblKPIMsg.Text = "Please enter kpi value.";
            txtKPIValue.Focus();
            return;
        }
        else if (Common.CastAsInt32(hfdlinkkpiid.Value)<=0 && txtlinkkpiname.Text.Trim() != "")
        {
            lblKPIMsg.Text = "KPI Not linked properly.";
            txtKPIName.Focus();
            return;
        }

        //string checkDuplicate = "SELECT * FROM HR_Competency WHERE CID <> " + CID.ToString() + "  AND QCat = " + PeapCategory.ToString() + " AND Competency = '" + txtCompetency.Text.Replace("'", "''").Trim() + "' ";
        //DataTable dtDuplicate = Common.Execute_Procedures_Select_ByQueryCMS(checkDuplicate);

        //if (dtDuplicate.Rows.Count > 0)
        //{
        //    lblMsgCP.Text = "Job responsibility already exist.";
        //    txtCompetency.Focus();
        //    return;
        //}

        Common.Set_Procedures("DBO.HR_InsertJobResponsibility_KPI");
        Common.Set_ParameterLength(5);
        Common.Set_Parameters(
                new MyParameter("@KPIId", KPIId),
                new MyParameter("@JSID", JRID),
                new MyParameter("@KPIName", txtKPIName.Text.Trim()),
                new MyParameter("@KPIValue", txtKPIValue.Text.Trim()),
                new MyParameter("@KPIKey", hfdlinkkpiid.Value)
                
            );
        Boolean Res;
        DataSet ds = new DataSet();
        Res = Common.Execute_Procedures_IUD(ds);
        if (Res)
        {
            lblKPIMsg.Text = "Data saved successfully";
            txtKPIName.Text = "";
            txtKPIValue.Text = "";
            txtlinkkpiname.Text = "";
            hfdlinkkpiid.Value= "";
            KPIId = 0;
            BindKPI();
        }
        else
        {
            lblKPIMsg.Text = "Error while saving data.";
        }
    }
    protected void imgEditKPI_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        KPIId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);

        Label lblKPIId = (Label)btn.Parent.FindControl("lblKPIId");
        Label lblKPIName = (Label)btn.Parent.FindControl("lblKPIName");
        Label lblKPIValue = (Label)btn.Parent.FindControl("lblKPIValue");

        Label lblLinkKPIName = (Label)btn.Parent.FindControl("lblLinkKPIName");
        HiddenField hfdLinkKPIId = (HiddenField)btn.Parent.FindControl("hfdLinkKPIId");

        txtKPIName.Text = lblKPIName.Text;
        txtKPIValue.Text = lblKPIValue.Text;

        txtlinkkpiname.Text = lblLinkKPIName.Text;
        hfdlinkkpiid.Value = hfdLinkKPIId.Value;
        BindKPI();


    }
    protected void imgDeleteKPI_Click(object sender, ImageClickEventArgs e)
    {
        KPIId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);

        string deleteQuery = "DELETE FROM HR_JobResponsibility_KPI WHERE KPIId=" + KPIId + " AND JSID= " + JRID + "; SELECT -1";

        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(deleteQuery);

        if (dt.Rows.Count > 0)
        {
            lblKPIMsg.Text = "Data Deleted successfully.";
            BindKPI();
        }
        else
        {
            lblKPIMsg.Text = "Unable to delete data.";
        }

    }

    #endregion

}

