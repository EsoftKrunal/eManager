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

public partial class CrewOperation_VesselPlanning_CrewSearch : System.Web.UI.Page
{
    
    public int PlanningVesselId
    {
        get { return Common.CastAsInt32(ViewState["PlanningVesselId"]); }
        set { ViewState["PlanningVesselId"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        this.txt_FirstName.Attributes.Add("onKeyPress", "javascript:if(event.keyCode==13){document.getElementById('" + btn_Search.UniqueID + "').focus();}");
        this.txt_LastName.Attributes.Add("onKeyPress", "javascript:if(event.keyCode==13){document.getElementById('" + btn_Search.UniqueID + "').focus();}");
        this.txt_EmpNo.Attributes.Add("onKeyPress", "javascript:if(event.keyCode==13){document.getElementById('" + btn_Search.UniqueID + "').focus();}");

        this.chk_BON.Attributes.Add("onKeyPress", "javascript:if(event.keyCode==13){document.getElementById('" + btn_Search.UniqueID + "').focus();}");
        this.chk_Exclude.Attributes.Add("onKeyPress", "javascript:if(event.keyCode==13){document.getElementById('" + btn_Search.UniqueID + "').focus();}");
        this.chkfamily.Attributes.Add("onKeyPress", "javascript:if(event.keyCode==13){document.getElementById('" + btn_Search.UniqueID + "').focus();}");

        
        Main.Text = "";
        if (!(IsPostBack))
        {
            PlanningVesselId = Common.CastAsInt32(Request.QueryString["VesselId"]);
            lblPlanVesselName.Text = Request.QueryString["VesselName"].ToString();
            ddl_Matrix.Enabled = false;
            Load_OwnerPool();
            Load_Vessel();
            Load_Vesseltype();
            Load_Rank();
            Load_Matrix();
            Load_Status();
            Load_Nationality();
            Load_RecruitingOffice();

            DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("SELECT v.VesselCode, (SELECT OwnerCode FROM Owner O WHERE O.OwnerId = v.OwnerId) AS OwnerCode FROM vessel v WHERE v.VesselId = " + PlanningVesselId);
            //lblGradingFor.Text = dt.Rows[0]["VesselCode"].ToString();
            ViewState["CurrentVesselCode"] = dt.Rows[0]["VesselCode"].ToString();
            ViewState["CurrentOwnerCode"] = dt.Rows[0]["OwnerCode"].ToString();
        }
    }

    #region PageLoaderControl
    
    private void Load_Status()
    {
        DataSet ds = NewPlanning.getstatus();
        ddl_Status.DataSource = ds.Tables[0];
        ddl_Status.DataTextField = "StatusName";
        ddl_Status.DataValueField = "StatusId";
        ddl_Status.DataBind();
    }
    private void Load_Rank()
    {
        DataSet ds = NewPlanning.getMasterData("Rank", "RankId", "RankCode");
        ddl_Rank.DataSource = ds.Tables[0];
        ddl_Rank.DataTextField = "RankCode";
        ddl_Rank.DataValueField = "RankId";
        ddl_Rank.DataBind();
        ddl_Rank.Items.Insert(0, new ListItem("< Select >", "0"));
    }
    private void Load_Vessel()
    {
        //DataSet ds = SearchSignOff.getVessel(Convert.ToInt32(Session["loginid"].ToString()));
        //ddl_Vessel.DataSource = ds.Tables[0];
        //ddl_Vessel.DataValueField = "VesselId";
        //ddl_Vessel.DataTextField = "VesselName1";
        //ddl_Vessel.DataBind();
        //ddl_Vessel.Items.Insert(0, new ListItem("< Select >", "0"));
       
    }
    private void Load_Vesseltype()
    {
        DataSet ds = NewPlanning.getMasterData("VesselType", "VesselTypeId", "VesselTypeName");
        ddl_VesselType.DataSource = ds.Tables[0];
        ddl_VesselType.DataTextField = "VesselTypeName";
        ddl_VesselType.DataValueField = "VesselTypeId";
        ddl_VesselType.DataBind();
        ddl_VesselType.Items.Insert(0, new ListItem("< Select >", "0"));
    }
    private void Load_OwnerPool()
    {
        DataSet ds = NewPlanning.getMasterData("OwnerPool", "OwnerPoolId", "OwnerPoolName");
        ddl_OwnerPool.DataSource = ds.Tables[0];
        ddl_OwnerPool.DataTextField = "OwnerPoolName";
        ddl_OwnerPool.DataValueField = "OwnerPoolId";
        ddl_OwnerPool.DataBind();
        ddl_OwnerPool.Items.Insert(0, new ListItem("< Select >", "0"));
    }
    private void Load_Matrix()
    {
        DataTable ds = NewPlanning.getmatrix();
        this.ddl_Matrix.DataSource = ds;
        ddl_Matrix.DataTextField = "MatrixName";
        ddl_Matrix.DataValueField = "MatrixId";
        ddl_Matrix.DataBind();
    }
    private void Load_RecruitingOffice()
    {
        DataSet ds = cls_SearchReliever.getMasterData("RecruitingOffice", "RecruitingOfficeId", "RecruitingOfficeName");
        dd_RecOff.DataSource = ds.Tables[0];
        dd_RecOff.DataTextField = "RecruitingOfficeName";
        dd_RecOff.DataValueField = "RecruitingOfficeId";
        dd_RecOff.DataBind();
        dd_RecOff.Items.Insert(0, new ListItem("< All >", ""));
    }
    private void Load_Nationality()
    {
        dd_Nationality.DataSource = cls_SearchReliever.getMasterData("Country", "CountryId", "CountryName");
        dd_Nationality.DataTextField = "CountryName";
        dd_Nationality.DataValueField = "CountryId";
        dd_Nationality.DataBind();
        dd_Nationality.Items.Insert(0, new ListItem("< All >", ""));
    }
    #endregion
    protected void ddl_Vessel_SelectedIndexChanged(object sender, EventArgs e)
    {
        //DataTable dt32 = cls_SearchReliever.Chk_VesselType_fromVessel(Convert.ToInt32(ddl_Vessel.SelectedValue));
        //if (dt32.Rows.Count > 0)
        //{
        //    if (dt32.Rows[0]["IsTanker"].ToString() == "Y")
        //    {
        //        ddl_Matrix.Enabled = true;
        //    }
        //    else
        //    {
        //        ddl_Matrix.Enabled = false;
        //    }
        //}
    }
    protected void ddl_VesselType_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Load_Vessel();
        DataTable dt3 = cls_SearchReliever.Chk_VesselType(Convert.ToInt32(ddl_VesselType.SelectedValue));
        if (ddl_VesselType.SelectedIndex == 0)
        {
            ddl_Matrix.Enabled = false;
            return;
        }
        if (dt3.Rows[0]["IsTanker"].ToString() == "Y")
        {
            ddl_Matrix.Enabled = true;
        }
        else
        {
            ddl_Matrix.Enabled = false;
        }
    }
    protected void btn_Search_Click(object sender, EventArgs e)
    {
        bindvesselplanningGrid();
    }
    public void bindvesselplanningGrid()
    {
        int RelType = 0;
        string VesselType;
        string OwnerPool;
        string Rank;
        string MatrixTankers;
        string BNationality;
        string FamilyMember;
        string RecruitingOffice;
        string Nationality;
        string Mode;

        DataSet ds;
        RelType = Convert.ToInt32(ddl_Status.SelectedValue);
        VesselType = (ddl_VesselType.SelectedValue.Trim() == "0") ? "" : ddl_VesselType.SelectedValue.Trim();        
        OwnerPool = (ddl_OwnerPool.SelectedValue.Trim() == "0") ? "" : ddl_OwnerPool.SelectedValue.Trim();
        Rank = (ddl_Rank.SelectedValue.Trim() == "0") ? "" : ddl_Rank.SelectedValue.Trim();
        MatrixTankers = (ddl_Matrix.SelectedValue.Trim() == "0") ? "" : ddl_Matrix.SelectedValue.Trim();
        BNationality = (chk_BON.Checked) ? "Y" : "N";
        FamilyMember = (chkfamily.Checked) ? "Y" : "N";
        RecruitingOffice = (dd_RecOff.SelectedValue.Trim() == "") ? "" : dd_RecOff.SelectedValue.Trim();
        Nationality = (dd_Nationality.SelectedValue.Trim() == "") ? "" : dd_Nationality.SelectedValue.Trim();
        Mode = (r1.Checked) ? "O" : "V";

        if (chkfamily.Checked == false)
        {
            ds = NewPlanning.getRelievers(OwnerPool, BNationality, VesselType, "", MatrixTankers, Rank, RelType, Convert.ToInt32((chk_Exclude.Checked) ? "1" : "0"), txt_FirstName.Text.Trim(), txt_LastName.Text.Trim(), txt_EmpNo.Text.Trim(), RecruitingOffice, Nationality, PlanningVesselId, Mode,Common.CastAsInt32(Session["loginid"]));

            //lblGradingFor.Text = (Mode == "V") ? ViewState["CurrentVesselCode"].ToString() : ViewState["CurrentOwnerCode"].ToString();

            string Filter = "";
            DataView dv = ds.Tables[0].DefaultView;

            if (CheckBoxOR.Checked)
            {
                Filter += "  (OwnerRep = 'A' OR OwnerRep = 'B') ";
            }
            if (CheckBoxCH.Checked)
            {
                Filter += (Filter.Trim() == "") ? "" : " AND " + "  (Charterer = 'A' OR Charterer = 'B') ";
            }
            if (CheckBoxTS.Checked)
            {
                Filter += (Filter.Trim() == "") ? "" : " AND " + "  (TechSupdt = 'A' OR TechSupdt = 'B') ";
            }
            if (CheckBoxFM.Checked)
            {
                Filter += (Filter.Trim() == "") ? "" : " AND " + "  (FleetMgr = 'A' OR FleetMgr = 'B') ";
            }
            if (CheckBoxMS.Checked)
            {
                Filter += (Filter.Trim() == "") ? "" : " AND " + "  (MarineSupdt = 'A' OR MarineSupdt = 'B') ";
            }

            dv.RowFilter = Filter;

            rpt_CrewList.DataSource = dv.ToTable();
            rpt_CrewList.DataBind();

        }
        else
        {
            ds = NewPlanning.getFamilyMemberRelivers();
            rpt_CrewList.DataSource = ds.Tables[0];
            rpt_CrewList.DataBind();
        }
        
    }   
    protected void Main_Click(object sender, EventArgs e)
    {
        int res;
        int crewid, vesselid, crewrankid;
        crewid = Convert.ToInt32(Session["PCid"].ToString());
        vesselid = Convert.ToInt32(Session["PVid"].ToString());
        crewrankid = Convert.ToInt32(Session["CrewRankId"].ToString());
        if (NewPlanning.Check_RelieverStatus(crewid, vesselid) == 1)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "c", "alert('This CrewMember is already a Reliever against a Crew Member in Relief Planning.');", true);
            return;
        }
        if (NewPlanning.Check_RelieverStatus(crewid, vesselid) == 2)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "c", "alert('This CrewMember is already Planned for This Vessel.');", true);              
            return;
        }
        if (txt_PRemarks.Text.Length > 255) { txt_PRemarks.Text.Substring(0, 255); }
        //res = NewPlanning.Add_Planning(crewid, vesselid,ddl_Port.SelectedValue,txt_PRemarks.Text.Substring(0,255));
        //Navin-Need to fix this
        res = NewPlanning.Add_Planning(crewid, vesselid, txt_PRemarks.Text, Convert.ToInt32(Session["loginid"].ToString()), crewrankid, "");

        ScriptManager.RegisterStartupScript(this, this.GetType(), "c", "alert('CrewMember added successfully.');", true);

        bindvesselplanningGrid();
    }
    //protected void btn_Save_Click(object sender, EventArgs e)
    //{
    //}
    //protected void GridView1_PreRender(object sender, EventArgs e)
    //{
    //    if (Convert.ToInt32(ddl_Status.SelectedValue) == 5)
    //    {
    //        for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
    //        {
    //            GridView1.Rows[i].BackColor = System.Drawing.Color.FromName("#fcc2bc");
    //        }
    //    }
    //}
    
    protected void btnAssign_Click(object sender, EventArgs e)
    {
        bool res1;
        res1 = false;        
        DataTable dtroleid = SearchSignOff.getCrewRoleId(Convert.ToInt32(Session["loginid"].ToString()));
        foreach (DataRow dr in dtroleid.Rows)
        {
            if (Convert.ToInt32(dr["RoleId"]) != 4)
            {
                ImageButton btn = ((ImageButton)sender);
                int Crewid = Convert.ToInt32(btn.CommandArgument);
                int RankId = Convert.ToInt32(btn.Attributes["RankId"]);                

                res1 = (cls_SearchReliever.IS_DOCUMENTS_AVAILABLE(PlanningVesselId, -1, RankId, Crewid) == 1);
                txt_PRemarks.Text = "";
                //-------------------
                if (res1)
                {
                    lbl_prompt.Text = "Some Documents are Missing.Do you want to continue?";
                    md1.Show();
                    Session["PCid"] = Crewid.ToString();
                    Session["PVid"] = PlanningVesselId;
                    Session["CrewRankId"] = RankId.ToString();
                    return;
                }
                if (NewPlanning.Check_RelieverStatus(Crewid, PlanningVesselId) == 1)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "c", "alert('This CrewMember is already a Reliever against a Crew Member in Relief Planning.');", true);
                    return;
                }
                if (NewPlanning.Check_RelieverStatus(Crewid, PlanningVesselId) == 2)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "c", "alert('This CrewMember is already Planned for This Vessel.');", true);
                    return;
                }
                lbl_prompt.Text = "Please Fill the Details.";
                md1.Show();
                Session["PCid"] = Crewid.ToString();
                Session["PVid"] = PlanningVesselId;
                Session["CrewRankId"] = RankId.ToString();
                return;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "c", "alert('ReadOnly Users Are Not Authorized to Add Crew Members.');", true);
            }
        }
    }
    
}