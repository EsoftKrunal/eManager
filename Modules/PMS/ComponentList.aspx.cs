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

public partial class ComponentList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // -------------------------- SESSION CHECK ----------------------------------------------
        ProjectCommon.SessionCheck();
        // -------------------------- SESSION CHECK END ----------------------------------------------

        lblMessage.Text = "";
        #region --------- USER RIGHTS MANAGEMENT -----------
        try
        {
            AuthenticationManager auth = new AuthenticationManager(206, int.Parse(Session["loginid"].ToString()), ObjectType.Page);
            if (!(auth.IsView))
            {
                Response.Redirect("~/NoPermission.aspx", false);
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("~/NoPermission.aspx?Message=" + ex.Message);
        }
        #endregion ----------------------------------------

        if (!Page.IsPostBack)
        {
            BindVessels();
        }
    }

    #region ----------- USER DEFINED FUNCTIONS --------------------
    private void BindVessels()
    {
        DataTable dtVessels = new DataTable();
        string strvessels = "SELECT VesselId,VesselCode,VesselName FROM dbo.Vessel ORDER BY VesselName";
        dtVessels = Common.Execute_Procedures_Select_ByQuery(strvessels);
        if (dtVessels.Rows.Count > 0)
        {
            ddlVessels.DataSource = dtVessels;
            ddlVessels.DataTextField = "VesselName";
            ddlVessels.DataValueField = "VesselCode";
            ddlVessels.DataBind();
        }
        else
        {
            ddlVessels.DataSource = null;
            ddlVessels.DataBind();
        }

        ddlVessels.Items.Insert(0, "< SELECT >");
    }
    private void BindComponents()
    {
        DataTable dtGroups = new DataTable();
        string strSQL = "SELECT ComponentCode, ComponentName,Descr FROM ComponentMaster where LEN(LTRIM(RTRIM(ComponentCode)))=2 Order By ComponentCode";
        dtGroups = Common.Execute_Procedures_Select_ByQuery(strSQL);
        tvComponents.Nodes.Clear();
        if (dtGroups.Rows.Count > 0)
        {
            for (int i = 0; i < dtGroups.Rows.Count; i++)
            {
                TreeNode gn = new TreeNode();
                gn.Text = dtGroups.Rows[i]["ComponentCode"].ToString() + " : " + dtGroups.Rows[i]["ComponentName"].ToString();
                gn.Value = dtGroups.Rows[i]["ComponentCode"].ToString();
                DataTable dtSystems;
                String strQuery = "SELECT ComponentCode, ComponentName,Descr FROM ComponentMaster where LEN(LTRIM(RTRIM(ComponentCode)))=4 AND LEFT(ComponentCode,2)='" + gn.Value.Trim() + "' Order By ComponentCode";
                dtSystems = Common.Execute_Procedures_Select_ByQuery(strQuery);
                if (dtSystems != null)
                {
                    for (int j = 0; j < dtSystems.Rows.Count; j++)
                    {
                        TreeNode sn = new TreeNode();
                        sn.Text = dtSystems.Rows[j]["ComponentCode"].ToString() + " : " + dtSystems.Rows[j]["ComponentName"].ToString();
                        sn.Value = dtSystems.Rows[j]["ComponentCode"].ToString();
                        sn.Expanded = true;
                        DataTable dtSubSystems;
                        string strSubSystems = "SELECT ComponentCode, ComponentName,Descr FROM ComponentMaster where LEN(LTRIM(RTRIM(ComponentCode)))=6 AND LEFT(ComponentCode,4)='" + sn.Value.Trim() + "' Order By ComponentCode";
                        dtSubSystems = Common.Execute_Procedures_Select_ByQuery(strSubSystems);
                        if (dtSubSystems != null)
                        {
                            for (int k = 0; k < dtSubSystems.Rows.Count; k++)
                            {
                                TreeNode ssn = new TreeNode();
                                ssn.Text = dtSubSystems.Rows[k]["ComponentCode"].ToString() + " : " + dtSubSystems.Rows[k]["ComponentName"].ToString();
                                ssn.Value = dtSubSystems.Rows[k]["ComponentCode"].ToString();
                                ssn.Expanded = true;
                                sn.ChildNodes.Add(ssn);
                            }
                        }
                        gn.ChildNodes.Add(sn);
                    }
                }
                gn.Expanded = true;
                tvComponents.Nodes.Add(gn);
            }
        }
    }
    #endregion ----------------------------------------------------

    #region ------------ EVENTS -----------------------------------
    protected void ddlVessels_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlVessels.SelectedIndex != 0)
        {
            BindComponents();
        }
        else
        {
            tvComponents.Nodes.Clear();
            rptComponentUnits.DataSource = null;
            rptComponentUnits.DataBind();
        }
    }
    protected void tvComponents_SelectedNodeChanged(object sender, EventArgs e)
    {
        if (tvComponents.SelectedNode.Value.Trim().Length != 2)
        {
            DataTable dtUnitsDetails;
            string strSQL;
            if (tvComponents.SelectedNode.Value.Trim().Length == 4)
            {
                strSQL = "SELECT LTRIM(RTRIM(CM.ComponentCode)) + ' : ' + CM.ComponentName  AS ComponentName,JM.JobName FROM JobMaster JM INNER JOIN VesselComponentJobMaster VJM ON JM.JobId = VJM.JobId " +
                         "INNER JOIN ComponentMaster CM ON CM.ComponentId = VJM.VesselComponentId " +
                         "WHERE VJM.VesselCode = '" + ddlVessels.SelectedValue.Trim() + "' AND CM.ComponentCode = '" + tvComponents.SelectedNode.Value.Trim() + "'";
            }
            else
            {
                strSQL = "SELECT LTRIM(RTRIM(VCM.VesselComponentCode))+ ' : ' + VCM.ComponentName AS ComponentName ,JM.JobName FROM VesselComponentMaster VCM " +
                         "INNER JOIN ComponentMaster CM ON VCM.ComponentId = CM.ComponentId " +
                         "INNER JOIN VesselComponentJobMaster VJM ON VCM.VesselCode = VJM.VesselCode AND VCM.VesselComponentId = VJM.VesselComponentId " +
                         "INNER JOIN JobMaster JM ON JM.JobId = VJM.JobId " +
                         "WHERE VCM.VesselCode = '" + ddlVessels.SelectedValue.Trim() + "' AND CM.ComponentCode = '" + tvComponents.SelectedNode.Value.Trim() + "'";
            }
            dtUnitsDetails = Common.Execute_Procedures_Select_ByQuery(strSQL);
            if (dtUnitsDetails.Rows.Count > 0)
            {
                string ComponentCode = dtUnitsDetails.Rows[0]["ComponentName"].ToString();
                for (int i = 0; i <= dtUnitsDetails.Rows.Count - 1; i++)
                {
                    if (i == 0)
                    {
                    }
                    else
                    {
                        if (ComponentCode == dtUnitsDetails.Rows[i]["ComponentName"].ToString())
                        {
                            dtUnitsDetails.Rows[i]["ComponentName"] = "";
                        }
                        else
                        {
                            ComponentCode = dtUnitsDetails.Rows[i]["ComponentName"].ToString();
                        }
                    }
                }  
                dtUnitsDetails.AcceptChanges();
                rptComponentUnits.DataSource = dtUnitsDetails;
                rptComponentUnits.DataBind();
            }
            else
            {
                rptComponentUnits.DataSource = null;
                rptComponentUnits.DataBind();
            }
        }
        else
        {
            rptComponentUnits.DataSource = null;
            rptComponentUnits.DataBind();
        }
    }
    #endregion ----------------------------------------------------
   
}
