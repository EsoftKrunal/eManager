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
using System.Data.SqlClient;
using System.IO;

public partial class Transactions_InspectionObservation1 : System.Web.UI.Page
{
    public int Login_Id
    {
        get
        {
            return int.Parse(ViewState["_Login_Id"].ToString());
        }
        set
        {
            ViewState["_Login_Id"] = value;
        }
    }
    public int DefTableID
    {
        get
        {
            return Common.CastAsInt32(ViewState["_DefTableID"].ToString());
        }
        set
        {
            ViewState["_DefTableID"] = value;
        }
    }
    public int Inspection_Id
    {
        get
        {
            return int.Parse(ViewState["_Inspection_Id"].ToString());
        }
        set
        {
            ViewState["_Inspection_Id"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        //--------------------------------- PAGE ACCESS AUTHORITY ----------------------------------------------------
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 1053);
        if (chpageauth <= 0)
            Response.Redirect("blank.aspx");
        //------------------------------------------------------------------------------------------------------------
        //this.Form.DefaultButton = this.Button1.UniqueID.ToString();
        lblMsgDeficiency.Text = "";
        lblmessage.Text = "";
        if ((Session["loginid"] == null) || (Session["UserName"] == null))
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Login.aspx';", true);
        }
        else
        {
            Login_Id = Convert.ToInt32(Session["loginid"].ToString());
        }
        if ((Session["Insp_Id"] == null))
        {
            Session.Add("PgFlag", 1); Response.Redirect("InspectionSearch.aspx");
        }
        else
        {
            Inspection_Id = int.Parse(Session["Insp_Id"].ToString());
        }
        if (!IsPostBack)
        {
            DefTableID = 0;
            Show_Header_Record(Inspection_Id);
            Show_Detail_Record(Inspection_Id);
        }
    }

    //-----------  Events
    protected void btnDeficiencyPopup_OnClick(object sender, EventArgs e)
    {
        dvAddUpdateDeficiency.Visible = true;
        txtDeficiency.Text = "";
        txtCorrAction.Text = "";
        txtlastdone.Text = "";
        txtQno.Text = "";
    }
    protected void btnCloseDeficiency_OnClick(object sender, EventArgs e)
    {
        dvAddUpdateDeficiency.Visible = false;
        DefTableID = 0;
        Show_Detail_Record(Inspection_Id);
    }

    protected void btnSaveDeficiency_OnClick(object sender, EventArgs e)
    {
        if (txtDeficiency.Text.Trim() == "")
        {
            lblMsgDeficiency.Text = "Please enter deficiency."; return;
        }
        if (txtCorrAction.Text.Trim() == "")
        {
            lblMsgDeficiency.Text = "Please enter corrective action."; return;
        }
        if (txtTCD.Text.Trim() == "")
        {
            lblMsgDeficiency.Text = "Please enter target closure date."; return;
        }

        Common.Set_Procedures("dbo.sp_IU_t_ObservationsNew");
        Common.Set_ParameterLength(9);
        Common.Set_Parameters(
                new MyParameter("@TABLEID", DefTableID),
                new MyParameter("@INSPDUE_ID", Inspection_Id.ToString()),
                new MyParameter("@DEFICIENCY", txtDeficiency.Text.Trim()),
                new MyParameter("@CORRACTIONS", txtCorrAction.Text.Trim()),
                new MyParameter("@TCLDATE", txtTCD.Text.Trim()),
                new MyParameter("@Responsibility", ""),
                new MyParameter("@CreatedBy", Login_Id),
                new MyParameter("@QuestionNo", txtQno.Text.Trim()),
                new MyParameter("@MasterComments", hfdMC.Value.Trim())
           );
        Boolean res;
        DataSet ds = new DataSet();
        res=Common.Execute_Procedures_IUD(ds);
        if (res)
        {
            lblMsgDeficiency.Text = "Record saved successfully.";
        }
        else
        {
            lblMsgDeficiency.Text = Common.ErrMsg;
        }
    }
    protected void btnEditDeficiency_OnClick(object sender, EventArgs e)
    {
        ImageButton imgEdit = (ImageButton)sender;
        DefTableID = Common.CastAsInt32(imgEdit.CommandArgument);
        dvAddUpdateDeficiency.Visible = true;
        Show_DeficiencyRecord();
    }
    
