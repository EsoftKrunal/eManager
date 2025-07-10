using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Applicant_ViewCrewCheckList : System.Web.UI.Page
{
    int planningid
    {
        set { ViewState["planningid"] = value; }
        get { return Common.CastAsInt32(ViewState["planningid"]); }
    }

    int crewid
    {
        set { ViewState["crewid"]=value ;}
        get { return  Common.CastAsInt32(ViewState["crewid"]);}
    }

    int rankid
    {
        set { ViewState["rankid"]=value ;}
        get { return  Common.CastAsInt32(ViewState["rankid"]);}
    }

    int vesselid
    {
        set { ViewState["vesselid"]=value ;}
        get { return  Common.CastAsInt32(ViewState["vesselid"]);}
    }

    public int LoginId
    {
        set { ViewState["LoginId"] = value; }
        get { return Common.CastAsInt32(ViewState["LoginId"]); }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        planningid = Common.CastAsInt32(Request.QueryString["_P"]);
        if (Convert.ToString(Request.QueryString["_M"]) == "V")
        {
            btnSave.Visible = false; 
        }
        if(!IsPostBack)
            ShowData(planningid);
    }
    protected void ShowData(int planningid)
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("SELECT RELIEVERID,VESSELID,RELIEVERRANKID FROM DBO.CREWVESSELPLANNINGHISTORY WHERE PlanningId=" + planningid.ToString());
        if (dt.Rows.Count > 0)
        {
            crewid= Common.CastAsInt32(dt.Rows[0][0]);
            vesselid = Common.CastAsInt32(dt.Rows[0][1]);
            rankid = Common.CastAsInt32(dt.Rows[0][2]);
            LoginId = Common.CastAsInt32(Session["LoginId"].ToString());
        }
         if (crewid > 0 && rankid > 0 && vesselid > 0)
        {

        string SQl = "SELECT CREWNUMBER , FIRSTNAME + ' ' +  isnull(MIDDLENAME,'') + ' ' + LASTNAME AS CREWNAME, " +
                    "(SELECT VESSELNAME FROM VESSEL WHERE VESSELID=" + vesselid.ToString() + ") AS VESSELNAME, " +
                    "(SELECT RANKNAME FROM RANK WHERE RANKID=" + rankid.ToString() + ") AS RANKNAME, " +
                    "(SELECT COUNTRYNAME FROM COUNTRY WHERE COUNTRYID=nationalityid) AS NATIONALITY, " +
                    "replace(convert(varchar,DateOfBirth,106) ,' ','-') as DOB " +
                    "FROM CREWPERSONALDETAILS WHERE CREWID=" + crewid.ToString();
        DataTable DT = Common.Execute_Procedures_Select_ByQueryCMS(SQl);
        if (DT.Rows.Count > 0)
        {
            lblID.Text = DT.Rows[0]["CREWNUMBER"].ToString();
            lblName.Text = DT.Rows[0]["CREWNAME"].ToString();
            lblRank.Text = DT.Rows[0]["RANKNAME"].ToString();
            lblNationality.Text = DT.Rows[0]["NATIONALITY"].ToString();
            lblDOB.Text = DT.Rows[0]["DOB"].ToString();
            lblVName.Text = DT.Rows[0]["VESSELNAME"].ToString(); 
            
        }

        Common.Set_Procedures("DBO.GET_CREW_REQ_DOCUMENTS");
        Common.Set_ParameterLength(1);
        Common.Set_Parameters(new MyParameter("@PLANNINGID", planningid));

        DataSet ds = Common.Execute_Procedures_Select();

        Repeater1.DataSource = ds.Tables[0];
        Repeater1.DataBind();

        Repeater2.DataSource = ds.Tables[1];
        Repeater2.DataBind();

        Repeater3.DataSource = ds.Tables[2];
        Repeater3.DataBind();

        Repeater4.DataSource = ds.Tables[3];
        Repeater4.DataBind();

        Repeater5.DataSource = ds.Tables[4];
        Repeater5.DataBind();
        }

        //DataTable DT = Common.Execute_Procedures_Select_ByQueryCMS("SELECT (SELECT FIRSTNAME+' '+LASTNAME FROM USERLOGIN WHERE LOGINID=CHECKEDBY) AS CHECKEDBY,CHECKEDON from CREW_Doc_CheckList CDC WHERE CDC.CREWID=" + CrewId.ToString());
        //if (DT.Rows.Count > 0)
        //{
        //   // lblMess.Text = DT.Rows[0]["CHECKEDBY"].ToString() + " / " + Common.ToDateString(DT.Rows[0]["CHECKEDON"].ToString());
        //}
    }
    protected string ExpireCheck(object date)
    {
        string ret = "";
        try
        {
            DateTime d1 = Convert.ToDateTime(date);
            if (d1 <= DateTime.Today) // fully expired
            {
                ret ="<span style='color:red'>" + Common.ToDateString(d1) + "</span>";
            }
            else if (d1 <= DateTime.Today.AddMonths(-1)) // will expired
            {
                ret = "<span style='color:orange'>" + Common.ToDateString(d1) + "</span>";
            }
            else
            {
                ret = Common.ToDateString(d1); 
            }
        }
        catch
        {
        }
        return ret; 
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        Repeater[] RPS = { Repeater1, Repeater2, Repeater3, Repeater4, Repeater5 };

        foreach (Repeater R in RPS)
        {
            foreach (RepeaterItem ri in R.Items)
            {
                CheckBox ch = (CheckBox)ri.FindControl("ckh_L");
                char[] c = { '|' };
                string[] ps = ch.ToolTip.Split(c);
                TextBox tx = (TextBox)ri.FindControl("txtRem");
                if (!(ch.Checked))
                {
                    if (tx.Text.Trim() == "")
                    {
                        lblMess.Text = "&nbsp;Remarks are manditory if not checked.";
                        tx.Focus();
                        return;
                    }
                }
            }
        }
        int INDEX = 1;
        foreach (Repeater R in RPS)
        {
            foreach (RepeaterItem ri in R.Items)
            {
                CheckBox ch = (CheckBox)ri.FindControl("ckh_L");
                char[] c = { '|' };
                int DnameId=Common.CastAsInt32(ch.ToolTip);
                TextBox tx = (TextBox)ri.FindControl("txtRem");

                if (ch.Checked)
                {
                    Common.Execute_Procedures_Select_ByQueryCMS("UPDATE CREW_Doc_CheckList SET STATUS='Y',REMARK='" + tx.Text.Trim().Replace("'", "''") + "',CHECKEDBY=" + LoginId.ToString() + ",CHECKEDON=getdate() WHERE PLANNINGID=" + planningid + " AND CREWID=" + crewid + " AND DOCUMENTTYPEID=" + INDEX + " AND DOCUMENTNAMEID=" + DnameId); 
                }
                else
                {
                    Common.Execute_Procedures_Select_ByQueryCMS("UPDATE CREW_Doc_CheckList SET STATUS='N',REMARK='" + tx.Text.Trim().Replace("'", "''") + "',CHECKEDBY=" + LoginId.ToString() + ",CHECKEDON=getdate() WHERE PLANNINGID=" + planningid + " AND CREWID=" + crewid + " AND DOCUMENTTYPEID=" + INDEX + " AND DOCUMENTNAMEID=" + DnameId);
                }
            }
            INDEX++;
        }
        //--------------------------------
        lblMess.Text = "&nbsp;Record Saved successfully.";
        Page.ClientScript.RegisterStartupScript(this.GetType(), "dialog", "window.opener.document.getElementById('ctl00_ContentMainMaster_btnRefresh').click();alert('Record Saved successfully.');window.close();", true);
    }
}