using System;
using System.Data;
using System.Configuration;
using System.Reflection;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class MRV_Home : System.Web.UI.Page
{
    public string VesselCode
    {
        get { return ViewState["VesselCode"].ToString(); }
        set { ViewState["VesselCode"] = value.ToString(); }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        Session["MM"] = 0;
        if (!Page.IsPostBack)
        {
            BindVoyageReport();
        }
    }

    // Events ---------------------------------------------------------------------
    protected void lnlVesselNameOpenVoyagePop_OnClick(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        litVesselName.Text = btn.Text;
        VesselCode = btn.CommandArgument;
        BindAllVoyageByVessel(VesselCode);
        divAllVoyageByVessel.Visible = true;
        dvModal.Visible = true;
    }
    protected void btnCloseVoyagePopup_Click(object sender, EventArgs e)
    {
        divAllVoyageByVessel.Visible = false;
        dvModal.Visible = false;
    }
    protected void btnSearch_OnClick(object sender, EventArgs e)
    {
        BindAllVoyageByVessel(VesselCode);
    }
    protected void btnClear_OnClick(object sender, EventArgs e)
    {
        txtFVoyageNo.Text = "";
        txtFPeriodFrom.Text = "";
        txtFPeriodTo.Text = "";
        ddlFCondition.SelectedIndex = 0;
        ddlFEUVoyage.SelectedIndex = 0;
        BindAllVoyageByVessel(VesselCode);
    }
    

    // Function ---------------------------------------------------------------------
    public void BindVoyageReport()
    {
        string  sql = " SELECT V.VesselId,V.VesselName as Name  "+
              "  ,tbl.VoyageId,tbl.VesselCode,tbl.VoyageNo,tbl.FromPort,tbl.FromPort_EU,tbl.ToPort,tbl.ToPort_EU,tbl.Condition,tbl.StartDate,tbl.EndDate " +

              " ,Case when tbl.Condition='B' then 'Ballast' when tbl.Condition='L' then 'Laden'  else '' end ConditionText " +

              "  ,EU =case when FromPort_EU = 1 or ToPort_EU = 1 then ' Yes' else 'No' End " +
              "          FROM dbo.Vessel V " +
              "  left join " +
              "  ( " +
              "      Select * from DBO.MRV_Voyage1 Voy where voy.VoyageId = (Select MAX(VoyageId) from DBO.MRV_Voyage1 where VesselCode = Voy.VesselCode ) " +
              "  )tbl on tbl.VesselCode = V.VesselCode " +
              "  WHERE VesselStatusId <> 2  and V.VesselId in (Select VesselId from UserVesselRelation with(nolock) where Loginid = "+ Convert.ToInt32(Session["loginid"].ToString()) + ") " +
              "  order by VesselName ";

        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
        rptData.DataSource = dt;
        rptData.DataBind();
    }
    public void BindAllVoyageByVessel(string VesselCode)
    {
        string sqlWhere = "";
        string sql = "  Select * from ( "+
                     "  SELECT V.VesselId,V.VesselName as Name,v.VesselStatusId " +
                     "  ,Voy.VoyageId,Voy.VesselCode,Voy.VoyageNo,Voy.FromPort,Voy.FromPort_EU,Voy.ToPort,Voy.ToPort_EU,Voy.Condition,Voy.StartDate,Voy.EndDate " +
                     "  ,EU =case when FromPort_EU = 1 or ToPort_EU = 1 then 'Yes' else 'No' End " +
                     "  FROM dbo.Vessel V " +
                     "  left join DBO.MRV_Voyage1 Voy on Voy.VesselCode = V.VesselCode " +
                     "   )tbl WHERE tbl.VesselStatusId <> 2   and tbl.VesselCode='" + VesselCode + "' and tbl.VesselId in (Select VesselId from UserVesselRelation with(nolock) where Loginid = " + Convert.ToInt32(Session["loginid"].ToString()) + ")  ";
        if (txtFVoyageNo.Text.Trim() != "")
        {
            sqlWhere = sqlWhere + " and tbl.VoyageNo like'%" + txtFVoyageNo.Text.Trim() + "%' ";
        }

        if (txtFPeriodFrom.Text.Trim() != "" || txtFPeriodTo.Text.Trim() != "")
        {
            if (txtFPeriodFrom.Text.Trim() != "" && txtFPeriodTo.Text.Trim() != "")
            {
                sqlWhere = sqlWhere + " and ( tbl.StartDate>='" + txtFPeriodFrom.Text.Trim() + "' and  tbl.StartDate<='" + txtFPeriodTo.Text.Trim() + "')  ";
            }
            else if (txtFPeriodFrom.Text.Trim() != "")
            {
                sqlWhere = sqlWhere + " and  tbl.StartDate>='" + txtFPeriodFrom.Text.Trim() + "' ";
            }
            else if (txtFPeriodTo.Text.Trim() != "")
            {
                sqlWhere = sqlWhere + " and tbl.StartDate <= '" + txtFPeriodTo.Text.Trim() + "' ";
                
            }
        }

        if(ddlFCondition.SelectedIndex!=0)
        {
            sqlWhere = sqlWhere + " and tbl.Condition='" + ddlFCondition.SelectedValue+"' ";
        }
        if (ddlFEUVoyage.SelectedIndex != 0)
        {
            sqlWhere = sqlWhere + " and tbl.EU='" + ddlFEUVoyage.SelectedValue + "' ";
        }


        sql = sql + sqlWhere + " order by tbl.Name ";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
        rptAllVoyage.DataSource = dt;
        rptAllVoyage.DataBind();
    }
    public string FormatDate(object dt)
    {
        try
        {
            return Convert.ToDateTime(dt).ToString("dd-MMM-yyyy HH:mm");
        }
        catch
        { return ""; }
    }

}
