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

public partial class Reporting_MedicalTestByPeriod : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();

    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        lblMessage.Text = "";
        this.ddl_Vessel.Attributes.Add("onkeydown", "javascript:if(event.keyCode==13){document.getElementById('" + Button1.ClientID + "').focus();}");
        this.ddl_MedicalDocuments.Attributes.Add("onkeydown", "javascript:if(event.keyCode==13){document.getElementById('" + Button1.ClientID + "').focus();}");
        this.txt_from.Attributes.Add("onkeydown", "javascript:if(event.keyCode==13){document.getElementById('" + Button1.ClientID + "').focus();}");
        this.txt_to.Attributes.Add("onkeydown", "javascript:if(event.keyCode==13){document.getElementById('" + Button1.ClientID + "').focus();}");
        if (!Page.IsPostBack)
        {
            //***********Code to check page acessing Permission
            int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 15);
            if (chpageauth <= 0)
            {
                Response.Redirect("DummyReport.aspx");

            }

            //*******************

            //--Old Code
            //ddl_Vessel.DataSource = cls_SearchReliever.getMasterData("Vessel", "VesselId", "VesselName");
            //ddl_Vessel.DataTextField = "VesselName";
            //ddl_Vessel.DataValueField = "VesselId";
            //ddl_Vessel.DataBind();
            //--New Code

            DataSet dt8 = SearchSignOff.getVessel(Convert.ToInt32(Session["loginid"].ToString()));
            this.ddl_Vessel.DataSource = dt8;
            this.ddl_Vessel.DataValueField = "VesselId";
            this.ddl_Vessel.DataTextField = "Name";
            this.ddl_Vessel.DataSource = dt8;
            this.ddl_Vessel.DataBind();
            ddl_Vessel.Items.Insert(0, new ListItem("< All >", "0"));

            DataSet dt = cls_SearchReliever.getMasterData("MedicalDocuments", "MedicalDocumentId", "MedicalDocumentName");
            ddl_MedicalDocuments.DataTextField = "MedicalDocumentName";
            ddl_MedicalDocuments.DataValueField = "MedicalDocumentId";
            ddl_MedicalDocuments.DataSource = dt;
            ddl_MedicalDocuments.DataBind();
            ddl_MedicalDocuments.Items.Insert(0, new ListItem("< Select >", "0"));
        }
        else
        {
            Button1_Click(sender, e);
        }
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Convert.ToDateTime(txt_from.Text) > Convert.ToDateTime(txt_to.Text))
        {
            this.lblMessage.Text = "From Date Should be less than To Date.";
        }
        else
        {
            IFRAME1.Attributes.Add("src", "MedicalTestContainer.aspx?vessel=" + ddl_Vessel.SelectedValue + "&docname=" + ddl_MedicalDocuments.SelectedItem.Text.Replace("&","@") + "&doctype=" + ddl_MedicalDocuments.SelectedValue + "&fdt=" + txt_from.Text + "&tdt=" + txt_to.Text);
        }
    }
}
