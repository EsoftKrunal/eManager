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

public partial class PortCallReport3Header : System.Web.UI.Page
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
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 117);
        if (chpageauth <= 0)
        {
            Response.Redirect("DummyReport.aspx");

        }
        //*******************
        if (!(IsPostBack))
        {
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
            ddl_ToMonth.SelectedIndex = DateTime.Today.Month;
            ddl_Year.SelectedValue = DateTime.Today.Year.ToString() ;
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        //if (DropDownList1.SelectedIndex == 0)
        //{
        //    Session["ReportPortCall1_Header"] = " Monthly port call's PO & Invoice details for the month of " + ddl_ToMonth.SelectedItem.ToString() + " - " + ddl_Year.SelectedItem.ToString() + ", Vessel : " + ddl_Vessel.SelectedItem.ToString() + "PoStatus : Open";
        //}
        //else
        //{
        //    Session["ReportPortCall1_Header"] = " Monthly port call's PO & Invoice details for the month of " + ddl_ToMonth.SelectedItem.ToString() + " - " + ddl_Year.SelectedItem.ToString() + ", Vessel : " + ddl_Vessel.SelectedItem.ToString() + "PoStatus : Closed";
        //}
        //IFRAME1.Attributes.Add("src", "PortCallReport3.aspx?Month=" + ddl_ToMonth.SelectedValue + "&Year=" + ddl_Year.SelectedValue + "&Vesselid=" + ddl_Vessel.SelectedValue + "&PoStatus=" + DropDownList1.SelectedValue);  
        IFRAME1.Attributes.Add("src", "PortCallReport3.aspx?Month=" + ddl_ToMonth.SelectedValue + "&Year=" + ddl_Year.SelectedValue + "&Vesselid=" + ddl_Vessel.SelectedValue + "&PoStatus=" + DropDownList1.SelectedValue + "&Month2=" + ddl_ToMonth.SelectedItem.ToString() + "&Year2=" + ddl_Year.SelectedItem.ToString() + "&Vessel2=" + ddl_Vessel.SelectedItem.ToString() + "&PoStatus2=" + DropDownList1.SelectedItem.ToString());  
    }
}
