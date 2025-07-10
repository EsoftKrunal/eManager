using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using Microsoft.ApplicationBlocks.Data;
public partial class MenuMaster :  System.Web.UI.Page
{
    AuthenticationManager Auth;
    #region GridView Sort Filter Paging

    public DataTable DataTableForGridViewSource
    {
        get
        {
            if (ViewState["DataTableForGridView"] == null)
                return new DataTable();
            else
                return (DataTable)ViewState["DataTableForGridView"];
        }
        set
        {
            ViewState["DataTableForGridView"] = value;
        }
    }
    public GridView gvGeneric
    {
        get
        {

            return (GridView)Session["gvGeneric_ItemUnit"];
        }
        set
        {
            Session["gvGeneric_ItemUnit"] = value;
        }
    }
    Image sortImage = new Image();
    private string _sortDirection;
    public string SortDireaction
    {
        get
        {
            if (ViewState["SortDireaction"] == null)
                return string.Empty;
            else
                return ViewState["SortDireaction"].ToString();
        }
        set
        {
            ViewState["SortDireaction"] = value;
        }
    }

    public int Current_PagerPgIndx
    {
        get
        {
            if (ViewState["Current_PagerPgIndx"] == null)
                return 0;
            else
                return Convert.ToInt32(ViewState["Current_PagerPgIndx"]);
        }
        set
        {
            ViewState["Current_PagerPgIndx"] = value;
        }
    }
    public int Last_PagerPgInterval
    {
        get
        {
            if (ViewState["Last_PagerPgInterval"] == null)
                return 0;
            else
                return Convert.ToInt32(ViewState["Last_PagerPgInterval"]);
        }
        set
        {
            ViewState["Last_PagerPgInterval"] = value;
        }
    }


    public void GetPageData()
    {



       
        energiosSecurity.Menu mnu = new energiosSecurity.Menu();

        System.Data.DataTable dt = new System.Data.DataTable();
        dt = mnu.GetAll();
        DataTable dtFirst = new DataTable();
        dtFirst = AppHelper.FilterDataTable(dt, "ParentMenuID is null");
        System.Data.DataTable dtFinal = new System.Data.DataTable();


        dtFinal = dt.Clone();
        foreach (DataRow MainMenuRow in dtFirst.Rows)
        {
            MainMenuRow["MenuName"] = MainMenuRow["ModuleName"].ToString() + " => " + MainMenuRow["MenuName"].ToString();
            dtFinal.ImportRow(MainMenuRow);
            System.Data.DataTable dtChild = new System.Data.DataTable();
            dtChild = ChildMenu(dt, Convert.ToInt32(MainMenuRow["MenuID"].ToString()));
            foreach (DataRow ChildRow in dtChild.Rows)
            {
                ChildRow["MenuName"] = MainMenuRow["MenuName"] + " => " + ChildRow["MenuName"].ToString();
                dtFinal.ImportRow(ChildRow);
            }
        }

        string srchcond = " ";
          string srchKeyWord =  txtSearchKeyWord.Text.Trim();
        if (txtSearchKeyWord.Text.Trim().Length > 0)
        {
            srchcond = " (MenuName Like '%" + srchKeyWord + "%'  OR MenuLocation  Like '%" + srchKeyWord + "%'  )";
        }
        dtFinal = AppHelper.FilterDataTable(dtFinal, srchcond);

        DataTableForGridViewSource = dtFinal;

    }
    private void BindGridView(GridView gv, DataTable dt)
    {
        gvGeneric = gv;
        //if (dt != null)
        //{
        //    if (dt.Rows.Count > 0)
        //    {

                gvGeneric.DataSource = dt;
                gvGeneric.DataBind();
               
                //gvMenuList.UseAccessibleHeader = true;
                //gvMenuList.HeaderRow.TableSection = TableRowSection.TableHeader;
        //    }
         
        //}


    }

