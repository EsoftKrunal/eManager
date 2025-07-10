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
using System.Collections.Generic;
using System.Xml;
using System.IO;
using Microsoft.Reporting.WebForms;

public partial class Reports_InspMisc_Report : System.Web.UI.Page
{
    DataSet ds = new DataSet();
    private DataSet m_dataSet;
    private MemoryStream m_rdl;
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    string strSelFields = "";
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
            BindInspGrpDDL();
            ddl_InspGroup_SelectedIndexChanged(sender, e);
            BindOwnerDDL();
            BindFleetDDL();
            ddl_Fleet_SelectedIndexChanged(sender, e);
            //BindAvailableFieldsLST();
        }
        else
        {
            //Show_Report();
        }
    }
    private void BindInspGrpDDL()
    {
        DataSet ds1 = Inspection_Master.getMasterData("m_InspectionGroup", "Id", "(Code+ ' - ' +Name) as Name");
        this.ddl_InspGroup.DataSource = ds1.Tables[0];
        if (ds1.Tables[0].Rows.Count > 0)
        {
            this.ddl_InspGroup.DataValueField = "Id";
            this.ddl_InspGroup.DataTextField = "Name";
            this.ddl_InspGroup.DataBind();
            this.ddl_InspGroup.Items.Insert(0, new ListItem("All", "0"));
            this.ddl_InspName.Items.Insert(0, new ListItem("All", "0"));
        }
    }
    protected void ddl_InspGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindInspectionDDL(Convert.ToInt32(ddl_InspGroup.SelectedValue));
    }
    private void BindInspectionDDL(int InspGrpId)
    {
        if (ddl_InspGroup.SelectedIndex > 0)
        {
            DataTable dt1 = InspCheckListReport.GetInspectionFromInspGroup(InspGrpId);
            if (dt1.Rows.Count > 0)
            {
                this.ddl_InspName.DataValueField = "Id";
                this.ddl_InspName.DataTextField = "InspName";
                this.ddl_InspName.DataSource = dt1;
                this.ddl_InspName.DataBind();
                this.ddl_InspName.Items.Insert(0, new ListItem("All", "0"));
            }
            else
            {
                ddl_InspName.Items.Clear();
                this.ddl_InspName.DataSource = null;
                this.ddl_InspName.Items.Insert(0, new ListItem("All", "0"));
            }
        }
        else
        {
            this.ddl_InspName.DataTextField = "Name";
            this.ddl_InspName.DataValueField = "Id";
            this.ddl_InspName.DataSource = Inspection_Master.getMasterData("m_Inspection", "Id", "Name");
            this.ddl_InspName.DataBind();
            this.ddl_InspName.Items.Insert(0, new ListItem("All", "0"));
            this.ddl_InspName.Items[0].Value = "0";
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
            this.ddl_Vessel.Items.Insert(0, new ListItem("All", "0"));
            this.ddl_Vessel.Items[0].Value = "0";
        }
    }
    protected void BindFleetDDL()
    {
        DataTable ds2 = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.FLEETMASTER ORDER BY FLEETNAME");
        this.ddlFleet.DataSource = ds2;
        this.ddlFleet.DataValueField = "FleetId";
        this.ddlFleet.DataTextField = "FleetName";
        this.ddlFleet.DataBind();
        this.ddlFleet.Items.Insert(0, new ListItem("All", "0"));
        this.ddlFleet.Items[0].Value = "0";
    }

    protected void ddl_Owner_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlFleet.SelectedIndex = 0;
        BindVesselDDL(Convert.ToInt32(ddl_Owner.SelectedValue));
    }
    protected void ddl_Fleet_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_Owner.SelectedIndex = 0;
        BindVesselDDL(Convert.ToInt32(ddlFleet.SelectedValue));
    }
    protected void BindVesselDDL(int intOwnerId)
    {

        if (ddl_Owner.SelectedIndex > 0)
        {
            ddl_Vessel.Controls.Clear();
            this.ddl_Vessel.DataTextField = "VesselName";
            this.ddl_Vessel.DataValueField = "VesselId";
            this.ddl_Vessel.DataSource = Search.GetVessel(intOwnerId, Convert.ToInt32(Session["loginid"].ToString()));
            this.ddl_Vessel.DataBind();
            this.ddl_Vessel.Items.Insert(0, new ListItem("All", "0"));
            this.ddl_Vessel.Items[0].Value = "0";
        }
        else if (ddlFleet.SelectedIndex > 0)
        {
            ddl_Vessel.Controls.Clear();
            this.ddl_Vessel.DataTextField = "VesselName";
            this.ddl_Vessel.DataValueField = "VesselId";
            this.ddl_Vessel.DataSource = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.VESSEL WHERE VESSELSTATUSID=1  and FLEETID=" + intOwnerId + " and VesselId in (Select VesselId from UserVesselRelation with(nolock) where Loginid = " + Convert.ToInt32(Session["loginid"].ToString()) + ") ORDER BY VESSELNAME ");
            this.ddl_Vessel.DataBind();
            this.ddl_Vessel.Items.Insert(0, new ListItem("All", "0"));
            this.ddl_Vessel.Items[0].Value = "0";
        }
        else
        {
            this.ddl_Vessel.DataTextField = "VesselName";
            this.ddl_Vessel.DataValueField = "VesselId";
            this.ddl_Vessel.DataSource = Inspection_Master.getMasterDataforInspection("Vessel", "VesselId", "VesselName", Convert.ToInt32(Session["loginid"].ToString()));
            this.ddl_Vessel.DataBind();
            this.ddl_Vessel.Items.Insert(0, new ListItem("All", "0"));
            this.ddl_Vessel.Items[0].Value = "0";
        }
    }
    protected void BindAvailableFieldsLST()
    {
        DataTable dt11 = InspMisc_Report.GetAvailableFields();
        if (dt11.Rows.Count > 0)
        {
            this.lst_AvailFlds.DataValueField = "Column_Name";
            this.lst_AvailFlds.DataTextField = "Column_Name";
            this.lst_AvailFlds.DataSource = dt11;
            this.lst_AvailFlds.DataBind();
        }
    }
    protected void btn_Frwd_Click(object sender, EventArgs e)
    {
        for (int i = 0; i <= lst_AvailFlds.Items.Count - 1; i++)
        {
            if (lst_AvailFlds.SelectedIndex >= 0)
            {
                if (lst_AvailFlds.Items[i].Selected)
                {
                    lst_SelFlds.Items.Add(lst_AvailFlds.Items[i]);
                    lst_AvailFlds.Items.RemoveAt(lst_AvailFlds.SelectedIndex);
                    i = i - 1;
                }
            }
        }
        lst_AvailFlds.SelectedIndex = -1;
        lst_SelFlds.SelectedIndex = -1;
    }
    protected void btn_Rev_Click(object sender, EventArgs e)
    {
        for (int i = 0; i <= lst_SelFlds.Items.Count - 1; i++)
        {
            if (lst_SelFlds.SelectedIndex >= 0)
            {
                if (lst_SelFlds.Items[i].Selected)
                {
                    lst_AvailFlds.Items.Add(lst_SelFlds.Items[i]);
                    lst_SelFlds.Items.RemoveAt(lst_SelFlds.SelectedIndex);
                    i = i - 1;
                }
            }
        }
        lst_AvailFlds.SelectedIndex = -1;
        lst_SelFlds.SelectedIndex = -1;
        //BindAvailableFieldsLST();
    }
    protected void btn_Show_Click(object sender, EventArgs e)
    {
        if ((txtfromdate.Text.Trim() != "") && (txttodate.Text.Trim() != ""))
        {
            if (DateTime.Parse(txtfromdate.Text) > DateTime.Parse(txttodate.Text))
            {
                lblMessage.Text = "To Date cannot be greater than From Date.";
                return;
            }
        }
        //Show_Report();
        OpenDataFile();
    }
    public void Show_Report()
    {
        this.ReportViewer1.LocalReport.DataSources.Clear();
        this.ReportViewer1.LocalReport.LoadReportDefinition(m_rdl);
        this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("MyData", m_dataSet.Tables[0]));
        Session["Data_Table"] = m_dataSet.Tables[0];
        this.ReportViewer1.LocalReport.Refresh();
        //this.ReportViewer1.LocalReport.Dispose();
        Button1.Visible = true;
        //Button2.Visible = true;
    }
    private void DumpRdl(MemoryStream rdl)
    {
#if DEBUG_RDLC
            using (FileStream fs = new FileStream(@"c:\test.rdlc", FileMode.Create))
            {
                rdl.WriteTo(fs);
            }
#endif
    }
    private void OpenDataFile()
    {
        try
        {
            m_dataSet = new DataSet();
            if (lst_SelFlds.Items.Count <= 0)
            {
                lblMessage.Text = "Please Select the Fields.";
                lst_AvailFlds.Focus();
                return;
            }
            else
            {
                for (int i = 0; i <= lst_SelFlds.Items.Count - 1; i++)
                {
                    if (strSelFields == "")
                        strSelFields = lst_SelFlds.Items[i].Value;
                    else
                        strSelFields = strSelFields + " , " + lst_SelFlds.Items[i].Value;
                }
            }
            DataTable dt44 = InspMisc_Report.SelectInspMiscDetails_1(Convert.ToInt32(ddl_InspGroup.SelectedValue), Convert.ToInt32(ddl_InspName.SelectedValue), Convert.ToInt32(ddl_Owner.SelectedValue), Convert.ToInt32(ddlFleet.SelectedValue), Convert.ToInt32(ddl_Vessel.SelectedValue), txtfromdate.Text.Trim(), txttodate.Text.Trim(), strSelFields);
            m_dataSet.Tables.Add(dt44);

            List<string> allFields = GetAvailableFields();
            ReportOptionsDialog(allFields);
            List<string> selectedFields = GetSelectedFields();

            if (m_rdl != null)
                m_rdl.Dispose();

            m_rdl = GenerateRdl(allFields, allFields);
            //DumpRdl(m_rdl);
            Show_Report();
        }
        catch (Exception ex)
        {
            lblMessage.Text = "No Record Found." +ex.Message;
        }
    }
    private List<string> GetAvailableFields()
    {
        DataTable dataTable = m_dataSet.Tables[0];
        List<string> availableFields = new List<string>();
        for (int i = 0; i < dataTable.Columns.Count; i++)
        {
            availableFields.Add(dataTable.Columns[i].ColumnName);
        }
        return availableFields;
    }
    public void ReportOptionsDialog(List<string> availableFields)
    {
        //lst_SelFlds.Items.Clear();
        //foreach (string field in availableFields)
        //    lst_SelFlds.Items.Add(field);
    }
    public List<string> GetSelectedFields()
    {
        List<string> fields = new List<string>();
        for (int i = 0; i <= lst_SelFlds.Items.Count - 1; i++)
        {
            fields.Add(lst_SelFlds.Items[i].Value);
        }
        return fields;
    }
    private MemoryStream GenerateRdl(List<string> allFields, List<string> selectedFields)
    {
        MemoryStream ms = new MemoryStream();
        RdlGenerator gen = new RdlGenerator();
        gen.AllFields = allFields;
        gen.SelectedFields = selectedFields;
        gen.WriteXml(ms);
        ms.Position = 0;
        return ms;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        //Response.Redirect("InspMiscXLS.aspx");
        if (Session["Data_Table"] != null)
        {
            ExportDatatableToXLS(Response, (DataTable)Session["Data_Table"], "InspectionMiscellaneous");
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