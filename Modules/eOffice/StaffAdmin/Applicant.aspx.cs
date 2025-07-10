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
//using ApplicationFormWebService;

public partial class Applicant : System.Web.UI.Page
{
    public int SelectedAPID
    {
        get
        {
            return Common.CastAsInt32(ViewState["SelectedAPID"]);
        }
        set
        {
            ViewState["SelectedAPID"] = value;
        }
    }
    public int SelectedPageNo
    {
        get
        {
            return Common.CastAsInt32(ViewState["SelectedPageNo"]);
        }
        set
        {
            ViewState["SelectedPageNo"] = value;
        }
    }
    public string SortMode
    {
        get
        {
            return Convert.ToString(ViewState["SortMode"]);
        }
        set
        {
            ViewState["SortMode"] = value;
        }
    }
    public string SortColumn
    {
        get
        {
            return Convert.ToString(ViewState["SortColumn"]);
        }
        set
        {
            ViewState["SortColumn"] = value;
        }
    }
    
    public int PageNo
    {
        set { ViewState["PageNo"] = value; }
        get { return int.Parse("0" + ViewState["PageNo"]); }
    }
    public int PagesSlot
    {
        set { ViewState["PagesSlot"] = value; }
        get { return int.Parse("0" + ViewState["PagesSlot"]); }
    }
    public int TotalPages
    {
        set { ViewState["TotalPages"] = value; }
        get { return int.Parse("0" + ViewState["TotalPages"]); }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        lbl_License_Message.Text = "";
        this.lbl_info.Text = "";
        if (!Page.IsPostBack)
        {
            SortColumn = "Name";
            SortMode = "Asc";
                BindNationality();
                BindRank();
                binddata();
                BindStatus();
        }
    }

    // -------------------------  Funcation
    private void binddata()
    {
        string Query = " SELECT Row_number() over(order by APID )RowNumber,APID,isnull(FirstName,'')+' '+isnull(MiddleName,'')+ ' '+isnull(LastName ,'') as Name " +
                      " ,REPLACE (CONVERT(VARCHAR,DOB,106),' ','-')DOB " +
                      " ,PasportNumber " +
                      " ,PositionApplied Rankid " +
                      " ,(Select PositionName from dbo.Position where Position.PositionId=AM.PositionApplied) as PositionName " +
                      " ,REPLACE (CONVERT(VARCHAR,AvailableFrom,106),' ','-')AvailableFrom " +
                      " ,Nationality as NationalityID " +
                      " ,(select NationalityCode from dbo.Country where countryid=AM.Nationality) NationalityName " +
                      " ,Add1,Add2,Add3,Country,State,City,ZipCode,TelNumber,(MobCountryCode+' '+MobAreaCode+' '+MobNumber)as MobNumber,FaxNumber,Email1 as EmailId ,Email2,'DISCUSSION' as DISCUSSION,(select StatusName from HR_ApplicantStatus where Statusid=AM.Status)StatusName,DOA,FileName " +
                  " FROM DBO.HR_ApplicantMaster AM ";

        string WhereClause = " Where 1=1";

        if (ddlNat.SelectedIndex > 0)
        {
            WhereClause = WhereClause + " And AM.Nationality=" + ddlNat.SelectedValue;
        }
        if (ddlRank.SelectedIndex > 0)
        {
            WhereClause = WhereClause + " And AM.PositionApplied=" + ddlRank.SelectedValue;
        }
        
        if (ddlStatus.SelectedIndex > 0)
        {
            WhereClause = WhereClause + " And isnull(Status,1) =" + ddlStatus.SelectedValue + "";
        }
        if (txtIDName.Text.Trim() != "")
        {
            int Id = 0;
            try
            { Id = int.Parse(txtIDName.Text); }
            catch { }

            if (Id > 0)
                WhereClause = WhereClause + " And APID =" + Id.ToString() + "";
            else
                WhereClause = WhereClause + " And FirstName like '%" + txtIDName.Text + "%'" +
                                            "OR MiddleName like '%" + txtIDName.Text + "%'" +
                                            "OR LastName like '%" + txtIDName.Text + "%'";
        }
        WhereClause = WhereClause + " ";

        int StartRow = 0, EndRow = 0;
        if (PageNo != 0)
            StartRow = (19 * (PageNo - 1)) + 1;
        else
            StartRow = 1;
        EndRow = StartRow + 19;
        DataTable dtCandidateDetails =Common.Execute_Procedures_Select_ByQueryCMS(Query + WhereClause);
        DataView dtFiltered = dtCandidateDetails.DefaultView;
        dtFiltered.Sort = SortColumn + " " + SortMode;
        DataTable Temp1 = dtFiltered.ToTable();
        int Cnt = 1;
        foreach (DataRow Dr in Temp1.Rows)
        {
            Dr["RowNumber"] = Cnt.ToString();
            Cnt++;
        }
        DataView DefaultView2 = Temp1.DefaultView;
        DefaultView2.RowFilter = "RowNumber>=" + StartRow + " and RowNumber<" + EndRow + "";
        

        //BindRepeateToDataTable(dtFiltered.ToTable());

        TotalPages = dtCandidateDetails.Rows.Count / 19;
        if (dtCandidateDetails.Rows.Count > TotalPages * 19)
            TotalPages = TotalPages + 1;

        BindPageRepeater();


        ////DataRow[] Dr;
        ////Dr = dtCandidateDetails.("RowNumber>=0 and RowNumber<=10");
        ////for(int i=0;i<=Dr .Length;i++)
        ////{
        ////    dtFiltered.Rows.Add(Dr[i]);
        ////}

        try
        {
            this.rptData.DataSource = DefaultView2;
            ViewState.Add("v_dtFiltered", DefaultView2.ToTable());
        }
        catch (Exception ex)
        {
            Response.Write("error  : " + ex.Message);
            Response.Write(Query + WhereClause);
        } 
            
        this.rptData.DataBind();
        //lblRCount.Text = "[ " + rptData.Items.Count.ToString() + " ] records found.";

    }
    public void BindRepeateToDataTable(DataTable Dt)
    {
        int Cnt = 1;
        foreach (DataRow Dr in Dt.Rows)
        {
            Dr["RowNumber"] = Cnt.ToString();
            Cnt++;
        }
        rptData.DataSource = Dt;
        rptData.DataBind();
    }

