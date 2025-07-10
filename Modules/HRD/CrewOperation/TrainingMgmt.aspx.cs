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
using System.IO;
using System.Text;

public partial class TrainingMgmt : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        if (!Page.IsPostBack)
        {
            string mode = "" + Request.QueryString["Mode"];
            if (mode == "OnBoard" || mode == "")
                frm.Attributes.Add("src", "MonitorTraining.aspx");
            else
                frm.Attributes.Add("src", "MonitorTraining_Shore.aspx");

        }
    }
}
