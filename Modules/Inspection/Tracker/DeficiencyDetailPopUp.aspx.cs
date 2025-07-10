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

public partial class FormReporting_DeficiencyDetailPopUp : System.Web.UI.Page
{
    int intInspDueId = 0, intObsvId = 0;
    string strResponsibility = "", strFlagName = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        lblmessage.Text = "";
        try
        {
            if (Page.Request.QueryString["INSPDId"].ToString() != "")
                intInspDueId = int.Parse(Page.Request.QueryString["INSPDId"].ToString());
            if (Page.Request.QueryString["OBSVTNID"].ToString() != "")
                intObsvId = int.Parse(Page.Request.QueryString["OBSVTNID"].ToString());
            if (Page.Request.QueryString["TBLFLAG"].ToString() != "")
                strFlagName = Page.Request.QueryString["TBLFLAG"].ToString();
            if (!Page.IsPostBack)
            {
                if (strFlagName == "NEW")
                    txt_DeficiencyText.ReadOnly = false;
                else
                    txt_DeficiencyText.ReadOnly = true;
                DataTable dt4 = FollowUp_Tracker.GetFollowUpDetailsByInspObsvId(intInspDueId, intObsvId, "", "", "SELECT", 0, 0, strFlagName, "", "", "", 0, "", "");
                if (dt4.Rows.Count > 0)
                {
                    if (dt4.Rows[0]["Closed"].ToString() == "True")
                    {
                        btn_Save.Visible = false;
                        btn_Cancel.Visible = false;
                    }
                    else
                    {
                        btn_Save.Visible = true;
                        btn_Cancel.Visible = true;
                    }
                }
                ShowRecord();
            }
        }
        catch (Exception ex) { throw ex; }
    }
    protected void ShowRecord()
    {
        DataTable dt1 = FollowUp_Tracker.GetFollowUpDetailsByInspObsvId(intInspDueId, intObsvId, "", "", "SELECT", 0, 0, strFlagName, "", "", "", 0, "", "");
        if (dt1.Rows.Count > 0)
        {
            txt_DeficiencyText.Text = dt1.Rows[0]["Deficiency"].ToString();
            txt_CorrActions.Text = dt1.Rows[0]["CorrectiveActions"].ToString();
            txt_TargetCDate.Text = dt1.Rows[0]["TargetCloseDt"].ToString();
            txt_CreatedBy.Text = dt1.Rows[0]["F_CreatedBy"].ToString();
            txt_CreatedOn.Text = dt1.Rows[0]["F_CreatedOn"].ToString();
            txt_FModifiedBy.Text = dt1.Rows[0]["Modified_By"].ToString();
            txt_FModifiedOn.Text = dt1.Rows[0]["Modified_On"].ToString();
            if (dt1.Rows[0]["Responsibilty"].ToString() != "")
            {
                char[] resp = { ',' };
                Array rs = dt1.Rows[0]["Responsibilty"].ToString().Split(resp);
                for (int l = 0; l < chklst_Respons.Items.Count; l++)
                {
                    chklst_Respons.Items[l].Selected = false;
                }
                for (int m = 0; m < rs.Length; m++)
                {
                    if (rs.GetValue(m).ToString() == "Vessel")
                        chklst_Respons.Items[0].Selected = true;
                    if (rs.GetValue(m).ToString() == "Office")
                        chklst_Respons.Items[1].Selected = true;
                }
            }
            else
                chklst_Respons.SelectedIndex = -1;

        }

        if (strFlagName == "OLD")
        {
            DataTable dtObservation = Inspection_FollowUp.InspectionObservation(intObsvId, "", "", "", "", 0, "", "SELECT", "", 0, 0, 0);
            if (dtObservation.Rows.Count > 0)
            {
                if (dtObservation.Rows[0]["Flaws"].ToString() != "")
                {
                    //rdbflaws.SelectedValue = Dt.Rows[0]["Flaws"].ToString();
                    char[] c = { ',' };
                    Array a = dtObservation.Rows[0]["Flaws"].ToString().Split(c);
                    for (int j = 0; j <= rdbflaws.Items.Count - 1; j++)
                    {
                        rdbflaws.Items[j].Selected = false;
                    }
                    for (int r = 0; r < a.Length; r++)
                    {
                        if (a.GetValue(r).ToString() == "People")
                            rdbflaws.Items[0].Selected = true;
                        if (a.GetValue(r).ToString() == "Process")
                            rdbflaws.Items[1].Selected = true;
                        if (a.GetValue(r).ToString() == "Technology" || a.GetValue(r).ToString() == "Equipment")
                            rdbflaws.Items[2].Selected = true;
                    }
                }
                else
                    rdbflaws.SelectedIndex = -1;
            }
            else
            {
                rdbflaws.SelectedIndex = -1;
            }
        }
        if (strFlagName == "OBN" && dt1.Rows.Count > 0)
        {
            
            DataTable dt_OBNEW = Common.Execute_Procedures_Select_ByQuery("SELECT FLAWS FROM t_Observationsnew WHERE TableId="  + intObsvId + " AND InspDue_Id=" + intInspDueId);

            if (dt_OBNEW.Rows[0]["Flaws"].ToString() != "")
            {
                //rdbflaws.SelectedValue = Dt.Rows[0]["Flaws"].ToString();
                char[] c = { ',' };
                Array a = dt_OBNEW.Rows[0]["Flaws"].ToString().Split(c);
                for (int j = 0; j <= rdbflaws.Items.Count - 1; j++)
                {
                    rdbflaws.Items[j].Selected = false;
                }
                for (int r = 0; r < a.Length; r++)
                {
                    if (a.GetValue(r).ToString() == "People")
                        rdbflaws.Items[0].Selected = true;
                    if (a.GetValue(r).ToString() == "Process")
                        rdbflaws.Items[1].Selected = true;
                    if (a.GetValue(r).ToString() == "Technology" || a.GetValue(r).ToString() == "Equipment")
                        rdbflaws.Items[2].Selected = true;
                }
            }
            else
                rdbflaws.SelectedIndex = -1;
        }
        if (strFlagName == "NEW" && dt1.Rows.Count > 0)
        {
            DataTable dt_OBNEW = Common.Execute_Procedures_Select_ByQuery("SELECT FP_Flaws FROM FR_FollowUpList WHERE FP_Id=" + intInspDueId);

            if (dt_OBNEW.Rows[0]["FP_Flaws"].ToString() != "")
            {
                //rdbflaws.SelectedValue = Dt.Rows[0]["Flaws"].ToString();
                char[] c = { ',' };
                Array a = dt_OBNEW.Rows[0]["FP_Flaws"].ToString().Split(c);
                for (int j = 0; j <= rdbflaws.Items.Count - 1; j++)
                {
                    rdbflaws.Items[j].Selected = false;
                }
                for (int r = 0; r < a.Length; r++)
                {
                    if (a.GetValue(r).ToString() == "People")
                        rdbflaws.Items[0].Selected = true;
                    if (a.GetValue(r).ToString() == "Process")
                        rdbflaws.Items[1].Selected = true;
                    if (a.GetValue(r).ToString() == "Technology" || a.GetValue(r).ToString() == "Equipment")
                        rdbflaws.Items[2].Selected = true;
                }
            }
            else
                rdbflaws.SelectedIndex = -1;       
        }
    }
    protected void btn_Cancel_Click(object sender, EventArgs e)
    {
        txt_CorrActions.Text = "";
        txt_TargetCDate.Text = "";
        chklst_Respons.SelectedIndex = -1;
        btn_Save.Enabled = true;
        txt_FModifiedBy.Text = "";
        txt_FModifiedOn.Text = "";
        
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        try
        {
            string strflaws = "";
            for (int cnt = 0; cnt <= rdbflaws.Items.Count - 1; cnt++)
            {
                if (rdbflaws.Items[cnt].Selected == true)
                {
                    if (strflaws == "")
                    {
                        strflaws = rdbflaws.Items[cnt].Value;
                    }
                    else
                    {
                        strflaws = strflaws + "," + rdbflaws.Items[cnt].Value;
                    }
                }
            }
            if (strflaws.Trim() == "") { lblmessage.Text = "Please select cause."; return; } 

            if (txt_CorrActions.Text.Trim() == "")
            {
                lblmessage.Text = "Please enter Corrective Actions.";
                return;
            }
            if (txt_TargetCDate.Text == "")
            {
                lblmessage.Text = "Please enter Target Closure Date.";
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
            if (strFlagName == "OLD")
            {
                DataTable dt2 = Inspection_FollowUp.InspectionObservation(intObsvId, txt_CorrActions.Text, strflaws, "", txt_TargetCDate.Text, 0, "", "MODIFY", strResponsibility, 0, int.Parse(Session["loginid"].ToString()), 0);
            }
            else if (strFlagName == "OBN")
            {
                DataTable dt3 = FollowUp_Tracker.GetFollowUpDetailsByInspObsvId(intInspDueId, intObsvId, "", "", "MODIFY", 0, 0, "OBN", txt_CorrActions.Text.Trim(), txt_TargetCDate.Text.Trim(), strResponsibility, int.Parse(Session["loginid"].ToString()), txt_DeficiencyText.Text.Trim(), strflaws);
            }
            else
            {
                DataTable dt3 = FollowUp_Tracker.GetFollowUpDetailsByInspObsvId(intInspDueId, 0, "", "", "MODIFY", 0, 0, "NEW", txt_CorrActions.Text.Trim(), txt_TargetCDate.Text.Trim(), strResponsibility, int.Parse(Session["loginid"].ToString()), txt_DeficiencyText.Text.Trim(), strflaws);
            }
            lblmessage.Text = "Record Saved Sucessfully.";
        }
        catch (Exception ex)
        {
            lblmessage.Text = ex.StackTrace.ToString();
        }
    }
}
