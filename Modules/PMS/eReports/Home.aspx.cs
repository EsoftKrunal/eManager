using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


public partial class eReports_Home : System.Web.UI.Page
{
    public string SelButton
    {
        get {return ViewState["SelButton"].ToString(); }
        set { ViewState["SelButton"] = value; }
    }
    public string lastGroupName = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["MHSS"] = 4;
        if (!IsPostBack)
        {
            hfSelectedCrewNumber.Value = "";
            SelButton = "";
            BindCrewDetails();
            BindForms();
        }

    }
    private void BindForms()
    {
        string SQL = "SELECT *,'' as PageExt FROM [DBO].[ER_Master] M inner join [DBO].[ER_REPORTGROUPMASTER] G ON G.REPORTGROUPID=M.REPORTGROUPID WHERE FormNo not in('D110','G113','G118') ";  //FormNo not in('G118','S115','S133')
        if (SelButton != "")
            SQL += " AND FORMNO LIKE '" + SelButton + "%'";
        if (txtFormNoName.Text.Trim() != "")
            SQL += " AND ( FORMNAME LIKE '%" + txtFormNoName.Text + "%' OR FORMNO LIKE '%" + txtFormNoName.Text + "%' )";

        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
        if (dt != null)
        {
            DataRow Dr = dt.NewRow();
            Dr[0] = "1";
            Dr[1] = "S115";
            Dr[2] = "Fleet Accident Report";
            Dr[4] = "V001";
            Dr[5] = "1";
            Dr[6] = "Incident";
            Dr[7] = "_Fleet";
            

            dt.Rows.InsertAt(Dr,1);
            rptFormList.DataSource = dt;
            rptFormList.DataBind();
        }
    }
    protected void lnkFormNo_OnClick(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        string FormNo = btn.CommandArgument;
        HiddenField hfdPageExt = (HiddenField)btn.Parent.FindControl("hfdPageExt");

        frm_Details.Attributes.Add("src", FormNo + "/" + FormNo + "_List"+ hfdPageExt.Value + ".aspx");
        UpdatePanel1.Update();
    }

    protected void lnkForm_Click(object sender, EventArgs e)
    {
        LinkButton CurButton = ((LinkButton)sender);
        string CurrButttonText = CurButton.Text.Trim();
        if (SelButton == CurrButttonText)
        {
            SelButton = "";
            CurButton.CssClass = "alpha";
        }
        else
        {
            if(SelButton!="")
                ((LinkButton)this.FindControl("LinkButton" + SelButton)).CssClass = "alpha";

            CurButton.CssClass = "alpha_selected";
            SelButton = CurrButttonText;
            CurButton.CssClass = "alpha_selected";
        }
        BindForms();
    }
    
    protected void txtFormNoName_OnTextChanged(object sender, EventArgs e)
    {
        BindForms();
    }

    //--------------------------------------
    protected void btnAddNewReport_OnClick(object sender, EventArgs e)
    {
        dv_CrewList.Visible = true;
    }
    protected void btnGoToAppraisal_Click(object sender, EventArgs e)
    {
        if (hfSelectedCrewNumber.Value == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ddd", "alert('Please select any crew.');", true);
            return;    
        }
        string CrewNumber = hfSelectedCrewNumber.Value;
        hfSelectedCrewNumber.Value = "";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "dd", "window.open('G113/ER_G113_Report.aspx?CrewNumber=" + CrewNumber + "')", true);
    }
    
    protected void btn_Close_Click(object sender, EventArgs e)
    {
        dv_CrewList.Visible = false;
    }
    
    public void BindCrewDetails()
    {
        string sqL = " select * from PMS_CREW_HISTORY CH inner join MP_AllRank R on r.rankid=CH.rankid " +
                     " where(SignOffDate is null or SignOffDate >= getdate())  and r.RankId<>1 order by r.RankId";
        
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sqL);
        rptCrewList.DataSource = dt;
        rptCrewList.DataBind();
    }
}