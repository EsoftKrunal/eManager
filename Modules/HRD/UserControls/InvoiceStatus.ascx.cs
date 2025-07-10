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

public partial class UserControls_InvoiceStatus : System.Web.UI.UserControl
{
    private int _InvoiceId;
    public int InoviceId
    {
        get { return _InvoiceId; }
        set { _InvoiceId = value;}
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        Refresh();
    }
    public void Refresh()
    {
        DataTable dt;
        dt = Alerts.get_Invoice_Status(this.InoviceId);
        if (dt.Rows.Count > 0 && this.InoviceId > 0)
        {
            lblcreatedUser.BackColor = System.Drawing.Color.FromName(dt.Rows[0][5].ToString());
            lbl_Verify1.BackColor = System.Drawing.Color.FromName(dt.Rows[0][1].ToString());
            lbl_Approval.BackColor = System.Drawing.Color.FromName(dt.Rows[0][2].ToString());
            lbl_Verify2.BackColor = System.Drawing.Color.FromName(dt.Rows[0][3].ToString());
            lbl_Payment.BackColor = System.Drawing.Color.FromName(dt.Rows[0][4].ToString());


            if (lblcreatedUser.BackColor == System.Drawing.Color.Red)
            {
                lbl_Verify1.BackColor = System.Drawing.Color.White;
                lbl_Approval.BackColor = System.Drawing.Color.White;
                lbl_Verify2.BackColor = System.Drawing.Color.White;
                lbl_Payment.BackColor = System.Drawing.Color.White;
            }
            else if (lbl_Verify1.BackColor == System.Drawing.Color.Red)
            {
                lbl_Approval.BackColor = System.Drawing.Color.White;
                lbl_Verify2.BackColor = System.Drawing.Color.White;
                lbl_Payment.BackColor = System.Drawing.Color.White;
            }
            else if (lbl_Approval.BackColor == System.Drawing.Color.Red)
            {
                lbl_Verify2.BackColor = System.Drawing.Color.White;
                lbl_Payment.BackColor = System.Drawing.Color.White;
            }
            else if (lbl_Verify2.BackColor == System.Drawing.Color.Red)
            {
                lbl_Payment.BackColor = System.Drawing.Color.White;
            }
            else if (lbl_Payment.BackColor == System.Drawing.Color.Red)
            {
            }
            //else
            //{
            //    lblcreatedUser.BackColor = System.Drawing.Color.White;
            //    lbl_Verify1.BackColor = System.Drawing.Color.White;
            //    lbl_Approval.BackColor = System.Drawing.Color.White;
            //    lbl_Verify2.BackColor = System.Drawing.Color.White;
            //    lbl_Payment.BackColor = System.Drawing.Color.White;
            //}


            lbl_Verify1.Text = dt.Rows[1][1].ToString();
            lbl_Approval.Text = dt.Rows[1][2].ToString();
            lbl_Verify2.Text = dt.Rows[1][3].ToString();
            lbl_Payment.Text = dt.Rows[1][4].ToString();
            lblcreatedUser.Text = dt.Rows[1][5].ToString();

            lbl_Date_Verfy1.Text = dt.Rows[2][1].ToString();
            lbl_Date_Approval.Text = dt.Rows[2][2].ToString();
            lbl_Date_Verfy2.Text = dt.Rows[2][3].ToString();
            lbl_Date_Payment.Text = dt.Rows[2][4].ToString();
            lblCreatedOn.Text = dt.Rows[2][5].ToString();
        }
    }
}
