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
using System.Data.OleDb;
using System.IO;

public partial class Transactions_InspectionTravelSchedule : System.Web.UI.Page
{
    Authority Auth;
    string strInsp_Status = "";

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
    public int Master
    {
        get
        {
            return int.Parse(ViewState["_Master"].ToString());
        }
        set
        {
            ViewState["_Master"] = value;
        }
    }
    public int ChiefOfficer
    {
        get
        {
            return int.Parse(ViewState["_ChiefOfficer"].ToString());
        }
        set
        {
            ViewState["_ChiefOfficer"] = value;
        }
    }
    public int SecondOffice
    {
        get
        {
            return int.Parse(ViewState["_SecondOffice"].ToString());
        }
        set
        {
            ViewState["_SecondOffice"] = value;
        }
    }
    public int ChiefEngineer
    {
        get
        {
            return int.Parse(ViewState["_ChiefEngineer"].ToString());
        }
        set
        {
            ViewState["_ChiefEngineer"] = value;
        }
    }
    public int AssistantEngineer
    {
        get
        {
            return int.Parse(ViewState["_AssistantEngineer"].ToString());
        }
        set
        {
            ViewState["_AssistantEngineer"] = value;
        }
    }
    protected void BindObservations()
    {
       
       
        if (Session["Insp_Id"] != null)
        {
            string strInspNo = "";
            DataTable dt = Inspection_Planning.AdInspectionPlanning(int.Parse(Session["Insp_Id"].ToString()), 0, "", 0, DateTime.Now, "", 0, 0, 0, 0, 0, "SELECT", "", "", "", "", "", "", "", 0);
            if (dt.Rows.Count > 0)
            {
                strInspNo = dt.Rows[0]["InspectionNo"].ToString();
            }
            rpt_Observations.DataSource = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM VIMS_Ins_Observations WHERE ltrim(rtrim(InspectionNo))='" + strInspNo.Trim() + "' order by Qno");
            rpt_Observations.DataBind();
        }
       
    }

