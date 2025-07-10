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

public partial class FormReporting_AddFollowUpList : System.Web.UI.Page
{
    string strResponsibility = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        lblmessage.Text = "";
        if (!Page.IsPostBack)
        {
            BindVessel();
        }
    }
    public void BindVessel()
    {
        try
        {
            /* DataSet dsVessel = Budget.getTable("SELECT VesselID,Vesselname FROM DBO.Vessel v where v.VesselStatusid<>2  order by VesselName");// */
            DataSet dsVessel =  Inspection_Master.getMasterDataforInspection("Vessel", "VesselId", "VesselName", Convert.ToInt32(Session["loginid"].ToString()));
            this.ddlvessel.DataSource = dsVessel;
            this.ddlvessel.DataValueField = "VesselID";
            this.ddlvessel.DataTextField = "Vesselname";
            this.ddlvessel.DataBind();
            ddlvessel.Items.Insert(0, "<Select>");
            ddlvessel.Items[0].Value = "0";
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlvessel.SelectedValue == "0")
            {
                lblmessage.Text = "Please select Vessel.";
                ddlvessel.Focus();
                return;
            }
            if (ddl_FollowUpCat.SelectedValue == "0")
            {
                lblmessage.Text = "Please select FollowUp Category.";
                ddl_FollowUpCat.Focus();
                return;
            }
            if (txt_DeficiencyText.Text == "")
            {
                lblmessage.Text = "Please enter Deficiency.";
                txt_DeficiencyText.Focus();
                return;
            }

            for (int k = 0; k < chklst_Respons.Items.Count; k++)
            {
                if (chklst_Respons.Items[k].Selected == true)
                {
                    if (strResponsibility == "")
                    {
                        strResponsibility = chklst_Respons.Items[k].Value;
                    }
                    else
                    {
                        strResponsibility = strResponsibility + "," + chklst_Respons.Items[k].Value;
                    }
                }
            }
            DataTable dt1 = FollowUp_Tracker.InserUpdateTrackerFollowUpList(int.Parse(ddlvessel.SelectedValue), int.Parse(ddl_FollowUpCat.SelectedValue), chk_Critical.Checked ? "Y" : "N", "Y", txt_TarClDate.Text, 0, "", strResponsibility, txt_DeficiencyText.Text.Trim(), txt_CorrActions.Text.Trim(), "", int.Parse(Session["loginid"].ToString()), 0, 0, "INSERT");
            lblmessage.Text = "Record Saved Successfully.";
            btn_Save.Enabled = false;
        }
        catch (Exception ex) { throw ex; }
    }
    protected void btn_Cancel_Click(object sender, EventArgs e)
    {
        ddlvessel.SelectedIndex = -1;
        ddl_FollowUpCat.SelectedIndex = -1;
        txt_CorrActions.Text = "";
        txt_DeficiencyText.Text = "";
        txt_TarClDate.Text = "";
        chk_Critical.Checked = false;
        chklst_Respons.SelectedIndex = -1;
        btn_Save.Enabled = true;
    }
}
