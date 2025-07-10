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
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Runtime.Remoting.Messaging;

public partial class CrewOperation_OrderTickets : System.Web.UI.Page
{
    public int PortCallId
    {
        get { return Common.CastAsInt32(ViewState["PortCallId"]); }
        set { ViewState["PortCallId"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        if (!IsPostBack)
        {
            if (Request.QueryString["PortCallId"] != null)
            {
                PortCallId = Common.CastAsInt32(Request.QueryString["PortCallId"]);
                BindTAgents();
                ShowHeaderData();
                ShowCrewList();
            }
        }
    }
    protected void ShowHeaderData()
    {
        DataTable dt=Budget.getTable("SELECT * FROM PORTCALLHEADER with(nolock) WHERE PORTCALLID=" + PortCallId.ToString()).Tables[0];
        lblPortRefNO.Text = dt.Rows[0]["PORTREFERENCENUMBER"].ToString();
    }
    protected void ShowCrewList()
    {
        string sql="select pp.portcallid,cc.crewid,cc.CrewNumber, " +
                    "cc.firstname + ' '+ cc.lastname as CrewName, " +
                    "(select rankcode from rank where rankid=cc.currentrankid)as rankname, " +
                    "(select Top 1 replace(Convert(varchar,cvh.signOnDate,106),' ','-') from crewonvesselhistory cvh where signonsignoff='I' and cc.crewid=cvh.crewid order by CrewOnVesselid Desc) as SignOnDate, " +
                    "(select Top 1 replace(Convert(varchar,cvh.ReliefDueDate,106),' ','-') from crewonvesselhistory cvh where signonsignoff='I' and cc.crewid=cvh.crewid order by CrewOnVesselid Desc) as ReliefDueDate, " +
                    "(null) as ExpectedJoinDate, " +
                    "pp.CrewFlag,(SELECT COUNT(*) FROM CREWPORTCALLTRAVELDETAILS CPTD1 WHERE CPTD1.PORTCALLID=pp.PORTCALLID AND CPTD1.CREWID=pp.CREWID AND CPTD1.TICKETSTATUS IN ('A','C')) as NO_TICS,'' As FromAirport,'' As ToAirPort, ''  As  DeptDate, '' As Remarks " +
                    "from " +
                    "PortCallDetail pp with(nolock) " +
                    "INNER JOIN CrewPersonalDetails cc with(nolock) ON cc.crewid=pp.crewid " +
                    "WHERE pp.PortCallId=" + PortCallId.ToString() + " and pp.crewId=cc.CrewId ORDER BY pp.CrewFlag";

        DataTable dt1 = Budget.getTable(sql).Tables[0];

        this.gvsearch.DataSource = dt1;
        this.gvsearch.DataBind();
    }
    protected void gvsearch_OnPreRender(object sender, EventArgs e)
    {
        foreach (GridViewRow gr in gvsearch.Rows)
        {
                if ((((HiddenField)gr.FindControl("HfdCrewFlag")).Value.Trim() == "I")) // SIGN ON MEMBERS ( CHECKLIST / CONTRACT / SIGNON )
                {
                    gr.BackColor = System.Drawing.Color.FromName("#99FFCC"); // SIGN ON
                }
                else
                {
                    gr.BackColor = System.Drawing.Color.FromName("#FFCCCC"); // SIGN OFF MEMBERS ( SIGN OFF )
                }
        }
        
    }
    protected void BindTAgents()
    {
        DataTable dt=Budget.getTable("SELECT TRAVELAGENTID,COMPANY FROM TRAVELAGENT with(nolock) ").Tables[0];
        ddl_TA.DataSource = dt;
        ddl_TA.DataTextField = "COMPANY";
        ddl_TA.DataValueField = "TRAVELAGENTID";
        ddl_TA.DataBind();
        ddl_TA.Items.Insert(0, new ListItem("Select", "0"));
    }
   
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddl_TA.SelectedIndex <= 0)
            {
                lblMsg.Text = "Please select travel agent. ";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "document.getElementById('" + ddl_TA.ClientID + "').focus();", true);
                return;
            }
            Boolean NoSelected = true;

