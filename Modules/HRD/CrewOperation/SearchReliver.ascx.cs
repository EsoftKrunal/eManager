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

public partial class CrewOperation_SearchReliver : System.Web.UI.UserControl
{
    Authority Auth;
    private string _VesselType;
    private string _Vessel;
    private string _Rank;
    private string _OwnerPool;
    private string _RecruitingOffice;
    private string _Nationality;
    private CrewOperation_SearchSignOff _Ctrl;

    public string VesselType
    {
        get
        {
            return (ddl_VesselType.SelectedValue == "0") ? "" : ddl_VesselType.SelectedValue;
        }
        set
        {
            _VesselType = value;
        }
    }
    public string Vessel
    {
        get
        {
            return (ddl_Vessel.SelectedValue == "0") ? "" : ddl_Vessel.SelectedValue;
        }
        set
        {
            _Vessel = value;
        }
    }
    public string Rank
    {
        get
        {
            return (ddl_Rank.SelectedValue == "0") ? "" : ddl_Rank.SelectedValue;
        }
        set
        {
            _Rank = value;
        }
    }
    public string OwnerPool
    {
        get
        {
            return (ddl_OwnerPool.SelectedValue == "0") ? "" : ddl_OwnerPool.SelectedValue;
        }
        set
        {
            _OwnerPool = value;
        }
    }
    public string Matrix
    {
        get { return (ddl_Matrix.SelectedValue == "0") ? "" : ddl_Matrix.SelectedValue; }
    }
    public string BNationality
    {
        get { return (chk_BON.Checked) ? "Y" : "N"; }
    }
    public string RecruitingOffice
    {
        get
        {
            return (ddl_RecOff.SelectedValue == "0") ? "" : ddl_RecOff.SelectedValue;
        }
        set
        {
            _RecruitingOffice = value;
        }
    }
    public string Nationality
    {
        get
        {
            return (ddl_Nationality.SelectedValue == "0") ? "" : ddl_Nationality.SelectedValue;
        }
        set
        {
            _Nationality = value;
        }
    }
    public CrewOperation_SearchSignOff Ctrl
    {
        get { return _Ctrl; }
        set { _Ctrl = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.chk_BON.Attributes.Add("onKeyPress", "javascript:if(event.keyCode==13){document.getElementById('" + btn_Search.UniqueID + "').focus();}");
        this.chk_Exclude.Attributes.Add("onKeyPress", "javascript:if(event.keyCode==13){document.getElementById('" + btn_Search.UniqueID + "').focus();}");
        this.txt_EmpNo.Attributes.Add("onKeyPress", "javascript:if(event.keyCode==13){document.getElementById('" + btn_Search.UniqueID + "').focus();}");
        this.txt_FirstName.Attributes.Add("onKeyPress", "javascript:if(event.keyCode==13){document.getElementById('" + btn_Search.UniqueID + "').focus();}");
        this.txt_LastName.Attributes.Add("onKeyPress", "javascript:if(event.keyCode==13){document.getElementById('" + btn_Search.UniqueID + "').focus();}");
        this.ddl_Matrix.Attributes.Add("onKeyPress", "javascript:if(event.keyCode==13){document.getElementById('" + btn_Search.UniqueID + "').focus();}");
        this.ddl_OwnerPool.Attributes.Add("onKeyPress", "javascript:if(event.keyCode==13){document.getElementById('" + btn_Search.UniqueID + "').focus();}");
        this.ddl_Rank.Attributes.Add("onKeyPress", "javascript:if(event.keyCode==13){document.getElementById('" + btn_Search.UniqueID + "').focus();}");
        this.ddl_Status.Attributes.Add("onKeyPress", "javascript:if(event.keyCode==13){document.getElementById('" + btn_Search.UniqueID + "').focus();}");
        this.ddl_Vessel.Attributes.Add("onKeyPress", "javascript:if(event.keyCode==13){document.getElementById('" + btn_Search.UniqueID + "').focus();}");
        //this.ddl_VesselType.Attributes.Add("onKeyPress", "javascript:if(event.keyCode==13){document.getElementById('" + btn_Search.UniqueID + "').focus();}");
        Main.Text = "";
        Ctrl.Error_Label.Text= "";
        ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 2);
        OBJ.Invoke();
        Session["Authority"] = OBJ.Authority;
        Auth = OBJ.Authority;
        if (!(IsPostBack))
        {
            Session["S_VesselId"] = null;
            Session["S_RankId"] = null;
            
            //Load_Port();
            Load_OwnerPool();
            Load_Vessel();
            Load_Vesseltype();
            Load_Rank();
            Load_Matrix();
            Load_Status();
            Load_Nationality();
            Load_RecruitingOffice();
            ddl_Matrix.Enabled = false;
            ddl_Year.Items.Add( new ListItem("Select","0"));
            ddl_Year.Items.Add(new ListItem(DateTime.Today.Year.ToString(), DateTime.Today.Year.ToString()));
            ddl_Year.Items.Add(new ListItem((DateTime.Today.Year + 1).ToString(), (DateTime.Today.Year + 1).ToString()));
        }
        Handle_Authority();
    }

