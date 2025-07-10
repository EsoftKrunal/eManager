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

public partial class Modules_Purchase_Invoice_LubeConsumption : System.Web.UI.Page
{
    Authority Auth;
    protected void Page_Load(object sender, EventArgs e)
    {
        //--------------------------------- PAGE ACCESS AUTHORITY ----------------------------------------------------
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 1099);
        if (chpageauth <= 0)
            Response.Redirect("blank.aspx");
        //------------------------------------------------------------------------------------------------------------
        //VesselPositionReporting mp = (VesselPositionReporting)this.Master;
        //mp.ShowMenu = false;
        //mp.ShowHeaderbar = false;
        if (Session["loginid"] != null)
        {
            ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 1099);
            OBJ.Invoke();
            Session["Authority"] = OBJ.Authority;
            Auth = OBJ.Authority;
        }
        if (!IsPostBack)
        {
            btnLubConsumpSummary.CssClass = "selbtn";
        }
    }

    protected void btnLubConsumpSummary_OnClick(object sender, EventArgs e)
    {
        btnLubConsumpSummary.CssClass = "selbtn";
        btnLubConsumpDetails.CssClass = "btn1";
        ifram1.Attributes.Add("src", "LubeConsumptionSummary.aspx");
    }

    protected void btnLubConsumpDetails_OnClick(object sender, EventArgs e)
    {
        btnLubConsumpSummary.CssClass = "btn1";
        btnLubConsumpDetails.CssClass = "selbtn";
        ifram1.Attributes.Add("src", "LubeConsumptionDetails.aspx");
    }
}