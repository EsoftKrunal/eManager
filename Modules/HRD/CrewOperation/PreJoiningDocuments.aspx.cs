using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Transactions;
using System.Data;
using ShipSoft.CrewManager.Operational;
using System.Security.Policy;
using System.Activities.Statements;
using Microsoft.SqlServer.Management.Smo.Agent;

public partial class Modules_HRD_CrewOperation_PreJoiningDocuments : System.Web.UI.Page
{
    int crewid
    {
        set { ViewState["crewid"] = value; }
        get { return Common.CastAsInt32(ViewState["crewid"]); }
    }

    int vesselid
    {
        set { ViewState["vesselid"] = value; }
        get { return Common.CastAsInt32(ViewState["vesselid"]); }
    }

    string Seafarer
    {
        set { ViewState["Seafarer"] = value; }
        get { return Convert.ToString(ViewState["Seafarer"]); }
    }

    string SeamansID
    {
        set { ViewState["SeamansID"] = value; }
        get { return Convert.ToString(ViewState["SeamansID"]); }
    }
    string vessel
    {
        set { ViewState["vessel"] = value; }
        get { return Convert.ToString(ViewState["vessel"]); }
    }

    string Rank
    {
        set { ViewState["Rank"] = value; }
        get { return Convert.ToString(ViewState["Rank"]); }
    }

    string shipowner
    {
        set { ViewState["shipowner"] = value; }
        get { return Convert.ToString(ViewState["shipowner"]); }
    }

    string passport
    {
        set { ViewState["passport"] = value; }
        get { return Convert.ToString(ViewState["passport"]); }
    }

    string LRIMONumber
    {
        set { ViewState["LRIMONumber"] = value; }
        get { return Convert.ToString(ViewState["LRIMONumber"]); }
    }

    string Nationality
    {
        set { ViewState["Nationality"] = value; }
        get { return Convert.ToString(ViewState["Nationality"]); }
    }

