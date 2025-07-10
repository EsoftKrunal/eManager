using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.IO;
using System.Text;

public partial class Emtm_OfficeAbsenceHotoViewOnly : System.Web.UI.Page
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
    public int TID
    {
        get
        {
            return Common.CastAsInt32(ViewState["_TID"]);
        }
        set
        {
            ViewState["_TID"] = value;
        }
    }
    public bool AnyAcceptedORTakeOver
    {
        get
        {
            return Convert.ToBoolean(ViewState["AnyAcceptedORTakeOver"]);
        }
        set
        {
            ViewState["AnyAcceptedORTakeOver"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        LastVslCode = "";
        lblUMsg.Text = "";
        if (!Page.IsPostBack)
        {
            if (Request.QueryString["id"] != null)
            {
                AnyAcceptedORTakeOver = false; 
                
                HotoID = Common.CastAsInt32(Request.QueryString["id"]);
                ShowRecord();
                BindHandoverDetails();
                BindTakeoverDetails();
            }
        }

    }

    // ----------------------------------------  EVENT

    //protected void btnNotify_Click(object sender, EventArgs e)
    //{
    //    if (ddlBackup.SelectedIndex <= 0)
    //    {
    //        lblUMsg.Text = "Please select an Backup Employee.";
    //        return;
    //    }

    //    bool r1 =SendNotifyHandOver(ddlHandover.SelectedValue,"H");
    //    bool r2 = SendNotifyHandOver(ddlBackup.SelectedValue, "B");

    //    bool Res = r1 & r2; 
    //    if (Res)
    //    {
    //        Common.Execute_Procedures_Select_ByQueryCMS("UPDATE HR_OfficeAbsence_HOTOMaster SET NotifIed='Y',NotifiedOn=GETDATE() WHERE HOTOID=" + HotoID.ToString());
    //        ShowRecord();
    //        lblUMsg.Text = "Notofication mail(s) sent successfully.";
    //    }
    //    else
    //    {
    //        lblUMsg.Text = "Unable to send notification mail." ;
    //    }
    //}
    //protected void btnSave_Click(object sender, EventArgs e)
    //{ 
    //    //--------------------------------- hand over checks
    //    if (txtActLeaveFrom.Text.Trim() == "")
    //    {
    //        lblUMsg.Text = "Please enter start date.";
    //        txtActLeaveFrom.Focus();
    //        return;
    //    }
    //    DateTime dt;
    //    if (!DateTime.TryParse(txtActLeaveFrom.Text.Trim(), out dt))
    //    {
    //        lblUMsg.Text = "Please enter valid date.";
    //        txtActLeaveFrom.Focus();
    //        return;
    //    }
    //    if (ddlBackup.SelectedIndex <= 0)
    //    {
    //        lblUMsg.Text = "Please select an Backup Employee.";
    //        return;
    //    }
    //    //---------------------------------

    //    string STime = txtActLeaveFrom.Text.Trim() + " " + ddlEtHr.SelectedValue.Trim() + ":" + ddlEtMin.SelectedValue.Trim();
    //    string ETime = "";

    //    if (txtEndDt.Text.Trim() != "")
    //    {
    //        ETime = txtEndDt.Text.Trim() + " " + ddlEndHr.SelectedValue.Trim() + ":" + ddlEndMin.SelectedValue.Trim();
    //    }

    //    Common.Set_Procedures("DBO.HR_IU_Hoto");
    //    Common.Set_ParameterLength(5);
    //    Common.Set_Parameters
    //        (
    //            new MyParameter("@STIME", STime),
    //            new MyParameter("@ETIME", ETime),
    //            new MyParameter("@HOTOId", HotoID),                
    //            new MyParameter("@HandOverToId", ddlHandover.SelectedValue),
    //            new MyParameter("@BackUpId", ddlBackup.SelectedValue)
    //        );
    //    Boolean Res;
    //    DataSet ds = new DataSet();
    //    Res = Common.Execute_Procedures_IUD(ds);

    //    if (Res)
    //    {
    //        HotoID = Common.CastAsInt32(ds.Tables[0].Rows[0][0]);
    //        ShowRecord();
    //        lblUMsg.Text = "Record saved successfully.";
    //    }
    //    else
    //    {
    //        lblUMsg.Text = "Unable to save record.";
    //    }
    //}

    //protected void btnNotify1_Click(object sender, EventArgs e)
    //{
    //    // mail will to / HR_OfficeAbsence.EMPId (emtpdi > userid > loginid > email)

    //    //if (ddlHandover.SelectedIndex <= 0)
    //    //{
    //    //    lblUMsg.Text = "Please select an employee to handover notes.";
    //    //    return;
    //    //}
    //    //if (ddlBackup.SelectedIndex <= 0)
    //    //{
    //    //    lblUMsg.Text = "Please select an Backup Employee.";
    //    //    return;
    //    //}

    //    DataTable dt = Common.Execute_Procedures_Select_ByQuery("Select EmpID from dbo.HR_OfficeAbsence Where LeaverequestID=" + HotoID.ToString());
    //    bool Res = SendNotifyTakeOver(dt.Rows[0]["EmpID"].ToString());
    //    if (Res)
    //    {
    //        Common.Execute_Procedures_Select_ByQueryCMS("UPDATE HR_OfficeAbsence_HOTOMaster SET TakeOver='Y',TakeOverOn=GETDATE() WHERE HOTOID=" + HotoID.ToString());
    //        ShowRecord();
    //        lblUMsg1.Text = "Notofication mail(s) sent successfully.";
    //    }
    //    else
    //    {
    //        lblUMsg1.Text = "Unable to send notification mail.";
    //    }

    //}
    //protected void btnSave1_Click(object sender, EventArgs e)
    //{
    //    //--------------------------------- hand over checks
    //    if (txtActLeaveFrom.Text.Trim() == "")
    //    {
    //        lblUMsg1.Text = "Please enter travel start date.";
    //        txtActLeaveFrom.Focus();
    //        return;
    //    }
    //    DateTime dt;
    //    if (!DateTime.TryParse(txtActLeaveFrom.Text.Trim(), out dt))
    //    {
    //        lblUMsg1.Text = "Please enter valid travel start date.";
    //        txtActLeaveFrom.Focus();
    //        return;
    //    }
    //    if (ddlBackup.SelectedIndex <= 0)
    //    {
    //        lblUMsg1.Text = "Please select a backup employee.";
    //        return;
    //    }
    //    //---------------------------------
    //    //--------------------------------- take over checks
    //    if (txtEndDt.Text.Trim() == "")
    //    {
    //        lblUMsg1.Text = "Please enter travel end date.";
    //        txtEndDt.Focus();
    //        return;
    //    }
    //    DateTime dt1;
    //    if (!DateTime.TryParse(txtEndDt.Text.Trim(), out dt1))
    //    {
    //        lblUMsg1.Text = "Please enter valid travel end date.";
    //        txtEndDt.Focus();
    //        return;
    //    }
    //    //---------------------------------

    //    Common.Execute_Procedures_Select_ByQueryCMS("UPDATE HR_OfficeAbsence_HOTOMaster SET TakeOver='Y',TakeOverOn=GETDATE() WHERE HOTOID=" + HotoID.ToString());
    //    ShowRecord();
    //    BindHandoverDetails();
    //    lblUMsg1.Text = "Notofication mail(s) sent successfully.";
    //}

    protected void btnHandover_Click(object sender, EventArgs e)
    {
        btnCash.CssClass = "btn11";
        btnExp.CssClass = "btn11";

        pnlHandover.Visible = true;
        pnlTakeover.Visible = false;

        btnCash.CssClass = "btn11sel";
    }
    protected void btnTakeover_Click(object sender, EventArgs e)
    {
        btnCash.CssClass = "btn11";
        btnExp.CssClass = "btn11";

        pnlHandover.Visible = false;
        pnlTakeover.Visible = true;

        btnExp.CssClass = "btn11sel";
    }

    // -- Import Fleet Popup
    //protected void btnOpenFleetNotePopup_OnClick(object sender, EventArgs e)
    //{
    //    dvImportFleet.Visible = true;
    //}
    //protected void btnCloseFleetPopUp_OnClick(object sender, EventArgs e)
    //{
    //    dvImportFleet.Visible = false;
    //}
    //protected void btnImportFleetNode_OnClick(object sender, EventArgs e)
    //{
    //    Boolean Res=false;
    //    DataSet ds = new DataSet();
    //    string Sql = "";
    //    foreach (RepeaterItem itm in rptFleetNote.Items)
    //    {
    //        HiddenField hfTID = (HiddenField)itm.FindControl("hfTID");
    //        CheckBox Chk = (CheckBox)itm.FindControl("chkFN");
    //        if (Chk.Checked)
    //        {
    //            ds.Clear();
    //            Sql = " Exec dbo.HR_IU_HOTODetails " + HotoID.ToString() + "," + hfTID.Value + ",'H'";
    //            Common.Execute_Procedures_Select_ByQuery(Sql);
    //            Res = true;
    //        }
    //    }
    //    if (Res)
    //    {
    //        lblMsg.Text = "Fleet notes imported successfully.";
    //        BindHandoverDetails();
    //        BindTakeoverDetails();
    //    }
    //    else
    //        lblMsg.Text = "Please select fleet note.";
    //}
    //protected void btnEditFleetNotes_OnClick(object sender, EventArgs e)
    //{
    //    ImageButton btn = (ImageButton)sender;
    //    TID = Common.CastAsInt32(btn.CommandArgument);
    //    divAddFleetPopup.Visible = true;
    //    if (TID != 0)
    //    {
    //        lblAddEditMsg.Text = "Edit Fleet Note";
    //        txtComments.Text = "";
    //        txtDueDate.Text = "";
    //        ShowFleet();
    //    }
    //}
    //protected void btnDeleteFleetNote_OnClick(object sender, EventArgs e)
    //{
    //    ImageButton btn = (ImageButton)sender;
    //    TID = Common.CastAsInt32(btn.CommandArgument);

    //    string sql = "Delete from HR_OfficeAbsence_HOTODetails Where HotoID="+HotoID.ToString()+" And TID=" + TID.ToString();
    //    Common.Execute_Procedures_Select_ByQueryCMS(sql);

    //    lblUMsg.Text = "Fleet Note deleted successfully.";
    //    BindHandoverDetails();
    //    BindTakeoverDetails();
    //}

    // -- Add Fleet Popup
    //protected void btnAddFleetPopup_OnClick(object sender, EventArgs e)
    //{
    //    Button btn=(Button )sender;
    //    divAddFleetPopup.Visible = true;
    //    btnSaveNewFleet.CommandArgument = btn.CommandArgument;
    //    lblAddEditMsg.Text = "Add New Fleet Note";
    //    txtComments.Text = "";
    //    txtDueDate.Text = "";
    //    TID = 0;
    //}
    //protected void btnCloseAddFleetPopup_OnClick(object sender, EventArgs e)
    //{
    //    divAddFleetPopup.Visible = false;
    //}
    //protected void btnSaveNewFleet_OnClick(object sender, EventArgs e)
    //{
    //    if (ddlVessel.SelectedIndex == 0)
    //    {
    //        lblMsgAddFleet.Text = "Please select vessel."; ddlVessel.Focus(); return;
    //    }
    //    if (txtComments.Text.Trim() == "")
    //    {
    //        lblMsgAddFleet.Text = "Please enter comments."; txtComments.Focus(); return;
    //    }

    //    try
    //    {
    //        //string sql = "exec dbo.sp_InsertTopicsEmtm '" + ddlVessel.SelectedValue + "','" + txtComments.Text.Trim() + "','"+txtDueDate.Text.Trim()+"'," + Common.CastAsInt32(Session["loginid"].ToString()) + "";
    //        //DataSet Ds = Budget.getTable(sql);

    //        object oDueDate = new object();
    //        if (txtDueDate.Text.Trim() == "")
    //            oDueDate = DBNull.Value;
    //        else
    //            oDueDate = txtDueDate.Text.Trim();

    //        Common.Set_Procedures("dbo.sp_InsertTopicsEmtm");
    //        Common.Set_ParameterLength(5);
    //        Common.Set_Parameters(
    //                new MyParameter("@TID", TID),
    //                new MyParameter("@VesselCode", ddlVessel.SelectedValue),
    //                new MyParameter("@TopicDetails", txtComments.Text.Trim()),
    //                new MyParameter("@DueDate", oDueDate),
    //                new MyParameter("@CreatedBy", Common.CastAsInt32(Session["loginid"].ToString()))
    //            );
    //        Boolean Res;
    //        DataSet ds = new DataSet();
    //        Res = Common.Execute_Procedures_IUD(ds);
    //        if (Res)
    //        {

    //            if (TID == 0)
    //            {
    //                string STID = ds.Tables[0].Rows[0]["ID"].ToString();
    //                //  import fleet node also
    //                string Sql = " Exec dbo.HR_IU_HOTODetails " + HotoID.ToString() + "," + STID.ToString() + ",'" + btnSaveNewFleet.CommandArgument + "'";
    //                Common.Execute_Procedures_Select_ByQuery(Sql);
    //                lblMsgAddFleet.Text = "Fleet note added successfully. ";
    //            }
    //            else
    //            {
    //                lblMsgAddFleet.Text = "Fleet note updated successfully. ";
    //            }
    //            BindFleetNote();
    //            BindHandoverDetails();
    //            BindTakeoverDetails();
    //        }
    //        else
    //        {
    //            lblMsgAddFleet.Text = "Error while saving. ";
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsgAddFleet.Text = "Error while saving. " + ex.Message;
    //    }
    //}

    // -- Talk Over
    protected void lnkTakeOverClosure_OnClick(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;

        string sql = "Update dbo.tbl_Topic set ClosedBy=1 ,ClosedOn =getdate() Where TID=" + lnk.CommandArgument;
        Budget.getTable(sql);
        BindHandoverDetails();
        BindTakeoverDetails();
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
            txtActLeaveFrom.Text = dt.Rows[0]["ActFromDt"].ToString();
            txtEndDt.Text = dt.Rows[0]["ActToDt"].ToString() == "01-Jan-1900" ? "" : dt.Rows[0]["ActToDt"].ToString();

            if (dt.Rows[0]["Time"].ToString() != "")
            {
                string time = dt.Rows[0]["Time"].ToString();
                string[] str = time.Split(' ');

                ddlEtHr.Text = str[1].ToString().Split(':').GetValue(0).ToString().Trim();
                ddlEtMin.Text = str[1].ToString().Split(':').GetValue(1).ToString().Trim();
            }
            if (dt.Rows[0]["EndTime"].ToString() != "")
            {
                string endtime = dt.Rows[0]["EndTime"].ToString();
                string[] str = endtime.Split(' ');

                ddlEndHr.Text = str[1].ToString().Split(':').GetValue(0).ToString().Trim() + "&nbsp (Hrs.)";
                ddlEndMin.Text = str[1].ToString().Split(':').GetValue(1).ToString().Trim() + "&nbsp (Min.)";
            }

            lblPurpose.Text = dt.Rows[0]["Purpose"].ToString();
            lblPeriod.Text = dt.Rows[0]["LeaveFrom"].ToString() + " to " + dt.Rows[0]["LeaveTo"].ToString();

            DataTable dtHoto = Common.Execute_Procedures_Select_ByQueryCMS("SELECT (Select FirstName +' '+FamilyName from Hr_PersonalDetails PD Where PD.EmpID =HT.HandOverToID)HandOverTo,(Select FirstName +' '+FamilyName from Hr_PersonalDetails PD Where PD.EmpID =HT.BackupId)BackupBy,* FROM HR_OfficeAbsence_HOTOMaster HT where HotoId=" + HotoID);
            if (dtHoto.Rows.Count > 0)
            {
                ddlHandover.Text = dtHoto.Rows[0]["HandOverTo"].ToString();
                ddlBackup.Text = dtHoto.Rows[0]["BackUpBy"].ToString();
            }
            
        }
    }
    //public void BindFleetNote()
    //{
    //    string SQL = " Select TID,VESSELCODE ,TOPICNAME,TOPICDETAILS from Dbo.tbl_Topic Where Createdby=" + Session["loginid"].ToString() + " and Status  < 2 order by VESSELCODE";
    //    DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(SQL);
    //    rptFleetNote.DataSource = dt;
    //    rptFleetNote.DataBind();
        
    //}
    public void BindHandoverDetails()
    {
        LastVslCode = "";
        string SQL = " Select * from dbo.tbl_Topic where TID IN(Select TID from HR_OfficeAbsence_HOTODetails Where HotoID=" + HotoID.ToString() + " And HTType='H' ) order by VesselCode";
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(SQL);
        rptHotoDetails.DataSource = dt;
        rptHotoDetails.DataBind();

    }
    public void BindTakeoverDetails()
    {
        LastVslCode = "";
        string SQL = " Select * from dbo.tbl_Topic where TID IN(Select TID from HR_OfficeAbsence_HOTODetails Where HotoID=" + HotoID.ToString() + " And HTType='T' ) order by VesselCode";
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(SQL);
        rptTakeover.DataSource = dt;
        rptTakeover.DataBind();
    }
    public bool CompareVSl(object vsl)
    {
        bool a=LastVslCode != vsl.ToString();
        LastVslCode = vsl.ToString();
        return a;
    }
    
}