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
using System.Text.RegularExpressions;


public partial class Popup_Postponejob : System.Web.UI.Page
{
    public string VesselCode
    {
        set { ViewState["VC"] = value; }
        get { return ViewState["VC"].ToString(); }
    }
    public string CriticalType
    {
        set { ViewState["CriticalType"] = value; }
        get { return ViewState["CriticalType"].ToString(); }
    }
    public int ComponentId
    {
        set { ViewState["CI"] = value; }
        get { return Common.CastAsInt32(ViewState["CI"]); }
    }
    public int JobId
    {
        set { ViewState["JI"] = value; }
        get { return Common.CastAsInt32(ViewState["JI"]); }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        // -------------------------- SESSION CHECK ----------------------------------------------
        ProjectCommon.SessionCheck();
        // -------------------------- SESSION CHECK END ----------------------------------------------
        if (!Page.IsPostBack)
        {
            if (Request.QueryString["CID"] != null && Request.QueryString["JID"] != null)
            {
                VesselCode = Session["CurrentShip"].ToString();
                
                ComponentId = Common.CastAsInt32(Request.QueryString["CID"].ToString());
                JobId = Common.CastAsInt32(Request.QueryString["JID"].ToString());

                ShowCompJobDetails();
                BindRanks();

                if(Convert.ToString(Request.QueryString["CriticalType"])=="C" )
                    lblCriticalMSG.Visible = true;
                else
                    lblCriticalMSG.Visible = false;
            }
        }
    }
    #region USER DEFINED FUNCTIONS

    public void BindRanks()
    {
        DataTable dtRanks = new DataTable();
        string strSQL = "SELECT RankId,RankCode FROM Rank WHERE RANKID IN (1,12) ORDER BY RankLevel";
        dtRanks = Common.Execute_Procedures_Select_ByQuery(strSQL);
        
        ddlPRank.DataSource = dtRanks;
        ddlPRank.DataTextField = "RankCode";
        ddlPRank.DataValueField = "RankId";
        ddlPRank.DataBind();
        ddlPRank.Items.Insert(0, "< SELECT >");
        ddlPRank.Items[0].Value = "0";
    }    
    public void UserDetails()
    {
        //string strSQL = "Select ((select UserName from ShipUserMaster where UserId=VSL_PlanMaster.ModifiedBy) + ' / ' + replace(convert(varchar,VSL_PlanMaster.ModifiedOn,106), ' ', '-')) as UpdatedByName FROM VSL_PlanMaster WHERE VesselCode = '" + VesselCode + "' AND ComponentId = " + ComponentId + " AND CompJobId = " + JobId + " ";
        //DataTable dt = Common.Execute_Procedures_Select_ByQuery(strSQL);
        //if (dt.Rows[0]["UpdatedByName"].ToString() != "")
        //{
        //    //lblPostPonedByOn.Text = dt.Rows[0]["UpdatedByName"].ToString();
        //}
    }    
    private void ShowCompJobDetails()
    {
        string strCompJobDetails = "SELECT (SELECT ShipName FROM Settings WHERE ShipCode = '" + VesselCode + "') AS VesselName, CM.ComponentCode,CM.ComponentName ,JM.JobCode,CJM.DescrSh AS JobName,JIM.IntervalName,VCJM.Interval FROM VSL_VesselComponentJobMaster VCJM " +
                                    "INNER JOIN ComponentMaster CM ON CM.ComponentId = VCJM.ComponentId " +
                                    "INNER JOIN ComponentsJobMapping CJM  ON VCJM.CompJobId = CJM.CompJobId " +
                                    "INNER JOIN JobMaster JM ON JM.JobId = CJM.JobId " +
                                    "INNER JOIN JobIntervalMaster JIM ON VCJM.IntervalId = JIM.IntervalId " +
                                    "WHERE VCJM.VesselCode = '" + VesselCode + "' AND VCJM.ComponentId =" + ComponentId + " AND VCJM.CompJobId=" + JobId + " ";
        DataTable dtCompJobDetails = Common.Execute_Procedures_Select_ByQuery(strCompJobDetails);
        if (dtCompJobDetails.Rows.Count > 0)
        {
            lblPostponeComponent.Text = dtCompJobDetails.Rows[0]["ComponentCode"].ToString() + " - " + dtCompJobDetails.Rows[0]["ComponentName"].ToString();
            lblPostponeJob.Text = dtCompJobDetails.Rows[0]["JobCode"].ToString() + " - " + dtCompJobDetails.Rows[0]["JobName"].ToString();
            lblPostponeInterval.Text = dtCompJobDetails.Rows[0]["Interval"].ToString() + " - " + dtCompJobDetails.Rows[0]["IntervalName"].ToString();
        }
    }

