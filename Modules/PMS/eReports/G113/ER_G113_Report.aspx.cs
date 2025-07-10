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
//using ShipSoft.CrewManager.BusinessLogicLayer;
//using ShipSoft.CrewManager.BusinessObjects;
//using ShipSoft.CrewManager.Operational;

public partial class CrewAppraisal_ER_G113_Report : System.Web.UI.Page
{
    public AuthenticationManager Auth;
    public int CrewID
    {
        get { return Convert.ToInt32("0" + Convert.ToString(ViewState["vCrewID"])); }
        set { ViewState["vCrewID"] = value.ToString(); }
    }    
    public int VesselID
    {
        get { return Convert.ToInt32("0" + ViewState["vVesselID"].ToString()); }
        set { ViewState["vVesselID"] = value.ToString(); }
    }
    public int PeapID
    {
        get { return Common.CastAsInt32("0" + ViewState["PeapID"]); }
        set { ViewState["PeapID"] = value; }
    }
    
    // -------------------------
    public string CrewNumber
    {
        get { return Convert.ToString(ViewState["vCrewNumber"]); }
        set { ViewState["vCrewNumber"] = value; }
    }
    public string VesselCode
    {
        get { return  Convert.ToString(ViewState["vVesselCode"]); }
        set { ViewState["vVesselCode"] = value; }
    }
    public string Location
    {
        get { return Convert.ToString(ViewState["vLocation"]); }
        set { ViewState["vLocation"] = value; }
    }
    // -------------------------

    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        //SessionManager.SessionCheck_New();
        //-----------------------------
        lblmsg.Text = "";
        Location = "S";
        if (!Page.IsPostBack)
        {
            PeapID = Common.CastAsInt32(Page.Request.QueryString["PeapID"]);
            CrewNumber = Convert.ToString(Page.Request.QueryString["CrewNumber"]);
            //BindSireChap();
            if (PeapID > 0)
            {
                ShowAppraisalValues();
            }
            else
            {
                btnSaveEvaluation.Visible = true;                
                ShowHeaderInformation();
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
        DataTable Dt = Common.Execute_Procedures_Select_ByQuery(sql);
        if (Dt.Rows.Count > 0)
        {
            int tid = Common.CastAsInt32(Dt.Rows[0][0]);
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

        Common.Execute_Procedures_Select_ByQuery("exec dbo.InsertUpdateCrewTrainingRequirementFromAppraisal -1," + CrewID + "," + ddlTrainingName.SelectedValue + ",''," + Session["loginid"].ToString() + "," + Session["loginid"].ToString() + ",'" + txt_DueDate.Text + "','O',0,'N'");
        BindTraining();

        //DataSet DS = (DataSet)Session["sTraining"];
        //DataRow[] DrHasValue = DS.Tables[0].Select("TrainingID=" + txtTraining.Text + "");
        //if (DrHasValue.Length > 0)
        //{
        //    lblmsg.Text = "Training already available.";
        //    return;
        //}
        //else
        //{
        //    string sql = "select TrainingTypeName from dbo.trainingtype where TrainingTypeId in (select TypeOfTraining  from dbo.training where TrainingID=" + txtTraining.Text + ")";
        //    DataTable dt = Common.Execute_Procedures_Select_ByQuery_ByQuery(sql);
        //    string sTrainingType="";
        //    if (dt.Rows.Count > 0)
        //        sTrainingType = dt.Rows[0][0].ToString();

        //    DataRow Dr = DS.Tables[0].NewRow();
        //    Dr["TrainingTypetxt"] = sTrainingType;
        //    Dr["TrainingIDtxt"] = txtTrainingName.Text;
        //    Dr["Todatetxt"] = txt_DueDate.Text.Trim();

        //    Dr["TrainingID"] = txtTraining.Text;
        //    Dr["Todatetxt"] = txt_DueDate.Text.Trim();

        //    DS.Tables[0].Rows.Add(Dr);
        //    rptTraining.DataSource = DS;
        //    rptTraining.DataBind();

        //    //Session["sTraining"] = DS;
        //    updata.Update(); 
        //}

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
        ddlTrainingName.DataSource = Common.Execute_Procedures_Select_ByQuery("select trainingid,trainingtypename + ' - ' +trainingname as trainingname from training inner join trainingtype on training.TypeOfTraining=trainingtype.TrainingTypeId where sirechap=" + ddlSireChap1.SelectedValue + " order by trainingtypename,trainingname");
        ddlTrainingName.DataTextField = "trainingname";
        ddlTrainingName.DataValueField = "trainingid";
        ddlTrainingName.DataBind();
        ddlTrainingName.Items.Insert(0, new ListItem("< Select >", ""));
        return;
        //string st = "<select class='required_box' style='width:300px' id='ddlTrainings'>";
        //st += "<option value=''> Select Training </option >";
        //DataTable drCat = Common.Execute_Procedures_Select_ByQuery_ByQueryCMS("select TrainingTypeId,TrainingTypeName from trainingtype order by TrainingTypeName");
        //foreach (DataRow dr in drCat.Rows)
        //{
        //    DataTable dt133 = Common.Execute_Procedures_Select_ByQuery_ByQueryCMS("select trainingid,trainingname from training where sirechap=" + ddlSireChap1.SelectedValue + " and TypeOfTraining=" + dr["TrainingTypeId"].ToString() + " order by trainingname");
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
    public void ShowHeaderInformation()
    {
        string sql = " select "+
                     " VesselCode,CrewId,ContractId,CrewNumber,CrewName,CH.RankId ,R.RankName" +
                     " , replace(convert(varchar(20), DOB, 106), ' ', '-')sDOB "+
                     " ,replace(convert(varchar(20), DJC, 106), ' ', '-')sDJC " +
                     " ,replace(convert(varchar(20), SignOnDate, 106), ' ', '-')sSignOnDate " +
                     " ,replace(convert(varchar(20), SignOffDate, 106), ' ', '-')sSignOffDate " +
                     "  from dbo.PMS_CREW_HISTORY CH inner join dbo.MP_AllRank R on r.rankid=CH.rankid " +
                     " where(SignOffDate is null or SignOffDate >= getdate()) and CrewNumber = '"+CrewNumber+"' ";
        DataTable Dt = Common.Execute_Procedures_Select_ByQuery(sql); 
        if (Dt != null)
        {
            if (Dt.Rows.Count > 0)

            {
                DataRow Dr = Dt.Rows[0];

                //if (IsAppraisalRecordExists(Dr["sSignOnDate"].ToString()))
                //{
                //    btnSaveEvaluation.Visible = false;
                //    lblmsg.Text = "Can not create appraisal. Another appraisal already exists for same period.";
                //}

                VesselCode = Dr["VesselCode"].ToString();
                lblVessel.Text = Dr["VesselCode"].ToString();
                lblFirstName.Text = Dr["CrewName"].ToString();
                lblCrewNo.Text = Dr["CrewNumber"].ToString();
                lblRank.Text = Dr["RankName"].ToString();
                //lblAppFrom.Text = Dr["sSignOnDate"].ToString();
                //lblAppTO.Text = Dr["sSignOffDate"].ToString();
                lblDateJoinedCompany.Text = Dr["sDJC"].ToString();
                lblDtJoinedVessel.Text = Dr["sSignOnDate"].ToString();
                //----------------------------------------
                txtManAppAppraiseeName.Text = Dr["CrewName"].ToString();
                txtManAppAppraiseeCrewNo.Text = Dr["CrewNumber"].ToString();
                txtManAppAppraiseeRank.Text = Dr["RankName"].ToString();
                //----------------------------------------

                //if (Dr["status"].ToString() == "1")
                //{
                //    DataTable Dt = Common.Execute_Procedures_Select_ByQuery_ByQuery("SELECT TechSupdt,MarineSupdt,FleetManager FROM DBO.VESSEL WHERE VESSELCODE IN (select vesselcode from tbl_Assessment WHERE AssMgntID=" + PeapID + ")");
                //    if (Dt.Rows.Count > 0)
                //    {
                //        int UserId = Common.CastAsInt32(Session["loginid"].ToString());

                //        int TechSupdt = Common.CastAsInt32(Dt.Rows[0][0]);
                //        int MarineSupdt = Common.CastAsInt32(Dt.Rows[0][1]);
                //        int FleetManager = Common.CastAsInt32(Dt.Rows[0][2]);

                //        btnSaveEvaluation.Visible = (UserId == TechSupdt) || (UserId == MarineSupdt) || (UserId == FleetManager);
                //    }
                //    btn_Save_PlanTraining.Visible = btnSaveEvaluation.Visible;
                //    btnClosure.Visible = btnSaveEvaluation.Visible;
                //}
            }
            else
            {
                btnSaveEvaluation.Visible = false;
                lblmsg.Text = "Crew does not exists.";
            }
        }
    }
    
    public void ShowAppraisalValues()
    {
        string sql = " select " +
                     " VesselCode,CrewId,ContractId,CrewNumber,CrewName,CH.RankId ,R.RankName" +
                     " , replace(convert(varchar(20), DOB, 106), ' ', '-')sDOB " +
                     " ,replace(convert(varchar(20), DJC, 106), ' ', '-')sDJC " +
                     " ,replace(convert(varchar(20), SignOnDate, 106), ' ', '-')sSignOnDate " +
                     " ,replace(convert(varchar(20), SignOffDate, 106), ' ', '-')sSignOffDate " +
                     "  from dbo.PMS_CREW_HISTORY CH inner join dbo.MP_AllRank R on r.rankid=CH.rankid " +
                     " where(SignOffDate is null or SignOffDate >= getdate()) and CrewNumber = '" + CrewNumber + "' ";

        sql = " select * " +
                //" , replace(convert(varchar(20), DOB, 106), ' ', '-')sDOB " +
                " ,replace(convert(varchar(20), DatejoinedComp, 106), ' ', '-')sDJC " +
                " ,replace(convert(varchar(20), AppraisalFromDate, 106), ' ', '-')sSignOnDate " +
                " ,replace(convert(varchar(20), AppraisalToDate, 106), ' ', '-')sSignOffDate " +

                " ,replace(convert(varchar(20), RemAppraiseeDate, 106), ' ', '-')sRemAppraiseeDate " +
                " ,replace(convert(varchar(20), RemAppraiserDate, 106), ' ', '-')sRemAppraiserDate " +
                " ,replace(convert(varchar(20), RemMasterDate, 106), ' ', '-')sRemMasterDate " +

                " from ER_G113_Report where AssMgntID=" + PeapID;
        DataTable Dt = Common.Execute_Procedures_Select_ByQuery(sql);
        if (Dt != null)
        {
            if (Dt.Rows.Count > 0)
            {
                DataRow Dr = Dt.Rows[0];

                if (Dr["ExportedBy"].ToString() == "")
                {
                    if (Dr["Status"].ToString() == "1")
                    {
                        btnSaveEvaluation.Visible = true;
                        btnExport.Visible = true;
                    }
                    //if (Dr["ExportedBy"].ToString() != "")
                    //{
                    //    btnSaveEvaluation.Visible = false;
                    //    btnExport.Visible = false;
                    //}


                    //HEader
                    ddlOccasion.SelectedValue = Dr["occasion"].ToString();
                    lblPerformanceScore.Text = Dr["PerformanceScore"].ToString();
                    hfPerformanceScore.Value = Dr["PerformanceScore"].ToString();

                    lblCompetencyScore.Text = Dr["CompetenciesScore"].ToString();
                    hfCompetencyScore.Value = Dr["CompetenciesScore"].ToString();
                    lblTotPer.Text = (Common.CastAsInt32(Dr["PerScale1"]) + Common.CastAsInt32(Dr["PerScale2"]) + Common.CastAsInt32(Dr["PerScale3"])).ToString();

                    lblPerPerformanceScore.Text = Dr["PerformanceScore"].ToString();
                    lblAssCompetencyScore.Text = Dr["CompetenciesScore"].ToString();
                    lblTotAss.Text = (Common.CastAsInt32(Dr["AssScale1"]) +
                        Common.CastAsInt32(Dr["AssScale2"]) +
                        Common.CastAsInt32(Dr["AssScale3"]) +
                        Common.CastAsInt32(Dr["AssScale4"]) +
                        Common.CastAsInt32(Dr["AssScale5"]) +
                        Common.CastAsInt32(Dr["AssScale6"]) +
                        Common.CastAsInt32(Dr["AssScale7"]) +
                        Common.CastAsInt32(Dr["AssScale8"])).ToString();

                    VesselCode = Dr["VesselCode"].ToString();
                    lblVessel.Text = Dr["VesselCode"].ToString();
                    lblFirstName.Text = Dr["AssName"].ToString() + " " + Dr["AssLName"].ToString();
                    lblCrewNo.Text = Dr["CrewNo"].ToString();
                    lblRank.Text = Dr["Rank"].ToString();
                    txtAppraisalFromDate.Text = Common.ToDateString(Dr["AppraisalFromDate"].ToString());
                    txtAppraisalToDate.Text = Common.ToDateString(Dr["AppraisalToDate"].ToString());
                    lblDateJoinedCompany.Text = Dr["sDJC"].ToString();
                    lblDtJoinedVessel.Text = Dr["sSignOnDate"].ToString();
                    //----------------------------------------
                    txtManAppAppraiseeName.Text = Dr["AssName"].ToString() + " " + Dr["AssLName"].ToString();
                    txtManAppAppraiseeCrewNo.Text = Dr["CrewNo"].ToString();
                    txtManAppAppraiseeRank.Text = Dr["Rank"].ToString();
                    //----------------------------------------
                    ddlPerScale1.SelectedValue = Dr["PerScale1"].ToString();
                    ddlPerScale2.SelectedValue = Dr["PerScale2"].ToString();
                    ddlPerScale3.SelectedValue = Dr["PerScale3"].ToString();

                    txtPerAss1.Text = Dr["perAss1"].ToString();
                    txtPerAss2.Text = Dr["perAss2"].ToString();
                    txtPerAss3.Text = Dr["perAss3"].ToString();

                    ddlAssScale1.SelectedValue = Dr["AssScale1"].ToString();
                    ddlAssScale2.SelectedValue = Dr["AssScale2"].ToString();
                    ddlAssScale3.SelectedValue = Dr["AssScale3"].ToString();
                    ddlAssScale4.SelectedValue = Dr["AssScale4"].ToString();
                    ddlAssScale5.SelectedValue = Dr["AssScale5"].ToString();
                    ddlAssScale6.SelectedValue = Dr["AssScale6"].ToString();
                    ddlAssScale7.SelectedValue = Dr["AssScale7"].ToString();
                    ddlAssScale8.SelectedValue = Dr["AssScale8"].ToString();


                    txtPotSecA1.Text = Dr["PotSecA"].ToString();
                    if (Dr["PotSecB"].ToString() == "Y")
                        ddlPotReadyForPromotion.SelectedIndex = 1;
                    else if (Dr["PotSecB"].ToString() == "N")
                        ddlPotReadyForPromotion.SelectedIndex = 2;

                    txtPotSecB1.Text = Dr["PotSecB1"].ToString();

                    txtPotSecC1.Text = Dr["PotSecC1"].ToString();
                    txtPotSecC2.Text = Dr["PotSecC2"].ToString();


                    txtManAppAppraiseeRem.Text = Dr["RemAppraiseeComment"].ToString();
                    txtManAppAppraiseeName.Text = Dr["RemAppraiseeName"].ToString();
                    txtManAppAppraiseeCrewNo.Text = Dr["RemAppraiseeCrewNo"].ToString();
                    txtManAppAppraiseeRank.Text = Dr["RemAppraiseeRank"].ToString();
                    txtManAppAppraiseeDate.Text = Dr["sRemAppraiseeDate"].ToString();


                    txtManAppAppraiserRem.Text = Dr["RemAppraiserComment"].ToString();
                    txtManAppAppraiserName.Text = Dr["RemAppraiserName"].ToString();
                    txtManAppAppraiserCrewNo.Text = Dr["RemAppraiserCrewNo"].ToString();
                    txtManAppAppraiserRank.Text = Dr["RemAppraiserRank"].ToString();
                    txtManAppAppraiserDate.Text = Dr["sRemAppraiserDate"].ToString();



                    txtManAppMasterRem.Text = Dr["RemMasterComments"].ToString();
                    txtManAppMasterName.Text = Dr["RemMasterName"].ToString();
                    txtManAppMasterCrewNo.Text = Dr["AppMasterCrewNo"].ToString();
                    txtManAppMasterDate.Text = Dr["sRemMasterDate"].ToString();
                }
                else
                {
                    lblmsg.Text = "This record is already exported.";
                }
            }
            else
            {
                lblmsg.Text = "Crew does not exits.";
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
        DataTable Dt = Common.Execute_Procedures_Select_ByQuery(sql);
        rptTraining.DataSource = Dt;
        rptTraining.DataBind();
    }    
    public bool IsCrewAvailable()
    {
        string sql = " select * from CrewPersonalDetails where CrewNumber='" +lblCrewNo.Text + "' and FirstName='" + lblFirstName.Text+ "' and lastName='" + lblLastName.Text+ "'";
        DataTable Dt = Common.Execute_Procedures_Select_ByQuery(sql);
        if (Dt == null)
            return false;
        else if(Dt.Rows.Count<=0)
            return false;
        else
            return true;
    }
    public string GetVesselCode(int VesselID)
    {
        //VesselCode
        string sql = "select VesselCode from Vessel where VesselID='" + VesselID + "'";
        DataTable Dt = Common.Execute_Procedures_Select_ByQuery(sql);
        if (Dt != null)
        {
            if (Dt.Rows.Count > 0)
            {
                return Dt.Rows[0][0].ToString(); ;
            }
        }
        return "";
    }
    public int GetVesselID(string VesselCode)
    {
        //VesselCode
        string sql = "select VesselID from Vessel where VesselCode='" + VesselCode + "'";
        DataTable Dt = Common.Execute_Procedures_Select_ByQuery(sql);
        if (Dt != null)
        {
            if (Dt.Rows.Count > 0)
            {
                return Common.CastAsInt32(Dt.Rows[0][0].ToString());
            }
        }
        return 0;
    }
    
    public int GetCrewID()
    {
        string sql = "select CrewID from CrewPersonalDetails where CrewNumber='" + lblCrewNo.Text + "'";
        DataTable Dt = Common.Execute_Procedures_Select_ByQuery(sql);
        if (Dt != null)
        {
            if (Dt.Rows.Count > 0)
            {
                return Common.CastAsInt32(Dt.Rows[0][0]);
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

        
        DataTable Dt = Common.Execute_Procedures_Select_ByQuery(sql);
        if (Dt != null)
        {
            rptTraining.DataSource = Dt;
            rptTraining.DataBind();
            Session.Add("sTraining", Dt);
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
    public bool Validate()
    {
        if (ddlOccasion.SelectedIndex == 0)
        {
            lblmsg.Text = "Please select occasion.";
            ddlOccasion.Focus();
            return false;
        }
        if (txtAppraisalFromDate.Text.Trim() == "")
        {
            lblmsg.Text = "Please enter From date.";
            txtAppraisalFromDate.Focus();
            return false;
        }
        if (txtAppraisalToDate.Text.Trim() == "")
        {
            lblmsg.Text = "Please enter To Date.";
            txtAppraisalToDate.Focus();
            return false;
        }

        if (IsAppraisalRecordExists())
        {
            lblmsg.Text = "Can not create appraisal. Another appraisal already exists for same period..";
            return false;
        }
        if (ddlPerScale1.SelectedIndex == 0 || txtPerAss1.Text.Trim() == "" || ddlPerScale2.SelectedIndex== 0 || txtPerAss3.Text.Trim() == "" || ddlPerScale3.SelectedIndex == 0 || txtPerAss3.Text.Trim() == "")
        {
            lblmsg.Text = "Please fill all the fields on performance tab.";
            return false;
        }
        else if (ddlAssScale1.SelectedIndex ==0 || ddlAssScale2.SelectedIndex == 0 || ddlAssScale3.SelectedIndex == 0 || ddlAssScale4.SelectedIndex == 0 || ddlAssScale5.SelectedIndex == 0 || ddlAssScale6.SelectedIndex == 0 || ddlAssScale7.SelectedIndex == 0 || ddlAssScale8.SelectedIndex == 0 )
        {
            lblmsg.Text = "Please fill all the fields on assessment tab.";
            return false;
        }
        else if (txtPotSecA1.Text == "" || ddlPotReadyForPromotion.SelectedIndex == 0 || txtPotSecC1.Text == "" || txtPotSecC2.Text == "")
        {
            lblmsg.Text = "Please fill all the fields on potential tab.";
            return false;
        }
        else if (ddlPotReadyForPromotion.SelectedIndex == 1 && txtPotSecB1.Text == "")
        {
            lblmsg.Text = "Please fill all the fields on potential tab.";
            return false;
        }

        if (txtManAppAppraiseeRem.Text.Trim() == "" || txtManAppAppraiseeName.Text.Trim() == "" || txtManAppAppraiseeCrewNo.Text.Trim() == "" || txtManAppAppraiseeRank.Text.Trim() == "" || txtManAppAppraiseeDate.Text.Trim() == "" || txtManAppAppraiserRem.Text.Trim() == "" || txtManAppAppraiserName.Text.Trim() == "" || txtManAppAppraiserCrewNo.Text.Trim() == "" || txtManAppAppraiserRank.Text.Trim() == "" || txtManAppAppraiserDate.Text.Trim() == "" || txtManAppMasterRem.Text.Trim() == "" || txtManAppMasterName.Text.Trim() == "" || txtManAppMasterCrewNo.Text.Trim() == "" || txtManAppMasterDate.Text.Trim() == "")
        {
            lblmsg.Text = "Please fill all the fields on remarks tab.";
            return false;
        }

        //if (txtManAppAppraiseeCrewNo.Text.Trim().Length > 6 || txtManAppAppraiseeCrewNo.Text.Trim().Substring(0, 2).ToUpper() != "FS")
        //{
        //    lblmsg.Text = "Invalid crew number on remarks tab.";
        //    return false;
        //}

        if (txtManAppAppraiserCrewNo.Text.Trim().Length > 6 || (txtManAppAppraiserCrewNo.Text.Trim().Substring(0, 1).ToUpper() != "S" && txtManAppAppraiserCrewNo.Text.Trim().Substring(0, 1).ToUpper() != "Y"))
        {
            lblmsg.Text = "Invalid crew number on remarks tab.";
            return false;
        }
        if (txtManAppMasterCrewNo.Text.Trim().Length > 6 || (txtManAppMasterCrewNo.Text.Trim().Substring(0, 1).ToUpper() != "S" && txtManAppMasterCrewNo.Text.Trim().Substring(0, 1).ToUpper() != "Y"))
        {
            lblmsg.Text = "Invalid crew number on remarks tab.";
            return false;
        }
        


        return true;

    }
    public bool IsAppraisalRecordExists( )
    {
        bool ret = false;
        //string sql = " select * from tbl_Assessment where CrewNo='"+CrewNumber+"' and AppraisalFromDate='"+ AppraisalFromDate + "' ";
        string sql = " select 1 from ER_G113_Report where CrewNo='" + lblCrewNo.Text + "' and Occasion="+ddlOccasion.SelectedValue+" and AppraisalToDate>='"+txtAppraisalFromDate.Text.Trim()+ "' and AssMgntID<>"+PeapID;
        DataTable Dt = Common.Execute_Procedures_Select_ByQuery(sql);
        if (Dt.Rows.Count > 0)
            ret = true;
        return ret;
    }
    
    protected void btnSaveEvaluation_OnClick(object sender,EventArgs e)
    {
        if (Validate())
        {
            //if (IsAppraisalRecordExists())
            //{
            //    lblmsg.Text = "Can not create appraisal. Another appraisal already exists for same period..";
            //    return ;
            //}
            string PromoRecomm = "";
            if (ddlPotReadyForPromotion.SelectedIndex == 1)
                PromoRecomm = "Y";
            else if (ddlPotReadyForPromotion.SelectedIndex == 2)
                PromoRecomm = "N";
            else
                PromoRecomm = "L";
            
            object obAppFrom;
            if (txtAppraisalFromDate.Text.Trim() == "") obAppFrom = DBNull.Value; else obAppFrom = txtAppraisalFromDate.Text.Trim();


            object obAppTO;
            if (txtAppraisalToDate.Text.Trim() == "") obAppTO = DBNull.Value; else obAppTO = txtAppraisalToDate.Text.Trim();

            object obDateJoinedCompany;
            if (lblDateJoinedCompany.Text.Trim() == "") obDateJoinedCompany = DBNull.Value; else obDateJoinedCompany = lblDateJoinedCompany.Text.Trim();

            object obDtJoinedVessel;
            if (lblDtJoinedVessel.Text.Trim() == "") obDtJoinedVessel = DBNull.Value; else obDtJoinedVessel = lblDtJoinedVessel.Text.Trim();

            object obAppAppraiseeDate = DBNull.Value;
            if (txtManAppAppraiseeDate.Text.Trim() == "") obAppAppraiseeDate = DBNull.Value; else obAppAppraiseeDate = txtManAppAppraiseeDate.Text.Trim();

            object obAppAppraiserDate;
            if (txtManAppAppraiserDate.Text.Trim() == "") obAppAppraiserDate = DBNull.Value; else obAppAppraiserDate = txtManAppAppraiserDate.Text.Trim();

            object obAppMasterDate;
            if (txtManAppMasterDate.Text.Trim() == "") obAppMasterDate = DBNull.Value; else obAppMasterDate = txtManAppMasterDate.Text.Trim();
            

            Common.Set_Procedures("ER_G113_InsertUpdate_ER_G113_Report");
            Common.Set_ParameterLength(55);
            Common.Set_Parameters(
                new MyParameter("@AssMgntID", PeapID),
                new MyParameter("@VesselCode", VesselCode),
                new MyParameter("@Occasion", ddlOccasion.SelectedValue),
                new MyParameter("@PeapType", "1"),
                new MyParameter("@AssName", lblFirstName.Text.Trim()),
                new MyParameter("@AssLname", lblLastName.Text.Trim()),
                new MyParameter("@CrewNo", lblCrewNo.Text.Trim()),
                new MyParameter("@rank", lblRank.Text),
                new MyParameter("@ShipSoftRank", hfRankID.Value),
                new MyParameter("@AppraisalFromDate", obAppFrom),
                new MyParameter("@AppraisalToDate", obAppTO),
                new MyParameter("@DatejoinedComp", obDateJoinedCompany),
                new MyParameter("@DatejoinedVessel", obDtJoinedVessel),
                new MyParameter("@PerformanceScore", Common.CastAsDecimal(hfPerformanceScore.Value)),
                new MyParameter("@CompetenciesScore", Common.CastAsDecimal(hfCompetencyScore.Value)),
                  
                

                
                new MyParameter("@PerScale1", ddlPerScale1.SelectedValue),
                new MyParameter("@PerScale2", ddlPerScale2.SelectedValue),
                new MyParameter("@PerScale3", ddlPerScale3.SelectedValue),
                new MyParameter("@perAss1", txtPerAss1.Text.Trim()),
                new MyParameter("@perAss2", txtPerAss2.Text.Trim()),
                new MyParameter("@perAss3", txtPerAss3.Text.Trim()),
                new MyParameter("@AssScale1", ddlAssScale1.SelectedValue),
                new MyParameter("@AssScale2", ddlAssScale2.SelectedValue),
                new MyParameter("@AssScale3", ddlAssScale3.SelectedValue),
                new MyParameter("@AssScale4", ddlAssScale4.SelectedValue),
                new MyParameter("@AssScale5", ddlAssScale5.SelectedValue),
                new MyParameter("@AssScale6", ddlAssScale6.SelectedValue),
                new MyParameter("@AssScale7", ddlAssScale7.SelectedValue),
                new MyParameter("@AssScale8", ddlAssScale8.SelectedValue),
                new MyParameter("@PotSecA", txtPotSecA1.Text.Trim()),
                new MyParameter("@PotSecB", PromoRecomm),
                new MyParameter("@PotSecB1", txtPotSecB1.Text.Trim()),
                new MyParameter("@PotSecB2", ""),
                new MyParameter("@PotSecC1", txtPotSecC1.Text.Trim()),
                new MyParameter("@PotSecC2", txtPotSecC2.Text.Trim()),
                new MyParameter("@RemAppraiseeComment", txtManAppAppraiseeRem.Text.Trim()),
                new MyParameter("@RemAppraiseeName", txtManAppAppraiseeName.Text.Trim()),
                new MyParameter("@RemAppraiseeRank", txtManAppAppraiseeRank.Text.Trim()),
                new MyParameter("@RemAppraiseeCrewNo", txtManAppAppraiseeCrewNo.Text.Trim()),
                new MyParameter("@RemAppraiseeDate", obAppAppraiseeDate),
                new MyParameter("@RemAppraiserComment", txtManAppAppraiserRem.Text.Trim()),
                new MyParameter("@RemAppraiserName", txtManAppAppraiserName.Text.Trim()),
                new MyParameter("@RemAppraiserCrewNo", txtManAppAppraiserCrewNo.Text.Trim()),
                new MyParameter("@RemAppraiserRank", txtManAppAppraiserRank.Text.Trim()),
                new MyParameter("@RemAppraiserDate", obAppAppraiserDate),
                new MyParameter("@RemMasterComments", txtManAppMasterRem.Text.Trim()),
                new MyParameter("@RemMasterName", txtManAppMasterName.Text.Trim()),
                new MyParameter("@AppMasterCrewNo", txtManAppMasterCrewNo.Text.Trim()),
                new MyParameter("@RemMasterDate", obAppMasterDate),
                new MyParameter("@DateJoinedVesselAppraisee", obDtJoinedVessel),
                new MyParameter("@ReviewedBy", ""),
                new MyParameter("@ReviewedDate", DBNull.Value),
                new MyParameter("@ReviewedComment", ""),

                new MyParameter("@IsTrainingRequired", ((ddlTrainingRequired.SelectedIndex != 0) ? 1 : 0)),
                new MyParameter("@AppraisalRecievedDate", DBNull.Value)
                
                );
            

            DataSet ds = new DataSet();
                try
                {
                    if (Common.Execute_Procedures_IUD(ds))
                    {
                        PeapID = Common.CastAsInt32(ds.Tables[0].Rows[0][0].ToString());
                        btnExport.Visible = true;
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Record saved successfully.');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Error while saving record. "+Common.getLastError()+". ');", true);
                    }
                }
                catch
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Error while saving record."+ Common.getLastError() + "');", true);
                }
        }
    }
    
    protected void btnExport_OnClick(object sender, EventArgs e)
    {
        if (PeapID <= 0)
        {
            lblmsg.Text = "Please first save the form to export.";
            
            return;
        }

        try
        {
            Common.Set_Procedures("[DBO].[sp_Insert_Communication_Export]");
            Common.Set_ParameterLength(5);
            Common.Set_Parameters(
                new MyParameter("@VesselCode", VesselCode),
                new MyParameter("@RecordType", "eForm - G113"),
                new MyParameter("@RecordId", PeapID),
                new MyParameter("@RecordNo", lblCrewNo.Text + "_" + ddlOccasion.SelectedValue + "_" + txtAppraisalFromDate.Text),
                new MyParameter("@CreatedBy", Session["FullName"].ToString().Trim())
            );

            DataSet ds = new DataSet();
            ds.Clear();
            Boolean res;
            res = Common.Execute_Procedures_IUD(ds);
            if (res)
            {
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    lblmsg.Text = ds.Tables[0].Rows[0][0].ToString();
                }
                else
                {
                    lblmsg.Text = "Sent for export successfully.";

                    //string sql = " update ER_G113_Report set ExportedBy='" + Session["FullName"].ToString().Trim() + "',ExportedOn=getdate() where AssMgntID=" + PeapID.ToString() + "";
                    //Common.Execute_Procedures_Select_ByQuery(sql);
                    btnExport.Visible = false;
                    btnSaveEvaluation.Visible = false;
                }
            }
            else
            {
                lblmsg.Text = "Unable to send for export.Error : " + Common.getLastError();

            }
        }
        catch (Exception ex)
        {
            lblmsg.Text = "Unable to send for export.Error : " + ex.Message;
        }
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
