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
using System.Text;

public partial class Vettting_VetttingPlannerReport : System.Web.UI.Page
{

    public AuthenticationManager Auth;
    int intLogin_Id = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        if (Session["loginid"] == null)
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Default.aspx';", true);
            return;
        }
        else
        {
            intLogin_Id = Convert.ToInt32(Session["loginid"].ToString());
            Auth = new AuthenticationManager(307, intLogin_Id, ObjectType.Page);
        }

        if (!IsPostBack)
        {
            BindFleet();
            BindVessel();
            BindInspectors();
            BindVettingPlanner();
        }
    }
    public string DoAction(int sno, string mode,object param,object param1,object param2)
    {
        string resultstring="";
        bool result = false;
        char[] sep = { '_' };
        int inspId=Common.CastAsInt32(param);
        int NextinspId = Common.CastAsInt32(param1);
        if (NextinspId == inspId)
        {
            NextinspId = 0;
        }
        
        if (sno == 1)
        {
            switch (mode)
            {
                case "A":
                    result = Auth.IsAdd && (inspId <= 0);
                    break;
                case "E":
                    result = Auth.IsUpdate && (inspId > 0);
                    break;
                case "D":
                    result = Auth.IsDelete && (inspId > 0);
                    break;
                default:
                    break;
            }
        }
        else
        {
            switch (mode)
            {
                case "A":
                    result = Auth.IsAdd && (NextinspId <= 0) && (inspId > 0);
                    break;
                case "E":
                    result = Auth.IsUpdate && (NextinspId > 0);
                    break;
                case "D":
                    result = Auth.IsDelete && (NextinspId > 0);
                    break;
                default:
                    break;
            }
        }
        //------------------
        if (param2.ToString().Trim() != "") // IF DONE DATE IS FILLED
        {
            if (mode == "E" || mode == "D")
            {
              //  result = false;
            }
        }
        if(result)
            resultstring="cursor:pointer";
        else
            resultstring = "display:none";

        return resultstring;
    }
    public string GetRating(object Rating)
    {
        string clip = "";
        switch (Rating.ToString())
        {
            case "A":
                clip = "background-position:-18px 3px;";
                break;
            case "B":
                clip = "background-position:-27px 3px;";
                break;
            case "C":
                clip = "background-position:-9px 3px;";
                break;
            default:
                clip = "background-position:0px 3px;";
                break;
        }

        string resultstring = "<div style='background-repeat:no-repeat;padding-top:3px;width:9px; height:9px; background-image:url(Images/crew_rating.png); float:left;" + clip + "' title='" + Rating + "'> </div>";
        return resultstring;
    }
    protected void ddlFleet_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        BindVessel();
    }
    protected void btnShowRating_Click(object sender, EventArgs e)
    {
        int Crewid = Common.CastAsInt32(hfd_Args.Value);
        if (Crewid > 0)
        {
            rat_NA.Checked = true;
            dv_Rating.Visible = true;
            DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT CREWNUMBER,FIRSTNAME + ' ' + MIDDLENAME + ' ' + LASTNAME AS CREWNAME,(SELECT RANKCODE FROM DBO.RANK WHERE RANKID=CURRENTRANKID) AS RANKCODE  FROM DBO.CREWPERSONALDETAILS WHERE CREWID=" + Crewid.ToString());
            lblCrewNumber.Text = dt.Rows[0]["CREWNUMBER"].ToString() + " / [ " + dt.Rows[0]["RANKCODE"].ToString() + " ] ";
            lblCrewName.Text = dt.Rows[0]["CREWNAME"].ToString();
            dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.CREWOTHERDETAILS WHERE CREWID=" + Crewid.ToString());
            if (dt.Rows.Count > 0)
            {
                rat_A.Checked = dt.Rows[0]["Rating"].ToString() == "A";
                rat_B.Checked = dt.Rows[0]["Rating"].ToString() == "B";
                rat_C.Checked = dt.Rows[0]["Rating"].ToString() == "C";
                rat_NA.Checked = dt.Rows[0]["Rating"].ToString() == " ";
            }
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        int Crewid = Common.CastAsInt32(hfd_Args.Value);
        string Rating = " ";
        if (rat_A.Checked)
        {
            Rating = "A";
        }
        if (rat_B.Checked)
        {
            Rating = "B";
        }
        if (rat_C.Checked)
        {
            Rating = "C";
        }
        DataTable dt=Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.CREWOTHERDETAILS WHERE CREWID=" + Crewid.ToString());
        if (dt.Rows.Count > 0)
        {
            Common.Execute_Procedures_Select_ByQuery("UPDATE DBO.CREWOTHERDETAILS SET RATING='" + Rating + "' WHERE CREWID=" + Crewid.ToString());
        }
        else
        {
            Common.Execute_Procedures_Select_ByQuery("INSERT INTO DBO.CREWOTHERDETAILS(CREWID,RATING) VALUES(" + Crewid.ToString() + ",'" + Rating + "')");
        }

        BindVettingPlanner();
        dv_Rating.Visible = false; 
    }
    protected void btnRatingClose_Click(object sender, EventArgs e)
    {
        dv_Rating.Visible = false;

    }
    public void BindVessel()
    {
        string WhereClause = "";
        string sql = "SELECT VesselID,Vesselname FROM DBO.Vessel v Where vesselstatusid<>2  and FleetId>0 and vesseltypeid not in (23,28)";
        if (ddlFleet.SelectedIndex != 0)
        {
            WhereClause = WhereClause + " and fleetID=" + ddlFleet.SelectedValue + "";
        }
        sql = sql + WhereClause + "ORDER BY VESSELNAME";

        ddlVessel.DataSource = VesselReporting.getTable(sql);
        ddlVessel.DataTextField = "Vesselname";
        ddlVessel.DataValueField = "VesselID";
        ddlVessel.DataBind();
        foreach (ListItem li in ddlVessel.Items)
        {
            li.Selected = true;
        }
    }  
    public void BindInspectors()
    {
        try
        {
            //DataSet dsInspectionR = Budget.getTable("select LoginId,firstname + ' ' + lastname as Fullname from dbo.userlogin where loginid in  ( SELECT distinct SUPERINTENDENTID FROM t_InspSupt) order by firstname + ' ' + lastname");
            DataTable dsInspectionR = Common.Execute_Procedures_Select_ByQuery("select LoginId,FirstName+' '+LastName as Name from DBO.USERLOGIN WHERE statusid='A' AND LOGINID IN (SELECT USERID FROM DBO.Hr_PersonalDetails U INNER JOIN DBO.POSITION P ON U.POSITION=P.POSITIONID AND ISINSPECTOR=1) ORDER BY [Name]");
            //Remote ID
            this.ddlInsRemote.DataSource = dsInspectionR;
            this.ddlInsRemote.DataValueField = "LoginId";
            this.ddlInsRemote.DataTextField = "Name";
            this.ddlInsRemote.DataBind();
            //ddlInsRemote.Items.Insert(0, "< ALL >");
            //ddlInsRemote.Items[0].Value = "0";
            foreach (ListItem li in ddlInsRemote.Items)
            {
                li.Selected = true;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public void BindFleet()
    {
        try
        {
            DataTable dtFleet = Budget.getTable("SELECT FleetId,FleetName as Name FROM dbo.FleetMaster").Tables[0];
           this.ddlFleet.DataSource = dtFleet;
            this.ddlFleet.DataValueField = "FleetId";
            this.ddlFleet.DataTextField = "Name";
            this.ddlFleet.DataBind();
            ddlFleet.Items.Insert(0, "< ALL >");
            ddlFleet.Items[0].Value = "0";
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public void BindVettingPlanner()
    {
        try
        {
            string Vessels = "";
            foreach (ListItem li in ddlVessel.Items)
            {
                if (li.Selected)
                    Vessels += "," + li.Value;
            }
            if (Vessels.Trim() != "")
                Vessels = Vessels.Substring(1);
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "fsd", "alert('Please select a vessl');", true);
                return; 
            }
            //------------------------
            string Inspectors = "";
            foreach (ListItem li in ddlInsRemote.Items)
            {
                if (li.Selected)
                    Inspectors += "," + li.Value;
            }
            if (Inspectors.Trim() != "")
                Inspectors = Inspectors.Substring(1);
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "fsd", "alert('Please select an inspector');", true);
                return; 
            }
            //------------------------
            DataTable dtVettingPlanner = Budget.getTable("exec dbo.VettingPlannerReport '" + Vessels + "'," + Common.CastAsInt32(txtdays.Text).ToString() + "," + Common.CastAsInt32(txtPDays.Text).ToString()).Tables[0];
            if (dtVettingPlanner != null)
            {
                string Filter = " 1=1";

                if (chkAll_S.Checked)
                {
                    Filter = Filter + " AND ( ( REMOTEID in (" + Inspectors + ") " + ((chk_PlannedOnly.Checked) ? "" : "OR REMOTEID<=0 OR REMOTEID is null") + " ) OR ( ATTENDID in (" + Inspectors + ") " + ((chk_PlannedOnly.Checked) ? "" : "OR ATTENDID<=0 OR ATTENDID is null") + " ) ) ";
                }
                else
                {
                    Filter = Filter + " AND ( ( REMOTEID in (" + Inspectors + ")) OR ( ATTENDID in (" + Inspectors + ") ) )";
                }
                
                //if (chk_PlannedOnly.Checked)
                //{
                //    Filter = Filter + " AND ISNULL(NEXTINSPID,0) > 0 ";
                //}
                DataView DtFiltered = dtVettingPlanner.DefaultView;
                DtFiltered.RowFilter = Filter;
                DataTable dtRes = DtFiltered.ToTable();
                rptVettingPlanner.DataSource = dtRes;
                rptVettingPlanner.DataBind();

                lblCount.Text = "( " + dtRes.Rows.Count.ToString() + " ) records Found.";
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void cklAll_V_CheckedChanged(object sender, EventArgs e)
    {
        foreach (ListItem li in ddlVessel.Items)
        {
            li.Selected = cklAll_V.Checked;
        }
    }
    protected void cklAll_S_CheckedChanged(object sender, EventArgs e)
    {
        foreach (ListItem li in ddlInsRemote.Items)
        {
            li.Selected = chkAll_S.Checked;
        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        string Vessels = "";
        foreach (ListItem li in ddlVessel.Items)
        {
            if (li.Selected)
                Vessels += "," + li.Value;
        }
        if (Vessels.Trim() != "")
            Vessels = Vessels.Substring(1);
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "fsd", "alert('Please select a vessel');", true);
            return;
        }

        //------------------------
        string Inspectors = "";
        foreach (ListItem li in ddlInsRemote.Items)
        {
            if (li.Selected)
                Inspectors += "," + li.Value;
        }
        if (Inspectors.Trim() != "")
            Inspectors = Inspectors.Substring(1);
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "fsd", "alert('Please select an inspector');", true);
            return;
        }
        //------------------------


        dvFilter.Visible = false;
        BindVettingPlanner();
    }
    protected void btnShowFilter_Click(object sender, EventArgs e)
    {
        dvFilter.Visible = true;
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        string Vessels = "";
        foreach (ListItem li in ddlVessel.Items)
        {
            if (li.Selected)
                Vessels += "," + li.Value;
        }
        if (Vessels.Trim() != "")
            Vessels = Vessels.Substring(1);
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "fsd", "alert('Please select a vessl');", true);
            return;
        }
        //------------------------
        string Inspectors = "";
        foreach (ListItem li in ddlInsRemote.Items)
        {
            if (li.Selected)
                Inspectors += "," + li.Value;
        }
        if (Inspectors.Trim() != "")
            Inspectors = Inspectors.Substring(1);
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "fsd", "alert('Please select an inspector');", true);
            return;
        }

        Session.Add("c1_Vessels",Vessels);
        Session.Add("c1_Inspectors",Inspectors);
        Session.Add("c1_PlannedOnly",chk_PlannedOnly.Checked);

        Session.Add("All", chkAll_S.Checked);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "fsd", "window.open('../Reports/VettingPlannerReport.aspx?FleetName=" + ddlFleet.SelectedItem.Text + "&Days=" + txtdays.Text + "&Plan=" + txtPDays.Text + "','Vetting','left=150,top=50,resizable=yes');", true);  
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        dvFilter.Visible = false;

    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        ddlFleet.SelectedIndex = 0;
        ddlFleet_OnSelectedIndexChanged(sender, e);
        chkAll_S.Checked = true;
        cklAll_S_CheckedChanged(sender, e);
        cklAll_V.Checked = true;
        cklAll_V_CheckedChanged(sender, e);
        txtdays.Text = "365";
        txtPDays.Text = "60";
        chk_PlannedOnly.Checked = false;

        btnShow_Click(sender,e);
    }
    protected void Days_TextChanged(object sender, EventArgs e)
    {
        BindVettingPlanner();
    }

    protected void AddInspection_Click(object sender, EventArgs e)
    {
        Session["Insp_Id"] = null;
        Session["DueMode"] = null;
        Session["Mode"] = "Add";

        Response.Redirect("../Transactions/InspectionPlanning.aspx?mode=m1&param=" + hid1.Value);
    }
    protected void EditInspection_Click(object sender, EventArgs e)
    {
        Session["Insp_Id"] = hid1.Value;
        Session["DueMode"] = "ShowFull";
        Response.Redirect("../Transactions/InspectionPlanning.aspx?mode=m2&param=" + hid1.Value);
    }
    protected void CancelInspection_Click(object sender, EventArgs e)
    {
        if (hid1.Value.Trim() != "")
        {
            Common.Execute_Procedures_Select_ByQuery("DELETE FROM dbo.t_inspectiondue where ltrim(rtrim(inspectionno))='" + hid1.Value.Trim() + "'");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "alert('Inspection cancelled successfully.');", true);
            btnShowFilter_Click(sender,e);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "alert('No inspection found to cancel.');", true);
        }
    }
}
