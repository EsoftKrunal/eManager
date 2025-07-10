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

public partial class CrewOperation_Crew_Required_Docs : System.Web.UI.Page
{
    public int CrewId
    {
        set{ ViewState["CrewId"]=value;}
        get{ return Common.CastAsInt32(ViewState["CrewId"]);}
    }
    public int VesselId 
    {
        set { ViewState["VesselId"] = value; }
        get { return Common.CastAsInt32(ViewState["VesselId"]); }
    }
    public int RankId 
    {
        set { ViewState["RankId"] = value; }
        get { return Common.CastAsInt32(ViewState["RankId"]); }
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
        if (!IsPostBack)
        {
            CrewId = Common.CastAsInt32(Request.QueryString["CrewId"].ToString());
            VesselId = Common.CastAsInt32(Request.QueryString["VesselId"].ToString());
            RankId = Common.CastAsInt32(Request.QueryString["RankId"].ToString());
            LoginId = Common.CastAsInt32(Session["LoginId"].ToString());

            string SQl = "SELECT '[' + CREWNUMBER + '] ' + FIRSTNAME + ' ' +  MIDDLENAME + ' ' + LASTNAME AS CREWNAME, " +
                        "(SELECT VESSELNAME FROM VESSEL WHERE VESSELID=" + VesselId.ToString() + ") AS VESSELNAME, " +
                        "(SELECT RANKNAME FROM RANK WHERE RANKID=" + RankId.ToString() + ") AS RANKNAME " +
                        "FROM CREWPERSONALDETAILS WHERE CREWID=" + CrewId.ToString();
            DataTable DT = Common.Execute_Procedures_Select_ByQueryCMS(SQl); 
            if(DT.Rows.Count >0)
            {
                lblheader.Text = " <b>CREW MEMBER :</b> " + DT.Rows[0][0].ToString();
                lblheader1.Text = " <b>VESSEL :</b>" + DT.Rows[0][1].ToString();
                lblheader2.Text = " <b>RANK :</b> " + DT.Rows[0][2].ToString(); 
            }
            BindGrid();
        }
    }
    protected void BindGrid()
    {
        Common.Set_Procedures("DBO.GET_CREW_REQ_DOCUMENTS");
        Common.Set_ParameterLength(3);
        Common.Set_Parameters(new MyParameter("@CREWID",CrewId) ,new MyParameter("@RANKID",RankId),new MyParameter("@VESSELID",VesselId));

        DataSet ds = Common.Execute_Procedures_Select();

        rptL.DataSource = ds.Tables[0];
        rptL.DataBind();

        rptC.DataSource = ds.Tables[1];
        rptC.DataBind();

        rptE.DataSource = ds.Tables[2];
        rptE.DataBind();

        rptT.DataSource = ds.Tables[3];
        rptT.DataBind();

        rptM.DataSource = ds.Tables[4];
        rptM.DataBind();

        DataTable DT = Common.Execute_Procedures_Select_ByQueryCMS("SELECT (SELECT FIRSTNAME+' '+LASTNAME FROM USERLOGIN WHERE LOGINID=CHECKEDBY) AS CHECKEDBY,CHECKEDON from CREW_Doc_CheckList CDC WHERE CDC.CREWID=" + CrewId.ToString());
        if (DT.Rows.Count > 0)
        {
            lblMess.Text = DT.Rows[0]["CHECKEDBY"].ToString() + " / " + Common.ToDateString(DT.Rows[0]["CHECKEDON"].ToString());
        }
    
    }
    protected void btnSaveCC_OnClick(object sender, EventArgs e)
    {
        Repeater[] RPS = { rptL, rptC, rptE, rptT, rptM };

        foreach (Repeater R in RPS)
        {
            foreach (RepeaterItem ri in R.Items)
            {
                CheckBox ch = (CheckBox)ri.FindControl("ckh_L");
                char[] c = { '|' };
                string[] ps = ch.ToolTip.Split(c);
                TextBox tx = (TextBox)ri.FindControl("txt_Rems");
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


        Common.Execute_Procedures_Select_ByQueryCMS("DELETE FROM CREW_Doc_CheckList WHERE CREWID=" + CrewId.ToString());
        foreach (Repeater R in RPS)
        {
            foreach (RepeaterItem ri in R.Items)
            {
                CheckBox ch = (CheckBox)ri.FindControl("ckh_L");
                char[] c = { '|' };
                string[] ps = ch.ToolTip.Split(c);
                TextBox tx = (TextBox)ri.FindControl("txt_Rems");

                if (ch.Checked)
                {
                    Common.Execute_Procedures_Select_ByQueryCMS("INSERT INTO CREW_Doc_CheckList(CREWID,DOCUMENTTYPEID,DOCUMENTNAMEID,STATUS,REMARK,CHECKEDBY,CHECKEDON) VALUES(" + CrewId.ToString() + "," + ps[0] + "," + ps[1] + ",'Y','" + tx.Text.Trim().Replace("'", "''") + "'," + LoginId.ToString() + ",GETDATE())");
                }
                else
                {
                    Common.Execute_Procedures_Select_ByQueryCMS("INSERT INTO CREW_Doc_CheckList(CREWID,DOCUMENTTYPEID,DOCUMENTNAMEID,STATUS,REMARK,CHECKEDBY,CHECKEDON) VALUES(" + CrewId.ToString() + "," + ps[0] + "," + ps[1] + ",'N','" + tx.Text.Trim().Replace("'", "''") + "'," + LoginId.ToString() + ",GETDATE())");
                }
            }
        }
        //--------------------------------
        lblMess.Text = "&nbsp;Record Saved successfully.";
    }
}
