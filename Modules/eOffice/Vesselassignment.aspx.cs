using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Emtm_Vesselassignment : System.Web.UI.Page
{
    public int UserId
    {
        get { return Common.CastAsInt32(ViewState["UserId"]); }
        set { ViewState["UserId"] = value; }
    }
    public int VesselId
    {
        get { return Common.CastAsInt32(ViewState["VesselId"]); }
        set { ViewState["VesselId"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        if (!IsPostBack)
        {
            UserId = Common.CastAsInt32(Session["loginid"]);
            DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("SELECT USERID,FIRSTNAME + ' ' + MIDDLENAME + ' ' + FAMILYNAME AS FULLNAME FROM DBO.Hr_PersonalDetails ORDER BY FIRSTNAME + ' ' + MIDDLENAME + ' ' + FAMILYNAME");
            rptVessels.DataSource = dt;
            dt.Rows.InsertAt(dt.NewRow(), 0);
            dt.Rows[0][0] = "0";
            dt.Rows[0][1] = " < SELECT > ";

            ddlAO.DataSource=dt;
            ddlAO.DataTextField = "FULLNAME";
            ddlAO.DataValueField = "USERID";
            ddlAO.DataBind();

            ddlFM.DataSource = dt;
            ddlFM.DataTextField = "FULLNAME";
            ddlFM.DataValueField = "USERID";
            ddlFM.DataBind();

            ddlMA.DataSource = dt;
            ddlMA.DataTextField = "FULLNAME";
            ddlMA.DataValueField = "USERID";
            ddlMA.DataBind();

            ddlMS.DataSource = dt;
            ddlMS.DataTextField = "FULLNAME";
            ddlMS.DataValueField = "USERID";
            ddlMS.DataBind();

            ddlSPA.DataSource = dt;
            ddlSPA.DataTextField = "FULLNAME";
            ddlSPA.DataValueField = "USERID";
            ddlSPA.DataBind();

            ddlTA.DataSource = dt;
            ddlTA.DataTextField = "FULLNAME";
            ddlTA.DataValueField = "USERID";
            ddlTA.DataBind();

            ddlTS.DataSource = dt;
            ddlTS.DataTextField = "FULLNAME";
            ddlTS.DataValueField = "USERID";
            ddlTS.DataBind();

           
            dt=Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.FLEETMASTER ORDER BY FLEETNAME");
            ddlFleet.DataSource = dt;
            ddlFleet.DataTextField = "FLEETNAME";
            ddlFleet.DataValueField = "FLEETID";
            ddlFleet.DataBind();
            ddlFleet.Items.Insert(0,new ListItem(" < ALL FLEET >","0"));

            BindVessels();
        }
    }
    protected void ddlFleet_OnSelectedIndexChanged(object sender,EventArgs e)
    {
        BindVessels();
    }
    protected void BindVessels()
    {
        string FleetWhereClause = "";
        if(ddlFleet.SelectedIndex >0)
            FleetWhereClause=" WHERE V.FLEETID=" + ddlFleet.SelectedValue ;

        string sql = "select ROW_NUMBER() OVER(ORDER BY VESSELNAME) AS SRNO,VesselId,VESSELNAME,FLEETNAME, " +
                             "(SELECT FIRSTNAME + ' ' + MIDDLENAME + ' ' + FAMILYNAME FROM DBO.Hr_PersonalDetails U WHERE U.USERID=V.techsupdt) AS TECHSUPDT, " +
                             "(SELECT FIRSTNAME + ' ' + MIDDLENAME + ' ' + FAMILYNAME FROM DBO.Hr_PersonalDetails U WHERE U.USERID=V.MARINESUPDT) AS MARINESUPDT, " +
                             "(SELECT FIRSTNAME + ' ' + MIDDLENAME + ' ' + FAMILYNAME FROM DBO.Hr_PersonalDetails U WHERE U.USERID=V.TECHASSISTANT) AS TECHASSISTANT, " +
                             "(SELECT FIRSTNAME + ' ' + MIDDLENAME + ' ' + FAMILYNAME FROM DBO.Hr_PersonalDetails U WHERE U.USERID=V.MARINEASSISTANT) AS MARINEASSISTANT, " +
                             "(SELECT FIRSTNAME + ' ' + MIDDLENAME + ' ' + FAMILYNAME FROM DBO.Hr_PersonalDetails U WHERE U.USERID=V.SPA) AS SPA, " +
                             "(SELECT FIRSTNAME + ' ' + MIDDLENAME + ' ' + FAMILYNAME FROM DBO.Hr_PersonalDetails U WHERE U.USERID=V.AcctOfficer) AS AcctOfficer, " +
                             "(SELECT FIRSTNAME + ' ' + MIDDLENAME + ' ' + FAMILYNAME FROM DBO.Hr_PersonalDetails U WHERE U.USERID=V.fleetmanager) AS fleetmanager " +
                             "from DBO.vessel V INNER JOIN DBO.FLEETMASTER F ON v.fleetid=f.fleetid " + FleetWhereClause + " ORDER BY VesselNAME";

        rptVessels.DataSource = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        rptVessels.DataBind();

    }
    public void BindVesselHistory()
    {
        string sql = "select ROW_NUMBER() OVER(ORDER BY EffDate DESC) AS SRNO,EffDate, " +
                          "(SELECT FIRSTNAME + ' ' + MIDDLENAME + ' ' + FAMILYNAME FROM DBO.Hr_PersonalDetails U WHERE U.USERID=V.TechSuptd) AS TECHSUPDT, " +
                          "(SELECT FIRSTNAME + ' ' + MIDDLENAME + ' ' + FAMILYNAME FROM DBO.Hr_PersonalDetails U WHERE U.USERID=V.MarineSuptd) AS MARINESUPDT, " +
                          "(SELECT FIRSTNAME + ' ' + MIDDLENAME + ' ' + FAMILYNAME FROM DBO.Hr_PersonalDetails U WHERE U.USERID=V.TechAssst) AS TECHASSISTANT, " +
                          "(SELECT FIRSTNAME + ' ' + MIDDLENAME + ' ' + FAMILYNAME FROM DBO.Hr_PersonalDetails U WHERE U.USERID=V.MarineAsst) AS MARINEASSISTANT, " +
                          "(SELECT FIRSTNAME + ' ' + MIDDLENAME + ' ' + FAMILYNAME FROM DBO.Hr_PersonalDetails U WHERE U.USERID=V.SPA) AS SPA, " +
                          "(SELECT FIRSTNAME + ' ' + MIDDLENAME + ' ' + FAMILYNAME FROM DBO.Hr_PersonalDetails U WHERE U.USERID=V.AccountOfficer) AS AcctOfficer, " +
                          "(SELECT FIRSTNAME + ' ' + MIDDLENAME + ' ' + FAMILYNAME FROM DBO.Hr_PersonalDetails U WHERE U.USERID=V.FleetManager) AS fleetmanager " +
                          "from DBO.VesselAssignments V where V.vesselid=" + VesselId + " ORDER BY EffDate DESC";


        rptVesselHistory.DataSource = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        rptVesselHistory.DataBind();
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT vesselid,vesselname,ISNULL(AcctOfficer,0) AS AcctOfficer,ISNULL(fleetmanager,0) AS fleetmanager,ISNULL(MARINEASSISTANT,0) AS MARINEASSISTANT,ISNULL(MARINESUPDT,0) AS MARINESUPDT,ISNULL(SPA,0) AS SPA,ISNULL(TECHASSISTANT,0) AS TECHASSISTANT,ISNULL(techsupdt,0) AS techsupdt FROM DBO.VESSEL WHERE VESSELID=" + ((ImageButton)sender).CommandArgument);
        if (dt.Rows.Count > 0)
        {
            VesselId =Common.CastAsInt32(dt.Rows[0]["vesselid"]);
            lblVesselname.Text = dt.Rows[0]["VESSELNAME"].ToString();
            dv_vsl.Visible = true;

            ddlAO.SelectedValue = dt.Rows[0]["AcctOfficer"].ToString();
            ddlFM.SelectedValue = dt.Rows[0]["fleetmanager"].ToString();
            ddlMA.SelectedValue = dt.Rows[0]["MARINEASSISTANT"].ToString();
            ddlMS.SelectedValue = dt.Rows[0]["MARINESUPDT"].ToString();
            ddlSPA.SelectedValue = dt.Rows[0]["SPA"].ToString();
            ddlTA.SelectedValue = dt.Rows[0]["TECHASSISTANT"].ToString();
            ddlTS.SelectedValue = dt.Rows[0]["techsupdt"].ToString();

            BindVesselHistory();
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        Common.Set_Procedures("UpdateVesselAssignment");
        Common.Set_ParameterLength(9);
        Common.Set_Parameters(new MyParameter("@VesselId", VesselId),
            new MyParameter("@StartDate", txtEffDate.Text),
            new MyParameter("@FM", ddlFM.SelectedValue),
            new MyParameter("@TS", ddlTS.SelectedValue),
            new MyParameter("@MS", ddlMS.SelectedValue),
            new MyParameter("@TA", ddlTA.SelectedValue),
            new MyParameter("@MA", ddlMA.SelectedValue),
            new MyParameter("@SPA", ddlSPA.SelectedValue),
            new MyParameter("@AO", ddlAO.SelectedValue));
        DataSet ds=new DataSet();
        if (Common.Execute_Procedures_IUD_CMS(ds))
        {
            if (ds.Tables[0].Rows[0][0].ToString() == "SUCCESS")
            {
                lblMsg.Text = "Record saved successfully";
                BindVesselHistory();
            }
            else
                lblMsg.Text = "Error. " + ds.Tables[0].Rows[0][1].ToString();
        }
        else
        {
            lblMsg.Text = "Error. " +  Common.ErrMsg;
        }
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        BindVessels();
        dv_vsl.Visible = false;

    }
    
}