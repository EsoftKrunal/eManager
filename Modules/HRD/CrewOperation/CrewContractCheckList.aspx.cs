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
using System.Data;
using System.Data.SqlClient;

public partial class CrewOperation_CrewContractCheckList : System.Web.UI.Page
{
    public int CheckListId
    {
        get { return Common.CastAsInt32(ViewState["CheckListId"]); }
        set { ViewState["CheckListId"] = value; }
    }
    public int CrewId
    {
        get { return Common.CastAsInt32(ViewState["CrewId"]); }
        set { ViewState["CrewId"] = value; }
    }
    public int VesselId
    {
        get { return Common.CastAsInt32(ViewState["VesselId"]); }
        set { ViewState["VesselId"] = value; }
    }

    
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        if (Request.QueryString["CheckListId"] != null)
        {
            CheckListId = Common.CastAsInt32(Request.QueryString["CheckListId"]);
        }
        if (Request.QueryString["ContractId"] != null)
        {
            DataTable dt = Budget.getTable("select checklistid from crewcontractchecklist where contractid=" + Request.QueryString["ContractId"].ToString()).Tables[0];
            if (dt.Rows.Count >0 )
            {
                CheckListId = Common.CastAsInt32(dt.Rows[0][0].ToString());
            }
        }
        CrewId = Convert.ToInt32(Page.Request.QueryString["CrewId"].ToString());
        if (Page.Request.QueryString["PortCallId"] != null)
        {
            int PortCallId = Convert.ToInt32(Page.Request.QueryString["PortCallId"].ToString());
            DataTable dt=Budget.getTable("SELECT VESSELID FROM PORTCALLHEADER WHERE PORTCALLID=" + PortCallId.ToString()).Tables[0] ;  
            VesselId=Convert.ToInt32(dt.Rows[0][0]);
        }
        if (Page.Request.QueryString["PromotionSignOnId"] != null)
        {
           int PromotionSignOnId = Convert.ToInt32(Page.Request.QueryString["PromotionSignOnId"].ToString());
           DataTable dt = Budget.getTable("SELECT VESSELID FROM PromotionSignOn WHERE PromotionSignOnId=" + PromotionSignOnId.ToString()).Tables[0];  
           VesselId = Convert.ToInt32(dt.Rows[0][0]);
        }
        if (Page.Request.QueryString["ContractRevisionId"] != null)
        {
            int ContractRevisionId = Convert.ToInt32(Page.Request.QueryString["ContractRevisionId"].ToString());
            DataTable dt = Budget.getTable("SELECT VESSELID FROM CrewContractRevision WHERE ContractRevisionId=" + ContractRevisionId.ToString()).Tables[0];
            VesselId = Convert.ToInt32(dt.Rows[0][0]);
        }

