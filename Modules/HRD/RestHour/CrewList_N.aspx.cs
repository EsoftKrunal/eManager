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
using System.Text;
using System.IO;
using System.Net;
using System.Net.Mail;

public partial class CrewList_N : System.Web.UI.Page
{
    int UserId = 0;

    public int VesselID
    {
        set { ViewState["VesselID"] = value; }
        get 
        {
            return Common.CastAsInt32(ViewState["VesselID"]);
        }
    }
    public string VesselCode
    {
        set { ViewState["vesselcode"] = value; }
        get
        {
            return Convert.ToString(ViewState["vesselcode"]);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        //-------------------------
        lblMsg.Text = "";
        if (!Page.IsPostBack)
        {
            txtDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
            Session["MM"] = "RH";
            Session["SM"] = "RH1";
            BindYear();
            LoadFleet();
            ddlMonthTO.SelectedValue = DateTime.Today.Month.ToString();

		ddlMonth.SelectedValue = DateTime.Today.Month.ToString();
            ddlMonthFrom.SelectedValue = "1";
            //ddlYear.SelectedValue = DateTime.Today.Year.ToString();
            //ShowCrewPortalUser();
            
            BindReason();
            ShowVesselList();

        }
    }
    // Event ----------------------------------------------------------------------------------------------------
    protected void lnkCrewNumber_OnClick(object sender, EventArgs e)
    {
        DateTime FromDate = Convert.ToDateTime("1-" + ddlMonthFrom.SelectedItem.Text + "-" + ddlYearFrom.SelectedValue + "");
        DateTime TODate = FromDate.AddMonths(1).AddDays(-1);
        LinkButton btn=(LinkButton )sender;        
        string CrewNumber = btn.CommandArgument.ToString();
        string RedirectPage = "RestHourEntry_N.aspx?c=" + CrewNumber + "&t=" + btn.CssClass + "&v=" + btn.Attributes["vessel"] + "&m=" + ddlMonth.SelectedValue + "&y=" + ddlYear.SelectedValue + "&vsc=" + VesselCode + "&CrewNumber=" + btn.Text;
        //Response.Redirect(RedirectPage);
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "opn", "window.open("+ RedirectPage + ")"  , true);
        ClientScript.RegisterStartupScript(this.GetType(), "", "<script> window.open('" + RedirectPage + "') </script>");
    }
    protected void Show_Click(object sender, EventArgs e)
    {
        ShowVesselList();
    }
    protected void btnRHStatus_OnClick(object sender, EventArgs e)
    {
        string RedirectPage = "RHMissingLog.aspx?v=" + VesselCode + "&m=" + ddlMonth.SelectedValue + "&y=" + ddlYear.SelectedValue;
        ClientScript.RegisterStartupScript(this.GetType(), "", "<script> window.open('" + RedirectPage + "',''); </script>");
    }
    
