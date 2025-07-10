using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Ionic.Zip;

public partial class Seminar : System.Web.UI.Page
{
    public int UserId
    {
        get { return Common.CastAsInt32(ViewState["UserId"]); }
        set { ViewState["UserId"] = value; }
    }
    public string UserName
    {
        get { return ViewState["UserName"].ToString(); }
        set { ViewState["UserName"] = value; }
    }
    public string SelGuid
    {
        get
        { return ViewState["SelGuid"].ToString(); }
        set { ViewState["SelGuid"] = value; }
    }

    public int SeminarId
    {
        get { return Common.CastAsInt32(ViewState["_SeminarId"]); }
        set { ViewState["_SeminarId"] = value; }
    }
    public int FeedbackID
    {
        get { return Common.CastAsInt32(ViewState["_FeedbackID"]); }
        set { ViewState["_FeedbackID"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        //ProjectCommon.SessionCheck_New();
        //------------------------------------
        lblMessageAttendies.Text = "";
        lblMsgFedback.Text = "";
        if (!IsPostBack)
        {
            UserId = Common.CastAsInt32(Session["loginid"]);
            UserName = Session["UserName"].ToString();
            BindOffice();
            BindSeminarCategory();
            Bindgrid();
        }
    }
    protected void Filter_Visits(object sender, EventArgs e)
    {
        Bindgrid();
    }
    private void BindOffice()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.RecruitingOffice ORDER BY RecruitingOfficeName");
        
        ddloffice.DataSource = dt;
        ddloffice.DataTextField = "RecruitingOfficeName";
        ddloffice.DataValueField = "RecruitingOfficeId";
        ddloffice.DataBind();
        ddloffice.Items.Insert(0, new ListItem(" -- All --- ", "0"));
    }
    private void BindSeminarCategory()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.tbl_SeminarCat ORDER BY SeminarCatName");
        ddlCategory.DataSource = dt;
        ddlCategory.DataTextField = "SeminarCatName";
        ddlCategory.DataValueField = "SeminarCatId";
        ddlCategory.DataBind();
        ddlCategory.Items.Insert(0, new ListItem(" -- All --- ", "0"));
    }
    
