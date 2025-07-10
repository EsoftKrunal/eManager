using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Drawing;

public partial class Modules_Purchase_Invoice_VendorSOA : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //---------------------------------------
            ProjectCommon.SessionCheck();
            //---------------------------------------
            if (!Page.IsPostBack)
            {
                BindVendor();
                BindddlYear();
            }
        }
        catch(Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Error : "+ex.Message.ToString()+"');", true);
        }
    }

    #region "Functions"
    public void BindVendor()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("select row_number() over (order by SupplierName) as Sno,SupplierId,SupplierName,SupplierPort,SupplierEmail,TravId from VW_tblSMDSuppliers Order By SupplierName"); //Where SupplierName like '" + txtVendor.Text + "%' 
        if (dt != null)
        {
            ddlVendor.DataSource = dt;
            ddlVendor.DataTextField = "SupplierName";
            ddlVendor.DataValueField = "SupplierId";
            ddlVendor.DataBind();
            ddlVendor.Items.Insert(0, new ListItem("< Select >", "0"));
        }
    }
    public void BindddlYear()
    {
        string sql = "Select  distinct CurFinYear from AccountCompanyBudgetMonthyear with(nolock) ";
        DataTable dtCurrentyear = Common.Execute_Procedures_Select_ByQuery(sql);
        if (dtCurrentyear != null)
        {
            ddlFinancialYr.DataSource = dtCurrentyear;
            ddlFinancialYr.DataTextField = "CurFinYear";
            ddlFinancialYr.DataValueField = "CurFinYear";
            ddlFinancialYr.DataBind();
            ddlFinancialYr.Items.Insert(0, new ListItem("< Select >", ""));
        }
    }

    protected void BindExporttoExcelVesselSOA(DataTable dt)
    {
        try
        {
            GVVendorSOA.Visible = true;
            GVVendorSOA.AllowPaging = false;
            GVVendorSOA.DataSource = dt;
            GVVendorSOA.DataBind();
            Response.Clear();
            Response.Buffer = true;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Charset = "";
            string FileName = "Vendor_Outstanding_Report_" + DateTime.Now + ".xls";
            StringWriter strwritter = new StringWriter();
            HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
            GVVendorSOA.GridLines = GridLines.Both;
            GVVendorSOA.HeaderStyle.Font.Bold = true;
            GVVendorSOA.RenderControl(htmltextwrtter);
            Response.Write(strwritter.ToString());
            GVVendorSOA.Visible = false;
            Response.End();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Error on Vessel SOA Export to Excel : " + ex.Message.ToString() + "');", true);
        }
    }

    protected void BindExporttoExcelVesselInvoicePayment(DataTable dt)
    {
        try
        {
            GvInvoicePaymentDetails.Visible = true;
            GvInvoicePaymentDetails.AllowPaging = false;
            GvInvoicePaymentDetails.DataSource = dt;
            GvInvoicePaymentDetails.DataBind();
            Response.Clear();
            Response.Buffer = true;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Charset = "";
            string FileName = "Vendor_Invoice_Payment_Report_" + DateTime.Now + ".xls";
            StringWriter strwritter = new StringWriter();
            HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
            GvInvoicePaymentDetails.GridLines = GridLines.Both;
            GvInvoicePaymentDetails.HeaderStyle.Font.Bold = true;
            GvInvoicePaymentDetails.RenderControl(htmltextwrtter);
            Response.Write(strwritter.ToString());
            GvInvoicePaymentDetails.Visible = false;
            Response.End();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Error on Vessel Invoice Payment Export to Excel : " + ex.Message.ToString() + "');", true);
        }
    }

    protected void BindExporttoExcelVesselSOASummary(DataTable dt)
    {
        try
        {
            GvVendorSOASummary.Visible = true;
            GvVendorSOASummary.AllowPaging = false;
            GvVendorSOASummary.DataSource = dt;
            GvVendorSOASummary.DataBind();
            Response.Clear();
            Response.Buffer = true;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Charset = "";
            string FileName = "All_Vendor_Outstanding_Summary_Report_" + DateTime.Now + ".xls";
            StringWriter strwritter = new StringWriter();
            HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
            GvVendorSOASummary.GridLines = GridLines.Both;
            GvVendorSOASummary.HeaderStyle.Font.Bold = true;
            GvVendorSOASummary.RenderControl(htmltextwrtter);
            Response.Write(strwritter.ToString());
            GvVendorSOASummary.Visible = false;
            Response.End();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Error on Vessel Invoice Payment Export to Excel : " + ex.Message.ToString() + "');", true);
        }
    }

    protected DataTable GetData(string SPName, int SupplierId, string FinancialYr)
    {
        string SQL = "";
        if (SupplierId > 0)
        {
            SQL = "EXEC " + SPName + " " + SupplierId + ",'" + FinancialYr + "' ";
        }
        else
        {
            SQL = "EXEC " + SPName + " '" + FinancialYr + "' ";
        }

        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
        return dt;
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }


    #endregion

    #region "Events"
    protected void btnClear_Click(object sender, EventArgs e)
    {
        try
        {
            ddlReport.SelectedIndex = 0;
            ddlVendor.SelectedIndex = 0;
            ddlFinancialYr.SelectedIndex = 0;
        }
        catch(Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Error on clear button : " + ex.Message.ToString() + "');", true);
        }
    }
    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        try
        {
           if (ddlReport.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please select Report.');", true);
                ddlReport.Focus();
                return;
            }
            if (ddlVendor.Enabled && ddlVendor.SelectedValue == "0")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please select Vendor.');", true);
                ddlVendor.Focus();
                return;
            }
            if (ddlFinancialYr.SelectedValue == "0")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please select Financial Year.');", true);
                ddlFinancialYr.Focus();
                return;
            }
            DataTable dt = new DataTable();
            if (Convert.ToInt32(ddlReport.SelectedValue) == 1)
            {
                dt = GetData("Get_Pos_Invoice_VendorSOA",Convert.ToInt32(ddlVendor.SelectedValue), ddlFinancialYr.SelectedValue);
                BindExporttoExcelVesselSOA(dt);
            }
            else if (Convert.ToInt32(ddlReport.SelectedValue) == 2)
            {
                dt = GetData("GetInvoicedetailforSupplier", Convert.ToInt32(ddlVendor.SelectedValue), ddlFinancialYr.SelectedValue);
                BindExporttoExcelVesselInvoicePayment(dt);
            }
            else if (Convert.ToInt32(ddlReport.SelectedValue) == 3 )
            {
                dt = GetData("Get_Pos_Invoice_VendorSOA_Summary", 0, ddlFinancialYr.SelectedValue);
                BindExporttoExcelVesselSOASummary(dt);
            }
        }
        catch(Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Error on Export to Excel button : " + ex.Message.ToString() + "');", true);
        }
    }
    protected void GVVendorSOA_DataBound(object sender, EventArgs e)
    {
        try
        {
            GridViewRow row = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
        TableHeaderCell tec = new TableHeaderCell();
        tec = new TableHeaderCell();
        tec.ColumnSpan = 9;
        tec.Text = "Vendor Outstanding Report - " + ddlVendor.SelectedItem.Text +" ("+ddlFinancialYr.SelectedValue+" ) ";
        row.Controls.Add(tec);
        row.BackColor = ColorTranslator.FromHtml("#3AC0F2");
        row.Font.Bold = true;
        row.Font.Size = 18;
        GVVendorSOA.HeaderRow.Parent.Controls.AddAt(0, row);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Error on Vendor Outstanding Report : " + ex.Message.ToString() + "');", true);
        }
    }
    protected void GvInvoicePaymentDetails_DataBound(object sender, EventArgs e)
    {
        try
        {
            GridViewRow row = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
        TableHeaderCell tec = new TableHeaderCell();
        tec = new TableHeaderCell();
        tec.ColumnSpan = 17;
        tec.Text = "Vendor Invoice Payment Detail - " + ddlVendor.SelectedItem.Text + " (" + ddlFinancialYr.SelectedValue + " ) ";
        row.Controls.Add(tec);
        row.BackColor = ColorTranslator.FromHtml("#3AC0F2");
        row.Font.Bold = true;
        row.Font.Size = 18;
        GvInvoicePaymentDetails.HeaderRow.Parent.Controls.AddAt(0, row);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Error on Vendor Invoice Payment Detail : " + ex.Message.ToString() + "');", true);
        }
    }
    protected void GvVendorSOASummary_DataBound(object sender, EventArgs e)
    {
        try
        {
            GridViewRow row = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
            TableHeaderCell tec = new TableHeaderCell();
            tec = new TableHeaderCell();
            tec.ColumnSpan = 4;
            tec.Text = "All Vendor Outstanding Summary - (" + ddlFinancialYr.SelectedValue + " ) ";
            row.Controls.Add(tec);
            row.BackColor = ColorTranslator.FromHtml("#3AC0F2");
            row.Font.Bold = true;
            row.Font.Size = 18;
            GvVendorSOASummary.HeaderRow.Parent.Controls.AddAt(0, row);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Error on All Vendor Outstanding Summary : " + ex.Message.ToString() + "');", true);
        }
    }
    protected void ddlReport_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToInt32(ddlReport.SelectedValue) == 3)
            {
                ddlVendor.Enabled = false;
            }
            else
            {
                ddlVendor.Enabled = true;
            }
        }

        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Error on Report Selection : " + ex.Message.ToString() + "');", true);
    }
}
    #endregion
}