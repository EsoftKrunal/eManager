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

public partial class CrewOperation_CrewTravel : System.Web.UI.Page
{
    Authority Auth;
    //----------------------------------------------------
    #region PageControlLoader
    private void Load_Vessel()
    {
        DataSet ds = SearchSignOff.getVessel(Convert.ToInt32(Session["loginid"].ToString()));
        ddl_VesselName.DataSource = ds.Tables[0];
        ddl_VesselName.DataTextField = "VesselName1";
        ddl_VesselName.DataValueField = "VesselId";
        ddl_VesselName.DataBind();
        ddl_VesselName.Items.Insert(0, new ListItem("< All >", "0"));
    }
    private void bindcountrynameddl()
    {
        DataTable dt3 = PortPlanner.selectCountryName();
        this.ddlCountry.DataValueField = "CountryId";
        this.ddlCountry.DataTextField = "CountryName";
        this.ddlCountry.DataSource = dt3;
        this.ddlCountry.DataBind();
    }
    private void Load_Port()
    {
        DataTable dt4 = PortPlanner.selectPortName(Convert.ToInt32(ddlCountry.SelectedValue));
        this.ddl_port.DataValueField = "PortId";
        this.ddl_port.DataTextField = "PortName";
        this.ddl_port.DataSource = dt4;
        this.ddl_port.DataBind();
    }
    #endregion
    //----------------------------------------------------
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 3);
        if (chpageauth <= 0)
        {
            Response.Redirect("Dummy2.aspx");
        }
        //*******************
        Session["PageName"] = " - Crew Change";
        ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 3);
        OBJ.Invoke();
        Session["Authority"] = OBJ.Authority;
        
        Auth = OBJ.Authority;
        if (!Page.IsPostBack)
        {
            bindcountrynameddl();
            ddlCountry_SelectedIndexChanged(sender, e);
            Load_Vessel();
            //Handle_Authority();
            //--------
            try
            {
                if (Session["CCVessel"] != null)
                {
                    ddl_VesselName.SelectedIndex = int.Parse(Session["CCVessel"].ToString());
                }
                if (Session["CCCountry"] != null)
                    {
                    ddlCountry.SelectedIndex = int.Parse(Session["CCCountry"].ToString());
                    ddlCountry_SelectedIndexChanged(new object(), new EventArgs());
                }
                if (Session["CCPort"] != null)
                { ddl_port.SelectedIndex = int.Parse(Session["CCPort"].ToString()); }

                if (Session["EmpNo"] != null)
                { txt_EmpNo.Text = Session["EmpNo"].ToString(); }
                    
                // CLOSE OPEN PORT CALLS IN WHICH NO MEMBER S PENDING FOR ACTION
                Budget.getTable("Update PortCallHeader Set Status='C' where (select count(*) from Portcalldetail where PortCallId=PortCallHeader.PortCallId and isnull(Status,'N')='N')<=0 and STATUS='O'");
            }
            catch { }
            Bind_grid_Port_reference_no("");
        }
    }
    //----------------------------------------------------
    protected void ddl_VesselName_SelectedIndexChanged(object sender, EventArgs e)
    {
        Bind_grid_Port_reference_no(GvRefno.Attributes["MySort"]);
        GvRefno.SelectedIndex = -1;
        // CLOSE OPEN PORT CALLS IN WHICH NO MEMBER S PENDING FOR ACTION
        Budget.getTable("Update PortCallHeader Set Status='C' where (select count(*) from Portcalldetail where PortCallId=PortCallHeader.PortCallId and isnull(Status,'N')='N')<=0 and STATUS='O'");
    }
    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_Port();
    }
    protected void ddl_port_SelectedIndexChanged(object sender, EventArgs e)
    {
        Bind_grid_Port_reference_no(GvRefno.Attributes["MySort"]);
        GvRefno.SelectedIndex = -1;
    }
    protected void txt_EmpNo_TextChanged(object sender, EventArgs e)
    {
        Bind_grid_Port_reference_no(GvRefno.Attributes["MySort"]);
        GvRefno.SelectedIndex = -1;
    }

    protected void Order_Tickets(object sender, EventArgs e)
    {
        try
        {
            if (GvRefno.Rows.Count > 0)
            {
                if (GvRefno.SelectedIndex < 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "as", "alert('Please select Portcall from the list.');", true);
                    return;
                }
                HiddenField hdnStatus = ((HiddenField)GvRefno.SelectedRow.FindControl("hdnStatus"));
                if (hdnStatus.Value.ToUpper() == "CLOSED")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "as", "alert('Selected Port Call status is Closed. System is not allow to generate Order Ticket.');", true);
                    return;
                }
                HiddenField hdnPortCallId = ((HiddenField)GvRefno.SelectedRow.FindControl("HiddenPortCallId"));
                
                if (hdnPortCallId.Value != "")
                {
                    int PortCallId = Common.CastAsInt32(hdnPortCallId.Value);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "new", "window.open('OrderTickets.aspx?PortCallId=" + PortCallId.ToString() + "','');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "as", "alert('Select Portcall.');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "as", "alert('No Data found for PortCall.');", true);
            }
            
        }
        catch {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "as", "alert('Select Portcall.');", true);
        }
    }
    protected void btnTicketMgmt_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "new", "window.open('ManageTickets.aspx','');", true);
    }
    protected void btnInvMgmt_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "new", "window.open('ManageInvoices.aspx','');", true);
    }
    protected void Bind_grid_Port_reference_no(String Sort)
    {
        Session["CCVessel"] = ddl_VesselName.SelectedIndex;
        Session["CCCountry"] = ddlCountry.SelectedIndex;
        Session["CCPort"] = ddl_port.SelectedIndex;
        Session["EmpNo"] = txt_EmpNo.Text;

        DataTable dt = PortPlanner1.selectPortReferenceNumberDetails(Convert.ToInt32(ddl_port.SelectedValue), Convert.ToInt32(ddl_VesselName.SelectedValue), txt_EmpNo.Text.Trim(), ddl_PCStatus.SelectedValue, Convert.ToInt32(Session["loginid"].ToString()));
        dt.DefaultView.Sort = " VSLCODE ASC, ETA ASC";
        this.GvRefno.DataSource = dt;

        this.GvRefno.DataBind();
        
        lblCount.Text ="( " + dt.Rows.Count.ToString() + " ) Records Found.";
    }
    //----------------------------------------------------

    protected void lbSendTicketRequest_Click(object sender, EventArgs e)
    {
        try
        {
            if (GvRefno.Rows.Count > 0)
            {
                if (GvRefno.SelectedIndex < 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "as", "alert('Please select Portcall from the list.');", true);
                    return;
                }
                //HiddenField hdnStatus = ((HiddenField)GvRefno.SelectedRow.FindControl("hdnStatus"));
                //if (hdnStatus.Value.ToUpper() == "CLOSED")
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "as", "alert('Selected Port Call status is Closed. System is not allow to generate Order Ticket.');", true);
                //    return;
                //}
                HiddenField hdnPortCallId = ((HiddenField)GvRefno.SelectedRow.FindControl("HiddenPortCallId"));

                if (hdnPortCallId.Value != "")
                {
                    int PortCallId = Common.CastAsInt32(hdnPortCallId.Value);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "new", "window.open('CrewPortcallSendTravelRequest.aspx?PortCallId=" + PortCallId.ToString() + "','');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "as", "alert('Select Portcall.');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "as", "alert('No Data found for PortCall.');", true);
            }

        }
        catch
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "as", "alert('Select Portcall.');", true);
        }
    }
}
