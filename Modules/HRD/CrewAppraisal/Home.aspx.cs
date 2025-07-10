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

public partial class CrewAppraisal_Home : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnVessel_Click(object sender, EventArgs e)
    {
        btnOffice.CssClass = "btn";
        btnVessel.CssClass = "btnsel";
        frame1.Attributes.Add("src","CrewAppraisal.aspx");
    }
    protected void btnOffice_Click(object sender, EventArgs e)
    {
        btnOffice.CssClass = "btnsel";
        btnVessel.CssClass = "btn";
        frame1.Attributes.Add("src", "OfficeCrewAppraisal.aspx");
    }    
}
