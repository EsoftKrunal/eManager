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
                ShowHeaderData();
                ShowCrewList();
            }
        }
    }
    protected void ShowHeaderData()
    {
        DataTable dt=Budget.getTable("SELECT * FROM PORTCALLHEADER WHERE PORTCALLID=" + PortCallId.ToString()).Tables[0];
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
                    "pp.CrewFlag,(SELECT COUNT(*) FROM CREWPORTCALLTRAVELDETAILS CPTD1 WHERE CPTD1.PORTCALLID=pp.PORTCALLID AND CPTD1.CREWID=pp.CREWID AND CPTD1.TICKETSTATUS IN ('A','C')) as NO_TICS " +
                    "from " +
                    "PortCallDetail pp " +
                    "INNER JOIN CrewPersonalDetails cc ON cc.crewid=pp.crewid " +
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
    public DataTable BindTAgents()
    {
        DataTable dt=Budget.getTable("SELECT TRAVELAGENTID,COMPANY FROM TRAVELAGENT").Tables[0];
        dt.Rows.InsertAt(dt.NewRow(),0);
        dt.Rows[0][0] = "0";
        dt.Rows[0][1] = "";
        return dt;
    }
    public DataTable BindCurrency()
    {
        DataTable dt=Budget.getTable("select DISTINCT FOR_CURR from dbo.xchangedaily ORDER BY FOR_CURR").Tables[0];
        dt.Rows.InsertAt(dt.NewRow(), 0);
        dt.Rows[0][0] = "";
        return dt; 
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        Boolean NoSelected=true;
        foreach (GridViewRow gr in gvsearch.Rows)
        {
            bool IsSelected = ((CheckBox)gr.FindControl("chkSelect")).Checked;
            if(IsSelected) {
                NoSelected=false;
                // CHECKS
                DropDownList ddl_TA = ((DropDownList)gr.FindControl("ddl_TA"));
                if (ddl_TA.SelectedIndex <= 0)
                {
                    lblMsg.Text = "Please select travel agent. ";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "document.getElementById('" + ddl_TA.ClientID + "').focus();", true);
                    return; 
                }

                TextBox txt_Airline = ((TextBox)gr.FindControl("txt_Airline"));
                if (txt_Airline.Text.Trim() == "")
                {
                    lblMsg.Text = "Please fill airline name. ";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "document.getElementById('" + txt_Airline.ClientID + "').focus();", true);
                    return;
                }

                TextBox txt_FA=((TextBox)gr.FindControl("txt_FA"));
                if (txt_FA.Text.Trim()=="")
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

                //DropDownList ddl_Class = ((DropDownList)gr.FindControl("ddl_Class"));
                //if (ddl_Class.SelectedIndex <= 0)
                //{
                //    lblMsg.Text = "Please select class. ";
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "document.getElementById('" + ddl_Class.ClientID + "').focus();", true);
                //    return;
                //}

                TextBox txt_PNR = ((TextBox)gr.FindControl("txt_PNR"));
                if (txt_PNR.Text.Trim() == "")
                {
                    lblMsg.Text = "Please fill PNR#. ";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "document.getElementById('" + txt_PNR.ClientID + "').focus();", true);
                    return;
                }

                TextBox txt_BookingDate = ((TextBox)gr.FindControl("txt_Bdate"));
                if (txt_BookingDate.Text.Trim() == "")
                {
                    lblMsg.Text = "Please fill Boking Date. ";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "document.getElementById('" + txt_BookingDate.ClientID + "').focus();", true);
                    return;
                }

                DropDownList ddl_Curr = ((DropDownList)gr.FindControl("ddl_Curr"));
                if (ddl_Curr.SelectedIndex <= 0)
                {
                    lblMsg.Text = "Please select currency. ";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "document.getElementById('" + ddl_Curr.ClientID + "').focus();", true);
                    return;
                }

                TextBox txt_Amount = ((TextBox)gr.FindControl("txt_Amount"));
                if (txt_Amount.Text.Trim() == "")
                {
                    lblMsg.Text = "Please fill fair amount. ";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "document.getElementById('" + txt_Amount.ClientID + "').focus();", true);
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
            foreach (GridViewRow gr in gvsearch.Rows)
            {
                bool IsSelected = ((CheckBox)gr.FindControl("chkSelect")).Checked;
                if (IsSelected)
                {
                    HiddenField hfd_CrewId = ((HiddenField)gr.FindControl("HiddencrewIdsignoff"));
                    HiddenField hfd_CrewFlag = ((HiddenField)gr.FindControl("HfdCrewFlag"));
                    
                    DropDownList ddl_TA = ((DropDownList)gr.FindControl("ddl_TA"));
                    TextBox txt_Airline = ((TextBox)gr.FindControl("txt_Airline"));
                    TextBox txt_FA = ((TextBox)gr.FindControl("txt_FA"));
                    TextBox txt_TA = ((TextBox)gr.FindControl("txt_TA"));
                    TextBox txt_DepDate = ((TextBox)gr.FindControl("txt_Date"));
                    //DropDownList ddl_Class = ((DropDownList)gr.FindControl("ddl_Class"));
                    TextBox txt_PNR = ((TextBox)gr.FindControl("txt_PNR"));
                    TextBox txt_BookingDate = ((TextBox)gr.FindControl("txt_Bdate"));
                    DropDownList ddl_Curr = ((DropDownList)gr.FindControl("ddl_Curr"));
                    TextBox txt_Amount = ((TextBox)gr.FindControl("txt_Amount"));
                    //----------------------
                    decimal LC_Amount = Common.CastAsDecimal(txt_Amount.Text);

                    //------------------
                    decimal Rate = 0;
                    decimal USD = 0;

                    string sql = "select TOP 1 EXC_RATE from dbo.xchangedaily WHERE RATEDATE<='" + txt_BookingDate.Text.Trim() + "' AND FOR_CURR='" + ddl_Curr.SelectedValue + "' ORDER BY RATEDATE DESC";
                    DataTable dt_1=Common.Execute_Procedures_Select_ByQuery(sql);
                    if (dt_1.Rows.Count > 0)
                        Rate = Common.CastAsDecimal(dt_1.Rows[0][0].ToString());
                    USD = LC_Amount/Rate;

                    //-------------
                    sql = "sp_INSERT_PORTCALL_CREWTRAVELDETAILS " + PortCallId.ToString() + "," + hfd_CrewId.Value.ToString() + ",'" + hfd_CrewFlag.Value.ToString() + "'," +
                                 ddl_TA.SelectedValue + ",'" + txt_Airline.Text.Trim() + "','" + txt_FA.Text.Trim() + "','" + txt_TA.Text.Trim() + "','" + txt_DepDate.Text.Trim() + "','" + txt_PNR.Text.Trim() + "','','A','" + ddl_Curr.SelectedValue + "'," + string.Format("{0:0.00}", LC_Amount) + "," + string.Format("{0:0.00}", Rate) + "," + string.Format("{0:0.00}", USD) + ",'" + txt_BookingDate.Text.Trim() + "'";

                    Common.Execute_Procedures_Select_ByQueryCMS(sql);
                    lblMsg.Text="Record Saved Successfully";
                }
            }
            ShowCrewList();
        }
    }
}

