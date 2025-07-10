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
using System.Text;

public partial class CrewOperatin_AssessmentQuestion : System.Web.UI.Page
{
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
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        lblMsg.Text = "";
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 334);
        if (chpageauth <= 0)
        {
            Response.Redirect("~/CrewOperation/Dummy_Training1.aspx");
        }

        if (!Page.IsPostBack)
        {
            bind_ddl_Assessmentmaster();
        }
    }


    protected void ddlAssessment_OnSelectedIndexChanged(object sender, EventArgs e)
    {

        AM_ID = Common.CastAsInt32(ddlAssessment.SelectedValue);
        QuesID = 0;
        CurrentSequence = 0;
        lblAssessmentHeading.Text = ddlAssessment.SelectedItem.Text;
        aListQuestion.Attributes.Add("href", "AssessmentQuestionList.aspx?AM_ID=" + AM_ID + "&mName=" + lblAssessmentHeading.Text);

        divQuesOneByOne.Visible = true;
        divContainer.Visible = true;
        ShowQuestionOneByOne();
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "fasdf", "window.open('AssessmentAddQuestion.aspx?AM_ID=" + AM_ID + "','');", true);
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
    //----- Funtion 
    public void bind_ddl_Assessmentmaster()
    {
        string sql = " select * from tbl_CrewAssessmentMaster order by AM_name ";
        DataTable dt1 = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        ddlAssessment.DataSource = dt1;
        ddlAssessment.DataTextField = "Am_Name";
        ddlAssessment.DataValueField = "Am_ID";
        ddlAssessment.DataBind();
        ddlAssessment.Items.Insert(0, new ListItem("Select", ""));
    }
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
            DT = Questionair.GetQuestions(AM_ID);
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
