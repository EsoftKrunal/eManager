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
         //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()),115);
        if (chpageauth <= 0)
        {
            Response.Redirect("DummyReport.aspx");

        }
        //*******************
        if (!(IsPostBack))
        {
            ddl_Vessel.DataSource = cls_SearchReliever.getMasterData("Vessel", "VesselId", "VesselName", Convert.ToInt32(Session["loginid"].ToString()));
            ddl_Vessel.DataTextField = "VesselName";
            ddl_Vessel.DataValueField = "VesselId";
            ddl_Vessel.DataBind();
            ddl_Vessel.Items.Insert(0, new ListItem("< All >", "0"));
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Session["ReportPortCall1_Header"] = " Monthly port call report for Dates From " + txt_from.Text + " - " + txt_to.Text + ", Vessel : " + ddl_Vessel.SelectedItem.ToString() + ", Status : " + DropDownList1.SelectedItem.ToString();
        IFRAME1.Attributes.Add("src", "PortCallReport1.aspx?CFrom=" + txt_from.Text + "&CTo=" + txt_to.Text  + "&Vesselid=" + ddl_Vessel.SelectedValue + "&CStatus=" + DropDownList1.SelectedValue);  
    }
}
