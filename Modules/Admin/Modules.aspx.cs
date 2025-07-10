using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using Microsoft.ApplicationBlocks.Data;

public partial class Modules : System.Web.UI.Page
{
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



    private void BindGridView(GridView gv, DataTable dt)
    {
        gvGeneric = gv;
        if (dt != null)
        {
            if (dt.Rows.Count > 0)
            {

              
                //gvMenuList.UseAccessibleHeader = true;

                //gvMenuList.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        gvGeneric.DataSource = dt;
        gvGeneric.DataBind();
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
                HeaderCell.ColumnSpan = gvGeneric.Columns.Count - 2;
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
            sortImage.ImageUrl = "~/images/sort-ascend.png";

        }
        else
        {
            _sortDirection = "ASC";
            sortImage.ImageUrl = "~/images/sort-descend.png";
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
        if (!IsPostBack)
        {
            ViewState["ModuleScreenEntryMode"] = "INSERT";
             GetPageData();
            SetSortDirection(SortDireaction);
            BindGridView(gvModuleList, DataTableForGridViewSource);

            EntryPanel.Visible = false;
            GridPanel.Visible = true;
        }
    }

    //private void BindGridview()
    // {
    //     eSoftSecutiy.Module mod = new eSoftSecutiy.Module();
    //     this.gvModuleList.SelectedIndex = -1;
    //     this.gvModuleList.DataSource = mod.GetAllModules();
    //     this.gvModuleList.DataBind();
    // }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        gvModuleList.UseAccessibleHeader = true;
        if (gvModuleList.Rows.Count > 0)
        {
            gvModuleList.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }

    protected void btnEditRow_Click(object sender, ImageClickEventArgs e)
    {
        ViewState["ModuleScreenEntryMode"] = "Update";
        energiosSecurity.Module mod = new energiosSecurity.Module();

        System.Data.DataTable dt = new System.Data.DataTable();
        ImageButton imgbtn = (ImageButton)sender;
        GridViewRow SelectedgvRow = (GridViewRow)imgbtn.NamingContainer;

        dt = mod.GetModuleDetails_ByModuleId(Convert.ToInt32(SelectedgvRow.Cells[0].Text));

        PopulateControls(dt.Rows[0]);
        EntryPanel.Visible = true;
        GridPanel.Visible = false;
        txtModuleName.Focus();
    }

    private void PopulateControls(DataRow dtRow)
    {
        ClearControls();

        this.txtModuleID.Text = dtRow["ModuleID"].ToString();
        this.txtModuleName.Text = dtRow["ModuleName"].ToString();

        this.txtShortName.Text = dtRow["ShortName"].ToString();

        if (dtRow["IsActive"].ToString() == "True")
        {
            this.chkIsActive.Checked = true;
        }
        else
        {
            this.chkIsActive.Checked = false;
        }
        this.txtAnlysticIconURL.Text = dtRow["ImageURL"].ToString();
        this.txtPageURL.Text = dtRow["PURL"].ToString();
        this.txtMenuIconURL.Text = dtRow["MenuIcon"].ToString();
        this.txtDisplayOrder.Text = dtRow["DisOrder"].ToString();
    }

    private void ClearControls()
    {
        this.txtModuleID.Text = "";
        this.txtModuleName.Text = "";
        this.txtShortName.Text = "";
        this.chkIsActive.Checked = true;
        this.txtAnlysticIconURL.Text = "";
        this.txtMenuIconURL.Text = "";
        this.txtDisplayOrder.Text = "";
        this.txtPageURL.Text = "";
    }

    private bool ValidateFormEntries()
    {
        energiosSecurity.Module mod = new energiosSecurity.Module();
        //Duplicity Check
        if (mod.IsModuleAlreadyExists(this.txtModuleName.Text.Trim().ToUpper(), ((ViewState["ModuleScreenEntryMode"].ToString() == "INSERT") == true) ? 0 : Convert.ToInt32(this.txtModuleID.Text),txtShortName.Text) == true)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", "ShowMessage('Error','Module Already Exists.')", true);
            return false;
        }
        return true;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (ValidateFormEntries() == false)
        {
            return;
        }

        int result = 0;
        energiosSecurity.Module mod = new energiosSecurity.Module();
        try
        {
            if (ViewState["ModuleScreenEntryMode"].ToString() == "INSERT")
            {
                //Insert record
                SqlParameter[] parameters =
                                            {   new SqlParameter( "@mName"		, this.txtModuleName.Text.Trim().ToUpper()),
                                                new SqlParameter( "@shortname"		, this.txtShortName.Text.Trim()),
                                                new SqlParameter( "@IsActive"		, this.chkIsActive.Checked),
                                                new SqlParameter( "@CreatedBy"		, Session["UserID"]),
                                                new SqlParameter( "@ImageURL"      , this.txtAnlysticIconURL.Text.Trim()),
                                                new SqlParameter( "@PageURL"      , this.txtPageURL.Text.Trim()),
                                                new SqlParameter( "@MenuIconURL"      , this.txtMenuIconURL.Text.Trim()),
                                                new SqlParameter( "@DisOrder"      , this.txtDisplayOrder.Text.Trim())
                                            };
                result = mod.Insert(parameters);
            }
            else
            {
                //Update record
                SqlParameter[] parameters =
                                                {   new SqlParameter( "@mID"		,Convert.ToInt32(this.txtModuleID.Text.Trim().ToUpper())),
                                                    new SqlParameter( "@mName"		, this.txtModuleName.Text.Trim().ToUpper()),
                                                    new SqlParameter( "@shortname"		, this.txtShortName.Text.Trim()),
                                                    new SqlParameter( "@IsActive"		, this.chkIsActive.Checked),
                                                    new SqlParameter( "@ModifiedBy"		, Session["UserID"]),
                                                    new SqlParameter( "@ImageURL"      , this.txtAnlysticIconURL.Text.Trim()),
                                                    new SqlParameter( "@PageURL"      , this.txtPageURL.Text.Trim()),
                                                    new SqlParameter( "@MenuIconURL"      , this.txtMenuIconURL.Text.Trim()),
                                                    new SqlParameter( "@DisOrder"      , this.txtDisplayOrder.Text.Trim())
                                                };
                result = mod.Update(parameters);
            }

            if (result == 1)
            {
                if (ViewState["ModuleScreenEntryMode"].ToString() == "INSERT")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", "ShowMessage('Success',' Module Created Successfully.')", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", "ShowMessage('Success',' Module Updated Successfully.')", true);
                }
                ClearControls();
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

        // ClearControls();


        GetPageData();
        BindGridView(gvModuleList, DataTableForGridViewSource);
        ViewState["ModuleScreenEntryMode"] = "INSERT";
        EntryPanel.Visible = false;
        GridPanel.Visible = true;
       // Response.Redirect("Modules.aspx");
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ViewState["ModuleScreenEntryMode"] = "INSERT";
        ClearControls();

       
        GetPageData();
        BindGridView(gvModuleList, DataTableForGridViewSource);

        EntryPanel.Visible = false;
        GridPanel.Visible = true;
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        EntryPanel.Visible = true;
        GridPanel.Visible = false;
        txtModuleName.Focus();
    }
   
    private void GetPageData()
    {

        energiosSecurity.Module mod = new energiosSecurity.Module();

        DataTable dt = mod.GetAllModules();

        DataTableForGridViewSource = dt;
         
    }
  

    protected void txtSearchKeyWord_TextChanged(object sender, EventArgs e)
    {
         GetPageData();

        FilterList();
        BindGridView(gvModuleList, DataTableForGridViewSource);
        txtSearchKeyWord.Focus();
    }
    private void FilterList()
    {
        string srchcond = " ";
        string srchKeyWord = txtSearchKeyWord.Text.Trim();
        if (txtSearchKeyWord.Text.Trim().Length > 0)
        {
            srchcond = " (ModuleName Like '%" + srchKeyWord + "%' or ShortName Like '%" + srchKeyWord + "%' )";
        }
        DataTableForGridViewSource = AppHelper.FilterDataTable(DataTableForGridViewSource, srchcond);
      
    }

   
}