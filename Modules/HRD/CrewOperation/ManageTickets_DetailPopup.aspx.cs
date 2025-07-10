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

public partial class CrewOperation_ManageTickets_DetailPopup : System.Web.UI.Page
{
    int CrewTravelId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        CrewTravelId = Common.CastAsInt32(Request.QueryString["CrewTravelId"]);
        if (!IsPostBack)
        {
            ShowDetails();
        }
    }
    public void ShowDetails()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("SELECT * FROM PORTCALLHEADER WHERE PORTCALLID IN (SELECT PORTCALLID FROM CREWPORTCALLTRAVELDETAILS WHERE CREWTRAVELID=" + CrewTravelId.ToString() + ")");
        if (dt.Rows.Count > 0)
        {
            lblPortCallNo.Text = dt.Rows[0]["PortReferenceNumber"].ToString();
        }
        string SQL=" select " +
                   " cc.firstname + ' '+ cc.lastname as CrewName,  " +
                   " (select rankcode from rank where rankid=cc.currentrankid)as Rankname,  " +
                   " pp.CrewFlag, " +
                   " ta.Company, " +
                   " v.vesselcode,  " +
                   " CPTD.*, " +
                   " TicketStatusText=CASE WHEN CPTD.TICKETSTATUS='A' THEN 'Issued' WHEN CPTD.TICKETSTATUS='D' THEN 'Cancelled' ELSE '' END, " +
                   " (SELECT invoiceID FROM CREWPORTCALLTRAVELDETAILS_INV_Details WHERE CREWTRAVELID=CPTD.CrewTravelId) AS INVID " +
                   " from   " +
                   " PortCallDetail pp   " +
                   " INNER JOIN PortCallHeader ph ON ph.portcallid=pp.portcallid   " +
                   " INNER JOIN Vessel v ON v.vesselid=ph.vesselid   " +
                   " INNER JOIN CrewPersonalDetails cc ON cc.crewid=pp.crewid   " +
                   " INNER JOIN CREWPORTCALLTRAVELDETAILS CPTD ON CPTD.PORTCALLID=pp.PortCallId AND CPTD.CREWID=PP.CREWID  " +
                   " INNER JOIN TravelAgent ta ON ta.TravelAgentId=CPTD.TravelAgentId   " +
                   " WHERE CPTD.CrewTravelId=" + CrewTravelId.ToString();

        dt = Common.Execute_Procedures_Select_ByQueryCMS(SQL);
        if (dt.Rows.Count > 0)
        {
            lblCrewName.Text = dt.Rows[0]["CrewName"].ToString();
            lblRankName.Text = dt.Rows[0]["Rankname"].ToString();
            lblTAName.Text = dt.Rows[0]["Company"].ToString();
            lblVessel.Text = dt.Rows[0]["vesselcode"].ToString();
            lblSource.Text = dt.Rows[0]["FromAirport"].ToString();
            lblDest.Text = dt.Rows[0]["ToAirport"].ToString();
            lblDeptDate.Text =Common.ToDateString(dt.Rows[0]["DeptDate"]);
            lblPNR.Text = dt.Rows[0]["PNR"].ToString();
            lblClass.Text = (dt.Rows[0]["Class"].ToString()=="E")?"Executive":((dt.Rows[0]["Class"].ToString()=="B")?"Business":"Economy");
            lblCurrency.Text = dt.Rows[0]["Currency"].ToString();

            lblLC1.Text = lblCurrency.Text;
            lblLC2.Text = lblCurrency.Text;
            lblLC3.Text = lblCurrency.Text;
            lblLC4.Text = lblCurrency.Text;
            lblLC5.Text = lblCurrency.Text;


            lbl_B_Date.Text = Common.ToDateString(dt.Rows[0]["BookingDate"]);
            lbl_B_LC.Text = dt.Rows[0]["Booking_LC"].ToString();
            lbl_B_Rate.Text = dt.Rows[0]["Boking_Rate"].ToString();
            lbl_B_Amount.Text = dt.Rows[0]["Booking_USD"].ToString();

            lbl_H_Date.Text = Common.ToDateString(dt.Rows[0]["ChangeDate"]);
            lbl_H_LC.Text = dt.Rows[0]["Change_LC"].ToString();
            lbl_H_Rate.Text = dt.Rows[0]["Change_Rate"].ToString();
            lbl_H_Amount.Text = dt.Rows[0]["Change_USD"].ToString();

            lbl_C_Date.Text = Common.ToDateString(dt.Rows[0]["CancellationDate"]);
            lbl_C_LC.Text = dt.Rows[0]["Cancellation_LC"].ToString();
            lbl_C_Rate.Text = dt.Rows[0]["Cancellation_Rate"].ToString();
            lbl_C_Amount.Text = dt.Rows[0]["Cancellation_USD"].ToString();

            lbl_R_Date.Text = Common.ToDateString(dt.Rows[0]["Refund_Date"]);
            lbl_R_LC.Text = dt.Rows[0]["Refund_LC"].ToString();
            lbl_R_Rate.Text = dt.Rows[0]["Refund_Rate"].ToString();
            lbl_R_Amount.Text = dt.Rows[0]["Refund_USD"].ToString();

            lbl_F_LC.Text = dt.Rows[0]["Final_LC"].ToString();
            lbl_F_Amount.Text = dt.Rows[0]["Final_USD"].ToString();

            if (dt.Rows[0]["TicketStatus"].ToString().Trim() == "D" || Common.CastAsInt32(dt.Rows[0]["INVID"]) > 0 )
            {
                btnCancelTic.Visible = false;
                btnChangeTic.Visible = false;
            }
        }
        
    }
    //public void BindVessel()
    //{
    //    DataTable dt = Budget.getTable("SELECT VESSELID,VESSELNAME FROM VESSEL WHERE VESSELSTATUSID<>2  ORDER BY VESSELNAME").Tables[0];
    //    ddlVessel.DataSource = dt;
    //    ddlVessel.DataTextField = "VESSELNAME";
    //    ddlVessel.DataValueField = "VESSELID";
    //    ddlVessel.DataBind();
    //    ddlVessel.Items.Insert(0, new ListItem("< ALL >", ""));
    //}
    //public void BindTAgents()
    //{
    //    DataTable dt = Budget.getTable("SELECT TRAVELAGENTID,COMPANY FROM TRAVELAGENT").Tables[0];
    //    ddlTA.DataSource = dt;
    //    ddlTA.DataTextField = "COMPANY";
    //    ddlTA.DataValueField = "TRAVELAGENTID";
    //    ddlTA.DataBind();
    //    ddlTA.Items.Insert(0, new ListItem("< ALL >", ""));
    //}
    //public DataTable BindCurrency()
    //{
    //    DataTable dt = Budget.getTable("select DISTINCT FOR_CURR from dbo.xchangedaily ORDER BY FOR_CURR").Tables[0];
    //    dt.Rows.InsertAt(dt.NewRow(), 0);
    //    dt.Rows[0][0] = "";
    //    return dt;
    //}
    protected void btn_ChangeTic_Click(object sender, EventArgs e)
    {
        lblM.Text = "";

        btn_Change_Submit.Visible =true;
        btn_Cancel_Submit.Visible = false;

        dv_ChangeCancel.Visible = true;

        tbl_Change.Visible = true;
        tblCancel.Visible = false; 
        
        lblBoxName.Text = "Change Ticket";

        txt_H_OldDate.Text = lblDeptDate.Text;
        txt_H_OldAmt.Text = lbl_F_LC.Text;

        txt_H_NewDate.Text = "";
        txt_H_ChangeAmt.Text = "0";
    }
    protected void btn_CancelTic_Click(object sender, EventArgs e)
    {
        lblM.Text = "";

        btn_Change_Submit.Visible = false;
        btn_Cancel_Submit.Visible = true;
        
        dv_ChangeCancel.Visible = true;

        tblCancel.Visible = true;
        tbl_Change.Visible = false;

        lblBoxName.Text = "Cancel Ticket";
        txt_C_OldAmt.Text = lbl_B_LC.Text;

        txt_C_Cancel_LC.Text = "";
        txt_C_Refund_LC.Text = "";
    }
    protected void btn_Close_Click(object sender, EventArgs e)
    {
        dv_ChangeCancel.Visible = false;
    }
    protected void btn_Change_Submit_Click(object sender, EventArgs e)
    {
        decimal LC_Amount = Common.CastAsDecimal(txt_H_ChangeAmt.Text);
        //------------------
        decimal Rate = 0;
        decimal H_USD = 0;
        decimal Total = 0;
        decimal Total_USD = 0;

        Total = Common.CastAsDecimal(lbl_B_LC.Text);
        Total += Common.CastAsDecimal(lbl_H_LC.Text);
        Total += LC_Amount;

        string sql = "select TOP 1 EXC_RATE from dbo.xchangedaily WHERE RATEDATE<='" + txt_H_NewDate.Text.Trim() + "' AND FOR_CURR='" + lblCurrency.Text + "' ORDER BY RATEDATE DESC";
        DataTable dt_1 = Common.Execute_Procedures_Select_ByQuery(sql);
        if (dt_1.Rows.Count > 0)
            Rate = Common.CastAsDecimal(dt_1.Rows[0][0].ToString());
        
        H_USD = LC_Amount / Rate;

        LC_Amount += Common.CastAsDecimal(lbl_H_LC.Text);
        H_USD += Common.CastAsDecimal(lbl_H_Amount.Text);

        Total_USD = Total / Rate;

        sql = "UPDATE CREWPORTCALLTRAVELDETAILS SET TicketStatus='C',DeptDate='" + txt_H_NewDate.Text + "',ChangeDate='" + DateTime.Today.ToString("dd-MMM-yyyy") + "',Change_LC=" + string.Format("{0:0.00}", LC_Amount) + ",Change_Rate=" + string.Format("{0:0.00}", Rate) + ",Change_USD=" + string.Format("{0:0.00}", H_USD) + ",Final_LC=" + string.Format("{0:0.00}", Total) + ",Final_USD=" + string.Format("{0:0.00}", Total_USD) + " WHERE CrewTravelId=" + CrewTravelId.ToString();
        Common.Execute_Procedures_Select_ByQueryCMS(sql);
        ShowDetails();
        dv_ChangeCancel.Visible = false;
    }
    protected void btn_Cancel_Submit_Click(object sender, EventArgs e)
    {
        decimal Cancel_LC = Common.CastAsDecimal(txt_C_Cancel_LC.Text);
        decimal Refund_LC = Common.CastAsDecimal(txt_C_Refund_LC.Text);
        //------------------
        decimal Rate = 0;
        decimal Cancel_USD=0,Refund_USD = 0;
        decimal Total = 0;
        decimal Total_USD = 0;

        Total = Common.CastAsDecimal(lbl_B_LC.Text);
        Total += Common.CastAsDecimal(lbl_H_LC.Text);
        Total += Cancel_LC;

        Total -= Refund_LC;

        string sql = "select TOP 1 EXC_RATE from dbo.xchangedaily WHERE RATEDATE<='" + DateTime.Today.ToString("dd-MMM-yyyy") + "' AND FOR_CURR='" + lblCurrency.Text + "' ORDER BY RATEDATE DESC";
        DataTable dt_1 = Common.Execute_Procedures_Select_ByQuery(sql);
        if (dt_1.Rows.Count > 0)
            Rate = Common.CastAsDecimal(dt_1.Rows[0][0].ToString());

        Cancel_USD = Cancel_LC / Rate;
        Refund_USD = Refund_LC / Rate;

        Total_USD = Total / Rate;

        //
        sql = "UPDATE CREWPORTCALLTRAVELDETAILS SET TicketStatus='D',CancellationDate='" + DateTime.Today.ToString("dd-MMM-yyyy") + "',Cancellation_LC=" + string.Format("{0:0.00}", Cancel_LC) + ",Cancellation_Rate=" + string.Format("{0:0.00}", Rate) + ",Cancellation_USD=" + string.Format("{0:0.00}", Cancel_USD) + ",Refund_Date='" + DateTime.Today.ToString("dd-MMM-yyyy") + "',Refund_LC=" + string.Format("{0:0.00}", Refund_LC) + ",Refund_USD=" + string.Format("{0:0.00}", Refund_USD) + ",Final_LC=" + string.Format("{0:0.00}", Total) + ",Final_USD=" + string.Format("{0:0.00}", Total_USD) + " WHERE CrewTravelId=" + CrewTravelId.ToString();
        Common.Execute_Procedures_Select_ByQueryCMS(sql);
        ShowDetails();
        dv_ChangeCancel.Visible = false;
    }
}

