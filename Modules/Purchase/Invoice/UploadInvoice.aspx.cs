using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;

public partial class Invoice_UploadInvoice : System.Web.UI.Page
{
    public int InvoiceId
    {
        get { return Convert.ToInt32(ViewState["InvoiceId"]); }
        set { ViewState["InvoiceId"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InvoiceId = Common.CastAsInt32(Request.QueryString["InvoiceId"]);
        }
    }
    protected void btn_UpdateAttachment_Click(object sender, EventArgs e)
    {
        if (fuAttachment.HasFile)
        {
            string fileName = Path.GetFileName(fuAttachment.PostedFile.FileName);
            if (fileName != String.Empty)
            {
                Stream fileStream = fuAttachment.PostedFile.InputStream;
                int fileLength = fuAttachment.PostedFile.ContentLength;

                byte[] file = new byte[fileLength];
                fileStream.Read(file, 0, fileLength);

                Common.Set_Procedures("Inv_UpdateAttachment");
                Common.Set_ParameterLength(3);
                Common.Set_Parameters(
                    new MyParameter("@InvoiceId", InvoiceId),
                    new MyParameter("@AttachmentName", fileName),
                    new MyParameter("@Attachment", file)
                    );

                DataSet ds = new DataSet();
                ds.Clear();
                Boolean res;
                res = Common.Execute_Procedures_IUD(ds);
                if (res)
                {
                    lbl_inv_Message.Text = "Uploaded Successfully.";
                }
                else
                {
                    lbl_inv_Message.Text = "Unable to update attachment. Error :" + Common.ErrMsg;
                }

            }
        }


    }
}