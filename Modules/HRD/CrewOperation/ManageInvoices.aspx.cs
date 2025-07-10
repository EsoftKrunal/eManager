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

public partial class CrewOperation_ManageInvoices : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        if (!IsPostBack)
        {
            BindVessel();
            BindTAgents();
            ShowCrewList();
        }
    }
    protected void ShowCrewList()
    {
        string WhereClause = "";
        string Inner_WhereClause = "";

        if (txtFDate.Text.Trim() != "" && txtFDate.Text.Trim() == "")
        {
            lblMsg.Text = "Please fill from & todate both. ";
            return; 
        }
        
        if (txtFDate.Text.Trim() != "")
        {
            WhereClause += "AND (";
            if(txtTDate.Text.Trim()=="")
                WhereClause += " ( InvoiceDate >='" + txtFDate.Text.Trim() + "' )";
            else
                WhereClause += " ( InvoiceDate >='" + txtFDate.Text.Trim() + "' AND InvoiceDate <='" + txtTDate.Text.Trim() + "' )";
            WhereClause += " )";
        }
        
        //------------------
        if (ddlVessel.SelectedIndex > 0)
        {
            WhereClause += " AND INV.VESSELID =" + ddlVessel.SelectedValue + "";
        }
        //------------------
        if (txtCrewNo.Text.Trim()!="")
        {
            Inner_WhereClause += " AND cc.CREWNUMBER LIKE '%" + txtCrewNo.Text.Trim() + "%'";
        }
        //------------------
        if (txtPNRNo.Text.Trim() != "")
        {
            Inner_WhereClause += " AND CPTD.PNR LIKE '%" + txtPNRNo.Text.Trim() + "%'";
        }
        //------------------
        if (ddlTA.SelectedIndex > 0)
        {
            WhereClause += " AND CPTD.TravelAgentId=" + ddlTA.SelectedValue + "";
        }

        string sql = "SELECT INVOICEID,INVOICENO,VESSELNAME,COMPANY,INVOICEDATE,RECEIVEDON, " +
                 "(SELECT SUM(AMOUNT) FROM dbo.CREWPORTCALLTRAVELDETAILS_INV_Details DET WHERE DET.INVOICEID=MST.INVOICEID) AS INVAMOUNT " +
                 "FROM dbo.CREWPORTCALLTRAVELDETAILS_INV MST " +
                 "INNER JOIN TRAVELAGENT TA ON TA.TRAVELAGENTID=MST.TRAVELAGENTID " +
                 "INNER JOIN VESSEL V ON MST.VESSELID=V.VESSELID " +
                 "WHERE MST.INVOICEID IN " +
                 "( " +
                 "    SELECT DISTINCT INVOICEID FROM  " +
                 "    dbo.CREWPORTCALLTRAVELDETAILS_INV_Details INV  " +
                 "    INNER JOIN dbo.CrewPortCallTravelDetails CPTD ON INV.CrewTravelId=CPTD.CrewTravelId " +
                 "    INNER JOIN CrewPersonalDetails cc ON cc.crewid=CPTD.crewid WHERE 1=1 " + Inner_WhereClause +
                 ") " + WhereClause;

        DataTable dt1 = Budget.getTable(sql).Tables[0];

        rpt_Data.DataSource = dt1;
        rpt_Data.DataBind();
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {

    }
    public void BindVessel()
    {
        DataTable dt = Budget.getTable("SELECT VESSELID,VESSELNAME FROM VESSEL WHERE VESSELSTATUSID<>2  ORDER BY VESSELNAME").Tables[0];
        ddlVessel.DataSource = dt;
        ddlVessel.DataTextField = "VESSELNAME";
        ddlVessel.DataValueField = "VESSELID";
        ddlVessel.DataBind();
        ddlVessel.Items.Insert(0, new ListItem("< ALL >", ""));
    }
    public void BindTAgents()
    {
        DataTable dt = Budget.getTable("SELECT TRAVELAGENTID,COMPANY FROM TRAVELAGENT").Tables[0];
        ddlTA.DataSource = dt;
        ddlTA.DataTextField = "COMPANY";
        ddlTA.DataValueField = "TRAVELAGENTID";
        ddlTA.DataBind();
        ddlTA.Items.Insert(0, new ListItem("< ALL >", ""));


        ddlInvTravelAgents.DataSource = dt;
        ddlInvTravelAgents.DataTextField = "COMPANY";
        ddlInvTravelAgents.DataValueField = "TRAVELAGENTID";
        ddlInvTravelAgents.DataBind();
        ddlInvTravelAgents.Items.Insert(0, new ListItem("< SELECT >", ""));
        
    }
    public DataTable BindCurrency()
    {
        DataTable dt = Budget.getTable("select DISTINCT FOR_CURR from dbo.xchangedaily ORDER BY FOR_CURR").Tables[0];
        dt.Rows.InsertAt(dt.NewRow(), 0);
        dt.Rows[0][0] = "";
        return dt;
    }
    protected void btnView_OnClick(object sender,EventArgs e) 
    {
        dvDetails.Visible = true;
        int CrewTravelId = 0;
        char[] sep={'|'};
        CrewTravelId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        
        //frmDetails.Attributes.Add("src", "ManageTickets_DetailPopup.aspx?CrewTravelId=" + CrewTravelId.ToString());
    }
    protected void btn_Close_Click(object sender, EventArgs e) 
    {
        ShowCrewList();
        dvDetails.Visible = false;
    }
    protected void btnInv_Click(object sender, EventArgs e)
    {
        dvDetails.Visible = true;
        
        string sql = "select CPTD.CrewTravelId,pp.portcallid,cc.crewid,cc.CrewNumber, " +
                  "cc.firstname + ' '+ cc.lastname as CrewName, " +
                  "(select rankcode from rank where rankid=cc.currentrankid)as rankname, " +
                  "(select Top 1 replace(Convert(varchar,cvh.signOnDate,106),' ','-') from crewonvesselhistory cvh where signonsignoff='I' and cc.crewid=cvh.crewid order by CrewOnVesselid Desc) as SignOnDate, " +
                  "(select Top 1 replace(Convert(varchar,cvh.ReliefDueDate,106),' ','-') from crewonvesselhistory cvh where signonsignoff='I' and cc.crewid=cvh.crewid order by CrewOnVesselid Desc) as ReliefDueDate, " +
                  "(null) as ExpectedJoinDate, " +
                  "pp.CrewFlag,CPTD.*,TicketStatusText=CASE WHEN CPTD.TICKETSTATUS='A' THEN 'Issued' WHEN CPTD.TICKETSTATUS='D' THEN 'Cancelled' ELSE '' END, " +
                  "ta.Company,v.vesselcode ,(SELECT COUNT(*) FROM CREWPORTCALLTRAVELDETAILS CPTD1 WHERE CPTD1.PORTCALLID=CPTD.PORTCALLID AND CPTD1.CREWID=CPTD.CREWID AND CPTD1.TICKETSTATUS IN ('A','C')) as NO_TICS " +
                  "from  " +
                  "PortCallDetail pp  " +
                  "INNER JOIN PortCallHeader ph ON ph.portcallid=pp.portcallid  " +
                  "INNER JOIN Vessel v ON v.vesselid=ph.vesselid  " +
                  "INNER JOIN CrewPersonalDetails cc ON cc.crewid=pp.crewid  " +
                  "LEFT JOIN CREWPORTCALLTRAVELDETAILS CPTD ON CPTD.PORTCALLID=pp.PortCallId AND CPTD.CREWID=PP.CREWID " +
                  "INNER JOIN TravelAgent ta ON ta.TravelAgentId=CPTD.TravelAgentId  " +
                  "WHERE CPTD.CrewTravelId NOT IN (SELECT CrewTravelId FROM CREWPORTCALLTRAVELDETAILS_INV_Details) ";
        Repeater2.DataSource = Budget.getTable(sql).Tables[0];
        Repeater2.DataBind();
       // frmDetails.Attributes.Add("src","");
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        ShowCrewList();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        
        
    }
}

