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

public partial class SundayNoonReport : System.Web.UI.UserControl
{
    Authority Auth;
    string Mode;
    int Vessel;
    int LoginId;
   
    protected void Page_Load(object sender, EventArgs e)
    {
        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 42);
        if (chpageauth <= 0)
        {
            Response.Redirect("../AuthorityError.aspx");
        }
        //**********
        Vessel = Convert.ToInt32(Session["VesselID"].ToString());
        LoginId = Convert.ToInt32(Session["loginid"].ToString());
        try
        {
            Mode = Session["VMode"].ToString();
        }
        catch { Mode = "New"; }
        ProcessCheckAuthority OBJ = new ProcessCheckAuthority(LoginId, 4);
        OBJ.Invoke();
        Session["Authority"] = OBJ.Authority;
        Auth = OBJ.Authority;
        
        gv_Arrival.Columns[1].Visible = (Mode == "New") || (Mode == "Edit");
        gv_Arrival.Columns[2].Visible = ((Mode == "New") || (Mode == "Edit")) && Auth.isDelete;
        
        lbl_Msg.Text = "";
        lbl_Msg_Grid.Text = "";
        if (Page.IsPostBack == false)
        {
            Bind_grid();

            ddport.DataSource = cls_SearchReliever.getMasterData("Port", "PortId", "PortName");
            ddport.DataTextField = "PortName";
            ddport.DataValueField = "PortId";
            ddport.DataBind();
            ddport.Items.Insert(0, new ListItem("< Select >", "0"));

            Hide_Panel();
            SetButtonStatus(0);
        }
    }
    private void Bind_grid()
    {
        DataSet ds=SundayNoon.getData(Vessel, -1);
        gv_Arrival.DataSource = ds;
        gv_Arrival.DataBind(); 
    }
   
    protected void gv_Arrival_SelectedIndexChanged(object sender, EventArgs e)
    {
        int Id;
        HiddenField hfd;
        hfd = (HiddenField)gv_Arrival.Rows[gv_Arrival.SelectedIndex].FindControl("hfd_SundayNoonId");
        Id=Convert.ToInt32(hfd.Value);
        Show_Panel();
        Set_Panel_Data(Id);
        SetButtonStatus(4);
    }
    protected void gv_Arrival_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int Id;
        HiddenField hfd;
        hfd = (HiddenField)gv_Arrival.Rows[e.RowIndex].FindControl("hfd_SundayNoonId");
        Id = Convert.ToInt32(hfd.Value);
        try
        {
            SundayNoon.deleteSundayNoonById(Id);
            Bind_grid();
            lbl_Msg.Text = "Record Deleted Successfully";
        }
        catch
        {
            lbl_Msg.Text = "Error in deleting";
            return;
        }
        Hide_Panel();
        SetButtonStatus(0);
    }
    protected void gv_Arrival_RowEditing(object sender, GridViewEditEventArgs e)
    {
        int Id;
        HiddenField hfd;
        hfd = (HiddenField)gv_Arrival.Rows[e.NewEditIndex].FindControl("hfd_SundayNoonId");
        Id=Convert.ToInt32(hfd.Value);
        Show_Panel();
        Set_Panel_Data(Id);
        SetButtonStatus(5);
    }
    protected void gv_PreRender(object sender, EventArgs e)
    {
        if (gv_Arrival.Rows.Count > 0)
        {
            lbl_Msg_Grid.Text = ""; 
        }
        else
        {
            lbl_Msg_Grid.Text = "No Records Found.";
        }
         
    }
    
    protected void btn_Add_Click(object sender, EventArgs e)
    {
        Show_Panel();
        SetButtonStatus(1);
    }
    public void Refresh()
    {
        Bind_grid();
        Hide_Panel();
        SetButtonStatus(0);
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        int Id;
        int ETAPORT;
        string Lat, Long, Course, Present_Speed, Dist_sailed, AvgSpeed, KTS, DistToGo, NM, MEFOCons;
        string AVGMEFOCons, AUXMDOCONS, AVGAUXCONS_MTDAY, ROBIFO, ROBDO, ROBFW, STOPPAGES;
        string STOPPAGEREASON, Weatherdelaystovsl, Wind, Direction, Force, Seas;
        DateTime OnDate=DateTime.Today ;
       
        if (hfd_SundayNoonId.Value.Trim()=="")
        {
               Id=-1;
        }
        else
        {
               Id=Convert.ToInt32(hfd_SundayNoonId.Value.Trim());
        }
        Lat=txt_Lat.Text;
        Long=txt_Long.Text;
        Course=txt_Course.Text;
        Present_Speed =txt_P_Speed.Text;
        Dist_sailed = txt_Distance_sailed.Text;
        AvgSpeed = txt_avg_speed.Text;
        KTS = txt_kts.Text;
        DistToGo = txt_distToGo.Text;
        NM = txt_NM.Text;
        MEFOCons = txt_MEFOCons.Text;
        AVGMEFOCons = txt_AVGMEFOCONS.Text;
        AUXMDOCONS = txt_auxMDOCONS.Text;
        AVGAUXCONS_MTDAY = txt_AVGAUXCONSMT.Text;
        ROBIFO = txt_ROBIFO.Text;
        ROBDO = txt_ROBDO.Text;
        ROBFW = txt_ROBFW.Text;
        ETAPORT = Convert.ToInt32(ddport.SelectedValue);
        STOPPAGES = txt_Stoppages.Text;
        STOPPAGEREASON = txt_StoppageReason.Text;
        Weatherdelaystovsl = txt_WeathyerDelays.Text;
        Wind = txt_wind.Text;
        Direction = txt_Direction.Text;
        Force = txt_Force.Text;
        Seas = txt_seas.Text;

        try
        {
            OnDate = DateTime.Parse(txt_Arrival.Text.Trim() + " " + txt_ArrivalHour.Text + ":" + txt_ArrivalMinuts.Text);
        }
        catch
        { 
            lbl_Msg.Text = "Please Fill a valid Date Time.";
            return;
        }
       
        try
        {
            SundayNoon.Insert_Data(Id, Vessel, OnDate, Lat, Long, Course, Present_Speed,Dist_sailed, AvgSpeed, KTS, DistToGo, NM, MEFOCons,AVGMEFOCons, AUXMDOCONS, AVGAUXCONS_MTDAY, ROBIFO, ROBDO, ROBFW,ETAPORT, STOPPAGES,STOPPAGEREASON, Weatherdelaystovsl, Wind, Direction, Force, Seas, LoginId, LoginId);
            lbl_Msg.Text = "Record Successfully Saved.";
            Bind_grid();
        }
        catch
        {
            lbl_Msg.Text = "Record not Saved.";
        }
        ClearPanel();
        SetButtonStatus(2);
    }
    protected void btn_Cancel_Click(object sender, EventArgs e)
    {
        Hide_Panel();
        SetButtonStatus(3);
    }
    
    protected void Show_Panel()
    {
        ClearPanel();
        pnl_Arrival.Visible = true;
    }
    protected void Hide_Panel()
    {
        ClearPanel();
        pnl_Arrival.Visible =  false;
    }
    protected void Set_Panel_Data( int Id)
    {
        DataSet ds = SundayNoon.getData(0, Id);
        gv_Arrival.DataSource = ds;
        if (ds.Tables[0].Rows.Count > 0)
        {
            hfd_SundayNoonId.Value = ds.Tables[0].Rows[0]["SundayNoonReportId"].ToString();
            txt_Arrival.Text=ds.Tables[0].Rows[0]["ArrivalDate"].ToString();
            txt_ArrivalHour.Text = ds.Tables[0].Rows[0]["ArrivalHour"].ToString();
            txt_ArrivalMinuts.Text = ds.Tables[0].Rows[0]["Arrivalmin"].ToString();
            txt_Lat.Text = ds.Tables[0].Rows[0]["Lat"].ToString();
            txt_Long.Text = ds.Tables[0].Rows[0]["Long"].ToString();
            txt_Course.Text = ds.Tables[0].Rows[0]["Course"].ToString();
            txt_P_Speed.Text = ds.Tables[0].Rows[0]["Present_Speed"].ToString();
            txt_Distance_sailed.Text = ds.Tables[0].Rows[0]["DSFLP"].ToString();
            txt_avg_speed.Text= ds.Tables[0].Rows[0]["AVG_SPEED"].ToString();
            txt_kts.Text= ds.Tables[0].Rows[0]["KTS"].ToString();
            txt_distToGo.Text= ds.Tables[0].Rows[0]["DIST_TO_GO"].ToString();
            txt_NM.Text= ds.Tables[0].Rows[0]["NM"].ToString();
            txt_MEFOCons.Text= ds.Tables[0].Rows[0]["ME_FO_CONS"].ToString();
            txt_AVGMEFOCONS.Text= ds.Tables[0].Rows[0]["AVG_ME_FO_CONS"].ToString();
            txt_auxMDOCONS.Text= ds.Tables[0].Rows[0]["AUX_MDO_CONS"].ToString();
            txt_AVGAUXCONSMT.Text= ds.Tables[0].Rows[0]["AVG_AUX_CONS"].ToString();
            txt_ROBIFO.Text= ds.Tables[0].Rows[0]["ROB_IFO"].ToString();
            txt_ROBDO.Text= ds.Tables[0].Rows[0]["ROB_DO"].ToString();
            txt_ROBFW.Text= ds.Tables[0].Rows[0]["ROB_FW"].ToString();
            ddport.SelectedIndex=Convert.ToInt32(ds.Tables[0].Rows[0]["ETA_PORT"].ToString());
            txt_Stoppages.Text= ds.Tables[0].Rows[0]["STOP_PAGES"].ToString();
            txt_StoppageReason.Text= ds.Tables[0].Rows[0]["STOP_PAGE_REASON"].ToString();
            txt_WeathyerDelays.Text= ds.Tables[0].Rows[0]["WHETHER_DELAY_TO_VSL"].ToString();
            txt_wind.Text= ds.Tables[0].Rows[0]["Wind"].ToString();
            txt_Direction.Text= ds.Tables[0].Rows[0]["DIRECTION"].ToString();
            txt_Force.Text= ds.Tables[0].Rows[0]["FORCE"].ToString();
            txt_seas.Text= ds.Tables[0].Rows[0]["SEAS"].ToString();
        }
    } 
    protected void ClearPanel()
    {
        hfd_SundayNoonId.Value = "";
        txt_Arrival.Text = "";
        txt_ArrivalHour.Text = "00";
        txt_ArrivalMinuts.Text = "00";
        txt_Lat.Text = "";
        txt_Long.Text = "";
        txt_Course.Text = "";
        txt_P_Speed.Text = "";
        txt_Distance_sailed.Text = "";
        txt_avg_speed.Text = "";
        txt_kts.Text = "";
        txt_distToGo.Text = "";
        txt_NM.Text = "";
        txt_MEFOCons.Text = "";
        txt_AVGMEFOCONS.Text = "";
        txt_auxMDOCONS.Text = "";
        txt_AVGAUXCONSMT.Text = "";
        txt_ROBIFO.Text = "";
        txt_ROBDO.Text = "";
        txt_ROBFW.Text = "";
        ddport.SelectedIndex = 0;
        txt_Stoppages.Text = "";
        txt_StoppageReason.Text = "";
        txt_WeathyerDelays.Text = "";
        txt_wind.Text = "";
        txt_Direction.Text = "";
        txt_Force.Text = "";
        txt_seas.Text = "";
    }
    protected void SetButtonStatus(int Status)
    {
        if (Status == 0) // when page load
        {
            SetButton(true, false, false);
        }
        else if (Status == 1) // the add button is clicked
        {
            SetButton(false, true, true);
        }
        else if (Status == 2) // the save button is clicked
        {
            SetButton(false,true, true);
        }
        else if (Status == 3) // the cancel button is clicked
        {
             SetButton(true,false,false);
        }
        else if (Status == 4) // the grid view button is clicked
        {
            SetButton(true, false, true);
        }
        else if (Status == 5) // the grid edit button is clicked
        {
            SetButton(true, true, true);
        }
    }
    public void SetButton(bool Add, bool Save, bool Cancel)
    {
        
        if (Add)
            btn_Add.Visible = (Mode=="New") || (Mode=="Edit");
        else
            btn_Add.Visible = false;
        
        if (Save)
            btn_Save.Visible = (Mode == "New") || (Mode == "Edit");
        else
            btn_Save.Visible = false;
        
        btn_Cancel.Visible = Cancel;
    }
}