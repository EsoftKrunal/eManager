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

public partial class Requisitions : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //---------------------------------------
        ProjectCommon.SessionCheck_New();
        //---------------------------------------
        #region --------- USER RIGHTS MANAGEMENT -----------
        try
        {
            AuthenticationManager auth = new AuthenticationManager(1061, Convert.ToInt32(Session["loginid"].ToString()), ObjectType.Page);
            if (!(auth.IsView))
            {
                Response.Redirect("~/AuthorityError.aspx");
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("~/NoPermission.aspx?Message=" + ex.Message);
        }
        #endregion ----------------------------------------
        if (!Page.IsPostBack)
        {
            BindFleet();
            BindVessel();
            btnShow_Click(sender, e);
        }
    }

    #region ----------- USER DEFINED FUNCTIONS --------------------
    public void BindFleet()
    {
        try
        {
            DataTable dtFleet = Common.Execute_Procedures_Select_ByQuery("SELECT FleetId,FleetName as Name FROM dbo.FleetMaster");
            this.ddlFleet.DataSource = dtFleet;
            this.ddlFleet.DataValueField = "FleetId";
            this.ddlFleet.DataTextField = "Name";
            this.ddlFleet.DataBind();
            ddlFleet.Items.Insert(0, new ListItem("< All >", "0"));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public void BindVessel()
    {
        string SQL = "SELECT [VesselId],[VesselName],[VesselCode] FROM [dbo].[Vessel] WHERE VESSELSTATUSID=1 and VesselId in (Select VesselId from UserVesselRelation with(nolock) where Loginid = " + Convert.ToInt32(Session["loginid"].ToString())+") ORDER BY VesselName ";
        DataTable dtVessel = Common.Execute_Procedures_Select_ByQuery(SQL);
        if (dtVessel != null && dtVessel.Rows.Count > 0)
        {
            ddlVessel.DataSource = dtVessel;
            ddlVessel.DataTextField = "VesselName";
            ddlVessel.DataValueField = "VesselCode";
            ddlVessel.DataBind();
        }

        ddlVessel.Items.Insert(0, new ListItem("< All >", "0"));

    }

    #endregion --------------------------------------------------

    #region ----------- EVENTS --------------------

    protected void btnShow_Click(object sender, EventArgs e)
    {
        string WhereCond = "WHERE 1=1 ";
        string VesselCodes = "";

        //if (ddlVessel.SelectedIndex != 0)
        //{
        //    WhereCond = WhereCond + " AND [VesselCode] = '" + ddlVessel.SelectedValue.Trim() + "' ";
        //}

        if (ddlVessel.SelectedIndex == 0)
        {
            foreach (ListItem vsl in ddlVessel.Items)
            {
                if (vsl.Value.Trim() != "")
                {
                    VesselCodes += "," + "'" +vsl.Value + "'";
                }
            }
            if (VesselCodes.Trim() != "") { VesselCodes = VesselCodes.Substring(1); }
        }
        else
        {
            VesselCodes = "'" + ddlVessel.SelectedValue + "'";
        }

        WhereCond = WhereCond + " AND [VesselCode] IN (" + VesselCodes.Trim() + ") ";


        if (txtFrom.Text.Trim() != "" && txtTo.Text.Trim() != "")
        {
            WhereCond = WhereCond + " AND ([ETA] >= '" + txtFrom.Text.Trim() + "' AND [ETA] <= '" + txtTo.Text.Trim() + "') ";
        }
        if (txtFrom.Text.Trim() != "")
        {
            WhereCond = WhereCond + " AND [ETA] >= '" + txtFrom.Text.Trim() + "' ";
        }
        if (txtTo.Text.Trim() != "")
        {
            WhereCond = WhereCond + " AND [ETA] <= '" + txtTo.Text.Trim() + "' ";
        }
        if (ddlStatus.SelectedIndex > 0)
        {
            if (ddlStatus.SelectedValue == "I")
                WhereCond = WhereCond + " AND [ActiveInActive] = 'I' ";
            else
                WhereCond = WhereCond + " AND ( [Status] = '" + ddlStatus.SelectedValue.Trim() + "' and [ActiveInActive] <> 'I') ";
        }
        if (ddlType.SelectedIndex != 0)
        {
            WhereCond = WhereCond + " AND [Type] like '" + ddlType.SelectedValue.Trim() + "%' ";
        }

        string SQLSearch = "SELECT * FROM ( " +
                           "SELECT [StoreReqId] As Id, 'Store' As Type,(V.[VesselCode] + ' - ' + V.[VesselName]) As Vessel,V.[VesselCode] AS [VesselCode],[ReqnNo],[Port],REPLACE(Convert(varchar(11), [ETA], 106), ' ', '-') AS [ETA],CASE WHEN LEN([Remarks]) > 50 THEN Substring([Remarks], 0, 50) + '...' ELSE [Remarks] END AS [Remarks],CASE WHEN ISNULL(ActiveInactive,'A')='I' THEN 'Inactive' WHEN [Status] = 'N' THEN 'New Requisition' WHEN [Status] = 'T' THEN 'Sent for RFQ'  ELSE '' END AS [Status1], [Status], ISNULL(ActiveInactive,'A') AS ActiveInactive " +
                           ",Row_Status=(CASE WHEN ISNULL(STATUS,'N')='T' THEN 'cls_T' WHEN ISNULL(ActiveInactive,'A')='I' THEN 'cls_I' ELSE 'cls_N' end) " +
                           ",(SELECT FirstName + ' ' + LastName FROM [dbo].[UserMaster] WHERE [LoginId] = PM.TransferedBy) AS TransferedBy,TransferedOn " +
                           "FROM  [dbo].[MP_VSL_StoreReqMaster] PM " +
                           "INNER JOIN [dbo].[Vessel] V ON PM.[VesselCode] = V.[VesselCode] " +
                           "UNION " +
                           "SELECT [SpareReqId] As Id, 'Spare' As Type,(V.[VesselCode] + ' - ' + V.[VesselName]) As Vessel,V.[VesselCode] AS [VesselCode],[ReqnNo],[Port],REPLACE(Convert(varchar(11), [ETA], 106), ' ', '-') AS [ETA],CASE WHEN LEN([Remarks]) > 50 THEN Substring([Remarks], 0, 50) + '...' ELSE [Remarks] END AS [Remarks],CASE WHEN ISNULL(ActiveInactive,'A')='I' THEN 'Inactive' WHEN [Status] = 'N' THEN 'New Requisition' WHEN [Status] = 'T' THEN 'Sent for RFQ' ELSE '' END AS [Status1], [Status], ISNULL(ActiveInactive,'A') AS ActiveInactive " +
                           ",Row_Status=(CASE WHEN ISNULL(STATUS,'N')='T' THEN 'cls_T' WHEN ISNULL(ActiveInactive,'A')='I' THEN 'cls_I' ELSE 'cls_N' end)  " +
                           ",(SELECT FirstName + ' ' + LastName FROM [dbo].[UserMaster] WHERE [LoginId] = PM.TransferedBy) AS TransferedBy,TransferedOn " +
                           "FROM  [dbo].[MP_VSL_SparesReqMaster] PM  " +
                           "INNER JOIN [dbo].[Vessel] V ON PM.[VesselCode] = V.[VesselCode] " +

                           "UNION "+
                           "SELECT[StoreReqId] As Id, 'StoreNew' As Type,(V.[VesselCode] + ' - ' + V.[VesselName]) As Vessel, V.[VesselCode] AS[VesselCode],[ReqnNo],[Port], REPLACE(Convert(varchar(11), [ETA], 106), ' ', '-') AS[ETA], CASE WHEN LEN([Remarks]) > 50 THEN Substring([Remarks], 0, 50) +'...' ELSE[Remarks] END AS[Remarks], CASE WHEN ISNULL(ActiveInactive,'A')= 'I' THEN 'Inactive' WHEN[Status] = 'N' THEN 'New Requisition' WHEN[Status] = 'T' THEN 'Sent for RFQ'  ELSE '' END AS[Status1], [Status], ISNULL(ActiveInactive,'A') AS ActiveInactive " +
                           ", Row_Status = (CASE WHEN ISNULL(STATUS,'N')= 'T' THEN 'cls_T' WHEN ISNULL(ActiveInactive,'A')= 'I' THEN 'cls_I' ELSE 'cls_N' end)  " +
                           ",(SELECT FirstName + ' ' + LastName FROM [dbo].[UserMaster] WHERE[LoginId] = PM.TransferedBy) AS TransferedBy, TransferedOn " +
                           "FROM [dbo].[MP_VSL_StoreReqMaster1] PM " +
                           "INNER JOIN [dbo].[Vessel] V ON PM.[VesselCode] = V.[VesselCode] " +

                           " )a " + WhereCond + " ORDER BY [ReqnNo] DESC  ";

        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQLSearch);

        rptManageMenu.DataSource = dt;
        rptManageMenu.DataBind();

    }

    protected void ddlFleet_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlFleet.SelectedIndex != 0)
        {
            DataTable dtVessels = new DataTable();
            string strvessels = "SELECT VesselId,VesselCode,VesselName FROM  dbo.Vessel WHERE FleetId = " + ddlFleet.SelectedValue + " AND VESSELSTATUSID=1  ORDER BY VesselName";
            dtVessels = Common.Execute_Procedures_Select_ByQuery(strvessels);
            ddlVessel.Items.Clear();
            if (dtVessels.Rows.Count > 0)
            {
                ddlVessel.DataSource = dtVessels;
                ddlVessel.DataTextField = "VesselName";
                ddlVessel.DataValueField = "VesselCode";
                ddlVessel.DataBind();
            }
            else
            {
                ddlVessel.DataSource = null;
                ddlVessel.DataBind();
            }
            ddlVessel.Items.Insert(0, "< All >");
        }
        else
        {
            ddlVessel.Items.Clear();
            BindVessel();
        }
    }

    #endregion --------------------------------------------------
    
}

   