using System;
using System.Data;
using System.Configuration;
using System.Reflection;
using System.Collections;
using System.Web;
using System.Data.Common;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;

public partial class SeafarerLogin: System.Web.UI.Page
{
    int UserId = 0;
    public string SelCrewnumber
    {
        set { ViewState["SelCrewnumber"] = value; }
        get { return ViewState["SelCrewnumber"].ToString(); }
    }
    public int SelectedCrewId
    {
        //get { return Convert.ToInt32("0" + hfPRID.Value); }
        //set { hfPRID.Value = value.ToString(); }
        get { return Common.CastAsInt32( ViewState["SelectedCrewId"]); }
        set { ViewState["SelectedCrewId"] = value.ToString(); }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

        SelectedCrewId = SelectedCrewId;

        if (Session["UserName"] == null)
        {
            Response.Redirect("Login.aspx");
        }
        Session["Home Page"] = "Home Page";
        UserId = int.Parse(Session["UserId"].ToString());
        if (!Page.IsPostBack)
        {
            BindRank();
            BindVessel();
            BindCrewStatus();            
        }        
    }
    //---------------- Event
    protected void btnSearch_OnClick(object sender,EventArgs e)
    {
        if (txtCrewNumber.Text.Trim() == "" && txtName.Text.Trim() == "" && ddlRank.SelectedIndex == 0 && ddlVessel.SelectedIndex == 0 && ddlCrewStatus.SelectedIndex == 0 && ddlLoginStatus.SelectedIndex == 0)
        {
            Msgbox.ShowMessage("Please select at least on filter",true);
            return;
        }
        BindUsers();
    }
    protected void imgResetPass_OnClick (object sender, EventArgs e)
    {
        ImageButton imgResetPass = (ImageButton)sender;
        SelCrewnumber = imgResetPass.CommandArgument;
        HiddenField hfLoginStatus = (HiddenField)imgResetPass.Parent.FindControl("hfLoginStatus");
        HiddenField hfCrewID = (HiddenField)imgResetPass.Parent.FindControl("hfCrewID");

        SelectedCrewId = Common.CastAsInt32(hfCrewID.Value);

        divReseetPass.Visible = true;
        BindUsers();
    }
    protected void btnResetPass_OnClick(object sender, EventArgs e)
    {
        if (txtNewPass.Text.Trim() == "")
        {
            Msgbox.ShowMessage("Please enter new password.",true);
            txtNewPass.Focus();return;
        }
        if (txtConfirmPass.Text.Trim() == "")
        {
            Msgbox.ShowMessage("Please enter confirm password.", true);
            txtConfirmPass.Focus(); return;
        }
        if (txtNewPass.Text.Trim() != txtConfirmPass.Text.Trim())
        {
            Msgbox.ShowMessage("Password does not match.", true);
            txtConfirmPass.Focus(); return;
        }

        //string sql = "update  DBO.Cp_crewLogin set Password='" + ProjectCommon.Encrypt(txtNewPass.Text.Trim(), "qwerty1235") + "' where UserName='" + SelCrewnumber.ToString() + "' ; select -1";
        ECommon.Set_Procedures("sp_Cp_ResetPassword");
        ECommon.Set_ParameterLength(2);
        ECommon.Set_Parameters(
                new EMyParameter("@USERNAME", SelCrewnumber),
                new EMyParameter("@PASSWORD", EProjectCommon.Encrypt(txtNewPass.Text.Trim(), "qwerty1235")));
        Boolean Res;
        DataSet ds=new DataSet();

        Res = ECommon.Execute_Procedures_IUD(ds);
        if (Res)
        {
            Msgbox.ShowMessage("Password has been updated.", false);
            divReseetPass.Visible = false;
            BindUsers();
            Clear();
            SelectedCrewId = 0;
            BindUsers();
        }
        else
        {
            Msgbox.ShowMessage("Password could not be updated.", true);
            Clear();
        }
    }
    protected void btnCancelPass_OnClick(object sender, EventArgs e)
    {
        divReseetPass.Visible = false;
        SelectedCrewId = 0;
        btnSearch_OnClick(sender, e);
    }
    protected void chkActive_OnCheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkActive = (CheckBox)sender;
        HiddenField hfCrewNumber = (HiddenField)chkActive.Parent.FindControl("hfCrewNumber");

        string vlSql = "select * from DBO.Cp_crewLogin where UserName='" + hfCrewNumber.Value + "'";
        DataTable valdt = Common.Execute_Procedures_Select_ByQuery(vlSql);
        if (valdt.Rows.Count == 0)
        {
            Msgbox.ShowMessage("No login details available for this user.",true);
            chkActive.Checked = false;return;
        }

