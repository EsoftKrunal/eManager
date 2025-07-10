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
using ShipSoft.CrewManager.BusinessObjects;
using ShipSoft.CrewManager.BusinessLogicLayer;
using ShipSoft.CrewManager.Operational;

public partial class CrewOperation_AssignTraining : System.Web.UI.Page
{
    
    AuthenticationManager Auth;
    public int LastTrainingId
    {
        get { return Common.CastAsInt32(ViewState["LastTrainingId"]); }
        set { ViewState["LastTrainingId"] = value; }
    }
    private void BindVessel()
    {
        DataSet ds = cls_SearchReliever.getMasterData("Vessel", "VesselId", "VesselName", Convert.ToInt32(Session["loginid"].ToString()));
        ddl_Vessel.DataValueField = "VesselId";
        ddl_Vessel.DataTextField = "VesselName";
        ddl_Vessel.DataSource = ds;
        ddl_Vessel.DataBind();
        ddl_Vessel.Items.Insert(0, new ListItem("< Select >", "0"));
    }
    private void BindRankDropDown()
    {
        ProcessSelectRank obj = new ProcessSelectRank();
        obj.Invoke();
        ddl_Rank_Search.DataSource = obj.ResultSet.Tables[0];
        ddl_Rank_Search.DataTextField = "RankName";
        ddl_Rank_Search.DataValueField = "RankId";
        ddl_Rank_Search.DataBind();

    }

    private void BindTrainingName()
    {
        DataTable dt133 = Common.Execute_Procedures_Select_ByQueryCMS("select trainingid,trainingname from training with(nolock) order by trainingname");
        this.ddl_Training.DataValueField = "trainingid";
        this.ddl_Training.DataTextField = "trainingname";
        this.ddl_Training.DataSource = dt133;
        this.ddl_Training.DataBind();

        ddl_Training.Items.Insert(0, new ListItem(" Select ", "0"));
    }
    //private void BindSireChap()
    //{
    //    DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("select ChapterNo,Convert(varchar,ChapterNo)+' '+ ChapterName as ChapterName from dbo.m_Chapters where InspectionGroup=1  order by ChapterNo");
    //    this.ddl_SireChap.DataValueField = "ChapterNo";
    //    this.ddl_SireChap.DataTextField = "ChapterName";
    //    this.ddl_SireChap.DataSource = dt;
    //    this.ddl_SireChap.DataBind();
    //    ddl_SireChap.Items.Add(new ListItem("OTHERS", "-1"));
    //    ddl_SireChap.Items.Insert(0, new ListItem(" Select ", "0"));
    //    ddlSireChap_SelectedIndexChanged(new object(), new EventArgs());

    //    dt.Rows.Add(dt.NewRow());
    //    dt.Rows[dt.Rows.Count - 1]["ChapterNo"] = -1;
    //    dt.Rows[dt.Rows.Count - 1]["ChapterName"] = "OTHERS";
    //    //---
    //    this.rptTrainingGroup.DataSource = dt;
    //    this.rptTrainingGroup.DataBind();
    //}
    //protected void ddlSireChap_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    //DataTable dt133 = Common.Execute_Procedures_Select_ByQueryCMS("select trainingid,trainingname from training where sirechap=" + ddl_SireChap.SelectedValue + " order by trainingname");
    //    DataTable dt133 = Common.Execute_Procedures_Select_ByQueryCMS("select trainingid,trainingname from training with(nolock) order by trainingname");
    //    this.ddl_Training.DataValueField = "trainingid";
    //    this.ddl_Training.DataTextField = "trainingname";
    //    this.ddl_Training.DataSource = dt133;
    //    this.ddl_Training.DataBind();

    //    ddl_Training.Items.Insert(0, new ListItem(" Select ", "0"));

