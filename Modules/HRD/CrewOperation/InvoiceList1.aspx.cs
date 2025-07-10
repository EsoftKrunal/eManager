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

public partial class CrewOperation_InvoiceList1 : System.Web.UI.Page
{
    int LoginId;
    Authority Auth;
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        Session["PageName"] = " - Invoice Entry"; 
        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()),26);
        if (chpageauth <= 0)
        {
            Response.Redirect("Dummy3.aspx");

        }
        LoginId = Convert.ToInt32(Session["loginid"].ToString());
        //*******************
        ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 8);
        OBJ.Invoke();
        Session["Authority"] = OBJ.Authority;
        Auth = OBJ.Authority;
        if (!Page.IsPostBack)
        {
            try
            {
                int retno=0;
               
                DataTable dt33 = InvoiceList1.SelectAuthorityOfDepartment(out retno,LoginId);
                if (retno > 0)
                {
                    bindVesselNameddl();
                    BindVendorDropDown();
                    bindstatusdropdown();
                    Bindgrid(gvinvoice.Attributes["MySort"], txtrefno.Text, txtInvno.Text, Convert.ToInt32(ddvendor.SelectedValue), Convert.ToInt32(ddVessel.SelectedValue), 0, Convert.ToInt32(ddstatus.SelectedValue), 1);
                    Handle_Authority();
                    btnAddInvoice.Visible = Auth.isAdd;
                }
                else
                {
                    Response.Redirect("dummy3.aspx?mess=You are not authorised to open Invoice Entry.");
                    return;
                }
            }
            catch
            {
               
            }
            
        }
    }
    private void Handle_Authority()
    {
        gvinvoice.Columns[1].Visible = Auth.isEdit;
    }
    private void Bindgrid(String Sort,string refno, string Invno, int ven, int vess,int loginid, int stat, int pageid)
    {
        DataTable dt1 = InvoiceList1.selectDataInvoiceDetails(refno, Invno, ven, vess, loginid, stat,pageid);
        dt1.DefaultView.Sort = Sort;
        this.gvinvoice.DataSource = dt1;
        this.gvinvoice.DataBind();
        gvinvoice.Attributes.Add("MySort", Sort);
    }
    protected void on_Sorting(object sender, GridViewSortEventArgs e)
    {
        Bindgrid(e.SortExpression, txtrefno.Text, txtInvno.Text, Convert.ToInt32(ddvendor.SelectedValue), Convert.ToInt32(ddVessel.SelectedValue), LoginId, Convert.ToInt32(ddstatus.SelectedValue), 1);
    }
    protected void on_Sorted(object sender, EventArgs e)
    {

    }
    private void bindstatusdropdown()
    {
        DataTable dt33 = InvoiceList1.selectDataStatus();
        this.ddstatus.DataValueField = "SearchMode";
        this.ddstatus.DataTextField = "SearchModeName";
        this.ddstatus.DataSource = dt33;
        this.ddstatus.DataBind();
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
        Bindgrid(gvinvoice.Attributes["MySort"], txtrefno.Text, txtInvno.Text, Convert.ToInt32(ddvendor.SelectedValue), Convert.ToInt32(ddVessel.SelectedValue), LoginId, Convert.ToInt32(ddstatus.SelectedValue), 1);
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
        int mode = 1;
        int a=0;
        int dup = 1;
        Response.Redirect("InvoiceEntry.aspx?InvoiceId=" + a + "&mode=" + mode + "&backpage=2" + "&duplicacy=" + dup);
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
        int dup = 1;
        Label2.Visible = false;
        Response.Redirect("InvoiceEntry.aspx?InvoiceId=" + gvinvoice.DataKeys[gvinvoice.SelectedIndex].Value.ToString() + "&mode=" + mode + "&backpage=2" + "&duplicacy=" + dup);
    }
    protected void gvinvoice_Row_Editing(object sender, GridViewEditEventArgs e)
    {
        int mode = 1;
        int dup = 2;
        HiddenField hfdInvoice;
        hfdInvoice = (HiddenField)gvinvoice.Rows[e.NewEditIndex].FindControl("HiddenVerifyStatusId");
        gvinvoice.SelectedIndex = e.NewEditIndex;
        if (Convert.ToInt32(hfdInvoice.Value.ToString()) >= 1)
        {
            Label2.Visible = true;
        }
        else
        {
            Response.Redirect("InvoiceEntry.aspx?InvoiceId=" + gvinvoice.DataKeys[gvinvoice.SelectedIndex].Value.ToString() + "&mode=" + mode + "&backpage=2" + "&duplicacy=" + dup);
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
}
