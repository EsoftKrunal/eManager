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

public partial class EventManagement_TemplateMaster : System.Web.UI.Page
{
    #region -------- PROPERTIES ------------------
    public int HazardId
    {
        set { ViewState["HazardId"] = value; }
        get { return Common.CastAsInt32(ViewState["HazardId"]); }
    }
    #endregion -----------------------------------

    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        if (!IsPostBack)
        {
            Session["RM"] = "T";
            BindGrid();
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindGrid();
    }
    protected void BindGrid()
    {
        string WhereClause = " ";

        if (txtEventName.Text.Trim() != "")
            WhereClause += " AND EventName LIKE '%" + txtEventName.Text.Trim() + "%' ";

        string SQL = "SELECT * FROM [dbo].[EV_TemplateMaster] M WHERE STATUS in ('A','P') ";

        rptTemplate.DataSource = Common.Execute_Procedures_Select_ByQuery(SQL + WhereClause);
        rptTemplate.DataBind();
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        string TemplateId = ((ImageButton)sender).CommandArgument;
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "edit", "window.open('ViewTemplate.aspx?TemplateId=" + TemplateId.Trim() + "','');", true);
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        string TC = ((ImageButton)sender).CommandArgument;
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "edit", "window.open('EditTemplate.aspx?TC=" + TC + "','');", true);
    }
    protected void btnAddRisk_Click(object sender, EventArgs e)
    {
        rpt_Events.DataSource = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.EV_EVENTMASTER ORDER BY EVENTNAME");
        rpt_Events.DataBind();
        dv_RiskTopics.Visible = true;
       
    }
    protected void btnSelect_Click(object sender, EventArgs e)
    {
        dv_RiskTopics.Visible = false;
        int Key = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "edit", "window.open('EditTemplate.aspx?EventId=" + Key + "','');", true);
    }
    protected void btnCancelNew_Click(object sender, EventArgs e)
    {
        dv_RiskTopics.Visible = false;
    }
    protected void btnHistory_Click(object sender, EventArgs e)
    {
        string TemplateCode = ((ImageButton)sender).CommandArgument;

        lblHistoryTital.Text = TemplateCode + " [ Change History ] ";

        string SQL = "SELECT * FROM [dbo].[EV_TemplateMaster] WHERE TemplateCode='" + TemplateCode + "' AND Status='H' ORDER BY ApprovedOn DESC ";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);

        rptHistory.DataSource = dt;
        rptHistory.DataBind();

        dv_History.Visible = true;
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        dv_History.Visible = false;
    } 
    protected void btnViewHistoryDetails_Click(object sender, EventArgs e)
    {
        string TemplateId = ((ImageButton)sender).CommandArgument;
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "History", "window.open('ViewTemplate.aspx?TemplateId=" + TemplateId.Trim() + "','');", true);
    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        BindGrid();
    }
    protected void btnLinkVessel_Click(object sender, EventArgs e)
    {
        int TemplateId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        hfdTemplateId.Value = TemplateId.ToString();

        string SQL = "SELECT V.*, CASE WHEN VL.VesselId=V.VesselId THEN 1 ELSE 0 END AS Checked  FROM dbo.Vessel V " +
                     "LEFT JOIN [dbo].[EV_Template_Vessel_Linking] VL ON VL.VesselId=V.VesselId AND VL.TemplateId =" + TemplateId + " " +
                     "WHERE ( V.VesselStatusId =1 )  ORDER BY VesselName ";

        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);

        rptVessel.DataSource = dt;
        rptVessel.DataBind();

        dv_LinkVessel.Visible = true;
    }
    protected void btnSaveVesselLinking_Click(object sender, EventArgs e)
    {
        bool selected = false;
        foreach (RepeaterItem item in rptVessel.Items)
        {
            CheckBox chkSelect = (CheckBox)item.FindControl("chkSelect");
            if (chkSelect.Checked)
            {
                selected = true;
                break;
            }
        }

        if (!selected)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "err", "alert('Please select a vessel.');", true);
            return;
        }

        try
        {

            Common.Execute_Procedures_Select_ByQuery("DELETE FROM [dbo].[EV_Template_Vessel_Linking] WHERE TemplateId = " + hfdTemplateId.Value);

            foreach (RepeaterItem item in rptVessel.Items)
            {
                CheckBox chkSelect = (CheckBox)item.FindControl("chkSelect");
                if (chkSelect.Checked)
                {
                    int VesselId = Common.CastAsInt32(chkSelect.Attributes["VesselId"]);
                    Common.Execute_Procedures_Select_ByQuery("INSERT INTO [dbo].[EV_Template_Vessel_Linking]([TemplateId],[VesselId])VALUES(" + hfdTemplateId.Value.Trim() + ", " + VesselId + ")");
                }
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "succ", "alert('Saved successfully.');", true);

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "err", "alert('Unable to save. Error : " + ex.Message + "');", true);
        }
    }
    protected void btnCloseVL_Click(object sender, EventArgs e)
    {
        hfdTemplateId.Value = "";
        dv_LinkVessel.Visible = false;
    }

}