using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using ShipSoft.CrewManager.BusinessObjects;
using ShipSoft.CrewManager.BusinessLogicLayer;

public partial class emtm_StaffAdmin_Emtm_Peap_AppraisersEntry : System.Web.UI.Page
{
    string MailClient = ConfigurationManager.AppSettings["SMTPServerName"].ToString();
    int Port = Convert.ToInt32(ConfigurationManager.AppSettings["SMTPPort"]);
    string SenderAddress = ConfigurationManager.AppSettings["FromAddress"];
    string SenderUserName = ConfigurationManager.AppSettings["SMTPUserName"];
    string SenderPass = ConfigurationManager.AppSettings["SMTPUserPwd"];

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
    public int PeapEmpID
    {
        get
        {
            return Common.CastAsInt32(ViewState["PeapEmpID"]);
        }
        set
        {
            ViewState["PeapEmpID"] = value;
        }
    }
    public int AppraiserId
    {
        get
        {
            return Common.CastAsInt32(ViewState["AppraiserId"]);
        }
        set
        {
            ViewState["AppraiserId"] = value;
        }
    }
    public string Mode
    {
        get
        {
            return "" + ViewState["Mode"].ToString();
        }
        set
        {
            ViewState["Mode"] = value;
        }
    }
    public int JSID
    {
        get
        {
            return Common.CastAsInt32(ViewState["JSID"]);
        }
        set
        {
            ViewState["JSID"] = value;
        }
    }
    public Authority Auth
    {
        get
        {
           

            if (Session["Auth"] == null)
            {
                ProcessCheckAuthority Obj = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 1);
                Obj.Invoke();
                Session["Authority"] = Obj.Authority;
                return Obj.Authority;
            }
            else
                return (Authority)Session["Auth"];

        }
        set
        {
            ViewState["Auth"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        lblMsg_JR.Text = "";
        lblMsg_Comp.Text = "";
        lblMsg_Pot.Text = "";
        
        if (!Page.IsPostBack)
        {
            if (Request.QueryString["PeapID"] != null || Request.QueryString["PeapID"].ToString() != "")
            {
                PeapID = Common.CastAsInt32(Request.QueryString["PeapID"].ToString());
                Mode = Request.QueryString["LoginMode"].ToString();
                AppraiserId = Common.CastAsInt32(Request.QueryString["AppraiserId"].ToString());

                if (Mode == "A")
                {
                    EmpID = Common.CastAsInt32(Session["EmpId"]);
                }
                else
                {
                    EmpID = Common.CastAsInt32(Session["ProfileId"]);
                }

                ShowRecord();
                BindJobResponsibility();
                BindCompetency();
                BindPotential();
                CheckPerfForPromotion();
            }
        }
    }
    public void ShowRecord()
    {
        string strSQL = "SELECT PEAPID,CATEGORY As PeapLevel,CASE Occasion WHEN 'R' THEN 'Routine' WHEN 'I' THEN 'Interim' ELSE '' END AS Occasion, POSITIONNAME,EMPCODE,FIRSTNAME , FAMILYNAME,D.DeptName AS DepartmentName, " +
                        "Replace(Convert(Varchar,PM.PEAPPERIODFROM,106),' ','-') AS PEAPPERIODFROM ,Replace(Convert(Varchar,PM.PEAPPERIODTO,106),' ','-') AS PEAPPERIODTO,Replace(Convert(Varchar,PD.DJC,106),' ','-') AS DJC,PM.Status,(SELECT FIRSTNAME + ' ' + FAMILYNAME FROM Hr_PersonalDetails WHERE EMPID=" + AppraiserId.ToString() + ") AS AppraiserName,PM.EMPID " +
                        "FROM HR_EmployeePeapMaster PM  " +
                        "INNER JOIN dbo.Hr_PersonalDetails PD ON PM.EMPID=PD.EMPID  " +
                        "LEFT JOIN dbo.HR_PeapCategory PC ON PC.PID= PM.PeapCategory  " +
                        "LEFT JOIN POSITION P ON P.POSITIONID=PD.POSITION  " +
                        "LEFT JOIN HR_Department D ON D.DeptId=PD.DEPARTMENT  " +
                        "WHERE PM.PeapId = " + PeapID.ToString() + " ";

        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(strSQL);

        
        if (dt.Rows.Count > 0)
        {

            PeapEmpID = Common.CastAsInt32(dt.Rows[0]["EMPID"]);
            lblPeapLevel.Text = dt.Rows[0]["PeapLevel"].ToString();
            txtOccasion.Text = dt.Rows[0]["Occasion"].ToString();
            txtFirstName.Text = dt.Rows[0]["FIRSTNAME"].ToString();
            txtLastName.Text = dt.Rows[0]["FAMILYNAME"].ToString();
            lblAppraiserName.Text = "|  Appraiser Name : " +  dt.Rows[0]["AppraiserName"].ToString();
            ViewState["AppraiserName"] = dt.Rows[0]["AppraiserName"].ToString();
            
            int PStatus = Common.CastAsInt32(dt.Rows[0]["Status"]);
            switch (PStatus)
            {
                case -1:
                    lblPeapStatus.Text = "PEAP Cancelled";
                    break;
                case 0:
                    lblPeapStatus.Text = "Self Assessment";
                    break;
                case 1:
                    lblPeapStatus.Text = "Self Assessment";
                    break;
                case 2:
                    lblPeapStatus.Text = "Assessment by Appraiser";
                    break;
                case 3:
                    lblPeapStatus.Text = "Assessment by MD";
                    break;
                case 4:
                    lblPeapStatus.Text = "PEAP Closed";
                    break;
                case 5:
                    lblPeapStatus.Text = "Assessment by Management";
                    break;
                case 6:
                    lblPeapStatus.Text = "Assessment by Management";
                    break;
                default:
                    lblPeapStatus.Text = "NA";
                    break;
            }
            lblPeapStatus.Text = "Current Status : " + lblPeapStatus.Text;

            if (Mode == "A")
            {
                btnSaveJR.Visible = false;
                btnSaveKPI.Visible = false;
                btnSave_Comp.Visible = false;
                btnSave_Pot.Visible = false;
                btnNotify.Visible = false;
                Button1.Visible = false;
                Button2.Visible = false;
                btnTrainingNeed.Visible = false;

            }
            else
            {
                if (dt.Rows[0]["Status"].ToString() == "2" && EmpID.ToString() == AppraiserId.ToString())
                {
                    btnSaveJR.Visible = true;
                    btnSaveKPI.Visible = true;
                    btnSave_Comp.Visible = true;
                    btnSave_Pot.Visible = true;

                    CheckToNotify();
                    btnTrainingNeed.Visible = true;
                }
                else
                {
                    btnSaveJR.Visible = false;
                    btnSaveKPI.Visible = false;
                    btnSave_Comp.Visible = false;
                    btnSave_Pot.Visible = false;

                    //btnNotify.Visible = false;
                    //Button1.Visible = false;
                    //Button2.Visible = false;
                    
                    btnTrainingNeed.Visible = false;
                }
            }

        }
        

    }
    //--------------- Bind All Grids --------------------------
    public void BindJobResponsibility()
    {
        string sql = "SELECT ROW_NUMBER() OVER (ORDER BY JSID) AS SNO,*, ISNULL(convert( DECIMAL(18,2),Answer),0) AS Answer1,(SELECT [Status] FROM HR_EmployeePeapMaster WHERE PeapId = " + PeapID + " ) AS Status, Waitage, " +
                     "(SELECT convert(DECIMAL(18,2),(convert( DECIMAL(18,2),SUM(Rating))/convert( DECIMAL(18,2),count(*)))) FROM HR_EmployeePeapJobResponsibility_KPI kpi WHERE ISNULL(Rating,0)>=0 and kpi.PeapId= " + PeapID + " AND kpi.AppraiserByUser = " + AppraiserId.ToString() + " AND kpi.JSID= EJS.JSID) AS Rating, " +
                     "(SELECT count(*)*5 FROM HR_EmployeePeapJobResponsibility_KPI kpi WHERE ISNULL(Rating,0)>=0 AND kpi.PeapId= " + PeapID + " AND kpi.AppraiserByUser = " + AppraiserId.ToString() + " AND kpi.JSID= EJS.JSID) AS TARGET, " +
                     "(SELECT count(*) FROM HR_EmployeePeapJobResponsibility_KPI kpi WHERE ISNULL(Rating,0)>=0  AND kpi.PeapId= " + PeapID + " AND kpi.AppraiserByUser = " + AppraiserId.ToString() + " AND kpi.JSID= EJS.JSID) AS NOQ, " +
                     "(SELECT convert(DECIMAL(18,2),SUM(Rating)) FROM HR_EmployeePeapJobResponsibility_KPI kpi WHERE kpi.PeapId= " + PeapID + " AND kpi.AppraiserByUser = " + AppraiserId.ToString() + " AND kpi.JSID= EJS.JSID) AS ACHEIVED " +
                     "FROM dbo.HR_EmployeePeapJobResponsibility EJS  WHERE EJS.PEAPID=" + PeapID + " AND  EJS.AppraiserByUser = " + AppraiserId.ToString() + " ORDER BY JSID";
        
        DataTable Dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);

        rptPerformanceOnTheJob.DataSource = Dt;
        rptPerformanceOnTheJob.DataBind();

        //string str = "SELECT [Status],EmpId,AppraiserByUser FROM HR_EmployeePeapMaster PM INNER JOIN HR_EmployeePeap_Appraisers PA ON PM.PeapId = PA.PeapId " +
        //             "WHERE PM.PeapId =  " + PeapID.ToString() + " AND PM.EmpId <> " + EmpID.ToString() + " AND AppraiserByUser = " + AppraiserId.ToString() + " ";

        //DataTable dtStatus = Common.Execute_Procedures_Select_ByQueryCMS(str);

        //if (dtStatus != null)
        //{
        //    if (Mode == "A")
        //    {
        //        btnSaveJR.Visible = false;
        //    }
        //    else
        //    {
        //        if (dtStatus.Rows.Count > 0)
        //        {
        //            if (dtStatus.Rows[0]["Status"].ToString() == "2")
        //            {
        //                btnSaveJR.Visible = true;
        //            }
        //            else
        //            {
        //                btnSaveJR.Visible = false;
        //            }
        //        }
        //        else
        //        {
        //            btnSaveJR.Visible = false;
        //        }
        //    }
        //}

        if (Dt.Rows.Count > 0)
        {
            txtRating_TextChanged(new object(), new EventArgs());
        }
        CheckPerfForPromotion();
        
    }
    protected void rptPerformanceOnTheJob_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            double Rating = Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Rating").ToString() == "" ? "0.0" : DataBinder.Eval(e.Item.DataItem, "Rating").ToString());
            double weightage = Convert.ToDouble(Common.CastAsDecimal(DataBinder.Eval(e.Item.DataItem, "waitage").ToString()));
            int Status = Common.CastAsInt32(DataBinder.Eval(e.Item.DataItem, "Status").ToString());
            string Remark = DataBinder.Eval(e.Item.DataItem, "Remark").ToString();