    public void Sorting_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnlBtn = (LinkButton)sender;
            DataTable Dt = (DataTable)ViewState["v_dtFiltered"];
            DataView DV = Dt.DefaultView;
            SortMode = (SortMode == "Asc") ? "Desc" : "Asc";
            SortColumn = lnlBtn.CommandArgument;
            DV.Sort = SortColumn + " " + SortMode;
            //BindRepeateToDataTable(DV.ToTable());
            binddata();

        }
        catch (Exception ex)
        {
        }
    }  
    public void BindNationality()
    {
        string sql = "select CountryName,CountryId from Country ORDER BY CountryName";
        DataTable DT = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        this.ddlNat.DataTextField = "CountryName";
        this.ddlNat.DataValueField = "CountryId";
        this.ddlNat.DataSource = DT;
        this.ddlNat.DataBind();
        ddlNat.Items[0].Text = "<All>";
    }
    public void BindRank()
    {
        //string sql = "SELECT RankCode,RankId FROM Rank Order by RankCode";
        string sql = "select PositionId,PositionName from Position where PositionID not in(1)";
        DataTable DT = Common.Execute_Procedures_Select_ByQueryCMS(sql);

        this.ddlRank.DataTextField = "PositionName";
        this.ddlRank.DataValueField = "PositionId";
        this.ddlRank.DataSource = DT;
        this.ddlRank.DataBind();
        ddlRank.Items.Insert(0, new ListItem("<All>", "0"));
    }
    public void BindStatus()
    {
        string sql = "select StatusId,StatusName from HR_ApplicantStatus";
        DataTable DT = Common.Execute_Procedures_Select_ByQueryCMS(sql);

        this.ddlStatus.DataTextField = "StatusName";
        this.ddlStatus.DataValueField = "StatusId";
        this.ddlStatus.DataSource = DT;
        this.ddlStatus.DataBind();
        ddlStatus.Items.Insert(0, new ListItem("<All>", "0"));
    }
    
    


    // -------------------------  Events
    protected void imgView_OnClick(object sender, EventArgs e)
    {
        ImageButton ib = ((ImageButton)sender);
        int APID = Convert.ToInt32(ib.CommandArgument);
        SelectedAPID = APID;
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "abc", "window.open('NewAppEntry.aspx?APID=" + APID.ToString() + "','','');", true);
    }
    protected void UpdateList(object sender, EventArgs e)
    {
        binddata();
    }
    protected void gvcandidate_PreRender(object sender, EventArgs e)
    {
        if (rptData.Items.Count == 0)
        {
            this.lbl_License_Message.Text = "No Record Found...";
        }
        else
        {
            this.lbl_License_Message.Text = "";
        }
    }
    protected void btnReferesh_OnClick(object sender, EventArgs e)
    {
        binddata();
    }
    protected void btnClear_OnClick(object sender, EventArgs e)
    {
        txtIDName.Text = "";
        ddlRank.SelectedIndex = 0;
        ddlNat.SelectedIndex = 0;
        ddlStatus.SelectedIndex = 0;
    }
      
   
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "tr", "SetLastFocus('dvscroll_cdinfo');", true);
    }
    protected void lnPageNumber_OnClick(object sender, EventArgs e)
    {
        LinkButton lnPageNumber = (LinkButton)sender;
        PageNo = Common.CastAsInt32(lnPageNumber.Text);
        binddata();
    }
    protected void lnkNext20Pages_OnClick(object sender, EventArgs e)
    {
        PageNo = 0;
        PagesSlot = PagesSlot + 1;
        if (TotalPages < ((PagesSlot * 20) + 20))
            lnkNext20Pages.Visible = false;

        if (PagesSlot != 0)
            lnkPrev20Pages.Visible = true;

        BindPageRepeater();
    }
    protected void lnkPrev20Pages_OnClick(object sender, EventArgs e)
    {
        PageNo = 0;
        if (PagesSlot > 0)
            PagesSlot = PagesSlot - 1;

        if (TotalPages > ((PagesSlot * 20) + 20))
            lnkNext20Pages.Visible = true;
        if (PagesSlot == 0)
            lnkPrev20Pages.Visible = false;
            
        BindPageRepeater();
    }
    public void BindPageRepeater()
    {
        DataTable DtPages = new DataTable();
        int i = 1;
        int StartRowNumber = 0,EndRowNumber=0;
        StartRowNumber = (PagesSlot * 20) + 1;

        EndRowNumber = StartRowNumber + 19;
        if (EndRowNumber > TotalPages)
            EndRowNumber = TotalPages;
            

        DtPages.Columns.Add("PageNumber", typeof(int));

        for (i = StartRowNumber; i <= EndRowNumber; i++)
        {
            DtPages.Rows.Add(i.ToString());
        }
        rptPageNumber.DataSource = DtPages;
        rptPageNumber.DataBind();
    }
}
