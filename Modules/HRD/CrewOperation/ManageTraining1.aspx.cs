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

public partial class CrewOperation_ManageTraining1 : System.Web.UI.Page
{
    public int VesselId
    {
        get { return Common.CastAsInt32(ViewState["VesselId"]); }
        set {  ViewState["VesselId"] = value;}
    }
    public string VesselCode
    {
        get { return Convert.ToString (ViewState["_VesselCode"]); }
        set { ViewState["_VesselCode"] = value; }
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
    public int TrainingMatrixId
    {
        get { return Common.CastAsInt32(ViewState["_TrainingMatrixId"]); }
        set { ViewState["_TrainingMatrixId"] = value; }
    }
    

    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "eee", "SetCalender();", true);
        lblMsg11.Text="";
        lblMsgTrainingMatrix.Text = "";
        //-----------------------------
        if (!IsPostBack)
        {   
            VesselId = Common.CastAsInt32(Request.QueryString["VSL"]);
            ShowVesselName();
            BindSireChap();
            BindCrewMember();

            if (Session["loginid"].ToString() == "1")
                btnDelete.Visible = true;
        }
    }
    protected void ShowVesselName()
    {
        string sqlTotRank = "select Vesselname,VesselCode from Vessel where VesselId=" + VesselId;
        DataTable DtVEssname = Common.Execute_Procedures_Select_ByQueryCMS(sqlTotRank);
        if (DtVEssname.Rows.Count > 0)
        {
            lblVesselName.InnerText = DtVEssname.Rows[0][0].ToString();
            VesselCode= DtVEssname.Rows[0][1].ToString();
        }
        
    }
    protected void BindCrewMember()
    {
        string sqlTotRank = " select *,Planned-Completed as Remaining from vw_Training_Onboard_Planned where CurrentVesselId = "+ VesselId + " ORDER BY RANKID ";
        DataTable DtCrew = Common.Execute_Procedures_Select_ByQueryCMS(sqlTotRank);
        rptCrewMember.DataSource = DtCrew;
        rptCrewMember.DataBind();
    }
    protected void BindTrainings()
    {
        string sqlTrng = " SELECT ROW_NUMBER()over(order by N_DueDate,CREWID)as SNO, CREWID,dbo.fn_getSimilerTrainingsName(TRAININGID) AS TrainingName,TRAININGID,TrainingRequirementId,VesselId,Plannedfor as PlanDate,CASE WHEN ISNULL(SOURCE,0)=1 THEN 'ASSIGNED' WHEN ISNULL(SOURCE,0)=2 THEN 'MATRIX' ELSE 'PEAP' END as Source,N_DueDate as DueDate,dbo.sp_getLastDone(" + CrewID + ",TRAININGID) as lastdone, " +
                  " (CASE WHEN EXISTS(SELECT ApplyType FROM TrainingMatrixRankDetails tr WHERE tr.TrainingMatrixId=" + TrainingMatrixId +
                  " AND tr.TrainingId in( " +
                  "     select TrainingId " +
                  "     union " +
                  "     select trainingid from Training t " +
                  "     where t.trainingid in (select d.SimilerTrainingId from TrainingSimiler d where d.TrainingId= TrainingId) " +
                  "     union select trainingid from Training t " +
                  "     where t.trainingid in (select d.TrainingId from TrainingSimiler d where d.SimilerTrainingId= TrainingId) " +
                  " ) " +
                  " AND tr.RankId=" + RankGroupID + " AND ApplyType =1) THEN '' ELSE ' [ NA ]' End) as MatrixCompatibility " +
                  " from CREWTRAININGREQUIREMENT WHERE CREWID=" + CrewID + " AND PlannedFor IS NOT NULL AND STATUSID='A' AND N_CREWTRAININGSTATUS='O' ORDER BY PlannedFor ";


        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sqlTrng);

        rptTrainingsDue.DataSource = dt;
        rptTrainingsDue.DataBind();
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
    protected int SetTrainingMatrixID(int VesselId)
    {
        int TrainingMatrixId = 0;
        string sql = " select * from TrainingMatrixForVessel Where VesselID=" + VesselId;
        DataTable Dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        if (Dt.Rows.Count > 0)
        {
            TrainingMatrixId = Common.CastAsInt32(Dt.Rows[0]["TrainingMatrixId"]);
        }
        return TrainingMatrixId;
    }

    //--------------------------------------------------------------------
    protected void lnkCrewNumber_OnClick(object sender, EventArgs e)
    {
        divTrainings.Visible = true;
        LinkButton lnk = (LinkButton)sender;
        CrewID = Common.CastAsInt32(lnk.CommandArgument);
        dvdue.Visible=true;
        dvdone.Visible=false;
        HiddenField hfdRankGroupID = (HiddenField)lnk.Parent.FindControl("hfdRankGroupID");
        


        RankGroupID = Common.CastAsInt32(hfdRankGroupID.Value);
        TrainingMatrixId = SetTrainingMatrixID(VesselId);

        string sql = " SElect CrewNumber,FirstName+' '+LastName as CrewName,SignOnDate,ReliefDueDate ,R.RankCode "+
                     "   from CrewPersonalDetails cpd inner join Rank R on R.RankId = cpd.CurrentrankId where CrewId ="+ CrewID;
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        if (dt.Rows.Count > 0)
        {
            DataRow dr = dt.Rows[0];
            lblselectedCrewNumber.Text = dr["CrewNumber"].ToString();
            lblselectedUsername.Text = dr["CrewName"].ToString();
            lblselectedRankCode.Text = dr["RankCode"].ToString();

            lblSignOnDate.Text="[ "+ Common.ToDateString(dr["SignOnDate"])+" : "+Common.ToDateString(dr["ReliefDueDate"])+" ]";
        }
        BindTrainings();
        BindCrewMember();
        //updatepanel1.Update();
    }
    protected void btnAssignTraining_OnClick(object sender, EventArgs e)
    {
        myModal.Visible = false;
        txt_DueDate.Text = "";
        lblmsg1.Text = "";
        myModal.Visible = true;
    }
    protected void btnCloseMyModel_OnClick(object sender, EventArgs e)
    {
        myModal.Visible = false;
    }
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
            if (trainingids == "")
            {
                lblMsgTrainingMatrix.Text = "Please select training.";
                return;
            }

            if (txtDueDateTrainingMatrix.Text.Trim() == "")
            {
                lblMsgTrainingMatrix.Text = "Please enter planned for date.";
                txtDueDateTrainingMatrix.Focus();
                return;
            }
            try
            {
                if (Convert.ToDateTime(txtDueDateTrainingMatrix.Text.Trim()) < DateTime.Today)
                {
                    lblMsgTrainingMatrix.Text = "Please enter valid valid planned for date.";
                    return;
                }
            }
            catch
            {
                lblMsgTrainingMatrix.Text = "Please enter valid valid planned for date.";
                return;
            }

            trainingids = trainingids.Substring(1);

            string[] crewlist;
            if (chkApplyAllCrew.Checked)
            {
                string allCrewID = "";
                foreach (RepeaterItem item in rptCrewMember.Items)
                {
                    HiddenField hfdCrewID = (HiddenField)item.FindControl("hfdCrewID");
                    allCrewID = allCrewID + "," + hfdCrewID.Value;
                }
                allCrewID = allCrewID.Substring(1);
                crewlist = allCrewID.Split(',');
            }
            else
            {
                crewlist = new string[] { CrewID.ToString() };
            }

            
            string[] traininglist = trainingids.Split(',');
            try
            {
                foreach (string cid in crewlist)
                {
                    foreach (string tid in traininglist)
                    {
                        int Crewid = Common.CastAsInt32(cid);
                        int trainingid = Common.CastAsInt32(tid);
                        DataTable DtExist = Common.Execute_Procedures_Select_ByQueryCMS("SELECT (SELECT CREWNUMBER FROM CrewPersonalDetails CPD WHERE CPD.CREWID=CT.CREWID) AS CREWNUMBER,(SELECT TRAININGNAME FROM Training T WHERE T.TrainingID=CT.TrainingId) AS TrainingName FROM CrewTrainingRequirement CT WHERE CREWID=" + Crewid + " AND TRAININGID=" + trainingid + " AND N_CREWTRAININGSTATUS='O' and PlannedFor is not null");

                        if (DtExist.Rows.Count <= 0)
                        {
                            Budget.getTable("exec dbo.InsertUpdateCrewTrainingRequirement1 -1," + Crewid + "," + trainingid + ",''," + Session["loginid"] .ToString()+ "," + Session["loginid"].ToString() + ",'" + txtDueDateTrainingMatrix.Text + "','O',0,'N',2");
                        }


                    }
                }
		        txtDueDateTrainingMatrix.Text="";
                chkApplyAllCrew.Checked = false;
                BindTrainings();
                BindTrainingMatrix();
                BindCrewMember();
            }
            catch (Exception ex)
            {
                
            }

            
        }
        catch (Exception ex)
        {
            
        }
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

    protected void btnCloseMyModel1_OnClick(object sender, EventArgs e)
    {
        myModal1.Visible = false;
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
            
        string sql="select TrainingRequirementId,DBO.fn_getSimilerTrainingsName(TRAININGID) as TrainingName,crewid,dbo.sp_getLastDone(crewid,trainingid) as LastDone,n_duedate,PlannedFor,CASE WHEN ISNULL(SOURCE, 0) = 1 THEN 'ASSIGNED' WHEN ISNULL(SOURCE,0)= 2 THEN 'MATRIX' ELSE 'PEAP' END as Sourcename from DBO.CrewTrainingRequirement where TrainingRequirementId in(" + trainingids.Substring(1) + ")";
        rptTrainingMatrix3.DataSource=Common.Execute_Procedures_Select_ByQueryCMS(sql);
        rptTrainingMatrix3.DataBind();
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

    protected void btnDeleteTraining_OnClick(object sender, EventArgs e)
    {
        try
        {
            ImageButton btn = (ImageButton)sender;
            string trids = btn.CommandArgument;

            DeleteTrainingRequirement(trids);

            //string sql = " Exec CancelTrainingRequirement '" + trids + "'," + VesselId;
            //Common.Execute_Procedures_Select_ByQueryCMS(sql);
            BindCrewMember();
            BindTrainings();
        }
        catch (Exception ex)
        {

        }
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
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "eee11", "alert('Please select training.')", true);
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

            //Common.Execute_Procedures_Select_ByQueryCMS("UPDATE CrewTrainingRequirement SET fromdate='" + txtFromDate.Text + "',todate='" + txtToDate.Text + "',trainingplanningid=" + ddlTrainingLocation.SelectedValue + ",Attended='Y',N_CrewTrainingStatus='C',N_CrewVerified='Y',N_VerifiedBy=0,N_VerifiedOn=getdate() WHERE TrainingRequirementID = " + hfdTrainingRequirementId.Value);

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
        if (txtPlanDue.Text.Trim() =="")
        {
            lblMsgUpdateDueDate.Text = "Please enter plan date.";
            return;
        }
        try
        {
            if (Convert.ToDateTime(txtPlanDue.Text.Trim()) < DateTime.Today)
            {
                lblMsgUpdateDueDate.Text = "Please enter valid plan date.";
                return;
            }
        }
        catch
        {
            lblMsgUpdateDueDate.Text = "Please enter valid plan date.";
            return;
        }

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

        BindCrewMember();
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
    protected void btnExportToShip_OnClick(object sender, EventArgs e)
    {

        string sql = "select isnull(email,'') from DBO.vessel where vesselid=" + VesselId;
        DataTable dtemail = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        string email="";
        if (dtemail.Rows.Count > 0)
            email = dtemail.Rows[0][0].ToString();

        //email = "emanager@energiossolutions.com";
        if (email.Trim() != "")
        {
            sql = "select V.VesselCode,TrainingRequirementId,ct.Crewid,TrainingId,DBO.fn_getSimilerTrainingsName(TRAININGID) as TrainingName, CASE WHEN ISNULL(SOURCE, 0) = 1 THEN 'ASSIGNED' WHEN ISNULL(SOURCE,0)= 2 THEN 'MATRIX' ELSE 'PEAP' END as Source, N_DueDate,PlannedFor,dbo.sp_getLastDone(ct.crewid, trainingid) as LastDone " +
                         "from CREWPERSONALDETAILS CPD INNER JOIN CrewTrainingRequirement ct ON CT.statusid = 'A' AND PlannedFor is not null AND CT.CREWID=CPD.CREWID AND CPD.CREWSTATUSID=3 AND CPD.CURRENTVESSELID=" + VesselId +
                         "INNER JOIN Vessel v on V.VesselId = CPD.CURRENTVESSELID WHERE PlannedFor>='01-nOV-2017'";

            DataTable DT_PMS_CREW_TRAININGREQUIREMENT = Common.Execute_Procedures_Select_ByQueryCMS(sql);
            DT_PMS_CREW_TRAININGREQUIREMENT.TableName = "PMS_CREW_TRAININGREQUIREMENT";

            DataSet ds = new DataSet();
            ds.Tables.Add(DT_PMS_CREW_TRAININGREQUIREMENT.Copy());

            string SchemaFile = Server.MapPath("~/TEMP/SCHEMA_PMS_CREW_TRAININGREQUIREMENT.xml");
            string DataFile = Server.MapPath("~/TEMP/DATA_PMS_CREW_TRAININGREQUIREMENT.xml");

            ds.WriteXml(DataFile);
            ds.WriteXmlSchema(SchemaFile);
            string ZipData = Server.MapPath("~/TEMP/PMS_CREW_TRAININGREQUIREMENT_" + VesselCode + "_O.zip");
            if (File.Exists(ZipData)) { File.Delete(ZipData); }

            using (ZipFile zip = new ZipFile())
            {
                zip.AddFile(SchemaFile);
                zip.AddFile(DataFile);
                zip.Save(ZipData);
                string[] toadd = { email };
                string[] noadd = { };
                string error="";
                string msg = "Dear Captain,<br/><br/>Please import the attached packet in PMS System.<br/><br/>Thanks";
                SendEmail.SendeMail(Common.CastAsInt32(Session["loginid"]), "emanager@energiossolutions.com", "emanager@energiossolutions.com", toadd, noadd, noadd, "CREW TRAINING UPDATE FROM OFFICE", msg,out error, ZipData);
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "ee5e", "alert('Sent to vessel successfully.')", true);

                //Response.Clear();
                //Response.ContentType = "application/zip";
                //Response.AddHeader("Content-Type", "application/zip");
                //Response.AddHeader("Content-Disposition", "inline;filename=" + Path.GetFileName(ZipData));
                //Response.WriteFile(ZipData);
                //Response.End();
            }
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
        DeleteTrainingRequirement(trainingids);
        BindCrewMember();
        BindTrainings();
    }

    public void DeleteTrainingRequirement(string trids)
    {
        try
        {
            string sql = " Exec CancelTrainingRequirement '" + trids+"'";
            Common.Execute_Procedures_Select_ByQueryCMS(sql);
            BindCrewMember();
            BindTrainings();
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
        try
        {
            if (Convert.ToDateTime(txt_DueDate.Text.Trim()) < DateTime.Today)
            {
                lblmsg1.Text = "Please enter valid due date.";
                return;
            }
        }
        catch
        {
            lblmsg1.Text = "Please enter valid due date.";
            return;
        }

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
            BindCrewMember();
            BindTrainings();
            rptTrainings.DataSource = null;
            rptTrainings.DataBind();
            txt_DueDate.Text = "";

            myModal.Visible = false;
            lblmsg1.Text = "";

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
    
    

}