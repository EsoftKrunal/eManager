using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using ShipSoft.CrewManager.BusinessObjects;
using ShipSoft.CrewManager.BusinessLogicLayer;
using ShipSoft.CrewManager.Operational;

public partial class InsuranceRecordManagement_Policies : System.Web.UI.Page
{
    public Authority Auth;
    Button btn = new Button();
    //string SessionDeleimeter = ",";
    protected void Page_Load(object sender, EventArgs e)
    {
        //--------------------------------- PAGE ACCESS AUTHORITY ----------------------------------------------------
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 311);
        if (chpageauth <= 0)
            Response.Redirect("blank.aspx");
        //------------------------------------------------------------------------------------------------------------

        if (Session["loginid"] == null)
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Default.aspx';", true);
        }

        lblmessage.Text = "";
        if (Session["loginid"] != null)
        {
            ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 235);
            OBJ.Invoke();
            Session["Authority"] = OBJ.Authority;
            Auth = OBJ.Authority;
            //btnNewVisit.Visible = Auth.isAdd;
        }
        if (!Page.IsPostBack)
        {
            BindGroups();
            BindOwner();
            BindVessel();
            BindUW();
            chkAllGroups.Checked = true; 
            chkallgp1(null, null);
            if ("" + Request.QueryString["Message"] != "")
            {
                lblmessage.Text = Request.QueryString["Message"];
            }
            if (Session["Search"] != null)
            {
                //LoadSession();
            }
            btnSearch_Click(null, null);
        }
        try
        {
            Alerts.HANDLE_AUTHORITY(1, btn, btn, btn, btn, Auth);
        }
        catch
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Default.aspx';", true);
        }


    }
    #region ----------------- UDF -----------------------

    public void BindGroups()
    {
        try
        {
            DataTable dtGroups = Budget.getTable("SELECT GroupId,GroupName,ShortName FROM IRM_Groups ORDER BY GroupName").Tables[0];
            this.chkGroups.DataSource = dtGroups;
            this.chkGroups.DataValueField = "GroupId";
            this.chkGroups.DataTextField = "ShortName";
            this.chkGroups.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    //public void BindSubGroups()
    //{
    //    string GroupIds = "";
    //    foreach (ListItem lst in chkGroups.Items)
    //    {
    //        if (lst.Selected)
    //        {
    //            GroupIds = GroupIds + lst.Value + ",";
    //        }
    //    }
    //    if (GroupIds.Trim().Length > 1)
    //    {
    //        try
    //        {
    //            GroupIds = GroupIds.Remove(GroupIds.Length - 1);
    //            DataTable dtSubGroups = Budget.getTable("SELECT SubGroupId,SubGroupName FROM IRM_SubGroups WHERE GroupId IN (" + GroupIds.Trim() + ")").Tables[0];
    //            this.chkSubGroups.DataSource = dtSubGroups;
    //            this.chkSubGroups.DataValueField = "SubGroupId";
    //            this.chkSubGroups.DataTextField = "SubGroupName";
    //            this.chkSubGroups.DataBind();
    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //        }
    //    }
    //    else
    //    {
    //        chkSubGroups.Items.Clear();
    //    }

    //}
    protected void BindOwner()
    {
        try
        {
            this.ddlOwner.DataTextField = "OwnerName";
            this.ddlOwner.DataValueField = "OwnerId";
            this.ddlOwner.DataSource = Inspection_Master.getMasterDataforInspection("Owner", "OwnerId", "OwnerName");
            this.ddlOwner.DataBind();
            this.ddlOwner.Items.Insert(0, new ListItem("All", "0"));
            this.ddlOwner.Items[0].Value = "0";
            this.ddlvessels.Items.Insert(0, new ListItem("All", "0"));
            this.ddlvessels.Items[0].Value = "0";
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public void BindVessel()
    {
        try
        {
            DataTable dt;
            if (ddlOwner.SelectedIndex > 0)
            {
                if (chkInactiveVessels.Checked)
                    dt = Common.Execute_Procedures_Select_ByQuery("select VESSELNAME,VESSELID FROM dbo.vessel where ownerid=" + ddlOwner.SelectedValue + " order by vesselname");
                else
                    dt = Common.Execute_Procedures_Select_ByQuery("select VESSELNAME,VESSELID FROM dbo.vessel where ownerid=" + ddlOwner.SelectedValue + " and VesselStatusid<>2 " + " order by vesselname");
            }
            else
            {
                if (chkInactiveVessels.Checked)
                    dt = Common.Execute_Procedures_Select_ByQuery("select VESSELNAME,VESSELID FROM dbo.vessel order by vesselname");
                else
                    dt = Common.Execute_Procedures_Select_ByQuery("select VESSELNAME,VESSELID FROM dbo.vessel where VesselStatusid<>2  order by vesselname");
            }

            ddlvessels.Controls.Clear();
            this.ddlvessels.DataTextField = "VesselName";
            this.ddlvessels.DataValueField = "VesselId";
            this.ddlvessels.DataSource = dt;
            this.ddlvessels.DataBind();
            this.ddlvessels.Items.Insert(0, new ListItem("All", "0"));
            this.ddlvessels.Items[0].Value = "0";
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public void BindUW()
    {
        string strUW = "SELECT UWId,UWName,ShortName FROM IRM_UWMaster ORDER BY UWName ";
        DataTable dtUW = Budget.getTable(strUW).Tables[0];
        ddlAllUW.DataSource = dtUW;
        ddlAllUW.DataTextField = "ShortName";
        ddlAllUW.DataValueField = "UWId";
        ddlAllUW.DataBind();
        ddlAllUW.Items.Insert(0,new ListItem("All","0"));

    }

    

    #endregion -------------------------------------------

    #region ----------------- EVENTS -----------------------

    protected void chkallgp1(object sender, EventArgs e)
    {
        int i = 0;
        for (i = 0; i < chkGroups.Items.Count; i++)
        {
            chkGroups.Items[i].Selected = chkAllGroups.Checked;
        }
    }
    //protected void chakallinsp(object sender, EventArgs e)
    //{
    //    int i = 0;
    //    for (i = 0; i < chkSubGroups.Items.Count; i++)
    //    {
    //        if (chkAllSubGroups.Items[0].Selected == true)
    //            chkSubGroups.Items[i].Selected = true;
    //        else
    //            chkSubGroups.Items[i].Selected = false;
    //    }
    //}
    protected void chkInactiveVessels_CheckedChanged(object sender, EventArgs e)
    {
        BindVessel();
    }
    protected void chkGroups_SelectedIndexChanged(object sender, EventArgs e)
    {
        //BindSubGroups();
    }
    protected void ddlOwner_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindVessel();
        }
        catch (Exception ex)
        {
            lblrecord.Text = "";
            lblmessage.Text = ex.StackTrace.ToString();
        }

    }
    //protected void chkDue_CheckedChanged(object sender, EventArgs e)
    //{
    //    if (chkDue.Checked)
    //    {
    //        if (txtDueDays.Text.Trim() == "")
    //            txtDueDays.Text = "60";
           
    //    }
    //    else
    //        txtDueDays.Text = "";

    //    //txtFromDt.Text = "";
    //    //txtToDt.Text = "";
    //}
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string SearchSQL = "SELECT (SELECT VesselName FROM dbo.Vessel WHERE VesselId = PM.VesselId) AS VesselName,PM.PolicyId,(Case when PM.PaymentByMTM=1 then 'Yes' else '' end )PaymentByMTM  ,UWM.ShortName AS UW, GP.ShortName As GroupName ,PM.PolicyNo,CASE REPLACE(CONVERT(varchar,PM.InceptionDt, 106),' ','-') WHEN '01-Jan-1900' THEN '' ELSE REPLACE(CONVERT(varchar,PM.InceptionDt, 106),' ','-') END AS InceptionDt,CASE REPLACE(CONVERT(varchar,PM.ExpiryDt, 106),' ','-')  WHEN '01-Jan-1900' THEN '' ELSE REPLACE(CONVERT(varchar,PM.ExpiryDt, 106),' ','-') END AS ExpiryDt,PM.InsuredAmount,CASE  WHEN PM.ExpiryDt > getdate() THEN 'Active' ELSE 'Expired' END AS PolicyStatus,(SELECT Count(*) FROM IRM_PolicyDetails Del WHERE Del.PolicyId = PM.PolicyId)As NoInstall,CAST(PM.TotalPremium AS money ) As TotalPremium FROM IRM_PolicyMaster PM " +
                           "LEFT JOIN IRM_Groups GP ON GP.GroupId = PM.GroupId " +
                           "LEFT JOIN IRM_UWMaster UWM ON UWM.UWId = PM.UWId ";
        string WhereCond = "WHERE 1=1 ";
        string GroupIds = "";
        foreach (ListItem lst in chkGroups.Items)
        {
            if (lst.Selected)
            {
                GroupIds = GroupIds + lst.Value + ",";
            }
        }
        if(GroupIds.Trim()!="") 
        GroupIds = GroupIds.Remove(GroupIds.Length - 1);
        else
            GroupIds ="-1";

        WhereCond = WhereCond + "AND  GP.GroupId IN (" + GroupIds.Trim() + ") ";
        if (ddlvessels.SelectedValue == "0")
        {
            string VesselIds = "";
            foreach (ListItem lst in ddlvessels.Items)
            {
                VesselIds = VesselIds + lst.Value + ",";
            }
            VesselIds = VesselIds.Remove(VesselIds.Length - 1);
            WhereCond = WhereCond + "AND PM.VesselId IN (" + VesselIds + ") ";
        }
        else
        {
            WhereCond = WhereCond + "AND PM.VesselId = " + ddlvessels.SelectedValue.Trim() +" ";

        }
        if (ddlAllUW.SelectedValue != "0")
        {
            WhereCond = WhereCond + "AND UWM.UWId = " + ddlAllUW.SelectedValue.Trim() + " ";
        }
        if (ddlRDC.SelectedValue != "0")
        {
            WhereCond = WhereCond + "AND PM.RDC = " + ddlRDC.SelectedValue.Trim() + " ";
        }
        if (txtPolicyNo.Text.Trim() != "")
        {
            WhereCond = WhereCond + "AND PM.PolicyNo LIKE '%" + txtPolicyNo.Text.Trim() + "%' ";
        }
        if (ddlpolicyStatus.SelectedIndex != 0)
        {
            if (ddlpolicyStatus.SelectedValue == "1")
            {
                WhereCond = WhereCond + "AND PM.ExpiryDt > getdate() ";
            }
            else
            {
                WhereCond = WhereCond + "AND PM.ExpiryDt < getdate() ";
            }
        }

        if (txtPremFromDt.Text.Trim() != "" && txtPremToDt.Text.Trim() != "")
        {
            WhereCond = WhereCond + " AND (PM.InceptionDt >= '" + txtPremFromDt.Text.Trim() + "' AND PM.ExpiryDt <= '" + txtPremToDt.Text.Trim() + "') ";
        }
        else
        {
            if (txtPremFromDt.Text != "")
            {
                WhereCond = WhereCond + " AND PM.InceptionDt >= '" + txtPremFromDt.Text.Trim() + "' ";
            }
            if (txtPremToDt.Text != "")
            {
                WhereCond = WhereCond + " AND PM.ExpiryDt <= '" + txtPremToDt.Text.Trim() + "' ";
            }
        }

        if (ddlPayByMTM.SelectedIndex != 0)
        {
            if (ddlPayByMTM.SelectedIndex == 1)
                WhereCond = WhereCond + " AND PM.PaymentByMTM =1 ";
            else
                WhereCond = WhereCond + " AND PM.PaymentByMTM = 0 or PM.PaymentByMTM is null ";
        }
        Session.Add("sWhereCond",WhereCond);
        string strSQL = SearchSQL + WhereCond + " order by PM.ExpiryDt Desc";
        DataTable dtSearchResult = Budget.getTable(strSQL).Tables[0];

        lblrecord.Text = "Total Records Found: " + dtSearchResult.Rows.Count.ToString();

        rptPolicies.DataSource = dtSearchResult;
        rptPolicies.DataBind();
                            
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        chkAllGroups.Checked = false;
        chkallgp1(sender, e);
        ddlOwner.SelectedIndex = 0;
        ddlvessels.SelectedIndex = 0;
        chkInactiveVessels.Checked = false;
        ddlAllUW.SelectedIndex = 0;
        txtPolicyNo.Text = "";
        ddlpolicyStatus.SelectedIndex = 0;
        ddlRDC.SelectedIndex = 0;
    }
    protected void btnNewPolicy_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "tr", "opennewpolicy('A','0');", true);
    }
    protected void btnPrint_OnClick(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "", "window.open('../Reports/PolicyDetails.aspx')", true);
    }

    #endregion -------------------------------------------

}