    // Closure
    protected void btnClosurePopup_OnClick(object sender, EventArgs e)
    {
        ImageButton imgEdit = (ImageButton)sender;
        DefTableID = Common.CastAsInt32(imgEdit.CommandArgument);
        dvClosure.Visible = true;

        btnSaveClosure.Visible = true;
        txt_ClosedBy.Text = "";
        txt_ClosedDate.Text = "";
        txt_ClosedOn.Text = "";
        txt_ClosedRemarks.Text = "";
        for (int j = 0; j <= rdbflaws.Items.Count - 1; j++)
        {
            rdbflaws.Items[j].Selected = false;
        }
        a_file.HRef = "";
    }
    protected void btnSaveClosure_Click(object sender, EventArgs e)
    {
        try
        {
            string strflaws = "";
            if (txt_ClosedDate.Text == "")
            {
                lblmessage.Text = "Please enter Closed Date.";
                txt_ClosedDate.Focus();
                return;
            }
            if (txt_ClosedRemarks.Text == "")
            {
                lblmessage.Text = "Please enter Closed Remarks.";
                txt_ClosedRemarks.Focus();
                return;
            }
            
            for (int cnt = 0; cnt <= rdbflaws.Items.Count - 1; cnt++)
            {
                if (rdbflaws.Items[cnt].Selected == true)
                {
                    if (strflaws == "")
                    {
                        strflaws = rdbflaws.Items[cnt].Value;
                    }
                    else
                    {
                        strflaws = strflaws + "," + rdbflaws.Items[cnt].Value;
                    }
                }
            }
            //----------------------------------------------------
            string FileName = "";
            string FileType = "";
            if (fu_ClosureEvidence.PostedFile != null && fu_ClosureEvidence.FileContent.Length > 0)
            {
                HttpPostedFile file1 = fu_ClosureEvidence.PostedFile;
                UtilityManager um = new UtilityManager();
                if (chk_FileExtension(Path.GetExtension(fu_ClosureEvidence.FileName).ToLower()) == true)
                {
                    FileName = "DeficiencyClosure_" + DefTableID.ToString() + Path.GetExtension(fu_ClosureEvidence.FileName).ToLower();
                    FileType = Path.GetExtension(fu_ClosureEvidence.FileName).ToLower();

                    if(File.Exists(Server.MapPath("~/EMANAGERBLOB/Inspection/" + FileName)))
                        File.Delete(Server.MapPath("~/EMANAGERBLOB/Inspection/" + FileName));
                    fu_ClosureEvidence.SaveAs(Server.MapPath("~/EMANAGERBLOB/Inspection/" + FileName));
                }
                else
                {
                    lblmessage.Text = "Invalid File Type. (Valid Files Are .doc, .docx, .xls, .xlsx, .pdf)";
                    fu_ClosureEvidence.Focus();
                    return;
                }
            }
            string sql = "Update dbo.t_ObservationsNew set Closure=1,ClosedBy='" + Session["UserName"].ToString() + "',ClosedOn='" + txt_ClosedDate.Text.Trim() + "',ClosureRemarks='" + txt_ClosedRemarks.Text.Replace("'","''").Trim() + "',flaws='" + strflaws + "',FileType='" + FileType + "' Where TableID=" + DefTableID.ToString() + "";
            DataTable DS = Common.Execute_Procedures_Select_ByQuery(sql);
            lblmessage.Text = " Closed successfully. ";
        }
        catch (Exception ex) { throw ex; }
    }
    protected void btnViewClosure_OnClick(object sender, EventArgs e)
    {
        ImageButton imgEdit = (ImageButton)sender;
        DefTableID = Common.CastAsInt32(imgEdit.CommandArgument);
        dvClosure.Visible = true;        

        btnSaveClosure.Visible = false;
        string sql = "Select Closure,ClosedBy,ClosedOn,ClosureRemarks,Flaws,FileType from dbo.t_ObservationsNew Where TableID=" + DefTableID.ToString() + "";
        DataTable DS = Common.Execute_Procedures_Select_ByQuery(sql);
        if (DS != null)
        {
            txt_ClosedDate.Text = Common.ToDateString(DS.Rows[0]["ClosedOn"].ToString());
            txt_ClosedRemarks.Text = DS.Rows[0]["ClosureRemarks"].ToString().Replace("''","'");
            txt_ClosedBy.Text = DS.Rows[0]["ClosedBy"].ToString();
            txt_ClosedOn.Text = Common.ToDateString( DS.Rows[0]["ClosedOn"].ToString());
            if (DS.Rows[0]["FileType"].ToString().Trim() != "")
                a_file.HRef = "~/EMANAGERBLOB/Inspection/DeficiencyClosure_" + DefTableID + DS.Rows[0]["FileType"].ToString();

            if (DS.Rows[0]["Flaws"].ToString() != "")
            {
                char[] c = { ',' };
                Array a = DS.Rows[0]["Flaws"].ToString().Split(c);
                for (int j = 0; j <= rdbflaws.Items.Count - 1; j++)
                {
                    rdbflaws.Items[j].Selected = false;
                }
                for (int r = 0; r < a.Length; r++)
                {
                    if (a.GetValue(r).ToString() == "People")
                        rdbflaws.Items[0].Selected = true;
                    if (a.GetValue(r).ToString() == "Process")
                        rdbflaws.Items[1].Selected = true;
                    if (a.GetValue(r).ToString() == "Equipment")
                        rdbflaws.Items[2].Selected = true;
                }
            }
            else
                rdbflaws.SelectedIndex = -1;
        }

        DefTableID = 0;

    }
    
