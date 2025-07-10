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
using System.Collections.Generic;

public class VoyageType
{
    public string VoyageNo { get; set; }
    public int Type { get; set; }
}
public partial class MRV_ReportFilter : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        Session["MM"] = 4;
        if (!Page.IsPostBack)
        {
            BindVessel();
            BindYear();
        }
    }

    // Events ---------------------------------------------------------------------
    
    protected void btnCloseVoyagePopup_Click(object sender, EventArgs e)
    {
        
        dvModal.Visible = false;
    }
    protected void btnSearch_OnClick(object sender, EventArgs e)
    {
        if (ddlVessel.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "ssss11", "alert('Please select any vessel.')", true);
            return;
        }
        BindAllVoyageByVessel();
    }
    protected void btnClear_OnClick(object sender, EventArgs e)
    {
        //txtFVoyageNo.Text = "";
        //txtFPeriodFrom.Text = "";
        //txtFPeriodTo.Text = "";
        //ddlFCondition.SelectedIndex = 0;
        ddlYear.SelectedIndex = 0;
        
        BindAllVoyageByVessel();
    }
    protected void btnReport_OnClick(object sender, EventArgs e)
    {
        if (ddlVessel.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "ssss1", "alert('Please select any vessel.')", true);
            return;
        }
        string voaygeIDs = "";
        string types = "";
        foreach (RepeaterItem itm in rptAllVoyage.Items)
        {
            HiddenField hfdVoyageID = (HiddenField)itm.FindControl("hfdVoyageID");
            RadioButtonList rdoCalType = (RadioButtonList)itm.FindControl("rdoCalType");
            voaygeIDs = voaygeIDs + "," + hfdVoyageID.Value;
            //types = types + "," + rdoCalType.SelectedValue;
            types = types + ",1";
        }
        if (voaygeIDs != "")
        {
            voaygeIDs=voaygeIDs.Substring(1);
            types=types.Substring(1);
        }

        bool res;
        Common.Set_Procedures("DBO.MRV_Report");
        Common.Set_ParameterLength(3);
        Common.Set_Parameters(
            new MyParameter("@VesselCode", ddlVessel.SelectedValue),
            new MyParameter("@voaygeIDs", voaygeIDs),
            new MyParameter("@Types", types));

        DataSet ds = new DataSet();
        res = Common.Execute_Procedures_IUD(ds);

        //return;
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "ssss", "window.open('Report.aspx?V="+ddlVessel.SelectedValue+ "&Year="+ddlYear.SelectedValue+"&EUVoyage="+ddlFEUVoyage.SelectedValue+"')", true);
    }
    


    // Function ---------------------------------------------------------------------    
    public void BindAllVoyageByVessel()
    {
        string sqlWhere = "";
        //string sql = "  Select * from ( "+
        //             "  SELECT V.VesselId,V.VesselName as Name,v.VesselStatusId " +
        //             "  ,Voy.VoyageId,Voy.VesselCode,Voy.VoyageNo,Voy.FromPort,Voy.FromPort_EU,Voy.ToPort,Voy.ToPort_EU,Voy.Condition,Voy.StartDate,Voy.EndDate,isnull(cal.CalcMode,1) as CalcMode " +
        //             "  ,EU =case when FromPort_EU = 1 or ToPort_EU = 1 then 'Yes' else 'No' End " +
        //             "  FROM dbo.Vessel V " +
        //             "  inner join DBO.MRV_Voyage Voy on Voy.VesselCode = V.VesselCode " +

        //             "  left join DBO.MRV_ReportCalc cal on cal.VesselCode = V.VesselCode and cal.VoyageID=Voy.VoyageID" +

        //             "   )tbl WHERE tbl.VesselCode='" + ddlVessel.SelectedValue + "' and tbl.EU='Yes' and Year(tbl.StartDate)="+ddlYear.SelectedValue;

        string sql = "  Select * from ( " +
                     "  SELECT V.VesselId,V.VesselName as Name,v.VesselStatusId " +
                     "  ,Voy.VoyageId,Voy.VesselCode,Voy.VoyageNo,Voy.FromPort,Voy.FromPort_EU,Voy.ToPort,Voy.ToPort_EU,Voy.Condition,Voy.StartDate,Voy.EndDate,isnull(cal.CalcMode,1) as CalcMode " +
                     "  ,EU =case when FromPort_EU = 1 or ToPort_EU = 1 then 'Yes' else 'No' End " +
                     "  FROM dbo.Vessel V " +
                     "  inner join DBO.MRV_Voyage1 Voy on Voy.VesselCode = V.VesselCode " +

                     "  left join DBO.MRV_ReportCalc cal on cal.VesselCode = V.VesselCode and cal.VoyageID=Voy.VoyageID" +

                     "   )tbl WHERE tbl.VesselCode='" + ddlVessel.SelectedValue + "' and tbl.VesselId in (Select VesselId from UserVesselRelation with(nolock) where Loginid = " + Convert.ToInt32(Session["loginid"].ToString()) + ") and Year(tbl.StartDate)=" + ddlYear.SelectedValue;
        if (ddlFEUVoyage.SelectedIndex != 0)
        {
            sqlWhere = sqlWhere + " and tbl.EU='" + ddlFEUVoyage.SelectedValue + "' ";
        }

        sql = sql + sqlWhere + " order by tbl.Name ";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
        rptAllVoyage.DataSource = dt;
        rptAllVoyage.DataBind();
    }
    public void BindVessel()
    {
        string sql = " SElect VesselCode,VesselName from dbo.Vessel  where VesselStatusID=1 and VesselId in (Select VesselId from UserVesselRelation with(nolock) where Loginid = "+ Convert.ToInt32(Session["loginid"].ToString()) + ") order by VesselName ";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
        ddlVessel.DataSource = dt;
        ddlVessel.DataTextField = "VesselName";
        ddlVessel.DataValueField = "VesselCode";
        ddlVessel.DataBind();
        ddlVessel.Items.Insert(0, new ListItem("Select", ""));
    }
    public void BindYear()
    {
        ddlYear.Items.Clear();
        for (int i = DateTime.Today.Year; i >= 2017; i--)
        {
            ddlYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
        }
        
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
