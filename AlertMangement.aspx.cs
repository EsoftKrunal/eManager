using System;
using System.Data;
using System.Configuration;
using System.Reflection;
using System.Collections;
using System.Web;
using System.Data.Common;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text; 
using System.IO;

public partial class AlertMangement : System.Web.UI.Page
{
    int UserId = 0; 
    public int AlertId
    {
        get { return Common.CastAsInt32(ViewState["AlertId"]); }
        set { ViewState["AlertId"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        if (Session["UserName"] == null)
        {
            Response.Redirect("Login.aspx");
        }
        UserId = int.Parse(Session["UserId"].ToString());
        if (!IsPostBack)
        {
            LoadAlerts();
            LoadVesselPositions();
            //rptOP.DataSource = Common.Execute_Procedures_Select_ByQuery("SELECT OFFICENAME,POSITIONNAME,POSITIONID FROM DBO.position p inner join DBO.office o on o.officeid=p.officeid order by OFFICENAME,POSITIONNAME");
            //rptOP.DataBind();
        }
    }     
    protected void LoadAlerts()
    {
        DataTable dt=Common.Execute_Procedures_Select_ByQuery("SELECT * FROM tbl_AlertTypes");
        rptItems.DataSource = dt;
        rptItems.DataBind();
    }
    protected void LoadVesselPositions()
    {
        rptEditVP.DataSource = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.VesselPositions order by POSITIONNAME");
        rptEditVP.DataBind();
    } 
    protected void btnShowVesselPositions_Click(object sender, EventArgs e)
    {
        AlertId = Common.CastAsInt32(txtAlertTypeId.Text.Trim());

        rptVP.DataSource = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.VesselPositions WHERE VPID in (SELECT RESULT FROM dbo.CSVtoTable((SELECT [VesselRanks] FRom [dbo].[tbl_AlertTypes] WHERE [AlertTypeId] = " + txtAlertTypeId.Text.Trim() + "),','))  order by POSITIONNAME");
        rptVP.DataBind();
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        AlertId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        string AlertDays = ((ImageButton)sender).CssClass;

        txtDays.Text = AlertDays;

        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT RESULT FROM dbo.CSVtoTable((SELECT [VesselRanks] FRom [dbo].[tbl_AlertTypes] WHERE [AlertTypeId] = " + AlertId + "),',')");

        foreach (DataRow dr in dt.Rows)
        {
            foreach (RepeaterItem itm in rptEditVP.Items)
            {
                CheckBox chkselect = (CheckBox)itm.FindControl("chkselect");
                int VPId = Common.CastAsInt32(chkselect.Attributes["VPId"]);

                if (VPId == Common.CastAsInt32(dr["RESULT"]))
                {
                    chkselect.Checked = true;
                }    
            }
        }

        dv_EditAlert.Visible = true;

    }     
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (txtDays.Text.Trim() == "")
        {
            lblMsg.Text = "Please enter Days.";
            txtDays.Focus();
            return;
        }
        int Days;
        if (!int.TryParse(txtDays.Text.Trim(), out Days))
        {
            lblMsg.Text = "Please enter valid Days.";
            txtDays.Focus();
            return;
        }
        string VPId = "";

        try
        {
            foreach (RepeaterItem itm in rptEditVP.Items)
            {
                CheckBox chkselect = (CheckBox)itm.FindControl("chkselect");
                string _VPId = chkselect.Attributes["VPId"].ToString().Trim();

                if (chkselect.Checked)
                {
                    VPId = VPId + _VPId + ",";
                }
            }

            if (VPId.Trim().Length > 0)
            {
                VPId = VPId.TrimEnd(',');
            }

            string SQL = "UPDATE tbl_AlertTypes SET AlertDays = " + txtDays.Text.Trim() + ", VesselRanks = '" + VPId + "' WHERE AlertTypeId = " + AlertId;
            Common.Execute_Procedures_Select_ByQuery(SQL);

            lblMsg.Text = "Record updated successfully.";
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Unable to update record. Error : " + ex.Message;
        }
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        txtDays.Text = "";

        foreach (RepeaterItem itm in rptEditVP.Items)
        {
            CheckBox chkselect = (CheckBox)itm.FindControl("chkselect");
            chkselect.Checked = false;
        }

        LoadAlerts();
        rptVP.DataSource = null;
        rptVP.DataBind();
        //btnShowVesselPositions_Click(sender,e);
        dv_EditAlert.Visible = false;
    }
     
}
