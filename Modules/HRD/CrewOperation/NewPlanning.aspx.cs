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

public partial class CrewOperation_NewPlanning : System.Web.UI.Page
{
    Authority Auth;
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        Session["PageName"] = " - Vessel Planning";
        this.txt_FirstName.Attributes.Add("onKeyPress", "javascript:if(event.keyCode==13){document.getElementById('" + btn_Search.UniqueID + "').focus();}");
        this.txt_LastName.Attributes.Add("onKeyPress", "javascript:if(event.keyCode==13){document.getElementById('" + btn_Search.UniqueID + "').focus();}");
        this.txt_EmpNo.Attributes.Add("onKeyPress", "javascript:if(event.keyCode==13){document.getElementById('" + btn_Search.UniqueID + "').focus();}");

        this.chk_BON.Attributes.Add("onKeyPress", "javascript:if(event.keyCode==13){document.getElementById('" + btn_Search.UniqueID + "').focus();}");
        this.chk_Exclude.Attributes.Add("onKeyPress", "javascript:if(event.keyCode==13){document.getElementById('" + btn_Search.UniqueID + "').focus();}");
        this.chkfamily.Attributes.Add("onKeyPress", "javascript:if(event.keyCode==13){document.getElementById('" + btn_Search.UniqueID + "').focus();}");

        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 15);
        if (chpageauth <= 0)
        {
            Response.Redirect("Dummy2.aspx");
        }
        //*******************
        ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 2);
        OBJ.Invoke();
        Session["Authority"] = OBJ.Authority;
        Auth = OBJ.Authority;
            lb_msg.Text = "";
            Main.Text = ""; 
            if (!(IsPostBack))
            {
                ddl_Matrix.Enabled = false;
                //Load_Port();
                Load_OwnerPool();
                Load_Vessel();
                Load_Vesseltype();
                Load_Rank();
                Load_Matrix();
                Load_Status();
                Load_Nationality();
                Load_RecruitingOffice();
                bindddl_VesselName();
            }
            Handle_Authority();
    }
    private void Handle_Authority()
    {
        gvsearch.Columns[2].Visible = Auth.isDelete;
        GridView1.Columns[0].Visible = Auth.isAdd;
    }
    //-- Top Page
    #region PageLoaderControl
    public void bindddl_VesselName()
    {
        DataSet dt8 = SearchSignOff.getVessel(Convert.ToInt32(Session["loginid"].ToString()));
        this.ddl_VesselName.DataSource = dt8;
        this.ddl_VesselName.DataValueField = "VesselId";
        this.ddl_VesselName.DataTextField = "Name";
        this.ddl_VesselName.DataSource = dt8;
        this.ddl_VesselName.DataBind();
        ddl_VesselName.Items.Insert(0, new ListItem("< Select >", "0"));
    }
    #endregion
    protected void ddl_VesselName_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_VesselName.SelectedIndex == 0)
        {
            this.gvsearch.DataBind();
        }
        else
        {
            binddata(gvsearch.Attributes["MySort"]);
        }
    }
    private void binddata(String Sort)
    {
        DataSet ds2 = NewPlanning.getCrewDetails(ddl_VesselName.SelectedValue);
        ds2.Tables[0].DefaultView.Sort = Sort;
        this.gvsearch.DataSource = ds2.Tables[0];
        this.gvsearch.DataBind();
        gvsearch.Attributes.Add("MySort", Sort);
    }
    protected void on_Sorting1(object sender, GridViewSortEventArgs e)
    {
        binddata(e.SortExpression);
    }
    protected void on_Sorted1(object sender, EventArgs e)
    {

    }
    protected void gvsearch_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        Label Lb_CrewID;
        int res;
        if (e.CommandName == "img_Delete")
        {
            DataTable dtroleid = SearchSignOff.getCrewRoleId(Convert.ToInt32(Session["loginid"].ToString()));
            foreach (DataRow dr in dtroleid.Rows)
            {
                if (Convert.ToInt32(dr["RoleId"]) != 4)
                {
                    GridViewRow row = (GridViewRow)((Control)e.CommandSource).Parent.Parent;
                    Lb_CrewID = (Label)gvsearch.Rows[row.RowIndex].FindControl("Lb_CrewID");
                    ////*********** CODE TO CHECK FOR BRANCHID ***********
                    //string xp = Alerts.Check_BranchId(Convert.ToInt32(Lb_CrewID.Text));
                    //if (xp.Trim() != "")
                    //{
                    //    gvsearch.SelectedIndex = -1;
                    //    lb_msg.Text = xp;
                    //    return;
                    //}
                    ////************
                    res = SearchSignOff.Check_Planning_Deleted(Convert.ToInt32(ddl_VesselName.SelectedValue), Convert.ToInt32(Lb_CrewID.Text));
                    if (res == 1)
                    {
                        lb_msg.Text = "Contract has been created for this Crew Member.";
                        return;
                    }
                    else if (res == 2)
                    {
                        lb_msg.Text = "RFQ has been created for this Crew Member.";
                        return;
                    }
                    else if (res == 3)
                    {
                        lb_msg.Text = "RFQ has been created for this Crew Member.";
                        return;
                    }
                    else
                    {
                        SearchSignOff.DeleteReliver_planning(Convert.ToInt32(Lb_CrewID.Text), Convert.ToInt32(ddl_VesselName.SelectedValue));
                    }
                }
                else
                {
                    lb_msg.Text = "ReadOnly Users Are Not Authorized to Delete.";
                }
            }
        }
        binddata(gvsearch.Attributes["MySort"]);
    }
    //-- Down page
    #region PageLoaderControl
    //private void Load_Port()
    //{
    //    DataSet ds = cls_SearchReliever.getMasterData("Port", "PortId", "PortName");
    //    ddl_Port.DataSource = ds.Tables[0];
    //    ddl_Port.DataTextField = "PortName";
    //    ddl_Port.DataValueField = "PortId";
    //    ddl_Port.DataBind();
    //    ddl_Port.Items.Insert(0, new ListItem("< Select >", "0"));
    //}
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
        DataSet ds = SearchSignOff.getVessel(Convert.ToInt32(Session["loginid"].ToString()));
        ddl_Vessel.DataSource = ds.Tables[0];
        ddl_Vessel.DataValueField = "VesselId";
        ddl_Vessel.DataTextField = "VesselName1";
        ddl_Vessel.DataBind();
        ddl_Vessel.Items.Insert(0, new ListItem("< Select >", "0"));
        //if (ddl_VesselType.SelectedIndex <= 0)
        //{
        //    ddl_Vessel.Items.Clear();
        //}
        //else
        //{
        //    DataSet ds = cls_SearchReliever.get_Vessels_Login(Convert.ToInt32(ddl_VesselType.SelectedValue), Convert.ToInt32(Session["loginid"].ToString()));
        //    ddl_Vessel.DataSource = ds.Tables[0];
        //    ddl_Vessel.DataTextField = "VesselName";
        //    ddl_Vessel.DataValueField = "VesselId";
        //    ddl_Vessel.DataBind();
        //}
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
        DataTable dt32 = cls_SearchReliever.Chk_VesselType_fromVessel(Convert.ToInt32(ddl_Vessel.SelectedValue));
        if (dt32.Rows.Count > 0)
        {
            if (dt32.Rows[0]["IsTanker"].ToString() == "Y")
            {
                ddl_Matrix.Enabled = true;
            }
            else
            {
                ddl_Matrix.Enabled = false;
            }
        }
    }
    protected void ddl_VesselType_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Load_Vessel();
        DataTable dt3 = cls_SearchReliever.Chk_VesselType(Convert.ToInt32(ddl_VesselType.SelectedValue));
        if (ddl_VesselType.SelectedIndex == 0)
        {
            ddl_Matrix.Enabled =  false;
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
        bindvesselplanningGrid(GridView1.Attributes["MySort"]);
    }
    public void bindvesselplanningGrid(String Sort)
    {
        int RelType = 0;
        int VesselId = 0;
        string VesselType;
        string Vessel;
        string OwnerPool;
        string Rank;
        string MatrixTankers;
        string BNationality;
        string FamilyMember;
        string RecruitingOffice;
        string Nationality;
        string Compatibility;

        DataSet ds;
        RelType = Convert.ToInt32(ddl_Status.SelectedValue);
        VesselType = (ddl_VesselType.SelectedValue.Trim() == "0") ? "" : ddl_VesselType.SelectedValue.Trim();
        Vessel = (ddl_Vessel.SelectedValue.Trim() == "0") ? "" : ddl_Vessel.SelectedValue.Trim();
        OwnerPool = (ddl_OwnerPool.SelectedValue.Trim() == "0") ? "" : ddl_OwnerPool.SelectedValue.Trim();
        Rank = (ddl_Rank.SelectedValue.Trim() == "0") ? "" : ddl_Rank.SelectedValue.Trim();
        MatrixTankers = (ddl_Matrix.SelectedValue.Trim() == "0") ? "" : ddl_Matrix.SelectedValue.Trim();
        BNationality = (chk_BON.Checked) ? "Y" : "N";
        FamilyMember = (chkfamily.Checked) ? "Y" : "N";
        RecruitingOffice = (dd_RecOff.SelectedValue.Trim() == "") ? "" : dd_RecOff.SelectedValue.Trim();
        Nationality = (dd_Nationality.SelectedValue.Trim() == "") ? "" : dd_Nationality.SelectedValue.Trim();
        VesselId = (ddl_VesselName.SelectedValue.Trim() == "0") ? 0 : Common.CastAsInt32(ddl_VesselName.SelectedValue);
        Compatibility = "V";

        if (chkfamily.Checked == false)
        {
            ds = NewPlanning.getRelievers(OwnerPool, BNationality, VesselType, Vessel, MatrixTankers, Rank, RelType, Convert.ToInt32((chk_Exclude.Checked) ? "1" : "0"), txt_FirstName.Text.Trim(), txt_LastName.Text.Trim(), txt_EmpNo.Text.Trim(), RecruitingOffice, Nationality, VesselId, Compatibility, Convert.ToInt32(Session["loginid"].ToString()));
        }
        else
        {
            ds = NewPlanning.getFamilyMemberRelivers();
        }
        ds.Tables[0].DefaultView.Sort = Sort;
        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();
        GridView1.Attributes.Add("MySort", Sort);
    }
    protected void on_Sorting(object sender, GridViewSortEventArgs e)
    {
        bindvesselplanningGrid(e.SortExpression);
    }
    protected void on_Sorted(object sender, EventArgs e)
    {

    }
    protected void Row_Command(object sender, GridViewCommandEventArgs e)
    {
        bool res1;
        res1 = false;
        if (e.CommandName == "Assign")
        {
            DataTable dtroleid = SearchSignOff.getCrewRoleId(Convert.ToInt32(Session["loginid"].ToString()));
            foreach (DataRow dr in dtroleid.Rows)
            {
                if (Convert.ToInt32(dr["RoleId"]) != 4)
                {
                    HiddenField hfd;
                    int res;
                    int Crewid, RankId;
                    GridViewRow row = (GridViewRow)((Control)e.CommandSource).Parent.Parent;
                    //--------------------
                    hfd = (HiddenField)GridView1.Rows[row.RowIndex].FindControl("lbl_HiddenCrewId");
                    Crewid = Convert.ToInt32(hfd.Value);
                    //--------------------         
                    ////*********** CODE TO CHECK FOR BRANCHID ***********
                    //string xp = Alerts.Check_BranchId(Crewid);
                    //if (xp.Trim() != "")
                    //{
                    //    GridView1.SelectedIndex = -1;
                    //    lb_msg.Text = xp;
                    //    return;
                    //}
                    ////************
                    hfd = (HiddenField)GridView1.Rows[row.RowIndex].FindControl("lbl_HiddenRankId");
                    RankId = Convert.ToInt32(hfd.Value);
                    //-------------------
                    if (ddl_VesselName.SelectedIndex <= 0)
                    {
                        lb_msg.Text = "Please Select a Vessel Name.";
                        return;
                    }
                    else
                    {
                        res1 = (cls_SearchReliever.IS_DOCUMENTS_AVAILABLE(Convert.ToInt32(ddl_VesselName.SelectedValue), -1, RankId, Crewid) == 1);
                        //ddl_Port.SelectedIndex = 0;
                        txt_PRemarks.Text = ""; 
                        //-------------------
                        if (res1)
                        {
                            lbl_prompt.Text = "Some Documents are Missing.Do you want to continue?"; 
                            md1.Show();
                            Session["PCid"] = Crewid.ToString();
                            Session["PVid"] = ddl_VesselName.SelectedValue;
                            return;
                        }
                        if (NewPlanning.Check_RelieverStatus(Crewid, Convert.ToInt32(ddl_VesselName.SelectedValue)) == 1)
                        {
                            lb_msg.Text = "This CrewMember is already a Reliever against a Crew Member in Relief Planning.";
                            return;
                        }
                        if (NewPlanning.Check_RelieverStatus(Crewid, Convert.ToInt32(ddl_VesselName.SelectedValue)) == 2)
                        {
                            lb_msg.Text = "This CrewMember is already Planned for This Vessel.";
                            return;
                        }
                        lbl_prompt.Text = "Please Fill the Details.";
                        md1.Show();
                        Session["PCid"] = Crewid.ToString();
                        Session["PVid"] = ddl_VesselName.SelectedValue;
                        return;
                        //--------------------------- the below code is become disabled.
                        //res = NewPlanning.Add_Planning(Crewid, Convert.ToInt32(ddl_VesselName.SelectedValue));
                        //binddata(gvsearch.Attributes["MySort"]);
                    }
                }
                else
                {
                    lb_msg.Text = "ReadOnly Users Are Not Authorized to Add Crew Members.";
                }
            }
        }
    }
    protected void Main_Click(object sender, EventArgs e)
    {
        int res;
        int crewid, vesselid;
        crewid = Convert.ToInt32(Session["PCid"].ToString());
        vesselid = Convert.ToInt32(Session["PVid"].ToString());

        if (NewPlanning.Check_RelieverStatus(crewid, vesselid) == 1)
        {
            lb_msg.Text = "This CrewMember is already a Reliever against a Crew Member in Relief Planning.";
            return;
        }
        if (NewPlanning.Check_RelieverStatus(crewid, vesselid) == 2)
        {
            lb_msg.Text = "This CrewMember is already Planned for This Vessel.";
            return;
        }
        if (txt_PRemarks.Text.Length > 255) { txt_PRemarks.Text.Substring(0, 255); }
        //res = NewPlanning.Add_Planning(crewid, vesselid,ddl_Port.SelectedValue,txt_PRemarks.Text.Substring(0,255));
        //Navin-Need to fix this
        res = NewPlanning.Add_Planning(crewid, vesselid, txt_PRemarks.Text, Convert.ToInt32(Session["loginid"].ToString()), 0, "");
        binddata(gvsearch.Attributes["MySort"]);
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
    }
    protected void GridView1_PreRender(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddl_Status.SelectedValue) == 5)
        {
            for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
            {
                GridView1.Rows[i].BackColor = System.Drawing.Color.FromName("#fcc2bc");
            }
        }
    }
    protected void GV_OnRowDataBound(Object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label L1 = (Label)e.Row.FindControl("Lb_ReliverRankId");
            int Rank = Common.CastAsInt32(L1.Text);

            L1 = (Label)e.Row.FindControl("lb_ReliverID");
            int Rid = Common.CastAsInt32(L1.Text);

            //L1 = (Label)e.Row.FindControl("lblvesselid");
            int Vid = Common.CastAsInt32(ddl_VesselName.SelectedValue);

            DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("SELECT PlanningId FROM DBO.CREWVESSELPLANNINGHISTORY WHERE RELIEVERID=" + Rid.ToString() + " AND RELIEVERRANKID=" + Rank.ToString() + " AND STATUS='A' AND PLANTYPE='N' AND VESSELID=" + Vid.ToString());
            if (dt.Rows.Count > 0)
            {
                ImageButton b1 = (ImageButton)e.Row.FindControl("btnCL");
                b1.CommandArgument = dt.Rows[0][0].ToString();
            }
        }
    }
    protected void btnCL_Click(object sender, EventArgs e)
    {
        string PlanningId = ((ImageButton)sender).CommandArgument;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "c", "window.open('ViewCrewCheckList.aspx?_P=" + PlanningId + "');", true);
    }
}
