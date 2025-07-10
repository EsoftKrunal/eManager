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
using System.IO;

public partial class Registers_ChangeStatus : System.Web.UI.Page
{

    string strStatus = "";
    Authority Auth;
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        //--------------------------------- PAGE ACCESS AUTHORITY ----------------------------------------------------
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 159);
        if (chpageauth <= 0)
            Response.Redirect("blank.aspx");
        //------------------------------------------------------------------------------------------------------------

        lbl_Message.Text = "";
        if (Session["loginid"] != null)
        {
            ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 12);
            OBJ.Invoke();
            Session["Authority"] = OBJ.Authority;
            Auth = OBJ.Authority;
        }
        if (!Page.IsPostBack)
        {
            btn_Save.Enabled = Auth.isFirstApproval;
            btn_Cancel.Enabled = Auth.isFirstApproval;
            BindStatusDDL();
        }
    }
    protected void txtInspectionNo_TextChanged(object sender, EventArgs e)
    {
        DataTable dt1 = Insp_ChangeStatus.CheckInspNo(txtInspectionNo.Text.Trim());
        if (dt1.Rows.Count <= 0)
        {
            lbl_Message.Text = "Invalid Inspection#.";
            txtInspectionNo.Focus();
            ddlStatus.Items.Clear();
            ddlStatus.Items.Insert(0, new ListItem("<Select>", "0"));
        }
        else
        {
            strStatus = dt1.Rows[0]["Status"].ToString().Trim();
            HiddenField_Status.Value = strStatus;
            HiddenField_InspDueId.Value = dt1.Rows[0]["Id"].ToString().Trim();
            if (HiddenField_Status.Value == "Planned")
            {
                lbl_Message.Text = "No previous status exists for this Inspection#.";
                return;
            }
            if ((HiddenField_Status.Value == "Due") || (HiddenField_Status.Value == "Closed"))
            {
                lbl_Message.Text = "Status for Due/Closed Inspection# cannot be changed.";
                return;
            }
            BindStatusDDL();
        }
    }
    private void BindStatusDDL()
    {
        if (HiddenField_Status.Value == "Planned")
        {
            ddlStatus.Items.Clear();
            ddlStatus.Items.Insert(0, new ListItem("<Select>", "0"));
        }
        else if (HiddenField_Status.Value == "Executed")
        {
            ddlStatus.Items.Clear();
            ddlStatus.Items.Insert(0, new ListItem("<Select>", "0"));
            ddlStatus.Items.Insert(1, new ListItem("Planned", "Planned"));
        }
        else if (HiddenField_Status.Value == "Observation")
        {
            ddlStatus.Items.Clear();
            ddlStatus.Items.Insert(0, new ListItem("<Select>", "0"));
            ddlStatus.Items.Insert(1, new ListItem("Planned", "Planned"));
            ddlStatus.Items.Insert(2, new ListItem("Executed", "Observation"));
        }
        else if (HiddenField_Status.Value == "Response")
        {
            ddlStatus.Items.Clear();
            ddlStatus.Items.Insert(0, new ListItem("<Select>", "0"));
            ddlStatus.Items.Insert(1, new ListItem("Planned", "Planned"));
            ddlStatus.Items.Insert(2, new ListItem("Executed", "Executed"));
            ddlStatus.Items.Insert(3, new ListItem("Observation", "Observation"));
        }
        else
        {
            ddlStatus.Items.Clear();
            ddlStatus.Items.Insert(0, new ListItem("<Select>", "0"));
        }
    }
    protected void btn_Cancel_Click(object sender, EventArgs e)
    {
        HiddenField_Status.Value = "";
        txtInspectionNo.Text = "";
        ddlStatus.Items.Clear();
        ddlStatus.Items.Insert(0, new ListItem("<Select>", "0"));
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        if (txtInspectionNo.Text.Trim() == "")
        {
            lbl_Message.Text = "Please enter Inspection#.";
            txtInspectionNo.Focus();
            return;
        }
        if (ddlStatus.SelectedIndex <= 0)
        {
            lbl_Message.Text = "Please select Status.";
            ddlStatus.Focus();
            return;
        }
        try
        {
            DataTable dt2 = Insp_ChangeStatus.ChangeInspectionStatus(int.Parse(HiddenField_InspDueId.Value), ddlStatus.SelectedValue);
            try
            {
                lbl_Message.Text = "Status of Inspection#: " + txtInspectionNo.Text.Trim() + " Changed Successfully to " + ddlStatus.SelectedItem.Text;
            }
            catch (Exception ex)
            {
                lbl_Message.Text = ex.StackTrace.ToString();
            }
        }
        catch { }
    }
}
