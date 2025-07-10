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

public partial class AccountMapping : System.Web.UI.Page
{
    public int SelectedVessId
    {
        get { return Convert.ToInt32("0" + hfPRID.Value); }
        set { hfPRID.Value = value.ToString(); }
    }
    //public AuthenticationManager authPR = new AuthenticationManager(0, 0, ObjectType.Page);

    #region ---------- PageLoad ------------
    // PAGE LOAD
    protected void Page_Load(object sender, EventArgs e)
    {
        //---------------------------------------
        ProjectCommon.SessionCheck();
        //---------------------------------------
        #region --------- USER RIGHTS MANAGEMENT -----------

        AuthenticationManager authPR = new AuthenticationManager(1062, int.Parse(Session["loginid"].ToString()), ObjectType.Page);
        try
        {
          //  authPR = new AuthenticationManager(210, int.Parse(Session["loginid"].ToString()), ObjectType.Page);
            if (!(authPR.IsView))
            {
                Response.Redirect("~/AuthorityError.aspx");
            }
            
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
            Type();
            Department();
            AvailableAccounts();
        }
    }
    #endregion



    public void AvailableAccounts()
    {
        string StrWhereClause = "";
        if (ddlType.SelectedIndex != 0 && ddlDep.SelectedIndex != 0)
            StrWhereClause = StrWhereClause + " where Dept='" + ddlDep.SelectedValue + "' and PRType=" + ddlType.SelectedValue + "";


        string sql = "SELECT Lk_tblSMDAccounts.AccountID, Lk_tblSMDAccounts.AccountNumber, Lk_tblSMDAccounts.AccountName FROM  " +
                " (SELECT AccountID, (convert(varchar, AccountNumber )+ '-' + AccountName) AS AcctName, AccountNumber, AccountName " +
                " FROM dbo.sql_tblSMDPRAccounts WHERE Active='Y' )Lk_tblSMDAccounts  " +
                " WHERE Lk_tblSMDAccounts.AccountID Not In (Select accountid from tblSMDDeptAccounts " + StrWhereClause + " ) " +
                " ORDER BY Lk_tblSMDAccounts.AccountNumber, Lk_tblSMDAccounts.AccountName ";
        DataTable DtAvailableAcc = Common.Execute_Procedures_Select_ByQuery(sql);
        if (DtAvailableAcc != null)
        {
            rptAvailableAccCode.DataSource = DtAvailableAcc;
            rptAvailableAccCode.DataBind();

            lblAvlAccCode.Text = "( " + DtAvailableAcc.Rows.Count.ToString() + " )";
        }
                

    }
    public void SelectedAccounts()
    {
        string StrWhereClause = "";
        if (ddlType.SelectedIndex != 0 && ddlDep.SelectedIndex != 0)
            StrWhereClause = StrWhereClause + " WHERE Lk_tblSMDDeptAccounts.Dept='" + ddlDep.SelectedValue + "' AND Lk_tblSMDDeptAccounts.PRType=" + ddlType.SelectedValue + "";
        else        
            StrWhereClause = StrWhereClause + " WHERE 1=2";
        

        string sql = " SELECT Lk_tblSMDDeptAccounts.DeptAcctID, Lk_tblSMDPRTypes.PRTypeDesc, Lk_tblSMDPRDept.DeptName, Lk_tblSMDPRAccounts.AccountNumber " +
                    " ,Lk_tblSMDPRAccounts.AccountName, Lk_tblSMDDeptAccounts.Dept, Lk_tblSMDDeptAccounts.AccountID, Lk_tblSMDDeptAccounts.PRType " +

                    " FROM  tblSMDDeptAccounts Lk_tblSMDDeptAccounts  " +
                    " INNER JOIN dbo.sql_tblSMDPRDept Lk_tblSMDPRDept ON Lk_tblSMDDeptAccounts.Dept = Lk_tblSMDPRDept.Dept " +
                    " INNER JOIN dbo.sql_tblSMDPRAccounts Lk_tblSMDPRAccounts ON Lk_tblSMDDeptAccounts.AccountID = Lk_tblSMDPRAccounts.AccountID " +
                    " INNER JOIN dbo.sql_tblSMDPRTypes Lk_tblSMDPRTypes ON Lk_tblSMDDeptAccounts.PRType = Lk_tblSMDPRTypes.PrType " +

                    " " + StrWhereClause + " " +
                    " ORDER BY Lk_tblSMDPRDept.DeptName, Lk_tblSMDPRAccounts.AccountNumber; ";

        DataTable DtSelectedAcc = Common.Execute_Procedures_Select_ByQuery(sql);
        if (DtSelectedAcc != null)
        {
            rptSelectedAcc.DataSource = DtSelectedAcc;
            rptSelectedAcc.DataBind();

            if (DtSelectedAcc.Rows.Count != 0)
                lblSelAccCode.Text = "( " + DtSelectedAcc.Rows.Count.ToString() + " )";
            else
                lblSelAccCode.Text = "";

        }
    }
    public void Type()
    {
        string sql = "SELECT Lk_tblSMDPRTypes.* FROM dbo.sql_tblSMDPRTypes Lk_tblSMDPRTypes ORDER BY Lk_tblSMDPRTypes.PRTypeDesc; ";
        DataTable DtType = Common.Execute_Procedures_Select_ByQuery(sql);
        if (DtType != null)
        {
            ddlType.DataSource = DtType;
            ddlType.DataTextField = "PrTypeDesc";
            ddlType.DataValueField = "PrType";
            ddlType.DataBind();
            ddlType.Items.Insert(0, new ListItem("< Select >", "0"));
        }
    }
    public void Department()
    {
        string sql = "SELECT Lk_tblSMDPRDept.* FROM dbo.sql_tblSMDPRDept Lk_tblSMDPRDept ORDER BY Lk_tblSMDPRDept.DeptName; ";
        DataTable DtDep= Common.Execute_Procedures_Select_ByQuery(sql);
        if (DtDep != null)
        {
            ddlDep.DataSource = DtDep;
            ddlDep.DataTextField = "DeptName";
            ddlDep.DataValueField = "Dept";
            ddlDep.DataBind();
            ddlDep.Items.Insert(0, new ListItem("< Select >", "0"));
        }
    }

