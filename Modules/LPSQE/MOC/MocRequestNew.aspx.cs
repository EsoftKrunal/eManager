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
using System.Threading;

public partial class HSSQE_MOC_MocRequestNew : System.Web.UI.Page
{
    public int LoginId
    {
        get { return Common.CastAsInt32(ViewState["loginid"]); }
        set { ViewState["loginid"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        
        lblMsg.Text = "";
        if (!IsPostBack)
        {
            LoginId = Common.CastAsInt32(Session["loginid"]);
            BindYear();
            BindMocRequest();
            BindCounters();
            Load_Stage();
            Load_ForwardedTo();

        }
    }
    public void BindMocRequest()
    {
        string whereClause = "";
        string SQL = " Select * from dbo.vw_MOC_RECORD Where 1=1 ";

        if (ddlStage.SelectedIndex>0)
        {
            whereClause = whereClause+" and STAGEID =" +ddlStage.SelectedValue;
        }
        if (ddlFYear.SelectedIndex > 0)
        {
            whereClause = whereClause+ " and Year(REquestDate)=" + ddlFYear.SelectedValue;
        }

        SQL = SQL + whereClause + "  order by MOCNumber ";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);

        rptMOC.DataSource = dt;
        rptMOC.DataBind();

    }
    public void BindYear()
    {
        ddlFYear.Items.Add(new ListItem(" All ","0"));
	    for(int i = DateTime.Today.Year; i >=2015;i--)
		{
			ddlFYear.Items.Add(new ListItem(i.ToString(),i.ToString()));
		}
        ddlFYear.SelectedIndex=1;
    }
    protected void ddlFYear_onSelectIndexChanged(object sender, EventArgs e)
    {
        BindMocRequest();

    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        int MocId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);

        ScriptManager.RegisterStartupScript(this, this.GetType(), "Edit", "window.open('MocRecord.aspx?MOCId='+ " + MocId + ", '_blank', '');", true);
    }
    protected void btnCreateNewMOC_Click(object sender, EventArgs e)
    {
        ClearRecords();
        dv_AddNewMOC.Visible = true;
        btnSaveNew.Visible = true;
        btnNext.Visible = false;
    }
    protected void btnSaveNew_Click(object sender, EventArgs e)
    {
        if (ddlSource.SelectedIndex == 0)
        {
            lblMsg.Text = "Please select source.";
            return;
        }
        if (ddlVessel_Office.SelectedIndex == 0)
        {
            lblMsg.Text = "Please select VSL/Office.";
            return;
        }
        if (txtTopic.Text.Trim() == "")
        {
            lblMsg.Text = "Please enter topic.";
            return;
        }
        string Impact = "";
        foreach( ListItem lst in cbImpact.Items )
        {
            if(lst.Selected)
            {
               Impact += lst.Value + "," ;
            }
        }

        if(Impact.Trim().Length > 0)
        {
            Impact = Impact.Remove(Impact.LastIndexOf(','));
        }

        if(Impact.Trim() == "")
        {
            lblMsg.Text = "Please select impact.";
            cbImpact.Focus();
            return;
        }
        if (txtReasonforChange.Text.Trim() == "")
        {
            lblMsg.Text = "Please enter reason for change.";
            return;
        }
        if (txtDescr.Text.Trim() == "")
        {
            lblMsg.Text = "Please enter brief description of change.";
            return;
        }
        DateTime dtPTL;        
        if (DateTime.TryParse(txtPropTL.Text.Trim(), out dtPTL))
        {
            if (dtPTL < DateTime.Today)
            {
                //lblMsg.Text = "Proposed TimeLine must be more than today.";
                //return;
            }
        }
        else
        {
            lblMsg.Text = "Please enter proposed TimeLine for completion of change.";
            return;
        }
       
        if (ddlForwardedTo.SelectedIndex == 0)
        {
            lblMsg.Text = "Please select forwarded to.";
            return;
        }

        try
        {
            Common.Set_Procedures("dbo.MOC_Moc_Request");
            Common.Set_ParameterLength(11);
            Common.Set_Parameters(
                new MyParameter("@OfficeVessel", ddlSource.SelectedValue),
                new MyParameter("@VesselId", (ddlSource.SelectedValue=="S")? ddlVessel_Office.SelectedValue:"0"),
                new MyParameter("@OfficeId", (ddlSource.SelectedValue == "O") ? ddlVessel_Office.SelectedValue : "0"),
                new MyParameter("@Topic", txtTopic.Text.Trim()),
                new MyParameter("@Impact", Impact),
                new MyParameter("@ReasonForChange", txtReasonforChange.Text.Trim()),
                new MyParameter("@DescriptionForChange", txtDescr.Text.Trim()),
                new MyParameter("@ProposedTimeline", txtPropTL.Text.Trim()),
                new MyParameter("@ClosedBy", Session["loginid"].ToString()),
                new MyParameter("@ClosureComments", txtForwardedComments.Text.Trim()),
                new MyParameter("@WaitingBy", ddlForwardedTo.SelectedValue)
                );
            DataSet ds = new DataSet();
            ds.Clear();
            Boolean res;
            res = Common.Execute_Procedures_IUD(ds);

            if (res)
            {
                ClearRecords();
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "aa", "alert('MOC Request created successfully.')", true);
                dv_AddNewMOC.Visible = false;
                BindMocRequest();
                BindCounters();
                btnSaveNew.Visible = false;
                btnNext.Visible = true;
                ViewState["NEWMOCID"] = Common.CastAsInt32(ds.Tables[0].Rows[0][0]);
            }
            else
            {
                lblMsg.Text = "Unable to create MOC Request.Error :" + Common.getLastError();
            }

        }
        catch (Exception ex)
        {

            lblMsg.Text = "Unable to create MOC Request.Error :" + ex.Message.ToString();
        }
    }

    protected void ddlSource_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSource.SelectedValue == "S")
        {
            Load_vessel(); 
        }
        else if (ddlSource.SelectedValue == "O")
        {
            Load_office();
        }
        else
        {
            ddlVessel_Office.Items.Clear();
            ddlVessel_Office.Items.Insert(0, new ListItem(" < All Office >", "0"));
        }
    }
    protected void btnCloseNew_Click(object sender, EventArgs e)
    {
        ddlSource.SelectedIndex = 0;
        ddlVessel_Office.Items.Clear();
        ddlVessel_Office.Items.Insert(0, new ListItem(" < All >", "0"));
        //txtMOCDate.Text = "";
        cbImpact.SelectedIndex = -1;
        txtReasonforChange.Text = "";
        txtDescr.Text = "";
        txtPropTL.Text = "";
        BindMocRequest();
        dv_AddNewMOC.Visible = false;

    }
    private void Load_vessel()
    {
        DataSet dt = Budget.getTable("Select * from dbo.vessel where vesselstatusid<>2  order by vesselname");
        ddlVessel_Office.DataSource = dt;
        ddlVessel_Office.DataTextField = "VesselName";
        ddlVessel_Office.DataValueField = "VesselID";
        ddlVessel_Office.DataBind();
        ddlVessel_Office.Items.Insert(0, new ListItem(" < Select Vessel >", "0"));

        //ddlVessel.DataTextField = "VesselName";
        //ddlVessel.DataValueField = "VesselCode";
        //ddlVessel.DataBind();
        //ddlVessel.Items.Insert(0, new ListItem(" < Select >", "0"));
    }
    private void Load_office()
    {
        DataSet dt = Budget.getTable("SELECT OfficeId, OfficeName, OfficeCode FROM [dbo].[Office] Order By OfficeName");
        ddlVessel_Office.DataSource = dt;
        ddlVessel_Office.DataTextField = "OfficeName";
        ddlVessel_Office.DataValueField = "OfficeId";
        ddlVessel_Office.DataBind();
        ddlVessel_Office.Items.Insert(0, new ListItem(" < Select Office>", "0"));

        //ddlOffice.DataTextField = "OfficeName";
        //ddlOffice.DataValueField = "OfficeId";
        //ddlOffice.DataBind();
        //ddlOffice.Items.Insert(0, new ListItem(" < Select >", "0"));
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        btnCloseNew_Click(sender,e);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "new", "window.open('EditMoc.aspx?MOCId=" + ViewState["NEWMOCID"].ToString() + "','');", true);
    }

    //---------------------------------------------------------------
    protected void ddlOfficeOrVessel_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddlOfficeOrVessel.SelectedIndex == 1)        
        //    ddlOffice.Visible = true;
        //else
        //    ddlVessel.Visible = true;


    }

    protected void ddlStage_OnTextChanged(object sender, EventArgs e)
    {
            BindMocRequest();
    }
    private void BindCounters()
    {
        DataSet dt = Budget.getTable(" SELECT M.STAGEID,M.STAGENAME,COUNT(D.MOCID)NOR,'color'+ convert(varchar(2) ,ROW_NUMBER()over(order BY M.STAGEID)) as cssname FROM MOC_STAGES M LEFT JOIN vw_MOC_RECORD D ON M.STAGEID=D.STAGEID GROUP BY M.STAGEID,M.STAGENAME ");
        rptGroupedItems.DataSource = dt;
        rptGroupedItems.DataBind();
    }
    private void Load_Stage()
    {
        DataSet dt = Budget.getTable("select * from MOC_STAGES");
        ddlStage.DataSource = dt;
        ddlStage.DataTextField = "StageName";
        ddlStage.DataValueField = "StageID";
        ddlStage.DataBind();
        ddlStage.Items.Insert(0, new ListItem(" All ", "0"));
        
    }
    private void Load_ForwardedTo()
    {
        DataSet dt = Budget.getTable("select * from [dbo].[GET_REPORTING_CHAIN_INCLUDE_SELF_UserID] (" + LoginId + ")");
        ddlForwardedTo.DataSource = dt;
        ddlForwardedTo.DataTextField = "EmpName";
        ddlForwardedTo.DataValueField = "LoginId";
        ddlForwardedTo.DataBind();
        ddlForwardedTo.Items.Insert(0, new ListItem(" Select ", "0"));

    }
    private void ClearRecords()
    {
        ddlSource.SelectedIndex = 0;        
        cbImpact.ClearSelection();
        txtReasonforChange.Text = "";
        txtTopic.Text = "";
        txtDescr .Text = "";
        txtPropTL.Text = "";
        ddlForwardedTo.SelectedIndex = 0;
        txtForwardedComments.Text = "";

        ddlVessel_Office.DataSource = null;
        ddlVessel_Office.DataBind();
    }



}