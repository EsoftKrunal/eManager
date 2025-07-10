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

public partial class OperatorReportingSummary_Report : System.Web.UI.Page
{
    string VesselId = "";
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        //--------------------------------- PAGE ACCESS AUTHORITY ----------------------------------------------------
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 163);
        if (chpageauth <= 0)
            Response.Redirect("blank.aspx");
        //------------------------------------------------------------------------------------------------------------
   
   
        if (Session["loginid"] == null)
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Default.aspx';", true);
        }
        lblMessage.Text = "";
        if (!Page.IsPostBack)
        {
            BindOwnerDDL();
            ddl_Owner_SelectedIndexChanged(sender, e);
            //chklst_AllVsl.Items[0].Selected = true;
            //chklst_AllVsl_SelectedIndexChanged(sender, e);
        }
    }
    protected void BindOwnerDDL()
    {
        DataSet ds2 = Inspection_Master.getMasterDataforInspection("Owner", "OwnerId", "OwnerName");
        this.ddl_Owner.DataSource = ds2;
        if (ds2.Tables[0].Rows.Count > 0)
        {
            this.ddl_Owner.DataValueField = "OwnerId";
            this.ddl_Owner.DataTextField = "OwnerName";
            this.ddl_Owner.DataBind();
            this.ddl_Owner.Items.Insert(0, new ListItem("All", "0"));
            this.ddl_Owner.Items[0].Value = "0";
        }
    }
    protected void BindVesselDDL(int intOwnerId)
    {
        if (ddl_Owner.SelectedIndex > 0)
        {

            chklst_Vsls.Items.Clear();
            //chklst_AllVsl.Items[0].Selected = false;
            this.chklst_Vsls.DataTextField = "VesselName";
            this.chklst_Vsls.DataValueField = "VesselId";
            DataTable dt = Budget.getTable("SELECT VESSELID, VESSELNAME FROM DBO.VESSEL WHERE VESSELSTATUSID=1  AND OWNERID=" + intOwnerId.ToString() + " ORDER BY VESSELNAME").Tables[0];
            this.chklst_Vsls.DataSource = dt;
            this.chklst_Vsls.DataBind();
            for (int a = 0; a < chklst_Vsls.Items.Count; a++)
            {
                chklst_Vsls.Items[a].Selected = true;
            }
        }
        else
        {
            chklst_Vsls.Items.Clear();
            this.chklst_Vsls.DataTextField = "VesselName";
            this.chklst_Vsls.DataValueField = "VesselId";
            DataTable dt = Budget.getTable("SELECT VESSELID, VESSELNAME FROM DBO.VESSEL WHERE VESSELSTATUSID=1  ORDER BY VESSELNAME").Tables[0];
            this.chklst_Vsls.DataSource = dt;
            this.chklst_Vsls.DataBind();
            //chklst_AllVsl.Items[0].Selected = true;
            //chklst_AllVsl_SelectedIndexChanged(new object(), new EventArgs());
            for (int a = 0; a < chklst_Vsls.Items.Count; a++)
            {
                chklst_Vsls.Items[a].Selected = true;
            }
        }
        
    }
    protected void ddl_Owner_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindVesselDDL(Convert.ToInt32(ddl_Owner.SelectedValue));
    }
    protected void chklst_AllVsl_SelectedIndexChanged(object sender, EventArgs e)
    {
        //int i = 0;
        //for (i = 0; i < chklst_Vsls.Items.Count; i++)
        //{
        //    if (chklst_AllVsl.Items[0].Selected == true)
        //        chklst_Vsls.Items[i].Selected = true;
        //    else
        //        chklst_Vsls.Items[i].Selected = false;
        //}
    }
    protected void btn_Show_Click(object sender, EventArgs e)
    {
        for (int J = 0; J < chklst_Vsls.Items.Count; J++)
        {
            if (chklst_Vsls.Items[J].Selected == true)
            {
                if (VesselId == "")
                {
                    VesselId = chklst_Vsls.Items[J].Value;
                }
                else
                {
                    VesselId = VesselId + "," + chklst_Vsls.Items[J].Value;
                }
            }
        }
        if(rad_Summary.Checked)
            IFRAME1.Attributes.Add("src", "OperatorReportingSummary_Crystal.aspx?&VesselID=" + VesselId + "&Mode=Summary");
        else
            IFRAME1.Attributes.Add("src", "OperatorReportingSummary_Crystal.aspx?&VesselID=" + VesselId + "&Mode=Details");
    }
    public void ExportDatatableToXLS(HttpResponse Response, DataTable dt, String FileName)
    {
        Response.Clear();
        Response.AddHeader("content-disposition", "attachment;filename=" + FileName + ".xls");
        Response.Charset = "";
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.xls";
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
        System.Web.UI.WebControls.DataGrid dg = new System.Web.UI.WebControls.DataGrid();
        dg.HeaderStyle.Font.Bold = true;
        dg.DataSource = dt;
        dg.DataBind();
        dg.RenderControl(htmlWrite);
        Response.Write(stringWrite.ToString());
        Response.End();
    }
}
