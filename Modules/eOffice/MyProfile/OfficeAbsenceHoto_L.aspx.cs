using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.IO;
using System.Text;

public partial class Emtm_OfficeAbsenceHoto_L : System.Web.UI.Page
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
        lblMsgAddFleet.Text = "";
        lblMsg.Text = "";
        if (!Page.IsPostBack)
        {
            //BindTime();
            if (Request.QueryString["id"] != null)
            {
                AnyAcceptedORTakeOver = false; 
                BindVessel();
                HotoID = Common.CastAsInt32(Request.QueryString["id"]);

                BindHandover();
                BindBackupEmp();

                ShowRecord();
             
                BindFleetNote();
                BindHandoverDetails();
                BindTakeoverDetails();
            }
        }

    }

    // ----------------------------------------  EVENT
    
    protected void btnNotify_Click(object sender, EventArgs e)
    {
        if (ddlHandover.SelectedIndex <= 0)
        {
            lblUMsg.Text = "Please select an handover Employee.";
            return;
        }

        bool r1 =SendNotifyHandOver(ddlHandover.SelectedValue,"H");
        bool r2 = true;

        if(ddlBackup.SelectedIndex >0)
            r2 = SendNotifyHandOver(ddlBackup.SelectedValue, "B");

        bool Res = r1 & r2; 
        if (Res)
        {
            Common.Execute_Procedures_Select_ByQueryCMS("UPDATE HR_OfficeAbsence_HOTOMaster_L SET NotifIed='Y',NotifiedOn=GETDATE() WHERE HOTOID=" + HotoID.ToString());
            ShowRecord();
            lblUMsg.Text = "Notofication mail(s) sent successfully.";
        }
        else
        {
            lblUMsg.Text = "Unable to send notification mail." ;
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    { 
        if (ddlHandover.SelectedIndex <= 0)
        {
            lblUMsg.Text = "Please select an handover Employee.";
            return;
        }
      

        Common.Set_Procedures("DBO.HR_IU_Hoto_L");
        Common.Set_ParameterLength(3);
        Common.Set_Parameters
            (
                new MyParameter("@HOTOId", HotoID),                
                new MyParameter("@HandOverToId", ddlHandover.SelectedValue),
                new MyParameter("@BackUpId", ddlBackup.SelectedValue)
            );
        Boolean Res;
        DataSet ds = new DataSet();
        Res = Common.Execute_Procedures_IUD(ds);

        if (Res)
        {
            HotoID = Common.CastAsInt32(ds.Tables[0].Rows[0][0]);
            ShowRecord();
            lblUMsg.Text = "Record saved successfully.";
        }
        else
        {
            lblUMsg.Text = "Unable to save record.";
        }
    }

    protected void btnNotify1_Click(object sender, EventArgs e)
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("Select EmpID from dbo.HR_LeaveRequest Where LeaverequestID=" + HotoID.ToString());
        bool Res = SendNotifyTakeOver(dt.Rows[0]["EmpID"].ToString());
        if (Res)
        {
            Common.Execute_Procedures_Select_ByQueryCMS("UPDATE HR_OfficeAbsence_HOTOMaster_L SET TakeOver='Y',TakeOverOn=GETDATE() WHERE HOTOID=" + HotoID.ToString());
            ShowRecord();
            lblUMsg1.Text = "Notofication mail(s) sent successfully.";
        }
        else
        {
            lblUMsg1.Text = "Unable to send notification mail.";
        }
        
    }
    protected void btnSave1_Click(object sender, EventArgs e)
    {
        ////--------------------------------- hand over checks
        //if (txtActLeaveFrom.Text.Trim() == "")
        //{
        //    lblUMsg1.Text = "Please enter travel start date.";
        //    txtActLeaveFrom.Focus();
        //    return;
        //}
        //DateTime dt;
        //if (!DateTime.TryParse(txtActLeaveFrom.Text.Trim(), out dt))
        //{
        //    lblUMsg1.Text = "Please enter valid travel start date.";
        //    txtActLeaveFrom.Focus();
        //    return;
        //}
        //if (ddlHandover.SelectedIndex <= 0)
        //{
        //    lblUMsg1.Text = "Please select a handover employee.";
        //    return;
        //}
        ////---------------------------------
        ////--------------------------------- take over checks
        //if (txtEndDt.Text.Trim() == "")
        //{
        //    lblUMsg1.Text = "Please enter travel end date.";
        //    txtEndDt.Focus();
        //    return;
        //}
        //DateTime dt1;
        //if (!DateTime.TryParse(txtEndDt.Text.Trim(), out dt1))
        //{
        //    lblUMsg1.Text = "Please enter valid travel end date.";
        //    txtEndDt.Focus();
        //    return;
        //}

        //---------------------------------
        if (txtTakeOverDate.Text.Trim() == "")
        {
            lblUMsg1.Text = "Please enter takeover date.";
            txtTakeOverDate.Focus();
            return;
        }
        DateTime dtTakeOver;
        if (!DateTime.TryParse(txtTakeOverDate.Text.Trim(), out dtTakeOver))
        {
            lblUMsg1.Text = "Please enter valid takeover date.";
            txtTakeOverDate.Focus();
            return;
        }
        //---------------------------------

        //string STime = txtActLeaveFrom.Text.Trim() + " " + ddlEtHr.SelectedValue.Trim() + ":" + ddlEtMin.SelectedValue.Trim();
        //string ETime = "";

        //if (txtEndDt.Text.Trim() != "")
        //{
        //    ETime = txtEndDt.Text.Trim() + " " + ddlEndHr.SelectedValue.Trim() + ":" + ddlEndMin.SelectedValue.Trim();
        //}
        ////---------------------------------
        //Common.Set_Procedures("DBO.Emtm_IU_Hoto_1_L");
        //Common.Set_ParameterLength(3);
        //Common.Set_Parameters
        //(
        //    new MyParameter("@STIME", STime),
        //    new MyParameter("@ETIME", ETime),
        //    new MyParameter("@HOTOId", HotoID)
        //);
        //Boolean Res;
        //DataSet ds = new DataSet();
        //Res = Common.Execute_Procedures_IUD(ds);

        Common.Execute_Procedures_Select_ByQueryCMS("UPDATE HR_OfficeAbsence_HOTOMaster_L SET TakeOver='Y',TakeOverOn='" + txtTakeOverDate.Text + "' WHERE HOTOID=" + HotoID.ToString());
        ShowRecord();
        BindHandoverDetails();
        lblUMsg1.Text = "Record saved successfully.";
    }

    protected void btnHandover_Click(object sender, EventArgs e)
    {
        btnCash.CssClass = "btn11";
        //btnExp.CssClass = "btn11";

        pnlHandover.Visible = true;
        //pnlTakeover.Visible = false;

        btnCash.CssClass = "btn11sel";
    }
    protected void btnTakeover_Click(object sender, EventArgs e)
    {
        btnCash.CssClass = "btn11";
        //btnExp.CssClass = "btn11";

        pnlHandover.Visible = false;
        //pnlTakeover.Visible = true;

        //btnExp.CssClass = "btn11sel";
    }
    
    // -- Import Fleet Popup
    protected void btnOpenFleetNotePopup_OnClick(object sender, EventArgs e)
    {
        dvImportFleet.Visible = true;
    }
    protected void btnCloseFleetPopUp_OnClick(object sender, EventArgs e)
    {
        dvImportFleet.Visible = false;
    }
    protected void btnImportFleetNode_OnClick(object sender, EventArgs e)
    {
        Boolean Res=false;
        DataSet ds = new DataSet();
        string Sql = "";
        foreach (RepeaterItem itm in rptFleetNote.Items)
        {
            HiddenField hfTID = (HiddenField)itm.FindControl("hfTID");
            CheckBox Chk = (CheckBox)itm.FindControl("chkFN");
            if (Chk.Checked)
            {
                ds.Clear();
                Sql = " Exec dbo.HR_IU_HOTODetails_L " + HotoID.ToString() + "," + hfTID.Value + ",'H'";
                Common.Execute_Procedures_Select_ByQuery(Sql);
                Res = true;
            }
        }
        if (Res)
        {
            lblMsg.Text = "Fleet notes imported successfully.";
            BindHandoverDetails();
            BindTakeoverDetails();
        }
        else
            lblMsg.Text = "Please select fleet note.";
    }
    protected void btnEditFleetNotes_OnClick(object sender, EventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        TID = Common.CastAsInt32(btn.CommandArgument);
        divAddFleetPopup.Visible = true;
        if (TID != 0)
        {
            lblAddEditMsg.Text = "Edit Fleet Note";
            txtComments.Text = "";
            //txtDueDate.Text = "";
            ShowFleet();
        }
    }
    protected void btnDeleteFleetNote_OnClick(object sender, EventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        TID = Common.CastAsInt32(btn.CommandArgument);

        string sql = "Delete from HR_OfficeAbsence_HOTODetails_L Where HotoID="+HotoID.ToString()+" And TID=" + TID.ToString();
        Common.Execute_Procedures_Select_ByQueryCMS(sql);

        lblUMsg.Text = "Fleet Note deleted successfully.";
        BindHandoverDetails();
        BindTakeoverDetails();
    }
    
    // -- Add Fleet Popup
    protected void btnAddFleetPopup_OnClick(object sender, EventArgs e)
    {
        Button btn=(Button )sender;
        divAddFleetPopup.Visible = true;
        btnSaveNewFleet.CommandArgument = btn.CommandArgument;
        lblAddEditMsg.Text = "Add New Fleet Note";
        txtComments.Text = "";
        //txtDueDate.Text = "";
        TID = 0;
    }
    protected void btnCloseAddFleetPopup_OnClick(object sender, EventArgs e)
    {
        divAddFleetPopup.Visible = false;
    }
    protected void btnSaveNewFleet_OnClick(object sender, EventArgs e)
    {
        
        if (ddlVessel.SelectedIndex == 0)
        {
            lblMsgAddFleet.Text = "Please select vessel."; ddlVessel.Focus(); return;
        }
        if (txtComments.Text.Trim() == "")
        {
            lblMsgAddFleet.Text = "Please enter comments."; txtComments.Focus(); return;
        }
        try
        {
            //string sql = "exec dbo.sp_InsertTopicsEmtm '" + ddlVessel.SelectedValue + "','" + txtComments.Text.Trim() + "','"+txtDueDate.Text.Trim()+"'," + Common.CastAsInt32(Session["loginid"].ToString()) + "";
            //DataSet Ds = Budget.getTable(sql);

            object oDueDate = new object();
            //if (txtDueDate.Text.Trim() == "")
                oDueDate = DBNull.Value;
            //else
            //    oDueDate = txtDueDate.Text.Trim();

            Common.Set_Procedures("dbo.sp_InsertTopicsEmtm");
            Common.Set_ParameterLength(5);
            Common.Set_Parameters(
                    new MyParameter("@TID", TID),
                    new MyParameter("@VesselCode", ddlVessel.SelectedValue),
                    new MyParameter("@TopicDetails", txtComments.Text.Trim()),
                    new MyParameter("@DueDate", oDueDate),
                    new MyParameter("@CreatedBy", Common.CastAsInt32(Session["loginid"].ToString()))
                );
            Boolean Res;
            DataSet ds = new DataSet();
            Res = Common.Execute_Procedures_IUD(ds);
            if (Res)
            {

                if (TID == 0)
                {
                    string STID = ds.Tables[0].Rows[0]["ID"].ToString();
                    //  import fleet node also
                    string Sql = " Exec dbo.HR_IU_HOTODetails_L " + HotoID.ToString() + "," + STID.ToString() + ",'" + btnSaveNewFleet.CommandArgument + "'";
                    Common.Execute_Procedures_Select_ByQuery(Sql);
                    lblMsgAddFleet.Text = "Fleet note added successfully. ";
                }
                else
                {
                    lblMsgAddFleet.Text = "Fleet note updated successfully. ";
                }
                BindFleetNote();
                BindHandoverDetails();
                BindTakeoverDetails();
            }
            else
            {
                lblMsgAddFleet.Text = "Error while saving. ";
            }
        }
        catch (Exception ex)
        {
            lblMsgAddFleet.Text = "Error while saving. " + ex.Message;
        }
    }

    protected void btnViewFleetNotes_OnClick(object sender, EventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        TID = Common.CastAsInt32(btn.CommandArgument);
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
        string SQL = "SELECT REPLACE(Convert(varchar(11), OA.LeaveFrom, 106),' ','-') AS ActFromDt,REPLACE(Convert(varchar(11), OA.LeaveTo, 106),' ','-') AS ActToDt, OA.LeaveFrom AS Time,OA.LeaveTo AS EndTime, 'Leave' as  Purpose, REPLACE(Convert(varchar(11), OA.LeaveFrom, 106),' ','-') AS LeaveFrom, REPLACE(Convert(varchar(11), OA.LeaveTo, 106),' ','-') AS LeaveTo FROM HR_LeaveRequest OA " +
                     //"INNER JOIN HR_LeavePurpose LP ON OA.PurposeId = LP.PurposeId " +
                     "WHERE OA.LeaveRequestId = " + HotoID;
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(SQL);

        if (dt.Rows.Count > 0)
        {
            //txtActLeaveFrom.Text = dt.Rows[0]["ActFromDt"].ToString();
            //txtEndDt.Text = dt.Rows[0]["ActToDt"].ToString() == "01-Jan-1900" ? "" : dt.Rows[0]["ActToDt"].ToString();

            //if (dt.Rows[0]["Time"].ToString() != "")
            //{
            //    string time = dt.Rows[0]["Time"].ToString();
            //    string[] str = time.Split(' ');

            //    ddlEtHr.SelectedValue = str[1].ToString().Split(':').GetValue(0).ToString().Trim();
            //    ddlEtMin.SelectedValue = str[1].ToString().Split(':').GetValue(1).ToString().Trim();
            //}
            //if (dt.Rows[0]["EndTime"].ToString() != "")
            //{
            //    string endtime = dt.Rows[0]["EndTime"].ToString();
            //    string[] str = endtime.Split(' ');

            //    ddlEndHr.SelectedValue = str[1].ToString().Split(':').GetValue(0).ToString().Trim();
            //    ddlEndMin.SelectedValue = str[1].ToString().Split(':').GetValue(1).ToString().Trim();
            //}

            lblPurpose.Text = dt.Rows[0]["Purpose"].ToString();
            lblPeriod.Text = dt.Rows[0]["LeaveFrom"].ToString() + " to " + dt.Rows[0]["LeaveTo"].ToString();

            DataTable dtHoto = Common.Execute_Procedures_Select_ByQueryCMS("SELECT * FROM HR_OfficeAbsence_HOTOMaster_L where HotoId=" + HotoID);
            if (dtHoto.Rows.Count > 0)
            {
                string TakeOverStatus = dtHoto.Rows[0]["TakeOver"].ToString().Trim();

                ddlHandover.SelectedValue = dtHoto.Rows[0]["HandOverToId"].ToString();
                ddlBackup.SelectedValue = dtHoto.Rows[0]["BackUpId"].ToString();

                lblNotifiedon.Text = Common.ToDateString(dtHoto.Rows[0]["NotifiedOn"]);
                lblTakeOverOn.Text = Common.ToDateString(dtHoto.Rows[0]["TakeOverOn"]);

                lblHAccrejOn.Text = "";
                lblBAccrejOn.Text = "";

                string HStatus = dtHoto.Rows[0]["HStatus"].ToString().Trim();
                if (HStatus == "A")
                {
                    lblHAccrejOn.Text = "Accepted / " + Common.ToDateString(dtHoto.Rows[0]["HOn"]);
                }
                if (HStatus == "R")
                {
                    lblHAccrejOn.Text = "Rejected / " + Common.ToDateString(dtHoto.Rows[0]["HOn"]);
                }

                string BStatus = dtHoto.Rows[0]["BStatus"].ToString().Trim();
                if (BStatus == "A")
                {
                    lblBAccrejOn.Text = "Accepted / " + Common.ToDateString(dtHoto.Rows[0]["BOn"]);
                }
                if (BStatus == "R")
                {
                    lblBAccrejOn.Text = "Rejected / " + Common.ToDateString(dtHoto.Rows[0]["BOn"]);
                }

                AnyAcceptedORTakeOver = false;
                btnNotify.Visible = true;

                btnNotify1.Enabled = false;
                btnSave1.Enabled = false;

                if (HStatus == "A" || BStatus == "A")
                {
                    AnyAcceptedORTakeOver = true;
                    btnFleetImportPopup.Visible = false;
                    btnAddFleetPopup.Visible = false;

                    btnNotify1.Enabled = true;
                    btnSave1.Enabled = true;
                }

                if (TakeOverStatus == "Y")
                {
                    AnyAcceptedORTakeOver = true;

                    btnFleetImportPopup.Visible = false;
                    btnAddFleetPopup.Visible = false;
                    //btnAddTakeover.Visible = false;

                    btnSave.Visible = false;
                    btnNotify.Visible = false;

                    btnSave1.Visible = false;
                    btnNotify1.Visible = true;

                    txtTakeOverDate.Text =Common.ToDateString(dtHoto.Rows[0]["TakeOverOn"]);
                }
                else
                {
                    
                    btnNotify1.Visible = false;
                    txtTakeOverDate.Text ="";
                    
                }
            }
            else
            {
                btnNotify.Visible = false;
                btnNotify1.Visible = false;

                btnSave1.Enabled = false;
            }
        }
    }
    public void BindFleetNote()
    {
        string SQL = " Select TID,VESSELCODE ,TOPICNAME,TOPICDETAILS from Dbo.tbl_Topic Where Createdby=" + Session["loginid"].ToString() + " and Status  < 2 order by VESSELCODE";
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(SQL);
        rptFleetNote.DataSource = dt;
        rptFleetNote.DataBind();
        
    }
    
    public void BindHandoverDetails()
    {
        LastVslCode = "";
        string SQL = " Select *,(select  '[ '+ UserID +' ]' from  DBO.userlogin U where U.Loginid=tbl_Topic.CreatedBy )CreatedByName from dbo.tbl_Topic where TID IN(Select TID from DBO.HR_OfficeAbsence_HOTODetails_L Where HotoID=" + HotoID.ToString() + " And HTType='H' ) order by VesselCode";
        DataTable dt = Budget.getTable(SQL).Tables[0];
        rptHotoDetails.DataSource = dt;
        rptHotoDetails.DataBind();

    }
    public void BindTakeoverDetails()
    {
        //LastVslCode = "";
        //string SQL = " Select * from dbo.tbl_Topic where TID IN(Select TID from HR_OfficeAbsence_HOTODetails Where HotoID=" + HotoID.ToString() + " And HTType='T' ) order by VesselCode";
        //DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(SQL);
        //rptTakeover.DataSource = dt;
        //rptTakeover.DataBind();
    }
    public void ShowFleet()
    {
        string SQL = " Select VesselCode,TopicDetails,DueDate from dbo.tbl_Topic Where Tid="+TID.ToString();
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(SQL);
        if (dt.Rows.Count > 0)
        {
            ddlVessel.SelectedValue = dt.Rows[0]["VesselCode"].ToString();
            txtComments.Text = dt.Rows[0]["TopicDetails"].ToString();
            //txtDueDate.Text = Common.ToDateString(dt.Rows[0]["DueDate"]);
        }
        
    }

    public void BindVessel()
    {
        DataTable dt = Budget.getTable("select * from DBO.vessel where vesselstatusid<>2  Order by vesselname ").Tables[0];
        ddlVessel.DataSource = dt;
        ddlVessel.DataTextField = "VesselName";
        ddlVessel.DataValueField= "VesselCode";
        ddlVessel.DataBind();
        ddlVessel.Items.Insert(0, new ListItem("< Select >", ""));
    }

    //public void BindTime()
    //{
    //    for (int i = 0; i < 24; i++)
    //    {
    //        if (i < 10)
    //        {
    //            ddlEndHr.Items.Add(new ListItem("0" + i.ToString(), "0" + i.ToString()));
    //            ddlEtHr.Items.Add(new ListItem("0" + i.ToString(), "0" + i.ToString()));
    //        }
    //        else
    //        {
    //            ddlEndHr.Items.Add(new ListItem(i.ToString(), i.ToString()));
    //            ddlEtHr.Items.Add(new ListItem(i.ToString(), i.ToString()));
    //        }
    //    }

    //    for (int i = 0; i < 60; i++)
    //    {
    //        if (i < 10)
    //        {
    //            ddlEndMin.Items.Add(new ListItem("0" + i.ToString(), "0" + i.ToString()));
    //            ddlEtMin.Items.Add(new ListItem("0" + i.ToString(), "0" + i.ToString()));
    //        }
    //        else
    //        {
    //            ddlEndMin.Items.Add(new ListItem(i.ToString(), i.ToString()));
    //            ddlEtMin.Items.Add(new ListItem(i.ToString(), i.ToString()));
    //        }
    //    }
    //}
    public void BindHandover()
    {
        //string SQL =" Select EmpID,(FirstName+' '+FamilyName)EmpName from Hr_PersonalDetails  " +
        //            " Where Convert(Varchar,Office)+'|'+Convert(Varchar,Position)+'|'+Convert(Varchar,Department) in  " +
        //            " (   " +
        //            "    Select Convert(varchar, Office ) +'|'+ Convert(varchar, Position ) +'|'+ Convert(varchar, Department ) from Hr_PersonalDetails Where EmpID=" + Session["ProfileId"].ToString() + " " +
        //            " ) and EMPId<>" + Session["ProfileId"].ToString() + "  order by EmpName";

        string SQL = " Select EmpID,(FirstName+' '+FamilyName)EmpName from Hr_PersonalDetails  " +
                    " Where Office in  " +
                    " (   " +
                    "    Select Office from Hr_PersonalDetails Where EmpID=" + Session["ProfileId"].ToString() + " " +
                    " ) and EMPId<>" + Session["ProfileId"].ToString() + " and DRC IS NULL order by EmpName";

        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(SQL);
        ddlHandover.DataSource = dt;
        ddlHandover.DataTextField = "EmpName";
        ddlHandover.DataValueField = "EmpID";
        ddlHandover.DataBind();
        ddlHandover.Items.Insert(0, new ListItem("< Select >","0"));
    }
    public void BindBackupEmp()
    {
        //string SQL = " exec HR_LeaveDiscussion_getApproverChain_ByEmpId " + Session["ProfileId"].ToString();
        string SQL = " Select EmpID,(FirstName+' '+FamilyName)EmpName from Hr_PersonalDetails  " +
                    " Where Office in  " +
                    " (   " +
                    "    Select Office from Hr_PersonalDetails Where EmpID=" + Session["ProfileId"].ToString() + " " +
                    " ) and EMPId<>" + Session["ProfileId"].ToString() + " and DRC IS NULL order by EmpName";

        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(SQL);
        ddlBackup.DataSource = dt;
        ddlBackup.DataTextField = "EmpName";
        ddlBackup.DataValueField = "EmpID";
        ddlBackup.DataBind();
        ddlBackup.Items.Insert(0, new ListItem("< Select >", "0"));
    }
    public bool CompareVSl(object vsl)
    {
        bool a=LastVslCode != vsl.ToString();
        LastVslCode = vsl.ToString();
        return a;
    }
    protected bool SendNotifyHandOver(string ToEmpId, string BH)
    {
        string ReplyMess;
        int LoginId = Common.CastAsInt32(Session["loginid"].ToString());
        DataTable dtLoginMail = Common.Execute_Procedures_Select_ByQueryCMS("select FirstName+ ' ' + LastName as UserName,email from userlogin where loginid=" + LoginId.ToString());
        DataTable dtHandOver = Common.Execute_Procedures_Select_ByQueryCMS("select FirstName+ ' ' + LastName as UserName,email from userlogin where loginid in (select userid from Hr_PersonalDetails where empid=" + ToEmpId + ")");

        string MailFrom = dtLoginMail.Rows[0]["email"].ToString();
        string LoginName = dtLoginMail.Rows[0]["UserName"].ToString();

        string[] ToAdds = { dtHandOver.Rows[0]["email"].ToString() };
        string ToName = dtHandOver.Rows[0]["UserName"].ToString();

        string[] CCAdds = { };

        string[] BCCAdds = { };

        string Subject = "Handover Note";

        string BasePath = Request.Url.AbsoluteUri;
        BasePath = BasePath.Substring(0, BasePath.LastIndexOf("/"));
        string LinkToAcceptReject = BasePath + "/Emtm_OfficeAbsenceHotoApproval_L.aspx?key=" + "5hjhkl25557" + HotoID.ToString().PadLeft(6, '_') + "vhjk2525jll" + BH;

        StringBuilder sb = new StringBuilder();
        sb.Append("==================================Handover Notes===================================" + "</br>");
        sb.Append("Dear " + ToName + "," + "</br></br>");
        sb.Append("My handover notes are updated in Fleet Notes, which can be accessed by below link. Please confirm by accepting it." + "</br></br>");
        sb.Append("<a href='" + LinkToAcceptReject + "'>Click here to Accept / Reject </a>" + "</br></br>");
        sb.Append("Thanks" + "</br></br>");
        sb.Append(LoginName + "</br>");
        sb.Append("===================================================================================" + "</br>");

        string MailBody = sb.ToString();
        bool Res = SendEmail.SendeMail(LoginId, MailFrom.ToString(), MailFrom, ToAdds, CCAdds, BCCAdds, Subject, MailBody, out ReplyMess, "");
        return Res;
    }
    protected bool SendNotifyTakeOver(string ToEmpId)
    {
        string ReplyMess;
        int LoginId = Common.CastAsInt32(Session["loginid"].ToString());
        DataTable dtLoginMail = Common.Execute_Procedures_Select_ByQueryCMS("select FirstName+ ' ' + LastName as UserName,email from userlogin where loginid=" + LoginId.ToString());
        DataTable dtHandOver = Common.Execute_Procedures_Select_ByQueryCMS("select FirstName+ ' ' + LastName as UserName,email from userlogin where loginid in (select userid from Hr_PersonalDetails where empid=" + ToEmpId + ")");

        string MailFrom = dtLoginMail.Rows[0]["email"].ToString();
        string LoginName = dtLoginMail.Rows[0]["UserName"].ToString();

        string[] ToAdds = { dtHandOver.Rows[0]["email"].ToString() };
        string ToName = dtHandOver.Rows[0]["UserName"].ToString();

        string[] CCAdds = { };

        string[] BCCAdds = { };

        string Subject = "Takeover Note";

        string BasePath = Request.Url.AbsoluteUri;
        BasePath = BasePath.Substring(0, BasePath.LastIndexOf("/"));        

        StringBuilder sb = new StringBuilder();
        sb.Append("==================================Takeover Note===================================" + "</br>");
        sb.Append("Dear " + ToName + "," + "</br></br>");
        sb.Append("My handover notes are updated in Fleet Notes, which can be accessed by below link. Please confirm by accepting it." + "</br></br>");
        //sb.Append("<a href='" + LinkToAcceptReject + "'>Click here to Accept / Reject </a>" + "</br></br>");
        sb.Append("Thanks" + "</br></br>");
        sb.Append(LoginName + "</br>");
        sb.Append("===================================================================================" + "</br>");

        string MailBody = sb.ToString();
        bool Res = SendEmail.SendeMail(LoginId, MailFrom.ToString(), MailFrom, ToAdds, CCAdds, BCCAdds, Subject, MailBody, out ReplyMess, "");
        return Res;
    }
}