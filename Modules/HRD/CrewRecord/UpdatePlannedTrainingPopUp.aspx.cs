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
using ShipSoft.CrewManager.BusinessLogicLayer;
using ShipSoft.CrewManager.BusinessObjects;
using ShipSoft.CrewManager.Operational;
using System.Text;

public partial class UpdatePlannedTrainingPopUp : System.Web.UI.Page
{
    public int PkId
    {
        get { return Common.CastAsInt32(ViewState["PkId"]); }
        set { ViewState["PkId"] = value; }
    }
    public int Tid
    {
        get { return Common.CastAsInt32(ViewState["Tid"]); }
        set { ViewState["Tid"] = value; }
    }
    public string DD
    {
        get { return ViewState["DD"].ToString(); }
        set { ViewState["DD"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        if (!Page.IsPostBack)
        {
            PkId = Common.CastAsInt32(Request.QueryString["PkId"]);
            Tid = Common.CastAsInt32(Request.QueryString["Tid"]);
            DD = Request.QueryString["DD"];
            BindTrainingInstitite();
            litSummary.Text = getTrainingSummary(true, PkId.ToString());
        }
        dvUP.Visible = Request.QueryString["mode"].ToString().Trim() == "P"; 
    }
    protected void BindTrainingInstitite()
    {
        DropDownList1.DataSource = Budget.getTable("Select InstituteId,InstituteName from TrainingInstitute");
        DropDownList1.DataTextField = "InstituteName";
        DropDownList1.DataValueField = "InstituteId";
        DropDownList1.DataBind();
        DropDownList1.Items.Insert(0, new System.Web.UI.WebControls.ListItem("< Select >", ""));
    }
    protected string getTrainingSummary(bool Update, string PK)
    {
        StringBuilder sb = new StringBuilder();
        string str = "select TRAININGTYPENAME,TRAININGNAME,N_DUEDATE,PLANNEDFOR,UL.FIRSTNAME+ ' '+ UL.LASTNAME AS PLANNEDBY,PLANNEDON,TI.InstituteName AS PlanInstitute,FROMDATE,TODATE,TI1.InstituteName AS DoneInstitute,Remark  " +
            " ,dbo.fn_getSimilerTrainingsName(T.TRAININGID) SimilarTraining " +              
            " from CREWTRAININGREQUIREMENT CTR  " +
                   "INNER JOIN TRAINING T ON CTR.TRAININGID=T.TRAININGID " +
                   "INNER JOIN TRAININGTYPE TT ON TT.TRAININGTYPEID=T.TYPEOFTRAINING " +
                   "LEFT JOIN TRAININGINSTITUTE TI ON TI.InstituteId=CTR.PlannedInstitute " +
                   "LEFT JOIN TRAININGINSTITUTE TI1 ON TI1.InstituteId=CTR.TrainingPlanningId " +
                   "LEFT JOIN USERLOGIN UL ON UL.LOGINID=CTR.PLANNEDBY where TRAININGREQUIREMENTID=" + PK;
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(str);
        if (dt.Rows.Count > 0)
        {
            sb.Append("<table cellpadding='2' cellspacing='0' border='0' width='100%' style='border-collapse:collapse;text-align:right'>");
            sb.Append("<col align='right' width='130px;' />");
            sb.Append("<col align='left' width='130px;' />");
            sb.Append("<col align='right' width='130px;'/>");
            sb.Append("<col align='left' width='130px;' />");
            sb.Append("<tr>");
            sb.Append("<td colspan='4' style='text-align:center'><b>" + dt.Rows[0]["SimilarTraining"].ToString().Replace(",", "<span style='color:red;'> OR </span>") + "</b></td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td>Due On : </td>");
            sb.Append("<td style='text-align:left'>" + Common.ToDateString(dt.Rows[0]["N_DUEDATE"]) + "</td>");
            sb.Append("<td>Planned For : </td>");
            sb.Append("<td style='text-align:left'>" + Common.ToDateString(dt.Rows[0]["PLANNEDFOR"]) + "</td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td>Planned Institute : </td>");
            sb.Append("<td style='text-align:left'>" + dt.Rows[0]["PlanInstitute"].ToString() + "</td>");
            sb.Append("<td>Planned By/On : </td>");
            sb.Append("<td style='text-align:left'>" + dt.Rows[0]["PLANNEDBY"].ToString() + " / " + dt.Rows[0]["PLANNEDON"].ToString() + "</td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td>From Date : </td>");
            sb.Append("<td style='text-align:left'>" + Common.ToDateString(dt.Rows[0]["FROMDATE"]) + "</td>");
            sb.Append("<td>To Date : </td>");
            sb.Append("<td style='text-align:left'>" + Common.ToDateString(dt.Rows[0]["TODATE"]) + "</td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td>Institute : </td>");
            sb.Append("<td colspan='3' style='text-align:left'>" + dt.Rows[0]["DoneInstitute"].ToString() + "</td>");
            sb.Append("</tr>");
            sb.Append("</table>");
        }
        return sb.ToString();
    }
    protected void btn_UpdateTraining_Click(object sender, EventArgs e)
    {
        try{
        string sql = "EXEC DBO.Update_Training " + PkId.ToString() + "," + Session["CrewId"].ToString() + "," + Tid.ToString() + ",'" + DD + "','" + txt_FromDate.Text + "','" + txt_ToDate.Text + "'," + DropDownList1.SelectedValue + "," + Session["LoginId"].ToString();
        Common.Execute_Procedures_Select_ByQueryCMS(sql);
        txt_FromDate.Text = "";
        txt_ToDate.Text = "";
        DropDownList1.SelectedIndex = 0;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "ta", "alert('Updated Successfully.');window.opener.__doPostBack('Menu1','4');window.close();", true); 
        }
        catch
        {
            
        }
        
    }
}