            foreach (GridViewRow gr in gvsearch.Rows)
            {
                bool IsSelected = ((CheckBox)gr.FindControl("chkSelect")).Checked;
                if (IsSelected)
                {
                    NoSelected = false;
                    // CHECKS





                    TextBox txt_FA = ((TextBox)gr.FindControl("txt_FA"));
                    if (txt_FA.Text.Trim() == "")
                    {
                        lblMsg.Text = "Please fill from airport. ";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "document.getElementById('" + txt_FA.ClientID + "').focus();", true);
                        return;
                    }
                    TextBox txt_TA = ((TextBox)gr.FindControl("txt_TA"));
                    if (txt_FA.Text.Trim() == "")
                    {
                        lblMsg.Text = "Please fill to airport. ";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "document.getElementById('" + txt_TA.ClientID + "').focus();", true);
                        return;
                    }
                    TextBox txt_DepDate = ((TextBox)gr.FindControl("txt_Date"));
                    if (txt_DepDate.Text.Trim() == "")
                    {
                        lblMsg.Text = "Please fill dept date. ";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "document.getElementById('" + txt_DepDate.ClientID + "').focus();", true);
                        return;
                    }


                }
            }
            if (NoSelected)
            {
                lblMsg.Text = "Please select crew members to create ticket. ";
                return;
            }
            else
            {
                string CrewList = "";
                foreach (GridViewRow gr in gvsearch.Rows)
                {

                    bool IsSelected = ((CheckBox)gr.FindControl("chkSelect")).Checked;
                    if (IsSelected)
                    {

                        HiddenField hfd_CrewId = ((HiddenField)gr.FindControl("HiddencrewIdsignoff"));
                        CrewList += "," + hfd_CrewId.Value;
                        HiddenField hfd_CrewFlag = ((HiddenField)gr.FindControl("HfdCrewFlag"));
                        //  DropDownList ddl_TA = ((DropDownList)gr.FindControl("ddl_TA"));
                        TextBox txt_FA = ((TextBox)gr.FindControl("txt_FA"));
                        TextBox txt_TA = ((TextBox)gr.FindControl("txt_TA"));
                        TextBox txt_DepDate = ((TextBox)gr.FindControl("txt_Date"));
                        TextBox txt_Remarks = ((TextBox)gr.FindControl("txt_remarks"));
                        //DropDownList ddl_Class = ((DropDownList)gr.FindControl("ddl_Class"));
                        //-------------
                        string sql = "sp_INSERT_PORTCALL_SendTravelDetails " + PortCallId.ToString() + "," + hfd_CrewId.Value.ToString() + ",'" + hfd_CrewFlag.Value.ToString() + "'," +
                                     ddl_TA.SelectedValue + ",'" + txt_FA.Text.Trim() + "','" + txt_TA.Text.Trim() + "','" + txt_DepDate.Text.Trim() + "','" + txt_Remarks.Text.Trim() + "',"+ Convert.ToInt32(Session["loginid"].ToString()) + "";

                        Common.Execute_Procedures_Select_ByQueryCMS(sql);
                        lblMsg.Text = "Record Saved Successfully";
                    }
                }


                if (!string.IsNullOrWhiteSpace(CrewList) && CrewList.StartsWith(","))
                    CrewList = CrewList.Substring(1);

                Session["TicketReqPortCallId"] = PortCallId;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "travel", "window.open('CrewMailSend.aspx?TicketReqPortCallId=" + Session["TicketReqPortCallId"].ToString() + "&CrewList=" + CrewList + "&mode=4');", true);
                //ShowCrewList();
            }
        }
        catch(Exception ex)
        {
            lblMsg.Text = ex.Message.ToString();
        }
    }

   

    protected void ddl_TA_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToInt32(ddl_TA.SelectedValue) > 0 )
            {
                ShowCrewTicketBookingRequest();
            }
            else
            {
                ShowCrewList();
            }
            
        }
        catch(Exception ex)
        {
            lblMsg.Text = ex.Message.ToString();
        }
    }

    protected void ShowCrewTicketBookingRequest()
    {
        string sql = "select pp.portcallid,cc.crewid,cc.CrewNumber, " +
                    "cc.firstname + ' '+ cc.lastname as CrewName, " +
                    "(select rankcode from rank where rankid=cc.currentrankid)as rankname, " +
                    "(select Top 1 replace(Convert(varchar,cvh.signOnDate,106),' ','-') from crewonvesselhistory cvh where signonsignoff='I' and cc.crewid=cvh.crewid order by CrewOnVesselid Desc) as SignOnDate, " +
                    "(select Top 1 replace(Convert(varchar,cvh.ReliefDueDate,106),' ','-') from crewonvesselhistory cvh where signonsignoff='I' and cc.crewid=cvh.crewid order by CrewOnVesselid Desc) as ReliefDueDate, " +
                    "(null) as ExpectedJoinDate, " +
                    "pp.CrewFlag,(SELECT COUNT(*) FROM CREWPORTCALLTRAVELDETAILS CPTD1 WHERE CPTD1.PORTCALLID=pp.PORTCALLID AND CPTD1.CREWID=pp.CREWID AND CPTD1.TICKETSTATUS IN ('A','C')) as NO_TICS,cpc.FromAirport As FromAirport,cpc.ToAirPort As ToAirPort, REPLACE(CONVERT(CHAR(11), cpc.DeptDate, 106),' ',' - ')  As  DeptDate, cpc.Remarks  " +
                    "from " +
                    "PortCallDetail pp with(nolock) " +
                    "INNER JOIN CrewPersonalDetails cc with(nolock) ON cc.crewid=pp.crewid " +
                    "LEFT OUTER JOIN CrewPortCallSendTravelDetails cpc with(nolock) ON cc.crewid=cpc.crewid and pp.portcallid = cpc.PORTCALLID  and cpc.TRAVELAGENTID = " +ddl_TA.SelectedValue.ToString()+ " " +
                    "WHERE pp.PortCallId=" + PortCallId.ToString() + " and pp.crewId=cc.CrewId ORDER BY pp.CrewFlag";

        DataTable dt1 = Budget.getTable(sql).Tables[0];

        this.gvsearch.DataSource = dt1;
        this.gvsearch.DataBind();
    }
}

