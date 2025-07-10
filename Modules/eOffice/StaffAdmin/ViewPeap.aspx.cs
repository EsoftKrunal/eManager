using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;

public partial class Emtm_ViewPeap : System.Web.UI.Page
{
    public AuthenticationManager auth;
    public string SessionDeleimeter = ConfigurationManager.AppSettings["SessionDelemeter"];
    public int PeapID
    {
        get
        {
            return Common.CastAsInt32(ViewState["PeapID"]);
        }
        set
        {
            ViewState["PeapID"] = value;
        }
    }
    public int EmpID
    {
        get
        {
            return Common.CastAsInt32(ViewState["EmpID"]);
        }
        set
        {
            ViewState["EmpID"] = value;
        }
    }
    public string Mode
    {
        get
        {
            return ""+ViewState["Mode"].ToString();
        }
        set
        {
            ViewState["Mode"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        //***********Code to check page acessing Permission
        //int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 244);
        //if (chpageauth <= 0)
        //{
        //    Response.Redirect("../AuthorityError.aspx");
        //}
        //*******************
        lblSAmsg.Text = "";
        lblMsg_JR.Text = "";
        lblMsg_Comp.Text = "";
        lblMsg_Pot.Text = "";

        if (!Page.IsPostBack)
        {
            if ((Request.QueryString["PeapID"] != null || Request.QueryString["PeapID"].ToString() != "") && (Request.QueryString["Mode"] != null || Request.QueryString["Mode"].ToString() != ""))
            {
                PeapID = Common.CastAsInt32( Request.QueryString["PeapID"].ToString());
                Mode = Request.QueryString["Mode"].ToString();

                if (Mode == "A")
                {
                    EmpID = Common.CastAsInt32(Session["EmpId"]);
                    trAppraiser.Visible = true;
                    BindAppraisers();
                }
                else
                {
                    EmpID = Common.CastAsInt32(Session["ProfileId"]);
                }
                ShowRecord();
                BindSelfAssessment();
                BindJobResponsibility();
                BindCompetency();
                BindPotential();
            }
        }
    }



    //------------------ Events
   

    //------------------ Function
    public void ShowRecord()
    {
        string strSQL = "SELECT PEAPID,CATEGORY As PeapLevel,CASE Occasion WHEN 'R' THEN 'Routine' WHEN 'I' THEN 'Interim' ELSE '' END AS Occasion, POSITIONNAME,EMPCODE,FIRSTNAME , FAMILYNAME,D.DeptName AS DepartmentName, " +
                        "Replace(Convert(Varchar,PM.PEAPPERIODFROM,106),' ','-') AS PEAPPERIODFROM ,Replace(Convert(Varchar,PM.PEAPPERIODTO,106),' ','-') AS PEAPPERIODTO,Replace(Convert(Varchar,PD.DJC,106),' ','-') AS DJC " +
                        "FROM HR_EmployeePeapMaster PM  " +
                        "INNER JOIN dbo.Hr_PersonalDetails PD ON PM.EMPID=PD.EMPID  " +
                        "LEFT JOIN dbo.HR_PeapCategory PC ON PC.PID= PD.PID  " +
                        "LEFT JOIN POSITION P ON P.POSITIONID=PD.POSITION  " +
                        "LEFT JOIN HR_Department D ON D.DeptId=PD.DEPARTMENT  " +
                        "WHERE PM.PeapId = " + PeapID.ToString() + " ";

        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(strSQL);

        if (dt.Rows.Count > 0)
        {
            lblPeapLevel.Text = dt.Rows[0]["PeapLevel"].ToString();
            txtOccasion.Text = dt.Rows[0]["Occasion"].ToString();
            lblPosition.Text = dt.Rows[0]["POSITIONNAME"].ToString();
            txtEmpNo.Text = dt.Rows[0]["EMPCODE"].ToString();
            txtFirstName.Text = dt.Rows[0]["FIRSTNAME"].ToString();
            txtLastName.Text = dt.Rows[0]["FAMILYNAME"].ToString();
            txtDept.Text = dt.Rows[0]["DepartmentName"].ToString();
            txtAppraisalFrom.Text = dt.Rows[0]["PEAPPERIODFROM"].ToString();
            txtAppraislaTo.Text = dt.Rows[0]["PEAPPERIODTO"].ToString();
            txtDJC.Text = dt.Rows[0]["DJC"].ToString();
        }

    }
    //--------------- Bind All Grids --------------------------
    public void BindSelfAssessment()
    {
        string sql = "SELECT  Row_Number() over(order by Qtext)Srno,* FROM dbo.HR_EmployeePeapSADetails WHERE PEAPID=" + PeapID + "";
        //string sql = "select Row_Number() over(order by Question)Srno,* from HR_SelfAppraisal where Qcat like '%" + ddlPeepType.SelectedValue + "%' Order by Question";
        DataTable Dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);

        rptSelfAssessment.DataSource = Dt;
        rptSelfAssessment.DataBind();

        string str = "SELECT [Status] FROM HR_EmployeePeapMaster WHERE PeapId = " + PeapID.ToString() + " AND EmpId = " + EmpID.ToString() + " ";
        DataTable dtStatus = Common.Execute_Procedures_Select_ByQueryCMS(str);

        if(dtStatus != null)
        {
            if (Mode == "A")
            {
                btnSaveSA.Visible = false;
                chkSAComp.Visible = false;
            }
            else
            {
                if (dtStatus.Rows.Count > 0)
                {
                    if (dtStatus.Rows[0]["Status"].ToString() == "0")
                    {
                        btnSaveSA.Visible = true;
                        chkSAComp.Visible = true;
                    }
                    else
                    {
                        btnSaveSA.Visible = false;
                        chkSAComp.Visible = false;
                    }
                }
                else
                {
                    btnSaveSA.Visible = false;
                    chkSAComp.Visible = false;
                }
            }
        }
    }
    public void BindJobResponsibility()
    {
        string sql = "";
        if (Mode == "A")
        {
            sql = "SELECT *, ISNULL(Answer,0) AS Answer1 FROM dbo.HR_EmployeePeapJobResponsibility WHERE PEAPID=" + PeapID + " AND  AppraiserByUser = " + ddlAppraiser.SelectedValue.Trim().ToString() + " ";
        }
        else
        {
            sql = "SELECT *, ISNULL(Answer,0) AS Answer1 FROM dbo.HR_EmployeePeapJobResponsibility WHERE PEAPID=" + PeapID + " AND  AppraiserByUser = " + EmpID.ToString() + " ";
        }

        DataTable Dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);

        rptPerformanceOnTheJob.DataSource = Dt;
        rptPerformanceOnTheJob.DataBind();

        string str = "SELECT [Status],EmpId,AppraiserByUser FROM HR_EmployeePeapMaster PM INNER JOIN HR_EmployeePeap_Appraisers PA ON PM.PeapId = PA.PeapId " +
                     "WHERE PM.PeapId =  " + PeapID.ToString() + " AND PM.EmpId <> " + EmpID.ToString() + " AND AppraiserByUser = " + EmpID.ToString() + " ";
        
        DataTable dtStatus = Common.Execute_Procedures_Select_ByQueryCMS(str);

        if (dtStatus != null)
        {
            if (Mode == "A")
            {
                btnSaveJR.Visible = false;
            }
            else
            {
                if (dtStatus.Rows.Count > 0)
                {
                    if (dtStatus.Rows[0]["Status"].ToString() == "2")
                    {
                        btnSaveJR.Visible = true;
                    }
                    else
                    {
                        btnSaveJR.Visible = false;
                    }
                }
                else
                {
                    btnSaveJR.Visible = false;
                }
            }
        }

        if (Dt.Rows.Count > 0)
        {
            ddlScale_SelectedIndexChanged(new object(), new EventArgs());
        }
        
    }
    public void BindCompetency()
    {
        string sql = "";

        if (Mode == "A")
        {
            sql = "SELECT *, ISNULL(Answer,0) AS Answer1 FROM dbo.HR_EmployeeCompetency WHERE PEAPID=" + PeapID + " AND  AppraiserByUser = " + ddlAppraiser.SelectedValue.Trim().ToString() + " ";
        }
        else
        {
            sql = "SELECT *, ISNULL(Answer,0) AS Answer1 FROM dbo.HR_EmployeeCompetency WHERE PEAPID=" + PeapID + " AND  AppraiserByUser = " + EmpID.ToString() + " ";
        }
        
        DataTable Dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);

