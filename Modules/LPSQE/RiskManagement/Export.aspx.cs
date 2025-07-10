using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Ionic.Zip;
using System.IO;

public partial class EventManagement_Export : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        lblMsg.Text = "";
        if (!IsPostBack)
        {
            Session["RM"] = "E";
            Load_vessel();
        }
        string[] files = Directory.GetFiles(Server.MapPath("~/Modules/LPSQE/Temp"));
        foreach (string f in files)
        {
            try { File.Delete(f); }
            catch { }
        }
    }
    protected void BindGrid()
    {
        string SQL = "SELECT M.*, VL.[VesselId],VL.[SentBy],VL.[SentOn],VL.[AckRecd],VL.[AckRecdOn] FROM [dbo].[EV_TemplateMaster] M " +
                      "INNER JOIN [dbo].[EV_Template_Vessel_Linking] VL ON VL.TemplateId = M.TemplateId " +
                      "WHERE M.[STATUS]='A' AND VL.VesselId=" + ddlVessel.SelectedValue;

        rptTemplate.DataSource = Common.Execute_Procedures_Select_ByQuery(SQL);
        rptTemplate.DataBind();
    }
    private void Load_vessel()
    {
        DataSet dt = Budget.getTable("Select * from dbo.vessel where vesselstatusid=1  and isnull(fleetid,0)>0 order by vesselname");
        ddlVessel.DataSource = dt;
        ddlVessel.DataTextField = "VesselName";
        ddlVessel.DataValueField = "VesselId";
        ddlVessel.DataBind();
        ddlVessel.Items.Insert(0, new ListItem(" < All >", "0"));
    }
    protected void ddlVessel_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGrid();
    }
    protected void btnDownload_Click(object sender, EventArgs e)
    {
        if(ddlVessel.SelectedIndex<=0)
            ScriptManager.RegisterStartupScript(this, this.GetType(), "afdf", "window.open('SendMailTemplate.aspx?Mode=ALL','');", true);
        else
            ScriptManager.RegisterStartupScript(this, this.GetType(), "afdf", "window.open('SendMailTemplate.aspx?Mode=" + ddlVessel.SelectedValue + "','');", true);
        return;
    }
}