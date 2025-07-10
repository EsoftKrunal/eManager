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
using Ionic.Zip;


public partial class Transactions_InspectionPlanning : System.Web.UI.Page
{
    int Login_Id;
    int Inspection_Id;
    int temp = 0;
    public string strInsp_Status = "";
    int intInspDueId = 0;
    Authority Auth;
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
        this.Form.DefaultButton = this.btnsave.UniqueID.ToString();
        if (Session["loginid"] == null)
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Login.aspx';", true);
        }
        else
        {
            Login_Id = Convert.ToInt32(Session["loginid"].ToString());
        }
        lblmessage.Text = "";

        int TmpInspid = Common.CastAsInt32(Session["Insp_Id"]);
        //if (TmpInspid > 0)
        //{
        //    DataTable dts = Common.Execute_Procedures_Select_ByQuery("SELECT INSPECTIONNO FROM T_INSPECTIONDUE WHERE ID=" + TmpInspid.ToString());
        //    if (dts.Rows.Count > 0)
        //    {
        //        lblINSPNo.Text = dts.Rows[0][0].ToString();
        //    }
        //}

        if (Session["Mode"] != null)
        {
            if (Session["Mode"].ToString() != "Add")
            {
                if ((Session["Insp_Id"] == null) && (Page.Request.QueryString["HMID"] == null))
                { 
                    Session.Add("PgFlag", 1); Response.Redirect("InspectionSearch.aspx"); 
                }
                else
                {
                    if (Page.Request.QueryString["HMID"] == null)
                        Inspection_Id = int.Parse(Session["Insp_Id"].ToString());
                    else
                    {
                        Inspection_Id = int.Parse(Page.Request.QueryString["HMID"].ToString());
                        Session.Add("Insp_Id", Inspection_Id.ToString());
                    }
                }
            }
            else
            { }
        }
        //try { Inspection_Id = int.Parse(Session["Insp_Id"].ToString()); } catch { }
        if (Session["loginid"] != null)
        {
            ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 7);
            OBJ.Invoke();
            Session["Authority"] = OBJ.Authority;
            Auth = OBJ.Authority;
        }
        if (!Page.IsPostBack)
        {
            bindVersionsDDl();
            try
            {
                try
                {
                    Alerts.HANDLE_AUTHORITY(7, btnadd, btnsave, null, btn_Print, Auth);
                }
                catch
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Default.aspx';", true);
                }
                //*********Code to disable Vessel & Inspection DDL once Inspection# is generated
                if (Session["Insp_Id"] != null)
                {
                    if (Session["DueMode"] != null)
                    {
                        if (Session["DueMode"].ToString() == "ShowId")
                        {
                            ddlvessel.Enabled = false;
                            ddlinspection.Enabled = false;
                        }
                        else
                        {
                            ddlvessel.Enabled = false;
                            ddlinspection.Enabled = false;
                        }
                    }
                    else
                    {
                        ddlvessel.Enabled = false;
                        ddlinspection.Enabled = false;
                    }
                }
                else
                {
                    ddlvessel.Enabled = true;
                    ddlinspection.Enabled = true;
                }
                //******************************************************************************
                if (Session["Insp_Id"] != null)
                {
                    DataTable dt88 = Inspection_Planning.CheckInspectionStatus(int.Parse(Session["Insp_Id"].ToString()));
                    if (dt88.Rows.Count > 0)
                    {
                        strInsp_Status = dt88.Rows[0]["Status"].ToString();
                    }
                    if (strInsp_Status != "Planned")
                    {
                        btnsave.Enabled = false;
                        btnadd.Enabled = false;
                        chk_OnHold.Enabled = false;
                        temp = 1;

                        btn_Notify.Enabled = false;
                        vtn_VNotify.Enabled = false; 
                    }
                    else
                    {
                        btnsave.Enabled = true;
                        btnadd.Enabled = true;
                        chk_OnHold.Enabled = true;

                        btn_Notify.Enabled = true;
                        vtn_VNotify.Enabled = true; 
                        
                    }
                }
                LoadDefaultRows();
                BindVessel();
                //***********
                if (Session["Insp_Id"] != null)
                {
                    string strInspType = "";
                    DataTable dt56 = Inspection_Observation.CheckInspType(int.Parse(Session["Insp_Id"].ToString()));
                    if (dt56.Rows.Count > 0)
                    {
                        strInspType = dt56.Rows[0]["InspectionType"].ToString();
                    }
                    if (strInspType == "External")
                        rdbexternalenternal.SelectedIndex = 1;
                    else
                        rdbexternalenternal.SelectedIndex = 0;
                    rdbexternalenternal_SelectedIndexChanged(sender, e);
                }
                else
                {
                    rdbexternalenternal.SelectedIndex = 1;
                    rdbexternalenternal_SelectedIndexChanged(sender, e);
                }
                //***********
              
                BindInspection();
                BindInspectors();
                //  if id is null then new record

                // if  Session["DueMode"] =show id  show id only

                // if  Session["DueMode"] =full record  show full record 

                if (Session["Insp_Id"] != null)
                {
                    DataTable dt1 = Inspection_TravelSchedule.SelectInspectionDetailsByInspId(int.Parse(Session["Insp_Id"].ToString()));
                    foreach (DataRow dr in dt1.Rows)
                    {
                        ddlvessel.SelectedValue = dr["Vid"].ToString();
                        ddlinspection.SelectedValue = dr["InsId"].ToString();
                        ddlinspection_SelectedIndexChanged(sender, e);  
                    }
                    if (Session["DueMode"] != null)
                    {
                        if (Session["DueMode"].ToString() == "ShowFull")
                        {
                            Show_Planning_Record(Inspection_Id);
                            BindSupGrid();
                        }
                        if (Session["DueMode"].ToString() == "ShowId")
                        {
                            btnadd.Enabled = true;
                            btnsave.Enabled = true;
                            btnadd.Enabled = Auth.isEdit;
                            btnsave.Enabled = Auth.isEdit;
                            chk_OnHold.Enabled = true;
                            chk_OnHold.Enabled = Auth.isEdit;
                        }
                        if (strInsp_Status == "Planned")
                        {
                            try
                            {
                                Alerts.HANDLE_AUTHORITY(7, btnadd, btnsave, null, btn_Print, Auth);
                                //******Accessing UserOnBehalf/Subordinate Right******
                                try
                                {
                                    if (Session["Insp_Id"] != null)
                                    {
                                        int useronbehalfauth = Alerts.UserOnBehalfRight(Convert.ToInt32(Session["loginid"].ToString()), Convert.ToInt32(Session["Insp_Id"].ToString()));
                                        if (useronbehalfauth <= 0)
                                        {
                                            btnadd.Enabled = false;
                                            btnsave.Enabled = false;
                                            chk_OnHold.Enabled = false;
                                            foreach (GridViewRow gr in grdinspector.Rows)
                                            {
                                                ImageButton imgbtndel = (ImageButton)gr.FindControl("imgbtn");
                                                imgbtndel.Enabled = false;
                                            }
                                        }
                                        else
                                        {
                                            if ((strInsp_Status != "Closed") && (strInsp_Status != "Due"))
                                            {
                                                btnadd.Enabled = true;
                                                btnsave.Enabled = true;
                                                chk_OnHold.Enabled = true;
                                                foreach (GridViewRow gr in grdinspector.Rows)
                                                {
                                                    ImageButton imgbtndel = (ImageButton)gr.FindControl("imgbtn");
                                                    imgbtndel.Enabled = true;
                                                }
                                            }
                                        }
                                        //*******Check Whether Superintendent exists or not****
                                        DataTable dt83 = Inspection_Planning.CheckSuperintendent(Convert.ToInt32(Session["Insp_Id"].ToString()));
                                        if (int.Parse(dt83.Rows[0][0].ToString()) == 0)
                                        {
                                            if ((strInsp_Status != "Closed") && (strInsp_Status != "Due"))
                                            {
                                                btnadd.Enabled = true;
                                                btnsave.Enabled = true;
                                                chk_OnHold.Enabled = true;
                                                foreach (GridViewRow gr in grdinspector.Rows)
                                                {
                                                    ImageButton imgbtndel = (ImageButton)gr.FindControl("imgbtn");
                                                    imgbtndel.Enabled = true;
                                                }
                                            }
                                        }
                                        //*****************************************************
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
                        }
                    }
                    else
                    {
                        Show_Planning_Record(Inspection_Id);
                        BindSupGrid();
                        
                        
                    }
                }
            }
            catch (Exception ex)
            {
                lblmessage.Text = ex.StackTrace.ToString();
            }
       }

        // mode to check enable & disable cancel planning button
        btnCancelPlan.Visible = false;
        string maa="start ";
        int int_Insid = Common.CastAsInt32(Session["Insp_Id"]);
        if (int_Insid > 0)
        {
            DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT status FROM DBO.t_inspectiondue WHERE ID=" + int_Insid.ToString());
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0][0].ToString().Trim() == "Planned") // ONLY PLAN 
                {

                    maa += ", planned";

                    if (Convert.ToInt32(Session["loginid"].ToString()) == 1)
                    {
                        btnCancelPlan.Visible = true;
                    }
                    else
                    {
                        int useronbehalfauth = Alerts.UserOnBehalfRight(Convert.ToInt32(Session["loginid"].ToString()), int_Insid); // ON BEHALF RIGHTS
                        if (useronbehalfauth > 0)
                        {
                            // CAN NOT CANCEL SIRE & CDI
                            maa += ", behalf";

                            dt = Common.Execute_Procedures_Select_ByQuery("select * from t_inspectiondue where ( ( inspectionid=21 ) or ( inspectionid IN (select ID from m_inspection where inspectiongroup=1) ) ) AND ID=" + int_Insid.ToString());
                            if (dt.Rows.Count <= 0)
                            {
                                maa += ", sire / cdi";
                                btnCancelPlan.Visible = true;
                            }
                            else
                            {

                            }
                        }
                    }
                }
            }
        }
        //Response.Write(maa);
        // mode for when refer is from vetting planner page
        btnBack.Visible = false; 
        if(Request.QueryString["mode"]!=null)
        {
            btnBack.Visible = true;
            if (!IsPostBack & Request.QueryString["mode"]=="m1")    
            {
                char[] sep = { '_' };
                string[] vals = Request.QueryString["param"].ToString().Split(sep);
                ddlvessel.SelectedValue = vals[0];
                ddlvessel.Enabled = false; 
                if (vals[1] == "CDI")
                {
                    ddlinspection.SelectedValue = "21";
                    ddlinspection_SelectedIndexChanged(sender, e);
                    ddlinspection.Enabled = false;
                    ddlVersions.SelectedIndex = ddlVersions.Items.Count - 1;
                }
            }
        }
        
    }
    protected void Main_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlsup.SelectedIndex != 0)
            {
                int YsInspection;
                if (ddlattendinspection.SelectedValue == "No")
                {
                    YsInspection = 0;
                }
                else
                {
                    YsInspection = 1;
                }
                AddRow1(Convert.ToInt32(ddlsup.SelectedValue), YsInspection, ddlsup.SelectedItem.Text, ddlattendinspection.SelectedItem.Text);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
       
}
    public void BindVessel()
    {
        try
        {
            DataSet dsVessel = Inspection_Master.getMasterDataforInspection("Vessel", "VesselId", "VesselName as Name",Convert.ToInt32(Session["loginid"].ToString()));
            this.ddlvessel.DataSource = dsVessel;
            this.ddlvessel.DataValueField = "VesselId";
            this.ddlvessel.DataTextField = "Name";
            this.ddlvessel.DataBind();
            ddlvessel.Items.Insert(0, "<Select>");
            ddlvessel.Items[0].Value = "0";
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public void BindInspection()
    {
        string ch = "";
        try
        {
            DataSet dsInspection = Inspection_Planning.GetInspection(rdbexternalenternal.SelectedValue);
            this.ddlinspection.DataSource = dsInspection;
            this.ddlinspection.DataValueField = "Id";
            this.ddlinspection.DataTextField = "CodeName";
            this.ddlinspection.DataBind();
            ddlinspection.Items.Insert(0, "<Select>");
            ddlinspection.Items[0].Value = "0";

            lblSire.Visible = false;
            ddlIsSire.Visible = false;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public void bindVersionsDDl()
    {
        try
        {

            DataTable dt2 = Budget.getTable("SELECT VersionId,VersionName FROM m_InspGroupVersions WHERE GROUPID In (select InspectionGroup from m_Inspection where id=" + Convert.ToInt32("0" + ddlinspection.SelectedValue) + ")").Tables[0];
            ddlVersions.DataSource = dt2;
            ddlVersions.DataValueField = "VersionId";
            ddlVersions.DataTextField = "VersionName";
            ddlVersions.DataBind();
            ddlVersions.Items.Insert(0, new ListItem("<Select>", "0"));
        }
        catch { }
    }
    public void BindInspectors()
    {
        try
        {
            //DataTable dtInspection = Common.Execute_Procedures_Select_ByQuery("select LoginId,FirstName+' '+LastName as Name from DBO.USERLOGIN WHERE LOGINID IN (SELECT USERID FROM DBO.Hr_PersonalDetails U INNER JOIN DBO.POSITION P ON U.POSITION=P.POSITIONID AND ISINSPECTOR=1) ORDER BY [Name]");
            DataTable dtInspection = Common.Execute_Procedures_Select_ByQuery("Select loginid As UserId,(FirstName+' '+lastname) AS EmpName from DBO.usermaster with(nolock) Where PositionId IN (Select PositionId from DBO.Position WHERE VesselPositions IN (1,2,3) or isinspector=1) order by (FirstName+' '+lastname)");

            this.ddlsup.DataSource = dtInspection;
            this.ddlsup.DataValueField = "UserId";
            this.ddlsup.DataTextField = "EmpName";
            this.ddlsup.DataBind();
            ddlsup.Items.Insert(0, "<Select>");   
            ddlsup.Items[0].Value ="0";   
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    //****Show Planning Record From InspectionDueId
    protected void Show_Planning_Record(int InspectionDueId)
    {
        DataTable dt = Inspection_Planning.AdInspectionPlanning(InspectionDueId, 0, "", 0, DateTime.Now, "", 0, 0, 0, 0, 0, "SELECT", "","", "", "", "", "","",0);
        if (dt.Rows.Count > 0)
        {
            foreach (DataRow dr in dt.Rows)
            {
                txtplandate.Text = DateTime.Parse(dr["Plan_Date"].ToString()).ToString("dd-MMM-yyyy");
                rdblocation.SelectedValue = dr["PlanLocation"].ToString();
                txtfromport.Text = dr["Plan_From_Port"].ToString();
                txttoport.Text = dr["Plan_To_Port"].ToString();
                txtremark.Text = dr["PlanRemark"].ToString();
                if (dr["ReqForInsp"].ToString() == "True")
                    chkrequest.Checked=true;
                else
                    chkrequest.Checked = false;
                txtCreatedBy_DocumentType.Text = dr["Created_By"].ToString();
                txtCreatedOn_DocumentType.Text = dr["Created_On"].ToString();
                txtModifiedBy_DocumentType.Text = dr["Modified_By"].ToString();
                txtModifiedOn_DocumentType.Text = dr["Modified_On"].ToString();
                if (dr["OnHold"].ToString() == "Y")
                    chk_OnHold.Checked = true;
                else
                    chk_OnHold.Checked = false;
                ddlIsSire.SelectedValue = dr["IsSire"].ToString().ToUpper();
                ddlVersions.SelectedValue = dr["VersionId"].ToString().ToUpper();
                
            }
        }
        DataTable DTS = Common.Execute_Procedures_Select_ByQuery("SELECT Inspector FROM T_INSPECTIONDUE WHERE ID=" + InspectionDueId.ToString());
        if (DTS.Rows.Count > 0)
        {
            txtInspectorName.Text = Convert.ToString(DTS.Rows[0][0]);
        }
            
    }
    //****Bind Superintendent Grid From InspectionDueId
    public void BindSupGrid()
    {
        try
        {
            DataTable Dt = Inspection_Planning.AddInspectors(0, Inspection_Id, 0, 0, DateTime.Now, 0, 0, DateTime.Now, "", 0, 0, "SELECT");
            if (Dt.Rows.Count == 0)
            {

                LoadDefaultRows();
                return;
            }
            if (Dt.Rows.Count < 5)
            {
                int check = 5 - Dt.Rows.Count;
                int CheckOut = 0;
                DataTable dttemp = new DataTable();
                dttemp.Columns.Add("Id");
                dttemp.Columns.Add("Attending");
                dttemp.Columns.Add("SuperintendentId");
                dttemp.Columns.Add("Name");
                dttemp.Columns.Add("Status");
                int s;
                int CheckIt = Dt.Rows.Count;

                for (s = 0; s < CheckIt; s++)
                {
                    DataRow drtemp = dttemp.NewRow();
                    drtemp["Id"] = Dt.Rows[s]["Id"];
                    drtemp["Attending"] = Dt.Rows[s]["Attending"];
                    drtemp["SuperintendentId"] = Dt.Rows[s]["SuperintendentId"];
                    drtemp["Name"] = Dt.Rows[s]["Name"];
                    drtemp["Status"] = Dt.Rows[s]["Status"];
                    dttemp.Rows.Add(drtemp);

                }

                int temp;
                if (Dt.Rows.Count == 0)
                    temp = 0;
                else
                    temp = int.Parse(Dt.Rows[Dt.Rows.Count - 1]["Id"].ToString());
                for (CheckOut = 0; CheckOut < check; CheckOut++)
                {


                    DataRow drtemp = dttemp.NewRow();
                    drtemp["Id"] = temp + 1;
                    drtemp["Name"] = "";
                    drtemp["Attending"] = "";
                    drtemp["SuperintendentId"] = 0;
                    drtemp["Name"] = "";
                    drtemp["Status"] = 0;
                    dttemp.Rows.Add(drtemp);
                    temp++;
                }
                this.grdinspector.DataSource = dttemp;
                this.grdinspector.DataBind();

            }
            else
            {
                this.grdinspector.DataSource = Dt;
                this.grdinspector.DataBind();
            }

            DataTable dt2;
            dt2 = new DataTable();
            dt2.Columns.Add("Id");
            DataColumn[] data = new DataColumn[1];
            data[0] = dt2.Columns[0];
            dt2.PrimaryKey = data;
            dt2.Columns.Add("Name");
            dt2.Columns.Add("Attending");
            dt2.Columns.Add("SuptId");
            dt2.Columns.Add("AttendingYesNo");
            dt2.Columns.Add("Status");
            int cnt;
            for (cnt = 0; cnt < Dt.Rows.Count; cnt++)
            {
                if (Dt.Rows[cnt]["SuperintendentId"].ToString() != "")
                {
                    DataRow dd = dt2.NewRow();
                    if (Dt.Rows[cnt]["Status"].ToString() != "1")
                        dd["Id"] = cnt + 1;
                    else
                        dd["Id"] = Dt.Rows[cnt]["Id"];
                    dd["Name"] = Dt.Rows[cnt]["Name"];
                    dd["Attending"] = Dt.Rows[cnt]["Attending"];
                    dd["SuptId"] = Dt.Rows[cnt]["SuperintendentId"];
                    dd["AttendingYesNo"] = "";
                    dd["Status"] = Dt.Rows[cnt]["Status"];
                    dt2.Rows.Add(dd);
                }
            }
            ViewState["GridData"] = dt2;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        try
        {
            int InspectionDueId = 0;
            try
            {
                if (Session["Insp_Id"] != null)
                {
                    InspectionDueId = Convert.ToInt32(Session["Insp_Id"].ToString());
                }       
            }
            catch { }

            int intCreatedBy = 0;
            int intModifiedBy = 0;
            intCreatedBy = Login_Id;
            intModifiedBy = Login_Id;
            int ReqForInsp;
            int fromport = 0;
            int toport = 0;
            //--------------------------
            DataTable dtg = null;
            try
            {
                dtg = (DataTable)ViewState["GridData"];
            }
            catch { }
            //--------------------------
            if (dtg==null)
            {
                DataTable dt15 = Inspection_Planning.GetRandomInspection(Convert.ToInt32(ddlinspection.SelectedValue));
                if (dt15.Rows.Count > 0)
                {
                    if (dt15.Rows[0]["RandomInspection"].ToString() == "N")
                    {
                        lblmessage.Text = "Please add Superintendent.";
                        return;
                    }
                }
            }
            //--------------------------
            if (dtg != null)
            {
                int IsValue = 0;
                int k;
                for (k = 0; k < dtg.Rows.Count; k++)
                {
                    if ((dtg.Rows[k]["SuptId"].ToString().Trim()!="0") && (dtg.Rows[k]["SuptId"].ToString().Trim()!=""))
                    {
                        IsValue = 1;
                        break; 
                    }
                   
                }
                if (IsValue == 0)
                {
                    DataTable dt15 = Inspection_Planning.GetRandomInspection(Convert.ToInt32(ddlinspection.SelectedValue));
                    if (dt15.Rows.Count > 0)
                    {
                        if (dt15.Rows[0]["RandomInspection"].ToString() == "N")
                        {
                            lblmessage.Text = "Please add Superintendent.";
                            return;
                        }
                    }
                }
                
            }
            //--------------------------
            if (dtg.Rows.Count <= 0)
            {
                lblmessage.Text = "Please add Superintendent to continue.";
                return;
            }
            //--------------------------
            if (txtfromport.Text.Trim() != "")
            {
                DataTable dt1 = Inspection_Planning.CheckPort(txtfromport.Text);
                if (dt1.Rows[0][0].ToString() == "0")
                {
                    lblmessage.Text = "Please Enter Correct From Port Name.";
                    return;
                }
                else
                {
                    fromport = int.Parse(dt1.Rows[0][0].ToString());
                }
            }
            //--------------------------
            if (txttoport.Text.Trim() != "")
            {
                DataTable dt2 = Inspection_Planning.CheckPort(txttoport.Text);
                if (dt2.Rows[0][0].ToString() == "0")
                {
                    lblmessage.Text = "Please Enter Correct To Port Name.";
                    return;
                }
                else
                {
                    toport = int.Parse(dt2.Rows[0][0].ToString());
                }
            }
            //--------------------------
            if (ddlIsSire.Visible)
            {
                if (ddlIsSire.SelectedIndex <= 0)
                {
                    lblmessage.Text = "Please select inspection is SIRE or Not.";
                    ddlIsSire.Focus(); 
                    return;
                }
            }
            //--------------------------
            if (rdblocation.SelectedValue == "Port")
            {
                txttoport.Text = txtfromport.Text;
                toport = fromport;
            }
            if (chkrequest.Checked == true)
            {
                ReqForInsp = 1;
            }
            else
            {
                ReqForInsp = 0;
            }

            //--------------------------- CHECK FOR NEW PLANNING DATE MUST BE MORE THAN LAST PLANNING DATE

            string InspectionNumber = GetInspectionNumber(int.Parse(ddlvessel.SelectedValue));
            string part = InspectionNumber.Substring(0, InspectionNumber.LastIndexOf("/"));

            DataTable dtRandom = Budget.getTable("select RandomInspection from m_inspection where id=" + ddlinspection.SelectedValue).Tables[0];
            if (dtRandom.Rows.Count > 0)
            {
                if (dtRandom.Rows[0][0].ToString().Trim() != "Y") // NOT A RANDOM INSPECTION THEN WE CHECK DATE
                {
                     DataTable dt_Pos = Budget.getTable("SELECT INSPECTIONNO,ISNULL(ACTUALDATE,'') AS ACTUALDATE FROM T_INSPECTIONDUE WHERE ID<>" + InspectionDueId.ToString() + " AND INSPECTIONNO LIKE '" + part + "%' ORDER BY ACTUALDATE DESC").Tables[0];
                    if (dt_Pos.Rows.Count > 0)
                    {
                        if (dt_Pos.Rows[0][1].ToString().Trim() != "")
                        {
                            if (DateTime.Parse(txtplandate.Text) <= DateTime.Parse(dt_Pos.Rows[0][1].ToString()))
                            {
                                lblmessage.Text = "Plan date is invalid. It should be more than last inspection done date which was (" + DateTime.Parse(dt_Pos.Rows[0][1].ToString()).ToString("dd-MMM-yyyy") + ").";
                                return;
                            }
                        }
                    }
                }
            }
            //---------------------------

            DataTable dt;
            if (Session["Insp_Id"] == null)
            {
                string strDoneDate = "", strActualLoc = "", strInspValidityDate = "";
                //****Code To Set Status of Due Inspection as "Closed"
                DataTable dt09 = Inspection_Planning.SetClosedStatus_OfDueInsp(int.Parse(ddlinspection.SelectedValue), int.Parse(ddlvessel.SelectedValue));
                if (dt09.Rows.Count > 0)
                {
                    intInspDueId = int.Parse(dt09.Rows[0][0].ToString());
                    //******Code To Get Previous History
                    DataTable dt12 = Inspection_TravelSchedule.SelectInspectionDetailsByInspId(intInspDueId);
                    if (dt12.Rows.Count > 0)
                    {
                        strDoneDate = dt12.Rows[0]["DoneDt"].ToString();
                        strActualLoc = dt12.Rows[0]["ActualLocation"].ToString();
                        strInspValidityDate = dt12.Rows[0]["InspectionValidityDt"].ToString();
                    }
                    //**********************************
                }
                //****************************************************

                dt = Inspection_Planning.AdInspectionPlanning(0, int.Parse(ddlvessel.SelectedValue), InspectionNumber, int.Parse(ddlinspection.SelectedValue), DateTime.Parse(txtplandate.Text), rdblocation.SelectedValue, fromport, toport, ReqForInsp, intCreatedBy, intModifiedBy, "NEWPLAN", txtremark.Text, "Planned", strDoneDate, strActualLoc, strInspValidityDate, (chk_OnHold.Checked) ? "Y" : "N",ddlIsSire.SelectedValue,Int32.Parse(ddlVersions.SelectedValue));
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0][0].ToString() == "YES")
                    {
                        lblmessage.Text = "This vessel has already been planned.";
                        return;
                    }
                    else
                    {
                        ViewState["Id"] = dt.Rows[0][0].ToString();
                        Session.Add("Insp_Id", ViewState["Id"].ToString());
                    }
                }
            }
            else
            {
                if (Session["DueMode"] != null)
                {
                    if (Session["DueMode"].ToString() == "ShowId")
                    {
                        //******Code To Get Previous History
                        string strDoneDt = "", strActualLocation = "", strInspValidityDt = "";
                        DataTable dt1 = Inspection_TravelSchedule.SelectInspectionDetailsByInspId(int.Parse(Session["Insp_Id"].ToString()));
                        if (dt1.Rows.Count > 0)
                        {
                            strDoneDt = dt1.Rows[0]["DoneDt"].ToString();
                            strActualLocation = dt1.Rows[0]["ActualLocation"].ToString();
                            strInspValidityDt = dt1.Rows[0]["InspectionValidityDt"].ToString();
                        }
                        //**********************************
                        dt = Inspection_Planning.AdInspectionPlanning(0, int.Parse(ddlvessel.SelectedValue), InspectionNumber, int.Parse(ddlinspection.SelectedValue), DateTime.Parse(txtplandate.Text), rdblocation.SelectedValue, fromport, toport, ReqForInsp, intCreatedBy, intModifiedBy, "NEWPLAN", txtremark.Text, "Planned", strDoneDt, strActualLocation, strInspValidityDt, (chk_OnHold.Checked) ? "Y" : "N", ddlIsSire.SelectedValue, Int32.Parse(ddlVersions.SelectedValue));
                        if (dt.Rows.Count > 0)
                        {
                            if (dt.Rows[0][0].ToString() == "YES")
                            {
                                lblmessage.Text = "This vessel has already been planned.";
                                return;
                            }
                            else
                            {
                                ViewState["Id"] = dt.Rows[0][0].ToString();
                                //****Code To Set Status of Old Inspection as "Closed"
                                Inspection_Planning.SetClosedStatus_OfOldInsp(int.Parse(Session["Insp_Id"].ToString()));
                                //****************************************************
                                Session.Add("Insp_Id", ViewState["Id"].ToString());
                            }
                        }
                    }
                    else
                    {
                        dt = Inspection_Planning.AdInspectionPlanning(Inspection_Id, int.Parse(ddlvessel.SelectedValue), "", int.Parse(ddlinspection.SelectedValue), DateTime.Parse(txtplandate.Text), rdblocation.SelectedValue, fromport, toport, ReqForInsp, intCreatedBy, intModifiedBy, "MODIFY", txtremark.Text, "", "", "", "", (chk_OnHold.Checked) ? "Y" : "N", ddlIsSire.SelectedValue, Int32.Parse(ddlVersions.SelectedValue));
                    }
                }
            }
            //--------------------------
            if (dtg != null)
            {
                for (int j = 0; j <= dtg.Rows.Count - 1; j++)
                {
                    if (dtg.Rows[j]["SuptId"].ToString() != "" && dtg.Rows[j]["AttendingYesNo"].ToString()!="")//int.Parse(ViewState["Id"].ToString())
                    {
                        DataTable dtIns = Inspection_Planning.AddInspectors(0, Convert.ToInt32(Session["Insp_Id"].ToString()), int.Parse(dtg.Rows[j]["SuptId"].ToString()), int.Parse(dtg.Rows[j]["AttendingYesNo"].ToString()), DateTime.Now, 0, 0, DateTime.Now, "", 0, 0, "ADD");
                    }
                }
                BindSupGrid();
                ViewState["GridData"] = null;
            }
            //-------------------------
            btnsave.Enabled = false;
            btnadd.Enabled = false;
            //if (Session["Insp_Id"] != null)
            //{
            //    string strInspNum = "", strSuptEmail = "", strSptDetails = "";
            //    string suptid = "";
            //    DataTable dt23 = Inspection_Planning.AdInspectionPlanning(int.Parse(Session["Insp_Id"].ToString()), 0, "", 0, DateTime.Now, "", 0, 0, 0, 0, 0, "SELECT", "", "", "", "", "", "");
            //    if (dt23.Rows.Count > 0)
            //    {
            //        strInspNum = dt23.Rows[0]["InspectionNo"].ToString();
            //    }
            //    DataTable dt56 = Inspection_Planning.AddInspectors(0, int.Parse(Session["Insp_Id"].ToString()), 0, 0, DateTime.Now, 0, 0, DateTime.Now, "", 0, 0, "TRAVSCHED");
            //    foreach (DataRow dr in dt56.Rows)
            //    {
            //        if (dt56.Rows.Count > 0)
            //        {
            //            if (suptid == "")
            //            {
            //                suptid = dr["SuperintendentId"].ToString();
            //            }
            //            else
            //            {
            //                suptid = suptid + "," + dr["SuperintendentId"].ToString();
            //            }
            //        }
            //    }
            //    DataTable dt81 = Inspection_Planning.GetEmailIdofSuperintendent(suptid);
            //    foreach (DataRow dr1 in dt81.Rows)
            //    {
            //        if (dt81.Rows.Count > 0)
            //        {
            //            if (strSuptEmail == "")
            //            {
            //                strSuptEmail = dr1["Email"].ToString();
            //            }
            //            else
            //            {
            //                strSuptEmail = strSuptEmail + "," + dr1["Email"].ToString();
            //            }
            //        }
            //    }
            //    DataSet dsupt = Inspection_Planning.GetSuptDetails(int.Parse(Session["Insp_Id"].ToString()));
            //    strSptDetails = Inspection_Planning.ExportDatatable(Response, dsupt);
            //}
            lblmessage.Text = "Planning Saved Sucessfully.";
            Common.Execute_Procedures_Select_ByQuery("UPDATE T_INSPECTIONDUE SET Inspector='" + txtInspectorName.Text.Trim()  +  "' WHERE ID=" + Session["Insp_Id"].ToString());
            // Response.Redirect("AddEditInspection.aspx", false);  
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "refresh", "OpenInspectionPlanning();", true);
        }
        catch (Exception ex)
        {
            lblmessage.Text = ex.Message.ToString();    
        }
    }
    protected string GetInspectionNumber(int VesselId)
    {
        try
        {
            string inspNum = "0";
            DataTable Dt = Inspection_Planning.CreateInspectionNo(VesselId);
            if (Dt.Rows.Count == 0)
            {
                inspNum = "001";
            }
            else if (int.Parse(Dt.Rows[0][0].ToString()) <=8)
            {
                inspNum = "00" + (int.Parse(Dt.Rows[0][0].ToString()) + 1).ToString();
            }
            else if (int.Parse(Dt.Rows[0][0].ToString()) <=98)
            {
                inspNum = "0" + (int.Parse(Dt.Rows[0][0].ToString()) + 1).ToString();
            }
            else
            {
                inspNum = (int.Parse(Dt.Rows[0][0].ToString()) + 1).ToString();
            }
            string[] str;
            char ch ='-';
            str= ddlinspection.SelectedItem.Text.Split(ch) ;
            DataTable dt1 = Inspection_Planning.GetVesselCode(VesselId);
            DataTable dt_pankaj=Common.Execute_Procedures_Select_ByQuery("select replace(str(ISNULL(MAX(convert(int,right(InspectionNo,3))),0)+1,3),' ','0') as NewInspNo from t_inspectiondue where vesselid=" + VesselId.ToString()); 
            if(dt_pankaj.Rows.Count>0)
            {
                inspNum=dt_pankaj.Rows[0][0].ToString();
            }
            
            string InspectionNumber = dt1.Rows[0][0].ToString() + "/" + str[0].ToString().Trim() + "/" + inspNum;           //VSL Code + Year (4)+Month(2)+2 Digits(Number)
            return InspectionNumber;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void BindGrid()
    {
        try
        {
            DataTable Dt = Inspection_Planning.AddInspectors(0, int.Parse(ViewState["Id"].ToString()), 0, 0, DateTime.Now, 0, 0, DateTime.Now, "", 0, 0, "SELECT");
            this.grdinspector.DataSource = Dt;
            this.grdinspector.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void btnCloseExportPopup_OnClick(object sender, EventArgs e)
    {
        dvConfirmationBox.Visible = false;
    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlsup.SelectedIndex != 0)
            {
                int YsInspection;
                if (ddlattendinspection.SelectedValue == "No")
                {
                    YsInspection = 0;
                }
                else
                {
                    DataSet Ds = Inspection_Planning.CheckSupt(DateTime.Parse(txtplandate.Text.ToString()).ToString("MM/dd/yyyy"), Convert.ToInt32(ddlsup.SelectedValue));
                    if (Ds.Tables[0].Rows.Count > 0)
                    {
                        dvConfirmationBox.Visible = true;
                    }
                   
                    YsInspection = 1;
                }
                AddRow(Convert.ToInt32(ddlsup.SelectedValue), YsInspection, ddlsup.SelectedItem.Text, ddlattendinspection.SelectedItem.Text);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void grdinspector_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbl1 = new Label();
            lbl1 = (Label)e.Row.FindControl("lblyes");
            Label lbl = new Label();
            lbl = (Label)e.Row.FindControl("lbl");
            ImageButton imgbtndel = (ImageButton)e.Row.FindControl("imgbtn");
            try
            {
                Alerts.HANDLE_PLANNING_GRID(imgbtndel, Auth);
            }
            catch
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Default.aspx';", true);
            }
            if (temp == 1)
                imgbtndel.Enabled = false;
            else
                imgbtndel.Enabled = true;
        }
    }
    protected void grdinspector_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            if (ViewState["GridData"] != null)
            {
                Label lbl = new Label();
                lbl = (Label)grdinspector.Rows[e.RowIndex].FindControl("lbl");
                if (lbl.Text == "0")
                    RemoveRow(int.Parse(grdinspector.DataKeys[e.RowIndex].Value.ToString()));
                else
                {
                    DataTable dt = Inspection_Planning.AddInspectors(int.Parse(grdinspector.DataKeys[e.RowIndex].Value.ToString()), 0, 0, 0, DateTime.Now, 0, 0, DateTime.Now, "", 0, 0, "Delete");
                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0][0].ToString() == "No")
                        {
                            lblmessage.Text = "Can not delete Supt.,Travel Schedule has already been created.";
                            return;
                        }
                    }
                    RemoveRow(int.Parse(grdinspector.DataKeys[e.RowIndex].Value.ToString()));
                }
            }
            else
            {
                if (grdinspector.DataKeys[e.RowIndex].Value.ToString() != "")
                {

                    DataTable dt = Inspection_Planning.AddInspectors(int.Parse(grdinspector.DataKeys[e.RowIndex].Value.ToString()), 0, 0, 0, DateTime.Now, 0, 0, DateTime.Now, "", 0, 0, "Delete");
                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0][0].ToString() == "No")
                        {
                            lblmessage.Text = "Can not delete Supt.,Travel Schedule has already been created.";
                            return;
                        }
                    }
                    RemoveRow(int.Parse(grdinspector.DataKeys[e.RowIndex].Value.ToString()));
                }
            }
            lblmessage.Text = "Record Deleted Sucessfully.";
        }
        catch (Exception ex)
        {
            lblmessage.Text = ex.StackTrace.ToString();
        }
    }
    protected void rdbexternalenternal_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (rdbexternalenternal.SelectedIndex == 0)
            {
                chkrequest.Checked = false;
                chkrequest.Enabled = false;

             
                lblInsname.Visible = false;
                txtInspectorName.Visible = false;
                txtInspectorName.Text="";
            }
            else
            {
                chkrequest.Enabled = true;
                chkrequest.Checked = true;

                lblInsname.Visible = true;
                txtInspectorName.Visible = true;
            }
            BindInspection();
        }
        catch (Exception ex)
        {
            lblmessage.Text = ex.StackTrace.ToString();
        }
    }
    protected void chkrandom_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            BindInspection();
        }
        catch (Exception ex)
        {
            lblmessage.Text = ex.StackTrace.ToString();
        }
    }
    protected void vtn_VNotify_Click (object sender, EventArgs e)
    {
        if (Inspection_Id > 0)
        {
            try
            {
                DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM T_INSPECTIONDUE WHERE ID=" + Inspection_Id);
                string _InspectionNo = dt.Rows[0]["InspectionNo"].ToString();
                string FileContent = System.IO.File.ReadAllText(Server.MapPath("~/Transactions/Inspection_Template.xml"));
                FileContent = FileContent.Replace("#InspectionNo#", _InspectionNo).Replace("#PlanDate#", txtplandate.Text).Replace("#PortName#", txtfromport.Text);
                System.IO.File.WriteAllText(Server.MapPath("~/Transactions/Inspection.xml"), FileContent);
                SendMailInspection(_InspectionNo);
                lblmessage.Text = "Vessel Notified Successfully.";
            }
            catch ( Exception ex)
            {
                lblmessage.Text = "Unable to Notify. " + ex.Message;
            }
        }
    }
    public void SendMailInspection(string InspectionNo)
    {
        string[] VesselAddress;
        string[] CCAddress;
        DataTable dtAds = Common.Execute_Procedures_Select_ByQuery("SELECT email,groupemail FROM DBO.VESSEL WHERE VESSELCODE='" + InspectionNo.Substring(0,3) + "'");
        if (dtAds.Rows.Count > 0)
        {
            VesselAddress = new string[] { dtAds.Rows[0]["email"].ToString() };
            CCAddress = new string[] { dtAds.Rows[0]["groupemail"].ToString() };
        }
        else
        {
            VesselAddress = new string[0];
            CCAddress = new string[0];
        }

        string[] NoAddress = { };
        string Error="";
        string FilePath = Server.MapPath("~/Transactions/Inspection.xml");
        string ZipFile = Server.MapPath("~/TEMP/INS_" + InspectionNo.Replace("/", "-").ToString() + ".zip");

        if (System.IO.File.Exists(ZipFile))
        {
            System.IO.File.Delete(ZipFile);
        }

        using (ZipFile zip = new ZipFile())
        {
            zip.AddFile(FilePath);
            zip.Save(ZipFile);
        }

        string Message = "Dear Captain," + "<br/><br/>" +
                        "Subject inspection has been planned for your vessel." + "<br/><br/>" +
                        "Save the attached file on your desktop and import the same in VIMS module  under PMS system." + "<br/><br/>" +
                        "Thanks" + "<br/>" +
                        "+++++++" + "<br/>" +
                        "Shipsoft";
        SendMail.SendeMail("emanager@energiossolutions.com", "emanager@energiossolutions.com", VesselAddress, CCAddress, NoAddress, "Inspection Planning : " + InspectionNo, Message, out Error, ZipFile); 
    }
    //By Default for Design Default 4 rows are added.
    private void LoadDefaultRows()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;
        dt.Columns.Add("Id");
        dt.Columns.Add("Name");
        dt.Columns.Add("Attending");
        dt.Columns.Add("SuptId");
        dt.Columns.Add("AttendingYesNo");
        dt.Columns.Add("Status");
        for (int i = 0; i < 5; i++)
        {
            dr = dt.NewRow();
            dt.Rows.Add(dr);
            dt.Rows[dt.Rows.Count - 1][0] = "";
            dt.Rows[dt.Rows.Count - 1][1] = "";
            dt.Rows[dt.Rows.Count - 1][2] = "";
            dt.Rows[dt.Rows.Count - 1][3] = "";
            dt.Rows[dt.Rows.Count - 1][4] = "";
            dt.Rows[dt.Rows.Count - 1][5] = 0;
        }

        grdinspector.DataSource = dt;
        grdinspector.DataBind();
        ViewState["GridData"] = dt;
    }
    protected void AddRow1(int SupId, int AttInsp, string SupName, string AttInspection)
    {
        DataTable dt = null;
        //------------------------
        try
        {
            dt = (DataTable)ViewState["GridData"];
        }
        catch { }
        if (dt != null)
        {
            DataTable dt2;
            dt2 = new DataTable();
            dt2.Columns.Add("Id");
            DataColumn[] data = new DataColumn[1];
            data[0] = dt2.Columns[0];
            dt2.PrimaryKey = data;
            dt2.Columns.Add("Name");
            dt2.Columns.Add("Attending");
            dt2.Columns.Add("SuptId");
            dt2.Columns.Add("AttendingYesNo");
            dt2.Columns.Add("Status");
            int cnt;
            for (cnt = 0; cnt < dt.Rows.Count; cnt++)
            {
                if (dt.Rows[cnt]["SuptId"].ToString() != "")
                {
                    DataRow dd = dt2.NewRow();
                    dd["Id"] = dt.Rows[cnt]["Id"];
                    dd["Name"] = dt.Rows[cnt]["Name"];
                    dd["Attending"] = dt.Rows[cnt]["Attending"];
                    dd["SuptId"] = dt.Rows[cnt]["SuptId"];
                    dd["AttendingYesNo"] = dt.Rows[cnt]["AttendingYesNo"];
                    dd["Status"] = dt.Rows[cnt]["Status"];
                    dt2.Rows.Add(dd);
                }
            }
            dt = dt2;
        }
        if (dt == null)
        {
            dt = new DataTable();
            dt.Columns.Add("Id");
            DataColumn[] data = new DataColumn[1];
            data[0] = dt.Columns[0];
            dt.PrimaryKey = data;
            dt.Columns.Add("Name");
            dt.Columns.Add("Attending");
            dt.Columns.Add("SuptId");
            dt.Columns.Add("AttendingYesNo");
            dt.Columns.Add("Status");
        }
        //------------------------
        if (dt == null)
        {
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                dt.Rows[i]["Id"] = -i;
            }
            for (int i = dt.Rows.Count - 1; i > 0; i--)
            {
                dt.Rows[i]["Id"] = i + 1;
            }
        }
        //------------------------
        DataRow dr = dt.NewRow();
        dr["Id"] = dt.Rows.Count + 1;
        dr["Name"] = SupName;
        dr["Attending"] = AttInspection;
        dr["SuptId"] = SupId;
        dr["AttendingYesNo"] = AttInsp;
        dr["Status"] = 0;
        if (dr.RowState == DataRowState.Detached)
            dt.Rows.Add(dr);
        //------------------------
        ViewState["RowNO"] = dt.Rows.Count;
        ViewState["GridData"] = dt;
        int k = 5 - dt.Rows.Count;
        for (int i = 0; i < k; i++)
        {
            dr = dt.NewRow();
            dr["Id"] = dt.Rows.Count + 1;
            dr["Name"] = "";
            dr["Attending"] = "";
            dr["SuptId"] = "";
            dr["AttendingYesNo"] = "";
            dr["Status"] = 0;
            dt.Rows.Add(dr);
        }
        grdinspector.DataSource = dt;
        grdinspector.DataBind();
        //------------------------
    }
    protected void AddRow(int SupId, int AttInsp, string SupName, string AttInspection)
    {
        DataTable dt = null;
        //------------------------
        try
        {
            dt = (DataTable)ViewState["GridData"];
        }
        catch { }
        //------------------------
        if (dt != null)
        {
            int j;
            for (j = 0; j < dt.Rows.Count; j++)
            {
                if (SupId.ToString() == dt.Rows[j]["SuptId"].ToString())
                {
                    lblmessage.Text = "Superintendent already exists in this Inspection.";
                    return;
                }
            }
        }
        DataSet Ds = Inspection_Planning.CheckSupt(DateTime.Parse(txtplandate.Text.ToString()).ToString("MM/dd/yyyy"), SupId);
        if (Ds.Tables[0].Rows.Count > 0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "1aa5", "<script>CheckEccNo_0K();</script>", false);
            if (hidval.Value == "0")
            {
                return;
            }

        }
        else
        {
            if (dt != null)
            {
                DataTable dt2;
                dt2 = new DataTable();
                dt2.Columns.Add("Id");
                DataColumn[] data = new DataColumn[1];
                data[0] = dt2.Columns[0];
                dt2.PrimaryKey = data;
                dt2.Columns.Add("Name");
                dt2.Columns.Add("Attending");
                dt2.Columns.Add("SuptId");
                dt2.Columns.Add("AttendingYesNo");
                dt2.Columns.Add("Status");
                int cnt;
                for (cnt = 0; cnt < dt.Rows.Count; cnt++)
                {
                    if (dt.Rows[cnt]["SuptId"].ToString() != "")
                    {
                        DataRow dd = dt2.NewRow();
                        dd["Id"] = dt.Rows[cnt]["Id"];
                        dd["Name"] = dt.Rows[cnt]["Name"];
                        dd["Attending"] = dt.Rows[cnt]["Attending"];
                        dd["SuptId"] = dt.Rows[cnt]["SuptId"];
                        dd["AttendingYesNo"] = dt.Rows[cnt]["AttendingYesNo"];
                        dd["Status"] = dt.Rows[cnt]["Status"];
                        dt2.Rows.Add(dd);
                    }
                }
                dt = dt2;
            }
            if (dt == null)
            {
                dt = new DataTable();
                dt.Columns.Add("Id");
                DataColumn[] data = new DataColumn[1];
                data[0] = dt.Columns[0];
                dt.PrimaryKey = data;
                dt.Columns.Add("Name");
                dt.Columns.Add("Attending");
                dt.Columns.Add("SuptId");
                dt.Columns.Add("AttendingYesNo");
                dt.Columns.Add("Status");
            }
            //------------------------
            if (dt == null)
            {
                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {

                    dt.Rows[i]["Id"] = -i;
                }
                for (int i = dt.Rows.Count - 1; i > 0; i--)
                {
                    dt.Rows[i]["Id"] = i + 1;
                }
            }
            //------------------------
            DataRow dr = dt.NewRow();
            dr["Id"] = dt.Rows.Count + 1;
            dr["Name"] = SupName;
            dr["Attending"] = AttInspection;
            dr["SuptId"] = SupId;
            dr["AttendingYesNo"] = AttInsp;
            dr["Status"] = 0;
            if (dr.RowState == DataRowState.Detached)
                dt.Rows.Add(dr);
            //------------------------
            ViewState["RowNO"] = dt.Rows.Count;
            ViewState["GridData"] = dt;
            int k = 5 - dt.Rows.Count;
            for (int i = 0; i < k; i++)
            {
                dr = dt.NewRow();
                dr["Id"] = dt.Rows.Count + 1;
                dr["Name"] = "";
                dr["Attending"] = "";
                dr["SuptId"] = "";
                dr["AttendingYesNo"] = "";
                dr["Status"] = 0;
                dt.Rows.Add(dr);
            }
            grdinspector.DataSource = dt;
            grdinspector.DataBind();
        }
        //------------------------
    }
    protected void RemoveRow(int Id)
    {
        DataTable dt = null;
        //------------------------
        try
        {
            dt = (DataTable)ViewState["GridData"];
        }
        catch { }
        //------------------------
        if (dt != null)
        {
            DataRow dr = dt.Rows.Find(Id);
            dt.Rows.Remove(dr);
        }
        //------------------------
        if (dt.Rows.Count > 0)
        {

            ViewState["GridData"] = dt;
            int pp;

            DataRow dr1 = dt.NewRow();
            int checkId = int.Parse(dt.Rows[dt.Rows.Count - 1]["Id"].ToString());
            int k = 5 - dt.Rows.Count;

            for (int i = 0; i < k; i++)
            {
                dr1 = dt.NewRow();
                dr1["Id"] = checkId + 1;
                dr1["Name"] = "";
                dr1["Attending"] = "";
                dr1["SuptId"] = "";
                dr1["AttendingYesNo"] = "";
                dr1["Status"] = 0;
                dt.Rows.Add(dr1);
                checkId++;
            }
            grdinspector.DataSource = dt;
            grdinspector.DataBind();
        }
        else
        {
            LoadDefaultRows();
        }
        //------------------------
    }
    protected void rdblocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdblocation.SelectedValue == "Port")
            txttoport.Text = txtfromport.Text;
        else
            txttoport.Text = "";

  
    }
    protected void txtfromport_TextChanged(object sender, EventArgs e)
    {
        if (rdblocation.SelectedValue == "Port")
        {
            txttoport.Text = txtfromport.Text;
        }
    }
    protected void btnNo_Click(object sender, EventArgs e)
    {
        btnCloseExportPopup_OnClick(sender, e);
    }
    protected void btnYes_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlsup.SelectedIndex != 0)
            {
                int YsInspection;
                if (ddlattendinspection.SelectedValue == "No")
                {
                    YsInspection = 0;
                }
                else
                {
                    YsInspection = 1;
                }
                AddRow1(Convert.ToInt32(ddlsup.SelectedValue), YsInspection, ddlsup.SelectedItem.Text, ddlattendinspection.SelectedItem.Text);
                dvConfirmationBox.Visible = false;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void btn_Notify_Click(object sender, EventArgs e)
    {
        try
        {
            string strInspNo = "", strSuptMail = "", strSpdtDetail = "", spdtid = "";
            DataTable dt231 = Inspection_Planning.AdInspectionPlanning(int.Parse(Session["Insp_Id"].ToString()), 0, "", 0, DateTime.Now, "", 0, 0, 0, 0, 0, "SELECT", "", "", "", "", "", "","",0);
            if (dt231.Rows.Count > 0)
            {
                strInspNo = dt231.Rows[0]["InspectionNo"].ToString();
            }
            DataTable dt561 = Inspection_Planning.AddInspectors(0, int.Parse(Session["Insp_Id"].ToString()), 0, 0, DateTime.Now, 0, 0, DateTime.Now, "", 0, 0, "TRAVSCHED");
            foreach (DataRow dr4 in dt561.Rows)
            {
                if (dt561.Rows.Count > 0)
                {
                    if (spdtid == "")
                    {
                        spdtid = dr4["SuperintendentId"].ToString();
                    }
                    else
                    {
                        spdtid = spdtid + "," + dr4["SuperintendentId"].ToString();
                    }
                }
            }
            DataTable dt811 = Inspection_Planning.GetEmailIdofSuperintendent(spdtid);
            if (dt811.Rows.Count > 0)
            {
                foreach (DataRow dr5 in dt811.Rows)
                {
                    if (dt811.Rows.Count > 0)
                    {
                        if (strSuptMail == "")
                        {
                            strSuptMail = dr5["Email"].ToString();
                        }
                        else
                        {
                            strSuptMail = strSuptMail + "," + dr5["Email"].ToString();
                        }
                    }
                }
            }
            else
            {
                lblmessage.Text = "Mail cannot be send as no superintendent is assigned.";
                return;
            }
            DataSet dtsupt = Inspection_Planning.GetSuptDetails(int.Parse(Session["Insp_Id"].ToString()));
            strSpdtDetail = Inspection_Planning.ExportDatatable(Response, dtsupt);
            SendMail.Mail("Planned", "Planned Inspection", strInspNo, rdblocation.SelectedValue, txtfromport.Text, txttoport.Text, txtplandate.Text, strSpdtDetail, txtremark.Text, strSuptMail);
            lblmessage.Text = "Mail Sent Successfully.";
        }
        catch (Exception ex) { lblmessage.Text = ex.Message.ToString(); }
    }
    protected void ddlinspection_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindVersionsDDl();
        if (ddlinspection.SelectedIndex <= 0)
        {
            ddlIsSire.SelectedIndex = 0;
            lblSire.Visible = false;
            ddlIsSire.Visible = false;
        }
        else
        {
            char[] sep = { '-' }; 
            string[] Codes =ddlinspection.SelectedItem.Text.Split(sep); 
            if(Codes.Length>1)
            {
                DataTable dt = Budget.getTable("select code from m_inspectiongroup Where id in(select InspectionGroup from m_Inspection Where CODE='" + Codes[0].Trim() + "')").Tables[0];
                if (dt.Rows.Count <= 0)
                {
                    lblSire.Visible = false;
                    ddlIsSire.Visible = false;
                }
                else
                {
                    if (dt.Rows[0][0].ToString().ToUpper() == "SIRE")
                    {
                        lblSire.Visible = true;
                        ddlIsSire.Visible = true;
                    }
                    else
                    {
                        lblSire.Visible = false;
                        ddlIsSire.Visible = false;
                        ddlIsSire.SelectedIndex = 0;  
                    }
                }
            }
        }
    }
    protected void GoBackToPlan(object sender,EventArgs e)
    {
        Response.Redirect("../Vetting/VetttingPlannerReport.aspx?planner=yes");
    }
    protected void btnCancelPlan_Click(object sender, EventArgs e)
    {
        Common.Execute_Procedures_Select_ByQuery("DELETE FROM dbo.t_inspectiondue where id=" + Session["Insp_Id"].ToString());
        Session["Insp_Id"] = null;
        Session["DueMode"] = null;
        Session["Mode"] = null;
        Response.Redirect("InspectionSearch.aspx");
    }
}