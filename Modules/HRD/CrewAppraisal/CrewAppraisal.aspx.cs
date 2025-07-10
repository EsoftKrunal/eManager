using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using ShipSoft.CrewManager.BusinessObjects;
using ShipSoft.CrewManager.BusinessLogicLayer;
using ShipSoft.CrewManager.Operational;


public partial class CrewAppraisal : System.Web.UI.Page
{
    public AuthenticationManager Auth;
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //lblMsg_AP.Text = "";
        //-----------------------------
        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 12);
        if (chpageauth <= 0)
        {
            Response.Redirect("Dummy3.aspx");
        }
        //*******************
        //*******************
        Auth = new AuthenticationManager(12, Convert.ToInt32(Session["loginid"].ToString()), ObjectType.Page);
        if (!Page.IsPostBack)
        {
            BindVessel();
            ddlStatus.SelectedIndex = 1;
            BindRepeater();
            LoadStatus(1);
        }
    }
    
    protected void btnClose_Click(object sender, EventArgs e)
    {
        dvFrame.Visible = false;
    }
	public void LoadStatus(int Mode)
	{
		ddlStatus.Items.Clear();
		ddlStatus.Items.Add(new ListItem("< All >","0"));
		ddlStatus.Items.Add(new ListItem("Open","1"));

		if(Mode==2)
		{
			ddlStatus.Items.Add(new ListItem("Verified","2"));	
			ddlStatus.Items.Add(new ListItem("Closed","3"));		
		}
		else
		{
			ddlStatus.Items.Add(new ListItem("Closed/Verified","2"));	
		}
		ddlStatus.SelectedValue="1";

        }
    protected void btnOffice_Click(object sender, EventArgs e)
    {
        btnOffice.CssClass = "selbtn";
        btnShip.CssClass = "btn1";
        btnCrewAssessment.CssClass = "btn1";

        lblMessage.Visible = true;
        lblNoOfrec.Visible = false;
        tblmain.Visible = true;
       // dvFrame.Visible = true;
        dvfrm.Visible = false;
        LoadStatus(1);
        BindRepeater();
    }
    protected void btnShip_Click(object sender, EventArgs e)
    {
        btnOffice.CssClass = "btn1";
        btnShip.CssClass = "selbtn";
        btnCrewAssessment.CssClass = "btn1";
        tblmain.Visible = true;
       // dvFrame.Visible = true;
        dvfrm.Visible = false;
        lblMessage.Visible = false;
        lblNoOfrec.Visible = true;
            LoadStatus(2);
        BindRepeater();
    }
    
    protected void lblMessage_OnClick(object sender, EventArgs e)
    {
        dvFrame.Visible = true;
        string sql = "SELECT CCB.CREWBONUSID,CCB.CREWID,CPD.CREWNUMBER,CCB.CONTRACTID,CCB.ContractRefNumber,CPD.FIRSTNAME + ' ' +CPD.MIDDLENAME + ' ' +CPD.LASTNAME AS CREWNAME ,CCB.RANKID,RANKCODE,CCB.VESSELID,V.VESSELNAME, " +
                    "ccb.SignOnDate,ccb.SignOffDate, AssMgntID AS PeapId " +
                    "FROM CREWCONTRACTBONUSMASTER CCB " +
                    "INNER JOIN CREWPERSONALDETAILS CPD ON CCB.CREWID = CPD.CREWID " +
                    "INNER JOIN RANK R ON CCB.RANKID = R.RANKID " +
                    "INNER JOIN VESSEL V ON V.VESSELID = CCB.VESSELID AND V.VESSELSTATUSID=1  " +
                    "LEFT JOIN tbl_Assessment ON tbl_Assessment.CREWBONUSID = CCB.CREWBONUSID " +
                    "WHERE CCB.RANKID IN (1,12) AND ccb.SignOnDate>='01-jan-2016' and " +
                    "CCB.STATUS = 'A' and ISNULL(AssMgntID,0)<= 0 ORDER BY V.VESSELNAME,CREWNAME";
        rprData1.DataSource = Common.Execute_Procedures_Select_ByQueryCMS("select * from vw_PendingCrewForAppraisal");
        rprData1.DataBind();
    }        

    public void BindRepeater()
    {
        string sql = "select Ass.status,App.AppraisalOccasionName as Occasion ,Ass.AssMgntID,Ass.AssName, Ass.AssLname, (Ass.AssName+' '+Ass.AssLname)AssNameMod,Ass.rank ,Ass.VesselCode,Ass.CrewNo   "+
        " ,(case when Ass.PeapType=1 then 'MANAGEMENT' when Ass.PeapType=2 then 'SUPPORT' when Ass.PeapType=3 then 'OPERATION' end )PeapType   "+
        " ,(select RankCode from Rank R where R.RankID in((select CurrentrankID from CrewPersonalDetails  CD where CD.CrewNumber=Ass.CrewNo))) ShipSoftRank" +
        " ,replace (convert(varchar, Ass.AppraisalFromDate,106),' ','-')AppraisalFromDate    "+
        " ,replace (convert(varchar, Ass.AppraisalToDate ,106),' ','-')AppraisalToDate    "+
        " ,replace (convert(varchar, Ass.DatejoinedComp,106),' ','-')DatejoinedComp    "+
        " ,replace (convert(varchar, Ass.DatejoinedVessel ,106),' ','-')DatejoinedVessel  "+
        " ,replace (convert(varchar, Ass.AppraisalRecievedDate,106),' ','-')AppraisalRecievedDate  " +        
        " ,(case when status=1 then 'Open' else 'Verified' end)StatusText,Location " +
        " from tbl_Assessment Ass left join AppraisalOccasion App on Ass.Occasion=App.AppraisalOccasionID " +
        " where Location='" + ((btnOffice.CssClass == "selbtn") ? "O" : "S") + "' ";

        string WhereClause="";
        
        if (ddl_Vessel.SelectedIndex != 0)
        {
            WhereClause = WhereClause + " and VesselCode='"+ddl_Vessel.SelectedValue+"'";
        }
        if (ddlPeepLevel.SelectedIndex != 0)
        {
            WhereClause = WhereClause + " and PeapType="+ddlPeepLevel.SelectedValue+"";
        }
        if (ddlOccation.SelectedIndex != 0)
        {
            WhereClause = WhereClause + " and Occasion=" + ddlOccation.SelectedValue+ "";
        }
        if (txtCrewNo.Text.Trim() != "")
        {
            WhereClause = WhereClause + " and CrewNo='" + txtCrewNo.Text.Trim() + "'";
        }
        if (ddlStatus.SelectedIndex != 0)
        {   
            if(btnOffice.CssClass == "activetab")
            {
                WhereClause = WhereClause + " and Status=" + ddlStatus.SelectedValue + "";
            }
            else
            {
		if(ddlStatus.SelectedValue=="3")
		{
	                WhereClause = WhereClause + " and Status=2 And CrewAppraisalId is not null";
		}
		else if(ddlStatus.SelectedValue=="2")
		{
			WhereClause = WhereClause + " and Status=2 And CrewAppraisalId is null";	
		}
		else
		{
			WhereClause = WhereClause + " and Status=" + ddlStatus.SelectedValue + "";
		}	
            }            
        }
        sql = sql + WhereClause + " order by Ass.AppraisalRecievedDate Desc";
            
        DataSet Ds = Budget.getTable(sql);

        if (Ds != null)
        {
            grdAppairasal.DataSource = Ds;
            grdAppairasal.DataBind();
            if (ddlStatus.SelectedIndex == 0)
            {
                lblNoOfrec.Text = "Total Records : " + Ds.Tables[0].Rows.Count.ToString();
            }
            else if (ddlStatus.SelectedIndex == 1)
            {
                lblNoOfrec.Text = "Total Records to be Verified : " + Ds.Tables[0].Rows.Count.ToString();
            }
            else
            {
                lblNoOfrec.Text = "Total Records : " + Ds.Tables[0].Rows.Count.ToString();
            }
        }

        string sql1= "SELECT CCB.CREWBONUSID,CCB.CREWID,CPD.CREWNUMBER,CCB.CONTRACTID,CCB.ContractRefNumber,CPD.FIRSTNAME + ' ' +CPD.MIDDLENAME + ' ' +CPD.LASTNAME AS CREWNAME ,CCB.RANKID,RANKCODE,CCB.VESSELID,V.VESSELNAME,  " +
                    "ccb.SignOnDate,ccb.SignOffDate, AssMgntID AS PeapId " +
                    "FROM DBO.CREWCONTRACTBONUSMASTER CCB " +
                    "INNER JOIN CREWPERSONALDETAILS CPD ON CCB.CREWID = CPD.CREWID " +
                    "INNER JOIN RANK R ON CCB.RANKID = R.RANKID " +
                    "INNER JOIN VESSEL V ON V.VESSELID = CCB.VESSELID  AND V.VESSELSTATUSID=1 " +
                    "LEFT JOIN tbl_Assessment ON tbl_Assessment.CREWBONUSID = CCB.CREWBONUSID " +
                    "WHERE CCB.STATUS = 'A' and ISNULL(AssMgntID,0)<= 0 and R.RankId in (1,12) and ccb.SignOnDate>='01-jan-2016' ORDER BY V.VESSELNAME,CREWNAME";
        DataTable dt= Common.Execute_Procedures_Select_ByQueryCMS("select * from vw_PendingCrewForAppraisal");

        lblMessage.Text ="( " + dt.Rows.Count.ToString() + " ) Crew has pending appraisal in office. Click here to View";
    }
    private void BindVessel()
    {
        //ProcessGetVessel processgetvessel = new ProcessGetVessel();
        //try
        //{
        //    processgetvessel.Invoke();
        //}
        //catch (Exception ex)
        //{
        //    //Response.Write(ex.Message.ToString());
        //}
        //ddl_Vessel.DataValueField = "VesselId";
        //ddl_Vessel.DataTextField = "VesselName";
        //ddl_Vessel.DataSource = processgetvessel.ResultSet.Tables[0];
        //ddl_Vessel.DataBind();   
        DataSet ds = cls_SearchReliever.getMasterData("Vessel", "VesselCode", "VesselName", Convert.ToInt32(Session["loginid"].ToString()));
        ddl_Vessel.DataValueField = "VesselCode";
        ddl_Vessel.DataTextField = "VesselName";
        ddl_Vessel.DataSource = ds;
        ddl_Vessel.DataBind();
        ddl_Vessel.Items.Insert(0, new ListItem(" Select ", "0"));
    }
    protected void btnFilter_OnClick(object sender, EventArgs e)
    {
        BindRepeater();
    }
    protected void grdAppairasal_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HiddenField hfCrewNo = (HiddenField)e.Row.FindControl("hfCrewNo");
            HiddenField hfFname = (HiddenField)e.Row.FindControl("hfFname");
            HiddenField hfLname = (HiddenField)e.Row.FindControl("hfLname");
            HiddenField hfAssMgntID = (HiddenField)e.Row.FindControl("hfAssMgntID");
            HiddenField hfVesselCode= (HiddenField)e.Row.FindControl("hfVesselCode");
            
            //HtmlImage imgView = (HtmlImage)e.Row.FindControl("imgView");
            HtmlImage imgEdit = (HtmlImage)e.Row.FindControl("imgEdit");
            
            
            Label lblMarking = (Label)e.Row.FindControl("lblMarking");
            Label lblMarkingWhite = (Label)e.Row.FindControl("lblMarkingWhite");
            HiddenField hfLocation = (HiddenField)e.Row.FindControl("hfLocation");

            string sql = " select * from CrewPersonalDetails where CrewNumber='" + hfCrewNo.Value.Trim().Replace("'", "''") + "' and FirstName='" + hfFname.Value.Trim().Replace("'", "''") + "' and lastName='" + hfLname.Value.Trim().Replace("'", "''") + "'";
            DataSet Ds = Budget.getTable(sql);
            if (Ds != null)
            {
                if (Ds.Tables[0].Rows.Count <= 0)
                {
                    lblMarking.Visible = true;
                    //imgEdit.Visible = true && Auth.IsUpdate;

                    imgEdit.Attributes.Add("onclick", "UpdateCrewDetails(" + hfAssMgntID.Value + ",'" + hfVesselCode.Value+ "','" + hfLocation.Value + "')");
                    //imgView.Attributes.Add("onclick", "ShowPeeap(" + hfAssMgntID .Value+ ",'UpdateCrew')");
                    //lblMarkingWhite.Visible = false;
                }
                else
                {
                    //lblMarkingWhite.Visible = true;
                    //imgEdit.Visible = false;
                    //imgView.Attributes.Add("onclick", "ShowPeeap(" + hfAssMgntID.Value + ",'')");
                }
            }
            else
            {
                lblMarking.Visible = true;
                //lblMarkingWhite.Visible = false;
            }
        }
    }
    protected void btnReload_OnClick(object sender, EventArgs e)
    {
        BindRepeater();
    }

    protected void imgView_onclick(object sender, EventArgs e)
    {
        ImageButton imgView = (ImageButton)sender;
        
        HiddenField hfAssMgntID = (HiddenField)imgView.Parent.FindControl("hfAssMgntID");
        GridViewRow clickedRow = (GridViewRow)((ImageButton)sender).NamingContainer;

        HiddenField hfCrewNo = (HiddenField)imgView.Parent.FindControl("hfCrewNo");
        HiddenField hfFname = (HiddenField)imgView.Parent.FindControl("hfFname");
        HiddenField hfLname = (HiddenField)imgView.Parent.FindControl("hfLname");
        HiddenField hfVesselCode = (HiddenField)imgView.Parent.FindControl("hfVesselCode");

        HiddenField hfLocation = (HiddenField)imgView.Parent.FindControl("hfLocation");
        

        HtmlImage imgEdit = (HtmlImage)imgView.Parent.FindControl("imgEdit");
        Label lblMarking = (Label)imgView.Parent.FindControl("lblMarking");
        //Label lblMarkingWhite = (Label)imgView.Parent.FindControl("lblMarkingWhite");

        
        grdAppairasal.SelectedIndex = clickedRow.RowIndex;
            //Attributes.Add("class", "selectedrow");


        string sql = " select * from CrewPersonalDetails where CrewNumber='" + hfCrewNo.Value.Trim().Replace("'", "''") + "' and FirstName='" + hfFname.Value.Trim().Replace("'", "''") + "' and lastName='" + hfLname.Value.Trim().Replace("'", "''") + "'";
            
        DataSet Ds = Budget.getTable(sql);

        if (hfLocation.Value == "O")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "dddd", "ShowPeeap_Office(" + hfAssMgntID.Value + ",'','" + hfVesselCode.Value + "','" + hfLocation.Value + "');", true);//
        }
        else
        { 
        if (Ds != null)
        {
            if (Ds.Tables[0].Rows.Count <= 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "dddd", "ShowPeeap(" + hfAssMgntID.Value + ",'UpdateCrew','" + hfVesselCode.Value + "','" + hfLocation.Value + "');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "dddd", "ShowPeeap(" + hfAssMgntID.Value + ",'','" + hfVesselCode.Value + "','" + hfLocation.Value + "');", true);//
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "dddd", "ShowPeeap(" + hfAssMgntID.Value + ",'','" + hfVesselCode.Value + "','" + hfLocation.Value + "');", true);//

        }
        }
    }
    protected void imgDel_OnClick(object sender, EventArgs e)
    {
        ImageButton imgDel = (ImageButton)sender;
        HiddenField hfAssMgntID = (HiddenField)imgDel.Parent.FindControl("hfAssMgntID");
        HiddenField hfVesselCode = (HiddenField)imgDel.Parent.FindControl("hfVesselCode");
        HiddenField hfLocation = (HiddenField)imgDel.Parent.FindControl("hfLocation");

        string sql = "delete from tbl_assessment where AssMgntID=" + hfAssMgntID.Value + " And VesselCode='" + hfVesselCode.Value + "' and Location='" + hfLocation.Value + "'";
        DataSet DS = Budget.getTable(sql);
        if (DS != null)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "dddd", "alert('Record deleted successfully.');", true);//
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "dddd", "alert('Unable to delete record.');", true);//
        }
        BindRepeater();
    }
    
    //--------------- BONUS BASED ----------------------
    protected void imgAddPeap_OnClick(object sender, EventArgs e)
    {        
        ImageButton img = (ImageButton)sender;
        int CREWBONUSID = Common.CastAsInt32(img.CommandArgument);
        if (CREWBONUSID > 0)
        {
            DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("exec dbo.StartPEAP " + CREWBONUSID);
            if (dt.Rows.Count > 0)
            {
                int PeapId = Common.CastAsInt32(dt.Rows[0][0]);
                string VesselCode = Convert.ToString(dt.Rows[0][1]);
                string Location = Convert.ToString(dt.Rows[0][2]);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", "window.open('../CrewAppraisal/AddPeap.aspx?PeapID=" + PeapId + "&VesselCode=" + VesselCode + "&Location=" + Location + "','');", true);
            }
        }
    }

    //--------------- PRE SIGN OFF BASED ----------------------
    protected void imgAddPeap1_OnClick(object sender, EventArgs e)
    {
        ImageButton img = (ImageButton)sender;
        int CREWID = Common.CastAsInt32(img.CommandArgument);
        if (CREWID > 0)
        {
            DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("exec dbo.StartPEAP1 " + CREWID);
            if (dt.Rows.Count > 0)
            {
                int PeapId = Common.CastAsInt32(dt.Rows[0][0]);
                string VesselCode = Convert.ToString(dt.Rows[0][1]);
                string Location = Convert.ToString(dt.Rows[0][2]);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", "window.open('../CrewAppraisal/AddPeap.aspx?PeapID=" + PeapId + "&VesselCode=" + VesselCode + "&Location=" + Location + "','');", true);
            }
        }
    }

    //------------------------------------------------------------------------------------------------------------------------
    //protected void btnAddPeapPopup_OnClick(object sender, EventArgs e)
    //{
    //    dvAddPeapPopup.Visible = true;
    //    ddlVessel_AP.SelectedIndex = 0;        
    //    ddlOccasion_AP.SelectedIndex = 0;
    //    rptUser_AP.DataSource = null;
    //    rptUser_AP.DataBind();
    //}
    //protected void btnShow_AP_OnClick(object sender, EventArgs e)
    //{
    //    if (ddlVessel_AP.SelectedIndex == 0)
    //    {
    //        lblMsg_AP.Text = "Please select vessle.";
    //        return;
    //    }
    //    if (ddlOccasion_AP.SelectedIndex == 0)
    //    {
    //        lblMsg_AP.Text = "Please select occasaion.";
    //        return;
    //    }
    //    string sql = " select CrewID,CrewNumber,FirstName+' '+MiddleName+' '+LastName as name, r.RankName from crewpersonaldetails CP "+
    //                 "   left join Rank R on r.RankId = CP.currentrankid " +
    //                 "   where(CurrentVesselId = "+ddlVessel_AP.SelectedValue+ " Or LastVesselId =  " + ddlVessel_AP.SelectedValue + " ) and currentrankid in (1,2) ";
    //    DataSet Ds = Budget.getTable(sql);
    //    rptUser_AP.DataSource = Ds;
    //    rptUser_AP.DataBind();
    //}

    //protected void btnClosePopup_Click(object sender, EventArgs e)
    //{
    //    dvAddPeapPopup.Visible = false;
    //}
    //protected void lnkAddPeap_OnClick(object sender, EventArgs e)
    //{
    //    RadioButton lnk = (RadioButton)sender;       
    //    {
    //        dvAddPeapPopup.Visible = false;
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", "window.open('AddPeap.aspx?VesselID=" + ddlVessel_AP.SelectedValue+"&Occasion="+ddlOccasion_AP.SelectedValue+"&CrewNumber="+ lnk.CssClass+ "')", true);
    //    }
    //}

    //public void BindVesselAP()
    //{
    //    DataSet ds = cls_SearchReliever.getMasterData("Vessel", "VesselID", "VesselName", Convert.ToInt32(Session["loginid"].ToString()));
    //    ddlVessel_AP.DataValueField = "VesselID";
    //    ddlVessel_AP.DataTextField = "VesselName";
    //    ddlVessel_AP.DataSource = ds;
    //    ddlVessel_AP.DataBind();
    //    ddlVessel_AP.Items.Insert(0, new ListItem(" Select ", "0"));



    //}

    protected void btnCrewAssessment_Click(object sender, EventArgs e)
    {
        btnOffice.CssClass = "btn1";
        btnShip.CssClass = "btn1";
        btnCrewAssessment.CssClass = "selbtn";
        tblmain.Visible = false;
        dvFrame.Visible = false;
        dvfrm.Visible = true;
        frm.Attributes.Add("src", "~/Modules/HRD/CrewApproval/CrewAssessment.aspx");
    }
}
