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
using ShipSoft.CrewManager.BusinessObjects;
using ShipSoft.CrewManager.BusinessLogicLayer;
using ShipSoft.CrewManager.Operational;

public partial class CrewAccounting_Arrier : System.Web.UI.Page
{
    Authority Auth;
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        Session["PageName"] = " - Calculate Arrear";
        lbl_ctm_Message.Text = ""; 
        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 118);
        if (chpageauth <= 0)
        {
            Response.Redirect("Dummy.aspx");

        }
        //*******************
        ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 3);
        OBJ.Invoke();
        Session["Authority"] = OBJ.Authority;
        Auth = OBJ.Authority;
        if (!Page.IsPostBack)
        {
            bindVesselNameddl();
            this.btnSaveCTM.Visible = Auth.isEdit;
            this.cmdprint.Visible = Auth.isPrint;
        }
    }
    protected void bindVesselNameddl()
    {
       DataSet ds = SearchSignOff.getVessel(Convert.ToInt32(Session["loginid"].ToString()));
        ddl_Vessel.DataSource = ds.Tables[0];
        ddl_Vessel.DataValueField = "VesselId";
        ddl_Vessel.DataTextField = "Name";
        ddl_Vessel.DataBind();
        ddl_Vessel.Items.Insert(0, new ListItem("<Select>", "0"));
    }
    protected void btnSaveCTM_Click(object sender, EventArgs e)
    {
        try
        {
            cls_Arrier.Calculate_Arrier(Convert.ToInt32(ddl_Vessel.SelectedValue), Convert.ToInt32(Session["loginid"].ToString()));
            lbl_ctm_Message.Text = "Arear Calculated Successfully."; 
        }
        catch { lbl_ctm_Message.Text = "Arear can't Calculated."; }

    }
    protected void cmdprint_Click(object sender, EventArgs e)
    {
        Session["ArrearReportheader"] = "Arear Report for Vessel : " + ddl_Vessel.SelectedItem.ToString();  
        Response.Redirect("~/Reporting/ArearReport.aspx?VesselId=" + ddl_Vessel.SelectedValue);
    }
}
