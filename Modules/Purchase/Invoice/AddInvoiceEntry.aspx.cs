using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Configuration;

public partial class Invoice_InvoiceEntry : System.Web.UI.Page
{
    Random r = new Random();
    public DataTable PoNos
    {
        set
        {ViewState["PoNos"] = value;}
        get 
        {
            if (ViewState["PoNos"] == null)
            {
                DataTable dt=new DataTable();
                dt.Columns.Add(new DataColumn("PONO",typeof (string)));
                dt.Columns.Add(new DataColumn("BIDID", typeof(string)));
                dt.Columns.Add(new DataColumn("OTHEROTHERINVID", typeof(string)));
                dt.Columns.Add(new DataColumn("OTHERREFNO", typeof(string)));
                ViewState["PoNos"] =dt;
            }

            return (DataTable)ViewState["PoNos"];
        }
    }
    public int InvId
    {
        get { return Convert.ToInt32(ViewState["InvId"]); }
        set { ViewState["InvId"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //---------------------------------------
        ProjectCommon.SessionCheck();
        //---------------------------------------
        if (!Page.IsPostBack)
        {
            txt_DueDate.Text = DateTime.Today.AddMonths(1).ToString("dd-MMM-yyyy");

            bindVesselNameddl();
            bindCurrencyddl();
            bindForwardToddl();
            ViewState["TempFilePath"] = "";
            if (Request.QueryString["InvoiceId"] != null && Request.QueryString["InvoiceId"].ToString() != "")
            {
                InvId =  Common.CastAsInt32(Request.QueryString["InvoiceId"].ToString());
                ShowInvoiceDetails();
            }

        }
        tab_Upload.Visible = (InvId <= 0);
    }
    public void ShowInvoiceDetails()
    {
        btnClip.Visible = false;
        btnClipText.Visible = false;

        string SQL = "SELECT * FROM dbo.vw_Pos_Invoices_001 WHERE [InvoiceId] = " + InvId;
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);

        if (dt.Rows.Count > 0)
        {
            lblRefNo.Text = dt.Rows[0]["RefNo"].ToString();
            txtSupplier.Text = dt.Rows[0]["Vendor"].ToString();
            hfdSupplier.Value = dt.Rows[0]["SupplierId"].ToString();
            txt_InvNo.Text = dt.Rows[0]["InvNo"].ToString();
            txt_InvDate.Text = Common.ToDateString(dt.Rows[0]["InvDate"]);
            txt_DueDate.Text = Common.ToDateString(dt.Rows[0]["DueDate"]);
            txt_InvAmount.Text = dt.Rows[0]["InvoiceAmount"].ToString();
            ddCurrency.SelectedValue = dt.Rows[0]["Currency"].ToString();
            ddl_Vessel.SelectedValue = dt.Rows[0]["INVVesselCode"].ToString();
            //txtPoNo.Text = dt.Rows[0]["PONo"].ToString();
            if (dt.Rows[0]["AttachmentName"].ToString().Trim() != "")
            {
                btnClip.Visible = true;
                btnClipText.Visible = true;
                btnClipText.Text = dt.Rows[0]["AttachmentName"].ToString();
            }

            txtEntryComments.Text = dt.Rows[0]["EntryComments"].ToString();
            ddlForwardTo.SelectedValue = dt.Rows[0]["ApprovalFwdTo"].ToString();

            chkAdvPayment.Checked = (Convert.ToString(dt.Rows[0]["IsAdvPayment"]) == "True");
            chkNonPO.Checked = (Convert.ToString(dt.Rows[0]["IsNonPo"]) == "True");
            //---------
            tblSaveInv.Visible = true;
            tblForwardTo.Visible = true;
            //---------
            string sql="SELECT PONO,BidId, " +
                       "CONVERT(VARCHAR,(SELECT TOP 1 INVOICEID FROM DBO.POS_Invoice_Payment_PO P WHERE P.BidId=I.BIDID AND ISNULL(P.BidId,0)<>0 AND  P.InvoiceId<>" + InvId + ")) AS OTHEROTHERINVID, " +
                       "(SELECT REFNO FROM DBO.POS_Invoice I1 WHERE I1.INVOICEID IN (SELECT TOP 1 INVOICEID FROM DBO.POS_Invoice_Payment_PO P WHERE P.BidId=I.BIDID AND ISNULL(P.BidId,0)<>0 AND P.InvoiceId<>" + InvId + ")) AS OTHERREFNO " +
                       "FROM " +
                       "DBO.POS_Invoice_Payment_PO I " +
                       "WHERE INVOICEID=" + InvId;
            PoNos = Common.Execute_Procedures_Select_ByQuery(sql);
            BindPoList();
            if (PoNos.Rows.Count != 0)
            {
                chkNonPO.Enabled = false;
            }
            else
            {
                chkNonPO.Enabled = true;
            }
        }

    }
    protected void bindVesselNameddl()
    {
        string sql = "SELECT shipid,shipid + ' - ' + SHIPNAME AS SHIPNAME from VW_ACTIVEVESSELS  where VesselNo in (Select VesselId from UserVesselRelation with(nolock) where Loginid = "+ Convert.ToInt32(Session["loginid"].ToString())+")  ORDER BY SHIPNAME";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
        ddl_Vessel.DataSource = dt;
        ddl_Vessel.DataValueField = "shipid";
        ddl_Vessel.DataTextField = "SHIPNAME";
        ddl_Vessel.DataBind();
        ddl_Vessel.Items.Insert(0, new ListItem("< Select Vessel >", "0"));
    }
    protected void bindCurrencyddl()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT Curr FROM [dbo].[VW_tblWebCurr]");
        this.ddCurrency.DataValueField = "Curr";
        this.ddCurrency.DataTextField = "Curr";
        this.ddCurrency.DataSource = dt;
        this.ddCurrency.DataBind();
        ddCurrency.Items.Insert(0, new ListItem("< Select Currency >", "0"));
    }
    protected void bindForwardToddl()
    {
        //string SQL = "select (FirstName + ' ' + LastName ) AS UserName, LoginId  from shipsoft_admin.dbo.usermaster where loginid in (select userid from pos_invoice_mgmt where Approval=1) AND statusId='A' Order By UserName";
        string SQL = "select (FirstName + ' ' + LastName ) AS UserName, LoginId  from dbo.usermaster where statusId='A' Order By UserName";

        DataTable dt1 = Common.Execute_Procedures_Select_ByQuery(SQL);
        this.ddlForwardTo.DataValueField = "LoginId";
        this.ddlForwardTo.DataTextField = "UserName";
        this.ddlForwardTo.DataSource = dt1;
        this.ddlForwardTo.DataBind();
        ddlForwardTo.Items.Insert(0, new ListItem("< Select Purchaser >", "0"));
    }

