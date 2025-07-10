using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Emtm_Vesselassignment : System.Web.UI.Page
{
    public int UserId
    {
        get { return Common.CastAsInt32(ViewState["UserId"]); }
        set { ViewState["UserId"] = value; }
    }
    public int VesselId
    {
        get { return Common.CastAsInt32(ViewState["VesselId"]); }
        set { ViewState["VesselId"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        VesselId = Common.CastAsInt32(Page.Request.QueryString["VesselID"]);
        //-----------------------------
        if (!IsPostBack)
        {

	    string sql = " select vesselname from vessel where vesselid="+ VesselId;
	    DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
	    lblvesselname.Text=" [ " + dt.Rows[0][0].ToString() + " ]";
            BindDays();
            //BindUsers();
        }
    }
   
    // Functions----------------------------------
    protected void BindDays()
    {
        string sql = " select distinct EFFDATE from vesselAssignment_1 vs "+
                     "   inner join VesselPositions vp on vp.VPId = vs.VPID " +
                     "   inner join UserLogin l on l.LoginId = vs.UserID " +
                     "  Where VesselId="+ VesselId +
                     "   order by EffDate desc ";

        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        rptDays.DataSource = dt;
        rptDays.DataBind();
    }

    public DataTable getChanges(object effdate)
    {
        string sql = " select vs.*,vp.PositionName,l.FirstName+' '+l.LastName as UserName from vesselAssignment_1 vs "+
                     "   inner join VesselPositions vp on vp.VPId = vs.VPID " +
                     "   inner join UserLogin l on l.LoginId = vs.UserID " +
                     "  Where VesselId="+ VesselId + " And EffDate='" + effdate.ToString() + "'" +
                     "   order by EffDate desc ";

        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        return dt;
    }

    protected void BindUsers(string Date)
    {
        string sql = " SELECT va.TableID,va.UserID,v.VPID, v.PositionName,l.FirstName+' '+l.MiddleName+' '+l.FamilyName as UserName FROM VesselPositions v " +
                     "  left join vesselAssignment_1 va on va.VPID = v.VPId and VesselID ="+VesselId +" and EffDate<= '"+DateTime.Today.ToString("dd-MMM-yyyy")+ "' and(LastDate is null or LastDate >='" + DateTime.Today.ToString("dd-MMM-yyyy") + "') " +
                     "  left join Hr_PersonalDetails l on l.UserID= va.UserID " +
                     "  order by v.PositionName ";

        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        rptusers.DataSource = dt;
        rptusers.DataBind();
        
    }
    protected void BindAvailabelUsers(string Date)
    {
        string sql = " select vs.*,vp.PositionName,l.FirstName+' '+l.MiddleName+' '+l.FamilyName as UserName from vesselAssignment_1 vs " +
                     "  inner join VesselPositions vp on vp.VPId = vs.VPID " +
                     "  inner join Hr_PersonalDetails l on l.UserID = vs.UserID " +
                     "  Where VesselID = " + VesselId + " and EffDate<= '" + Date + "' and(LastDate is null or LastDate >= '" + Date + "') " +
                     "  order by EffDate desc ";

        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        rptAvailableUsers.DataSource = dt;
        rptAvailableUsers.DataBind();
    }
    protected DataTable ddlUser(object vpid)
    {
        string sql = " select UserID,FirstName+' '+MiddleName+' '+FamilyName as UserName from DBO.Hr_PersonalDetails  "+
                        " where DRC is null and UserId is not null "+
                        " and Position in (select positionid from Position where VesselPositions="+vpid + ") " +
                        " order by UserName ";

        DataTable dt= Common.Execute_Procedures_Select_ByQueryCMS(sql);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = " Select ";
        dt.Rows.InsertAt(dr, 0);
        return dt;

    }
    protected void OnCheckedChanged_chkSelectForUser(object sender, EventArgs e)
    {
        CheckBox chk = (CheckBox)sender;
        DropDownList ddl = (DropDownList)chk.Parent.FindControl("ddlUsers");
        if (chk.Checked)
            ddl.Visible = true;
        else
        {
            ddl.Visible = false;
            ddl.SelectedIndex = 0;
        }
    }

    // Events----------------------------------
    protected void btnTempPost_OnClick(object sender, EventArgs e)
    {
        string ID = hfdVesselAssignmentID.Value;
        lblVesselAssignmentListHeading.Text = "Vessel Assignments as on " + hfdAssignmentDate.Value;
        BindAvailabelUsers(hfdAssignmentDate.Value);
        divAvailableUser.Visible = true;
    }
    protected void btnModifyAssignement_OnClick(object sender, EventArgs e)
    {
        divModifyAssignment.Visible = true;
        BindUsers(hfdAssignmentDate.Value);        
    }
    protected void btnClosePopup_OnClick(object sender, EventArgs e)
    {
        divModifyAssignment.Visible = false;
    }

    protected void btnSave_OnClick(object sender, EventArgs e)
    {
        bool anyChecked = false;
        bool brk = false;
        foreach (RepeaterItem itm in rptusers.Items)
        {
            CheckBox chk = (CheckBox)itm.FindControl("chkSelectForUser");
            DropDownList ddl = (DropDownList)itm.FindControl("ddlUsers");
            Label positionname = (Label)itm.FindControl("lblPositionName");
            HiddenField hfdCurrUserID = (HiddenField)itm.FindControl("hfdCurrUserID");
            


            if (chk.Checked)
            {
                anyChecked = true;
                if (ddl.SelectedIndex == 0)
                {
                    brk = true;
                    lblMsg.Text = "Please select any user for " + positionname.Text + ".";
                    return;
                }
                else
                {
                    if (hfdCurrUserID.Value == ddl.SelectedValue)
                    {
                        lblMsg.Text = "Please select another user for " + positionname.Text + ".User is already assigned."; ;
                        return;
                    }
                }
            }
        }

        if (!anyChecked)
        {
            lblMsg.Text = "Please select any user to change.";
            return;
        }
        if (txtEffectiveDate.Text.Trim() == "")
        {
            lblMsg.Text = "Please enter effective date.";
            return;
        }
        else
        {
            try
            {
                Convert.ToDateTime(txtEffectiveDate.Text.Trim());
                string sql = " select 1 from vesselAssignment_1 where VesselID="+VesselId+" and EffDate>'"+ txtEffectiveDate.Text.Trim() + "' ";
                DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
                if (dt.Rows.Count > 0)
                {
                    lblMsg.Text = "Effective date should be more than last effective date.";
                    return;
                }
            }
            catch(Exception ex)
            {
                lblMsg.Text = "Invalid effective date.";
                return;
            }

        }

        foreach (RepeaterItem itm in rptusers.Items)
        {
            CheckBox chk = (CheckBox)itm.FindControl("chkSelectForUser");
            DropDownList ddl = (DropDownList)itm.FindControl("ddlUsers");
            Label positionname = (Label)itm.FindControl("lblPositionName");
            HiddenField hfdVesselPositionID = (HiddenField)itm.FindControl("hfdVesselPositionID");
            HiddenField hfdTableID = (HiddenField)itm.FindControl("hfdTableID");
            
            if (chk.Checked)
            {
                
                string sql = "EXEC DBO.sp_IU_vesselAssignment_1 " + Common.CastAsInt32(hfdTableID.Value) + "," + VesselId.ToString() + ",'" + txtEffectiveDate.Text.Trim() + "'," + hfdVesselPositionID.Value + "," + ddl.SelectedValue + ",'"+ Session["UserName"] + "'"; 
                Common.Execute_Procedures_Select_ByQueryCMS(sql);
            }
        }

        
        Response.Redirect("~/emtm/Emtm_VesselassignmentHistory.aspx?VesselID="+VesselId);
    }

}