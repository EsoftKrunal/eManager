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
using System.Data.SqlClient;
using System.Xml;
using ShipSoft.CrewManager.BusinessLogicLayer;
using ShipSoft.CrewManager.BusinessObjects;
using ShipSoft.CrewManager.Operational;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

public partial class Edit_NavigationAudit : System.Web.UI.Page
{
    public Authority Auth;
    public int SelectedQID
    {
        get { return Convert.ToInt32("0" + ViewState["SelectedQID"]); }
        set { ViewState["SelectedQID"] = value.ToString(); }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        //ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 292);
        //OBJ.Invoke();
        //Session["Authority"] = OBJ.Authority;
        //Auth = OBJ.Authority;
        lblMessage.Text = "";
        lblMsgQuesUpdate.Text = "";
        if (!Page.IsPostBack)
        {
            lblNANo.Text = Page.Request.QueryString["VNANo"];
            BindRepeater();
        }
    }

    // ------------ Function
    protected void SelectQuestion(object sender, EventArgs e)
    {
        int Qid =Common.CastAsInt32(((LinkButton)sender).CommandArgument);
        SelectedQID = Qid;
        DataTable DT= Common.Execute_Procedures_Select_ByQuery("select * from vna_details where vnaid in (select vnaid from vna_master where vnano='" + lblNANo.Text + "') and Qid=" + Qid.ToString());
        if (DT.Rows.Count > 0)
        {
            lblQNo.Text = DT.Rows[0]["QNo"].ToString();
            lblQComply.Text = DT.Rows[0]["Comply"].ToString();
            lblQText.Text = DT.Rows[0]["QText"].ToString();
            lbldeficiency.Text = DT.Rows[0]["Deficiency"].ToString();
            txtRemarks.Text = DT.Rows[0]["VerifyRemark"].ToString();
        }
        BindRepeater();
    }
    protected void btnVerify_Click(object sender, EventArgs e)
    {
        string UserName = Session["UserName"].ToString();
        Common.Execute_Procedures_Select_ByQuery("update vna_master set officeremark='" + txtGenComments.Text.Trim().Replace("'", "`") + "',verified=1,verifiedby='" + UserName + "',VerifiedOn='" + DateTime.Today.ToString("dd-MMM-yyyy") + "' where vnano='" + lblNANo.Text + "'");
        BindRepeater();
        lblMessage.Text = "Navigation alert verified sucessfully.";
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        string UserName = Session["UserName"].ToString();
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("update vna_master set Closed=1 ,ClosedBy='"+UserName+"',closedon='"+DateTime.Today.ToString("dd-MMM-yyyy")+"' where vnano='" + lblNANo.Text + "'");
        BindRepeater();
        lblMessage.Text = "Navigation alert closed sucessfully.";
    }
    protected void btnSaveQuesDetails_OnClick(object sender, EventArgs e)
     {
         if (SelectedQID > 0)
         {
             string sql = "update vna_details set VerifyRemark='" + txtRemarks.Text.Trim().Replace("'", "`") + "' where Qid=" + SelectedQID + " and vnaid in (select vnaid from vna_master where vnano='" + lblNANo.Text + "')";
             DataTable DT = Common.Execute_Procedures_Select_ByQuery(sql);

             lblMsgQuesUpdate.Text = "Remarks updated successfully.";
         }
         else
         {
             lblMsgQuesUpdate.Text = "Please select observation first.";
         }
     }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "tr", "SetLastFocus('divNavigationAuditQUES');", true);
    }

    // ------------ Function
    protected void BindRepeater()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("select *,ClosedBy+' / '+replace ( isnull(convert(varchar,ClosedOn,106),''),' ','-') ClosedByOn,Verifiedby+' / '+replace ( isnull(convert(varchar,VerifiedOn,106),''),' ','-') VerifiedByOn from vna_master where vnano='" + lblNANo.Text + "'");
        rptData.DataSource = Common.Execute_Procedures_Select_ByQuery("select * from vna_details where vnaid in (select vnaid from vna_master where vnano='" + lblNANo.Text + "') and Comply='No' and ltrim(rtrim(deficiency))<>''");
        rptData.DataBind();

        if (dt.Rows.Count > 0)
        {
            txtGenComments.Text = dt.Rows[0]["OfficeRemark"].ToString();


            if (dt.Rows[0]["Closed"].ToString() == "True")
            {
                lblVerifiedText.Text = "Verified By/On : <b>" + dt.Rows[0]["VerifiedByOn"].ToString() + "</b> ";
                lblClosureText.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Closed By/On : <b>" + dt.Rows[0]["ClosedByOn"].ToString() + "</b> ";
                btnClose.Visible = true;
                btnVerify.Visible = false;
                btnClose.Visible = false;
                btnSaveQuesDetails.Visible = false;
            }
            else if (dt.Rows[0]["Verified"].ToString() == "True")
            {
                lblVerifiedText.Text = "Verified By/On: <b>" + dt.Rows[0]["VerifiedByOn"].ToString() + "</b> ";
                btnClose.Visible = true;
                btnVerify.Visible = false;
                btnSaveQuesDetails.Visible = false;
            }
            else
            {
                btnVerify.Visible = true;
                btnClose.Visible = false;
                btnSaveQuesDetails.Visible = true;
            }
        }
    }
}

