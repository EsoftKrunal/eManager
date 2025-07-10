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
using System.Data.SqlClient;
using System.Xml;
using ShipSoft.CrewManager.BusinessLogicLayer;
using ShipSoft.CrewManager.BusinessObjects;
using ShipSoft.CrewManager.Operational;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

public partial class PEAP : System.Web.UI.Page
{
    public Authority Auth;
    //public int SCMID
    //{
    //    set { ViewState["SCMID"] = value; }
    //    get { return int.Parse("0" + ViewState["SCMID"]); }
    //}
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
       
    }
    protected void ddlPeepType_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        tblManagement.Visible = false;
        tblSupport.Visible = false;
        tblOperation.Visible = false;

        if (ddlPeepType.SelectedIndex == 1)
            tblManagement.Visible = true;
        if (ddlPeepType.SelectedIndex == 2)
            tblSupport.Visible = true;
        if (ddlPeepType.SelectedIndex == 3)
            tblOperation.Visible = true;
    }

        
}