    //    //string st = "<select class='required_box' style='width:400px' id='ddlTrainings' onchange='setSearch()'>";
    //    //st += "<option value='' > Select Training </option >";
    //    //DataTable drCat = Common.Execute_Procedures_Select_ByQueryCMS("select TrainingTypeId,TrainingTypeName from trainingtype order by TrainingTypeName");
    //    //if(ddl_SireChap.SelectedIndex!=0)
    //    //foreach (DataRow dr in drCat.Rows)
    //    //{
    //    //     if(dt133.Rows.Count>0)
    //    //    {
    //    //        st+="<optgroup label='-- " + dr["TrainingTypeName"].ToString() + " --'></optgroup>";
    //    //        foreach (DataRow dr1 in dt133.Rows)
    //    //        {
    //    //            string selected = (txthTid.Text.Trim() == dr1["trainingid"].ToString().Trim())?"selected":"";
    //    //            st += "<option value='" + dr1["trainingid"].ToString() + "' " + selected + " >" + dr1["trainingname"].ToString() + "</option >";
    //    //        }
    //    //    }
    //    //}
    //    //st+="</select>";
    //    //litSelectTr.Text = st;
    //}
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        //***********Code to check page acessing Permission
        this.lbl_Message.Text = "";
        lblmsg1.Text = "";

        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 11);
        if (chpageauth <= 0)
        {
            Response.Redirect("Dummy_Training1.aspx");
        }
        Auth =new AuthenticationManager(11,Convert.ToInt32(Session["loginid"].ToString()),ObjectType.Page);
        btn_Save_PlanTraining.Visible = Auth.IsAdd;

        if (!Page.IsPostBack)
        {
            BindVessel();
            BindTrainingName();
            //BindSireChap();
            BindRankDropDown();
        }
    }
    protected void btn_Save_PlanTraining_Click(object sender, EventArgs e)
    {
        if (ddl_Training.SelectedIndex<=0)
        {
            lbl_Message.Text = "Please select Training.";
            ddl_Training.Focus();
            return; 
        }
        if (txt_DueDate.Text.Trim()== "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "test", "alert('Please Enter Valid Due Date.');", true); 
            lbl_Message.Text = "Please Enter Due Date.";
            txt_DueDate.Focus();
            return;
        }
        DateTime dt;
        if (! DateTime.TryParse(txt_DueDate.Text,out dt))
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "test", "alert('Please Enter Valid Due Date.');", true); 
            lbl_Message.Text = "Please Enter Valid Due Date.";
            txt_DueDate.Focus();
            return;
        }
                
        try
        {
            bool ANYSELECTED = false;
            for (int i = 0; i <= rptAssignTraining.Items.Count - 1; i++)
            {
                HiddenField hfd = (HiddenField)rptAssignTraining.Items[i].FindControl("hfdCrewId");
                CheckBox chk = (CheckBox)rptAssignTraining.Items[i].FindControl("chkSelect");
                if (chk.Checked)
                {
                    ANYSELECTED = true;
                    ///------------------- dupcasy check
                    DataTable DtExist = Common.Execute_Procedures_Select_ByQueryCMS("SELECT * FROM CrewTrainingRequirement WHERE CREWID=" + hfd.Value + " AND TRAININGID=" + ddl_Training.SelectedValue + " AND N_CREWTRAININGSTATUS='O'");
                    //if (DtExist.Rows.Count > 0)
                    //{
                    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "test", "alert('Selected training is already assigned.');", true);
                    //    return;
                    //}
                    ///------------------- dupcasy check end
                    ///
                    if (DtExist.Rows.Count <= 0)
                        Budget.getTable("exec dbo.InsertUpdateCrewTrainingRequirement -1," + hfd.Value + "," + ddl_Training.SelectedValue + ",''," + Session["loginid"].ToString() + "," + Session["loginid"].ToString() + ",'" + txt_DueDate.Text + "','O',0,'N'");
                   
                }
            }
            if (!ANYSELECTED)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "test", "alert('Please Select a Crew Member');", true);
                lbl_Message.Text = "Please Select a Crew Member.";
            }
            else
            {
                lbl_Message.Text = "Training Assigned Successfully.";
            }
        }
        catch
        {
            lbl_Message.Text = "Unable to assign training to selected crew members.";
        }
    }
    protected void btn_Search_Click(object sender, EventArgs e)
    {
        int trainingid = Common.CastAsInt32(txthTid.Text);
        string qry = "";
        if (trainingid > 0)
        {
            qry = "SELECT CrewId,CrewNumber,FullName,CrewStatusName, " +
                       "(select vesselcode from vessel v where v.vesselid=A.vesselid) as VesselName, " +
                       "(select RankCode from Rank R where R.RankId=A.RankId) as RankName, " +
                       "CrewStatusId,Vesselid,Rankid,replace(convert(varchar,dbo.sp_getLastDone(CrewId," + trainingid + "),106),' ','-') as DoneDt,replace(convert(varchar,dbo.sp_getNextDue(CrewId," + trainingid + "),106),' ','-') as NextDueDt,replace(convert(varchar,dbo.sp_getNextPlanDate(CrewId," + trainingid + "),106),' ','-') as NextPlanDt " +
                       "from " +
                       "( " +
                       "SELECT cpd.CrewId,CrewNumber,FIRSTNAME + ' ' + MIDDLENAME + ' ' + LASTNAME AS FullName,CrewStatusName,cpd.CrewStatusId, " +
                       "VesselId=case " +
                       "when cpd.crewstatusid=2 then cpd.lastvesselid " +
                       "when cpd.crewstatusid=3 then cpd.currentvesselid " +
                       "else '' end, " +
                       "RankId=case " +
                       "when cpd.crewstatusid=1 then cpd.RankAppliedId " +
                       "else cpd.CurrentRankId end " +
                       "from crewpersonaldetails cpd inner join crewstatus cs on cpd.crewstatusid=cs.crewstatusid and cs.crewstatusid in (1,2,3) and left(crewnumber,1) <> 'F'" +
                       ") A";
        }
        else
        {
            qry = "SELECT CrewId,CrewNumber,FullName,CrewStatusName, " +
                        "(select vesselcode from vessel v where v.vesselid=A.vesselid) as VesselName, " +
                        "(select RankCode from Rank R where R.RankId=A.RankId) as RankName, " +
                        "CrewStatusId,Vesselid,Rankid,'' as DoneDt,'' as NextDueDt,'' as NextPlanDt " +
                        "from " +
                        "( " +
                        "SELECT cpd.CrewId,CrewNumber,FIRSTNAME + ' ' + MIDDLENAME + ' ' + LASTNAME AS FullName,CrewStatusName,cpd.CrewStatusId, " +
                        "VesselId=case " +
                        "when cpd.crewstatusid=2 then cpd.lastvesselid " +
                        "when cpd.crewstatusid=3 then cpd.currentvesselid " +
                        "else '' end, " +
                        "RankId=case " +
                        "when cpd.crewstatusid=1 then cpd.RankAppliedId " +
                        "else cpd.CurrentRankId end " +
                        "from crewpersonaldetails cpd inner join crewstatus cs on cpd.crewstatusid=cs.crewstatusid and cs.crewstatusid in (1,2,3) and left(crewnumber,1) <> 'F'" +
                        ") A";
        }
        string filter=" Where 1=1 ";

        if (txt_MemberId.Text != "") filter = filter + " And CrewNumber='" + txt_MemberId.Text.Trim() + "'";
        if (ddl_CrewStatus_Search.SelectedIndex > 0) filter = filter + " And crewstatusid=" + ddl_CrewStatus_Search.SelectedValue;
        if (ddl_Rank_Search.SelectedIndex > 0) filter = filter + " And RankId=" + ddl_Rank_Search.SelectedValue;
        if (ddl_Vessel.SelectedIndex > 0) filter = filter + " And VesselId=" + ddl_Vessel.SelectedValue;
        DataSet ds=Budget.getTable(qry + filter);
        rptAssignTraining.DataSource = ds.Tables[0];
        rptAssignTraining.DataBind();
        //ddlSireChap_SelectedIndexChanged(sender, e); 
    }
    protected void btn_Go_Click(object sender, EventArgs e)
    {
        string crewids = "";
        bool ANYSELECTED = false;
        for (int i = 0; i <= rptAssignTraining.Items.Count - 1; i++)
        {
            HiddenField hfd = (HiddenField)rptAssignTraining.Items[i].FindControl("hfdCrewId");
            CheckBox chk = (CheckBox)rptAssignTraining.Items[i].FindControl("chkSelect");
            if (chk.Checked)
            {
                ANYSELECTED = true;
                crewids += "," + hfd.Value;
            }
        }
        if (!ANYSELECTED)
        {
            lbl_Message.Text = "Please Select a Crew Member.";
        }
        else
        {
            crewids = crewids.Substring(1);
            string sql = "SELECT CrewId,CrewNumber,FIRSTNAME + ' ' + MIDDLENAME + ' ' + LASTNAME AS FullName,(select RankCode from Rank R where R.RankId in (case when cpd.crewstatusid=1 then cpd.RankAppliedId else cpd.CurrentRankId end)) as RankName from crewpersonaldetails cpd where cpd.crewid in (" + crewids  + ")";
            Response.Write(sql);
            DataSet ds = Budget.getTable(sql);
            rptCrew.DataSource = ds.Tables[0];
            rptCrew.DataBind();
            dvFrame.Visible = true;
        }
    }

    protected void btnFinalSave_Click(object sender,EventArgs e)
    {
        bool ANYSELECTED = false;
        string crewids = "";
        for (int i = 0; i <= rptCrew.Items.Count - 1; i++)
        {
            HiddenField hfd = (HiddenField)rptCrew.Items[i].FindControl("hfdCrewId");
            CheckBox chk = (CheckBox)rptCrew.Items[i].FindControl("chkSelect");
            if (chk.Checked)
            {
                ANYSELECTED = true;
                crewids += "," + hfd.Value;
            }
        }
        if (!ANYSELECTED)
        {
            lblmsg1.Text = "Please select crew members.";
            return;
        }
        //----------
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
        crewids = crewids.Substring(1);
        trainingids = trainingids.Substring(1);
        string[] crewlist = crewids.Split(',');
        string[] traininglist = trainingids.Split(',');
        //----------
        //foreach (string cid in crewlist)
        //{
        //    foreach (string tid in traininglist)
        //    {
        //        int crewid = Common.CastAsInt32(cid);
        //        int trainingid = Common.CastAsInt32(tid);
        //        DataTable DtExist = Common.Execute_Procedures_Select_ByQueryCMS("SELECT (SELECT CREWNUMBER FROM CrewPersonalDetails CPD WHERE CPD.CREWID=CT.CREWID) AS CREWNUMBER,(SELECT TRAININGNAME FROM Training T WHERE T.TrainingID=CT.TrainingId) AS TrainingName FROM CrewTrainingRequirement CT WHERE CREWID=" + crewid + " AND TRAININGID=" + trainingid + " AND N_CREWTRAININGSTATUS='O'");
        //        if (DtExist.Rows.Count > 0)
        //        {
        //            lblmsg1.Text = "Sorry ! Crew member ( " + DtExist.Rows[0]["CREWNUMBER"] + " ) already have assigned training ( " + DtExist.Rows[0]["TrainingName"] + " ).";
        //            return;
        //        }
        //    }
        //}
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
                        Budget.getTable("exec dbo.InsertUpdateCrewTrainingRequirement -1," + crewid + "," + trainingid + ",''," + Session["loginid"].ToString() + "," + Session["loginid"].ToString() + ",'" + txt_DueDate.Text + "','O',0,'N'");
                    }
                }
            }
            lblmsg1.Text = "Trainings assigned successfully.";

        }
        catch (Exception ex)
        {
            lblmsg1.Text = ex.Message.ToString();
        }
     
    }
    
    protected void btnClose_Click(object sender, EventArgs e)
    {
        dvFrame.Visible = false;
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
}
