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

public partial class Crew_Reports: System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        //Alerts.SetHelp(imgHelp);   
        if (!(IsPostBack))
        {
            try
            {
                //MultiView1.ActiveViewIndex = int.Parse(Convert.ToString(Session["RIndex"]));  
            }
            catch 
            {
                Session["RIndex"] = "0";
                //MultiView1.ActiveViewIndex = int.Parse(Session["RIndex"].ToString());
                  
            }
            //Menu1_MenuItemClick(null, new MenuEventArgs(Menu1.Items[MultiView1.ActiveViewIndex])); 
        }
       // singaporetable.Visible = (Alerts.getLocation() == "S");
        //yangoontable.Visible = !singaporetable.Visible;
    }

    //protected void Menu1_MenuItemClick(object sender, MenuEventArgs e)
    //{
    //    int i = 0;
    //    MultiView1.ActiveViewIndex = Convert.ToInt32(e.Item.Value) ;
    //    Session["RIndex"] = e.Item.Value;
    //    for (; i < Menu1.Items.Count; i++)
    //    {
    //        this.Menu1.Items[i].ImageUrl = this.Menu1.Items[i].ImageUrl.Replace("_a.gif", "_d.gif");
    //    }
    //    this.Menu1.Items[Int32.Parse(e.Item.Value)].ImageUrl = this.Menu1.Items[Int32.Parse(e.Item.Value)].ImageUrl.Replace("_d.gif", "_a.gif");
    
    //}
}
  