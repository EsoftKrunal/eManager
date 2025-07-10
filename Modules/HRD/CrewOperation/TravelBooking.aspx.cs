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

public partial class CrewAccounting_TravelBooking : System.Web.UI.Page
{
    int portoncalliid;
    Authority Auth;
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        try
        {
            Session["PageName"] = " - Travel Booking [ " + Session["Planned_PortOnCallRef"].ToString() + " ]";
        }
        catch
        {
            lbl_gdsignoff_Message.Text = "Select Atleast One Travel RFQ.";
            return;
        }

        lbl_gdsignoff_Message.Text = "";
        ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 2);
        OBJ.Invoke();
        Session["Authority"] = OBJ.Authority;
        Auth = OBJ.Authority;
        string str1, str2;

        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 17);
        if (chpageauth <= 0)
        {
            Response.Redirect("Dummy2.aspx");

        }
        //*******************

        try
        {
            str1 = Session["Planned_PortOnCallId"].ToString();
            str2 = Session["Planned_CrewId_List"].ToString();
            portoncalliid = Convert.ToInt32(Session["Planned_PortOnCallId"].ToString());
            if (!Page.IsPostBack)
            {
                bindFromddl();
                bindToddl();
                bindTravelAgentddl();
                bindGridSignoff(GD_Signoff.Attributes["MySort"]);
                bindGridportcall(gdportcall.Attributes["MySort"]);
                btn_Save.Visible = Auth.isAdd;
                btn_MakePO.Visible = Auth.isAdd;
                Handle_Authority();
            }
        }
        catch
        {
            Response.Redirect("Dummy.aspx?mess=Please Select Atleast One Crew Member.");
        }
    }
    private void Handle_Authority()
    {
        gdportcall.Columns[1].Visible = Auth.isDelete;
        gdportcall.Columns[6].Visible = Auth.isAdd;
    }
    protected void bindFromddl()
    {
        DataTable dt3 = TravelBooking.selectAirportsName();
        this.dp_from.DataValueField = "NearestAirportId";
        this.dp_from.DataTextField = "Name";
        this.dp_from.DataSource = dt3;
        this.dp_from.DataBind();
    }
    protected void bindToddl()
    {
        DataTable dt3 = TravelBooking.selectAirportsName();
        this.dp_to.DataValueField = "NearestAirportId";
        this.dp_to.DataTextField = "Name";
        this.dp_to.DataSource = dt3;
        this.dp_to.DataBind();
    }
    protected void bindTravelAgentddl()
    {
        DataSet ds = cls_SearchReliever.getMasterData("TravelAgent", "TravelAgentId", "Company");
        dp_travelAgent.DataSource = ds.Tables[0];
        dp_travelAgent.DataValueField = "TravelAgentId";
        dp_travelAgent.DataTextField = "Company";
        dp_travelAgent.DataBind();
        dp_travelAgent.Items.Insert(0, new ListItem("<Select>", "0"));
    }
    private void bindGridSignoff(String Sort)
    {
        int PortCallId;
        string CrewListId;
      
        PortCallId = Convert.ToInt32(Session["Planned_PortOnCallId"].ToString());
        CrewListId = Session["Planned_CrewId_List"].ToString();
       
        DataTable dt1 = TravelBooking.selectSignOnOffTravelBookingDetails(CrewListId,PortCallId);
        dt1.DefaultView.Sort = Sort;
        this.GD_Signoff.DataSource = dt1;
        this.GD_Signoff.DataBind();
        ViewState["Gridbindingvariable1"] = Convert.ToString(1);
        GD_Signoff.Attributes.Add("MySort", Sort);
    }
    protected void on_Sorting(object sender, GridViewSortEventArgs e)
    {
        if (Convert.ToInt32(ViewState["Gridbindingvariable1"]) == 1)
        {
            bindGridSignoff(e.SortExpression);
        }
        else
        {
            bindGridSignoffOnselectindexofSecondGrid(e.SortExpression, ViewState["str1"].ToString());
        }
    }
    protected void on_Sorted(object sender, EventArgs e)
    {

    }
    private void bindGridportcall(String Sort)
    {
        DataTable dt2 = TravelBooking.selectPortCallDetails(portoncalliid);
        dt2.DefaultView.Sort = Sort;
        this.gdportcall.DataSource = dt2;
        this.gdportcall.DataBind();
        gdportcall.Attributes.Add("MySort", Sort);
    }
    protected void on_Sorting1(object sender, GridViewSortEventArgs e)
    {
        bindGridportcall(e.SortExpression);
    }
    protected void on_Sorted1(object sender, EventArgs e)
    {

    }
    protected void GD_Signoff_PreRender(object sender, EventArgs e)
    {
        if (this.GD_Signoff.Rows.Count <= 0)
        {
            lbl_gdsignoff.Text = "No Records Found..!";
        }
        else
        {
            lbl_gdsignoff.Text = "";
        }
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        int portcallid;
       
        portcallid = Convert.ToInt32(Session["Planned_PortOnCallId"].ToString());
        int fromairport=Convert.ToInt32(dp_from.SelectedValue);
        int toairport=Convert.ToInt32(dp_to.SelectedValue);
        int travelagentid=Convert.ToInt32(dp_travelAgent.SelectedValue);
       
        DateTime departuredate=Convert.ToDateTime(txt_depdate.Text);
        int classid=Convert.ToInt32(dp_class.SelectedValue);
        int createdby=Convert.ToInt32(Session["loginid"].ToString());
        string CrewID="";
        string Contractid = "";

       

        for(int i=0;i<this.GD_Signoff.Rows.Count;i++)
        {
            CheckBox chk=((CheckBox)GD_Signoff.Rows[i].FindControl("chk1"));
            if(chk.Checked==true)
            {
                if(CrewID=="")
                {
                    CrewID=this.GD_Signoff.DataKeys[i].Value.ToString();
                }
                else
                {
                    CrewID=CrewID + ","+this.GD_Signoff.DataKeys[i].Value.ToString();
                }
                
                if(Contractid=="")
                {
                    Contractid=((Label)this.GD_Signoff.Rows[i].FindControl("lblcontractid")).Text;
                }
                else
                {
                    Contractid=Contractid+","+((Label)this.GD_Signoff.Rows[i].FindControl("lblcontractid")).Text;
                }


            }
        }
        if (CrewID == "")
        {
            lbl_gdsignoff_Message.Text = "Please Select Atleast One Crew Member.";
        }
        else
        {
            TravelBooking.InsertTravelBookingHeaderDetails("InsertTravelBookingHeaderDetails",
                                                              "",
                                                              portcallid,
                                                              fromairport,
                                                              toairport,
                                                              travelagentid,
                                                              //previousfare,
                                                              departuredate,
                                                              classid,
                                                              'O',
                                                              createdby,
                                                              CrewID,
                                                              Contractid
                                                              );
            //lbl_gdsignoff_Message.Visible = true;
            lbl_gdsignoff_Message.Text = "Record Successfully Saved.";
        }
        bindGridSignoff(GD_Signoff.Attributes["MySort"]);
        bindGridportcall(gdportcall.Attributes["MySort"]);
        
    }
    protected void GD_Signoff_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (DataBinder.Eval(e.Row.DataItem, "Status").ToString().ToUpper() == "YES")
            {
                CheckBox chk = ((CheckBox)e.Row.FindControl("chk1"));
                //chk.Enabled = false;
            }
            //---------------------
            HiddenField hfd;
            string onoff;
            hfd = (HiddenField)e.Row.FindControl("hfd_OnOff");
            if (hfd != null)
            {
                onoff = hfd.Value;
                if (onoff.Trim() == "I")
                {
                    e.Row.BackColor = System.Drawing.Color.FromName("#C9F4DA");
                }
                else
                {
                    e.Row.BackColor = System.Drawing.Color.LightYellow;
                }
            }
        }
    }
    private void bindGridSignoffOnselectindexofSecondGrid(String Sort, string str11)
    {
        DataTable ds = TravelBooking.selectTravelBookingDetail_Show(str11);
        ds.DefaultView.Sort = Sort;
        this.GD_Signoff.DataSource = ds;
        this.GD_Signoff.DataBind();
        ViewState["Gridbindingvariable1"] = Convert.ToString(2);
        GD_Signoff.Attributes.Add("MySort", Sort);
    }
    protected void gdportcall_SelectedIndexChanged(object sender, EventArgs e)
    {
        string Mess= "";
        ViewState["str1"] = this.gdportcall.DataKeys[gdportcall.SelectedIndex].Value.ToString();
        bindGridSignoffOnselectindexofSecondGrid(GD_Signoff.Attributes["MySort"], ViewState["str1"].ToString());
       

        DataTable ds1 = TravelBooking.selectTravelHeaderDetail(Convert.ToInt32(this.gdportcall.DataKeys[gdportcall.SelectedIndex].Value.ToString()));
        foreach (DataRow dr in ds1.Rows)
        {
       
            Mess = Mess + Alerts.Set_DDL_Value(dp_from, dr["FromId"].ToString(), "From Airport");
            
       
            Mess = Mess + Alerts.Set_DDL_Value(dp_to, dr["ToId"].ToString(), "To Airport");
            
       
            Mess = Mess + Alerts.Set_DDL_Value(dp_travelAgent, dr["TravelAgentId"].ToString(), "Travel Agent");
            
            this.txt_depdate.Text = Convert.ToDateTime(dr["DepartureDate"].ToString()).ToString("dd-MMM-yyyy");
            this.dp_class.SelectedValue=dr["Class"].ToString();

        }
        btn_Save.Enabled = false;

        if (Mess.Length > 0)
        {
            this.lbl_gdsignoff_Message.Text = "The Status Of Following Has Been Changed To In-Active <br/>" + Mess.Substring(1);
        }
    }
    protected void dp_travelAgent_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable ds = TravelBooking.selectTravelAgentDetails(Convert.ToInt32(this.dp_travelAgent.SelectedValue),Convert.ToInt32(this.dp_from.SelectedValue),Convert.ToInt32(this.dp_to.SelectedValue));
        foreach (DataRow dr in ds.Rows)
        {
            this.txt_Fare.Text = dr["previouscost"].ToString();
        }
    }
    protected void dp_from_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable ds = TravelBooking.selectTravelAgentDetails(Convert.ToInt32(this.dp_travelAgent.SelectedValue), Convert.ToInt32(this.dp_from.SelectedValue), Convert.ToInt32(this.dp_to.SelectedValue));
        foreach (DataRow dr in ds.Rows)
        {
            this.txt_Fare.Text = dr["previouscost"].ToString();
        }
    }
    protected void dp_to_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable ds = TravelBooking.selectTravelAgentDetails(Convert.ToInt32(this.dp_travelAgent.SelectedValue), Convert.ToInt32(this.dp_from.SelectedValue), Convert.ToInt32(this.dp_to.SelectedValue));
        foreach (DataRow dr in ds.Rows)
        {
            this.txt_Fare.Text = dr["previouscost"].ToString();
        }
    }

    // DELETE THE RECORD (changing the status)
    protected void gdportcall_Row_Deleting(object sender, GridViewDeleteEventArgs e)
    {
       
        int returnvalue;
        TravelBooking.deleteTravelBookingDetails("deleteTravelBookingHeader", Convert.ToInt32(gdportcall.DataKeys[e.RowIndex].Value.ToString()), out returnvalue);
        if (returnvalue == 0)
        {
            lbl_gdsignoff_Message.Visible = true;
            lbl_gdsignoff_Message.Text = "Record Successfully Deleted.";
        }
        else
        {
            lbl_gdsignoff_Message.Visible = true;
            lbl_gdsignoff_Message.Text = "The RFQ is closed so cannot  be deleted ";
        }
        bindGridSignoff(GD_Signoff.Attributes["MySort"]);
        bindGridportcall(gdportcall.Attributes["MySort"]);
    }
    protected void gdportcall_PreRender(object sender, EventArgs e)
    {
        if (this.gdportcall.Rows.Count <= 0)
        {
            lbl_portcall.Text = "No Records Found..!";
        }
        else
        {
            lbl_portcall.Text = "";
        }
    }
    protected void gdportcall_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "SendMail")
        {
            ImageButton img;
            HiddenField hfd;
            img = ((ImageButton)e.CommandSource);
            GridViewRow r1 = (GridViewRow)img.Parent.Parent;
            hfd = (HiddenField)r1.FindControl("hfd_Id");
            Response.Redirect("MailSend.aspx?Id=" + hfd.Value + "&mode=2");
        }


    }
    protected void btn_MakePO_Click(object sender, EventArgs e)
    {
        try
        {
            string vendor = "";
            string vendorid = "";
            string newvendorid = "";
            string ven_id = "";
            string from_airport = "";
            string to_airport = "";
            Boolean vendor_count = false;
            for (int i = 0; i < this.gdportcall.Rows.Count; i++)
            {
               if (((CheckBox)gdportcall.Rows[i].FindControl("chk_travel")).Checked == true)
                {
                    if (gdportcall.Rows[i].Cells[5].Text.Trim() == "Closed")
                    {
                        lbl_gdsignoff_Message.Text = "Can't Make PO as RFQ is Already Closed.";
                        return;
                    }
                    //--s
                    newvendorid = ((Label)this.gdportcall.Rows[i].FindControl("lbl_vendorid")).Text;
                    if (vendorid != newvendorid && vendorid != "")
                    {
                        lbl_gdsignoff_Message.Text = "Selected Travel RFQs do not have same Vendors.";
                        vendor_count = true;
                        break;
                    }
                    else
                    {
                        vendorid = newvendorid;
                        ven_id = ((Label)this.gdportcall.Rows[i].FindControl("lbl_vendorid")).Text;
                        from_airport = ((Label)this.gdportcall.Rows[i].FindControl("lbl_FromAirport")).Text;
                        to_airport = ((Label)this.gdportcall.Rows[i].FindControl("lbl_ToAirport")).Text;
                    }
                }

                CheckBox chkvendorrfq = ((CheckBox)this.gdportcall.Rows[i].FindControl("chk_travel"));
                if (chkvendorrfq.Checked == true)
                {
                    if (vendor == "")
                    {
                        vendor = this.gdportcall.DataKeys[i].Value.ToString();
                    }
                    else
                    {
                        vendor = vendor + "," + this.gdportcall.DataKeys[i].Value.ToString();
                    }
                }
            }

            if (vendor_count == false)
            {
                if (vendor == "")
                {
                    lbl_gdsignoff_Message.Text = "Select Atleast One Travel RFQ.";
                }
                else
                {
                    Session["TravelPortSession"] = "1";
                    Session["VendorId_RFQ"] = newvendorid;
                    Session["VendorId_For_TravelRFQ"] = vendor;
                    Session["From_TravelRFQ"] = from_airport;
                    Session["To_TravelRFQ"] = to_airport;
                    Response.Redirect("POList.aspx?Port_Ref_Num=" + portoncalliid);
                }
            }
        }
        catch
        {

        }
    }
}