    DateTime Dob
    {
        set { ViewState["Dob"] = value; }
        get { return Convert.ToDateTime(ViewState["Dob"]); }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            SessionManager.SessionCheck_New();
            crewid = Common.CastAsInt32(Request.QueryString["CrewId"]);
            vesselid = Common.CastAsInt32(Request.QueryString["VesselId"]);
            if (!Page.IsPostBack)
            {
                dvDocContent.Visible = false;
                litMessage.Text = "";

                this.ddlManningAgent.DataTextField = "CompanyName";
                this.ddlManningAgent.DataValueField = "CompanyId";
                this.ddlManningAgent.DataSource = Budget.getTable("Select CompanyId, CompanyName from ContractCompanyMaster with(nolock) where StatusId = 'A' and IsManningAgent = 1 Order by CompanyName Asc").Tables[0];
                this.ddlManningAgent.DataBind();
                ddlManningAgent.Items.Insert(0, new ListItem("< Select >", "0"));

                String sql = "Select top 1 upper(ltrim(rtrim(cp.FirstName)) + ' ' + isnull(Cp.MiddleName,'') + ' ' + Cp.LastName)  As Seafarer, (Select top 1 DocumentNumber from CrewTravelDocument with(nolock) where DocumentTypeId = 2 and CrewId = cp.CrewId order by CreatedOn Desc ) As  SeamansID,(Select r.RankName from Rank r with(nolock) where r.RankId = cp.CurrentRankId) As Rank, (Select top 1 DocumentNumber from CrewTravelDocument with(nolock) where DocumentTypeId = 0 and CrewId = cp.CrewId order by CreatedOn Desc ) As  Passport,(SELECT NationalityName  from Country where statusid='A'  and Cp.NationalityId = CountryId) As Nationality, DateOfBirth  from CrewPersonalDetails cp with(nolock) Where cp.Crewid = '" + crewid.ToString() + "'";
                DataTable dtSeafarer = Budget.getTable(sql).Tables[0];
                if (dtSeafarer.Rows.Count > 0)
                {
                    Seafarer = dtSeafarer.Rows[0]["Seafarer"].ToString();
                    SeamansID = dtSeafarer.Rows[0]["SeamansID"].ToString();
                    Rank = dtSeafarer.Rows[0]["Rank"].ToString();
                    passport = dtSeafarer.Rows[0]["Passport"].ToString();
                    Nationality = dtSeafarer.Rows[0]["Nationality"].ToString();
                    Dob = Convert.ToDateTime(dtSeafarer.Rows[0]["DateOfBirth"].ToString());
                }

                String sql1 = "Select v.VesselName As VesselName,(Select OwnerName from Owner o with(nolock) where o.OwnerId = v.OwnerId) As OwnerName, Isnull(LRIMONumber,'') As LRIMONumber from Vessel v with(nolock) where v.VesselId = '" + vesselid.ToString() + "'";
                DataTable dtVessel= Budget.getTable(sql1).Tables[0];
                if (dtVessel.Rows.Count > 0)
                {
                    vessel = dtVessel.Rows[0]["VesselName"].ToString();
                    txtVessel.Text = vessel;
                    shipowner = dtVessel.Rows[0]["OwnerName"].ToString();
                    LRIMONumber = dtVessel.Rows[0]["LRIMONumber"].ToString();
                }
            }
        }
        catch(Exception ex)
        {
            lblMessage.Text = ex.Message.ToString();
        }
    }

    protected void ddlManningAgent_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            dvDocContent.Visible = false;
            litMessage.Text = "";
           // ddlDocuments.SelectedIndex = 0;
            if (Convert.ToInt32(ddlManningAgent.SelectedValue) > 0)
            {
                this.ddlDocuments.DataTextField = "DocumentName";
                this.ddlDocuments.DataValueField = "DocumentId";
                this.ddlDocuments.DataSource = Budget.getTable("Select pj.DocumentId , Pj.DocumentName from PreJoiningDocuments pj with(nolock) Inner Join PrejoiningDocsManningAgentMapping pjd with(nolock) on pj.DocumentId = Pjd.DocumentId where Pjd.Statusid = 'A' and pjd.Manning_AgentId = '" + ddlManningAgent.SelectedValue + "' order By pj.DocumentId").Tables[0];
                this.ddlDocuments.DataBind();
                ddlDocuments.Items.Insert(0, new ListItem("< Select >", "0"));
                dvDocContent.Visible = false;
                litMessage.Text = "";
            }
            else
            {
                ddlDocuments.Items.Insert(0, new ListItem("< Select >", "0"));
            }  
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message.ToString();
        }
    }



    protected void ddlDocuments_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            dvDocContent.Visible = false;
            litMessage.Text = ""; 
            if (Convert.ToInt32(ddlManningAgent.SelectedValue) > 0 && Convert.ToInt32(ddlDocuments.SelectedValue) > 0)
            {

                string sql1 = "Select CompanyName,ContactPerson,TelephoneNumber1,Email1 from ContractCompanyMaster with(nolock) where StatusId = 'A' and and IsManningAgent = 1 and CompanyId = '" + ddlManningAgent.SelectedValue + "'";
                DataTable dtMannningAgent = Budget.getTable(sql1).Tables[0];

                if (dtMannningAgent.Rows.Count > 0)
                {
                    dvDocContent.Visible = true;
                    string DocContent = System.IO.File.ReadAllText(Server.MapPath("~/EMANAGERBLOB/HRD/PrejoiningDocHtml/"+ ddlDocuments.SelectedItem.Text.ToString()+ ".htm"));
                    DocContent = DocContent.Replace("$ManningAgent$", dtMannningAgent.Rows[0]["CompanyName"].ToString());
                    DocContent = DocContent.Replace("$ContactPerson$", dtMannningAgent.Rows[0]["ContactPerson"].ToString());
                    DocContent = DocContent.Replace("$ContactNo$", dtMannningAgent.Rows[0]["TelephoneNumber1"].ToString());
                    DocContent = DocContent.Replace("$Email$", dtMannningAgent.Rows[0]["Email1"].ToString());
                    DocContent = DocContent.Replace("$Seafarer$", Seafarer);
                    DocContent = DocContent.Replace("$SeamansID$", SeamansID);
                    DateTime CurrentDate = DateTime.Now;
                    DocContent = DocContent.Replace("$CurrentDate$", CurrentDate.ToString("dd-MMM-yyyy"));
                    DocContent = DocContent.Replace("$Vessel$", vessel);
                    DocContent = DocContent.Replace("$Rank$", Rank);
                    DocContent = DocContent.Replace("$ShipOwner$", shipowner);
                    DocContent = DocContent.Replace("$PassportNo$", passport);
                    DocContent = DocContent.Replace("$IMO$", LRIMONumber);
                    DocContent = DocContent.Replace("$Nationality$", Nationality);
                    DocContent = DocContent.Replace("$DOB$", Dob.ToString("dd-MMM-yyyy"));
                    
                    litMessage.Text = DocContent;
                }
                else
                {
                    dvDocContent.Visible = false;
                    litMessage.Text = "";
                }
            }
            else
            {
                dvDocContent.Visible = false;
                litMessage.Text = "";
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message.ToString();
        }
    }
}