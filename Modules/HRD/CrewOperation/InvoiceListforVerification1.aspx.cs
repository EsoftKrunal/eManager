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

public partial class CrewOperation_InvoiceListforVerification1 : System.Web.UI.Page
{
    int LoginId;
    Authority Auth;
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        Session["PageName"] = " - Accounts Verification"; 
        lbl_Message.Text = "";
        LoginId = Convert.ToInt32(Session["loginid"].ToString());
        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()),79);
        if (chpageauth <= 0)
        {
            Response.Redirect("Dummy3.aspx");

        }
        //*******************
        ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 8);
        OBJ.Invoke();
        Session["Authority"] = OBJ.Authority;
        Auth = OBJ.Authority;
        if (!Page.IsPostBack)
        {
            int mode=0;
            ddstatus.Items.Add(new ListItem("Invoice for Approval", "2"));
            ddstatus.Items.Add(new ListItem("Invoice for Payment", "4"));
            bindForwardToddl(2);
            try { mode = Convert.ToInt32(Request.QueryString["mode"]); }catch { }
            ddstatus.SelectedIndex = mode;
            btnAddInvoice.Visible = Auth.isVerify;
            btnAddInvoice.Text = "ForWard"; 
            try
            {
                int retno=0;
                
                DataTable dt33 = InvoiceVerification.SelectAuthorityOfDepartment(out retno,LoginId);
                if (retno > 0)
                {
                    bindVesselNameddl();
                    BindVendorDropDown();
                           
                    //ddstatus.SelectedIndex = 1;
                    Bindgrid(gvinvoice.Attributes["MySort"], txtrefno.Text, txtInvno.Text, Convert.ToInt32(ddvendor.SelectedValue), Convert.ToInt32(ddVessel.SelectedValue),LoginId, 0, Convert.ToInt32(ddstatus.SelectedValue));
                }
                else
                {
                    Response.Redirect("Dummy3.aspx?mess=You are not authorised to open Invoice Verification.");
                    return;
                }
            }
            catch
            {
               
            }
            
        }
    }
    private void Bindgrid(String Sort,string refno, string Invno, int ven, int vess,int loginid, int stat, int pageid)
    {
        DataTable dt1 = InvoiceList1.selectDataInvoiceDetails(refno, Invno, ven, vess, -1, stat, pageid);
        DataView dv = dt1.DefaultView;
        dv.Sort = Sort;
        this.gvinvoice.DataSource = dv;
        this.gvinvoice.DataBind();
        gvinvoice.Attributes.Add("MySort", Sort);
    }
    protected void on_Sorting(object sender, GridViewSortEventArgs e)
    {
        Bindgrid(e.SortExpression, txtrefno.Text, txtInvno.Text, Convert.ToInt32(ddvendor.SelectedValue), Convert.ToInt32(ddVessel.SelectedValue), LoginId,0, Convert.ToInt32(ddstatus.SelectedValue));
    }
    protected void on_Sorted(object sender, EventArgs e)
    {

    }
    private void BindVendorDropDown()
    {
        DataTable dt = InvoiceList1.selectDataVendor();
        this.ddvendor.DataValueField = "VendorId";
        this.ddvendor.DataTextField = "VendorName";
        this.ddvendor.DataSource = dt;
        this.ddvendor.DataBind();
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        Label2.Visible = false;
        Bindgrid(gvinvoice.Attributes["MySort"], txtrefno.Text, txtInvno.Text, Convert.ToInt32(ddvendor.SelectedValue), Convert.ToInt32(ddVessel.SelectedValue), LoginId, 0, Convert.ToInt32(ddstatus.SelectedValue));
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
    protected void btn_addinvoice_Click(object sender, EventArgs e)
    {
        string s;
        CheckBox chk;
        s = "";
        for (int i = 0; i < gvinvoice.Rows.Count; i++)
        {
            chk=(CheckBox)gvinvoice.Rows[i].FindControl("chk_Ok");
            if (chk.Checked)
            {
                s = s + ","+ gvinvoice.DataKeys[i].Value.ToString();
            }
        }
        s = (s.Trim() == "") ? "" : s.Substring(1);
        if (s == "")
        {
            lbl_Message.Text = "Please Select At least One Invoice.";
            return;
        }
        InvoiceVerification.insertUpdateInvoiceDetails(Convert.ToInt32(ddstatus.SelectedValue), Convert.ToInt32(ddl_User.SelectedValue), s, Convert.ToInt32(Session["loginid"].ToString()));
        btn_search_Click(new object(), new EventArgs());
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
    protected void gvinvoice_selectedIndex(object sender, EventArgs e)
    {
        int mode=0;
        Label2.Visible = false;
        if (ddstatus.SelectedIndex == 0)
        {
            Response.Redirect("InvoiceEntry.aspx?InvoiceId=" + gvinvoice.DataKeys[gvinvoice.SelectedIndex].Value.ToString() + "&mode=" + mode + "&backpage=1");
        }
        else 
        {
            Response.Redirect("InvoiceApproval.aspx?InvoiceId=" + gvinvoice.DataKeys[gvinvoice.SelectedIndex].Value.ToString() + "&page=pay&backpage=1");
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
            Response.Redirect("InvoiceEntry.aspx?InvoiceId=" + gvinvoice.DataKeys[gvinvoice.SelectedIndex].Value.ToString() + "&mode=" + mode);
            Label2.Visible = false;
        }
        else
        {
            Label2.Visible = true;
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
    
    protected void bindForwardToddl(int Mode)
    {
        DataTable dt1 = InvoiceEntry.selectUserNames(Mode); //- Verification department users
        this.ddl_User.DataValueField = "LoginId";
        this.ddl_User.DataTextField = "UserName";
        this.ddl_User.DataSource = dt1;
        this.ddl_User.DataBind();
    }
    protected void ddstatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddstatus.SelectedIndex == 0)
        {
            bindForwardToddl(2);
            btnAddInvoice.Text = "Forward"; 
        }
        else
        {
            bindForwardToddl(3);
            btnAddInvoice.Text = "Forward";
        }
        btn_search_Click(new object(), new EventArgs()); 
    }
}
