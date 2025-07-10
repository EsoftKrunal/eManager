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
using System.Text;
using System.Xml;
public partial class CrewApproval_AddFSDocuments : System.Web.UI.Page
{
    //--------------------------------------------------
    public int FSId
    {
        get { return Common.CastAsInt32(ViewState["FSId"]); }
        set { ViewState["FSId"] = value; }
    }
    public int Loginid
    {
        get { return Common.CastAsInt32(ViewState["Loginid"]); }
        set { ViewState["Loginid"] = value; }
    }
    //--------------------------------------------------
    private void BindRankDropDown()
    {
        ProcessSelectRank obj = new ProcessSelectRank();
        obj.Invoke();
        ddl_RWRankFilter.DataSource = obj.ResultSet.Tables[0];
        ddl_RWRankFilter.DataTextField = "RankName";
        ddl_RWRankFilter.DataValueField = "RankId";
        ddl_RWRankFilter.DataBind();

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        if (!IsPostBack)
        {
            BindRankDropDown();
            Loginid = Common.CastAsInt32(Session["loginid"].ToString());  
           
            if (Request.QueryString["FID"] != null)
            {
                FSId = Common.CastAsInt32(Request.QueryString["FID"].ToString());
                DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("SELECT COUNTRYNAME FROM COUNTRY WHERE COUNTRYID=" + FSId.ToString());
                if (dt.Rows.Count > 0)
                {
                    lblRType.Text = "Flag State :" + dt.Rows[0][0].ToString();
                }
            }
            BindData();
        }
    }
    protected void BindData()
    {
        int RankId = Common.CastAsInt32(ddl_RWRankFilter.SelectedValue);
        // Bind Licences
        {
            DataTable dt = cls_VesselDocuments.SelectFSDocumentsName(1, RankId, FSId);

            rptL.DataSource = dt;
            rptL.DataBind();
        }
        // Bind Course & Certificates
        {
            DataTable dt = cls_VesselDocuments.SelectFSDocumentsName(2, RankId, FSId);

            rptC.DataSource = dt;
            rptC.DataBind();
        }
        // Bind Endorsements
        {
            DataTable dt = cls_VesselDocuments.SelectFSDocumentsName(3, RankId, FSId);

            rptE.DataSource = dt;
            rptE.DataBind();
        }
        // Travel Documents
        {
            DataTable dt = cls_VesselDocuments.SelectFSDocumentsName(4, RankId, FSId);

            rptT.DataSource = dt;
            rptT.DataBind();
        }
        // Medical Endorsements
        {
            DataTable dt = cls_VesselDocuments.SelectFSDocumentsName(5, RankId, FSId);

            rptM.DataSource = dt;
            rptM.DataBind();
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ddl_RWRankFilter.SelectedIndex > 0 && FSId > 0)
        {
            Common.Execute_Procedures_Select_ByQueryCMS("DELETE FROM FLAGSTATEDOCUMENTS WHERE FSID=" + FSId.ToString() + " AND RANKID=" + ddl_RWRankFilter.SelectedValue);
            //---------------------        
            for (int i = 0; i <= rptL.Items.Count - 1; i++)
            {
                CheckBox chkb = ((CheckBox)rptL.Items[i].FindControl("ckh_L"));
                if (chkb.Checked)
                {
                    Common.Execute_Procedures_Select_ByQueryCMS("INSERT INTO FLAGSTATEDOCUMENTS(FSId,DocumentTypeId,DocumentNameId,RankId,CreatedBy) VALUES(" + FSId.ToString() + ",1," + chkb.ToolTip + "," + ddl_RWRankFilter.SelectedValue + "," + Loginid.ToString() + ")");
                }
            }
            //---------------------
            for (int i = 0; i <= rptC.Items.Count - 1; i++)
            {
                CheckBox chkb = ((CheckBox)rptC.Items[i].FindControl("ckh_L"));
                if (chkb.Checked)
                {
                    Common.Execute_Procedures_Select_ByQueryCMS("INSERT INTO FLAGSTATEDOCUMENTS(FSId,DocumentTypeId,DocumentNameId,RankId,CreatedBy) VALUES(" + FSId.ToString() + ",2," + chkb.ToolTip + "," + ddl_RWRankFilter.SelectedValue + "," + Loginid.ToString() + ")");
                }
            }
            //---------------------
            for (int i = 0; i <= rptE.Items.Count - 1; i++)
            {
                CheckBox chkb = ((CheckBox)rptE.Items[i].FindControl("ckh_L"));
                if (chkb.Checked)
                {
                    Common.Execute_Procedures_Select_ByQueryCMS("INSERT INTO FLAGSTATEDOCUMENTS(FSId,DocumentTypeId,DocumentNameId,RankId,CreatedBy) VALUES(" + FSId.ToString() + ",3," + chkb.ToolTip + "," + ddl_RWRankFilter.SelectedValue + "," + Loginid.ToString() + ")");
                }
            }
            //---------------------
            for (int i = 0; i <= rptT.Items.Count - 1; i++)
            {
                CheckBox chkb = ((CheckBox)rptT.Items[i].FindControl("ckh_L"));
                if (chkb.Checked)
                {
                    Common.Execute_Procedures_Select_ByQueryCMS("INSERT INTO FLAGSTATEDOCUMENTS(FSId,DocumentTypeId,DocumentNameId,RankId,CreatedBy) VALUES(" + FSId.ToString() + ",4," + chkb.ToolTip + "," + ddl_RWRankFilter.SelectedValue + "," + Loginid.ToString() + ")");
                }
            }
            //---------------------
            for (int i = 0; i <= rptM.Items.Count - 1; i++)
            {
                CheckBox chkb = ((CheckBox)rptM.Items[i].FindControl("ckh_L"));
                if (chkb.Checked)
                {
                    Common.Execute_Procedures_Select_ByQueryCMS("INSERT INTO FLAGSTATEDOCUMENTS(FSId,DocumentTypeId,DocumentNameId,RankId,CreatedBy) VALUES(" + FSId.ToString() + ",5," + chkb.ToolTip + "," + ddl_RWRankFilter.SelectedValue + "," + Loginid.ToString() + ")");
                }
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "alert('Record saved successfully');", true);
        }
       
    }
    //--------------------------------------------------
    protected void ddlRank_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }
    
}