    protected void ddlType_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        AvailableAccounts();
        SelectedAccounts();
    }
    protected void ddlDep_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        AvailableAccounts();
        SelectedAccounts();
    }


    protected void imgRight_OnClick(object sender, EventArgs e)
    {
        if (ddlType.SelectedIndex == 0)
        {
            lblmsg.Text = "Please select type.";
            return;
        }
        if (ddlDep.SelectedIndex == 0)
        {
            lblmsg.Text = "Please select department.";
            return;
        }

        foreach (RepeaterItem RptItem in rptAvailableAccCode.Items)
        {
            CheckBox chkSelect = (CheckBox)RptItem.FindControl("chkSelect");

            if (chkSelect.Checked)
            {
                HiddenField hfAccID = (HiddenField)RptItem.FindControl("hfAccID");
                string sql = "insert into tblSMDDeptAccounts values('" + ddlDep.SelectedValue + "'," + hfAccID.Value + "," + ddlType.SelectedValue + ") select 'aa'";
                DataTable DtDep = Common.Execute_Procedures_Select_ByQuery(sql);
            }
        }
        
        AvailableAccounts();
        SelectedAccounts();

    }
    protected void imgLeft_OnClick(object sender, EventArgs e)
    {
        foreach (RepeaterItem RptItem in rptSelectedAcc.Items)
        {
            CheckBox chkSelect = (CheckBox)RptItem.FindControl("chkSelect");

            if (chkSelect.Checked)
            {
                HiddenField hfDeptAcctID = (HiddenField)RptItem.FindControl("hfDeptAcctID");
                string sql = "delete from tblSMDDeptAccounts  where DeptAcctID= " + hfDeptAcctID .Value+ "";
                DataTable DtDep = Common.Execute_Procedures_Select_ByQuery(sql);
            }
        }
        AvailableAccounts();
        SelectedAccounts();

        
    }

    protected void imgPring_OnClick(object sender, EventArgs e)
    {
        
        //-------------------------------------------------


        //string strAccNu = "";
        //foreach (RepeaterItem RptItm in rptSelectedAcc.Items)
        //{
        //    HiddenField hfAccNum = (HiddenField)RptItm.FindControl("hfAccNum");
        //    strAccNu = strAccNu +","+ hfAccNum.Value;
        //}
        //if (strAccNu != "")
        //    strAccNu = strAccNu.Substring(1);

        ScriptManager.RegisterStartupScript(this, this.GetType(), "Printtttt", "window.open('../Print.aspx?AccountMapping=1&AccTypeText=" + ddlType.SelectedItem.Text + "&AccDepText=" + ddlDep.SelectedItem.Text + "&AccType=" + ddlType.SelectedValue + "&AccDep="+ddlDep.SelectedValue+"');", true); 
    }
}
