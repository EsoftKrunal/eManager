using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class OfficeRunningHourPrint : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {

    }


    protected void btnShow_OnClick(object sender, EventArgs e)
    {
        
        //iframReport.Attributes.Add("src", "Reports/PrintCrystal.aspx?ReportType=RuningHour");
        iframReport.Attributes.Add("src", "Reports/PrintCrystal.aspx?ReportType=RuningHour&FD=" + txtFrom.Text.Trim() + "&TD=" + txtTo.Text.Trim() + "");




        //ScriptManager.RegisterStartupScript(Page, this.GetType(), "PP", "window.open('Reports/PrintCrystal.aspx?ReportType=RuningHour&From='"+txtFrom.Text.Trim()+"'&ToDate='"+txtTo.Text.Trim()+"'');", true);
    }
}
