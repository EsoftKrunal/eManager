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


public partial class VesselRecord_Arrival : System.Web.UI.UserControl
{
    int vesselid=0;
    Authority Auth;
    int id;
    string Mode = "New";
    int Loginid;
    protected void Page_Load(object sender, EventArgs e)
    {
        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 42);
        if (chpageauth <= 0)
        {
            Response.Redirect("../AuthorityError.aspx");
        }
        //**********
        vesselid = Convert.ToInt32(Session["VesselID"]);
        Loginid = Convert.ToInt32(Session["loginid"].ToString());
        try
        {
            Mode = Session["VMode"].ToString();
        }
        catch { Mode = "New"; }

        ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Loginid,4);
        OBJ.Invoke();
        Session["Authority"] = OBJ.Authority;      
         
        Auth = OBJ.Authority;
        gv_Arrival.Columns[1].Visible = (Mode == "New") || (Mode == "Edit");
        gv_Arrival.Columns[2].Visible = ((Mode == "New") || (Mode == "Edit")) && Auth.isDelete;
        lbl_Msg.Text = "";
        lbl_gv.Text = "";
        if (Page.IsPostBack == false)
        {
                 this.txt_ArrivalHour.Text = "00";
        this.txt_ArrivalMinuts.Text = "00";
            
            Bind_grid();
            pnl_Arrival.Visible = false;
            SetButtonStatus(0);
       }
    }

    public void getdata()
    {
       SetButtonStatus(0);
    }
    public void Refresh()
    {
        Bind_grid();
        pnl_Arrival.Visible = false;
        SetButtonStatus(0);
    }
    private void Bind_grid()
    {
        DataSet ds = cls_Arrival.getData(Convert.ToInt32(vesselid), -1);
        gv_Arrival.DataSource = ds;
        gv_Arrival.DataBind(); 
    }
    protected void gv_Arrival_SelectedIndexChanged(object sender, EventArgs e)
    {
       
        id = Convert.ToInt32(this.gv_Arrival.DataKeys[this.gv_Arrival.SelectedIndex].Value.ToString());
        pnl_Arrival.Visible = true;
        showrecord(id);
        SetButtonStatus(4);
    }
   
    private void showrecord(int arrivalid)
    {
        cleardata();
        DataSet ds = cls_Arrival.getData(-1, arrivalid);
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            hiddenfieldarrival.Value = arrivalid.ToString(); 
            this.txt_Arrival.Text = dr["ArrivalDate"].ToString(); ;
            this.txt_ArrivalHour.Text = dr["ArrivalHour"].ToString();
            this.txt_ArrivalMinuts.Text = dr["ArrivalMin"].ToString();
            this.txt_Lat.Text = dr["LAT"].ToString();
            this.txt_long.Text = dr["LONG"].ToString();
            this.txt_FWD.Text = dr["DRAFT_FWD"].ToString();
            this.txt_AFT.Text = dr["DRAFT_AFT"].ToString();
            this.txt_Ifo.Text = dr["ROB_IFO"].ToString();
            this.txt_mdo.Text = dr["ROB_MDO"].ToString();

            this.txt_mw.Text = dr["Rob_FW"].ToString();
            this.txt_odflp.Text = dr["ROB_ODLP"].ToString();
            this.txt_btb.Text = dr["ROB_BTB"].ToString();
            this.txt_faop_eosp.Text = dr["ROB_FAOP_EOSP"].ToString();
            this.txt_avg_speed.Text = dr["ROB_AVG_SPEED"].ToString();
            this.txt_KTS.Text = dr["KTS_SAILING_TIME"].ToString();
            txt_EOSPBerth.Text = dr["KTS_EOSP_BERTH"].ToString();
            this.txt_distbtb.Text = dr["DBTB"].ToString();
            this.txtmefocons.Text = dr["ME_FO_CONS"].ToString();
            this.txtavg.Text = dr["AVG"].ToString();
            this.txtauxmdocons.Text = dr["AUX_MDO_CONS"].ToString();
            this.txtavg1.Text = dr["AVG1"].ToString();
            this.txt_avg_revs.Text = dr["AVG_REVS"].ToString();
            this.txt_slip.Text = dr["SLIP"].ToString();
            this.txt_stopages.Text = dr["STOP_AGES"].ToString();
            this.txt_reason.Text = dr["REASON"].ToString();
            this.txt_anchoringreason.Text = dr["REASON_FOR_ANC"].ToString();

            
           
            
           
           
        }
    }
    protected void btn_Cancel_Click(object sender, EventArgs e)
    {
        if (Mode == "New")
        {
            btn_Add.Visible = (Auth.isAdd || Auth.isEdit);
        }
        else if (Mode == "Edit")
        {
            btn_Add.Visible = (Auth.isAdd || Auth.isEdit);
        }
        pnl_Arrival.Visible = false;
        btn_Save.Visible = false;
        btn_Cancel.Visible = false;

        btn_Add.Visible = ((Mode == "Edit") || (Mode == "New")) ? true : false;
        gv_Arrival.SelectedIndex = -1;
        hiddenfieldarrival.Value = "-1";
        
    }
    protected void btn_Add_Click(object sender, EventArgs e)
    {
        cleardata();
        gv_Arrival.SelectedIndex = -1;
         pnl_Arrival.Visible = true;
         SetButtonStatus(1);
        hiddenfieldarrival.Value = "-1";
        
    }
    private void cleardata()
    {
        hiddenfieldarrival.Value = "-1";
        this.txt_Arrival.Text = "" ;
        this.txt_ArrivalHour.Text = "00";
        this.txt_ArrivalMinuts.Text = "00";
        this.txt_Lat.Text = "";
        this.txt_long.Text = "";
        this.txt_FWD.Text = "";
        this.txt_AFT.Text = "";
        this.txt_Ifo.Text = "";
        this.txt_mdo.Text = "";

        this.txt_mw.Text = "";
        this.txt_odflp.Text = "";
        this.txt_btb.Text = "";
        this.txt_faop_eosp.Text = "";
        this.txt_avg_speed.Text = "";
        this.txt_KTS.Text = "";
        txt_EOSPBerth.Text = "";
        this.txt_distbtb.Text = "";
        this.txtmefocons.Text = "";
        this.txtavg.Text = "";
        this.txtauxmdocons.Text = "";
        this.txtavg1.Text = "";
        this.txt_avg_revs.Text = "";
        this.txt_slip.Text = "";
        this.txt_stopages.Text = "";
        this.txt_reason.Text = "";
        this.txt_anchoringreason.Text = "";

    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        int intArrivalId;
        DateTime OnDate = DateTime.Today;
       
        intArrivalId = Convert.ToInt32(this.hiddenfieldarrival.Value);
        vesselid = Convert.ToInt32(vesselid);
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
            cls_Arrival.insertUpdateArrivalDetails("insertupdate_Positionarrival",
                                                  intArrivalId,
                                                  vesselid,
                                                 OnDate,
                                                  this.txt_Lat.Text,
                                                  this.txt_long.Text,
                                                this.txt_FWD.Text,
                                                this.txt_AFT.Text,
                                                this.txt_Ifo.Text,
                                                this.txt_mdo.Text,
                                                this.txt_mw.Text,
                                                this.txt_odflp.Text,
                                                this.txt_btb.Text,
                                                this.txt_faop_eosp.Text,
                                                this.txt_avg_speed.Text,
                                                this.txt_KTS.Text,
                                                txt_EOSPBerth.Text,
                                                this.txt_distbtb.Text,
                                                this.txtmefocons.Text,
                                                this.txtavg.Text,
                                                this.txtauxmdocons.Text,
                                                this.txtavg1.Text,
                                                this.txt_avg_revs.Text,
                                                this.txt_slip.Text,
                                                this.txt_stopages.Text,
                                                this.txt_reason.Text,
                                                this.txt_anchoringreason.Text,
                                                Convert.ToInt32(Session["loginid"].ToString()));
            Bind_grid();
            lbl_Msg.Text = "Record Successfully Saved.";
        }
        catch 
        {
            lbl_Msg.Text = "Record not Saved.";
        }
        cleardata();
        SetButtonStatus(2);
    }
    protected void gv_Arrival_RowEditing(object sender, GridViewEditEventArgs e)
    {
        Mode = "Edit";
       
        id = Convert.ToInt32(gv_Arrival.DataKeys[e.NewEditIndex].Value.ToString());
         
        showrecord(id);
        this.hiddenfieldarrival.Value = id.ToString();
        gv_Arrival.SelectedIndex = e.NewEditIndex;
        pnl_Arrival.Visible = true;
        SetButtonStatus(5);
    }
    protected void gv_Arrival_PreRender(object sender, EventArgs e)
    {
        if (gv_Arrival.Rows.Count <= 0)
        {
            lbl_gv.Text = "No Record Found";
        }
        else
        {
            lbl_gv.Text = "";
        }
    }
    protected void gv_Arrival_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            cls_Arrival.deleteArrivalDetailsById("delete_vesselarrivalposition", Convert.ToInt32(gv_Arrival.DataKeys[e.RowIndex].Value.ToString()));
            Bind_grid();
            lbl_Msg.Text = "Record Deleted Successfully";
        }
        catch
        {
            lbl_Msg.Text = "Error in deleting";
            return;
        }
        cleardata();
        this.hiddenfieldarrival.Value = "-1";
        pnl_Arrival.Visible = false;
        SetButtonStatus(0);
        
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

