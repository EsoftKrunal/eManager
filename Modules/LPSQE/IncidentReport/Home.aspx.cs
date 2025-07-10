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
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        if (!IsPostBack)
        {
            SelButton = "";
            BindForms();
        }

    }
    private void BindForms()
    {
        string SQL = "SELECT * FROM [DBO].[ER_Master] M inner join [DBO].[ER_REPORTGROUPMASTER] G ON G.REPORTGROUPID=M.REPORTGROUPID WHERE 1=1 ";
        if (SelButton != "")
            SQL += " AND FORMNO LIKE '" + SelButton + "%'";
        if (txtFormNoName.Text.Trim() != "")
            SQL += " AND ( FORMNAME LIKE '%" + txtFormNoName.Text + "%' OR FORMNO LIKE '%" + txtFormNoName.Text + "%' )";

        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
        if (dt != null)
        {
            rptFormList.DataSource = dt;
            rptFormList.DataBind();
        }
    }
    protected void lnkFormNo_OnClick(object sender, EventArgs e)
    {
        string FormNo = ((LinkButton)sender).CommandArgument;
        frm_Details.Attributes.Add("src", FormNo + "/" + FormNo + "_List.aspx");
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
    protected void btnReport_Click(object sender, EventArgs e)
    {
        string FormNo = ((ImageButton)sender).CommandArgument.Trim();

        if(FormNo == "Accident")
            ScriptManager.RegisterStartupScript(this, this.GetType(), "aa", "window.open('Accident/ER_Accident_Analysis.aspx','','');", true);

        if (FormNo == "NearMiss")
            ScriptManager.RegisterStartupScript(this, this.GetType(), "aa", "window.open('NearMiss/NearMiss_Analysis.aspx','','');", true);



    }    
}