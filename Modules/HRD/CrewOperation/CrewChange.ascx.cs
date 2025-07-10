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

public partial class CrewOperation_CrewChange : System.Web.UI.UserControl
{
    int temp;
    Authority Auth;
    protected void Page_Load(object sender, EventArgs e)
    {
        ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 2);
        OBJ.Invoke();
        Session["Authority"] = OBJ.Authority;
        Auth = OBJ.Authority;
        temp = 0;
        if (!Page.IsPostBack)
        {
            bindcountrynameddl();
            ddlCountry_SelectedIndexChanged(sender,e);
            Load_Vessel();
            Handle_Authority();
            //--------
            try
            {
                ddl_VesselName.SelectedIndex= int.Parse(Session["CCVessel"].ToString() );
                ddlCountry.SelectedIndex=int.Parse(Session["CCCountry"].ToString());
                ddlCountry_SelectedIndexChanged(new object(), new EventArgs());  
                ddl_port.SelectedIndex=int.Parse(Session["CCPort"].ToString());
                txt_EmpNo.Text=Session["EmpNo"].ToString();
                ddl_port_SelectedIndexChanged(new object(), new EventArgs());  
            }
            catch { }
        }
        Message.Text = "";
    }
    private void Handle_Authority()
    {
        GvRefno.Columns[0].Visible = Auth.isDelete;
    }
    #region PageControlLoader
    private void Load_Vessel()
    {
        //DataSet ds = cls_SearchReliever.getMasterData("Vessel", "VesselId", "VesselName", Convert.ToInt32(Session["loginid"].ToString()));
        //ddl_VesselName.DataSource = ds.Tables[0];
        //ddl_VesselName.DataValueField = "VesselId";
        //ddl_VesselName.DataTextField = "VesselName";
        //ddl_VesselName.DataBind();
        //ddl_VesselName.Items.Insert(0, new ListItem("<Select>", "0"));

        DataSet ds = SearchSignOff.getVessel(Convert.ToInt32(Session["loginid"].ToString()));
        ddl_VesselName.DataSource = ds.Tables[0];
        ddl_VesselName.DataTextField = "VesselName1";
        ddl_VesselName.DataValueField = "VesselId";
        ddl_VesselName.DataBind();
        ddl_VesselName.Items.Insert(0, new ListItem("< Select >", "0"));
    }
    private void bindcountrynameddl()
    {
        DataTable dt3 = PortPlanner.selectCountryName();
        this.ddlCountry.DataValueField = "CountryId";
        this.ddlCountry.DataTextField = "CountryName";
        this.ddlCountry.DataSource = dt3;
        this.ddlCountry.DataBind();
    }
    private void Load_Port()
    {
        DataTable dt4 = PortPlanner.selectPortName(Convert.ToInt32(ddlCountry.SelectedValue));
        this.ddl_port.DataValueField = "PortId";
        this.ddl_port.DataTextField = "PortName";
        this.ddl_port.DataSource = dt4;
        this.ddl_port.DataBind();
    }
    #endregion
    protected void ddl_VesselName_SelectedIndexChanged(object sender, EventArgs e)
    {
        Bind_grid_Port_reference_no(GvRefno.Attributes["MySort"]);
        panel_portref.Visible = true;
        panel_SignOff.Visible = false;
        GvRefno.SelectedIndex = -1;
    }
    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_Port();
    }
    protected void ddl_port_SelectedIndexChanged(object sender, EventArgs e)
    {
        Bind_grid_Port_reference_no(GvRefno.Attributes["MySort"]);
        panel_portref.Visible = true;
        panel_SignOff.Visible = false;
        GvRefno.SelectedIndex = -1;
    }
    protected void txt_EmpNo_TextChanged(object sender, EventArgs e)
    {
        Bind_grid_Port_reference_no(GvRefno.Attributes["MySort"]);
        panel_portref.Visible = true;
        panel_SignOff.Visible = false;
        GvRefno.SelectedIndex = -1;
    }
    
    private void Bind_grid_Port_reference_no(String Sort)
    {
        Session["CCVessel"]=ddl_VesselName.SelectedIndex; 
        Session["CCCountry"]=ddlCountry.SelectedIndex; 
        Session["CCPort"]=ddl_port.SelectedIndex;
        Session["EmpNo"] = txt_EmpNo.Text;   

        DataTable dt = PortPlanner1.selectPortReferenceNumberDetails(Convert.ToInt32(ddl_port.SelectedValue), Convert.ToInt32(ddl_VesselName.SelectedValue),txt_EmpNo.Text.Trim(),"", Convert.ToInt32(Session["loginid"].ToString()));
        dt.DefaultView.Sort = Sort;
        this.GvRefno.DataSource = dt;
        this.GvRefno.DataBind();
        GvRefno.Attributes.Add("MySort", Sort);

        //string sql = "Select case when (select ExpectedJoinDate from crewpersonaldetails cpd where cpd.crewid=Pso.crewId)=(SELECT TOP 1 Promotiondate FROM CrewPromotionDetails CPD1 WHERE CPD1.CREWID=Pso.CrewId Order by PromotionId Desc) " +
        //           "then 'PROMOTION PORTCALL' else 'VESSEL RENAME PORTCALL'	end	as PortCallNo,Pso.CrewID " +
        //           "from PromotionSignOn Pso Where ContractId<=0 and Pso.VesselId=1 ";

        //DataTable dt = Budget.getTable("")  .selectPortReferenceNumberDetails(Convert.ToInt32(ddl_port.SelectedValue), Convert.ToInt32(ddl_VesselName.SelectedValue), txt_EmpNo.Text.Trim());
        //dt.DefaultView.Sort = Sort;
        //this.GvRefno.DataSource = dt;
        //this.GvRefno.DataBind();
        //GvRefno.Attributes.Add("MySort", Sort);
    }
    protected void on_Sorting(object sender, GridViewSortEventArgs e)
    {
        Bind_grid_Port_reference_no(e.SortExpression);
    }
    protected void on_Sorted(object sender, EventArgs e)
    {
        foreach (GridViewRow dg in gvsearch.Rows)
        {
            HiddenField hfd1;
            hfd1 = (HiddenField)dg.FindControl("HfdCrewFlag");
            if (hfd1 != null)
            {
                if (hfd1.Value == "I")
                {
                    dg.BackColor = System.Drawing.Color.FromName("#C9F4DA");
                }
                else
                {
                    dg.BackColor = System.Drawing.Color.LightYellow;
                }
            }
        }
    }
    protected void GvRefno_Row_Deleting(object sender, GridViewDeleteEventArgs e)
    {
        HiddenField hfdportcallid;
        int Id;
        hfdportcallid = (HiddenField)GvRefno.Rows[e.RowIndex].FindControl("HiddenPortCallId");
        ////*********** CODE TO CHECK FOR BRANCHID ***********
        //DataTable dt68 = Alerts.GetCrewIdFromPortCallId(Convert.ToInt32(hfdportcallid.Value));
        //foreach (DataRow dr in dt68.Rows)
        //{
        //    string xpc = Alerts.Check_BranchId(Convert.ToInt32(dr["CrewId"].ToString()));
        //    if (xpc.Trim() != "")
        //    {
        //        GvRefno.SelectedIndex = -1;
        //        temp = 1;
        //        int B_Id = Convert.ToInt32(ConfigurationManager.AppSettings["BranchId"].ToString());
        //        if (B_Id == 1)
        //        {
        //            Message.Text = "This PortCall belongs to Yangon Office.";
        //        }
        //        else
        //        {
        //            Message.Text = "This Port Call belongs to Singapore/Mumbai Office.";
        //        }
        //        return;
        //    }
        //}
        ////************
        Id = Convert.ToInt32(hfdportcallid.Value);
        DataTable dt1 = PortPlanner1.Check_PortCallDeleted(Id);
        if (Convert.ToInt32(dt1.Rows[0][0].ToString()) == 1)
        {
            Message.Text = "Port Call Can't be Deleted Because Some RFQ Has been Generated.";
        }
        Bind_grid_Port_reference_no(GvRefno.Attributes["MySort"]);
        gvsearch.DataBind();
    }
    protected void GvRefno_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            int index;
            HiddenField hfd;
            LinkButton btn;
            GridViewRow row = (GridViewRow)((Control)e.CommandSource).Parent.Parent;
            hfd = (HiddenField)GvRefno.Rows[row.RowIndex].FindControl("HiddenPortCallId");
            ////*********** CODE TO CHECK FOR BRANCHID ***********
            //DataTable dt68 = Alerts.GetCrewIdFromPortCallId(Convert.ToInt32(hfd.Value));
            //foreach (DataRow dr in dt68.Rows)
            //{
            //    string xpc = Alerts.Check_BranchId(Convert.ToInt32(dr["CrewId"].ToString()));
            //    if (xpc.Trim() != "")
            //    {
            //        GvRefno.SelectedIndex = -1;
            //        temp = 1;
            //        gvsearch.DataBind();
            //        Message.Text = xpc;
            //        return;
            //    }
            //}
            ////************
            btn = (LinkButton)GvRefno.Rows[row.RowIndex].FindControl("btnrefno");
            if (row.Cells[5].Text.Trim() == "Open")
            {
                Session["Planned_PortOnCallId"] = hfd.Value;
                Session["Planned_PortOnCallRef"] = btn.Text;

                Session["Planned_CrewId_List"] = null;
                Session["CrewID_Planning"] = null;
                GvRefno.SelectedIndex = row.RowIndex;
                index = Convert.ToInt32(hfd.Value.ToString());
                Session["portdelid"] = index.ToString();
                bindsignoffdata(index, gvsearch.Attributes["MySort"]);
                gvsearch.SelectedIndex = -1;
                panel_SignOff.Visible = true;
                gvsearch.Columns[0].Visible = true && Auth.isDelete ;
                gvsearch.Columns[1].Visible = true;
               
            }
            else
            {
                Session["Planned_PortOnCallId"] = hfd.Value;
                Session["Planned_PortOnCallRef"] = btn.Text;
                //Session["Planned_PortOnCallId"] = null;
                //Session["Planned_PortOnCallRef"] = "";
                Session["Planned_CrewId_List"] = null;
                Session["CrewID_Planning"] = null;
                GvRefno.SelectedIndex = -1;
                index = Convert.ToInt32(hfd.Value.ToString());
                Session["portdelid"] = index.ToString();
                bindsignoffdata(index, gvsearch.Attributes["MySort"]);
                gvsearch.SelectedIndex = -1;
                panel_SignOff.Visible = true;
                gvsearch.Columns[0].Visible = false && Auth.isDelete;
                gvsearch.Columns[1].Visible = true; 
            }
            for (int k = 0; k < this.gvsearch.Rows.Count; k++)
            {
                CheckBox chkbx = (CheckBox)gvsearch.Rows[k].FindControl("chkselect");
                chkbx.Checked = true;
            }
            checked_Change(sender, e);
        }
    }
    protected void GvRefno_SelectIndexChanged(object sender, EventArgs e)
    {
    }
    protected void GvRefno_RowEditing(object sender, GridViewEditEventArgs e)
    {
    }
    protected void GvRefno_prerender(object sender, EventArgs e)
    {
        if (temp == 1)
        {
            GvRefno.SelectedIndex = -1;
        }
    }

    private void bindsignoffdata(int id,String Sort)
    {
        DataTable dt1 = PortPlanner1.selectSignOffGridDetails(id);
        dt1.DefaultView.Sort = Sort;
        this.gvsearch.DataSource = dt1;
        this.gvsearch.DataBind();
        gvsearch.Attributes.Add("MySort", Sort);
    }
    protected void on_Sorting1(object sender, GridViewSortEventArgs e)
    {
        bindsignoffdata(Convert.ToInt32(Session["Planned_PortOnCallId"]),e.SortExpression);
    }
    protected void on_Sorted1(object sender, EventArgs e)
    {
    }
    protected void gvsearch_Row_Deleting(object sender, GridViewDeleteEventArgs e)
    {
        int portcallid = Convert.ToInt32(Session["Planned_PortOnCallId"].ToString());
        HiddenField hfdcrewid;
        hfdcrewid = (HiddenField)gvsearch.Rows[e.RowIndex].FindControl("HiddencrewIdsignoff");
        int crewid = Convert.ToInt32(hfdcrewid.Value.ToString());
        ////*********** CODE TO CHECK FOR BRANCHID ***********
        //string xpc = Alerts.Check_BranchId(crewid);
        //if (xpc.Trim() != "")
        //{
        //    //gvsearch.SelectedIndex = -1;
        //    temp = 1;
        //    Message.Text = xpc;
        //    return;
        //}
        ////************
        DataTable dt1 = PortPlanner1.Check_PortCallDeleted_travel(crewid, Convert.ToInt32(Session["portdelid"]));
        if (Convert.ToInt32(dt1.Rows[0][0].ToString()) == 1)
        {
            Message.Text = "Crew Member Can't be Deleted Because PO Has been Generated.";
        }
        else if (Convert.ToInt32(dt1.Rows[0][0].ToString()) == 2)
        {
            Message.Text = "Crew Member Can't be Deleted Because Contract Has been Created.";
        }
        else if (Convert.ToInt32(dt1.Rows[0][0].ToString()) == 3)
        {
            Message.Text = "Crew Member Can't be Deleted Because Crew Has been Sign Off.";
        }
        else
        {
            PortPlanner1.deletePortCallCrewDetailsById("DeletePortCallCrewById", crewid, portcallid);
        }
        bindsignoffdata(portcallid, gvsearch.Attributes["MySort"]);
    }
    protected void checked_Change(object sender, EventArgs e)
    {
        string crewString = "";
        int i = 0;
        CheckBox chk = new CheckBox();
        foreach (GridViewRow dg in gvsearch.Rows)
        {
            chk = (CheckBox)dg.FindControl("chkselect");
            if (chk.Checked == true)
            {
                HiddenField hfd;
                hfd = (HiddenField)dg.FindControl("HiddencrewIdsignoff");
                ////*********** CODE TO CHECK FOR BRANCHID ***********
                //string xpc = Alerts.Check_BranchId(Convert.ToInt32(hfd.Value));
                //if (xpc.Trim() != "")
                //{
                //    gvsearch.SelectedIndex = -1;
                //    Message.Text = xpc;
                //    return;
                //}
                ////************
                if (i == 0)
                {
                    i = i + 1;
                    crewString = hfd.Value;
                }
                else
                {
                    crewString = crewString + " , " + hfd.Value;
                }
            }
            HiddenField hfd1;
            hfd1 = (HiddenField)dg.FindControl("HfdCrewFlag");
            if (hfd1 != null)
            {
                if (hfd1.Value == "I")
                {
                    dg.BackColor = System.Drawing.Color.FromName("#C9F4DA");
                }
                else
                {
                    dg.BackColor = System.Drawing.Color.LightYellow;
                }
            }
        }
        string crewStringstr = crewString;
        Session["Planned_CrewId_List"] = crewStringstr;
    }
    protected void gvsearch_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //if (e.commandname == "select")
        //{
        //    //gd_reliver.selectedindex = -1;
        //    int crewid;
        //    hiddenfield hfd;
        //    gridviewrow row = (gridviewrow)((control)e.commandsource).parent.parent;
        //    hfd = (hiddenfield)gvsearch.rows[row.rowindex].findcontrol("hiddencrewidsignoff");
        //    gvsearch.selectedindex = row.rowindex;
        //    crewid = convert.toint32(hfd.value.tostring());
        //    session["crewid_planning"] = crewid;
        //    foreach (GridViewRow dg in gvsearch.Rows)
        //    {
        //        HiddenField hfd1;
        //        hfd1 = (HiddenField)dg.FindControl("HfdCrewFlag");
        //        if (hfd1 != null)
        //        {
        //            if (hfd1.Value == "I")
        //            {
        //                dg.BackColor = System.Drawing.Color.FromName("#C9F4DA");
        //            }
        //            else
        //            {
        //                dg.BackColor = System.Drawing.Color.LightYellow;
        //            }
        //        }
        //    }
        //}
    }
    protected void gvsearch_RowDataBound1(object sender, GridViewRowEventArgs e)
    {
        HiddenField hfd;
        hfd = (HiddenField)e.Row.FindControl("HfdCrewFlag");
        if (hfd != null)
        {
            if (hfd.Value == "I")
            {
                e.Row.BackColor=System.Drawing.Color.FromName("#C9F4DA");     
            }
            else
            {
                e.Row.BackColor = System.Drawing.Color.LightYellow;     
            }        
        }
    }

    //private void Bind_grid_Port_refno_forBlankCrew(String Sort)
    //{
    //    DataTable dt = PortPlanner1.selectPortReferenceNumberDetails(Convert.ToInt32(ddl_port.SelectedValue), Convert.ToInt32(ddl_VesselName.SelectedValue), "");
    //    dt.DefaultView.Sort = Sort;
    //    this.GvRefno.DataSource = dt;
    //    this.GvRefno.DataBind();
    //    GvRefno.Attributes.Add("MySort", Sort);
    //}
    protected void btn_Add_Click(object sender, EventArgs e)
    {
        //HiddenField hfd;
        //int res;
        //int crewid=0, vesselid;

        //if (this.txt_EmpNo.Text == "")
        //{
        //    this.Message.Text = "Please Enter Emp#.";
        //    return;
        //}
        //if (this.GvRefno.SelectedIndex < 0)
        //{
        //    this.Message.Text = "Please Select a PortCall.";
        //    Bind_grid_Port_refno_forBlankCrew(GvRefno.Attributes["MySort"]);
        //    return;
        //}
        //if (this.GvRefno.SelectedRow.Cells[5].Text.Trim() == "Closed")
        //{
        //    this.Message.Text = "Please Select a Open PortCall.";
        //    return;
        //}

        //DataTable dt22 = ReportPrintCV.selectCrewIdCrewNumber(txt_EmpNo.Text.Trim());
        //if (dt22.Rows.Count == 0 && txt_EmpNo.Text != "")
        //{
        //    this.Message.Text = "Invalid Emp#.";
        //    return;
        //}
        //else
        //{
        //    foreach (DataRow dr in dt22.Rows)
        //    {
        //        crewid = Convert.ToInt32(dr["CrewId"].ToString());
        //    }
        //}
        
        
        //try
        //{
        //    vesselid = Convert.ToInt32(ddl_VesselName.SelectedValue);

        //    if (NewPlanning.Check_RelieverStatus(crewid, vesselid) == 1)
        //    {
        //        this.Message.Text = "This CrewMember is already a Reliever against a Crew Member in Relief Planning.";
        //        return;
        //    }
        //    if (NewPlanning.Check_RelieverStatus(crewid, vesselid) == 2)
        //    {
        //        this.Message.Text = "This CrewMember is already Planned for This Vessel.";
        //        return;
        //    }
        //    hfd = (HiddenField)GvRefno.SelectedRow.FindControl("HiddenPortCallId");
        //    int dtch = PortPlanner1.Insert_NewMembertoPortCall(crewid, Convert.ToInt32(hfd.Value));
        //    if (dtch == 0)
        //    {
        //        this.Message.Text = "Crew Member's Status must be New/OnLeave.";
        //        return;
        //    }
        //    if (dtch == -1)
        //    {
        //        this.Message.Text = "Crew Member does not Exists.";
        //        return;
        //    }
        //    if (dtch == 1)
        //    {
        //        this.Message.Text = "Crew Member Already Exists in same Port Call.";
        //        return;
        //    }
        //    res = NewPlanning.Add_Planning(crewid, vesselid);
             
        //    this.Message.Text = "Successfully Added.";
        //    bindsignoffdata(Convert.ToInt32(hfd.Value.ToString()), gvsearch.Attributes["MySort"]);
        //}
        //catch { this.Message.Text = "Can't be Added."; }
    }
    protected void btn_Show_Click(object sender, EventArgs e)
    {
        //Bind_grid_Port_reference_no(GvRefno.Attributes["MySort"]);
    }
}