        rptCompetency.DataSource = Dt;
        rptCompetency.DataBind();

        string str = "SELECT [Status],EmpId,AppraiserByUser FROM HR_EmployeePeapMaster PM INNER JOIN HR_EmployeePeap_Appraisers PA ON PM.PeapId = PA.PeapId " +
                     "WHERE PM.PeapId =  " + PeapID.ToString() + " AND PM.EmpId <> " + EmpID.ToString() + " AND AppraiserByUser = " + EmpID.ToString() + " ";

        DataTable dtStatus = Common.Execute_Procedures_Select_ByQueryCMS(str);

        if (dtStatus != null)
        {
            if (Mode == "A")
            {
                btnSave_Comp.Visible = false;
            }
            else
            {
                if (dtStatus.Rows.Count > 0)
                {
                    if (dtStatus.Rows[0]["Status"].ToString() == "2")
                    {
                        btnSave_Comp.Visible = true;
                    }
                    else
                    {
                        btnSave_Comp.Visible = false;
                    }
                }
                else
                {
                    btnSave_Comp.Visible = false;
                }
            }
        }

        if (Dt.Rows.Count > 0)
        {
            ddlScale_C_SelectedIndexChanged(new object(), new EventArgs());
        }
        
    }
    public void BindPotential()
    {
        string strSQL = "";

        if (Mode == "A")
        {
            strSQL = "SELECT [Status],AppraiserAddResponse,ISNULL(ReadyPromo,'') AS ReadyPromo,UnsuitableReason,MoreTimeReason FROM HR_EmployeePeapMaster PM " +
                        "INNER JOIN HR_EmployeePeap_Appraisers PA ON PM.PeapId = PA.PeapId " +
                        "WHERE PM.PEAPID= " + PeapID.ToString() + " AND  PA.AppraiserByUser = " + ddlAppraiser.SelectedValue.Trim().ToString() + " ";
        }
        else
        {
            strSQL = "SELECT [Status],AppraiserAddResponse,ISNULL(ReadyPromo,'') AS ReadyPromo,UnsuitableReason,MoreTimeReason FROM HR_EmployeePeapMaster PM " +
                        "INNER JOIN HR_EmployeePeap_Appraisers PA ON PM.PeapId = PA.PeapId " +
                        "WHERE PM.PEAPID= " + PeapID.ToString() + " AND PM.EmpId <> " + EmpID.ToString() + " AND  PA.AppraiserByUser = " + EmpID.ToString() + " ";
        }

        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(strSQL);

        if (dt != null)
        {
            if (dt.Rows.Count > 0)
            {
                txtPotAdditionalResponsibilities.Text = dt.Rows[0]["AppraiserAddResponse"].ToString();
                if (dt.Rows[0]["ReadyPromo"].ToString() != "")
                {
                    rdoReadyForPromotion.SelectedValue = dt.Rows[0]["ReadyPromo"].ToString();
                }
                txtLaterPromotionReasion.Text = dt.Rows[0]["UnsuitableReason"].ToString();
                txtTrainingNeed.Text = dt.Rows[0]["MoreTimeReason"].ToString();
            }


            if (Mode == "A")
            {
                btnSave_Pot.Visible = false;
            }
            else
            {
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["Status"].ToString() == "2")
                    {
                        btnSave_Pot.Visible = true;
                    }
                    else
                    {
                        btnSave_Pot.Visible = false;
                    }
                }
                else
                {
                    btnSave_Pot.Visible = false;
                }
            }
        }

    }
    public void BindAppraisers()
    {
        string strSQL = "SELECT EMPID ,FIRSTNAME + ' ' + MIDDLENAME + ' ' + FAMILYNAME AS EMPNAME FROM HR_EmployeePeap_Appraisers PA " +
                        "INNER JOIN Hr_PersonalDetails PD ON PA.AppraiserByUser=PD.EMPID " +
                        "WHERE PA.PEAPID= " + PeapID.ToString() + " ORDER BY FIRSTNAME ";

        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(strSQL);

        ddlAppraiser.DataSource = dt;
        ddlAppraiser.DataTextField = "EMPNAME";
        ddlAppraiser.DataValueField = "EMPID";
        ddlAppraiser.DataBind();

        ddlAppraiser.Items.Insert(0, new ListItem("< Select >", "0"));
    }

    //--------------- Calculate Total/Performance Score --------------------------
    protected void ddlScale_SelectedIndexChanged(object sender, EventArgs e)
    {
        decimal TotalScore = 0;
        decimal PerformanceScore = 0;

        foreach (RepeaterItem rpt in rptPerformanceOnTheJob.Items)
        {
            DropDownList ddlScale = (DropDownList) rpt.FindControl("ddlScale");

            TotalScore = TotalScore + Common.CastAsDecimal(ddlScale.SelectedValue.Trim());
        }

        PerformanceScore = (TotalScore / rptPerformanceOnTheJob.Items.Count);

        lblTotScore_JS.Text = TotalScore.ToString() == "0" ? "" : TotalScore.ToString();
        lblPerformanceScore_JS.Text = PerformanceScore.ToString() == "0" ? "" : PerformanceScore.ToString();

        //------------------ For Competency ---------------------------------

       
    }
    protected void ddlScale_C_SelectedIndexChanged(object sender, EventArgs e)
    {
        decimal TotalScore_C = 0;
        decimal PerformanceScore_C = 0;

        foreach (RepeaterItem rpt in rptCompetency.Items)
        {
            DropDownList ddlScale_C = (DropDownList)rpt.FindControl("ddlScale_C");

            TotalScore_C = TotalScore_C + Common.CastAsDecimal(ddlScale_C.SelectedValue.Trim());
        }

        PerformanceScore_C = (TotalScore_C / rptCompetency.Items.Count);

        lblTotScore_Comp.Text = TotalScore_C.ToString() == "0" ? "" : TotalScore_C.ToString();
        lblPerformanceScore_Comp.Text = PerformanceScore_C.ToString() == "0" ? "" : PerformanceScore_C.ToString();
    }
    protected void ddlAppraiser_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindJobResponsibility();
        BindCompetency();
        BindPotential();
    }

    //--------------- Save of All Tabs --------------------------
    protected void btnSaveSA_Click(object sender, EventArgs e)
    {
        if (chkSAComp.Checked)
        {
            foreach (RepeaterItem rptItem in rptSelfAssessment.Items)
            {
                TextBox txtAnswer = (TextBox)rptItem.FindControl("txtAnswer");

                if (txtAnswer.Text.Trim() == "")
                {
                    lblSAmsg.Text = "Please enter answer.";
                    txtAnswer.Focus();
                    return;
                }
            }
        }

        bool Success = true;

        foreach (RepeaterItem rpt in rptSelfAssessment.Items)
        {
            TextBox txtAnswer = (TextBox)rpt.FindControl("txtAnswer");
            HiddenField hfQID = (HiddenField)rpt.FindControl("hfQID");

            string strUpdate = "UPDATE HR_EmployeePeapSADetails SET  Answer = '" + txtAnswer.Text.Trim() + "' WHERE PeapId = " + PeapID.ToString() + " AND QID = " + hfQID.Value.Trim() + " ; SELECT -1; ";
            DataTable dtResult = Common.Execute_Procedures_Select_ByQueryCMS(strUpdate);

            if (!(dtResult.Rows.Count > 0))
            {
                Success = false;
            }
        }

        if (Success)
        {
            if (chkSAComp.Checked)
            {
                string Update = "UPDATE HR_EmployeePeapMaster SET Status = 1 WHERE PeapId = " + PeapID.ToString() + " ; SELECT -1;";
                DataTable Result = Common.Execute_Procedures_Select_ByQueryCMS(Update);

                if (Result.Rows.Count > 0)
                {
                    BindSelfAssessment();
                    lblSAmsg.Text = "Record saved successfully.";
                }
                else
                {
                    lblSAmsg.Text = "Unable to save record.";
                }
            }
            else
            {
                BindSelfAssessment();
                lblSAmsg.Text = "Record saved successfully.";
            }
        }
        else
        {
            lblSAmsg.Text = "Unable to save record.";
        }
    }
    protected void btnSaveJR_Click(object sender, EventArgs e)
    {

        foreach (RepeaterItem rptItem in rptPerformanceOnTheJob.Items)
        {
            DropDownList ddlScale = (DropDownList)rptItem.FindControl("ddlScale");

            if (ddlScale.SelectedIndex == 0)
            {
                lblMsg_JR.Text = "Please select scale.";
                ddlScale.Focus();
                return;
            }
        }
        

        bool Success = true;

        foreach (RepeaterItem rpt in rptPerformanceOnTheJob.Items)
        {
            DropDownList ddlScale = (DropDownList)rpt.FindControl("ddlScale");
            TextBox txtAnswerJR = (TextBox)rpt.FindControl("txtAnswerJR");
            HiddenField hfJSId = (HiddenField)rpt.FindControl("hfJSId");

            string strUpdate = "UPDATE HR_EmployeePeapJobResponsibility SET Answer = " + ddlScale.SelectedValue.Trim() + ", Remark = '" + txtAnswerJR.Text.Trim() + "'  WHERE PeapId =" + PeapID.ToString() + " AND  AppraiserByUser = " + EmpID.ToString() + " AND JSId = " + hfJSId.Value.Trim() + " ; UPDATE HR_EmployeePeap_Appraisers SET AppraisedOn = GetDate() , IsJSFilled = 1  WHERE PeapId = " + PeapID.ToString() + " AND  AppraiserByUser = " + EmpID.ToString() + " ; SELECT -1; ";
            DataTable dtResult = Common.Execute_Procedures_Select_ByQueryCMS(strUpdate);

            if (!(dtResult.Rows.Count > 0))
            {
                Success = false;
            }
        }

        if (Success)
        {
            Common.Set_Procedures("HR_Peap_CheckFilledAppraisal");
            Common.Set_ParameterLength(1);
            Common.Set_Parameters(new MyParameter("@PEAPID", PeapID));
            
            DataSet ds = new DataSet();

            if (Common.Execute_Procedures_IUD_CMS(ds))
            {
                lblMsg_JR.Text = "Record saved successfully.";
            }
            else
            {
                lblMsg_JR.Text = "Unable to save record.";
            }
        }
        else
        {
            lblMsg_JR.Text = "Unable to save record.";
        }

    }
    protected void btnSave_Comp_Click(object sender, EventArgs e)
    {
        foreach (RepeaterItem rptItem in rptCompetency.Items)
        {
            DropDownList ddlScale_C = (DropDownList)rptItem.FindControl("ddlScale_C");

            if (ddlScale_C.SelectedIndex == 0)
            {
                lblMsg_Comp.Text = "Please select scale.";
                ddlScale_C.Focus();
                return;
            }
            
        }


        bool Success = true;

        foreach (RepeaterItem rpt in rptCompetency.Items)
        {
            DropDownList ddlScale_C = (DropDownList)rpt.FindControl("ddlScale_C");
            HiddenField hfComp = (HiddenField)rpt.FindControl("hfComp");

            string strUpdate = "UPDATE HR_EmployeeCompetency SET Answer = " + ddlScale_C.SelectedValue.Trim() + " WHERE PeapId =" + PeapID.ToString() + " AND  AppraiserByUser = " + EmpID.ToString() + " AND CId = " + hfComp.Value.Trim() + " ; UPDATE HR_EmployeePeap_Appraisers SET AppraisedOn = GetDate() , IsCompFilled = 1  WHERE PeapId = " + PeapID.ToString() + " AND  AppraiserByUser = " + EmpID.ToString() + " ; SELECT -1; ";
            DataTable dtResult = Common.Execute_Procedures_Select_ByQueryCMS(strUpdate);

            if (!(dtResult.Rows.Count > 0))
            {
                Success = false;
            }
        }

        if (Success)
        {
            Common.Set_Procedures("HR_Peap_CheckFilledAppraisal");
            Common.Set_ParameterLength(1);
            Common.Set_Parameters(new MyParameter("@PEAPID", PeapID));

            DataSet ds = new DataSet();

            if (Common.Execute_Procedures_IUD_CMS(ds))
            {
                lblMsg_Comp.Text = "Record saved successfully.";
            }
            else
            {
                lblMsg_Comp.Text = "Unable to save record.";
            }
        }
        else
        {
            lblMsg_Comp.Text = "Unable to save record.";
        }
      
    }
    protected void btnSave_Pot_Click(object sender, EventArgs e)
    {
        if (txtPotAdditionalResponsibilities.Text.Trim() == "")
        {
            lblMsg_Pot.Text = "Please enter additional responsibilities.";
            txtPotAdditionalResponsibilities.Focus();
            return;
        }
        if (rdoReadyForPromotion.SelectedIndex == -1)
        {
            lblMsg_Pot.Text = "Please select ready for promotion.";
            rdoReadyForPromotion.Focus();
            return;
        }
        if ((rdoReadyForPromotion.SelectedValue == "L") && (txtLaterPromotionReasion.Text.Trim() == ""))
        {
            lblMsg_Pot.Text = "Please enter reason.";
            txtLaterPromotionReasion.Focus();
            return;
        }
        

        string strUpdate = "UPDATE HR_EmployeePeap_Appraisers  SET AppraiserAddResponse = '" + txtPotAdditionalResponsibilities.Text.Trim() + "' , ReadyPromo = '" + rdoReadyForPromotion.SelectedValue.Trim() + "' , UnsuitableReason = '" + txtLaterPromotionReasion.Text.Trim() + "',MoreTimeReason = '" + txtTrainingNeed.Text.Trim() + "', IsPotFilled = 1 " +
                           "WHERE PeapId = " + PeapID.ToString() + " AND AppraiserByUser = " + EmpID.ToString() + " ; SELECT -1 ";
           
        DataTable dtResult = Common.Execute_Procedures_Select_ByQueryCMS(strUpdate);

        if (dtResult.Rows.Count > 0)
        {
            Common.Set_Procedures("HR_Peap_CheckFilledAppraisal");
            Common.Set_ParameterLength(1);
            Common.Set_Parameters(new MyParameter("@PEAPID", PeapID));

            DataSet ds = new DataSet();

            if (Common.Execute_Procedures_IUD_CMS(ds))
            {
                lblMsg_Pot.Text = "Record saved successfully.";
            }
            else
            {
                lblMsg_Pot.Text = "Unable to save record.";
            }
        }
    }
}
