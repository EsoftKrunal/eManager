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

public partial class ViewManualHeadings : System.Web.UI.Page
{
    AuthenticationManager Auth;
    public ManualBOAdmin ob_ManualBO;
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

        Auth = new AuthenticationManager(1096, UserId, ObjectType.Page);
        if (!(Auth.IsView))
        {
            Response.Redirect("NotAuthorized.aspx");
        }

        if (!IsPostBack)
        {
            Session["MM1"] = "M";
            Session["MM"] = "S";
            BindManualCategories();
            LoadManuals();
        }
    }
    public void LoadManuals()
    {
        DataTable dt = new DataTable();

        if (ddlManualCategories.SelectedIndex == 0)
        {
            dt = ManualAdmin.getManualList();
        }
        else
        {
            dt = ManualAdmin.getManualListByCategoryId(Common.CastAsInt32(ddlManualCategories.SelectedValue.Trim()));
        }

        rptManuals.DataSource = dt;
        rptManuals.DataBind();
        upMList.Update();
        
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
        if (LastMode == "L")
        {
            int ManualId = Common.CastAsInt32(txtM.Text);
            ob_ManualBO = new ManualBOAdmin(ManualId);
            ob_ManualBO.LoadManualHeadingsHTML(); 
            litManual.Text = ob_ManualBO.ManualHTML;
        }
        else
        {
            bool AnyCheck = false;
            List<int> ManualIds = new List<int>();
            foreach (RepeaterItem RI in rptManuals.Items)
            {
                CheckBox ch = (CheckBox)RI.FindControl("chkSelect");
                if (ch.Checked)
                {
                    ManualIds.Add(Common.CastAsInt32(((LinkButton)RI.FindControl("lnkManual")).CommandArgument));
                    AnyCheck = true;
                }
            }
            if (!AnyCheck)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ask", "alert('Please select manual(s) to search.');", true);
                return;
            }

            string Filter = txtSearchManual.Text.Trim();

            if (txtFromDate.Text.Trim() != "")
            {
                Filter = Filter + "♦" + txtFromDate.Text.Trim();
            }
            if (txtToDate.Text.Trim() != "")
            {
                Filter = Filter + "♦" + txtToDate.Text.Trim();
            }

            litManual.Text = ManualAdmin.SearchManuals(ManualIds, Filter);
        }
        upHeadings.Update();
    }
    protected void SelectManual(object sender, EventArgs e)
    {
        LastMode = "L";
        txtM.Text = ((LinkButton)sender).CommandArgument;
        GetResult();
    }
    protected void SearchManual(object sender, EventArgs e)
    {
        LastMode = "S";
        GetResult();
        btnClose_Click(sender, e);
    }
    protected void btnSelSection_Click(object sender, EventArgs e)
    {
        frmSection.Attributes.Add("src", "../ViewManualSection.aspx?ManualId=" + txtM.Text + "&SectionId=" + txtS.Text);
        upSection.Update();
    }
    protected void btnShowChangeRecord_Click(object sender, EventArgs e)
    {
        frmSection.Attributes.Add("src", "../ShowChangeRecord.aspx?ManualId=" + txtM.Text);
        upSection.Update();
    }
    
    protected void btnSearchC_Click(object sender, EventArgs e)
    {
        chall.Visible= true;
        ShowSearch = true;
        LoadManuals();
        txtSearchManual.Visible = true;
        btnSearch.Visible = true;
        btnSearchC.Visible = false;
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "tr", "SetLastFocus('dvscroll_Order');", true);
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        txtSearchManual.Text = "";
        txtFromDate.Text = "";
        txtToDate.Text = "";
        dvSearchBox.Style.Add("display", "none");
    }
    protected void ddlManualCategories_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadManuals();
    }
}
