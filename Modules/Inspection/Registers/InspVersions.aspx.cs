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

public partial class Registers_InspectionGroupVersions : System.Web.UI.Page
{
    int LoginId=0;
    Authority Auth;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        //--------------------------------- PAGE ACCESS AUTHORITY ----------------------------------------------------
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 155);
        if (chpageauth <= 0)
            Response.Redirect("blank.aspx");
        //------------------------------------------------------------------------------------------------------------
   
        if (Session["loginid"] == null)
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Default.aspx';", true);
        }
        lbl_GrpVersion_Message.Text = "";
        if (Session["loginid"] != null)
        {
            ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 12);
            OBJ.Invoke();
            Session["Authority"] = OBJ.Authority;
            Auth = OBJ.Authority;
        }
        if (!Page.IsPostBack)
        {
            bindInspectionGroupDDL();
            btn_Save_GrpVersion.Visible = false;
            pnl_GrpVersion.Visible = false;   
            btn_Cancel_GrpVersion.Visible = false;
            ddl_InspGroup_SelectedIndexChanged(sender, e);
        }
    }
    public void bindInspectionGroupDDL()
    {
        DataSet ds1 = Inspection_Master.getMasterData("m_InspectionGroup", "Id", "(Code+ ' - ' +Name) as Name");
        if (ds1.Tables[0].Rows.Count > 0)
        {
            this.ddl_InspGroup.DataSource = ds1.Tables[0];
            this.ddl_InspGroup.DataValueField = "Id";
            this.ddl_InspGroup.DataTextField = "Name";
            this.ddl_InspGroup.DataBind();
        }
        else
        {
            this.ddl_InspGroup.Items.Insert(0, new ListItem("<Select>", "0"));
        }
        //this.ddl_InspGroup.Items.Insert(0, new ListItem("<Select>", "0"));
    }
    public void bindBlankGrid()
    {
        DataTable dt = new DataTable();
        DataRow dr;
        dt.Columns.Add("VERSIONID");
        dt.Columns.Add("VERSIONNAME");
        dt.Columns.Add("APPLYDATE");

        for (int i = 0; i < 8; i++)
        {
            dr = dt.NewRow();
            dt.Rows.Add(dr);
            dt.Rows[dt.Rows.Count - 1][0] = "";
            dt.Rows[dt.Rows.Count - 1][1] = "";
            dt.Rows[dt.Rows.Count - 1][2] = "";
        }

        GridView_GrpVersion.DataSource = dt;
        GridView_GrpVersion.DataBind();
        GridView_GrpVersion.SelectedIndex = -1;
    }
    public void bindGrpVersionsGrid()
    {
        DataTable dt1 = Budget.getTable("SELECT VERSIONID,VERSIONNAME,replace(convert(varchar,APPLYDATE,106),' ','-') as APPLYDATE FROM dbo.m_InspGroupVersions WHERE GROUPID=" + ddl_InspGroup.SelectedValue).Tables[0];
        if (dt1.Rows.Count > 0)
        {
            this.GridView_GrpVersion.DataSource = dt1;
            this.GridView_GrpVersion.DataBind();
            HiddenFieldGridRowCount.Value = dt1.Rows.Count.ToString();
        }
        else
        {
            bindBlankGrid();
            HiddenFieldGridRowCount.Value = "0";
        }
    }
    protected void ddl_InspGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridView_GrpVersion.SelectedIndex = -1;
        bindGrpVersionsGrid();
    }
    protected void btn_New_GrpVersion_Click(object sender, EventArgs e)
    {
        txtVersionNo.Text = "";
        txtplandate.Text = "";
        HiddenGrpVersion.Value = "";  
        pnl_GrpVersion.Visible = true;
        btn_Save_GrpVersion.Visible = true;
        btn_Cancel_GrpVersion.Visible = true; 
    }
    protected void Show_Record_GrpVersions(int GrpVersionId, object sender, EventArgs e)
    {
        pnl_GrpVersion.Visible = true;
        btn_Save_GrpVersion.Visible = true;
        btn_Cancel_GrpVersion.Visible = true;
        HiddenGrpVersion.Value = GrpVersionId.ToString();
        DataTable dt1 = Budget.getTable("SELECT VERSIONID,VERSIONNAME,replace(convert(varchar,APPLYDATE,106),' ','-') as APPLYDATE FROM dbo.m_InspGroupVersions WHERE GROUPID=" + ddl_InspGroup.SelectedValue + " AND VERSIONID=" + HiddenGrpVersion.Value).Tables[0];
        if(dt1.Rows.Count >0)
        {
            txtVersionNo.Text = dt1.Rows[0][1].ToString();
            txtplandate.Text = dt1.Rows[0][2].ToString();
        }
    }
    protected void GridView_GrpVersion_RowEditing(object sender, GridViewEditEventArgs e)
    {
        HiddenField hfdGrpVersions;
        hfdGrpVersions = (HiddenField)GridView_GrpVersion.Rows[e.NewEditIndex].FindControl("Hidden_GrpVersionId");
        int id = Convert.ToInt32(hfdGrpVersions.Value.ToString());
        Show_Record_GrpVersions(id, sender,e);
        GridView_GrpVersion.SelectedIndex = e.NewEditIndex;
        
    }
    protected void btn_Cancel_GrpVersion_Click(object sender, EventArgs e)
    {
        pnl_GrpVersion.Visible = false;
        btn_Save_GrpVersion.Visible = false;
        btn_Cancel_GrpVersion.Visible = false;
        HiddenGrpVersion.Value = "";
        GridView_GrpVersion.SelectedIndex = -1;
        //ddl_InspGroup_SelectedIndexChanged(sender, e);
        //ddlChapterName_SelectedIndexChanged(sender, e);
    }
    protected void GridView_GrpVersion_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView_GrpVersion.PageIndex = e.NewPageIndex;
        GridView_GrpVersion.SelectedIndex = -1;
        bindGrpVersionsGrid();
    }
    protected void btn_Save_GrpVersion_Click(object sender, EventArgs e)
    {
        DataTable dt1;
        int intGrpVersionId = -1;
        int intCreatedBy = 0;
        int intModifiedBy = 0;

        if (HiddenGrpVersion.Value.ToString().Trim() == "")
        {
            intCreatedBy = Convert.ToInt32(Session["loginid"].ToString());
        }
        else
        {
            intGrpVersionId = Convert.ToInt32(HiddenGrpVersion.Value);
            intModifiedBy = Convert.ToInt32(Session["loginid"].ToString());
        }
        //----------------
        int VersionId = 0;
        try
        {
            VersionId = Int32.Parse(HiddenGrpVersion.Value);
        }
        catch { }
        //----------------
        try
        {
            Budget.getTable("EXEC PR_ADMS_Versions " + VersionId.ToString() + "," + ddl_InspGroup.SelectedValue + ",'" + txtVersionNo.Text + "','" + txtplandate.Text + "'");
            lbl_GrpVersion_Message.Text = "Record Successfully Saved.";
        }
        catch (Exception ex)
        {
            lbl_GrpVersion_Message.Text = "Transaction Failed.";
        }
        bindGrpVersionsGrid();
        btn_New_GrpVersion_Click(sender, e);
        btn_Cancel_GrpVersion_Click(sender, e);
    }


    protected void GridView_GrpVersion_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Modify")
        {
            GridViewRow row = (GridViewRow)(((ImageButton)sender).NamingContainer);
            int Rowindx = row.RowIndex;
            HiddenField hfdGrpVersions;
            hfdGrpVersions = (HiddenField)GridView_GrpVersion.Rows[Rowindx].FindControl("Hidden_GrpVersionId");
            int id = Convert.ToInt32(hfdGrpVersions.Value.ToString());
            Show_Record_GrpVersions(id, sender, e);
            GridView_GrpVersion.SelectedIndex = Rowindx;
        }
    }

    protected void btnEditInsGroup_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow row = (GridViewRow)(((ImageButton)sender).NamingContainer);
        int Rowindx = row.RowIndex;
        HiddenField hfdGrpVersions;
        hfdGrpVersions = (HiddenField)GridView_GrpVersion.Rows[Rowindx].FindControl("Hidden_GrpVersionId");
        int id = Convert.ToInt32(hfdGrpVersions.Value.ToString());
        Show_Record_GrpVersions(id, sender, e);
        GridView_GrpVersion.SelectedIndex = Rowindx;
    }
    }
