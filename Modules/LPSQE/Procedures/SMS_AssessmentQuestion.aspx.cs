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

public partial class SMS_AssessmentQuestion : System.Web.UI.Page
{
    AuthenticationManager Auth;    
    public int QuesID
    {
        get {return Common.CastAsInt32(ViewState["_QuesID"]) ;}
        set { ViewState["_QuesID"] = value; }
    }
    public int MannualID
    {
        get { return Common.CastAsInt32(ViewState["_MannualI"]); }
        set { ViewState["_MannualI"] = value; }
    }
    public int CurrentSequence
    {
        get { return Common.CastAsInt32(ViewState["_CurrentSequence"]); }
        set { ViewState["_CurrentSequence"] = value; }
    }

    protected void btnAdd_Click(object sender,EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "fasdf", "window.open('SMS_AddNewQuestion.aspx?MID=" + MannualID + "','');", true); 
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
            LoadManuals();
       }
    }

    //- Events ------------------------------------------------------------------------------------------------------------------------
    protected void SelectManual(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        MannualID = Common.CastAsInt32(lnk.CommandArgument);
        QuesID = 0;
        CurrentSequence = 0;
        lblManualNameHeading.Text = lnk.Text;
        aListQuestion.Attributes.Add("href", "SMS_QuestionList.aspx?MID=" + MannualID + "&mName=" + lblManualNameHeading.Text);

        divQuesOneByOne.Visible = true;
        divContainer.Visible = true;
        ShowQuestionOneByOne();
        
    }
    protected void ddlManualCategories_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadManuals();
    }

    
    protected void btnPrev_OnClick(object sender, EventArgs e)
    {
        CurrentSequence = CurrentSequence - 1;
        ShowQuestionOneByOne();
    }
    protected void btnNext_OnClick(object sender, EventArgs e)
    {
        CurrentSequence = CurrentSequence + 1;
        ShowQuestionOneByOne();
    }
    protected void btnQuestionList_OnClick(object sender, EventArgs e)
    {
        //dvFormVersion.Visible = true;
        //iframQuesList.Attributes.Add("src", "SMS_QuestionList.aspx?MID=" + MannualID+"&mName="+lblManualNameHeading.Text);
        //   //SMS_QuestionList.aspx
        //Response.Redirect("SMS_QuestionList.aspx?MID=" + MannualID);


    }
    protected void btnCloseQuestionList_OnClick(object sender, EventArgs e)
    {
        dvFormVersion.Visible = false;
    }
    

    //- Funation ------------------------------------------------------------------------------------------------------------------------
    public void LoadManuals()
    {
        DataTable dt = new DataTable();
        dt = Manual.getManualList();
        

        rptManuals.DataSource = dt;
        rptManuals.DataBind();
        upMList.Update();

    }    
    // Questionair ----
    
    public void ShowQuestionOneByOne()
    {
        btnPrev.Visible = true;
        btnNext.Visible = true;
        if (CurrentSequence == 0)            
            CurrentSequence = 1;

        if (CurrentSequence == 1)
        {
            btnPrev.Visible = false;
        }
        

        DataTable DT = null;
        if (ViewState["Questions"] == null)
        {
            DT = Questionair.GetQuestions(MannualID);
        }
        else
        {
            DT = (DataTable)ViewState["Questions"];
        }
        if (DT.Rows.Count > 0)
        {
            divQuesOneByOne.Visible = true;
            if (CurrentSequence == DT.Rows.Count)
            {
                btnNext.Visible = false;
            }

            lblTotalQuestion.Text = CurrentSequence.ToString() + " of " + DT.Rows.Count.ToString();


            DT.DefaultView.RowFilter = "RowNo=" + CurrentSequence;


            lblQuestion.Text = DT.DefaultView.ToTable().Rows[0]["QuestionText"].ToString();
            lblOptionA.Text = DT.DefaultView.ToTable().Rows[0]["Option1"].ToString();
            lblOptionB.Text = DT.DefaultView.ToTable().Rows[0]["Option2"].ToString();
            lblOptionC.Text = DT.DefaultView.ToTable().Rows[0]["Option3"].ToString();
            lblOptionD.Text = DT.DefaultView.ToTable().Rows[0]["Option4"].ToString();
            lblStatus.Text = DT.DefaultView.ToTable().Rows[0]["StatusText"].ToString();
            SetCorrectOptionClass(DT.DefaultView.ToTable().Rows[0]["Ans"].ToString());
        }
        else
        {
            divQuesOneByOne.Visible = false;
            lblTotalQuestion.Text = "";
            lblQuestion.Text = "";
            lblOptionA.Text = "";
            lblOptionB.Text = "";
            lblOptionC.Text = "";
            lblOptionD.Text = "";
            lblStatus.Text = "";
            
        }
    }


    public void SetCorrectOptionClass(string Option)
    {
        tdOptionA.Attributes.Remove("class");
        tdOptionB.Attributes.Remove("class");
        tdOptionC.Attributes.Remove("class");
        tdOptionD.Attributes.Remove("class");
        if (Option == "1") tdOptionA.Attributes.Add("class", "correct");
        if (Option == "2") tdOptionB.Attributes.Add("class", "correct");
        if (Option == "3") tdOptionC.Attributes.Add("class", "correct");
        if (Option == "4") tdOptionD.Attributes.Add("class", "correct");
                
    }
}