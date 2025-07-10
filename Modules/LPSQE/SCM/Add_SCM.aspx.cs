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
using System.Data.SqlClient;
using System.Xml;
using ShipSoft.CrewManager.BusinessLogicLayer;
using ShipSoft.CrewManager.BusinessObjects;
using ShipSoft.CrewManager.Operational;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

public partial class HSSQE_Add_SCM : System.Web.UI.Page
{
    public int SCMID
    {
        get{ return Common.CastAsInt32(ViewState["SCMID"]);}
        set {ViewState["SCMID"]=value;}
    }
    public Authority Auth;
    public DataTable RankTable
    {
        get
        {
            return (DataTable)ViewState["RankTable"];
        }
        set 
        
        {
            ViewState["RankTable"] = value;
        }
    }
    public DataTable A_RankTable
    {
        get
        {
            return (DataTable)ViewState["A_RankTable"];
        }
        set
        {
            ViewState["A_RankTable"] = value;
        }
    }
    //public int SCMID
    //{
    //    set { ViewState["SCMID"] = value; }
    //    get { return int.Parse("0" + ViewState["SCMID"]); }
    //}
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        lblSaveMSG.Text = "";

        //if (Page.Request.QueryString["SCMID"] != null)
        //    SCMID = Common.CastAsInt32(Page.Request.QueryString["SCMID"].ToString());
        //else
        //    return;