    protected void txtQuestionNo_Changed(object sender, EventArgs e)
    {
        int InspectionId = int.Parse(Session["Insp_Id"].ToString());
        string Qno = txtQno.Text.Trim();
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(" exec [dbo].[PR_GetQuestion] '" + Qno + "'," + InspectionId.ToString());
        if (dt.Rows.Count > 0)
        {
            txtQuestion.Text = dt.Rows[0]["Question"].ToString();
            txtQId.Text = dt.Rows[0]["QuestionId"].ToString();
            btnImportObs.Enabled = true;
        }
        else
        {
            txtQId.Text = "0";
            txtQuestion.Text = "";
            btnImportObs.Enabled = false;
        }
    }
    protected void btnImportObs_Click(object sender, EventArgs e)
    {

        string strInspNo = "";
        int inspectionid = int.Parse(Session["Insp_Id"].ToString());
        int inspectiongrp =0;

        DataTable Dt_insgrp = Common.Execute_Procedures_Select_ByQuery("select (select inspectiongroup from dbo.m_Inspection where id=inspectionid),InspectionNo from dbo.t_inspectiondue where id=" + inspectionid.ToString());
        strInspNo = Dt_insgrp.Rows[0]["InspectionNo"].ToString();

        if (Common.CastAsInt32(Dt_insgrp.Rows[0][0]) == 3) // mtm inspections
        {
            inspectiongrp = Common.CastAsInt32(Dt_insgrp.Rows[0][0]);
        }
        if(Common.CastAsInt32(txtQId.Text)<=0)
        {
            lblMsgAppRej.Text="Question# is invalid.";
            return ;
        }
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("select * from T_OBSERVATIONS WHERE INSPECTIONDUEID=" + inspectionid + " AND QUESTIONID=" + txtQId.Text);
        if (dt.Rows.Count > 0)
        {
            lblMsgAppRej.Text = "Question# already exists in observations.";
            return;
        }
        else
        {
            
            if (inspectiongrp == 3) // mtm inspections ( t_observationsnew )
            {
                Common.Set_Procedures("dbo.sp_IU_t_ObservationsNew");
                Common.Set_ParameterLength(9);
                Common.Set_Parameters(
                        new MyParameter("@TABLEID", 0),
                        new MyParameter("@INSPDUE_ID", inspectionid),
                        new MyParameter("@DEFICIENCY", txtDeficiency.Text.Trim()),
                        new MyParameter("@CORRACTIONS", txtC.Text.Trim()),
                        new MyParameter("@TCLDATE", DBNull.Value),
                        new MyParameter("@Responsibility", ""),
                        new MyParameter("@CreatedBy", Login_Id),
                        new MyParameter("@QuestionNo", ""),
                        new MyParameter("@MasterComments", txtMC.Text.Trim())
                    );
                DataSet ds = new DataSet();
                bool res = Common.Execute_Procedures_IUD(ds);
                if (res)
                {
                    Common.Execute_Procedures_Select_ByQuery("DELETE FROM VIMS_Ins_Observations WHERE LTRIM(RTRIM(InspectionNo))='" + strInspNo.Trim() + "' AND LTRIM(RTRIM(QNO))='" + txtQno.Text.Trim() + "'");
                    BindObservations();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "alert('Imported Successfully');", true);
                    dbViewObservations.Visible = false; 
                }
                else
                {
                    lblMsgAppRej.Text = "Unble to import." + Common.ErrMsg;
                }
            }
            else 
            {
                try
                {
                    Common.Execute_Procedures_Select_ByQuery("INSERT INTO t_Observations (QuestionId,InspectionDueId,SuperIntendentId,Deficiency,Comment,HighRisk,NCR,CreatedBy,ModifiedBy,ObservationStatus,IsObservation,CorrectiveActions,MasterComments) " +
                                                                  "VALUES(" + txtQId.Text + "," + inspectionid.ToString() + "," + Login_Id.ToString() + ",'" + txtDeficiency.Text.Trim().Replace("'", "`") + "','',0,0," + Login_Id + ",NULL,'NA',0,'" + txtC.Text.Trim().Replace("'", "`") + "','" + txtMC.Text.Trim().Replace("'", "`") + "')");
                    Common.Execute_Procedures_Select_ByQuery("DELETE FROM VIMS_Ins_Observations WHERE LTRIM(RTRIM(InspectionNo))='" + strInspNo.Trim() + "' AND LTRIM(RTRIM(QNO))='" + txtQno.Text.Trim() + "'");
                    BindObservations();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "alert('Imported Successfully');", true);
                    dbViewObservations.Visible = false;

                }
                catch ( Exception ex)
                {
                    lblMsgAppRej.Text = "Unble to import." + ex.Message;
                }

            }

        }

    }
    protected void btnCloseOb_Click(object sender, EventArgs e)
    {
        dbViewObservations.Visible = false;
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        string strInspNo = "";
        btnImportObs.Enabled = false;
        int InspectionId=int.Parse(Session["Insp_Id"].ToString());

        DataTable dt = Inspection_Planning.AdInspectionPlanning(int.Parse(Session["Insp_Id"].ToString()), 0, "", 0, DateTime.Now, "", 0, 0, 0, 0, 0, "SELECT", "", "", "", "", "", "", "", 0);
        if (dt.Rows.Count > 0)
        {
            strInspNo = dt.Rows[0]["InspectionNo"].ToString();
        }
        string Qno = ((LinkButton)sender).CommandArgument;
        dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM VIMS_Ins_Observations WHERE [InspectionNo]='" + strInspNo + "' AND QNO='" + Qno + "'");
        if (dt.Rows.Count > 0)
        {
            txtQId.Text = dt.Rows[0]["QuestionId"].ToString();
            txtQno.Text = dt.Rows[0]["QNO"].ToString();
            txtDeficiency.Text = dt.Rows[0]["Deficiency"].ToString();
            txtMC.Text = dt.Rows[0]["MasterComments"].ToString();
            txtC.Text = dt.Rows[0]["CorrectiveActions"].ToString();
            hfdId.Value = dt.Rows[0]["QNO"].ToString();

            dbViewObservations.Visible = true;

            dt = Common.Execute_Procedures_Select_ByQuery(" exec [dbo].[PR_GetQuestion] '" + Qno + "'," + InspectionId.ToString());
            if (dt.Rows.Count > 0)
            {
                txtQuestion.Text = dt.Rows[0]["Question"].ToString();
                txtQId.Text = dt.Rows[0]["QuestionId"].ToString();
                btnImportObs.Enabled = true;
            }
            else
            {
                txtQId.Text = "0";
                txtQuestion.Text = "";
                btnImportObs.Enabled = false;
            }
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsgMain.Text = "";
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        //--------------------------------- PAGE ACCESS AUTHORITY ----------------------------------------------------
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 1053);
        if (chpageauth <= 0)
            Response.Redirect("blank.aspx");
        //------------------------------------------------------------------------------------------------------------
        this.Form.DefaultButton = this.btnSave.UniqueID.ToString();
        lblmessage.Text = "";
        if ((Session["loginid"] == null) || (Session["UserName"] == null))
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Login.aspx';", true);
        }
        else
        {
            Login_Id = Convert.ToInt32(Session["loginid"].ToString());
        }
        if (Session["loginid"] != null)
        {
            ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 7);
            OBJ.Invoke();
            Session["Authority"] = OBJ.Authority;
            Auth = OBJ.Authority;
        }

        if (!Page.IsPostBack)
        {
            BindObservations();
            EnableDisabledCrewList(false);
            try
            {
                if (Session["Insp_Id"] != null)
                {
                    Inspection_Id = int.Parse(Session["Insp_Id"].ToString());
                }
                
            }
            catch
            {
                Session.Add("PgFlag", 1);
                Response.Redirect("InspectionSearch.aspx");
                return;
            }
            Show_ExecutionDetails(Inspection_Id);

            btn_UpdateCrew.Enabled = false;
            btnSave.Enabled = false;

            if (Session["Insp_Id"] != null)
            {
                //--------------------------
                DataTable dt88 = Inspection_Planning.CheckInspectionStatus(int.Parse(Session["Insp_Id"].ToString()));
                if (dt88.Rows.Count > 0)
                {
                    strInsp_Status = dt88.Rows[0]["Status"].ToString();
                }
                if ((strInsp_Status == "Planned") || (strInsp_Status == "Executed") || (strInsp_Status == "Observation"))
                {
                    btnSaveOb.Enabled = true;
                    //----------------------
                    btnSave.Enabled = true;
                    btn_UpdateCrew.Enabled = (txtdonedt.Text.Trim() != "");
                }
                else
                {
                    btnSaveOb.Enabled = false;
                    //----------------------
                    btnSave.Enabled = false;
                    btn_UpdateCrew.Enabled = false;
                }

                //--------------------------
                string strInspType = "";
                DataTable dt56 = Inspection_Observation.CheckInspType(int.Parse(Session["Insp_Id"].ToString()));
                if (dt56.Rows.Count > 0)
                {
                    strInspType = dt56.Rows[0]["InspectionType"].ToString();
                }
                if (strInspType == "External")
                    txtinspector.ReadOnly = false;
                else
                    txtinspector.ReadOnly = true;
                //--------------------------

                //--------------------------
                string SQL123 = "SELECT Status FROM t_InspectionDue WHERE Id=" + Inspection_Id;
                DataTable D1 = Budget.getTable(SQL123).Tables[0];
                if (D1.Rows[0][0].ToString().Trim() == "Observation")
                {
                    btnSaveOb.Enabled = false;
                }
            }
            if (("" + Request.QueryString["Message"]) != "")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "fsd", "alert('" + "" + Request.QueryString["Message"] + "');", true);
        }
    }
    protected void Show_ExecutionDetails(int intInspectionId)
    {
        DataTable dt1 = Inspection_TravelSchedule.SelectInspectionDetailsByInspId(intInspectionId);
        if (dt1.Rows.Count > 0)
        {
            foreach (DataRow dr in dt1.Rows)
            {
                txtinspector.Text = dr["InspectorName"].ToString();
                txtmaster.Text = dr["MasterName"].ToString();
                txtchiefofficer.Text = dr["ChiefOfficerName"].ToString();
                txtsecofficer.Text = dr["SecondOfficeName"].ToString();
                txtchiefengg.Text = dr["ChiefEngineerName"].ToString();
                txtfirstassistant.Text = dr["FirstAssistantEnggName"].ToString();
                DataTable dt3 = Inspection_Planning.AddInspectors(0, int.Parse(Session["Insp_Id"].ToString()), 0, 0, DateTime.Now, 0, 0, DateTime.Now, "", 0, 0, "SELECT");
                txtmtmsupt.Text = "";
                txtmtmsupt.ReadOnly = true;
                foreach (DataRow Drchk in dt3.Rows)
                {
                    if (Drchk["Attending"].ToString() == "True")
                    {
                        txtmtmsupt.Text = Drchk["Name"].ToString();
                        break;
                    }
                    else if (Drchk["Attending"].ToString() == "False")
                    {
                        txtmtmsupt.Text = Drchk["Name"].ToString();
                    }
                }
                HiddenField_MTMSupt.Value = txtmtmsupt.Text;
                if ((strInsp_Status != "Planned") && (strInsp_Status != "Executed"))
                {
                    txtdonedt.Text = dr["DoneDt"].ToString();
                }
                if ((strInsp_Status != "Planned") && (strInsp_Status != "Executed"))
                {
                    txtportdone.Text = dr["PortDone"].ToString();
                }
                txtresponseduedt.Text = dr["Responsedt"].ToString();
                if (txtresponseduedt.Text.Trim().ToLower() == "01-jan-1900")
                {
                    txtresponseduedt.Text = "";
                }
                txtstartdate.Text = dr["StartDate1"].ToString();
                if (dr["Master"].ToString() != "")
                    Master = int.Parse(dr["Master"].ToString());
                if (dr["ChiefEngineer"].ToString() != "")
                    ChiefEngineer = int.Parse(dr["ChiefEngineer"].ToString());
                if (dr["AssistantEngineer"].ToString() != "")
                    AssistantEngineer = int.Parse(dr["AssistantEngineer"].ToString());
                if (dr["ChiefOfficer"].ToString() != "")
                    ChiefOfficer = int.Parse(dr["ChiefOfficer"].ToString());
                if (dr["SecondOffice"].ToString() != "")
                    SecondOffice = int.Parse(dr["SecondOffice"].ToString());
                txtinspector.Text  = dr["Inspector"].ToString();


                int InsGrpid = Common.CastAsInt32(dr["InspectionGroup"]);
                
                txtresponseduedt.Visible = (InsGrpid == 1 || InsGrpid == 2);
                lblrespdue.Visible = txtresponseduedt.Visible;
                ImageButton1.Visible = txtresponseduedt.Visible;
            }
        }

    }
    protected void Show_CrewDetails(int intInspectionId)
    {
        DataTable dt1 = Inspection_TravelSchedule.SelectInspectionDetailsByInspId(intInspectionId);
        if (dt1.Rows.Count > 0)
        {
            foreach (DataRow dr in dt1.Rows)
            {
                txtinspector.Text = dr["InspectorName"].ToString();
                txtmaster.Text = dr["MasterName"].ToString();
                txtchiefofficer.Text = dr["ChiefOfficerName"].ToString();
                txtsecofficer.Text = dr["SecondOfficeName"].ToString();
                txtchiefengg.Text = dr["ChiefEngineerName"].ToString();
                txtfirstassistant.Text = dr["FirstAssistantEnggName"].ToString();
                //DataTable dt3 = Inspection_Planning.AddInspectors(0, int.Parse(Session["Insp_Id"].ToString()), 0, 0, DateTime.Now, 0, 0, DateTime.Now, "", 0, 0, "CHKATT");
                DataTable dt3 = Inspection_Planning.AddInspectors(0, int.Parse(Session["Insp_Id"].ToString()), 0, 0, DateTime.Now, 0, 0, DateTime.Now, "", 0, 0, "SELECT");
                txtmtmsupt.Text = "";
                txtmtmsupt.ReadOnly = true;
                foreach (DataRow Drchk in dt3.Rows)
                {
                    if (Drchk["Attending"].ToString() == "True")
                    {
                        txtmtmsupt.Text = Drchk["Name"].ToString();
                        break;
                    }
                    else if (Drchk["Attending"].ToString() == "False")
                    {
                        txtmtmsupt.Text = Drchk["Name"].ToString();
                    }
                }
                //if (dt3.Rows.Count > 0)
                //{
                //    if (dt3.Rows[0][0].ToString() == "NO")
                //    {
                //        txtmtmsupt.ReadOnly = true;
                //        txtmtmsupt.Text = "";
                //    }
                //    else
                //    {
                //       txtmtmsupt.Text = dr["Supt"].ToString();
                //    }
                //}
                HiddenField_MTMSupt.Value = txtmtmsupt.Text;

                if ((strInsp_Status != "Planned") && (strInsp_Status != "Executed"))
                {
                    txtdonedt.Text = dr["DoneDt"].ToString();
                }
                if ((strInsp_Status != "Planned") && (strInsp_Status != "Executed"))
                {
                    txtportdone.Text = dr["PortDone"].ToString();
                }
                txtresponseduedt.Text = dr["Responsedt"].ToString();
                txtstartdate.Text = dr["StartDate1"].ToString();
                if (dr["Master"].ToString() != "")
                    Master = int.Parse(dr["Master"].ToString());
                if (dr["ChiefEngineer"].ToString() != "")
                    ChiefEngineer = int.Parse(dr["ChiefEngineer"].ToString());
                if (dr["AssistantEngineer"].ToString() != "")
                    AssistantEngineer = int.Parse(dr["AssistantEngineer"].ToString());
                if (dr["ChiefOfficer"].ToString() != "")
                    ChiefOfficer = int.Parse(dr["ChiefOfficer"].ToString());
                if (dr["SecondOffice"].ToString() != "")
                    SecondOffice = int.Parse(dr["SecondOffice"].ToString());
                
                txtinspector.Text = dr["Inspector"].ToString();

            }
        }

    }
    #region "Events"
    protected void btnUpdateCrewList_Click(object sender, EventArgs e)
    {
        EnableDisabledCrewList(true); 
    }
    protected void btnClosePopUp_Click(object sender, EventArgs e)
    {
        EnableDisabledCrewList(false);
        
    }

    private void EnableDisabledCrewList(Boolean enable)
    {
        txtmaster.Enabled = enable;
        txtchiefengg.Enabled = enable;
        txtchiefofficer.Enabled = enable;
        txtfirstassistant.Enabled = enable;
        txtsecofficer.Enabled = enable;
        txtinspector.Enabled = enable;
        txtmtmsupt.Enabled = enable;
        btn_UpdateCrew.Enabled = enable;
        btnSave.Enabled = enable;
        btnClosePopUp.Enabled = enable;
    }
    
    protected void btn_UpdateCrew_Click(object sender, EventArgs e)
    {
        DataSet ds122 = Inspection_Observation.UpdateCrew(int.Parse(Session["Insp_Id"].ToString()));
        if (ds122.Tables["Table"].Rows.Count > 0)
        {
            txtmaster.Text = ds122.Tables["Table"].Rows[0]["MasterName"].ToString();
            txtmaster_TextChanged(sender, e);
        }
        if (ds122.Tables["Table1"].Rows.Count > 0)
        {
            txtchiefengg.Text = ds122.Tables["Table1"].Rows[0]["ChiefEngineerName"].ToString();
            txtchiefengg_TextChanged(sender, e);
        }
        if (ds122.Tables["Table2"].Rows.Count > 0)
        {
            txtchiefofficer.Text = ds122.Tables["Table2"].Rows[0]["ChiefOfficerName"].ToString();
            txtchiefofficer_TextChanged(sender, e);
        }
        if (ds122.Tables["Table3"].Rows.Count > 0)
        {
            txtfirstassistant.Text = ds122.Tables["Table3"].Rows[0]["FirstAssistantName"].ToString();
            txtfirstassistant_TextChanged(sender, e);
        }
        if (ds122.Tables["Table4"].Rows.Count > 0)
        {
            txtsecofficer.Text = ds122.Tables["Table4"].Rows[0]["SecondOfficerName"].ToString();
            txtsecofficer_TextChanged(sender, e);
        }
        setColor(txtmaster);
        setColor(txtchiefengg);
        setColor(txtchiefofficer);
        setColor(txtfirstassistant);
        setColor(txtsecofficer);
    }
    public void setColor(TextBox tx)
    {
        if (tx.Text.ToUpper().Trim() == "DUPLICATE")
            tx.ForeColor = System.Drawing.Color.Red;
        else
            tx.ForeColor = System.Drawing.Color.Black;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (Session["Insp_Id"] == null) { lblmessage.Text = "Please save Planning first."; return; }
        try
        {
            DataTable dt3 = Inspection_Planning.AddInspectors(0, int.Parse(Session["Insp_Id"].ToString()), 0, 0, DateTime.Now, 0, 0, DateTime.Now, "", 0, 0, "CHKATT");
            //if (dt3.Rows.Count > 0)
            //{
            //    if (dt3.Rows[0][0].ToString() == "YES")
            //    {
            //        DataTable dt23 = Inspection_Observation.UpdateInspectionObservation(Convert.ToInt32(Session["Insp_Id"].ToString()), DateTime.Now.ToShortDateString(), "", 0, 0, 0, 0, 0, "", "", "", "", 0, "", "", 0, 0, "CHKTRAV", 0, 0, "", 0);
            //        if (dt23.Rows[0][0].ToString() != "1")
            //        {
            //            lblmessage.Text = "Please save Travel Schedule first.";
            //            return;
            //        }
            //    }
            //}
            if (txtportdone.Text.Trim() != "")
            {
                DataTable dt1 = Inspection_Planning.CheckPort(txtportdone.Text);
                if (dt1.Rows[0][0].ToString() == "0")
                {
                    lblmessage.Text = "Please enter correct Port Name.";
                    return;
                }
                else
                {
                    txtportdone.Text="";
                }
            }
            if (Master == 0)
            {
                lblmessage.Text = "Enter correct Master.";
                return;
            }
            if (txtchiefofficer.Text == "")
                ChiefOfficer = 0;
            if (txtsecofficer.Text == "")
                SecondOffice = 0;

            if (ChiefEngineer == 0)
            {
                lblmessage.Text = "Enter correct Chief Engineer(C/E).";
                return;
            }

            if (txtfirstassistant.Text == "")
                AssistantEngineer = 0; ;
         
            //if (ActualLocation == "")
            //{
            //    lblmessage.Text = "Enter correct Actual Location.";
            //    return;
            //}

            //TransType = "UPDATEOBUP";
            //ResponseDueDate = txtresponseduedt.Text;
            //ActualDate = txtdonedt.Text;
            //ID = Inspection_Id;
            //Inspection_Observation.UpdateInspectionObservation(ID, txtstartdate.Text.ToString(), InspectionNo, Master, ChiefOfficer, SecondOffice, ChiefEngineer, AssistantEngineer, txtinspector.Text, ResponseDueDate, ActualLocation, ActualDate, QuestionId, Deficiency, Comment, HighRisk, NCR, TransType, Login_Id, Login_Id, "", IsObservation);
            //lblmessage.Text = "Officer Details Updated Sucessfully.";

            //UPDATE t_InspectionDue SET 
            // Master=@Master
            //,ChiefOfficer=@ChiefOfficer
            //,SecondOffice=@SecondOffice
            //,ChiefEngineer=@ChiefEngineer	
            //,AssistantEngineer=@AssistantEngineer
            //,Inspector=@Inspector	
            //,Status='Observation'
            //WHERE Id=@ID 

            string SQL = "UPDATE t_InspectionDue SET " +
                "Master=" + Master + " " +
                ",ChiefOfficer=" + ChiefOfficer + " " +
                ",SecondOffice=" + SecondOffice + " " +
                ",ChiefEngineer=" + ChiefEngineer + " " +
                ",AssistantEngineer=" + AssistantEngineer + " " +
                ",Inspector='" + txtinspector.Text + "' " +
                ",Status='Observation' " +
                "WHERE Id=" + Inspection_Id;

            Budget.getTable(SQL);
            lblmessage.Text = "Officer Details Updated Sucessfully.";
            EnableDisabledCrewList(false);
        }
        catch (Exception ex)
        {
            lblmessage.Text = ex.StackTrace.ToString();
        }

    }
    protected void btnSaveOb_Click(object sender, EventArgs e)
    {
        if (Session["Insp_Id"] == null) { lblMsgMain.Text = "Please save Planning first."; return; }
        try
        {
            //--------------------
            //if (txtstartdate.Text.Trim() != "" && txtdonedt.Text.Trim() != "")
            //{
            //    if (Convert.ToDateTime(txtstartdate.Text) > Convert.ToDateTime(txtdonedt.Text))
            //    {
            //        lblMsgMain.Text = "Start dt. will be always less or equal to done dt.";
            //        return;
            //    }
            //}

            if (txtstartdate.Text.Trim() == "")
            {
                lblMsgMain.Text = "Please enter start date.";
                return;
            }
            if ( txtdonedt.Text.Trim() == "")
            {
                lblMsgMain.Text = "Please enter end date.";
                return;
            }
            if (Convert.ToDateTime(txtdonedt.Text.Trim()) < Convert.ToDateTime(txtstartdate.Text.Trim()))
            {
                lblMsgMain.Text = "Done date should be  more or equal to start date.";
                return;
            }
            if (Convert.ToDateTime(txtdonedt.Text.Trim()) > Convert.ToDateTime(DateTime.Today))
            {
                lblMsgMain.Text = "Done date should not be more than today.";
                return;
            }


            //--------------------
            if (txtresponseduedt.Visible)
            {
                if (txtresponseduedt.Text.Trim()=="")
                {
                    lblMsgMain.Text = "Please enter response due dt to continue.";
                    return;
                }
            }
            //--------------------
            if (txtportdone.Text.Trim() != "")
            {
                DataTable dt1 = Inspection_Planning.CheckPort(txtportdone.Text);
                if (dt1.Rows[0][0].ToString() == "0")
                {
                    lblMsgMain.Text = "Please enter correct Port Name.";
                    return;
                }
            }
            string respduedt = "NULL";
            if (txtresponseduedt.Text.Trim() != "")
            { respduedt = "'" + txtresponseduedt.Text.Trim() + "'"; }
            string SQL = "UPDATE t_InspectionDue SET ResponseDueDate=" + respduedt + ",ActualLocation='" + txtportdone.Text + "'" +
                       ",ActualDate ='" + txtdonedt.Text + "'" +
                       ",StartDate='" + txtstartdate.Text.ToString() + "'" +
                       ",Status='Executed' " +
                       "WHERE Id=" + Inspection_Id;
            
            Budget.getTable(SQL);
            lblMsgMain.Text = "Inspection Details Updated Sucessfully.";
            btn_UpdateCrew.Enabled = true; 

        }
        catch (Exception ex)
        {
            lblmessage.Text = ex.StackTrace.ToString();
        }

    }
    # endregion
    #region "Text Change Event"
    protected void txtmaster_TextChanged(object sender, EventArgs e)
    {
        if (txtmaster.Text.Trim() != "")
        {
            DataTable dt1 = Inspection_Observation.CheckUserName(txtmaster.Text, "MSTR");
            if (dt1.Rows[0][0].ToString() == "0")
            {
                lblmessage.Text = "Please enter correct Master Name.";
                Master = 0;
                return;
            }
            else
            {
                Master = int.Parse(dt1.Rows[0][0].ToString());
                txtmaster.Text = dt1.Rows[0][1].ToString();
            }
            //**** Code To Check Duplicate Entry in Officer TextBoxes
            if(txtmaster.Text.Trim() != "")
            {
                if ((txtmaster.Text == txtchiefofficer.Text) || (txtmaster.Text == txtsecofficer.Text) || (txtmaster.Text == txtchiefengg.Text) || (txtmaster.Text == txtfirstassistant.Text))
                {
                    lblmessage.Text = "Duplicate Crew entry is not permitted.";
                    txtmaster.Text = "";
                    txtmaster.Focus();
                    btnSave.Enabled = false;
                    return;
                }
                else
                    btnSave.Enabled = true;
            }
            //*******************************************************
        }

    }
    protected void txtchiefengg_TextChanged(object sender, EventArgs e)
    {
        if (txtchiefengg.Text.Trim() != "")
        {
            DataTable dt1 = Inspection_Observation.CheckUserName(txtchiefengg.Text, "C/E");
            if (dt1.Rows[0][0].ToString() == "0")
            {
                lblmessage.Text = "Please enter correct Chief Engg Name.";
                ChiefEngineer = 0;
                return;
            }
            else
            {
                ChiefEngineer = int.Parse(dt1.Rows[0][0].ToString());
                txtchiefengg.Text = dt1.Rows[0][1].ToString();
            }
            //**** Code To Check Duplicate Entry in Officer TextBoxes
            if (txtchiefengg.Text.Trim() != "")
            {
                if ((txtchiefengg.Text == txtmaster.Text) || (txtchiefengg.Text == txtchiefofficer.Text) || (txtchiefengg.Text == txtsecofficer.Text) || (txtchiefengg.Text == txtfirstassistant.Text))
                {
                    lblmessage.Text = "Duplicate Crew entry is not permitted.";
                    txtchiefengg.Text = "";
                    txtchiefengg.Focus();
                    btnSave.Enabled = false;
                    return;
                }
                else
                    btnSave.Enabled = true;
            }
            //*******************************************************
        }

    }
    protected void txtchiefofficer_TextChanged(object sender, EventArgs e)
    {
        if (txtchiefofficer.Text.Trim() != "")
        {
            DataTable dt1 = Inspection_Observation.CheckUserName(txtchiefofficer.Text, "C/O");
            if (dt1.Rows[0][0].ToString() == "0")
            {
                lblmessage.Text = "Please enter correct Chief Officer Name.";
                ChiefOfficer = 0;
                return;
            }
            else
            {
                ChiefOfficer = int.Parse(dt1.Rows[0][0].ToString());
                txtchiefofficer.Text = dt1.Rows[0][1].ToString();
            }
            //**** Code To Check Duplicate Entry in Officer TextBoxes
            if (txtchiefofficer.Text.Trim() != "")
            {
                if ((txtchiefofficer.Text == txtmaster.Text) || (txtchiefofficer.Text == txtsecofficer.Text) || (txtchiefofficer.Text == txtchiefengg.Text) || (txtchiefofficer.Text == txtfirstassistant.Text))
                {
                    lblmessage.Text = "Duplicate Crew entry is not permitted.";
                    txtchiefofficer.Text = "";
                    txtchiefofficer.Focus();
                    btnSave.Enabled = false;
                    return;
                }
                else
                    btnSave.Enabled = true;
            }
            //*******************************************************
        }

    }
    protected void txtfirstassistant_TextChanged(object sender, EventArgs e)
    {
        if (txtfirstassistant.Text.Trim() != "")
        {
            DataTable dt1 = Inspection_Observation.CheckUserName(txtfirstassistant.Text, "1 A/E");
            if (dt1.Rows[0][0].ToString() == "0")
            {
                lblmessage.Text = "Please enter correct First Assistant Name.";
                AssistantEngineer = 0;
                return;
            }
            else
            {
                AssistantEngineer = int.Parse(dt1.Rows[0][0].ToString());
                txtfirstassistant.Text = dt1.Rows[0][1].ToString();
            }
            //**** Code To Check Duplicate Entry in Officer TextBoxes
            if (txtfirstassistant.Text.Trim() != "")
            {
                if ((txtfirstassistant.Text == txtmaster.Text) || (txtfirstassistant.Text == txtchiefofficer.Text) || (txtfirstassistant.Text == txtsecofficer.Text) || (txtfirstassistant.Text == txtchiefengg.Text))
                {
                    lblmessage.Text = "Duplicate Crew entry is not permitted.";
                    txtfirstassistant.Text = "";
                    txtfirstassistant.Focus();
                    btnSave.Enabled = false;
                    return;
                }
                else
                    btnSave.Enabled = true;
            }
            //*******************************************************
        }

    }
    protected void txtdonedt_TextChanged(object sender, EventArgs e)
    {
        if (txtstartdate.Text != "" && txtdonedt.Text != "")
        {
            if (DateTime.Parse(txtstartdate.Text) > DateTime.Parse(txtdonedt.Text))
            {
                lblmessage.Text = "Done Date cannot be less than Start Date.";
                return;
            }
        }

    }
    protected void txtsecofficer_TextChanged(object sender, EventArgs e)
    {
        if (txtsecofficer.Text.Trim() != "")
        {
            DataTable dt1 = Inspection_Observation.CheckUserName(txtsecofficer.Text.Trim(), "2/O");
            if (dt1.Rows[0][0].ToString() == "0")
            {
                lblmessage.Text = "Please enter correct 2nd Officer Name.";
                SecondOffice = 0;
                return;
            }
            else
            {
                SecondOffice = int.Parse(dt1.Rows[0][0].ToString());
                txtsecofficer.Text = dt1.Rows[0][1].ToString();
            }
        }
        //**** Code To Check Duplicate Entry in Officer TextBoxes
        if (txtsecofficer.Text.Trim() != "")
        {
            if ((txtsecofficer.Text == txtmaster.Text) || (txtsecofficer.Text == txtchiefofficer.Text) || (txtsecofficer.Text == txtchiefengg.Text) || (txtsecofficer.Text == txtfirstassistant.Text))
            {
                lblmessage.Text = "Duplicate Crew entry is not permitted.";
                txtsecofficer.Text = "";
                txtsecofficer.Focus();
                btnSave.Enabled = false;
                return;
            }
            else
                btnSave.Enabled = true;
        }
        //*******************************************************

    }
    protected void txtresponseduedt_TextChanged(object sender, EventArgs e)
    {
        if (txtresponseduedt.Text != "")
        {
            if (DateTime.Parse(txtdonedt.Text) > DateTime.Parse(txtresponseduedt.Text))
            {
                lblmessage.Text = "Response Due Date cannot be less than Done Date.";
                return;
            }
        }

    }
    protected void txtportdone_TextChanged(object sender, EventArgs e)
    {
        if (txtportdone.Text.Trim() != "")
        {
            DataTable dt1 = Inspection_Planning.CheckPort(txtportdone.Text);
            if (dt1.Rows[0][0].ToString() == "0")
            {
                lblmessage.Text = "Please enter correct Port Name.";
                txtportdone.Text = "";
                return;
            }
            
        }

    }
    #endregion
}