    protected void ddlPgSz_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvGeneric.PageSize = Convert.ToInt32(((DropDownList)sender).SelectedValue);
        Last_PagerPgInterval = gvGeneric.PageSize;
        gvGeneric.PageIndex = 0;
        BindGridView(gvGeneric, DataTableForGridViewSource);
    }
    protected void lnlPage_Clicked(object sender, EventArgs e)
    {

        gvGeneric.PageSize = Last_PagerPgInterval;


        gvGeneric.PageIndex = int.Parse((sender as Button).CommandArgument);

        Current_PagerPgIndx = gvGeneric.PageIndex - 1;
        BindGridView(gvGeneric, DataTableForGridViewSource);
    }


    protected void btnNextSet_Clicked(object sender, EventArgs e)
    {



        gvGeneric.PageIndex = gvGeneric.PageIndex + 20;

        BindGridView(gvGeneric, DataTableForGridViewSource);

    }
    //
    protected void btnPreviousSet_Clicked(object sender, EventArgs e)
    {
        if (gvGeneric.PageIndex - 20 < 0)
        {
            gvGeneric.PageIndex = 0;
        }
        else
        {

            gvGeneric.PageIndex = gvGeneric.PageIndex - 20;
        }
        BindGridView(gvGeneric, DataTableForGridViewSource);

    }
    protected void ddl_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (int.Parse(((DropDownList)sender).SelectedValue) == 1)
        {
            gvGeneric.PageIndex = 0;
        }
        else
        {


            gvGeneric.PageIndex = int.Parse(((DropDownList)sender).SelectedValue);
        }
        BindGridView(gvGeneric, DataTableForGridViewSource);

    }


    protected void btnRefereshResltSet_Clicked(object sender, EventArgs e)
    {
        gvGeneric.PageIndex = 0;
        gvGeneric.PageSize = 10;
        GetPageData();

        BindGridView(gvGeneric, DataTableForGridViewSource);
    }
    protected void OnRow_Created(object sender, GridViewRowEventArgs e)
    {
        gvGeneric = (GridView)sender;
        gvGeneric.VirtualItemCount = DataTableForGridViewSource.Rows.Count;

        #region Add Header

        if (e.Row.RowType == DataControlRowType.Header)
        {


            int pgInterval = gvGeneric.PageSize;
            int Init_pgInterval = 10;

            int TotalPageItems = 1;
            if (gvGeneric.VirtualItemCount > Init_pgInterval)
            {
                DropDownList ddlPgSz = new DropDownList();
                ddlPgSz.ID = "ddlPgSz";
                ddlPgSz.AutoPostBack = true;
                ddlPgSz.Items.Clear();
                ddlPgSz.Width = Unit.Pixel(80);
                TotalPageItems = Convert.ToInt32((gvGeneric.VirtualItemCount / Init_pgInterval)); //.ToString("#.###")
                decimal tempval = Convert.ToDecimal((gvGeneric.VirtualItemCount / Init_pgInterval).ToString("#.###"));

                int decimalValue = Convert.ToInt32(Convert.ToDouble(tempval.ToString().Replace(TotalPageItems.ToString().Substring(0, 1) + ".", "")));
                if (decimalValue > 0) TotalPageItems = TotalPageItems + 1;
                for (int i = 1; i <= TotalPageItems; i++)
                {
                    ddlPgSz.Items.Add((i * Init_pgInterval).ToString());
                }
                ddlPgSz.SelectedIndexChanged += new EventHandler(ddlPgSz_SelectedIndexChanged);


                //ddlPgSz.Items.Add(new ListItem("- Page Size -", "10"));
                if (TotalPageItems > 0)
                {
                    if (Last_PagerPgInterval == 0) Last_PagerPgInterval = pgInterval;

                    if (Last_PagerPgInterval >= pgInterval && TotalPageItems >= Last_PagerPgInterval)
                    {
                        ddlPgSz.SelectedIndex = -1;
                        ddlPgSz.Items.FindByValue(Last_PagerPgInterval.ToString()).Selected = true;

                        Last_PagerPgInterval = Convert.ToInt32(ddlPgSz.SelectedValue);
                        gvGeneric.PageSize = Last_PagerPgInterval;
                    }
                    else
                    {
                        ddlPgSz.SelectedIndex = -1;
                        ddlPgSz.Items.FindByValue(pgInterval.ToString()).Selected = true;

                        Last_PagerPgInterval = pgInterval;

                    }

                }




                Label lbl = new Label();

                int FirstRowCurrentPage = ((gvGeneric.PageIndex * gvGeneric.PageSize) + 1);
                int LastRowCurrentPage = 0;
                int LastPageIndicator = gvGeneric.VirtualItemCount % gvGeneric.PageSize;
                LastRowCurrentPage = (gvGeneric.PageCount == gvGeneric.PageIndex + 1) ? (FirstRowCurrentPage + LastPageIndicator - 1) : (FirstRowCurrentPage + gvGeneric.PageSize - 1);
                lbl.Text = String.Format("Record {0} to {1} of {2}", FirstRowCurrentPage, LastRowCurrentPage, gvGeneric.VirtualItemCount);
                lbl.Font.Bold = true;

                GridView HeaderGrid = (GridView)sender;
                GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

                TableHeaderCell HeaderCell = new TableHeaderCell();
                //It is important to say that if you want your cells to be rendered as <th> reather than <td> you should use a TableHeaderCell instead of TableCell
                Button btnRefershList = new Button() { Text = "Refresh", ID = "btnRefereshResltSet" };

                HeaderCell.Controls.Add(btnRefershList);
                btnRefershList.Click += new EventHandler(btnRefereshResltSet_Clicked);
                HeaderCell.ColumnSpan = 1;
                HeaderCell.HorizontalAlign = HorizontalAlign.Left;
                HeaderGridRow.Cells.Add(HeaderCell);

                HeaderCell.BackColor = System.Drawing.Color.Transparent;
                HeaderCell.BorderColor = System.Drawing.Color.Transparent;
                HeaderGridRow.TableSection = TableRowSection.TableHeader;

                HeaderCell = new TableHeaderCell();
                HeaderCell.Controls.Add(lbl);
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell.BackColor = System.Drawing.Color.Transparent;
                HeaderCell.BorderColor = System.Drawing.Color.Transparent;
                HeaderCell.ColumnSpan = 4 ;
                HeaderGridRow.Cells.Add(HeaderCell);
                HeaderGridRow.TableSection = TableRowSection.TableHeader;



                HeaderCell = new TableHeaderCell();
                HeaderCell.Controls.Add(new Literal() { Text = "Page Size : <br />" });
                HeaderCell.Controls.Add(ddlPgSz);
                HeaderCell.ColumnSpan = 1;
                HeaderCell.BackColor = System.Drawing.Color.Transparent;
                HeaderCell.BorderColor = System.Drawing.Color.Transparent;
                HeaderCell.HorizontalAlign = HorizontalAlign.Right;

                HeaderGridRow.Cells.Add(HeaderCell);

                gvGeneric.UseAccessibleHeader = true;
                HeaderGridRow.TableSection = TableRowSection.TableHeader;



                HeaderGridRow.BorderStyle = BorderStyle.None;
                HeaderGridRow.BorderWidth = Unit.Pixel(0);
                gvGeneric.Controls[0].Controls.AddAt(0, HeaderGridRow);



            }

        }
        #endregion
        #region AddPager


        if (e.Row.RowType == DataControlRowType.Pager)
        {
            if (e.Row.Cells.GetCellIndex(e.Row.Cells[0]) == 0)
            {


                e.Row.Cells[0].Controls.Clear();

                System.Web.UI.WebControls.Table tbl = new System.Web.UI.WebControls.Table();
                System.Web.UI.WebControls.TableRow tblRow = new System.Web.UI.WebControls.TableRow();
                tblRow.BackColor = System.Drawing.Color.Transparent;
                tblRow.BorderColor = System.Drawing.Color.Transparent;
                System.Web.UI.WebControls.TableCell tblCell;





                //int MaxbuttonIndex = startPgIndx + record_slotCount;




                double dblPageCount = (double)((decimal)gvGeneric.VirtualItemCount / gvGeneric.PageSize);
                int pageCount = (int)Math.Ceiling(dblPageCount);


                Panel PgNumberContainer = new Panel();

                LinkButton lnkPageNumber = new LinkButton();
                PgNumberContainer.Controls.Add(lnkPageNumber);


                PgNumberContainer.Controls.Clear();
                int maxcounter = pageCount;
                if (pageCount > 20) maxcounter = 20;


                PgNumberContainer.Controls.Add(new Button() { Text = "First", CommandArgument = "0", Enabled = (gvGeneric.PageIndex > 0), ID = "lnkFirstButton" });

                ((Button)(PgNumberContainer.Controls[PgNumberContainer.Controls.Count - 1])).Click += new EventHandler(lnlPage_Clicked);


                PgNumberContainer.Controls.Add(new Button() { Text = "Previous", ID = "lnkPreviousButton", CommandArgument = (gvGeneric.PageIndex - 1).ToString(), Enabled = gvGeneric.PageIndex > 0 });
                ((Button)(PgNumberContainer.Controls[PgNumberContainer.Controls.Count - 1])).Click += new EventHandler(lnlPage_Clicked);


                for (int i = 1; i <= maxcounter; i++)
                {

                    PgNumberContainer.Controls.Add(new Button() { Text = (i).ToString(), CommandArgument = (i - 1).ToString(), Enabled = (i - 1 != gvGeneric.PageIndex), ID = "lnkButton_" + i.ToString() + "" });
                    ((Button)(PgNumberContainer.Controls[PgNumberContainer.Controls.Count - 1])).Click += new EventHandler(lnlPage_Clicked);

                }



                PgNumberContainer.Controls.Add(new Button() { Text = "Next", ID = "lnkNextButton", CommandArgument = (gvGeneric.PageIndex + 1).ToString(), Enabled = (gvGeneric.PageCount - 1 > gvGeneric.PageIndex) });
                ((Button)(PgNumberContainer.Controls[PgNumberContainer.Controls.Count - 1])).Click += new EventHandler(lnlPage_Clicked);


                PgNumberContainer.Controls.Add(new Button() { Text = "Last", CommandArgument = (gvGeneric.PageCount).ToString(), Enabled = (gvGeneric.PageCount - 1 != gvGeneric.PageIndex), ID = "lnkLastButton" });
                ((Button)(PgNumberContainer.Controls[PgNumberContainer.Controls.Count - 1])).Click += new EventHandler(lnlPage_Clicked);

                tblCell = new System.Web.UI.WebControls.TableCell();
                tblCell.Controls.Add(PgNumberContainer);
                tblCell.HorizontalAlign = HorizontalAlign.Left;
                tblCell.BackColor = System.Drawing.Color.Transparent;
                tblCell.BorderColor = System.Drawing.Color.Transparent;
                tblRow.Controls.Add(tblCell);
                tbl.Controls.Add(tblRow);




                //tblCell = new System.Web.UI.WebControls.TableCell();
                //tblCell.HorizontalAlign = HorizontalAlign.Right;
                //tblCell.BackColor = System.Drawing.Color.Transparent;
                //tblCell.BorderColor = System.Drawing.Color.Transparent;
                //tblCell.Controls.Add(ddlPgSz);
                //tblRow.Controls.Add(tblCell);
                //tbl.Controls.Add(tblRow);


                //tblCell = new System.Web.UI.WebControls.TableCell();
                //tblCell.HorizontalAlign = HorizontalAlign.Right;
                //tblCell.Controls.Add(lbl);
                //tblCell.BackColor = System.Drawing.Color.Transparent;
                //tblCell.BorderColor = System.Drawing.Color.Transparent;
                //tblRow.Controls.Add(tblCell);
                //tbl.Controls.Add(tblRow);

                tbl.Width = Unit.Percentage(100);
                tbl.CellPadding = 0;
                tbl.CellSpacing = 0;
                tbl.BorderStyle = BorderStyle.None;
                tbl.BorderWidth = Unit.Pixel(0);


                e.Row.Controls[0].Controls.Add(tbl);




            }
        }
        #endregion
    }
    protected void OnPageIndex_Changing(object sender, GridViewPageEventArgs e)
    {
        gvGeneric = (GridView)sender;
        gvGeneric.PageIndex = e.NewPageIndex;
        Current_PagerPgIndx = gvGeneric.PageIndex - 1;
        BindGridView(gvGeneric, DataTableForGridViewSource);
    }
    #region Sorting
    protected void SetSortDirection(string sortDirection)
    {
        if (sortDirection == "ASC")
        {
            _sortDirection = "DESC";
          //  sortImage.ImageUrl = "~/images/sort-ascend.png";
            sortImage.ImageUrl = "~/images/sort-descend.png";

        }
        else
        {
            _sortDirection = "ASC";
            //sortImage.ImageUrl = "~/images/sort-descend.png";
            sortImage.ImageUrl = "~/images/sort-ascend.png";
        }
    }
    protected void OnGrid_Sorting(object sender, GridViewSortEventArgs e)
    {

        gvGeneric = (GridView)sender;
        SetSortDirection(SortDireaction);


        DataTableForGridViewSource.DefaultView.Sort = e.SortExpression + " " + _sortDirection;
        gvGeneric.PageIndex = 0;
        BindGridView(gvGeneric, DataTableForGridViewSource);

        SortDireaction = _sortDirection;
        int columnIndex = 0;
        foreach (DataControlFieldHeaderCell headerCell in gvGeneric.HeaderRow.Cells)
        {
            if (headerCell.ContainingField.SortExpression == e.SortExpression)
            {
                columnIndex = gvGeneric.HeaderRow.Cells.GetCellIndex(headerCell);
            }
        }

        gvGeneric.HeaderRow.Cells[columnIndex].Controls.Add(sortImage);
    }

    #endregion

    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {

        Auth = new AuthenticationManager(145, Convert.ToInt32(Session["loginid"].ToString()), ObjectType.Page);

        if (!IsPostBack)
        {
           // SortExpr = "MenuID" ;
          
           
           GetPageData();

           BindGridView(gvMenuList, DataTableForGridViewSource);
            ViewState["FormEntryMode"] = "INSERT";
           
            BindModules();
            EntryPanel.Visible = false;
            GridPanel.Visible = true;

        }

        if (ViewState["FormEntryMode"].ToString().ToUpper() == "INSERT")
        {
            if (Auth.IsAdd) { btnSubmit.Visible = true; }
            else { btnSubmit.Visible = false; }
        }


    }



    protected void Page_PreRender(object sender, EventArgs e)
    {

        gvMenuList.UseAccessibleHeader = true;
        if (gvMenuList.Rows.Count > 0)
            gvMenuList.HeaderRow.TableSection = TableRowSection.TableHeader;


    }

    private void BindModules()
    {
        energiosSecurity.Module mod = new energiosSecurity.Module();
        ddlModules.SelectedIndex = -1;
        ddlModules.Items.Clear();
        ddlModules.DataSource = mod.GetAllModules();
        ddlModules.DataTextField = "ModuleName";
        ddlModules.DataValueField = "ModuleID";
        ddlModules.DataBind();
        ListItem lst = new ListItem();
        lst.Text = "< Select Module >";
        lst.Value = "-1";
        ddlModules.Items.Insert(0, lst);

    }
    private void BindPages(int modid)
    {
        energiosSecurity.Pages pgs = new energiosSecurity.Pages();
        ddlPages.SelectedIndex = -1;
        ddlPages.Items.Clear();
       // ddlPages.DataSource = pgs.GetPagesByModuleID(modid);
        ddlPages.DataSource = pgs.GetAll();

        ddlPages.DataTextField = "PageUrl";
        ddlPages.DataValueField = "PageID";
        ddlPages.DataBind();
        ListItem lst = new ListItem();
        lst.Text = "< Select Page >";
        lst.Value = "-1";
        ddlPages.Items.Insert(0, lst);

    }
    private void PopulateControls(DataRow dtRow)
    {
        ClearControls();


        ClearControls();
        EventArgs e = new EventArgs();

        this.txtMenuID.Text = dtRow["MenuID"].ToString();
        this.txtMenuName.Text = dtRow["MenuName"].ToString();


        this.txtParentMenuID.Text = dtRow["ParentMenuID"].ToString();
        this.txtParentMenu.Text = dtRow["ParentMenu"].ToString();
        this.txtSortOrder.Text = dtRow["DisplayOrder"].ToString();
        if (dtRow["IsActive"].ToString() == "True")
        {
            this.chkIsActive.Checked = true;
        }
        else
        {
            this.chkIsActive.Checked = false;
        }
        if (ddlModules.Items.FindByValue(dtRow["ModuleID"].ToString()) != null)
        {
            ddlModules.Items.FindByValue(dtRow["ModuleID"].ToString()).Selected = true;
            ddlModules_SelectedIndexChanged(ddlModules, e);
        }
        if (ddlPages.Items.FindByValue(dtRow["PageID"].ToString()) != null)
        {
            ddlPages.Items.FindByValue(dtRow["PageID"].ToString()).Selected = true;
        }



       
        EntryPanel.Visible = true;
        GridPanel.Visible = false;
    }
    protected void txtParentMenu_TextChanged(object sender, EventArgs e)
    {
        int seprator = txtParentMenu.Text.Trim().IndexOf('-');
        int mid = Convert.ToInt32(txtParentMenu.Text.Trim().Substring(0, seprator));
        txtParentMenuID.Text = mid.ToString();
        txtParentMenu.Text = txtParentMenu.Text.Trim().Substring(seprator + 2, txtParentMenu.Text.Length - (seprator + 2));
        GetNewSortOrder();
    }
    protected void ddlModules_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        //dt = GetPageData();

        if (ddlModules.SelectedValue.ToString() != "-1")
        {
            BindPages(Convert.ToInt32(ddlModules.SelectedValue.ToString()));
            DataView dv1 = DataTableForGridViewSource.DefaultView;

            dv1.RowFilter = " ModuleID=" + ddlModules.SelectedValue.ToString();
            DataTable dtNew = dv1.ToTable();

            this.gvMenuList.DataSource = dtNew;
        }
        else
        {
            this.gvMenuList.DataSource = dt;
        }
        this.gvMenuList.SelectedIndex = -1;

        this.gvMenuList.DataBind();
        GetNewSortOrder();
    }
    private void ClearControls()
    {
        this.txtMenuID.Text = "";
        this.txtMenuName.Text = "";

        this.txtParentMenu.Text = "";
        this.txtParentMenuID.Text = "";
        this.txtSortOrder.Text = "";
        this.chkIsActive.Checked = true;
        this.ddlModules.SelectedIndex = -1;
        this.ddlPages.SelectedIndex = -1;
        this.chkIsActive.Checked = true;

    }
    private bool ValidateFormEntries()
    {
        //eSoftSecutiy.Role rol = new eSoftSecutiy.Role();
        ////Duplicity Check
        //if (rol.IsAlreadyExists(this.txtRoleName.Text.Trim().ToUpper(), ((ViewState["RoleScreenEntryMode"].ToString() == "INSERT") == true) ? 0 : Convert.ToInt32(this.txtRoleID.Text)) == true)
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", "ShowMessage('Error','Role Already Exists.')", true);
        //    return false;
        //}

        return true;
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        if (ValidateFormEntries() == false)
        {
            return;
        }
        if (ddlPages.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", "ShowMessage('Error','Select Page URL')", true);
            return;
        }

        energiosSecurity.Menu mnu = new energiosSecurity.Menu();
        int result = 0;
        try
        {

            if (ViewState["FormEntryMode"].ToString() == "INSERT")
            {
                if (txtMenuID.Text == "")
                {
                    txtMenuID.Text = "0";
                }
                //Insert record
                if (this.txtParentMenuID.Text.Trim() == "") this.txtParentMenuID.Text = "0";
                SqlParameter[] parameters =
            {   new SqlParameter( "@PageName"		, this.txtMenuName.Text.Trim().ToUpper()   ) ,
                new SqlParameter( "@mUrl"		, this.ddlPages.SelectedItem.Text  ),
                new SqlParameter( "@ModuleID"		, this.ddlModules.SelectedValue.ToString().ToUpper()   ) ,
                new SqlParameter( "@pid"		, (Convert.ToInt32(this.txtParentMenuID.Text.Trim() ) <=0 )?null:this.txtParentMenuID.Text.Trim()    ),
                new SqlParameter( "@SortOrder"		, this.txtSortOrder.Text.Trim().ToUpper()   ),
                new SqlParameter( "@IsActive"		, this.chkIsActive.Checked ),
               // new SqlParameter( "@pageid"		, this.txtMenuID.Text   ),
                new SqlParameter( "@CreatedBy"		, Session["UserID"]   )
                
            };

                result = mnu.Insert(parameters);
            }
            else
            {

                //Update record
                SqlParameter[] parameters =
            {   new SqlParameter( "@mID"		,Convert.ToInt32(this.txtMenuID.Text.Trim().ToUpper()   ) ),
                new SqlParameter( "@PageName"		, this.txtMenuName.Text.Trim().ToUpper()   ) ,
                new SqlParameter( "@mUrl"		, this.ddlPages.SelectedItem.Text   ), 
                new SqlParameter( "@ModuleID"		, this.ddlModules.SelectedValue.ToString().ToUpper()   ) ,
                 new SqlParameter( "@pid"		, (Convert.ToInt32(this.txtParentMenuID.Text.Trim() ) <=0 )?null:this.txtParentMenuID.Text.Trim()    ),
                new SqlParameter( "@SortOrder"		, this.txtSortOrder.Text.Trim().ToUpper()   ),
                new SqlParameter( "@IsActive"		, this.chkIsActive.Checked ),
                //new SqlParameter( "@PageID"		, this.txtMenuID.Text ),
                new SqlParameter( "@ModifiedBy"		, Session["UserID"]   )
               
            };

                result = mnu.Update(parameters);

            }

            if (result == 1)
            {
                
              
                   GetPageData();
               
               
                if (ViewState["FormEntryMode"].ToString() == "INSERT")
                {
                    AppHelper.SortDataTable(DataTableForGridViewSource, "MenuID DESC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", "ShowMessage('Success',' Menu Created Successfully.')", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", "ShowMessage('Success',' Menu Updated Successfully.')", true);
                }

                BindGridView(gvGeneric, DataTableForGridViewSource);
                ClearControls();

                ViewState["FormEntryMode"] = "INSERT";
                EntryPanel.Visible = false;
                GridPanel.Visible = true;

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", "ShowMessage('Error','Saving Process Failed.')", true);
            }
        }
        catch (Exception ex)
        {

            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", "ShowMessage('Error','" + ex.StackTrace.ToString() + "')", true);

        }

      
    }

    protected void btnEditRow_Click(object sender, ImageClickEventArgs e)
    {
        ViewState["FormEntryMode"] = "Update";
        energiosSecurity.Menu mnu = new energiosSecurity.Menu();

        System.Data.DataTable dt = new System.Data.DataTable();
        ImageButton imgbtn = (ImageButton)sender;
        GridViewRow SelectedgvRow = (GridViewRow)imgbtn.NamingContainer;

        dt = mnu.GetDetails_ByMenuId(Convert.ToInt32(SelectedgvRow.Cells[0].Text));

        PopulateControls(dt.Rows[0]);
        EntryPanel.Visible = true;
        GridPanel.Visible = false;
        txtMenuName.Focus();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ViewState["FormEntryMode"] = "INSERT";
        ClearControls();
       GetPageData();

       BindGridView(gvGeneric, DataTableForGridViewSource);
        if (ViewState["FormEntryMode"].ToString().ToUpper() == "INSERT")
        {
            if (Auth.IsAdd) { btnSubmit.Visible = true; }
            else { btnSubmit.Visible = false; }
        }
        EntryPanel.Visible = false;
        GridPanel.Visible = true;
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        EntryPanel.Visible = true;
        GridPanel.Visible = false;


        

        txtMenuName.Focus();
    }
    
   
     
    private DataTable ChildMenu(DataTable dt, int ParentMenuID)
    {
        DataTable dtNext = new DataTable();
        dtNext = AppHelper.FilterDataTable(dt, "ParentMenuID =" + ParentMenuID.ToString());
        System.Data.DataTable dtFinal = new System.Data.DataTable();
        dtFinal = dt.Clone();
        foreach (DataRow MainMenuRow in dtNext.Rows)
        {
            dtFinal.ImportRow(MainMenuRow);

            System.Data.DataTable dtChild = new System.Data.DataTable();
            dtChild = ChildMenu(dt, Convert.ToInt32(MainMenuRow["MenuID"].ToString()));
            if (dtChild.Rows.Count > 0)
            {

                for (int i = dtChild.Rows.Count - 1; i >= 0; i--)
                {
                    dtChild.Rows[i]["MenuName"] = dtChild.Rows[i]["ParentMenuName"].ToString() + " => " + dtChild.Rows[i]["MenuName"].ToString();
                    dtFinal.ImportRow(dtChild.Rows[i]);
                    dtFinal.AcceptChanges();
                }

            }


        }
        return dtFinal;
    }
    

   

 
    private void GetNewSortOrder()
    {
        if (txtParentMenuID.Text.Trim() == "") txtParentMenuID.Text = 0.ToString();
        if (ViewState["FormEntryMode"].ToString() == "INSERT")
        {
            txtSortOrder.Text = "1";
            if (Convert.ToInt32(ddlModules.SelectedValue) > 0)
            {
                // Get New Sor order based on Selected Module  and Menu Items having no Childs
                string qry = "";
                qry = "select coalesce(max(DisplayOrder),0)+1 from appmstr_Menu where ParentMenuID is null and ModuleID=" + Convert.ToInt32(ddlModules.SelectedValue) + " ";
                int newSortOrderNo = Convert.ToInt32(ESqlHelper.ExecuteScalar(ESqlHelper.connection(), CommandType.Text, qry));

                // Get the New Sort Order based on Selected parent 
                if (Convert.ToInt32(txtParentMenuID.Text) > 0)
                {
                    qry = "select coalesce(max(DisplayOrder),0)+1 from appmstr_Menu where ParentMenuID =" + Convert.ToInt32(txtParentMenuID.Text) + " and ModuleID=" + Convert.ToInt32(ddlModules.SelectedValue) + " ";
                    newSortOrderNo = Convert.ToInt32(ESqlHelper.ExecuteScalar(ESqlHelper.connection(), CommandType.Text, qry));
                }
                txtSortOrder.Text = newSortOrderNo.ToString();
            }
        }
       
      
    }
    protected void txtSearchKeyWord_TextChanged(object sender, EventArgs e)
    {

        GetPageData();
        BindGridView(gvGeneric, DataTableForGridViewSource);
       // txtSearchKeyWord.Focus();
        //if (DataTableForGridViewSource.Rows.Count <= 0)
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", "ShowMessage('Message','No Record Found.')", true);           
        //}
    }

    protected void OnKeywordSearch(object sender, EventArgs e)
    {
        string searchstring = ((TextBox)sender).Text.Trim();
        string srchcond = " (";
        int kk = 0;
        for (int i = 0; i < gvGeneric.Columns.Count; i++)
        {
            DataControlField field = gvGeneric.Columns[i];
            BoundField bfield = field as BoundField;

            if (bfield != null)
            {

                if (kk == 0)
                {
                    srchcond += " " + bfield.DataField.ToString() + " LIKE '%" + searchstring + "%'";
                }
                else
                {
                    srchcond += " OR " + bfield.DataField.ToString() + " LIKE '%" + searchstring + "%'";
                }
                kk = 1;
            }
        }

        srchcond += " )";
        DataTableForGridViewSource = AppHelper.FilterDataTable(DataTableForGridViewSource, srchcond);
        BindGridView(gvGeneric, DataTableForGridViewSource);
        ((TextBox)sender).Focus();
    }  
}
 