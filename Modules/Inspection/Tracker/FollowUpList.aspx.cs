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
using ShipSoft.CrewManager.BusinessLogicLayer;
using ShipSoft.CrewManager.BusinessObjects;
using ShipSoft.CrewManager.Operational;
using System.IO;

public partial class FormReporting_FollowUpList : System.Web.UI.Page
{
    string VesselId = "", FollowUpCat = "";
    Authority Auth;
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        //--------------------------------- PAGE ACCESS AUTHORITY ----------------------------------------------------
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 1079);
        if (chpageauth <= 0)
            Response.Redirect("blank.aspx");
        //------------------------------------------------------------------------------------------------------------
   
        try
        {
            //this.Form.DefaultButton = this.btn_Show.UniqueID.ToString();
            //lblMessage.Text = "";
            if (Session["loginid"] == null)
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Default.aspx';", true);
            }
            if (Session["loginid"] != null)
            {
                ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 7);
                OBJ.Invoke();
                Session["Authority"] = OBJ.Authority;
                Auth = OBJ.Authority;
            }
            if (!Page.IsPostBack)
            {
                BindFleet();
                BindVessel();
                txtFromDate.Text = DateTime.Parse("01/01/" + DateTime.Today.Year.ToString()).ToString("dd-MMM-yyyy");//daysinmonth.ToString("dd-MMM-yyyy");
                txtToDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");   //daysinmonth.AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                btn_Show_Click(sender, e);
            }
        }
        catch (Exception ex) { throw ex; }
    }
    //protected void BindVessel()
    //{
    //    try
    //    {
    //        chklst_Vsls.Items.Clear();
    //        this.chklst_Vsls.DataTextField = "VesselName";
    //        this.chklst_Vsls.DataValueField = "VesselId";
    //        this.chklst_Vsls.DataSource = Inspection_Master.getMasterDataforInspection("Vessel", "VesselId", "VesselName" ,Convert.ToInt32(Session["loginid"].ToString()));
    //        this.chklst_Vsls.DataBind();
    //        for (int a = 0; a < chklst_Vsls.Items.Count; a++)
    //        {
    //            chklst_Vsls.Items[a].Selected = true;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    public void BindBlankGrid()
    {
        try
        {
            DataTable dt = new DataTable();
            DataRow dr;
            dt.Columns.Add("VesselId");
            dt.Columns.Add("VesselName");
            dt.Columns.Add("FollowUpItems");
            dt.Columns.Add("OpenItems");
            dt.Columns.Add("OverDueItems");

            for (int i = 0; i < 14; i++)
            {
                dr = dt.NewRow();
                dt.Rows.Add(dr);
                dt.Rows[dt.Rows.Count - 1][0] = "";
                dt.Rows[dt.Rows.Count - 1][1] = "";
                dt.Rows[dt.Rows.Count - 1][2] = "";
                dt.Rows[dt.Rows.Count - 1][3] = "";
                dt.Rows[dt.Rows.Count - 1][4] = "";
            }

            Grd_FollowUpList.DataSource = dt;
            Grd_FollowUpList.DataBind();
            //Grd_FollowUpList.SelectedIndex = -1;
        }
        catch (Exception ex) { throw ex; }
    }
    protected void btn_Show_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtFromDate.Text != "")
            {
                if (txtToDate.Text.Trim() == "")
                {
                    //lblMessage.Text = "Please enter To Date.";
                    txtToDate.Focus();
                    return;
                }
            }
            if (txtFromDate.Text != "" || txtToDate.Text != "")
            {
                if (DateTime.Parse(txtToDate.Text.Trim()) < DateTime.Parse(txtFromDate.Text.Trim()))
                {
                    //lblMessage.Text = "To Date cannot be less than From Date.";
                    txtToDate.Focus();
                    return;
                }
            }
            if (ddlVessel.SelectedIndex == 0)
            {
                for (int J = 0; J < ddlVessel.Items.Count; J++)
                {
                    //if (chklst_Vsls.Items[J].Selected == true)
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

            for (int i = 0; i < chklst_FollowUpCat.Items.Count; i++)
            {
                if (chklst_FollowUpCat.Items[i].Selected == true)
                {
                    if (FollowUpCat == "")
                    {
                        FollowUpCat = chklst_FollowUpCat.Items[i].Value;
                    }
                    else
                    {
                        FollowUpCat = FollowUpCat + "," + chklst_FollowUpCat.Items[i].Value;
                    }
                }
            }
            HiddenField_FollowUpCat.Value = FollowUpCat;
            HiddenField_FromDate.Value = txtFromDate.Text.Trim();
            HiddenField_ToDate.Value = txtToDate.Text.Trim();
            DataTable dt1 = FollowUp_Tracker.SelectFollowUpSearching(FollowUpCat, VesselId, txtFromDate.Text, txtToDate.Text, Convert.ToInt32(Session["loginid"].ToString()));
            if (dt1.Rows.Count > 0)
            {
                Grd_FollowUpList.DataSource = dt1;
                Grd_FollowUpList.DataBind();
                lblTotRec.Text = "Total Records : " + dt1.Rows.Count.ToString();
            }
            else
            {
                BindBlankGrid();
                lblTotRec.Text = "Total Records : " + dt1.Rows.Count.ToString();
            }
        }
        catch (Exception ex) { throw ex; }
    }
   
    protected void btn_Reset_Click(object sender, EventArgs e)
    {
        chklst_FollowUpCat.Items[0].Selected = true;
        chklst_FollowUpCat.Items[1].Selected = true;
        chklst_FollowUpCat.Items[2].Selected = true;
        chklst_FollowUpCat.Items[3].Selected = true;
        BindVessel();
        txtFromDate.Text = "";
        txtToDate.Text = "";
        btn_Show_Click(sender, e);
    }
    protected void Grd_FollowUpList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            //Grd_FollowUpList.PageIndex = e.NewPageIndex;
            //Grd_FollowUpList.SelectedIndex = -1;
            btn_Show_Click(sender, e);
        }
        catch (Exception ex) { throw ex; }
    }
    protected void btn_ShowAll_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlVessel.SelectedIndex == 0)
            {
                for (int J = 0; J < ddlVessel.Items.Count; J++)
                {
                    //if (chklst_Vsls.Items[J].Selected == true)
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
            for (int i = 0; i < chklst_FollowUpCat.Items.Count; i++)
            {
                if (chklst_FollowUpCat.Items[i].Selected == true)
                {
                    if (FollowUpCat == "")
                    {
                        FollowUpCat = chklst_FollowUpCat.Items[i].Value;
                    }
                    else
                    {
                        FollowUpCat = FollowUpCat + "," + chklst_FollowUpCat.Items[i].Value;
                    }
                }
            }
            HiddenField_FollowUpCat.Value = FollowUpCat;
            HiddenField_FromDate.Value = "";
            HiddenField_ToDate.Value = "";
            DataTable dt1 = FollowUp_Tracker.SelectFollowUpSearching(FollowUpCat, VesselId, HiddenField_FromDate.Value, HiddenField_ToDate.Value, Convert.ToInt32(Session["loginid"].ToString()));
            if (dt1.Rows.Count > 0)
            {
                Grd_FollowUpList.DataSource = dt1;
                Grd_FollowUpList.DataBind();
            }
            else
            {
                BindBlankGrid();
            }
        }
        catch (Exception ex) { throw ex; }
    }

    // Added events and function
    protected void chk_Inactive_OnCheckedChanged(object sender, EventArgs e)
    {
        BindVessel();
    }
    protected void ddlFleet_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        BindVessel();
        //BindGrid();
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
        string sql = "SELECT VesselID,Vesselname FROM DBO.Vessel v with(nolock) Where 1=1 and v.VesselId in (Select VesselId from UserVesselRelation with(nolock) where Loginid = " +  Convert.ToInt32(Session["loginid"].ToString()) + ") ";
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
    protected void btnClear_OnClick(object sender, EventArgs e)
    {
        ddlFleet.SelectedIndex = 0;
        chk_Inactive.Checked = false;
        txtFromDate.Text = DateTime.Parse("01/01/" + DateTime.Today.Year.ToString()).ToString("dd-MMM-yyyy");//daysinmonth.ToString("dd-MMM-yyyy");
        txtToDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");   //daysinmonth.AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
        BindVessel();
        ddlVessel.SelectedIndex = 0;
        btn_Show_Click(sender, e);
    }

   
}