    protected void btnPO_delete_Click(object sender, EventArgs e)
    {
        int BIDID = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        DataRow[] drs=PoNos.Select("BIDID='" + BIDID + "'");
        foreach (DataRow dr in drs)
        {
            PoNos.Rows.Remove(dr);
        }
        
        if(PoNos.Rows.Count==0)
        {
            ddl_Vessel.Enabled = true;
            chkNonPO.Enabled = true;
        }
        BindPoList();
        
    }
    protected void btnAddPO_Click(object sender, EventArgs e)
    {
        
        if (txtPoNo.Text.Trim() != "")
        {
            string povsl = txtPoNo.Text.Substring(0,3);
            if (Common.CastAsInt32(povsl)==0)
            {
                if (ddl_Vessel.SelectedItem.Text.Substring(0,3) != povsl)
                {
                    lbl_inv_Message.Text = "PO vessel not matching with invoice vessel.";
                    txtPoNo.Focus();
                    return;
                }
            }

            if (PoNos.Select("PONO='" + txtPoNo.Text.Trim() + "'").Length <= 0)
            {
                int BidId=Common.CastAsInt32(hfdbidid.Value);

                if (BidId<=0)
                {
                    lbl_inv_Message.Text = "Invalid PO#.";
                    txtPoNo.Focus();
                    return;
                }
                string InvoiceRefNo = "";
                string OTHEROTHERINVID = "";
                DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT InvoiceId,RefNo, ISNULL(IsAdvPayment,0) As IsAdvPayment FROM [dbo].[POS_Invoice] I WHERE I.InvoiceId IN ( SELECT INVOICEID FROM POS_Invoice_Payment_PO WHERE BIDID=" + BidId + " AND INVOICEID<>" + InvId + ")");
                if (dt.Rows.Count > 0)
                {
                    OTHEROTHERINVID = dt.Rows[0][0].ToString();
                    InvoiceRefNo = dt.Rows[0][1].ToString();
                    if (Convert.ToBoolean(dt.Rows[0][2]) == false)
                    {
                        lbl_inv_Message.Text = "Sorry this PO is already linked with some other invoice ( Ref No : " + InvoiceRefNo + ").";
                        return;
                    }
                }

                PoNos.Rows.Add(PoNos.NewRow());
                PoNos.Rows[PoNos.Rows.Count - 1]["PONO"] = txtPoNo.Text;
                PoNos.Rows[PoNos.Rows.Count - 1]["BIDID"] = BidId.ToString();
                PoNos.Rows[PoNos.Rows.Count - 1]["OTHEROTHERINVID"] = OTHEROTHERINVID;
                PoNos.Rows[PoNos.Rows.Count - 1]["OTHERREFNO"] = InvoiceRefNo;

                txtPoNo.Text = "";
                hfdbidid.Value = "0";
                BindPoList();
                ddl_Vessel.Enabled = false; 
            }
        }
    }
    public void BindPoList()
    {
        rptPo.DataSource = PoNos;
        rptPo.DataBind();
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        string SupplierName = txtSupplier.Text.Trim().Replace("'","`");
        if (Common.CastAsInt32(hfdSupplier.Value) <= 0)
        {
            lbl_inv_Message.Text = "Please select vendor";
            txtSupplier.Focus();
            return;
        }

        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT SupplierName FROM [dbo].[VW_tblSMDSuppliers] WHERE SupplierName='" + SupplierName + "'");
        if (dt.Rows.Count <= 0)
        {
            lbl_inv_Message.Text = "Please select valid vendor";
            txtSupplier.Focus();
            return;
        }

        dt = Common.Execute_Procedures_Select_ByQuery("SELECT InvoiceId, RefNo FROM [dbo].[POS_Invoice] WHERE SupplierId=" + hfdSupplier.Value.Trim() + " AND LTRIM(RTRIM(InvNo)) = '" + txt_InvNo.Text.Trim() + "' AND InvoiceId <> " + InvId);
        if (dt.Rows.Count > 0)
        {
            lbl_inv_Message.Text = "Duplicate invoice. Already entered with Ref#: <a target='_blank' href='ViewInvoice.aspx?InvoiceId=" + dt.Rows[0]["InvoiceId"].ToString() + "' >" + dt.Rows[0]["RefNo"].ToString() + "</a>";
            txt_InvNo.Focus();
            return;
        }

        //if (rptPo.Items.Count>0)
        //{
        //    if (!fuAttachment.HasFile)
        //    {
        //        lbl_inv_Message.Text = "To continue please attach the invoice pdf file for linked purchase orders.";
        //        fuAttachment.Focus();
        //        return;
        //    }
        //}

        string fileName = "";
        byte[] file = new byte[0];

        if (ViewState["TempFilePath"].ToString().Trim() != "")
        {
            fileName = ViewState["TempFilePath"].ToString().Trim();
            string appname = ConfigurationManager.AppSettings["AppName"].ToString();
            file = File.ReadAllBytes(Server.MapPath("/" + appname + "/EMANAGERBLOB/Purchase/Invoice/" + ViewState["TempFilePath"].ToString()));
            //file = File.ReadAllBytes(Server.MapPath("/EMANAGERBLOB/Purchase/Invoice/" + ViewState["TempFilePath"].ToString()));
        }


        //if (fuAttachment.HasFile)
        //{

        //    fileName = Path.GetFileName(fuAttachment.PostedFile.FileName);
        //    if (fileName != String.Empty)
        //    {
        //        Stream fileStream = fuAttachment.PostedFile.InputStream;
        //        int fileLength = fuAttachment.PostedFile.ContentLength;

        //        file = new byte[fileLength];
        //        fileStream.Read(file, 0, fileLength);
        //    }
        //}
         
        string bids = "";
        foreach (DataRow dr in PoNos.Rows)
        {
            bids +=","+ dr["BIDID"].ToString();
        }
        if (bids.StartsWith(","))
            bids = bids.Substring(1);
        try
        {
            Common.Set_Procedures("Inv_InsertInvoice_001");
            Common.Set_ParameterLength(16);
            Common.Set_Parameters(
                new MyParameter("@InvoiceId", InvId),
                new MyParameter("@SupplierId", hfdSupplier.Value.Trim()),
                new MyParameter("@INVVesselCode", ddl_Vessel.SelectedValue.Trim()),                 
                new MyParameter("@InvNo", txt_InvNo.Text.Trim()),
                new MyParameter("@InvDate", txt_InvDate.Text.Trim()),
                new MyParameter("@DueDate", txt_DueDate.Text.Trim()),
                new MyParameter("@InvoiceAmount", txt_InvAmount.Text.Trim()),
                new MyParameter("@Currency", ddCurrency.SelectedValue.Trim()),
                new MyParameter("@PONo", ""),
                new MyParameter("@EntertedBy", Session["Loginid"].ToString()),
                new MyParameter("@EntryComments", txtEntryComments.Text.Trim()),
                new MyParameter("@AttachmentName", fileName),
                new MyParameter("@Bids", bids),
                new MyParameter("@Attachment", file),
                new MyParameter("@IsAdvPayment",chkAdvPayment.Checked ? 1 : 0),
                new MyParameter("@IsNonPo", chkNonPO.Checked ? 1 : 0)
                );

            DataSet ds = new DataSet();
            ds.Clear();
            Boolean res;
            res = Common.Execute_Procedures_IUD(ds);
            if (res)
            {
                InvId = Common.CastAsInt32(ds.Tables[0].Rows[0][0].ToString());
                lblRefNo.Text = ds.Tables[0].Rows[0][1].ToString();
                lbl_inv_Message.Text = "Record Successfully Saved.";
                tblSaveInv.Visible = false;
                tblForwardTo.Visible = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "Refresh();", true);
            }
            else
            {
                lbl_inv_Message.Text = "Unable to save record." + Common.getLastError();
            }
        }
        catch (Exception ex)
        {
            lbl_inv_Message.Text = "Unable to save record." + ex.Message + Common.getLastError();
        }
           
        
    }
    protected void btnForwardTo_Click(object sender, EventArgs e)
    {
        try
        {
            string ft_SQL = "UPDATE POS_Invoice SET ApprovalFwdTo=" + ddlForwardTo.SelectedValue.Trim() + " , Stage = 1  WHERE InvoiceId=" + InvId;
            Common.Execute_Procedures_Select_ByQuery(ft_SQL);
            lbl_inv_Message.Text = "Forwarded Successfully.";
            btnForwardTo.Visible = false;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "", "Refresh();", true);
        }
        catch (Exception ex)
        {
            lbl_inv_Message.Text = "Unable to forward. Error :" + ex.Message;
        }

    }
    protected void txt_InvDate_TextChanged(object sender, EventArgs e)
    {
        //try
        //{
        //    //DateTime dt = Convert.ToDateTime(txt_InvDate.Text);
        //    //txt_DueDate.Text = dt.AddMonths(1).ToString("dd-MMM-yyyy");
            
        //}
        //catch
        //{
        //}
    }
    protected void ddl_Vessel_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_Vessel.SelectedIndex > 0)
            txtPoNo.Text = ddl_Vessel.SelectedItem.Text.Substring(0, 3) + "-";
        else
            txtPoNo.Text = "";

        hfdbidid.Value = "0";
    }
    protected void btnClip_Click(object sender, ImageClickEventArgs e)
    {
        DownloadFile();
    }
    protected void btnClipText_Click(object sender, EventArgs e)
    {
        DownloadFile();
    }
    protected void DownloadFile()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT AttachmentName,Attachment FROM POS_Invoice WHERE InvoiceId=" + InvId);
        if (dt.Rows.Count > 0)
        {
            string FileName = dt.Rows[0]["AttachmentName"].ToString();
            if (FileName.Trim() != "")
            {
                byte[] buff = (byte[])dt.Rows[0]["Attachment"];
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileName);
                Response.BinaryWrite(buff);
                Response.Flush();
                Response.End();
            }
        }
    }
    protected void btn_Upload_Click(object sender, EventArgs e)
    {
        if (fuAttachment.HasFile)
        {
            string fileName = Path.GetFileName(fuAttachment.PostedFile.FileName);
            if (fuAttachment.HasFile)
            {
                if (fileName != String.Empty)
                {
                    Stream fileStream = fuAttachment.PostedFile.InputStream;
                    int fileLength = fuAttachment.PostedFile.ContentLength;
                    string appname = ConfigurationManager.AppSettings["AppName"].ToString();
                    fuAttachment.SaveAs(Server.MapPath("/" + appname + "/EMANAGERBLOB/Purchase/Invoice/" + fileName));
                    //fuAttachment.SaveAs(Server.MapPath("/EMANAGERBLOB/Purchase/Invoice/" + fileName));
                    tr_Frm.Visible = true;
                    frmInvoice.Attributes.Add("src", "/" + appname + "/EMANAGERBLOB/Purchase/Invoice/" + fileName + "?" + r.NextDouble().ToString());
                    //frmInvoice.Attributes.Add("src", "/EMANAGERBLOB/Purchase/Invoice/" + fileName + "?" + r.NextDouble().ToString());
                    ViewState["TempFilePath"] = fileName;
                }
            }
        }
    }



    protected void chkAdvPayment_CheckedChanged(object sender, EventArgs e)
    {
       if(chkAdvPayment.Checked)
        {
            chkNonPO.Checked = false;
            chkNonPO.Enabled = false;
            btnAddPO.Enabled = true;
        }
       else
        {
            chkNonPO.Checked = false;
            chkNonPO.Enabled = true;
            btnAddPO.Enabled = true;
        }
    }

    protected void chkNonPO_CheckedChanged(object sender, EventArgs e)
    {
        if (chkNonPO.Checked)
        {
            chkAdvPayment.Checked = false;
            chkAdvPayment.Enabled = false;
            btnAddPO.Visible = false;
        }
        else
        {
            chkAdvPayment.Checked = false;
            chkAdvPayment.Enabled = true;
            btnAddPO.Visible = true;
        }
    }
}