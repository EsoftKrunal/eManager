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

public partial class Reporting_VesselCrewMatrix : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        this.ddl_Vessel.Attributes.Add("onkeydown", "javascript:if(event.keyCode==13){document.getElementById('" + btn_Show.ClientID + "').focus();}");
        this.ddl_Matrix.Attributes.Add("onkeydown", "javascript:if(event.keyCode==13){document.getElementById('" + btn_Show.ClientID + "').focus();}");
        
        //***********Code to Check Page Accessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 15);
        if (chpageauth <= 0)
        {
            Response.Redirect("DummyReport.aspx");
        }
        //*******************
        this.lblMessage.Text = "";
        if (!Page.IsPostBack)
        {
            BindVesselDDL();
            BindMatrixDDL();
        }
        else
        {
            Show_Report();
        }
    }
    private void BindVesselDDL()
    {
        DataSet dtvsl = cls_SearchReliever.getMasterData("Vessel", "VesselId", "VesselName", Convert.ToInt32(Session["loginid"].ToString()));
        this.ddl_Vessel.DataTextField = "VesselName";
        this.ddl_Vessel.DataValueField = "Vesselid";
        this.ddl_Vessel.DataSource = dtvsl;
        this.ddl_Vessel.DataBind();
        this.ddl_Vessel.Items.Insert(0, new ListItem("<All>", "0"));
    }
    private void BindMatrixDDL()
    {
        DataSet dtmtrx = cls_SearchReliever.getMasterData("MatrixHeader", "MatrixId", "MatrixName");
        this.ddl_Matrix.DataTextField = "MatrixName";
        this.ddl_Matrix.DataValueField = "MatrixId";
        this.ddl_Matrix.DataSource = dtmtrx;
        this.ddl_Matrix.DataBind();
        this.ddl_Matrix.Items.Insert(0, new ListItem("<Select>", "0"));
    }
    private void Show_Report()
    {
        IFRAME1.Attributes.Add("src", "VesselCrewMatrixContainer.aspx?vid=" + ddl_Vessel.SelectedValue + "&vname=" + ddl_Vessel.SelectedItem.Text + "&matid=" + ddl_Matrix.SelectedValue + "&matname=" + ddl_Matrix.SelectedItem.Text);
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
}
