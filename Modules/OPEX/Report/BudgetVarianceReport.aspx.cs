using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Configuration;

public partial class BudgetVarianceReport : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        //---------------------------------------
        ProjectCommon.SessionCheck();
        //---------------------------------------
        //AuthenticationManager authRecInv;
        #region --------- USER RIGHTS MANAGEMENT -----------
        //try
        //{
        //    authRecInv = new AuthenticationManager(234, int.Parse(Session["loginid"].ToString()), ObjectType.Page);
        //    if (!(authRecInv.IsView))
        //    {
        //        Response.Redirect("~/NoPermission.aspx", false);
        //    }
        //}
        //catch (Exception ex)
        //{
        //    Response.Redirect("~/NoPermission.aspx?Message=" + ex.Message);
        //}

        #endregion ----------------------------------------
        
        try
        {
            if (!Page.IsPostBack)
            {
                BindOwner();
                for (int i = DateTime.Today.Year; i >=2006 ; i--)
                    ddlYear.Items.Add(i.ToString());

                ddlMonth.Items.Add(new ListItem("Jan", "1"));
                ddlMonth.Items.Add(new ListItem("Feb", "2"));
                ddlMonth.Items.Add(new ListItem("Mar", "3"));
                ddlMonth.Items.Add(new ListItem("Apr", "4"));
                ddlMonth.Items.Add(new ListItem("May", "5"));
                ddlMonth.Items.Add(new ListItem("Jun", "6"));
                ddlMonth.Items.Add(new ListItem("Jul", "7"));
                ddlMonth.Items.Add(new ListItem("Aug", "8"));
                ddlMonth.Items.Add(new ListItem("Sep", "9"));
                ddlMonth.Items.Add(new ListItem("Oct", "10"));
                ddlMonth.Items.Add(new ListItem("Nov", "11"));
                ddlMonth.Items.Add(new ListItem("Dec", "12"));
           }

        }
        catch { }
    }
    // Event ----------------------------------------------------------------
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Search.aspx");
    }

    // Button     
    protected void btnReport_Click(object sender, EventArgs e)
    {
        if (ddlSearchBy.SelectedValue == "1")
        {
            //UP1.Update();
            iframe.Attributes.Add("src", "BudgetVarianceReportCrystal.aspx?FleetID=" + ddlFleet.SelectedValue + "&Year=" + ddlYear.SelectedValue + "&Month=" + ddlMonth.SelectedValue);
        }
        else
        {
            //UP1.Update();
            iframe.Attributes.Add("src", "BudgetVarianceReportCrystal.aspx?Owner=" + ddlOwner.SelectedValue+ "&Year=" + ddlYear.SelectedValue + "&Month=" + ddlMonth.SelectedValue);
        }
    }
    protected void ddlSearchBy_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSearchBy.SelectedValue == "1")
        {
            lblFleetOwnerText.Text = "Fleet";
            ddlFleet.Visible = true;
            ddlOwner.Visible = false;
        }
        else
        {
            lblFleetOwnerText.Text = "Owner";
            ddlFleet.Visible = false;
            ddlOwner.Visible = true;

        }
    }

    protected void BindOwner()
    {
        try
        {
            this.ddlOwner.DataTextField = "OwnerName";
            this.ddlOwner.DataValueField = "OwnerId";
            this.ddlOwner.DataSource = Common.Execute_Procedures_Select_ByQueryCMS("select OwnerId,OwnerName from dbo.Owner");

            //Inspection_Master.getMasterDataforInspection("Owner", "OwnerId", "OwnerName");

            this.ddlOwner.DataBind();
            this.ddlOwner.Items.Insert(0, new ListItem("All", "0"));
            this.ddlOwner.Items[0].Value = "0";            
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}


