using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class AssessmentAddQuestion : System.Web.UI.Page
{

    AuthenticationManager Auth;
    public int QuesID
    {
        get { return Common.CastAsInt32(ViewState["_QuesID"]); }
        set { ViewState["_QuesID"] = value; }
    }
    public int AM_ID
    {
        get { return Common.CastAsInt32(ViewState["_AM_ID"]); }
        set { ViewState["_AM_ID"] = value; }
    }
    public int CurrentSequence
    {
        get { return Common.CastAsInt32(ViewState["_CurrentSequence"]); }
        set { ViewState["_CurrentSequence"] = value; }
    }



    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        //------------------------------------
        //ProjectCommon.SessionCheck_New();
        //------------------------------------
        int UserId = 0;
        UserId = int.Parse(Session["loginid"].ToString());
        AM_ID = Common.CastAsInt32(Page.Request.QueryString["AM_ID"]);
        Auth = new AuthenticationManager(334, UserId, ObjectType.Page);
        if (!(Auth.IsView))
        {
            Response.Redirect("NotAuthorized.aspx");
        }

        if (!IsPostBack)
        {
            BindRank();
            BindQuestion(AM_ID);
        }
    }

    //- Events ------------------------------------------------------------------------------------------------------------------------
    protected void SelectManual(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        AM_ID = Common.CastAsInt32(lnk.CommandArgument);
        QuesID = 0;
        BindQuestion(AM_ID);

        ClearQuestionSection();

    }


    // Questionair ----
    protected void btnAddQuestionair_OnClick(object sender, EventArgs e)
    {

         chkStatus.Checked = true;
    }
    protected void btnSaveQuestionair_OnClick(object sender, EventArgs e)
    {


        if (AM_ID == 0)
        {
            lblMsg.Text = "Please select any mannual";
            return;
        }
        string strchklistRank = "";
        foreach (ListItem Items in chklistRank.Items)
        {
            if (Items.Selected)
                strchklistRank = strchklistRank + "," + Items.Value;
        }
        if (strchklistRank != "")
            strchklistRank = strchklistRank.Substring(1);
        else
        {
            lblMsg.Text = "Please select question for rank";
            return;
        }

        if (txtQuesiton.Text.Trim() == "")
        {
            lblMsg.Text = "Please enter question";
            return;
        }
        if (txtOption_1.Text.Trim() == "")
        {
            lblMsg.Text = "Please enter Option A"; return;
        }
        if (txtOption_2.Text.Trim() == "")
        {
            lblMsg.Text = "Please enter Option B"; return;
        }
        if (txtOption_3.Text.Trim() == "")
        {
            lblMsg.Text = "Please enter Option C"; return;
        }
        if (txtOption_4.Text.Trim() == "")
        {
            lblMsg.Text = "Please enter Option D"; return;
        }
        if (ddlAnswer.SelectedIndex == 0)
        {
            lblMsg.Text = "Please select answer"; return;
        }



        bool Status = Questionair.AddNewQuestion(QuesID, AM_ID, txtQuesiton.Text.Trim(), txtOption_1.Text.Trim(), txtOption_2.Text.Trim(), txtOption_3.Text.Trim(), txtOption_4.Text.Trim(), Common.CastAsInt32(ddlAnswer.SelectedValue), chkStatus.Checked, strchklistRank);
        if (Status)
        {
            lblMsg.Text = "Data saved successfully.";
            if (QuesID == 0)
                ClearQuestionSection();
            BindQuestion(AM_ID);
        }
        else
        {
            lblMsg.Text = "Data could not be saved";
        }
    }
    protected void btnCancelSaveQuestion_OnClick(object sender, EventArgs e)
    {
         QuesID = 0;
        ClearQuestionSection();
    }
    protected void lnkEditQuestion_OnClick(object sender, EventArgs e)
    {
   
        LinkButton lnk = (LinkButton)sender;
        QuesID = Common.CastAsInt32(lnk.CommandArgument);
        Label lblQuestion = (Label)lnk.Parent.FindControl("rLblQuestion");
        Label rLblOption1 = (Label)lnk.Parent.FindControl("rLblOption1");
        Label rLblOption2 = (Label)lnk.Parent.FindControl("rLblOption2");
        Label rLblOption3 = (Label)lnk.Parent.FindControl("rLblOption3");
        Label rLblOption4 = (Label)lnk.Parent.FindControl("rLblOption4");
        Label rLblAns = (Label)lnk.Parent.FindControl("rLblAns");
        Label rLblStatus = (Label)lnk.Parent.FindControl("rLblStatus");
        HiddenField hfAns = (HiddenField)lnk.Parent.FindControl("hfAns");



        txtQuesiton.Text = lblQuestion.Text;
        txtOption_1.Text = rLblOption1.Text;
        txtOption_2.Text = rLblOption2.Text;
        txtOption_3.Text = rLblOption3.Text;
        txtOption_4.Text = rLblOption4.Text;
        ddlAnswer.SelectedValue = hfAns.Value;
        chkStatus.Checked = (rLblStatus.Text == "Active");

        string Ranks = Questionair.GetRankForEdit(QuesID);
        string[] RanksArray = Ranks.Split(',');

        chklistRank.ClearSelection();
        foreach (ListItem li in chklistRank.Items)
        {
            foreach (string s in RanksArray)
            {
                if (li.Value == s)
                    li.Selected = true;
            }
        }
    }



    //- Funation ------------------------------------------------------------------------------------------------------------------------

    // Questionair ----
    public void BindQuestion(int AM_ID)
    {
        DataTable DT = Questionair.GetQuestions(AM_ID);
    }
    public void ClearQuestionSection()
    {
        txtQuesiton.Text = "";
        txtOption_1.Text = "";
        txtOption_2.Text = "";
        txtOption_3.Text = "";
        txtOption_4.Text = "";
        ddlAnswer.SelectedIndex = 0;
        chkStatus.Checked = false;
        chklistRank.ClearSelection();
    }
    public void BindRank()
    {
        DataTable DT = Questionair.GetRank();
        chklistRank.DataSource = DT;
        chklistRank.DataTextField = "RankName";
        chklistRank.DataValueField = "RankID";
        chklistRank.DataBind();

    }
}