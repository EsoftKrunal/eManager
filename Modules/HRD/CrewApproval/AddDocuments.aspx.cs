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

public partial class CrewApproval_AddDocuments : System.Web.UI.Page
{
    //--------------------------------------------------
    public int RankId
    {
        get { return Common.CastAsInt32(ViewState["RankId"]); }
        set { ViewState["RankId"] = value; } 
    }
    public int VesselId
    {
        get { return Common.CastAsInt32(ViewState["VesselId"]); }
        set { ViewState["VesselId"] = value; }
    }
    public int Loginid
    {
        get { return Common.CastAsInt32(ViewState["Loginid"]); }
        set { ViewState["Loginid"] = value; }
    }
    //--------------------------------------------------
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        if (!IsPostBack)
        {
            Loginid = Common.CastAsInt32(Session["loginid"].ToString());  
            if (Request.QueryString["RID"] != null)
            {
                RankId=Common.CastAsInt32(Request.QueryString["RID"].ToString());
                DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("SELECT RANKNAME FROM RANK WHERE RANKID=" + RankId.ToString());
                if (dt.Rows.Count > 0)
                {
                    lblRType.Text = "Rank :" + dt.Rows[0][0].ToString();
                }
            }

            // Bind Licences
            {
                DataTable dt = cls_VesselDocuments.SelectVesselDocumentsName(1,RankId,0);
                
                rptL.DataSource = dt;
                rptL.DataBind();
            }
            // Bind Course & Certificates
            {
                DataTable dt = cls_VesselDocuments.SelectVesselDocumentsName(2, RankId, 0);
                
                rptC.DataSource = dt;
                rptC.DataBind();
            }
            // Bind Endorsements
            {
                DataTable dt = cls_VesselDocuments.SelectVesselDocumentsName(3, RankId, 0);
                
                rptE.DataSource = dt;
                rptE.DataBind();
            }
            // Travel Documents
            {
                DataTable dt = cls_VesselDocuments.SelectVesselDocumentsName(4, RankId, 0); 
                
                rptT.DataSource = dt;
                rptT.DataBind();
            }
            // Medical Endorsements
            {
                DataTable dt = cls_VesselDocuments.SelectVesselDocumentsName(5, RankId, 0);
                
                rptM.DataSource = dt;
                rptM.DataBind();
            }
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        Common.Execute_Procedures_Select_ByQueryCMS("DELETE FROM RANKDOCUMENTS WHERE RANKID=" + RankId.ToString());
        //---------------------        
        for (int i = 0; i <= rptL.Items.Count - 1; i++)
        {
            CheckBox chkb= ((CheckBox)rptL.Items[i].FindControl("ckh_L"));
            if (chkb.Checked)
            {
                Common.Execute_Procedures_Select_ByQueryCMS("INSERT INTO RANKDOCUMENTS(RankId,DocumentTypeId,DocumentNameId,AlertDays,CreatedBy) VALUES(" + RankId.ToString() + ",1," + chkb.ToolTip + ",0," + Loginid.ToString() + ")");
            }
        }
        //---------------------
        for (int i = 0; i <= rptC.Items.Count - 1; i++)
        {
            CheckBox chkb = ((CheckBox)rptC.Items[i].FindControl("ckh_L"));
            if (chkb.Checked)
            {
                Common.Execute_Procedures_Select_ByQueryCMS("INSERT INTO RANKDOCUMENTS(RankId,DocumentTypeId,DocumentNameId,AlertDays,CreatedBy) VALUES(" + RankId.ToString() + ",2," + chkb.ToolTip + ",0," + Loginid.ToString() + ")");
            }
        }
        //---------------------
        for (int i = 0; i <= rptE.Items.Count - 1; i++)
        {
            CheckBox chkb = ((CheckBox)rptE.Items[i].FindControl("ckh_L"));
            if (chkb.Checked)
            {
                Common.Execute_Procedures_Select_ByQueryCMS("INSERT INTO RANKDOCUMENTS(RankId,DocumentTypeId,DocumentNameId,AlertDays,CreatedBy) VALUES(" + RankId.ToString() + ",3," + chkb.ToolTip + ",0," + Loginid.ToString() + ")");
            }
        }
        //---------------------
        for (int i = 0; i <= rptT.Items.Count - 1; i++)
        {
            CheckBox chkb = ((CheckBox)rptT.Items[i].FindControl("ckh_L"));
            if (chkb.Checked)
            {
                Common.Execute_Procedures_Select_ByQueryCMS("INSERT INTO RANKDOCUMENTS(RankId,DocumentTypeId,DocumentNameId,AlertDays,CreatedBy) VALUES(" + RankId.ToString() + ",4," + chkb.ToolTip + ",0," + Loginid.ToString() + ")");
            }
        }
        //---------------------
        for (int i = 0; i <= rptM.Items.Count - 1; i++)
        {
            CheckBox chkb = ((CheckBox)rptM.Items[i].FindControl("ckh_L"));
            if (chkb.Checked)
            {
                Common.Execute_Procedures_Select_ByQueryCMS("INSERT INTO RANKDOCUMENTS(RankId,DocumentTypeId,DocumentNameId,AlertDays,CreatedBy) VALUES(" + RankId.ToString() + ",5," + chkb.ToolTip + ",0," + Loginid.ToString() + ")");
            }
        }
    }
    //--------------------------------------------------
}
