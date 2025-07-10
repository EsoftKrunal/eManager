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

public partial class Reports_InspSearch_Report : System.Web.UI.Page
{
    string strStatus = "";
    string strFrDt = "";
    string strTDt = "";
    string strinspid = "";
    int intownerid, intvesselid, intloginid, intport, intcrewid;
    string strduedt = "", strinspname="", strchap="", strinspnum="";
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        this.lblmessage.Text = "";
        //this.ddl_Status.Attributes.Add("onkeydown", "javascript:if(event.keyCode==13){document.getElementById('" + btn_ShowReport.ClientID + "').focus();}");
        //this.txt_FromDt.Attributes.Add("onkeydown", "javascript:if(event.keyCode==13){document.getElementById('" + btn_ShowReport.ClientID + "').focus();}");
        //this.txt_ToDt.Attributes.Add("onkeydown", "javascript:if(event.keyCode==13){document.getElementById('" + btn_ShowReport.ClientID + "').focus();}");
        //if (!Page.IsPostBack)
        //{
        //    txt_FromDt.Text = DateTime.Now.ToString("dd-MMM-yyyy");
        //}
        strStatus = Page.Request.QueryString["STATUS"].ToString();
        strFrDt = Page.Request.QueryString["FRMDT"].ToString();
        strTDt = Page.Request.QueryString["TDT"].ToString();
        strinspid = Page.Request.QueryString["INPID"].ToString();
        intownerid = Convert.ToInt32(Page.Request.QueryString["OWNID"].ToString());
        intvesselid = Convert.ToInt32(Page.Request.QueryString["VSLID"].ToString());
        strduedt = Page.Request.QueryString["DUEDT"].ToString();

        if (Page.Request.QueryString["LOGID"].ToString() == "")
            intloginid = 0;
        else
            intloginid = Convert.ToInt32(Page.Request.QueryString["LOGID"].ToString());
        if (Page.Request.QueryString["PORT"].ToString()=="")
            intport = 0;
        else
            intport = Convert.ToInt32(Page.Request.QueryString["PORT"].ToString());
        strinspname = Page.Request.QueryString["IPNAME"].ToString();
        strchap = Page.Request.QueryString["CHAP"].ToString();
        strinspnum = Page.Request.QueryString["IPNO"].ToString();
        if (Page.Request.QueryString["CRNO"].ToString() == "")
            intcrewid = 0;
        else
            intcrewid = Convert.ToInt32(Page.Request.QueryString["CRNO"].ToString());
        if (!Page.IsPostBack)
        {
            btn_ShowReport_Click(sender, e);
        }
    }
    //protected void ddl_Status_SelectedIndexChanged(object sender, EventArgs e)
    //{
        
    //}
    protected void btn_ShowReport_Click(object sender, EventArgs e)
    {
        //if (ddl_Status.SelectedValue == "Due")
        //{
        //    if (txt_FromDt.Text == "")
        //    {
        //        lblmessage.Text = "Please enter From Date.";
        //        return;
        //    }
        //    if (txt_ToDt.Text == "")
        //    {
        //        lblmessage.Text = "Please enter To Date.";
        //        return;
        //    }
        //}
        //IFRAME1.Attributes.Add("src", "InspSearch_Crystal.aspx?SID=" + ddl_Status.SelectedValue + "&FROMDT=" + txt_FromDt.Text + "&TODT=" + txt_ToDt.Text);
        IFRAME1.Attributes.Add("src", "InspSearch_Crystal.aspx?SID=" + strStatus + "&FROMDT=" + strFrDt + "&TODT=" + strTDt + "&INID=" + strinspid + "&OWNID=" + intownerid + "&VSLID=" + intvesselid + "&DUEDT=" + strduedt + "&LGNID=" + intloginid + "&PRTNAME=" + intport + "&INSPNAME=" + strinspname + "&CHAPNAME=" + strchap + "&INPNUM=" + strinspnum + "&CREWID=" + intcrewid);
    }
}
