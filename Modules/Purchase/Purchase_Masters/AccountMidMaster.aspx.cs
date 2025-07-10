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

public partial class AccountMidMaster : System.Web.UI.Page
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
        if (!Page.IsPostBack)
        {
            BindMidCat();
            BindGroup();
            BindMajorGroup();
        }
    }
    #endregion
    private void BindMidCat()
    {
        string str = "select "+
            " (select MajorCat  from dbo.tblAccountsMajor RM where RM.MajCatID =AM.MajGroupID )MajGroup, " +
            " (select GroupName from dbo.tblReportingGroups RG where RG.GroupID =AM.GroupID )GroupName, " +
            " * from dbo.tblAccountsMid AM order by MIDcat asc";
        DataTable dtMidCat = Common.Execute_Procedures_Select_ByQuery(str);
        if (dtMidCat != null)
        {
            RptMidCat.DataSource = dtMidCat;
            RptMidCat.DataBind();
            if (dtMidCat.Rows.Count <= 0)
            {
                lblmsg.Text = "No Data Found.";
            }
        }
        else
        {
            lblmsg.Text = "No Data Found.";
        }

    }
    public void BindGroup()
    {
        string sql = "select GroupName ,GroupID  from dbo.tblReportingGroups ";
        DataTable dtMidCat = Common.Execute_Procedures_Select_ByQuery(sql);
        if (dtMidCat != null)
        {
            ddlGroup.DataSource = dtMidCat;
            ddlGroup.DataTextField = "GroupName";
            ddlGroup.DataValueField = "GroupID";
            ddlGroup.DataBind();
            ddlGroup.Items.Insert(0, new ListItem("< Select >", "0"));

        }

    }
    public void BindMajorGroup()
    {
        string sql = "select MajorCat,MajCatID  from dbo.tblAccountsMajor ";
        DataTable dtMidCat = Common.Execute_Procedures_Select_ByQuery(sql);
        if (dtMidCat != null)
        {
            ddlMajorGroup.DataSource = dtMidCat;
            ddlMajorGroup.DataTextField = "MajorCat";
            ddlMajorGroup.DataValueField = "MajCatID";
            ddlMajorGroup.DataBind();
            ddlMajorGroup.Items.Insert(0, new ListItem("< Select >", "0"));
        }
    }



    protected void imgEdit_OnClick(object sender, EventArgs e)
    {
        ImageButton imgEdit = (ImageButton)sender;

        Label lblMidCatID = (Label)imgEdit.Parent.FindControl("lblMidCatID");
        Label lblMidCat = (Label)imgEdit.Parent.FindControl("lblMidCat");
        Label lblMidSeqNo = (Label)imgEdit.Parent.FindControl("lblMidSeqNo");

        HiddenField hfMajGroupID = (HiddenField)imgEdit.Parent.FindControl("hfMajGroupID");
        HiddenField hfGroupID = (HiddenField)imgEdit.Parent.FindControl("hfGroupID");
        Label lblSOAField = (Label)imgEdit.Parent.FindControl("lblSOAField");

        SelectedMidId = Common.CastAsInt32(lblMidCatID.Text);

        hfAccID.Value = lblMidCatID.Text;
        txtMidCat.Text = lblMidCat.Text;
        txtMidSeqNo.Text = lblMidSeqNo.Text;

        if (hfMajGroupID.Value != "")
            ddlMajorGroup.SelectedValue = hfMajGroupID.Value;
        else
            ddlMajorGroup.SelectedIndex = 0;

        if (hfGroupID.Value != "")
            ddlGroup.SelectedValue = hfGroupID.Value;
        else
            ddlGroup.SelectedIndex = 0;

        txtSOA.Text = lblSOAField.Text;

        //DivUpdate.Visible = true;
        dvConfirmCancel.Visible = true;
        BindMidCat();
    }

    protected void imgUpdate_OnClick(object sender, EventArgs e)
    {
        try
        {
            if (txtMidCat.Text.Trim() == "")
            {
                lblmsg.Text = "Enter mid category .";return;
            }
            if (txtMidSeqNo.Text.Trim() == "")
            {
                lblmsg.Text = "Enter mid Seq No.";return;
            }
            

            string sql = "";
            if (hfAccID.Value == "")
                sql = "insert into  dbo.tblAccountsMid     values('" + txtMidCat.Text.Trim() + "'," + txtMidSeqNo.Text.Trim() + "," + ((ddlMajorGroup.SelectedIndex == 0) ? "null" : ddlMajorGroup.SelectedValue) + "," + ((ddlGroup.SelectedIndex == 0) ? "null" : ddlGroup.SelectedValue) + "," + ((txtSOA.Text.Trim() == "") ? "null" : "'" + txtSOA.Text.Trim()) + "'" + ")";
            else
                sql = "update dbo.tblAccountsMid set MidCat='" + txtMidCat.Text + "',MidSeqNo=" + txtMidSeqNo.Text + ",MajGroupID=" + ((ddlMajorGroup.SelectedIndex == 0) ? "null" : ddlMajorGroup.SelectedValue) + ",GroupID=" + ((ddlGroup.SelectedIndex == 0) ? "null" : ddlGroup.SelectedValue) + ",SOAField=" + ((txtSOA.Text.Trim() == "") ? "null" : "'" + txtSOA.Text.Trim() + "'") + " where MidCatID=" + hfAccID.Value + "";
            Common.Execute_Procedures_Select_ByQuery(sql);
            //DivUpdate.Visible = false;
            SelectedMidId = 0;
            BindMidCat();
            if (hfAccID.Value == "")
                lblmsg.Text = "Record saved successfully.";
            else
                lblmsg.Text = "Record Updated successfully.";
            hfAccID.Value = "";
            dvConfirmCancel.Visible = false;

        }
        catch (Exception ex)
        {
            lblmsg.Text = "Error while saving. "+ex.Message;
        }

    }

    protected void imgCalcel_OnClick(object sender, EventArgs e)
    {
        //DivUpdate.Visible = false;
        dvConfirmCancel.Visible = false;
        SelectedMidId = 0;
        hfAccID.Value = "";
        BindMidCat();
    }
    protected void imgAddNew_OnClick(object sender, EventArgs e)
    {
        //DivUpdate.Visible = true;
        dvConfirmCancel.Visible = true;
        txtMidCat.Text = "";
        txtMidSeqNo.Text = "";
        ddlMajorGroup.SelectedIndex = 0;
        ddlGroup.SelectedIndex = 0;
        txtSOA.Text = "";
        SelectedMidId = 0;

    }

    protected void imgDel_OnClick(object sender, EventArgs e)
    {
        try
        {
            ImageButton imgDelete = (ImageButton)sender;
            Label lblMidCatID = (Label)imgDelete.Parent.FindControl("lblMidCatID");
            hfAccID.Value = lblMidCatID.Text;


            string sql = "delete from dbo.tblAccountsMid where MidCatID=" + hfAccID.Value + "";
            Common.Execute_Procedures_Select_ByQuery(sql);
            BindMidCat();
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
