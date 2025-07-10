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

public partial class CRMActivities_CRM_InventoryHome : System.Web.UI.Page
{
    public int InventoryId
    {
        set { ViewState["InventoryId"] = value; }
        get { return Common.CastAsInt32(ViewState["InventoryId"]); }
    }
    public int CardType
    {
        set { ViewState["CardType"] = value; }
        get { return Common.CastAsInt32(ViewState["CardType"]); }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        lblMsg.Text = "";
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 1);
        if (chpageauth <= 0)
        {
            Response.Redirect("../AuthorityError.aspx");
        }
        if (!IsPostBack)
        {
            CardType = Common.CastAsInt32(Request.QueryString["CT"]);
            switch(CardType)
            {
                case 1: lblCardType.Text = "Birth Day Card";
                        break;
                case 2: lblCardType.Text = "Seasons Greeting Card";
                        break;
                case 3: lblCardType.Text = "Welcome Home Card";
                        break;
                default: lblCardType.Text = "";
                         break;
            }
            BindRecruitingOffice();
            //btnSearch_Click(sender, e);
        }
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
        ddl_Recr_Office.Items.RemoveAt(0);
        ddl_Recr_Office.Items.Insert(0, new ListItem("< Select >", "0"));


        //ddlRecOffice.DataValueField = "RecruitingOfficeId";
        //ddlRecOffice.DataTextField = "RecruitingOfficeName";
        //ddlRecOffice.DataSource = processgetrecruitingoffice.ResultSet;
        //ddlRecOffice.DataBind();
        //ddlRecOffice.Items.RemoveAt(0);
        //ddlRecOffice.Items.Insert(0, new ListItem("< Select >", "0"));
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (ddl_Recr_Office.SelectedIndex == 0)
        {
            lblMsg.Text = "Please select recruiting office.";
            ddl_Recr_Office.Focus();
            return;
        }
        string SQL = "";
        string WHERE = " WHERE CardType = " + CardType + " ";

        SQL = "SELECT InventoryId,RO.RecruitingOfficeName, AssignDate, NoOfCards, (SELECT FirstName + ' ' + LastName  FROM UserLogin WHERE LoginId = CCI.UpdatedBy) AS UpdatedBy, UpdatedOn " + 
              "FROM CrewCardInventory CCI " +
              "INNER JOIN RecruitingOffice RO ON CCI.RecruitingOfficeId = RO.RecruitingOfficeId ";

        if (ddl_Recr_Office.SelectedIndex != 0)
        {
            WHERE = WHERE + " AND RO.RecruitingOfficeId = " + ddl_Recr_Office.SelectedValue.Trim();
        }        

        SQL = SQL + WHERE;
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(SQL);
        rpt_Received.DataSource = dt;
        rpt_Received.DataBind();

        lbltotrecvd.Text = dt.Compute("SUM(NoOfCards)", "").ToString();

        //lblRcount1.Text = "Total Records : " + dt.Rows.Count;

        if (CardType == 1)
        {
            SQL = "SELECT CBCD.SentOn,COUNT(*) As Issued FROM CrewBirthDayCardDetails CBCD " + 
                  "INNER JOIN crewpersonaldetails CPD ON CBCD.CrewId = CPD.CrewId " +
                  "WHERE CPD.recruitmentOfficeId = " + ddl_Recr_Office.SelectedValue.Trim() + " Group By SentOn ";            
        }
        if (CardType == 2)
        {
            SQL = "SELECT CSCD.SentOn,COUNT(*) As Issued FROM CrewSeasonsGreetingCardDetails CSCD " +
                  "INNER JOIN crewpersonaldetails CPD ON CSCD.CrewId = CPD.CrewId " +
                  "WHERE CPD.recruitmentOfficeId = " + ddl_Recr_Office.SelectedValue.Trim() + " Group By SentOn ";
        }
        if (CardType == 3)
        {
            SQL = "SELECT CWCD.SentOn,COUNT(*) As Issued FROM CrewWelcomeCardDetails CWCD " +
                  "INNER JOIN crewpersonaldetails CPD ON CWCD.CrewId = CPD.CrewId " +
                  "WHERE CPD.recruitmentOfficeId = " + ddl_Recr_Office.SelectedValue.Trim() + " Group By SentOn ";
        }

        dt = Common.Execute_Procedures_Select_ByQueryCMS(SQL);
        rpt_Issued.DataSource = dt;
        rpt_Issued.DataBind();

        lbltotissued.Text = dt.Compute("SUM(Issued)", "").ToString();

        lblBalance.Text = (Common.CastAsInt32(lbltotrecvd.Text) - Common.CastAsInt32(lbltotissued.Text)).ToString(); 


    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("CRMHome.aspx");
    }
    protected void btnAssignCard_Click(object sender, EventArgs e)
    {
        if (ddl_Recr_Office.SelectedIndex == 0)
        {
            lblMsg.Text = "Please select recruiting office.";
            ddl_Recr_Office.Focus();
            return;
        }
        dv_SendCard.Visible = true;
    }
    
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (DateTime.Parse(txtIssueDate.Text.Trim()) > DateTime.Today.Date)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "asdsad", "alert('Card issue date can not be more than today.')", true);
            txtIssueDate.Focus();
            return;
        }

        if (InventoryId == 0)
        {
            string SQL = "INSERT INTO CrewCardInventory VALUES ((SELECT ISNULL(MAX([InventoryId]), 0) + 1 FROM CrewCardInventory), " + ddl_Recr_Office.SelectedValue.Trim() + ", " + CardType + ", '" + txtIssueDate.Text.Trim() + "', " + txtNoofCards.Text.Trim() + ", " + Session["loginid"].ToString() + ", getdate())";
            Common.Execute_Procedures_Select_ByQueryCMS(SQL);
        }

        if (InventoryId > 0)
        {
            string SQL = "UPDATE CrewCardInventory SET RecruitingOfficeId = " + ddl_Recr_Office.SelectedValue.Trim() + ", CardType = " + CardType + ", AssignDate = '" + txtIssueDate.Text.Trim() + "', NoOfCards = " + txtNoofCards.Text.Trim() + "  WHERE InventoryId = " + InventoryId;
            Common.Execute_Procedures_Select_ByQueryCMS(SQL);
        }
        
        lblMsg.Text = "Cards issued successfully.";
        btn_Close_Click(sender, e);

    }
    protected void btn_Close_Click(object sender, EventArgs e)
    {
        InventoryId = 0;
        btnSearch_Click(sender, e);
        //ddlRecOffice.SelectedIndex = 0;
        //ddlCardType.SelectedIndex = 0;
        txtIssueDate.Text = "";
        txtNoofCards.Text = "";
        dv_SendCard.Visible = false;
    }

    protected void btnEditInventory_Click(object sender, EventArgs e)
    {
        InventoryId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("SELECT * FROM CrewCardInventory WHERE InventoryId = " + InventoryId);
        if (dt != null && dt.Rows.Count > 0)
        {
            //ddlRecOffice.SelectedValue = dt.Rows[0]["RecruitingOfficeId"].ToString();
            //ddlCardType.SelectedValue = dt.Rows[0]["CardType"].ToString();
            txtIssueDate.Text = Common.ToDateString(dt.Rows[0]["AssignDate"]);
            txtNoofCards.Text = dt.Rows[0]["NoOfCards"].ToString();
        }
        dv_SendCard.Visible = true;
    }
}