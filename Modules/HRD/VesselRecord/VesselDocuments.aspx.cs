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
public partial class VesselDocumentsPage : System.Web.UI.Page
{
    Authority Auth;
    string Mode;
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        // CODE FOR UDATING THE AUTHORITY
        ProcessCheckAuthority Obj = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 1);
        Obj.Invoke();
        Session["Authority"] = Obj.Authority;
        Auth =(Authority) Session["Authority"];
        //--
        try
        {
            Mode = Request.QueryString["VMode"].ToString();
            
        }
        catch
        {
            try
            {
                HiddenPK.Value = Session["CrewId"].ToString();
                Mode = Session["VMode"].ToString();
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
        }
        catch
        { HiddenPK.Value = ""; }

        if (HiddenPK.Value.Trim() == "")
        {
            Response.Redirect("VesselSearch.aspx");
            VesselMiningScale1.Vesselid = Convert.ToInt32(-1);
            CrewMatrixl1.Vesselid = Convert.ToInt32(-1);
            
        }
        else
        {
            VesselMiningScale1.Vesselid = Convert.ToInt32(HiddenPK.Value);
            CrewMatrixl1.Vesselid = Convert.ToInt32(HiddenPK.Value);
        }
        try
        {
            if (Session["UserName"] == "")
            {
                Response.Redirect("default.aspx");
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("default.aspx");
            return;
        }
        if (!(IsPostBack))
        {
            
            HANDLE_AUTHORITY(0);
              txt_arrival_VesselName.Text = Session["VesselName"].ToString();
              Bind_arrival_FlagNameDropDown();
            
            txt_arrival_FormerVesselName.Text = Session["FormerName"].ToString();
            ddl_arrival_flag.SelectedValue = Session["FlagStateId"].ToString();
             
        }

 
    }
    protected void Menu1_MenuItemClick(object sender, MenuEventArgs e)
    {
        int i = 0;
        if (HiddenPK.Value.Trim() == "")
        {
            Response.Redirect("vesselsearch.aspx"); 
            return;
        }
        this.MultiView1.ActiveViewIndex = Int32.Parse(e.Item.Value);
        if (e.Item.Value == "0")
        {
            //***********Code to check page acessing Permission
            int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 41);
            if (chpageauth <= 0)
            {
                Response.Redirect("../AuthorityError.aspx");

            }
            //*******************
        }
        else if (e.Item.Value == "1")
        {
            //***********Code to check page acessing Permission
            int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 42);
            if (chpageauth <= 0)
            {
                Response.Redirect("../AuthorityError.aspx");

            }
            //*******************
        }
        else if (e.Item.Value == "2")
        {
            //***********Code to check page acessing Permission
            int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 43);
            if (chpageauth <= 0)
            {
                Response.Redirect("../AuthorityError.aspx");

            }
            this.RadioButtonList1.SelectedIndex = 0;
            showview();
                       
            
          
            //*******************
        }
        else if (e.Item.Value == "3")
        {
            //***********Code to check page acessing Permission
            int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 43);
            if (chpageauth <= 0)
            {
                Response.Redirect("../AuthorityError.aspx");

            }
            this.RadioButtonList1.SelectedIndex = 0;
            showview();



            //*******************
        }
       
        for (; i < Menu1.Items.Count; i++)
        {
            this.Menu1.Items[i].ImageUrl = this.Menu1.Items[i].ImageUrl.Replace("_a.gif", "_d.gif");
        }
        this.Menu1.Items[Int32.Parse(e.Item.Value)].ImageUrl = this.Menu1.Items[Int32.Parse(e.Item.Value)].ImageUrl.Replace("_d.gif", "_a.gif");
        
    }
    private void Bind_arrival_FlagNameDropDown()
    {
        DataTable dt1 = VesselDetailsGeneral.selectDataFlag();
        this.ddl_arrival_flag.DataValueField = "FlagStateId";
        this.ddl_arrival_flag.DataTextField = "FlagStateName";
        this.ddl_arrival_flag.DataSource = dt1;
        this.ddl_arrival_flag.DataBind();
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
        Response.Redirect("VesselDetails.aspx");  
    }
    protected void imgbtn_Search_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("VesselSearch.aspx");  
    }
    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
       
        showview();
        
    }
    private void showview()
    {

        if (RadioButtonList1.SelectedIndex == 0)
        {
           multi1.SetActiveView(poition_view1);
           arrival_control.Refresh(); 
        }
        else if (RadioButtonList1.SelectedIndex == 1)
        {
            this.multi1.SetActiveView(position_view2);
            sundaynoon_control.Refresh();
        }
        else if (RadioButtonList1.SelectedIndex == 2)
        {
            this.multi1.SetActiveView(position_view3);
            dailynoon_control.Refresh(); 
        }
        else if (RadioButtonList1.SelectedIndex == 3)
        {
            this.multi1.SetActiveView(position_view4);
           departure_control.Refresh();
        }
    }
}
