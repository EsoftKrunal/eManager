using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using Microsoft.ApplicationBlocks.Data;
public partial class RoleRights : System.Web.UI.Page
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
        if (ddlModules.SelectedValue != "-1")
        {
            gvGeneric = gv;
            DataTable selectedTable = dt.AsEnumerable()
                                  .Where(r => r.Field<string>("ModuleName") != "SETTINGS")
                                  .CopyToDataTable();

            gvGeneric.DataSource = selectedTable;
            gvGeneric.DataBind();
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ddlRoles_SelectedIndexChanged(gv, new EventArgs());
                }
            }
        }
        else
        {
            gvGeneric.DataSource = null;
            gvGeneric.DataBind();
        }

       
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
                HeaderCell.Controls.Add(new Literal() { Text = "Page Size : " });
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
            sortImage.ImageUrl = "~/images/sort-descend.png";

        }
        else
        {
            _sortDirection = "ASC";
            sortImage.ImageUrl = "~/images/sort-ascend.png";
        }
    }
    protected void OnGrid_Sorting(object sender, GridViewSortEventArgs e)
    {   

      
        gvGeneric = (GridView)sender;
        SetSortDirection(SortDireaction);

        DataTableForGridViewSource.DefaultView.Sort = e.SortExpression + " " + _sortDirection;
        gvGeneric.PageIndex = 0;
     //   BindGridView(gvGeneric, DataTableForGridViewSource);
        gvGeneric.DataSource = DataTableForGridViewSource;
        gvGeneric.DataBind();

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
            ViewState["FormEntryMode"] = "INSERT";

            BindRoles();
            BindModules();
            GetPageData();
            //BindGridView(grdPages, DataTableForGridViewSource);
        }

    }
    private void BindModules()
    {
        energiosSecurity.Module mod = new energiosSecurity.Module();
        ddlModules.SelectedIndex = -1;
        ddlModules.Items.Clear();

        DataTable tb = new DataTable();
        tb = mod.GetAllModules();
        DataTable selectedTable = tb.AsEnumerable()
                             .Where(r => r.Field<bool>("IsActive") == true && r.Field<string>("ShortName").ToUpper() != "SETTINGS")
                             .CopyToDataTable();

        ddlModules.DataSource = selectedTable;
        ddlModules.DataTextField = "ShortName";
        ddlModules.DataValueField = "ModuleID";
        ddlModules.DataBind();
        ListItem lst = new ListItem();
        lst.Text = "Select";
        lst.Value = "-1";
        ddlModules.Items.Insert(0, lst);
        BindSections(Convert.ToInt32(ddlModules.SelectedValue.ToString()));
    }
    private void BindSections(int mid)
    {
        System.Data.DataTable dt = new System.Data.DataTable();
        ESqlHelper.FillDataTable(ECommon.ConString, CommandType.Text, "SELECT * FROM appmstr_ModuleSections Where isactive=1 and ModuleId=" + mid, dt);
        ddlSections.SelectedIndex = -1;
        ddlSections.Items.Clear();
        ddlSections.DataSource = dt;
        ddlSections.DataTextField = "SectionName";
        ddlSections.DataValueField = "ModuleSectionID";
        ddlSections.DataBind();





        ListItem lst = new ListItem();
        lst.Text = "All";
        lst.Value = "-1";
        ddlSections.Items.Insert(0, lst);
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {

        grdPages.UseAccessibleHeader = true;
        if (grdPages.Rows.Count > 0)
            grdPages.HeaderRow.TableSection = TableRowSection.TableHeader;


    }

    private void BindRoles()
    {
        energiosSecurity.Role rol = new energiosSecurity.Role();
        ddlRoles.SelectedIndex = -1;
        ddlRoles.Items.Clear();
        DataTable tb = new DataTable();
        tb = rol.GetAll();
        DataTable selectedTable = tb.AsEnumerable()
                            // .Where(r => r.Field<bool>("IsActive") == true)
                             .CopyToDataTable();
        ddlRoles.DataSource = selectedTable;
        ddlRoles.DataTextField = "RoleName";
        ddlRoles.DataValueField = "RoleID";
        ddlRoles.DataBind();
        ListItem lst = new ListItem();

        lst.Text = "< Select Role >";
        lst.Value = "-1";
        // ddlRoles.Items.Insert(0, "< Select Role >");
        ddlRoles.Items.Insert(0, lst);
    }

    private void GetPageData()
    {
        energiosSecurity.Pages pgs = new energiosSecurity.Pages();
        System.Data.DataTable dt = new System.Data.DataTable();
        dt = pgs.GetAll();
        System.Data.DataTable dtFinal = new System.Data.DataTable();
        dtFinal = dt.Clone();
        foreach (DataRow pgRow in dt.Rows)
        {
            pgRow["PageName"] = pgRow["PageName"].ToString();
            //pgRow["ModuleName"].ToString() + " => " +
            dtFinal.ImportRow(pgRow);

        }

        DataTableForGridViewSource = dtFinal;

    }



    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        energiosSecurity.RoleRights rights = new energiosSecurity.RoleRights();
        int result = 0;
        if (ddlRoles.SelectedValue.ToString() != "-1")
        {


            foreach (GridViewRow gvRow in grdPages.Rows)
            {
                CheckBox chkView = (CheckBox)(gvRow.FindControl("chkView"));
                CheckBox chkAdd = (CheckBox)(gvRow.FindControl("chkAdd"));
                CheckBox chkEdit = (CheckBox)(gvRow.FindControl("chkEdit"));
                CheckBox chkDelete = (CheckBox)(gvRow.FindControl("chkDelete"));
                CheckBox chkPrint = (CheckBox)(gvRow.FindControl("chkPrint"));
                CheckBox chkVerify1 = (CheckBox)(gvRow.FindControl("chkVerify1"));
                CheckBox chkVerify2 = (CheckBox)(gvRow.FindControl("chkVerify2"));


                if (rights.IsAlreadyExists(Convert.ToInt32(this.ddlRoles.SelectedValue.ToString()), Convert.ToInt32(gvRow.Cells[0].Text)) == false)
                {
                    if (chkView.Checked == false && chkAdd.Checked == false && chkEdit.Checked == false && chkDelete.Checked == false && chkPrint.Checked == false)
                    {

                    }
                    else
                    {
                        SqlParameter[] parameters =
                    {   new SqlParameter( "@roleid"		, Convert.ToInt32(this.ddlRoles.SelectedValue.ToString())  ) ,
                        new SqlParameter( "@pgid"		, Convert.ToInt32(gvRow.Cells[0].Text)  ) ,
                        new SqlParameter( "@IsView"		, chkView.Checked) ,
                        new SqlParameter( "@IsAdd"		, chkAdd.Checked),
                        new SqlParameter( "@IsUpdate"	, chkEdit.Checked),
                        new SqlParameter( "@IsDelete"	, chkDelete.Checked),
                        new SqlParameter( "@IsPrint"	, chkPrint.Checked),
                         new SqlParameter( "@IsVerify1"	, chkVerify1.Checked),
                          new SqlParameter( "@IsVerify2"	, chkVerify2.Checked),
                        new SqlParameter( "@CreatedBy"		, Session["UserID"]   )
                
                    };
                        result = rights.Insert(parameters);

                    }
                }
                else
                {
                    SqlParameter[] parameters =
                    {   new SqlParameter( "@roleid"		, Convert.ToInt32(this.ddlRoles.SelectedValue.ToString())  ) ,
                        new SqlParameter( "@pgid"		, Convert.ToInt32(gvRow.Cells[0].Text)  ) ,
                        new SqlParameter( "@IsView"		, chkView.Checked) ,
                        new SqlParameter( "@IsAdd"		, chkAdd.Checked),
                        new SqlParameter( "@IsUpdate"	, chkEdit.Checked),
                        new SqlParameter( "@IsDelete"	, chkDelete.Checked),
                        new SqlParameter( "@IsPrint"	, chkPrint.Checked),
                         new SqlParameter( "@IsVerify1"	, chkVerify1.Checked),
                          new SqlParameter( "@IsVerify2"	, chkVerify2.Checked),
                        new SqlParameter( "@Modifiedby"		, Session["UserID"]   )
                
                    };
                    result = rights.Update(parameters);

                }

            }
            if (result == 1)
            {
                if (ViewState["FormEntryMode"].ToString() == "INSERT")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", "ShowMessage('Success',' Rights Added Successfully.')", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", "ShowMessage('Success',' Rights Updated Successfully.')", true);
                }
                GetPageData();
                ddlSections_SelectedIndexChanged(sender, e);
                //BindGridView(grdPages, DataTableForGridViewSource);
                //ResetForm();

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", "ShowMessage('Error','Saving Process Failed.')", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", "ShowMessage('Error','Please select a role to assign rights.')", true);
        }
    }
    private void ResetForm()
    {
        foreach (GridViewRow gvRow in grdPages.Rows)
        {
            CheckBox chkView = (CheckBox)(gvRow.FindControl("chkView"));
            CheckBox chkAdd = (CheckBox)(gvRow.FindControl("chkAdd"));
            CheckBox chkEdit = (CheckBox)(gvRow.FindControl("chkEdit"));
            CheckBox chkDelete = (CheckBox)(gvRow.FindControl("chkDelete"));
            CheckBox chkPrint = (CheckBox)(gvRow.FindControl("chkPrint"));
            CheckBox chkVerify1 = (CheckBox)(gvRow.FindControl("chkVerify1"));
            CheckBox chkVerify2 = (CheckBox)(gvRow.FindControl("chkVerify2"));
            chkView.Checked = false;
            chkAdd.Checked = false;
            chkEdit.Checked = false;
            chkDelete.Checked = false;
            chkPrint.Checked = false;
            chkVerify1.Checked = false;
            chkVerify2.Checked = false;
        }
        this.ddlRoles.SelectedIndex = -1;
        this.ddlRoles.Focus();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ResetForm();
    }
    protected void ddlRoles_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlRoles.SelectedIndex != -1)
        {
            if (ddlRoles.SelectedValue.ToString() != "-1")
            {
                //Get Rights against Roles
                energiosSecurity.RoleRights rights = new energiosSecurity.RoleRights();
                System.Data.DataTable dt = new System.Data.DataTable();
                dt = rights.GetRoleRights(Convert.ToInt32(ddlRoles.SelectedValue.ToString()));

                foreach (GridViewRow gvRow in grdPages.Rows)
                {
                    CheckBox chkView = (CheckBox)(gvRow.FindControl("chkView"));
                    CheckBox chkAdd = (CheckBox)(gvRow.FindControl("chkAdd"));
                    CheckBox chkEdit = (CheckBox)(gvRow.FindControl("chkEdit"));
                    CheckBox chkDelete = (CheckBox)(gvRow.FindControl("chkDelete"));
                    CheckBox chkPrint = (CheckBox)(gvRow.FindControl("chkPrint"));
                    CheckBox chkVerify1 = (CheckBox)(gvRow.FindControl("chkVerify1"));
                    CheckBox chkVerify2 = (CheckBox)(gvRow.FindControl("chkVerify2"));

                    chkView.Checked = false;
                    chkAdd.Checked = false;
                    chkEdit.Checked = false;
                    chkDelete.Checked = false;
                    chkPrint.Checked = false;
                    chkVerify1.Checked = false;
                    chkVerify2.Checked = false;
                    foreach (DataRow dtRow in dt.Rows)
                    {
                        if (gvRow.Cells[0].Text == dtRow["PageID"].ToString())
                        {

                            if (dtRow["IsView"].ToString() == "True") chkView.Checked = true;
                            if (dtRow["IsAdd"].ToString() == "True") chkAdd.Checked = true;
                            if (dtRow["IsUpdate"].ToString() == "True") chkEdit.Checked = true;
                            if (dtRow["IsDelete"].ToString() == "True") chkDelete.Checked = true;
                            if (dtRow["IsPrint"].ToString() == "True") chkPrint.Checked = true;
                            if (dtRow["IsVerify1"].ToString() == "True") chkVerify1.Checked = true;
                            if (dtRow["IsVerify2"].ToString() == "True") chkVerify2.Checked = true;

                        }
                    }
                }
            }
            else
            {
                foreach (GridViewRow gvRow in grdPages.Rows)
                {
                    CheckBox chkView = (CheckBox)(gvRow.FindControl("chkView"));
                    CheckBox chkAdd = (CheckBox)(gvRow.FindControl("chkAdd"));
                    CheckBox chkEdit = (CheckBox)(gvRow.FindControl("chkEdit"));
                    CheckBox chkDelete = (CheckBox)(gvRow.FindControl("chkDelete"));
                    CheckBox chkPrint = (CheckBox)(gvRow.FindControl("chkPrint"));
                    CheckBox chkVerify1 = (CheckBox)(gvRow.FindControl("chkVerify1"));
                    CheckBox chkVerify2 = (CheckBox)(gvRow.FindControl("chkVerify2"));
                    chkView.Checked = false;
                    chkAdd.Checked = false;
                    chkEdit.Checked = false;
                    chkDelete.Checked = false;
                    chkPrint.Checked = false;
                    chkVerify1.Checked = false;
                    chkVerify2.Checked = false;
                }
            }
        }

    }



    protected void txtSearchKeyWord_TextChanged(object sender, EventArgs e)
    {
        GetPageData();




        BindGridView(grdPages, DataTableForGridViewSource);

    }
    //private void FilterList()
    //{
    //    string srchcond = " ";
    //    string srchKeyWord = txtSearchKeyWord.Text.Trim();
    //    if (txtSearchKeyWord.Text.Trim().Length > 0)
    //    {
    //        srchcond = " (PageName Like '%" + srchKeyWord + "%' OR ModuleName LIKE '%" + srchKeyWord + "%' OR SectionName  LIKE '%" + srchKeyWord + "%'  )";
    //    }
    //    DataTableForGridViewSource = AppHelper.FilterDataTable(DataTableForGridViewSource, srchcond);

    //}

    protected void ddlModules_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindSections(Convert.ToInt32(ddlModules.SelectedValue.ToString()));
        string srchcond = " ";
        if (ddlModules.SelectedIndex > 0)
        {
            GetPageData();
            srchcond = " (ModuleName LIKE '%" + ddlModules.SelectedItem.Text + "%'  )";
            DataTableForGridViewSource = AppHelper.FilterDataTable(DataTableForGridViewSource, srchcond);
            BindGridView(grdPages, DataTableForGridViewSource);
        }
        else
        {
            GetPageData();
            BindGridView(grdPages, DataTableForGridViewSource);
        }
    }
    protected void ddlSections_SelectedIndexChanged(object sender, EventArgs e)
    {

        string srchcond = " ";

        if (ddlSections.SelectedIndex > 0)
        {
            GetPageData();
            srchcond = " ( ModuleName LIKE '%" + ddlModules.SelectedItem.Text + "%' AND SectionName LIKE '%" + ddlSections.SelectedItem.Text + "%'  )";
            DataTableForGridViewSource = AppHelper.FilterDataTable(DataTableForGridViewSource, srchcond);
            BindGridView(grdPages, DataTableForGridViewSource);
        }
        else
        {
            if (ddlModules.SelectedIndex > 0)
            {
                GetPageData();
                srchcond = " (ModuleName LIKE '%" + ddlModules.SelectedItem.Text + "%'  )";
                DataTableForGridViewSource = AppHelper.FilterDataTable(DataTableForGridViewSource, srchcond);
                BindGridView(grdPages, DataTableForGridViewSource);
            }
            else
            {
                GetPageData();
                BindGridView(grdPages, DataTableForGridViewSource);
            }
        }

    }
}