using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using ShipSoft.CrewManager.BusinessObjects;
using ShipSoft.CrewManager.BusinessLogicLayer;
using ShipSoft.CrewManager.Operational;
using System.Data.SqlClient;

public partial class Transactions_InspectionObservation_PopUp : System.Web.UI.Page
{   

    /// <summary>
    /// Page Name            : InspectionObservation_PopUp.aspx
    /// Purpose              : This is the popup page to do the crew update
    /// Author               : Pankaj Verma
    /// Developed on         : 21 Aug 2010
    /// Last Modified by/on  : 
    /// Modifier Comments    : 
    /// </summary>

    
    Authority Auth;
    string strInsp_Status = "";
    

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
        this.Form.DefaultButton = this.btnSave.UniqueID.ToString();
        lblmessage.Text = "";
        if ((Session["loginid"] == null) || (Session["UserName"] == null))
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Default.aspx';", true);
        }
        else
        {
            Login_Id = Convert.ToInt32(Session["loginid"].ToString());
        }
        if ((Session["Insp_Id"] == null) && (Page.Request.QueryString["HMID"] == null))
        {
            Session.Add("PgFlag", 1); Response.Redirect("InspectionSearch.aspx");
        }
        else
        {
            if (Page.Request.QueryString["HMID"] != null)
            {
                int Inspection_Id = int.Parse(Page.Request.QueryString["HMID"].ToString());
                Session.Add("Insp_Id", Inspection_Id.ToString());
            }
        }
        HiddenField_InspId.Value = Session["Insp_Id"].ToString();
        if (Session["loginid"] != null)
        {
            ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 7);
            OBJ.Invoke();
            Session["Authority"] = OBJ.Authority;
            Auth = OBJ.Authority;
        }
        if (!Page.IsPostBack)
        {
            try
            {
                Button lnkadd = new Button();
                Button btnsave1 = new Button();
                Button btn_Print = new Button();
                Alerts.HANDLE_AUTHORITY(9, lnkadd, btnSave, btnsave1, btn_Print, Auth);
                Alerts.HANDLE_AUTHORITY(9, lnkadd, btn_UpdateCrew, btnsave1, btn_Print, Auth);
            }
            catch
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Default.aspx';", true);
            }
            
            if (Session["Insp_Id"] != null)
            {
                DataTable dt88 = Inspection_Planning.CheckInspectionStatus(int.Parse(Session["Insp_Id"].ToString()));
                if (dt88.Rows.Count > 0)
                {
                    strInsp_Status = dt88.Rows[0]["Status"].ToString();
                }
                if ((strInsp_Status != "Planned") && (strInsp_Status != "Executed") && (strInsp_Status != "Observation"))
                {
                    btnSave.Enabled = false;
                    btn_UpdateCrew.Enabled = false;
                                    }
                else
                {
                    btnSave.Enabled = true;
                    btn_UpdateCrew.Enabled = true;                    
                }
            }
            //****Code To Whether Inspection is External or Internal
            string strInspType = "";
            if (Session["Insp_Id"] != null)
            {
                DataTable dt56 = Inspection_Observation.CheckInspType(int.Parse(Session["Insp_Id"].ToString()));
                if (dt56.Rows.Count > 0)
                {
                    strInspType = dt56.Rows[0]["InspectionType"].ToString();
                }
                if (strInspType == "External")
                    txtinspector.ReadOnly = false;
                else
                    txtinspector.ReadOnly = true;
            }
            //******************************************************

            //*********************************************
            Session["ds"] = null;
            Login_Id = 0;
            Inspection_Id = 0;
            InspectionNo = "";
            ID = 0;
            Master = 0;
            ChiefOfficer = 0;
            SecondOffice = 0;
            ChiefEngineer = 0;
            AssistantEngineer = 0;
            firstassistant = 0;
            Inspector = "";
            ResponseDueDate = DateTime.Now.ToShortDateString();
            ActualDate = DateTime.Now.ToShortDateString();
            ActualLocation = "";
            QuestionId = 0;
            Deficiency = "";
            Comment = "";
            HighRisk = 0;
            NCR = 0;
            IsObservation = 0;
            TransType = "";
            try
            {
                
                if (Session["Insp_Id"] != null)
                {
                    Inspection_Id = int.Parse(Session["Insp_Id"].ToString());
                    
                    if (Session["DueMode"] != null)
                    {
                        if (Session["DueMode"].ToString() == "ShowFull")
                        {
                            Show_Header_Record1(Inspection_Id);
                            
                            //****Code To Check Whether Observation has Response or Not
                            if (HiddenField_ObId.Value != "")
                            {
                                DataTable dtInsp1 = Inspection_Observation.UpdateInspectionObservation(int.Parse(HiddenField_ObId.Value), DateTime.Now.ToShortDateString(), "", 0, 0, 0, 0, 0, "", "", "", "", 0, "", "", 0, 0, "CHKRESP", 0, 0, "", 0);
                                if (dtInsp1.Rows.Count > 0)
                                {
                                    if (dtInsp1.Rows[0][0].ToString() == "NO")
                                    {
                                        btnSave.Enabled = false;
                                        btn_UpdateCrew.Enabled = false;
                                        
                                    }
                                    else
                                    {
                                        btnSave.Enabled = true;
                                        btn_UpdateCrew.Enabled = true;
                                        
                                    }
                                }
                            }
                            //*********************************************************
                        }
                        if (Session["DueMode"].ToString() == "ShowId")
                        {
                            btnSave.Enabled = true;
                            btn_UpdateCrew.Enabled = true;
                            btnSave.Enabled = Auth.isEdit;
                            btn_UpdateCrew.Enabled = Auth.isEdit;
                            
                        }
                        if ((strInsp_Status == "Planned") || (strInsp_Status == "Executed") || (strInsp_Status == "Observation"))
                        {
                            try
                            {
                                Button lnkadd = new Button();
                                Button btnsave1 = new Button();
                                Button btn_Print = new Button();
                                Alerts.HANDLE_AUTHORITY(9, lnkadd, btnSave, btnsave1, btn_Print, Auth);
                                Alerts.HANDLE_AUTHORITY(9, lnkadd, btn_UpdateCrew, btnsave1, btn_Print, Auth);
                                //******Accessing UserOnBehalf/Subordinate Right******
                                try
                                {
                                    if (Session["Insp_Id"] != null)
                                    {
                                        int useronbehalfauth = Alerts.UserOnBehalfRight(Convert.ToInt32(Session["loginid"].ToString()), Convert.ToInt32(Session["Insp_Id"].ToString()));
                                        if (useronbehalfauth <= 0)
                                        {
                                            btnSave.Enabled = false;
                                            btn_UpdateCrew.Enabled = false;
                                            
                                        }
                                        else
                                        {
                                            if ((strInsp_Status != "Closed") && (strInsp_Status != "Due"))
                                            {
                                                btnSave.Enabled = true;
                                                btn_UpdateCrew.Enabled = true;
                                               
                                            }
                                        }
                                    }
                                }
                                catch
                                {
                                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Default.aspx';", true);
                                }
                                //****************************************************
                            }
                            catch
                            {
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Default.aspx';", true);
                            }
                            
                            //******Accessing UserOnBehalf/Subordinate Right******
                            try
                            {
                                if (Session["Insp_Id"] != null)
                                {
                                    int useronbehalfauth = Alerts.UserOnBehalfRight(Convert.ToInt32(Session["loginid"].ToString()), Convert.ToInt32(Session["Insp_Id"].ToString()));
                                    if (useronbehalfauth <= 0)
                                    {
                                        btnSave.Enabled = false;
                                        btn_UpdateCrew.Enabled = false;
                                        
                                    }
                                    else
                                    {
                                        if ((strInsp_Status != "Closed") && (strInsp_Status != "Due"))
                                        {
                                            btnSave.Enabled = true;
                                            btn_UpdateCrew.Enabled = true;
                                            
                                        }
                                    }
                                }
                            }
                            catch
                            {
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Default.aspx';", true);
                            }
                            //****************************************************
                        }
                    }
                    else
                    {
                        Show_Header_Record1(Inspection_Id);
                        
                        //****Code To Check Whether Observation has Response or Not
                        if (HiddenField_ObId.Value != "")
                        {
                            DataTable dtInsp1 = Inspection_Observation.UpdateInspectionObservation(int.Parse(HiddenField_ObId.Value), DateTime.Now.ToShortDateString(), "", 0, 0, 0, 0, 0, "", "", "", "", 0, "", "", 0, 0, "CHKRESP", 0, 0, "", 0);
                            if (dtInsp1.Rows.Count > 0)
                            {
                                if (dtInsp1.Rows[0][0].ToString() == "NO")
                                {
                                    btnSave.Enabled = false;
                                    btn_UpdateCrew.Enabled = false;
                                    
                                }
                                else
                                {
                                    btnSave.Enabled = true;
                                    btn_UpdateCrew.Enabled = true;
                                    
                                }
                            }
                        }
                        //*********************************************************
                    }
                }
                if ((strInsp_Status == "Closed") || (strInsp_Status == "Due"))
                {
                    btnSave.Enabled = false;
                    btn_UpdateCrew.Enabled = false;
                    
                }
            }
            catch (Exception ex)
            {
                lblmessage.Text = ex.Message.ToString();
            }
            //***Check whether Inspection is On Hold or not*******
            DataTable dtchk = Inspection_TravelSchedule.CheckInspectionOnHold(Convert.ToInt32(Session["Insp_Id"].ToString()));
            if (dtchk.Rows.Count > 0)
            {
                if (dtchk.Rows[0][0].ToString() == "Y")
                {
                    btnSave.Enabled = false;
                    btn_UpdateCrew.Enabled = false;
                    
                }
            }
            //**************************************************** POST BACK ENDING  HERE
            DataTable dt = Budget.getTable("select Master from T_INSPECTIONDUE where id=" + Session["Insp_Id"].ToString()).Tables[0];
            if (Convert.IsDBNull(dt.Rows[0][0]))                                        // CHECK IF FIRST TIME POPUP IS COMING ( DATA FROM CMS)
                btn_UpdateCrew_Click(sender, e);
            else                                                                        // ELSE FETCH DATA FROM VIMS
                Show_Header_Record1(Inspection_Id);
            //**************************************************** POST BACK ENDING  HERE
        }

    }

    #region "User defined Properties"
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
    public int ID
    {
        get
        {
            return int.Parse(ViewState["_ID"].ToString());
        }
        set
        {
            ViewState["_ID"] = value;
        }
    }
    public string InspectionNo
    {
        get
        {
            return ViewState["_InspectionNo"].ToString();
        }
        set
        {
            ViewState["_InspectionNo"] = value;
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
    public int firstassistant
    {
        get
        {
            return int.Parse(ViewState["_firstassistant"].ToString());
        }
        set
        {
            ViewState["_firstassistant"] = value;
        }
    }
    public string Inspector
    {
        get
        {
            return ViewState["_Inspector"].ToString();
        }
        set
        {
            ViewState["_Inspector"] = value;
        }
    }
    public string ResponseDueDate
    {
        get
        {
            return ViewState["_ResponseDueDate"].ToString();
        }
        set
        {
            ViewState["_ResponseDueDate"] = value;
        }
    }
    public string ActualDate
    {
        get
        {
            return ViewState["_ActualDate"].ToString();
        }
        set
        {
            ViewState["_ActualDate"] = value;
        }
    }
    public string ActualLocation
    {
        get
        {
            return ViewState["_ActualLocation"].ToString();
        }
        set
        {
            ViewState["_ActualLocation"] = value;
        }
    }
    public int QuestionId
    {
        get
        {
            return int.Parse(ViewState["_QuestionId"].ToString());
        }
        set
        {
            ViewState["_QuestionId"] = value;
        }
    }
    public string Deficiency
    {
        get
        {
            return ViewState["_Deficiency"].ToString();
        }
        set
        {
            ViewState["_Deficiency"] = value;
        }
    }
    public string Comment
    {
        get
        {
            return ViewState["_Comment"].ToString();
        }
        set
        {
            ViewState["_Comment"] = value;
        }
    }
    public int HighRisk
    {
        get
        {
            return int.Parse(ViewState["_HighRisk"].ToString());
        }
        set
        {
            ViewState["_HighRisk"] = value;
        }
    }
    public int NCR
    {
        get
        {
            return int.Parse(ViewState["_NCR"].ToString());
        }
        set
        {
            ViewState["_NCR"] = value;
        }
    }
    public string TransType
    {
        get
        {
            return ViewState["_TransType"].ToString();
        }
        set
        {
            ViewState["_TransType"] = value;
        }
    }
    public int IsObservation
    {
        get
        {
            return int.Parse(ViewState["_IsObsv"].ToString());
        }
        set
        {
            ViewState["_IsObsv"] = value;
        }
    }
    #endregion
    #region"User Defined Functions"
    //Show Observation Records By InspectionDueId
    protected void Show_Header_Record1(int intInspectionId)
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
                Inspector = dr["Inspector"].ToString();
               
            }
        }
        
    }        
    #endregion
    #region "Events"
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
    public void setColor( TextBox tx)
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
            if (dt3.Rows.Count > 0)
            {
                if (dt3.Rows[0][0].ToString() == "YES")
                {
                    DataTable dt23 = Inspection_Observation.UpdateInspectionObservation(Convert.ToInt32(Session["Insp_Id"].ToString()), DateTime.Now.ToShortDateString(), "", 0, 0, 0, 0, 0, "", "", "", "", 0, "", "", 0, 0, "CHKTRAV", 0, 0, "", 0);
                    if (dt23.Rows[0][0].ToString() != "1")
                    {
                        lblmessage.Text = "Please save Travel Schedule first.";
                        return;
                    }
                }
            }
            //if (txtportdone.Text.Trim() != "")
            //{
            //    DataTable dt1 = Inspection_Planning.CheckPort(txtportdone.Text);
            //    if (dt1.Rows[0][0].ToString() == "0")
            //    {
            //        lblmessage.Text = "Please enter correct Port Name.";
            //        return;
            //    }
            //    else
            //    {
            //        ActualLocation = txtportdone.Text;
            //    }
            //}
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
            if (txtinspector.Text == "")
                Inspector = "";

            //if (ActualLocation == "")
            //{
            //    lblmessage.Text = "Enter correct Actual Location.";
            //    return;
            //}

            TransType = "UPDATEOBUP";            
            ResponseDueDate = txtresponseduedt.Text;            
            ActualDate = txtdonedt.Text;
            ID = Inspection_Id;
            Inspection_Observation.UpdateInspectionObservation(ID, txtstartdate.Text.ToString(), InspectionNo, Master, ChiefOfficer, SecondOffice, ChiefEngineer, AssistantEngineer, txtinspector.Text, ResponseDueDate, ActualLocation, ActualDate, QuestionId, Deficiency, Comment, HighRisk, NCR, TransType, Login_Id, Login_Id, "", IsObservation);
            lblmessage.Text = "Officer Details Updated Sucessfully.";
            
        }
        catch (Exception ex)
        {
            lblmessage.Text = ex.StackTrace.ToString();
        }

    }
    protected void btnSaveOb_Click(object sender, EventArgs e)
    {
        if (Session["Insp_Id"] == null) { lblmessage.Text = "Please save Planning first."; return; }
        try
        {
            DataTable dt3 = Inspection_Planning.AddInspectors(0, int.Parse(Session["Insp_Id"].ToString()), 0, 0, DateTime.Now, 0, 0, DateTime.Now, "", 0, 0, "CHKATT");
            if (dt3.Rows.Count > 0)
            {
                if (dt3.Rows[0][0].ToString() == "YES")
                {
                    DataTable dt23 = Inspection_Observation.UpdateInspectionObservation(Convert.ToInt32(Session["Insp_Id"].ToString()), DateTime.Now.ToShortDateString(), "", 0, 0, 0, 0, 0, "", "", "", "", 0, "", "", 0, 0, "CHKTRAV", 0, 0, "", 0);
                    if (dt23.Rows[0][0].ToString() != "1")
                    {
                        lblmessage.Text = "Please save Travel Schedule first.";
                        return;
                    }
                }
            }
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
                    ActualLocation = txtportdone.Text;
                }
            }
            
            Master = 0; // CODE ADDED
            ChiefOfficer = 0;
            SecondOffice = 0;
            ChiefEngineer = 0; // CODE ADDED
            AssistantEngineer = 0; ;
            Inspector = "";

            TransType = "UPDATEOB";
            ResponseDueDate = txtresponseduedt.Text;
            ActualDate = txtdonedt.Text;
            ID = Inspection_Id;
            Inspection_Observation.UpdateInspectionObservation(ID, txtstartdate.Text.ToString(), "", 0, 0, 0, 0, 0, "", ResponseDueDate, ActualLocation, ActualDate, QuestionId, "", "", HighRisk, NCR, TransType , Login_Id, Login_Id, "", 0);
            //Inspection_Observation.UpdateInspectionObservation(ID, txtstartdate.Text.ToString(), InspectionNo, Master, ChiefOfficer, SecondOffice, ChiefEngineer, AssistantEngineer, txtinspector.Text, ResponseDueDate, ActualLocation, ActualDate, QuestionId, Deficiency, Comment, HighRisk, NCR, TransType, Login_Id, Login_Id, "", IsObservation);
            //Show_Header_Record1(Inspection_Id);
            btn_UpdateCrew_Click(sender, e);
            lblmessage.Text = "Inspection Details Updated Sucessfully.";

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
                ActualLocation = "";
                return;
            }
            else
            {
                ActualLocation = txtportdone.Text;
            }
        }

    }
    #endregion
}
