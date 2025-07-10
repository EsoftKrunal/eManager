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

public partial class VesselRecord_VesselAllocation : System.Web.UI.UserControl
{
    Authority Auth;
    public Label ErrorLabel;
    string Mode;
   
    public int VesselId
    {
        get { return  Common.CastAsInt32(ViewState["VesselId"]); }
        set { ViewState["VesselId"] = value; }
    }
    public void show_Message(string mess)
    {
        try
        {
            ErrorLabel.Text = mess;
        }
        catch { }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        ////***********Code to check page acessing Permission
        //int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 36);
        //if (chpageauth <= 0)
        //{
        //    Response.Redirect("../AuthorityError.aspx");
        //}
        ////**********
        lblMsg.Text = "";
        //ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 4);
        //OBJ.Invoke();
        //Session["Authority"] = OBJ.Authority;
        //Auth = OBJ.Authority;
        //try
        //{
        //    Mode = Session["VMode"].ToString();
        //}
        //catch { Mode = "New"; }
        if (!Page.IsPostBack)
        {
            VesselId = Common.CastAsInt32(Session["VesselID"]);
            Bindddls();
            BindEmails();            
        }
    }

    public void BindEmails()
    {
        string SQL = "select email as VesselEmail,groupemail AS TechnincalGroupEmail,groupemail1 AS CrewGroupEmail,TechSupdt,MarineSupdt,FleetManager,OwnerRepEmail,ChartererEmail,TechAssistant,MarineAssistant,SPA,AcctOfficer, ISNULL(IsBonusApplicable, 'N') As IsBonusApplicable,VesselEmailNew  " +
                     "from DBO.vessel WHERE VesselId = " + VesselId + " Order By VesselName";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);

        if (dt != null && dt.Rows.Count > 0)
        {
            txtVesselEmail.Text = dt.Rows[0]["VesselEmailNew"].ToString();
            txtShipsoftEmail.Text = dt.Rows[0]["VesselEmail"].ToString();
            txtTechGrpEmail.Text = dt.Rows[0]["TechnincalGroupEmail"].ToString();
            txtCrewGrpEmail.Text = dt.Rows[0]["CrewGroupEmail"].ToString();
            txtOwnerRepEmail.Text = dt.Rows[0]["OwnerRepEmail"].ToString();
            txtChartererEmail.Text = dt.Rows[0]["ChartererEmail"].ToString();
            try
            {
                ddlAcctOfficer.SelectedValue = dt.Rows[0]["AcctOfficer"].ToString();
                ddlFleetMgr.SelectedValue = dt.Rows[0]["FleetManager"].ToString();
                ddlMarineAssistant.SelectedValue = dt.Rows[0]["MarineAssistant"].ToString();
                ddlMarineSupdt.SelectedValue = dt.Rows[0]["MarineSupdt"].ToString();
                ddlSPA.SelectedValue = dt.Rows[0]["SPA"].ToString();
                ddlTechAssistant.SelectedValue = dt.Rows[0]["TechAssistant"].ToString();
                ddlTechSupdt.SelectedValue = dt.Rows[0]["TechSupdt"].ToString();
                r1.Checked = (dt.Rows[0]["IsBonusApplicable"].ToString().Trim() == "Y");
                r2.Checked = (dt.Rows[0]["IsBonusApplicable"].ToString().Trim() == "N");
            }
            catch { }
        }

    }
    public void Bindddls()
    {
        string SQL = "SELECT LoginId, (FirstName + ' ' + LastName) As UserName  FROM DBO.UserLogin WHERE statusId = 'A' Order By FirstName,LastName";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);

        ddlAcctOfficer.DataSource = dt;
        ddlAcctOfficer.DataTextField = "UserName";
        ddlAcctOfficer.DataValueField = "LoginId";
        ddlAcctOfficer.DataBind();
        ddlAcctOfficer.Items.Insert(0, new ListItem("< Select >", "0"));

        ddlFleetMgr.DataSource = dt;
        ddlFleetMgr.DataTextField = "UserName";
        ddlFleetMgr.DataValueField = "LoginId";
        ddlFleetMgr.DataBind();
        ddlFleetMgr.Items.Insert(0, new ListItem("< Select >", "0"));

        ddlMarineAssistant.DataSource = dt;
        ddlMarineAssistant.DataTextField = "UserName";
        ddlMarineAssistant.DataValueField = "LoginId";
        ddlMarineAssistant.DataBind();
        ddlMarineAssistant.Items.Insert(0, new ListItem("< Select >", "0"));

        ddlMarineSupdt.DataSource = dt;
        ddlMarineSupdt.DataTextField = "UserName";
        ddlMarineSupdt.DataValueField = "LoginId";
        ddlMarineSupdt.DataBind();
        ddlMarineSupdt.Items.Insert(0, new ListItem("< Select >", "0"));

        ddlSPA.DataSource = dt;
        ddlSPA.DataTextField = "UserName";
        ddlSPA.DataValueField = "LoginId";
        ddlSPA.DataBind();
        ddlSPA.Items.Insert(0, new ListItem("< Select >", "0"));

        ddlTechAssistant.DataSource = dt;
        ddlTechAssistant.DataTextField = "UserName";
        ddlTechAssistant.DataValueField = "LoginId";
        ddlTechAssistant.DataBind();
        ddlTechAssistant.Items.Insert(0, new ListItem("< Select >", "0"));

        ddlTechSupdt.DataSource = dt;
        ddlTechSupdt.DataTextField = "UserName";
        ddlTechSupdt.DataValueField = "LoginId";
        ddlTechSupdt.DataBind();
        ddlTechSupdt.Items.Insert(0, new ListItem("< Select >", "0"));
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string IsBonusApplicable = (r1.Checked) ? "Y" : "N";
        string strSql = "UPDATE DBO.vessel SET VesselEmailNew='" + txtVesselEmail.Text.Trim() + "',email = '" + txtShipsoftEmail.Text.Trim() + "', groupemail = '" + txtTechGrpEmail.Text.Trim() + "', groupemail1='" + txtCrewGrpEmail.Text.Trim() + "', TechSupdt=" + ddlTechSupdt.SelectedValue.Trim() + ",MarineSupdt=" + ddlMarineSupdt.SelectedValue.Trim() + ",FleetManager=" + ddlFleetMgr.SelectedValue.Trim() + ",OwnerRepEmail='" + txtOwnerRepEmail.Text.Trim() + "',ChartererEmail='" + txtChartererEmail.Text.Trim() + "',TechAssistant=" + ddlTechAssistant.SelectedValue.Trim() + ",MarineAssistant=" + ddlMarineAssistant.SelectedValue.Trim() + ",SPA=" + ddlSPA.SelectedValue.Trim() + ",AcctOfficer=" + ddlAcctOfficer.SelectedValue.Trim() + ", IsBonusApplicable='" + IsBonusApplicable + "' " +
                        " WHERE VesselId =" + VesselId;

        try
        {
            Common.Execute_Procedures_Select_ByQuery(strSql);
            lblMsg.Text = "Record saved successfully.";
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Unable to save record. Error: " + ex.Message;
        }

    }
}