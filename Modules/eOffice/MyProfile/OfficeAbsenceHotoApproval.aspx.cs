using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.IO;
using System.Text;

public partial class Emtm_OfficeAbsenceHotoApproval : System.Web.UI.Page
{
    public string LastVslCode
    {
        get
        {
            return ViewState["LastVslCode"].ToString();
        }
        set
        {
            ViewState["LastVslCode"] = value;
        }
    }
    public int HotoID
    {
        get
        {
            return Common.CastAsInt32(ViewState["_HotoID"]);
        }
        set
        {
            ViewState["_HotoID"] = value;
        }
    }
    public int RequestUserID
    {
        get
        {
            return Common.CastAsInt32(ViewState["_UserID"]);
        }
        set
        {
            ViewState["_UserID"] = value;
        }
    }
    public string HStatus
    {
        get
        {
            return ViewState["HStatus"].ToString();
        }
        set
        {
            ViewState["HStatus"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        LastVslCode = "";
        lblMsgStatus.Text = "";
        lblMsg.Text = "";
        if (!Page.IsPostBack) 
        {
            if (Request.QueryString["key"] != null)
            {
                HotoID = Common.CastAsInt32(Request.QueryString["key"].Trim().Substring(11, 6).Replace("_", ""));                
                ShowRecord();
                BindHandoverDetails();
            }
        }

    }

    // ----------------------------------------  EVENT
    protected void btnApprove_Onclick(object sender, EventArgs e)
    {
        string sql = "";
        if (Request.QueryString["key"].Trim().EndsWith("B"))
            sql = "Update DBO.HR_OfficeAbsence_HOTOMaster set BStatus='A' , BOn=Getdate() Where HotoID=" + HotoID.ToString();
        else if (Request.QueryString["key"].Trim().EndsWith("H"))
            sql = "Update DBO.HR_OfficeAbsence_HOTOMaster set HStatus='A' , HOn=Getdate() Where HotoID=" + HotoID.ToString();
        else
            return;
        
        Budget.getTable(sql);
        lblMsg.Text = "Request accepted successfully.";
        ShowRecord();
    }
    protected void btnReject_Onclick(object sender, EventArgs e)
    {
        string sql = "";
        if (Request.QueryString["key"].Trim().EndsWith("B"))
            sql = "Update DBO.HR_OfficeAbsence_HOTOMaster set BStatus='R' , BOn=Getdate() Where HotoID=" + HotoID.ToString();
        else if (Request.QueryString["key"].Trim().EndsWith("H"))
            sql = "Update DBO.HR_OfficeAbsence_HOTOMaster set HStatus='R' , HOn=Getdate() Where HotoID=" + HotoID.ToString();
        else
            return;

        Budget.getTable(sql);
        lblMsg.Text = " Request rejected. ";
        ShowRecord();
    }

    protected void btnViewFleetNotes_OnClick(object sender, EventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        int TID = Common.CastAsInt32(btn.CommandArgument);
        dvViewNotes.Visible = true;
        if (TID != 0)
        {
            string SQL = " Select VesselCode,TopicDetails,DueDate from dbo.tbl_Topic Where Tid=" + TID.ToString();
            DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(SQL);
            if (dt.Rows.Count > 0)
            {
                txtViewComments.Text = dt.Rows[0]["TopicDetails"].ToString();
            }
        }
    }
    protected void btnCloseViewFleetPopup_OnClick(object sender, EventArgs e)
    {
        dvViewNotes.Visible = false;
    }
    // ----------------------------------------  FUNCTION
    public void ShowRecord()
    {
        string SQL = "SELECT  REPLACE(Convert(varchar(11), OA.ActFromDt, 106),' ','-') AS ActFromDt,REPLACE(Convert(varchar(11), OA.ActToDt, 106),' ','-') AS ActToDt, OA.ActFromDt AS Time,OA.ActToDt AS EndTime, LP.Purpose, REPLACE(Convert(varchar(11), OA.LeaveFrom, 106),' ','-') AS LeaveFrom, REPLACE(Convert(varchar(11), OA.LeaveTo, 106),' ','-') AS LeaveTo FROM HR_OfficeAbsence OA " +
                     "INNER JOIN HR_LeavePurpose LP ON OA.PurposeId = LP.PurposeId " +
                     "WHERE OA.LeaveRequestId = " + HotoID;
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(SQL);

        if (dt.Rows.Count > 0)
        {
            lblPurpose.Text = dt.Rows[0]["Purpose"].ToString();
            lblPeriod.Text = dt.Rows[0]["LeaveFrom"].ToString() + " to " + dt.Rows[0]["LeaveTo"].ToString();

            DataTable dtHoto = Common.Execute_Procedures_Select_ByQueryCMS("SELECT (Select FirstName +' '+FamilyName from Hr_PersonalDetails PD Where PD.EmpID =HT.HandOverToID)HandOverTo,(Select FirstName +' '+FamilyName from Hr_PersonalDetails PD Where PD.EmpID =HT.BackupId)BackupBy,* FROM HR_OfficeAbsence_HOTOMaster HT where HotoId=" + HotoID);
            if (dtHoto.Rows.Count > 0)
            {
                ddlHandover.Text = dtHoto.Rows[0]["HandOverTo"].ToString();
                ddlBackup.Text = dtHoto.Rows[0]["BackUpBy"].ToString();


                if (Request.QueryString["key"].Trim().EndsWith("B"))
                {
                    if (dtHoto.Rows[0]["BStatus"].ToString() == "A" || dtHoto.Rows[0]["BStatus"].ToString() == "R")
                    {
                        btnApprove.Visible = false;
                        btnReject.Visible = false;
                        lblMsgStatus.Text = " Current Status : " + ((dtHoto.Rows[0]["BStatus"].ToString() == "A") ? " Accepted " : " Rejected ");
                    }
                }
                else if (Request.QueryString["key"].Trim().EndsWith("H"))
                {
                    if (dtHoto.Rows[0]["HStatus"].ToString() == "A" || dtHoto.Rows[0]["HStatus"].ToString() == "R")
                    {
                        btnApprove.Visible = false;
                        btnReject.Visible = false;
                        lblMsgStatus.Text = " Current Status : " + ((dtHoto.Rows[0]["HStatus"].ToString() == "A") ? " Accepted " : " Rejected ");
                    }
                }
            }
        }
    }

    public void BindHandoverDetails()
    {
        LastVslCode = "";
        string SQL = " Select *,(select  '[ '+ UserID +' ]' from  DBO.userlogin U where U.Loginid=tbl_Topic.CreatedBy )CreatedByName from dbo.tbl_Topic where TID IN(Select TID from DBO.HR_OfficeAbsence_HOTODetails Where HotoID=" + HotoID.ToString() + " And HTType='H' ) order by VesselCode";
        DataTable dt = Budget.getTable(SQL).Tables[0];
        rptHotoDetails.DataSource = dt;
        rptHotoDetails.DataBind();

    }
    public bool CompareVSl(object vsl)
    {
        bool a=LastVslCode != vsl.ToString();
        LastVslCode = vsl.ToString();
        return a;
    }
}