using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections.Generic;
using ShipSoft.CrewManager.BusinessLogicLayer;
using ShipSoft.CrewManager.BusinessObjects;

public partial class Voyage : System.Web.UI.Page
{
    Authority Auth;
    protected void Page_Load(object sender, EventArgs e)
    {

        //--------------------------------- PAGE ACCESS AUTHORITY ----------------------------------------------------
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 1089);
        if (chpageauth <= 0)
            Response.Redirect("blank.aspx");
        //------------------------------------------------------------------------------------------------------------
        //VesselPositionReporting mp = (VesselPositionReporting)this.Master;
        //mp.ShowMenu = false;
        //mp.ShowHeaderbar = false;
        if (Session["loginid"] != null)
        {
            ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 1089);
            OBJ.Invoke();
            Session["Authority"] = OBJ.Authority;
            Auth = OBJ.Authority;
        }
        if (!IsPostBack)
        {
            btnVoyage.CssClass = "selbtn";
        }
    }

    protected void btnVoyage_OnClick(object sender, EventArgs e)
    {
        btnVoyage.CssClass = "selbtn";
        btnPositionReport.CssClass = "btn1";
        ifram1.Attributes.Add("src", "BunkerReport.aspx");
    }
    protected void btnPositionReport_OnClick(object sender, EventArgs e)
    {
        btnVoyage.CssClass = "btn1";
        btnPositionReport.CssClass = "selbtn";
        ifram1.Attributes.Add("src", "PositionReport.aspx");

    }


}