        if (!Page.IsPostBack)
        {
            SCMID = Common.CastAsInt32(Request.QueryString["SCMID"]);
            BindVessel();
            lblOccasion.Text = "Suptd. Visit";
            for (int i = 0; i <= 23; i++)
            {
                string part=i.ToString().PadLeft(2,'0');
                ddlCommTime1.Items.Add(new System.Web.UI.WebControls.ListItem(part,part));
                ddlCompTime1.Items.Add(new System.Web.UI.WebControls.ListItem(part, part));
            }

            lblOccasion.Text = "Suptd. Visit";
            for (int i = 0; i <= 59; i++)
            {
                string part=i.ToString().PadLeft(2,'0');
                ddlCommTime2.Items.Add(new System.Web.UI.WebControls.ListItem(part, part));
                ddlCompTime2.Items.Add(new System.Web.UI.WebControls.ListItem(part, part));
            }
            if (SCMID > 0)
            {
                ShowSCMData();
            }
        }
    }
    protected void btnSave_OnClick(object sender, EventArgs e)
    {
        //----------------------------
        if (ddlVessel.SelectedIndex <= 0)
        {
            ShowMessage("Please select vessel");
            ddlVessel.Focus();
            return;
        }
        DateTime dtR;
        if (!DateTime.TryParse(txtRDate.Text, out dtR))
        {
            ShowMessage("Please fill valid date.");
            txtRDate.Focus();
            return;
        }
        else
        {
            if (dtR > DateTime.Today)
            {
                ShowMessage("Please fill valid date ( less than today ).");
                txtRDate.Focus();
                return;
            }
        }
       
        //----------------------------
        string VesselCode = "";
        DataTable dtVsl = Common.Execute_Procedures_Select_ByQuery("SELECT VESSELCODE FROM DBO.VESSEL WHERE VESSELID=" + ddlVessel.SelectedValue);
        if (dtVsl.Rows.Count > 0)
        {
            VesselCode = dtVsl.Rows[0][0].ToString();
        }
        //----------------------------
        if (SCMID <= 0)
        {
            DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM SCM_Master WHERE VESSELCODE='" + VesselCode + "' AND OCASSION='S' AND MONTH(SDATE)=" + dtR.Month.ToString() + " AND YEAR(SDATE)=" + dtR.Year.ToString());
            if (dt.Rows.Count > 0)
            {
                ShowMessage("Record already exists for same month.");
                txtRDate.Focus();
                return;
            }
        }
        try
        {
            Common.Set_Procedures("DBO.SP_INSERT_SCM_MASTER_SUPTD_UPDATED");
            Common.Set_ParameterLength(19);
            Common.Set_Parameters(

                new MyParameter("@SCMID", SCMID),
                new MyParameter("@VESSELCODE", VesselCode),
                new MyParameter("@SDATE", dtR),
                new MyParameter("@ShipPosFrom", txtVoyFrom.Text),
                new MyParameter("@ShipPosTo", txtVoyTo.Text),
                new MyParameter("@COMMTIME", ddlCommTime1.SelectedValue + ":" + ddlCommTime2.SelectedValue),
                new MyParameter("@COMPTIME", ddlCompTime1.SelectedValue + ":" + ddlCompTime2.SelectedValue),
                new MyParameter("@OCASSION", "S"),
                new MyParameter("@SHIPPOSITION", ddlShipPosition.SelectedValue),
                new MyParameter("@Str_MINOFPREVSAGECOM", 0),
                new MyParameter("@Str_ABSPREVSAGECOM", 0),
                new MyParameter("@Str_OFFCOMMPREVSAFECOM", 0),
                new MyParameter("@SUPTD_CompliancewithRegulations", txtSUPTD_CompliancewithRegulations.Text.Trim().Replace(",", "`")),
                new MyParameter("@SUPTD_DeviationsfromSafety", txtSUPTD_DeviationsfromSafety.Text.Trim().Replace(",", "`")),
                new MyParameter("@SUPTD_DetailsofTraining", txtSUPTD_DetailsofTraining.Text.Trim().Replace(",", "`")),
                new MyParameter("@SUPTD_HealthSafetyMeasures", txtSUPTD_HealthSafetyMeasures.Text.Trim().Replace(",", "`")),
                new MyParameter("@SUPTD_Suggestions", txtSUPTD_Suggestions.Text.Trim().Replace(",", "`")),
                new MyParameter("@SUPTD_BPI", txtSUPTD_BPI.Text.Trim().Replace(",", "`")),
                new MyParameter("@SUPTD_AnyOtherTopic", txtSUPTD_AnyOtherTopic.Text.Trim().Replace(",", "`")));

            DataSet dsResult = new DataSet();

            if (Common.Execute_Procedures_IUD(dsResult))
            {
                if (SCMID > 0)
                {
                    Common.Execute_Procedures_Select_ByQuery("DELETE FROM DBO.SCM_RANKDETAILS WHERE SCMID=" + SCMID.ToString());
                }

                SCMID = Convert.ToInt32(dsResult.Tables[0].Rows[0][0]);
                
                foreach (DataRow dr in RankTable.Rows)
                {
                    dsResult.Tables.Clear();
                    Common.Set_Procedures("DBO.SP_INSERT_SCM_RANKDETAILS");
                    Common.Set_ParameterLength(5);
                    Common.Set_Parameters(new MyParameter("@SCMID", SCMID),
                        new MyParameter("@RANKNAME", dr["RANKNAME"]),
                        new MyParameter("@NAME", dr["NAME"]),
                        new MyParameter("@REMARKS", dr["REMARKS"]),
                        new MyParameter("@ABSENT", 0));
                    bool saved=Common.Execute_Procedures_IUD(dsResult);

                }
                foreach (DataRow dr in A_RankTable.Rows)
                {
                    dsResult.Tables.Clear(); 
                    Common.Set_Procedures("DBO.SP_INSERT_SCM_RANKDETAILS");
                    Common.Set_ParameterLength(5);
                    Common.Set_Parameters(new MyParameter("@SCMID", SCMID),
                        new MyParameter("@RANKNAME", dr["RANKNAME"]),
                        new MyParameter("@NAME", dr["NAME"]),
                        new MyParameter("@REMARKS", dr["REMARKS"]),
                        new MyParameter("@ABSENT", 1));
                    bool saved = Common.Execute_Procedures_IUD(dsResult);
                }

                ShowMessage("SCM created/updated successfully"); 
            }
            
        }
        catch ( Exception ex)
        {
            lblSaveMSG.Text = "Unable to create SCM. Error :" + ex.Message;
        }
    }
    protected void btnOpenImport_OnClick(object sender, EventArgs e)
    {
        //----------------------------
        if (ddlVessel.SelectedIndex <= 0)
        {
            ShowMessage("Please select vessel");
            ddlVessel.Focus();
            return; 
        }
        DateTime dtR;
        if (!DateTime.TryParse(txtRDate.Text,out dtR))
        {
            ShowMessage("Please fill valid date.");
            txtRDate.Focus();
            return;
        }
        //----------------------------
        dvImport.Visible = true;

        string sql="SELECT * FROM " +
                   "( " +
                   " SELECT -1 AS RANKID,'SUPERINTENDENT' AS RANKNAME,'' AS NAME ,'Chairman' AS REMARKS,-1 AS RANKLEVEL " +
                   " UNION " +
                   " SELECT RANKID,RANKNAME,FIRSTNAME + ' ' + ISNULL(MIDDLENAME,'') + ' ' + LASTNAME AS [NAME],(CASE WHEN RANKID=1 THEN '' WHEN RANKID=2 THEN 'Safety Officer' ELSE '' END ) AS REMARKS,RANKLEVEL FROM  " +
                   " dbo.get_Crew_History H  " +
                   " INNER JOIN DBO.CREWPERSONALDETAILS C ON H.CREWID=C.CREWID AND H.VESSELID=" + ddlVessel.SelectedValue + " AND ( H.SIGNONDATE <='" + txtRDate.Text + "' AND  (H.SINGOFFDATE >='" + txtRDate.Text + "' OR H.SINGOFFDATE IS NULL) ) " +
                   " INNER JOIN DBO.RANK R ON C.CURRENTRANKID=R.RANKID " +
                   ") A ORDER BY RANKLEVEL ";

        rptImport.DataSource = Common.Execute_Procedures_Select_ByQuery(sql);
        rptImport.DataBind();

        //RankTable = Common.Execute_Procedures_Select_VIMS_ByQuery(sql);
        //rptAttendeeRank.DataSource = RankTable;
        //rptAttendeeRank.DataBind();

        //A_RankTable = Common.Execute_Procedures_Select_VIMS_ByQuery(sql);
        //rptAbsenteeRank.DataSource = A_RankTable;
        //rptAbsenteeRank.DataBind();
    }
    protected void btnCancel_OnClick(object sender, EventArgs e)
    {
        dvImport.Visible = false;
    }
    protected void btnImport_OnClick(object sender, EventArgs e)
    {
        string SuptdName=((TextBox)rptImport.Items[0].FindControl("txt_SName")).Text.Trim();
        if (SuptdName == "")
        {
            ShowMessage("Please enter superintendent name.");
            return;
        }

        //----------------------------
        DataTable dt = new DataTable();
        dt.Columns.Add("RankName");
        dt.Columns.Add("Name");
        dt.Columns.Add("Remarks");

        DataTable dt1 = dt.Clone();

        bool anychecked = false;
        foreach(RepeaterItem ri in rptImport.Items)
        {
            anychecked = ((CheckBox)ri.FindControl("chkSelect")).Checked;
            if (anychecked)
            {
                break;
            }
        }
        if (!anychecked)
        {
            ShowMessage("Please select Crewlist to import.");
            return; 
        }

        foreach (RepeaterItem ri in rptImport.Items)
        {
            anychecked = ((CheckBox)ri.FindControl("chkSelect")).Checked;
            string RankName = ((HiddenField)ri.FindControl("hfd_RankName")).Value;
            string Name = ((HiddenField)ri.FindControl("hfd_Name")).Value;
            string Remarks = ((HiddenField)ri.FindControl("hfd_Remarks")).Value;
            if(Remarks=="")
            {
                Remarks = ((TextBox)ri.FindControl("txtRemarks")).Text.Trim();
            }
            if (anychecked)
            {
                dt.Rows.Add(dt.NewRow());
                dt.Rows[dt.Rows.Count - 1]["RankName"] = RankName;
                if (RankName.Trim() == "SUPERINTENDENT") { Name = SuptdName; }
                dt.Rows[dt.Rows.Count - 1]["Name"] = Name;
                dt.Rows[dt.Rows.Count - 1]["Remarks"] = Remarks;
            }
            else
            {
                dt1.Rows.Add(dt1.NewRow());
                dt1.Rows[dt1.Rows.Count - 1]["RankName"] = RankName;
                if (RankName.Trim() == "SUPERINTENDENT") { Name = SuptdName; }
                dt1.Rows[dt1.Rows.Count - 1]["Name"] = Name;
                dt1.Rows[dt1.Rows.Count - 1]["Remarks"] = Remarks;
            }
        }
        
        RankTable = dt;
        rptAttendeeRank.DataSource = RankTable;
        rptAttendeeRank.DataBind();

        A_RankTable = dt1;
        rptAbsenteeRank.DataSource = A_RankTable;
        rptAbsenteeRank.DataBind();

        dvImport.Visible = false;  
    }
    public void ShowMessage( string Msg)
    {
        Msg = Msg.Replace("'", "`");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "q", "alert('" + Msg + "');", true);
    }
    protected void btnPrint_OnClick(object sender, EventArgs e)
    {
        //Response.Redirect("~/Reports/SCM.aspx?SCMID="+SCMID.ToString()+"");
    }
    public void BindVessel()
    {
        string sql = "SELECT VesselId,Vesselname FROM DBO.Vessel v Where vesselstatusid<>2  ORDER BY VESSELNAME";
        ddlVessel.DataSource = VesselReporting.getTable(sql);
        ddlVessel.DataTextField = "Vesselname";
        ddlVessel.DataValueField = "VesselId";
        ddlVessel.DataBind();
        ddlVessel.Items.Insert(0, new System.Web.UI.WebControls.ListItem("<--Select-->", "0"));
    }
    public void ShowSCMData()
    {
        string sql = "select *,Year(SDate )SCMYear ,upper(VesselCode +'-'+right( Replace(convert(varchar,SDate ,106),' ','-'),8))SCMNo,replace(convert(varchar, SDate,106),' ','-') SDate1,(case Ocassion when 'M' then 'Monthly' else 'SUPTD Visit' end ) OccasionName " +
                " ,(select VesselId from dbo.Vessel V where V.VesselCode=SCM_Master.VesselCode) as VesselId,sdate,ShipPosition " +
                " ,UpdatedBy +' / '+ Replace( convert(varchar,UpdatedOn,106),' ','-') UpdateByOn " +
                " from dbo.SCM_Master  where SCMID=" + SCMID;
        DataTable DT = Common.Execute_Procedures_Select_ByQuery(sql);
        if (DT.Rows.Count > 0)
        {
            DataRow Dr = DT.Rows[0];
            ddlVessel.SelectedValue = Dr["VesselId"].ToString();
            txtRDate.Text = Common.ToDateString(Dr["SDate"]);
            ddlShipPosition.SelectedValue = Dr["ShipPosition"].ToString();
            lblOccasion.Text = Dr["OccasionName"].ToString();
            ddlCommTime1.SelectedValue = Dr["COMMTIME"].ToString().Split(':')[0].ToString();
            ddlCommTime2.SelectedValue = Dr["COMMTIME"].ToString().Split(':')[1].ToString();

            ddlCompTime1.SelectedValue = Dr["COMPTIME"].ToString().Split(':')[0].ToString();
            ddlCompTime2.SelectedValue = Dr["COMPTIME"].ToString().Split(':')[1].ToString();

            if (Dr["ShipPosition"].ToString() == "S")
            {
                //lblPlaceLabel.Text = "Voy From/To ";
                txtVoyFrom.Text = Dr["ShipPosFrom"].ToString();
                txtVoyTo.Text = Dr["ShipPosTo"].ToString();

            }
            else
            {
                //lblPlaceLabel.Text = "Port Anchorage ";
                txtVoyFrom.Text = Dr["ShipPosFrom"].ToString();
            }

            
            // SUPTD ------------------------------------------------
            txtSUPTD_CompliancewithRegulations.Text = Dr["SUPTD_CompliancewithRegulations"].ToString();
            txtSUPTD_DeviationsfromSafety.Text = Dr["SUPTD_DeviationsfromSafety"].ToString();
            txtSUPTD_DetailsofTraining.Text = Dr["SUPTD_DetailsofTraining"].ToString();
            txtSUPTD_HealthSafetyMeasures.Text = Dr["SUPTD_HealthSafetyMeasures"].ToString();
            txtSUPTD_Suggestions.Text = Dr["SUPTD_Suggestions"].ToString();
            txtSUPTD_BPI.Text = Dr["SUPTD_BPI"].ToString();
            txtSUPTD_AnyOtherTopic.Text = Dr["SUPTD_AnyOtherTopic"].ToString();
        }

        sql = "select RankName,Name,Remarks  from dbo.SCM_rankdetails where SCMID=" + SCMID + " and Absent=0 ";
        DataTable dtS1=Common.Execute_Procedures_Select_ByQuery(sql);

        sql = "select RankName,Name from dbo.SCM_rankdetails where SCMID=" + SCMID + " and Absent=1 ";
        DataTable dtS2=Common.Execute_Procedures_Select_ByQuery(sql);
        
        //----------------------------
        DataTable dt = new DataTable();
        dt.Columns.Add("RankName");
        dt.Columns.Add("Name");
        dt.Columns.Add("Remarks");
        DataTable dt1 = dt.Clone();

        foreach(DataRow dr in dtS1.Rows)
        {
            dt.Rows.Add(dt.NewRow());
            dt.Rows[dt.Rows.Count - 1]["RankName"] = dr["RankName"];
            dt.Rows[dt.Rows.Count - 1]["Name"] =  dr["Name"];
            dt.Rows[dt.Rows.Count - 1]["Remarks"] = dr["Remarks"];
        }

        foreach(DataRow dr in dtS2.Rows)
        {
            dt1.Rows.Add(dt1.NewRow());
            dt1.Rows[dt1.Rows.Count - 1]["RankName"] = dr["RankName"];
            dt1.Rows[dt1.Rows.Count - 1]["Name"] =  dr["Name"];
            dt1.Rows[dt1.Rows.Count - 1]["Remarks"] = "";
        }

        RankTable = dt;
        rptAttendeeRank.DataSource = RankTable;
        rptAttendeeRank.DataBind();

        A_RankTable = dt1;
        rptAbsenteeRank.DataSource = A_RankTable;
        rptAbsenteeRank.DataBind();

        dvImport.Visible = false; 

    }
    // ------------ Function - ( ShowSCMData ) ------------ 
    //public void ShowSCMData()
    //{
    //    //OutStandingItems 
    //    string sql = "select *,Year(SDate )SCMYear ,upper(VesselCode +'-'+right( Replace(convert(varchar,SDate ,106),' ','-'),8))SCMNo,replace(convert(varchar, SDate,106),' ','-') SDate1,(case Ocassion when 'M' then 'Monthly' else 'SUPTD Visit' end ) OccasionName " +
    //            " ,(select VesselName from dbo.Vessel V where V.VesselCode=SCM_Master.VesselCode)VesselName "+
    //            " ,(Case ShipPosition when 'S' then 'At Sea' else 'In Port' end)ShipPosition1 ,UpdatedBy +' / '+ Replace( convert(varchar,UpdatedOn,106),' ','-') UpdateByOn " +
    //            " from dbo.SCM_Master  where SCMID=" + SCMID;
    //    DataTable DT = Common.Execute_Procedures_Select_VIMS_ByQuery(sql);
    //    if (DT.Rows.Count > 0)
    //    {
    //        DataRow Dr = DT.Rows[0];


    //        lblNANo.Text = Dr["SCMNo"].ToString();
    //        lblSCMYear.Text = Dr["SCMYear"].ToString();


    //        lblVessel.Text = Dr["VesselCode"].ToString();
    //        lblVesselName.Text = Dr["VesselName"].ToString();
    //        lblOccasion.Text = Dr["OccasionName"].ToString();
            
    //        lblDate.Text = Dr["SDate1"].ToString();
    //        lblTimeCommenced.Text = Dr["CommTime"].ToString();

    //        lblShipPosition.Text = Dr["ShipPosition1"].ToString();
    //        if (Dr["ShipPosition"].ToString() == "S")
    //        {
    //            lblPlaceLabel.Text = "Voy From/To ";

    //            lblShipFromTo.Text = Dr["ShipPosFrom"].ToString() + " To " + Dr["ShipPosTo"].ToString();
                
    //        }
    //        else
    //        {
    //            lblPlaceLabel.Text = "Port Anchorage ";
    //            lblShipFromTo.Text = Dr["ShipPosFrom"].ToString();
    //        }
            
    //        lblTimeCompleted.Text = Dr["CompTime"].ToString();

    //        // SUPTD ------------------------------------------------
    //        txtSUPTD_CompliancewithRegulations.Text = Dr["SUPTD_CompliancewithRegulations"].ToString();
    //        txtSUPTD_DeviationsfromSafety.Text = Dr["SUPTD_DeviationsfromSafety"].ToString();
    //        txtSUPTD_DetailsofTraining.Text = Dr["SUPTD_DetailsofTraining"].ToString();
    //        txtSUPTD_HealthSafetyMeasures.Text = Dr["SUPTD_HealthSafetyMeasures"].ToString();
    //        txtSUPTD_Suggestions.Text = Dr["SUPTD_Suggestions"].ToString();
    //        txtSUPTD_AnyOtherTopic.Text = Dr["SUPTD_AnyOtherTopic"].ToString();
    //    }
        
    //    sql = "select RankName,Name,Remarks  from dbo.SCM_rankdetails where SCMID=" + SCMID + " and Absent=0 ";
    //    rptAttendeeRank.DataSource = Common.Execute_Procedures_Select_VIMS_ByQuery(sql);
    //    rptAttendeeRank.DataBind();

    //    sql = "select RankName,Name from dbo.SCM_rankdetails where SCMID=" + SCMID + " and Absent=1 ";
    //    rptAbsenteeRank.DataSource = Common.Execute_Procedures_Select_VIMS_ByQuery(sql);
    //    rptAbsenteeRank.DataBind();

    //}
}