            TextBox txtRating = (TextBox)e.Item.FindControl("txtRating");

            double Scale = Convert.ToDouble((Rating * weightage) / 100.00);

            txtRating.Text = Scale.ToString() == "0" ? "" : Math.Round(Scale,2).ToString();

            //string asdf = string.Format("{0.00}", Scale);

            //txtRating.Text = asdf == "0" ? "" : asdf;


            ImageButton imgRemark = (ImageButton)e.Item.FindControl("imgRemark");

            if (Status >= 3)
            {
                if (Remark == "")
                {
                    imgRemark.Visible = false;
                }
                else
                {
                    imgRemark.ToolTip = "View Remark";
                    imgRemark.ImageUrl = "~/Modules/HRD/Images/icon_comment.gif";
                    imgRemark.Visible = true;
                }
            }
            else
            {
                if (Remark == "")
                {
                    imgRemark.ToolTip = "Enter Remark";
                    imgRemark.ImageUrl = "~/Modules/HRD/Images/AddPencil.gif";

                }
                else
                {
                    imgRemark.ToolTip = "View Remark";
                    imgRemark.ImageUrl = "~/Modules/HRD/Images/icon_comment.gif";
                }

                imgRemark.Visible = true;
            }
        }
    }
    public void BindCompetency()
    {
        string sql = "SELECT *, ISNULL(Answer,'') AS Answer1 FROM dbo.HR_EmployeeCompetency WHERE PEAPID=" + PeapID + " AND  AppraiserByUser = " + AppraiserId.ToString() + " ORDER BY CID";
        

        DataTable Dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);

        rptCompetency.DataSource = Dt;
        rptCompetency.DataBind();

        //string str = "SELECT [Status],EmpId,AppraiserByUser FROM HR_EmployeePeapMaster PM INNER JOIN HR_EmployeePeap_Appraisers PA ON PM.PeapId = PA.PeapId " +
        //             "WHERE PM.PeapId =  " + PeapID.ToString() + " AND PM.EmpId <> " + EmpID.ToString() + " AND AppraiserByUser = " + AppraiserId.ToString() + " ";

        //DataTable dtStatus = Common.Execute_Procedures_Select_ByQueryCMS(str);

        //if (dtStatus != null)
        //{
        //    //if (Mode == "A")
        //    //{
        //    //    btnSave_Comp.Visible = false;
        //    //}
        //    //else
        //    //{
        //    //    if (dtStatus.Rows.Count > 0)
        //    //    {
        //    //        if (dtStatus.Rows[0]["Status"].ToString() == "2")
        //    //        {
        //    //            btnSave_Comp.Visible = true;
        //    //        }
        //    //        else
        //    //        {
        //    //            btnSave_Comp.Visible = false;
        //    //        }
        //    //    }
        //    //    else
        //    //    {
        //    //        btnSave_Comp.Visible = false;
        //    //    }
        //    //}
        //}

        if (Dt.Rows.Count > 0)
        {
            txtRating_Comp_TextChanged(new object(), new EventArgs());
        }
        CheckPerfForPromotion();

    }
    public void BindPotential()
    {
        string strSQL = "SELECT [Status],AppraiserAddResponse,ISNULL(ReadyPromo,'') AS ReadyPromo,UnsuitableReason,MoreTimeReason FROM HR_EmployeePeapMaster PM " +
                        "INNER JOIN HR_EmployeePeap_Appraisers PA ON PM.PeapId = PA.PeapId " +
                        "WHERE PM.PEAPID= " + PeapID.ToString() + " AND  PA.AppraiserByUser = " + AppraiserId.ToString() + " ";
        

        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(strSQL);

        if (dt != null)
        {
            if (dt.Rows.Count > 0)
            {
                txtPotAdditionalResponsibilities.Text = dt.Rows[0]["AppraiserAddResponse"].ToString();
                if (dt.Rows[0]["ReadyPromo"].ToString().Trim() != "" )
                {
                    rdoReadyForPromotion.SelectedValue = dt.Rows[0]["ReadyPromo"].ToString();
                }
                txtLaterPromotionReasion.Text = dt.Rows[0]["UnsuitableReason"].ToString();
                //txtTrainingNeed.Text = dt.Rows[0]["MoreTimeReason"].ToString();
            }
            
            
            //if (Mode == "A")
            //{
            //    btnSave_Pot.Visible = false;
            //}
            //else
            //{
            //    if (dt.Rows.Count > 0)
            //    {
            //        if (dt.Rows[0]["Status"].ToString() == "2")
            //        {
            //            btnSave_Pot.Visible = true;
            //        }
            //        else
            //        {
            //            btnSave_Pot.Visible = false;
            //        }
            //    }
            //    else
            //    {
            //        btnSave_Pot.Visible = false;
            //    }
            //}
        }
        BindAllDoneTraining();
        BindRecommTraining();
        CheckPerfForPromotion();

    }
    //public void BindAppraisers()
    //{
    //    string strSQL = "SELECT EMPID ,FIRSTNAME + ' ' + MIDDLENAME + ' ' + FAMILYNAME AS EMPNAME FROM HR_EmployeePeap_Appraisers PA " +
    //                    "INNER JOIN Hr_PersonalDetails PD ON PA.AppraiserByUser=PD.EMPID " +
    //                    "WHERE PA.PEAPID= " + PeapID.ToString() + " ORDER BY FIRSTNAME ";

    //    DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(strSQL);

    //    ddlAppraiser.DataSource = dt;
    //    ddlAppraiser.DataTextField = "EMPNAME";
    //    ddlAppraiser.DataValueField = "EMPID";
    //    ddlAppraiser.DataBind();

    //    ddlAppraiser.Items.Insert(0, new ListItem("< Select >", "0"));
    //}

    //--------------- Calculate Total/Performance Score --------------------------

    protected void txtRating_TextChanged(object sender, EventArgs e)
    {
        double TotalScore = 0;
        double PerformanceScore = 0;

        foreach (RepeaterItem rpt in rptPerformanceOnTheJob.Items)
        {
            TextBox txtRating = (TextBox)rpt.FindControl("txtRating");

            if (txtRating.Text != "")
            {
                TotalScore = TotalScore + Convert.ToDouble(txtRating.Text.Trim());
            }
            else
            {
                TotalScore = TotalScore + 0;
            }
        }

        PerformanceScore = Convert.ToDouble(Math.Truncate((TotalScore / rptPerformanceOnTheJob.Items.Count) * 10000) / 10000);        
        
        string[] Colors={"#FF0000","#FF8583","#FFFF00","#3DEB3D","#00AE00"};
       
        lblTotScore_JS.Text = TotalScore.ToString() == "0" ? "" : TotalScore.ToString();        
        lblPerformanceScore_JS.Text = string.Format("{0:0.00}", PerformanceScore);
        
        //int index = Common.CastAsInt32(Math.Ceiling(Common.CastAsDecimal(lblPerformanceScore_JS.Text)))-1;
        int index = Common.CastAsInt32(Math.Ceiling(Common.CastAsDecimal(lblTotScore_JS.Text))) - 1;
        if (index <= 0) { index = 0; }

        JRColor.Style.Add("background-Color", Colors[index]);
        if (index == 2 || index == 3)
            JRColor.Style.Add("color", "Black"); 
        else
            JRColor.Style.Add("color", "White"); 
        //------------------ For Competency ---------------------------------

        
        
    }
    protected void txtRating_Comp_TextChanged(object sender, EventArgs e)
    {
        double TotalScore_C = 0;
        double PerformanceScore_C = 0;

        foreach (RepeaterItem rpt in rptCompetency.Items)
        {
            HiddenField hfComp = (HiddenField)rpt.FindControl("hfComp");
            TextBox txtRating_Comp = (TextBox)rpt.FindControl("txtRating_Comp");

            string Value = txtRating_Comp.Text;
            if (Value == "")
                Value = "null";
            string strUpdate = "UPDATE HR_EmployeeCompetency SET Answer = " + Value + " WHERE PeapId =" + PeapID.ToString() + " AND  AppraiserByUser = " + EmpID.ToString() + " AND CId = " + hfComp.Value.Trim() + " ; UPDATE HR_EmployeePeap_Appraisers SET AppraisedOn = GetDate() , IsCompFilled = 1  WHERE PeapId = " + PeapID.ToString() + " AND  AppraiserByUser = " + EmpID.ToString() + " ; SELECT -1; ";
            DataTable dtResult = Common.Execute_Procedures_Select_ByQueryCMS(strUpdate);


            if (txtRating_Comp.Text != "")
            {
                TotalScore_C = TotalScore_C + Convert.ToDouble(txtRating_Comp.Text.Trim());
            }
            else
            {
                TotalScore_C = TotalScore_C + 0;
            }
        }

        PerformanceScore_C = Convert.ToDouble(Math.Truncate((TotalScore_C / rptCompetency.Items.Count) * 10000) / 10000); 

        string[] Colors = { "#FF0000", "#FF9966", "#33CC66", "#008000" };

        lblTotScore_Comp.Text = TotalScore_C.ToString() == "0" ? "" : TotalScore_C.ToString();
        //lblPerformanceScore_Comp.Text = PerformanceScore_C.ToString() == "0" ? "" : PerformanceScore_C.ToString();
        lblPerformanceScore_Comp.Text = string.Format("{0:0.00}", PerformanceScore_C);

        int index = Common.CastAsInt32(Math.Ceiling(Common.CastAsDecimal(lblPerformanceScore_Comp.Text))) - 1;
        if (index <= 0) { index = 0; }

        CompColor.Style.Add("background-Color", Colors[index]);
        //if (index == 2 || index == 3)
        //    CompColor.Style.Add("color", "Black");
        //else
            CompColor.Style.Add("color", "White");



    }
    //protected void ddlAppraiser_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    BindJobResponsibility();
    //    BindCompetency();
    //    BindPotential();
    //}

    //--------------- Save of All Tabs --------------------------
    //protected void btnSaveSA_Click(object sender, EventArgs e)
    //{
    //    if (chkSAComp.Checked)
    //    {
    //        foreach (RepeaterItem rptItem in rptSelfAssessment.Items)
    //        {
    //            TextBox txtAnswer = (TextBox)rptItem.FindControl("txtAnswer");

    //            if (txtAnswer.Text.Trim() == "")
    //            {
    //                lblSAmsg.Text = "Please enter answer.";
    //                txtAnswer.Focus();
    //                return;
    //            }
    //        }
    //    }

    //    bool Success = true;

    //    foreach (RepeaterItem rpt in rptSelfAssessment.Items)
    //    {
    //        TextBox txtAnswer = (TextBox)rpt.FindControl("txtAnswer");
    //        HiddenField hfQID = (HiddenField)rpt.FindControl("hfQID");

    //        string strUpdate = "UPDATE HR_EmployeePeapSADetails SET  Answer = '" + txtAnswer.Text.Trim() + "' WHERE PeapId = " + PeapID.ToString() + " AND QID = " + hfQID.Value.Trim() + " ; SELECT -1; ";
    //        DataTable dtResult = Common.Execute_Procedures_Select_ByQueryCMS(strUpdate);

    //        if (!(dtResult.Rows.Count > 0))
    //        {
    //            Success = false;
    //        }
    //    }

    //    if (Success)
    //    {
    //        if (chkSAComp.Checked)
    //        {
    //            string Update = "UPDATE HR_EmployeePeapMaster SET Status = 1 WHERE PeapId = " + PeapID.ToString() + " ; SELECT -1;";
    //            DataTable Result = Common.Execute_Procedures_Select_ByQueryCMS(Update);

    //            if (Result.Rows.Count > 0)
    //            {
    //                BindSelfAssessment();
    //                lblSAmsg.Text = "Record saved successfully.";
    //            }
    //            else
    //            {
    //                lblSAmsg.Text = "Unable to save record.";
    //            }
    //        }
    //        else
    //        {
    //            BindSelfAssessment();
    //            lblSAmsg.Text = "Record saved successfully.";
    //        }
    //    }
    //    else
    //    {
    //        lblSAmsg.Text = "Unable to save record.";
    //    }
    //}
    protected void btnSaveJR_Click(object sender, EventArgs e)
    {

        foreach (RepeaterItem rptItem in rptPerformanceOnTheJob.Items)
        {
            TextBox txtRating = (TextBox)rptItem.FindControl("txtRating");

            if (txtRating.Text == "")
            {
                lblMsg_JR.Text = "Please fill rating.";
                txtRating.Focus();
                return;
            }
        }
        

        bool Success = true;

        foreach (RepeaterItem rpt in rptPerformanceOnTheJob.Items)
        {
            //DropDownList ddlScale = (DropDownList)rpt.FindControl("ddlScale");
            TextBox txtRating = (TextBox)rpt.FindControl("txtRating");
            //TextBox txtAnswerJR = (TextBox)rpt.FindControl("txtAnswerJR");
            HiddenField hfJSId = (HiddenField)rpt.FindControl("hfJSId");

            string strUpdate = "UPDATE HR_EmployeePeapJobResponsibility SET Answer = " + txtRating.Text.Trim() + " WHERE PeapId =" + PeapID.ToString() + " AND  AppraiserByUser = " + EmpID.ToString() + " AND JSId = " + hfJSId.Value.Trim() + " ; UPDATE HR_EmployeePeap_Appraisers SET AppraisedOn = GetDate() , IsJSFilled = 1  WHERE PeapId = " + PeapID.ToString() + " AND  AppraiserByUser = " + EmpID.ToString() + " ; SELECT -1; ";
            DataTable dtResult = Common.Execute_Procedures_Select_ByQueryCMS(strUpdate);

            if (!(dtResult.Rows.Count > 0))
            {
                Success = false;
            }
        }

        if (Success)
        {
            //Common.Set_Procedures("HR_Peap_CheckFilledAppraisal");
            //Common.Set_ParameterLength(1);
            //Common.Set_Parameters(new MyParameter("@PEAPID", PeapID));

            //DataSet ds = new DataSet();

            //if (Common.Execute_Procedures_IUD_CMS(ds))
            //{
            lblMsg_JR.Text = "Record saved successfully.";
                CheckPerfForPromotion();
                CheckToNotify();
            //}
            //else
            //{
            //    lblMsg_JR.Text = "Unable to save record.";
            //}
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
            //DropDownList ddlScale_C = (DropDownList)rptItem.FindControl("ddlScale_C");
            TextBox txtRating_Comp = (TextBox)rptItem.FindControl("txtRating_Comp");

            if (txtRating_Comp.Text == "")
            {
                lblMsg_Comp.Text = "Please select scale.";
                txtRating_Comp.Focus();
                return;
            }

        }

        
        bool Success = true;

        foreach (RepeaterItem rpt in rptCompetency.Items)
        {
            //DropDownList ddlScale_C = (DropDownList)rpt.FindControl("ddlScale_C");
            TextBox txtRating_Comp = (TextBox)rpt.FindControl("txtRating_Comp");
            HiddenField hfComp = (HiddenField)rpt.FindControl("hfComp");

            string strUpdate = "UPDATE HR_EmployeeCompetency SET Answer = " + txtRating_Comp.Text.Trim() + " WHERE PeapId =" + PeapID.ToString() + " AND  AppraiserByUser = " + EmpID.ToString() + " AND CId = " + hfComp.Value.Trim() + " ; UPDATE HR_EmployeePeap_Appraisers SET AppraisedOn = GetDate() , IsCompFilled = 1  WHERE PeapId = " + PeapID.ToString() + " AND  AppraiserByUser = " + EmpID.ToString() + " ; SELECT -1; ";
            DataTable dtResult = Common.Execute_Procedures_Select_ByQueryCMS(strUpdate);

            if (!(dtResult.Rows.Count > 0))
            {
                Success = false;
            }
        }

        if (Success)
        {
            //Common.Set_Procedures("HR_Peap_CheckFilledAppraisal");
            //Common.Set_ParameterLength(1);
            //Common.Set_Parameters(new MyParameter("@PEAPID", PeapID));

            //DataSet ds = new DataSet();

            //if (Common.Execute_Procedures_IUD_CMS(ds))
            //{
            lblMsg_Comp.Text = "Record saved successfully.";
                CheckPerfForPromotion();
                CheckToNotify();
            //}
            //else
            //{
            //    lblMsg_Comp.Text = "Unable to save record.";
            //}
        }
        else
        {
            lblMsg_Comp.Text = "Unable to save record.";
        }

    }
    protected void btnSave_Pot_Click(object sender, EventArgs e)
    {
        CheckPerfForPromotion();

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
        if ((rdoReadyForPromotion.SelectedValue == "A") && (txtLaterPromotionReasion.Text.Trim() == ""))
        {
            lblMsg_Pot.Text = "Please enter reason.";
            txtLaterPromotionReasion.Focus();
            return;
        }


        string strUpdate = "UPDATE HR_EmployeePeap_Appraisers  SET AppraiserAddResponse = '" + txtPotAdditionalResponsibilities.Text.Trim().Replace("'","''") + "' , ReadyPromo = '" + rdoReadyForPromotion.SelectedValue.Trim() + "' , UnsuitableReason = '" + txtLaterPromotionReasion.Text.Trim().Replace("'","''") + "', IsPotFilled = 1 " +
                           "WHERE PeapId = " + PeapID.ToString() + " AND AppraiserByUser = " + EmpID.ToString() + " ; SELECT -1 ";

        DataTable dtResult = Common.Execute_Procedures_Select_ByQueryCMS(strUpdate);

        if (dtResult.Rows.Count > 0)
        {
            //Common.Set_Procedures("HR_Peap_CheckFilledAppraisal");
            //Common.Set_ParameterLength(1);
            //Common.Set_Parameters(new MyParameter("@PEAPID", PeapID));

            //DataSet ds = new DataSet();

            //if (Common.Execute_Procedures_IUD_CMS(ds))
            //{
            lblMsg_Pot.Text = "Record saved successfully.";
                CheckToNotify();
            //}
            //else
            //{
            //    lblMsg_Pot.Text = "Unable to save record.";
            //}
        }
    }
    protected void btnBack_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("Emtm_PeapSummary.aspx?PeapID=" + PeapID.ToString() + "&Mode=" + Mode);
    }

    protected void imgGuidelines_Click(object sender, ImageClickEventArgs e)
    {
        int CID = Common.CastAsInt32(((ImageButton)sender).CommandArgument);

        ScriptManager.RegisterStartupScript(this, this.GetType(), "openpopup", "showGuidance('" + CID + "');", true);
    }
    protected void imgRemark_Click(object sender, ImageClickEventArgs e)
    {
        int JSID = Common.CastAsInt32(((ImageButton)sender).CommandArgument);


        ScriptManager.RegisterStartupScript(this, this.GetType(), "openpopup", "enterremark( '" + PeapID + "','" + JSID + "','" + AppraiserId + "');", true);
    }

    public void CheckPerfForPromotion()
    {
        decimal Score_JR = Common.CastAsDecimal(lblTotScore_JS.Text.Trim());
        decimal Score_Comp = Common.CastAsDecimal(lblPerformanceScore_Comp.Text.Trim());

        if ((Score_JR <= 3) || (Score_Comp <= 3))
        {
            rdoReadyForPromotion.SelectedValue = "N";
            rdoReadyForPromotion.Enabled = false;

            txtLaterPromotionReasion.Text = "INSUFFICIENT SCORES.";
            //txtLaterPromotionReasion.Attributes.Add("required", "yes");
        }
        else
        {
            rdoReadyForPromotion.Enabled = true;
            //txtLaterPromotionReasion.Text = "";
            //txtLaterPromotionReasion.Attributes.Remove("required");
        }
    }

    protected void btnNotify_Click(object sender, EventArgs e)
    {
        foreach (RepeaterItem rptItem in rptPerformanceOnTheJob.Items)
        {
            TextBox txtRating = (TextBox)rptItem.FindControl("txtRating");

            if (txtRating.Text == "")
            {
                lblMsg_JR.Text = "Please Complete all sections before notify.";
                lblMsg_Comp.Text = "Please Complete all sections before notify.";
                lblMsg_Pot.Text = "Please Complete all sections before notify.";
               
                return;
            }
        }

        foreach (RepeaterItem rptItem in rptCompetency.Items)
        {
            TextBox txtRating_Comp = (TextBox)rptItem.FindControl("txtRating_Comp");

            if (txtRating_Comp.Text == "")
            {
                lblMsg_JR.Text = "Please Complete all sections before notify.";
                lblMsg_Comp.Text = "Please Complete all sections before notify.";
                lblMsg_Pot.Text = "Please Complete all sections before notify.";
                return;
            }

        }

        if (txtPotAdditionalResponsibilities.Text.Trim() == "")
        {
            lblMsg_JR.Text = "Please Complete all sections before notify.";
            lblMsg_Comp.Text = "Please Complete all sections before notify.";
            lblMsg_Pot.Text = "Please Complete all sections before notify.";
            return;
        }

        if (rdoReadyForPromotion.SelectedIndex == -1)
        {
            lblMsg_JR.Text = "Please Complete all sections before notify.";
            lblMsg_Comp.Text = "Please Complete all sections before notify.";
            lblMsg_Pot.Text = "Please Complete all sections before notify.";
            return;
        }

        if ((rdoReadyForPromotion.SelectedValue == "N") && (txtLaterPromotionReasion.Text.Trim() == ""))
        {
            lblMsg_JR.Text = "Please Complete all sections before notify.";
            lblMsg_Comp.Text = "Please Complete all sections before notify.";
            lblMsg_Pot.Text = "Please Complete all sections before notify.";
            return;
        }

        string strSQL = "UPDATE HR_EmployeePeap_Appraisers SET IsNotified = 1 WHERE PeapId = " + PeapID + " AND AppraiserByUser = " + AppraiserId + " ; SELECT -1 ";

        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(strSQL);

        if (dt.Rows.Count > 0)
        {
            Common.Set_Procedures("HR_Peap_CheckFilledAppraisal");
            Common.Set_ParameterLength(1);
            Common.Set_Parameters(new MyParameter("@PEAPID", PeapID));

            DataSet ds = new DataSet();

            if (Common.Execute_Procedures_IUD_CMS(ds))
            {
                Common.Set_Procedures("HR_Peap_AutoForwardToManagement");
                Common.Set_ParameterLength(1);
                Common.Set_Parameters(new MyParameter("@PEAPID", PeapID));

                DataSet ds1 = new DataSet();
                if (Common.Execute_Procedures_IUD_CMS(ds1))
                {
                    lblMsg_JR.Text = "Notified Successfully.";
                    lblMsg_Comp.Text = "Notified Successfully.";
                    lblMsg_Pot.Text = "Notified Successfully.";

                    btnSaveJR.Visible = false;
                    btnSaveKPI.Visible = false;
                    btnSave_Comp.Visible = false;
                    btnSave_Pot.Visible = false;

                    btnNotify.Visible = false;
                    Button1.Visible = false;
                    Button2.Visible = false;
                }
                else
                {
                    lblMsg_JR.Text = "Unable to notify.";
                    lblMsg_Comp.Text = "Unable to notify.";
                    lblMsg_Pot.Text = "Unable to notify.";
                    return;
                }
            }
            else
            {
                lblMsg_JR.Text = "Unable to notify.";
                lblMsg_Comp.Text = "Unable to notify.";
                lblMsg_Pot.Text = "Unable to notify.";
                return;
            }
            
        }
        else
        {
            lblMsg_JR.Text = "Unable to notify.";
            lblMsg_Comp.Text = "Unable to notify.";
            lblMsg_Pot.Text = "Unable to notify.";
            return;
        }

        string checkMailNotify = "SELECT [Status] FROM HR_EmployeePeapMaster WHERE PeapId =" + PeapID.ToString();
        DataTable dtCheckMail = Common.Execute_Procedures_Select_ByQueryCMS(checkMailNotify);

        if (dtCheckMail.Rows[0]["Status"].ToString() == "3")
        {
            string Mess = SendMail();
        }
        
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        if (TabContainer1.ActiveTabIndex == 0)
        {
            BindJobResponsibility();
        }
        
        if (TabContainer1.ActiveTabIndex == 3)
        {
            BindAllDoneTraining();
            BindRecommTraining();
        }
        
    }

    protected void TabContainer1_ActiveTabChanged(object sender, EventArgs e)
    {
        if (TabContainer1.ActiveTabIndex == 0)
        {
            BindJobResponsibility();
        }
        if (TabContainer1.ActiveTabIndex == 1)
        {
            BindCompetency();
        }
        if (TabContainer1.ActiveTabIndex == 2)
        {
            BindPotential();
        }
        if (TabContainer1.ActiveTabIndex == 3)
        {
            BindAllDoneTraining();
        }
    }

    public void CheckToNotify()
    {

        string strNotify = "SELECT * FROM HR_EmployeePeap_Appraisers WHERE PeapId = " + PeapID.ToString() + " AND AppraiserByUser = " + AppraiserId.ToString() + " AND ISNULL(IsJSFilled,0) = 1 AND ISNULL(IsCompFilled,0) = 1 AND ISNULL(IsPotFilled,0) = 1 AND ISNULL(IsNotified,0) = 0 ";

       DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(strNotify);
       if (dt.Rows.Count > 0)
       {
           btnNotify.Visible = true;
           Button1.Visible = true;
           Button2.Visible = true;
       }
       else
       {

            //string strCheck = "SELECT ((SELECT COUNT(*) FROM HR_EmployeePeapJobResponsibility WHERE PeapId = " + PeapID.ToString() + " AND AppraiserByUser = " + AppraiserId.ToString() + " AND Answer IS NULL) + " +
            //                  "(SELECT COUNT(*) FROM HR_EmployeeCompetency WHERE PeapId = " + PeapID.ToString() + " AND AppraiserByUser = " + AppraiserId.ToString() + " AND Answer IS NULL) + " +
            //                  "(SELECT COUNT(*) FROM HR_EmployeePeap_Appraisers WHERE PeapId = " + PeapID.ToString() + " AND AppraiserByUser = " + AppraiserId.ToString() + " AND IsPotFilled IS NULL)) AS AllFilled ";

            //DataTable dtCheck = Common.Execute_Procedures_Select_ByQueryCMS(strCheck);

            //if (dtCheck.Rows[0][""].ToString() == "0")
            //{
            //}

            btnNotify.Visible = false;
           Button1.Visible = false;
           Button2.Visible = false;

       }
    }

    protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
    {
        int TrainingRecommId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);

        string strDeleteSQL = "DELETE FROM HR_Peap_TrainingRecommended WHERE PeapId = " + PeapID.ToString() + "  AND TrainingRecommId = " + TrainingRecommId.ToString() + " ; DELETE FROM HR_TrainingRecommended WHERE TrainingRecommId = " + TrainingRecommId.ToString() + " ; SELECT -1 ";
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(strDeleteSQL);

        if (dt.Rows.Count > 0)
        {
            BindAllDoneTraining();
            BindRecommTraining();
            lblMsg_Pot.Text = "Training deleted successfully.";
        }
        else
        {
            lblMsg_Pot.Text = "Unable to delete training.";
        }
    }
    
    //----------- Mail Code

    public string SendMail()
    {
        string ReplyMess = "";
        string MailFrom = "", MailTo = "emanager@energiossolutions.com";
        string sqlGetEmployeeInfo = "select pd.EmpCode,pd.FirstName +' ' + pd.MiddleName +' ' + pd.FamilyName as [Name], pd.Office,pd.Department,c.OfficeName,pd.Position,dept.DeptName,p.PositionName, " +
                                    "( SELECT REPLACE(CONVERT(VARCHAR(12),PeapPeriodFrom,106),' ','-') + ' - ' + REPLACE(CONVERT(VARCHAR(12),PeapPeriodTo,106),' ','-') FROM HR_EmployeePeapMaster WHERE PeapId = " + PeapID.ToString() + " ) AS Period, " +
                                    "(SELECT PositionName FROM Position AP WHERE AP.PositionId = (SELECT Position FROM Hr_PersonalDetails APD WHERE APD.EmpId = " + AppraiserId.ToString() + ")) AS AppPosition " +
                                    "from Hr_PersonalDetails pd  " +
                                    "left outer join Position p on pd.Position=p.PositionId  " +
                                    "Left Outer Join Office c on pd.Office= c.OfficeId  " +
                                    "Left Outer Join HR_Department dept on pd.Department=dept.DeptId " +
                                    "WHERE pd.EmpId = (SELECT EmpId FROM HR_EmployeePeapMaster WHERE PeapId = " + PeapID.ToString() + " ) ";

        DataTable dtGetEmployeeInfo = Common.Execute_Procedures_Select_ByQueryCMS(sqlGetEmployeeInfo);

        if (dtGetEmployeeInfo != null)
            if (dtGetEmployeeInfo.Rows.Count > 0)
            {
                DataRow drGetEmployeeInfo = dtGetEmployeeInfo.Rows[0];
                //Sending mails
                char[] Sep = { ';' };
                string[] ToAdds = MailTo.ToString().Split(Sep);
                string[] CCAdds = MailTo.ToString().Split(Sep);
                string[] BCCAdds = "".Split(Sep);
                //----------------------
                String Subject = "PEAP for " + drGetEmployeeInfo["Name"].ToString() + " - (" + drGetEmployeeInfo["Period"].ToString() + ") by Appraiser";
                String MailBody;


                MailBody = "<br><br>PEAP for <b>" + drGetEmployeeInfo["Name"].ToString() + " /" + drGetEmployeeInfo["PositionName"].ToString() + "</b> is completed .";
                MailBody = MailBody + "<br><br>Thank You.";
                MailBody = MailBody + "<br>____________________________";
                //MailBody = MailBody + "<br>" + drGetEmployeeInfo["Name"].ToString() + "<br><font color=000080 size=2 face=Century Gothic><strong>" + MailFrom.ToString() + "</strong></font>";
                MailBody = MailBody + "<br>" + UppercaseWords(ViewState["AppraiserName"].ToString());
                MailBody = MailBody + "<br>" + drGetEmployeeInfo["AppPosition"].ToString();

                //------------------
                //string AttachmentFilePath = "";
                //SendEmail.SendeMail(EmpID, MailFrom.ToString(), MailFrom.ToString(), ToAdds, CCAdds, BCCAdds, Subject, MailBody, out ReplyMess, AttachmentFilePath);
                string error="";
                SendEmail.SendeMail(0, "", "", ToAdds, CCAdds, BCCAdds, Subject, MailBody, out error, "");
            }

        return ReplyMess;
    }
    static string UppercaseWords(string value)
    {
        char[] array = value.ToCharArray();
        // Handle the first letter in the string.
        if (array.Length >= 1)
        {
            if (char.IsLower(array[0]))
            {
                array[0] = char.ToUpper(array[0]);
            }
        }
        // Scan through the letters, checking for spaces.
        // ... Uppercase the lowercase letters following spaces.
        for (int i = 1; i < array.Length; i++)
        {
            if (array[i - 1] == ' ')
            {
                if (char.IsLower(array[i]))
                {
                    array[i] = char.ToUpper(array[i]);
                }
            }
        }
        return new string(array);
    }
    public void SendeMail(string MailFrom, string[] ToAddresses, string[] CCAddresses, string Subject, string BodyContent, string AttachMentPath, string MailDetails)
    {
        MailMessage objMessage = new MailMessage();
        SmtpClient objSmtpClient = new SmtpClient();
        StringBuilder msgFormat = new StringBuilder();

        try
        {
            //if (chkTest.Checked)
            //{
            //objMessage.From = new MailAddress("emanager@energiossolutions.com");
            //objMessage.To.Add("emanager@energiossolutions.com");
            //objMessage.To.Add("emanager@energiossolutions.com");
            //objMessage.To.Add("emanager@energiossolutions.com");
            //objMessage.To.Add("emanager@energiossolutions.com");

            //objSmtpClient.Host = "smtp.gmail.com";
            //objSmtpClient.Port = 25;
            //objSmtpClient.EnableSsl = true;
            //objSmtpClient.Credentials = new NetworkCredential("a@a.com", "esoft99");
            //}
            //else
            //{
            if (MailDetails == "Accident Notification Mail")
            {
                objMessage.Bcc.Add("emanager@energiossolutions.com");
            }
            objMessage.From = new MailAddress(SenderAddress);
            objSmtpClient.Credentials = new NetworkCredential(SenderUserName, SenderPass);
            objSmtpClient.Host = MailClient;
            objSmtpClient.Port = Port;

            foreach (string add in ToAddresses)
            {
                objMessage.To.Add(add);
            }
            if (CCAddresses != null)
            {
                foreach (string add in CCAddresses)
                {
                    objMessage.CC.Add(add);
                }
            }
            //}
            objMessage.Body = BodyContent;
            objMessage.Subject = Subject;
            objMessage.IsBodyHtml = true;
            objMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            if (File.Exists(AttachMentPath))
                objMessage.Attachments.Add(new System.Net.Mail.Attachment(AttachMentPath));
            objSmtpClient.Send(objMessage);
            
        }
        catch (System.Exception ex)
        {
            
            lblMsg_JR.Text = "Error while sending mail." + ex.Message;
            lblMsg_Comp.Text = "Error while sending mail." + ex.Message;
            lblMsg_Pot.Text = "Error while sending mail." + ex.Message;
        }
    }

    //------------------ Training & Development ---------------------

    protected void btnTrainingNeed_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "trSSaining", "ShowTrainingNeed('" + PeapID + "','" + Mode + "','" + AppraiserId + "');", true);
    }

    public void BindAllDoneTraining()
    {
        string strSQL = "SELECT Row_number() OVER(ORDER BY TP.TrainingId) AS SrNo,TP.TrainingId,TM.TrainingName, " +
                        "LastDoneDate=(SELECT TOP 1 REPLACE(Convert(VARCHAR(11), ENDDATE1,106),' ','-') AS ENDDATE1 FROM HR_TrainingPlanning TP1 WHERE TP1.TRAININGID=TP.TRAININGID AND TP1.STATUS='E' AND TP1.TRAININGPLANNINGID IN (SELECT TPD1.TRAININGPLANNINGID FROM dbo.HR_TrainingPlanningDetails TPD1 WHERE TPD1.EMPID=(SELECT EmpId FROM HR_EmployeePeapMaster WHERE PeapId = " + PeapID + " )) ORDER BY ENDDATE1 DESC) " +
                        "FROM dbo.HR_TrainingPlanning TP " +
                        "INNER JOIN HR_TrainingMaster TM ON TM.TrainingId = TP.TrainingId " +
                        "WHERE TP.STATUS='E' AND TP.TRAININGPLANNINGID IN (SELECT TPD.TRAININGPLANNINGID FROM dbo.HR_TrainingPlanningDetails TPD WHERE TPD.EMPID=(SELECT EmpId FROM HR_EmployeePeapMaster WHERE PeapId = " + PeapID + " )) " +
                        "ORDER BY ENDDATE1 DESC";


        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(strSQL);

        if (dt != null)
        {
            rptTrainingDone.DataSource = dt;
            rptTrainingDone.DataBind();
        }


    }

    public void BindRecommTraining()
    {
        string strSQL = "SELECT  Row_number() OVER(ORDER BY TR.TrainingId) AS SrNo,TR.TrainingRecommId,TR.TrainingId,TM.TrainingName, REPLACE(CONVERT(VARCHAR(12), TR.DueDate, 106), ' ','-') AS DueDate, " +
                        "LastDoneDate=(SELECT TOP 1 REPLACE(CONVERT(VARCHAR(11), ENDDATE1, 106),' ','-') AS ENDDATE1  FROM dbo.HR_TrainingPlanning TP WHERE TP.TRAININGID=TR.TRAININGID AND TP.STATUS='E' AND TP.TRAININGPLANNINGID IN (SELECT TPD.TRAININGPLANNINGID FROM dbo.HR_TrainingPlanningDetails TPD WHERE TPD.EMPID=TR.EMPID) ORDER BY ENDDATE1 DESC) " +
                        "FROM HR_TrainingRecommended TR " +
                        "LEFT JOIN HR_Peap_TrainingRecommended PR ON PR.TrainingRecommId = TR.TrainingRecommId " +
                        "INNER JOIN HR_TrainingMaster TM ON TM.TrainingId = TR.TrainingId " +
                        "WHERE  PR.PeapId = " + PeapID + " ";        

        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(strSQL);

        if (dt != null)
        {
            rptPlannedTrainings.DataSource = dt;
            rptPlannedTrainings.DataBind();
        }
    }

    protected void chkReassign_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkReassign = (CheckBox)sender;

        if (chkReassign.Checked)
        {
            TextBox txtDueDt = (TextBox)((CheckBox)sender).FindControl("txtDueDt");
            HiddenField hfTrainingId = (HiddenField)((CheckBox)sender).FindControl("hfTrainingId");

            if (txtDueDt.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this,this.GetType(), "error","alert('Please enter due date.');", true);
                txtDueDt.Focus();
                return;
            }
            DateTime dt;
            if (!DateTime.TryParse(txtDueDt.Text.Trim(), out dt))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error", "alert('Please enter valid date.');", true);                
                txtDueDt.Focus();
                return;
            }

            Common.Set_Procedures("DBO.HR_InsertPeapTrainingRecommanded");
            Common.Set_ParameterLength(4);
            Common.Set_Parameters(
                    new MyParameter("@PeapId", PeapID),
                    new MyParameter("@TrainingId", hfTrainingId.Value.Trim()),
                    new MyParameter("@RecommBy", AppraiserId),
                    new MyParameter("@DueDate", txtDueDt.Text.Trim())
                );
            Boolean Res;
            DataSet ds = new DataSet();
            Res = Common.Execute_Procedures_IUD(ds);
            if (Res)
            {
                BindRecommTraining();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "alert('Data saved successfully.');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Failure", "alert('Unable to save data.');", true);
            }
        }
    }

    


    //---------------------------------------------------------------

    #region ------------- KPI SECTION ----------------------

    protected void btnFillKPI_Click(object sender, EventArgs e)
    {
        JSID = Common.CastAsInt32(((Button)sender).CommandArgument);
        BindKPI();
        dvAssignKPI.Visible = true;
        //btnCancel_MFB.Text = "Cancel";
    }

    protected void btnCancelKPI_Click(object sender, EventArgs e)
    {
        lblKPIMsg.Text = "";
        JSID = 0;
        dvAssignKPI.Visible = false;
        txtAnswerJR.Text = "";
        BindJobResponsibility();
    }

    protected void BindKPI()
    {
        string Query = "SELECT PeapId,AppraiserByUser,JSID,KPIId,KPIName,KPIValue,Rating,Remark FROM HR_EmployeePeapJobResponsibility_KPI " +
                       "WHERE PeapId= " + PeapID + " AND AppraiserByUser = " + AppraiserId + " AND JSID= " + JSID + " ";

        DataTable dtKPI = Common.Execute_Procedures_Select_ByQueryCMS(Query);

        rptKPI.DataSource = dtKPI;
        rptKPI.DataBind();

        if (dtKPI.Rows.Count > 0)
            txtAnswerJR.Text = dtKPI.Rows[0]["Remark"].ToString();
    }

    protected void btnSaveKPI_Click(object sender, EventArgs e)
    {
        int count = 0;

        foreach (RepeaterItem rpt in rptKPI.Items)
        {
            //TextBox txtRating = (TextBox)rpt.FindControl("txtRating_KPI");
            DropDownList ddlRating = (DropDownList)rpt.FindControl("ddlRating");

            if (ddlRating.SelectedIndex != 0)
            {
                count = count + 1;
                break;
            }
        }

        if (count == 0)
        {
            lblKPIMsg.Text = "Please fill rating.";
            return;
        }


        try
        {
            foreach (RepeaterItem rpt in rptKPI.Items)
            {
                HiddenField hfKPIID = (HiddenField)rpt.FindControl("hfKPIID");
                //TextBox txtRating = (TextBox)rpt.FindControl("txtRating_KPI");
                DropDownList ddlRating = (DropDownList)rpt.FindControl("ddlRating");

                if (ddlRating.SelectedIndex != 0)
                {
                    
                    string SQL = "UPDATE HR_EmployeePeapJobResponsibility_KPI SET Rating = " + ddlRating.SelectedValue.Trim() + ",Remark = '" + txtAnswerJR.Text.Trim().Replace("'", "`") + "' WHERE PeapId= " + PeapID + " AND AppraiserByUser = " + AppraiserId + " AND JSID= " + JSID + " AND KPIId = " + hfKPIID.Value.Trim() + " ; SELECT -1 ";
                    DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(SQL);
                    if (dt.Rows.Count > 0)
                    {
                        lblKPIMsg.Text = "Record saved successfully.";
                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblKPIMsg.Text = "Unable to save. Error: " + ex.Message.ToString();
        }

    }

    protected void txtRating_KPI_TextChanged(object sender, EventArgs e)
    {
        //double TotalScore = 0;
        //double PerformanceScore = 0;

        //foreach (RepeaterItem rpt in rptPerformanceOnTheJob.Items)
        //{
        //    TextBox txtRating = (TextBox)rpt.FindControl("txtRating");

        //    if (txtRating.Text != "")
        //    {
        //        TotalScore = TotalScore + Convert.ToDouble(txtRating.Text.Trim());
        //    }
        //    else
        //    {
        //        TotalScore = TotalScore + 0;
        //    }
        //}

        //PerformanceScore = Convert.ToDouble(Math.Truncate((TotalScore / rptPerformanceOnTheJob.Items.Count) * 10000) / 10000);
        

        //string[] Colors = { "#FF0000", "#FF8583", "#FFFF00", "#3DEB3D", "#00AE00" };

        //lblTotScore_JS.Text = TotalScore.ToString() == "0" ? "" : TotalScore.ToString();        
        //lblPerformanceScore_JS.Text = string.Format("{0:0.00}", PerformanceScore);

        //int index = Common.CastAsInt32(Math.Ceiling(Common.CastAsDecimal(lblPerformanceScore_JS.Text))) - 1;
        //if (index <= 0) { index = 0; }

        //JRColor.Style.Add("background-Color", Colors[index]);
        //if (index == 2 || index == 3)
        //    JRColor.Style.Add("color", "Black");
        //else
        //    JRColor.Style.Add("color", "White");
        



    }

    //protected void imgEditKPI_Click(object sender, ImageClickEventArgs e)
    //{
    //    ImageButton btn = (ImageButton)sender;
    //    KPIId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);

    //    Label lblKPIName = (Label)btn.Parent.FindControl("lblKPIName");
    //    Label lblKPIValue = (Label)btn.Parent.FindControl("lblKPIValue");

    //    txtKPIName.Text = lblKPIName.Text;
    //    txtKPIValue.Text = lblKPIValue.Text;

    //    BindKPI();


    //}

    //protected void imgDeleteKPI_Click(object sender, ImageClickEventArgs e)
    //{
    //    KPIId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);

    //    string deleteQuery = "DELETE FROM HR_JobResponsibility_KPI WHERE KPIId=" + KPIId + " AND JSID= " + JRID + "; SELECT -1";

    //    DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(deleteQuery);

    //    if (dt.Rows.Count > 0)
    //    {
    //        lblKPIMsg.Text = "Data Deleted successfully.";
    //        BindKPI();
    //    }
    //    else
    //    {
    //        lblKPIMsg.Text = "Unable to delete data.";
    //    }

    //}

    #endregion
}