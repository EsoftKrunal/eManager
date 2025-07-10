using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Applicant_ViewPromotionChecklist1 : System.Web.UI.Page
{
    int planningid
    {
        set { ViewState["planningid"] = value; }
        get { return Common.CastAsInt32(ViewState["planningid"]); }
    }
    int CheckListMasterID
    {
        set { ViewState["_CheckListMasterID"] = value; }
        get { return Common.CastAsInt32(ViewState["_CheckListMasterID"]); }
    }
    int crewid
    {
        set { ViewState["crewid"] = value; }
        get { return Common.CastAsInt32(ViewState["crewid"]); }
    }
    int rankid
    {
        set { ViewState["rankid"] = value; }
        get { return Common.CastAsInt32(ViewState["rankid"]); }
    }
    int vesselid
    {
        set { ViewState["vesselid"] = value; }
        get { return Common.CastAsInt32(ViewState["vesselid"]); }
    }
    int TableID
    {
        set { ViewState["_TableID"] = value; }
        get { return Common.CastAsInt32(ViewState["_TableID"]); }
    }
    public int LoginId
    {
        set { ViewState["LoginId"] = value; }
        get { return Common.CastAsInt32(ViewState["LoginId"]); }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMess.Text = "";
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        if (!IsPostBack)
        {
            planningid = Common.CastAsInt32(Request.QueryString["_P"]);
            CheckListMasterID = Common.CastAsInt32(Request.QueryString["type"]);
            string sql = "select CheckListname from tblCheckListMaster where id=" + CheckListMasterID;
            DataTable dt = Budget.getTable(sql).Tables[0];
            if (dt.Rows.Count > 0)
            {
                litPageHeading.Text = dt.Rows[0][0].ToString();
                ShowData();
                Bind_Checklist();
            }
            else
            {
                btnSave.Visible = false;
            }


        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        bool AllChecked = true;
        foreach (RepeaterItem item in rptPromotionChecklist.Items)
        {
            CheckBox chKVerify = (CheckBox)item.FindControl("chKVerify");
            if (!chKVerify.Checked)
                AllChecked = false;
        }
        if (!AllChecked)
        {
            lblMess.Text = "Please verify all checklist";
            return;
        }
        foreach (RepeaterItem item in rptPromotionChecklist.Items)
        {
            HiddenField hfdTableID = (HiddenField)item.FindControl("hfdTableID");
            HiddenField hfdChecklistItemID = (HiddenField)item.FindControl("hfdChecklistItemID");


            CheckBox chKVerify = (CheckBox)item.FindControl("chKVerify");
            TextBox txtComments = (TextBox)item.FindControl("txtComments");
            int tableId = 0;
            if (! string.IsNullOrWhiteSpace(hfdTableID.Value))
            tableId = Convert.ToInt32(hfdTableID.Value);

            Common.Set_Procedures("sp_IU_tbl_CrewPlanningCheckList");
            Common.Set_ParameterLength(7);
            Common.Set_Parameters(
                 new MyParameter("@TableId", tableId),
                 new MyParameter("@PlanningID", planningid),
                 new MyParameter("@CheckListMasterId", CheckListMasterID),
                 new MyParameter("@CheckListItemId", hfdChecklistItemID.Value),
                 new MyParameter("@Verified", (chKVerify.Checked) ? 1 : 0),
                 new MyParameter("@Comments", txtComments.Text.Trim()),
                 new MyParameter("@VerifiedBy", Session["UserName"].ToString())
                );
            Boolean res;
            DataSet Ds = new DataSet();
            res = Common.Execute_Procedures_IUD_CMS(Ds);
            if (res)
            {

            }
            else
            {

            }
        }
        //--------------------------------
        Bind_Checklist();
        lblMess.Text = "&nbsp;Record Saved successfully.";
    }
    public void Bind_Checklist()
    {
        string sql = " select ROW_NUMBER() over(order by m.CheckListItemName) as Sr,m.ID,m.CheckListItemName,TableId,FileName,Verified,VerifiedBy,VerifiedOn,Comments from tblCheckListItems m " +
                     "   left join tbl_CrewPlanningCheckList d on m.CheckListMasterID = " + CheckListMasterID + " and m.ID = d.CheckListItemId and d.planningid =" + planningid +  
                     "   where m.CheckListMasterID = " + CheckListMasterID;

        DataTable dt = Budget.getTable(sql).Tables[0];
        rptPromotionChecklist.DataSource = dt;
        rptPromotionChecklist.DataBind();

    }
    protected void ShowData()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("SELECT RELIEVERID,VESSELID,RELIEVERRANKID FROM DBO.CREWVESSELPLANNINGHISTORY WHERE PlanningId=" + planningid.ToString());
        if (dt.Rows.Count > 0)
        {
            crewid = Common.CastAsInt32(dt.Rows[0][0]);
            vesselid = Common.CastAsInt32(dt.Rows[0][1]);
            rankid = Common.CastAsInt32(dt.Rows[0][2]);
            LoginId = Common.CastAsInt32(Session["LoginId"].ToString());
        }
        if (crewid > 0 && rankid > 0 && vesselid > 0)
        {

            string SQl = "SELECT CREWNUMBER , FIRSTNAME + ' ' +  isnull(MIDDLENAME,'') + ' ' + LASTNAME AS CREWNAME, " +
                        "(SELECT VESSELNAME FROM VESSEL WHERE VESSELID=" + vesselid.ToString() + ") AS VESSELNAME, " +
                        "(SELECT RANKNAME FROM RANK WHERE RANKID=" + rankid.ToString() + ") AS RANKNAME, " +
                        "(SELECT COUNTRYNAME FROM COUNTRY WHERE COUNTRYID=nationalityid) AS NATIONALITY, " +
                        "replace(convert(varchar,DateOfBirth,106) ,' ','-') as DOB " +
                        "FROM CREWPERSONALDETAILS WHERE CREWID=" + crewid.ToString();
            DataTable DT = Common.Execute_Procedures_Select_ByQueryCMS(SQl);
            if (DT.Rows.Count > 0)
            {
                lblID.Text = DT.Rows[0]["CREWNUMBER"].ToString();
                lblName.Text = DT.Rows[0]["CREWNAME"].ToString();
                lblRank.Text = DT.Rows[0]["RANKNAME"].ToString();
                lblNationality.Text = DT.Rows[0]["NATIONALITY"].ToString();
                lblDOB.Text = DT.Rows[0]["DOB"].ToString();
                lblVName.Text = DT.Rows[0]["VESSELNAME"].ToString();
            }
        }
    }

    protected void btnUploadPopup_OnClick(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        HiddenField hfdTableID = (HiddenField)btn.Parent.FindControl("hfdTableID");
        TableID = Common.CastAsInt32(hfdTableID.Value);
        divFileUpload.Visible = true;
    }
    protected void btnCloseFileUPloadPopup_OnClick(object sender, EventArgs e)
    {
        divFileUpload.Visible = false;
    }

    protected void btnUploadFile_OnClick(object sender, EventArgs e)
    {

        if (UplaodFile.HasFile)
        {
            Common.Set_Procedures("sp_UploadFile_tbl_CrewPlanningCargoUpload");
            Common.Set_ParameterLength(3);
            Common.Set_Parameters(
                 new MyParameter("@TableID", TableID),                 
                 new MyParameter("@FileName", UplaodFile.FileName),
                 new MyParameter("@FileContent", UplaodFile.FileContent)
                );
            Boolean res;
            DataSet Ds = new DataSet();
            res = Common.Execute_Procedures_IUD_CMS(Ds);
            if (res)
            {
                Bind_Checklist();
                divFileUpload.Visible = false;
                //lblUPloadFile.Text = "File uploaded successfully";
            }
            else
            {
                lblUPloadFile.Text = "File could not be uploaded.";                
            }
        }
        else
        {
            lblUPloadFile.Text = "Please select the file to uploaded.";
        }
    }
    protected void btnDownload_OnClick(object sender, EventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        int TableId = Common.CastAsInt32(btn.CommandArgument);
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("select FileName,FileContent from DBO.tbl_CrewPlanningCheckList WHERE TABLEID=" + TableId);
        if (TableId > 0)
        {
            string filename = dt.Rows[0]["FileName"].ToString();
            byte[] filedata = (byte[])dt.Rows[0]["FileContent"];
            //--------------
            Response.Clear();
            Response.AppendHeader("content-disposition", "attachment; filename=" + filename);
            Response.ContentType = "application/octet-stream";
            Response.BinaryWrite(filedata);
            Response.Flush();
            Response.End();
        }
    }
    
}
    

    

