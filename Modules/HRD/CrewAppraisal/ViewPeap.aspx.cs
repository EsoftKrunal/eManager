using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.IO;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using ShipSoft.CrewManager.BusinessLogicLayer;
using ShipSoft.CrewManager.BusinessObjects;
using ShipSoft.CrewManager.Operational;

public partial class CrewAppraisal_ViewPeap : System.Web.UI.Page
{
    public AuthenticationManager Auth;
    public int PeapID
    {
        get { return Convert.ToInt32("0" + Convert.ToString(ViewState["vPeapID"])); }
        set { ViewState["vPeapID"]= value.ToString(); }
    }
    public int PeapLeveID
    {
        get { return Convert.ToInt32("0" + Convert.ToString(ViewState["vPeapLeveID"])); }
        set { ViewState["vPeapLeveID"] = value.ToString(); }
    }
    public int CrewID
    {
        get { return Convert.ToInt32("0" + Convert.ToString(ViewState["vCrewID"])); }
        set { ViewState["vCrewID"] = value.ToString(); }
    }
    public int OccasionID
    {
        get { return Convert.ToInt32("0" + Convert.ToString(ViewState["vOccasion"])); }
        set { ViewState["vOccasion"] = value.ToString(); }
    }
    //public int IsVerified
    //{
    //    get { return Convert.ToInt32("0" + ViewState["vIsVerified"].ToString()); }
    //    set { ViewState["vIsVerified"] = value.ToString(); }
    //}
    public int VesselID
    {
        get { return Convert.ToInt32("0" + ViewState["vVesselID"].ToString()); }
        set { ViewState["vVesselID"] = value.ToString(); }
    }
    // -------------------------
    public string CrewNumber
    {
        get { return ViewState["vCrewNumber"].ToString(); }
        set { ViewState["vCrewNumber"] = value.ToString(); }
    }
    public string VesselCode
    {
        get { return  ViewState["vVesselCode"].ToString(); }
        set { ViewState["vVesselCode"] = value.ToString(); }
    }
    public string Location
    {
        get { return ViewState["Location"].ToString(); }
        set { ViewState["Location"] = value.ToString(); }
    }

