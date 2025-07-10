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

public partial class Vetting_Vetting_Performance : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {   
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        if (!IsPostBack)
        {
            BindOwner();
            BindFleet();
            BindVessel();
            BindYear();
            BindInspection();
        }
    }
    protected void BindFleet()
    {
        ddl_fleet.DataTextField = "FleetName";
        ddl_fleet.DataValueField = "FleetId";
        ddl_fleet.DataSource = Budget.getTable("SELECT * FROM DBO.FLEETMASTER ORDER BY FLEETNAME");
        ddl_fleet.DataBind();
        ddl_fleet.Items.Insert(0, new ListItem("All", "0"));
        ddl_fleet.Items[0].Value = "0";
    }
    protected void BindOwner()
    {
        ddl_owner.DataTextField = "OwnerName";
        ddl_owner.DataValueField = "OwnerId";
        ddl_owner.DataSource = Inspection_Master.getMasterDataforInspection("Owner", "OwnerId", "OwnerName");
        ddl_owner.DataBind();
        ddl_owner.Items.Insert(0, new ListItem("All", "0"));
        ddl_owner.Items[0].Value = "0";
    }
    protected void BindInspection()
    {
        ddlInspection.DataTextField = "Name";
        ddlInspection.DataValueField = "Id";
        ddlInspection.DataSource = Common.Execute_Procedures_Select_ByQuery("select * from m_Inspection where InspectionGroup=1");
        ddlInspection.DataBind();
        ddlInspection.Items.Insert(0, new ListItem("All", "0"));
        ddlInspection.Items[0].Value = "0";
    }
    
    protected void rad_Owner_fleet_OnCheckedChanged(object sender, EventArgs e)
    {
        ddl_owner.Visible = rad_Owner.Checked;
        ddl_fleet.Visible = rad_Fleet.Checked;
        BindVessel();
    }
    protected void ddl_owner_fleet_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        BindVessel();
    }
        
    protected void BindVessel()
    {
        try
        {
            DataTable dt;
            if (ddl_owner.Visible)
            {
                if(ddl_owner.SelectedIndex > 0)
                    dt = Common.Execute_Procedures_Select_ByQuery("select VESSELNAME,VESSELID FROM dbo.vessel where ownerid=" + ddl_owner.SelectedValue + " and VesselStatusid<>2  and vesseltypeid not in (23,28) " + " order by vesselname");
                else
                    dt = Common.Execute_Procedures_Select_ByQuery("select VESSELNAME,VESSELID FROM dbo.vessel where VesselStatusid<>2  and vesseltypeid not in (23,28) " + " order by vesselname");
            }
            else
            {
                if (ddl_fleet.SelectedIndex > 0)
                    dt = Common.Execute_Procedures_Select_ByQuery("select VESSELNAME,VESSELID FROM dbo.vessel where fleetid=" + ddl_fleet.SelectedValue + " and VesselStatusid<>2  and vesseltypeid not in (23,28)  order by vesselname");
                else
                    dt = Common.Execute_Procedures_Select_ByQuery("select VESSELNAME,VESSELID FROM dbo.vessel where VesselStatusid<>2  and vesseltypeid not in (23,28) order by vesselname");
            }

            ddlVessel.Controls.Clear();
            ddlVessel.DataTextField = "VesselName";
            ddlVessel.DataValueField = "VesselId";
            ddlVessel.DataSource = dt;
            ddlVessel.DataBind();
            ddlVessel.Items.Insert(0, new ListItem("All", "0"));
            ddlVessel.Items[0].Value = "0";
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void BindYear()
    {
        try
        {
           for(int i=DateTime.Today.Year;i>=2008;i--)
           {
               ddlYear.Items.Add(new ListItem(i.ToString(),i.ToString()));
           }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        ShowReport("C");
    }
    protected void ShowReport(string Mode)
    {
        string Critera = "";

        int InspectionGroupId = 0;
        string ReportType = "";

        if(RadioButton1.Checked)
        {
            InspectionGroupId = 1;
            ReportType = "S1";
        }
        if (RadioButton2.Checked)
        {
            InspectionGroupId = 1;
            ReportType = "S2";
        }
        if (RadioButton3.Checked)
        {
            InspectionGroupId = 1;
            ReportType = "S3";
        }
        if (RadioButton4.Checked)
        {
            InspectionGroupId = 1;
            ReportType = "S4";
        }
        if (RadioButton5.Checked)
        {
            InspectionGroupId = 1;
            ReportType = "S5";
        }
        if (RadioButton6.Checked)
        {
            InspectionGroupId = 1;
            ReportType = "S6";
        }
        if (RadioButton7.Checked)
        {
            InspectionGroupId = 1;
            ReportType = "S7";
        }
        //===============
        if (RadioButton11.Checked)
        {
            InspectionGroupId = 2;
            ReportType = "S1";
        }
        if (RadioButton12.Checked)
        {
            InspectionGroupId = 2;
            ReportType = "S2";
        }
        if (RadioButton13.Checked)
        {
            InspectionGroupId = 2;
            ReportType = "S3";
        }
        if (RadioButton14.Checked)
        {
            InspectionGroupId = 2;
            ReportType = "S4";
        }
        if (RadioButton15.Checked)
        {
            InspectionGroupId = 2;
            ReportType = "S5";
        }
        if (RadioButton16.Checked)
        {
            InspectionGroupId = 2;
            ReportType = "S6";
        }
        if (RadioButton17.Checked)
        {
            InspectionGroupId = 2;
            ReportType = "S7";
        }
        //===============
        string VesselIds = "";

        if (ddlVessel.SelectedIndex > 0)
        {
            VesselIds = ddlVessel.SelectedValue;
            Critera = "Vessel : " + ddlVessel.SelectedItem.Text;
        }
        else
        {
            //string SQl = "SELECT VESSELID FROM DBO.VESSEL WHERE  VESSELSTATUSID=1 ";

            //if (ddl_owner.Visible && ddl_owner.SelectedIndex > 0)
            //    SQl += " AND OWNERID=" + ddl_owner.SelectedValue;
            //else 
            //    if(ddl_fleet.SelectedIndex > 0)
            //        SQl += " AND FLEETID=" + ddl_fleet.SelectedValue;

            //DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQl);

            foreach (ListItem li in ddlVessel.Items)
            {
                if (Common.CastAsInt32(li.Value) > 0)
                    VesselIds += "," + li.Value;
            }
            if (VesselIds.StartsWith(","))
                VesselIds = VesselIds.Substring(1);

            if (ddl_owner.Visible && ddl_owner.SelectedIndex > 0)
                Critera = "Owner : " + ddl_owner.SelectedItem.Text;
            else if (ddl_fleet.Visible && ddl_fleet.SelectedIndex > 0)
                Critera = "Fleet : " + ddl_fleet.SelectedItem.Text;

        }
        if (VesselIds.Trim() == "")
        {
            ProjectCommon.ShowMessage("There are no vessels under this company.");
            return;
        }

        frmChart.Attributes.Add("src", "chart.aspx?InspectionGroupId=" + InspectionGroupId + "&ReportType=" + ReportType + "&ReportLevel=" + ddlReportLevel.SelectedValue + "&VesselIds=" + VesselIds + "&Year=" + ddlYear.SelectedValue + "&InspectionId=" + ddlInspection.SelectedValue + "&Mode=" + Mode + "&Critera=" + Critera);
    }
    protected void btnDownload_Click(object sender, EventArgs e)
    {
        ShowReport("D");
    }
}
