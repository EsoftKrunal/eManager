using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using Microsoft.ApplicationBlocks.Data;
public partial class ModuleSections : System.Web.UI.Page
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
                HeaderCell.Controls.Add(new Literal() { Text = "Page Size :  <br />" });
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
            BindModules();
             GetPageData();
             BindGridView(gvModuleSections, DataTableForGridViewSource);
            ViewState["SectionScreenEntryMode"] = "INSERT";
            EntryPanel.Visible = false;
            GridPanel.Visible = true;

        }

        if (ViewState["SectionScreenEntryMode"].ToString().ToUpper() == "INSERT")
        {
            //if (Auth.IsAdd) {
                btnSubmit.Visible = true;
            //}
            //else { btnSubmit.Visible = false; }
        }


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
    
    protected void Page_PreRender(object sender, EventArgs e)
    {

        gvModuleSections.UseAccessibleHeader = true;
        if (gvModuleSections.Rows.Count > 0)
            gvModuleSections.HeaderRow.TableSection = TableRowSection.TableHeader;


    }


    private void PopulateControls(DataRow dtRow)
    {
        ClearControls();


        this.txtSectionID.Text = dtRow["ModuleSectionID"].ToString();
        this.txtSection.Text = dtRow["SectionName"].ToString();
        this.ddlModules.SelectedIndex = -1;
        this.ddlModules.Items.FindByValue(dtRow["ModuleID"].ToString()).Selected = true;
        Session["ModuleID"] = dtRow["ModuleID"];

        if (dtRow["IsActive"].ToString() == "True")
        {
            this.chkIsActive.Checked = true;
        }
        else
        {
            this.chkIsActive.Checked = false;
        }
        EntryPanel.Visible = true;
        GridPanel.Visible = false;
    }
    private void ClearControls()
    {
        this.txtSectionID.Text = "";
        this.txtSection.Text = "";


        this.chkIsActive.Checked = true;


    }
    private bool ValidateFormEntries()
    {
        if (this.txtSection.Text.Trim() == "")
        {
            
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", "ShowMessage('Error','Please Enter Section Name')", true);
            return false;
        }
        if (ddlModules.SelectedValue.ToString() == "-1")
        {
            
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", "ShowMessage('Error','Please select a Module to Add Section.')", true);
            return false;
        }

        //Check for Duplicity 
        int cnt = Convert.ToInt32(ESqlHelper.ExecuteScalar(ECommon.ConString, CommandType.Text, " select count(1) from appmstr_ModuleSections where SECTIONNAME='" + this.txtSection.Text.Trim().ToUpper() + "' AND MODULEID=" + Convert.ToInt32(ddlModules.SelectedValue.ToString()) + ""));

        if (ViewState["SectionScreenEntryMode"].ToString() == "INSERT")
         {
             if (cnt > 0)
             {

                 ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", "ShowMessage('Error','Entered Section Name Already against selected Module.')", true);
                 return false;
             }
         }
         else
         {
             if (cnt > 1)
             {

                 ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", "ShowMessage('Error','Entered Section Name Already against selected Module.')", true);
                 return false;
             }
         }
        

        if (Convert.ToInt32(ddlModules.SelectedValue.ToString()) < 0)
        {
           
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", "ShowMessage('Error','Please select a Module to Add Section.')", true);
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
        energiosSecurity.Role rol = new energiosSecurity.Role();
        try
        {
            if (ViewState["SectionScreenEntryMode"].ToString() == "INSERT")
            {
                ESqlHelper.ExecuteNonQuery(ECommon.ConString, CommandType.Text, " INSERT INTO appmstr_ModuleSections (SECTIONNAME,MODULEID,ISACTIVE) VALUES('" + this.txtSection.Text.Trim().ToUpper() + "'," + Convert.ToInt32(ddlModules.SelectedValue.ToString()) + ", " + Convert.ToInt16(chkIsActive.Checked) + ")");
                result = 1;
                
                
                
            }
            else
            {
                // SqlHelper.ExecuteNonQuery(Common.AppConString, CommandType.Text, " Update  appmstr_ModuleSections SET SECTIONNAME='" + txtSection.Text.Trim().ToUpper() + "',IsActive="   + Convert.ToInt16(chkIsActive.Checked) + " WHERE MODULEID=" + Convert.ToInt32(ddlModules.SelectedValue.ToString()) + " AND MODULESECTIONID=" + txtSectionID.Text);
                ESqlHelper.ExecuteNonQuery(ECommon.ConString, CommandType.Text, " Update  appmstr_ModuleSections SET SECTIONNAME='" + txtSection.Text.Trim().ToUpper() + "',IsActive=" + Convert.ToInt16(chkIsActive.Checked) + " , MODULEID=" + Convert.ToInt32(ddlModules.SelectedValue.ToString()) + " WHERE MODULEID=" + Convert.ToInt32(Session["ModuleID"].ToString()) + " AND MODULESECTIONID=" + txtSectionID.Text);
                result = 1;
              

            }

            if (result == 1)
            {
                ClearControls();

              
                 GetPageData();
                 BindGridView(gvModuleSections, DataTableForGridViewSource);


               
                if (ViewState["SectionScreenEntryMode"].ToString() == "INSERT")
                {
                   DataTableForGridViewSource= AppHelper.SortDataTable(DataTableForGridViewSource, "ModuleSectionID DESC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", "ShowMessage('Success','Section Added Successfuly.')", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", "ShowMessage('Success','Section Update Successfuly.')", true);
                }
                ViewState["SectionScreenEntryMode"] = "INSERT";
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
        ViewState["SectionScreenEntryMode"] = "Update";
        
        if (ViewState["SectionScreenEntryMode"].ToString().ToUpper() == "UPDATE")
        {
            //if (Auth.IsEdit) { 
                btnSubmit.Visible = true; 
        //}
        //    else { btnSubmit.Visible = false; }
        }
        System.Data.DataTable dt = new System.Data.DataTable();
        ImageButton imgbtn = (ImageButton)sender;
        GridViewRow SelectedgvRow = (GridViewRow)imgbtn.NamingContainer;
 
      
        ESqlHelper.FillDataTable(ECommon.ConString, CommandType.Text, "SELECT * FROM appmstr_ModuleSections WHERE MOduleSectionID=" + Convert.ToInt32(SelectedgvRow.Cells[0].Text) + " ", dt);

        PopulateControls(dt.Rows[0]);
        txtSection.Focus();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ViewState["SectionScreenEntryMode"] = "INSERT";
        ClearControls();
        GetPageData();

        BindGridView(gvModuleSections, DataTableForGridViewSource);
        if (ViewState["SectionScreenEntryMode"].ToString().ToUpper() == "INSERT")
        {
            //if (Auth.IsAdd) { 
                btnSubmit.Visible = true; 
            //}
            //else { btnSubmit.Visible = false; }
        }
        EntryPanel.Visible = false;
        GridPanel.Visible = true;
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        EntryPanel.Visible = true;
        GridPanel.Visible = false;
        txtSection.Focus();
    }
    
    private void GetPageData()
    {

        System.Data.DataTable dt = new System.Data.DataTable();

        
         ESqlHelper.FillDataTable(ECommon.ConString, CommandType.Text, "SELECT * FROM appmstr_ModuleSections ", dt);
      
         DataTableForGridViewSource = dt;
        
    }
    

    protected void txtSearchKeyWord_TextChanged(object sender, EventArgs e)
    {
        GetPageData();

        FilterList();
        BindGridView(gvModuleSections, DataTableForGridViewSource);
        txtSearchKeyWord.Focus();
        //if (DataTableForGridViewSource.Rows.Count < 1)
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", "ShowMessage('Message','No Records Found')", true);
        //}
    }
    private void FilterList()
    {
        string srchcond = " ";
        string srchKeyWord = txtSearchKeyWord.Text.Trim();
        if (txtSearchKeyWord.Text.Trim().Length > 0)
        {
            srchcond = " (SectionName Like '%" + srchKeyWord + "%' )";
        }
        DataTableForGridViewSource = AppHelper.FilterDataTable(DataTableForGridViewSource, srchcond);
      
    }

    
}