    // -------------------------

    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        Auth = new AuthenticationManager(267, Convert.ToInt32(Session["loginid"].ToString()), ObjectType.Page);
        lblmsg.Text = "";
        if (!Page.IsPostBack)
        {
            BindSireChap();
            if (Page.Request.QueryString["PeapID"] != null)
            {
                PeapID = Common.CastAsInt32(Page.Request.QueryString["PeapID"]);
                VesselCode = Convert.ToString(Page.Request.QueryString["VesselCode"]);
                Location = Convert.ToString(Page.Request.QueryString["Location"]);

                ShowData();
            }
        }
    }

    //Events *******************************************************************************
    protected void ddlTraining_OnSelectedIndexChanged(object sender,EventArgs e)
    {
        tblTraining.Visible= (ddlTrainingRequired.SelectedValue=="Yes");
    }
    public bool IsTrainingAssignedAlready(string TrainingID)
    {
        string sql = "select dbo.sp_IsTrainingExists(" + CrewID +"," + TrainingID + ")";
        DataSet Ds = Budget.getTable(sql);
        if (Ds.Tables[0].Rows.Count > 0)
        {
            int tid = Common.CastAsInt32(Ds.Tables[0].Rows[0][0]);
            if (tid > 0)
                return true;
        }
        return false;
    }
    protected void btn_Save_PlanTraining_Click(object sender, EventArgs e)
    {
        if (ddlTrainingName.SelectedIndex<=0)
        {
            lblmsg.Text = "Select training type.";
            return;
        }
        if (txt_DueDate.Text.Trim() == "")
        {
            lblmsg.Text = "Enter due date.";
            return;
        }

        if (IsTrainingAssignedAlready(ddlTrainingName.SelectedValue))
        {
            lblmsg.Text = "This training is already assigned to the user.";
            return;
        }

        Budget.getTable("exec dbo.InsertUpdateCrewTrainingRequirementFromAppraisal -1," + CrewID + "," + ddlTrainingName.SelectedValue + ",''," + Session["loginid"].ToString() + "," + Session["loginid"].ToString() + ",'" + txt_DueDate.Text + "','O',0,'N'");
        BindTraining();
    }
    protected void imgDel_OnClick(object sender, EventArgs e)
    {
        //ImageButton imgDel = (ImageButton)sender;
        //HiddenField hfTrainingID = (HiddenField)imgDel.Parent.FindControl("hfTrainingID");

        //DataSet Ds = (DataSet)Session["sTraining"];
        //DataRow[] Dr = Ds.Tables[0].Select("TrainingID=" + hfTrainingID .Value+ "");
        //Ds.Tables[0].Rows.Remove(Dr[0]);
        
        //rptTraining.DataSource = Ds;
        //rptTraining.DataBind();

        //Session["sTraining"] = Ds;
    }

    protected void ddlSireChap1_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlTrainingName.DataSource = Common.Execute_Procedures_Select_ByQueryCMS("select trainingid,trainingtypename + ' - ' +trainingname as trainingname from training inner join trainingtype on training.TypeOfTraining=trainingtype.TrainingTypeId where sirechap=" + ddlSireChap1.SelectedValue + " order by trainingtypename,trainingname");
        ddlTrainingName.DataTextField = "trainingname";
        ddlTrainingName.DataValueField = "trainingid";
        ddlTrainingName.DataBind();
        ddlTrainingName.Items.Insert(0, new ListItem("< Select >", ""));
        return;
        //string st = "<select class='required_box' style='width:300px' id='ddlTrainings'>";
        //st += "<option value=''> Select Training </option >";
        //DataTable drCat = Common.Execute_Procedures_Select_ByQueryCMS("select TrainingTypeId,TrainingTypeName from trainingtype order by TrainingTypeName");
        //foreach (DataRow dr in drCat.Rows)
        //{
        //    DataTable dt133 = Common.Execute_Procedures_Select_ByQueryCMS("select trainingid,trainingname from training where sirechap=" + ddlSireChap1.SelectedValue + " and TypeOfTraining=" + dr["TrainingTypeId"].ToString() + " order by trainingname");
        //    if (dt133.Rows.Count > 0)
        //    {
        //        st += "<optgroup label='-- " + dr["TrainingTypeName"].ToString() + " --'></optgroup>";
        //        foreach (DataRow dr1 in dt133.Rows)
        //        {
        //            st += "<option value='" + dr1["trainingid"].ToString() + "'>" + dr1["trainingname"].ToString() + "</option >";
        //        }
        //    }
        //}
        //st += "</select>";
        //litSelectTr.Text = st;
    }
    protected void rptTraining_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        ImageButton imgDel = (ImageButton)e.Item.FindControl("imgDel");
    }
    //Function *****************************************************************************
    public void ShowData()
    {
        string sql = "select Ass.*,App.AppraisalOccasionName as OccasionCus ,(Ass.AssName+' '+Ass.AssLname)AssFullName   " +
        " ,(case when Ass.PeapType=1 then 'MANAGEMENT' when Ass.PeapType=2 then 'SUPPORT' when Ass.PeapType=3 then 'OPERATION' end )PeapTypeCus   " +
        " ,(select VesselName from Vessel V where V.VesselCode=Ass.VesselCode)VesselFullName" +        
        " ,replace (convert(varchar, Ass.AppraisalFromDate,106),' ','-')AppraisalFromDateCus    " +
        " ,replace (convert(varchar, Ass.AppraisalToDate ,106),' ','-')AppraisalToDateCus    " +
        " ,replace (convert(varchar, Ass.DatejoinedComp,106),' ','-')DatejoinedCompCus    " +
        " ,replace (convert(varchar, Ass.DatejoinedVessel ,106),' ','-')DatejoinedVesselCus  " +
        " ,replace (convert(varchar, Ass.RemAppraiserDate ,106),' ','-')RemAppraiserDateCus  " +
        " ,replace (convert(varchar, Ass.RemAppraiseeDate ,106),' ','-')RemAppraiseeDateCus  " +
        " ,replace (convert(varchar, Ass.RemMasterDate ,106),' ','-')RemMasterDateCus  " +
        " ,replace (convert(varchar, Ass.ReviewedDate ,106),' ','-')ReviewedDateCus" +
        " from tbl_Assessment Ass left join AppraisalOccasion App on Ass.Occasion=App.AppraisalOccasionID " +
        " where AssMgntID=" + PeapID.ToString() + " And VesselCode='" + VesselCode + "' And Location='" + Location + "' ";
   

        DataSet Ds = Budget.getTable(sql);
        if (Ds != null)
        {
            if (Ds.Tables[0].Rows.Count > 0)
            {
                DataRow Dr = Ds.Tables[0].Rows[0];
                
                CrewNumber = Dr["CrewNo"].ToString();
                VesselID = GetVesselID(Dr["VesselCode"].ToString());
                OccasionID = Common.CastAsInt32(Dr["Occasion"].ToString());
                hfOccasion.Value = Dr["Occasion"].ToString();
                lblPeapID.Text = PeapID.ToString();
                lblVessel.Text = Dr["VesselCode"].ToString()+"( "+ Dr["VesselFullName"].ToString()+")";
                lblPeapLevel.Text = Dr["PeapTypeCus"].ToString();
                hfPeaplevel.Value = Dr["PeapTypeCus"].ToString();
                hfPeapID.Value = Dr["AssMgntID"].ToString();
                lblOccation.Text = Dr["OccasionCus"].ToString();
                OccasionID = Common.CastAsInt32( Dr["OccasionCus"]);
                lblFirstName.Text = Dr["AssName"].ToString();
                lblLastName.Text = Dr["AssLname"].ToString();
                lblCrewNo.Text = Dr["CrewNo"].ToString();
                CrewID = GetCrewID();

                lblRank.Text = Dr["Rank"].ToString();                
                lblPerformanceScore.Text = Dr["PerformanceScore"].ToString();
                lblCompetencyScore.Text = Dr["CompetenciesScore"].ToString();

                lblDateJoinedCompany.Text = Dr["DatejoinedCompCus"].ToString();
                lblDtJoinedVessel.Text = Dr["DatejoinedVesselCus"].ToString();

                lblAppFrom.Text= Dr["AppraisalFromDateCus"].ToString();
                lblAppTO.Text = Dr["AppraisalToDateCus"].ToString();
                
                //-------------------------------------------------
                txtPerScale1.Text = Dr["PerScale1"].ToString();
                txtPerScale2.Text = Dr["PerScale2"].ToString();
                txtPerScale3.Text = Dr["PerScale3"].ToString();
                lblTotPer.Text = Convert.ToString(Common.CastAsInt32(Dr["PerScale1"]) + Common.CastAsInt32(Dr["PerScale2"]) + Common.CastAsInt32(Dr["PerScale3"]));
                lblPerPerformanceScore.Text = Dr["PerformanceScore"].ToString();
                txtPerAss1.Text = Dr["perAss1"].ToString();
                txtPerAss2.Text = Dr["perAss2"].ToString();
                txtPerAss3.Text = Dr["perAss3"].ToString();
                txtAssScale1.Text = Dr["AssScale1"].ToString();
                txtAssScale2.Text = Dr["AssScale2"].ToString();
                txtAssScale3.Text = Dr["AssScale3"].ToString();
                txtAssScale4.Text = Dr["AssScale4"].ToString();
                txtAssScale5.Text = Dr["AssScale5"].ToString();
                txtAssScale6.Text = Dr["AssScale6"].ToString();
                txtAssScale7.Text = Dr["AssScale7"].ToString();
                txtAssScale8.Text = Dr["AssScale8"].ToString();
                lblTotAss.Text = Convert.ToString(Common.CastAsInt32(Dr["AssScale1"]) + Common.CastAsInt32(Dr["AssScale2"]) + Common.CastAsInt32(Dr["AssScale3"]) + Common.CastAsInt32(Dr["AssScale4"]) + Common.CastAsInt32(Dr["AssScale5"]) + Common.CastAsInt32(Dr["AssScale6"]) + Common.CastAsInt32(Dr["AssScale7"]) + Common.CastAsInt32(Dr["AssScale8"]));
                lblAssCompetencyScore.Text = Dr["CompetenciesScore"].ToString();
                ShowAVGColor();
                txtPotSecA1.Text = Dr["PotSecA"].ToString();
                string PromoRecomm = Dr["PotSecB"].ToString();
                ddlPotReadyForPromotion.SelectedIndex = (PromoRecomm == "Y") ? 1 : ((PromoRecomm == "N") ? 2 : 3);
                txtPotSecB1.Text = Dr["PotSecB1"].ToString();
                txtPotSecC1.Text = Dr["PotSecC1"].ToString();
                txtPotSecC2.Text = Dr["PotSecC2"].ToString();


                if (Dr["ReviewedBy"].ToString() != "")
                {
                     if (Dr["IsTrainingRequired"].ToString() == "1")
                    {
                        ddlTrainingRequired.SelectedIndex = 2;
                        ShowTrainingDate();
                    }
                    else
                    {
                        ddlTrainingRequired.SelectedIndex = 1;
                    }
                }
                ddlTraining_OnSelectedIndexChanged(new object(), new EventArgs());
                txtReviewedby.Text = Dr["ReviewedBy"].ToString();
                txtReviewedDate.Text = Dr["ReviewedDateCus"].ToString();
                txtOfficeComment.Text = Dr["ReviewedComment"].ToString();


                txtManAppAppraiseeRem.Text = Dr["RemAppraiseeComment"].ToString();
                txtManAppAppraiseeName.Text = Dr["RemAppraiseeName"].ToString();
                txtManAppAppraiseeCrewNo.Text = Dr["RemAppraiseeCrewNo"].ToString();
                txtManAppAppraiseeRank.Text = Dr["RemAppraiseeRank"].ToString();
                txtManAppAppraiseeDate.Text = Common.ToDateString(Dr["RemAppraiseeDate"]);


                txtManAppAppraiserRem.Text = Dr["RemAppraiserComment"].ToString();
                txtManAppAppraiserName.Text = Dr["RemAppraiserName"].ToString();
                txtManAppAppraiserCrewNo.Text = Dr["RemAppraiserCrewNo"].ToString();
                txtManAppAppraiserRank.Text = Dr["RemAppraiserRank"].ToString();
                txtManAppAppraiserDate.Text = Common.ToDateString(Dr["RemAppraiserDate"]);

                txtManAppMasterRem.Text = Dr["RemMasterComments"].ToString();
                txtManAppMasterName.Text = Dr["RemMasterName"].ToString();
                txtManAppMasterCrewNo.Text = Dr["AppMasterCrewNo"].ToString();
                txtManAppMasterDate.Text = Common.ToDateString(Dr["RemMasterDate"]);

		int exportappid = Common.CastAsInt32(Dr["CrewAppraisalId"]);

                //----------------------------------------
                btnSave.Visible = false;
                btnClosure.Visible = false;

                if (Dr["status"].ToString().Trim() == "1")
                {
                    btnSave.Visible = Auth.IsUpdate;
                    btn_Save_PlanTraining.Visible = true;
                }
                else
                {
                    btn_Save_PlanTraining.Visible = false;
                }

		if(exportappid<=0 && Dr["status"].ToString().Trim() == "2" && Auth.IsUpdate)
	  		btnClosure.Visible = true;

		//if(Session["loginid"].ToString()=="1")
		//	btnClosure.Visible = true;
            }
        }
    }
    public void ShowTrainingDate()
    {
        string sql = "select "+
        " (Select TrainingTypeName from TrainingType TT where TrainingTypeID=(SELECT TypeofTraining From Training T Where T.TrainingID=C.TrainingID))TrainingTypetxt " +
        " ,(SELECT TrainingName From Training T Where T.TrainingID=C.TrainingID)TrainingIDtxt  " +
        " ,(replace( convert(varchar,N_DueDate ,106),' ','-'))Todatetxt" +
        " ,(SELECT TypeofTraining From Training T Where T.TrainingID=C.TrainingID)TypeOfTraining  "+
        " ,TrainingID  "+
        " from  " +
        " CrewTrainingRequirement C where C.CrewID=" + CrewID + " and ISNULL(C.SOURCE,0)=0";
        DataSet Ds = Budget.getTable(sql);
        rptTraining.DataSource = Ds.Tables[0];
        rptTraining.DataBind();
    }    
    public bool IsCrewAvailable()
    {
        string sql = " select * from CrewPersonalDetails where CrewNumber='" +lblCrewNo.Text + "' and FirstName='" + lblFirstName.Text+ "' and lastName='" + lblLastName.Text+ "'";
        DataSet Ds = Budget.getTable(sql);
        if (Ds == null)
            return false;
        else if(Ds.Tables[0].Rows.Count<=0)
            return false;
        else
            return true;
    }
    public string GetVesselCode(int VesselID)
    {
        //VesselCode
        string sql = "select VesselCode from Vessel where VesselID='" + VesselID + "'";
        DataSet Ds = Budget.getTable(sql);
        if (Ds != null)
        {
            if (Ds.Tables[0].Rows.Count > 0)
            {
                return Ds.Tables[0].Rows[0][0].ToString(); ;
            }
        }
        return "";
    }
    public int GetVesselID(string VesselCode)
    {
        //VesselCode
        string sql = "select VesselID from Vessel where VesselCode='" + VesselCode + "'";
        DataSet Ds = Budget.getTable(sql);
        if (Ds != null)
        {
            if (Ds.Tables[0].Rows.Count > 0)
            {
                return Common.CastAsInt32( Ds.Tables[0].Rows[0][0].ToString());
            }
        }
        return 0;
    }
    
    public int GetCrewID()
    {
        string sql = "select CrewID from CrewPersonalDetails where CrewNumber='" + lblCrewNo.Text + "'";
        DataSet Ds = Budget.getTable(sql);
        if (Ds != null)
        {
            if (Ds.Tables[0].Rows.Count > 0)
            {
                return Common.CastAsInt32(Ds.Tables[0].Rows[0][0]);
            }
        }
        return 0;
    }
    public void BindTraining()
    {
        
        string sql = "select " +
        " (Select TrainingTypeName from TrainingType TT where TrainingTypeID=(SELECT TypeofTraining From Training T Where T.TrainingID=C.TrainingID))TrainingTypetxt " +
        " ,(SELECT TrainingName From Training T Where T.TrainingID=C.TrainingID)TrainingIDtxt  " +
        " ,(replace( convert(varchar,N_DueDate ,106),' ','-'))Todatetxt" +
        " ,(SELECT TypeofTraining From Training T Where T.TrainingID=C.TrainingID)TypeOfTraining  " +
        " ,TrainingID  " +
        " from  " +
        " CrewTrainingRequirement C where C.CrewID=" + CrewID + " and ISNULL(C.SOURCE,0)=0";

        
        DataSet Ds = Budget.getTable(sql);
        if (Ds != null)
        {
            rptTraining.DataSource = Ds;
            rptTraining.DataBind();
            Session.Add("sTraining",Ds);
        }
    }    
    public void ShowAVGColor()
    {
         if (Common.CastAsDecimal(lblPerformanceScore.Text) < (decimal)2.50)
        {
            tdPerformanceScore.Attributes.Add("class", "Red");
        }
        else if (Common.CastAsDecimal(lblPerformanceScore.Text) >= (decimal)2.50 && Common.CastAsDecimal(lblPerformanceScore.Text) <= (decimal)3.50)
        {
            //lblPerformanceScore.CssClass = "Silver";
            tdPerformanceScore.Attributes.Add("class", "Silver");
        }
        else if (Common.CastAsDecimal(lblPerformanceScore.Text) > (decimal)3.50)
        {
            //lblPerformanceScore.CssClass = "Gold";
            tdPerformanceScore.Attributes.Add("class", "Gold");
        }

        //Competency
        if (Common.CastAsDecimal(lblCompetencyScore.Text) < (decimal)2.0) 
        {
            //lblCompetencyScore.CssClass = "red";
            tdCompetencyScore.Attributes.Add("class", "Red");             
        }
        else if (Common.CastAsDecimal(lblCompetencyScore.Text) >= (decimal)2.0 && Common.CastAsDecimal(lblCompetencyScore.Text) <= (decimal)2.80)
        {
            //lblCompetencyScore.CssClass = "Silver";
            tdCompetencyScore.Attributes.Add("class", "Silver");
        }
        else if (Common.CastAsDecimal(lblCompetencyScore.Text) > (decimal)2.80)
        {
            //lblCompetencyScore.CssClass = "Gold";
            tdCompetencyScore.Attributes.Add("class", "Gold");
        }
        
    }
    public void BindSireChap()
    {
        string sql = "select ChapterNo,Convert(varchar,ChapterNo)+' '+ ChapterName as ChapterName from dbo.m_Chapters where InspectionGroup=1 order by ChapterNo ";
        DataTable Dt = Common.Execute_Procedures_Select_ByQuery(sql);

        ddlSireChap1.DataSource = Dt;
        ddlSireChap1.DataTextField = "ChapterName";
        ddlSireChap1.DataValueField = "ChapterNo";
        ddlSireChap1.DataBind();
        ddlSireChap1.Items.Add(new ListItem("OTHERS","-1"));

        ddlSireChap1_SelectedIndexChanged(new object(), new EventArgs());
    }
    //------------------------------------------------------------------------------------------------------------------------
    
    protected void btnSave_OnClick(object sender, EventArgs e)
    {
        if (txtOfficeComment.Text.Trim() == "")
        {
            lblmsg.Text = "Please fill office comments.";
            return;
        }
        int IsTrainingRequired = 0;
        if (ddlTrainingRequired.SelectedIndex != 0)
            IsTrainingRequired = 1;
        Budget.getTable("update tbl_assessment set status=2,ReviewedBy='" + Session["UserName"].ToString() + "',ReviewedDate=getdate(),ReviewedComment='" + txtOfficeComment.Text.Trim().Replace("'","`") + "',IsTrainingRequired=" + IsTrainingRequired.ToString() + " where AssMgntID=" + PeapID.ToString() + " And VesselCode='" + VesselCode + "' And Location='" + Location + "'");
        ShowData();
    }
    protected void btnClosure_OnClick(object sender, EventArgs e)
    {
            CrewApprasialDetails crewappraisal = new CrewApprasialDetails();
            try
            {
                crewappraisal.CrewAppraisalId = -1;
                crewappraisal.CreatedBy = Convert.ToInt32(Session["loginid"].ToString());

                // set the path and file
                string uploadFileName = System.IO.Path.GetRandomFileName() + ".pdf";
                string path = HttpContext.Current.Server.MapPath("~/EMANAGERBLOB/HRD/Documents/Appraisal/");
                string fullPath = Path.Combine(path, uploadFileName);

                string PromoRecomm = "";
                if (ddlPotReadyForPromotion.SelectedIndex == 1)
                    PromoRecomm = "Y";
                else if (ddlPotReadyForPromotion.SelectedIndex == 1)
                    PromoRecomm = "N";
                else
                    PromoRecomm = "L";

                crewappraisal.CrewId = Convert.ToInt16(CrewID);
                crewappraisal.AppraisalOccasionId = Common.CastAsInt32(hfOccasion.Value);
                try
                {
                    crewappraisal.AverageMarks = Convert.ToDouble(00.00);//$$$$
                }
                catch { crewappraisal.AverageMarks = 0; }

                crewappraisal.ApprasialFrom = lblAppFrom.Text;
                crewappraisal.ApprasialTo = lblAppTO.Text;
                crewappraisal.AppraiserRemarks = txtOfficeComment.Text.Trim();
                crewappraisal.AppraiseeRemarks = "";
                crewappraisal.OfficeRemarks = "";  //divAppMasterRem.InnerHtml;
                crewappraisal.VesselId = VesselID;
                crewappraisal.Recommended = PromoRecomm;
                crewappraisal.N_Recommended = PromoRecomm;
                crewappraisal.N_PerfScore = lblPerformanceScore.Text;
                crewappraisal.N_CompScore = lblCompetencyScore.Text;
                crewappraisal.N_ReportNo = ""; //$$$$
                crewappraisal.ImagePath = uploadFileName;
                crewappraisal.N_TrainingRequired = (ddlTrainingRequired.SelectedIndex == 1) ? "N" : "Y";
                ProcessCrewMemberApprasialDetailsInsertData addapprasialdetails = new ProcessCrewMemberApprasialDetailsInsertData();

                addapprasialdetails.ApprasialDetails = crewappraisal;
                addapprasialdetails.Invoke();

                // Update  genrated CrewAppraislID to Assessment table 
                //
                Budget.getTable("update tbl_assessment set CrewAppraisalId=" + crewappraisal.CrewAppraisalId + " where AssMgntID=" + PeapID.ToString() + " And VesselCode='" + VesselCode + "' And Location='" + Location + "'");
                //-----------------------------------------------------------------------------------------------------------------------------
                string filename = "";
                if (true)
                {

                    string JR1 = "", JR2 = "", JR3 = "", JRScale1 = "", JRScale2 = "", JRScale3 = "", Competency1 = "", Competency2 = "", Competency3 = "", Competency4 = "", Competency5 = "";
                    string Competency6 = "", Competency7 = "", Competency8 = "", CopmScale1 = "", CopmScale2 = "", CopmScale3 = "", CopmScale4 = "", CopmScale5 = "", CopmScale6 = "", CopmScale7 = "", CopmScale8 = "";
                    string AppraiseeName = "", AppraiseeRank = "", AppraiseeVessel = "", AppraiseeOccasion = "", AppraiseeAppraisalFrom = "", AppraiseeAppraisalTo = "", AppraiseeDTJoinedVessel = "", AppraiseePerformanceScore = "", AppraiseeCompetencyScore = "", PerformanceSecTotal = "", CompetencySecTotal = "";
                    string PotSecA = "", PotSecB = "", PotSecC1 = "", PotSecC2 = "", AppraiserRemark = "", AppraiserName = "", AppraiserRank = "", AppraiserDate = "", MasterRemark = "", MasterName = "", MasterCrewNo = "", MasterDate = "";
                    string AppraiseeRemarks = "", AppraiserCrewNo = "", JRAss1 = "", JRAss2 = "", JRAss3 = "", ReviewedBy = "", ReviewedDate = "", ReviewedComment = "", AppraiseeCrewNo = "";
                    string CpmpetencyScore = "", PerformanceFormula = "", AppraisalRecievedDate = "";
                    DataSet ds;

                    string sql = "SELECT AssMgntID,VesselCode,PeapType,CrewNo,rank,DatejoinedComp " +
                        " ,(AssName+' '+AssLname)AssName" +
                        " ,(select VesselName  FROM Vessel V where V.VesselCode=A.VesselCode)VesselName " +
                        " ,replace(convert(varchar,AppraisalFromDate,106),' ','-')AppraisalFromDate " +
                        " ,replace(convert(varchar,AppraisalToDate,106),' ','-')AppraisalToDate " +
                        " ,replace(convert(varchar,DatejoinedVessel,106),' ','-')  DatejoinedVessel " +

                        " ,replace(convert(varchar,RemAppraiserDate,106),' ','-')  RemAppraiserDate" +
                        " ,replace(convert(varchar,RemMasterDate,106),' ','-')  RemMasterDate" +
                        " ,(select RankName from rank R where R.rankID=A.ShipSoftRank )ShipSoftRank " +
                        " ,(select AppraisalOccasionName  from AppraisalOccasion o where a.Occasion=o.AppraisalOccasionID ) Occasion " +
                        " ,PerformanceScore,CompetenciesScore,PerScale1,PerScale2,PerScale3 " +
              " ,(PerScale1+PerScale2+PerScale3)TotalPerFormanceScore " +
              " ,perAss1,perAss2,perAss3,AssScale1,AssScale2,AssScale3,AssScale4,AssScale5,AssScale6,AssScale7,AssScale8 " +
              " ,(AssScale1+AssScale2+AssScale3+AssScale4+AssScale5+AssScale6+AssScale7+AssScale8)TotalcompetencyScore " +
              " ,PotSecA,PotSecB,PotSecB1,PotSecB2,PotSecC1,PotSecC2,RemAppraiseeComment,RemAppraiseeName,RemAppraiseeRank,RemAppraiseeCrewNo,RemAppraiseeDate " +
              " ,RemAppraiserComment,RemAppraiserName,RemAppraiserCrewNo,RemAppraiserRank,RemMasterComments,RemMasterName " +
              " ,AppMasterCrewNo,DateJoinedVesselAppraisee,ReviewedBy " +
              " ,replace(convert(varchar,ReviewedDate ,106),' ','-')ReviewedDate " +
              " ,ReviewedComment,IsTrainingRequired,Status,AppraisalRecievedDate " +
              " FROM DBO.tbl_Assessment A where AssMgntID=" + PeapID + " And A.VesselCode='" + VesselCode + "' And A.Location='" + Location + "'";
                    ds = Budget.getTable(sql);
                    string PeapType = "";
                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow Dr = ds.Tables[0].Rows[0];

                            if (Dr["PerScale1"].ToString() == "1")
                                PeapType = "MANAGEMENT TEAM";
                            else if (Dr["PerScale1"].ToString() == "2")
                                PeapType = "SUPPORT TEAM";
                            else if (Dr["PerScale1"].ToString() == "3")
                                PeapType = "OPERATION TEAM";


                            JRScale1 = Dr["PerScale1"].ToString();
                            JRScale2 = Dr["PerScale2"].ToString();
                            JRScale3 = Dr["PerScale3"].ToString();

                            JRAss1 = Dr["perAss1"].ToString();
                            JRAss2 = Dr["perAss2"].ToString();
                            JRAss3 = Dr["perAss3"].ToString();



                            CopmScale1 = Dr["AssScale1"].ToString();
                            CopmScale2 = Dr["AssScale2"].ToString();
                            CopmScale3 = Dr["AssScale3"].ToString();
                            CopmScale4 = Dr["AssScale4"].ToString();
                            CopmScale5 = Dr["AssScale5"].ToString();
                            CopmScale6 = Dr["AssScale6"].ToString();
                            CopmScale7 = Dr["AssScale7"].ToString();
                            CopmScale8 = Dr["AssScale8"].ToString();

                            AppraiseeName = Dr["AssName"].ToString();
                            AppraiseeRank = Dr["ShipSoftRank"].ToString();
                            AppraiseeVessel = Dr["VesselName"].ToString();
                            AppraiseeOccasion = Dr["Occasion"].ToString();
                            AppraiseeAppraisalFrom = Dr["AppraisalFromDate"].ToString();
                            AppraiseeAppraisalTo = Dr["AppraisalToDate"].ToString();
                            AppraiseeDTJoinedVessel = Dr["DatejoinedVessel"].ToString();
                            AppraiseePerformanceScore = Dr["PerformanceScore"].ToString();
                            AppraiseeCompetencyScore = Dr["CompetenciesScore"].ToString();

                            PerformanceSecTotal = Dr["TotalPerFormanceScore"].ToString();
                            CompetencySecTotal = Dr["TotalcompetencyScore"].ToString();

                            PotSecA = Dr["PotSecA"].ToString();
                            PotSecB = Dr["PotSecB"].ToString();
                            PotSecC1 = Dr["PotSecC1"].ToString();
                            PotSecC2 = Dr["PotSecC1"].ToString();

                            AppraiseeRemarks = Dr["RemAppraiseeComment"].ToString();

                            AppraiserRemark = Dr["RemAppraiserComment"].ToString();
                            AppraiserName = Dr["RemAppraiserName"].ToString();
                            AppraiserRank = Dr["RemAppraiserRank"].ToString();
                            AppraiserCrewNo = Dr["RemAppraiserCrewNo"].ToString();
                            AppraiserDate = Dr["RemAppraiserDate"].ToString();

                            MasterRemark = Dr["RemMasterComments"].ToString();
                            MasterName = Dr["RemMasterName"].ToString();
                            MasterCrewNo = Dr["AppMasterCrewNo"].ToString();
                            MasterDate = Dr["RemMasterDate"].ToString();

                            ReviewedBy = Dr["ReviewedBy"].ToString();
                            ReviewedDate = Dr["ReviewedDate"].ToString();
                            ReviewedComment = Dr["ReviewedComment"].ToString();
                            AppraisalRecievedDate = Common.ToDateString(Dr["AppraisalRecievedDate"].ToString());

                            AppraiseeCrewNo = Dr["CrewNo"].ToString();


                            if (Convert.ToInt32(Dr["PeapType"]) == 1)
                            {
                                JR1 = "Compliance with & enforcement of vessel’s KPIs including but not limited to Zero Spill, Zero Incidents and Zero off-hire. ";
                                JR2 = "Compliance with & enforcement of Policies & Procedures from Owners, Charterers & Company’s Safety Management System. ";
                                JR3 = "Compliance with & enforcement of all Statutory, Flag State, Port State, National and International Regulations. ";

                                Competency1 = "Team Management";
                                Competency2 = "Decision Making";
                                Competency3 = "Process Orientation";
                                Competency4 = "Leadership";
                                Competency5 = "Strategy Execution";
                                Competency6 = "Technical Know-How";
                                Competency7 = "Managing Pressure and Stress";
                                Competency8 = "Initiative";

                            }
                            else if (Convert.ToInt32(Dr["PeapType"]) == 2)
                            {
                                JR1 = "Compliance with Company’s Policies and Procedures including but not limited to Safety and Protection of environment. ";
                                JR2 = "Understanding of & Compliance with Company’s Safety Management System. ";
                                JR3 = "";

                                Competency1 = "Team Management";
                                Competency2 = "Decision Making";
                                Competency3 = "Drive and Resilience";
                                Competency4 = "Interpersonal Skills ";
                                Competency5 = "Strategy Execution";
                                Competency6 = "Technical Know-how";
                                Competency7 = "Initiative";
                                Competency8 = "";
                            }
                            else if (Convert.ToInt32(Dr["PeapType"]) == 3)
                            {

                                JR1 = "Understanding of & Compliance with vessel’s KPIs including but not limited to Zero Spill, Zero Incidents and Zero off-hire.";
                                JR2 = "Understanding of & Compliance with Policies & Procedures of Owners, Charterers & Company’s Safety Management System. ";
                                JR3 = "Understanding of & Compliance with all Statutory, Flag State, Port State, National and International Regulations. ";

                                Competency1 = "Team Management";
                                Competency2 = "Decision Making";
                                Competency3 = "Drive and Resilience";
                                Competency4 = "Interpersonal Skills ";
                                Competency5 = "Strategy Execution";
                                Competency6 = "Technical Know-how";
                                Competency7 = "Initiative";
                                Competency8 = "";
                            }

                        }
                    }
                    else
                        return;


                    string SqlTraining = "select  " +
                        " (Select TrainingTypeName from TrainingType TT where TrainingTypeID= " +
                        " (SELECT TypeofTraining From Training T Where T.TrainingID=C.TrainingID))TrainingTypetxt  " +
                        " ,(SELECT Trainingname  From Training T Where T.TrainingID=C.TrainingID)TrainingName " +
                        " ,replace(convert(varchar,n_duedate,106),' ','-')Todate  " +
                        " from CrewTrainingRequirement C where CrewID=" + GetCrewID().ToString() + " and isnull(c.source,0)=0";
                    
                    DataSet DsTraining = Budget.getTable(SqlTraining);

                    rpt.Load(Server.MapPath("~/Reporting/AppraisalReport.rpt"));


                    DsTraining.Tables[0].TableName = "ShowAppraisalreportTraining";
                    rpt.SetDataSource(DsTraining.Tables[0]);
                    DataTable dt1 = PrintCrewList.selectCompanyDetails();

                    rpt.SetParameterValue("JR1", JR1);
                    rpt.SetParameterValue("JR2", JR2);
                    rpt.SetParameterValue("JR3", JR3);


                    rpt.SetParameterValue("JRScale1", JRScale1);
                    rpt.SetParameterValue("JRScale2", JRScale2);
                    rpt.SetParameterValue("JRScale3", JRScale3);

                    rpt.SetParameterValue("JRAss1", JRAss1);
                    rpt.SetParameterValue("JRAss2", JRAss2);
                    rpt.SetParameterValue("JRAss3", JRAss3);



                    rpt.SetParameterValue("Competency1", Competency1);
                    rpt.SetParameterValue("Competency2", Competency2);
                    rpt.SetParameterValue("Competency3", Competency3);
                    rpt.SetParameterValue("Competency4", Competency4);
                    rpt.SetParameterValue("Competency5", Competency5);
                    rpt.SetParameterValue("Competency6", Competency6);
                    rpt.SetParameterValue("Competency7", Competency7);
                    rpt.SetParameterValue("Competency8", Competency8);

                    rpt.SetParameterValue("CopmScale1", CopmScale1);
                    rpt.SetParameterValue("CopmScale2", CopmScale2);
                    rpt.SetParameterValue("CopmScale3", CopmScale3);
                    rpt.SetParameterValue("CopmScale4", CopmScale4);
                    rpt.SetParameterValue("CopmScale5", CopmScale5);
                    rpt.SetParameterValue("CopmScale6", CopmScale6);
                    rpt.SetParameterValue("CopmScale7", CopmScale7);
                    rpt.SetParameterValue("CopmScale8", CopmScale8);

                    rpt.SetParameterValue("AppraiseeName", AppraiseeName);
                    rpt.SetParameterValue("AppraiseeRank", AppraiseeRank);
                    rpt.SetParameterValue("AppraiseeCrewNo", AppraiseeCrewNo);

                    rpt.SetParameterValue("AppraiseeVessel", AppraiseeVessel);
                    rpt.SetParameterValue("AppraiseeOccasion", AppraiseeOccasion);
                    rpt.SetParameterValue("AppraiseeAppraisalFrom", AppraiseeAppraisalFrom);
                    rpt.SetParameterValue("AppraiseeAppraisalTo", AppraiseeAppraisalTo);
                    rpt.SetParameterValue("AppraiseeDTJoinedVessel", AppraiseeDTJoinedVessel);
                    rpt.SetParameterValue("AppraiseePerformanceScore", AppraiseePerformanceScore);
                    rpt.SetParameterValue("AppraiseeCompetencyScore", AppraiseeCompetencyScore);
                    rpt.SetParameterValue("PerformanceSecTotal", PerformanceSecTotal);
                    rpt.SetParameterValue("CompetencySecTotal", CompetencySecTotal);
                    rpt.SetParameterValue("PotSecA", PotSecA);
                    rpt.SetParameterValue("PotSecB", PotSecB);
                    rpt.SetParameterValue("PotSecC1", PotSecC1);
                    rpt.SetParameterValue("PotSecC2", PotSecC2);

                    rpt.SetParameterValue("AppraiseeRemarks", AppraiseeRemarks);
                    rpt.SetParameterValue("AppraiserRemark", AppraiserRemark);
                    rpt.SetParameterValue("AppraiserName", AppraiserName);
                    rpt.SetParameterValue("AppraiserRank", AppraiserRank);
                    rpt.SetParameterValue("AppraiserCrewNo", AppraiserCrewNo);
                    rpt.SetParameterValue("AppraiserDate", AppraiserDate);

                    rpt.SetParameterValue("MasterRemark", MasterRemark);
                    rpt.SetParameterValue("MasterName", MasterName);
                    rpt.SetParameterValue("MasterCrewNo", MasterCrewNo);
                    rpt.SetParameterValue("MasterDate", MasterDate);

                    rpt.SetParameterValue("ReviewedBy", ReviewedBy);
                    rpt.SetParameterValue("ReviewedDate", ReviewedDate);
                    rpt.SetParameterValue("ReviewedComment", ReviewedComment);


                    rpt.SetParameterValue("Peaplevel", "");
                    rpt.SetParameterValue("PerformanceFormula", PerformanceFormula);
                    rpt.SetParameterValue("CpmpetencyScore", CpmpetencyScore);
                    rpt.SetParameterValue("AppraisalRecievedDate", AppraisalRecievedDate);


                    rpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, fullPath);

                    lblmsg.Text = "Record Saved Successfully.";
                    btnClosure.Visible = false;
                }
            }
            catch (Exception es)
            {
                lblmsg.Text = "Record Not Saved." + es.Message.ToString();
            }
            ShowData();

    }
    
    public string GetPeapLevelByID(int  id)
    {
        string text = "";
        if (id == 1)text = "Management";
        else if (id == 2) text = "Support";
        else if(id == 3) text = "Operation";
        return text;
    }
    public string GetOccasionByID(int id)
    {
        string text = "";
        if (id == 101) text = "ROUTINE";
        else if (id == 102) text = "ON DEMAND";
        else if (id == 103) text = "INTERIM";
        return text;
    }
    public void ShowTextByPlapLevel()
    {
        int PeapLevel = 1;
        trPerScale3.Visible = true;
        trAssScale8.Visible = true;
        if (PeapLevel == 1)
        {
            lblPerScale1.Text = "Compliance with & enforcement of vessel’s KPIs including but not limited to Zero Spill, Zero Incidents and Zero off-hire. ";
            lblPerScale2.Text = "Compliance with & enforcement of Policies & Procedures from Owners, Charterers & Company’s Safety Management System. ";
            lblPerScale3.Text = "Compliance with & enforcement of all Statutory, Flag State, Port State, National and International Regulations. ";
            
            lblAssScale1.Text = "Decision Making";
            lblAssScale2.Text = "Process Orientation";
            lblAssScale3.Text = "Leadership";
            lblAssScale4.Text = "Strategy Execution";
            lblAssScale5.Text = "Technical Know-How";
            lblAssScale6.Text = "Managing Pressure and Stress";
            lblAssScale7.Text = "Initiative";
            lblAssScale8.Text = "Team Management";
        }
        else if (PeapLevel == 2)
        {
            trPerScale3.Visible = false;
            trAssScale8.Visible = false;
            lblPerScale1.Text = "Compliance with Company’s Policies and Procedures including but not limited to Safety and Protection of environment. ";
            lblPerScale2.Text = "Understanding of & Compliance with Company’s Safety Management System. ";
            lblPerScale3.Text = "";

            
            lblAssScale1.Text = "Team Management";
            lblAssScale2.Text = "Decision Making";
            lblAssScale3.Text = "Drive and Resilience";
            lblAssScale4.Text = "Interpersonal Skills ";
            lblAssScale5.Text = "Strategy Execution";
            lblAssScale6.Text = "Technical Know-how";
            lblAssScale7.Text = "Initiative";
            lblAssScale8.Text = "";
        }
        else if (PeapLevel == 3)
        {
            trAssScale8.Visible = false;

            lblPerScale1.Text = "Understanding of & Compliance with vessel’s KPIs including but not limited to Zero Spill, Zero Incidents and Zero off-hire. ";
            lblPerScale2.Text = "Understanding of & Compliance with Policies & Procedures of Owners, Charterers & Company’s Safety Management System. ";
            lblPerScale3.Text = "Understanding of & Compliance with all Statutory, Flag State, Port State, National and International Regulations. ";
            
            lblAssScale1.Text = "Team Management";
            lblAssScale2.Text = "Decision Making";
            lblAssScale3.Text = "Drive and Resilience";
            lblAssScale4.Text = "Interpersonal Skills ";
            lblAssScale5.Text = "Strategy Execution";
            lblAssScale6.Text = "Technical Know-how";
            lblAssScale7.Text = "Initiative";
            lblAssScale8.Text = "";
        }

    }
}
