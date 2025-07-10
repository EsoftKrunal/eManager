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

public partial class CrewOperation_CrewChange1 : System.Web.UI.Page
{
    Authority Auth;
    //----------------------------------------------------
    #region PageControlLoader
    private void Load_Vessel()
    {
        DataSet ds = SearchSignOff.getVessel(Convert.ToInt32(Session["loginid"].ToString()));
        ddl_VesselName.DataSource = ds.Tables[0];
        ddl_VesselName.DataTextField = "VesselName1";
        ddl_VesselName.DataValueField = "VesselId";
        ddl_VesselName.DataBind();
        ddl_VesselName.Items.Insert(0, new ListItem("< All >", "0"));
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
    //----------------------------------------------------
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
         //-----------------------------
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 3);
        if (chpageauth <= 0)
        {
            Response.Redirect("Dummy2.aspx");
        }
        //*******************
        Session["PageName"] = " - Crew Change";
        ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 1);
        OBJ.Invoke();
        Session["Authority"] = OBJ.Authority;
        Auth = OBJ.Authority;
        if (!Page.IsPostBack)
        {
            ddlStatus.SelectedValue = "O";
            bindcountrynameddl();
            ddlCountry_SelectedIndexChanged(sender, e);
            Load_Vessel();
            //Handle_Authority();
            //--------
            
            try
            {
                ddl_VesselName.SelectedIndex = int.Parse(Convert.ToString(Session["CCVessel"]));
                ddlCountry.SelectedIndex = int.Parse(Convert.ToString(Session["CCCountry"]));
                ddlCountry_SelectedIndexChanged(new object(), new EventArgs());
                ddl_port.SelectedIndex = int.Parse(Convert.ToString(Session["CCPort"]));
                txt_EmpNo.Text = Session["EmpNo"].ToString();
                // CLOSE OPEN PORT CALLS IN WHICH NO MEMBER S PENDING FOR ACTION
                Budget.getTable("Update PortCallHeader Set Status='C' where (select count(*) from Portcalldetail where PortCallId=PortCallHeader.PortCallId and isnull(Status,'N')='N')<=0 and STATUS='O'");
            }
            catch { }
            Bind_grid_Port_reference_no("");
        }
    }
    //----------------------------------------------------
    protected void ddl_VesselName_SelectedIndexChanged(object sender, EventArgs e)
    {
        Bind_grid_Port_reference_no(GvRefno.Attributes["MySort"]);
        GvRefno.SelectedIndex = -1;
        // CLOSE OPEN PORT CALLS IN WHICH NO MEMBER S PENDING FOR ACTION
        Budget.getTable("Update PortCallHeader Set Status='C' where (select count(*) from Portcalldetail where PortCallId=PortCallHeader.PortCallId and isnull(Status,'N')='N')<=0 and STATUS='O'");
    }
    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_Port();
    }
    protected void ddl_port_SelectedIndexChanged(object sender, EventArgs e)
    {
        Bind_grid_Port_reference_no(GvRefno.Attributes["MySort"]);
        GvRefno.SelectedIndex = -1;
    }
    protected void ddlStatus_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        Bind_grid_Port_reference_no(GvRefno.Attributes["MySort"]);
        GvRefno.SelectedIndex = -1;
    }

    protected void txt_EmpNo_TextChanged(object sender, EventArgs e)
    {
        Bind_grid_Port_reference_no(GvRefno.Attributes["MySort"]);
        GvRefno.SelectedIndex = -1;
    }
    protected void Bind_grid_Port_reference_no(String Sort)
    {
        Session["CCVessel"] = ddl_VesselName.SelectedIndex;
        Session["CCCountry"] = ddlCountry.SelectedIndex;
        Session["CCPort"] = ddl_port.SelectedIndex;
        Session["EmpNo"] = txt_EmpNo.Text;

        DataTable dt ;
        dt = PortPlanner1.selectPortReferenceNumberDetails(Convert.ToInt32(ddl_port.SelectedValue), Convert.ToInt32(ddl_VesselName.SelectedValue), txt_EmpNo.Text.Trim(), ddlStatus.SelectedValue, Convert.ToInt32(Session["loginid"].ToString()));

        //if (ddl_VesselName.SelectedIndex <= 0)
        //{
        //    dt = PortPlanner1.selectPortReferenceNumberDetails(Convert.ToInt32(ddl_port.SelectedValue), Convert.ToInt32(ddl_VesselName.SelectedValue), txt_EmpNo.Text.Trim(), "O");
        //}
        //else
        //{
        //    dt = PortPlanner1.selectPortReferenceNumberDetails(Convert.ToInt32(ddl_port.SelectedValue), Convert.ToInt32(ddl_VesselName.SelectedValue), txt_EmpNo.Text.Trim(), "");
        //}

        dt.DefaultView.Sort = "PortReferenceNumber";
        this.GvRefno.DataSource = dt;
        this.GvRefno.DataBind();
        GvRefno.Attributes.Add("MySort", Sort);
        bindsignoffdata(0, "");

        string sql = "";
        if (ddl_VesselName.SelectedIndex > 0)
        {
            sql = "Select PromotionSignonId,case when (select ExpectedJoinDate from crewpersonaldetails cpd where cpd.crewid=Pso.crewId)=(SELECT TOP 1 Promotiondate FROM CrewPromotionDetails CPD1 WHERE CPD1.CREWID=Pso.CrewId Order by PromotionId Desc) then 'PROMOTION PORTCALL' else 'VESSEL RENAME PORTCALL'	end	" +
            "+ '/' + v.vesselcode +   '/' + cpd.crewnumber as PortReferenceNumber, 0 as PortId, Pso.VesselId,Pso.CrewID,Pso.ContractId, 2 As PCMode from PromotionSignOn Pso inner join vessel v on v.vesselid=pso.vesselid inner join crewpersonaldetails cpd on cpd.crewid=Pso.crewid Where Pso.VesselId=" + ddl_VesselName.SelectedValue + " UNION Select ContractRevisionId As PromotionSignonId,case when (select ExpectedJoinDate from crewpersonaldetails cpd where cpd.crewid=ccr.crewId)=ccr.ContractRevisionDate then 'CONTRACT REVESION PORTCALL' else 'VESSEL RENAME PORTCALL' end + '/' + v.vesselcode + '/' + cpd.crewnumber as PortReferenceNumber, 0 as PortId, ccr.VesselId,ccr.CrewID,ccr.ContractId, 3 As PCMode from CrewContractRevision ccr with(nolock) inner join vessel v with(nolock) on v.vesselid = ccr.vesselid inner join crewpersonaldetails cpd with(nolock) on cpd.crewid = ccr.crewid Where ccr.VesselId=" + ddl_VesselName.SelectedValue + "";

        }
        else
        {
            sql = "Select PromotionSignonId,case when (select ExpectedJoinDate from crewpersonaldetails cpd where cpd.crewid=Pso.crewId)=(SELECT TOP 1 Promotiondate FROM CrewPromotionDetails CPD1 WHERE CPD1.CREWID=Pso.CrewId Order by PromotionId Desc) then 'PROMOTION PORTCALL' else 'VESSEL RENAME PORTCALL'	end	" +
                  "+ '/' + v.vesselcode +   '/' + cpd.crewnumber as PortReferenceNumber, 0 as PortId, Pso.VesselId,Pso.CrewID,Pso.ContractId, 2 As PCMode from PromotionSignOn Pso inner join vessel v on v.vesselid=pso.vesselid inner join crewpersonaldetails cpd on cpd.crewid=Pso.crewid UNION Select ContractRevisionId As PromotionSignonId,case when (select ExpectedJoinDate from crewpersonaldetails cpd where cpd.crewid=ccr.crewId)=ccr.ContractRevisionDate then 'CONTRACT REVESION PORTCALL' else 'VESSEL RENAME PORTCALL' end + '/' + v.vesselcode + '/' + cpd.crewnumber as PortReferenceNumber, 0 as PortId, ccr.VesselId,ccr.CrewID,ccr.ContractId, 3 As PCMode from CrewContractRevision ccr with(nolock) inner join vessel v with(nolock) on v.vesselid = ccr.vesselid inner join crewpersonaldetails cpd with(nolock) on cpd.crewid = ccr.crewid";
        }
        DataTable dt1 = Budget.getTable(sql).Tables[0];
        dt.DefaultView.Sort = Sort;
        this.GvRefno2.DataSource = dt1;
        this.GvRefno2.DataBind();
        GvRefno2.Attributes.Add("MySort", Sort);

        //string sql1 = "";
        //if (ddl_VesselName.SelectedIndex > 0)
        //{
        //    sql1 = "Select ContractRevisionId,case when (select ExpectedJoinDate from crewpersonaldetails cpd where cpd.crewid=ccr.crewId)=ccr.ContractRevisionDate then 'CONTRACT REVESION PORTCALL' else 'VESSEL RENAME PORTCALL'	end	" +
        //    " + '/' + v.vesselcode + '/' + cpd.crewnumber as PortReferenceNumber, 0 as PortId, ccr.VesselId,ccr.CrewID,ccr.ContractId from CrewContractRevision ccr with(nolock) inner join vessel v with(nolock) on v.vesselid = ccr.vesselid inner join crewpersonaldetails cpd with(nolock) on cpd.crewid = ccr.crewid Where ccr.VesselId=" + ddl_VesselName.SelectedValue 
           

        //}
        //else
        //{
        //    sql1 = "Select ContractRevisionId,case when (select ExpectedJoinDate from crewpersonaldetails cpd where cpd.crewid=ccr.crewId)=ccr.ContractRevisionDate then 'CONTRACT REVESION PORTCALL' else 'VESSEL RENAME PORTCALL'	end	" +
        //    " + '/' + v.vesselcode + '/' + cpd.crewnumber as PortReferenceNumber, 0 as PortId, ccr.VesselId,ccr.CrewID,ccr.ContractId from CrewContractRevision ccr with(nolock) inner join vessel v with(nolock) on v.vesselid = ccr.vesselid inner join crewpersonaldetails cpd with(nolock) on cpd.crewid = ccr.crewid";
        //}
        //DataTable dt2 = Budget.getTable(sql1).Tables[0];
        //dt2.DefaultView.Sort = Sort;
        //this.Gv_ContractRivision.DataSource = dt2;
        //this.Gv_ContractRivision.DataBind();
        //Gv_ContractRivision.Attributes.Add("MySort", Sort);
    }
    //----------------------------------------------------
    protected void chkAll_OnCheckedChanged(object sender, EventArgs e)
    {
        bindsignoffdata(Convert.ToInt32(Session["Planned_PortOnCallId"]), gvsearch.Attributes["MySort"]);
    }
    protected void SortGrid(object sender, EventArgs e)
    {
        LinkButton li = (LinkButton)sender;
        bindsignoffdata(Convert.ToInt32(Session["Planned_PortOnCallId"]), li.CommandArgument);
    }
    protected void GvRefno_Row_Deleting(object sender, GridViewDeleteEventArgs e)
    {
        HiddenField hfdportcallid = (HiddenField)GvRefno.Rows[e.RowIndex].FindControl("HiddenPortCallId");
        int Id = Convert.ToInt32(hfdportcallid.Value);
        DataTable dt1 = PortPlanner1.Check_PortCallDeleted(Id);
        if (Convert.ToInt32(dt1.Rows[0][0].ToString()) == 1)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "travel", "alert('Unable to delete. RFQ has been created.');", true);
            return; 
        }
        Bind_grid_Port_reference_no(GvRefno.Attributes["MySort"]);
        gvsearch.DataBind();
    }
    protected void Delete_Crew(object sender, EventArgs e)
    {
        ImageButton imgDel=((ImageButton)sender);
        int CrewId = Convert.ToInt32(imgDel.CommandArgument);
        string crewtype = imgDel.Attributes["crewtype"];
        //-----------------------------
        if (crewtype == "I") // SIGN ON 
        {
            DataTable dt = Budget.getTable("SELECT isnull(CONTRACTID,0) FROM CREWPERSONALDETAILS WHERE CREWID=" + CrewId.ToString()).Tables[0];
            if (Common.CastAsInt32(dt.Rows[0][0]) > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "travel", "alert('Unable to delete. Contract has been created.');", true);
                return; 
            }
        }
        //-----------------------------
        PortPlanner1.deletePortCallCrewDetailsById("DeletePortCallCrewById", CrewId, int.Parse(Session["Planned_PortOnCallId"].ToString()));
        Budget.getTable("DELETE FROM CrewContractcheckList WHERE Contractid=-" + CrewId.ToString());
        bindsignoffdata(int.Parse(Session["Planned_PortOnCallId"].ToString()), gvsearch.Attributes["MySort"]);
    }
    protected void gvsearch_OnPreRender(object sender, EventArgs e)
    {
        foreach (GridViewRow gr in gvsearch.Rows)
        {
            if (Session["PCMode"].ToString().Trim() == "1") // general portcall
            {
            int VesselId = 0;
            DataTable dtVess = Budget.getTable("select vesselid from portcallheader where portcallid=" + Session["Planned_PortOnCallId"].ToString()).Tables[0];
            VesselId = Convert.ToInt32(dtVess.Rows[0][0].ToString());
            //-------------------------------------------
                
            HiddenField hfdPlanningId = (HiddenField)gr.FindControl("hfdPlanningId");
            int _PlanningId=Common.CastAsInt32(hfdPlanningId.Value);

            HiddenField hfdCrewNo = (HiddenField)gr.FindControl("hfdEmpNo");

            ImageButton prnCheckList = (ImageButton)gr.FindControl("prnCheckList");
            ImageButton prnContract = (ImageButton)gr.FindControl("prnContract");
           
                prnCheckList.Visible = false;
            prnContract.Visible = false;
                LinkButton lbProjoingDocs = (LinkButton)gr.FindControl("lbPrejoingDoc");
                lbProjoingDocs.Visible = true;

                            ImageButton imgCheckList=(ImageButton)gr.FindControl("imgCheckList");
            imgCheckList.Visible = false; 
            ImageButton imgContract = (ImageButton)gr.FindControl("imgContract");
            imgContract.Visible = false; 

            LinkButton lnkAction = (LinkButton)gr.FindControl("lnkAction");
            lnkAction.Visible = ViewState["PC_Status"].ToString() == "Open";

            HiddenField hfdCrew = (HiddenField)gr.FindControl("HiddencrewIdsignoff");
            DataTable dtDet = Budget.getTable("select isnull(status,'') as status from portcalldetail where portcallid=" + Session["Planned_PortOnCallId"].ToString() + " and Crewid=" + hfdCrew.Value).Tables[0];

            lnkAction.Visible=  lnkAction.Visible && dtDet.Rows[0][0].ToString()=="N";

            ImageButton btnDel = (ImageButton)gr.FindControl("btnDel");
            btnDel.Visible = ViewState["PC_Status"].ToString() == "Open" && dtDet.Rows[0][0].ToString() == "N";
               // LinkButton lbProjoingDocs = (LinkButton)gr.FindControl("lbPrejoingDoc");
               

                if ((((HiddenField)gr.FindControl("HfdCrewFlag")).Value.Trim() == "I")) // SIGN ON MEMBERS ( CHECKLIST / CONTRACT / SIGNON )
                {
                    gr.BackColor = System.Drawing.Color.FromName("#99FFCC"); // SIGN ON
                    //DataTable dt = Budget.getTable("SELECT * FROM CrewContractCheckList WHERE CONTRACTID=-" + hfdCrew.Value).Tables[0];
                    DataTable dt = Budget.getTable("SELECT top 1 * FROM crew_doc_checklist WHERE PlanningId=" + _PlanningId + " And CheckedOn is not null").Tables[0];

                    int ActiveContractId = 0;
                    DataTable dtCont = Budget.getTable("select isnull(contractId,0) from crewpersonaldetails where crewid=" + hfdCrew.Value).Tables[0];
                    ActiveContractId = Convert.ToInt32(dtCont.Rows[0][0].ToString());

                    // -- ACTIONS ALREADY COMPLETED
                    int ContId = 0;
                    string SignOnDate = "";
                    DataTable dt_Contract = Budget.getTable("SELECT contractid,SIGNONDATE FROM CREWONVESSELHISTORY WHERE PORTCALLID=" + Session["Planned_PortOnCallId"].ToString() + " AND CREWID=" + hfdCrew.Value).Tables[0];
                    if (dt_Contract.Rows.Count > 0)
                    {
                        ContId = Common.CastAsInt32(dt_Contract.Rows[0][0]);
                        SignOnDate = Common.ToDateString(dt_Contract.Rows[0][1]);
                    }
                    //-----------------------
                    if (dtDet.Rows[0][0].ToString() == "Y")
                    {
                        prnCheckList.Visible = true;
                        prnContract.Visible = true;
                        lbProjoingDocs.Visible = false;
                        if (ContId > 0)
                        {
                            DataTable dtPlanning = Budget.getTable("select top 1 * from crewvesselplanninghistory where relieverid=" + hfdCrew.Value + " and vesselid=" + VesselId.ToString() + " and plannedon<='" + SignOnDate + "' order by plannedon desc").Tables[0];
                            if (dtPlanning.Rows.Count > 0)
                            {
                                prnCheckList.Attributes.Add("onclick", "window.open('ViewCrewCheckList.aspx?_P=" + dtPlanning.Rows[0]["PlanningId"].ToString() + "&_M=V');");
                            }
                        }

                        //prnCheckList.Attributes.Add("onclick", "window.open('../Reporting/ReportCrewCheckLists.aspx?CrewId=" + hfdCrew.Value + "&VesselId=" + VesselId.ToString() + "&ContractId=" + ContId.ToString() + "');");

                        //prnContract.Attributes.Add("onclick", "window.open('../Reporting/PrintContract.aspx?ContractId=" + ContId.ToString() + "&mode=2');");
                        prnContract.Attributes.Add("onclick", "window.open('../Reporting/PrintCrewContract.aspx?ContractId=" + ContId.ToString() + "');");
                        lbProjoingDocs.Attributes.Add("onclick", "window.open('PreJoiningDocuments.aspx?CrewId=" + hfdCrew.Value + "&VesselId=" + VesselId.ToString() + "');");
                    }
                    //----------------------------
                    bool IsCrewMember = false;
                    string CrewNo_1 = hfdCrewNo.Value.Trim();
                    Label lbl_1 = (Label)gr.FindControl("lblranknamesignoff");
                    IsCrewMember =! (lbl_1.Text.Trim() == "SUPTD" || lbl_1.Text.Trim() == "SUPY" || CrewNo_1.Substring(0, 2) == "FS" || CrewNo_1.Substring(0, 2) == "FY");
                    //----------------------------
                    if (dt.Rows.Count <= 0 && (!IsCrewMember))
                    {
                        lnkAction.Text = "[ Checklist ]";
                        lnkAction.ForeColor = System.Drawing.Color.DarkOrange;
                        //lnkAction.Attributes.Add("onclick", "window.open('CrewContractCheckList.aspx?CrewId=" + hfdCrew.Value + "&PortCallId=" + Session["Planned_PortOnCallId"].ToString() + "');");
                        lnkAction.Attributes.Add("onclick", "window.open('ViewCrewCheckList.aspx?_P=" + _PlanningId + "');");
                        lbProjoingDocs.Visible = false;
                    }
                    //---------------------------- 
                    if (dt.Rows.Count <= 0 && ActiveContractId <= 0 && IsCrewMember)  // CHECKLIST IS NOT READY
                    {
                        lnkAction.Text = "[ Checklist ]";
                        lnkAction.ForeColor = System.Drawing.Color.DarkOrange;
                        //lnkAction.Attributes.Add("onclick", "window.open('CrewContractCheckList.aspx?CrewId=" + hfdCrew.Value + "&PortCallId=" + Session["Planned_PortOnCallId"].ToString() + "');");
                        lnkAction.Attributes.Add("onclick", "window.open('ViewCrewCheckList.aspx?_P=" + _PlanningId + "');");
                       
                        lbProjoingDocs.Visible = false;
                    }
                    else
                    {
                        if (ActiveContractId <= 0) // CHECKLIST READY CONTRACT IS PENDING
                        {
                            string CrewNo = hfdCrewNo.Value.Trim();
                            Label lbl = (Label)gr.FindControl("lblranknamesignoff");
                            if (lbl.Text.Trim() == "SUPTD" || lbl.Text.Trim() == "SUPY" || CrewNo.Substring(0, 2) == "FS" || CrewNo.Substring(0, 2) == "FY")
                            {
                                // DIRECT SIGN ON PROCESS WITHOUT CONTRACT

                                imgCheckList.Visible = lnkAction.Visible;
                                imgContract.Visible = false;

                                //imgCheckList.Attributes.Add("onclick", "window.open('CrewContractCheckList.aspx?ContractId=" + ActiveContractId.ToString() + "&CrewId=" + hfdCrew.Value + "&PortCallId=" + Session["Planned_PortOnCallId"].ToString() + "');");
                                imgCheckList.Attributes.Add("onclick", "window.open('ViewCrewCheckList.aspx?_P=" + _PlanningId + "');"); 

                                lnkAction.Text = "[ SignOn ]";
                                lnkAction.ForeColor = System.Drawing.Color.Green;
                                lnkAction.Attributes.Add("onclick", "window.open('CrewSignOn.aspx?CrewId=" + hfdCrew.Value + "&PortCallId=" + Session["Planned_PortOnCallId"].ToString() + "');");
                                lbProjoingDocs.Attributes.Add("onclick", "window.open('PreJoiningDocuments.aspx?CrewId=" + hfdCrew.Value + "&VesselId=" + VesselId.ToString() + "');");
                                lbProjoingDocs.Visible = true;

                            }
                            else
                            {
                                imgCheckList.Visible = lnkAction.Visible;
                                //imgCheckList.Attributes.Add("onclick", "window.open('CrewContractCheckList.aspx?CheckListId=" + dt.Rows[0]["CheckListId"].ToString() + "&CrewId=" + hfdCrew.Value + "&PortCallId=" + Session["Planned_PortOnCallId"].ToString() + "');");
                                imgCheckList.Attributes.Add("onclick", "window.open('ViewCrewCheckList.aspx?_P=" + _PlanningId + "');"); 

                                lnkAction.Text = "[ Contract ]";
                                lnkAction.ForeColor = System.Drawing.Color.Blue;
                                lnkAction.Attributes.Add("onclick", "window.open('CrewContract.aspx?CrewId=" + hfdCrew.Value + "&PortCallId=" + Session["Planned_PortOnCallId"].ToString() + "');");
                                lbProjoingDocs.Attributes.Add("onclick", "window.open('PreJoiningDocuments.aspx?CrewId=" + hfdCrew.Value + "&VesselId=" + VesselId.ToString() + "');");
                                lbProjoingDocs.Visible = true;
                            }
                        }
                        else // READY TO SIGN ON
                        {
                        
                    
                            imgCheckList.Visible = lnkAction.Visible;
                            imgContract.Visible = lnkAction.Visible;

                            //imgCheckList.Attributes.Add("onclick", "window.open('CrewContractCheckList.aspx?ContractId=" + ActiveContractId.ToString() + "&CrewId=" + hfdCrew.Value + "&PortCallId=" + Session["Planned_PortOnCallId"].ToString() + "');");
                            imgCheckList.Attributes.Add("onclick", "window.open('ViewCrewCheckList.aspx?_P=" + _PlanningId + "');"); 
                            
                            imgContract.Attributes.Add("onclick", "window.open('CrewContract.aspx?CrewId=" + hfdCrew.Value + "&PortCallId=" + Session["Planned_PortOnCallId"].ToString() + "&VesselId=" + VesselId.ToString() + "&ContractId=" + ActiveContractId.ToString() + "');");

                            lnkAction.Text = "[ SignOn ]";
                            lnkAction.ForeColor = System.Drawing.Color.Green;
                            lnkAction.Attributes.Add("onclick", "window.open('CrewSignOn.aspx?CrewId=" + hfdCrew.Value + "&PortCallId=" + Session["Planned_PortOnCallId"].ToString() + "');");
                            lbProjoingDocs.Attributes.Add("onclick", "window.open('PreJoiningDocuments.aspx?CrewId=" + hfdCrew.Value + "&VesselId=" + VesselId.ToString() + "');");
                            lbProjoingDocs.Visible = true;
                        }
                    }
                    
                }
                else
                {
                    gr.BackColor = System.Drawing.Color.FromName("#FFCCCC"); // SIGN OFF MEMBERS ( SIGN OFF )
                    lnkAction.Text = "[ Sign Off ]";
                    lnkAction.ForeColor = System.Drawing.Color.Red;
                    lnkAction.Attributes.Add("onclick", "window.open('CrewSignOff.aspx?CrewId=" + hfdCrew.Value + "&PortCallId=" + Session["Planned_PortOnCallId"].ToString() + "');");
                   
                    lbProjoingDocs.Visible = false;
                }

            } 
            else if (Session["PCMode"].ToString().Trim() == "3") // Contract Revision portcall
                {
                int VesselId = 0;
                DataTable dtVess = Budget.getTable("select vesselid from CrewContractRevision with(nolock) where ContractRevisionId=" + Session["ContractRevisionId"].ToString()).Tables[0];
                VesselId = Convert.ToInt32(dtVess.Rows[0][0].ToString());
                //-------------------------------------------
                ImageButton prnCheckList = (ImageButton)gr.FindControl("prnCheckList");
                ImageButton prnContract = (ImageButton)gr.FindControl("prnContract");
                prnCheckList.Visible = false;
                prnContract.Visible = false;
                LinkButton lbProjoingDocs = (LinkButton)gr.FindControl("lbPrejoingDoc");
                lbProjoingDocs.Visible = true;
                ImageButton imgCheckList = (ImageButton)gr.FindControl("imgCheckList");
                imgCheckList.Visible = false;
                ImageButton imgContract = (ImageButton)gr.FindControl("imgContract");
                imgContract.Visible = false;

                LinkButton lnkAction = (LinkButton)gr.FindControl("lnkAction");
                lnkAction.Visible = ViewState["PC_Status"].ToString() == "Open";

                HiddenField hfdCrew = (HiddenField)gr.FindControl("HiddencrewIdsignoff");
                //DataTable dtDet = Budget.getTable("select isnull(status,'') as status from portcalldetail where portcallid=" + Session["Planned_PortOnCallId"].ToString() + " and Crewid=" + hfdCrew.Value).Tables[0];
                //lnkAction.Visible = lnkAction.Visible && dtDet.Rows[0][0].ToString() == "N";

                //ImageButton btnDel = (ImageButton)gr.FindControl("btnDel");
                //btnDel.Visible = ViewState["PC_Status"].ToString() == "Open" && dtDet.Rows[0][0].ToString() == "N";

                if ((((HiddenField)gr.FindControl("HfdCrewFlag")).Value.Trim() == "I")) // SIGN ON MEMBERS ( CHECKLIST / CONTRACT / SIGNON )
                {
                    gr.BackColor = System.Drawing.Color.FromName("#99FFCC"); // SIGN ON
                    //DataTable dt = Budget.getTable("SELECT * FROM CrewContractCheckList WHERE CONTRACTID=-" + hfdCrew.Value).Tables[0];

                    int ActiveContractId = 0;
                    DataTable dtCont = Budget.getTable("select isnull(contractId,0) from crewpersonaldetails where crewid=" + hfdCrew.Value).Tables[0];
                    ActiveContractId = Convert.ToInt32(dtCont.Rows[0][0].ToString());

                    // -- ACTIONS ALREADY COMPLETED

                    //if (dtDet.Rows[0][0].ToString() == "Y")
                    //{
                    //    prnCheckList.Visible = true;
                    //    prnContract.Visible = true;

                    //    prnCheckList.Attributes.Add("onclick", "window.open('../Reporting/ReportCrewCheckLists.aspx?CrewId=" + hfdCrew.Value + "&VesselId=" + VesselId.ToString() + "&ContractId=" + ActiveContractId.ToString() + "');");
                    //    prnContract.Attributes.Add("onclick", "window.open('../Reporting/PrintContract.aspx?ContractId=" + ActiveContractId.ToString() + "&mode=2');");
                    //}


                    imgCheckList.Visible = false;
                    prnCheckList.Visible = false;
                    lbProjoingDocs.Visible = false;
                    //if (dt.Rows.Count <= 0 && ActiveContractId <= 0) // CHECKLIST IS NOT READY
                    if (false) // CHECKLIST ALWAYS READY
                    {
                        lnkAction.Text = "[ Checklist ]";
                        lnkAction.ForeColor = System.Drawing.Color.DarkOrange;
                        lnkAction.Attributes.Add("onclick", "window.open('CrewContractCheckList.aspx?CrewId=" + hfdCrew.Value + "&ContractRevisionId=" + Session["ContractRevisionId"].ToString() + "');");
                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "contract", "window.open('CrewContractCheckList.aspx?CrewId=" + hfdCrew.Value + "&PortCallId=" + Session["Planned_PortOnCallId"].ToString() + "');", true);  
                    }
                    else
                    {
                        if (ActiveContractId <= 0) // CHECKLIST READY CONTRACT IS PENDING
                        {
                            //imgCheckList.Visible = lnkAction.Visible;
                            //imgCheckList.Attributes.Add("onclick", "window.open('CrewContractCheckList.aspx?CheckListId=" + dt.Rows[0]["CheckListId"].ToString() + "&CrewId=" + hfdCrew.Value + "&PromotionSignOnId=" + Session["PromotionSignOnId"].ToString() + "');");

                            lnkAction.Text = "[ Contract ]";
                            lnkAction.ForeColor = System.Drawing.Color.Blue;
                            lnkAction.Attributes.Add("onclick", "window.open('CrewContract.aspx?CrewId=" + hfdCrew.Value + "&ContractRevisionId=" + Session["ContractRevisionId"].ToString() + "');");
                            lbProjoingDocs.Attributes.Add("onclick", "window.open('PreJoiningDocuments.aspx?CrewId=" + hfdCrew.Value + "&VesselId=" + VesselId.ToString() + "');");
                            lbProjoingDocs.Visible = true;
                            //ScriptManager.RegisterStartupScript(this, this.GetType(), "contract", "window.open('CrewContractCheckList.aspx?CrewId=" + hfdCrew.Value + "&PortCallId=" + Session["Planned_PortOnCallId"].ToString() + "');", true);  
                        }
                        else // READY TO SIGN ON
                        {
                            //imgCheckList.Visible = lnkAction.Visible;
                            imgContract.Visible = lnkAction.Visible;

                            //imgCheckList.Attributes.Add("onclick", "window.open('CrewContractCheckList.aspx?ContractId=" + ActiveContractId.ToString() + "&CrewId=" + hfdCrew.Value + "&PromotionSignOnId=" + Session["PromotionSignOnId"].ToString() + "');");
                            imgContract.Attributes.Add("onclick", "window.open('CrewContract.aspx?CrewId=" + hfdCrew.Value + "&ContractRevisionId=" + Session["ContractRevisionId"].ToString() + "&VesselId=" + VesselId.ToString() + "&ContractId=" + ActiveContractId.ToString() + "');");

                            lnkAction.Text = "[ SignOn ]";
                            lnkAction.ForeColor = System.Drawing.Color.Green;
                            lnkAction.Attributes.Add("onclick", "window.open('CrewSignOn.aspx?CrewId=" + hfdCrew.Value + "&ContractRevisionId=" + Session["ContractRevisionId"].ToString() + "');");
                            lbProjoingDocs.Visible = true;
                        }
                    }

                }
                else
                {
                    //lbProjoingDocs.Visible = false;
                    gr.BackColor = System.Drawing.Color.FromName("#FFCCCC"); // SIGN OFF MEMBERS ( SIGN OFF )
                    lnkAction.Text = "[ Sign Off ]";
                    lnkAction.ForeColor = System.Drawing.Color.Red;
                    lnkAction.Attributes.Add("onclick", "window.open('CrewSignOff.aspx?CrewId=" + hfdCrew.Value + "&PortCallId=" + Session["Planned_PortOnCallId"].ToString() + "');");
                    lbProjoingDocs.Visible = false;
                }
            }
            else // promotion / vessel rename portcall
            {

                int VesselId = 0;
                DataTable dtVess = Budget.getTable("select vesselid from promotionsignon where promotionsignonid=" + Session["PromotionSignOnId"].ToString()).Tables[0];
                VesselId = Convert.ToInt32(dtVess.Rows[0][0].ToString());
                //-------------------------------------------
                ImageButton prnCheckList = (ImageButton)gr.FindControl("prnCheckList");
                ImageButton prnContract = (ImageButton)gr.FindControl("prnContract");
                prnCheckList.Visible = false;
                prnContract.Visible = false;
                LinkButton lbProjoingDocs = (LinkButton)gr.FindControl("lbPrejoingDoc");
                lbProjoingDocs.Visible = true;
                ImageButton imgCheckList = (ImageButton)gr.FindControl("imgCheckList");
                imgCheckList.Visible = false;
                ImageButton imgContract = (ImageButton)gr.FindControl("imgContract");
                imgContract.Visible = false;

                LinkButton lnkAction = (LinkButton)gr.FindControl("lnkAction");
                lnkAction.Visible = ViewState["PC_Status"].ToString() == "Open";

                HiddenField hfdCrew = (HiddenField)gr.FindControl("HiddencrewIdsignoff");
                //DataTable dtDet = Budget.getTable("select isnull(status,'') as status from portcalldetail where portcallid=" + Session["Planned_PortOnCallId"].ToString() + " and Crewid=" + hfdCrew.Value).Tables[0];
                //lnkAction.Visible = lnkAction.Visible && dtDet.Rows[0][0].ToString() == "N";

                //ImageButton btnDel = (ImageButton)gr.FindControl("btnDel");
                //btnDel.Visible = ViewState["PC_Status"].ToString() == "Open" && dtDet.Rows[0][0].ToString() == "N";

                if ((((HiddenField)gr.FindControl("HfdCrewFlag")).Value.Trim() == "I")) // SIGN ON MEMBERS ( CHECKLIST / CONTRACT / SIGNON )
                {
                    gr.BackColor = System.Drawing.Color.FromName("#99FFCC"); // SIGN ON
                    //DataTable dt = Budget.getTable("SELECT * FROM CrewContractCheckList WHERE CONTRACTID=-" + hfdCrew.Value).Tables[0];

                    int ActiveContractId = 0;
                    DataTable dtCont = Budget.getTable("select isnull(contractId,0) from crewpersonaldetails where crewid=" + hfdCrew.Value).Tables[0];
                    ActiveContractId = Convert.ToInt32(dtCont.Rows[0][0].ToString());

                    // -- ACTIONS ALREADY COMPLETED

                    //if (dtDet.Rows[0][0].ToString() == "Y")
                    //{
                    //    prnCheckList.Visible = true;
                    //    prnContract.Visible = true;

                    //    prnCheckList.Attributes.Add("onclick", "window.open('../Reporting/ReportCrewCheckLists.aspx?CrewId=" + hfdCrew.Value + "&VesselId=" + VesselId.ToString() + "&ContractId=" + ActiveContractId.ToString() + "');");
                    //    prnContract.Attributes.Add("onclick", "window.open('../Reporting/PrintContract.aspx?ContractId=" + ActiveContractId.ToString() + "&mode=2');");
                    //}

                    
                    imgCheckList.Visible = false;
                    prnCheckList.Visible = false;
                    lbProjoingDocs.Visible = false;
                    //if (dt.Rows.Count <= 0 && ActiveContractId <= 0) // CHECKLIST IS NOT READY
                    if (false) // CHECKLIST ALWAYS READY
                    {
                        lnkAction.Text = "[ Checklist ]";
                        lnkAction.ForeColor = System.Drawing.Color.DarkOrange;
                        lnkAction.Attributes.Add("onclick", "window.open('CrewContractCheckList.aspx?CrewId=" + hfdCrew.Value + "&PromotionSignOnId=" + Session["PromotionSignOnId"].ToString() + "');");
                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "contract", "window.open('CrewContractCheckList.aspx?CrewId=" + hfdCrew.Value + "&PortCallId=" + Session["Planned_PortOnCallId"].ToString() + "');", true);  
                    }
                    else
                    {
                        if (ActiveContractId <= 0) // CHECKLIST READY CONTRACT IS PENDING
                        {
                            //imgCheckList.Visible = lnkAction.Visible;
                            //imgCheckList.Attributes.Add("onclick", "window.open('CrewContractCheckList.aspx?CheckListId=" + dt.Rows[0]["CheckListId"].ToString() + "&CrewId=" + hfdCrew.Value + "&PromotionSignOnId=" + Session["PromotionSignOnId"].ToString() + "');");

                            lnkAction.Text = "[ Contract ]";
                            lnkAction.ForeColor = System.Drawing.Color.Blue;
                            lnkAction.Attributes.Add("onclick", "window.open('CrewContract.aspx?CrewId=" + hfdCrew.Value + "&PromotionSignOnId=" + Session["PromotionSignOnId"].ToString() + "');");
                            lbProjoingDocs.Attributes.Add("onclick", "window.open('PreJoiningDocuments.aspx?CrewId=" + hfdCrew.Value + "&VesselId=" + VesselId.ToString() + "');");
                            lbProjoingDocs.Visible = true;
                            //ScriptManager.RegisterStartupScript(this, this.GetType(), "contract", "window.open('CrewContractCheckList.aspx?CrewId=" + hfdCrew.Value + "&PortCallId=" + Session["Planned_PortOnCallId"].ToString() + "');", true);  
                        }
                        else // READY TO SIGN ON
                        {
                            //imgCheckList.Visible = lnkAction.Visible;
                            imgContract.Visible = lnkAction.Visible;

                            //imgCheckList.Attributes.Add("onclick", "window.open('CrewContractCheckList.aspx?ContractId=" + ActiveContractId.ToString() + "&CrewId=" + hfdCrew.Value + "&PromotionSignOnId=" + Session["PromotionSignOnId"].ToString() + "');");
                            imgContract.Attributes.Add("onclick", "window.open('CrewContract.aspx?CrewId=" + hfdCrew.Value + "&PromotionSignOnId=" + Session["PromotionSignOnId"].ToString() + "&VesselId=" + VesselId.ToString() + "&ContractId=" + ActiveContractId.ToString() + "');");

                            lnkAction.Text = "[ SignOn ]";
                            lnkAction.ForeColor = System.Drawing.Color.Green;
                            lnkAction.Attributes.Add("onclick", "window.open('CrewSignOn.aspx?CrewId=" + hfdCrew.Value + "&PromotionSignOnId=" + Session["PromotionSignOnId"].ToString() + "');");
                            lbProjoingDocs.Visible = true;
                        }
                    }

                }
                else
                {
                    //lbProjoingDocs.Visible = false;
                    gr.BackColor = System.Drawing.Color.FromName("#FFCCCC"); // SIGN OFF MEMBERS ( SIGN OFF )
                    lnkAction.Text = "[ Sign Off ]";
                    lnkAction.ForeColor = System.Drawing.Color.Red;
                    lnkAction.Attributes.Add("onclick", "window.open('CrewSignOff.aspx?CrewId=" + hfdCrew.Value + "&PortCallId=" + Session["Planned_PortOnCallId"].ToString() + "');");
                    lbProjoingDocs.Visible = false;
                }
            }
            //----------------
            if (chkAll.Checked)
                ((CheckBox)gr.FindControl("chkselect")).Checked = true;
 
        }
       
    }
    protected void GvRefno_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Select")
            {
                int index;
                HiddenField hfd;
                LinkButton btn;
                GridViewRow row = (GridViewRow)((Control)e.CommandSource).Parent.Parent;
                hfd = (HiddenField)GvRefno.Rows[row.RowIndex].FindControl("HiddenPortCallId");
                btn = (LinkButton)GvRefno.Rows[row.RowIndex].FindControl("btnrefno");
                Session["PCMode"] = "1";
                Session["Planned_PortOnCallId"] = hfd.Value;
                ViewState["PC_Status"] = btn.Attributes["status"];
                GvRefno.SelectedIndex = row.RowIndex;
                GvRefno2.SelectedIndex = -1;
                index = Convert.ToInt32(hfd.Value.ToString());
                bindsignoffdata(index, gvsearch.Attributes["MySort"]);
                gvsearch.SelectedIndex = -1;
                gvsearch.Columns[0].Visible = true && Auth.isDelete;
                gvsearch.Columns[1].Visible = true;
                chkAll.Visible = ViewState["PC_Status"].ToString() == "Open";
                btnAddCrew.Visible = ViewState["PC_Status"].ToString() == "Open";
            }
        }catch(Exception ex)
        {
            lnkCrewNo.Text = ex.Message;
        }
    }
    protected void GvRefno_RowCommand2(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            int index;
            HiddenField hfd;
            HiddenField hdnpcmode;
            LinkButton btn;
            GridViewRow row = (GridViewRow)((Control)e.CommandSource).Parent.Parent;
            hfd = (HiddenField)GvRefno2.Rows[row.RowIndex].FindControl("HiddenPromotionSignOnId");
            btn = (LinkButton)GvRefno2.Rows[row.RowIndex].FindControl("btnrefno");
            hdnpcmode = (HiddenField)GvRefno2.Rows[row.RowIndex].FindControl("hdnPCMode"); 
            
            Session["PCMode"] = hdnpcmode.Value;
            if (hdnpcmode.Value == "2")
            {
                Session["PromotionSignOnId"] = hfd.Value;
            }
            if (hdnpcmode.Value == "3")
            {
                Session["ContractRevisionId"] = hfd.Value;
            }
            ViewState["PC_Status"] = "Open";
            GvRefno.SelectedIndex = -1;
            GvRefno2.SelectedIndex = row.RowIndex;
            index = Convert.ToInt32(hfd.Value.ToString());
            bindsignoffdata(index, gvsearch.Attributes["MySort"]);
            gvsearch.SelectedIndex = -1;
            gvsearch.Columns[0].Visible = true && Auth.isDelete;
            gvsearch.Columns[1].Visible = true;
            chkAll.Visible = ViewState["PC_Status"].ToString() == "Open";
            btnAddCrew.Visible = false;
        }
    }

    protected void Refresh_Click(object sender, EventArgs e)
    {
        if (Session["PCMode"].ToString().Trim() == "1")
        {
            bindsignoffdata(int.Parse(Session["Planned_PortOnCallId"].ToString()), gvsearch.Attributes["MySort"]);
        }
        if (Session["PCMode"].ToString().Trim() == "3")
        {
            bindsignoffdata(int.Parse(Session["Planned_PortOnCallId"].ToString()), gvsearch.Attributes["MySort"]);
        }
        else
        {
            bindsignoffdata(int.Parse(Session["PromotionSignOnId"].ToString()), gvsearch.Attributes["MySort"]);
        }
        ddl_VesselName_SelectedIndexChanged(sender, e);
    }
    protected void bindsignoffdata(int PortCallId, String Sort)
    {
        btnAddCrew.Visible = false;
        DataTable dt1=new DataTable();
        try
        {
            if (Session["PCMode"] != null && Session["PCMode"].ToString().Trim() == "1")
            {
                dt1 = PortPlanner1.selectSignOffGridDetails(PortCallId);
            }
            else if (Session["PCMode"] != null && Session["PCMode"].ToString().Trim() == "3")
            {
                string sql = "select ccr.crewid,cpd.crewnumber,cpd.firstname+ ' ' + cpd.middlename + ' ' +cpd.lastname as  CrewName, r.rankcode as rankname,cpd.signondate,cpd.signoffdate,cpd.ReliefDueDate,'I' as CrewFlag,-1 as PlanningId from CrewContractRevision ccr inner join crewpersonaldetails cpd on ccr.crewid=cpd.crewid inner join rank r on cpd.currentrankid=r.rankid where ccr.ContractRevisionId=" + PortCallId.ToString();
                dt1 = Budget.getTable(sql).Tables[0];
            }
            else
            {
                string sql = "select ps.crewid,cpd.crewnumber,cpd.firstname+ ' ' + cpd.middlename + ' ' +cpd.lastname as  CrewName, " +
                           "r.rankcode as rankname,cpd.signondate,cpd.signoffdate,cpd.ReliefDueDate,'I' as CrewFlag,-1 as PlanningId " +
                           "from promotionsignon ps inner join crewpersonaldetails cpd on ps.crewid=cpd.crewid " +
                           "inner join rank r on cpd.currentrankid=r.rankid where ps.promotionsignonid=" + PortCallId.ToString();
                dt1 = Budget.getTable(sql).Tables[0];

                btnAddCrew.Visible = true;
            }
            dt1.DefaultView.Sort = Sort;
        }
        catch { } 
        this.gvsearch.DataSource = dt1;
        this.gvsearch.DataBind();
        gvsearch.Attributes.Add("MySort", Sort);
        
        
    }
    //----------------------------------------------------
    protected void btnMailTravel_Click(object sender, EventArgs e)
    {
        string CrewList = "";
        foreach (GridViewRow gr in gvsearch.Rows)
        {
            CheckBox chkBx = (CheckBox)gr.FindControl("chkselect");
            if (chkBx.Checked)
            {
                CrewList += "," + chkBx.Attributes["value"]; 
            }
            if (CrewList.StartsWith(","))
                CrewList = CrewList.Substring(1);
        }
        if (Session["PCMode"].ToString() == "2")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "travel", "window.open('CrewMailSend.aspx?PortCallId=" + Session["Planned_PortOnCallId"].ToString() + "&CrewList=" + CrewList + "&mode=2');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "travel", "window.open('CrewMailSend.aspx?PortCallId=" + Session["Planned_PortOnCallId"].ToString() + "&CrewList=" + CrewList + "&mode=3');", true);
        }
    }
    protected void btnMailPort_Click(object sender, EventArgs e)
    {
        string CrewList = "";
        foreach (GridViewRow gr in gvsearch.Rows)
        {
            CheckBox chkBx = (CheckBox)gr.FindControl("chkselect");
            if (chkBx.Checked)
            {
                CrewList += "," + chkBx.Attributes["value"]; 
            }
            if (CrewList.StartsWith(","))
                CrewList = CrewList.Substring(1);
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "travel", "window.open('CrewMailSend.aspx?PortCallId=" + Session["Planned_PortOnCallId"].ToString() + "&CrewList=" + CrewList + "&mode=1');", true);  
    }
    protected void btnAddCrew_Click(object sender, EventArgs e)
    {
        //---------------
        if (Session["Planned_PortOnCallId"] != null)
        {
            dvAddCrew.Visible = true;
            ftmCrewlist.Attributes.Add("src", "ManagePortCall.aspx?PCId=" + Session["Planned_PortOnCallId"].ToString());
        } 
        //---------------
    }
    protected void btnClose1_Click(object sender, EventArgs e)
    {
        if (GvRefno.SelectedIndex >= 0)
        {
            HiddenField hfd = (HiddenField)GvRefno.Rows[GvRefno.SelectedIndex].FindControl("HiddenPortCallId");
            int index = Convert.ToInt32(hfd.Value.ToString());
            bindsignoffdata(index, gvsearch.Attributes["MySort"]);
        }
        //---------------
        dvAddCrew.Visible = false;
        //---------------
    }


    //protected void Gv_ContractRivision_RowCommand(object sender, GridViewCommandEventArgs e)
    //{
    //    try
    //    {
    //        if (e.CommandName == "Select")
    //        {
    //            int index;
    //            HiddenField hfd;
    //            LinkButton btn;
    //            GridViewRow row = (GridViewRow)((Control)e.CommandSource).Parent.Parent;
    //            hfd = (HiddenField)Gv_ContractRivision.Rows[row.RowIndex].FindControl("hdnCrewContractRevisionId");
    //            btn = (LinkButton)Gv_ContractRivision.Rows[row.RowIndex].FindControl("btnContractrefno");
    //            Session["PCMode"] = "3";
    //            Session["ContractRevisionId"] = hfd.Value;
    //            ViewState["PC_Status"] = "Open";
    //            GvRefno.SelectedIndex = -1;
    //            GvRefno2.SelectedIndex = -1;
    //            Gv_ContractRivision.SelectedIndex = row.RowIndex;
    //            index = Convert.ToInt32(hfd.Value.ToString());
    //            bindsignoffdata(index, gvsearch.Attributes["MySort"]);
    //            gvsearch.SelectedIndex = -1;
    //            gvsearch.Columns[0].Visible = true && Auth.isDelete;
    //            gvsearch.Columns[1].Visible = true;
    //            chkAll.Visible = ViewState["PC_Status"].ToString() == "Open";
    //            btnAddCrew.Visible = false;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        lnkCrewNo.Text = ex.Message;
    //    }
    //}
}
    
