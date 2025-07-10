using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Ionic.Zip;
using System.IO;
using System.Text;
using System.Configuration;
using System.Net.Mail;
using DocumentFormat.OpenXml.Bibliography;


public partial class Modules_Purchase_Invoice_LubeConsumptionDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                BindVessel();
                BindYear();
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message.ToString();
        }
    }

    public void BindVessel()
    {
        string WhereClause = "";
        string sql = "SELECT VesselId,VesselCode,Vesselname FROM Vessel v Where 1=1 ";
        sql = sql + WhereClause + " and v.VesselId in (Select VesselId from UserVesselRelation with(nolock) where Loginid = " + Convert.ToInt32(Session["loginid"].ToString()) + ") ORDER BY VESSELNAME";
        ddlVessel.DataSource = VesselReporting.getTable(sql);

        ddlVessel.DataTextField = "Vesselname";
        ddlVessel.DataValueField = "VesselCode";
        ddlVessel.DataBind();
        ddlVessel.Items.Insert(0, new ListItem("< Select >", "0"));
    }

    private void BindYear()
    {
        ddlYear.Items.Add(new ListItem("< Select >", "0"));
        for (int i = DateTime.Today.Year; i >= 2023; i--)
        {
            ddlYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {

            if (ddlVessel.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "asdf", "alert('Please select Vessel.');", true);
                ddlVessel.Focus();
                return;
            }

            if (ddlMonth.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "asdf", "alert('Please select Month.');", true);
                ddlMonth.Focus();
                return;
            }

            if (ddlYear.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "asdf", "alert('Please select Year.');", true);
                ddlYear.Focus();
                return;
            }

           
            GetOrderConsumptionDetails(Convert.ToInt32(ddlMonth.SelectedValue), Convert.ToInt32(ddlYear.SelectedValue));
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message.ToString();
        }
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        if (ddlVessel.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "succ", "alert('Please select Vessel.');", true);
            ddlVessel.Focus();
            return;
        }
        if (ddlMonth.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "succ", "alert('Please select Month.');", true);
            ddlMonth.Focus();
            return;
        }
        if (ddlYear.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "succ", "alert('Please select Year.');", true);
            ddlVessel.Focus();
            return;
        }
        if (string.IsNullOrEmpty(txtOfficeRemark.Text.Trim()))
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "succ", "alert('Please enter Office Remark.');", true);
            txtOfficeRemark.Focus();
            return;
        }

        // int RiskId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        // string VesselCode = ((ImageButton)sender).Attributes["VesselCode"];
        try
        {
            //DataTable dtVesselEmail = Common.Execute_Procedures_Select_ByQuery("select EMAIL, VesselEmailNew from DBO.VESSEL WHERE VESSELCODE='" + ddlVessel.SelectedValue + "'");
            //string EmailAddress = "";
            //List<string> CCMails = new List<string>();
            //List<string> BCCMails = new List<string>();
            //if (dtVesselEmail.Rows.Count > 0)
            //{
            //    EmailAddress = dtVesselEmail.Rows[0]["VesselEmailNew"].ToString();
            //    if (!string.IsNullOrWhiteSpace(dtVesselEmail.Rows[0]["EMAIL"].ToString()))
            //    {
            //        BCCMails.Add(dtVesselEmail.Rows[0]["EMAIL"].ToString());
            //    }

            //    DataTable dtLoginUser = Common.Execute_Procedures_Select_ByQuery("Select Email from UserLogin with(nolock) where LoginId = " + Convert.ToInt32(Session["LoginId"].ToString()) + "");
            //    if (dtLoginUser.Rows.Count > 0 && !string.IsNullOrWhiteSpace(dtVesselEmail.Rows[0]["Email"].ToString()))
            //    {
            //        CCMails.Add(dtVesselEmail.Rows[0]["Email"].ToString());
            //    }
            //}

            //if (EmailAddress.Trim() != "")
            //{
                // int IsStatus = chkMonthClosed.Checked ? 1 : 0;
                //Common.Execute_Procedures_Select_ByQuery("Update POS_OrderReceiptDetails_Consumption SET OfficeRemark = '" + txtOfficeRemark.Text.Trim() + "', Status = " + IsStatus + " where VesselCode = '" + ddlVessel.SelectedValue + "' AND  ConsumpMonth = " + Convert.ToInt32(ddlMonth.SelectedValue) + " AND ConsumpYear = " + Convert.ToInt32(ddlYear.SelectedValue));
                Common.Execute_Procedures_Select_ByQuery("UPDATE [DBO].[POS_OrderReceiptDetails_Consumption] SET OfficeRemark = '" + txtOfficeRemark.Text.Trim() + "',OfficeVerifiedBy='" + Session["FullName"].ToString() + "', OfficedVerifiedOn = getdate(),Status = 1, updated = 1, updatedon= getdate() where VesselCode = '" + ddlVessel.SelectedValue + "' AND  ConsumpMonth = " + Convert.ToInt32(ddlMonth.SelectedValue) + " AND ConsumpYear = " + Convert.ToInt32(ddlYear.SelectedValue));
                 GetOrderConsumptionDetails(Convert.ToInt32(ddlMonth.SelectedValue), Convert.ToInt32(ddlYear.SelectedValue));
                //string SQL = "SELECT * FROM DBO.POS_OrderReceiptDetails_Consumption with(nolock) WHERE VESSELCODE='" + ddlVessel.SelectedValue + "' AND ConsumpMonth=" + ddlMonth.SelectedValue + "  AND ConsumpYear= " + ddlYear.SelectedValue;
            //DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
            //dt.TableName = "POS_OrderReceiptDetails_Consumption";
            //DataSet ds = new DataSet();
            //ds.Tables.Add(dt.Copy());
            //string SchemaFile = Server.MapPath("~/Modules/Purchase/Temp/LUBE_CONSUMPTION_SCHEMA.xml");
            //string DataFile = Server.MapPath("~/Modules/Purchase/Temp/LUBE_CONSUMPTION_DATA.xml");

            //ds.WriteXml(DataFile);
            //ds.WriteXmlSchema(SchemaFile);
            //string RefNo = ddlVessel.SelectedValue + "_" + ddlMonth.SelectedItem.Text + "_" + ddlYear.SelectedValue;
            //string ZipData = Server.MapPath("~/Modules/Purchase/Temp/LUBE_CONSUMPTION_REPORT_O_" + RefNo + ".zip");
            //if (File.Exists(ZipData)) { File.Delete(ZipData); }

            //using (ZipFile zip = new ZipFile())
            //{
            //    zip.AddFile(SchemaFile);
            //    zip.AddFile(DataFile);
            //    zip.Save(ZipData);
            //}

            //string Subject = "LUBE CONSUMPTION REPORT - [ " + RefNo.ToString() + " ] - Office Reply";

            //StringBuilder sb = new StringBuilder();
            //sb.Append("Dear Captain,<br /><br />");
            //sb.Append("Attached please find the office reply for your Lube Consumption Report.<br /><br />");
            //sb.Append("Thank You,");

            //string fromAddress = ConfigurationManager.AppSettings["FromAddress"].ToString();

            //string result = SendMail.SendSimpleMail(fromAddress, EmailAddress, CCMails.ToArray(), BCCMails.ToArray(), Subject, sb.ToString(), ZipData);
            //if (result == "SENT")
            //{

            //    if (chkMonthClosed.Checked)
            //    {
            //       
            //    }
            //   
            //}
            //else
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "abort", "alert('Unable to export. Error : " + result + "');", true);
            //}
            //}
            AddGLEntry(ddlVessel.SelectedValue, Convert.ToInt32(ddlMonth.SelectedValue), Convert.ToInt32(ddlYear.SelectedValue));
            ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "alert('Lube Consumption Verification successfully.');", true);

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "err", "alert('Unable to export.Error : " + ex.Message + "');", true);
        }
    }

    protected void AddGLEntry(string vesselCode, int consumpMonth, int consumpYear)
    {
        Common.Execute_Procedures_Select_ByQuery("EXEC SP_InserttblGLEntry  '" + vesselCode + "', " + consumpMonth + ", " + consumpYear + ", "+ Convert.ToInt32(Session["LoginId"].ToString()) + " ");
    }

    private void GetOrderConsumptionDetails(int month, int year)
    {
        
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("EXEC GetPOSOrder_Consumption '" + ddlVessel.SelectedValue + "'," + month + "," + year + " ");
        if (dt.Rows.Count > 0)
        {
            RptPOConsumption.DataSource = dt;
            RptPOConsumption.DataBind();
            txtConsumptionDescription.Text = dt.Rows[0]["Remarks"].ToString();
            txtMasterName.Text = dt.Rows[0]["Master_Name"].ToString();
            txtCEName.Text = dt.Rows[0]["CE_Name"].ToString();
            txtAddedBy.Text = dt.Rows[0]["AddedBy"].ToString();
            lblAddedon.Text = Common.ToDateString(dt.Rows[0]["AddedOn"].ToString());
            //if (Convert.ToBoolean(dt.Rows[0]["Status"].ToString()) == true)
            //{
            //    chkMonthClosed.Checked = true;
            //}
            //else
            //{
            //    chkMonthClosed.Checked = false;
            //}

            if (! string.IsNullOrEmpty(dt.Rows[0]["OfficeRemark"].ToString()))
            {
                txtOfficeRemark.Text = dt.Rows[0]["OfficeRemark"].ToString();
            }
            else
            {
                txtOfficeRemark.Text = "";
            }

            if (!string.IsNullOrEmpty(dt.Rows[0]["OfficeVerifiedBy"].ToString()))
            {
                btnVerification.Visible = false;
                lblVerifiedBy.Text = dt.Rows[0]["OfficeVerifiedBy"].ToString();
                lblVerifiedOn.Text = Common.ToDateString(dt.Rows[0]["OfficedVerifiedOn"].ToString());
            }
            else
            {
                btnVerification.Visible = true;
                lblVerifiedBy.Text = "";
                lblVerifiedOn.Text = "";
            }

            //int TotalConsumptionCount = 0;
            //TotalConsumptionCount = Convert.ToInt32(dt.Rows[0]["UtilCount"]);
            //if (TotalConsumptionCount > 0)
            //{
            //    btnExport.Visible = true;
            //}
            //else
            //{
            //    btnExport.Visible = false;
            //}
            int selectedMonth = Convert.ToInt32(ddlMonth.SelectedValue);
            int previousmonth = selectedMonth - 1;
            string PreviousMonthName = ProjectCommon.GetMonthName(previousmonth.ToString());
            lblPreviousMonthROB.Text = PreviousMonthName.ToUpper() + " - ROB ";
            string CurrentMonthName = ProjectCommon.GetMonthName(selectedMonth.ToString());
            lblCurrMonthConsumption.Text = CurrentMonthName.ToUpper() + " - Consumption ";
            lblCurrMonthROB.Text = CurrentMonthName.ToUpper() + " - ROB ";
        }
        else
        {
            RptPOConsumption.DataSource = null;
            RptPOConsumption.DataBind();
            txtConsumptionDescription.Text = "";
            txtMasterName.Text = "";
            txtCEName.Text = "";
            txtAddedBy.Text = "";
            lblAddedon.Text = "";
           
           // btnExport.Visible = false;
            int selectedMonth = Convert.ToInt32(ddlMonth.SelectedValue);
            int previousmonth = selectedMonth - 1;
            string PreviousMonthName = ProjectCommon.GetMonthName(previousmonth.ToString());
            lblPreviousMonthROB.Text = PreviousMonthName.ToUpper() + " - ROB ";
            string CurrentMonthName = ProjectCommon.GetMonthName(selectedMonth.ToString());
            lblCurrMonthConsumption.Text = CurrentMonthName.ToUpper() + " - Consumption ";
            lblCurrMonthROB.Text = CurrentMonthName.ToUpper() + " - ROB ";
        }
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {

            if (ddlVessel.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "asdf", "alert('Please select Vessel.');", true);
                ddlVessel.Focus();
                return;
            }

            if (ddlMonth.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "asdf", "alert('Please select Month.');", true);
                ddlMonth.Focus();
                return;
            }

            if (ddlYear.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "asdf", "alert('Please select Year.');", true);
                ddlYear.Focus();
                return;
            }


            DataTable dt = Common.Execute_Procedures_Select_ByQuery("EXEC GetPOSOrder_Consumption '" + ddlVessel.SelectedValue + "'," + Convert.ToInt32(ddlMonth.SelectedValue) + "," + Convert.ToInt32(ddlYear.SelectedValue) + " ");
            //Create a dummy GridView
            //GridView GridView1 = new GridView();
            GvPOConsumption.Visible = true;
            GvPOConsumption.AllowPaging = false;
            GvPOConsumption.DataSource = dt;
            GvPOConsumption.DataBind();


            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=LubeConsumption_"+ddlMonth.SelectedItem.Text+"_"+ddlYear.SelectedValue+".xls");
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.Charset = "UTF-8";
            Response.ContentType = "application/vnd.ms-excel.12";

            //Response.ContentType = "application/vnd.ms-excel";
            ////Response.ContentType = "application/vnd.ms-excel";
            //Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            //for (int i = 0; i < GridView1.Rows.Count; i++)
            //{
            //    //Apply text style to each Row
            //    GridView1.Rows[i].Attributes.Add("class", "textmode");
            //}
            GvPOConsumption.RenderControl(hw);
            //style to format numbers to string
            //string style = @"<style> .textmode { mso-number-format:\@; } </style>";
            //Response.Write(style);
            Response.Output.Write(sw.ToString());
            GvPOConsumption.Visible = false;
            Response.Flush();
            Response.End();
        }
        catch (Exception ex)
        {

        }
    }
    public override void VerifyRenderingInServerForm(Control control)

    {

    }
    protected void GvPOConsumption_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[6].Text = lblPreviousMonthROB.Text;
            e.Row.Cells[7].Text = lblCurrMonthConsumption.Text;
            e.Row.Cells[10].Text = lblCurrMonthROB.Text;
        }
    }
}