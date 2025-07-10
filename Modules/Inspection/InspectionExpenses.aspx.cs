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
public partial class Transactions_InspectionExpenses : System.Web.UI.Page
{
    Authority Auth;
    int Inspid;
    int LoginId = 0;
    public int EnableDed;
    public int LastStatus
    { 
        get {return Common.CastAsInt32( ViewState["LastStatus"]);}
        set { ViewState["LastStatus"] = value; }
    }
    
    #region"Page_Load"
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        lblmessage.Text = "";
        //InspetionMaster im = (InspetionMaster)this.Master;
        lblMess2.Text = "";
        //--------------------------------- PAGE ACCESS AUTHORITY ----------------------------------------------------
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 1053);
        if (chpageauth <= 0)
            Response.Redirect("blank.aspx");
        //------------------------------------------------------------------------------------------------------------
        if (Session["Insp_Id"] == null) { Session.Add("PgFlag", 1); Response.Redirect("InspectionSearch.aspx"); }
        if (Session["loginid"] != null)
        {
            ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 7);
            OBJ.Invoke();
            Session["Authority"] = OBJ.Authority;
            Auth = OBJ.Authority;
           Inspid= int.Parse(Session["Insp_Id"].ToString());
           LoginId = int.Parse(Session["loginid"].ToString());
        }
        if (!Page.IsPostBack)
        {
            try
            {
                if (Session["Insp_Id"] != null)
                {
                    LastStatus = 0;
                    EnableDed = 0;
                    txtDedPer.Enabled = false; 
                    bindCurrency();
                    BindBonus();
                }
                //****************************************************
            }
            catch (Exception ex)
            {
                lblmessage.Text = ex.StackTrace.ToString();
            }
        }
        ShowChangeHistory();
    }
    protected void btnReview_Click(object sender, EventArgs e)
    {
        EnableDed = 1;
        BindBonus();
        LastStatus = 1;
        btnSaveBonus.Visible = true;
        txtDedPer.Enabled = true;
    }
    protected void btnApproval_Click(object sender, EventArgs e)
    {
        EnableDed = 1;
        BindBonus();
        LastStatus = 2;
        btnSaveBonus.Visible = true;
        txtDedPer.Enabled = true;
    }
    protected void btnApproval2_Click(object sender, EventArgs e)
    {
      
        EnableDed = 1;
        BindBonus();
        LastStatus = 3;
        btnSaveBonus.Visible = true;
        txtDedPer.Enabled = true;
    }
    protected void btn_VBC_Click(object sender, EventArgs e)
    {
        pnlOtherExp.Visible = false;
        pnlVBC.Visible = true;

        btn_VBC.CssClass = "btns";
        btn_OtherExp.CssClass = "btns_sel";
        BindBonus();
    }
    protected void btn_OtherExp_Click(object sender, EventArgs e)
    {
        pnlVBC.Visible = false;
        pnlOtherExp.Visible = true;

        btn_VBC.CssClass = "btns_sel";
        btn_OtherExp.CssClass = "btns";
    }
    protected void CalculateDed_Sum(bool Full)
    {
        decimal SuptdPer = Common.CastAsDecimal(lblSuperPer.Text);
        decimal SuperTotal = 0;
        decimal PayableTotal = 0;
        foreach (RepeaterItem ri in RPT_BONUS.Items)
        {
            decimal DedPer = Common.CastAsDecimal(txtDedPer.Text);
            decimal Amt = Common.CastAsDecimal(((TextBox)ri.FindControl("txtAmount")).Text);
            decimal ded = 0;
            
            if (Full)
            {
                ded = (Amt * DedPer) / 100;
                ((TextBox)ri.FindControl("txtDed")).Text = ded.ToString();
            }
            else
            {
                ded = Common.CastAsDecimal(((TextBox)ri.FindControl("txtDed")).Text);
            }

            decimal SuperAmt = ((Amt - ded) * SuptdPer / 100);
            SuperTotal += SuperAmt;
            ((TextBox)ri.FindControl("txtSuptdAmt")).Text = SuperAmt.ToString();

            Amt = (Amt - ded) - SuperAmt;
            ((TextBox)ri.FindControl("txtPayable")).Text = Amt.ToString();
            PayableTotal += Amt;
        }
        txtSuptdAmt.Text = SuperTotal.ToString();
        txtTotalPayable.Text = string.Format("{0:0}", SuperTotal + PayableTotal);
    }
    protected void txtDedPer_OnTextChanged(object sender, EventArgs e)
    {
        CalculateDed_Sum(true);
    }
    protected void txtDedPer_OnTextChanged2(object sender, EventArgs e)
    {
        CalculateDed_Sum(false);
    }
    
    #endregion
    protected void btnSavebonus_Click(object sender, EventArgs e)
    {
        Common.Execute_Procedures_Select_ByQuery("delete from InspectionBonusMaster where InspectionId=" + Inspid.ToString() + " and Mode=" + LastStatus);
        Common.Execute_Procedures_Select_ByQuery("delete from InspectionBonusDetails where InspectionId=" + Inspid.ToString() + " and Mode=" + LastStatus);

        string sql="INSERT INTO [dbo].[InspectionBonusMaster]([InspectionId] " +
           ",[BEDate] " +
           ",[DedPer] " +
           ",[SuptdPer] " +
           ",[SuptdAmt] " +
           ",[InspectionCount] " +
           ",[ShellScore] " +
           ",[SameSire] " +
           ",[NoRepeat] " +
           ",[Mode] " +
           ",[CreatedBy] " +
           ",[CreatedOn]) " +
           "VALUES " +
           "(" + Inspid.ToString() +
           ",'" + ViewState["EFF"].ToString() + "'" +
           "," + Common.CastAsInt32(txtDedPer.Text).ToString() +
           "," + Common.CastAsDecimal(lblSuperPer.Text).ToString() +
           "," + Common.CastAsDecimal(txtSuptdAmt.Text).ToString() +
           "," + lblInspCount.Text +
           "," + lblShellScore.Text  +
           ",'" + lbl_SameSire.Text.Substring(0,1) + "'" +
           ",'" + lbl_NoRepeat.Text.Substring(0,1) + "'" +
           "," + LastStatus +
           "," + LoginId.ToString() +
           ",GETDATE())";
        Common.Execute_Procedures_Select_ByQuery(sql);
        
        foreach (RepeaterItem ri in RPT_BONUS.Items)
        {
            int CrewId=Common.CastAsInt32(((HiddenField)ri.FindControl("hdfCrewId")).Value);
            int RankId = Common.CastAsInt32(((HiddenField)ri.FindControl("hfdRankId")).Value);

            decimal Bonus = Common.CastAsInt32(((TextBox)ri.FindControl("txtAmount")).Text);
            decimal Ded = Common.CastAsInt32(((TextBox)ri.FindControl("txtDed")).Text);
            decimal Suptd = Common.CastAsInt32(((TextBox)ri.FindControl("txtSuptdAmt")).Text);
            decimal Payable = Common.CastAsInt32(((TextBox)ri.FindControl("txtPayable")).Text);

            string Remark = ((TextBox)ri.FindControl("txtRemarks")).Text.Trim();
            if (Payable + Suptd + Ded + Bonus > 0)
            {
                string sql1 = "INSERT INTO [dbo].[InspectionBonusDetails] " +
                               "([InspectionId] " +
                               ",[Mode] " +
                               ",[CrewId] " +
                               ",[RankId] " +
                               ",[Bonus] " +
                               ",[Ded] " +
                               ",[Suptd] " +
                               ",[Payable] " +
                               ",[Remarks]) " +
                           "VALUES " +
                               "(" + Inspid.ToString() +
                               "," + LastStatus +
                               "," + CrewId.ToString() +
                               "," + RankId.ToString() +
                               "," + Bonus.ToString() +
                               "," + Ded.ToString() +
                               "," + Suptd.ToString() +
                               "," + Payable.ToString() +
                               ",'" + Remark.Replace("'", "`") + "')";
                Common.Execute_Procedures_Select_ByQuery(sql1);
            }
            
        }
       
        //--------------------
        decimal ObservationsCount = Common.CastAsDecimal(lblInspCount.Text);
        decimal ShellScore = Common.CastAsDecimal(lblShellScore.Text);
        
        //--------------------
        if (LastStatus == 0)
        {
            int MaxMode = 2;
            string Approval = "";
            //--------------------
            if ( (ObservationsCount <= 0) || ((ObservationsCount <= 5) && (ShellScore < 15) && lbl_NoRepeat.Text.Trim() == "No" && lbl_SameSire.Text.Trim() == "No") )
            {
                Approval = "Y";
                MaxMode = 2;
            }
            else
            {
                if ((ObservationsCount <= 5) && (((ShellScore > 15) && (ShellScore < 35)) || lbl_NoRepeat.Text.Trim() == "Yes" || lbl_SameSire.Text.Trim() == "Yes"))
                {
                    Approval = "Y";
                    MaxMode = 2;
                }
                else
                {
                    Approval = "Y";
                    MaxMode = 3;
                }
            }
            //--------------------
            string sql_str = "UPDATE InspectionBonusMaster SET Approval='" + Approval + "',MAXMODE=" + MaxMode.ToString() + " WHERE INSPECTIONID=" + Inspid.ToString();
            Common.Execute_Procedures_Select_ByQuery(sql_str);

            ViewState["Mode"] = LastStatus;
            ViewState["MaxMode"] = MaxMode;
            ViewState["ApprovalNeeded"] = Approval;
        }
        else
        {
            string sql_str = "UPDATE InspectionBonusMaster SET Approval='" + ViewState["ApprovalNeeded"].ToString() + "',MAXMODE=" + ViewState["MaxMode"].ToString() + " WHERE INSPECTIONID=" + Inspid.ToString();
            Common.Execute_Procedures_Select_ByQuery(sql_str);
        }
        //--------------------

        SetColors();
        BindBonus();
        ShowChangeHistory();
        //--------------------
        lblmessage.Text = "Record saved successfully.";
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "abc", "window.open('../Reports/InspExpenseReport.aspx?InspId=" + Inspid.ToString() + "&MaxMode=" + ViewState["MaxMode"] .ToString() + "','');", true);
    }
    protected void btnUpdateScale_Click(object sender, EventArgs e)
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT VESSELID,ACTUALDATE,INSPECTIONNO FROM T_INSPECTIONDUE WHERE ID=" + Session["Insp_Id"].ToString());
        if (dt.Rows.Count > 0)
        {
            int VesselId = Common.CastAsInt32(dt.Rows[0][0]);
            string Actdate = Common.ToDateString(dt.Rows[0][1]);
            string INSPNO = dt.Rows[0][2].ToString();
            decimal SuptdPer=0;

            if (Actdate != "")
            {
                
                int BonusId = 0;
                dt = Common.Execute_Procedures_Select_ByQuery("select TOP 1 BONUSID,SuptdPer,EFFDATE from BONUSMASTER WHERE EFFDATE<='" + Actdate + "' ORDER BY EFFDATE DESC");
                if (dt.Rows.Count > 0)
                {
                    BonusId = Common.CastAsInt32(dt.Rows[0][0]);
                    SuptdPer= Common.CastAsDecimal(dt.Rows[0][1]);
                    lblSuperPer.Text ="0";// SuptdPer.ToString();
                    lblEffDate.Text = "<b>Inspection# :</b> " + INSPNO + " | <b>Inspection Date :</b> " + Actdate + " | <b>Bonus Scale Date :</b> " + Common.ToDateString(dt.Rows[0][2]);
                    ViewState["EFF"] = Common.ToDateString(dt.Rows[0][2]);
                    ViewState["SuptdPer"] = SuptdPer;
                }
                dt = Common.Execute_Procedures_Select_ByQuery("SELECT ROW_NUMBER() OVER (ORDER BY R.RANKLEVEL) AS SNO,CPD.CREWID,NEWRANKID,CPD.CREWNUMBER,CPD.FIRSTNAME + ' ' + ISNULL(CPD.MIDDLENAME,' ') + ' ' + CPD.LASTNAME AS CREWNAME,RANKCODE,(SELECT AMOUNT FROM BonusRankDetails WHERE RANKID=R.RANKID AND BONUSID=" + BonusId.ToString() + ") AS BONUS,0 AS DED,0 AS SUPTD,0 AS PAYABLE,'' AS REMARKS " +
                    "FROM DBO.GET_CREW_HISTORY H  " +
                   "INNER JOIN DBO.CREWPERSONALDETAILS CPD ON CPD.CREWID=H.CREWID " +
                   "INNER JOIN DBO.RANK R ON R.RANKID=H.NEWRANKID AND R.STATUSID='A' AND R.RANKID NOT IN (141,142)" +
                   "WHERE VESSELID=" + VesselId.ToString() + " AND H.SIGNONDATE<='" + Actdate + "' AND (H.SINGOFFDATE>='" + Actdate + "' OR H.SINGOFFDATE IS NULL) ORDER BY R.RANKLEVEL");
                RPT_BONUS.DataSource = dt;
                RPT_BONUS.DataBind();
                lblRcount.Text = " [ " + dt.Rows.Count.ToString() + " ]  Records Found.";
                CalculateDed_Sum(true);
            }

            decimal ObservationsCount=0;
            decimal ShellScore=0;
            //------------------------------------
            DataTable dt1 = Common.Execute_Procedures_Select_ByQuery("SELECT COUNT(*) FROM T_OBSERVATIONS WHERE INSPECTIONDUEID=" + Inspid.ToString());
            if (dt1.Rows.Count > 0)
            {
                lblInspCount.Text = dt1.Rows[0][0].ToString();
                ObservationsCount=Common.CastAsDecimal(lblInspCount.Text);
                
            }

            DataTable dt2 = Common.Execute_Procedures_Select_ByQuery("SELECT AVG(CAST( SHELLSCORE AS FLOAT)) AS 'Score' FROM DBO.T_OBSERVATIONS INNER JOIN T_INSPECTIONDUE I ON T_OBSERVATIONS.INSPECTIONDUEID=I.ID INNER JOIN M_QUESTIONS M ON M.Id=T_OBSERVATIONS.QuestionId WHERE I.ID=" + Inspid.ToString());
            if (dt2.Rows.Count > 0)
            {
                lblShellScore.Text = string.Format("{0:0}", Common.CastAsDecimal(dt2.Rows[0][0]));
                ShellScore=Common.CastAsDecimal(lblShellScore.Text);
            }

            string SQL = "select chapterid,count(*) as Qnos from t_observations b " +
                       "inner join m_questions q on b.questionid=q.id " +
                       "inner join m_SubChapters s on q.subchapterid=s.id " +
                       "inner join m_Chapters m on s.chapterid=m.id and inspectiongroup=1 " +
                       "where inspectiondueid=" + Inspid.ToString() + " group by chapterid having Count(*) > 1";

            dt2 = Common.Execute_Procedures_Select_ByQuery(SQL);
            if (dt2.Rows.Count > 0)
                lbl_SameSire.Text = "Yes";
            else
                lbl_SameSire.Text = "No";

            SQL = "select * from t_observations main where main.inspectiondueid=" + Inspid.ToString() + " " +
                        "and main.questionid in " +
                        "(  " +
                        "    select questionid from t_observations where inspectiondueid in  " +
                        "    ( " +
                        "    select top 2 t.id from t_inspectiondue t  " +
                        "    inner join m_inspection m on m.id=t.inspectionid " +
                        "    inner join m_inspectiongroup g on g.id=m.InspectionGroup " +
                        "    where t.vesselid=" + VesselId + " and t.id<" + Inspid.ToString() + " order by id desc " +
                        "    ) " +
                        ") ";

            dt2 = Common.Execute_Procedures_Select_ByQuery(SQL);
            if (dt2.Rows.Count > 0)
                lbl_NoRepeat.Text = "Yes";
            else
                lbl_NoRepeat.Text = "No";
            //-------------------------------------
            btnSaveBonus.Visible = true;
            //-------------------------------------
        }
    }
    protected void Bind_Inspe_ShellScore()
    {

    }
    protected void BindBonus()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT TOP 1 * FROM InspectionBonusMaster WHERE INSPECTIONID=" + Inspid.ToString() + " ORDER BY MODE DESC");
        if (dt.Rows.Count > 0)
        {
            DataTable dt_ins = Common.Execute_Procedures_Select_ByQuery("select actualdate,vesselid,INSPECTIONNO from t_inspectiondue where id=" + Inspid.ToString());
            string INSPNO = "";
               if (dt_ins.Rows.Count > 0)
               {
                   INSPNO = dt_ins.Rows[0][2].ToString();
                   lblEffDate.Text = "<b>Inspection# :</b> " + INSPNO + " | <b>Inspection Date :</b> " + Common.ToDateString(dt_ins.Rows[0]["actualdate"]) + " | <b>Bonus Scale Date :</b> " + Common.ToDateString(dt.Rows[0]["BEDate"]);
                   ViewState["EFF"] = Common.ToDateString(dt.Rows[0]["BEDate"]);
                   
               }
            
               ViewState["Mode"] = Common.CastAsInt32(dt.Rows[0]["Mode"]);
               ViewState["MaxMode"] = Common.CastAsInt32(dt.Rows[0]["MaxMode"]);
               ViewState["ApprovalNeeded"] = dt.Rows[0]["Approval"].ToString();

               LastStatus = Common.CastAsInt32(ViewState["Mode"]);
               lblSuperPer.Text= dt.Rows[0]["SuptdPer"].ToString();
               
               txtSuptdAmt.Text =string.Format("{0:0}",Common.CastAsDecimal(dt.Rows[0]["suptdamt"]));
               lblInspCount.Text = dt.Rows[0]["InspectionCount"].ToString();
               lblShellScore.Text = string.Format("{0:0}",dt.Rows[0]["ShellScore"].ToString());
               txtDedPer.Text = string.Format("{0:0}", dt.Rows[0]["Dedper"].ToString());

               lbl_SameSire.Text =(Convert.ToString(dt.Rows[0]["SameSire"])=="Y")?"Yes":"No";
               lbl_NoRepeat.Text = (Convert.ToString(dt.Rows[0]["NoRepeat"]) == "Y") ? "Yes" : "No";
               
               dt = Common.Execute_Procedures_Select_ByQuery("SELECT ROW_NUMBER() OVER (ORDER BY R.RANKLEVEL) AS SNO,CPD.CREWID,B.RANKID AS NEWRANKID,CPD.CREWNUMBER,CPD.FIRSTNAME + ' ' + CPD.MIDDLENAME + ' ' + CPD.LASTNAME AS CREWNAME,RANKCODE,BONUS,DED,SUPTD,PAYABLE,B.REMARKS " +
                                                                    "FROM DBO.RANK R " +
                                                                    "LEFT JOIN InspectionBonusDetails B ON R.RANKID=B.RANKID  " + 
                                                                    "INNER JOIN DBO.CREWPERSONALDETAILS CPD ON CPD.CREWID=B.CREWID " +
                                                                    "WHERE B.INSPECTIONID=" + Inspid.ToString() + "and MODE=" + LastStatus.ToString());    

               RPT_BONUS.DataSource = dt;
               RPT_BONUS.DataBind();
               lblRcount.Text = " [ " + dt.Rows.Count.ToString() + " ]  Records Found.";
               txtTotalPayable.Text = string.Format("{0:0}", Common.CastAsDecimal(txtSuptdAmt.Text) + Common.CastAsDecimal(dt.Compute("SUM(PAYABLE)", "")));

               SetColors();
               ResetButtons();
        }
    }
    public void SetColors()
    {
        //--------------------------
        int _mmode = Common.CastAsInt32(ViewState["MaxMode"]);
        string _appneeded = ViewState["ApprovalNeeded"].ToString();
        if (_mmode == 3)
        {
            dvHistory.Style.Add("background-color", "Red");
            lblreason.Text = "Red Approval";
            lblreason.ForeColor = System.Drawing.Color.White;
        }
        if (_mmode == 2)
        {
            if ( (Common.CastAsInt32(lblInspCount.Text) <= 0) || ((Common.CastAsInt32(lblInspCount.Text) <= 5) && (Common.CastAsInt32(lblShellScore.Text) < 15) && lbl_NoRepeat.Text.Trim() == "No" && lbl_SameSire.Text.Trim() == "No"))
            {
                dvHistory.Style.Add("background-color", "Green");
                lblreason.Text = "Green Approval";
                lblreason.ForeColor = System.Drawing.Color.White;
            }
            else
            {
                if (_appneeded == "Y")
                {
                    dvHistory.Style.Add("background-color", "Orange");
                    lblreason.Text = "Orange Approval";
                    lblreason.ForeColor = System.Drawing.Color.Black;
                }
                else
                {
                    dvHistory.Style.Add("background-color", "Green");
                    lblreason.Text = "Green Approval";
                    lblreason.ForeColor = System.Drawing.Color.White;
                }
            }
        }
        //-------------------
        int InspCount=Common.CastAsInt32(lblInspCount.Text);
        if (InspCount <= 5)
            tdInspCount.Style.Add("background-color","Green");
        else
            tdInspCount.Style.Add("background-color", "Red");
        //-------------------
        decimal SS = Common.CastAsDecimal(lblShellScore.Text);
        if (SS < 15)
            tdShellScore.Style.Add("background-color","Green");
        else if (SS > 15 && SS< 35)
            tdShellScore.Style.Add("background-color","Orange");
        else
            tdShellScore.Style.Add("background-color","Red");
            
        //-------------------
        if(lbl_SameSire.Text=="Yes")
            td_SameSire.Style.Add("background-color","Red");
        else
            td_SameSire.Style.Add("background-color", "Green");
        //-------------------
        if (lbl_NoRepeat.Text == "Yes")
            td_NoRepeat.Style.Add("background-color","Red");
        else
            td_NoRepeat.Style.Add("background-color", "Green");
    }
    protected void ResetButtons()
    {
        int Mode = Common.CastAsInt32(ViewState["Mode"]);
        int MaxMode = Common.CastAsInt32(ViewState["MaxMode"]);
        string ApprovalNeeded = ViewState["ApprovalNeeded"].ToString();
        btnSaveBonus.Visible = false;
        
        if (Mode == MaxMode)
        {
            Button1.Visible = false;
            btnReview.Visible = false;
            btnApprove.Visible = false;
            btnPrint.Visible = true;
        }
        else
        {
            if (Mode == 2) // Approve
            {
                Button1.Visible = false;
                btnReview.Visible = false;
                btnApprove2.Visible = true;
            }
            else if (Mode == 1) // Review
            {
                Button1.Visible = false;
                btnReview.Visible = false;
                btnApprove.Visible = true;
            }
            else
            {
                Button1.Visible = true;
                btnApprove.Visible = false;
                if (ApprovalNeeded == "Y")
                {
                    btnReview.Visible = true;
                }
                else
                {
                    btnReview.Visible = false;
                }
            }
        }
    }
    protected void ShowChangeHistory()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("select row_number() over(order by Mode desc) as sno,firstname + ' ' + lastname as ModifiedBy,i.CreatedOn,case when Mode=2 then 'Approval' when Mode=3 then 'Approval-2' else 'Review' end as Action,i.inspectionid,i.Mode " +
                                                                     "from [dbo].[InspectionBonusMaster] i inner join dbo.userlogin u on u.loginid=i.createdby where Mode >0 and i.inspectionid=" + Inspid.ToString() + " order by Mode desc");
        rptData.DataSource = dt;
        rptData.DataBind();
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        dvAccountBox.Visible = false;
    }
    protected void lblreason_click(object sender, EventArgs e)
    {
        dvAccountBox.Visible = true;
        if (lblreason.Text.Contains("Green"))
        {
            spn_G.Visible = true;
        }
        if (lblreason.Text.Contains("Orange"))
        {
            spn_O.Visible = true;
        }
        if (lblreason.Text.Contains("Red"))
        {
            spn_R.Visible = true;
        }
    }

    #region "User Defined Function"
    public void bindBlankGrid2()
    {
        DataTable dt = new DataTable();
        DataRow dr;
        dt.Columns.Add("TableId");
        dt.Columns.Add("Sno");
        dt.Columns.Add("Descr");
        dt.Columns.Add("CostHead");
        dt.Columns.Add("Amt");
        dt.Columns.Add("CurrencyName");
        dt.Columns.Add("ExchRates");
        dt.Columns.Add("FileName");
        for (int i = 0; i < 9; i++)
        {
            dr = dt.NewRow();
            dt.Rows.Add(dr);
            dt.Rows[dt.Rows.Count - 1][0] = "";
            dt.Rows[dt.Rows.Count - 1][1] = "";
            dt.Rows[dt.Rows.Count - 1][2] = "";
            dt.Rows[dt.Rows.Count - 1][3] = "";
            dt.Rows[dt.Rows.Count - 1][4] = "";
            dt.Rows[dt.Rows.Count - 1][5] = "";
            dt.Rows[dt.Rows.Count - 1][6] = "";
            dt.Rows[dt.Rows.Count - 1][7] = "";
        }
        gvOtherExp.DataSource = dt;
        gvOtherExp.DataBind();
    }
    public void bindGrid2()
    {
        try
        {
            DataTable dt = Budget.getTable("select Row_Number() Over(order by tableid) as Sno,TableId,InsId,Descr,CostHead,Amt, " +
                                           "(select currencyname from dbo.currency sc where sc.currencyid=det.curr) as CurrencyName, " +
                                           "ExchRates,FileName from t_inspectionOthercost det Where InsId=" + Session["Insp_Id"].ToString()).Tables[0];
            if (dt.Rows.Count > 0)
            {
                this.gvOtherExp.DataSource = dt;
                this.gvOtherExp.DataBind();
                string ret="0.00";
                ret=dt.Compute("Sum(Amt)", "").ToString();
                Decimal val=Decimal.Parse(ret);
                lblSum.Text = val.ToString("0.00");
                //if (txt_SubTotal.Text!="")
                //    val=val+decimal.Parse(txt_SubTotal.Text);
                lblGTotal.Text = val.ToString("0.00");
            }
            else
            {
                bindBlankGrid2();
                lblSum.Text = "0.00";
                Decimal val = 0;
                //if (txt_SubTotal.Text != "")
                //    val = val + decimal.Parse(txt_SubTotal.Text);
                lblGTotal.Text = val.ToString("0.00");
            }
            btnSave2.Visible = false;
            btnCancel.Visible = false;
            pnlOtherExEdit.Visible = false;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public void bindCurrency()
    {
        try
        {
            DataSet dsCurrency = Inspection_Master.getMasterDataforInspection("Currency", "CurrencyId", "CurrencyName as Name");
         
            this.ddlCurrency2.DataSource = dsCurrency;
            this.ddlCurrency2.DataValueField = "CurrencyId";
            this.ddlCurrency2.DataTextField = "Name";
            this.ddlCurrency2.DataBind();
            //this.ddlCurrency2.Items.Insert(0, new ListItem("", "0"));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void ShowValues2()
    {
        try
        {
            DataTable dt = Budget.getTable("select InsId,Descr,CostHead,Amt,Curr,ExchRates,FileName from t_inspectionOthercost Where TableId=" + ("" + ViewState["Edit2Id"])).Tables[0];
            if (dt.Rows.Count > 0)
            {
                txtDesc.Text = dt.Rows[0]["Descr"].ToString();
                txtCostHead.Text = dt.Rows[0]["CostHead"].ToString();
                txtAmt.Text = dt.Rows[0]["Amt"].ToString();
                ddlCurrency2.SelectedValue = dt.Rows[0]["Curr"].ToString();
                txtExRates.Text = dt.Rows[0]["ExchRates"].ToString();
                ViewState["FileName"] = dt.Rows[0]["FileName"].ToString();
            }
            else
            {
                txtDesc.Text = "";
                txtCostHead.Text = "";
                txtAmt.Text = "";
                //ddl_Curr.SelectedIndex = 0;
                txtExRates.Text = "";
                ViewState["FileName"]="";
            }
            pnlOtherExEdit.Visible = true;
            btnSave2.Visible = true;
            btnCancel.Visible = true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion
  
    #region "Events - 2"
    protected void gvOtherExp_DataBound(object sender, EventArgs e)
    {
        Alerts.HANDLE_GRID(gvOtherExp, Auth);
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        ViewState["Edit2Id"] = "0";
        ViewState["FileName"] = "";
        btnSave2.Text = "Save";
        ShowValues2();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        pnlOtherExEdit.Visible = false;
        btnSave2.Visible = false;
        btnCancel.Visible = false;
    }
    protected void btnSave2_Click(object sender, EventArgs e)
    {
        if (Session["Insp_Id"] == null) { lblMess2.Text = "Please save Planning first."; return; }
        if (txtDesc.Text.Trim() == "") { lblMess2.Text = "Please enter details."; txtDesc.Focus();return; }
        if (txtAmt.Text.Trim() == "") { lblMess2.Text = "Please enter Amount(USD)."; txtAmt.Focus(); return; }
        if (txtExRates.Text.Trim() == "") { lblMess2.Text = "Please enter Exch. rates."; txtExRates.Focus(); return; }
        try
        {
            double Amt = 0;
            double ExAmt = 0;
            Amt = double.Parse(txtAmt.Text.Trim());
            ExAmt = double.Parse(txtExRates.Text.Trim());
            string FileName = ViewState["FileName"].ToString();
            if (flp1.HasFile)
            {
                FileName = System.IO.Path.GetFileName(flp1.FileName);
                string Name=System.IO.Path.GetFileNameWithoutExtension(FileName);
                string Ext = System.IO.Path.GetExtension(FileName); 
                FileName=Name+DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") +Ext; 
                //---------------------
                flp1.SaveAs(Server.MapPath("~\\EMANAGERBLOB\\Inspection\\OtherExp\\") + FileName);
            }
            if (Int32.Parse(ViewState["Edit2Id"].ToString()) == 0) // Insert
            { 
                Budget.getTable("INSERT INTO t_inspectionOthercost(InsId, Descr, CostHead, Amt, Curr, ExchRates, FileName, CreatedBy, CreatedOn) " +
                    "VALUES(" + Session["Insp_Id"].ToString() + ",'" + txtDesc.Text + "','" + txtCostHead.Text + "'," + Amt.ToString() + "," + ddlCurrency2.SelectedValue + "," + ExAmt.ToString() + ",'" + FileName + "'," + Session["LoginId"].ToString() + ",'" + DateTime.Today.ToString("MM/dd/yyyy") + "')");
                lblmessage.Text = "Expense Added Sucessfully.";
            }
            else
            {
                Budget.getTable("UPDATE t_inspectionOthercost SET Descr='"+ txtDesc.Text +"', CostHead='"+ txtCostHead.Text +"', Amt=" + Amt.ToString() +", Curr="+ ddlCurrency2.SelectedValue +", ExchRates="+ ExAmt.ToString() +", FileName='"+ FileName +"', ModifiedBy="+ Session["LoginId"].ToString() +", ModifiedOn='"+ DateTime.Today.ToString("MM/dd/yyyy") +"' Where TableId=" + ViewState["Edit2Id"].ToString());
                lblmessage.Text = "Expense Updated Sucessfully.";
            }
            bindGrid2();
        }
        catch (Exception ex)
        {
            lblmessage.Text = ex.StackTrace.ToString();
        }
    }
    public string FileExists(string _path)
    {
        string res = "";
        if (_path.StartsWith("U"))
        {
            res = "../" + _path;
        }
        else
        {
            res = "../EMANAGERBLOB/Inspection/OtherExp/" + _path;
        }
        if (!(System.IO.File.Exists(Server.MapPath(res)))) { return "none"; }
        else { return "block"; }

    }
    public string GetPath(string _path)
    {
        string res = "";
        if (_path.StartsWith("U"))
        {
            res = "../" + _path;
        }
        else
        {
            res = "../EMANAGERBLOB/Inspection/OtherExp/" + _path;
        }
        if (!(System.IO.File.Exists(Server.MapPath(res)))) { res = ""; }
        return res;
    }
    protected void gvOtherExp_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (gvOtherExp.DataKeys[e.RowIndex].Value.ToString() != "")
        {
            Budget.getTable("Delete From t_inspectionOthercost Where tableId=" + gvOtherExp.DataKeys[e.RowIndex].Value.ToString());
            lblmessage.Text = "Expense Deleted Successfully.";
            bindGrid2();
        }
    }
    protected void gvOtherExp_RowEditing(object sender, GridViewEditEventArgs e)
    {
        if (gvOtherExp.DataKeys[e.NewEditIndex].Value.ToString() != "")
        {
            ViewState["Edit2Id"] = gvOtherExp.DataKeys[e.NewEditIndex].Value.ToString();
            ShowValues2();
            btnSave2.Text = "Update";
        }
    }
    protected void gvOtherExp_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvOtherExp.PageIndex = e.NewPageIndex;
        bindGrid2();
    }
    #endregion
}
