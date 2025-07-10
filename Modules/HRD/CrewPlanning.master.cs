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

public partial class Crew_Planning : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ////-----------------------------
        //SessionManager.SessionCheck_New();
        ////-----------------------------
        //Alerts.SetHelp(imgHelp);   
        //if (Page.IsPostBack)
        //{
        //    Session["lasttab"] = "0";
        //}
        //b1.CssClass = "btn1";
        //b2.CssClass = "btn1";
        //b3.CssClass = "btn1";
        //b4.CssClass = "btn1";
        //b5.CssClass = "btn1";
        //if (Session["lasttab"] == null)
        //{
        //    Session["lasttab"] = 0;
        //}
        //switch (Common.CastAsInt32(Session["lasttab"]))
        //{
        //    case 0:
        //        b1.CssClass = "selbtn";
        //        break;
        //    case 1:
        //        b2.CssClass = "selbtn";
        //        break;
        //    case 2:
        //        b3.CssClass = "selbtn";
        //        break;
        //    case 3:
        //        b4.CssClass = "selbtn";
        //        break;
        //    case 4:
        //        b5.CssClass = "selbtn";
        //        break;
        //    default:
        //        break;
        //}
        // ((Button)this.FindControl("b" + (Common.CastAsInt32(Session["lasttab"]) + 1).ToString())).CssClass = "activetab";
    }
   
    protected void SubMenu1_MenuItemClick(object sender, MenuEventArgs e)
    {
        //int i;
        //for (i = 0; i < MainMenu.Items.Count; i++)
        //{
        //    this.MainMenu.Items[i].ImageUrl = this.MainMenu.Items[i].ImageUrl.Replace("_a.gif", "_d.gif");
        //}
        //this.MainMenu.Items[Int32.Parse(e.Item.Value)].ImageUrl = this.MainMenu.Items[Int32.Parse(e.Item.Value)].ImageUrl.Replace("_d.gif", "_a.gif");
        //MultiView1.ActiveViewIndex = Int32.Parse(e.Item.Value);
        //Session["lasttab"] = MultiView1.ActiveViewIndex;
       
    }
    //protected void SubMenu1_MenuItemClick(object sender, MenuEventArgs e)
    //{
    //    Session["lasttab"] = MultiView1.ActiveViewIndex;
    //}
}
  