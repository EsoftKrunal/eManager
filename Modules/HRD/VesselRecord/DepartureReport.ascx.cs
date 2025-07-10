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

public partial class VesselRecord_DepartureReport : System.Web.UI.UserControl
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
        
        gv_Departure.Columns[1].Visible = (Mode == "New") || (Mode == "Edit");
        gv_Departure.Columns[2].Visible = ((Mode == "New") || (Mode == "Edit")) && Auth.isDelete;
        
        lbl_Msg.Text = "";
        lbl_Msg_Grid.Text = "";
        if (Page.IsPostBack == false)
        {
            Bind_grid();
            ddl_Port.DataSource = cls_SearchReliever.getMasterData("Port", "PortId", "PortName");
            ddl_Port.DataTextField = "PortName";
            ddl_Port.DataValueField = "PortId";
            ddl_Port.DataBind();
            ddl_Port.Items.Insert(0, new ListItem("< Select >", "0"));
            Hide_Panel();
            SetButtonStatus(0);
        }
    }
    
    private void Bind_grid()
    {
        DataSet ds = DepartureReport.getData(Vessel, -1);
        gv_Departure.DataSource = ds;
        gv_Departure.DataBind();
    }
    public void Refresh()
    {
        Bind_grid();
        Hide_Panel();
        SetButtonStatus(0);
    }
    
    protected void gv_Departure_SelectedIndexChanged(object sender, EventArgs e)
    {
        int Id;
        HiddenField hfd;
        hfd = (HiddenField)gv_Departure.Rows[gv_Departure.SelectedIndex].FindControl("hfd_DepartureId");
        Id = Convert.ToInt32(hfd.Value);
        Show_Panel();
        Set_Panel_Data(Id);
        SetButtonStatus(4);
    }
    protected void gv_Departure_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int Id;
        HiddenField hfd;
        hfd = (HiddenField)gv_Departure.Rows[e.RowIndex].FindControl("hfd_DepartureId");
        Id = Convert.ToInt32(hfd.Value);
        try
        {
            DepartureReport.deletedepartureById(Id);
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
    protected void gv_Departure_RowEditing(object sender, GridViewEditEventArgs e)
    {
        int Id;
        HiddenField hfd;
        hfd = (HiddenField)gv_Departure.Rows[e.NewEditIndex].FindControl("hfd_DepartureId");
        Id = Convert.ToInt32(hfd.Value);
        Show_Panel();
        Set_Panel_Data(Id);
        SetButtonStatus(5);
    }
    protected void gv_PreRender(object sender, EventArgs e)
    {
        if (gv_Departure.Rows.Count > 0)
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
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        int Id;
        int PortId;
        
        string Lat,Long,Draft_fwd,Draft_Aft,Recd_Fuel,Recd_Ifo,
               Recd_Mdo,Recd_Fwd,Rob_Fuel,Rob_Ifo,Rob_Mdo,Rob_Fwd,
               Eta_Next_Port,Distance_Next_Port,Cargo_Activity,Cargo_Name,
               Ships_Qty,Shore_Qty,Stowage,Tank_Capacity_Each,Time_Log;
        DateTime OnDate=DateTime.Today ;
        
        if (hfd_DepartureId.Value.Trim()=="")
        {
               Id=-1;
        }
        else
        {
               Id=Convert.ToInt32(hfd_DepartureId.Value.Trim());
        }

        PortId = Convert.ToInt32(ddl_Port.SelectedValue);
        Lat=txt_Lat.Text;
        Long=txt_Long.Text;
        Draft_fwd = txt_DraftFWD.Text;
        Draft_Aft = txt_DraftAFT.Text;
        Recd_Fuel = txt_RecdFuel.Text;
        Recd_Ifo = txt_RecdIFO.Text;
        Recd_Mdo = txt_RecdMDO.Text;
        Recd_Fwd = txt_RecdFW.Text;
        Rob_Fuel = txt_ROBFuel.Text;
        Rob_Ifo = txt_RobIFO.Text;
        Rob_Mdo = txt_RobMDO.Text;
        Rob_Fwd = txt_RobFWD.Text;
        Eta_Next_Port = txt_ETANextPort.Text;
        Distance_Next_Port = txt_DistanceNextPort.Text;
        Cargo_Activity = txt_CargoActivity.Text;
        Cargo_Name = txt_CargoName.Text;
        Ships_Qty = txt_ShipsQty.Text;
        Shore_Qty = txt_ShoreQty.Text;
        Stowage = txt_Stowage.Text;
        Tank_Capacity_Each = txt_TankCapacityEach.Text;
        Time_Log = txt_TimeLog.Text;

        try
        {
            OnDate = DateTime.Parse(txt_ArrivalDate.Text.Trim() + " " + txt_ArrivalHour.Text + ":" + txt_ArrivalMinuts.Text);
        }
        catch
        { 
            lbl_Msg.Text = "Please Fill a Valid Date Time.";
            return;
        }
        try
        {
            DepartureReport.Insert_Data(Id, Vessel, PortId, OnDate, Lat, Long, Draft_fwd, Draft_Aft,
                                        Recd_Fuel, Recd_Ifo, Recd_Mdo, Recd_Fwd, Rob_Fuel, Rob_Ifo, Rob_Mdo, Rob_Fwd,
                                        Eta_Next_Port, Distance_Next_Port, Cargo_Activity, Cargo_Name, Ships_Qty,
                                        Shore_Qty, Stowage, Tank_Capacity_Each, Time_Log, LoginId, LoginId);
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
        pnl_Departure.Visible = true;
    }
    protected void Hide_Panel()
    {
        ClearPanel();
        pnl_Departure.Visible = false;
    }
    protected void Set_Panel_Data(int Id)
    {
        DataSet ds = DepartureReport.getData(0, Id);
        gv_Departure.DataSource = ds;
        if (ds.Tables[0].Rows.Count > 0)
        {
            hfd_DepartureId.Value = ds.Tables[0].Rows[0]["DepartureReportId"].ToString();
            ddl_Port.SelectedValue = ds.Tables[0].Rows[0]["PortId"].ToString();
            txt_ArrivalDate.Text = ds.Tables[0].Rows[0]["ArrivalDate"].ToString();
            txt_ArrivalHour.Text = ds.Tables[0].Rows[0]["ArrivalHour"].ToString();
            txt_ArrivalMinuts.Text = ds.Tables[0].Rows[0]["Arrivalmin"].ToString();
            txt_Lat.Text = ds.Tables[0].Rows[0]["Lat"].ToString();
            txt_Long.Text = ds.Tables[0].Rows[0]["Long"].ToString();
            txt_DraftFWD.Text = ds.Tables[0].Rows[0]["Draft_Fwd"].ToString();
            txt_DraftAFT.Text = ds.Tables[0].Rows[0]["Draft_Aft"].ToString();
            txt_RecdFuel.Text = ds.Tables[0].Rows[0]["Recd_Fuel"].ToString(); ;
            txt_RecdIFO.Text = ds.Tables[0].Rows[0]["Recd_Ifo"].ToString();
            txt_RecdMDO.Text = ds.Tables[0].Rows[0]["Recd_Mdo"].ToString();
            txt_RecdFW.Text = ds.Tables[0].Rows[0]["Recd_Fw"].ToString();
            txt_ROBFuel.Text = ds.Tables[0].Rows[0]["Rob_Fuel"].ToString();
            txt_RobIFO.Text = ds.Tables[0].Rows[0]["Rob_Ifo"].ToString();
            txt_RobMDO.Text = ds.Tables[0].Rows[0]["Rob_Mdo"].ToString();
            txt_RobFWD.Text = ds.Tables[0].Rows[0]["Rob_Fw"].ToString();
            txt_ETANextPort.Text = ds.Tables[0].Rows[0]["Eta_Next_Port"].ToString();
            txt_DistanceNextPort.Text = ds.Tables[0].Rows[0]["Distance_Next_Port"].ToString();
            txt_CargoActivity.Text = ds.Tables[0].Rows[0]["Cargo_Activity"].ToString();
            txt_CargoName.Text = ds.Tables[0].Rows[0]["Name_of_Cargo"].ToString();
            txt_ShipsQty.Text = ds.Tables[0].Rows[0]["Ships_Qty"].ToString();
            txt_ShoreQty.Text = ds.Tables[0].Rows[0]["Shore_Qty"].ToString();
            txt_Stowage.Text = ds.Tables[0].Rows[0]["Stowage"].ToString();
            txt_TankCapacityEach.Text = ds.Tables[0].Rows[0]["Tank_Capacity_Each"].ToString();
            txt_TimeLog.Text = ds.Tables[0].Rows[0]["Time_Log"].ToString();
        }
    }
    protected void ClearPanel()
    {
        hfd_DepartureId.Value = "";
        ddl_Port.SelectedIndex = 0;
        txt_ArrivalDate.Text = "";
        txt_ArrivalHour.Text = "00";
        txt_ArrivalMinuts.Text = "00";
        txt_Lat.Text = "";
        txt_Long.Text = "";
        txt_DraftFWD.Text = "";
        txt_DraftAFT.Text = "";
        txt_RecdFuel.Text = "";
        txt_RecdIFO.Text = "";
        txt_RecdMDO.Text = "";
        txt_RecdFW.Text = "";
        txt_ROBFuel.Text = "";
        txt_RobIFO.Text = "";
        txt_RobMDO.Text = "";
        txt_RobFWD.Text = "";
        txt_ETANextPort.Text = "";
        txt_DistanceNextPort.Text = "";
        txt_CargoActivity.Text = "";
        txt_CargoName.Text = "";
        txt_ShipsQty.Text = "";
        txt_ShoreQty.Text = "";
        txt_Stowage.Text = "";
        txt_TankCapacityEach.Text = "";
        txt_TimeLog.Text="";
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
            SetButton(false, true, true);
        }
        else if (Status == 3) // the cancel button is clicked
        {
            SetButton(true, false, false);
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
            btn_Add.Visible = (Mode == "New") || (Mode == "Edit");
        else
            btn_Add.Visible = false;
        
        if (Save)
            btn_Save.Visible = (Mode == "New") || (Mode == "Edit");
        else
            btn_Save.Visible = false;
        
        btn_Cancel.Visible = Cancel;
    }
}
