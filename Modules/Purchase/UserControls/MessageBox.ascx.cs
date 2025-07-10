using System;
using System.Collections;
using System.Configuration;
using System.Data;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class UserControls_MessageBox : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            dvBox.Visible = false; 
            dvError.Visible = false;
            dvSuccess.Visible = false;
            lblEMsg.Text = "";
            LblSMsg.Text = "";
        }
        public void ShowMessage(string Msg, bool IsError)
        {
            dvBox.Visible = true;
            dvError.Visible = IsError;
            dvSuccess.Visible = !(dvError.Visible);
            lblEMsg.Text = Msg;
            LblSMsg.Text = Msg;
        }
    }
