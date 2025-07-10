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
using System.Text;

public partial class emtm_MyProfile_Emtm_Profile_TrainingManagement : System.Web.UI.Page
{
    public int Month
    {
        get
        {
            return Common.CastAsInt32(ViewState["Month"]);
        }
        set
        {
            ViewState["Month"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        if (!IsPostBack)
        {
            BindYear();
        }
    }
    public void BindYear()
    {
        for (int i = DateTime.Now.Year+1; i >=2012 ; i--)
        {
            ddlYear.Items.Add(new System.Web.UI.WebControls.ListItem(i.ToString(), i.ToString()));            
        }
        ddlYear.SelectedValue = DateTime.Today.Year.ToString();
        ddlYear_SelectedIndexChanged(new object() , new EventArgs()); 
    }
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        Clear();       

        lbJan.Text = "Jan " + ddlYear.SelectedValue.Trim();
        lbFeb.Text = "Feb " + ddlYear.SelectedValue.Trim();
        lbMar.Text = "Mar " + ddlYear.SelectedValue.Trim();
        lbApr.Text = "Apr " + ddlYear.SelectedValue.Trim();
        lbMay.Text = "May " + ddlYear.SelectedValue.Trim();
        lbJun.Text = "Jun " + ddlYear.SelectedValue.Trim();
        lbJul.Text = "Jul " + ddlYear.SelectedValue.Trim();
        lbAug.Text = "Aug " + ddlYear.SelectedValue.Trim();
        lbSep.Text = "Sep " + ddlYear.SelectedValue.Trim();
        lbOct.Text = "Oct " + ddlYear.SelectedValue.Trim();
        lbNov.Text = "Nov " + ddlYear.SelectedValue.Trim();
        lbDec.Text = "Dec " + ddlYear.SelectedValue.Trim();

        BindYearTrainings();

    }
    public void BindYearTrainings()
    {
        int EmpId = Common.CastAsInt32(Session["ProfileId"]);
       
        for (int i = 1; i <= 12; i++)
        {

            string strSQL = "SELECT TP.TrainingPlanningId,TP.TrainingId,TM.TrainingName,Location,Replace(Convert(varchar(11),StartDate,106),' ', '-') AS StartDate,Replace(Convert(varchar(11),EndDate,106),' ', '-') AS EndDate,StartTime,EndTime,[Status],CancelledByUser,Replace(Convert(varchar(11),CancelledOn,106),' ', '-') AS CancelledOn,CompletedByUser,Replace(Convert(varchar(11),CompletedOn,106),' ', '-') AS CompletedOn,Location1,Replace(Convert(varchar(11),StartDate1,106),' ', '-') AS StartDate1,Replace(Convert(varchar(11),EndDate1,106),' ', '-') AS EndDate1,StartTime1,EndTime1 FROM HR_TrainingPlanning TP " +
                            "INNER JOIN HR_TrainingPlanningDetails TPD ON TP.TrainingPlanningId = TPD.TrainingPlanningId " +
                            "INNER JOIN HR_TrainingMaster TM ON TP.TrainingId = TM.TrainingId WHERE YEAR(StartDate) = " + ddlYear.SelectedValue.Trim() + " AND MONTH(StartDate) = " + i + " AND TPD.EmpId = " + EmpId.ToString() + "  ORDER BY StartDate ASC";

            DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(strSQL);
            if (dt.Rows.Count > 0)
            {
                
                StringBuilder sb = new StringBuilder();
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["Status"].ToString() == "A")
                    {
                        sb.Append("<div>");
                        sb.Append("<div class='_tr_trainingboxblank' style='float:left; width:99%;'><a href='#' style='float:left; width:89%' onclick='ViewPlaning(" + dr["TrainingPlanningId"].ToString() + ")'>" + dr["TrainingName"].ToString() + " planned at " + dr["Location"].ToString() + " from " + dr["StartDate"].ToString() + " " + dr["StartTime"].ToString() + " Hrs " + " to " + dr["EndDate"].ToString() + " " + dr["EndTime"].ToString() + " Hrs");
                        sb.Append("</a><div  style='float:right; width:10%;background-color:#FFFFCC;'>&nbsp;</div></div>");
                        sb.Append("");
                        sb.Append("</div>");

                        //sb.Append("<div class='_tr_trainingboxplanned'><a href='#' onclick='ViewPlaning(" + dr["TrainingPlanningId"].ToString() + ")'>" + dr["TrainingName"].ToString() + " planned at " + dr["Location"].ToString() + " from " + dr["StartDate"].ToString() + " " + dr["StartTime"].ToString() + " Hrs " + " to " + dr["EndDate"].ToString() + " " + dr["EndTime"].ToString() + " Hrs");
                        //sb.Append("</a></div>");
                    }
                    if (dr["Status"].ToString() == "C")
                    {
                        sb.Append("<div>");
                        sb.Append("<div class='_tr_trainingboxblank' style='float:left; width:99%;'><a href='#' style='float:left; width:89%' onclick='ViewPlaning(" + dr["TrainingPlanningId"].ToString() + ")'>" + dr["TrainingName"].ToString() + " planned at " + dr["Location"].ToString() + " from " + dr["StartDate"].ToString() + " to " + dr["EndDate"].ToString() + " <b>[ Cancelled on " + dr["CancelledOn"].ToString() + "</b> ]");
                        sb.Append("</a><div  style='float:right; width:10%;background-color:#FF5F5F;'>&nbsp;</div></div>");
                        sb.Append("");
                        sb.Append("</div>");

                        //sb.Append("<div class='_tr_trainingboxcanceled'><a style='color:white;' href='#' onclick='ViewPlaning(" + dr["TrainingPlanningId"].ToString() + ")'>" + dr["TrainingName"].ToString() + " planned at " + dr["Location"].ToString() + " from " + dr["StartDate"].ToString() + " to " + dr["EndDate"].ToString() + " <b>[ Cancelled on " + dr["CancelledOn"].ToString() + "</b> ]");
                        //sb.Append("</a></div>");
                    }
                    if (dr["Status"].ToString() == "E")
                    {
                        sb.Append("<div>");
                        sb.Append("<div class='_tr_trainingboxblank' style='float:left; width:99%;'><a  href='#' style='float:left; width:89%' onclick='ViewPlaning(" + dr["TrainingPlanningId"].ToString() + ")'>" + dr["TrainingName"].ToString() + " completed at " + dr["Location1"].ToString() + " from " + dr["StartDate1"].ToString() + " to " + dr["EndDate1"].ToString());
                        sb.Append("</a><div  style='float:right; width:10%;background-color:#51B751;'>&nbsp;</div></div>");
                        sb.Append("");
                        sb.Append("</div>");

                        //sb.Append("<div class='_tr_trainingboxcompleted'><a style='color:white;' href='#' onclick='ViewPlaning(" + dr["TrainingPlanningId"].ToString() + ")'>" + dr["TrainingName"].ToString() + " completed at " + dr["Location1"].ToString() + " from " + dr["StartDate1"].ToString() + " to " + dr["EndDate1"].ToString());
                        //sb.Append("</a></div>");
                    }

                }
                
                if (i == 1)
                {
                    litJan.Text = sb.ToString();
                }
                else if (i == 2)
                {
                    litFeb.Text = sb.ToString();
                }
                else if (i == 3)
                {
                    litMar.Text = sb.ToString();
                }
                else if (i == 4)
                {
                    litApr.Text = sb.ToString();
                }
                else if (i == 5)
                {
                    litMay.Text = sb.ToString();
                }
                else if (i == 6)
                {
                    litJun.Text = sb.ToString();
                }
                else if (i == 7)
                {
                    litJul.Text = sb.ToString();
                }
                else if (i == 8)
                {
                    litAug.Text = sb.ToString();
                }
                else if (i == 9)
                {
                    litSep.Text = sb.ToString();
                }
                else if (i == 10)
                {
                    litOct.Text = sb.ToString();
                }
                else if (i == 11)
                {
                    litNov.Text = sb.ToString();
                }
                else 
                {
                    litDec.Text = sb.ToString();
                }
            }
        }

    }
    //public void BindRequirements()
    //{
    //    for (int i = 1; i <= 12; i++)
    //    {
    //        string strSQL = "SELECT COUNT(*) AS Requirement FROM HR_TrainingRecommended WHERE YEAR(DueDate) =" + ddlYear.SelectedValue.Trim() + " AND MONTH(DueDate) = " + i + " AND TrainingRecommId NOT IN (SELECT TrainingRecommId FROM HR_TrainingPlanningDetails P1 WHERE P1.TrainingPlanningId In (SELECT P2.TrainingPlanningId from HR_TrainingPlanning P2 WHERE P2.Status <> 'C'))";
    //        DataTable dtReq = Common.Execute_Procedures_Select_ByQueryCMS(strSQL);            

    //        if (i == 1)
    //        {
    //            lbReq1.Text = dtReq.Rows[0]["Requirement"].ToString();
    //        }
    //        else if (i == 2)
    //        {
    //            lbReq2.Text = dtReq.Rows[0]["Requirement"].ToString();
    //        }
    //        else if (i == 3)
    //        {
    //            lbReq3.Text = dtReq.Rows[0]["Requirement"].ToString();
    //        }
    //        else if (i == 4)
    //        {
    //            lbReq4.Text = dtReq.Rows[0]["Requirement"].ToString();
    //        }
    //        else if (i == 5)
    //        {
    //            lbReq5.Text = dtReq.Rows[0]["Requirement"].ToString();
    //        }
    //        else if (i == 6)
    //        {
    //            lbReq6.Text = dtReq.Rows[0]["Requirement"].ToString();
    //        }
    //        else if (i == 7)
    //        {
    //            lbReq7.Text = dtReq.Rows[0]["Requirement"].ToString();
    //        }
    //        else if (i == 8)
    //        {
    //            lbReq8.Text = dtReq.Rows[0]["Requirement"].ToString();
    //        }
    //        else if (i == 9)
    //        {
    //            lbReq9.Text = dtReq.Rows[0]["Requirement"].ToString();
    //        }
    //        else if (i == 10)
    //        {
    //            lbReq10.Text = dtReq.Rows[0]["Requirement"].ToString();
    //        }
    //        else if (i == 11)
    //        {
    //            lbReq11.Text = dtReq.Rows[0]["Requirement"].ToString();
    //        }
    //        else 
    //        {
    //            lbReq12.Text = dtReq.Rows[0]["Requirement"].ToString();
    //        }
    //    }
    //}

    protected void lbJan_Click(object sender, EventArgs e)
    {
        Month = Common.CastAsInt32(((LinkButton)sender).CommandArgument.ToString().Trim());
        int Year = Common.CastAsInt32(ddlYear.SelectedValue.Trim());

        DateTime Date = new DateTime(Year, Month, 1);
        CalTrainingDetails.VisibleDate = Convert.ToDateTime(Date);
        lblMonthYearName.Text = Common.ToDateString(Date).Substring(3).ToUpper(); 

        pnlMonth.Visible = true;
        btnBack.Visible = true;
        pnlYear.Visible = false;
        btnPrint.Visible = false;
    }
    //protected void lbReq_Click(object sender, EventArgs e)
    //{
    //    Month = Common.CastAsInt32(((LinkButton)sender).CommandArgument.ToString().Trim());
    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "asdf", "OpenRecomm('" + ddlYear.SelectedValue.Trim() + "','" + Month + "');", true);
        
    //}
    public void Clear()
    {
        lbJan.Text = "";
        lbFeb.Text = "";
        lbMar.Text = "";
        lbApr.Text = "";
        lbMay.Text = "";
        lbJun.Text = "";
        lbJul.Text = "";
        lbAug.Text = "";
        lbSep.Text = "";
        lbOct.Text = "";
        lbNov.Text = "";
        lbDec.Text = "";

        litJan.Text = "";
        litFeb.Text = "";
        litMar.Text = "";
        litApr.Text = "";
        litMay.Text = "";
        litJun.Text = "";
        litJul.Text = "";
        litAug.Text = "";
        litSep.Text = "";
        litOct.Text = "";
        litNov.Text = "";
        litDec.Text = "";

    }

    #region --------------- Training Details -------------
    protected void CalTrainingDetails_DayRender(object sender, DayRenderEventArgs e)
    {
        int EmpId = Common.CastAsInt32(Session["ProfileId"]);

        string strSQL = "SELECT TP.TrainingPlanningId,TP.TrainingId,TM.TrainingName,Location,Replace(Convert(varchar(11),StartDate,106),' ', '-') AS StartDate,Replace(Convert(varchar(11),EndDate,106),' ', '-') AS EndDate,StartTime,EndTime,[Status],DAY(StartDate) as StartDay,DAY(EndDate) as EndDay FROM HR_TrainingPlanning TP " +
                        "INNER JOIN HR_TrainingPlanningDetails TPD ON TP.TrainingPlanningId = TPD.TrainingPlanningId " +
                        "INNER JOIN HR_TrainingMaster TM ON TP.TrainingId = TM.TrainingId WHERE YEAR(StartDate) = " + ddlYear.SelectedValue.Trim() + " AND MONTH(StartDate) = " + Month + " AND TPD.EmpId = " + EmpId.ToString() + " ORDER BY DAY(StartDate) ";

        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(strSQL);
        if (dt.Rows.Count > 0)
        {
            //foreach (DataRow dr in dt.Rows)
            //{
            //    int calDayNum = e.Day.Date.Day;
            //    int calMonth = e.Day.Date.Month;

            //    int TStartDay = Common.CastAsInt32(dr["StartDay"].ToString());
            //    int TEndDay = Common.CastAsInt32(dr["EndDay"].ToString());
            //    e.Cell.Controls.Clear();
                
            //        TableCell tc = e.Cell;

            //        string str = "<div style='min-height:100px;border:dotted 1px green;'><div class='" + ((calMonth == Month) ? "_tr_dayheader_cmonth" : "_tr_dayheader_other") + "'>" + calDayNum + "</div>";
            //        if (calMonth == Month && calDayNum >= TStartDay && calDayNum <= TEndDay)
            //        {
            //            if (dr["Status"].ToString() == "A")
            //            {   
            //                str += "<div class='_tr_trainingboxplanned'><a href='#'onclick='ViewPlaning(" + dr["TrainingPlanningId"].ToString() + ")'> " + dr["TrainingName"].ToString() + "</a></div></div>";
            //            }
            //            if (dr["Status"].ToString() == "C")
            //            {
            //                str += "<div class='_tr_trainingboxcanceled'><a href='#'onclick='ViewPlaning(" + dr["TrainingPlanningId"].ToString() + ")'> " + dr["TrainingName"].ToString() + "</a></div></div>";
            //            }
            //            if (dr["Status"].ToString() == "E")
            //            {
            //                str += "<div class='_tr_trainingboxcompleted'><a href='#'onclick='ViewPlaning(" + dr["TrainingPlanningId"].ToString() + ")'> " + dr["TrainingName"].ToString() + "</a></div></div>";
            //            }
                        
            //        }
            //        tc.Controls.Add(new LiteralControl(str));
            //}

            int calDayNum = e.Day.Date.Day;
            int calMonth = e.Day.Date.Month;


            e.Cell.Controls.Clear();
            TableCell tc = e.Cell;
            string strMain = "<div style='min-height:100px;border:dotted 1px green;'><div class='" + ((calMonth == Month) ? "_tr_dayheader_cmonth" : "_tr_dayheader_other") + "'>" + calDayNum + "</div>";

            foreach (DataRow dr in dt.Rows)
            {

                int TStartDay = Common.CastAsInt32(dr["StartDay"].ToString());
                int TEndDay = Common.CastAsInt32(dr["EndDay"].ToString());

                
                string str = "";
                if (calMonth == Month && calDayNum >= TStartDay && calDayNum <= TEndDay)
                {
                    if (dr["Status"].ToString() == "A")
                    {
                        str += "<div class='_tr_trainingboxplanned'><a href='#'onclick='ViewPlaning(" + dr["TrainingPlanningId"].ToString() + ")'> " + dr["TrainingName"].ToString() + "</a></div>";
                        
                    }
                    if (dr["Status"].ToString() == "C")
                    {
                        str += "<div class='_tr_trainingboxcanceled'><a href='#'onclick='ViewPlaning(" + dr["TrainingPlanningId"].ToString() + ")'> " + dr["TrainingName"].ToString() + "</a></div>";
                        
                    }
                    if (dr["Status"].ToString() == "E")
                    {
                        str += "<div class='_tr_trainingboxcompleted'><a href='#'onclick='ViewPlaning(" + dr["TrainingPlanningId"].ToString() + ")'> " + dr["TrainingName"].ToString() + "</a></div>";
                        
                    }
                }
                strMain += str;
            }
            strMain += "</div>";
            tc.Controls.Add(new LiteralControl(strMain));

        }
    }
    #endregion
    protected void btnBack_Click(object sender, EventArgs e)
    {
        pnlMonth.Visible = false;
        btnBack.Visible = false;
        pnlYear.Visible = true;
        btnPrint.Visible = true;
    }    
    protected void btnHidden_Click(object sender, EventArgs e)
    {
        int TrainingPlanningId = Common.CastAsInt32(txtHidden.Text);

        ScriptManager.RegisterStartupScript(this, this.GetType(), "asdf", "OpenPlanningDetails('" + TrainingPlanningId.ToString() + "');", true);
        
    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        BindYearTrainings();        
    }

}
