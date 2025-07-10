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
using ShipSoft.CrewManager.Operational;
using ShipSoft.CrewManager.BusinessObjects;
using ShipSoft.CrewManager.BusinessLogicLayer;
using ShipSoft.CrewManager.Operational;

public partial class CrewOperation_InvoiceApprovalSearchScreen : System.Web.UI.Page
{
    int loginid;
    Authority Auth;
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        Session["PageName"] = " - Invoice Approval"; 
        this.txtrefno.Attributes.Add("onkeydown", "javascript:if(event.keyCode==13){document.getElementById('" + btn_search.ClientID + "').focus();}");
        this.txtInvno.Attributes.Add("onkeydown", "javascript:if(event.keyCode==13){document.getElementById('" + btn_search.ClientID + "').focus();}");
        this.ddvendor.Attributes.Add("onkeydown", "javascript:if(event.keyCode==13){document.getElementById('" + btn_search.ClientID + "').focus();}");
        this.ddl_Status.Attributes.Add("onkeydown", "javascript:if(event.keyCode==13){document.getElementById('" + btn_search.ClientID + "').focus();}");
        this.ddVessel.Attributes.Add("onkeydown", "javascript:if(event.keyCode==13){document.getElementById('" + btn_search.ClientID + "').focus();}");
        //***********Code to check page acessing Permission
        loginid = Convert.ToInt32(Session["loginid"].ToString());
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 27);
        if (chpageauth <= 0)
        {
            Response.Redirect("Dummy3.aspx");

        }
        //*******************
        ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 8);
        OBJ.Invoke();
        Session["Authority"] = OBJ.Authority;
        Auth = OBJ.Authority;
        loginid = Convert.ToInt32(Session["loginid"].ToString());
        if (!Page.IsPostBack)
        {
            try
            {
                int retno = 0;
                
                DataTable dt33 = InvoiceSearchScreen.SelectAuthorityOfDepartment(out retno, loginid);
                if (retno > 0)
                {
                    bindVesselNameddl();
                    BindVendorDropDown();
                    BindStatusDropDown();
                    ddl_Status.SelectedValue = "2";
                    Bindgrid(gvinvoice.Attributes["MySort"], txtrefno.Text, txtInvno.Text, Convert.ToInt32(ddvendor.SelectedValue), Convert.ToInt32(ddVessel.SelectedValue), loginid, Convert.ToInt32(ddl_Status.SelectedValue));
                }
                else
                {
                    Response.Redirect("Dummy3.aspx?mess=You are not authorised to open Invoice Approval.");
                    return;
                }
            }
            catch
            {
            }
        }
    }
    private void Bindgrid(String Sort, string refno, string invno, int vendor, int vessel, int lgid, int status)
    {
        DataTable dt1 = InvoiceSearchScreen.selectApprovalDetails(refno,invno,vendor,vessel,lgid,status);
        dt1.DefaultView.Sort = Sort;
        this.gvinvoice.DataSource = dt1;
        this.gvinvoice.DataBind();
        gvinvoice.Attributes.Add("MySort", Sort);
    }
    protected void on_Sorting(object sender, GridViewSortEventArgs e)
    {
        Bindgrid(e.SortExpression, txtrefno.Text, txtInvno.Text, Convert.ToInt32(ddvendor.SelectedValue), Convert.ToInt32(ddVessel.SelectedValue), loginid, Convert.ToInt32(ddl_Status.SelectedValue));
    }
    protected void on_Sorted(object sender, EventArgs e)
    {

    }
    private void BindVendorDropDown()
    {
        DataTable dt = InvoiceSearchScreen.selectDataVendor();
        this.ddvendor.DataValueField = "VendorId";
        this.ddvendor.DataTextField = "VendorName";
        this.ddvendor.DataSource = dt;
        this.ddvendor.DataBind();
    }
    private void BindStatusDropDown()
    {
        DataTable dt10 = InvoiceSearchScreen.selectDataInvoiceStatus();
        this.ddl_Status.DataValueField = "SearchMode";
        this.ddl_Status.DataTextField = "SearchModeName";
        this.ddl_Status.DataSource = dt10;
        this.ddl_Status.DataBind();
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        Bindgrid(gvinvoice.Attributes["MySort"], txtrefno.Text, txtInvno.Text, Convert.ToInt32(ddvendor.SelectedValue), Convert.ToInt32(ddVessel.SelectedValue), loginid, Convert.ToInt32(ddl_Status.SelectedValue));
    }
    protected void btn_clear_Click(object sender, EventArgs e)
    {
        txtrefno.Text = "";
        txtInvno.Text = "";
        ddvendor.SelectedIndex = 0;
        ddVessel.SelectedIndex = 0;
        ddl_Status.SelectedIndex = 0;
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
        Response.Redirect("InvoiceApproval.aspx?InvoiceId="+gvinvoice.DataKeys[gvinvoice.SelectedIndex].Value.ToString());
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
    protected void gvinvoice_PreRender(object sender, EventArgs e)
    {
        if (this.gvinvoice.Rows.Count <= 0)
        {
            lbl_gvinvoice.Text = "No Records Found..!";
        }
        else
        {
            lbl_gvinvoice.Text = "";
        }
    }
}
