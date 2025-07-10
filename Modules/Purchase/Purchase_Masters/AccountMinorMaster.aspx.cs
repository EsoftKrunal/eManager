using System;
using System.Collections;
using System.Collections.Specialized; 
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Xml;
//using Microsoft.Office.Interop.Outlook;    
/// <summary>
/// Page Name            : ReqFromVessels.aspx
/// Purpose              : Listing Of Files Received From Vessel
/// Author               : Shobhita Singh
/// Developed on         : 15 September 2010
/// </summary>

public partial class AccountMinorMaster : System.Web.UI.Page
{
    public int SelectedMidId
    {
        get { return Convert.ToInt32("0" + hfPRID.Value); }
        set { hfPRID.Value = value.ToString(); }
    }
    public AuthenticationManager authPR = new AuthenticationManager(0, 0, ObjectType.Page);
    #region ---------- PageLoad ------------
    // PAGE LOAD
    protected void Page_Load(object sender, EventArgs e)
    {
        //---------------------------------------
        ProjectCommon.SessionCheck();
        //---------------------------------------
        #region --------- USER RIGHTS MANAGEMENT -----------

         
        try
        {
            authPR = new AuthenticationManager(1062, int.Parse(Session["loginid"].ToString()), ObjectType.Page);
            if (!(authPR.IsView))
            {
                Response.Redirect("~/AuthorityError.aspx");
            }
            imgAddNew.Visible = authPR.IsAdd;
            
            
            //authRFQList = new AuthenticationManager(227, int.Parse(Session["loginid"].ToString()), ObjectType.Page);
        }
        catch (Exception ex)
        {
            Response.Redirect("~/NoPermission.aspx?Message=" + ex.Message);
        }
        #endregion ----------------------------------------


        lblmsg.Text = "";
        BindMinCat();
    }
    #endregion
    private void BindMinCat()
    {
        string str = "select * from dbo.tblAccountsMinor order by MinorCat asc";
        DataTable dtMinCat = Common.Execute_Procedures_Select_ByQuery(str);
        if (dtMinCat != null)
        {
            RptMinCat.DataSource = dtMinCat;
            RptMinCat.DataBind();
            if (dtMinCat.Rows.Count <= 0)
            {
                lblmsg.Text = "No Data Found.";
            }
        }
        else
        {
            lblmsg.Text = "No Data Found.";
        }

    }

    protected void imgEdit_OnClick(object sender, EventArgs e)
    {
        ImageButton imgEdit = (ImageButton)sender;

        Label lblMinCatID = (Label)imgEdit.Parent.FindControl("lblMinCatID");
        Label lblMinorCat = (Label)imgEdit.Parent.FindControl("lblMinorCat");
        Label lblMinSeqNo = (Label)imgEdit.Parent.FindControl("lblMinSeqNo");

        SelectedMidId = Common.CastAsInt32(lblMinCatID.Text);

        hfAccID.Value = lblMinCatID.Text;
        txtMinCat.Text = lblMinorCat.Text;
        txtMinSeqNo.Text = lblMinSeqNo.Text;
        //DivUpdate.Visible = true;
        dvConfirmCancel.Visible = true;
    }
    protected void imgUpdate_OnClick(object sender, EventArgs e)
    {
        try
        {
            if (txtMinCat.Text.Trim() == "")
            {
                lblmsg.Text = "Please enter minot cat."; return;
            }
            if (txtMinSeqNo.Text.Trim() == "")
            {
                lblmsg.Text = "Please enter minor seq no."; return;
            }

            string sql = "";
            if (hfAccID.Value == "")
                sql = "insert into  dbo.tblAccountsMinor values('" + txtMinCat.Text.Trim() + "'," + txtMinSeqNo.Text.Trim() + ")";
            else
                sql = "update dbo.tblAccountsMinor set MinorCat='" + txtMinCat.Text + "',MinSeqNo=" + txtMinSeqNo.Text + " where MinCatID=" + hfAccID.Value + "";


            Common.Execute_Procedures_Select_ByQuery(sql);
            //DivUpdate.Visible = false;
            SelectedMidId = 0;
            BindMinCat();
            if (hfAccID.Value == "")
                lblmsg.Text = "Record saved successfully.";
            else
                lblmsg.Text = "Record Updated successfully.";
            hfAccID.Value = "";

            dvConfirmCancel.Visible = false;
        }
        catch (Exception ex)
        {
            lblmsg.Text = "Error while saving. " + ex.Message;
        }
    }
    protected void imgCalcel_OnClick(object sender, EventArgs e)
    {
        //DivUpdate.Visible = false;
        dvConfirmCancel.Visible = false;
        SelectedMidId = 0;
        hfAccID.Value = "";
        BindMinCat();
    }
    protected void imgAddNew_OnClick(object sender, EventArgs e)
    {
        //DivUpdate.Visible = true;
        dvConfirmCancel.Visible = true;
        hfAccID.Value = "";
        txtMinCat.Text = "";
        txtMinSeqNo.Text = "";
        SelectedMidId = 0;
    }

    protected void imgDel_OnClick(object sender, EventArgs e)
    {
        try
        {
            ImageButton imgDelete = (ImageButton)sender;
            Label lblMinCatID = (Label)imgDelete.Parent.FindControl("lblMinCatID");
            hfAccID.Value = lblMinCatID.Text;


            string sql = "delete from dbo.tblAccountsMinor where MinCatID=" + hfAccID.Value + "";
            Common.Execute_Procedures_Select_ByQuery(sql);
            BindMinCat();
            lblmsg.Text = "Record deleted successfully.";
        }
        catch (Exception ex)
        {
            lblmsg.Text = "Error while deleting. " + ex.Message;
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "tr", "SetLastFocus('dvscroll_RFQ');", true);
    }
}
