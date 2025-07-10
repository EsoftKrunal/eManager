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

public partial class AccountMaster : System.Web.UI.Page
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
            
            //authRFQList = new AuthenticationManager(210, int.Parse(Session["loginid"].ToString()), ObjectType.Page);
        }
        catch (Exception ex)
        {
            Response.Redirect("~/NoPermission.aspx?Message=" + ex.Message);
        }
        #endregion ----------------------------------------



        lblmsg.Text = "";
        if (!Page.IsPostBack)
        {
            BindMajCat();
            BindMidCat();
            BindMinCat();
            BindAccounts();
            BindClsCat();
        }
    }
    #endregion
    private void BindAccounts()
    {
        string str = string.Empty;
        if (ddlMidCate.SelectedIndex > 0)
        {
            str = "select " +
           " (select MajorCat from dbo.tblAccountsMajor M where M.MajCatID=A.MajCatID ) MajCat " +
           " ,(select MidCat from dbo.tblAccountsMid M where M.MidCatID=A.MidCatID ) MidCat " +
           " ,(select MinorCat from dbo.tblAccountsMinor M where M.MinCatID=A.MinCatID ) MinCat " +
           " ,(select Cls_Cat_name from dbo.tblClsCat C where C.Cls_Cat=a.Cls_Cat)cls_catName" +
           " ,* from dbo.sql_tblSMDPRAccounts A with(nolock) where A.MidCatID = " +ddlMidCate.SelectedValue+ " order by AccountName";
        }
        else
        {
            str = "select " +
           " (select MajorCat from dbo.tblAccountsMajor M where M.MajCatID=A.MajCatID ) MajCat " +
           " ,(select MidCat from dbo.tblAccountsMid M where M.MidCatID=A.MidCatID ) MidCat " +
           " ,(select MinorCat from dbo.tblAccountsMinor M where M.MinCatID=A.MinCatID ) MinCat " +
           " ,(select Cls_Cat_name from dbo.tblClsCat C where C.Cls_Cat=a.Cls_Cat)cls_catName" +
           " ,* from dbo.sql_tblSMDPRAccounts A order by AccountName";
        }
       
        DataTable dtMidCat = Common.Execute_Procedures_Select_ByQuery(str);
        if (dtMidCat != null)
        {
            RptAccount.DataSource = dtMidCat;
            RptAccount.DataBind();
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

    protected void imgEdit_OnClick(object sender, EventArgs e)
    {
        try
        {
            ImageButton imgEdit = (ImageButton)sender;
            HiddenField hfAccountID = (HiddenField)imgEdit.Parent.FindControl("hfAccountID");
            hfAccID.Value = hfAccountID.Value;


            Label lblAccountNumber = (Label)imgEdit.Parent.FindControl("lblAccountNumber");
            Label lblAccountName = (Label)imgEdit.Parent.FindControl("lblAccountName");
            Label lblActive = (Label)imgEdit.Parent.FindControl("lblActive");

            Label lblMajCatID = (Label)imgEdit.Parent.FindControl("lblMajCatID");
            Label lblMidCatID = (Label)imgEdit.Parent.FindControl("lblMidCatID");
            Label lblMinCatID = (Label)imgEdit.Parent.FindControl("lblMinCatID");
            Label lblcls_cat = (Label)imgEdit.Parent.FindControl("lblcls_cat");

            HiddenField hfActive = (HiddenField)imgEdit.Parent.FindControl("hfActive");
            HiddenField hfMajCatID = (HiddenField)imgEdit.Parent.FindControl("hfMajCatID");
            HiddenField hfMidCatID = (HiddenField)imgEdit.Parent.FindControl("hfMidCatID");
            HiddenField hfMinCatID = (HiddenField)imgEdit.Parent.FindControl("hfMinCatID");



            SelectedMidId = Common.CastAsInt32(hfAccountID.Value);


            txtAccNo.Text = lblAccountNumber.Text;
            txtAccName.Text = lblAccountName.Text;
            if (hfActive.Value != "")
            {
                ddlActive.SelectedValue = hfActive.Value;
            }
            else
            {
                ddlActive.SelectedIndex = 0;
            }
            
            
            if (hfMajCatID.Value != "")
            {
                ddlMajCat.SelectedValue = hfMajCatID.Value;
            }
            else
            {
                ddlMajCat.SelectedIndex = 0;
            }

            if (hfMidCatID.Value != "")
            {
                ddlMidCat.SelectedValue = hfMidCatID.Value;
            }
            else
            {
                ddlMidCat.SelectedIndex = 0;
            }
           
            if (hfMinCatID.Value != "")
            {
                ddlMinCat.SelectedValue = hfMinCatID.Value;
            }
            else
            {
                ddlMinCat.SelectedIndex = 0;
            }
            


            if (lblcls_cat.Text.Trim() != "")
                ddlClsCat.SelectedValue = lblcls_cat.Text;
            else
                ddlClsCat.SelectedIndex = 0;


            //DivUpdate.Visible = true;
            dvConfirmCancel.Visible = true;
            BindAccounts();
        }
        catch(Exception ex) { }
    }

    protected void imgUpdate_OnClick(object sender, EventArgs e)
    {
        try
        {
            if (txtAccNo.Text.Trim() == "")
            {
                lblmsg.Text = "Enter account number .";return;
            }
            if (txtAccName.Text.Trim() == "")
            {
                lblmsg.Text = "Enter account name.";return;
            }
            if (ddlActive.SelectedIndex == 0)
            {
                lblmsg.Text = "Select active field."; return;
            }
            if (ddlMajCat.SelectedIndex == 0)
            {
                lblmsg.Text = "Select major category."; return;
            }

            string sql = "";
            if (hfAccID.Value == "")
                sql = "insert into  dbo.sql_tblSMDPRAccounts values(" + txtAccNo.Text.Trim() + ",'" + txtAccName.Text.Trim() + "','" + ddlActive.SelectedValue + "'," + ddlMajCat.SelectedValue + ",'" + ((ddlMidCat.SelectedValue == "") ? "null" : ddlMidCat.SelectedValue) + "'," + ((ddlMinCat.SelectedValue == "") ? "null" : ddlMinCat.SelectedValue) + "," + ((ddlClsCat.SelectedIndex == 0) ? "null" : ddlClsCat.SelectedValue) + ")";
            else
                sql = "update dbo.sql_tblSMDPRAccounts set AccountNumber='" + txtAccNo.Text + "',AccountName='" + txtAccName.Text.Trim() + "',Active='" + ddlActive.SelectedValue + "',MajCatID=" + ddlMajCat.SelectedValue + ",MidCatID=" + ((ddlMidCat.SelectedValue == "") ? "null" : ddlMidCat.SelectedValue) + ",MinCatID=" + ((ddlMinCat.SelectedValue == "") ? "null" : ddlMinCat.SelectedValue) + ",cls_cat =" + ((ddlClsCat.SelectedIndex == 0) ? "null" : ddlClsCat.SelectedValue) + " where AccountID=" + hfAccID.Value + "";
            Common.Execute_Procedures_Select_ByQuery(sql);
            //DivUpdate.Visible = false;
            SelectedMidId = 0;
            BindMidCat();
            if (hfAccID.Value == "")
                lblmsg.Text = "Record saved successfully.";
            else
                lblmsg.Text = "Record Updated successfully.";
            hfAccID.Value = "";
            BindAccounts();
            dvConfirmCancel.Visible = false; ;

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
        hfAccID.Value = "";
        SelectedMidId = 0;
        hfAccID.Value = "";
        BindMidCat();
    }
    protected void imgAddNew_OnClick(object sender, EventArgs e)
    {
        //DivUpdate.Visible = true;
        dvConfirmCancel.Visible = true;
        txtAccName.Text = "";
        txtAccNo.Text = "";
        ddlClsCat.SelectedIndex = 0;
        ddlActive.SelectedIndex = 0;
        ddlMajCat.SelectedIndex = 0;
        ddlMidCat.SelectedIndex = 0; ddlMinCat.SelectedIndex = 0;
        hfAccID.Value = "";
        SelectedMidId = 0;
    }

    protected void imgDel_OnClick(object sender, EventArgs e)
    {
        try
        {
            ImageButton imgDelete = (ImageButton)sender;
            HiddenField hfAccountID = (HiddenField)imgDelete.Parent.FindControl("hfAccountID");
            hfAccID.Value = hfAccountID.Value;


            string sql = "delete from dbo.sql_tblSMDPRAccounts where AccountID=" + hfAccID.Value + "";
            Common.Execute_Procedures_Select_ByQuery(sql);
            //  BindMidCat();
            BindAccounts(); 
            lblmsg.Text = "Record deleted successfully.";
        }
        catch (Exception ex)
        {
            lblmsg.Text = "Error while deleting. " + ex.Message;
        }
    }

    public void BindMajCat()
    {
        string str = "select * from dbo.tblAccountsMajor order by MajorCat asc";
        DataTable dtMinCat = Common.Execute_Procedures_Select_ByQuery(str);
        if (dtMinCat != null)
        {
            ddlMajCat.DataSource = dtMinCat;
            ddlMajCat.DataTextField = "MajorCat";
            ddlMajCat.DataValueField = "majCatID";
            ddlMajCat.DataBind();
            ddlMajCat.Items.Insert(0,new  ListItem("Select",""));

        }
    }
    public void BindMidCat()
    {
        string str = "select * from dbo.tblAccountsMid order by MIDcat asc";
        DataTable dtMinCat = Common.Execute_Procedures_Select_ByQuery(str);
        if (dtMinCat != null)
        {
            ddlMidCat.DataSource = dtMinCat;

            ddlMidCat.DataTextField = "MidCat";
            ddlMidCat.DataValueField = "MidCatid";
            ddlMidCat.DataBind();
            ddlMidCat.Items.Insert(0,new  ListItem("Select",""));

            ddlMidCate.DataSource = dtMinCat;

            ddlMidCate.DataTextField = "MidCat";
            ddlMidCate.DataValueField = "MidCatid";
            ddlMidCate.DataBind();
            ddlMidCate.Items.Insert(0, new ListItem("--All--", ""));
        }
    }
    public void BindMinCat()
    {
        string str = "select * from dbo.tblAccountsMinor order by MinorCat asc";
        DataTable dtMinCat = Common.Execute_Procedures_Select_ByQuery(str);
        if (dtMinCat != null)
        {
            ddlMinCat.DataSource = dtMinCat;

            ddlMinCat.DataTextField = "MinorCat";
            ddlMinCat.DataValueField = "MinCatid";
            ddlMinCat.DataBind();
            ddlMinCat.Items.Insert(0,new  ListItem("Select",""));
        }
    }

    public void BindClsCat()
    {
        string str = "select Cls_Cat,Cls_Cat_name from dbo.tblClsCat";
        DataTable dtClsCat = Common.Execute_Procedures_Select_ByQuery(str);
        if (dtClsCat != null)
        {
            ddlClsCat.DataSource = dtClsCat;

            ddlClsCat.DataTextField = "Cls_Cat_name";
            ddlClsCat.DataValueField = "Cls_Cat";
            ddlClsCat.DataBind();
            ddlClsCat.Items.Insert(0, new ListItem("Select", ""));
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "tr", "SetLastFocus('dvscroll_RFQ');", true);
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            BindAccounts();
        }
        catch (Exception ex) { }
        
    }
}
