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
using System.IO;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Transactions;
using System.Drawing;

public partial class Modules_HRD_Vacancy_VacancyDetails : System.Web.UI.Page
{
    Authority Obj;
    string BCCMail;
    string ToMail;
    string UserFullName;
    string crewFullName;
    string crewRank;
    string EmailBody;
    Int32 candidateId;
    string EmailSubject;
    string randomPwd;
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
       

        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 1);
        if (chpageauth <= 0)
        {
            Response.Redirect("../AuthorityError.aspx");
        }
        //*******************
       
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
            if (Convert.ToString(Session["UserName"]) == "")
            {
                Response.Redirect("Login.aspx");
            }
            if (!(IsPostBack))
            {
                BindOwnerPoolDropDown();
                BindVessel();
               
                BindRankDropDown();
                BindVesselType();
                BindRecruitingOffice();
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
            return;
        }
    }

    private void BindOwnerPoolDropDown()
    {
        DataTable dt;
        dt = VesselDetailsGeneral.selectDataOwnerName();
        ddl_Owner.DataValueField = "Ownerid";
        ddl_Owner.DataTextField = "OwnerShortName";
        ddl_Owner.DataSource = dt;
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
        ddl_VesselType.DataSource = ds;
        ddl_VesselType.DataBind();
        ddl_VesselType.Items.Insert(0, new ListItem("< Select >", "0"));
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
    private void BindRankDropDown()
    {
        ProcessSelectRank obj = new ProcessSelectRank();
        obj.Invoke();
        ddl_Rank_Search.DataSource = obj.ResultSet.Tables[0];
        ddl_Rank_Search.DataTextField = "RankName";
        ddl_Rank_Search.DataValueField = "RankId";
        ddl_Rank_Search.DataBind();

    }
    protected void btn_Clear_Click(object sender, EventArgs e)
    {
      
       
        ddl_Rank_Search.SelectedIndex = 0;
        
        ddl_Vessel.SelectedIndex = 0;
       
        ddl_Owner.SelectedIndex = 0;
        ddl_Recr_Office.SelectedIndex = 0;
        ddl_VesselType.SelectedIndex = 0;
        chk_UsVisa.Checked = false;
        chk_SchengenVisa.Checked = false;
        Gv_Vacancy.DataSource = null;
        Gv_Vacancy.DataBind();
        
        tblCandidate.Visible = false;
        rptData.DataSource = null;
        rptData.DataBind();
        lblGrid.Visible = false;
        VacancyCount.Text = "";
    }

    protected void btn_Search_Click(object sender, EventArgs e)
    {
        Gv_Vacancy.DataSource = null;
        Session["Vacancy"] = null;
        Gv_Vacancy.DataBind();
        VacancyCount.Text = "0";
        string sql = "Select hv.VacancyID As VacancyID , o.OwnerName As Owner,v.VesselName As VesselName, r.RankName As Rank, REPLACE(CONVERT(CHAR(11), Joining_Date, 106),' ','-') As  DOJ,  REPLACE(CONVERT(CHAR(11), Proposal_Date, 106),' ','-') As  DOP, Vt.VesselTypeName As VesselTypeName,ISNULL((Select Count(HVC_CandidateId) As CandidateCount from HRD_Vacancy_CandidateMapping where HVC_Status = 'A' and HVC_VacancyId = hv.VacancyID),0) As CandidateCount  from HRD_Vacancy hv with(nolock) Inner Join Owner o with(nolock) on hv.OwnerId = o.OwnerId Inner Join Rank r with(nolock) on hv.RankId = r.RankId LEFT OUTER JOIN Vessel v with(nolock) on hv.VesselId = v.VesselId LEFT OUTER JOIN VesselType vt with(nolock) on v.VesselTypeId = vt.VesselTypeId where hv.Status = 'A' and 1=1";
        if (chk_UsVisa.Checked == true)
        {

            sql = sql + " And hv.IsUSVisa = 1";
        }
        //else
        //{
        //    sql = sql + " And hv.IsUSVisa = 0";
        //}

        if (chk_SchengenVisa.Checked == true)
        {

            sql = sql + " And hv.IsSchengenVisa = 1";
        }
        //else
        //{
        //    sql = sql + " And hv.IsSchengenVisa = 0";
        //}

        if (ddl_Rank_Search.SelectedIndex > 0)
        {
            sql = sql + " And hv.RankId = " + Convert.ToInt32(ddl_Rank_Search.SelectedValue) +"";
        }

        if (ddl_Owner.SelectedIndex > 0)
        {
            sql = sql + " And hv.OwnerId = " + Convert.ToInt32(ddl_Owner.SelectedValue) + "";
        }

        if (ddl_Vessel.SelectedIndex > 0)
        {
            sql = sql + " And hv.VesselId = " + Convert.ToInt32(ddl_Vessel.SelectedValue) + "";
        }

        if (ddl_VesselType.SelectedIndex > 0)
        {
            sql = sql + " And vt.VesselTypeId = " + Convert.ToInt32(ddl_VesselType.SelectedValue) + "";
        }

        if (ddl_Recr_Office.SelectedIndex > 0)
        {
            sql = sql + " And hv.Recruiter_Office = " + Convert.ToInt32(ddl_Recr_Office.SelectedValue) + "";
        }

        if (ddlVStatus.SelectedValue == "1")
        {
            sql = sql + " And hv.Vacancy_Status = '1' ";
        }
        else
        {
            sql = sql + " And hv.Vacancy_Status = '0' ";
        }

        DataTable dt = Budget.getTable(sql).Tables[0];

        if (dt.Rows.Count > 0)
        {
            Gv_Vacancy.DataSource = dt;
            Session["Vacancy"] = dt;
            Gv_Vacancy.DataBind();
            VacancyCount.Text = Gv_Vacancy.Rows.Count.ToString();

        }
        else
        {
            Gv_Vacancy.DataSource = null;
            Session["Vacancy"] = null;
            Gv_Vacancy.DataBind();
            VacancyCount.Text = "0";
        }
    }

    protected void btn_Add_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "PP", "window.open('AddUpdateVacancy.aspx?Mode=New&Vacancyid=0&Key=0');", true);
    }

    protected void Gv_Vacancy_PreRender(object sender, EventArgs e)
    {
        if (Gv_Vacancy.Rows.Count <= 0)
        {
            if (IsPostBack)
            {
                lblGrid.Text = "No Records Found.";
            }
        }
        //for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
        //{
        //    if (GridView1.Rows[i].Cells[11].Text.ToUpper() == "NTBR")
        //    {
        //        GridView1.Rows[i].BackColor = System.Drawing.Color.FromName("#fcc2bc");
        //    }
        //}
    }

    protected void Gv_Vacancy_RowCommand(object sender, GridViewCommandEventArgs e)
    {
       

        if (e.CommandName == "Modify")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow r = Gv_Vacancy.Rows[index];

            Gv_Vacancy.SelectedIndex = index;
            HiddenField hfd = (HiddenField)Gv_Vacancy.Rows[index].FindControl("hdnVacancyId");
            Session["Mode"] = "View";
            Session["VacancyId"] = hfd.Value.ToString();
            tblCandidate.Visible = true;
            binddata(Convert.ToInt32(hfd.Value));
            //ScriptManager.RegisterStartupScript(Page, this.GetType(), "PP", "window.open('AddUpdateVacancy.aspx?Mode=" + Session["Mode"] + "&Vacancyid=" + Session["VacancyId"].ToString() + "');", true);
        }
        //else
        //{
        //    tblCandidate.Visible = true;
        //    int index = Convert.ToInt32(e.CommandArgument);
        //    HiddenField hfd = (HiddenField)Gv_Vacancy.Rows[index].FindControl("hdnVacancyId");
        //    binddata(Convert.ToInt32(hfd.Value));
        //}
        //if (e.CommandName == "AssignVacancy")
        //{
        //    int index = Convert.ToInt32(e.CommandArgument);
        //    GridViewRow r = Gv_Vacancy.Rows[index];
        //}

    }

    //protected void Gv_Vacancy_RowCreated(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {

    //        LinkButton lnk_Vacancy = (LinkButton)e.Row.FindControl("lnk_VacancyId");  //e.Row.Cells[0].Controls[0];
    //        lnk_Vacancy.CommandArgument = e.Row.RowIndex.ToString();

    //    }
    //}

    protected void Gv_Vacancy_RowDataBound(object sender, GridViewRowEventArgs e)
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

        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(Gv_Vacancy, "RowSelect$" + e.Row.RowIndex);
        //   // e.Row.ToolTip = "Click to select this row.";
        //}
    }

    private void BindGrid(String Sort)
    {
        string sql = "Select hv.VacancyID As VacancyID , o.OwnerName As Owner,ISNULL(v.VesselName,'') As VesselName, r.RankName As Rank, REPLACE(CONVERT(CHAR(11), Joining_Date, 106),' ','-') As  DOJ,  REPLACE(CONVERT(CHAR(11), Proposal_Date, 106),' ','-') As  DOP, ISNULL(Vt.VesselTypeName,'') As VesselTypeName,ISNULL((Select Count(HVC_CandidateId) As CandidateCount from HRD_Vacancy_CandidateMapping where HVC_Status = 'A' and HVC_VacancyId = hv.VacancyID),0) As CandidateCount from HRD_Vacancy hv with(nolock) Inner Join Owner o with(nolock) on hv.OwnerId = o.OwnerId Inner Join Rank r with(nolock) on hv.RankId = r.RankId LEFT OUTER JOIN Vessel v with(nolock) on hv.VesselId = v.VesselId  LEFT OUTER Join VesselType vt with(nolock) on v.VesselTypeId = vt.VesselTypeId where hv.Status = 'A' and 1=1";
        if (chk_UsVisa.Checked == true)
        {

            sql = sql + " And hv.IsUSVisa = 1";
        }
        //else
        //{
        //    sql = sql + " And hv.IsUSVisa = 0";
        //}

        if (chk_SchengenVisa.Checked == true)
        {

            sql = sql + " And hv.IsSchengenVisa = 1";
        }
        //else
        //{
        //    sql = sql + " And hv.IsSchengenVisa = 0";
        //}

        if (ddl_Rank_Search.SelectedIndex > 0)
        {
            sql = sql + " And hv.RankId = " + Convert.ToInt32(ddl_Rank_Search.SelectedValue) + "";
        }

        if (ddl_Owner.SelectedIndex > 0)
        {
            sql = sql + " And hv.OwnerId = " + Convert.ToInt32(ddl_Owner.SelectedValue) + "";
        }

        if (ddl_Vessel.SelectedIndex > 0)
        {
            sql = sql + " And hv.VesselId = " + Convert.ToInt32(ddl_Vessel.SelectedValue) + "";
        }

        if (ddl_VesselType.SelectedIndex > 0)
        {
            sql = sql + " And vt.VesselTypeId = " + Convert.ToInt32(ddl_VesselType.SelectedValue) + "";
        }

        if (ddl_Recr_Office.SelectedIndex > 0)
        {
            sql = sql + " And hv.Recruiter_Office = " + Convert.ToInt32(ddl_Recr_Office.SelectedValue) + "";
        }

        DataTable dt = Budget.getTable(sql).Tables[0];

        if (dt.Rows.Count > 0)
        {

            dt.DefaultView.Sort = Sort;
            Gv_Vacancy.DataSource = dt;
            Session["Vacancy"] = dt;
            Gv_Vacancy.DataBind();
            Gv_Vacancy.Attributes.Add("MySort", Sort);
            VacancyCount.Text = Gv_Vacancy.Rows.Count.ToString();
        }

   
    }

    protected void Row_Deleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int intModifiedBy = Convert.ToInt32(Session["loginid"].ToString());
            HiddenField hfdVacancy;
            hfdVacancy = (HiddenField)Gv_Vacancy.Rows[e.RowIndex].FindControl("hdnVacancyId");
            // id = Convert.ToInt32(hfdVacancy.Value.ToString());
            // ManningAgent.deleteManningAgentDetails("deleteMinningAgent", id, intModifiedBy);
            int vacancyid = Convert.ToInt32(hfdVacancy.Value.ToString());
            Common.Execute_Procedures_Select_ByQueryCMS("UPDATE DBO.ProposalToOwner SET PTO_VacancyId = null WHERE PTO_VacancyId=" + vacancyid + " and PTO_StatusId = 'A' ");
            Common.Execute_Procedures_Select_ByQueryCMS("UPDATE DBO.HRD_Vacancy_CandidateMapping SET HVC_Status = 'D', HVC_modifiedBy = '" + intModifiedBy + "' , HVC_ModifiedOn = GETDATE() WHERE HVC_VacancyId=" + vacancyid + " and HVC_Status = 'A' ");
            Common.Execute_Procedures_Select_ByQueryCMS("UPDATE DBO.HRD_Vacancy SET Status = 'D', ModifiedBy = '" + intModifiedBy + "' , ModifiedOn = GETDATE() WHERE VacancyID=" + vacancyid + " and Status = 'A' ");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "axdxasda", "alert('Vacancy deleted successfully.');", true);
            tblCandidate.Visible = false;
            rptData.DataSource = null;
            rptData.DataBind();
            BindVacancyData();

        }
        catch(Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "axdxasda", "alert('Delete record Error : "+ex.Message.ToString()+"');", true);
        }
    }

    //protected void Row_Editing(object sender, GridViewEditEventArgs e)
    //{
    //    HiddenField hfd;
    //    hfd = (HiddenField)Gv_Vacancy.Rows[e.NewEditIndex].FindControl("hdnVacancyId");
    //    Session["Mode"] = "Edit";
    //    Session["VacancyId"] = hfd.Value.ToString();
    //    ScriptManager.RegisterStartupScript(Page, this.GetType(), "PP", "window.open('AddUpdateVacancy.aspx?Mode="+ Session["Mode"] + "&Vacancyid=" + Session["VacancyId"].ToString() + "');", true);
    //}

    protected void Gv_Vacancy_SelectIndexChanged(object sender, EventArgs e)
    {

        foreach (GridViewRow row in Gv_Vacancy.Rows)
        {
            if (row.RowIndex == Gv_Vacancy.SelectedIndex)
            {
                row.BackColor = ColorTranslator.FromHtml("#A1DCF2");
                row.ForeColor = ColorTranslator.FromHtml("#000000");
                row.ToolTip = string.Empty;
            }
            else
            {
                row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                row.ToolTip = "Click to select this row.";
            }
        }
        HiddenField hfd = (HiddenField)Gv_Vacancy.Rows[Gv_Vacancy.SelectedIndex].FindControl("hdnVacancyId");
        Session["Mode"] = "View";
        Session["VacancyId"] = hfd.Value.ToString();
        tblCandidate.Visible = true;
        binddata(Convert.ToInt32(hfd.Value.ToString()));
    }

    protected void on_Sorting(object sender, GridViewSortEventArgs e)
    {
        BindGrid(e.SortExpression);
    }

    protected void on_Sorted(object sender, EventArgs e)
    {

    }

    protected void DataBound(object sender, EventArgs e)
    {

    }

    protected void gvCustomers_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        if (e.NewPageIndex >= 0 && e.NewPageIndex <= Gv_Vacancy.PageCount)
        {
            Gv_Vacancy.PageIndex = e.NewPageIndex;
            BindGrid(Gv_Vacancy.Attributes["MySort"]);
        }
    }
    protected void GoToPage_TextChanged(object sender, EventArgs e)
    {
        TextBox txtGoToPage = (TextBox)sender;
        int pageindex = int.Parse(txtGoToPage.Text.Trim());
        if (Gv_Vacancy.PageCount >= pageindex)
        {
            Gv_Vacancy.PageIndex = pageindex - 1;
        }
        BindGrid(Gv_Vacancy.Attributes["MySort"]);
    }

    protected void btnCloseAddSignerDiv_Click(object sender, EventArgs e)
    {
        dvAddSignerDetails.Visible = false;
        BindGrid(Gv_Vacancy.Attributes["MySort"]);
    }

    protected void btnAssignCrew_Click(object sender, EventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        HiddenField hdnVacancyId = (HiddenField)btn.Parent.FindControl("hdnVacancyId");
        dvAddSignerDetails.Visible = true;
        ifmAddSignerDetails.Attributes.Add("src", "AssignCrewtoVacancy.aspx?VacancyId=" + hdnVacancyId.Value.ToString());
    }

    protected void lbWithDraw_click(object sender, EventArgs e)
    {
        try
        {
            LinkButton ib = ((LinkButton)sender);
            Int32   candidateId = 0;
            candidateId = Convert.ToInt32(ib.CommandArgument);
            hdnCandidateId.Value = Convert.ToString(candidateId);
            // RepeaterItem Rptitem = (RepeaterItem)ib.NamingContainer;
            //  HiddenField hdnVacancyId = (HiddenField)Rptitem.FindControl("hdnVacancyId");
            HiddenField hdnVacancyId =  (HiddenField)ib.Parent.FindControl("hdnVacancyId");
            if (hdnVacancyId.Value != "")
            {
                lblVacancyId.Text = hdnVacancyId.Value;
            }
            string strwithDraw = "W";
            Common.Execute_Procedures_Select_ByQueryCMS("UPDATE DBO.ProposalToOwner SET PTO_OwnerAppRej='" + strwithDraw + "' , PTO_OwnerAppRejDate = GETDATE(), PTO_VacancyId = null WHERE PTO_CandidateId=" + hdnCandidateId.Value.ToString() + " and PTO_StatusId = 'A' AND  PTO_VacancyId = "+ Convert.ToInt32(hdnVacancyId.Value) + "");
            if (hdnVacancyId.Value != "")
            {
                Common.Execute_Procedures_Select_ByQueryCMS("UPDATE DBO.HRD_Vacancy_CandidateMapping SET HVC_OwnerAppRej='" + strwithDraw + "' , HVC_OwnerAppRejDate = GETDATE(), HVC_Status = 'D' WHERE HVC_CandidateId=" + hdnCandidateId.Value.ToString() + " and HVC_VacancyId=" + Convert.ToInt32(hdnVacancyId.Value) + "   and HVC_Status = 'A' ");
            }
           
            binddata(Convert.ToInt32(Session["VacancyId"]));
            ScriptManager.RegisterStartupScript(this, this.GetType(), "axdxasda", "alert('Crew proposal withdraw successfully.');", true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "axdxasda", "alert('"+ ex.Message + "');", true);
        }
    }
    protected void lbResendProposal_click(object sender, EventArgs e)
    {
        try
        {
            hdnResendProposal.Value = "true";
            LinkButton ib = ((LinkButton)sender);
            Int32 candidateId = 0;
            candidateId = Convert.ToInt32(ib.CommandArgument);
            hdnCandidateId.Value = Convert.ToString(candidateId);
            HiddenField hdnVacancyId = (HiddenField)ib.Parent.FindControl("hdnVacancyId");
            if (hdnVacancyId.Value != "")
            {
                lblVacancyId.Text = hdnVacancyId.Value;
            }
            dvProposalToOwner.Visible = true;
            BindDDlOwnList();
           
            GetProposalToOwnerDetail(candidateId);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "axdxasda", "alert('" + ex.Message + "');", true);
        }
    }
    protected void GetProposalToOwnerDetail(int candidateId)
    {
        string sql = "Select top 1 * from ProposalToOwner with(nolock) where PTO_CandidateId = " + candidateId + " Order by PTO_Id Desc";
        DataTable dt = Budget.getTable(sql).Tables[0];
        if (dt.Rows.Count > 0)
        {
            ddl_Ownerlist.SelectedValue = dt.Rows[0]["PTO_OwnerId"].ToString();
            //txtFromAddress.Text = dt.Rows[0]["PTO_FromEmail"].ToString();
            //txtToEmail.Text = dt.Rows[0]["PTO_ToEmail"].ToString();
            //txtBCCEmail.Text = dt.Rows[0]["PTO_BCCEmail"].ToString();
            //txtSubject.Text = dt.Rows[0]["PTO_Subject"].ToString();
            //litMessage.Text = dt.Rows[0]["PTO_Body"].ToString();
            //randomPwd = dt.Rows[0]["PTO_RandomPassword"].ToString();
            BindDDlVessel(Convert.ToInt32(ddl_Ownerlist.SelectedValue));
            txtManningOfficerRemarks.Text = dt.Rows[0]["PTO_OfficerRemarks"].ToString();
            ddlVesselOwner.SelectedValue = dt.Rows[0]["PTO_VesselId"].ToString();
            lblVacancyId.Text = dt.Rows[0]["PTO_VacancyId"].ToString();
        }
    }
    protected void Delete_Candidate(object sender, EventArgs e)
    {

        ImageButton ib = ((ImageButton)sender);
        int candidateid = Convert.ToInt32(ib.CommandArgument);
        HiddenField hdnVacancyId = (HiddenField)ib.Parent.FindControl("hdnVacancyId");
        Common.Execute_Procedures_Select_ByQueryCMS("Update ProposalToOwner SET PTO_VacancyId = null where PTO_CandidateId = " + Convert.ToInt32(candidateid) + " and PTO_VacancyId = " + hdnVacancyId.Value + " ");
        Common.Execute_Procedures_Select_ByQueryCMS("Update HRD_Vacancy_CandidateMapping SET HVC_Status = 'D' where HVC_CandidateId = " + Convert.ToInt32(candidateid) + " and HVC_VacancyId = " + hdnVacancyId.Value + " ");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "axdxasda", "alert('Candidate removed successfully for selected Vacancy.');", true);
        binddata(Convert.ToInt32(Session["VacancyId"]));
    }

    protected void rptData_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            HiddenField statusName = e.Item.FindControl("hdfStatusName") as HiddenField;
            HiddenField clientStatus = e.Item.FindControl("hdnClientStatus") as HiddenField;
            LinkButton lkbutton = e.Item.FindControl("lbProposalToOwner") as LinkButton;
            LinkButton lkResendProposal = e.Item.FindControl("lbResendProposal") as LinkButton;
            LinkButton lbWithDraw = e.Item.FindControl("lbWithDraw") as LinkButton;
            Label lblSlase = e.Item.FindControl("lblslase") as Label;
            HiddenField crewnumber = e.Item.FindControl("hdnCrewNumber") as HiddenField;
            if (statusName.Value.Trim().ToUpper() == "READY FOR PROPOSAL" && string.IsNullOrWhiteSpace(crewnumber.Value))
            {
                lkbutton.Visible = true;
                if (!string.IsNullOrWhiteSpace(clientStatus.Value) && clientStatus.Value != "" && (clientStatus.Value.Trim().ToUpper() == "AWAITING CLIENT APPROVAL" || clientStatus.Value.Trim().ToUpper() == "CLIENT APPROVED"))
                {
                    lkbutton.Visible = false;
                    lkResendProposal.Visible = true;
                    lbWithDraw.Visible = true;
                    lblSlase.Visible = true;
                }
            }
            else
            {
                lkbutton.Visible = false;
            }
        }
    }

    private void binddata(int vacancyid)
    {
        try
        {
            string Query = "select Row_number() over(order by CANDIDATEID )RowNumber,candidateid,isnull(ModifiedBy,'WebSite') as ModifiedBy,ModifiedOn, " +
                       "(Select RankCode from dbo.rank where Rank.Rankid=cpd.RankAppliedId) as Rank, " +
                       "FirstName + ' ' + MiddleName + ' ' + Lastname as [Name],City, " +
                       "(select NationalityCode from dbo.Country where countryid=cpd.nationalityid) as Country,isnull(FileName,'') as FileName,replace(convert(varchar,AvailableFrom,106) ,' ','-') as AvailableFrom,ContactNo,ContactNo2,EmailId," +
                       "Status,StatusName=(case " +
                       "when isnull(Status,1)=1 then 'Applicant' " +
                       "when isnull(Status,1)=2 then 'Awaiting Manning Approval' " +
                       "when isnull(Status,1)=3 then 'Ready for Proposal' " +
                       "when isnull(Status,1)=4 then 'Changes to Manning Rejected' " +
                       "when isnull(Status,1)=5 then 'Archived' " +
                       "else '-' End),(SELECT TOP 1 CD.DISCTYPE FROM dbo.CANDIDATEDISCUSSION CD WHERE CD.CANDIDATEID=CPD.CANDIDATEID ORDER BY DISC_DATE DESC) AS DISCTYPE, " +
                       "(SELECT TOP 1 REPLACE(CONVERT(VARCHAR,DISC_DATE,106),' ','-') + ' [ ' + (SELECT USERID FROM DBO.USERLOGIN WHERE LOGINID=CD.CALLEDBY) + ' ] ' + replace(CD.discussion,'''''','''')" +
                       " FROM dbo.CANDIDATEDISCUSSION CD WHERE CD.CANDIDATEID=CPD.CANDIDATEID ORDER BY DISC_DATE DESC) AS DISCUSSION, " +
                        " (Select top 1 CASE WHEN Isnull(PTO_OwnerAppRej,'') = 'A'  then 'Client Approved' WHEN Isnull(PTO_OwnerAppRej,'') = 'R'  then 'Client Rejected' WHEN Isnull(PTO_OwnerAppRej,'') = 'W'  then 'Withdraw Proposal'  ELSE 'Awaiting Client Approval'  END from ProposalToOwner with(nolock) where PTO_CandidateId = cpd.CandidateId) As ClientStatus, " +
                        " (Select CrewNumber from CrewPersonalDetails cp with(nolock) where cp.CandidateId = cpd.candidateid)  As CrewNumber, " +
                         " "+ vacancyid + " As VacancyId " +
                       "from dbo.CandidatePersonalDetails cpd ";

            string WhereClause = " Where 1=1 and cpd.candidateid not in (Select CandidateId from CrewPersonalDetails with(nolock) where isnull(CandidateId,0) <> 0)  and cpd.candidateid in (Select HVC_CandidateId from HRD_Vacancy_CandidateMapping with(nolock) where HVC_VacancyId = " + vacancyid + " and HVC_Status = 'A') ";

            WhereClause = WhereClause + " order by candidateid";
            DataTable dt = Budget.getTable(Query + WhereClause).Tables[0];
            if (dt.Rows.Count > 0)
            {
                DataView dtFiltered = dt.DefaultView;
                this.rptData.DataSource = dtFiltered;
                this.rptData.DataBind();
            }
            else
            {
                this.rptData.DataSource = null;
                this.rptData.DataBind();
            }
        }
         
        catch (Exception ex)
        {
            Response.Write("error  : " + ex.Message);
            
        }

        
    }

    protected void ProposalToOwner(object sender, EventArgs e)
    {
        try
        {
            //lblMessage.Text = "";
            LinkButton ib = ((LinkButton)sender);
            Int32 candidateId = 0;
            candidateId = Convert.ToInt32(ib.CommandArgument);
            hdnCandidateId.Value = Convert.ToString(candidateId);
            dvProposalToOwner.Visible = true;
            //RepeaterItem Rptitem = (RepeaterItem)ib.NamingContainer;
            //HiddenField hdnVacancyId = (HiddenField)Rptitem.FindControl("hdnVacancyId");
            HiddenField hdnVacancyId = (HiddenField)ib.Parent.FindControl("hdnVacancyId");
            if (hdnVacancyId.Value != "")
            {
                lblVacancyId.Text = hdnVacancyId.Value;
            }
            BindDDlOwnList();
          //  BindDDlVessel();
            GetloginIdDetail();
            GetCompanyDetail();
            GetProposalDetails(candidateId);
          //  GetMaildetail(candidateId);
            //ScriptManager.RegisterStartupScript(Page, this.GetType(), "abc", "window.open('ProposalSendOwner.aspx?candidate=" + candidateid.ToString() + "','','');", true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "axdxasda", "alert('" + ex.Message + "');", true);
        }
    }


    private void BindDDlOwnList()
    {
        string sql = "";
        sql = " select OwnerId,OwnerName from Owner with(nolock) where StatusId = 'A' order by OwnerName";
        DataTable dt = Budget.getTable(sql).Tables[0];
        ddl_Ownerlist.DataSource = dt;
        ddl_Ownerlist.DataTextField = "OwnerName";
        ddl_Ownerlist.DataValueField = "OwnerId";
        ddl_Ownerlist.DataBind();
        ddl_Ownerlist.Items.Insert(0, new ListItem(" < Select > ", "0"));
    }

    private void BindDDlVessel(int OwnerId)
    {
        string sql = "";
        sql = " Select VesselId, VesselName from Vessel with(nolock) where StatusId = 'A' and OwnerId = " + OwnerId + " and VesselStatusId in (1,3) order by VesselName ";
        DataTable dt = Budget.getTable(sql).Tables[0];
        ddlVesselOwner.DataSource = dt;
        ddlVesselOwner.DataTextField = "VesselName";
        ddlVesselOwner.DataValueField = "VesselId";
        ddlVesselOwner.DataBind();
        ddlVesselOwner.Items.Insert(0, new ListItem(" < Select > ", "0"));
    }

    private void GetloginIdDetail()
    {
        BCCMail = "";
        UserFullName = "";
        string sql = " Select Email, FirstName + ' ' + LastName As Full_Name  from UserLogin with(nolock) where LoginId = " + Session["loginid"].ToString();
        DataTable dt = Budget.getTable(sql).Tables[0];
        if (dt.Rows.Count > 0)
        {
            BCCMail = dt.Rows[0]["Email"].ToString().Trim();
            UserFullName = dt.Rows[0]["Full_Name"].ToString().Trim();
        }
    }

    private void GetCompanyDetail()
    {
        DataTable dtCompany = Budget.getTable("Select top 1 CompanyName from Company with(nolock) where DefaultCompany='Y' and  StatusId = 'A' ").Tables[0];
        if (dtCompany.Rows.Count > 0)
        {
            hdnCompany.Value = dtCompany.Rows[0]["CompanyName"].ToString().Trim();
        }
    }

    protected void ddl_Ownerlist_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //ToMail = "";
            //string sql = " Select Mail1, Mail2 from Owner with(nolock) where StatusId = 'A' and OwnerId = " + ddl_Ownerlist.SelectedValue.ToString();
            //DataTable dt = Budget.getTable(sql).Tables[0];
            //if (dt.Rows.Count > 0)
            //{
            //    string mail1 = "";
            //    string mail2 = "";
            //    foreach (DataRow dr in dt.Rows)
            //    {
            //        mail1 = dr["Mail1"].ToString().Trim();
            //        mail2 = dr["Mail2"].ToString().Trim();
            //    }

            //    ToMail = mail1 + ";" + mail2;
            //    txtToEmail.Text = ToMail;
            //}
            BindDDlVessel(Convert.ToInt32(ddl_Ownerlist.SelectedValue));
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "axdxasda", "alert('" + ex.Message + "');", true);
           
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        dvProposalToOwner.Visible = false;
        ddl_Ownerlist.SelectedIndex = 0;
        //ddl_Vessel.SelectedIndex = 0;
        txtFromAddress.Text = "";
        txtToEmail.Text = "";
        txtBCCEmail.Text = "";
        litMessage.Text = "";
        txtManningOfficerRemarks.Text = "";
        txtSubject.Text = "";
        binddata(Convert.ToInt32(Session["VacancyId"]));

    }
    private void GetMaildetail(int candidateId)
    {
        //string sql, sql1;
        string strcandidateId = Convert.ToString(candidateId);
        EmailBody = "";
        EmailSubject = "";
        randomPwd = "";
        DataTable dtRanPwd = Budget.getTable("EXEC DBO.GENEREATERamdomPassword " + strcandidateId + " ").Tables[0];

        if (dtRanPwd.Rows.Count > 0)
        {
            randomPwd = dtRanPwd.Rows[0]["RandomPwd"].ToString();
            hdnRandomPwd.Value = randomPwd;
        }
       
        txtFromAddress.Text = ConfigurationManager.AppSettings["FromAddress"];
        txtSubject.Text = ddlVesselOwner.SelectedItem.Text +  " - Crew Approval Request for Rank : " + crewRank ;
        string MailContent = System.IO.File.ReadAllText(Server.MapPath("~/Modules/HRD/Applicant/SendProposaltoClient.htm"));
        string SendProposalMailURL = ConfigurationManager.AppSettings["SendProposalMail"];

        string URl = SendProposalMailURL + candidateId.ToString();

        MailContent = MailContent.Replace("$SendProposalLINK$", URl);
        MailContent = MailContent.Replace("$ProposalId$", strcandidateId);
        MailContent = MailContent.Replace("$crewFullName$", crewFullName);
        MailContent = MailContent.Replace("$crewRank$", crewRank);
        MailContent = MailContent.Replace("$randomPwd$", randomPwd);
        MailContent = MailContent.Replace("$UserFullName$", UserFullName);
        litMessage.Text = MailContent;
    }

    private void GetProposalDetails(Int32 CandidateId)
    {
        crewFullName = "";
        crewRank = "";
        string sql = " Select cd.CandidateId, cd.FirstName + ' ' + cd.MiddleName + ' ' + cd.LastName As Crew_FullName, (Select RankName + '(' + RankCode +')' from Rank with(nolock) where RankId = cd.RankAppliedId and StatusId = 'A' ) As Rank from CandidatePersonalDetails cd with(nolock) where cd.CandidateId = '" + CandidateId.ToString() + "'";
        DataTable dt = Budget.getTable(sql).Tables[0];
        if (dt.Rows.Count > 0)
        {
            crewFullName = dt.Rows[0]["Crew_FullName"].ToString();
            crewRank = dt.Rows[0]["Rank"].ToString();
        }
    }

    public void insertProposalToOwnerDetails(string _strProc, Int32 _candidateId, Int32 _ownerId, string _OfficerRemarks, string _FromEmail, string _ToEmail, string _BCCEmail, string _Subject, string _body, Int32 _createdby, string _randompwd, Int32 _vesselId, int _vacancyid)
    {
        Database oDatabase = DatabaseFactory.CreateDatabase();

        DbCommand odbCommand = oDatabase.GetStoredProcCommand(_strProc);
        oDatabase.AddInParameter(odbCommand, "@CandidateId", DbType.Int32, _candidateId);
        oDatabase.AddInParameter(odbCommand, "@ownerId", DbType.Int32, _ownerId);
        oDatabase.AddInParameter(odbCommand, "@officerremarks", DbType.String, _OfficerRemarks);
        oDatabase.AddInParameter(odbCommand, "@fromEmail", DbType.String, _FromEmail);
        oDatabase.AddInParameter(odbCommand, "@ToEmail", DbType.String, _ToEmail);
        oDatabase.AddInParameter(odbCommand, "@BCCEmail", DbType.String, _BCCEmail);
        oDatabase.AddInParameter(odbCommand, "@subject", DbType.String, _Subject.ToString().Trim());
        oDatabase.AddInParameter(odbCommand, "@body", DbType.String, _body.ToString().Trim());
        oDatabase.AddInParameter(odbCommand, "@createdby", DbType.Int32, _createdby);
        oDatabase.AddInParameter(odbCommand, "@randomPwd", DbType.String, _randompwd.ToString().Trim());
        oDatabase.AddInParameter(odbCommand, "@vesselId", DbType.Int32, _vesselId);
        oDatabase.AddInParameter(odbCommand, "@vacancyId", DbType.Int32, _vacancyid);
        using (TransactionScope scope = new TransactionScope())
        {
            try
            {
                // execute command and get records
                oDatabase.ExecuteNonQuery(odbCommand);
                scope.Complete();
            }
            catch (Exception ex)
            {
                // if error is coming throw that error
                throw ex;
            }
            finally
            {
                // after used dispose commmond            
                odbCommand.Dispose();
            }
        }
    }

    protected void ibClose_Click(object sender, ImageClickEventArgs e)
    {
        btnCancel_Click(sender, e);
    }

    protected void ddl_Vessel_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToInt32(ddl_Vessel.SelectedValue) > 0)
            {
                if (!string.IsNullOrWhiteSpace(txtSubject.Text))
                {
                    txtSubject.Text = string.Format(txtSubject.Text, ddlVesselOwner.SelectedItem.Text, hdnCompany.Value);
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "axdxasda", "alert('" + ex.Message + "');", true);
        }
    }

    protected void btnSendProposal_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddl_Ownerlist.SelectedValue == "0")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "axdxasda", "alert('Please select Owner name.');", true);
                return;
            }
            if (ddlVesselOwner.SelectedValue == "0")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "axdxasda", "alert('Please select vessel.');", true);
                return;
            }

            if (txtManningOfficerRemarks.Text == "" || string.IsNullOrWhiteSpace(txtManningOfficerRemarks.Text))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "axdxasda", "alert('Please fill Manning officer remarks.');", true);
                 return;
            }

            //if (txtToEmail.Text == "" || string.IsNullOrWhiteSpace(txtToEmail.Text))
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "axdxasda", "alert('Please fill To Email field.');", true);
            //    return;
            //}

            //if (txtSubject.Text.Trim() == "")
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "axdxasda", "alert('Please fill subject field.');", true);
            //     return;
            //}
            Int32 loginId = Common.CastAsInt32(Session["loginid"]);
            string ccmail = string.Empty;
            string sqlEmail = "Select Email from UserMaster with(nolock) where LoginId = " + loginId;
            DataTable dtEmail = Budget.getTable(sqlEmail).Tables[0];
            if (dtEmail.Rows.Count > 0)
            {
               if (! string.IsNullOrEmpty(dtEmail.Rows[0]["Email"].ToString()))
                {
                    ccmail = dtEmail.Rows[0]["Email"].ToString();
                }
            }
            int MailSendCount = 0;
            string sql = " Select Mail1 As Mail from Owner with(nolock) where StatusId = 'A' and ISNULL(Mail1,'') <> '' and OwnerId = " + ddl_Ownerlist.SelectedValue.ToString() + " UNION ALL  Select Mail2 As Mail from Owner with(nolock) where StatusId = 'A' and ISNULL(Mail2,'') <> '' and OwnerId = " + ddl_Ownerlist.SelectedValue.ToString() + "  UNION ALL  Select Mail3 As Mail from Owner with(nolock) where StatusId = 'A' and ISNULL(Mail3,'') <> '' and OwnerId = " + ddl_Ownerlist.SelectedValue.ToString() + " ";
            DataTable dt = Budget.getTable(sql).Tables[0];
            if (dt.Rows.Count > 0)
            {
                MailSendCount = dt.Rows.Count;
                string ErrMsg = "";
                string AttachmentFilePath = "";
                Int32 ownerid = Convert.ToInt32(ddl_Ownerlist.SelectedValue);
                Int32 vesselId = Convert.ToInt32(ddlVesselOwner.SelectedValue);
                string officerremarks = txtManningOfficerRemarks.Text.Trim();
                GetProposalDetails(Convert.ToInt32(hdnCandidateId.Value));
                int sendercount = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    if (! string.IsNullOrEmpty(dr["Mail"].ToString()))
                    {
                        char[] Sep = { ';' };
                        string toAdds = dr["Mail"].ToString();
                        string[] ToAdds = { toAdds } ;
                        string[] CCAdds = { ccmail };
                        string[] BCCAdds = { "" } ;
                        //------------------
                        string strcandidateId = Convert.ToString(hdnCandidateId.Value);
                        EmailBody = "";
                        EmailSubject = "";
                        randomPwd = "";
                        DataTable dtRanPwd = Budget.getTable("EXEC DBO.GENEREATERamdomPassword " + strcandidateId + " ").Tables[0];

                        if (dtRanPwd.Rows.Count > 0)
                        {
                            randomPwd = dtRanPwd.Rows[0]["RandomPwd"].ToString();
                            hdnRandomPwd.Value = randomPwd;
                        }
                        string mailStatus = "";
                        Boolean OwnerAppRejStatus = false;
                        DataTable Dt = Budget.getTable(" Select PTO_MailStatus,PTO_OwnerAppRej  from ProposalToOwner with(nolock) where PTO_CandidateId =" + hdnCandidateId.Value.ToString() + " AND PTO_VacancyId = " + Convert.ToInt32(lblVacancyId.Text) + " AND PTO_ToEmail = '" + toAdds.Trim() + "' ").Tables[0];
                        if (Dt.Rows.Count > 0)
                        {
                            mailStatus = Dt.Rows[0]["PTO_MailStatus"].ToString();
                            if (Dt.Rows[0]["PTO_OwnerAppRej"].ToString() == "W" || Dt.Rows[0]["PTO_OwnerAppRej"].ToString() == "R")
                            {
                                OwnerAppRejStatus = true;
                            }
                        }
                        if (mailStatus != "S" || (hdnResendProposal.Value == "true") || OwnerAppRejStatus)
                        {
                            txtFromAddress.Text = ConfigurationManager.AppSettings["FromAddress"];
                            txtSubject.Text = ddlVesselOwner.SelectedItem.Text + " - Crew Approval Request for Rank : " + crewRank;

                            insertProposalToOwnerDetails("InsertProposalToOwner",
                                                                    Convert.ToInt32(hdnCandidateId.Value),
                                                                    ownerid,
                                                                    officerremarks,
                                                                    txtFromAddress.Text.Trim(),
                                                                    toAdds.Trim(),
                                                                    ccmail.Trim(),
                                                                    txtSubject.Text.Trim(),
                                                                    litMessage.Text.Trim(),
                                                                    loginId,
                                                                    hdnRandomPwd.Value.Trim(), 
                                                                    vesselId, 
                                                                    Convert.ToInt32(lblVacancyId.Text));
                                Int32 proposalId = 0;
                                DataTable Dt1 = Budget.getTable(" Select top 1 PTO_Id  from ProposalToOwner with(nolock) where PTO_CandidateId =" + hdnCandidateId.Value.ToString() + " AND PTO_VacancyId = "+ Convert.ToInt32(lblVacancyId.Text) + " AND PTO_ToEmail = '" + toAdds.Trim()+"' ").Tables[0] ;
                                if (Dt1.Rows.Count > 0)
                                {
                                    proposalId = Convert.ToInt32(Dt1.Rows[0][0]);
                                }
                                string MailContent = System.IO.File.ReadAllText(Server.MapPath("~/Modules/HRD/Applicant/SendProposaltoClient.htm"));
                                string SendProposalMailURL = ConfigurationManager.AppSettings["SendProposalMail"];
                                string URl = SendProposalMailURL + strcandidateId.ToString() + "&ProposalId=" + proposalId.ToString();
                                MailContent = MailContent.Replace("$SendProposalLINK$", URl);
                                MailContent = MailContent.Replace("$ProposalId$", proposalId.ToString());
                                MailContent = MailContent.Replace("$crewFullName$", crewFullName);
                                MailContent = MailContent.Replace("$crewRank$", crewRank);
                                MailContent = MailContent.Replace("$randomPwd$", randomPwd);
                                MailContent = MailContent.Replace("$UserFullName$", UserFullName);
                                litMessage.Text = MailContent;
                            if (!string.IsNullOrEmpty(litMessage.Text))
                            {
                                if (SendEmail.SendeMail(Common.CastAsInt32(Session["loginid"]), txtFromAddress.Text.Trim(), txtFromAddress.Text.Trim(), ToAdds, CCAdds, BCCAdds, txtSubject.Text.Trim(), litMessage.Text.Trim(), out ErrMsg, AttachmentFilePath))
                                {
                                    Budget.getTable("UPDATE DBO.ProposalToOwner SET PTO_MailStatus='S', PTO_Body = '" + litMessage.Text + "' WHERE PTO_CandidateId=" + hdnCandidateId.Value.ToString() + " AND PTO_Id = " + proposalId.ToString() +"");
                                    sendercount++;
                                    if (MailSendCount == sendercount)
                                    {
                                        //lblMessage.Text = "Mail sent successfully.";
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "axdxasda", "alert('Mail sent successfully.');", true);
                                        dvProposalToOwner.Visible = false;
                                        UpdateList(sender, e);
                                        hdnResendProposal.Value = "false";
                                    }
                                }
                                else
                                {
                                    Budget.getTable("UPDATE DBO.ProposalToOwner SET PTO_MailStatus='F' WHERE PTO_CandidateId=" + hdnCandidateId.Value.ToString());
                                    // lblMessage.Text = "Unable to send Mail. Error : " + ErrMsg;
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "axdxasda", "alert('Unable to send Mail. Error : " + ErrMsg + "');", true);
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "axdxasda", "alert('Error in email body.');", true);
                               
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "axdxasda", "alert('Mail already sent.');", true);
                        }
                    }
                }
            }
        }
        catch (SystemException ex)
        {
            //this.lblMessage.Text = "Unable to send mail : " + ex.Message;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "axdxasda", "alert('Unable to send Mail. Error : " + ex.Message + "');", true);
        }
    }
    protected void UpdateList(object sender, EventArgs e)
    {
        try
        {
            binddata(Convert.ToInt32(Session["VacancyId"]));
        }
        catch { }

    }


    protected void Gv_Vacancy_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {

    }
    protected void btnEditVacancy_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow row = (GridViewRow)(((ImageButton)sender).NamingContainer);
        int Rowindx = row.RowIndex;
       // Mode = "Edit";
        Session["Mode"] = "Edit";
        HiddenField hfdVacancyId;
        hfdVacancyId = (HiddenField)Gv_Vacancy.Rows[Rowindx].FindControl("HiddenVacancyId");
        Session["VacancyId"] = hfdVacancyId.Value.ToString();
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "PP", "window.open('AddUpdateVacancy.aspx?Mode=" + Session["Mode"] + "&Vacancyid=" + Session["VacancyId"].ToString() + "&Key=0');", true);
    }

    protected void BindVacancyData()

    {
        string sql = "Select hv.VacancyID As VacancyID , o.OwnerName As Owner,v.VesselName As VesselName, r.RankName As Rank, REPLACE(CONVERT(CHAR(11), Joining_Date, 106),' ','-') As  DOJ,  REPLACE(CONVERT(CHAR(11), Proposal_Date, 106),' ','-') As  DOP, Vt.VesselTypeName As VesselTypeName,ISNULL((Select Count(HVC_CandidateId) As CandidateCount from HRD_Vacancy_CandidateMapping where HVC_Status = 'A' and HVC_VacancyId = hv.VacancyID),0) As CandidateCount from HRD_Vacancy hv with(nolock) Inner Join Owner o with(nolock) on hv.OwnerId = o.OwnerId Inner Join Rank r with(nolock) on hv.RankId = r.RankId LEFT OUTER Join Vessel v with(nolock) on hv.VesselId = v.VesselId LEFT OUTER Join VesselType vt with(nolock) on v.VesselTypeId = vt.VesselTypeId  where hv.Status = 'A' and 1=1";
        if (chk_UsVisa.Checked == true)
        {

            sql = sql + " And hv.IsUSVisa = 1";
        }
        //else
        //{
        //    sql = sql + " And hv.IsUSVisa = 0";
        //}

        if (chk_SchengenVisa.Checked == true)
        {

            sql = sql + " And hv.IsSchengenVisa = 1";
        }
        //else
        //{
        //    sql = sql + " And hv.IsSchengenVisa = 0";
        //}

        if (ddl_Rank_Search.SelectedIndex > 0)
        {
            sql = sql + " And hv.RankId = " + Convert.ToInt32(ddl_Rank_Search.SelectedValue) + "";
        }

        if (ddl_Owner.SelectedIndex > 0)
        {
            sql = sql + " And hv.OwnerId = " + Convert.ToInt32(ddl_Owner.SelectedValue) + "";
        }

        if (ddl_Vessel.SelectedIndex > 0)
        {
            sql = sql + " And hv.VesselId = " + Convert.ToInt32(ddl_Vessel.SelectedValue) + "";
        }

        if (ddl_VesselType.SelectedIndex > 0)
        {
            sql = sql + " And vt.VesselTypeId = " + Convert.ToInt32(ddl_VesselType.SelectedValue) + "";
        }

        if (ddl_Recr_Office.SelectedIndex > 0)
        {
            sql = sql + " And hv.Recruiter_Office = " + Convert.ToInt32(ddl_Recr_Office.SelectedValue) + "";
        }

        DataTable dt = Budget.getTable(sql).Tables[0];

        if (dt.Rows.Count > 0)
        {
            Gv_Vacancy.DataSource = dt;
            Session["Vacancy"] = dt;
            Gv_Vacancy.DataBind();
            VacancyCount.Text = Gv_Vacancy.Rows.Count.ToString();
        }
        else
        {

            Gv_Vacancy.DataSource = null;
            Session["Vacancy"] = null;
            Gv_Vacancy.DataBind();
            VacancyCount.Text = "0";
        }
    }
    protected void Open_Candidate(object sender, EventArgs e)
    {
        LinkButton ib = ((LinkButton)sender);
        int candidateid = Convert.ToInt32(ib.CommandArgument);
        string AppName = ConfigurationManager.AppSettings["AppName"].ToString();
        // Response.Redirect("CandidateDetailPopUp.aspx?candidate=" + candidateid.ToString() + "&M=" + ib.ToolTip);
        //ScriptManager.RegisterStartupScript(Page, this.GetType(), "abc", "window.open('ShipjobAppDetailPopUp.aspx?candidate=" + candidateid.ToString() + "&M=App','','');", true);
        //ScriptManager.RegisterStartupScript(Page, this.GetType(), "abc", "window.open('/" + AppName + "/Modules/HRD/Applicant/ShipjobAppDetailPopUp.aspx?candidate=" + candidateid.ToString() + "&M=App','','');", true);

        ScriptManager.RegisterStartupScript(Page, this.GetType(), "abc", "window.open('/Modules/HRD/Applicant/ShipjobAppDetailPopUp.aspx?candidate=" + candidateid.ToString() + "&M=App','','');", true);
    }
}