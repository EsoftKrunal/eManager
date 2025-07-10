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

public partial class AssessmentQuestionList : System.Web.UI.Page
{
    AuthenticationManager Auth;    
    public int QuesID
    {
        get {return Common.CastAsInt32(ViewState["_QuesID"]) ;}
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
        lblCompyMannualMsg.Text = "";
        //------------------------------------
        //ProjectCommon.SessionCheck_New();
        //------------------------------------
        int UserId = 0;
        UserId = int.Parse(Session["loginid"].ToString());
        AM_ID = Common.CastAsInt32(Page.Request.QueryString["AM_ID"]);
        lblManualNameHeading.Text = Page.Request.QueryString["mName"].ToString();
        Auth = new AuthenticationManager(334, UserId, ObjectType.Page);
        if (!(Auth.IsView))
        {
            Response.Redirect("NotAuthorized.aspx");
        }

        if (!IsPostBack)
        {
            trCopy.Visible =  Common.CastAsInt32(Session["loginid"])==1;
            BindRank();
            BindAssessmentDdl();
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

        DivAddQuestion.Visible = false;
        DivShowQuestion.Visible = true;        
        ClearQuestionSection();        
        
    }
  
    // Questionair ----
    protected void btnAddQuestionair_OnClick(object sender, EventArgs e)
    {
        
        DivAddQuestion.Visible = true;
        DivShowQuestion.Visible = false;
        chkStatus.Checked = true;
    }
    protected void btnSaveQuestionair_OnClick(object sender, EventArgs e)
    {
       

        if (AM_ID == 0)
        {
            lblMsg.Text = "Please select any assessment";
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

        if (txtQuesiton.Text.Trim()== "")
        {
            lblMsg.Text = "Please enter question";
            return;
        }
        if (txtOption_1.Text.Trim() == "")
        {
            lblMsg.Text = "Please enter Option 1";return;
        }
        if (txtOption_2.Text.Trim() == "")
        {
            lblMsg.Text = "Please enter Option 2"; return;
        }
        if (txtOption_3.Text.Trim() == "")
        {
            lblMsg.Text = "Please enter Option 3"; return;
        }
        if (txtOption_4.Text.Trim() == "")
        {
            lblMsg.Text = "Please enter Option 4"; return;
        }
        if (ddlAnswer.SelectedIndex==0)
        {
            lblMsg.Text = "Please select answer"; return;
        }

        

        bool Status = Questionair.AddNewQuestion(QuesID, AM_ID, txtQuesiton.Text.Trim(), txtOption_1.Text.Trim(), txtOption_2.Text.Trim(), txtOption_3.Text.Trim(), txtOption_4.Text.Trim(), Common.CastAsInt32(ddlAnswer.SelectedValue), chkStatus.Checked, strchklistRank);
        if (Status)
        {
            lblMsg.Text = "Data saved successfully.";
            if (QuesID==0)
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
        DivAddQuestion.Visible = false;
        DivShowQuestion.Visible = true;
        
        QuesID = 0;
        ClearQuestionSection();
    }
    protected void lnkEditQuestion_OnClick(object sender, EventArgs e)
    {
        DivAddQuestion.Visible = true;
        DivShowQuestion.Visible = false;
        

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
        txtOption_1.Text=rLblOption1.Text;
        txtOption_2.Text=rLblOption2.Text;
        txtOption_3.Text=rLblOption3.Text;
        txtOption_4.Text=rLblOption4.Text;
        ddlAnswer.SelectedValue = hfAns.Value;
        chkStatus.Checked = (rLblStatus.Text == "Active");

        string Ranks = Questionair.GetRankForEdit(QuesID);
        string []RanksArray = Ranks.Split(',');

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
    protected void btnCopyQuestion_OnClick(object sender, EventArgs e)
    {
        int QuestionCount =0;
        if (dllCopyAssessment.SelectedIndex == 0)
        {
            lblCompyMannualMsg.Text = "Please select assessment.";
            return;
        }


        int QuestionID = 0;
        foreach (RepeaterItem itm in rptQuestionList.Items)
        {
            
            CheckBox chkbox = (CheckBox)itm.FindControl("chkSelectQuestion");
            HiddenField hfSelectedQuesID =(HiddenField)itm.FindControl("hfSelectedQuesID");
            if (chkbox.Checked)
            {
                QuestionCount = 1;
                QuestionID = Common.CastAsInt32(hfSelectedQuesID.Value);
                Questionair.CopyQuestion(QuestionID, Common.CastAsInt32(dllCopyAssessment.SelectedValue) );
            }

        }
        if (QuestionCount==0)
            lblCompyMannualMsg.Text = "Please select any Question.";
        else
            lblCompyMannualMsg.Text = "Assessment Coppied successfully.";
    }
    protected void ddlRanks_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        BindQuestion(AM_ID);
    }
    

    //- Funation ------------------------------------------------------------------------------------------------------------------------
    
    // Questionair ----
    public void BindQuestion(int AM_ID)
    {
        //DataTable DT = Questionair.GetQuestions(MannualID);
        DataTable DT = Questionair.GetQuestions(AM_ID,Common.CastAsInt32( ddlRanks.SelectedValue));
        rptQuestionList.DataSource = DT;
        rptQuestionList.DataBind();
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

        ddlRanks.DataSource = DT;
        ddlRanks.DataTextField = "RankName";
        ddlRanks.DataValueField = "RankID";
        ddlRanks.DataBind();
        ddlRanks.Items.Insert(0, new ListItem("< All >", ""));
    }
    public void BindAssessmentDdl()
    {
        DataTable DT = Questionair.GetAssessment();


        dllCopyAssessment.DataSource = DT;
        dllCopyAssessment.DataTextField = "AM_Name";
        dllCopyAssessment.DataValueField = "AM_id";
        dllCopyAssessment.DataBind();
        
        ListItem removeItem = dllCopyAssessment.Items.FindByValue(AM_ID.ToString());
        dllCopyAssessment.Items.Remove(removeItem);
        dllCopyAssessment.Items.Insert(0, new ListItem("< select >", ""));
    }
    
}