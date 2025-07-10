using System;
using System.Data;
using System.Configuration;
using System.Reflection;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using ShipSoft.CrewManager.BusinessLogicLayer;
using ShipSoft.CrewManager.BusinessObjects;
using ShipSoft.CrewManager.Operational;
public partial class AddVesselDetails : System.Web.UI.Page
{
    Authority Auth;
    string Mode;
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        Alerts.SetHelp(imgHelp);  
        // CODE FOR UDATING THE AUTHORITY
        ProcessCheckAuthority Obj = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 1);
        Obj.Invoke();
        Session["Authority"] = Obj.Authority;
        Auth =(Authority) Session["Authority"];
        //--
        try
        {
            Mode = Request.QueryString["Mode"].ToString();
            
        }
        catch
        {
            try
            {
                Mode = Session["VMode"].ToString();
                HiddenPK.Value = Session["CrewId"].ToString();
            }
            catch
            {
                Mode = "New";
                HiddenPK.Value = ""; 
            }
        }
        
        lblMessage.Text = "";
        try
        {
            HiddenPK.Value = Session["VesselId"].ToString() ;
            //VesselMiningScale1.Vesselid = int.Parse(Session["VesselId"].ToString());
            AddVesselMiningScale1.Vesselid = int.Parse(Session["VesselId"].ToString());  
        }
        catch
        { HiddenPK.Value = ""; }

        if (HiddenPK.Value.Trim() == "")
        {
            VesselDocuments1.VesselId = Convert.ToInt32(-1);
            VesselDetailsOther1.VesselId  = Convert.ToInt32(-1);
            VesselDetailsGeneral1.VesselId = Convert.ToInt32(-1);
            VesselBudget1.Vesselid = Convert.ToInt32(-1); ;
            VesselDetailsGeneral1.ErrorLabel = lblMessage;  
        }
        else
        {
            VesselDocuments1.VesselId = Convert.ToInt32(HiddenPK.Value);
            VesselDetailsOther1.VesselId = Convert.ToInt32(HiddenPK.Value);
            VesselDetailsGeneral1.VesselId = Convert.ToInt32(HiddenPK.Value);
            VesselBudget1.Vesselid = Convert.ToInt32(HiddenPK.Value);
            VesselDetailsGeneral1.ErrorLabel = lblMessage;
        }
        try
        {
            if (Session["UserName"] == "")
            {
                Response.Redirect("Login.aspx");
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
            return;
        }
        if (!(IsPostBack))
        {
            if (Session["LastVIndex"]==null) 
                Session["LastVIndex"]="0";

            HANDLE_AUTHORITY(0);
            if (Session["VMode"].ToString() == "View")
            {
                Menu1_MenuItemClick(sender, new MenuEventArgs(Menu1.Items[4]));
            }
            else
            {
                int index = int.Parse(Session["LastVIndex"].ToString());
                Menu1_MenuItemClick(sender, new MenuEventArgs(Menu1.Items[index]));
            }
        }
        
    }
    protected void Menu1_MenuItemClick(object sender, MenuEventArgs e)
    {
        Session["LastVIndex"] = e.Item.Value;  
        int i = 0;
        if (HiddenPK.Value.Trim() == "")
        {
            this.MultiView1.ActiveViewIndex = 0;
            lblMessage.Text = "Please Enter Vessel Details First.";
           
        }
        else
        {
            this.MultiView1.ActiveViewIndex = Int32.Parse("5");
            //***********Code to check page acessing Permission
            if (e.Item.Value == "0")
            {
               
                int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 36);
                if (chpageauth <= 0)
                {
                    Response.Redirect("../AuthorityError.aspx");

                }
              
            }
            else if (e.Item.Value == "1")
            {
                int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 37);
                if (chpageauth <= 0)
                {
                    Response.Redirect("../AuthorityError.aspx");

                }
            }
            else if (e.Item.Value == "2")
            {
                int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 38);
                if (chpageauth <= 0)
                {
                    Response.Redirect("../AuthorityError.aspx");

                }
            }
            else if (e.Item.Value == "3")
            {
                int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 39);
                if (chpageauth <= 0)
                {
                    Response.Redirect("../AuthorityError.aspx");

                }
            }
            else if (e.Item.Value == "4")
            {
                int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 41);
                if (chpageauth <= 0)
                {
                    Response.Redirect("../AuthorityError.aspx");

                }
            }
            else if (e.Item.Value == "5")
            {
                int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 42);
                if (chpageauth <= 0)
                {
                    Response.Redirect("../AuthorityError.aspx");

                }
            }
            else if (e.Item.Value == "6")
            {
                int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 42);
                if (chpageauth <= 0)
                {
                    Response.Redirect("../AuthorityError.aspx");

                }
            }
            //*******************
            for (; i < Menu1.Items.Count; i++)
            {
                this.Menu1.Items[i].ImageUrl = this.Menu1.Items[i].ImageUrl.Replace("_a.gif", "_d.gif");
            }
            this.Menu1.Items[Int32.Parse(e.Item.Value)].ImageUrl = this.Menu1.Items[Int32.Parse(e.Item.Value)].ImageUrl.Replace("_d.gif", "_a.gif");
     
        }
    }
    private void HANDLE_AUTHORITY(int index)
    {
        if (Mode == "New")
        {
          
        }
        else if (Mode == "Edit")
        {
     
        }
        else // Mode=View
        {
        }
    }
    protected void imgbtn_Documents_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("VesselDocuments.aspx");  
    }
    protected void imgbtn_Search_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("VesselSearch.aspx");  
    }
}
