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
using ShipSoft.CrewManager.BusinessObjects;
using ShipSoft.CrewManager.BusinessLogicLayer;
using ShipSoft.CrewManager.Operational;

public partial class VesselContractSearch : System.Web.UI.Page
{
    

    Authority Auth;
    int intTemp = 0;
    #region "PageLoad"
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //------------------------------------
            ProjectCommon.SessionCheck_New();
            //------------------------------------
            //--------------------------------- PAGE ACCESS AUTHORITY ----------------------------------------------------
            int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 1094);
            if (chpageauth <= 0)
                Response.Redirect("blank.aspx");
            //------------------------------------------------------------------------------------------------------------
            //this.Form.DefaultButton = this.btndearch.UniqueID.ToString();
            if (Session["loginid"] == null)
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Login.aspx';", true);
            }
            lblmessage.Text = "";
            lblrecord.Text = "";
            if (Session["PgFlag"] != null) 
            { lblmessage.Text = "Please select Contract from Search Page."
                    ; Session["PgFlag"] = null; } 
            else { 
                Session["Mode"] = ""; 
                //hid1.Text = "Init"; 
            }
            //if (Session["NwInspFlag"] != null) { lblmessage.Text = "Please save Planning first."; Session["NwInspFlag"] = null; } else { Session["Mode"] = ""; }

            if (Session["loginid"] != null)
            {
               
                ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 14);
                OBJ.Invoke();
                Session["Authority"] = OBJ.Authority;
                Auth = OBJ.Authority;
            }

            if (Page.IsPostBack == false)
            {
                if (Page.Request.QueryString["VSession"] != null)
                {
                    Session["Sort_Exp"] = null;
                    Session["Charter"] = null;
                    Session["VoyageNo"] = null;
                    Session["vesselId"] = null;
                    Session["ContractId"] = null;
                    Session["FromDate"] = null;
                    Session["Todate"] = null;
                }
                try
                {
                    if (Request.QueryString["Session"] != null)
                    {

                        Session["Sort_Exp"] = null;                      
                        Session["Charter"] = null;
                        Session["VoyageNo"] = null;
                        Session["vesselId"] = null;                      
                        Session["ContractId"] = null;
                        Session["FromDate"] = null;
                        Session["Todate"] = null;
                       
                    }
                    //--------------------
                    Session["SortExp"] = "ASC";
                    
                    
                    
                    BindCharter();
                    
                    if (Session["Charter"] != null)
                    {
                        if (Session["Charter"].ToString() != "")
                        {
                            ddlCharter.SelectedValue = Session["Charter"].ToString();
                        }
                    }
                    BindVessel();
                    if (Session["vesselId"] != null)
                    {
                        if (Session["vesselId"].ToString() != "")
                        {
                            ddVessel.SelectedValue = Session["vesselId"].ToString();
                        }
                    }


                    if (Session["Todate"] != null)
                    {
                        if (Session["Todate"].ToString() != "")
                        {
                            txt_todt.Text = Session["Todate"].ToString();
                        }
                        else
                        {
                            txt_todt.Text = "";
                        }
                    }
                    else
                    {
                        txt_todt.Text = "";
                    }
                    btnsearch_Click(sender, e);
                   
                    if (Session["PgIndex"] != null)
                        GrdContractRevenue.PageIndex = int.Parse(Session["PgIndex"].ToString());
                    
                      //  SearchData(GrdContractRevenue.Attributes["MySort"]);
                   
                    //------------
                    // bindgrid();
                    try
                    {
                        Alerts.HANDLE_AUTHORITY(11, btnNewContract, null, null, null, Auth);
                    }
                    catch
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Login.aspx';", true);
                    }
                }
                catch (Exception ex)
                {
                    lblrecord.Text = "";
                    lblmessage.Text = ex.StackTrace.ToString();
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        
    }
    #endregion

    #region "User Defined functions"

    protected void BindCharter()
    {
        try
        {
            this.ddlCharter.DataTextField = "ContractType";
            this.ddlCharter.DataValueField = "ContractTypeId";
            this.ddlCharter.DataSource = Revenue.getMasterDataforRevenue("RV_ContractTypeMaster", "ContractTypeId", "ContractType");
            this.ddlCharter.DataBind();
            this.ddlCharter.Items.Insert(0, new ListItem("All", "0"));
            this.ddlCharter.Items[0].Value = "0";
            
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    //Bind Grid when there is no data
    private void bindgrid()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;
        dt.Columns.Add("ContractId");
        dt.Columns.Add("ContractNo");
        dt.Columns.Add("VesselId");
        dt.Columns.Add("VesselName");
        dt.Columns.Add("ContractTypeId");
        dt.Columns.Add("ContractType");
        dt.Columns.Add("HireAmount");
        dt.Columns.Add("ContractStatus");
        dt.Columns.Add("Status");
        dt.Columns.Add("ExpectedExpenses");
        //       dt.Columns.Add("PortName");
        //       dt.Columns.Add("PlanDate");
        //       dt.Columns.Add("Supt");
        //       dt.Columns.Add("Status");
        //       dt.Columns.Add("LastDone");
        //       dt.Columns.Add("Planned");
        //       dt.Columns.Add("StatusColor");
        //    dt.Columns.Add("FromPort");
        //dt.Columns.Add("ToPort");

        for (int i = 0; i < 7; i++)
        {
            dr = dt.NewRow();
            dt.Rows.Add(dr);
            dt.Rows[dt.Rows.Count - 1][0] = "";
            dt.Rows[dt.Rows.Count - 1][1] = "";
            dt.Rows[dt.Rows.Count - 1][2] = "";
            dt.Rows[dt.Rows.Count - 1][3] = "";
            dt.Rows[dt.Rows.Count - 1][4] = "";
            dt.Rows[dt.Rows.Count - 1][5] = "";
            dt.Rows[dt.Rows.Count - 1][6] = "";
            dt.Rows[dt.Rows.Count - 1][7] = "";
            dt.Rows[dt.Rows.Count - 1][8] = "";
            dt.Rows[dt.Rows.Count - 1][9] = "";
            //dt.Rows[dt.Rows.Count - 1][10] = "";
            //dt.Rows[dt.Rows.Count - 1][11] = "";
            //dt.Rows[dt.Rows.Count - 1][12] = "";
            //dt.Rows[dt.Rows.Count - 1][13] = "";
           
        }
        GrdContractRevenue.AllowSorting = false;
        GrdContractRevenue.DataSource = dt;
        GrdContractRevenue.DataBind();
        GrdContractRevenue.SelectedIndex = -1;
    }
    //Bind Inspection Vessel
    protected void BindVessel()
    {
        try
        {
            DataTable dt;

                if (chk_Inact.Checked)
                    dt = Common.Execute_Procedures_Select_ByQuery("select VESSELNAME,VESSELID FROM dbo.vessel with(nolock) where  VesselId in (Select VesselId from UserVesselRelation with(nolock) where Loginid = " + Convert.ToInt32(Session["loginid"].ToString()) + ") order by vesselname");
                else
                    dt = Common.Execute_Procedures_Select_ByQuery("select VESSELNAME,VESSELID FROM dbo.vessel with(nolock) where  VesselStatusid<>2 and VesselId in (Select VesselId from UserVesselRelation with(nolock) where Loginid = " + Convert.ToInt32(Session["loginid"].ToString()) + ") order by vesselname");
            

            ddVessel.Controls.Clear();
            this.ddVessel.DataTextField = "VesselName";
            this.ddVessel.DataValueField = "VesselId";
            this.ddVessel.DataSource = dt;
            this.ddVessel.DataBind();
            this.ddVessel.Items.Insert(0, new ListItem("All", "0"));
            this.ddVessel.Items[0].Value = "0";
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    
    
    //function check value is numeric or not
    public bool IsNumeric(string s)
    {
        try
        {
            Convert.ToInt32(s);
            return true;
        }
        catch (FormatException)
        {
            return false;
        }
    }
    //method for searching data
    protected void SearchData(String sort)
    {
        Session["Sort_Exp"] = sort;
        int ContractId = 0;
        int Vessel = 0;
        string FromDate = txt_fromdt.Text;
        string Todate = txt_todt.Text;       
        int charter = 0;
        string VoyageNo = ""; 
        try
        {
           
            
            if (ddVessel.SelectedIndex != 0)
            {
                Vessel = int.Parse(ddVessel.SelectedValue);
            }
            Session["vesselId"] = ddVessel.SelectedValue;
            if (! string.IsNullOrWhiteSpace(txtContractId.Text))
            {
                ContractId = int.Parse(txtContractId.Text);
            }
            Session[ContractId] = txtContractId.Text;
            if (ddlCharter.SelectedIndex != 0)
            {
                charter = int.Parse(ddlCharter.SelectedValue);
            }
            Session["charter"] = ddlCharter.SelectedValue;
            if (!string.IsNullOrWhiteSpace(txtVoyage.Text))
            {
                VoyageNo = txtVoyage.Text;
            }
            Session[VoyageNo] = txtVoyage.Text;
            //---------------------------------



            if (txt_fromdt.Text != "")
            {
                Session["FromDate"] = txt_fromdt.Text;
            }
            else
            {
                Session["FromDate"] = "";
            }
            if (txt_todt.Text != "")
            {
                Session["Todate"] = txt_todt.Text;
            }
            else
            {
                Session["Todate"] = "";
            }


            //DataTable Dt = null;
            string sql = "Select VC.ContractId, vc.VesselId,v.VesselName , vc.ContractTypeId, CT.ContractType,  case when vc.ContractTypeId = 1 then convert(varchar,cast(VC.ContractAmount as money),1) Else convert(varchar,cast(VC.TotalHireAmtVoyage as money),1) END As HireAmount , vc.ContractStatus, Case when  vc.ContractStatus = 1 then 'Open' Else 'Closed' END As Status, convert(varchar,cast(ISNULL(RVCEE.ExpectedExpenses,0) + ISNULL(VC.AddCOMAmout,0) as money),1) As ExpectedExpenses, v.VesselCode +'-'+CAST(Year(VC.CreatedOn) As varchar(4))+'-'+replace(str(ISNULL(convert(int,right(VC.ContractId,3)),0),3),' ','0') as ContractNo, convert(varchar,cast(case when vc.ContractTypeId = 1 then ISNULL(VC.ContractAmount,0) ELSE ISNULL(VC.TotalHireAmtVoyage,0) END - ISNULL(RVCEE.ExpectedExpenses,0) - ISNULL(VC.AddCOMAmout,0) as money),1)  As TotalExpRevenue, convert(varchar,cast(ISNULL(RVCED.ActualExpenses,0) as money),1)  As ActualExpenses, convert(varchar,cast(case when vc.ContractTypeId = 1 then ISNULL(VC.ContractAmount,0) ELSE ISNULL(VC.TotalHireAmtVoyage,0) END - ISNULL(RVCED.ActualExpenses,0) - ISNULL(RVohd.OffhireAmount,0) as money),1) As TotalActualRevenue , convert(varchar,cast(ISNULL(RVohd.OffhireAmount,0) as money),1)  As OffhireAmount from RV_VesselContractDetails VC with(nolock) Inner Join Vessel v with(nolock) on vc.VesselId = v.VesselId Inner Join RV_ContractTypeMaster CT with(nolock) on VC.ContractTypeId = CT.ContractTypeId LEFT OUTER JOIN (Select RVCEE_ContractId, SUM(RVCEE_Amount) As ExpectedExpenses from RV_VesselContractExpectedExpensesDetails Group by RVCEE_ContractId) As RVCEE on VC.ContractId = RVCEE.RVCEE_ContractId LEFT OUTER JOIN (Select ContractId, SUM(Amount) As ActualExpenses from RV_VesselContractExpensesDetails Group by ContractId) As RVCED on VC.ContractId = RVCED.ContractId LEFT OUTER JOIN (Select ContractId, SUM(OffHireAmount) As OffhireAmount from RV_VesselContractOffHireDetails Group by ContractId) As RVohd on VC.ContractId = RVohd.ContractId   ";

            string WhereClause = "";
            if (ddVessel.SelectedValue != "0")
            {
                if (WhereClause != "")
                {
                    WhereClause = WhereClause + " and vc.VesselId='" + Convert.ToInt32(ddVessel.SelectedValue) + "'";
                }
                else
                {
                    WhereClause = " where vc.VesselId='" + Convert.ToInt32(ddVessel.SelectedValue) + "'";
                }
            }

            if (ddlCharter.SelectedValue != "0")
            {
                if (WhereClause != "")
                {
                    WhereClause = WhereClause + " and VC.ContractTypeId='" + Convert.ToInt32(ddlCharter.SelectedValue) + "'";
                }
                else
                {
                    WhereClause = " where VC.ContractTypeId='" + Convert.ToInt32(ddlCharter.SelectedValue) + "'";
                }
            }

            if (ddlStatus.SelectedValue != "0")
            {
                if (WhereClause != "")
                {
                    WhereClause = WhereClause + " and vc.ContractStatus='" + Convert.ToInt32(ddlStatus.SelectedValue) + "'";
                }
                else
                {
                    WhereClause = " where vc.ContractStatus='" + Convert.ToInt32(ddlStatus.SelectedValue) + "'";
                }
            }

            if (! string.IsNullOrWhiteSpace(txtContractId.Text))
            {
                if (WhereClause != "")
                {
                    WhereClause = WhereClause + " and VC.ContractId=" + Convert.ToInt32(txtContractId.Text) + "";
                }
                else
                {
                    WhereClause = " where VC.ContractId=" + Convert.ToInt32(txtContractId.Text) + "";
                }
            }

            if (!string.IsNullOrWhiteSpace(txtVoyage.Text))
            {
                if (WhereClause != "")
                {
                    WhereClause = WhereClause + " and VC.ContractId in (Select RCVD_ContractId from RV_ContractVoyageDetails with(nolock) where RCVD_IsActive = 0 and RCVD_VoyageNo Like '" + txtVoyage.Text.Trim() + "%')" ;
                }
                else
                {
                    WhereClause = " where VC.ContractId in (Select RCVD_ContractId from RV_ContractVoyageDetails with(nolock) where RCVD_IsActive = 0 and RCVD_VoyageNo Like '" + txtVoyage.Text.Trim() + "%')";
                }
            }


            sql = sql + WhereClause;
            sql = sql + "  order by VC.ContractId desc";

            DataTable Dt = Common.Execute_Procedures_Select_ByQuery(sql);

            if (Dt.Rows.Count == 0)
            {
                lblrecord.Text = "Total Records Found: " + Dt.Rows.Count.ToString();
                lblmessage.Text = "No Records Found";
                GrdContractRevenue.DataSource = null;
                GrdContractRevenue.DataBind();
                GrdContractRevenue.Visible = false;
                //bindgrid();
                return;
            }


            lblrecord.Text = "Total Records Found: " + Dt.Rows.Count.ToString();
            GrdContractRevenue.AllowSorting = true;
            if (Session["SortExp"] != null)
            {
                if ((sort != null) || (Session["Sort_Exp"] != null))
                {
                    if (intTemp == 0)
                    {
                        if (Session["SortExp"].ToString() == "ASC")
                        {
                            Dt.DefaultView.Sort = sort;
                            Session["SortExp"] = "DESC";
                        }
                        else
                        {
                            if (sort == "AA,BB")
                            {
                                char[] c = { ',' };
                                Array a = sort.Split(c);
                                for (int l = 0; l < a.Length; l++)
                                {
                                    if (l == 0)
                                        Dt.DefaultView.Sort = a.GetValue(0).ToString() + " " + "DESC";
                                    else
                                        Dt.DefaultView.Sort = Dt.DefaultView.Sort + "," + a.GetValue(1).ToString() + " " + "DESC";
                                }
                            }
                            else
                            {
                                Dt.DefaultView.Sort = sort + " " + "DESC";
                            }
                            Session["SortExp"] = "ASC";
                        }
                    }
                    else
                    {
                        if (Session["SortExp"].ToString() == "ASC")
                        {
                            if (sort == "AA,BB")
                            {
                                char[] c = { ',' };
                                Array a = sort.Split(c);
                                for (int l = 0; l < a.Length; l++)
                                {
                                    if (l == 0)
                                        Dt.DefaultView.Sort = a.GetValue(0).ToString() + " " + "DESC";
                                    else
                                        Dt.DefaultView.Sort = Dt.DefaultView.Sort + "," + a.GetValue(1).ToString() + " " + "DESC";
                                }
                            }
                            else
                            {
                                Dt.DefaultView.Sort = sort + " " + "DESC";
                            }
                            Session["SortExp"] = "ASC";
                        }
                        else
                        {
                            Dt.DefaultView.Sort = sort;
                            Session["SortExp"] = "DESC";
                        }
                    }
                }
            }

            Session["SearchPrint"] = Dt;  
            GrdContractRevenue.DataSource = Dt;
            GrdContractRevenue.DataBind();

            GrdContractRevenue.Visible = true;
            txt_fromdt.Text = FromDate;
            txt_todt.Text = Todate;
        }
        catch (Exception ex)
        {
            lblrecord.Text = "";
            lblmessage.Text = ex.StackTrace.ToString();
        }
    }
    
    
    #endregion

    #region "Events"
    
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState.Add("Sort_Flag", 0);
            SearchData(GrdContractRevenue.Attributes["MySort"]);
        }
        catch (Exception ex)
        {

        }    
    }
    

    protected void btnClear_Click(object sender, EventArgs e)
    {

        ddVessel.SelectedIndex = 0;
        txtContractId.Text = "";
        txtVoyage.Text = "";
        txt_fromdt.Text = "";
        txt_todt.Text = "";
        ddlCharter.SelectedIndex = 0;
        Session["ContractId"] = null;
        hid1.Text = "Init";
        GrdContractRevenue.DataSource = null;
        GrdContractRevenue.DataBind();
    }
    protected void btnNewContract_Click(object sender, EventArgs e)
    {
        if (hid1.Text == "Init")
        {
            Session["ContractId"] = null;
            //Session["DueMode"] = null; 
            Session["Mode"] = "Add";
             //Response.Redirect("InspectionPlanning.aspx");
            //ScriptManager.RegisterStartupScript(Page, this.GetType(), "PP", "window.open('AddEditInspection.aspx');", true);
            Response.Redirect("~/Modules/REVENUE/AddEditVesselContract.aspx");
        }
        
    }
    protected void lnk_Click(object sender, EventArgs e)
    {
        Session["ContractId"] = hid.Text;
        //Session["DueMode"] = "ShowFull";
    }
    #endregion

    #region "GridEvent"
    

    
    
   
   
    protected void GrdContractRevenue_Sorted(object sender, EventArgs e)
    {

    }
    protected void GrdContractRevenue_Sorting(object sender, GridViewSortEventArgs e)
    {
        SearchData(e.SortExpression);
    }
    
    protected void GrdContractRevenue_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            Alerts.HANDLE_SEARCH_GRID(GrdContractRevenue, Auth);
        }
        catch
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Login.aspx';", true);
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (Session["Contract_Id"] != null)
            {
                //RadioButton rdb = new RadioButton();
                Label lbl = new Label();
                lbl = (Label)e.Row.FindControl("lblid");
               // rdb = (RadioButton)e.Row.FindControl("rd_select");
                HiddenField hfdfld = (HiddenField)e.Row.FindControl("hfdid");
                if (Session["Contract_Id"].ToString() == lbl.Text)
                {
                   // rdb.Checked = true;
                    hid1.Text = hfdfld.Value;
                    //Session["DueMode"] = "ShowFull";
                }
            }            
            //if (DataBinder.Eval(e.Row.DataItem, "StatusColor").ToString() != "")
            //{
            //    e.Row.BackColor = System.Drawing.Color.FromName("#" + DataBinder.Eval(e.Row.DataItem, "StatusColor").ToString());
            //}
        }
    }
    #endregion
    
    
    protected void GrdContractRevenue_RowCreated(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.Pager)
        //{
        //    //TableCell tb = new TableCell();
        //    //tb.HorizontalAlign = HorizontalAlign.Left;
        //    //tb.VerticalAlign = VerticalAlign.Middle;
        //    //tb.Text = "&nbsp;&nbsp;";
        //    //tb.Controls.Add(lblrecord);
        //    //TableCell tb1 = new TableCell();
        //    //tb1.VerticalAlign = VerticalAlign.Middle;  
        //    //tb1.HorizontalAlign = HorizontalAlign.Right;
        //    //tb1.Controls.Add(btnPrint);
        //    ////e.Row.Cells[0].Width = new Unit(100);
        //    //e.Row.Controls[0].Controls[0].Controls[0].Controls.AddAt(0, tb);
        //    //e.Row.Controls[0].Controls[0].Controls[0].Controls.AddAt(e.Row.Controls[0].Controls[0].Controls[0].Controls.Count, tb1);

        //    if (e.Row.Cells[0].Controls[0] != null)
        //    {
        //        Table tbl = (Table)e.Row.Cells[0].Controls[0];
        //        TableCell tblcell = new TableCell();
        //        TableCell tblcell1 = new TableCell();
        //        TableCell tblcell2 = new TableCell();
        //        //tblcell.Width = new Unit(50, UnitType.Percentage); 
        //        tblcell.HorizontalAlign = HorizontalAlign.Center;
        //        //tblcell.Text = "You are viewing page " + Convert.ToInt32(GrdInspection.PageIndex + 1) + " of " + GrdInspection.PageCount + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
        //        tblcell.Controls.Add(lblrecord);
        //        tblcell2.Text = "&nbsp;";
        //        tblcell1.Controls.Add(btnPrint);
                
        //        tbl.Rows[0].Cells.AddAt(0, tblcell);
        //        tbl.Rows[0].Cells.AddAt(tbl.Rows[0].Cells.Count, tblcell2);
        //        tbl.Rows[0].Cells.AddAt(tbl.Rows[0].Cells.Count, tblcell1);
        //        tbl.Rows[0].Cells[0].Width = new Unit(25, UnitType.Percentage);
        //        tbl.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Left;
        //        //tbl.HorizontalAlign = HorizontalAlign.Left; 
        //        if (tbl.Rows[0].Cells.Count > 1)
        //        {
        //            tbl.Rows[0].Cells[1].Width = new Unit(50, UnitType.Percentage);
        //            tbl.Rows[0].Cells[1].HorizontalAlign = HorizontalAlign.Right;
        //        }
        //        //tbl.Rows[0].Cells[tbl.Rows[0].Cells.Count-1].Width = new Unit(50);
        //        //tbl.Rows[0].Cells[tbl.Rows[0].Cells.Count - 1].Text = "&nbsp;";
        //        tbl.Rows[0].Cells[tbl.Rows[0].Cells.Count - 1].Width = new Unit(25, UnitType.Percentage);
        //        tbl.Rows[0].Cells[tbl.Rows[0].Cells.Count-1].HorizontalAlign = HorizontalAlign.Right;

        //    }
        //    //e.Row.Controls.AddAt(e.Row.Controls.Count - 1, tb1);
        //    //e.Row.Controls.AddAt(0, tb);
        //    //e.Row.Controls.AddAt(e.Row.Controls.Count - 1, btnPrint);
        //}
    }
    
    
   
    protected void chk_Inact_OnCheckedChanged(object sender, EventArgs e)
    {
        BindVessel();
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "tr", "SetLastFocus('d1');", true);
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "tr1", "SetLastFocus('d2');", true);
    }
    protected void GrdContractRevenue_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            Session["PgIndex"] = null;
            GrdContractRevenue.PageIndex = e.NewPageIndex;
            Session.Add("PgIndex", GrdContractRevenue.PageIndex);
           
                if (Session["Sort_Exp"] != null)
                {
                    intTemp = 1;
                    SearchData(Session["Sort_Exp"].ToString());
                }
                else
                    SearchData(GrdContractRevenue.Attributes["MySort"]);
           
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void GrdContractRevenue_SelectedIndexChanged(object sender, EventArgs e)
    {
        Label lblContractId = (Label)GrdContractRevenue.Rows[GrdContractRevenue.SelectedIndex].FindControl("lblid");
        Session["Mode"] = "View";
        Session["ContractId"] = lblContractId.Text.ToString();
        Response.Redirect("~/Modules/REVENUE/AddEditVesselContract.aspx");
    }

    protected void txt_todt_TextChanged(object sender, EventArgs e)
    {
        if (txt_fromdt.Text != "")
        {
            if (txt_todt.Text != "")
            {
                if (DateTime.Parse(txt_todt.Text) < DateTime.Parse(txt_fromdt.Text))
                {
                    lblmessage.Text = "From Date cannot be less than To Date.";
                    txt_fromdt.Focus();
                    return;
                }
            }
        }
    }



    protected void GrdContractRevenue_PreRender(object sender, EventArgs e)
    {

    }

    protected void Row_Editing(object sender, GridViewEditEventArgs e)
    {
        Label lblContractId;
        lblContractId = (Label)GrdContractRevenue.Rows[e.NewEditIndex].FindControl("lblid");
        Session["Mode"] = "Edit";
        Session["ContractId"] = lblContractId.Text.ToString();
        
        Response.Redirect("~/Modules/REVENUE/AddEditVesselContract.aspx");
    }

    protected void lnkbtnAddEditVoyage_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        Label lblid = (Label)btn.Parent.FindControl("lblid");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Print", "openAddEditVoyage("+ Convert.ToInt32(lblid.Text) + ");", true);

        
    }



    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            btnsearch_Click(sender, e);
        }
        catch(Exception ex)
        {

        }
    }
}