    protected void btnCloseClosure_Click(object sender, EventArgs e)
    {
        dvClosure.Visible = false;
        Show_Detail_Record(Inspection_Id);
        DefTableID = 0;
    }
    public bool chk_FileExtension(string str)
    {
        string extension = str;
        switch (extension)
        {
            case ".doc":
                return true;
            case ".docx":
                return true;
            case ".xls":
                return true;
            case ".xlsx":
                return true;
            case ".pdf":
                return true;
            default:
                return false;
                break;
        }
    }
    //protected void btnSaveClosure_OnClick(object sender, EventArgs e)
    //{
    //    if (txtClosureRemarks.Text.Trim() == "")
    //    {
    //        lblMsgDefClosure.Text = "Please enter closure remarks."; return;
    //    }
    //    if (txtClosureDate.Text.Trim() == "")
    //    {
    //        lblMsgDefClosure.Text = "Please enter closure date."; return;
    //    }

    //    string sql = "Update dbo.t_ObservationsNew set Closure=1,ClosedBy='" + Session["UserName"].ToString() + "',ClosedOn='"+txtClosureDate.Text.Trim()+"',ClosureRemarks='"+txtClosureRemarks.Text.Trim()+"' Where TableID="+DefTableID.ToString()+"";
    //    DataTable DS = Common.Execute_Procedures_Select_ByQuery(sql);

    //    lblMsgDefClosure.Text = "Closed successfully.";
    //}
    //protected void btnCloseClosure_OnClick(object sender, EventArgs e)
    //{
    //    dvClosure.Visible = false;
    //    DefTableID = 0;
    //}
    
    

    //-----------  Function
    protected void Show_Header_Record(int InspectionId)
    {
        int intInsGrp=0;        
        try
        {
            DataTable dt1 = Inspection_TravelSchedule.SelectInspectionDetailsByInspId(InspectionId);
            foreach (DataRow dr in dt1.Rows)
            {
                txtinspno.Text = dr["InspectionNo"].ToString();
                txtvessel.Text = dr["VesselName"].ToString();
                txtinspname.Text = dr["Name"].ToString();
                txtlastdone.Text = dr["DoneDt"].ToString();
                txtnextdue.Text = dr["InspectionValidityDt"].ToString();
                txtplannedport.Text = dr["Planned_Port"].ToString();
                txtplanneddate.Text = dr["Plan_Date"].ToString();
                txt_Status.Text = dr["Status"].ToString();
                intInsGrp = int.Parse (dr["InspectionGroup"].ToString());
                ViewState["VersionId"] = dr["VersionId"].ToString();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void Show_Detail_Record(int InspectionId)
    {
        string SQL = "Select Row_Number() over(order by Deficiency) as SrNo, TableId,QuestionNo,Deficiency,CorrActions,TCLDate,Closure=Case When Closure=1 then 'Yes' else 'No' end,(ClosedBy+ '/')ClosedBy,ClosedOn,ClosureRemarks from t_observationsnew Where InspDue_Id=" + Inspection_Id.ToString();
        rptList.DataSource = Budget.getTable(SQL);
        rptList.DataBind(); 
    }
    protected void Show_DeficiencyRecord()
    {
        string SQL = "Select Deficiency,CorrActions,TCLDate,QuestionNo,isnull(MasterComments,'') as MasterComments from t_ObservationsNew Where TableID=" + DefTableID.ToString() + "";
        DataSet DS = Budget.getTable(SQL);
        if (DS != null)
        {
            DataRow Dr = DS.Tables[0].Rows[0];
            txtDeficiency.Text = Dr["Deficiency"].ToString();
            txtCorrAction.Text = Dr["CorrActions"].ToString();
            txtTCD.Text = Common.ToDateString( Dr["TCLDate"].ToString());
            txtQno.Text = Dr["QuestionNo"].ToString();
            hfdMC.Value = Dr["MasterComments"].ToString();
        }
        
    }

    // print
    protected void btnPrint_OnClick(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "ac", "window.open('../Reports/ManualObservations.aspx?InspId=" + Inspection_Id + "');", true);
    }
    
}