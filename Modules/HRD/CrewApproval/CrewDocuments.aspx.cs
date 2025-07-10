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
using System.IO;
using System.Text;
using System.Xml;

public partial class CrewApproval_CrewDocuments : System.Web.UI.Page
{
    Authority Auth;
    string Mode;

    protected void rbl_Type_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbl_Type.SelectedIndex == 0)
        { 
            pnlRankWise.Visible =true;
            pnlFlagStateWise.Visible=false;
            pnlVesselWise.Visible=false; 
        }
        else if (rbl_Type.SelectedIndex == 1)
        {
            pnlRankWise.Visible = false;
            pnlFlagStateWise.Visible=true;
            pnlVesselWise.Visible=false; 
        }
        else
        { 
            pnlRankWise.Visible =false;
            pnlFlagStateWise.Visible=false;
            pnlVesselWise.Visible=true; 
        }
    }
    protected void bindVessels()
    {
        DataSet ds = Budget.getTable("select VesselID,VesselName as Name from dbo.Vessel where VesselStatusid<>2  ORDER BY VESSELNAME");
        ddl_VW_Vessel.DataSource = ds.Tables[0];
        ddl_VW_Vessel.DataValueField = "VesselId";
        ddl_VW_Vessel.DataTextField = "Name";
        ddl_VW_Vessel.DataBind();
        ddl_VW_Vessel.Items.Insert(0, new ListItem("<Select>", "0"));
    }
    protected void bindFS()
    {
        DataSet ds = Budget.getTable("select distinct CountryId,CountryName from vessel v inner join country c on c.countryid=v.flagstateid order by CountryName");
        ddl_VW_FS.DataSource = ds.Tables[0];
        ddl_VW_FS.DataValueField = "CountryId";
        ddl_VW_FS.DataTextField = "CountryName";
        ddl_VW_FS.DataBind();
        ddl_VW_FS.Items.Insert(0, new ListItem("<Select>", "0"));
    }
    private void BindRankDropDown()
    {
        ProcessSelectRank obj = new ProcessSelectRank();
        obj.Invoke();
        ddl_RWRankFilter.DataSource = obj.ResultSet.Tables[0];
        ddl_RWRankFilter.DataTextField = "RankName";
        ddl_RWRankFilter.DataValueField = "RankId";
        ddl_RWRankFilter.DataBind();

        //ddl_VW_Rank.DataSource = obj.ResultSet.Tables[0];
        //ddl_VW_Rank.DataTextField = "RankName";
        //ddl_VW_Rank.DataValueField = "RankId";
        //ddl_VW_Rank.DataBind();

    }
    protected void Refresh(object sender, EventArgs e)
    {
        int Mode = Common.CastAsInt32(txtMode.Text);
        
        if (Mode==1)
            VW_Bind_grid();

        if (Mode == 2)
            RW_Bind_grid(); 

        if (Mode == 3)
            FS_Bind_grid();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 29);
        if (chpageauth <= 0)
        {
            Response.Redirect("../AuthorityError.aspx");
        }
        //**********
        ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 1);
        OBJ.Invoke();
        Session["Authority"] = OBJ.Authority;
        Auth = OBJ.Authority;
        try
        {
            if (Session["VMode"] != null)
                Mode = Session["VMode"].ToString();
            else
                Mode = "New";
        }
        catch { Mode = "New"; }

        if (!(IsPostBack))
        {
            bindVessels();
            bindFS();
            BindRankDropDown();

            VW_Bind_grid();
            RW_Bind_grid();
        }
        try
        {
            //txtFormerVesselName.Text = Session["FormerName"].ToString();
            //ddlFlagStateName.SelectedValue = Session["FlagStateId"].ToString();
        }
        catch { }
    }

    // vessel wise --------------------------
    protected void ddlVWRank_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        VW_Bind_grid();
    }
    protected void ddlVessel_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        VW_Bind_grid();
    }
    private void VW_Bind_grid()
    {
        DataTable dt;
        dt = cls_VesselDocuments.SelectVesselDocuments(Common.CastAsInt32(ddl_VW_Vessel.SelectedValue));
        rprData1.DataSource = dt;
        rprData1.DataBind();
    }
    protected void DeleteSingle_ByVesel(object sender, EventArgs e)
    {
        string id = ((ImageButton)sender).CommandArgument;
        Common.Execute_Procedures_Select_ByQueryCMS("DELETE FROM VESSELDOCUMENTS WHERE VESSELDOCUMENTID=" + id);
        VW_Bind_grid();
    }

    // rank wise --------------------------
    protected void ddlRank_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        RW_Bind_grid();
    }
    private void RW_Bind_grid()
    {
        DataTable dt;
        dt = cls_VesselDocuments.SelectRankDocuments(Common.CastAsInt32(ddl_RWRankFilter.SelectedValue));
        rptData.DataSource = dt;
        rptData.DataBind();
    }
    protected void DeleteSingle_ByRank(object sender, EventArgs e)
    {
        string id = ((ImageButton)sender).CommandArgument;
        Common.Execute_Procedures_Select_ByQueryCMS("DELETE FROM RANKDOCUMENTS WHERE RANKDOCUMENTID=" + id); 
        RW_Bind_grid();
    }
    
    // flag state wise --------------------
    protected void ddlFSRank_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        FS_Bind_grid();
    }
    protected void ddlFS_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        FS_Bind_grid();
    }
    private void FS_Bind_grid()
    {
        DataTable dt;
        dt = cls_VesselDocuments.SelectFSDocuments(Common.CastAsInt32(ddl_VW_FS.SelectedValue));
        rptDataFS.DataSource = dt;
        rptDataFS.DataBind();
    }
    protected void DeleteSingle_ByFS(object sender, EventArgs e)
    {
            string id = ((ImageButton)sender).CommandArgument;
        Common.Execute_Procedures_Select_ByQueryCMS("DELETE FROM flagstatedocuments WHERE FSDOCUMENTID=" + id);
        FS_Bind_grid();
    }
}