    #endregion

    #region EVENTS
    protected void ddlPpReason_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlPpReason.SelectedIndex == 1)
        {
            lblmsg1.Visible = true;
        }
        else
        {
            lblmsg1.Visible = false;
        }
    }
    protected void btnPostpone_Click(object sender, EventArgs e)
    {
        if (ddlPpReason.SelectedIndex == 0)
        {
            mbPostPone.ShowMessage("Please select postpone reason.", true);
            ddlPpReason.Focus();
            return;
        }
        if (ddlPRank.SelectedIndex == 0)
        {
            mbPostPone.ShowMessage("Please select rank.", true);
            ddlPRank.Focus();
            return;
        }
        if (txtPEmpCode.Text == "")
        {
            mbPostPone.ShowMessage("Please enter employee code.", true);
            txtPEmpCode.Focus();
            return;
        }
        Regex reg = new Regex("^[S/Y]\\d\\d\\d\\d\\d$");
        if (!reg.IsMatch(txtPEmpCode.Text.Trim().ToUpper()))
        {
            mbPostPone.ShowMessage("Please enter valid employee number.", true);
            txtPEmpCode.Focus();
            return;
        }
        if (txtPEmpname.Text == "")
        {
            mbPostPone.ShowMessage("Please enter employee name.", true);
            txtPEmpname.Focus();
            return;
        }
        if (txtPostponeRemarks.Text.Trim()=="")
        {
            mbPostPone.ShowMessage("Remark is manditory.", true);
            txtPostponeRemarks.Focus();
            return;

        }
        if (txtPostponeRemarks.Text.Trim().Length > 500)
        {
            mbPostPone.ShowMessage("Remarks length can not be more than 500 characters.", true);
            txtPostponeRemarks.Focus();
            return;

        }
        if (txtPostponedate.Text.Trim() == "")
        {
            mbPostPone.ShowMessage("Please enter postpone till date.", true);
            txtPostponedate.Focus();
            return;
        }
        else
        {
            DateTime mindate = DateTime.Today;
            DateTime maxdate = mindate.AddDays(91);
            DateTime dt = Convert.ToDateTime(txtPostponedate.Text);
            if (dt < mindate || dt >= maxdate)
            {
                mbPostPone.ShowMessage("Postpone date must be between today and next 90 days.", true);
                txtPostponedate.Focus();
                return;
            }
        }

        Common.Set_Procedures("sp_PostponePlannedJob");
        Common.Set_ParameterLength(10);
        Common.Set_Parameters(
            new MyParameter("@VesselCode", VesselCode),
            new MyParameter("@ComponentId", ComponentId),
            new MyParameter("@CompjobId", JobId),
            new MyParameter("@PostPoneReason", ddlPpReason.SelectedValue),
            new MyParameter("@PostPoneRemarks", txtPostponeRemarks.Text.Trim()),
            new MyParameter("@PlanDate", txtPostponedate.Text.Trim()),
            new MyParameter("@ModifiedBy", Session["loginid"].ToString()),
            new MyParameter("@P_DoneBy", ddlPRank.SelectedValue),
            new MyParameter("@P_DoneBy_Code", txtPEmpCode.Text.Trim()),
            new MyParameter("@P_DoneBy_Name", txtPEmpname.Text.Trim())
            );
        DataSet dsJobPostPone = new DataSet();
        dsJobPostPone.Clear();
        Boolean res;
        res = Common.Execute_Procedures_IUD(dsJobPostPone);
        if (res)
        {
            mbPostPone.ShowMessage("Job postponeded successfully.", false);
            UserDetails();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "tr", "refreshparent();", true);
        }
        else
        {
            mbPostPone.ShowMessage("Unable to postpone Job.Error :" + Common.getLastError(), true);
        }
    }
   
    #endregion
}
