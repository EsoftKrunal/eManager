using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;

public partial class Emtm_HR_Peap : System.Web.UI.Page
{
    public AuthenticationManager auth;
    public string SessionDeleimeter = ConfigurationManager.AppSettings["SessionDelemeter"];
    public int PeapID
    {
        get
        {
            return Common.CastAsInt32(ViewState["PeapID"]);
        }
        set
        {
            ViewState["PeapID"] = value;
        }
    }
    public int SelectedId
    {
        get
        {
            return Common.CastAsInt32(ViewState["SelectedId"]);
        }
        set
        {
            ViewState["SelectedId"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        //***********Code to check page acessing Permission
        //int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 244);
        //if (chpageauth <= 0)
        //{
        //    Response.Redirect("../AuthorityError.aspx");
        //}
        //*******************
        Session["CurrentModule"] = "PEAP";
        Session["CurrentPage"] = 1;

        lblmessage.Text = "";
        lblForwardMsg.Text = "";
        lblMsgAddNew.Text = "";
        lblMsg_MFB.Text = "";

        if (!IsPostBack)
        {
            BindYear();
            BindCategoty();
            BindOffice();
            BindPosition();
            BindDepartmentCNP();
            BindPosition_CNP();
            BindEmployee();
            LoadSession();

            BindGrid();
        }
    }
    protected void LoadSession()
    {
        {
            string[] Delemeters = { SessionDeleimeter };
            string values = "" + Session["Peap_SEARCH"];
            string[] ValueList = values.Split(Delemeters, StringSplitOptions.None);
            try
            {
                ddlYear.SelectedValue = ValueList[0];
                txt_FirstName_Search.Text = ValueList[1];
                ddlPeapCategory.SelectedValue = ValueList[2];
                ddlOffice.SelectedValue = ValueList[3];
                ddlPosition.SelectedValue = ValueList[4];
                ddlStatus.SelectedValue = ValueList[5];
            }
            catch { }
        }
    }
    protected void WriteSession()
    {
        string values = ddlYear.SelectedValue + SessionDeleimeter +
                        txt_FirstName_Search.Text + SessionDeleimeter +
                        ddlPeapCategory.SelectedValue + SessionDeleimeter +
                        ddlOffice.SelectedValue + SessionDeleimeter +
                        ddlPosition.SelectedValue + SessionDeleimeter +
                        ddlStatus.SelectedValue;
        Session["Peap_SEARCH"] = values;
    }
    protected void btnViewPeap_Click(object sender, EventArgs e)
    {
        int _PeapID = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "fsad", "window.open('PeapSummary.aspx?PeapID=" + _PeapID + "&Mode=A','');", true);
        Response.Redirect("PeapSummary.aspx?PeapID=" + _PeapID + "&Mode=A");
        //        Response.Redirect("PeapSummary.aspx?PeapID=" + _PeapID);
    }
    protected void btn_Search_Click(object sender, EventArgs e)
    {
        WriteSession();
        PeapID = 0;
        BindGrid();
    }
    protected void btn_Clear_Click(object sender, EventArgs e)
    {
        ClearSession();
        ClearAllData();
    }
    protected void btn_add_Click(object sender, EventArgs e)
    {
        dvConfirmCancel.Visible = true;
        ddlEmployeeList.SelectedIndex = 0;
        ddlPeapCategory.SelectedIndex = 0;
        txtPeriodFrom.Text = "";
        txtPeriodTo.Text = "";
    }
    protected void ClearSession()
    {
        Session["Peap_SEARCH"] = null;
    }
    protected void lbViewPeap_Click(object sender, EventArgs e)
    {
        PeapID = Common.CastAsInt32(((LinkButton)sender).CommandArgument);
        HiddenField hfEmpId = (HiddenField)((LinkButton)sender).FindControl("hfEmpId");

        Session["EmpId"] = hfEmpId.Value;
        BindGrid();

        //Response.Redirect("Emtm_PeapSummary.aspx?PeapID=" + PeapID + "&Mode=A");
    }


    protected void btn_Delete_Click(object sender, EventArgs e)
    {
        int _PeapID = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        if (_PeapID != 0)
        {

            Common.Set_Procedures("dbo.HR_DELETE_PEAP");
            Common.Set_ParameterLength(1);
            Common.Set_Parameters(
                new MyParameter("@PeapId", _PeapID)
                );

            Boolean Res;
            DataSet Ds = new DataSet();
            Res = Common.Execute_Procedures_IUD(Ds);
            if (Res)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "del", "alert('Peap deleted successfully.');", true);
                PeapID = 0;
                BindGrid();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "undel", "alert('Unable to delete Peap.');", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", "alert('Please select a peap to delete.');", true);
        }
    }

