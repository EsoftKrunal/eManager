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
using System.Collections.Generic;
using System.IO;

public partial class ManualReports : System.Web.UI.Page
{
    
    AuthenticationManager Auth;
    public ReadManualBO ob_ManualBO;
    public DataTable dtResult
    {
        get
        {
            return (DataTable)ViewState["dtResult"];
        }
        set
        {
            ViewState["dtResult"] = value;
        }
    }
    public string LastMode
    {
        get
        {
            return ViewState["LM"].ToString();
        }
        set
        {
            ViewState["LM"] = value;
        }
    }
    public bool ShowSearch
    {
        get
        {
            try
            {
                return (bool)ViewState["ShowSearch"];
            }
            catch { return false; }

        }
        set
        {
            ViewState["ShowSearch"] = value;
        }

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        int UserId = 0;
        UserId = int.Parse(Session["loginid"].ToString());

        Auth = new AuthenticationManager(1057, UserId, ObjectType.Page);
        if (!(Auth.IsView))
        {
            Response.Redirect("NotAuthorized.aspx");
        }


        if (!IsPostBack)
        {
            Session["MM1"] = "S";
            BindManualCategories();
            LoadManuals();
        }
    }

    public void LoadManuals()
    {
        rptManuals.DataSource = ReadManual.getManualList_FOR_READ();
        rptManuals.DataBind();

    }
    public void BindManualCategories()
    {
        string SQL = "SELECT [ManualCatId],[ManualCatName] FROM [dbo].[SMS_ManualCategory] ORDER BY [ManualCatName]";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);

        ddlManualCategories.DataSource = dt;
        ddlManualCategories.DataTextField = "ManualCatName";
        ddlManualCategories.DataValueField = "ManualCatId";
        ddlManualCategories.DataBind();

        ddlManualCategories.Items.Insert(0, new ListItem("< All Category >", "0"));
    }
    protected void Reload(object sender, EventArgs e)
    {
        GetResult();
    }
    protected void GetResult()
    {
        
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        //ScriptManager.RegisterStartupScript(Page, this.GetType(), "tr", "SetLastFocus('dvscroll_Order');", true);
    }

    protected void btn_Report_Click(object sender, EventArgs e)
    {
        string Manuals="";
        bool AnyCheck = false;
        foreach (RepeaterItem RI in rptManuals.Items)
        {
            CheckBox ch = (CheckBox)RI.FindControl("chkSelect");
            if (ch.Checked)
            {
                Manuals = Manuals + "," + ((CheckBox)RI.FindControl("chkSelect")).Attributes["CommandArgument"];
                AnyCheck = true;
            }
        }
        if (!AnyCheck)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ask", "alert('Please select manual(s) to search.');", true);
            return;
        }
        if(Manuals.StartsWith(","))
        {
            Manuals = Manuals.Substring(1);
        }

        string whereclause =" where mm.ManualId in (" + Manuals + ") ";
        string datewhereclause = "";

        if (txt_FromDt.Text.Trim() != "")
            datewhereclause += "And ApprovedOn>='" + txt_FromDt.Text + "' ";
        if (txt_ToDt.Text.Trim() != "")
            datewhereclause += ((datewhereclause!="")?" And ":"") + "ApprovedOn<'" + Convert.ToDateTime(txt_ToDt.Text).AddDays(1).ToString("dd-MMM-yyyy") + "' ";


        //string manualqry = "select distinct mm.ManualId,mm.ManualName " +
        //                   "from dbo.SMS_ManualDetails_History h inner join [dbo].[SMS_APP_ManualMaster] mm on mm.ManualId=h.ManualId " + whereclause + datewhereclause +
        //                   "ORDER BY ManualName";
        //DataTable dtManuals = Common.Execute_Procedures_Select_ByQuery(manualqry);
     
        string qry = "select HistoryId,mm.ManualId,ManualName,Heading,SECTIONID AS Section,SVERSION as Revision#,ApprovedOn 'Approved On',MODIFIEDBY as 'Modified By',ManualVersion " +
                   "from dbo.SMS_ManualDetails_History h inner join [dbo].[SMS_APP_ManualMaster] mm on mm.ManualId=h.ManualId " + whereclause + datewhereclause + " " +
                   "ORDER BY SECTIONID ASC,ManualVersion Desc,SVERSION DESC";

        string manualqry = "select distinct mm.ManualId,mm.ManualName " +
                          "from dbo.SMS_APP_ManualDetails h inner join [dbo].[SMS_APP_ManualMaster] mm on mm.ManualId=h.ManualId " + whereclause + datewhereclause +
                          "ORDER BY ManualName";
        DataTable dtManuals = Common.Execute_Procedures_Select_ByQuery(manualqry);

        //string qry = "select mm.ManualId,ManualName,Heading,SECTIONID AS Section,SVERSION as Revision#,ApprovedOn 'Approved On',MODIFIEDBY as 'Modified By',mm.VersionNo as ManualVersion " +
        //           "from dbo.SMS_APP_ManualDetails h inner join [dbo].[SMS_APP_ManualMaster] mm on mm.ManualId=h.ManualId " + whereclause + datewhereclause + " " +
        //           "ORDER BY SECTIONID ASC,ManualVersion Desc,SVERSION DESC";

        dtResult= Common.Execute_Procedures_Select_ByQuery(qry);

        rptManualslist.DataSource = dtManuals;
        rptManualslist.DataBind();

        //if (txtFromDate.Text.Trim() != "")
        //{
        //    Filter = Filter + "♦" + txtFromDate.Text.Trim();
        //}
        //if (txtToDate.Text.Trim() != "")
        //{
        //    Filter = Filter + "♦" + txtToDate.Text.Trim();
        //}

        //litManual.Text = ReadManual.SearchManuals_FOR_READ(ManualIds, Filter);
    }
    protected DataTable getManualChanges(object ManualId)
    {
        DataView dv = dtResult.DefaultView;
        dv.RowFilter = "ManualId=" + ManualId.ToString();
        return dv.ToTable();
    }
    protected void ddlManualCategories_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlManualCategories.SelectedIndex == 0)
        {
            LoadManuals();
        }
        else
        {
            string SQL = "SELECT * FROM [dbo].[SMS_APP_MANUALMASTER] WHERE [ManualId] IN (SELECT [ManualId] FROM [dbo].[SMS_ManualCatMappings] WHERE [ManualCatId] = " + ddlManualCategories.SelectedValue.Trim() + ") ORDER BY MANUALNAME";
            DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);

            rptManuals.DataSource = dt;
            rptManuals.DataBind();
        }

    }
}
