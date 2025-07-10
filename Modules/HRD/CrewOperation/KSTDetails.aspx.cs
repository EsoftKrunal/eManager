using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Exl = Microsoft.Office.Interop.Excel;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
public partial class CrewOperation_KSTDetails : System.Web.UI.Page
{
    
    public int kstid
    {
        get
        { return Common.CastAsInt32(ViewState["kstid"]); }
        set { ViewState["kstid"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        kstid = Common.CastAsInt32(Request.QueryString["kstid"]);
        //--------------
        if(! IsPostBack)
        {
            BindKST();
        }
    }
    
    protected void BindKST()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("select *,(select vesselname from vessel v where v.VesselCode=k.incidentVesselCode) as vesselname,case when Severity=1 then 'Minor' when Severity=2 then 'Major' when Severity=3 then 'Sever' else 'NA' end accidentseverity from KST_MASTER k WHERE KST_ID=" + kstid);
        if(dt.Rows.Count>0)
        {
            lblvesselname.Text = dt.Rows[0]["vesselname"].ToString();
            lblIncidentNo.Text = dt.Rows[0]["IncidentNo"].ToString();
            lblkstno.Text = dt.Rows[0]["kst_no"].ToString();
            lblIncidentDate.Text =Common.ToDateString(dt.Rows[0]["IncidentDate"]);
            lblSeverity.Text = dt.Rows[0]["accidentseverity"].ToString();
            lblTopic.Text = dt.Rows[0]["Topic"].ToString();
            string Classification= dt.Rows[0]["Classification"].ToString();
            string[] names = { "Injury to People", "Mooring", "Security", "Cargo Contamination", "", "Equipment Failure", "", "Navigation", "Damage to Property", "Pollution", "Fire" };
            lblClasification.Text = "";
            string[] parts = Classification.Split(',');
            foreach (string s in parts)
            {
                lblClasification.Text += ", " + names[Common.CastAsInt32(s)-1];
            }
            if (lblClasification.Text.Trim() != "")
                lblClasification.Text = lblClasification.Text.Substring(1);

            rptcomments.DataSource = Common.Execute_Procedures_Select_ByQueryCMS("SELECT a.*,cpd.firstname,cpd.crewnumber,cpd.middlename,cpd.lastname,r.rankcode FROM KST_Attedance a inner join CrewPersonalDetails cpd on a.crewid=cpd.crewid inner join rank r on r.RankId=cpd.CurrentRankId where kstid="  + kstid + " order by updatedon desc");
            rptcomments.DataBind();
        }
    }
}
