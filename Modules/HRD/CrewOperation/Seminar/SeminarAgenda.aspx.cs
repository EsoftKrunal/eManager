using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Xml;
using System.IO;
using System.Text;
using Ionic.Zip;
using System.Linq;

public partial class SeminarAgenda : System.Web.UI.Page
{
    public int SeminarId
    {
        get
        { return Common.CastAsInt32(ViewState["SeminarId"]); }
        set { ViewState["SeminarId"] = value; }
    }
    public int TableId
    {
        get
        { return Common.CastAsInt32(ViewState["TableId"]); }
        set { ViewState["TableId"] = value; }
    }
    public int UserId
    {
        get
        { return Common.CastAsInt32(ViewState["UserId"]); }
        set { ViewState["UserId"] = value; }
    }
    public string SelGuid
    {
        get
        { return ViewState["SelGuid"].ToString(); }
        set { ViewState["SelGuid"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        lblMessage.Text = "";
        //------------------------------------
        //ProjectCommon.SessionCheck_New();
        //------------------------------------
        if (!Page.IsPostBack)
        {
            SeminarId = Common.CastAsInt32(Request.QueryString["K"]);
            UserId = Common.CastAsInt32(Session["LoginId"]);



            LoadEmployees();
            ShowRecord();
            BindAgenda();
            //ShowInviteDetails();
        }
    }
    protected void lnkEditSeminar_Click(object sender, EventArgs e)
    {

    }
    public void ShowRecord()
    {
        DataTable DT = Common.Execute_Procedures_Select_ByQuery("select * from DBO.vw_tbl_Seminar where SeminarId=" + SeminarId);
        if (DT.Rows.Count > 0)
        {
            lblCategoryName.Text = DT.Rows[0]["SeminarCatName"].ToString();
            lblOfficeName.Text = DT.Rows[0]["OfficeName"].ToString();
            //lblStartTime.Text = lblDuration.Text = Convert.ToDateTime(DT.Rows[0]["StartDate"]).ToString("hh:mm tt");
            if (Common.ToDateString(DT.Rows[0]["StartDate"]) == Common.ToDateString(DT.Rows[0]["EndDate"]))
                lblDuration.Text = String.Format("{0:ddddd, dd MMM yyyy}", DT.Rows[0]["StartDate"]);
            else
                lblDuration.Text = String.Format("{0:ddddd, dd MMM yyyy}", DT.Rows[0]["StartDate"]) + " to " + String.Format("{0:ddddd, dd MMM yyyy}", DT.Rows[0]["EndDate"]);

            lblEventLocation.Text = DT.Rows[0]["Location"].ToString();
            lblRemarks.Text = DT.Rows[0]["Topic"].ToString();


            lblContactPerson.Text = DT.Rows[0]["ContactPerson"].ToString();
            lblContactNumber.Text = DT.Rows[0]["ContactNumber"].ToString();
            lblContactEmail.Text = DT.Rows[0]["ContactEmail"].ToString();

            
        }
        ShowAgendaList();
    }

    protected void btnAddAgenda_Click(object sender, EventArgs e)
    {
        TableId = 0;
        txtTopic1.Text = "";
        txtTime1.Text = "";
        txtTime2.Text = "";
        chkPresenters.ClearSelection();
        SetAgendaStartDate();
        dvFrame.Visible = true;
    }
    protected void btEditAgenda_Click(object sender, EventArgs e)
    {
        //TableId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        TableId = Common.CastAsInt32(hfdAgendaID.Value);
        chkPresenters.ClearSelection();
        if (TableId > 0)
        {
            DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT PresenterId  as Presenters,* FROM DBO.tbl_SeminarAgenda WHERE TABLEID=" + TableId);
            if (dt.Rows.Count > 0)
            {
                txtTopic1.Text = dt.Rows[0]["Agenda"].ToString();
                txtTime1.Text = Convert.ToDateTime(dt.Rows[0]["StartTime"]).ToString("dd-MMM-yyyy HH:mm");
                txtTime2.Text = Convert.ToDateTime(dt.Rows[0]["EndTime"]).ToString("dd-MMM-yyyy HH:mm");

                string[] Presenters = dt.Rows[0]["Presenters"].ToString().Split(',');
                //ddlPresenter.SelectedValue = dt.Rows[0]["PresenterId"].ToString();

                foreach (ListItem li in chkPresenters.Items)
                {
                    //if ((Presenters + ",").Contains(li.Value + ","))
                    //    li.Selected = true;
                    var chk = Presenters.Where(a => a == li.Value);
                    if (chk.Count() > 0)
                        li.Selected = true;

                }


                dvFrame.Visible = true;
            }
        }
    }
    protected void btnDeleteAgenda_Click(object sender, EventArgs e)
    {
        //TableId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        TableId = Common.CastAsInt32(hfdAgendaID.Value);
        if (TableId > 0)
        {
            Common.Execute_Procedures_Select_ByQuery("DELETE FROM DBO.tbl_SeminarAgenda WHERE TABLEID=" + TableId);
            BindAgenda();
            TableId = 0;
        }
    }
    public void BindAgenda()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("select ROW_NUMBER() OVER(ORDER BY TABLEID DESC) AS SNO,case when isnull(AttachmentName,'')='' then 'none' else 'block' end as download,* from DBO.tbl_SeminarAgenda WHERE SEMINARID=" + SeminarId);
        rptAgenda.DataSource = dt;
        rptAgenda.DataBind();
    }
    protected void btDownloadAttachment_Click(object sender, EventArgs e)
    {
        //TableId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        TableId = Common.CastAsInt32(hfdAgendaID.Value);
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.tbl_SeminarAgenda WHERE TABLEID=" + TableId);
        if (TableId > 0)
        {
            string filename = dt.Rows[0]["AttachmentName"].ToString();
            byte[] filedata = (byte[])dt.Rows[0]["Attachment"];
            //--------------
            Response.Clear();
            Response.AppendHeader("content-disposition", "attachment; filename=" + filename);
            Response.ContentType = "application/octet-stream";
            Response.BinaryWrite(filedata);
            Response.Flush();
            Response.End();
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (chkPresenters.SelectedIndex == -1)
        {
            ShowMessage("Please select presenter.", true);
            chkPresenters.Focus();
            return;
        }

        if (txtTopic1.Text.Trim() == "")
        {
            ShowMessage("Please enter agenda text.", true);
            txtTopic1.Focus();
            return;
        }

        DateTime d1 = new DateTime();
        DateTime d2 = new DateTime();

        

        if (!(DateTime.TryParse(txtTime1.Text, out d1)))
        {

            ShowMessage("Please enter valid start time.", true);
            txtTime1.Focus();
            return;
        }


        if (!(DateTime.TryParse(txtTime2.Text, out d2)))
        {
            ShowMessage("Please enter valid end time.", true);
            txtTime2.Focus();
            return;

        }
        if (d1 > d2)
        {
            ShowMessage("Start time should be less than end time.", true);
            txtTime2.Focus();
            return;
        }

        if (TableId == 0)
        {
            string sql = "select S.StartDate,S.EndDate, A.StartTime, A.EndTime  from DBO.tbl_Seminar S left join DBO.tbl_SeminarAgenda A on S.SeminarId = A.SeminarId where S.SEMINARID = " + SeminarId + " order by StartTime desc";
            DataTable DT = Common.Execute_Procedures_Select_ByQuery(sql);
            if (DT.Rows.Count > 0)
            {
                DateTime S_StatDate = new DateTime();
                DateTime S_EndDate = new DateTime();
                DateTime A_StatDate = new DateTime();
                DateTime A_EndDate = new DateTime();


                DateTime.TryParse(DT.Rows[0][0].ToString(), out S_StatDate);
                DateTime.TryParse(DT.Rows[0][1].ToString(), out S_EndDate);



                //DateTime.TryParse(DT.Rows[0][2].ToString(), out A_StatDate);
                //DateTime.TryParse(DT.Rows[0][3].ToString(), out A_EndDate);

                if (d1 < S_StatDate || d1 >= S_EndDate.AddDays(1))
                {
                    ShowMessage("Invalid start date range", true);
                    txtTime1.Focus();
                    return;
                }

                if (Convert.ToString(DT.Rows[0][3]) != "")
                {
                    DateTime.TryParse(DT.Rows[0][3].ToString(), out A_EndDate);
                    if (d1 < A_EndDate)
                    {
                        ShowMessage("Start date should be greater than Last seminar end date", true);
                        txtTime1.Focus();
                        return;
                    }
                }


                if (d2 < S_StatDate || d2 >= S_EndDate.AddDays(1))
                {
                    ShowMessage("Invalid end date range", true);
                    txtTime2.Focus();
                    return;
                }

            }
        }
        

        string filename = "";
        byte[] filedata = { };

        if (flpUpload.HasFile)
        {
            filename = Path.GetFileName(flpUpload.FileName);
            filedata = (byte[])flpUpload.FileBytes;
        }

        string selectedPresenters = "";
        foreach (ListItem li in chkPresenters.Items)
        {
            if (li.Selected)
                selectedPresenters = selectedPresenters + "," + li.Value;
        }
        if (selectedPresenters != "")
            selectedPresenters = selectedPresenters.Substring(1);

        Common.Set_Procedures("DBO.InsertUpdateSeminarTopic");
        Common.Set_ParameterLength(9);
        Common.Set_Parameters(
            new MyParameter("@TableId", TableId),
            new MyParameter("@SeminarId", SeminarId),
            new MyParameter("@PresenterId", selectedPresenters),
            new MyParameter("@Agenda", txtTopic1.Text.Trim()),
            new MyParameter("@StartTime", d1),
            new MyParameter("@EndTime", d2),
            new MyParameter("@AttachmentName", filename),
            new MyParameter("@Attachment", filedata),
            new MyParameter("@ModifiedBy", UserId));

        DataSet ds = new DataSet();
        if (Common.Execute_Procedures_IUD(ds))
        {
            TableId = Common.CastAsInt32(ds.Tables[0].Rows[0][0]);
            BindAgenda();
            ShowRecord();
            ShowMessage("Record saved sucessfully.", false);
        }
        else
        {
            ShowMessage("Unable to save record.", true);
        }

    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        divSeminar.Visible = false;
        ShowRecord();
    }
    protected void btnCloseAgenda_Click(object sender, EventArgs e)
    {
        dvFrame.Visible = false;
    }
    public void ShowAgendaList()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT ROW_NUMBER() OVER(ORDER BY PRESENTERNAME) AS SNO,* FROM DBO.vw_tbl_SeminarPresenters WHERE SEMINARID=" + SeminarId + " order by PRESENTERNAME");
        //rptPresenters.DataSource = dt;
        //rptPresenters.DataBind();
    }


    public void LoadEmployees()
    {
        //DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT USERID,FIRSTNAME + ' ' + MIDDLENAME + ' ' + FAMILYNAME AS EMPNAME FROM DBO.Hr_PersonalDetails WHERE USERID IN (SELECT PresenterId FROM DBO.tbl_SeminarPresenters where SeminarId=" + SeminarId + ") ORDER BY EMPNAME");
        //ddlPresenter.DataSource = dt;
        //ddlPresenter.DataTextField = "EMPNAME";
        //ddlPresenter.DataValueField = "USERID";
        //ddlPresenter.DataBind();
        //ddlPresenter.Items.Insert(0, new ListItem("----- Select -----", "0"));

        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT USERID,FIRSTNAME + ' ' + MIDDLENAME + ' ' + FAMILYNAME AS EMPNAME FROM DBO.Hr_PersonalDetails WHERE POSITION IN (select P.positionid from DBO.position P where isinspector=1) AND DRC IS NULL and USERID is NOT NULL ORDER BY EMPNAME");
        chkPresenters.DataSource = dt;
        chkPresenters.DataTextField = "EMPNAME";
        chkPresenters.DataValueField = "USERID";
        chkPresenters.DataBind();
    }
    public void ShowMessage(string Message, bool Error)
    {
        lblMessage.Text = Message;
        lblMessage.ForeColor = (Error) ? System.Drawing.Color.Red : System.Drawing.Color.Green;

        //lblMessage1.Text = Message;
        //lblMessage1.ForeColor = (Error) ? System.Drawing.Color.Red : System.Drawing.Color.Green;

    }
    //---------
    public string GetAgendaDate(object strat, object end)
    {
        if (Common.ToDateString(strat) == Common.ToDateString(end))
        {
            //return Convert.ToDateTime(strat).ToString("hh:mm tt");
            return Convert.ToDateTime(strat).ToString("ddddd, dd MMM hh:mm tt") + " to " + Convert.ToDateTime(end).ToString("hh:mm tt");
        }
        else
            return Convert.ToDateTime(strat).ToString("ddddd, dd MMM hh:mm tt") + " to " + Convert.ToDateTime(end).ToString("ddddd, dd MMM hh:mm tt");
    }
    public string GetAgendaTime(object strat, object end)
    {

        return Convert.ToDateTime(strat).ToShortTimeString() + " To " + Convert.ToDateTime(end).ToShortTimeString();


    }
    public string ShowPresenter(string IDs)
    {
        StringBuilder ret=new StringBuilder("");
        string sql = " select pd.FIRSTNAME + ' ' + pd.MIDDLENAME + ' ' + pd.FAMILYNAME AS EMPNAME "+
                     "   from dbo.tbl_SeminarPresenters p inner join DBO.Hr_PersonalDetails pd on pd.UserId = p.PresenterId " +
                     "   where AgendaId = "+ ID + " and SeminarId = "+SeminarId+" ";

        sql = " select pd.FIRSTNAME + ' ' + pd.MIDDLENAME + ' ' + pd.FAMILYNAME AS EMPNAME  " +
             "   from dbo.CSVtoTable('"+ IDs + "',',') p" +
             "   inner join DBO.Hr_PersonalDetails pd on pd.UserId = p.result order by pd.FIRSTNAME + ' ' + pd.MIDDLENAME + ' ' + pd.FAMILYNAME ";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
        foreach (DataRow dr in dt.Rows)
        {
            ret.Append("<div class='presenter' ><i class='fa fa-microphone'></i>&nbsp;");
            ret.Append(dr["EMPNAME"].ToString());
            ret.Append("</div>");
        }
        return ret.ToString();
    }
    public void SetAgendaStartDate()
    {
        StringBuilder ret = new StringBuilder("");
        string sql = " select top 1 rw,tt from  " +
        "    ( " +
        "        select rw,tt from " +
        "        ( " +
        "            select top 1  1 As rw ,EndTime as tt   from DBO.tbl_SeminarAgenda WHERE SEMINARID = " + SeminarId+" order by StartTime desc " +
        "        )t " +
        "      union " +
        "       select  top 1  2 As rw,StartDate   as tt from DBO.tbl_Seminar where SEMINARID = " + SeminarId + " " +
        "  ) tbl ORDER BY TT DESC";

      
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);

        if (dt.Rows.Count > 0)
        {
            txtTime1.Text = Convert.ToDateTime(dt.Rows[0]["tt"]).ToString("dd-MMM-yyyy HH:mm");
            txtTime2.Text = Convert.ToDateTime(dt.Rows[0]["tt"]).ToString("dd-MMM-yyyy HH:mm");
        }
    }

    

    protected void btnPost_Click(object sender, EventArgs e)
    {
        if (hfdActionType.Value == "D")
        {
            btnDeleteAgenda_Click(sender, e);
        }
        else if (hfdActionType.Value == "E")
        {
            btEditAgenda_Click(sender, e);
        }
        else if (hfdActionType.Value == "DW")
        {
            btDownloadAttachment_Click(sender, e);
        }

        
    }
    protected void lnkEditSeminarMaster_OnClick(object sender, EventArgs e)
    {
        divSeminar.Visible = true;
        frame1.Attributes.Add("Src", "AddEditSeminar.aspx?SeminarId=" + SeminarId);
    }
    
}
