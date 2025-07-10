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

public partial class CrewAccounting_WagePayment : System.Web.UI.Page
{
    Authority Auth;
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        Session["PageName"] = " - Wage Payment";
        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 33);
        if (chpageauth <= 0)
        {
            Response.Redirect("Dummy.aspx");

        }
        //*******************
        lbl_Message.Text = "";
        ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 3);
        OBJ.Invoke();
        Session["Authority"] = OBJ.Authority;
        Auth = OBJ.Authority;
        if (!Page.IsPostBack)
        {
            int Yr;
            bindVesselNameddl();
            Yr = DateTime.Today.Year;
            Yr = Yr - 1;
            ddl_Year.Items.Add(new ListItem(Yr.ToString(), Yr.ToString()));
            Yr = Yr + 1;
            ddl_Year.Items.Add(new ListItem(Yr.ToString(), Yr.ToString()));
            Yr = Yr + 1;
            ddl_Year.Items.Add(new ListItem(Yr.ToString(), Yr.ToString()));
            btnAddInvoice.Visible = Auth.isEdit;
            btnprint.Visible = Auth.isPrint;
        }

    }
    protected void bindVesselNameddl()
    {
        DataSet ds = SearchSignOff.getVesselCodeandName();
        ddl_Vessel.DataSource = ds.Tables[0];
        ddl_Vessel.DataValueField = "VesselId";
        ddl_Vessel.DataTextField = "Name";
        ddl_Vessel.DataBind();
        ddl_Vessel.Items.Insert(0, new ListItem("<Select>", "0"));
        //DataSet ds = cls_SearchReliever.getMasterData("Vessel", "VesselId", "VesselName", Convert.ToInt32(Session["loginid"].ToString()));
        //ddl_Vessel.DataSource = ds.Tables[0];
        //ddl_Vessel.DataValueField = "VesselId";
        //ddl_Vessel.DataTextField = "VesselName";
        //ddl_Vessel.DataBind();
        //ddl_Vessel.Items.Insert(0, new ListItem("< Select >", "0"));
    }
    protected void btnAddInvoice_Click(object sender, EventArgs e)
    {
        bool OneChecked;
        OneChecked = false;
        int checkmsg=0;
        HiddenField h1;
        CheckBox ch;
        string CrewList, ContractList;
        CrewList = "";
        ContractList = "";

        int VesselId, Month, Year,LoginId,CrewId,ContractId;
        VesselId = Convert.ToInt32(ddl_Vessel.SelectedValue);
        Month = Convert.ToInt32(ddl_Month.SelectedValue);
        Year = Convert.ToInt32(ddl_Year.SelectedValue);
        LoginId = Convert.ToInt32(Session["loginid"].ToString());

        for (int i = 0; i <= gv_Main.Rows.Count - 1; i++)
        {
            ch = (CheckBox)gv_Last.Rows[i].FindControl("chk_Select");
            if (ch.Enabled)
            {
                checkmsg = checkmsg + 1;
            }
            if (ch.Checked)
            {
                OneChecked = true;
            }
        }
        if (checkmsg <= 0)
        {
            lbl_Message.Text = "Wage Payment has been done for this month.";
            return;
        }
        else if (OneChecked == false)
        {
            lbl_Message.Text = "Please Select at Least One Row to Save.";
            return;
        }
        try
        {
            for (int i = 0; i <= gv_Main.Rows.Count - 1; i++)
            {
                ch = (CheckBox)gv_Last.Rows[i].FindControl("chk_Select");
                h1 = (HiddenField)gv_Main.Rows[i].FindControl("lbl_CrewId");
                CrewId = Convert.ToInt32(h1.Value);
                h1 = (HiddenField)gv_Main.Rows[i].FindControl("lbl_ContractId");
                ContractId = Convert.ToInt32(h1.Value);
                //----------------
                WagePayment.PaidPayrollDetails(VesselId, Month, Year,CrewId,ContractId, (ch.Checked)?0:1);
            }
            lbl_Message.Text = "Wage Payment Saved Successfully.";
        }
        catch 
        {
            lbl_Message.Text = "Wage Payment can't Saved.";
            return;
        }
        btn_search_Click(sender,e);
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        int VesselId, Month, Year;
        VesselId = Convert.ToInt32(ddl_Vessel.SelectedValue);
        Month = Convert.ToInt32(ddl_Month.SelectedValue);
        Year = Convert.ToInt32(ddl_Year.SelectedValue);
        try
        {
            gv_Main.DataSource = WagePayment.get_Payroll(VesselId, Month, Year);
        }
        catch { }
        gv_Main.DataBind();
        try
        {
            gv_Income.DataSource = WagePayment.Get_Payroll_Income(VesselId, Month, Year);
        }
        catch { }
        gv_Income.DataBind();
        try
        {
            gv_Expence.DataSource = WagePayment.Get_Payroll_Expence_Fixed(VesselId, Month, Year);
        }
        catch { }
        gv_Expence.DataBind();
        try
        {
            gv_Expence1.DataSource = WagePayment.Get_Payroll_Expence(VesselId, Month, Year);
        }
        catch { }
        gv_Expence1.DataBind();
        try
        {
            gv_Last.DataSource = WagePayment.Get_Payroll_Last(VesselId, Month, Year);
        }
        catch { }
        gv_Last.DataBind();
       Udpate_Grids();
    }
    private void Udpate_Grids()
    {
        DataTable dt;
        HiddenField h1;
        CheckBox ch;
        int CrewId, ContractId;

        int VesselId, Month, Year;
        VesselId = Convert.ToInt32(ddl_Vessel.SelectedValue);
        Month = Convert.ToInt32(ddl_Month.SelectedValue);
        Year = Convert.ToInt32(ddl_Year.SelectedValue);

        int max;
        Double TotalEarning, NetEarning, Leave, OpBalance, TotPayable, Netpayable, Deductions;
        max = gv_Main.Rows.Count;

        for (int i = 0; i < max; i++)
        {
            HiddenField hfd;
            hfd =(HiddenField)gv_Last.Rows[i].FindControl("hfd_TotalEarning");
            gv_Expence.Rows[i].Cells[4].Text = Convert.ToDouble(hfd.Value).ToString("F"); 

            ch = (CheckBox)gv_Last.Rows[i].FindControl("chk_Select");
            h1 = (HiddenField)gv_Main.Rows[i].FindControl("lbl_CrewId");
            CrewId = Convert.ToInt32(h1.Value);
            h1 = (HiddenField)gv_Main.Rows[i].FindControl("lbl_ContractId");
            ContractId = Convert.ToInt32(h1.Value);

            dt = Payroll.IS_PAYROL_GENERATED(VesselId, Month, Year, CrewId, ContractId);
            if (Convert.ToInt32(dt.Rows[0][0].ToString()) == 0)
            {
                ch.Enabled = true;
                ch.Checked = true;
            }
            else
            {
                ch.Enabled = false;
            }
        }
    }

    protected void btnprint_Click1(object sender, EventArgs e)
    {
        Response.Redirect("~/Reporting/WagePayableToCrew_new.aspx?vi=" + this.ddl_Vessel.SelectedValue + "&wagemonth=" + this.ddl_Month.SelectedValue + "&wageyear=" + this.ddl_Year.SelectedValue);
    }
    protected void gv_Main_PreRender(object sender, EventArgs e)
    {
        if (ddl_Vessel.SelectedIndex <= 0 && ddl_Month.SelectedIndex <= 0 && ddl_Year.SelectedIndex <= 0)
        {
            lbl_gv_Main.Text = "";
        }
        else if (gv_Main.Rows.Count <= 0)
        {
            lbl_gv_Main.Text = "No Records Found..!";
        }
        else
        {
            lbl_gv_Main.Text = "";
        }
    }
}
