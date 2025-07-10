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

public partial class CrewAppraisal_OficeCrewAppraisal : System.Web.UI.Page
{
    public AuthenticationManager Auth;
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //lblMsg_AP.Text = "";
        //-----------------------------
        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 268);
        if (chpageauth <= 0)
        {
            Response.Redirect("Dummy3.aspx");

        }
        //*******************
        //*******************
        Auth = new AuthenticationManager(268, Convert.ToInt32(Session["loginid"].ToString()), ObjectType.Page);
        

        lblMessage.Text = "";
        if (!Page.IsPostBack)
        {
            BindVessel();
            ddlStatus.SelectedIndex = 1;
            BindRepeater();
            //BindVesselAP();
        }
    }
    protected void btnclose_Click(object sender, EventArgs e)
    {
        dvAdd.Visible = false;

    }
    public void BindRepeater()
    {

   

        //string sql = "select Ass.status,App.AppraisalOccasionName as Occasion ,Ass.AssMgntID,Ass.AssName, Ass.AssLname, (Ass.AssName+' '+Ass.AssLname)AssNameMod,Ass.rank ,Ass.VesselCode,Ass.CrewNo   "+
        //" ,(case when Ass.PeapType=1 then 'MANAGEMENT' when Ass.PeapType=2 then 'SUPPORT' when Ass.PeapType=3 then 'OPERATION' end )PeapType   "+
        //" ,(select RankCode from Rank R where R.RankID in((select CurrentrankID from CrewPersonalDetails  CD where CD.CrewNumber=Ass.CrewNo))) ShipSoftRank" +
        //" ,replace (convert(varchar, Ass.AppraisalFromDate,106),' ','-')AppraisalFromDate    "+
        //" ,replace (convert(varchar, Ass.AppraisalToDate ,106),' ','-')AppraisalToDate    "+
        //" ,replace (convert(varchar, Ass.DatejoinedComp,106),' ','-')DatejoinedComp    "+
        //" ,replace (convert(varchar, Ass.DatejoinedVessel ,106),' ','-')DatejoinedVessel  "+
        //" ,replace (convert(varchar, Ass.AppraisalRecievedDate,106),' ','-')AppraisalRecievedDate  " +        
        //" ,(case when status=1 then 'Open' else 'Verified' end)StatusText,Location " +
        //" from tbl_Assessment Ass left join AppraisalOccasion App on Ass.Occasion=App.AppraisalOccasionID " +
        //" where Location='O' ";

        string sql = "SELECT CCB.CREWBONUSID,CCB.CREWID,CPD.CREWNUMBER,CCB.CONTRACTID,CCB.ContractRefNumber,CPD.FIRSTNAME + ' ' +CPD.MIDDLENAME + ' ' +CPD.LASTNAME AS CREWNAME ,CCB.RANKID,RANKCODE,CCB.VESSELID,V.VESSELNAME, BonusApproved,ccb.SignOnDate,ccb.SignOffDate " +
                   " ,(case when Ass.PeapType=1 then 'MANAGEMENT' when Ass.PeapType=2 then 'SUPPORT' when Ass.PeapType=3 then 'OPERATION' end )PeapType   " +
                   " ,replace (convert(varchar, Ass.AppraisalFromDate,106),' ','-')AppraisalFromDate    " +
                   " ,replace (convert(varchar, Ass.AppraisalToDate ,106),' ','-')AppraisalToDate    " +
                   " ,replace (convert(varchar, Ass.DatejoinedComp,106),' ','-')DatejoinedComp    " +
                   " ,replace (convert(varchar, Ass.DatejoinedVessel ,106),' ','-')DatejoinedVessel  " +
                   " ,replace (convert(varchar, Ass.AppraisalRecievedDate,106),' ','-')AppraisalRecievedDate  " +
                   " ,(case when isnull(Ass.status,0)=2 then 'Verified' else 'Open' end)StatusText,Location,V.VESSELCODE,CS.CREWSTATUSNAME, " +
                  "ISNULL(Ass.AssMgntID, 0) AS AssMgntID " +
                  "FROM CREWCONTRACTBONUSMASTER CCB " +
                  "INNER JOIN CREWPERSONALDETAILS CPD ON CCB.CREWID = CPD.CREWID " +
                  "INNER JOIN RANK R ON CCB.RANKID = R.RANKID " +
		  "INNER JOIN CREWSTATUS CS ON CS.CREWSTATUSID= CPD.CREWSTATUSID " +
                  "INNER JOIN VESSEL V ON V.VESSELID = CCB.VESSELID " +
                  "LEFT JOIN tbl_Assessment Ass ON Ass.CREWBONUSID = CCB.CREWBONUSID " +
                  "left join AppraisalOccasion App on Ass.Occasion=App.AppraisalOccasionID " +
                  "WHERE CCB.STATUS = 'A' and CurrentRankId in (1,12) AND cs.crewstatusid in (2,3) and ccb.SignOffDate>'31-dec-2015'";

        string WhereClause="";

        if (ddl_Vessel.SelectedIndex != 0)
        {
            WhereClause = WhereClause + " and V.VesselCode='"+ddl_Vessel.SelectedValue+"'";
        }
        if (ddlPeepLevel.SelectedIndex != 0)
        {
            WhereClause = WhereClause + " and cs.crewstatusid="+ddlPeepLevel.SelectedValue+"";
        }
        if (ddlOccation.SelectedIndex != 0)
        {
            WhereClause = WhereClause + " and Ass.Occasion=" + ddlOccation.SelectedValue+ "";
        }
        if (txtCrewNo.Text.Trim() != "")
        {
            WhereClause = WhereClause + " and CPD.CREWNUMBER='" + txtCrewNo.Text.Trim() + "'";
        }
        if (ddlStatus.SelectedIndex != 0)
        {   
            if(ddlStatus.SelectedValue=="2")
                WhereClause = WhereClause + " and isnull(Ass.status,0)=" + ddlStatus.SelectedValue+" ";
            else
                WhereClause = WhereClause + " and isnull(Ass.status,0)<>2 ";

        }
        sql = sql + WhereClause + " order by CCB.CREWBONUSID Desc";
            
        DataTable Ds = Common.Execute_Procedures_Select_ByQueryCMS(sql);

        if (Ds != null)
        {
            grdAppairasal.DataSource = Ds;
            grdAppairasal.DataBind();
            lblNoOfrec.Text = " ( " + Ds.Rows.Count.ToString() + " ) Records Found.";            
        }
        
    }
    private void BindVessel()
    {
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
        HiddenField hfVSL = (HiddenField)imgView.Parent.FindControl("hfVSL");
        
        int AssmgntId = Common.CastAsInt32(hfAssMgntID.Value);
        int ccbid = Common.CastAsInt32(imgView.CommandArgument);
        if (AssmgntId > 0)
        {
            string script="window.open('AddPeap.aspx?PeapID=" + AssmgntId + "&VesselCode=" + hfVSL.Value + "&Location=O','');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "dddd", script, true);
        }
        else
        {
            int CREWBONUSID = Common.CastAsInt32(hfdcbid.Text);
            DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("exec dbo.StartPEAP " + ccbid);
            if (dt.Rows.Count > 0)
            {
                int PeapId = Common.CastAsInt32(dt.Rows[0][0]);
                string VesselCode = Convert.ToString(dt.Rows[0][1]);
                string Location = Convert.ToString(dt.Rows[0][2]);
                string script = "window.open('AddPeap.aspx?PeapID=" + PeapId + "&VesselCode=" + VesselCode + "&Location=O','');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "dddd", script, true);
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
            lblMessage.Text = "Record deleted successfully.";
        }
        else
        {
            lblMessage.Text = "Unable to delete record.";
        }
        BindRepeater();
    }

    //------------------------------------------------------------------------------------------------------------------------
    protected void btnAddPeapPopup_OnClick(object sender, EventArgs e)
    {
        string sql = "SELECT CCB.CREWBONUSID,CCB.CREWID,CPD.CREWNUMBER,CCB.CONTRACTID,CCB.ContractRefNumber,CPD.FIRSTNAME + ' ' +CPD.MIDDLENAME + ' ' +CPD.LASTNAME AS CREWNAME ,CCB.RANKID,RANKCODE,CCB.VESSELID,V.VESSELNAME, BonusApproved,ccb.SignOnDate,ccb.SignOffDate, " +
                   "ISNULL(A.AssMgntID, 0) AS AssMgntID " +
                   "FROM CREWCONTRACTBONUSMASTER CCB " +
                   "INNER JOIN CREWPERSONALDETAILS CPD ON CCB.CREWID = CPD.CREWID " +
                   "INNER JOIN RANK R ON CCB.RANKID = R.RANKID " +
                   "INNER JOIN VESSEL V ON V.VESSELID = CCB.VESSELID " +
                   "LEFT JOIN tbl_Assessment A ON A.CREWBONUSID = CCB.CREWBONUSID " +
                   "WHERE CCB.STATUS = 'A' and CurrentRankId in (1,12) and ISNULL(A.AssMgntID, 0)<=0 ";


        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        rprData1.DataSource = dt;

        dvAdd.Visible = true;
    }
    protected void btnSelectRow_Click(object sender, EventArgs e)
    {
        //int CREWBONUSID = Common.CastAsInt32(hfdcbid.Text);
        //DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("exec dbo.StartPEAP " + CREWBONUSID);
        //if (dt.Rows.Count > 0)
        //{
        //    int PeapId = Common.CastAsInt32(dt.Rows[0][0]);
        //    string VesselCode = Convert.ToString(dt.Rows[0][1]);
        //    string Location = Convert.ToString(dt.Rows[0][2]);
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", "window.open('../CrewAppraisal/AddPeap.aspx?PeapID=" + PeapId + "&VesselCode=" + VesselCode + "&Location=" + Location + "','');", true);
        //}
    }

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
}
