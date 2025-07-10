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

public partial class DepartmentMaster : System.Web.UI.Page
{
    public string SelectedDepId  
    {
        get { return hfPRID.Value ; }
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
            //authPR.IsDelete
            //authRFQList = new AuthenticationManager(227, int.Parse(Session["loginid"].ToString()), ObjectType.Page);
        }
        catch (Exception ex)
        {
            Response.Redirect("~/NoPermission.aspx?Message=" + ex.Message);
        }
        #endregion ----------------------------------------


       lblmsg.Text = "";
       BindDepartment();
    }
    #endregion
    private void BindDepartment()
    {
        string str = "select Dept,DeptName from dbo.sql_tblSMDPRDept order by DeptName ";
        DataTable dtMajCat = Common.Execute_Procedures_Select_ByQuery(str);
        if (dtMajCat != null)
        {
            RptDept.DataSource = dtMajCat;
            RptDept.DataBind();
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
        ImageButton imgEdit = (ImageButton)sender;

        Label lblDepCode = (Label)imgEdit.Parent.FindControl("lblDepCode");
        Label lblDepName = (Label)imgEdit.Parent.FindControl("lblDepName");
        SelectedDepId = lblDepCode.Text;


        txtDeptCode.Text = lblDepCode.Text;
        txtDeptName.Text = lblDepName.Text;

        hfDepID.Value = lblDepCode.Text;
        //DivUpdate.Visible = true;
        dvConfirmCancel.Visible = true;
    }

    protected void imgUpdate_OnClick(object sender, EventArgs e)
    {
        try
        {
            string sql = "";
            if (txtDeptCode.Text.Trim() == "")
            {
                lblmsg.Text = "Enter department code.";
                return;
            }
            if (txtDeptName.Text.Trim() == "")
            {
                lblmsg.Text = "Enter department name.";
                return;
            }

            if (hfDepID.Value == "")
            {
                sql = "insert into  dbo.sql_tblSMDPRDept values('" + txtDeptCode.Text.Trim() + "','" + txtDeptName.Text.Trim() + "' )";
            }
            else
            {
                sql = "update dbo.sql_tblSMDPRDept set Dept='" + txtDeptCode.Text + "',DeptName='" + txtDeptName.Text + "' where Dept='" + hfDepID.Value + "'";
            }
            Common.Execute_Procedures_Select_ByQuery(sql);
            //DivUpdate.Visible = false;
            SelectedDepId = "0";
            BindDepartment();
            if (hfDepID.Value == "")
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
            Label lblDepCode = (Label)imgDelete.Parent.FindControl("lblDepCode");
            hfDepID.Value = lblDepCode.Text;


            string sql = "delete from dbo.sql_tblSMDPRDept where Dept='" + hfDepID.Value + "'";
            Common.Execute_Procedures_Select_ByQuery(sql);
            BindDepartment();
            lblmsg.Text = "Record deleted successfully.";
        }
        catch (Exception ex)
        {
            lblmsg.Text = "Error while deleting. " + ex.Message;
        }
    }
    protected void imgCalcel_OnClick(object sender, EventArgs e)
    {
        //DivUpdate.Visible = false;
        dvConfirmCancel.Visible = false;
        SelectedDepId = "0";
        hfDepID.Value = "";
        BindDepartment();
    }
    protected void imgAddNew_OnClick(object sender, EventArgs e)
    {
        //DivUpdate.Visible = true;
        dvConfirmCancel.Visible = true;
        txtDeptCode.Text = "";
        txtDeptName.Text = "";

        SelectedDepId = "0";
        hfDepID.Value = "";

    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "tr", "SetLastFocus('dvscroll_RFQ');", true);
    }
}
