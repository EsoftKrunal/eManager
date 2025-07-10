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
using System.Text;
using ShipSoft.CrewManager.BusinessObjects;
using ShipSoft.CrewManager.BusinessLogicLayer;
using ShipSoft.CrewManager.Operational;

public partial class CrewAccounting_PortAgent : System.Web.UI.Page
{
    int portcallid;
    HiddenField hfd_Port;
    Authority Auth;
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        try
        {
            Session["PageName"] = " - Port Agent [ " + Session["Planned_PortOnCallRef"].ToString() + " ]";
        }
        catch 
        {
            lbl_gdsignoff_Message.Text = "Select Atleast One Travel RFQ.";
            return;
            return; 
        }
        string str1, str2; 
        //***********Code to check page acessing Permission
            int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 18);
            if (chpageauth <= 0)
            {
                Response.Redirect("Dummy2.aspx");

            }
       //*******************
            ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 2);
            OBJ.Invoke();
            Session["Authority"] = OBJ.Authority;
            Auth = OBJ.Authority;

        try
        {
            str1 = Session["Planned_PortOnCallId"].ToString();
            str2 = Session["Planned_CrewId_List"].ToString();
            portcallid = Convert.ToInt32(Session["Planned_PortOnCallId"].ToString());
            lbl_gdsignoff_Message.Text = "";
            if (!Page.IsPostBack)
            {
                bindGridSignoff(GD_Signoff.Attributes["MySort"]);
                bindGridportcall(gdportcall.Attributes["MySort"]);
                bindportagent(portcallid);
                btn_Save.Visible = Auth.isAdd;
                btn_MakePO_Port.Visible = Auth.isAdd;
                Handle_Authority();
            }
        }
        catch
        {
            Response.Redirect("Dummy.aspx?mess=Please Select Atleast One Crew Member");
        }
    }
    private void Handle_Authority()
    {
        gdportcall.Columns[1].Visible = Auth.isDelete;
        gdportcall.Columns[6].Visible = Auth.isAdd;
    }
    private void bindGridSignoff(String Sort)
    {
         int PortCallId;
        string CrewList;
        PortCallId = Convert.ToInt32(Session["Planned_PortOnCallId"].ToString());
        CrewList = Session["Planned_CrewId_List"].ToString();
     
         DataTable ds = PortAgent1.selectPortAgentBookDetail(PortCallId,CrewList);
         ds.DefaultView.Sort = Sort;
        this.GD_Signoff.DataSource= ds;
        this.GD_Signoff.DataBind ();
        Session["Gridbindingvariable"] =Convert.ToString(1);
        GD_Signoff.Attributes.Add("MySort", Sort);
    }
    protected void on_Sorting(object sender, GridViewSortEventArgs e)
    {
        if (Convert.ToInt32(Session["Gridbindingvariable"]) == 1)
        {
            bindGridSignoff(e.SortExpression);
        }
        else
        {
            bindGridSignoffOnselectindexofSecondGrid(e.SortExpression, Session["str"].ToString());
        }
    }
    protected void on_Sorted(object sender, EventArgs e)
    {

    }
    private void bindGridportcall(String Sort)
    {
        DataTable ds = PortAgent1.selectPortCallRefNo(portcallid);
        ds.DefaultView.Sort = Sort;
        this.gdportcall.DataSource = ds;
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
    private void bindportagent(int PortCallId)
    {
        DataTable ds = PortAgent1.selectPortAgent(PortCallId);
        dp_port.DataTextField = "Company";
        dp_port.DataValueField = "PortAgentId";
        this.dp_port.DataSource = ds;
        this.dp_port.DataBind();
        dp_port.Items.Insert(0, new ListItem("<Select>", "0"));
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        int PortCallId,createdby;
        PortCallId = Convert.ToInt32(Session["Planned_PortOnCallId"].ToString());
       
        createdby = Convert.ToInt32(Session["loginid"].ToString());
        string CrewID="";
        string Contractid="";
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
            lbl_gdsignoff_Message.Visible = true;
            lbl_gdsignoff_Message.Text = "Select Atleast One Crew ";
        }
        else
        {
            PortAgent1.insertPortAgentBookingDetails("InsertPortAgentBookingHeaderDetails", "", PortCallId, Convert.ToInt16(this.dp_port.SelectedValue), this.txt_VSLETA.Text, this.Txt_VSLETD.Text,"O", createdby, CrewID, Contractid);
            lbl_gdsignoff_Message.Visible = true;
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
                chk.Enabled = false;
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
    private void bindGridSignoffOnselectindexofSecondGrid(String Sort,string str1)
    {
        DataTable ds = PortAgent1.selectPortAgentBookDetail_Show(str1);
        ds.DefaultView.Sort = Sort;
        this.GD_Signoff.DataSource = ds;
        this.GD_Signoff.DataBind();
        Session["Gridbindingvariable"] = Convert.ToString(2);
        GD_Signoff.Attributes.Add("MySort", Sort);
    }
    protected void gdportcall_SelectedIndexChanged(object sender, EventArgs e)
    {
        lbl_gdsignoff_Message.Visible = false;
        Session["str"] = this.gdportcall.DataKeys[gdportcall.SelectedIndex].Value.ToString();
        bindGridSignoffOnselectindexofSecondGrid(GD_Signoff.Attributes["MySort"],Session["str"].ToString());
       

        DataTable ds1= PortAgent1.selectPortAgentBookHeaderDetail(Convert.ToInt32(this.gdportcall.DataKeys[gdportcall.SelectedIndex].Value.ToString()));
        foreach (DataRow dr in ds1.Rows)
        {
            this.dp_port.Items.Clear();
            this.dp_port.Items.Add(new ListItem(dr["company"].ToString(),dr["portagentid"].ToString()));
            this.dp_port.SelectedValue = dr["PortAgentId"].ToString();

            if (dr["ETA"].ToString().Trim()=="") 
                this.txt_VSLETA.Text = "";
            else
                this.txt_VSLETA.Text = Convert.ToDateTime(dr["ETA"].ToString()).ToString("dd-MMM-yyyy");


            if (dr["ETD"].ToString().Trim() == "")
                this.Txt_VSLETD.Text = "";
            else
                this.Txt_VSLETD.Text = Convert.ToDateTime(dr["ETD"].ToString()).ToString("dd-MMM-yyyy");
        }
        btn_Save.Enabled = false;  

    }
    protected void dp_port_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable ds = PortAgent1.selectPortAgentDetails(Convert.ToInt32(this.dp_port.SelectedValue));
        foreach (DataRow dr in ds.Rows)
        {
            this.txt_prevCost.Text = dr["previouscost"].ToString();
            this.txtTotCrew.Text = dr["totalcrew"].ToString();
        }
    }
    protected void gdportcall_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        lbl_gdsignoff_Message.Visible = false;
         int returnvalue;
         returnvalue = -1;
         PortAgent1.deletePortAgentBookDetails("deletePortAgentBookingHeader", Convert.ToInt32(gdportcall.DataKeys[e.RowIndex].Value.ToString()), out returnvalue);
        if (returnvalue == 0)
        {
            lbl_gdsignoff_Message.Visible = true;
            lbl_gdsignoff_Message.Text = "Record Successfully Deleted.";
        }
        else
        {
            lbl_gdsignoff_Message.Visible = true;
            lbl_gdsignoff_Message.Text = "The RFQ is closed so cannot  be Deleted ";
        }
        bindGridSignoff(GD_Signoff.Attributes["MySort"]);
        bindGridportcall(gdportcall.Attributes["MySort"]);


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
            Response.Redirect("MailSend.aspx?Id=" + hfd.Value +"&mode=1");
        }

 
    }
    protected void gdportcall_PreRender(object sender, EventArgs e)
    {
        if (this.gdportcall.Rows.Count <= 0)
        {
            label1.Visible = true;
            label1.Text = "No Records Found..!";
        }
        else
        {
            label1.Text = "";
        }
    }
    protected void GD_Signoff_PreRender(object sender, EventArgs e)
    {
        if (this.GD_Signoff.Rows.Count <= 0)
        {
            Label2.Visible = true;
            Label2.Text = "No Records Found..!";
        }
        else
        {
            Label2.Text = "";
        }
    }
    protected void gdportcall_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        
    }
    protected void btn_MakePO_Port_Click(object sender, EventArgs e)
    {
        try
        {
            string vendor = "";
            string vendorid = "";
            string newvendorid = "";
            string ven_id = "";
            Boolean vendor_count = false;
            for (int i = 0; i < this.gdportcall.Rows.Count; i++)
            {
                if (((CheckBox)gdportcall.Rows[i].FindControl("chk_port")).Checked == true)
                {
                    if (gdportcall.Rows[i].Cells[5].Text.Trim() == "Closed")
                    {
                        lbl_gdsignoff_Message.Text = "Can't Make PO as RFQ is Already Closed.";
                        lbl_gdsignoff_Message.Visible = true;
                        return;
                    }
                    //--
                    newvendorid = ((Label)this.gdportcall.Rows[i].FindControl("lbl_vendorid")).Text;
                    hfd_Port = ((HiddenField)gdportcall.Rows[i].FindControl("hfd_PortAgentName"));
                    if (vendorid != newvendorid && vendorid != "")
                    {
                        lbl_gdsignoff_Message.Visible = true;
                        lbl_gdsignoff_Message.Text = "Selected Port Agent RFQs do not have same Vendors.";
                        vendor_count = true;
                        break;
                    }
                    else
                    {
                        vendorid = newvendorid;
                        ven_id = ((Label)this.gdportcall.Rows[i].FindControl("lbl_vendorid")).Text;
                    }
                }

                CheckBox chkvendorrfq = ((CheckBox)this.gdportcall.Rows[i].FindControl("chk_port"));
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
                    lbl_gdsignoff_Message.Visible = true;
                    lbl_gdsignoff_Message.Text = "Select Atleast One Port Agent RFQ.";
                }
                else
                {
                    Session["TravelPortSession"] = "2";
                    Session["VendorId_RFQ"] = newvendorid;
                    Session["VendorId_For_TravelRFQ"] = vendor;
                    Session["PortAgentName_RFQ"] = hfd_Port.Value;
                    Response.Redirect("POList.aspx?Port_Ref_Num=" + portcallid);
                }
            }
        }
        catch
        {

        }

        //try
        //{
        //    Response.Redirect("POList.aspx?Port_Ref_Num=" + portcallid);
        //}
        //catch
        //{

        //}
    }
}