    #region OldCode
    #endregion

    #region PageLoaderControl

    private void Handle_Authority()
    {

        GridView1.Columns[0].Visible = Auth.isAdd;
    }

    private void Load_Status()
    {
        DataSet ds = cls_SearchReliever.getMasterData("vw_PlanningStatus", "StatusId", "StatusName");
        ddl_Status.DataSource = ds.Tables[0];
        ddl_Status.DataTextField = "StatusName";
        ddl_Status.DataValueField = "StatusId";
        ddl_Status.DataBind();
    }
    private void Load_Rank()
    {
        DataSet ds = cls_SearchReliever.getMasterData("Rank", "RankId", "RankCode");
        ddl_Rank.DataSource = ds.Tables[0];
        ddl_Rank.DataTextField = "RankCode";
        ddl_Rank.DataValueField = "RankId";
        ddl_Rank.DataBind();
        ddl_Rank.Items.Insert(0, new ListItem("< Select >", "0"));
    }
    //private void Load_Port()
    //{
    //    DataSet ds = cls_SearchReliever.getMasterData("Port", "PortId", "PortName");
    //    ddl_Port.DataSource = ds.Tables[0];
    //    ddl_Port.DataTextField = "PortName";
    //    ddl_Port.DataValueField = "PortId";
    //    ddl_Port.DataBind();
    //    ddl_Port.Items.Insert(0, new ListItem("< Select >", "0"));
    //}
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
        //    //ddl_Vessel.Items.Clear();
        //    //DataSet ds = cls_SearchReliever.get_Vessels(0);
        //    //ddl_Vessel.DataSource = ds.Tables[0];
        //    //ddl_Vessel.DataTextField = "VesselName";
        //    //ddl_Vessel.DataValueField = "VesselId";
        //    //ddl_Vessel.DataBind();
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
        DataSet ds = cls_SearchReliever.getMasterData("VesselType", "VesselTypeId", "VesselTypeName");
        ddl_VesselType.DataSource = ds.Tables[0];
        ddl_VesselType.DataTextField = "VesselTypeName";
        ddl_VesselType.DataValueField = "VesselTypeId";
        ddl_VesselType.DataBind();
        ddl_VesselType.Items.Insert(0, new ListItem("< Select >", "0"));
    }
    private void Load_OwnerPool()
    {
        DataSet ds = cls_SearchReliever.getMasterData("OwnerPool", "OwnerPoolId", "OwnerPoolName");
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
    private void Load_Nationality()
    {
        ddl_Nationality.DataSource = cls_SearchReliever.getMasterData("Country", "CountryId", "CountryName");
        ddl_Nationality.DataTextField = "CountryName";
        ddl_Nationality.DataValueField = "CountryId";
        ddl_Nationality.DataBind();
        ddl_Nationality.Items.Insert(0, new ListItem("< All >", ""));
    }
    private void Load_RecruitingOffice()
    {
        DataSet ds = cls_SearchReliever.getMasterData("RecruitingOffice", "RecruitingOfficeId", "RecruitingOfficeName");
        ddl_RecOff.DataSource = ds.Tables[0];
        ddl_RecOff.DataTextField = "RecruitingOfficeName";
        ddl_RecOff.DataValueField = "RecruitingOfficeId";
        ddl_RecOff.DataBind();
        ddl_RecOff.Items.Insert(0, new ListItem("< All >", ""));
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

        if (dt3.Rows.Count > 0)
        {
            if (dt3.Rows[0]["IsTanker"].ToString() == "Y")
            {
                ddl_Matrix.Enabled = true;
            }
            else
            {
                ddl_Matrix.Enabled = false;
            }
        }
    }
    protected void btn_Search_Click(object sender, EventArgs e)
    {
        bindReliefplanningGrid(GridView1.Attributes["MySort"]);  
    }
    public void bindReliefplanningGrid(String Sort)
    {
        int RelType = 0;
        DataSet ds;
        RelType = Convert.ToInt32(ddl_Status.SelectedValue);
        ds = cls_SearchReliever.getRelievers(OwnerPool, BNationality, VesselType, Vessel, Matrix, Rank, RelType, Convert.ToInt32((chk_Exclude.Checked) ? "1" : "0"),txt_FirstName.Text.Trim(),txt_LastName.Text.Trim(),txt_EmpNo.Text.Trim(),RecruitingOffice,Nationality,ddl_Month.SelectedValue,ddl_Year.SelectedValue);
        ds.Tables[0].DefaultView.Sort = Sort;
        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();
        GridView1.Attributes.Add("MySort", Sort);
    }
    protected void on_Sorting(object sender, GridViewSortEventArgs e)
    {
        bindReliefplanningGrid(e.SortExpression);
    }
    protected void on_Sorted(object sender, EventArgs e)
    {

    }
    protected void Row_Command(object sender, GridViewCommandEventArgs e)
    {
        bool res1, res2;
        res1 = false;
        res2 = false;
        if (e.CommandName == "Assign")
        {
            DataTable dtroleid = SearchSignOff.getCrewRoleId(Convert.ToInt32(Session["loginid"].ToString()));
            foreach (DataRow dr in dtroleid.Rows)
            {
                if (Convert.ToInt32(dr["RoleId"]) != 4)
                {
                    HiddenField hfd;
                    int GroupId1, GroupId2;
                    GridViewRow row = (GridViewRow)((Control)e.CommandSource).Parent.Parent;
                    int res;
                    int ReliverId, ReliverRankId, ReliveeId, ReliveeRankId, Loginid;
                    hfd = (HiddenField)GridView1.Rows[row.RowIndex].FindControl("lbl_HiddenCrewId");
                    ReliverId = Convert.ToInt32(hfd.Value);
                    ////*********** CODE TO CHECK FOR BRANCHID ***********
                    //string xp = Alerts.Check_BranchId(ReliverId);
                    //if (xp.Trim() != "")
                    //{
                    //    GridView1.SelectedIndex = -1;
                    //    Ctrl.Error_Label.Text = xp;
                    //    return;
                    //}
                    ////************
                    hfd = (HiddenField)GridView1.Rows[row.RowIndex].FindControl("lbl_HiddenRankId");
                    ReliverRankId = Convert.ToInt32(hfd.Value);
                    if (Ctrl.SelectedIndex < 0)
                    {
                        Ctrl.Error_Label.Text = "Please Select a Crew Member.";
                        return;
                    }
                    else
                    {
                        ReliveeId = Convert.ToInt32(Ctrl.crewid);
                        ReliveeRankId = Convert.ToInt32(Ctrl.RankId);
                        GroupId1 = cls_SearchReliever.getRankGroupId(Convert.ToInt32(ReliveeRankId));
                        GroupId2 = cls_SearchReliever.getRankGroupId(Convert.ToInt32(ReliverRankId));

                        if (ReliveeId == ReliverId)
                        {
                            Ctrl.Error_Label.Text = "Same Person Can't Relieve Himself.";
                            return;
                        }
                        // RES1 - ALL THE DOCUMENT ARE NOT AVAILABLE FOR THAT VESSEL 
                        // RES2 - RANK GROUP NOT MATCH
                        res1 = (cls_SearchReliever.IS_DOCUMENTS_AVAILABLE(-1, ReliveeId, ReliveeRankId, ReliverId) == 1);
                        res2 = (GroupId1 != GroupId2);
                        //--------------
                        //ddl_Port.SelectedIndex = 0;
                        txt_PRemarks.Text = ""; 
                        if ((res1 == true) && (res2 == true))
                        {
                            md1.Show();
                            lbl_MessText.Text = "Ranks does not match And Some Documents Are Also Missing. Do You Want To Continue?";
                            Session["RId"] = ReliveeId.ToString();
                            Session["RNId"] = ReliveeRankId.ToString();
                            Session["RId1"] = ReliverId.ToString();
                            Session["RNId1"] = ReliverRankId.ToString();
                            return;
                        }
                        else if ((res1 == true) && (res2 == false))
                        {
                            md1.Show();
                            lbl_MessText.Text = "Some Documents Are Also Missing. Do You Want To Continue?";
                            Session["RId"] = ReliveeId.ToString();
                            Session["RNId"] = ReliveeRankId.ToString();
                            Session["RId1"] = ReliverId.ToString();
                            Session["RNId1"] = ReliverRankId.ToString();
                            return;
                        }
                        else if ((res1 == false) && (res2 == true))
                        {
                            md1.Show();
                            lbl_MessText.Text = "Ranks Does Not Match. Do You Want To Continue?";
                            Session["RId"] = ReliveeId.ToString();
                            Session["RNId"] = ReliveeRankId.ToString();
                            Session["RId1"] = ReliverId.ToString();
                            Session["RNId1"] = ReliverRankId.ToString();
                            return;
                        }
                        else
                        {
                            md1.Show();
                            lbl_MessText.Text = "Please Provide the Details Below.";
                            Session["RId"] = ReliveeId.ToString();
                            Session["RNId"] = ReliveeRankId.ToString();
                            Session["RId1"] = ReliverId.ToString();
                            Session["RNId1"] = ReliverRankId.ToString();
                            return;
                         }
                        //----------- below code is goes disabled while we added the above code.
                        //if (NewPlanning.Check_RelieverStatus1(ReliverId, ReliveeId) > 0)
                        //{
                        //    Ctrl.Error_Label.Text = "This CrewMember is already Planned for the Same Vessel.";
                        //    return;
                        //}

                        //res = cls_SearchReliever.Add_Reliver(ReliverId, ReliverRankId, ReliveeId, ReliveeRankId);
                        //if (res == -1)
                        //{
                        //    Ctrl.Error_Label.Text = "There are Already Two Relievers Exists.";
                        //}
                        //if (res == 2)
                        //{
                        //    Ctrl.Error_Label.Text = "You can't Assign same Reliever Twice";
                        //}
                        //Ctrl.Show_Grid();
                    }
                }
                else
                {
                    Ctrl.Error_Label.Text = "ReadOnly Users Are Not Authorized to Assign any Reliever.";
                }
            }
        }
    }
    protected void Main_Click(object sender, EventArgs e)
    {
        int res;
        int ReliverId, ReliverRankId, ReliveeId, ReliveeRankId, Loginid;
        
        ReliveeId= Convert.ToInt32(Session["RId"].ToString());
        ReliveeRankId = Convert.ToInt32(Session["RNId"].ToString());
        ReliverId = Convert.ToInt32(Session["RId1"].ToString());
        ReliverRankId = Convert.ToInt32(Session["RNId1"].ToString());
        Loginid = Convert.ToInt32(Session["loginid"].ToString());

        if (NewPlanning.Check_RelieverStatus1(ReliverId, ReliveeId) > 0)
        {
            Ctrl.Error_Label.Text = "This CrewMember is already Planned for the Same Vessel.";
            return;
        }
        if (txt_PRemarks.Text.Length > 255) {txt_PRemarks.Text.Substring(0, 255);}
        //res = cls_SearchReliever.Add_Reliver(ReliverId, ReliverRankId, ReliveeId, ReliveeRankId, ddl_Port.SelectedValue, txt_PRemarks.Text);
        //Navin-Need to fix this
        res = cls_SearchReliever.Add_Reliver(ReliverId, ReliverRankId, ReliveeId, ReliveeRankId, txt_PRemarks.Text, Loginid, ReliverRankId, "");
        if (res == -1)
        {
            Ctrl.Error_Label.Text = "There are Already Two Relievers Exists.";
        }
        if (res == 2)
        {
            Ctrl.Error_Label.Text = "You can't Assign same Reliver Twice";
        }
        Ctrl.Show_Grid();
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
}