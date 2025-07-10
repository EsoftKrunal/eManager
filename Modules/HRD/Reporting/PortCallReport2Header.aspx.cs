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

public partial class PortCallReport1Header : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        this.ddl_Vessel.Attributes.Add("onkeydown", "javascript:if(event.keyCode==13){document.getElementById('" + Button1.ClientID + "').focus();}");
        this.ddl_ToMonth.Attributes.Add("onkeydown", "javascript:if(event.keyCode==13){document.getElementById('" + Button1.ClientID + "').focus();}");
        this.ddl_Year.Attributes.Add("onkeydown", "javascript:if(event.keyCode==13){document.getElementById('" + Button1.ClientID + "').focus();}");
        this.DropDownList1.Attributes.Add("onkeydown", "javascript:if(event.keyCode==13){document.getElementById('" + Button1.ClientID + "').focus();}");
        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()),116);
        if (chpageauth <= 0)
        {
            Response.Redirect("DummyReport.aspx");

        }
        //*******************
        if (!(IsPostBack))
        {
            BindVendorDDL();
            DataSet dt8 = SearchSignOff.getVessel(Convert.ToInt32(Session["loginid"].ToString()));
            this.ddl_Vessel.DataSource = dt8;
            this.ddl_Vessel.DataValueField = "VesselId";
            this.ddl_Vessel.DataTextField = "Name";
            this.ddl_Vessel.DataSource = dt8;
            this.ddl_Vessel.DataBind();
            ddl_Vessel.Items.Insert(0, new ListItem("< All >", "0"));
            for(int i=2000;i<2050;i++)
            {
                ddl_Year.Items.Add( new ListItem(i.ToString(),i.ToString()));    
            }
            ddl_Year.Items.Insert(0,new ListItem(" < Select >","0"));
            ddl_Year.SelectedValue = DateTime.Today.Year.ToString();
            ddl_ToMonth.SelectedIndex = DateTime.Today.Month;
        }
    }
    public void BindVendorDDL()
    {
        DataTable dt = cls_PortCallReport2.selectVendorName_forReport();
        this.ddl_Vendor.DataValueField = "VendorId";
        this.ddl_Vendor.DataTextField = "VendorName";
        this.ddl_Vendor.DataSource = dt;
        this.ddl_Vendor.DataBind();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        //if (DropDownList1.SelectedIndex == 0)
        //{
        //    Session["ReportPortCall1_Header"] = " Monthly port call's PO details for the month of " + ddl_ToMonth.SelectedItem.ToString() + " - " + ddl_Year.SelectedItem.ToString() + ", Vessel : " + ddl_Vessel.SelectedItem.ToString() + ", PoStatus : Open " + ", Vendor : " + ddl_Vendor.SelectedItem.ToString();
        //}
        //else
        //{
        //    Session["ReportPortCall1_Header"] = " Monthly port call's PO details for the month of " + ddl_ToMonth.SelectedItem.ToString() + " - " + ddl_Year.SelectedItem.ToString() + ", Vessel : " + ddl_Vessel.SelectedItem.ToString() + ", PoStatus : Closed " + ", Vendor : " + ddl_Vendor.SelectedItem.ToString();
        //}
        //IFRAME1.Attributes.Add("src", "PortCallReport2.aspx?Month=" + ddl_ToMonth.SelectedValue + "&Year=" + ddl_Year.SelectedValue + "&Vesselid=" + ddl_Vessel.SelectedValue + "&PoStatus=" + DropDownList1.SelectedValue + "&Vendor=" + ddl_Vendor.SelectedValue + "&PoStatus1=" + DropDownList1.SelectedItem.ToString());  
        IFRAME1.Attributes.Add("src", "PortCallReport2.aspx?Month=" + ddl_ToMonth.SelectedValue + "&Year=" + ddl_Year.SelectedValue + "&Vesselid=" + ddl_Vessel.SelectedValue + "&PoStatus=" + DropDownList1.SelectedValue + "&Vendor=" + ddl_Vendor.SelectedValue + "&Month1=" + ddl_ToMonth.SelectedItem.ToString() + "&Year1=" + ddl_Year.SelectedItem.ToString() + "&Vessel1=" + ddl_Vessel.SelectedItem.ToString() + "&PoStatus1=" + DropDownList1.SelectedItem.ToString() + "&Vendor1=" + ddl_Vendor.SelectedItem.ToString());  
    }
}
