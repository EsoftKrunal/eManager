using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class SMS_SearchManuals : System.Web.UI.Page
{
    public ReadManualBO ob_ManualBO;
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
        lblMsg.Text = "";
        if (!IsPostBack)
        {
            LoadManuals();
        }
    }

    protected void SelectManual(object sender, EventArgs e)
    {
        LastMode = "L";
        txtM.Text = ((LinkButton)sender).CommandArgument;

        CheckBox chk = (CheckBox)((LinkButton)sender).FindControl("chkSelect");
        foreach (RepeaterItem RI in rptManuals.Items)
        {
            CheckBox ch = (CheckBox)RI.FindControl("chkSelect");
            ch.Checked = false;
        }

        chk.Checked = true;
        GetResult1("", txtM.Text, "", "","");
    }
    protected void SearchManual(object sender, EventArgs e)
    {
        LinkButton lnk;
        string sManualID = "";

        LastMode = "S";

        foreach (RepeaterItem RI in rptManuals.Items)
        {
            CheckBox ch = (CheckBox)RI.FindControl("chkSelect");
            if (ch.Checked)
            {
                lnk = (LinkButton)RI.FindControl("lnkManual");
                sManualID = sManualID + "," + lnk.CommandArgument;
            }
        }
        if (sManualID.Length > 0)
            sManualID = sManualID.Substring(1);
        else
        {
            lblMsg.Text = "Please select manual.";
            return;
        }
        GetResult1(txtSearchManual.Text.Trim(), sManualID, txtFromDate.Text.Trim(), txtToDate.Text.Trim(),txtFormNo.Text.Trim());
    }
    protected void ClearText(object sender, EventArgs e)
    {
        chall.Checked = false;
        foreach (RepeaterItem RI in rptManuals.Items)
        {
            CheckBox ch = (CheckBox)RI.FindControl("chkSelect");
            ch.Checked = false;
        }
        txtSearchManual.Text = "";
        txtFromDate.Text = "";
        txtToDate.Text = "";
    }

    protected void btnSelSection_Click(object sender, EventArgs e)
    {
        //frmSection.Attributes.Add("src", "ReadManualSection1.aspx?ManualId=" + txtM.Text + "&SectionId=" + txtS.Text);
        //upSection.Update();
    }
    protected void btnShowChangeRecord_Click(object sender, EventArgs e)
    {
        //frmSection.Attributes.Add("src", "ShowChangeRecord.aspx?ManualId=" + txtM.Text);
        //upSection.Update();
    }
    
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "tr", "SetLastFocus('dvscroll_Order');", true);
    }

    //----------------  Function
    public void LoadManuals()
    {
        rptManuals.DataSource = ReadManual.getManualList_FOR_READ();
        rptManuals.DataBind();

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
            ob_ManualBO = new ReadManualBO(ManualId);
            ob_ManualBO.LoadManualHeadingsHTML_FOR_READ();
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
            //litManual.Text = ReadManual.SearchManuals_FOR_READ(ManualIds, txtSearchManual.Text.Trim());
        }
        upHeadings.Update();
    }
    protected void GetResult1(string sText, string sManualID, string FromDate, string ToDate,string FormNo)
    {
        string WhereClause = "";
        string InnerWhereClause = "";
        //string sql = " Select MM.ManualID,MM.ManualName,MM.CreatedOn,MD.Heading,MD.SectionID,MD.Sversion,MD.ApprovedOn  " +
        //           " ,(Select Top 1 SentDate from dbo.vw_SMS_GetSendManuals VW Where VW.ManualID=MD.ManualID And VW.SectionID=MD.SectionID Order by SentDate desc)LastSent " +
        //           " from DBO.SMS_ManualMaster MM Inner Join DBO.SMS_ManualDetails MD on MM.ManualID=MD.ManualID " +
        //           " Where MM.ManualName like '%" + sText + "%' OR MD.Heading like '%" + sText + "%' and MM.ManualID in (" + sManualID + ") ";

        if (sText != "")
            InnerWhereClause = " (MD.SearchTags like '%" + sText + "%' OR MD.Heading like '%" + sText + "%') and ";

        //string sql = "select * from " +
        //             " ( " +
        //             "    Select Distinct MM.ManualID,MM.ManualName,Convert(Varchar,MM.CreatedOn,106)CreatedOn,MD.Heading,MD.SectionID,MD.Sversion,MD.ApprovedOn,F.FormName " +
        //             "    from DBO.SMS_App_ManualMaster MM Inner Join DBO.SMS_App_ManualDetails MD on MM.ManualID=MD.ManualID   " +
        //             "    left Join DBO.SMS_APP_ManualDetailsForms DF on MD.ManualID=DF.ManualID And MD.SectionID=DF.SectionID " +
        //             "    left Join DBO.SMS_Forms F ON F.FormID= DF.FormID " +
        //             "    Where " + InnerWhereClause + " MM.ManualID in (" + sManualID + ")   " +
        //             "   )a  Where 1=1 ";


        string sql = "   Select Distinct MM.ManualID,MM.ManualName,Convert(Varchar,MM.CreatedOn,106)CreatedOn,MD.Heading,MD.SectionID,MD.Sversion,MD.ApprovedOn " +
                     "    from DBO.SMS_App_ManualMaster MM Inner Join DBO.SMS_App_ManualDetails MD on MM.ManualID=MD.ManualID Where  MM.ManualID in (" + sManualID + ")  ";


        if (sText != "")
            WhereClause = WhereClause + " And (MD.SearchTags like '%" + sText + "%' OR MD.Heading like '%" + sText + "%') ";


        if (FromDate != "")
            WhereClause = WhereClause + " And ApprovedOn>=Convert(Datetime,'" + FromDate + "') ";
        if (ToDate != "")
            WhereClause = WhereClause + " And ApprovedOn<DateAdd(d,1,Convert(Datetime,'" + ToDate + "')) ";

        if (FormNo != "")
            WhereClause = WhereClause + " And convert(varchar,MD.ManualId)+'|'+ltrim(rtrim(MD.SectionID)) in(Select Distinct convert(varchar,ManualId)+'|'+ltrim(rtrim(SectionID)) from  DBO.SMS_Forms F inner join DBO.SMS_APP_ManualDetailsForms MDF on MDF.FormID=F.FormID  Where FormNo like '%" + FormNo + "%')";


        sql = sql + WhereClause;
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);        

        rptFilteredManuals.DataSource = dt;
        rptFilteredManuals.DataBind();

        upHeadings.Update();
    }
}