    protected void Bindgrid()
    {
        string sql = "SELECT ROW_NUMBER() OVER(ORDER BY STARTDATE DESC) AS SNO,* FROM DBO.vw_tbl_Seminar WHERE 1=1 ";

        if (ddloffice.SelectedIndex > 0)
            sql += " AND OFFICEID=" + ddloffice.SelectedValue;
        if (ddlCategory.SelectedIndex > 0)
            sql += " AND CATEGORYID=" + ddloffice.SelectedValue;

        if(txtFromDate.Text.Trim()!="")
            sql += " AND STARTDATE>='" + txtFromDate.Text.Trim()+"'";
        if (txtTODate.Text.Trim() != "")
            sql += " AND ENDDATE<='" + txtTODate.Text.Trim() + "'";

        sql += " ORDER BY STARTDATE DESC";

        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
        rptSeminars.DataSource = dt;
        rptSeminars.DataBind();
    }
    protected void ddloffice_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        Bindgrid();
    }
    protected void ddlCategory_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        Bindgrid();
    }
    
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        dvFrame.Visible = true;
        frame1.Attributes.Add("Src", "AddEditSeminar.aspx?SeminarId=0");
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        int SeminarId=Common.CastAsInt32(((ImageButton)sender).CommandArgument );
        dvFrame.Visible = true;
        frame1.Attributes.Add("Src", "AddEditSeminar.aspx?SeminarId=" + SeminarId);
    }
    protected void btnAgenda_Click(object sender, EventArgs e)
    {
        int SeminarId=Common.CastAsInt32(((ImageButton)sender).CommandArgument );
        dvFrame.Visible = true;
        frame1.Attributes.Add("Src", "SeminarAgenda.aspx?SeminarId=" + SeminarId);
    }
    
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        HiddenField hfSeminarID = (HiddenField)btn.Parent.FindControl("hfSeminarID");
        string sql = " Exec dbo.delete_Seminar "+ hfSeminarID.Value;
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
        Bindgrid();
    }
    protected void btnInvite_Click(object sender, EventArgs e)
    {
        int SeminarId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        dvFrame.Visible = true;
        frame1.Attributes.Add("Src", "SeminarInvite.aspx?SeminarId=" + SeminarId);
    }
    protected void btnExecute_Click(object sender, EventArgs e)
    {
        dvFrame.Visible = true;
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        dvFrame.Visible = false;
        Bindgrid();
    }
    
    /////////////////////////////////////

    protected void lnkInviteClick_OnClick(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        DivInvitedPerson.Visible = true;
        BindInviteDetails(Common.CastAsInt32(lnk.CommandArgument));
    }

    protected void btnClose_DivInvitedPerson_OnClick(object sender, EventArgs e)
    {
        DivInvitedPerson.Visible = false;
        divUpdatePresence.Visible = false;
    }
    public void BindInviteDetails(int SeminarId)
    {
        string sql = "SELECT ROW_NUMBER() OVER(ORDER BY CREWNUMBER) AS SNO,TableId,sp.CREWID,CREWNUMBER,CREWNAME,CrewStatusName AS CREWSTATUS,RecruitingOfficeName,SP.RequstedOn,(CASE WHEN SP.ReplyStatus='P' then 'Yes' WHEN SP.ReplyStatus='A' then 'No' WHEN SP.ReplyStatus='N' then 'TBC' else '' end) as ReplyStatus,SP.ReplyStatus as ReplyStatusCode,SP.RepliedOn,R1.RankCode,SP.City,GUID,EMAIL  " +
                    "from " +
                    "DBO.tbl_SeminarInvite SP  " +
                    "INNER JOIN DBO.CREWPERSONALDETAILS P ON SP.CrewId=P.CrewId  " +
                    "INNER JOIN DBO.CrewStatus S ON P.CrewStatusId=S.CrewStatusId " +
                    "INNER JOIN DBO.Rank R1 ON R1.RankID=P.CurrentRankID " +
                    "INNER JOIN DBO.RecruitingOffice R ON P.RecruitmentOfficeId=R.RecruitingOfficeId WHERE SP.SEMINARID=" + SeminarId;


        DataTable DT = Common.Execute_Procedures_Select_ByQuery(sql);
        rptInvite.DataSource = DT;
        rptInvite.DataBind();
    }
    protected void btEditAttendies_OnClick(object sender, EventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        HiddenField hfGUID = (HiddenField)btn.FindControl("hfdGUID");
        HiddenField hfReplyStatusCode = (HiddenField)btn.FindControl("hfReplyStatusCode");
        Label lblCrewNumner = (Label)btn.FindControl("lblCrewNumner");
        Label lblCrewName = (Label)btn.FindControl("lblCrewName");

        

        SelGuid = hfGUID.Value;
        lblCrewNumber.Text = lblCrewNumner.Text + " [ " + lblCrewName.Text+" ]" ;
        rdoGrade.SelectedValue = hfReplyStatusCode.Value;

        divUpdatePresence.Visible = true;
    }

    protected void btnUpdateAttendies_Click(object sender, EventArgs e)
    {
        Common.Execute_Procedures_Select_ByQuery("UPDATE DBO.tbl_SeminarInvite SET REPLYSTATUS='" + rdoGrade.SelectedValue + "',RepliedOn=GETDATE() WHERE GUID='" + SelGuid + "'");
        lblMessageAttendies.Text = "Saved";
        rdoGrade.ClearSelection();
        lblCrewNumber.Text = "";
        BindInviteDetails(1);
    }

    protected void btnCloseAttendies_Click(object sender, EventArgs e)
    {
        SelGuid = "";
        lblCrewNumber.Text = "";
        divUpdatePresence.Visible = false;
    }


    //-----------------
    
    protected void btnFeedback_OnClick(object sender, EventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        HiddenField hfSeminarID = (HiddenField)btn.Parent.FindControl("hfSeminarID");

        HiddenField hfTopic = (HiddenField)btn.Parent.FindControl("hfTopic");
        
        SeminarId = Common.CastAsInt32(hfSeminarID.Value);
        lblFeedbackTopic.Text = hfTopic.Value;

        dvFeedback.Visible = true;
        ShowFeedbackList();
    }
    protected void imgCloseFeedbackPopup_OnClick(object sender, EventArgs e)
    {
        dvFeedback.Visible = false;
        trfeedbackOption.Visible = true;
        SeminarId = 0;        
        ClearFeedbackData();
    }
    protected void btnSaveFeedback_OnClick(object sender, EventArgs e)
    {
        if (rdoFeedbackType.SelectedIndex < 0)
        {
            lblMsgFedback.Text = " Please select feedback type.";
            rdoFeedbackType.Focus();
            return;
        }
        if (txtFeedbackQuestion.Text.Trim()=="")
        {
            lblMsgFedback.Text = " Please enter question.";
            txtFeedbackQuestion.Focus();
            return;
        }
        if (rdoFeedbackType.SelectedIndex == 0)
        {
            if (txtFeedbackOption1.Text.Trim() == "")
            {
                lblMsgFedback.Text = " Please enter feedbackO option 1.";
                txtFeedbackOption1.Focus();
                return;
            }
            if (txtFeedbackOption2.Text.Trim() == "")
            {
                lblMsgFedback.Text = " Please enter feedbackO option 2.";
                txtFeedbackOption2.Focus();
                return;
            }
            if (txtFeedbackOption3.Text.Trim() == "")
            {
                lblMsgFedback.Text = " Please enter feedbackO option 3.";
                txtFeedbackOption3.Focus();
                return;
            }
            if (txtFeedbackOption4.Text.Trim() == "")
            {
                lblMsgFedback.Text = " Please enter feedbackO option 4.";
                txtFeedbackOption4.Focus();
                return;
            }
        }
        Common.Set_Procedures("DBO.sp_IU_tbl_SeminarFeedback");
        Common.Set_ParameterLength(9);
        Common.Set_Parameters(
            new MyParameter("@FeedbackID", FeedbackID),
            new MyParameter("@SeminarID", SeminarId),
            new MyParameter("@FeedbackType", rdoFeedbackType.SelectedValue),
            new MyParameter("@Question", txtFeedbackQuestion.Text.Trim()),
            new MyParameter("@Option1", txtFeedbackOption1.Text.Trim()),
            new MyParameter("@Option2", txtFeedbackOption2.Text.Trim()),
            new MyParameter("@Option3", txtFeedbackOption3.Text.Trim()),
            new MyParameter("@Option4", txtFeedbackOption4.Text.Trim()),
            new MyParameter("@ModifiedBy", Session["UserName"].ToString())
            );

        DataSet ds = new DataSet();
        if (Common.Execute_Procedures_IUD(ds))
        {
            FeedbackID= Common.CastAsInt32(ds.Tables[0].Rows[0][0]);
            lblMsgFedback.Text= "Record saved sucessfully.";
            ShowFeedbackList();
            ClearFeedbackData();
        }
        else
        {
            lblMsgFedback.Text = "Unable to save record.";
        }

    }
    protected void btnEditFeedbackQuestion_OnClick(object sender, EventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        HiddenField hfFeedbackID = (HiddenField)btn.Parent.FindControl("hfFeedbackID");
        FeedbackID = Common.CastAsInt32(hfFeedbackID.Value);
        ShowFeedbackDataForEdit();
    }
    protected void btnCancelFeedback_OnClick(object sender, EventArgs e)
    {
        ClearFeedbackData();
    }
    protected void btnDeleteFeedbackQuestion_OnClick(object sender, EventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        HiddenField hfFeedbackID = (HiddenField)btn.Parent.FindControl("hfFeedbackID");
        

        string sql = " delete from dbo.tbl_SeminarFeedback where FeedbackID=" + hfFeedbackID.Value + " ";
        DataTable DT = Common.Execute_Procedures_Select_ByQuery(sql);
        lblMsgFedback.Text = "Record deleted successfully.";
        ShowFeedbackList();
    }



    protected void rdoFeedbackType_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdoFeedbackType.SelectedValue == "D")
        {
            trfeedbackOption.Visible = false;
            txtFeedbackOption1.Text = "";
            txtFeedbackOption2.Text = "";
            txtFeedbackOption3.Text = "";
            txtFeedbackOption4.Text = "";
        }
        else
        {
            trfeedbackOption.Visible = true;
        }
    }

    public void ShowFeedbackList()
    {
        string sql = " select case when FeedbackType='M' then 'Muliple Choice' when FeedbackType='R' then 'Single Choice' when FeedbackType='D'  then 'Comemnt Box'  else '' end QuesType,* from dbo.tbl_SeminarFeedback where SeminarID=" + SeminarId+" ";
        DataTable DT = Common.Execute_Procedures_Select_ByQuery(sql);
        rptFeedbackList.DataSource = DT;
        rptFeedbackList.DataBind();

    }
    public void ShowFeedbackDataForEdit()
    {
        string sql = " select * from dbo.tbl_SeminarFeedback where FeedbackID=" + FeedbackID + " ";
        DataTable DT = Common.Execute_Procedures_Select_ByQuery(sql);
        if (DT.Rows.Count > 0)
        {
            DataRow dr = DT.Rows[0];
            rdoFeedbackType.SelectedValue = dr["FeedbackType"].ToString();
            if (rdoFeedbackType.SelectedValue == "D")
                trfeedbackOption.Visible = false;
            else
                trfeedbackOption.Visible = true;


            txtFeedbackQuestion.Text = dr["Question"].ToString();
            txtFeedbackOption1.Text = dr["Option1"].ToString();
            txtFeedbackOption2.Text = dr["Option2"].ToString();
            txtFeedbackOption3.Text = dr["Option3"].ToString();
            txtFeedbackOption4.Text = dr["Option4"].ToString();
        }
    }
    public void ClearFeedbackData()
    {
        FeedbackID = 0;
        trfeedbackOption.Visible = true;
        rdoFeedbackType.SelectedIndex = 0;
        txtFeedbackQuestion.Text = "";
        txtFeedbackOption1.Text = "";
        txtFeedbackOption2.Text = "";
        txtFeedbackOption3.Text = "";
        txtFeedbackOption4.Text = "";
    }



}
