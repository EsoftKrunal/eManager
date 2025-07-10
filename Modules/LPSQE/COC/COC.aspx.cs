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
using System.IO;

public partial class FormReporting_COC : System.Web.UI.Page
{
    string VesselId = "", COCCat = "";
    Authority Auth;
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        //--------------------------------- PAGE ACCESS AUTHORITY ----------------------------------------------------
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 1080);
        if (chpageauth <= 0)
            Response.Redirect("blank.aspx");
        //------------------------------------------------------------------------------------------------------------
        try
        {
            //this.Form.DefaultButton = this.btn_Show.UniqueID.ToString();
            lblMsg.Text = "";
            if (Session["loginid"] == null)
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Default.aspx';", true);
            }
            if (Session["loginid"] != null)
            {
                ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 6);
                OBJ.Invoke();
                Session["Authority"] = OBJ.Authority;
                Auth = OBJ.Authority;
            }
            if (!Page.IsPostBack)
            {
                BindFleet();
                BindVessel();
                btnSearch_Click(sender, e);
            }
        }
        catch (Exception ex) { throw ex; }
    }
    protected void BindFleet()
    {
        ddlFleet.DataSource = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.FLEETMASTER ORDER BY FLEETNAME");
        ddlFleet.DataTextField = "FleetName";
        ddlFleet.DataValueField = "FleetId";
        ddlFleet.DataBind();
        ddlFleet.Items.Insert(0, new ListItem(" < ALL > ", "0"));
    }
    protected void ddlFleet_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        BindVessel();
    }
    protected void BindVessel()
    {
        if (ddlFleet.SelectedIndex > 0)
            ddlVessel.DataSource = Common.Execute_Procedures_Select_ByQuery("SELECT VESSELID,VESSELNAME FROM DBO.VESSEL WHERE VESSELSTATUSID=1  AND FLEETID=" + ddlFleet.SelectedValue + " and VesselId in (Select VesselId from UserVesselRelation with(nolock) where Loginid = " + Convert.ToInt32(Session["loginid"].ToString()) + ")  ORDER BY VESSELNAME");
        else
            ddlVessel.DataSource = Common.Execute_Procedures_Select_ByQuery("SELECT VESSELID,VESSELNAME FROM DBO.VESSEL WHERE VESSELSTATUSID=1 and VesselId in (Select VesselId from UserVesselRelation with(nolock) where Loginid = " + Convert.ToInt32(Session["loginid"].ToString()) + ") ORDER BY VESSELNAME");

        ddlVessel.DataTextField = "VESSELNAME";
        ddlVessel.DataValueField = "VESSELID";
        ddlVessel.DataBind();
        ddlVessel.Items.Insert(0, new ListItem(" < ALL > ", "0"));
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        { 
            string WHERE = "WHERE 1=1 ";

            //if (ddlFleet.SelectedIndex != 0 && ddlVessel.SelectedIndex != 0)
            //{
            //    WHERE = WHERE + " AND VESSELID = " + ddlVessel.SelectedValue + " ";
            //}
            if (ddlFleet.SelectedIndex != 0)
            {
                WHERE = WHERE + " AND FleetId = " + ddlFleet.SelectedValue + " ";
            }
            if (ddlVessel.SelectedIndex != 0)
            {
                WHERE = WHERE + " AND VESSELID = " + ddlVessel.SelectedValue + " ";
            }

            if (txtFromDt.Text.Trim() != "" && txtToDt.Text.Trim() != "")
            {
                if (DateTime.Parse(txtToDt.Text.Trim()) < DateTime.Parse(txtFromDt.Text.Trim()))
                {
                    lblMsg.Text = "To Date cannot be less than From Date.";
                    txtToDt.Focus();
                    return;
                }

                WHERE = WHERE + " AND (co_issuedate > '" + txtFromDt.Text.Trim() + "' AND co_issuedate < '" + txtToDt.Text.Trim() + "') ";
            }
            if (txtFromDt.Text != "" && txtToDt.Text.Trim() == "")
            {
                WHERE = WHERE + " AND co_issuedate > '" + txtFromDt.Text.Trim() + "' ";
            }
            if (txtFromDt.Text == "" && txtToDt.Text.Trim() != "")
            {
                WHERE = WHERE + " AND co_issuedate < '" + txtToDt.Text.Trim() + "' ";
            }

            if (ddl_Status.SelectedIndex != 0)
            {

                WHERE = WHERE + " AND ISNULL(A.CLOSED,0)='" + ddl_Status.SelectedValue.Trim() + "' ";
            }

            string SQL = "SELECT * FROM " +
                         "(  " +
                         "	SELECT CO.CO_Id AS COCID,CO.CO_Number AS COCNO,CO.CO_VesselId AS VESSELID,co_issuedate,  " +
                         "	(SELECT VSL.VESSELNAME FROM DBO.VESSEL VSL WHERE VSL.VESSELID=CO.CO_VesselId) AS VESSELNAME,  " +
                         "  (SELECT VSL.FleetId FROM DBO.VESSEL VSL WHERE VSL.VESSELID=CO.CO_VesselId) AS FleetId, " +
                         "	CO_IssuedFrom as IssuedFrom,CO.CO_DefCode AS DEFCODE,CO.CO_TargetClosedDate AS TGCLOSEDT,CO.CO_TargetClosedDate AS TARGETCLOSEDATE,CO.CO_Responsibility AS RESPONSIBILITY,CO.CO_Filepload AS UPFILENAME,CO.CO_ClosedDate AS COMPLETIONDATE,  " +
                         "	CO.CO_Closed AS CLOSED,(SUBSTRING(CO.CO_Number,0,CHARINDEX('/',CO.CO_Number))) AS AA,(REVERSE(SUBSTRING(REVERSE(CO.CO_Number),0,CHARINDEX('/',REVERSE(CO.CO_Number))))) AS BB,  " +
                         "	RespVessel=CASE WHEN EXISTS(SELECT COC.CO_Responsibility FROM FR_COC COC WHERE COC.CO_VesselId=CO.CO_VesselId AND COC.CO_Responsibility LIKE '%Vessel%' AND ISNULL(COC.CO_Responsibility,'')<>'' AND COC.CO_Id=CO.CO_Id) THEN 'X' ELSE '' END,  " +
                         "	RespOffice=CASE WHEN EXISTS(SELECT COC.CO_Responsibility FROM FR_COC COC WHERE COC.CO_VesselId=CO.CO_VesselId AND COC.CO_Responsibility LIKE '%Office%' AND ISNULL(COC.CO_Responsibility,'')<>'' AND COC.CO_Id=CO.CO_Id) THEN 'X' ELSE '' END,  " +
                         "	CO.CO_IssueDate AS ISSUEDATE,CO.CO_Description,CO.CO_IMDate AS IMMACTDATE,CO.CO_PADate AS PRVACTDATE,CO.CO_FollowUpDate AS FOLLOWUPDATE,CO_Suerveyor,CO_PlaceIssued,CO.CO_Status   " +
                         "	FROM FR_COC CO " +
                         "  WHERE CO.CO_Status='A' AND CO.CO_VesselId IN (SELECT VESSELID FROM DBO.VESSEL V WHERE V.VESSELSTATUSID=1 ) and CO.CO_VesselId in (Select VesselId from UserVesselRelation with(nolock) where Loginid = " + Convert.ToInt32(Session["loginid"].ToString()) + ") " +
                         ") A " + WHERE + " ORDER BY A.AA,A.BB ";

            DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
            if (dt.Rows.Count > 0)
            {
                rptCOC.DataSource = dt;
                rptCOC.DataBind();
            }
            else
            {
                rptCOC.DataSource = null;
                rptCOC.DataBind();
            }

            txt_VesselId.Text = "";
            hid.Text = "";
        }
        catch (Exception ex) { throw ex; }
    }
    
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        int COCId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);

        try
        {
            DataTable DTDEL = COC.GetFCOCDetailsByCOCId(COCId, 0, "", "", "", "", "", "", 0, 0, "DELETE", "", 0, "", "", "", "", "", "", "", "", "", 0, "", "");
            btnSearch_Click(sender, e);
            lblMsg.Text = "Record Deleted Successfully.";
        }
        catch(Exception ex) 
        {
            lblMsg.Text = "Unable to delete. Error : " + ex.Message;
        }

    }
    public string GetPath(string _path)
    {
        string res = "";
        if (_path.StartsWith("U"))
        {
            res = "../" + _path;
        }
        else
        {
            res = "/EMANAGERBLOB/LPSQE/COC_Tracker/" + _path;
        }
        return res;
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ddlFleet.SelectedIndex = 0;
        ddlFleet_OnSelectedIndexChanged(sender, e);
        txtFromDt.Text = "";
        txtToDt.Text = "";
        ddl_Status.SelectedValue = "0";
        btnSearch_Click(sender, e);
    }
}