        //------------------------------
        if (Page.IsPostBack == false)
        {
                ShowHeaderData(CrewId);
                if (CheckListId > 0)
                {
                    Show_Details(CheckListId,CrewId);
                    btn_Save.Visible = false;  
                }
        }
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        DataTable dt = CrewJoiningCheckList.get_Check_List_Details(-1);
        DataRow dr = dt.NewRow();
        dr["EnglishTest"] = Convert.ToInt16(this.ddlenglishtest.SelectedValue);
        dr["BookNumber"] = txt_bookno.Text;
        if ((checkdate(txt_dateofIssue.Text) == "aa")) { dr["IssueDate"] = DBNull.Value; } else { dr["IssueDate"] = txt_dateofIssue.Text; }
        dr["PlaceOfIssue"] = txt_pob.Text;
        dr["Nat_1"] = nat1.Text;
        // dr["Nat_Date_1"] = checkdate(natdate1.Text);
        if ((checkdate(natdate1.Text) == "aa")) { dr["Nat_Date_1"] = DBNull.Value; } else { dr["Nat_Date_1"] = natdate1.Text; }
        dr["Nat_2"] = nat2.Text;
        //dr["Nat_Date_2"]=checkdate(natdate2.Text);
        if ((checkdate(natdate2.Text) == "aa")) { dr["Nat_Date_2"] = DBNull.Value; } else { dr["Nat_Date_2"] = natdate2.Text; }
        dr["Nat_3"] = nat3.Text;
        //dr["Nat_Date_3"]=checkdate(natdate3.Text);
        if ((checkdate(natdate3.Text) == "aa")) { dr["Nat_Date_3"] = DBNull.Value; } else { dr["Nat_Date_3"] = natdate3.Text; }
        dr["Nat_4"] = nat4.Text;
        //dr["Nat_Date_4"]=checkdate(natdate4.Text);
        if ((checkdate(natdate4.Text) == "aa")) { dr["Nat_Date_4"] = DBNull.Value; } else { dr["Nat_Date_4"] = natdate4.Text; }
        dr["Nat_5"] = nat5.Text;
        //dr["Nat_Date_5"]=checkdate(natdate5.Text);
        if ((checkdate(natdate5.Text) == "aa")) { dr["Nat_Date_5"] = DBNull.Value; } else { dr["Nat_Date_5"] = natdate5.Text; }
        dr["Nat_6"] = nat6.Text;
        //dr["Nat_Date_6"]=checkdate(natdate6.Text);
        if ((checkdate(natdate6.Text) == "aa")) { dr["Nat_Date_6"] = DBNull.Value; } else { dr["Nat_Date_6"] = natdate6.Text; }
        dr["Nat_7"] = nat7.Text;
        //dr["Nat_Date_7"]=checkdate(natdate7.Text);
        if ((checkdate(natdate7.Text) == "aa")) { dr["Nat_Date_7"] = DBNull.Value; } else { dr["Nat_Date_7"] = natdate7.Text; }
        dr["Lib_1"] = Lib1.Text;
        //dr["Lib_Date_1"]=checkdate(Libdate1.Text);
        if ((checkdate(Libdate1.Text) == "aa")) { dr["Lib_Date_1"] = DBNull.Value; } else { dr["Lib_Date_1"] = Libdate1.Text; }
        dr["Lib_2"] = Lib2.Text;
        //dr["Lib_Date_2"]=checkdate(Libdate2.Text);
        if ((checkdate(Libdate2.Text) == "aa")) { dr["Lib_Date_2"] = DBNull.Value; } else { dr["Lib_Date_2"] = Libdate2.Text; }
        dr["Lib_3"] = Lib3.Text;
        //dr["Lib_Date_3"]=checkdate(Libdate3.Text);
        if ((checkdate(Libdate3.Text) == "aa")) { dr["Lib_Date_3"] = DBNull.Value; } else { dr["Lib_Date_3"] = Libdate3.Text; }
        dr["Lib_4"] = Lib4.Text;
        //dr["Lib_Date_4"]=checkdate(Libdate4.Text);
        if ((checkdate(Libdate4.Text) == "aa")) { dr["Lib_Date_4"] = DBNull.Value; } else { dr["Lib_Date_4"] = Libdate4.Text; }
        dr["Lib_5"] = Lib5.Text;
        // dr["Lib_Date_5"]=checkdate(Libdate5.Text);
        if ((checkdate(Libdate5.Text) == "aa")) { dr["Lib_Date_5"] = DBNull.Value; } else { dr["Lib_Date_5"] = Libdate5.Text; }
        dr["Lib_6"] = Lib6.Text;
        //dr["Lib_Date_6"]=checkdate(Libdate6.Text);
        if ((checkdate(Libdate6.Text) == "aa")) { dr["Lib_Date_6"] = DBNull.Value; } else { dr["Lib_Date_6"] = Libdate6.Text; }
        dr["Lib_7"] = Lib7.Text;
        //dr["Lib_Date_7"]=checkdate(Libdate7.Text);
        if ((checkdate(Libdate7.Text) == "aa")) { dr["Lib_Date_7"] = DBNull.Value; } else { dr["Lib_Date_7"] = Libdate7.Text; }
        dr["Pan_1"] = Panama1.Text;
        //dr["Pan_Date_1"]=checkdate(Panamadate1.Text);
        if ((checkdate(Panamadate1.Text) == "aa")) { dr["Pan_Date_1"] = DBNull.Value; } else { dr["Pan_Date_1"] = Panamadate1.Text; }
        dr["Pan_2"] = Panama2.Text;
        //dr["Pan_Date_2"]=checkdate(Panamadate2.Text);
        if ((checkdate(Panamadate2.Text) == "aa")) { dr["Pan_Date_2"] = DBNull.Value; } else { dr["Pan_Date_2"] = Panamadate2.Text; }
        dr["Pan_3"] = Panama3.Text;
        //dr["Pan_Date_3"]=checkdate(Panamadate3.Text);
        if ((checkdate(Panamadate3.Text) == "aa")) { dr["Pan_Date_3"] = DBNull.Value; } else { dr["Pan_Date_3"] = Panamadate3.Text; }
        dr["Pan_4"] = Panama4.Text;
        //dr["Pan_Date_4"]=checkdate(Panamadate4.Text);
        if ((checkdate(Panamadate4.Text) == "aa")) { dr["Pan_Date_4"] = DBNull.Value; } else { dr["Pan_Date_4"] = Panamadate4.Text; }
        dr["Pan_5"] = Panama5.Text;
        //dr["Pan_Date_5"]=checkdate(Panamadate5.Text);
        if ((checkdate(Panamadate5.Text) == "aa")) { dr["Pan_Date_5"] = DBNull.Value; } else { dr["Pan_Date_5"] = Panamadate5.Text; }
        dr["Pan_6"] = Panama6.Text;
        //dr["Pan_Date_6"]=checkdate(Panamadate6.Text);
        if ((checkdate(Panamadate6.Text) == "aa")) { dr["Pan_Date_6"] = DBNull.Value; } else { dr["Pan_Date_6"] = Panamadate6.Text; }
        dr["Pan_7"] = Panama7.Text;
        //dr["Pan_Date_7"]=checkdate(Panamadate7.Text);
        if ((checkdate(Panamadate7.Text) == "aa")) { dr["Pan_Date_7"] = DBNull.Value; } else { dr["Pan_Date_7"] = Panamadate7.Text; }
        dr["Sin_1"] = Sing1.Text;
        //dr["Sin_Date_1"]=checkdate(Singdate1.Text);
        if ((checkdate(Singdate1.Text) == "aa")) { dr["Sin_Date_1"] = DBNull.Value; } else { dr["Sin_Date_1"] = Singdate1.Text; }
        dr["Sin_2"] = Sing2.Text;
        //dr["Sin_Date_2"]=checkdate(Singdate2.Text);
        if ((checkdate(Singdate2.Text) == "aa")) { dr["Sin_Date_2"] = DBNull.Value; } else { dr["Sin_Date_2"] = Singdate2.Text; }
        dr["Sin_3"] = Sing3.Text;
        //dr["Sin_Date_3"]=checkdate(Singdate3.Text);
        if ((checkdate(Singdate3.Text) == "aa")) { dr["Sin_Date_3"] = DBNull.Value; } else { dr["Sin_Date_3"] = Singdate3.Text; }
        dr["Sin_4"] = Sing4.Text;
        //dr["Sin_Date_4"]=checkdate(Singdate4.Text);
        if ((checkdate(Singdate4.Text) == "aa")) { dr["Sin_Date_4"] = DBNull.Value; } else { dr["Sin_Date_4"] = Singdate4.Text; }
        dr["Sin_5"] = Sing5.Text;
        //dr["Sin_Date_5"]=checkdate(Singdate5.Text);
        if ((checkdate(Singdate5.Text) == "aa")) { dr["Sin_Date_5"] = DBNull.Value; } else { dr["Sin_Date_5"] = Singdate5.Text; }
        dr["Sin_6"] = Sing6.Text;
        //dr["Sin_Date_6"]=checkdate(Singdate6.Text);
        if ((checkdate(Singdate6.Text) == "aa")) { dr["Sin_Date_6"] = DBNull.Value; } else { dr["Sin_Date_6"] = Singdate6.Text; }
        dr["Sin_7"] = Sing7.Text;
        //dr["Sin_Date_7"]=checkdate(Singdate7.Text);
        if ((checkdate(Singdate7.Text) == "aa")) { dr["Sin_Date_7"] = DBNull.Value; } else { dr["Sin_Date_7"] = Singdate7.Text; }
        dr["RadioButtonList1"] = RadioButtonList1.SelectedValue;
        dr["RadioButtonList2"] = RadioButtonList2.SelectedValue;
        dr["RadioButtonList3"] = RadioButtonList3.SelectedValue;
        dr["RadioButtonList4"] = RadioButtonList4.SelectedValue;
        dr["RadioButtonList5"] = RadioButtonList5.SelectedValue;
        dr["RadioButtonList6"] = RadioButtonList6.SelectedValue;
        dr["RadioButtonList7"] = RadioButtonList7.SelectedValue;
        dr["RadioButtonList8"] = RadioButtonList8.SelectedValue;
        dr["RadioButtonList9"] = RadioButtonList9.SelectedValue;
        dr["RadioButtonList10"] = RadioButtonList10.SelectedValue;
        dr["RadioButtonList11"] = RadioButtonList11.SelectedValue;
        dr["RadioButtonList12"] = RadioButtonList12.SelectedValue;
        dr["RadioButtonList13"] = RadioButtonList13.SelectedValue;
        dr["RadioButtonList14"] = RadioButtonList14.SelectedValue;
        dr["RadioButtonList15"] = RadioButtonList15.SelectedValue;
        dr["RadioButtonList16"] = RadioButtonList16.SelectedValue;
        dr["RadioButtonList17"] = RadioButtonList17.SelectedValue;
        dr["RadioButtonList18"] = RadioButtonList18.SelectedValue;
        dr["RadioButtonList19"] = RadioButtonList19.SelectedValue;
        dr["RadioButtonList20"] = RadioButtonList20.SelectedValue;
        dr["RadioButtonList21"] = RadioButtonList21.SelectedValue;
        dr["RadioButtonList22"] = RadioButtonList22.SelectedValue;
        dr["RadioButtonList23"] = RadioButtonList23.SelectedValue;
        dr["Doc1"] = Doc1.Text;
        // dr["Date1"]=checkdate(Docdate1.Text); 
        if ((checkdate(Docdate1.Text) == "aa")) { dr["Date1"] = DBNull.Value; } else { dr["Date1"] = Docdate1.Text; }
        dr["Doc2"] = Doc2.Text;
        //dr["Date2"]=checkdate(Docdate2.Text); 
        if ((checkdate(Docdate2.Text) == "aa")) { dr["Date2"] = DBNull.Value; } else { dr["Date2"] = Docdate2.Text; }
        dr["Doc3"] = Doc3.Text;
        //dr["Date3"]=checkdate(Docdate3.Text); 
        if ((checkdate(Docdate3.Text) == "aa")) { dr["Date3"] = DBNull.Value; } else { dr["Date3"] = Docdate3.Text; }
        dr["Doc4"] = Doc4.Text;
        //dr["Date4"]=checkdate(Docdate4.Text); 
        if ((checkdate(Docdate4.Text) == "aa")) { dr["Date4"] = DBNull.Value; } else { dr["Date4"] = Docdate4.Text; }
        dr["Doc5"] = Doc5.Text;
        //dr["Date5"]=checkdate(Docdate5.Text);
        if ((checkdate(Docdate5.Text) == "aa")) { dr["Date5"] = DBNull.Value; } else { dr["Date5"] = Docdate5.Text; }
        dt.Rows.Add(dr);
        // SAVING DATA ------------------------
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter adp = new SqlDataAdapter();
        SqlCommandBuilder cb = new SqlCommandBuilder(adp);
        DataSet dsjoin = new DataSet();
        DataRow drS;
        DataTable dtcon;
        con.ConnectionString = ConfigurationManager.ConnectionStrings["eMANAGER"].ToString();
        cmd.Connection = con;
        adp.SelectCommand = cmd;
        cmd.CommandText = "select * from CrewContractcheckList where 1=2";
        adp.Fill(dsjoin, "com");
        drS = dsjoin.Tables[0].NewRow();
        dr["Contractid"] = -CrewId;
        for (int i = 1; i < dt.Columns.Count - 2; i++)
        {
            drS[i] = dt.Rows[0][i];
        }
        dsjoin.Tables[0].Rows.Add(drS);
        adp.Update(dsjoin.Tables[0]);
        Page.ClientScript.RegisterStartupScript(this.GetType(), "dialog", "window.opener.document.getElementById('ctl00_contentPlaceHolder1_btnRefresh').click();window.close();", true);
    }
    protected string checkdate(string str)
    {
        try
        {
            DateTime dt;
            dt=Convert.ToDateTime(str);
            return dt.ToString();
        }
        catch
        {
            return "aa" ;
        }
    }
    protected void ShowHeaderData(int CrewId)
    {
        string sql = "SELECT CREWNUMBER,FIRSTNAME + ' ' + MIDDLENAME + ' ' + LASTNAME AS CREWNAME,COUNTRYNAME,replace(convert(varchar,DATEOFBIRTH,106),' ','-') as DATEOFBIRTH,replace(convert(varchar,DateFirstJoin,106),' ','-') as DateFirstJoin,PLACEOFBIRTH, " +
                   "(SELECT TOP 1 DOCUMENTNUMBER FROM CREWTRAVELDOCUMENT CTD WHERE CTD.CREWID=CPD.CREWID AND DOCUMENTTYPEID=0) AS PASS_NO,  " +
                   "(SELECT TOP 1 replace(convert(varchar,ISSUEDATE,106),' ','-') FROM CREWTRAVELDOCUMENT CTD WHERE CTD.CREWID=CPD.CREWID AND DOCUMENTTYPEID=0) AS PASS_IDATE,  " +
                   "(SELECT TOP 1 replace(convert(varchar,EXPIRYDATE,106),' ','-') FROM CREWTRAVELDOCUMENT CTD WHERE CTD.CREWID=CPD.CREWID AND DOCUMENTTYPEID=0) AS PASS_EDATE,  " +
                   "(SELECT TOP 1 PLACEOFISSUE FROM CREWTRAVELDOCUMENT CTD WHERE CTD.CREWID=CPD.CREWID AND DOCUMENTTYPEID=0) AS PASS_PLACE, " +
                   "RANKNAME FROM " +
                   "CREWPERSONALDETAILS CPD INNER JOIN COUNTRY C ON C.COUNTRYID=CPD.NATIONALITYID " +
                   "INNER JOIN RANK R ON R.RANKID=CPD.CURRENTRANKID " +
                   "WHERE CPD.CREWID=" + CrewId.ToString();

        DataTable dt = Budget.getTable(sql).Tables[0];
        if (dt.Rows.Count > 0)
        {
            DataRow dr = dt.Rows[0];
            this.lbl_Name.Text = dr["CREWNAME"].ToString();
            this.lbl_Rank.Text = dr["RANKNAME"].ToString();
            this.lbl_Nationality.Text = dr["COUNTRYNAME"].ToString();
            this.lbl_DOB.Text = dr["DATEOFBIRTH"].ToString();
            this.lbl_POB.Text = dr["PLACEOFBIRTH"].ToString();

            this.lbl_DOJ.Text = dr["DateFirstJoin"].ToString();
            this.lbl_PPTNo.Text = dr["PASS_NO"].ToString();
            this.lbl_PPT_IssueDate.Text = dr["PASS_IDATE"].ToString();
            this.lbl_PPT_IssuePlace.Text = dr["PASS_PLACE"].ToString();
            this.lbl_PPT_ExpDate.Text = dr["PASS_EDATE"].ToString();
        }

        dt = Budget.getTable("select vesselname from vessel v where v.vesselid=" + VesselId.ToString()).Tables[0];
        if (dt.Rows.Count > 0)
        {
            //vesselid = Convert.ToInt32(dt.Rows[0][0].ToString());
            this.lbl_Vessel.Text = dt.Rows[0]["vesselname"].ToString();
        }
    }
    protected void Show_Details(int CheckListId,int CrewId)
    {
        DataTable dt;
        int Rank = 0;

        dt = Budget.getTable("SELECT CURRENTRANKID FROM CREWPERSONALDETAILS WHERE CREWID=" + CrewId.ToString()).Tables[0];
        Rank = Convert.ToInt32(dt.Rows[0][0].ToString());
        
        dt = CrewJoiningCheckList.get_contractchecklistheader_Other(CrewId, 0, Rank);
        nat1.Text = dt.Rows[0][0].ToString();
        natdate1.Text = dt.Rows[0][1].ToString();
        nat2.Text = dt.Rows[0][2].ToString();
        natdate2.Text = dt.Rows[0][3].ToString();
        nat3.Text = dt.Rows[0][4].ToString();
        natdate3.Text = dt.Rows[0][5].ToString();
        nat4.Text = dt.Rows[0][6].ToString();
        natdate4.Text = dt.Rows[0][7].ToString();
        nat5.Text = dt.Rows[0][8].ToString();
        natdate5.Text = dt.Rows[0][9].ToString();
        nat6.Text = dt.Rows[0][10].ToString();
        natdate6.Text = dt.Rows[0][11].ToString();
        nat7.Text = dt.Rows[0][12].ToString();
        natdate7.Text = dt.Rows[0][13].ToString();

        dt = CrewJoiningCheckList.get_contractchecklistheader_Other(CrewId, 1, Rank);
        Lib1.Text = dt.Rows[0][0].ToString();
        Libdate1.Text = dt.Rows[0][1].ToString();
        Lib2.Text = dt.Rows[0][2].ToString();
        Libdate2.Text = dt.Rows[0][3].ToString();
        Lib3.Text = dt.Rows[0][4].ToString();
        Libdate3.Text = dt.Rows[0][5].ToString();
        Lib4.Text = dt.Rows[0][6].ToString();
        Libdate4.Text = dt.Rows[0][7].ToString();
        Lib5.Text = dt.Rows[0][8].ToString();
        Libdate5.Text = dt.Rows[0][9].ToString();
        Lib6.Text = dt.Rows[0][10].ToString();
        Libdate6.Text = dt.Rows[0][11].ToString();
        Lib7.Text = dt.Rows[0][12].ToString();
        Libdate7.Text = dt.Rows[0][13].ToString();

        dt = CrewJoiningCheckList.get_contractchecklistheader_Other(CrewId, 2, Rank);
        Panama1.Text = dt.Rows[0][0].ToString();
        Panamadate1.Text = dt.Rows[0][1].ToString();
        Panama2.Text = dt.Rows[0][2].ToString();
        Panamadate2.Text = dt.Rows[0][3].ToString();
        Panama3.Text = dt.Rows[0][4].ToString();
        Panamadate3.Text = dt.Rows[0][5].ToString();
        Panama4.Text = dt.Rows[0][6].ToString();
        Panamadate4.Text = dt.Rows[0][7].ToString();
        Panama5.Text = dt.Rows[0][8].ToString();
        Panamadate5.Text = dt.Rows[0][9].ToString();
        Panama6.Text = dt.Rows[0][10].ToString();
        Panamadate6.Text = dt.Rows[0][11].ToString();
        Panama7.Text = dt.Rows[0][12].ToString();
        Panamadate7.Text = dt.Rows[0][13].ToString();

        dt = CrewJoiningCheckList.get_contractchecklistheader_Other(CrewId, 3, Rank);
        Sing1.Text = dt.Rows[0][0].ToString();
        Singdate1.Text = dt.Rows[0][1].ToString();
        Sing2.Text = dt.Rows[0][2].ToString();
        Singdate2.Text = dt.Rows[0][3].ToString();
        Sing3.Text = dt.Rows[0][4].ToString();
        Singdate3.Text = dt.Rows[0][5].ToString();
        Sing4.Text = dt.Rows[0][6].ToString();
        Singdate4.Text = dt.Rows[0][7].ToString();
        Sing5.Text = dt.Rows[0][8].ToString();
        Singdate5.Text = dt.Rows[0][9].ToString();
        Sing6.Text = dt.Rows[0][10].ToString();
        Singdate6.Text = dt.Rows[0][11].ToString();
        Sing7.Text = dt.Rows[0][12].ToString();
        Singdate7.Text = dt.Rows[0][13].ToString();

    }

}

