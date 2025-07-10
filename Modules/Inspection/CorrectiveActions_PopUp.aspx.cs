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

public partial class Transactions_CorrectiveActions_PopUp : System.Web.UI.Page
{
    string RespText = "", DefText = "", strResponsibility = "";
    int intObsvId = 0;
    Authority Auth;
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        if (Session["loginid"] != null)
        {
            ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 10);
            OBJ.Invoke();
            Session["Authority"] = OBJ.Authority;
            Auth = OBJ.Authority;
        }
        try
        {
            btn_Save.Enabled = Auth.isEdit;
            btn_Cancel.Enabled = Auth.isEdit;
        }
        catch { }
        try
        {
            intObsvId = int.Parse(Page.Request.QueryString["ObsvtnId"].ToString());
            if (!Page.IsPostBack)
            {
                DefText = Session["DefText"].ToString();
                txt_DeficiencyText.Text = DefText;
                if (Convert.ToBoolean(Session["FU"])) // FOLLOW UP DONE
                {
                    DataTable dtFollowUp = Inspection_FollowUp.InspectionObservation(intObsvId, "", "", "", "", 0, "", "SELECT", "", 0, 0, 0);
                    if (dtFollowUp.Rows.Count > 0)
                    {
                        RespText = dtFollowUp.Rows[0]["CorrectiveActions"].ToString();
                        dvActions.InnerHtml = Session["RespMgmtText"].ToString();
                        txt_CorrActions.Text = RespText;
                        if (dtFollowUp.Rows[0]["TargetCloseDt"].ToString() != "")
                            //txtcloseddt.Text = DateTime.Parse(Dt.Rows[0]["ClosedDate"].ToString()).ToString("MM/dd/yyyy");
                            txt_TargetCDate.Text = dtFollowUp.Rows[0]["TargetCloseDt"].ToString();
                        else
                            txt_TargetCDate.Text = "";

                        if (dtFollowUp.Rows[0]["Responsibilty"].ToString() != "")
                        {
                            char[] resp = { ',' };
                            Array rs = dtFollowUp.Rows[0]["Responsibilty"].ToString().Split(resp);
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
                }
                else // FOLLOW UP PENDING
                {
                    RespText = Session["RespMgmtText"].ToString();
                    dvActions.InnerHtml = RespText;
                    txt_CorrActions.Text = "";
                }
                DataTable dtObservation = Inspection_FollowUp.InspectionObservation(intObsvId, "", "", "", "", 0, "", "SELECT", "", 0, 0, 0);
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
        }
        catch { }
    }
    protected void btn_Notify_Click(object sender, EventArgs e)
    {
        string strInspNum = "", strQuestNum = "", strDeficiency = "", strCorrectiveActions = "", strResponsibility = "", strTargetCloseDt = "";
        try
        {
            DataTable dt1 = Inspection_FollowUp.GetFollowUpMailDetails(int.Parse(Session["Insp_Id"].ToString()), intObsvId);
            if (dt1.Rows.Count > 0)
            {
                strInspNum = dt1.Rows[0]["InspectionNo"].ToString();
                strQuestNum = dt1.Rows[0]["QuestionNo"].ToString();
                strDeficiency = dt1.Rows[0]["Deficiency"].ToString();
                strCorrectiveActions = dt1.Rows[0]["CorrectiveActions"].ToString();
                strResponsibility = dt1.Rows[0]["Responsibilty"].ToString();
                strTargetCloseDt = dt1.Rows[0]["TargetCloseDate"].ToString();
            }
        }
        catch { }
        try
        {
            SendMail.FollowUpMail("FollowUp", strInspNum, strQuestNum, strDeficiency, strCorrectiveActions, strResponsibility, strTargetCloseDt);
            lblmessage.Text = "Mail Send Successfully.";
        }
        catch { }
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
            
            DataTable dt = Inspection_FollowUp.InspectionObservation(intObsvId, txt_CorrActions.Text, strflaws, "", txt_TargetCDate.Text, 0, "", "MODIFY", strResponsibility, int.Parse(Session["loginid"].ToString()), int.Parse(Session["loginid"].ToString()), 0);
            Inspection_Response.ResponseDetails(intObsvId, 0, "", "", 0, 0, 0, 0, 0, "", "", "UPFOLL", "", "", "", "Y");
            lblmessage.Text = "Record Saved Sucessfully.";
            btn_Save.Enabled = false;
            btn_Notify.Enabled = true;

        }
        catch (Exception ex)
        {
            lblmessage.Text = ex.StackTrace.ToString();
        }
    }
    protected void btn_Cancel_Click(object sender, EventArgs e)
    {
        RespText = Session["RespMgmtText"].ToString();
        txt_CorrActions.Text = "";
        DefText = Session["DefText"].ToString();
        txt_DeficiencyText.Text = DefText;
        txt_TargetCDate.Text = "";
        chklst_Respons.SelectedIndex = -1;
        rdbflaws.SelectedIndex = -1;  
    }
}
