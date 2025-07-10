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

public partial class AllInvoiceList : System.Web.UI.Page
{
    int LoginId;
    AuthenticationManager Auth;
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        Session["PageName"] = " - Invoice Enquiry"; 
        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 137);
        if (chpageauth <= 0)
        {
            Response.Redirect("Dummy3.aspx");
        }

        //*******************
        
        LoginId = Convert.ToInt32(Session["loginid"].ToString());
        Auth = new AuthenticationManager(137, LoginId, ObjectType.Page);  
        Label2.Text = "";
        if (!Page.IsPostBack)
        {
            try
            {
                    bindVesselNameddl();
                    BindVendorDropDown();
                    bindstatusdropdown();
                    bindCurrencyddl(); 
                    int yr=DateTime.Today.Year-1;
                    ddlYear.Items.Add(new ListItem(yr.ToString(), yr.ToString()));
                    yr = yr - 1;
                    ddlYear.Items.Add(new ListItem(yr.ToString(), yr.ToString()));
                    yr = yr - 1;
                    ddlYear.Items.Add(new ListItem(yr.ToString(), yr.ToString()));       
            }
            catch
            {
               
            }
            ddlYear.Visible = false;
            Button1.Visible = false;
            lblYear.Visible = false;
  
            DataTable dt = Budget.getTable("select roleid from userlogin where loginid=" + LoginId.ToString()).Tables[0];
            if (dt !=null)
                if (dt.Rows.Count > 0)
                {
                    if (int.Parse(dt.Rows[0]["RoleId"].ToString()) == 1)
                    {
                        ddlYear.Visible = true;
                        Button1.Visible = true;
                        lblYear.Visible = true;
                    }
                }
            
        }
    }
    private void clear()
    {
        lbl_RefNo.Text = "";
        ddCurrency.SelectedIndex = 0; 
        lbl_DueDate.Text = "";
        txt_InvAmount.Text = "";
        lbl_InvoiceDate.Text = "";
        txt_InvNo.Text = "";
        //lbl_PoAmt.Text = "";
        //lbl_PONo.Text = "";
        lbl_Remarks.Text = "";
        lbl_TotalInvAmt.Text = "";
        lbl_Vendor.Text = "";
        ddl_Vessel.SelectedIndex=0;
        lbl_VoucherNo.Text = "";
    }
    private void Bindgrid(String Sort,string refno, string Invno, int ven, int vess,int loginid, int stat, int pageid)
    {
        DataTable dt1 = cls_AllInvoiceList.SelectInvoice_Search_All(refno, Invno, ven, vess,stat);
        dt1.DefaultView.Sort = Sort;
        this.gvinvoice.DataSource = dt1;
        this.gvinvoice.DataBind();
        gvinvoice.Attributes.Add("MySort", Sort);
    }
    protected void on_Sorting(object sender, GridViewSortEventArgs e)
    {
        Bindgrid(e.SortExpression, txtrefno.Text, txtInvno.Text, Convert.ToInt32(ddvendor.SelectedValue), Convert.ToInt32(ddVessel.SelectedValue), LoginId, Convert.ToInt32(ddstatus.SelectedValue), 1);
    }
    private void bindstatusdropdown()
    {
        DataTable dt33 = InvoiceList1.selectDataAllStatusSearchMode();
        this.ddstatus.DataValueField = "SearchMode";
        this.ddstatus.DataTextField = "SearchModeName";
        this.ddstatus.DataSource = dt33;
        this.ddstatus.DataBind();
        ddstatus.Items.Add(new ListItem("Cancelled", "5"));
        ddstatus.Items.Add(new ListItem("Archived", "6"));
    }
    private void BindVendorDropDown()
    {
        DataTable dt = InvoiceList1.selectDataVendor();
        this.ddvendor.DataValueField = "VendorId";
        this.ddvendor.DataTextField = "VendorName";
        this.ddvendor.DataSource = dt;
        this.ddvendor.DataBind();
        
        DataSet ds = cls_SearchReliever.getMasterData("Vessel", "VesselId", "VesselName", Convert.ToInt32(Session["loginid"].ToString()));
        ddVessel.DataSource = ds.Tables[0];
        ddVessel.DataValueField = "VesselId";
        ddVessel.DataTextField = "VesselName";
        ddVessel.DataBind();
        ddVessel.Items.Insert(0, new ListItem("<Select>", "0"));
    }
    protected void bindVesselNameddl()
    {
        DataSet ds = cls_SearchReliever.getMasterData("Vessel", "VesselId", "VesselName", Convert.ToInt32(Session["loginid"].ToString()));
        ddl_Vessel.DataSource = ds.Tables[0];
        ddl_Vessel.DataValueField = "VesselId";
        ddl_Vessel.DataTextField = "VesselName";
        ddl_Vessel.DataBind();
        ddl_Vessel.Items.Insert(0, new ListItem("<Select>", "0"));
    }
    protected void bindCurrencyddl()
    {
        DataTable dt11 = InvoiceEntry.selectCurrencyDD();
        this.ddCurrency.DataValueField = "CurrencyId";
        this.ddCurrency.DataTextField = "CurrencyName";
        this.ddCurrency.DataSource = dt11;
        this.ddCurrency.DataBind();
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        if (txtrefno.Text.Trim() == "" && txtInvno.Text.Trim() == "" && ddvendor.SelectedIndex == 0 && ddVessel.SelectedIndex == 0)
        {
            Label2.Text = "Please select at least two filters.";
        }
        else
        {
            Bindgrid(gvinvoice.Attributes["MySort"], txtrefno.Text, txtInvno.Text, Convert.ToInt32(ddvendor.SelectedValue), Convert.ToInt32(ddVessel.SelectedValue), LoginId, Convert.ToInt32(ddstatus.SelectedValue), 1);
        }
    }
    protected void btn_clear_Click(object sender, EventArgs e)
    {
        Label2.Visible = false;
        txtrefno.Text = "";
        txtInvno.Text = "";
        ddvendor.SelectedIndex = 0;
        ddVessel.SelectedIndex = 0;
        ddstatus.SelectedIndex = 0;
    }
    protected void gvinvoice_selectedIndex(object sender, EventArgs e)
    {
        int invoiceid,mode=0;
        Label2.Visible = false;
        btn_Save.Visible = false ;
        invoiceid = Convert.ToInt32(gvinvoice.DataKeys[gvinvoice.SelectedIndex].Value.ToString());
        Session["Inv_Id"] = invoiceid.ToString();
        Show_Record_gvinvoice(invoiceid);
        InvoiceStatus1.InoviceId = invoiceid;
        InvoiceStatus1.Refresh();
    }
    protected void Show_Record_gvinvoice(int Invoiceid)
    {
        HiddenEnquiry.Value = Session["Inv_Id"].ToString();
        DataTable dt33 = InvoiceSearchScreen.selectDataInvoiceEnquiryDetailsById(Convert.ToInt32(HiddenEnquiry.Value));
        foreach (DataRow dr in dt33.Rows)
        {
            pnl_Det.Visible = true;
            lbl_RefNo.Text = dr["RefNo"].ToString();
            txt_InvNo.Text = dr["InvNo"].ToString();
            lbl_Vendor.Text = dr["VendorId"].ToString();
            double dbl = Convert.ToDouble(dr["InvoiceAmount"].ToString());
            txt_InvAmount.Text = dbl.ToString("F");
            lbl_InvoiceDate.Text = dr["Invdate"].ToString();
            lbl_DueDate.Text = dr["Duedate"].ToString();
            ddl_Vessel.SelectedValue= dr["VesselId1"].ToString();
            ddCurrency.SelectedValue = dr["CurrId"].ToString();
            //lbl_PONo.Text = dr["PoNo"].ToString();
            if (dr["PoAmount"].ToString() == "")
            {
             //   lbl_PoAmt.Text = "0.0";
            }
            else
            {
                double dbl3 = Convert.ToDouble(dr["PoAmount"].ToString());
              //  lbl_PoAmt.Text = dbl3.ToString("F");
            }
            double dbl2 = Convert.ToDouble(dr["TotalInvoiceAmount"].ToString());
            lbl_TotalInvAmt.Text = dbl2.ToString("F");
            lbl_VoucherNo.Text = dr["VoucherNo"].ToString();
            lbl_Remarks.Text=dr["Remarks"].ToString();
        }
    }
    protected void gvinvoice_Row_Editing(object sender, GridViewEditEventArgs e)
    {
        int mode = 1;
        HiddenField hfdInvoice;
        hfdInvoice = (HiddenField)gvinvoice.Rows[e.NewEditIndex].FindControl("HiddenVerifyStatusId");
        gvinvoice.SelectedIndex = e.NewEditIndex;
        if (Convert.ToInt32(hfdInvoice.Value.ToString()) >= 1)
        {
            Label2.Visible = true;
        }
        else
        {
            Response.Redirect("InvoiceEntry.aspx?InvoiceId=" + gvinvoice.DataKeys[gvinvoice.SelectedIndex].Value.ToString() + "&mode=" + mode);
            Label2.Visible = false;
        }
    }
    protected void gvinvoice_PreRender(object sender, EventArgs e)
    {
        if (this.gvinvoice.Rows.Count <= 0)
        {
            Label1.Text = "No Records Found..!";
        }
        else
        {
            Label1.Text = "";
        }
        this.gvinvoice.Columns[1].Visible = Auth.IsUpdate;
        this.gvinvoice.Columns[2].Visible = Auth.IsDelete;
    }
    protected void gvinvoice_DataBound(object sender, GridViewRowEventArgs e)
    {
        Image img = ((Image)e.Row.FindControl("imgattach"));
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (DataBinder.Eval(e.Row.DataItem, "Attachment") == null)
            {

                img.Visible = false;
            }
            else if (DataBinder.Eval(e.Row.DataItem, "Attachment").ToString() == "")
            {

                img.Visible = false;
            }
            else
            {
                img.Attributes.Add("onclick", "javascript:Show_Image_Large1('../EMANAGERBLOB/Invoice/" + DataBinder.Eval(e.Row.DataItem, "Attachment").ToString() + "');");
                img.ToolTip = "Click to Preview";
                img.Style.Add("cursor", "hand");
            }
        }
    }
    protected void CanceInvoiceClick(object sender, ImageClickEventArgs e)
    {
        Int32 InvoiceId = Int32.Parse(((ImageButton)sender).CommandArgument);
        Budget.getTable("insert into CancelInvoice select * from Invoice where invoiceid=" + InvoiceId.ToString() + ";delete from invoice where invoiceid=" + InvoiceId.ToString());
        btn_search_Click(sender,new EventArgs());
    }
    protected void btn_InvArc_Click(object sender, EventArgs e)
    {
        Budget.getTable("insert into ArchiveInvoice select * from Invoice where year(createdon)=" + ddlYear.SelectedValue + ";delete from invoice where year(createdon)=" + ddlYear.SelectedValue);
        btn_search_Click(sender, new EventArgs());
    }
    protected void EditInvoiceClick(object sender, ImageClickEventArgs e)
    {
        Int32 InvoiceId = Int32.Parse(((ImageButton)sender).CommandArgument);
        Session["Inv_Id"] = InvoiceId.ToString();
        Show_Record_gvinvoice(InvoiceId);
        gvinvoice.SelectedIndex = Int32.Parse(((ImageButton)sender).ToolTip) -1;
        InvoiceStatus1.InoviceId = InvoiceId;
        btn_Save.Visible = true;
        InvoiceStatus1.Refresh();
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        Budget.getTable("Update Invoice Set Vesselid=" + ddl_Vessel.SelectedValue + ",InvoiceAmount=" + txt_InvAmount.Text + ",CurrencyId=" + ddCurrency.SelectedValue + ",InvNo='" + txt_InvNo.Text.Trim() + "' Where Invoiceid=" + Session["Inv_Id"].ToString());  
        gvinvoice.SelectedIndex = -1; 
        pnl_Det.Visible = false;
        btn_search_Click(sender, e);   
    }
    protected void btn_Cancel_Click(object sender, EventArgs e)
    {
        pnl_Det.Visible = false;
        gvinvoice.SelectedIndex = -1; 

    }
    protected void Voucher_Click(object sender, EventArgs e)
    {
        LinkButton l = (LinkButton)sender;
        Session["PrintVoucherId"] = l.CommandArgument;
        Page.ClientScript.RegisterStartupScript(this.GetType(),"abc","window.open('../Reporting/PaymentVoucher.aspx');",true);   
    }
    }
