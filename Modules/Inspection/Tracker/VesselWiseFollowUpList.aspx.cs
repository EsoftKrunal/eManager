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

public partial class FormReporting_VesselWiseFollowUpList : System.Web.UI.Page
{
    int intVesselId = 0;
    int intTemp = 0;
    string strFollowUpCat = "", strFollowUpFDate = "", strFollowUpTDate = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        try
        {
            if (Page.Request.QueryString["FPVesselId"].ToString() != "")
            {
                intVesselId = int.Parse(Page.Request.QueryString["FPVesselId"].ToString());
            }
            if (Page.Request.QueryString["FPCatID"].ToString() != "")
            {
                strFollowUpCat = Page.Request.QueryString["FPCatID"].ToString();
                HiddenField_FollowUpCat.Value = strFollowUpCat;
            }
            if (Page.Request.QueryString["FPFrmDate"].ToString() != "")
            {
                strFollowUpFDate = Page.Request.QueryString["FPFrmDate"].ToString();
                HiddenField_FrmDate.Value = strFollowUpFDate;
            }
            if (Page.Request.QueryString["FPToDate"].ToString() != "")
            {
                strFollowUpTDate = Page.Request.QueryString["FPToDate"].ToString();
                HiddenField_ToDate.Value = strFollowUpTDate;
            }
            if (!Page.IsPostBack)
            {
                Session["SortingExp"] = "ASC";
                BindVslFollowUpDetailsGrid(Grd_VslFollowUpDetail.Attributes["MySort"]);
            }
        }
        catch (Exception ex) { throw ex; }
        //--------------------------------
        btn_View.Enabled =new AuthenticationManager(1079, int.Parse(Session["loginid"].ToString()),ObjectType.Page).IsUpdate;    
        btn_Print.Enabled =new AuthenticationManager(1079, int.Parse(Session["loginid"].ToString()),ObjectType.Page).IsPrint;
        //--------------------------------
    }
    public void BindVslFollowUpDetailsGrid(String sort)
    {
        try
        {
            Session["Sorting_Exp"] = sort;
            DataTable dt1 = FollowUp_Tracker.SelectVesselWiseFollowUpDetails(intVesselId, strFollowUpCat, strFollowUpFDate, strFollowUpTDate, txt_DueDays.Text.Trim(), ddl_Status.SelectedValue, (chk_Critical.Checked) ? "Y" : "N", ddl_Resp.SelectedValue, (chk_OverDue.Checked) ? "Y" : "N");
            HiddenField_DueInDays.Value = txt_DueDays.Text.Trim();
            HiddenField_Status.Value = ddl_Status.SelectedValue;
            if (chk_Critical.Checked == true)
                HiddenField_Critical.Value = "Y";
            else
                HiddenField_Critical.Value = "N";
            HiddenField_Responsibility.Value = ddl_Resp.SelectedValue;
            if (chk_OverDue.Checked == true)
                HiddenField_OverDue.Value = "Y";
            else
                HiddenField_OverDue.Value = "N";
            if (dt1.Rows.Count > 0)
            {
                lbl_VesselName.Text = dt1.Rows[0]["VesselName"].ToString();
                Grd_VslFollowUpDetail.AllowSorting = true;
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
                lbl_RecordCount.Text = "Total Record : " + dt1.Rows.Count.ToString();
                txt_VesselId.Text = dt1.Rows[0]["VesselId"].ToString();
                Grd_VslFollowUpDetail.DataSource = dt1;
                Grd_VslFollowUpDetail.DataBind();
                hid.Text = "";
                hid1.Text = "";
                hid2.Text = "";
            }
            else
            {
                BindBlankGrid();
                lbl_RecordCount.Text = "Total Record : 0";
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
            dt.Columns.Add("InspDueId");
            dt.Columns.Add("Deficiency");
            dt.Columns.Add("Source");
            dt.Columns.Add("TargetCloseDate");
            dt.Columns.Add("Responsibility");
            dt.Columns.Add("CompletionDate");
            dt.Columns.Add("ObsvId");
            dt.Columns.Add("TblName");
            dt.Columns.Add("Closed");

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
                dt.Rows[dt.Rows.Count - 1][8] = "";
            }

            Grd_VslFollowUpDetail.DataSource = dt;
            Grd_VslFollowUpDetail.DataBind();
            Grd_VslFollowUpDetail.SelectedIndex = -1;
        }
        catch (Exception ex) { throw ex; }
    }
    protected void Grd_VslFollowUpDetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            Grd_VslFollowUpDetail.PageIndex = e.NewPageIndex;
            Grd_VslFollowUpDetail.SelectedIndex = -1;
            if (Session["Sorting_Exp"] != null)
                {
                    intTemp = 1;
                    BindVslFollowUpDetailsGrid(Session["Sorting_Exp"].ToString());
                }
                else
                    BindVslFollowUpDetailsGrid(Grd_VslFollowUpDetail.Attributes["MySort"]);
        }
        catch (Exception ex) { throw ex; }
    }
    //protected void lnk_Click(object sender, EventArgs e)
    //{
    //    Session["VslFP_Id"] = int.Parse(hid.Text);
    //}
    protected void btn_Show_Click(object sender, EventArgs e)
    {
        BindVslFollowUpDetailsGrid(Grd_VslFollowUpDetail.Attributes["MySort"]);
    }
    protected void btn_Reset_Click(object sender, EventArgs e)
    {
        txt_DueDays.Text = "";
        ddl_Status.SelectedIndex = 0;
        chk_Critical.Checked = false;
        ddl_Resp.SelectedIndex = 0;
        btn_Show_Click(sender, e);
        hid.Text = "";
        hid1.Text = "";
        hid2.Text = "";
        chk_OverDue.Checked = false;
        txt_DueDays.ReadOnly = false;
    }
    protected void Grd_VslFollowUpDetail_Sorted(object sender, EventArgs e)
    {

    }
    protected void Grd_VslFollowUpDetail_Sorting(object sender, GridViewSortEventArgs e)
    {
        BindVslFollowUpDetailsGrid(e.SortExpression);
    }
    protected void Grd_VslFollowUpDetail_PreRender(object sender, EventArgs e)
    {
        for (int a = 0; a < Grd_VslFollowUpDetail.Rows.Count; a++)
        {
            Label lblDate = (Label)Grd_VslFollowUpDetail.Rows[a].FindControl("lbl_DueDate");
            HiddenField hfdClosed=(HiddenField)Grd_VslFollowUpDetail.Rows[a].FindControl("hfd_Closed");
            if (lblDate.Text != "")
            {
                if (hfdClosed.Value =="False")
                {
                    if (DateTime.Parse(lblDate.Text.Trim()) < DateTime.Parse(DateTime.Today.Date.ToString()))
                    {
                        Grd_VslFollowUpDetail.Rows[a].Cells[0].BackColor = System.Drawing.Color.FromName("#FFCCCC");
                    }
                }
            }
        }
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
}
