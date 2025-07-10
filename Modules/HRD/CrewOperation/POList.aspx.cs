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

public partial class CrewOperation_POList : System.Web.UI.Page
{
    int PCRefNo;
    bool recall = true, recall1 = true;
    Authority Auth;
    string VendorIdForTravelRFQ, VendorIfforRFQ;
    string FromAirport_RFQ, ToAirport_RFQ, PortAgentName_RFQ;
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        //***********Code to check page acessing Permission
        Session["PageName"] = " - Purchase Order"; 
        Label1.Text = "";
        lbl_gvpo.Visible = false ;
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 24);
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
        {PCRefNo = Convert.ToInt32(Request.QueryString["Port_Ref_Num"].ToString());}
        catch{}
        if (!Page.IsPostBack)
        {
            this.txtpodt.Text = System.DateTime.Today.Date.ToString("dd-MMM-yyyy");
            bindVesselNameddl();
            bindcountrynameddl();
            ddlCountry_SelectedIndexChanged(sender, e);
            load_currency();
            Bindgrid1();
            Bindgrid2();
            bindcountrynameddl_forSearch();
            DropDownList1_SelectedIndexChanged(sender, e);
            bindVendorDDl_forSearch();
            this.btn_save.Enabled = true && Auth.isAdd;
            this.btn_Cancel.Visible = Auth.isDelete;
            DataTable dttp = POList.getPODetailsforPortRefNumber(PCRefNo);
            foreach (DataRow dr in dttp.Rows)
            {
                VendorIfforRFQ = Session["VendorId_RFQ"].ToString();
                VendorIdForTravelRFQ = Session["VendorId_For_TravelRFQ"].ToString();
                char[] sep = new char[] {','};
                string[] RFQNumber = VendorIdForTravelRFQ.Split(sep);
                int travelbookingidforrfq = Convert.ToInt32(RFQNumber[0].ToString());
                ddvessel.SelectedValue = dr["VesselId"].ToString();
                ddvessel_SelectedIndexChanged1(sender,e);
                ddlCountry.SelectedValue = dr["CountryId"].ToString();
                ddlCountry_SelectedIndexChanged(sender, e);
                ddport.SelectedValue = dr["PortId"].ToString();
                ddport_SelectedIndexChanged(sender, e);
                ddPortcall.SelectedValue = dr["PortCallId"].ToString();
                ddPortcall_SelectedIndexChanged(sender, e);
                ddvendor.SelectedValue = VendorIfforRFQ;
                ddvendor_SelectedIndexChanged(sender, e);
                
                for (int j = 0; j < RFQNumber.Length; j++)
                {
                    DropDownList dd_rfq = ((DropDownList)this.GvPO.Rows[j].FindControl("ddrfq"));
                    dd_rfq.SelectedValue = RFQNumber[j].ToString(); 
                }
            }
            Handle_Authority();
        }
    }
    private void Handle_Authority()
    {
        gvPOList.Columns[1].Visible = Auth.isDelete;
    }
    private void bindcountrynameddl()
    {
        DataTable dt3 = PortPlanner.selectCountryName();
        this.ddlCountry.DataValueField = "CountryId";
        this.ddlCountry.DataTextField = "CountryName";
        this.ddlCountry.DataSource = dt3;
        this.ddlCountry.DataBind();
    }
    private void Bindgrid1()
    {
        if (recall)
        {
            DataTable dt5 = POList.selectGVPODetails();
            this.GvPO.DataSource = dt5;
            this.GvPO.DataBind();
            recall = false; 
        }
    }
    private void Bindgrid2()
    {
        if (recall)
        {
            DataTable dt2 = POList.selectGVPOListDetails(Convert.ToInt32(Session["loginid"].ToString()));
            this.gvPOList.DataSource = dt2;
            this.gvPOList.DataBind();
            recall1 = false;  
        }
    }
    private void load_currency()
    {
        DataTable dt1 = POList.SelectCurrency();
        this.ddlcurrency.DataValueField = "CurrencyId";
        this.ddlcurrency.DataTextField = "CurrencyName";
        this.ddlcurrency.DataSource = dt1;
        this.ddlcurrency.DataBind();
        ddlcurrency.Items.Insert(0, new ListItem("<Select>", "0"));
    }
    private void BindVendorDropDown()
    {
        DataTable dt = POList.selectDataPOVendor(Convert.ToInt32(this.ddPortcall.SelectedValue.ToString()));
        this.ddvendor.DataValueField = "VendorId";
        this.ddvendor.DataTextField = "VendorName";
        this.ddvendor.DataSource = dt;
        this.ddvendor.DataBind();
        ddvendor.Items.Insert(0,new ListItem("<Select>", "0"));

    }
    private void BindPortCallRefNoDropDown(int vesselid, int portid)
    {   try
        {
            DataTable dt1 = POList.selectPortCallRefNo(vesselid, portid);
            this.ddPortcall.DataValueField = "PortCallId";
            this.ddPortcall.DataTextField = "PortCallHeader";
            this.ddPortcall.DataSource = dt1;
            this.ddPortcall.DataBind(); 
        } catch{}
    }
    protected void bindVesselNameddl()
    {
        DataSet ds = SearchSignOff.getVessel(Convert.ToInt32(Session["loginid"].ToString()));
        ddvessel.DataSource = ds.Tables[0];
        ddvessel.DataValueField = "VesselId";
        ddvessel.DataTextField = "Name";
        ddvessel.DataBind();
        ddvessel.Items.Insert(0, new ListItem("<Select>", "0"));
        ///--
        ddl_VesselName.DataSource = ds.Tables[0];
        ddl_VesselName.DataValueField = "VesselId";
        ddl_VesselName.DataTextField = "Name";
        ddl_VesselName.DataBind();
        ddl_VesselName.Items.Insert(0, new ListItem("<Select>", "0"));
    }
    private void bindcountrynameddl_forSearch()
    {
        DataTable dt3 = PortPlanner.selectCountryName();
        this.DropDownList1.DataValueField = "CountryId";
        this.DropDownList1.DataTextField = "CountryName";
        this.DropDownList1.DataSource = dt3;
        this.DropDownList1.DataBind();
    }
    private void Load_Port_forSearch()
    {
        DataTable dt4 = PortPlanner.selectPortName(Convert.ToInt32(DropDownList1.SelectedValue));
        this.DropDownList2.DataValueField = "PortId";
        this.DropDownList2.DataTextField = "PortName";
        this.DropDownList2.DataSource = dt4;
        this.DropDownList2.DataBind();
    }
    private void Load_Port()
    {
        DataTable dt4 = PortPlanner.selectPortName(Convert.ToInt32(ddlCountry.SelectedValue));
        this.ddport.DataValueField = "PortId";
        this.ddport.DataTextField = "PortName";
        this.ddport.DataSource = dt4;
        this.ddport.DataBind();
        
    }
    private void bindVendorDDl_forSearch()
    {
        DataTable dt48 = POList.selectVendorName_forSearch();
        this.ddl_VendorName.DataValueField = "VendorId";
        this.ddl_VendorName.DataTextField = "VendorName";
        this.ddl_VendorName.DataSource = dt48;
        this.ddl_VendorName.DataBind();
    }
    public  DataTable accountcode()
    {
        DataTable dt3;
        DateTime dt=Convert.ToDateTime(txtpodt.Text);
        int todayyear;
        todayyear = dt.Year;
 
        dt3 = POList.account_head(Convert.ToInt32(ddvessel.SelectedValue),todayyear);
        return dt3;
    }
    public DataTable rfqnumber()
     {
        DataTable dt4;
        Int32 vendorid, portcallid;
        if (ddvendor.SelectedIndex < 0)
        {
            vendorid = 0;
        }
        else
        {
            vendorid = Convert.ToInt32(ddvendor.SelectedValue);
        }

        if (ddPortcall.SelectedIndex < 0)
        {
            portcallid = 0;
        }
        else
        {
            portcallid = Convert.ToInt32(ddPortcall.SelectedValue);
        }

        dt4 = POList.rfq_number(vendorid, portcallid);
        
        return dt4;
    }

    protected void btn_Cancel_Click(object sender, EventArgs e)
    {
        try
        {
            txteta.Text = "";
            txtetd.Text = "";
            txtexchangerate.Text = "";
            txtpodt.Text = System.DateTime.Today.Date.ToString("MM/dd/yyyy");
            ddvessel.SelectedIndex = 0;
            ddlCountry.SelectedIndex = 0;
            ddlcurrency.SelectedIndex = 0;
            Load_Port();
            BindPortCallRefNoDropDown(Convert.ToInt32(ddvessel.SelectedValue), Convert.ToInt32(ddport.SelectedValue));
            BindVendorDropDown();
            Bindgrid1();
            showamoutdetails();
            //this.Label1.Text = "";
            this.btn_save.Enabled = true && Auth.isAdd;
            gvPOList.SelectedIndex = -1; 
            
        }
        catch
        { 

        }
    }
    protected void btn_Print_Click(object sender, EventArgs e)
    {

    }
    protected void ddvessel_SelectedIndexChanged1(object sender, EventArgs e)
    {
        BindPortCallRefNoDropDown(Convert.ToInt32(ddvessel.SelectedValue), Convert.ToInt32(ddport.SelectedValue));
        Bindgrid1();
        showamoutdetails();
    }
    protected void ddport_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindPortCallRefNoDropDown(Convert.ToInt32(ddvessel.SelectedValue), Convert.ToInt32(ddport.SelectedValue));
    }
    protected void ddPortcall_SelectedIndexChanged(object sender, EventArgs e)
    {
        //*** Code to get ETA & ETD
        DataTable dt=POList.SelectPortCallHeaderETA(Convert.ToInt32(ddPortcall.SelectedValue.ToString()));
        foreach(DataRow dr in dt.Rows) 
        {
            try
            {
                this.txteta.Text = DateTime.Parse(dr["ETA"].ToString()).ToString("dd-MMM-yyyy");
            }
            catch { this.txteta.Text = ""; }
            try
            {
                this.txtetd.Text = DateTime.Parse(dr["ETD"].ToString()).ToString("dd-MMM-yyyy");
            }
            catch { this.txtetd.Text = ""; }
            
        }
        //***
        BindVendorDropDown();
        Bindgrid1();
    }
    protected void ddvendor_SelectedIndexChanged(object sender, EventArgs e)
    {
        recall = true;
        Bindgrid1();
    }

    protected void btn_save_Click(object sender, EventArgs e)
    {
        if (DateTime.Parse(txtpodt.Text) > DateTime.Today)
        {
            Label1.Text = "PO date must be less or equal todfay date.";
            return; 
        }
        Boolean duplicate=false ;
        Boolean accselect = false;
        Boolean valreq = true;
        for (int i = 0; i < GvPO.Rows.Count; i++)
        {
            DropDownList ddaccode = ((DropDownList)this.GvPO.Rows[i].FindControl("ddAC"));
            DropDownList dddrfq=((DropDownList)this.GvPO.Rows[i].FindControl("ddrfq"));
            TextBox txtamt = ((TextBox)this.GvPO.Rows[i].FindControl("txtamtlocalcurrency"));
            if (ddaccode.SelectedIndex > 0 && i < GvPO.Rows.Count-1)
            {
                if (dddrfq.Text == "")
                {
                    valreq = false;
                }
                //if (dddrfq.Text == "" || Convert.ToDouble(txtamt.Text) <= 0)
                //{
                //    valreq = false;
                //}
                accselect = true;
                for (int j = i + 1; j < GvPO.Rows.Count; j++)
                {
                    DropDownList ddcode = ((DropDownList)this.GvPO.Rows[j].FindControl("ddAC"));
                    if (ddaccode.SelectedValue == ddcode.SelectedValue)
                    {
                        duplicate = true;
                        break;
                    }
                }
            }
        }
        if (accselect == false)
        {
            Label1.Text = "Please Select Atleast One Account Code";
        }
        else if (duplicate == true)
        {
            Label1.Text = "Please Select Unique Account Code";
        }
        else if (valreq == false)
        {
            Label1.Text = "Please Fill All The Required Information Of Account Head";
        }
        else
        {
            try
            {
                int returnvalue;
                returnvalue = -1;
                POList.insertPOHeader("InsertPOHeader", "", this.txtpodt.Text, Convert.ToInt32(this.ddvessel.SelectedValue), Convert.ToInt32(ddport.SelectedValue), Convert.ToInt16(this.ddPortcall.SelectedValue), this.txteta.Text, this.txtetd.Text, Convert.ToInt32(this.ddvendor.SelectedValue), Convert.ToDouble(this.txtexchangerate.Text), "O", Convert.ToInt32(Session["loginid"]), out returnvalue, Convert.ToInt32(this.ddlcurrency.SelectedValue));
                for (int i = 0; i < this.GvPO.Rows.Count; i++)
                {
                    DropDownList ddaccode = ((DropDownList)this.GvPO.Rows[i].FindControl("ddAC"));
                    TextBox txtdesc = ((TextBox)this.GvPO.Rows[i].FindControl("txtdesofservices"));
                    DropDownList ddrfq = ((DropDownList)this.GvPO.Rows[i].FindControl("ddrfq"));
                    TextBox txtamount = ((TextBox)this.GvPO.Rows[i].FindControl("txtamtlocalcurrency"));
                    TextBox txtamtused = ((TextBox)this.GvPO.Rows[i].FindControl("txtamtusd"));
                    //************
                    if (ddaccode.SelectedIndex != 0 && Convert.ToDouble(txtamount.Text) >= 0.0 && ddrfq.SelectedIndex > -1)
                    {
                        string strtablename;
                        if (Convert.ToInt32(this.ddvendor.SelectedValue) > 100000)
                        {
                            strtablename = "travel";
                        }
                        else
                        {
                            strtablename = "port";
                        }
                        POList.insertPODetails("InsertPoDetails", returnvalue, Convert.ToInt32(ddaccode.SelectedValue), txtdesc.Text, Convert.ToInt32(ddrfq.SelectedValue), Convert.ToDouble(txtamount.Text), (Convert.ToDouble(txtamount.Text) / Convert.ToDouble(txtexchangerate.Text)), strtablename);
                        HiddenField1.Value = ddrfq.SelectedItem.Text;
                    }

                }
                Bindgrid2();
                txteta.Text = "";
                txtetd.Text = "";
                txtexchangerate.Text = "";
                txtpodt.Text = System.DateTime.Today.Date.ToString("MM/dd/yyyy");
                ddvessel.SelectedIndex = 0;
                ddport.SelectedIndex = 0;

                ddPortcall.SelectedIndex = 0;

                ddvendor.SelectedIndex = 0;
                //this.Label1.Text = "";
                this.btn_save.Enabled = true && Auth.isAdd;
                Bindgrid1();
                Label1.Text = "Record Saved Successfully";
                
                showamoutdetails();
                
            }
            catch
            {

            }
        }
    }

    protected void GvPO_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox txtamount = ((TextBox)e.Row.FindControl("txtamtlocalcurrency"));
            TextBox txtamtused = ((TextBox)e.Row.FindControl("txtamtusd"));
            txtamount.Attributes.Add("onchange", "Calculate('" + txtamount.ClientID + "','" + txtamtused.ClientID + "','" + txtexchangerate.ClientID + "')");
            txtamount.Attributes.Add("onblur", "Calculate('" + txtamount.ClientID + "','" + txtamtused.ClientID + "','" + txtexchangerate.ClientID + "')");
            Label lblsrno = ((Label)e.Row.FindControl("lblsrno"));
            lblsrno.Text = Convert.ToString(e.Row.RowIndex + 1);
            
        }


    }
    protected void gvPOList_SelectedIndexChanged(object sender, EventArgs e)
    {
        string Mess;
        Mess = "";

      DataTable dt = POList.SelectPOHeaderDetails(Convert.ToInt32(gvPOList.DataKeys[gvPOList.SelectedIndex].Value.ToString()));
        foreach (DataRow dr in dt.Rows)
        {
            
            Mess = Mess + Alerts.Set_DDL_Value(ddvessel, dr["VesselId"].ToString(), "Vessel");
            //=========
            HiddenFieldVesselId.Value = dr["VesselId"].ToString();
            //=========
            //****** To Get Country According To Port
            DataTable dtcountry = PortPlanner.selectCountry(Convert.ToInt32(dr["PortId"].ToString()));
            foreach (DataRow drr in dtcountry.Rows)
            {
            
                Mess = Mess + Alerts.Set_DDL_Value(ddlCountry, drr["CountryId"].ToString(), "Country");
                ddlCountry_SelectedIndexChanged(sender, e);
            }
            //***********
           
            Mess = Mess + Alerts.Set_DDL_Value(ddport, dr["PortId"].ToString(), "Port");
            BindPortCallRefNoDropDown(Convert.ToInt32(ddvessel.SelectedValue), Convert.ToInt32(ddport.SelectedValue));
           
            Mess = Mess + Alerts.Set_DDL_Value(ddPortcall, dr["PortCalliD"].ToString(), "Port Call");
            BindVendorDropDown();
           
            Mess = Mess + Alerts.Set_DDL_Value(ddvendor, dr["VendorId"].ToString(), "Vendor");
            try
            {
                this.txteta.Text = DateTime.Parse(dr["ETA"].ToString()).ToString("dd-MMM-yyyy");
            }
            catch { this.txteta.Text ="";}
            try
            {
                this.txtetd.Text = DateTime.Parse(dr["ETD"].ToString()).ToString("dd-MMM-yyyy");
            }
            catch { this.txtetd.Text = ""; }

            this.txtexchangerate.Text = Convert.ToDouble(dr["ExchangeRate"].ToString()).ToString("0.00");
            try
            {
                this.txtpodt.Text = DateTime.Parse(dr["PODate"].ToString()).ToString("dd-MMM-yyyy"); ;
            }
            catch { this.txtpodt.Text = ""; }
            //=========
            HiddenFieldPodate.Value = dr["PODate"].ToString();
            //=========
            this.ddlcurrency.SelectedValue = dr["Currency"].ToString();
            HiddenVendorId.Value = dr["VendorId"].ToString();
            HiddenField1.Value = dr["PortCalliD"].ToString();

        }

        DataTable dt1 = POList.selectPODetailsByPoId(Convert.ToInt32(gvPOList.DataKeys[gvPOList.SelectedIndex].Value.ToString()));
        this.GvPO.DataSource = dt1;
        this.GvPO.DataBind();

        for (int i = 0; i < GvPO.Rows.Count ; i++)
        {
            DropDownList ddaccode = ((DropDownList)this.GvPO.Rows[i].FindControl("ddAC"));
            TextBox txtdesc = ((TextBox)this.GvPO.Rows[i].FindControl("txtdesofservices"));
            DropDownList ddrfq = ((DropDownList)this.GvPO.Rows[i].FindControl("ddrfq"));
            TextBox txtamount = ((TextBox)this.GvPO.Rows[i].FindControl("txtamtlocalcurrency"));
            TextBox txtamtused = ((TextBox)this.GvPO.Rows[i].FindControl("txtamtusd"));
            
            DataRow dr=dt1.Rows[i] ;
            ddaccode.SelectedValue = dr["AccountHeadId"].ToString();
            txtdesc.Text = dr["Description"].ToString();
            // To Get RFQ No Details
            DataTable dtrfq = POList.selectRFQDetailsById(Convert.ToInt32(dr["RFQNO"].ToString()), Convert.ToInt32(HiddenVendorId.Value));
            if (dtrfq.Rows.Count > 0)
            {
                ddrfq.Items.Insert(ddrfq.Items.Count, new ListItem(dtrfq.Rows[0]["RFQNo"].ToString(), dtrfq.Rows[0]["agentid"].ToString()));
            }
           
            ddrfq.SelectedValue = dr["RFQNO"].ToString();
            //*****************************
            txtamount.Text =Convert.ToDouble(dr["AmountLocal"].ToString()).ToString("0.00");
            txtamtused.Text = Convert.ToDouble(dr["AmountUsd"].ToString()).ToString("0.00");
        }
        showamoutdetails();
        this.btn_save.Enabled = false;
        this.btn_Printpo.Enabled = true && Auth.isPrint;
    }
    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_Port();
    }

    protected void gvPOList_PreRender(object sender, EventArgs e)
    {
        lbl_gvpo.Visible = true;
        if (gvPOList.Rows.Count <= 0)
        {
            lbl_gvpo.Text = "No Record Found!";
        }
        else
        {
            lbl_gvpo.Text = "";
        }
    }
    private void showamoutdetails()
    {
        int byear;
        byear=Convert.ToDateTime(this.txtpodt.Text).Year;  
       
                this.txtbudget.Text = "0.00";
                this.txtcommitted.Text = "0.00";
                this.txtActual.Text = "0.00";
                this.txtbudget.Text = "0.00";        
                this.txtcunsumed.Text ="0.00";
                this.txtremaining.Text = "0.00";
                
        DataTable dt = POList.SelectAmountDetails(Convert.ToInt32(this.ddvessel.SelectedValue), byear);
        
        foreach (DataRow dr in dt.Rows)
        {
            try
            {
        
                this.txtbudget.Text = dr["AnnualBudget"].ToString();
              this.txtcommitted.Text = Convert.ToString(Math.Round(Convert.ToDouble(dr["AmtCommited"].ToString()),2));
                this.txtActual.Text = dr["Actual"].ToString();
                if (txtcommitted.Text == "0")
                {
                    txtcommitted.Text = "0.00";
                }
                if (this.txtbudget.Text == "")
                {
                    this.txtbudget.Text = "0.00";
                }
                if (this.txtcommitted.Text == "")
                {
                    txtcommitted.Text = "0.00";
                }
                if (this.txtActual.Text == "")
                {
                    this.txtActual.Text = "0.00";
                }

                this.txtcunsumed.Text = Convert.ToString(Convert.ToDecimal(this.txtcommitted.Text) + Convert.ToDecimal(this.txtActual.Text));
                this.txtremaining.Text = Convert.ToString(Convert.ToDecimal(this.txtbudget.Text) - Convert.ToDecimal(this.txtcunsumed.Text));
                if (Convert.ToDecimal(txtbudget.Text) > 0)
                {
                    this.txtutilization.Text = Convert.ToString(Math.Round(Convert.ToDecimal(txtcunsumed.Text) * 100 / Convert.ToDecimal(txtbudget.Text), 2));
                }
                else
                {
                    this.txtutilization.Text = "0.00";
                }
            }
            catch (SystemException es)
            { 

            }
        }
    }
    
    protected void txtpodt_TextChanged(object sender, EventArgs e)
    {
        Bindgrid1(); 
        showamoutdetails();

    }
    protected void gvPOList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int poid;
        poid = Convert.ToInt32(gvPOList.DataKeys[e.RowIndex].Value.ToString());
        DataTable dt = POList.SelectInvoiceAccordingToPO(poid);
        if (dt.Rows.Count > 0)
        {
        
            this.Label1 .Text = "Invoice Generated For this PO.So It Can't be Cancelled";
            return;
        }
        else
        {
            POList.CancelPO(poid);
            Bindgrid2();
            this.gvPOList.SelectedIndex = -1;
        }


    
    }
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        
        DataTable dt=POList.SelectPO(Convert.ToInt32(ddl_VesselName.SelectedValue),Convert.ToInt32(DropDownList2.SelectedValue),this.txtsearchpo.Text,Convert.ToInt32(ddl_VendorName.SelectedValue),Convert.ToInt32(Session["loginid"].ToString()));
        this.gvPOList.DataSource = dt;
        this.gvPOList.DataBind();
        this.gvPOList.SelectedIndex = -1;
         
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_Port_forSearch();
    }
    protected void btn_Printpo_Click1(object sender, EventArgs e)
    {
        try
        {
            Session.Add("POId_Print", Convert.ToInt32(gvPOList.DataKeys[gvPOList.SelectedIndex].Value.ToString()));
            Session.Add("VendorId_Print", Convert.ToInt32(HiddenVendorId.Value));
            //=========
            Session.Add("VesselId_Print",Convert.ToInt32(HiddenFieldVesselId.Value));
            Session.Add("PoDate_Print",Convert.ToDateTime(HiddenFieldPodate.Value));
            //=========
            Response.Redirect("../Reporting/PO Printing.aspx");
        }
        catch
        {

        }
    }

    protected void btn_checkcount_Click(object sender, EventArgs e)
    {
        try
        {
            string checkcount = "";
            for (int cc = 0; cc < this.GvPO.Rows.Count - 1; cc++)
            {
                DropDownList ddrfqno = ((DropDownList)this.GvPO.Rows[cc].FindControl("ddrfq"));
                if (checkcount == "")
                {
                    checkcount = ddrfqno.SelectedValue;
                }
                else
                {
                    checkcount = checkcount + "," + ddrfqno.SelectedValue;
                }
            }
            DataTable dtcount = POList.getTotalCountofCrew(Convert.ToInt32(ddvendor.SelectedValue), checkcount);
            foreach (DataRow dr in dtcount.Rows)
            {
                lbl_CrewCount.Text = dr["CrewCount"].ToString();
            }
        }
        catch
        {
            lbl_CrewCount.Text = "0";
        }
    }
    ////********
    //protected string checkcountcrew()
    //{
    //    string t="";
    //    try
    //    {
    //        string checkcount = "";
    //        for (int cc = 0; cc < this.GvPO.Rows.Count - 1; cc++)
    //        {
    //            DropDownList ddrfqno = ((DropDownList)this.GvPO.Rows[cc].FindControl("ddrfq"));
    //            if (checkcount == "")
    //            {
    //                checkcount = ddrfqno.SelectedValue;
    //            }
    //            else
    //            {
    //                checkcount = checkcount + "," + ddrfqno.SelectedValue;
    //            }
    //        }
    //        DataTable dtcount = POList.getTotalCountofCrew(Convert.ToInt32(ddvendor.SelectedValue), checkcount);
    //        foreach (DataRow dr in dtcount.Rows)
    //        {
    //            t = dr["CrewCount"].ToString();
    //        }
    //        return t;
    //    }
    //    catch
    //    {
    //        t = "0";
    //        return t;
    //    }
    //}
    ////********
}