    protected void btnShow11_OnClick(object sender, EventArgs e)
    {
        lblMn.Text = txtDate.Text.Substring(3);
        ShowCrewPortalUser();
    }
    protected void lnlVessel_OnClick(object sender, EventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        HiddenField hfVesselCode = (HiddenField)btn.Parent.FindControl("hfVesselCode");
        VesselID = Common.CastAsInt32(btn.CommandArgument);
        VesselCode = hfVesselCode.Value;
        //lblSelVessel.Text = btn.Text +" as on "+GetMonthName( ddlMonthFrom.SelectedValue)+"-"+ddlYearFrom.SelectedValue+"  To  "+GetMonthName(ddlMonthTO.SelectedValue)+"-"+ddlYearTO.SelectedValue+"";

        lblSelVessel.Text = btn.ToolTip + " ";
        ShowCrewPortalUser();
        tblSearchPanel.Visible = false;
    }
    protected void btnBack_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("CrewList_N.aspx");
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ddlFleet.SelectedIndex = 0;        
        ddlReason.SelectedIndex = 0;
        ddlMonthTO.SelectedValue = DateTime.Today.Month.ToString();
        ddlMonthFrom.SelectedValue = DateTime.Today.Month.ToString();
        ShowVesselList();

    }
    // Functon --------------------------------------------------------------------------------------------------------
    public void ShowCrewPortalUser()
    {




        divCrewPortalUser.Visible = true;
        divVesselList.Visible = false;
        
        DateTime FromDate = Convert.ToDateTime("01-" + ddlMonth.SelectedItem.Text + "-" + ddlYear.SelectedValue);
        DateTime TODate = FromDate.AddMonths(1);

        int Rank=Common.CastAsInt32(Session["RankId"]);
 
        string sql = " SELECT v.VesselCode,h.CrewId,h.VesselId,h.ContractId,cpd.CrewNumber,cpd.DateofBirth as DOB,cpd.DATEFIRSTJOIN as DJC " +
              "  ,cpd.FirstName + ' ' + cpd.MiddleName + ' ' + cpd.LastName as CrewName,h.NewRankId AS RankId " +
              " ,(select RankCode from rank R where R.RankID=h.NewRankId)RankName " +
              " ,replace(convert(varchar,h.SignOnDate,106),' ','-')sSignOnDate " +
              " ,replace(convert(varchar,h.SingOffDate,106),' ','-')sSignOffDate,1 As WatchKeeper " +
              "  ,CASE WHEN CrewStatusId = 3 THEN cpd.ReliefDueDate ELSE NULL END AS ReliefDueDate " +
              "  , (SELECT COUNT(1) FROM dbo.RH_CrewMonthData WHERE VesselCode = v.VesselCode AND CrewNumber = cpd.CrewNumber AND ForDate< (CASE WHEN h.SingOffDate<'" + TODate.ToString("dd-MMM-yyyy") + "' then dateadd(day,1,h.SingOffDate) else '" + TODate.ToString("dd-MMM-yyyy") + "' end) AND ForDate>= (CASE WHEN h.SignOnDate>'" + FromDate.ToString("dd-MMM-yyyy") + "' then h.SignOnDate else '" + FromDate.ToString("dd-MMM-yyyy") + "' end) ) as ME " +
              "  ,(SELECT  case when Count(1)>=2 then 'block' else 'none' end  nc_cnt FROM DBO.RH_NCList where VesselCode='" + VesselCode + "' and CrewNumber=cpd.CrewNumber and month(FORDATE)="+ddlMonth.SelectedValue+" and year(FORDATE)="+ddlYear.SelectedValue+")as nc_cnt" +
              "  ,dbo.fn_Get_DaysRequired(h.SignOnDate,h.SingOffDate,'" + FromDate + "', '" + TODate.AddDays(-1) + "')DaysRequired " +              
              "  FROM get_Crew_History h " +
              "  inner join vessel v on h.VesselId = v.VesselId " +
              "  inner join CrewPersonalDetails cpd on h.crewid = cpd.crewid  "+
              " where h.SignOnDate<'"+ TODate.ToString("dd-MMM-yyyy")+ "' and  (h.SingOffDate is null or h.SingOffDate >='" + FromDate.ToString("dd-MMM-yyyy") + "') ";              


        string WhereClause = "";
        //-----------------------------
        if (VesselID > 0)
        {
            WhereClause += " AND v.VESSELID=" + VesselID;
        }
    
        string OrderBy = " ORDER BY (select RankLevel from rank R where R.RankID=h.NewRankId)";

        sql = sql + WhereClause + OrderBy;
//Response.Write(sql);
        DataTable Dt = Common.Execute_Procedures_Select_ByQueryCMS(sql );
        rptCrewPortalUser.DataSource = Dt;
        rptCrewPortalUser.DataBind();
    }
    public string GetCountNewNC(object CREWID,object VESSELID)
    {
        string ss="";
        DataTable DT = Common.Execute_Procedures_Select_ByQueryCMS("SELECT DBO.WC_GetCountNewNC(" + CREWID + "," + VESSELID + ",'" + txtDate.Text + "')");
        ss = DT.Rows[0][0].ToString();
        return ss;
    }
    public void ShowVesselList()
    {
        divCrewPortalUser.Visible = false;
        divVesselList.Visible = true;

        String FleetWhereClause = "";
        if (ddlFleet.SelectedIndex != 0)
            FleetWhereClause = " and V.FleetID=" + ddlFleet.SelectedValue ;
        
        
        FleetWhereClause = FleetWhereClause +" Order by VesselName";

        String Sql = "select Row_Number() over(order by VesselName )Row,VesselID,VesselCode,VesselName, " +
                     "(case when exists(select top 1 * from cp_crewdailyworkresthours c where c.vesselid=V.vesselid) then 'green' else 'black' end ) as VColor, " +
                     "(SELECT TOP 1 RECDON FROM CP_VesselRestHourPackets D WHERE D.VESSELCODE=V.VESSELCODE ORDER BY RECDON DESC) AS LASTUPDATE," +
                     "(select top 1 RecievedOn from dbo.RH_OfficePacketRecieved where VesselCode=V.VESSELCODE and PacketType='Month-Packet' order by RecievedOn Desc) AS LASTUPDATE_New" +

                     " from Vessel V where V.VesselStatusid=1 and ISNULL(FleetID,0)>0 and VesselId in (Select VesselId from UserVesselRelation with(nolock) where Loginid = " + Convert.ToInt32(Session["loginid"].ToString()) + ") " + FleetWhereClause;

        DataTable Dt = Common.Execute_Procedures_Select_ByQueryCMS(Sql);
        rptVesselList.DataSource = Dt;
        rptVesselList.DataBind();

    }
    public string getCountVerified(string VesselId)
    {
        DateTime FromDate = Convert.ToDateTime("1-" + ddlMonthFrom.SelectedItem.Text + "-" + ddlYearFrom.SelectedValue + "");
        DateTime TODate = Convert.ToDateTime("1-" + ddlMonthTO.SelectedItem.Text + "-" + ddlYearTO.SelectedValue + "").AddMonths(1).AddDays(-1);
        string strFrm, strTo;
        strFrm = "'" + FromDate.ToString("dd-MMM-yyyy") + "'";
        strTo = "'" + TODate.ToString("dd-MMM-yyyy") + "'";

        string Reasion = "";
        if (ddlReason.SelectedIndex != 0)
            Reasion = " and exists(select * from dbo.CP_NonConformanceReason NCR where NCR.VesselID=c.VesselID and NCR.NCDate=c.NCDate and NCR.Reason=" + ddlReason.SelectedValue + ")";

        string Sql = " select distinct c.vesselid,c.crewid,c.ncdate,c.nctype from CP_NONCONFORMANCE c inner join CP_NONCONFORMANCE_verification v1 on c.vesselid=v1.vesselid and c.crewid=v1.crewid and c.ncdate=v1.ncdate and c.nctype=v1.nctype where c.nctype<>7 and c.VesselId in (Select VesselId from UserVesselRelation with(nolock) where Loginid = " + Convert.ToInt32(Session["loginid"].ToString()) + ")  and c.NCDate>=" + strFrm + "  and c.NCDate<=" + strTo + " and c.VesselID=" + VesselId + Reasion + " " +
                     " union " +
                     " select distinct c.vesselid,c.crewid,c.ncdate,c.nctype from CP_NONCONFORMANCE c inner join CP_NONCONFORMANCE_verification v1 on c.vesselid=v1.vesselid and c.crewid=v1.crewid and c.ncdate=v1.ncdate and c.nctype=v1.nctype where c.nctype=7 and c.VesselId in (Select VesselId from UserVesselRelation with(nolock) where Loginid = " + Convert.ToInt32(Session["loginid"].ToString()) + ")  and c.NCDate>=" + strFrm + " and c.NCDate<=" + strTo + " and c.VesselID=" + VesselId + Reasion + " ";
        DataTable Dt = Common.Execute_Procedures_Select_ByQueryCMS(Sql);
        if (Dt.Rows.Count > 0)
        {
            return Dt.Rows.Count.ToString();
        }
        else 
        {
            return "";
        }

    }
    public string getCountUnVerified(string VesselId)
    {
        int cnt = Common.CastAsInt32(getCountTotal(VesselId)) - Common.CastAsInt32(getCountVerified(VesselId));
        if (cnt > 0)
            return cnt.ToString();
        else
            return "";
    }
    public string getCountTotal(string VesselId)
    {
        DateTime FromDate = Convert.ToDateTime("1-" + ddlMonthFrom.SelectedItem.Text + "-" + ddlYearFrom.SelectedValue + "");
        DateTime TODate = Convert.ToDateTime("1-" + ddlMonthTO.SelectedItem.Text + "-" + ddlYearTO.SelectedValue + "").AddMonths(1).AddDays(-1);
        string strFrm, strTo;
        strFrm = "'" + FromDate.ToString("dd-MMM-yyyy") + "'";
        strTo = "'" + TODate.ToString("dd-MMM-yyyy") + "'";

        string Reasion = "";
        if (ddlReason.SelectedIndex != 0)
            Reasion = " and exists(select * from dbo.CP_NonConformanceReason NCR where NCR.VesselID=c.VesselID and NCR.NCDate=c.NCDate and NCR.Reason=" + ddlReason.SelectedValue + ")";

        string Sql = " select distinct c.vesselid,c.crewid,c.ncdate,c.nctype from CP_NONCONFORMANCE c where c.nctype<>7 and c.VesselId in (Select VesselId from UserVesselRelation with(nolock) where Loginid = " + Convert.ToInt32(Session["loginid"].ToString()) + ") and c.NCDate>=" + strFrm + " 	and c.NCDate<=" + strTo + " and c.VesselID=" + VesselId + Reasion + "" +
                     " union " +
                     " select distinct c.vesselid,c.crewid,c.ncdate,c.nctype from CP_NONCONFORMANCE c where c.nctype=7 and c.VesselId in (Select VesselId from UserVesselRelation with(nolock) where Loginid = " + Convert.ToInt32(Session["loginid"].ToString()) + ")  and c.NCDate>=" + strFrm + " 	and c.NCDate<=" + strTo + " and c.VesselID=" + VesselId + Reasion + "";

        DataTable Dt = Common.Execute_Procedures_Select_ByQueryCMS(Sql);
        if (Dt.Rows.Count > 0)
        {
            return Dt.Rows.Count.ToString();
        }
        else
        {
            return "";
        }
     
    }
    public string getCountNC(string VesselId,int Type)
    {
        DateTime FromDate = Convert.ToDateTime("1-" + ddlMonthFrom.SelectedItem.Text + "-" + ddlYearFrom.SelectedValue + "");
        DateTime TODate = Convert.ToDateTime("1-" + ddlMonthTO.SelectedItem.Text + "-" + ddlYearTO.SelectedValue + "").AddMonths(1).AddDays(-1);
        string strFrm, strTo;
        strFrm = "'" + FromDate.ToString("dd-MMM-yyyy") + "'";
        strTo = "'" + TODate.ToString("dd-MMM-yyyy") + "'";

        string Reasion = "";
        if (ddlReason.SelectedIndex != 0)
            Reasion = " and exists(select * from dbo.CP_NonConformanceReason NCR where NCR.VesselID=c.VesselID and NCR.NCDate=c.NCDate and NCR.Reason=" + ddlReason.SelectedValue + ")";

        string JoinSQL="";
        string WhereSQL = "";
        if (chkUVNC.Checked)
        {
            JoinSQL = " left join CP_NonConformance_Verification v on c.vesselid=v.vesselid and c.crewid=v.crewid and c.ncdate=v.ncdate and c.nctype=v.nctype";
            WhereSQL = " and V.VERIFIEDBY IS NULL" ;
        }

        string sql="";
        if(Type==1)
            sql = "select distinct c.vesselid,c.crewid,c.ncdate,c.nctype from CP_NONCONFORMANCE c " + JoinSQL + " where C.NCType=2 and C.NCDate>=" + strFrm + " and C.NCDate<=" + strTo + " " + WhereSQL + " AND C.VesselID=" + VesselId + Reasion;
        else if(Type==2)
            sql = "select distinct c.vesselid,c.crewid,c.ncdate,c.nctype  from CP_NONCONFORMANCE c " + JoinSQL + " where C.NCType=6 and C.NCDate>=" + strFrm + "  and C.NCDate<=" + strTo + " " + WhereSQL + " AND C.VesselID=" + VesselId + Reasion;
        else if(Type==3)
            sql = "select distinct c.vesselid,c.crewid,c.ncdate,c.nctype  from CP_NONCONFORMANCE c " + JoinSQL + " where C.NCType=7 and C.NCDate>=" + strFrm + "  and C.NCDate<=" + strTo + " " + WhereSQL + " AND C.VesselID=" + VesselId + Reasion;
        else if(Type==4)
            sql = "select distinct c.vesselid,c.crewid,c.ncdate,c.nctype  from CP_NONCONFORMANCE c " + JoinSQL + " where C.NCType=24 and C.NCDate>=" + strFrm + "  and C.NCDate<=" + strTo + " " + WhereSQL + " AND C.VesselID=" + VesselId + Reasion;

        DataTable Dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        if (Dt.Rows.Count > 0)
        {
            return Dt.Rows.Count.ToString();
        }
        else
        {
            return "";
        }
                   
    }
    
    public void BindReason()
    {
        string sql = "select NCReasonID,NCReasonName from DBO.CP_NCReason";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
        ddlReason.DataSource = dt;
        ddlReason.DataTextField = "NCReasonName";
        ddlReason.DataValueField = "NCReasonID";
        ddlReason.DataBind();
        ddlReason.Items.Insert(0, new ListItem(" All Reason ", ""));
    }
   
    public void BindYear()
    {
        for (int i = DateTime.Now.Year; i >= 2000; i--)
        {
            ddlYearFrom.Items.Add(new System.Web.UI.WebControls.ListItem(i.ToString(), i.ToString()));
            ddlYearTO.Items.Add(new System.Web.UI.WebControls.ListItem(i.ToString(), i.ToString()));
            ddlYear.Items.Add(new System.Web.UI.WebControls.ListItem(i.ToString(), i.ToString()));
        }
        ddlYearFrom.SelectedValue = DateTime.Now.Year.ToString();
        ddlYearTO.SelectedValue = DateTime.Now.Year.ToString();
        ddlYear.SelectedValue = DateTime.Now.Year.ToString();
    }
    public void LoadFleet()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("SELECT * FROM FLEETMASTER");
        ddlFleet.DataSource = dt;
        ddlFleet.DataTextField = "FleetName";
        ddlFleet.DataValueField = "FleetId";
        ddlFleet.DataBind();  
        ddlFleet.Items.Insert(0, new ListItem(" All Fleet ","0"));      
    }
    public string Total(string a, string b, string c, string d)
    {

        int i= Common.CastAsInt32(Common.CastAsInt32(a) + Common.CastAsInt32(b) + Common.CastAsInt32(c) + Common.CastAsInt32(d));
        if (i == 0)
            return "";
        else
            return i.ToString();
    }
    public static string GetMonthName(String MonthId)
    {
        int mid = Convert.ToInt16(MonthId);
        switch (mid)
        {
            case 1:
                return "Jan";
            case 2:
                return "Feb";
            case 3: ;
                return "Mar";
            case 4:
                return "Apr";
            case 5:
                return "May";
            case 6:
                return "Jun";
            case 7:
                return "Jul";
            case 8:
                return "Aug";
            case 9:
                return "Sep";
            case 10:
                return "Oct";
            case 11:
                return "Nov";
            case 12:
                return "Dec";
            default:
                return "";
        }
    }
    //------------------------------------------------------
    //------------------------------------------------------
    //------------------------------------------------------
    //------------------------------------------------------
    protected void btnMail_Click(object sender, EventArgs e)
    {
        int VesselId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        if (GenerateWCPaketHTMLForm(VesselId.ToString()))
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "alert('Mail sent successfully.');", true);
        }
    }
    public bool GenerateWCPaketHTMLForm(string VesselID)
    {
        //goto Last;

        string FilePath = "C:\\inetpub\\wwwroot\\SHIPSOFT\\Auto Alert\\RestHourTemplate" + ".htm";
        if (!(File.Exists(FilePath)))
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "alert('Mail template file not exists.');", true);
            return false;
        }

        string VesselCode = "";
        StringBuilder sForm = new StringBuilder();
        string sql = "select V1.VESSELCode,V1.VesselName,cpd.crewnumber,cpd.firstname + ' ' + cpd.middlename + ' ' + cpd.lastname as CrewName,replace(Convert(Varchar, c.NCDate,106),' ','-')NCDate,TP.NCTYPENAME,v.VerifiedBy,V.VerifiedOn,V.Remarks,rm.NCReasonName,C.VESSELID,C.CREWID,C.NCTYPE,isnull(R.REASON,0) as REASON,rk.rankcode " +
                   "from " +
                   " ( select distinct vesselid,crewid,ncdate,nctype from CP_NONCONFORMANCE ) c " +
                   "inner join CP_NCTYPE TP ON TP.NCTYPEID=C.NCTYPE " +
                   "inner join vessel v1 on v1.vesselid=c.vesselid " +
                   "inner join crewpersonaldetails cpd on cpd.crewid=c.crewid " +
                   "inner join rank rk on rk.rankid=cpd.currentrankid " +
                   "left join CP_NONCONFORMANCE_verification v on c.vesselid=v.vesselid and c.crewid=v.crewid and c.ncdate=v.ncdate and c.nctype=v.nctype  " +
                   "left join CP_NonConformanceReason r on c.vesselid=r.vesselid and c.crewid=r.crewid and c.ncdate=r.ncdate  " +
                   "left join cp_NCREASON rm on r.reason=rm.NCReasonId where v.VERIFIEDBY IS NULL and c.vesselid='" + VesselID + "' and v1.VesselId in (Select VesselId from UserVesselRelation with(nolock) where Loginid = " + Convert.ToInt32(Session["loginid"].ToString()) + ") order by v1.VESSELCode,c.ncdate";

        DataTable dtRes = Common.Execute_Procedures_Select_ByQueryCMS(sql);

        if (dtRes.Rows.Count > 0)
        {
            
            string sFileText = File.ReadAllText(FilePath);
            
            int L1 = sFileText.Substring(0, sFileText.IndexOf("<!--$$End$$-->")).Length;
            int L2 = sFileText.Substring(0, sFileText.IndexOf("<!--$$Start$$-->")).Length;
            int fLenth = L1 - L2;
            string tr = sFileText.Substring(sFileText.IndexOf("<!--$$Start$$-->"), fLenth);

            string FirstPart = sFileText.Substring(0, sFileText.IndexOf("<!--$$Start$$-->"));
            string LastPart = sFileText.Substring(sFileText.IndexOf("<!--$$End$$-->"));
            FirstPart = FirstPart.Replace("$$Count$$", dtRes.Rows.Count.ToString());


            tr = tr.Replace("<!--$$Start$$-->", "");
            int cnt = 1;
            string trTotal = "";
            foreach (DataRow dr in dtRes.Rows)
            {
                string s1 = tr;
                FirstPart = FirstPart.Replace("$$VesselName$$", dr["VesselName"].ToString() + " As on : " + DateTime.Today.ToString("dd-MMM-yyyy"));
                VesselCode = dr["VESSELCode"].ToString();

                s1 = s1.Replace("lblCrewNo", "lblCrewNo" + cnt.ToString());
                s1 = s1.Replace("hfVesselID", "hfVesselID" + cnt.ToString());
                s1 = s1.Replace("hfCrewID", "hfCrewID" + cnt.ToString());
                s1 = s1.Replace("hfNCTypeID", "hfNCTypeID" + cnt.ToString());
                s1 = s1.Replace("lblName", "lblName" + cnt.ToString());
                s1 = s1.Replace("lblRank", "lblRank" + cnt.ToString());
                s1 = s1.Replace("lblDate", "lblDate" + cnt.ToString());
                s1 = s1.Replace("lblNCType", "lblNCType" + cnt.ToString());
                s1 = s1.Replace("ddlRestHour", "ddlRestHour" + cnt.ToString());
                s1 = s1.Replace("txtRemarks", "txtRemarks" + cnt.ToString());

                s1 = s1.Replace(" $$CrewNumber$$", dr["crewnumber"].ToString());
                s1 = s1.Replace("$$VesselID$$", dr["VESSELID"].ToString());
                s1 = s1.Replace("$$CrewID$$", dr["CrewID"].ToString());
                s1 = s1.Replace("$$NCTypeID$$", dr["NCTYPE"].ToString());
                s1 = s1.Replace("$$Name$$", dr["CrewName"].ToString());
                s1 = s1.Replace("$$Rank$$", dr["rankcode"].ToString());
                s1 = s1.Replace("$$Date$$", dr["NCDate"].ToString());
                s1 = s1.Replace("$$NCType$$", dr["NCTYPENAME"].ToString());

                s1 = s1.Replace("<option value=\"" + dr["REASON"].ToString() + "\">", "<option value=\"" + dr["REASON"].ToString() + "\" selected>");

                trTotal = trTotal + s1;
                cnt++;

            }
           

            //---------------------------
            //Last :

            string SenderAddress = "emanager@energiossolutions.com";
            DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("SELECT isnull(EMAIL,''),isnull(GROUPEMAIL,'') FROM DBO.VESSEL WHERE VESSELID=" + VesselID.ToString());
            
            string ToEmail = dt.Rows[0][0].ToString();
            string GroupEmail = dt.Rows[0][1].ToString();

            string[] ToAddress = { ToEmail };
            string[] CCAddress = { "emanager@energiossolutions.com", GroupEmail };
            //---------------------------

            StringBuilder msgFormat = new StringBuilder();
            msgFormat.Append("<html><head></head><body>" + "\n");
            msgFormat.Append("<table width=100% align=center> " + "\n");
            msgFormat.Append("<tr><td width=100% height=20 style=\"text-align: left\">Dear Captain, </td></tr>" + "\n");
            msgFormat.Append("<tr><td width=100% height=20 style=\"text-align: left\"> We have received a Non Conformity for the Breach of  Rest Hours from your vessel as attached. </td></tr>" + "\n");
            msgFormat.Append("<tr><td width=100% style=\"text-align: left\">  </td></tr>" + "\n");
            msgFormat.Append("<tr><td width=100% style=\"text-align: left\">  </td></tr>" + "\n");
            msgFormat.Append("<tr><td width=100% style=\"text-align: left\"> We would like to reiterate that failing to comply with rest hour requirements may lead to fatigue which in turn could lead to accidents on board.In order to close out this Non Conformity please send us the following information as soon as possible; </td></tr>" + "\n");
            msgFormat.Append("<tr><td width=100% style=\"text-align: left\"> 1)      The detailed reasons/circumstances which led to this non-conformity. </td></tr>" + "\n");
            msgFormat.Append("<tr><td width=100% style=\"text-align: left\"> 2)      Could this have been avoided? If Yes, then how? </td></tr>" + "\n");
            msgFormat.Append("<tr><td width=100% style=\"text-align: left\"> 3)      The work plan for the next 7 days to ensure compliance. </td></tr>" + "\n");

            msgFormat.Append("<tr><td width=100% style=\"text-align: left\">&nbsp;" + "</td></tr>" + "\n");
            msgFormat.Append("<tr><td width=100% style=\"text-align: left\">Thank you" + "</td></tr>" + "\n");
            msgFormat.Append("<tr><td width=100% style=\"text-align: left\"> <br/> HSQE Dept. </td></tr>" + "\n");
            msgFormat.Append("<tr><td width=100% height=20 style=\"text-align: left\"></td></tr>" + "\n");

            msgFormat.Append("</table>" + "\n");
            msgFormat.Append("</body></html>" + "\n");

            string Error="";

            string DestPath = Server.MapPath("RestHour.htm");

            using (StreamWriter sw = new StreamWriter(DestPath, false))
            {
                sw.Write(FirstPart + trTotal + LastPart);
            }
            SendEmail.SendeMail(0, SenderAddress, SenderAddress, ToAddress, CCAddress, CCAddress, "Rest Hour Update Packet - [ " + VesselCode + "-" + DateTime.Today.ToString("dd-MMM-yyyy") + " ] ", msgFormat.ToString(), out Error, DestPath);
            //-----------------------------
            return true;
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "alert('NC not exists.');", true);
            return false;

        }
    }
}
