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

public partial class FormReporting_VesselWiseCOCList : System.Web.UI.Page
{
    public Boolean GridStatus = true;
    int intVesselId = 0;
    int intTemp = 0;
    string strCOCFDate = "", strCOCTDate = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        lblmessage.Text = "";
        lbl_RecordCount.Text = "";
        try
        {
            if (Page.Request.QueryString["COCVesselId"].ToString() != "")
                intVesselId = int.Parse(Page.Request.QueryString["COCVesselId"].ToString());

            if (Page.Request.QueryString["COCFrmDate"].ToString() != "")
            {
                strCOCFDate = Page.Request.QueryString["COCFrmDate"].ToString();
                HiddenField_FrmDate.Value = strCOCFDate;
            }
            if (Page.Request.QueryString["COCToDate"].ToString() != "")
            {
                strCOCTDate = Page.Request.QueryString["COCToDate"].ToString();
                HiddenField_ToDate.Value = strCOCTDate;
            }
            if (!Page.IsPostBack)
            {
                Session["SortingExp"] = "ASC";
                BindVslCOCDetailsGrid(Grd_VslCOCDetail.Attributes["MySort"]);
            }
        }
        catch (Exception ex) { throw ex; }
        //--------------------------------
        btn_View.Enabled = new AuthenticationManager(166, int.Parse(Session["loginid"].ToString()), ObjectType.Page).IsUpdate;
        btn_Print.Enabled = new AuthenticationManager(166, int.Parse(Session["loginid"].ToString()), ObjectType.Page).IsPrint;
        //--------------------------------

    }
    public void BindVslCOCDetailsGrid(String sort)
    {
        try
        {
            Session["Sorting_Exp"] = sort;
            DataTable dt1 = COC.SelectVesselWiseCOCDetails(intVesselId,strCOCFDate, strCOCTDate, txt_DueDays.Text.Trim(), ddl_Status.SelectedValue, ddl_Resp.SelectedValue, (chk_OverDue.Checked) ? "Y" : "N");
            HiddenField_DueInDays.Value = txt_DueDays.Text.Trim();
            HiddenField_Status.Value = ddl_Status.SelectedValue;
            HiddenField_Responsibility.Value = ddl_Resp.SelectedValue;
            if (chk_OverDue.Checked == true)
                HiddenField_OverDue.Value = "Y";
            else
                HiddenField_OverDue.Value = "N";
            if (dt1.Rows.Count > 0)
            {
                GridStatus = true;
                lbl_VesselName.Text = dt1.Rows[0]["VesselName"].ToString();
                Grd_VslCOCDetail.AllowSorting = true;
                if (Session["SortingExp"] != null)
                {
                    if ((sort != null) || (Session["Sorting_Exp"] != null))
                    {
                        if (intTemp == 0)
                        {
                            if (Session["SortingExp"].ToString() == "ASC")
                            {
                                dt1.DefaultView.Sort = sort;
                                Session["SortingExp"] = "DESC";
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
                                            dt1.DefaultView.Sort = a.GetValue(0).ToString() + " " + "DESC";
                                        else
                                            dt1.DefaultView.Sort = dt1.DefaultView.Sort + "," + a.GetValue(1).ToString() + " " + "DESC";
                                    }
                                }
                                else
                                {
                                    dt1.DefaultView.Sort = sort + " " + "DESC";
                                }
                                Session["SortingExp"] = "ASC";
                            }
                        }
                        else
                        {
                            if (Session["SortingExp"].ToString() == "ASC")
                            {
                                if (sort == "AA,BB")
                                {
                                    char[] c = { ',' };
                                    Array a = sort.Split(c);
                                    for (int l = 0; l < a.Length; l++)
                                    {
                                        if (l == 0)
                                            dt1.DefaultView.Sort = a.GetValue(0).ToString() + " " + "DESC";
                                        else
                                            dt1.DefaultView.Sort = dt1.DefaultView.Sort + "," + a.GetValue(1).ToString() + " " + "DESC";
                                    }
                                }
                                else
                                {
                                    dt1.DefaultView.Sort = sort + " " + "DESC";
                                }
                                Session["SortingExp"] = "ASC";
                            }
                            else
                            {
                                dt1.DefaultView.Sort = sort;
                                Session["SortingExp"] = "DESC";
                            }
                        }
                    }
                }
                lbl_RecordCount.Text = "Total Records : " + dt1.Rows.Count.ToString();
                txt_VesselId.Text = dt1.Rows[0]["VesselId"].ToString();
                Grd_VslCOCDetail.DataSource = dt1;
                Grd_VslCOCDetail.DataBind();
                hid.Text = "";
                HiddenField1.Value = dt1.Rows.Count.ToString();
            }
            else
            {
                GridStatus = false;
                BindBlankGrid();
                HiddenField1.Value = "";
            }
        }
        catch (Exception ex) { throw ex; }
    }
    public void BindBlankGrid()
    {
        try
        {
            DataTable dt = new DataTable();
            DataRow dr;
            dt.Columns.Add("COCID");
            dt.Columns.Add("COCNO");
            dt.Columns.Add("IssuedFrom");
            dt.Columns.Add("TargetCloseDate");
            dt.Columns.Add("Responsibility");
            dt.Columns.Add("CompletionDate");
            dt.Columns.Add("Closed");
            dt.Columns.Add("UPFILENAME");

            for (int i = 0; i < 9; i++)
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
            }

            Grd_VslCOCDetail.DataSource = dt;
            Grd_VslCOCDetail.DataBind();
            Grd_VslCOCDetail.SelectedIndex = -1;
        }
        catch (Exception ex) { throw ex; }
    }
    protected void Grd_VslCOCDetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            Grd_VslCOCDetail.PageIndex = e.NewPageIndex;
            Grd_VslCOCDetail.SelectedIndex = -1;
            if (Session["Sorting_Exp"] != null)
            {
                intTemp = 1;
                BindVslCOCDetailsGrid(Session["Sorting_Exp"].ToString());
            }
            else
                BindVslCOCDetailsGrid(Grd_VslCOCDetail.Attributes["MySort"]);
        }
        catch (Exception ex) { throw ex; }
    }
    protected void btn_Show_Click(object sender, EventArgs e)
    {
        BindVslCOCDetailsGrid(Grd_VslCOCDetail.Attributes["MySort"]);
    }
    protected void btn_Reset_Click(object sender, EventArgs e)
    {
        txt_DueDays.Text = "";
        ddl_Status.SelectedValue = "0";
        ddl_Resp.SelectedIndex = 0;
        btn_Show_Click(sender, e);
        hid.Text = "";
        chk_OverDue.Checked = false;
        txt_DueDays.ReadOnly = false;
    }
    protected void Grd_VslCOCDetail_Sorted(object sender, EventArgs e)
    {

    }
    protected void Grd_VslCOCDetail_Sorting(object sender, GridViewSortEventArgs e)
    {
        BindVslCOCDetailsGrid(e.SortExpression);
    }
    protected void Grd_VslCOCDetail_PreRender(object sender, EventArgs e)
    {
        for (int a = 0; a < Grd_VslCOCDetail.Rows.Count; a++)
        {
            Label lblDate = (Label)Grd_VslCOCDetail.Rows[a].FindControl("lbl_DueDate");
            HiddenField hfdClosed = (HiddenField)Grd_VslCOCDetail.Rows[a].FindControl("hfd_Closed");
            if (lblDate.Text != "")
            {
                if ((hfdClosed.Value == "False") || (hfdClosed.Value==""))
                {
                    if (DateTime.Parse(lblDate.Text.Trim()) < DateTime.Parse(DateTime.Today.Date.ToString()))
                    {
                        Grd_VslCOCDetail.Rows[a].Cells[0].BackColor = System.Drawing.Color.FromName("#FFCCCC");
                    }
                }
            }
            if (hfdClosed.Value == "True")
            {
                ImageButton ImgBtnDelete = (ImageButton)Grd_VslCOCDetail.Rows[a].FindControl("ImageButton1");
                ImgBtnDelete.Enabled = false;
            }
        }
        if (Grd_VslCOCDetail.Rows.Count <= 0) { lbl_RecordCount.Text = ""; } else { if (GridStatus == false) lbl_RecordCount.Text = ""; else lbl_RecordCount.Text = "Total Records : " + HiddenField1.Value; }
    }
    protected void chk_OverDue_CheckedChanged(object sender, EventArgs e)
    {
        if (chk_OverDue.Checked)
        {
            txt_DueDays.ReadOnly = true;
            txt_DueDays.Text = "";
        }
        else
            txt_DueDays.ReadOnly = false;
    }
    public string GetPath(string _path)
    {
        string res = "";
        if (_path.StartsWith("U"))
        {
            res = "../" + _path;
        }
        else
        {
            res = "../UserUploadedDocuments/COC_Tracker/" + _path;
        }
        return res;
    }
    protected void Grd_VslCOCDetail_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            HiddenField hfddel = (HiddenField)Grd_VslCOCDetail.Rows[e.RowIndex].FindControl("hfd_COCId");
            DataTable DTDEL = COC.GetFCOCDetailsByCOCId(int.Parse(hfddel.Value), 0, "", "", "", "", "", "", 0, 0, "DELETE", "", 0, "", "", "", "", "", "","", "", "", 0,"","");
            lblmessage.Text = "Record Deleted Successfully.";
            BindVslCOCDetailsGrid(Grd_VslCOCDetail.Attributes["MySort"]);
        }
        catch { }
    }
}
