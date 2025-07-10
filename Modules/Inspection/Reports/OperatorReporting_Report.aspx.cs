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

public partial class Reports_OperatorReporting_Report : System.Web.UI.Page
{
    string InspectionId = "", VesselId = "";
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        //--------------------------------- PAGE ACCESS AUTHORITY ----------------------------------------------------
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 1054);
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
            chkallgp.Items[0].Selected = true;
            BindInspectionGroup();
            chkallgp_SelectedIndexChanged(sender, e);
            chkallinsp.Items[0].Selected = true;
            chkallinsp_SelectedIndexChanged(sender, e);
            BindOwnerDDL();
            ddl_Owner_SelectedIndexChanged(sender, e);
            //chklst_AllVsl.Items[0].Selected = true;
            //chklst_AllVsl_SelectedIndexChanged(sender, e);
        }
    }
    public void BindInspectionGroup()
    {
        try
        {
            DataSet ds1 = Inspection_Master.getMasterData("m_InspectionGroup", "Id", "Code as Name");
            this.chkgroup.DataSource = ds1.Tables[0];
            if (ds1.Tables[0].Rows.Count > 0)
            {
                this.chkgroup.DataValueField = "Id";
                this.chkgroup.DataTextField = "Name";
                this.chkgroup.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void BindInspection()
    {
        try
        {
            int i = 0;
            string str = "";
            chk_inspection.Items.Clear();
            chkallinsp.Items[0].Selected = false;
            for (i = 0; i < chkgroup.Items.Count; i++)
            {
                if (chkgroup.Items[i].Selected == true)
                {
                    if (str == "")
                        str = chkgroup.Items[i].Value;
                    else
                        str = str + "," + chkgroup.Items[i].Value;
                }
            }
            Session["Group"] = str;
            this.chk_inspection.DataTextField = "InspName";
            this.chk_inspection.DataValueField = "ID";
            if (str != "")
            {
                this.chk_inspection.DataSource = Search.GetInspections(str);
                this.chk_inspection.DataBind();
            }
            else
            {
                this.chk_inspection.DataSource = null;
                this.chk_inspection.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void chkallgp_SelectedIndexChanged(object sender, EventArgs e)
    {
        int i = 0;
        for (i = 0; i < chkgroup.Items.Count; i++)
        {
            if (chkallgp.Items[0].Selected == true)
                chkgroup.Items[i].Selected = true;
            else
                chkgroup.Items[i].Selected = false;
        }
        BindInspection();
    }
    protected void chkgroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //------------
            for (int i = 0; i < chkgroup.Items.Count; i++)
            {
                if (chkgroup.Items[i].Selected == false)
                    chkallgp.Items[0].Selected = false;
            }
            //------------
            chk_inspection.Items.Clear();
            BindInspection();
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.StackTrace.ToString();
        }
    }
    protected void chkallinsp_SelectedIndexChanged(object sender, EventArgs e)
    {
        int i = 0;
        for (i = 0; i < chk_inspection.Items.Count; i++)
        {
            if (chkallinsp.Items[0].Selected == true)
                chk_inspection.Items[i].Selected = true;
            else
                chk_inspection.Items[i].Selected = false;
        }
    }
    protected void chk_inspection_SelectedIndexChanged(object sender, EventArgs e)
    {
        for (int i = 0; i < chk_inspection.Items.Count; i++)
        {
            if (chk_inspection.Items[i].Selected == false)
                chkallinsp.Items[0].Selected = false;
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
            this.chklst_Vsls.DataSource = Search.GetVessel(intOwnerId,Convert.ToInt32(Session["loginid"].ToString()));
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
            this.chklst_Vsls.DataSource = Inspection_Master.getMasterDataforInspection("Vessel", "VesselId", "VesselName", Convert.ToInt32(Session["loginid"].ToString()));
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
        for (int I = 0; I < chk_inspection.Items.Count; I++)
        {
            if (chk_inspection.Items[I].Selected == true)
            {
                if (InspectionId == "")
                {
                    InspectionId = chk_inspection.Items[I].Value;
                }
                else
                {
                    InspectionId = InspectionId + "," + chk_inspection.Items[I].Value;
                }
            }
        }
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
        IFRAME1.Attributes.Add("src", "OperatorReporting_Crystal.aspx?InspID=" + InspectionId + "&OwnerID=" + Convert.ToInt32(ddl_Owner.SelectedValue) + "&VesselID=" + VesselId + "&FromDate=" + txtfromdate.Text.Trim() + "&ToDate=" + txttodate.Text.Trim() + "&OwnerName=" + ddl_Owner.SelectedItem.Text + "&VesselName=" + VesselId + "&FromText=" + txtfromdate.Text.Trim() + "&ToText=" + txttodate.Text.Trim());
        //DataTable dt = Operator_Reporting.SelectOperatorReportingDetails(InspectionId, int.Parse(ddl_Owner.SelectedValue), VesselId, txtfromdate.Text.Trim(), txttodate.Text.Trim());
        //if (dt.Rows.Count > 0)
        //{
        //    this.CrystalReportViewer1.Visible = true;

        //    CrystalReportViewer1.ReportSource = rpt;
        //    rpt.Load(Server.MapPath("RPT_OperatorReporting.rpt"));
        //    rpt.SetDataSource(dt);

        //    rpt.SetParameterValue("@Header", "Inspection Status - " + ddl_Owner.SelectedItem.Text);
        //    rpt.SetParameterValue("@Header1", "As On : " + System.DateTime.Now.Date.ToString("dd-MMM-yyyy"));
        //}
        //else
        //{
        //    lblMessage.Text = "No Record Found.";
        //    this.CrystalReportViewer1.Visible = false;
        //}
    }
    //protected void Page_Unload(object sender, EventArgs e)
    //{
    //    rpt.Close();
    //    rpt.Dispose();
    //}
    protected void btn_Export_Click(object sender, EventArgs e)
    {
        DataTable dt = Operator_Reporting.SelectOperatorReportingDetails(InspectionId, int.Parse(ddl_Owner.SelectedValue), VesselId, txtfromdate.Text, txttodate.Text);
        if (dt.Rows.Count > 0)
        {
            ExportDatatableToXLS(Response, dt, "OperatorReporting");
        }
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
