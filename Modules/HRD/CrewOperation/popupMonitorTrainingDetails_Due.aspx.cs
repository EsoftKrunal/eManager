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

public partial class CrewOperation_popupMonitorTrainingDetails_Due : System.Web.UI.Page
{
    public int Office
    {
        get
        {return Common.CastAsInt32(ViewState["office"]);}
        set
        {ViewState["office"] = value;}
    }
    public string Mode
    {
        get
        { return Convert.ToString(ViewState["Mode"]); }
        set
        { ViewState["Mode"] = value; }
    }
    public string Mode1
    {
        get
        { return Convert.ToString(ViewState["Mode1"]); }
        set
        { ViewState["Mode1"] = value; }
    }
    public string FD
    {
        get
        { return Convert.ToString(ViewState["FD"]); }
        set
        { ViewState["FD"] = value; }
    }
    public string TD
    {
        get
        { return Convert.ToString(ViewState["TD"]); }
        set
        { ViewState["TD"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        if (!IsPostBack)
        {
            Office = Common.CastAsInt32(Request.QueryString["OfficeId"]);
            Mode = Request.QueryString["Mode"].Substring(0, 1);
            Mode1 = Request.QueryString["Mode"].Substring(1);

            FD = Request.QueryString["FD"].ToString();
            TD = Request.QueryString["TD"].ToString();
            TD = Convert.ToDateTime(TD).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");

            ddl_TrainingReq_Training.DataSource = Budget.getTable("Select InstituteId,InstituteName from TrainingInstitute where InstituteName<>'ONBOARD'");
            ddl_TrainingReq_Training.DataTextField = "InstituteName";
            ddl_TrainingReq_Training.DataValueField = "InstituteId";
            ddl_TrainingReq_Training.DataBind();
            ddl_TrainingReq_Training.Items.Insert(0, new ListItem("< Select >", ""));
            ShowData();
        }
    }

    public void ShowData()
    {
        if (Office > 0)
        {
            string officename = "";
            string VesselSQL = "SELECT RecruitingOfficeName FROM DBO.RecruitingOffice WHERE RecruitingOfficeId = " + Office;
            DataTable dtVessel = Common.Execute_Procedures_Select_ByQueryCMS(VesselSQL);
            if (dtVessel.Rows.Count > 0)
            {
                officename = dtVessel.Rows[0]["RecruitingOfficeName"].ToString();
            }
            switch (Mode1)
            {
                case "Due":
                    switch (Mode)
                    {
                        case "C":
                            {
                                lblPageTitle.Text = "Trainings Due by Crew Members ( Office : " + officename + " )";

                                string SQL = "SELECT DISTINCT CREWID,CREWNumber,CREWNAME,rankname FROM DBO.VW_DUE_TRAININGS_ONSHORE WHERE RecruitmentOfficeId=" + Office;
                                DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(SQL);
                                StringBuilder sb = new StringBuilder();

                                foreach (DataRow dr in dt.Rows)
                                {
                                    int odCOUNT = 0;
                                    SQL = "SELECT count(*) from DBO.VW_DUE_TRAININGS_ONSHORE WHERE RecruitmentOfficeId=" + Office + " AND N_DUEDATE<GETDATE() AND CREWID=" + dr["CREWID"].ToString();
                                    DataTable dt1 = Common.Execute_Procedures_Select_ByQueryCMS(SQL);
                                    odCOUNT = Common.CastAsInt32(dt1.Rows[0][0]);

                                    SQL = "SELECT * from DBO.VW_DUE_TRAININGS_ONSHORE WHERE RecruitmentOfficeId=" + Office + " AND CREWID=" + dr["CREWID"].ToString();
                                    dt1 = Common.Execute_Procedures_Select_ByQueryCMS(SQL);
                                    if (dt1.Rows.Count <= 0)
                                    {
                                        continue;
                                    }

                                    sb.Append("<div class='MainRow' style='background-color:#e2e2e2'>");
                                    sb.Append("<div>");
                                    sb.Append("<span style = 'display:inline-block;width:500px;'>");
                                    sb.Append("<span class='spanCrewNumber'>" + dr["CREWNumber"].ToString() + "</span> | ");
                                    sb.Append("<span class='spanCrewName'>" + dr["CREWNAME"].ToString() + "</span> | ");
                                    sb.Append("<span class='spanRank'>" + dr["rankname"].ToString() + "</span> ");
                                    sb.Append("</span>");
                                    sb.Append("<input type='button' onclick='post(this)' crewid='" + dr["crewid"].ToString() + "' trainingid='0' rankid='0' value='Update Training' style='margin-left:50px;'/>");
                                    sb.Append("&nbsp;&nbsp;&nbsp;<div style='display:inline-table;border:solid 1px red; width:18px;height:18px;text-align:center;font-size:18px; cursor:pointer; background-color:white;' onclick='OpenClose(this)' target='dvchild_" + dr["crewid"].ToString() + "'>+</div>");
                                    sb.Append("<i style='color:red; font-size:11px;float:right;'>( " + odCOUNT + " OverDue Trainings. )</i></div>");
                                    sb.Append("</div>");

                                    sb.Append("<div class='ChildRow' id='dvchild_" + dr["crewid"].ToString() + "' style='display:none;'>");
                                    sb.Append("<table cellspacing='0' cellpadding='0' width='100%' >");
                                    sb.Append("<thead>");
                                    sb.Append("<tr>");
                                    sb.Append("<td style='width:200px;'>Training Type</td>");
                                    sb.Append("<td style=''>Training Name</td>");
                                    sb.Append("<td style='width:150px;'>Source </td>");
                                    sb.Append("<td style='width:100px;text-align:center;'>Last Done Date</td>");
                                    sb.Append("<td style='width:100px;text-align:center;'>Due Date</td>");
                                    sb.Append("</tr>");
                                    sb.Append("</thead>");
                                    sb.Append("<tbody>");
                                    foreach (DataRow dr1 in dt1.Rows)
                                    {
                                        sb.Append("<tr>");
                                        sb.Append("<td>" + dr1["TrainingTypeNAME"].ToString() + "</td>");
                                        sb.Append("<td>" + dr1["TrainingName"].ToString() + "</td>");
                                        sb.Append("<td>" + dr1["AssignSource"].ToString() + "</td>");
                                        
                                        sb.Append("<td style='text-align:center;'>" + Common.ToDateString(dr1["LASTDONECOMPUTED"]) + "</td>");
                                        DateTime dtt = Convert.ToDateTime(dr1["N_DueDate"]);
                                        if (dtt < DateTime.Today)
                                            sb.Append("<td style='color:#FF0000; text-align:center;'>" + Common.ToDateString(dr1["N_DueDate"]) + "</td>");
                                        else
                                            sb.Append("<td style='text-align:center;'>" + Common.ToDateString(dr1["N_DueDate"]) + "</td>");
                                        sb.Append("</tr>");
                                    }
                                    sb.Append("</tbody>");
                                    sb.Append("</table>");
                                    sb.Append("</div>");

                                }

                                litTraining.Text = sb.ToString();
                            }
                            break;
                        case "R":
                            {
                                lblPageTitle.Text = "Trainings Due by Rank ( Office : " + officename + " )";

                                string SQL = "SELECT DISTINCT CURRENTRANKID,RANKNAME FROM DBO.VW_DUE_TRAININGS_ONSHORE WHERE RecruitmentOfficeId=" + Office;
                                DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(SQL);
                                StringBuilder sb = new StringBuilder();

                                foreach (DataRow dr in dt.Rows)
                                {
                                    int odCOUNT = 0;
                                    SQL = "SELECT count(*) from DBO.VW_DUE_TRAININGS_ONSHORE WHERE RecruitmentOfficeId=" + Office + " AND N_DUEDATE<GETDATE() AND CURRENTRANKID=" + dr["CURRENTRANKID"].ToString();
                                    DataTable dt1 = Common.Execute_Procedures_Select_ByQueryCMS(SQL);
                                    odCOUNT = Common.CastAsInt32(dt1.Rows[0][0]);

                                    SQL = "SELECT * from DBO.VW_DUE_TRAININGS_ONSHORE WHERE RecruitmentOfficeId=" + Office + " AND CURRENTRANKID=" + dr["CURRENTRANKID"].ToString();
                                    dt1 = Common.Execute_Procedures_Select_ByQueryCMS(SQL);
                                    if (dt1.Rows.Count <= 0)
                                    {
                                        continue;
                                    }

                                    sb.Append("<div class='MainRow' style='background-color:#e2e2e2'>");
                                    sb.Append("<div>");
                                    sb.Append("<span class='spanRank' style='display:inline-block;width:500px;'>" + dr["rankname"].ToString() + "</span>");
                                    sb.Append("<input type='button' onclick='post(this)' crewid='0' trainingid='0' rankid='" + dr["currentrankid"].ToString() + "' value='Update Training' style='margin-left:50px;'/>");
                                    sb.Append("&nbsp;&nbsp;&nbsp;<div style='display:inline-table;border:solid 1px red; width:18px;height:18px;text-align:center;font-size:18px; cursor:pointer; background-color:white;' onclick='OpenClose(this)' target='dvchild_" + dr["currentrankid"].ToString() + "'>+</div>");
                                    sb.Append("<i style='color:red; font-size:11px;float:right'>( " + odCOUNT + " OverDue Trainings. )</i></div>");
                                    sb.Append("</div>");

                                    sb.Append("<div class='ChildRow' id='dvchild_" + dr["currentrankid"].ToString() + "' style='display:none;'>");
                                    sb.Append("<table cellspacing='0' cellpadding='0' width='100%' >");
                                    sb.Append("<thead>");
                                    sb.Append("<tr>");
                                    sb.Append("<td style='width:200px;'>Training Type</td>");
                                    sb.Append("<td style=''>Training Name</td>");
                                    sb.Append("<td style='width:100px;'>Crew Number</td>");
                                    sb.Append("<td style='width:300px;'>Crew Name</td>");
                                    sb.Append("<td style='width:100px;'>Source </td>");
                                    sb.Append("<td style='width:100px;text-align:center;'>Last Done Date</td>");
                                    sb.Append("<td style='width:100px;text-align:center;'>Due Date</td>");
                                    sb.Append("</tr>");
                                    sb.Append("</thead>");
                                    sb.Append("<tbody>");
                                    foreach (DataRow dr1 in dt1.Rows)
                                    {
                                        sb.Append("<tr>");
                                        sb.Append("<td>" + dr1["TrainingTypeNAME"].ToString() + "</td>");
                                        sb.Append("<td>" + dr1["TrainingName"].ToString() + "</td>");
                                        sb.Append("<td>" + dr1["CREWNumber"].ToString() + "</td>");
                                        sb.Append("<td>" + dr1["CREWNAME"].ToString() + "</td>");
                                        sb.Append("<td>" + dr1["AssignSource"].ToString() + "</td>");
                                        sb.Append("<td style='text-align:center;'>" + Common.ToDateString(dr1["LASTDONECOMPUTED"]) + "</td>");
					if(!Convert.IsDBNull(dr1["N_DueDate"]))
{
                                        DateTime dtt = Convert.ToDateTime(dr1["N_DueDate"]);
                                        if (dtt < DateTime.Today)
                                            sb.Append("<td style='color:#FF0000; text-align:center;'>" + Common.ToDateString(dr1["N_DueDate"]) + "</td>");
                                        else
                                            sb.Append("<td style='text-align:center;'>" + Common.ToDateString(dr1["N_DueDate"]) + "</td>");
}
else
 sb.Append("<td style='text-align:center;'>" + Common.ToDateString(dr1["N_DueDate"]) + "</td>");
                                        sb.Append("</tr>");
                                    }
                                    sb.Append("</tbody>");
                                    sb.Append("</table>");
                                    sb.Append("</div>");

                                }

                                litTraining.Text = sb.ToString();
                            }
                            break;
                        case "T":
                            {
                                lblPageTitle.Text = "Trainings Due by Training ( Office : " + officename  + " )";

                                string SQL = "SELECT DISTINCT TRAININGtypeID,TRAININGID,TrainingTypeNAME,ltrim(rtrim(TrainingName)) as TrainingName FROM DBO.VW_DUE_TRAININGS_ONSHORE WHERE RecruitmentOfficeId=" + Office + " order by ltrim(rtrim(TrainingName))";
                                DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(SQL);
                                StringBuilder sb = new StringBuilder();

                                foreach (DataRow dr in dt.Rows)
                                {
                                    int odCOUNT = 0;
                                    SQL = "SELECT count(*) from DBO.VW_DUE_TRAININGS_ONSHORE WHERE RecruitmentOfficeId=" + Office + " AND N_DUEDATE<GETDATE() AND TRAININGID=" + dr["TRAININGID"].ToString() + " AND TRAININGtypeID = " + dr["TRAININGtypeID"].ToString();
                                    DataTable dt1 = Common.Execute_Procedures_Select_ByQueryCMS(SQL);
                                    odCOUNT = Common.CastAsInt32(dt1.Rows[0][0]);


                                    SQL = "SELECT * from DBO.VW_DUE_TRAININGS_ONSHORE WHERE RecruitmentOfficeId=" + Office + " AND TRAININGID=" + dr["TRAININGID"].ToString() + " AND TRAININGtypeID = " + dr["TRAININGtypeID"].ToString();
                                    dt1 = Common.Execute_Procedures_Select_ByQueryCMS(SQL);
                                    if (dt1.Rows.Count <= 0)
                                    {
                                        continue;
                                    }

                                    sb.Append("<div class='MainRow' style='background-color:#e2e2e2'>");
                                    sb.Append("<div>");
                                    sb.Append("<span style = 'display:inline-block;width:500px;'>");
                                    //sb.Append("<span class='spanCrewNumber'>" + dr["TrainingTypeNAME"].ToString() + "</span> | ");
                                    sb.Append("<span class='spanCrewName'>" + dr["TrainingName"].ToString() + "</span> | ");
                                    sb.Append("</span>");
                                    sb.Append("<input type='button' onclick='post(this)' crewid='0' trainingid='" + dr["trainingid"].ToString() + "' rankid='0' value='Update Training' style='margin-left:50px;'/>");
                                    sb.Append("&nbsp;&nbsp;&nbsp;<div style='display:inline-table;border:solid 1px red; width:18px;height:18px;text-align:center;font-size:18px; cursor:pointer; background-color:white;' onclick='OpenClose(this)' target='dvchild_" + dr["trainingid"].ToString() + "'>+</div>");
                                    sb.Append("<i style='color:red; font-size:11px;float:right'>( " + odCOUNT + " OverDue Trainings. )</i></div>");
                                    sb.Append("</div>");

                                    sb.Append("<div class='ChildRow' id='dvchild_" + dr["trainingid"].ToString() + "' style='display:none;'>");
                                    sb.Append("<table cellspacing='0' cellpadding='0' width='100%' >");
                                    sb.Append("<thead>");
                                    sb.Append("<tr>");
                                    sb.Append("<td style='width:200px;'>Crew #</td>");
                                    sb.Append("<td style=''>Crew Name</td>");
                                    sb.Append("<td style='width:150px;'>Rank Name</td>");
                                    sb.Append("<td style='width:100px;'>Source </td>");
                                    sb.Append("<td style='width:100px;text-align:center;'>Last Done Date</td>");
                                    sb.Append("<td style='width:100px;text-align:center;'>Due Date</td>");
                                    sb.Append("</tr>");
                                    sb.Append("</thead>");
                                    sb.Append("<tbody>");
                                    foreach (DataRow dr1 in dt1.Rows)
                                    {
                                        sb.Append("<tr>");
                                        sb.Append("<td>" + dr1["CREWNumber"].ToString() + "</td>");
                                        sb.Append("<td>" + dr1["CREWNAME"].ToString() + "</td>");
                                        sb.Append("<td>" + dr1["rankname"].ToString() + "</td>");
                                        sb.Append("<td>" + dr1["AssignSource"].ToString() + "</td>");
                                        sb.Append("<td style='text-align:center;'>" + Common.ToDateString(dr1["LASTDONECOMPUTED"]) + "</td>");
                                        DateTime dtt = Convert.ToDateTime(dr1["N_DueDate"]);
                                        if (dtt < DateTime.Today)
                                            sb.Append("<td style='color:#FF0000; text-align:center;width:100px;'>" + Common.ToDateString(dr1["N_DueDate"]) + "</td>");
                                        else
                                            sb.Append("<td style='text-align:center;'>" + Common.ToDateString(dr1["N_DueDate"]) + "</td>");
                                        sb.Append("</tr>");
                                    }
                                    sb.Append("</tbody>");
                                    sb.Append("</table>");
                                    sb.Append("</div>");

                                }

                                litTraining.Text = sb.ToString();
                            }
                            break;
                    }

                    break;
                default:
                    break;
            }
        }
    }

    protected void b1_Click(object sender, EventArgs e)
    {
        string sql = "SELECT * from DBO.VW_DUE_TRAININGS_ONSHORE where RecruitmentOfficeId=" + Office ;
        string whereclause = "";

        int crewid = Common.CastAsInt32(hfdcrewid.Text);
        int rankid = Common.CastAsInt32(hfdrankid.Text);
        int trainingid = Common.CastAsInt32(hfdtrainingid.Text);

        if (crewid > 0)
            whereclause += " and crewid=" + crewid;
        if (rankid > 0)
            whereclause += " and currentrankid=" + rankid;
        if (trainingid > 0)
            whereclause += " and trainingid=" + trainingid;

        rptdata.DataSource = Common.Execute_Procedures_Select_ByQueryCMS(sql + whereclause);
        rptdata.DataBind();
    }

    protected void btn_Save_Training_Click(object sender,EventArgs e)
    {
        foreach(RepeaterItem ri in rptdata.Items)
        {
            CheckBox ch = (CheckBox)ri.FindControl("chkSel");
            if (ch.Checked)
            {
                HiddenField hd = (HiddenField)ri.FindControl("hfdPK");
                Common.Execute_Procedures_Select_ByQueryCMS("UPDATE CrewTrainingRequirement SET fromdate='" + txt_FromDate.Text + "',todate='" + txt_ToDate.Text + "',trainingplanningid=" + ddl_TrainingReq_Training.SelectedValue + ",Attended='Y',N_CrewTrainingStatus='C',N_CrewVerified='Y',N_VerifiedBy=0,N_VerifiedOn=getdate(),PlannedFor = '" + txt_FromDate.Text + "', PlannedInstitute = " + ddl_TrainingReq_Training.SelectedValue + ", PlannedBy = " + Common.CastAsInt32(Session["loginid"]) + ",PlannedOn='" + DateTime.Now.ToString("dd-MMM-yyyy") + "' WHERE TrainingRequirementID = " + hd.Value);
            }

        }
        ScriptManager.RegisterStartupScript(this,this.GetType(),"fsaf","alert('Training completed successfully.')",true);
        b1_Click(sender, e);
    }
}