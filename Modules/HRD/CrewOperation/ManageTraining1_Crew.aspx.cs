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
using System.Text;
//using Newtonsoft.Json;
using System.IO;
using Ionic.Zip;

public partial class CrewOperation_ManageTraining1_Crew : System.Web.UI.Page
{
    public int VesselId
    {
        get { return Common.CastAsInt32(ViewState["VesselId"]); }
        set {  ViewState["VesselId"] = value;}
    }
    public int CrewID
    {
        get { return Common.CastAsInt32(ViewState["_CrewID111"]); }
        set { ViewState["_CrewID111"] = value; }
    }
    public int RankGroupID
    {
        get { return Common.CastAsInt32(ViewState["_hfdRankGroupID"]); }
        set { ViewState["_hfdRankGroupID"] = value; }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "eee", "SetCalender();", true);
        lblMsg11.Text="";
        //-----------------------------
        if (!IsPostBack)
        {   
            CrewID=Common.CastAsInt32(Request.QueryString["CREW"]);
            ShowCrewDetails();          
            BindSireChap();            
        }
    }
    
    protected void BindTrainings()
    {
        string sqlTrng = " SELECT ROW_NUMBER()over(order by N_DueDate,CREWID)as SNO, CREWID,dbo.fn_getSimilerTrainingsName(TRAININGID) AS TrainingName,TRAININGID,TrainingRequirementId,VesselId,Plannedfor as PlanDate,CASE WHEN ISNULL(SOURCE,0)=1 THEN 'ASSIGNED' WHEN ISNULL(SOURCE,0)=2 THEN 'MATRIX' ELSE 'PEAP' END as Source,N_DueDate as DueDate,dbo.sp_getLastDone(" + CrewID + ",TRAININGID) as lastdone " +
                         " from CREWTRAININGREQUIREMENT WHERE CREWID=" + CrewID + " AND N_DUEDATE IS NOT NULL AND STATUSID='A' AND N_CREWTRAININGSTATUS='O' ORDER BY DueDate ";

        sqlTrng = " SELECT ROW_NUMBER()over(order by N_DueDate,CREWID)as SNO, CREWID,dbo.fn_getSimilerTrainingsName(TRAININGID) AS TrainingName,TRAININGID,TrainingRequirementId,VesselId,Plannedfor as PlanDate,CASE WHEN ISNULL(SOURCE,0)=1 THEN 'ASSIGNED' WHEN ISNULL(SOURCE,0)=2 THEN 'MATRIX' ELSE 'PEAP' END as Source,N_DueDate as DueDate,dbo.sp_getLastDone(" + CrewID + ",TRAININGID) as lastdone " +
                      
                      " from CREWTRAININGREQUIREMENT WHERE CREWID=" + CrewID + " AND PlannedFor IS NOT NULL AND STATUSID='A' AND N_CREWTRAININGSTATUS='O' ORDER BY DueDate ";


        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sqlTrng);

        rptTrainingsDue.DataSource = dt;
        rptTrainingsDue.DataBind();
    }
    
    
    protected void ShowCrewDetails()
    {
        divTrainings.Visible = true;
        dvdue.Visible=true;
        dvdone.Visible=false;

        string sql = " SElect CrewNumber,FirstName+' '+LastName as CrewName,SignOnDate,ReliefDueDate,cpd.currentvesselid,R.RankCode,R.RankGroupID,cpd.crewstatusid,cs.CrewStatusName,v.vesselname,v.vesselcode " +
                     "   from CrewPersonalDetails cpd inner join Rank R on R.RankId = cpd.CurrentrankId inner join CrewStatus cs on cs.CrewStatusId = cpd.CrewStatusId left join vessel v on v.vesselid = cpd.currentvesselid where CrewId ="+ CrewID;
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        if (dt.Rows.Count > 0)
        {
            DataRow dr = dt.Rows[0];
            lblselectedCrewNumber.Text = dr["CrewNumber"].ToString();
            lblselectedUsername.Text = dr["CrewName"].ToString();
            lblselectedRankCode.Text = dr["RankCode"].ToString();
            lblVesselName.InnerText = dr["vesselname"].ToString();
         
            int crewstatusid=Common.CastAsInt32(dr["crewstatusid"]);
            RankGroupID = Common.CastAsInt32(dr["RankGroupID"]);
            if (crewstatusid==3)
            {
                VesselId=Common.CastAsInt32(dr["currentvesselid"]);
                lblCrewStatus.Text=dr["CrewStatusName"].ToString();
                btnImportFromMatrix.Visible = true;
                div_btnImportFromMatrix.Visible = true;
            }   
            lblSignOnDate.Text="[ "+ Common.ToDateString(dr["SignOnDate"])+" : "+Common.ToDateString(dr["ReliefDueDate"])+" ]";
        }
        BindTrainings();
        
    }
    protected void btnAssignTraining_OnClick(object sender, EventArgs e)
    {
        myModal.Visible = true;
    }
    protected void btnCloseMyModel_OnClick(object sender, EventArgs e)
    {
        myModal.Visible = false;
    }
    
    protected void btnCloseDue_OnClick(object sender, EventArgs e)
    {
        string trainingids = "";
        foreach (RepeaterItem item in rptTrainingsDue.Items)
        {
            CheckBox chkTraining = (CheckBox)item.FindControl("chkTraining");
            HiddenField hfdTrainingRequirementId = (HiddenField)item.FindControl("hfdTrainingRequirementId");
            if (chkTraining.Checked)
            {
                trainingids = trainingids + "," + hfdTrainingRequirementId.Value;
            }
        }

        if (trainingids == "")
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "eee", "alert('Please select training.')", true);
            return;
        }
        myModel3.Visible = true;
        try
        {
            string sql = "select TrainingRequirementId,DBO.fn_getSimilerTrainingsName(TRAININGID) as TrainingName,crewid,dbo.sp_getLastDone(crewid,trainingid) as LastDone,n_duedate,PlannedFor,CASE WHEN ISNULL(SOURCE, 0) = 1 THEN 'ASSIGNED' WHEN ISNULL(SOURCE,0)= 2 THEN 'MATRIX' ELSE 'PEAP' END as Sourcename from DBO.CrewTrainingRequirement where TrainingRequirementId in(" + trainingids.Substring(1) + ")";
            rptTrainingMatrix3.DataSource = Common.Execute_Procedures_Select_ByQueryCMS(sql);
            rptTrainingMatrix3.DataBind();
        }
        catch
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "eee", "alert('Please select training.')", true);
            return;
        }
    }
   
    protected void btnShowHistory_OnClick(object sender, EventArgs e)
    {
        string sql= "select ROW_NUMBER()over(order by N_DueDate,CREWID)as SNO,TrainingRequirementId,DBO.fn_getSimilerTrainingsName(TRAININGID) as TrainingName,PlannedFor,FromDate,ToDate,Attended,institutename, CASE WHEN ISNULL(SOURCE, 0) = 1 THEN 'ASSIGNED' WHEN ISNULL(SOURCE,0)= 2 THEN 'MATRIX' ELSE 'PEAP' END as SourceName ,Case when isnull(AttachmentName,'')='' then 0 else 1 end HasFile " +
                   "from DBO.CrewTrainingRequirement ct left join DBO.traininginstitute i on ct.TrainingPlanningId=i.instituteid where ct.crewid=" + CrewID + " and ct.StatusId='A' AND N_CrewTrainingStatus='C' order by N_DueDate ";
        DataTable dt=Common.Execute_Procedures_Select_ByQueryCMS(sql);
        dvdue.Visible=false;
        dvdone.Visible=true;
        rptTrainingsDone.DataSource=dt;
        rptTrainingsDone.DataBind();
    }

    public DataTable BindTrainingInstitute()
    {
        string sql="select InstituteId,InstituteName from TrainingInstitute where InstituteName<>'ONBOARD' order by InstituteName";
        DataTable dt=Common.Execute_Procedures_Select_ByQueryCMS(sql);
        dt.Rows.InsertAt(dt.NewRow(),0);
        dt.Rows[0][0]=0;
        dt.Rows[0][1]="< Select >";
        return dt;
    }

    protected void btnUpdatePlanDuePopup_OnClick(object sender, EventArgs e)
    {
        string trainingids = "";
        foreach (RepeaterItem item in rptTrainingsDue.Items)
        {
            CheckBox chkTraining = (CheckBox)item.FindControl("chkTraining");
            HiddenField hfdTrainingRequirementId = (HiddenField)item.FindControl("hfdTrainingRequirementId");
            if (chkTraining.Checked)
            {
                trainingids = trainingids + "," + hfdTrainingRequirementId.Value;
            }
        }

        if (trainingids == "")
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "eee", "alert('Please select training.')", true);
            return;
        }
        myModel2.Visible = true;
    }
    protected void btnCloseMyModel2_OnClick(object sender, EventArgs e)
    {
        myModel2.Visible = false;
    }
    
    protected void btnCloseMyModel3_OnClick(object sender, EventArgs e)
    {
        myModel3.Visible = false;
    }
     protected void btnSaveClosure_OnClick(object sender, EventArgs e)
    {
        byte[] attachedfile;
        string FileName = "";
        foreach (RepeaterItem item in rptTrainingMatrix3.Items)
        {
            HiddenField hfdTrainingRequirementId = (HiddenField)item.FindControl("hfdTrainingRequirementId");
            TextBox txtFromDate = (TextBox)item.FindControl("txtFromDate");
            TextBox txtToDate = (TextBox)item.FindControl("txtToDate");
            DropDownList ddlTrainingLocation = (DropDownList)item.FindControl("ddlTrainingLocation");
            FileUpload fuAttachment = (FileUpload)item.FindControl("fuAttachment");

            if (txtFromDate.Text=="" || txtToDate.Text=="" )
            {
                lblMsg11.Text = "Please enter actual training dates.";
                return;
            }
            else if(ddlTrainingLocation.SelectedIndex<=0)
            {
                lblMsg11.Text = "Please select training institute.";
                return;
            }

            
            


        }

      

       foreach (RepeaterItem item in rptTrainingMatrix3.Items)
        {

            
            HiddenField hfdTrainingRequirementId = (HiddenField)item.FindControl("hfdTrainingRequirementId");
            TextBox txtFromDate = (TextBox)item.FindControl("txtFromDate");
            TextBox txtToDate = (TextBox)item.FindControl("txtToDate");
            DropDownList ddlTrainingLocation = (DropDownList)item.FindControl("ddlTrainingLocation");
            //Common.Execute_Procedures_Select_ByQueryCMS("UPDATE CrewTrainingRequirement SET fromdate='" + txtFromDate.Text + "',todate='" + txtToDate.Text + "',trainingplanningid=" + ddlTrainingLocation.SelectedValue + ",Attended='Y',N_CrewTrainingStatus='C',N_CrewVerified='Y',N_VerifiedBy=0,N_VerifiedOn=getdate() WHERE TrainingRequirementID = " + hfdTrainingRequirementId.Value);
            
            FileUpload fuAttachment = (FileUpload)item.FindControl("fuAttachment");
            FileName = "";
            if (fuAttachment.HasFile)
            {
                attachedfile = (byte[])fuAttachment.FileBytes;
                FileName = fuAttachment.FileName;
            }
            else
            {
                attachedfile = new byte[0];
            }

            Common.Set_Procedures("sp_UpdateCrewTrainingRequirement");
            Common.Set_ParameterLength(7);
            Common.Set_Parameters(
                 new MyParameter("@TrainingRequirementID", hfdTrainingRequirementId.Value),
                 new MyParameter("@txtFromDate", txtFromDate.Text.Trim()),
                 new MyParameter("@txtToDate", txtToDate.Text.Trim()),
                 new MyParameter("@trainingplanningid", ddlTrainingLocation.SelectedValue),
                 new MyParameter("@Attachment", attachedfile),
                 new MyParameter("@AttachmentName", FileName),
                 new MyParameter("@ModifiedBy", Session["loginid"].ToString())
                );


            Boolean res;
            DataSet Ds = new DataSet();
            res = Common.Execute_Procedures_IUD_CMS(Ds);
            
        }
        myModel3.Visible = false;
        BindTrainings();
    }
    
    protected void btnSavePlanDue_OnClick(object sender, EventArgs e)
    {
        //if( Convert.ToDateTime(txtPlanDue.Text.Trim())<DateTime.Today )
        //{
        //    lblMsgUpdateDueDate.Text = "Please enter valid plan date.";
        //    return;
        //}

        string trainingids = "";
        foreach (RepeaterItem item in rptTrainingsDue.Items)
        {
            CheckBox chkTraining = (CheckBox)item.FindControl("chkTraining");
            HiddenField hfdTrainingRequirementId = (HiddenField)item.FindControl("hfdTrainingRequirementId");
            if (chkTraining.Checked)
            {
                trainingids = trainingids + "," + hfdTrainingRequirementId.Value;
            }
        }

        if (trainingids != "")
            trainingids = trainingids.Substring(1);
        else
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "eee", "alert('Please select training.')", true);
            return;
        }
        string sql = " Exec UpdateTrainingRequirementPlanDue '" + trainingids + "','" + txtPlanDue.Text.Trim() +"',"+ Session["loginid"].ToString() + "" ;
        Common.Execute_Procedures_Select_ByQueryCMS(sql);

        BindTrainings();
        myModel2.Visible = false;
    }

    protected void chkSelAll_OnCheckedChanged(object sender, EventArgs e)
    {
        CheckBox chk = (CheckBox)sender;
        foreach (RepeaterItem item in rptTrainingsDue.Items)
        {
            CheckBox chkTraining = (CheckBox)item.FindControl("chkTraining");
            chkTraining.Checked = chk.Checked;
        }

    }
   

    protected void btnDelete_OnClick(object sender, EventArgs e)
    {
        string trainingids = "";
        foreach (RepeaterItem item in rptTrainingsDue.Items)
        {
            CheckBox chkTraining = (CheckBox)item.FindControl("chkTraining");
            HiddenField hfdTrainingRequirementId = (HiddenField)item.FindControl("hfdTrainingRequirementId");
            HiddenField hfdSource = (HiddenField)item.FindControl("hfdSource");

            if (chkTraining.Checked)
            {
                if (hfdSource.Value.ToLower().ToString() == "peap")
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "eee", "alert('Trainings can not be deleted. Peap has been selected')", true);
                    return;
                }
                else
                {
                    trainingids = trainingids + "," + hfdTrainingRequirementId.Value;
                }

            }
        }

        if (trainingids == "")
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "eee1", "alert('Please select training.')", true);
            return;
        }
        trainingids = trainingids.Substring(1);        
        DeleteTrainingRequirement(trainingids);
        BindTrainings();
    }

    public void DeleteTrainingRequirement(string trids)
    {
        try
        {
            string sql = " Exec CancelTrainingRequirement '" + trids+"'";
            Common.Execute_Procedures_Select_ByQueryCMS(sql);            
        }
        catch (Exception ex)
        {

        }
    }

    public void btndownload_OnClick(object sender, EventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        int TrainingRequirementId = Common.CastAsInt32(btn.CommandArgument);

        DataTable dt1 = Common.Execute_Procedures_Select_ByQuery("select Attachment,AttachmentName from dbo.CrewTrainingRequirement where TrainingRequirementId="+ TrainingRequirementId + "");
        if (dt1.Rows.Count > 0)
        {
            byte[] fileBytes = (byte[])dt1.Rows[0][0];
            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Type", "application/pdf");
            Response.AddHeader("Content-Disposition", "attachment;filename=" + dt1.Rows[0][1]);
            Response.BinaryWrite(fileBytes);
            Response.Flush();
            Response.End();
            
        }
    }
    
    // Assign Training --------------------------------------------------------------------
    protected void btnFinalSave_Click(object sender, EventArgs e)
    {
        bool ANYTRAININGSELECTED = false;
        string trainingids = "";
        for (int i = 0; i <= rptTrainings.Items.Count - 1; i++)
        {
            CheckBox chk = (CheckBox)rptTrainings.Items[i].FindControl("chkSelect");
            if (chk.Checked)
            {
                ANYTRAININGSELECTED = true;
                trainingids += "," + chk.CssClass;
            }
        }
        if (!ANYTRAININGSELECTED)
        {
            lblmsg1.Text = "Please select trainings.";
            return;
        }
        //----------
        if (txt_DueDate.Text.Trim() == "")
        {
            lblmsg1.Text = "Please enter due date.";
            txt_DueDate.Focus();
            return;
        }
        //if (Convert.ToDateTime(txt_DueDate.Text.Trim()) < DateTime.Today)
        //{
        //    lblmsg1.Text = "Please enter valid due date.";
        //    return;
        //}

        trainingids = trainingids.Substring(1);
        string[] crewlist = { CrewID.ToString() };
        string[] traininglist = trainingids.Split(',');

        
        //------
        try
        {
            foreach (string cid in crewlist)
            {
                foreach (string tid in traininglist)
                {
                    int crewid = Common.CastAsInt32(cid);
                    int trainingid = Common.CastAsInt32(tid);
                    DataTable DtExist = Common.Execute_Procedures_Select_ByQueryCMS("SELECT (SELECT CREWNUMBER FROM CrewPersonalDetails CPD WHERE CPD.CREWID=CT.CREWID) AS CREWNUMBER,(SELECT TRAININGNAME FROM Training T WHERE T.TrainingID=CT.TrainingId) AS TrainingName FROM CrewTrainingRequirement CT WHERE CREWID=" + crewid + " AND TRAININGID=" + trainingid + " AND N_CREWTRAININGSTATUS='O'");

                    //if (DtExist.Rows.Count > 0)
                    //{
                    //    lblmsg1.Text = "Sorry ! Crew member ( " + DtExist.Rows[0]["CREWNUMBER"] + " ) already have assigned training ( " + DtExist.Rows[0]["TrainingName"] + " ).";
                    //    return;
                    //}
                    //else
                    //{
                    //Budget.getTable("exec dbo.InsertUpdateCrewTrainingRequirement -1," + crewid + "," + trainingid + ",''," + Session["loginid"].ToString() + "," + Session["loginid"].ToString() + ",'" + txt_DueDate.Text + "','O',0,'N'");
                    //}

                    if (DtExist.Rows.Count <= 0)
                    {
                        Budget.getTable("exec dbo.InsertUpdateCrewTrainingRequirement1 -1," + crewid + "," + trainingid + ",''," + Session["loginid"].ToString() + "," + Session["loginid"].ToString() + ",'" + txt_DueDate.Text + "','O',0,'N',1");
                    }
                }
            }
            lblmsg1.Text = "Trainings assigned successfully.";
            BindSireChap();
            
            BindTrainings();
            rptTrainings.DataSource = null;
            rptTrainings.DataBind();
            txt_DueDate.Text = "";

            myModal.Visible = false;

        }
        catch (Exception ex)
        {
            lblmsg1.Text = ex.Message.ToString();
        }

    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        
    }
    protected void chkSelectGroup_CheckedChanged(object sender, EventArgs e)
    {
        string selected = "";
        for (int i = 0; i <= rptTrainings.Items.Count - 1; i++)
        {
            CheckBox chk = (CheckBox)rptTrainings.Items[i].FindControl("chkSelect");
            if (chk.Checked)
            {
                selected += "," + chk.CssClass;
            }
        }

        string sirechaps = ",-99";
        for (int i = 0; i <= rptTrainingGroup.Items.Count - 1; i++)
        {
            CheckBox chk = (CheckBox)rptTrainingGroup.Items[i].FindControl("chkSelectGroup");
            if (chk.Checked)
            {
                sirechaps += "," + chk.CssClass;
            }
        }
        if (sirechaps.Length > 0)
            sirechaps = sirechaps.Substring(1);

        DataTable dt133 = Common.Execute_Procedures_Select_ByQueryCMS("select trainingid,trainingname,0 as myorder  from training where sirechap in (" + sirechaps + ") order by trainingname");
        for (int i = 0; i <= dt133.Rows.Count - 1; i++)
        {
            if ((selected).Contains("," + dt133.Rows[i]["trainingid"]))
            {
                dt133.Rows[i]["myorder"] = 1;
            }
        }
        DataView V = dt133.DefaultView;
        V.Sort = "myorder desc,trainingname";
        this.rptTrainings.DataSource = V.ToTable();
        this.rptTrainings.DataBind();
        //----------------------
        for (int i = 0; i <= rptTrainings.Items.Count - 1; i++)
        {
            CheckBox chk = (CheckBox)rptTrainings.Items[i].FindControl("chkSelect");
            if ((selected).Contains("," + chk.CssClass))
            {
                chk.Checked = true;
            }
        }

    }
    private void BindSireChap()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("select ChapterNo,Convert(varchar,ChapterNo)+' '+ ChapterName as ChapterName from dbo.m_Chapters where InspectionGroup=1  order by ChapterNo");
        dt.Rows.Add(dt.NewRow());
        dt.Rows[dt.Rows.Count - 1]["ChapterNo"] = -1;
        dt.Rows[dt.Rows.Count - 1]["ChapterName"] = "OTHERS";
        //---
        this.rptTrainingGroup.DataSource = dt;
        this.rptTrainingGroup.DataBind();
    }

    //--------------------------------------
    protected void btnImportFromMatrix_OnClick(object sender, EventArgs e)
    {
        myModal1.Visible = true;
        BindTrainingMatrix();
    }
    protected void btnSaveTrainingsFromMatrix_OnClick(object sender, EventArgs e)
    {
        try
        {
            string trainingids = "";
            foreach (RepeaterItem item in rptTrainingMatrix.Items)
            {
                CheckBox chkTraining = (CheckBox)item.FindControl("chkTraining");
                HiddenField hfdTrainingId = (HiddenField)item.FindControl("hfdTrainingId");
                if (chkTraining.Checked)
                {
                    trainingids = trainingids + "," + hfdTrainingId.Value;
                }
            }

            trainingids = trainingids.Substring(1);
            string[] crewlist = { CrewID.ToString() };
            string[] traininglist = trainingids.Split(',');
            try
            {
                foreach (string cid in crewlist)
                {
                    foreach (string tid in traininglist)
                    {
                        int Crewid = Common.CastAsInt32(cid);
                        int trainingid = Common.CastAsInt32(tid);
                        DataTable DtExist = Common.Execute_Procedures_Select_ByQueryCMS("SELECT (SELECT CREWNUMBER FROM CrewPersonalDetails CPD WHERE CPD.CREWID=CT.CREWID) AS CREWNUMBER,(SELECT TRAININGNAME FROM Training T WHERE T.TrainingID=CT.TrainingId) AS TrainingName FROM CrewTrainingRequirement CT WHERE CREWID=" + Crewid + " AND TRAININGID=" + trainingid + " AND N_CREWTRAININGSTATUS='O'");

                        if (DtExist.Rows.Count <= 0)
                        {
                            Budget.getTable("exec dbo.InsertUpdateCrewTrainingRequirement1 -1," + Crewid + "," + trainingid + ",''," + Session["loginid"].ToString() + "," + Session["loginid"].ToString() + ",'" + txtDueDateTrainingMatrix.Text + "','O',0,'N',2");
                        }
                    }
                }
                txtDueDateTrainingMatrix.Text = "";
                BindTrainings();
                BindTrainingMatrix();
            }
            catch (Exception ex)
            {

            }


        }
        catch (Exception ex)
        {

        }
    }
    protected void BindTrainingMatrix()
    {
        string sqlTrng = " select * from  " +
                  "     ( " +
                  "     select td.trainingid, dbo.fn_getSimilerTrainingsName(td.TRAININGID) AS TrainingName, scheduletype, schedulecount, " +
                  "     dbo.sp_getnextdue(" + CrewID + ", td.trainingid) as DueDate, " +
                  "     dbo.sp_getlastdone(" + CrewID + ", td.trainingid) as LastDoneDate " +
                  "     from trainingmatrixdetails td " +
                  "     inner join training t on td.trainingid = t.trainingid " +
                  "     inner join trainingMatrixRankDetails rd on rd.trainingmatrixid = td.trainingmatrixid and rd.trainingid=td.trainingid and rd.rankid=" + RankGroupID + "" +
                  "     where td.trainingmatrixid in (select tv.trainingmatrixid from  trainingmatrixforvessel tv where tv.vesselid = " + VesselId + ") " +
                  "     ) a  ";


        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sqlTrng);

        rptTrainingMatrix.DataSource = dt;
        rptTrainingMatrix.DataBind();
    }
    protected void btnCloseMyModel1_OnClick(object sender, EventArgs e)
    {
        myModal1.Visible = false;
    }
    protected void chkSeleAllMatrix_OnCheckedChanged(object sender, EventArgs e)
    {
        CheckBox chk = (CheckBox)sender;
        foreach (RepeaterItem item in rptTrainingMatrix.Items)
        {
            CheckBox chkTraining = (CheckBox)item.FindControl("chkTraining");
            chkTraining.Checked = chk.Checked;
        }
    }
}