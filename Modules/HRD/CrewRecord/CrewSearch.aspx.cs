using System;
using System.Data;
using System.Configuration;
using System.Reflection;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using ShipSoft.CrewManager.BusinessLogicLayer;
using ShipSoft.CrewManager.BusinessObjects;
using ShipSoft.CrewManager.Operational;
using System.Drawing;
//using System.Windows.Forms;

struct SeatchFields
{
    public string EmpNo;
    public string FirstName;
    public string FamilyName;
    public int Rank;
    public string ExpFrom, ExpTo;
    public int Nationality;
    public string onBoardFrom, onBoardTo;
    public int Vessel;
    public int Status;
    public string AgeFrom, AgeTo;
    public string PassportNo;
    public int Owner;
    public int RecOff;
    public int VesselType;
    public bool UsVisa;
    public bool FamilyMems;
    public bool SchengenVisa;
};
public partial class CrewSearch : System.Web.UI.Page
{
    SeatchFields FilterVar;
    Authority Obj;
    bool IsPrint = false;
    protected void CrewDb_Click(object sender, EventArgs e)
    {
        Response.Redirect(ConfigurationManager.AppSettings["OtherLink"].ToString() + "?u=" + Session["UN"].ToString() + "&p=" + Session["Pwd"].ToString());
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        // GridView1.HeaderStyle.CssClass = "headerstylefixedheadergrid";
        //-----------------------------
        SessionManager.SessionCheck_New();
        this.Form.DefaultButton = btn_Search.UniqueID;
        //-----------------------------
        Button3.Visible = false;// (Alerts.getLocation() == "S") && (Convert.ToInt32(Session["loginid"].ToString()) == 1);  
        //this.Form.DefaultButton = "btn_Search";
        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 1);
        if (chpageauth <= 0)
        {
            Response.Redirect("../AuthorityError.aspx");
        }
        //*******************
        //********* Crew Approval
        if (Page.Request.QueryString["showcrewdetails"] != null && Page.Request.QueryString["showcrewdetails"] == "1")
        {
            Response.Redirect("crewdetails.aspx?id="+ Convert.ToInt32(Session["CrewId"].ToString()));
        }
        //*********
        // CODE FOR UDATING THE AUTHORITY
        lblmessage.Text = "";
        ProcessCheckAuthority Auth = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 1);
        Auth.Invoke();
        lblGrid.Text = ""; 
        Session["Authority"] = Auth.Authority;
        // END CODE

        Obj = (Authority)Session["Authority"];
        try
        {
            if (Session["UserName"] == "")
            {
                Response.Redirect("Login.aspx");
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
            return;
        }
        if (!(IsPostBack))
        {
            BindOwnerPoolDropDown();
            BindVessel();
            BindCrewStatusDropDown();
            BindNationalityDropDown();
            BindRankDropDown();
            BindVesselType();
            BindRecruitingOffice();
            //btn_Add.Visible = Obj.isAdd;
            //btn_Add.Visible = !(Alerts.getLocation() == "S");

            //---------------------------------------
            try
            {
                if (Session["FilterVar"] != null)
                {
                    FilterVar = (SeatchFields)Session["FilterVar"];
                    txt_MemberId_Search.Text = FilterVar.EmpNo;
                    txt_FirstName_Search.Text = FilterVar.FirstName;
                  // txt_LastName_Search.Text = FilterVar.FamilyName;
                    ddl_Rank_Search.SelectedIndex = FilterVar.Rank;
                    txt_Exp_From.Text = FilterVar.ExpFrom;
                    txt_Exp_To.Text = FilterVar.ExpTo;
                    ddl_Nationality.SelectedIndex = FilterVar.Nationality;
                    txt_SignOn_Date.Text = FilterVar.onBoardFrom;
                    txt_SignOff_Date.Text = FilterVar.onBoardTo;
                    ddl_Vessel.SelectedIndex = FilterVar.Vessel;
                    ddl_CrewStatus_Search.SelectedIndex = FilterVar.Status;
                    txt_Age_From.Text = FilterVar.AgeFrom;
                    txt_Age_To.Text = FilterVar.AgeTo;
                    txt_PassportNo.Text = FilterVar.PassportNo;
                    ddl_Owner.SelectedIndex = FilterVar.Owner;
                    ddl_Recr_Office.SelectedIndex = FilterVar.RecOff;
                    ddl_VesselType.SelectedIndex = FilterVar.VesselType;
                    chk_UsVisa.Checked = (bool)FilterVar.UsVisa;
                    chk_SchengenVisa.Checked = (bool)FilterVar.SchengenVisa;
                    chk_Family.Checked = (bool)FilterVar.FamilyMems;
                    btn_Search_Click(new object(), new EventArgs());
                }
            }
            catch
            { }
            //---------------------------------------
        }
   
    }
    private void BindOwnerPoolDropDown()
    {
        DataTable dt;
        dt = VesselDetailsGeneral.selectDataOwnerName(); 
        ddl_Owner.DataValueField = "Ownerid";
        ddl_Owner.DataTextField = "OwnerShortName";
        ddl_Owner.DataSource =dt;
        ddl_Owner.DataBind();
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
        DataSet ds = cls_SearchReliever.getMasterData("Vessel", "VesselId", "VesselName", Convert.ToInt32(Session["loginid"].ToString()));
        ddl_Vessel.DataValueField = "VesselId";
        ddl_Vessel.DataTextField = "VesselName";
        ddl_Vessel.DataSource = ds;
        ddl_Vessel.DataBind();
        ddl_Vessel.Items.Insert(0, new ListItem("< Select >", "0")); 
    }
    private void BindVesselType()
    {
        //ProcessSelectVesselType vesseletype = new ProcessSelectVesselType();
        //try
        //{
        //    vesseletype.Invoke();
        //}
        //catch (Exception ex)
        //{
        //    //Response.Write(ex.Message.ToString());
        //}
        DataSet ds = cls_SearchReliever.getMasterData("VesselType", "VesselTypeId", "VesselTypeName"); 
        ddl_VesselType.DataValueField = "VesselTypeId";
        ddl_VesselType.DataTextField = "VesselTypeName";
        ddl_VesselType.DataSource =ds;
        ddl_VesselType.DataBind();
        ddl_VesselType.Items.Insert(0, new ListItem("< Select >","0")); 
    }
    private void BindRecruitingOffice()
    {
        ProcessGetRecruitingOffice processgetrecruitingoffice = new ProcessGetRecruitingOffice();
        try
        {
            processgetrecruitingoffice.Invoke();
        }
        catch (Exception ex)
        {
            //Response.Write(ex.Message.ToString());
        }
        ddl_Recr_Office.DataValueField = "RecruitingOfficeId";
        ddl_Recr_Office.DataTextField = "RecruitingOfficeName";
        ddl_Recr_Office.DataSource = processgetrecruitingoffice.ResultSet;
        ddl_Recr_Office.DataBind();
    }
    private void BindGrid(String Sort)
    {
        //ProcessSelectCrewMemberPrimaryDetails_Search obj = new ProcessSelectCrewMemberPrimaryDetails_Search();
        //BoCrewSearch Member = new BoCrewSearch();
        //Member.CrewNumber = txt_MemberId_Search.Text;
        //Member.FirstName = txt_FirstName_Search.Text;
        //Member.LastName = txt_LastName_Search.Text;
        //Member.Nationality = (ddl_Nationality.SelectedIndex <= 0) ? 0 : Convert.ToInt32(ddl_Nationality.SelectedValue);
        //Member.CrewStatusId = Convert.ToInt32(ddl_CrewStatus_Search.SelectedValue.ToString());
        //Member.Rank = Convert.ToInt32(ddl_Rank_Search.SelectedValue.ToString());
        //Member.PassportNo  = txt_PassportNo.Text.Trim();
        //Member.RecOffId = Convert.ToInt32(ddl_Recr_Office.SelectedValue.ToString());
        
        //if (txt_Age_From.Text.Trim() != "")
        //{
        //    Member.AgeFrom = Convert.ToInt32(txt_Age_From.Text.Trim());
        //    Member.AgeTo = Convert.ToInt32(txt_Age_To.Text.Trim());
        //}
        //else
        //{
        //    Member.AgeFrom =0;
        //    Member.AgeTo = 0;
        //}
        //if (txt_Exp_From.Text.Trim() != "")
        //{
        //    Member.ExpFrom = Convert.ToInt32(txt_Exp_From.Text.Trim());
        //    Member.ExpTo = Convert.ToInt32(txt_Exp_To.Text.Trim());
        //}
        //else
        //{
        //    Member.ExpFrom = 0;
        //    Member.ExpTo = 0;
        //}

        //obj.CrewSearch  = Member;
        //obj.Invoke();
        ProcessSelectCrewMemberPrimaryDetails_Search obj = new ProcessSelectCrewMemberPrimaryDetails_Search();
        BoCrewSearch Member = new BoCrewSearch();
        Member.LoginId = Convert.ToInt32(Session["loginid"].ToString());
        Member.CrewNumber = txt_MemberId_Search.Text;
        Member.FirstName = txt_FirstName_Search.Text;
       // Member.LastName = txt_LastName_Search.Text;
        Member.Nationality = (ddl_Nationality.SelectedIndex <= 0) ? 0 : Convert.ToInt32(ddl_Nationality.SelectedValue);
        Member.CrewStatusId = Convert.ToInt32(ddl_CrewStatus_Search.SelectedValue.ToString());
        Member.Rank = Convert.ToInt32(ddl_Rank_Search.SelectedValue.ToString());
        Member.PassportNo = txt_PassportNo.Text.Trim();
        Member.RecOffId = Convert.ToInt32(ddl_Recr_Office.SelectedValue.ToString());

        Member.FromDate = txt_SignOn_Date.Text.Trim();
        Member.ToDate = txt_SignOff_Date.Text.Trim();

        Member.VesselId = Convert.ToInt32(ddl_Vessel.SelectedValue);
        Member.VesselType = Convert.ToInt32(ddl_VesselType.SelectedValue);
        Member.Owner = Convert.ToInt32(ddl_Owner.SelectedValue);
        // Member.ReliefDue = txt_ReliefDue.Text;  

        if (chk_UsVisa.Checked == true)
        {

            Member.USvisa = 1;
        }
        else
        {
            Member.USvisa = 0;
        }



        if (chk_Family.Checked == true)
        {

            Member.FamilyMember = 1;
        }
        else
        {
            Member.FamilyMember = 0;
        }

        if (txt_Age_From.Text.Trim() != "")
        {
            Member.AgeFrom = Convert.ToInt32(txt_Age_From.Text.Trim());
            Member.AgeTo = Convert.ToInt32(txt_Age_To.Text.Trim());
        }
        else
        {
            Member.AgeFrom = 0;
            Member.AgeTo = 0;
        }
        if (txt_Exp_From.Text.Trim() != "")
        {
            Member.ExpFrom = Convert.ToInt32(txt_Exp_From.Text.Trim());
            Member.ExpTo = Convert.ToInt32(txt_Exp_To.Text.Trim());
        }
        else
        {
            Member.ExpFrom = 0;
            Member.ExpTo = 0;
        }

        obj.CrewSearch = Member;
        obj.Invoke();
       
        obj.ResultSet.Tables[0].DefaultView.Sort = Sort;
        GridView1.DataSource = obj.ResultSet.Tables[0];
        GridView1.DataBind();
        GridView1.Attributes.Add("MySort", Sort);

    }
    private void BindCrewStatusDropDown()
    {
        ProcessSelectCrewStatus obj = new ProcessSelectCrewStatus();
        obj.Invoke();
        ddl_CrewStatus_Search.DataSource = obj.ResultSet.Tables[0];
        ddl_CrewStatus_Search.DataTextField = "CrewStatusName";
        ddl_CrewStatus_Search.DataValueField = "CrewStatusId";
        ddl_CrewStatus_Search.DataBind();

    }
    private void BindCrewStatusDropDownForVessel()
    {
        string sql = "Select CrewStatusId,CrewStatusName from CrewStatus with(nolock) where CrewStatusName <> 'New' ";
       DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
        ddl_CrewStatus_Search.DataSource = dt;
        ddl_CrewStatus_Search.DataTextField = "CrewStatusName";
        ddl_CrewStatus_Search.DataValueField = "CrewStatusId";
        ddl_CrewStatus_Search.DataBind();
        ddl_CrewStatus_Search.Items.Insert(0, new ListItem("< Select >", "0"));
    }
    private void BindRankDropDown()
    {
        ProcessSelectRank obj = new ProcessSelectRank();
        obj.Invoke();
        ddl_Rank_Search.DataSource = obj.ResultSet.Tables[0];
        ddl_Rank_Search.DataTextField = "RankName";
        ddl_Rank_Search.DataValueField = "RankId";
        ddl_Rank_Search.DataBind();

    }
    private void BindNationalityDropDown()
    {
        ProcessSelectNationality obj = new ProcessSelectNationality();
        obj.Invoke();
        ddl_Nationality.DataSource = obj.ResultSet.Tables[0];
        ddl_Nationality.DataTextField = "CountryName";
        ddl_Nationality.DataValueField = "CountryId";
        ddl_Nationality.DataBind();
    }
    protected void btn_Print_Click(object sender, EventArgs e)
    {
        IsPrint = true; 
    }
    protected void btn_Search_Click(object sender, EventArgs e)
    {
        //---------------------------------------
        FilterVar.EmpNo = txt_MemberId_Search.Text;
        FilterVar.FirstName = txt_FirstName_Search.Text;
        //FilterVar.FamilyName = txt_LastName_Search.Text;
        FilterVar.Rank = ddl_Rank_Search.SelectedIndex;
        FilterVar.ExpFrom = txt_Exp_From.Text;
        FilterVar.ExpTo = txt_Exp_To.Text;
        FilterVar.Nationality = ddl_Nationality.SelectedIndex;
        FilterVar.onBoardFrom = txt_SignOn_Date.Text;
        FilterVar.onBoardTo = txt_SignOff_Date.Text;
        FilterVar.Vessel = ddl_Vessel.SelectedIndex;
        FilterVar.Status = ddl_CrewStatus_Search.SelectedIndex;
        FilterVar.AgeFrom = txt_Age_From.Text;
        FilterVar.AgeTo = txt_Age_To.Text;
        FilterVar.PassportNo = txt_PassportNo.Text;
        FilterVar.Owner = ddl_Owner.SelectedIndex;
        FilterVar.RecOff = ddl_Recr_Office.SelectedIndex;
        FilterVar.VesselType = ddl_VesselType.SelectedIndex;
        FilterVar.UsVisa = chk_UsVisa.Checked;
        FilterVar.SchengenVisa = chk_SchengenVisa.Checked;
        FilterVar.FamilyMems = chk_Family.Checked;
        Session["FilterVar"] = FilterVar;  
        //---------------------------------------
        lblGrid.Visible = true;  
        if (checkform() == true)
        {
            ProcessSelectCrewMemberPrimaryDetails_Search obj = new ProcessSelectCrewMemberPrimaryDetails_Search();
            BoCrewSearch Member = new BoCrewSearch();
            Member.LoginId = Convert.ToInt32(Session["loginid"].ToString());
            Member.CrewNumber = txt_MemberId_Search.Text;
            Member.FirstName = txt_FirstName_Search.Text;
            //Member.LastName = txt_LastName_Search.Text;
            Member.Nationality = (ddl_Nationality.SelectedIndex <= 0) ? 0 : Convert.ToInt32(ddl_Nationality.SelectedValue);
            Member.CrewStatusId = Convert.ToInt32(ddl_CrewStatus_Search.SelectedValue.ToString());
            Member.Rank = Convert.ToInt32(ddl_Rank_Search.SelectedValue.ToString());
            Member.PassportNo = txt_PassportNo.Text.Trim();
            Member.RecOffId = Convert.ToInt32(ddl_Recr_Office.SelectedValue.ToString());
            
            Member.FromDate=txt_SignOn_Date.Text.Trim();
            Member.ToDate =txt_SignOff_Date.Text.Trim();

            Member.VesselId = Convert.ToInt32(ddl_Vessel.SelectedValue);
            Member.VesselType = Convert.ToInt32(ddl_VesselType.SelectedValue);
            Member.Owner = Convert.ToInt32(ddl_Owner.SelectedValue);
           // Member.ReliefDue = txt_ReliefDue.Text;  

            if (chk_UsVisa.Checked==true)
            {
        
                Member.USvisa =1;
            }
            else 
            {
                Member.USvisa = 0;
            }

            if (chk_SchengenVisa.Checked == true)
            {

                Member.SchengenVisa = 1;
            }
            else
            {
                Member.SchengenVisa = 0;
            }

            if (chk_Family.Checked == true)
            {

                Member.FamilyMember = 1;
            }
            else
            {
                Member.FamilyMember = 0;
            }

            if (txt_Age_From.Text.Trim() != "")
            {
                Member.AgeFrom = Convert.ToInt32(txt_Age_From.Text.Trim());
                Member.AgeTo = Convert.ToInt32(txt_Age_To.Text.Trim());
            }
            else
            {
                Member.AgeFrom = 0;
                Member.AgeTo = 0;
            }
            if (txt_Exp_From.Text.Trim() != "")
            {
                Member.ExpFrom = Convert.ToInt32(txt_Exp_From.Text.Trim());
                Member.ExpTo = Convert.ToInt32(txt_Exp_To.Text.Trim());
            }
            else
            {
                Member.ExpFrom = 0;
                Member.ExpTo = 0;
            }
             
            obj.CrewSearch = Member;
            obj.Invoke();
            GridView1.DataSource = obj.ResultSet.Tables[0];
            Session["ShowRep"] = obj.ResultSet.Tables[0];
            GridView1.DataBind();
            CrewCount.Text = GridView1.Rows.Count.ToString();
        }
    }
    private Boolean checkform()
    {
        if (((this.txt_Exp_From.Text != "") && (this.txt_Exp_To.Text == "")) || ((this.txt_Exp_From.Text == "") && (this.txt_Exp_To.Text != "")))
        {
            this.lblmessage.Text = "Enter both from and to experience";
            return false;
        }
         if (this.txt_Exp_From.Text != "" && this.txt_Exp_To.Text != "")
        {
            if (Convert.ToInt16(txt_Exp_From.Text) > Convert.ToInt16(this.txt_Exp_To.Text))
            {
                this.lblmessage.Text = "From Exp. Should be less than To Exp.";
                return false;
            }
        
        }
         if(((txt_SignOn_Date.Text!="")&&(this.txt_SignOff_Date.Text==""))||  ((txt_SignOn_Date.Text=="")&&(this.txt_SignOff_Date.Text!="")))
        {
            this.lblmessage.Text = "Enter Both From Date and To Date";
            return false;
        }
        if ((txt_SignOn_Date.Text != "") && (this.txt_SignOff_Date.Text != ""))
        {

            if (Convert.ToDateTime(txt_SignOn_Date.Text) > Convert.ToDateTime(this.txt_SignOff_Date.Text))
            {
                this.lblmessage.Text = "To date should be greater than or equal to From date";
                return false;
            }
        
        }
        if (((this.txt_Age_From.Text != "") && (this.txt_Age_To.Text == "")) || ((this.txt_Age_From.Text == "") && (this.txt_Age_To.Text != "")))
        {
            this.lblmessage.Text = "Enter both from and to age";
            return false;
        }
        if (this.txt_Age_From.Text != "" && this.txt_Age_To.Text != "")
        {
            if (Convert.ToInt16(txt_Age_From.Text) > Convert.ToInt16(this.txt_Age_To.Text))
            {
                this.lblmessage.Text = "From age sholud be less than or equal to to age";
                return false;
            }
        
        }
        
            return true;
        
 
    }
    protected void GridView1_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        GridView1.PageIndex = e.NewPageIndex;
        BindGrid(GridView1.Attributes["MySort"]);
    }
    protected void GridView1_OnPageIndexChanged(object sender, EventArgs e)
    {
    }
    // VIEW ONLY
    protected void GridView_SelectIndexChanged(object sender, EventArgs e)
    {
        HiddenField hfd = (HiddenField)GridView1.Rows[GridView1.SelectedIndex].FindControl("HiddenCrewNumber");
            
        if (hfd.Value.Trim().StartsWith("F"))
        {
            int familycrewid=0;
            Session["Mode"] = "View";
            DataTable dts = cls_SearchReliever.SelectParentOfFamilyMember(hfd.Value.ToString());
            foreach(DataRow dr in dts.Rows)
            {
                familycrewid =Convert.ToInt32(dr["CrewId"].ToString());
            }
            Session["CrewId"] = familycrewid.ToString();
            Response.Redirect("crewdetails.aspx?id=" + familycrewid.ToString());
        }
        else
        {
            HiddenField hfd1;
            hfd1 = (HiddenField)GridView1.Rows[GridView1.SelectedIndex].FindControl("HiddenId");
            Session["Mode"] = "View";
            Session["CrewId"] = hfd1.Value.ToString();
            Response.Redirect("crewdetails.aspx?id=" + hfd1.Value.ToString());
        }
        
    }
    // EDIT THE RECORD
    protected void Row_Editing(object sender, GridViewEditEventArgs e)
    {
        HiddenField hfd;
        hfd = (HiddenField)GridView1.Rows[e.NewEditIndex].FindControl("HiddenId");
        Session["Mode"] = "Edit";
        Session["CrewId"] = hfd.Value.ToString();
        string crewnumber = ((HiddenField)GridView1.Rows[e.NewEditIndex].FindControl("HiddenCrewNumber")).Value.ToUpper().Trim();
        if (Alerts.getLocation()=="Y")
        {
            if (crewnumber.StartsWith("S")) 
            {
                lblmessage.Text = "Sorry! you are only able to view this record."; 
                return; 
            }
        }
        Response.Redirect("crewdetails.aspx?id=" + hfd.Value.ToString());
    }
    // DELETE THE RECORD
    protected void Row_Deleting(object sender, EventArgs e)
    {
        
        //hfd = (HiddenField)GridView1.Rows[GridView1.SelectedIndex].FindControl("HiddenId");
        //Response.Redirect("crewdetails.aspx?id=" + hfd.Value.ToString());
    }
    protected void DataBound(object sender, EventArgs e)
    {
        if (chk_Family.Checked)
        {
            GridView1.Columns[0].Visible = true;
            GridView1.Columns[1].Visible = false;
            GridView1.Columns[2].Visible = false;
            return;
        }

        GridView1.Columns[0].Visible = true;
        GridView1.Columns[1].Visible = false;
        GridView1.Columns[2].Visible = false;
        try
        {
            // Can Add
            if (Obj.isAdd)
            {

            }

            // Can Modify
            if (Obj.isEdit)
            {
                GridView1.Columns[1].Visible = true;
            }

            // Can Delete
            if (Obj.isDelete)
            {
                GridView1.Columns[2].Visible = false;
            }

            // Can Print
            if (Obj.isPrint)
            {
            }
            
        }
        catch{}
        
    }
    protected void GoToPage_TextChanged(object sender, EventArgs e)
    {
        TextBox txtGoToPage = (TextBox)sender;
        int pageindex = int.Parse(txtGoToPage.Text.Trim());
        if (GridView1.PageCount >= pageindex)
        {
            GridView1.PageIndex = pageindex - 1;
        }
        BindGrid(GridView1.Attributes["MySort"]);
    }
    protected void gvCustomers_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        if (e.NewPageIndex >= 0 && e.NewPageIndex <= GridView1.PageCount)
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindGrid(GridView1.Attributes["MySort"]);
        }
    }
    protected void on_Sorting(object sender, GridViewSortEventArgs e)
    {
        BindGrid(e.SortExpression);
    }
    protected void on_Sorted(object sender, EventArgs e)
    {

    }
    protected void GvCustomers_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        GridView gridView = (GridView)sender;

        if (gridView.SortExpression.Length > 0)
        {
            int cellIndex = -1;
            foreach (DataControlField field in gridView.Columns)
            {
                if (field.SortExpression == gridView.SortExpression)
                {
                    cellIndex = gridView.Columns.IndexOf(field);
                    break;
                }
            }

            if (cellIndex > -1)
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    //  this is a header row,
                    //  set the sort style
                    e.Row.Cells[cellIndex].CssClass = gridView.SortDirection == SortDirection.Ascending ? "sortascheaderstyle" : "sortdescheaderstyle";
                }
                else if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    //  this is an alternating row
                    e.Row.Cells[cellIndex].CssClass = e.Row.RowIndex % 2 == 0 ? "sortalternatingrowstyle" : "sortrowstyle";
                }
            }
        }

        if (e.Row.RowType == DataControlRowType.Pager)
        {
            Label lblTotalNumberOfPages = (Label)e.Row.FindControl("lblTotalNumberOfPages");
            lblTotalNumberOfPages.Text = gridView.PageCount.ToString();

            TextBox txtGoToPage = (TextBox)e.Row.FindControl("txtGoToPage");
            txtGoToPage.Text = (gridView.PageIndex + 1).ToString();

            //DropDownList ddlPageSize = (DropDownList)e.Row.FindControl("ddlPageSize");
            //ddlPageSize.SelectedValue = gridView.PageSize.ToString();
        }
    }
    protected void btn_Clear_Click(object sender, EventArgs e)
    {
        txt_MemberId_Search.Text = "";
        txt_FirstName_Search.Text = "";
        //txt_LastName_Search.Text = "";
        ddl_Rank_Search.SelectedIndex = 0;
        ddl_Nationality.SelectedIndex = 0; 
        ddl_Vessel.SelectedIndex = 0;
        txt_SignOn_Date.Text = "";
        txt_SignOff_Date.Text = "";
        ddl_CrewStatus_Search.SelectedIndex = 0;
        txt_PassportNo.Text = "";
        ddl_Owner.SelectedIndex = 0;
        ddl_Recr_Office.SelectedIndex = 0;
        ddl_VesselType.SelectedIndex = 0;
        chk_UsVisa.Checked = false;
        chk_SchengenVisa.Checked = false;
        txt_Exp_From.Text = "";
        txt_Exp_To.Text= "";
        txt_Age_From.Text = "";
        txt_Age_To.Text = ""; 
        GridView1.DataBind();
        lblGrid.Visible = false;
        CrewCount.Text = "";
    }
    protected void btn_Add_Click(object sender, EventArgs e)
    {
        dvConfirmAddNew.Visible = true;
    }
    protected void btnWeb_Click(object sender, EventArgs e)
    {
        Response.Redirect("CandidateDetailInformation.aspx");
    }
    protected void GridView1_RowCreated(Object sender, GridViewRowEventArgs e)
            {

                if (e.Row.RowType == DataControlRowType.DataRow)
                {

                    LinkButton lnk_Empno = (LinkButton)e.Row.FindControl("lnk_Empno");  //e.Row.Cells[0].Controls[0];
                    lnk_Empno.CommandArgument = e.Row.RowIndex.ToString();

                }

    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
             {
                 Label Lb_crewID;

        if (e.CommandName == "lnk_Emp")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow r = GridView1.Rows[index];
            Lb_crewID = (Label)r.FindControl("Lb_crewID");
            Session["Promotion_CrewID_Planning"] = Lb_crewID.Text;
            GridView1.SelectedIndex = index;
        }
}
    protected void Grid_PreRender(object sender, EventArgs e)
    {
        if (GridView1.Rows.Count <= 0)
        {
            if (IsPostBack) 
            { 
                lblGrid.Text = "No Records Found."; 
            } 
        }
        for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
        {
            if (GridView1.Rows[i].Cells[11].Text.ToUpper() == "NTBR")
            {
                GridView1.Rows[i].BackColor = System.Drawing.Color.FromName("#fcc2bc");
            }
        }
    }
    protected void btnNewGo_OnClick(object sender, EventArgs e)
    {
        int CandidateId = 0;
        if (txtComments.Text.Trim() == "")
        {
            lblNewGoMsg.Text = "Please Enter Approval No to Continue..";
            return; 
        }
        DataTable dtChk = Budget.getTable("select CandidateId from DBO.CANDIDATEPERSONALDETAILS CD " +
                                        "where ApprovalId='" + txtComments.Text.Trim() + "' AND STATUS=3 AND CD.CandidateId NOT IN (SELECT CPD.CandidateId FROM DBO.CREWPERSONALDETAILS CPD WHERE CPD.CandidateId IS NOT NULL)").Tables[0];
        if (dtChk.Rows.Count <= 0)
        {
            lblNewGoMsg.Text = "Invalid Approval No.";
            return;
        }
        else
        {
            CandidateId = Common.CastAsInt32(dtChk.Rows[0]["CandidateId"]);
        }
        //-------------------------------------
        Session["CrewId"] = "";
        Response.Redirect("CrewDetails.aspx?Mode=New&CandidateId=" + CandidateId.ToString()); 
    }
    protected void btnNewCancel_OnClick(object sender, EventArgs e)
    {
        dvConfirmAddNew.Visible = false;
    }


    protected void ddl_Vessel_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_Vessel.SelectedIndex != 0)
        {
            BindCrewStatusDropDownForVessel();
        }
        else
        {
            BindCrewStatusDropDown();
        }
    }
}