    //protected void btnedit_Click(object sender, ImageClickEventArgs e)
    //{
    //    SelectedId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
    //    Session["EmpMode"] = "Edit";
    //    Session["EmpId"] = SelectedId;
    //    Response.Redirect("HR_PersonalDetail.aspx");
    //}
    protected void lnkSort_Click(object sender, EventArgs e)
    {
        string LastKey = "" + ViewState["SortKey"];
        string NewKey = ((LinkButton)(sender)).CommandArgument;
        string LastMode = "" + ViewState["SortMode"];
        string NewMode = "Asc";
        if (LastKey == NewKey)
        {
            NewMode = (LastMode == "Asc") ? "Desc" : "Asc";
        }

        ViewState["SortKey"] = NewKey;
        ViewState["SortMode"] = NewMode;
        BindGrid();
    }
    protected void ddlOffice_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        BindPosition();
    }

    protected void ddlOccasion_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlOccasion.SelectedIndex != 0)
        {

            //troffice.Visible = true;
            //trAppPeriod.Visible = false;
            if (ddlOccasion.SelectedValue.Trim() == "I")
            {
                ddlOffice_CNP.SelectedIndex = 0;
                ddlDepartment_CNP.SelectedIndex = 0;
                ddlPosition_CNP.SelectedIndex = 0;
                ddlEmployeeList.SelectedIndex = 0;
                //ddlOffice_CNP.Enabled = true;
                //ddlDepartment_CNP.Enabled = true;
                //ddlPosition_CNP.Enabled = true;
                ddlEmployeeList.Enabled = true;

                chkSelAll.Checked = false;
                chkSelAll.Visible = false;
            }
            else
            {
                ddlOffice_CNP.SelectedIndex = 0;
                ddlDepartment_CNP.SelectedIndex = 0;
                ddlPosition_CNP.SelectedIndex = 0;
                ddlEmployeeList.SelectedIndex = 0;
                //ddlOffice_CNP.Enabled = false;
                //ddlDepartment_CNP.Enabled = false;
                //ddlPosition_CNP.Enabled = false;
                ddlEmployeeList.Enabled = false;

                chkSelAll.Visible = true;
                chkSelAll.Checked = true;
            }
        }
        else
        {
            ddlEmployeeList.SelectedIndex = 0;
            txtPeriodFrom.Text = "";
            txtPeriodTo.Text = "";

            //trEmployees.Visible = false;
            //trAppPeriod.Visible = false;
            //troffice.Visible = false;
        }
    }
    protected void ddlOffice_CNP_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindDepartmentCNP();
        BindPosition_CNP();
        BindEmployee();
        //trEmployees.Visible = true;
        //trAppPeriod.Visible = true;
    }
    protected void ddlDepartment_CNP_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindEmployee();
    }

    public void BindDepartmentCNP()
    {
        string sql = "";
        if (ddlOffice_CNP.SelectedIndex == 0)
            sql = "SELECT DeptId,DeptName FROM HR_Department order by DeptName";
        else
            sql = "SELECT DeptId,DeptName FROM HR_Department WHERE OfficeId=" + Common.CastAsInt32(ddlOffice_CNP.SelectedValue) + " order by DeptName";
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);

        ddlDepartment_CNP.DataSource = dt;
        ddlDepartment_CNP.DataTextField = "DeptName";
        ddlDepartment_CNP.DataValueField = "DeptId";
        ddlDepartment_CNP.DataBind();
        ddlDepartment_CNP.Items.Insert(0, new ListItem(" All ", "0"));

    }

    protected void ddlPosition_CNP_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindEmployee();
    }

    public void BindPosition_CNP()
    {
        string sql = "";
        if (ddlOffice_CNP.SelectedIndex == 0)
            sql = "Select PositionID,PositionName from Position order by PositionName";
        else
            sql = "Select PositionID,PositionName from Position where OfficeID=" + Common.CastAsInt32(ddlOffice_CNP.SelectedValue) + " order by PositionName";
        DataTable dtPosition = Common.Execute_Procedures_Select_ByQueryCMS(sql);

        ddlPosition_CNP.DataSource = dtPosition;
        ddlPosition_CNP.DataTextField = "PositionName";
        ddlPosition_CNP.DataValueField = "PositionID";
        ddlPosition_CNP.DataBind();
        ddlPosition_CNP.Items.Insert(0, new ListItem(" All ", ""));
    }


    protected void chkSelAll_CheckedChanged(object sender, EventArgs e)
    {
        if (!chkSelAll.Checked)
        {
            //ddlOffice_CNP.Enabled = true;
            //ddlDepartment_CNP.Enabled = true;
            //ddlPosition_CNP.Enabled = true;
            ddlEmployeeList.Enabled = true;
        }
        else
        {
            ddlOffice_CNP.SelectedIndex = 0;
            ddlDepartment_CNP.SelectedIndex = 0;
            ddlPosition_CNP.SelectedIndex = 0;
            ddlEmployeeList.SelectedIndex = 0;
            //ddlOffice_CNP.Enabled = false;
            //ddlDepartment_CNP.Enabled = false;
            //ddlPosition_CNP.Enabled = false;
            ddlEmployeeList.Enabled = false;
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ddlOccasion.SelectedValue.Trim() == "R")
        {
            if (chkSelAll.Checked)
            {
            }
            else
            {
                if (ddlEmployeeList.SelectedIndex == 0)
                {
                    lblMsgAddNew.Text = "Please select an employee.";
                    ddlEmployeeList.Focus();
                    return;
                }
            }
        }
        if (ddlOccasion.SelectedValue.Trim() == "I")
        {
            if (ddlEmployeeList.SelectedIndex == 0)
            {
                lblMsgAddNew.Text = "Please select an employee.";
                ddlEmployeeList.Focus();
                return;
            }
        }
        DateTime dt = new DateTime();
        DateTime dt1 = new DateTime();

        if (!DateTime.TryParse(txtPeriodFrom.Text.Trim(), out dt))
        {
            lblMsgAddNew.Text = "Please enter valid date.";
            txtPeriodFrom.Focus();
            return;
        }

        if (!DateTime.TryParse(txtPeriodTo.Text.Trim(), out dt1))
        {
            lblMsgAddNew.Text = "Please enter valid date.";
            txtPeriodTo.Focus();
            return;
        }

        if (Convert.ToDateTime(txtPeriodFrom.Text.Trim()) > Convert.ToDateTime(txtPeriodTo.Text.Trim()))
        {
            lblMsgAddNew.Text = "Period from can not be more than period to.";
            txtPeriodFrom.Focus();
            return;
        }

        if (Convert.ToDateTime(txtPeriodFrom.Text.Trim()) == Convert.ToDateTime(txtPeriodTo.Text.Trim()))
        {
            lblMsgAddNew.Text = "Period from and period to can not be same.";
            txtPeriodFrom.Focus();
            return;
        }
        if (ddlEmployeeList.SelectedIndex > 0)
        {
            Common.Set_Procedures("dbo.HR_Peap_Init");
            Common.Set_ParameterLength(5);
            Common.Set_Parameters(
                new MyParameter("@LoginId", Session["loginid"].ToString()),
                new MyParameter("@EmployeeId", ddlEmployeeList.SelectedValue.Trim() == "" ? "0" : ddlEmployeeList.SelectedValue.Trim()),
                new MyParameter("@PeriodFrom", txtPeriodFrom.Text.Trim()),
                new MyParameter("@PeriodTo", txtPeriodTo.Text.Trim()),
                new MyParameter("@Occasion", ddlOccasion.SelectedValue.Trim())
                );

            Boolean Res;
            DataSet Ds = new DataSet();
            Res = Common.Execute_Procedures_IUD(Ds);
        }
        else
        {
            foreach (ListItem li in ddlEmployeeList.Items)
            {
                int _EmpId = Common.CastAsInt32(li.Value);
                if (_EmpId > 0)
                {
                    Common.Set_Procedures("dbo.HR_Peap_Init");
                    Common.Set_ParameterLength(5);
                    Common.Set_Parameters(
                        new MyParameter("@LoginId", Session["loginid"].ToString()),
                        new MyParameter("@EmployeeId", _EmpId),
                        new MyParameter("@PeriodFrom", txtPeriodFrom.Text.Trim()),
                        new MyParameter("@PeriodTo", txtPeriodTo.Text.Trim()),
                        new MyParameter("@Occasion", ddlOccasion.SelectedValue.Trim())
                        );

                    Boolean Res;
                    DataSet Ds = new DataSet();
                    Res = Common.Execute_Procedures_IUD(Ds);
                }
            }
        }

        lblMsgAddNew.Text = "Data saved successfully.";
        btnCancel.Text = "Close";

        //if (Res)
        //{
        //    lblMsgAddNew.Text = "Data saved successfully.";
        //    btnCancel.Text = "Close";  
        //}
        //else
        //    lblMsgAddNew.Text = "Data could not be saved.";
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ddlOccasion.SelectedIndex = 0;
        ddlEmployeeList.SelectedIndex = 0;
        //ddlOffice_CNP.SelectedIndex = 0;
        //ddlOffice_CNP.Enabled = true;
        ddlEmployeeList.Enabled = true;
        chkSelAll.Visible = false;
        txtPeriodFrom.Text = "";
        txtPeriodTo.Text = "";
        //trEmployees.Visible = false;
        //trAppPeriod.Visible = false;
        dvConfirmCancel.Visible = false;
        BindGrid();
    }

    protected void btnFordward_Click(object sender, ImageClickEventArgs e)
    {
        PeapID = Common.CastAsInt32(((ImageButton)sender).CommandArgument);

        string SQL = "SELECT EmpID FROM HR_EmployeePeapMaster WHERE PeapId = " + PeapID.ToString();
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(SQL);
        int _EmpId = 0;

        if (dt.Rows.Count > 0)
        {
            _EmpId = Common.CastAsInt32(dt.Rows[0]["EmpId"]);
            ddlAppraisers.DataSource = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.GET_REPORTING_CHAIN(" + _EmpId + ")");
            ddlAppraisers.DataTextField = "EMPNAME";
            ddlAppraisers.DataValueField = "EMPID";
            ddlAppraisers.DataBind();
            dvForwardForAppraisal.Visible = true;
            btnCancelForward.Text = "Cancel";
        }

    }
    protected void btnForwardApp_Click(object sender, EventArgs e)
    {
        bool IsSelected = false;
        bool Success = true;
        foreach (ListItem item in ddlAppraisers.Items)
        {
            if (item.Selected)
            {
                IsSelected = true;
                break;
            }
        }

        if (!IsSelected)
        {
            lblForwardMsg.Text = "Please select an appraiser.";
            return;
        }

        foreach (ListItem item in ddlAppraisers.Items)
        {
            if (item.Selected)
            {

                Common.Set_Procedures("HR_Peap_ForwardForAppraisal");
                Common.Set_ParameterLength(2);
                Common.Set_Parameters(new MyParameter("@PEAPID", PeapID),
                               new MyParameter("@AppraiserId ", item.Value.Trim()));
                DataSet ds = new DataSet();

                if (Common.Execute_Procedures_IUD_CMS(ds))
                {

                    int res = Common.CastAsInt32(ds.Tables[0].Rows[0][0]);
                    if(res>0)
                    {
                        Success = false;
                        break;
                    }   
	       	    else
			{
			  Success = true;
			}
                
                }
                else
                {
                    Success = false;
                    break;
                }
            }
        }


        if (Success)
        {
            string str = "UPDATE HR_EmployeePeapMaster SET [STATUS] = 2 WHERE PeapId = " + PeapID.ToString() + " ; SELECT -1";

            DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(str);

            if (dt.Rows.Count > 0)
            {
                PeapID = 0;
                BindGrid();
                lblForwardMsg.Text = "Appraisal forwarded successfully.";
                btnCancelForward.Text = "Close";
            }
            else
            {
                lblForwardMsg.Text = "Unable to forward appraisal.";
            }
        }
        else
        {
            lblForwardMsg.Text = "Unable to forward appraisal. Job responsibility OR competency questionaire is not ready.";
        }

    }
    protected void btnCancelForward_Click(object sender, EventArgs e)
    {
        PeapID = 0;
        dvForwardForAppraisal.Visible = false;
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {

        ScriptManager.RegisterStartupScript(Page, this.GetType(), "tr", "SetLastFocus('dvscroll_Peap');", true);
    }
    //------------------ Function
    protected void BindGrid()
    {
        string WhereClause = "";
        string sql = "SELECT  " +
                    " PEAPID,CATEGORY,EMPCODE,PM.EMPID,FIRSTNAME + ' ' + MIDDLENAME + ' ' + FAMILYNAME AS EMPNAME,OFFICENAME,POSITIONNAME,DeptName AS DEPARTMENTNAME, " +
                    " AppraiselType =  CASE WHEN PM.Occasion = 'R' THEN 'Routine' ELSE 'Interim' END " +
                    " ,Replace(Convert(Varchar,PM.PEAPPERIODFROM,106),' ','-')PEAPPERIODFROM ,Replace(Convert(Varchar,PM.PEAPPERIODTO,106),' ','-')PEAPPERIODTO ,PM.STATUS AS STATUS1,  " +
                    " STATUS = CASE  " +
                    " WHEN PM.STATUS=-1 THEN 'PEAP Cancelled' " +
                    " WHEN PM.STATUS=0 THEN 'Self Assessment' " +
                    " WHEN PM.STATUS=1 THEN 'Self Assessment' " +
                    " WHEN PM.STATUS=2 THEN 'With Appraiser' " +
                    " WHEN PM.STATUS=3 THEN 'Fwd to Management' " +
                    " WHEN PM.STATUS=4 THEN 'PEAP Closed' " +
                    " WHEN PM.STATUS=5 THEN 'With Management' " +
                    " WHEN PM.STATUS=6 THEN 'With Management' " +
                    " ELSE '' END " +
                    " FROM  " +
                    " HR_EmployeePeapMaster PM  " +
                    " INNER JOIN dbo.Hr_PersonalDetails PD ON PM.EMPID=PD.EMPID " +
                    " LEFT JOIN dbo.HR_PeapCategory PC ON PC.PID=PM.PeapCategory " +
                    " INNER JOIN OFFICE O ON O.OFFICEID=PM.OFFICEID " +
                    " LEFT JOIN POSITION P ON P.POSITIONID=PD.POSITION " +
                    " LEFT JOIN HR_Department D ON D.DeptId=PD.DEPARTMENT where 1=1 ";

        if (ddlYear.SelectedValue.Trim() != "0")
            WhereClause = WhereClause + " And YEAR(PM.PeapPeriodFrom) =" + ddlYear.SelectedValue.Trim() + "";
        if (txt_FirstName_Search.Text.Trim() != "")
            WhereClause = WhereClause + "And ( PD.FIRSTNAME Like '%" + txt_FirstName_Search.Text.Trim() + "%' OR PD.MiddleName Like '%" + txt_FirstName_Search.Text.Trim() + "%' OR PD.FamilyName Like '%" + txt_FirstName_Search.Text.Trim() + "%' )";
        if (ddlPeapCategory.SelectedIndex != 0)
            WhereClause = WhereClause + " And PM.PeapCategory='" + ddlPeapCategory.SelectedValue.Trim() + "'";
        if (ddlOffice.SelectedIndex != 0)
            WhereClause = WhereClause + " And PM.OfficeID=" + ddlOffice.SelectedValue + "";
        if (ddlPosition.SelectedIndex != 0)
            WhereClause = WhereClause + " And PM.PositionID=" + ddlPosition.SelectedValue + "";
        if (ddlStatus.SelectedIndex != 0)
            WhereClause = WhereClause + " And PM.STATUS=" + ddlStatus.SelectedValue + "";

        if (ViewState["SortKey"] != null && ViewState["SortMode"] != null)
        {
            sql = sql + WhereClause + " ORDER BY " + ViewState["SortKey"].ToString() + " " + ViewState["SortMode"].ToString();
        }
        else
        {
            sql = sql + WhereClause + " ORDER BY FIRSTNAME,MIDDLENAME";
        }

        DataTable Ds = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        rptData.DataSource = Ds;
        rptData.DataBind();
        RowsCounter.Text = Ds.Rows.Count.ToString();

    }
    //protected void rptData_ItemDataBound(object sender, RepeaterItemEventArgs e)
    //{
    //    if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
    //    {
    //        HiddenField hfStatus = (HiddenField)e.Item.FindControl("hfStatus");
    //        System.Web.UI.HtmlControls.HtmlTableCell td_SA = (System.Web.UI.HtmlControls.HtmlTableCell)e.Item.FindControl("td_SA");
    //        System.Web.UI.HtmlControls.HtmlTableCell td_ABA = (System.Web.UI.HtmlControls.HtmlTableCell)e.Item.FindControl("td_ABA");
    //        System.Web.UI.HtmlControls.HtmlTableCell td_MF = (System.Web.UI.HtmlControls.HtmlTableCell)e.Item.FindControl("td_MF");
    //        System.Web.UI.HtmlControls.HtmlTableCell td_PC = (System.Web.UI.HtmlControls.HtmlTableCell)e.Item.FindControl("td_PC");

    //        if (hfStatus.Value.ToString() == "0")
    //        {
    //            td_SA.BgColor = "Yellow";                
    //            td_ABA.BgColor = "Gray";                
    //            td_MF.BgColor = "Gray";                
    //            td_PC.BgColor = "Gray";

    //            td_SA.Style.Add("color", "Black");
    //            td_ABA.Style.Add("color", "White");
    //            td_MF.Style.Add("color", "White");
    //            td_PC.Style.Add("color", "White");

    //        }
    //        else if (hfStatus.Value.ToString() == "1")
    //        {
    //            td_SA.BgColor = "Green";                
    //            td_ABA.BgColor = "Yellow";                
    //            td_MF.BgColor = "Gray";                
    //            td_PC.BgColor = "Gray";

    //            td_SA.Style.Add("color", "White");
    //            td_ABA.Style.Add("color", "Black");
    //            td_MF.Style.Add("color", "White");
    //            td_PC.Style.Add("color", "White");

    //        }
    //        else if (hfStatus.Value.ToString() == "2")
    //        {
    //            td_SA.BgColor = "Green";                
    //            td_ABA.BgColor = "Yellow";                
    //            td_MF.BgColor = "Gray";
    //            td_PC.BgColor = "Gray";

    //            td_SA.Style.Add("color", "White");
    //            td_ABA.Style.Add("color", "Black");
    //            td_MF.Style.Add("color", "White");
    //            td_PC.Style.Add("color", "White");

    //        }
    //        else if (hfStatus.Value.ToString() == "3" || hfStatus.Value.ToString() == "5")
    //        {
    //            td_SA.BgColor = "Green";
    //            td_ABA.BgColor = "Green";
    //            td_MF.BgColor = "Yellow";
    //            td_PC.BgColor = "Gray";

    //            td_SA.Style.Add("color", "White");
    //            td_ABA.Style.Add("color", "White");
    //            td_MF.Style.Add("color", "Black");
    //            td_PC.Style.Add("color", "White");

    //        }
    //        else if (hfStatus.Value.ToString() == "4")
    //        {
    //            td_SA.BgColor = "Green";
    //            td_ABA.BgColor = "Green";
    //            td_MF.BgColor = "Green";
    //            td_PC.BgColor = "Green";

    //            td_SA.Style.Add("color", "White");
    //            td_ABA.Style.Add("color", "White");
    //            td_MF.Style.Add("color", "White");
    //            td_PC.Style.Add("color", "White");
    //        }
    //        else if (hfStatus.Value.ToString() == "6")
    //        {
    //            td_SA.BgColor = "Green";
    //            td_ABA.BgColor = "Green";
    //            td_MF.BgColor = "Green";
    //            td_PC.BgColor = "Yellow";

    //            td_SA.Style.Add("color", "White");
    //            td_ABA.Style.Add("color", "White");
    //            td_MF.Style.Add("color", "White");
    //            td_PC.Style.Add("color", "Black");
    //        }
    //        else
    //        {
    //            td_SA.BgColor = "Gray";
    //            td_ABA.BgColor = "Gray";
    //            td_MF.BgColor = "Gray";
    //            td_PC.BgColor = "Gray";

    //            td_SA.Style.Add("color", "White");
    //            td_ABA.Style.Add("color", "White");
    //            td_MF.Style.Add("color", "White");
    //            td_PC.Style.Add("color", "White");
    //        }

    //    }
    //}
    public void BindCategoty()
    {
        string sql = "select PID,Category from HR_PeapCategory";
        DataTable dtPosition = Common.Execute_Procedures_Select_ByQueryCMS(sql);

        ddlPeapCategory.DataSource = dtPosition;
        ddlPeapCategory.DataTextField = "Category";
        ddlPeapCategory.DataValueField = "PID";
        ddlPeapCategory.DataBind();
        ddlPeapCategory.Items.Insert(0, new ListItem(" All ", ""));
    }
    public void BindOffice()
    {
        string sql = "Select OfficeID,OfficeName from Office order by OfficeName";
        DataTable dtOffice = Common.Execute_Procedures_Select_ByQueryCMS(sql);

        ddlOffice.DataSource = dtOffice;
        ddlOffice.DataTextField = "OfficeName";
        ddlOffice.DataValueField = "OfficeID";
        ddlOffice.DataBind();
        ddlOffice.Items.Insert(0, new ListItem(" All ", ""));

        ddlOffice_CNP.DataSource = dtOffice;
        ddlOffice_CNP.DataTextField = "OfficeName";
        ddlOffice_CNP.DataValueField = "OfficeID";
        ddlOffice_CNP.DataBind();
        ddlOffice_CNP.Items.Insert(0, new ListItem(" All ", ""));

        string officesql = "Select Office from DBO.Hr_PersonalDetails WHERE USERID=" + Session["loginid"].ToString();
        DataTable dtOffice1 = Common.Execute_Procedures_Select_ByQueryCMS(officesql);
        int UserOfficeId = 0;
        if(dtOffice1.Rows.Count>0)
            UserOfficeId=Common.CastAsInt32(dtOffice1.Rows[0][0].ToString());
        if (UserOfficeId > 0)
        {
            ddlOffice.SelectedValue = UserOfficeId.ToString();
            ddlOffice_OnSelectedIndexChanged(new object(), new EventArgs());
            ddlOffice_CNP.SelectedValue = UserOfficeId.ToString();
            ddlOffice_CNP_SelectedIndexChanged(new object(), new EventArgs());
            if (UserOfficeId != 3)
            {
                ddlOffice.Enabled = false;
                ddlOffice_CNP.Enabled = false;
                
            }
        }
    }
    public void BindPosition()
    {
        string sql = "";
        if (ddlOffice.SelectedIndex == 0)
            sql = "Select PositionID,PositionName from Position order by PositionName";
        else
            sql = "Select PositionID,PositionName from Position where OfficeID=" + Common.CastAsInt32(ddlOffice.SelectedValue) + " order by PositionName";
        DataTable dtPosition = Common.Execute_Procedures_Select_ByQueryCMS(sql);

        ddlPosition.DataSource = dtPosition;
        ddlPosition.DataTextField = "PositionName";
        ddlPosition.DataValueField = "PositionID";
        ddlPosition.DataBind();
        ddlPosition.Items.Insert(0, new ListItem(" All ", ""));
    }
    public void ClearAllData()
    {
        PeapID = 0;
        //txtempno.Text = "";
        txt_FirstName_Search.Text = "";
        ddlPeapCategory.SelectedIndex = 0;
        ddlOffice.SelectedIndex = 0;
        ddlPosition.SelectedIndex = 0;
        ddlStatus.SelectedIndex = 0;
        ddlYear.SelectedValue = DateTime.Today.Year.ToString();
        BindGrid();

    }
    public void BindEmployee()
    {
        string sql = "select EmpID, FirstName+' '+MiddleName + ' ' + FamilyName as Name from Hr_PersonalDetails WHERE ( POSITION <> 1 ) AND ( [Status] <> 'I' ) ";

        string WHERE = "";

        if (ddlOffice_CNP.SelectedIndex != 0)
        {
            WHERE = "AND (Office = " + ddlOffice_CNP.SelectedValue.Trim() + " ) ";
        }

        if (ddlDepartment_CNP.SelectedIndex != 0)
        {
            WHERE = WHERE + "AND (department = " + ddlDepartment_CNP.SelectedValue.Trim() + " )";
        }

        if (ddlPosition_CNP.SelectedIndex != 0)
        {
            WHERE = WHERE + "AND (Position = " + ddlPosition_CNP.SelectedValue.Trim() + " )";
        }

        sql = sql + WHERE + " ORDER By FirstName ";

        DataTable Dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);

        ddlEmployeeList.DataSource = Dt;
        ddlEmployeeList.DataTextField = "Name";
        ddlEmployeeList.DataValueField = "EmpID";
        ddlEmployeeList.DataBind();
        ddlEmployeeList.Items.Insert(0, new ListItem(" All Employees in the List ", ""));

    }

    public void BindYear()
    {
        for (int i = DateTime.Now.Year + 1; i >= 2012; i--)
        {
            ddlYear.Items.Add(new System.Web.UI.WebControls.ListItem(i.ToString(), i.ToString()));
        }
        ddlYear.SelectedValue = DateTime.Today.Year.ToString();
    }

    //--------------- Forward - Searching

    //public void BindAppraiser(object sender, EventArgs e)
    //{
    //    string sql = "select EmpID, FirstName+' '+ MiddleName + ' ' + FamilyName as Name from Hr_PersonalDetails  WHERE ( EMPID <> (SELECT EmpID FROM HR_EmployeePeapMaster WHERE PeapId = " + PeapID.ToString() + " ) ) AND ( [Status] <> 'I' ) ";
    //    string Where = "";
    //    if (ddlOffice_Forward.SelectedIndex != 0)
    //    {
    //        Where = Where + "AND (Office = " + ddlOffice_Forward.SelectedValue.Trim() + ") ";
    //    }
    //    if (ddlDept_Forward.SelectedIndex != 0)
    //    {
    //        Where = Where + "AND (Department = " + ddlDept_Forward.SelectedValue.Trim() + ") ";
    //    }

    //    sql = sql + Where + " ORDER By FirstName ";

    //    DataTable dtAppraiser = Common.Execute_Procedures_Select_ByQueryCMS(sql);

    //    ddlAppraisers.DataSource = dtAppraiser;
    //    ddlAppraisers.DataTextField = "Name";
    //    ddlAppraisers.DataValueField = "EmpID";
    //    ddlAppraisers.DataBind();
    //}

    //--------------- Forward - Management Feed Back

    protected void btnForward_M_Click(object sender, ImageClickEventArgs e)
    {
        PeapID = Common.CastAsInt32(((ImageButton)sender).CommandArgument);

        //string SQL = " SELECT Office,Department from Hr_PersonalDetails WHERE ( EMPID = (SELECT EmpID FROM HR_EmployeePeapMaster WHERE PeapId = " + PeapID.ToString() + " ) ) ";
        //DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(SQL);

        //if (dt.Rows.Count > 0)
        //{
        //    ddlOffice_Forward.SelectedValue = dt.Rows[0]["Office"].ToString();
        //    ddlOffice_Forward_OnSelectedIndexChanged(new object(), new EventArgs());
        //    ddlDept_Forward.SelectedValue = dt.Rows[0]["Department"].ToString();
        //}

        BindManagers(sender, e);
        dvForwardForManagement.Visible = true;
        btnCancel_MFB.Text = "Cancel";
    }

    public void BindManagers(object sender, EventArgs e)
    {
        //string sql = "select EmpID, FirstName+' '+ MiddleName + ' ' + FamilyName as Name from Hr_PersonalDetails  WHERE ( EMPID <> (SELECT EmpID FROM HR_EmployeePeapMaster WHERE PeapId = " + PeapID.ToString() + " ) ) AND ( [Status] <> 'I' AND PID = 1 ) ";
        string sql = "select EmpID, FirstName+' '+ MiddleName + ' ' + FamilyName as Name from Hr_PersonalDetails  WHERE position in (4,1,89) or empid in (83,234,184) order by firstname";

        string Where = "";
        //if (ddlOffice_Forward.SelectedIndex != 0)
        //{
        //    Where = Where + "AND (Office = " + ddlOffice_Forward.SelectedValue.Trim() + ") ";
        //}
        //if (ddlDept_Forward.SelectedIndex != 0)
        //{
        //    Where = Where + "AND (Department = " + ddlDept_Forward.SelectedValue.Trim() + ") ";
        //}

        //sql = sql + Where + " ORDER By FirstName ";

        DataTable dtAppraiser = Common.Execute_Procedures_Select_ByQueryCMS(sql);

        cblManagers.DataSource = dtAppraiser;
        cblManagers.DataTextField = "Name";
        cblManagers.DataValueField = "EmpID";
        cblManagers.DataBind();

    }

    protected void btnSave_MFB_Click(object sender, EventArgs e)
    {
        bool IsSelected = false;
        bool Success = true;
        foreach (ListItem item in cblManagers.Items)
        {
            if (item.Selected)
            {
                IsSelected = true;
                break;
            }
        }

        if (!IsSelected)
        {
            lblMsg_MFB.Text = "Please select a manager.";
            return;
        }


        foreach (ListItem item in cblManagers.Items)
        {
            if (item.Selected)
            {
                Common.Set_Procedures("HR_Peap_ForwardToManagement");
                Common.Set_ParameterLength(2);
                Common.Set_Parameters(new MyParameter("@PEAPID", PeapID),
                               new MyParameter("@ManagerId", item.Value.Trim()));
                DataSet ds = new DataSet();

                if (!Common.Execute_Procedures_IUD_CMS(ds))
                {
                    Success = false;
                    break;
                }
            }
        }

        if (Success)
        {
            string str = "UPDATE HR_EmployeePeapMaster SET [STATUS] = 5 WHERE PeapId = " + PeapID.ToString() + " ; SELECT -1";

            DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(str);

            if (dt.Rows.Count > 0)
            {
                PeapID = 0;
                BindGrid();
                lblMsg_MFB.Text = "Management forwarded successfully.";
                btnCancel_MFB.Text = "Close";
            }
            else
            {
                lblMsg_MFB.Text = "Unable to forward management.";
            }

        }
        else
        {
            lblMsg_MFB.Text = "Unable to forward management.";
        }

    }
    protected void btnCancel_MFB_Click(object sender, EventArgs e)
    {
        PeapID = 0;
        dvForwardForManagement.Visible = false;
    }
    protected void btnCloasure_Click(object sender, ImageClickEventArgs e)
    {
        PeapID = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "rpt", "showReportClosure('" + PeapID + "');", true);


    }
    protected void btnPeapReport_Click(object sender, ImageClickEventArgs e)
    {
        PeapID = Common.CastAsInt32(((ImageButton)sender).CommandArgument);

        ScriptManager.RegisterStartupScript(this, this.GetType(), "rpt", "showReport('" + PeapID + "');", true);
    }
    protected void btn_Print_Click(object sender, EventArgs e)
    {

        ScriptManager.RegisterStartupScript(this, this.GetType(), "rpt", "window.open('../../Reporting/Peap_Summary.aspx?Year=" + ddlYear.SelectedValue + "&Ofc=" + ddlOffice.SelectedValue + "&PL=" + ddlPeapCategory.SelectedValue + "','');", true);
    }
    
    
}
