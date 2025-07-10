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

public partial class CrewAccounting_ApprovedInvoices : System.Web.UI.Page
{
    int loginid;
    Authority Auth;
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        Session["PageName"] = " - Invoice Payment"; 
        this.txtrefno.Attributes.Add("onkeydown", "javascript:if(event.keyCode==13){document.getElementById('" + btn_search.ClientID + "').focus();}");
        this.txtInvno.Attributes.Add("onkeydown", "javascript:if(event.keyCode==13){document.getElementById('" + btn_search.ClientID + "').focus();}");
        this.ddvendor.Attributes.Add("onkeydown", "javascript:if(event.keyCode==13){document.getElementById('" + btn_search.ClientID + "').focus();}");
        this.ddVessel.Attributes.Add("onkeydown", "javascript:if(event.keyCode==13){document.getElementById('" + btn_search.ClientID + "').focus();}");
        this.lblstatus.Attributes.Add("onkeydown", "javascript:if(event.keyCode==13){document.getElementById('" + btn_search.ClientID + "').focus();}");
        loginid = Convert.ToInt32(Session["loginid"].ToString());
        Label1.Text = "";
        Label2.Text = "";
        ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 8);
        OBJ.Invoke();
        Session["Authority"] = OBJ.Authority;
        Auth = OBJ.Authority;
        if (!Page.IsPostBack)
        {
            try
            {
                //***********Code to check page acessing Permission
                int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 28);
                if (chpageauth <= 0)
                {
                    Response.Redirect("Dummy3.aspx");
                }
                //*******************

                int retno = 0;                
                DataTable dt33 = ApprovedInvoice.SelectAuthorityOfDepartment(out retno, loginid);
                if (retno > 0)
                {
                    bindstatus();
                    bindVesselNameddl();
                    BindVendorDropDown();
                    //Bindgrid(gvinvoice.Attributes["MySort"]);              
                }
                else
                {
                    Response.Redirect("Dummy3.aspx?mess=You are not authorised to open Payment.");
                    return;
                }
            }
            catch
            {
            }
        }
    }
    private void Bindgrid(String Sort)
    {
        
        DataTable dt = new DataTable();
        dt = ApprovedInvoice.selectapprovedinvoice(this.txtrefno.Text, this.txtInvno.Text, Convert.ToInt32(this.ddvendor.SelectedValue), Convert.ToInt32(this.ddVessel.SelectedValue), Convert.ToInt32(Session["loginid"].ToString()), Convert.ToInt32(this.lblstatus.SelectedValue),txtMTMPvNo.Text);
        dt.DefaultView.Sort = Sort;
        gvinvoice.DataSource = dt;
        gvinvoice.DataBind();
        gvinvoice.Attributes.Add("MySort", Sort);
    }
    protected void on_Sorting(object sender, GridViewSortEventArgs e)
    {
        Bindgrid(e.SortExpression);
    }
    protected void on_Sorted(object sender, EventArgs e)
    {

    }
    private void bindstatus()
    {
        
        DataTable ds = ApprovedInvoice.getstatus();
        lblstatus.DataTextField = "SearchModeName";
        lblstatus.DataValueField = "SearchMode";
        this.lblstatus.DataSource = ds;
        this.lblstatus.DataBind();
        this.lblstatus.SelectedValue = "1";
        this.btn_Book.Enabled = true && Auth.isEdit;
        this.btnpay.Enabled=false ;


    }
    private void BindVendorDropDown()
    {
        DataTable dt = InvoiceSearchScreen.selectDataVendor();
        this.ddvendor.DataValueField = "VendorId";
        this.ddvendor.DataTextField = "VendorName";
        this.ddvendor.DataSource = dt;
        this.ddvendor.DataBind();
    }
    protected void gvinvoice_selectedIndex(object sender, EventArgs e)
    {
        Response.Redirect("InvoiceApproval.aspx?InvoiceId=" + gvinvoice.DataKeys[gvinvoice.SelectedIndex].Value.ToString() + "&page=pay" + "&backpage=3");
    }
    protected void bindVesselNameddl()
    {
        DataSet ds = cls_SearchReliever.getMasterData("Vessel", "VesselId", "VesselName", Convert.ToInt32(Session["loginid"].ToString()));
        ddVessel.DataSource = ds.Tables[0];
        ddVessel.DataValueField = "VesselId";
        ddVessel.DataTextField = "VesselName";
        ddVessel.DataBind();
        ddVessel.Items.Insert(0, new ListItem("<Select>", "0"));
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        this.btn_Book.Enabled = true && Auth.isEdit;
        this.btnpay.Enabled = true && Auth.isEdit;
        if (lblstatus.SelectedIndex == 0)
        {
            if (txtrefno.Text.Trim() == "" && txtInvno.Text.Trim() == "" && ddvendor.SelectedIndex == 0 && ddVessel.SelectedIndex == 0 && txtMTMPvNo.Text.Trim()=="")
            {
                Label2.Text = "Please Select Atleast One More Filter Condition.";
                return;
            }
        }
        else if (lblstatus.SelectedIndex == 1)
        {
            if (txtrefno.Text.Trim() == "" && txtInvno.Text.Trim() == "" && ddvendor.SelectedIndex == 0 && ddVessel.SelectedIndex == 0 && txtMTMPvNo.Text.Trim() == "")
            {
                Label2.Text = "Please Select Atleast One More Filter Condition.";
                return;
            }
            this.btn_Book.Enabled = true && Auth.isEdit;
            this.btnpay.Enabled = false;
        }
        else if (lblstatus.SelectedIndex == 2)
        {
            this.btn_Book.Enabled = false;
            this.btnpay.Enabled =true && Auth.isEdit;
        }
        else if (lblstatus.SelectedIndex == 3)
        {
            if (txtrefno.Text.Trim() == "" && txtInvno.Text.Trim() == "" && ddvendor.SelectedIndex == 0 && ddVessel.SelectedIndex == 0 && txtMTMPvNo.Text.Trim() == "")
            {
                Label2.Text = "Please Select Atleast One More Filter Condition.";
                return;
            }
            this.btn_Book.Enabled = false;
            this.btnpay.Enabled =false;
        }
        Bindgrid(gvinvoice.Attributes["MySort"]);
        if (lblstatus.SelectedIndex == 2)
        {
            this.gvinvoice.Columns[14].Visible = false;
        }
        else

        {
            this.gvinvoice.Columns[14].Visible = true;
        }
    }
    protected void btn_clear_Click(object sender, EventArgs e)
    {
        this.txtInvno.Text = "";
        this.txtrefno.Text = "";
        this.ddvendor.SelectedIndex = 0;
        this.ddVessel.SelectedIndex = 0;
        this.lblstatus.SelectedIndex = 0;
    }
    protected void btn_pay_Click(object sender, EventArgs e)
    {
        string newvendorid = "";
        string vendorid = "";
        string newcurrency = "";
        string currency = "";
        string venid="";
        string invoiceid="";
        Boolean vendorcount=false;
        Boolean currencycount = false;
        for (int i = 0; i < this.gvinvoice.Rows.Count; i++)
        {
            string status = ((Label)this.gvinvoice.Rows[i].FindControl("lblstatus")).Text; 
            if(((CheckBox)gvinvoice.Rows[i].FindControl("chkpay")).Checked==true && status !="3" )
            {
                newvendorid = ((Label)this.gvinvoice.Rows[i].FindControl("lblvendor")).Text;
                newcurrency = ((Label)this.gvinvoice.Rows[i].FindControl("lblcurrency")).Text;

                if (vendorid != newvendorid && vendorid != "")
                {
                    Label1.Text = "Select Single Vendor For Payment";
                    vendorcount = true; 
                    break;
                }
                else
                {
                    vendorid = newvendorid;
                    venid=((Label)this.gvinvoice.Rows[i].FindControl("lblvenid")).Text; 
                }
                if (currency != newcurrency && currency != "")
                {
                    Label1.Text = "Select Single Currency For Payment";
                    currencycount = true;
                    break;
                }
                else
                {
                    currency = newcurrency;
                }
                if (invoiceid == "")
                {
                    invoiceid = this.gvinvoice.DataKeys[i].Value.ToString();
                }
                else
                {
                    invoiceid = invoiceid + "," + this.gvinvoice.DataKeys[i].Value.ToString();
                }
            }
        }
        if (vendorcount == false && currencycount==false)
        {
            if (invoiceid == "")
            {
                Label1.Text = "Select Atleast One invoice for pay";

            }
            else
            {
                Session.Add("payinvoice", invoiceid);
                Response.Redirect("~/CrewOperation/InvoicePayment.aspx?Vendorname=" + vendorid + "&vendorid=" + venid);
            }
        }
        
    }
  
    protected void gvinvoice_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (((Label)e.Row.FindControl("lblstatus")).Text == "1")
            {
                CheckBox chk = ((CheckBox)e.Row.FindControl("chkpay"));
                chk.Enabled = false;
                CheckBox chkbook = ((CheckBox)e.Row.FindControl("chkbook"));
                chkbook.Enabled = true;
            }
            else if( ((Label)e.Row.FindControl("lblstatus")).Text == "0")
            {
                CheckBox chk = ((CheckBox)e.Row.FindControl("chkpay"));
                chk.Enabled = false;
                chk.Checked=false;
                CheckBox chkbook = ((CheckBox)e.Row.FindControl("chkbook"));
                chkbook.Enabled = false; 
                chkbook.Checked=false;
            }
            else if (((Label)e.Row.FindControl("lblstatus")).Text == "2")
            {
                CheckBox chk = ((CheckBox)e.Row.FindControl("chkpay"));
                chk.Enabled = true;
                CheckBox chkbook = ((CheckBox)e.Row.FindControl("chkbook"));
                chkbook.Checked = true;
                chkbook.Enabled = false;
            }
            else if (((Label)e.Row.FindControl("lblstatus")).Text == "3")
            {
                CheckBox chk = ((CheckBox)e.Row.FindControl("chkpay"));
                chk.Enabled = false;
                chk.Checked = true;
                CheckBox chkbook = ((CheckBox)e.Row.FindControl("chkbook"));
                chkbook.Checked = true;
                chkbook.Enabled = false;
            }
        }

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
    protected void btn_Book_Click(object sender, EventArgs e)
    {
        string invoiceid="";
        for (int i = 0; i < this.gvinvoice.Rows.Count; i++)
        {
            CheckBox ckhbook = ((CheckBox)this.gvinvoice.Rows[i].FindControl("chkbook"));
            if (ckhbook.Checked == true)
            {
                if (invoiceid == "")
                {
                    invoiceid = this.gvinvoice.DataKeys[i].Value.ToString();
                }
                else
                {
                    invoiceid = invoiceid + "," + this.gvinvoice.DataKeys[i].Value.ToString();
                }
                
            }
        }
        if (invoiceid == "")
        {
            Label1.Text = "Select Atleast One invoice for Book.";
        }
        else
        {
            ApprovedInvoice.updatebookinvoice(invoiceid);
            btn_Book.Enabled = false;
        }
        Bindgrid(gvinvoice.Attributes["MySort"]); 
    }
    protected void gvinvoice_PreRender(object sender, EventArgs e)
    {
        if (this.gvinvoice.Rows.Count <= 0)
        {
            this.Label1.Visible = true;
            Label1.Text = "No Records Found..!";
        }
        
    }
    protected void lblstatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        //btn_search_Click(sender, e);
    }
    protected void gvinvoice_rowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            HiddenField hfdvoucher;
            GridViewRow gr; 
            if (e.CommandSource.GetType().ToString()=="System.Web.UI.WebControls.LinkButton")
            {
                LinkButton l=(LinkButton )e.CommandSource;
                gr = (GridViewRow)l.Parent.Parent;
                hfdvoucher = (HiddenField)gr.FindControl("hiddenVoucherId");
                Session["PrintVoucherId"] = hfdvoucher.Value.ToString();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "abc", "window.open('../Reporting/PaymentVoucher.aspx');", true);      
                //Response.Redirect("~/Reporting/PaymentVoucher.aspx");
            }
            else
            {
                hfdvoucher = (HiddenField)gvinvoice.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("hiddenVoucherId");
                Response.Write(hfdvoucher.Value);  
            }
        }
    }
    protected void GridRowEditing(object sender, GridViewEditEventArgs e)
    {

    }
}
