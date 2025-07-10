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

public partial class CrewAppraisal_UpdateCrewDetails : System.Web.UI.Page
{
    public int PeapID
    {
        get { return Convert.ToInt32("0" + ViewState["vPeapID"].ToString()); }
        set { ViewState["vPeapID"] = value.ToString(); }
    }
    public int CrewID
    {
        get { return Convert.ToInt32("0" + ViewState["vCrewID"].ToString()); }
        set { ViewState["vCrewID"] = value.ToString(); }
    }
    public string VesselCode
    {
        get { return Convert.ToString(ViewState["vVesselCode"].ToString()); }
        set { ViewState["vVesselCode"] = value.ToString(); }
    }
    public string Location
    {
        get { return Convert.ToString(ViewState["vLocation"].ToString()); }
        set { ViewState["vLocation"] = value.ToString(); }
    }
    public int VesselID
    {
        get { return Convert.ToInt32("0" + ViewState["vVesselID"].ToString()); }
        set { ViewState["vVesselID"] = value.ToString(); }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        lblmsgMissMatch.Text = "";
        if (!Page.IsPostBack)
        {
            PeapID = Common.CastAsInt32(Page.Request.QueryString["PeapID"]);
            VesselCode = Page.Request.QueryString["VesselCode"].ToString();
            Location = Page.Request.QueryString["Location"].ToString();
            VesselID = 0;
        }
    }
    protected void btnUpdMissMatch_OnClick(object sender, EventArgs e)
    {
        if (txtCrewNoforSearch.Text.Trim() == "")
        {
            lblmsgMissMatch.Text = "Please enter Crew Number.";
            return;
        }

        String sqlJDT = "select Top 1 SignOn from CrewExperienceDetails where CrewID=" + CrewID + " and VesselID=" + VesselID.ToString() + " order by signon desc";
        DataSet DsJDT = Budget.getTable(sqlJDT);
        DateTime DtJDT=new DateTime();
        if (DsJDT != null)
        {
            if (DsJDT.Tables[0].Rows.Count > 0)
            {
                DtJDT = Convert.ToDateTime(DsJDT.Tables[0].Rows[0][0]);
            }
        }

        string sql = "update tbl_assessment set CrewNo='" + txtCrewNoforSearch.Text.Trim() + "' , AssName='" + txtFnameMisMatch.Text.Trim() + "',AssLname='" + txtLnameMisMatch.Text.Trim() + "',DateJoinedVessel='" + DtJDT.ToString("dd-MMM-yyyy") + "',ShipSoftRank="+hfShipSoftRank.Value+" where AssMgntID=" + PeapID.ToString() + " and VesselCode='" + VesselCode+ "' and Location='" + Location + "'" + "  select 'a'";
        DataSet Ds = Budget.getTable(sql);
        if (Ds!=null)
        {

            lblmsgMissMatch.Text = "Updated successfully.";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ss", "BindParentGrid()", true);
        }
        else
        {
            lblmsgMissMatch.Text = "Data could not be updated.";
        }
    }

    protected void txtCrewNoforSearch_OnTextChanged(object sender, EventArgs e)
    {
        SetCrewID();
        SetVesselID();

        String sqlJDT = "select Top 1 SignOn from CrewExperienceDetails where CrewID=" + CrewID + " and VesselID=" + VesselID.ToString() + " order by signon desc";
        DataSet DsJDT = Budget.getTable(sqlJDT);
        DateTime DtJDT = new DateTime();
        if (DsJDT != null)
        {
            if (DsJDT.Tables[0].Rows.Count > 0)
            {
                DtJDT = Convert.ToDateTime(DsJDT.Tables[0].Rows[0][0]);
                btnUpdMissMatch.Visible = true;
            }
            else
            {
                lblmsgMissMatch.Text = "This user have not ever signed in on this vessel.";
                btnUpdMissMatch.Visible = false;
                return;
            }
        }

        string sql = "select FirstName,LastName,MiddleName ,CurrentRankID from CrewPersonalDetails where CrewNumber='" + txtCrewNoforSearch.Text.Trim() + "'";
        DataSet Ds = Budget.getTable(sql);
        if (Ds != null)
        {
            if (Ds.Tables[0].Rows.Count > 0)
            {

                txtFnameMisMatch.Text = Ds.Tables[0].Rows[0]["FirstName"].ToString() ;
                txtLnameMisMatch.Text = Ds.Tables[0].Rows[0]["LastName"].ToString();
                hfShipSoftRank.Value = Ds.Tables[0].Rows[0]["CurrentRankID"].ToString(); 
            }
            else
            {
                lblmsgMissMatch.Text = "Not Found.";
            }
        }
    }
    public void SetCrewID()
    {
        string sql = "select CrewID  from CrewPersonalDetails where CrewNumber='" + txtCrewNoforSearch .Text.Trim()+ "'";
        DataSet Ds = Budget.getTable(sql);
        if (Ds != null)
        {
            if (Ds.Tables[0].Rows.Count > 0)
            {
                CrewID=Convert.ToInt32(Ds.Tables[0].Rows[0][0]);
            }
        }
    }
    public void SetVesselID()
    {
        //VesselCode
        string sql = "select VesselID from Vessel where VesselCode='"+VesselCode.ToString()+"'";
        DataSet Ds = Budget.getTable(sql);
        if (Ds != null)
        {
            if (Ds.Tables[0].Rows.Count > 0)
            {
                VesselID = Convert.ToInt32(Ds.Tables[0].Rows[0][0]);
            }
        }
    }
}