        string sql = "update DBO.Cp_crewLogin set Status=" + ((chkActive.Checked) ? "1" : "0") + " where UserName='" + hfCrewNumber .Value+ "' ; select -1;";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
        if (dt != null)
        {
            Msgbox.ShowMessage("Record updated successfully.", false);
            divReseetPass.Visible = false;
            BindUsers();
            Clear();
        }
    }

    protected void btnBack_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("AdminDashBoard.aspx");
    }
    
    //---------------- Function

    public void BindUsers()
    {
        string sql = " select LoginStatus1,(case when isnull(LoginStatus1,0)=1 then 'Active' else 'In Active'end)LoginStatus  ,* from (select CrewNumber ,CrewID ,CurrentRankID ,CurrentVesselID,CrewStatusID " +
                    " , (FirstName+' '+MiddleName+' '+LastName)UName "+
                    " ,(Select RankCOde from DBO.Rank R where R.RankID=CP.CurrentRankID)Rank " +
                    " ,(Select VesselName from DBO.Vessel V where V.VesselID=CP.CurrentVesselID)Vessel " +
                    " ,(Select CrewStatusName from DBO.CrewStatus CS where CS.CrewStatusID=CP.CrewStatusID )CrewStatus " +
                    " ,(select Status from DBO.Cp_crewLogin l where l.Username=CP.CrewNumber)LoginStatus1 " +
                    " from DBO.CrewPersonalDetails CP  )tbl Where 1=1 ";

        string WhereClause = "";
        if (txtCrewNumber.Text.Trim() != "")
        {
            WhereClause = WhereClause + " and tbl.CrewNumber='" + txtCrewNumber.Text.Trim() + "'";
        }
        if (txtName.Text.Trim() != "")
        {
            WhereClause = WhereClause + " and tbl.UName like '%" + txtName.Text.Trim().Replace("'","''") + "%'";
        }
        if (ddlRank.SelectedIndex != 0)
        {
            WhereClause = WhereClause + " and tbl.CurrentRankID =" + ddlRank.SelectedValue + "";
        }
        if (ddlVessel.SelectedIndex != 0)
        {
            WhereClause = WhereClause + " and tbl.CurrentVesselID =" + ddlVessel.SelectedValue + "";
        }
        if (ddlCrewStatus.SelectedIndex != 0)
        {
            WhereClause = WhereClause + " and tbl.CrewStatusID =" + ddlCrewStatus.SelectedValue + "";
        }
        else
        {
            WhereClause = WhereClause + " and tbl.CrewStatusID not in(4,5)";
        }
        if (ddlLoginStatus.SelectedIndex != 0)
        {
            if (ddlLoginStatus.SelectedIndex==1)
                WhereClause = WhereClause + " and tbl.LoginStatus1=" + ddlLoginStatus.SelectedValue + "";
            else
                WhereClause = WhereClause + " and ( tbl.LoginStatus1=" + ddlLoginStatus.SelectedValue + " or tbl.LoginStatus1 is null )";
        }
        sql = sql + WhereClause + " order by (Select Ranklevel from DBO.Rank R where R.RankID=tbl.CurrentRankID)";
        DataTable ds = Common.Execute_Procedures_Select_ByQuery(sql);
        rpt_Users.DataSource = ds;
        rpt_Users.DataBind();
    }
    public void BindRank()
    {
        string sql = "Select RankID,RankCode from DBO.Rank ";
        DataTable ds = Common.Execute_Procedures_Select_ByQuery(sql);
        ddlRank.DataSource = ds;
        ddlRank.DataTextField = "RankCode";
        ddlRank.DataValueField = "RankID";
        ddlRank.DataBind();
        ddlRank.Items.Insert(0, new ListItem("All", "0"));
    }
    public void BindVessel()
    {
        string sql = "Select VesselID,VesselName from DBO.Vessel v where v.VesselStatusid<>2   ";
        DataTable ds = Common.Execute_Procedures_Select_ByQuery(sql);
        ddlVessel.DataSource = ds;
        ddlVessel.DataTextField = "VesselName";
        ddlVessel.DataValueField = "VesselID";
        ddlVessel.DataBind();
        ddlVessel.Items.Insert(0, new ListItem("All", "0"));
    }
    public void BindCrewStatus()
    {
        string sql = "select CrewStatusID,CrewStatusName from DBO.CrewStatus where CrewStatusID not in(4,5) ";
        DataTable ds = Common.Execute_Procedures_Select_ByQuery(sql);
        ddlCrewStatus.DataSource = ds;
        ddlCrewStatus.DataTextField = "CrewStatusName";
        ddlCrewStatus.DataValueField = "CrewStatusID";
        ddlCrewStatus.DataBind();
        ddlCrewStatus.Items.Insert(0, new ListItem("All", "0"));
    }
    public void Clear()
    {
        txtNewPass.Text = "";
        txtConfirmPass.Text = "";
     
    }


   
}
