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

public partial class CrewOperation_TicketCancellation : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        lbl_Message.Text = "";
        Session["PageName"] = " - Ticket Management"; 
        //***********Code to check page acessing Permission
        string Mess;
        Mess = "";
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 128);
        if (chpageauth <= 0)
        {
            Response.Redirect("Dummy2.aspx");
        }
        //*******************
        if (!Page.IsPostBack)
        {
            bindddl_VesselName();
            bindddlTravelAgent();
            pnl_Update.Visible = false;  
        }
    } 
    public void bindddl_VesselName()
    {
        DataSet dt1 = SearchSignOff.getVessel(Convert.ToInt32(Session["loginid"].ToString()));
        this.ddl_Vessel.DataSource = dt1;
        this.ddl_Vessel.DataValueField = "VesselId";
        this.ddl_Vessel.DataTextField = "Name";
        this.ddl_Vessel.DataSource = dt1;
        this.ddl_Vessel.DataBind();
        ddl_Vessel.Items.Insert(0, new ListItem(" < All > ", "0"));  
    }
    public void bindddlTravelAgent()
    {
        DataSet dt2 = Budget.getTable("SELECT TRAVELAGENTID,COMPANY FROM TRAVELAGENT");
        this.ddl_TravelAgents.DataValueField = "TRAVELAGENTID";
        this.ddl_TravelAgents.DataTextField = "COMPANY";
        this.ddl_TravelAgents.DataSource = dt2.Tables[0];
        this.ddl_TravelAgents.DataBind();
        ddl_TravelAgents.Items.Insert(0,new ListItem(" < All > ", "0"));  
    }
    public void BindGrid()
    {
        String Qry = "'";
        if (ddl_Vessel.SelectedIndex > 0)
        {
            Qry = Qry + " and ph.vesselid=" + ddl_Vessel.SelectedValue;
        }
        if (ddl_TravelAgents.SelectedIndex > 0)
        {
            Qry =Qry + " and ph.vendorid=" + ddl_TravelAgents.SelectedValue;
        }
        if (txt_from.Text.Trim() !="")
        {
            Qry = Qry + " and ph.podate >=''" + txt_from.Text.Trim() + "''";
        }
        if (txt_to.Text.Trim() !="")
        {
            Qry = Qry + " and ph.podate <=''" + txt_to.Text.Trim() + "''";
        }
        Qry = Qry + " Order By v.VesselName,Company,PONo'";
        Session["Qry"] = Qry;  
        DataSet ds = Budget.getTable("exec dbo.Filter_TicketCancellation " + Qry);
        grid_List.DataSource = ds.Tables[0];
        lblRowCount.Text = (ds.Tables[0].Rows.Count > 0) ? "" : "No records found.";
        grid_List.DataBind();

        DataView dv = ds.Tables[0].DefaultView;
        lblTotPoAmt.Text = ds.Tables[0].Compute("Sum(PoAmount)", "").ToString();
        lblTotRefAmount.Text = ds.Tables[0].Compute("Sum(CancelAmount)", "").ToString();
        lblTotRefAmountDone.Text = ds.Tables[0].Compute("Sum(CancelAmount)", " Status='Close'").ToString();

        if (lblTotPoAmt.Text.Trim() == "") lblTotPoAmt.Text = "0.0";
        if (lblTotRefAmount.Text.Trim() == "") lblTotRefAmount.Text = "0.0";
        if (lblTotRefAmountDone.Text.Trim() == "") lblTotRefAmountDone.Text = "0.0";
        
        lblPending.Text = Convert.ToString(decimal.Parse(lblTotRefAmount.Text) - decimal.Parse(lblTotRefAmountDone.Text));
        if (lblPending.Text.Trim() == "") lblPending.Text = "0.0";

    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        string str = "dbo.InsertUpdateTicketCancellation " + ViewState["PoId"] + "," + decimal.Parse(txt_CancelAmt.Text) + "," + Session["loginid"].ToString() + ",'" + ddl_Status.SelectedValue + "','" + txt_RefDate.Text + "','" + txt_Remarks.Text + "'";
        try
        {
            Budget.getTable(str); 
            lbl_Message.Text = "Update successfully.";
            BindGrid();
            pnl_Update.Visible = false; 
        }
        catch
        {
            lbl_Message.Text = "Unable to update.";
        }
        txt_CancelAmt.Text = "0.0";
        txt_Remarks.Text = "";
        txt_RefDate.Text = "";  
    }
    protected void btn_Cancel_Click(object sender, EventArgs e)
    {
       pnl_Update.Visible = false;
    }
    protected void Edit_Click(object sender, EventArgs e)
    {
        pnl_Update.Visible = true;
        
        txt_CancelAmt.Text = "";
        txt_CancelAmt.Text = "0.0";
        txt_Remarks.Text = "";
        DataTable dt = Budget.getTable("SELECT * FROM TICKETCANCELLATION WHERE POID=" + ((ImageButton)sender).CommandArgument).Tables[0];
        ViewState["PoId"]=((ImageButton)sender).CommandArgument;
        if (dt.Rows.Count >0)
        {
            txt_Remarks.Text = dt.Rows[0]["Remarks"].ToString();
            ddl_Status.SelectedValue=dt.Rows[0]["Status"].ToString();
            txt_CancelAmt.Text=dt.Rows[0]["CancellAmount"].ToString();
            txt_RefDate.Text = Convert.ToString(dt.Rows[0]["RefDate"]);    
        }
        for (int i = 0; i <= grid_List.Rows.Count - 1;i++ )
        {
            if (((HiddenField)grid_List.Rows[i].FindControl("hfd_PoId")).Value == ViewState["PoId"].ToString())
            {
                grid_List.SelectedIndex = i; 
            }

        }
    }
    protected void btn_Search_Click(object sender, EventArgs e)
    {
        BindGrid();
    }
    protected void lnlHLink_Click(object sender,EventArgs e)
    {
        LinkButton lnk=((LinkButton)sender);
        Session.Add("POId_Print", ((HiddenField)lnk.Parent.FindControl("hfd_PoId")).Value);
        Session.Add("VendorId_Print", ((HiddenField)lnk.Parent.FindControl("hfd_VendorId")).Value);
        //=========
        Session.Add("VesselId_Print", ((HiddenField)lnk.Parent.FindControl("hfd_VslId")).Value);
        Session.Add("PoDate_Print", ((HiddenField)lnk.Parent.FindControl("hfd_PoDate")).Value);
        //=========
        Page.ClientScript.RegisterStartupScript(this.GetType(), "popup", "window.open('../Reporting/PO Printing.aspx',null,'title=no,toolbars=no,scrollbars=yes,width=850,height=650,left=20,top=20,addressbar=no');", true);    
    }
    protected void btn_Print_Click(object sender, EventArgs e)
    {
        BindGrid();
        Page.ClientScript.RegisterStartupScript(this.GetType(), "popup", "window.open('../Reporting/FilterTravelReport.aspx',null,'title=no,toolbars=no,scrollbars=yes,width=850,height=650,left=20,top=20,addressbar=no');", true);    
    }
}
