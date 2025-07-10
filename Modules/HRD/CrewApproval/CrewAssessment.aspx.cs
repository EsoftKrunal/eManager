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
using System.IO;
using System.Text;
using System.Xml;
using System.Collections.Generic;

public partial class CrewApproval_CrewAssessment : System.Web.UI.Page
{
    Authority Auth;
    string Mode;

    public int Sel_VesselId
    {
        get { return Common.CastAsInt32(ViewState["Sel_VesselId"]); }
        set { ViewState["Sel_VesselId"] = value; }
    }
    public string Sel_CrewNumber
    {
        get { return Convert.ToString(ViewState["Sel_CrewNumber"]); }
        set { ViewState["Sel_CrewNumber"] = value; }
    }

    protected void bindVessels()
    {
        DataSet ds = Budget.getTable("select VesselID,VesselName as Name from dbo.Vessel where VesselStatusid<>2  ORDER BY VESSELNAME");
        ddl_VW_Vessel.DataSource = ds.Tables[0];
        ddl_VW_Vessel.DataValueField = "VesselId";
        ddl_VW_Vessel.DataTextField = "Name";
        ddl_VW_Vessel.DataBind();
        ddl_VW_Vessel.Items.Insert(0, new ListItem("<Select>", "0"));
    }
    protected void bindRanks()
    {
        DataSet ds = Budget.getTable("SELECT RankId, RankCode FROM [Rank] Order by RankCode");
        ddl_Rank.DataSource = ds.Tables[0];
        ddl_Rank.DataValueField = "RankId";
        ddl_Rank.DataTextField = "RankCode";
        ddl_Rank.DataBind();
        ddl_Rank.Items.Insert(0, new ListItem("<Select>", "0"));
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        // AUTOIMPORTCREW_FOR_CONTRACTBONUS -- THIS PROC CALLS EVERYDAY USING A JOB TO ADD DATA IN THE TABLE ---

        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 12);
        if (chpageauth <= 0)
        {
            Response.Redirect("../AuthorityError.aspx");
        }
        //**********
        ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 1);
        OBJ.Invoke();
        Session["Authority"] = OBJ.Authority;
        Auth = OBJ.Authority;
        try
        {
            if (Session["VMode"] != null)
            {
                Mode = Session["VMode"].ToString();
            }  
        }
        catch { Mode = "New"; }

        if (!(IsPostBack))
        {
            bindVessels();
            bindRanks();
            VW_Bind_grid();
        }
    }
    // vessel wise --------------------------
    protected void ddlVessel_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        VW_Bind_grid();
    }
    protected void btnMail_Click(object sender, EventArgs e)
    {
        int CrewBonusId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        int VesselId = Common.CastAsInt32(((ImageButton)sender).CssClass);
        int RankId = Common.CastAsInt32(((ImageButton)sender).Attributes["RankId"]); 
              

        string SQL = "SELECT email as VesselEmail,groupemail AS TechnincalGroupEmail,groupemail1 AS CrewGroupEmail,(SELECT Email FROM DBO.UserLogin WHERE LoginId = V.TechSupdt ) AS TechSupdt,  " +
                     "(SELECT Email FROM DBO.UserLogin WHERE LoginId = V.MarineSupdt ) AS MarineSupdt,  " +
                     "(SELECT Email FROM DBO.UserLogin WHERE LoginId = V.FleetManager ) AS FleetManager,  " +
                     "OwnerRepEmail,ChartererEmail,  " +
                     "(SELECT Email FROM DBO.UserLogin WHERE LoginId = V.TechAssistant ) AS TechAssistant,  " +
                     "(SELECT Email FROM DBO.UserLogin WHERE LoginId = V.MarineAssistant ) AS MarineAssistant, " +
                     "(SELECT Email FROM DBO.UserLogin WHERE LoginId = V.SPA ) AS SPA,  " +
                     "(SELECT Email FROM DBO.UserLogin WHERE LoginId = V.AcctOfficer ) AS AcctOfficer " +  
                     "FROM DBO.vessel V WHERE V.VesselId = " + VesselId;
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);

        if (dt.Rows.Count > 0)
        {
            string [] Emails = {dt.Rows[0]["OwnerRepEmail"].ToString(),dt.Rows[0]["ChartererEmail"].ToString(),dt.Rows[0]["TechSupdt"].ToString(),dt.Rows[0]["FleetManager"].ToString(), dt.Rows[0]["MarineSupdt"].ToString()};

            int Max = 4;
            if (RankId == 1 || RankId == 169 || RankId == 60 || RankId == 2)
            {
                Max = 5;
            }

            for (int i=0; i<=Max-1; i++)
            {
                string eMail = Emails[i];
                if (eMail.Trim() != "")
                {
                    Common.Set_Procedures("insert_CrewContractBonusDetails");
                    Common.Set_ParameterLength(3);
                    Common.Set_Parameters(new MyParameter("@CrewBonusId", CrewBonusId),
                        new MyParameter("@MailUserMode", i+1),
                        new MyParameter("@MailAddress", eMail));
                    DataSet ds = new DataSet();
                    if (Common.Execute_Procedures_IUD_CMS(ds))
                    {
                        if (ds.Tables[0].Rows[0]["AckRecd"].ToString() != "Y")
                        {
                            SendMail(CrewBonusId, i + 1);
                        }
                        SendMailSelf(CrewBonusId);
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Email sent successfully.');window.close();", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "failure", "alert('Unable To Send Email. Error : " + Common.getLastError() + "');", true);
                    }
                }                 
            }
            VW_Bind_grid();
        }
    }
    private void VW_Bind_grid()
    {
        string sql = "SELECT CCB.CREWBONUSID,CCB.CREWID,CPD.CREWNUMBER,CCB.CONTRACTID,CCB.ContractRefNumber,CPD.FIRSTNAME + ' ' +CPD.MIDDLENAME + ' ' +CPD.LASTNAME AS CREWNAME ,CCB.RANKID,RANKCODE,CCB.VESSELID,V.VESSELNAME, BonusApproved,ccb.SignOnDate,ccb.SignOffDate, " +
                     "(SELECT TOP 1 MailSent FROM CrewContractBonusDetails WHERE CrewBonusId = CCB.CREWBONUSID AND MailSent = 'Y') AS IsMailSent, " +
                     "(SELECT Grade FROM CrewContractBonusDetails WHERE CrewBonusId = CCB.CREWBONUSID AND MailUserMode = 1) As OwnerRep, " +
                     "(SELECT Grade FROM CrewContractBonusDetails WHERE CrewBonusId = CCB.CREWBONUSID AND MailUserMode = 2) As Charterer,  " +
                     "(SELECT Grade FROM CrewContractBonusDetails WHERE CrewBonusId = CCB.CREWBONUSID AND MailUserMode = 3) As TechSupdt,  " +
                     "(SELECT Grade FROM CrewContractBonusDetails WHERE CrewBonusId = CCB.CREWBONUSID AND MailUserMode = 4) As FleetMgr,  " +
                     "(SELECT Grade FROM CrewContractBonusDetails WHERE CrewBonusId = CCB.CREWBONUSID AND MailUserMode = 5) As MarineSupdt, " +
                     "(SELECT top 1 SENTON FROM CrewContractBonusDetails WHERE CrewBonusId = CCB.CREWBONUSID AND SENTON IS NOT NULL ORDER BY SENTON) As NotifyDt, " +
                     "(SELECT AssMgntID from tbl_Assessment WHERE CREWBONUSID=CCB.CREWBONUSID and status=2) AS PeapId," +
                     "V.TechSupdt AS TechSupdtId,V.MarineSupdt AS MarineSupdtId,V.FleetManager AS FleetManagerId, V.OwnerId " +
                     "FROM CREWCONTRACTBONUSMASTER CCB " + 
                     "INNER JOIN CREWPERSONALDETAILS CPD ON CCB.CREWID=CPD.CREWID " + 
                     "INNER JOIN RANK R ON CCB.RANKID=R.RANKID " + 
                     "INNER JOIN VESSEL V ON V.VESSELID=CCB.VESSELID " + 
                     "WHERE STATUS='A' ";
        string Where = "";
        if (ddl_VW_Vessel.SelectedIndex > 0)
           Where += " And CCB.Vesselid=" + ddl_VW_Vessel.SelectedValue;
        if (ddl_Rank.SelectedIndex > 0)
            Where += " And CCB.RANKID=" + ddl_Rank.SelectedValue;
        if (ddl_Status.SelectedIndex > 0)
        {
            if(ddl_Status.SelectedValue.Trim() == "C")
                Where += " And CCB.BonusApproved IS NOT NULL ";
            else
                Where += " And CCB.BonusApproved IS NULL ";
        }
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql + Where + " Order By V.VESSELNAME,CPD.CREWNUMBER");

        rprData1.DataSource = dt; 
        rprData1.DataBind();

        lblRecCount.Text = "Total Records : " + dt.Rows.Count;


    }
    //protected void btnAddCrew_Click(object sender, EventArgs e)
    //{

    //}

    public void SendMail(int CrewBonusId, int MailUserMode)
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("SELECT *,(select crewnumber from crewpersonaldetails cpd where cpd.crewid=ccm.Crewid) as CrewNumber,(select VesselCode from vessel v where v.vesselid=ccm.vesselid) as VesselCode FROM [dbo].[CrewContractBonusDetails] ccb inner join [dbo].[CrewContractBonusmaster] ccm on ccm.CrewBonusId=ccb.CrewBonusId WHERE ccb.[CrewBonusId]= " + CrewBonusId + " AND ccb.[MailUserMode] = " + MailUserMode);

        string ToAddress = dt.Rows[0]["MailAddress"].ToString();
        char[] sep={',',';'};
        string[] Adds=ToAddress.Split(sep);
        List<string> lst_Adds = new List<string>();
        foreach (string s in Adds)
        {
            try
            {
                System.Net.Mail.MailAddress ma = new System.Net.Mail.MailAddress(s);
                lst_Adds.Add(s);
            }
            catch { }
        }
        
        string[] CCAdds = { };
        string[] BCCAdds = { };
        //string Link = "http://localhost:54817/Web/Public/AssessmentMail.aspx?key=" + dt.Rows[0]["GUID"].ToString();
        //string Link = "http://emanagershore.energiosmaritime.com/cms/Public/AssessmentMail.aspx?key=" + dt.Rows[0]["GUID"].ToString();
        string Link = ConfigurationManager.AppSettings["AssessmentMail"].ToString() + dt.Rows[0]["GUID"].ToString();
        string CrewNumber=dt.Rows[0]["CrewNumber"].ToString();
        string VesselCode=dt.Rows[0]["VesselCode"].ToString();
        
        ////----------------------
        String Subject = "Crew Assessment - Crew # : " + CrewNumber + " , Vessel : " + VesselCode;
        String MailBody;
        //MailBody = "Employee Name : " + lblEmpName.Text + " || Position:" + lblDesignation.Text + " || Department: " + lblDepartment.Text + "";
        //MailBody = MailBody + "<br><br>Leave Type: " + lblLeaveType.Text + " || Leave Period: ( " + lblLeaveFrom.Text + " to " + lblLeaveTo.Text + ") || Duration: " + lblLeaveDays.Text + " || Total Office Absence: " + lblAbsentDays.Text;
        //MailBody = MailBody + "<br><br>Approver Comments : " + ApproverComments + "";
        
        MailBody = "<br><br>The subject crew member is due for relief.";
        MailBody =  MailBody + "<br><br>Kindly access the link below and submit your grading and remarks if any.";
        MailBody = MailBody + "<br><br><a href='" + Link + "' target='_blank' >" + Link + "</a>";
        MailBody = MailBody + "<br><br>Thanks & Best Regards";
        //MailBody = MailBody + "<br>" + UppercaseWords(dtApprover.Rows[0]["Empname"].ToString());
        //MailBody = MailBody + "<br>" + dtApprover.Rows[0]["POSITION"].ToString() + "<br><font color=000080 size=2 face=Century Gothic><strong>" + dtApprover.Rows[0]["EMAIL"].ToString() + "</strong></font>";
        ////------------------
        string ErrMsg = "";
        string AttachmentFilePath = "";
        SendEmail.SendeMail(Common.CastAsInt32(Session["LoginId"].ToString()), "emanager@energiossolutions.com", "emanager@energiossolutions.com", lst_Adds.ToArray(), CCAdds, BCCAdds, Subject, MailBody, out ErrMsg, AttachmentFilePath);
    }
    public void SendMailSelf(int CrewBonusId)
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("SELECT *,(select crewnumber from crewpersonaldetails cpd where cpd.crewid=ccm.Crewid) as CrewNumber,(select VesselCode from vessel v where v.vesselid=ccm.vesselid) as VesselCode FROM [dbo].[CrewContractBonusmaster] ccm WHERE ccm.[CrewBonusId]= " + CrewBonusId);
        DataTable dtSelfMail=Common.Execute_Procedures_Select_ByQueryCMS("SELECT email FROM userlogin where LoginId=" + Session["LoginId"].ToString());
        if(dtSelfMail.Rows.Count>0)
        {
            string ToAddress = dtSelfMail.Rows[0][0].ToString();
            string[] ToAdds = { ToAddress };
            string[] CCAdds = { };
            string[] BCCAdds = { };
            string CrewNumber = dt.Rows[0]["CrewNumber"].ToString();
            string VesselCode = dt.Rows[0]["VesselCode"].ToString();
            ////----------------------
            String Subject = "Crew Performance Bonus Approval - Crew # : " + CrewNumber + " , Vessel : " + VesselCode;
            String MailBody;
        
            MailBody = "<br><br>The subject crew member is due for relief and eligible for performance bonus.";
            MailBody = MailBody + "<br><br>";
            MailBody = MailBody + "<br><br>Thanks & Best Regards";
            ////------------------
            string ErrMsg = "";
            string AttachmentFilePath = "";
            SendEmail.SendeMail(Common.CastAsInt32(Session["LoginId"].ToString()), ToAddress, ToAddress, ToAdds, CCAdds, BCCAdds, Subject, MailBody, out ErrMsg, AttachmentFilePath);
        }
    }
    protected void btnShowRemarks_Click(object sender, EventArgs e)
    {
        int CrewBonusId = Common.CastAsInt32(hfCBId.Value);
        int MailUserMode = Common.CastAsInt32(hfMUM.Value);

        string SQL = "SELECT ACKON,MailAddress,Comments FROM CrewContractBonusDetails WHERE CrewBonusId=" + CrewBonusId + " AND MailUserMode=" + MailUserMode;
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(SQL);
        lblEmail.Text = dt.Rows[0]["MailAddress"].ToString();
        txtRemarks.Text = dt.Rows[0]["Comments"].ToString();
        lblCommentDate.Text = Common.ToDateString(dt.Rows[0]["ACKON"]);

        dv_ViewRemarks.Visible = true;
    }
    protected void btn_Close_Click(object sender, EventArgs e)
    {
        hfCBId.Value = "";
        hfMUM.Value = "";
        lblEmail.Text = "";
        txtRemarks.Text = "";
        dv_ViewRemarks.Visible = false;
    }
    protected void btnClosure_Click(object sender, EventArgs e)
    {
        string CrewBonusId = ((ImageButton)sender).CommandArgument;
        hfCrewBonusId.Value = CrewBonusId;

        lblCrewno.Text = ((ImageButton)sender).Attributes["CREWNO"].ToString();
        lblCrewName.Text = ((ImageButton)sender).Attributes["CREWNAME"].ToString();
        lblVessel.Text = ((ImageButton)sender).Attributes["VESSEL"].ToString();

        DataTable dt=Common.Execute_Procedures_Select_ByQueryCMS("SELECT * FROM CrewContractBonusDetails WHERE CREWBONUSID=" + CrewBonusId + " AND GRADE IN ('C','D')");
        if (dt.Rows.Count > 0)
        {
            //rdoApprove.SelectedValue = "N";
            
            
            
            
            //lblBonusStatus.Text = "Declined";
            //hfBonusStatus.Value = "N";
            //txtBonusAmt.Text = "0";
            //txtBonusAmt.Enabled = false;



            txtOfficeComments.Text = "Crew grading is not sufficient for bonus approval.";
            ViewState["BonusStatus"] = "N";
        }
        else
        {
            //rdoApprove.SelectedValue = "Y";
            



            //lblBonusStatus.Text = "Approved";
            //hfBonusStatus.Value = "Y";
            //txtBonusAmt.Text = "";
            //txtBonusAmt.Enabled = true;




            ViewState["BonusStatus"] = "Y";
            DataTable dtSignOn = Common.Execute_Procedures_Select_ByQueryCMS("select * from crewcontractbonusmaster where crewbonusid=" + CrewBonusId);
            if (dtSignOn.Rows.Count > 0)
            {
                lblSignOnDt.Text = Common.ToDateString(dtSignOn.Rows[0]["SignOnDate"]);
                txtSignOffDate.Text = Common.ToDateString(dtSignOn.Rows[0]["SignOffDate"]);
                CalculateBonus(CrewBonusId);
            }
        }
        dv_Closure.Visible = true;
    }
    protected void txtSignOffDate_OnTextChanged(object sender, EventArgs e)
    {
        if (txtSignOffDate.Text.Trim() != "")
        {
            CalculateBonus(hfCrewBonusId.Value);
        }
    }
    public void CalculateBonus(string CrewBonusId)
    {
        
        //if (ViewState["BonusStatus"].ToString() == "Y")
        //{
        //    DataTable dtContract = Common.Execute_Procedures_Select_ByQueryCMS("SELECT EndDate FROM CrewContractHeader WHERE ContractId = (SELECT ContractId FROM CrewContractBonusMaster WHERE CrewBonusId = " + CrewBonusId + " )");
        //    DateTime ContractEndDate = DateTime.Parse(dtContract.Rows[0]["EndDate"].ToString()).AddDays(-15);

        //    if (ContractEndDate <= DateTime.Parse(txtSignOffDate.Text))
        //    {

        //        lblBonusStatus.Text = "Approved";
        //        hfBonusStatus.Value = "Y";
        //        txtBonusAmt.Text = "";
        //        txtBonusAmt.Enabled = true;


        //        txtOfficeComments.Text = "";

        //        DataTable dtBonusCalc = new DataTable();
        //        dtBonusCalc.Columns.Add(new DataColumn("StartPeriod", typeof(DateTime)));
        //        dtBonusCalc.Columns.Add(new DataColumn("EndPeriod", typeof(DateTime)));
        //        dtBonusCalc.Columns.Add(new DataColumn("VesselAge", typeof(Int16)));
        //        dtBonusCalc.Columns.Add(new DataColumn("BonusAmtMonthly", typeof(float)));
        //        dtBonusCalc.Columns.Add(new DataColumn("CalcBonus", typeof(float)));

        //        float BonusAmt = 0;
        //        List<float> Bonus = new List<float>();
        //        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("SELECT YEARBUILT,ISBONUSAPPLICABLE FROM VESSEL WHERE VESSELID IN (SELECT VESSELID FROM CrewContractBonusMASTER M WHERE M.CREWBONUSID=" + CrewBonusId + ")");
        //        if (dt.Rows.Count > 0)
        //        {
        //            int YearBulit = Common.CastAsInt32(dt.Rows[0]["YearBuilt"]);
        //            //lblBuiltYear.Text = YearBulit.ToString();

        //            if (YearBulit > 0)
        //            {
        //                DateTime DtStart = DateTime.Parse(lblSignOnDt.Text);
        //                DateTime DtEnd = DateTime.Parse(txtSignOffDate.Text);
        //                List<DateTime> Slots = new List<DateTime>();

        //                while (true)
        //                {
        //                    Slots.Add(DtStart);
        //                    DtStart = new DateTime(DtStart.Year, DtStart.Month, 1).AddMonths(1);
        //                    if (DtStart > DtEnd)
        //                    {
        //                        Slots.Add(DtEnd.AddDays(1));
        //                        break;
        //                    }
        //                }

        //                for (int i = 0; i < Slots.Count - 1; i++)
        //                {
        //                    float MonthBonus = 0;
        //                    DataRow dr = dtBonusCalc.NewRow();
        //                    float tmp = 0;
        //                    DateTime dtCalc = Slots[i];
        //                    DateTime dtNext = Slots[i + 1].AddDays(-1);
        //                    string VesselColumnName = "";
        //                    int DiffYear = dtCalc.Year - YearBulit;
        //                    if (DiffYear > 0 && DiffYear <= 5)
        //                    {
        //                        VesselColumnName = "Bonus1";
        //                    }
        //                    else if (DiffYear > 5 && DiffYear <= 10)
        //                    {
        //                        VesselColumnName = "Bonus2";
        //                    }
        //                    else if (DiffYear > 10)
        //                    {
        //                        VesselColumnName = "Bonus3";
        //                    }
        //                    DataTable dtBonus = Common.Execute_Procedures_Select_ByQueryCMS("SELECT TOP 1 " + VesselColumnName + " AS BONUSAMT FROM PERFORMANCEBONUS WHERE APPDATE<='" + dtCalc.ToString("dd-MMM-yyyy") + "' order by APPDATE desc");
        //                    if (dtBonus.Rows.Count > 0)
        //                    {
        //                        MonthBonus = float.Parse(dtBonus.Rows[0][0].ToString());
        //                        //tmp = (float)Math.Round((decimal)(MonthBonus / DateTime.DaysInMonth(dtCalc.Year, dtCalc.Month)) * (dtNext.Subtract(dtCalc).Days + 1), 2);
        //                        tmp = (float)Math.Round((decimal)(MonthBonus / 30) * (dtNext.Subtract(dtCalc).Days + 1), 2);
        //                        if (tmp > MonthBonus)
        //                        {
        //                            tmp = MonthBonus;
        //                        }
        //                    }
        //                    Bonus.Add(tmp);
        //                    dr["StartPeriod"] = dtCalc;
        //                    dr["EndPeriod"] = dtNext;
        //                    dr["VesselAge"] = DiffYear;
        //                    dr["BonusAmtMonthly"] = MonthBonus;
        //                    dr["CalcBonus"] = tmp;
        //                    dtBonusCalc.Rows.Add(dr);
        //                }
        //            }
        //        }
        //        for (int i = 0; i <= Bonus.Count - 1; i++)
        //        {
        //            BonusAmt += Bonus[i];
        //        }
        //        txtBonusAmt.Text = string.Format("{0:0.00}", BonusAmt);

        //        rptCalcBonus.DataSource = dtBonusCalc;
        //        rptCalcBonus.DataBind();

        //    }
        //    else
        //    {
        //        lblBonusStatus.Text = "Declined";
        //        hfBonusStatus.Value = "N";
        //        txtBonusAmt.Text = "0";
        //        txtBonusAmt.Enabled = false;

        //        txtOfficeComments.Text = "Crew signed off prior to the contract end date.";
        //    }
        //}
    }
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        if (txtSignOffDate.Text.Trim() == "")
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "failure", "alert('Please enter Expected disembarkation date.');", true);
            return;
        }
        try
        {
            //string SQL = "UPDATE CrewContractBonusMaster SET SignOffDate='" + txtSignOffDate.Text + "',BonusApproved= '" + hfBonusStatus.Value.Trim() + "',BonusAmount =" + txtBonusAmt.Text.Trim() + ", OfficeCommentBy = " + Session["loginid"].ToString() + ",OfficeCommentOn = getdate(), OfficeComments = '" + txtOfficeComments.Text.Trim() + "' WHERE CrewBonusId = " + hfCrewBonusId.Value.Trim();
            string SQL = "UPDATE CrewContractBonusMaster SET SignOffDate='" + txtSignOffDate.Text + "',BonusApproved= 'N', OfficeCommentBy = " + Session["loginid"].ToString() + ",OfficeCommentOn = getdate(), OfficeComments = '" + txtOfficeComments.Text.Trim() + "' WHERE CrewBonusId = " + hfCrewBonusId.Value.Trim();
            Common.Execute_Procedures_Select_ByQueryCMS(SQL);

            Common.Execute_Procedures_Select_ByQueryCMS("Delete From CrewContractBonusCalculation Where CrewBonusId=" + hfCrewBonusId.Value.Trim());

            foreach (RepeaterItem rpt in rptCalcBonus.Items)
            {
                Label lblSD = (Label)rpt.FindControl("lblSD");
                Label lblED = (Label)rpt.FindControl("lblED");
                Label lblVA = (Label)rpt.FindControl("lblVA");
                Label lblBA = (Label)rpt.FindControl("lblBA");
                Label lblCB = (Label)rpt.FindControl("lblCB");

                
                string insert_SQL = "INSERT INTO CrewContractBonusCalculation (CrewBonusId, StartPeriod, EndPeriod, VesselAge, BonusAmt, PayableAmt) " +
                                    "VALUES (" + hfCrewBonusId.Value.Trim() + ", '" + lblSD.Text.Trim() + "', '" + lblED.Text.Trim() + "'," + lblVA.Text.Trim() + "," + lblBA.Text.Trim() + "," + lblCB.Text.Trim() + ")";
                Common.Execute_Procedures_Select_ByQueryCMS(insert_SQL);
            }


            ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Record saved successfully.');", true);
            dv_Closure.Visible = false;
            VW_Bind_grid();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "failure", "alert('Unable To save record. Error : " + ex.Message + "');", true);
        }
         
    }
    protected void btn_Closure_Close_Click(object sender, EventArgs e)
    {
        lblCrewno.Text = "";
        lblCrewName.Text = "";
        lblVessel.Text = "";

        
        //lblBonusStatus.Text = "";
        //hfBonusStatus.Value = "";
        //txtBonusAmt.Text = "";


        txtOfficeComments.Text = "";
        hfCrewBonusId.Value = "";
        rptCalcBonus.DataSource = null;
        rptCalcBonus.DataBind();
        dv_Closure.Visible = false;
    }
    //protected void rdoApprove_OnSelectedIndexChanged(object sender, EventArgs e)
    //{

    //    txtBonusAmt.Enabled = rdoApprove.SelectedIndex == 0;
    //    if (rdoApprove.SelectedIndex == 1)
    //        txtBonusAmt.Text = "";

    //}

    protected void imgOwnerRepEdit_Click(object sender, EventArgs e)
    {
        int CrewBonusId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);

        string SQL = "SELECT [GUID] FROM CrewContractBonusDetails WHERE CrewBonusId = " + CrewBonusId + " AND MailUserMode = 1 ";
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(SQL);
        string path = ConfigurationManager.AppSettings["AssessmentMail"].ToString();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "owner", "window.open('"+path + dt.Rows[0]["GUID"].ToString() + "', '_blank', '', '');", true);
    }
    protected void imgTechSupdtEdit_Click(object sender, EventArgs e)
    {
        int CrewBonusId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);

        string SQL = "SELECT [GUID] FROM CrewContractBonusDetails WHERE CrewBonusId = " + CrewBonusId + " AND MailUserMode = 3 ";
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(SQL);
        string path = ConfigurationManager.AppSettings["AssessmentMail"].ToString();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "tech", "window.open('" + path + dt.Rows[0]["GUID"].ToString() + "', '_blank', '', '');", true);        
    }
    protected void imgFleetMgrEdit_Click(object sender, EventArgs e)
    {
        int CrewBonusId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);

        string SQL = "SELECT [GUID] FROM CrewContractBonusDetails WHERE CrewBonusId = " + CrewBonusId + " AND MailUserMode = 4 ";
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(SQL);
        string path = ConfigurationManager.AppSettings["AssessmentMail"].ToString();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "tech", "window.open('" + path + dt.Rows[0]["GUID"].ToString() + "', '_blank', '', '');", true); 
    }
    protected void imgMarineSupdtEdit_Click(object sender, EventArgs e)
    {
        int CrewBonusId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);

        string SQL = "SELECT [GUID] FROM CrewContractBonusDetails WHERE CrewBonusId = " + CrewBonusId + " AND MailUserMode = 5 ";
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(SQL);
        string path = ConfigurationManager.AppSettings["AssessmentMail"].ToString();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "tech", "window.open('" +path + dt.Rows[0]["GUID"].ToString() + "', '_blank', '', '');", true); 
    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        VW_Bind_grid();
    }

    // Add Peap -------------------------------------------------------------
    protected void imgAddPeap_OnClick(object sender, EventArgs e)
    {
        ImageButton img = (ImageButton)sender;
        int CREWBONUSID = Common.CastAsInt32(img.CommandArgument);
        if (CREWBONUSID > 0)
        {
            DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("exec dbo.StartPEAP " + CREWBONUSID);
            if (dt.Rows.Count > 0)
            {
                int PeapId = Common.CastAsInt32(dt.Rows[0][0]);
                string VesselCode = Convert.ToString(dt.Rows[0][1]);
                string Location = Convert.ToString(dt.Rows[0][2]);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", "window.open('../CrewAppraisal/AddPeap.aspx?PeapID=" + PeapId + "&VesselCode=" + VesselCode + "&Location=" + Location + "','');", true);
            }
        }
    }
    //protected void btnClosePopup_Click(object sender, EventArgs e)
    //{
    //    dvAddPeapPopup.Visible = false;
    //    Sel_VesselId = 0;
    //    Sel_CrewNumber = "";
    //}
    //protected void ddlOccasion_AP_OnSelectedIndexChanged(object sender, EventArgs e)
    //{
    //    dvAddPeapPopup.Visible = false;
    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", "window.open('../CrewAppraisal/AddPeap.aspx?VesselID=" + Sel_VesselId + "&Occasion=" + ddlOccasion_AP.SelectedValue + "&CrewNumber=" + Sel_CrewNumber + "')", true);
    //    Sel_VesselId = 0;
    //    Sel_CrewNumber = "";
    //}
    
}
