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

public partial class RiskManagement_ViewRisk : System.Web.UI.Page
{
    #region -------- PROPERTIES ------------------
    public string VesselCode
    {
        set { ViewState["VesselCode"] = value; }
        get { return ViewState["VesselCode"].ToString(); }
    }
    public int RiskId
    {
        set { ViewState["RiskId"] = value; }
        get { return Common.CastAsInt32(ViewState["RiskId"]); }
    }
    public DataTable HazardsList
    {
        set { ViewState["AditionalHazard"] = value; }
        get { return (DataTable)ViewState["AditionalHazard"]; }
    }
    #endregion -----------------------------------
    int LoginId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        if (Session["loginid"] == null)
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.close();", true);
        }
        else
        {
            LoginId = Convert.ToInt32(Session["loginid"].ToString());
        }

        if (!IsPostBack)
        {
            RiskId = Common.CastAsInt32(Request.QueryString["RiskId"]);
            VesselCode = Request.QueryString["VesselCode"];
            ShowEventDetails();            
            ShowRecord();
        }
    }

    public void ShowEventDetails()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM dbo.EV_EventMaster WHERE EventId=(SELECT EventId FROM DBO.EV_VSL_RISKMGMT_MASTER M WHERE VESSELCODE='" + VesselCode + "' AND RISKID=" + RiskId.ToString() + ")");
        if (dt.Rows.Count > 0)
        {
            lblEventName.Text = dt.Rows[0]["EventName"].ToString();
        }
    }

    public void ShowRecord()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT *,(SELECT VESSELNAME FROM DBO.Vessel V WHERE V.VESSELCODE=M.VESSELCODE) AS VESSELNAME FROM DBO.EV_VSL_RISKMGMT_MASTER M WHERE VESSELCODE='" + VesselCode + "' AND RISKID=" + RiskId.ToString());
        if (dt.Rows.Count > 0)
        {

            lblVesselName.Text=dt.Rows[0]["VESSELNAME"].ToString();            
            txtEventDate.Text=Common.ToDateString(dt.Rows[0]["EVENTDATE"]);
            lblRefNo.Text=dt.Rows[0]["REFNO"].ToString();
            txtRiskDescr.Text = dt.Rows[0]["RiskDescr"].ToString();
            txtDetails.Text = dt.Rows[0]["Details"].ToString();
            txtPosition.Text = dt.Rows[0]["POSITION"].ToString();
            ddlAlt.SelectedValue = dt.Rows[0]["ALTERNATEMETHODS"].ToString();
            txtDetails.Visible = (ddlAlt.SelectedValue.Trim() == "Y");
            txtCreatedBy.Text = dt.Rows[0]["CREATED_BY"].ToString();
            txtHOD.Text=dt.Rows[0]["HOD_NAME"].ToString();
            txtSO.Text=dt.Rows[0]["SAF_OFF_NAME"].ToString();
            txtMaster.Text=dt.Rows[0]["MASTER_NAME"].ToString(); 
            lblCommentsByOn.Text = "<b>" + dt.Rows[0]["OFFICECOMMENTBY"].ToString() + " </b> ( <i style='color:blue'>" + dt.Rows[0]["DESIGNATION"].ToString() + " </i> ) / " + Common.ToDateString(dt.Rows[0]["COMMENTDATE"]);
            lblOfficeComments.Text = dt.Rows[0]["OFFICE_COMMENTS"].ToString();
            
            btnClosure.Visible = (dt.Rows[0]["Status"].ToString() == "O");
            trClosure.Visible = (dt.Rows[0]["Status"].ToString() == "C");             

            HazardsList = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.EV_VSL_RISKMGMT_DETAILS WHERE VESSELCODE='" + VesselCode + "' AND RISKID=" + RiskId.ToString());
            rptRisk.DataSource = HazardsList;
            rptRisk.DataBind();

            ShowFinalRatings();
        }
    }
    
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (txtOfficeComments.Text.Trim() == "")
        {
            txtEventDate.Focus();
            lblMsg_Closure.Text = "Please enter office comments.";
            return;
        }
        try
        {
            DataTable dtPos = Common.Execute_Procedures_Select_ByQuery("SELECT POSITIONNAME FROM DBO.POSITION WHERE POSITIONID IN (SELECT POSITION FROM DBO.Hr_PersonalDetails WHERE USERID=" + LoginId + ")");
            string PositionName = "";
            if (dtPos.Rows.Count > 0)
                PositionName = dtPos.Rows[0]["POSITIONNAME"].ToString();


            Common.Execute_Procedures_Select_ByQuery("UPDATE DBO.EV_VSL_RISKMGMT_MASTER SET STATUS='C',OFFICECOMMENTBY='" + Session["UserName"].ToString() + "',DESIGNATION='" + PositionName + "',COMMENTDATE=GETDATE(),OFFICE_COMMENTS='" + txtOfficeComments.Text.Trim().Replace("'", "`") + "' WHERE VESSELCODE='" + VesselCode + "' AND RISKID=" + RiskId.ToString());
            lblMsg_Closure.Text = "Approved successfully.";
        }
        catch (Exception ex)
        {
            lblMsg_Closure.Text = "Unable to approve .Error :" + Common.ErrMsg;
        }

    }

    protected void btnClosure_Click(object sender, EventArgs e)
    {
        //if (RiskId == 0)
        //{
        //    msg1.Text = "Please save risk first";
        //    return;
        //}
        //else
        //{
        //    string strchk = "SELECT * FROM dbo.EV_Off_RISKMGMT_DETAILS WHERE OfficeId=" + ddlOffice.SelectedValue + " AND RISKID=" + RiskId.ToString() + " AND ( LIKELIHOOD IS NULL OR CONSEQUENCES IS NULL OR RISKRANK IS NULL OR ISNULL(STD_CONTROL_MESASRUES,'')='' OR ISNULL(ADD_CONTROL_MEASURES,'')='' OR ISNULL(AGREED_TIME,'')='' OR ISNULL(RISK_AFTER_CONTROL,'')='' OR ISNULL(PIC_NAME,'')='' )";
        //    DataTable dt1 = Common.Execute_Procedures_Select_ByQuery(strchk);
        //    if (dt1.Rows.Count > 0)
        //    {
        //        msg1.Text = "Please fill all data before approval.";
        //        return;
        //    }
        //}

        dv_Closure.Visible = true;
    }     
    protected void btnCloseClosure_Click(object sender, EventArgs e)
    {
        if (RiskId > 0)
            ShowRecord();

        dv_Closure.Visible = false;
    }

    public string getFullText(string Code, string Type)
    {
        string res = "";
        if (Type == "L")
        {
            switch (Code)
            {
                case "1": res = "Unlikely";
                    break;
                case "2": res = "Possible";
                    break;
                case "3": res = "Quite Possible";
                    break;
                case "4": res = "Likely";
                    break;
                case "5": res = "Very Likely";
                    break;
                default: res = "";
                    break;
            }
        }
        if (Type == "C")
        {
            switch (Code)
            {
                case "1": res = "Negligible";
                    break;
                case "2": res = "Slight";
                    break;
                case "3": res = "Moderate";
                    break;
                case "4": res = "High";
                    break;
                case "5": res = "Very High";
                    break;
                default: res = "";
                    break;
            }
        }
        if (Type == "R")
        {
            switch (Code)
            {
                case "L": res = "Low Risk";
                    break;
                case "M": res = "Medium Risk";
                    break;
                case "H": res = "High Risk";
                    break;
                default: res = "";
                    break;
            }
        }
        return res;
    }

    public string getColor(string R)
    {
        string res = "";
        if (R == "L")
        {
            res = "#80E6B2";
        }
        else if (R == "M")
        {
            res = "#FFFFAD";
        }
        else if (R == "H")
        {
            res = "#FF7373";
        }
        else
        {
            res = "#ffffff";
        }
        return res;
    }

    public void ShowFinalRatings()
    {
        if (HazardsList.Rows.Count > 0)
        {
            lbl_In_Probability.Text = getFullText(HazardsList.Compute("MAX(LIKELIHOOD)", "").ToString(), "L");
            lbl_In_Impact.Text = getFullText(HazardsList.Compute("MAX(CONSEQUENCES)", "").ToString(), "C");
            string MaxRank = "";
            MaxRank = (Common.CastAsInt32(HazardsList.Compute("Count(RISKRANK)", "RISKRANK='H'")) > 0) ? "H" : "";
            if (MaxRank == "")
                MaxRank = (Common.CastAsInt32(HazardsList.Compute("Count(RISKRANK)", "RISKRANK='M'")) > 0) ? "M" : "";
            if (MaxRank == "")
                MaxRank = (Common.CastAsInt32(HazardsList.Compute("Count(RISKRANK)", "RISKRANK='L'")) > 0) ? "L" : "";

            lbl_In_Rating.Text = getFullText(MaxRank, "R");
            tdInRating.Style.Add("background-color", getColor(MaxRank));

            MaxRank = "";
            lbl_Re_Probability.Text = getFullText(HazardsList.Compute("MAX(Re_LIKELIHOOD)", "").ToString(), "L");
            lbl_Re_Impact.Text = getFullText(HazardsList.Compute("MAX(Re_CONSEQUENCES)", "").ToString(), "C");

            MaxRank = (Common.CastAsInt32(HazardsList.Compute("Count(Re_RISKRANK)", "Re_RISKRANK='H'")) > 0) ? "H" : "";
            if (MaxRank == "")
                MaxRank = (Common.CastAsInt32(HazardsList.Compute("Count(Re_RISKRANK)", "Re_RISKRANK='M'")) > 0) ? "M" : "";
            if (MaxRank == "")
                MaxRank = (Common.CastAsInt32(HazardsList.Compute("Count(Re_RISKRANK)", "Re_RISKRANK='L'")) > 0) ? "L" : "";

            lbl_Re_Rating.Text = getFullText(MaxRank, "R");
            tdReRating.Style.Add("background-color", getColor(MaxRank));
        }
    }
}