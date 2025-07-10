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
using System.Text;

public partial class Circular_SCM : System.Web.UI.Page
{
    string VesselId = "";
    int intLogin_Id = 0;
    Authority Auth;
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        //--------------------------------- PAGE ACCESS AUTHORITY ----------------------------------------------------
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 164);
        if (chpageauth <= 0)
            Response.Redirect("blank.aspx");
        //------------------------------------------------------------------------------------------------------------
           try
           {
            //this.Form.DefaultButton = this.btn_Show.UniqueID.ToString();
            //lblMessage.Text = ""+Session["ms"];
            if (Session["loginid"] == null)
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Default.aspx';", true);
            }
            else
            {
                intLogin_Id = Convert.ToInt32(Session["loginid"].ToString());
            }
            if (Session["loginid"] != null)
            {
                ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()),13);
                OBJ.Invoke();
                Session["Authority"] = OBJ.Authority;
                Auth = OBJ.Authority;
            }
            if (Session["loginid"] != null)
            {
                HiddenField_LoginId.Value = Session["loginid"].ToString();
            }
            if (!Page.IsPostBack)
            {
                Session["FormSelection"] = "1";
                for(int i=DateTime.Today.Year ;i>=2012;i--)
                {
                    ddlYear.Items.Add(new ListItem(i.ToString(), i.ToString()));  
                }
                BindFleet();
                BindVessel();
                btn_Show_Click(sender, e);
                downloadSUPTDForm();
            }
            else
            {
                Session.Remove("FormSelection");
            }
        }
        catch (Exception ex) { throw ex; }
    }

    // -------------- Events
    protected void btn_Show_Click(object sender, EventArgs e)
    {
        ViewState["Last"] = "Show";
        try
        {
            int intCrewId = 0;
            Grd_NearMiss.DataSource = null;
            Grd_NearMiss.DataBind();

            if (ddlVessel.SelectedIndex == 0)
            {
                for (int J = 1; J < ddlVessel.Items.Count; J++)
                {
                    //if (ddlVessel.Items[J].Selected == true)
                    //{
                    if (VesselId == "")
                    {
                        VesselId = ddlVessel.Items[J].Value;
                    }
                    else
                    {
                        VesselId = VesselId + "," + ddlVessel.Items[J].Value;
                    }
                    //}
                }
            }
            else
            {
                VesselId = ddlVessel.SelectedValue;
            }
            string CYear = ddlYear.SelectedValue; 
           

            string SQL = "EXEC SCM_GETMONTHLYDATA " + ddlYear.SelectedValue + ", " + ddlVessel .SelectedValue+ " ,"+ ddlFleet.SelectedValue +" ";
            DataTable dt3 = VesselReporting.getTable(SQL).Tables[0]; 
            if (dt3.Rows.Count > 0)
            {
                Grd_NearMiss.DataSource = dt3;
                Grd_NearMiss.DataBind();
            }
            else
            {
                BindBlankGrid();
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }
    protected void Grd_NearMiss_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            if( ViewState["Last"].ToString()=="Show")
                btn_Show_Click(sender, e);
            //else
            //    btn_ShowAll_Click(sender, e);
        }
        catch (Exception ex) { throw ex; }
    }
    protected void ddlFleet_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        BindVessel();
        //BindGrid();
    }
    protected void chk_Inactive_OnCheckedChanged(object sender, EventArgs e)
    {
        BindVessel();
    }
    protected void btnClear_OnClick(object sender, EventArgs e)
    {
        ddlFleet.SelectedIndex = 0;
        chk_Inactive.Checked = false;
        BindVessel();
        ddlVessel.SelectedIndex = 0;
        btn_Show_Click(sender, e);
    }

    protected void lnkdownloadSUPTDForm_OnClick(object sender, EventArgs e)
    {
        string sourcepath = Server.MapPath("SUPTD_Template.htm");
        string destpath = Server.MapPath("SUPTD" + ".htm");
        string Content = File.ReadAllText(sourcepath);
        int StartIndex = 0;
        int AbsentRows = 20;
        string MonthYearSecion = "$MonthYear$";
        DataSet ds_SCMRanks = Budget.getTable("Exec Get_SUPTDRanks");
        ds_SCMRanks.Tables[0].TableName = "SCM_RANKDETAILS";

        StartIndex = Content.IndexOf("<!--RankStart");
        string RankSection = Content.Substring(StartIndex, Content.IndexOf("<!--RankEnd-->") - StartIndex).Replace("<!--RankStart-->", "");
        string RankSectionNew = "";

        int i = 1;
        int cnt = 1;
        for (cnt = 1; cnt <= 25; cnt++)
        {
            bool disabled = (i <= 3);
            DataRow dr = ds_SCMRanks.Tables["SCM_RANKDETAILS"].Rows[i - 1];
            RankSectionNew += RankSection.Replace("<!--RANKLIST-->", getRankSelect(((i <= 3) ? dr["RANK"].ToString() : ""), disabled, cnt)).Replace("txtHName", "txtHName" + cnt.ToString()).Replace("$lblHDesignation$", dr["DESG"].ToString()).Replace("lblHDesignation", "lblHDesignation" + cnt.ToString()).Replace("$DescIDValue$", "");
            if (i < 7)
                i++;
            if (cnt > 25)
                break;
        }

        StartIndex = Content.IndexOf("<!--AbsentListStart");
        string AbsentSection = Content.Substring(StartIndex, Content.IndexOf("<!--AbsentListEnd-->") - StartIndex).Replace("<!--AbsentListStart-->", "");
        string AbsentSectionNew = "";

        i = 1;
        while (i <= AbsentRows)
        {
            AbsentSectionNew += AbsentSection.Replace("ddlRankAbsent", "ddlRankAbsent" + i.ToString()).Replace("txtNameAbsent", "txtNameAbsent" + i.ToString());
            i++;
        }

        string NewContent = Content.Replace(MonthYearSecion, DateTime.Today.ToString("MMM - yyyy"));

        NewContent = NewContent.Replace("$hfRANKCounter$", "25");  
        NewContent = NewContent.Replace(RankSection, RankSectionNew);

        NewContent = NewContent.Replace("$hfABSCounter$", AbsentRows.ToString());
        NewContent = NewContent.Replace(AbsentSection, AbsentSectionNew);

        NewContent = NewContent.Replace("<option value='M'>Monthly</option>", "<option value='S'>By Suptd.</option>");

        File.WriteAllText(destpath, NewContent);
    }
    // -------------- Function
    public void BindBlankGrid()
    {
        try
        {
            DataTable dt = new DataTable();
            DataRow dr;
            dt.Columns.Add("VesselId");
            dt.Columns.Add("VesselName");
            dt.Columns.Add("KPI");
            dt.Columns.Add("TotalReceived");
            dt.Columns.Add("TotalVerified");

            for (int i = 0; i < 13; i++)
            {
                dr = dt.NewRow();
                dt.Rows.Add(dr);
                dt.Rows[dt.Rows.Count - 1][0] = "";
                dt.Rows[dt.Rows.Count - 1][1] = "";
                dt.Rows[dt.Rows.Count - 1][2] = "";
                dt.Rows[dt.Rows.Count - 1][3] = "";
                dt.Rows[dt.Rows.Count - 1][4] = "";
            }

            Grd_NearMiss.DataSource = dt;
            Grd_NearMiss.DataBind();
            //Grd_NearMiss.SelectedIndex = -1;
        }
        catch (Exception ex) { throw ex; }
    }
    public void BindFleet()
    {
        string Query = "select * from dbo.FleetMaster";
        ddlFleet.DataSource = Budget.getTable(Query);
        ddlFleet.DataTextField = "FleetName";
        ddlFleet.DataValueField = "FleetID";
        ddlFleet.DataBind();
        ddlFleet.Items.Insert(0, new ListItem("<--All-->", "0"));
    }
    public void BindVessel()
    {
        string WhereClause = "";
        string sql = "SELECT VesselID,Vesselname FROM DBO.Vessel v Where 1=1 ";
        if (!chk_Inactive.Checked)
        {
            WhereClause = " and v.VesselStatusid<>2 ";
        }
        if (ddlFleet.SelectedIndex != 0)
        {
            WhereClause = WhereClause + " and fleetID=" + ddlFleet.SelectedValue + "";
        }
        sql = sql + WhereClause + "ORDER BY VESSELNAME";
        ddlVessel.DataSource = VesselReporting.getTable(sql);

        ddlVessel.DataTextField = "Vesselname";
        ddlVessel.DataValueField = "VesselID";
        ddlVessel.DataBind();
        ddlVessel.Items.Insert(0, new ListItem("<--All-->", "0"));
    }

    public string getRankSelect(string Default, bool Disabled, int SrNo)
    {
        DataSet ds = new DataSet();
        StringBuilder sb = new StringBuilder();
        string SQL ="";
        //if(Default=="SUPTD")
        //    SQL = "Select '<option value=''' + RANKNAME + ''' ' + ' selected '+ '>' + RANKNAME + '</option>' FROM DBO.RANK WHERE STATUSID='A'  ORDER BY RANKLEVEL";
        //else
            SQL = "Select '<option value=''' + RANKNAME + ''' ' + (case when rankname='" + Default + "' then ' selected ' else '' end )+ '>' + RANKNAME + '</option>' FROM DBO.RANK WHERE STATUSID='A'  ORDER BY RANKLEVEL";
        
        ds = Budget.getTable(SQL);

        sb.Append("<select id='ddlHRank" + SrNo + "' style='width:200px;' " + ((Disabled) ? "disabled" : "") + "> <option value=''>SELECT</option>");
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            sb.Append(dr[0]);
        }
        sb.Append("</select> ");
        return sb.ToString();
    }
    public void downloadSUPTDForm()
    {
        string sourcepath = Server.MapPath("SUPTD_Template.htm");
        string destpath = Server.MapPath("SUPTD" + ".htm");
        string Content = File.ReadAllText(sourcepath);
        int StartIndex = 0;
        int AbsentRows = 20;
        string MonthYearSecion = "$MonthYear$";
        DataSet ds_SCMRanks = Budget.getTable("Exec Get_SUPTDRanks");
        ds_SCMRanks.Tables[0].TableName = "SCM_RANKDETAILS";

        StartIndex = Content.IndexOf("<!--RankStart");
        string RankSection = Content.Substring(StartIndex, Content.IndexOf("<!--RankEnd-->") - StartIndex).Replace("<!--RankStart-->", "");
        string RankSectionNew = "";

        int i = 1;
        int cnt = 1;
        for (cnt = 1; cnt <= 25; cnt++)
        {
            bool disabled = (i <= 3);
            DataRow dr = ds_SCMRanks.Tables["SCM_RANKDETAILS"].Rows[i - 1];
            RankSectionNew += RankSection.Replace("<!--RANKLIST-->", getRankSelect(((i <= 3) ? dr["RANK"].ToString() : ""), disabled, cnt)).Replace("txtHName", "txtHName" + cnt.ToString()).Replace("$lblHDesignation$", dr["DESG"].ToString()).Replace("lblHDesignation", "lblHDesignation" + cnt.ToString()).Replace("$DescIDValue$", "");
            if (i < 7)
                i++;
            if (cnt > 25)
                break;
        }

        StartIndex = Content.IndexOf("<!--AbsentListStart");
        string AbsentSection = Content.Substring(StartIndex, Content.IndexOf("<!--AbsentListEnd-->") - StartIndex).Replace("<!--AbsentListStart-->", "");
        string AbsentSectionNew = "";

        i = 1;
        while (i <= AbsentRows)
        {
            AbsentSectionNew += AbsentSection.Replace("ddlRankAbsent", "ddlRankAbsent" + i.ToString()).Replace("txtNameAbsent", "txtNameAbsent" + i.ToString());
            i++;
        }

        string NewContent = Content.Replace(MonthYearSecion, DateTime.Today.ToString("MMM - yyyy"));

        NewContent = NewContent.Replace("$hfRANKCounter$", "25");
        NewContent = NewContent.Replace(RankSection, RankSectionNew);

        NewContent = NewContent.Replace("$hfABSCounter$", AbsentRows.ToString());
        NewContent = NewContent.Replace(AbsentSection, AbsentSectionNew);

        NewContent = NewContent.Replace("<option value='M'>Monthly</option>", "<option value='S'>By Suptd.</option>");

        File.WriteAllText(destpath, NewContent);
    }
}
