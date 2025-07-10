using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VIMS_StoreSpareMenu : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        btn_SpareMgmt.CssClass = "newbtn";
        btn_Store.CssClass = "newbtn";
        if (!IsPostBack)
        {
            switch (Common.CastAsInt32(Session["CP"]))
            {
                case 0:
                    btn_SpareMgmt.CssClass = "newbtnsel";
                    break;
                case 1:
                    btn_Store.CssClass = "newbtnsel";                     
                    break;
                case 2:
                    btn_UncategorisedStoreItem.CssClass = "newbtnsel";
                    break;
            }
        }
    }
    protected void Menu_Click(object sender, EventArgs e)
    {
        int cArg = Common.CastAsInt32(((Button)sender).CommandArgument);
        Session["CP"] = cArg;
        switch (cArg)
        {
            case 0:
                Response.Redirect("~/SpareManagement.aspx");
                break;
            case 1:
                Response.Redirect("~/StoreManagement.aspx");
                break;
            case 2:
                Response.Redirect("~/StoreManagementUncategorised.aspx");
                break;

        }

    }
}