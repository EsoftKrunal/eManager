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

public partial class AccountMajorMaster : System.Web.UI.Page
{
    public int SelectedMajId
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
       BindMajorCat();
    }
    #endregion
    private void BindMajorCat()
    {
        string str = "select * from dbo.tblAccountsMajor order by MajorCat asc";
        DataTable dtMajCat = Common.Execute_Procedures_Select_ByQuery(str);
        if (dtMajCat != null)
        {
            RptMajCat.DataSource = dtMajCat;
            RptMajCat.DataBind();
            if (dtMajCat.Rows.Count <= 0)
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
        dvConfirmCancel.Visible = true;
        ImageButton imgEdit = (ImageButton)sender;

        Label lblMajCatID = (Label)imgEdit.Parent.FindControl("lblMajCatID");
        Label lblMajorCat = (Label)imgEdit.Parent.FindControl("lblMajorCat");
        Label lblMajSeqNo = (Label)imgEdit.Parent.FindControl("lblMajSeqNo");

        SelectedMajId = Common.CastAsInt32( lblMajCatID.Text);

        hfAccID.Value = lblMajCatID.Text;
        txtMajorCat.Text = lblMajorCat.Text;
        txtMajSeqNo.Text = lblMajSeqNo.Text;
        DivUpdate.Visible = true;
    }

    protected void imgUpdate_OnClick(object sender, EventArgs e)
    {
        try
        {
            string sql = "";
            if (txtMajorCat.Text.Trim() == "")
            {
                lblmsg.Text = "Enter major category .";
                return;
            }
            if (txtMajSeqNo.Text.Trim() == "")
            {
                lblmsg.Text = "Enter Major Seq No.";
                return;
            }
            if (hfAccID.Value == "")
            {
                sql = "insert into  dbo.tblAccountsMajor values('" + txtMajorCat.Text.Trim() + "'," + txtMajSeqNo.Text.Trim() + " )";
            }
            else
            {
                sql = "update dbo.tblAccountsMajor set MajorCat='" + txtMajorCat.Text + "',MajSeqNo=" + txtMajSeqNo.Text + " where MajCatID=" + hfAccID.Value + "";
            }
            Common.Execute_Procedures_Select_ByQuery(sql);
            //DivUpdate.Visible = false;
            SelectedMajId = 0;
            BindMajorCat();
            if (hfAccID.Value == "")
                lblmsg.Text = "Record saved successfully.";
            else
                lblmsg.Text = "Record Updated successfully.";
            dvConfirmCancel.Visible = false;
        }
        catch(Exception ex)
        {
            lblmsg.Text = "Error while saving. "+ex.Message;
        }

    }

    protected void imgDelete_OnClick(object sender, EventArgs e)
    {
        try
        {
            ImageButton imgDelete = (ImageButton)sender;
            Label lblMajCatID = (Label)imgDelete.Parent.FindControl("lblMajCatID");
            hfAccID.Value = lblMajCatID.Text;


            string sql = "delete from dbo.tblAccountsMajor where MajCatID=" + hfAccID.Value + "";
            Common.Execute_Procedures_Select_ByQuery(sql);
            BindMajorCat();
            lblmsg.Text = "Record deleted successfully.";
        }
        catch (Exception ex)
        {
            lblmsg.Text = "Error while deleting. " + ex.Message;
        }
    }
    protected void imgCalcel_OnClick(object sender, EventArgs e)
    {
        SelectedMajId = 0;
        hfAccID.Value = "";
        BindMajorCat();
        dvConfirmCancel.Visible = false;
    }
    protected void imgAddNew_OnClick(object sender, EventArgs e)
    {
        //DivUpdate.Visible = true;
        dvConfirmCancel.Visible = true;
        txtMajorCat.Text = "";
        txtMajSeqNo.Text = "";
        hfAccID.Value = "";
        SelectedMajId = 0;

    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "tr", "SetLastFocus('dvscroll_RFQ');", true);
    }
    
}
